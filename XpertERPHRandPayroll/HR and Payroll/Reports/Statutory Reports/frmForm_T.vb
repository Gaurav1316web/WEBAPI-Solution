'--29/07/2013--form Add By- Pradeep Sharma ---------
'' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmForm_T
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "frmForm_T"

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
    Dim DT_Details As DataTable

    Const colLineNo As String = "LineNo"
    Const colEmpCode As String = "EmpCode"
    Const colESICode As String = "ESICode"
    Const colEmpName As String = "EmpName"
    Const colDays As String = "Days"
    Const colMonthWages As String = "MonthWages"
    Const colResionCode As String = "ResionCode"
    Const colLastWorkDate As String = "LastWorkDate"

#End Region
    Sub LoadData()
        Try
            If clsCommon.myLen(txtFromPP.Value) < 1 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period First.", Me.Text)
            End If
            Dim Qry As String = ""
            Dim count As Int16 = clsPayPeriodMaster.GetLastDay(txtFromPP.Value, Nothing)
            Qry += ""
            Qry += " SELECT ROW_NUMBER() over (ORDER BY T2.Emp_CODE ) AS 'SL No',  T2.Emp_Name as 'Employee Name', T2.FATHERS_NAME as 'Father Name', T2.SEX, T3.Designation_Desc as 'Designation', T4.DEPARTMENT_NAME as 'Department', T2.Joining_date as 'Joining Date', T2.ESI_NO AS 'ESI NO', T2.PF_NO AS 'PF NO', T1.NET_SALARY AS 'Net Salary', FINAL.*   FROM ("
            Qry += " select * from ("
            Qry += " select EMP_CODE, DAY(ATTENDANCE_DATE ) AS [DAY], "
            Qry += " (CASE WHEN FIRST_HALF = 'A'  AND SECOND_HALF = 'A' THEN 'A' ELSE (CASE WHEN (FIRST_HALF = 'A'  AND SECOND_HALF <> 'A') OR (FIRST_HALF <> 'A'  AND SECOND_HALF = 'A') THEN 'P/2' ELSE 'P'  END ) END ) AS FIRST_HALF from TSPL_DAILY_ATTENDANCE_DETAIL "
            Qry += " LEFT OUTER JOIN TSPL_DAILY_ATTENDANCE ON TSPL_DAILY_ATTENDANCE.DLA_CODE = TSPL_DAILY_ATTENDANCE_DETAIL .DLA_CODE "
            Qry += " WHERE TSPL_DAILY_ATTENDANCE.PAY_PERIOD_CODE ='" + txtFromPP.Value + "'"
            Qry += "    UNION ALL   "
            Qry += " select EMP_CODE, DAY(ATTENDANCE_DATE ) AS [DAY], (CASE WHEN FIRST_HALF = 'A'  AND SECOND_HALF = 'A' THEN 'A' ELSE (CASE WHEN (FIRST_HALF = 'A'  AND SECOND_HALF <> 'A') OR (FIRST_HALF <> 'A'  AND SECOND_HALF = 'A') THEN 'P/2' ELSE 'P'  END ) END ) AS FIRST_HALF from TSPL_HOURLY_ATTENDANCE_DETAIL  "
            Qry += " LEFT OUTER JOIN TSPL_HOURLY_ATTENDANCE ON TSPL_HOURLY_ATTENDANCE.DLA_CODE = TSPL_HOURLY_ATTENDANCE_DETAIL.DLA_CODE "
            Qry += " WHERE TSPL_HOURLY_ATTENDANCE.PAY_PERIOD_CODE ='" + txtFromPP.Value + "'"
            Qry += " ) AS S"
            Qry += "    PIVOT "
            Qry += " ("
            Qry += "    MAX(FIRST_HALF)"
            Qry += " FOR [DAY] IN ([1]"
            For ii As Int16 = 2 To count
                Qry += ",[" + ii.ToString() + "]"
            Next
            ',[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31]
            Qry += " ) ) AS pvt) AS FINAL"
            Qry += " LEFT OUTER JOIN ("
            Qry += " select TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE, TSPL_GENERATE_SALARY_ATTENDANCE.NET_SALARY   from TSPL_GENERATE_SALARY_ATTENDANCE  "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE "
            Qry += " WHERE TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "') AS T1 ON T1.EMP_CODE =FINAL .EMP_CODE "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER T2  ON FINAL.EMP_CODE =  T2.EMP_CODE "
            Qry += " LEFT OUTER JOIN TSPL_DESIGNATION_MASTER  T3  ON T3.Designation_id   =  T2.Designation "
            Qry += " LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER  T4  ON T4.DEPARTMENT_CODE    =  T2.DEPARTMENT_CODE "
            Qry += " ORDER BY T2.EMP_CODE"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.Columns("EMP_CODE").HeaderText = "Employee Id"
                'gv1.Columns.FindByHeaderText("EMP_CODE")
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmForm_T_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmForm_T)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub frmForm_T_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        End If
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Form - T")
        arr.Add("For Pay Period  " + txtFromPP.Value)
        arr.Add("as on : " + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToExcelGrid("Form - T", gv1, arr, "Form - T", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Form - T")
        arr.Add("For Pay Period  " + txtFromPP.Value)
        arr.Add("as on : " + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Form - T", gv1, arr, "Form - T", False)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)
    End Sub
    Sub LoadGridColumns()
        Dim DT_CBO As New DataTable
        DT_CBO.Columns.Add("Code", GetType(Integer))
        DT_CBO.Columns.Add("Name", GetType(String))

        Dim Dr As DataRow = DT_CBO.NewRow()
        Dr("Code") = 0
        Dr("Name") = "Without Reason"
        DT_CBO.Rows.Add(Dr)

        Dr = DT_CBO.NewRow()
        Dr("Code") = 1
        Dr("Name") = "On Leave"
        DT_CBO.Rows.Add(Dr)

        Dr = DT_CBO.NewRow()
        Dr("Code") = 2
        Dr("Name") = "Left Service"
        DT_CBO.Rows.Add(Dr)

        Dr = DT_CBO.NewRow()
        Dr("Code") = 3
        Dr("Name") = "Retired"
        DT_CBO.Rows.Add(Dr)

        Dr = DT_CBO.NewRow()
        Dr("Code") = 4
        Dr("Name") = "Out of Coverage"
        DT_CBO.Rows.Add(Dr)

        Dr = DT_CBO.NewRow()
        Dr("Code") = 5
        Dr("Name") = "Expired"
        DT_CBO.Rows.Add(Dr)

        DT_CBO.AcceptChanges()

        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.ReadOnly = False

        Dim lineNo As New GridViewTextBoxColumn
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colLineNo
        lineNo.Width = 75
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)

        Dim empCode As New GridViewTextBoxColumn
        empCode.FormatString = ""
        empCode.HeaderText = "Employee Code"
        empCode.Name = colEmpCode
        empCode.Width = 150
        empCode.ReadOnly = True
        empCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(empCode)

        Dim ESICode As New GridViewTextBoxColumn
        ESICode.FormatString = ""
        ESICode.HeaderText = "ESI No"
        ESICode.Name = colESICode
        ESICode.Width = 150
        ESICode.ReadOnly = True
        ESICode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ESICode)

        Dim empName As New GridViewTextBoxColumn
        empName.FormatString = ""
        empName.HeaderText = "Employee Name"
        empName.Name = colEmpName
        empName.Width = 200
        empName.ReadOnly = True
        empName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(empName)

        Dim Days As New GridViewTextBoxColumn
        Days.FormatString = ""
        Days.HeaderText = "Payable Days"
        Days.Name = colDays
        Days.Width = 100
        Days.ReadOnly = True
        Days.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Days)

        Dim Wages As New GridViewTextBoxColumn
        Wages.FormatString = ""
        Wages.HeaderText = "Total Monthly Wages"
        Wages.Name = colMonthWages
        Wages.Width = 150
        Wages.ReadOnly = True
        Wages.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Wages)

        Dim firstHalf As New GridViewComboBoxColumn
        firstHalf.DataSource = DT_CBO
        firstHalf.ValueMember = "Code"
        firstHalf.DisplayMember = "Name"
        firstHalf.ReadOnly = False
        firstHalf.FormatString = ""
        firstHalf.HeaderText = "Reason Code"
        firstHalf.Name = colResionCode
        firstHalf.Width = 100
        firstHalf.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(firstHalf)

        Dim attendanceDate As New GridViewTextBoxColumn
        attendanceDate.FormatString = ""
        attendanceDate.HeaderText = "Last Working Day"
        attendanceDate.Name = colLastWorkDate
        attendanceDate.Width = 150
        attendanceDate.ReadOnly = True
        attendanceDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(attendanceDate)
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        LoadData()
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Form - T")
        arr.Add("For Pay Period  " + txtFromPP.Value)
        arr.Add("as on : " + clsCommon.GETSERVERDATE() + " ")
        If gv1.Rows.Count <= 0 Then
            gv1.Focus()
            clsCommon.MyMessageBoxShow(Me, "Data not found.", Me.Text)
        Else
            clsCommon.MyExportToExcelGrid("Form - T", gv1, arr, "Form - T", False)
        End If
    End Sub

    Private Sub rmPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Adjustment Report ")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Adjustment Register", gv1, arr, "Adjustment Register", False)
    End Sub
End Class
