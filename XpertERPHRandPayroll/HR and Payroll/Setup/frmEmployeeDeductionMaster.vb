Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmEmployeeDeductionMaster
#Region "Variables"
    Dim isNewEntry As Boolean = False
#End Region

    Private Sub frmEmployeeDeductionMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtEmpCode._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "' and Emp_Status<>'Inactive'"
                End If
            Else
                whrcls += " Emp_Status<>'Inactive'"
            End If

            Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name FROM TSPL_EMPLOYEE_MASTER "
            txtEmpCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER1", qry, "Code", whrcls, txtEmpCode.Value, "", isButtonClicked)
            Dim clsemp As clsEmployeeMaster
            clsemp = clsEmployeeMaster.FinderForEmployee(txtEmpCode.Value, Nothing)
            If Not clsemp Is Nothing Then
                lblEmpName.Text = clsemp.Emp_Name
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                Dim obj As New clsEmployeeDeductionMaster()
                obj.CODE = txtCode.Value
                obj.EMP_CODE = txtEmpCode.Value
                obj.LIC_POLICY_NO = txtLICPolicyNo.Text
                obj.LIC_PREMIUM_AMT = txtLICPremiumAmt.Text
                obj.BANK_NAME = txtBankName.Text
                obj.BANK_ACCOUNT_NO = txtBankAccountNo.Text
                obj.BANK_INSTALMENT = txtBankInstalment.Text
                obj.QUARTER_TYPE = txtQuarterType.Text
                obj.QUARTER_ALLOTED_DATE = txtQuarterAllotedDate.Value
                obj.QUARTER_LEFT_DATE = txtQuarterLeftDate.Value
                obj.KKK_INSTALMENT = txtKKKInstalment.Text
                obj.KKK_LOAN_TOTAL = txtKKKLoanTotal.Text
                If clsCommon.myLen(obj.CODE) <= 0 Then
                    isNewEntry = True
                End If
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Save Successfully.", Me.Text)
                    LoadData(obj.CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtEmpCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Employee Code can't be blank !", Me.Text)
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function


    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsEmployeeDeductionMaster.DeleteData(txtCode.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Data Delete Successfully.", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        txtCode.Value = ""
        txtEmpCode.Value = ""
        lblEmpName.Text = ""
        txtLICPolicyNo.Text = ""
        txtLICPremiumAmt.Text = "0"
        txtBankName.Text = ""
        txtBankAccountNo.Text = ""
        txtBankInstalment.Text = "0"
        txtQuarterType.Text = ""
        txtQuarterAllotedDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtQuarterLeftDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtKKKInstalment.Text = "0"
        txtKKKLoanTotal.Text = "0"
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "CODE", "EMP CODE", "LIC POLICY NO", "LIC PREMIUM AMT", "BANK NAME", "BANK ACCOUNT NO", "BANK INSTALMENT", "QUARTER TYPE", "QUARTER ALLOTED DATE", "QUARTER LEFT DATE", "KKK INSTALMENT", "KKK LOAN TOTAL") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsEmployeeDeductionMaster()
                    obj.CODE = clsCommon.myCstr(grow.Cells(0).Value)
                    obj.EMP_CODE = clsCommon.myCstr(grow.Cells(1).Value)
                    obj.LIC_POLICY_NO = clsCommon.myCstr(grow.Cells(2).Value)
                    obj.LIC_PREMIUM_AMT = clsCommon.myCstr(grow.Cells(3).Value)
                    obj.BANK_NAME = clsCommon.myCstr(grow.Cells(4).Value)
                    obj.BANK_ACCOUNT_NO = clsCommon.myCstr(grow.Cells(5).Value)
                    obj.BANK_INSTALMENT = clsCommon.myCstr(grow.Cells(6).Value)
                    obj.QUARTER_TYPE = clsCommon.myCstr(grow.Cells(7).Value)
                    obj.QUARTER_ALLOTED_DATE = clsCommon.myCstr(grow.Cells(8).Value)
                    obj.QUARTER_LEFT_DATE = clsCommon.myCstr(grow.Cells(9).Value)
                    obj.KKK_INSTALMENT = clsCommon.myCstr(grow.Cells(10).Value)
                    obj.KKK_LOAN_TOTAL = clsCommon.myCstr(grow.Cells(11).Value)
                    obj.SaveData(obj, clsEmployeeDeductionMaster.CheckNewEntry(obj.EMP_CODE))
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        clsCommon.ProgressBarHide()
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim str As String
            str = " select CODE,EMP_CODE as [EMP CODE], LIC_POLICY_NO as [LIC POLICY NO]  ,LIC_PREMIUM_AMT as [LIC PREMIUM AMT], BANK_NAME as [BANK NAME],
                    BANK_ACCOUNT_NO As [BANK ACCOUNT NO],BANK_INSTALMENT As [BANK INSTALMENT],QUARTER_TYPE As [QUARTER TYPE],QUARTER_ALLOTED_DATE As [QUARTER ALLOTED DATE],
                    QUARTER_LEFT_DATE As [QUARTER LEFT DATE],KKK_INSTALMENT As [KKK INSTALMENT],KKK_LOAN_TOTAL As [KKK LOAN TOTAL]
                    from TSPL_EMPLOYEE_DEDUCTION_MASTER "
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "select CODE,EMP_CODE from TSPL_EMPLOYEE_DEDUCTION_MASTER"
            txtCode.Value = clsCommon.ShowSelectForm("EMPDEDMST", qry, "CODE", "", txtCode.Value, "CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsEmployeeDeductionMaster()
        obj = clsEmployeeDeductionMaster.GetData(strCode, NavType)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CODE) > 0) Then
            Reset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.CODE
            txtEmpCode.Value = obj.EMP_CODE
            lblEmpName.Text = clsDBFuncationality.getSingleValue("Select Emp_Name From TSPL_EMPLOYEE_MASTER Where EMP_CODE='" + txtEmpCode.Value + "'")
            txtLICPolicyNo.Text = obj.LIC_POLICY_NO
            txtLICPremiumAmt.Text = obj.LIC_PREMIUM_AMT
            txtBankName.Text = obj.BANK_NAME
            txtBankAccountNo.Text = obj.BANK_ACCOUNT_NO
            txtBankInstalment.Text = obj.BANK_INSTALMENT
            txtQuarterType.Text = obj.QUARTER_TYPE
            txtQuarterAllotedDate.Value = obj.QUARTER_ALLOTED_DATE
            txtQuarterLeftDate.Value = obj.QUARTER_LEFT_DATE
            txtKKKInstalment.Text = obj.KKK_INSTALMENT
            txtKKKLoanTotal.Text = obj.KKK_LOAN_TOTAL
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class