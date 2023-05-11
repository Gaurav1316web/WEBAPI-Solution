Imports common
Imports System.Data.SqlClient
Public Class frmBulkMilkPurchaseInvoiceMultiple
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim BulkProcPriceChartStandardRateWithZero As Integer = 0
    Dim IsItemMilkType As Integer = 0
    Dim IsPriceChartGradeWise As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Dim isSRNselected As Boolean = False
    Dim AllowTruncateAmount As Boolean = False
    Public isLoadData As Boolean = False
    Dim AllowDateChanged As Boolean = False
    Dim settApplyTCSTax As Boolean = False

    Public Const colSelect As String = "colSelect"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorName As String = "colVendorName"
    Public Const colSRNTrade As String = "colSRNTrade"
    Public Const colSRNNo As String = "SRNNO"
    Public Const colSRNDATe As String = "SRNDate"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colPendingQty As String = "colPendingQty"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colFatRate As String = "colFATRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colAmt As String = "colAmt"
    Public Const colDeduc As String = "colDeduc"
    Public Const colIncen As String = "colIncen"
    Public Const colSpecialDeduc As String = "colSpecialDeduc"
    Public Const colActAmt As String = "colActAmt"
    Public Const colNetRate As String = "colNetRate"
    Public Const colPriceCode As String = "colPriceCode"
    Public Const colTnkrNo As String = "colTnkrNo"


    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"
    Const colTaxOnBaseAmt1 As String = "colTaxOnBaseAmt1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"
    Const colTaxOnBaseAmt2 As String = "colTaxOnBaseAmt2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"
    Const colTaxOnBaseAmt3 As String = "colTaxOnBaseAmt3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"
    Const colTaxOnBaseAmt4 As String = "colTaxOnBaseAmt4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"
    Const colTaxOnBaseAmt5 As String = "colTaxOnBaseAmt5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"
    Const colTaxOnBaseAmt6 As String = "colTaxOnBaseAmt6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"
    Const colTaxOnBaseAmt7 As String = "colTaxOnBaseAmt7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"
    Const colTaxOnBaseAmt8 As String = "colTaxOnBaseAmt8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"
    Const colTaxOnBaseAmt9 As String = "colTaxOnBaseAmt9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colTaxOnBaseAmt10 As String = "colTaxOnBaseAmt10"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colItemNetAmt As String = "AMTAFTERTAX"


    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"


    Public isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsMilkPurchaseInvoiceHead = Nothing
    Dim IsInsideLoadData As Boolean = False
#End Region

    Private Sub FrmMilkPurchaseInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RadPageView1.SelectedPage = RadPageViewPage1
        settApplyTCSTax = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyTaxInBulkMilkPurchaseInvoice, clsFixedParameterCode.ApplyTaxInBulkMilkPurchaseInvoice, Nothing)) = 1)
        SetUserMgmtNew()
        BulkProcPriceChartStandardRateWithZero = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        AllowTruncateAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_AmountTruncate_BulkMilkSRN, clsFixedParameterCode.Allow_AmountTruncate_BulkMilkSRN, Nothing)) = "1", True, False)
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        txtInvoiceDate.Value = clsCommon.GETSERVERDATE()
        chkAutoPost.Checked = True
        If Not settApplyTCSTax Then
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = True
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBulkMilkPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub TxtVendorUpdate__MYValidating_1(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycle._MYValidating
        Dim qry As String = "select PC_CODE as CODE,DESCRIPTION,PC_VALUE as VALUE,PC_TYPE as TYPE  from TSPL_PAYMENT_CYCLE_MASTER "
        txtPaymentCycle.Value = clsCommon.ShowSelectForm("PayCFinder", qry, "CODE", "", txtPaymentCycle.Value, "CODE", isButtonClicked)
        If clsCommon.myLen(txtPaymentCycle.Value) > 0 Then
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
            SetToDate()
        End If
    End Sub

    Sub SetToDate()
        Dim qry As String = "select PC_CODE as CODE,DESCRIPTION,PC_VALUE as VALUE,PC_TYPE as TYPE  from TSPL_PAYMENT_CYCLE_MASTER "
        qry += "Where PC_CODE='" + txtPaymentCycle.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim PaymentCycleType As String = clsCommon.myCstr(dt.Rows(0)("TYPE"))
            Dim PaymentCycleValue As Integer = clsCommon.myCdbl(dt.Rows(0)("VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because payment Cycle of " & PaymentCycleValue & " Day ")
                    txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate.Value = txtFromDate.Value
                    Exit Sub
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)
                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because payment Cycle of Month Type")
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because payment Cycle of Year Type")
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            End If
        End If
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " SELECT distinct xxx .Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name    FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,max(TSPL_Bulk_MILK_SRN.Vendor_Code) as Vendor_code ,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty, " &
            "(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join " &
            "( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl " &
            "on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code, " &
            "(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   " &
            "left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO " &
            "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxx.Vendor_Code    " &
            "left outer join Tspl_Gate_Entry_Details on TSPL_Bulk_MILK_SRN.Gate_Entry_No= Tspl_Gate_Entry_Details.Gate_Entry_No "
        qry += " where TSPL_VENDOR_MASTER.Status='N' and (xxx.Net_Weight-xxx.invoice_qty) >0  and   Tspl_Gate_Entry_Details.Date_And_Time >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and  Tspl_Gate_Entry_Details.Date_And_Time <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'  and TSPL_VENDOR_MASTER.PC_CODE='" + txtPaymentCycle.Value + "' "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("SRNVENDFND", qry, "Vendor_Code", Nothing, txtVendor.arrValueMember, Nothing)
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        txtLocation.Value = clsLocation.getFinder("", txtLocation.Value, isButtonClicked)
        lblLocationName.Text = clsLocation.GetName(txtLocation.Value, Nothing)
    End Sub

    Sub EnableDisableControl(ByVal val As Boolean)
        txtPaymentCycle.Enabled = val
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        txtLocation.Enabled = val
        txtVendor.Enabled = val
        txtInvoiceDate.Enabled = val
        chkAutoPost.Enabled = val
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        EnableDisableControl(True)
    End Sub

    Sub LoadBlankGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()

        Dim repoCheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheck.FormatString = ""
        repoCheck.HeaderText = " "
        repoCheck.Name = colSelect
        repoCheck.Width = 60
        repoCheck.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoCheck)

        repoCheck = New GridViewCheckBoxColumn()
        repoCheck.FormatString = ""
        repoCheck.HeaderText = "Is SRN Trade"
        repoCheck.Name = colSRNTrade
        repoCheck.Width = 60
        repoCheck.ReadOnly = True
        repoCheck.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoCheck)

        Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTnkrNo.FormatString = ""
        repoTnkrNo.HeaderText = "Tanker No"
        repoTnkrNo.Name = colTnkrNo
        repoTnkrNo.Width = 100
        repoTnkrNo.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTnkrNo)

        Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRNNO.FormatString = ""
        repoSRNNO.HeaderText = "Vendor Code"
        repoSRNNO.Name = colVendorCode
        repoSRNNO.Width = 150
        repoSRNNO.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSRNNO)

        repoSRNNO = New GridViewTextBoxColumn()
        repoSRNNO.FormatString = ""
        repoSRNNO.HeaderText = "Vendor"
        repoSRNNO.Name = colVendorName
        repoSRNNO.Width = 200
        repoSRNNO.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSRNNO)

        repoSRNNO = New GridViewTextBoxColumn()
        repoSRNNO.FormatString = ""
        repoSRNNO.HeaderText = "SRN No"
        repoSRNNO.Name = colSRNNo
        repoSRNNO.Width = 200
        repoSRNNO.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSRNNO)

        Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoSRNDate.FormatString = "{0:d}"
        repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        repoSRNDate.HeaderText = "SRN Date"
        repoSRNDate.Name = colSRNDATe
        repoSRNDate.Width = 100
        repoSRNDate.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSRNDate)

        Dim repoChName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChName.FormatString = ""
        repoChName.HeaderText = "Chamber Desc"
        repoChName.Name = colChamberDesc
        repoChName.Width = 180
        repoChName.ReadOnly = True
        If TankerFromMaster = 0 Then repoChName.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoChName)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colItemDesc
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoIName)


        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.ReadOnly = True
        repoUOM.Width = 80
        repoUOM.WrapText = True
        repoUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoUOM)

        Dim repoGrossWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrossWeight.FormatString = ""
        repoGrossWeight.HeaderText = "Gross Weight"
        repoGrossWeight.Name = colGrossWeight
        repoGrossWeight.Width = 100
        repoGrossWeight.ReadOnly = True
        repoGrossWeight.IsVisible = False
        repoGrossWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoGrossWeight)

        Dim repoTareWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTareWeight.FormatString = ""
        repoTareWeight.HeaderText = "Tare Weight"
        repoTareWeight.Name = colTareWeight
        repoTareWeight.ReadOnly = True
        repoTareWeight.Width = 100
        repoTareWeight.WrapText = True
        repoTareWeight.IsVisible = False
        repoTareWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTareWeight)

        Dim repoNetWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNetWeight.FormatString = ""
        repoNetWeight.HeaderText = "Net Weight"
        repoNetWeight.Name = colNetWeight
        repoNetWeight.ReadOnly = True
        repoNetWeight.Width = 100
        repoNetWeight.WrapText = True
        repoNetWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoNetWeight)

        Dim repoPendingQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Qty"
        repoPendingQty.Name = colPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.WrapText = True
        repoPendingQty.Width = 100
        If TankerFromMaster = 1 Then
            repoPendingQty.IsVisible = False
        End If
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoInvoiceQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceQty.FormatString = ""
        repoInvoiceQty.HeaderText = "Invoice Qty"
        repoInvoiceQty.Name = colQty
        repoInvoiceQty.ReadOnly = True
        repoInvoiceQty.WrapText = True
        repoInvoiceQty.Width = 100
        repoInvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoInvoiceQty)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCode
        repoPriceCode.ReadOnly = True
        repoPriceCode.Width = 80
        repoPriceCode.WrapText = True
        repoPriceCode.IsVisible = False
        repoPriceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "FAT(%)"
        repoFatPer.Name = colFat
        repoFatPer.ReadOnly = True
        repoFatPer.Width = 80
        repoFatPer.WrapText = True
        repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoFatPer)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF(%)"
        repoSNFPer.Name = colSNF
        repoSNFPer.ReadOnly = True
        repoSNFPer.Width = 80
        repoSNFPer.WrapText = True
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSNFPer)


        Dim repoFATKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATKG.FormatString = ""
        repoFATKG.HeaderText = "FAT(KG)"
        repoFATKG.Name = colFatKG
        repoFATKG.ReadOnly = True
        repoFATKG.Width = 80
        repoFATKG.WrapText = True
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoFATKG)

        Dim repoSNFKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF(KG)"
        repoSNFKG.Name = colSNFKG
        repoSNFKG.ReadOnly = True
        repoSNFKG.Width = 80
        repoSNFKG.WrapText = True
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSNFKG)

        Dim repoAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmt
        repoAmount.ReadOnly = True
        repoAmount.Width = 100
        repoAmount.WrapText = True
        repoAmount.IsVisible = False
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoAmount)

        Dim repoDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDeduc.FormatString = ""
        repoDeduc.HeaderText = "Deduction"
        repoDeduc.Name = colDeduc
        repoDeduc.ReadOnly = True
        repoDeduc.Width = 80
        repoDeduc.WrapText = True
        repoDeduc.IsVisible = False
        repoDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoDeduc)

        Dim repoIncen As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIncen.FormatString = ""
        repoIncen.HeaderText = "Incentive"
        repoIncen.Name = colIncen
        repoIncen.ReadOnly = True
        repoIncen.Width = 80
        repoIncen.WrapText = True
        repoIncen.IsVisible = False
        repoIncen.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoIncen)

        Dim repoSpclDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpclDeduc.FormatString = ""
        repoSpclDeduc.HeaderText = "Special Deduction"
        repoSpclDeduc.Name = colSpecialDeduc
        repoSpclDeduc.ReadOnly = True
        repoSpclDeduc.Width = 80
        repoSpclDeduc.WrapText = True
        repoSpclDeduc.IsVisible = False
        repoSpclDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSpclDeduc)


        Dim repoActAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Net Rate"
        repoActAmt.Name = colNetRate
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoActAmt)

        Dim repoFATRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATRate.FormatString = ""
        repoFATRate.HeaderText = "FAT Rate"
        repoFATRate.Name = colFatRate
        repoFATRate.ReadOnly = True
        repoFATRate.Width = 80
        repoFATRate.WrapText = True
        repoFATRate.IsVisible = True
        repoFATRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoFATRate)


        Dim repoSNFRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFRate.FormatString = ""
        repoSNFRate.HeaderText = "SNF Rate"
        repoSNFRate.Name = colSNFRate
        repoSNFRate.ReadOnly = True
        repoSNFRate.Width = 80
        repoSNFRate.WrapText = True
        repoSNFRate.IsVisible = True
        repoSNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSNFRate)

        repoActAmt = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Amount"
        repoActAmt.Name = colActAmt
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoActAmt)



        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax1) '26

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt1) '27

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate1) '28

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt1) '29

        Dim repoIsSurTax1 = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax1) '30

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode1) '31

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1) '32

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable1) '106

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 1"
        repoIsTaxable1.Name = colTaxOnBaseAmt1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1) '32

        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax2) '34

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt2) '35

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate2) '36

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt2) '37

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax2) '38

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode2) '39

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable2) '40

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 2"
        repoIsTaxable1.Name = colTaxOnBaseAmt2
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable2)

        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax3) '42

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt3) '43

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate3) '44

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt3) '45

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax3) '46

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode3) '47

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable3) '48

        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable3)

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax4) '50

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 3"
        repoIsTaxable1.Name = colTaxOnBaseAmt3
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt4) '51

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate4) '52

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt4) '53

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax4) '54

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode4) '55

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable4) '56

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 4"
        repoIsTaxable1.Name = colTaxOnBaseAmt4
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)


        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable4)

        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax5) '58

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt5) '59

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate5) '60

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt5) '61

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax5) '62

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode5) '63

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable5) '64

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 5"
        repoIsTaxable1.Name = colTaxOnBaseAmt5
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax6) '66

        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable5)

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt6) '67

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable6) '72

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 6"
        repoIsTaxable1.Name = colTaxOnBaseAmt6
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable6)

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax7) '74

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable7) '80

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 7"
        repoIsTaxable1.Name = colTaxOnBaseAmt7
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable7)

        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax8) '82

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable8) '88

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 8"
        repoIsTaxable1.Name = colTaxOnBaseAmt8
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable8)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax9) '90

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable9) '96

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 9"
        repoIsTaxable1.Name = colTaxOnBaseAmt9
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable9)

        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax10) '98

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable10) '104

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 10"
        repoIsTaxable1.Name = colTaxOnBaseAmt10
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)



        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsExcisable10) '115


        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTotTaxAmt) '116

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Net Amount"
        repoAmtAfterTax.Name = colItemNetAmt
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoAmtAfterTax) '117



        gvItem.AllowAddNewRow = False
        gvItem.AllowColumnChooser = True
        gvItem.ShowGroupPanel = False
        gvItem.AllowColumnReorder = True
        gvItem.AllowRowReorder = True
        gvItem.EnableSorting = False
        gvItem.MasterTemplate.ShowRowHeaderColumn = False
        gvItem.MasterTemplate.ShowColumnHeaders = True
        gvItem.EnableAlternatingRowColor = True
        gvItem.EnableFiltering = True
        gvItem.ShowFilteringRow = True
        gvItem.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            LoadBlankGrid()
            LoadBlankGridTax()
            If clsCommon.myLen(txtPaymentCycle.Value) <= 0 Then
                Throw New Exception("Please select payment cycle")
            End If
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                Throw New Exception("From Date can't be greator than To Date")
            End If
            If clsCommon.GetDateWithStartTime(txtToDate.Value) > clsCommon.GetDateWithStartTime(txtInvoiceDate.Value) Then
                Throw New Exception("To Date can't be greater than Invoice Date")
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If txtVendor.arrValueMember Is Nothing OrElse txtVendor.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select vendor")
            End If
            Dim strZeroSRN As String = ""
            If BulkProcPriceChartStandardRateWithZero = 1 Then
                strZeroSRN = " and TSPL_Bulk_MILK_SRN.Actual_Amount > 0 "
            End If
            Dim qry As String = " SELECT xxx.*,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,TSPL_Bulk_MILK_SRN.SRN_Date ,TSPL_Bulk_MILK_SRN.UOM, " &
                "TSPL_Bulk_MILK_SRN.FormType   FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty, " &
                "(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join " &
                "( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  ) as  tdl " &
                "on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code, " &
                "(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO " &
                "left outer join Tspl_Gate_Entry_Details on TSPL_Bulk_MILK_SRN.Gate_Entry_No= Tspl_Gate_Entry_Details.Gate_Entry_No " &
                "where (xxx.Net_Weight-xxx.invoice_qty) >0 " +
            " and TSPL_Bulk_MILK_SRN.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")" + Environment.NewLine +
            " and Tspl_Gate_Entry_Details.Date_And_Time >=  '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and Tspl_Gate_Entry_Details.Date_And_Time <=  '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'" &
            " and TSPL_Bulk_MILK_SRN.Loc_Code='" & txtLocation.Value & "'"
            Dim dtData As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                IsInsideLoadData = True
                Dim objB As clsBulkMilkSRN
                For Each dr As DataRow In dtData.Rows
                    objB = clsBulkMilkSRN.getData(clsCommon.myCstr(dr("SRN_NO")), NavigatorType.Current)
                    If objB IsNot Nothing AndAlso objB.Arr.Count > 0 Then
                        For Each objTr As clsBulkMilkSRNChemberNoDetails In objB.Arr
                            gvItem.Rows.AddNew()
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSelect).Value = True
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVendorCode).Value = objB.Vendor_Code
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colVendorName).Value = clsVendorMaster.GetName(objB.Vendor_Code, Nothing)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSRNNo).Value = objB.SRN_NO
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSRNDATe).Value = clsCommon.GetPrintDate(objB.SRN_Date, "dd/MM/yyyy hh:mm:ss tt")
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSRNTrade).Value = (clsCommon.CompairString(clsCommon.myCstr(dr("FormType")), "Bulk Milk SRN Trade") = CompairStringResult.Equal)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTnkrNo).Value = objB.Tanker_No
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myFormat(objTr.Gross_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myFormat(objTr.Tare_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objTr.Net_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetRate).Value = clsCommon.myFormat(objTr.NetRate)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = clsCommon.myFormat(objTr.Net_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objTr.fat_per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat(objTr.fat_KG, False, True, False, 3, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colPriceCode).Value = objTr.Price_Code
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objTr.snf_Per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat(objTr.SNF_KG, False, True, False, 3, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(objTr.fat_Rate)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(objTr.SNF_Rate)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = objTr.Actual_Amount
                            If AllowTruncateAmount Then
                                Dim xNewAmt As Double = objTr.Actual_Amount
                                If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                    xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                End If
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = CInt(xNewAmt)
                            End If
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemNetAmt).Value = clsCommon.myCdbl(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value)
                        Next
                    End If
                Next
                If settApplyTCSTax Then
                    SetTax()
                End If

                IsInsideLoadData = False
                EnableDisableControl(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        If clsCommon.myLen(txtPaymentCycle.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select payment cycle", Me.Text)
            txtPaymentCycle.Focus()
        Else
            SetToDate()
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvItem.ValueChanging
        Try
            If gvItem.CurrentRow.Index >= 0 AndAlso Not IsInsideLoadData Then
                IsInsideLoadData = True
                If gvItem.CurrentColumn Is gvItem.Columns(colSelect) Then
                    Dim strSRNNo As String = clsCommon.myCstr(gvItem.CurrentRow.Cells(colSRNNo).Value)
                    For ii As Integer = 0 To gvItem.Rows.Count - 1
                        If ii = gvItem.CurrentRow.Index Then
                            Continue For
                        End If
                        If clsCommon.CompairString(strSRNNo, clsCommon.myCstr(gvItem.Rows(ii).Cells(colSRNNo).Value)) = CompairStringResult.Equal Then
                            gvItem.Rows(ii).Cells(colSelect).Value = e.NewValue
                        End If
                    Next
                End If
                IsInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmMilkPurchaseInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If allowToSave() Then
                Dim arr As Dictionary(Of String, TempSRNTrade) = New Dictionary(Of String, TempSRNTrade)
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    If clsCommon.myCBool(gvItem.Rows(ii).Cells(colSelect).Value) Then
                        If arr.ContainsKey(clsCommon.myCstr(gvItem.Rows(ii).Cells(colVendorCode).Value)) Then
                            If clsCommon.myCBool(gvItem.Rows(ii).Cells(colSRNTrade).Value) Then
                                arr(clsCommon.myCstr(gvItem.Rows(ii).Cells(colVendorCode).Value)).SRNTade = True
                            Else
                                arr(clsCommon.myCstr(gvItem.Rows(ii).Cells(colVendorCode).Value)).Normal = True
                            End If
                        Else
                            Dim obj As New TempSRNTrade
                            If clsCommon.myCBool(gvItem.Rows(ii).Cells(colSRNTrade).Value) Then
                                obj.SRNTade = True
                            Else
                                obj.Normal = True
                            End If
                            arr.Add(clsCommon.myCstr(gvItem.Rows(ii).Cells(colVendorCode).Value), obj)
                        End If
                    End If
                Next
                If arr Is Nothing OrElse arr.Count <= 0 Then
                    Throw New Exception("No data found to save")
                End If


                Dim dtServer As DateTime = clsCommon.GETSERVERDATE()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()


                Try
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.BulkMilkPurchaseInvoiceMultiple, txtLocation.Value, txtInvoiceDate.Value, trans)

                    clsCommon.ProgressBarPercentShow()
                    Dim ii As Integer = 1
                    For Each str As String In arr.Keys
                        CreateInvoice(str, arr(str).SRNTade, dtServer, trans)
                        clsCommon.ProgressBarPercentUpdate(ii * 100 / arr.Keys.Count, "")
                        ii += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Invoice created successfully", Me.Text)
                    LoadBlankGrid()
                    LoadBlankGridTax()
                    EnableDisableControl(True)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Function CreateInvoice(ByVal strVendorCode As String, ByVal isTradeSRN As Boolean, ByVal dtServerDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        obj = New clsMilkPurchaseInvoiceHead
        If isTradeSRN Then
            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, txtInvoiceDate.Value, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, txtLocation.Value)
        Else
            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, txtInvoiceDate.Value, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithoutVendorInvoiceNo, txtLocation.Value)
        End If
        If clsCommon.myLen(obj.DOC_NO) <= 0 Then
            Throw New Exception("Error In Document No Genertion")
        End If
        obj.DOC_DATE = txtToDate.Value
        obj.vendor_code = strVendorCode
        obj.Loc_Code = txtLocation.Value
        obj.Vendor_Invoice_No = ""
        obj.SRN_From_Date = txtFromDate.Value
        obj.SRN_TO_Date = txtToDate.Value
        obj.isSRNTradeInvoice = IIf(isTradeSRN, 1, 0)
        obj.isPosted = 0
        obj.Modified_By = objCommonVar.CurrentUserCode
        obj.Modified_Date = clsCommon.GetPrintDate(dtServerDate, "dd/MM/yyyy hh:mm:ss tt")
        obj.Comp_Code = objCommonVar.CurrentCompanyCode
        obj.Created_By = objCommonVar.CurrentUserCode
        obj.Created_Date = clsCommon.GetPrintDate(dtServerDate, "dd/MM/yyyy hh:mm:ss tt")
        obj.Total_FAT_KG = 0
        obj.Total_SNF_KG = 0
        obj.Total_QTY = 0
        obj.Total_AMT = 0
        obj.RoundOffAmount = 0

        obj.Tax_Group = txtTaxGroup.Value
        obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
        If (gv2.Rows.Count > 0) Then
            obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
            obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
        End If
        If (gv2.Rows.Count > 1) Then
            obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
            obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
        End If
        If (gv2.Rows.Count > 2) Then
            obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
            obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
        End If
        If (gv2.Rows.Count > 3) Then
            obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
            obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)

        End If
        If (gv2.Rows.Count > 4) Then
            obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
            obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)

        End If
        If (gv2.Rows.Count > 5) Then
            obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
            obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)

        End If
        If (gv2.Rows.Count > 6) Then
            obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
            obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)

        End If
        If (gv2.Rows.Count > 7) Then
            obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
            obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)

        End If
        If (gv2.Rows.Count > 8) Then
            obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
            obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)

        End If
        If (gv2.Rows.Count > 9) Then
            obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
            obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)

        End If
        Dim intCounter As Integer = 1
        Dim objDetail As New clsMilkPurchaseInvoiceDetail
        obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
        For i As Integer = 0 To gvItem.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvItem.Rows(i).Cells(colVendorCode).Value), strVendorCode) = CompairStringResult.Equal Then
                If isTradeSRN = clsCommon.myCBool(gvItem.Rows(i).Cells(colSRNTrade).Value) Then
                    objDetail = New clsMilkPurchaseInvoiceDetail
                    objDetail.DOC_NO = ""
                    objDetail.SL_NO = intCounter
                    objDetail.SRN_NO = clsCommon.myCstr(gvItem.Rows(i).Cells(colSRNNo).Value)
                    objDetail.SRN_Date = clsCommon.GetPrintDate(gvItem.Rows(i).Cells(colSRNDATe).Value, "dd/MMM/yyyy hh:mm:ss tt")
                    objDetail.Item_Code = clsCommon.myCstr(gvItem.Rows(i).Cells(colItemCode).Value)
                    objDetail.Item_Desc = clsCommon.myCstr(gvItem.Rows(i).Cells(colItemDesc).Value)
                    objDetail.UOM = clsCommon.myCstr(gvItem.Rows(i).Cells(colUOM).Value)
                    objDetail.Gross_Weight = clsCommon.myCdbl(gvItem.Rows(i).Cells(colGrossWeight).Value)
                    objDetail.Tare_Weight = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTareWeight).Value)
                    objDetail.Net_Weight = clsCommon.myCdbl(gvItem.Rows(i).Cells(colNetWeight).Value)
                    objDetail.Invoice_Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value)
                    objDetail.fat_per = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFat).Value)
                    objDetail.fat_KG = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatKG).Value)
                    objDetail.fat_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatRate).Value)
                    objDetail.snf_Per = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNF).Value)
                    objDetail.SNF_KG = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFKG).Value)
                    objDetail.SNF_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFRate).Value)
                    objDetail.Amount = clsCommon.myCdbl(gvItem.Rows(i).Cells(colAmt).Value)
                    objDetail.Deduction = clsCommon.myCdbl(gvItem.Rows(i).Cells(colDeduc).Value)
                    objDetail.Incentive = clsCommon.myCdbl(gvItem.Rows(i).Cells(colIncen).Value)
                    objDetail.Special_Deduction = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSpecialDeduc).Value)
                    objDetail.Actual_Amount = clsCommon.myCdbl(gvItem.Rows(i).Cells(colActAmt).Value)
                    objDetail.price_code = clsCommon.myCstr(gvItem.Rows(i).Cells(colPriceCode).Value)
                    objDetail.NetRate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colNetRate).Value)
                    objDetail.CHAMBER_DESC = clsCommon.myCstr(gvItem.Rows(i).Cells(colChamberDesc).Value)


                    objDetail.TAX1 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax1).Value)
                    objDetail.TAX1_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt1).Value)
                    objDetail.TAX1_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate1).Value)
                    objDetail.TAX1_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt1).Value)
                    objDetail.TAX2 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax2).Value)
                    objDetail.TAX2_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt2).Value)
                    objDetail.TAX2_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate2).Value)
                    objDetail.TAX2_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt2).Value)
                    objDetail.TAX3 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax3).Value)
                    objDetail.TAX3_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt3).Value)
                    objDetail.TAX3_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate3).Value)
                    objDetail.TAX3_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt3).Value)
                    objDetail.TAX4 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax4).Value)
                    objDetail.TAX4_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt4).Value)
                    objDetail.TAX4_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate4).Value)
                    objDetail.TAX4_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt4).Value)
                    objDetail.TAX5 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax5).Value)
                    objDetail.TAX5_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt5).Value)
                    objDetail.TAX5_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate5).Value)
                    objDetail.TAX5_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt5).Value)
                    objDetail.TAX6 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax6).Value)
                    objDetail.TAX6_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt6).Value)
                    objDetail.TAX6_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate6).Value)
                    objDetail.TAX6_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt6).Value)
                    objDetail.TAX7 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax7).Value)
                    objDetail.TAX7_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt7).Value)
                    objDetail.TAX7_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate7).Value)
                    objDetail.TAX7_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt7).Value)
                    objDetail.TAX8 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax8).Value)
                    objDetail.TAX8_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt8).Value)
                    objDetail.TAX8_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate8).Value)
                    objDetail.TAX8_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt8).Value)
                    objDetail.TAX9 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax9).Value)
                    objDetail.TAX9_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt9).Value)
                    objDetail.TAX9_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate9).Value)
                    objDetail.TAX9_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt9).Value)
                    objDetail.TAX10 = clsCommon.myCstr(gvItem.Rows(i).Cells(colTax10).Value)
                    objDetail.TAX10_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxBaseAmt10).Value)
                    objDetail.TAX10_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxRate10).Value)
                    objDetail.TAX10_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTaxAmt10).Value)
                    objDetail.Total_Tax_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTotTaxAmt).Value)
                    objDetail.Item_Net_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(colItemNetAmt).Value)

                    obj.arrDetail.Add(objDetail)

                    obj.Total_FAT_KG += clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatKG).Value)
                    obj.Total_SNF_KG += clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFKG).Value)
                    obj.Total_QTY += clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value)

                    obj.Total_Taxable_Amount += clsCommon.myCdbl(gvItem.Rows(i).Cells(colActAmt).Value)
                    obj.Total_Tax_Amt += clsCommon.myCdbl(gvItem.Rows(i).Cells(colTotTaxAmt).Value)
                    obj.Total_AMT += clsCommon.myCdbl(gvItem.Rows(i).Cells(colItemNetAmt).Value)

                    obj.TAX1_Base_Amt += objDetail.TAX1_Base_Amt
                    obj.TAX1_Amt += objDetail.TAX1_Amt
                    obj.TAX2_Base_Amt += objDetail.TAX2_Base_Amt
                    obj.TAX2_Amt += objDetail.TAX2_Amt
                    obj.TAX3_Base_Amt += objDetail.TAX3_Base_Amt
                    obj.TAX3_Amt += objDetail.TAX3_Amt
                    obj.TAX4_Base_Amt += objDetail.TAX4_Base_Amt
                    obj.TAX4_Amt += objDetail.TAX4_Amt
                    obj.TAX5_Base_Amt += objDetail.TAX5_Base_Amt
                    obj.TAX5_Amt += objDetail.TAX5_Amt
                    obj.TAX6_Base_Amt += objDetail.TAX6_Base_Amt
                    obj.TAX6_Amt += objDetail.TAX6_Amt
                    obj.TAX7_Base_Amt += objDetail.TAX7_Base_Amt
                    obj.TAX7_Amt += objDetail.TAX7_Amt
                    obj.TAX8_Base_Amt += objDetail.TAX8_Base_Amt
                    obj.TAX8_Amt += objDetail.TAX8_Amt
                    obj.TAX9_Base_Amt += objDetail.TAX9_Base_Amt
                    obj.TAX9_Amt += objDetail.TAX9_Amt
                    obj.TAX10_Base_Amt += objDetail.TAX10_Base_Amt
                    obj.TAX10_Amt += objDetail.TAX10_Amt
                End If
            End If
        Next



        obj.Total_FAT_KG = Math.Round(obj.Total_FAT_KG, 3)
        obj.Total_SNF_KG = Math.Round(obj.Total_SNF_KG, 3)
        obj.Total_QTY = Math.Round(obj.Total_QTY, 2)

        Dim intPart As Double = Math.Round(obj.Total_AMT, 0, MidpointRounding.AwayFromZero)
        Dim roundOffAmt As Double = -(obj.Total_AMT - intPart)
        obj.Total_Tax_Amt = Math.Round(obj.Total_Tax_Amt, 2)
        obj.Total_Taxable_Amount = Math.Round(obj.Total_Taxable_Amount, 2)
        obj.Total_AMT = Math.Round(intPart, 2)
        obj.RoundOffAmount = Math.Round(roundOffAmt, 2)

        Dim dblPreviousTDSAmt As Decimal = 0
        Dim objRemittance As clsRemittance = clsVSPBillAndIncentiveCalculation.SetVendorTDSDetails((clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal), obj.DOC_NO, obj.DOC_DATE, dblPreviousTDSAmt, obj.vendor_code, obj.Total_Taxable_Amount, obj.Total_AMT, trans)
        'clsVSPBillAndIncentiveCalculation.UpdateTDSAmount(objRemittance, obj.DOC_NO, obj.DOC_DATE, dblPreviousTDSAmt, obj.vendor_code, obj.Total_Taxable_Amount, obj.Total_AMT, trans)
        obj.objPIRemittance = clsPIRemittance.Convert(objRemittance, dblPreviousTDSAmt)

        obj.isNewEntry = True
        clsMilkPurchaseInvoiceHead.saveData(obj, trans)
        If chkAutoPost.Checked Then
            clsMilkPurchaseInvoiceHead.postData(obj.DOC_NO, clsUserMgtCode.frmBulkMilkPurchaseInvoice, trans)
        End If
        Return True
    End Function

    'Function SetVendorTDSDetails(ByVal DocNo As String, ByVal docDate As DateTime, ByRef dblPreviousTDSAmt As Decimal, ByVal VendorCode As String, ByVal TaxableAmt As Decimal, ByVal TotalAmt As Decimal, ByVal trans As SqlTransaction) As clsRemittance
    '    Dim objRemittance As clsRemittance = Nothing
    '    Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(VendorCode, trans)
    '    If objVendor IsNot Nothing Then
    '        Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + VendorCode + "'", trans)
    '        Dim appAmt As Double = 0
    '        If (clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal) Then
    '            appAmt = clsCommon.myCdbl(TaxableAmt)
    '        Else
    '            appAmt = clsCommon.myCdbl(TotalAmt)
    '        End If
    '        Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, appAmt, trans)
    '        If (objDedDetails IsNot Nothing) Then
    '            Dim isApplyTDS As Boolean = False
    '            Dim qry As String = "select Fiscal_Code,Start_Date,End_Date from TSPL_Fiscal_Year_Master where convert(date,'" + docDate + "',103)>=  convert(date,Start_Date,103)  and convert(date,'" + docDate + "',103)<=convert(date,End_Date,103) "
    '            Dim dtFY As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dtFY Is Nothing OrElse dtFY.Rows.Count <= 0 Then
    '                Throw New Exception("Please make fiscal year where document date exists")
    '            End If

    '            ''Check if any TDS entry found in Document Fiscal Year
    '            qry = "select top 1 Remittance_Code from TSPL_REMITTANCE  where Vendor_Code='" + VendorCode + "' and convert(date, Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and Document_No not in ('" + DocNo + "')"
    '            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
    '                isApplyTDS = True
    '            Else
    '                qry = "select Cumm_Cutoff,Cumm_Cutoff_Document from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + objVendor.Nature_Of_Deduction + "'"
    '                dtTemp = clsDBFuncationality.GetDataTable(qry, trans)
    '                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
    '                    If clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) <= 0 AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) <= 0 Then
    '                        isApplyTDS = True
    '                    Else
    '                        qry = "select sum( " + IIf(clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal, "TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount", "TSPL_VENDOR_INVOICE_HEAD.Document_Total") + ") as Document_Total from TSPL_VENDOR_INVOICE_HEAD where Vendor_Code='" + VendorCode + "' and Document_Type in ('I','C') and Document_No not in ('" + DocNo + "') and  convert(date, Invoice_Entry_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Invoice_Entry_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null "
    '                        dblPreviousTDSAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    '                        If appAmt >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) > 0 Then
    '                            isApplyTDS = True
    '                        ElseIf (dblPreviousTDSAmt + appAmt) >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) > 0 Then
    '                            isApplyTDS = True
    '                        End If
    '                    End If
    '                End If
    '            End If

    '            If isApplyTDS Then
    '                objRemittance = New clsRemittance()
    '                objRemittance.Branch_Code = objVendor.Branch_Code
    '                objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
    '                objRemittance.TDS_Per = objDedDetails.TDS
    '                objRemittance.Surcharge_Per = objDedDetails.Surcharge
    '                objRemittance.Edu_Cess_Per = objDedDetails.Educess
    '                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
    '                objRemittance.IsTDSOverride = False

    '                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
    '                    objRemittance.IsApplyTDS = True
    '                Else
    '                    objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(DocNo, trans)
    '                End If
    '                objRemittance.Section_Code = objVendor.TDSSection
    '                objRemittance.Section_Description = objVendor.TDSSectionDescription
    '                objRemittance.Select_By = objVendor.VendorTypeCode
    '                'objRemittance.Include_Tax = objVendor.Include_Tax

    '                objRemittance.Fiscal_Year = clsCommon.myCstr(dtFY.Rows(0)("Fiscal_Code"))
    '                objRemittance.Quarter = "First"
    '            End If
    '        End If
    '    End If
    '    Return objRemittance
    'End Function
    'Sub UpdateTDSAmount(ByRef objRemittance As clsRemittance, ByVal DocNo As String, ByVal docDate As DateTime, ByRef dblPreviousTDSAmt As Decimal, ByVal VendorCode As String, ByVal TaxableAmt As Decimal, ByVal TotalAmt As Decimal, ByVal trans As SqlTransaction)

    '    If (objRemittance IsNot Nothing) Then
    '        Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + VendorCode + "'", trans)
    '        Dim applicableAmt As Double = 0
    '        If clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal Then
    '            applicableAmt = TaxableAmt
    '        Else
    '            applicableAmt = TotalAmt
    '        End If
    '        applicableAmt += dblPreviousTDSAmt


    '        Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, applicableAmt, trans)
    '        If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
    '            objRemittance.TDS_Per = objDedDetails.TDS
    '            objRemittance.Surcharge_Per = objDedDetails.Surcharge
    '            objRemittance.Edu_Cess_Per = objDedDetails.Educess
    '            objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
    '        End If

    '        objRemittance.Vendor_Code = VendorCode
    '        objRemittance.Vendor_Name = lblVendorName.Text
    '        objRemittance.Document_Date = docDate
    '        objRemittance.Document_Type = "I"
    '        objRemittance.Document_Amount = TotalAmt
    '        objRemittance.Calculated_TDS_Base = applicableAmt
    '        If Not objRemittance.IsTDSOverride Then
    '            objRemittance.Actual_TDS_Base = applicableAmt
    '        End If

    '        objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
    '        objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

    '        objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
    '        objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

    '        objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
    '        objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

    '        objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
    '        objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

    '        objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
    '        objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
    '    End If
    'End Sub

    Function allowToSave() As Boolean
        If AllowFutureDateTransaction(txtInvoiceDate.Value, Nothing) = False Then
            txtInvoiceDate.Focus()
            Return False
        End If
        If txtPaymentCycle.Enabled Then
            RadButton1.Focus()
            Throw New Exception("Please first click refresh button")
        End If
        If gvItem.Rows.Count <= 0 Then
            Throw New Exception("No data found to create invoice")
        End If
        Return True
    End Function


    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendor.arrValueMember(0), txtLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
            Next
            SetitemWiseTaxSetting(True, False)
        Else
            lblTaxGrpName.Text = ""
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                BlankTaxDetails(ii)
            Next
        End If

        For ii As Integer = 0 To gvItem.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        'UpdateAllTotals()
    End Sub

    Sub LoadBlankGridTax()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.IsVisible = False
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.IsVisible = False
        repoTaxRate.ReadOnly = False
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = False
        repoTaxAmt.IsVisible = False
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)

        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False

    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblCurrentTaxableAmount As Decimal = clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(colActAmt).Value)
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
            If clsCommon.myLen(strTaxCode) > 0 Then
                Dim dblTaxRate As Double = clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                Dim IsSurTax As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                Dim strSurTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                Dim IsTaxOnBaseAmt As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value)
                Dim dblBaseAmt As Double = 0
                Dim dblTaxAmt As Double = 0
                If IsTaxOnBaseAmt Then
                    dblBaseAmt = dblCurrentTaxableAmount
                ElseIf IsSurTax Then
                    Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                    dblBaseAmt = dblSurTaxAmt
                Else
                    Dim dblOtherTaxAmt As Double = 0
                    dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                    dblBaseAmt = (dblCurrentTaxableAmount + dblOtherTaxAmt)
                End If
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
                If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                    arrTaxableAuth.Add(strTaxCode.ToUpper())
                End If
            Else
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value = Nothing
            End If
        Next
        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblCurrentTaxableAmount + dblTotTaxAmt
        gvItem.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gvItem.Rows(IntRowNo).Cells(colItemNetAmt).Value = Math.Round(dblAmtAfterTax, 2)
    End Sub
    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function
    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function
    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gvItem.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gvItem.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gvItem.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gvItem.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gvItem.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
                gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
            Else
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
            End If
        Next
    End Sub
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendor.arrValueMember(0), txtLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gvItem.CurrentRow.Index)
                If clsCommon.myLen(gvItem.CurrentRow.Cells(colItemCode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                        End If
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gvItem.Rows.Count - 1
                    BlankTaxDetails(intRowNo)
                    If clsCommon.myLen(gvItem.Rows(intRowNo).Cells(colItemCode)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub SetTax()
        txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtLocation.Value, txtVendor.arrValueMember(0), "P", txtInvoiceDate.Value)
        lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
        SetTaxDetails()
    End Sub

    Private Sub txtTaxGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTaxGroup._MYValidating
        Try
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtLocation.Value, txtVendor.arrValueMember(0), "P", txtTaxGroup.Value, isButtonClicked)
            SetTaxDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(txtLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtVendor.arrValueMember(0), "P")
            Dim intRowNo As Integer = gv2.CurrentRow.Index
            If gvItem.RowCount > 0 AndAlso intRowNo >= 0 Then
                Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    gvItem.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                Next
                gv2.CurrentRow.Cells(colTTaxRate).Value = dblNewRate
            End If
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvItem_DoubleClick(sender As Object, e As EventArgs) Handles gvItem.DoubleClick
        Try
            If gvItem.CurrentColumn Is gvItem.Columns(colTotTaxAmt) Then
                'If Not PurchaseModulePickFixTaxRate OrElse Not clsCommon.myCBool(gvItem.CurrentRow.Cells(colItemTaxable).Value) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = 1
                frm.strItemCode = clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value)
                frm.strItemName = clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemDesc).Value)
                frm.dblTotTax = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colActAmt).Value)
                ''New Column for location wise
                frm.strTaxGroup = txtTaxGroup.Value
                frm.strTransLocation = txtLocation.Value
                frm.strTaxType = "P"
                frm.strVendorCustomerCode = txtVendor.arrValueMember(0)
                ''End of New Column for location wise

                frm.PurchaseModulePickFixTaxRate = False
                frm.IsTaxableItem = clsItemMaster.IsTaxableItem(frm.strItemCode, Nothing)

                If clsCommon.myLen(frm.strItemCode) > 0 Then
                    frm.ArrIn = New List(Of clsTempItemTaxDetails)
                    For ii As Integer = 1 To 10
                        Dim strii As String = clsCommon.myCstr(ii)
                        Dim obj As New clsTempItemTaxDetails()
                        obj.AuthorityCode = clsCommon.myCstr(gvItem.CurrentRow.Cells("COLTAX" + strii).Value)
                        If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                            obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                            obj.Rate = clsCommon.myCdbl(gvItem.CurrentRow.Cells("COLTAXRATE" + strii).Value)
                            obj.BaseAmt = clsCommon.myCdbl(gvItem.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
                            obj.TaxAmt = clsCommon.myCdbl(gvItem.CurrentRow.Cells("COLTAXAMT" + strii).Value)
                            obj.isSurTax = clsCommon.myCBool(gvItem.CurrentRow.Cells("ISSURTAX" + strii).Value)
                            obj.SurTax = clsCommon.myCstr(gvItem.CurrentRow.Cells("SURTAXCODE" + strii).Value)
                            obj.IsTaxable = clsCommon.myCBool(gvItem.CurrentRow.Cells("ISTAXABLE" + strii).Value)
                            frm.ArrIn.Add(obj)
                        End If
                    Next

                    frm.ShowDialog()
                    If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                        BlankTaxDetails(gvItem.CurrentRow.Index)
                        For ii As Integer = 0 To frm.ArrOut.Count - 1
                            Dim strii As String = clsCommon.myCstr(ii + 1)
                            gvItem.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
                            gvItem.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
                            gvItem.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
                            gvItem.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
                            gvItem.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                            gvItem.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                            gvItem.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                        Next
                        gvItem.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                        UpdateCurrentRow(gvItem.CurrentRow.Index)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class

Class TempSRNTrade
    Public SRNTade As Boolean
    Public Normal As Boolean
End Class
