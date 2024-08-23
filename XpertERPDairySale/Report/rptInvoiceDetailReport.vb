Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' Ticket No-ALF/09/04/19-000096 ,Client- Alpha,Created by- Sanjay
Public Class rptInvoiceDetailReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtItem.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtMultiCustomer.arrValueMember = Nothing

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            GetReportGridID()
            If rbtnBatchWise.IsChecked = True Then
                PageSetupReport_ID = MyBase.Form_ID + "B"
            ElseIf rbtnItemWise.IsChecked = True Then
                PageSetupReport_ID = MyBase.Form_ID + "I"
            End If

            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            If rbtnBatchWise.IsChecked = True Then
                qry = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code  as [Invoice No],TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [invoice Date] ,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code as [DO No],TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] ,case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Customer_Name   else TSPL_SHIP_TO_LOCATION.Ship_To_Desc end  as Consignee " &
                        ",ISNULL(TSPL_SD_SHIPMENT_HEAD.Cust_PO_No,'') as [Customer PO]  " &
                        ", TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code] ,  TSPL_ITEM_MASTER.Item_Desc as [Item Desc]  " &
                        ", isnull(TSPL_BATCH_ITEM.Qty,TSPL_SD_SHIPMENT_DETAIL.Qty) as Qty  " &
                        ", TSPL_SD_SHIPMENT_DETAIL.Unit_code as UOM,isnull(TSPL_BATCH_ITEM.Batch_No,'')  as [Batch No]  ,isnull(TSPL_BATCH_ITEM.Manual_BatchNo,'') as [Manual Batch No]  , isnull(TSPL_SD_SALE_INVOICE_HEAD.CancelFlag,'N')  as [Is Cancelled]  " &
                        " from TSPL_SD_SHIPMENT_DETAIL " &
                        "Left Outer Join TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  " &
                        "left join TSPL_BATCH_ITEM on TSPL_SD_SHIPMENT_DETAIL.Document_Code   =TSPL_BATCH_ITEM.Document_Code AND TSPL_BATCH_ITEM.Parent_Line_No=TSPL_SD_SHIPMENT_DETAIL.Line_No  " &
                        "AND TSPL_BATCH_ITEM.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code AND TSPL_BATCH_ITEM.UOM =TSPL_SD_SHIPMENT_DETAIL.Unit_code " &
                        "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code   " &
                        "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_SD_SHIPMENT_HEAD.Bill_To_Location  " &
                        "left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code   " &
                        "left outer join  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SHIPMENT_HEAD .Ship_To_Location " &
                        "left outer join TSPL_SD_SALE_INVOICE_HEAD  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No " &
                       " where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  "


                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SHIPMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
                End If

                qry += " AND  TSPL_SD_SALE_INVOICE_HEAD.Screen_Type ='DS' order by TSPL_SD_SHIPMENT_HEAD.Document_Date asc "
            ElseIf rbtnItemWise.IsChecked = True Then
                qry = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code  as [Invoice No],TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [invoice Date] ,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code as [DO No]" &
                    ",TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] " &
                    ",case when isnull(TSPL_SD_SHIPMENT_HEAD .Ship_To_Location,'')='' then TSPL_CUSTOMER_MASTER.Customer_Name  " &
                    " else TSPL_SHIP_TO_LOCATION.Ship_To_Desc end  as Consignee " &
                    ",ISNULL(TSPL_SD_SHIPMENT_HEAD.Cust_PO_No,'') as [Customer PO] " &
                    ", TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code] ,  TSPL_ITEM_MASTER.Item_Desc as [Item Desc] " &
                    ", TSPL_SD_SHIPMENT_DETAIL.Qty , TSPL_SD_SHIPMENT_DETAIL.Unit_code AS UOM,TSPL_SD_SHIPMENT_DETAIL.Amount [Item Amt],TSPL_SD_SHIPMENT_DETAIL.Total_Tax_Amt [Item Tax Amt],TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt [Item Net Amt] " &
                    ", isnull(TSPL_SD_SALE_INVOICE_HEAD.CancelFlag,'N')  as [Is Cancelled] " &
                    " from TSPL_SD_SHIPMENT_DETAIL " &
                    "LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD .Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  " &
                    "LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code  " &
                    "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code = TSPL_SD_SHIPMENT_HEAD.Bill_To_Location   " &
                    "left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code " &
                    "left outer join  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SHIPMENT_HEAD .Ship_To_Location " &
                    "left outer join TSPL_SD_SALE_INVOICE_HEAD  on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No " &
                   " where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  "

                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SHIPMENT_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If

                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
                End If

                qry += " AND  TSPL_SD_SALE_INVOICE_HEAD.Screen_Type ='DS' order by TSPL_SD_SHIPMENT_HEAD.Document_Date asc "
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Gv1.Columns("Qty").FormatString = "{0:n2}"
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim itemQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemQty)
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rbtnItemWise.IsChecked = True Then
            VarID += "_IW"
        Else
            rbtnBatchWise.IsChecked = True
            VarID += "_BW"
        End If
        Gv1.VarID = VarID
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    'Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

    '    txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    'End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptInvoiceDetailReport & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            'If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Invoice Detail Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Invoice Detail Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

   
    Private Sub rbtnItemWise_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnItemWise.CheckStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
    End Sub

    Private Sub rbtnBatchWise_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnBatchWise.CheckStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
    End Sub

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSelect", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub
End Class
