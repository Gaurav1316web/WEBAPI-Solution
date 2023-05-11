'==========created by Monika BM00000003447 
Imports common
Imports System.Data.SqlClient


Public Class FrmCHAChargeMaster
    Inherits FrmMainTranScreen

#Region "variables"
    Dim ButtonToolTip As New ToolTip()
    Dim ErrorControl As New clsErrorControl()
    Dim isNewEntry As Boolean = True
#End Region


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCHAChargeMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnsave.Visible = True Then
            btnexport.Enabled = True
            btnimport.Enabled = True
        Else
            btnexport.Enabled = False
            btnimport.Enabled = False
        End If
        '--------------------------------------------------
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadComboBox()
        Dim xvlue As String = ""

        Dim frm As New FrmCheckBoxGrid()
        frm.qry = "select * from (select 'Dry Port-ICD(Inland Container Depot)' as Value"
        frm.qry += " union all select 'Sea Port' as Value"
        frm.qry += " union all select 'Terminal Handling Charges at ICD& Sea Port' as Value"
        frm.qry += " union all select 'Inland Haulage Charges at ICD& Sea Port' as Value"
        frm.qry += " union all select 'Other' as Value)a"
        frm.arrValue = New List(Of String)
        While (clsCommon.myLen(cmbChargeType.Text) > 0)
            If Not cmbChargeType.Text.Contains(",") Then
                xvlue = cmbChargeType.Text
                frm.arrValue.Add(xvlue)
                cmbChargeType.Text = ""
            Else
                xvlue = cmbChargeType.Text.Substring(0, cmbChargeType.Text.IndexOf(","))
                frm.arrValue.Add(xvlue)
                If clsCommon.myLen(cmbChargeType.Text) > 0 Then
                    cmbChargeType.Text = cmbChargeType.Text.Replace(xvlue + ",", "")
                End If
            End If

        End While
        frm.ShowDialog()

        txtCHACode.Text = ""
        cmbChargeType.Text = ""

        If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
            For Each Str As String In frm.arrValue
                cmbChargeType.Text = cmbChargeType.Text + "," + Str
                If clsCommon.CompairString(Str, "Dry Port-ICD(Inland Container Depot)") = CompairStringResult.Equal Then
                    txtCHACode.Text = txtCHACode.Text + ",ICD"
                ElseIf clsCommon.CompairString(Str, "Sea Port") = CompairStringResult.Equal Then
                    txtCHACode.Text = txtCHACode.Text + ",ISD"
                ElseIf clsCommon.CompairString(Str, "Terminal Handling Charges at ICD& Sea Port") = CompairStringResult.Equal Then
                    txtCHACode.Text = txtCHACode.Text + ",THC"
                ElseIf clsCommon.CompairString(Str, "Inland Haulage Charges at ICD& Sea Port") = CompairStringResult.Equal Then
                    txtCHACode.Text = txtCHACode.Text + ",IHC"
                ElseIf clsCommon.CompairString(Str, "Other") = CompairStringResult.Equal Then
                    txtCHACode.Text = txtCHACode.Text + ",OTH"
                End If
            Next
        End If

        If clsCommon.myLen(cmbChargeType.Text) > 0 AndAlso cmbChargeType.Text.Substring(0, 1) = "," Then
            cmbChargeType.Text = cmbChargeType.Text.Substring(1, clsCommon.myLen(cmbChargeType.Text) - 1)
        End If
        If clsCommon.myLen(txtCHACode.Text) > 0 AndAlso txtCHACode.Text.Substring(0, 1) = "," Then
            txtCHACode.Text = txtCHACode.Text.Substring(1, clsCommon.myLen(txtCHACode.Text) - 1)
        End If
    End Sub

    Private Sub FunReset()
        txtCode.Value = ""
        txtdesc.Text = ""
        txtCHACode.Text = ""
        dtpdate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtrate.Text = Nothing
        cmbChargeType.Text = ""

        txtCode.MyReadOnly = False
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnsave.Text = "Save"
        isNewEntry = True

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        RadPageView1.SelectedPage = RadPageViewPage1

        txtdesc.Focus()
        txtdesc.Select()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myCdbl(txtrate.Text) <= 0 Then
                txtrate.Focus()
                txtrate.Select()
                ErrorControl.SetError(txtrate, "Fill amount")
                Throw New Exception("Fill amount")
            Else
                ErrorControl.ResetError(txtrate)
            End If

            If clsCommon.CompairString(cmbChargeType.Text, "") = CompairStringResult.Equal Then
                cmbChargeType.Select()
                cmbChargeType.Focus()
                ErrorControl.SetError(cmbChargeType, "Select CHA Charge Type")
                Throw New Exception("Select CHA Charge Type")
            Else
                ErrorControl.ResetError(cmbChargeType)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmCHAChargeMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim obj As New clsCHAChargeMaster()
        Dim objtr As New clsCHAChargeMaster()
        Try
            obj = New clsCHAChargeMaster()
            obj.arr = New List(Of clsCHAChargeMaster)

            Dim xsplit() As String = Nothing
            xsplit = txtCHACode.Text.Split(",")

            If xsplit IsNot Nothing AndAlso xsplit.Length > 0 Then
                For Each Str As String In xsplit
                    objtr = New clsCHAChargeMaster()

                    objtr.DocNo = clsCommon.myCstr(txtCode.Value)
                    objtr.DocDate = clsCommon.myCDate(dtpdate.Text)
                    objtr.Doc_Desc = clsCommon.myCstr(txtdesc.Text).Replace("'", "`")
                    objtr.CHA_Type = Str
                    objtr.amount = clsCommon.myCdbl(txtrate.Text)

                    If clsCommon.myLen(objtr.CHA_Type) > 0 Then
                        obj.arr.Add(objtr)
                    End If
                Next
            End If


            If clsCHAChargeMaster.SaveData(txtCode.Value, obj, isNewEntry) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)

                txtCode.Value = obj.DocNo
                UcAttachment1.SaveData(txtCode.Value)

                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            objtr = Nothing
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsCHAChargeMaster()
        Try
            obj = clsCHAChargeMaster.GetData(strCode, NavType)
            FunReset()
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.arr.Count) > 0 Then
                For Each objtr As clsCHAChargeMaster In obj.arr
                    txtCode.Value = objtr.DocNo
                    txtdesc.Text = objtr.Doc_Desc
                    dtpdate.Text = objtr.DocDate
                    txtCHACode.Text = txtCHACode.Text + "," + objtr.CHA_Type
                    cmbChargeType.Text = cmbChargeType.Text + "," + clsCHAChargeMaster.GetCHATypeDescription(objtr.CHA_Type)
                    txtrate.Text = objtr.amount
                Next

                If clsCommon.myLen(cmbChargeType.Text) > 0 AndAlso cmbChargeType.Text.Substring(0, 1) = "," Then
                    cmbChargeType.Text = cmbChargeType.Text.Substring(1, clsCommon.myLen(cmbChargeType.Text) - 1)
                End If
                If clsCommon.myLen(txtCHACode.Text) > 0 AndAlso txtCHACode.Text.Substring(0, 1) = "," Then
                    txtCHACode.Text = txtCHACode.Text.Substring(1, clsCommon.myLen(txtCHACode.Text) - 1)
                End If

                isNewEntry = False
                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True

                UcAttachment1.LoadData(txtCode.Value)

                txtdesc.Focus()
                txtdesc.Select()
            Else
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub FrmCHAChargeMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            clsERPFuncationality.closeForm(Me)
        End If
    End Sub

    Private Sub FrmCHAChargeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()

        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for Refresh window.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for Close window.")
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                ErrorControl.SetError(txtCode, "Select document for deletion.")
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Select document for deletion.")
            Else
                ErrorControl.ResetError(txtCode)
            End If

            If Not myMessages.deleteConfirm() Then
                Exit Sub
            End If

            Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_vendor_master where cha_doc_no='" + txtCode.Value + "'")
            If check > 0 Then
                Throw New Exception("Record is in used,no data deleted.")
            End If

            If clsCHAChargeMaster.DeleteData(txtCode.Value) Then
                myMessages.delete()
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "select count(*) from TSPL_CHA_CHARGE_MASTER where doc_no='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
            End If

            If txtCode.MyReadOnly OrElse isButtonClicked Then
                txtCode.Value = clsCHAChargeMaster.GetFinder("", txtCode.Value, isButtonClicked)

                If clsCommon.myLen(txtCode.Value) > 0 Then
                    LoadData(txtCode.Value, NavigatorType.Current)
                Else
                    FunReset()
                End If
            Else
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from TSPL_CHA_CHARGE_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select doc_no as [Code],doc_date as [Date],[Description],(case when cha_type='ICD' then 'Dry Port-ICD(Inland Container Depot)' else case when cha_type='ISD' then 'Sea Port' else case when cha_type='THC' then 'Terminal Handling Charges at ICD& Sea Port' else case when cha_type='IHC' then 'Inland Haulage Charges at ICD& Sea Port' else case when cha_type='OTH' then 'Other' else 'None' end end end end end) as [CHA Type],Amount from TSPL_CHA_CHARGE_MASTER"
        Else
            qry = "select '' as [Code],'' as [Date],'' as [Description],'[Dry Port-ICD(Inland Container Depot)] Or [Sea Port] Or [Terminal Handling Charges at ICD& Sea Port] Or [Inland Haulage Charges at ICD& Sea Port] Or [Other]' as [CHA Type],0 as Amount"
        End If

        transportSql.ExporttoExcel(qry, "", "", Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim coll As New Hashtable()
        Dim obj As New clsCHAChargeMaster()
        Try
            Dim qry As String = ""
            Dim check As Integer = 0
            Dim isSaved As Boolean = True

            If transportSql.importExcel(gv, "Code", "Date", "Description", "CHA Type", "Amount") Then
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsCHAChargeMaster()

                    obj.DocNo = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.DocNo) <= 0 Then
                        isNewEntry = True
                    ElseIf clsCommon.myLen(obj.DocNo) > 0 Then
                        qry = "select count(*) from TSPL_CHA_CHARGE_MASTER where doc_no='" + obj.DocNo + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Code value is invalid at line no." + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                        isNewEntry = False
                    End If

                    Try
                        obj.DocDate = clsCommon.myCDate(grow.Cells("Date").Value)
                    Catch exx As Exception
                        obj.DocDate = clsCommon.GETSERVERDATE(trans)
                    End Try
                    If clsCommon.myLen(obj.DocDate) <= 0 Then
                        obj.DocDate = clsCommon.GETSERVERDATE(trans)
                    End If

                    obj.Doc_Desc = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                    If obj.Doc_Desc IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Desc) > 200 Then
                        Throw New Exception("Description length exceed max. character length is 200 at line no." + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    obj.CHA_Type = clsCommon.myCstr(grow.Cells("CHA Type").Value)
                    If clsCommon.myLen(obj.CHA_Type) <= 0 Then
                        Throw New Exception("Fill CHA Type at line no." + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.CompairString(obj.CHA_Type, "Dry Port-ICD(Inland Container Depot)") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.CHA_Type, "Sea Port") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.CHA_Type, "Terminal Handling Charges at ICD& Sea Port") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.CHA_Type, "Inland Haulage Charges at ICD& Sea Port") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.CHA_Type, "Other") <> CompairStringResult.Equal Then
                        Throw New Exception("CHA Type should be any one from 1.Dry Port-ICD(Inland Container Depot)" + Environment.NewLine + "2.Sea Port" + Environment.NewLine + "3.Terminal Handling Charges at ICD& Sea Port" + Environment.NewLine + "4.Inland Haulage Charges at ICD& Sea Port" + Environment.NewLine + "5.Other" + Environment.NewLine + " at line no." + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    obj.CHA_Type = clsCHAChargeMaster.GetCHAType(obj.CHA_Type)

                    obj.amount = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    If clsCommon.myCdbl(obj.amount) <= 0 Then
                        Throw New Exception("Fill amount at line no." + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    '===============insert=====================
                    If isNewEntry Then
                        obj.DocNo = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.DocDate, clsDocType.CHACHARGEMASTER, "", ""))
                    End If

                    clsCommon.AddColumnsForChange(coll, "Doc_No", obj.DocNo)
                    clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(obj.DocDate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "description", obj.Doc_Desc)
                    clsCommon.AddColumnsForChange(coll, "cha_type", obj.CHA_Type)
                    clsCommon.AddColumnsForChange(coll, "amount", obj.amount)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                    If isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CHA_CHARGE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CHA_CHARGE_MASTER", OMInsertOrUpdate.Update, " doc_no='" + obj.DocNo + "' and cha_type='" + obj.CHA_Type + "' ", trans)
                    End If
                Next

                clsCommon.ProgressBarHide()

                trans.Commit()
                If isSaved Then
                    clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("No Data Found To Transfer", Me.Text)
                End If
                FunReset()
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            Me.Controls.Remove(gv)
            coll = New Hashtable()
            obj = Nothing
        End Try

    End Sub

    Private Sub cmbChargeType_DoubleClick(sender As Object, e As EventArgs) Handles cmbChargeType.DoubleClick
        LoadComboBox()
    End Sub
End Class
