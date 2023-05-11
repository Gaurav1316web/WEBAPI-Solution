Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
' Ticket no : ERO/25/03/19-000520 for jw estimate screen 
' Ticket no : ERO/25/03/19-000521 for jw estimate screen  (print) 
' Ticket no :ERO/25/03/19-000518 for jernal weighment 
' Ticket No : ERO/01/04/19-000529 By Prabhakar 
' Ticket No : ERO/01/04/19-000530 for jw estimate screen (print[rpt])
Public Class rptBiometricAttendanceReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "rptBiometricAttendanceReport"
    Dim arrBack As New List(Of String)
    Dim arrEmp As New ArrayList()
    Dim arrLeave As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region

    Sub LoadData()
        Try
            Dim qry As String = "  select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Employee Code] ,TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],TSPL_BIOMETRIC_RAW_DATA.Machine_Sr_No as [Machine SNO],TSPL_BIOMETRIC_RAW_DATA.Emp_ID as [Biometric ID],TSPL_BIOMETRIC_RAW_DATA.In_Out_Date as [In/Out Date] " & _
                                "  from TSPL_BIOMETRIC_RAW_DATA " & _
                                " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.BioMetricEmpID=TSPL_BIOMETRIC_RAW_DATA.Emp_ID " & _
                                " "
            qry += " where 2=2 "
            qry += " and CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,103)>=CONVERT(Date,'" & txtFromDate.Value.Date & "',103) AND CONVERT(Date, TSPL_BIOMETRIC_RAW_DATA.In_Out_Date,103)<=CONVERT(Date,'" & txtToDate.Value.Date & "',103)"
            If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE in (" + clsCommon.GetMulcallString(TxtMultiEmployee.arrValueMember) + ")  "
            End If
            qry += "  order by In_Out_Date "
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
                btnGenrate.Enabled = False
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        TxtMultiEmployee.Enabled = True
        btnGenrate.Enabled = True
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
        arr.Add("Biometric Attendance Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToExcelGrid("Biometric Attendance Report", gv3, arr, "Biometric Attendance Report", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Biometric Attendance Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Attendance Report", gv3, arr, "Biometric Attendance Report", False)
    End Sub

#Region "grid operations"

    'Private Sub SetupMasterForAutoGenerateHierarchy()
    '    Using Me.gv3.DeferRefresh()
    '        Me.gv3.AutoGenerateHierarchy = True
    '        Me.gv3.MasterTemplate.Reset()
    '        Me.gv3.TableElement.RowHeight = 20
    '        'Me.gv3.DataSource = DT
    '        Me.gv3.MasterTemplate.Columns("empcode").HeaderText = "empcode"
    '        Me.gv3.MasterTemplate.Columns("EmpName").HeaderText = "empname"
    '        Me.gv3.MasterTemplate.Columns("leavecode").HeaderText = "leavecode"
    '        'Me.gv3.MasterTemplate.Columns("LEAVE_Name").HeaderText = "Leave Name"
    '        'Me.gv3.MasterTemplate.Columns("OPENING").HeaderText = "Opening"
    '        Me.gv3.MasterTemplate.Columns("Alloted").HeaderText = "Alloted"
    '        Me.gv3.MasterTemplate.Columns("Taken").HeaderText = "Taken"
    '        'Me.gv3.MasterTemplate.Columns("ADJUSTMENT_PLUS").HeaderText = "Adjustment Plus"
    '        'Me.gv3.MasterTemplate.Columns("ADJUSTMENT_MINUS").HeaderText = "Adjustment Minus"
    '        Me.gv3.MasterTemplate.Columns("Balance").HeaderText = "Balance"
    '        Me.gv3.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill

    '    End Using
    'End Sub

#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)

        arr.Add("Biometric Attendance Report")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
            arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
        End If

        If gv3.Rows.Count <= 0 Then
            gv3.Focus()
            clsCommon.MyMessageBoxShow("Data not found.")
        Else
            clsCommon.MyExportToExcelGrid("Biometric Attendance Report", gv3, arr, "Biometric Attendance Report", False)
        End If

    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)

        arr.Add("Biometric Attendance Report")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
            arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
        End If
        clsCommon.MyExportToPDF("Biometric Attendance Report", gv3, arr, "Biometric Attendance Report", False)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBiometricAttendanceReport & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'If TxtMultiEmployee.arrValueMember IsNot Nothing AndAlso TxtMultiEmployee.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtMultiEmployee.arrDispalyMember))
            'End If
            transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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


End Class
