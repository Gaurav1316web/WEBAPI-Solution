
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.IO
'========> created by shivani

Public Class RptEmployeeBday6
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptEmployeeBday6)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtDepartment__My_Click(sender As Object, e As EventArgs) Handles txtDepartment._My_Click
        Dim qry As String = " select DEPARTMENT_CODE as Code,DEPARTMENT_NAME as Name  from TSPL_DEPARTMENT_MASTER "
        txtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtDepartment.arrValueMember, txtDepartment.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtDepartment, "DEPARTMENT_NAME", "TSPL_DEPARTMENT_MASTER", "DEPARTMENT_CODE")
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        '' against ticket No : BM00000006769
        Dim qry As String = "select TSPL_EMPLOYEE_MASTER.LOCATION_CODE ,Location_Desc,EMP_CODE ,Emp_Name,convert(varchar,Birth_date,103) as Birth_date ,convert(varchar,ANNIVERSARY_DATE,103) as  ANNIVERSARY_DATE ,TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,DEPARTMENT_NAME,TSPL_EMPLOYEE_MASTER.Designation ,Designation_Desc ,TSPL_EMPLOYEE_MASTER.DEVISION_CODE  ,TSPL_DEVISION_MASTER.DEVISION_NAME   from TSPL_EMPLOYEE_MASTER left join TSPL_LOcation_Master on TSPL_LOcation_Master.Location_Code =TSPL_EMPLOYEE_MASTER.LOCATION_CODE left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_EMPLOYEE_MASTER.DEVISION_CODE  left join TSPL_DEPARTMENT_MASTER  on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE =TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE "
        qry += " left join TSPL_DESIGNATION_MASTER on  TSPL_DESIGNATION_MASTER.Designation_id =TSPL_EMPLOYEE_MASTER.Designation "
        If rbtnBirthDate.IsChecked = True Then
            qry += " where  month(convert(date,TSPL_EMPLOYEE_MASTER.Birth_date ,103))>= month(convert(date,'" & txtFromDate.Value & "',103)) and month(convert(date,TSPL_EMPLOYEE_MASTER.Birth_date,103))<= month(convert(date,'" & txtToDate.Value & "',103)) and Day(convert(date,TSPL_EMPLOYEE_MASTER.Birth_date ,103))>= Day(convert(date,'" & txtFromDate.Value & "',103)) and Day(convert(date,TSPL_EMPLOYEE_MASTER.Birth_date,103))<= Day(convert(date,'" & txtToDate.Value & "',103))"
        End If
        If rbtnAnniversaryDate.IsChecked = True Then
            qry += " where  month(convert(date,TSPL_EMPLOYEE_MASTER.ANNIVERSARY_DATE ,103))>= month(convert(date,'" & txtFromDate.Value & "',103)) " & _
                   " and month(convert(date,TSPL_EMPLOYEE_MASTER.ANNIVERSARY_DATE,103))<= month(convert(date,'" & txtToDate.Value & "',103)) " & _
                   " and Day(convert(date,TSPL_EMPLOYEE_MASTER.ANNIVERSARY_DATE ,103))>= Day(convert(date,'" & txtFromDate.Value & "',103)) " & _
                   " and Day(convert(date,TSPL_EMPLOYEE_MASTER.ANNIVERSARY_DATE,103))<= Day(convert(date,'" & txtToDate.Value & "',103))"
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If
        If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE in (" & clsCommon.GetMulcallString(txtDepartment.arrValueMember) & " )"
        End If

        If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
            qry += " and TSPL_EMPLOYEE_MASTER.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
        End If
        If rbtnBirthDate.IsChecked = True Then
            qry += " order by month(convert(date,Birth_date,103)),day(convert(date,Birth_date,103))"
        Else
            qry += " order by month(convert(date,ANNIVERSARY_DATE,103)),day(convert(date,ANNIVERSARY_DATE,103))"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
            Panel1.Enabled = False
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
    Sub FormatGrid()


        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("LOCATION_CODE").IsVisible = True
        gv.Columns("LOCATION_CODE").Width = 150
        gv.Columns("LOCATION_CODE").HeaderText = "Location Code"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 150
        gv.Columns("Location_Desc").HeaderText = "Location Desc"


        gv.Columns("DEVISION_CODE").IsVisible = True
        gv.Columns("DEVISION_CODE").Width = 150
        gv.Columns("DEVISION_CODE").HeaderText = "Division code"

        gv.Columns("DEVISION_NAME").IsVisible = True
        gv.Columns("DEVISION_NAME").Width = 150
        gv.Columns("DEVISION_NAME").HeaderText = "Division Name"


        gv.Columns("EMP_CODE").IsVisible = True
        gv.Columns("EMP_CODE").Width = 150
        gv.Columns("EMP_CODE").HeaderText = "Employee Code"

        If rbtnBirthDate.IsChecked = True Then
            gv.Columns("Birth_date").IsVisible = True
            gv.Columns("Birth_date").Width = 150
            gv.Columns("Birth_date").HeaderText = "Birth Date"
            gv.Columns("Birth_date").FormatString = "{0:d}"
        Else
            gv.Columns("Birth_date").IsVisible = False
            gv.Columns("Birth_date").Width = 150
            gv.Columns("Birth_date").HeaderText = "Birth Date"
            gv.Columns("Birth_date").FormatString = "{0:d}"
        End If


        gv.Columns("Emp_Name").IsVisible = True
        gv.Columns("Emp_Name").Width = 90
        gv.Columns("Emp_Name").HeaderText = "Employee Name"

        If rbtnAnniversaryDate.IsChecked = True Then
            gv.Columns("ANNIVERSARY_DATE").IsVisible = True
            gv.Columns("ANNIVERSARY_DATE").Width = 150
            gv.Columns("ANNIVERSARY_DATE").HeaderText = "Anniversary Date"
            gv.Columns("ANNIVERSARY_DATE").FormatString = "{0:d}"
        Else
            gv.Columns("ANNIVERSARY_DATE").IsVisible = False
            gv.Columns("ANNIVERSARY_DATE").Width = 150
            gv.Columns("ANNIVERSARY_DATE").HeaderText = "Anniversary Date"
            gv.Columns("ANNIVERSARY_DATE").FormatString = "{0:d}"
        End If


        gv.Columns("DEPARTMENT_CODE").IsVisible = True
        gv.Columns("DEPARTMENT_CODE").Width = 90
        gv.Columns("DEPARTMENT_CODE").HeaderText = "Department Code"

        gv.Columns("DEPARTMENT_NAME").IsVisible = True
        gv.Columns("DEPARTMENT_NAME").Width = 90
        gv.Columns("DEPARTMENT_NAME").HeaderText = "Department Name"

        gv.Columns("Designation").IsVisible = True
        gv.Columns("Designation").Width = 90
        gv.Columns("Designation").HeaderText = "Designation Code"

        gv.Columns("Designation_Desc").IsVisible = True
        gv.Columns("Designation_Desc").Width = 90
        gv.Columns("Designation_Desc").HeaderText = "Designation Name"





        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Panel1.Enabled = True
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        'print(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptEmployeeBday6 & "'"))
                arrHeader.Add("Report Type : " & IIf(rbtnBirthDate.IsChecked = True, "Birth Date", "Anniversary Date"))

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If txtDepartment.arrDispalyMember IsNot Nothing AndAlso txtDepartment.arrDispalyMember.Count > 0 Then
                    arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrDispalyMember))
                End If
                If txtDivisionMult.arrDispalyMember IsNot Nothing AndAlso txtDivisionMult.arrDispalyMember.Count > 0 Then
                    arrHeader.Add(" Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Birthday and Anniversary Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtDepartment.arrDispalyMember IsNot Nothing AndAlso txtDepartment.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrDispalyMember))
            End If
            If txtDivisionMult.arrDispalyMember IsNot Nothing AndAlso txtDivisionMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
            End If


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Birthday and Anniversary Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Birthday and Anniversary Report", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RptEmployeeBday6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        rbtnBirthDate.IsChecked = True


    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class
