'' Create new Screen for ALPHA against ticket no. ALF/08/05/18-000061 
'' Work on Export too Excel against ticket no. ALF/14/06/18-000072 
'' work on Balance Qty and Short Close Qty against ticket no. ALF/26/06/18-000073 
Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared


Public Class FrmPendingBookingReport
    Inherits FrmMainTranScreen
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvData
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        SelFromDate = clsCommon.myCstr(txtToDate.Value.AddDays(-1).ToShortDateString())
        SelToDate = clsCommon.myCstr(txtToDate.Value.ToShortDateString())
        Dim strFromDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        'Sanjay ,Add Customer Category
        'Sanjay BHA/15/08/18-000429 Add Route Code and Name
        'Sanjay Ticket No-TEC/19/06/19-000552 Short Close Qty=(DELIVERY_NOTE_Qty-SHIPMENT_Qty)
        ' Ticket No : VIJ/09/12/19-000104 By Prabhakar Add customer Group and Booking Amount And Separate Scheme Query because Qty came double 
        Dim qry As String = ""
        qry = "  select max(XXXFinal.[Customer Category Code]) as [Customer Category Code],max(XXXFinal.[Customer Category Desc]) as [Customer Category Desc]"
        qry += " , max(XXXFinal.[Route Code]) as [Route Code],max(XXXFinal.[Route Name]) as [Route Name],max(XXXFinal.[Customer Code]) as [Customer Code],max(XXXFinal.[Customer Name]) as [Customer Name] ,max(XXXFinal.[Cust Group Code]) as [Cust Group Code],max(XXXFinal.[Cust Group Desc]) as [Cust Group Desc],XXXFinal.[Booking No],max(XXXFinal.[Booking Type]) as [Booking Type],XXXFinal.[Item Code],max(XXXFinal.[Item Desc]) as [Item Desc],XXXFinal.UOM, sum (XXXFinal.[Booking Qty]) as [Booking Qty], Sum( XXXFinal.[Booking Amount]) as [Booking Amount] ,max (XXXFinal.[DO No]) as [DO No],sum(XXXFinal.[DO Qty]) as [DO Qty],max(XXXFinal.[Dispatch No]) as [Dispatch No] ,sum(XXXFinal.[Dispatch Qty]) as [Dispatch Qty],sum(XXXFinal.[Short Close Qty]) as [Short Close Qty],sum (XXXFinal.[Balance Qty]) as [Balance Qty]  from ( "
        qry += " select final.*,isnull([DO Qty]-([Short Close Qty]+[Dispatch Qty]),0) as [Balance Qty] from ( "
        qry += "select max(TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code) as [Customer Category Code],max(TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC) as [Customer Category Desc]"
        qry += ", max(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No) as [Route Code],max(TSPL_ROUTE_MASTER.Route_Desc) as [Route Name] "
        qry += " ,max(TSPL_BOOKING_DETAIL.Cust_Code) as [Customer Code],max(TSPL_CUSTOMER_MASTER.Customer_Name) as [Customer Name], max(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as [Cust Group Code],max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as [Cust Group Desc],TSPL_BOOKING_MATSER.Document_No as [Booking No],max(TSPL_BOOKING_MATSER.Booking_Type) as [Booking Type],TSPL_BOOKING_DETAIL.Item_Code as [Item Code],max(TSPL_ITEM_MASTER.Item_Desc) as [Item Desc],max(TSPL_BOOKING_DETAIL.Unit_code) as UOM,sum(TSPL_BOOKING_DETAIL.Booking_Qty) as [Booking Qty],Sum (isnull(TSPL_BOOKING_DETAIL.Amount_with_Tax,0)) as [Booking Amount],max(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No) as [DO No],sum(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty) as [DO Qty],max(isnull(TSPL_SD_SHIPMENT_DETAIL.Document_Code,0)) as [Dispatch No],isnull(sum(TSPL_SD_SHIPMENT_DETAIL.Qty),0) as [Dispatch Qty] "
        qry += " ,case when max(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close)='Y' then sum( TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty)-isnull(sum(TSPL_SD_SHIPMENT_DETAIL.Qty),0) else 0 end as [Short Close Qty] "
        qry += " from TSPL_BOOKING_MATSER"
        qry += " left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No"
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code"
        qry += " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code "
        qry += " left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No=TSPL_BOOKING_DETAIL.Document_No"
        qry += " left outer join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item = 'N'"
        qry += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code=TSPL_BOOKING_DETAIL.Item_Code"
        qry += "  left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No "
        qry += " and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code "
        qry += " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code   where 2=2 "
        qry += " and  CONVERT(date,TSPL_BOOKING_MATSER.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " + Environment.NewLine & _
                    "CONVERT(date,TSPL_BOOKING_MATSER.document_Date,103) <= convert(date,'" + strToDate + "',103)  and TSPL_BOOKING_DETAIL.Scheme_Item='N' "
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_DETAIL.Cust_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_DETAIL.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_MATSER.Booking_Type in(" + clsCommon.GetMulcallString(txtBookingType.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
        End If
        qry += " group by TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Item_Code"
        qry += " )Final "
        qry += " Union All "
        qry += " select final.*,isnull([DO Qty]-([Short Close Qty]+[Dispatch Qty]),0) as [Balance Qty] from ( "
        qry += "select max(TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code) as [Customer Category Code],max(TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC) as [Customer Category Desc]"
        qry += ", max(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No) as [Route Code],max(TSPL_ROUTE_MASTER.Route_Desc) as [Route Name] "
        qry += " ,max(TSPL_BOOKING_DETAIL.Cust_Code) as [Customer Code],max(TSPL_CUSTOMER_MASTER.Customer_Name) as [Customer Name], max(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as [Cust Group Code],max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as [Cust Group Desc],TSPL_BOOKING_MATSER.Document_No as [Booking No],max(TSPL_BOOKING_MATSER.Booking_Type) as [Booking Type],TSPL_BOOKING_DETAIL.Item_Code as [Item Code],max(TSPL_ITEM_MASTER.Item_Desc) as [Item Desc],max(TSPL_BOOKING_DETAIL.Unit_code) as UOM,sum(TSPL_BOOKING_DETAIL.Booking_Qty) as [Booking Qty],Sum (isnull(TSPL_BOOKING_DETAIL.Amount_with_Tax,0)) as [Booking Amount],max(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No) as [DO No],sum(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty) as [DO Qty],max(isnull(TSPL_SD_SHIPMENT_DETAIL.Document_Code,0)) as [Dispatch No],isnull(sum(TSPL_SD_SHIPMENT_DETAIL.Qty),0) as [Dispatch Qty] "
        qry += " ,case when max(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close)='Y' then sum( TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty)-isnull(sum(TSPL_SD_SHIPMENT_DETAIL.Qty),0) else 0 end as [Short Close Qty] "
        qry += " from TSPL_BOOKING_MATSER"
        qry += " left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No"
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code"
        qry += " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code "
        qry += " left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No=TSPL_BOOKING_DETAIL.Document_No"
        qry += " left outer join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item = 'Y'"
        qry += " and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code=TSPL_BOOKING_DETAIL.Item_Code"
        qry += "  left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No "
        qry += " and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code "
        qry += " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code   where 2=2 "
        qry += " and  CONVERT(date,TSPL_BOOKING_MATSER.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " + Environment.NewLine & _
                    "CONVERT(date,TSPL_BOOKING_MATSER.document_Date,103) <= convert(date,'" + strToDate + "',103)  and TSPL_BOOKING_DETAIL.Scheme_Item='Y' "
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_DETAIL.Cust_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_DETAIL.Item_Code in(" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtBookingType.arrValueMember IsNot Nothing AndAlso txtBookingType.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_MATSER.Booking_Type in(" + clsCommon.GetMulcallString(txtBookingType.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No in(" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
        End If
        qry += " group by TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Item_Code"
        qry += " )Final "
        qry += " ) XXXFinal Group by XXXFinal.[Booking No],XXXFinal.[Item Code],XXXFinal.UOM Order By XXXFinal.[Booking No] "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt

            gvData.Columns("DO No").IsVisible = False
            gvData.Columns("Dispatch No").IsVisible = False

            SetGridFormationOFGV1()
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = False
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            gvData.BestFitColumns()
        End If

    End Sub
    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True
        Next
  
      
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Booking Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("DO Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Dispatch Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Balance Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Short Close Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Booking Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        gvData.ShowGroupPanel = False
        gvData.MasterTemplate.AutoExpandGroups = True
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ReStoreGridLayout()
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

            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Pending Booking Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Pending Booking Report", gvData, arrHeader, "Pending Booking Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub gvData_DoubleClick(sender As Object, e As EventArgs) Handles gvData.DoubleClick
        Try
            If gvData.Rows.Count > 0 Then
                Dim strDispatchNo As String = Nothing
                Dim strDoNo As String = Nothing
                strDoNo = gvData.CurrentRow.Cells("DO No").Value
                strDispatchNo = gvData.CurrentRow.Cells("Dispatch No").Value
                Dim columnName As String = gvData.CurrentCell.ColumnInfo.Name

                If clsCommon.myLen(strDoNo) > 0 AndAlso clsCommon.CompairString(columnName, "DO Qty") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDeliveryOrderDairy, strDoNo)
                ElseIf clsCommon.myLen(strDispatchNo) > 0 AndAlso clsCommon.CompairString(columnName, "Dispatch Qty") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleDispatchDairy, strDispatchNo)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        txtMultItem.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtBookingType.arrValueMember = Nothing
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
    End Sub
    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub
    Private Sub txtMultItem__My_Click(sender As Object, e As EventArgs) Handles txtMultItem._My_Click
        Dim qry As String = " select Item_Code as [Code], Item_Desc as [Name] from TSPL_ITEM_MASTER "
        txtMultItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtMultItem.arrValueMember, txtMultItem.arrDispalyMember)
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

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
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
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click ''delete layout
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

    Private Sub txtBookingType__My_Click(sender As Object, e As EventArgs) Handles txtBookingType._My_Click
        Dim strQry As String = " Select final.Code From (Select 'CD' Code Union All Select 'CR' Code Union All Select 'SO' Code Union All Select 'Cash' Code Union All Select 'Festive Order' Code Union All Select 'Distributor' Code ) final  "
        txtBookingType.arrValueMember = clsCommon.ShowMultipleSelectForm("BookingType@PendingBookingReport", strQry, "Code", "Code", txtBookingType.arrValueMember, txtBookingType.arrDispalyMember)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCaMulSel", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
End Class
