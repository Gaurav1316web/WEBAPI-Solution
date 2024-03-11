'' New Report DOne agaist ticket no BHA/17/01/19-000784
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmEmployeeTDSRpt
    Inherits FrmMainTranScreen
    Const ReportID As String = "TDSReport"
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub FrmrptTDSLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = dtpFromDate.Value
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        PageSetupReport_ID = Me.Form_ID
        TemplateGridview = gvReport
        LoadData()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvReport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvReport.Columns.Count - 1 Step ii + 1
                        gvReport.Columns(ii).IsVisible = False
                        gvReport.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gvReport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If


            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub LoadData()
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()


        Dim FromdateFilter As String = Nothing
        Dim TodateFilter As String = Nothing
        Dim dt As DataTable = Nothing
        Dim qry As String = ""
        Dim strCodeColumn As String = ""
        Dim strTaxColumn As String = ""
        FromdateFilter = dtpFromDate.Value.ToString("dd/MM/yyyy")
        TodateFilter = dtpToDate.Value.ToString("dd/MM/yyyy")




        If RbtnSummary.IsChecked = True Then
            qry = "select ROW_NUMBER() OVER(ORDER BY TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code ASC) AS Row#,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_MASTER.SEX as Gender,TSPL_EMPLOYEE_MASTER.Birth_date as DOB,TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Fiscal_Code as [Year],TSPL_Fiscal_Year_Master.Fiscal_Name as [Description]"
            qry += " ,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Gross_Amt as [Gross Salary],TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Allowance_Amt+TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Section_Amt as Section_Amt,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Taxable_Amt,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Total_TDS_Amt "
            qry += " from "
            qry += " TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD"
            qry += " left outer join TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP on TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Code"
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code"
            qry += " left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Fiscal_Code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Fiscal_Code "
        Else

            Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable("select Code from TSPL_SECTION_ALLOWANCE_MASTER")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("Code")).Trim() + "]"
                Next
            End If
            '' Second Pivot tax 
            Dim dtTax As DataTable = clsDBFuncationality.GetDataTable("select distinct Tax_Code from TSPL_HR_TDS_INCOME_TAX_SLAB left outer join TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_HR_TDS_INCOME_TAX_SLAB.Tax_Group order by Tax_Code desc ")
            If dtTax IsNot Nothing AndAlso dtTax.Rows.Count > 0 Then
                For ii As Integer = 0 To dtTax.Rows.Count - 1
                    If ii <> 0 Then
                        strTaxColumn += ","
                    End If
                    strTaxColumn += "[" + clsCommon.myCstr(dtTax.Rows(ii)("Tax_Code")).Trim() + "]"
                Next
            End If

            qry = "select *,Total_TDS_Amt as [Total TDS] from (select * from (select *,Taxable_Amt as [Taxable Amount] from  (select TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_MASTER.SEX as Gender,TSPL_EMPLOYEE_MASTER.Birth_date as DOB,TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Fiscal_Code as [Year],TSPL_Fiscal_Year_Master.Fiscal_Name as [Description]"
            qry += " ,TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.Applicable_Amt as Gross_Amt ,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Gross_Amt as [Gross Salary],TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.Type_Code"
            qry += " ,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Allowance_Amt+TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Section_Amt as Section_Amt,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Taxable_Amt,TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Total_TDS_Amt "
            qry += " from "
            qry += " TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD"
            qry += " left outer join TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL on TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.Code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code "
            qry += " left outer join TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP on TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Code"
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code"
            qry += " left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Fiscal_Code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Fiscal_Code "
            qry += " left outer join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.Type_Code "

            End If

            qry += " where 2=2 "
            qry += " and CONVERT(Date, TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Doc_Date,103)>=CONVERT(Date,'" & FromdateFilter & "',103) AND CONVERT(Date, TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Doc_Date,103)<=CONVERT(Date,'" & TodateFilter & "',103)"
            If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
                qry += " and  TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Fiscal_Code='" & txtFiscalYear.Value & "'"
            End If
            If txtEmployee.arrValueMember IsNot Nothing AndAlso txtEmployee.arrValueMember.Count > 0 Then
                qry += " and TSPL_EMPLOYEE_MASTER.emp_code in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ")  "
        End If
        If RbtnDetail.IsChecked = True Then
            qry += " and isnull(TSPL_HR_TDS_INCOME_TAX_CALCULATION_DETAIL.Type_Code,'')<>'' "
            qry += " ) as final "
            qry += " pivot (sum(final.Gross_Amt) for type_code in (" & strCodeColumn & "))as pvt "
            qry += " ) as xxx "
            qry += " left outer join (select max(TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.code) as taxcode,max(TAX1) as TAX,sum(TAX1_Amt) as TAX_Amt from TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX"
            qry += " group by Emp_Code, TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.code "
            qry += "    union all"
            qry += " select max(TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.code) as taxcode, max(TAX2) as TAX2,sum(TAX2_Amt) as TAX2_Amt from TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX"
            qry += " group by Emp_Code, TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.code "
            qry += " union all"
            qry += " select  max(TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.code) as taxcode,max(tax3) as tax3,sum(TAX3_Amt) as TAX3_Amt from TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX"
            qry += " group by Emp_Code, TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.code"
            qry += " )as TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX on TSPL_HR_TDS_INCOME_TAX_CALCULATION_TAX.taxcode=xxx.Code"
            qry += " )as finaldata"
            qry += " pivot (sum(finaldata.TAX_Amt) for TAX in (" & strTaxColumn & "))as pvt  "
        End If
        Try

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvReport.DataSource = dt
                gvReport.GroupDescriptors.Clear()
                gvReport.MasterTemplate.SummaryRowsBottom.Clear()
                gvReport.EnableGrouping = False
                gvReport.EnableFiltering = True
                gvReport.BestFitColumns()
                'RadPageView1.SelectedPage = RadPageViewPage2

                If RbtnDetail.IsChecked = True Then
                    gvReport.Columns("Section_Amt").IsVisible = False
                    gvReport.Columns("Taxable_Amt").IsVisible = False
                    gvReport.Columns("Total_TDS_Amt").IsVisible = False
                    gvReport.Columns("taxcode").IsVisible = False
                    gvReport.Columns("code").IsVisible = False
                End If
                FormatGrid()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If




        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtEmployee__My_Click(sender As Object, e As EventArgs) Handles txtEmployee._My_Click
        Try
            Dim qry As String = "select EMP_CODE,Emp_Name from TSPL_EMPLOYEE_MASTER"
            txtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "EmpF@TDSC", qry, "EMP_CODE", "", txtEmployee.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            'gvReport.Columns(ii).FormatString = "{0:n2}"
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If RbtnSummary.IsChecked = True Then

            gvReport.Columns("Row#").Width = 50
            gvReport.Columns("Row#").HeaderText = "S.No"

            gvReport.Columns("Emp_Code").Width = 50
            gvReport.Columns("Emp_Code").HeaderText = "Employee Code"

            gvReport.Columns("Emp_Name").Width = 100
            gvReport.Columns("Emp_Name").HeaderText = "Employee Name"

            gvReport.Columns("Gender").Width = 100
            gvReport.Columns("Gender").HeaderText = "Gender"

            gvReport.Columns("DOB").Width = 100
            gvReport.Columns("DOB").HeaderText = "DOB"

            gvReport.Columns("Year").Width = 80
            gvReport.Columns("Year").HeaderText = "Year"

            gvReport.Columns("Gross Salary").Width = 50
            gvReport.Columns("Gross Salary").HeaderText = "Gross Salary"

            gvReport.Columns("Section_Amt").Width = 100
            gvReport.Columns("Section_Amt").HeaderText = "Deductions"

            gvReport.Columns("Taxable_Amt").Width = 80
            gvReport.Columns("Taxable_Amt").HeaderText = "Taxable Salary"

            gvReport.Columns("Total_TDS_Amt").Width = 80
            gvReport.Columns("Total_TDS_Amt").HeaderText = "Total TDS"

            Dim item1 As New GridViewSummaryItem("Total_TDS_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            Dim item2 As New GridViewSummaryItem("Taxable_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            Dim item3 As New GridViewSummaryItem("Section_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)

        Else
            gvReport.Columns("Emp_Code").Width = 50
            gvReport.Columns("Emp_Code").HeaderText = "Employee Code"

            gvReport.Columns("Emp_Name").Width = 100
            gvReport.Columns("Emp_Name").HeaderText = "Employee Name"


        End If


        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gvReport.MasterTemplate.ShowTotals = True
        RadPageView1.SelectedPage = RadPageViewPage2
        gvReport.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

  


    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Export(EnumExportTo.Excel)
    End Sub

    Public Sub printdata(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add(strtemp)
         


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Employee TDS Register", gvReport, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Employee TDS Register ", gvReport, arrHeader, Me.Text, True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadMenuItem4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(ReportID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvReport.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            obj = Nothing
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmrptTDSLedger & "'"))

          

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
                transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
                'transportSql.exportdata(gvReport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Employee TDS Register", gvReport, arrHeader, "Employee TDS Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvReport.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub txtGLAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim Qry As String = "select Fiscal_Code, Fiscal_Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("ITSL@Fiscal", Qry, "fiscal_code", "", txtFiscalYear.Value, "fiscal_code", isButtonClicked)
        lblFiscalYear.Text = clsDBFuncationality.getSingleValue("select  Fiscal_Name from TSPL_Fiscal_Year_Master where Fiscal_Code ='" + txtFiscalYear.Value + "' ")
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        txtEmployee.arrValueMember = Nothing
        txtFiscalYear.Value = ""
        RadPageView1.SelectedPage = RadPageViewPage1
        lblFiscalYear.Text = ""
        RbtnSummary.IsChecked = True
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = dtpFromDate.Value
    End Sub
End Class
