'--------Created By Monika 10/07/2014-----------BM00000003051
Imports common
Imports System.Data.SqlClient

Public Class FrmFormSerialNoMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmFormSerialNoMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        txtDocNo.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE()
        txtformcode.Value = ""
        txtformdesc.Text = ""
        txtformtype.Text = ""
        txtprefix.Text = ""
        txtstart_no.Text = ""
        txtend_no.Text = ""
        txttotal_no.Text = ""
        txtDocNo.MyReadOnly = False

        btnsave.Text = "&Save"
        btndelete.Enabled = False
    End Sub

    Private Sub FrmFormSerialNoMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtformcode.Value) <= 0 Then
                txtformcode.Focus()
                txtformcode.Select()
                Errorcontrol.SetError(txtformdesc, "Please Select Form Code")
                Throw New Exception("Please Select Form Code")
            Else
                Errorcontrol.ResetError(txtformdesc)
            End If

            If clsCommon.myLen(txtprefix.Text) <= 0 Then
                txtprefix.Focus()
                txtprefix.Select()
                Errorcontrol.SetError(txtprefix, "Please fill Prefix for Form Code")
                Throw New Exception("Please fill Prefix for Form Code")
            Else
                Errorcontrol.ResetError(txtprefix)
            End If


            '------------------------------
            Dim prfx As String = ""
            Dim strtno As Decimal = 0
            Dim endno As Decimal = 0
            Dim qry As String = "select  prefix,start_no,end_no from TSPL_FORM_SERIAL_NO_MASTER where form_code='" + txtformcode.Value + "' and doc_no <> '" + txtDocNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    prfx = clsCommon.myCstr(dr("prefix"))
                    strtno = clsCommon.myCdbl(dr("start_no"))
                    endno = clsCommon.myCdbl(dr("end_no"))

                    If clsCommon.CompairString(prfx, txtprefix.Text) = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(txtstart_no.Text) >= clsCommon.myCdbl(strtno) AndAlso clsCommon.myCdbl(txtstart_no.Text) <= clsCommon.myCdbl(endno)) Then
                        txtstart_no.Focus()
                        txtstart_no.Select()
                        Errorcontrol.SetError(txtstart_no, "Filled starting serial no. of Form is already in used.")
                        Throw New Exception("Filled starting serial no. of Form is already in used.")
                    Else
                        Errorcontrol.ResetError(txtstart_no)
                    End If

                    If clsCommon.CompairString(prfx, txtprefix.Text) = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(txtend_no.Text) >= clsCommon.myCdbl(strtno) AndAlso clsCommon.myCdbl(txtend_no.Text) <= clsCommon.myCdbl(endno)) Then
                        txtend_no.Focus()
                        txtend_no.Select()
                        Errorcontrol.SetError(txtend_no, "Filled end serial no. of Form is already in used.")
                        Throw New Exception("Filled end serial no. of Form is already in used.")
                    Else
                        Errorcontrol.ResetError(txtend_no)
                    End If
                Next
            End If
            '-------------------------------------------

            If clsCommon.myCdbl(txtstart_no.Text) <= 0 Then
                txtstart_no.Focus()
                txtstart_no.Select()
                Errorcontrol.SetError(txtstart_no, "Please fill starting serial no. of Form")
                Throw New Exception("Please fill starting serial no. of Form")
            Else
                Errorcontrol.ResetError(txtstart_no)
            End If

            If clsCommon.myCdbl(txtend_no.Text) <= 0 Then
                txtend_no.Focus()
                txtend_no.Select()
                Errorcontrol.SetError(txtend_no, "Please fill end serial no. of Form")
                Throw New Exception("Please fill end serial no. of Form")
            Else
                Errorcontrol.ResetError(txtend_no)
            End If

            If clsCommon.myCdbl(txtend_no.Text) <= clsCommon.myCdbl(txtstart_no.Text) Then
                txtend_no.Focus()
                txtend_no.Select()
                Errorcontrol.SetError(txtend_no, "Please fill end serial no. greater than start no.")
                Throw New Exception("Please fill end serial no. greater than start no.")
            Else
                Errorcontrol.ResetError(txtend_no)
            End If

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Sub SaveData()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmFormSerialNoMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New clsFormSerialNoMaster()
            obj.docno = clsCommon.myCstr(txtDocNo.Value)
            obj.docdate = clsCommon.myCDate(txtDate.Text)
            obj.formcode = clsCommon.myCstr(txtformcode.Value)
            obj.prefix = clsCommon.myCstr(txtprefix.Text)
            obj.startno = clsCommon.myCdbl(txtstart_no.Text)
            obj.endno = clsCommon.myCdbl(txtend_no.Text)
            obj.totalno = clsCommon.myCdbl(txttotal_no.Text)

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

            If clsFormSerialNoMaster.SaveData(obj, trans) Then
                If btnsave.Text = "&Save" Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If
                txtDocNo.Value = obj.docno
                txtDocNo.MyReadOnly = True
                btnsave.Text = "&Update"
                btndelete.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                txtDocNo.Focus()
                txtDocNo.Select()
                Errorcontrol.SetError(txtDocNo, "Please select document no. for deletion")
                Throw New Exception("Please select document no. for deletion")
            Else
                Errorcontrol.ResetError(txtDocNo)
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsFormSerialNoMaster.DeleteData(txtDocNo.Value, trans) Then
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtformcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtformcode._MYValidating
        Dim qry As String = "select form_code as Code,form_name as Description,form_type as [Form Type],Remarks,created_by as [Created By],created_date as [Created Date],modified_by as [Modified By],modified_date as [Modified Date] from tspl_form_master"
        txtformcode.Value = clsCommon.ShowSelectForm("FRMFND", qry, "Code", "", txtformcode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtformcode.Value) > 0 Then
            txtformdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from tspl_form_master where form_code='" + txtformcode.Value + "'"))
            txtformtype.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_type from tspl_form_master where form_code='" + txtformcode.Value + "'"))
        Else
            txtformcode.Value = ""
            txtformdesc.Text = ""
            txtformtype.Text = ""
        End If
    End Sub

    Sub LoadData(ByVal formcode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsFormSerialNoMaster = clsFormSerialNoMaster.GetData(txtDocNo.Value, NavType)

            If clsCommon.myLen(obj.docno) > 0 Then
                txtDocNo.Value = obj.docno
                txtDate.Text = obj.docdate
                txtformcode.Value = obj.formcode
                txtformdesc.Text = obj.formname
                txtformtype.Text = obj.formtype
                txtprefix.Text = obj.prefix
                txtstart_no.Text = obj.startno
                txtend_no.Text = obj.endno
                txttotal_no.Text = obj.totalno

                txtDocNo.MyReadOnly = True
                btnsave.Text = "&Update"
                btndelete.Enabled = True
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select count(*) from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + txtDocNo.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtDocNo.MyReadOnly = True
        Else
            txtDocNo.MyReadOnly = False
        End If

        If txtDocNo.MyReadOnly Or isButtonClicked Then
            txtDocNo.Value = clsFormSerialNoMaster.GetFinder("", txtDocNo.Value, isButtonClicked)
            LoadData(txtDocNo.Value, NavigatorType.Current)

        Else
            Reset()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from TSPL_FORM_SERIAL_NO_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select TSPL_FORM_SERIAL_NO_MASTER.doc_no as [Code],TSPL_FORM_SERIAL_NO_MASTER.doc_date as [Date],TSPL_FORM_SERIAL_NO_MASTER.form_code as [Form Code],tspl_form_master.form_name as [Form Name],tspl_form_master.form_type as [Form Type],TSPL_FORM_SERIAL_NO_MASTER.Prefix,TSPL_FORM_SERIAL_NO_MASTER.start_no as [Series Start At],TSPL_FORM_SERIAL_NO_MASTER.end_no as [End At],TSPL_FORM_SERIAL_NO_MASTER.total_form as [Total No of Form] from TSPL_FORM_SERIAL_NO_MASTER left outer join tspl_form_master on tspl_form_master.form_code=TSPL_FORM_SERIAL_NO_MASTER.form_code"
        Else
            qry = "select '' as [Code],'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "' as [Date],'' as [Form Code],'' as [Form Name],'' as [Form Type],'' as Prefix,0 as [Series Start At],0 as [End At],0 as [Total No of Form]"
        End If
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Code", "Date", "Form Code", "Form Name", "Form Type", "Prefix", "Series Start At", "End At", "Total No of Form") Then
            Dim obj As New clsFormSerialNoMaster()
            clsCommon.ProgressBarShow()
            Dim lineno As Integer = 1
            Dim qry As String = ""
            Dim check As Integer = 0
            Try
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsFormSerialNoMaster()
                    obj.docno = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.docno) <= 0 Or clsCommon.myLen(obj.docno) > 30 Then
                        Throw New Exception("Fill document no. and it does not excedd max.30 characters,see at line no. " + clsCommon.myCstr(lineno) + "")
                    End If

                    obj.docdate = clsCommon.myCDate(grow.Cells("Date").Value)
                    If clsCommon.myLen(obj.docdate) <= 0 Then
                        Throw New Exception("Fill document date,see at line no. " + clsCommon.myCstr(lineno) + "")
                    End If

                    obj.formcode = clsCommon.myCstr(grow.Cells("Form Code").Value)
                    obj.formname = clsCommon.myCstr(grow.Cells("Form Name").Value)
                    obj.formtype = clsCommon.myCstr(grow.Cells("Form Type").Value)
                    If clsCommon.myLen(obj.formcode) <= 0 AndAlso clsCommon.myLen(obj.formname) <= 0 AndAlso clsCommon.myLen(obj.formtype) <= 0 Then
                        Throw New Exception("Fill Form Code/Name/Type at line no. " + clsCommon.myCstr(lineno) + "")
                    ElseIf clsCommon.myLen(obj.formcode) > 0 Then
                        qry = "select count(*) from tspl_form_master where form_code='" + obj.formcode + "'"
                        check = clsDBFuncationality.getSingleValue(qry)

                        If check <= 0 Then
                            Throw New Exception("Filled Form Code does not exist,see at line no. " + clsCommon.myCstr(lineno) + "")
                        End If

                    ElseIf clsCommon.myLen(obj.formname) > 0 Then
                        qry = "select count(*) from tspl_form_master where form_name='" + obj.formname + "'"
                        check = clsDBFuncationality.getSingleValue(qry)

                        If check <= 0 Then
                            Throw New Exception("Filled Form Code/Name does not exist,see at line no. " + clsCommon.myCstr(lineno) + "")
                        End If
                    End If
                    If clsCommon.myLen(obj.formtype) > 0 AndAlso clsCommon.CompairString(obj.formtype, "C") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.formtype, "F") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.formtype, "38-Inward") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.formtype, "38-Outward") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.formtype, "Others") <> CompairStringResult.Equal Then
                        Throw New Exception("From Type should any of from C/F/38-Inward/38-Outward Form Code/Name does not exist,see at line no. " + clsCommon.myCstr(lineno) + "")
                    End If

                    obj.prefix = clsCommon.myCstr(grow.Cells("Prefix").Value)
                    If clsCommon.myLen(obj.prefix) <= 0 AndAlso clsCommon.myLen(obj.prefix) > 3 Then
                        Throw New Exception("Fill Prefix at line no. " + clsCommon.myCstr(lineno) + ",and it should not exceed max.3 characters.")
                    End If

                    obj.startno = clsCommon.myCdbl(grow.Cells("Series Start At").Value)
                    If clsCommon.myCdbl(obj.startno) <= 0 Then
                        Throw New Exception("Series Start No should not zero(0) at line no. " + clsCommon.myCstr(lineno) + "")
                    End If

                    obj.endno = clsCommon.myCdbl(grow.Cells("End At").Value)
                    If clsCommon.myCdbl(obj.endno) <= 0 Then
                        Throw New Exception("Series End No should not zero(0) at line no. " + clsCommon.myCstr(lineno) + "")
                    End If
                    If clsCommon.myCdbl(obj.endno) <= clsCommon.myCdbl(obj.startno) Then
                        Throw New Exception("Series End No should be greater than start no. at line no. " + clsCommon.myCstr(lineno) + "")
                    End If

                    obj.totalno = clsCommon.myCdbl(obj.endno) - clsCommon.myCdbl(obj.startno) 'clsCommon.myCdbl(grow.Cells("Total No of Form").Value)

                    If clsCommon.myLen(obj.docno) > 0 Then
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        If clsFormSerialNoMaster.SaveData(obj, trans) Then
                        End If
                    End If

                    lineno += 1
                Next


                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub FrmFormSerialNoMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyData = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub txtend_no_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtend_no.TextChanged
        If clsCommon.myCdbl(txtend_no.Text) > 0 Then
            txttotal_no.Text = (clsCommon.myCdbl(txtend_no.Text) - clsCommon.myCdbl(txtstart_no.Text)) + 1
        End If
    End Sub

    Private Sub txtstart_no_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtstart_no.TextChanged
        If clsCommon.myCdbl(txtstart_no.Text) > 0 Then
            txttotal_no.Text = (clsCommon.myCdbl(txtend_no.Text) - clsCommon.myCdbl(txtstart_no.Text)) + 1
        End If
    End Sub
End Class
