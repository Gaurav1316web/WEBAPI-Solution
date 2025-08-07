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
Imports System.Text

Public Class RptRouteWiseSaleRegister
    Inherits FrmMainTranScreen
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Dim strCodeColumn As String = ""
    Dim arr As List(Of String) = New List(Of String)
    Dim CrateToLTR As Integer
    Dim CanToLTR As Integer
    Dim dtItem As DataTable = Nothing


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New UnauthorizedAccessException("Permission Denied")
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ' ticket No : ERO/19/10/19-001070
    'Sanjay add route join
    Sub GetReportGridID()
        Dim VarID As String = ""
        If chkdemand.Checked Then
            VarID += "_DE"
        End If
        gvData.VarID = VarID
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            GetReportGridID()
            PageSetupReport_ID = MyBase.Form_ID & gvData.VarID
            gvData.DataSource = Nothing
            gvData.Rows.Clear()
            arr = Nothing
            SelFromDate = clsCommon.myCstr(txtToDate.Value.AddDays(-1).ToShortDateString())
            SelToDate = clsCommon.myCstr(txtToDate.Value.ToShortDateString())
            Dim strFromDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            'Dim strSumColumn As String = ""


            If Not chkdemand.Checked Then
                Dim PivtQry = " select distinct  TSPL_ITEM_MASTER.Item_Desc+' '+'('+TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')' as Item_Desc,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.Sku_Seq "
                PivtQry += "         from TSPL_SD_SALE_INVOICE_HEAD"
                PivtQry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                PivtQry += " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No"
                PivtQry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
                PivtQry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "
                PivtQry += " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code "
                PivtQry += " where 2=2 and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" & strFromDate & "',103) AND " & Environment.NewLine &
                   "CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" & strToDate & "',103)"
                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" & clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) & ")" & Environment.NewLine
                End If
                If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" & clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" & clsCommon.GetMulcallString(txtStructure.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
                End If
                PivtQry += " Order By TSPL_ITEM_MASTER.Sku_Seq"
                Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable(PivtQry)
                Dim strCodeColumn As New StringBuilder()
                Dim strTotalItemColumn As New StringBuilder()
                If dtCategory Is Nothing OrElse dtCategory.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
#Disable Warning
                    Exit Sub
#Enable Warning
                End If
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If Not clsCommon.myCstr(strCodeColumn).Contains("[" & clsCommon.myCstr(dtCategory.Rows(ii)("Item_Desc")).Trim() & "]") Then
                        If ii <> 0 Then
                            strCodeColumn.Append(",")
                            strTotalItemColumn.Append(",")
                            'strCodeColumn.Append("+")
                        End If
                        strCodeColumn.Append("[" & clsCommon.myCstr(dtCategory.Rows(ii)("Item_Desc")).Trim() & "]")
                        strTotalItemColumn.Append("Sum([" & clsCommon.myCstr(dtCategory.Rows(ii)("Item_Desc")).Trim() & "]) As [" & clsCommon.myCstr(dtCategory.Rows(ii)("Item_Desc")).Trim() & "]")
                    End If
                Next


                Dim qry As String = "select Max(LocationCode) As [Location Code],Max(LocationDesc) As [Location Desc],Max([Customer Category Code])[Customer Category Code],Max([Customer Category Desc])[Customer Category Desc],[Route No],Max(Route)Route,Customer_Code As [Customer Code],Max(CustomerName) As [Customer Name]," & clsCommon.myCstr(strTotalItemColumn) & ",Sum(isnull(CRATE_Qty,0)) as CRT,Sum(isnull(CAN_Qty,0)) as CAN,Sum(ISNULL(Pouch_Qty,0)) As POUCH,Sum(isnull(BOX_Qty,0)) as BOX, Sum(ISNULL(CRATE_Qty,0)+isnull(CAN_Qty,0)+isnull(BOX_Qty,0)) as [Total],Sum(isnull(CarteQtyLtr,0)) as [CRATE LTR],Sum(ISNULL(PouchLTR,0)) As [POUCH LTR],Sum(isnull(CanQtyLtr,0)) as [CAN LTR],Sum(isnull(CarteQtyLtr,0) + ISNULL(PouchLTR,0) + isnull(CanQtyLtr,0)) As [Total LTR] from "
                qry += "(select (TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as LocationCode,(tspl_location_master.location_desc) as LocationDesc,isnull(TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code,'') as [Customer Category Code],isnull(TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC,'') as [Customer Category Desc],(TSPL_SD_SALE_INVOICE_HEAD.Customer_Code) as Customer_Code,(TSPL_CUSTOMER_MASTER.Customer_Name) as CustomerName,(TSPL_ITEM_MASTER.Item_Desc+' '+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')') as Item_Desc " & Environment.NewLine &
                " ,Cast((TSPL_SD_SALE_INVOICE_DETAIL.Qty) - isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) As decimal(18,2)) as Qty " & Environment.NewLine &
                " ,(TSPL_ROUTE_MASTER.Route_Desc) as Route,TSPL_ROUTE_MASTER.Route_No as [Route No],Customerqty,"
                qry += " Cast(CAN_Qty As decimal(18,2))CAN_Qty,Cast(CRATE_Qty As decimal(18,2))CRATE_Qty, Cast(Case When TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='Pouch' Then TSPL_SD_SALE_INVOICE_DETAIL.Qty Else 0 End As decimal(18,2)) As Pouch_Qty, Cast(BOX_Qty As decimal(18,2))BOX_Qty,cast(CRATE_Total.CarteQtyLtr as decimal(18,2)) as CarteQtyLtr,cast(CAN_Total.CanQtyLtr as decimal(18,2)) as CanQtyLtr,CAST(Case When TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='Pouch' Then TSPL_SD_SALE_INVOICE_DETAIL.Qty Else 0 End * TSPL_ITEM_UOM_DETAIL.Conversion_Factor/CinPouchLTR.Conversion_Factor As Decimal(18,2)) As PouchLTR "
                qry += " from TSPL_SD_SALE_INVOICE_HEAD" & Environment.NewLine &
                " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & Environment.NewLine &
                " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No" & Environment.NewLine &
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
                " left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code" & Environment.NewLine &
                " left Outer join (Select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL Where UOM_Code='LTR') CinPouchLTR ON CinPouchLTR.Item_Code=TSPL_ITEM_MASTER.Item_Code" & Environment.NewLine &
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location" & Environment.NewLine &
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
                " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
                " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
                " ---------------------------------------------- Total Qty ------------------------- " & Environment.NewLine &
                " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as Customerqty from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+' '+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
                " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
                " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
                " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
                " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" & strFromDate & "',103) AND " & Environment.NewLine &
                " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" & strToDate & "',103) "

                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" & clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) & ")" & Environment.NewLine
                End If
                If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" & clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
                End If
                'If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                '    PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" + clsCommon.GetMulcallString(txtStructure.arrValueMember) + ")" + Environment.NewLine
                'End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
                End If

                qry += " )xx group by Customer_Code,Route_No) as CustomerTotal on CustomerTotal.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
                 " and CustomerTotal.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
            " ------------------------------------------------- Total CAN ------------------------------------- " & Environment.NewLine &
            " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as CAN_Qty,sum(CanQtyLtr) as CanQtyLtr from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+' '+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
    " ,(case when coalesce(stockLtr.Conversion_Factor,0)=0 then 0 else cast((TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0))* (Stock_SU.Conversion_Factor)/(coalesce(stockLtr.Conversion_Factor,1)) as numeric(18,3)) end) CanQtyLtr " & Environment.NewLine &
    " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
    " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =Stock_SU.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =Stock_SU.UOM_Code " & Environment.NewLine &
            " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =StockLtr.Item_Code " & Environment.NewLine &
     " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" & strFromDate & "',103) AND " & Environment.NewLine &
            " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" & strToDate & "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='CAN' "

                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" & clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) & ")" & Environment.NewLine
                End If
                If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" & clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" & clsCommon.GetMulcallString(txtStructure.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
                End If

                qry += " )xx group by Customer_Code,Route_No) as CAN_Total on CAN_Total.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
                 " and CAN_Total.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
            " -------------------------------------------- Total CRATE ----------------------------------- " & Environment.NewLine &
            " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as CRATE_Qty,sum(CarteQtyLtr) as CarteQtyLtr  from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+' '+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
            " ,(case when coalesce(stockLtr.Conversion_Factor,0)=0 then 0 else cast((TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0))* (Stock_SU.Conversion_Factor)/(coalesce(stockLtr.Conversion_Factor,1)) as numeric(18,3)) end) CarteQtyLtr " & Environment.NewLine &
            " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
            " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =Stock_SU.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =Stock_SU.UOM_Code " & Environment.NewLine &
            " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =StockLtr.Item_Code " & Environment.NewLine &
            " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" & strFromDate & "',103) AND " & Environment.NewLine &
            " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" & strToDate & "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='CRATE'"

                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" & clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) & ")" & Environment.NewLine
                End If
                If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" & clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" & clsCommon.GetMulcallString(txtStructure.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
                End If

                qry += " )xx group by Customer_Code,Route_No) as CRATE_Total on CRATE_Total.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
                " and CRATE_Total.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
            " -------------------------------------- Total BOX ----------------------------------- " & Environment.NewLine &
            " left outer join (select Route_No,Customer_Code,max(Item_Desc) as Item_Desc,sum(Qty) as BOX_Qty from (select TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_ITEM_MASTER.Item_Desc+' '+'('+ TSPL_SD_SALE_INVOICE_DETAIL.Unit_code+')'  as Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.qty- isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) as Qty " & Environment.NewLine &
            " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code" & Environment.NewLine &
            " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" & strFromDate & "',103) AND " & Environment.NewLine &
            " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" & strToDate & "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code='BOX'"

                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" & clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) & ")" & Environment.NewLine
                End If
                If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" & clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                    PivtQry += " and TSPL_ITEM_MASTER.Structure_Code in(" & clsCommon.GetMulcallString(txtStructure.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
                End If

                qry += "  )xx group by Customer_Code,Route_No) as BOX_Total on BOX_Total.Customer_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & Environment.NewLine &
            " and BOX_Total.Route_No=TSPL_SD_SALE_INVOICE_HEAD.Route_No " & Environment.NewLine &
            " where 2=2  and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) >= convert(date,'" & strFromDate & "',103) AND " & Environment.NewLine &
            " CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.document_Date,103) <= convert(date,'" & strToDate & "',103)"

                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in(" & clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) & ")" & Environment.NewLine
                End If
                If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" & clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_Detail.Item_Code in(" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Structure_Code in(" & clsCommon.GetMulcallString(txtStructure.arrValueMember) & ")" & Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
                End If

                qry += " )Final pivot(Sum(Qty) for Item_Desc in (" & clsCommon.myCstr(strCodeColumn) & "))pvt Group By [Route No],Customer_Code Order By [Route No],Customer_Code"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
#Disable Warning
                    Exit Sub
#Enable Warning
                Else
                    gvData.Rows.Clear()
                    gvData.Columns.Clear()
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gvData.GroupDescriptors.Clear()
                    gvData.MasterTemplate.SummaryRowsBottom.Clear()
                    gvData.DataSource = Nothing
                    gvData.Refresh()
                    gvData.DataSource = dt
                    SetGridFormationOFGV1()
                    ReStoreGridLayout()
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For i As Integer = (gvData.Columns("Customer Name").Index + 1) To gvData.Columns.Count - 1
            Dim aa = gvData.Columns(i).HeaderText()
            Dim item8 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
        Next

        gvData.ShowGroupPanel = True
        gvData.MasterTemplate.AutoExpandGroups = True
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gvData.MasterTemplate.ShowTotals = True
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
            Dim strtemp As String = "Date Range : " & clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " & clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If
            If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " & clsCommon.GetMulcallStringWithComma(txtMultItem.arrDispalyMember))
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add(" Route : " & clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " & clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer Category : " & clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Route Wise Sale Register", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Route Wise Sale Register", gvData, arrHeader, "Route Wise Sale Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
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
        ItemType()
    End Sub
    Private Sub FrmPendingBookingReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub FrmPendingBookingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        Reset()
        CrateToLTR = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" & clsFixedParameterType.CrateToLTR & "' and code='" & clsFixedParameterCode.CrateToLTR & "'"))
        CanToLTR = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" & clsFixedParameterType.CanToLTR & "' and code='" & clsFixedParameterCode.CanToLTR & "'"))


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
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If obj IsNot Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
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
        Dim dt As DataTable = Nothing
        'Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Unit_code AS UOM,Sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise) AS [Item Wise Total Crates],
Sum(Case When TSPL_DEMAND_BOOKING_DETAIL.Unit_code='Pouch' Then TSPL_DEMAND_BOOKING_DETAIL.Qty Else 0 End) As [Item Wise Total Pouch],
        Sum(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) AS [Item Wise Total LTR],Max(TSPL_ITEM_MASTER.Short_Description)Short_Description,
        Max(TSPL_ITEM_MASTER.Short_Description+' '+TSPL_DEMAND_BOOKING_DETAIL.Unit_code) As [Short Desc With UOM],
        Sum(CAST(TSPL_DEMAND_BOOKING_DETAIL.Qty AS DECIMAL(18,2))) AS Qty
    FROM TSPL_DEMAND_BOOKING_DETAIL 
    LEFT JOIN TSPL_DEMAND_BOOKING_MASTER  
        ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
    LEFT JOIN TSPL_ITEM_MASTER  
        ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
    LEFT JOIN TSPL_CUSTOMER_MASTER  
        ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
    LEFT JOIN TSPL_CUSTOMER_CATEGORY_MASTER 
        ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE = TSPL_CUSTOMER_MASTER.cust_category_code
    WHERE 1=1 "
        If rbtnFresh.Checked Then
            qry += " AND Is_FreshItem = '1' "
        ElseIf rbtnProduct.Checked Then
            qry += " AND Is_Ambient = '1' "
        End If
        qry += " AND TSPL_DEMAND_BOOKING_MASTER.Document_Date >= '" & clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy") & "' AND TSPL_DEMAND_BOOKING_MASTER.Document_Date <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "


        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" & clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) & ")" & Environment.NewLine
        End If
        If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" & clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) & ")" & Environment.NewLine
        End If
        If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
            qry += " and TSPL_DEMAND_BOOKING_DETAIL.Item_Code in(" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")" & Environment.NewLine
        End If
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
        End If
        If txtStructure.arrValueMember IsNot Nothing AndAlso txtStructure.arrValueMember.Count > 0 Then
            qry += " and TSPL_ITEM_MASTER.Structure_Code in(" & clsCommon.GetMulcallString(txtStructure.arrValueMember) & ")" & Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_DEMAND_BOOKING_MASTER.Location_Code in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
        End If

        qry += " Group By TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Unit_code"


        dt = clsDBFuncationality.GetDataTable(qry & " ,TSPL_ITEM_MASTER.Sku_Seq Order By TSPL_ITEM_MASTER.Sku_Seq")

        Dim strItemColumn As New StringBuilder()
        Dim strSumItemColumn As New StringBuilder()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
#Disable Warning
            Exit Sub
#Enable Warning
        End If
        For ii As Integer = 0 To dt.Rows.Count - 1
            If Not clsCommon.myCstr(strItemColumn).Contains("[" & clsCommon.myCstr(dt.Rows(ii)("Short Desc With UOM")).Trim() & "]") Then
                If ii <> 0 Then
                    strItemColumn.Append(",")
                    strSumItemColumn.Append(",")
                    'strCodeColumn.Append("+")
                End If
                strItemColumn.Append("[" & clsCommon.myCstr(dt.Rows(ii)("Short Desc With UOM")).Trim() & "]")
                strSumItemColumn.Append("Sum([" & clsCommon.myCstr(dt.Rows(ii)("Short Desc With UOM")).Trim() & "]) As " & "[" & clsCommon.myCstr(dt.Rows(ii)("Short Desc With UOM")).Trim() & "]")
            End If
        Next

        dtItem = dt.DefaultView.ToTable(True, "Item_Code", "Short_Description")

        Dim strFinalQry As String = "SELECT Route_No As [Route No],Sum([Item Wise Total Crates])[Total Crates],Sum([Item Wise Total Pouch])[Total Pouch],Sum([Item Wise Total LTR])[Total LTR],"
        strFinalQry += "" & clsCommon.myCstr(strSumItemColumn) & ""
        strFinalQry += "FROM (" & qry & ")AS SourceTable PIVOT (SUM(Qty) FOR [Short Desc With UOM] IN (" & clsCommon.myCstr(strItemColumn) & ")) AS PivotTable 
Group By Route_No ORDER BY Route_No"

        dt = Nothing
        dt = clsDBFuncationality.GetDataTable(strFinalQry)
        strItemColumn = Nothing
        strSumItemColumn = Nothing
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
#Disable Warning
            Exit Sub
#Enable Warning
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt
            FormatGrid()
            ReStoreGridLayout()
            View()
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

    Sub View()
        Try
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns(0).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns(1).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns(2).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns(3).Name)
            For i As Integer = 0 To dtItem.Rows.Count - 1
                Dim shortDesc As String = clsCommon.myCstr(dtItem.Rows(i)("Short_Description"))
                view.ColumnGroups.Add(New GridViewColumnGroup(shortDesc))
                view.ColumnGroups(i + 1).Rows.Add(New GridViewColumnGroupRow())

                For j As Integer = 4 To gvData.Columns.Count - 1
                    If gvData.Columns(j).Name.Contains(shortDesc) Then
                        Dim ColumnName As String = gvData.Columns(j).Name
                        Dim UOMName As String = ColumnName.Replace(shortDesc, "").Trim()
                        gvData.Columns(j).HeaderText = UOMName
                        view.ColumnGroups(i + 1).Rows(0).ColumnNames.Add(ColumnName)
                    End If
                Next
            Next
            gvData.ViewDefinition = view
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub FormatGrid()
        Try
            gvData.TableElement.TableHeaderHeight = 25
            gvData.MasterTemplate.ShowRowHeaderColumn = True

            gvData.TableElement.TableHeaderHeight = 40
            gvData.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gvData.Columns.Count - 1
                gvData.Columns(ii).ReadOnly = True
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For i As Integer = 1 To gvData.Columns.Count - 1
                Dim aa = gvData.Columns(i).HeaderText()
                Dim item8 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item8)
            Next

            gvData.ShowGroupPanel = True
            gvData.MasterTemplate.AutoExpandGroups = True
            gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            gvData.MasterTemplate.ShowTotals = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub rmiSaveLayout_Click(sender As Object, e As EventArgs) Handles rmiSaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                gvData.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvData.SaveLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = gvData.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                ''stuti regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ReStoreGridLayout()
                ''---------------
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmiDeleteLayout.Click
        Try
            If clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode) Then
                clsCommon.MyMessageBoxShow(Me, "Layout Deleted !", "Information", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkdemand_CheckStateChanged(sender As Object, e As EventArgs) Handles chkdemand.CheckStateChanged
        Try
            ItemType()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub ItemType()
        If chkdemand.Checked Then
            RadGroupBox1.Visible = True
            rbtnFresh.Checked = True
        Else
            RadGroupBox1.Visible = False
            rbtnFresh.Checked = False
            rbtnProduct.Checked = False
            rbtnBoth.Checked = False
        End If
    End Sub
End Class
