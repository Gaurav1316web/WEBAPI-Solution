'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmOT_Reports
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

    Private Sub frmOT_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmOT_Reports)
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

        qry = " "
        qry += " select distinct TSPL_OT_SHEET.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_OT_SHEET "
        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_OT_SHEET.EMP_CODE "
        qry += " where TSPL_OT_SHEET.PAY_PERIOD_CODE  ='" + txtFromPP.Value + "' "
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
            Qry += " select TSPL_OT_SHEET.EMP_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_OT_SHEET.OT_HOURS, TSPL_OT_SHEET.OT_RATE ,TSPL_OT_SHEET.OT_TOTAL_AMOUNT,TSPL_OT_SHEET.PAY_PERIOD_CODE, '" + objCommonVar.CurrentCompanyName + "' as 'Company_Name' , 1 as grp from TSPL_OT_SHEET"
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_OT_SHEET.EMP_CODE "
            Qry += " WHERE TSPL_OT_SHEET.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") AND TSPL_OT_SHEET.PAY_PERIOD_CODE= '" + txtFromPP.Value + "' "

            Dim Dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If Dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, Dt, "crptOTReport", "OT Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
