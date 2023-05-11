'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmAditionalEarning_DeductionReport
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

    Private Sub frmAditionalEarning_DeductionReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAditionalEarning_DeductionReport)
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

        qry = ""
        qry += " SELECT DISTINCT T1.EMP_CODE, TSPL_EMPLOYEE_MASTER.Emp_Name  FROM ( "
        qry += " select  distinct EMP_CODE from TSPL_ALLOWANCE WHERE PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
        qry += " union "
        qry += " select  distinct EMP_CODE  from TSPL_DEDUCTION  WHERE PAY_PERIOD_CODE ='" + txtFromPP.Value + "' ) AS T1"
        qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER  ON T1.EMP_CODE = TSPL_EMPLOYEE_MASTER .EMP_CODE "
        qry += " ORDER BY T1.EMP_CODE "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"
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
            Dim Qry As String = ""
            Qry = ""

            Qry += " SELECT FINAL .*, TSPL_EMPLOYEE_MASTER.Emp_Name , '" + objCommonVar.CurrentCompanyName + "' AS 'Company_Name', 1 as 'grp' FROM ( "
            Qry += " select  TSPL_ALLOWANCE_DETAIL.EMP_CODE,TSPL_ALLOWANCE_DETAIL.ALLOWANCE_CODE AS CODE,  TSPL_PAYHEAD_MASTER.PAY_HEAD_NAME AS ALL_PAY_HEAD  ,TSPL_ALLOWANCE_DETAIL.ALLOWANCE_AMOUNT,'' AS 'Dedu_Pay_head',  0 AS 'DEDUCTION_AMOUNT',  TSPL_ALLOWANCE.PAY_PERIOD_CODE from TSPL_ALLOWANCE_DETAIL"
            Qry += " left outer join TSPL_ALLOWANCE  on TSPL_ALLOWANCE_DETAIL.ALLOWANCE_CODE =TSPL_ALLOWANCE .ALLOWANCE_CODE  "
            Qry += " left outer join TSPL_PAYHEAD_MASTER  on TSPL_PAYHEAD_MASTER .PAY_HEAD_CODE= TSPL_ALLOWANCE_DETAIL.PAY_HEAD_CODE"
            Qry += " WHERE TSPL_ALLOWANCE.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' "
            Qry += " UNION "
            Qry += " select TSPL_DEDUCTION_DETAIL.EMP_CODE,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE AS CODE, '' AS 'ALL_PAY_HEAD' ,0 AS 'ALLOWANCE_AMOUNT', TSPL_PAYHEAD_MASTER .PAY_HEAD_NAME AS Dedu_Pay_head,TSPL_DEDUCTION_DETAIL.DEDUCTION_AMOUNT,  TSPL_DEDUCTION.PAY_PERIOD_CODE from TSPL_DEDUCTION_DETAIL"
            Qry += " left outer join TSPL_DEDUCTION on TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE   =TSPL_DEDUCTION.DEDUCTION_CODE"
            Qry += " left outer join TSPL_PAYHEAD_MASTER  on TSPL_PAYHEAD_MASTER .PAY_HEAD_CODE= TSPL_DEDUCTION_DETAIL.PAY_HEAD_CODE"
            Qry += " WHERE TSPL_DEDUCTION.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' "
            Qry += " ) AS FINAL"
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on FINAL.EMP_CODE = TSPL_EMPLOYEE_MASTER.EMP_CODE "
            Qry += " where FINAL.EMP_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Qry += " order by FINAL.EMP_CODE, FINAL.CODE "

            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, DT, "crptAdditionalEarningDeduction", "Employee Pay Slip Report")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
