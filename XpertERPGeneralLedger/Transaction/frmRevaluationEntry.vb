Imports common
Public Class frmRevaluationEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Const colSelect As String = "colSelect"
    Const colLineNo As String = "colLineNo"
    Const colAPNo As String = "colAPNo"
    Const colARNo As String = "colARNo"
    Const colReceiptNo As String = "colReceiptNo"
    Const colPaymentNo As String = "colPaymentNo"
    Const colTranSubType As String = "colTranSubType"
    Const colInvoiceDate As String = "colInvoiceDate"
    Const colInvoiceDateView As String = "colInvoiceDateView"
    Const colVCCode As String = "colVCCode"
    Const colVCName As String = "colVCName"
    Const colInvCurrencyRate As String = "colInvCurrencyRate"
    Const colInvAmount As String = "colInvAmount"
    Const colCompanyCurrencyAmt As String = "colCompanyCurrencyAmt"
    Const colRevaluateAmt As String = "colRevaluateAmt"
    Const colBalanceAmt As String = "colBalanceAmt"
    Const colGainAmt As String = "colGainAmt"
    Const colLossAmt As String = "colLossAmt"

    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RevaluationEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        txtDocNo.MyReadOnly = True
       
        LoadBlankGrid()
        LoadTransType()
        cboDocType.SelectedIndex = 1
        AddNew()
       
        SetLength()

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Public Sub SetLength()
        txtDesc.MaxLength = 200
    End Sub

    Sub LoadTransType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "AP"
        dr("Name") = "Accounts Payable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AR"
        dr("Name") = "Accounts Receivable"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "RC"
        'dr("Name") = "Receipt"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "PY"
        'dr("Name") = "Payment"
        'dt.Rows.Add(dr)

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtCurrencyCode.Value = ""
        cboDocType.SelectedIndex = 0
        txtDesc.Text = ""
        LoadBlankGrid()
        MyLabel2.Visible = False
        txtPostDate.Visible = False
        txtCurrencyRate.Value = 0
    End Sub

    Private Sub TxtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCurrencyCode._MYValidating
        Try
            Dim Qry As String = "select CURRENCY_CODE,CURRENCY_NAME,DESCRIPTION,CURRENCY_SIGN  from TSPL_CURRENCY_MASTER "
            txtCurrencyCode.Value = clsCommon.ShowSelectForm("RevCurrF", Qry, "CURRENCY_CODE", "CURRENCY_CODE <>'" + objCommonVar.BaseCurrencyCode + "'", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Function GetRowType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Vendor"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Customer"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoNumber As GridViewDecimalColumn
        Dim repoText As GridViewTextBoxColumn
        Dim repoDate As GridViewDateTimeColumn
        Dim repoCheckBox As GridViewCheckBoxColumn

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "  "
        repoCheckBox.Name = colSelect
        repoCheckBox.Width = 40
        repoCheckBox.ReadOnly = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCheckBox)

        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = "SNo"
        repoNumber.Name = colLineNo
        repoNumber.Width = 50
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor/Customer Code"
        repoText.Name = colVCCode
        repoText.Width = 100
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor/Customer Name"
        repoText.Name = colVCName
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "AP Invoice No"
        repoText.Name = colAPNo
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "AR Invoice No"
        repoText.Name = colARNo
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Receipt No"
        repoText.Name = colReceiptNo
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Payment No"
        repoText.Name = colPaymentNo
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Type"
        repoText.Name = colTranSubType
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        repoDate = New GridViewDateTimeColumn
        repoDate.FormatString = ""
        repoDate.HeaderText = "Invoice Date"
        repoDate.CustomFormat = "dd/MM/yyyy"
        repoDate.FormatString = "{0:dd/MM/yyyy}"
        repoDate.Name = colInvoiceDate
        repoDate.Width = 100
        repoDate.ReadOnly = True
        repoDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDate)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Invoice Date"
        repoText.Name = colInvoiceDateView
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)


        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = "Invoice Currency Rate"
        repoNumber.Name = colInvCurrencyRate
        repoNumber.Width = 100
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = "Invoice Amount"
        repoNumber.Name = colInvAmount
        repoNumber.Width = 100
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = "Balance Amount"
        repoNumber.Name = colBalanceAmt
        repoNumber.Width = 100
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = objCommonVar.BaseCurrencyCode + "  Amount"
        repoNumber.Name = colCompanyCurrencyAmt
        repoNumber.Width = 100
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = "Revaluate Amount"
        repoNumber.Name = colRevaluateAmt
        repoNumber.Width = 100
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = "Gain Amount"
        repoNumber.Name = colGainAmt
        repoNumber.Width = 100
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        repoNumber = New GridViewDecimalColumn()
        repoNumber.FormatString = ""
        repoNumber.HeaderText = "Loss Amount"
        repoNumber.Name = colLossAmt
        repoNumber.Width = 100
        repoNumber.ReadOnly = True
        repoNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumber)

        SetVCColumn()

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False

        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Smitem As New GridViewSummaryItem(colBalanceAmt, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        Smitem = New GridViewSummaryItem(colGainAmt, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        Smitem = New GridViewSummaryItem(colLossAmt, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        Smitem = New GridViewSummaryItem(colRevaluateAmt, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        Smitem = New GridViewSummaryItem(colCompanyCurrencyAmt, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        Smitem = New GridViewSummaryItem(colInvAmount, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtPostDate.Value = txtDate.Value
        cboDocType.Enabled = True
        isNewEntry = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        cboDocType.SelectedIndex = 0
        Panel1.Enabled = True
    End Sub

    Function AllowToSave() As Boolean
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)

    End Sub

    Sub SaveData(ByVal chekPostBtn As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsRevaluationHead()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.Trans_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                obj.Currency_Code = txtCurrencyCode.Value
                obj.Currency_Rate = txtCurrencyRate.Value

                obj.Arr = New List(Of clsRevaluationDetail)
                Dim ii As Integer = 1
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colSelect).Value) Then
                        Dim objTr As New clsRevaluationDetail()
                        objTr.SNo = ii
                        objTr.AP_Invoice_No = clsCommon.myCstr(grow.Cells(colAPNo).Value)
                        objTr.AR_Invoice_No = clsCommon.myCstr(grow.Cells(colARNo).Value)
                        objTr.Payment_No = clsCommon.myCstr(grow.Cells(colPaymentNo).Value)
                        objTr.Receipt_No = clsCommon.myCstr(grow.Cells(colReceiptNo).Value)
                        objTr.Trans_Sub_Type = clsCommon.myCstr(grow.Cells(colTranSubType).Value)
                        objTr.Tran_Conv_Rate = clsCommon.myCstr(grow.Cells(colInvCurrencyRate).Value)
                        objTr.Revaluate_Amount = clsCommon.myCdbl(grow.Cells(colRevaluateAmt).Value)
                        objTr.Balance_Amount = clsCommon.myCdbl(grow.Cells(colBalanceAmt).Value)
                        objTr.Gain_Amount = clsCommon.myCdbl(grow.Cells(colGainAmt).Value)
                        objTr.Loss_Amount = clsCommon.myCdbl(grow.Cells(colLossAmt).Value)
                        If (clsCommon.myLen(objTr.AP_Invoice_No) > 0 OrElse clsCommon.myLen(objTr.AR_Invoice_No) > 0 OrElse clsCommon.myLen(objTr.Receipt_No) > 0 OrElse clsCommon.myLen(objTr.Payment_No) > 0) Then
                            ii += 1
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("No invoice Document found to save")
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    txtDocNo.Value = obj.Document_No
                    If chekPostBtn = True Then
                    Else
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal NavType As common.NavigatorType)
        Try
            Dim obj As New clsRevaluationHead()
            obj = clsRevaluationHead.GetData(strDocumentNo, NavType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                Panel1.Enabled = False
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                cboDocType.Enabled = False
                BlankAllControls()
                LoadBlankGrid()
                isNewEntry = False

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtDesc.Text = obj.Description
                cboDocType.SelectedValue = obj.Trans_Type
                txtCurrencyCode.Value = obj.Currency_Code
                txtCurrencyRate.Value = obj.Currency_Rate
                UsLock1.Status = obj.Status

                If obj.Posted_Date IsNot Nothing Then
                    txtPostDate.Value = obj.Posted_Date
                End If
                Dim arrDoc As New List(Of String)
                For Each objTr As clsRevaluationDetail In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAPNo).Value = objTr.AP_Invoice_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colARNo).Value = objTr.AR_Invoice_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentNo).Value = objTr.Payment_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReceiptNo).Value = objTr.Receipt_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTranSubType).Value = objTr.Trans_Sub_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceDate).Value = objTr.Invoice_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceDateView).Value = clsCommon.GetPrintDate(objTr.Invoice_Date, "dd/MM/yyyy")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVCCode).Value = objTr.Vendor_Customer_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVCName).Value = objTr.Vendor_Customer_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvCurrencyRate).Value = objTr.Tran_Conv_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvAmount).Value = objTr.Invoice_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = objTr.Balance_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRevaluateAmt).Value = objTr.Revaluate_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCompanyCurrencyAmt).Value = objTr.Company_Currency_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGainAmt).Value = objTr.Gain_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLossAmt).Value = objTr.Loss_Amount

                    If clsCommon.myLen(objTr.AP_Invoice_No) > 0 Then
                        arrDoc.Add(objTr.AP_Invoice_No)
                    ElseIf clsCommon.myLen(objTr.AR_Invoice_No) > 0 Then
                        arrDoc.Add(objTr.AR_Invoice_No)
                    ElseIf clsCommon.myLen(objTr.Payment_No) > 0 Then
                        arrDoc.Add(objTr.Payment_No)
                    ElseIf clsCommon.myLen(objTr.Receipt_No) > 0 Then
                        arrDoc.Add(objTr.Receipt_No)
                    End If
                    gv1.Refresh()
                    Application.DoEvents()
                Next
                If obj.Status = ERPTransactionStatus.Pending Then
                    PickData(arrDoc)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsRevaluationHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Posted Successfully ")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsRevaluationHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Document_No,convert(varchar,Document_Date,103) as Date,[Description],Trans_Type,Currency_code,Currency_Rate,case when status=1 then 'Posted' else 'Pending' end Status  from TSPL_REVALUATION_HEAD"
        LoadData(clsCommon.ShowSelectForm("RevEntrMain", qry, "Document_No", "", txtDocNo.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            PrintData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            'Dim frm As New FrmPWD(Nothing)
            'frm.strType = "SIRC"
            'frm.strCode = "SIReversAndCreate"
            'frm.ShowDialog()
            'If frm.isPasswordCorrect Then
            '    btnReverse.Visible = True
            'End If
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        e.Cancel = True
        'If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
        '    e.Cancel = True
        'End If
    End Sub

    Sub SetVCColumn()
        Try
            gv1.Columns(colAPNo).IsVisible = False
            gv1.Columns(colARNo).IsVisible = False
            gv1.Columns(colReceiptNo).IsVisible = False
            gv1.Columns(colPaymentNo).IsVisible = False

            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "AP") = CompairStringResult.Equal Then
                gv1.Columns(colVCCode).HeaderText = "Vendor Code"
                gv1.Columns(colVCName).HeaderText = "Vendor Name"
                gv1.Columns(colAPNo).IsVisible = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "AR") = CompairStringResult.Equal Then
                gv1.Columns(colVCCode).HeaderText = "Customer Code"
                gv1.Columns(colVCName).HeaderText = "Customer Name"
                gv1.Columns(colARNo).IsVisible = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "PY") = CompairStringResult.Equal Then
                gv1.Columns(colVCCode).HeaderText = "Vendor Code"
                gv1.Columns(colVCName).HeaderText = "Vendor Name"
                gv1.Columns(colPaymentNo).IsVisible = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "RC") = CompairStringResult.Equal Then
                gv1.Columns(colVCCode).HeaderText = "Customer Code"
                gv1.Columns(colVCName).HeaderText = "Customer Name"
                gv1.Columns(colReceiptNo).IsVisible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        LoadBlankGrid()
        PickData(Nothing)
    End Sub

    Sub PickData(ByVal arrDoc As List(Of String))
        Try
            If clsCommon.myLen(txtCurrencyCode.Value) <= 0 Then
                txtCurrencyCode.Value = 0
                Throw New Exception("Currency Code can't be blank")
            End If
            If txtCurrencyRate.Value <= 0 Then
                txtCurrencyRate.Value = 0
                Throw New Exception("Currency Rate can't be less than or equal to zero")
            End If
            Dim qry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "AP") = CompairStringResult.Equal Then
                qry = " select   *,convert(decimal(18,2),(case when Document_Type='Debit Note' then (case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end) else (case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end) end)) as GainAmount,convert(decimal(18,2),(case when Document_Type='Debit Note' then (case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end) else (case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end) end)) as LossAmount  from (" + Environment.NewLine & _
               " select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Invoice_Date,case when Document_Type='I' then 'Invoice' else case when Document_Type='D' then 'Debit Note' else case when Document_Type='C' then 'Credit Note' else '' end end end as  Document_Type,ConvRate,Document_Total,BalanceAmt, convert(decimal(18,2),(BalanceAmt*ConvRate)) as BaseCurrencyAmt,convert(decimal(18,2),(BalanceAmt*" + clsCommon.myCstr(txtCurrencyRate.Value) + ")) as RevalualteAmt  from ( " + Environment.NewLine & _
                "select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Document_Type,Document_Total,BalanceAmt,Invoice_Date,case when RevCurrency_Rate is null then ConvRate else RevCurrency_Rate end as ConvRate from ( " & _
               " select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as Vendor_Customer_Code,TSPL_VENDOR_MASTER.Vendor_Name as Vendor_Customer_Name, TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Document_Total,( TSPL_VENDOR_INVOICE_HEAD.Document_Total-isnull( TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0)- " + Environment.NewLine & _
               " isnull((select sum(isnull(Applied_Amount,0)) from TSPL_PAYMENT_DETAIL where TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No " + Environment.NewLine & _
               " and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No)),0) -isnull((select sum(isnull( PR_Total_Amt,0)) from TSPL_PR_HEAD left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PR_HEAD.Against_PI LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD as innINVHead ON innINVHead.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where innINVHead.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  ),0) -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Payment_Adjustment_Header where TSPL_Payment_Adjustment_Header.Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No),0)) as BalanceAmt   " + Environment.NewLine & _
               " ,Invoice_Entry_Date as Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.ConvRate,( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc) as RevCurrency_Rate  " + Environment.NewLine & _
               " from TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine & _
               " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  " + Environment.NewLine & _
               " where  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)<=convert(date,'" + txtDate.Value + "',103) and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null and TSPL_VENDOR_INVOICE_HEAD.ConvRate<>1 and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY' and TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE='" + clsCommon.myCstr(txtCurrencyCode.Value) + "' " + Environment.NewLine & _
               " and not exists ( select 1 from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No where TSPL_REVALUATION_HEAD.Status=0 and TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_DETAIL.Document_No not in ('" + txtDocNo.Value + "')) " + Environment.NewLine + _
               " )x " + Environment.NewLine & _
               " ) xx " + Environment.NewLine & _
               " where  BalanceAmt > 0 " + Environment.NewLine & _
               " )xxx order by Invoice_Date "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "AR") = CompairStringResult.Equal Then
                qry = " select   *,convert(decimal(18,2),(case when Document_Type='Credit Note' then (case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end) else (case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end) end)) as GainAmount,convert(decimal(18,2),(case when Document_Type='Credit Note' then (case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end) else (case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end) end)) as LossAmount  from (" + Environment.NewLine & _
                " select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Invoice_Date ,ConvRate, case when Document_Type='I' then 'Invoice' else case when Document_Type='D' then 'Debit Note' else case when Document_Type='C' then 'Credit Note' else '' end end end as Document_Type,Document_Total,BalanceAmt, convert(decimal(18,2),(BalanceAmt*ConvRate)) as BaseCurrencyAmt,convert(decimal(18,2),(BalanceAmt*" + clsCommon.myCstr(txtCurrencyRate.Value) + ")) as RevalualteAmt  from ( " + Environment.NewLine & _
                " select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Document_Type,Document_Total,BalanceAmt,Invoice_Date,case when RevCurrency_Rate is null then ConvRate else RevCurrency_Rate end as ConvRate from (" + Environment.NewLine & _
                " select TSPL_Customer_Invoice_Head.Customer_Code as Vendor_Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Customer_Name, TSPL_Customer_Invoice_Head.Document_No,TSPL_Customer_Invoice_Head.Document_Type, TSPL_Customer_Invoice_Head.Document_Total,  " + Environment.NewLine & _
                " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine & _
                " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine & _
                " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine & _
                " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine & _
                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine & _
                " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine & _
                " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine & _
                " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0)" + Environment.NewLine & _
                " -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as BalanceAmt  " + Environment.NewLine & _
                " ,TSPL_Customer_Invoice_Head.Document_Date as Invoice_Date,TSPL_Customer_Invoice_Head.ConvRate,( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc) as RevCurrency_Rate " + Environment.NewLine & _
                " from TSPL_Customer_Invoice_Head  " + Environment.NewLine & _
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code " + Environment.NewLine & _
                " where convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)<=convert(date,'" + txtDate.Value + "',103) and TSPL_Customer_Invoice_Head.Status =1  and TSPL_Customer_Invoice_Head.ConvRate<>1 and TSPL_Customer_Invoice_Head.RefDocType<>'REVALUATION ENTRY' and TSPL_Customer_Invoice_Head.CURRENCY_CODE='" + txtCurrencyCode.Value + "'" + Environment.NewLine & _
                " and not exists ( select 1 from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No where TSPL_REVALUATION_HEAD.Status=0 and TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_DETAIL.Document_No not in ('" + txtDocNo.Value + "')) " + Environment.NewLine + _
                " )x " + Environment.NewLine & _
               " ) xx" + Environment.NewLine & _
                " where  BalanceAmt<>0 " + Environment.NewLine & _
                " )xxx order by Invoice_Date"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "RC") = CompairStringResult.Equal Then
                qry = " select *,case when Document_Type ='Refund' then convert(decimal(18,2),(case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end)) else convert(decimal(18,2),(case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end)) end as GainAmount, case when Document_Type ='Refund' then convert(decimal(18,2),(case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end)) else convert(decimal(18,2),(case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end)) end as LossAmount  from (" + Environment.NewLine + _
                " select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Invoice_Date ,Document_Type,ConvRate,Document_Total,BalanceAmt, convert(decimal(18,2),(BalanceAmt*ConvRate)) as BaseCurrencyAmt,convert(decimal(18,2),(BalanceAmt*" + clsCommon.myCstr(txtCurrencyRate.Value) + ")) as RevalualteAmt  from (" + Environment.NewLine + _
                " select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Document_Type,Document_Total,BalanceAmt,Invoice_Date,case when RevCurrency_Rate is null then ConvRate else RevCurrency_Rate end as ConvRate from (" + Environment.NewLine & _
                " select TSPL_RECEIPT_HEADER.Cust_Code as Vendor_Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Vendor_Customer_Name,TSPL_RECEIPT_HEADER.Receipt_No as Document_No,TSPL_RECEIPT_HEADER.Receipt_Date as Invoice_Date,case when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'On Account' else case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'Advance' else case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'Refund' else  '' end end end as Document_Type,TSPL_RECEIPT_HEADER.Receipt_Amount as Document_Total" + Environment.NewLine + _
                " ,(TSPL_RECEIPT_HEADER.Receipt_Amount " + Environment.NewLine + _
                " - isnull((select sum(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No " + Environment.NewLine + _
                " and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) " + Environment.NewLine + _
                " - isnull((select sum(isnull(RHInner.Receipt_Amount,0)) from TSPL_RECEIPT_HEADER as RHInner where RHInner.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=RHInner.Receipt_No)),0) " + Environment.NewLine + _
                " ) as BalanceAmt, TSPL_RECEIPT_HEADER.ConvRate,( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc) as RevCurrency_Rate  " + Environment.NewLine + _
                " from TSPL_RECEIPT_HEADER " + Environment.NewLine + _
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  " + Environment.NewLine + _
                " where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + txtDate.Value + "',103) and TSPL_RECEIPT_HEADER.Receipt_Type in ('O','P','F') and TSPL_RECEIPT_HEADER.Posted='Y'  and TSPL_RECEIPT_HEADER.ConvRate<>1   and TSPL_RECEIPT_HEADER.CURRENCY_CODE='" + txtCurrencyCode.Value + "'" + Environment.NewLine + _
                " and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) " + Environment.NewLine + _
                " and not exists ( select 1 from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No where TSPL_REVALUATION_HEAD.Status=0 and TSPL_REVALUATION_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_REVALUATION_DETAIL.Document_No not in ('" + txtDocNo.Value + "')) " + Environment.NewLine + _
                " )x " + Environment.NewLine & _
                " ) xx " + Environment.NewLine + _
                " where  BalanceAmt > 0 " + Environment.NewLine + _
                " )xxx order by Invoice_Date "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "PY") = CompairStringResult.Equal Then
                qry = "select *,case when Document_Type='Receipt' then convert(decimal(18,2),(case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end)) else  (convert(decimal(18,2),(case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end))) end as GainAmount,case when Document_Type='Receipt' then (convert(decimal(18,2),(case when BaseCurrencyAmt>RevalualteAmt then BaseCurrencyAmt-RevalualteAmt else 0 end))) else  (convert(decimal(18,2),(case when BaseCurrencyAmt<RevalualteAmt then RevalualteAmt-BaseCurrencyAmt else 0 end))) end as LossAmount  from (" + Environment.NewLine & _
                " select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Invoice_Date ,Document_Type,ConvRate,Document_Total,BalanceAmt, convert(decimal(18,2),(BalanceAmt*ConvRate)) as BaseCurrencyAmt,convert(decimal(18,2),(BalanceAmt*" + clsCommon.myCstr(txtCurrencyRate.Value) + ")) as RevalualteAmt  from (" + Environment.NewLine & _
                " select Vendor_Customer_Code,Vendor_Customer_Name,Document_No,Document_Type,Document_Total,BalanceAmt,Invoice_Date,case when RevCurrency_Rate is null then ConvRate else RevCurrency_Rate end as ConvRate from (" + Environment.NewLine & _
                " select TSPL_PAYMENT_HEADER.Vendor_Code as Vendor_Customer_Code,TSPL_VENDOR_MASTER.Vendor_Name as Vendor_Customer_Name,TSPL_PAYMENT_HEADER.Payment_No as Document_No,TSPL_PAYMENT_HEADER.Payment_Date as Invoice_Date,case when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' else case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' else case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else '' end end end as Document_Type,TSPL_PAYMENT_HEADER.Payment_Amount as Document_Total " + Environment.NewLine & _
                " ,(TSPL_PAYMENT_HEADER.Payment_Amount - isnull((select sum(isnull(Applied_Amount,0)) from TSPL_PAYMENT_DETAIL where Document_No=TSPL_PAYMENT_HEADER.Payment_No " + Environment.NewLine & _
                " and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No)),0)" + Environment.NewLine +
                " - isnull((select sum(isnull(PHInner.Payment_Amount,0))+sum(isnull(PHInner.TDS_Amount,0)) from TSPL_PAYMENT_HEADER as PHInner where PHInner.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=PHInner.Payment_No)),0)) as BalanceAmt," + Environment.NewLine & _
                " TSPL_PAYMENT_HEADER.ConvRate,( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_REVALUATION_HEAD.Status=1 order by TSPL_REVALUATION_HEAD.Document_Date desc) as RevCurrency_Rate  " + Environment.NewLine & _
                " from TSPL_PAYMENT_HEADER " + Environment.NewLine & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code  " + Environment.NewLine & _
                " where  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)<=convert(date,'" + txtDate.Value + "',103) and TSPL_PAYMENT_HEADER.Payment_Type in ('OA','AV','RC') and TSPL_PAYMENT_HEADER.Posted=1  and TSPL_PAYMENT_HEADER.ConvRate<>1   and TSPL_PAYMENT_HEADER.CURRENCY_CODE='" + txtCurrencyCode.Value + "' " + Environment.NewLine & _
                " and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No) " + Environment.NewLine + _
                " and not exists ( select 1 from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No where TSPL_REVALUATION_HEAD.Status=0 and TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_REVALUATION_DETAIL.Document_No not in ('" + txtDocNo.Value + "')) " + Environment.NewLine + _
                " )x " + Environment.NewLine & _
                " ) xx " + Environment.NewLine & _
                " where  BalanceAmt > 0 " + Environment.NewLine & _
                " )xxx order by Invoice_Date"
            Else
                Throw New Exception("Not a valid transaction type")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Invoice found")
            End If

            For Each dr As DataRow In dt.Rows
                If arrDoc IsNot Nothing AndAlso arrDoc.Count > 0 Then
                    If arrDoc.Contains(clsCommon.myCstr(dr("Document_No"))) Then
                        Continue For
                    End If
                End If

                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = False
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "AP") = CompairStringResult.Equal Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAPNo).Value = clsCommon.myCstr(dr("Document_No"))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "AR") = CompairStringResult.Equal Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colARNo).Value = clsCommon.myCstr(dr("Document_No"))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "RC") = CompairStringResult.Equal Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReceiptNo).Value = clsCommon.myCstr(dr("Document_No"))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "PY") = CompairStringResult.Equal Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentNo).Value = clsCommon.myCstr(dr("Document_No"))
                End If
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTranSubType).Value = clsCommon.myCstr(dr("Document_Type"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceDate).Value = clsCommon.myCDate(dr("Invoice_Date"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceDateView).Value = clsCommon.GetPrintDate(clsCommon.myCDate(dr("Invoice_Date")), "dd/MM/yyyy")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVCCode).Value = clsCommon.myCstr(dr("Vendor_Customer_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVCName).Value = clsCommon.myCstr(dr("Vendor_Customer_Name"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvCurrencyRate).Value = clsCommon.myCstr(dr("ConvRate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvAmount).Value = clsCommon.myCstr(dr("Document_Total"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = clsCommon.myCstr(dr("BalanceAmt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCompanyCurrencyAmt).Value = clsCommon.myCstr(dr("BaseCurrencyAmt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRevaluateAmt).Value = clsCommon.myCstr(dr("RevalualteAmt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colGainAmt).Value = clsCommon.myCstr(dr("GainAmount"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLossAmt).Value = clsCommon.myCstr(dr("LossAmount"))
                gv1.Refresh()
                Application.DoEvents()
            Next
            Panel1.Enabled = False
            Try
                gv1.CurrentRow = gv1.Rows(0)
            Catch ex As Exception
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        Panel1.Enabled = True
        LoadBlankGrid()
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged
        SetVCColumn()
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No Document no found to export")
            End If
            clsCommon.MyExportToExcelGrid(Me.Text + " " + txtDocNo.Value, gv1, Nothing, Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        CheckUncheckAll(True)
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckUncheckAll(False)
    End Sub

    Sub CheckUncheckAll(ByVal val As Boolean)
        Try
            If gv1.Rows.Count > 0 Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    gv1.Rows(ii).Cells(colSelect).Value = val
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
