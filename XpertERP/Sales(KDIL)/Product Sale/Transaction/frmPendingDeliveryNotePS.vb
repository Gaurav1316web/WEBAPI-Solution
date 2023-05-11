'-changes By--[Pankaj Kumar Chaudhary]--Against Ticket No -[BM00000002083]
Imports common
Public Class frmPendingDeliveryNotePS
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public LocationCode As String = Nothing
    Public VendorName As String = Nothing
    Dim ShowAllPendingDOIrrespectiveOfDeliveryDate As Boolean = False
    Public ShipLocCode As String = Nothing
    Public ShipLocName As String = Nothing
    Public strCurrCode As String = Nothing
    Public ArrReturn As List(Of clsPSDeliveryOrderDetail) = Nothing
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
    Const colDShipParty As String = "colDShipParty"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colSchemCode As String = "colSchemCode"
    Const colHLocation As String = "colHLocation"
    Const colHLocationName As String = "colHLocationName"
    Const colHShipParty As String = "colHShipParty"
    Const colHShipPartyName As String = "colHShipPartyName"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        setGridPropery()

        '' work to be done agaist ticket no.TEC/30/08/18-000317 
        ShowAllPendingDOIrrespectiveOfDeliveryDate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllPendingDOIrrespectiveOfDeliveryDate, clsFixedParameterCode.ShowAllPendingDOIrrespectiveOfDeliveryDate, Nothing)) = 1, True, False)
        '' End

        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim StrCondition As String = ""
        Dim StrCondition1 As String = ""
        strwherecls = Xtra.CustomerPermission()
        If clsCommon.myLen(strwherecls) > 0 Then
            StrCondition = "   AND Final.Vendor IN (" + strwherecls + ")"
            StrCondition1 = "   WHERE Final.Vendor IN (" + strwherecls + ")"
        Else
            StrCondition = ""
            StrCondition1 = ""
        End If
        '-------------------------------------
        Dim qry As String = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Unit,(Location) as Location,(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,Ship_Party,max(TSPL_CUSTOMER_MASTER_1.Customer_Name) as ShipPartyName, SUM(Qty* case when RI=1 then 1 else 0 end) as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Final.MRP as MRP ,max(Disc_Per) as Disc_Per,max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName,0 as Assessable,max(Scheme_Code) as Scheme_Code from ( " + Environment.NewLine
        qry += " select TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Ship_Party,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Line_No,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Document_Code as Code,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code as Vendor,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Row_Type as IType, TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Qty  as Qty,0 as Unapproved,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Unit_Code as Unit,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Location as Location,1 as RI,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Cost as Rate,1 as Chk,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Tax_Group,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX1_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX2_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX3_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX4_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX5_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX6_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX7_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX8_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX9_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.TAX10_Rate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.MRP ,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Disc_Per,Document_Date as TransDate,TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Assessable " + Environment.NewLine
        qry += " ,Scheme_Code from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE " + Environment.NewLine
        qry += " left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Document_Code  " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code" + Environment.NewLine
        qry += " where Scheme_Item='N'  and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted=1 and  isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.close_yn='N'  "

        If ShowAllPendingDOIrrespectiveOfDeliveryDate = False Then
            qry += " and Delivery_date >= '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' " + Environment.NewLine
        End If


        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code='" + VendorCode + "'"
        End If
        If clsCommon.myLen(LocationCode) > 0 Then
            qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Location ='" + LocationCode + "'"
        Else
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Location in (select Location_Code from TSPL_LOCATION_MASTER where Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N'  and  Location_Code in (" + objCommonVar.strCurrUserLocations + "))"
            End If
        End If
        qry += " union all " + Environment.NewLine
        qry += " select case when isnull(Ship_To_Party,'') <> '' and IsSameBillShipParty=0 then TSPL_SD_SHIPMENT_Head.Ship_To_Party else TSPL_SD_SHIPMENT_Head.Customer_Code end  as Ship_Party,TSPL_SD_SHIPMENT_DETAIL.Line_No,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,'' as IName,'' as IType,isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,TSPL_SD_SHIPMENT_DETAIL.Location as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
        qry += " ,Scheme_Code from TSPL_SD_SHIPMENT_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code" + Environment.NewLine
        qry += " where TSPL_SD_SHIPMENT_Head.Status=1 and Scheme_Item='N' and TSPL_SD_SHIPMENT_Head.Trans_Type='PS' and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,''))>0   " + Environment.NewLine
        qry += " union all   " + Environment.NewLine
        qry += " select case when isnull(Ship_To_Party,'') <> '' and IsSameBillShipParty=0 then TSPL_SD_SHIPMENT_Head.Ship_To_Party else TSPL_SD_SHIPMENT_Head.Customer_Code end  as Ship_Party,TSPL_SD_SHIPMENT_DETAIL.Line_No,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,'' as IName,'' as IType,0 as Qty,isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,TSPL_SD_SHIPMENT_DETAIL.Location as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,isnull(TSPL_SD_SHIPMENT_DETAIL.Assessable,0)as Assessable" + Environment.NewLine
        qry += " ,Scheme_Code from TSPL_SD_SHIPMENT_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code" + Environment.NewLine
        qry += " where TSPL_SD_SHIPMENT_Head.Status=0 and Scheme_Item='N' and TSPL_SD_SHIPMENT_Head.Trans_Type='PS' and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,''))>0 and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')  " + Environment.NewLine
        qry += " )Final " + Environment.NewLine
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " + Environment.NewLine
        qry += "   left outer join TSPL_CUSTOMER_MASTER as TSPL_CUSTOMER_MASTER_1 on TSPL_CUSTOMER_MASTER_1.Cust_Code=final.Ship_Party  " + Environment.NewLine

        If IsAllowSingleSI4SingleSO = True Then
            qry += " WHERE Code Not in (Select Distinct Order_Code from TSPL_SD_SHIPMENT_DETAIL WHERE ISNULL(Order_Code,'')<>'') " + StrCondition + " "
        Else
            qry += "" + StrCondition1 + "  "
        End If
        qry += " group by Code,ICode,Unit,MRP,Location,Location_Desc,Ship_Party having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by Code,Ship_Party,Location,max(Line_No)"
        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            If clsCommon.myLen(VendorName) > 0 Then
                common.clsCommon.MyMessageBoxShow("No record found for vendor " + VendorName + "")
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
            Dim strLocCode As String = clsCommon.myCstr(dr("code")) + clsCommon.myCstr(dr("Location")) + clsCommon.myCstr(dr("Ship_Party"))
            If Not arr.Contains(strLocCode) Then
                arr.Add(strLocCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHLocation).Value = clsCommon.myCstr(dr("Location"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHLocationName).Value = clsCommon.myCstr(dr("LocationName"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHShipParty).Value = clsCommon.myCstr(dr("Ship_Party"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHShipPartyName).Value = clsCommon.myCstr(dr("ShipPartyName"))
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
        repoCode.HeaderText = "Order No"
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

        Dim repShipParty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repShipParty.FormatString = ""
        repShipParty.HeaderText = "Ship Party"
        repShipParty.Name = colHShipParty
        repShipParty.Width = 100
        repShipParty.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repShipParty)

        Dim repShipPartyName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repShipPartyName.FormatString = ""
        repShipPartyName.HeaderText = "Ship Party Name"
        repShipPartyName.Name = colHShipPartyName
        repShipPartyName.Width = 150
        repShipPartyName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repShipPartyName)

        Dim repoLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLoc.FormatString = ""
        repoLoc.HeaderText = "Location"
        repoLoc.Name = colHLocation
        repoLoc.Width = 100
        repoLoc.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoLoc)

        Dim repoLocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocName.FormatString = ""
        repoLocName.HeaderText = "Location Name"
        repoLocName.Name = colHLocationName
        repoLocName.Width = 150
        repoLocName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoLocName)

   
        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Customer"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 100
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Customer Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 150
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
        repoCode.HeaderText = "Order No"
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

        Dim repoShipParty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShipParty.FormatString = ""
        repoShipParty.HeaderText = "Ship party"
        repoShipParty.Name = colDShipParty
        repoShipParty.Width = 180
        repoShipParty.ReadOnly = True
        repoShipParty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoShipParty)


        Dim repoScheme As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScheme.FormatString = ""
        repoScheme.HeaderText = "Scheme Code"
        repoScheme.Name = colSchemCode
        repoScheme.Width = 180
        repoScheme.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoScheme)

        Dim repoIType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIType.FormatString = ""
        repoIType.HeaderText = "Row Type"
        repoIType.Name = colDIType
        repoIType.Width = 180
        repoIType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIType)

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
        repoRate.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Qty"
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


        Dim repoTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxCode.FormatString = ""
        repoTaxCode.HeaderText = "Tax Group Code"
        repoTaxCode.Name = colDTaxGroup
        repoTaxCode.Width = 100
        repoTaxCode.ReadOnly = True
        repoTaxCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxCode)

        Dim repoTaxName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxName.FormatString = ""
        repoTaxName.HeaderText = "Tax Group"
        repoTaxName.Name = colDTaxGroupName
        repoTaxName.Width = 100
        repoTaxName.ReadOnly = True
        repoTaxName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxName)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colDTaxRate1
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.IsVisible = False
        repoTaxRate1.WrapText = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colDTaxRate2
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.IsVisible = False
        repoTaxRate2.WrapText = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colDTaxRate3
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.IsVisible = False
        repoTaxRate3.WrapText = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colDTaxRate4
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.IsVisible = False
        repoTaxRate4.WrapText = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colDTaxRate5
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.IsVisible = False
        repoTaxRate5.WrapText = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colDTaxRate6
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.IsVisible = False
        repoTaxRate6.WrapText = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colDTaxRate7
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.IsVisible = False
        repoTaxRate7.WrapText = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colDTaxRate8
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.IsVisible = False
        repoTaxRate8.WrapText = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colDTaxRate9
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.IsVisible = False
        repoTaxRate9.WrapText = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colDTaxRate10
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.IsVisible = False
        repoTaxRate10.WrapText = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = False
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessable.FormatString = ""
        repoAssessable.HeaderText = "Assessable"
        repoAssessable.Name = colDAssessable
        repoAssessable.ReadOnly = True
        repoAssessable.IsVisible = False
        repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessable)




        Dim repoDiscPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscPer.FormatString = ""
        repoDiscPer.HeaderText = "Discount Per"
        repoDiscPer.Name = colDDisPer
        repoDiscPer.ReadOnly = True
        repoDiscPer.IsVisible = False
        repoDiscPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDiscPer)

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
        ArrReturn = New List(Of clsPSDeliveryOrderDetail)
        Dim obj As clsPSDeliveryOrderDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsPSDeliveryOrderDetail()
                obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                obj.Ship_Party = clsCommon.myCstr(gv1.Rows(ii).Cells(colDShipParty).Value)
                ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)



                'obj.MRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
                obj.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate1).Value)
                obj.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate2).Value)
                obj.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate3).Value)
                obj.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate4).Value)
                obj.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate5).Value)
                obj.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate6).Value)
                obj.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate7).Value)
                obj.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate8).Value)
                obj.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate9).Value)
                obj.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate10).Value)
                obj.Disc_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)

                obj.Assessable = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAssessable).Value)
                obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                If (obj.Balance_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Sale Order item")
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

                    Dim strShipLocCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHLocation).Value)
                    Dim strShipLocName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHLocationName).Value)
                    'VendorCode = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                    'ShipLocCode = strShipLocCode1
                    Dim strShipPartyCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHShipParty).Value)
                    Dim strShipPartyCodeName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHShipPartyName).Value)
                    If clsCommon.myLen(VendorCode) <= 0 Then
                        'VendorCode = strVendorCode
                        'VendorName = strVendorName
                        VendorCode = strShipPartyCode1
                        VendorName = strShipPartyCodeName

                        ShipLocCode = strShipLocCode1
                        ShipLocName = strShipLocName
                    End If
                    If clsCommon.CompairString(strShipPartyCode1, VendorCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strShipLocCode1, ShipLocCode) = CompairStringResult.Equal Then
                        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                        Dim strLocCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHLocation).Value)
                        Dim strShipPartyCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHShipParty).Value)

                        If clsCommon.myLen(strCode) > 0 Then
                            LoadDetailData(e.NewValue, strCode, strLocCode, strShipPartyCode)
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("Order's Customer should be `" + VendorName + " Location should be `" + strShipLocCode1)
                        e.Cancel = True
                    End If
                End If
            Else
                gv1.Rows.Clear()
                Dim strCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                Dim strLocCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHLocation).Value)
                Dim strShipPartyCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHShipParty).Value)
                If e.NewValue = True Then
                    For Each grow As GridViewRowInfo In gvHead.Rows
                        grow.Cells(colHSelect).Value = False
                    Next
                    LoadDetailData(e.NewValue, strCode1, strLocCode1, strShipPartyCode1)
                End If
            End If

        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String, ByVal strLocCode As String, ByVal strShipPartyCode As String)
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strLocCode, clsCommon.myCstr(dr("Location"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strShipPartyCode, clsCommon.myCstr(dr("Ship_Party"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDShipParty).Value = clsCommon.myCstr(dr("Ship_Party"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIType).Value = clsCommon.myCstr(dr("IType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroupName).Value = clsCommon.myCstr(dr("TaxGroupName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate1).Value = clsCommon.myCdbl(dr("TAX1_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate2).Value = clsCommon.myCdbl(dr("TAX2_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate3).Value = clsCommon.myCdbl(dr("TAX3_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate4).Value = clsCommon.myCdbl(dr("TAX4_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate5).Value = clsCommon.myCdbl(dr("TAX5_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate6).Value = clsCommon.myCdbl(dr("TAX6_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate7).Value = clsCommon.myCdbl(dr("TAX7_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate8).Value = clsCommon.myCdbl(dr("TAX8_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate9).Value = clsCommon.myCdbl(dr("TAX9_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate10).Value = clsCommon.myCdbl(dr("TAX10_Rate"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))



                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDDisPer).Value = clsCommon.myCdbl(dr("Disc_Per"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemCode).Value = clsCommon.myCstr(dr("Scheme_Code"))

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

