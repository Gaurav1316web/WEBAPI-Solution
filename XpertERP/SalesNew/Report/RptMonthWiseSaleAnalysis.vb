Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop


Public Class RptMonthWiseSaleAnalysis
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrTransaction As ArrayList
    Dim dtCategory As DataTable
    Dim Categorytype As ArrayList
    Dim Categoryvalues As ArrayList
    Dim FORMTYPE As String = Nothing
    'Dim dt As DataTable
#End Region
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()

        InitializeComponent()
    End Sub

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    'Sub LoadTypes()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Code") = "Product Wise"
    '    dt.Rows.Add(dr)
    '    dr = dt.NewRow()
    '    dr("Code") = "Location Wise"
    '    dt.Rows.Add(dr)
    '    'dt.Rows.Add("Product Wise")
    '    ' dt.Rows.Add("Location Wise")
    '    ddlReportType.DataSource = clsDBFuncationality.getSingleValue("select 'Product Wise' as Code union all select 'Location Wise' as Code")
    '    ddlReportType.ValueMember = "Code"
    '    ddlReportType.DisplayMember = "Code"
    'End Sub
    Sub LoadTypes()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Product Wise")
        dt.Rows.Add("Location Wise")

        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub

    Sub LoadCategory()
        rbtnCategoryAll.IsChecked = True
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

    Private Sub GVcategorydata()
        Categorytype = New ArrayList()
        Categoryvalues = New ArrayList()
        Try
            If rbtnCategorySelect.IsChecked Then
                For ii As Integer = 0 To gvCategory.RowCount - 1
                    If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                        Categorytype.Add(gvCategory.Rows(ii).Cells("CODE").Value)
                        Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            For Each strInn As String In arr.Keys
                                Categoryvalues.Add(strInn)
                            Next
                        Else
                            Dim qry As String = " select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "'"
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                For Each dr As DataRow In dt1.Rows
                                    Categoryvalues.Add(clsCommon.myCstr(dr("CODE").ToString()))
                                Next
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Categorytype = Nothing
            Categoryvalues = Nothing
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub SetUserMgmtNew()

        ' MyBase.SetUserMgmt(clsUserMgtCode.stockRecoBatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport

    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub


    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            GVcategorydata()
            '=====1st Date========
            Dim strd As String = "01/" + "04" + "/" + clsCommon.myCstr(fromDate.Value.Year)
            Dim From_Date As Date = clsCommon.myCDate(strd) 'fromDate.Value.AddYears(-1)
            Dim F_Year1 As String = clsCommon.myCstr(fromDate.Value.Year) + "-" + clsCommon.myCstr(Todate.Value.Year)

            '============================
            '=====2nd Date========
            Dim strd2 As String = "01/" + "04" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(-1).Year)
            Dim From_Date2 As Date = clsCommon.myCDate(strd2) 'fromDate.Value.AddYears(-1)

            Dim strTd2 As String = "01/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.Year)
            Dim To_Date2 As Date = clsCommon.myCDate(strTd2) 'fromDate.Value.AddYears(-1)
            Dim F_Year2 As String = clsCommon.myCstr(fromDate.Value.AddYears(-1).Year) + "-" + clsCommon.myCstr(fromDate.Value.Year)
            '==========================
            '=====3rd Date========
            Dim strd3 As String = "01/" + "04" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(-2).Year)
            Dim From_Date3 As Date = clsCommon.myCDate(strd3) 'fromDate.Value.AddYears(-1)

            Dim strTd3 As String = "01/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(-1).Year)
            Dim To_Date3 As Date = clsCommon.myCDate(strTd3) 'fromDate.Value.AddYears(-1)
            Dim F_Year3 As String = clsCommon.myCstr(fromDate.Value.AddYears(-2).Year) + "-" + clsCommon.myCstr(fromDate.Value.AddYears(-1).Year)
            '============================

            Dim strCategoryTable As String = ""

            Dim qry As String = Nothing
            Dim strpivot As String = Nothing
            Dim strpivotposition As String = Nothing
            Dim NoMaxqry As String = Nothing
            Dim NoMaxposition As String = Nothing

            Dim strTotal As String = "select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('isnull(SUM('+A.MonthName+'),0)+') from " & _
                "(SELECT DATENAME(MONTH, cast('2013 -'+ ltrim(rtrim(CAST(number as varchar(2)))) + '-1' as datetime)) MonthName FROM master..spt_values " & _
                " WHERE Type = 'P' and number between 1 and 12 )as A FOR XML PATH(''))as xvalue)a "
            Dim StrAddTotalMonth As String = clsDBFuncationality.getSingleValue(strTotal)

            Dim strRateTotal As String = "select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('isnull(avg('+A.MonthName+'),0)+') from " & _
             "(SELECT DATENAME(MONTH, cast('2013 -'+ ltrim(rtrim(CAST(number as varchar(2)))) + '-1' as datetime)) MonthName FROM master..spt_values " & _
             " WHERE Type = 'P' and number between 1 and 12 )as A FOR XML PATH(''))as xvalue)a "
            Dim StrTotalMonthRate As String = clsDBFuncationality.getSingleValue(strRateTotal)

            Dim StrQuery As String = Nothing
            Dim strCategory As String = Nothing
            Dim strInnQry As String = Nothing
            strInnQry = "select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost as Rate,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt AS Item_NetAmt,TSPL_SD_SALE_INVOICE_DETAIL.Amount as Amount,Customer_Code,Total_Amt,Document_Date ,Bill_To_Location,Trans_Type from TSPL_SD_SALE_INVOICE_HEAD left outer join " & _
             " TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
             " union all " & _
            " SELECT TSPL_SCRAPINVOICE_DETAIL.Item_Code,TSPL_SCRAPINVOICE_DETAIL.Unit_code,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty AS Qty,TSPL_SCRAPINVOICE_DETAIL.price AS Rate,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt as Item_NetAmt,TSPL_SCRAPINVOICE_DETAIL.ItemAmt as Amount,TSPL_SCRAPINVOICE_HEAD.cust_Code as Customer_Code,TSPL_SCRAPINVOICE_HEAD.ship_Total_Amt as Total_Amt,TSPL_SCRAPINVOICE_HEAD.shipment_Date as Document_Date,TSPL_SCRAPINVOICE_HEAD.ToLoc_Code as Bill_To_Location,'SS' as Trans_Type FROM TSPL_SCRAPINVOICE_HEAD LEFT OUTER JOIN TSPL_SCRAPINVOICE_DETAIL ON TSPL_SCRAPINVOICE_HEAD.invoice_No=TSPL_SCRAPINVOICE_DETAIL.invoice_No  " & _
             " union all  " & _
           " select TSPL_Dispatch_Detail_BulkSale.Item_Code,TSPL_Dispatch_Detail_BulkSale.Unit_code,TSPL_Dispatch_Detail_BulkSale.Qty,TSPL_Dispatch_Detail_BulkSale.NetMilkRate as Rate,TSPL_Dispatch_Detail_BulkSale.Amount as Item_NetAmt,TSPL_Dispatch_Detail_BulkSale.Amount as Amount,Customer_Code,Total_Amt,Document_Date,Location_Code as Bill_To_Location,'BS' AS Trans_Type from TSPL_Dispatch_BulkSale left outer join " & _
            " TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No "

            strCategory = " select Item_Code,max([CATEGORY RM]) as [CATEGORY RM],max([BRAND]) as [BRAND],max([SUB BRAND]) as [SUB BRAND],max([DESCRP]) as [DESCRP],max([PACK]) as [PACK],max([PACK SIZE]) as [PACK SIZE],max([CATEGORY OT]) as [CATEGORY OT],max([CATEGORY FA]) as [CATEGORY FA],max([P TYPE]) as [P TYPE],max([L TYPE]) as [L TYPE],max([JW]) as [JW],max([SCRAP]) as [SCRAP],max([CATEGORY RMDESC]) as [CATEGORY RMDESC],max([BRANDDESC]) as [BRANDDESC],max([SUB BRANDDESC]) as [SUB BRANDDESC],max([DESCRPDESC]) as [DESCRPDESC],max([PACKDESC]) as [PACKDESC],max([PACK SIZEDESC]) as [PACK SIZEDESC],max([CATEGORY OTDESC]) as [CATEGORY OTDESC],max([CATEGORY FADESC]) as [CATEGORY FADESC],max([P TYPEDESC]) as [P TYPEDESC],max([L TYPEDESC]) as [L TYPEDESC],max([JWDESC]) as [JWDESC],max([SCRAPDESC]) as [SCRAPDESC]  from (" & _
           " select * from ( " & _
          "  select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code  " & _
           " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " & _
          "  ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
           " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " & _
           " from  TSPL_ITEM_MASTER  " & _
          "  left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
           " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
          "  where 2=2 )xx " & _
          " Pivot " & _
          "  ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [CATEGORY RM],[BRAND],[SUB BRAND],[DESCRP],[PACK],[PACK SIZE],[CATEGORY OT],[CATEGORY FA],[P TYPE],[L TYPE],[JW],[SCRAP]) " & _
            " ) Pivt " & _
          "  Pivot " & _
           " ( max(Category_Value_Desc) for Item_Category_CodeDesc in ([CATEGORY RMDESC],[BRANDDESC],[SUB BRANDDESC],[DESCRPDESC],[PACKDESC],[PACK SIZEDESC],[CATEGORY OTDESC],[CATEGORY FADESC],[P TYPEDESC],[L TYPEDESC],[JWDESC],[SCRAPDESC]) " & _
            " ) Pivt1 ) xxx "
            If Categoryvalues.Count > 0 Then
                strCategory += " WHERE [SUB BRAND] IN('" + clsCommon.GetMulcallStringWithComma(Categoryvalues).Replace(",", "','") + "')  "
            End If
            strCategory += " group by Item_Code"

            Dim Str_Rat_qty_amt As String = Nothing
            Str_Rat_qty_amt = "isnull((isnull(SALE_INFO.Qty,0) * (case when ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor/ (CASE WHEN ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 THEN 1 ELSE ConvertedUnit_MT.Conversion_Factor end) END))  ,0) as [QtyKG] , " & _
            "case when ISNULL(SALE_INFO.Amount,0)=0 then 0 else cast((ISNULL(SALE_INFO.Amount,0)/100000) as decimal(10,3)) end  AS [AMOUNT], "
            'Str_Rat_qty_amt += " cast(ISNULL(SALE_INFO.Amount,0)/(CASE WHEN isnull((SALE_INFO.Qty * (case when ISNULL(ConvertedUnit_kg.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor/(CASE WHEN ISNULL(ConvertedUnit_kg.Conversion_Factor,0)=0 THEN 1 ELSE ConvertedUnit_kg.Conversion_Factor end) END))  ,0)=0 THEN 1 ELSE isnull((SALE_INFO.Qty * (case when ISNULL(ConvertedUnit_kg.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor/(CASE WHEN ISNULL(ConvertedUnit_kg.Conversion_Factor,0)=0 THEN 1 ELSE ConvertedUnit_kg.Conversion_Factor end) END))  ,0) END)as decimal(10,2)) AS [Rate] "
            Str_Rat_qty_amt += " (CASE WHEN isnull((isnull(SALE_INFO.Qty,0) * (case when ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor/ (CASE WHEN ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 THEN 1 ELSE ConvertedUnit_MT.Conversion_Factor end) END))  ,0 )=0 and  ISNULL(SALE_INFO.Amount,0)=0 THEN 0 ELSE " & _
                "cast(((cast((ISNULL(SALE_INFO.Amount,0)/100000) as decimal(10,3))/ case when isnull((SALE_INFO.Qty * (case when ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor/ (CASE WHEN ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 THEN 1 ELSE ConvertedUnit_MT.Conversion_Factor end) END))  ,0)   = 0 then 1 else isnull((SALE_INFO.Qty * (case when ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 then 1 else FATSNFConvertedUnit.Conversion_Factor/ (CASE WHEN ISNULL(ConvertedUnit_MT.Conversion_Factor,0)=0 THEN 1 ELSE ConvertedUnit_MT.Conversion_Factor end) END))  ,0) end            )*100)as decimal(10,3)) end )AS [Rate] "
            Dim TBL_Rat_qty_amt As String = Nothing
            TBL_Rat_qty_amt = "left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=SALE_INFO.Item_Code   and FATSNFConvertedUnit.UOM_Code= SALE_INFO.unit_code " & _
                "left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit_MT on ConvertedUnit_MT.Item_Code=SALE_INFO.Item_Code  and ConvertedUnit_MT.UOM_Code= 'MT' " & _
               " left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit_KG on ConvertedUnit_KG.Item_Code=SALE_INFO.Item_Code and ConvertedUnit_KG.UOM_Code=   'KG' "

            StrQuery = "with ctCurrent as(SELECT * FROM (select Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc, VirtualCategoryTabel.[SUB BRAND],DATENAME(MONTH,SALE_INFO.Document_Date) as [Months]," &
               "" + Str_Rat_qty_amt + " from " & _
               "  (" + strInnQry + ") AS  SALE_INFO  " & _
               "  left outer join TSPL_ITEM_MASTER on SALE_INFO.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_LOCATION_MASTER   on SALE_INFO.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
               " " + TBL_Rat_qty_amt + " " & _
               " left outer join ( " + strCategory + " ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=SALE_INFO.Item_Code"

            StrQuery += "  where 2=2 and convert(date,SALE_INFO.Document_Date,103)>=convert(date,'" + From_Date + "',103) and convert(date,SALE_INFO.Document_Date,103)<=convert(date,'" + Todate.Value + "',103)"
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Location Wise") = CompairStringResult.Equal Then
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    StrQuery += " and Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
            End If
            StrQuery += "  ) MainQuery  ),"

            StrQuery += " ctPrevious as(SELECT * FROM (select Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc, VirtualCategoryTabel.[SUB BRAND],DATENAME(MONTH,SALE_INFO.Document_Date) as [Months]," & _
             " " + Str_Rat_qty_amt + " from " & _
           "  (" + strInnQry + ") AS  SALE_INFO  " & _
           "  left outer join TSPL_ITEM_MASTER on SALE_INFO.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_LOCATION_MASTER   on SALE_INFO.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
            " " + TBL_Rat_qty_amt + " " & _
           " left outer join ( " + strCategory + " ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=SALE_INFO.Item_Code"

            StrQuery += "  where 2=2 and convert(date,SALE_INFO.Document_Date,103)>=convert(date,'" + From_Date2 + "',103) and convert(date,SALE_INFO.Document_Date,103)<=convert(date,'" + To_Date2 + "',103)"
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "location Wise") = CompairStringResult.Equal Then
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    StrQuery += " and Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
            End If
            StrQuery += "  ) MainQuery  ),"

            StrQuery += "  ctNexToPrevious as(SELECT * FROM (select  Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,VirtualCategoryTabel.[SUB BRAND],DATENAME(MONTH,SALE_INFO.Document_Date) as [Months]," & _
               " " + Str_Rat_qty_amt + "  from " & _
             "  (" + strInnQry + ") AS  SALE_INFO  " & _
             "  left outer join TSPL_ITEM_MASTER on SALE_INFO.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_LOCATION_MASTER   on SALE_INFO.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
              " " + TBL_Rat_qty_amt + " " & _
             " left outer join ( " + strCategory + " ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=SALE_INFO.Item_Code"

            StrQuery += "  where 2=2 and convert(date,SALE_INFO.Document_Date,103)>=convert(date,'" + From_Date3 + "',103) and convert(date,SALE_INFO.Document_Date,103)<=convert(date,'" + To_Date3 + "',103)"
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "location Wise") = CompairStringResult.Equal Then
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    StrQuery += " and Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
            End If
            StrQuery += "  ) MainQuery  )"

            Dim StrGrpBy As String = Nothing
            Dim StrGrpType As String = Nothing
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Product Wise") = CompairStringResult.Equal Then
                StrGrpType = " [SUB BRAND],"
                StrGrpBy = " group by [SUB BRAND] "
            Else
                StrGrpType = "Location_Desc as [Location Name] ,[SUB BRAND],"
                StrGrpBy = " group by Location_Desc,[SUB BRAND] "
            End If

            StrQuery += "  select " + StrGrpType + " 'Qty./MT.' as Type,'" + F_Year1 + "' AS F_YEAR,isnull(SUM([APRIL]),0) AS [APRIL],isnull(SUM([MAY]),0) AS [MAY],isnull(SUM([JUNE]),0) AS [JUNE],isnull(SUM([JULY]),0) AS [JULY],isnull(SUM([AUGUST]),0) AS [AUGUST],isnull(SUM([SEPTEMBER]),0) AS [SEPTEMBER],isnull(SUM([OCTOBER]),0) AS [OCTOBER],isnull(SUM([NOVEMBER]),0) AS [NOVEMBER],isnull(SUM([DECEMBER]),0) AS [DECEMBER],isnull(SUM([JANUARY]),0) AS [JANUARY],isnull(SUM([FEBRUARY]),0) AS [FEBRUARY],isnull(SUM([MARCH]),0) AS [MARCH]," + StrAddTotalMonth + " as TOTAL from ctCurrent " & _
            " Pivot( SUM([QtyKG]) for Months   in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + "" & _
            " union all " & _
                 " select " + StrGrpType + "'Rs./Lacs' as Type,'" + F_Year1 + "' AS F_YEAR,isnull(SUM([APRIL]),0) AS [APRIL],isnull(SUM([MAY]),0) AS [MAY],isnull(SUM([JUNE]),0) AS [JUNE],isnull(SUM([JULY]),0) AS [JULY],isnull(SUM([AUGUST]),0) AS [AUGUST],isnull(SUM([SEPTEMBER]),0) AS [SEPTEMBER],isnull(SUM([OCTOBER]),0) AS [OCTOBER],isnull(SUM([NOVEMBER]),0) AS [NOVEMBER],isnull(SUM([DECEMBER]),0) AS [DECEMBER],isnull(SUM([JANUARY]),0) AS [JANUARY],isnull(SUM([FEBRUARY]),0) AS [FEBRUARY],isnull(SUM([MARCH]),0) AS [MARCH]," + StrAddTotalMonth + " as TOTAL from ctCurrent " & _
                 " Pivot ( SUM([Amount]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + " " & _
                 " union all " & _
               "  select " + StrGrpType + "'Rate/Kg.' as Type,'" + F_Year1 + "' AS F_YEAR,isnull(avg([APRIL]),0) AS [APRIL],isnull(avg([MAY]),0) AS [MAY],isnull(avg([JUNE]),0) AS [JUNE],isnull(avg([JULY]),0) AS [JULY],isnull(avg([AUGUST]),0) AS [AUGUST],isnull(avg([SEPTEMBER]),0) AS [SEPTEMBER],isnull(avg([OCTOBER]),0) AS [OCTOBER],isnull(avg([NOVEMBER]),0) AS [NOVEMBER],isnull(avg([DECEMBER]),0) AS [DECEMBER],isnull(avg([JANUARY]),0) AS [JANUARY],isnull(avg([FEBRUARY]),0) AS [FEBRUARY],isnull(avg([MARCH]),0) AS [MARCH]," + StrTotalMonthRate + " as TOTAL from ctCurrent " & _
           " Pivot( avg([Rate]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + " " & _
            " union all "
            StrQuery += " select " + StrGrpType + "'Qty./MT.' as Type,'" + F_Year2 + "' AS F_YEAR,isnull(SUM([APRIL]),0) AS [APRIL],isnull(SUM([MAY]),0) AS [MAY],isnull(SUM([JUNE]),0) AS [JUNE],isnull(SUM([JULY]),0) AS [JULY],isnull(SUM([AUGUST]),0) AS [AUGUST],isnull(SUM([SEPTEMBER]),0) AS [SEPTEMBER],isnull(SUM([OCTOBER]),0) AS [OCTOBER],isnull(SUM([NOVEMBER]),0) AS [NOVEMBER],isnull(SUM([DECEMBER]),0) AS [DECEMBER],isnull(SUM([JANUARY]),0) AS [JANUARY],isnull(SUM([FEBRUARY]),0) AS [FEBRUARY],isnull(SUM([MARCH]),0) AS [MARCH]," + StrAddTotalMonth + " as TOTAL from ctPrevious  " & _
                  " Pivot(SUM([QtyKG]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + "  " & _
                       " union all   " & _
                " select " + StrGrpType + "'Rs./Lacs' as Type,'" + F_Year2 + "' AS F_YEAR,isnull(SUM([APRIL]),0) AS [APRIL],isnull(SUM([MAY]),0) AS [MAY],isnull(SUM([JUNE]),0) AS [JUNE],isnull(SUM([JULY]),0) AS [JULY],isnull(SUM([AUGUST]),0) AS [AUGUST],isnull(SUM([SEPTEMBER]),0) AS [SEPTEMBER],isnull(SUM([OCTOBER]),0) AS [OCTOBER],isnull(SUM([NOVEMBER]),0) AS [NOVEMBER],isnull(SUM([DECEMBER]),0) AS [DECEMBER],isnull(SUM([JANUARY]),0) AS [JANUARY],isnull(SUM([FEBRUARY]),0) AS [FEBRUARY],isnull(SUM([MARCH]),0) AS [MARCH]," + StrAddTotalMonth + " as TOTAL from ctPrevious  " & _
                 " Pivot ( SUM([Amount]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + " " & _
                " union all  " & _
                " select " + StrGrpType + "'Rate/Kg.' as Type,'" + F_Year2 + "' AS F_YEAR,isnull(avg([APRIL]),0) AS [APRIL],isnull(avg([MAY]),0) AS [MAY],isnull(avg([JUNE]),0) AS [JUNE],isnull(avg([JULY]),0) AS [JULY],isnull(avg([AUGUST]),0) AS [AUGUST],isnull(avg([SEPTEMBER]),0) AS [SEPTEMBER],isnull(avg([OCTOBER]),0) AS [OCTOBER],isnull(avg([NOVEMBER]),0) AS [NOVEMBER],isnull(avg([DECEMBER]),0) AS [DECEMBER],isnull(avg([JANUARY]),0) AS [JANUARY],isnull(avg([FEBRUARY]),0) AS [FEBRUARY],isnull(avg([MARCH]),0) AS [MARCH]," + StrTotalMonthRate + " as TOTAL from ctPrevious " & _
                 " Pivot ( avg([Rate]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + "  " & _
                    " union all  "
            StrQuery += " select " + StrGrpType + "'Qty./MT.' as Type,'" + F_Year3 + "' AS F_YEAR,isnull(SUM([APRIL]),0) AS [APRIL],isnull(SUM([MAY]),0) AS [MAY],isnull(SUM([JUNE]),0) AS [JUNE],isnull(SUM([JULY]),0) AS [JULY],isnull(SUM([AUGUST]),0) AS [AUGUST],isnull(SUM([SEPTEMBER]),0) AS [SEPTEMBER],isnull(SUM([OCTOBER]),0) AS [OCTOBER],isnull(SUM([NOVEMBER]),0) AS [NOVEMBER],isnull(SUM([DECEMBER]),0) AS [DECEMBER],isnull(SUM([JANUARY]),0) AS [JANUARY],isnull(SUM([FEBRUARY]),0) AS [FEBRUARY],isnull(SUM([MARCH]),0) AS [MARCH]," + StrAddTotalMonth + " as TOTAL from ctNexToPrevious  " & _
            " Pivot ( SUM([QtyKG]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + "  " & _
              " union all " & _
               "  select " + StrGrpType + "'Rs./Lacs' as Type,'" + F_Year3 + "' AS F_YEAR,isnull(SUM([APRIL]),0) AS [APRIL],isnull(SUM([MAY]),0) AS [MAY],isnull(SUM([JUNE]),0) AS [JUNE],isnull(SUM([JULY]),0) AS [JULY],isnull(SUM([AUGUST]),0) AS [AUGUST],isnull(SUM([SEPTEMBER]),0) AS [SEPTEMBER],isnull(SUM([OCTOBER]),0) AS [OCTOBER],isnull(SUM([NOVEMBER]),0) AS [NOVEMBER],isnull(SUM([DECEMBER]),0) AS [DECEMBER],isnull(SUM([JANUARY]),0) AS [JANUARY],isnull(SUM([FEBRUARY]),0) AS [FEBRUARY],isnull(SUM([MARCH]),0) AS [MARCH]," + StrAddTotalMonth + " as TOTAL from ctNexToPrevious " & _
                  " Pivot( SUM([Amount]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + " " & _
                 " union all " & _
                " select " + StrGrpType + "'Rate/Kg.' as Type,'" + F_Year3 + "' AS F_YEAR,isnull(avg([APRIL]),0) AS [APRIL],isnull(avg([MAY]),0) AS [MAY],isnull(avg([JUNE]),0) AS [JUNE],isnull(avg([JULY]),0) AS [JULY],isnull(avg([AUGUST]),0) AS [AUGUST],isnull(avg([SEPTEMBER]),0) AS [SEPTEMBER],isnull(avg([OCTOBER]),0) AS [OCTOBER],isnull(avg([NOVEMBER]),0) AS [NOVEMBER],isnull(avg([DECEMBER]),0) AS [DECEMBER],isnull(avg([JANUARY]),0) AS [JANUARY],isnull(avg([FEBRUARY]),0) AS [FEBRUARY],isnull(avg([MARCH]),0) AS [MARCH]," + StrTotalMonthRate + " as TOTAL from ctNexToPrevious " & _
                  " Pivot( avg([Rate]) for Months  in ([APRIL],[MAY],[JUNE],[JULY],[AUGUST],[SEPTEMBER],[OCTOBER],[NOVEMBER],[DECEMBER],[JANUARY],[FEBRUARY],[MARCH]) ) PVT " + StrGrpBy + " "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
            If dt.Rows.Count > 0 Then
                gv1.GroupDescriptors.Clear()
                gv1.DataSource = dt
                If clsCommon.CompairString(ddlReportType.SelectedValue, "Product Wise") = CompairStringResult.Equal Then
                    gv1.GroupDescriptors.Add(New GridGroupByExpression("[SUB BRAND] AS SubBrand format ""{0}: {1}"" Group By [SUB BRAND]"))
                Else
                    gv1.GroupDescriptors.Add(New GridGroupByExpression("[Location Name] AS Location format ""{0}: {1}"" Group By Location"))
                    gv1.GroupDescriptors.Add(New GridGroupByExpression("[SUB BRAND] AS SubBrand format ""{0}: {1}"" Group By [SUB BRAND]"))
                End If
                gv1.GroupDescriptors.Add(New GridGroupByExpression("F_YEAR AS F_YEAR format ""{0}: {1}"" Group By F_YEAR"))

                ' Dim summaryRowItem As New GridViewSummaryRowItem()

                'Dim intCount As Integer = 0
                'Dim item1 As New GridViewSummaryItem("APRIL", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item1)
                'Dim item2 As New GridViewSummaryItem("MAY", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item2)
                'Dim item3 As New GridViewSummaryItem("JUNE", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item3)
                'Dim item4 As New GridViewSummaryItem("JULY", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item4)
                'Dim item5 As New GridViewSummaryItem("AUGUST", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item5)
                'Dim item6 As New GridViewSummaryItem("SEPTEMBER", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item6)
                'Dim item7 As New GridViewSummaryItem("OCTOBER", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item7)
                'Dim item8 As New GridViewSummaryItem("NOVEMBER", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item8)
                'Dim item9 As New GridViewSummaryItem("DECEMBER", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item9)
                'Dim item10 As New GridViewSummaryItem("JANUARY", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item10)
                'Dim item11 As New GridViewSummaryItem("FEBRUARY", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item11)
                'Dim item12 As New GridViewSummaryItem("MARCH", "{0:F0}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(item12)

                'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                gv1.AutoExpandGroups = True
                gv1.ShowGroupPanel = False
                gv1.ShowRowHeaderColumn = False
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.ReadOnly = True
                gv1.BestFitColumns()
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found..", Me.Text)
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)

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
    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData(0)

            Dim arrHeader As List(Of String) = New List(Of String)()
            '  arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Month Wise Sale Analysis Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Month Wise Sale Analysis Report", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'print(EnumExportTo.Excel)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMonthWiseSaleAnalysis & "'"))
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            Dim Export As New ExportToExcelML(gv1)
            Export.RunExport(filePath)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'print(EnumExportTo.PDF)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "yyyy") + " To " + clsCommon.GetPrintDate(Todate.Value, "yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMonthWiseSaleAnalysis & "'"))
           
            arrHeader.Add(" Report Type : " + ddlReportType.Text)
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If

            If txtLocation.Visible = True Then
                If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
            End If

            If rbtnCategorySelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvCategory.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Category : " + strLoca)
            End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        Reset()
    End Sub

    Sub Reset()
        EnableDisableCtrl(True)
        LoadCategory()
        LoadTypes()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        fromDate.Value = clsCommon.GETSERVERDATE()
        fromDate.CustomFormat = "yyyy"
        fromDate.Format = DateTimePickerFormat.Custom
        fromDate.Culture.Calendar.GetYear(clsCommon.GETSERVERDATE())
        Dim strToDate As String = Nothing
        Dim To_Date As Date
        strToDate = "31/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(+1).Year)
        To_Date = clsCommon.myCDate(strToDate)
        Todate.Value = To_Date
        Todate.Enabled = False
        txtCustomer.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        UnCheckedAll(gvCategory)
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        lblLocation.Visible = False
        txtLocation.Visible = False
        RadPageViewPage2.Text = "Report"


    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)

        txtTransaction.Enabled = val

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'clsGridLayout.DeleteData("stReNCatg", objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData("stReNItem", objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData("stReNLOCItem", objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData("stReNDoc", objCommonVar.CurrentUserCode)
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub
    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim Str As String = String.Empty

        Dim qry As String = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()

        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) <> CompairStringResult.Equal Then
            txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
        End If

        'Dim qry As String = " select Code,Name,InOutType as [In/Out Type],Type from TSPL_INVENTORY_SOURCE_CODE where 2=2 "

        'txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSe", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs)
        Try
            LoadData(2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Shared Sub ExportBulkData(ByVal qry As String, ByVal arrVisibleColumAndCaption As Dictionary(Of String, String), ByVal strReportNameInSaveDialog As String)
        'clsCommon.ProgressBarPercentShow()
        clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            If arrVisibleColumAndCaption Is Nothing OrElse arrVisibleColumAndCaption.Count <= 0 Then
                Throw New Exception("Please provice column and caption for export")
            End If

            Dim rowsPerSheet As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QuickExport, clsFixedParameterCode.MaxRowsForQuickExport, Nothing))

            Dim FilePath As String = "C:\ERPTempFolder"
            Dim IsExists As Boolean = System.IO.Directory.Exists(FilePath)
            If Not IsExists Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If
            strReportNameInSaveDialog += clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss")
            FilePath = "C:\ERPTempFolder\" + strReportNameInSaveDialog.Replace("/", "_").Replace("\\", "_") + ".xlsx"

            Dim intTotalRows As Integer = 0
            Dim intSheetCounter As Integer = 1
            Dim intReaderCounter As Integer = 0
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            Dim ResultsData As DataTable = Nothing
            Dim c As Integer = 0
            Dim firstTime As Boolean = True

            'Get the Columns names, types, this will help when we need to format the cells in the excel sheet.
            Dim dtSchema As DataTable = reader.GetSchemaTable()
            'Dim listCols = New List(Of DataColumn)()

            If dtSchema IsNot Nothing Then
                ResultsData = New DataTable()
                For Each drow As DataRow In dtSchema.Rows
                    Dim columnName As String = clsCommon.myCstr(drow("ColumnName"))
                    If arrVisibleColumAndCaption.ContainsKey(columnName) Then
                        Dim column = New DataColumn(columnName, DirectCast(drow("DataType"), Type))
                        column.Unique = CBool(drow("IsUnique"))
                        column.AllowDBNull = CBool(drow("AllowDBNull"))
                        column.AutoIncrement = CBool(drow("IsAutoIncrement"))
                        column.Caption = arrVisibleColumAndCaption(columnName)
                        'listCols.Add(column)
                        ResultsData.Columns.Add(column)
                    End If
                Next
            End If
            Dim rowData(rowsPerSheet, ResultsData.Columns.Count) As Object
            Dim workBook As Microsoft.Office.Interop.Excel.Workbook = Nothing

            While reader.Read()
                intReaderCounter += 1
                clsCommon.ProgressBarUpdate("Fatching Data for Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                For i As Integer = 0 To ResultsData.Columns.Count - 1
                    rowData(c, i) = reader(ResultsData.Columns(i).ColumnName)
                Next
                c += 1
                If c = rowsPerSheet Then
                    clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                    workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
                    c = 0
                    ResultsData.Clear()
                    firstTime = False
                    intSheetCounter += 1
                End If
            End While

            If c <> 0 Then

                clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
            End If

            workBook = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If intReaderCounter > 0 Then
                clsCommon.ProgressBarUpdate("Data exported.Opening File " + FilePath + ".Please wait...")
                Process.Start(FilePath)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Shared Function ExportToOxml(ByVal intSheetNo As Integer, ByVal firstTime As Boolean, ByVal RowsToWrite As Integer, ByVal Schema As DataTable, ByVal rawData(,) As Object, ByVal FilePath As String, ByRef wbook As Microsoft.Office.Interop.Excel.Workbook) As Microsoft.Office.Interop.Excel.Workbook
        Try
            Dim dt As New System.Data.DataTable()
            For i As Integer = 0 To Schema.Columns.Count - 1
                dt.Columns.Add("Column" & (i + 1))
                If clsCommon.myLen(Schema.Columns(i).Caption) > 0 Then
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).Caption
                Else
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).ColumnName
                End If
            Next

            Dim excel As New Microsoft.Office.Interop.Excel.Application
            If wbook Is Nothing Then
                Dim wBook1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
                wbook = wBook1
                wbook = excel.Workbooks.Add()
            Else
                wbook = excel.Workbooks.Open(FilePath)
            End If
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim GridCurrentRowIndex As Int64 = -1
            Dim GridLastSavedRowIndex As Int64 = -1
            wSheet = wbook.Sheets.Add(, , 1)
            wbook.ActiveSheet.Move(After:=wbook.Sheets(wbook.Sheets.Count))
            If firstTime Then
                Try
                    CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet2"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet3"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                Catch ex As Exception
                End Try
            End If
            wSheet.Name = "Sheet" & intSheetNo

            Dim jk As Integer = 0
            For i As Integer = 0 To Schema.Columns.Count - 1
                jk += 1
                Dim MyType As TypeCode = Type.GetTypeCode(Schema.Columns(i).DataType)
                If MyType = TypeCode.String Then
                    wSheet.Range(ColumnIndexToColumnLetter(jk) & ":" & ColumnIndexToColumnLetter(jk)).Cells.NumberFormat = "@"
                End If
            Next

            Dim colnum As Integer = -1
            Dim PrevCol As Integer = -1
            Dim ColNums(0 To Schema.Columns.Count - 1) As Integer

            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            Dim dc As System.Data.DataColumn
            colIndex = 0
            For Each dc In Schema.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex, colIndex) = dc.Caption
            Next

            Dim LastColumn As String = ColumnIndexToColumnLetter(Schema.Columns.Count)
            Dim Lastrow As Integer = RowsToWrite

            Dim row As Integer = 0
            Dim col As Integer = 0

            wSheet.Range("A2", LastColumn & (Lastrow + 1)).Value2 = rawData
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            wSheet.Columns.AutoFit()
            CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Select()
            excel.DisplayAlerts = False
            wbook.SaveAs(FilePath, , , , , , Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive)
            wbook.Close(True)

            excel.Quit()
            excel = Nothing
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return wbook
    End Function

    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function


    Private Sub LoadDataInGridViaDataReader(ByVal qry As String)
        'clsCommon.ProgressBarPercentShow()
        'clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            If reader Is Nothing OrElse Not reader.HasRows Then
                Throw New Exception("No Data found")
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dtTest As New DataTable
            dtTest.Load(reader)
            gv1.DataSource = dtTest
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Try
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
            TemplateGridview = gv1
            gv1.EnableFiltering = True
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
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
    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        If rbtnCategoryAll.IsChecked = True Then
            CheckedAll(gvCategory)
            gvCategory.Enabled = False
        Else
            UnCheckedAll(gvCategory)
            gvCategory.Enabled = True
        End If
    End Sub

    Private Sub rbtnCategorySelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategorySelect.ToggleStateChanged
        If rbtnCategoryAll.IsChecked = True Then
            CheckedAll(gvCategory)
            gvCategory.Enabled = False
        Else
            UnCheckedAll(gvCategory)
            gvCategory.Enabled = True
        End If
    End Sub

    Private Sub fromDate_ValueChanged(sender As Object, e As EventArgs) Handles fromDate.ValueChanged
        Dim strToDate As String = Nothing
        Dim To_Date As Date
        strToDate = "31/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(+1).Year)
        To_Date = clsCommon.myCDate(strToDate)
        Todate.Value = To_Date
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where location_type IN ('Physical','Virtual') "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub ddlReportType_SelectedValueChanged(sender As Object, e As EventArgs) Handles ddlReportType.SelectedValueChanged
        If clsCommon.CompairString(ddlReportType.SelectedText, "Product Wise") = CompairStringResult.Equal Then
            lblLocation.Visible = False
            txtLocation.Visible = False
        Else
            lblLocation.Visible = True
            txtLocation.Visible = True
        End If
    End Sub

    Private Sub RadMenuItemSett1_Click(sender As Object, e As EventArgs) Handles RadMenuItemSett1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

End Class
