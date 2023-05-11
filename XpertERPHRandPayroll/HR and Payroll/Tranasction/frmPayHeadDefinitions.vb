'--27/06/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmPayHeadDefinitions
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub frmPayHeadDefinitions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        cboArrearType.Visible = False
        lblMaxHRA.Visible = False
        TxtMaxHRA.Visible = False
        lblEmpGLACC.Visible = False
        Me.fndEmpGLAcc.Visible = False
        Me.txtEmpGLAcc.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsPayHeadDefinitions()
            obj.PAY_HEAD_CODE = txtCode.Value
            obj.PAY_HEAD_NAME = txtPayHeadName.Text
            obj.PRINT_NAME = txtPrintName.Text
            obj.HEAD_TYPE = CboPayHeadType.SelectedValue
            obj.SUB_HEAD_TYPE = cboPayHeadCategory.SelectedValue
            If clsCommon.CompairString(obj.SUB_HEAD_TYPE, "HRA") = CompairStringResult.Equal Then
                obj.MaximumHRA = TxtMaxHRA.Value
            Else
                obj.MaximumHRA = 0
            End If
            obj.PERIODICITY = cboPeriodicity.SelectedValue
            obj.CALC_BASIS = cboCalcBasis.SelectedValue
            obj.ROUND_OFF_TYPE = cboRoundOffType.SelectedValue
            obj.GROUP_SEQ = clsCommon.myCdbl(txtSerialNo.Text)
            obj.PRINT_GROUP_SEQ = clsCommon.myCdbl(txtPrintSerialNo.Text)
            obj.Deduction_Code = clsCommon.myCstr(fndNatureOfDeduction.Value)
            If chkIsEarning.Checked Then
                obj.ISEARNING = True
            Else
                obj.ISEARNING = False
            End If
            obj.Do_Not_Include_In_Gross_Salary_For_Over_Time = chkDoNotIncludeInGrossSalaryForOverTime.Checked ''BHA/13/03/19-000839 by balwinder on 09/04/2019
            If clsCommon.CompairString(obj.SUB_HEAD_TYPE, "Arrear") = CompairStringResult.Equal Then
                obj.ARREAR_TYPE = cboArrearType.SelectedValue
            Else
                obj.ARREAR_TYPE = ""
            End If

            obj.IsHiddenComponent = ChkHiddenComponents.Checked
            If chkFullnFinal.Checked Then
                obj.IsFullnFinal = True
            Else
                obj.IsFullnFinal = False
            End If
            ' Ticket No : BHA/31/12/18-000766 By Prabhakar
            If chkTDSExempted.Checked Then
                obj.IsTDSExempted = True
            Else
                obj.IsTDSExempted = False
            End If
            If chkCreateAPInvoice.Checked Then
                obj.IsCreateAPInvoice = True
            Else
                obj.IsCreateAPInvoice = False
            End If

            obj.Account_Code = clsCommon.myCstr(Me.fndGLAccount.Value)
            obj.GL_Employer_Account = clsCommon.myCstr(Me.fndEmpGLAcc.Value)
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.PAY_HEAD_CODE, NavigatorType.Current)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsPayHeadDefinitions()
        obj = clsPayHeadDefinitions.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.PAY_HEAD_CODE
            txtPrintName.Text = obj.PRINT_NAME
            txtPayHeadName.Text = obj.PAY_HEAD_NAME
            CboPayHeadType.SelectedValue = obj.HEAD_TYPE
            isInsideLoadData = True
            cboPayHeadCategory.DataSource = GetcboSubHeadTypeDataTable(obj.SUB_HEAD_TYPE)

            cboPayHeadCategory.ValueMember = "Code"
            cboPayHeadCategory.DisplayMember = "Description"
            isInsideLoadData = False
            cboPayHeadCategory.SelectedValue = obj.SUB_HEAD_TYPE
            TxtMaxHRA.Value = obj.MaximumHRA

            If clsCommon.CompairString(obj.SUB_HEAD_TYPE, "HRA") = CompairStringResult.Equal Then
                lblMaxHRA.Visible = True
                TxtMaxHRA.Visible = True
            Else
                lblMaxHRA.Visible = False
                TxtMaxHRA.Visible = False
            End If

            cboArrearType.DataSource = GetcboArrearTypeDataTable(obj.ARREAR_TYPE)
            cboArrearType.ValueMember = "Code"
            cboArrearType.DisplayMember = "Name"
            cboArrearType.SelectedValue = obj.ARREAR_TYPE

            cboPeriodicity.SelectedValue = obj.PERIODICITY
            cboCalcBasis.SelectedValue = obj.CALC_BASIS
            cboRoundOffType.SelectedValue = obj.ROUND_OFF_TYPE
            txtSerialNo.Text = obj.GROUP_SEQ
            txtPrintSerialNo.Text = obj.PRINT_GROUP_SEQ
            If obj.ISEARNING Then
                chkIsEarning.Checked = True
            Else
                chkIsEarning.Checked = False
            End If
            ChkHiddenComponents.Checked = obj.IsHiddenComponent
            If obj.IsFullnFinal Then
                chkFullnFinal.Checked = True
            Else
                chkFullnFinal.Checked = False
            End If
            If obj.IsTDSExempted Then
                chkTDSExempted.Checked = True
            Else
                chkTDSExempted.Checked = False
            End If
            If obj.IsCreateAPInvoice Then
                chkCreateAPInvoice.Checked = True
            Else
                chkCreateAPInvoice.Checked = False
            End If
            Me.fndGLAccount.Value = obj.Account_Code
            Me.txtGLAccountDesc.Text = obj.Description
            Me.fndEmpGLAcc.Value = obj.GL_Employer_Account
            fndNatureOfDeduction.Value = obj.Deduction_Code
            lblNatureOfDeduction.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" & fndNatureOfDeduction.Value & "'"))
            Me.txtEmpGLAcc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(fndEmpGLAcc.Value) + "'"))
            chkDoNotIncludeInGrossSalaryForOverTime.Checked = obj.Do_Not_Include_In_Gross_Salary_For_Over_Time
            SUBHEAD()
        End If

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Pay Head Code ")
            txtCode.Focus()
            Return False


        ElseIf clsCommon.myCstr(txtCode.Value).Contains("_") = True Then
            clsCommon.MyMessageBoxShow("Pay Head must not contain (_) ie. underscore.")
            txtCode.Focus()
            Return False


        ElseIf clsCommon.myLen(txtPayHeadName.Text) <= 0 Then
            myMessages.blankValue("Pay Head Name")
            txtPayHeadName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtPrintName.Text) <= 0 Then
            myMessages.blankValue("Print Name")
            txtPrintName.Focus()
            Return False
        ElseIf CboPayHeadType.SelectedIndex <= -1 Then
            myMessages.blankValue("Pay Head Type")
            CboPayHeadType.Focus()
            Return False
        ElseIf cboPayHeadCategory.SelectedIndex <= -1 Then
            myMessages.blankValue(" Sub Pay Head Type")
            cboPayHeadCategory.Focus()
            Return False
        ElseIf cboPeriodicity.SelectedIndex <= -1 Then
            myMessages.blankValue(" Periodicity")
            cboPeriodicity.Focus()
            Return False
        ElseIf cboCalcBasis.SelectedIndex <= -1 Then
            myMessages.blankValue(" Calculation Basis")
            cboCalcBasis.Focus()
            Return False
        ElseIf cboRoundOffType.SelectedIndex <= -1 Then
            myMessages.blankValue(" Round Off Type")
            cboRoundOffType.Focus()
            Return False
        End If
        If cboPayHeadCategory.ValueMember = "Arrear" And cboArrearType.SelectedIndex <= -1 Then
            myMessages.blankValue("Arrear Type")
            cboArrearType.Focus()
            Return False

        End If
        Dim strCode As String = clsPayHeadDefinitions.CheckNameExistness(txtPayHeadName.Text, txtCode.Value, Nothing)
        If clsCommon.myLen(strCode) > 0 Then
            clsCommon.MyMessageBoxShow("Name Allready Exist in Pay Period Code : " + strCode + ". Please Choose another  Name.")
            Return False
        End If
        If clsCommon.myCdbl(txtSerialNo.Text) <= 0 Or clsCommon.myCdbl(txtSerialNo.Text) > 99 Then
            clsCommon.MyMessageBoxShow("Sequence No must be 1 to 99")
            Return False
        End If
        Dim Pay_Head_Code As String = clsPayHeadDefinitions.ValidatePayHeadSequence(txtCode.Value, txtSerialNo.Text, IIf(chkIsEarning.Checked, 1, 0))
        If clsCommon.myLen(Pay_Head_Code) > 0 Then
            clsCommon.MyMessageBoxShow("Invalid Sequence no for Pay Head " & txtCode.Value & ". Pay Head  " & Pay_Head_Code & " already contains sequence no " & txtSerialNo.Text & "")
            Return False
        End If

        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsPayHeadDefinitions.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPayHeadDefinitions)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        '' Anubhooti 24-July-2014 BM00000003183
        RadMenuItem3.Enabled = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtPrintName.Text = Nothing
        txtPayHeadName.Text = Nothing
        chkIsEarning.Checked = False
        ChkHiddenComponents.Checked = False
        chkFullnFinal.Checked = False
        chkTDSExempted.Checked = False
        chkCreateAPInvoice.Checked = False
        txtSerialNo.Text = ""
        txtPrintSerialNo.Text = ""
        CboPayHeadType.DataSource = GetcboHeadTypeDataTable()
        CboPayHeadType.ValueMember = "Code"
        CboPayHeadType.DisplayMember = "Name"
        CboPayHeadType.SelectedIndex = -1
        isInsideLoadData = True
        cboPayHeadCategory.DataSource = GetcboSubHeadTypeDataTable("")

        cboPayHeadCategory.ValueMember = "Code"
        cboPayHeadCategory.DisplayMember = "Description"
        isInsideLoadData = False
        cboPayHeadCategory.SelectedIndex = -1

        cboCalcBasis.DataSource = GetcboCalculationBasisDataTable()
        cboCalcBasis.ValueMember = "Code"
        cboCalcBasis.DisplayMember = "Name"
        cboCalcBasis.SelectedIndex = -1

        cboRoundOffType.DataSource = clsFixedParameter.GetCboDataTable("CboRound", Nothing)
        cboRoundOffType.ValueMember = "Code"
        cboRoundOffType.DisplayMember = "DESCRIPTION"
        cboRoundOffType.SelectedIndex = -1

        cboPeriodicity.DataSource = clsFixedParameter.GetCboDataTable("cboPeriodicity", Nothing)
        cboPeriodicity.ValueMember = "Code"
        cboPeriodicity.DisplayMember = "DESCRIPTION"
        cboPeriodicity.SelectedIndex = -1
        cboArrearType.Visible = False
        lblMaxHRA.Visible = False
        TxtMaxHRA.Visible = False
        TxtMaxHRA.Value = 0
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        fndEmpGLAcc.Value = ""
        txtEmpGLAcc.Text = ""
        fndNatureOfDeduction.Value = ""
        lblNatureOfDeduction.Text = ""
        chkDoNotIncludeInGrossSalaryForOverTime.Checked = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select PAY_HEAD_CODE as Code, PAY_HEAD_NAME as Name, PRINT_NAME as 'Print Name' ,HEAD_TYPE as 'Pay Head Type', SUB_HEAD_TYPE as 'Sub Pay Head Type', PERIODICITY as 'Periodicity', CALC_BASIS as 'Calculation Basis', ROUND_OFF_TYPE as 'Round Off Type',convert(int,TSPL_PAYHEAD_MASTER.ISEARNING) as 'Is Earning' ,TSPL_PAYHEAD_MASTER.Account_Code as 'GL Account Head' ,TSPL_GL_ACCOUNTS.Description 'GL Account Head Description' from TSPL_PAYHEAD_MASTER left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PAYHEAD_MASTER.Account_Code  "
            txtCode.Value = clsCommon.ShowSelectForm("PAY_HEAD", qry, "Code", "", txtCode.Value, "PAY_HEAD_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmPayHeadDefinitions_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim ArrStr As String() = {"Code", "Name", "Print Name", "Pay Head Type", "Sub Pay Head Type", "Periodicity", "Calculation Basis", "Round Off Type", "Is Earning", "Is Hidden", "Account Code", "Sequence", "Print Sequence", "Arrear Type", "Employer Gl Account"}
        If transportSql.importExcel(gv, ArrStr) Then
            'Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsPayHeadDefinitions()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception(" Code can not be blank or incorrect.")
                    End If
                    obj.PAY_HEAD_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells(1).Value)
                    If strCode.Length > 100 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Name can not be blank or incorrect.")
                    End If
                    obj.PAY_HEAD_NAME = strCode

                    strCode = clsCommon.myCstr(grow.Cells(2).Value)
                    If strCode.Length > 50 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Print Name can not be blank or incorrect.")
                    End If
                    obj.PRINT_NAME = strCode

                    strCode = clsCommon.myCstr(grow.Cells(3).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Pay Head Type can not be blank or incorrect.")
                    End If
                    obj.HEAD_TYPE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Arrear Type").Value)
                    'If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                    '    Throw New Exception("Arrear Type can not be blank or incorrect.")
                    'End If
                    obj.ARREAR_TYPE = strCode

                    strCode = clsCommon.myCstr(grow.Cells(4).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Sub Pay Head Type can not be blank or incorrect.")
                    End If
                    obj.SUB_HEAD_TYPE = strCode

                    strCode = clsCommon.myCstr(grow.Cells(5).Value)
                    If strCode.Length > 10 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Periodicity can not be blank or incorrect.")
                    End If
                    obj.PERIODICITY = strCode

                    strCode = clsCommon.myCstr(grow.Cells(6).Value)
                    If strCode.Length > 100 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Calculation Basis can not be blank or incorrect.")
                    End If
                    obj.CALC_BASIS = strCode

                    strCode = clsCommon.myCstr(grow.Cells(7).Value)
                    If strCode.Length > 10 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Round Off Type can not be blank or incorrect.")
                    End If
                    obj.ROUND_OFF_TYPE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Is Earning").Value)
                    If clsCommon.myCdbl(strCode) > 1 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Is Earing must be 0 or 1.")
                    End If
                    obj.ISEARNING = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Is Hidden").Value)
                    If clsCommon.myCdbl(strCode) > 1 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Is Hidden must be 0 or 1.")
                    End If
                    obj.IsHiddenComponent = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Account Code").Value)
                    'If strCode.Length > 2 Or (String.IsNullOrEmpty(strCode)) Or clsCommon.myCdbl(strCode) = 0 Then
                    '    Throw New Exception("Sequence Number must be 1 to 99.")
                    'End If
                    obj.Account_Code = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Employer Gl Account").Value)
                    obj.GL_Employer_Account = strCode

                    Dim Pay_Head_Code As String = ""
                    strCode = clsCommon.myCstr(grow.Cells("Sequence").Value)
                    If strCode.Length > 2 Or (String.IsNullOrEmpty(strCode)) Or clsCommon.myCdbl(strCode) = 0 Then
                        Throw New Exception("Sequence Number must be 1 to 99.")
                    End If
                    Pay_Head_Code = clsPayHeadDefinitions.ValidatePayHeadSequence(obj.PAY_HEAD_CODE, strCode, IIf(obj.ISEARNING = True, 1, 0))
                    If clsCommon.myLen(Pay_Head_Code) > 0 Then
                        Throw New Exception("Invalid Sequence no for Pay Head " & obj.PAY_HEAD_CODE & ". Pay Head  " & Pay_Head_Code & " already contains sequence no " & strCode & "")
                    End If
                    obj.GROUP_SEQ = strCode
                    '=====shivani
                    strCode = clsCommon.myCstr(grow.Cells("Print Sequence").Value)
                    If strCode.Length > 2 Or (String.IsNullOrEmpty(strCode)) Or clsCommon.myCdbl(strCode) = 0 Then
                        Throw New Exception("Print Sequence Number must be 1 to 99.")
                    End If
                    Pay_Head_Code = clsPayHeadDefinitions.ValidatePayHeadPrintSequence(obj.PAY_HEAD_CODE, strCode, IIf(obj.ISEARNING = True, 1, 0))
                    If clsCommon.myLen(Pay_Head_Code) > 0 Then
                        Throw New Exception("Invalid Print Sequence no for Pay Head " & obj.PAY_HEAD_CODE & ". Pay Head  " & Pay_Head_Code & " already contains Print sequence no " & strCode & "")
                    End If
                    '==============
                    obj.PRINT_GROUP_SEQ = strCode
                    obj.SaveData(obj, clsPayHeadDefinitions.CheckNewEntry(obj.PAY_HEAD_CODE))
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = " select PAY_HEAD_CODE as Code, PAY_HEAD_NAME as Name, PRINT_NAME as 'Print Name' ,HEAD_TYPE as 'Pay Head Type', SUB_HEAD_TYPE as 'Sub Pay Head Type'," & _
              " PERIODICITY as 'Periodicity', CALC_BASIS as 'Calculation Basis', ROUND_OFF_TYPE as 'Round Off Type',cast(ISEARNING as integer) as [Is Earning],cast(IsHiddenComponent as Integer) as [Is Hidden], " & _
              " Account_Code as [Account Code],Group_Seq as [Sequence],PRINT_GROUP_SEQ as [Print Sequence],ARREAR_TYPE as [Arrear Type],GL_Employer_Account as [Employer Gl Account]  from TSPL_PAYHEAD_MASTER "
        transportSql.ExporttoExcel(str, "", "[Is Earning] desc,[Sequence]", Me)
    End Sub


    Public Shared Function GetcboHeadTypeDataTable() As DataTable
        Dim DT_Cbo As DataTable = New DataTable
        DT_Cbo.Columns.Add("Code", GetType(String))
        DT_Cbo.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Cbo.NewRow()
        DR("Name") = "Formula"
        DR("Code") = "F"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Attendance Based"
        DR("Code") = "ATTN"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Fixed"
        DR("Code") = "FIXED"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "User Define"
        DR("Code") = "UD"
        DT_Cbo.Rows.Add(DR)

        'DR = DT_Cbo.NewRow()
        'DR("Name") = "Arrear"
        'DR("Code") = "ARREAR"
        'DT_Cbo.Rows.Add(DR)

        DT_Cbo.AcceptChanges()
        Return DT_Cbo
    End Function

    Public Shared Function GetcboSubHeadTypeDataTable(ByVal IncludeCode As String) As DataTable
        Dim StrQry As String = " Select Description,Code from TSPL_FIXED_PARAMETER where Type = 'PayHeadSubHead' and Code not in (Select Distinct SUB_HEAD_TYPE from TSPL_PAYHEAD_MASTER where SUB_HEAD_TYPE not in ('OTHER','Arrear','" + IncludeCode + "')) "
        Dim DT_Cbo As DataTable = clsDBFuncationality.GetDataTable(StrQry)
        Return DT_Cbo
    End Function

    Public Shared Function GetcboCalculationBasisDataTable() As DataTable
        Dim DT_Cbo As DataTable = New DataTable
        DT_Cbo.Columns.Add("Code", GetType(String))
        DT_Cbo.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Cbo.NewRow()
        DR("Name") = "Pay Days"
        DR("Code") = "PAY_DAYS"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Present Days"
        DR("Code") = "PRESENT_DAYS"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Independent"
        DR("Code") = "INDEPENDENT"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Pay Days-Daily Basis"
        DR("Code") = "PAY_DAYS_DAILY_BASIS"
        DT_Cbo.Rows.Add(DR)

        DR = DT_Cbo.NewRow()
        DR("Name") = "Present Days-Daily Basis"
        DR("Code") = "PRESENT_DAYS_DAILY_BASIS"
        DT_Cbo.Rows.Add(DR)

        DT_Cbo.AcceptChanges()
        Return DT_Cbo
    End Function

    Private Sub CboPayHeadType_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboPayHeadType.Enter
        Dim Selected_val As String = String.Empty

        If CboPayHeadType.SelectedIndex > -1 Then
            Selected_val = CboPayHeadType.SelectedValue
        End If

        CboPayHeadType.DataSource = GetcboHeadTypeDataTable()
        CboPayHeadType.ValueMember = "Code"
        CboPayHeadType.DisplayMember = "Name"

        If Selected_val.Length > 0 Then
            CboPayHeadType.SelectedValue = Selected_val
        Else
            CboPayHeadType.SelectedIndex = -1
        End If

    End Sub

    Private Sub cboPayHeadCategory_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPayHeadCategory.Enter
        Dim Selected_val As String = String.Empty

        If cboPayHeadCategory.SelectedIndex > -1 Then
            Selected_val = cboPayHeadCategory.SelectedValue
        End If
        isInsideLoadData = True
        cboPayHeadCategory.DataSource = GetcboSubHeadTypeDataTable("")
        cboPayHeadCategory.ValueMember = "Code"
        cboPayHeadCategory.DisplayMember = "Description"
        isInsideLoadData = False
        If Selected_val.Length > 0 Then
            cboPayHeadCategory.SelectedValue = Selected_val
        Else
            cboPayHeadCategory.SelectedIndex = -1
        End If

    End Sub

    Private Sub cboCalcBasis_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCalcBasis.Enter
        Dim Selected_val As String = String.Empty

        If cboCalcBasis.SelectedIndex > -1 Then
            Selected_val = cboCalcBasis.SelectedValue
        End If

        cboCalcBasis.DataSource = GetcboCalculationBasisDataTable()
        cboCalcBasis.ValueMember = "Code"
        cboCalcBasis.DisplayMember = "Name"

        If Selected_val.Length > 0 Then
            cboCalcBasis.SelectedValue = Selected_val
        Else
            cboCalcBasis.SelectedIndex = -1
        End If

    End Sub

    Private Sub cboRoundOffType_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoundOffType.Enter
        Dim Selected_val As String = String.Empty

        If cboRoundOffType.SelectedIndex > -1 Then
            Selected_val = cboRoundOffType.SelectedValue
        End If

        cboRoundOffType.DataSource = clsFixedParameter.GetCboDataTable("CboRound", Nothing)
        cboRoundOffType.ValueMember = "Code"
        cboRoundOffType.DisplayMember = "DESCRIPTION"

        If Selected_val.Length > 0 Then
            cboRoundOffType.SelectedValue = Selected_val
        Else
            cboRoundOffType.SelectedIndex = -1
        End If

    End Sub

    Private Sub cboPeriodicity_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPeriodicity.Enter

        Dim Selected_val As String = String.Empty

        If cboPeriodicity.SelectedIndex > -1 Then
            Selected_val = cboPeriodicity.SelectedValue
        End If

        cboPeriodicity.DataSource = clsFixedParameter.GetCboDataTable("cboPeriodicity", Nothing)
        cboPeriodicity.ValueMember = "Code"
        cboPeriodicity.DisplayMember = "DESCRIPTION"

        If Selected_val.Length > 0 Then
            cboPeriodicity.SelectedValue = Selected_val
        Else
            cboPeriodicity.SelectedIndex = -1
        End If

    End Sub

    Private Sub fndGLAccount__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGLAccount._MYValidating
        OpenGLAccount(isButtonClicked)
    End Sub
    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String
            Dim whrcls As String
            Dim arr As New ArrayList()
            Dim isEarningCond As String = ""
            '===============commented because debit,credit cond is add at salary generation screen, done on 28/07/2015
            'If Me.chkIsEarning.Checked = True Then
            '    isEarningCond = "Account_Balance='Debit'"
            'Else
            '    isEarningCond = "Account_Balance='Credit'"
            'End If
            ''============================end here=====================================
            arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arr.Item(0)
            whrcls = arr.Item(1)

            If clsCommon.myLen(whrcls) > 0 AndAlso clsCommon.myLen(isEarningCond) > 0 Then

                whrcls += " AND " & isEarningCond
            ElseIf clsCommon.myLen(isEarningCond) > 0 Then
                whrcls = isEarningCond
            End If
            fndGLAccount.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("GLACJournalEntry", qry, "Account_Code", whrcls, clsCommon.myCstr(fndGLAccount.Value), "Account_Code", isButtonClick))
            txtGLAccountDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(fndGLAccount.Value) + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkIsEarning_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIsEarning.ToggleStateChanged
        Me.fndGLAccount.Value = Nothing
        Me.txtGLAccountDesc.Text = ""
    End Sub

    Private Sub txtSerialNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSerialNo.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub CboPayHeadType_SelectedValueChanged(sender As Object, e As EventArgs) Handles CboPayHeadType.SelectedValueChanged
        'If CboPayHeadType.Text = "Arrear" Then
        '    cboArrearType.Visible = True
        'Else
        If CboPayHeadType.Text = "Formula" Then
            cboArrearType.Visible = False
        ElseIf CboPayHeadType.Text = "Attendance Based" Then
            cboArrearType.Visible = False
        ElseIf CboPayHeadType.Text = "Fixed" Then
            cboArrearType.Visible = False
        ElseIf CboPayHeadType.Text = "User Define" Then
            cboArrearType.Visible = False
        End If

    End Sub
    Public Shared Function GetcboArrearTypeDataTable(ByVal IncludeCode As String) As DataTable
        Dim StrQry As String = " select PAY_HEAD_CODE as Code ,PAY_HEAD_NAME as Name from TSPL_PAYHEAD_MASTER where HEAD_TYPE not in ('ARREAR','" + IncludeCode + "')"
        Dim DT_Cbo As DataTable = clsDBFuncationality.GetDataTable(StrQry)
        Return DT_Cbo
    End Function

    Private Sub cboArrearType_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles cboArrearType.Enter
        Dim Selected_val As String = String.Empty

        If cboArrearType.SelectedIndex > -1 Then
            Selected_val = cboArrearType.SelectedValue
        End If

        cboArrearType.DataSource = GetcboArrearTypeDataTable("")
        cboArrearType.ValueMember = "Code"
        cboArrearType.DisplayMember = "Name"

        If Selected_val.Length > 0 Then
            cboArrearType.SelectedValue = Selected_val
        Else
            cboArrearType.SelectedIndex = -1
        End If
    End Sub
    Private Sub OpenGLAccount1(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String
            Dim whrcls As String
            Dim arr As New ArrayList()
            Dim isEarningCond As String = ""
            '===============commented because debit,credit cond is add at salary generation screen, done on 28/07/2015
            'If Me.chkIsEarning.Checked = True Then
            '    isEarningCond = "Account_Balance='Debit'"
            'Else
            '    isEarningCond = "Account_Balance='Credit'"
            'End If
            ''============================end here=====================================
            arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arr.Item(0)
            whrcls = arr.Item(1)

            If clsCommon.myLen(whrcls) > 0 AndAlso clsCommon.myLen(isEarningCond) > 0 Then

                whrcls += " AND " & isEarningCond
            ElseIf clsCommon.myLen(isEarningCond) > 0 Then
                whrcls = isEarningCond
            End If
            fndEmpGLAcc.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("GLACJournalEntry", qry, "Account_Code", whrcls, clsCommon.myCstr(fndGLAccount.Value), "Account_Code", isButtonClick))
            txtEmpGLAcc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(fndGLAccount.Value) + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub fndEmpGLAcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEmpGLAcc._MYValidating
        OpenGLAccount1(isButtonClicked)
    End Sub
    Sub SUBHEAD()
        If cboPayHeadCategory.SelectedValue = "Arrear" Then
            cboArrearType.Visible = True
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboPayHeadCategory.SelectedValue), "HRA") = CompairStringResult.Equal Then
            lblMaxHRA.Visible = True
            TxtMaxHRA.Visible = True
        Else
            lblMaxHRA.Visible = False
            TxtMaxHRA.Visible = False
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboPayHeadCategory.SelectedValue), "EPF") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(cboPayHeadCategory.SelectedValue), "EMPESI") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(cboPayHeadCategory.SelectedValue), "LWF") = CompairStringResult.Equal Then
            lblEmpGLACC.Visible = True
            fndEmpGLAcc.Visible = True
            txtEmpGLAcc.Visible = True
        Else
            lblEmpGLACC.Visible = False
            fndEmpGLAcc.Visible = False
            txtEmpGLAcc.Visible = False
        End If

    End Sub

    Private Sub cboPayHeadCategory_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboPayHeadCategory.SelectedValueChanged
        If isInsideLoadData = False Then
            SUBHEAD()
        Else
            Try
                If cboPayHeadCategory.SelectedValue = "Arrear" Then
                    cboArrearType.Visible = True
                End If

                If clsCommon.CompairString(clsCommon.myCstr(cboPayHeadCategory.SelectedValue), "HRA") = CompairStringResult.Equal Then
                    lblMaxHRA.Visible = True
                    TxtMaxHRA.Visible = True
                Else
                    lblMaxHRA.Visible = False
                    TxtMaxHRA.Visible = False
                End If

            Catch ex As Exception

            End Try

        End If
    End Sub
    ''richa agarwal26 Dec,2019
    Private Sub fndNatureOfDeduction__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndNatureOfDeduction._MYValidating
        Try
            Dim qry As String = "select TSPL_TDS_DEDUCTION_HEAD.Deduction_Code as [Code] ,TSPL_TDS_DEDUCTION_HEAD.Description as [Description] ,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as [TDS Section] ,TSPL_TDS_DEDUCTION_HEAD.Cumm_Cutoff as [Cumm Cutoff] ,TSPL_TDS_DEDUCTION_HEAD.Percent_Amount as [Percent Amount] ,TSPL_TDS_DEDUCTION_HEAD.Inactive as [Inactive] ,TSPL_TDS_DEDUCTION_HEAD.Comment as [Comment] ,TSPL_TDS_DEDUCTION_HEAD.Created_By as [Created By] ,TSPL_TDS_DEDUCTION_HEAD.Created_Date as [Created Date] ,TSPL_TDS_DEDUCTION_HEAD.Modify_By as [Modify By] ,TSPL_TDS_DEDUCTION_HEAD.Modify_Date as [Modify Date] ,TSPL_TDS_DEDUCTION_HEAD.Comp_Code as [Company Code] ,TSPL_TDS_DEDUCTION_HEAD.Gl_Account as [GL Account]  From TSPL_TDS_DEDUCTION_HEAD "

            fndNatureOfDeduction.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("NatureofDED", qry, "Code", "", clsCommon.myCstr(fndNatureOfDeduction.Value), "Code", isButtonClicked))
            lblNatureOfDeduction.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + clsCommon.myCstr(fndNatureOfDeduction.Value) + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
