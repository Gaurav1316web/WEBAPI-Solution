Imports common
Public Class frmAccountSalesReport
    Private Sub frmAccountSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim whrcls As String = ""
            whrcls = " Location_Type='Physical' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls += " and Location_Code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],TSPL_Location_MASTER.Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1 ,Hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Seg.Description as [Location Segment Description],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],tspl_location_master.Created_By as [Created By],tspl_location_master.Created_Date as [Created Date],tspl_location_master.Modify_By as [Modify By],tspl_location_master.Modify_Date as [Modify Date],tspl_location_master.Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C] ,Is_Consumption_Location as [Is Consumption Location],Is_Section as [Is Section],Section_Code as [Section Code],Is_Sub_Location as [Is Sub Location],Main_Location_Code as [Main Location Code],IsSubLocationWise as [Is Sub Location Wise] from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code "
            txtLocation.Value = clsCommon.ShowSelectForm("@Location", qry, "Code", whrcls, txtLocation.Value, "code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        BlankGrid()
        EnableDisable(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub EnableDisable(ByVal isVal As Boolean)
        txtFromDate.Enabled = isVal
        txtToDate.Enabled = isVal
        rgbDate.Enabled = isVal
        RadGroupBox1.Enabled = isVal
    End Sub

    Sub BlankGrid()
        gvData.AutoExpandGroups = True
        gvData.ShowGroupPanel = False
        gvData.ShowRowHeaderColumn = False
        gvData.AllowAddNewRow = False
        gvData.AllowDeleteRow = False
        gvData.EnableFiltering = True
        gvData.ShowFilteringRow = True
    End Sub

    Function ReturnBaseQry() As String
        Dim BaseQry As String = "SELECT CONVERT(VARCHAR(10), H.Document_Date, 103) AS Document_Date,H.Document_Code,C.Customer_Name,H.Total_Amt,H.Is_Taxable,I.Is_FreshItem,I.Is_Ambient,I.Item_Desc,D.Item_Net_Amt,I.HSN_Code,C.GST_Registered,C.GSTNO,CONCAT(C.Add1, C.Add2, C.Add3) AS Address,ISNULL(C.PIN_Code, C.PIN_NO) AS [Pin Code],H.Remarks
FROM TSPL_SD_SALE_INVOICE_DETAIL D
LEFT JOIN TSPL_SD_SALE_INVOICE_HEAD H ON H.Document_Code = D.Document_Code 
LEFT JOIN TSPL_CUSTOMER_MASTER C ON C.Cust_Code = H.Customer_Code
LEFT JOIN TSPL_ITEM_MASTER I ON I.Item_Code = D.Item_Code Where
CONVERT(date,H.Document_Date,103)>=Convert(Date,'" & txtFromDate.Value & "',103) And CONVERT(date,H.Document_Date,103)<=Convert(Date,'" & txtToDate.Value & "',103)"
        If clscommon.myLen(txtLocation.Value)>0 Then
            BaseQry &= " and (H.Bill_To_Location='" & clsCommon.myCstr(txtLocation.Value) & "' or H.Sub_Location_code='" & clsCommon.myCstr(txtLocation.Value) & "') "
        End If
        BaseQry &= " and H.Status=1 "
        Return BaseQry
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim Qry As String = Nothing
            If rbtnSaleVoucher.Checked Then
                Qry = "; With BaseQry As (" & ReturnBaseQry() & ")"
                Qry &= ",
--Header Row (Customer only)
HeaderRows AS
(SELECT DISTINCT Document_Date,CASE WHEN Is_Ambient = 1 AND Is_Taxable = 1 THEN 'TAX INVOICE' WHEN Is_FreshItem = 1 AND Is_Taxable = 0 THEN 'MILK' ELSE 'BILL OF SUPPLY' END AS [Sale Vch. Type Name],Document_Code,Customer_Name,Total_Amt,'Dr' AS [Dr/Cr],NULL AS Item_Desc,NULL AS Item_Net_Amt,NULL AS HSN_Code,GSTNO,Address,[Pin Code],Remarks,CASE  WHEN GST_Registered = 1 THEN 'Regular' ELSE 'Unregisterd'END AS [GST Type],1 AS SortOrder FROM BaseQry),
--Detail Rows (Item only)
DetailRows AS
(SELECT Document_Date,CASE WHEN Is_Ambient = 1 AND Is_Taxable = 1 THEN 'TAX INVOICE' WHEN Is_FreshItem = 1 AND Is_Taxable = 0 THEN 'MILK' ELSE 'BILL OF SUPPLY' END AS [Sale Vch. Type Name],Document_Code,NULL AS Customer_Name,NULL AS Total_Amt, NULL AS [Dr/Cr],Item_Desc,Item_Net_Amt,HSN_Code,NULL AS GSTNO,NULL AS Address,NULL AS [Pin Code],NULL AS Remarks,NULL AS [GST Type],2 AS SortOrder FROM BaseQry
)
--Final Output
SELECT Document_Date AS [Sale Vch. Date],[Sale Vch. Type Name],Document_Code AS [Vch. Number],Customer_Name AS [Ledger Name],Total_Amt AS [Ledger Amt.],[Dr/Cr],Item_Desc AS [Product/Ledger Name],Item_Net_Amt AS [Product Value],HSN_Code AS [HSN/SAC],GSTNO AS [Buyer/Supplier - GSTIN/UIN],Address,[Pin Code],Remarks AS [Vch. Narration],[GST Type] FROM
(
    SELECT * FROM HeaderRows
    UNION ALL
    SELECT * FROM DetailRows
) A
ORDER BY Document_Date,Document_Code,[Sale Vch. Type Name] "
            ElseIf rbtnHSNWise.Checked Then
                'Dim rpt As New rptHSNWiseSaleReport()
                'Dim BaseQry As String = rpt.ReturnQry(True, txtFromDate.Value, txtToDate.Value, txtLocation.Value)
                'rpt = Nothing
                'Qry = "Select * from  (" & BaseQry & ")finalQry"
            Else
                Dim rpt As New rptSaleInvoiceStatusReport()
                Dim BaseQry As String = rpt.BaseQryLoadDataInvoiceCount(txtFromDate.Value, txtToDate.Value, txtLocation.Value)
                rpt = Nothing
                Qry = "Select Invoice_Tax_Type As [Sale Voucher Type],First_Invoice As [Sr.No. From],Last_Invoice As [Sr. No. To],Total_Invoice As [Total Number],Total_CancelInvoice As [Cancelled] from (" & BaseQry & ")final order by Transcation_Type"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                BlankGrid()
                gvData.DataSource = dt
                gvData.AutoExpandGroups = True
                gvData.ShowGroupPanel = True
                gvData.ShowRowHeaderColumn = False
                gvData.AllowAddNewRow = False
                gvData.AllowDeleteRow = False
                gvData.EnableFiltering = True
                gvData.ShowFilteringRow = True
                gvData.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        Try
            If gvData.Rows.Count > 0 Then
                ExporttoExcel(EnumExportTo.Excel)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Sale Voucher Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Sale Voucher Report", gvData, arrHeader, "Sale Voucher Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        Try
            If gvData.Rows.Count > 0 Then
                ExportToExcel(EnumExportTo.PDF)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class