'--29/07/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmESIOnline
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "frmESIOnline"

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
                clsCommon.MyMessageBoxShow("Please Select Pay Period First.")
            End If
            Dim Qry As String = ""

            Qry += " select T1.EMP_CODE, T3.ESI_NO,T3.Emp_Name, T2.PAYABLE_DAYS, T1.HEAD_VALUE AS ESI_EARNING, T1.ACTUAL_AMOUNT as EMPESI,T3.rel_date,0 as 'Reason_Code',  "
            Qry += " (CASE WHEN T1.ACTUAL_AMOUNT  > 0 THEN (T1.HEAD_VALUE*.0475) ELSE 0.00  END ) AS COESI,"
            Qry += " T6.PAY_PERIOD_CODE "
            Qry += " from TSPL_GENERATE_SALARY_PAYHEADS T1 "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T2 ON T2.EMP_CODE = T1.EMP_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER  T3 ON T3.EMP_CODE = T1.EMP_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_PAYHEAD_MASTER  T5 ON T5.PAY_HEAD_CODE  = T1.PAY_HEAD_CODE  "
            Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY T6 ON T6.SALARY_GENERATION_CODE = T1.SALARY_GENERATION_CODE  "
            Qry += " WHERE T5.SUB_HEAD_TYPE  = 'EMPESI' and T3.ISESI  =1 AND T6.PAY_PERIOD_CODE= '" + txtFromPP.Value + "' "
            If ChkIncludeZero.Checked = False Then
                Qry += " and T1.ACTUAL_AMOUNT > 0 "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim ii As Int16 = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    ii = ii + 1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = ii
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = dr("EMP_CODE")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colESICode).Value = dr("ESI_NO")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpName).Value = dr("Emp_Name")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDays).Value = dr("PAYABLE_DAYS")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMonthWages).Value = dr("ESI_EARNING")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colResionCode).Value = dr("Reason_Code")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLastWorkDate).Value = dr("RELIEVING_DATE")
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmESIOnline_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmESIOnline)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub frmESIOnline_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
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

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpoExl.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("ESI Online")
        arr.Add("as on : " + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("ESI Online", gv1, arr, "ESI Online")
        clsCommon.MyExportToExcelGrid("ESI Online", gv1, arr, "ESI Online", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpoPDF.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("ESI Online")
        arr.Add("as on : " + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("ESI Online", gv1, arr, "ESI Online", False)
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
End Class
