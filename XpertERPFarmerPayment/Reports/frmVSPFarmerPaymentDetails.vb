
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmVSPFarmerPaymentDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Const ReportID As String = "FarmerLedgerReport"
    Dim arrBack As New List(Of String)
    Dim arrVSP As New ArrayList()
    Dim arrFarmer As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region

    Sub LoadData()
        Try
            Dim qry As String = "select  TSPL_Location_MASTER.Location_Code,TSPL_Location_MASTER.Location_Desc, tspl_Payment_Process_HEad.Doc_Date, tspl_Payment_Process_detail.PP_Detail_No,tspl_Payment_Process_detail.Doc_No,tspl_Payment_Process_detail.Is_select,tspl_Payment_Process_detail.SNo,tspl_Payment_Process_detail.Milk_Purchase_Invoice_No,tspl_Payment_Process_detail.Milk_Purchase_Invoice_Date,tspl_Payment_Process_detail.AP_Invoice_No,tspl_Payment_Process_detail.AP_Invoice_Date,tspl_Payment_Process_detail.VLC_CODE_Uploader,tspl_Payment_Process_detail.VLC_Name,tspl_Payment_Process_detail.VSP_CODE,tspl_Payment_Process_detail.VSP_Name,tspl_Payment_Process_detail.Main_VSP_CODE,tspl_Payment_Process_detail.Main_VSP_NAME,tspl_Payment_Process_detail.Payee_Joint_Name,tspl_Payment_Process_detail.Payee_Joint_Bank_Code,tspl_Payment_Process_detail.Payee_Joint_Bank_Name,tspl_Payment_Process_detail.Payee_Joint_Branch_Code,tspl_Payment_Process_detail.Payee_Joint_Branch_Name,tspl_Payment_Process_detail.Payee_Joint_Account_No,tspl_Payment_Process_detail.Payee_Joint_IFSC_Code,tspl_Payment_Process_detail.Bank_Code,tspl_Payment_Process_detail.Bank_Desc,tspl_Payment_Process_detail.Payment_Mode,tspl_Payment_Process_detail.Cheque_No, tspl_Payment_Process_detail.Cheque_Dated,  tspl_Payment_Process_detail.Milk_Qty,tspl_Payment_Process_detail.Milk_Amount,tspl_Payment_Process_detail.Incentive_Amount" + Environment.NewLine +
            ",tspl_Payment_Process_detail.Incentive_EMP_Amount,tspl_Payment_Process_detail.Total_EMP_Amount,tspl_Payment_Process_detail.Total,tspl_Payment_Process_detail.Total_Invoice_Amount,tspl_Payment_Process_detail.Vsp_Own_System_Amount,tspl_Payment_Process_detail.Head_Load_Amount,tspl_Payment_Process_detail.Invoice_Deduction_Amount,tspl_Payment_Process_detail.Reduce_Deduc_Amt,tspl_Payment_Process_detail.MCC_Sale_Amount,tspl_Payment_Process_detail.MCC_Sale_Return_Amount,tspl_Payment_Process_detail.Item_Issue_Amount,tspl_Payment_Process_detail.Item_Issue_Return_Amount,tspl_Payment_Process_detail.Deduction_Amount,tspl_Payment_Process_detail.Credit_Note_Amount,tspl_Payment_Process_detail.Payable_Amount,tspl_Payment_Process_detail.VSP_Amount,tspl_Payment_Process_detail.EMP_Amount,tspl_Payment_Process_detail.Service_Charge_Amt,tspl_Payment_Process_detail.Advance_Payment_Amount,tspl_Payment_Process_detail.Advance_Payment_Amount_Knock_Off,(case when abs(tspl_Payment_Process_detail.Milk_Amount-tspl_Payment_Process_detail.FarmerPayment)<0.11 then 0 else convert(decimal(18,2),tspl_Payment_Process_detail.FarmerPayment-tspl_Payment_Process_detail.Milk_Amount,2) end) as MP_VSP_Diff_Amount,tspl_Payment_Process_detail.is_Hold_Payment_Process" + Environment.NewLine +
            ",tspl_Payment_Process_detail.NextCycleDebitNote,tspl_Payment_Process_detail.Handling_Charges_Amount,tspl_Payment_Process_detail.SRN_Net_Amount,tspl_Payment_Process_detail.SRN_RO_Amount,tspl_Payment_Process_detail.PrevCycleDebitNote,tspl_Payment_Process_detail.VSP_Excess_Amount,tspl_Payment_Process_detail.MP_EMP,tspl_Payment_Process_detail.FarmerPayment," + Environment.NewLine +
            "tspl_Payment_Process_detail.MP_IncentiveEMP,TSPL_MP_PAY_PROCESS_DETAIL.FarmerMilkQty,TSPL_MP_PAY_PROCESS_DETAIL.MP_Amount,TSPL_MP_PAY_PROCESS_DETAIL.MP_Net_Amount,TSPL_MP_PAY_PROCESS_DETAIL.FarmerSaleAmount,TSPL_MP_PAY_PROCESS_DETAIL.FarmerSaleReturnAmount,TSPL_MP_PAY_PROCESS_DETAIL.FarmerPayableAmount,TSPL_MP_PAY_PROCESS_DETAIL.FarmerAdjustmentAmount,TSPL_MP_PAY_PROCESS_DETAIL.NextCycleDebitNoteMP,TSPL_MP_PAY_PROCESS_DETAIL.PrevCycleDebitNoteMP,TSPL_MP_PAY_PROCESS_DETAIL.MP_Incentive,TSPL_MP_PAY_PROCESS_DETAIL.MP_Total_Deduction,TSPL_MP_PAY_PROCESS_DETAIL.Total_Advance_Amount as MP_Total_Advance_Amount from tspl_Payment_Process_detail " + Environment.NewLine +
            "left outer join tspl_Payment_Process_HEad on tspl_Payment_Process_HEad.Doc_No=tspl_Payment_Process_detail.Doc_No" + Environment.NewLine +
            "left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Loc_Segment_Code=tspl_Payment_Process_HEad.Loc_Seg_Code" + Environment.NewLine +
            "left outer join (select Doc_No,VSP_CODE,sum(Milk_Qty) as FarmerMilkQty,sum(Milk_Amount) as MP_Amount,sum(Total_Invoice_Amount) as MP_Net_Amount,sum(MCC_Sale_Amount) as FarmerSaleAmount,sum(MCC_Sale_Return_Amount) as FarmerSaleReturnAmount,sum(Payable_Amount) as FarmerPayableAmount,sum(MP_Adjust_Amount) as FarmerAdjustmentAmount,sum(NextCycleDebitNoteMP) as NextCycleDebitNoteMP,sum(PrevCycleDebitNoteMP) as PrevCycleDebitNoteMP,sum(Incentive_Amount) as MP_Incentive,sum(Deduction_Amount) as MP_Total_Deduction,sum(Total_Advance_Amount) as Total_Advance_Amount" + Environment.NewLine +
            "from TSPL_MP_PAY_PROCESS_DETAIL  group by Doc_No,VSP_CODE)TSPL_MP_PAY_PROCESS_DETAIL  on TSPL_MP_PAY_PROCESS_DETAIL .Doc_No=tspl_Payment_Process_detail.Doc_No and TSPL_MP_PAY_PROCESS_DETAIL.VSP_CODE=tspl_Payment_Process_detail.VSP_CODE" + Environment.NewLine +
            "where  tspl_Payment_Process_HEad.FarmType='PPF' and  convert(date, tspl_Payment_Process_HEad.Doc_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") + "' and convert(date, tspl_Payment_Process_HEad.Doc_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") + "'" + Environment.NewLine
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_Location_MASTER.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv3.DataSource = Nothing
            gv3.Rows.Clear()
            gv3.Columns.Clear()
            gv3.DataSource = dt2
            gv3.GroupDescriptors.Clear()
            gv3.MasterTemplate.BestFitColumns()
            gv3.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2

            gv3.ReadOnly = True
            btnGenrate.Enabled = True
            SetGridLayout()
            PageSetupReport_ID = MyBase.Form_ID
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            btnGenrate.Enabled = True
        End Try
    End Sub
    Sub SetGridLayout()


        gv3.Columns("Location_Code").Width = 100
        gv3.Columns("Location_Code").HeaderText = "MCC"

        gv3.Columns("Location_Desc").Width = 100
        gv3.Columns("Location_Desc").HeaderText = "MCC Name"

        gv3.Columns("Doc_Date").Width = 100
        gv3.Columns("Doc_Date").HeaderText = "Payment Process Date"

        gv3.Columns("PP_Detail_No").IsVisible = False

        gv3.Columns("Doc_No").Width = 100
        gv3.Columns("Doc_No").HeaderText = "Payment Process No"

        gv3.Columns("Is_select").IsVisible = False
        gv3.Columns("SNo").IsVisible = False

        gv3.Columns("Milk_Purchase_Invoice_No").Width = 100
        gv3.Columns("Milk_Purchase_Invoice_No").HeaderText = "Milk Purchase Invoice"

        gv3.Columns("Milk_Purchase_Invoice_Date").Width = 100
        gv3.Columns("Milk_Purchase_Invoice_Date").HeaderText = "Milk Purchase Invoice Date"

        gv3.Columns("AP_Invoice_No").Width = 100
        gv3.Columns("AP_Invoice_No").HeaderText = "AP Invoice"

        gv3.Columns("AP_Invoice_Date").Width = 100
        gv3.Columns("AP_Invoice_Date").HeaderText = "AP Invoice Date"

        gv3.Columns("VLC_CODE_Uploader").Width = 100
        gv3.Columns("VLC_CODE_Uploader").HeaderText = "Uploader No"

        gv3.Columns("VLC_Name").Width = 100
        gv3.Columns("VLC_Name").HeaderText = "VLC Name"

        gv3.Columns("VSP_CODE").Width = 100
        gv3.Columns("VSP_CODE").HeaderText = "VSP Code"

        gv3.Columns("VSP_Name").Width = 100
        gv3.Columns("VSP_Name").HeaderText = "VSP Name"

        gv3.Columns("Main_VSP_CODE").Width = 100
        gv3.Columns("Main_VSP_CODE").HeaderText = "Main VSP"

        gv3.Columns("Main_VSP_NAME").Width = 100
        gv3.Columns("Main_VSP_NAME").HeaderText = "Main VSP Name"

        gv3.Columns("Payee_Joint_Name").Width = 100
        gv3.Columns("Payee_Joint_Name").HeaderText = "Payee Joint Name"

        gv3.Columns("Payee_Joint_Bank_Code").Width = 100
        gv3.Columns("Payee_Joint_Bank_Code").HeaderText = "Payee Joint Bank Code"

        gv3.Columns("Payee_Joint_Bank_Name").Width = 100
        gv3.Columns("Payee_Joint_Bank_Name").HeaderText = "Payee Joint Bank Name"

        gv3.Columns("Payee_Joint_Branch_Code").Width = 100
        gv3.Columns("Payee_Joint_Branch_Code").HeaderText = "Payee Joint Branch Code"

        gv3.Columns("Payee_Joint_Branch_Name").Width = 100
        gv3.Columns("Payee_Joint_Branch_Name").HeaderText = "Payee Joint Branch Name"

        gv3.Columns("Payee_Joint_Account_No").Width = 100
        gv3.Columns("Payee_Joint_Account_No").HeaderText = "Payee Joint Account No"

        gv3.Columns("Payee_Joint_IFSC_Code").Width = 100
        gv3.Columns("Payee_Joint_IFSC_Code").HeaderText = "Payee Joint IFSC Code"

        gv3.Columns("Bank_Code").Width = 100
        gv3.Columns("Bank_Code").HeaderText = "Bank Code"

        gv3.Columns("Bank_Desc").Width = 100
        gv3.Columns("Bank_Desc").HeaderText = "Bank Name"

        gv3.Columns("Payment_Mode").Width = 100
        gv3.Columns("Payment_Mode").HeaderText = "Payment Mode"

        gv3.Columns("Cheque_No").Width = 100
        gv3.Columns("Cheque_No").HeaderText = "Cheque No"

        gv3.Columns("Cheque_Dated").Width = 100
        gv3.Columns("Cheque_Dated").HeaderText = "Document Date"

        gv3.Columns("Milk_Qty").Width = 100
        gv3.Columns("Milk_Qty").HeaderText = "Milk Qty"

        gv3.Columns("Milk_Amount").Width = 100
        gv3.Columns("Milk_Amount").HeaderText = "Milk Amount"

        gv3.Columns("Incentive_Amount").Width = 100
        gv3.Columns("Incentive_Amount").HeaderText = "Incentive Amount"

        gv3.Columns("EMP_Amount").Width = 100
        gv3.Columns("EMP_Amount").HeaderText = "EMP Amount"

        gv3.Columns("Incentive_EMP_Amount").Width = 100
        gv3.Columns("Incentive_EMP_Amount").HeaderText = "Incentive EMP Amount"

        gv3.Columns("Total_EMP_Amount").Width = 100
        gv3.Columns("Total_EMP_Amount").HeaderText = "Total EMP Amount"

        gv3.Columns("Total").Width = 100
        gv3.Columns("Total").HeaderText = "Total"

        gv3.Columns("Total_Invoice_Amount").Width = 100
        gv3.Columns("Total_Invoice_Amount").HeaderText = "Total Invoice Amount"

        gv3.Columns("Vsp_Own_System_Amount").Width = 100
        gv3.Columns("Vsp_Own_System_Amount").HeaderText = "Vsp Own System Amount"

        gv3.Columns("Head_Load_Amount").Width = 100
        gv3.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"

        gv3.Columns("Invoice_Deduction_Amount").Width = 100
        gv3.Columns("Invoice_Deduction_Amount").HeaderText = "Invoice Deduction Amount"

        gv3.Columns("Reduce_Deduc_Amt").Width = 100
        gv3.Columns("Reduce_Deduc_Amt").HeaderText = "Reduce Deduc Amt"

        gv3.Columns("MCC_Sale_Amount").Width = 100
        gv3.Columns("MCC_Sale_Amount").HeaderText = "MCC Sale Amount"

        gv3.Columns("MCC_Sale_Return_Amount").Width = 100
        gv3.Columns("MCC_Sale_Return_Amount").HeaderText = "MCC Sale Return Amount"

        gv3.Columns("Item_Issue_Amount").Width = 100
        gv3.Columns("Item_Issue_Amount").HeaderText = "Item Issue Amount"

        gv3.Columns("Item_Issue_Return_Amount").Width = 100
        gv3.Columns("Item_Issue_Return_Amount").HeaderText = "Item Issue Return Amount"

        gv3.Columns("Deduction_Amount").Width = 100
        gv3.Columns("Deduction_Amount").HeaderText = "Deduction Amount"

        gv3.Columns("Credit_Note_Amount").Width = 100
        gv3.Columns("Credit_Note_Amount").HeaderText = "Credit Note Amount"

        gv3.Columns("Payable_Amount").Width = 100
        gv3.Columns("Payable_Amount").HeaderText = "Payable Amount"

        gv3.Columns("VSP_Amount").Width = 100
        gv3.Columns("VSP_Amount").HeaderText = "VSP Amount"

        gv3.Columns("MP_Amount").Width = 100
        gv3.Columns("MP_Amount").HeaderText = "MP Amount"



        gv3.Columns("Service_Charge_Amt").Width = 100
        gv3.Columns("Service_Charge_Amt").HeaderText = "Service Charge Amt"

        gv3.Columns("Advance_Payment_Amount").Width = 100
        gv3.Columns("Advance_Payment_Amount").HeaderText = "Advance Payment Amount"

        gv3.Columns("Advance_Payment_Amount_Knock_Off").Width = 100
        gv3.Columns("Advance_Payment_Amount_Knock_Off").HeaderText = "Advance Payment Amount Knock Off"

        gv3.Columns("MP_VSP_Diff_Amount").Width = 100
        gv3.Columns("MP_VSP_Diff_Amount").HeaderText = "MP VSP Diff Amount"

        gv3.Columns("is_Hold_Payment_Process").IsVisible = False
        gv3.Columns("is_Hold_Payment_Process").HeaderText = "Hold"

        gv3.Columns("MP_EMP").Width = 100
        gv3.Columns("MP_EMP").HeaderText = "MP EMP"

        gv3.Columns("MP_Incentive").Width = 100
        gv3.Columns("MP_Incentive").HeaderText = "MP Incentive"

        gv3.Columns("MP_IncentiveEMP").Width = 100
        gv3.Columns("MP_IncentiveEMP").HeaderText = "MP Incentive EMP"

        gv3.Columns("MP_Net_Amount").Width = 100
        gv3.Columns("MP_Net_Amount").HeaderText = "MP Net Amount"

        gv3.Columns("NextCycleDebitNote").Width = 100
        gv3.Columns("NextCycleDebitNote").HeaderText = "Next Cycle Debit Note"

        gv3.Columns("FarmerPayment").Width = 100
        gv3.Columns("FarmerPayment").HeaderText = "Farmer Payment"

        gv3.Columns("FarmerMilkQty").Width = 100
        gv3.Columns("FarmerMilkQty").HeaderText = "Farmer Milk Qty"

        gv3.Columns("FarmerSaleAmount").Width = 100
        gv3.Columns("FarmerSaleAmount").HeaderText = "Farmer Sale Amount"

        gv3.Columns("FarmerSaleReturnAmount").Width = 100
        gv3.Columns("FarmerSaleReturnAmount").HeaderText = "Farmer Sale Return Amount"

        gv3.Columns("FarmerAdjustmentAmount").Width = 100
        gv3.Columns("FarmerAdjustmentAmount").HeaderText = "Farmer Adjustment Amount"

        gv3.Columns("FarmerPayableAmount").Width = 100
        gv3.Columns("FarmerPayableAmount").HeaderText = "Farmer Payable Amount"

        gv3.Columns("Handling_Charges_Amount").Width = 100
        gv3.Columns("Handling_Charges_Amount").HeaderText = "Handling Charges Amount"

        gv3.Columns("SRN_Net_Amount").Width = 100
        gv3.Columns("SRN_Net_Amount").HeaderText = "SRN Net Amount"

        gv3.Columns("SRN_RO_Amount").Width = 100
        gv3.Columns("SRN_RO_Amount").HeaderText = "SRN Round off Amount"

        gv3.Columns("PrevCycleDebitNote").Width = 100
        gv3.Columns("PrevCycleDebitNote").HeaderText = "Prev Cycle Debit Note"

        gv3.Columns("PrevCycleDebitNoteMP").Width = 100
        gv3.Columns("PrevCycleDebitNoteMP").HeaderText = "Prev Cycle Debit Note MP"

        gv3.Columns("NextCycleDebitNoteMP").Width = 100
        gv3.Columns("NextCycleDebitNoteMP").HeaderText = "Next Cycle Debit Note MP"

        gv3.Columns("VSP_Excess_Amount").Width = 100
        gv3.Columns("VSP_Excess_Amount").HeaderText = "VSP Excess Amount"

        gv3.Columns("MP_Total_Deduction").Width = 100
        gv3.Columns("MP_Total_Deduction").HeaderText = "MP Total Deduction"

        gv3.Columns("MP_Total_Advance_Amount").Width = 100
        gv3.Columns("MP_Total_Advance_Amount").HeaderText = "MP Advance Amount"

        gv3.SummaryRowsBottom.Clear()


        Dim summaryRowItem As New GridViewSummaryRowItem()








        Dim item1 As New GridViewSummaryItem("Milk_Qty", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Milk_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Incentive_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("EMP_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Incentive_EMP_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Total_EMP_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Total", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Total_Invoice_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Vsp_Own_System_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Head_Load_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Invoice_Deduction_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Reduce_Deduc_Amt", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MCC_Sale_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MCC_Sale_Return_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        item1 = New GridViewSummaryItem("Item_Issue_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Item_Issue_Return_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Deduction_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        item1 = New GridViewSummaryItem("Credit_Note_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        item1 = New GridViewSummaryItem("Payable_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("VSP_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Service_Charge_Amt", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Advance_Payment_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Advance_Payment_Amount_Knock_Off", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_VSP_Diff_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_EMP", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_Incentive", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_IncentiveEMP", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_Net_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("NextCycleDebitNote", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("FarmerPayment", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        item1 = New GridViewSummaryItem("FarmerMilkQty", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("FarmerSaleAmount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("FarmerSaleReturnAmount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        item1 = New GridViewSummaryItem("FarmerAdjustmentAmount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        item1 = New GridViewSummaryItem("FarmerPayableAmount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Handling_Charges_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("SRN_Net_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("SRN_RO_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("PrevCycleDebitNote", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("PrevCycleDebitNoteMP", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("NextCycleDebitNoteMP", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("VSP_Excess_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_Total_Deduction", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("MP_Total_Advance_Amount", "{0:N2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)


        gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Private Sub frmFarmerLedgerReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
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
        btnGenrate.Enabled = True
        gv3.DataSource = Nothing
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

    Private Sub frmFarmerLedgerReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv3
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
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
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout delete successfully", "Information")
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Distributer Ledger Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Distributer Ledger Report", gv3, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Distributer Ledger Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
    End Sub

#Region "grid operations"


#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    ' ============= Addded by Preeti gupta============
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")

                arrHeader.Add("Name : " & Me.Text)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv3, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub





    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtCustCode._My_Click
        Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Code as [Code],Vendor_Name as [Name],VLCH.VLC_Code AS [VLC Code],VLCH.VLC_Name as [VLC Name],VLCH.MCC as [MCC Code]," &
                            " ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'') As [Alies Name],TSPL_VENDOR_MASTER.Add1,TSPL_VENDOR_MASTER.Add2,Add3,TSPL_VENDOR_MASTER.Closing_Date as [Closing Date], " &
                            " TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group Code],TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc as [Vendor Group Description], " &
                            " TSPL_VENDOR_MASTER.City_Code as [City Code],TSPL_VENDOR_MASTER.City_Code_Desc as [City Description],TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Phone1,TSPL_VENDOR_MASTER.Phone2,TSPL_VENDOR_MASTER.Fax,TSPL_VENDOR_MASTER.Email,TSPL_VENDOR_MASTER.WebSite, " &
                            " TSPL_VENDOR_MASTER.Contact_Person_Name as [Contact Person Name],TSPL_VENDOR_MASTER.Contact_Person_Phone as [Contact Person Phone] from TSPL_VENDOR_MASTER " &
                            " inner join TSPL_VLC_MASTER_HEAD VLCH ON TSPL_VENDOR_MASTER.Vendor_Code=VLCH.VSP_Code where 2=2 "
        If Not txtLocation.arrValueMember Is Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry = qry & " and VLCH.MCC in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & " )"
        End If

        TxtCustCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivVendMulSel", qry, "Code", "Name", TxtCustCode.arrValueMember, TxtCustCode.arrDispalyMember)
    End Sub

    Private Sub txtMultDistr__My_Click(sender As Object, e As EventArgs) Handles txtMultDistr._My_Click
        Dim qry As String = " select tspl_mp_master.MP_Code as [Code] ,tspl_mp_master.MP_Name as [Name] ,tspl_mp_master.VLC_Code as [VLC Code] ,tspl_mp_master.Village_Code as [Village Code] ,tspl_mp_master.Father_Name as [Father Name] ,tspl_mp_master.Add1 as [Address1] ,tspl_mp_master.Add2 as [Address2] ,tspl_mp_master.Zila as [Zila] ,tspl_mp_master.Tehsil as [Tehsil] ,tspl_mp_master.City_code as [City Code] ,tspl_mp_master.State_Code as [State Code] ,tspl_mp_master.Country_code as [Country Code] ,tspl_mp_master.Pin_code as [Pin Code] ,tspl_mp_master.Telphone as [Telphone] ,tspl_mp_master.Email as [Email] ,tspl_mp_master.Fax as [Fax] ,tspl_mp_master.DOB as [Date Of Birth] ,tspl_mp_master.Education as [Education] ,tspl_mp_master.Land_Holding as [Land Holding] ,tspl_mp_master.No_Of_Buffaloes as [No Of Buffaloes] ,tspl_mp_master.No_Of_Cows as [No Of Cows] ,tspl_mp_master.No_Of_breedable_milk_animal as [No Of Breedable Milk Animal] ,tspl_mp_master.Milk_production as [Total Milk Production] ,tspl_mp_master.Milk_Home_consumption as [Total Milk Home Consumption] ,tspl_mp_master.Milk_For_sale as [Remaining Milk For Sale] ,tspl_mp_master.PayeeName as [Payee Name] ,tspl_mp_master.BankName as [Bank Name] ,tspl_mp_master.BankBranch as [Bank Branch] ,tspl_mp_master.BankCityCode as [Bank City Code] ,tspl_mp_master.BankStateCode as [Bank State Code] ,tspl_mp_master.IFCICode as [IFCI Code] ,tspl_mp_master.AccountNO as [Account No] ,tspl_mp_master.Created_By as [Created By] ,tspl_mp_master.Created_Date as [Created Date] ,tspl_mp_master.Modified_By as [Modified By] ,tspl_mp_master.Modified_Date as [Modified Date] ,tspl_mp_master.Comp_Code as [Company Code],tspl_mp_master.Mp_code_Vlc_uploader as [MP Code VLC Uploder],VLCH.MCC as [MCC Code],VLCH.VSP_Code as [VSP Code]  From tspl_mp_master left join TSPL_VLC_MASTER_HEAD VLCH on tspl_mp_master.VLC_Code=VLCH.VLC_Code where 2=2 "
        If Not TxtCustCode.arrValueMember Is Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
            qry = qry & " and VLCH.VSP_Code IN (" & clsCommon.GetMulcallString(TxtCustCode.arrValueMember) & " )"
        End If
        txtMultDistr.arrValueMember = clsCommon.ShowMultipleSelectForm("DistrMulSel", qry, "Code", "Name", txtMultDistr.arrValueMember, txtMultDistr.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " SELECT MCC_Code AS Code,MCC_NAME as Name,MCC_Type as [MCC Type],Add1 as Address1,Add2 as Address2,City_code as [City Code],Pin_code as [Pin Code],Telphone,Email FROM TSPL_MCC_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
End Class
