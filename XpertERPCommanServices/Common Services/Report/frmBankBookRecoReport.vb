'Created By- Sanjay ,Ticket No-TEC/09/05/19-000479
Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class frmBankBookRecoReport
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strERPStartDate As String

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
    End Sub
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport 'MyBase.isQuickExportFlag
    End Sub
    Private Sub FrmBankBook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ddlBankType.SelectedIndex = 0
        SetUserMgmtNew()
        chkDetail.IsChecked = True
        dtFrm.Value = System.DateTime.Now.Date
        dtTo.Value = System.DateTime.Now.Date
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        'lbltype.Visible = False
        chkbankcharges.Visible = False
        chkSummary.Visible = False
        chkDetail.Visible = False
        strERPStartDate = objCommonVar.ERPStartDate
        chkExcludeProvisionBank.Visible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowCheckExcludeProvisionBank, clsFixedParameterCode.ShowCheckExcludeProvisionBank, Nothing)) = 1, True, False)
    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        dtFrm.Value = System.DateTime.Now.Date
        dtTo.Value = System.DateTime.Now.Date
        ddlBankType.SelectedIndex = 0
        gvReport.DataSource = Nothing
        gvReport.Rows.Clear()
        gvReport.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        chkSummary.IsChecked = True
        txtBank.arrValueMember = Nothing
        TxtDocType.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        'lbltype.Visible = False
        chkbankcharges.Checked = False
        chkbankcharges.Visible = False
        chkExcludeProvisionBank.Checked = False
    End Sub
    Private Sub txtBank__My_Click(sender As Object, e As EventArgs) Handles txtBank._My_Click
        Dim qry As String = ""
        qry = " select BANK_CODE as Code  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type in ('B','C')"

        txtBank.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeDoc", qry, "Code", "Code", txtBank.arrValueMember, txtBank.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID '+ IIf(chkDetail.IsChecked = True, "D", "S")
        TemplateGridview = gvReport
        RefreshData()
    End Sub


    Public Sub RefreshData()

        Try
            Dim Qry As String = Nothing
            'skg
            Dim strExcludeProvisionBank As String = ""
            If chkExcludeProvisionBank.Checked = True Then
                strExcludeProvisionBank = " and TSPL_BANK_MASTER.IsProvisionBank = 0 "
            End If
            Qry = "select xx.Bank_Code as [Bank Code],xx.V_C_Code as [Vendor-Customer],xx.Doc_Date as [Doc Date],xx.DocType as [Doc Type],xx.TransactionType as [Trans Type],xx.DocNo  " & _
  " as [Document No] ,xx.Dramt as [Transaction Dr],xx.CrAmt as [Transaction Cr],xx.SOURCEDOC_NO as [BB Document No]"
            Qry += ",isnull(xx.Debit_Amount,0) as [Bank Book Dr],isnull(xx.Credit_Amount,0) as [Bank Book Cr] "
            'Qry += ",CONVERT(DECIMAL(18,2),isnull(xx.Debit_Amount,0)* xx.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as [Bank Book Dr] " & _
            '    ",CONVERT(DECIMAL(18,2),isnull(xx.Credit_Amount,0)* xx.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as [Bank Book Cr] "
            'Qry += ",ISNULL(xx.Dramt,0)-CONVERT(DECIMAL(18,2),isnull(xx.Debit_Amount,0)* xx.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as [Diff Dr Amt],isnull(xx.CrAmt,0)-CONVERT(DECIMAL(18,2),isnull(xx.Credit_Amount,0)* xx.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as [Diff Cr Amt] from ( " & _
            Qry += ",ISNULL(xx.Dramt,0)-isnull(xx.Debit_Amount,0) as [Diff Dr Amt],isnull(xx.CrAmt,0)-isnull(xx.Credit_Amount,0) as [Diff Cr Amt] from ( " & _
  " select DocMaster.Bank_Code,DocMaster.Doc_Date,DocMaster.V_C_Code,DocMaster.DocType,DocMaster.DocNo as  " & _
  " DocNo,DocMaster.Dramt,DocMaster.CrAmt  ,BB.SOURCEDOC_NO,BB.Debit_Amount,BB.Credit_Amount , " & _
  " (case when DocMaster.DocType='Reverse' then 'RV-TA' else  case when DocMaster.DocType='BankTransfer' then 'BK-TF' else    " & _
  " case when DocMaster.DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) " & _
  " else    case when DocMaster.DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF'  " & _
  " else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else  " & _
  " case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end " & _
  " ) as TransactionType,ConvRate,BankCharge  From (   "
            Qry += "Select TSPL_RECEIPT_HEADER.Receipt_No as DocNo,TSPL_RECEIPT_HEADER.Entry_Desc ,Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge   " & _
           " , TSPL_RECEIPT_HEADER.Posted,'Receipt' as DocType  ,TSPL_RECEIPT_HEADER.CURRENCY_CODE  " & _
           " ,TSPL_RECEIPT_HEADER.ConvRate,TSPL_RECEIPT_HEADER.Payment_Code   ,case when TSPL_RECEIPT_HEADER.Receipt_Type<>'F' and TSPL_RECEIPT_HEADER.Receipt_Type<>'S' THEN TSPL_RECEIPT_HEADER.Receipt_Amount else 0 end as DrAmt,case when TSPL_RECEIPT_HEADER.Receipt_Type='F' or TSPL_RECEIPT_HEADER.Receipt_Type='S' THEN TSPL_RECEIPT_HEADER.Receipt_Amount else 0  end as CrAmt ,TSPL_RECEIPT_HEADER.Bank_Code " & _
           " ,TSPL_RECEIPT_HEADER.Receipt_Type as TransactionType  ,TSPL_RECEIPT_HEADER.Receipt_Date as Doc_Date,TSPL_RECEIPT_HEADER.Cust_Code as V_C_Code from TSPL_RECEIPT_HEADER  " & _
           " TSPL_RECEIPT_HEADER  WHERE 1=1 "
            Qry += " AND TSPL_RECEIPT_HEADER.Receipt_Type<>'A' and TSPL_RECEIPT_HEADER.Receipt_No not in (select Receipt_No from TSPL_RECEIPT_HEADER where SecurityDeposit='Y' and Receipt_Date< CONVERT(DATE,'" + strERPStartDate + "',103))"
            Qry += " and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) >= convert(date,'" & dtFrm.Value & "',103) and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <= convert(date,'" & dtTo.Value & "',103) "
            Qry += " Union All  " & _
  " Select TSPL_PAYMENT_HEADER.Payment_No  as DocNo,TSPL_PAYMENT_HEADER.Entry_Desc , - TSPL_PAYMENT_HEADER.Bank_Charges AS BankCharge  , Case When TSPL_PAYMENT_HEADER.Posted=1  " & _
  " Then 'Y' Else 'N' End as Posted,'Payment' as DocType  ,TSPL_PAYMENT_HEADER.CURRENCY_CODE ,TSPL_PAYMENT_HEADER.ConvRate,TSPL_PAYMENT_HEADER.Payment_Code   " & _
              " ,case when TSPL_PAYMENT_HEADER.Payment_Type='RC' THEN TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT else 0  end as DrAmt ,case when TSPL_PAYMENT_HEADER.Payment_Type<>'RC' THEN TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT else 0 end as CrAmt ,TSPL_PAYMENT_HEADER.Bank_Code,TSPL_PAYMENT_HEADER.Payment_Type as TransactionType ,TSPL_PAYMENT_HEADER.Payment_Date " & _
  " as Doc_Date,TSPL_PAYMENT_HEADER.Vendor_Code as V_C_Code from TSPL_PAYMENT_HEADER   WHERE 1=1 "
            Qry += " AND TSPL_PAYMENT_HEADER.Payment_Type<>'AD' "
            Qry += " and convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103) >= convert(date,'" & dtFrm.Value & "',103) and convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103) <= convert(date,'" & dtTo.Value & "',103) "
            Qry += " Union All   " & _
  " Select TSPL_BANK_TRANSFER.Transfer_No   as DocNo,TSPL_BANK_TRANSFER.Description AS Entry_Desc , TSPL_BANK_TRANSFER.BankCharges AS BankCharge,Case When TSPL_BANK_TRANSFER.Post='P'  " & _
  " Then 'Y' Else 'N' End as Posted " & _
  " ,'BankTransfer' as DocType,'' as CURRENCY_CODE ,1 as ConvRate,TSPL_BANK_TRANSFER.Payment_Mode , 0 as DrAmt,Transfer_Amount  as CrAmt ,TSPL_BANK_TRANSFER.From_Bank_Code as " & _
  " Bank_Code,TSPL_BANK_TRANSFER.transaction_type as TransactionType ,TSPL_BANK_TRANSFER.Transfer_Date as Doc_Date,'' as V_C_Code from TSPL_BANK_TRANSFER  where 1=1 "
            Qry += " and convert(date,TSPL_BANK_TRANSFER.Transfer_Date,103) >= convert(date,'" & dtFrm.Value & "',103) and convert(date,TSPL_BANK_TRANSFER.Transfer_Date,103) <= convert(date,'" & dtTo.Value & "',103) "
            Qry += " Union All " & _
  " Select TSPL_BANK_TRANSFER.Transfer_No   as DocNo,TSPL_BANK_TRANSFER.Description AS Entry_Desc , TSPL_BANK_TRANSFER.BankCharges AS BankCharge   " & _
  " , Case When TSPL_BANK_TRANSFER.Post='P' Then 'Y' Else 'N' End as Posted,'BankTransfer' as DocType,'' as CURRENCY_CODE ,1 as ConvRate,TSPL_BANK_TRANSFER.Payment_Mode  " & _
  " , Deposit_Amount as DrAmt,0  as CrAmt ,TSPL_BANK_TRANSFER.To_Bank_Code as Bank_Code,TSPL_BANK_TRANSFER.transaction_type as TransactionType ,TSPL_BANK_TRANSFER.Transfer_Date as " & _
  " Doc_Date,'' as V_C_Code from TSPL_BANK_TRANSFER  where 1=1 "
            Qry += " and convert(date,TSPL_BANK_TRANSFER.Transfer_Date,103) >= convert(date,'" & dtFrm.Value & "',103) and convert(date,TSPL_BANK_TRANSFER.Transfer_Date,103) <= convert(date,'" & dtTo.Value & "',103) "
            Qry += " Union All   " & _
  " Select TSPL_BANK_REVERSE.Reverse_Code   as DocNo,TSPL_PAYMENT_HEADER.Entry_Desc , - TSPL_PAYMENT_HEADER.Bank_Charges AS BankCharge  , Case When TSPL_BANK_REVERSE.Post='P'  " & _
  " Then 'Y' Else 'N' End as Posted ,'Reverse' as DocType,TSPL_PAYMENT_HEADER.CURRENCY_CODE ,TSPL_PAYMENT_HEADER.ConvRate,TSPL_PAYMENT_HEADER.Payment_Code  " & _
  " ,case when TSPL_PAYMENT_HEADER.Payment_Type<>'RC' THEN TSPL_BANK_REVERSE.AMOUNT-Reverse_TDS_Amount else 0 end as DrAmt,case when TSPL_PAYMENT_HEADER.Payment_Type='RC' THEN TSPL_BANK_REVERSE.AMOUNT-Reverse_TDS_Amount else 0  end as CrAmt   ,TSPL_PAYMENT_HEADER.Bank_Code,TSPL_PAYMENT_HEADER.Payment_Type as TransactionType,TSPL_BANK_REVERSE.Reversal_Date as Doc_Date " & _
  " ,COALESCE(TSPL_BANK_REVERSE.Vendor_Code,TSPL_BANK_REVERSE.Cust_Code) as V_C_Code  from TSPL_BANK_REVERSE    LEFT OUTER JOIN TSPL_PAYMENT_HEADER   ON TSPL_BANK_REVERSE.Document_No " & _
  "  =TSPL_PAYMENT_HEADER.Payment_No  WHERE  TSPL_BANK_REVERSE.Source_Type='AP' AND TSPL_PAYMENT_HEADER.Payment_Type<>'AD' "
            Qry += " and convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) >= convert(date,'" & dtFrm.Value & "',103) and convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) <= convert(date,'" & dtTo.Value & "',103) "
            Qry += " Union All   " & _
  " Select TSPL_BANK_REVERSE.Reverse_Code  as DocNo,TSPL_RECEIPT_HEADER.Entry_Desc ,Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate  AS BankCharge " & _
  " ,Case When TSPL_BANK_REVERSE.Post='P' Then 'Y' Else 'N' End as Posted  " & _
  " ,'Reverse' as DocType,TSPL_RECEIPT_HEADER.CURRENCY_CODE,TSPL_RECEIPT_HEADER.ConvRate ,TSPL_RECEIPT_HEADER.Payment_Code ,case when TSPL_RECEIPT_HEADER.Receipt_Type='F' or TSPL_RECEIPT_HEADER.Receipt_Type='S' THEN TSPL_RECEIPT_HEADER.Receipt_Amount else 0  end as DrAmt  " & _
  " ,case when TSPL_RECEIPT_HEADER.Receipt_Type<>'F' and TSPL_RECEIPT_HEADER.Receipt_Type<>'S' THEN TSPL_RECEIPT_HEADER.Receipt_Amount else 0 end as CrAmt , TSPL_RECEIPT_HEADER.Bank_Code,TSPL_RECEIPT_HEADER.Receipt_Type as TransactionType  ,TSPL_BANK_REVERSE.Reversal_Date as Doc_Date " & _
  " ,COALESCE(TSPL_BANK_REVERSE.Vendor_Code,TSPL_BANK_REVERSE.Cust_Code) as V_C_Code  from TSPL_BANK_REVERSE    LEFT OUTER JOIN TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER   ON " & _
  " TSPL_BANK_REVERSE.Document_No =TSPL_RECEIPT_HEADER.Receipt_No   WHERE 1=1 "
            Qry += " AND TSPL_RECEIPT_HEADER.Receipt_Type<>'A' and TSPL_RECEIPT_HEADER.Receipt_No not in (select Receipt_No from TSPL_RECEIPT_HEADER where SecurityDeposit='Y' and Receipt_Date< CONVERT(DATE, '" + strERPStartDate + "',103))"
            Qry += " and convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) >= convert(date,'" & dtFrm.Value & "',103) and convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) <= convert(date,'" & dtTo.Value & "',103) "
            Qry += " and TSPL_BANK_REVERSE.Source_Type='AR'   " &
  " ) as DocMaster  " &
  " left join (select SOURCEDOC_NO,DocType,BANK_CODE, sum(Debit_Amount) as Debit_Amount,sum(Credit_Amount) as  Credit_Amount  " &
  " from TSPL_BANK_BOOK group by SOURCEDOC_NO,DocType,BANK_CODE " &
  " )BB ON DocMaster.DocNo=BB.SOURCEDOC_NO AND DocMaster.DocType=BB.DocType  " &
  " and case when DocMaster.DocType='BankTransfer' then  DocMaster.Bank_Code else '' end=case when DocMaster.DocType='BankTransfer' then  BB.Bank_Code else '' end " &
  " Left Outer Join TSPL_BANK_MASTER on DocMaster.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE  WHERE 1=1 AND DocMaster.Posted='Y'  " + strExcludeProvisionBank + " " &
  " )xx "


            'skg

            If clsCommon.CompairString(ddlBankType.Text, "All") = CompairStringResult.Equal Then
                'Qry = "select xx.Bank_Code as [Bank Code],xx.V_C_Code as [Vendor-Customer],xx.Doc_Date as [Doc Date],xx.Doc_Type as [Doc Type],xx.TransactionType as [Trans Type],xx.DocNo as [Document No] " & _
                '    ",xx.Dramt as [Transaction Dr],xx.CrAmt as [Transaction Cr],xx.SOURCEDOC_NO as [BB Document No],xx.Debit_Amount as [Bank Book Dr],xx.Credit_Amount as [Bank Book Cr] " & _
                '    ",ISNULL(xx.Dramt,0)-ISNULL(xx.Debit_Amount,0) as [Diff Dr Amt],isnull(xx.CrAmt,0)-isnull(xx.Credit_Amount,0) as [Diff Cr Amt] from (" + Qry + ")xx where 1=1 "
                Qry += " where 1=1 "
            ElseIf clsCommon.CompairString(ddlBankType.Text, "Document Mismatch") = CompairStringResult.Equal Then
                'Qry = "select xx.Bank_Code as [Bank Code],xx.V_C_Code as [Vendor-Customer],xx.Doc_Date as [Doc Date],xx.Doc_Type as [Doc Type],xx.TransactionType as [Trans Type],xx.DocNo as [Document No] " & _
                '  ",xx.Dramt as [Transaction Dr],xx.CrAmt as [Transaction Cr],xx.SOURCEDOC_NO as [BB Document No],xx.Debit_Amount as [Bank Book Dr],xx.Credit_Amount as [Bank Book Cr] " & _
                '  ",ISNULL(xx.Dramt,0)-ISNULL(xx.Debit_Amount,0) as [Diff Dr Amt],isnull(xx.CrAmt,0)-isnull(xx.Credit_Amount,0) as [Diff Cr Amt] from (" + Qry + ")xx " & _
                '  " where xx.SOURCEDOC_NO is null "
                Qry += " where xx.SOURCEDOC_NO is null "
            ElseIf clsCommon.CompairString(ddlBankType.Text, "Data Mismatch") = CompairStringResult.Equal Then
                'Qry = "select xx.Bank_Code as [Bank Code],xx.V_C_Code as [Vendor-Customer],xx.Doc_Date as [Doc Date],xx.Doc_Type as [Doc Type],xx.TransactionType as [Trans Type],xx.DocNo as [Document No] " & _
                '       ",xx.Dramt as [Transaction Dr],xx.CrAmt as [Transaction Cr],xx.SOURCEDOC_NO as [BB Document No],xx.Debit_Amount as [Bank Book Dr],xx.Credit_Amount as [Bank Book Cr] " & _
                '       ",ISNULL(xx.Dramt,0)-ISNULL(xx.Debit_Amount,0) as [Diff Dr Amt],isnull(xx.CrAmt,0)-isnull(xx.Credit_Amount,0) as [Diff Cr Amt] from (" + Qry + ")xx " & _
                '       " where ISNULL(xx.Dramt,0)-ISNULL(xx.Debit_Amount,0)<>0 or isnull(xx.CrAmt,0)-isnull(xx.Credit_Amount,0)<>0 "
                Qry += " where ISNULL(xx.Dramt,0)-ISNULL(xx.Debit_Amount,0)<>0 or isnull(xx.CrAmt,0)-isnull(xx.Credit_Amount,0)<>0 "
            End If

            If TxtDocType.arrValueMember IsNot Nothing AndAlso TxtDocType.arrValueMember.Count > 0 Then
                Qry += " and xx.DocType in (" + clsCommon.GetMulcallString(TxtDocType.arrValueMember) + ") "
            End If
            If txtBank.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
                Qry += " and xx.Bank_Code in (" + clsCommon.GetMulcallString(txtBank.arrValueMember) + ") " + Environment.NewLine
            End If
            Qry += " order by xx.DocNo"
            dt = clsDBFuncationality.GetDataTable(Qry)


            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("No Data found ")
            Else
                gvReport.MasterTemplate.SummaryRowsBottom.Clear()
                gvReport.DataSource = Nothing
                gvReport.Rows.Clear()
                gvReport.Columns.Clear()
                gvReport.DataSource = dt
                gvReport.EnableFiltering = True
                gvReport.EnableSorting = True
                gvReport.ShowFilteringRow = True
                RadPageView1.SelectedPage = RadPageViewPage2

                                                'gvReport.Columns("TotCredAmt").IsVisible = False
                                                'gvReport.Columns("TotDebAmt").IsVisible = False
                FormatGrid()
                gvReport.BestFitColumns()
                                            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadExcel(ByVal IsPrint As Exporter)
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy") + " ")
        '+ IIf(chkDetail.IsChecked, "( Detail )", "( Summary )")
        If IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid("Bank Book Reco Report", gvReport, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            clsCommon.MyExportToPDF("Bank Book Reco Report", gvReport, arrHeader, "Bank Book Reco Report", True)
        End If
    End Sub
    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        gvReport.EnableFiltering = True
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = True
        Next

        'gvReport.Columns("DocType").IsVisible = False
        'gvReport.Columns("DocType").Width = 121
        'gvReport.Columns("DocType").HeaderText = "DocType"

        'gvReport.Columns("TransactionType").IsVisible = False
        'gvReport.Columns("TransactionType").Width = 121
        'gvReport.Columns("TransactionType").HeaderText = "Transaction Type"

        'gvReport.Columns("TotCredAmt").IsVisible = True
        'gvReport.Columns("TotCredAmt").Width = 121
        'gvReport.Columns("TotCredAmt").HeaderText = "Total Credit Amt"

        'gvReport.Columns("TotDebAmt").IsVisible = True
        'gvReport.Columns("TotDebAmt").Width = 121
        'gvReport.Columns("TotDebAmt").HeaderText = "Total Debit Amt"


        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim TranSUMDrAmt As New GridViewSummaryItem("Transaction Dr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TranSUMDrAmt)
        Dim TranSUMCrAmt As New GridViewSummaryItem("Transaction Cr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TranSUMCrAmt)

        Dim SUMDrAmt As New GridViewSummaryItem("Bank Book Dr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMDrAmt)
        Dim SUMCrAmt As New GridViewSummaryItem("Bank Book Cr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMCrAmt)

        Dim item1 As New GridViewSummaryItem("Diff Cr Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Diff Dr Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvReport.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        '--------------------------------------------------------------------------------------------


    End Sub

    Public Sub ExportToExcelGV()
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""

            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            clsCommon.MyExportToExcelGrid("Bank Book Reco Report", gvReport, arrHeader, "Bank Book Reco Report")

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub FrmBankBook_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()       
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub
    Private Sub chkSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSummary.ToggleStateChanged

    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBookRecoReport & "'"))
            If txtBank.arrDispalyMember IsNot Nothing AndAlso txtBank.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Bank : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
            End If

            transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvReport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvReport.Columns.Count - 1 Step ii + 1
                        gvReport.Columns(ii).IsVisible = False
                        gvReport.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvReport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvReport.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)

    End Sub

    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBookRecoReport & "'"))
            If txtBank.arrDispalyMember IsNot Nothing AndAlso txtBank.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Bank : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs)
        Try
            If gvReport Is Nothing OrElse gvReport.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(gvReport, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            IO.File.WriteAllLines(filePath, transportSql.ExportCSV(sender, AddHeader))
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBookRecoReport & "'"))
            If txtBank.arrDispalyMember IsNot Nothing AndAlso txtBank.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Bank : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
            '+ IIf(chkDetail.IsChecked, "( Detail )", "( Summary )")
            clsCommon.MyExportToPDF("Bank Book Reco Report", gvReport, arrHeader, "Bank Book Reco Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtDocType__My_Click(sender As Object, e As EventArgs) Handles TxtDocType._My_Click
        Dim qry As String = ""
        qry = "select 'Receipt' as Code union select 'Reverse' as Code union select 'Payment' as Code union select 'BankTransfer' as Code"

        TxtDocType.arrValueMember = clsCommon.ShowMultipleSelectForm("DocTypeDoc", qry, "Code", "Code", TxtDocType.arrValueMember, TxtDocType.arrDispalyMember)
    End Sub
End Class
