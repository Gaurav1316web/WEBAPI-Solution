'-changes By--[Pankaj Kumar Chaudhary]--Against Ticket No -[BM00000002083]
Imports common
Public Class frmPendingDeliveryForGatePass
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public VehicleCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public LocCode As String = Nothing
    Public PriceCode As String = Nothing
    Public TaxCode As String = Nothing
    Public StartDate As Date = Nothing

    Public ArrReturn As List(Of clsDeliveryNoteDairySaleDetail) = Nothing
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
    Const colDSellingRate As String = "colDSellingRate"
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
    'Const colSchemeIUOM As String = "colSchemeIUOM"
    'Const colSchemeIQty As String = "colSchemeIQty"
    'Const colSchemeICode As String = "colSchemeICode"
    'Const colSchemeICodeDes As String = "colSchemeICodeDes"
    'Const ColFOC As String = "ColFOC"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colSchemCode As String = "colSchemCode"
    Const colSchemType As String = "colSchemType"
#End Region

    Private Sub frmPendingDelivery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setGridPropery()

        '-------------------------------------
        'Dim qry As String = "select CAST(0 as bit) as Sel,code,ICode,max(Item_Desc) as IName,MAX(Unit) AS Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,max(TransDate) as TransDate , " & _
        '"SUM(final.Qty* case when RI=1 then 1 else 0 end) as DeliverQty, SUM(final.Qty* case when RI=-1 then 1 else 0 end) as DispatchQty, SUM(Unapproved) as UnapprovedQty, " & _
        '"SUM((final.Qty *RI)- Unapproved) as PedningQty ,MAX(final.Rate) as Rate,MAX(final.Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName, " & _
        '"MAX(MRP) as MRP,max(final.Price_Code) as Price_Code,max(price_date) as price_date,max(Conv_Factor) as Conv_Factor  from ( " + Environment.NewLine

        'qry += "select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as Code, " & _
        '"TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code as Vendor,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as ICode,'' as IName, " & _
        '"isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty,0) as Qty,0 as Unapproved, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code   as Unit, " & _
        '"TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code as Location,1 as RI,Item_Selling_Price as Rate,1 as Chk,Document_Date as TransDate, " & _
        '"TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Date,0 AS Conv_Factor, " & _
        '"TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.MRP  from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " & _
        '"left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
        '"where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted=1  and location_code in (select Location_Code from TSPL_LOCATION_MASTER where	DairyDispatchFromDO=0)  " + Environment.NewLine
        'If clsCommon.myLen(VendorCode) > 0 Then
        '    qry += "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & VendorCode & "'    " + Environment.NewLine
        'End If
        'If clsCommon.myLen(LocCode) > 0 Then
        '    qry += "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code='" & LocCode & "'    " + Environment.NewLine
        'End If
        'If clsCommon.myLen(VehicleCode) > 0 Then
        '    qry += "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.lorry_no='" & VehicleCode & "'    " + Environment.NewLine
        'End If
        'qry += " union all " + Environment.NewLine
        'qry += "  select TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as Code, " & _
        '"TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code as Vendor,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code as ICode,'' as IName, " & _
        '"isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty,0) as Qty,0 as Unapproved, TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code   as Unit, " & _
        '"TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code as Location,-1 as RI, Rate,0 as Chk,null as TransDate, " & _
        '"TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Date,0 AS Conv_Factor, " & _
        '"TSPL_GATEPASS_DETAIL_DAIRYSALE.MRP  from TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
        '"left outer join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
        '"where TSPL_GATEPASS_MASTER_DAIRYSALE.Posted=1     " + Environment.NewLine
        'qry += " union all   " + Environment.NewLine
        'qry += "  select TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as Code, " & _
        '"TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code as Vendor,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code as ICode,'' as IName, " & _
        '"0 as Qty,isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty,0) as Unapproved, TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code   as Unit, " & _
        '"TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code as Location,-1 as RI, Rate,0 as Chk,null as TransDate, " & _
        '"TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Date,0 AS Conv_Factor, " & _
        '"TSPL_GATEPASS_DETAIL_DAIRYSALE.MRP  from TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
        '"left outer join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
        '"where TSPL_GATEPASS_MASTER_DAIRYSALE.Posted=0   " + Environment.NewLine
        'qry += " )Final " + Environment.NewLine
        'qry += "  left outer join tspl_item_master on tspl_item_master.Item_Code=Final.ICode " + Environment.NewLine
        'qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
        'qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine

        'qry += " group by Code,ICode having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,max(Line_No)"
        'dtAllData = clsDBFuncationality.GetDataTable(qry)
        'If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
        '    If clsCommon.myLen(VendorName) > 0 Then
        '        common.clsCommon.MyMessageBoxShow("No record found for Customer " + VendorName + "")
        '    Else
        '        common.clsCommon.MyMessageBoxShow("No record found.")
        '    End If
        '    Me.Close()
        'End If
        'LoadHeadData()
        'LoadBlankGridDetail()


        Dim qry As String = "select CAST(0 as bit) as Sel,code,ICode,max(Item_Desc) as IName,max(Scheme_Code) as 'Scheme Code',MAX(Unit) AS Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,max(TransDate) as TransDate , " & _
        "SUM(final.Qty* case when RI=1 then 1 else 0 end) as DeliverQty, SUM(final.Qty* case when RI=-1 then 1 else 0 end) as DispatchQty, SUM(Unapproved) as UnapprovedQty, " & _
        "SUM((final.Qty *RI)- Unapproved) as PedningQty ,MAX(final.Rate) as Rate,MAX(final.Item_Selling_Price) as 'Selling Rate',MAX(final.Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName, " & _
        "MAX(MRP) as MRP,max(final.Price_Code) as Price_Code,max(price_date) as price_date,max(Conv_Factor) as Conv_Factor  from ( " + Environment.NewLine

        qry += "select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Selling_Price ,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Type,isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item_Code,'') as 'Scheme_Item_Code',isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Qty,'') as 'Scheme_Qty',isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item_UOM,'') as 'Scheme_Item_UOM',isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.FOC_Item,'0') as 'FOC_Item',TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as Code, " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code as Vendor,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as ICode,'' as IName, " & _
        "isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty,0) as Qty,0 as Unapproved, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code   as Unit, " & _
        "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code as Location,1 as RI,Item_Selling_Price as Rate,1 as Chk,Document_Date as TransDate, " & _
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Date,0 AS Conv_Factor, " & _
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.MRP  from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " & _
        "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No  " & _
        "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted=1 and isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.FOC_Item,'0')<>'1'  and location_code in (select Location_Code from TSPL_LOCATION_MASTER where	DairyDispatchFromDO=0)  " + Environment.NewLine
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & VendorCode & "'    " + Environment.NewLine
        End If
        If clsCommon.myLen(LocCode) > 0 Then
            qry += "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code='" & LocCode & "'    " + Environment.NewLine
        End If
        If clsCommon.myLen(VehicleCode) > 0 Then
            qry += "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.lorry_no='" & VehicleCode & "'    " + Environment.NewLine
        End If
        qry += " union all " + Environment.NewLine
        qry += "  select TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Selling_Price,TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Type,isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_Code,'') as 'Scheme_Item_Code',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Qty,'') as 'Scheme_Qty',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_UOM,'') as 'Scheme_Item_UOM',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item,'0') as 'FOC_Item',TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as Code, " & _
        "TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code as Vendor,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code as ICode,'' as IName, " & _
        "isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty,0) as Qty,0 as Unapproved, TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code   as Unit, " & _
        "TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code as Location,-1 as RI, Rate,0 as Chk,null as TransDate, " & _
        "TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Date,0 AS Conv_Factor, " & _
        "TSPL_GATEPASS_DETAIL_DAIRYSALE.MRP  from TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
        "left outer join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
        "where TSPL_GATEPASS_MASTER_DAIRYSALE.Posted=1 and  isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item,'0')<>'1'     " + Environment.NewLine
        qry += " union all   " + Environment.NewLine
        qry += "  select TSPL_GATEPASS_DETAIL_DAIRYSALE.Line_No,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Selling_Price,TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Type,isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_Code,'') as 'Scheme_Item_Code',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Qty,'') as 'Scheme_Qty',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item_UOM,'') as 'Scheme_Item_UOM',isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item,'0') as 'FOC_Item',TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as Code, " & _
        "TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code as Vendor,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code as ICode,'' as IName, " & _
        "0 as Qty,isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty,0) as Unapproved, TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code   as Unit, " & _
        "TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code as Location,-1 as RI, Rate,0 as Chk,null as TransDate, " & _
        "TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Price_Date,0 AS Conv_Factor, " & _
        "TSPL_GATEPASS_DETAIL_DAIRYSALE.MRP  from TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
        "left outer join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " & _
        "where TSPL_GATEPASS_MASTER_DAIRYSALE.Posted=0 and  isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item,'0')<>'1'  " + Environment.NewLine
        qry += " )Final " + Environment.NewLine
        qry += "  left outer join tspl_item_master on tspl_item_master.Item_Code=Final.ICode " + Environment.NewLine
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine

        qry += " group by Code,ICode,Unit having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,max(Line_No)"
        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            If clsCommon.myLen(VendorName) > 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No record found for Customer " + VendorName + "", Me.Text)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No record found.", Me.Text)
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

        Dim repoSchemeType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeType.FormatString = ""
        repoSchemeType.HeaderText = "Scheme Type"
        repoSchemeType.Name = colSchemType
        repoSchemeType.Width = 180
        repoSchemeType.ReadOnly = True
        repoSchemeType.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSchemeType)


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

        Dim repoSellingRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSellingRate.FormatString = ""
        repoSellingRate.HeaderText = "Selling Rate"
        repoSellingRate.Name = colDSellingRate
        repoSellingRate.ReadOnly = True
        repoSellingRate.IsVisible = True
        repoSellingRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSellingRate)


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


        'Dim repoIsSchmItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoIsSchmItemCode.FormatString = ""
        'repoIsSchmItemCode.HeaderText = "Scheme Item Code"
        'repoIsSchmItemCode.Name = colSchemeICode
        'repoIsSchmItemCode.Width = 50
        'repoIsSchmItemCode.IsVisible = True
        'gv1.MasterTemplate.Columns.Add(repoIsSchmItemCode)




        'Dim repoIsSchmItemQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoIsSchmItemQty.FormatString = ""
        'repoIsSchmItemQty.HeaderText = "Scheme Item Qty"
        'repoIsSchmItemQty.Name = colSchemeIQty
        'repoIsSchmItemQty.Width = 50
        'repoIsSchmItemQty.IsVisible = False
        'gv1.MasterTemplate.Columns.Add(repoIsSchmItemQty)

        'Dim repoIsSchmItemUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoIsSchmItemUOM.FormatString = ""
        'repoIsSchmItemUOM.HeaderText = "Scheme Item UOM"
        'repoIsSchmItemUOM.Name = colSchemeIUOM
        'repoIsSchmItemUOM.Width = 50
        'repoIsSchmItemUOM.IsVisible = False
        'gv1.MasterTemplate.Columns.Add(repoIsSchmItemUOM)

        'Dim repoFOC As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoFOC.FormatString = ""
        'repoFOC.HeaderText = "FOC"
        'repoFOC.Name = ColFOC
        'repoFOC.ReadOnly = True
        'repoFOC.IsVisible = False
        'gv1.MasterTemplate.Columns.Add(repoFOC)



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
        ArrReturn = New List(Of clsDeliveryNoteDairySaleDetail)
        Dim obj As clsDeliveryNoteDairySaleDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsDeliveryNoteDairySaleDetail()
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
                obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                obj.Scheme_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemType).Value)
                'obj.Scheme_Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeICode).Value)
                'obj.Scheme_Qty = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeIQty).Value)
                'obj.Scheme_Item_UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeIUOM).Value)
                'obj.FOC_Item = clsCommon.myCstr(gv1.Rows(ii).Cells(ColFOC).Value)
                obj.SellingPrice = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDSellingRate).Value)
                obj.Amount = obj.Qty * obj.Rate
                If (obj.Balance_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending Delivery item", Me.Text)
        Else
            Me.Close()
        End If
        'Dim dblBalQty As Double = 0
        'ArrReturn = New List(Of clsDeliveryNoteDairySale)
        'Dim obj As clsDeliveryNoteDairySale = Nothing
        'For ii As Integer = 0 To gv1.RowCount - 1
        '    If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
        '        obj = New clsDeliveryNoteDairySale()
        '        obj.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
        '        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
        '        obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
        '        obj.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
        '        obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)

        '        obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
        '        obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
        '        obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
        '        obj.Conv_Factor = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDConvF).Value)
        '        obj.Price_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDPriceCode).Value)
        '        obj.Price_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDPriceDate).Value)
        '        obj.Amount = obj.Qty * obj.Rate
        '        If (obj.Balance_Qty > 0) Then
        '            ArrReturn.Add(obj)
        '        End If
        '    End If
        'Next

        'If ArrReturn.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Booking item")
        'Else
        '    Me.Close()
        'End If
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
            gv1.Rows.Clear()
            Dim strCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
            If e.NewValue = True Then
                For Each grow As GridViewRowInfo In gvHead.Rows
                    grow.Cells(colHSelect).Value = False
                Next
                LoadDetailData(e.NewValue, strCode1)
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemCode).Value = clsCommon.myCstr(dr("Scheme Code"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemType).Value = clsCommon.myCstr(dr("Scheme Type"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIQty).Value = clsCommon.myCstr(dr("Scheme Item Qty"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeICode).Value = clsCommon.myCstr(dr("Scheme Item Code"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIUOM).Value = clsCommon.myCstr(dr("Scheme Item UOM"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = clsCommon.myCstr(dr("FOC"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSellingRate).Value = clsCommon.myCdbl(dr("Selling Rate"))
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

