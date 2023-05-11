'' work done against ticket no.KDI/27/03/18-000165 
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
'Ticket No-BHA/26/04/19-000869, Sanjay
'Ticket No-BHA/13/03/19-000845
Public Class frmReconcilationReport
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

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
        lbltype.Visible = False
        chkbankcharges.Visible = False
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
        txtLocation.arrValueMember = Nothing
        lbltype.Visible = False
        chkbankcharges.Checked = False
        chkbankcharges.Visible = False
        chkExcludeProvisionBank.Checked = False
    End Sub
    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtBank._My_Click
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
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvReport
        RefreshData()
    End Sub


    Public Sub RefreshData()
    
        Try
            Dim Qry As String = Nothing
            Dim BankCharge As String = Nothing

            If chkbankcharges.Checked = True Then
                BankCharge = "BankCharge"
            Else
                BankCharge = 0
            End If
            ' Ticket No  BHA/26/04/19-000869 By Prabhakar: 
            Qry = " select final.*,[Bank Net Amount]-[Net Amount] as [Diff Net] from (select xxx.*,xxx.TotDebAmt-xxx.TotCredAmt as [Net Amount] "
            Qry += " from (select DocType,TransactionType,BANKGL_Account_Code as [GL Account],BANKGL_Account_Name as [Description],BANK_CODE as [Bank Code],BANK_NAME as [Bank Name]"
            If chkDetail.IsChecked = True Then
                Qry += " ,TSPL_BANK_BOOK.SOURCEDOC_NO as [Doc No],convert(varchar,TSPL_BANK_BOOK.SOURCEDOC_DATE,103) as [Doc Date] "
            End If
            Qry += " ,CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as [Bank Debit]"
            Qry += " , CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then  BankCharge  Else 0 End) as [Bank Credit]"
            Qry += " ,isnull(CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End),0)-isnull(CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then  BankCharge  Else 0 End),0) as [Bank Net Amount]"
            If chkDetail.IsChecked = True Then
                Qry += " ,TBL_RECO_CONTROl_ACCOUNT.Voucher_No as [Journal Voucher No] "
            End If
            Qry += " , CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt ,  CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge  Else 0 End) as TotCredAmt  "
            Qry += "      from TSPL_BANK_BOOK"
            ' Qry += " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_BANK_BOOK.SOURCEDOC_NO"
            ' Qry += " left outer join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No and TSPL_JOURNAL_DETAILS.Account_code =TSPL_BANK_BOOK.BANKGL_Account_Code "
            Qry += " left outer join ( Select  TSPL_JOURNAL_MASTER.Source_Doc_No,max(TSPL_JOURNAL_MASTER.Voucher_No) as Voucher_No, max(TSPL_JOURNAL_DETAILS.Reco_Control_Account) as Reco_Control_Account   from TSPL_JOURNAL_MASTER left outer join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No  where TSPL_JOURNAL_DETAILS.Reco_Control_Account = 'B' group by TSPL_JOURNAL_MASTER.Source_Doc_No ) TBL_RECO_CONTROl_ACCOUNT on TBL_RECO_CONTROl_ACCOUNT.Source_Doc_No = TSPL_BANK_BOOK.SOURCEDOC_NO "
            Qry += " LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_RECEIPT_HEADER "
            Qry += " Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_PAYMENT_HEADER "
            Qry += " Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'0' as doctypefororder from TSPL_BANK_TRANSFER "
            Qry += " Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' "
            Qry += " Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType"
            Qry += " where TBL_RECO_CONTROl_ACCOUNT.Reco_Control_Account='B' and sourceDoc_Date >= convert(date,'" & dtFrm.Value & "',103) and sourceDoc_Date <= convert(date,'" & dtTo.Value & "',103)  AND DocMaster.Posted='Y' "

            If txtBank.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
                Qry += " and TSPL_BANK_BOOK.BANK_CODE in (" + clsCommon.GetMulcallString(txtBank.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Qry += " and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If

            Qry += " )as xxx)as final  "
            If chkExcludeProvisionBank.Checked = True Then
                Qry += " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE = final.[Bank Code]  where 2=2 and TSPL_BANK_MASTER.IsProvisionBank =0 "
            End If

            dt = clsDBFuncationality.GetDataTable(Qry)


            If Not (chkSummary.IsChecked) Then

            End If

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

                gvReport.Columns("TotCredAmt").IsVisible = False
                gvReport.Columns("TotDebAmt").IsVisible = False
                FormatGrid()
                gvReport.BestFitColumns()
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
    
        If IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid("Reconcilation Report" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )"), gvReport, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            clsCommon.MyExportToPDF("Reconcilation Report" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )"), gvReport, arrHeader, "Reconcilation Report", True)
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

        gvReport.Columns("DocType").IsVisible = True
        gvReport.Columns("DocType").Width = 121
        gvReport.Columns("DocType").HeaderText = "DocType"

        gvReport.Columns("TransactionType").IsVisible = True
        gvReport.Columns("TransactionType").Width = 121
        gvReport.Columns("TransactionType").HeaderText = "Transaction Type"

        gvReport.Columns("TotCredAmt").IsVisible = True
        gvReport.Columns("TotCredAmt").Width = 121
        gvReport.Columns("TotCredAmt").HeaderText = "Total Credit Amt"

        gvReport.Columns("TotDebAmt").IsVisible = True
        gvReport.Columns("TotDebAmt").Width = 121
        gvReport.Columns("TotDebAmt").HeaderText = "Total Debit Amt"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim SUMDrAmt As New GridViewSummaryItem("Bank Debit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMDrAmt)
        Dim SUMCrAmt As New GridViewSummaryItem("Bank Credit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMCrAmt)
        Dim SUMDiffNet As New GridViewSummaryItem("Diff Net", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMDiffNet)

        Dim item1 As New GridViewSummaryItem("TotCredAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("TotDebAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        '--------------------------------------------------------------------------------------------
       

    End Sub

    Public Sub ExportToExcelGV()
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""

            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
           
            clsCommon.MyExportToExcelGrid("Reconcilation Report", gvReport, arrHeader, "Reconcilation Report")

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBook & "'"))

            transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvReport.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")

    End Sub

    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmReconcilationRpt & "'"))
            
          
            transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
          
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            clsCommon.MyMessageBoxShow(ex.Message)
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
            clsCommon.MyMessageBoxShow("Data Exported successfully")
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmReconcilationRpt & "'"))
           
            transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Reconcilation Report" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )"), gvReport, arrHeader, "Reconcilation Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
