Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'========Preeti Gupta=========='
Public Class RptMCCsaleRegister
    Dim isInsideLoadData As Boolean = False
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptMccSaleRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        btnQuickExport.Visible = MyBase.isExport
        btnExportToExcel.Visible = MyBase.isExport
    End Sub
#End Region
    Sub LoadCategory()
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
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGO.Enabled = False
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub
    Sub LoadCustomerGroup()
        'Dim Qry As String = "select Cust_Group_Code ,Cust_Group_Desc  from TSPL_CUSTOMER_GROUP_MASTER  "
        'cbgCustomerGroup.DataSource = clsDBFuncationality.GetDataTable(Qry)
        'cbgCustomerGroup.ValueMember = "Cust_Group_Code"
        'cbgCustomerGroup.DisplayMember = "Cust_Group_Desc"
    End Sub
    Private Sub txtUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        '==================update by preeti guptab Against ticket no [BM00000008806]
        '=========================changes by shivani against ticket no[BM00000008843]
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            'If rbtnItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
            '    Throw New Exception("Please select at least one item")
            'End If
            'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            '    Throw New Exception("Please select at least one location")
            'End If
            'If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            '    Throw New Exception("Please select at least one customer")
            'End If


            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strMCCMaterial As String = ""



            '=========
            Dim strPivotForOuter As String
            strPivotForOuter = " select distinct (select Distinct ',sum(isnull(aa.'+tax1+',0)) as '+TAX1 from ("
            strPivotForOuter += " select TAX1 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX2 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX3 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX4 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX5 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX6 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX7 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX8 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX9 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForOuter += " union all"
            strPivotForOuter += " select TAX10 from TSPL_SD_SHIPMENT_DETAIL)aa where len(isnull(TAX1,''))>0 for xml path('') )"

            Dim strPivotForOuterQuery As String = clsDBFuncationality.getSingleValue(strPivotForOuter)

            Dim strPivotForInner As String
            strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
            strPivotForInner += " select distinct (select Distinct ',['+tax1+']' from ("
            strPivotForInner += " select TAX1 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += " select TAX2 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += " select TAX3 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += "  select TAX4 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += " select TAX5 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += " select TAX6 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += " select TAX7 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += "  select TAX8 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += " select TAX9 from TSPL_SD_SHIPMENT_DETAIL"
            strPivotForInner += " union all"
            strPivotForInner += " select TAX10 from TSPL_SD_SHIPMENT_DETAIL)a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

            Dim strPivotForInnerQuery As String = clsDBFuncationality.getSingleValue(strPivotForInner)


            Dim strPivotForOuterReturn As String
            strPivotForOuterReturn = " select distinct (select Distinct ',sum(isnull(aa.'+tax1+',0)) as '+TAX1 from ("
            strPivotForOuterReturn += " select TAX1 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX2 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX3 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX4 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX5 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX6 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX7 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX8 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX9 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForOuterReturn += " union all"
            strPivotForOuterReturn += " select TAX10 from TSPL_SD_SALE_RETURN_DETAIL)aa where len(isnull(TAX1,''))>0 for xml path('') )"

            Dim strPivotForOuterQueryReturn As String = clsDBFuncationality.getSingleValue(strPivotForOuterReturn)

            Dim strPivotForInnerReturn As String
            strPivotForInnerReturn = "select SUBSTRING(ax,2,len(Ax)) from ("
            strPivotForInnerReturn += " select distinct (select Distinct ',['+tax1+']' from ("
            strPivotForInnerReturn += " select TAX1 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += " select TAX2 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += " select TAX3 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += "  select TAX4 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += " select TAX5 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += " select TAX6 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += " select TAX7 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += "  select TAX8 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += " select TAX9 from TSPL_SD_SALE_RETURN_DETAIL"
            strPivotForInnerReturn += " union all"
            strPivotForInnerReturn += " select TAX10 from TSPL_SD_SALE_RETURN_DETAIL)a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

            Dim strPivotForInnerQueryReturn As String = clsDBFuncationality.getSingleValue(strPivotForInnerReturn)
            If rdbDetail.IsChecked Then
                strMCCMaterial = " select final.* from (select Trans_Type,DOCUMENT_CODE ,max(Sale_Invoice_No) as Invoice_No,'' as [Return No],max(convert(varchar,Document_Date,103))as Document_Date,max(Document_Date) as Date ,aa.Item_Code,max(TSPL_ITEM_MASTER .Item_Desc) as Item_Desc,max(TSPL_ITEM_MASTER.ITF_CODE )as ITF_CODE,max(aa.Customer_Code) as Customer_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name ,max(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as Parent_Customer_No,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code) as Cust_Group_Code ,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc ,max(Parent_Master.Cust_Code) as Parent_Customer_Code,max(Parent_Master.Customer_Name) as Parent_Cust_Name,max(TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end) as Address,max(Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End ) as Phone_No,max(Cust_State_Master.STATE_NAME)as STATE_NAME ,max(Cust_City_Master.City_Name) as City_Name ,Location ,max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,aa.Unit_code, Qty  as Qty , Item_Cost   as Item_Cost"
                strMCCMaterial += "" + strPivotForOuterQuery + " ,  max(aa.Amount)  as Amount,  sum(aa.Disc_Amt)  as Disc_Amt, max(Amt_Less_Discount)  as Amt_Less_Discount, (Total_Tax_Amt)   as Total_Tax_Amt, (Item_Net_Amt)  as Total_Amount from ("
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX1,  TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt   as TAX1_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost   as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX2, TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt   as TAX2_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    ,TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost   as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX3, TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt  as TAX3_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "     union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,   TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX4, TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt  as TAX4_Amt from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "     union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX5,  TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt  as TAX5_Amt from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "    union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX6, TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt  as  TAX6_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "     union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX7, TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt  as TAX7_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "    union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX8, TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt   as TAX8_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost,TSPL_SD_SHIPMENT_DETAIL.TAX9, TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt   TAX9_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SHIPMENT_DETAIL.Amount  as Amount    , TSPL_SD_SHIPMENT_DETAIL.Disc_Amt  as Disc_Amt , TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount  as Amt_Less_Discount  , TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  as Item_Net_Amt  ,TSPL_SD_SHIPMENT_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SHIPMENT_DETAIL.Qty  as Qty,TSPL_SD_SHIPMENT_DETAIL.Unit_code,TSPL_SD_SHIPMENT_DETAIL.Item_Cost  as Item_Cost ,TSPL_SD_SHIPMENT_DETAIL.TAX10, TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt  TAX10_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += ")aa "
                strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on aa.Customer_Code=TSPL_CUSTOMER_MASTER .Cust_Code  "
                strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on aa .Bill_To_Location=TSPL_LOCATION_MASTER .Location_Code  "
                strMCCMaterial += " left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
                strMCCMaterial += " left outer join TSPL_STATE_MASTER as Cust_State_Master on Cust_State_Master .STATE_CODE =TSPL_CUSTOMER_MASTER.State "
                strMCCMaterial += " left outer join TSPL_CITY_MASTER as Cust_City_Master on Cust_City_Master .City_Code =TSPL_CUSTOMER_MASTER.City_Code "
                strMCCMaterial += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =aa .Item_Code "
                strMCCMaterial += "    where 2 = 2  and  Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + clsCommon.myCstr(IIf(clsCommon.myLen(txtUOM.Value) > 0, "and aa.Unit_code='" + txtUOM.Value + "' ", ""))
                If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and  TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") " + Environment.NewLine
                End If
                If TxtMultiSelectCustomer.arrValueMember IsNot Nothing AndAlso TxtMultiSelectCustomer.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and aa.Customer_Code in (" + clsCommon.GetMulcallString(TxtMultiSelectCustomer.arrValueMember) + ") " + Environment.NewLine
                End If
                If TxtMultiSelectItem.arrValueMember IsNot Nothing AndAlso TxtMultiSelectItem.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and aa.Item_Code in (" + clsCommon.GetMulcallString(TxtMultiSelectItem.arrValueMember) + ") " + Environment.NewLine
                End If




                If btnPosted.IsChecked Then
                    strMCCMaterial += " and aa.Status=1  "

                ElseIf btnUnposted.IsChecked Then
                    strMCCMaterial += " and aa.Status=0  "

                End If
                strMCCMaterial += "  group by Trans_Type,DOCUMENT_CODE,aa.Item_Code,Location,Qty,aa.Unit_code,Item_Cost,aa.status ,Total_Tax_Amt,Item_Net_Amt "
                strMCCMaterial += "   union all"

                strMCCMaterial += " select Trans_Type,DOCUMENT_CODE ,max(Sale_Invoice_No) as Invoice_No,max( [Return No] ) as [Return No],max(convert(varchar,Document_Date,103))as Document_Date,max(Document_Date) as Date ,aa.Item_Code,max(TSPL_ITEM_MASTER .Item_Desc) as Item_Desc,max(TSPL_ITEM_MASTER.ITF_CODE )as ITF_CODE,max(aa.Customer_Code) as Customer_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name ,max(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as Parent_Customer_No,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code) as Cust_Group_Code ,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc ,max(Parent_Master.Cust_Code) as Parent_Customer_Code,max(Parent_Master.Customer_Name) as Parent_Cust_Name,max(TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end) as Address,max(Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End ) as Phone_No,max(Cust_State_Master.STATE_NAME)as STATE_NAME ,max(Cust_City_Master.City_Name) as City_Name ,Location ,max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,aa.Unit_code, Qty  as Qty ,  Item_Cost  as Item_Cost"
                strMCCMaterial += "" + strPivotForOuterQuery + " ,  max(aa.Amount) as Amount,  sum(aa.Disc_Amt)  as Disc_Amt, max(Amt_Less_Discount)  as Amt_Less_Discount, (Total_Tax_Amt)  as Total_Tax_Amt,  (Item_Net_Amt)  as Total_Amount from ("
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1) as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)  as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD. Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)  as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX1,   TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt *(-1) as TAX1_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1) as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)  as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] ,TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)  as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX2,TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt*(-1)   as TAX2_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)   as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX3, TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt*(-1)  as TAX3_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "     union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)  as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1) as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX4,  TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt *(-1)  as TAX4_Amt from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "     union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  ,  TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)  as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX5, TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt *(-1)  as TAX5_Amt from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "    union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)  as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)  as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX6, TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt *(-1)  as  TAX6_Amt  from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "     union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)   as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)  as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX7, TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt *(-1)  as TAX7_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "    union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1) as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)  as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)  as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX8,TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt*(-1)   as TAX8_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1) as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1)  as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)  as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1)  as Item_Cost,TSPL_SD_SALE_RETURN_DETAIL.TAX9, TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt*(-1)   TAX9_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strMCCMaterial += " select * from (select TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type,TSPL_SD_SHIPMENT_HEAD.status ,TSPL_SD_SHIPMENT_DETAIL.document_Code,TSPL_SD_SHIPMENT_HEAD.Document_Date,  TSPL_SD_SALE_RETURN_DETAIL.Amount*(-1)  as Amount    , TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt*(-1)  as Disc_Amt , TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount*(-1) as Amt_Less_Discount  , TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt*(-1)  as Item_Net_Amt  ,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Return No] , TSPL_SD_SALE_RETURN_DETAIL.Qty*(-1)  as Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*(-1) as Item_Cost ,TSPL_SD_SALE_RETURN_DETAIL.TAX10, TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt*(-1)  TAX10_Amt   from TSPL_SD_SHIPMENT_DETAIL   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_head.Document_Code  where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC')s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t"
                strMCCMaterial += ")aa "
                strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on aa.Customer_Code=TSPL_CUSTOMER_MASTER .Cust_Code  "
                strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on aa .Bill_To_Location=TSPL_LOCATION_MASTER .Location_Code  "
                strMCCMaterial += " left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
                strMCCMaterial += " left outer join TSPL_STATE_MASTER as Cust_State_Master on Cust_State_Master .STATE_CODE =TSPL_CUSTOMER_MASTER.State "
                strMCCMaterial += " left outer join TSPL_CITY_MASTER as Cust_City_Master on Cust_City_Master .City_Code =TSPL_CUSTOMER_MASTER.City_Code "
                strMCCMaterial += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =aa .Item_Code "
                strMCCMaterial += "    where 2 = 2 and [Return No]  is not null  and  Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + clsCommon.myCstr(IIf(clsCommon.myLen(txtUOM.Value) > 0, "and aa.Unit_code='" + txtUOM.Value + "' ", ""))

                'If rbtnItemSelect.IsChecked Then
                '    strMCCMaterial += " and aa.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                'End If
                'If cbgLocation.CheckedValue.Count > 0 Then
                '    strMCCMaterial += " and  TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                'End If
                'If rbtnCustomerSelect.IsChecked Then
                '    strMCCMaterial += " and aa.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                'End If
                If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and  TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") " + Environment.NewLine
                End If
                If TxtMultiSelectCustomer.arrValueMember IsNot Nothing AndAlso TxtMultiSelectCustomer.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and aa.Customer_Code in (" + clsCommon.GetMulcallString(TxtMultiSelectCustomer.arrValueMember) + ") " + Environment.NewLine
                End If
                If TxtMultiSelectItem.arrValueMember IsNot Nothing AndAlso TxtMultiSelectItem.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and aa.Item_Code in (" + clsCommon.GetMulcallString(TxtMultiSelectItem.arrValueMember) + ") " + Environment.NewLine
                End If




                If btnPosted.IsChecked Then
                    strMCCMaterial += " and aa.Status=1  "

                ElseIf btnUnposted.IsChecked Then
                    strMCCMaterial += " and aa.Status=0  "

                End If
                strMCCMaterial += " group by Trans_Type,DOCUMENT_CODE,aa.Item_Code,Location,Qty,aa.Unit_code,Item_Cost,aa.status ,Total_Tax_Amt,Item_Net_Amt ) as final"

            End If

            If rdbSummary.IsChecked Then
                strMCCMaterial = " select final.* from (select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.status ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Document_Date as Date ,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name ,tSPL_CUSTOMER_MASTER.Parent_Customer_No as Parent_Customer_No,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code as Cust_Group_Code ,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as Cust_Group_Desc ,(Parent_Master.Cust_Code) as Parent_Customer_Code,(Parent_Master.Customer_Name) as Parent_Cust_Name,(TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end) as Address,(Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End ) as Phone_No,(Cust_State_Master.STATE_NAME)as STATE_NAME ,(Cust_City_Master.City_Name) as City_Name,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Location_Code ,(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,'' as Against_Invoice_No , TSPL_SD_SHIPMENT_HEAD.Discount_Base as Discount_Base  , TSPL_SD_SHIPMENT_HEAD.Discount_Amt  as Discount_Amt  , TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount  as Amount_Less_Discount , TSPL_SD_SHIPMENT_HEAD.Total_Tax_Amt  as Total_Tax_Amt, TSPL_SD_SHIPMENT_HEAD.Total_Amt  as Total_Amt "
                strMCCMaterial += " from TSPL_SD_SHIPMENT_HEAD"
                'strMCCMaterial += " left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code 
                strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SHIPMENT_HEAD .Customer_Code  "
                strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location  "
                strMCCMaterial += " left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
                strMCCMaterial += " left outer join TSPL_STATE_MASTER as Cust_State_Master on Cust_State_Master .STATE_CODE =TSPL_CUSTOMER_MASTER.State "
                strMCCMaterial += " left outer join TSPL_CITY_MASTER as Cust_City_Master on Cust_City_Master .City_Code =TSPL_CUSTOMER_MASTER.City_Code"
                strMCCMaterial += "  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code "
                strMCCMaterial += " left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code "
                strMCCMaterial += " where 2 = 2  and TSPL_SD_SHIPMENT_HEAD.Trans_Type ='MCC' and  TSPL_SD_SHIPMENT_HEAD.Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  TSPL_SD_SHIPMENT_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

                strMCCMaterial += "   union all"

                strMCCMaterial += " select TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.status ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Document_Date as Date ,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name ,tSPL_CUSTOMER_MASTER.Parent_Customer_No as Parent_Customer_No,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code as Cust_Group_Code ,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as Cust_Group_Desc ,(Parent_Master.Cust_Code) as Parent_Customer_Code,(Parent_Master.Customer_Name) as Parent_Cust_Name,(TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end) as Address,(Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End ) as Phone_No,(Cust_State_Master.STATE_NAME)as STATE_NAME ,(Cust_City_Master.City_Name) as City_Name,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Location_Code ,(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,TSPL_SD_SALE_RETURN_HEAD.Document_Code as Against_Invoice_No , TSPL_SD_SALE_RETURN_HEAD.Discount_Base*(-1)  as Discount_Base  , TSPL_SD_SALE_RETURN_HEAD.Discount_Amt*(-1)  as Discount_Amt  , TSPL_SD_SALE_RETURN_HEAD.Amount_Less_Discount*(-1)  as Amount_Less_Discount , TSPL_SD_SALE_RETURN_HEAD.Total_Tax_Amt*(-1)  as Total_Tax_Amt, TSPL_SD_SALE_RETURN_HEAD.Total_Amt *(-1)  as Total_Amt "
                strMCCMaterial += " from TSPL_SD_SHIPMENT_HEAD"
                'strMCCMaterial += " left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE =TSPL_SD_SHIPMENT_HEAD.Document_Code 
                strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SHIPMENT_HEAD .Customer_Code  "
                strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_SD_SHIPMENT_HEAD .Bill_To_Location  "
                strMCCMaterial += " left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
                strMCCMaterial += " left outer join TSPL_STATE_MASTER as Cust_State_Master on Cust_State_Master .STATE_CODE =TSPL_CUSTOMER_MASTER.State "
                strMCCMaterial += " left outer join TSPL_CITY_MASTER as Cust_City_Master on Cust_City_Master .City_Code =TSPL_CUSTOMER_MASTER.City_Code"
                strMCCMaterial += "  left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No =TSPL_SD_SHIPMENT_HEAD.Document_Code "
                strMCCMaterial += " left join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.Document_Code "
                strMCCMaterial += " where 2 = 2 and TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No is not null and TSPL_SD_SHIPMENT_HEAD.Trans_Type ='MCC' and  TSPL_SD_SHIPMENT_HEAD.Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  TSPL_SD_SHIPMENT_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' ) as Final where 2=2"

                If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and   final.Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMul.arrValueMember) + ") " + Environment.NewLine
                End If
                If TxtMultiSelectCustomer.arrValueMember IsNot Nothing AndAlso TxtMultiSelectCustomer.arrValueMember.Count > 0 Then
                    strMCCMaterial += " and final.Customer_Code in (" + clsCommon.GetMulcallString(TxtMultiSelectCustomer.arrValueMember) + ") " + Environment.NewLine
                End If
               If btnPosted.IsChecked Then
                    strMCCMaterial += " and final.Status=1  "

                ElseIf btnUnposted.IsChecked Then
                    strMCCMaterial += " and final.Status=0  "

                End If
                strMCCMaterial += " order by DOCUMENT_CODE "
            End If
            dt = clsDBFuncationality.GetDataTable(strMCCMaterial)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            If rdbSummary.IsChecked Then
                Gv1.Tag = "Summary"
            Else
                Gv1.Tag = "Details"
            End If

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
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
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("UOM : " + txtUOM.Value)
            If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMul.arrDispalyMember))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If TxtMultiSelectCustomer.arrValueMember IsNot Nothing AndAlso TxtMultiSelectCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(TxtMultiSelectCustomer.arrDispalyMember))
            Else
                arrHeader.Add((" Customer : All"))
            End If
            If TxtMultiSelectItem.arrValueMember IsNot Nothing AndAlso TxtMultiSelectItem.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiSelectItem.arrDispalyMember))
            Else
                arrHeader.Add((" Item : All"))
            End If

            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("MCC Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("MCC Sale Register" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Sale Register", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub SetGridFormationOFGV1()


        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        If rdbSummary.IsChecked = True Then

            Gv1.Columns("Document_Code").IsVisible = True
            Gv1.Columns("Document_Code").Width = 70
            Gv1.Columns("Document_Code").HeaderText = "Document Code"

            Gv1.Columns("Sale_Invoice_No").IsVisible = True
            Gv1.Columns("Sale_Invoice_No").Width = 70
            Gv1.Columns("Sale_Invoice_No").HeaderText = "Invoice No"

            Gv1.Columns("Against_Invoice_No").IsVisible = True
            Gv1.Columns("Against_Invoice_No").Width = 70
            Gv1.Columns("Against_Invoice_No").HeaderText = "Return No"


            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 100
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("Customer_Code").IsVisible = True
            Gv1.Columns("Customer_Code").Width = 100
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 150
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("Parent_Customer_No").IsVisible = True
            Gv1.Columns("Parent_Customer_No").Width = 100
            Gv1.Columns("Parent_Customer_No").HeaderText = "Parent Customer Code"

            Gv1.Columns("Cust_Group_Code").IsVisible = True
            Gv1.Columns("Cust_Group_Code").Width = 100
            Gv1.Columns("Cust_Group_Code").HeaderText = "Cust Group Code"

            Gv1.Columns("Cust_Group_Desc").IsVisible = True
            Gv1.Columns("Cust_Group_Desc").Width = 150
            Gv1.Columns("Cust_Group_Desc").HeaderText = "Cust Group Desc"

            Gv1.Columns("Parent_Customer_Code").IsVisible = True
            Gv1.Columns("Parent_Customer_Code").Width = 100
            Gv1.Columns("Parent_Customer_Code").HeaderText = "Parent Cust Code"

            Gv1.Columns("Parent_Cust_Name").IsVisible = True
            Gv1.Columns("Parent_Cust_Name").Width = 100
            Gv1.Columns("Parent_Cust_Name").HeaderText = "Parent Cust Name"

            Gv1.Columns("Address").IsVisible = True
            Gv1.Columns("Address").Width = 120
            Gv1.Columns("Address").HeaderText = "Address"

            Gv1.Columns("Phone_No").IsVisible = True
            Gv1.Columns("Phone_No").Width = 100
            Gv1.Columns("Phone_No").HeaderText = "Phone No"

            Gv1.Columns("STATE_NAME").IsVisible = True
            Gv1.Columns("STATE_NAME").Width = 100
            Gv1.Columns("STATE_NAME").HeaderText = "State Name"

            Gv1.Columns("City_Name").IsVisible = True
            Gv1.Columns("City_Name").Width = 100
            Gv1.Columns("City_Name").HeaderText = "City Name"

            Gv1.Columns("Location_Code").IsVisible = True
            Gv1.Columns("Location_Code").Width = 100
            Gv1.Columns("Location_Code").HeaderText = "Location Code"

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").Width = 100
            Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            Gv1.Columns("Discount_Base").IsVisible = True
            Gv1.Columns("Discount_Base").Width = 100
            Gv1.Columns("Discount_Base").HeaderText = "Amount"

            Gv1.Columns("Discount_Amt").IsVisible = True
            Gv1.Columns("Discount_Amt").Width = 100
            Gv1.Columns("Discount_Amt").HeaderText = "Discount Amt"

            Gv1.Columns("Amount_Less_Discount").IsVisible = True
            Gv1.Columns("Amount_Less_Discount").Width = 100
            Gv1.Columns("Amount_Less_Discount").HeaderText = "Amount Less Discount"

            Gv1.Columns("Total_Tax_Amt").IsVisible = True
            Gv1.Columns("Total_Tax_Amt").Width = 100
            Gv1.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"

            Gv1.Columns("Total_Amt").IsVisible = True
            Gv1.Columns("Total_Amt").Width = 100
            Gv1.Columns("Total_Amt").HeaderText = "Total Amt"


        ElseIf rdbDetail.IsChecked Then
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If clsCommon.CompairString("ITF_CODE", Gv1.Columns(i).HeaderText) = CompairStringResult.Equal Then
                    Gv1.Columns(i).HeaderText = "ITF Code"
                Else
                    Gv1.Columns(i).HeaderText = Replace(Gv1.Columns(i).HeaderText, "_", " ")
                    Gv1.Columns(i).HeaderText = StrConv(Gv1.Columns(i).HeaderText, VbStrConv.ProperCase)
                    Gv1.Columns("Date").IsVisible = False
                End If

            Next

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If rdbSummary.IsChecked Then
            Dim item1 As New GridViewSummaryItem("Discount_Base", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Discount_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Amount_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item5 As New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item4 As New GridViewSummaryItem("Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
        Else

            Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Disc_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Amt_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Total_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)


        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtUOM.Value = ""
        'LoadCustomer()
        'LoadLocation()
        'LoadItem()
        LoadCategory()
        LoadCustomerGroup()
        'rbtnCategoryAll.IsChecked = True
        'rbtnCustomerAll.IsChecked = True
        'rbtnLocationAll.IsChecked = True
        'rbtnItemAll.IsChecked = True
        'rbtnCustomerGroupAll.IsChecked = True
        rdbSummary.IsChecked = True
        'ddlSaleType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        'If rbtnLocationAll.IsChecked Then
        '    cbgLocation.CheckedAll()
        'Else
        '    cbgLocation.UnCheckedAll()
        'End If
        RadPageView1.SelectedPage = RadPageViewPage1
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
    'Sub send()
    '    Try
    '        Dim repotype As String = ""
    '        Dim invtype As String = ""
    '        If Gv1.Rows.Count <= 0 Then
    '            clsCommon.MyMessageBoxShow("No Data Found To Send Mail", Me.Text)
    '            Return
    '        End If

    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.RptMccSaleRegister)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Try

    '            Dim strEmail As String = ""


    '            If Process.GetProcessesByName("OutLook").Length < 1 Then
    '                'restarts the Process
    '                Process.Start("OutLook.exe")
    '            End If
    '            Dim oApp As New Outlook.Application()
    '            Dim oMsg As Outlook.MailItem

    '            'If chkAll.IsChecked Then
    '            '    invtype = ""
    '            'ElseIf chkTax.IsChecked Then
    '            '    invtype = "Tax Invoice"
    '            'ElseIf chkRetail.IsChecked Then
    '            '    invtype = "Retail Invoice"
    '            'End If

    '            If rdbDetail.IsChecked Then
    '                repotype = "Detail Report"
    '            Else
    '                repotype = "Summary Report"
    '            End If

    '            oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '            strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

    '            Try
    '                If strEmail.Substring(0, 1) = "," Then
    '                    strEmail = strEmail.Substring(1, strEmail.Length - 1)
    '                End If
    '            Catch ex As Exception
    '            End Try

    '            If clsCommon.myLen(strEmail) <= 0 Then
    '                clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
    '                Return
    '            End If

    '            oMsg.Body = obj.mailbody

    '            oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

    '            If oMsg.Body.Contains(clsEmailSMSConstants.fromDate) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.ToDate) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
    '            End If
    '            If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
    '                oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '            End If


    '            oMsg.Subject = obj.mailsubjct

    '            oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

    '            If oMsg.Subject.Contains(clsEmailSMSConstants.FromDate) Then
    '                oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
    '            End If
    '            If oMsg.Subject.Contains(clsEmailSMSConstants.ToDate) Then
    '                oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
    '            End If
    '            If oMsg.Subject.Contains(clsEmailSMSConstants.ReportType) Then
    '                oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ReportType, repotype)
    '            End If
    '            If oMsg.Subject.Contains(clsEmailSMSConstants.InvoiceType) Then
    '                oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '            End If

    '            '------------------------code for attchament-------------------------------------
    '            If obj.atchmnt = "Y" Then
    '                Dim sDisplayname As [String] = "MyAttachment"
    '                If oMsg.Body Is Nothing Then
    '                    oMsg.Body = " "
    '                End If
    '                Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
    '                Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

    '                Dim strRptPath As String = ""

    '                Dim oAttachment As Outlook.Attachment = Nothing
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

    '                If dt.Rows.Count > 0 Then
    '                    Dim subPath As String = Application.StartupPath + "\Mail Reports"

    '                    Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

    '                    If (IsExists = False) Then

    '                        System.IO.Directory.CreateDirectory(subPath)
    '                    End If
    '                    strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
    '                    'transportSql.exportdata(Gv1, strRptPath, "Sheet1")
    '                    transportSql.exportdata(Gv1, strRptPath, "Sheet1", False, Nothing, False, False, False, True)
    '                    oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
    '                End If
    '            End If
    '            '---------------------------------------------------------------------------


    '            oMsg.Recipients.Add(strEmail)
    '            oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
    '            oMsg.Send()
    '            oMsg = Nothing
    '            oApp = Nothing

    '            clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try

    '        Try
    '            Dim client As New System.Net.WebClient()

    '            If clsCommon.myLen(obj.smsbody) <= 0 Then
    '                Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
    '            End If

    '            Dim strMes As String = ""

    '            strMes = obj.smsbody
    '            strMes = strMes.Replace("'", " ").Replace("`", "/")

    '            If strMes.Contains(clsEmailSMSConstants.FromDate) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.ToDate) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.ReportType) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
    '            End If
    '            If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
    '                strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '            End If


    '            Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

    '            Try
    '                If strphone.Substring(0, 1) = "," Then
    '                    strphone = strphone.Substring(1, strphone.Length - 1)
    '                End If
    '            Catch ex As Exception
    '            End Try

    '            Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
    '            Dim data As Stream = client.OpenRead(baseurl)
    '            Dim reader As StreamReader = New StreamReader(data)
    '            Dim s As String = reader.ReadToEnd()
    '            data.Close()
    '            reader.Close()

    '            clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try

    'End Sub

    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    End Sub

    Private Sub rbtnItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnItemAll.ToggleStateChanged
        cbgItem.Enabled = rbtnItemSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        cbgLocation.Enabled = rbtnLocationSelect.IsChecked
        If rbtnLocationAll.IsChecked Then
            cbgLocation.CheckedAll()
        Else
            cbgLocation.UnCheckedAll()
        End If
    End Sub



    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptMCCsaleRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGO, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        Reset()
        rmSend.Visibility = ElementVisibility.Collapsed
    End Sub

    Private Sub RptMCCsaleRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub rbtnCustomerGroupAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        LoadCustomerGroup()
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub rmPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.PDF)
    End Sub

    Private Sub rmSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptMccSaleRegister
        frm.ShowDialog()
    End Sub

    Private Sub rmSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSend.Click
        'send()
    End Sub



    Private Sub btnGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGO.Click
        Print(Exporter.Refresh)
    End Sub

    Private Sub Gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        'If rdbSummary.IsChecked Then
        If Gv1.Rows.Count > 0 Then
            Dim strDoc = Gv1.CurrentRow.Cells("Document_Code").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strDoc)

        End If
        'End If
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
            'If rbtnLocationSelect.IsChecked Then
            '    Dim strlocName As String = ""
            '    For Each StrName As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strlocName) > 0 Then
            '            strlocName += ", "
            '        End If
            '        strlocName += StrName
            '    Next
            '    Dim strlocCode As String = ""
            '    For Each StrCode As String In cbgLocation.CheckedValue
            '        If clsCommon.myLen(strlocCode) > 0 Then
            '            strlocCode += ", "
            '        End If
            '        strlocCode += StrCode
            '    Next
            '    arrHeader.Add(("Location : " + strlocName + " "))

            'End If

            'If rbtnCustomerSelect.IsChecked Then
            '    Dim strCustomerName As String = ""
            '    For Each StrName As String In cbgCustomer.CheckedDisplayMember
            '        If clsCommon.myLen(strCustomerName) > 0 Then
            '            strCustomerName += ", "
            '        End If
            '        strCustomerName += StrName
            '    Next
            '    Dim strCustomerCode As String = ""
            '    For Each StrCode As String In cbgCustomer.CheckedValue
            '        If clsCommon.myLen(strCustomerCode) > 0 Then
            '            strCustomerCode += ", "
            '        End If
            '        strCustomerCode += StrCode
            '    Next
            '    arrHeader.Add(("Customer : " + strCustomerName + " "))

            'End If

            'If rbtnCustomerSelect.IsChecked Then
            '    Dim strCustomerName As String = ""
            '    For Each StrName As String In cbgCustomer.CheckedDisplayMember
            '        If clsCommon.myLen(strCustomerName) > 0 Then
            '            strCustomerName += ", "
            '        End If
            '        strCustomerName += StrName
            '    Next
            '    Dim strCustomerCode As String = ""
            '    For Each StrCode As String In cbgCustomer.CheckedValue
            '        If clsCommon.myLen(strCustomerCode) > 0 Then
            '            strCustomerCode += ", "
            '        End If
            '        strCustomerCode += StrCode
            '    Next
            '    arrHeader.Add(("Customer : " + strCustomerName + " "))

            'End If

            'If rbtnItemSelect.IsChecked Then
            '    Dim strItemName As String = ""
            '    For Each StrName As String In cbgItem.CheckedDisplayMember
            '        If clsCommon.myLen(strItemName) > 0 Then
            '            strItemName += ", "
            '        End If
            '        strItemName += StrName
            '    Next
            '    Dim strItemCode As String = ""
            '    For Each StrCode As String In cbgItem.CheckedValue
            '        If clsCommon.myLen(strItemCode) > 0 Then
            '            strItemCode += ", "
            '        End If
            '        strItemCode += StrCode
            '    Next
            '    arrHeader.Add(("Item : " + strItemName + " "))

            'End If
            If txtLocationMul.arrValueMember IsNot Nothing AndAlso txtLocationMul.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMul.arrDispalyMember))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If TxtMultiSelectCustomer.arrValueMember IsNot Nothing AndAlso TxtMultiSelectCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(TxtMultiSelectCustomer.arrDispalyMember))
            Else
                arrHeader.Add((" Customer : All"))
            End If
            If TxtMultiSelectItem.arrValueMember IsNot Nothing AndAlso TxtMultiSelectItem.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiSelectItem.arrDispalyMember))
            Else
                arrHeader.Add((" Item : All"))
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocationMul__My_Click(sender As Object, e As EventArgs) Handles txtLocationMul._My_Click
        Dim qry As String = Nothing
        qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "
        txtLocationMul.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroup", qry, "Code", "Name", txtLocationMul.arrValueMember, txtLocationMul.arrDispalyMember)
   End Sub

    Private Sub TxtMultiSelectCustomer__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectCustomer._My_Click
        Dim qry As String = "select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master"
        TxtMultiSelectCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroup", qry, "Code", "Name", TxtMultiSelectCustomer.arrValueMember, TxtMultiSelectCustomer.arrDispalyMember)
    End Sub

    Private Sub TxtMultiSelectItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectItem._My_Click
        Dim qry As String = "select item_code as [Code] ,item_Desc as [Name] from TSPL_ITEM_MASTER order by Item_Code"
        TxtMultiSelectItem.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroup", qry, "Code", "Name", TxtMultiSelectItem.arrValueMember, TxtMultiSelectItem.arrDispalyMember)
    End Sub
End Class
