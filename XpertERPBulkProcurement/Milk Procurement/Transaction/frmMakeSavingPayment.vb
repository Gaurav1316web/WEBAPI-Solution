Imports System.Data.SqlClient
Imports common
Public Class frmMakeSavingPayment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Const colSlno As String = "colSlno"
    Public Const colDCSUploaderCode As String = "colDCSUploaderCode"
    Public Const colDCSCode As String = "colDCSCode"
    Public Const colDCSName As String = "colDCSName"
    Public Const colSavingAmt As String = "colSavingAmt"
    Public Const colSaleAmt As String = "colSaleAmt"
    Public Const colDeductionAmt As String = "colDeductionAmt"
    Public Const colPayableAmt As String = "colPayableAmt"

    Dim IsNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag

    End Sub
    Private Sub FrmMultipleProcDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")


        AddNew()
    End Sub
    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name
from TSPL_MCC_MASTER 
inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.MCC_Code"
            Dim whrCls As String = ""
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                End If
            End If
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "MSP@BMC", qry, "Code", "", txtBMC.arrValueMember, Nothing)
            txtVSP.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If txtBMC.arrValueMember Is Nothing OrElse txtBMC.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one" + txtBMC.MyLinkLable1.Text)
            End If
            Dim qry As String = "select VSP_Code as DCSCode,TSPL_VENDOR_MASTER.Vendor_Name as DCSName,VLC_Code_VLC_Uploader as UploaderNo,VLC_Code as VLCCode,VLC_Name as VLCNAme
from TSPL_VENDOR_MASTER
inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code"
            Dim whrCls As String = " TSPL_VENDOR_MASTER.Form_Type='VSP' and and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")"
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "MSP@DCS", qry, "DCSCode", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            LoadBlankGrid()
            If txtBMC.arrValueMember Is Nothing OrElse txtBMC.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one" + txtBMC.MyLinkLable1.Text)
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one " + txtVSP.MyLinkLable1.Text)
            End If

            Dim qry As String = "select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as DCSCode,TSPL_VENDOR_MASTER.Vendor_Name as DCSName,
TSPL_VENDOR_INVOICE_HEAD.Document_No as APInvoiceNo,TSPL_VENDOR_INVOICE_HEAD.Posting_Date, TSPL_PAYMENT_PROCESS_HEAD.Doc_No as PaymentProcessNo,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as PaymentProcessDate,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)+'-' +convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Cycle,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code,TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Amount
from  TSPL_PAYMENT_PROCESS_SAVING
Left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No 
Left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  
left outer join TSPL_PAYMENT_PROCESS_DETAIL on TSPL_PAYMENT_PROCESS_DETAIL.Doc_No=TSPL_PAYMENT_PROCESS_SAVING.Doc_No  and  TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
WHERE TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") 
and  TSPL_VENDOR_INVOICE_HEAD.posting_date is not null  
and ISNULL(TSPL_VENDOR_INVOICE_HEAD.Saving,0)=1 and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 
and isnull(TSPL_PAYMENT_PROCESS_DETAIL.is_Hold_Payment_Process_Saving,0)=1 and ISNULL(TSPL_PAYMENT_PROCESS_HEAD.isPosted,0)=1
and TSPL_VENDOR_INVOICE_HEAD.posting_date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
and TSPL_VENDOR_INVOICE_HEAD.posting_date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
and not exists(select 1 from TSPL_MAKE_SAVING_PAYMENT_SAVING where TSPL_MAKE_SAVING_PAYMENT_SAVING.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_MAKE_SAVING_PAYMENT_SAVING.Doc_Code not in ('" + txtCode.Value + "'))
order by DCSCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date"
            Dim dtSaving As DataTable = clsDBFuncationality.GetDataTable(qry)

            qry = "select  Vendor_Code,Sale_Invoice_No,[Sale_Inoivce_Date] ,[AR_Invoice_No], Balance_Amt as OriginalBalanceAmt from (  
select  TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] , TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Inoivce_Date] ,TSPL_Customer_Invoice_Head.Document_No as [AR_Invoice_No] ,   TSPL_Customer_Invoice_Head.Document_Date as [AR_Invoice_Date], TSPL_Customer_Invoice_Head.Balance_Amt 
from TSPL_SD_SHIPMENT_HEAD  
left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code     
inner join TSPL_Customer_Invoice_Head on  TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No  and coalesce(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>''   
left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code   
left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No    
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_SD_SALE_INVOICE_HEAD.Deduction 
where  isnull(TSPL_SD_SHIPMENT_HEAD.Is_CashSale,'N')='N' and TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "' and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "'  and tspl_customer_invoice_head.Balance_Amt<>0 
and TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code  in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") 
)xx left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xx.Vendor_Code "
            Dim dtDCSSale As DataTable = clsDBFuncationality.GetDataTable(qry)

            qry = "select Vendor_Code into #DCS1 from tspl_Vendor_master where vendor_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") ;" + Environment.NewLine +
"Select Document_No,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(Vendor_Code) as Vendor_Code,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(Vendor_Name) as Vendor_Name,(case when min(DeductionCode)<>max(DeductionCode) then '*' else '' end)+max(DeductionCode) as DeductionCode,(case when min(DeductionCode)<>max(DeductionCode) then '*' else '' end)+max(Deduction_Desc) as Deduction_Desc,Max(Total_Amount) as Total_Amount,max(Sequence_No) as Sequence_No,max(Sequence_No2) as Sequence_No2,max(Posting_Date) as Posting_Date from (  
select TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,
TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.DeductionCode else TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction end as DeductionCode , case when len(isnull(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,''))>0 then TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc else TSPL_DCS_ADDITION_DEDUCTION.Description end as Deduction_Desc,
TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Total_Amount ,(case when TSPL_VENDOR_INVOICE_HEAD.RefDocType='VSP-NGT' then -2 else (case when  TSPL_DEDUCTION_MASTER.Sequence_No is null then -1 else 0 end) end)  as Sequence_No 
,TSPL_VENDOR_INVOICE_HEAD.Posting_Date 
,TSPL_DEDUCTION_MASTER.Sequence_No as Sequence_No2 
from TSPL_VENDOR_INVOICE_DETAIL 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction  
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
inner join #DCS1 on #DCS1.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  
where  Document_Type ='D' and ((TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' And (ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'' or ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'')) or  len(coalesce(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,''))>0)  
And TSPL_VENDOR_INVOICE_HEAD.Balance_Amt > 0  And  coalesce(Posting_Date,'')<>'' And isnull(TSPL_VENDOR_INVOICE_HEAD.Saving,0)=0 
and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "' and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "'  
            )xxx group by Document_No  order by Vendor_Code,Sequence_No,Posting_Date,Sequence_No2  "
            Dim dtDeduction As DataTable = clsDBFuncationality.GetDataTable(qry)


            Dim arr As New Dictionary(Of String, clsMakeSavingPaymentDetail)
            If dtSaving IsNot Nothing AndAlso dtSaving.Rows.Count > 0 Then
                For Each dr As DataRow In dtSaving.Rows
                    If Not arr.ContainsKey(clsCommon.myCstr(dr("DCSCode"))) Then
                        Dim obj As New clsMakeSavingPaymentDetail
                        obj.DCS_Code = clsCommon.myCstr(dr("DCSCode"))
                        obj.DCS_Name = clsCommon.myCstr(dr("DCSName"))
                        obj.DCS_UploaderNo = clsfrmVLCMaster.getVLCUploaderOnVSPCode(obj.DCS_Code, Nothing)
                        obj.Saving_Amt = 0
                        obj.DCS_Sale_Amt = 0
                        obj.Deduction_Amt = 0
                        obj.Payable_Amt = 0
                        obj.ArrDCSSaving = New List(Of clsMakeSavingPaymentSaving)
                        obj.ArrDCSSale = New List(Of clsMakeSavingPaymentDCSSale)
                        obj.ArrDCSDeduction = New List(Of clsMakeSavingPaymentDeduction)
                        arr.Add(clsCommon.myCstr(dr("DCSCode")), obj)
                    End If
                    Dim objSaving As New clsMakeSavingPaymentSaving
                    objSaving.IsSelect = True
                    objSaving.SNo = arr(clsCommon.myCstr(dr("DCSCode"))).ArrDCSSaving.Count + 1
                    objSaving.AP_Invoice_No = clsCommon.myCstr(dr("APInvoiceNo"))
                    objSaving.DocDate = clsCommon.myCDate(dr("Posting_Date"))
                    objSaving.Amount = clsCommon.myCDecimal(dr("Amount"))
                    arr(clsCommon.myCstr(dr("DCSCode"))).Saving_Amt += objSaving.Amount
                    arr(clsCommon.myCstr(dr("DCSCode"))).ArrDCSSaving.Add(objSaving)
                Next
                For Each dr As DataRow In dtDCSSale.Rows
                    If arr.ContainsKey(clsCommon.myCstr(dr("Vendor_Code"))) Then
                        Dim objSale As New clsMakeSavingPaymentDCSSale
                        objSale.IsSelect = True
                        objSale.SNo = arr(clsCommon.myCstr(dr("Vendor_Code"))).ArrDCSSale.Count + 1
                        objSale.AR_Invoice_No = clsCommon.myCstr(dr("AR_Invoice_No"))
                        objSale.DocDate = clsCommon.myCDate(dr("Sale_Inoivce_Date"))
                        objSale.Amount = clsCommon.myCDecimal(dr("OriginalBalanceAmt"))
                        objSale.Red_Ded_Amount = 0
                        arr(clsCommon.myCstr(dr("Vendor_Code"))).DCS_Sale_Amt += objSale.Amount
                        arr(clsCommon.myCstr(dr("Vendor_Code"))).ArrDCSSale.Add(objSale)
                    End If
                Next

                For Each dr As DataRow In dtDeduction.Rows
                    If arr.ContainsKey(clsCommon.myCstr(dr("Vendor_Code"))) Then
                        Dim objDed As New clsMakeSavingPaymentDeduction
                        objDed.IsSelect = True
                        objDed.SNo = arr(clsCommon.myCstr(dr("Vendor_Code"))).ArrDCSDeduction.Count + 1
                        objDed.AP_Invoice_No = clsCommon.myCstr(dr("Document_No"))
                        objDed.DocDate = clsCommon.myCDate(dr("Posting_Date"))
                        objDed.Amount = clsCommon.myCDecimal(dr("Total_Amount"))
                        objDed.Red_Ded_Amount = 0
                        arr(clsCommon.myCstr(dr("Vendor_Code"))).Deduction_Amt += objDed.Amount
                        arr(clsCommon.myCstr(dr("Vendor_Code"))).ArrDCSDeduction.Add(objDed)
                    End If
                Next
            End If

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each strkey As String In arr.Keys
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = arr.Item(strkey).DCS_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderCode).Value = arr.Item(strkey).DCS_UploaderNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSName).Value = arr.Item(strkey).DCS_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSavingAmt).Value = arr.Item(strkey).Saving_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSavingAmt).Tag = arr.Item(strkey).ArrDCSSaving
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleAmt).Value = arr.Item(strkey).DCS_Sale_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionAmt).Value = arr.Item(strkey).Deduction_Amt
                    Dim balanceAmt As Decimal = arr.Item(strkey).Saving_Amt - arr.Item(strkey).DCS_Sale_Amt - arr.Item(strkey).Deduction_Amt
                    If balanceAmt < 0 Then
                        balanceAmt = Math.Abs(balanceAmt)
                        If balanceAmt > 0 Then
                            If arr.Item(strkey).ArrDCSSale IsNot Nothing AndAlso arr.Item(strkey).ArrDCSSale.Count > 0 Then
                                For ii As Integer = arr.Item(strkey).ArrDCSSale.Count - 1 To 0 Step -1
                                    If balanceAmt > arr.Item(strkey).ArrDCSSale(ii).Amount Then
                                        arr.Item(strkey).ArrDCSSale(ii).Red_Ded_Amount = arr.Item(strkey).ArrDCSSale(ii).Amount
                                        balanceAmt = balanceAmt - arr.Item(strkey).ArrDCSSale(ii).Red_Ded_Amount
                                    Else
                                        arr.Item(strkey).ArrDCSSale(ii).Red_Ded_Amount = balanceAmt
                                        balanceAmt = 0
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                        If balanceAmt > 0 Then
                            If arr.Item(strkey).ArrDCSDeduction IsNot Nothing AndAlso arr.Item(strkey).ArrDCSDeduction.Count > 0 Then
                                For ii As Integer = arr.Item(strkey).ArrDCSDeduction.Count - 1 To 0 Step -1
                                    If balanceAmt > arr.Item(strkey).ArrDCSDeduction(ii).Amount Then
                                        arr.Item(strkey).ArrDCSDeduction(ii).Red_Ded_Amount = arr.Item(strkey).ArrDCSDeduction(ii).Amount
                                        balanceAmt = balanceAmt - arr.Item(strkey).ArrDCSDeduction(ii).Red_Ded_Amount
                                    Else
                                        arr.Item(strkey).ArrDCSDeduction(ii).Red_Ded_Amount = balanceAmt
                                        balanceAmt = 0
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleAmt).Tag = arr.Item(strkey).ArrDCSSale
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionAmt).Tag = arr.Item(strkey).ArrDCSDeduction
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPayableAmt).Value = balanceAmt
                    UpdateCurrentRow(gv1.Rows.Count - 1)
                Next
                EnableDisableAllControl(False)
            Else
                Throw New Exception("No Data Found to Display")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub EnableDisableAllControl(v As Boolean)
        txtFromDate.Enabled = v
        txtToDate.Enabled = v
        txtBMC.Enabled = v
        txtVSP.Enabled = v
        RadButton1.Enabled = v
        RadButton2.Enabled = Not v
    End Sub

    Sub SetGridFormat()
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("Loc_Seg_Code").IsVisible = False
        gv1.Columns("Doc_Date").IsVisible = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.AutoSizeRows = False
        gv1.BestFitColumns()
    End Sub

    Sub SaveDataNA(ByVal isPost As Boolean)
        clsCommon.ProgressBarPercentShow()
        Try
            clsCommon.ProgressBarPercentUpdate(0, 100, "Varify DCS Details(1/3)")
            Dim MaxDate As Date = txtPaymentDate.Value
            Dim ArrVendor As New ArrayList
            For ii As Integer = 0 To gv1.Rows.Count - 1
                clsCommon.ProgressBarPercentUpdate(ii + 1, gv1.Rows.Count, "Varify DCS Details(1/3)")
                If Not ArrVendor.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value)) Then
                    ArrVendor.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value))
                End If
                If MaxDate < clsCommon.myCDate(gv1.Rows(ii).Cells("Doc_Date").Value) Then
                    MaxDate = clsCommon.myCDate(gv1.Rows(ii).Cells("Doc_Date").Value)
                End If
            Next
            If MaxDate > txtPaymentDate.Value Then
                Throw New Exception("Payment date should be greater than [" + clsCommon.GetPrintDate(MaxDate, "dd/MM/yyyy") + "]")
            End If
            If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
                Dim qry As String = "select vendor_code from TSPL_VENDOR_MASTER where vendor_code in (" + clsCommon.GetMulcallString(ArrVendor) + ") and LEN(ISNULL(Company_Bank,''))<=0"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "Please define Company saving account for following DCS "
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        qry += "[" + dt.Rows(ii)("vendor_code") + "] "
                    Next
                    Throw New Exception(qry)
                End If


                clsCommon.ProgressBarPercentUpdate(0, 100, "Collecting Payment Data(2/3)")

                Dim arrPayment As New Dictionary(Of String, clsPaymentHeader)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate(ii + 1, gv1.Rows.Count, "Collecting Payment Data(2/3)")
                    If Not arrPayment.ContainsKey(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value)) Then
                        Dim objPay As New clsPaymentHeader()
                        'objPay.Against_PP_Detail_No = obj.ArrPPDetail(i).PP_Detail_No
                        objPay.Payment_No = ""
                        objPay.Entry_Desc = "Saving Payment entry"
                        objPay.Payment_Date = txtPaymentDate.Value
                        objPay.Payment_Post_Date = txtPaymentDate.Value
                        objPay.Bank_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Company_Bank from TSPL_VENDOR_MASTER where vendor_code in ('" + clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value) + "')  "))
                        objPay.Payment_Type = "PY"
                        objPay.Vendor_Code = clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value)
                        objPay.Vendor_Name = clsVendorMaster.GetName(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value), Nothing)
                        objPay.Payment_Code = "NEFT"
                        objPay.Account_Payee = 0
                        objPay.memorndmamt = "0"
                        objPay.Applied_Payment = clsCommon.myCstr(gv1.Rows(ii).Cells("APInvoiceNo").Value)
                        objPay.Is_Security = 0
                        objPay.IsChkReverse = "N"
                        objPay.Bank_Charges = 0
                        objPay.Saving = True

                        objPay.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells("Loc_Seg_Code").Value)
                        objPay.ArrTr = New List(Of clsPaymentDetail)
                        arrPayment.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value), objPay)
                    End If
                    Dim objTr As New clsPaymentDetail()
                    objTr.Apply = "1"
                    objTr.Payment_Type = "PY"
                    objTr.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells("APInvoiceNo").Value)
                    objTr.Original_Invoice_Amt = clsCommon.myCDecimal(gv1.Rows(ii).Cells("Amount").Value)
                    objTr.Applied_Amount = clsCommon.myCDecimal(gv1.Rows(ii).Cells("Amount").Value)
                    objTr.Pending_Balance = 0
                    objTr.Net_Balance = 0
                    'objTr.Vendor_Invoice_No = clsCommon.myCstr(gv1.Rows(ii).Cells("APInvoiceNo").Value)
                    objTr.Security_Amount = 0

                    arrPayment.Item(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value)).ArrTr.Add(objTr)
                    arrPayment.Item(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value)).Payment_Amount += clsCommon.myCDecimal(gv1.Rows(ii).Cells("Amount").Value)
                    arrPayment.Item(clsCommon.myCstr(gv1.Rows(ii).Cells("DCSCode").Value)).Balance_Amt = clsCommon.myCDecimal(gv1.Rows(ii).Cells("Amount").Value)
                Next

                clsCommon.ProgressBarPercentUpdate(0, 100, "Generating Payment Data (3/3)")
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    For ii As Integer = 0 To arrPayment.Count - 1
                        clsCommon.ProgressBarPercentUpdate(ii + 1, arrPayment.Count, "Generating Payment Entry (3/3)")
                        Dim objPay As clsPaymentHeader = arrPayment(arrPayment.Keys(ii))
                        objPay.SaveData(objPay, True, trans, True)
                        clsPaymentHeader.PostData(objPay.Payment_No, "Payable", trans)
                    Next
                    'Throw New Exception("Balwinder singh premi")
                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
                EnableDisableAllControl(True)
                gv1.DataSource = Nothing
            Else
                Throw New Exception("No valid DCS found")
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow

    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        EnableDisableAllControl(True)
        LoadBlankGrid()
    End Sub



    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub

    Sub AddNew()
        btnSave.Enabled = True
        btnPost.Enabled = True
        btndelete.Enabled = True

        IsNewEntry = True
        txtCode.Value = ""
        txtRemarks.Text = ""
        txtPaymentDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtPaymentDate.Value
        txtToDate.Value = txtPaymentDate.Value
        txtBMC.arrValueMember = Nothing
        txtVSP.arrValueMember = Nothing
        lblPrePending.Status = ERPTransactionStatus.Pending
        EnableDisableAllControl(True)
        LoadBlankGrid()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "DCS Code"
        repoString.WrapText = True
        repoString.Name = colDCSCode
        'repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.IsVisible = False
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "DCS "
        repoString.WrapText = True
        repoString.Name = colDCSUploaderCode
        'repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "DCS Name"
        repoString.WrapText = True
        repoString.Name = colDCSName
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Saving Amount"
        repoDecimal.Name = colSavingAmt
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Sale Amount"
        repoDecimal.Name = colSaleAmt
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Deduction Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colDeductionAmt
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Payable Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colPayableAmt
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)



        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            Dim obj As clsMakeSavingPayment = New clsMakeSavingPayment
            obj.Doc_Code = txtCode.Value
            obj.Doc_Date = txtPaymentDate.Value
            obj.Remarks = txtRemarks.Text
            obj.Filter_From_Date = txtFromDate.Value
            obj.Filter_To_Date = txtToDate.Value
            obj.arr = New List(Of clsMakeSavingPaymentDetail)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim objtr As New clsMakeSavingPaymentDetail()
                objtr.DCS_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSCode).Value)
                objtr.Saving_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colSavingAmt).Value)
                objtr.DCS_Sale_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colSaleAmt).Value)
                objtr.Deduction_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDeductionAmt).Value)
                objtr.Payable_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPayableAmt).Value)

                objtr.ArrDCSSaving = TryCast(gv1.Rows(ii).Cells(colSavingAmt).Tag, List(Of clsMakeSavingPaymentSaving))
                objtr.ArrDCSSale = TryCast(gv1.Rows(ii).Cells(colSaleAmt).Tag, List(Of clsMakeSavingPaymentDCSSale))
                objtr.ArrDCSDeduction = TryCast(gv1.Rows(ii).Cells(colDeductionAmt).Tag, List(Of clsMakeSavingPaymentDeduction))
                obj.arr.Add(objtr)
            Next
            obj.SaveData(obj, IsNewEntry)
            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            LoadData(obj.Doc_Code, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As New clsMakeSavingPayment()
            obj = clsMakeSavingPayment.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                If obj.Status = ERPTransactionStatus.Posted Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                lblPrePending.Status = obj.Status
                IsNewEntry = False
                txtCode.Value = obj.Doc_Code
                txtPaymentDate.Value = obj.Doc_Date
                txtFromDate.Value = obj.Filter_From_Date
                txtToDate.Value = obj.Filter_To_Date
                txtRemarks.Text = obj.Remarks
                txtVSP.arrValueMember = obj.Filter_arrVSP
                txtBMC.arrValueMember = obj.Filter_arrBMC
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objTr As clsMakeSavingPaymentDetail In obj.arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = objTr.DCS_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderCode).Value = clsfrmVLCMaster.getVLCUploaderOnVSPCode(objTr.DCS_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSName).Value = objTr.DCS_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSavingAmt).Value = objTr.Saving_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSavingAmt).Tag = objTr.ArrDCSSaving
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleAmt).Value = objTr.DCS_Sale_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleAmt).Tag = objTr.ArrDCSSale
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionAmt).Value = objTr.Deduction_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionAmt).Tag = objTr.ArrDCSDeduction
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPayableAmt).Value = objTr.Payable_Amt
                    Next
                End If
                EnableDisableAllControl(False)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No Document found to delete")
            End If

            If (myMessages.deleteConfirm()) Then
                If clsMakeSavingPayment.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (clsCommon.myLen(txtCode.Value) > 0 AndAlso myMessages.postConfirm()) Then
                If (clsMakeSavingPayment.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_MAKE_SAVING_PAYMENT where Doc_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT.Doc_Code,TSPL_MAKE_SAVING_PAYMENT.Doc_Date,  TSPL_MAKE_SAVING_PAYMENT.Filter_From_Date,TSPL_MAKE_SAVING_PAYMENT.Filter_To_Date,case when TSPL_MAKE_SAVING_PAYMENT.Status=1 then 'Approved' else 'Pending' end as	status " + Environment.NewLine +
        "from TSPL_MAKE_SAVING_PAYMENT" + Environment.NewLine + ""
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("msp@mf", qry, "Doc_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtCode.Value, "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT", "TSPL_MAKE_SAVING_PAYMENT_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIR
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub



    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentRow.Index >= 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colSavingAmt) Then
                    Dim frm As New frmMakeSavingPaymentSelectDocs()
                    frm.InType = 1
                    frm.strDCSUploaderNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSUploaderCode).Value)
                    frm.strDCSName = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSName).Value)
                    frm.ArrInSaving = TryCast(gv1.CurrentRow.Cells(colSavingAmt).Tag, List(Of clsMakeSavingPaymentSaving))
                    If frm.ArrInSaving IsNot Nothing AndAlso frm.ArrInSaving.Count > 0 Then
                        frm.ShowDialog()
                        If Not frm.IsCancelClicked Then
                            gv1.CurrentRow.Cells(colSavingAmt).Tag = frm.ArrOutSaving
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        End If
                    End If
                ElseIf gv1.CurrentColumn Is gv1.Columns(colSaleAmt) Then
                    Dim frm As New frmMakeSavingPaymentSelectDocs()
                    frm.InType = 2
                    frm.strDCSUploaderNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSUploaderCode).Value)
                    frm.strDCSName = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSName).Value)
                    frm.ArrInSale = TryCast(gv1.CurrentRow.Cells(colSaleAmt).Tag, List(Of clsMakeSavingPaymentDCSSale))
                    If frm.ArrInSale IsNot Nothing AndAlso frm.ArrInSale.Count > 0 Then
                        frm.ShowDialog()
                        If Not frm.IsCancelClicked Then
                            gv1.CurrentRow.Cells(colSaleAmt).Tag = frm.ArrOutSale
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        End If
                    End If
                ElseIf gv1.CurrentColumn Is gv1.Columns(colDeductionAmt) Then
                    Dim frm As New frmMakeSavingPaymentSelectDocs()
                    frm.InType = 3
                    frm.strDCSUploaderNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSUploaderCode).Value)
                    frm.strDCSName = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSName).Value)
                    frm.ArrInDeduction = TryCast(gv1.CurrentRow.Cells(colDeductionAmt).Tag, List(Of clsMakeSavingPaymentDeduction))
                    If frm.ArrInDeduction IsNot Nothing AndAlso frm.ArrInDeduction.Count > 0 Then
                        frm.ShowDialog()
                        If Not frm.IsCancelClicked Then
                            gv1.CurrentRow.Cells(colDeductionAmt).Tag = frm.ArrOutDeduction
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdateCurrentRow(ByVal idx As Integer)
        Dim dclSavingAmt As Decimal = 0
        Dim dclSaleAmt As Decimal = 0
        Dim dclDeductionAmt As Decimal = 0

        Dim arrSaving As List(Of clsMakeSavingPaymentSaving) = TryCast(gv1.Rows(idx).Cells(colSavingAmt).Tag, List(Of clsMakeSavingPaymentSaving))
        Dim arrSale As List(Of clsMakeSavingPaymentDCSSale) = TryCast(gv1.Rows(idx).Cells(colSaleAmt).Tag, List(Of clsMakeSavingPaymentDCSSale))
        Dim arrDeduction As List(Of clsMakeSavingPaymentDeduction) = TryCast(gv1.Rows(idx).Cells(colDeductionAmt).Tag, List(Of clsMakeSavingPaymentDeduction))
        If arrSaving IsNot Nothing AndAlso arrSaving.Count > 0 Then
            For Each obj As clsMakeSavingPaymentSaving In arrSaving
                If obj.IsSelect Then
                    dclSavingAmt += obj.Amount
                End If
            Next
        End If
        If arrSale IsNot Nothing AndAlso arrSale.Count > 0 Then
            For Each obj As clsMakeSavingPaymentDCSSale In arrSale
                If obj.IsSelect Then
                    dclSaleAmt += (obj.Amount - obj.Red_Ded_Amount)
                End If
            Next
        End If
        If arrDeduction IsNot Nothing AndAlso arrDeduction.Count > 0 Then
            For Each obj As clsMakeSavingPaymentDeduction In arrDeduction
                If obj.IsSelect Then
                    dclDeductionAmt += (obj.Amount - obj.Red_Ded_Amount)
                End If
            Next
        End If
        Dim dclPayableAmt As Decimal = dclSavingAmt - dclSaleAmt - dclDeductionAmt
        gv1.CurrentRow.Cells(colSavingAmt).Value = dclSavingAmt
        gv1.CurrentRow.Cells(colSaleAmt).Value = dclSaleAmt
        gv1.CurrentRow.Cells(colDeductionAmt).Value = dclDeductionAmt
        gv1.CurrentRow.Cells(colPayableAmt).Value = dclPayableAmt
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Reverse and unpost The payment Process " + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsMakeSavingPayment.ReverseAndUnpost(txtCode.Value)
                    clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String = ""
            qry = " Select TSPL_Vendor_MASTER.BankCode2 as GRPColumn,TSPL_MAKE_SAVING_PAYMENT_DETAIL.Doc_Code,TSPL_MAKE_SAVING_PAYMENT_DETAIL.DCS_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader,coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
  TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name, TSPL_MAKE_SAVING_PAYMENT_DETAIL.Payable_Amt,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,

  '16/04/2024' as FD,'30/04/2024' AS TD,'' AS CycleRange,  TSPL_BANK_MASTER2.DESCRIPTION as Bank_Code_Desc, 
  case when isnull(
    coalesce(
      TSPL_VENDOR_MASTER.vsp_payment, 
      Mp_V.vsp_payment
    ), 
    ''
  )= 'Self' then coalesce(
    TSPL_VENDOR_MASTER.IFSCCode2, mp_V.IFSC_Code
  ) else coalesce(
    TSPL_VENDOR_MASTER.Joint_IFSC_Code, 
    mp_v.Joint_IFSC_Code
  ) end as Payee_Joint_IFSC_Code, 
  case when isnull(
    coalesce(
      TSPL_VENDOR_MASTER.vsp_payment, 
      Mp_V.vsp_payment
    ), 
    ''
  )= 'Self' then coalesce( 
    TSPL_VENDOR_MASTER.AccNo2, mp_V.Account_No
  ) else coalesce(
    TSPL_VENDOR_MASTER.Joint_Account_No, 
    mp_V.Joint_Account_No
  ) end as Payee_Joint_Account_No

  
  from TSPL_MAKE_SAVING_PAYMENT_DETAIL
left outer join TSPL_MAKE_SAVING_PAYMENT on TSPL_MAKE_SAVING_PAYMENT.Doc_Code=TSPL_MAKE_SAVING_PAYMENT_DETAIL.Doc_Code
left outer join TSPL_MAKE_SAVING_PAYMENT_SAVING on TSPL_MAKE_SAVING_PAYMENT_SAVING.Ref_PK_ID=TSPL_MAKE_SAVING_PAYMENT_DETAIL.PK_ID
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MAKE_SAVING_PAYMENT_SAVING.AP_Invoice_No
left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
left outer join TSPL_COMPANY_MASTER on 2=2
left outer join ( select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code 
Left outer join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code 
left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Bank_Code
left outer join TSPL_BANK_MASTER as TSPL_BANK_MASTER2 on TSPL_BANK_MASTER2.BANK_CODE=TSPL_Vendor_MASTER.BankCode2 

Where convert(date,TSPL_MAKE_SAVING_PAYMENT.Filter_From_Date,103) >=convert(date,'" + txtFromDate.Value + "',103)  
and	convert(date,TSPL_MAKE_SAVING_PAYMENT.Filter_To_Date,103) <=convert(date,'" + txtToDate.Value + "',103)  "

            If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")  "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "crptBankAdviceNewSaving", "Bank Advice Saving")
            frmCRV = Nothing


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
