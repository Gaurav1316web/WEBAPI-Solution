''''' for bug no BM00000000904
Imports XpertERPEngine
Imports common
Imports Telerik.WinControls.UI

Public Class FrmDistrbutorSaleTarget
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim refreshGrid As String = "F"
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDistrbutorSaleTarget)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub



    Private Sub FrmDistrbutorSaleTarget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(False, 2)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmDistrbutorSaleTarget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        chkcategoryall.IsChecked = True
        chkCustomerAll.IsChecked = True
        chkLocAll.IsChecked = True
        chkRouteAll.IsChecked = True
        LoadCustomerType()
        LoadCustomer()
        LoadLocation()
        LoadCustomerCategory()
        LoadRoute()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        ddlconversion.SelectedIndex = 1
        rdbCustDetail.Visible = True
        SetDataBaseGrid()
        LoadTemplate()
        chktempall.IsChecked = True

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        rdoposted.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next


    End Sub

    Sub LoadLocation()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub


    ''''Added By---Pankaj ON 04/02/2012
    Sub LoadCustomerCategory()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        cbgcategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgcategory.ValueMember = "Code"
        cbgcategory.DisplayMember = "Name"
    End Sub


    Sub LoadCustomerType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Normal"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Credit"
        dt.Rows.Add(dr)

        cboCustomerType.DataSource = dt
        cboCustomerType.ValueMember = "Code"
        cboCustomerType.DisplayMember = "Name"
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master WHERE 2=2 "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    '''' Added By----Pankaj Kumar---On----date-03/03/2012
    Sub LoadRoute()
        Dim strquery As String = "select Route_No as [Route No], Route_Desc as [Description] from TSPL_ROUTE_MASTER WHERE 1=1 "
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgRoute.ValueMember = "Route No"
        cbgRoute.DisplayMember = "Description"
    End Sub


    Sub print(ByVal chk As Boolean, ByVal exporter As EnumExportTo)
        Dim qryqty, strConverted, strConvertedRet, strReturnQty As String
        Dim conversion As String
        Dim str As String = "Net Sale Detail Report"
        Dim head1 As String = ""
        Dim head2 As String
        strConverted = ""
        qryqty = ""
        strConvertedRet = ""
        strReturnQty = ""
        Try
            Dim postingdata As String = ""
            ' Dim qryconqty As String
            ' Dim qtyReturn As String
            If rdoposted.IsChecked = True Then
                postingdata = "('Y')"
            ElseIf rdoalldata.IsChecked = True Then
                postingdata = "('Y','N')"
            End If

            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least Single 'Customer'")
            ElseIf chkcategoryselect.IsChecked AndAlso cbgcategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least Single 'Customer Class'")
            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least Single 'Location'")
            ElseIf chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least Single 'Route'")
            End If

            If ddlconversion.Text = "Converted" Then
                conversion = "Converted"
                qryqty = "isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code= 'FB' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code= 'con' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)  AS Invoice_Qty "
                strReturnQty = "- (isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0))  as Invoice_Qty  "
            ElseIf ddlconversion.Text = "Raw" Then
                conversion = "Raw"
                qryqty = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )  AS Invoice_Qty "
                strReturnQty = " - ((case when TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ) )   as Invoice_Qty  "
            ElseIf ddlconversion.Text = "8oz" Then
                conversion = "8oz"
                qryqty = " isnull((((select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code= 'fb' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code = 'con' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) ) * (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code = '8oz' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)  * (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0)) * (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0)  AS Invoice_Qty "
                strReturnQty = " - (isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) ) as   Invoice_Qty "
            End If




            If ddlconversion.Text = "Converted" Then
                strConverted = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='Con')"
                strConvertedRet = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='Con')"
            ElseIf ddlconversion.Text = "8oz" Then
                strConverted = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='Con') * (select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz')"
                strConvertedRet = "(isnull((select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/(select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='Con') * (select TSPL_ITEM_UOM_DETAIL.Conversion_Factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz')"
            ElseIf ddlconversion.Text = "Raw" Then
                strConverted = "1"
                strConvertedRet = "1"
            End If

            Dim strSeq, strItemSale, strItemRet As String
            If rdbSKU.IsChecked Then
                strSeq = "Sku_Seq as Seq"
                strItemSale = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strItemRet = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code "
            Else
                strSeq = "Pack_Seq  as Seq"
                strItemSale = "Class_Desc"
                strItemRet = "Class_Desc"
            End If

            Dim qry As String = " select convert(decimal(18,2),MRPCase) as MRPCase,convert(decimal(18,2),MRPBottle) as MRPBottle,Seq,[Trade Margin],[Dist Margin],case when  (Discount_Code)='' and (Scheme_Item)='Y' then sum(DiscAmt) else 0 end  as TradeDiscAMt," & _
            "case when  (Discount_Code) <> '' and (Scheme_Item)='Y' then sum(DiscAmt) else 0 end  as TargetDiscAMt, " & _
            "case when  (Scheme_Item)='Y' then sum(DiscAmt) else 0 end  as TotDiscAMt, " & _
            "case when  (Discount_Code)='' and (Scheme_Item)='Y' then sum(Invoice_Qty) else 0 end  as TradeDiscQty, " & _
            "case when  (Discount_Code) <> '' and (Scheme_Item)='Y' then sum(Invoice_Qty) else 0 end  as TargetDiscQty, "
            qry += " max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,sale_Invoice_No,"
            qry += " max(Sale_Invoice_Date) as Sale_Invoice_Date,"
            qry += " MAX(Cust_Code) as Cust_Code,"
            'qry += " max(Cust_Name) as Cust_Name,Item_Code,MAX(Item_Desc) as Item_Desc,"
            qry += " max(Cust_Name) as Cust_Name,Item_Code,MAX(Item_Desc) as Item_Desc,MAX(Customer_Class ) as [Customer Class],"  '''' Added by Pankaj
            qry += " SUM(Invoice_Qty) as GrossSale,"
            qry += " SUM(Invoice_Qty * case when Scheme_Item='Y' then 1 else 0 end) as Trade_Disc,"
            qry += " MAX(case when Scheme_Item='N' then ( TPT+Item_Net_Amt+TAX1_Amt+TAX2_Amt+TAX3_Amt+TAX4_Amt) else 0  end) as Rate,"
            qry += " Unit_code,MRP_Amt,"
            qry += " max(case when Scheme_Item='N' then Item_Net_Amt else 0 end) as Basic_Rate ,"
            qry += " max(case when Scheme_Item='N' then TAX1_Amt else 0 end) as TAX1_Amt,"
            qry += " max(case when Scheme_Item='N' then TAX2_Amt+TAX3_Amt else 0 end) as TAX3_Amt,"
            qry += " max(case when Scheme_Item='N' then TAX4_Amt else 0 end) as TAX4_Amt,"

            qry += " max(case when Scheme_Item='N' then TAX1_Rate else 0 end) as TAX1_Rate,"
            qry += " max(case when Scheme_Item='N' then TAX2_Rate+TAX3_Rate else 0 end) as TAX3_Rate,"
            qry += " max(case when Scheme_Item='N' then TAX4_Rate else 0 end) as TAX4_Rate, "
            qry += " sum(case when Scheme_Item='N' then TPT else 0  end) as TPT, MAX(Location) as Location ,sum(Total_Amt)as Total_Amt,Price_To_Show ,sum (SaleAccAmt) as SaleAccAmt,Comp_Code ,sum (cust_item_discount_NoTax) as cust_item_discount_NoTax "

            qry += " from( "
            qry += " select MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase,  " & _
            "case when TSPL_SALE_INVOICE_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end  as MRPBottle," & strSeq & "," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS [Trade Margin]," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 AS [Dist Margin],Discount_Code,CASE WHEN Scheme_Item = 'Y'  THEN ((Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) * " & strConverted & " ELSE 0 END as DiscAmt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location , " & _
            "" & strItemSale & " as Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc, ( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N'then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt else 0 end ) * " & strConverted & " as Total_Amt, "
            qry += " case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code<> 'FC' then 'FC' else " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code end as Unit_code,isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt,0) * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as MRP_Amt,                " + qryqty + "              , isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Cust_Discount,0) as Cust_Discount, isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Disc_Amt,0) as Disc_Amt ,  round ( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='fc' then case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt,0) else 0 end else ( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt,0) else 0 end)*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2) as TAX1_Amt, round ( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='fc' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2_Amt,0) else (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2_Amt,0)  )*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2)  as TAX2_Amt,  round ( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='fc' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3_Amt,0) else (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3_Amt,0)  )*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2)  as TAX3_Amt ,round( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code='fc' then case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4_Amt,0) else isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt,0) end else ( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4_Amt,0) else isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Amt,0) end)*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2)  as TAX4_Amt, case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Rate,0) else 0 end as TAX1_Rate,"
            qry += " isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX2_Rate,0) as TAX2_Rate, "
            qry += " isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX3_Rate,0) as TAX3_Rate ,"
            qry += " case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX4_Rate,0) else isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TAX1_Rate,0) end as TAX4_Rate, isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.TPT,0) as TPTold, case when (select COUNT(*)  from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL as s1 where s1.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and s1.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code )>1  then "
            qry += "  case when (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code)='fc' then "
            qry += "  (select  top 1 s3.tpt from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL as s3 where s3.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and s3.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and s3.Unit_code='fc'  )else 0 end  else"
            qry += " case when (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code)='fc' then (select s4.tpt from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL as s4 where s4.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and s4.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and s4.Unit_code='fc'  ) else   round ((select s2.tpt from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL as s2 where s2.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and s2.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and s2.Unit_code='fb'  ) * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) end end tpt,(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate,0) * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Basic_Rate,Item_Net_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class ,round(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_To_Show * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) as Price_To_Show ,  ( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Account_Amount is null then 0 else " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Account_Amount end ) as SaleAccAmt ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.cust_item_discount_NoTax  "
            qry += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL "
            qry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No "
            qry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code "
            qry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code "
            qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code "
            qry += " left outer join TSPL_ITEM_DETAILS on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_DETAILS.Item_Code "
            qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ITEM_UOM_DETAIL_1.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code "
            qry += " where  TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and TSPL_ITEM_DETAILS.Class_Name='size' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post in " + postingdata + " "


            If chktempall.IsChecked = True Then

                If chkCustomerSelect.IsChecked = True Then
                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkCustomerSelect.IsChecked = True Then
                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkcategoryselect.IsChecked AndAlso cbgcategory.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class IN (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If
            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If




            qry += "  Union All "
            qry += "  select MRP_Amt*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            " case when TSPL_SALE_Return_DETAIL.Unit_Code='FC' then  MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else MRP_Amt end  as MRPBottle," & strSeq & "," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 AS [Trade Margin]," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount4 AS [Dist Margin],Discount_Code, -(CASE WHEN Scheme_Item = 'Y'  THEN ((Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END)  * " & strConvertedRet & "  AS DiscAMt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No , " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date as Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Name," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location , " & _
           "" & strItemRet & " as Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Desc, "
            qry += "  -1 *( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Scheme_Item='N'then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Item_Amt else 0 end ) * " & strConvertedRet & " as Total_Amt,  case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code<> 'FC' then 'FC' else " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code end as Unit_code,isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt,0) * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as MRP_Amt,             " + strReturnQty + "        , isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Cust_Discount,0) as Cust_Discount, isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Disc_Amt,0) as Disc_Amt , "
            qry += "  round ( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='fc' then case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Amt,0) else 0 end else ( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Amt,0) else 0 end)*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2) as TAX1_Amt, "
            qry += " round ( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='fc' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX2_Amt,0) else (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX2_Amt,0)  )*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2)  as TAX2_Amt,"
            qry += " round( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='fc' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX3_Amt,0) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX3_Amt,0)  )*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2)  as TAX3_Amt ,"
            qry += "  round( case when  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code='fc' then case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX4_Amt,0) else isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Amt,0) end else ( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX4_Amt,0) else isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Amt,0) end)*(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end,2)  as TAX4_Amt, "
            qry += " case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Rate,0) else 0 end as TAX1_Rate, isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX2_Rate,0) as TAX2_Rate,  isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX3_Rate,0) as TAX3_Rate ,"
            qry += "  case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Invoice_Type='Excise' then isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX4_Rate,0) else isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TAX1_Rate,0) end as TAX4_Rate,  isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.TPT,0) as TPTold, case when (select COUNT(*)  from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL as s1 where s1.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No and s1.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code )>1  then   case when (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code)='fc' then   (select  top 1 s3.tpt from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL as s3 where s3.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No and s3.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and s3.Unit_code='fc'  )else 0 end  else case when (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code)='fc' then (select s4.tpt from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL as s4 where s4.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No and s4.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and s4.Unit_code='fc'  ) else   round ((select s2.tpt from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL as s2 where s2.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No and s2.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and s2.Unit_code='fb'  ) * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) end end tpt,(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Basic_Rate,0) * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Basic_Rate,Item_Net_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Scheme_Item," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class   , 0 as  Price_To_Show,  -1 * ( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Account_Amount is null then 0 else " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Account_Amount end ) as SaleAccAmt," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Comp_Code ,-1 * " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.cust_item_discount_NoTax     from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL  "
            qry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No  "
            qry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No"
            qry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code "
            qry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code "
            qry += "  and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code "
            qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code "
            qry += " left outer join TSPL_ITEM_DETAILS on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_DETAILS.Item_Code "
            qry += "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_ITEM_UOM_DETAIL_1.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code "
            qry += "  where  TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and TSPL_ITEM_DETAILS.Class_Name='size' and convert( date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd") + "' and convert( date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "yyyy-MM-dd") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post in " + postingdata + ""



            If chktempall.IsChecked = True Then

                If chkCustomerSelect.IsChecked = True Then
                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkCustomerSelect.IsChecked = True Then
                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location in  (Select Location_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkcategoryselect.IsChecked AndAlso cbgcategory.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class IN (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If
            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If
            qry += " )Final group by sale_Invoice_No,item_code,Unit_code,Mrp_Amt,Price_To_Show,Comp_Code,Scheme_Item,Discount_Code,[Trade Margin],[Dist Margin] ,seq ,MRPCase,MRPBottle "
            If rbtnItemWise.IsChecked Then
                qry += ",Customer_Class"
            End If




            Dim compdetail As String = "select Comp_Code,Comp_Name,Add1+case when add1='' then '' else ' ,'+ Add2 end as Comaddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
            Dim comdt As DataTable = clsDBFuncationality.GetDataTable(compdetail)
            Dim CurrentCompCode As String = comdt.Rows(0)(0).ToString()
            Dim CurrentCompName As String = comdt.Rows(0)(1).ToString()
            Dim CurrentCompAdd As String = comdt.Rows(0)(2).ToString()

            Dim finpree As String = ""
            Dim finpregrid As String = ""

            Dim Finalqry As String = ""
            Dim FinalGridQry As String = ""



            finpregrid = " select item_code,Seq,MRPCase,MRPBottle,sum(GrossSaleQty) as GrossSaleQty,sum(TradeDiscQty) as TradeDiscQty,sum(TargetDiscQty) as TargetDiscQty, " & _
            "sum(Net_Sale) as Net_Sale,sum(GrossAmt) as GrossAmt,sum(TradeDiscAMt) as TradeDiscAMt,sum(TargetDiscAMt) as TargetDiscAMt, " & _
            "sum(NetSaleAmt) as NetSaleAmt,sum(DMMarginAmt) as DMMarginAmt,sum(TMMarginAmt) as  TMMarginAmt from ( " & _
                "  select MRPCase,MRPBottle,Seq,item_code,Cust_Code,Cust_Name as [Customer],Location_Desc,Location,Sale_Invoice_No,Sale_Invoice_Date,sum(GrossSale) as GrossSaleQty, " & _
                "sum(GrossSale-Trade_Disc)  as Net_Sale," & _
                "sum(TradeDiscQty) as TradeDiscQty,sum(TargetDiscQty) as TargetDiscQty, " & _
                "sum(Total_Amt) + sum(TotDiscAMt) as GrossAmt,sum(Total_Amt) as NetSaleAmt, " & _
                "sum(TradeDiscAMt) as TradeDiscAMt,sum(TargetDiscAMt) as TargetDiscAMt,([Trade Margin] * sum(GrossSale-Trade_Disc)) as TMMarginAmt, " & _
                "([Dist Margin] * sum(GrossSale-Trade_Disc)) as DMMarginAmt   from(   " + qry + " ) a  " & _
                "LEFT OUTER JOIN TSPL_LOCATION_MASTER on a.Location=TSPL_LOCATION_MASTER.Location_Code " & _
                "group by Cust_Code,Cust_Name ,Sale_Invoice_No,Sale_Invoice_Date,item_code,Location,Location_Desc,[Trade Margin],[Dist Margin],Seq ,MRPCase,MRPBottle ) superfinal  group by item_code,Seq,MRPCase,MRPBottle   "



            FinalGridQry = clsCommon.GetQueryWithAllSelectedDataBase(finpregrid, GetSelectedDatabase(), False)
            'FinalGridQry += " order by Customer,Cust_Code"


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(FinalGridQry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtgv
                gridformat()
            End If



            ' head2 = "B-42,Lawrence Road, Industrial Area, New Delhi"
            head2 = CurrentCompAdd


            If refreshGrid = "F" AndAlso chk = True Then

                If rbtnMemoWise.IsChecked Then
                    head1 = "Memo wise Net Sale Report  "
                ElseIf rbtnItemWise.IsChecked Then
                    head1 = "Item wise Net Sale Report  "
                ElseIf rbtnCustomerWise.IsChecked Then
                    head1 = "Customer wise Net Sale Report  "
                End If
                Dim strTemp As String = ""
                Dim arr As New List(Of String)()
                arr.Add(head1)
                arr.Add(objCommonVar.CurrentCompanyName)
                arr.Add(head2)
                arr.Add("  From:  " + txtFromDate.Value + "  To: " + txtToDate.Value + "")
                If chkLocSelect.IsChecked Then
                    strTemp = ""
                    For Each Str2 As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str2
                    Next
                    arr.Add("Location Segment : " + strTemp)
                End If
                If chkcategoryselect.IsChecked Then
                    strTemp = ""
                    For Each Str2 As String In cbgcategory.CheckedDisplayMember
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str2
                    Next
                    arr.Add("Customer Class Segment : " + strTemp)
                End If
                If chkCustomerSelect.IsChecked Then
                    strTemp = ""
                    For Each Str2 As String In cbgCustomer.CheckedDisplayMember
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str2
                    Next
                    arr.Add("Customer Segment : " + strTemp)
                End If
                If chktempselect.IsChecked Then
                    strTemp = ""
                    For Each Str2 As String In cgvtemplate.CheckedDisplayMember
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str2
                    Next
                    arr.Add("Template Segment : " + strTemp)
                End If
                If chkRouteSelect.IsChecked Then
                    strTemp = ""
                    For Each Str2 As String In cbgRoute.CheckedDisplayMember
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str2
                    Next
                    arr.Add("Route Segment : " + strTemp)
                End If

                If exporter = EnumExportTo.Excel Then
                    If rdbCustDetail.IsChecked Then
                        clsCommon.MyExportToExcelGrid(str, gv1, arr, "Distributor Sales Target Report")
                    Else
                        clsCommon.MyExportToExcel(str, gv1, arr, "Distributor Sales Target Report")
                    End If

                Else
                    clsCommon.MyExportToPDF(str, gv1, arr, Me.Text, True)
                End If


            End If



            refreshGrid = "F"

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try




    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub



    Public Sub gridformat()
        Try

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False

            For index As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(index).ReadOnly = True
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()

            gv1.AllowAddNewRow = False


            'gv1.Columns("Cust_Code").IsVisible = True
            'gv1.Columns("Cust_Code").Width = 100
            'gv1.Columns("Cust_Code").HeaderText = "Customer Code"


            'gv1.Columns("Customer").IsVisible = True
            'gv1.Columns("Customer").Width = 100
            'gv1.Columns("Customer").HeaderText = "Customer"

            'gv1.Columns("Location").Width = 100
            'gv1.Columns("Location").IsVisible = False
            'gv1.Columns("Location").HeaderText = "Location"

            'gv1.Columns("Location_Desc").IsVisible = True
            'gv1.Columns("Location_Desc").Width = 100
            'gv1.Columns("Location_Desc").HeaderText = "Location"


            'gv1.Columns("Sale_Invoice_No").IsVisible = True
            'gv1.Columns("Sale_Invoice_No").Width = 200
            'gv1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            'gv1.Columns("Sale_Invoice_Date").IsVisible = True
            'gv1.Columns("Sale_Invoice_Date").Width = 100
            'gv1.Columns("Sale_Invoice_Date").HeaderText = "Sale Invoice Date"

            If rdbSKU.IsChecked Then
                gv1.Columns("Item_Code").IsVisible = True
                gv1.Columns("Item_Code").Width = 100
                gv1.Columns("Item_Code").HeaderText = "SKU"
            Else
                gv1.Columns("Item_Code").IsVisible = True
                gv1.Columns("Item_Code").Width = 100
                gv1.Columns("Item_Code").HeaderText = "Pack"
            End If

            gv1.Columns("MRPCase").IsVisible = True
            gv1.Columns("MRPCase").Width = 100
            gv1.Columns("MRPCase").HeaderText = "MRP Case"

            gv1.Columns("MRPBottle").IsVisible = True
            gv1.Columns("MRPBottle").Width = 100
            gv1.Columns("MRPBottle").HeaderText = "MRP Bottle"


            gv1.Columns("Seq").IsVisible = True
            gv1.Columns("Seq").Width = 100
            gv1.Columns("Seq").HeaderText = "No"

            gv1.Columns("GrossSaleQty").IsVisible = True
            gv1.Columns("GrossSaleQty").Width = 100
            gv1.Columns("GrossSaleQty").HeaderText = "GrossSale Qty"

            gv1.Columns("Net_Sale").IsVisible = True
            gv1.Columns("Net_Sale").Width = 70
            gv1.Columns("Net_Sale").HeaderText = "NetSale Qty"

            gv1.Columns("TradeDiscQty").IsVisible = True
            gv1.Columns("TradeDiscQty").Width = 100
            gv1.Columns("TradeDiscQty").HeaderText = "TradeDisc Qty"

            gv1.Columns("TargetDiscQty").IsVisible = True
            gv1.Columns("TargetDiscQty").Width = 100
            gv1.Columns("TargetDiscQty").HeaderText = "TargetDisc Qty"

            gv1.Columns("GrossAmt").IsVisible = True
            gv1.Columns("GrossAmt").Width = 100
            gv1.Columns("GrossAmt").HeaderText = "Gross Amt"

            gv1.Columns("NetSaleAmt").IsVisible = True
            gv1.Columns("NetSaleAmt").Width = 100
            gv1.Columns("NetSaleAmt").HeaderText = "NetSale Amt"

            gv1.Columns("TradeDiscAMt").IsVisible = True
            gv1.Columns("TradeDiscAMt").Width = 100
            gv1.Columns("TradeDiscAMt").HeaderText = "TradeDisc Amt"

            gv1.Columns("TargetDiscAMt").IsVisible = True
            gv1.Columns("TargetDiscAMt").Width = 100
            gv1.Columns("TargetDiscAMt").HeaderText = "TargetDisc Amt"

            gv1.Columns("DMMarginAmt").IsVisible = True
            gv1.Columns("DMMarginAmt").Width = 100
            gv1.Columns("DMMarginAmt").HeaderText = "DM Margin Amt"

            gv1.Columns("TMMarginAmt").IsVisible = True
            gv1.Columns("TMMarginAmt").Width = 100
            gv1.Columns("TMMarginAmt").HeaderText = "TM Margin Amt"

            ''gv1.GroupDescriptors.Add(New GridGroupByExpression("Location as Location format ""{0}: {1}"" Group By Location"))
            ''gv1.GroupDescriptors.Add(New GridGroupByExpression("CustomerClass as CustomerClass format ""{0}: {1}"" Group By CustomerClass"))
            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Customer as Customer format ""{0}: {1}"" Group By Customer"))
            'gv1.MasterTemplate.ExpandAllGroups()

            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True






            Dim SumGrossSaleQty As New GridViewSummaryItem("GrossSaleQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumGrossSaleQty)
            Dim SumNetSaleQty As New GridViewSummaryItem("Net_Sale", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumNetSaleQty)
            Dim SumTradeDisc As New GridViewSummaryItem("TradeDiscQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTradeDisc)
            Dim SumTargetDisc As New GridViewSummaryItem("TargetDiscQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTargetDisc)

            Dim SumGrossSaleAMt As New GridViewSummaryItem("GrossAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumGrossSaleAMt)
            Dim SumNetSaleAmt As New GridViewSummaryItem("NetSaleAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumNetSaleAmt)
            Dim SumTradeDiscAmt As New GridViewSummaryItem("TradeDiscAMt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTradeDiscAmt)
            Dim SumTargetDiscAmt As New GridViewSummaryItem("TargetDiscAMt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTargetDiscAmt)
            Dim SumTM As New GridViewSummaryItem("TMMarginAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTM)
            Dim SumDM As New GridViewSummaryItem("DMMarginAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumDM)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)




        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub
    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub



    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub



    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub



    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub



    Private Sub chkcategoryall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcategoryall.ToggleStateChanged
        cbgcategory.Enabled = False
    End Sub

    Private Sub chkcategoryselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcategoryselect.ToggleStateChanged
        cbgcategory.Enabled = True
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = False
    End Sub

    Private Sub chkRouteSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = True
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print(False, 2)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub cboCustomerType_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboCustomerType.Validating
        LoadCustomer()
        chkCustomerAll.IsChecked = True
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        refreshGrid = "T"
        print(False, 2)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(True, EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(True, EnumExportTo.PDF)
    End Sub

    Private Sub RadGroupBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox8.Click

    End Sub
End Class
