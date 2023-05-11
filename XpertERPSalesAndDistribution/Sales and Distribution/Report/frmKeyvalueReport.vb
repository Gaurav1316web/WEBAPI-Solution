
' '' ''for bug no BM00000000563
Imports XpertERPEngine
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Public Class FrmKeyvalueReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.KeyValue)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

   

    Sub Loadlocation()
        cbglocation.DataSource = clsLocation.GetLocationSegments()
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Name"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Code"
    End Sub
    Sub LoadItem()
        Dim qry As String = "select Item_Code as [Item Code],Item_Desc as [Item Description] from TSPL_ITEM_MASTER where Item_Type='F' "
        cbgItem1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem1.ValueMember = "Item Code"
        cbgItem1.DisplayMember = "Item Description"
    End Sub
    Sub LoadRoute()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
   
   
    Private Sub FrmKeyvalueReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chkItemAll1.IsChecked = True
        LoadItem()
        LoadCustomer()
        LoadRoute()
        Loadlocation()
        rdbSku.IsChecked = True
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        rdbPost.IsChecked = True
    End Sub

    Sub reset()
        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chkItemAll1.IsChecked = True
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        rdbPost.IsChecked = True
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Sub print()
        Try
            Dim strReturnPost, strPost As String
            If rdbAll.IsChecked = True Then
                strReturnPost = ""
                strPost = ""
            Else
                strReturnPost = " and TSPL_SALE_RETURN_HEAD.Is_Post='Y' "
                strPost = " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y' "
            End If

            GV1.EnableFiltering = True
            Dim dt As DataTable
            If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkChkSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer or select ALL")
                Return
            ElseIf chkItemSelect1.IsChecked = True AndAlso cbgItem1.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Item or select ALL")
                Return
            ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Route or select ALL")
                Return

            End If
            Dim LocName, strFOCRetGroup, strFOCInvGroup, strCustAll, strLocAll, strItemAll, strRouteAll, strSQL1Group, strOrderColumn, strOrderBy As String
            LocName = ""
            strOrderBy = ""
            strSQL1Group = ""
            strOrderColumn = ""
            If chklocAll.IsChecked = True Then
                strLocAll = "Y"
            Else
                strLocAll = "N"
            End If
            If chkItemAll1.IsChecked = True Then
                strItemAll = "Y"
            Else
                strItemAll = "N"
            End If
            If chkRouteAll.IsChecked = True Then
                strRouteAll = "Y"
            Else
                strRouteAll = "N"
            End If
            If chkCustAll.IsChecked = True Then
                strCustAll = "Y"
            Else
                strCustAll = "N"
            End If




            If rdbSku.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_MASTER.Item_Code"
                strFOCInvGroup = "TSPL_SALE_INVOICE_DETAIL.Main_Item"
                strFOCRetGroup = "TSPL_SALE_Return_DETAIL.Main_Item"
                strOrderColumn = "Sku_Seq"
                strOrderBy = "Order By Sku_Seq"
            ElseIf rdbPack.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc"
                strFOCInvGroup = "TSPL_ITEM_DETAILS.Class_Desc"
                strFOCRetGroup = "TSPL_ITEM_DETAILS.Class_Desc"
                strOrderColumn = "Pack_Seq"
                strOrderBy = "Order By Pack_Seq"
            ElseIf rdbFlavour.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strFOCInvGroup = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strFOCRetGroup = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strOrderColumn = "Flavour_Seq"
                strOrderBy = "Order By Flavour_Seq"
            End If
            If chklocSelect.IsChecked AndAlso cbglocation.CheckedValue.Count > 0 Then
                For ii As Integer = 0 To cbglocation.CheckedDisplayMember.Count - 1
                    If clsCommon.myLen(LocName) > 0 Then
                        LocName += ", "
                    End If
                    LocName += clsCommon.myCstr(cbglocation.CheckedDisplayMember(ii))
                Next
            Else
                LocName = "All"
            End If





            '''''   Sale invoice data where main item unit is FC and scheme item N  and FOC item is Y .It will include quantitiave scheme and maunal scheme 
            '''''   of MAin item of FC unit.

            Dim strInvoice As String = "select Item_Code,MRP_Amt,SUM(FOCAMt) as FOCAMt,max(Invoice_Qty)+ sum(FOCQty ) as saleqty,max(Basicamt)+ sum(FOCBasicamt ) as Basicamt, Sale_Invoice_No from ( " & _
            "select (TSPL_SALE_INVOICE_DETAIL.Basic_Rate* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as Basicamt, " & _
            "isnull((TSPL_SALE_INVOICE_DETAIL_1.Basic_Rate * TSPL_SALE_INVOICE_DETAIL_1.Invoice_Qty),0) as FOCBasicamt, " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No,TSPL_SALE_INVOICE_DETAIL.Item_Code, " & _
            "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as Invoice_Qty,isnull(TSPL_SALE_INVOICE_DETAIL_1.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) as FOCQty , " & _
            "case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then TSPL_SALE_INVOICE_DETAIL.MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " & _
            "TSPL_SALE_INVOICE_DETAIL.MRP_Amt end as MRP_Amt,isnull(((TSPL_SALE_INVOICE_DETAIL_1.Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((TSPL_SALE_INVOICE_DETAIL_1.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (TSPL_SALE_INVOICE_DETAIL_1.Price_Amount1 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount2 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount3 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount4 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount5 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount6 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount7 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount8 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount9))),0) AS FOCAMt " & _
            "from TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " & _
            "TSPL_SALE_INVOICE_DETAIL as  TSPL_SALE_INVOICE_DETAIL_1 on " & _
            "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL_1.Sale_Invoice_No  and " & _
            "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL_1.Main_Item and " & _
            "TSPL_SALE_INVOICE_DETAIL_1.Scheme_Item <> 'N' left outer join TSPL_ITEM_UOM_DETAIL on " & _
            "TSPL_SALE_INVOICE_DETAIL_1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_INVOICE_DETAIL_1.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join TSPL_ITEM_UOM_DETAIL as  " & _
            "TSPL_ITEM_UOM_DETAIL_1 on  TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "where TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB'  and TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N' and " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' and  " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strPost & " " & _
            ") a group by Item_Code,MRP_Amt,Sale_Invoice_No "


           
            strInvoice += " union all  "


            '''''   Sale invoice data where main item unit is FB and scheme item N  and FOC item is Y .It will include those item where main item 
            '''''   with only FB unit in that invoice of quantitiave scheme and maunal scheme.Sub query will check count of unit for item in every 
            '''''   invoice . if count =1  it means invoice has no item with fc unit and foc item is free with this FB item so It will use sale qty and FOC qty  ,and
            '''''   if count > 1  it means invoice has  item with fc unit and foc item is not free with this FB item so It will not use sale qty and FOC qty , this 
            '''''   FOC  is already inculded in 1st query 

            strInvoice += "select Item_Code,MRP_Amt,case when FBCount=1 then FOCAMt else 0 end as FOCAMt, " & _
            "case when FBCount=1 then (saleqty) else 0 end as saleqty, " & _
            "case when FBCount=1 then (Basicamt) else 0 end as Basicamt, Sale_Invoice_No from ( " & _
            "select (TSPL_SALE_INVOICE_DETAIL_1.Basic_Rate* TSPL_SALE_INVOICE_DETAIL_1.Invoice_Qty) as Basicamt,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.MRP_Amt as  MRP_Amt, " & _
            "(select distinct COUNT(Unit_code) from TSPL_SALE_INVOICE_DETAIL as a where " & _
            "a.Sale_Invoice_No= TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and " & _
            "a.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and Scheme_Item='N') as FBCount, " & _
            "isnull(((TSPL_SALE_INVOICE_DETAIL_1.Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((TSPL_SALE_INVOICE_DETAIL_1.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (TSPL_SALE_INVOICE_DETAIL_1.Price_Amount1 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount2 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount3 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount4 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount5 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount6 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount7 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount8 + TSPL_SALE_INVOICE_DETAIL_1.Price_Amount9))),0) as FOCAMt , " & _
            "isnull((TSPL_SALE_INVOICE_DETAIL_1.Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0)   as saleqty, " & _
            "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No from  TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " & _
            "TSPL_SALE_INVOICE_DETAIL as  TSPL_SALE_INVOICE_DETAIL_1 on " & _
            "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL_1.Sale_Invoice_No  and " & _
            "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL_1.Main_Item and " & _
            "TSPL_SALE_INVOICE_DETAIL_1.Scheme_Item <> 'N' left outer join TSPL_ITEM_UOM_DETAIL on " & _
            "TSPL_SALE_INVOICE_DETAIL_1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_INVOICE_DETAIL_1.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            "where TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N'   and TSPL_SALE_INVOICE_DETAIL.Unit_code='FB'  and  " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strPost & " " & _
            ") bb "

            strInvoice += " union all  "



            '''''   Sale invoice data where target discount is applied .qty will be same for both sale and FOC

            strInvoice += "select TSPL_SALE_INVOICE_DETAIL.Item_Code,case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then " & _
            "TSPL_SALE_INVOICE_DETAIL.MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  else MRP_Amt end as MRP_Amt, " & _
            "((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((TSPL_SALE_INVOICE_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + TSPL_SALE_INVOICE_DETAIL.Price_Amount4 + TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + TSPL_SALE_INVOICE_DETAIL.Price_Amount8 + TSPL_SALE_INVOICE_DETAIL.Price_Amount9))) AS FOCAMt, " & _
            "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as saleqty, " & _
            "(TSPL_SALE_INVOICE_DETAIL.Basic_Rate* TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as Basicamt, " & _
            "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No from TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code  left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as  TSPL_ITEM_UOM_DETAIL_1 on  TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "where TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB'  and TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y'   and TSPL_SALE_INVOICE_DETAIL.Discount_Code <> ''  and " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strPost & " "

            strInvoice += " union all  "

            '''''   Sale invoice data where unit is FB and no scheme is applied .(Only for sale qty of FB  item .)

            strInvoice += "select TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.MRP_Amt as MRP_Amt,0 AS FOCAMt, " & _
            "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/Conversion_Factor as saleqty, " & _
            "(TSPL_SALE_INVOICE_DETAIL.Basic_Rate * TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) as Basicamt, " & _
            "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No from " & _
            "TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code  where TSPL_SALE_INVOICE_DETAIL.Scheme_Item='N'   and " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code='FB' and " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strPost & " "

            strInvoice = "select AA.Item_Code,MRP_Amt, FOCAMt,CONVERT(DECIMAL(18,2),saleqty) AS saleqty,Basicamt,AA.Sale_Invoice_No from ( " & strInvoice & "  )AA "

            Dim strReturn As String = "select  c.Item_Code,c.MRP_Amt,sum(FOCAMt) as FOCAMt, " & _
            "max(SaleQty)  + SUM(FOCqty) as saleqty,max(basicamt)  + SUM(FOCbasicamt) as Basicamt, " & _
            "c.Sale_Return_No  From " & _
            "(select aa.Item_Code, aa.MRP_Amt,isnull(((b.Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((b.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (b.Price_Amount1 + b.Price_Amount2 + b.Price_Amount3 + b.Price_Amount4 + b.Price_Amount5 + b.Price_Amount6 + b.Price_Amount7 + b.Price_Amount8 + b.Price_Amount9))),0) as FOCAMt, " & _
            "(SaleQty) , isnull(b.Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0) as FOCqty, " & _
            " basicamt,isnull((b.Basic_Rate * b.Return_Qty),0) as FOCbasicamt, aa.Sale_Return_No from ( " & _
            "select a.Item_Code,a.MRP_Amt,MAX(SaleQty) as SaleQty,MAX(basicamt) as basicamt,Sale_Return_No from ( " & _
            "select TSPL_SALE_INVOICE_DETAIL.Item_Code,case when TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' then " & _
            "TSPL_SALE_INVOICE_DETAIL.MRP_Amt/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else TSPL_SALE_INVOICE_DETAIL.MRP_Amt end MRP_Amt, " & _
            "0 as FOCAMt,0 as SaleQty,0 as basicamt,Main_Item,Scheme_Item,Unit_code,TSPL_SALE_RETURN_HEAD.Sale_Return_No  from " & _
            "TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
            "TSPL_SALE_RETURN_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  where " & _
            "Scheme_Item='N' and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' and " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strReturnPost & " " & _
            "union all " & _
            "select TSPL_SALE_return_DETAIL.Item_Code,case when TSPL_SALE_return_DETAIL.Unit_code='FC' then " & _
            "TSPL_SALE_return_DETAIL.MRP_Amt/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else TSPL_SALE_return_DETAIL.MRP_Amt end as MRP_Amt, " & _
            "0 as FOCAMt,Return_Qty as SaleQty,(TSPL_SALE_return_DETAIL.Basic_Rate * TSPL_SALE_return_DETAIL.Return_Qty) as basicamt, " & _
            "Main_Item,Scheme_Item,Unit_code,TSPL_SALE_return_HEAD.Sale_Return_No " & _
            "from TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_return_DETAIL on " & _
            "TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_return_DETAIL.Sale_Return_No left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_return_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code " & _
            "where Scheme_Item='N' and Unit_code='FC'  and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' and " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strReturnPost & " " & _
            ")a group by Item_Code,MRP_Amt,Sale_Return_No " & _
            ") aa left outer join TSPL_SALE_RETURN_DETAIL as b on aa.Sale_Return_No=b.Sale_Return_No and aa.Item_Code=b.Main_Item and " & _
            "b.Scheme_Item <> 'N'  left outer join TSPL_ITEM_UOM_DETAIL on b.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
            "b.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            ") c   group by Item_Code,MRP_Amt,Sale_Return_No "

            strReturn += " union all  "

            strReturn += " select c.Item_Code,c.MRP_Amt,case when FBCount=1 then FOCAMt else 0 end as FOCAMt, " & _
            "case when FBCount=1 then (saleqty) else 0 end as saleqty, " & _
            "case when FBCount=1 then (Basicamt) else 0 end as Basicamt,c.Sale_Return_No from ( " & _
            "select aa.Item_Code,aa.MRP_Amt,(select distinct COUNT(Unit_code) from TSPL_SALE_INVOICE_DETAIL as a  " & _
            "where a.Sale_Invoice_No= aa.Sale_Invoice_No and a.Item_Code=aa.Item_Code and Scheme_Item='N') as FBCount, " & _
            "isnull(((b.Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((b.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (b.Price_Amount1 + b.Price_Amount2 + b.Price_Amount3 + b.Price_Amount4 + b.Price_Amount5 + b.Price_Amount6 + b.Price_Amount7 + b.Price_Amount8 + b.Price_Amount9))),0) as FOCAMt, " & _
            "(SaleQty) as saleqty,Basicamt, aa.Sale_Return_No,aa.Scheme_Item,aa.Main_Item,aa.Unit_code,aa.Sale_Invoice_No from " & _
            "(select Item_Code,MRP_Amt,0 as FOCAMt,sum(SaleQty) as saleqty,sum(basicamt) as Basicamt, Sale_Return_No,Scheme_Item,Main_Item,Unit_code,Sale_Invoice_No from ( " & _
            "select Item_Code,MRP_Amt,0 as SaleQty,0 as basicamt,Main_Item,Scheme_Item,Unit_code,TSPL_SALE_RETURN_HEAD.Sale_Return_No, " & _
            "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  from TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
            "TSPL_SALE_RETURN_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No where Scheme_Item='N' and " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strReturnPost & " " & _
            "union all " & _
            "select Item_Code,MRP_Amt,Return_Qty as SaleQty,Basic_Rate * Return_Qty as basicamt,Main_Item,Scheme_Item,Unit_code,TSPL_SALE_return_HEAD.Sale_Return_No, " & _
            "Invoice_No as Sale_Invoice_No  from TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_return_DETAIL on " & _
            "TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_return_DETAIL.Sale_Return_No where " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strReturnPost & " " & _
            ") a  " & _
            "group by Sale_Invoice_No,Sale_Return_No,Unit_code,Main_Item,Item_Code,MRP_Amt,Scheme_Item  ) aa " & _
            "left outer join TSPL_SALE_RETURN_DETAIL as b on aa.Sale_Return_No=b.Sale_Return_No and " & _
            "aa.Item_Code=b.Main_Item and b.Scheme_Item <> 'N'  left outer join TSPL_ITEM_UOM_DETAIL on " & _
            "b.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and b.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "where  aa.Scheme_Item='N' and aa.Unit_code='FB' " & _
            ")c "

            strReturn += " union all  "

            strReturn += " select TSPL_SALE_return_DETAIL.Item_Code,case when Unit_code='FC' then " & _
            "MRP_Amt/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor  else MRP_Amt end as MRP_Amt, " & _
            "isnull(((Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))),0) as FOCAMt, " & _
            "Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as SaleQty,Basic_Rate * Return_Qty as basicamt, " & _
            "TSPL_SALE_RETURN_HEAD.sale_return_no from  " & _
            "TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_return_DETAIL on " & _
            "TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_return_DETAIL.Sale_Return_No left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_return_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_return_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_SALE_return_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code where " & _
            "Scheme_Item='Y'  and Discount_Code <> ''   and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strReturnPost & " "

            strReturn += " union all  "

            strReturn += " select TSPL_SALE_return_DETAIL.Item_Code,MRP_Amt,0 as FOCAMt, " & _
            "Return_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as SaleQty,Basic_Rate*Return_Qty as basicamt, " & _
            "TSPL_SALE_RETURN_HEAD.sale_return_no from  TSPL_SALE_RETURN_HEAD left outer join  " & _
            "TSPL_SALE_return_DETAIL on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_return_DETAIL.Sale_Return_No left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_return_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_return_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where  Scheme_Item='N'  and Unit_code='FB' and " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strReturnPost & " "

            strReturn = " select Item_Code,MRP_Amt, - FOCAMt as FOCAMt, - (CONVERT(DECIMAL(18,2),saleqty)) AS saleqty,-(CONVERT(DECIMAL(18,2),Basicamt)) as Basicamt, " & _
            "Invoice_No as Sale_Invoice_No from ( " & strReturn & ") d left outer join TSPL_SALE_RETURN_HEAD on " & _
            "d.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No "

            'Dim strQuery As String = "select '" & LocName & "' as StrLoc,'" & dtpstart.Value & "' as Fdate,'" & dtpend.Value & "' as Tdate,Route_No,Route_Desc,item_code,convert(decimal(18,2),MRP_Amt) as MRP, " & _
            '"convert(decimal(18,2),sum(SaleQty)) as NetsaleQTy, " & _
            '"convert(decimal(18,2),SUM(basicamt)) as basicamt, " & _
            '"case when sum(SaleQty) > 0 then convert(decimal(18,2),SUM(basicamt)/sum(SaleQty)) else 0 end as basicrate, " & _
            '"convert(decimal(18,2),sum(FOCAMt)) as TradeTotal, " & _
            '"convert(decimal(18,2),case when sum(SaleQty) > 0 then (sum(FOCAMt)/sum(SaleQty)) else sum(SaleQty) end) as TradePerCase " & _
            '"from ( " & str1 & ") aa group by Route_No,Route_Desc,item_code,MRP_Amt," & strOrderColumn & " " & strOrderBy & " "

            Dim strQuery As String = "select " & strOrderColumn & "," & strSQL1Group & " as Item_Code,MRP_Amt,FOCAMt, saleqty,Basicamt,final.Sale_Invoice_No,TSPL_SALE_INVOICE_HEAD.Cust_Code, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_SALE_INVOICE_HEAD.Location, " & _
            "TSPL_LOCATION_MASTER.Location_Desc from ( " & _
            "select Item_Code,MRP_Amt,SUM(FOCAMt) as FOCAMt,SUM(saleqty) as saleqty,SUM(Basicamt) as Basicamt,Sale_Invoice_No from ( " & strInvoice & " union all " & strReturn & "  )e " & _
            "group by Item_Code,Sale_Invoice_No,MRP_Amt " & _
            ") final LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=final.Sale_Invoice_No " & _
            "LEFT OUTER JOIN TSPL_ITEM_DETAILS ON final.Item_Code=TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
            "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON final.Item_Code=TSPL_ITEM_DETAILS_1.Item_Code left outer join " & _
            "TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join " & _
            "TSPL_ROUTE_MASTER on TSPL_SALE_INVOICE_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_ITEM_MASTER on final.Item_Code=TSPL_ITEM_MASTER.Item_Code WHERE  (TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
            "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour' )"

            If strLocAll = "N" Then
                strQuery += " and TSPL_LOCATION_MASTER.Location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            End If
            If strCustAll = "N" Then
                strQuery += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            End If

            If strItemAll = "N" Then
                strQuery += " and final.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
            End If
            If strRouteAll = "N" Then
                strQuery += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            Dim strFinal As String = "select '" & LocName & "' as StrLoc,'" & dtpstart.Value & "' as Fdate,'" & dtpend.Value & "' as Tdate,Route_No,Route_Desc , " & _
            "item_code,convert(decimal(18,2),MRP_Amt) as MRP, " & _
            "convert(decimal(18,2),sum(SaleQty)) as NetsaleQTy, " & _
            "convert(decimal(18,2),SUM(basicamt)) as basicamt, case when sum(SaleQty) > 0 then convert(decimal(18,2), " & _
            "SUM(basicamt)/sum(SaleQty)) else 0 end as basicrate, convert(decimal(18,2),sum(FOCAMt)) as TradeTotal, " & _
            "convert(decimal(18,2),case when sum(SaleQty) > 0 then (sum(FOCAMt)/sum(SaleQty)) else sum(SaleQty) end) as TradePerCase " & _
            "from ( " & strQuery & " ) GrandFinal group by Route_No,Route_Desc,item_code,MRP_Amt," & strOrderColumn & " " & strOrderBy & " "


            dt = clsDBFuncationality.GetDataTable(strFinal)
            GV1.DataSource = Nothing
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadMessageBox.Show("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt
                SetGridFormationOFGV1()
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptKeyValue", "Key Sale Report")
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpstart.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpend.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkRouteSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgRoute.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer Type : " + strtemp)
            End If

            If chklocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location Segment : " + strtemp)
            End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Key Sale Report", GV1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Key Sale Report", GV1, arrHeader, "Key Sale Report ", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
        GV1.DataSource = Nothing
        GV1.Columns.Clear()
        GV1.Rows.Clear()

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        print()
    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If GV1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        If GV1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Sub SetGridFormationOFGV1()
        'Dim strItemCode, head2 As String

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        GV1.Columns("Route_No").IsVisible = True
        GV1.Columns("Route_No").Width = 100
        GV1.Columns("Route_No").HeaderText = "Route No"

        GV1.Columns("Route_Desc").IsVisible = True
        GV1.Columns("Route_Desc").Width = 100
        GV1.Columns("Route_Desc").HeaderText = "Route Desc"

        GV1.Columns("Item_code").IsVisible = True
        GV1.Columns("Item_code").Width = 100
        GV1.Columns("Item_code").HeaderText = "Item code"

        GV1.Columns("MRP").IsVisible = True
        GV1.Columns("MRP").Width = 70
        GV1.Columns("MRP").HeaderText = "MRP"
        'GV1.Columns("Location").BestFit()

        GV1.Columns("basicamt").IsVisible = True
        GV1.Columns("basicamt").Width = 120
        GV1.Columns("basicamt").HeaderText = "Sales(basic)  Rs TOTAL"
        ''GV1.Columns("Customer Group").BestFit()

        GV1.Columns("basicrate").IsVisible = True
        GV1.Columns("basicrate").Width = 120
        GV1.Columns("basicrate").HeaderText = "Sales(basic)  Rs P/C"
        ''GV1.Columns("Customer Type").BestFit()

        GV1.Columns("NetsaleQTy").IsVisible = True
        GV1.Columns("NetsaleQTy").Width = 120
        GV1.Columns("NetsaleQTy").HeaderText = "Qty"
        ''GV1.Columns("Customer").BestFit()

        GV1.Columns("TradeTotal").IsVisible = True
        GV1.Columns("TradeTotal").Width = 120
        GV1.Columns("TradeTotal").HeaderText = "Trade  Scheme TOTAL"
        ''GV1.Columns("Invoice").BestFit()

        GV1.Columns("TradePerCase").IsVisible = True
        GV1.Columns("TradePerCase").Width = 80
        GV1.Columns("TradePerCase").HeaderText = "Trade  Scheme P/C"
        ''GV1.Columns("Invoice Date").BestFit()

        GV1.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Route format ""{0}: {1}"" Group By Route_No"))
        GV1.GroupDescriptors.Add(New GridGroupByExpression("Route_Desc as Route format ""{0}: {1}"" Group By Route_Desc"))
        GV1.ShowGroupPanel = False
        GV1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0


        Dim item8 As New GridViewSummaryItem("basicamt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("NetsaleQTy", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("TradeTotal", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)

        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub


    Private Sub chkItemAll1_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll1.ToggleStateChanged
        cbgItem1.Enabled = Not chkItemAll1.IsChecked
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub chklocAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub
End Class
