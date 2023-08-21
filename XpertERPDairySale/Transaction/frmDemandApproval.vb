Imports common
Public Class frmDemandApproval
    Private Sub SplitContainer2_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer2.Panel1.Paint
    End Sub
    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = "Select DISTINCT TSPL_CUSTOMER_MASTER.Route_No as Code,TSPL_CUSTOMER_MASTER.Route_Desc as Description from TSPL_CUSTOMER_MASTER"
            Dim whrcls As String = "" ' "Route_No is not null and TSPL_CUSTOMER_MASTER.Area_Code='" + clsCommon.myCstr() + "'"
            txtRoute.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", whrcls, txtRoute.Value, "", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRoute.Value, Nothing))
            lblDistributorNameDesc.Text = clsDBFuncationality.getSingleValue("select  Customer_Name from TSPL_CUSTOMER_MASTER where Route_No='" + clsCommon.myCstr(txtRoute.Value) + "' and Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "' and IsDistributor ='Y' ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmDemandApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandApproval, clsFixedParameterCode.ApplyDemandApproval, Nothing)) = 1 Then
            Reset()
        Else
            clsCommon.MyMessageBoxShow(Me, "This Funcationality not for you!")
            Me.Close()
        End If
    End Sub
    Sub Reset()
        txtZone.Value = ""
        txtRoute.Value = ""
        txtRoute.Enabled = True
        lblZoneDesc.Text = ""
        lblRouteDesc.Text = ""
        lblDistributorNameDesc.Text = ""
        lblSAmtDesc.Text = ""
        lblBAmtDesc.Text = ""
        lblDocAmtDesc.Text = ""
        lblDiffAmtDesc.Text = ""
        rbtnMorning.IsChecked = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        GV1.DataSource = Nothing
        GV1.Rows.Clear()
        GV1.Columns.Clear()
    End Sub
    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Try
            Dim qry As String = "Select DISTINCT TSPL_CUSTOMER_MASTER.Zone_Code as Code,TSPL_ZONE_MASTER.Description as Description from TSPL_CUSTOMER_MASTER left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            Dim whrcls As String = "TSPL_CUSTOMER_MASTER.Zone_Code is not null and  TSPL_CUSTOMER_MASTER.Zone_Code<>''"
            txtZone.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", whrcls, txtZone.Value, "", isButtonClicked)
            lblZoneDesc.Text = clsCommon.myCstr(ClsZoneMaster.GetName(txtZone.Value))
            'TxtArea.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        GV1.ShowGroupPanel = False
        GV1.ShowRowHeaderColumn = False
        GV1.AllowAddNewRow = False
        GV1.AllowDeleteRow = False
        GV1.EnableFiltering = True
        GV1.ShowFilteringRow = True
        GV1.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To GV1.Columns.Count - 2
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).BestFit()
        Next
        GV1.Columns("Sl_No").HeaderText = "S.N"
        GV1.Columns("Sl_No").IsVisible = True
        GV1.Columns("TR_Code").HeaderText = "TR_Code"
        GV1.Columns("TR_Code").IsVisible = False
        GV1.Columns("Document_No").HeaderText = "Document No"
        GV1.Columns("Document_No").IsVisible = False
        GV1.Columns("Cust_Code").HeaderText = "Cust Code"
        GV1.Columns("Cust_Code").IsVisible = True
        GV1.Columns("Customer_Name").HeaderText = "Customer Name"
        GV1.Columns("Customer_Name").IsVisible = True
        GV1.Columns("Item_Code").HeaderText = "Item Code"
        GV1.Columns("Item_Code").IsVisible = False
        GV1.Columns("Item_Desc").HeaderText = "Item Desc "
        GV1.Columns("Item_Desc").IsVisible = True
        GV1.Columns("Qty").HeaderText = "Qty"
        GV1.Columns("Qty").IsVisible = True
        GV1.Columns("Unit_Code").HeaderText = "Unit Code"
        GV1.Columns("Unit_Code").IsVisible = True
        GV1.Columns("ItemNetAmount").HeaderText = "Item Net Amt"
        GV1.Columns("ItemNetAmount").IsVisible = True
        GV1.Columns("ItemNetAmount").ReadOnly = True
        GV1.Columns("ItemNetAmount").FormatString = "{0:n2}"
        GV1.Columns("Location_Code").HeaderText = "Location Code"
        GV1.Columns("Location_Code").IsVisible = False
        GV1.Columns("Location_Code").ReadOnly = True
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim TotalQty As New GridViewSummaryItem("Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(TotalQty)
        Dim TotalAmt As New GridViewSummaryItem("ItemNetAmount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(TotalAmt)
        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        GV1.AutoSizeRows = True
        GV1.BestFitColumns()
        GV1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim custCode As String = clsDBFuncationality.getSingleValue("select  Cust_Code from TSPL_CUSTOMER_MASTER where Route_No='" + clsCommon.myCstr(txtRoute.Value) + "' and Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "' and IsDistributor ='Y'")

            Dim dt As New DataTable()
            Dim strQry As String = "select ROW_NUMBER() Over (Order by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) As Sl_No,TSPL_DEMAND_BOOKING_DETAIL.TR_Code,TSPL_DEMAND_BOOKING_DETAIL.Document_No,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,TSPL_DEMAND_BOOKING_MASTER.Location_Code
from TSPL_DEMAND_BOOKING_DETAIL 
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
left outer join TSPL_TRANSACTION_APPROVAL on TSPL_TRANSACTION_APPROVAL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
where CONVERT(DATE,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" + clsCommon.GetPrintDate(txtDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + clsCommon.myCstr(txtRoute.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Posted=1
and TSPL_CUSTOMER_MASTER.Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "' and TSPL_TRANSACTION_APPROVAL.Approve=0 "
            If rbtnMorning.IsChecked Then
                strQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Morning'"
            ElseIf rbtnEvening.IsChecked Then
                strQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Evening'"
            End If
            dt = clsDBFuncationality.GetDataTable(strQry)
            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            GV1.DataSource = Nothing
            GV1.Rows.Clear()
            GV1.Columns.Clear()
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            GV1.MasterView.Refresh()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Demand Not Found", Me.Text)
                Exit Sub
            Else
                Dim SecurityAmtQry As String = "Select (SUM(Opening)+SUM(Debit)-SUM(Credit)) as Closing from (
Select  max(type) as type,Opening.Customer_Code, MAX(CM.Customer_Name) as Customer_Name, '' as Document_No, NULL as Document_Date, 'Opening' as DocType,  Case When SecurityDepositType='S' Then 'Security' When SecurityDepositType='C' Then 'Crate Security' When SecurityDepositType='R' Then 'Refrigerator Security' Else 'Others' end as SecurityDepositType, (SUM(Debit)-SUM(Credit)) as Opening, 0 as Debit, 0 as  Credit, Loc_code,max(Location_Desc) as Location_Desc,Opening.Cust_Group_Code,max(Cust_Group_Desc) as Cust_Group_Desc,isnull(max(CM.Zone_Code),'') as [Zone Code],isnull(max(TSPL_ZONE_MASTER.Description),'') as [Zone Desc] from (
select 'AR Invoice Entry' as Type,TSPL_Customer_Invoice_Head.Document_No, TSPL_Customer_Invoice_Head.Document_Date, Case When TSPL_Customer_Invoice_Head.Document_Type='D' Then 'Debit Note' When TSPL_Customer_Invoice_Head.Document_Type='C' Then 'Credit Note' End as DocType, TSPL_Customer_Invoice_Head.Customer_Code, TSPL_Customer_Invoice_Head.SecurityDepositType, case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Customer_Invoice_Head.Document_Total Else 0 end as Debit, case when TSPL_Customer_Invoice_Head.Document_Type='D' then TSPL_Customer_Invoice_Head.Document_Total Else 0 end Credit, Case When TSPL_Customer_Invoice_Head.Status=1 Then 'Y' Else 'N' End as Posted,Loc_code,Location_Desc ,TSPL_CUSTOMeR_MASTer.Cust_Group_Code, Cust_Group_Desc from TSPL_Customer_Invoice_Head left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMeR_MASTer.Cust_Group_Code  where TSPL_Customer_Invoice_Head.SecurityDeposit='Y'
 UNION ALL
 select 'Receipt Entry' as Type ,TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, Case When Receipt_Type='P' Then 'Advance' When Receipt_Type='O' Then 'On Account' When Receipt_Type='M' Then 'Misc Receipt' When Receipt_Type='F' Then 'Refund' When Receipt_Type='S' Then 'Misc Refund' End as DocType, TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.SecurityDepositType, case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount Else 0 end as  Debit, case when TSPL_RECEIPT_HEADER.Receipt_Type<>'F' then Receipt_Amount Else 0 end as Credit, TSPL_RECEIPT_HEADER.Posted,Location_GL_Code as Loc_code,Location_Desc,TSPL_CUSTOMeR_MASTer.Cust_Group_Code, Cust_Group_Desc from TSPL_RECEIPT_HEADER left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_RECEIPT_HEADER.Location_GL_Code  left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_RECEIPT_HEADER.Cust_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMeR_MASTer.Cust_Group_Code  where TSPL_RECEIPT_HEADER.SecurityDeposit='Y'
 UNION ALL
 select 'Bank Reverse Entry' as Type, TSPL_BANK_REVERSE.Reverse_Code, TSPL_BANK_REVERSE.Reversal_Date, 'Bank Reverse' as DocType, TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.SecurityDepositType, case when TSPL_RECEIPT_HEADER.Receipt_Type<>'F' then Receipt_Amount Else 0 end as  Debit, case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount Else 0 end as Credit, Case When TSPL_BANK_REVERSE.Post='P' Then 'Y' Else 'N' End as Posted ,Location_GL_Code as Loc_code,Location_Desc,TSPL_CUSTOMeR_MASTer.Cust_Group_Code, Cust_Group_Desc From TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_RECEIPT_HEADER.Location_GL_Code left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_BANK_REVERSE.Cust_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMeR_MASTer.Cust_Group_Code Where Reverse_Document='Receipts' AND TSPL_RECEIPT_HEADER.SecurityDeposit='Y'
) Opening LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=Opening.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=cm.Zone_Code  WHERE CONVERT(Date, Document_Date,103)<CONVERT(Date,'08/07/2023',103)  AND Posted='Y' AND Customer_Code in ('D12345') Group By Opening.Customer_Code,Loc_code,Opening.Cust_Group_Code,SecurityDepositType
 UNION ALL--=========================MAIN UNION===============================
Select xxx.type, XXX.Customer_Code, CM.Customer_Name, Document_No, Document_Date, DocType, Case When SecurityDepositType='S' Then 'Security' When SecurityDepositType='C' Then 'Crate Security' When SecurityDepositType='R' Then 'Refrigerator Security' Else 'Others' End as SecurityDepositType, 0 as Opening, Debit, Credit,Loc_code,Location_Desc,XXX.Cust_Group_Code,Cust_Group_Desc,isnull(CM.Zone_Code,'') as [Zone Code],isnull(TSPL_ZONE_MASTER.Description,'') as [Zone Desc] from (
select 'AR Invoice Entry' as Type,TSPL_Customer_Invoice_Head.Document_No, TSPL_Customer_Invoice_Head.Document_Date, Case When TSPL_Customer_Invoice_Head.Document_Type='D' Then 'Debit Note' When TSPL_Customer_Invoice_Head.Document_Type='C' Then 'Credit Note' End as DocType, TSPL_Customer_Invoice_Head.Customer_Code, TSPL_Customer_Invoice_Head.SecurityDepositType, case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Customer_Invoice_Head.Document_Total Else 0 end as Debit, case when TSPL_Customer_Invoice_Head.Document_Type='D' then TSPL_Customer_Invoice_Head.Document_Total Else 0 end Credit, Case When TSPL_Customer_Invoice_Head.Status=1 Then 'Y' Else 'N' End as Posted,Loc_code,Location_Desc ,TSPL_CUSTOMeR_MASTer.Cust_Group_Code, Cust_Group_Desc from TSPL_Customer_Invoice_Head left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMeR_MASTer.Cust_Group_Code  where TSPL_Customer_Invoice_Head.SecurityDeposit='Y'
 UNION ALL
 select 'Receipt Entry' as Type ,TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Date, Case When Receipt_Type='P' Then 'Advance' When Receipt_Type='O' Then 'On Account' When Receipt_Type='M' Then 'Misc Receipt' When Receipt_Type='F' Then 'Refund' When Receipt_Type='S' Then 'Misc Refund' End as DocType, TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.SecurityDepositType, case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount Else 0 end as  Debit, case when TSPL_RECEIPT_HEADER.Receipt_Type<>'F' then Receipt_Amount Else 0 end as Credit, TSPL_RECEIPT_HEADER.Posted,Location_GL_Code as Loc_code,Location_Desc,TSPL_CUSTOMeR_MASTer.Cust_Group_Code, Cust_Group_Desc from TSPL_RECEIPT_HEADER left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_RECEIPT_HEADER.Location_GL_Code  left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_RECEIPT_HEADER.Cust_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMeR_MASTer.Cust_Group_Code  where TSPL_RECEIPT_HEADER.SecurityDeposit='Y'
 UNION ALL
 select 'Bank Reverse Entry' as Type, TSPL_BANK_REVERSE.Reverse_Code, TSPL_BANK_REVERSE.Reversal_Date, 'Bank Reverse' as DocType, TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.SecurityDepositType, case when TSPL_RECEIPT_HEADER.Receipt_Type<>'F' then Receipt_Amount Else 0 end as  Debit, case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount Else 0 end as Credit, Case When TSPL_BANK_REVERSE.Post='P' Then 'Y' Else 'N' End as Posted ,Location_GL_Code as Loc_code,Location_Desc,TSPL_CUSTOMeR_MASTer.Cust_Group_Code, Cust_Group_Desc From TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_RECEIPT_HEADER.Location_GL_Code left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_BANK_REVERSE.Cust_Code left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMeR_MASTer.Cust_Group_Code Where Reverse_Document='Receipts' AND TSPL_RECEIPT_HEADER.SecurityDeposit='Y'
) XXX LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=XXX.Customer_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=cm.Zone_Code WHERE Document_Date>='" + clsCommon.GetPrintDate(txtDate.Value) + "' AND Document_Date<='" + clsCommon.GetPrintDate(txtDate.Value) + "'  AND Posted='Y' AND Customer_Code in ('" & custCode & "')
) YYY Group By Customer_Code ORDER BY Customer_Code
"
                lblSAmtDesc.Text = clsDBFuncationality.getSingleValue(SecurityAmtQry)
                Dim docQry As String = "select sum(TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount) as NetTotal
from TSPL_DEMAND_BOOKING_DETAIL 
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
where CONVERT(DATE,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" + clsCommon.GetPrintDate(txtDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + clsCommon.myCstr(txtRoute.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Posted=1
and TSPL_CUSTOMER_MASTER.Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "'"
                If rbtnMorning.IsChecked Then
                    docQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Morning'"
                ElseIf rbtnEvening.IsChecked Then
                    docQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Evening'"
                End If
                lblDocAmtDesc.Text = clsDBFuncationality.getSingleValue(docQry)
                Dim OutStandingAmtQry As String = "Select  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " &
                    "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " &
                    "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " &
                    "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " &
                    "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   " &
                    "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", "'" & custCode & "'", True, clsCommon.GetPrintDate(txtDate.Value.AddDays(1), "dd/MMM/yyyy"), "", False, False, True, Nothing, False) & "   " &
                    " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                    "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " &
                    "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & custCode & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " &
                    ") XXX GROUP BY ACode ORDER BY ACode"
                lblBAmtDesc.Text = clsDBFuncationality.getSingleValue(OutStandingAmtQry)
                lblDiffAmtDesc.Text = clsCommon.myCDecimal(lblSAmtDesc.Text) + clsCommon.myCDecimal(lblBAmtDesc.Text) + clsCommon.myCDecimal(lblDocAmtDesc.Text)
                GV1.DataSource = dt
                SetGridFormat()
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
    'Sub SaveData()
    '    Try
    '        Dim Document_No As String = GV1.Rows(0).Cells("Document_No").Value
    '        Dim Cust_Code As String = clsDBFuncationality.getSingleValue("select  Cust_Code from TSPL_CUSTOMER_MASTER where Route_No='" + clsCommon.myCstr(txtRoute.Value) + "' and Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "' and IsDistributor ='Y'")
    '        Dim Location_Code As String = GV1.Rows(0).Cells("Location_Code").Value
    '        If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyDemandApproval, clsFixedParameterCode.ApplyDemandApproval, Nothing)) = 1 Then
    '            'For Each grow As GridViewRowInfo In GV1.Rows
    '            '    Document_No = grow.Cells("Document_No").Value
    '            '    Cust_Code = grow.Cells("Cust_Code").Value
    '            '    Location_Code = grow.Cells("Location_Code").Value
    '            '    Dim StrQry As String = "Update TSPL_BOOKING_DETAIL set CreditApproval_Reqd='Y',Booking_Status=3 where  Document_No='" & Document_No & "' and Cust_Code='" & Cust_Code & "' and    Loc_Code='" & clsCommon.myCstr(Location_Code) & "'"
    '            '    If rbtnMorning.IsChecked Then
    '            '        StrQry += " and Line_no=1"
    '            '    ElseIf rbtnEvening.IsChecked Then
    '            '        StrQry += " and Line_no=2"
    '            '    End If
    '            '    clsDBFuncationality.ExecuteNonQuery(StrQry)
    '            'Next
    '            '     Dim qry As String = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code,Cust_Code,Loc_Code) " &
    '            '"values ('Booking Dairy','" & clsUserMgtCode.frmbookingdairy & "','" & Document_No & "', " &
    '            '"'" & clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "', " &
    '            '"'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "', " &
    '            '"'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "', " &
    '            '"'" & objCommonVar.CurrentCompanyCode & "','" & Cust_Code & "','" & Location_Code & "')"
    '            '     clsDBFuncationality.ExecuteNonQuery(qry)
    '            Dim qry As String = "Update TSPL_TRANSACTION_APPROVAL set Approve=1,Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "' where Document_No='" & Document_No & "' and Cust_Code='" & Cust_Code & "' and Loc_Code='" & Location_Code & "'"
    '            clsDBFuncationality.ExecuteNonQuery(qry)
    '            clsCommon.MyMessageBoxShow("Approved Successfully")
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim Document_No As String = GV1.Rows(0).Cells("Document_No").Value
            Dim Cust_Code As String = clsDBFuncationality.getSingleValue("select  Cust_Code from TSPL_CUSTOMER_MASTER where Route_No='" + clsCommon.myCstr(txtRoute.Value) + "' and Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "' and IsDistributor ='Y'") 'GV1.Rows(0).Cells("Cust_Code").Value
            Dim Location_Code As String = GV1.Rows(0).Cells("Location_Code").Value
            Dim qry As String = "Update TSPL_TRANSACTION_APPROVAL set Approve=1,Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "' where Document_No='" & Document_No & "' and Cust_Code='" & Cust_Code & "' and Loc_Code='" & Location_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            clsCommon.MyMessageBoxShow("Approved Successfully")
            btnApprove.Enabled = False
            btnReject.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Try
            Dim Document_No As String = GV1.Rows(0).Cells("Document_No").Value
            Dim Cust_Code As String = clsDBFuncationality.getSingleValue("select  Cust_Code from TSPL_CUSTOMER_MASTER where Route_No='" + clsCommon.myCstr(txtRoute.Value) + "' and Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "' and IsDistributor ='Y'") 'GV1.Rows(0).Cells("Cust_Code").Value
            Dim Location_Code As String = GV1.Rows(0).Cells("Location_Code").Value
            Dim qry As String = "Update TSPL_TRANSACTION_APPROVAL set Approve=2,Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "' where Document_No='" & Document_No & "' and Cust_Code='" & Cust_Code & "' and Loc_Code='" & Location_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            clsCommon.MyMessageBoxShow("Reject Successfully")
            btnApprove.Enabled = False
            btnReject.Enabled = False
        Catch ex As Exception
        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class