<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkGradeMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblMilkType = New common.Controls.MyLabel()
        Me.lblMilkDescription = New common.Controls.MyLabel()
        Me.txtMilktypeCode = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.cmbMilkType = New common.Controls.MyComboBox()
        Me.MyLabel56 = New common.Controls.MyLabel()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImportHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImportdetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtSequenceNo = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel56, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSequenceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtSequenceNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lblMilkType)
        Me.RadGroupBox1.Controls.Add(Me.lblMilkDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtMilktypeCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.cmbMilkType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel56)
        Me.RadGroupBox1.Controls.Add(Me.fndCode)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.rbtnReset)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(563, 110)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblMilkType
        '
        Me.lblMilkType.AutoSize = False
        Me.lblMilkType.BorderVisible = True
        Me.lblMilkType.FieldName = Nothing
        Me.lblMilkType.Location = New System.Drawing.Point(401, 82)
        Me.lblMilkType.Name = "lblMilkType"
        Me.lblMilkType.Size = New System.Drawing.Size(151, 19)
        Me.lblMilkType.TabIndex = 1054
        Me.lblMilkType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMilkDescription
        '
        Me.lblMilkDescription.AutoSize = False
        Me.lblMilkDescription.BorderVisible = True
        Me.lblMilkDescription.FieldName = Nothing
        Me.lblMilkDescription.Location = New System.Drawing.Point(253, 82)
        Me.lblMilkDescription.Name = "lblMilkDescription"
        Me.lblMilkDescription.Size = New System.Drawing.Size(146, 20)
        Me.lblMilkDescription.TabIndex = 1053
        Me.lblMilkDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMilktypeCode
        '
        Me.txtMilktypeCode.CalculationExpression = Nothing
        Me.txtMilktypeCode.FieldCode = Nothing
        Me.txtMilktypeCode.FieldDesc = Nothing
        Me.txtMilktypeCode.FieldMaxLength = 0
        Me.txtMilktypeCode.FieldName = Nothing
        Me.txtMilktypeCode.isCalculatedField = False
        Me.txtMilktypeCode.IsSourceFromTable = False
        Me.txtMilktypeCode.IsSourceFromValueList = False
        Me.txtMilktypeCode.IsUnique = False
        Me.txtMilktypeCode.Location = New System.Drawing.Point(112, 82)
        Me.txtMilktypeCode.MendatroryField = True
        Me.txtMilktypeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMilktypeCode.MyLinkLable1 = Me.RadLabel2
        Me.txtMilktypeCode.MyLinkLable2 = Nothing
        Me.txtMilktypeCode.MyReadOnly = False
        Me.txtMilktypeCode.MyShowMasterFormButton = False
        Me.txtMilktypeCode.Name = "txtMilktypeCode"
        Me.txtMilktypeCode.ReferenceFieldDesc = Nothing
        Me.txtMilktypeCode.ReferenceFieldName = Nothing
        Me.txtMilktypeCode.ReferenceTableName = Nothing
        Me.txtMilktypeCode.Size = New System.Drawing.Size(136, 20)
        Me.txtMilktypeCode.TabIndex = 1051
        Me.txtMilktypeCode.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(16, 84)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 1052
        Me.RadLabel2.Text = "Milk Type"
        '
        'cmbMilkType
        '
        Me.cmbMilkType.AutoCompleteDisplayMember = Nothing
        Me.cmbMilkType.AutoCompleteValueMember = Nothing
        Me.cmbMilkType.CalculationExpression = Nothing
        Me.cmbMilkType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbMilkType.FieldCode = Nothing
        Me.cmbMilkType.FieldDesc = Nothing
        Me.cmbMilkType.FieldMaxLength = 0
        Me.cmbMilkType.FieldName = Nothing
        Me.cmbMilkType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMilkType.ForeColor = System.Drawing.Color.Lime
        Me.cmbMilkType.isCalculatedField = False
        Me.cmbMilkType.IsSourceFromTable = False
        Me.cmbMilkType.IsSourceFromValueList = False
        Me.cmbMilkType.IsUnique = False
        RadListDataItem1.Text = "COM1"
        RadListDataItem2.Text = "COM2"
        RadListDataItem3.Text = "COM3"
        RadListDataItem4.Text = "COM4"
        Me.cmbMilkType.Items.Add(RadListDataItem1)
        Me.cmbMilkType.Items.Add(RadListDataItem2)
        Me.cmbMilkType.Items.Add(RadListDataItem3)
        Me.cmbMilkType.Items.Add(RadListDataItem4)
        Me.cmbMilkType.Location = New System.Drawing.Point(113, 60)
        Me.cmbMilkType.MendatroryField = True
        Me.cmbMilkType.MyLinkLable1 = Me.MyLabel56
        Me.cmbMilkType.MyLinkLable2 = Nothing
        Me.cmbMilkType.Name = "cmbMilkType"
        Me.cmbMilkType.ReferenceFieldDesc = Nothing
        Me.cmbMilkType.ReferenceFieldName = Nothing
        Me.cmbMilkType.ReferenceTableName = Nothing
        '
        '
        '
        Me.cmbMilkType.RootElement.StretchVertically = True
        Me.cmbMilkType.Size = New System.Drawing.Size(135, 18)
        Me.cmbMilkType.TabIndex = 1049
        '
        'MyLabel56
        '
        Me.MyLabel56.FieldName = Nothing
        Me.MyLabel56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel56.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.MyLabel56.Location = New System.Drawing.Point(13, 62)
        Me.MyLabel56.Name = "MyLabel56"
        Me.MyLabel56.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel56.TabIndex = 1050
        Me.MyLabel56.Text = "Grade Type"
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(115, 15)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.lblCode
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(199, 21)
        Me.fndCode.TabIndex = 0
        Me.fndCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(13, 21)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(34, 16)
        Me.lblCode.TabIndex = 6
        Me.lblCode.Text = "Code"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 43)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 5
        Me.lblDescription.Text = "Description"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.Location = New System.Drawing.Point(319, 17)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(14, 18)
        Me.rbtnReset.TabIndex = 1
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(113, 39)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(220, 18)
        Me.txtDescription.TabIndex = 2
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(483, 7)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 2
        Me.rbtnClose.Text = "Close"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(91, 7)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 1
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(20, 7)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 0
        Me.rbtnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(563, 422)
        Me.SplitContainer1.SplitterDistance = 390
        Me.SplitContainer1.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Size = New System.Drawing.Size(563, 370)
        Me.SplitContainer2.SplitterDistance = 110
        Me.SplitContainer2.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gv)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(563, 256)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Chamber Desc"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(3, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(557, 235)
        Me.gv.TabIndex = 63
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(563, 20)
        Me.rdmenufile.TabIndex = 1
        Me.rdmenufile.Text = "File"
        '
        'RadMenufile
        '
        Me.RadMenufile.AccessibleDescription = "File"
        Me.RadMenufile.AccessibleName = "File"
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        '
        'rdmenuimport
        '
        Me.rdmenuimport.AccessibleDescription = "import"
        Me.rdmenuimport.AccessibleName = "import"
        Me.rdmenuimport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnImportHead, Me.btnImportdetail})
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        '
        'btnImportHead
        '
        Me.btnImportHead.AccessibleDescription = "RadMenuItem1"
        Me.btnImportHead.AccessibleName = "RadMenuItem1"
        Me.btnImportHead.Name = "btnImportHead"
        Me.btnImportHead.Text = "Import Head"
        '
        'btnImportdetail
        '
        Me.btnImportdetail.AccessibleDescription = "RadMenuItem2"
        Me.btnImportdetail.AccessibleName = "RadMenuItem2"
        Me.btnImportdetail.Name = "btnImportdetail"
        Me.btnImportdetail.Text = "Import Detail"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "Export"
        Me.rdmenuexport.AccessibleName = "Export"
        Me.rdmenuexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExportHead, Me.btnExportDetail})
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        '
        'btnExportHead
        '
        Me.btnExportHead.AccessibleDescription = "Export Head"
        Me.btnExportHead.AccessibleName = "Export Head"
        Me.btnExportHead.Name = "btnExportHead"
        Me.btnExportHead.Text = "Export Head"
        '
        'btnExportDetail
        '
        Me.btnExportDetail.AccessibleDescription = "Export Detail"
        Me.btnExportDetail.AccessibleName = "Export Detail"
        Me.btnExportDetail.Name = "btnExportDetail"
        Me.btnExportDetail.Text = "Export Detail"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.AccessibleDescription = "Exit"
        Me.rdmenuexit.AccessibleName = "Exit"
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'txtSequenceNo
        '
        Me.txtSequenceNo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSequenceNo.CalculationExpression = Nothing
        Me.txtSequenceNo.DecimalPlaces = 0
        Me.txtSequenceNo.FieldCode = Nothing
        Me.txtSequenceNo.FieldDesc = Nothing
        Me.txtSequenceNo.FieldMaxLength = 0
        Me.txtSequenceNo.FieldName = Nothing
        Me.txtSequenceNo.isCalculatedField = False
        Me.txtSequenceNo.IsSourceFromTable = False
        Me.txtSequenceNo.IsSourceFromValueList = False
        Me.txtSequenceNo.IsUnique = False
        Me.txtSequenceNo.Location = New System.Drawing.Point(335, 60)
        Me.txtSequenceNo.MendatroryField = False
        Me.txtSequenceNo.MyLinkLable1 = Me.MyLabel2
        Me.txtSequenceNo.MyLinkLable2 = Nothing
        Me.txtSequenceNo.Name = "txtSequenceNo"
        Me.txtSequenceNo.ReferenceFieldDesc = Nothing
        Me.txtSequenceNo.ReferenceFieldName = Nothing
        Me.txtSequenceNo.ReferenceTableName = Nothing
        Me.txtSequenceNo.Size = New System.Drawing.Size(64, 20)
        Me.txtSequenceNo.TabIndex = 1055
        Me.txtSequenceNo.Text = "0"
        Me.txtSequenceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSequenceNo.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(254, 63)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel2.TabIndex = 1056
        Me.MyLabel2.Text = "Sequence No"
        '
        'frmMilkGradeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 422)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMilkGradeMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Grade Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel56, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSequenceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cmbMilkType As common.Controls.MyComboBox
    Friend WithEvents MyLabel56 As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnImportHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImportdetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblMilkDescription As common.Controls.MyLabel
    Friend WithEvents txtMilktypeCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblMilkType As common.Controls.MyLabel
    Friend WithEvents txtSequenceNo As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

