Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
'' work done agasit ticket no. ERO/13/08/18-000392,ERO/16/10/18-000407
' Ticket No : ERO/04/04/19-000541 By Prabhakar 
Public Class RptAttendaceReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "RptAttendaceReport"
    'Dim arrBack As New List(Of String)
    'Dim arrEmp As New ArrayList()
    'Dim arrLeave As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region
    
    Sub LoadData()
        Try
            Dim dtFrom As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
            Dim dtTo As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
            If txtMonth.Checked = True Then
                txtFromDate.Value = dtFrom
                txtToDate.Value = dtTo
            End If

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            Dim strDateColumnForIN As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STUFF(a.strr,1,1,'') from (select (select +',['+Format(thedate,'dd/MMM/yyy')+'(IN)]'  from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr )a"))
            Dim strDateColumnForOUT As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STUFF(a.strr,1,1,'') from ( select (select +',['+Format(thedate,'dd/MMM/yyy')+'(OUT)]'  from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr )a"))
            Dim strDateColumnForIN_OUT As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select +',['+Format(thedate,'dd/MMM/yyy')+'(IN)]' + ',' + '['+Format(thedate,'dd/MMM/yyy')+'(OUT)]' from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr"))
            Dim strDateColumnforIN_OUT_MAX As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select +',max(['+Format(thedate,'dd/MMM/yyy')+'(IN)]) as '+ '['+Format(thedate,'dd/MMM/yyy')+'(IN)]'+',' + 'max(['+Format(thedate,'dd/MMM/yyy')+'(OUT)]) as ' + '['+Format(thedate,'dd/MMM/yyy')+'(OUT)]'  from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr "))
            Dim qry As String = " select [Employee Code],max([Employee Name]) as [Employee Name],max (Gender) as Gender , max ([Department Name]) as [Department Name] " + strDateColumnforIN_OUT_MAX + "  from ( select [Employee Code],[Employee Name],Gender, [Department Name] "
                qry += " " + strDateColumnForIN_OUT + " "
                qry += " from ( select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code] ,max(TSPL_EMPLOYEE_MASTER.Emp_Name) as [Employee Name],Max(TSPL_EMPLOYEE_MASTER.SEX) as Gender "
            qry += " ,max(TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME) as [Department Name],Format(TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,'dd/MMM/yyy')+'(IN)' as AttendanceDate,Format(TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,'dd/MMM/yyy')+'(OUT)' as AttendanceDate2 , min( Format(TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,'HH:mm')) as AttendanceTimeIn ,(case when min( Format(TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,'HH:mm:ss'))=max( Format(TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,'HH:mm:ss')) then null else max( Format(TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,'HH:mm')) end) as AttendanceTimeOut "
            qry += " from TSPL_EMPLOYEE_MASTER "
                qry += " left outer join TSPL_BIOMETRIC_RAW_DATA on TSPL_EMPLOYEE_MASTER.BioMetricEmpID=TSPL_BIOMETRIC_RAW_DATA.Emp_ID "
                qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE =  TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE "
                qry += " where 2=2  and CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,103)>=CONVERT(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) AND CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,103)<=CONVERT(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103)  "
                If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE in (" + clsCommon.GetMulcallString(TxtMultiEmployee.arrValueMember) + ")  "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_MASTER.LOCATION_CODE in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                End If
                If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                    qry += " and TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE in (" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ")  "
                End If
                qry += " Group by TSPL_EMPLOYEE_MASTER.EMP_CODE , Format(TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,'dd/MMM/yyy') "
                qry += " ) XXX "
                qry += " PIVOT "
                qry += " ( "
                qry += " min(AttendanceTimeIn) "
                qry += " FOR AttendanceDate IN ( "
                qry += " " + strDateColumnForIN + " "
                qry += " ) "
                qry += " ) AS PivotTable "
                qry += " PIVOT "
                qry += " ( "
                qry += " max(AttendanceTimeOut) "
                qry += " FOR AttendanceDate2 IN ( "
                qry += " " + strDateColumnForOUT + ""
            qry += " )) AS PivotTable2 ) PPPP group by [Employee Code] order by [Department Name] "
                
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv3.DataSource = Nothing

                    gv3.Rows.Clear()
                    gv3.Columns.Clear()

                    gv3.DataSource = dt

                    gv3.GroupDescriptors.Clear()
                    gv3.MasterTemplate.SummaryRowsBottom.Clear()
                    gv3.EnableGrouping = False
                    gv3.EnableFiltering = True

                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv3.MasterTemplate.AllowAddNewRow = False
                    FormatGridDetails()

                gv3.BestFitColumns()

                txtFromDate.Enabled = False
                txtToDate.Enabled = False
                TxtMultiEmployee.Enabled = False
                txtDepartment.Enabled = False
                txtLocation.Enabled = False
                btnGenrate.Enabled = False
                txtMonth.Enabled = False

                Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGenrate.Enabled = True
        End Try
    End Sub
    Sub FormatGridDetails()
        gv3.TableElement.TableHeaderHeight = 40
        gv3.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv3.Columns.Count - 1
            gv3.Columns(ii).ReadOnly = True
            gv3.Columns(ii).IsVisible = True
        Next
        'gv3.Columns("Duration In Minute").IsVisible = False
    End Sub
    Private Sub frmLeaveRegisterReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtMonth.Value = clsCommon.GETSERVERDATE()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnQuickExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        'txtFromDate.Value = clsCommon.GETSERVERDATE
        'txtToDate.Value = txtFromDate.Value
        btnGenrate.Enabled = True
        gv3.DataSource = Nothing
        TxtMultiEmployee.arrValueMember = Nothing
        txtDepartment.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtMonth.Checked = False
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        TxtMultiEmployee.Enabled = True
        txtDepartment.Enabled = True
        txtLocation.Enabled = True
        btnGenrate.Enabled = True
        txtMonth.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

   

    Private Sub frmLeaveRegisterReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
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
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
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
        arr.Add("Attendance Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToExcelGrid("Attendance Report", gv3, arr, "Attendance Report", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Attendance Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Attendance Report", gv3, arr, "Attendance Report", False)
    End Sub

    '#Region "grid operations"

    '    Private Sub SetupMasterForAutoGenerateHierarchy()
    '        Using Me.gv3.DeferRefresh()
    '            Me.gv3.AutoGenerateHierarchy = True
    '            Me.gv3.MasterTemplate.Reset()
    '            Me.gv3.TableElement.RowHeight = 20
    '            'Me.gv3.DataSource = DT
    '            Me.gv3.MasterTemplate.Columns("empcode").HeaderText = "empcode"
    '            Me.gv3.MasterTemplate.Columns("EmpName").HeaderText = "empname"
    '            Me.gv3.MasterTemplate.Columns("leavecode").HeaderText = "leavecode"
    '            'Me.gv3.MasterTemplate.Columns("LEAVE_Name").HeaderText = "Leave Name"
    '            'Me.gv3.MasterTemplate.Columns("OPENING").HeaderText = "Opening"
    '            Me.gv3.MasterTemplate.Columns("Alloted").HeaderText = "Alloted"
    '            Me.gv3.MasterTemplate.Columns("Taken").HeaderText = "Taken"
    '            'Me.gv3.MasterTemplate.Columns("ADJUSTMENT_PLUS").HeaderText = "Adjustment Plus"
    '            'Me.gv3.MasterTemplate.Columns("ADJUSTMENT_MINUS").HeaderText = "Adjustment Minus"
    '            Me.gv3.MasterTemplate.Columns("Balance").HeaderText = "Balance"
    '            Me.gv3.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill

    '        End Using
    '    End Sub

    '#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)

        arr.Add("Attendance Report")
        arr.Add("Date Range:-" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + " ")
        'If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
        '    arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
        'End If

        If gv3.Rows.Count <= 0 Then
            gv3.Focus()
            clsCommon.MyMessageBoxShow(Me, "Data not found.", Me.Text)
        Else
            clsCommon.MyExportToExcelGrid("Leave Register Report", gv3, arr, "Leave Register Report", False)
        End If

    End Sub

    'Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim arr As New List(Of String)()
    '    arr.Add(objCommonVar.CurrentCompanyName)

    '    arr.Add("Leave Register Report (Detail)")
    '    arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
    '    If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
    '        arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
    '    End If
    '    clsCommon.MyExportToPDF("Leave Register Report", gv3, arr, "Leave Register Report", False)
    'End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptAttendanceReport & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range:-" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + " ")
            'If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
            'End If
            transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

 
    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtMultiEmployee._My_Click
        Dim qry As String = "select EMP_CODE AS [Code],Emp_Name as [Name],tspl_employee_MASTER.Location_Code as Loaction, tspl_employee_MASTER.Devision_Code as Division " & _
            " from tspl_employee_MASTER left join tspl_location_master on tspl_location_master.Location_Code =tspl_employee_MASTER.LOCATION_CODE  "

        TxtMultiEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", TxtMultiEmployee.arrValueMember, TxtMultiEmployee.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub

 
    Private Sub txtDepartment__My_Click(sender As Object, e As EventArgs) Handles txtDepartment._My_Click
        Dim qry As String = "select TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE as Code, TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as Name,TSPL_DEPARTMENT_MASTER.DESCRIPTION as Description from TSPL_DEPARTMENT_MASTER "
        txtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("DepartmentMulSel", qry, "Code", "Name", txtDepartment.arrValueMember, txtDepartment.arrDispalyMember)
    End Sub

    
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Name],TSPL_Location_MASTER.Loc_Short_Name as [Short Name] from TSPL_Location_MASTER  where Location_Type='Physical' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationMulSelATTrpt", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        If txtMonth.Checked = True Then
            txtFromDate.Enabled = False
            txtToDate.Enabled = False
        Else
            txtFromDate.Enabled = True
            txtToDate.Enabled = True
        End If
    End Sub

    Private Sub txtMonth_Click(sender As Object, e As EventArgs) Handles txtMonth.Click
        If txtMonth.Checked = True Then
            txtFromDate.Enabled = True
            txtToDate.Enabled = True
        Else
            txtFromDate.Enabled = False
            txtToDate.Enabled = False
        End If
    End Sub

    'Private Sub txtMonth_VisibleChanged(sender As Object, e As EventArgs) Handles txtMonth.VisibleChanged
    '    If txtMonth.Checked = True Then
    '        txtFromDate.Enabled = False
    '        txtToDate.Enabled = False
    '    Else
    '        txtFromDate.Enabled = True
    '        txtToDate.Enabled = True
    '    End If
    'End Sub
End Class
