<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMilkCollectionDCS
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtMDCSDate = New common.Controls.MyDateTimePicker()
        Me.lblMDCSDate = New common.Controls.MyLabel()
        Me.btnMGo = New Telerik.WinControls.UI.RadButton()
        Me.txtTotPendingSNFPer = New common.Controls.MyLabel()
        Me.txtTotPendingFATPer = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cboFATSNFType = New common.Controls.MyComboBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtSlipNo = New common.Controls.MyTextBox()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblTotPendingSNFKg = New common.Controls.MyLabel()
        Me.lblTotReceivedSNFKg = New common.Controls.MyLabel()
        Me.lblTotPendingFATKg = New common.Controls.MyLabel()
        Me.lblTotReceivedFATKg = New common.Controls.MyLabel()
        Me.lblTotPendingQty = New common.Controls.MyLabel()
        Me.lblTotReceivedQty = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.lblSNFPer = New common.Controls.MyLabel()
        Me.lblFATKg = New common.Controls.MyLabel()
        Me.lblSNFKg = New common.Controls.MyLabel()
        Me.lblFATPer = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.lblQty = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gv2 = New Telerik.WinControls.UI.RadGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnAddMissing = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnImport = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadButton()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtMDCSDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMDCSDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotPendingSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotPendingFATPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFATSNFType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSlipNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotPendingSNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotReceivedSNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotPendingFATKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotReceivedFATKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotPendingQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotReceivedQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFATKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFATPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.btnAddMissing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableAlternatingRowColor = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(1091, 199)
        Me.gv1.TabIndex = 2
        Me.gv1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Controls.Add(Me.txtTotPendingSNFPer)
        Me.Panel1.Controls.Add(Me.txtTotPendingFATPer)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.cboFATSNFType)
        Me.Panel1.Controls.Add(Me.MyLabel7)
        Me.Panel1.Controls.Add(Me.txtSlipNo)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.lblTotPendingSNFKg)
        Me.Panel1.Controls.Add(Me.lblTotReceivedSNFKg)
        Me.Panel1.Controls.Add(Me.lblTotPendingFATKg)
        Me.Panel1.Controls.Add(Me.lblTotReceivedFATKg)
        Me.Panel1.Controls.Add(Me.lblTotPendingQty)
        Me.Panel1.Controls.Add(Me.lblTotReceivedQty)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.lblMCC)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtMCC)
        Me.Panel1.Controls.Add(Me.lblSNFPer)
        Me.Panel1.Controls.Add(Me.lblFATKg)
        Me.Panel1.Controls.Add(Me.lblSNFKg)
        Me.Panel1.Controls.Add(Me.lblFATPer)
        Me.Panel1.Controls.Add(Me.MyLabel23)
        Me.Panel1.Controls.Add(Me.lblQty)
        Me.Panel1.Controls.Add(Me.MyLabel15)
        Me.Panel1.Controls.Add(Me.MyLabel10)
        Me.Panel1.Controls.Add(Me.UsLock1)
        Me.Panel1.Controls.Add(Me.RadLabel3)
        Me.Panel1.Controls.Add(Me.RadLabel4)
        Me.Panel1.Controls.Add(Me.RadLabel1)
        Me.Panel1.Controls.Add(Me.txtDocNo)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Controls.Add(Me.txtDesc)
        Me.Panel1.Controls.Add(Me.btnAddNew)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1091, 93)
        Me.Panel1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtMDCSDate)
        Me.RadGroupBox1.Controls.Add(Me.btnMGo)
        Me.RadGroupBox1.Controls.Add(Me.lblMDCSDate)
        Me.RadGroupBox1.HeaderText = "Generate DCS Data By Mobile"
        Me.RadGroupBox1.Location = New System.Drawing.Point(842, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(179, 79)
        Me.RadGroupBox1.TabIndex = 45
        Me.RadGroupBox1.Text = "Generate DCS Data By Mobile"
        '
        'txtMDCSDate
        '
        Me.txtMDCSDate.CalculationExpression = Nothing
        Me.txtMDCSDate.CustomFormat = "dd/MM/yyyy"
        Me.txtMDCSDate.FieldCode = Nothing
        Me.txtMDCSDate.FieldDesc = Nothing
        Me.txtMDCSDate.FieldMaxLength = 0
        Me.txtMDCSDate.FieldName = Nothing
        Me.txtMDCSDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMDCSDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMDCSDate.isCalculatedField = False
        Me.txtMDCSDate.IsSourceFromTable = False
        Me.txtMDCSDate.IsSourceFromValueList = False
        Me.txtMDCSDate.IsUnique = False
        Me.txtMDCSDate.Location = New System.Drawing.Point(63, 22)
        Me.txtMDCSDate.MendatroryField = True
        Me.txtMDCSDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMDCSDate.MyLinkLable1 = Me.lblMDCSDate
        Me.txtMDCSDate.MyLinkLable2 = Nothing
        Me.txtMDCSDate.Name = "txtMDCSDate"
        Me.txtMDCSDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMDCSDate.ReferenceFieldDesc = Nothing
        Me.txtMDCSDate.ReferenceFieldName = Nothing
        Me.txtMDCSDate.ReferenceTableName = Nothing
        Me.txtMDCSDate.Size = New System.Drawing.Size(90, 18)
        Me.txtMDCSDate.TabIndex = 28
        Me.txtMDCSDate.TabStop = False
        Me.txtMDCSDate.Text = "13/06/2011"
        Me.txtMDCSDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblMDCSDate
        '
        Me.lblMDCSDate.FieldName = Nothing
        Me.lblMDCSDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDCSDate.Location = New System.Drawing.Point(25, 23)
        Me.lblMDCSDate.Name = "lblMDCSDate"
        Me.lblMDCSDate.Size = New System.Drawing.Size(30, 16)
        Me.lblMDCSDate.TabIndex = 29
        Me.lblMDCSDate.Text = "Date"
        '
        'btnMGo
        '
        Me.btnMGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMGo.Location = New System.Drawing.Point(25, 42)
        Me.btnMGo.Name = "btnMGo"
        Me.btnMGo.Size = New System.Drawing.Size(128, 18)
        Me.btnMGo.TabIndex = 13
        Me.btnMGo.Text = "Get DCS by Mobile"
        '
        'txtTotPendingSNFPer
        '
        Me.txtTotPendingSNFPer.AutoSize = False
        Me.txtTotPendingSNFPer.BorderVisible = True
        Me.txtTotPendingSNFPer.FieldName = Nothing
        Me.txtTotPendingSNFPer.Location = New System.Drawing.Point(632, 67)
        Me.txtTotPendingSNFPer.Name = "txtTotPendingSNFPer"
        Me.txtTotPendingSNFPer.Size = New System.Drawing.Size(60, 20)
        Me.txtTotPendingSNFPer.TabIndex = 10
        Me.txtTotPendingSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotPendingSNFPer.TextWrap = False
        '
        'txtTotPendingFATPer
        '
        Me.txtTotPendingFATPer.AutoSize = False
        Me.txtTotPendingFATPer.BorderVisible = True
        Me.txtTotPendingFATPer.FieldName = Nothing
        Me.txtTotPendingFATPer.Location = New System.Drawing.Point(632, 46)
        Me.txtTotPendingFATPer.Name = "txtTotPendingFATPer"
        Me.txtTotPendingFATPer.Size = New System.Drawing.Size(60, 20)
        Me.txtTotPendingFATPer.TabIndex = 11
        Me.txtTotPendingFATPer.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotPendingFATPer.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(497, 6)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel1.TabIndex = 28
        Me.MyLabel1.Text = "FAT/SNF Type"
        '
        'cboFATSNFType
        '
        Me.cboFATSNFType.AutoCompleteDisplayMember = Nothing
        Me.cboFATSNFType.AutoCompleteValueMember = Nothing
        Me.cboFATSNFType.CalculationExpression = Nothing
        Me.cboFATSNFType.DropDownAnimationEnabled = True
        Me.cboFATSNFType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFATSNFType.Enabled = False
        Me.cboFATSNFType.FieldCode = Nothing
        Me.cboFATSNFType.FieldDesc = Nothing
        Me.cboFATSNFType.FieldMaxLength = 0
        Me.cboFATSNFType.FieldName = Nothing
        Me.cboFATSNFType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFATSNFType.isCalculatedField = False
        Me.cboFATSNFType.IsSourceFromTable = False
        Me.cboFATSNFType.IsSourceFromValueList = False
        Me.cboFATSNFType.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboFATSNFType.Items.Add(RadListDataItem1)
        Me.cboFATSNFType.Items.Add(RadListDataItem2)
        Me.cboFATSNFType.Location = New System.Drawing.Point(584, 5)
        Me.cboFATSNFType.MendatroryField = True
        Me.cboFATSNFType.MyLinkLable1 = Me.MyLabel1
        Me.cboFATSNFType.MyLinkLable2 = Nothing
        Me.cboFATSNFType.Name = "cboFATSNFType"
        Me.cboFATSNFType.ReferenceFieldDesc = Nothing
        Me.cboFATSNFType.ReferenceFieldName = Nothing
        Me.cboFATSNFType.ReferenceTableName = Nothing
        Me.cboFATSNFType.Size = New System.Drawing.Size(106, 18)
        Me.cboFATSNFType.TabIndex = 29
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(496, 27)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel7.TabIndex = 44
        Me.MyLabel7.Text = "SlipNo"
        '
        'txtSlipNo
        '
        Me.txtSlipNo.CalculationExpression = Nothing
        Me.txtSlipNo.FieldCode = Nothing
        Me.txtSlipNo.FieldDesc = Nothing
        Me.txtSlipNo.FieldMaxLength = 0
        Me.txtSlipNo.FieldName = Nothing
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.isCalculatedField = False
        Me.txtSlipNo.IsSourceFromTable = False
        Me.txtSlipNo.IsSourceFromValueList = False
        Me.txtSlipNo.IsUnique = False
        Me.txtSlipNo.Location = New System.Drawing.Point(544, 26)
        Me.txtSlipNo.MaxLength = 30
        Me.txtSlipNo.MendatroryField = False
        Me.txtSlipNo.MyLinkLable1 = Me.MyLabel7
        Me.txtSlipNo.MyLinkLable2 = Nothing
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.ReferenceFieldDesc = Nothing
        Me.txtSlipNo.ReferenceFieldName = Nothing
        Me.txtSlipNo.ReferenceTableName = Nothing
        Me.txtSlipNo.Size = New System.Drawing.Size(148, 18)
        Me.txtSlipNo.TabIndex = 2
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(694, 46)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(102, 41)
        Me.RadButton1.TabIndex = 12
        Me.RadButton1.Text = "Add To Suspense"
        '
        'lblTotPendingSNFKg
        '
        Me.lblTotPendingSNFKg.AutoSize = False
        Me.lblTotPendingSNFKg.BorderVisible = True
        Me.lblTotPendingSNFKg.FieldName = Nothing
        Me.lblTotPendingSNFKg.Location = New System.Drawing.Point(572, 67)
        Me.lblTotPendingSNFKg.Name = "lblTotPendingSNFKg"
        Me.lblTotPendingSNFKg.Size = New System.Drawing.Size(60, 20)
        Me.lblTotPendingSNFKg.TabIndex = 9
        Me.lblTotPendingSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTotPendingSNFKg.TextWrap = False
        '
        'lblTotReceivedSNFKg
        '
        Me.lblTotReceivedSNFKg.AutoSize = False
        Me.lblTotReceivedSNFKg.BorderVisible = True
        Me.lblTotReceivedSNFKg.FieldName = Nothing
        Me.lblTotReceivedSNFKg.Location = New System.Drawing.Point(512, 67)
        Me.lblTotReceivedSNFKg.Name = "lblTotReceivedSNFKg"
        Me.lblTotReceivedSNFKg.Size = New System.Drawing.Size(60, 20)
        Me.lblTotReceivedSNFKg.TabIndex = 8
        Me.lblTotReceivedSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTotReceivedSNFKg.TextWrap = False
        '
        'lblTotPendingFATKg
        '
        Me.lblTotPendingFATKg.AutoSize = False
        Me.lblTotPendingFATKg.BorderVisible = True
        Me.lblTotPendingFATKg.FieldName = Nothing
        Me.lblTotPendingFATKg.Location = New System.Drawing.Point(572, 46)
        Me.lblTotPendingFATKg.Name = "lblTotPendingFATKg"
        Me.lblTotPendingFATKg.Size = New System.Drawing.Size(60, 20)
        Me.lblTotPendingFATKg.TabIndex = 13
        Me.lblTotPendingFATKg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTotPendingFATKg.TextWrap = False
        '
        'lblTotReceivedFATKg
        '
        Me.lblTotReceivedFATKg.AutoSize = False
        Me.lblTotReceivedFATKg.BorderVisible = True
        Me.lblTotReceivedFATKg.FieldName = Nothing
        Me.lblTotReceivedFATKg.Location = New System.Drawing.Point(512, 46)
        Me.lblTotReceivedFATKg.Name = "lblTotReceivedFATKg"
        Me.lblTotReceivedFATKg.Size = New System.Drawing.Size(60, 20)
        Me.lblTotReceivedFATKg.TabIndex = 14
        Me.lblTotReceivedFATKg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTotReceivedFATKg.TextWrap = False
        '
        'lblTotPendingQty
        '
        Me.lblTotPendingQty.AutoSize = False
        Me.lblTotPendingQty.BorderVisible = True
        Me.lblTotPendingQty.FieldName = Nothing
        Me.lblTotPendingQty.Location = New System.Drawing.Point(226, 46)
        Me.lblTotPendingQty.Name = "lblTotPendingQty"
        Me.lblTotPendingQty.Size = New System.Drawing.Size(69, 20)
        Me.lblTotPendingQty.TabIndex = 19
        Me.lblTotPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTotPendingQty.TextWrap = False
        '
        'lblTotReceivedQty
        '
        Me.lblTotReceivedQty.AutoSize = False
        Me.lblTotReceivedQty.BorderVisible = True
        Me.lblTotReceivedQty.FieldName = Nothing
        Me.lblTotReceivedQty.Location = New System.Drawing.Point(154, 46)
        Me.lblTotReceivedQty.Name = "lblTotReceivedQty"
        Me.lblTotReceivedQty.Size = New System.Drawing.Size(72, 20)
        Me.lblTotReceivedQty.TabIndex = 20
        Me.lblTotReceivedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTotReceivedQty.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(298, 69)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel5.TabIndex = 4
        Me.MyLabel5.Text = "SNF %"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(397, 48)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel4.TabIndex = 16
        Me.MyLabel4.Text = "FAT KG"
        '
        'lblMCC
        '
        Me.lblMCC.AutoSize = False
        Me.lblMCC.BorderVisible = True
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Location = New System.Drawing.Point(298, 25)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(194, 20)
        Me.lblMCC.TabIndex = 15
        Me.lblMCC.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(6, 26)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel3.TabIndex = 23
        Me.MyLabel3.Text = "BMC/MCC"
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(85, 26)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel3
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(210, 18)
        Me.txtMCC.TabIndex = 1
        Me.txtMCC.Value = ""
        '
        'lblSNFPer
        '
        Me.lblSNFPer.AutoSize = False
        Me.lblSNFPer.BorderVisible = True
        Me.lblSNFPer.FieldName = Nothing
        Me.lblSNFPer.Location = New System.Drawing.Point(346, 67)
        Me.lblSNFPer.Name = "lblSNFPer"
        Me.lblSNFPer.Size = New System.Drawing.Size(48, 20)
        Me.lblSNFPer.TabIndex = 5
        Me.lblSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSNFPer.TextWrap = False
        '
        'lblFATKg
        '
        Me.lblFATKg.AutoSize = False
        Me.lblFATKg.BorderVisible = True
        Me.lblFATKg.FieldName = Nothing
        Me.lblFATKg.Location = New System.Drawing.Point(452, 46)
        Me.lblFATKg.Name = "lblFATKg"
        Me.lblFATKg.Size = New System.Drawing.Size(60, 20)
        Me.lblFATKg.TabIndex = 15
        Me.lblFATKg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFATKg.TextWrap = False
        '
        'lblSNFKg
        '
        Me.lblSNFKg.AutoSize = False
        Me.lblSNFKg.BorderVisible = True
        Me.lblSNFKg.FieldName = Nothing
        Me.lblSNFKg.Location = New System.Drawing.Point(452, 67)
        Me.lblSNFKg.Name = "lblSNFKg"
        Me.lblSNFKg.Size = New System.Drawing.Size(60, 20)
        Me.lblSNFKg.TabIndex = 7
        Me.lblSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSNFKg.TextWrap = False
        '
        'lblFATPer
        '
        Me.lblFATPer.AutoSize = False
        Me.lblFATPer.BorderVisible = True
        Me.lblFATPer.FieldName = Nothing
        Me.lblFATPer.Location = New System.Drawing.Point(346, 46)
        Me.lblFATPer.Name = "lblFATPer"
        Me.lblFATPer.Size = New System.Drawing.Size(48, 20)
        Me.lblFATPer.TabIndex = 17
        Me.lblFATPer.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFATPer.TextWrap = False
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(397, 69)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel23.TabIndex = 6
        Me.MyLabel23.Text = "SNF KG"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = False
        Me.lblQty.BorderVisible = True
        Me.lblQty.FieldName = Nothing
        Me.lblQty.Location = New System.Drawing.Point(85, 46)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(69, 20)
        Me.lblQty.TabIndex = 21
        Me.lblQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblQty.TextWrap = False
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(298, 48)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel15.TabIndex = 18
        Me.MyLabel15.Text = "FAT %"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(6, 48)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel10.TabIndex = 22
        Me.MyLabel10.Text = "Quantity"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(694, 5)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(102, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 30
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(6, 69)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 3
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(361, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 27
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 24
        Me.RadLabel1.Text = "Document No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(85, 5)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(250, 19)
        Me.txtDocNo.TabIndex = 25
        Me.txtDocNo.Value = ""
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(397, 5)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(90, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(85, 68)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(210, 18)
        Me.txtDesc.TabIndex = 3
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(335, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 26
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.SplitContainer1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 93)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1091, 292)
        Me.Panel2.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1091, 264)
        Me.SplitContainer1.SplitterDistance = 199
        Me.SplitContainer1.TabIndex = 3
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.EnableAlternatingRowColor = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.Size = New System.Drawing.Size(1091, 61)
        Me.gv2.TabIndex = 3
        Me.gv2.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnAddMissing)
        Me.Panel3.Controls.Add(Me.RadButton2)
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Controls.Add(Me.btnClose)
        Me.Panel3.Controls.Add(Me.btnHistory)
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Controls.Add(Me.btnPrint)
        Me.Panel3.Controls.Add(Me.btnPost)
        Me.Panel3.Controls.Add(Me.btnImport)
        Me.Panel3.Controls.Add(Me.btnExport)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 264)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1091, 28)
        Me.Panel3.TabIndex = 40
        '
        'btnAddMissing
        '
        Me.btnAddMissing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddMissing.Location = New System.Drawing.Point(684, 3)
        Me.btnAddMissing.Name = "btnAddMissing"
        Me.btnAddMissing.Size = New System.Drawing.Size(78, 22)
        Me.btnAddMissing.TabIndex = 41
        Me.btnAddMissing.Text = "Add Missing"
        Me.btnAddMissing.Visible = False
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(521, 3)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(161, 22)
        Me.RadButton2.TabIndex = 40
        Me.RadButton2.Text = "Add DCS To Current BMC"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1016, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 22)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(447, 3)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(72, 22)
        Me.btnHistory.TabIndex = 39
        Me.btnHistory.Text = "&History"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(77, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(373, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(72, 22)
        Me.btnPrint.TabIndex = 9
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(151, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(72, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImport.Location = New System.Drawing.Point(299, 3)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(72, 22)
        Me.btnImport.TabIndex = 8
        Me.btnImport.Text = "Import"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.Location = New System.Drawing.Point(225, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 22)
        Me.btnExport.TabIndex = 7
        Me.btnExport.Text = "Export"
        '
        'frmMilkCollectionDCS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1091, 385)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmMilkCollectionDCS"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DCS Milk Collection"
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtMDCSDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMDCSDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotPendingSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotPendingFATPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFATSNFType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSlipNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotPendingSNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotReceivedSNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotPendingFATKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotReceivedFATKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotPendingQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotReceivedQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFATKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFATPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.btnAddMissing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents gv1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblSNFPer As common.Controls.MyLabel
    Friend WithEvents lblFATKg As common.Controls.MyLabel
    Friend WithEvents lblSNFKg As common.Controls.MyLabel
    Friend WithEvents lblFATPer As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents lblQty As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblTotPendingQty As common.Controls.MyLabel
    Friend WithEvents lblTotReceivedQty As common.Controls.MyLabel
    Friend WithEvents lblTotPendingSNFKg As common.Controls.MyLabel
    Friend WithEvents lblTotReceivedSNFKg As common.Controls.MyLabel
    Friend WithEvents lblTotPendingFATKg As common.Controls.MyLabel
    Friend WithEvents lblTotReceivedFATKg As common.Controls.MyLabel
    Friend WithEvents btnImport As RadButton
    Friend WithEvents btnExport As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtSlipNo As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel3 As Panel
    Friend WithEvents gv2 As RadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cboFATSNFType As common.Controls.MyComboBox
    Friend WithEvents txtTotPendingSNFPer As common.Controls.MyLabel
    Friend WithEvents txtTotPendingFATPer As common.Controls.MyLabel
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents btnAddMissing As RadButton
    Friend WithEvents lblMDCSDate As common.Controls.MyLabel
    Friend WithEvents txtMDCSDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnMGo As RadButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
End Class

