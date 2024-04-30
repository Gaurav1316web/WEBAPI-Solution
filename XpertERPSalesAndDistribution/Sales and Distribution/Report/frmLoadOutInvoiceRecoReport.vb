'---------------------------------------------------------------
'--27/07/2012--Created By--[Pankaj Kumar]----By_Ranjana Mam
'---------------------------------------------------------------
'by vipin for pdf.
Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class FrmLoadOutInvoiceRecoReport
    Inherits FrmMainTranScreen
    Dim ArrCompleteTranfer As List(Of String)
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLoadOutInvoiceRecoReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmLoadOutInvoiceRecoReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadTransfer()
        Reset()
    End Sub

    Public Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate1.Value = clsCommon.GETSERVERDATE
        chkTransferAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub
    Sub LoadTransfer()
        Dim qry As String = "Select Transfer_No as [Transfer No], Transfer_Date as [Transfer Date]  from TSPL_TRANSFER_HEAD LEFT OUTER JOIN TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code Where TSPL_TRANSFER_HEAD.Transfer_Type='LO' AND TSPL_LOCATION_MASTER.Location_Type='Logical'"
        dgvTransfer.DataSource = clsDBFuncationality.GetDataTable(qry)
        dgvTransfer.ValueMember = "Transfer No"
        dgvTransfer.DisplayMember = "Transfer Date"
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefreshData()
    End Sub

    Public Sub RefreshData()
        gv1.EnableFiltering = True
        Dim Where As String = ""
        Dim whre As String = ""
        Dim Where1 As String = ""
        Dim DateRange1 As String = "AND COnvert(Date,TSPL_SHIPMENT_MASTER.Shipment_Date, 103)>=Convert(Date, '" + txtFromDate.Value + "', 103) AND COnvert(Date,TSPL_SHIPMENT_MASTER.Shipment_Date, 103)<=Convert(Date, '" + txtToDate1.Value + "', 103)"
        Dim DateRange As String = "AND  COnvert(Date,TSPL_TRANSFER_HEAD.Transfer_Date , 103)>=Convert(Date, '" + txtFromDate.Value + "', 103) AND COnvert(Date,TSPL_TRANSFER_HEAD.Transfer_Date , 103)<=Convert(Date, '" + txtToDate1.Value + "', 103)"

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If chkTransferSelect.IsChecked AndAlso dgvTransfer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Transfer No", Me.Text)
            Exit Sub
        End If
        If chkTransferSelect.IsChecked AndAlso dgvTransfer.CheckedValue.Count > 0 Then
            whre = "And TSPL_TRANSFER_HEAD.Transfer_No IN (" + clsCommon.GetMulcallString(dgvTransfer.CheckedValue) + ")"
            Where = "AND TSPL_TRANSFER_HEAD.Load_Out_No IN (" + clsCommon.GetMulcallString(dgvTransfer.CheckedValue) + ")"
            Where1 = "AND TSPL_SHIPMENT_MASTER.Transfer_No IN (" + clsCommon.GetMulcallString(dgvTransfer.CheckedValue) + ")"
        End If

        Dim qry As String = "Select *, Convert(Integer,(BalanceQty /1)) as [Balance In FC], Ceiling((BalanceQty %1)*(Select Conversion_Factor from TSPL_ITEM_UOM_DETAIL Where Item_Code=Final.Item_Code  AND UOM_Code='FB')) as [Balance In FB] from (select Transfer_No, MAX(Transfer_Date ) as TransferDate , MAX(Route_No) as Route_No, Item_Code "
        qry += " ,CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =1 then 1 else 0 end  ),2)) as LoadoutQty "
        qry += ",CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =3 then 1 else 0 end  ),2)) as LoadinQty "
        qry += ",CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =1 then 1 else case when RI in (3) then -1 else 0 end end),2)) as ProposedSale "
        qry += ",CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =2 then 1 else 0 end  ),2)) as GrossSaleQty  "
        qry += ",CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =2 and FOCItem=1 then 1 else 0 end  ),2)) as DiscountQty  "
        qry += ",CONVERT(decimal(18,2), ROUND(sum( Item_Qty * case when RI =2 then 1 else 0 end)-sum(Item_Qty * case when RI =2 and FOCItem=1 then 1 else 0 end),2)) as NetSale "
        qry += ",CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =1 then 1 else case when RI in (2,3) then -1 else 0 end end),2)) as BalanceQty "
        qry += "from ( "
        qry += "select xxx.Transfer_No,xxx.Transfer_Date,xxx.Route_No, xxx.Route_Desc,xxx.Item_Code,xxx.Price_Date,xxx.Item_Qty as OrgItem_Qty,xxx.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Item_Qty,'FC' as Uom,xxx.RI,xxx.FOCItem,xxx.Chk from ( "
        qry += "select TSPL_TRANSFER_HEAD.Transfer_No,CONVERT(varchar(11), TSPL_TRANSFER_HEAD.Transfer_Date,103) as Transfer_Date,TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Route_Desc,Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,Item_Qty,TSPL_TRANSFER_DETAIL.Uom,1 as RI ,0  as FOCItem,1 as Chk "
        qry += "from TSPL_TRANSFER_DETAIL "
        qry += "left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD .Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No"
        qry += " left Outer Join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location=TSPL_LOCATION_MASTER.Location_Code  "
        qry += "where TSPL_TRANSFER_DETAIL.Uom<> 'SH' AND TSPL_TRANSFER_HEAD.Transfer_Type='LO' AND TSPL_LOCATION_MASTER.Location_Type='Logical' "
        qry += " " + DateRange + " " + whre + " "
        qry += "union all  "
        qry += "select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,'' as Transfer_Date,'' as Route_No,'' as Route_Desc,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Price_Date,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as Item_Qty,TSPL_SALE_INVOICE_DETAIL.Unit_code ,2 as RI, "
        qry += "(CASE WHEN TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' or TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 THEN 1 ELSE 0 END) as FOCItem ,0 as Chk "
        qry += "from TSPL_SALE_INVOICE_DETAIL "
        qry += "left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No "
        qry += "left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No WHERE 1=1 "
        qry += " " + DateRange1 + " " + Where1 + " "
        qry += "union all "
        qry += "select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,'' as Transfer_Date,'' as Route_No,'' as Route_Desc,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,(ISNULL( TSPL_TRANSFER_DETAIL.Burst,0)+isnull(TSPL_TRANSFER_DETAIL.Leak,0)+isnull(TSPL_TRANSFER_DETAIL.Shortage,0)+TSPL_TRANSFER_DETAIL.LoadIn_Qty) as Item_Qty,TSPL_TRANSFER_DETAIL.Uom  ,3 as RI,0  as FOCItem,0 as Chk "
        qry += "from TSPL_TRANSFER_DETAIL  "
        qry += "left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No "
        qry += "where TSPL_TRANSFER_DETAIL.Uom<> 'SH' and Transfer_Type='LI' "
        qry += " " + DateRange + " " + Where + "  "
        qry += ") xxx  "
        qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.Uom "
        qry += ")xxxx "
        qry += "group by Transfer_No,Item_Code "
        qry += "having SUM(chk) > 0 "
        qry += ") Final"

        Try
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt1
            FormatGV1()
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGV1()
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Transfer_No").IsVisible = True
        gv1.Columns("Transfer_No").Width = 120
        gv1.Columns("Transfer_No").HeaderText = "Transfer No"

        gv1.Columns("TransferDate").IsVisible = True
        gv1.Columns("TransferDate").Width = 80
        gv1.Columns("TransferDate").FormatString = "{0:d}"
        gv1.Columns("TransferDate").HeaderText = "Transfer Date"

        gv1.Columns("Route_No").IsVisible = True
        gv1.Columns("Route_No").Width = 100
        gv1.Columns("Route_No").HeaderText = "Route No"

        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").Width = 100
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("LoadoutQty").IsVisible = True
        gv1.Columns("LoadoutQty").Width = 100
        gv1.Columns("LoadoutQty").HeaderText = "Loadout"

        gv1.Columns("LoadinQty").IsVisible = True
        gv1.Columns("LoadinQty").Width = 100
        gv1.Columns("LoadinQty").HeaderText = "Loadin"


        gv1.Columns("ProposedSale").IsVisible = True
        gv1.Columns("ProposedSale").Width = 100
        gv1.Columns("ProposedSale").HeaderText = "Provisional Sale"


        gv1.Columns("GrossSaleQty").IsVisible = True
        gv1.Columns("GrossSaleQty").Width = 100
        gv1.Columns("GrossSaleQty").HeaderText = "Gross Sale"

        gv1.Columns("DiscountQty").IsVisible = True
        gv1.Columns("DiscountQty").Width = 100
        gv1.Columns("DiscountQty").HeaderText = "Discount"

        gv1.Columns("NetSale").IsVisible = True
        gv1.Columns("NetSale").Width = 100
        gv1.Columns("NetSale").HeaderText = "Net Sale"

        gv1.Columns("BalanceQty").IsVisible = True
        gv1.Columns("BalanceQty").Width = 100
        gv1.Columns("BalanceQty").HeaderText = "Balance"

        gv1.Columns("Balance In FC").IsVisible = True
        gv1.Columns("Balance In FC").Width = 100

        gv1.Columns("Balance In FB").IsVisible = True
        gv1.Columns("Balance In FB").Width = 100

        'gv1.GroupDescriptors.Add(New GridGroupByExpression("Transfer_No as Transfer_No format ""{0}: {1}"" Group By Transfer_No"))
        'gv1.MasterTemplate.ExpandAllGroups()
        gv1.ShowGroupPanel = False
        'gv1.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("LoadoutQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("LoadinQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("ProposedSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("GrossSaleQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("DiscountQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("NetSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("BalanceQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Balance In FC", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Balance In FB", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        ' Printdata()
    End Sub

    Public Sub Printdata(ByVal exporter As EnumExportTo)
        RefreshData()
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        If chkTransferSelect.IsChecked Then
            strTemp = ""
            For Each Str2 As String In dgvTransfer.CheckedDisplayMember
                If clsCommon.myLen(strTemp) > 0 Then
                    strTemp += ", "
                End If
                strTemp += Str2
            Next
            arrHeader.Add("Transfer Segment : " + strTemp)
        End If
        arrHeader.Add("Date : " + txtFromDate.Value + " To " + txtToDate1.Value)
        If chkTransferSelect.IsChecked Then
            arrHeader.Add("Transfer:" + clsCommon.GetMulcallString(dgvTransfer.CheckedValue))
        End If
        'clsCommon.MyExportToExcel("LoadOut Invoice  Reconcilation Report", gv1, arrHeader, Me.Text)
        If gv1.Rows.Count <= 0 Then
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("LoadOut Invoice  Reconcilation Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("LoadOut Invoice  Reconcilation Report", gv1, arrHeader, "LoadOut Invoice  Reconcilation Report", True)
            End If
        Else
            clsCommon.MyMessageBoxShow("No data foung to export.")
        End If

    End Sub

    Private Sub chkTransferAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTransferAll.ToggleStateChanged
        dgvTransfer.Enabled = False
    End Sub

    Private Sub chkTransferSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTransferSelect.ToggleStateChanged
        dgvTransfer.Enabled = True
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Printdata(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Printdata(EnumExportTo.PDF)
    End Sub
End Class
