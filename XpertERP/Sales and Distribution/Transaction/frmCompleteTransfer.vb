Imports common
Imports System.Data.SqlClient

Public Class FrmCompleteTransfer
    Inherits FrmMainTranScreen
    Dim ArrCompleteTranfer As List(Of String)
    Dim colLineNo As String = "SNO"
    Dim colShipmentNo As String = "SHIPEMNTNO"
    Dim colInvoiceNo As String = "INVOICENO"
    Dim colCustName As String = "CUSTOMERCODE"
    Dim colCustCode As String = "CUSTOMERNAME"

    Private Sub FrmCompleteTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            ''  funReset()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            postdata()

        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmCompleteTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtFromDate.Value
        LoadTransfer()
        SetUserMgmtNew()
        loadBlankGridPostedInvoice()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmCompleteTransfer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        btnPostShipment.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Function GetQueryOfLoadTransfer(ByVal WhrCls As String) As String
        Dim qry As String = "select xxx.Code,xxx.Date,xxx.[Route Code],xxx.Route,xxx.[Location Code],xxx.Location,xxx.LOAmount as [Loadout Amt],(xxx.LIAmt) as [Loadin Amt]," + Environment.NewLine
        qry += " CONVERT(decimal(18,2),ROUND( xxx.EmtpyValue,2)) as [Empty Amount],(xxx.LOAmount-xxx.EmtpyValue-xxx.LIAmt) as [Provisional Sale Amt],CONVERT(decimal(18,2),ROUND( xxx.InvAmountNew,2)) as [Gross Sale Amt],Total_Invoice_Amt as [Invoice Amount],(xxx.LOAmount-xxx.LIAmt-xxx.EmtpyValue-xxx.InvAmountNew) as [Balance Amt]  from( " + Environment.NewLine



        qry += " select TSPL_TRANSFER_HEAD.Transfer_No as Code,CONVERT(varchar(11), TSPL_TRANSFER_HEAD.Transfer_Date,103) as Date,TSPL_TRANSFER_HEAD.Route_No as [Route Code],TSPL_TRANSFER_HEAD.Route_Desc as Route,TSPL_TRANSFER_HEAD.From_Location as [Location Code],TSPL_TRANSFER_HEAD.FromLoc_Desc as Location,TSPL_TRANSFER_HEAD.Total_Transfer_Amount as LOAmount," + Environment.NewLine
        qry += " ( " + Environment.NewLine
        qry += " select SUM( xx.EmptyAmt * RI) from" + Environment.NewLine
        qry += " ( select   Empty_Value*Item_Qty as EmptyAmt,1 as RI from TSPL_TRANSFER_DETAIL as innerTransDetailLO where innerTransDetailLO.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No" + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select   Empty_Value*(LoadIn_Qty+Leak+Burst+Shortage) as EmptyAmt,-1 as RI from TSPL_TRANSFER_DETAIL as innerTransDetailLI" + Environment.NewLine
        qry += " left outer join TSPL_TRANSFER_HEAD as innerTransHeadLI on innerTransHeadLI.Transfer_No=innerTransDetailLI.Transfer_No" + Environment.NewLine
        qry += " where innerTransHeadLI.Load_Out_No=TSPL_TRANSFER_HEAD.Transfer_No)xx" + Environment.NewLine
        qry += " ) as EmtpyValue," + Environment.NewLine
        qry += " (select ISNULL( SUM(Total_Transfer_Amount),0)  from TSPL_TRANSFER_HEAD as LoadInTable where LoadInTable.Transfer_Type='LI' and LoadInTable.Load_Out_No=TSPL_TRANSFER_HEAD.Transfer_No) as LIAmt," + Environment.NewLine
        qry += " (select isnull( SUM(ISNULL( TSPL_SALE_INVOICE_DETAIL.Invoice_Qty*TSPL_SALE_INVOICE_DETAIL.Price_To_Show,0)),0)  " + Environment.NewLine
        qry += " from TSPL_SHIPMENT_MASTER " + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No " + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' and TSPL_SHIPMENT_MASTER.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No) as InvAmountNew," + Environment.NewLine
        qry += "  (select isnull( SUM( TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt),0)  " + Environment.NewLine
        qry += "  from TSPL_SHIPMENT_MASTER " + Environment.NewLine
        qry += "  left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No " + Environment.NewLine
        qry += "  where TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' and TSPL_SHIPMENT_MASTER.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No) as Total_Invoice_Amt" + Environment.NewLine
        qry += " from TSPL_TRANSFER_HEAD  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_HEAD.To_Location  where TSPL_TRANSFER_HEAD.Post='Y' and TSPL_LOCATION_MASTER.Location_Type='Logical' and TSPL_TRANSFER_HEAD.Transfer_Type='LO' and TSPL_TRANSFER_HEAD.Transfer_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_TRANSFER_HEAD.Transfer_Date <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' " + Environment.NewLine

        qry += " " + WhrCls + " )xxx order by  xxx.Date, xxx.Code"

        Return qry
    End Function

    Private Sub LoadTransfer()
        Dim WhrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls = " AND TSPL_TRANSFER_HEAD.From_Location in (" + objCommonVar.strCurrUserLocations + " )"
        End If

        
        Dim qry As String = GetQueryOfLoadTransfer(WhrCls)

        If chkTransferWithMismatchZero.Checked Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arr As ArrayList = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Code")))
            Next
            Dim baseQty As String = clsShipmentMaster.GetTranferForCompleteQuery(arr)
            baseQty = " select Transfer_No from(" + baseQty + ") xxxxxx group by Transfer_No having sum(BalanceQty)=0"
            dt = clsDBFuncationality.GetDataTable(baseQty)

            Dim arrTransferNo As List(Of String) = New List(Of String)

            For Each dr As DataRow In dt.Rows
                arrTransferNo.Add(clsCommon.myCstr(dr("Transfer_No")))
            Next
            If arrTransferNo IsNot Nothing AndAlso arrTransferNo.Count > 0 Then
                WhrCls += " and TSPL_TRANSFER_HEAD.Transfer_No in (" + clsCommon.GetMulcallString(arrTransferNo) + ")"
                qry = GetQueryOfLoadTransfer(WhrCls)
            End If

        End If

        cbgTransferNo.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTransferNo.ValueMember = "Code"
    End Sub

    Private Sub txtToDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        LoadData(False)
    End Sub

    Sub LoadData(ByVal isForDiffAmt As Boolean)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        gv2.DataSource = Nothing
        gv2.Columns.Clear()
        gv2.Rows.Clear()
        gv2.GroupDescriptors.Clear()
        gv2.MasterTemplate.SummaryRowsBottom.Clear()

        If cbgTransferNo.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Transfer No", Me.Text)
            Exit Sub
        End If

        Dim dt As DataTable = clsShipmentMaster.GetTranferForComplete(cbgTransferNo.CheckedValue, ArrCompleteTranfer)

        gv1.DataSource = dt
        SetGridFormationOFGV1()

        If cbgTransferNo.CheckedValue IsNot Nothing AndAlso cbgTransferNo.CheckedValue.Count > 0 Then
            Dim qry As String = "select  CAST("
            If isForDiffAmt Then
                qry += " case when ABS(isnull( TSPL_SALE_INVOICE_HEAD.Mannual_Invoice_Amt,0)-(TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt))<=15 then 1 else 0 end"
            Else
                qry += " 0 "
            End If

            qry += " as bit ) as SEL, TSPL_SHIPMENT_MASTER.Transfer_No,ABS(isnull( TSPL_SALE_INVOICE_HEAD.Mannual_Invoice_Amt,0)-(TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt)) as DiffAmt ,TSPL_SHIPMENT_MASTER.Shipment_No,convert(varchar(11), TSPL_SHIPMENT_MASTER.Shipment_Date,103) as Shipment_Date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,convert(varchar(11), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,  ISNULL( TSPL_SALE_INVOICE_HEAD.Mannual_Invoice_Amt,0) as Mannual_Invoice_Amt,(TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt) as InvoiceAmt "
            qry += " from TSPL_SHIPMENT_MASTER "
            qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No"
            qry += " where TSPL_SHIPMENT_MASTER.Transfer_No in(" + clsCommon.GetMulcallString(cbgTransferNo.CheckedValue) + ") and TSPL_SHIPMENT_MASTER.Is_Post='N'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv2.DataSource = dt1
            SetGridFormationOFGV2()
        End If
    End Sub

    Sub SetGridFormationOFGV2()
        gv2.MasterTemplate.AllowAddNewRow = False
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = False
        Next



        gv2.Columns("SEL").ReadOnly = False
        gv2.Columns("SEL").IsVisible = True
        gv2.Columns("SEL").Width = 30
        gv2.Columns("SEL").HeaderText = " "


        gv2.Columns("Transfer_No").IsVisible = True
        gv2.Columns("Transfer_No").Width = 150
        gv2.Columns("Transfer_No").HeaderText = "Transfer No"

        gv2.Columns("Shipment_No").IsVisible = True
        gv2.Columns("Shipment_No").Width = 150
        gv2.Columns("Shipment_No").HeaderText = "Shipment No"

        gv2.Columns("Shipment_Date").IsVisible = True
        gv2.Columns("Shipment_Date").Width = 100
        gv2.Columns("Shipment_Date").HeaderText = "Shipment Date"


        gv2.Columns("Sale_Invoice_No").IsVisible = True
        gv2.Columns("Sale_Invoice_No").Width = 100
        gv2.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

        gv2.Columns("Sale_Invoice_Date").IsVisible = True
        gv2.Columns("Sale_Invoice_Date").Width = 100
        gv2.Columns("Sale_Invoice_Date").HeaderText = "Sale Invoice Date"

        gv2.Columns("Cust_Code").IsVisible = True
        gv2.Columns("Cust_Code").Width = 100
        gv2.Columns("Cust_Code").HeaderText = "Customer Code"

        gv2.Columns("Cust_Name").IsVisible = True
        gv2.Columns("Cust_Name").Width = 200
        gv2.Columns("Cust_Name").HeaderText = "Customer Name"

        gv2.Columns("Mannual_Invoice_Amt").IsVisible = True
        gv2.Columns("Mannual_Invoice_Amt").Width = 70
        gv2.Columns("Mannual_Invoice_Amt").HeaderText = "Mannual Amount"

        gv2.Columns("InvoiceAmt").IsVisible = True
        gv2.Columns("InvoiceAmt").Width = 70
        gv2.Columns("InvoiceAmt").HeaderText = "Invoice Amount"

        gv2.Columns("DiffAmt").IsVisible = True
        gv2.Columns("DiffAmt").Width = 70
        gv2.Columns("DiffAmt").HeaderText = "Difference Amount"

        gv2.ShowGroupPanel = False
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Transfer_No").IsVisible = True
        gv1.Columns("Transfer_No").Width = 100
        gv1.Columns("Transfer_No").HeaderText = "Transfer No"

        gv1.Columns("Transfer_Date").IsVisible = True
        gv1.Columns("Transfer_Date").Width = 100
        gv1.Columns("Transfer_Date").HeaderText = "Transfer Date"

        gv1.Columns("Route_No").IsVisible = True
        gv1.Columns("Route_No").Width = 100
        gv1.Columns("Route_No").HeaderText = "Route No"


        gv1.Columns("Route_Desc").HeaderText = "Route Description"

        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").Width = 100
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("LoadoutQty").IsVisible = True
        gv1.Columns("LoadoutQty").Width = 100
        gv1.Columns("LoadoutQty").HeaderText = "Loadout"

        gv1.Columns("LoadinQty").IsVisible = True
        gv1.Columns("LoadinQty").Width = 100
        gv1.Columns("LoadinQty").HeaderText = "Loadin"


        gv1.Columns("ProposedSale").IsVisible = True
        gv1.Columns("ProposedSale").Width = 100
        gv1.Columns("ProposedSale").HeaderText = "Provisional Sale"


        gv1.Columns("GrossSaleQty").IsVisible = True
        gv1.Columns("GrossSaleQty").Width = 100
        gv1.Columns("GrossSaleQty").HeaderText = "Gross Sale"

        gv1.Columns("DiscountQty").IsVisible = True
        gv1.Columns("DiscountQty").Width = 100
        gv1.Columns("DiscountQty").HeaderText = "Discount"

        gv1.Columns("NetSale").IsVisible = True
        gv1.Columns("NetSale").Width = 100
        gv1.Columns("NetSale").HeaderText = "Net Sale"

        gv1.Columns("BalanceQty").IsVisible = True
        gv1.Columns("BalanceQty").Width = 100
        gv1.Columns("BalanceQty").HeaderText = "Balance"

        gv1.GroupDescriptors.Add(New GridGroupByExpression("Transfer_No as Transfer_No format ""{0}: {1}"" Group By Transfer_No"))
        gv1.MasterTemplate.ExpandAllGroups()
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("LoadoutQty", "{0:F2}", GridAggregateFunction.Sum)

        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("LoadinQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("ProposedSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("GrossSaleQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("DiscountQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("NetSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("BalanceQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.DataSource IsNot Nothing AndAlso gv1.RowCount > 0 Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Code").Value)
                Dim strTransferNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Transfer_No").Value)
                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strTransferNo) > 0 Then
                    Dim dt As DataTable = clsShipmentMaster.GetTranferInvoices(strTransferNo, strICode)
                    Dim frm As New FrmFreeGrid()
                    frm.strFormName = "Gross sale of Transfer - " + strTransferNo + " and Item - " + strICode
                    frm.dt = dt
                    frm.ReportID = "CompleteTransferItem"
                    frm.arrFooter = New List(Of String)
                    frm.arrFooter.Add("Sale FC")
                    frm.arrFooter.Add("Sale FB")
                    frm.arrFooter.Add("Sale Total")
                    frm.arrFooter.Add("FOC FC")
                    frm.arrFooter.Add("FOC FB")
                    frm.arrFooter.Add("FOC Total")
                    frm.arrFooter.Add("Total FC")
                    frm.arrFooter.Add("Total FB")
                    frm.arrFooter.Add("Total")
                    frm.WindowState = FormWindowState.Maximized
                    frm.ShowDialog()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Dim iiDeadlockErrors As Integer
    Dim intCounter As Integer
   
    Private Sub btnPostShipment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostShipment.Click
        loadBlankGridPostedInvoice()
        RadPageView1.SelectedPage = RadPageViewPage3


        Dim ii As Integer = 0
        For jj As Integer = 0 To gv2.RowCount - 1
            If clsCommon.myCBool(gv2.Rows(jj).Cells("SEL").Value) Then
                ii = ii + 1
                arrAllInvoice.Add(clsCommon.myCstr(gv2.Rows(jj).Cells("Sale_Invoice_No").Value))
            End If
        Next

        If ii = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Invoice to post", Me.Text)
            Exit Sub
        End If

        Dim strMessage As String = "Post " + clsCommon.myCstr(ii) + " Selected Invoices " + Environment.NewLine
        If ii > 100 Then
            strMessage += "But you can post maximum 100 Invoices.Post Top 100 Invoice" + Environment.NewLine
        End If

        If common.clsCommon.MyMessageBoxShow(Me, strMessage + "Are You Sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        intCounter = 0
        iiDeadlockErrors = 1
        arrPostedInvoice = New List(Of String)

        postdata()
    End Sub

    Dim arrPostedInvoice As List(Of String) = New List(Of String)
    Dim arrAllInvoice As List(Of String) = New List(Of String)
    Sub postdata()
        Dim strDocNo As String = ""
        Try
            If gv2.DataSource IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
                Dim strBankCode As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptDefaultBankForSettlement, clsFixedParameterCode.LOReceiptDefaultBankForSettlement, Nothing)
                If clsCommon.myLen(strBankCode) <= 0 Then
                    Throw New Exception("Default Bank code not found")
                End If
                Dim strPaymentCode As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptPaymentTypeForSettlement, clsFixedParameterCode.LOReceiptPaymentTypeForSettlement, Nothing)
                If clsCommon.myLen(strPaymentCode) <= 0 Then
                    Throw New Exception("Default Payemnt code not found")
                End If
                'clsCommon.ProgressBarShow()

                Dim qry As String
                'Dim strDocNo As String = ""
                Try
                    For j As Integer = 0 To gv2.RowCount - 1
                        Dim InvNo As String = clsCommon.myCstr(gv2.Rows(j).Cells("Sale_Invoice_No").Value)
                        If clsCommon.myCBool(gv2.Rows(j).Cells("SEL").Value) AndAlso Not arrPostedInvoice.Contains(InvNo) Then
                            Dim strTransferNo As String = clsCommon.myCstr(gv2.Rows(j).Cells("Transfer_No").Value)
                            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                            Try
                                intCounter += 1
                                Dim msg As String = ""
                                Dim dtStartTime As DateTime = DateTime.Now

                                strDocNo = clsCommon.myCstr(gv2.Rows(j).Cells("Shipment_No").Value)
                                clsShipmentMaster.postShipment(strDocNo, trans)

                                Dim dtEndTime As DateTime = DateTime.Now
                                Dim span As TimeSpan = dtEndTime.Subtract(dtStartTime)
                                msg += "Post Shipment :" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds)


                                ''If intCounter Mod 2 = 0 Then
                                ''    Throw New Exception("deadlocked occred")
                                ''End If


                                ''dtStartTime = DateTime.Now
                                ''clsSaleHead.createInvoice(InvNo, strDocNo, trans)
                                ''dtEndTime = DateTime.Now
                                ''span = dtEndTime.Subtract(dtStartTime)
                                ''msg += " Create Invoice :" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds)

                                dtStartTime = DateTime.Now
                                clsSaleHead.Postdata(InvNo, trans)
                                dtEndTime = DateTime.Now
                                span = dtEndTime.Subtract(dtStartTime)
                                msg += " Post Invoice :" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds)


                                dtStartTime = DateTime.Now
                                qry = "select Adjustment_No  from TSPL_Receipt_Adjustment_Header  where Doc_No='" + InvNo + "' and (Is_Post is null or Is_Post <> 'Y')"
                                Dim dtReceiptAdjustment As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtReceiptAdjustment IsNot Nothing AndAlso dtReceiptAdjustment.Rows.Count > 0 Then
                                    For Each dr As DataRow In dtReceiptAdjustment.Rows
                                        clsAdjustmentEntryReceivables.FunPost(clsCommon.myCstr(dr("Adjustment_No")), trans)
                                    Next
                                End If
                                dtEndTime = DateTime.Now
                                span = dtEndTime.Subtract(dtStartTime)
                                msg += " Adustment:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds)



                                dtStartTime = DateTime.Now
                                clsReceiptHeader.ReciepEntryWithPostOfInvoice(InvNo, strBankCode, strPaymentCode, trans)
                                dtEndTime = DateTime.Now
                                span = dtEndTime.Subtract(dtStartTime)
                                msg += " Receipt:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds)



                                qry = " insert into temp_deadlock(shipment_no,NoofDeadlock,MachineName,Ptime,MSG) values('" + strDocNo + "','0','" + My.User.Name + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "yyyy/MM/dd hh:mm:ss tt") + "','" + msg + "')"

                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                trans.Commit()
                                arrPostedInvoice.Add(InvNo)

                                ''Filling data in posted Invoice
                                gvPostedInvoice.Rows.AddNew()
                                gvPostedInvoice.Rows(gvPostedInvoice.Rows.Count - 1).Cells(colLineNo).Value = intCounter
                                gvPostedInvoice.Rows(gvPostedInvoice.Rows.Count - 1).Cells(colCustCode).Value = clsCommon.myCstr(gv2.Rows(j).Cells("Cust_Code").Value)
                                gvPostedInvoice.Rows(gvPostedInvoice.Rows.Count - 1).Cells(colCustName).Value = clsCommon.myCstr(gv2.Rows(j).Cells("Cust_Name").Value)
                                gvPostedInvoice.Rows(gvPostedInvoice.Rows.Count - 1).Cells(colShipmentNo).Value = strDocNo
                                gvPostedInvoice.Rows(gvPostedInvoice.Rows.Count - 1).Cells(colInvoiceNo).Value = InvNo
                                gvPostedInvoice.Refresh()
                                ''End of Filling data in posted Invoice

                                System.Threading.Thread.Sleep(3000)
                                If intCounter >= 100 Then
                                    ''clsCommon.ProgressBarHide()
                                    ''common.clsCommon.MyMessageBoxShow("Data Posted Successfully")
                                    Exit For
                                End If

                            Catch ex As Exception
                                trans.Rollback()
                                If Not ex.Message.Contains("Already Post on") Then
                                    Throw New Exception(ex.Message)
                                End If
                            End Try
                        End If
                    Next
                Catch ex As Exception
                    ''clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try

                ''clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                Me.Close()
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Transfer number not found to Post", Me.Text)
            End If

        Catch ex As Exception
            If ex.Message.Contains("deadlocked") Or ex.Message.Contains("PK_TSPL_RECEIPT_HEADER") Then
                iiDeadlockErrors += 1
                Dim qry1 As String = " insert into temp_deadlock(shipment_no,NoofDeadlock,MachineName,Ptime) values('" + strDocNo + "'," + clsCommon.myCstr(iiDeadlockErrors) + ",'" + My.User.Name + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "yyyy/MM/dd hh:mm:ss tt") + "')"
                clsDBFuncationality.ExecuteNonQuery(qry1)
                If iiDeadlockErrors >= 15 Then
                    Me.Close()
                    Exit Sub
                End If
                System.Threading.Thread.Sleep(3000)
                postdata()
            Else
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                LoadData(False)
            End If
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        SelectInvocices(True)
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        SelectInvocices(False)
    End Sub

    Private Sub SelectInvocices(ByVal isCheck As Boolean)
        If gv2.DataSource IsNot Nothing AndAlso gv2.RowCount > 0 Then
            For jj As Integer = 0 To gv2.RowCount - 1
                gv2.Rows(jj).Cells("SEL").Value = isCheck
            Next
        End If
    End Sub

    Private Sub loadBlankGridPostedInvoice()
        gvPostedInvoice.Rows.Clear()
        gvPostedInvoice.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPostedInvoice.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Customer Code"
        repoCustCode.Name = colCustCode
        repoCustCode.Width = 100
        gvPostedInvoice.MasterTemplate.Columns.Add(repoCustCode)

        Dim repoCustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustName.FormatString = ""
        repoCustName.HeaderText = "Customer Name"
        repoCustName.Name = colCustName
        repoCustName.Width = 150
        gvPostedInvoice.MasterTemplate.Columns.Add(repoCustName)

        Dim repoShipmentNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShipmentNo.FormatString = ""
        repoShipmentNo.HeaderText = "Shipment No"
        repoShipmentNo.Name = colShipmentNo
        repoShipmentNo.Width = 150
        repoShipmentNo.ReadOnly = True
        gvPostedInvoice.MasterTemplate.Columns.Add(repoShipmentNo)

        Dim repoInvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceNo.FormatString = ""
        repoInvoiceNo.HeaderText = "Invoice No"
        repoInvoiceNo.Name = colInvoiceNo
        repoInvoiceNo.Width = 200
        repoInvoiceNo.ReadOnly = True
        gvPostedInvoice.MasterTemplate.Columns.Add(repoInvoiceNo)

        gvPostedInvoice.AllowDeleteRow = True
        gvPostedInvoice.AllowAddNewRow = False
        gvPostedInvoice.ShowGroupPanel = False
        gvPostedInvoice.AllowColumnReorder = False
        gvPostedInvoice.AllowRowReorder = False
        gvPostedInvoice.EnableSorting = False
        gvPostedInvoice.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPostedInvoice.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub btnRefreshTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshTransfer.Click
        LoadTransfer()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        LoadData(True)
    End Sub
End Class
