Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class frmMakeSavingPayment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()

#End Region

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag AndAlso MyBase.isPostFlag

    End Sub
    Private Sub FrmMultipleProcDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        RadButton2.Enabled = False
        btnSave.Enabled = False
        txtPaymentDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtPaymentDate.Value.AddMonths(-1)
        txtToDate.Value = txtPaymentDate.Value
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
            If txtBMC.arrValueMember Is Nothing OrElse txtBMC.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one" + txtBMC.MyLinkLable1.Text)
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one " + txtVSP.MyLinkLable1.Text)
            End If

            Dim qry As String = "select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as DCSCode,TSPL_VENDOR_MASTER.Vendor_Name as DCSName,
TSPL_VENDOR_INVOICE_HEAD.Document_No as APInvoiceNo, TSPL_PAYMENT_PROCESS_HEAD.Doc_No as PaymentProcessNo,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as PaymentProcessDate,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)+'-' +convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Cycle,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code,TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Amount
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
order by DCSCode,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date"
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                SetGridFormat()
                EnableDisableAllControl(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
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
        btnSave.Enabled = Not v
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
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Sub SaveData(ByVal isPost As Boolean)
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
    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
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
        gv1.DataSource = Nothing
    End Sub
End Class
