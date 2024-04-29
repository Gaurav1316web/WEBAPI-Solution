Imports common
Imports System.Data.SqlClient

'''' <summary>
'''' ''''
'''' </summary>
'''' <remarks></remarks>

''''''''''''''''''''''''''''''''''''''''''Ticket No:BM00000001529''''''''''''''''''''''''''''''''''''''''''''''''Created by PanchRaj on 06/01/2014''''''
Public Class frmPJCAccountSetting
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False


    Private Sub frmPJCAccountSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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


    Private Sub fndWIP__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWIP._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndWIP.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", "", fndWIP.Value, "", isButtonClicked)
        txtWIP.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndWIP.Value + "' ")
    End Sub

    Private Sub fndSetupLabor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCostofSales._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndCostofSales.Value = clsCommon.ShowSelectForm("REC_CONfnd1", qry, "AccountCode", "", fndCostofSales.Value, "", isButtonClicked)
        txtCostofSales.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndCostofSales.Value + "' ")
    End Sub


    Private Sub fndRunLabor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBillings._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndBillings.Value = clsCommon.ShowSelectForm("REC_CONfnd2", qry, "AccountCode", "", fndBillings.Value, "", isButtonClicked)
        txtBillings.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndBillings.Value + "' ")

    End Sub

    Private Sub fndSubContract__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRevenue._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndRevenue.Value = clsCommon.ShowSelectForm("REC_CONfnd3", qry, "AccountCode", "", fndRevenue.Value, "", isButtonClicked)
        txtRevenue.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndRevenue.Value + "' ")
    End Sub

    Private Sub fndOverhead__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPayrollExpense._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndPayrollExpense.Value = clsCommon.ShowSelectForm("REC_CONfnd4", qry, "AccountCode", "", fndPayrollExpense.Value, "", isButtonClicked)
        txtPayrollExpense.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndPayrollExpense.Value + "' ")
    End Sub

    Private Sub fndMaterialVariance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndEmployeeExpense._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndEmployeeExpense.Value = clsCommon.ShowSelectForm("REC_CONfnd5", qry, "AccountCode", "", fndEmployeeExpense.Value, "", isButtonClicked)
        txtEmployeeExpense.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndEmployeeExpense.Value + "' ")
    End Sub

    Private Sub fndProductionVariance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLabor._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndLabor.Value = clsCommon.ShowSelectForm("REC_CONfnd6", qry, "AccountCode", "", fndLabor.Value, "", isButtonClicked)
        txtLabor.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndLabor.Value + "' ")
    End Sub
    
    Private Sub fndaccountsetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_PJC_ACCOUNTSETS where ACCOUNT_SET_CODE ='" + fndaccountsetcode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                fndaccountsetcode.MyReadOnly = False
            Else
                fndaccountsetcode.MyReadOnly = True
            End If
            If fndaccountsetcode.MyReadOnly OrElse isButtonClicked Then

                'Dim qry As String = " select ACCOUNT_SET_CODE as Code,  DESCRIPTION as 'Description' from TSPL_PJC_ACCOUNTSETS "
                'fndaccountsetcode.Value = clsCommon.ShowSelectForm("TSPL_PJC_ACCOUNTSETS", qry, "Code", "", fndaccountsetcode.Value, "", isButtonClicked)
                fndaccountsetcode.Value = clsPJCAccountSet.getFinder("", fndaccountsetcode.Value, isButtonClicked)
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
                If (clsPJCAccountSet.DeleteData(fndaccountsetcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPJCAccountSets)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsPJCAccountSet = clsPJCAccountSet.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndaccountsetcode.Value = obj.ACCOUNT_SET_CODE
            txtAccdescription.Text = obj.DESCRIPTION
            fndEmployeeExpense.Value = obj.GL_EMPLOYEE_EXPENSE
            fndPayrollExpense.Value = obj.GL_PAYROLL_EXPENSE
            fndLabor.Value = obj.GL_LABOR
            fndBillings.Value = obj.GL_BILLINGS
            fndCostofSales.Value = obj.GL_SALES_COST
            fndRevenue.Value = obj.GL_REVENUE
            fndWIP.Value = obj.GL_WIP
            fndOverhead.Value = obj.GL_OVERHEAD
            fndEquipment.Value = obj.GL_EQUIPMENT
            fndCost.Value = obj.GL_COST

            txtWIP.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndWIP.Value + "' ")
            txtCostofSales.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndCostofSales.Value + "' ")
            txtBillings.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndBillings.Value + "' ")
            txtRevenue.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndRevenue.Value + "' ")
            txtPayrollExpense.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndPayrollExpense.Value + "' ")
            txtEmployeeExpense.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndEmployeeExpense.Value + "' ")
            txtLabor.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndLabor.Value + "' ")

            txtOverhead.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndOverhead.Value + "' ")
            txtEquipment.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndEquipment.Value + "' ")
            txtCost.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndCost.Value + "' ")



            fndaccountsetcode.MyReadOnly = True
        End If
    End Sub
    Sub Reset()
        fndaccountsetcode.Value = ""
        txtAccdescription.Text = ""
        fndaccountsetcode.MyReadOnly = False
        fndEmployeeExpense.Value = ""
        txtEmployeeExpense.Text = ""
        fndEmployeeExpense.MyReadOnly = False
        fndPayrollExpense.Value = ""
        txtPayrollExpense.Text = ""
        fndPayrollExpense.MyReadOnly = False
        fndLabor.Value = ""
        txtLabor.Text = ""
        fndLabor.MyReadOnly = False
        fndBillings.Value = ""
        txtBillings.Text = ""
        fndBillings.MyReadOnly = False
        fndCostofSales.Value = ""
        txtCostofSales.Text = ""
        fndCostofSales.MyReadOnly = False
        fndRevenue.Value = ""
        txtRevenue.Text = ""
        fndRevenue.MyReadOnly = False
        fndWIP.Value = ""
        txtWIP.Text = ""
        fndWIP.MyReadOnly = False

        fndOverhead.Value = ""
        txtOverhead.Text = ""
        fndOverhead.MyReadOnly = False

        fndEquipment.Value = ""
        txtEquipment.Text = ""
        fndEquipment.MyReadOnly = False

        fndCost.Value = ""
        txtCost.Text = ""
        fndEquipment.MyReadOnly = False

    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPJCAccountSets, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                trans = clsDBFuncationality.GetTransactin()
                Dim obj As New clsPJCAccountSet()
                obj.ACCOUNT_SET_CODE = fndaccountsetcode.Value
                obj.DESCRIPTION = txtAccdescription.Text
                obj.GL_EMPLOYEE_EXPENSE = fndEmployeeExpense.Value
                obj.GL_PAYROLL_EXPENSE = fndPayrollExpense.Value
                obj.GL_LABOR = fndLabor.Value
                obj.GL_BILLINGS = fndBillings.Value
                obj.GL_SALES_COST = fndCostofSales.Value
                obj.GL_REVENUE = fndRevenue.Value
                obj.GL_WIP = fndWIP.Value
                obj.GL_OVERHEAD = fndOverhead.Value
                obj.GL_EQUIPMENT = fndEquipment.Value
                obj.GL_COST = fndCost.Value

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(ACCOUNT_SET_CODE) from TSPL_PJC_ACCOUNTSETS where ACCOUNT_SET_CODE='" + obj.ACCOUNT_SET_CODE + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsPJCAccountSet.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.ACCOUNT_SET_CODE, NavigatorType.Current)

                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(clsCommon.myCstr(fndaccountsetcode.Value)) <= 0 Then
                fndaccountsetcode.Focus()
                clsCommon.MyMessageBoxShow("Please Fill AccountSet Code")
                Return False
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtAccdescription.Text)) <= 0 Then
            txtAccdescription.Focus()
            clsCommon.MyMessageBoxShow("Please Fill Description")
            Return False
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

    Private Sub frmPJCAccountSetting_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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


    Private Sub fndOverhead__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndOverhead._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndOverhead.Value = clsCommon.ShowSelectForm("REC_CONfnd6", qry, "AccountCode", "", fndOverhead.Value, "", isButtonClicked)
        txtOverhead.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndOverhead.Value + "' ")
    End Sub

    Private Sub fndEquipment__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndEquipment._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndEquipment.Value = clsCommon.ShowSelectForm("REC_CONfnd6", qry, "AccountCode", "", fndEquipment.Value, "", isButtonClicked)
        txtEquipment.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndEquipment.Value + "' ")
    End Sub

    Private Sub fndCost__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCost._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndCost.Value = clsCommon.ShowSelectForm("REC_CONfnd6", qry, "AccountCode", "", fndCost.Value, "", isButtonClicked)
        txtCost.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndCost.Value + "' ")
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim str As String
        str = " SELECT ACCOUNT_SET_CODE AS Code, DESCRIPTION AS Description, GL_WIP AS [Work In Progress Account], GL_SALES_COST as [Sales Cost Account], " & _
              " GL_BILLINGS AS [Billings Account] ,GL_REVENUE AS [Revenue Account],GL_PAYROLL_EXPENSE AS [Payroll Expense Account], " & _
              " GL_EMPLOYEE_EXPENSE AS [Employee Expense Account],GL_LABOR AS [Labor Account],GL_OVERHEAD AS [Overhead Account],GL_EQUIPMENT AS [Equipment Account], " & _
              " GL_COST AS [Cost Account] FROM TSPL_PJC_ACCOUNTSETS "

        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Work In Progress Account", "Sales Cost Account", "Billings Account", "Revenue Account", "Payroll Expense Account", "Employee Expense Account", "Labor Account", "Overhead Account", "Equipment Account", "Cost Account") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsPJCAccountSet()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.ACCOUNT_SET_CODE = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strName.Length > 200 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.DESCRIPTION = strName

                    strCode = clsCommon.myCstr(grow.Cells("Work In Progress Account").Value)
                    obj.GL_WIP = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    strCode = clsCommon.myCstr(grow.Cells("Sales Cost Account").Value)
                    obj.GL_SALES_COST = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    ''"Work In Progress", "Sales Cost Account", "Billings Account", "Revenue Account", "Payroll Expense Account", "Employee Expense Account", "Labor Account", "Overhead Account", "Equipment Account", "Cost Account"
                    strCode = clsCommon.myCstr(grow.Cells("Billings Account").Value)
                    obj.GL_BILLINGS = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    strCode = clsCommon.myCstr(grow.Cells("Revenue Account").Value)
                    obj.GL_REVENUE = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    strCode = clsCommon.myCstr(grow.Cells("Payroll Expense Account").Value)
                    obj.GL_PAYROLL_EXPENSE = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    strCode = clsCommon.myCstr(grow.Cells("Employee Expense Account").Value)
                    obj.GL_EMPLOYEE_EXPENSE = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    strCode = clsCommon.myCstr(grow.Cells("Labor Account").Value)
                    obj.GL_LABOR = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    

                    strCode = clsCommon.myCstr(grow.Cells("Overhead Account").Value)
                    obj.GL_OVERHEAD = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    strCode = clsCommon.myCstr(grow.Cells("Equipment Account").Value)
                    obj.GL_EQUIPMENT = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    
                    strCode = clsCommon.myCstr(grow.Cells("Cost Account").Value)
                    obj.GL_COST = strCode
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsPJCAccountSet.checkGLAccount(strCode, trans) = False Then
                            Throw New Exception("GL Account Code " & strCode & " does not exist !")
                        End If
                    End If
                    

                    '' check for new entry
                    Dim isNewEntry As Boolean = False
                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(ACCOUNT_SET_CODE) from TSPL_PJC_ACCOUNTSETS where ACCOUNT_SET_CODE='" + obj.ACCOUNT_SET_CODE + "'", trans)
                    If (qry = 0) Then
                        isNewEntry = True
                    Else
                        isNewEntry = False
                    End If
                    clsPJCAccountSet.SaveData(obj, isNewEntry, trans)
                Next
                trans.Commit()

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub
End Class
