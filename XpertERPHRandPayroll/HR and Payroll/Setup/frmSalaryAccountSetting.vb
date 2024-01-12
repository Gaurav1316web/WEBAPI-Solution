Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine


''''''''''''''''''''''''''''''''''''''''''Ticket No:BM000000001250''''''''''''''''''''''''''''''''''''''''''''''''Created by Panch Raj on 25/11/13''''''
Public Class frmSalaryAccountSetting
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False


    Private Sub frmSalaryAccountSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        LoadData("", NavigatorType.Current)
    End Sub
#Region "Finders"


  
    Private Sub fndWIPCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSalaryPayableAccount._MYValidating
        OpenGLAccount(isButtonClicked)
    End Sub
    Private Sub fndaccountsetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_PAYROLL_ACCOUNTSETS where ACCOUNT_SET_CODE ='" + fndaccountsetcode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                fndaccountsetcode.MyReadOnly = False
            Else
                fndaccountsetcode.MyReadOnly = True
            End If
            If fndaccountsetcode.MyReadOnly OrElse isButtonClicked Then

                'Dim qry As String = " select ACCOUNT_SET_CODE as Code,  DESCRIPTION as 'Description' from TSPL_PAYROLL_ACCOUNTSETS "
                'fndaccountsetcode.Value = clsCommon.ShowSelectForm("TSPL_PAYROLL_ACCOUNTSETS", qry, "Code", "", fndaccountsetcode.Value, "", isButtonClicked)
                fndaccountsetcode.Value = clsSalaryAccountSetting.getFinder("", fndaccountsetcode.Value, isButtonClicked)
                If fndaccountsetcode.Value <> "" Then
                    LoadData(fndaccountsetcode.Value, NavigatorType.Current)
                Else
                    Reset()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message(), Me.Text)
        End Try
    End Sub
    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String
            Dim whrcls As String
            Dim arr As New ArrayList()
            ' Dim isEarningCond As String
            
            arr = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
            qry = arr.Item(0)
            whrcls = arr.Item(1)

            
            fndSalaryPayableAccount.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("GLACJournalEntry", qry, "Account_Code", whrcls, clsCommon.myCstr(fndSalaryPayableAccount.Value), "Account_Code", isButtonClick))
            txtSalaryPayableAccountDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(fndSalaryPayableAccount.Value) + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub fndaccountsetcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccountsetcode._MYNavigator
        Try
            LoadData(fndaccountsetcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region
#Region "Functions"
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsfrmSalaryAccountSetting.DeleteData(fndaccountsetcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryAccountSetting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsfrmSalaryAccountSetting = clsfrmSalaryAccountSetting.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndaccountsetcode.Value = obj.ACCOUNT_SET_CODE
            txtAccdescription.Text = obj.DESCRIPTION
            fndSalaryPayableAccount.Value = obj.GL_SALARY_PAYABLE
            fndBankAccount.Value = obj.BANK_CODE
            txtBankAccountName.Text = obj.BANK_CODE
            fndSourceCode.Value = obj.SourceCode
            txtSourceCodeName.Text = obj.SourceCodeDesc
            txtSalaryPayableAccountDesc.Text = obj.GL_SALARY_PAYABLE_Desc
            txtSourceCodeName.Text = obj.SourceCodeDesc
            txtBankGLAccount.Text = clsDBFuncationality.getSingleValue("select BANKACC from TSPL_BANK_MASTER where bank_code='" + clsCommon.myCstr(fndBankAccount.Value) + "' ")

            '' new accounts by panch raj
            fndPFPayableAcc.Value = obj.GL_Employer_PF_PAYABLE
            fndESIPayableAcc.Value = obj.GL_Employer_ESI_PAYABLE
            fndOthrPayable.Value = obj.GL_EMPLOYER_OTHERS_PAYABLE

            lblPFPayableAccDesc.Text = obj.GL_Employer_PF_PAYABLE_Desc
            lblESIPayableAccDesc.Text = obj.GL_Employer_ESI_PAYABLE_Desc
            lblOthrPayableDesc.Text = obj.GL_EMPLOYER_OTHERS_PAYABLE_Desc

            fndaccountsetcode.MyReadOnly = True
        End If
    End Sub
    Sub Reset()
        fndaccountsetcode.Value = ""
        txtAccdescription.Text = ""
        fndaccountsetcode.MyReadOnly = False
        
        fndSalaryPayableAccount.Value = Nothing
        txtSalaryPayableAccountDesc.Text = ""
        fndBankAccount.Value = Nothing
        txtAccdescription.Text = ""
        txtBankGLAccount.Text = ""
        txtBankAccountName.Text = ""

        '' new accounts by panch raj
        fndPFPayableAcc.Value = Nothing
        fndESIPayableAcc.Value = Nothing
        fndOthrPayable.Value = Nothing

        lblPFPayableAccDesc.Text = ""
        lblESIPayableAccDesc.Text = ""
        lblOthrPayableDesc.Text = ""

    End Sub
    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmSalaryAccountSetting, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        If AllowToSave() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim obj As New clsfrmSalaryAccountSetting()
                obj.ACCOUNT_SET_CODE = fndaccountsetcode.Value
                obj.DESCRIPTION = txtAccdescription.Text
                obj.GL_SALARY_PAYABLE = clsCommon.myCstr(fndSalaryPayableAccount.Value)
                obj.BANK_CODE = clsCommon.myCstr(fndBankAccount.Value)
                obj.SourceCode = clsCommon.myCstr(fndSourceCode.Value)

                '' new accounts by panch raj
                obj.GL_Employer_PF_PAYABLE = clsCommon.myCstr(fndPFPayableAcc.Value)
                obj.GL_Employer_ESI_PAYABLE = clsCommon.myCstr(fndESIPayableAcc.Value)
                obj.GL_EMPLOYER_OTHERS_PAYABLE = clsCommon.myCstr(fndOthrPayable.Value)

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(ACCOUNT_SET_CODE) from TSPL_PAYROLL_ACCOUNTSETS where ACCOUNT_SET_CODE='" + obj.ACCOUNT_SET_CODE + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsfrmSalaryAccountSetting.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.ACCOUNT_SET_CODE, NavigatorType.Current)

                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(clsCommon.myCstr(fndaccountsetcode.Value)) <= 0 Then
                fndaccountsetcode.Focus()
                Throw New Exception("Please Fill AccountSet Code")
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtAccdescription.Text)) <= 0 Then
            txtAccdescription.Focus()
            Throw New Exception("Please Fill Description")
        End If
        If clsCommon.myLen(clsCommon.myCstr(fndSalaryPayableAccount.Value)) <= 0 Then
            fndSalaryPayableAccount.Focus()
            Throw New Exception("Please Fill Salary Payable Account")
        End If
        If clsCommon.myLen(clsCommon.myCstr(fndBankAccount.Value)) <= 0 Then
            fndBankAccount.Focus()
            Throw New Exception("Please Fill Bank Account")
        End If
        If clsCommon.myLen(clsCommon.myCstr(fndSourceCode.Value)) <= 0 Then
            fndSourceCode.Focus()
            Throw New Exception("Please Fill Source Code")
        End If

        Return True
    End Function
#End Region

#Region " Events"


    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        funDelete()
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        Reset()
    End Sub

    Private Sub FrmAccountSetting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnnew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            funDelete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region


    Private Sub fndBankAccount__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBankAccount._MYValidating
        Dim qry As String = "select BANK_CODE AS Code,DESCRIPTION as Name,BANKACC from TSPL_BANK_MASTER "
        fndBankAccount.Value = clsCommon.ShowSelectForm("Bank", qry, "Code", "", fndBankAccount.Value, "", isButtonClicked)
        Dim dt As DataTable
        qry = qry & " where BANK_CODE='" & clsCommon.myCstr(fndBankAccount.Value) & "'"

        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            txtBankAccountName.Text = clsCommon.myCstr(dt.Rows(0).Item("Name"))
            txtBankGLAccount.Text = clsCommon.myCstr(dt.Rows(0).Item("BANKACC"))
        Else
            txtSalaryPayableAccountDesc.Text = ""
            txtBankGLAccount.Text = ""
        End If

    End Sub

    Private Sub fndSourceCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSourceCode._MYValidating
        Dim qry As String = "select SourceCode as [Code],SourceDescription as [Description]  from TSPL_GL_SOURCECODE   "
        fndSourceCode.Value = clsCommon.ShowSelectForm("SourceCode Selector", qry, "Code", "SourceLedger  ='GL'", fndSourceCode.Value, "Code", isButtonClicked)
        Dim strq As String
        strq = "select SourceDescription as [Description]  from TSPL_GL_SOURCECODE  where SourceCode='" & clsCommon.myCstr(fndSourceCode.Value) & "' "
        Me.txtSourceCodeName.Text = clsDBFuncationality.getSingleValue(strq)

    End Sub

    Private Sub fndPFPayableAcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPFPayableAcc._MYValidating
        fndPFPayableAcc.Value = clsGLAccount.getFinder("", fndPFPayableAcc.Value, isButtonClicked)
        lblPFPayableAccDesc.Text = clsGLAccount.GetName(fndPFPayableAcc.Value)
    End Sub

    Private Sub fndESIPayableAcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndESIPayableAcc._MYValidating
        fndESIPayableAcc.Value = clsGLAccount.getFinder("", fndESIPayableAcc.Value, isButtonClicked)
        lblESIPayableAccDesc.Text = clsGLAccount.GetName(fndESIPayableAcc.Value)
    End Sub

    Private Sub fndOthrPayable__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndOthrPayable._MYValidating
        fndOthrPayable.Value = clsGLAccount.getFinder("", fndOthrPayable.Value, isButtonClicked)
        lblOthrPayableDesc.Text = clsGLAccount.GetName(fndOthrPayable.Value)
    End Sub
    
End Class
