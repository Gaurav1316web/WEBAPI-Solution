Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
' Ticket No : ERO/28/03/19-000522 By Prabhakar 
Public Class rptCustomerIncentiveEntry
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        txtMonth.Value = clsCommon.GETSERVERDATE()
    End Sub
    Sub Reset()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        txtLocation.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtMonth.Enabled = True
        txtLocation.Enabled = True
        txtCustomer.Enabled = True
        txtZone.Enabled = True
        btnGo.Enabled = True
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim dtFrom As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
            Dim dtTo As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
            Dim dtFromPendingLeger As DateTime = Nothing
            Dim dtToPendingLeger As DateTime = Nothing
            Dim whrforPending As String = Nothing

            Dim Query As String = "select * from TSPL_Fiscal_Year_Master where '" + clsCommon.GetPrintDate(txtMonth.Value, "dd/MMM/yyyy") + "' between start_Date and End_Date  "
            Dim dtFinYear As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dtFinYear IsNot Nothing AndAlso dtFinYear.Rows.Count > 0 Then
                dtFromPendingLeger = clsCommon.myCDate(dtFinYear.Rows(0)("start_Date"))
                dtToPendingLeger = clsCommon.myCDate(dtFinYear.Rows(0)("End_Date"))
            End If
            Dim isRecordExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD where CONVERT(DATE,TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month,103) >= CONVERT(DATE,'" & clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt")) & "',103) and CONVERT(DATE,TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month,103) <= CONVERT(DATE,'" & clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt")) & "',103)  "))
            If isRecordExist = True Then
                '===================================================================ppppppppppppppppppppp
                Dim type As String = "Aged Trial Balance By Document date"
                Dim IsFifoBased As String = "N"
                Dim ArryLst As New ArrayList
                ArryLst.Add("IN")
                ArryLst.Add("DB")
                ArryLst.Add("CR")
                ArryLst.Add("RC")

                ArryLst.Add("UC")
                ArryLst.Add("SR")

                ArryLst.Add("AD")
                ArryLst.Add("RF")

                ArryLst.Add("AV")
                ArryLst.Add("OA")
                ArryLst.Add("VGCL")
                Dim ArryLstLocation As New ArrayList
                ArryLstLocation.Add(clsCommon.GetMulcallString(txtLocation.arrValueMember))
                Dim CheckCustomer As String
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
                Dim rptHeading As String
                rptHeading = "Aged Trial Balance Report"
                Dim isonduedate As String = String.Empty
                isonduedate = "DueDate"
                whrforPending = " and convert (date,Final.[Document Date],103) >= convert (date, '" + dtFromPendingLeger + "',103)  and convert (date,Final.[Document Date],103) <= convert (date,'" + dtToPendingLeger + "',103)"
                
                Dim strInnerQry As String = GetOutStandingQry(clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToPendingLeger), "dd/MMM/yyyy hh:mm:ss tt")), clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToPendingLeger), "dd/MMM/yyyy hh:mm:ss tt"), False, ArryLst, isonduedate, "ConvRate", IIf((txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0), txtCustomer.arrValueMember, Nothing), IIf((txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0), txtLocation.arrValueMember, Nothing), Nothing, False, Nothing, IIf(False, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"))

                Dim qry As String = "  WITH PenidngAmountTotal (Cust_Code,TotalDueAmount)  AS  ( select xxxx.Cust_Code ,sum (xxxx.[Due Amount])  as TotalDueAmount from ( " &
                     "  select  Final.[Document Id],convert (varchar, Final.[Document Date],103) as [Document Date] ,Final.[Customer Id] as [Cust_Code] , Final.[Customer Name], Final.[Due Amount], Case when Document_Type not in ('CR','OA','AV') then  Final.[Document Amount] else 0 end [Document Amount], " &
                     "  Case when Document_Type in ('CR','OA','AV') then   (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0) * (-1)) ) else (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0)) ) end  + Case when Document_Type in ('CR','OA','AV') then isnull(Final.[Document Amount],0) else 0 end  as [Receipt Amount] from (  " &
                     "  " + strInnerQry + "   " &
                     "   ) Final where Final.[Due Amount] <> 0   " + whrforPending + "  " &
                     "  ) XXXX group by Cust_Code  )  "
                ''richa agarwal Total amount should be shown in round off 31 oct,2020
                qry += " select TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Cust_Code as [Distributor Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Distributor Name]" &
                ",TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as [Zone Name]" &
                ",TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Qty  as [Total Litre],TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Range_Qty  as [Total Crate] ,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE. Range_AVg_Qty  as [Avg Crate],TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Rate  as  [Slab],   cast( TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Amount as decimal(18,0))  as [Total Amount] ,TSPL_CUSTOMER_INCENTIVE_DETAIL.Deduction_Amount as [Standing Recovery Amount],PenidngAmountTotal.TotalDueAmount as [Pending Recovery Amount],isnull (TSPL_CUSTOMER_INCENTIVE_DETAIL.Amount,0) - isnull (TotalDueAmount,0) as [Payable Amount]  from TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE " &
                                " left outer join TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD on TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Doc_Code  " &
                                " left outer join TSPL_CUSTOMER_INCENTIVE_DETAIL on TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code = TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Doc_Code And TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code = TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Cust_Code " &
                                " left outer join PenidngAmountTotal on PenidngAmountTotal.Cust_Code = TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Cust_Code " &
                                " left outer join TSPL_CUSTOMER_MASTER on  TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " &
                                " left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code" &
                                " where CONVERT(DATE,TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month,103) >= CONVERT(DATE,'" & clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt")) & "',103) " &
                                " and CONVERT(DATE,TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month,103) <= CONVERT(DATE,'" & clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt")) & "',103) "
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
                If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                    qry += " and TSPL_ZONE_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    Gv1.DataSource = dt
                    For ii As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).ReadOnly = True
                    Next

                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.BestFitColumns()
                    Gv1.EnableFiltering = True
                    ' Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim totalLtr As New GridViewSummaryItem("Total Litre", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(totalLtr)

                    Dim totalAmount As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(totalAmount)

                    Dim StandingRecoveryAmount As New GridViewSummaryItem("Standing Recovery Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(StandingRecoveryAmount)

                    'Pending Recovery Amount
                    Dim PendingRecoveryAmount As New GridViewSummaryItem("Pending Recovery Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(PendingRecoveryAmount)

                    'Payable Amount
                    Dim PayableAmount As New GridViewSummaryItem("Payable Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(PayableAmount)

                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    txtMonth.Enabled = False
                    txtLocation.Enabled = False
                    txtCustomer.Enabled = False
                    txtZone.Enabled = False
                    btnGo.Enabled = False
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If

                ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            


           
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    
    
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCustomerIncentiveEntry & "'"))
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            'If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If
            
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Turnover Discount Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Turnover Discount Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Cust_Code as Code ,Customer_Name as Name,add1 as Address from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "cusInEnRpt@c", qry, "Code", "", txtCustomer.arrValueMember, Nothing)
    End Sub

    Public Shared Function GetOutStandingQry(ByVal AgeOfDate As DateTime, ByVal CutOfDate As DateTime, ByVal IsInactiveCustomer As Boolean, ByVal DocTypeList As ArrayList, ByVal IsOnDueDate As String, Optional ByVal strCurrency As String = "", Optional ByVal CustomerList As ArrayList = Nothing, Optional ByVal LocationList As ArrayList = Nothing, Optional ByVal CustomerGroupList As ArrayList = Nothing, Optional ByVal IsParentCustomer As Boolean = False, Optional ByVal ParentCustomerList As ArrayList = Nothing, Optional ByVal Is_Security As String = "", Optional ByVal ISShowCustomerInvoiceorDocNo As Boolean = False) As String
        Try

            Dim Qry As String = " Select MAX(xxx.Comp_Code) AS Comp_Code, [Customer Id], MAX([Parent Code]) AS [Parent Code],MAX(Parent_Master.Customer_Name) as ParentName, MAX([Customer Name]) AS [Customer Name], MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code) AS Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) AS Cust_Group_Desc, [Document Id], MAX([Desc]) as [Desc]," & _
             " SUM([Due Amount]*" & strCurrency & ")  AS [Due Amount],  max([Document Amount]) as [Document Amount]," & _
"" & IIf(strCurrency = "1", "MAX(xxx.CURRENCY_CODE)", "'INR'") & "  as Currency,max(xxx.CURRENCY_CODE) As CURRENCY_CODE,MAX(xxx.ConvRate) As ConvRate, MAX([Due Date]) AS [Due Date], MAX(type) AS type, MAX([Document Date]) AS [Document Date], MAX(Ageing_Days) AS Ageing_Days, MAX(Document_Type) AS Document_Type, MAX(Location) AS Location  from ("

            Qry += " SELECT  TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_Customer_Invoice_Head.Comp_Code,TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
         " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name]," & _
         " " + IIf(ISShowCustomerInvoiceorDocNo = True, " TSPL_Customer_Invoice_Head.Document_No ", "(case when ISNULL( Against_Sale_No,'')<>'' then Against_Sale_No when ISNULL(Against_Sale_Return_No,'')<>'' then TSPL_Customer_Invoice_Head.Document_No  when ISNULL( AgainstScrap,'')<>'' then AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end)") + "  as [Document Id], " & _
         " TSPL_Customer_Invoice_Head.Description as [Desc], (case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then TSPL_Customer_Invoice_Head.Document_Total Else (TSPL_Customer_Invoice_Head.Document_Total-  ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' ),0) " & _
         "  - ISNULL((Select SUM(TSPL_RECEIPT_HEADER.Receipt_Amount) from TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' and  ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A') AND TSPL_RECEIPT_HEADER.Applied_RECEIPT=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)),0) " & _
         ") *-1 end " & _
         " - case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' ),0) else 0 end -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SD_SALE_RETURN_HEAD.Status,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE  left outer join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo   LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No  where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Posted ,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN.Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap =TSPL_SCRAPINVOICE_HEAD.Invoice_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_SCRAPSALE_HEAD_RETURN.Status,'0') =1),0)) as [Due Amount],TSPL_Customer_Invoice_Head.Document_Total as [Document Amount]   ,TSPL_Customer_Invoice_Head.CURRENCY_CODE, case when isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) <>0 then isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) else TSPL_Customer_Invoice_Head.ConvRate end as ConvRate  ,"

            Qry += "  TSPL_Customer_Invoice_Head.due_date as [Due Date],'' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date], "

            If clsCommon.CompairString(IsOnDueDate, "DueDate") = CompairStringResult.Equal Then
                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Due_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
            ElseIf clsCommon.CompairString(IsOnDueDate, "DocumentDate") = CompairStringResult.Equal Then
                'Qry += " DATEDIFF(dd,convert(date,Invoice_Entry_Date,103),'" & StrAgeOfDate & "') as datedifference, "
                ' Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head." + IIf(IsOnDueDate = True, "Due_Date", "Document_Date") + ",103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
            End If

            Qry += " case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
           " TSPL_Customer_Invoice_Head.Loc_Code as Location FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No " & _
           " left outer join (  Select  TSPL_REVALUATION_DETAIL.AR_Invoice_No,TSPL_REVALUATION_HEAD.Currency_Rate,TSPL_REVALUATION_HEAD.Document_Date,TSPL_REVALUATION_HEAD.Document_No  " & _
           " from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD  on TSPL_REVALUATION_DETAIL.Document_No =TSPL_REVALUATION_HEAD.Document_No and TSPL_REVALUATION_HEAD.Document_No in  (select top 1 h.Document_No  from TSPL_REVALUATION_HEAD as h  left outer join TSPL_REVALUATION_DETAIL d on d.Document_No=h.Document_No" & _
           " where d.AR_Invoice_No=TSPL_REVALUATION_DETAIL.AR_Invoice_No and CONVERT(DATE,h.Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(d.AR_Invoice_No ,'')<>'' order by h.Document_Date desc) where CONVERT(DATE,TSPL_REVALUATION_HEAD .Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(AR_Invoice_No ,'')<>'')TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD .AR_Invoice_No =TSPL_Customer_Invoice_Head .Document_No where TSPL_Customer_Invoice_Head.Status='1' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
           " UNION ALL SELECT ''  as ARINvoiceNo,  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name] ,Document_No as [Document Id]  ,Description as [Desc] ,(Total_Order_Amt)*-1 as [Due Amount] ,Total_Order_Amt as [Document Amount] ,'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date]  , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END  as [Due Amount],TSPL_VCGL_Head.Amount as [Document Amount], 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Head.Document_No  where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'  AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''" & _
           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END as [Due Amount], TSPL_VCGL_Head.Amount as [Document Amount], 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Detail.Document_No  where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL ,'') =''" & _
           " UNION ALL select ''  as ARINvoiceNo, TSPL_RECEIPT_HEADER.Comp_Code ,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , (Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
           " COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
           " AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " & _
           " CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z))*-1 End) as [Due Amount],TSPL_RECEIPT_HEADER.Receipt_Amount as [Document Amount], TSPL_RECEIPT_HEADER.CURRENCY_CODE  AS CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate AS ConvRate ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" & AgeOfDate & "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location  from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M', 'A','R') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" + Is_Security + "" & _
           "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " & _
            "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " & _
                       " AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN ( " & Environment.NewLine & _
                       " sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')" & Environment.NewLine & _
                       " union all" & Environment.NewLine & _
                       " sELECT Receipt_No  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')" & Environment.NewLine & _
           " union all " & Environment.NewLine & _
           " Select distinct TSPL_RECEIPT_DETAIL.Document_No  from TSPL_RECEIPT_DETAIL left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL .Receipt_No where TSPL_RECEIPT_DETAIL.Document_No in (Select ISNULL(Receipt_No ,'') from TSPL_RECEIPT_HEADER where Receipt_Type ='F' and isnull(Applied_Receipt ,'')='') and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))"




            ''richa agarwal use TSPL_Receipt_Adjustment_Header.ARInvoiceNo in place of Adjustment_no in below line to show ar invoice no BM00000007349
            ' Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then 0 else TSPL_Receipt_Adjustment_Header.Adjustment_Amount end *-1 as [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
            Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else TSPL_Receipt_Adjustment_Header.Adjustment_Amount * -1 end  as [Due Amount]  ,TSPL_Receipt_Adjustment_Header.Adjustment_Amount as [Document Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
                              " UNION ALL SELECT ''  as ARINvoiceNo, TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name],Document_No as [Document Id] ,Description as [Desc] , Empty_Value*-1 AS [Due Amount] , Empty_Value as [Document Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
                              " UNION ALL SELECT '' as ARINvoiceNo,  TSPL_ADJUSTMENT_HEADER.Comp_Code,TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end  AS [Due Amount] ,(SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) as [Document Amount] ,'INR' AS CURRENCY_CODE, 1 AS ConvRate,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code) FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')='' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
                          " ) XXX LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON XXX.[Customer Id]=TSPL_CUSTOMER_MASTER.Cust_Code" & _
                          " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=XXX.[Parent Code]" & _
                          " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code" & _
                          " where  XXX.Document_Type in (" + clsCommon.GetMulcallString(DocTypeList) + "  ) " & _
                          " and convert(date,XXX.[Document Date] ,103) <= convert(date,'" & CutOfDate & "',103)"
            If IsParentCustomer Then
                If ParentCustomerList IsNot Nothing AndAlso ParentCustomerList.Count > 0 Then
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " AND ((XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ")) or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
                    Else
                        Qry += " AND (XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
                    End If

                Else
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
                    End If
                End If
            Else
                Dim AllowtoSHOWParentChildCustomer As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSHOWParentChildCustomer, clsFixedParameterCode.AllowtoSHOWParentChildCustomer, Nothing)) = 1, True, False))
                If AllowtoSHOWParentChildCustomer = True Then
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and (XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") or XXX.[Parent Code]  in (" + clsCommon.GetMulcallString(CustomerList) + ") ) "
                    End If
                Else
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
                    End If
                End If

            End If

            'If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
            '    Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
            'End If
            If LocationList IsNot Nothing AndAlso LocationList.Count > 0 Then
                Qry += " and XXX.Location in (" + clsCommon.GetMulcallString(LocationList) + ") "
            End If
            If CustomerGroupList IsNot Nothing AndAlso CustomerGroupList.Count > 0 Then
                Qry += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(CustomerGroupList) + ") "
            End If
            ''richa 12 Dec, 2016
            'Qry += " AND XXX.[Document Id]  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine & _
            '" XXX.[Document Id] AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine & _
            '" AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"
            Qry += " AND XXX.ARINvoiceNo  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine & _
           " XXX.ARINvoiceNo AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine & _
           " AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"


            ''--------
            Qry += " Group By XXX.[Customer Id], XXX.[Document Id]"
            'Qry += "AND [Due Amount] <> 0 Group By XXX.[Customer Id], XXX.[Document Id]"

            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub TxtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        strQry = "Select TSPL_ZONE_MASTER.Zone_Code As Code,  TSPL_ZONE_MASTER.Description As Name From TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMul1", strQry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub
End Class
