Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'================Create by Sanjeet(11/01/2018) Gupta=========
'Done by priti GKD/29/05/18-000139
Public Class FrmSaleVsReceipReport
    Inherits FrmMainTranScreen
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Dim NopreviousDaysInSaleVSReceipt As Double = 0
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isQuickExportFlag
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    ' done by priti GKD/15/05/18-000132
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = Me.Form_ID
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        SelFromDate = String.Empty
        SelToDate = String.Empty
        ''done by richa agarwal BHA/15/01/19-000779 
        If NopreviousDaysInSaleVSReceipt = 2 Or (NopreviousDaysInSaleVSReceipt > 2 And NopreviousDaysInSaleVSReceipt <= 5) Then
            For i As Integer = NopreviousDaysInSaleVSReceipt - 1 To 0 Step -1
                SelFromDate += "[" + clsCommon.myCstr(txtToDate.Value.AddDays(-i).ToShortDateString()) + "],"
                SelToDate += "[" + clsCommon.myCstr(txtToDate.Value.AddDays(-i).ToShortDateString()) + " Receipt],"
            Next
        Else
            For i As Integer = 1 To 0 Step -1
                SelFromDate += "[" + clsCommon.myCstr(txtToDate.Value.AddDays(-i).ToShortDateString()) + "],"
                SelToDate += "[" + clsCommon.myCstr(txtToDate.Value.AddDays(-i).ToShortDateString()) + " Receipt],"
            Next
        End If

        SelFromDate = SelFromDate.Substring(0, SelFromDate.Length - 1)
        SelToDate = SelToDate.Substring(0, SelToDate.Length - 1)
        ''------------------------end of ticket BHA/15/01/19-000779 

        'Dim strFromDate As String = clsCommon.GetPrintDate(txtToDate.Value.AddDays(-1), "dd/MMM/yyyy")
        Dim strFromDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        'SelFromDate = clsCommon.myCstr(txtToDate.Value.AddDays(-1).ToShortDateString())
        'SelToDate = clsCommon.myCstr(txtToDate.Value.ToShortDateString())
        Dim StrQuery As String = Nothing
        Dim ACodeFilter As String = String.Empty
        Dim strcustomerfilter As String = String.Empty
        Dim strFIlterCheck As String = String.Empty
        Dim CheckCustomer As String = String.Empty
        Dim BaseQry As String = String.Empty
        Dim BaseQryOPENINGINCASEOFMIS As String = String.Empty
        Dim strCustCategoryMappInUserMaster As String = String.Empty

        Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
        If chkCustCategoryMappInUserMaster = True Then
            strCustCategoryMappInUserMaster = " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
        End If

        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            strFIlterCheck += "and ACode in (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
            strcustomerfilter = clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember)
        End If
        BaseQry = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, False, clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"), False, False, False)
        BaseQryOPENINGINCASEOFMIS = clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, True, clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"), False, False, False)

        If clsCommon.CompairString(clsCommon.myCstr(ddlActiveInactive.SelectedValue), "Both") <> CompairStringResult.Equal Then
            CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='" + ddlActiveInactive.SelectedValue + "'"

        End If
        ''richa agarwal add condition into receipt header that it will not include apply documents   and tspl_receipt_header.Receipt_Type <>'A' BHA/10/01/19-000777
        StrQuery = "select tspl_customer_master.state as State, " & Environment.NewLine & _
                "tspl_state_master.state_name as stateName, " & Environment.NewLine & _
                "case when tspl_customer_master.status='N' then 'Active' else 'InActive' end as [active/inactive], " & Environment.NewLine & _
                "tspl_customer_master.cust_code as [party code], " & Environment.NewLine & _
                "tspl_customer_master.Customer_Name as [party name], " & Environment.NewLine & _
                " tspl_customer_master.zone_code as zone, " & Environment.NewLine & _
                "tspl_employee_master.Emp_Name as [s.r. name] , " & Environment.NewLine & _
                "tspl_customer_master.Credit_Limit as [Credit Limit], " & Environment.NewLine & _
                "outstanding_amt.OpngBal,outstanding_amt.DrAmt,outstanding_amt.CrAmt, " & Environment.NewLine & _
                "customer_invoice_detail.*,receipt_detail.* ," & Environment.NewLine & _
                "outstanding_amt.DrAmt-outstanding_amt.CrAmt as [Period Closing], " & Environment.NewLine & _
                "outstanding_amt.BalAmt  as Outstanding,outstanding_amt.BalAmt-security as [Outstanding Over Security] " & Environment.NewLine & _
                " from (select tspl_customer_invoice_head.Customer_Code , " & Environment.NewLine & _
                "convert(varchar(15),tspl_customer_invoice_head.document_date,103) as document_date,  " & Environment.NewLine & _
                "(select sum(isnull(receipt_amount,0)) as s_amt from tspl_receipt_header where  " & Environment.NewLine & _
                "tspl_receipt_header.cust_code=tspl_customer_invoice_head.customer_code and isnull(securitydeposit,'n')='y' and tspl_receipt_header.Receipt_Type <>'A') as security," & Environment.NewLine & _
                "isnull(tspl_customer_invoice_head.document_total,0) as document_total from tspl_customer_invoice_head" + Environment.NewLine & _
                " where  Document_Type <>'C' and convert(date,tspl_customer_invoice_head.document_date,103)>='" + strFromDate + "' and " & Environment.NewLine & _
                "convert(date,tspl_customer_invoice_head.document_date,103)<='" + strToDate + "' " + Environment.NewLine
      
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            StrQuery += " and tspl_customer_invoice_head.customer_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        ''richa agarwal add condition into receipt header that it will not include apply documents BHA/10/01/19-000777 , refund type of receipt should be in -ve (case when tspl_receipt_header.Receipt_Type ='F' then -1 else 1 end) 
        StrQuery += " ) as customer_invoice " + Environment.NewLine & _
                " pivot (sum(document_total) for document_date in(" + SelFromDate + ")) as customer_invoice_detail " + Environment.NewLine & _
                " left outer join " + Environment.NewLine & _
                " (select * from (select convert(varchar(15),tspl_receipt_header.receipt_date,103) + ' Receipt'  as receipt_date,(case when tspl_receipt_header.Receipt_Type ='F' then -1 else 1 end) *  isnull(tspl_receipt_header.receipt_amount,0) as receipt_amount,tspl_receipt_header.cust_code " + Environment.NewLine & _
                " from tspl_receipt_header where convert(date,receipt_date,103) > ='" + strFromDate + "' and  convert(date,receipt_date,103) <='" + strToDate + "' and posted='y' and tspl_receipt_header.Receipt_Type <>'A' " + Environment.NewLine
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            StrQuery += " and tspl_receipt_header.cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        StrQuery += " Union all " + Environment.NewLine
        StrQuery += "select  convert(varchar(15),TSPL_Customer_Invoice_Head.Document_Date,103) + ' Receipt'  as receipt_date,Document_Total as receipt_amount, " & Environment.NewLine & _
                "TSPL_Customer_Invoice_Head.Customer_Code as  cust_code  from TSPL_Customer_Invoice_Head where Document_Type='C' and " & Environment.NewLine & _
                "convert(date,Document_Date,103) > ='" + strFromDate + "' and  convert(date,Document_Date,103) <='" + strToDate + "' and Status=1 " + Environment.NewLine
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            StrQuery += " and TSPL_Customer_Invoice_Head.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If
        StrQuery += " ) as receipt_data " + Environment.NewLine & _
                    " pivot(sum(receipt_amount) for receipt_date in (" + SelToDate + ")) as pvt) receipt_detail on receipt_detail.cust_code=customer_invoice_detail.Customer_Code " + Environment.NewLine & _
                    " right outer join " + Environment.NewLine & _
                    " ( Select ACode, SUM(convert(decimal(18,2),OpngBal)) as OpngBal, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, " + Environment.NewLine & _
                    "SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales)) as [Sales], -1* SUM(convert(decimal(18,2),CrNote)) as CrNote, " + Environment.NewLine & _
                    "( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " + Environment.NewLine & _
                    "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, " + Environment.NewLine & _
                    "'' as CurrencyCode, 1 as ConvRate,  " + Environment.NewLine & _
                    "SUM(DrAmt*final.ConvRate)-SUM(CrAmt) as OpngBal,  " + Environment.NewLine & _
                     "0 as DrAmt, " + Environment.NewLine & _
                    "0 as CrAmt,  " + Environment.NewLine & _
                    "0  as [Sales],  " + Environment.NewLine & _
                    "0 as CollectionRefund, " + Environment.NewLine & _
                    "0  as DrNote, " + Environment.NewLine & _
                    "0 as CrNote, " + Environment.NewLine & _
                    "MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc, " + Environment.NewLine & _
                    "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " & BaseQryOPENINGINCASEOFMIS & " ) Final " + Environment.NewLine & _
                    "left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                    "LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                    "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  " + Environment.NewLine & _
                    "LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code  " + Environment.NewLine & _
                    "where  CONVERT(DATE,final.DocDate,103) <  '" + strFromDate + "' AND LEN(ACode)>0   "

        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            StrQuery += " and ACode in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If


        StrQuery += "  GROUP BY ACode " + Environment.NewLine & _
                    " Union All   " + Environment.NewLine & _
                     "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, " + Environment.NewLine & _
                    "'' as CurrencyCode, null as ConvRate,0 as OpngBal, " + Environment.NewLine & _
                    "SUM(convert(decimal(18,2),DrAmt*  Final.ConvRate)) as DrAmt, " + Environment.NewLine & _
                    "SUM(convert(decimal(18,2),CrAmt)) as CrAmt,  " + Environment.NewLine & _
                    "SUM(convert(decimal(18,2),Sales*  Final.ConvRate))  as [Sales],  " + Environment.NewLine & _
                    "SUM(convert(decimal(18,2),CollectionRefund*  Final.ConvRate)) as CollectionRefund, " + Environment.NewLine & _
                    "SUM(convert(decimal(18,2),DrNote*  Final.ConvRate))  as DrNote, " + Environment.NewLine & _
                    "SUM(convert(decimal(18,2),CrNote*  Final.ConvRate)) as CrNote, " + Environment.NewLine & _
                    "MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc, " + Environment.NewLine & _
                    "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from (  " & BaseQry & " ) Final " + Environment.NewLine & _
                    "left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code " + Environment.NewLine & _
                    "LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                    "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  " + Environment.NewLine & _
                    "LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code  " + Environment.NewLine & _
                    "where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND " + Environment.NewLine & _
                    "CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0   "

        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            StrQuery += " and ACode in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
        End If

        StrQuery += "  GROUP BY ACode " + Environment.NewLine &
                    ") XXX GROUP BY ACode   " + Environment.NewLine &
                    ") as outstanding_amt on outstanding_amt.ACode=customer_invoice_detail.Customer_Code " + Environment.NewLine &
                    "left outer join tspl_customer_master on tspl_customer_master.cust_code=customer_invoice_detail.Customer_Code " + Environment.NewLine &
                    "left outer join tspl_employee_master on tspl_employee_master.emp_code=tspl_customer_master.service_dealer_code  " + Environment.NewLine &
                    "left outer join tspl_zone_master on tspl_zone_master.zone_code=tspl_customer_master.zone_code " + Environment.NewLine &
                    "left outer join tspl_state_master on tspl_state_master.state_code=tspl_customer_master.state where 1=1  " + strCustCategoryMappInUserMaster + " "
        If clsCommon.CompairString(clsCommon.myCstr(ddlActiveInactive.SelectedValue), "both") <> CompairStringResult.Equal Then
            StrQuery += " and TSPL_CUSTOMER_MASTER.Status='" + ddlActiveInactive.SelectedValue + "' " + Environment.NewLine
        End If
        If txtMultiState.arrValueMember IsNot Nothing AndAlso txtMultiState.arrValueMember.Count > 0 Then
            StrQuery += " and tspl_customer_master.state_code in(" + clsCommon.GetMulcallString(txtMultiState.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
            StrQuery += " and tspl_customer_master.zone_code in(" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtServiceExecutive.arrValueMember IsNot Nothing AndAlso txtServiceExecutive.arrValueMember.Count > 0 Then
            StrQuery += " and tspl_customer_master.service_dealer_code in(" + clsCommon.GetMulcallString(txtServiceExecutive.arrValueMember) + ")" + Environment.NewLine
        End If
        If txtBusinessVertical.arrValueMember IsNot Nothing AndAlso txtBusinessVertical.arrValueMember.Count > 0 Then
            StrQuery += " and  tspl_customer_master.Cust_Account in(" + clsCommon.GetMulcallString(txtBusinessVertical.arrValueMember) + ")" + Environment.NewLine
        End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt
            gvData.Columns("Party Code").IsVisible = True
            gvData.GroupDescriptors.Add(New GridGroupByExpression("State AS State format ""{0}: {1}"" Group By State"))
            'gvData.GroupDescriptors.Add(New GridGroupByExpression("Zone AS Zone format ""{0}: {1}"" Group By Zone"))

            SetGridFormationOFGV1()
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = False
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            gvData.BestFitColumns()
            End If

    End Sub
    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True
        Next
        gvData.Columns("Customer_Code").IsVisible = False
        gvData.Columns("Customer_Code").VisibleInColumnChooser = False

        gvData.Columns("cust_code").IsVisible = False
        gvData.Columns("cust_code").VisibleInColumnChooser = False
        'For Each grow As GridViewRowInfo In gvData.Rows
        '    If clsCommon.myCdbl(grow.Cells("Outstanding").Value) > clsCommon.myCdbl(grow.Cells("Security").Value) Then
        '        grow.Cells("Outstanding").Style.BackColor = Color.Red
        '    End If

        'Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Security", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Credit Limit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem(SelFromDate, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem(SelToDate, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem(SelToDate + "(Receipt)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Outstanding", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Outstanding Over Security", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Period Closing", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        gvData.ShowGroupPanel = False
        gvData.MasterTemplate.AutoExpandGroups = True
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ReStoreGridLayout()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiCustomer.arrValueMember = Nothing
        txtMultiState.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        txtServiceExecutive.arrValueMember = Nothing
        gvData.DataSource = Nothing
        LoadStatus()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
  

    Private Sub rptSaleRegisterDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub FrmSaleVsReceipReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LoadCurrencyType()
        Pnl_Currency.Visible = False
        NopreviousDaysInSaleVSReceipt = clsCommon.myCdbl(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.NopreviousDaysInSaleVSReceipt, clsFixedParameterCode.NopreviousDaysInSaleVSReceipt, Nothing)))
    End Sub
    Private Sub LoadCurrencyType()
        Dim dtOpening As DataTable
        dtOpening = New DataTable()
        dtOpening.Columns.Add("Code", GetType(String))
        dtOpening.Columns.Add("Name", GetType(String))
        dtOpening.Rows.Add("ConvRate", "Functional Currency")
        dtOpening.Rows.Add("1", "Customer Currency")
        ddlCurrencyType.DataSource = dtOpening
        ddlCurrencyType.ValueMember = "Code"
        ddlCurrencyType.DisplayMember = "Name"
    End Sub
    Private Sub LoadStatus()
        'done by priti GKD/22/05/18-000136
        Dim dt As DataTable = Nothing
        dt = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("N", "Active")
        dt.Rows.Add("Y", "InActive")
        'dt.Rows.Add("Both", "Both")
        ddlActiveInactive.DataSource = dt
        ddlActiveInactive.ValueMember = "Code"
        ddlActiveInactive.DisplayMember = "Name"
        ddlActiveInactive.SelectedIndex = 0
    End Sub
    Private Sub txtMultiState__My_Click(sender As Object, e As EventArgs) Handles txtMultiState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtMultiState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtMultiState.arrValueMember, txtMultiState.arrDispalyMember)
    End Sub
    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim WhrClause As String = ""
        If txtBusinessVertical.arrValueMember IsNot Nothing AndAlso txtBusinessVertical.arrValueMember.Count > 0 Then
            WhrClause += " where  Cust_Account in(" + clsCommon.GetMulcallString(txtBusinessVertical.arrValueMember) + ")" + Environment.NewLine
        End If
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master " & WhrClause
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select Zone_Code as Code,Description as Name from tspl_zone_master "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMultiSel", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub txtServiceExecutive__My_Click(sender As Object, e As EventArgs) Handles txtServiceExecutive._My_Click
        Dim qry As String = " select EMP_CODE as Code,Emp_Name as Name from tspl_employee_master  WHERE Emp_Status='Active' AND Emp_type='Service Dealer'"
        txtServiceExecutive.arrValueMember = clsCommon.ShowMultipleSelectForm("SAMultiSel", qry, "Code", "Name", txtServiceExecutive.arrValueMember, txtServiceExecutive.arrDispalyMember)
    End Sub

    Private Sub gvData_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvData.CellFormatting
        Dim dbl As Double = clsCommon.myCdbl(e.Row.Cells("Outstanding").Value)
        Dim bl As Double = clsCommon.myCdbl(e.Row.Cells("Security").Value)
        If clsCommon.myCdbl(e.Row.Cells("Outstanding").Value) > clsCommon.myCdbl(e.Row.Cells("Security").Value) Then
            e.Row.Cells("Outstanding").Style.DrawFill = True
            e.Row.Cells("Outstanding").Style.BackColor = System.Drawing.Color.Red
            e.Row.Cells("Outstanding").Style.ForeColor = System.Drawing.Color.Red
            ' e.Row.Cells("Outstanding").Style.Font = Font.Bold
        End If
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvData.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()

            gvData.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvData.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvData.Columns.Count - 1 Step ii + 1
                        gvData.Columns(ii).IsVisible = False
                        gvData.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvData.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub txtBusinessVertical__My_Click(sender As Object, e As EventArgs) Handles txtBusinessVertical._My_Click
        Dim qry As String = " select distinct TSPL_CUSTOMER_MASTER.Cust_Account as Code,Cust_Acct_Desc as Description from TSPL_CUSTOMER_MASTER left join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_MASTER.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account"
        txtBusinessVertical.arrValueMember = clsCommon.ShowMultipleSelectForm("BusiVertSVR", qry, "Code", "Description", txtBusinessVertical.arrValueMember, txtBusinessVertical.arrDispalyMember)
    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmMonthlySaleReport & "'"))
            'If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            ' transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'clsCommon.MyExportToExcelGrid(" Monthly Sale Report:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
            Dim Export As New ExportToExcelML(gvData)
            Export.RunExport(filePath)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Sale Vs Receipt")

            arrHeader.Add("ActiveInactive : " + ddlActiveInactive.Text)

            If txtMultiCustomer.arrDispalyMember IsNot Nothing AndAlso txtMultiCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If

            If txtMultiState.arrDispalyMember IsNot Nothing AndAlso txtMultiState.arrDispalyMember.Count > 0 Then
                arrHeader.Add("State : " + clsCommon.GetMulcallStringWithComma(txtMultiState.arrDispalyMember))
            End If
            If txtZone.arrDispalyMember IsNot Nothing AndAlso txtZone.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
            End If

            If txtServiceExecutive.arrDispalyMember IsNot Nothing AndAlso txtServiceExecutive.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Service Executive : " + clsCommon.GetMulcallStringWithComma(txtServiceExecutive.arrDispalyMember))
            End If
            If txtBusinessVertical.arrDispalyMember IsNot Nothing AndAlso txtBusinessVertical.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Business Vertical : " + clsCommon.GetMulcallStringWithComma(txtBusinessVertical.arrDispalyMember))
            End If

            clsCommon.MyExportToPDF("Sale Register Detail", gvData, arrHeader, "Sale Register Detail", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
