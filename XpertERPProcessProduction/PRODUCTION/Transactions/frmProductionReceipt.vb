''21/09/2013---Created by --[Panch Raj]-- Ticket no : 

Imports common
Imports Microsoft.Office.Interop

Public Class frmProductionReceipt
    Inherits FrmMainTranScreen
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsFormLoad As Boolean = False

    Const colCheck As String = "Check"
    Const colLineNo As String = "LineNO"
    Const colPPCode As String = "PPCode"
    Const colProdLineCode As String = "ProdLineCode"
    Const colBOMCode As String = "BOMCode"

    Const colItemCode As String = "ItemCode"
    Const colItemDesc As String = "ItemDesc"
    Const colUOM As String = "UOM"
    Const colBatchQty As String = "BatchQTY"
    Const colIssueQty As String = "IssueQty"
    Const colBalanceBatchQty As String = "BalanceBatchQTY"
    Const colBalanceIssueQty As String = "BalanceIssueQty"
    Const colReceiptQty As String = "ReceiptQty"
    Const colRejHead As String = "RejHead"
    Const colRejQty As String = "RejQty"
    Const colBreakageHead As String = "BreakageHead"
    Const colBreakageQty As String = "BreakageQty"
    Const colLabTesting As String = "LabTesting"
    Const colStartTime As String = "StartTime"
    Const colEndTime As String = "EndTime"
    Const colMfgDate As String = "MfgDate"
    Const colExpDate As String = "ExpDate"
    Const colFIFO_Cost As String = "colFIFO_Cost"
    Const colLIFO_Cost As String = "colLIFO_Cost"
    Const colAVG_Cost As String = "colAVG_Cost"
    Const colIsPickAutoSrNo As String = "colIsPickAutoSrNo"
    Const colIsSerialseItem As String = "colIsSerialseItem"

    Const colCost_Method As String = "colCost_Method"
    '===========
    Const colAttachment As String = "colAttachment"
    Const colShowAttachment As String = "colShowAttachment"
    Const colAttachmentId As String = "colAttachmentId"
    Const colAttachmentpath As String = "colAttachmentpath"
    Const ColDelete As String = "ColDelete"
    '===========
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Public strDocumentNo As String = ""
    Dim objList As New List(Of clsProductionReceipt)
    Dim obj As New clsProductionReceipt
    Public Const strCostTransaction As String = "Production Entry"
    Public MOActive As Boolean = False
    Private Const ReportID As String = clsUserMgtCode.frmProductionReceiptSTD
    Dim openFileDialog1 As New OpenFileDialog
#End Region
    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmProductionReceiptSTD Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionReceiptSTD)
        ElseIf formtype = clsUserMgtCode.frmProductionReceiptPepsi Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionReceiptPepsi)
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnUnpost.Enabled = True
        Else
            btnUnpost.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '' get mo setting
        '===========================

        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MF_RECEIPT_DETAIL alter column BOM_CODE Varchar(30)  NULL")
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MF_RECEIPT_DETAIL alter column PRODUCTION_LINE_CODE VARCHAR(30)  NULL")
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MF_RECEIPT_DETAIL alter column BATCH_QTY NUMERIC(18, 6)  NULL")
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MF_RECEIPT_DETAIL alter column BALANCE_BATCH_QTY NUMERIC(18, 6)  NULL")
        '===========================
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        RadPageView1.SelectedPage = RadPageViewPage1

        LoadBlankGrid()
        funReset()
        SetLength()
        btnUnpost.Visible = False
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtComment.MaxLength = 200
    End Sub


    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtComment.Text = ""
        dtpDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        dtpBatchDate.Value = dtpDate.Value
        txtReceivedBy.Value = ""
        lblEmpName.Text = ""
        txtCode.MyReadOnly = False
        fndIssueCode.Value = ""
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn

        Dim PPCode As New GridViewTextBoxColumn
        Dim ProdLineCode As New GridViewTextBoxColumn
        Dim BOMCode As New GridViewTextBoxColumn

        Dim ItemCode As New GridViewTextBoxColumn
        Dim ItemDesc As New GridViewTextBoxColumn
        Dim UOM As New GridViewTextBoxColumn
        Dim BatchQty As New GridViewDecimalColumn
        Dim IssueQty As New GridViewDecimalColumn
        Dim BalanceBatchQty As New GridViewDecimalColumn
        Dim BalanceIssueQty As New GridViewDecimalColumn
        Dim ReceiptQty As New GridViewDecimalColumn
        Dim RejHead As New GridViewTextBoxColumn
        Dim RejQty As New GridViewDecimalColumn
        Dim BreakageHead As New GridViewTextBoxColumn
        Dim BreakageQty As New GridViewDecimalColumn
        Dim LabTesting As New GridViewTextBoxColumn
        Dim StartTime As New GridViewDateTimeColumn
        Dim EndTime As New GridViewDateTimeColumn
        Dim MfgDate As New GridViewDateTimeColumn
        Dim ExpDate As New GridViewDateTimeColumn

        Dim FIFOCost As New GridViewDecimalColumn
        Dim LIFOCost As New GridViewDecimalColumn
        Dim AvgCost As New GridViewDecimalColumn
        Dim CostingMethod As New GridViewTextBoxColumn
        Dim check As New GridViewCheckBoxColumn ' colCheck

        check.FormatString = ""
        check.HeaderText = ""
        check.Name = colCheck
        check.Width = 50
        check.ReadOnly = False
        check.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(check)

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 50
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(LineNo)

        PPCode.FormatString = ""
        PPCode.HeaderText = "PP Code"
        PPCode.Name = colPPCode
        PPCode.Width = 70
        PPCode.ReadOnly = True
        PPCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(PPCode)

        ProdLineCode.FormatString = ""
        ProdLineCode.HeaderText = "Prod.Line Code"
        ProdLineCode.Name = colProdLineCode
        ProdLineCode.Width = 70
        ProdLineCode.ReadOnly = True
        ProdLineCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ProdLineCode)

        BOMCode.FormatString = ""
        BOMCode.HeaderText = "BOM Code"
        BOMCode.Name = colBOMCode
        BOMCode.Width = 70
        BOMCode.ReadOnly = True
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BOMCode)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ItemCode)

        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        ItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ItemDesc)

        UOM.FormatString = ""
        UOM.HeaderText = "UOM"
        UOM.Name = colUOM
        UOM.Width = 100
        UOM.ReadOnly = True
        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(UOM)

        BatchQty.FormatString = ""
        BatchQty.HeaderText = "Batch Qty"
        BatchQty.Name = colBatchQty
        BatchQty.Width = 100
        BatchQty.ReadOnly = True
        BatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BatchQty)


        BalanceBatchQty.FormatString = ""
        BalanceBatchQty.HeaderText = "Balance Batch Qty"
        BalanceBatchQty.Name = colBalanceBatchQty
        BalanceBatchQty.Width = 130
        BalanceBatchQty.ReadOnly = True
        BalanceBatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BalanceBatchQty)
        '===============================
        IssueQty.FormatString = ""
        IssueQty.HeaderText = "Issue Qty"
        IssueQty.Name = colIssueQty
        IssueQty.Width = 100
        IssueQty.ReadOnly = True
        IssueQty.IsVisible = chkTrading.Checked
        IssueQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(IssueQty)

        'BalanceIssueQty
        BalanceIssueQty.FormatString = ""
        BalanceIssueQty.HeaderText = "Balance Issue Qty"
        BalanceIssueQty.Name = colBalanceIssueQty
        BalanceIssueQty.Width = 130
        BalanceIssueQty.ReadOnly = True
        BalanceIssueQty.IsVisible = chkTrading.Checked
        BalanceIssueQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BalanceIssueQty)
        '========================
        ReceiptQty.FormatString = ""
        ReceiptQty.HeaderText = "Receipt Qty"
        ReceiptQty.Name = colReceiptQty
        ReceiptQty.Width = 100
        'ReceiptQty.ReadOnly = True
        ReceiptQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ReceiptQty)

        RejHead.FormatString = ""
        RejHead.HeaderText = "Rejection Head"
        RejHead.Name = colRejHead
        RejHead.Width = 100
        'RejHead.ReadOnly = True
        RejHead.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(RejHead)

        RejQty.FormatString = ""
        RejQty.HeaderText = "Rejection Qty"
        RejQty.Name = colRejQty
        RejQty.Width = 100
        'RejQty.ReadOnly = True
        RejQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(RejQty)

        BreakageHead.FormatString = ""
        BreakageHead.HeaderText = "Breakage Head"
        BreakageHead.Name = colBreakageHead
        BreakageHead.Width = 100
        'BreakageHead.ReadOnly = True
        BreakageHead.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BreakageHead)

        BreakageQty.FormatString = ""
        BreakageQty.HeaderText = "Breakage Qty"
        BreakageQty.Name = colBreakageQty
        BreakageQty.Width = 100
        'BreakageQty.ReadOnly = True
        BreakageQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BreakageQty)

        LabTesting.FormatString = ""
        LabTesting.HeaderText = "Lab Testing"
        LabTesting.Name = colLabTesting
        LabTesting.Width = 100
        'LabTesting.ReadOnly = True
        LabTesting.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(LabTesting)

        StartTime.Format = DateTimePickerFormat.Time
        StartTime.HeaderText = "Start Time"
        StartTime.Name = colStartTime
        StartTime.CustomFormat = "hh:mm tt"
        StartTime.FormatString = "{0:hh:mm tt}"
        StartTime.Width = 130
        StartTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(StartTime)

        EndTime.Format = DateTimePickerFormat.Time
        EndTime.HeaderText = "End Time"
        EndTime.CustomFormat = "hh:mm tt"
        EndTime.FormatString = "{0:hh:mm tt}"
        EndTime.Name = colEndTime
        EndTime.Width = 130
        EndTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(EndTime)

        MfgDate.FormatString = ""
        MfgDate.HeaderText = "Manufacturing Date"
        MfgDate.Name = colMfgDate
        MfgDate.Width = 100
        MfgDate.ReadOnly = True
        MfgDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(MfgDate)

        ExpDate.FormatString = ""
        ExpDate.HeaderText = "Expiry Date"
        ExpDate.Name = colExpDate
        ExpDate.Width = 100
        'ExpDate.ReadOnly = True
        ExpDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ExpDate)

        ''costing columns

        CostingMethod.FormatString = ""
        CostingMethod.HeaderText = "Costing Method"
        CostingMethod.Name = colCost_Method
        CostingMethod.Width = 100
        CostingMethod.ReadOnly = True
        CostingMethod.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(CostingMethod)

        FIFOCost.FormatString = ""
        FIFOCost.HeaderText = "FIFO Cost"
        FIFOCost.Name = colFIFO_Cost
        FIFOCost.Width = 100
        FIFOCost.ReadOnly = True
        FIFOCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FIFOCost)

        LIFOCost.FormatString = ""
        LIFOCost.HeaderText = "LIFO Cost"
        LIFOCost.Name = colLIFO_Cost
        LIFOCost.Width = 100
        LIFOCost.ReadOnly = True
        LIFOCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(LIFOCost)

        AvgCost.FormatString = ""
        AvgCost.HeaderText = "AVG Cost"
        AvgCost.Name = colAVG_Cost
        AvgCost.Width = 100
        AvgCost.ReadOnly = True
        AvgCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(AvgCost)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIsPickAutoSrNo
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsPickAutoSerNo) '140

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoAttachment As GridViewCommandColumn = New GridViewCommandColumn()
        repoAttachment.FormatString = ""
        repoAttachment.UseDefaultText = True
        repoAttachment.DefaultText = "Add"
        repoAttachment.HeaderText = "Add"
        repoAttachment.Width = 70
        repoAttachment.Name = colAttachment
        'repoSelect.IsVisible = False
        repoAttachment.FieldName = colAttachment
        repoAttachment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoAttachment)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Show"
        ShowBtn.Name = colShowAttachment
        ShowBtn.FieldName = colShowAttachment
        ShowBtn.Width = 70
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ShowBtn)

        Dim repoDelete As GridViewCommandColumn = New GridViewCommandColumn()
        repoDelete.FormatString = ""
        repoDelete.UseDefaultText = True

        repoDelete.DefaultText = "Delete"
        repoDelete.HeaderText = "Delete"
        repoDelete.Width = 100
        repoDelete.Name = ColDelete
        repoDelete.FieldName = ColDelete
        repoDelete.Width = 70
        repoDelete.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDelete)


        Dim repoAttachment_Id As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachment_Id.FormatString = ""
        repoAttachment_Id.HeaderText = "Attachment_id"
        repoAttachment_Id.Name = colAttachmentId
        repoAttachment_Id.Width = 0
        repoAttachment_Id.ReadOnly = True
        repoAttachment_Id.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAttachment_Id)

        Dim repoAttachmentpath As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachmentpath.FormatString = ""
        repoAttachmentpath.HeaderText = "Attachment Path"
        repoAttachmentpath.Name = colAttachmentpath
        repoAttachmentpath.Width = 330
        repoAttachmentpath.ReadOnly = True
        repoAttachmentpath.IsVisible = True
        repoAttachmentpath.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoAttachmentpath)

    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim frm As FrmSerializeItemIn = New FrmSerializeItemIn()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemDesc).Value)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colReceiptQty).Value)
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            'gv1.CurrentRow.Cells(colReceiptQty).Value = frm.AcceptedQty
            'gv1.CurrentRow.Cells(colRejQty).Value = frm.RejectedQty
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
            End If
        End If
    End Sub

    Sub funReset()

        '' get mo setting
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()
        If MOActive = True Then
            lblBatchNo.Text = "MO NO"
        Else
            lblBatchNo.Text = "Batch NO"
        End If
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        dtpDate.Focus()
        txtLocation.Enabled = True
        gv1.Rows.Clear()
        Me.txtBatchNo.Value = Nothing
        Me.txtReceivedBy.Value = Nothing
        Me.txtLocation.Value = Nothing
        Me.txtDesc.Text = ""
        Me.txtComment.Text = ""
        txtCode.MyReadOnly = False
        fndIssueCode.Value = ""
        chkTrading.Checked = False
        fndIssueCode.Visible = False
        dtpIssueDate.Visible = False
        txtBatchNo.Visible = True
        dtpBatchDate.Visible = True

    End Sub

    Function AllowToSave() As Boolean
        '===================Added by preeti Gupta==============
        If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
            dtpDate.Select()
            Return False
        End If
        '===========================================================
        If btnSave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_BATCH_ORDER where BO_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If

        If clsCommon.myLen(txtBatchNo.Value) <= 0 AndAlso chkTrading.Checked = False Then
            myMessages.blankValue("Batch Order Code")
            txtBatchNo.Focus()
            Return False
        End If

        If clsCommon.myLen(fndIssueCode.Value) <= 0 AndAlso chkTrading.Checked = True Then
            myMessages.blankValue("Issue Code")
            txtBatchNo.Focus()
            Return False
        End If

        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue("Location Code")
            txtLocation.Focus()
            Return False
        End If
        '' CHECK FOR AVAILBLE STOCK ON THE SELECTED LOCATION/STORE
        'Dim ProdItemsQty As Decimal = 0
        'For Each dr As GridViewRowInfo In gv1.Rows
        '    ProdItemsQty = ProdItemsQty + clsCommon.myCdbl(dr.Cells(colReceiptQty).Value) + clsCommon.myCdbl(dr.Cells(colBreakageQty).Value)
        'Next


        'If clsProductionReceipt.checkStock(IIf(MOActive = True, "MO", "BO"), Me.txtBatchNo.Value, ProdItemsQty, clsCommon.GetPrintDate(Me.dtpDate.Value, "dd/MMM/yyyy"), Me.txtLocation.Value, Me.txtCode.Value) = False Then
        '    'clsCommon.MyMessageBoxShow("Insufficient Raw Material on selected location " & Me.txtLocation.Value & ".")

        '    Return False
        'End If
        If clsCommon.myLen(txtReceivedBy.Value) <= 0 Then
            myMessages.blankValue("Received By")
            txtReceivedBy.Focus()
            Return False
        End If

        If Me.gv1.Rows.Count = 0 Then
            myMessages.blankValue("List is Empty")
            Return False
        End If
        Dim ii As Int16 = 0

        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCBool(grow.Cells(colCheck).Value) = True Then

                If ((clsCommon.myCdbl(grow.Cells(colReceiptQty).Value) + clsCommon.myCdbl(grow.Cells(colBreakageQty).Value) + clsCommon.myCdbl(grow.Cells(colRejQty).Value)) > (IIf(MOActive = True, clsCommon.myCdbl(grow.Cells(colBatchQty).Value), clsCommon.myCdbl(grow.Cells(colBatchQty).Value)))) AndAlso chkTrading.Checked = False Then
                    clsCommon.MyMessageBoxShow("Entered Quantities are not valid in Line NO- " & clsCommon.myCstr(grow.Cells(colLineNo).Value & "!"))
                    Return False
                ElseIf ((clsCommon.myCdbl(grow.Cells(colReceiptQty).Value) + clsCommon.myCdbl(grow.Cells(colBreakageQty).Value) + clsCommon.myCdbl(grow.Cells(colRejQty).Value)) > clsCommon.myCdbl(grow.Cells(colBalanceIssueQty).Value)) AndAlso chkTrading.Checked = True Then
                    clsCommon.MyMessageBoxShow("Entered [Receipt Qty] +[Breakage Qty] + [Reject Qty] not greater then [Balance Issue Qty] valid in Line NO- " & clsCommon.myCstr(grow.Cells(colLineNo).Value & "!"))
                    Return False
                ElseIf (clsCommon.myCdbl(grow.Cells(colReceiptQty).Value) + clsCommon.myCdbl(grow.Cells(colBreakageQty).Value) + clsCommon.myCdbl(grow.Cells(colRejQty).Value)) = 0 Then
                    clsCommon.MyMessageBoxShow("Please Enter Qty in Line NO- " & clsCommon.myCstr(grow.Cells(colLineNo).Value & "!"))
                    Return False
                    'ElseIf clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAttachmentpath).Value)) <= 0 AndAlso chkTrading.Checked = False Then
                    '    clsCommon.MyMessageBoxShow("Attachment can not be blank Line NO- " & clsCommon.myCstr(grow.Cells(colLineNo).Value & "!"))
                    '    Return False
                ElseIf clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLabTesting).Value)) <= 0 AndAlso chkTrading.Checked = True Then
                    clsCommon.MyMessageBoxShow("Lab Testing can not be blank Line NO- " & clsCommon.myCstr(grow.Cells(colLineNo).Value & "!"))
                    Return False
                Else
                    ii += 1
                End If
                '' 05-Oct-2015 BM00000008094    
                Dim dblQty As Double = clsCommon.myCdbl(grow.Cells(colReceiptQty).Value)
                Dim strICode As String = clsCommon.myCstr(grow.Cells(colItemCode).Value)

                If clsCommon.myCBool(grow.Cells(colIsSerialseItem).Value) Then
                    Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                    If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                        common.clsCommon.MyMessageBoxShow("Please provice serial no for item : " + strICode + " . at line no." + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If

            End If

        Next
        If ii = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Item.", Me.Text)
            Return False
        End If
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Function SaveData(ByVal ChekBtnPost As Boolean) As Boolean
        If AllowToSave() Then
            Dim obj As New clsProductionReceipt
            obj.RECEIPT_CODE = Me.txtCode.Value
            obj.DESCRIPTION = Me.txtDesc.Text
            obj.RECEIPT_DATE = Me.dtpDate.Value
            obj.BO_CODE = Me.txtBatchNo.Value
            obj.RECEIVED_BY = clsCommon.myCstr(Me.txtReceivedBy.Value)
            obj.LOCATION_CODE = clsCommon.myCstr(Me.txtLocation.Value)
            obj.COMMENTS = Me.txtComment.Text
            If MOActive = True Then
                obj.TR_TYPE = "MO"
            Else
                obj.TR_TYPE = "BO"
            End If
            If obj.TR_TYPE = "BO" Then
                obj.BO_CODE = clsCommon.myCstr(txtBatchNo.Value)
                obj.MO_CODE = ""
            Else
                obj.BO_CODE = ""
                obj.MO_CODE = clsCommon.myCstr(txtBatchNo.Value)
            End If
            obj.IS_TRADING = chkTrading.Checked
            If obj.IS_TRADING = True Then
                obj.ISSUE_CODE = fndIssueCode.Value
                obj.ISSUE_DATE = dtpIssueDate.Value
            Else
                obj.BATCH_DATE = Me.dtpBatchDate.Value
            End If


            Dim obj1 As clsProductionReceipt
            objList = New List(Of clsProductionReceipt)
            Dim objListAttachment As New List(Of clsProductionReceiptAttachment)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 AndAlso clsCommon.myCBool(grow.Cells(colCheck).Value) = True Then

                    If clsCommon.myLen(grow.Cells(colAttachmentpath).Value) > 0 And clsCommon.myCstr(grow.Cells(colAttachmentpath).Value).ToString.Contains("\") Then '
                        Dim objAttachment As clsProductionReceiptAttachment

                        objAttachment = New clsProductionReceiptAttachment
                        objAttachment.Transaction_ID = txtCode.Value
                        'Prod_Plan_Code,BOM_CODE,ITEM_CODE
                        objAttachment.Prod_Plan_Code = clsCommon.myCstr(grow.Cells(colPPCode).Value)
                        objAttachment.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                        objAttachment.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objAttachment.Form_ID = Form_ID
                        objAttachment.ColCOMMENTS = ""
                        objAttachment.ColFileName = clsCommon.myCstr(grow.Cells(colAttachmentpath).Value.ToString.Substring(grow.Cells(colAttachmentpath).Value.ToString.LastIndexOf("\") + 1, grow.Cells(colAttachmentpath).Value.ToString.Length - grow.Cells(colAttachmentpath).Value.ToString.LastIndexOf("\") - 1))
                        objAttachment.ColFormId = Form_ID
                        objAttachment.ColPath = clsCommon.myCstr(grow.Cells(colAttachmentpath).Value) 'IIf(clsCommon.myLen(grow.Cells(col_Attachment_ID).Value) <= 0, clsCommon.myCstr(grow.Cells(colVLC_Procurement_Data_MP).Value), "")
                        objAttachment.ColTransactionId = txtCode.Value
                        objListAttachment.Add(objAttachment)
                    End If



                    obj1 = New clsProductionReceipt()

                    obj1.TR_TYPE = obj.TR_TYPE
                    obj1.RECEIPT_CODE = txtCode.Value
                    obj1.PROD_PLAN_CODE = clsCommon.myCstr(grow.Cells(colPPCode).Value)
                    obj1.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colProdLineCode).Value)
                    obj1.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)

                    obj1.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                    obj1.BATCH_QTY = clsCommon.myCdbl(grow.Cells(colBatchQty).Value)
                    obj1.BALANCE_BATCH_QTY = clsCommon.myCdbl(grow.Cells(colBalanceBatchQty).Value)

                    obj1.ISSUE_QTY = clsCommon.myCdbl(grow.Cells(colIssueQty).Value)
                    obj1.BALANCE_ISSUE_QTY = clsCommon.myCdbl(grow.Cells(colBalanceIssueQty).Value)

                    obj1.RECEIPT_QTY = clsCommon.myCdbl(grow.Cells(colReceiptQty).Value)
                    obj1.REJ_HEAD = clsCommon.myCstr(grow.Cells(colRejHead).Value)
                    obj1.REJ_QTY = clsCommon.myCdbl(grow.Cells(colRejQty).Value)
                    obj1.BREAKAGE_HEAD = clsCommon.myCstr(grow.Cells(colBreakageHead).Value)
                    obj1.BREAKAGE_QTY = clsCommon.myCdbl(grow.Cells(colBreakageQty).Value)
                    obj1.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    obj1.LAB_TESTING = clsCommon.myCstr(grow.Cells(colLabTesting).Value)

                    obj1.MFG_DATE = clsCommon.myCDate(grow.Cells(colMfgDate).Value)
                    obj1.EXP_DATE = clsCommon.myCDate(grow.Cells(colExpDate).Value)

                    If clsCommon.myLen(grow.Cells(colStartTime).Value) > 0 Then
                        obj1.START_TIME = clsCommon.myCDate(grow.Cells(colStartTime).Value)
                    Else
                        obj1.START_TIME = Nothing
                    End If

                    If clsCommon.myLen(grow.Cells(colEndTime).Value) > 0 Then
                        obj1.END_TIME = clsCommon.myCDate(grow.Cells(colEndTime).Value)
                    Else
                        obj1.END_TIME = Nothing
                    End If
                    '' serialize inventory
                    obj1.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                    objList.Add(obj1)

                End If
            Next

            Dim issaved As Boolean = False
            issaved = obj.SaveData(obj, objList, isNewEntry, objListAttachment, clsCommon.myCstr(txtCode.Value))

            If issaved = True Then
                If ChekBtnPost = False Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
                LoadData(obj.RECEIPT_CODE, NavigatorType.Current)
                Return True
            End If


        End If
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        obj = clsProductionReceipt.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso (clsCommon.myLen(obj.BO_CODE) > 0 Or clsCommon.myLen(obj.MO_CODE) > 0 Or clsCommon.myLen(obj.ISSUE_CODE) > 0)) Then
            If obj.TR_TYPE = "MO" Then
                MOActive = True
            Else
                MOActive = False
            End If
            isNewEntry = False
            btnSave.Text = "Update"
            If obj.POSTED Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            LoadBlankGrid()
            If obj.IS_TRADING = True Then
                Me.fndIssueCode.Value = obj.ISSUE_CODE
                dtpIssueDate.Value = obj.ISSUE_DATE
                chkTrading.Checked = obj.IS_TRADING
                lblBatchNo.Text = "Issue Code"
                lblBatchDate.Text = "Issue Date"
            End If
            txtCode.Value = obj.RECEIPT_CODE
            Me.txtDesc.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.txtComment.Text = clsCommon.myCstr(obj.COMMENTS)
            If obj.TR_TYPE = "BO" Then
                Me.txtBatchNo.Value = obj.BO_CODE
            Else
                Me.txtBatchNo.Value = obj.MO_CODE
            End If


            Me.txtReceivedBy.Value = clsCommon.myCstr(obj.RECEIVED_BY)
            Me.txtLocation.Value = obj.LOCATION_CODE
            Me.dtpDate.Value = obj.RECEIPT_DATE
            Me.dtpBatchDate.Value = obj.BATCH_DATE
            Me.lblLocation.Text = obj.LOCATION_NAME
            Me.lblEmpName.Text = obj.RECEIVED_BY_NAME
            If (clsProductionReceipt.ObjList IsNot Nothing AndAlso clsProductionReceipt.ObjList.Count > 0) Then
                For Each obj As clsProductionReceipt In clsProductionReceipt.ObjList
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Tag = obj.arrSrItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = Me.gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCheck).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPPCode).Value = clsCommon.myCstr(obj.PROD_PLAN_CODE)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colProdLineCode).Value = clsCommon.myCstr(obj.PRODUCTION_LINE_CODE)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(obj.BOM_CODE)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(obj.ITEM_CODE)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(obj.ITEM_DESCRIPTION)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchQty).Value = clsCommon.myCdbl(obj.BATCH_QTY)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceBatchQty).Value = clsCommon.myCdbl(obj.BALANCE_BATCH_QTY)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIssueQty).Value = clsCommon.myCdbl(obj.ISSUE_QTY)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceIssueQty).Value = clsCommon.myCdbl(obj.BALANCE_ISSUE_QTY)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReceiptQty).Value = clsCommon.myCdbl(obj.RECEIPT_QTY)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejHead).Value = clsCommon.myCstr(obj.REJ_HEAD)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejQty).Value = clsCommon.myCdbl(obj.REJ_QTY)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBreakageHead).Value = clsCommon.myCstr(obj.BREAKAGE_HEAD)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBreakageQty).Value = clsCommon.myCdbl(obj.BREAKAGE_QTY)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLabTesting).Value = obj.LAB_TESTING

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj.UNIT_CODE

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colStartTime).Value = obj.START_TIME
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEndTime).Value = obj.END_TIME

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMfgDate).Value = obj.MFG_DATE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colExpDate).Value = obj.EXP_DATE

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCost_Method).Value = obj.Costing_Method
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFIFO_Cost).Value = obj.FIFO_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLIFO_Cost).Value = obj.LIFO_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAVG_Cost).Value = obj.AVG_Cost

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAttachmentId).Value = obj.Attachment
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShowAttachment).Value = "show Attachment"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAttachment).Value = "Add Attachment"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAttachmentpath).Value = obj.FileName


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.ITEM_CODE)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(obj.ITEM_CODE)
                Next
            Else
                'gv1.Rows.AddNew()
            End If
        End If

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
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try

            If (myMessages.postConfirm()) Then
                SaveData(True)
                trans = clsDBFuncationality.GetTransactin

                clsProductionRM.UpdateInventoryMovement(Me.txtCode.Value, trans)
                clsProductionReceipt.PostData(txtCode.Value, True, trans)
                'SaveDataProductionEntry(trans)

                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub

    Private Function SaveDataProductionEntry(ByVal trans As System.Data.SqlClient.SqlTransaction) As Boolean
        Try
            'If (AllowToSave()) Then
            Dim obj As New ClsAdjustments()
            obj.Adjustment_No = ""
            obj.Adjustment_Date = dtpDate.Value
            'obj.Posting_Date
            obj.Reference = txtComment.Text
            obj.Description = txtDesc.Text
            'obj.Posted()
            obj.EMP_CODE = clsCommon.myCstr(txtReceivedBy.Value)
            obj.EMP_NAME = lblEmpName.Text
            obj.Unit_Code = "ALL"
            obj.ItemType = "FM"
            obj.Loc_Code = txtLocation.Value
            obj.Loc_Desc = lblLocation.Text
            obj.Trans_Type = "In"
            obj.Stock_Type = "P"
            obj.Reference_Document = strCostTransaction
            obj.Document_No = txtCode.Value
            obj.Arr = New List(Of ClsAdjustmentsDetails)()

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New ClsAdjustmentsDetails()
                'objTr.Adjustment_No=
                objTr.Adjustment_Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.Item_Description = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                objTr.Adjustment_Type = "BI"
                'objTr.Location_Code=Pick in SaveData from header
                objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colReceiptQty).Value)
                If grow.Cells(colCost_Method).Value = 0 Then
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colFIFO_Cost).Value)
                ElseIf grow.Cells(colCost_Method).Value = 1 Then
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colLIFO_Cost).Value)
                ElseIf grow.Cells(colCost_Method).Value = 2 Then
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colAVG_Cost).Value)
                Else
                    objTr.Item_Cost = 0
                End If

                objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                'objTr.Account_Code= Pick in SaveData
                'objTr.Account_Description=Pick in SaveData
                objTr.Remarks = clsCommon.myCstr(grow.Cells(colLabTesting).Value)
                objTr.Comments = clsCommon.myCstr(grow.Cells(colLabTesting).Value)
                objTr.mrp = objTr.Item_Cost ''Math.Max(clsCommon.myCdbl(grow.Cells(colFIFO_Cost).Value), Math.Max(clsCommon.myCdbl(grow.Cells(colLIFO_Cost).Value), clsCommon.myCdbl(grow.Cells(colAVG_Cost).Value))) 

                objTr.BreakageType = clsCommon.myCstr(grow.Cells(colBreakageHead).Value)
                objTr.Breakage = clsCommon.myCdbl(grow.Cells(colBreakageQty).Value)
                objTr.Breakage_Cost = (objTr.Breakage * objTr.Item_Cost) / IIf(objTr.Item_Quantity = 0, 1, objTr.Item_Quantity) ''clsCommon.myCdbl(grow.Cells(colBreakCost).Value)
                objTr.LeakageQty = 0 ''clsCommon.myCdbl(grow.Cells(colLeakQty).Value)

                objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colMfgDate).Value)
                objTr.Batch_No = Me.txtBatchNo.Value ''clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpDate).Value)

                objTr.ItemType = "FM" ''clsCommon.myCstr(cboItemType.SelectedValue)
                obj.ItemType = obj.ItemType
                objTr.Basic_Price = clsItemBasicPrice.GetBasicPrice(objTr.Item_Code, objTr.mrp, trans)
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                Throw New Exception("Please Fill at list one Item")
            End If

            Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)
            Dim strq As String = ""
            strq = "select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where document_no='" & Me.txtCode.Value & "' and reference_document='" & strCostTransaction & "' "
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strq, trans)
            If dt.Rows.Count > 0 Then
                ClsAdjustments.PostData(dt.Rows(0).Item("Adjustment_No"), strCostTransaction, trans)
            Else
                clsCommon.MyMessageBoxShow("Document not found !")
                Exit Function
            End If

            'clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
            'LoadData(obj.Adjustment_No, NavigatorType.Current)
            'Return isSaved
            Return True
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False

        End Try
        'Return False
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
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
                If (clsProductionReceipt.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function


    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim cond As String
        If MOActive Then
            cond = " TR_TYPE='MO'"
        Else
            cond = " TR_TYPE='BO'"
        End If
        Dim str As String = "select count(*) from TSPL_MF_RECEIPT where RECEIPT_CODE ='" + txtCode.Value + "' AND " & cond & " "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.RECEIPT_CODE AS Code,T1.DESCRIPTION,T1.RECEIPT_DATE, "
            qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE FROM TSPL_MF_RECEIPT AS T1"

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_RECEIPT", qry, "Code", cond, txtCode.Value, "Code", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmProductionReceipt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then

            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'If txtCode.Value = "" Then
        '    myMessages.blankValue("Requisition Number")
        'Else
        '    funPrint()
        'End If
    End Sub

    Private Sub funPrint()
        'Try
        '    Dim no As Integer = 0
        '    Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id ,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as Requisition_Date , " & _
        '    "convert(varchar,TSPL_REQUISITION_HEAD.Expire_Date,103) as Expire_Date ,convert(varchar,TSPL_REQUISITION_HEAD.Require_Date,103) as Require_Date , " & _
        '    "TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By , " & _
        '    "TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc,TSPL_REQUISITION_DETAIL.Specification, " & _
        '    "TSPL_REQUISITION_DETAIL.Remarks as DRemarks ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty, " & _
        '    "(select SUM( case when InOut='I' then Qty else  -1* Qty end )from TSPL_INVENTORY_MOVEMENT where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code and TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_REQUISITION_HEAD.Location) as AvaiQty  , " & _
        '    "TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img , " & _
        '    "TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,'' as AuthorizeBy ,TSPL_REQUISITION_HEAD.Request_By, " & _
        '    "TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location , " & _
        '    "TSPL_COMPANY_MASTER.Add1,case when  is_internal ='Y' then 'MATERIAL REQUISITION' else 'PURCHASE INDENT' END AS Heading  " & _
        '    "from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code   where(2 = 2)"
        '    If txtReceiptCode.Value <> "" Then
        '        qry += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReceiptCode.Value + "'"
        '    End If
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        If dt.Rows(i)("vendor_name").ToString() <> "" Then
        '            no = no + 1
        '        End If
        '    Next
        '    If no = 0 Then
        '        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisitionWithoutVendor-G", "Purchase Requisition")
        '        Else
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisitionWithoutVendor", "Purchase Requisition")
        '        End If

        '    Else
        '        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisition-G", "Purchase Requisition")
        '        Else
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisition", "Purchase Requisition")
        '        End If
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub




    Private Sub txtReceivedBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReceivedBy._MYValidating
        Try
            Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtReceivedBy.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
                txtReceivedBy.Value = obj.EMP_CODE
                lblEmpName.Text = obj.Emp_Name
            Else
                txtReceivedBy.Value = ""
                lblEmpName.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub




    Private Sub txtBatchNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBatchNo._MYValidating
        Try
            Dim qry As String = ""
            Dim WhrCls As String = ""
            Dim qryDate As String = ""
            If MOActive = True Then
                qry = " Select MO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),MO_DATE,103) AS [Date], POSTED AS [Is Posted], convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By],'MO' as Type from TSPL_MF_MANUFACTURING_ORDER "
                WhrCls = " POSTED=1 and MO_STATUS NOT IN ('Closed')"

            Else
                'qry = "SELECT  T1.BO_CODE AS CODE,T1.DESCRIPTION AS NAME,T1.BO_DATE FROM TSPL_MF_BATCH_ORDER T1 LEFT JOIN TSPL_MF_RECEIPT T2 ON T1.BO_CODE=T2.BO_CODE "
                qry = "SELECT distinct  T1.BO_CODE AS CODE,T1.DESCRIPTION AS NAME,T1.BO_DATE FROM TSPL_MF_BATCH_ORDER T1 LEFT JOIN TSPL_MF_RECEIPT T2 ON T1.BO_CODE=T2.BO_CODE left outer join TSPL_MF_BATCH_PP_DETAIL on T1.BO_CODE=TSPL_MF_BATCH_PP_DETAIL.BO_CODE left outer join (select max(BATCH_QTY) -( sum(RECEIPT_QTY) +sum(REJ_QTY)+sum(BREAKAGE_QTY)) as Balanc_Qty ,PROD_PLAN_CODE,BOM_CODE,ITEM_CODE  from TSPL_MF_RECEIPT_DETAIL     group by  PROD_PLAN_CODE,BOM_CODE,ITEM_CODE ) TBL_BATCH on TBL_BATCH.BOM_CODE = TSPL_MF_BATCH_PP_DETAIL.BOM_CODE  and TBL_BATCH.ITEM_CODE = TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE and TBL_BATCH.PROD_PLAN_CODE =TSPL_MF_BATCH_PP_DETAIL.PROD_PLAN_CODE " 'where BATCH_QTY -( RECEIPT_QTY +REJ_QTY+BREAKAGE_QTY) > 0
                WhrCls = " T1.POSTED=1 and  TBL_BATCH.BOM_CODE IS NULL or  (isnull (TBL_BATCH.Balanc_Qty ,0) > 0 and  TBL_BATCH.BOM_CODE IS not  NULL)   "  'AND T2.BO_CODE IS NULL

            End If

            txtBatchNo.Value = clsCommon.ShowSelectForm("Batch", qry, "Code", WhrCls, txtBatchNo.Value, "Code", isButtonClicked)
            If MOActive = True Then
                qryDate = "select MO_DATE from TSPL_MF_MANUFACTURING_ORDER where MO_CODE='" + txtBatchNo.Value + "'"
            Else
                qryDate = "select convert (varchar, BO_DATE,103) from TSPL_MF_BATCH_ORDER where BO_CODE='" + txtBatchNo.Value + "'"
            End If

            dtpBatchDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(qryDate))
            ShowBatchItems()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub ShowBatchItems()
        If isNewEntry = False Then
            Exit Sub
        End If
        Dim strq As String
        strq = ""
        If MOActive Then
            strq = " SELECT Cast(1 as BIT) as 'Check', '' as PROD_PLAN_CODE,TSPL_MF_MANUFACTURING_ORDER.PRODUCTION_AREA as PRODUCTION_LINE_CODE,TSPL_MF_MANUFACTURING_ORDER.BOM_CODE," &
                   " TSPL_MF_MANUFACTURING_ORDER.BOM_REVISION_NO,TSPL_MF_MANUFACTURING_ORDER.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc as ITEM_DESCRIPTION, " &
                   " TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK as BATCH_QTY,TSPL_MF_MANUFACTURING_ORDER.UNIT_CODE,TSPL_MF_MANUFACTURING_ORDER.PLANNED_START_DATE as START_TIME, " &
                   " TSPL_MF_MANUFACTURING_ORDER.PLANNED_END_DATE AS END_TIME,'' as PLAN_FOR_DATE,TSPL_MF_MANUFACTURING_ORDER.MO_DUE_DATE as EXP_DATE, " &
                   " TSPL_MF_MANUFACTURING_ORDER.ACTUAL_START_DATE AS MF_DATE,TSPL_ITEM_MASTER.Is_Serial_Item,TSPL_ITEM_MASTER.Is_Pick_Auto_SrNo " &
                   " FROM TSPL_MF_MANUFACTURING_ORDER inner join TSPL_ITEM_MASTER on TSPL_MF_MANUFACTURING_ORDER.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE " &
                   " WHERE TSPL_MF_MANUFACTURING_ORDER.MO_CODE='" & clsCommon.myCstr(Me.txtBatchNo.Value) & "' "
        Else
            strq += " SELECT Cast(1 as BIT) as 'Check', T1.PROD_PLAN_CODE,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T1.BOM_REVISION_NO,T1.ITEM_CODE,T1.ITEM_DESCRIPTION, "
            strq += " T1.BATCH_QTY,T1.BATCH_QTY - isnull (XFinal.Total_Qty,0) as  'Balance_Batch_Qty',T1.UNIT_CODE,T1.START_TIME,T1.END_TIME,T3.PLAN_FOR_DATE,T1.EXP_DATE,T1.MF_DATE,T1.EXP_DATE,TSPL_ITEM_MASTER.Is_Serial_Item,TSPL_ITEM_MASTER.Is_Pick_Auto_SrNo FROM TSPL_MF_BATCH_PP_DETAIL T1 INNER JOIN TSPL_MF_BATCH_ORDER T2 ON T1.BO_CODE=T2.BO_CODE inner join TSPL_ITEM_MASTER on T1.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE "
            strq += " LEFT JOIN TSPL_MF_PRODUCTION_PLAN_HEAD T3 ON T1.PROD_PLAN_CODE=T3.PROD_PLAN_CODE "
            strq += " left outer Join (Select sum (RECEIPT_QTY) + sum(REJ_QTY) +sum( BREAKAGE_QTY) as Total_Qty, PROD_PLAN_Code, BOM_Code, ITEM_CODE  from TSPL_MF_RECEIPT_DETAIL left outer join TSPL_MF_RECEIPT on TSPL_MF_RECEIPT.Receipt_Code = TSPL_MF_RECEIPT_DETAIL.Receipt_Code where TSPL_MF_RECEIPT.BO_CODE='" & clsCommon.myCstr(Me.txtBatchNo.Value) & "' group by  PROD_PLAN_Code, BOM_Code, ITEM_CODE ) XFinal on XFinal.ITEM_CODE = T1.ITEM_CODE and XFinal.BOM_CODE =T1.BOM_CODE and XFinal.PROD_PLAN_CODE = T1.PROD_PLAN_CODE"
            strq += " WHERE T1.BO_CODE='" & clsCommon.myCstr(Me.txtBatchNo.Value) & "' and T1.BATCH_QTY > isnull (XFinal.Total_Qty,0) "
        End If

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        Me.gv1.Rows.Clear()
        For intloop As Integer = 0 To dt.Rows.Count - 1
            Me.gv1.Rows.AddNew()
            Me.gv1.Rows(intloop).Cells(colLineNo).Value = intloop + 1
            Me.gv1.Rows(intloop).Cells(colCheck).Value = dt.Rows(intloop).Item("Check")
            Me.gv1.Rows(intloop).Cells(colPPCode).Value = dt.Rows(intloop).Item("PROD_PLAN_CODE")
            Me.gv1.Rows(intloop).Cells(colProdLineCode).Value = dt.Rows(intloop).Item("PRODUCTION_LINE_CODE")
            Me.gv1.Rows(intloop).Cells(colBOMCode).Value = dt.Rows(intloop).Item("BOM_CODE")

            Me.gv1.Rows(intloop).Cells(colItemCode).Value = dt.Rows(intloop).Item("ITEM_CODE")
            Me.gv1.Rows(intloop).Cells(colItemDesc).Value = dt.Rows(intloop).Item("ITEM_DESCRIPTION")
            Me.gv1.Rows(intloop).Cells(colBatchQty).Value = dt.Rows(intloop).Item("BATCH_QTY")

            If MOActive = False Then
                Me.gv1.Rows(intloop).Cells(colBalanceBatchQty).Value = dt.Rows(intloop).Item("Balance_Batch_Qty")
            End If

            Me.gv1.Rows(intloop).Cells(colUOM).Value = dt.Rows(intloop).Item("UNIT_CODE")
            Me.gv1.Rows(intloop).Cells(colIsPickAutoSrNo).Value = dt.Rows(intloop).Item("Is_Pick_Auto_SrNo")
            Me.gv1.Rows(intloop).Cells(colIsSerialseItem).Value = dt.Rows(intloop).Item("Is_Serial_Item")

            If clsCommon.myLen(dt.Rows(intloop).Item("MF_DATE")) > 0 Then
                Me.gv1.Rows(intloop).Cells(colMfgDate).Value = dt.Rows(intloop).Item("MF_DATE")
            End If
            If clsCommon.myLen(dt.Rows(intloop).Item("EXP_DATE")) > 0 Then
                Me.gv1.Rows(intloop).Cells(colExpDate).Value = dt.Rows(intloop).Item("EXP_DATE")
            End If
            If MOActive Then
                'If clsCommon.myLen(dt.Rows(intloop).Item("START_TIME")) > 0 Then
                '    Me.gv1.Rows(intloop).Cells(colStartTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("START_TIME"))
                'End If
            Else
                If clsCommon.myLen(dt.Rows(intloop).Item("START_TIME")) > 0 Then
                    Me.gv1.Rows(intloop).Cells(colStartTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("PLAN_FOR_DATE")).Add(dt.Rows(intloop).Item("START_TIME"))
                End If
            End If
            If MOActive Then
                'If clsCommon.myLen(dt.Rows(intloop).Item("END_TIME")) > 0 Then
                '    Me.gv1.Rows(intloop).Cells(colEndTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("END_TIME"))
                'End If
            Else
                If clsCommon.myLen(dt.Rows(intloop).Item("END_TIME")) > 0 Then
                    Me.gv1.Rows(intloop).Cells(colEndTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("PLAN_FOR_DATE")).Add(dt.Rows(intloop).Item("END_TIME"))
                End If
            End If



        Next
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funReset()

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If (e.Column Is gv1.Columns(colReceiptQty) AndAlso Not clsCommon.myCBool(e.Column Is gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value)) Then
            OpenSerialItem()
        End If
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
    End Sub

    Private Sub gv1_CommandCellClick(sender As Object, e As EventArgs) Handles gv1.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If gv1.CurrentColumn Is gv1.Columns(colShowAttachment) Then
                    Dim objAttachment As New ucAttachment
                    objAttachment.FunShow(gv1.CurrentRow.Cells(colAttachmentId).Value)
                End If
                isInsideLoadData = False
            End If
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                Dim isPosted As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_MF_RECEIPT where RECEIPT_CODE = '" + txtCode.Value + "' and POSTED = 1"))
                If gv1.CurrentColumn Is gv1.Columns(ColDelete) AndAlso isPosted = False Then
                    Dim objAttachment As New ucAttachment
                    Dim qry As String = "update TSPL_MF_RECEIPT_DETAIL set Attachment ='' where Prod_Plan_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colPPCode).Value) + "' and BOM_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colBOMCode).Value) + "' and ITEM_CODE ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "' and Receipt_Code = '" + txtCode.Value + "'  "
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    If clsCommon.myCstr(gv1.CurrentRow.Cells(colAttachmentpath).Value).ToString.Contains("\") Then
                        clsCommon.MyMessageBoxShow("Document Deleted")
                    Else
                        objAttachment.funDelete(gv1.CurrentRow.Cells(colAttachmentId).Value)
                    End If
                    gv1.CurrentRow.Cells(colAttachmentId).Value = ""
                    gv1.CurrentRow.Cells(colAttachmentpath).Value = ""


                End If
                isInsideLoadData = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellClick
        Try
            If e.Column Is gv1.Columns(colAttachment) Then
                Dim isPosted As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_MF_RECEIPT where RECEIPT_CODE = '" + txtCode.Value + "' and POSTED = 1"))
                If isPosted = False Then
                    openFileDialog1.FileName = ""
                    openFileDialog1.ShowDialog()

                    If openFileDialog1.FileName <> "" Then
                        gv1.CurrentRow.Cells(colAttachmentpath).Value = openFileDialog1.FileName

                    End If
                    'If clsCommon.myCBool(gv1.CurrentRow.Cells(colReceived).Value) = True And clsCommon.myCBool(gv1.CurrentRow.Cells(colJoiningMandatory).Value) = True Then
                    '    gv1.CurrentRow.Cells(colAttachmentpath).Value = openFileDialog1.FileName
                    'ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colReceived).Value) = False Then

                    '    clsCommon.MyMessageBoxShow("Please Check the Received CheckBox for'" + clsCommon.myCstr(gv1.CurrentRow.Cells(colJoiningCode).Value) + "'")
                    'End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub fndIssueCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndIssueCode._MYValidating
        Try
            Dim qry As String = ""
            Dim WhrCls As String = ""
            Dim qryDate As String = ""

            qry = " select TSPL_MF_ISSUE.ISSUE_CODE as Code , ISSUE_DATE as [Issue Date]  , ISSUED_BY as [Issue By] , ISSUED_TO as  [Issue To] , LOCATION_CODE as [To Locaion] , LOCATION_CODE_FROM  as [From Location], Case when isnull ( isTrading,0) > 0 then 'Yes' else 'No' end  Trading, Total_Qty as  [Total Balance Qty]  from TSPL_MF_ISSUE  left outer join ( (select  sum (QTY) as Total_Qty, ISSUE_CODE from (
                    Select sum (isnull(ISSUE_QTY,0)) as  QTY , TSPL_MF_ISSUE_DETAIL.ISSUE_CODE from TSPL_MF_ISSUE_DETAIL left outer join TSPL_MF_ISSUE on TSPL_MF_ISSUE_DETAIL .ISSUE_CODE =TSPL_MF_ISSUE.ISSUE_CODE  where  TSPL_MF_ISSUE.isTrading = 1 group by TSPL_MF_ISSUE_DETAIL.ISSUE_CODE 
                    union 
                    Select -1 * sum (isnull (RECEIPT_QTY,0)) + sum(isnull (REJ_QTY,0)) +sum( isnull (BREAKAGE_QTY,0)) as Qty , TSPL_MF_RECEIPT.ISSUE_CODE   from TSPL_MF_RECEIPT_DETAIL left outer join TSPL_MF_RECEIPT on TSPL_MF_RECEIPT.Receipt_Code = TSPL_MF_RECEIPT_DETAIL.Receipt_Code  where  TSPL_MF_RECEIPT.ISSUE_CODE is not null 
                    group by   TSPL_MF_RECEIPT.Issue_Code  
                    ) Final Group by Final.ISSUE_CODE) ) XFinal on XFinal.ISSUE_CODE = TSPL_MF_ISSUE.ISSUE_CODE  "
            WhrCls = " POSTED=1 and  isTrading = 1  and Total_Qty > 0"

            fndIssueCode.Value = clsCommon.ShowSelectForm("ISSUE@Finder@Receipt", qry, "Code", WhrCls, fndIssueCode.Value, "Code", isButtonClicked)

            qryDate = "select ISSUE_DATE from TSPL_MF_ISSUE where ISSUE_CODE='" + fndIssueCode.Value + "'"
            dtpIssueDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(qryDate))
            ShowIssueItems()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub ShowIssueItems()
        If isNewEntry = False Then
            Exit Sub
        End If
        Dim strq As String
        strq = " "
        'If MOActive Then
        strq = " SELECT Cast(1 as BIT) as 'Check', '' as PROD_PLAN_CODE,'' as PRODUCTION_LINE_CODE,TSPL_MF_ISSUE_DETAIL.ISSUE_CODE as BOM_CODE," &
               " '' as BOM_REVISION_NO,TSPL_MF_ISSUE_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc as ITEM_DESCRIPTION, " &
               " TSPL_MF_ISSUE_DETAIL.ISSUE_QTY as ISSUE_QTY,TSPL_MF_ISSUE_DETAIL.UNIT_CODE as UNIT_CODE,'' as START_TIME, " &
               " '' AS END_TIME,'' as PLAN_FOR_DATE,'' as EXP_DATE, " &
               " '' AS MF_DATE,TSPL_ITEM_MASTER.Is_Serial_Item,TSPL_ITEM_MASTER.Is_Pick_Auto_SrNo, TSPL_MF_ISSUE_DETAIL.Issue_Qty - isnull (XFinal.Total_Qty,0) as  'Balance_Issue_Qty'  " &
               " FROM TSPL_MF_ISSUE_DETAIL inner join TSPL_ITEM_MASTER on TSPL_MF_ISSUE_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE " &
               " left outer Join (Select sum (RECEIPT_QTY) + sum(REJ_QTY) +sum( BREAKAGE_QTY) as Total_Qty,  ISSUE_Code, ITEM_CODE  from TSPL_MF_RECEIPT_DETAIL left outer join TSPL_MF_RECEIPT on TSPL_MF_RECEIPT.Receipt_Code = TSPL_MF_RECEIPT_DETAIL.Receipt_Code where TSPL_MF_RECEIPT.Issue_Code='" & clsCommon.myCstr(Me.fndIssueCode.Value) & "' group by   Issue_Code, ITEM_CODE ) XFinal on XFinal.ITEM_CODE = TSPL_MF_ISSUE_DETAIL.ITEM_CODE and XFinal.Issue_CODE =TSPL_MF_ISSUE_DETAIL.Issue_CODE  " &
               " WHERE TSPL_MF_ISSUE_DETAIL.ISSUE_CODE='" & clsCommon.myCstr(Me.fndIssueCode.Value) & "' "
        'Else
        '    strq += " SELECT Cast(1 as BIT) as 'Check', T1.PROD_PLAN_CODE,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T1.BOM_REVISION_NO,T1.ITEM_CODE,T1.ITEM_DESCRIPTION, "
        '    strq += " T1.BATCH_QTY,T1.BATCH_QTY - isnull (XFinal.Total_Qty,0) as  'Balance_Batch_Qty',T1.UNIT_CODE,T1.START_TIME,T1.END_TIME,T3.PLAN_FOR_DATE,T1.EXP_DATE,T1.MF_DATE,T1.EXP_DATE,TSPL_ITEM_MASTER.Is_Serial_Item,TSPL_ITEM_MASTER.Is_Pick_Auto_SrNo FROM TSPL_MF_BATCH_PP_DETAIL T1 INNER JOIN TSPL_MF_BATCH_ORDER T2 ON T1.BO_CODE=T2.BO_CODE inner join TSPL_ITEM_MASTER on T1.ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE "
        '    strq += " LEFT JOIN TSPL_MF_PRODUCTION_PLAN_HEAD T3 ON T1.PROD_PLAN_CODE=T3.PROD_PLAN_CODE "
        '    strq += " left outer Join (Select sum (RECEIPT_QTY) + sum(REJ_QTY) +sum( BREAKAGE_QTY) as Total_Qty, PROD_PLAN_Code, BOM_Code, ITEM_CODE  from TSPL_MF_RECEIPT_DETAIL left outer join TSPL_MF_RECEIPT on TSPL_MF_RECEIPT.Receipt_Code = TSPL_MF_RECEIPT_DETAIL.Receipt_Code where TSPL_MF_RECEIPT.BO_CODE='" & clsCommon.myCstr(Me.txtBatchNo.Value) & "' group by  PROD_PLAN_Code, BOM_Code, ITEM_CODE ) XFinal on XFinal.ITEM_CODE = T1.ITEM_CODE and XFinal.BOM_CODE =T1.BOM_CODE and XFinal.PROD_PLAN_CODE = T1.PROD_PLAN_CODE"
        '    strq += " WHERE T1.BO_CODE='" & clsCommon.myCstr(Me.txtBatchNo.Value) & "' and T1.BATCH_QTY > isnull (XFinal.Total_Qty,0) "
        'End If


        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        Me.gv1.Rows.Clear()
        For intloop As Integer = 0 To dt.Rows.Count - 1
            Me.gv1.Rows.AddNew()
            Me.gv1.Rows(intloop).Cells(colLineNo).Value = intloop + 1
            Me.gv1.Rows(intloop).Cells(colCheck).Value = dt.Rows(intloop).Item("Check")
            Me.gv1.Rows(intloop).Cells(colPPCode).Value = dt.Rows(intloop).Item("PROD_PLAN_CODE")
            Me.gv1.Rows(intloop).Cells(colProdLineCode).Value = dt.Rows(intloop).Item("PRODUCTION_LINE_CODE")
            Me.gv1.Rows(intloop).Cells(colBOMCode).Value = "" 'dt.Rows(intloop).Item("BOM_CODE")

            Me.gv1.Rows(intloop).Cells(colItemCode).Value = dt.Rows(intloop).Item("ITEM_CODE")
            Me.gv1.Rows(intloop).Cells(colItemDesc).Value = dt.Rows(intloop).Item("ITEM_DESCRIPTION")
            Me.gv1.Rows(intloop).Cells(colBatchQty).Value = 0 'dt.Rows(intloop).Item("BATCH_QTY")
            Me.gv1.Rows(intloop).Cells(colBalanceBatchQty).Value = 0
            Me.gv1.Rows(intloop).Cells(colBalanceIssueQty).Value = dt.Rows(intloop).Item("Balance_Issue_Qty")
            '
            Me.gv1.Rows(intloop).Cells(colIssueQty).Value = dt.Rows(intloop).Item("ISSUE_QTY")
            Me.gv1.Rows(intloop).Cells(colUOM).Value = dt.Rows(intloop).Item("UNIT_CODE")
            Me.gv1.Rows(intloop).Cells(colIsPickAutoSrNo).Value = dt.Rows(intloop).Item("Is_Pick_Auto_SrNo")
            Me.gv1.Rows(intloop).Cells(colIsSerialseItem).Value = dt.Rows(intloop).Item("Is_Serial_Item")

            If clsCommon.myLen(dt.Rows(intloop).Item("MF_DATE")) > 0 Then
                Me.gv1.Rows(intloop).Cells(colMfgDate).Value = dt.Rows(intloop).Item("MF_DATE")
            End If
            If clsCommon.myLen(dt.Rows(intloop).Item("EXP_DATE")) > 0 Then
                Me.gv1.Rows(intloop).Cells(colExpDate).Value = dt.Rows(intloop).Item("EXP_DATE")
            End If
            If MOActive Then
                'If clsCommon.myLen(dt.Rows(intloop).Item("START_TIME")) > 0 Then
                '    Me.gv1.Rows(intloop).Cells(colStartTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("START_TIME"))
                'End If
            Else
                If clsCommon.myLen(dt.Rows(intloop).Item("START_TIME")) > 0 Then
                    Me.gv1.Rows(intloop).Cells(colStartTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("PLAN_FOR_DATE")).Add(dt.Rows(intloop).Item("START_TIME"))
                End If
            End If
            If MOActive Then
                'If clsCommon.myLen(dt.Rows(intloop).Item("END_TIME")) > 0 Then
                '    Me.gv1.Rows(intloop).Cells(colEndTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("END_TIME"))
                'End If
            Else
                If clsCommon.myLen(dt.Rows(intloop).Item("END_TIME")) > 0 Then
                    Me.gv1.Rows(intloop).Cells(colEndTime).Value = clsCommon.myCDate(dt.Rows(intloop).Item("PLAN_FOR_DATE")).Add(dt.Rows(intloop).Item("END_TIME"))
                End If
            End If



        Next
    End Sub

    Private Sub chkTrading_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkTrading.ToggleStateChanged
        If chkTrading.Checked = True Then
            txtBatchNo.Visible = False
            dtpBatchDate.Visible = False
            fndIssueCode.Visible = True
            dtpIssueDate.Visible = True
            lblBatchNo.Text = "Issue Code"
            lblBatchDate.Text = "Issue Date"
            LoadBlankGrid()
            gv1.Rows.Clear()
        Else
            txtBatchNo.Visible = True
            dtpBatchDate.Visible = True
            fndIssueCode.Visible = False
            dtpIssueDate.Visible = False
            lblBatchNo.Text = "Batch Code"
            lblBatchDate.Text = "Batch Date"
            LoadBlankGrid()
            gv1.Rows.Clear()
        End If
    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Private Sub btnUnpost_Click(sender As Object, e As EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    clsProductionReceipt.ReverseAndUnpost(txtCode.Value)
                    clsCommon.MyMessageBoxShow("Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class


