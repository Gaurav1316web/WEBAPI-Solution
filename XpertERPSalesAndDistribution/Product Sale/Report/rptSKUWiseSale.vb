Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class rptSKUWiseSale
    Inherits FrmMainTranScreen

    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable

    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList
    '' new filters

    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim FORMTYPE As String = Nothing
    Dim qryList As ArrayList

    Dim fromdateValue As String = ""
    Dim TodateValue As String = ""
    Dim strToDateFiscalYear As Integer = 0

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.rptSKUWiseSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport

    End Sub
#End Region

    'Sub LoadTypes()
    '    dt = New DataTable
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Rows.Add("Document Detail")
    '    ddlReportType.DataSource = dt
    '    ddlReportType.ValueMember = "Code"
    '    ddlReportType.DisplayMember = "Code"
    'End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub


    Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim colMonthQtyValueWiseSummery As String = ""
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            strToDateFiscalYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1
            fromdateValue = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
            TodateValue = clsCommon.myCDate("31/03/" + clsCommon.myCstr(strToDateFiscalYear) + "", "dd/MM/yyyy")
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            colMonthQtyValueWiseSummery = "January,February,March,April,May,June,July,August,September,October,November,December"
            Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromdateValue, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(TodateValue, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("SKU WISE SALE", Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("SKU WISE SALE", Gv1, arrHeader, "SKU WISE SALE", True)
                Exit Sub
            End If

            clsCommon.ProgressBarShow()
            'ddlReportType.Enabled = False
            txtState.Enabled = False
            txtLocation.Enabled = False
            txtTransaction.Enabled = False
            txtItemGroup.Enabled = False
            txtItem.Enabled = False
            txtCustomer.Enabled = False
            txtCustGroup.Enabled = False
            txtUOM.Enabled = False
            cboFiscalYear.Enabled = False
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()


            Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strCodeColumn As String = ""
           Dim strOuterMaxDescColumn As String = ""
            Dim strIneerMaxDescColumn As String = ""

            Dim strRunQuery As String = ""

            Dim dtCategory As DataTable

            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strOuterMaxDescColumn += ","
                        strIneerMaxDescColumn += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"

                    strIneerMaxDescColumn += " max(RunningTotals.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strOuterMaxDescColumn += " max(kkkk.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                Next
            End If



            

                Dim strMain As String = ClsUDLSalesQuery.ReturnQueryWithCSASalePatti(obj, FORMTYPE)(0)
                RadPageViewPage2.Text = "SKU WISE SALE"
            'strRunQuery = " ;with RunningTotals as ( " + strMain + ") " & _
            '              " select case when kkkk.[P TYPE] is null then 'Grand Total' else kkkk.[P TYPE] end as [Product Type],case when kkkk.[SUB BRAND] is null and kkkk.[P TYPE] is not null then 'Total' else kkkk.[SUB BRAND] end as [Product Sub Brand] ,case when kkkk.[P TYPE] is not null and kkkk.[SUB BRAND] is not null and kkkk.[PACK SIZE] is Null then 'Total' else kkkk.[PACK SIZE] end as [SKU], " & _
            '              " sum(isnull(kkkk.April,0)) as April ,sum(isnull(kkkk.May,0)) as May,sum(isnull(kkkk.June,0)) as June,sum(isnull(kkkk.July,0)) as July,sum(isnull(kkkk.August,0)) as August ,sum(isnull(kkkk.September,0)) as September,sum(isnull(kkkk.October,0)) as October ,sum(isnull(kkkk.November,0)) as November,sum(isnull(kkkk.December,0)) as December,sum (isnull(kkkk.January,0)) as January,sum (isnull(kkkk.February,0)) as February,sum(isnull(kkkk.March,0)) as March, " & _
            '              " (sum (isnull(kkkk.January,0)) +sum (isnull(kkkk.February,0)) +sum(isnull(kkkk.March,0)) +sum(isnull(kkkk.April,0)) +sum(isnull(kkkk.May,0))+sum(isnull(kkkk.June,0)) +sum(isnull(kkkk.July,0)) +sum(isnull(kkkk.August,0)) +sum(isnull(kkkk.September,0)) +sum(isnull(kkkk.October,0)) +sum(isnull(kkkk.November,0)) +sum(isnull(kkkk.December,0))) as Total  from ( " & _
            '              " select * from ( " & _
            '              " select RunningTotals.[P TYPE] ,RunningTotals.[SUB BRAND],RunningTotals.[PACK SIZE],sum(Quantity ) as Quantity,datename(month,convert(date, RunningTotals.Document_date,103)) as [Document_Date] from RunningTotals group by RunningTotals.[P TYPE], RunningTotals.[SUB BRAND], RunningTotals.[PACK SIZE] , datename(month,convert(date, RunningTotals.Document_date,103)) having RunningTotals.[P TYPE] is not null  ) pppp pivot (sum(Quantity)   for [Document_Date] in ([January], [February],[March],[April],[May],[June],[July],[August],[September],[October],[November],[December],[Total]) ) piv ) kkkk group by kkkk.[P TYPE], kkkk.[SUB BRAND],kkkk.[PACK SIZE]  " 'WITH ROLLUP

            '==========update by preeti Gupta Against ticket no[ERO/23/04/19-000569]
            RadPageViewPage2.Text = "SKU WISE SALE"
            strRunQuery = " ;with RunningTotals as ( " + strMain + ") " & _
                          " select " + strOuterMaxDescColumn + ", " & _
                          " sum(isnull(kkkk.April,0)) as April ,sum(isnull(kkkk.May,0)) as May,sum(isnull(kkkk.June,0)) as June,sum(isnull(kkkk.July,0)) as July,sum(isnull(kkkk.August,0)) as August ,sum(isnull(kkkk.September,0)) as September,sum(isnull(kkkk.October,0)) as October ,sum(isnull(kkkk.November,0)) as November,sum(isnull(kkkk.December,0)) as December,sum (isnull(kkkk.January,0)) as January,sum (isnull(kkkk.February,0)) as February,sum(isnull(kkkk.March,0)) as March, " & _
                          " (sum (isnull(kkkk.January,0)) +sum (isnull(kkkk.February,0)) +sum(isnull(kkkk.March,0)) +sum(isnull(kkkk.April,0)) +sum(isnull(kkkk.May,0))+sum(isnull(kkkk.June,0)) +sum(isnull(kkkk.July,0)) +sum(isnull(kkkk.August,0)) +sum(isnull(kkkk.September,0)) +sum(isnull(kkkk.October,0)) +sum(isnull(kkkk.November,0)) +sum(isnull(kkkk.December,0))) as Total  from ( " & _
                          " select * from ( " & _
                          " select " + strIneerMaxDescColumn + "," + strCodeColumn + ",sum(Quantity ) as Quantity,datename(month,convert(date, RunningTotals.Document_date,103)) as [Document_Date] from RunningTotals group by " + strCodeColumn + " , datename(month,convert(date, RunningTotals.Document_date,103))   ) pppp pivot (sum(Quantity)   for [Document_Date] in ([January], [February],[March],[April],[May],[June],[July],[August],[September],[October],[November],[December],[Total]) ) piv ) kkkk group by " + strCodeColumn + "  " 'WITH ROLLUP

                dt = clsDBFuncationality.GetDataTable(strRunQuery)
                Gv1.DataSource = Nothing
                Gv1.Columns.Clear()
                Gv1.Rows.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.EnableFiltering = True

                Gv1.Tag = "SKU WISE SALE"


                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    Gv1.DataSource = dt
                'SetGridFormationOFGV1()
                'Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Product Type] AS [Product Type] format ""{0}: {1}"" Group By [Product Type]"))
                'Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Product Sub Brand] AS [Product Sub Brand] format ""{0}: {1}"" Group By [Product Sub Brand]"))
                    ' Gv1.GroupDescriptors.Add(New GridGroupByExpression("[SKU] AS [SKU] format ""{0}: {1}"" Group By [SKU]"))
                    'Gv1.BestFitColumns()
                    'For Each row As GridViewRowInfo In Gv1.Rows
                    'If row.Cells(0).Value = "Grand Total" Then
                    '    For Each cell As GridViewCellInfo In row.Cells
                    '        cell.Style.Font = New Font("Arial", 9, FontStyle.Bold)
                    '    Next
                    'End If
                    'If row.Cells(1).Value = "Total" Then
                    '    For Each cell As GridViewCellInfo In row.Cells
                    '        cell.Style.Font = New Font("Arial", 9, FontStyle.Bold)
                    '    Next
                    'End If
                    'If row.Cells(2).Value = "Total" Then
                    '    For Each cell As GridViewCellInfo In row.Cells
                    '        cell.Style.Font = New Font("Arial", 9, FontStyle.Bold)
                    '    Next
                    'End If
                    'Next
                End If
                'FindAndRestoreGridLayout(Me)
                ReStoreGridLayout()
                Gv1.AutoExpandGroups = True
                Gv1.ShowGroupPanel = False
                Gv1.ShowRowHeaderColumn = False
                Gv1.MasterTemplate.AllowAddNewRow = False
                FooterSummery(colMonthQtyValueWiseSummery)

            'Gv1.Columns("Product Type").IsPinned = True
            'Gv1.Columns("Product Sub Brand").IsPinned = True
            'Gv1.Columns("SKU").IsPinned = True
                Gv1.Columns("Total").IsPinned = True
                Gv1.Columns("Total").PinPosition = PinnedColumnPosition.Right
            RadPageView1.SelectedPage = RadPageViewPage2
            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)

        Finally
            clsCommon.ProgressBarHide()
        End Try


    End Sub

    Public Sub FooterSummery(ByVal strColumnName As String)
        strColumnName = strColumnName + ",Total"
        Dim words As String() = strColumnName.Split(New Char() {","c})
        If Gv1.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim word As String
            For Each word In words
                Dim item1 As New GridViewSummaryItem(word, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            'Dim item2 As New GridViewSummaryItem("Product Type", "", "Total")
            'summaryRowItem.Add(item2)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
        Gv1.MasterTemplate.ShowTotals = True
        'Gv1.MasterTemplate.ShowParentGroupSummaries = True
    End Sub
    'Public Sub FooterGrouppingSummery()
    '    Dim strColumnName As String = "Product Type,Product Sub Brand,SKU"
    '    Dim words As String() = strColumnName.Split(New Char() {","c})
    '    If Gv1.Rows.Count > 0 Then
    '        Dim summaryRowItem As New GridViewSummaryRowItem()
    '        Dim word As String
    '        For Each word In words
    '            Dim item1 As New GridViewSummaryItem(word, "", GridAggregateFunction.Max)
    '            summaryRowItem.Add(item1)
    '        Next
    '        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    '    End If
    'End Sub

    Function ReturnDataReader() As SqlClient.SqlDataReader
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
        Dim rd As SqlClient.SqlDataReader = clsPSInvoiceHead.GetReportDataReaderWithCSASalePatti(obj, clsUserMgtCode.rptSKUWiseSale)
        Return rd
    End Function

    Function ReturnFilterData() As clsSaleRegisterParameterType
        strPivotForFinalOuterQuery = ClsUDLSalesQuery.GetPivotForFinalOuterQry()
        Dim obj As New clsSaleRegisterParameterType
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            obj.Item_Code_List = txtItem.arrValueMember

        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            obj.Trans_Type_List = txtTransaction.arrValueMember
            If obj.Trans_Type_List.Contains("CSA Sale") Then
                obj.Trans_Type_List.Remove("CSA Sale")
            End If
            txtTransaction.arrValueMember = obj.Trans_Type_List
        Else
            Dim qry As String
            qry = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
            Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrTrans As New ArrayList
            For Each dr As DataRow In dtTrans.Rows
                If clsCommon.CompairString(clsCommon.myCstr(dr.Item("Name")), "CSA Sale") <> CompairStringResult.Equal Then
                    arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
                End If
            Next
            obj.Trans_Type_List = arrTrans
        End If
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            obj.State_List = txtState.arrValueMember

        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            obj.Location_Code_List = txtLocation.arrValueMember

        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            obj.Customer_Code_List = txtCustomer.arrValueMember

        End If
        If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
            obj.Item_Group_List = txtItemGroup.arrValueMember

        End If

        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            obj.Cust_Group_Code_List = txtCustGroup.arrValueMember

        End If
        If clsCommon.myLen(Document_No) > 0 Then
            obj.Document_Code = Document_No
        End If
        Dim Other_Cond As String = ""
        Dim strWhrCatg As String = ""
        strWhrCatg = ""
        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    Else
                        strWhrCatg += " 2=2  "
                    End If
                    strWhrCatg += ")"
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one category")
            End If
            Other_Cond += " and (" + strWhrCatg + ")"
        End If
        If btnPosted.IsChecked Then
            Other_Cond += " and xx.Status=1  "
        ElseIf btnUnposted.IsChecked Then
            Other_Cond += " and xx.Status=0  "
        End If
        obj.Other_Cond = Other_Cond
        obj.Unit_Code = txtUOM.Value
        obj.ReportType = "SKU WISE SALE"
        obj.From_Date = fromdateValue
        obj.To_Date = TodateValue

        Return obj
    End Function


    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).FormatString = "{0:n2}"
        Next


        Gv1.Columns("Product Type").IsVisible = True
        Gv1.Columns("Product Type").Width = 150
        Gv1.Columns("Product Type").HeaderText = "Product Type"

        Gv1.Columns("Product Sub Brand").IsVisible = True
        Gv1.Columns("Product Sub Brand").Width = 150
        Gv1.Columns("Product Sub Brand").HeaderText = "Product Sub Brand"

        Gv1.Columns("SKU").IsVisible = True
        Gv1.Columns("SKU").Width = 100
        Gv1.Columns("SKU").HeaderText = "SKU"

        Gv1.Columns("SKU").IsVisible = True
        Gv1.Columns("SKU").Width = 100
        Gv1.Columns("SKU").HeaderText = "SKU"

        Gv1.Columns("April").IsVisible = True
        Gv1.Columns("April").Width = 100
        Gv1.Columns("April").HeaderText = "April"

        Gv1.Columns("May").IsVisible = True
        Gv1.Columns("May").Width = 100
        Gv1.Columns("May").HeaderText = "May"

        Gv1.Columns("June").IsVisible = True
        Gv1.Columns("June").Width = 100
        Gv1.Columns("June").HeaderText = "June"

        Gv1.Columns("July").IsVisible = True
        Gv1.Columns("July").Width = 100
        Gv1.Columns("July").HeaderText = "July"

        Gv1.Columns("August").IsVisible = True
        Gv1.Columns("August").Width = 100
        Gv1.Columns("August").HeaderText = "August"

        Gv1.Columns("September").IsVisible = True
        Gv1.Columns("September").Width = 100
        Gv1.Columns("September").HeaderText = "September"

        Gv1.Columns("October").IsVisible = True
        Gv1.Columns("October").Width = 100
        Gv1.Columns("October").HeaderText = "October"

        Gv1.Columns("November").IsVisible = True
        Gv1.Columns("November").Width = 100
        Gv1.Columns("November").HeaderText = "November"

        Gv1.Columns("December").IsVisible = True
        Gv1.Columns("December").Width = 100
        Gv1.Columns("December").HeaderText = "December"

        Gv1.Columns("January").IsVisible = True
        Gv1.Columns("January").Width = 100
        Gv1.Columns("January").HeaderText = "January"

        Gv1.Columns("February").IsVisible = True
        Gv1.Columns("February").Width = 100
        Gv1.Columns("February").HeaderText = "February"

        Gv1.Columns("March").IsVisible = True
        Gv1.Columns("March").Width = 100
        Gv1.Columns("March").HeaderText = "March"

        Gv1.Columns("Total").IsVisible = True
        Gv1.Columns("Total").Width = 120
        Gv1.Columns("Total").HeaderText = "Total"

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        ' Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        Document_No = ""
        Document_No_Old = ""
        TodateValue = clsCommon.GETSERVERDATE()
        fromdateValue = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtUOM.Value = ""
        ' LoadTypes()
        ' ddlReportType.SelectedValue = "SKU WISE SALE"
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItemGroup.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        rbtnCategoryAll.IsChecked = True

        'ddlReportType.Enabled = True
        txtState.Enabled = True
        txtLocation.Enabled = True
        txtItemGroup.Enabled = True
        txtItem.Enabled = True
        txtCustomer.Enabled = True
        txtCustGroup.Enabled = True
        txtUOM.Enabled = True
        cboFiscalYear.Enabled = True

        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            txtTransaction.Enabled = False
        Else
            txtTransaction.arrValueMember = Nothing
            txtTransaction.Enabled = True
        End If

        'ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = "SKU WISE SALE"
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub rmSetting_Click(sender As Object, e As EventArgs)
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub RptSaleRegisterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub RptSaleRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        FillFicalYear()
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromdateValue = dtFrom
            TodateValue = dtTo
            txtUOM.Value = Unit_Code

            txtLocation.arrValueMember = arrLocation
            txtItem.arrValueMember = arrItem
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup

            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
            'ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            Dim arr As New ArrayList
            arr.Add("Fresh Sale")
            arr.Add("Fresh Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        End If
    End Sub

    Private Sub chkAllType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbgType.Enabled = chkselecttype.IsChecked
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            stateCond = " and state in  (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where location_type IN ('Physical','Virtual') " & stateCond & " "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmPendingRequisitionQty As New FrmPendingRequisitionQty()
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim Str As String = String.Empty
        Dim qry As String = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) <> CompairStringResult.Equal Then
            txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
        End If
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select Value as [Code],Description as Name from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Code", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
    End Sub

  
    Private Sub txtState__My_Click(sender As Object, e As EventArgs) Handles txtState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtState.arrValueMember, txtState.arrDispalyMember)
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        'If (Gv1.Rows.Count <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("No Data To Export")
        '    Exit Sub
        'End If
        'Print(Exporter.Excel)


        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromdateValue, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(TodateValue, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSKUWiseSale & "'"))
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            Dim Export As New ExportToExcelML(Gv1)
            Export.RunExport(filePath)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub FillFicalYear()
        Try
            Dim qry As String = "select convert (varchar ,min( datepart(year,Document_Date)) -1) +' - '+ convert (varchar ,min( datepart(year,Document_Date))-1 +1 ) as FiscalYear , convert (varchar ,min( datepart(year,Document_Date)) -1) as Year from  TSPL_SD_SALE_INVOICE_HEAD  union all  select distinct convert (varchar, datepart(year,Document_Date) ) +' - '+  convert (varchar, datepart(year,Document_Date) +1 ) as FiscalYear , convert (varchar, datepart(year,Document_Date) ) as Year  from TSPL_SD_SALE_INVOICE_HEAD "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                cboFiscalYear.DataSource = Nothing
                cboFiscalYear.DataSource = dt
                cboFiscalYear.ValueMember = "Year"
                cboFiscalYear.DisplayMember = "FiscalYear"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub Gv1_GroupSummaryEvaluate(sender As Object, e As GroupSummaryEvaluationEventArgs) Handles Gv1.GroupSummaryEvaluate
        'If e.SummaryItem.Name = "Product Sub Brand" Then
        '    e.FormatString = e.Group.Header
        'End If
        'If TypeOf e.Context Is GridViewGroupRowInfo Then
        '    e.FormatString = "{0}"
        'ElseIf TypeOf e.Context Is GridViewSummaryRowInfo Then
        '    e.FormatString = "Total:{0}"
        'End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If clsCommon.myLen(cboFiscalYear.Text) > 0 Then
                arrHeader.Add("Fiscal Year : " + cboFiscalYear.Text)
            End If

            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If
            If txtState.arrDispalyMember IsNot Nothing AndAlso txtState.arrDispalyMember.Count > 0 Then
                arrHeader.Add("State : " + clsCommon.GetMulcallStringWithComma(txtState.arrDispalyMember))
            End If

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtItemGroup.arrDispalyMember IsNot Nothing AndAlso txtItemGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Group : " + clsCommon.GetMulcallStringWithComma(txtItemGroup.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If

            If txtCustGroup.arrDispalyMember IsNot Nothing AndAlso txtCustGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

   
End Class
