
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class FrmEmployeeSetOff
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim SettingVendorSetOffDayWise As Boolean = False
    Dim strAllowSetOffUntilTransactionsnotend As Boolean = False
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Public isInsideLoadData As Boolean = False
    Dim dtAllData As DataTable = Nothing
    Dim dtAllDataDetail As DataTable = Nothing
    Dim dtmain As DataTable = Nothing
    Dim strQuery As String = String.Empty

    Public vndorforInvoice As String = String.Empty
    '==============Grid Columns===================
    Const colApply As String = "Apply"
    Const colDocType As String = "DocType"
    Const colPINo As String = "PurchaseInvoice"
    Const colDocNo As String = "DocNo"
    Const colDocDate As String = "VendorDocDate"
    Const colDocumentDate As String = "DocumentDate"
    Const colVendorInvNo As String = "VendorInvNo"
    Const colNetAmt As String = "NetAmount"
    Const colPendingAmt As String = "PendingAmt"
    Const colAppliedAmt As String = "AppliedAmt"
    Const colSecurityAmt As String = "SecurityAmt"
    Const colOriginalAmt As String = "OriginalAmt"
    Const colTDSAmt As String = "TDSAmt"
    Const colComment As String = "Comment"
    Const colTemp As String = "Temp"

    Const colLineNo As String = "LineNo"
    Const colGLAccount As String = "GLAccount"
    Const colAccDesc As String = "AccDesc"
    Const colAmount As String = "Amount"
    Const colRemark As String = "Remark"
    Const colExpenseCode As String = "ExpenseCode"
    Public StrdocNo As String = ""
    Const colAdjustedAmt As String = "Adjusted Amount"
    '---------------------------------------------
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmVendorSetOff_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave_Click(btnsave, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub FrmVendorSetOff_Load(sender As Object, e As EventArgs) Handles Me.Load
        SettingVendorSetOffDayWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VendorSetOffDayWise, clsFixedParameterCode.VendorSetOffDayWise, Nothing)) = 1, True, False)
        strAllowSetOffUntilTransactionsnotend = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSetOffUntilTransactionsnotend, clsFixedParameterCode.AllowSetOffUntilTransactionsnotend, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")

        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Sub Reset()
        btnsave.Enabled = True
        isNewEntry = True
        vndorforInvoice = ""
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtMulEmployee.arrValueMember = Nothing
        txtDocumentNo.Value = ""
        lbldocinvoicedate.Text = ""
        lblreceiptamount.Text = 0
        lbldocumentdate.Text = ""
        lblDocumentType.Text = ""
        lblBalAmt.Text = 0
    End Sub

    Sub ResetOnDate()
        btnsave.Enabled = True
        isNewEntry = True
        vndorforInvoice = ""
        txtMulEmployee.arrValueMember = Nothing
        txtDocumentNo.Value = ""
        lbldocinvoicedate.Text = ""
        lblreceiptamount.Text = 0
        lbldocumentdate.Text = ""
        lblDocumentType.Text = ""
        lblBalAmt.Text = 0
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub LoadBlankGrid()
        dgvReceipt.Rows.Clear()
        dgvReceipt.Columns.Clear()

        dgvReceipt.AllowDeleteRow = True
        dgvReceipt.AllowAddNewRow = False

        Dim apply As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        apply.FormatString = ""
        apply.HeaderText = colApply
        apply.Name = colApply
        apply.Width = 50
        apply.ReadOnly = True
        apply.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvReceipt.MasterTemplate.Columns.Add(apply)

        Dim docType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docType.FormatString = ""
        docType.HeaderText = "Document Type"
        docType.Name = colDocType
        docType.Width = 100
        docType.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(docType)

        Dim PINo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        PINo.FormatString = ""
        PINo.HeaderText = "Document No"
        PINo.Name = colPINo
        PINo.Width = 150
        PINo.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(PINo)

        Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docNo.FormatString = ""
        docNo.HeaderText = "AP Document No"
        docNo.Name = colDocNo
        docNo.Width = 150
        docNo.ReadOnly = True
        docNo.IsVisible = False
        dgvReceipt.MasterTemplate.Columns.Add(docNo)

        Dim documentDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        documentDate.FormatString = ""
        documentDate.HeaderText = "Document Date"
        documentDate.Name = colDocumentDate
        documentDate.Width = 150
        documentDate.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(documentDate)

        Dim docDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docDate.FormatString = ""
        docDate.HeaderText = "Vendor Invoice Date"
        docDate.Name = colDocDate
        docDate.Width = 150
        docDate.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(docDate)

        Dim vendorInvNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        vendorInvNo.FormatString = ""
        vendorInvNo.HeaderText = "Vendor Invoice No"
        vendorInvNo.Name = colVendorInvNo
        vendorInvNo.Width = 100
        vendorInvNo.ReadOnly = True
        dgvReceipt.MasterTemplate.Columns.Add(vendorInvNo)

        Dim originalInvAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        originalInvAmt = New GridViewDecimalColumn()
        originalInvAmt.FormatString = ""
        originalInvAmt.HeaderText = "Original Inv Amt"
        originalInvAmt.Name = colOriginalAmt
        originalInvAmt.Width = 100
        originalInvAmt.ReadOnly = True
        originalInvAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(originalInvAmt)

        Dim tdsAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        tdsAmt = New GridViewDecimalColumn()
        tdsAmt.FormatString = ""
        tdsAmt.HeaderText = "TDS Amount"
        tdsAmt.Name = colTDSAmt
        tdsAmt.Width = 100
        tdsAmt.ReadOnly = True
        tdsAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(tdsAmt)

        Dim payableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        payableAmt = New GridViewDecimalColumn()
        payableAmt.FormatString = ""
        payableAmt.HeaderText = "Net Amount"
        payableAmt.Name = colNetAmt
        payableAmt.Width = 100
        payableAmt.ReadOnly = True
        payableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(payableAmt)

        Dim SecurityAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        SecurityAmt = New GridViewDecimalColumn()
        SecurityAmt.FormatString = ""
        SecurityAmt.HeaderText = "Security Amount"
        SecurityAmt.Name = colSecurityAmt
        SecurityAmt.Width = 0
        SecurityAmt.IsVisible = False
        SecurityAmt.ReadOnly = False
        SecurityAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(SecurityAmt)

        Dim pendingAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        pendingAmt = New GridViewDecimalColumn()
        pendingAmt.FormatString = ""
        pendingAmt.HeaderText = "Current Pending"
        pendingAmt.Name = colPendingAmt
        pendingAmt.Width = 100
        pendingAmt.ReadOnly = True
        pendingAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(pendingAmt)

        Dim appliedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        appliedAmt = New GridViewDecimalColumn()
        appliedAmt.FormatString = ""
        appliedAmt.HeaderText = "Applied Amount"
        appliedAmt.Name = colAppliedAmt
        appliedAmt.Width = 100
        appliedAmt.ReadOnly = False
        appliedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(appliedAmt)

        '' Anubhooti 14-Nov-2014
        Dim AdjustedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        AdjustedAmt = New GridViewDecimalColumn()
        AdjustedAmt.FormatString = ""
        AdjustedAmt.HeaderText = "Adjusted/Paid Amount"
        AdjustedAmt.Name = colAdjustedAmt
        AdjustedAmt.Width = 100
        AdjustedAmt.ReadOnly = True
        AdjustedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(AdjustedAmt)
        ''
        Dim temp As GridViewDecimalColumn = New GridViewDecimalColumn()
        temp = New GridViewDecimalColumn()
        temp.FormatString = ""
        temp.HeaderText = "Temporary"
        temp.Name = colTemp
        temp.Width = 100
        temp.ReadOnly = True
        temp.IsVisible = False
        temp.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvReceipt.MasterTemplate.Columns.Add(temp)

        Dim comment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        comment.FormatString = ""
        comment.HeaderText = "Comment"
        comment.Name = colComment
        comment.Width = 150
        comment.ReadOnly = False
        dgvReceipt.MasterTemplate.Columns.Add(comment)

        dgvReceipt.ShowGroupPanel = False
        dgvReceipt.AllowColumnReorder = False
        dgvReceipt.AllowRowReorder = False
        dgvReceipt.EnableSorting = False
        dgvReceipt.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvReceipt.MasterTemplate.ShowRowHeaderColumn = False
        dgvReceipt.AllowAddNewRow = False
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim trans As SqlTransaction = Nothing
        Dim strvendor As String = String.Empty
        Try
            If txtFromDate.Value > txtToDate.Value Then
                Throw New Exception("From date cannot be greater than to Date")
            End If
            'If txtMulVendor.arrValueMember.Count <= 0 Then
            '    Throw New Exception("Please select atleast one vendor to set off.")
            'End If
            If strAllowSetOffUntilTransactionsnotend = True Then
                If txtMulEmployee.arrValueMember IsNot Nothing AndAlso txtMulEmployee.arrValueMember.Count > 0 Then
                    While txtMulEmployee.arrValueMember.Count > 0
                        If txtMulEmployee.arrValueMember.Count > 0 Then
                            Dim list As New ArrayList
                            list = txtMulEmployee.arrValueMember

                            Dim j As Integer = 0
                            clsCommon.ProgressBarPercentShow()
                            For i As Integer = 0 To list.Count - 1
                                j = j + 1
                                trans = clsDBFuncationality.GetTransactin()

                                If i > list.Count - 1 Then
                                    strvendor = TryCast(list.Item(list.Count - 1), String)
                                Else
                                    strvendor = TryCast(list.Item(i), String)
                                End If
                                AutoInvoice(i + 1, txtMulEmployee.arrValueMember.Count, strvendor, "AD", trans)
                                trans.Commit()
                                list = txtMulEmployee.arrValueMember

                            Next
                            clsCommon.ProgressBarPercentHide()
                            ResetonDateforContinuousTransaction()
                        Else
                            clsCommon.MyMessageBoxShow("Please select atleast one Employee to set off.", Me.Text)
                        End If
                    End While
                    clsCommon.MyMessageBoxShow("Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Please select atleast one Employee to set off.")
                End If
            Else
                If txtMulEmployee.arrValueMember IsNot Nothing AndAlso txtMulEmployee.arrValueMember.Count > 0 Then
                    clsCommon.ProgressBarPercentShow()
                    For i As Integer = 0 To txtMulEmployee.arrValueMember.Count - 1
                        trans = clsDBFuncationality.GetTransactin()
                        Try
                            'clsCommon.ProgressBarPercentUpdate((i + 1) / txtMulVendor.arrValueMember.Count * 100, " Saving and posting Record(s) " & (i + 1) & " of Total vendors " & txtMulVendor.arrValueMember.Count)
                            strvendor = TryCast(txtMulEmployee.arrValueMember.Item(i), String)
                            AutoInvoice(i + 1, txtMulEmployee.arrValueMember.Count, strvendor, "AD", trans)
                            'Throw New Exception("Balwinder")
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.myLen(vndorforInvoice) > 1 Then
                        vndorforInvoice = vndorforInvoice.Substring(0, vndorforInvoice.Length - 2)
                        clsCommon.MyMessageBoxShow("Data Saved Successfully and Invoices are not found for Employee(s) " & vndorforInvoice & " ")
                    Else
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    End If
                    ResetOnDate()
                Else
                    clsCommon.MyMessageBoxShow("Please select atleast one Employee to set off.")
                End If
            End If


        Catch ex As Exception
            'trans.Rollback()
            ResetOnDate()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message + " for Employee " + strvendor, Me.Text)
        End Try
    End Sub
    Sub ResetonDateforContinuousTransaction()
        btnsave.Enabled = True
        isNewEntry = True
        vndorforInvoice = ""
        'txtMulVendor.arrValueMember = Nothing
        txtDocumentNo.Value = ""
        lbldocinvoicedate.Text = ""
        lblreceiptamount.Text = 0
        lbldocumentdate.Text = ""
        lblDocumentType.Text = ""
        lblBalAmt.Text = 0
    End Sub
    Sub AutoInvoice(ByVal intVendorNo As Integer, ByVal intTotalVendor As Integer, ByVal Vendor As String, ByVal PaymentType As String, ByVal trans As SqlTransaction)
        Dim DocNo As String = Nothing
        Try
            clsCommon.ProgressBarPercentUpdate(intVendorNo / intTotalVendor * 100, " Saving and posting Record(s) " & clsCommon.myCstr(intVendorNo) & " of Total vendors " & clsCommon.myCstr(intTotalVendor))
            Dim intdocGenerated As Integer = 0
            Dim WhrCls As String = String.Empty

            Dim strQryForRejectedAmt As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
            " from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_PurchaseReturn_No  in (SELECT PR_No  FROM TSPL_PR_HEAD WHERE Against_PI =TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ) and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "

            '' this code is wrritten for that debit note which is created auto through PI and Pr is not created against that PI
            Dim strQryForRejectedAmtforNonPR As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
            " from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_POInvoice_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "


            Dim strTaxRecovarableQuery As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then " &
          " ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'  " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end) "

            Dim NOOFTRANSACTIONS As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSetNoOfTransactionsforSetOff, clsFixedParameterCode.AllowtoSetNoOfTransactionsforSetOff, trans))
            If clsCommon.myLen(NOOFTRANSACTIONS) > 0 Then
                NOOFTRANSACTIONS = " TOP " & NOOFTRANSACTIONS & ""
            Else
                NOOFTRANSACTIONS = ""
            End If


            Dim qry As String = "Select " & NOOFTRANSACTIONS & " * from (" &
            " Select Payment_No as [Code], Entry_Desc as [Description], convert(varchar,Payment_Date,103) as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount+isnull(TDS_Amount ,0) as [Payment Amt]," &
            " Payment_Amount+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.Payment_Type='AD' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No in (select PPH.Payment_No from TSPL_PAYMENT_HEADER PPH where PPH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') and isnull(TSPL_BANK_REVERSE.POST,'')='P' ))  AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No ),0) as [Bal Amt]  from TSPL_PAYMENT_HEADER WHERE Posted='1' AND Vendor_Code='" + Vendor + "' AND   IsChkReverse='N' and Payment_Type IN ('AV','OA') " &
            "  and (convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "')  and TSPL_PAYMENT_HEADER.advance_against_salary=1 " & Environment.NewLine &
            " Union All" & Environment.NewLine &
            "Select Document_No AS Code,'' as Description,DocumentDate as [Payment Date],DocType as [Payment Type],OriginalAmt as [Payment Amt],PendingAmt as [Bal Amt]  from ( select Vendor_Code, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType],  " &
            " TSPL_VENDOR_INVOICE_HEAD.Document_No, Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else Document_No End as PurchaseInvoice," &
            " Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date End as DocumentDate, " &
            " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [DocDate] ,  " &
            " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [VendorInvoiceNo], " &
            " TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " as [OriginalAmt] ," &
            " TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as [TDSAmt], " &
            " (TSPL_VENDOR_INVOICE_HEAD.Document_Total- case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end) " + strTaxRecovarableQuery + " as [NetAmount], " &
            " TSPL_VENDOR_INVOICE_HEAD.Document_Total - case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end " + strTaxRecovarableQuery + " - ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') and isnull(TSPL_BANK_REVERSE.POST,'')='P') ),0) " &
            " -isnull((select sum(isnull(Payment_Amount,0)) from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Applied_Payment =TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') and isnull(TSPL_BANK_REVERSE.POST,'')='P' ) and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and isnull(TSPL_PAYMENT_HEADER.Applied_Payment ,'')<>'' AND Payment_No <> ''),0)  " & Environment.NewLine &
            " " + strQryForRejectedAmt + " " + strQryForRejectedAmtforNonPR + " " &
            " -ISNULL((Select SUM(isnull(TSPL_PAYMENT_ADJUSTMENT_DETAIL.Amount,0)) from TSPL_PAYMENT_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_ADJUSTMENT_DETAIL on TSPL_PAYMENT_ADJUSTMENT_Header.Adjustment_No=TSPL_PAYMENT_ADJUSTMENT_DETAIL.Adjustment_No Where isnull(TSPL_PAYMENT_ADJUSTMENT_Header.Doc_No,'') = isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,'') ),0) " &
            " as [PendingAmt],TSPL_VENDOR_INVOICE_HEAD.ConvRate " &
            " ,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation " &
            " from TSPL_VENDOR_INVOICE_HEAD " &
             " WHERE ((ISNULL(RefDocNo,'')= '' or ISNULL((select VI.Document_Type from TSPL_VENDOR_INVOICE_HEAD VI where VI.Document_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo ),'') in ('I','')) AND ISNULL(Against_PurchaseReturn_No,'')= '') and not ( ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D') " &
            " and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY' " &
            " and isnull(TSPL_VENDOR_INVOICE_HEAD.posting_date,'')<>''" &
            " ) FINALQRY WHERE FINALQRY.PendingAmt>0 and (convert(date,FINALQRY.DocumentDate ,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,FINALQRY.DocumentDate ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' )  and DocType ='Debit Note' and Vendor_Code='" + Vendor + "' and 1=2  " & Environment.NewLine &
            "  ) Final where [Bal Amt]>0 order by [Payment Date]"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    txtDocumentNo.Value = clsCommon.myCstr(dt.Rows(i)("Code"))
                    lblDocumentType.Text = clsCommon.myCstr(dt.Rows(i)("Payment Type"))
                    lbldocumentdate.Text = clsCommon.myCDate(dt.Rows(i)("Payment Date"))
                    Dim isRunWhile As Boolean = True
                    While isRunWhile
                        lblBalAmt.Text = clsPaymentHeader.GetBalance(txtDocumentNo.Value, "", trans)
                        If clsCommon.myCdbl(lblBalAmt.Text) > 0 Then
                            funFillGrid(Vendor, trans)
                            Dim strcountofdebitnote As Integer = 0
                            If dgvReceipt.Rows.Count > 0 Then
                                For Each grow As GridViewRowInfo In dgvReceipt.Rows
                                    If clsCommon.CompairString(grow.Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal Then
                                        strcountofdebitnote = strcountofdebitnote + 1
                                    End If
                                Next
                                If strcountofdebitnote <> dgvReceipt.Rows.Count Then
                                    AutoApplyAmt(clsCommon.myCdbl(lblBalAmt.Text))
                                    SaveData(Vendor, trans)
                                    intdocGenerated += 1
                                    clsCommon.ProgressBarPercentUpdate(intVendorNo / intTotalVendor * 100, " Saving and posting Record(s) " & clsCommon.myCstr(intVendorNo) & " of Total vendors " & clsCommon.myCstr(intTotalVendor) + ".No of Documents Generated(" + clsCommon.myCstr(intdocGenerated) + ")")
                                End If

                                If strcountofdebitnote = dgvReceipt.Rows.Count AndAlso strAllowSetOffUntilTransactionsnotend = True Then
                                    Dim list As New ArrayList
                                    ''list = cbgCustomer.CheckedValue
                                    list = txtMulEmployee.arrValueMember
                                    list.Remove(Vendor)
                                    txtMulEmployee.arrValueMember = list
                                End If
                            Else
                                vndorforInvoice = vndorforInvoice + Vendor + ", "
                            End If
                            If SettingVendorSetOffDayWise Then
                                isRunWhile = isNextInvoiceDateExists
                            Else
                                isRunWhile = False
                            End If
                        Else
                            isRunWhile = False
                        End If

                    End While
                Next
            Else
                If strAllowSetOffUntilTransactionsnotend = True Then
                    Dim list As New ArrayList
                    list = txtMulEmployee.arrValueMember
                    list.Remove(Vendor)
                    txtMulEmployee.arrValueMember = list
                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub

    Dim isNextInvoiceDateExists As Boolean = False
    Private Sub funFillGrid(ByVal strVendorCode As String, ByVal trans As SqlTransaction)
        Try
            isNextInvoiceDateExists = False
            LoadBlankGrid()
            Dim WhrCls As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                WhrCls = ""
            Else
                WhrCls = " and location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            Dim strQryForRejectedAmt As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
         "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_PurchaseReturn_No  in (SELECT PR_No  FROM TSPL_PR_HEAD WHERE Against_PI =TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ) and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "

            '' this code is wrritten for that debit note which is created auto through PI and Pr is not created against that PI
            Dim strQryForRejectedAmtforNonPR As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
          "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_POInvoice_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "

            Dim strTaxRecovarableQuery As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then " &
          " ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'  " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end + " &
          " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y' " &
          " then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end) "
            If clsCommon.myLen(strVendorCode) > 0 Then
                strQuery = "Select * from ( select Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 1 else Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 2 else Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 3 else 0 end end end  as OrderRow , Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType],  " &
                " TSPL_VENDOR_INVOICE_HEAD.Document_No, Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else Document_No End as PurchaseInvoice," &
                " Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date End as DocumentDate, " &
                " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [DocDate] ,  " &
                " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [VendorInvoiceNo], " &
                " TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " as [OriginalAmt] ," &
                " TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as [TDSAmt], " &
                " (TSPL_VENDOR_INVOICE_HEAD.Document_Total- case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end) " + strTaxRecovarableQuery + " as [NetAmount], " &
                " TSPL_VENDOR_INVOICE_HEAD.Document_Total - case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end " + strTaxRecovarableQuery + " - ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') and isnull(TSPL_BANK_REVERSE.POST,'')='P') ),0) " &
                " -isnull((select sum(isnull(Payment_Amount,0)) from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Applied_Payment =TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') and isnull(TSPL_BANK_REVERSE.POST,'')='P') and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and isnull(TSPL_PAYMENT_HEADER.Applied_Payment ,'')<>'' AND Payment_No <> ''),0)  " & Environment.NewLine &
                " " + strQryForRejectedAmt + " " + strQryForRejectedAmtforNonPR + " " &
                " -ISNULL((Select SUM(isnull(TSPL_PAYMENT_ADJUSTMENT_DETAIL.Amount,0)) from TSPL_PAYMENT_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_ADJUSTMENT_DETAIL on TSPL_PAYMENT_ADJUSTMENT_Header.Adjustment_No=TSPL_PAYMENT_ADJUSTMENT_DETAIL.Adjustment_No Where isnull(TSPL_PAYMENT_ADJUSTMENT_Header.Doc_No,'') = isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,'') ),0) " &
                " as [PendingAmt],TSPL_VENDOR_INVOICE_HEAD.ConvRate " &
                " ,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation " &
                " from TSPL_VENDOR_INVOICE_HEAD " &
                " WHERE Vendor_Code ='" + strVendorCode + "'  AND ((ISNULL(RefDocNo,'')= '' or ISNULL((select VI.Document_Type from TSPL_VENDOR_INVOICE_HEAD VI where VI.Document_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo ),'') in ('I','')) AND ISNULL(Against_PurchaseReturn_No,'')= '') and not ( ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D') " &
                " and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY' " &
                " and isnull(TSPL_VENDOR_INVOICE_HEAD.posting_date,'')<>'' and isnull(TSPL_VENDOR_INVOICE_HEAD.Against_Salary_Generation_Code ,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='C' " &
                "  union all " & Environment.NewLine &
                " select  4 as OrderRow ,'Receipt Note' as [DocType], Payment_No  as Document_No, Payment_No as [PurchaseInvoice],convert(varchar ,Payment_Date ,103) as [DocumentDate] ,convert(varchar,Payment_Date ,103) as [DocDate] ,'' as VendorInvoiceNo,Payment_Amount  as [OriginalAmt],TDS_Amount as TDSAmt ,Payment_Amount - TDS_Amount as NetAmount," & Environment.NewLine &
                " (Payment_Amount - TDS_Amount -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_PAYMENT_DETAIL where TSPL_PAYMENT_DETAIL.Document_No=TSPL_PAYMENT_HEADER.Payment_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No )),0) ) as [PendingAmt] " & Environment.NewLine &
                " , ConvRate ,1 as ConvRateRevaluation from TSPL_PAYMENT_HEADER WHERE Payment_Type  ='RC' and IsChkReverse ='N' AND ISNULL(TSPL_PAYMENT_HEADER.Applied_Payment  ,'')='' AND TSPL_PAYMENT_HEADER.Is_Security=0  and  TSPL_PAYMENT_HEADER.Vendor_Code='" + strVendorCode + "' AND 1=2 " & Environment.NewLine &
                " ) FINALQRY WHERE FINALQRY.PendingAmt>0 and convert(date,FINALQRY.DocumentDate  ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' and Document_No <>'" & txtDocumentNo.Value & "' ORDER BY Convert(Date,FINALQRY.DocumentDate,103) ,OrderRow "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dtDocDate As Date = clsCommon.myCDate(dt.Rows(0)("DocumentDate"))
                    For Each dr As DataRow In dt.Rows
                        If SettingVendorSetOffDayWise Then
                            If Not clsCommon.myCDate(dr("DocumentDate")) = dtDocDate Then
                                Dim isAllDebitNote As Boolean = True
                                For ii As Integer = 0 To dgvReceipt.Rows.Count - 1
                                    If Not clsCommon.CompairString(dgvReceipt.Rows(ii).Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal Then
                                        isAllDebitNote = False
                                        Exit For
                                    End If
                                Next
                                If isAllDebitNote Then
                                    LoadBlankGrid()
                                    dtDocDate = clsCommon.myCDate(dr("DocumentDate"))
                                Else
                                    isNextInvoiceDateExists = True
                                    Exit For
                                End If
                            End If
                        End If
                        dgvReceipt.Rows.AddNew()
                        dgvReceipt.CurrentRow.Cells(colApply).Value = "No"
                        dgvReceipt.CurrentRow.Cells(colDocType).Value = clsCommon.myCstr(dr("DocType"))
                        dgvReceipt.CurrentRow.Cells(colPINo).Value = clsCommon.myCstr(dr("PurchaseInvoice"))
                        dgvReceipt.CurrentRow.Cells(colDocNo).Value = clsCommon.myCstr(dr("Document_No"))
                        If clsCommon.myCdbl(dr("ConvRateRevaluation")) > 0 Then
                            dgvReceipt.CurrentRow.Cells(colDocNo).Tag = clsCommon.myCdbl(dr("ConvRateRevaluation"))
                        Else
                            dgvReceipt.CurrentRow.Cells(colDocNo).Tag = clsCommon.myCdbl(dr("ConvRate"))
                        End If
                        dgvReceipt.CurrentRow.Cells(colDocDate).Value = clsCommon.myCstr(dr("DocDate"))
                        dgvReceipt.CurrentRow.Cells(colDocumentDate).Value = clsCommon.myCstr(dr("DocumentDate"))
                        dgvReceipt.CurrentRow.Cells(colVendorInvNo).Value = clsCommon.myCstr(dr("VendorInvoiceNo"))
                        dgvReceipt.CurrentRow.Cells(colOriginalAmt).Value = clsCommon.myCdbl(dr("OriginalAmt"))
                        dgvReceipt.CurrentRow.Cells(colTDSAmt).Value = clsCommon.myCstr(dr("TDSAmt"))
                        dgvReceipt.CurrentRow.Cells(colNetAmt).Value = clsCommon.myCdbl(dr("NetAmount"))
                        dgvReceipt.CurrentRow.Cells(colPendingAmt).Value = clsCommon.myCdbl(dr("PendingAmt"))
                        dgvReceipt.CurrentRow.Cells(colTemp).Value = clsCommon.myCdbl(dr("PendingAmt"))
                        dgvReceipt.CurrentRow.Cells(colAppliedAmt).ReadOnly = True
                        dgvReceipt.CurrentRow.Cells(colAdjustedAmt).Value = clsCommon.myCdbl(dr("NetAmount")) - clsCommon.myCdbl(dr("PendingAmt"))
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub AutoApplyAmt(ByVal tempAmt As Decimal)
        Try
            Dim ReceiptAmt As Decimal = 0
            For Each grow As GridViewRowInfo In dgvReceipt.Rows
                If tempAmt > 0 Then
                    If clsCommon.CompairString(grow.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Receipt Note") = CompairStringResult.Equal Then
                        grow.Cells(colApply).Value = "Yes"
                        If clsCommon.myCdbl(grow.Cells(colPendingAmt).Value) <= tempAmt Then
                            grow.Cells(colAppliedAmt).Value = clsCommon.myCdbl(grow.Cells(colPendingAmt).Value)
                            grow.Cells(colPendingAmt).Value = 0.0
                        ElseIf clsCommon.myCdbl(grow.Cells(colPendingAmt).Value) > tempAmt Then
                            grow.Cells(colAppliedAmt).Value = tempAmt
                            grow.Cells(colPendingAmt).Value = clsCommon.myCdbl(grow.Cells(colPendingAmt).Value) - tempAmt
                        End If
                        If clsCommon.CompairString(grow.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells(colDocType).Value, "Receipt Note") = CompairStringResult.Equal Then
                            tempAmt = tempAmt - clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            ReceiptAmt = ReceiptAmt + clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                        End If
                        lblreceiptamount.Text = clsCommon.myCdbl(ReceiptAmt)
                    End If
                Else
                    '' to apply credit note amount with debit note and invoices
                    Dim strinvoicecount As Integer = 0
                    For Each grow1 As GridViewRowInfo In dgvReceipt.Rows
                        If clsCommon.CompairString(grow1.Cells(colApply).Value, "No") = CompairStringResult.Equal OrElse (clsCommon.CompairString(grow1.Cells(colApply).Value, "Yes") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(grow1.Cells(colPendingAmt).Value) > 0) Then
                            'If clsCommon.CompairString(grow1.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(grow1.Cells(colPendingAmt).Value) > 0 Then
                            '    strinvoicecount = strinvoicecount + 1
                            'End If
                            If (clsCommon.CompairString(grow1.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Or clsCommon.CompairString(grow1.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colDocType).Value, "Receipt Note") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(grow1.Cells(colPendingAmt).Value) > 0 Then
                                strinvoicecount = strinvoicecount + 1
                            End If


                            If strinvoicecount > 0 Then
                                For Each gvexternal As GridViewRowInfo In dgvReceipt.Rows
                                    If clsCommon.CompairString(gvexternal.Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvexternal.Cells(colPendingAmt).Value) > 0 Then
                                        tempAmt = clsCommon.myCdbl(gvexternal.Cells(colPendingAmt).Value)
                                        gvexternal.Cells(colApply).Value = "Yes"

                                        For Each gvinternal As GridViewRowInfo In dgvReceipt.Rows
                                            If clsCommon.CompairString(gvinternal.Cells(colDocType).Value, "Debit Note") <> CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvinternal.Cells(colPendingAmt).Value) > 0 Then
                                                gvinternal.Cells(colApply).Value = "Yes"
                                                If clsCommon.myCdbl(gvinternal.Cells(colPendingAmt).Value) <= tempAmt Then
                                                    Dim dblinternalremaingbal As Double = clsCommon.myCdbl(gvinternal.Cells(colPendingAmt).Value)

                                                    gvinternal.Cells(colAppliedAmt).Value = clsCommon.myCdbl(gvinternal.Cells(colAppliedAmt).Value) + clsCommon.myCdbl(gvinternal.Cells(colPendingAmt).Value)
                                                    gvinternal.Cells(colPendingAmt).Value = 0.0

                                                    gvexternal.Cells(colAppliedAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colAppliedAmt).Value) + clsCommon.myCdbl(dblinternalremaingbal)
                                                    gvexternal.Cells(colPendingAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colPendingAmt).Value) - clsCommon.myCdbl(dblinternalremaingbal)
                                                    tempAmt = 0
                                                    'tempAmt =
                                                ElseIf clsCommon.myCdbl(gvinternal.Cells(colPendingAmt).Value) > tempAmt Then
                                                    gvinternal.Cells(colAppliedAmt).Value = gvinternal.Cells(colAppliedAmt).Value + tempAmt
                                                    gvinternal.Cells(colPendingAmt).Value = clsCommon.myCdbl(gvinternal.Cells(colPendingAmt).Value) - tempAmt

                                                    gvexternal.Cells(colAppliedAmt).Value = gvexternal.Cells(colAppliedAmt).Value + tempAmt
                                                    gvexternal.Cells(colPendingAmt).Value = clsCommon.myCdbl(gvexternal.Cells(colPendingAmt).Value) - tempAmt
                                                    tempAmt = 0
                                                End If

                                                If tempAmt = 0 Then
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                        If tempAmt = 0 Then
                                            Exit For
                                        End If
                                    End If
                                Next
                                strinvoicecount = 0
                            End If

                        End If

                    Next
                    Exit For
                    ''---------------------------------- to apply credit note amount with debit note and invoices
                End If
                '---------------
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Function SaveData(ByVal strvendor As String, ByVal trans As SqlTransaction)
        Try

            Dim obj As New clsPaymentHeader()


            obj.Entry_Desc = "Against Employee set off for " + lblDocumentType.Text + " document No. " + txtDocumentNo.Value

            For Each grow As GridViewRowInfo In dgvReceipt.Rows
                If grow.Cells(colApply).Value = "Yes" Then
                    If clsCommon.myCDate(dgvReceipt.Rows(0).Cells(colDocDate).Value) >= clsCommon.myCDate(grow.Cells(colDocDate).Value) Then
                        lbldocinvoicedate.Text = clsCommon.myCDate(dgvReceipt.Rows(0).Cells(colDocDate).Value)
                    Else
                        lbldocinvoicedate.Text = clsCommon.myCDate(grow.Cells(colDocDate).Value)
                    End If
                End If
            Next

            If clsCommon.myCDate(lbldocumentdate.Text) >= clsCommon.myCDate(lbldocinvoicedate.Text) Then
                obj.Payment_Date = clsCommon.myCDate(lbldocumentdate.Text)
                obj.Payment_Post_Date = clsCommon.myCDate(lbldocumentdate.Text)
            Else

                obj.Payment_Date = clsCommon.myCDate(lbldocinvoicedate.Text)
                obj.Payment_Post_Date = clsCommon.myCDate(lbldocinvoicedate.Text)
            End If

            Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & txtDocumentNo.Value & "'", trans))

            If clsCommon.myLen(strdocumentType) > 0 AndAlso clsCommon.CompairString(strdocumentType, "D") = CompairStringResult.Equal Then
                Dim strbancode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDefaultBankCodeforCreditNote, clsFixedParameterCode.AllowDefaultBankCodeforCreditNote, trans))
                If clsCommon.myLen(strbancode) > 0 Then
                    Dim bankcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE as [Code] from TSPL_BANK_MASTEr where TSPL_bank_master.INACTIVE ='Active' and TSPL_BANK_MASTER.bank_type<>'S' and Bank_Code='" & clsCommon.myCstr(strbancode) & "' ", trans))
                    If clsCommon.myLen(bankcode) <= 0 Then
                        Throw New Exception("Please enter Bank Code into fixed parameter.")
                    End If
                Else
                    Throw New Exception("Please enter Bank Code into fixed parameter.")
                End If


                obj.Bank_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDefaultBankCodeforCreditNote, clsFixedParameterCode.AllowDefaultBankCodeforCreditNote, trans))
                obj.Payment_Code = "NEFT"
                obj.ConvRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_VENDOR_INVOICE_HEAD.ConvRate,1) as ConvRate, isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.Payment_Date), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & txtDocumentNo.Value & "' )xx", trans))
                If clsCommon.myCdbl(obj.ConvRate) = 0 Then
                    obj.ConvRate = 1
                End If
                obj.ConvRateOld = obj.ConvRate
                obj.BASE_CURRENCY_CODE = objCommonVar.BaseCurrencyCode

                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select CURRENCY_CODE,ApplicableFrom ,Vendor_Name from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & txtDocumentNo.Value & "'", trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                    obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                    If clsCommon.myLen(dt.Rows(0)("ApplicableFrom")) > 0 Then
                        obj.ApplicableFrom = dt.Rows(0)("ApplicableFrom")
                    Else
                        obj.ApplicableFrom = Nothing
                    End If
                End If
            Else
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_PAYMENT_HEADER.ConvRate,TSPL_PAYMENT_HEADER.ConvRateOld ,TSPL_PAYMENT_HEADER.ApplicableFrom,TSPL_PAYMENT_HEADER.Bank_Code,TSPL_PAYMENT_HEADER.Payment_Code,TSPL_PAYMENT_HEADER.Vendor_Name ,TSPL_PAYMENT_HEADER.BASE_CURRENCY_CODE,TSPL_PAYMENT_HEADER.CURRENCY_CODE FROM TSPL_PAYMENT_HEADER  WHERE TSPL_PAYMENT_HEADER.Payment_No ='" & txtDocumentNo.Value & "'", trans)
                ' Dim dt As DataTable = clsDBFuncationality.GetDataTable("select xx.Bank_Code,xx.Payment_Code,xx.DESCRIPTION,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate  from ( SELECT TSPL_PAYMENT_HEADER.Bank_Code,TSPL_PAYMENT_HEADER.Payment_Code,TSPL_BANK_MASTER.DESCRIPTION,TSPL_PAYMENT_HEADER.ConvRate,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.Payment_Date), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation FROM TSPL_PAYMENT_HEADER left join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.BANK_CODE  WHERE TSPL_PAYMENT_HEADER.Payment_No ='" & txtDocumentNo.Value & "')xx", trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                    obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
                    obj.Vendor_Name = clsCommon.myCstr(clsVendorMaster.GetName(strvendor, trans))
                    obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                    obj.BASE_CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("BASE_CURRENCY_CODE"))
                    'obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
                    obj.ConvRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate  from ( SELECT TSPL_PAYMENT_HEADER.Bank_Code,TSPL_PAYMENT_HEADER.Payment_Code,TSPL_BANK_MASTER.DESCRIPTION,TSPL_PAYMENT_HEADER.ConvRate,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.Payment_Date), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation FROM TSPL_PAYMENT_HEADER left join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.BANK_CODE  WHERE TSPL_PAYMENT_HEADER.Payment_No ='" & txtDocumentNo.Value & "')xx", trans))
                    obj.ConvRateOld = clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
                    If clsCommon.myLen(dt.Rows(0)("ApplicableFrom")) > 0 Then
                        obj.ApplicableFrom = dt.Rows(0)("ApplicableFrom")
                    Else
                        obj.ApplicableFrom = Nothing
                    End If

                End If
            End If

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_PAYMENT_HEADER.ConvRate,TSPL_PAYMENT_HEADER.ConvRateOld ,TSPL_PAYMENT_HEADER.ApplicableFrom,TSPL_PAYMENT_HEADER.Bank_Code,TSPL_PAYMENT_HEADER.Payment_Code,TSPL_PAYMENT_HEADER.Vendor_Name ,TSPL_PAYMENT_HEADER.BASE_CURRENCY_CODE,TSPL_PAYMENT_HEADER.CURRENCY_CODE FROM TSPL_PAYMENT_HEADER  WHERE TSPL_PAYMENT_HEADER.Payment_No ='" & txtDocumentNo.Value & "'", trans)
            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '    obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            '    obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            '    obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            '    obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            '    obj.BASE_CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("BASE_CURRENCY_CODE"))
            '    obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            '    obj.ConvRateOld = clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))
            '    If clsCommon.myLen(dt.Rows(0)("ApplicableFrom")) > 0 Then
            '        obj.ApplicableFrom = dt.Rows(0)("ApplicableFrom")
            '    Else
            '        obj.ApplicableFrom = Nothing
            '    End If

            'End If

            obj.Payment_Type = "AD"
            obj.Vendor_Code = clsCommon.myCstr(strvendor)
            obj.Account_Payee = 0
            obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "')", trans))


            obj.memorndmamt = "0"
            obj.CHECK_PRINT = 0
            obj.Cheque_No = ""
            obj.Cheque_Date = Nothing


            Dim OutstandingAmt As Decimal = 0

            If clsCommon.myCdbl(lblBalAmt.Text) > clsCommon.myCdbl(lblreceiptamount.Text) Then
                obj.Payment_Amount = clsCommon.myCdbl(lblreceiptamount.Text)
            Else
                obj.Payment_Amount = clsCommon.myCdbl(lblBalAmt.Text)
            End If
            obj.Balance_Amt = obj.Payment_Amount
            obj.Is_Security = 0
            obj.Advance_Against_Salary = 0


            obj.IsChkReverse = "N"
            obj.Bank_Charges = 0

            '  obj.objRemittance = objRemittance

            obj.Applied_Payment = clsCommon.myCstr(txtDocumentNo.Value)

            obj.ArrTr = New List(Of clsPaymentDetail)

            '============================Detail Section==============================
            Dim TotalSecurityAmount As Double = 0
            For Each grow As GridViewRowInfo In dgvReceipt.Rows
                If clsCommon.myCstr(grow.Cells(colApply).Value) = "Yes" Then
                    Dim objTr As New clsPaymentDetail()
                    objTr.Apply = "1"
                    objTr.Payment_Type = "AD"
                    objTr.Document_No = clsCommon.myCstr(grow.Cells(colDocNo).Value)
                    If grow.Cells(colDocNo).Tag Is Nothing Then
                        objTr.ConvRateOld = 1
                    Else
                        objTr.ConvRateOld = IIf(clsCommon.myCdbl(grow.Cells(colDocNo).Tag) = 0, 1, clsCommon.myCdbl(grow.Cells(colDocNo).Tag))
                    End If

                    objTr.Vendor_Invoice_No = clsCommon.myCstr(grow.Cells(colVendorInvNo).Value)
                    objTr.Original_Invoice_Amt = clsCommon.myCdbl(grow.Cells(colOriginalAmt).Value)
                    objTr.TDS_Amount = clsCommon.myCdbl(grow.Cells(colTDSAmt).Value)
                    objTr.Applied_Amount = clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)

                    objTr.Pending_Balance = clsCommon.myCdbl(grow.Cells(colPendingAmt).Value)
                    objTr.Net_Balance = clsCommon.myCdbl(grow.Cells(colOriginalAmt).Value)
                    objTr.Security_Amount = clsCommon.myCdbl(grow.Cells(colSecurityAmt).Value)
                    TotalSecurityAmount += objTr.Security_Amount
                    objTr.Comment = clsCommon.myCstr(grow.Cells(colComment).Value)
                    obj.ArrTr.Add(objTr)
                End If
            Next
            obj.Total_Security_Amount = TotalSecurityAmount



            '==================Detail Section Ends Here=======================


            If clsCommon.CompairString(obj.CURRENCY_CODE, obj.BASE_CURRENCY_CODE) <> CompairStringResult.Equal Then

                Dim dt1 As DataTable
                dt1 = clsPaymentHeader.GetExchangeDetailDt(strvendor, trans)
                If dt1.Rows.Count > 0 Then
                    obj.EXCHANGE_GAIN_ACCOUNT = clsCommon.myCstr(dt1.Rows(0).Item("EXCHANGE_GAIN_ACCOUNT"))
                    obj.EXCHANGE_LOSS_ACCOUNT = clsCommon.myCstr(dt1.Rows(0).Item("EXCHANGE_LOSS_ACCOUNT"))
                Else
                    obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                    obj.EXCHANGE_LOSS_ACCOUNT = Nothing
                End If
                Dim dtLastRate As DataTable
                '' gather conv rate and amount of transaction to calculate exchange loss and gain
                Dim strInvoiceNo As String = String.Empty
                Dim lossorgainamount As Double = 0
                Dim Totallossorgainamount As Double = 0
                Dim InvoiceType As String = ""
                For i As Integer = 0 To dgvReceipt.Rows.Count - 1
                    strInvoiceNo = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colDocNo).Value)
                    InvoiceType = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colDocType).Value)
                    dtLastRate = clsPaymentHeader.GetExchangeRateAmount(strInvoiceNo, obj.Payment_Date, trans)
                    If clsCommon.CompairString(InvoiceType, "Debit Note") = CompairStringResult.Equal Then
                        lossorgainamount = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colAppliedAmt).Value) * dtLastRate.Rows(0).Item("ConvRate") * -1
                    Else
                        lossorgainamount = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colAppliedAmt).Value) * dtLastRate.Rows(0).Item("ConvRate")
                    End If
                    Totallossorgainamount = Totallossorgainamount + lossorgainamount
                Next
                Dim diff As Double = 0.0
                If Totallossorgainamount <> 0 Then
                    diff = obj.PAYMENT_AMOUNT_BASE_CURRENCY - Totallossorgainamount
                End If

                If diff = 0 Then
                    obj.EXCHANGE_LOSS_AMT = 0
                    obj.EXCHANGE_GAIN_AMT = 0
                ElseIf diff > 0 Then
                    If clsCommon.myLen(obj.EXCHANGE_LOSS_ACCOUNT) = 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Exchange Loss Account not defined.", Me.Text)
                        Return False
                    End If
                    obj.EXCHANGE_LOSS_AMT = diff
                    obj.EXCHANGE_GAIN_AMT = 0
                Else
                    If clsCommon.myLen(obj.EXCHANGE_GAIN_ACCOUNT) = 0 Then
                        clsCommon.MyMessageBoxShow("Exchange Gain Account not defined.")
                        Return False
                    End If
                    obj.EXCHANGE_LOSS_AMT = 0
                    obj.EXCHANGE_GAIN_AMT = -diff
                End If
            End If

            obj.PAYMENT_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(obj.Payment_Amount * obj.ConvRate)
            '   obj.PAYMENT_AMOUNT_BASE_CURRENCY = (clsCommon.myCdbl(txtPaymentAmt.Text) - clsCommon.myCdbl(txtTDSAmt.Text)) * clsCommon.myCdbl(txtConversionRate.Value)
            ''--------------------------
            ' obj.SaveData(obj, isNewEntry)
            obj.IsApplyDocAuto = 1

            obj.SaveData1(obj, True, trans)
            clsPaymentHeader.PostData(obj.Payment_No, "MPayable", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        ResetOnDate()
    End Sub

    Private Sub txtToDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtToDate.Validating
        ResetOnDate()
    End Sub


    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtEmployeeGroup._My_Click
        Dim strQry As String = "select Ven_Group_Code as Code, Group_Desc as Name from TSPL_VENDOR_GROUP WHERE Ven_Group_Code in (Select distinct Vendor_Group_Code  from tspl_vendor_master where isemployee=1 ) order by Ven_Group_Code "
        txtEmployeeGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ESOffEmpG", strQry, "Code", "Name", txtEmployeeGroup.arrValueMember, txtEmployeeGroup.arrDispalyMember)
        txtMulEmployee.arrValueMember = Nothing
    End Sub

    Private Sub txtMulVendor__My_Click(sender As Object, e As EventArgs) Handles txtMulEmployee._My_Click
        Dim strQryForRejectedAmt As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
        "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_PurchaseReturn_No  in (SELECT PR_No  FROM TSPL_PR_HEAD WHERE Against_PI =TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ) and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "

        '' this code is wrritten for that debit note which is created auto through PI and Pr is not created against that PI
        Dim strQryForRejectedAmtforNonPR As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
      "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_POInvoice_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "


        Dim strTaxRecovarableQuery As String = " - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then " &
      " ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'  " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end + " &
      " case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y' " &
      " then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end) "

        Dim qry As String = "Select distinct Final.Vendor_Code as Code,v.Vendor_Name as Name from ( Select TSPL_PAYMENT_HEADER.Vendor_Code ,Payment_No as [Code], Entry_Desc as [Description], Payment_Date as [Payment Date], Case When Payment_Type='AV' Then 'Advance' Else 'On Account' End As [Payment Type], Payment_Amount+isnull(TDS_Amount ,0) as [Payment Amt], Payment_Amount+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.Payment_Type='AD' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No in (select PPH.Payment_No from TSPL_PAYMENT_HEADER PPH where PPH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No and isnull(TSPL_BANK_REVERSE.POST,'')='P' and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'))) AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No ),0) as [Bal Amt]  from TSPL_PAYMENT_HEADER WHERE Posted='1'  AND   IsChkReverse='N' and Payment_Type IN ('AV','OA') " &
     " and (convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' )  and TSPL_PAYMENT_HEADER.advance_against_salary=1 " & Environment.NewLine &
     "Union All" & Environment.NewLine &
     "Select Vendor_Code ,Document_No AS Code,'' as Description,DocumentDate as [Payment Date],DocType as [Payment Type],OriginalAmt as [Payment Amt],PendingAmt as [Bal Amt]  from ( select Vendor_Code, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType],  " &
     " TSPL_VENDOR_INVOICE_HEAD.Document_No, Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else Document_No End as PurchaseInvoice," &
     " Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date End as DocumentDate, " &
     " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [DocDate] ,  " &
     " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [VendorInvoiceNo], " &
     " TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " as [OriginalAmt] ," &
     " TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as [TDSAmt], " &
     " (TSPL_VENDOR_INVOICE_HEAD.Document_Total - case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end) " + strTaxRecovarableQuery + " as [NetAmount], " &
     " TSPL_VENDOR_INVOICE_HEAD.Document_Total - case when isnull(TSPL_VENDOR_INVOICE_HEAD .is_For_TDS,0) =1 and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='D' then 0 else isnull(TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,0) end " + strTaxRecovarableQuery + " - ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No  and isnull(TSPL_BANK_REVERSE.POST,'')='P' and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "')) ),0) " &
     " -isnull((select sum(isnull(Payment_Amount,0)) from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Applied_Payment =TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No  and isnull(TSPL_BANK_REVERSE.POST,'')='P' and (Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and Convert(Date,TSPL_BANK_REVERSE.Reversal_Date ,103)<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') ) and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and isnull(TSPL_PAYMENT_HEADER.Applied_Payment ,'')<>'' AND Payment_No <> ''),0)  " & Environment.NewLine &
     " " + strQryForRejectedAmt + " " + strQryForRejectedAmtforNonPR + " " &
     " -ISNULL((Select SUM(isnull(TSPL_PAYMENT_ADJUSTMENT_DETAIL.Amount,0)) from TSPL_PAYMENT_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_ADJUSTMENT_DETAIL on TSPL_PAYMENT_ADJUSTMENT_Header.Adjustment_No=TSPL_PAYMENT_ADJUSTMENT_DETAIL.Adjustment_No Where isnull(TSPL_PAYMENT_ADJUSTMENT_Header.Doc_No,'') = isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,'') ),0) " &
     " as [PendingAmt],TSPL_VENDOR_INVOICE_HEAD.ConvRate " &
     " ,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation " &
     " from TSPL_VENDOR_INVOICE_HEAD " &
       " WHERE  ((ISNULL(RefDocNo,'')= '' or ISNULL((select VI.Document_Type from TSPL_VENDOR_INVOICE_HEAD VI where VI.Document_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo ),'') in ('I','')) AND ISNULL(Against_PurchaseReturn_No,'')= '') and not ( ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D') " &
     " and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY' " &
     " and isnull(TSPL_VENDOR_INVOICE_HEAD.posting_date,'')<>''" &
    "  union all " & Environment.NewLine &
    " select  TSPL_PAYMENT_HEADER.Vendor_Code,'Receipt Note' as [DocType], Payment_No  as Document_No, Payment_No as [PurchaseInvoice],convert(varchar ,Payment_Date ,103) as [DocumentDate] ,convert(varchar,Payment_Date ,103) as [DocDate] ,'' as VendorInvoiceNo,Payment_Amount  as [OriginalAmt],TDS_Amount as TDSAmt ,Payment_Amount - TDS_Amount as NetAmount," & Environment.NewLine &
    " (Payment_Amount - TDS_Amount -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_PAYMENT_DETAIL where TSPL_PAYMENT_DETAIL.Document_No=TSPL_PAYMENT_HEADER.Payment_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No )),0) ) as [PendingAmt] " & Environment.NewLine &
    " , ConvRate ,1 as ConvRateRevaluation from TSPL_PAYMENT_HEADER WHERE Payment_Type  ='RC' and IsChkReverse ='N' AND ISNULL(TSPL_PAYMENT_HEADER.Applied_Payment  ,'')='' AND TSPL_PAYMENT_HEADER.Is_Security=0 " & Environment.NewLine &
     " ) FINALQRY WHERE FINALQRY.PendingAmt>0 and (convert(date,FINALQRY.DocumentDate ,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'  and convert(date,FINALQRY.DocumentDate ,103) <='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' ) and DocType ='Debit Note' and 1=2 " & Environment.NewLine &
     " ) Final" &
     " left outer join TSPL_VENDOR_MASTER v on v.Vendor_Code=Final.Vendor_Code " &
     " where [Bal Amt]>0  and v.isemployee=1  "

        If txtEmployeeGroup.arrValueMember IsNot Nothing AndAlso txtEmployeeGroup.arrValueMember.Count > 0 Then
            qry += " and v.Vendor_Group_Code in (" + clsCommon.GetMulcallString(txtEmployeeGroup.arrValueMember) + ")  "
        End If


        txtMulEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "ESOffEmpG", qry, "Code", "", txtMulEmployee.arrValueMember, Nothing)
    End Sub
End Class