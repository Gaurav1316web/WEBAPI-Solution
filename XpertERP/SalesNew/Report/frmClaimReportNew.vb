Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmClaimReportNew
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "CLaimReportNew"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
    Dim qry As String
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmClaimReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub FrmClaimReportNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = fromDate.Value
        dtFromDate.Value = fromDate.Value
        dtToDate.Value = fromDate.Value
        LoadCustomer()
        LoadVendor()
        LoadLocation()
        gv.AllowEditRow = False
        gv.AllowDragToGroup = False
        gv.AllowAddNewRow = False
    End Sub
    Sub LoadCustomer()
        qry = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 and Parent_Customer_YN='N' "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadVendor()
        qry = "Select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub
    Sub LoadLocation()
        qry = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        Try

            If cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast single customer.")
            End If
            If cbgVendor.CheckedValue.Count <> 1 Then
                Throw New Exception("Select only single vendor.")
            End If
            If cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast single location.")
            End If

            Dim Margin As String = clsCommon.myCstr(clsCommon.myCdbl(txtMargin.Text))

            If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then
                qry = "Select FINAL.[Invoice No], FINAL.[Invoice Type], FINAL.[Invoice Date], FINAL.[SKU Code], FINAL.[SKU Name], FINAL.UOM, FINAL.[Selling Qty], FINAL.MRP, FINAL.[Selling Rate]," + Environment.NewLine & _
    " FINAL.[Basic Selling Amount], FINAL.[Discount Amount], FINAL.[Amount After Discount], FINAL.[Tax %/Rate],FINAL.ParentCode,Parent_Master.Customer_Name as ParentName,FINAL.[Customer Code] ,FINAL.[Customer Name], FINAL.[Customer Ship To Location]," + Environment.NewLine & _
    " Landed_Cost_Amount as [SKU Purchase Rate]," + Environment.NewLine & _
    " [CST/Excise], (Landed_Cost_Amount+[CST/Excise])*[Selling Qty] as [Total Purchase Landed(Without Vat)]," + Environment.NewLine & _
    " FINAL.[Purchase Tax %/rate]," + Environment.NewLine & _
    " VendorName as [Principle/Vendor Name]," + Environment.NewLine & _
    " " + Margin + " as [Margin% on purchase landed]," + Environment.NewLine & _
    " (Landed_Cost_Amount+[CST/Excise])*[Selling Qty]*" + Margin + "/100 as [Margin In Rs.]," + Environment.NewLine & _
    " (Landed_Cost_Amount+[CST/Excise])*[Selling Qty]+((Landed_Cost_Amount+[CST/Excise])*[Selling Qty]*" + Margin + "/100) as [Margin Amount (Purchase+Margin)]," + Environment.NewLine & _
    " [Amount After Discount]-((Landed_Cost_Amount+[CST/Excise])*[Selling Qty]+((Landed_Cost_Amount+[CST/Excise])*[Selling Qty]*" + Margin + "/100)) as [Difference]," + Environment.NewLine & _
    " 0 as [Vat On Margin], [Amount After Discount]-((Landed_Cost_Amount+[CST/Excise])*[Selling Qty]+((Landed_Cost_Amount+[CST/Excise])*[Selling Qty]*" + Margin + "/100)) as [Total Claim] FROM( " + Environment.NewLine & _
     " Select YYY.Document_Code as [Invoice No], YYY.InvoiceType as [Invoice Type], YYY.Document_Date as [Invoice Date], YYY.SKUCode as [SKU Code], YYY.SKUName as [SKU Name], YYY.Unit_code as [UOM], YYY.Qty as [Selling Qty], YYY.MRP, YYY.Item_Cost as [Selling Rate], YYY.Amount as [Basic Selling Amount], YYY.Disc_Amt as [Discount Amount], YYY.Amt_Less_Discount as [Amount After Discount], 0 as [CST/Excise], YYY.TAX1_Rate as [Tax %/Rate],YYY.ParentCode,YYY.Customer_Code as [Customer Code] ,YYY.Customer_Name as [Customer Name], YYY.ShipToLocation as [Customer Ship To Location], XXXX.Landed_Cost_Amount*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Landed_Cost_Amount, VendorName, 0 as [Purchase Tax %/rate] from (" + Environment.NewLine & _
     " Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code, 'Sale Invoice' as [InvoiceType ], TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as [SKUCode], TSPL_ITEM_MASTER.Item_Desc as [SKUName], TSPL_SD_SALE_INVOICE_DETAIL.Unit_code, TSPL_SD_SALE_INVOICE_DETAIL.Qty, TSPL_SD_SALE_INVOICE_DETAIL.MRP, TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost, TSPL_SD_SALE_INVOICE_DETAIL.Amount, TSPL_SD_SALE_INVOICE_DETAIL.Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_CUSTOMER_MASTER.Parent_Customer_No as ParentCode ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocation from TSPL_SD_SALE_INVOICE_HEAD LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL On TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code LEFT OUTER JOIN TSPL_SHIP_TO_LOCATION ON TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location WHERE TSPL_SD_SALE_INVOICE_HEAD.Status=1 AND CONVERT(Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(fromDate.Value) + "' AND CONVERT(Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value) + "'" + Environment.NewLine & _
    " AND TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")" + Environment.NewLine & _
    " AND TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")" + Environment.NewLine & _
     " UNION ALL" + Environment.NewLine & _
    " Select TSPL_SD_SALE_RETURN_HEAD.Document_Code, 'Sale Return' as [InvoiceType], TSPL_SD_SALE_RETURN_HEAD.Document_Date, TSPL_SD_SALE_RETURN_DETAIL.Item_Code as [SKUCode], TSPL_ITEM_MASTER.Item_Desc as [SKUName], TSPL_SD_SALE_RETURN_DETAIL.Unit_code, TSPL_SD_SALE_RETURN_DETAIL.Qty, TSPL_SD_SALE_RETURN_DETAIL.MRP, TSPL_SD_SALE_RETURN_DETAIL.Item_Cost, TSPL_SD_SALE_RETURN_DETAIL.Amount, TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt, TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount, TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate,TSPL_CUSTOMER_MASTER.Parent_Customer_No as ParentCode,TSPL_SD_SALE_RETURN_HEAD.Customer_Code ,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocation from TSPL_SD_SALE_RETURN_HEAD LEFT OUTER JOIN TSPL_SD_SALE_RETURN_DETAIL On TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code LEFT OUTER JOIN TSPL_SHIP_TO_LOCATION ON TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SALE_RETURN_HEAD.Ship_To_Location" + Environment.NewLine & _
     " WHERE TSPL_SD_SALE_RETURN_HEAD.Status=1 AND CONVERT(Date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(Date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'" + Environment.NewLine & _
    " AND TSPL_SD_SALE_RETURN_HEAD.Customer_Code IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")" + Environment.NewLine & _
    " AND TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")" + Environment.NewLine & _
     " ) YYY LEFT OUTER JOIN (" + Environment.NewLine & _
     " Select Item_Code, MAX(BaseUnit) As BaseUnit, AVG(PurchaseTax) as PurchaseTax, SUM(Landed_Cost_Amount)/SUM(Base_Qty) as [Landed_Cost_Amount], MAX(TSPL_VENDOR_MASTER.Vendor_Name) as [VendorName] from (" + Environment.NewLine & _
     " Select TSPL_PI_HEAD.Vendor_Code, TSPL_PI_DETAIL.Item_Code, TSPL_PI_DETAIL.Unit_code, TSPL_PI_DETAIL.Landed_Cost_Amount, TSPL_PI_DETAIL.PI_Qty, TSPL_PI_DETAIL.PI_Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Base_Qty, BaseUnit.UOM_Code as BaseUnit, Case When TSPL_PI_HEAD.Tax_Group='CST' Then TSPL_PI_DETAIL.TAX1_Rate When TSPL_PI_HEAD.Tax_Group='EXCISE' Then TSPL_PI_DETAIL.TAX4_Rate Else 0 End as PurchaseTax from TSPL_PI_HEAD" + Environment.NewLine & _
     " LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_DETAIL.PI_No=TSPL_PI_HEAD.PI_No" + Environment.NewLine & _
     " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PI_DETAIL.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PI_DETAIL.Unit_code" + Environment.NewLine & _
     " left outer join TSPL_ITEM_UOM_DETAIL as BaseUnit on BaseUnit.Item_Code=TSPL_PI_DETAIL.Item_Code and BaseUnit.Conversion_Factor=1" + Environment.NewLine & _
     " WHERE TSPL_PI_HEAD.Status=1 AND CONVERT(Date,TSPL_PI_HEAD.PI_Date,103)>='" + clsCommon.GetPrintDate(dtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(Date,TSPL_PI_HEAD.PI_Date,103)<='" + clsCommon.GetPrintDate(dtToDate.Value, "dd/MMM/yyyy") + "' AND ISNULL(Against_SRN,'')<>''" + Environment.NewLine & _
     " AND TSPL_PI_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")" + Environment.NewLine & _
     " UNION ALL" + Environment.NewLine & _
     " Select TSPL_SRN_HEAD.Vendor_Code, TSPL_SRN_DETAIL.Item_Code, TSPL_SRN_DETAIL.Unit_code, TSPL_SRN_DETAIL.Landed_Cost_Amount, TSPL_SRN_DETAIL.SRN_Qty, TSPL_SRN_DETAIL.SRN_Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Base_Qty, BaseUnit.UOM_Code as BaseUnit, Case When TSPL_SRN_HEAD.Tax_Group='CST' Then TSPL_SRN_DETAIL.TAX1_Rate When TSPL_PI_HEAD.Tax_Group='EXCISE' Then TSPL_SRN_DETAIL.TAX4_Rate Else 0 End as PurchaseTax from TSPL_SRN_HEAD" + Environment.NewLine & _
     " LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No" + Environment.NewLine & _
     " LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.Against_SRN=TSPL_SRN_HEAD.SRN_No" + Environment.NewLine & _
     " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SRN_DETAIL.Unit_code left outer join TSPL_ITEM_UOM_DETAIL as BaseUnit on BaseUnit.Item_Code=TSPL_SRN_DETAIL.Item_Code and BaseUnit.Conversion_Factor=1" + Environment.NewLine & _
     " WHERE TSPL_SRN_HEAD.Status=1 AND CONVERT(Date,TSPL_SRN_HEAD.SRN_Date,103)>='" + clsCommon.GetPrintDate(dtFromDate.Value, "dd/MMM/yyyy") + "' AND CONVERT(Date,TSPL_SRN_HEAD.SRN_Date,103)<='" + clsCommon.GetPrintDate(dtToDate.Value, "dd/MMM/yyyy") + "' AND ISNULL(TSPL_PI_HEAD.Against_SRN,'')=''" + Environment.NewLine & _
     " AND TSPL_SRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")" + Environment.NewLine & _
     " ) XXX Right OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=XXX.Vendor_Code WHERE XXX.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") Group By Item_Code" + Environment.NewLine & _
     " ) XXXX ON XXXX.Item_Code=YYY.SKUCode" + Environment.NewLine & _
     " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=YYY.SKUCode AND TSPL_ITEM_UOM_DETAIL.UOM_Code=YYY .Unit_code WHERE ISNULL(VendorName,'')<>''" + Environment.NewLine & _
     " ) FINAL  left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=FINAL.ParentCode"

                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    gv.DataSource = dt
                    SetGridFormation()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
            ElseIf IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcel("Claim Report New", gv, Nothing, Me.Text)
            Else
                clsCommon.MyExportToPDF(Me.Text, gv, Nothing, Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SetGridFormation()
        For Each col As GridViewColumn In gv.Columns
            col.BestFit()
        Next
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(Exporter.Refresh)
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = fromDate.Value
        dtFromDate.Value = fromDate.Value
        dtToDate.Value = fromDate.Value
        cbgCustomer.UnCheckedAll()
        cbgVendor.UnCheckedAll()
        gv.DataSource = Nothing
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.Excel)
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.PDF)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
End Class
