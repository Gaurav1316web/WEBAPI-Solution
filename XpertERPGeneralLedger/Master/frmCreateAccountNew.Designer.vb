<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateAccountNew
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.rdgrpbxacctstruct = New Telerik.WinControls.UI.RadGroupBox()
        Me.ChkCntrlAcc = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndcreateacctwithstrcode = New common.UserControls.txtFinder()
        Me.rdlblcreateacctwithstruct = New common.Controls.MyLabel()
        Me.fndfromaccount = New common.UserControls.txtFinder()
        Me.rdlblfromacctwithstruct = New common.Controls.MyLabel()
        Me.rdtxtcreateaccctwithstrcode = New common.Controls.MyTextBox()
        Me.rdtxtfromaccountwithstrcode = New common.Controls.MyTextBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.rdbtnpreview = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnprocess = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnClear = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gbVehicle = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgMainGLAccount = New common.MyCheckBoxGrid()
        CType(Me.rdgrpbxacctstruct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxacctstruct.SuspendLayout()
        CType(Me.ChkCntrlAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblcreateacctwithstruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblfromacctwithstruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtcreateaccctwithstrcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtfromaccountwithstrcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnpreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnprocess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnClear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gbVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVehicle.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdgrpbxacctstruct
        '
        Me.rdgrpbxacctstruct.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxacctstruct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdgrpbxacctstruct.Controls.Add(Me.ChkCntrlAcc)
        Me.rdgrpbxacctstruct.Controls.Add(Me.fndcreateacctwithstrcode)
        Me.rdgrpbxacctstruct.Controls.Add(Me.fndfromaccount)
        Me.rdgrpbxacctstruct.Controls.Add(Me.rdtxtcreateaccctwithstrcode)
        Me.rdgrpbxacctstruct.Controls.Add(Me.rdtxtfromaccountwithstrcode)
        Me.rdgrpbxacctstruct.Controls.Add(Me.rdlblcreateacctwithstruct)
        Me.rdgrpbxacctstruct.Controls.Add(Me.rdlblfromacctwithstruct)
        Me.rdgrpbxacctstruct.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxacctstruct.HeaderText = "Select Account Structures"
        Me.rdgrpbxacctstruct.Location = New System.Drawing.Point(19, 10)
        Me.rdgrpbxacctstruct.Name = "rdgrpbxacctstruct"
        Me.rdgrpbxacctstruct.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxacctstruct.Size = New System.Drawing.Size(750, 41)
        Me.rdgrpbxacctstruct.TabIndex = 0
        Me.rdgrpbxacctstruct.Text = "Select Account Structures"
        '
        'ChkCntrlAcc
        '
        Me.ChkCntrlAcc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCntrlAcc.Location = New System.Drawing.Point(641, 14)
        Me.ChkCntrlAcc.Name = "ChkCntrlAcc"
        Me.ChkCntrlAcc.Size = New System.Drawing.Size(101, 16)
        Me.ChkCntrlAcc.TabIndex = 11
        Me.ChkCntrlAcc.Text = "Control Account"
        Me.ChkCntrlAcc.Visible = False
        '
        'fndcreateacctwithstrcode
        '
        Me.fndcreateacctwithstrcode.CalculationExpression = Nothing
        Me.fndcreateacctwithstrcode.FieldCode = Nothing
        Me.fndcreateacctwithstrcode.FieldDesc = Nothing
        Me.fndcreateacctwithstrcode.FieldMaxLength = 0
        Me.fndcreateacctwithstrcode.FieldName = Nothing
        Me.fndcreateacctwithstrcode.isCalculatedField = False
        Me.fndcreateacctwithstrcode.IsSourceFromTable = False
        Me.fndcreateacctwithstrcode.IsSourceFromValueList = False
        Me.fndcreateacctwithstrcode.IsUnique = False
        Me.fndcreateacctwithstrcode.Location = New System.Drawing.Point(196, 13)
        Me.fndcreateacctwithstrcode.MendatroryField = True
        Me.fndcreateacctwithstrcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcreateacctwithstrcode.MyLinkLable1 = Me.rdlblcreateacctwithstruct
        Me.fndcreateacctwithstrcode.MyLinkLable2 = Nothing
        Me.fndcreateacctwithstrcode.MyReadOnly = False
        Me.fndcreateacctwithstrcode.MyShowMasterFormButton = False
        Me.fndcreateacctwithstrcode.Name = "fndcreateacctwithstrcode"
        Me.fndcreateacctwithstrcode.ReferenceFieldDesc = Nothing
        Me.fndcreateacctwithstrcode.ReferenceFieldName = Nothing
        Me.fndcreateacctwithstrcode.ReferenceTableName = Nothing
        Me.fndcreateacctwithstrcode.Size = New System.Drawing.Size(115, 20)
        Me.fndcreateacctwithstrcode.TabIndex = 1
        Me.fndcreateacctwithstrcode.Value = ""
        '
        'rdlblcreateacctwithstruct
        '
        Me.rdlblcreateacctwithstruct.FieldName = Nothing
        Me.rdlblcreateacctwithstruct.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblcreateacctwithstruct.Location = New System.Drawing.Point(5, 15)
        Me.rdlblcreateacctwithstruct.Name = "rdlblcreateacctwithstruct"
        Me.rdlblcreateacctwithstruct.Size = New System.Drawing.Size(187, 16)
        Me.rdlblcreateacctwithstruct.TabIndex = 1
        Me.rdlblcreateacctwithstruct.Text = "Create Account with Structure Code"
        '
        'fndfromaccount
        '
        Me.fndfromaccount.CalculationExpression = Nothing
        Me.fndfromaccount.FieldCode = Nothing
        Me.fndfromaccount.FieldDesc = Nothing
        Me.fndfromaccount.FieldMaxLength = 0
        Me.fndfromaccount.FieldName = Nothing
        Me.fndfromaccount.isCalculatedField = False
        Me.fndfromaccount.IsSourceFromTable = False
        Me.fndfromaccount.IsSourceFromValueList = False
        Me.fndfromaccount.IsUnique = False
        Me.fndfromaccount.Location = New System.Drawing.Point(197, 42)
        Me.fndfromaccount.MendatroryField = True
        Me.fndfromaccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndfromaccount.MyLinkLable1 = Me.rdlblfromacctwithstruct
        Me.fndfromaccount.MyLinkLable2 = Nothing
        Me.fndfromaccount.MyReadOnly = False
        Me.fndfromaccount.MyShowMasterFormButton = False
        Me.fndfromaccount.Name = "fndfromaccount"
        Me.fndfromaccount.ReferenceFieldDesc = Nothing
        Me.fndfromaccount.ReferenceFieldName = Nothing
        Me.fndfromaccount.ReferenceTableName = Nothing
        Me.fndfromaccount.Size = New System.Drawing.Size(115, 20)
        Me.fndfromaccount.TabIndex = 0
        Me.fndfromaccount.Value = ""
        '
        'rdlblfromacctwithstruct
        '
        Me.rdlblfromacctwithstruct.FieldName = Nothing
        Me.rdlblfromacctwithstruct.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rdlblfromacctwithstruct.Location = New System.Drawing.Point(5, 42)
        Me.rdlblfromacctwithstruct.Name = "rdlblfromacctwithstruct"
        Me.rdlblfromacctwithstruct.Size = New System.Drawing.Size(194, 16)
        Me.rdlblfromacctwithstruct.TabIndex = 0
        Me.rdlblfromacctwithstruct.Text = "From Account with Structure Code"
        '
        'rdtxtcreateaccctwithstrcode
        '
        Me.rdtxtcreateaccctwithstrcode.CalculationExpression = Nothing
        Me.rdtxtcreateaccctwithstrcode.FieldCode = Nothing
        Me.rdtxtcreateaccctwithstrcode.FieldDesc = Nothing
        Me.rdtxtcreateaccctwithstrcode.FieldMaxLength = 0
        Me.rdtxtcreateaccctwithstrcode.FieldName = Nothing
        Me.rdtxtcreateaccctwithstrcode.isCalculatedField = False
        Me.rdtxtcreateaccctwithstrcode.IsSourceFromTable = False
        Me.rdtxtcreateaccctwithstrcode.IsSourceFromValueList = False
        Me.rdtxtcreateaccctwithstrcode.IsUnique = False
        Me.rdtxtcreateaccctwithstrcode.Location = New System.Drawing.Point(322, 13)
        Me.rdtxtcreateaccctwithstrcode.MendatroryField = False
        Me.rdtxtcreateaccctwithstrcode.MyLinkLable1 = Nothing
        Me.rdtxtcreateaccctwithstrcode.MyLinkLable2 = Nothing
        Me.rdtxtcreateaccctwithstrcode.Name = "rdtxtcreateaccctwithstrcode"
        Me.rdtxtcreateaccctwithstrcode.ReferenceFieldDesc = Nothing
        Me.rdtxtcreateaccctwithstrcode.ReferenceFieldName = Nothing
        Me.rdtxtcreateaccctwithstrcode.ReferenceTableName = Nothing
        Me.rdtxtcreateaccctwithstrcode.Size = New System.Drawing.Size(308, 20)
        Me.rdtxtcreateaccctwithstrcode.TabIndex = 3
        '
        'rdtxtfromaccountwithstrcode
        '
        Me.rdtxtfromaccountwithstrcode.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.rdtxtfromaccountwithstrcode.CalculationExpression = Nothing
        Me.rdtxtfromaccountwithstrcode.FieldCode = Nothing
        Me.rdtxtfromaccountwithstrcode.FieldDesc = Nothing
        Me.rdtxtfromaccountwithstrcode.FieldMaxLength = 0
        Me.rdtxtfromaccountwithstrcode.FieldName = Nothing
        Me.rdtxtfromaccountwithstrcode.isCalculatedField = False
        Me.rdtxtfromaccountwithstrcode.IsSourceFromTable = False
        Me.rdtxtfromaccountwithstrcode.IsSourceFromValueList = False
        Me.rdtxtfromaccountwithstrcode.IsUnique = False
        Me.rdtxtfromaccountwithstrcode.Location = New System.Drawing.Point(322, 42)
        Me.rdtxtfromaccountwithstrcode.MendatroryField = False
        Me.rdtxtfromaccountwithstrcode.MyLinkLable1 = Nothing
        Me.rdtxtfromaccountwithstrcode.MyLinkLable2 = Nothing
        Me.rdtxtfromaccountwithstrcode.Name = "rdtxtfromaccountwithstrcode"
        Me.rdtxtfromaccountwithstrcode.ReferenceFieldDesc = Nothing
        Me.rdtxtfromaccountwithstrcode.ReferenceFieldName = Nothing
        Me.rdtxtfromaccountwithstrcode.ReferenceTableName = Nothing
        Me.rdtxtfromaccountwithstrcode.Size = New System.Drawing.Size(308, 20)
        Me.rdtxtfromaccountwithstrcode.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(788, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'rdmenuimport
        '
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Segment Name"
        GridViewTextBoxColumn1.Name = "colsegmentname"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 181
        Me.gv1.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1})
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(730, 118)
        Me.gv1.TabIndex = 3
        Me.gv1.TabStop = False
        '
        'rdbtnpreview
        '
        Me.rdbtnpreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnpreview.Location = New System.Drawing.Point(4, 2)
        Me.rdbtnpreview.Name = "rdbtnpreview"
        Me.rdbtnpreview.Size = New System.Drawing.Size(78, 23)
        Me.rdbtnpreview.TabIndex = 0
        Me.rdbtnpreview.Text = "Preview"
        '
        'rdbtnprocess
        '
        Me.rdbtnprocess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnprocess.Location = New System.Drawing.Point(85, 2)
        Me.rdbtnprocess.Name = "rdbtnprocess"
        Me.rdbtnprocess.Size = New System.Drawing.Size(78, 23)
        Me.rdbtnprocess.TabIndex = 1
        Me.rdbtnprocess.Text = "Process"
        '
        'rdbtnClear
        '
        Me.rdbtnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnClear.Location = New System.Drawing.Point(165, 2)
        Me.rdbtnClear.Name = "rdbtnClear"
        Me.rdbtnClear.Size = New System.Drawing.Size(78, 23)
        Me.rdbtnClear.TabIndex = 2
        Me.rdbtnClear.Text = "Clear"
        '
        'rdbtnClose
        '
        Me.rdbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnClose.Location = New System.Drawing.Point(690, 2)
        Me.rdbtnClose.Name = "rdbtnClose"
        Me.rdbtnClose.Size = New System.Drawing.Size(92, 23)
        Me.rdbtnClose.TabIndex = 3
        Me.rdbtnClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbVehicle)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgrpbxacctstruct)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnprocess)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnpreview)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnClear)
        Me.SplitContainer1.Size = New System.Drawing.Size(788, 487)
        Me.SplitContainer1.SplitterDistance = 455
        Me.SplitContainer1.TabIndex = 7
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(626, 436)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(156, 16)
        Me.RadLabel12.TabIndex = 74
        Me.RadLabel12.Text = "Double click to set segment"
        Me.RadLabel12.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Specify the segments to create"
        Me.RadGroupBox1.Location = New System.Drawing.Point(19, 282)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(750, 148)
        Me.RadGroupBox1.TabIndex = 4
        Me.RadGroupBox1.Text = "Specify the segments to create"
        '
        'gbVehicle
        '
        Me.gbVehicle.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVehicle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbVehicle.Controls.Add(Me.cbgMainGLAccount)
        Me.gbVehicle.HeaderText = "GL Main Account"
        Me.gbVehicle.Location = New System.Drawing.Point(19, 52)
        Me.gbVehicle.Name = "gbVehicle"
        Me.gbVehicle.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbVehicle.Size = New System.Drawing.Size(750, 230)
        Me.gbVehicle.TabIndex = 73
        Me.gbVehicle.Text = "GL Main Account"
        '
        'cbgMainGLAccount
        '
        Me.cbgMainGLAccount.CheckedValue = Nothing
        Me.cbgMainGLAccount.DataSource = Nothing
        Me.cbgMainGLAccount.DisplayMember = "Name"
        Me.cbgMainGLAccount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMainGLAccount.Location = New System.Drawing.Point(10, 20)
        Me.cbgMainGLAccount.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMainGLAccount.MyShowHeadrText = False
        Me.cbgMainGLAccount.Name = "cbgMainGLAccount"
        Me.cbgMainGLAccount.Size = New System.Drawing.Size(730, 200)
        Me.cbgMainGLAccount.TabIndex = 1
        Me.cbgMainGLAccount.ValueMember = "Code"
        '
        'frmCreateAccountNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(788, 507)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCreateAccountNew"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Create Accounts"
        CType(Me.rdgrpbxacctstruct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxacctstruct.ResumeLayout(False)
        Me.rdgrpbxacctstruct.PerformLayout()
        CType(Me.ChkCntrlAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblcreateacctwithstruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblfromacctwithstruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtcreateaccctwithstrcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtfromaccountwithstrcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnpreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnprocess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnClear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gbVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVehicle.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdgrpbxacctstruct As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdtxtcreateaccctwithstrcode As common.Controls.MyTextBox
    Friend WithEvents rdtxtfromaccountwithstrcode As common.Controls.MyTextBox
    Friend WithEvents rddgvcreateaccount As common.UserControls.MyRadGridView
    Friend WithEvents rdbtnpreview As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnprocess As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnClear As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents fndfromaccount As common.UserControls.txtFinder
    Friend WithEvents fndcreateacctwithstrcode As common.UserControls.txtFinder
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents rdlblcreateacctwithstruct As common.Controls.MyLabel
    Friend WithEvents rdlblfromacctwithstruct As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gbVehicle As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgMainGLAccount As common.MyCheckBoxGrid
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents ChkCntrlAcc As Telerik.WinControls.UI.RadCheckBox
End Class

