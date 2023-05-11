Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine

' Ticket No : BHA/16/04/19-000861,BHA/23/04/19-000865 By Prabhakar - Create New Report
Public Class rptEmployeeStatusReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isShowEmployeeCurrentSalary As Boolean = False
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try

            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
                Exit Sub
            End If

            Dim qry As String = Nothing

            qry = " Select TSPL_EMPLOYEE_MASTER.EMP_CODE as [EMP CODE], TSPL_EMPLOYEE_MASTER.Emp_Name as [Emp Name] ,TSPL_EMPLOYEE_MASTER.Designation as [Designation Code],TSPL_DESIGNATION_MASTER.Designation_Desc as [Designation Name], TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE as [Department Code] ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name],convert (varchar, TSPL_EMPLOYEE_MASTER.Joining_Date ,103) as [Joining Date] , convert ( varchar, TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) as [Relieving Date], " & _
                  " CASE " & _
                  " WHEN DATEDIFF(YEAR, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) < 1 THEN CAST(DATEDIFF(mm, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) AS VARCHAR)+' Months & '+CAST(DATEDIFF(dd, DATEADD(mm, DATEDIFF(mm, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)), Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103)), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) AS VARCHAR)+' Days' " & _
                  " WHEN DATEDIFF(YEAR, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) BETWEEN 1 AND 5 THEN CAST(DATEDIFF(mm, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) / 12 AS VARCHAR)+' Years & '+CAST(DATEDIFF(mm, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) % 12 AS VARCHAR)+' Months & '+CAST(DATEDIFF(dd, DATEADD(mm, DATEDIFF(mm, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)), Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103)), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) AS VARCHAR)+' Days' " & _
                  " WHEN DATEDIFF(YEAR, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) >= 6 THEN CAST(DATEDIFF(YEAR, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) AS VARCHAR)+' Years & '+CAST(DATEDIFF(mm, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) % 12 AS VARCHAR)+' Months & '+CAST(DATEDIFF(dd, DATEADD(mm, DATEDIFF(mm, Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)), Convert (date,TSPL_EMPLOYEE_MASTER.Joining_Date,103)), Convert (date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103)) AS VARCHAR)+' Days'  " & _
                  " END as [Duration Worked with Us] "
            If isShowEmployeeCurrentSalary = True Then
                qry += " ,TBL_LATEST_SALARY.Latest_Salary as [Latest Salary]  "
            End If
            qry += " from TSPL_EMPLOYEE_MASTER " & _
                  " Left Outer Join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id = TSPL_EMPLOYEE_MASTER.Designation " & _
                  " Left Outer Join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE "
            If isShowEmployeeCurrentSalary = True Then
                qry += "  left outer join (  select Sal.EMP_CODE ,Sal.Latest_Salary from (select Sal.*,Latest_Sal.Latest_Salary from TSPL_EMPLOYEE_SALARY Sal inner join  (select TSPL_EMPLOYEE_SALARY.EMP_CODE,max(TSPL_EMPLOYEE_SALARY.REVISION_NO) as REVISION_NO,Sum (RATE_AMOUNT)  as Latest_Salary from TSPL_EMPLOYEE_SALARY  " & _
                      "  inner join TSPL_EMPLOYEE_SALARY_PAYHEADS on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE = TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE  " & _
                      "  inner join  ( select  Emp_Code, max(REVISION_NO) as REVISION_NO from TSPL_EMPLOYEE_SALARY group by TSPL_EMPLOYEE_SALARY.Emp_Code) as TBL_Revision on TBL_Revision.Emp_Code = TSPL_EMPLOYEE_SALARY.EMP_CODE and TSPL_EMPLOYEE_SALARY.REVISION_NO = TBL_Revision.REVISION_NO   " & _
                      "  group by TSPL_EMPLOYEE_SALARY.EMP_CODE) as Latest_Sal  on Sal.EMP_CODE=Latest_Sal.EMP_CODE and Sal.REVISION_NO=Latest_Sal.REVISION_NO  " & _
                      "  ) Sal  LEFT JOIN TSPL_EMPLOYEE_MASTER Emp ON Sal.EMP_CODE=Emp.EMP_CODE   LEFT JOIN TSPL_SALARY_STRUCTURE Struct ON Sal.SALARY_STRUCTURE_CODE=Struct.SALARY_STRUCTURE_CODE) TBL_LATEST_SALARY on TBL_LATEST_SALARY.EMP_CODE = TSPL_EMPLOYEE_MASTER.EMP_CODE   "
            End If

            qry += " where 2=2 "
            If rbtByJoiningDate.Checked = True Then
                qry += " and convert(date,TSPL_EMPLOYEE_MASTER.Joining_Date,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_EMPLOYEE_MASTER.Joining_Date,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            End If
            If btnByRelievingDate.Checked = True Then
                qry += " and convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_EMPLOYEE_MASTER.RELIEVING_DATE,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            End If
            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ")  "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True

                gv1.EnableFiltering = True


                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()
            gv1.BestFitColumns()

            ReStoreGridLayout()


        Catch ex As Exception

        End Try
    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtEmployee.arrValueMember = Nothing
        Panel1.Visible = False
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptTankerStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
        isShowEmployeeCurrentSalary = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowEmpCurrentSalaryOnEmployeeSatatusReport, clsFixedParameterCode.ShowEmpCurrentSalaryOnEmployeeSatatusReport, Nothing)) = "1", True, False))
    End Sub


    'Private Sub TxtMultiToLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiToLocation._My_Click
    '    Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
    '    TxtMultiToLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiTo", qry, "Code", "Name", TxtMultiToLocation.arrValueMember, TxtMultiToLocation.arrDispalyMember)
    'End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Employee Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                'End If


                'If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Employee Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                'End If

                'If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Employee Status Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
    '    Dim qry As String = "select TSPL_VENDOR_MASTER.Vendor_Code as [Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Name] from TSPL_VENDOR_MASTER "
    '    txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiVendor", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    'End Sub

    Private Sub txtEmployee__My_Click(sender As Object, e As EventArgs) Handles txtEmployee._My_Click
        Dim qry As String = "select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Code] ,TSPL_EMPLOYEE_MASTER.Emp_Name as [Name] from TSPL_EMPLOYEE_MASTER "
        txtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@Employee", qry, "Code", "Name", txtEmployee.arrValueMember, txtEmployee.arrDispalyMember)
    End Sub

    Private Sub rbnAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbnAll.CheckedChanged
        Panel1.Visible = False
    End Sub

    Private Sub rbtByJoiningDate_CheckedChanged(sender As Object, e As EventArgs) Handles rbtByJoiningDate.CheckedChanged
        Panel1.Visible = True
    End Sub

    Private Sub btnByRelievingDate_CheckedChanged(sender As Object, e As EventArgs) Handles btnByRelievingDate.CheckedChanged
        Panel1.Visible = True
    End Sub
End Class
