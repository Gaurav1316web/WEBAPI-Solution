Imports common
Public Class RptSalesCancelledRegister
    Inherits FrmMainTranScreen

    Private Sub RptSalesCancelledRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        txtUOM.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtUOM.Value) > 0 Then
            lblBilltoLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtUOM.Value + "' "))
        End If

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        'btnSplitExport.Visible = MyBase.isExport
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim dt As New DataTable()
            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select location first")
                Exit Sub
            End If
            Dim qry As String = "
                          
                           SELECT   TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Qty as InvoiceQty, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,Item_Cost as rate,Amount,Disc_Per,Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt,  TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as TotalAmt 
                           FROM TSPL_CUSTOMER_MASTER 
                           left outer join tspl_customer_master as Parent_Master on Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No 
                           RIGHT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data TSPL_SD_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
                           RIGHT OUTER JOIN TSPL_ITEM_MASTER 
                           RIGHT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code 
                           left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(Date,'" + clsCommon.GetPrintDate(fromDate.Value) + "',103) And Convert(Date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=  convert(Date,'" + clsCommon.GetPrintDate(ToDate.Value) + "',103)     and Location_Code='" + txtUOM.Value + "' 

                           union all

                         SELECT   TSPL_SD_SALE_INVOICE_HEAD.shipment_No as document_code, TSPL_SD_SALE_INVOICE_HEAD.shipment_Date as document_date,TSPL_SD_SALE_INVOICE_HEAD.cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Loc_Code ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALE_INVOICE_DETAIL.shipped_Qty as InvoiceQty, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,price as rate,NetPriceAmt,0 AS Disc_Per,Discount_Amt as Disc_Amt, TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,TSPL_SD_SALE_INVOICE_HEAD.Total_Tax_Amt,  TSPL_SD_SALE_INVOICE_DETAIL.TotalAmt as TotalAmt 
                         FROM TSPL_CUSTOMER_MASTER 
                         left outer join tspl_customer_master as Parent_Master on Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No 
                         RIGHT OUTER JOIN TSPL_SCRAPSALE_HEAD_Cancel_Data TSPL_SD_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.cust_Code 
                         RIGHT OUTER JOIN TSPL_ITEM_MASTER 
                         RIGHT OUTER JOIN TSPL_SCRAPSALE_DETAIL_Cancel_Data TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ON TSPL_SD_SALE_INVOICE_HEAD.shipment_No = TSPL_SD_SALE_INVOICE_DETAIL.shipment_No 
                         left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Loc_Code  where convert(date,TSPL_SD_SALE_INVOICE_HEAD.shipment_Date,103) >=  convert(Date,'" + clsCommon.GetPrintDate(fromDate.Value) + "',103)  And Convert(Date, TSPL_SD_SALE_INVOICE_HEAD.shipment_Date,103) <=  convert(Date,'" + clsCommon.GetPrintDate(ToDate.Value) + "',103) and Location_Code='" + txtUOM.Value + "' "
            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow("No data found to display")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        'If clsCommon.myLen(qry) > 0 Then
        '    dt = clsDBFuncationality.GetDataTable(qry)
        'End If
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        'Gv1.DataSource = Nothing
        'Gv1.GroupDescriptors.Clear()
        'Gv1.SummaryRowsBottom.Clear()
        'Gv1.DataSource = dt
        ''gv1.Columns("TransType").IsVisible = False
        ''gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
        'RadPageView1.SelectedPage = RadPageViewPage2
        'Gv1.BestFitColumns()
        'FormatGrid()
        'ReStoreGridLayout()
        'End If
    End Sub


    'Private Sub txtUOM_Load(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM.Load
    '    Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
    '    Dim WhrCls As String = " Location_Type='Physical'  "
    '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '        WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
    '    End If
    '    txtUOM.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtUOM.Value, "Code", isButtonClicked)
    '    lblBilltoLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtUOM.Value + "'"))
    'End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtUOM.Value = ""
        lblBilltoLocation.Text = ""
        Gv1.DataSource = Nothing
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtUOM.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtUOM.Value, "Code", isButtonClicked)
        lblBilltoLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtUOM.Value + "'"))
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Sales Cancelled Register")
            arrHeader.Add("From Period : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            Dim StrHeading As String = objCommonVar.CurrentCompanyName + Environment.NewLine + "Sales Cancelled Register From Period " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "DispathLedgerReport")
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            'clsCommon.MyOldExportToPDF(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "DispathLedgerReport")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Sales Cancelled Register")
            arrHeader.Add("From Period : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            Dim StrHeading As String = objCommonVar.CurrentCompanyName + Environment.NewLine + "Sales Cancelled Register From Period " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            'clsCommon.MyExportToPDF("a", Gv1, arrHeader, "b", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            clsCommon.MyOldExportToPDF(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "DispathLedgerReport")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    '    Private Sub btn_savelayout_Click(sender As Object, e As EventArgs) Handles btn_savelayout.Click
    '        If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
    '            Gv1.MasterTemplate.FilterDescriptors.Clear()
    '            Dim obj As New clsGridLayout()
    '            obj.ReportID = MyBase.Form_ID
    '            obj.UserID = objCommonVar.CurrentUserCode
    '            obj.GridLayout = New MemoryStream()
    '            Gv1.SaveLayout(obj.GridLayout)
    '            obj.GridColumns = Gv1.ColumnCount
    '            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            If obj.SaveData() Then
    '                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
    '            End If
    '            ''stuti regarding memory leakage
    '            obj.GridLayout.Close()
    '            obj.GridLayout.Dispose()
    '        End If
    'Private Sub btn_deletelayout_Click(sender As Object, e As EventArgs)

    '    End Sub
End Class
