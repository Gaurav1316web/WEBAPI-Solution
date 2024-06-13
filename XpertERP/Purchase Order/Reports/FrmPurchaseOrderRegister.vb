Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
' Update BY abhishek as on 29 oct 2012 5:15 pm For Excel
' by vipin for pdf work on 31/01/2013
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
''--preeti gupta--ticket no.[BM00000003142]

Public Class FrmPurchaseOrderRegister
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim btnReferesh As Boolean = False
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dtCategory As DataTable
    Public arrCat As Dictionary(Of String, Object) = Nothing

    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrStructure As ArrayList
    
    '' new filters

    Private Sub SetUserMgmtNew()
        ' MyBase.SetUserMgmt(clsUserMgtCode.FrmPurchaseOrderRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isModifyFlag
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub FrmPurchaseOrderRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData(EnumExportTo.Excel)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If

    End Sub
    
    Private Sub FrmPurchaseOrderRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        rbtnSummary.IsChecked = True
        chkPoInvoiceAll.IsChecked = True
        chkVendorAll.IsChecked = True
        cboMonthlyYearly.Text = "None"
        cboMonthName.Enabled = False
        FillFicalYear()
        LoadTypes()
        LoadMonthlyReportType()
        ItemLoad()
        LoadCategory()
        LoadPOType()
        LoadLocation()
        LoadPo()
        LoadVendor()
        'rdbtnFinishedGood.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        'If objCommonVar.IsDemoERP Then
        '    grpItemType.Visible = False
        '    rdbtnOther.IsChecked = True
        'End If
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

        If isDataLoad Then
            dtpFromdate1.Value = dtFrom
            dtpToDate.Value = dtTo
            txtStructure.arrValueMember = arrStructure
            If clsCommon.CompairString(strType, "Detail") = CompairStringResult.Equal Then
                rbtnDetail.IsChecked = True
            End If
            PrintData(Nothing)
            Me.Visible = True
        End If

    End Sub
    'Public Sub LoadCategory()
    '    Dim qry As String = "select Code,Name,Parent from ("
    '    qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
    '    qry += " union all"
    '    qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
    '    qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
    '    qry += " Union all"
    '    qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
    '    qry += " )xxx order by Sno"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    tvCategory.DataSource = Nothing
    '    tvCategory.TreeViewElement.AutoSizeItems = True
    '    tvCategory.ShowLines = True
    '    tvCategory.ShowRootLines = True
    '    tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
    '    tvCategory.ShowExpandCollapse = True
    '    tvCategory.TreeIndent = 15
    '    tvCategory.FullRowSelect = False
    '    tvCategory.ShowLines = True
    '    tvCategory.LineStyle = TreeLineStyle.Dot
    '    tvCategory.LineColor = Color.FromArgb(110, 153, 210)
    '    tvCategory.ExpandAnimation = ExpandAnimation.Opacity
    '    tvCategory.AllowEdit = False
    '    tvCategory.ShowRootLines = False
    '    tvCategory.TreeViewElement.AllowAlternatingRowColor = True
    '    tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

    '    tvCategory.TreeViewElement.DrawBorder = True
    '    tvCategory.ValueMember = "Code"
    '    tvCategory.DisplayMember = "Name"
    '    tvCategory.ChildMember = "Code"
    '    tvCategory.ParentMember = "Parent"
    '    tvCategory.DataSource = dt
    '    tvCategory.CheckBoxes = True

    '    tvCategory.ExpandAll()
    'End Sub


    'Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '    Me.Close()
    'End Sub



    ''Private Sub FrmPurchaseOrderRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    ''    If e.Alt And e.KeyCode = Keys.P Then
    ''        PrintData()
    ''    ElseIf e.Alt And e.KeyCode = Keys.C Then
    ''        Close()
    ''    End If

    ''End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True

        chkPoInvoiceAll.IsChecked = True
        chkVendorAll.IsChecked = True
        cboMonthlyYearly.Text = "None"
        cboMonthName.Enabled = False
        FillFicalYear()
        ItemLoad()

        LoadCategory()
        LoadLocation()
        LoadPo()
        LoadVendor()
        gv.Columns.Clear()

        RadPageView1.SelectedPage = RadPageViewPage1      'rdbtnFinishedGood.IsChecked = True
    End Sub

    Private Sub chkPoInvoiceAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPoInvoiceAll.ToggleStateChanged
        cbgPoInvoice.Enabled = Not chkPoInvoiceAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnReferesh = False
        PageSetupReport_ID = MyBase.Form_ID
        PrintData(Nothing)
    End Sub

    Public Sub FooterSummery(ByVal strColumnName As String)
        strColumnName = strColumnName + ",Total"
        Dim words As String() = strColumnName.Split(New Char() {","c})
        If gv.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim word As String
            For Each word In words
                Dim item1 As New GridViewSummaryItem(word, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
    End Sub

    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub LoadPOType()
        cboPOType.DataSource = clsPurchaseOrderHead.LoadPurchaseType()
        cboPOType.ValueMember = "Code"
        cboPOType.DisplayMember = "Name"
    End Sub
    Public Sub FillFicalYear()
        'Dim qry As String = " select min( datepart(year,PurchaseOrder_Date)) -1 as FiscalYear from TSPL_PURCHASE_ORDER_HEAD Union all  select distinct datepart(year,PurchaseOrder_Date) as FiscalYear from TSPL_PURCHASE_ORDER_HEAD "
        Dim qry As String = "select convert (varchar ,min( datepart(year,PurchaseOrder_Date)) -1) +' - '+ convert (varchar ,min( datepart(year,PurchaseOrder_Date))-1 +1 ) as FiscalYear , convert (varchar ,min( datepart(year,PurchaseOrder_Date)) -1) as Year from  TSPL_PURCHASE_ORDER_HEAD  union all  select distinct convert (varchar, datepart(year,PurchaseOrder_Date) ) +' - '+  convert (varchar, datepart(year,PurchaseOrder_Date) +1 ) as FiscalYear , convert (varchar, datepart(year,PurchaseOrder_Date) ) as Year  from TSPL_PURCHASE_ORDER_HEAD"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            cboFiscalYear.DataSource = Nothing
            cboFiscalYear.DataSource = dt
            If cboMonthlyYearly.Text = "Daily" Then
                cboFiscalYear.ValueMember = "Year"
                cboFiscalYear.DisplayMember = "Year"
            Else
                cboFiscalYear.ValueMember = "Year"
                cboFiscalYear.DisplayMember = "FiscalYear"
            End If
            
        End If
       
    End Sub
    Sub PrintData(ByVal exporter As EnumExportTo)
        '****changes in print and design add new category and sub-category tree linking************'
        Try
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim qry As String = ""
            Dim whrcate As String = ""
            Dim fromdate As String = ""
            Dim Todate As String = ""
            Dim colDate As String = ""
            Dim colDateWithNull As String = ""
            Dim colDateWithNullSum As String = ""
            Dim colMonth As String = ""
            Dim colMonthWithNull As String = ""
            Dim colMonthWithNullSum As String = ""
            Dim colDatforSummery As String = ""
            Dim colMonthforSummery As String = ""
            Dim colMonthQtyValueWiseSummery As String = ""
            Dim colCategoryWithMax As String = ""
            Dim colCategoryWithtttt As String = ""
            Dim colCategoryName As String = ""
            Dim colCategoryWithpppp As String = ""
            Dim postatussummary As String = Nothing
            postatussummary = "(SELECT case when isnull(max(PO.close_yn),'N')='Y' then 0 when isnull(max(TSPL_PURCHASE_ORDER_DETAIL.itemstatus),'0')='1' then 0 WHEN ((sum(ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0))+sum(ISNULL(TSPL_GRN_DETAIL.Tolerence_Qty,0)))-sum(ISNULL(TSPL_GRN_DETAIL.GRN_Qty,0)) +sum(ISNULL(TSPL_SRN_DETAIL.Short_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Leak_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Burst_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Rejected_Qty,0)))>0 THEN 2 else 1 end as status FROM TSPL_PURCHASE_ORDER_HEAD PO " & _
                        " LEFT OUTER JOIN (select PurchaseOrder_No,sum(PurchaseOrder_Qty) as PurchaseOrder_Qty,Item_Code,max(status) as itemstatus  from TSPL_PURCHASE_ORDER_DETAIL group by PurchaseOrder_No,Item_Code)TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=PO.PurchaseOrder_No " & _
                        " LEFT OUTER JOIN TSPL_GRN_DETAIL ON TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
                        " LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
                        " where PO.PurchaseOrder_No=PO_Head .PurchaseOrder_No and  TSPL_PURCHASE_ORDER_DETAIL.Item_Code=PO_Detail.Item_Code group  BY PO.PurchaseOrder_No) as postatus"


            Dim postatusdetail As String = Nothing
            postatusdetail = "(SELECT case when isnull(max(PO.close_yn),'N')='Y' then 'Close' when isnull(max(TSPL_PURCHASE_ORDER_DETAIL.itemstatus),'0')='1' then 'Close' WHEN ((sum(ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0))+sum(ISNULL(TSPL_GRN_DETAIL.Tolerence_Qty,0)))-sum(ISNULL(TSPL_GRN_DETAIL.GRN_Qty,0)) +sum(ISNULL(TSPL_SRN_DETAIL.Short_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Leak_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Burst_Qty,0))+sum(ISNULL(TSPL_SRN_DETAIL.Rejected_Qty,0)))>0 THEN 'Open' else 'Complete' end as status FROM TSPL_PURCHASE_ORDER_HEAD PO " & _
                        " LEFT OUTER JOIN (select PurchaseOrder_No,sum(PurchaseOrder_Qty) as PurchaseOrder_Qty,Item_Code,max(status) as itemstatus  from TSPL_PURCHASE_ORDER_DETAIL group by PurchaseOrder_No,Item_Code)TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=PO.PurchaseOrder_No " & _
                        " LEFT OUTER JOIN TSPL_GRN_DETAIL ON TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
                        " LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_DETAIL.GRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_SRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
                        " where PO.PurchaseOrder_No=PO_Head .PurchaseOrder_No and  TSPL_PURCHASE_ORDER_DETAIL.Item_Code=PO_Detail.Item_Code group  BY PO.PurchaseOrder_No) as postatus"


            If cboMonthlyYearly.Text = "None" Then
                fromdate = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
                Todate = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Else
                Dim strToDateFiscalYear As Integer = 0
                strToDateFiscalYear = clsCommon.myCdbl(cboFiscalYear.SelectedValue) + 1


                fromdate = clsCommon.myCDate("01/04/" + cboFiscalYear.SelectedValue + "", "dd/MM/yyyy")
                Todate = clsCommon.myCDate("31/03/" + clsCommon.myCstr(strToDateFiscalYear) + "", "dd/MM/yyyy")
                colMonth = "[January], [February],[March],[April],[May],[June],[July],[August],[September],[October],[November],[December]"
                colMonthWithNull = "  isnull(April,0) as April , isnull(May,0) as May,isnull(June,0) as June ,isnull (July,0) as July,isnull (August,0) as August,isnull (September,0) as September ,isnull(October,0) as October ,isnull( November,0) as November ,isnull (December,0) as December,isnull( January,0) as January,isnull(February,0) as February,isnull(March,0) as March "
                colMonthWithNullSum = " (tttt.January +tttt. February+tttt. March+ tttt.April + tttt.May+tttt.June +tttt.July+tttt.August+tttt.September +tttt.October +tttt.November +tttt.December )"
                colMonthforSummery = "January,February,March,April,May,June,July,August,September,October,November,December"
                colMonthQtyValueWiseSummery = "January_Value, February_Value,March_Value,April_Value,May_Value,June_Value,July_Value,August_Value,September_Value,October_Value,November_Value,December_Value,January_Qty,February_Qty,March_Qty,April_Qty,May_Qty,June_Qty,July_Qty,August_Qty,September_Qty,October_Qty,November_Qty,December_Qty,Total_Qty,Total_Value"
                colCategoryWithMax = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+ 'max(ppk.['+aa.ITEM_CATEGORY_CODE+']) as'+'[' +aa.ITEM_CATEGORY_CODE+']' from (select distinct ITEM_CATEGORY_CODE  FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by ITEM_CATEGORY_CODE FOR XML PATH ('')), 1, 1, '') ")
                colCategoryWithtttt = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+ 'tttt.['+aa.ITEM_CATEGORY_CODE+']'  from (select distinct ITEM_CATEGORY_CODE  FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by ITEM_CATEGORY_CODE FOR XML PATH ('')), 1, 1, '')")
                colCategoryName = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+ '['+aa.ITEM_CATEGORY_CODE+']'  from (select distinct ITEM_CATEGORY_CODE  FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by ITEM_CATEGORY_CODE FOR XML PATH ('')), 1, 1, '') ")
                colCategoryWithpppp = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+ 'pppp.['+aa.ITEM_CATEGORY_CODE+']'  from (select distinct ITEM_CATEGORY_CODE  FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by ITEM_CATEGORY_CODE FOR XML PATH ('')), 1, 1, '') ")
                If rbtnDetail.IsChecked = True Then
                    If cboMonthlyYearly.Text = "Daily" Then
                        Try
                            colDate = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + +'['+aa.purchaseorder_date+']' from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, PurchaseOrder_Date,103))  = '" + cboFiscalYear.SelectedValue + "')aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '')")
                            colDateWithNull = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +'isnull (' +'['+aa.purchaseorder_date+']' +',0 ) as ' +'['+aa.purchaseorder_date+']'  from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, PurchaseOrder_Date,103))  = '" + cboFiscalYear.SelectedValue + "')aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
                            colDateWithNullSum = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT '+' +'tttt.' +'['+aa.purchaseorder_date+']' from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, PurchaseOrder_Date,103))  = '" + cboFiscalYear.SelectedValue + "')aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
                            colDatforSummery = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+aa.purchaseorder_date from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, PurchaseOrder_Date,103))  = '" + cboFiscalYear.SelectedValue + "')aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '')")

                            'colDate = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + +'['+aa.purchaseorder_date+']' from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "'  and Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + fromdate + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + Todate + "',103))aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '')")
                            'colDateWithNull = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +'isnull (' +'['+aa.purchaseorder_date+']' +',0 ) as ' +'['+aa.purchaseorder_date+']'  from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "' and Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + fromdate + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + Todate + "',103))aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
                            'colDateWithNullSum = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT '+' +'tttt.' +'['+aa.purchaseorder_date+']' from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "' and Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + fromdate + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + Todate + "',103))aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
                            'colDatforSummery = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+aa.purchaseorder_date from (select distinct convert(varchar,PurchaseOrder_Date,103) PurchaseOrder_Date FROM TSPL_PURCHASE_ORDER_HEAD where datename(month,convert(date, PurchaseOrder_Date,103)) ='" + cboMonthName.Text + "' and Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + fromdate + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + Todate + "',103))aa order by convert(date,aa.PurchaseOrder_Date,103)  FOR XML PATH ('')), 1, 1, '')")
                        Catch ex As Exception
                            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                            Return
                        End Try
                    End If
                End If
                End If
                'Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
                'Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")

                Dim Po As ArrayList = cbgPoInvoice.CheckedValue
                Dim ItemArr As ArrayList = cbgItem.CheckedValue
                Dim locationArr As ArrayList = cbgLocation.CheckedValue
                Dim VendorArr As ArrayList = cbgVendor.CheckedValue

                Dim PoNo As String = ""
                Dim Vendor As String = ""
                Dim item As String = ""
                Dim location As String = ""
                'Dim category As String
                'Dim subcategory As String


                Dim StrPoNo As String = ""
                Dim StrVendor As String = ""
                Dim StrItem As String = ""
                Dim StrLocation As String = ""
                Dim StrCategory As String = ""
                Dim StrSubcategory As String = ""
                Dim type As String = ""
                If rdbtnFinishedGood.IsChecked = True Then
                    type = "Finished Goods"

                ElseIf rdbtnOther.IsChecked = True Then
                    type = "Others"
                End If




                If chkPoInvoiceSelect.IsChecked = True AndAlso cbgPoInvoice.CheckedValue.Count > 0 Then
                    PoNo = "'" + clsCommon.GetMulcallString(Po) + "'"
                    StrPoNo = PoNo.Replace("'", "")
                End If



                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
                    item = "'" + clsCommon.GetMulcallString(ItemArr) + "'"
                    StrItem = item.Replace("'", "")
                End If

                If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count > 0 Then
                    Vendor = "'" + clsCommon.GetMulcallString(VendorArr) + "'"
                    StrVendor = Vendor.Replace("'", "")
                End If

                Dim strCodeColumn As String = ""
                Dim strCodeColumnMax As String = ""
                Dim strCodeDescColumn As String = ""
                Dim strCodeDescColumnMax As String = ""

                Dim strCodeColumnSelect As String = ""
                Dim strCodeDescColumnSelect As String = ""

                Dim strCategoryPPPP As String = ""

                Dim strCategoryTable As String = ""
                If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtCategory.Rows.Count - 1
                        If ii <> 0 Then
                            strCodeColumn += ","
                            strCodeColumnMax += ","
                            strCodeDescColumn += ","
                            strCodeDescColumnMax += ","

                            strCodeColumnSelect += ","
                            strCategoryPPPP += ","
                            strCodeDescColumnSelect += ","
                        End If
                        strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                        strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                        strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                        strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"

                        strCodeColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                        strCategoryPPPP += "pppp.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                        strCodeDescColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    Next
                    strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
                    " select * from ( " + Environment.NewLine & _
                    " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
                    " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
                    " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
                    " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
                    " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
                    " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
                    " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
                    " where 2=2 " + Environment.NewLine & _
                    " )xx" + Environment.NewLine

                    If clsCommon.myLen(strCodeColumn) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016,Monika)
                        strCategoryTable += " Pivot " + Environment.NewLine & _
                   " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
                   " ) Pivt" + Environment.NewLine
                    End If

                    If clsCommon.myLen(strCodeDescColumn) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016,Monika)
                        strCategoryTable += " Pivot " + Environment.NewLine & _
                   " (" + Environment.NewLine & _
                   " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
                   " ) Pivt1 " + Environment.NewLine
                    End If

                    strCategoryTable += " ) xxx group by Item_Code "
                    ''End of Category Table start now.
                End If


                If rbtnDetail.IsChecked = True Then

                'If rdbtnFinishedGood.IsChecked = True Then
                qry = "SELECT distinct TSPL_REQUISITION_HEAD.Requisition_Id as [Indent No] ,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as [Indent Date],convert(varchar,PO_Head .PurchaseOrder_Date,103) as [PO Date],PO_Head .PurchaseOrder_No as [PONO]," + postatusdetail + ",stuff((select ',' + PJV_No from TSPL_PJV_HEAD  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No  =TSPL_PJV_HEAD.SRN_No  where TSPL_SRN_HEAD.Against_PO =PO_Head .PurchaseOrder_No for xml path('')),1,1,'') as PJVNo  " &
                        " ,PO_Head .Vendor_Code as Party, " &
                          " PO_Head .Vendor_Name as [Party Name],PO_Detail .Requisition_Id as Indent,PO_Detail .Item_Code as Item,PO_Detail .Item_Desc as [Name Of Item],TSPL_STRUCTURE_MASTER.Structure_Descq as [Structure Of Item]," &
                          "  convert(decimal(18,3),PO_Detail .PurchaseOrder_Qty) as Qty, Item_Cost as Rate,PO_Detail .Amount ,convert(decimal(18,2),PO_Detail .Disc_Per) as [Disc%],PO_Detail .Disc_Amt,(PO_Detail .Amount -Po_detail.Disc_Amt )as Value " &
                          " , (Case when tax1 .Type ='E'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='E'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='E'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='E'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='E'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='E'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='E'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='E'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='E'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='E'then PO_Detail.TAX10_Rate  else 0 end)as Excise_Rate "

                qry += " , (Case when tax1 .Type ='E'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='E'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='E'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='E'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='E'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='E'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='E'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='E'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='E'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='E'then PO_Detail.TAX10_Amt  else 0 end)as Excise_Amount"

                qry += ",(Case when tax1 .Type ='V'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='V'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='V'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='V'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='V'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='V'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='V'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='V'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='V'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='V'then PO_Detail.TAX10_Rate  else 0 end)as VAT_Rate"

                qry += ",(Case when tax1 .Type ='V'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='V'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='V'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='V'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='V'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='V'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='V'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='V'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='V'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='V'then PO_Detail.TAX10_Amt  else 0 end)as VAT_Amount"

                qry += ", (Case when tax1 .Type ='C'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='C'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='C'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='C'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='C'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='C'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='C'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='C'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='C'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='C'then PO_Detail.TAX10_Rate  else 0 end)as CST_Rate"

                qry += ", (Case when tax1 .Type ='C'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='C'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='C'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='C'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='C'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='C'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='C'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='C'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='C'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='C'then PO_Detail.TAX10_Amt  else 0 end)as CST_Amount"

                qry += ",  (Case when tax1 .Type ='IGST'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='IGST'then PO_Detail.TAX2_Rate else 0 end+Case when tax3 .Type ='IGST'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='IGST'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='IGST'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='IGST'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='IGST'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='IGST'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='IGST'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='IGST'then PO_Detail.TAX10_Rate  else 0 end)as IGST_Rate"

                qry += ",  (Case when tax1 .Type ='IGST'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='IGST'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='IGST'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='IGST'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='IGST'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='IGST'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='IGST'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='IGST'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='IGST'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='IGST'then PO_Detail.TAX10_Amt  else 0 end)as IGST_Amount"

                qry += ",  (Case when tax1 .Type ='CGST'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='CGST'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='CGST'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='CGST'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='CGST'then PO_Detail.TAX5_Rate   else 0 end+Case when tax6 .Type ='CGST'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='CGST'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='CGST'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='CGST'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='CGST'then PO_Detail.TAX10_Rate  else 0 end)as CGST_Rate"

                qry += ",  (Case when tax1 .Type ='CGST'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='CGST'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='CGST'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='CGST'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='CGST'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='CGST'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='CGST'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='CGST'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='CGST'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='CGST'then PO_Detail.TAX10_Amt  else 0 end)as CGST_Amount"

                qry += ",  (Case when tax1 .Type ='SGST'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='SGST'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='SGST'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='SGST'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='SGST'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='SGST'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='SGST'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='SGST'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='SGST'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='SGST'then PO_Detail.TAX10_Rate  else 0 end)as SGST_Rate"

                qry += ",  (Case when tax1 .Type ='SGST'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='SGST'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='SGST'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='SGST'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='SGST'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='SGST'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='SGST'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='SGST'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='SGST'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='SGST'then PO_Detail.TAX10_Amt  else 0 end)as SGST_Amount"

                qry += ",  (Case when tax1 .Type ='UGST'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='UGST'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='UGST'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='UGST'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='UGST'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='UGST'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='UGST'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='UGST'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='UGST'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='UGST'then PO_Detail.TAX10_Rate  else 0 end)as UGST_Rate"

                qry += ",  (Case when tax1 .Type ='UGST'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='UGST'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='UGST'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='UGST'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='UGST'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='UGST'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='UGST'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='UGST'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='UGST'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='UGST'then PO_Detail.TAX10_Amt  else 0 end)as UGST_Amount"

                qry += ",  (Case when tax1 .Type ='M'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='M'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='M'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='M'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='M'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='M'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='M'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='M'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='M'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='M'then PO_Detail.TAX10_Rate  else 0 end)as Mandi_Rate"

                qry += ",  (Case when tax1 .Type ='M'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='M'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='M'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='M'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='M'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='M'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='M'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='M'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='M'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='M'then PO_Detail.TAX10_Amt  else 0 end)as Mandi_Amount"

                qry += ",  (Case when tax1 .Type ='A'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='A'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='A'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='A'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='A'then PO_Detail.TAX5_Rate  else 0 end+Case when tax6 .Type ='A'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='A'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='A'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='A'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='A'then PO_Detail.TAX10_Rate  else 0 end)as ADDTAX_Rate"

                qry += ",  (Case when tax1 .Type ='A'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='A'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='A'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='A'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='A'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='A'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='A'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='A'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='A'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='A'then PO_Detail.TAX10_Amt  else 0 end)as ADDTAX_Amount"

                qry += ",(Case when tax1 .Type ='O'then PO_Detail.TAX1_Rate  else 0 end+Case when tax2.Type ='O'then PO_Detail.TAX2_Rate  else 0 end+Case when tax3 .Type ='O'then PO_Detail.TAX3_Rate  else 0 end+Case when tax4 .Type ='O'then PO_Detail.TAX4_Rate  else 0 end+Case when tax5 .Type ='O'then PO_Detail.TAX5_Rate else 0 end+Case when tax6 .Type ='O'then PO_Detail.TAX6_Rate  else 0 end+Case when tax7 .Type ='O'then PO_Detail.TAX7_Rate  else 0 end+Case when tax8 .Type ='O'then PO_Detail.TAX8_Rate  else 0 end+Case when tax9 .Type ='O'then PO_Detail.TAX9_Rate  else 0 end+Case when tax10 .Type ='O'then PO_Detail.TAX10_Rate  else 0 end)as OTHER_Rate"

                qry += ",(Case when tax1 .Type ='O'then PO_Detail.TAX1_Amt  else 0 end+Case when tax2.Type ='O'then PO_Detail.TAX2_Amt  else 0 end+Case when tax3 .Type ='O'then PO_Detail.TAX3_Amt  else 0 end+Case when tax4 .Type ='O'then PO_Detail.TAX4_Amt  else 0 end+Case when tax5 .Type ='O'then PO_Detail.TAX5_Amt  else 0 end+Case when tax6 .Type ='O'then PO_Detail.TAX6_Amt  else 0 end+Case when tax7 .Type ='O'then PO_Detail.TAX7_Amt  else 0 end+Case when tax8 .Type ='O'then PO_Detail.TAX8_Amt  else 0 end+Case when tax9 .Type ='O'then PO_Detail.TAX9_Amt  else 0 end+Case when tax10 .Type ='O'then PO_Detail.TAX10_Amt  else 0 end)as OTHER_Amount "

                qry += ",PO_Detail.Total_Tax_Amt as Tax_Amount"

                qry += ",PO_Detail.Item_Net_Amt as Net_Amount"

                qry += " ,PO_Head .Delivery_date as DlDate,(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =PO_Head .Bill_To_Location) as Location,PO_Detail.Unit_code,PO_Detail .Specification as Spaces"
                If clsCommon.myLen(strCategoryTable) > 0 Then
                        qry += "," + strCodeColumnSelect + "," + strCodeDescColumnSelect
                    End If
                qry += " from TSPL_PURCHASE_ORDER_DETAIL  as PO_Detail inner join TSPL_PURCHASE_ORDER_HEAD  as PO_Head On PO_Detail .PurchaseOrder_No =PO_Head  .PurchaseOrder_No  left outer join   TSPL_ITEM_MASTER on PO_Detail.Item_Code =TSPL_ITEM_MASTER .Item_Code left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.structure_code=tspl_item_master.Structure_Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =PO_Head .tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = PO_Head .tax2 left outer join tspl_tax_master as tax3 on tax3.Tax_Code=PO_Head  .TAX3 left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= PO_Head  .tax4 left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=PO_Head  .tax5 left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =PO_Head  .TAX6 left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =PO_Head  .TAX7 left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =PO_Head  .TAX8 left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =PO_Head  .TAX9 left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =PO_Head  .TAX10  left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id =PO_Head.Against_Requisition "
                If clsCommon.myLen(strCategoryTable) > 0 Then
                        qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=PO_Detail.Item_Code"
                    End If
                    qry += " where PO_Head .Status =1  "
                    If cboMonthlyYearly.Text <> "Daily" Then
                        qry += " and Convert(Date,PO_Head .PurchaseOrder_Date,103) >=convert(date,'" + fromdate + "',103) and Convert(Date,PO_Head .PurchaseOrder_Date,103) <=Convert(Date,'" + Todate + "',103) "
                    End If

                    If cboPOType.SelectedValue = "J" Then
                        qry += "and PurchaseOrder_Type ='J' "
                    ElseIf cboPOType.SelectedValue = "L" Then
                        qry += "and PurchaseOrder_Type ='L' "
                    ElseIf cboPOType.SelectedValue = "I" Then
                        qry += "and PurchaseOrder_Type ='I' "


                    End If

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
                        qry += " and (" + strWhrCatg + ")"
                    End If




                '====added by shivani
                If txtPoNo.arrValueMember IsNot Nothing AndAlso txtPoNo.arrValueMember.Count > 0 Then
                    qry += " and PO_Head .PurchaseOrder_No In (" + clsCommon.GetMulcallString(txtPoNo.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and PO_Head.Bill_To_Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and PO_Head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    qry += "  and PO_Head .Vendor_Code  in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += "  and PO_Detail .Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                    qry += "  and tspl_item_master.Structure_Code in (" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ") " + Environment.NewLine
                End If
                '==============
                If clsCommon.myLen(whrcate) > 0 Then
                        whrcate = " and " + whrcate
                    End If
                    If cboMonthlyYearly.Text = "None" Then
                        qry = "select axaa.* from (" + qry + ")axaa order by [PO Date]"
                    Else
                        qry = "select axaa.* from (" + qry + ")axaa "
                    End If


                ElseIf rbtnSummary.IsChecked = True Then
                qry = " select [Indent No],[Indent Date],[PO Date],[PONO],PJVNo,case when max(ll.postatus)=2 then 'Open' when max(ll.postatus)=1 then 'Complete' else 'Close' end as postatus,Party,max([Party Name]) as [Party Name],Bill_To_Location ,max(Location_Desc) as Location_Desc ,sum(Qty) as Qty,max(Amount_Less_Discount) as Amount_Less_Discount,max(Total_Tax_Amt) as Total_Tax_Amt,max(Total_Add_Charge ) as Total_Add_Charge,max(PO_Total_Amt) as PO_Total_Amt from(SELECT distinct TSPL_REQUISITION_HEAD.Requisition_Id as [Indent No] ,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as [Indent Date],convert(varchar,PO_Head .PurchaseOrder_Date,103) as [PO Date],PO_Head .PurchaseOrder_No as [PONO]," + postatussummary + ",stuff((select ',' + PJV_No from TSPL_PJV_HEAD  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No  =TSPL_PJV_HEAD.SRN_No  where TSPL_SRN_HEAD.Against_PO =PO_Head .PurchaseOrder_No for xml path('')),1,1,'') as PJVNo   ,PO_Head .Vendor_Code as Party,  PO_Head .Vendor_Name as [Party Name],PO_Head.Bill_To_Location ,Location_Desc ,PO_Detail .Requisition_Id as Indent, PO_Detail .PurchaseOrder_Qty as Qty,PO_Head .Amount_Less_Discount ,PO_Head.Total_Tax_Amt ,PO_Total_Amt ,PO_Detail.item_code,Total_Add_Charge   from TSPL_PURCHASE_ORDER_DETAIL  as PO_Detail inner join TSPL_PURCHASE_ORDER_HEAD  as PO_Head On PO_Detail .PurchaseOrder_No =PO_Head  .PurchaseOrder_No   left outer join   TSPL_ITEM_MASTER on PO_Detail.Item_Code =TSPL_ITEM_MASTER .Item_Code left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.structure_code=tspl_item_master.Structure_Code    left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id =PO_Head.Against_Requisition " &
                    " left join TSPL_LOCATION_MASTER on PO_Head.Bill_To_Location = TSPL_LOCATION_MASTER .Location_Code " &
                    " where PO_Head .Status =1   and Convert(Date,PO_Head .PurchaseOrder_Date,103) >=convert(date,'" + fromdate + "',103) and Convert(Date,PO_Head .PurchaseOrder_Date,103) <=Convert(Date,'" + Todate + "',103) "
                If cboPOType.SelectedValue = "J" Then
                    qry += "and PurchaseOrder_Type ='J' "
                ElseIf cboPOType.SelectedValue = "L" Then
                    qry += "and PurchaseOrder_Type ='L' "
                ElseIf cboPOType.SelectedValue = "I" Then
                    qry += "and PurchaseOrder_Type ='I' "
                End If
                If txtPoNo.arrValueMember IsNot Nothing AndAlso txtPoNo.arrValueMember.Count > 0 Then
                    qry += " and PO_Head .PurchaseOrder_No In (" + clsCommon.GetMulcallString(txtPoNo.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and PO_Head.Bill_To_Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        qry += " and PO_Head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                        qry += "  and PO_Head .Vendor_Code  in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
                    End If
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += "  and PO_Detail .Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                    qry += "  and tspl_item_master.Structure_Code in (" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ") " + Environment.NewLine
                End If
                qry += " )as ll group by [Indent No],[Indent Date],[PO Date],[PONO],PJVNo,Party,Bill_To_Location "
                End If

                If cboMonthlyYearly.Text = "Daily" Then
                qry = " select *, " + colDateWithNullSum + " as Total from ( select [Item Code],[Item Name]," + strCodeColumn + ",Unit_code as [Unit],Location as [Location], " + colDateWithNull + " from ( select pppp.Item as [Item Code] , pppp.[Name Of Item] as [Item Name], " + strCategoryPPPP + ",pppp.Unit_code, pppp.Location,pppp.Qty,pppp.[PO Date]  from ( " + qry + " ) pppp  ) strll Pivot ( sum(qty)   for [PO Date] in (" + colDate + ") ) piv ) tttt "
            End If

            If cboMonthlyYearly.Text = "Monthly" Then
                If cboQtyValueWise.Text = "Qty Wise" Then
                    qry = " select *, (" + colMonthWithNullSum + ")  as Total from ( select [Item Code],[Item Name]," + strCodeColumn + ",Unit_code as [Unit],Location as [Location] , " + colMonthWithNull + " from (select pppp.Item as [Item Code] , pppp.[Name Of Item] as [Item Name], " + strCategoryPPPP + ",pppp.Unit_code, pppp.Location,pppp.Qty, datename(month,convert(date, pppp.[PO Date],103)) as [PO Date] from ( " + qry + ") pppp ) strll Pivot ( sum(qty)   for [PO Date] in ( " + colMonth + ") ) piv ) tttt "
                ElseIf cboQtyValueWise.Text = "Value Wise" Then
                    qry = " select *, (" + colMonthWithNullSum + ")  as Total from ( select [Item Code],[Item Name]," + strCodeColumn + ",Unit_code as [Unit],Location as [Location] , " + colMonthWithNull + " from (select pppp.Item as [Item Code] , pppp.[Name Of Item] as [Item Name], " + strCategoryPPPP + ",pppp.Unit_code, pppp.Location,pppp.Amount, datename(month,convert(date, pppp.[PO Date],103)) as [PO Date] from ( " + qry + ") pppp ) strll Pivot ( sum(Amount)   for [PO Date] in ( " + colMonth + ") ) piv ) tttt "
                ElseIf cboQtyValueWise.Text = "Both" Then
                    qry = " select ppk.[Item Code], max(ppk.[Item Name]) as [Item Name] , " + colCategoryWithMax + " ,max(ppk.Unit) as Unit,max(ppk.Location) as Location, " &
                          " sum(isnull(ppk.[April_Qty],0)) as [April_Qty],sum(isnull(ppk.[May_Qty],0)) as [May_Qty],sum(isnull(ppk.[June_Qty],0)) as [June_Qty] ,sum(isnull(ppk.[July_Qty],0)) as [July_Qty] ,sum(isnull(ppk.[August_Qty],0)) as [August_Qty],sum(isnull(ppk.[September_Qty],0)) as [September_Qty] ,sum(isnull(ppk.[October_Qty],0)) as [October_Qty],sum(isnull (ppk.[November_Qty],0)) as [November_Qty], sum (isnull(ppk.[December_Qty],0)) as [December_Qty] ,sum(isnull(ppk.[January_Qty],0)) as [January_Qty] ,sum( isnull(ppk.[February_Qty],0)) as [February_Qty] ,sum(isnull(ppk.[March_Qty],0)) as [March_Qty], sum (isnull (ppk.Total_Qty,0)) as Total_Qty ," &
                          " sum(isnull(ppk.[April_Value],0)) as [April_Value] ,sum(isnull(ppk.[May_Value],0)) as [May_Value] ,sum(isnull(ppk.[June_Value],0)) as [June_Value] ,sum(isnull(ppk.[July_Value],0)) as [July_Value] ,sum(isnull(ppk.[August_Value],0)) as [August_Value],sum(isnull(ppk.[September_Value],0)) as [September_Value],sum(isnull(ppk.[October_Value],0)) as [October_Value] ,sum(isnull(ppk.[November_Value],0)) as [November_Value],sum(isnull(ppk.[December_Value],0)) as [December_Value] ,sum(isnull(ppk.[January_Value],0)) as [January_Value],sum( isnull(ppk.[February_Value],0)) as [February_Value],sum(isnull(ppk.[March_Value],0)) as [March_Value] , sum(isnull(ppk.Total_Value,0)) as Total_Value from ( " &
                          "  select tttt.[Item Code], tttt.[Item Name], " + colCategoryWithtttt + " , tttt.Unit,tttt.Location, " &
                          " [January_Value], [February_Value],[March_Value],[April_Value],[May_Value],[June_Value],[July_Value],[August_Value],[September_Value],[October_Value],[November_Value],[December_Value],( (tttt.January_Value +tttt. February_Value+tttt. March_Value+ tttt.April_Value + tttt.May_Value+tttt.June_Value +tttt.July_Value+tttt.August_Value+tttt.September_Value +tttt.October_Value +tttt.November_Value +tttt.December_Value ))  as Total_Value,  " &
                          " 0 as [January_Qty], 0 as [February_Qty], 0 as [March_Qty],0 as [April_Qty], 0 as [May_Qty],0 as [June_Qty],0 as [July_Qty],0 as [August_Qty],0 as [September_Qty],0 as [October_Qty], 0 as [November_Qty],0 as [December_Qty] , 0 as Total_Qty  " &
                          "  from ( select [Item Code],[Item Name], " + colCategoryName + ",Unit_code as [Unit],Location as [Location] , " &
                          " isnull( January_Value,0) as January_Value,isnull(February_Value,0) as February_Value,isnull(March_Value,0) as March_Value, isnull(April_Value,0) as April_Value , isnull(May_Value,0) as May_Value,isnull(June_Value,0) as June_Value ,isnull (July_Value,0) as July_Value,isnull (August_Value,0) as August_Value,isnull (September_Value,0) as September_Value ,isnull(October_Value,0) as October_Value ,isnull( November_Value,0) as November_Value ,isnull (December_Value,0) as December_Value " &
                          " from (select pppp.Item as [Item Code] , pppp.[Name Of Item] as [Item Name], " + colCategoryWithpppp + ",pppp.Unit_code, pppp.Location,pppp.Amount, datename(month,convert(date, pppp.[PO Date],103))+'_Value' as [PO Date] from ( " &
                          " " + qry + " " &
                          " ) pppp ) strll  Pivot ( sum(Amount)   for [PO Date] in ( [January_Value], [February_Value],[March_Value],[April_Value],[May_Value],[June_Value],[July_Value],[August_Value],[September_Value],[October_Value],[November_Value],[December_Value]) ) piv  ) tttt   " &
                          " union all " &
                          " select tttt.[Item Code], tttt.[Item Name]," + colCategoryWithtttt + " ,tttt.Unit,tttt.Location,0 as [January_Value],0 as  [February_Value],0 as [March_Value], 0 as [April_Value],0 as [May_Value],0 as [June_Value],0 as [July_Value],0 as [August_Value],0 as [September_Value],0 as [October_Value],0 as [November_Value],0 as [December_Value],0 as Total_Value, " &
                          " [January_Qty],  [February_Qty], [March_Qty], [April_Qty], [May_Qty],[June_Qty],[July_Qty],[August_Qty],[September_Qty],[October_Qty],[November_Qty],[December_Qty], ( (tttt.January_Qty +tttt. February_Qty+tttt. March_Qty+ tttt.April_Qty + tttt.May_Qty+tttt.June_Qty +tttt.July_Qty+tttt.August_Qty+tttt.September_Qty +tttt.October_Qty +tttt.November_Qty +tttt.December_Qty ))  as Total_Qty " &
                          " from ( select [Item Code],[Item Name]," + colCategoryName + " , Unit_code as [Unit],Location as [Location] , " &
                          " isnull( January_Qty,0) as January_Qty,isnull(February_Qty,0) as February_Qty,isnull(March_Qty,0) as March_Qty ,   isnull(April_Qty,0) as April_Qty , isnull(May_Qty,0) as May_Qty,isnull(June_Qty,0) as June_Qty ,isnull (July_Qty,0) as July_Qty,isnull (August_Qty,0) as August_Qty,isnull (September_Qty,0) as September_Qty ,isnull(October_Qty,0) as October_Qty ,isnull( November_Qty,0) as November_Qty ,isnull (December_Qty,0) as December_Qty " &
                          " from (select pppp.Item as [Item Code] ,pppp.[Name Of Item] as [Item Name], " + colCategoryWithpppp + " ,pppp.Unit_code, pppp.Location,pppp.Qty, datename(month,convert(date, pppp.[PO Date],103))+'_Qty' as [PO Date] from (  " &
                          " " + qry + " " &
                          " ) pppp ) strll Pivot ( sum(qty)   for [PO Date] in ( [January_Qty], [February_Qty],[March_Qty],[April_Qty],[May_Qty],[June_Qty],[July_Qty],[August_Qty],[September_Qty],[October_Qty],[November_Qty],[December_Qty]) ) piv ) tttt " &
                          " )ppk group by ppk.[Item Code] "
                End If

            End If

                Dim dtgv As New DataTable
                dtgv = clsDBFuncationality.GetDataTable(qry)
                If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dtgv
                    'For ii As Integer = 0 To gv.Columns.Count - 1
                    '    gv.Columns(ii).ReadOnly = True
                    '    gv1.Columns(ii).IsVisible = False
                    'Next
                    If cboMonthlyYearly.Text = "Daily" Then
                        FooterSummery(colDatforSummery)
                    End If
                If cboMonthlyYearly.Text = "Monthly" AndAlso cboQtyValueWise.Text <> "Both" Then
                    FooterSummery(colMonthforSummery)
                Else
                    FooterSummery(colMonthQtyValueWiseSummery)
                End If

                gv.BestFitColumns()

                End If

                If dtgv.Rows.Count <= 0 Then
                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                Exit Sub
                End If
                'If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                '    For Each dr As DataRow In dtCategory.Rows
                '        Dim strCol As String = clsCommon.myCstr(dr("CodeDescColumn"))
                '        gv.Columns(strCol).IsVisible = True
                '        gv.Columns(strCol).Width = 100
                '        gv.Columns(strCol).HeaderText = clsCommon.myCstr(dr("DescColumn"))
                '    Next
                'End If
                If cboMonthlyYearly.Text = "None" Then
                    SetGridFormationOFGV1()
                End If

            ReStoreGridLayout()
            If btnReferesh = False Then
                RadPageView1.SelectedPage = RadPageViewPage2
            Else

                'Dim str As String = "PurchaseOrderRegister Report"
                Dim arr As New List(Of String)()
                'arr.Add("PurchaseOrderRegister Report")
                arr.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmPurchaseOrderRegister & "'"))
                    If cboMonthlyYearly.Text = "None" Then
                        arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "Type:   " + type + "")
                    Else
                        arr.Add("  Fiscal Year:  " + cboFiscalYear.SelectedValue + "  Type: " + cboMonthlyYearly.Text + "")
                    End If

                    arr.Add("Company : " + objCommonVar.CurrentCompanyName)
                    'If StrLocation <> "" Then
                    '    arr.Add(" Location:   " + StrLocation + "")
                    'End If
                    If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                        arr.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                    End If
                    If StrItem <> "" Then
                        arr.Add(" Item:  " + StrItem + "")
                    End If
                    If StrVendor <> "" Then
                        arr.Add(" Vendor:  " + StrVendor + "")
                    End If
                    If StrPoNo <> "" Then
                        arr.Add(" PoNo:   " + StrPoNo + "")
                    End If
                    If StrCategory <> "" Then
                        arr.Add(" Category:  " + StrCategory + "")
                    End If
                    If StrSubcategory <> "" Then
                        arr.Add(" SubCategory:   " + StrSubcategory + "")
                    End If

                    '  clsCommon.MyExportToExcel(str, gv, arr, "PurchaseOrderRegister Report")
                    ' ExporttoMyExcel(qry, Me)

                '    If exporter = EnumExportTo.Excel Then
                '        clsCommon.MyExportToExcelGrid("Purchase Tracking Report", gv, arr, Me.Text)

                '    Else
                '        clsCommon.MyExportToPDF("Purchase Tracking Report", gv, arr, Me.Text, True)
                '    End If
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
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arr)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arr)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gv, arr, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If

            End If
            ' ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gv.SummaryRowsBottom.Clear()
        gv.TableElement.TableHeaderHeight = 42
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        If rbtnDetail.IsChecked = True Then
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For Each dr As DataRow In dtCategory.Rows
                    Dim strCol As String = clsCommon.myCstr(dr("CodeDescColumn"))
                    gv.Columns(strCol).IsVisible = True
                    gv.Columns(strCol).Width = 100
                    gv.Columns(strCol).HeaderText = clsCommon.myCstr(dr("DescColumn"))
                Next
            End If
        End If
        gv.Columns("Indent No").IsVisible = True
        gv.Columns("Indent No").Width = 100
        gv.Columns("Indent No").HeaderText = "Indent No"

        gv.Columns("Indent Date").IsVisible = True
        gv.Columns("Indent Date").Width = 100
        gv.Columns("Indent Date").HeaderText = "Indent Date"
        gv.Columns("Indent Date").FormatString = "{0:d}"

        gv.Columns("PO Date").IsVisible = True
        gv.Columns("PO Date").Width = 100
        gv.Columns("PO Date").HeaderText = "PO Date"
        gv.Columns("PO Date").FormatString = "{0:d}"

        gv.Columns("PONO").IsVisible = True
        gv.Columns("PONO").Width = 100
        gv.Columns("PONO").HeaderText = "PONO"

        gv.Columns("PJVNo").IsVisible = True
        gv.Columns("PJVNo").Width = 100
        gv.Columns("PJVNo").HeaderText = "PJVNo"

        gv.Columns("Party").IsVisible = True
        gv.Columns("Party").Width = 100
        gv.Columns("Party").HeaderText = "Party"

        gv.Columns("Party Name").IsVisible = True
        gv.Columns("Party Name").Width = 100
        gv.Columns("Party Name").HeaderText = "Party Name"
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rbtnDetail.IsChecked = True Then

            gv.Columns("postatus").IsVisible = True
            gv.Columns("postatus").Width = 100
            gv.Columns("postatus").HeaderText = "PO Status"

            gv.Columns("Indent").IsVisible = True
            gv.Columns("Indent").Width = 100
            gv.Columns("Indent").HeaderText = "Indent"

            gv.Columns("Item").IsVisible = True
            gv.Columns("Item").Width = 100
            gv.Columns("Item").HeaderText = "Item"

            gv.Columns("Name Of Item").IsVisible = True
            gv.Columns("Name Of Item").Width = 100
            gv.Columns("Name Of Item").HeaderText = "Name Of Item"

            gv.Columns("Rate").IsVisible = True
            gv.Columns("Rate").Width = 100
            gv.Columns("Rate").HeaderText = "Rate"

            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"

            gv.Columns("Disc%").IsVisible = True
            gv.Columns("Disc%").Width = 100
            gv.Columns("Disc%").HeaderText = "Disc%"

            gv.Columns("Disc_Amt").IsVisible = True
            gv.Columns("Disc_Amt").Width = 100
            gv.Columns("Disc_Amt").HeaderText = "Disc Amt"

            gv.Columns("Value").IsVisible = True
            gv.Columns("Value").Width = 100
            gv.Columns("Value").HeaderText = "Value"

            gv.Columns("Excise_Amount").IsVisible = True
            gv.Columns("Excise_Amount").Width = 100
            gv.Columns("Excise_Amount").HeaderText = "Excise"

            gv.Columns("VAT_Amount").IsVisible = True
            gv.Columns("VAT_Amount").Width = 100
            gv.Columns("VAT_Amount").HeaderText = "VAT"

            gv.Columns("CST_Amount").IsVisible = True
            gv.Columns("CST_Amount").Width = 100
            gv.Columns("CST_Amount").HeaderText = "CST"

            gv.Columns("IGST_Amount").IsVisible = True
            gv.Columns("IGST_Amount").Width = 100
            gv.Columns("IGST_Amount").HeaderText = "IGST"

            gv.Columns("CGST_Amount").IsVisible = True
            gv.Columns("CGST_Amount").Width = 100
            gv.Columns("CGST_Amount").HeaderText = "CGST"

            gv.Columns("SGST_Amount").IsVisible = True
            gv.Columns("SGST_Amount").Width = 100
            gv.Columns("SGST_Amount").HeaderText = "SGST"

            gv.Columns("UGST_Amount").IsVisible = True
            gv.Columns("UGST_Amount").Width = 100
            gv.Columns("UGST_Amount").HeaderText = "UGST"

            gv.Columns("Mandi_Amount").IsVisible = True
            gv.Columns("Mandi_Amount").Width = 100
            gv.Columns("Mandi_Amount").HeaderText = "Mandi Tax"

            gv.Columns("ADDTAX_Amount").IsVisible = True
            gv.Columns("ADDTAX_Amount").Width = 100
            gv.Columns("ADDTAX_Amount").HeaderText = "ADDTAX"

            gv.Columns("OTHER_Amount").IsVisible = True
            gv.Columns("OTHER_Amount").Width = 100
            gv.Columns("OTHER_Amount").HeaderText = "OTHER"

            gv.Columns("Excise_Rate").IsVisible = False
            gv.Columns("Excise_Rate").Width = 100
            gv.Columns("Excise_Rate").VisibleInColumnChooser = True
            gv.Columns("Excise_Rate").HeaderText = "Excise Rate"

            gv.Columns("VAT_Rate").IsVisible = False
            gv.Columns("VAT_Rate").Width = 100
            gv.Columns("VAT_Rate").VisibleInColumnChooser = True
            gv.Columns("VAT_Rate").HeaderText = "VAT Rate"

            gv.Columns("CST_Rate").IsVisible = False
            gv.Columns("CST_Rate").Width = 100
            gv.Columns("CST_Rate").VisibleInColumnChooser = True
            gv.Columns("CST_Rate").HeaderText = "CST Rate"

            gv.Columns("IGST_Rate").IsVisible = False
            gv.Columns("IGST_Rate").Width = 100
            gv.Columns("IGST_Rate").VisibleInColumnChooser = True
            gv.Columns("IGST_Rate").HeaderText = "IGST Rate"

            gv.Columns("CGST_Rate").IsVisible = False
            gv.Columns("CGST_Rate").Width = 100
            gv.Columns("CGST_Rate").VisibleInColumnChooser = True
            gv.Columns("CGST_Rate").HeaderText = "CGST Rate"

            gv.Columns("SGST_Rate").IsVisible = False
            gv.Columns("SGST_Rate").Width = 100
            gv.Columns("SGST_Rate").VisibleInColumnChooser = True
            gv.Columns("SGST_Rate").HeaderText = "SGST Rate"

            gv.Columns("UGST_Rate").IsVisible = False
            gv.Columns("UGST_Rate").Width = 100
            gv.Columns("UGST_Rate").VisibleInColumnChooser = True
            gv.Columns("UGST_Rate").HeaderText = "UGST Rate"

            gv.Columns("Mandi_Rate").IsVisible = False
            gv.Columns("Mandi_Rate").Width = 100
            gv.Columns("Mandi_Rate").VisibleInColumnChooser = True
            gv.Columns("Mandi_Rate").HeaderText = "Mandi Tax Rate"

            gv.Columns("ADDTAX_Rate").IsVisible = False
            gv.Columns("ADDTAX_Rate").Width = 100
            gv.Columns("ADDTAX_Rate").VisibleInColumnChooser = True
            gv.Columns("ADDTAX_Rate").HeaderText = "ADDTAX Rate"

            gv.Columns("OTHER_Rate").IsVisible = False
            gv.Columns("OTHER_Rate").Width = 100
            gv.Columns("OTHER_Rate").VisibleInColumnChooser = True
            gv.Columns("OTHER_Rate").HeaderText = "OTHER Rate"

            gv.Columns("Location").IsVisible = True
            gv.Columns("Location").Width = 100
            gv.Columns("Location").HeaderText = "Location"

            gv.Columns("Spaces").IsVisible = True
            gv.Columns("Spaces").Width = 100
            gv.Columns("Spaces").HeaderText = "Spaces"

            gv.Columns("DlDate").IsVisible = True
            gv.Columns("DlDate").Width = 100
            gv.Columns("DlDate").HeaderText = "DlDate"
            gv.Columns("DlDate").FormatString = "{0:d}"

            gv.Columns("Tax_Amount").IsVisible = True
            gv.Columns("Tax_Amount").Width = 100
            gv.Columns("Tax_Amount").HeaderText = "Tax Amount"

            gv.Columns("Net_Amount").IsVisible = True
            gv.Columns("Net_Amount").Width = 100
            gv.Columns("Net_Amount").HeaderText = "Net Amount"

            Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Disc_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Excise_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("VAT_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("CST_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("IGST_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("CGST_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("SGST_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("UGST_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Mandi_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("ADDTAX_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("OTHER_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Tax_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Net_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

        End If
        If rbtnSummary.IsChecked = True Then

            gv.Columns("postatus").IsVisible = True
            gv.Columns("postatus").Width = 100
            gv.Columns("postatus").HeaderText = "PO Status"

            gv.Columns("Bill_To_Location").IsVisible = True
            gv.Columns("Bill_To_Location").Width = 100
            gv.Columns("Bill_To_Location").HeaderText = "Location"

            gv.Columns("Location_Desc").IsVisible = True
            gv.Columns("Location_Desc").Width = 100
            gv.Columns("Location_Desc").HeaderText = "Location Name"

            gv.Columns("Amount_Less_Discount").IsVisible = True
            gv.Columns("Amount_Less_Discount").Width = 100
            gv.Columns("Amount_Less_Discount").HeaderText = "Amount"

            gv.Columns("Total_Tax_Amt").IsVisible = True
            gv.Columns("Total_Tax_Amt").Width = 100
            gv.Columns("Total_Tax_Amt").HeaderText = "Tax Amount"

            gv.Columns("Total_Add_Charge").IsVisible = True
            gv.Columns("Total_Add_Charge").Width = 100
            gv.Columns("Total_Add_Charge").HeaderText = "Additional charges"

            gv.Columns("PO_Total_Amt").IsVisible = True
            gv.Columns("PO_Total_Amt").Width = 100
            gv.Columns("PO_Total_Amt").HeaderText = "Net Amt"

            Dim item1 As New GridViewSummaryItem("Amount_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Total_Add_Charge", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("PO_Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
        End If
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Public Sub FillGridView(ByVal sql As String, ByVal gv As RadGridView)
        Dim bs As New BindingSource()
        ds = connectSql.RunSQLReturnDS(sql)
        bs.DataSource = ds.Tables(0)
        gv.DataSource = bs
    End Sub
    Public Function ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm) As Boolean
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        sfd.FileName = frm.Text
        Dim path As String

        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Return False
        End If



        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "gTax"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("There is no data for Show Excel Report.", Me.Text)
                    Return False
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                    If TypeOf grow.Cells(i).Value Is Decimal Then
                        Dim datecol As GridViewDecimalColumn = TryCast(gv.Columns(i), GridViewDecimalColumn)
                        datecol.ExcelExportType = DisplayFormatType.Standard
                    End If
                Next i
                '    exporter.Export(gv, path, frm.Text)

                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
                exporter.ExportHierarchy = True
                ' exporter.ExportVisualSettings = True
                exporter.SheetMaxRows = ExcelMaxRows._65536
                exporter.SheetName = frm.Text
                exporter.RunExport(Fullpath)

                frm.Controls.Remove(gv)
                '' Added By Abhishek For Show Excel Without save.

                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)
                Return True
            Catch ex As Exception
                frm.Controls.Remove(gv)
                common.clsCommon.MyMessageBoxShow("No Report Created.", "Export Error", MessageBoxButtons.OK)
                Return False
            End Try
        End If
    End Function
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = False
            e.ExcelStyleElement.FontStyle.Size = 8
        End If

        e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8

    End Sub

    Private Sub rdbtnOther_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbtnOther.ToggleStateChanged
        If rdbtnOther.IsChecked = True Then
            Dim Qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + dtpFromdate1.Value + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)AND STATUS=1 and item_type<>'F'  "
            cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
            cbgPoInvoice.ValueMember = "Code"
        ElseIf rdbtnFinishedGood.IsChecked = True Then
            Dim Qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + dtpFromdate1.Value + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)AND STATUS=1 and item_type='F'  "
            cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
            cbgPoInvoice.ValueMember = "Code"

        End If
    End Sub

    'Private Sub dtpFromdate1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromdate1.ValueChanged
    '    If rdbtnOther.IsChecked = True Then
    '        Dim Qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + dtpFromdate1.Value + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)and STATUS=1 and item_type<>'F'  "
    '        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
    '        cbgPoInvoice.ValueMember = "Code"
    '    ElseIf rdbtnFinishedGood.IsChecked = True Then
    '        Dim Qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + dtpFromdate1.Value + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)and STATUS=1 and item_type='F'  "
    '        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
    '        cbgPoInvoice.ValueMember = "Code"

    '    End If
    'End Sub

    'Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
    '    If rdbtnOther.IsChecked = True Then
    '        Dim Qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + dtpFromdate1.Value + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)and  STATUS=1 and item_type<>'F'  "
    '        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
    '        cbgPoInvoice.ValueMember = "Code"
    '    ElseIf rdbtnFinishedGood.IsChecked = True Then
    '        Dim Qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE Convert(Date,PurchaseOrder_Date,103) >=convert(date,'" + dtpFromdate1.Value + "',103) and Convert(Date,PurchaseOrder_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)and STATUS=1 and item_type='F'  "
    '        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
    '        cbgPoInvoice.ValueMember = "Code"

    '    End If
    'End Sub


    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        btnReferesh = True
        PrintData(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        btnReferesh = True
        PrintData(EnumExportTo.PDF)
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        If gv.Rows.Count > 0 Then
            Dim strDoc
            strDoc = gv.CurrentRow.Cells("PONO").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, strDoc)
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")

    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Public Sub LoadPo()
        Dim Qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE STATUS=1 and item_type='F'  "
        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgPoInvoice.ValueMember = "Code"
    End Sub

    Public Sub LoadVendor()
        Dim Qry As String = "select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER Where Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgVendor.ValueMember = "Code"
    End Sub

    Private Sub txtPoNo__My_Click(sender As Object, e As EventArgs) Handles txtPoNo._My_Click
        'Dim qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE STATUS=1 and item_type='F'"
        'as per amit sir
        Dim qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as PoDate from TSPL_PURCHASE_ORDER_HEAD WHERE STATUS=1 "
        txtPoNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "PoDate", txtPoNo.arrValueMember, txtPoNo.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click

        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)

    End Sub

    Private Sub gv_CellDoubleClick_1(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        If cboMonthlyYearly.Text = "None" Then
            If clsCommon.myLen(gv.CurrentRow.Cells("PONO").Value) >= 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, gv.CurrentRow.Cells("PONO").Value)
            End If
        End If
    End Sub


    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub
    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        CheckedAll(gvCategory)
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        UnCheckedAll(gvCategory)
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged_1(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
        RadButton7.Enabled = rbtnCategorySelect.IsChecked
        RadButton6.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    
    Private Sub rbtnSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnSummary.ToggleStateChanged
        If rbtnSummary.IsChecked = True Then
            cboMonthlyYearly.Enabled = False
            cboFiscalYear.Enabled = False
            cboQtyValueWise.Enabled = False
            dtpFromdate1.Enabled = True
            dtpToDate.Enabled = True
            cboMonthlyYearly.Text = "None"
        Else
            cboMonthlyYearly.Enabled = True
            If cboMonthlyYearly.Text <> "None" Then
                cboFiscalYear.Enabled = True
                cboQtyValueWise.Enabled = True
            Else
                cboFiscalYear.Enabled = False
                cboQtyValueWise.Enabled = False
            End If
        End If
    End Sub

    Private Sub rbtnDetail_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnDetail.ToggleStateChanged
        If rbtnDetail.IsChecked = True Then
            cboMonthlyYearly.Enabled = True
            cboFiscalYear.Enabled = True
            If cboMonthlyYearly.Text = "None" Then
                dtpFromdate1.Enabled = True
                dtpToDate.Enabled = True
                cboFiscalYear.Enabled = False
                cboQtyValueWise.Enabled = False
            Else
                dtpFromdate1.Enabled = False
                dtpToDate.Enabled = False
                cboFiscalYear.Enabled = True
                cboQtyValueWise.Enabled = True
            End If
        Else
            cboMonthlyYearly.Enabled = False
            If cboMonthlyYearly.Text <> "None" Then
                cboFiscalYear.Enabled = True
                cboQtyValueWise.Enabled = True
            Else
                cboFiscalYear.Enabled = False
                cboQtyValueWise.Enabled = False
            End If

        End If
    End Sub

    Private Sub cboMonthlyYearly_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboMonthlyYearly.SelectedIndexChanged
        If cboMonthlyYearly.Text <> "None" Then
            dtpFromdate1.Enabled = False
            dtpToDate.Enabled = False
            cboFiscalYear.Enabled = True
            cboQtyValueWise.Enabled = True
        Else
            dtpFromdate1.Enabled = True
            dtpToDate.Enabled = True
            cboFiscalYear.Enabled = False
            cboQtyValueWise.Enabled = False
        End If
        If cboMonthlyYearly.Text = "Daily" Then
            MyLabel1.Text = "Year"
            cboMonthName.Enabled = True
            cboQtyValueWise.Enabled = False
            FillFicalYear()
        Else
            MyLabel1.Text = "Fiscal Year"
            cboMonthName.Enabled = False
            If cboMonthlyYearly.Text = "None" Then
                cboQtyValueWise.Enabled = False
            Else
                cboQtyValueWise.Enabled = True
            End If

            FillFicalYear()
        End If
    End Sub

    Sub LoadTypes()
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("January")
        dt.Rows.Add("February")
        dt.Rows.Add("March")
        dt.Rows.Add("April")
        dt.Rows.Add("May")
        dt.Rows.Add("June")
        dt.Rows.Add("July")
        dt.Rows.Add("August")
        dt.Rows.Add("September")
        dt.Rows.Add("October")
        dt.Rows.Add("November")
        dt.Rows.Add("December")
        cboMonthName.DataSource = dt
        cboMonthName.ValueMember = "Code"
        cboMonthName.DisplayMember = "Code"
    End Sub

    Sub LoadMonthlyReportType()
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Qty Wise")
        dt.Rows.Add("Value Wise")
        dt.Rows.Add("Both")
        cboQtyValueWise.DataSource = dt
        cboQtyValueWise.ValueMember = "Code"
        cboQtyValueWise.DisplayMember = "Code"
    End Sub

    Private Sub TxtStructure__My_Click(sender As Object, e As EventArgs) Handles txtStructure._My_Click
        Dim qry As String = "select Structure_Code as Code,Structure_Descq as Name from  TSPL_STRUCTURE_MASTER  "
        txtStructure.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSelStr1", qry, "Code", "Name", txtStructure.arrValueMember, txtStructure.arrDispalyMember)
    End Sub
End Class
