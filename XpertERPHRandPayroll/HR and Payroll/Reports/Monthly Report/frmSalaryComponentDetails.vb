'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmSalaryComponentDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PrintData()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmSalaryComponentDetailss_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryComponentDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '' Anubhooti 23-July-2014 (BM00000003141)
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)

    End Sub

    Sub PrintData()
        Try
            Dim ZeroCond As String = ""
            Dim Qry As String = ""

            Qry = ""
            Qry += " SELECT  T1.EMP_CODE,T5.Emp_Name,T1.PAY_HEAD_CODE,T4.PAY_HEAD_NAME,T2.PAY_PERIOD_CODE,T1.ACTUAL_AMOUNT AS AMOUNT  "
            Qry += " FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
            Qry += " INNER JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE "
            Qry += " INNER JOIN TSPL_PAYPERIOD_MASTER T3 ON T2.PAY_PERIOD_CODE=T3.PAY_PERIOD_CODE "
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T4 ON T1.PAY_HEAD_CODE=T4.PAY_HEAD_CODE "
            Qry += " INNER JOIN TSPL_EMPLOYEE_MASTER T5 ON T1.EMP_CODE=T5.EMP_CODE "
            Qry += " WHERE T3.DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "') "
            Qry += " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & txtToPP.Value & "') AND T1.PAY_HEAD_CODE='" & txtComponent.Value & "' "
            If chkShowZero.Checked = False Then
                ZeroCond = "AND T1.ACTUAL_AMOUNT>0"
            Else
                ZeroCond = ""
            End If
            Qry += ZeroCond
            Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


            If Hader_Info.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim dtFinal As DataTable = New DataTable
                
               
                Qry = ""
                Qry += " SELECT '" & objCommonVar.CurrentCompanyName & "' AS Company_Name,'" & objCommonVar.CurrLocationName & "' AS Company_Address,"
                Qry += " '" & txtFromPP.Value & "' AS FROM_PAY_PERIOD,'" & txtToPP.Value & "' AS TO_PAY_PERIOD,'" & IIf(Me.rdbEarning.IsChecked, "Earning", "Deduction") & "' AS Head_Type, "
                Qry += " T1.EMP_CODE,T5.Emp_Name,T1.PAY_HEAD_CODE,T4.PAY_HEAD_NAME,T2.PAY_PERIOD_CODE,T1.ACTUAL_AMOUNT AS AMOUNT  "
                Qry += " FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                Qry += " INNER JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE "
                Qry += " INNER JOIN TSPL_PAYPERIOD_MASTER T3 ON T2.PAY_PERIOD_CODE=T3.PAY_PERIOD_CODE "
                Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T4 ON T1.PAY_HEAD_CODE=T4.PAY_HEAD_CODE "
                Qry += " INNER JOIN TSPL_EMPLOYEE_MASTER T5 ON T1.EMP_CODE=T5.EMP_CODE "
                Qry += " WHERE T3.DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "') "
                Qry += " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & txtToPP.Value & "') AND T1.PAY_HEAD_CODE='" & txtComponent.Value & "' "
                If chkShowZero.Checked = False Then
                    ZeroCond = "AND T1.ACTUAL_AMOUNT>0"
                Else
                    ZeroCond = ""
                End If
                Qry += ZeroCond

                dtFinal = clsDBFuncationality.GetDataTable(Qry)
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpSalaryComponentDetails", "Salary Component Details")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSalStruct__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtToPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtToPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtToPP.Value, "", isButtonClicked)
        lblToPPName.Text = clsPayPeriodMaster.GetName(txtToPP.Value, Nothing)
    End Sub

    Private Sub txtComponent__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtComponent._MYValidating
        If clsCommon.myLen(txtFromPP.Value) = 0 Or clsCommon.myLen(Me.txtToPP.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Select From Pay Period and To Pay Period.", Me.Text)
            Exit Sub
        End If
        Dim qry As String = "SELECT DISTINCT T1.PAY_HEAD_CODE as Code,T4.PAY_HEAD_NAME FROM TSPL_GENERATE_SALARY_PAYHEADS T1 " _
                            & " INNER JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE " _
                            & " INNER JOIN TSPL_PAYPERIOD_MASTER T3 ON T2.PAY_PERIOD_CODE=T3.PAY_PERIOD_CODE " _
                            & " INNER JOIN TSPL_PAYHEAD_MASTER T4 ON T1.PAY_HEAD_CODE=T4.PAY_HEAD_CODE "

        Dim cond As String = "T3.DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "') " _
                            & " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & Me.txtToPP.Value & "') " _
                            & " AND T4.ISEARNING=" & IIf(Me.rdbEarning.IsChecked, 1, 0) & ""
        txtComponent.Value = clsCommon.ShowSelectForm("TSPL_PAYHEAD_MASTER", qry, "Code", cond, txtComponent.Value, "", isButtonClicked)
        Try
            lblComponentName.Text = clsPayHeadDefinitions.GetData(txtComponent.Value, NavigatorType.Current).PAY_HEAD_NAME
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rdbEarning_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbEarning.ToggleStateChanged
        Me.txtComponent.Value = Nothing
        lblComponentName.Text = ""
    End Sub

    Private Sub rdbDeduction_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbDeduction.ToggleStateChanged
        Me.txtComponent.Value = Nothing
        lblComponentName.Text = ""
    End Sub
End Class
