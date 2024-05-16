'' Create new Screen for Amritha against ticket no. ERO/28/05/18-000328 
'' work done agaist ticket no. ERO/04/06/18-000334
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

Public Class FrmCustomerZoneReport
    Inherits FrmMainTranScreen
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Dim ReportID As String = ""
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
        Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))

        Dim qry As String = "select * from (select 'Dairy Sales' as DocType,TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code as [Customer Category Code],TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC as [Customer Category Desc],TSPL_ZONE_MASTER.Description as Zone,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_BOOKING_MATSER.location_code as Location,TSPL_BOOKING_MATSER.Document_No as [Booking No],convert(varchar,TSPL_BOOKING_MATSER.Document_Date,103) as [Booking Date] "
        qry += " ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],convert(varchar,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_date,103) as [DO Date]"
        qry += " ,TSPL_SD_SHIPMENT_HEAD.Document_Code as [Dispatch No],convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as [Dispatch Date]"
        qry += " ,TSPL_SD_SALE_INVOICE_head.DOCUMENT_CODE as [Sale Invoice No]"
        qry += " ,TSPL_Customer_Invoice_Head.Document_No as [Invoice No],convert(varchar,TSPL_Customer_Invoice_Head.document_Date,103) as [Invoice Date]"
        qry += " ,TSPL_RECEIPT_DETAIL.Receipt_No as [Receipt No],convert(varchar,TSPL_RECEIPT_HEADER.receipt_Date,103) as [Receipt Date],TSPL_RECEIPT_DETAIL.Applied_Amount as [Receipt Amount]"
        qry += " from TSPL_BOOKING_MATSER"
        qry += " left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No=TSPL_BOOKING_MATSER.Document_No"
        qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No"
        qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_head.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code"

        qry += " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_head.Document_Code  "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code"
        qry += " left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code "
        qry += " left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No "
        qry += " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code "
        qry += " where 2=2 "
        qry += " and  CONVERT(date,TSPL_BOOKING_MATSER.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " + Environment.NewLine & _
                    "CONVERT(date,TSPL_BOOKING_MATSER.document_Date,103) <= convert(date,'" + strToDate + "',103)  and TSPL_BOOKING_MATSER.Posted=1 "
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        If chkCustCategoryMappInUserMaster = True Then
            qry += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
        End If
        If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
        End If

        If txtMultLoc.arrValueMember IsNot Nothing AndAlso txtMultLoc.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_MATSER.location_code in(" + clsCommon.GetMulcallString(txtMultLoc.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtmultiZone.arrValueMember IsNot Nothing AndAlso txtmultiZone.arrValueMember.Count > 0 Then
            qry += " and TSPL_ZONE_MASTER.Zone_Code in(" + clsCommon.GetMulcallString(txtmultiZone.arrValueMember) + ")" + Environment.NewLine
        End If
        If clsCommon.myCBool(RbtnTaxable.Checked) = True Then
            qry += " and TSPL_BOOKING_MATSER.Is_Taxable=1 "
        ElseIf clsCommon.myCBool(RbtnNonTaxable.Checked) = True Then
            qry += " and TSPL_BOOKING_MATSER.Is_Taxable=0 "
        End If
        qry += " union all"
        qry += " select 'Product Sale' as DocType,TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code as [Customer Category Code],TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC as [Customer Category Desc],TSPL_ZONE_MASTER.Description as Zone,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_BOOKING_MASTER_PRODUCTSALE.Bill_To_Location as [Location],TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code as [Booking No],convert(varchar,TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Date,103) as [Booking Date] "
        qry += " ,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code as [DO No],convert(varchar,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.document_date,103) as [DO Date]"
        qry += " ,TSPL_SD_SHIPMENT_HEAD.Document_Code as [Dispatch No],convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as [Dispatch Date]"
        qry += " ,TSPL_SD_SALE_INVOICE_head.DOCUMENT_CODE as [Sale Invoice No]"
        qry += " ,TSPL_Customer_Invoice_Head.Document_No as [Invoice No],convert(varchar,TSPL_Customer_Invoice_Head.document_Date,103) as [Invoice Date]"
        qry += " ,TSPL_RECEIPT_DETAIL.Receipt_No as [Receipt No],convert(varchar,TSPL_RECEIPT_HEADER.receipt_Date,103) as [Receipt Date],TSPL_RECEIPT_DETAIL.Applied_Amount as [Receipt Amount]"
        qry += " from TSPL_BOOKING_MASTER_PRODUCTSALE"
        qry += " left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Against_Booking_No=TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code"
        qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code"
        qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_head.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code"

        qry += " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_head.Document_Code"
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_MASTER_PRODUCTSALE.Customer_Code "
        qry += " left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code "
        qry += " left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No"
        qry += " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code "
        qry += " where 2=2 "
        qry += " and  CONVERT(date,TSPL_BOOKING_MASTER_PRODUCTSALE.document_Date,103) >= convert(date,'" + strFromDate + "',103) AND " + Environment.NewLine & _
                    "CONVERT(date,TSPL_BOOKING_MASTER_PRODUCTSALE.document_Date,103) <= convert(date,'" + strToDate + "',103) and TSPL_BOOKING_MASTER_PRODUCTSALE.Status=1 "
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        If chkCustCategoryMappInUserMaster = True Then
            qry += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
        End If
        If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
            qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtMultLoc.arrValueMember IsNot Nothing AndAlso txtMultLoc.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_MASTER_PRODUCTSALE.Bill_To_Location in(" + clsCommon.GetMulcallString(txtMultLoc.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtmultiZone.arrValueMember IsNot Nothing AndAlso txtmultiZone.arrValueMember.Count > 0 Then
            qry += " and TSPL_ZONE_MASTER.Zone_Code in(" + clsCommon.GetMulcallString(txtmultiZone.arrValueMember) + ")" + Environment.NewLine
        End If
        If clsCommon.myCBool(RbtnTaxable.Checked) = True Then
            qry += " TSPL_BOOKING_MASTER_PRODUCTSALE.Is_Taxable=1 "
        ElseIf clsCommon.myCBool(RbtnNonTaxable.Checked) = True Then
            qry += " and TSPL_BOOKING_MASTER_PRODUCTSALE.Is_Taxable=0 "
        End If
        qry += " ) xx"
        If clsCommon.myCBool(rbtnDairy.Checked) = True Then
            qry += " where xx.DocType='Dairy Sales' "
        ElseIf clsCommon.myCBool(RbtnProduct.Checked) = True Then
            qry += " where xx.DocType='Product Sale' "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt

          
            gvData.Columns("Sale Invoice No").IsVisible = False

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
        'Dim item1 As New GridViewSummaryItem("Booking Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("DO Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("Dispatch Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        gvData.ShowGroupPanel = False
        gvData.MasterTemplate.AutoExpandGroups = True
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ReStoreGridLayout()
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        txtMultLoc.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtmultiZone.arrValueMember = Nothing
        rBtnAll.Checked = True
        TxtMultiCustomerCategory.arrValueMember = Nothing
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
        ReportID = MyBase.Form_ID
    End Sub
    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub
    Private Sub txtMultItem__My_Click(sender As Object, e As EventArgs) Handles txtMultLoc._My_Click
        Dim qry As String = " select location_Code as [Code], Location_desc as [Name] from TSPL_Location_MASTER "
        txtMultLoc.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtMultLoc.arrValueMember, txtMultLoc.arrDispalyMember)
    End Sub
    Private Sub txtmultiZone__My_Click(sender As Object, e As EventArgs) Handles txtmultiZone._My_Click
        Dim qry As String = " select Zone_Code as [Code], Description as [Name] from TSPL_Zone_MASTER "
        txtmultiZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneulSel", qry, "Code", "Name", txtmultiZone.arrValueMember, txtmultiZone.arrDispalyMember)
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
    Private Sub rmexcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(Exporter.Excel)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtMultLoc.arrValueMember IsNot Nothing AndAlso txtMultLoc.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtMultLoc.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If

            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                Dim strCustomerCat As String = clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember)
                arrHeader.Add((" Customer Category : " + strCustomerCat + " "))
            Else
                arrHeader.Add((" Customer Category : All"))
            End If

            transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Customer Zone Wise Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Customer Zone Wise Report", gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gvData.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvData.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvData.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCateMulSel", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub
End Class
