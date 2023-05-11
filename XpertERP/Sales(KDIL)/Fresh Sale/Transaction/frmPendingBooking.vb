'-changes By--[Pankaj Kumar Chaudhary]--Against Ticket No -[BM00000002083]
Imports common
Public Class frmPendingBooking
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public LocCode As String = Nothing
    Public PriceCode As String = Nothing
    Public TaxCode As String = Nothing
    Public StartDate As Date = Nothing

    Public ArrReturn As List(Of clsDeliveryNoteFreshSaleDetail) = Nothing
    Dim dtAllData As DataTable = Nothing

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDIType As String = "IType"
    Const colDTaxGroup As String = "TAXGROUP"
    Const colDTaxGroupName As String = "TAXGROUPNAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDTaxRate1 As String = "TaxRate1"
    Const colDTaxRate2 As String = "TaxRate2"
    Const colDTaxRate3 As String = "TaxRate3"
    Const colDTaxRate4 As String = "TaxRate4"
    Const colDTaxRate5 As String = "TaxRate5"
    Const colDTaxRate6 As String = "TaxRate6"
    Const colDTaxRate7 As String = "TaxRate7"
    Const colDTaxRate8 As String = "TaxRate8"
    Const colDTaxRate9 As String = "TaxRate9"
    Const colDTaxRate10 As String = "TaxRate10"
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "ASSESSABLE"
    Const colDDisPer As String = "DISCOUNTPER"
    Const colDPriceCode As String = "colDPriceCode"
    Const colDPriceDate As String = "colDPriceDate"
    Const colDConvF As String = "colDConvF"


    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colSchemCode As String = "colSchemCode"
#End Region

    Private Sub frmPendingDelivery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setGridPropery()

        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        'Dim strwherecls As String = ""
        'Dim StrCondition As String = ""
        'Dim StrCondition1 As String = ""
        'strwherecls = Xtra.CustomerPermission()
        'If clsCommon.myLen(strwherecls) > 0 Then
        '    StrCondition = "   AND Final.Vendor IN (" + strwherecls + ")"
        '    StrCondition1 = "   WHERE Final.Vendor IN (" + strwherecls + ")"
        'Else
        '    StrCondition = ""
        '    StrCondition1 = ""
        'End If
        '-------------------------------------
        Dim qry As String = "select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName,MAX(Unit) AS Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,max(TransDate) as TransDate , " & _
        "SUM(Qty* case when RI=1 then 1 else 0 end) as DeliverQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as DispatchQty, SUM(Unapproved) as UnapprovedQty, " & _
        "SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName, " & _
        "MAX(MRP) as MRP,max(final.Price_Code) as Price_Code,max(price_date) as price_date,max(Conv_Factor) as Conv_Factor  from ( " + Environment.NewLine

        qry += "Select  TSPL_BOOKING_DETAIL.Line_No,TSPL_BOOKING_DETAIL.Document_No as Code,TSPL_BOOKING_DETAIL.Cust_Code as Vendor,  " & _
        "TSPL_BOOKING_DETAIL.Item_Code as ICode,Item_Desc as IName,TSPL_BOOKING_DETAIL.Booking_Qty as Qty, 0 as Unapproved, TSPL_ITEM_UOM_DETAIL.UOM_Code  as Unit, " & _
        "TSPL_BOOKING_DETAIL.Loc_Code as Location,1 as RI,IPM.Item_Basic_Price as Rate,1 as Chk,Document_Date as TransDate, IPM.Price_Code, " & _
        "IPM.Start_Date as Price_Date,TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Conv_Factor, isnull(IPM.Item_Basic_Net,0) as MRP   from " & _
        "TSPL_BOOKING_MATSER LEFT OUTER JOIN TSPL_BOOKING_DETAIL ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No LEFT OUTER JOIN ( " & _
        "Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code from ( " & _
        "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code, Start_Date,Item_Price_ID Desc) as RowNo, " & _
        "Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code from TSPL_ITEM_PRICE_MASTER  " & _
        "left  outer join  TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'     and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 " & _
        "and TSPL_ITEM_PRICE_MASTER.Price_Code='" & PriceCode & "'  " & _
        ") XXXE WHERE RowNo=1 " & _
        ") IPM ON IPM.Item_Code=TSPL_BOOKING_DETAIL.Item_Code left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join " & _
        "TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 " & _
        "where   Booking_Qty > 0 and TSPL_BOOKING_MATSER.posted=1 AND Loc_Code='" & LocCode & "' AND Cust_Code='" & VendorCode & "'"


        'qry += " select  TSPL_BOOKING_DETAIL.Line_No,TSPL_BOOKING_DETAIL.Document_No as Code,TSPL_BOOKING_DETAIL.Cust_Code as Vendor, " & _
        '"TSPL_BOOKING_DETAIL.Item_Code as ICode,Item_Desc as IName,TSPL_BOOKING_DETAIL.Booking_Qty as Qty, 0 as Unapproved, TSPL_ITEM_MASTER.Unit_Code as Unit, " & _
        '"TSPL_BOOKING_DETAIL.Loc_Code as Location,1 as RI,isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Price,0) as Rate,1 as Chk,Document_Date as TransDate, " & _
        '"TSPL_ITEM_PRICE_MASTER.Price_Code,TSPL_ITEM_PRICE_MASTER.Start_Date as Price_Date,TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Conv_Factor, " & _
        '"isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,0) as MRP  from TSPL_BOOKING_MATSER left outer join TSPL_BOOKING_DETAIL on  " & _
        '"TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No  left outer join TSPL_ITEM_MASTER on " & _
        '"TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_PRICE_MASTER  on " & _
        '"TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code   and TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_PRICE_MASTER.UOM and " & _
        '"Start_Date =(SELECT  TSPL_ITEM_PRICE_MASTER.Start_Date AS Start_Date FROM TSPL_ITEM_PRICE_MASTER INNER Join  " & _
        '"( SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, max(Item_Basic_Net) as Item_Basic_Net,  Price_Code, Tax_group " & _
        '"FROM TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") & "'   GROUP BY Item_Code, UOM,  Price_Code, Tax_group,Item_Baisc_Price )  AS groupedP  ON " & _
        '"TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
        '"TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND " & _
        '"TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  where " & _
        '"TSPL_ITEM_PRICE_MASTER.Tax_group='" & TaxCode & " '    and TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code and " & _
        '"TSPL_ITEM_PRICE_MASTER.Price_Code='" & PriceCode & "'  ) and Price_Code='" & PriceCode & "' and  Tax_group='" & TaxCode & "'  left  outer join  " & _
        '"TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        '"where Loc_Code='" & LocCode & "' and Cust_Code='" & VendorCode & "'  and Booking_Qty > 0  and TSPL_BOOKING_MATSER.posted=1  " + Environment.NewLine

        qry += " union all " + Environment.NewLine
        qry += "  select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No as Code, " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code as Vendor,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as ICode,'' as IName, " & _
        "isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty,0) as Qty,0 as Unapproved, TSPL_ITEM_UOM_DETAIL.UOM_Code   as Unit, " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate, " & _
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Date,0 AS Conv_Factor, " & _
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.MRP  from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " & _
        "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
        "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  " & _
        "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted=1  and  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code='" & LocCode & "' and " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & VendorCode & "'    " + Environment.NewLine
        qry += " union all   " + Environment.NewLine
        qry += "  select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No as Code, " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code as Vendor,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as ICode,'' as IName, " & _
        "0 as Qty,isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty,0) as Unapproved, TSPL_ITEM_UOM_DETAIL.UOM_Code   as Unit, " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate, " & _
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Date,0 AS Conv_Factor, " & _
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.MRP  from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " & _
        "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
"left outer join TSPL_ITEM_UOM_DETAIL on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  " & _
        "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted=0  and  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code='" & LocCode & "' and " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & VendorCode & "'   " + Environment.NewLine
        qry += " )Final " + Environment.NewLine
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine

        qry += " group by Code,ICode having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,max(Line_No)"
        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            If clsCommon.myLen(VendorName) > 0 Then
                common.clsCommon.MyMessageBoxShow("No record found for Customer " + VendorName + "")
            Else
                common.clsCommon.MyMessageBoxShow("No record found.")
            End If
            Me.Close()
        End If
        LoadHeadData()
        LoadBlankGridDetail()
    End Sub

    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))
            End If
        Next
        IsInsideLoadData = False
    End Sub

    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Delivery No"
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Customer"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Customer Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Delivery No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoScheme As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScheme.FormatString = ""
        repoScheme.HeaderText = "Scheme Code"
        repoScheme.Name = colSchemCode
        repoScheme.Width = 180
        repoScheme.ReadOnly = True
        repoScheme.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoScheme)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Deliver Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used Qty"
        repoAppQty.Name = colDApprovedQty
        repoAppQty.ReadOnly = True
        repoAppQty.Width = 100
        repoAppQty.WrapText = True
        repoAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAppQty)

        Dim repoUnAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnAppQty.FormatString = ""
        repoUnAppQty.HeaderText = "Unapproved Qty"
        repoUnAppQty.Name = colDUnApprovedQty
        repoUnAppQty.ReadOnly = True
        repoUnAppQty.Width = 80
        repoUnAppQty.WrapText = True
        repoUnAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnAppQty)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending"
        repoPendingQty.Name = colDPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.Width = 80
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = False
        repoMRP.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoMRP)


        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Conv. Factor"
        repoConv.Name = colDConvF
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.ReadOnly = False
        repoConv.IsVisible = False
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConv)

        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colDPriceCode
        repoPriceCode.IsVisible = True
        repoPriceCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceCode)


        Dim repoPriceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDate.Format = DateTimePickerFormat.Custom
        repoPriceDate.CustomFormat = "dd-MM-yyyy"
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.WrapText = True
        repoPriceDate.FormatString = "{0:d}"
        repoPriceDate.Name = colDPriceDate
        repoPriceDate.ReadOnly = True
        repoPriceDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoPriceDate)



        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Sub setGridPropery()
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        ''gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()
    End Sub

    Sub btnCancelPressed()
        Me.Close()
    End Sub

    Sub btnOKPressed()
        Dim dblBalQty As Double = 0
        ArrReturn = New List(Of clsDeliveryNoteFreshSaleDetail)
        Dim obj As clsDeliveryNoteFreshSaleDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsDeliveryNoteFreshSaleDetail()
                obj.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)

                obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
                obj.Conv_Factor = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDConvF).Value)
                obj.Price_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDPriceCode).Value)
                obj.Price_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDPriceDate).Value)
                obj.Amount = obj.Qty * obj.Rate
                If (obj.Balance_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Booking item")
        Else
            Me.Close()
        End If
    End Sub

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentColumn Is gv1.Columns(colDSelect) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Dim IsAllowSingleSI4SingleSO As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData("AllowSingleInvoiceAgainstSingleOrder", "AllowSingleInvoiceAgainstSingleOrder", Nothing)) = 1, True, False)
    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        If Not IsInsideLoadData Then
            If Not IsAllowSingleSI4SingleSO Then
                If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                    Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                    Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
                    If clsCommon.myLen(VendorCode) <= 0 Then
                        VendorCode = strVendorCode
                        VendorName = strVendorName
                    End If
                    If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
                        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                        If clsCommon.myLen(strCode) > 0 Then
                            LoadDetailData(e.NewValue, strCode)
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("Order's Customer should be `" + VendorName)
                        e.Cancel = True
                    End If
                End If
            Else
                gv1.Rows.Clear()
                Dim strCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                If e.NewValue = True Then
                    For Each grow As GridViewRowInfo In gvHead.Rows
                        grow.Cells(colHSelect).Value = False
                    Next
                    LoadDetailData(e.NewValue, strCode1)
                End If
            End If

        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("DeliverQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("DispatchQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDConvF).Value = clsCommon.myCdbl(dr("Conv_Factor"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPriceCode).Value = clsCommon.myCstr(dr("Price_code"))
                    If dr("Price_Date") IsNot DBNull.Value Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDPriceDate).Value = clsCommon.myCDate(dr("Price_Date"))
                    End If


                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
    End Sub
End Class

