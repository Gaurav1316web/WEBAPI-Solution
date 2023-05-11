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
'========shivani Tyagi=========='
'===========update by preeti gupta-[BM00000007213]
Public Class RptBulkSaleRegister
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim atchqry As String = ""
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptBulkSaleRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
        btnQuickExport.Visible = MyBase.isExport
    End Sub
#End Region
    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        tvCategory.ExpandAll()
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub
    Sub LoadCustomerGroup()
        Dim Qry As String = "select Cust_Group_Code ,Cust_Group_Desc  from TSPL_CUSTOMER_GROUP_MASTER  "
        cbgCustomerGroup.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgCustomerGroup.ValueMember = "Cust_Group_Code"
        cbgCustomerGroup.DisplayMember = "Cust_Group_Desc"
    End Sub
#Region "COMMENDTED -> OLD PRINT FUNCTION"
    'Sub Print(ByVal IsPrint As Exporter)
    '    '========================Update by Preeti gupta Against ticket no [BM00000008987]
    '    Try
    '        Gv1.DataSource = Nothing
    '        Gv1.Rows.Clear()

    '        If rbtnItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
    '            Throw New Exception("Please select at least one item")
    '        End If
    '        If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    '            Throw New Exception("Please select at least one location")
    '        End If
    '        If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
    '            Throw New Exception("Please select at least one customer")
    '        End If
    '        If rbtnCustomerGroupSelect.IsChecked AndAlso cbgCustomerGroup.CheckedValue.Count <= 0 Then
    '            Throw New Exception("Please select at least one Customer Group")
    '        End If

    '        If clsCommon.myLen(ddlSaleType.SelectedIndex) < 0 Then
    '            Throw New Exception("Please select at least one Sales Type")
    '        End If
    '        If chkSerializeInv.Checked AndAlso rdbDetail.IsChecked = False Then
    '            Throw New Exception("Please Select Detail Type Report")
    '        End If
    '        Dim str As String = ""
    '        Dim strSaleInvoice As String = ""
    '        Dim strSaleReturn As String = ""
    '        Dim strInvoicePosted As String = ""
    '        Dim strReturnPosted As String = ""
    '        If btnAll.IsChecked Then
    '            strInvoicePosted = ""
    '            strReturnPosted = ""
    '        ElseIf btnPosted.IsChecked Then
    '            strInvoicePosted = " m.Posted=1 and "
    '            strReturnPosted = " m.Posted=1 and "
    '        ElseIf btnUnposted.IsChecked Then
    '            strInvoicePosted = " m.Posted=0 and "
    '            strReturnPosted = " m.Posted=0 and "
    '        End If

    '        'strSaleInvoice = "SELECT TSPL_INVOICE_MASTER_BULKSALE.Document_No,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No,Document_Date, 'Sale Invoice' as  Type, TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  " & _
    '        '               " ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  as address,tspl_city_master.city_name," & _
    '        '               " tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_INVOICE_MASTER_BULKSALE.Location_Code ,Location_Desc, TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ,Item_Desc,tspl_item_master.itf_code,Dispatch_Code ,Dispatch_Date ,DispatchQty ,DispatchFatPer ,DispatchSNFPer,Fat_KG as DispatchFat_KG,SNF_KG as DispatchSNF_KG ,DispatchRate ," & _
    '        '               " DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,InvoiceFatKG,InvoiceSNFKG ,InvoiceAmount,RoundOffAmount ,Total_Amt   FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_INVOICE_MASTER_BULKSALE ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_INVOICE_DETAIL_BULKSALE ON " & _
    '        '               " TSPL_ITEM_MASTER.Item_Code = TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ON TSPL_INVOICE_MASTER_BULKSALE.Document_No  = TSPL_INVOICE_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =  TSPL_INVOICE_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join " & _
    '        '               " tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code   left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code" & _
    '        '               "  where  " & strInvoicePosted & "  TSPL_INVOICE_MASTER_BULKSALE.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
    '        '               " TSPL_INVOICE_MASTER_BULKSALE.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"



    '        strSaleInvoice = "select *  from((SELECT distinct  TSPL_INVOICE_MASTER_BULKSALE.Document_No ,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_INVOICE_MASTER_BULKSALE.Document_Date, 'Sale Invoice' as  Type, TSPL_INVOICE_MASTER_BULKSALE.Customer_Code   ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'')  " & _
    '                        " else ' ' end  as address,tspl_city_master.city_name, tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_INVOICE_MASTER_BULKSALE.Location_Code ,Location_Desc, TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ,Item_Desc,tspl_item_master.itf_code,Dispatch_Code ,Dispatch_Date ,DispatchQty ,DispatchFatPer ,DispatchSNFPer,Fat_KG as DispatchFat_KG,SNF_KG as DispatchSNF_KG ,DispatchRate , DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,InvoiceFatKG,InvoiceSNFKG ,InvoiceAmount  ,TSPL_INVOICE_MASTER_BULKSALE.Posted ,TSPL_CUSTOMER_MASTER.Cust_Group_Code,Cust_Group_Desc    ,TSPL_QUALITY_CHECK_BULKSALE.Weighment_No ,TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No ,TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No ,TSPL_QUALITY_CHECK_BULKSALE.fat ,TSPL_QUALITY_CHECK_BULKSALE.snf ,TSPL_GATEOUT_SALE.Document_No as GateOut,TSPL_QUALITY_CHECK_BULKSALE.Tanker_No ,TSPL_Dispatch_Detail_BulkSale.FatAmount ,TSPL_Dispatch_Detail_BulkSale.SNFAmount ,TSPL_Dispatch_Detail_BulkSale.StandardRate as Standard_Rate " & _
    '                        " FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_INVOICE_MASTER_BULKSALE ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_INVOICE_DETAIL_BULKSALE ON  TSPL_ITEM_MASTER.Item_Code = TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ON TSPL_INVOICE_MASTER_BULKSALE.Document_No  = TSPL_INVOICE_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =  TSPL_INVOICE_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on " & _
    '                        " tspl_state_master.state_code=tspl_customer_master.state left outer join  tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code   left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code left join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No =TSPL_Dispatch_Detail_BulkSale.Document_No  left join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code =TSPL_Dispatch_BulkSale.Price_Code left join TSPL_QUALITY_CHECK_BULKSALE on TSPL_QUALITY_CHECK_BULKSALE.QC_No =TSPL_Dispatch_BulkSale.QC_Code left join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo =TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No   )as m left join (select (Total_Amt-InvoiceAmount) as RoundOffAmount,Total_Amt,Document_No as Doc_no,Dispatch_Code as Dis_Code,posted as Status,Document_date " & _
    '                        " as Doc_date from (SELECT  ROUND(InvoiceAmount,0) AS Total_Amt,TSPL_INVOICE_MASTER_BULKSALE.Document_No,InvoiceAmount,Dispatch_Code ,posted,Document_date " & _
    '                        " FROM TSPL_INVOICE_DETAIL_BULKSALE left join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Document_No  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code)as d)d on d.Doc_no = m.Document_No and d.Dis_Code = m.Dispatch_Code ) " & _
    '                        "  where  " & strInvoicePosted & "  m.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
    '                        " m.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"

    '        If rbtnItemSelect.IsChecked Then
    '            strSaleInvoice += " and m.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
    '        End If
    '        If rbtnLocationSelect.IsChecked Then
    '            strSaleInvoice += " and  m.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '        End If
    '        If rbtnCustomerSelect.IsChecked Then
    '            strSaleInvoice += " and m.Customer_Code  in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
    '        End If
    '        If rbtnCustomerGroupSelect.IsChecked Then
    '            strSaleInvoice += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
    '        End If
    '        Dim strInvType As String = ""

    '        strSaleInvoice += strInvType


    '        'strSaleReturn = "SELECT TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,Document_Date,'Sale Return' as Type," & _
    '        '                " TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  as address,tspl_city_master.city_name, tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone,TSPL_SALE_RETURN_MASTER_BULKSALE. Location_Code ,Location_Desc," & _
    '        '                " TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code, Item_Desc, tspl_item_master.itf_code,TSPL_SALE_RETURN_DETAIL_BULKSALE. Dispatch_Code,TSPL_SALE_RETURN_DETAIL_BULKSALE. Dispatch_Date,TSPL_SALE_RETURN_DETAIL_BULKSALE. DispatchQty, TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchFatPer,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchSNFPer,Fat_KG as DispatchFat_KG,SNF_KG as DispatchSNF_KG,TSPL_SALE_RETURN_DETAIL_BULKSALE. DispatchRate, TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchAmount,TSPL_SALE_RETURN_DETAIL_BULKSALE. InvoiceQty, TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceRate,TSPL_SALE_RETURN_DETAIL_BULKSALE. InvoiceFatPer, TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFPer,TSPL_SALE_RETURN_DETAIL_BULKSALE. InvoiceAmount, RoundOffAmount, Total_Amt ,InvoiceFatKG,InvoiceSNFKG " & _
    '        '                " FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_MASTER_BULKSALE   ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_DETAIL_BULKSALE  ON TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code " & _
    '        '                " on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No  = TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =	TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code " & _
    '        '                " left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code" & _
    '        '                " left join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & _
    '        '               "  where  " & strReturnPosted & "  TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
    '        '               " TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"


    '        strSaleReturn = "select * from ((SELECT distinct  TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date, 'Sale Return' as  Type, TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code   ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  as address,tspl_city_master.city_name, tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code ,Location_Desc, TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code " & _
    '                        " ,Item_Desc,tspl_item_master.itf_code,TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Date ,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchQty as DispatchQty ,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchFatPer ,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchSNFPer,-1*Fat_KG as DispatchFat_KG,-1*SNF_KG as DispatchSNF_KG ,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchRate ,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchAmount as DispatchAmount ,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as InvoiceQty,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceRate ,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatPer ,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFPer,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatKG as InvoiceFatKG,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFKG  as InvoiceSNFKG ,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount  as InvoiceAmount  ,TSPL_SALE_RETURN_MASTER_BULKSALE.Posted    ,TSPL_CUSTOMER_MASTER.Cust_Group_Code,Cust_Group_Desc   ,TSPL_QUALITY_CHECK_BULKSALE.Weighment_No ,TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No ,TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No ,TSPL_QUALITY_CHECK_BULKSALE.fat ,TSPL_QUALITY_CHECK_BULKSALE.snf ,TSPL_GATEOUT_SALE.Document_No as GateOut,TSPL_QUALITY_CHECK_BULKSALE.Tanker_No,0 as FatAmount,0 as SNFAmount,0 as Standard_Rate FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_MASTER_BULKSALE   ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_DETAIL_BULKSALE  ON TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code  on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No  = TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =	TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code " & _
    '                        " =TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code left join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo 	left join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No =TSPL_Dispatch_Detail_BulkSale.Document_No left join TSPL_QUALITY_CHECK_BULKSALE on TSPL_QUALITY_CHECK_BULKSALE.QC_No =TSPL_Dispatch_BulkSale.QC_Code  left join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo =TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No  " & _
    '                        " )as m left join (select -1*(Total_Amt-InvoiceAmount) as RoundOffAmount,-1*Total_Amt as Total_Amt,Document_No as Doc_no,Dispatch_Code as Dis_Code,posted as Status,Document_date as Doc_date from (SELECT  ROUND(InvoiceAmount,0) AS Total_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No,InvoiceAmount,Dispatch_Code ,posted,Document_date" & _
    '                        " FROM TSPL_SALE_RETURN_DETAIL_BULKSALE left join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code)as d)d on d.Doc_no = m.Document_No and d.Dis_Code = m.Dispatch_Code )" & _
    '                        " where  " & strReturnPosted & "  m.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
    '                       " m.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"


    '        If rbtnItemSelect.IsChecked Then
    '            strSaleReturn += "  and  m.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
    '        End If
    '        If rbtnLocationSelect.IsChecked Then
    '            strSaleReturn += " and  m.Location_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
    '        End If
    '        If rbtnCustomerSelect.IsChecked Then
    '            strSaleReturn += "  and m.Customer_Code  in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
    '        End If
    '        If rbtnCustomerGroupSelect.IsChecked Then
    '            strSaleInvoice += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
    '        End If



    '        If ddlSaleType.SelectedIndex = 0 Then
    '            str = strSaleInvoice + " union  all " + strSaleReturn
    '        ElseIf ddlSaleType.SelectedIndex = 1 Then
    '            str = strSaleInvoice
    '        Else
    '            str = strSaleReturn
    '        End If

    '        Dim qry As String

    '        qry = "select FDate,TDate,Type,Document_No,Document_Date,Parent_Customer_No,Customer_Code,Customer_Name,address,City_Name,STATE_NAME,Telephone,Location_Code ,Location_Desc ,ada.Item_Code ,ada.Item_Desc ,ada.ITF_CODE ,DispatchQty ,DispatchRate ,DispatchFatPer ,DispatchSNFPer,DispatchSNF_KG,DispatchFat_KG  ,DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,RoundOffAmount ,InvoiceAmount,Total_Amt  ,InvoiceFatKG,InvoiceSNFKG,Cust_Group_Code,Cust_Group_Desc ,Weighment_No ,GateEntry_Document_No ,LoadingTanker_No ,fat ,snf , GateOut ,Tanker_No,FatAmount ,SNFAmount ,Standard_Rate from (" + str + ") as ada"



    '        If rdbSummary.IsChecked Then
    '            qry = "select max(FDate)as FDate ,max(TDate) as TDate ,max(Type)as Type,Document_No,convert(varchar,Document_Date,103)as Document_Date,max(aa.Parent_Customer_No)as Parent_Customer_No,  max( Parent_Master.Customer_Name) as ParentName ,max(Customer_Code)as Customer_Code,max(aa.Customer_Name)as Customer_Name,max(aa.Cust_Group_Code)as Cust_Group_Code,max(aa.Cust_Group_Desc)as Cust_Group_Desc,max(address)as address,max(City_Name)as City_Name ,max(STATE_NAME)as STATE_NAME ,max(Telephone)as Telephone,max(Location_Code)as Location_Code ,max(Location_Desc )as Location_Desc,max(aa.Item_Code)as Item_Code ,max(aa.Item_Desc)as Item_Desc ,max(aa.ITF_CODE)as ITF_CODE,max(aa.Weighment_No )as Weighment_No,max(aa.GateEntry_Document_No )as GateEntry_Document_No,max(aa.LoadingTanker_No )as LoadingTanker_No,sum(aa.fat )as fat,sum(aa.SNF )as SNF,max(aa.GateOut )as GateOut,max(aa.Tanker_No) as Tanker_No ,convert(decimal(18,3),sum(DispatchQty))as DispatchQty,convert(decimal(18,2),(SUM(DispatchAmount)/SUM(DispatchQty)))as DispatchRate ,convert(decimal(18,2),sum(DispatchFat_KG))as DispatchFat_KG ,convert(decimal(18,2),sum(DispatchSNF_KG)) as  DispatchSNF_KG,convert(decimal(18,2),((sum(DispatchFat_KG) / sum(DispatchQty)) * 100 )) as FAT_PER_Dispatch ,convert(decimal(18,2),(sum(DispatchSNF_KG) / sum(DispatchQty) * 100 )) as SNF_PER_Dispatch ,convert(decimal(18,2),sum(DispatchAmount))as DispatchAmount ,convert(decimal(18,3),sum(InvoiceQty))as InvoiceQty ,convert(decimal(18,2),(sum(InvoiceAmount)/sum(InvoiceQty)))as InvoiceRate,convert(decimal(18,2),sum(InvoiceFatKG))as InvoiceFatKG ,convert(decimal(18,2),sum(InvoiceSNFKG))as  InvoiceSNFKG ,convert(decimal(18,2),((sum(InvoiceFatKG) / sum(InvoiceQty))* 100 )) as FAT_PER_Invoice,convert(decimal(18,2),(sum(InvoiceSNFKG) / sum(InvoiceQty)* 100 )) as SNF_PER_Invoice ,convert(decimal(18,2),sum(InvoiceAmount))as InvoiceAmount,convert(decimal(18,2),sum(RoundOffAmount))as RoundOffAmount,convert(decimal(18,2),sum(Total_Amt))as Total_Amt ,sum(FatAmount) as FatAmount ,sum(SNFAmount) as SNFAmount ,sum(Standard_Rate) as Standard_Rate    " & _
    '            " from (" & qry & ")as  aa LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aa.Parent_Customer_No group by Document_No,Document_Date  "
    '        ElseIf rdbDetail.IsChecked = True Then
    '            qry = "select FDate,TDate,Type,Document_No,convert(varchar,Document_Date,103)as Document_Date,aa.Parent_Customer_No, ( Parent_Master.Customer_Name) as ParentName  ,Customer_Code,aa.Customer_Name,aa.Cust_Group_Code,aa.Cust_Group_Desc,address,City_Name,STATE_NAME,Telephone,Location_Code ,Location_Desc ,aa.Item_Code ,aa.Item_Desc ,aa.ITF_CODE,aa.Weighment_No ,aa.GateEntry_Document_No ,aa.LoadingTanker_No ,aa.fat ,aa.snf ,aa. GateOut,aa.Tanker_No,convert(decimal(18,3),DispatchQty)as DispatchQty ,convert(decimal(18,2),DispatchRate)as DispatchRate  ,convert(decimal(18,2),DispatchFatPer) as DispatchFatPer ,convert(decimal(18,2),DispatchSNFPer) as DispatchSNFPer ,convert(decimal(18,2),DispatchSNF_KG) as DispatchSNF_KG,convert(decimal(18,2),DispatchFat_KG) as DispatchFat_KG,convert(decimal(18,2),DispatchAmount)as DispatchAmount ,convert(decimal(18,3),InvoiceQty ) as InvoiceQty,convert(decimal(18,2),InvoiceRate) as InvoiceRate ,convert(decimal(18,2),InvoiceFatPer)as InvoiceFatPer ,convert(decimal(18,2),InvoiceSNFPer)as InvoiceSNFPer ,convert(decimal(18,2),InvoiceFatKG)as InvoiceFatKG,convert(decimal(18,2),InvoiceSNFKG)as InvoiceSNFKG ,convert(decimal(18,2),InvoiceAmount) as InvoiceAmount ,convert(decimal(18,2),RoundOffAmount) as RoundOffAmount,convert(decimal(18,2),Total_Amt) as Total_Amt ,FatAmount ,SNFAmount ,Standard_Rate  " & _
    '            " from (" & qry & ") as aa LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aa.Parent_Customer_No "
    '        End If

    '        If chkSerializeInv.Checked Then
    '            qry = "select final.Type, final.Fdate, final.Tdate, final.Document_No , final.Document_Date,final.Customer_Code,final.Customer_Name,final.Cust_Group_Code,final.Cust_Group_Desc,final.Parent_Customer_No,final.ParentName,final.Address, final.city_name, final.STATE_NAME,final.telephone, final.Location_Code ,Location_Desc ,final.Item_Code ,Item_Desc,Weighment_No ,GateEntry_Document_No ,LoadingTanker_No ,fat ,snf , GateOut,aa.Tanker_No ,ITF_CODE,DispatchQty ,DispatchRate ,DispatchFatPer ,DispatchSNFPer ,DispatchSNF_KG,DispatchFat_KG,DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,InvoiceFatKG,InvoiceSNFKG ,final.RoundOffAmount,InvoiceAmount,final.Total_Amt from( " + qry + ") as final left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No = final.Document_No left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Item_Code= final.Item_Code and TSPL_SERIAL_ITEM.Document_Code= TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst "

    '        End If
    '        qry += " order by convert(date,Document_Date,103) "

    '        atchqry = qry


    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        Gv1.DataSource = Nothing
    '        Gv1.Columns.Clear()
    '        Gv1.Rows.Clear()
    '        Gv1.GroupDescriptors.Clear()
    '        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        Gv1.EnableFiltering = True

    '        'If chkSerializeInv.Checked Then
    '        '    Dim lstItems As New List(Of String)
    '        '    For Each dr As DataRow In dt.Rows
    '        '        If lstItems.Contains(dr("Item_Code").ToString()) Then
    '        '            dr("Total_Amt") = 0.0
    '        '        Else
    '        '            lstItems.Add(dr("Item_Code").ToString())
    '        '        End If
    '        '    Next
    '        'End If

    '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '            Exit Sub
    '        Else
    '            Gv1.DataSource = dt
    '            SetGridFormationOFGV1()
    '        End If
    '        ReStoreGridLayout()
    '        Gv1.MasterTemplate.AllowAddNewRow = False
    '        RadPageView1.SelectedPage = RadPageViewPage2

    '        Dim arrHeader As List(Of String) = New List(Of String)()
    '        Dim strTemp As String = ""
    '        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
    '        'arrHeader.Add("UOM : " + txtUOM.DisplayMember)
    '        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
    '        If rbtnLocationSelect.IsChecked Then
    '            Dim strLocationName As String = ""
    '            For Each StrName As String In cbgLocation.CheckedDisplayMember
    '                If clsCommon.myLen(strLocationName) > 0 Then
    '                    strLocationName += ", "
    '                End If
    '                strLocationName += StrName
    '            Next
    '            Dim strLocationCode As String = ""
    '            For Each StrCode As String In cbgLocation.CheckedValue
    '                If clsCommon.myLen(strLocationCode) > 0 Then
    '                    strLocationCode += ", "
    '                End If
    '                strLocationCode += StrCode
    '            Next
    '            arrHeader.Add(("Location: " + strLocationName + " "))
    '        End If

    '        If IsPrint = Exporter.Excel Then
    '            clsCommon.MyExportToExcelGrid("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
    '        ElseIf IsPrint = Exporter.PDF Then
    '            clsCommon.MyExportToPDF("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Sale Register", True)
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try


    'End Sub

    'KUNAL > TICKET : BM00000007213 >  REQUEST : KDILREQ000149 > DATE UPDATED DUE TO SOURCE SAFE CRASHED : 15 NOV 2016
#End Region
    Sub Print(ByVal IsPrint As Exporter)
        '========================Update by Preeti gupta Against ticket no [BM00000008987]
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            If rbtnItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one item")
            End If
            If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one location")
            End If
            If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one customer")
            End If
            If rbtnCustomerGroupSelect.IsChecked AndAlso cbgCustomerGroup.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Customer Group")
            End If

            If clsCommon.myLen(ddlSaleType.SelectedIndex) < 0 Then
                Throw New Exception("Please select at least one Sales Type")
            End If
            If chkSerializeInv.Checked AndAlso rdbDetail.IsChecked = False Then
                Throw New Exception("Please Select Detail Type Report")
            End If
            Dim str As String = ""
            Dim strSaleInvoice As String = ""
            Dim strSaleReturn As String = ""
            Dim strInvoicePosted As String = ""
            Dim strReturnPosted As String = ""
            If btnAll.IsChecked Then
                strInvoicePosted = ""
                strReturnPosted = ""
            ElseIf btnPosted.IsChecked Then
                strInvoicePosted = " m.Posted=1 and "
                strReturnPosted = " m.Posted=1 and "
            ElseIf btnUnposted.IsChecked Then
                strInvoicePosted = " m.Posted=0 and "
                strReturnPosted = " m.Posted=0 and "
            End If

            'strSaleInvoice = "SELECT TSPL_INVOICE_MASTER_BULKSALE.Document_No,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No,Document_Date, 'Sale Invoice' as  Type, TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  " & _
            '               " ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  as address,tspl_city_master.city_name," & _
            '               " tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_INVOICE_MASTER_BULKSALE.Location_Code ,Location_Desc, TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ,Item_Desc,tspl_item_master.itf_code,Dispatch_Code ,Dispatch_Date ,DispatchQty ,DispatchFatPer ,DispatchSNFPer,Fat_KG as DispatchFat_KG,SNF_KG as DispatchSNF_KG ,DispatchRate ," & _
            '               " DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,InvoiceFatKG,InvoiceSNFKG ,InvoiceAmount,RoundOffAmount ,Total_Amt   FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_INVOICE_MASTER_BULKSALE ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_INVOICE_DETAIL_BULKSALE ON " & _
            '               " TSPL_ITEM_MASTER.Item_Code = TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ON TSPL_INVOICE_MASTER_BULKSALE.Document_No  = TSPL_INVOICE_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =  TSPL_INVOICE_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join " & _
            '               " tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code   left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code" & _
            '               "  where  " & strInvoicePosted & "  TSPL_INVOICE_MASTER_BULKSALE.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
            '               " TSPL_INVOICE_MASTER_BULKSALE.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"

            '===============added by preeti gupta Agianst ticket no [BM00000009570] [19/12/2016]


            strSaleInvoice = "select *  from((SELECT distinct  TSPL_INVOICE_MASTER_BULKSALE.Document_No ,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_INVOICE_MASTER_BULKSALE.Document_Date, 'Sale Invoice' as  Type, TSPL_INVOICE_MASTER_BULKSALE.Customer_Code   ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'')  " & _
                            " else ' ' end  as address,tspl_city_master.city_name, tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_INVOICE_MASTER_BULKSALE.Location_Code ,Location_Desc, TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ,Item_Desc,tspl_item_master.itf_code, Dispatch_Code ,Dispatch_Date ,DispatchQty ,DispatchFatPer ,DispatchSNFPer, case when TSPL_INVOICE_MASTER_BULKSALE.invoiceagainst='Against Dispatch' then TSPL_Dispatch_Detail_BulkSale.Fat_KG else TSPL_Dispatch_Detail_BulkSale_Trade.Fat_KG end as  DispatchFat_KG,case when TSPL_INVOICE_MASTER_BULKSALE.invoiceagainst='Against Dispatch' then  TSPL_Dispatch_Detail_BulkSale.SNF_KG else    TSPL_Dispatch_Detail_BulkSale_Trade.SNF_KG  end as  DispatchSNF_KG ,DispatchRate , DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,InvoiceFatKG,InvoiceSNFKG ,InvoiceAmount  ,TSPL_INVOICE_MASTER_BULKSALE.Posted ,TSPL_CUSTOMER_MASTER.Cust_Group_Code,Cust_Group_Desc ,TSPL_QUALITY_CHECK_BULKSALE.QC_No as [QC Doc],convert(varchar,TSPL_QUALITY_CHECK_BULKSALE.QC_Date,103) as [QC Date]   ,TSPL_QUALITY_CHECK_BULKSALE.Weighment_No,convert(varchar,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date,103) as Weighment_Date  ,TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No,convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as GateEntry_Document_Date ,TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No,convert(varchar,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103) as LoadingTanker_Date ,TSPL_QUALITY_CHECK_BULKSALE.fat ,TSPL_QUALITY_CHECK_BULKSALE.snf ,TSPL_GATEOUT_SALE.Document_No as GateOut,convert(varchar,TSPL_GATEOUT_SALE.Document_Date,103)  as GateOutDate,case when TSPL_INVOICE_MASTER_BULKSALE.invoiceagainst='Against Dispatch' then TSPL_QUALITY_CHECK_BULKSALE.Tanker_No else TSPL_Dispatch_BulkSale_Trade.Tanker_No end as Tanker_No ,case when TSPL_INVOICE_MASTER_BULKSALE.invoiceagainst='Against Dispatch' then TSPL_Dispatch_Detail_BulkSale.FatAmount else TSPL_Dispatch_Detail_BulkSale_Trade.FatAmount  end as FatAmount ,case when TSPL_INVOICE_MASTER_BULKSALE.invoiceagainst='Against Dispatch' then TSPL_Dispatch_Detail_BulkSale.SNFAmount else TSPL_Dispatch_Detail_BulkSale_Trade.SNFAmount  end as SNFAmount ,case when TSPL_INVOICE_MASTER_BULKSALE.invoiceagainst='Against Dispatch' then TSPL_Dispatch_Detail_BulkSale.StandardRate else TSPL_Dispatch_Detail_BulkSale_Trade.StandardRate  end as  Standard_Rate " & _
                            " FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_INVOICE_MASTER_BULKSALE ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_INVOICE_DETAIL_BULKSALE ON  TSPL_ITEM_MASTER.Item_Code = TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ON TSPL_INVOICE_MASTER_BULKSALE.Document_No  = TSPL_INVOICE_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =  TSPL_INVOICE_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on " & _
                            " tspl_state_master.state_code=tspl_customer_master.state left outer join  tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code   left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code left join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No =TSPL_Dispatch_Detail_BulkSale.Document_No left join TSPL_Dispatch_Detail_BulkSale_Trade on TSPL_Dispatch_Detail_BulkSale_Trade.document_no=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Document_No=TSPL_Dispatch_Detail_BulkSale_Trade.Document_No left join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code =TSPL_Dispatch_BulkSale.Price_Code left join TSPL_QUALITY_CHECK_BULKSALE on TSPL_QUALITY_CHECK_BULKSALE.QC_No =TSPL_Dispatch_BulkSale.QC_Code left join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo =TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No  " & _
                            " left join TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No =TSPL_QUALITY_CHECK_BULKSALE.Weighment_No " & _
                            " left join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Document_No =TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No " & _
                            " left join TSPL_LOADING_TANKER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No =TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No " & _
                            " )as m left join (select (Total_Amt-InvoiceAmount) as RoundOffAmount,Total_Amt,Document_No as Doc_no,Dispatch_Code as Dis_Code,posted as Status,Document_date " & _
                            " as Doc_date from (SELECT  ROUND(InvoiceAmount,0) AS Total_Amt,TSPL_INVOICE_MASTER_BULKSALE.Document_No,InvoiceAmount,Dispatch_Code ,posted,Document_date " & _
                           " FROM TSPL_INVOICE_DETAIL_BULKSALE left join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Document_No  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code)as d)d on d.Doc_no = m.Document_No and d.Dis_Code = m.Dispatch_Code ) " & _
                            "  where  " & strInvoicePosted & "  m.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
                            " m.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"

            If rbtnItemSelect.IsChecked Then
                strSaleInvoice += " and m.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            If rbtnLocationSelect.IsChecked Then
                strSaleInvoice += " and  m.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If rbtnCustomerSelect.IsChecked Then
                strSaleInvoice += " and m.Customer_Code  in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            If rbtnCustomerGroupSelect.IsChecked Then
                strSaleInvoice += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            Dim strInvType As String = ""

            strSaleInvoice += strInvType


            'strSaleReturn = "SELECT TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,Document_Date,'Sale Return' as Type," & _
            '                " TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  as address,tspl_city_master.city_name, tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone,TSPL_SALE_RETURN_MASTER_BULKSALE. Location_Code ,Location_Desc," & _
            '                " TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code, Item_Desc, tspl_item_master.itf_code,TSPL_SALE_RETURN_DETAIL_BULKSALE. Dispatch_Code,TSPL_SALE_RETURN_DETAIL_BULKSALE. Dispatch_Date,TSPL_SALE_RETURN_DETAIL_BULKSALE. DispatchQty, TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchFatPer,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchSNFPer,Fat_KG as DispatchFat_KG,SNF_KG as DispatchSNF_KG,TSPL_SALE_RETURN_DETAIL_BULKSALE. DispatchRate, TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchAmount,TSPL_SALE_RETURN_DETAIL_BULKSALE. InvoiceQty, TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceRate,TSPL_SALE_RETURN_DETAIL_BULKSALE. InvoiceFatPer, TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFPer,TSPL_SALE_RETURN_DETAIL_BULKSALE. InvoiceAmount, RoundOffAmount, Total_Amt ,InvoiceFatKG,InvoiceSNFKG " & _
            '                " FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_MASTER_BULKSALE   ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_DETAIL_BULKSALE  ON TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code " & _
            '                " on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No  = TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =      TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code " & _
            '                " left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code" & _
            '                " left join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & _
            '               "  where  " & strReturnPosted & "  TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
            '               " TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"

            ' KUNAL > DATE : 12-NOV2016 -Dispatch_Code, Dispatch_Date,
            strSaleReturn = "select * from ((SELECT distinct  TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No,'" & fromDate.Value & "' as FDate,'" & ToDate.Value & "' as TDate,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date, 'Sale Return' as  Type, TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code   ,tspl_customer_master.Customer_Name,TSPL_CUSTOMER_MASTER.add1+ case when len (TSPL_CUSTOMER_MASTER.add2)>0 then ','+  +TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  as address,tspl_city_master.city_name, tspl_state_master.state_name,(tspl_customer_master.phone1+' '+tspl_customer_master.phone2) as Telephone, TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code ,Location_Desc, TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code " & _
                            " ,Item_Desc,tspl_item_master.itf_code, TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code , TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Date as Dispatch_Date , -1*TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchQty as DispatchQty ,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchFatPer ,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchSNFPer,-1 * TSPL_Dispatch_Detail_BulkSale.Fat_KG  as DispatchFat_KG,-1*  TSPL_Dispatch_Detail_BulkSale.SNF_KG  as DispatchSNF_KG ,TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchRate ,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.DispatchAmount as DispatchAmount ,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as InvoiceQty,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceRate ,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatPer ,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFPer,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatKG as InvoiceFatKG,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFKG  as InvoiceSNFKG ,-1*TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount  as InvoiceAmount  ,TSPL_SALE_RETURN_MASTER_BULKSALE.Posted    ,TSPL_CUSTOMER_MASTER.Cust_Group_Code,Cust_Group_Desc,TSPL_QUALITY_CHECK_BULKSALE.QC_No as [QC Doc],convert(varchar,TSPL_QUALITY_CHECK_BULKSALE.QC_Date,103) as [QC Date]   ,TSPL_QUALITY_CHECK_BULKSALE.Weighment_No ,convert(varchar,TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_Date,103) as Weighment_Date ,TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No,convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as GateEntry_Document_Date ,TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No,convert(varchar,TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_Date,103) as LoadingTanker_Date ,TSPL_QUALITY_CHECK_BULKSALE.fat ,TSPL_QUALITY_CHECK_BULKSALE.snf ,TSPL_GATEOUT_SALE.Document_No as GateOut,convert(varchar,TSPL_GATEOUT_SALE.Document_Date,103)  as GateOutDate,TSPL_QUALITY_CHECK_BULKSALE.Tanker_No,0 as FatAmount,0 as SNFAmount,0 as Standard_Rate FROM  TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_MASTER_BULKSALE   ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code  RIGHT OUTER JOIN TSPL_ITEM_MASTER RIGHT OUTER JOIN TSPL_SALE_RETURN_DETAIL_BULKSALE  ON TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code  on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No  = TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No    left OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER .Location_Code  =      TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code  left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state left outer join tspl_city_master on tspl_city_master.city_code=tspl_customer_master.city_code  left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code " & _
                            " =TSPL_CUSTOMER_MASTER.Cust_Group_Code left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code left join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo    left join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No =TSPL_Dispatch_Detail_BulkSale.Document_No   left join TSPL_Dispatch_Detail_BulkSale_Trade on TSPL_Dispatch_Detail_BulkSale_Trade.document_no=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code  left join TSPL_QUALITY_CHECK_BULKSALE on TSPL_QUALITY_CHECK_BULKSALE.QC_No =TSPL_Dispatch_BulkSale.QC_Code  left join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo =TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No  " & _
                            " left join TSPL_WEIGHMENT_DETAIL_BULKSALE on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No =TSPL_QUALITY_CHECK_BULKSALE.Weighment_No " & _
                            " left join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Document_No =TSPL_QUALITY_CHECK_BULKSALE.GateEntry_Document_No " & _
                            " left join TSPL_LOADING_TANKER_DETAIL_BULKSALE on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No =TSPL_QUALITY_CHECK_BULKSALE.LoadingTanker_No " & _
                            " )as m left join (select -1*(Total_Amt-InvoiceAmount) as RoundOffAmount,-1*Total_Amt as Total_Amt,Document_No as Doc_no,Dispatch_Code as Dis_Code,posted as Status,Document_date as Doc_date from (SELECT  ROUND(InvoiceAmount,0) AS Total_Amt,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No,InvoiceAmount,Dispatch_Code ,posted,Document_date" & _
                            " FROM TSPL_SALE_RETURN_DETAIL_BULKSALE left join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No  left join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_SALE_RETURN_DETAIL_BULKSALE.Dispatch_Code)as d)d on d.Doc_no = m.Document_No and d.Dis_Code = m.Dispatch_Code )" & _
                            " where  " & strReturnPosted & "  m.Document_Date  >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  " & _
                           " m.Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'"


            If rbtnItemSelect.IsChecked Then
                strSaleReturn += "  and  m.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            If rbtnLocationSelect.IsChecked Then
                strSaleReturn += " and  m.Location_Code  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If rbtnCustomerSelect.IsChecked Then
                strSaleReturn += "  and m.Customer_Code  in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If
            If rbtnCustomerGroupSelect.IsChecked Then
                strSaleInvoice += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If


            '3
            If ddlSaleType.SelectedIndex = 0 Then
                str = strSaleInvoice + " union  all " + strSaleReturn
            ElseIf ddlSaleType.SelectedIndex = 1 Then
                str = strSaleInvoice
            Else
                str = strSaleReturn
            End If

            Dim qry As String
            ' KUNAL > DATE 12-NOV-2016
            qry = "select  FDate,TDate,Type,Document_No,Document_Date, Parent_Customer_No,Customer_Code,Customer_Name,address,City_Name,STATE_NAME,Telephone,Location_Code ,Location_Desc ,ada.Item_Code ,ada.Item_Desc ,ada.ITF_CODE , ada.Dispatch_Code , ada.Dispatch_Date ,DispatchQty ,DispatchRate ,DispatchFatPer ,DispatchSNFPer,DispatchSNF_KG,DispatchFat_KG  ,DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,RoundOffAmount ,InvoiceAmount,Total_Amt  ,InvoiceFatKG,InvoiceSNFKG,Cust_Group_Code,Cust_Group_Desc,[QC Doc],[QC Date] ,Weighment_No,Weighment_Date ,GateEntry_Document_No,GateEntry_Document_Date ,LoadingTanker_No,LoadingTanker_Date ,fat ,snf , GateOut,GateOutDate ,Tanker_No,FatAmount ,SNFAmount ,Standard_Rate from (" + str + ") as ada"


            '1
            If rdbSummary.IsChecked Then
                qry = "select  max(FDate)as FDate ,max(TDate) as TDate ,max(Type)as Type,Document_No,  convert(varchar,Document_Date,103)as Document_Date,max(aa.Parent_Customer_No)as Parent_Customer_No,  max( Parent_Master.Customer_Name) as ParentName ,max(Customer_Code)as Customer_Code,max(aa.Customer_Name)as Customer_Name,max(aa.Cust_Group_Code)as Cust_Group_Code,max(aa.Cust_Group_Desc)as Cust_Group_Desc,max(address)as address,max(City_Name)as City_Name ,max(STATE_NAME)as STATE_NAME ,max(Telephone)as Telephone,max(Location_Code)as Location_Code ,max(Location_Desc )as Location_Desc,max(aa.Item_Code)as Item_Code ,max(aa.Item_Desc)as Item_Desc ,max(aa.ITF_CODE)as ITF_CODE, max(aa.Dispatch_Code) as Dispatch_Code ,  max(convert(varchar,aa.Dispatch_Date,103)) as Dispatch_Date,max([QC Doc]) as [QC Doc],max([QC Date]) as [QC Date],   max(aa.Weighment_No )as Weighment_No,max(aa.Weighment_Date )as Weighment_Date,max(aa.GateEntry_Document_No )as GateEntry_Document_No,max(aa.GateEntry_Document_Date )as GateEntry_Document_Date,max(aa.LoadingTanker_No )as LoadingTanker_No,max(aa.LoadingTanker_Date )as LoadingTanker_Date,sum(aa.fat )as fat,sum(aa.SNF )as SNF,max(aa.GateOut )as GateOut,max(aa.GateOutDate )as GateOutDate,max(aa.Tanker_No) as Tanker_No ,convert(decimal(18,3),sum(DispatchQty))as DispatchQty,convert(decimal(18,2),(SUM(DispatchAmount)/SUM(DispatchQty)))as DispatchRate ,convert(decimal(18,2),sum(DispatchFat_KG))as DispatchFat_KG ,convert(decimal(18,2),sum(DispatchSNF_KG)) as  DispatchSNF_KG,convert(decimal(18,2),((sum(DispatchFat_KG) / sum(DispatchQty)) * 100 )) as FAT_PER_Dispatch ,convert(decimal(18,2),(sum(DispatchSNF_KG) / sum(DispatchQty) * 100 )) as SNF_PER_Dispatch ,convert(decimal(18,2),sum(DispatchAmount))as DispatchAmount ,convert(decimal(18,3),sum(InvoiceQty))as InvoiceQty ,convert(decimal(18,2),(sum(InvoiceAmount)/sum(InvoiceQty)))as InvoiceRate,convert(decimal(18,2),sum(InvoiceFatKG))as InvoiceFatKG ,convert(decimal(18,2),sum(InvoiceSNFKG))as  InvoiceSNFKG ,convert(decimal(18,2),((sum(InvoiceFatKG) / sum(InvoiceQty))* 100 )) as FAT_PER_Invoice,convert(decimal(18,2),(sum(InvoiceSNFKG) / sum(InvoiceQty)* 100 )) as SNF_PER_Invoice ,convert(decimal(18,2),sum(InvoiceAmount))as InvoiceAmount,convert(decimal(18,2),sum(RoundOffAmount))as RoundOffAmount,convert(decimal(18,2),sum(Total_Amt))as Total_Amt ,sum(FatAmount) as FatAmount ,sum(SNFAmount) as SNFAmount ,sum(Standard_Rate) as Standard_Rate    " & _
                " from (" & qry & ")as  aa LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aa.Parent_Customer_No group by Document_No,Document_Date  "
                '1 or 2 
            ElseIf rdbDetail.IsChecked = True Then
                qry = "select  FDate,TDate,Type,Document_No,convert(varchar,Document_Date,103)as Document_Date,aa.Parent_Customer_No, ( Parent_Master.Customer_Name) as ParentName  ,Customer_Code,aa.Customer_Name,aa.Cust_Group_Code,aa.Cust_Group_Desc,address,City_Name,STATE_NAME,Telephone,Location_Code ,Location_Desc ,aa.Item_Code ,aa.Item_Desc ,aa.ITF_CODE,  Dispatch_Code , convert(varchar,Dispatch_Date,103) as Dispatch_Date,aa.[QC Doc],aa.[QC Date], aa.Weighment_No,aa.Weighment_Date ,aa.GateEntry_Document_No,aa.GateEntry_Document_Date ,aa.LoadingTanker_No,aa.LoadingTanker_Date ,aa.fat ,aa.snf ,aa. GateOut,aa. GateOutDate,aa.Tanker_No,convert(decimal(18,3),DispatchQty)as DispatchQty ,convert(decimal(18,2),DispatchRate)as DispatchRate  ,convert(decimal(18,2),DispatchFatPer) as DispatchFatPer ,convert(decimal(18,2),DispatchSNFPer) as DispatchSNFPer ,convert(decimal(18,2),DispatchSNF_KG) as DispatchSNF_KG,convert(decimal(18,2),DispatchFat_KG) as DispatchFat_KG,convert(decimal(18,2),DispatchAmount)as DispatchAmount ,convert(decimal(18,3),InvoiceQty ) as InvoiceQty,convert(decimal(18,2),InvoiceRate) as InvoiceRate ,convert(decimal(18,2),InvoiceFatPer)as InvoiceFatPer ,convert(decimal(18,2),InvoiceSNFPer)as InvoiceSNFPer ,convert(decimal(18,2),InvoiceFatKG)as InvoiceFatKG,convert(decimal(18,2),InvoiceSNFKG)as InvoiceSNFKG ,convert(decimal(18,2),InvoiceAmount) as InvoiceAmount ,convert(decimal(18,2),RoundOffAmount) as RoundOffAmount,convert(decimal(18,2),Total_Amt) as Total_Amt ,FatAmount ,SNFAmount ,Standard_Rate  " & _
                " from (" & qry & ") as aa LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=aa.Parent_Customer_No "
            End If

            If chkSerializeInv.Checked Then
                qry = "select final.Type, final.Fdate, final.Tdate, final.Document_No , convert(varchar,final.Document_Date,103) as Document_Date,final.Customer_Code,final.Customer_Name,final.Cust_Group_Code,final.Cust_Group_Desc,final.Parent_Customer_No,final.ParentName,final.Address, final.city_name, final.STATE_NAME,final.telephone, final.Location_Code ,Location_Desc ,final.Item_Code ,Item_Desc,[QC Doc],[QC Date],Weighment_No ,Weighment_Date,GateEntry_Document_No,GateEntry_Document_Date ,LoadingTanker_No,LoadingTanker_Date ,fat ,snf , GateOut,GateOutDate,aa.Tanker_No ,ITF_CODE,DispatchQty ,DispatchRate ,DispatchFatPer ,DispatchSNFPer ,DispatchSNF_KG,DispatchFat_KG,DispatchAmount ,InvoiceQty ,InvoiceRate ,InvoiceFatPer ,InvoiceSNFPer,InvoiceFatKG,InvoiceSNFKG ,final.RoundOffAmount,InvoiceAmount,final.Total_Amt from( " + qry + ") as final left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No = final.Document_No left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Item_Code= final.Item_Code and TSPL_SERIAL_ITEM.Document_Code= TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst "

            End If
            qry += " order by convert(date,Document_Date,103) "

            atchqry = qry


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)




            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            'If chkSerializeInv.Checked Then
            '    Dim lstItems As New List(Of String)
            '    For Each dr As DataRow In dt.Rows
            '        If lstItems.Contains(dr("Item_Code").ToString()) Then
            '            dr("Total_Amt") = 0.0
            '        Else
            '            lstItems.Add(dr("Item_Code").ToString())
            '        End If
            '    Next
            'End If

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            'arrHeader.Add("UOM : " + txtUOM.DisplayMember)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If rbtnLocationSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Location: " + strLocationName + " "))
            End If

            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Sale Register", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
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
#Region "COMMENTED - > OLD SetGridFormationOFGV1 "
    'Sub SetGridFormationOFGV1()


    '    Gv1.TableElement.TableHeaderHeight = 40
    '    Gv1.MasterTemplate.ShowRowHeaderColumn = False
    '    For ii As Integer = 0 To Gv1.Columns.Count - 1
    '        Gv1.Columns(ii).ReadOnly = True
    '        Gv1.Columns(ii).IsVisible = False
    '    Next
    '    '=================update by preeti gupta Against ticket no[BM00000008803]
    '    If rdbDetail.IsChecked = True Then

    '        Gv1.Columns("Type").IsVisible = True
    '        Gv1.Columns("Type").Width = 70
    '        Gv1.Columns("Type").HeaderText = "Type"

    '        Gv1.Columns("Document_No").IsVisible = True
    '        Gv1.Columns("Document_No").Width = 100
    '        Gv1.Columns("Document_No").HeaderText = "Document No"

    '        Gv1.Columns("Document_Date").IsVisible = True
    '        Gv1.Columns("Document_Date").Width = 100
    '        Gv1.Columns("Document_Date").HeaderText = "Document Date"

    '        Gv1.Columns("Parent_Customer_No").IsVisible = True
    '        Gv1.Columns("Parent_Customer_No").Width = 100
    '        Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

    '        Gv1.Columns("ParentName").IsVisible = True
    '        Gv1.Columns("ParentName").Width = 100
    '        Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"


    '        Gv1.Columns("Customer_Code").IsVisible = True
    '        Gv1.Columns("Customer_Code").Width = 100
    '        Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

    '        Gv1.Columns("Customer_Name").IsVisible = True
    '        Gv1.Columns("Customer_Name").Width = 150
    '        Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

    '        Gv1.Columns("Cust_Group_Code").IsVisible = True
    '        Gv1.Columns("Cust_Group_Code").Width = 150
    '        Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"


    '        Gv1.Columns("Cust_Group_Desc").IsVisible = True
    '        Gv1.Columns("Cust_Group_Desc").Width = 150
    '        Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"


    '        Gv1.Columns("address").IsVisible = True
    '        Gv1.Columns("address").Width = 150
    '        Gv1.Columns("address").HeaderText = "Address"

    '        Gv1.Columns("city_name").IsVisible = True
    '        Gv1.Columns("city_name").Width = 100
    '        Gv1.Columns("city_name").HeaderText = "City"

    '        Gv1.Columns("state_name").IsVisible = True
    '        Gv1.Columns("state_name").Width = 100
    '        Gv1.Columns("state_name").HeaderText = "State"

    '        Gv1.Columns("telephone").IsVisible = True
    '        Gv1.Columns("telephone").Width = 120
    '        Gv1.Columns("telephone").HeaderText = "Telephone Number"


    '        Gv1.Columns("Location_Code").IsVisible = True
    '        Gv1.Columns("Location_Code").Width = 100
    '        Gv1.Columns("Location_Code").HeaderText = "Location Code"

    '        Gv1.Columns("Location_Desc").IsVisible = True
    '        Gv1.Columns("Location_Desc").Width = 100
    '        Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

    '        Gv1.Columns("Item_Code").IsVisible = True
    '        Gv1.Columns("Item_Code").Width = 100
    '        Gv1.Columns("Item_Code").HeaderText = "Item Code"

    '        Gv1.Columns("Item_Desc").IsVisible = True
    '        Gv1.Columns("Item_Desc").Width = 100
    '        Gv1.Columns("Item_Desc").HeaderText = "Item Desc"

    '        'Gv1.Columns("Unit_code").IsVisible = True
    '        'Gv1.Columns("Unit_code").Width = 100
    '        'Gv1.Columns("Unit_code").HeaderText = "Unit_code"
    '        'Try
    '        '    Gv1.Columns("Category").IsVisible = True
    '        '    Gv1.Columns("Category").Width = 100
    '        '    Gv1.Columns("Category").HeaderText = "Item Category"
    '        'Catch ex As Exception
    '        'End Try


    '        'Gv1.Columns("Item Serial No").IsVisible = True
    '        'Gv1.Columns("Item Serial No").Width = 80

    '        Gv1.Columns("itf_code").IsVisible = True
    '        Gv1.Columns("itf_code").Width = 70
    '        Gv1.Columns("itf_code").HeaderText = "ITF Code"

    '        Gv1.Columns("Weighment_No").IsVisible = True
    '        Gv1.Columns("Weighment_No").Width = 150
    '        Gv1.Columns("Weighment_No").HeaderText = "Weighment No"

    '        Gv1.Columns("GateEntry_Document_No").IsVisible = True
    '        Gv1.Columns("GateEntry_Document_No").Width = 100
    '        Gv1.Columns("GateEntry_Document_No").HeaderText = "Gate Entry No"

    '        Gv1.Columns("LoadingTanker_No").IsVisible = True
    '        Gv1.Columns("LoadingTanker_No").Width = 100
    '        Gv1.Columns("LoadingTanker_No").HeaderText = "Loading No"

    '        Gv1.Columns("fat").IsVisible = True
    '        Gv1.Columns("fat").Width = 70
    '        Gv1.Columns("fat").HeaderText = "FAT Check"

    '        Gv1.Columns("snf").IsVisible = True
    '        Gv1.Columns("snf").Width = 70
    '        Gv1.Columns("snf").HeaderText = "SNF Check"

    '        Gv1.Columns("GateOut").IsVisible = True
    '        Gv1.Columns("GateOut").Width = 100
    '        Gv1.Columns("GateOut").HeaderText = "Gate Out"

    '        Gv1.Columns("Tanker_No").IsVisible = True
    '        Gv1.Columns("Tanker_No").Width = 100
    '        Gv1.Columns("Tanker_No").HeaderText = "Tanker No"

    '        Gv1.Columns("DispatchQty").IsVisible = True
    '        Gv1.Columns("DispatchQty").Width = 100
    '        Gv1.Columns("DispatchQty").HeaderText = "Dispatch Qty"


    '        Gv1.Columns("DispatchRate").IsVisible = True
    '        Gv1.Columns("DispatchRate").Width = 100
    '        Gv1.Columns("DispatchRate").HeaderText = "Dispatch Rate"


    '        Gv1.Columns("DispatchFatPer").IsVisible = True
    '        Gv1.Columns("DispatchFatPer").Width = 100
    '        Gv1.Columns("DispatchFatPer").HeaderText = "Dispatch FAT%"


    '        Gv1.Columns("DispatchSNFPer").IsVisible = True
    '        Gv1.Columns("DispatchSNFPer").Width = 100
    '        Gv1.Columns("DispatchSNFPer").HeaderText = "Dispatch SNF%"

    '        Gv1.Columns("DispatchFat_KG").IsVisible = True
    '        Gv1.Columns("DispatchFat_KG").Width = 100
    '        Gv1.Columns("DispatchFat_KG").HeaderText = "Dispatch FAT[KG]"

    '        Gv1.Columns("DispatchSNF_KG").IsVisible = True
    '        Gv1.Columns("DispatchSNF_KG").Width = 100
    '        Gv1.Columns("DispatchSNF_KG").HeaderText = "Dispatch SNF[KG]"


    '        Gv1.Columns("DispatchAmount").IsVisible = True
    '        Gv1.Columns("DispatchAmount").Width = 100
    '        Gv1.Columns("DispatchAmount").HeaderText = "Dispatch Amt"

    '        Gv1.Columns("InvoiceQty").IsVisible = True
    '        Gv1.Columns("InvoiceQty").Width = 100
    '        Gv1.Columns("InvoiceQty").HeaderText = "Qty"

    '        Gv1.Columns("InvoiceRate").IsVisible = True
    '        Gv1.Columns("InvoiceRate").Width = 100
    '        Gv1.Columns("InvoiceRate").HeaderText = "Invoice Rate"

    '        Gv1.Columns("InvoiceFatPer").IsVisible = True
    '        Gv1.Columns("InvoiceFatPer").Width = 100
    '        Gv1.Columns("InvoiceFatPer").HeaderText = "Invoice FAT%"

    '        Gv1.Columns("InvoiceSNFPer").IsVisible = True
    '        Gv1.Columns("InvoiceSNFPer").Width = 100
    '        Gv1.Columns("InvoiceSNFPer").HeaderText = "Invoice SNF%"

    '        Gv1.Columns("InvoiceFatKG").IsVisible = True
    '        Gv1.Columns("InvoiceFatKG").Width = 100
    '        Gv1.Columns("InvoiceFatKG").HeaderText = "Invoice FAT[KG]"

    '        Gv1.Columns("InvoiceSNFKG").IsVisible = True
    '        Gv1.Columns("InvoiceSNFKG").Width = 100
    '        Gv1.Columns("InvoiceSNFKG").HeaderText = "Invoice SNF[KG]"

    '        Gv1.Columns("InvoiceAmount").IsVisible = True
    '        Gv1.Columns("InvoiceAmount").Width = 100
    '        Gv1.Columns("InvoiceAmount").HeaderText = "Invoice Amt"

    '        Gv1.Columns("RoundOffAmount").IsVisible = True
    '        Gv1.Columns("RoundOffAmount").Width = 100
    '        Gv1.Columns("RoundOffAmount").HeaderText = "Round Of Amt"



    '        Gv1.Columns("Total_Amt").IsVisible = True
    '        Gv1.Columns("Total_Amt").Width = 100
    '        Gv1.Columns("Total_Amt").HeaderText = "Total Amt"

    '        Gv1.Columns("FatAmount").IsVisible = True
    '        Gv1.Columns("FatAmount").Width = 100
    '        Gv1.Columns("FatAmount").HeaderText = "FAT Value"

    '        Gv1.Columns("SNFAmount").IsVisible = True
    '        Gv1.Columns("SNFAmount").Width = 100
    '        Gv1.Columns("SNFAmount").HeaderText = "SNF Value"

    '        Gv1.Columns("Standard_Rate").IsVisible = True
    '        Gv1.Columns("Standard_Rate").Width = 100
    '        Gv1.Columns("Standard_Rate").HeaderText = "Standard Rate"




    '    ElseIf rdbSummary.IsChecked Then

    '        Gv1.Columns("Type").IsVisible = True
    '        Gv1.Columns("Type").Width = 70
    '        Gv1.Columns("Type").HeaderText = "Type"

    '        Gv1.Columns("Document_No").IsVisible = True
    '        Gv1.Columns("Document_No").Width = 100
    '        Gv1.Columns("Document_No").HeaderText = "Document No"

    '        Gv1.Columns("Document_Date").IsVisible = True
    '        Gv1.Columns("Document_Date").Width = 100
    '        Gv1.Columns("Document_Date").HeaderText = "Document Date"

    '        Gv1.Columns("Parent_Customer_No").IsVisible = True
    '        Gv1.Columns("Parent_Customer_No").Width = 100
    '        Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

    '        Gv1.Columns("ParentName").IsVisible = True
    '        Gv1.Columns("ParentName").Width = 100
    '        Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"


    '        Gv1.Columns("Customer_Code").IsVisible = True
    '        Gv1.Columns("Customer_Code").Width = 100
    '        Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

    '        Gv1.Columns("Customer_Name").IsVisible = True
    '        Gv1.Columns("Customer_Name").Width = 150
    '        Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

    '        Gv1.Columns("Cust_Group_Code").IsVisible = True
    '        Gv1.Columns("Cust_Group_Code").Width = 150
    '        Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"


    '        Gv1.Columns("Cust_Group_Desc").IsVisible = True
    '        Gv1.Columns("Cust_Group_Desc").Width = 150
    '        Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"

    '        Gv1.Columns("address").IsVisible = True
    '        Gv1.Columns("address").Width = 150
    '        Gv1.Columns("address").HeaderText = "Address"

    '        Gv1.Columns("city_name").IsVisible = True
    '        Gv1.Columns("city_name").Width = 100
    '        Gv1.Columns("city_name").HeaderText = "City"

    '        Gv1.Columns("state_name").IsVisible = True
    '        Gv1.Columns("state_name").Width = 100
    '        Gv1.Columns("state_name").HeaderText = "State"

    '        Gv1.Columns("telephone").IsVisible = True
    '        Gv1.Columns("telephone").Width = 120
    '        Gv1.Columns("telephone").HeaderText = "Telephone Number"


    '        Gv1.Columns("Location_Code").IsVisible = True
    '        Gv1.Columns("Location_Code").Width = 100
    '        Gv1.Columns("Location_Code").HeaderText = "Location Code"

    '        Gv1.Columns("Location_Desc").IsVisible = True
    '        Gv1.Columns("Location_Desc").Width = 100
    '        Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

    '        Gv1.Columns("Item_Code").IsVisible = False
    '        Gv1.Columns("Item_Code").Width = 100
    '        Gv1.Columns("Item_Code").HeaderText = "Item Code"

    '        Gv1.Columns("Item_Desc").IsVisible = False
    '        Gv1.Columns("Item_Desc").Width = 100
    '        Gv1.Columns("Item_Desc").HeaderText = "Item Desc"

    '        'Gv1.Columns("Unit_code").IsVisible = True
    '        'Gv1.Columns("Unit_code").Width = 100
    '        'Gv1.Columns("Unit_code").HeaderText = "Unit_code"


    '        Gv1.Columns("Weighment_No").IsVisible = False
    '        Gv1.Columns("Weighment_No").Width = 150
    '        Gv1.Columns("Weighment_No").HeaderText = "Weighment No"

    '        Gv1.Columns("GateEntry_Document_No").IsVisible = False
    '        Gv1.Columns("GateEntry_Document_No").Width = 100
    '        Gv1.Columns("GateEntry_Document_No").HeaderText = "Gate Entry No"

    '        Gv1.Columns("LoadingTanker_No").IsVisible = False
    '        Gv1.Columns("LoadingTanker_No").Width = 100
    '        Gv1.Columns("LoadingTanker_No").HeaderText = "Loading No"

    '        Gv1.Columns("fat").IsVisible = False
    '        Gv1.Columns("fat").Width = 70
    '        Gv1.Columns("fat").HeaderText = "FAT Check"

    '        Gv1.Columns("snf").IsVisible = False
    '        Gv1.Columns("snf").Width = 70
    '        Gv1.Columns("snf").HeaderText = "SNF Check"

    '        Gv1.Columns("GateOut").IsVisible = False
    '        Gv1.Columns("GateOut").Width = 100
    '        Gv1.Columns("GateOut").HeaderText = "Gate Out"

    '        Gv1.Columns("Tanker_No").IsVisible = False
    '        Gv1.Columns("Tanker_No").Width = 100
    '        Gv1.Columns("Tanker_No").HeaderText = "Tanker No"

    '        Gv1.Columns("DispatchQty").IsVisible = True
    '        Gv1.Columns("DispatchQty").Width = 100
    '        Gv1.Columns("DispatchQty").HeaderText = "Dispatch Qty"


    '        Gv1.Columns("DispatchRate").IsVisible = True
    '        Gv1.Columns("DispatchRate").Width = 100
    '        Gv1.Columns("DispatchRate").HeaderText = "Dispatch Rate"

    '        Gv1.Columns("FAT_PER_Dispatch").IsVisible = True
    '        Gv1.Columns("FAT_PER_Dispatch").Width = 100
    '        Gv1.Columns("FAT_PER_Dispatch").HeaderText = "Dispatch FAt%"

    '        Gv1.Columns("SNF_PER_Dispatch").IsVisible = True
    '        Gv1.Columns("SNF_PER_Dispatch").Width = 100
    '        Gv1.Columns("SNF_PER_Dispatch").HeaderText = "Dispatch SNF%"


    '        Gv1.Columns("DispatchAmount").IsVisible = True
    '        Gv1.Columns("DispatchAmount").Width = 100
    '        Gv1.Columns("DispatchAmount").HeaderText = "Dispatch Amt"


    '        Gv1.Columns("InvoiceQty").IsVisible = True
    '        Gv1.Columns("InvoiceQty").Width = 100
    '        Gv1.Columns("InvoiceQty").HeaderText = "Invoice Qty"

    '        Gv1.Columns("InvoiceRate").IsVisible = True
    '        Gv1.Columns("InvoiceRate").Width = 100
    '        Gv1.Columns("InvoiceRate").HeaderText = "Invoice Rate"


    '        Gv1.Columns("FAT_PER_Invoice").IsVisible = True
    '        Gv1.Columns("FAT_PER_Invoice").Width = 100
    '        Gv1.Columns("FAT_PER_Invoice").HeaderText = "Invoice FAt%"

    '        Gv1.Columns("SNF_PER_Invoice").IsVisible = True
    '        Gv1.Columns("SNF_PER_Invoice").Width = 100
    '        Gv1.Columns("SNF_PER_Invoice").HeaderText = "Invoice SNF%"


    '        Gv1.Columns("InvoiceAmount").IsVisible = True
    '        Gv1.Columns("InvoiceAmount").Width = 100
    '        Gv1.Columns("InvoiceAmount").HeaderText = "Invoice Amt"

    '        Gv1.Columns("RoundOffAmount").IsVisible = True
    '        Gv1.Columns("RoundOffAmount").Width = 100
    '        Gv1.Columns("RoundOffAmount").HeaderText = "Disc Amt"


    '        Gv1.Columns("Total_Amt").IsVisible = True
    '        Gv1.Columns("Total_Amt").Width = 100
    '        Gv1.Columns("Total_Amt").HeaderText = "Total Amt"

    '        Gv1.Columns("FatAmount").IsVisible = True
    '        Gv1.Columns("FatAmount").Width = 100
    '        Gv1.Columns("FatAmount").HeaderText = "FAT Value"

    '        Gv1.Columns("SNFAmount").IsVisible = True
    '        Gv1.Columns("SNFAmount").Width = 100
    '        Gv1.Columns("SNFAmount").HeaderText = "SNF Value"

    '        Gv1.Columns("Standard_Rate").IsVisible = True
    '        Gv1.Columns("Standard_Rate").Width = 100
    '        Gv1.Columns("Standard_Rate").HeaderText = "Standard Rate"

    '    End If

    '    Dim summaryRowItem As New GridViewSummaryRowItem()
    '    Dim intCount As Integer = 0

    '    Dim item4 As New GridViewSummaryItem("Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item4)
    '    If Not chkSerializeInv.Checked Then
    '        Dim item5 As New GridViewSummaryItem("InvoiceAmount", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item5)
    '        Dim item6 As New GridViewSummaryItem("DispatchAmount", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item6)
    '        Dim item7 As New GridViewSummaryItem("InvoiceQty", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item7)
    '        Dim item8 As New GridViewSummaryItem("InvoiceRate", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item8)
    '        Dim item9 As New GridViewSummaryItem("DispatchRate", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item9)
    '        Dim item10 As New GridViewSummaryItem("DispatchQty", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item10)
    '        Dim item11 As New GridViewSummaryItem("FatAmount", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item11)
    '        Dim item12 As New GridViewSummaryItem("SNFAmount", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item12)
    '        Dim item13 As New GridViewSummaryItem("Standard_Rate", "{0:F2}", GridAggregateFunction.Sum)
    '        summaryRowItem.Add(item13)





    '    End If
    '    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    '    RadPageView1.SelectedPage = RadPageViewPage2
    '    Gv1.AllowAddNewRow = False
    '    Gv1.ShowGroupPanel = False
    'End Sub
#End Region
    Sub SetGridFormationOFGV1()
        Try
            'KUNAL > TICKET : BM00000007213   
            'REQUEST : KDILREQ000149  
            'DATE UPDATED DUE TO SOURCE SAFE CRASHED : 15 NOV 2016

            Gv1.TableElement.TableHeaderHeight = 40
            Gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(ii).ReadOnly = True
                Gv1.Columns(ii).IsVisible = False
            Next
            '=================update by preeti gupta Against ticket no[BM00000008803]
            If rdbDetail.IsChecked = True Then

                Gv1.Columns("Type").IsVisible = True
                Gv1.Columns("Type").Width = 70
                Gv1.Columns("Type").HeaderText = "Type"

                Gv1.Columns("Document_No").IsVisible = True
                Gv1.Columns("Document_No").Width = 100
                Gv1.Columns("Document_No").HeaderText = "Document No"

                Gv1.Columns("Document_Date").IsVisible = True
                Gv1.Columns("Document_Date").Width = 100
                Gv1.Columns("Document_Date").HeaderText = "Document Date"

                Gv1.Columns("Parent_Customer_No").IsVisible = True
                Gv1.Columns("Parent_Customer_No").Width = 100
                Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

                Gv1.Columns("ParentName").IsVisible = True
                Gv1.Columns("ParentName").Width = 100
                Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"


                Gv1.Columns("Customer_Code").IsVisible = True
                Gv1.Columns("Customer_Code").Width = 100
                Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

                Gv1.Columns("Customer_Name").IsVisible = True
                Gv1.Columns("Customer_Name").Width = 150
                Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

                Gv1.Columns("Cust_Group_Code").IsVisible = True
                Gv1.Columns("Cust_Group_Code").Width = 150
                Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"


                Gv1.Columns("Cust_Group_Desc").IsVisible = True
                Gv1.Columns("Cust_Group_Desc").Width = 150
                Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"


                Gv1.Columns("address").IsVisible = True
                Gv1.Columns("address").Width = 150
                Gv1.Columns("address").HeaderText = "Address"

                Gv1.Columns("city_name").IsVisible = True
                Gv1.Columns("city_name").Width = 100
                Gv1.Columns("city_name").HeaderText = "City"

                Gv1.Columns("state_name").IsVisible = True
                Gv1.Columns("state_name").Width = 100
                Gv1.Columns("state_name").HeaderText = "State"

                Gv1.Columns("telephone").IsVisible = True
                Gv1.Columns("telephone").Width = 120
                Gv1.Columns("telephone").HeaderText = "Telephone Number"


                Gv1.Columns("Location_Code").IsVisible = True
                Gv1.Columns("Location_Code").Width = 100
                Gv1.Columns("Location_Code").HeaderText = "Location Code"

                Gv1.Columns("Location_Desc").IsVisible = True
                Gv1.Columns("Location_Desc").Width = 100
                Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

                Gv1.Columns("Item_Code").IsVisible = True
                Gv1.Columns("Item_Code").Width = 100
                Gv1.Columns("Item_Code").HeaderText = "Item Code"

                Gv1.Columns("Item_Desc").IsVisible = True
                Gv1.Columns("Item_Desc").Width = 100
                Gv1.Columns("Item_Desc").HeaderText = "Item Desc"

                'Gv1.Columns("Unit_code").IsVisible = True
                'Gv1.Columns("Unit_code").Width = 100
                'Gv1.Columns("Unit_code").HeaderText = "Unit_code"
                'Try
                '    Gv1.Columns("Category").IsVisible = True
                '    Gv1.Columns("Category").Width = 100
                '    Gv1.Columns("Category").HeaderText = "Item Category"
                'Catch ex As Exception
                'End Try


                'Gv1.Columns("Item Serial No").IsVisible = True
                'Gv1.Columns("Item Serial No").Width = 80

                Gv1.Columns("itf_code").IsVisible = True
                Gv1.Columns("itf_code").Width = 70
                Gv1.Columns("itf_code").HeaderText = "ITF Code"

                Gv1.Columns("QC Doc").IsVisible = True
                Gv1.Columns("QC Doc").Width = 150
                Gv1.Columns("QC Doc").HeaderText = "QC No"

                Gv1.Columns("QC Date").IsVisible = True
                Gv1.Columns("QC Date").Width = 150
                Gv1.Columns("QC Date").HeaderText = "QC Date"

                Gv1.Columns("Weighment_No").IsVisible = True
                Gv1.Columns("Weighment_No").Width = 150
                Gv1.Columns("Weighment_No").HeaderText = "Weighment No"

                Gv1.Columns("Weighment_Date").IsVisible = True
                Gv1.Columns("Weighment_Date").Width = 150
                Gv1.Columns("Weighment_Date").HeaderText = "Weighment Date"

                Gv1.Columns("GateEntry_Document_No").IsVisible = True
                Gv1.Columns("GateEntry_Document_No").Width = 100
                Gv1.Columns("GateEntry_Document_No").HeaderText = "Gate Entry No"

                Gv1.Columns("GateEntry_Document_Date").IsVisible = True
                Gv1.Columns("GateEntry_Document_Date").Width = 100
                Gv1.Columns("GateEntry_Document_Date").HeaderText = "Gate Entry Date"

                Gv1.Columns("LoadingTanker_No").IsVisible = True
                Gv1.Columns("LoadingTanker_No").Width = 100
                Gv1.Columns("LoadingTanker_No").HeaderText = "Loading No"

                Gv1.Columns("LoadingTanker_Date").IsVisible = True
                Gv1.Columns("LoadingTanker_Date").Width = 100
                Gv1.Columns("LoadingTanker_Date").HeaderText = "Loading Date"

                Gv1.Columns("fat").IsVisible = True
                Gv1.Columns("fat").Width = 70
                Gv1.Columns("fat").HeaderText = "FAT Check"

                Gv1.Columns("snf").IsVisible = True
                Gv1.Columns("snf").Width = 70
                Gv1.Columns("snf").HeaderText = "SNF Check"

                Gv1.Columns("GateOut").IsVisible = True
                Gv1.Columns("GateOut").Width = 100
                Gv1.Columns("GateOut").HeaderText = "Gate Out"

                Gv1.Columns("GateOutDate").IsVisible = True
                Gv1.Columns("GateOutDate").Width = 100
                Gv1.Columns("GateOutDate").HeaderText = "Gate Out Date"

                Gv1.Columns("Tanker_No").IsVisible = True
                Gv1.Columns("Tanker_No").Width = 100
                Gv1.Columns("Tanker_No").HeaderText = "Tanker No"

                'KUNAL > DATE :13-NOV-2016
                Gv1.Columns("Dispatch_Code").IsVisible = True
                Gv1.Columns("Dispatch_Code").Width = 100
                Gv1.Columns("Dispatch_Code").HeaderText = "Dispatch Code"

                'KUNAL > DATE :13-NOV-2016
                Gv1.Columns("Dispatch_Date").IsVisible = True
                Gv1.Columns("Dispatch_Date").Width = 100
                Gv1.Columns("Dispatch_Date").HeaderText = "Dispatch Date"


                Gv1.Columns("DispatchQty").IsVisible = True
                Gv1.Columns("DispatchQty").Width = 100
                Gv1.Columns("DispatchQty").HeaderText = "Dispatch Qty"


                Gv1.Columns("DispatchRate").IsVisible = True
                Gv1.Columns("DispatchRate").Width = 100
                Gv1.Columns("DispatchRate").HeaderText = "Dispatch Rate"


                Gv1.Columns("DispatchFatPer").IsVisible = True
                Gv1.Columns("DispatchFatPer").Width = 100
                Gv1.Columns("DispatchFatPer").HeaderText = "Dispatch FAT%"


                Gv1.Columns("DispatchSNFPer").IsVisible = True
                Gv1.Columns("DispatchSNFPer").Width = 100
                Gv1.Columns("DispatchSNFPer").HeaderText = "Dispatch SNF%"

                Gv1.Columns("DispatchFat_KG").IsVisible = True
                Gv1.Columns("DispatchFat_KG").Width = 100
                Gv1.Columns("DispatchFat_KG").HeaderText = "Dispatch FAT[KG]"

                Gv1.Columns("DispatchSNF_KG").IsVisible = True
                Gv1.Columns("DispatchSNF_KG").Width = 100
                Gv1.Columns("DispatchSNF_KG").HeaderText = "Dispatch SNF[KG]"


                Gv1.Columns("DispatchAmount").IsVisible = True
                Gv1.Columns("DispatchAmount").Width = 100
                Gv1.Columns("DispatchAmount").HeaderText = "Dispatch Amt"

                Gv1.Columns("InvoiceQty").IsVisible = True
                Gv1.Columns("InvoiceQty").Width = 100
                Gv1.Columns("InvoiceQty").HeaderText = "Qty"

                Gv1.Columns("InvoiceRate").IsVisible = True
                Gv1.Columns("InvoiceRate").Width = 100
                Gv1.Columns("InvoiceRate").HeaderText = "Invoice Rate"

                Gv1.Columns("InvoiceFatPer").IsVisible = True
                Gv1.Columns("InvoiceFatPer").Width = 100
                Gv1.Columns("InvoiceFatPer").HeaderText = "Invoice FAT%"

                Gv1.Columns("InvoiceSNFPer").IsVisible = True
                Gv1.Columns("InvoiceSNFPer").Width = 100
                Gv1.Columns("InvoiceSNFPer").HeaderText = "Invoice SNF%"

                Gv1.Columns("InvoiceFatKG").IsVisible = True
                Gv1.Columns("InvoiceFatKG").Width = 100
                Gv1.Columns("InvoiceFatKG").HeaderText = "Invoice FAT[KG]"

                Gv1.Columns("InvoiceSNFKG").IsVisible = True
                Gv1.Columns("InvoiceSNFKG").Width = 100
                Gv1.Columns("InvoiceSNFKG").HeaderText = "Invoice SNF[KG]"

                Gv1.Columns("InvoiceAmount").IsVisible = True
                Gv1.Columns("InvoiceAmount").Width = 100
                Gv1.Columns("InvoiceAmount").HeaderText = "Invoice Amt"

                Gv1.Columns("RoundOffAmount").IsVisible = True
                Gv1.Columns("RoundOffAmount").Width = 100
                Gv1.Columns("RoundOffAmount").HeaderText = "Round Of Amt"

                Gv1.Columns("Total_Amt").IsVisible = True
                Gv1.Columns("Total_Amt").Width = 100
                Gv1.Columns("Total_Amt").HeaderText = "Total Amt"

                Gv1.Columns("FatAmount").IsVisible = True
                Gv1.Columns("FatAmount").Width = 100
                Gv1.Columns("FatAmount").HeaderText = "FAT Value"

                Gv1.Columns("SNFAmount").IsVisible = True
                Gv1.Columns("SNFAmount").Width = 100
                Gv1.Columns("SNFAmount").HeaderText = "SNF Value"

                Gv1.Columns("Standard_Rate").IsVisible = True
                Gv1.Columns("Standard_Rate").Width = 100
                Gv1.Columns("Standard_Rate").HeaderText = "Standard Rate"
            ElseIf rdbSummary.IsChecked Then

                Gv1.Columns("Type").IsVisible = True
                Gv1.Columns("Type").Width = 70
                Gv1.Columns("Type").HeaderText = "Type"

                Gv1.Columns("Document_No").IsVisible = True
                Gv1.Columns("Document_No").Width = 100
                Gv1.Columns("Document_No").HeaderText = "Document No"

                Gv1.Columns("Document_Date").IsVisible = True
                Gv1.Columns("Document_Date").Width = 100
                Gv1.Columns("Document_Date").HeaderText = "Document Date"

                Gv1.Columns("Parent_Customer_No").IsVisible = True
                Gv1.Columns("Parent_Customer_No").Width = 100
                Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

                Gv1.Columns("ParentName").IsVisible = True
                Gv1.Columns("ParentName").Width = 100
                Gv1.Columns("ParentName").HeaderText = "Parent Customer Name"


                Gv1.Columns("Customer_Code").IsVisible = True
                Gv1.Columns("Customer_Code").Width = 100
                Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

                Gv1.Columns("Customer_Name").IsVisible = True
                Gv1.Columns("Customer_Name").Width = 150
                Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

                Gv1.Columns("Cust_Group_Code").IsVisible = True
                Gv1.Columns("Cust_Group_Code").Width = 150
                Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"


                Gv1.Columns("Cust_Group_Desc").IsVisible = True
                Gv1.Columns("Cust_Group_Desc").Width = 150
                Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"

                Gv1.Columns("address").IsVisible = True
                Gv1.Columns("address").Width = 150
                Gv1.Columns("address").HeaderText = "Address"

                Gv1.Columns("city_name").IsVisible = True
                Gv1.Columns("city_name").Width = 100
                Gv1.Columns("city_name").HeaderText = "City"

                Gv1.Columns("state_name").IsVisible = True
                Gv1.Columns("state_name").Width = 100
                Gv1.Columns("state_name").HeaderText = "State"

                Gv1.Columns("telephone").IsVisible = True
                Gv1.Columns("telephone").Width = 120
                Gv1.Columns("telephone").HeaderText = "Telephone Number"


                Gv1.Columns("Location_Code").IsVisible = True
                Gv1.Columns("Location_Code").Width = 100
                Gv1.Columns("Location_Code").HeaderText = "Location Code"

                Gv1.Columns("Location_Desc").IsVisible = True
                Gv1.Columns("Location_Desc").Width = 100
                Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

                Gv1.Columns("Item_Code").IsVisible = False
                Gv1.Columns("Item_Code").Width = 100
                Gv1.Columns("Item_Code").HeaderText = "Item Code"

                Gv1.Columns("Item_Desc").IsVisible = False
                Gv1.Columns("Item_Desc").Width = 100
                Gv1.Columns("Item_Desc").HeaderText = "Item Desc"

                'Gv1.Columns("Unit_code").IsVisible = True
                'Gv1.Columns("Unit_code").Width = 100
                'Gv1.Columns("Unit_code").HeaderText = "Unit_code"

                Gv1.Columns("QC Doc").IsVisible = True
                Gv1.Columns("QC Doc").Width = 150
                Gv1.Columns("QC Doc").HeaderText = "QC No"

                Gv1.Columns("QC Date").IsVisible = True
                Gv1.Columns("QC Date").Width = 150
                Gv1.Columns("QC Date").HeaderText = "QC Date"

                Gv1.Columns("Weighment_No").IsVisible = False
                Gv1.Columns("Weighment_No").Width = 150
                Gv1.Columns("Weighment_No").HeaderText = "Weighment No"

                Gv1.Columns("GateEntry_Document_No").IsVisible = False
                Gv1.Columns("GateEntry_Document_No").Width = 100
                Gv1.Columns("GateEntry_Document_No").HeaderText = "Gate Entry No"

                Gv1.Columns("LoadingTanker_No").IsVisible = False
                Gv1.Columns("LoadingTanker_No").Width = 100
                Gv1.Columns("LoadingTanker_No").HeaderText = "Loading No"

                Gv1.Columns("fat").IsVisible = False
                Gv1.Columns("fat").Width = 70
                Gv1.Columns("fat").HeaderText = "FAT Check"

                Gv1.Columns("snf").IsVisible = False
                Gv1.Columns("snf").Width = 70
                Gv1.Columns("snf").HeaderText = "SNF Check"

                Gv1.Columns("GateOut").IsVisible = False
                Gv1.Columns("GateOut").Width = 100
                Gv1.Columns("GateOut").HeaderText = "Gate Out"

                Gv1.Columns("Tanker_No").IsVisible = False
                Gv1.Columns("Tanker_No").Width = 100
                Gv1.Columns("Tanker_No").HeaderText = "Tanker No"


                'KUNAL > DATE :13-NOV-2016
                Gv1.Columns("Dispatch_Code").IsVisible = True
                Gv1.Columns("Dispatch_Code").Width = 100
                Gv1.Columns("Dispatch_Code").HeaderText = "Dispatch Code"

                'KUNAL > DATE :13-NOV-2016
                Gv1.Columns("Dispatch_Date").IsVisible = True
                Gv1.Columns("Dispatch_Date").Width = 100
                Gv1.Columns("Dispatch_Date").HeaderText = "Dispatch Date"
                Gv1.Columns("DispatchQty").IsVisible = True
                Gv1.Columns("DispatchQty").Width = 100
                Gv1.Columns("DispatchQty").HeaderText = "Dispatch Qty"


                Gv1.Columns("DispatchRate").IsVisible = True
                Gv1.Columns("DispatchRate").Width = 100
                Gv1.Columns("DispatchRate").HeaderText = "Dispatch Rate"

                Gv1.Columns("FAT_PER_Dispatch").IsVisible = True
                Gv1.Columns("FAT_PER_Dispatch").Width = 100
                Gv1.Columns("FAT_PER_Dispatch").HeaderText = "Dispatch FAt%"

                Gv1.Columns("SNF_PER_Dispatch").IsVisible = True
                Gv1.Columns("SNF_PER_Dispatch").Width = 100
                Gv1.Columns("SNF_PER_Dispatch").HeaderText = "Dispatch SNF%"


                Gv1.Columns("DispatchAmount").IsVisible = True
                Gv1.Columns("DispatchAmount").Width = 100
                Gv1.Columns("DispatchAmount").HeaderText = "Dispatch Amt"


                Gv1.Columns("InvoiceQty").IsVisible = True
                Gv1.Columns("InvoiceQty").Width = 100
                Gv1.Columns("InvoiceQty").HeaderText = "Invoice Qty"

                Gv1.Columns("InvoiceRate").IsVisible = True
                Gv1.Columns("InvoiceRate").Width = 100
                Gv1.Columns("InvoiceRate").HeaderText = "Invoice Rate"


                Gv1.Columns("FAT_PER_Invoice").IsVisible = True
                Gv1.Columns("FAT_PER_Invoice").Width = 100
                Gv1.Columns("FAT_PER_Invoice").HeaderText = "Invoice FAt%"

                Gv1.Columns("SNF_PER_Invoice").IsVisible = True
                Gv1.Columns("SNF_PER_Invoice").Width = 100
                Gv1.Columns("SNF_PER_Invoice").HeaderText = "Invoice SNF%"


                Gv1.Columns("InvoiceAmount").IsVisible = True
                Gv1.Columns("InvoiceAmount").Width = 100
                Gv1.Columns("InvoiceAmount").HeaderText = "Invoice Amt"

                Gv1.Columns("RoundOffAmount").IsVisible = True
                Gv1.Columns("RoundOffAmount").Width = 100
                Gv1.Columns("RoundOffAmount").HeaderText = "Disc Amt"


                Gv1.Columns("Total_Amt").IsVisible = True
                Gv1.Columns("Total_Amt").Width = 100
                Gv1.Columns("Total_Amt").HeaderText = "Total Amt"

                Gv1.Columns("FatAmount").IsVisible = True
                Gv1.Columns("FatAmount").Width = 100
                Gv1.Columns("FatAmount").HeaderText = "FAT Value"

                Gv1.Columns("SNFAmount").IsVisible = True
                Gv1.Columns("SNFAmount").Width = 100
                Gv1.Columns("SNFAmount").HeaderText = "SNF Value"

                Gv1.Columns("Standard_Rate").IsVisible = True
                Gv1.Columns("Standard_Rate").Width = 100
                Gv1.Columns("Standard_Rate").HeaderText = "Standard Rate"

            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item4 As New GridViewSummaryItem("Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            If Not chkSerializeInv.Checked Then
                Dim item5 As New GridViewSummaryItem("InvoiceAmount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("DispatchAmount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)
                Dim item7 As New GridViewSummaryItem("InvoiceQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item7)
                Dim item8 As New GridViewSummaryItem("InvoiceRate", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item8)
                Dim item9 As New GridViewSummaryItem("DispatchRate", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item9)
                Dim item10 As New GridViewSummaryItem("DispatchQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item10)
                Dim item11 As New GridViewSummaryItem("FatAmount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item11)
                Dim item12 As New GridViewSummaryItem("SNFAmount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item12)
                Dim item13 As New GridViewSummaryItem("Standard_Rate", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item13)
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            RadPageView1.SelectedPage = RadPageViewPage2
            Gv1.AllowAddNewRow = False
            Gv1.ShowGroupPanel = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadCustomer()
        LoadLocation()
        LoadItem()
        ' LoadCategory()
        LoadCustomerGroup()
        'txtUOM.SelectedIndex = 1
        rbtnCategoryAll.IsChecked = True
        rbtnCustomerAll.IsChecked = True
        rbtnLocationAll.IsChecked = True
        rbtnItemAll.IsChecked = True
        rbtnCustomerGroupAll.IsChecked = True
        rdbDetail.IsChecked = True
        ddlSaleType.SelectedIndex = 0
        btnPosted.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    'Private Sub txtUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtUOM1._MYValidating
    '    Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
    '    txtUOM1.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM1.Value, "Code", isButtonClicked)
    'End Sub

    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub rbtnCustomerGroupAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerGroupAll.ToggleStateChanged
        cbgCustomerGroup.Enabled = rbtnCustomerGroupSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        cbgLocation.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnItemAll.ToggleStateChanged
        cbgItem.Enabled = rbtnItemSelect.IsChecked
    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub



    Private Sub Gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If rdbSummary.IsChecked Then
            If Gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Type").Value)
                Dim strDoc = Gv1.CurrentRow.Cells("Document_No").Value
                If strTransType = "Sale Invoice" Then
                    strTransType = "SD-IN"
                ElseIf clsCommon.CompairString(strTransType, "Bulk Sale") = CompairStringResult.Equal Then
                    strTransType = "Bulk_Sale"
                Else
                    strTransType = "Sale Return"
                End If
                Select Case strTransType
                    Case "SD-IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strDoc)
                    Case "Bulk_Sale"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strDoc)
                    Case "Sale Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, strDoc)
                End Select
            End If
        End If
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub rmPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Print(Exporter.PDF)
    End Sub

    Private Sub btnSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptBulkSaleRegister
        frm.ShowDialog()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        'Try
        '    Dim repotype As String = ""
        '    Dim invtype As String = ""
        '    If Gv1.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Data Found To Send Mail", Me.Text)
        '        Return
        '    End If

        '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.RptBulkSaleRegister)

        '    If obj Is Nothing Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If
        '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If

        '    Try

        '        Dim strEmail As String = ""


        '        If Process.GetProcessesByName("OutLook").Length < 1 Then
        '            'restarts the Process
        '            Process.Start("OutLook.exe")
        '        End If
        '        Dim oApp As New Outlook.Application()
        '        Dim oMsg As Outlook.MailItem

        '        If chkAll.IsChecked Then
        '            invtype = ""
        '        ElseIf chkTax.IsChecked Then
        '            invtype = "Tax Invoice"
        '        ElseIf chkRetail.IsChecked Then
        '            invtype = "Retail Invoice"
        '        End If

        '        If rdbDetail.IsChecked Then
        '            repotype = "Detail Report"
        '        Else
        '            repotype = "Summary Report"
        '        End If

        '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
        '        strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

        '        Try
        '            If strEmail.Substring(0, 1) = "," Then
        '                strEmail = strEmail.Substring(1, strEmail.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        If clsCommon.myLen(strEmail) <= 0 Then
        '            clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
        '            Return
        '        End If

        '        oMsg.Body = obj.mailbody

        '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Body.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        oMsg.Subject = obj.mailsubjct

        '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Subject.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If

        '        '------------------------code for attchament-------------------------------------
        '        If obj.atchmnt = "Y" Then
        '            Dim sDisplayname As [String] = "MyAttachment"
        '            If oMsg.Body Is Nothing Then
        '                oMsg.Body = " "
        '            End If
        '            Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
        '            Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

        '            Dim strRptPath As String = ""

        '            Dim oAttachment As Outlook.Attachment = Nothing
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

        '            If dt.Rows.Count > 0 Then
        '                Dim subPath As String = Application.StartupPath + "\Mail Reports"

        '                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

        '                If (IsExists = False) Then

        '                    System.IO.Directory.CreateDirectory(subPath)
        '                End If
        '                strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
        '                transportSql.exportdata(Gv1, strRptPath, "Sheet1", False, Nothing, False, False, False, True)
        '                oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
        '            End If
        '        End If
        '        '---------------------------------------------------------------------------


        '        oMsg.Recipients.Add(strEmail)
        '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
        '        oMsg.Send()
        '        oMsg = Nothing
        '        oApp = Nothing

        '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try

        '    Try
        '        Dim client As New System.Net.WebClient()

        '        If clsCommon.myLen(obj.smsbody) <= 0 Then
        '            Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
        '        End If

        '        Dim strMes As String = ""

        '        strMes = obj.smsbody
        '        strMes = strMes.Replace("'", " ").Replace("`", "/")

        '        If strMes.Contains(clsEmailSMSConstants.FromDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ToDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ReportType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

        '        Try
        '            If strphone.Substring(0, 1) = "," Then
        '                strphone = strphone.Substring(1, strphone.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        'Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
        '        'Dim data As Stream = client.OpenRead(baseurl)
        '        'Dim reader As StreamReader = New StreamReader(data)
        '        'Dim s As String = reader.ReadToEnd()
        '        'data.Close()
        '        'reader.Close()

        '        Dim UserId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_Name, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim Paswd As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_PWD, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim SenderId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Sendor_ID, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim SMS_Provider As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Provider, clsFixedParameterCode.MilkSetting, Nothing))

        '        If clsCommon.CompairString(SMS_Provider, "Bulk SMS") = CompairStringResult.Equal Then
        '            '================send sms through PerfectBulkSMS====================
        '            Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
        '            Dim str As String = "http://www.perfectbulksms.in/Sendsmsapi.aspx?USERID=" + UserId + "&PASSWORD=" + Paswd + "&SENDERID=" + SenderId + "&TO=" & strphone & "&MESSAGE=" & strMes & ""
        '            Dim wrquest As HttpWebRequest = WebRequest.Create(str)
        '            Dim getresponse As HttpWebResponse = Nothing
        '            getresponse = wrquest.GetResponse()

        '            Dim objStream As Stream = getresponse.GetResponseStream()
        '            Dim objSR As StreamReader = New StreamReader(objStream, encode, True)
        '            Dim strResponse As String = objSR.ReadToEnd()
        '            'clsCommon.MyMessageBoxShow(getresponse.StatusDescription)

        '            objSR.Close()
        '            objStream.Close()
        '            getresponse.Close()
        '            '===========================================================
        '        ElseIf clsCommon.CompairString(SMS_Provider, "BSWS") = CompairStringResult.Equal Then
        '            Dim consumeWebService As BSWS.BSWS
        '            consumeWebService = New BSWS.BSWS
        '            Dim xmlResult As XmlElement
        '            xmlResult = consumeWebService.SubmitSMS("prashant@tecxpert.in", "tecxpert", strphone, strMes, "", 0, "TSPLSW", "")
        '        End If

        '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

    End Sub

    Private Sub RptBulkSaleRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Reset()
            btnSend.Visibility = ElementVisibility.Collapsed
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBulkSaleRegister & "'"))
            If rbtnLocationSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Location: " + strLocationName + " "))
            End If


            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
