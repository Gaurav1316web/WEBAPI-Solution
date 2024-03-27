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
        EnableDisableControl(False)
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
        Dim whr As String = ""
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            whr = " and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
            whr = " and TSPL_DEMAND_BOOKING_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtMultItem.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            whr = " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")" + Environment.NewLine
        End If
        qry = "  Select max(x.Customer_Code)[Distributor Code],max(x.Customer_Name)[Distributor Name],max(x.Route_No)[Route No.],max(x.Route_Desc)[Route Description],
                 x.Item_Code,max(x.Item_Desc)[Item Description],max(x.Unit_code)UOM,sum(x.DemandQty)[Demand Qty],sum(x.DispatchQty)[Dispatch Qty],
                 sum(x.BalanceQty)[Balance Qty] from (
                select TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,
                TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_SD_SHIPMENT_HEAD.Route_Desc,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
                TSPL_ITEM_MASTER.Item_Desc,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty as DemandQty,
                TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty as DispatchQty,TSPL_DEMAND_BOOKING_DETAIL.Qty-TSPL_SD_SHIPMENT_BOOKING_DETAIL.Qty  as BalanceQty,TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code 
                from TSPL_SD_SHIPMENT_HEAD
                left join TSPL_SD_SHIPMENT_BOOKING_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_BOOKING_DETAIL.DOCUMENT_CODE
                left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_SD_SHIPMENT_BOOKING_DETAIL.Booking_TR_Code=TSPL_DEMAND_BOOKING_DETAIL.TR_Code
                left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code
                left join TSPL_CUSTOMER_MASTER ON TSPL_SD_SHIPMENT_HEAD.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code
                where 2=2 and CONVERT(date,Supply_Date,103) >= '" + strFromDate + "' and CONVERT(date,Supply_Date,103) <= '" + strToDate + "' " + whr + " )X Group by Item_Code"
        'where 2=2 and CONVERT(date,Supply_Date,103) >= convert(date,'" + strFromDate + "',103) AND 
        'CONVERT(date,Supply_Date,103) <= convert(date,'" + strToDate + "',103) "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt

            SetGridFormat()

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

    Sub SetGridFormat()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True
        Next

        'gvData.Columns("Booking_TR_Code").IsVisible = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Demand Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item3 As New GridViewSummaryItem("Dispatch Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Balance Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        gvData.ShowGroupPanel = False
        gvData.MasterTemplate.AutoExpandGroups = True
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ReStoreGridLayout()
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub gvData_DoubleClick(sender As Object, e As EventArgs) Handles gvData.DoubleClick
        'Try
        '    If gvData.Rows.Count > 0 Then
        '        If rbtnDistributor.IsChecked Then
        '        Else
        '            Dim strDispatchNo As String = Nothing
        '            Dim strDoNo As String = Nothing
        '            strDoNo = gvData.CurrentRow.Cells("DO No").Value
        '            strDispatchNo = gvData.CurrentRow.Cells("Dispatch No").Value
        '            Dim columnName As String = gvData.CurrentCell.ColumnInfo.Name

        '            If clsCommon.myLen(strDoNo) > 0 AndAlso clsCommon.CompairString(columnName, "DO Qty") = CompairStringResult.Equal Then
        '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDeliveryOrderDairy, strDoNo)
        '            ElseIf clsCommon.myLen(strDispatchNo) > 0 AndAlso clsCommon.CompairString(columnName, "Dispatch Qty") = CompairStringResult.Equal Then
        '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleDispatchDairy, strDispatchNo)
        '            End If
        '        End If

        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        EnableDisableControl(True)
        txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        txtMultItem.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtBookingType.arrValueMember = Nothing
        TxtMultiCustomerCategory.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnDistributor.IsChecked = False
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox4.Enabled = val
        txtMultiCustomer.Enabled = val
        txtMultItem.Enabled = val
        txtRoute.Enabled = val

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
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master where IsDistributor='Y'"
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click ''delete layout
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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
