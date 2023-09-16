
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmAdvancePaymentRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "AdvancePaymentRegister"
    Dim arrBack As New List(Of String)
    Dim arrVSP As New ArrayList()
    Dim arrFarmer As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region

    Sub LoadData()
        Try
            Dim qry As String = "select Payment_No,max(Payment_Date) as Payment_Date,max(Vendor_Code) as Vendor_Code,max(Vendor_Name) as Vendor_Name," & _
                " max(GSTIN_NO_Vendor) as GSTIN_NO_Vendor,max(PurchaseOrder_No) as PurchaseOrder_No,max(PurchaseOrder_Date) as PurchaseOrder_Date," & _
                " sum(Payment_Amount) as Payment_Amount,sum(Bank_Charges) as Bank_Charges,max(SGST_RATE) as SGST_RATE,sum(SGST_AMT) as SGST_AMT,max(CGST_RATE) as CGST_RATE,sum(CGST_AMT) as CGST_AMT," & _
                " max(IGST_RATE) as IGST_RATE,sum(IGST_AMT) as IGST_AMT,max(Other_Tax_RATE) as Other_Tax_RATE,sum(Other_Tax_Amt) as Other_Tax_Amt," & _
                " sum(Total_Tax_Amt) as Total_Tax_Amt,max([Place of Supply]) as [Place of Supply] from " & _
                " (select TSPL_PAYMENT_HEADER.Payment_No,convert(varchar,TSPL_PAYMENT_HEADER.Payment_Date,103) as Payment_Date,TSPL_PAYMENT_HEADER.Vendor_Code, " & _
                " TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.GSTFinalNo as GSTIN_NO_Vendor, " & _
                " coalesce(TSPL_PAYMENT_HEADER.PurchaseOrder_No,PO_GST.PurchaseOrder_No) as PurchaseOrder_No,convert(varchar,coalesce(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,PO_GST.PurchaseOrder_Date),103) as PurchaseOrder_Date,TSPL_PAYMENT_HEADER.Payment_Amount,TSPL_PAYMENT_HEADER.Bank_Charges, " & _
                " (CASE WHEN TAX1.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX1_Rate WHEN TAX2.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX2_Rate " & _
                " WHEN TAX3.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX3_Rate WHEN TAX4.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX4_Rate " & _
                " WHEN TAX5.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX5_Rate ELSE 0 END) AS SGST_RATE, " & _
                " (CASE WHEN TAX1.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX1_Amt WHEN TAX2.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX2_Amt " & _
                " WHEN TAX3.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX3_Amt WHEN TAX4.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX4_Amt " & _
                " WHEN TAX5.Type='SGST' THEN TSPL_PAYMENT_HEADER.TAX5_Amt ELSE 0 END) AS SGST_AMT, " & _
                " (CASE WHEN TAX1.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX1_Rate WHEN TAX2.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX2_Rate " & _
                " WHEN TAX3.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX3_Rate WHEN TAX4.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX4_Rate " & _
                " WHEN TAX5.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX5_Rate ELSE 0 END) AS CGST_RATE, " & _
                " (CASE WHEN TAX1.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX1_Amt WHEN TAX2.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX2_Amt " & _
                " WHEN TAX3.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX3_Amt WHEN TAX4.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX4_Amt " & _
                " WHEN TAX5.Type='CGST' THEN TSPL_PAYMENT_HEADER.TAX5_Amt ELSE 0 END) AS CGST_AMT, " & _
                " (CASE WHEN TAX1.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX1_Rate WHEN TAX2.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX2_Rate " & _
                " WHEN TAX3.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX3_Rate WHEN TAX4.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX4_Rate " & _
                " WHEN TAX5.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX5_Rate ELSE 0 END) AS IGST_RATE, " & _
                " (CASE WHEN TAX1.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX1_Amt WHEN TAX2.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX2_Amt " & _
                " WHEN TAX3.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX3_Amt WHEN TAX4.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX4_Amt " & _
                " WHEN TAX5.Type='IGST' THEN TSPL_PAYMENT_HEADER.TAX5_Amt ELSE 0 END) AS IGST_AMT, " & _
                " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX1_Rate WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX2_Rate " & _
                " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX3_Rate WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX4_Rate " & _
                " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX5_Rate ELSE 0 END) AS Other_Tax_RATE, " & _
                " (CASE WHEN TAX1.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX1_Amt WHEN TAX2.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX2_Amt " & _
                " WHEN TAX3.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX3_Amt WHEN TAX4.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX4_Amt " & _
                " WHEN TAX5.Type NOT IN ('SGST','CGST','IGST') THEN TSPL_PAYMENT_HEADER.TAX5_Amt ELSE 0 END) AS Other_Tax_AMT, " & _
                " (coalesce(TSPL_PAYMENT_HEADER.TAX1_Amt,0)+coalesce(TSPL_PAYMENT_HEADER.TAX2_Amt,0)+coalesce(TSPL_PAYMENT_HEADER.TAX3_Amt,0) " & _
                " +coalesce(TSPL_PAYMENT_HEADER.TAX4_Amt,0)+coalesce(TSPL_PAYMENT_HEADER.TAX5_Amt,0)) as Total_Tax_Amt,coalesce(TSPL_LOCATION_MASTER.City_Code,coalesce(Loc_GST.City_Code,TSPL_CITY_MASTER.City_Name)) as [Place of Supply] " & _
                " from TSPL_PAYMENT_HEADER " & _
                " left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
                " left join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PAYMENT_HEADER.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
                " left join TSPL_PURCHASE_ORDER_HEAD PO_GST ON TSPL_PAYMENT_HEADER.PurchaseOrder_No_GST=PO_GST.PurchaseOrder_No " & _
                " left join TSPL_LOCATION_MASTER on TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                " left join TSPL_LOCATION_MASTER Loc_GST on PO_GST.Bill_To_Location=Loc_GST.Location_Code " & _
                " left join TSPL_CITY_MASTER on TSPL_VENDOR_MASTER.CITY_CODE=TSPL_CITY_MASTER.City_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX1 ON TSPL_PAYMENT_HEADER.TAX1=TAX1.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX2 ON TSPL_PAYMENT_HEADER.TAX2=TAX2.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX3 ON TSPL_PAYMENT_HEADER.TAX3=TAX3.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX4 ON TSPL_PAYMENT_HEADER.TAX4=TAX4.Tax_Code " & _
                " LEFT JOIN TSPL_TAX_MASTER TAX5 ON TSPL_PAYMENT_HEADER.TAX5=TAX5.Tax_Code " & _
                " where TSPL_PAYMENT_HEADER.Posted=1 and Payment_Type in ('AV','RC') and cast(TSPL_PAYMENT_HEADER.Payment_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "' " & _
                " And ((coalesce(TSPL_PAYMENT_HEADER.TAX1_Amt,0)+coalesce(TSPL_PAYMENT_HEADER.TAX2_Amt,0)+coalesce(TSPL_PAYMENT_HEADER.TAX3_Amt,0) " & _
                " +coalesce(TSPL_PAYMENT_HEADER.TAX4_Amt,0)+coalesce(TSPL_PAYMENT_HEADER.TAX5_Amt,0))>0 or Bank_Charges_Tax>0)" & _
                " union all " & _
                " select Payment_No,convert(varchar,Payment_Date,103) as Payment_Date,Vendor_Code,Vendor_Name,GSTIN_No_Vendor,PurchaseOrder_No,PurchaseOrder_Date,Payment_Amt,Bank_Charges,SGST_Rate,SGST_Amt, " & _
                " CGST_Rate,CGST_Amt,IGST_Rate,IGST_Amt,Other_Tax_Rate,Other_Tax_Amt,(coalesce(SGST_Amt,0)+coalesce(CGST_Amt,0)+coalesce(IGST_Amt,0)+coalesce(Other_Tax_Amt,0)) as Total_Tax_Amt,'' as [Place of Supply] from ( " & _
                " select TSPL_PAYMENT_BANK_CHARGES_TAX.Payment_No,max(TSPL_PAYMENT_HEADER.Payment_Date) as Payment_Date,max(TSPL_PAYMENT_HEADER.Vendor_Code) as Vendor_Code, " & _
                " max(TSPL_PAYMENT_HEADER.Vendor_Name) as Vendor_Name,'' as GSTIN_No_Vendor,'' as PurchaseOrder_No,null as PurchaseOrder_Date,0 as Payment_Amt,0 as Bank_Charges," & _
                " max(case when TSPL_TAX_MASTER.Type='SGST' then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Rate else null end) as SGST_Rate, " & _
                " sum(case when TSPL_TAX_MASTER.Type='SGST' then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Amount else null end) as SGST_Amt, " & _
                " max(case when TSPL_TAX_MASTER.Type='CGST' then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Rate else null end) as CGST_Rate, " & _
                " sum(case when TSPL_TAX_MASTER.Type='CGST' then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Amount else null end) as CGST_Amt, " & _
                " max(case when TSPL_TAX_MASTER.Type='IGST' then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Rate else null end) as IGST_Rate, " & _
                " sum(case when TSPL_TAX_MASTER.Type='IGST' then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Amount else null end) as IGST_Amt, " & _
                " max(case when TSPL_TAX_MASTER.Type not in ('SGST','CGST','IGST') then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Rate else null end) as Other_Tax_Rate," & _
                " sum(case when TSPL_TAX_MASTER.Type not in ('SGST','CGST','IGST') then TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Amount else null end) as Other_Tax_Amt " & _
                " from TSPL_PAYMENT_BANK_CHARGES_TAX  " & _
                " inner join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_BANK_CHARGES_TAX.Payment_No=TSPL_PAYMENT_HEADER.Payment_No " & _
                " left join TSPL_TAX_MASTER on TSPL_PAYMENT_BANK_CHARGES_TAX.Tax_Code=TSPL_TAX_MASTER.Tax_Code " & _
                " where TSPL_PAYMENT_HEADER.Posted=1 and TSPL_PAYMENT_HEADER.Payment_Type in ('AV','RC') and cast(TSPL_PAYMENT_HEADER.Payment_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "' " & _
                " group by TSPL_PAYMENT_BANK_CHARGES_TAX.Payment_No) as Bank_Charges_Tax) as Fin group by Payment_No"

            If Not TxtVendorCode.arrValueMember Is Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
                qry = qry & " and TSPL_PAYMENT_HEADER.Vendor_Code in (" & clsCommon.GetMulcallString(TxtVendorCode.arrValueMember) & ")"
                    End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv3.DataSource = Nothing
            gv3.Rows.Clear()
            gv3.Columns.Clear()
            gv3.DataSource = dt
            gv3.GroupDescriptors.Clear()
            gv3.MasterTemplate.BestFitColumns()
            gv3.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2

            gv3.ReadOnly = True
            btnGenrate.Enabled = True
            SetGridLayout()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            btnGenrate.Enabled = True
        End Try
    End Sub
    Sub SetGridLayout()

        gv3.Columns("Payment_No").Width = 100
        gv3.Columns("Payment_No").HeaderText = "Payment No"

        gv3.Columns("Payment_Date").Width = 100
        gv3.Columns("Payment_Date").HeaderText = "Payment Date"

        gv3.Columns("Vendor_Code").Width = 100
        gv3.Columns("Vendor_Code").HeaderText = "Vendor Code"

        gv3.Columns("Vendor_Name").Width = 100
        gv3.Columns("Vendor_Name").HeaderText = "Vendor Name"

        gv3.Columns("GSTIN_NO_Vendor").Width = 100
        gv3.Columns("GSTIN_NO_Vendor").HeaderText = "GSTIN No"

        gv3.Columns("PurchaseOrder_No").Width = 100
        gv3.Columns("PurchaseOrder_No").HeaderText = "Order/Invoice No"

        gv3.Columns("PurchaseOrder_Date").Width = 100
        gv3.Columns("PurchaseOrder_Date").HeaderText = "Document Date"

        gv3.Columns("Payment_Amount").Width = 100
        gv3.Columns("Payment_Amount").HeaderText = "Advance Amount"

        gv3.Columns("Bank_Charges").Width = 100
        gv3.Columns("Bank_Charges").HeaderText = "Bank Charges"

        gv3.Columns("SGST_RATE").Width = 100
        gv3.Columns("SGST_RATE").HeaderText = "SGST Rate"

        gv3.Columns("SGST_RATE").Width = 100
        gv3.Columns("SGST_RATE").HeaderText = "SGST Rate"

        gv3.Columns("SGST_AMT").Width = 100
        gv3.Columns("SGST_AMT").HeaderText = "SGST Amt"

        gv3.Columns("CGST_RATE").Width = 100
        gv3.Columns("CGST_RATE").HeaderText = "CGST Rate"

        gv3.Columns("CGST_AMT").Width = 100
        gv3.Columns("CGST_AMT").HeaderText = "CGST Amt"

        gv3.Columns("IGST_RATE").Width = 100
        gv3.Columns("IGST_RATE").HeaderText = "IGST Rate"

        gv3.Columns("IGST_AMT").Width = 100
        gv3.Columns("IGST_AMT").HeaderText = "IGST Amt"

        gv3.Columns("Other_Tax_RATE").Width = 100
        gv3.Columns("Other_Tax_RATE").HeaderText = "Other Tax Rate"

        gv3.Columns("Other_Tax_AMT").Width = 100
        gv3.Columns("Other_Tax_AMT").HeaderText = "Other Tax Amt"

        gv3.Columns("Total_Tax_Amt").Width = 100
        gv3.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"

        gv3.Columns("Place of Supply").Width = 100
        gv3.Columns("Place of Supply").HeaderText = "Place of Supply"

        gv3.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Debit", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Credit", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub frmAdvancePaymentRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAdvancePaymentRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        btnGenrate.Enabled = True
        'rbtnSummary.IsChecked = True
        gv3.DataSource = Nothing

        'txtLocation.arrValueMember = Nothing

        TxtVendorCode.arrValueMember = Nothing
        'txtMultDistr.arrValueMember = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmAdvancePaymentRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = ReportID
        TemplateGridview = gv3
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Advance Payment Register")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Distributer Ledger Report", gv3, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Advance Payment Register", gv3, arr, "Advance Payment Register", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Advance Payment Register")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Advance Payment Register", gv3, arr, "Advance Payment Register", False)
    End Sub

#Region "grid operations"


#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)

        'arr.Add("Advance Payment Register")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If TxtVendorCode.arrValueMember IsNot Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
        '    arr.Add(" Vendor Code : " + clsCommon.GetMulcallStringWithComma(TxtVendorCode.arrDispalyMember))
        'End If
        ''clsCommon.MyExportToExcel("Distributer Ledger Report", gv3, arr, "Salary Register")
        'If gv3.Rows.Count <= 0 Then
        '    gv3.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Advance Payment Register", gv3, arr, "Advance Payment Register", False)
        'End If
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)

        'arr.Add("Advance Payment Register")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If TxtVendorCode.arrValueMember IsNot Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
        '    arr.Add(" Vendor Code : " + clsCommon.GetMulcallStringWithComma(TxtVendorCode.arrDispalyMember))
        'End If
        'clsCommon.MyExportToPDF("Advance Payment Register", gv3, arr, "Advance Payment Register", False)
        Export(EnumExportTo.PDF)
    End Sub
    ' ============= Addded by Preeti gupta============
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where PROGRAM_CODE='" & clsUserMgtCode.frmAdvancePaymentRegister & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            If TxtVendorCode.arrValueMember IsNot Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor Code : " + clsCommon.GetMulcallStringWithComma(TxtVendorCode.arrDispalyMember))
            End If
            'If txtMultDistr.arrValueMember IsNot Nothing AndAlso txtMultDistr.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Distributer : " + clsCommon.GetMulcallStringWithComma(txtMultDistr.arrDispalyMember))
            'End If
            'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
            'End If
            'arrHeader.Add("Pay Period: " + txtFromPP.Value)
            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Advance Payment Register", gv3, arrHeader, "Advance Payment Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtVendorCode._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER where 2=2 "
        TxtVendorCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivVendorMulSel", qry, "Code", "Name", TxtVendorCode.arrValueMember, TxtVendorCode.arrDispalyMember)
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub
End Class
