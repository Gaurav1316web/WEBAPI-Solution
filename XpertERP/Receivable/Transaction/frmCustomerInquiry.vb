'--Created By--Pankaj Kumar-------Date-27/09/2012---------------
'--21/12/2012--Updation By --Pankaj Kumar--- Applied Validations
'------------------BM00000003394
'Puran Singh Negi- BM00000003392

Imports common
Imports System.Data.SqlClient

Public Class FrmCustomerInquiry
    Inherits FrmMainTranScreen
    Private Sub FrmCustomerInquiry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmCustomerInquiry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub Reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        fndCustomer.Value = ""
        txtCustName.Text = ""
        ClearGV1()
        ClearGV2()
        'gv1.Rows.Clear()
        ''gv1.Columns.Clear()
        'gv1.DataSource = Nothing
        'gv2.Rows.Clear()
        'gv2.Columns.Clear()
        'gv2.DataSource = Nothing
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Dim qry As String = ""
        Dim whrclas As String = ""
        Dim orderBy As String = ""
        '-------richa 13/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""

        strwherecls = Xtra.CustomerPermission()
        If clsCommon.myLen(strwherecls) > 0 Then
            whrclas += "  TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        qry = "select Cust_Code ,Customer_Name, (TSPL_CUSTOMER_MASTER.Add1 + case When TSPL_CUSTOMER_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_CUSTOMER_MASTER.Add2, 103) End + Case When TSPL_CUSTOMER_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_CUSTOMER_MASTER.Add3,103) end + case When TSPL_CUSTOMER_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_CUSTOMER_MASTER.City_Code, 103) end+ Case When TSPL_CUSTOMER_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_CUSTOMER_MASTER.State) end ) as Address  from TSPL_CUSTOMER_MASTER"
        orderBy = " Cust_Code"
        fndCustomer.Value = clsCommon.ShowSelectForm("Customer Code", qry, "Cust_Code", whrclas, fndCustomer.Value, orderBy, isButtonClicked)
        txtCustName.Text = clsDBFuncationality.getSingleValue("SELECT Customer_Name FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + fndCustomer.Value + "'")
        ClearGV1()
        ClearGV2()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Start Date Can Not Be Greater Then End Date")
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            FillSaleInvoice()
        Else
            common.clsCommon.MyMessageBoxShow("Please Select Customer Code")
            fndCustomer.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub FillSaleInvoice()
        Try

            'Dim Qry As String = "SELECT Sale_Invoice_No AS [Invoice No], COnvert(Varchar, Sale_Invoice_Date, 103) AS [Date], Empty_Value AS [Container Deposite], Total_Invoice_Amt AS [Invoice Amt], (ISNULL(Empty_Value, 0)+ISNULL(Total_Invoice_Amt, 0)) AS [Total Invoice Amt], Balance_Amt AS [Balance Amt], CASE When Is_Post='Y' THEN 'Posted' ELSE 'Unposted' END AS Status FROM TSPL_SALE_INVOICE_HEAD  "
            Dim Qry As String = "select [Document No],[Invoice No],[Date],[Container Deposite], [Invoice Amt] ,[Total Invoice Amt],[Balance Amt] ,Status from (" 'SELECT customer_code as Cust_Code, document_code AS [Invoice No], COnvert(Varchar, document_date, 103) AS [Date], 0 AS [Container Deposite], Total_Amt AS [Invoice Amt], (ISNULL(Total_Amt, 0)) AS [Total Invoice Amt], 0 AS [Balance Amt], CASE When posting_date is not null THEN 'Posted' ELSE 'Unposted' END AS Status FROM TSPL_SD_SALE_INVOICE_HEAD  "
            'Qry += "   union all"
            Qry += " SELECT customer_code as cust_code, Document_No  AS [Document No], Against_Sale_No as [Invoice No], Against_Sale_Return_No as [Against Sale Return], COnvert(Varchar, Document_Date , 103) AS [Date], 0 AS [Container Deposite], Document_Total  AS [Invoice Amt], (ISNULL(0, 0)+ISNULL(Document_Total, 0)) AS [Total Invoice Amt], Balance_Amt AS [Balance Amt], CASE When Status ='1' THEN 'Posted' ELSE 'Unposted' END AS Status FROM TSPL_Customer_Invoice_Head  )Invoice "
            Qry += " WHERE Cust_Code='" + fndCustomer.Value + "' "
            Qry += " AND CONVERT(DATE, Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103)"

            ClearGV1()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                FormatGrid()
            Else
                Throw New Exception("No Sale Invoice Found Against '" + txtCustName.Text + "' Between '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "'")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.Columns("Document No").Width = 141
        gv1.Columns("Invoice No").Width = 141
        'gv1.Columns("Against Sale Return").Width = 141

        gv1.Columns("Date").Width = 100

        gv1.Columns("Container Deposite").Width = 131
        gv1.Columns("Container Deposite").HeaderText = "Container Deposit"

        gv1.Columns("Invoice Amt").Width = 131

        gv1.Columns("Total Invoice Amt").Width = 131

        gv1.Columns("Balance Amt").Width = 131

        gv1.Columns("Status").Width = 111
        '---------------Total of Container Deposite, Invoice Amt, Total Invoice Amt,Balance Amt----- 
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim SumContainerDp As New GridViewSummaryItem("Container Deposite", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumContainerDp)
        Dim SumInvAmt As New GridViewSummaryItem("Invoice Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumInvAmt)
        Dim SumTTLInvAmt As New GridViewSummaryItem("Total Invoice Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumTTLInvAmt)
        Dim SumBalAmt As New GridViewSummaryItem("Balance Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumBalAmt)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        '--------------------------------------------------------------------------------------------
    End Sub

    Private Sub MasterTemplate_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If gv1.CurrentRow IsNot Nothing Then
            Dim strInvoiceNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Document No").Value)
            If clsCommon.myLen(strInvoiceNo) > 0 Then
                FillFinalGrid(strInvoiceNo)
            End If
        End If
    End Sub

    Private Sub FillFinalGrid(ByVal InvNo As String)
        Try
            Dim Qry As String = " SELECT DocType, DocNo, CONVERT(VARCHAR, Date, 103) AS [Date], Amount, Status  FROM ("
            Qry += "SELECT 'Receipt' AS [DocType], TSPL_RECEIPT_HEADER.Receipt_No AS [DocNo], CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Receipt_Date, 103) as [Date],  TSPL_RECEIPT_DETAIL.Applied_Amount AS [Amount], CASE WHEN TSPL_RECEIPT_HEADER.Posted='Y' THEN 'Posted' ELSE 'Unposted' end AS [Status] "
            Qry += " FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No "
            Qry += " WHERE TSPL_RECEIPT_DETAIL.Document_No='" + InvNo + "'"
            Qry += " UNION ALL"
            Qry += " SELECT 'AR Adjustment' AS [DocType], TSPL_Receipt_Adjustment_Header.Adjustment_No AS [DocNo], CONVERT(VARCHAR,TSPL_Receipt_Adjustment_Header.Adjustment_Date, 103) AS [Date], TSPL_Receipt_Adjustment_Header.Adjustment_Amount AS [Amount], CASE When TSPL_Receipt_Adjustment_Header.Is_Post='Y' THEN 'Posted' ELSE 'Unposted' END AS Status "
            Qry += " FROM TSPL_Receipt_Adjustment_Header  WHERE Doc_No='" + InvNo + "'"
            Qry += " UNION ALL"
            Qry += " SELECT 'Empty Adjustment' AS [DocType], TSPL_ADJUSTMENT_HEADER.Adjustment_No AS [DocNo], CONVERT(VARCHAR,TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) AS [Date], (Select SUM(ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0)) FROM TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) AS [Amount] , CASE When TSPL_ADJUSTMENT_HEADER.Posted='Y' THEN 'Posted' ELSE 'Unposted' END AS Status "
            Qry += " FROM TSPL_ADJUSTMENT_HEADER  "
            Qry += " WHERE TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' AND TSPL_ADJUSTMENT_HEADER.Document_No='" + InvNo + "'"
            Qry += " UNION ALL"
            Qry += " SELECT 'Sale Return' AS [DocType], TSPL_SD_SALE_RETURN_HEAD.Document_Code AS DocNo, CONVERT(VARCHAR,TSPL_SD_SALE_RETURN_HEAD.Document_Date, 103) AS [Date], (ISNULL(Total_Amt, 0)) AS [Amount], CASE WHEN TSPL_SD_SALE_RETURN_HEAD.Posting_Date Is not null THEN 'Posted' ELSE 'Unposted' END AS [Status] "
            Qry += " FROM TSPL_SD_SALE_RETURN_HEAD WHERE TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No='" + InvNo + "'"
            Qry += " union all SELECT 'Sale Return' AS [DocType], TSPL_SD_SALE_RETURN_HEAD.Document_Code AS DocNo, CONVERT(VARCHAR,TSPL_SD_SALE_RETURN_HEAD.Document_Date, 103) AS [Date], (ISNULL(Total_Amt, 0)) AS [Amount], CASE WHEN TSPL_SD_SALE_RETURN_HEAD.Posting_Date Is not null THEN 'Posted' ELSE 'Unposted' END AS [Status] "
            Qry += " FROM TSPL_SD_SALE_RETURN_HEAD WHERE TSPL_SD_SALE_RETURN_HEAD.Document_Code in (select Against_Sale_Return_No from TSPL_Customer_Invoice_Head where Document_No='" + InvNo + "')"
            Qry += " ) Final"

            ClearGV2()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                gv2.DataSource = dt
                FormatGrid2()
            Else
                Throw New Exception("No Document Found Against Document No '" + InvNo + "'")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid2()
        gv2.MasterTemplate.SummaryRowsBottom.Clear()
        gv2.Columns("DocType").Width = 141
        gv2.Columns("DocType").HeaderText = "Document Type"

        gv2.Columns("DocNo").Width = 141
        gv2.Columns("DocNo").HeaderText = "Document No"

        gv2.Columns("Date").Width = 100

        gv2.Columns("Amount").Width = 131

        gv2.Columns("Status").Width = 111
        '---------------Total of Amount------------------- 
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim SumAmt As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SumAmt)
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ClearGV1()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub
    Private Sub ClearGV2()
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting, gv2.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub
End Class
