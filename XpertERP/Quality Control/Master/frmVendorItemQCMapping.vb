''created by Monika 19/02/2015
Imports common
Imports System.Data.SqlClient


Public Class FrmVendorItemQCMapping
    Inherits FrmMainTranScreen

#Region "variables"
    Dim ButtonToolTip As New ToolTip()
    Dim Errorcontrol As New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False

    Const colLineno As String = "Lineno"
    Const colItemCode As String = "ItemCode"
    Const colIname As String = "iname"
    Const colUnit As String = "Unit"
    Const colParamCode As String = "Paramcode"
    Const colParamdesc As String = "Paramdesc"
    Const colNature As String = "nature"
    Const colremarks As String = "remarks"
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVendorItemQCMapping)

        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmVendorItemQCMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                btnclose.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
                FunReset()
            End If

            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colItemCode) Then
                isCellValueChanged = True
                OpenIcode(True)
                isCellValueChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colParamCode) Then
                isCellValueChanged = True
                OpenParameter(True)
                isCellValueChanged = False
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub FrmVendorItemQCMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save/update record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N for refresh screen.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
    End Sub

    Private Sub FunReset()
        fndCode.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtdesc.Text = ""
        fndVendor_code.Value = ""
        TxtVendor_desc.Text = ""

        gv1.Rows.Clear()
        gv1.Rows.AddNew()

        fndCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        isNewEntry = True
        fndVendor_code.Enabled = True

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        txtdesc.Focus()
        txtdesc.Select()
    End Sub

    Private Sub LoadBlankGrid()
        gv1.Columns.Clear()
        gv1.Rows.Clear()

        Dim icode As New GridViewTextBoxColumn()

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colLineno
        icode.HeaderText = "S.No."
        icode.ReadOnly = True
        icode.Width = 60
        gv1.MasterTemplate.Columns.Add(icode)

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colItemCode
        icode.HeaderText = "Item Code"
        icode.Width = 110
        icode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        icode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(icode)

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colIname
        icode.HeaderText = "Description"
        icode.ReadOnly = True
        icode.Width = 250
        gv1.MasterTemplate.Columns.Add(icode)

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colUnit
        icode.HeaderText = "UOM"
        icode.ReadOnly = True
        icode.Width = 80
        gv1.MasterTemplate.Columns.Add(icode)

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colParamCode
        icode.HeaderText = "Parameter Code"
        icode.Width = 100
        icode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        icode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(icode)

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colParamdesc
        icode.HeaderText = "Parameter Description"
        icode.ReadOnly = True
        icode.Width = 200
        icode.WrapText = True
        gv1.MasterTemplate.Columns.Add(icode)

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colNature
        icode.HeaderText = "Nature"
        icode.ReadOnly = True
        icode.Width = 100
        gv1.MasterTemplate.Columns.Add(icode)

        icode = New GridViewTextBoxColumn()
        icode.FormatString = ""
        icode.Name = colremarks
        icode.HeaderText = "Remarks"
        icode.Width = 120
        icode.MaxLength = 200
        gv1.MasterTemplate.Columns.Add(icode)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 25

        icode = Nothing
    End Sub

    Private Sub OpenIcode(ByVal isButtonClicked As Boolean)
        Dim icode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)

        icode = clsItemMaster.getFinder(" active='1'", icode, isButtonClicked)
        gv1.CurrentRow.Cells(colItemCode).Value = icode
        gv1.CurrentRow.Cells(colIname).Value = clsItemMaster.GetItemName(icode, Nothing)
        gv1.CurrentRow.Cells(colUnit).Value = clsItemMaster.GetStockUnit(icode, Nothing)
        gv1.CurrentRow.Cells(colParamCode).Value = Nothing
        gv1.CurrentRow.Cells(colParamdesc).Value = Nothing
        gv1.CurrentRow.Cells(colNature).Value = Nothing
    End Sub

    Private Sub OpenParameter(ByVal isButtonClicked As Boolean)
        Dim frm As New FrmCheckBoxGrid()
        frm.qry = "Select (Code+' '+Description) as Value from TSPL_QC_LOG_SHEET_MASTER where trans_id='STANDARD'"
        frm.ShowDialog()
        Dim paramcode As String = ""

        Dim XR As Integer = gv1.CurrentRow.Index
        Dim counter As Integer = 0
        If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
            For Each Str As String In frm.arrValue
                If counter = 0 Then
                    paramcode = Str.Substring(0, Str.IndexOf(" "))
                    gv1.Rows(XR).Cells(colParamCode).Value = paramcode
                    gv1.Rows(XR).Cells(colParamdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_QC_LOG_SHEET_MASTER where trans_id='STANDARD' and code='" + paramcode + "'"))
                    gv1.Rows(XR).Cells(colNature).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when nature='A' then'Alphanumeric' else case when nature='R' then 'Range' else case when nature='B' then 'Boolean' else case when nature='M' then 'Mannual Input' end end end end) as nature from TSPL_QC_LOG_SHEET_MASTER where trans_id='STANDARD' and code='" + paramcode + "'"))

                    counter += 1
                    gv1.Rows.AddNew()
                Else
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineno).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(gv1.Rows(XR).Cells(colItemCode).Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIname).Value = clsCommon.myCstr(gv1.Rows(XR).Cells(colIname).Value)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(gv1.Rows(XR).Cells(colUnit).Value)
                    paramcode = Str.Substring(0, Str.IndexOf(" "))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colParamCode).Value = paramcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colParamdesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_QC_LOG_SHEET_MASTER where trans_id='STANDARD' and code='" + paramcode + "'"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNature).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when nature='A' then'Alphanumeric' else case when nature='R' then 'Range' else case when nature='B' then 'Boolean' else case when nature='M' then 'Mannual Input' end end end end) as nature from TSPL_QC_LOG_SHEET_MASTER where trans_id='STANDARD' and code='" + paramcode + "'"))

                    gv1.Rows.AddNew()
                End If
                
            Next
        Else
            gv1.Rows(XR).Cells(colParamCode).Value = Nothing
            gv1.Rows(XR).Cells(colParamdesc).Value = Nothing
            gv1.Rows(XR).Cells(colNature).Value = Nothing
        End If

        '=============refresh grid===========
        Dim icode As String = ""
        Dim paramcode1 As String = ""
        For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
            icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
            paramcode1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colParamCode).Value)

            If clsCommon.myLen(icode) <= 0 AndAlso clsCommon.myLen(paramcode1) <= 0 Then
                gv1.Rows.RemoveAt(ii)
            End If
        Next

        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                grow.Cells(colLineno).Value = grow.Index + 1
            End If
        Next
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select document code first.", Me.Text)
                fndCode.Focus()
                fndCode.Select()
                Errorcontrol.SetError(fndCode, "Select document code first.")
                Exit Sub
            Else
                Errorcontrol.ResetError(fndCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsVendorItemQCMapping.DeleteData(fndCode.Value) Then
                    myMessages.delete()
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        FunReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndVendor_code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Select vendor detail.", Me.Text)
                fndVendor_code.Focus()
                fndVendor_code.Select()
                Errorcontrol.SetError(TxtVendor_desc, "Select vendor detail.")
                Return False
            Else
                Errorcontrol.ResetError(TxtVendor_desc)
            End If

            Dim qry As String = "select document_code from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD where document_code<>'" + fndCode.Value + "' and vendor_code='" + fndVendor_code.Value + "'"
            Dim check As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            If clsCommon.myLen(check) > 0 Then
                Throw New Exception("Vendor is used,see document code:" + check + "")
            End If

            Dim icode As String = ""
            Dim paramcode As String = ""
            Dim oldicode As String = ""
            Dim oldparamcode As String = ""

            icode = clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value)

            If clsCommon.myLen(icode) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Fill atleast one row.", Me.Text)
                gv1.CurrentRow = gv1.Rows(0)
                Return False
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
                paramcode = clsCommon.myCstr(gv1.Rows(ii).Cells(colParamCode).Value)

                If clsCommon.myLen(icode) > 0 Then
                    If clsCommon.myLen(paramcode) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        clsCommon.MyMessageBoxShow("Select parameter at row no. " + clsCommon.myCstr(ii + 1) + "")
                        gv1.CurrentRow = gv1.Rows(ii)
                        Return False
                    End If

                    For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                        oldicode = clsCommon.myCstr(gv1.Rows(jj).Cells(colItemCode).Value)
                        oldparamcode = clsCommon.myCstr(gv1.Rows(jj).Cells(colParamCode).Value)

                        If clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(paramcode, oldparamcode) = CompairStringResult.Equal Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            clsCommon.MyMessageBoxShow("Duplicate data at row no. " + clsCommon.myCstr(jj + 1) + "")
                            gv1.CurrentRow = gv1.Rows(jj)
                            Return False
                        End If
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVendorItemQCMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim obj As New clsVendorItemQCMapping()
        Dim objtr As New clsVendorItemQCMappingDetail()
        Try
            obj.Document_Code = clsCommon.myCstr(fndCode.Value)
            obj.Description = clsCommon.myCstr(txtdesc.Text).Replace("'", "`")
            obj.Doc_Date = clsCommon.myCDate(txtDate.Text)
            obj.Vendor_Code = clsCommon.myCstr(fndVendor_code.Value)

            obj.Arr = New List(Of clsVendorItemQCMappingDetail)

            For Each grow As GridViewRowInfo In gv1.Rows
                objtr = New clsVendorItemQCMappingDetail()

                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.Line_No = clsCommon.myCstr(grow.Cells(colLineno).Value)
                objtr.Nature = clsCommon.myCstr(grow.Cells(colNature).Value)
                If clsCommon.CompairString(objtr.Nature, "Range") = CompairStringResult.Equal Then
                    objtr.Nature = "R"
                End If
                If clsCommon.CompairString(objtr.Nature, "Boolean") = CompairStringResult.Equal Then
                    objtr.Nature = "B"
                End If
                If clsCommon.CompairString(objtr.Nature, "Alphanumeric") = CompairStringResult.Equal Then
                    objtr.Nature = "A"
                End If
                If clsCommon.CompairString(objtr.Nature, "Mannual Input") = CompairStringResult.Equal Then
                    objtr.Nature = "M"
                End If
                objtr.Parameter_Code = clsCommon.myCstr(grow.Cells(colParamCode).Value)
                objtr.Remarks = clsCommon.myCstr(grow.Cells(colremarks).Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next

            If obj.Arr.Count <= 0 Then
                Throw New Exception("No data found to save.")
            End If

            If clsVendorItemQCMapping.SaveData(obj, isNewEntry) Then
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                fndCode.Value = obj.Document_Code

                UcAttachment1.SaveData(fndCode.Value)

                LoadData(fndCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            objtr = Nothing
            obj = Nothing
        End Try
    End Sub

    Private Sub fndCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        LoadData(fndCode.Value, NavType)
    End Sub

    Private Sub fndCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCode._MYValidating
        'Ticket No  UDL/16/05/19-000296 ,Sanjay
        Dim qry As String = "select count(*) from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD where document_code='" + fndCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check > 0 Then
            fndCode.MyReadOnly = True
        Else
            fndCode.MyReadOnly = False
        End If

        If fndCode.MyReadOnly OrElse isButtonClicked Then
            fndCode.Value = clsVendorItemQCMapping.GetFinder("", fndCode.Value, isButtonClicked)
            LoadData(fndCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsVendorItemQCMapping()

        Try
            obj = clsVendorItemQCMapping.GetData(strCode, NavType)

            gv1.Rows.Clear()
            gv1.Rows.AddNew()
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                isNewEntry = False
                isInsideLoadData = True

                fndCode.Value = obj.Document_Code
                txtDate.Text = obj.Doc_Date
                txtdesc.Text = obj.Description
                fndVendor_code.Value = obj.Vendor_Code
                TxtVendor_desc.Text = obj.Vendor_Name

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsVendorItemQCMappingDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineno).Value = objtr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIname).Value = objtr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNature).Value = objtr.Nature
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colParamCode).Value = objtr.Parameter_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colParamdesc).Value = objtr.Parameter_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colremarks).Value = objtr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Unit_Code

                        gv1.Rows.AddNew()
                    Next
                End If

                UcAttachment1.LoadData(fndCode.Value)

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                fndCode.MyReadOnly = True
                fndVendor_code.Enabled = False
            Else
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
        End Try
    End Sub

    Private Sub fndVendor_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendor_code._MYValidating
        fndVendor_code.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Status='N'", fndVendor_code.Value, isButtonClicked)
        TxtVendor_desc.Text = clsVendorMaster.GetName(fndVendor_code.Value, Nothing)
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv1.Columns(colItemCode) Then
                        isCellValueChanged = True
                        OpenIcode(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv1.Columns(colParamCode) Then
                        isCellValueChanged = True
                        OpenParameter(False)
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineno).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class
