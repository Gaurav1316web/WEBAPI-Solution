
Imports common
Imports Telerik.WinControls.UI
Imports System.IO
Imports XpertERPEngine

Public Class rptPerformanceRating
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "rptPerformRating"

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRPerformanceRatingRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Sub LoadUser()
        Dim qry As String = "select Emp_code as [Code],Emp_NAME as [Emp Name] from TSPL_EMPLOYEE_MASTER  "
        cbgTelecaller.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTelecaller.ValueMember = "Code"
        cbgTelecaller.DisplayMember = "Emp Name"
    End Sub

    Sub LoadDepartment()
        Dim qry As String = "select Department_Code as [Code], Department_Name as [Name] from TSPL_Department_Master  "
        cbgDepartment.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDepartment.ValueMember = "Code"
        cbgDepartment.DisplayMember = "Name"
    End Sub

    Private Sub rptPerformanceRating_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt And e.KeyCode = Keys.E Then
            printdata(EnumExportTo.Excel)
        ElseIf e.Alt And e.KeyCode = Keys.P Then
            printdata(EnumExportTo.PDF)
        End If
    End Sub

    Private Sub rptPerformanceRating_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadUser()
        LoadDepartment()
        chkTeleCallerAll.IsChecked = True
        chkDepartmentAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        txtTodate.Value = clsCommon.GETSERVERDATE()

    End Sub

    Private Sub chkTeleCallerAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTeleCallerAll.ToggleStateChanged
        cbgTelecaller.Enabled = Not chkTeleCallerAll.IsChecked
    End Sub

    Private Sub chkDepartmentAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDepartmentAll.ToggleStateChanged
        cbgDepartment.Enabled = Not chkDepartmentAll.IsChecked
    End Sub

    Sub LoadData()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.ReadOnly = True

        If chkTelecallerSelect.IsChecked AndAlso cbgTelecaller.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one User.")
        End If

        If chkDepartmentSelect.IsChecked AndAlso cbgDepartment.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Department.")
        End If

        If txtFromDate.Value > txtTodate.Value Then
            Throw New Exception("From Month can not be greater than To Month.")
        End If

        Dim strFromDate As Date = clsCommon.myCDate("01/" + txtFromDate.Value.Month.ToString() + "/" + txtFromDate.Value.Year.ToString())
        Dim strToDate As Date = clsCommon.myCDate("01/" + txtTodate.Value.Month.ToString() + "/" + txtTodate.Value.Year.ToString())
        strToDate = strToDate.AddMonths(1)
        strToDate = strToDate.AddDays(-1)

        Dim StrQuy As String = " select TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,MAX(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME) As [Department Name],TSPL_HR_PERFORMANCE_RATING.Emp_Code,TSPL_HR_PERFORMANCE_RATING.MONTH_YEAR, TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_GROUP, " & _
            " max(TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent) as [Group_per], SUM(TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_PERSENT_GAIN) as [Gain], " & _
            " ((SUM(TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_PERSENT_GAIN) * max(TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent)) / 100) as [Total_Gain]" & _
            " from TSPL_HR_PERFORMANCE_RATING " & _
            " left outer join TSPL_HR_PERFORMANCE_GROUP_MAPPING on TSPL_HR_PERFORMANCE_GROUP_MAPPING.Emp_Code = TSPL_HR_PERFORMANCE_RATING.Emp_Code and TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code = TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_GROUP " & _
            " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.Emp_Code = TSPL_HR_PERFORMANCE_RATING.Emp_Code" & _
            " LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER  on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE = TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE  " & _
            " where TSPL_HR_PERFORMANCE_RATING.MONTH_YEAR >= '" + clsCommon.GetPrintDate(strFromDate, "dd/MMM/yyyy") + "' and TSPL_HR_PERFORMANCE_RATING.MONTH_YEAR <= '" + clsCommon.GetPrintDate(strToDate, "dd/MMM/yyyy") + "' " & _
            ""

        If chkTelecallerSelect.IsChecked AndAlso cbgTelecaller.CheckedValue.Count > 0 Then
            StrQuy += " and TSPL_HR_PERFORMANCE_RATING.Emp_Code in (" + clsCommon.GetMulcallString(cbgTelecaller.CheckedValue) + ")"
        End If

        If chkDepartmentSelect.IsChecked AndAlso cbgDepartment.CheckedValue.Count > 0 Then
            StrQuy += " and TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE in (" + clsCommon.GetMulcallString(cbgDepartment.CheckedValue) + ")"
        End If

        StrQuy += " group by TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,TSPL_HR_PERFORMANCE_RATING.Emp_Code,TSPL_HR_PERFORMANCE_RATING.MONTH_YEAR,TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_GROUP "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuy)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
            Exit Sub
        End If
        gv1.DataSource = dt
        gv1.MasterTemplate.AllowAddNewRow = False
        SetGridFormationOFGV1()
        ReStoreGridLayout()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).Width = 100
        Next

        gv1.Columns("DEPARTMENT_CODE").Width = 120
        gv1.Columns("DEPARTMENT_CODE").HeaderText = "Department Code"

        gv1.Columns("DEPARTMENT NAME").Width = 250

        gv1.Columns("Emp_Code").Width = 150
        gv1.Columns("Emp_Code").HeaderText = "Employee"
        gv1.Columns("PERFORMANCE_GROUP").Width = 200
        gv1.Columns("PERFORMANCE_GROUP").HeaderText = "Performance Group"

        gv1.Columns("Group_per").Width = 100
        gv1.Columns("Group_per").FormatString = "{0:n2}"
        gv1.Columns("Group_per").HeaderText = "Group Persent"

        gv1.Columns("MONTH_YEAR").Width = 100
        'gv1.Columns("MONTH_YEAR").CustomFormat = "MM-yyyy"
        gv1.Columns("MONTH_YEAR").FormatString = "{0:MMM-yyyy}"
        gv1.Columns("MONTH_YEAR").HeaderText = "Month/Year"

        gv1.Columns("Gain").Width = 100
        gv1.Columns("Gain").IsVisible = False
        gv1.Columns("Gain").FormatString = "{0:n2}"

        gv1.Columns("Total_Gain").Width = 100
        gv1.Columns("Total_Gain").HeaderText = "Total Gain"
        gv1.Columns("Total_Gain").FormatString = "{0:n2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.AllowDeleteRow = False
        gv1.EnableAlternatingRowColor = True
        gv1.MasterView.TableFilteringRow.IsCurrent = True
        gv1.Columns(0).IsCurrent = True
        gv1.Focus()
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        ExcelFormate = 2
    End Enum

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            gv1.EnableFiltering = True
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Sub Reset()
        LoadUser()
        chkTeleCallerAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
        gv1.Columns.Clear()
        LoadDepartment()
        chkDepartmentAll.IsChecked = True
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Public Sub printdata(ByVal exporter As EnumExportTo)
        Try
            LoadData()
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = ""
            arrHeader.Add(strtemp)

            arrHeader.Add("From Date : " + txtFromDate.Value)
            arrHeader.Add("To Date : " + txtTodate.Value)
            If chkTelecallerSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgTelecaller.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("User : " + strtemp)
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Performance Rating ", gv1, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.ExcelFormate Then
                clsCommon.MyExportToExcelGrid("Performance Rating", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Performance Rating", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        printdata(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        printdata(EnumExportTo.PDF)
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

    Private Sub mbtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If
    End Sub

    Private Sub mbtnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayout.Click
        If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Deleted successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub BtnExcelFormate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        printdata(EnumExportTo.ExcelFormate)
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick

        If clsCommon.myLen(gv1.CurrentRow.Cells("Emp_Code").Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells("MONTH_YEAR").Value) > 0 Then
            Dim frm As New FrmPerformanceRating()
            frm.SetUserMgmt(clsUserMgtCode.frmHRPerformanceRating)
            frm.Emp_Code = clsCommon.myCstr(gv1.CurrentRow.Cells("Emp_Code").Value)
            frm.SelectedMonth = clsCommon.myCDate(gv1.CurrentRow.Cells("MONTH_YEAR").Value)
            frm.WindowState = FormWindowState.Normal
            frm.Show()
        End If

    End Sub

End Class
