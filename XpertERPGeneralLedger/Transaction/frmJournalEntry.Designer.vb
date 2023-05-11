<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJournalEntry
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJournalEntry))
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim GridViewDecimalColumn4 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn5 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewDecimalColumn6 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn10 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDateTimeColumn2 As Telerik.WinControls.UI.GridViewDateTimeColumn = New Telerik.WinControls.UI.GridViewDateTimeColumn()
        Dim GridViewTextBoxColumn11 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn12 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim SortDescriptor2 As Telerik.WinControls.Data.SortDescriptor = New Telerik.WinControls.Data.SortDescriptor()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mItmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mItmExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportblank = New Telerik.WinControls.UI.RadMenuItem()
        Me.mItmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtBoxVoucher = New common.Controls.MyTextBox()
        Me.txtBoxSrcType = New common.Controls.MyTextBox()
        Me.txtBoxCr = New common.Controls.MyTextBox()
        Me.txtBoxDr = New common.Controls.MyTextBox()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndCode = New common.UserControls.txtFinder()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtCodeDesc = New common.Controls.MyTextBox()
        Me.rdbOther = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbVendor = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbCustomer = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkMonthly = New Telerik.WinControls.UI.RadCheckBox()
        Me.dtRevese = New common.Controls.MyDateTimePicker()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.chkReverse = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.btnProAuth = New Telerik.WinControls.UI.RadButton()
        Me.btnAuth = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndSrcType = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.txtTapalNo = New common.Controls.MyTextBox()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.txtDataAndTimeSelection = New common.Controls.MyDateTimePicker()
        Me.txtReverseVoucher = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox30 = New System.Windows.Forms.GroupBox()
        Me.txtGLAC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnInvSummaryUpdate = New Telerik.WinControls.UI.RadButton()
        Me.chkIndAS = New Telerik.WinControls.UI.RadCheckBox()
        Me.LblManAuto = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyTextBox()
        Me.txtUnbalAmt = New common.MyNumBox()
        Me.txtCr = New common.MyNumBox()
        Me.txtDr = New common.MyNumBox()
        Me.UsLock1 = New common.usLock()
        Me.dtSrc = New common.Controls.MyDateTimePicker()
        Me.dtVoucher = New common.Controls.MyDateTimePicker()
        Me.fndSrcCode = New common.UserControls.txtFinder()
        Me.fndVoucher = New common.UserControls.txtNavigator()
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.txtDocDesc = New common.Controls.MyTextBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtVoucherDesc = New common.Controls.MyTextBox()
        Me.txtComments = New common.Controls.MyTextBox()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtSrcDesc = New common.Controls.MyTextBox()
        Me.txtSrcDoc = New common.Controls.MyTextBox()
        Me.cmbType = New common.Controls.MyComboBox()
        Me.gdAcc1 = New common.UserControls.MyRadGridView()
        Me.btnUnpostTransaction = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.txtToExpDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtFromExpDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnSendToTally = New Telerik.WinControls.UI.RadButton()
        Me.butCostCenterAndHirerachy_Update_AfterPost = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtBoxVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBoxSrcType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBoxCr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBoxDr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRevese, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnProAuth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAuth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndSrcType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fndSrcType.SuspendLayout()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTapalNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDataAndTimeSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox30.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInvSummaryUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManAuto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUnbalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtSrc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVoucherDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSrcDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSrcDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdAcc1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdAcc1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpostTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage5.SuspendLayout()
        Me.Attachments.SuspendLayout()
        CType(Me.txtToExpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromExpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendToTally, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(1106, 20)
        Me.RadMenu2.TabIndex = 65
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mItmExport, Me.mItmExit, Me.btnExportblank, Me.mItmImport, Me.RadMenuItem4})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'mItmExport
        '
        Me.mItmExport.AccessibleDescription = "Export"
        Me.mItmExport.AccessibleName = "Export"
        Me.mItmExport.Name = "mItmExport"
        Me.mItmExport.Text = "Export"
        '
        'mItmExit
        '
        Me.mItmExit.AccessibleDescription = "Exit"
        Me.mItmExit.AccessibleName = "Exit"
        Me.mItmExit.Name = "mItmExit"
        Me.mItmExit.Text = "Exit"
        '
        'btnExportblank
        '
        Me.btnExportblank.AccessibleDescription = "Export blank sheet"
        Me.btnExportblank.AccessibleName = "Export blank sheet"
        Me.btnExportblank.Name = "btnExportblank"
        Me.btnExportblank.Text = "Export blank sheet"
        '
        'mItmImport
        '
        Me.mItmImport.AccessibleDescription = "Import"
        Me.mItmImport.AccessibleName = "Import"
        Me.mItmImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mItmImport.Name = "mItmImport"
        Me.mItmImport.Text = "Import Closing Entry"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Import Entry"
        Me.RadMenuItem4.AccessibleName = "Import Entry"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import Entry"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Exit"
        Me.RadMenuItem2.AccessibleName = "Exit"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Exit"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadLabel11
        '
        Me.RadLabel11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Location = New System.Drawing.Point(682, 351)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(41, 18)
        Me.RadLabel11.TabIndex = 18
        Me.RadLabel11.Text = "Credits"
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Location = New System.Drawing.Point(514, 350)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(38, 18)
        Me.RadLabel10.TabIndex = 16
        Me.RadLabel10.Text = "Debits"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(2, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(65, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(69, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(65, 24)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(1033, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 24)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(13, 92)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel9.TabIndex = 48
        Me.RadLabel9.Text = "Entry Type"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtBoxVoucher)
        Me.RadGroupBox4.Controls.Add(Me.txtBoxSrcType)
        Me.RadGroupBox4.Controls.Add(Me.txtBoxCr)
        Me.RadGroupBox4.Controls.Add(Me.txtBoxDr)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel16)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel15)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel14)
        Me.RadGroupBox4.Controls.Add(Me.RadLabel12)
        Me.RadGroupBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox4.HeaderText = "Summary"
        Me.RadGroupBox4.Location = New System.Drawing.Point(563, 36)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(226, 116)
        Me.RadGroupBox4.TabIndex = 5
        Me.RadGroupBox4.Text = "Summary"
        '
        'txtBoxVoucher
        '
        Me.txtBoxVoucher.CalculationExpression = Nothing
        Me.txtBoxVoucher.Enabled = False
        Me.txtBoxVoucher.FieldCode = Nothing
        Me.txtBoxVoucher.FieldDesc = Nothing
        Me.txtBoxVoucher.FieldMaxLength = 0
        Me.txtBoxVoucher.FieldName = Nothing
        Me.txtBoxVoucher.isCalculatedField = False
        Me.txtBoxVoucher.IsSourceFromTable = False
        Me.txtBoxVoucher.IsSourceFromValueList = False
        Me.txtBoxVoucher.IsUnique = False
        Me.txtBoxVoucher.Location = New System.Drawing.Point(89, 18)
        Me.txtBoxVoucher.MendatroryField = False
        Me.txtBoxVoucher.MyLinkLable1 = Nothing
        Me.txtBoxVoucher.MyLinkLable2 = Nothing
        Me.txtBoxVoucher.Name = "txtBoxVoucher"
        Me.txtBoxVoucher.ReadOnly = True
        Me.txtBoxVoucher.ReferenceFieldDesc = Nothing
        Me.txtBoxVoucher.ReferenceFieldName = Nothing
        Me.txtBoxVoucher.ReferenceTableName = Nothing
        Me.txtBoxVoucher.Size = New System.Drawing.Size(128, 20)
        Me.txtBoxVoucher.TabIndex = 0
        Me.txtBoxVoucher.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBoxSrcType
        '
        Me.txtBoxSrcType.CalculationExpression = Nothing
        Me.txtBoxSrcType.Enabled = False
        Me.txtBoxSrcType.FieldCode = Nothing
        Me.txtBoxSrcType.FieldDesc = Nothing
        Me.txtBoxSrcType.FieldMaxLength = 0
        Me.txtBoxSrcType.FieldName = Nothing
        Me.txtBoxSrcType.isCalculatedField = False
        Me.txtBoxSrcType.IsSourceFromTable = False
        Me.txtBoxSrcType.IsSourceFromValueList = False
        Me.txtBoxSrcType.IsUnique = False
        Me.txtBoxSrcType.Location = New System.Drawing.Point(89, 90)
        Me.txtBoxSrcType.MendatroryField = False
        Me.txtBoxSrcType.MyLinkLable1 = Nothing
        Me.txtBoxSrcType.MyLinkLable2 = Nothing
        Me.txtBoxSrcType.Name = "txtBoxSrcType"
        Me.txtBoxSrcType.ReadOnly = True
        Me.txtBoxSrcType.ReferenceFieldDesc = Nothing
        Me.txtBoxSrcType.ReferenceFieldName = Nothing
        Me.txtBoxSrcType.ReferenceTableName = Nothing
        Me.txtBoxSrcType.Size = New System.Drawing.Size(128, 20)
        Me.txtBoxSrcType.TabIndex = 3
        Me.txtBoxSrcType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBoxCr
        '
        Me.txtBoxCr.CalculationExpression = Nothing
        Me.txtBoxCr.Enabled = False
        Me.txtBoxCr.FieldCode = Nothing
        Me.txtBoxCr.FieldDesc = Nothing
        Me.txtBoxCr.FieldMaxLength = 0
        Me.txtBoxCr.FieldName = Nothing
        Me.txtBoxCr.isCalculatedField = False
        Me.txtBoxCr.IsSourceFromTable = False
        Me.txtBoxCr.IsSourceFromValueList = False
        Me.txtBoxCr.IsUnique = False
        Me.txtBoxCr.Location = New System.Drawing.Point(89, 66)
        Me.txtBoxCr.MendatroryField = False
        Me.txtBoxCr.MyLinkLable1 = Nothing
        Me.txtBoxCr.MyLinkLable2 = Nothing
        Me.txtBoxCr.Name = "txtBoxCr"
        Me.txtBoxCr.ReadOnly = True
        Me.txtBoxCr.ReferenceFieldDesc = Nothing
        Me.txtBoxCr.ReferenceFieldName = Nothing
        Me.txtBoxCr.ReferenceTableName = Nothing
        Me.txtBoxCr.Size = New System.Drawing.Size(128, 20)
        Me.txtBoxCr.TabIndex = 2
        Me.txtBoxCr.Text = "0.00"
        Me.txtBoxCr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBoxDr
        '
        Me.txtBoxDr.CalculationExpression = Nothing
        Me.txtBoxDr.Enabled = False
        Me.txtBoxDr.FieldCode = Nothing
        Me.txtBoxDr.FieldDesc = Nothing
        Me.txtBoxDr.FieldMaxLength = 0
        Me.txtBoxDr.FieldName = Nothing
        Me.txtBoxDr.isCalculatedField = False
        Me.txtBoxDr.IsSourceFromTable = False
        Me.txtBoxDr.IsSourceFromValueList = False
        Me.txtBoxDr.IsUnique = False
        Me.txtBoxDr.Location = New System.Drawing.Point(89, 42)
        Me.txtBoxDr.MendatroryField = False
        Me.txtBoxDr.MyLinkLable1 = Nothing
        Me.txtBoxDr.MyLinkLable2 = Nothing
        Me.txtBoxDr.Name = "txtBoxDr"
        Me.txtBoxDr.ReadOnly = True
        Me.txtBoxDr.ReferenceFieldDesc = Nothing
        Me.txtBoxDr.ReferenceFieldName = Nothing
        Me.txtBoxDr.ReferenceTableName = Nothing
        Me.txtBoxDr.Size = New System.Drawing.Size(128, 20)
        Me.txtBoxDr.TabIndex = 1
        Me.txtBoxDr.Text = "0.00"
        Me.txtBoxDr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(6, 92)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel16.TabIndex = 20
        Me.RadLabel16.Text = "Sources Type"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(6, 68)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(42, 16)
        Me.RadLabel15.TabIndex = 22
        Me.RadLabel15.Text = "Credits"
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(6, 44)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel14.TabIndex = 22
        Me.RadLabel14.Text = "Debits"
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(6, 20)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(69, 16)
        Me.RadLabel12.TabIndex = 17
        Me.RadLabel12.Text = "Voucher No."
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndCode)
        Me.RadGroupBox2.Controls.Add(Me.lblCode)
        Me.RadGroupBox2.Controls.Add(Me.txtCodeDesc)
        Me.RadGroupBox2.Controls.Add(Me.rdbOther)
        Me.RadGroupBox2.Controls.Add(Me.rdbVendor)
        Me.RadGroupBox2.Controls.Add(Me.rdbCustomer)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(116, 199)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(677, 30)
        Me.RadGroupBox2.TabIndex = 14
        '
        'fndCode
        '
        Me.fndCode.CalculationExpression = Nothing
        Me.fndCode.FieldCode = Nothing
        Me.fndCode.FieldDesc = Nothing
        Me.fndCode.FieldMaxLength = 0
        Me.fndCode.FieldName = Nothing
        Me.fndCode.isCalculatedField = False
        Me.fndCode.IsSourceFromTable = False
        Me.fndCode.IsSourceFromValueList = False
        Me.fndCode.IsUnique = False
        Me.fndCode.Location = New System.Drawing.Point(306, 5)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCode.MyLinkLable1 = Nothing
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyReadOnly = False
        Me.fndCode.MyShowMasterFormButton = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.ReferenceFieldDesc = Nothing
        Me.fndCode.ReferenceFieldName = Nothing
        Me.fndCode.ReferenceTableName = Nothing
        Me.fndCode.Size = New System.Drawing.Size(128, 19)
        Me.fndCode.TabIndex = 4
        Me.fndCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(266, 5)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 3
        Me.lblCode.Text = "Code"
        '
        'txtCodeDesc
        '
        Me.txtCodeDesc.CalculationExpression = Nothing
        Me.txtCodeDesc.FieldCode = Nothing
        Me.txtCodeDesc.FieldDesc = Nothing
        Me.txtCodeDesc.FieldMaxLength = 0
        Me.txtCodeDesc.FieldName = Nothing
        Me.txtCodeDesc.isCalculatedField = False
        Me.txtCodeDesc.IsSourceFromTable = False
        Me.txtCodeDesc.IsSourceFromValueList = False
        Me.txtCodeDesc.IsUnique = False
        Me.txtCodeDesc.Location = New System.Drawing.Point(448, 5)
        Me.txtCodeDesc.MendatroryField = False
        Me.txtCodeDesc.MyLinkLable1 = Nothing
        Me.txtCodeDesc.MyLinkLable2 = Nothing
        Me.txtCodeDesc.Name = "txtCodeDesc"
        Me.txtCodeDesc.ReadOnly = True
        Me.txtCodeDesc.ReferenceFieldDesc = Nothing
        Me.txtCodeDesc.ReferenceFieldName = Nothing
        Me.txtCodeDesc.ReferenceTableName = Nothing
        Me.txtCodeDesc.Size = New System.Drawing.Size(223, 20)
        Me.txtCodeDesc.TabIndex = 5
        Me.txtCodeDesc.TabStop = False
        '
        'rdbOther
        '
        Me.rdbOther.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbOther.Location = New System.Drawing.Point(162, 5)
        Me.rdbOther.Name = "rdbOther"
        Me.rdbOther.Size = New System.Drawing.Size(54, 16)
        Me.rdbOther.TabIndex = 2
        Me.rdbOther.Text = "Others"
        '
        'rdbVendor
        '
        Me.rdbVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbVendor.Location = New System.Drawing.Point(92, 5)
        Me.rdbVendor.Name = "rdbVendor"
        Me.rdbVendor.Size = New System.Drawing.Size(57, 16)
        Me.rdbVendor.TabIndex = 1
        Me.rdbVendor.Text = "Vendor"
        '
        'rdbCustomer
        '
        Me.rdbCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbCustomer.Location = New System.Drawing.Point(13, 5)
        Me.rdbCustomer.Name = "rdbCustomer"
        Me.rdbCustomer.Size = New System.Drawing.Size(69, 16)
        Me.rdbCustomer.TabIndex = 0
        Me.rdbCustomer.Text = "Customer"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkMonthly)
        Me.RadGroupBox1.Controls.Add(Me.dtRevese)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox1.Controls.Add(Me.chkReverse)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(116, 231)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(677, 27)
        Me.RadGroupBox1.TabIndex = 15
        '
        'chkMonthly
        '
        Me.chkMonthly.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMonthly.Location = New System.Drawing.Point(120, 4)
        Me.chkMonthly.Name = "chkMonthly"
        '
        '
        '
        Me.chkMonthly.RootElement.StretchHorizontally = True
        Me.chkMonthly.RootElement.StretchVertically = True
        Me.chkMonthly.Size = New System.Drawing.Size(89, 16)
        Me.chkMonthly.TabIndex = 3
        Me.chkMonthly.Text = "Monthly"
        '
        'dtRevese
        '
        Me.dtRevese.CalculationExpression = Nothing
        Me.dtRevese.CustomFormat = "dd/MM/yyyy"
        Me.dtRevese.FieldCode = Nothing
        Me.dtRevese.FieldDesc = Nothing
        Me.dtRevese.FieldMaxLength = 0
        Me.dtRevese.FieldName = Nothing
        Me.dtRevese.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtRevese.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRevese.isCalculatedField = False
        Me.dtRevese.IsSourceFromTable = False
        Me.dtRevese.IsSourceFromValueList = False
        Me.dtRevese.IsUnique = False
        Me.dtRevese.Location = New System.Drawing.Point(357, 4)
        Me.dtRevese.MendatroryField = False
        Me.dtRevese.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtRevese.MyLinkLable1 = Nothing
        Me.dtRevese.MyLinkLable2 = Nothing
        Me.dtRevese.Name = "dtRevese"
        Me.dtRevese.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtRevese.ReferenceFieldDesc = Nothing
        Me.dtRevese.ReferenceFieldName = Nothing
        Me.dtRevese.ReferenceTableName = Nothing
        Me.dtRevese.Size = New System.Drawing.Size(77, 18)
        Me.dtRevese.TabIndex = 2
        Me.dtRevese.TabStop = False
        Me.dtRevese.Text = "13/06/2011"
        Me.dtRevese.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(307, 5)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel8.TabIndex = 1
        Me.RadLabel8.Text = "Date"
        '
        'chkReverse
        '
        Me.chkReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReverse.Location = New System.Drawing.Point(10, 5)
        Me.chkReverse.Name = "chkReverse"
        '
        '
        '
        Me.chkReverse.RootElement.StretchHorizontally = True
        Me.chkReverse.RootElement.StretchVertically = True
        Me.chkReverse.Size = New System.Drawing.Size(89, 16)
        Me.chkReverse.TabIndex = 0
        Me.chkReverse.Text = "Auto Reverse"
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(13, 50)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(78, 16)
        Me.RadLabel7.TabIndex = 47
        Me.RadLabel7.Text = "Sources Code"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(13, 157)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel13.TabIndex = 45
        Me.RadLabel13.Text = "Remarks"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(13, 113)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(89, 16)
        Me.RadLabel6.TabIndex = 46
        Me.RadLabel6.Text = "Source Doc. No."
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(13, 135)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(94, 16)
        Me.RadLabel5.TabIndex = 44
        Me.RadLabel5.Text = "Sub Ledger Desc"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(13, 179)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel19.TabIndex = 46
        Me.RadLabel19.Text = "Comments"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(13, 29)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel4.TabIndex = 42
        Me.RadLabel4.Text = "Description"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(296, 113)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(85, 16)
        Me.RadLabel3.TabIndex = 9
        Me.RadLabel3.Text = "Document Date"
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(13, 206)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(70, 16)
        Me.RadLabel21.TabIndex = 45
        Me.RadLabel21.Text = "Source Type"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(385, 7)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel2.TabIndex = 2
        Me.RadLabel2.Text = "Voucher Date"
        '
        'btnProAuth
        '
        Me.btnProAuth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnProAuth.Location = New System.Drawing.Point(369, 6)
        Me.btnProAuth.Name = "btnProAuth"
        Me.btnProAuth.Size = New System.Drawing.Size(85, 24)
        Me.btnProAuth.TabIndex = 5
        Me.btnProAuth.Text = "Prov. Authorise"
        Me.btnProAuth.Visible = False
        '
        'btnAuth
        '
        Me.btnAuth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAuth.Location = New System.Drawing.Point(136, 6)
        Me.btnAuth.Name = "btnAuth"
        Me.btnAuth.Size = New System.Drawing.Size(62, 24)
        Me.btnAuth.TabIndex = 2
        Me.btnAuth.Text = "Post"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(13, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(69, 16)
        Me.RadLabel1.TabIndex = 8
        Me.RadLabel1.Text = "Voucher No."
        '
        'fndSrcType
        '
        Me.fndSrcType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.fndSrcType.Controls.Add(Me.MyLabel38)
        Me.fndSrcType.Controls.Add(Me.txtTapalNo)
        Me.fndSrcType.Controls.Add(Me.MyLabel37)
        Me.fndSrcType.Controls.Add(Me.txtDataAndTimeSelection)
        Me.fndSrcType.Controls.Add(Me.txtReverseVoucher)
        Me.fndSrcType.Controls.Add(Me.MyLabel5)
        Me.fndSrcType.Controls.Add(Me.Panel1)
        Me.fndSrcType.Controls.Add(Me.chkIndAS)
        Me.fndSrcType.Controls.Add(Me.LblManAuto)
        Me.fndSrcType.Controls.Add(Me.txtLocation)
        Me.fndSrcType.Controls.Add(Me.MyLabel1)
        Me.fndSrcType.Controls.Add(Me.lblLocation)
        Me.fndSrcType.Controls.Add(Me.txtUnbalAmt)
        Me.fndSrcType.Controls.Add(Me.txtCr)
        Me.fndSrcType.Controls.Add(Me.txtDr)
        Me.fndSrcType.Controls.Add(Me.UsLock1)
        Me.fndSrcType.Controls.Add(Me.dtSrc)
        Me.fndSrcType.Controls.Add(Me.dtVoucher)
        Me.fndSrcType.Controls.Add(Me.fndSrcCode)
        Me.fndSrcType.Controls.Add(Me.fndVoucher)
        Me.fndSrcType.Controls.Add(Me.btnDrillDown)
        Me.fndSrcType.Controls.Add(Me.RadLabel1)
        Me.fndSrcType.Controls.Add(Me.RadLabel17)
        Me.fndSrcType.Controls.Add(Me.txtDocDesc)
        Me.fndSrcType.Controls.Add(Me.btnNew)
        Me.fndSrcType.Controls.Add(Me.txtVoucherDesc)
        Me.fndSrcType.Controls.Add(Me.RadLabel2)
        Me.fndSrcType.Controls.Add(Me.RadLabel21)
        Me.fndSrcType.Controls.Add(Me.RadLabel3)
        Me.fndSrcType.Controls.Add(Me.txtComments)
        Me.fndSrcType.Controls.Add(Me.RadLabel4)
        Me.fndSrcType.Controls.Add(Me.RadLabel19)
        Me.fndSrcType.Controls.Add(Me.RadLabel5)
        Me.fndSrcType.Controls.Add(Me.txtRemarks)
        Me.fndSrcType.Controls.Add(Me.RadLabel6)
        Me.fndSrcType.Controls.Add(Me.RadLabel13)
        Me.fndSrcType.Controls.Add(Me.RadLabel7)
        Me.fndSrcType.Controls.Add(Me.txtSrcDesc)
        Me.fndSrcType.Controls.Add(Me.txtSrcDoc)
        Me.fndSrcType.Controls.Add(Me.RadGroupBox1)
        Me.fndSrcType.Controls.Add(Me.RadGroupBox2)
        Me.fndSrcType.Controls.Add(Me.cmbType)
        Me.fndSrcType.Controls.Add(Me.RadGroupBox4)
        Me.fndSrcType.Controls.Add(Me.RadLabel9)
        Me.fndSrcType.Controls.Add(Me.gdAcc1)
        Me.fndSrcType.Controls.Add(Me.RadLabel10)
        Me.fndSrcType.Controls.Add(Me.RadLabel11)
        Me.fndSrcType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fndSrcType.HeaderText = ""
        Me.fndSrcType.Location = New System.Drawing.Point(0, 0)
        Me.fndSrcType.Name = "fndSrcType"
        Me.fndSrcType.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.fndSrcType.Size = New System.Drawing.Size(1085, 374)
        Me.fndSrcType.TabIndex = 0
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(799, 213)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel38.TabIndex = 12132
        Me.MyLabel38.Text = "Tapal No"
        '
        'txtTapalNo
        '
        Me.txtTapalNo.CalculationExpression = Nothing
        Me.txtTapalNo.FieldCode = Nothing
        Me.txtTapalNo.FieldDesc = Nothing
        Me.txtTapalNo.FieldMaxLength = 0
        Me.txtTapalNo.FieldName = Nothing
        Me.txtTapalNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTapalNo.isCalculatedField = False
        Me.txtTapalNo.IsSourceFromTable = False
        Me.txtTapalNo.IsSourceFromValueList = False
        Me.txtTapalNo.IsUnique = False
        Me.txtTapalNo.Location = New System.Drawing.Point(887, 212)
        Me.txtTapalNo.MendatroryField = False
        Me.txtTapalNo.MyLinkLable1 = Nothing
        Me.txtTapalNo.MyLinkLable2 = Nothing
        Me.txtTapalNo.Name = "txtTapalNo"
        Me.txtTapalNo.ReferenceFieldDesc = Nothing
        Me.txtTapalNo.ReferenceFieldName = Nothing
        Me.txtTapalNo.ReferenceTableName = Nothing
        Me.txtTapalNo.Size = New System.Drawing.Size(191, 18)
        Me.txtTapalNo.TabIndex = 12131
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(799, 234)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel37.TabIndex = 12130
        Me.MyLabel37.Text = "Date And Time"
        '
        'txtDataAndTimeSelection
        '
        Me.txtDataAndTimeSelection.CalculationExpression = Nothing
        Me.txtDataAndTimeSelection.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDataAndTimeSelection.FieldCode = Nothing
        Me.txtDataAndTimeSelection.FieldDesc = Nothing
        Me.txtDataAndTimeSelection.FieldMaxLength = 0
        Me.txtDataAndTimeSelection.FieldName = Nothing
        Me.txtDataAndTimeSelection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDataAndTimeSelection.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDataAndTimeSelection.isCalculatedField = False
        Me.txtDataAndTimeSelection.IsSourceFromTable = False
        Me.txtDataAndTimeSelection.IsSourceFromValueList = False
        Me.txtDataAndTimeSelection.IsUnique = False
        Me.txtDataAndTimeSelection.Location = New System.Drawing.Point(886, 233)
        Me.txtDataAndTimeSelection.MendatroryField = False
        Me.txtDataAndTimeSelection.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDataAndTimeSelection.MyLinkLable1 = Nothing
        Me.txtDataAndTimeSelection.MyLinkLable2 = Nothing
        Me.txtDataAndTimeSelection.Name = "txtDataAndTimeSelection"
        Me.txtDataAndTimeSelection.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDataAndTimeSelection.ReferenceFieldDesc = Nothing
        Me.txtDataAndTimeSelection.ReferenceFieldName = Nothing
        Me.txtDataAndTimeSelection.ReferenceTableName = Nothing
        Me.txtDataAndTimeSelection.ShowCheckBox = True
        Me.txtDataAndTimeSelection.Size = New System.Drawing.Size(143, 18)
        Me.txtDataAndTimeSelection.TabIndex = 12129
        Me.txtDataAndTimeSelection.TabStop = False
        Me.txtDataAndTimeSelection.Text = "13/06/2011 11:29 AM"
        Me.txtDataAndTimeSelection.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtReverseVoucher
        '
        Me.txtReverseVoucher.CalculationExpression = Nothing
        Me.txtReverseVoucher.FieldCode = Nothing
        Me.txtReverseVoucher.FieldDesc = Nothing
        Me.txtReverseVoucher.FieldMaxLength = 0
        Me.txtReverseVoucher.FieldName = Nothing
        Me.txtReverseVoucher.isCalculatedField = False
        Me.txtReverseVoucher.IsSourceFromTable = False
        Me.txtReverseVoucher.IsSourceFromValueList = False
        Me.txtReverseVoucher.IsUnique = False
        Me.txtReverseVoucher.Location = New System.Drawing.Point(652, 6)
        Me.txtReverseVoucher.MendatroryField = True
        Me.txtReverseVoucher.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReverseVoucher.MyLinkLable1 = Nothing
        Me.txtReverseVoucher.MyLinkLable2 = Nothing
        Me.txtReverseVoucher.MyReadOnly = False
        Me.txtReverseVoucher.MyShowMasterFormButton = False
        Me.txtReverseVoucher.Name = "txtReverseVoucher"
        Me.txtReverseVoucher.ReferenceFieldDesc = Nothing
        Me.txtReverseVoucher.ReferenceFieldName = Nothing
        Me.txtReverseVoucher.ReferenceTableName = Nothing
        Me.txtReverseVoucher.Size = New System.Drawing.Size(141, 19)
        Me.txtReverseVoucher.TabIndex = 76
        Me.txtReverseVoucher.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(555, 7)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel5.TabIndex = 77
        Me.MyLabel5.Text = "Reverse Voucher"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.GroupBox30)
        Me.Panel1.Location = New System.Drawing.Point(799, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(277, 94)
        Me.Panel1.TabIndex = 75
        Me.Panel1.Visible = False
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(7, 6)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(261, 24)
        Me.RadButton1.TabIndex = 72
        Me.RadButton1.Text = "Create All Tables"
        '
        'GroupBox30
        '
        Me.GroupBox30.Controls.Add(Me.txtGLAC)
        Me.GroupBox30.Controls.Add(Me.MyLabel4)
        Me.GroupBox30.Controls.Add(Me.btnInvSummaryUpdate)
        Me.GroupBox30.Location = New System.Drawing.Point(3, 36)
        Me.GroupBox30.Name = "GroupBox30"
        Me.GroupBox30.Size = New System.Drawing.Size(270, 37)
        Me.GroupBox30.TabIndex = 71
        Me.GroupBox30.TabStop = False
        Me.GroupBox30.Text = "Update GL Summary"
        '
        'txtGLAC
        '
        Me.txtGLAC.arrDispalyMember = Nothing
        Me.txtGLAC.arrValueMember = Nothing
        Me.txtGLAC.Location = New System.Drawing.Point(45, 13)
        Me.txtGLAC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAC.MyLinkLable1 = Me.MyLabel4
        Me.txtGLAC.MyLinkLable2 = Nothing
        Me.txtGLAC.MyNullText = "Please Select..."
        Me.txtGLAC.Name = "txtGLAC"
        Me.txtGLAC.Size = New System.Drawing.Size(174, 19)
        Me.txtGLAC.TabIndex = 285
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(2, 13)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 18)
        Me.MyLabel4.TabIndex = 286
        Me.MyLabel4.Text = "GL A/C"
        '
        'btnInvSummaryUpdate
        '
        Me.btnInvSummaryUpdate.Location = New System.Drawing.Point(221, 13)
        Me.btnInvSummaryUpdate.Name = "btnInvSummaryUpdate"
        Me.btnInvSummaryUpdate.Size = New System.Drawing.Size(48, 19)
        Me.btnInvSummaryUpdate.TabIndex = 51
        Me.btnInvSummaryUpdate.Text = "Process"
        '
        'chkIndAS
        '
        Me.chkIndAS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIndAS.Location = New System.Drawing.Point(250, 93)
        Me.chkIndAS.Name = "chkIndAS"
        '
        '
        '
        Me.chkIndAS.RootElement.StretchHorizontally = True
        Me.chkIndAS.RootElement.StretchVertically = True
        Me.chkIndAS.Size = New System.Drawing.Size(89, 16)
        Me.chkIndAS.TabIndex = 74
        Me.chkIndAS.Text = "Ind As"
        '
        'LblManAuto
        '
        Me.LblManAuto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblManAuto.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblManAuto.FieldName = Nothing
        Me.LblManAuto.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManAuto.Location = New System.Drawing.Point(986, 8)
        Me.LblManAuto.Name = "LblManAuto"
        Me.LblManAuto.Size = New System.Drawing.Size(83, 17)
        Me.LblManAuto.TabIndex = 73
        Me.LblManAuto.Text = "GENERATED"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(116, 70)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(128, 19)
        Me.txtLocation.TabIndex = 70
        Me.txtLocation.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 71)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel1.TabIndex = 72
        Me.MyLabel1.Text = "Location"
        '
        'lblLocation
        '
        Me.lblLocation.CalculationExpression = Nothing
        Me.lblLocation.FieldCode = Nothing
        Me.lblLocation.FieldDesc = Nothing
        Me.lblLocation.FieldMaxLength = 0
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.isCalculatedField = False
        Me.lblLocation.IsSourceFromTable = False
        Me.lblLocation.IsSourceFromValueList = False
        Me.lblLocation.IsUnique = False
        Me.lblLocation.Location = New System.Drawing.Point(247, 69)
        Me.lblLocation.MaxLength = 150
        Me.lblLocation.MendatroryField = False
        Me.lblLocation.MyLinkLable1 = Nothing
        Me.lblLocation.MyLinkLable2 = Nothing
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.ReadOnly = True
        Me.lblLocation.ReferenceFieldDesc = Nothing
        Me.lblLocation.ReferenceFieldName = Nothing
        Me.lblLocation.ReferenceTableName = Nothing
        Me.lblLocation.Size = New System.Drawing.Size(311, 20)
        Me.lblLocation.TabIndex = 71
        Me.lblLocation.TabStop = False
        '
        'txtUnbalAmt
        '
        Me.txtUnbalAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUnbalAmt.BackColor = System.Drawing.Color.White
        Me.txtUnbalAmt.CalculationExpression = Nothing
        Me.txtUnbalAmt.DecimalPlaces = 0
        Me.txtUnbalAmt.FieldCode = Nothing
        Me.txtUnbalAmt.FieldDesc = Nothing
        Me.txtUnbalAmt.FieldMaxLength = 0
        Me.txtUnbalAmt.FieldName = Nothing
        Me.txtUnbalAmt.isCalculatedField = False
        Me.txtUnbalAmt.IsSourceFromTable = False
        Me.txtUnbalAmt.IsSourceFromValueList = False
        Me.txtUnbalAmt.IsUnique = False
        Me.txtUnbalAmt.Location = New System.Drawing.Point(953, 349)
        Me.txtUnbalAmt.MendatroryField = False
        Me.txtUnbalAmt.MyLinkLable1 = Nothing
        Me.txtUnbalAmt.MyLinkLable2 = Nothing
        Me.txtUnbalAmt.Name = "txtUnbalAmt"
        Me.txtUnbalAmt.ReadOnly = True
        Me.txtUnbalAmt.ReferenceFieldDesc = Nothing
        Me.txtUnbalAmt.ReferenceFieldName = Nothing
        Me.txtUnbalAmt.ReferenceTableName = Nothing
        Me.txtUnbalAmt.Size = New System.Drawing.Size(125, 20)
        Me.txtUnbalAmt.TabIndex = 21
        Me.txtUnbalAmt.TabStop = False
        Me.txtUnbalAmt.Text = "0"
        Me.txtUnbalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtUnbalAmt.Value = 0R
        '
        'txtCr
        '
        Me.txtCr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCr.BackColor = System.Drawing.Color.White
        Me.txtCr.CalculationExpression = Nothing
        Me.txtCr.DecimalPlaces = 0
        Me.txtCr.FieldCode = Nothing
        Me.txtCr.FieldDesc = Nothing
        Me.txtCr.FieldMaxLength = 0
        Me.txtCr.FieldName = Nothing
        Me.txtCr.isCalculatedField = False
        Me.txtCr.IsSourceFromTable = False
        Me.txtCr.IsSourceFromValueList = False
        Me.txtCr.IsUnique = False
        Me.txtCr.Location = New System.Drawing.Point(726, 349)
        Me.txtCr.MendatroryField = False
        Me.txtCr.MyLinkLable1 = Nothing
        Me.txtCr.MyLinkLable2 = Nothing
        Me.txtCr.Name = "txtCr"
        Me.txtCr.ReadOnly = True
        Me.txtCr.ReferenceFieldDesc = Nothing
        Me.txtCr.ReferenceFieldName = Nothing
        Me.txtCr.ReferenceTableName = Nothing
        Me.txtCr.Size = New System.Drawing.Size(125, 20)
        Me.txtCr.TabIndex = 19
        Me.txtCr.TabStop = False
        Me.txtCr.Text = "0"
        Me.txtCr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCr.Value = 0R
        '
        'txtDr
        '
        Me.txtDr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDr.BackColor = System.Drawing.Color.White
        Me.txtDr.CalculationExpression = Nothing
        Me.txtDr.DecimalPlaces = 0
        Me.txtDr.FieldCode = Nothing
        Me.txtDr.FieldDesc = Nothing
        Me.txtDr.FieldMaxLength = 0
        Me.txtDr.FieldName = Nothing
        Me.txtDr.isCalculatedField = False
        Me.txtDr.IsSourceFromTable = False
        Me.txtDr.IsSourceFromValueList = False
        Me.txtDr.IsUnique = False
        Me.txtDr.Location = New System.Drawing.Point(555, 349)
        Me.txtDr.MendatroryField = False
        Me.txtDr.MyLinkLable1 = Nothing
        Me.txtDr.MyLinkLable2 = Nothing
        Me.txtDr.Name = "txtDr"
        Me.txtDr.ReadOnly = True
        Me.txtDr.ReferenceFieldDesc = Nothing
        Me.txtDr.ReferenceFieldName = Nothing
        Me.txtDr.ReferenceTableName = Nothing
        Me.txtDr.Size = New System.Drawing.Size(125, 20)
        Me.txtDr.TabIndex = 17
        Me.txtDr.TabStop = False
        Me.txtDr.Text = "0"
        Me.txtDr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDr.Value = 0R
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(801, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(114, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 69
        '
        'dtSrc
        '
        Me.dtSrc.CalculationExpression = Nothing
        Me.dtSrc.CustomFormat = "dd/MM/yyyy"
        Me.dtSrc.FieldCode = Nothing
        Me.dtSrc.FieldDesc = Nothing
        Me.dtSrc.FieldMaxLength = 0
        Me.dtSrc.FieldName = Nothing
        Me.dtSrc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtSrc.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtSrc.isCalculatedField = False
        Me.dtSrc.IsSourceFromTable = False
        Me.dtSrc.IsSourceFromValueList = False
        Me.dtSrc.IsUnique = False
        Me.dtSrc.Location = New System.Drawing.Point(383, 112)
        Me.dtSrc.MendatroryField = False
        Me.dtSrc.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtSrc.MyLinkLable1 = Nothing
        Me.dtSrc.MyLinkLable2 = Nothing
        Me.dtSrc.Name = "dtSrc"
        Me.dtSrc.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtSrc.ReferenceFieldDesc = Nothing
        Me.dtSrc.ReferenceFieldName = Nothing
        Me.dtSrc.ReferenceTableName = Nothing
        Me.dtSrc.Size = New System.Drawing.Size(77, 18)
        Me.dtSrc.TabIndex = 10
        Me.dtSrc.TabStop = False
        Me.dtSrc.Text = "13/06/2011"
        Me.dtSrc.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'dtVoucher
        '
        Me.dtVoucher.CalculationExpression = Nothing
        Me.dtVoucher.CustomFormat = "dd/MM/yyyy"
        Me.dtVoucher.FieldCode = Nothing
        Me.dtVoucher.FieldDesc = Nothing
        Me.dtVoucher.FieldMaxLength = 0
        Me.dtVoucher.FieldName = Nothing
        Me.dtVoucher.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtVoucher.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtVoucher.isCalculatedField = False
        Me.dtVoucher.IsSourceFromTable = False
        Me.dtVoucher.IsSourceFromValueList = False
        Me.dtVoucher.IsUnique = False
        Me.dtVoucher.Location = New System.Drawing.Point(466, 6)
        Me.dtVoucher.MendatroryField = False
        Me.dtVoucher.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtVoucher.MyLinkLable1 = Nothing
        Me.dtVoucher.MyLinkLable2 = Nothing
        Me.dtVoucher.Name = "dtVoucher"
        Me.dtVoucher.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtVoucher.ReferenceFieldDesc = Nothing
        Me.dtVoucher.ReferenceFieldName = Nothing
        Me.dtVoucher.ReferenceTableName = Nothing
        Me.dtVoucher.Size = New System.Drawing.Size(77, 18)
        Me.dtVoucher.TabIndex = 1
        Me.dtVoucher.TabStop = False
        Me.dtVoucher.Text = "13/06/2011"
        Me.dtVoucher.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'fndSrcCode
        '
        Me.fndSrcCode.CalculationExpression = Nothing
        Me.fndSrcCode.FieldCode = Nothing
        Me.fndSrcCode.FieldDesc = Nothing
        Me.fndSrcCode.FieldMaxLength = 0
        Me.fndSrcCode.FieldName = Nothing
        Me.fndSrcCode.isCalculatedField = False
        Me.fndSrcCode.IsSourceFromTable = False
        Me.fndSrcCode.IsSourceFromValueList = False
        Me.fndSrcCode.IsUnique = False
        Me.fndSrcCode.Location = New System.Drawing.Point(116, 49)
        Me.fndSrcCode.MendatroryField = True
        Me.fndSrcCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSrcCode.MyLinkLable1 = Nothing
        Me.fndSrcCode.MyLinkLable2 = Nothing
        Me.fndSrcCode.MyReadOnly = False
        Me.fndSrcCode.MyShowMasterFormButton = False
        Me.fndSrcCode.Name = "fndSrcCode"
        Me.fndSrcCode.ReferenceFieldDesc = Nothing
        Me.fndSrcCode.ReferenceFieldName = Nothing
        Me.fndSrcCode.ReferenceTableName = Nothing
        Me.fndSrcCode.Size = New System.Drawing.Size(128, 19)
        Me.fndSrcCode.TabIndex = 3
        Me.fndSrcCode.Value = ""
        '
        'fndVoucher
        '
        Me.fndVoucher.FieldName = Nothing
        Me.fndVoucher.Location = New System.Drawing.Point(116, 5)
        Me.fndVoucher.MendatroryField = False
        Me.fndVoucher.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndVoucher.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndVoucher.MyLinkLable1 = Nothing
        Me.fndVoucher.MyLinkLable2 = Nothing
        Me.fndVoucher.MyMaxLength = 32767
        Me.fndVoucher.MyReadOnly = False
        Me.fndVoucher.Name = "fndVoucher"
        Me.fndVoucher.Size = New System.Drawing.Size(238, 20)
        Me.fndVoucher.TabIndex = 0
        Me.fndVoucher.TabStop = False
        Me.fndVoucher.Value = ""
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDrillDown.Location = New System.Drawing.Point(245, 111)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(20, 20)
        Me.btnDrillDown.TabIndex = 8
        '
        'RadLabel17
        '
        Me.RadLabel17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Location = New System.Drawing.Point(853, 351)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(97, 18)
        Me.RadLabel17.TabIndex = 20
        Me.RadLabel17.Text = "Out Of Balance By"
        '
        'txtDocDesc
        '
        Me.txtDocDesc.CalculationExpression = Nothing
        Me.txtDocDesc.FieldCode = Nothing
        Me.txtDocDesc.FieldDesc = Nothing
        Me.txtDocDesc.FieldMaxLength = 0
        Me.txtDocDesc.FieldName = Nothing
        Me.txtDocDesc.isCalculatedField = False
        Me.txtDocDesc.IsSourceFromTable = False
        Me.txtDocDesc.IsSourceFromValueList = False
        Me.txtDocDesc.IsUnique = False
        Me.txtDocDesc.Location = New System.Drawing.Point(116, 133)
        Me.txtDocDesc.MendatroryField = False
        Me.txtDocDesc.MyLinkLable1 = Nothing
        Me.txtDocDesc.MyLinkLable2 = Nothing
        Me.txtDocDesc.Name = "txtDocDesc"
        Me.txtDocDesc.ReferenceFieldDesc = Nothing
        Me.txtDocDesc.ReferenceFieldName = Nothing
        Me.txtDocDesc.ReferenceTableName = Nothing
        Me.txtDocDesc.Size = New System.Drawing.Size(442, 20)
        Me.txtDocDesc.TabIndex = 11
        '
        'btnNew
        '
        Me.btnNew.Image = Global.XpertERPGeneralLedger.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(356, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(19, 20)
        Me.btnNew.TabIndex = 0
        '
        'txtVoucherDesc
        '
        Me.txtVoucherDesc.CalculationExpression = Nothing
        Me.txtVoucherDesc.FieldCode = Nothing
        Me.txtVoucherDesc.FieldDesc = Nothing
        Me.txtVoucherDesc.FieldMaxLength = 0
        Me.txtVoucherDesc.FieldName = Nothing
        Me.txtVoucherDesc.isCalculatedField = False
        Me.txtVoucherDesc.IsSourceFromTable = False
        Me.txtVoucherDesc.IsSourceFromValueList = False
        Me.txtVoucherDesc.IsUnique = False
        Me.txtVoucherDesc.Location = New System.Drawing.Point(116, 27)
        Me.txtVoucherDesc.MaxLength = 200
        Me.txtVoucherDesc.MendatroryField = False
        Me.txtVoucherDesc.MyLinkLable1 = Nothing
        Me.txtVoucherDesc.MyLinkLable2 = Nothing
        Me.txtVoucherDesc.Name = "txtVoucherDesc"
        Me.txtVoucherDesc.ReferenceFieldDesc = Nothing
        Me.txtVoucherDesc.ReferenceFieldName = Nothing
        Me.txtVoucherDesc.ReferenceTableName = Nothing
        Me.txtVoucherDesc.Size = New System.Drawing.Size(441, 20)
        Me.txtVoucherDesc.TabIndex = 2
        '
        'txtComments
        '
        Me.txtComments.CalculationExpression = Nothing
        Me.txtComments.FieldCode = Nothing
        Me.txtComments.FieldDesc = Nothing
        Me.txtComments.FieldMaxLength = 0
        Me.txtComments.FieldName = Nothing
        Me.txtComments.isCalculatedField = False
        Me.txtComments.IsSourceFromTable = False
        Me.txtComments.IsSourceFromValueList = False
        Me.txtComments.IsUnique = False
        Me.txtComments.Location = New System.Drawing.Point(116, 177)
        Me.txtComments.MaxLength = 130
        Me.txtComments.MendatroryField = False
        Me.txtComments.MyLinkLable1 = Nothing
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReferenceFieldDesc = Nothing
        Me.txtComments.ReferenceFieldName = Nothing
        Me.txtComments.ReferenceTableName = Nothing
        Me.txtComments.Size = New System.Drawing.Size(676, 20)
        Me.txtComments.TabIndex = 13
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(116, 155)
        Me.txtRemarks.MaxLength = 130
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(676, 20)
        Me.txtRemarks.TabIndex = 12
        '
        'txtSrcDesc
        '
        Me.txtSrcDesc.CalculationExpression = Nothing
        Me.txtSrcDesc.FieldCode = Nothing
        Me.txtSrcDesc.FieldDesc = Nothing
        Me.txtSrcDesc.FieldMaxLength = 0
        Me.txtSrcDesc.FieldName = Nothing
        Me.txtSrcDesc.isCalculatedField = False
        Me.txtSrcDesc.IsSourceFromTable = False
        Me.txtSrcDesc.IsSourceFromValueList = False
        Me.txtSrcDesc.IsUnique = False
        Me.txtSrcDesc.Location = New System.Drawing.Point(247, 48)
        Me.txtSrcDesc.MaxLength = 150
        Me.txtSrcDesc.MendatroryField = False
        Me.txtSrcDesc.MyLinkLable1 = Nothing
        Me.txtSrcDesc.MyLinkLable2 = Nothing
        Me.txtSrcDesc.Name = "txtSrcDesc"
        Me.txtSrcDesc.ReadOnly = True
        Me.txtSrcDesc.ReferenceFieldDesc = Nothing
        Me.txtSrcDesc.ReferenceFieldName = Nothing
        Me.txtSrcDesc.ReferenceTableName = Nothing
        Me.txtSrcDesc.Size = New System.Drawing.Size(311, 20)
        Me.txtSrcDesc.TabIndex = 4
        Me.txtSrcDesc.TabStop = False
        '
        'txtSrcDoc
        '
        Me.txtSrcDoc.CalculationExpression = Nothing
        Me.txtSrcDoc.FieldCode = Nothing
        Me.txtSrcDoc.FieldDesc = Nothing
        Me.txtSrcDoc.FieldMaxLength = 0
        Me.txtSrcDoc.FieldName = Nothing
        Me.txtSrcDoc.isCalculatedField = False
        Me.txtSrcDoc.IsSourceFromTable = False
        Me.txtSrcDoc.IsSourceFromValueList = False
        Me.txtSrcDoc.IsUnique = False
        Me.txtSrcDoc.Location = New System.Drawing.Point(116, 111)
        Me.txtSrcDoc.MendatroryField = False
        Me.txtSrcDoc.MyLinkLable1 = Nothing
        Me.txtSrcDoc.MyLinkLable2 = Nothing
        Me.txtSrcDoc.Name = "txtSrcDoc"
        Me.txtSrcDoc.ReferenceFieldDesc = Nothing
        Me.txtSrcDoc.ReferenceFieldName = Nothing
        Me.txtSrcDoc.ReferenceTableName = Nothing
        Me.txtSrcDoc.Size = New System.Drawing.Size(128, 20)
        Me.txtSrcDoc.TabIndex = 7
        '
        'cmbType
        '
        Me.cmbType.AutoCompleteDisplayMember = Nothing
        Me.cmbType.AutoCompleteValueMember = Nothing
        Me.cmbType.CalculationExpression = Nothing
        Me.cmbType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbType.FieldCode = Nothing
        Me.cmbType.FieldDesc = Nothing
        Me.cmbType.FieldMaxLength = 0
        Me.cmbType.FieldName = Nothing
        Me.cmbType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbType.isCalculatedField = False
        Me.cmbType.IsSourceFromTable = False
        Me.cmbType.IsSourceFromValueList = False
        Me.cmbType.IsUnique = False
        RadListDataItem5.Text = "Normal"
        RadListDataItem6.Text = "Adjustment"
        RadListDataItem7.Enabled = False
        RadListDataItem7.Text = "Closing"
        RadListDataItem8.Text = "Opening Only"
        Me.cmbType.Items.Add(RadListDataItem5)
        Me.cmbType.Items.Add(RadListDataItem6)
        Me.cmbType.Items.Add(RadListDataItem7)
        Me.cmbType.Items.Add(RadListDataItem8)
        Me.cmbType.Location = New System.Drawing.Point(116, 91)
        Me.cmbType.MendatroryField = False
        Me.cmbType.MyLinkLable1 = Nothing
        Me.cmbType.MyLinkLable2 = Nothing
        Me.cmbType.Name = "cmbType"
        Me.cmbType.ReferenceFieldDesc = Nothing
        Me.cmbType.ReferenceFieldName = Nothing
        Me.cmbType.ReferenceTableName = Nothing
        Me.cmbType.Size = New System.Drawing.Size(127, 18)
        Me.cmbType.TabIndex = 6
        '
        'gdAcc1
        '
        Me.gdAcc1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gdAcc1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gdAcc1.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnKeystroke
        Me.gdAcc1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gdAcc1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gdAcc1.ForeColor = System.Drawing.Color.Black
        Me.gdAcc1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gdAcc1.Location = New System.Drawing.Point(13, 261)
        '
        'gdAcc1
        '
        Me.gdAcc1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gdAcc1.MasterTemplate.AllowAddNewRow = False
        Me.gdAcc1.MasterTemplate.AllowDeleteRow = False
        GridViewDecimalColumn4.HeaderText = "Line No"
        GridViewDecimalColumn4.Name = "gdcolLineNo"
        GridViewDecimalColumn4.ReadOnly = True
        GridViewTextBoxColumn7.HeaderText = "Account Code"
        GridViewTextBoxColumn7.Name = "gdcolAcc"
        GridViewTextBoxColumn7.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        GridViewTextBoxColumn7.Width = 126
        GridViewTextBoxColumn8.HeaderText = "Account Description"
        GridViewTextBoxColumn8.Name = "gdColAccDesc"
        GridViewTextBoxColumn8.ReadOnly = True
        GridViewTextBoxColumn8.Width = 141
        GridViewDecimalColumn5.HeaderText = "Debit"
        GridViewDecimalColumn5.Name = "gdColDr"
        GridViewDecimalColumn5.Width = 106
        GridViewDecimalColumn6.HeaderText = "Credit"
        GridViewDecimalColumn6.Name = "gdColCr"
        GridViewDecimalColumn6.Width = 100
        GridViewTextBoxColumn9.HeaderText = "Description"
        GridViewTextBoxColumn9.Name = "gdColDesc"
        GridViewTextBoxColumn9.Width = 153
        GridViewTextBoxColumn10.HeaderText = "Reference"
        GridViewTextBoxColumn10.Name = "gdColRef"
        GridViewTextBoxColumn10.Width = 203
        GridViewDateTimeColumn2.CustomFormat = "dd/MM/yyyy"
        GridViewDateTimeColumn2.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.GeneralDate
        GridViewDateTimeColumn2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        GridViewDateTimeColumn2.HeaderText = "Posting Date"
        GridViewDateTimeColumn2.Name = "gdColPostDate"
        GridViewDateTimeColumn2.ReadOnly = True
        GridViewDateTimeColumn2.Width = 70
        GridViewTextBoxColumn11.HeaderText = "Hierarchy Code"
        GridViewTextBoxColumn11.Name = "Hierarchy Code"
        GridViewTextBoxColumn12.HeaderText = "Cost Centre"
        GridViewTextBoxColumn12.Name = "Cost Centre"
        Me.gdAcc1.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewDecimalColumn4, GridViewTextBoxColumn7, GridViewTextBoxColumn8, GridViewDecimalColumn5, GridViewDecimalColumn6, GridViewTextBoxColumn9, GridViewTextBoxColumn10, GridViewDateTimeColumn2, GridViewTextBoxColumn11, GridViewTextBoxColumn12})
        Me.gdAcc1.MasterTemplate.EnableGrouping = False
        Me.gdAcc1.MasterTemplate.ShowHeaderCellButtons = True
        SortDescriptor2.PropertyName = "column1"
        Me.gdAcc1.MasterTemplate.SortDescriptors.AddRange(New Telerik.WinControls.Data.SortDescriptor() {SortDescriptor2})
        Me.gdAcc1.Name = "gdAcc1"
        Me.gdAcc1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gdAcc1.ShowGroupPanel = False
        Me.gdAcc1.ShowHeaderCellButtons = True
        Me.gdAcc1.Size = New System.Drawing.Size(1060, 82)
        Me.gdAcc1.TabIndex = 16
        Me.gdAcc1.TabStop = False
        Me.gdAcc1.Text = "RadGridView1"
        '
        'btnUnpostTransaction
        '
        Me.btnUnpostTransaction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnpostTransaction.Location = New System.Drawing.Point(264, 6)
        Me.btnUnpostTransaction.Name = "btnUnpostTransaction"
        Me.btnUnpostTransaction.Size = New System.Drawing.Size(103, 24)
        Me.btnUnpostTransaction.TabIndex = 4
        Me.btnUnpostTransaction.Text = "Unpost Transaction"
        Me.btnUnpostTransaction.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(200, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(62, 24)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.butCostCenterAndHirerachy_Update_AfterPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtToExpDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtFromExpDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSendToTally)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpostTransaction)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnProAuth)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAuth)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(1106, 457)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage5
        Me.RadPageView1.Size = New System.Drawing.Size(1106, 420)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.fndSrcType)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(90.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(1085, 374)
        Me.RadPageViewPage5.Text = "Voucher Detail"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1085, 374)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1085, 374)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'txtToExpDate
        '
        Me.txtToExpDate.CalculationExpression = Nothing
        Me.txtToExpDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToExpDate.FieldCode = Nothing
        Me.txtToExpDate.FieldDesc = Nothing
        Me.txtToExpDate.FieldMaxLength = 0
        Me.txtToExpDate.FieldName = Nothing
        Me.txtToExpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToExpDate.isCalculatedField = False
        Me.txtToExpDate.IsSourceFromTable = False
        Me.txtToExpDate.IsSourceFromValueList = False
        Me.txtToExpDate.IsUnique = False
        Me.txtToExpDate.Location = New System.Drawing.Point(881, 9)
        Me.txtToExpDate.MendatroryField = False
        Me.txtToExpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToExpDate.MyLinkLable1 = Nothing
        Me.txtToExpDate.MyLinkLable2 = Nothing
        Me.txtToExpDate.Name = "txtToExpDate"
        Me.txtToExpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToExpDate.ReferenceFieldDesc = Nothing
        Me.txtToExpDate.ReferenceFieldName = Nothing
        Me.txtToExpDate.ReferenceTableName = Nothing
        Me.txtToExpDate.Size = New System.Drawing.Size(77, 18)
        Me.txtToExpDate.TabIndex = 10
        Me.txtToExpDate.TabStop = False
        Me.txtToExpDate.Text = "13/06/2011"
        Me.txtToExpDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.txtToExpDate.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(791, 10)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel3.TabIndex = 11
        Me.MyLabel3.Text = "To Export Date"
        Me.MyLabel3.Visible = False
        '
        'txtFromExpDate
        '
        Me.txtFromExpDate.CalculationExpression = Nothing
        Me.txtFromExpDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromExpDate.FieldCode = Nothing
        Me.txtFromExpDate.FieldDesc = Nothing
        Me.txtFromExpDate.FieldMaxLength = 0
        Me.txtFromExpDate.FieldName = Nothing
        Me.txtFromExpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromExpDate.isCalculatedField = False
        Me.txtFromExpDate.IsSourceFromTable = False
        Me.txtFromExpDate.IsSourceFromValueList = False
        Me.txtFromExpDate.IsUnique = False
        Me.txtFromExpDate.Location = New System.Drawing.Point(714, 9)
        Me.txtFromExpDate.MendatroryField = False
        Me.txtFromExpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromExpDate.MyLinkLable1 = Nothing
        Me.txtFromExpDate.MyLinkLable2 = Nothing
        Me.txtFromExpDate.Name = "txtFromExpDate"
        Me.txtFromExpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromExpDate.ReferenceFieldDesc = Nothing
        Me.txtFromExpDate.ReferenceFieldName = Nothing
        Me.txtFromExpDate.ReferenceTableName = Nothing
        Me.txtFromExpDate.Size = New System.Drawing.Size(77, 18)
        Me.txtFromExpDate.TabIndex = 8
        Me.txtFromExpDate.TabStop = False
        Me.txtFromExpDate.Text = "13/06/2011"
        Me.txtFromExpDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.txtFromExpDate.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(611, 10)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel2.TabIndex = 9
        Me.MyLabel2.Text = "From Export Date"
        Me.MyLabel2.Visible = False
        '
        'btnSendToTally
        '
        Me.btnSendToTally.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSendToTally.Location = New System.Drawing.Point(961, 6)
        Me.btnSendToTally.Name = "btnSendToTally"
        Me.btnSendToTally.Size = New System.Drawing.Size(72, 24)
        Me.btnSendToTally.TabIndex = 6
        Me.btnSendToTally.Text = "Send To Tally"
        '
        'butCostCenterAndHirerachy_Update_AfterPost
        '
        Me.butCostCenterAndHirerachy_Update_AfterPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.butCostCenterAndHirerachy_Update_AfterPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butCostCenterAndHirerachy_Update_AfterPost.Location = New System.Drawing.Point(456, 6)
        Me.butCostCenterAndHirerachy_Update_AfterPost.Name = "butCostCenterAndHirerachy_Update_AfterPost"
        Me.butCostCenterAndHirerachy_Update_AfterPost.Size = New System.Drawing.Size(186, 24)
        Me.butCostCenterAndHirerachy_Update_AfterPost.TabIndex = 50
        Me.butCostCenterAndHirerachy_Update_AfterPost.Text = "Update Cost Center And Hirerachy"
        '
        'frmJournalEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1106, 477)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmJournalEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Journal Entry"
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtBoxVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBoxSrcType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBoxCr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBoxDr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRevese, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnProAuth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAuth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndSrcType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fndSrcType.ResumeLayout(False)
        Me.fndSrcType.PerformLayout()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTapalNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDataAndTimeSelection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox30.ResumeLayout(False)
        Me.GroupBox30.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInvSummaryUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManAuto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUnbalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtSrc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVoucherDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSrcDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSrcDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdAcc1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdAcc1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpostTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        CType(Me.txtToExpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromExpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendToTally, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mItmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtBoxVoucher As common.Controls.MyTextBox
    Friend WithEvents txtBoxSrcType As common.Controls.MyTextBox
    Friend WithEvents txtBoxCr As common.Controls.MyTextBox
    Friend WithEvents txtBoxDr As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCodeDesc As common.Controls.MyTextBox
    Friend WithEvents rdbOther As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbVendor As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbCustomer As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkReverse As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnProAuth As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAuth As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndSrcType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDocDesc As common.Controls.MyTextBox
    Friend WithEvents txtVoucherDesc As common.Controls.MyTextBox
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtSrcDesc As common.Controls.MyTextBox
    Friend WithEvents txtSrcDoc As common.Controls.MyTextBox
    Friend WithEvents cmbType As common.Controls.MyComboBox
    Friend WithEvents gdAcc As common.UserControls.MyRadGridView
    Friend WithEvents gdAcc1 As common.UserControls.MyRadGridView
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndVoucher As common.UserControls.txtNavigator
    Friend WithEvents fndSrcCode As common.UserControls.txtFinder
    Friend WithEvents dtRevese As common.Controls.MyDateTimePicker
    Friend WithEvents dtSrc As common.Controls.MyDateTimePicker
    Friend WithEvents dtVoucher As common.Controls.MyDateTimePicker
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents fndCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents mItmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mItmExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnUnpostTransaction As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDr As common.MyNumBox
    Friend WithEvents txtCr As common.MyNumBox
    Friend WithEvents txtUnbalAmt As common.MyNumBox
    Friend WithEvents btnSendToTally As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyTextBox
    Friend WithEvents LblManAuto As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents chkMonthly As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIndAS As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox30 As System.Windows.Forms.GroupBox
    Friend WithEvents btnInvSummaryUpdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtGLAC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtToExpDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtFromExpDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnExportblank As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtReverseVoucher As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents txtTapalNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents txtDataAndTimeSelection As common.Controls.MyDateTimePicker
    Friend WithEvents butCostCenterAndHirerachy_Update_AfterPost As RadButton
End Class

