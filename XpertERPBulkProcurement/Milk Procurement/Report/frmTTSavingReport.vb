Imports System.IO
Imports common

Public Class frmTTSavingReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim MultipleFinderFillAuto As Boolean = False
#End Region

    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub frmTTSavingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = dtpToDate.Value.AddMonths(-1)
        Reset()
    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        'ControlEnableDisable(True)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click_1(sender As Object, e As EventArgs) Handles btnGo.Click
        PrintData()
    End Sub

    Sub PrintData()
        Try
            If fromDate.Value > dtpToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If

            Gv1.MasterTemplate.SummaryRowsBottom.Clear()

            Dim whrcls As String = Nothing
            Dim Query As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            Dim from_Date As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy")
            Dim To_date As String = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")

            Query = " select ROW_NUMBER() over(order by TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as SNo,'" + from_Date + "' as FD,'" + To_date + "' as TD,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,xxxx.Vendor_Code,TSPL_VENDOR_MASTER.Bank_Code,
                      TSPL_BANK_MASTER.DESCRIPTION AS Bank_Name,xxxx.Invoice_Amt as Total_Amt,xxxx.Settlement_Amt,xxxx.Transfer_Amt,(xxxx.Invoice_Amt-xxxx.Settlement_Amt) as Outstanding_BLC from 
(select Vendor_Code,sum(Document_Total)Document_Total,
sum(Document_Total* case when RI=4 and Posting_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' then 1 else 0 end )  as Invoice_Amt,
sum(Document_Total* case when RI=2 and Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' then 1 else 0 end )  as Total_Amt,
sum(Document_Total* case when RI=3 and From_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'and To_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' then 1 else 0 end ) AS Settlement_Amt,
sum(Document_Total* case when RI=2 and Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'and Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' then 1 else 0 end ) AS Transfer_Amt

from (
select Vendor_Code,TSPL_TRANSFER_TO_SAVING.Document_No,Document_Date as Document_Date,Posted_Date as Posting_Date,'' as From_Date,'' as To_Date ,Amount as Document_Total,2 as RI,0 AS CHK  
from TSPL_TRANSFER_TO_SAVING_DETAIL
left outer join TSPL_TRANSFER_TO_SAVING on TSPL_TRANSFER_TO_SAVING.Document_No = TSPL_TRANSFER_TO_SAVING_DETAIL.Document_No
where  Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'and Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' 
union all
select Vendor_Code,Document_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as Document_Date,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date ,Document_Total,3 as RI,0 AS CHK  from TSPL_PAYMENT_PROCESS_SAVING
LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
LEFT OUTER JOIN TSPL_PAYMENT_PROCESS_HEAD ON TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_SAVING.Doc_No
where  2=2 AND Against_TransferToSavingPKID IS NOT NULL and TSPL_PAYMENT_PROCESS_HEAD.From_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'and TSPL_PAYMENT_PROCESS_HEAD.To_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' 
union all
select Vendor_Code,Document_No,'' as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'' as From_Date,'' as To_Date ,Document_Total,4 as RI,0 AS CHK  from TSPL_VENDOR_INVOICE_HEAD
where  2=2 AND Transfer_To_Saving=1
)xxx   
group by Vendor_Code)xxxx 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xxxx.Vendor_Code
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxxx.Vendor_Code
left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_VENDOR_MASTER.Company_Bank

 "

            If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
                Query += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ")"
            End If
            If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                Query += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtMultDCS.arrValueMember) + ")"
            End If

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(Query)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dt
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.BestFitColumns()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            Dim summaryRowItem As New GridViewSummaryRowItem()
            FormatGridData()
            EnableDisaableControls(False)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGridData()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
            Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).VisibleInColumnChooser = True
            'Gv1.Columns(ii). = True
        Next

        Gv1.Columns("SNo").IsVisible = True
        Gv1.Columns("SNo").Width = 100
        Gv1.Columns("SNo").HeaderText = "SNo"

        Gv1.Columns("FD").IsVisible = True
        Gv1.Columns("FD").Width = 100
        Gv1.Columns("FD").HeaderText = "From Date"

        Gv1.Columns("TD").IsVisible = True
        Gv1.Columns("TD").Width = 100
        Gv1.Columns("TD").HeaderText = "To Date"

        'Gv1.Columns("Invoice_Entry_Date").IsVisible = True
        'Gv1.Columns("Invoice_Entry_Date").Width = 100
        'Gv1.Columns("Invoice_Entry_Date").HeaderText = "Invoice Date"

        Gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
        Gv1.Columns("VLC_Code_VLC_Uploader").Width = 100
        Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"

        Gv1.Columns("VLC_Name").IsVisible = True
        Gv1.Columns("VLC_Name").Width = 100
        Gv1.Columns("VLC_Name").HeaderText = "DCS Name"

        Gv1.Columns("Bank_Name").IsVisible = True
        Gv1.Columns("Bank_Name").Width = 100
        Gv1.Columns("Bank_Name").HeaderText = "Bank"

        Gv1.Columns("Total_Amt").IsVisible = True
        Gv1.Columns("Total_Amt").Width = 100
        Gv1.Columns("Total_Amt").HeaderText = "Total Amt"

        Gv1.Columns("Settlement_Amt").IsVisible = True
        Gv1.Columns("Settlement_Amt").Width = 100
        Gv1.Columns("Settlement_Amt").HeaderText = "Settlement Amt"

        Gv1.Columns("Transfer_Amt").IsVisible = True
        Gv1.Columns("Transfer_Amt").Width = 100
        Gv1.Columns("Transfer_Amt").HeaderText = "Transfer Amt"

        Gv1.Columns("Outstanding_BLC").IsVisible = True
        Gv1.Columns("Outstanding_BLC").Width = 150
        Gv1.Columns("Outstanding_BLC").HeaderText = "Outstanding Balance"


        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim Total_Amt As New GridViewSummaryItem("Total_Amt", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Total_Amt)
        Dim Settlement_Amt As New GridViewSummaryItem("Settlement_Amt", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Settlement_Amt)
        Dim Transfer_Amt As New GridViewSummaryItem("Transfer_Amt", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Transfer_Amt)
        Dim Outstanding_BLC As New GridViewSummaryItem("Outstanding_BLC", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Outstanding_BLC)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If fromDate.Value > dtpToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If

            Gv1.MasterTemplate.SummaryRowsBottom.Clear()

            Dim whrcls As String = Nothing
            Dim Query As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty

            Query = " Select ROW_NUMBER() over(order by TSPL_VENDOR_INVOICE_HEAD.Document_No) as SNo,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ,
                      TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Document_Type ,
                      TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Bank_Code,
                      TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Total_Amount,TSPL_TRANSFER_TO_SAVING_DETAIL.Amount as TTSAmt,
                      (TSPL_VENDOR_INVOICE_HEAD.Balance_Amt-TSPL_TRANSFER_TO_SAVING_DETAIL.Amount)As OutstandingBalance,
                      TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,
                      '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' as fromdate,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' as ToDate,
                      TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME
                      from TSPL_VENDOR_INVOICE_DETAIL
                      left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No
                      left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                      LEFT OUTER JOIN TSPL_TRANSFER_TO_SAVING_DETAIL ON TSPL_TRANSFER_TO_SAVING_DETAIL.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                      left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                      LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1= '" + objCommonVar.CurrComp_Code1 + "'
                      where 2=2  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>=convert(date,'" + fromDate.Value + "',103) 
                      and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<=convert(date,'" + dtpToDate.Value + "',103)
                      and Transfer_To_Saving=1 "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "rptSavingBalanceReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultBmc__My_Click(sender As Object, e As EventArgs) Handles txtMultBmc._My_Click
        Dim qry As String = " select TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where 2=2  "
        txtMultBmc.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "MCC_NAME", "", txtMultBmc.arrValueMember, txtMultBmc.arrDispalyMember)
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Dim qry As String = " Select VSP_Code AS Code,VLC_Code_VLC_Uploader,VLC_Name as Name from TSPL_VLC_MASTER_HEAD WHERE 2=2 "
        txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Code", "Name", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("", Gv1, Nothing, Me.Text)
            Else
                clsCommon.MyExportToPDF("", Gv1, Nothing, Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnReset_Click_1(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisaableControls(True)
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub EnableDisaableControls(ByVal flag As Boolean)
        fromDate.Enabled = flag
        dtpToDate.Enabled = flag
        'Gv1.DataSource = Nothing
        'RadPageView1.SelectedPage = RadPageViewPage1

    End Sub
End Class