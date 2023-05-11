'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmDeductionDetailsReport
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

    Private Sub frmDeductionDetailsReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDeductionDetailsReport)
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
    Sub LoadData()
        qry = ""
        qry = " SELECT DISTINCT(T1.PAY_HEAD_CODE),T3.PAY_HEAD_NAME FROM TSPL_GENERATE_SALARY_PAYHEADS T1 INNER JOIN TSPL_GENERATE_SALARY T2 "
        qry += " ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE "
        Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T3 ON  T1.PAY_HEAD_CODE=T3.PAY_HEAD_CODE "
        Qry += " Left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= T2.Location_Code"
        Qry += " WHERE T2.PAY_PERIOD_CODE='" + txtFromPP.Value + "' AND T3.ISEARNING=0 "
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code='" & fndLocationCode.Value & "' "
        End If
        qry += " ORDER BY T3.PAY_HEAD_NAME "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "PAY_HEAD_CODE"
        cbgLocation.DisplayMember = "PAY_HEAD_NAME"
    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)
        LoadData()
    
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Pay Period.")
                Return
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select AtLeast Single Employee Or Select All")
                Return
            End If

            Dim header1 As String = "Salary Certificate for the month of " & Me.txtFromPP.Value & ""
            Dim header2 As String = ""
            Dim strQry As String
            If clsCommon.myLen(fndLocationCode.Value) > 0 Then
                strQry = " Location_Desc "
            Else
                strQry = "'All'"
            End If
            Dim Qry As String = ""
            Qry = ""
            Qry += " SELECT '" & objCommonVar.CurrentCompanyName & "' as Company_Name,'" & objCommonVar.CurrLocationCode & "' as Company_Address,T1.PAY_PERIOD_CODE,T2.PAY_PERIOD_NAME,T_1.EMP_CODE,T3.Emp_Name,T_1.PAY_HEAD_CODE,T4.PAY_HEAD_NAME,T_1.ACTUAL_AMOUNT AS AMOUNT," & strQry & " as Location_Desc,Logo_Img"
            Qry += " FROM TSPL_GENERATE_SALARY T1 INNER JOIN TSPL_GENERATE_SALARY_PAYHEADS T_1 ON T1.SALARY_GENERATION_CODE=T_1.SALARY_GENERATION_CODE "
            Qry += " LEFT JOIN TSPL_PAYPERIOD_MASTER T2 ON T1.PAY_PERIOD_CODE=T2.PAY_PERIOD_CODE "
            Qry += " LEFT JOIN TSPL_PAYHEAD_MASTER T4 ON T_1.PAY_HEAD_CODE=T4.PAY_HEAD_CODE "
            Qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T3 ON T_1.EMP_CODE=T3.EMP_CODE "
            Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =T1.LOCATION_CODE "
            Qry += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =T3.Comp_Code "
            Qry += " WHERE t1.PAY_PERIOD_CODE='" & Me.txtFromPP.Value & "' AND T4.ISEARNING=0 AND T_1.ACTUAL_AMOUNT>0"
           
            If cbgLocation.CheckedValue.Count > 0 Then
                Qry += " AND T_1.PAY_HEAD_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            Dim dtFinal As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpDeductionDetails", "Deduction Details Report")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
        Loaddata()
    End Sub

End Class
