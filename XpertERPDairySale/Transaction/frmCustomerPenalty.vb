Imports System.Data.SqlClient
Imports common
Imports Telerik
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmCustomerPenalty
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colSNo As String = "colSNo"
    Const colDate As String = "colDate"
    Const colSaleAmount As String = "colSaleAmount"
    Const colDepositAmount As String = "colDepositAmount"
    Const colCurrBalanceAmount As String = "colCurrBalanceAmount"
    Const colBalanceAmt As String = "colBalanceAmt"
    Const colPenalty As String = "colPenalty"
    Dim isLoadData As Boolean = False
    Dim isCopyData As Boolean = False
    Dim j As Integer = 0
    Dim obj As New clsCustomerPenalty()
    Dim objtr As New clsCustomerPenaltyDetail()

#End Region

    Private Sub frmCustomerPenalty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        SetUserMgmtNew()
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim repoSNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNO.FormatString = ""
        repoSNO.HeaderText = "SNo"
        repoSNO.Name = colSNo
        repoSNO.Width = 40
        repoSNO.ReadOnly = True
        repoSNO.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoSNO)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd/MM/yyyy"
        repoDate.FormatString = "{0:dd/MM/yyyy}"
        repoDate.HeaderText = "Date"
        repoDate.Name = colDate
        repoDate.Width = 120
        repoDate.ReadOnly = True
        repoDate.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoDate)

        Dim repoSaleAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSaleAmount.FormatString = "{0:n2}"
        repoSaleAmount.HeaderText = "Sale Amount"
        repoSaleAmount.Name = colSaleAmount
        repoSaleAmount.Width = 120
        repoSaleAmount.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoSaleAmount)

        Dim repoDepositAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDepositAmount.FormatString = "{0:n2}"
        repoDepositAmount.HeaderText = "Deposit Amount"
        repoDepositAmount.Name = colDepositAmount
        repoDepositAmount.Width = 140
        repoDepositAmount.ReadOnly = True
        repoDepositAmount.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoDepositAmount)

        Dim repoCurrBalanceAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCurrBalanceAmount.FormatString = "{0:n2}"
        repoCurrBalanceAmount.HeaderText = "Current Balance Amt"
        repoCurrBalanceAmount.Name = colCurrBalanceAmount
        repoCurrBalanceAmount.Width = 130
        repoCurrBalanceAmount.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoCurrBalanceAmount)

        Dim repoBalanceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalanceAmt.FormatString = "{0:n2}"
        repoBalanceAmt.HeaderText = "Balance Amt"
        repoBalanceAmt.Name = colBalanceAmt
        repoBalanceAmt.Width = 130
        repoBalanceAmt.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoBalanceAmt)

        Dim repoPenalty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPenalty.FormatString = "{0:n2}"
        repoPenalty.HeaderText = "Penalty"
        repoPenalty.Name = colPenalty
        repoPenalty.Width = 130
        repoPenalty.ReadOnly = True
        repoPenalty.ShowUpDownButtons = False
        Gv1.MasterTemplate.Columns.Add(repoPenalty)

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.AutoSizeRows = False
        Gv1.Rows.AddNew()
        ReStoreGridLayoutgv1()
    End Sub

    Private Sub ReStoreGridLayoutgv1()
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To Gv1.Columns.Count - 1 Step ii & 1
                    Gv1.Columns(ii).IsVisible = False
                    Gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                Gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        btnGo.Enabled = val
    End Sub
    Function AllowToSave() As Boolean
        If txtPenaltyPer.Value = 0 Then
            txtPenaltyPer.Focus()
            Throw New Exception("Penalty % can't be blank.")
        End If
        Dim isDocExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(1) from TSPL_CUSTOMER_PENALTY where convert(date,from_date ,103)>=  convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,To_Date,103) >= convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)") > 0)
        'If isDocExist Then
        '    Throw New Exception("Document already exist in this cycle")
        'End If
        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                obj = New clsCustomerPenalty()
                obj.Document_No = txtDocumentNo.Value
                obj.Penalty_Per = txtPenaltyPer.Value
                obj.Document_date = clsCommon.myCDate(txtDocumentDate.Value)
                obj.From_Date = clsCommon.myCDate(txtFromDate.Value)
                obj.To_Date = clsCommon.myCDate(txtToDate.Value)
                obj.Cust_Code = txtDistributor.Value
                obj.Remarks = txtRemarks.Text
                obj.Total_Penalty = Math.Round(clsCommon.myCdbl(lblTotalPenalty.Text), 2)
                obj.Arr = New List(Of clsCustomerPenaltyDetail)

                For Each grow As GridViewRowInfo In Gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colDate).Value)) > 0 Then
                        Dim objTr As New clsCustomerPenaltyDetail()
                        objTr.Invoice_Date = clsCommon.myCDate(grow.Cells(colDate).Value)
                        objTr.Sale_Amt = clsCommon.myCDecimal((grow.Cells(colSaleAmount).Value))
                        objTr.Deposit_Amt = clsCommon.myCDecimal((grow.Cells(colDepositAmount).Value))
                        objTr.Curr_Balance_Amt = clsCommon.myCDecimal((grow.Cells(colCurrBalanceAmount).Value))
                        objTr.Balance_Amt = clsCommon.myCDecimal((grow.Cells(colBalanceAmt).Value))
                        objTr.Penalty = clsCommon.myCDecimal((grow.Cells(colPenalty).Value))

                        objTr.ArrInvoiceAllDetails = TryCast(grow.Cells(colSaleAmount).Tag, List(Of clsCustomerPenaltyInvoiceDetail))
                        objTr.ArrReceiptAllDetails = TryCast(grow.Cells(colDepositAmount).Tag, List(Of clsCustomerPenaltyReceiptDetail))
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.Value = ""
        txtDistributor.Value = ""
        lblDistributorName.Text = ""
        lblTotalPenalty.Text = "0"
        btnSave.Enabled = True
        btnPost.Enabled = True
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()
        btnSave.Text = "Save"
        txtPenaltyPer.Value = 0
        txtRemarks.Text = ""
        LoadBlankGrid()
        isInsideLoadData = False
        btnDelete.Enabled = True
        lblStatus.Status = ERPTransactionStatus.Pending
        ReStoreGridLayoutgv1()
        EnableDisableControls(True)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click

        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" & txtDocumentNo.Value & "]" & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                obj.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (obj.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        Try
            If clsCommon.myLen(txtDocumentNo) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
            End If
            obj = New clsCustomerPenalty()
            txtDocumentNo.Value = obj.getFinder(txtDocumentNo.Value, isButtonClicked)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_CUSTOMER_PENALTY where Document_No='" & txtDocumentNo.Value & "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            Addnew()
            obj = New clsCustomerPenalty()
            obj = obj.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0) Then
                Addnew()
                isLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                EnableDisableControls(False)
                If obj.Status = 1 Then
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    'btnImport.Enabled = False
                Else
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = True
                    'btnImport.Enabled = True
                End If
                txtDocumentNo.Value = obj.Document_No
                txtDocumentDate.Value = obj.Document_date
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                txtPenaltyPer.Value = obj.Penalty_Per
                lblTotalPenalty.Text = obj.Total_Penalty
                txtRemarks.Text = obj.Remarks
                txtDistributor.Value = obj.Cust_Code
                lblDistributorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & txtDistributor.Value & "' "))
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each objtr As clsCustomerPenaltyDetail In obj.Arr
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSNo).Value = Gv1.Rows.Count
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDate).Value = objtr.Invoice_Date
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSaleAmount).Tag = objtr.ArrInvoiceAllDetails
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDepositAmount).Tag = objtr.ArrReceiptAllDetails
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSaleAmount).Value = objtr.Sale_Amt
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDepositAmount).Value = objtr.Deposit_Amt
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCurrBalanceAmount).Value = objtr.Curr_Balance_Amt
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = objtr.Balance_Amt
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPenalty).Value = objtr.Penalty
                        Gv1.Rows.AddNew()
                    Next
                End If

            End If
            isInsideLoadData = True
            isInsideLoadData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub frmCustomerPenalty_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIR
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseUnpost.Visible = True
            End If
            'Dim frmCustPenaltyInvoice = New frmCustomerPenaltyInvoiceDetails()
            'If isLoadData Then
            '    frmCustPenaltyInvoice.arr = obj.ArrInvoiceDetails
            'Else
            '    frmCustPenaltyInvoice.arr = objtr.ArrInvoiceAllDetails
            'End If
            'frmCustPenaltyInvoice.ShowDialog()
        End If
    End Sub

    Private Sub btnReverseUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseUnpost.Click
        Try
            obj = New clsCustomerPenalty()
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" & Environment.NewLine & "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If obj.ReverseAndUnpost(txtDocumentNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If (txtPenaltyPer.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Enter Penalty Percent", Me.Text)
            Exit Sub
        End If
        LoadBlankGrid()
        isLoadData = False
        LoadGridData(isLoadData)
    End Sub

    Private Sub LoadGridData(ByVal isLoadData As Boolean)
        Try
            If clsCommon.myLen(txtDistributor.Value) <= 0 Then
                Throw New Exception("Please select Distributor first.")
            End If
            Dim Finalqry As String = ""

            Dim Invoiceqry As String = " select Document_Code,max(InvoiceDate ) as InvoiceDate ,sum(Sale_Amt*RI) as Sale_Amt,0 as Deposit_Amt from ( select tspl_sd_sale_invoice_head.Document_Code, convert(date,tspl_sd_sale_invoice_head.Document_Date,103) as InvoiceDate ,tspl_sd_sale_invoice_head.Total_Amt AS Sale_Amt ,1 as RI,1 as chk from tspl_sd_sale_invoice_head 
            where  tspl_sd_sale_invoice_head.status=1 and  tspl_sd_sale_invoice_head.Document_Date >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "' and  tspl_sd_sale_invoice_head.Document_Date < = '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "'  and tspl_sd_sale_invoice_head.Customer_Code = '" & txtDistributor.Value & "'  
            union all
            select TSPL_CUSTOMER_PENALTY_INVOICE.Invoice_No,null as InvoiceDate ,tspl_sd_sale_invoice_head.Total_Amt as Sale_Amt,-1 as RI,0 as chk from TSPL_CUSTOMER_PENALTY_INVOICE  left outer join tspl_sd_sale_invoice_head on tspl_sd_sale_invoice_head.Document_Code=TSPL_CUSTOMER_PENALTY_INVOICE.Invoice_No 
            where tspl_sd_sale_invoice_head.Customer_Code = '" & txtDistributor.Value & "'  and TSPL_CUSTOMER_PENALTY_INVOICE.Document_No not in ('" & txtDocumentNo.Value & "') ) xx group by Document_Code having sum(chk)>0 and sum(Sale_Amt*ri)>0 "

            Dim dtInvoice As DataTable = clsDBFuncationality.GetDataTable(Invoiceqry)

            Dim Receiptqry As String = " select Document_Code,max(InvoiceDate ) as InvoiceDate ,0 as Sale_Amt,sum(Deposit_Amt*RI) as Deposit_Amt from ( select TSPL_RECEIPT_HEADER.Receipt_No as Document_Code, convert(date,Receipt_Date,103) as InvoiceDate, Receipt_Amount as Deposit_Amt,1 as RI ,1 as chk from TSPL_RECEIPT_HEADER 
            where Posted='Y' and  Receipt_Date >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "' and  Receipt_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") & "' 
            union all 
            select TSPL_CUSTOMER_PENALTY_RECEIPT.Receipt_No as Document_Code,null as InvoiceDate ,Receipt_Amount as Deposit_Amt,-1 as RI,0 as chk from TSPL_CUSTOMER_PENALTY_RECEIPT left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_CUSTOMER_PENALTY_RECEIPT.Receipt_No 
            where TSPL_RECEIPT_HEADER.Cust_Code = 'D050'  and TSPL_CUSTOMER_PENALTY_RECEIPT.Document_No not in ('" & txtDocumentNo.Value & "')  ) xx group by Document_Code having sum(chk)>0 and sum(Deposit_Amt*RI)>0 "
            Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(Receiptqry)

            Finalqry = " select ROW_NUMBER( ) over( order by InvoiceDate) as SNo,InvoiceDate,sum(Sale_Amt) as Sale_Amt,sum(Deposit_Amt) as Deposit_Amt,sum(Sale_Amt-Deposit_Amt) as Curr_Bal_Amt from (
             " & Invoiceqry & " " & Environment.NewLine & " union all " & Environment.NewLine & " " & Receiptqry & " )  xx group by InvoiceDate  order by InvoiceDate  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Finalqry)

            If dt.Rows.Count > 0 Then
                obj = New clsCustomerPenalty()
                objtr.ArrInvoiceAllDetails = New List(Of clsCustomerPenaltyInvoiceDetail)
                objtr.ArrReceiptAllDetails = New List(Of clsCustomerPenaltyReceiptDetail)

                For ii As Integer = 0 To dt.Rows.Count - 1
                    Dim Used_Amt As Decimal = 0
                    obj.ArrInvoiceDetails = New List(Of clsCustomerPenaltyInvoiceDetail)
                    obj.ArrReceiptDetails = New List(Of clsCustomerPenaltyReceiptDetail)
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSNo).Value = ii & 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDate).Value = dt.Rows(ii)("InvoiceDate")
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSaleAmount).Value = dt.Rows(ii)("Sale_Amt")
                    Dim InvoiceDate As DateTime = clsCommon.myCDate(dt.Rows(ii)("InvoiceDate"))
                    Dim row As DataRow() = dtInvoice.Select("InvoiceDate = #" & InvoiceDate.ToString("MM/dd/yyyy") & "#")
                    For Each dr As DataRow In row
                        Dim objInvoice As clsCustomerPenaltyInvoiceDetail = New clsCustomerPenaltyInvoiceDetail()
                        objInvoice.Invoice_No = clsCommon.myCstr(dr("Document_Code"))
                        objInvoice.Invoice_Amt = clsCommon.myCDecimal(dr("Sale_Amt"))
                        obj.ArrInvoiceDetails.Add(objInvoice)
                        objtr.ArrInvoiceAllDetails.Add(objInvoice)
                    Next
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSaleAmount).Tag = obj.ArrInvoiceDetails
                    row = dtReceipt.Select("InvoiceDate = #" & InvoiceDate.ToString("MM/dd/yyyy") & "#")
                    For Each dr As DataRow In row
                        Dim objReceipt As clsCustomerPenaltyReceiptDetail = New clsCustomerPenaltyReceiptDetail()
                        objReceipt.Receipt_No = clsCommon.myCstr(dr("Document_Code"))
                        objReceipt.Receipt_Amt = clsCommon.myCDecimal(dr("Deposit_Amt"))
                        obj.ArrReceiptDetails.Add(objReceipt)
                        objtr.ArrReceiptAllDetails.Add(objReceipt)
                    Next
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDepositAmount).Tag = obj.ArrReceiptDetails
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDepositAmount).Value = dt.Rows(ii)("Deposit_Amt")
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCurrBalanceAmount).Value = dt.Rows(ii)("Curr_Bal_Amt")
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = 0

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPenalty).Value = txtPenaltyPer.Value
                    UpdateCurrentRow(ii)
                    Gv1.Rows.AddNew()
                Next
                UpdateTotal()
                EnableDisableControls(False)
            Else
                Throw New Exception("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim dblCurrBalance_Amt As Double = 0
        Dim days As Integer = (New DateTime(txtFromDate.Value.Year() & 1, 1, 1) - New DateTime(txtFromDate.Value.Year, 1, 1)).Days
        If clsCommon.myLen(clsCommon.myCDate(Gv1.Rows(IntRowNo).Cells(colDate).Value)) > 0 Then
            dblCurrBalance_Amt = Gv1.Rows(IntRowNo).Cells(colCurrBalanceAmount).Value
            If IntRowNo > 0 Then
                Gv1.Rows(IntRowNo).Cells(colBalanceAmt).Value = dblCurrBalance_Amt + (Gv1.Rows(IntRowNo - 1).Cells(colBalanceAmt).Value)
            Else
                Gv1.Rows(IntRowNo).Cells(colBalanceAmt).Value = dblCurrBalance_Amt
            End If
            Gv1.Rows(IntRowNo).Cells(colPenalty).Value = (Gv1.Rows(IntRowNo).Cells(colBalanceAmt).Value * txtPenaltyPer.Value) / (100 * days)
        End If
    End Sub
    Sub UpdateTotal()
        Dim dblTotalPenalty As Decimal = 0
        For ii As Integer = 0 To Gv1.Rows.Count - 1
            dblTotalPenalty = dblTotalPenalty + clsCommon.myCdbl(Gv1.Rows(ii).Cells(colPenalty).Value)
        Next
        lblTotalPenalty.Text = Math.Round(clsCommon.myCdbl(dblTotalPenalty), 2)
    End Sub
    'Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
    '    clsCommon.MyExportToExcelGrid("", Gv1, Nothing, Me.Text)
    '    clsCommon.MyMessageBoxShow(Me, "Exported Successfully", Me.Text)
    'End Sub

    'Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
    '    Try
    '        Dim gvImport As New UserControls.MyRadGridView
    '        Me.Controls.Add(gvImport)
    '        Dim currentdate As Date = Date.Today
    '        If transportSql.importExcel(gvImport, "DCS Code", "DCS Name", "Share Captial Opening Amount", "Total Share Captial Deducted Amount", "Balance Amt", "Share Rate Per Share", "No of Share to be allocated", "Share Certificate No Start From", "Share Certificate No To") Then
    '            Dim arr As New List(Of String)
    '            Dim strDCSCode As String = ""
    '            Dim dtError As New DataTable
    '            dtError.Columns.Add("RowNo", GetType(Integer))
    '            dtError.Columns.Add("Error", GetType(String))
    '            Try
    '                If gvImport IsNot Nothing AndAlso gvImport.Rows.Count > 0 Then
    '                    clsCommon.ProgressBarPercentShow()
    '                    For ii As Integer = 0 To gvImport.Rows.Count - 1
    '                        Try
    '                            clsCommon.ProgressBarPercentUpdate(ii & 1, gvImport.Rows.Count, "Validating Data...")
    '                            If clsCommon.myLen(clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value)) <= 0 Then
    '                                Throw New Exception("DCS Code can't be blank !")
    '                            End If
    '                            strDCSCode = clsDBFuncationality.getSingleValue("Select VLC_Code_VLC_Uploader FROM TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" & clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value) & "' ")
    '                            If clsCommon.myLen(strDCSCode) <= 0 Then
    '                                Throw New Exception("DCS Uploader Code cannot exist in DCS Master")
    '                            End If
    '                            arr.Add(strDCSCode)
    '                        Catch ex As Exception
    '                            Dim dr As DataRow = dtError.NewRow()
    '                            dr("RowNo") = ii
    '                            dr("Error") = ex.Message
    '                            dtError.Rows.Add(dr)

    '                        End Try
    '                    Next
    '                    clsCommon.ProgressBarPercentHide()
    '                End If
    '                Try

    '                    If dtError.Rows.Count > 0 Then
    '                        Dim ff As New FrmFreeGrid
    '                        ff.ReportID = MyBase.Form_ID
    '                        ff.Text = "Share Allotment DCS Errors"
    '                        ff.dt = dtError
    '                        ff.ShowDialog()
    '                    ElseIf arr IsNot Nothing AndAlso arr.Count > 0 Then
    '                        Dim qry As String = "Valid Row [" & clsCommon.myCstr(arr.Count) & "] Do You want to Proceed"

    '                        If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '                            clsCommon.ProgressBarPercentShow()
    '                            For ii As Integer = 0 To gvImport.Rows.Count - 1

    '                                If clsCommon.myLen(gvImport.Rows(ii).Cells("DCS Code").Value) > 0 Then

    '                                    clsCommon.ProgressBarPercentUpdate((gvImport.Rows(ii).Index & 1) * 100 / (gvImport.Rows.Count & 1), "Importing  : " & (gvImport.Rows(ii).Index & 1) & "/" & gvImport.Rows.Count & "")
    '                                    Try
    '                                        Gv1.Rows(ii).Cells(colSNo).Value = ii & 1
    '                                        Gv1.Rows(ii).Cells(colDCSUploaderNo).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value)
    '                                        Gv1.Rows(ii).Cells(colDCSName).Value = clsDBFuncationality.getSingleValue("Select VLC_NAME FROM TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" & clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value) & "'")
    '                                        Gv1.Rows(ii).Cells(colVendorCode).Value = clsDBFuncationality.getSingleValue("Select VSP_CODE FROM TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader = '" & clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value) & "'")

    '                                        Gv1.Rows(ii).Cells(colShareCaptialOpeningAmount).Value = Math.Round(clsCommon.myCDecimal(gvImport.Rows(ii).Cells("Share Captial Opening Amount").Value), 2)

    '                                        Gv1.Rows(ii).Cells(colShareDeductedAmount).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Total Share Captial Deducted Amount").Value)
    '                                        'gv1.Rows(ii).Cells(colBalanceAmt).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Balance Amt").Value)
    '                                        Gv1.Rows(ii).Cells(colRatePerShare).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Share Rate Per Share").Value)
    '                                        UpdateCurrentRow(ii)
    '                                        'gv1.Rows(ii).Cells(colNoOfShareToBeAllocated).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Total Share Captial Deducted Amount").Value)
    '                                        txtPenaltyPer.Value = Gv1.Rows(ii).Cells(colRatePerShare).Value
    '                                        If clsCommon.myLen(txtDocumentNo.Value) = 0 Then
    '                                            If gv1.Rows.Count = gvImport.Rows.Count Then
    '                                            Else
    '                                                gv1.Rows.AddNew()
    '                                            End If
    '                                        End If

    '                                    Catch ex As Exception
    '                                        gv1.Rows.RemoveAt(ii)
    '                                        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '                                    End Try
    '                                End If

    '                            Next
    '                            clsCommon.ProgressBarPercentHide()
    '                            clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
    '                        End If
    '                    Else
    '                        Throw New Exception("No Valid Rows Found ")
    '                    End If
    '                Catch ex As Exception
    '                    clsCommon.ProgressBarPercentHide()
    '                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '                End Try
    '            Catch ex As Exception
    '                clsCommon.ProgressBarPercentHide()
    '                Throw New Exception(ex.Message)
    '            End Try
    '        End If
    '    Catch ex As Exception
    '        clsCommon.ProgressBarPercentHide()
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs)
        For ii As Integer = 1 To Gv1.Rows.Count
            Gv1.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If Gv1.CurrentCell.ColumnInfo.Name Is colSaleAmount Then
            OpenInvoiceDetails()
        ElseIf Gv1.CurrentCell.ColumnInfo.Name Is colDepositAmount Then
            OpenReceiptDetails()
        End If
    End Sub
    Sub OpenInvoiceDetails()
        Dim frm As frmCustomerPenaltyInvoiceDetails = New frmCustomerPenaltyInvoiceDetails()
        frm.arr = TryCast(Gv1.CurrentRow.Cells(colSaleAmount).Tag, List(Of clsCustomerPenaltyInvoiceDetail))
        If frm.arr IsNot Nothing Then
            frm.ShowDialog()
            If Not frm.isCancelButtonClicked Then
                Gv1.CurrentRow.Cells(colSaleAmount).Tag = frm.arr
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
        End If
    End Sub
    Sub OpenReceiptDetails()
        Dim frm As frmCustomerPenaltyReceiptDetails = New frmCustomerPenaltyReceiptDetails()
        frm.arr = TryCast(Gv1.CurrentRow.Cells(colDepositAmount).Tag, List(Of clsCustomerPenaltyReceiptDetail))
        If frm.arr IsNot Nothing Then
            frm.ShowDialog()
            If Not frm.isCancelButtonClicked Then
                Gv1.CurrentRow.Cells(colDepositAmount).Tag = frm.arr
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
        End If

    End Sub

    Private Sub txtDistributor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDistributor._MYValidating
        Try
            Dim qry As String = " select Cust_Code AS Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER "
            txtDistributor.Value = clsCommon.ShowSelectForm("CustPnltDis", qry, "Code", " IsDistributor='Y' ", txtDistributor.Value, "Code", isButtonClicked)
            lblDistributorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & txtDistributor.Value & "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisableControls(True)
        LoadBlankGrid()
        lblTotalPenalty.Text = "0"
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim qry As String = " select  '" & objCommonVar.CurrentUser & "' as User_Code, ROW_NUMBER( ) over( order by TSPL_CUSTOMER_PENALTY.Document_date) as SNo,TSPL_COMPANY_MASTER.Comp_Name,convert(varchar,TSPL_CUSTOMER_PENALTY_detail.Invoice_Date,103) as Invoice_Date,TSPL_CUSTOMER_PENALTY_detail.Sale_Amt,TSPL_CUSTOMER_PENALTY_detail.Deposit_Amt,TSPL_CUSTOMER_PENALTY_detail.Curr_Balance_Amt,TSPL_CUSTOMER_PENALTY_detail.Balance_Amt,TSPL_CUSTOMER_PENALTY_detail.Penalty,convert(varchar,TSPL_CUSTOMER_PENALTY.Document_date,103) as Document_date,TSPL_CUSTOMER_PENALTY.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,
        convert(varchar,TSPL_CUSTOMER_PENALTY.From_Date,103) as From_Date,convert(varchar,TSPL_CUSTOMER_PENALTY.To_Date,103) as To_Date,TSPL_CUSTOMER_PENALTY.Penalty_Per from  TSPL_CUSTOMER_PENALTY_detail
        left join TSPL_CUSTOMER_PENALTY on TSPL_CUSTOMER_PENALTY.Document_No = TSPL_CUSTOMER_PENALTY_detail.Document_No left join TSPL_COMPANY_MASTER on 1=1 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code = TSPL_CUSTOMER_PENALTY.Cust_Code
        where TSPL_CUSTOMER_PENALTY.Document_No='" & txtDocumentNo.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(MyBase.Form_ID, False, CrystalReportFolder.KwalitySalesReport, dt, "rptCustomerPenalty", "Customer Penalty", clsCommon.myCDate(dt.Rows(0)("Document_date")))
            frmCRV = Nothing
        End If
    End Sub
End Class


