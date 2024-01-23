'' work to be done agaist ticket no. BHA/26/11/18-000709,BHA/10/12/18-000746

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


Public Class RptRouteWiseSaleRegister
    Inherits FrmMainTranScreen
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Dim strCodeColumn As String = ""
    Dim arr As List(Of String) = New List(Of String)
    Dim CrateToLTR As Integer
    Dim CanToLTR As Integer


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ' ticket No : ERO/19/10/19-001070
    'Sanjay add route join
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        arr = Nothing
        SelFromDate = clsCommon.myCstr(txtToDate.Value.AddDays(-1).ToShortDateString())
        SelToDate = clsCommon.myCstr(txtToDate.Value.ToShortDateString())
        Dim strFromDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        Dim strSumColumn As String = ""

        If chkdemand.Checked = False Then

            Dim PivtQry = " select distinct  TSPL_ITEM_MASTER.Item_Desc+char(10)+'('+TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')' as Item_Desc "
            PivtQry += "         from TSPL_SD_SALE_INVOICE_HEAD"
            PivtQry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
            PivtQry += " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No"
            PivtQry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
            PivtQry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "
            PivtQry += " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code "
            PivtQry += " where 2=2 and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " + Environment.NewLine &
                   "CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" + strToDate + "',103)"
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable(PivtQry)
            strCodeColumn = ""
            If dtCategory Is Nothing OrElse dtCategory.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strSumColumn += "+"
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("Item_Desc")).Trim() + "]"
            Next

            Dim qry As String = "select *,[CRATE LTR]+[CAN LTR] as [Total LTR] from (select *,isnull(CRATE_Qty,0) as CRT,isnull(CAN_Qty,0) as CAN,isnull(BOX_Qty,0) as BOX, ISNULL(CRATE_Qty,0)+isnull(CAN_Qty,0)+isnull(BOX_Qty,0) as [Total],isnull(CarteQtyLtr,0) as [CRATE LTR],isnull(CanQtyLtr,0) as [CAN LTR] from ( select * from (select (TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as LocationCode,(tspl_location_master.location_desc) as LocationDesc,isnull(TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code,'') as [Customer Category Code],isnull(TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC,'') as [Customer Category Desc],(TSPL_SD_SALE_INVOICE_HEAD.Customer_Code) as Customer_Code,(TSPL_CUSTOMER_MASTER.Customer_Name) as CustomerName,(TSPL_ITEM_MASTER.Item_Desc+char(10)+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')') as Item_Desc " & Environment.NewLine &
        " ,(TSPL_SD_SALE_INVOICE_DETAIL.Qty) - isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
        " ,(TSPL_ROUTE_MASTER.Route_Desc) as Route,Customerqty,CAN_Qty,CRATE_Qty,BOX_Qty,cast(CRATE_Total.CarteQtyLtr as decimal(18,0) ) as CarteQtyLtr,cast(CAN_Total.CanQtyLtr as decimal(18,0) ) as CanQtyLtr " & Environment.NewLine &
        " from TSPL_SD_SALE_INVOICE_HEAD" & Environment.NewLine &
        " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & Environment.NewLine &
        " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No" & Environment.NewLine &
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location" & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
        " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
        " ---------------------------------------------- Total Qty ------------------------- " & Environment.NewLine &
        " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as Customerqty from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+char(13)+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
        " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
        " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
        " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " + Environment.NewLine &
        " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" + strToDate + "',103) "

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
            End If
            'If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
            '    PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ")" + Environment.NewLine
            'End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If

            qry += " )xx group by Customer_Code,Route_No) as CustomerTotal on CustomerTotal.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
             " and CustomerTotal.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
        " ------------------------------------------------- Total CAN ------------------------------------- " & Environment.NewLine &
        " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as CAN_Qty,sum(CanQtyLtr) as CanQtyLtr from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+char(13)+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
" ,(case when coalesce(stockLtr.Conversion_Factor,0)=0 then 0 else cast((TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0))* (Stock_SU.Conversion_Factor)/(coalesce(stockLtr.Conversion_Factor,1)) as numeric(18,3)) end) CanQtyLtr " & Environment.NewLine &
" from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
        " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
" left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =Stock_SU.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =Stock_SU.UOM_Code " & Environment.NewLine &
        " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =StockLtr.Item_Code " & Environment.NewLine &
 " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " & Environment.NewLine &
        " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" + strToDate + "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='CAN' "

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If

            qry += " )xx group by Customer_Code,Route_No) as CAN_Total on CAN_Total.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
             " and CAN_Total.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
        " -------------------------------------------- Total CRATE ----------------------------------- " & Environment.NewLine &
        " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as CRATE_Qty,sum(CarteQtyLtr) as CarteQtyLtr  from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+char(13)+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
        " ,(case when coalesce(stockLtr.Conversion_Factor,0)=0 then 0 else cast((TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0))* (Stock_SU.Conversion_Factor)/(coalesce(stockLtr.Conversion_Factor,1)) as numeric(18,3)) end) CarteQtyLtr " & Environment.NewLine &
        " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
        " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
        " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =Stock_SU.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =Stock_SU.UOM_Code " & Environment.NewLine &
        " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =StockLtr.Item_Code " & Environment.NewLine &
        " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " & Environment.NewLine &
        " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" + strToDate + "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='CRATE'"

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If

            qry += " )xx group by Customer_Code,Route_No) as CRATE_Total on CRATE_Total.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
            " and CRATE_Total.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
        " -------------------------------------- Total BOX ----------------------------------- " & Environment.NewLine &
        " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as BOX_Qty from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+char(13)+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
        " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
        " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
        " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
        " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " & Environment.NewLine &
        " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" + strToDate + "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='BOX'"

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If

            qry += "  )xx group by Customer_Code,Route_No) as BOX_Total on BOX_Total.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
        " and BOX_Total.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
        " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " & Environment.NewLine &
        " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" + strToDate + "',103)"

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER.Structure_Code in(" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If

            qry += " )Final " & Environment.NewLine &
        " pivot(Sum(Qty) for Item_Desc in (" + strCodeColumn + "))pvt" & Environment.NewLine &
        " ) finalQry" & Environment.NewLine &
        " ) LastQry"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.DataSource = dt

                gvData.Columns("Customer_Code").IsVisible = False
                gvData.Columns("Customerqty").IsVisible = False
                gvData.Columns("CAN_Qty").IsVisible = False
                gvData.Columns("CRATE_Qty").IsVisible = False
                gvData.Columns("BOX_Qty").IsVisible = False
                gvData.Columns("CarteQtyLtr").IsVisible = False
                gvData.Columns("CanQtyLtr").IsVisible = False


                SetGridFormationOFGV1()
                gvData.AutoExpandGroups = True
                gvData.ShowGroupPanel = True
                gvData.ShowRowHeaderColumn = False
                gvData.AllowAddNewRow = False
                gvData.AllowDeleteRow = False
                gvData.EnableFiltering = True
                gvData.ShowFilteringRow = True
                gvData.BestFitColumns()
            End If
        Else
            Printt()
        End If
    End Sub
    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        ' gvData.GroupDescriptors.Add(New GridGroupByExpression("Route as RouteName format ""{0}: {1}"" Group By Route"))

        For i As Integer = 9 To gvData.Columns.Count - 1
            Dim aa = gvData.Columns(i).HeaderText()
            Dim item8 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

        Next

        gvData.ShowGroupPanel = True
        gvData.MasterTemplate.AutoExpandGroups = True

        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterTemplate.ShowTotals = True
        'ReStoreGridLayout()

    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If gvData.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtMultItem.arrDispalyMember))
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add(" Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Route Wise Sale Register", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Route Wise Sale Register", gvData, arrHeader, "Route Wise Sale Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        txtMultItem.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtStructure.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        TxtMultiCustomerCategory.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub FrmPendingBookingReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub FrmPendingBookingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        Reset()
        CrateToLTR = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.CrateToLTR + "' and code='" + clsFixedParameterCode.CrateToLTR + "'"))
        CanToLTR = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.CanToLTR + "' and code='" + clsFixedParameterCode.CanToLTR + "'"))


    End Sub
    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub
    Private Sub txtStructure__My_Click(sender As Object, e As EventArgs) Handles txtStructure._My_Click
        Dim qry As String = " select distinct Structure_Code as Code,Structure_Desc as Name from TSPL_ITEM_MASTER "
        txtStructure.arrValueMember = clsCommon.ShowMultipleSelectForm("StrMulSel", qry, "Code", "Name", txtStructure.arrValueMember, txtStructure.arrDispalyMember)
    End Sub
    Private Sub txtMultItem__My_Click(sender As Object, e As EventArgs) Handles txtMultItem._My_Click
        Dim qry As String = " select Item_Code as [Code], Item_Desc as [Name] from TSPL_ITEM_MASTER "
        txtMultItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtMultItem.arrValueMember, txtMultItem.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_code as [Code], Location_Desc as [Name] from tspl_Location_master "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvData.Columns.Count - 1 Step ii + 1
                        gvData.Columns(ii).IsVisible = False
                        gvData.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvData.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs)
        If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            gvData.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvData.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvData.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)  ''delete layout
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim strQry As String = ""
        strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCategMulSel", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
    Sub Printt()
        'Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select * from (
                 SELECT TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise, TSPL_ROUTE_MASTER.Route_No,Total_Qty,tspl_item_master.Short_Description as Short_Description FROM TSPL_BOOKING_DETAIL 
                 LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code 
                 left outer join TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No 
                 left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No= TSPL_BOOKING_DETAIL.Document_No
				 left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.Item_Code= TSPL_BOOKING_DETAIL.Item_Code
                 left outer join tspl_item_master on tspl_item_master.Item_code=TSPL_BOOKING_DETAIL.Item_code WHERE IsTaxable='0' and Is_FreshItem='1' and TSPL_BOOKING_MATSER.Document_Date<='" & clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy") & "'  and (TSPL_BOOKING_MATSER.Document_Date >= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'  or TSPL_BOOKING_MATSER.Document_Date is null)) as st
				 PIVOT ( sum(Total_Qty)
         FOR Short_Description IN ([GM 500] , [GM 1LT] , [GM 6LT] , [SM 500] , [SM 1LT] , [TM 500] , [TM 1LT] , [TM 6LT])
         ) AS pivot1 "
        '  Dim whrClas As String = " From_Screen_code='" & clsUserMgtCode.frmDairyBookingCustomer & "'"
        '-------richa 17/12/2019 show customer according to customer permission Ticket No. ---------
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt

            'gvData.Columns("Customer_Code").IsVisible = False
            'gvData.Columns("Customerqty").IsVisible = False
            'gvData.Columns("CAN_Qty").IsVisible = False
            'gvData.Columns("CRATE_Qty").IsVisible = False
            'gvData.Columns("CouponCode").IsVisible = False
            'gvData.Columns("CouponDate").IsVisible = False
            'gvData.Columns("CanQtyLtr").IsVisible = False


            FormatGrid()
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = True
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            gvData.BestFitColumns()
        End If
        'End If
    End Sub

    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        gvData.TableElement.TableHeaderHeight = 25
        gvData.MasterTemplate.ShowRowHeaderColumn = True
        If chkdemand.Checked Then
            'gvData.Columns("Item_Code").IsVisible = True
            'gvData.Columns("Item_Code").Width = 100
            'gvData.Columns("Item_Code").HeaderText = "Item_Code"

            'gvData.Columns("route_no").IsVisible = True
            'gvData.Columns("route_no").Width = 100
            'gvData.Columns("route_no").HeaderText = " route_no"

            'gvData.Columns("item_desc").IsVisible = True
            'gvData.Columns("item_desc").Width = 100
            'gvData.Columns("item_desc").HeaderText = "item_desc"

            'gvData.Columns("item_Short_Description").IsVisible = True
            'gvData.Columns("item_Short_Description").Width = 100
            'gvData.Columns("item_Short_Description").HeaderText = "item_Short_Description"

            ''gvData.Columns("Shift Type").IsVisible = False
            ''gvData.Columns("Shift Type").Width = 100
            ''gvData.Columns("Shift Type").HeaderText = "Shift Type"

            'gvData.Columns("HSN_Code").IsVisible = True
            'gvData.Columns("HSN_Code").Width = 100
            'gvData.Columns("HSN_Code").HeaderText = "HSN_Code"

            'gvData.Columns("Scheme_Item_UOM").IsVisible = True
            'gvData.Columns("Scheme_Item_UOM").Width = 100
            'gvData.Columns("Scheme_Item_UOM").HeaderText = "Scheme_Item_UOM"

            'gvData.Columns("Delivery No").IsVisible = True
            'gvData.Columns("Delivery No").Width = 100
            'gvData.Columns("Delivery No").HeaderText = "Delivery No"

            'gvData.Columns("Customer Category Code").IsVisible = True
            'gvData.Columns("Customer Category Code").Width = 100
            'gvData.Columns("Customer Category Code").HeaderText = "Customer Category Code"

            'gvData.Columns("Booking Type").IsVisible = True
            'gvData.Columns("Booking Type").Width = 100
            'gvData.Columns("Booking Type").HeaderText = "Booking Type"

            'gvData.Columns("Against Demand Booking No").IsVisible = True
            'gvData.Columns("Against Demand Booking No").Width = 100
            'gvData.Columns("Against Demand Booking No").HeaderText = "Against Demand Booking No"

            'gvData.Columns("Coupon Code").IsVisible = True
            'gvData.Columns("Coupon Code").Width = 100
            'gvData.Columns("Coupon Code").HeaderText = "Coupon Code"

            'gvData.Columns("Coupon Date").IsVisible = True
            'gvData.Columns("Coupon Date").Width = 100
            'gvData.Columns("Coupon Date").HeaderText = "Coupon Date"

            'gvData.Columns("Is_FreshItem").IsVisible = True
            'gvData.Columns("Is_FreshItem").Width = 100
            'gvData.Columns("Is_FreshItem").HeaderText = "Is_FreshItem"

            'gvData.Columns("IsTaxable").IsVisible = True
            'gvData.Columns("IsTaxable").Width = 100
            'gvData.Columns("IsTaxable").HeaderText = "IsTaxable"
        End If
    End Sub
End Class
