Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class rptSaleRecoNew
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList
    Public boolChecked As Boolean
    Public arrGLAccount As ArrayList
    Public Stocking_Uom As Boolean = False
    '' new filters
    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim strPivotForAddChargeFinalOutersumQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim is_Other_VoucherDrCr As Boolean = False

#End Region

    Private Sub rptSaleRecoNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyLabel9.Visible = True
        cboType.Visible = True
        btnBack.Visible = True
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        LoadReportTypes()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        Document_No = ""
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtLocation.Enabled = True
        txtTransaction.Enabled = True
        txtCustomer.Enabled = True
        Gv1.DataSource = Nothing
        cboType.SelectedValue = "Account Wise"
        RadPageViewPage2.Text = clsCommon.myCstr(cboType.SelectedValue)
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtLocation.arrValueMember = arrLocation
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            cboType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
    End Sub

    Sub LoadReportTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Account Wise")
        dt.Rows.Add("Customer And Account Wise")
        dt.Rows.Add("Detail")
        dt.Rows.Add("Sale Register Wise")
        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptSaleReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Sale Reco:" + cboType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Reco" + cboType.SelectedValue, Gv1, arrHeader, "Sale Reco", True)
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            Dim strMain As String = Nothing
            Dim obj As New clsSaleRegisterParameterType
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                obj.Trans_Type_List = txtTransaction.arrValueMember
            Else
                Dim qry As String
                qry = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
                Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim arrTrans As New ArrayList
                For Each dr As DataRow In dtTrans.Rows
                    arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
                Next
                obj.Trans_Type_List = arrTrans
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                obj.Location_Code_List = txtLocation.arrValueMember
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                obj.Customer_Code_List = txtCustomer.arrValueMember
            End If
            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                obj.Acc_Code_List = txtMultAccountNo.arrValueMember
            End If

            obj.From_Date = fromDate.Value
            obj.To_Date = ToDate.Value
            obj.Cust_Group_Code_List = fndMultiCustGroup.arrValueMember
            obj.ShowMismatchDoc = chkMismatchDoc.Checked
            obj.PickCSASaleFromSalePatti = chkShowCSASaleFromSalePatti.Checked
            strRunQuery = clsPSInvoiceHead.GetReportDataQuerySaleReco(obj)
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                '' richa BHA/01/03/19-000828 show opening for others type 
                strRunQuery = "select xxxxx.*,isnull( JEOther.OpeningDRAmount,0)-isnull( JEOther.OpeningCRAmount ,0)  as OtherOpeningAmount,isnull( JEOther.Other_Amount_Debit,0)  as Other_Amount_Debit, isnull( JEOther.Other_Amount_Credit,0)  as Other_Amount_Credit,(isnull( JEOther.Other_Amount_Debit,0)-isnull(JEOther.Other_Amount_Credit,0)) as OtherNetAmount,(isnull(xxxxx.Net_Trial_Amount,0)+(isnull( JEOther.OpeningDRAmount,0)-isnull( JEOther.OpeningCRAmount ,0))+(isnull( JEOther.Other_Amount_Debit,0)-isnull(JEOther.Other_Amount_Credit,0))) as TotalAmount from (" + Environment.NewLine + _
                "select Account_code,max(Account_Desc) as Account_Desc,sum(Sale_Amount_Debit) as Sale_Amount_Debit,sum(Sale_Amount_Credit) as Sale_Amount_Credit,sum(Net_Sale_Amount) as Net_Sale_Amount,sum(Trial_Amount_Debit) as Trial_Amount_Debit,sum(Trial_Amount_Credit) as Trial_Amount_Credit,sum(Net_Trial_Amount) as Net_Trial_Amount,sum(Net_Sale_Amount-Net_Trial_Amount) as DiffAmount " + Environment.NewLine + _
                "from (" + strRunQuery + ")Final group by Account_code" + Environment.NewLine + _
                ")xxxxx  " + Environment.NewLine + _
                "left outer join " & Environment.NewLine & _
                " (select z.Account_code,sum(z.Other_Amount_Debit) as Other_Amount_Debit,sum(z.Other_Amount_Credit) as Other_Amount_Credit,sum(z.OpeningDRAmount) as OpeningDRAmount,sum(z.OpeningCRAmount) as OpeningCRAmount from ( " & Environment.NewLine & _
                " select TSPL_JOURNAL_DETAILS.Account_code,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as Other_Amount_Debit ," & Environment.NewLine & _
                " sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as Other_Amount_Credit ,0 as OpeningDRAmount,0 as OpeningCRAmount" & Environment.NewLine & _
                " from TSPL_JOURNAL_DETAILS" & Environment.NewLine & _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " & Environment.NewLine & _
                " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'S' ) and TSPL_JOURNAL_MASTER.Authorized='A' " & Environment.NewLine & _
                " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) >= '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' " & Environment.NewLine & _
                " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) <= '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' group by TSPL_JOURNAL_DETAILS.Account_code" & Environment.NewLine & _
                " Union All" & Environment.NewLine & _
                " select TSPL_JOURNAL_DETAILS.Account_code,0 as Other_Amount_Debit,0 as Other_Amount_Credit,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as OpeningDRAmount ," & Environment.NewLine & _
                " sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as OpeningCRAmount " & Environment.NewLine & _
                " from TSPL_JOURNAL_DETAILS " & Environment.NewLine & _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " & Environment.NewLine & _
                " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'S' ) and TSPL_JOURNAL_MASTER.Authorized='A' " & Environment.NewLine & _
                " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) < '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' " & Environment.NewLine & _
                " group by TSPL_JOURNAL_DETAILS.Account_code)z group by z.Account_code)" & Environment.NewLine & _
                "  JEOther on  JEOther.Account_code=xxxxx.Account_code"

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal Then
                strRunQuery = "select  max(Cust_Group_Code) as Cust_Group_Code,max(Cust_Group_Desc) as Group_Desc,[Customer Code],max(Customer_Name) as Customer_Name, Account_code,max(Account_Desc) as Account_Desc,sum(Sale_Amount_Debit) as Sale_Amount_Debit,sum(Sale_Amount_Credit) as Sale_Amount_Credit,sum(Net_Sale_Amount) as Net_Sale_Amount,sum(Trial_Amount_Debit) as Trial_Amount_Debit,sum(Trial_Amount_Credit) as Trial_Amount_Credit,sum(Net_Trial_Amount) as Net_Trial_Amount,sum(Net_Sale_Amount-Net_Trial_Amount) as DiffAmount " + Environment.NewLine + _
                "from (" + strRunQuery + ")Final group by [Customer Code],Account_code "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                    '' query added by Panch Raj against ticket no: KDI/06/06/18-000345 ,richa BHA/01/03/19-000828 add Reference Doc No column
                If is_Other_VoucherDrCr Then
                    strRunQuery = " select TSPL_JOURNAL_MASTER.Voucher_No as [Document Code],convert(varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as [Voucher Date],TSPL_JOURNAL_MASTER.Voucher_Desc as [Voucher Desc],TSPL_JOURNAL_MASTER.Source_Code as [Source Code],TSPL_JOURNAL_MASTER.Source_Desc as [Source Desc]," & _
                        " case when isnull(TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' )<>'' then TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'' )<>'' then TSPL_Customer_Invoice_Head.Against_Sale_Return_No when isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrapReturn  else '' end as [Reference Doc No], " & _
                                  " TSPL_JOURNAL_MASTER.Source_Doc_No as [Source Doc No],convert(varchar,TSPL_JOURNAL_MASTER.Source_Doc_Date,103) as [Source Doc Date],TSPL_JOURNAL_DETAILS.Account_code as [Account Code],(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>=0  then 1 else 0 end   ) as [Other Amount Debit] ," & _
                                  " (TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as [Other Amount Credit] " & _
                                  " from TSPL_JOURNAL_DETAILS " & _
                                  " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " & _
                                  " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No =TSPL_JOURNAL_MASTER.Source_Doc_No  " & _
                                  " where isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'S' and TSPL_JOURNAL_MASTER.Authorized='A' " & _
                                  " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' " & _
                                  " and TSPL_JOURNAL_DETAILS.Account_code in (" & clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember) & ")"
                    End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Sale Register Wise") = CompairStringResult.Equal Then
                Dim qry As String = ReturnSaleRegQuery()
                qry = "select [Document No] as [Document Code],[Customer Code],max([Customer Name]) as Customer_Name,max([Document_date]) as [Document Date],max([Trans Type]) as Trans_Type,sum([Sale Amount]) as [Sale Amount] from (" & qry & ") SaleReg group by [Document No],[Customer Code]"
                strRunQuery = " select Sale.[Document Code],Sale.[Document Date],Sale.Trans_Type,Sale.[Customer Code],Sale.Customer_Name,Reco.Cust_Group_Code,Reco.Cust_Group_Desc,Reco.JE_Source_Doc_No,Reco.Voucher_No,Reco.Voucher_Date," & _
                              " Reco.Sale_Amount_Debit,Reco.Sale_Amount_Credit,Reco.Net_Sale_Amount,Sale.[Sale Amount] as [Sale Register Amount],Reco.Trial_Amount_Debit,Reco.Trial_Amount_Credit,Reco.Net_Trial_Amount,Reco.DiffAmount,round((abs(Sale.[Sale Amount])-abs(Reco.Net_Trial_Amount)),2) as [Diff Sale Reg Trial] from (" & qry & ") Sale " & _
                              " left join (select Reco.[Document Code],max(Reco.[Document Date]) as [Document Date],max(Reco.Trans_Type) as Trans_Type,Reco.[Customer Code],max(Reco.Customer_Name) as Customer_Name,max(Reco.Cust_Group_Code) as Cust_Group_Code,max(Reco.Cust_Group_Desc) as Cust_Group_Desc,max(Reco.JE_Source_Doc_No) as JE_Source_Doc_No,max(Reco.Voucher_No) as Voucher_No,max(Reco.Voucher_Date) as Voucher_Date," & _
                              " sum(Reco.Sale_Amount_Debit) as Sale_Amount_Debit,sum(Reco.Sale_Amount_Credit) as Sale_Amount_Credit,sum(Reco.Net_Sale_Amount) as Net_Sale_Amount,sum(Reco.Trial_Amount_Debit) as Trial_Amount_Debit,sum(Reco.Trial_Amount_Credit) as Trial_Amount_Credit,sum(Reco.Net_Trial_Amount) as Net_Trial_Amount,sum(Reco.DiffAmount) as DiffAmount " & _
                              " from (" & strRunQuery & ") Reco group by Reco.[Document Code],Reco.[Customer Code]) Reco on Sale.[Document Code]=Reco.[Document Code] "
            Else
                Throw New Exception("Wrong Report type")
            End If


            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            RadPageViewPage2.Text = cboType.Text
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            Gv1.Tag = cboType.SelectedValue
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)

                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Sub EnableDisableAllControl(ByVal val As Boolean)
        txtTransaction.Enabled = val
        txtLocation.Enabled = val
        txtCustomer.Enabled = val
        txtMultAccountNo.Enabled = val
        fndMultiAccSet.Enabled = val
        fndMultiCustGroup.Enabled = val
        cboType.Enabled = val
        fromDate.Enabled = val
        ToDate.Enabled = val
        chkMismatchDoc.Enabled = val

    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).BestFit()
        Next
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("Sale_Amount_Debit").IsVisible = True
            Gv1.Columns("Sale_Amount_Debit").Width = 120
            Gv1.Columns("Sale_Amount_Debit").HeaderText = "Sale Amount Debit"

            Gv1.Columns("Sale_Amount_Credit").IsVisible = True
            Gv1.Columns("Sale_Amount_Credit").Width = 120
            Gv1.Columns("Sale_Amount_Credit").HeaderText = "Sale Amount Credit"

            Gv1.Columns("Net_Sale_Amount").IsVisible = True
            Gv1.Columns("Net_Sale_Amount").Width = 120
            Gv1.Columns("Net_Sale_Amount").HeaderText = "Net Sale Amount"

            Gv1.Columns("Trial_Amount_Debit").IsVisible = True
            Gv1.Columns("Trial_Amount_Debit").Width = 120
            Gv1.Columns("Trial_Amount_Debit").HeaderText = "Trial Amount Debit"

            Gv1.Columns("Trial_Amount_Credit").IsVisible = True
            Gv1.Columns("Trial_Amount_Credit").Width = 120
            Gv1.Columns("Trial_Amount_Credit").HeaderText = "Trial Amount Credit"

            Gv1.Columns("Net_Trial_Amount").IsVisible = True
            Gv1.Columns("Net_Trial_Amount").Width = 120
            Gv1.Columns("Net_Trial_Amount").HeaderText = "Net Trial Amount"

            Gv1.Columns("OtherOpeningAmount").IsVisible = True
            Gv1.Columns("OtherOpeningAmount").Width = 120
            Gv1.Columns("OtherOpeningAmount").HeaderText = "Other Opening Amount"

            Gv1.Columns("Other_Amount_Debit").IsVisible = True
            Gv1.Columns("Other_Amount_Debit").Width = 120
            Gv1.Columns("Other_Amount_Debit").HeaderText = "Other Amount Debit"

            Gv1.Columns("Other_Amount_Credit").IsVisible = True
            Gv1.Columns("Other_Amount_Credit").Width = 120
            Gv1.Columns("Other_Amount_Credit").HeaderText = "Other Amount Credit"

            Gv1.Columns("OtherNetAmount").IsVisible = True
            Gv1.Columns("OtherNetAmount").Width = 120
            Gv1.Columns("OtherNetAmount").HeaderText = "Other Net Amt"

            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

            Gv1.Columns("TotalAmount").IsVisible = True
            Gv1.Columns("TotalAmount").Width = 120
            Gv1.Columns("TotalAmount").HeaderText = "Total Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("Sale_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Sale_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Net_Sale_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Trial_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Trial_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Net_Trial_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherOpeningAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Other_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Other_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("TotalAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("Cust_Group_Code").IsVisible = True
            Gv1.Columns("Cust_Group_Code").Width = 120
            Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 120
            Gv1.Columns("Group_Desc").HeaderText = "Customer Group"


            Gv1.Columns("Customer Code").IsVisible = True
            Gv1.Columns("Customer Code").Width = 120
            Gv1.Columns("Customer Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 120
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("Sale_Amount_Debit").IsVisible = True
            Gv1.Columns("Sale_Amount_Debit").Width = 120
            Gv1.Columns("Sale_Amount_Debit").HeaderText = "Sale Amount Debit"

            Gv1.Columns("Sale_Amount_Credit").IsVisible = True
            Gv1.Columns("Sale_Amount_Credit").Width = 120
            Gv1.Columns("Sale_Amount_Credit").HeaderText = "Sale Amount Credit"

            Gv1.Columns("Net_Sale_Amount").IsVisible = True
            Gv1.Columns("Net_Sale_Amount").Width = 120
            Gv1.Columns("Net_Sale_Amount").HeaderText = "Net Sale Amount"

            Gv1.Columns("Trial_Amount_Debit").IsVisible = True
            Gv1.Columns("Trial_Amount_Debit").Width = 120
            Gv1.Columns("Trial_Amount_Debit").HeaderText = "Trial Amount Debit"

            Gv1.Columns("Trial_Amount_Credit").IsVisible = True
            Gv1.Columns("Trial_Amount_Credit").Width = 120
            Gv1.Columns("Trial_Amount_Credit").HeaderText = "Trial Amount Credit"

            Gv1.Columns("Net_Trial_Amount").IsVisible = True
            Gv1.Columns("Net_Trial_Amount").Width = 120
            Gv1.Columns("Net_Trial_Amount").HeaderText = "Net Trial Amount"


            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("Sale_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Sale_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Net_Sale_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Trial_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Trial_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Net_Trial_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            If is_Other_VoucherDrCr = False Then
                'Gv1.Columns("Cust_Group_Code").IsVisible = True
                'Gv1.Columns("Cust_Group_Code").Width = 120
                'Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

                'Gv1.Columns("Group_Desc").IsVisible = True
                'Gv1.Columns("Group_Desc").Width = 120
                'Gv1.Columns("Group_Desc").HeaderText = "Customer Group"


                Gv1.Columns("Customer Code").IsVisible = True
                Gv1.Columns("Customer Code").Width = 120
                Gv1.Columns("Customer Code").HeaderText = "Customer Code"

                Gv1.Columns("Customer_Name").IsVisible = True
                Gv1.Columns("Customer_Name").Width = 120
                Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

                Gv1.Columns("Account_code").IsVisible = True
                Gv1.Columns("Account_code").Width = 120
                Gv1.Columns("Account_code").HeaderText = "Account code"

                Gv1.Columns("Account_Desc").IsVisible = True
                Gv1.Columns("Account_Desc").Width = 120
                Gv1.Columns("Account_Desc").HeaderText = "Account"

                Gv1.Columns("Sale_Amount_Debit").IsVisible = True
                Gv1.Columns("Sale_Amount_Debit").Width = 120
                Gv1.Columns("Sale_Amount_Debit").HeaderText = "Sale Amount Debit"

                Gv1.Columns("Sale_Amount_Credit").IsVisible = True
                Gv1.Columns("Sale_Amount_Credit").Width = 120
                Gv1.Columns("Sale_Amount_Credit").HeaderText = "Sale Amount Credit"

                Gv1.Columns("Net_Sale_Amount").IsVisible = True
                Gv1.Columns("Net_Sale_Amount").Width = 120
                Gv1.Columns("Net_Sale_Amount").HeaderText = "Net Sale Amount"

                Gv1.Columns("Trial_Amount_Debit").IsVisible = True
                Gv1.Columns("Trial_Amount_Debit").Width = 120
                Gv1.Columns("Trial_Amount_Debit").HeaderText = "Trial Amount Debit"

                Gv1.Columns("Trial_Amount_Credit").IsVisible = True
                Gv1.Columns("Trial_Amount_Credit").Width = 120
                Gv1.Columns("Trial_Amount_Credit").HeaderText = "Trial Amount Credit"

                Gv1.Columns("Net_Trial_Amount").IsVisible = True
                Gv1.Columns("Net_Trial_Amount").Width = 120
                Gv1.Columns("Net_Trial_Amount").HeaderText = "Net Trial Amount"


                Gv1.Columns("DiffAmount").IsVisible = True
                Gv1.Columns("DiffAmount").Width = 120
                Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"


                Gv1.Columns("Document Code").IsVisible = True
                Gv1.Columns("Document Code").Width = 120
                Gv1.Columns("Document Code").HeaderText = "Document No"

                Gv1.Columns("Document Date").IsVisible = True
                Gv1.Columns("Document Date").Width = 120
                Gv1.Columns("Document Date").HeaderText = "Document Date"

                Gv1.Columns("Trans_Type").IsVisible = True
                Gv1.Columns("Trans_Type").Width = 120
                Gv1.Columns("Trans_Type").HeaderText = "Trans Type"

                'Gv1.Columns("DocumentType").IsVisible = True
                'Gv1.Columns("DocumentType").Width = 120
                'Gv1.Columns("DocumentType").HeaderText = "Document Type"

                Gv1.Columns("Voucher_No").IsVisible = True
                Gv1.Columns("Voucher_No").Width = 120
                Gv1.Columns("Voucher_No").HeaderText = "Voucher No"

                Gv1.Columns("Voucher_Date").IsVisible = True
                Gv1.Columns("Voucher_Date").Width = 120
                Gv1.Columns("Voucher_Date").HeaderText = "Voucher Date"

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item As New GridViewSummaryItem("Sale_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                item = New GridViewSummaryItem("Sale_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                item = New GridViewSummaryItem("Net_Sale_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                item = New GridViewSummaryItem("Trial_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                item = New GridViewSummaryItem("Trial_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                item = New GridViewSummaryItem("Net_Trial_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else
                Gv1.Columns("Document Code").IsVisible = True
                Gv1.Columns("Document Code").Width = 120
                Gv1.Columns("Document Code").HeaderText = "Document Code"

                Gv1.Columns("Voucher Date").IsVisible = True
                Gv1.Columns("Voucher Date").Width = 120
                Gv1.Columns("Voucher Date").HeaderText = "Voucher Date"

                Gv1.Columns("Voucher Desc").IsVisible = True
                Gv1.Columns("Voucher Desc").Width = 120
                Gv1.Columns("Voucher Desc").HeaderText = "Voucher Desc"

                Gv1.Columns("Source Code").IsVisible = True
                Gv1.Columns("Source Code").Width = 120
                Gv1.Columns("Source Code").HeaderText = "Source Code"

                Gv1.Columns("Source Desc").IsVisible = True
                Gv1.Columns("Source Desc").Width = 120
                Gv1.Columns("Source Desc").HeaderText = "Source Desc"

                Gv1.Columns("Reference Doc No").IsVisible = True
                Gv1.Columns("Reference Doc No").Width = 120
                Gv1.Columns("Reference Doc No").HeaderText = "Reference Doc No"

                Gv1.Columns("Source Doc No").IsVisible = True
                Gv1.Columns("Source Doc No").Width = 120
                Gv1.Columns("Source Doc No").HeaderText = "Source Doc No"

                Gv1.Columns("Source Doc Date").IsVisible = True
                Gv1.Columns("Source Doc Date").Width = 120
                Gv1.Columns("Source Doc Date").HeaderText = "Source Doc Date"

                Gv1.Columns("Account Code").IsVisible = True
                Gv1.Columns("Account Code").Width = 120
                Gv1.Columns("Account Code").HeaderText = "Account Code"

                Gv1.Columns("Other Amount Debit").IsVisible = True
                Gv1.Columns("Other Amount Debit").Width = 120
                Gv1.Columns("Other Amount Debit").HeaderText = "Other Amount Debit"

                Gv1.Columns("Other Amount Credit").IsVisible = True
                Gv1.Columns("Other Amount Credit").Width = 120
                Gv1.Columns("Other Amount Credit").HeaderText = "Other Amount Credit"


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item As New GridViewSummaryItem("Other Amount Debit", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                item = New GridViewSummaryItem("Other Amount Credit", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
           
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Sale Register Wise") = CompairStringResult.Equal Then

            Gv1.Columns("Customer Code").IsVisible = True
            Gv1.Columns("Customer Code").Width = 120
            Gv1.Columns("Customer Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 120
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"


            Gv1.Columns("Sale_Amount_Debit").IsVisible = True
            Gv1.Columns("Sale_Amount_Debit").Width = 120
            Gv1.Columns("Sale_Amount_Debit").HeaderText = "Sale Amount Debit"

            Gv1.Columns("Sale_Amount_Credit").IsVisible = True
            Gv1.Columns("Sale_Amount_Credit").Width = 120
            Gv1.Columns("Sale_Amount_Credit").HeaderText = "Sale Amount Credit"

            Gv1.Columns("Net_Sale_Amount").IsVisible = True
            Gv1.Columns("Net_Sale_Amount").Width = 120
            Gv1.Columns("Net_Sale_Amount").HeaderText = "Net Sale Amount"

            Gv1.Columns("Sale Register Amount").IsVisible = True
            Gv1.Columns("Sale Register Amount").Width = 120
            Gv1.Columns("Sale Register Amount").HeaderText = "Sale Register Amount"

            Gv1.Columns("Trial_Amount_Debit").IsVisible = True
            Gv1.Columns("Trial_Amount_Debit").Width = 120
            Gv1.Columns("Trial_Amount_Debit").HeaderText = "Trial Amount Debit"

            Gv1.Columns("Trial_Amount_Credit").IsVisible = True
            Gv1.Columns("Trial_Amount_Credit").Width = 120
            Gv1.Columns("Trial_Amount_Credit").HeaderText = "Trial Amount Credit"

            Gv1.Columns("Net_Trial_Amount").IsVisible = True
            Gv1.Columns("Net_Trial_Amount").Width = 120
            Gv1.Columns("Net_Trial_Amount").HeaderText = "Net Trial Amount"


            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

            Gv1.Columns("Diff Sale Reg Trial").IsVisible = True
            Gv1.Columns("Diff Sale Reg Trial").Width = 120
            Gv1.Columns("Diff Sale Reg Trial").HeaderText = "Diff Sale Reg Trial"


            Gv1.Columns("Document Code").IsVisible = True
            Gv1.Columns("Document Code").Width = 120
            Gv1.Columns("Document Code").HeaderText = "Document No"

            Gv1.Columns("Document Date").IsVisible = True
            Gv1.Columns("Document Date").Width = 120
            Gv1.Columns("Document Date").HeaderText = "Document Date"

            Gv1.Columns("Trans_Type").IsVisible = True
            Gv1.Columns("Trans_Type").Width = 120
            Gv1.Columns("Trans_Type").HeaderText = "Trans Type"

            'Gv1.Columns("DocumentType").IsVisible = True
            'Gv1.Columns("DocumentType").Width = 120
            'Gv1.Columns("DocumentType").HeaderText = "Document Type"

            Gv1.Columns("Voucher_No").IsVisible = True
            Gv1.Columns("Voucher_No").Width = 120
            Gv1.Columns("Voucher_No").HeaderText = "Voucher No"

            Gv1.Columns("Voucher_Date").IsVisible = True
            Gv1.Columns("Voucher_Date").Width = 120
            Gv1.Columns("Voucher_Date").HeaderText = "Voucher Date"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("Sale_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Sale_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Net_Sale_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Sale Register Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Trial_Amount_Debit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Trial_Amount_Credit", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Net_Trial_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("Diff Sale Reg Trial", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        EnableDisableAllControl(True)
        Gv1.DataSource = Nothing
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Sale Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptSaleRecoNew_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual')   "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        ' Dim Dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MODULE_PERMISSION")
        Dim Str As String = String.Empty

        Dim qry As String = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)

    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub

    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeZeroQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        If clsCommon.myLen(qry) > 0 Then
            qry = "select * from( " & qry & ") as t1 where Add_Charge_Code1 not in ('AC_')"
        End If
        Return qry
    End Function


    Private Sub txtMultAccountNo__My_Click(sender As Object, e As EventArgs) Handles txtMultAccountNo._My_Click
        Dim qry As String = " select  Account_Code AS Code,Description as [Name] from TSPL_GL_ACCOUNTS "
        txtMultAccountNo.arrValueMember = clsCommon.ShowMultipleSelectForm("GLMulSel", qry, "Code", "Name", txtMultAccountNo.arrValueMember, txtMultAccountNo.arrDispalyMember)
    End Sub

    Private Sub fndMultiAccSet__My_Click(sender As Object, e As EventArgs) Handles fndMultiAccSet._My_Click
        Dim qry As String = " select Acct_Set_Code as [Code],Acct_Set_Desc as Name,Payable_Account as [Payable Account],Discount_Account as [Discount Account],Advance_Account as [Advance Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CURRENCY_CODE as [Currency Code],EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] from TSPL_VENDOR_ACCOUNT_SET "
        fndMultiAccSet.arrValueMember = clsCommon.ShowMultipleSelectForm("VenAccMulSel", qry, "Code", "Name", fndMultiAccSet.arrValueMember, fndMultiAccSet.arrDispalyMember)
    End Sub

    Private Sub fndMultiVendorGroup__My_Click(sender As Object, e As EventArgs) Handles fndMultiCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        fndMultiCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", fndMultiCustGroup.arrValueMember, fndMultiCustGroup.arrDispalyMember)
    End Sub

    Sub DrillDown()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Account Wise") Then
                    arrBack.Add("Account Wise")
                End If
                cboType.SelectedValue = "Customer And Account Wise"
                arrGLAccount = New ArrayList()
                arrGLAccount = txtMultAccountNo.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Account_code").Value))
                txtMultAccountNo.arrValueMember = tmp
                If (Gv1.CurrentColumn Is Gv1.Columns("Other_Amount_Debit") AndAlso clsCommon.myCdbl(Gv1.CurrentRow.Cells("Other_Amount_Debit").Value) <> 0) OrElse (Gv1.CurrentColumn Is Gv1.Columns("Other_Amount_Credit") AndAlso clsCommon.myCdbl(Gv1.CurrentRow.Cells("Other_Amount_Credit").Value) <> 0) Then
                    is_Other_VoucherDrCr = True
                    cboType.SelectedValue = "Detail"                    
                ElseIf Gv1.CurrentColumn Is Gv1.Columns("DiffAmount") AndAlso clsCommon.myCdbl(Gv1.CurrentRow.Cells("DiffAmount").Value) <> 0 Then
                Else
                    chkMismatchDoc.Tag = Nothing
                End If

                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Customer And Account Wise") Then
                    arrBack.Add("Customer And Account Wise")
                End If
                cboType.SelectedValue = "Detail"
                arrCustomer = New ArrayList()
                arrCustomer = txtCustomer.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value))
                txtCustomer.arrValueMember = tmp
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                ''Reached at last Node '' done by richa agarwal 6 Apr,2018 tciket no KDI/05/04/18-000192
                Dim strTransType As String = If(is_Other_VoucherDrCr = False, clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans_Type").Value), "JE")
                Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document Code").Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "Fresh Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, strTransCode)
                        Case "Product Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleInvoiceProductSale, strTransCode)
                        Case "Export Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, strTransCode)
                        Case "MCC Sale Farmer"
                            ' clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialFarmer, strTransCode)
                        Case "MCC Sale Return Farmer"
                            '  clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer, strTransCode)
                        Case "MCC Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strTransCode)
                        Case "CSA Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                        Case "Fresh Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strTransCode)
                        Case "Product Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, strTransCode)
                        Case "Export Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strTransCode)
                        Case "CSA Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                        Case "MCC Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, strTransCode)
                        Case "JE"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strTransCode)
                        Case "Tanker Dispatch Return"
                            '   clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MCCDispatchReturn, strTransCode)
                        Case "Bulk Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strTransCode)
                        Case "Bulk Sale Trade"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strTransCode)
                        Case "Bulk Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, strTransCode)
                            'Case "Bulk Sale Return Trade"
                            '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, strTransCode)
                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "Transfer Return"
                            ' clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, strTransCode)
                        Case "Misc Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strTransCode)
                        Case "MCC Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                        Case "Merchant Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strTransCode)
                        Case "Misc Sale Return"
                            'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSaleRetrun, strTransCode)
                    End Select

                End If
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                ''Reached at First Node 
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Account Wise") Then
                arrBack.Remove("Account Wise")
                cboType.SelectedValue = "Account Wise"
                txtMultAccountNo.arrValueMember = arrGLAccount
                If clsCommon.CompairString(clsCommon.myCstr(chkMismatchDoc.Tag), "D") = CompairStringResult.Equal Then
                    chkMismatchDoc.Checked = boolChecked
                    chkMismatchDoc.Tag = Nothing
                End If
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal AndAlso (arrBack.Contains("Customer And Account Wise") OrElse (arrBack.Contains("Account Wise") AndAlso is_Other_VoucherDrCr = True)) Then
                If arrBack.Contains("Customer And Account Wise") Then
                    arrBack.Remove("Customer And Account Wise")
                    cboType.SelectedValue = "Customer And Account Wise"
                    txtCustomer.arrValueMember = arrCustomer
                Else
                    arrBack.Remove("Account Wise")
                    cboType.SelectedValue = "Account Wise"
                    is_Other_VoucherDrCr = False
                    txtMultAccountNo.arrValueMember = arrGLAccount
                End If
                
                Print(Exporter.Refresh)
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Function ReturnSaleRegQuery() As String
        Dim qryList As ArrayList
        Dim obj As New clsSaleRegisterParameterType
        obj.From_Date = fromDate.Value
        obj.To_Date = ToDate.Value
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            obj.Trans_Type_List = txtTransaction.arrValueMember
        Else
            Dim qry As String
            qry = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
            Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrTrans As New ArrayList
            For Each dr As DataRow In dtTrans.Rows
                arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
            Next
            obj.Trans_Type_List = arrTrans
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            obj.Location_Code_List = txtLocation.arrValueMember
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            obj.Customer_Code_List = txtCustomer.arrValueMember
        End If
        If arrCustGroup IsNot Nothing AndAlso arrCustGroup.Count > 0 Then
            obj.Cust_Group_Code_List = fndMultiCustGroup.arrValueMember
        End If
        If clsCommon.myLen(Document_No) > 0 Then
            obj.Document_Code = Document_No
        End If
        obj.Other_Cond = " and xx.Status=1  "
        obj.PickCSASaleFromSalePatti = chkShowCSASaleFromSalePatti.Checked
        qryList = clsPSInvoiceHead.ReturnQuery(obj)
        Dim strMCCMaterial As String = qryList(0)
        strPivotForFinalOuterQuery = qryList(1)
        ''richa agarwal 13 Mar,2019 to show only posted documents BHA/13/03/19-000841
        If clsCommon.myLen(obj.Other_Cond) > 0 Then
            strMCCMaterial += obj.Other_Cond
        End If
        'strPivotForAddChargeFinalOutersumQuery = qryList(2)        
        Return strMCCMaterial
    End Function

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Sale Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
          
            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Sale Reco", Gv1, arrHeader, "Sale Reco", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class


