Imports System.ComponentModel
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine

'created by --> Vipin
'createddate --> 17/06/2011
'modifiedby --> Vipin
'Modified date -->17/06/2011
'Tables Used --> TSPL_TDS_DEDUCTION_HEAD,TSPL_TDS_DEDUCTION_DETAILS
'--preeti gupta..ticket no.[BM00000003134]
Public Class frmNatureOfDeduction
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.NatureOfDeduction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            MenuImport.Enabled = True
            menuExport.Enabled = True
        Else
            MenuImport.Enabled = False
            menuExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmNatureOfDeduction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub
    'Main Form Load
    Private Sub frmNatureOfDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ToolTipNDeduction.SetToolTip(btnnew, "New")
        Fnd_DeductionNew.MyCharacterCasing = CharacterCasing.Upper
        ddldeduction.Enabled = True
        btndelete.Enabled = False
        btnsave.Enabled = True
        txtcum.Enabled = False
        txtCuttoffDocument.Enabled = False
        SetLength()
        If clsCommon.myLen(Me.Tag) > 0 Then
            Fnd_DeductionNew.Value = clsCommon.myCstr(Me.Tag)
            LoadData()
        End If
    End Sub
    Public Sub SetLength()
        Fnd_DeductionNew.MyMaxLength = 12
        txtdes.MaxLength = 50
        txtremark.MaxLength = 200

    End Sub
    'This will work on Drop Down List Items change
    Private Sub ddldeduction_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddldeduction.TextChanged
        If (ddldeduction.Text = "Percentage") Then
            dgvdeduction.Columns(3).HeaderText = "TDS % "
            dgvdeduction.Columns(4).HeaderText = "Surcharge %"
            dgvdeduction.Columns(5).HeaderText = "Edu.Cess %"
            dgvdeduction.Columns(6).HeaderText = "Sec.Edu.Cess %"
            dgvdeduction.Columns(7).HeaderText = "Non PAN TDS % "
        ElseIf (ddldeduction.Text = "Amount") Then
            dgvdeduction.Columns(3).HeaderText = "TDS Amount"
            dgvdeduction.Columns(4).HeaderText = "Surcharge Amount"
            dgvdeduction.Columns(5).HeaderText = "Edu.Cess Amount"
            dgvdeduction.Columns(6).HeaderText = "Sec.Edu.Cess Amount"
            dgvdeduction.Columns(7).HeaderText = "Non PAN TDS Amount"
        End If
    End Sub
    Public Sub LoadData()
        Try
            Dim str As String = " select deduction_code from TSPL_TDS_DEDUCTION_HEAD where deduction_code='" + Fnd_DeductionNew.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
            If strvalue <> "" Then
                funfill()
            Else
                txtdes.Text = ""
                fndTdsNew.Value = ""
                txttdsdes.Text = ""
                txtcum.Text = "0.00"
                txtmdate.Text = ""
                txtremark.Text = ""
                txtmdate.Text = "  /    /"
                ddldeduction.Text = "Percentage"
                chkinactive.Checked = False
                ddldeduction.Enabled = True
                fnd_GL_Account.Value = ""
                dgvdeduction.DataSource = Nothing
                dgvdeduction.Rows.Clear()
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
#Region "Function"
    'Funtion for insertion of data
    Public Sub funinsert()
        Dim trans As SqlTransaction = Nothing
        Try
            connectSql.OpenConnection()
            trans = connectSql.OpenConnection.BeginTransaction()
            Dim strchk As String = ""
            If chkinactive.Checked = True Then
                strchk = "Y"

            ElseIf chkinactive.Checked = False Then
                strchk = "N"

            End If

            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" & Fnd_DeductionNew.Value & "'", trans)
                If ChkNewEntry = 0 Then
                    Fnd_DeductionNew.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.NatureOfDeduction, "", "")
                    If clsCommon.myLen(Fnd_DeductionNew.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If

            connectSql.RunSpTransaction(trans, "SP_DEDUCTION_HEAD_INSERT", New SqlParameter("@deduction_code", Fnd_DeductionNew.Value), New SqlParameter("@description", txtdes.Text.ToString()), New SqlParameter("@TDS_Section", fndTdsNew.Value), New SqlParameter("@cumm_cutoff", txtcum.Text.ToString()), New SqlParameter("@percent_Amount", ddldeduction.Text.ToString()), New SqlParameter("@inactive", strchk), New SqlParameter("@comment", txtremark.Text.ToString()), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode), New SqlParameter("@Gl_Account", fnd_GL_Account.Value))
            '' Anubhooti 16-Sep-2014 BM00000003934
            UpdateOtherColumns(trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(Fnd_DeductionNew.Value), "TSPL_TDS_DEDUCTION_HEAD", "Deduction_Code", trans)

            UpdateOtherColumnsDetails(trans)
            trans.Commit()
            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
            'If objCommonVar.CurrentUserCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    Sub UpdateOtherColumnsDetails(ByVal trans As SqlTransaction)
        For i As Integer = 0 To dgvdeduction.Rows.Count - 1
            connectSql.RunSpTransaction(trans, "SP_TDS_DEDUCTION_DETAILS_INSERT", New SqlParameter("@Detail_Line_No", dgvdeduction.Rows(i).Cells(0).Value), New SqlParameter("@Deduction_Code", Fnd_DeductionNew.Value), New SqlParameter("@From_Range", dgvdeduction.Rows(i).Cells(1).Value), New SqlParameter("@To_Range", dgvdeduction.Rows(i).Cells(2).Value), New SqlParameter("@TDS", dgvdeduction.Rows(i).Cells(3).Value), New SqlParameter("@Surcharge", dgvdeduction.Rows(i).Cells(4).Value), New SqlParameter("@Educess", dgvdeduction.Rows(i).Cells(5).Value), New SqlParameter("@Seceducess", dgvdeduction.Rows(i).Cells(6).Value), New SqlParameter("@TDSNonPAN", dgvdeduction.Rows(i).Cells(7).Value))
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(Fnd_DeductionNew.Value), "TSPL_TDS_DEDUCTION_DETAIL", "Deduction_Code", trans)
        Next
    End Sub
    Sub UpdateOtherColumns(ByVal trans As SqlTransaction)
        '' Anubhooti 16-Sep-2014 BM00000003934
        Dim strNonPAN As String = ""
        If ChkNonPAN.Checked = True Then
            strNonPAN = "Y"
        ElseIf ChkNonPAN.Checked = False Then
            strNonPAN = "N"
        End If
        clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_TDS_DEDUCTION_HEAD SET Non_PAN_No='" & strNonPAN & "',Cumm_Cutoff_Document='" + txtCuttoffDocument.Text + "',IsBuyerFileReturnInLastTwoYears=" + IIf(chkbuyerfilereturnlasttwoyear.Checked, "1", "0") + ",IsTCS_TDSAmountGreaterThan50KPreviousYear=" + IIf(chkTCSTDSamountgreater50KpreviousYear.Checked, "1", "0") + ",Min_Service_Per=" + clsCommon.myCstr(txtMinServicePer.Value) + " WHERE Deduction_Code='" & clsCommon.myCstr(Fnd_DeductionNew.Value) & "'", trans)
    End Sub
    'Funtion for updation  of data
    Public Sub funupdate()
        Dim trans As SqlTransaction = Nothing
        Try
            connectSql.OpenConnection()
            trans = connectSql.OpenConnection.BeginTransaction()
            Dim strchk As String = ""
            If chkinactive.Checked = True Then
                strchk = "Y"
            ElseIf chkinactive.Checked = False Then
                strchk = "N"

            End If

            connectSql.RunSpTransaction(trans, "SP_DEDUCTION_HEAD_UPDATE", New SqlParameter("@deduction_code", Fnd_DeductionNew.Value), New SqlParameter("@description", txtdes.Text.ToString()), New SqlParameter("@TDS_Section", fndTdsNew.Value), New SqlParameter("@cumm_cutoff", txtcum.Text.ToString()), New SqlParameter("@percent_Amount", ddldeduction.Text.ToString()), New SqlParameter("@inactive", strchk), New SqlParameter("@comment", txtremark.Text.ToString()), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode), New SqlParameter("@Gl_Account", fnd_GL_Account.Value))
            UpdateOtherColumns(trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(Fnd_DeductionNew.Value), "TSPL_TDS_DEDUCTION_HEAD", "Deduction_Code", trans)
            connectSql.RunSpTransaction(trans, "SP_TDS_DEDUCTION_DETAILS_DELETE", New SqlParameter("@deduction_code", Fnd_DeductionNew.Value))
            UpdateOtherColumnsDetails(trans)
            trans.Commit()
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    'Function for deletion of data
    Public Sub fundelete()
        Try
            connectSql.RunSp("SP_DEDUCTION_HEAD_DELETE", New SqlParameter("@deduction_code", Fnd_DeductionNew.Value))
            connectSql.RunSp("SP_TDS_DEDUCTION_DETAILS_DELETE", New SqlParameter("@deduction_code", Fnd_DeductionNew.Value))
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Try
            Dim str As String = " select deduction_code,description,TDS_Section,Cumm_Cutoff,Percent_Amount,Inactive,Comment,Modify_Date,Gl_account,Non_PAN_No,isnull(Cumm_Cutoff_Document,0) as Cumm_Cutoff_Document,TSPL_TDS_DEDUCTION_HEAD.IsBuyerFileReturnInLastTwoYears,TSPL_TDS_DEDUCTION_HEAD.IsTCS_TDSAmountGreaterThan50KPreviousYear,TSPL_TDS_DEDUCTION_HEAD.Min_Service_Per from TSPL_TDS_DEDUCTION_HEAD where deduction_code='" + Fnd_DeductionNew.Value + "' "
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Fnd_DeductionNew.Value = dt.Rows(i)("deduction_code")
                    txtdes.Text = dt.Rows(i)("description")
                    fndTdsNew.Value = dt.Rows(i)("TDS_Section")
                    Dim TdsDesc As String = "select description ,Cumulative_Cutoff from TSPL_TDS_SECTION_MASTER where tds_group ='" & fndTdsNew.Value & "'"
                    Dim TDSDesc1 As String = clsDBFuncationality.getSingleValue(TdsDesc)
                    If TDSDesc1.Length > 0 Then
                        txttdsdes.Text = TDSDesc1
                    Else
                        txttdsdes.Text = ""
                    End If

                    txtcum.Text = dt.Rows(i)("Cumm_Cutoff")
                    Dim strddl As String = dt.Rows(i)("Percent_Amount")
                    If strddl = "P" Then
                        ddldeduction.Text = "Percentage"
                    ElseIf strddl = "A" Then
                        ddldeduction.Text = "Amount"
                    End If

                    Dim strchk As String = dt.Rows(i)("Inactive")
                    If strchk = "Y" Then
                        chkinactive.Checked = True
                    ElseIf strchk = "N" Then
                        chkinactive.Checked = False
                    End If
                    txtremark.Text = dt.Rows(i)("Comment")
                    txtmdate.Text = dt.Rows(i)("Modify_Date")
                    fnd_GL_Account.Value = dt.Rows(i)("Gl_account")
                    '' Anubhooti 16-Sep-2014 BM00000003934
                    Dim strNonPAN As String = clsCommon.myCstr(dt.Rows(i)("Non_PAN_No"))
                    If strNonPAN = "Y" Then
                        ChkNonPAN.Checked = True
                    ElseIf strNonPAN = "N" Then
                        ChkNonPAN.Checked = False
                    End If
                    txtCuttoffDocument.Text = clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(i)("Cumm_Cutoff_Document")))
                    txtMinServicePer.Value = clsCommon.myCdbl(dt.Rows(i)("Min_Service_Per"))
                    chkbuyerfilereturnlasttwoyear.Checked = (clsCommon.myCdbl(dt.Rows(i)("IsBuyerFileReturnInLastTwoYears")) > 0)
                    chkTCSTDSamountgreater50KpreviousYear.Checked = (clsCommon.myCdbl(dt.Rows(i)("IsTCS_TDSAmountGreaterThan50KPreviousYear")) > 0)
                Next
            End If


            dgvdeduction.AutoGenerateColumns = False
            Dim strcmd As String = "select Detail_Line_No,Deduction_Code,From_Range,To_Range,TDS,Surcharge,Educess,Seceducess,TDS_Non_PAN  from TSPL_TDS_DEDUCTION_DETAIL where Deduction_Code = '" + Fnd_DeductionNew.Value + "'"
            transportSql.FillGridView(strcmd, dgvdeduction)
            dgvdeduction.Columns(0).FieldName = "Detail_Line_No"

            dgvdeduction.Columns(1).FieldName = "From_Range"
            dgvdeduction.Columns(2).FieldName = "To_Range"
            dgvdeduction.Columns(3).FieldName = "TDS"
            dgvdeduction.Columns(4).FieldName = "Surcharge"
            dgvdeduction.Columns(5).FieldName = "Educess"
            dgvdeduction.Columns(6).FieldName = "Seceducess"
            dgvdeduction.Columns(7).FieldName = "TDS_Non_PAN"
            ddldeduction.Enabled = False
            btnsave.Enabled = True
            btndelete.Enabled = True
            btnsave.Text = "Update"
            'If objCommonVar.CurrentUserCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub funreset()

        Fnd_DeductionNew.Value = ""
        txtdes.Text = ""
        fndTdsNew.Value = ""
        txttdsdes.Text = ""
        txtcum.Text = "0.000"
        txtcum.Enabled = False
        txtCuttoffDocument.Text = "0.00"
        txtCuttoffDocument.Enabled = False
        txtmdate.Text = "  /    /"
        txtremark.Text = ""
        ddldeduction.Text = "Percentage"
        chkinactive.Checked = False
        ChkNonPAN.Checked = False
        fnd_GL_Account.Value = ""
        chkTCSTDSamountgreater50KpreviousYear.Checked = False
        chkbuyerfilereturnlasttwoyear.Checked = False
        txtMinServicePer.Value = 0
        dgvdeduction.DataSource = Nothing
        dgvdeduction.Rows.Clear()
        ddldeduction.Enabled = True
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
#End Region

#Region "Button Click"
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso Fnd_DeductionNew.Value = "" Then
                myMessages.blankValue("Nature of Deduction")
                Fnd_DeductionNew.Focus()
            ElseIf txtdes.Text = "" Then
                myMessages.blankValue("Description")
                txtdes.Focus()
            ElseIf fndTdsNew.Value = "" Then
                myMessages.blankValue("TDS Section")
                fndTdsNew.Focus()
            ElseIf fnd_GL_Account.Value = "" Then
                myMessages.blankValue("GL Account")
                fnd_GL_Account.Focus()
            Else

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.NatureOfDeduction, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If Fnd_DeductionNew.Value = "" Then
            myMessages.blankValue("Nature of Deduction")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            'myMessages.delete()
            'btnsave.Text = "Save"
            'btndelete.Enabled = False
        End If
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub
#End Region
    'This code will give the default values grid cells
    Private Sub dgvdeduction_DefaultValuesNeeded(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles dgvdeduction.DefaultValuesNeeded
        Dim grow As GridViewNewRowInfo = DirectCast(e.Row, GridViewNewRowInfo)
        grow.Cells(0).Value = dgvdeduction.RowCount + 1

        If dgvdeduction.RowCount > 0 Then


            Dim preMaxRange As Decimal = CDec(dgvdeduction.Rows(dgvdeduction.RowCount - 1).Cells(2).Value)

            If preMaxRange = 999999999 Then
                grow.Cells(1).Value = "0"
                grow.Cells(2).Value = "999999999"
                grow.Cells(3).Value = "0.00"
                grow.Cells(4).Value = "0.00"
                grow.Cells(5).Value = "0.00"
                grow.Cells(6).Value = "0.00"
            Else
                grow.Cells(1).Value = preMaxRange + 1
                grow.Cells(2).Value = "999999999"
                grow.Cells(3).Value = "0.00"
                grow.Cells(4).Value = "0.00"
                grow.Cells(5).Value = "0.00"
                grow.Cells(6).Value = "0.00"
            End If

        Else
            grow.Cells(1).Value = 0
            grow.Cells(2).Value = "999999999"
            grow.Cells(3).Value = "0.00"
            grow.Cells(4).Value = "0.00"
            grow.Cells(5).Value = "0.00"
            grow.Cells(6).Value = "0.00"
        End If


    End Sub
    ' For Export Functionality
    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        Dim str As String = " select m.deduction_code as [Deduction Code],m.description as [Description],m.TDS_Section as [TDS Section],m.Cumm_Cutoff as [Cumulative Cuttoff],m.Percent_Amount as [Percent/Amount],Inactive as [Status],m.comment as [Remark],m.Gl_account as [GL_Account] ,d.detail_line_no as [Line No],d.deduction_code as [Details Deduction Code],d.from_range as [From Range],d.to_range as [To Range],d.tds as [TDS],d.surcharge as [Surcharge],d.educess as [EDU Cess],d.seceducess as [Sec Edu Cess],Non_PAN_No AS [Non PAN No],m.Cumm_Cutoff_Document as [Cumulative Cuttoff Document]  from TSPL_TDS_DEDUCTION_HEAD m join TSPL_TDS_DEDUCTION_DETAIL d on m.deduction_code = d.deduction_code"
        transportSql.ExporttoExcel(str, Me)
    End Sub
    ' For Import Functionality
    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Deduction Code", "Description", "TDS Section", "Cumulative Cuttoff", "Percent/Amount", "Status", "Remark", "GL_Account", "Line No", "Details Deduction Code", "From Range", "To Range", "TDS", "Surcharge", "EDU Cess", "Sec Edu Cess", "Non PAN No", "Cumulative Cuttoff Document") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strcode As String = grow.Cells(0).Value.ToString()
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 12 Then
                        Throw New Exception("Deduction Code can not be blank or Check the length")
                    End If

                    Dim strdes As String = grow.Cells(1).Value.ToString()
                    If (String.IsNullOrEmpty(strdes)) Or clsCommon.myLen(strdes) > 50 Then
                        Throw New Exception("Description can not be blank or Check the length")
                    End If

                    Dim strtdssec As String = grow.Cells(2).Value.ToString()
                    If (String.IsNullOrEmpty(strtdssec)) Or clsCommon.myLen(strtdssec) > 12 Then
                        Throw New Exception("TDS Deduction Code can not be blank or Check the length")
                    End If

                    Dim strcumu As String = grow.Cells(3).Value.ToString()
                    If clsCommon.myLen(strcumu) > 20 Then
                        Throw New Exception("Check the length of Cumulative Cutoff")
                    End If

                    Dim strcumuDoc As String = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Cumulative Cuttoff Document").Value))


                    Dim strpercentage As String = grow.Cells(4).Value.ToString()
                    If clsCommon.myLen(strpercentage) > 20 Then
                        Throw New Exception("Check the length of Percentage/Amount")
                    End If

                    Dim strstatus As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If (strstatus = "Y" Or strstatus = "N") Then
                    Else
                        Throw New Exception("Value for Inactive should be  Y or N")
                        Exit Sub
                    End If

                    Dim strremark As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If clsCommon.myLen(strremark) > 150 Then
                        Throw New Exception("Check the length of Remark")
                    End If
                    Dim Gl_Account As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If (clsCommon.myLen(Gl_Account) > 50) Or (String.IsNullOrEmpty(Gl_Account)) Then
                        Throw New Exception("GL_ACCOUNT can not be blank or Check the length")
                    End If

                    Dim strline As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If (String.IsNullOrEmpty(strline)) Or clsCommon.myLen(strline) > 12 Then
                        Throw New Exception("Line No. can not be blank or Check the length")
                    End If

                    Dim strdetailcode As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If (String.IsNullOrEmpty(strdetailcode)) Or clsCommon.myLen(strdetailcode) > 12 Then
                        Throw New Exception("Details Deduction Code can not be blank or Check the length")
                    End If

                    Dim strfrom As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If clsCommon.myLen(strfrom) > 20 Then
                        Throw New Exception("Check the length of From Range")
                    End If



                    Dim strto As String = clsCommon.myCstr(grow.Cells(11).Value)
                    If clsCommon.myLen(strto) > 20 Then
                        Throw New Exception("Check the length of To Range")
                    End If

                    Dim strTDS As String = clsCommon.myCstr(grow.Cells(12).Value)
                    If clsCommon.myLen(strTDS) > 20 Then
                        Throw New Exception("Check the length of TDS")
                    End If

                    Dim strsurcharge As String = clsCommon.myCstr(grow.Cells(13).Value)
                    If clsCommon.myLen(strsurcharge) > 20 Then
                        Throw New Exception("Check the length of Surcharge")
                    End If

                    Dim stredu As String = clsCommon.myCstr(grow.Cells(14).Value)
                    If clsCommon.myLen(stredu) > 20 Then
                        Throw New Exception("Check the length of EduSec")
                    End If

                    Dim strsecedu As String = clsCommon.myCstr(grow.Cells(15).Value)
                    If clsCommon.myLen(strsecedu) > 20 Then
                        Throw New Exception("Check the length of SecEduCess")
                    End If

                    '' Anubhooti 16-Sep-2014 BM00000003934
                    Dim strNonPAN As String = clsCommon.myCstr(grow.Cells("Non PAN No").Value)
                    If clsCommon.myLen(strNonPAN) > 0 Then
                        If clsCommon.CompairString(strNonPAN.ToUpper().Trim(), "Y") = CompairStringResult.Equal Or clsCommon.CompairString(strNonPAN.ToUpper().Trim(), "N") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Non PAN No should be between 'Y' or 'N'.")
                        End If
                    End If
                    strNonPAN = strNonPAN.ToUpper().Trim()
                    ''
                    Dim sql1 As String = "select count(*) from TSPL_TDS_DEDUCTION_HEAD where deduction_code='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then

                        connectSql.RunSpTransaction(trans, "SP_DEDUCTION_HEAD_INSERT", New SqlParameter("@deduction_code", strcode), New SqlParameter("@description", strdes), New SqlParameter("@TDS_Section", strtdssec), New SqlParameter("@cumm_cutoff", strcumu), New SqlParameter("@percent_Amount", strpercentage), New SqlParameter("@inactive", strstatus), New SqlParameter("@comment", strremark), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode), New SqlParameter("@Gl_Account", Gl_Account))
                        '' Anubhooti 16-Sep-2014 BM00000003934
                        clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_TDS_DEDUCTION_HEAD SET Non_PAN_No='" & strNonPAN & "',Cumm_Cutoff_Document='" + strcumuDoc + "' WHERE Deduction_Code='" & clsCommon.myCstr(strcode) & "'", trans)
                    Else
                        connectSql.RunSpTransaction(trans, "SP_DEDUCTION_HEAD_UPDATE", New SqlParameter("@deduction_code", strcode), New SqlParameter("@description", strdes), New SqlParameter("@TDS_Section", strtdssec), New SqlParameter("@cumm_cutoff", strcumu), New SqlParameter("@percent_Amount", strpercentage), New SqlParameter("@inactive", strstatus), New SqlParameter("@comment", strremark), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode), New SqlParameter("@Gl_Account", Gl_Account))
                        '' Anubhooti 16-Sep-2014 BM00000003934
                        clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_TDS_DEDUCTION_HEAD SET Non_PAN_No='" & strNonPAN & "',Cumm_Cutoff_Document='" + strcumuDoc + "' WHERE Deduction_Code='" & clsCommon.myCstr(strcode) & "'", trans)

                    End If
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strcode), "TSPL_TDS_DEDUCTION_HEAD", "Deduction_Code", trans)


                    Dim sql2 As String = "select count(*) from TSPL_TDS_DEDUCTION_DETAIL where deduction_code='" + strcode + "' and detail_line_no='" + strline + "'"
                    Dim int As Integer = CInt(connectSql.RunScalar(trans, sql2))
                    If (int = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TDS_DEDUCTION_DETAILS_INSERT", New SqlParameter("@Detail_Line_No", strline), New SqlParameter("@Deduction_Code", strdetailcode), New SqlParameter("@From_Range", strfrom), New SqlParameter("@To_Range", strto), New SqlParameter("@TDS", strTDS), New SqlParameter("@Surcharge", strsurcharge), New SqlParameter("@Educess", stredu), New SqlParameter("@Seceducess", strsecedu))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TDS_DEDUCTION_DETAILS_UPDATE", New SqlParameter("@Detail_Line_No", strline), New SqlParameter("@Deduction_Code", strdetailcode), New SqlParameter("@From_Range", strfrom), New SqlParameter("@To_Range", strto), New SqlParameter("@TDS", strTDS), New SqlParameter("@Surcharge", strsurcharge), New SqlParameter("@Educess", stredu), New SqlParameter("@Seceducess", strsecedu))
                    End If
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strcode), "TSPL_TDS_DEDUCTION_DETAIL", "Deduction_Code", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    'For Validation in grid cells
    Private Sub dgvdeduction_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvdeduction.CellValueChanged
        'This code will chech the range og percentage,if percentage become greater then 100 ,it will give a message

        If e.ColumnIndex = 3 Then
            Dim preMaxRange As Decimal = CDec(dgvdeduction.CurrentRow.Cells(3).Value)
            If ddldeduction.Text = "Percentage" Then
                If preMaxRange > 100 Then
                    common.clsCommon.MyMessageBoxShow("Percent  can not be greater then 100 ")
                    dgvdeduction.CurrentRow.Cells(3).Value = 0
                End If
            End If

        ElseIf e.ColumnIndex = 4 Then
            Dim preMaxRange1 As Decimal = CDec(dgvdeduction.CurrentRow.Cells(4).Value)
            If ddldeduction.Text = "Percentage" Then
                If preMaxRange1 > 100 Then
                    common.clsCommon.MyMessageBoxShow("Percent  can not be greater then 100 ")
                    dgvdeduction.CurrentRow.Cells(4).Value = 0
                End If
            End If
        ElseIf e.ColumnIndex = 5 Then

            Dim preMaxRange2 As Decimal = CDec(dgvdeduction.CurrentRow.Cells(5).Value)
            If ddldeduction.Text = "Percentage" Then
                If preMaxRange2 > 100 Then
                    common.clsCommon.MyMessageBoxShow("Percent  can not be greater then 100 ")
                    dgvdeduction.CurrentRow.Cells(5).Value = 0
                End If
            End If
        ElseIf e.ColumnIndex = 6 Then
            Dim preMaxRange3 As Decimal = CDec(dgvdeduction.CurrentRow.Cells(6).Value)
            If ddldeduction.Text = "Percentage" Then
                If preMaxRange3 > 100 Then
                    common.clsCommon.MyMessageBoxShow("Percent  can not be greater then 100 ")
                    dgvdeduction.CurrentRow.Cells(6).Value = 0
                End If
            End If
        End If

        'This code will check the range of From date ,which should be greater then To Date
        If e.ColumnIndex = 2 Then
            Dim precellvalue As Decimal = CDec(dgvdeduction.CurrentRow.Cells(1).Value)
            If (dgvdeduction.CurrentRow.Cells(2).Value < precellvalue) Then
                common.clsCommon.MyMessageBoxShow(" 'From Range' cannot be greater then 'To Range' ")
                dgvdeduction.CurrentRow.Cells(2).Value = 999999999

            End If
        End If

    End Sub
    'File menu close event
    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub
    Private Sub dgvdeduction_UserAddingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvdeduction.UserAddingRow

    End Sub
    Private Sub fndTdsNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTdsNew._MYValidating
        Dim Qry As String = "select tds_group as [Code],description as [Description] from TSPL_TDS_SECTION_MASTER"
        fndTdsNew.Value = clsCommon.ShowSelectForm("TDSSectionnew", Qry, "Code", "", fndTdsNew.Value, "Code", isButtonClicked)
        Dim TdsDesc As String = "select description ,Cumulative_Cutoff from TSPL_TDS_SECTION_MASTER where tds_group ='" & fndTdsNew.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(TdsDesc)
        If dt.Rows.Count > 0 Then
            txttdsdes.Text = dt.Rows(0)("description")
            Dim strchk As String = dt.Rows(0)("Cumulative_Cutoff")
            If strchk = "Y" Then
                txtcum.Enabled = True
                txtCuttoffDocument.Enabled = True
                txtcum.Text = "0.00"
                txtCuttoffDocument.Text = "0.00"
            ElseIf strchk = "N" Then
                txtcum.Enabled = False
                txtCuttoffDocument.Enabled = False
                txtcum.Text = "0.00"
                txtCuttoffDocument.Text = "0.00"
            End If
        Else
            txtdes.Text = ""
        End If
    End Sub
    Private Sub Fnd_DeductionNew__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles Fnd_DeductionNew._MYNavigator
        Dim qry As String = "select deduction_code ,description  as [Description]from TSPL_TDS_DEDUCTION_HEAD where   2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_TDS_DEDUCTION_HEAD   .deduction_code  in ('" + Fnd_DeductionNew.Value + "')"
            Case NavigatorType.Next
                qry += " and TSPL_TDS_DEDUCTION_HEAD   .deduction_code  in (select min(deduction_code ) from TSPL_TDS_DEDUCTION_HEAD  where deduction_code  >'" + Fnd_DeductionNew.Value + "')"
            Case NavigatorType.First
                qry += " and TSPL_TDS_DEDUCTION_HEAD   .deduction_code  in (select MIN(deduction_code ) from TSPL_TDS_DEDUCTION_HEAD )"

            Case NavigatorType.Last
                qry += " and TSPL_TDS_DEDUCTION_HEAD   .deduction_code  in (select Max(deduction_code ) from TSPL_TDS_DEDUCTION_HEAD )"
            Case NavigatorType.Previous
                qry += " and TSPL_TDS_DEDUCTION_HEAD   .deduction_code  in (select Max(deduction_code  ) from TSPL_TDS_DEDUCTION_HEAD  where deduction_code  <'" + Fnd_DeductionNew.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Fnd_DeductionNew.Value = clsCommon.myCstr(dt.Rows(0)("deduction_code"))
        End If

        LoadData()
    End Sub
    Private Sub Fnd_DeductionNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Fnd_DeductionNew._MYValidating

        Dim Qry1 As String = " Select Count(*) From TSPL_TDS_DEDUCTION_HEAD where deduction_code='" & Fnd_DeductionNew.Value & "'"
        Dim Count As String = clsDBFuncationality.getSingleValue(Qry1)
        If Count = 0 Then
            Fnd_DeductionNew.MyReadOnly = False
        Else
            Fnd_DeductionNew.MyReadOnly = True
        End If
        If Fnd_DeductionNew.MyReadOnly Or isButtonClicked Then
            Fnd_DeductionNew.Value = clsNatureOfDeduction.getFinder("", Fnd_DeductionNew.Value, isButtonClicked)
            Fnd_DeductionNew.MyMaxLength = 12
        End If
        LoadData()
    End Sub
    Private Sub Fnd_DeductionNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Fnd_DeductionNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub fnd_GL_Account__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnd_GL_Account._MYValidating
        Dim qry As String = "select  Account_code as Code,Description as [Description]from TSPL_GL_ACCOUNTS "
        fnd_GL_Account.Value = clsCommon.ShowSelectForm("GLAccounts", qry, "Code", "", fnd_GL_Account.Value, "Code", isButtonClicked)
    End Sub
    Private Sub dgvdeduction_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvdeduction.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub txtCuttoffDocument_Validating(sender As Object, e As CancelEventArgs) Handles txtCuttoffDocument.Validating
        txtCuttoffDocument.Text = clsCommon.myCstr(clsCommon.myCdbl(txtCuttoffDocument.Text))
    End Sub
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(Fnd_DeductionNew.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Nature of Deduction")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(Fnd_DeductionNew.Value, "Deduction_Code", "TSPL_TDS_DEDUCTION_HEAD", "TSPL_TDS_DEDUCTION_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
