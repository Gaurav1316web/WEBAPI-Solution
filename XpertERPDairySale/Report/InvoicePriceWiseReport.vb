Imports common
Imports System.IO
'' Work Done agaist ticket no. BHA/10/10/18-000616,BHA/15/10/18-000623
Public Class InvoicePriceWiseReport
    Inherits FrmMainTranScreen
    Dim isSchemeItem As Boolean = False
    Dim strQry As String = ""
    Dim dt As DataTable
 

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtUserName__My_Click(sender As Object, e As EventArgs) Handles txtUserName._My_Click
        strQry = "select User_Code  as [Code],User_Name as [Name] from TSPL_User_MASTER"
        txtUserName.arrValueMember = clsCommon.ShowMultipleSelectForm("UserMulSel", strQry, "Code", "Name", txtUserName.arrValueMember, txtUserName.arrDispalyMember)
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = "select Cust_Code  as [Code],Customer_Name as [Name] from TSPL_Customer_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)

        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
            Dim strFromDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            ''richa agarwal TEC/19/06/19-000553 show item code and scheme item column for more clarification becuase price code shows only blank for those scheme item only
            Dim query As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Shipment No],convert(varchar,TSPL_SD_SHIPMENT_HEAD.document_date,103) as [Shipment Date],TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Sale Invoice No],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Sale Invoice Date]" & Environment.NewLine & _
            " ,TSPL_Customer_Invoice_Head.Document_No as [AR Invoice No],convert(varchar,TSPL_Customer_Invoice_Head.document_date,103) as [AR Invoice Date], " & Environment.NewLine & _
            " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Name]  ,CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' THEN 'Yes' ELSE 'No' END AS [Scheme Item], " & Environment.NewLine & _
            " TSPL_SD_SALE_INVOICE_Detail.Price_code as [Price Code],TSPL_SD_SALE_INVOICE_Detail.Price_date as [Price Date],TSPL_ITEM_PRICE_MASTER.Price_Code_Desc as [Price Desc],TSPL_ITEM_PRICE_MASTER.Item_Selling_Price as [Selling Price],TSPL_ITEM_PRICE_MASTER.Item_Basic_Price as [Basic Price],TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location],tspl_location_master.Location_Desc as [Location Name],TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_INVOICE_HEAD.Created_By as [Created By],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Created_Date,103) as [Created Date],TSPL_USER_MASTER.User_Name as [Modify By],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Modify_Date,103) as [Modify Date]  from TSPL_SD_SALE_INVOICE_HEAD" & Environment.NewLine & _
            " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & Environment.NewLine & _
            " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & Environment.NewLine & _
            " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code  " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" & Environment.NewLine & _
            " left outer join tspl_location_master on tspl_location_master.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location" & Environment.NewLine & _
            " left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_SD_SALE_INVOICE_HEAD.Modify_By " & Environment.NewLine & _
            " left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.price_code=TSPL_SD_SALE_INVOICE_DETAIL.price_code  and TSPL_ITEM_PRICE_MASTER.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and TSPL_ITEM_PRICE_MASTER.location_code=TSPL_SD_SALE_INVOICE_HEAD.bill_to_location" & Environment.NewLine & _
            " and TSPL_ITEM_PRICE_MASTER.uom=TSPL_SD_SALE_INVOICE_DETAIL.unit_code and TSPL_ITEM_PRICE_MASTER.start_date=TSPL_SD_SALE_INVOICE_DETAIL.price_date " & Environment.NewLine & _
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine & _
            " where 2=2" & Environment.NewLine & _
            " and CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" + strFromDate + "' AND CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" + strToDate + "' "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                query += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtUserName.arrValueMember IsNot Nothing AndAlso txtUserName.arrValueMember.Count > 0 Then
                query += " and TSPL_SD_SALE_INVOICE_HEAD.Modify_By in (" + clsCommon.GetMulcallString(txtUserName.arrValueMember) + ")"
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                query += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.BestFitColumns()

            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
            Gv1.BestFitColumns()
            Gv1.EnableGrouping = False


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

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

    Private Sub rptDairySaleRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtLocation.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtUserName.arrValueMember = Nothing
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

   
    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.InvoicePriceWiseReport & "'"))


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
  
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try

            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Invoice Price Wise Report")
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Invoice Price Wise Report", Gv1, arrHeader, "Invoice Price Wise Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Invoice Price Wise Report")
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Invoice Price Wise Report", Gv1, arrHeader, "Invoice Price Wise Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    
   
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As New clsGridLayout()
                Gv1.MasterTemplate.FilterDescriptors.Clear()
                obj = New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                Gv1.SaveLayout(obj.GridLayout)
                obj.GridColumns = Gv1.ColumnCount
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class
