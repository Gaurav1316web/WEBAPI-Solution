<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomFieldEditor
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkButtons = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblFieldDescription = New common.Controls.MyLabel()
        Me.lblFieldName = New common.Controls.MyLabel()
        Me.fndCustomFieldName = New common.UserControls.txtFinder()
        Me.lblbacc = New common.Controls.MyLabel()
        Me.chkCalculatedField = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkManualEntryField = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.chkIsForPrint = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIsForDetailLevel = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtFieldHeight = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.chkIsUnique = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtMaxLength = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.isMandatory = New Telerik.WinControls.UI.RadCheckBox()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.chkManualList = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFromTable = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndFieldName = New common.UserControls.txtFinder()
        Me.fndReferenceTable = New common.UserControls.txtFinder()
        Me.gv3 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnEditExpression = New Telerik.WinControls.UI.RadButton()
        Me.txtExpression = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblMethodDesc = New common.Controls.MyLabel()
        Me.fndMethodCode = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.gv4 = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFieldDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFieldName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbacc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCalculatedField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkManualEntryField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.chkIsForPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsForDetailLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsUnique, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaxLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.isMandatory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.chkManualList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFromTable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.btnEditExpression, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpression, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.lblMethodDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.gv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv4.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(869, 477)
        Me.SplitContainer1.SplitterDistance = 448
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkButtons)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFieldDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFieldName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCustomFieldName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblbacc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkCalculatedField)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkManualEntryField)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(869, 448)
        Me.SplitContainer2.SplitterDistance = 32
        Me.SplitContainer2.TabIndex = 1
        '
        'chkButtons
        '
        Me.chkButtons.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkButtons.Location = New System.Drawing.Point(204, 9)
        Me.chkButtons.Name = "chkButtons"
        Me.chkButtons.Size = New System.Drawing.Size(59, 16)
        Me.chkButtons.TabIndex = 27
        Me.chkButtons.Text = "Buttons"
        '
        'lblFieldDescription
        '
        Me.lblFieldDescription.BorderVisible = True
        Me.lblFieldDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFieldDescription.Location = New System.Drawing.Point(637, 8)
        Me.lblFieldDescription.Name = "lblFieldDescription"
        Me.lblFieldDescription.Size = New System.Drawing.Size(2, 2)
        Me.lblFieldDescription.TabIndex = 26
        '
        'lblFieldName
        '
        Me.lblFieldName.BorderVisible = True
        Me.lblFieldName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFieldName.Location = New System.Drawing.Point(494, 8)
        Me.lblFieldName.Name = "lblFieldName"
        Me.lblFieldName.Size = New System.Drawing.Size(2, 2)
        Me.lblFieldName.TabIndex = 25
        '
        'fndCustomFieldName
        '
        Me.fndCustomFieldName.CalculationExpression = Nothing
        Me.fndCustomFieldName.FieldCode = Nothing
        Me.fndCustomFieldName.FieldDesc = Nothing
        Me.fndCustomFieldName.FieldMaxLength = 0
        Me.fndCustomFieldName.FieldName = Nothing
        Me.fndCustomFieldName.isCalculatedField = False
        Me.fndCustomFieldName.IsSourceFromTable = False
        Me.fndCustomFieldName.IsSourceFromValueList = False
        Me.fndCustomFieldName.IsUnique = False
        Me.fndCustomFieldName.Location = New System.Drawing.Point(323, 7)
        Me.fndCustomFieldName.MendatroryField = True
        Me.fndCustomFieldName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomFieldName.MyLinkLable1 = Me.lblbacc
        Me.fndCustomFieldName.MyLinkLable2 = Nothing
        Me.fndCustomFieldName.MyReadOnly = False
        Me.fndCustomFieldName.MyShowMasterFormButton = False
        Me.fndCustomFieldName.Name = "fndCustomFieldName"
        Me.fndCustomFieldName.ReferenceFieldDesc = Nothing
        Me.fndCustomFieldName.ReferenceFieldName = Nothing
        Me.fndCustomFieldName.ReferenceTableName = Nothing
        Me.fndCustomFieldName.Size = New System.Drawing.Size(175, 18)
        Me.fndCustomFieldName.TabIndex = 23
        Me.fndCustomFieldName.Value = ""
        '
        'lblbacc
        '
        Me.lblbacc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbacc.Location = New System.Drawing.Point(260, 9)
        Me.lblbacc.Name = "lblbacc"
        Me.lblbacc.Size = New System.Drawing.Size(65, 16)
        Me.lblbacc.TabIndex = 24
        Me.lblbacc.Text = "Select Field"
        '
        'chkCalculatedField
        '
        Me.chkCalculatedField.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCalculatedField.Location = New System.Drawing.Point(108, 9)
        Me.chkCalculatedField.Name = "chkCalculatedField"
        Me.chkCalculatedField.Size = New System.Drawing.Size(101, 16)
        Me.chkCalculatedField.TabIndex = 22
        Me.chkCalculatedField.Text = "Calculated Field"
        '
        'chkManualEntryField
        '
        Me.chkManualEntryField.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkManualEntryField.Location = New System.Drawing.Point(2, 9)
        Me.chkManualEntryField.Name = "chkManualEntryField"
        Me.chkManualEntryField.Size = New System.Drawing.Size(115, 16)
        Me.chkManualEntryField.TabIndex = 21
        Me.chkManualEntryField.Text = "Manual Entry Field"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage4
        Me.RadPageView1.Size = New System.Drawing.Size(869, 412)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.ItemList
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoScroll = True
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(848, 364)
        Me.RadPageViewPage1.Text = "General"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkIsForPrint)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkIsForDetailLevel)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtFieldHeight)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkIsUnique)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtMaxLength)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.isMandatory)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gv2)
        Me.SplitContainer3.Size = New System.Drawing.Size(848, 364)
        Me.SplitContainer3.SplitterDistance = 33
        Me.SplitContainer3.TabIndex = 0
        '
        'chkIsForPrint
        '
        Me.chkIsForPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsForPrint.Location = New System.Drawing.Point(577, 9)
        Me.chkIsForPrint.Name = "chkIsForPrint"
        Me.chkIsForPrint.Size = New System.Drawing.Size(76, 16)
        Me.chkIsForPrint.TabIndex = 15
        Me.chkIsForPrint.Text = "Is For Print"
        '
        'chkIsForDetailLevel
        '
        Me.chkIsForDetailLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsForDetailLevel.Location = New System.Drawing.Point(460, 8)
        Me.chkIsForDetailLevel.Name = "chkIsForDetailLevel"
        Me.chkIsForDetailLevel.Size = New System.Drawing.Size(111, 16)
        Me.chkIsForDetailLevel.TabIndex = 14
        Me.chkIsForDetailLevel.Text = "Is For Detail Level"
        '
        'txtFieldHeight
        '
        Me.txtFieldHeight.CalculationExpression = Nothing
        Me.txtFieldHeight.FieldCode = Nothing
        Me.txtFieldHeight.FieldDesc = Nothing
        Me.txtFieldHeight.FieldMaxLength = 0
        Me.txtFieldHeight.FieldName = Nothing
        Me.txtFieldHeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFieldHeight.isCalculatedField = False
        Me.txtFieldHeight.IsSourceFromTable = False
        Me.txtFieldHeight.IsSourceFromValueList = False
        Me.txtFieldHeight.IsUnique = False
        Me.txtFieldHeight.Location = New System.Drawing.Point(381, 7)
        Me.txtFieldHeight.MaxLength = 50
        Me.txtFieldHeight.MendatroryField = False
        Me.txtFieldHeight.MyLinkLable1 = Me.MyLabel5
        Me.txtFieldHeight.MyLinkLable2 = Nothing
        Me.txtFieldHeight.Name = "txtFieldHeight"
        Me.txtFieldHeight.ReferenceFieldDesc = Nothing
        Me.txtFieldHeight.ReferenceFieldName = Nothing
        Me.txtFieldHeight.ReferenceTableName = Nothing
        Me.txtFieldHeight.Size = New System.Drawing.Size(70, 18)
        Me.txtFieldHeight.TabIndex = 12
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(314, 8)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel5.TabIndex = 13
        Me.MyLabel5.Text = "Field Height"
        '
        'chkIsUnique
        '
        Me.chkIsUnique.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsUnique.Location = New System.Drawing.Point(251, 9)
        Me.chkIsUnique.Name = "chkIsUnique"
        Me.chkIsUnique.Size = New System.Drawing.Size(56, 16)
        Me.chkIsUnique.TabIndex = 11
        Me.chkIsUnique.Text = "Unique"
        '
        'txtMaxLength
        '
        Me.txtMaxLength.CalculationExpression = Nothing
        Me.txtMaxLength.FieldCode = Nothing
        Me.txtMaxLength.FieldDesc = Nothing
        Me.txtMaxLength.FieldMaxLength = 0
        Me.txtMaxLength.FieldName = Nothing
        Me.txtMaxLength.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxLength.isCalculatedField = False
        Me.txtMaxLength.IsSourceFromTable = False
        Me.txtMaxLength.IsSourceFromValueList = False
        Me.txtMaxLength.IsUnique = False
        Me.txtMaxLength.Location = New System.Drawing.Point(73, 7)
        Me.txtMaxLength.MaxLength = 50
        Me.txtMaxLength.MendatroryField = False
        Me.txtMaxLength.MyLinkLable1 = Me.MyLabel1
        Me.txtMaxLength.MyLinkLable2 = Nothing
        Me.txtMaxLength.Name = "txtMaxLength"
        Me.txtMaxLength.ReferenceFieldDesc = Nothing
        Me.txtMaxLength.ReferenceFieldName = Nothing
        Me.txtMaxLength.ReferenceTableName = Nothing
        Me.txtMaxLength.Size = New System.Drawing.Size(70, 18)
        Me.txtMaxLength.TabIndex = 7
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 7)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "Max Length"
        '
        'isMandatory
        '
        Me.isMandatory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isMandatory.Location = New System.Drawing.Point(145, 8)
        Me.isMandatory.Name = "isMandatory"
        Me.isMandatory.Size = New System.Drawing.Size(86, 16)
        Me.isMandatory.TabIndex = 9
        Me.isMandatory.Text = "Is Mandatory"
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.White
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        'gv2
        '
        Me.gv2.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv2.MasterTemplate.AllowDragToGroup = False
        Me.gv2.MasterTemplate.EnableGrouping = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ShowRowHeaderColumn = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(848, 327)
        Me.gv2.TabIndex = 20
        Me.gv2.TabStop = False
        Me.gv2.Text = "GV Load Out"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(66.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(848, 364)
        Me.RadPageViewPage2.Text = "Advanced"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.SplitContainer4)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(848, 364)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "DataSource"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.IsSplitterFixed = True
        Me.SplitContainer4.Location = New System.Drawing.Point(3, 18)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.SplitContainer5)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.gv3)
        Me.SplitContainer4.Size = New System.Drawing.Size(842, 343)
        Me.SplitContainer4.SplitterDistance = 98
        Me.SplitContainer4.TabIndex = 0
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer5.IsSplitterFixed = True
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.chkManualList)
        Me.SplitContainer5.Panel1.Controls.Add(Me.chkFromTable)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer5.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer5.Panel2.Controls.Add(Me.fndFieldName)
        Me.SplitContainer5.Panel2.Controls.Add(Me.fndReferenceTable)
        Me.SplitContainer5.Size = New System.Drawing.Size(842, 98)
        Me.SplitContainer5.SplitterDistance = 25
        Me.SplitContainer5.TabIndex = 0
        '
        'chkManualList
        '
        Me.chkManualList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkManualList.Location = New System.Drawing.Point(102, 6)
        Me.chkManualList.Name = "chkManualList"
        Me.chkManualList.Size = New System.Drawing.Size(131, 16)
        Me.chkManualList.TabIndex = 20
        Me.chkManualList.Text = "Manual List Of Values"
        '
        'chkFromTable
        '
        Me.chkFromTable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFromTable.Location = New System.Drawing.Point(18, 6)
        Me.chkFromTable.Name = "chkFromTable"
        Me.chkFromTable.Size = New System.Drawing.Size(78, 16)
        Me.chkFromTable.TabIndex = 19
        Me.chkFromTable.Text = "From Table"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(18, 10)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel2.TabIndex = 13
        Me.MyLabel2.Text = "Reference Table"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(18, 31)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 15
        Me.MyLabel3.Text = "Field"
        '
        'fndFieldName
        '
        Me.fndFieldName.CalculationExpression = Nothing
        Me.fndFieldName.FieldCode = Nothing
        Me.fndFieldName.FieldDesc = Nothing
        Me.fndFieldName.FieldMaxLength = 0
        Me.fndFieldName.FieldName = Nothing
        Me.fndFieldName.isCalculatedField = False
        Me.fndFieldName.IsSourceFromTable = False
        Me.fndFieldName.IsSourceFromValueList = False
        Me.fndFieldName.IsUnique = False
        Me.fndFieldName.Location = New System.Drawing.Point(108, 32)
        Me.fndFieldName.MendatroryField = True
        Me.fndFieldName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFieldName.MyLinkLable1 = Me.MyLabel3
        Me.fndFieldName.MyLinkLable2 = Nothing
        Me.fndFieldName.MyReadOnly = False
        Me.fndFieldName.MyShowMasterFormButton = False
        Me.fndFieldName.Name = "fndFieldName"
        Me.fndFieldName.ReferenceFieldDesc = Nothing
        Me.fndFieldName.ReferenceFieldName = Nothing
        Me.fndFieldName.ReferenceTableName = Nothing
        Me.fndFieldName.Size = New System.Drawing.Size(281, 18)
        Me.fndFieldName.TabIndex = 14
        Me.fndFieldName.Value = ""
        '
        'fndReferenceTable
        '
        Me.fndReferenceTable.CalculationExpression = Nothing
        Me.fndReferenceTable.FieldCode = Nothing
        Me.fndReferenceTable.FieldDesc = Nothing
        Me.fndReferenceTable.FieldMaxLength = 0
        Me.fndReferenceTable.FieldName = Nothing
        Me.fndReferenceTable.isCalculatedField = False
        Me.fndReferenceTable.IsSourceFromTable = False
        Me.fndReferenceTable.IsSourceFromValueList = False
        Me.fndReferenceTable.IsUnique = False
        Me.fndReferenceTable.Location = New System.Drawing.Point(108, 10)
        Me.fndReferenceTable.MendatroryField = True
        Me.fndReferenceTable.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndReferenceTable.MyLinkLable1 = Me.MyLabel2
        Me.fndReferenceTable.MyLinkLable2 = Nothing
        Me.fndReferenceTable.MyReadOnly = False
        Me.fndReferenceTable.MyShowMasterFormButton = False
        Me.fndReferenceTable.Name = "fndReferenceTable"
        Me.fndReferenceTable.ReferenceFieldDesc = Nothing
        Me.fndReferenceTable.ReferenceFieldName = Nothing
        Me.fndReferenceTable.ReferenceTableName = Nothing
        Me.fndReferenceTable.Size = New System.Drawing.Size(281, 18)
        Me.fndReferenceTable.TabIndex = 12
        Me.fndReferenceTable.Value = ""
        '
        'gv3
        '
        Me.gv3.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv3.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv3.ForeColor = System.Drawing.Color.Black
        Me.gv3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv3.Location = New System.Drawing.Point(0, 0)
        '
        'gv3
        '
        Me.gv3.MasterTemplate.AllowDeleteRow = False
        Me.gv3.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv3.Name = "gv3"
        Me.gv3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv3.ShowGroupPanel = False
        Me.gv3.ShowHeaderCellButtons = True
        Me.gv3.Size = New System.Drawing.Size(842, 241)
        Me.gv3.TabIndex = 2
        Me.gv3.TabStop = False
        Me.gv3.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.btnEditExpression)
        Me.RadPageViewPage3.Controls.Add(Me.txtExpression)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(127.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(848, 364)
        Me.RadPageViewPage3.Text = "Calculation Expression"
        '
        'btnEditExpression
        '
        Me.btnEditExpression.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditExpression.Location = New System.Drawing.Point(645, 3)
        Me.btnEditExpression.Name = "btnEditExpression"
        Me.btnEditExpression.Size = New System.Drawing.Size(104, 22)
        Me.btnEditExpression.TabIndex = 11
        Me.btnEditExpression.Text = "Edit Expression"
        '
        'txtExpression
        '
        Me.txtExpression.AutoSize = False
        Me.txtExpression.CalculationExpression = Nothing
        Me.txtExpression.FieldCode = Nothing
        Me.txtExpression.FieldDesc = Nothing
        Me.txtExpression.FieldMaxLength = 0
        Me.txtExpression.FieldName = Nothing
        Me.txtExpression.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpression.isCalculatedField = False
        Me.txtExpression.IsSourceFromTable = False
        Me.txtExpression.IsSourceFromValueList = False
        Me.txtExpression.IsUnique = False
        Me.txtExpression.Location = New System.Drawing.Point(68, 3)
        Me.txtExpression.MaxLength = 50
        Me.txtExpression.MendatroryField = False
        Me.txtExpression.Multiline = True
        Me.txtExpression.MyLinkLable1 = Me.MyLabel4
        Me.txtExpression.MyLinkLable2 = Nothing
        Me.txtExpression.Name = "txtExpression"
        Me.txtExpression.ReferenceFieldDesc = Nothing
        Me.txtExpression.ReferenceFieldName = Nothing
        Me.txtExpression.ReferenceTableName = Nothing
        Me.txtExpression.Size = New System.Drawing.Size(573, 176)
        Me.txtExpression.TabIndex = 9
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(1, 19)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel4.TabIndex = 10
        Me.MyLabel4.Text = "Expression"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.SplitContainer6)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(168.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(848, 364)
        Me.RadPageViewPage4.Text = "Custom Buttons Configuration"
        '
        'lblMethodDesc
        '
        Me.lblMethodDesc.AutoSize = False
        Me.lblMethodDesc.BorderVisible = True
        Me.lblMethodDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMethodDesc.Location = New System.Drawing.Point(266, 12)
        Me.lblMethodDesc.Name = "lblMethodDesc"
        Me.lblMethodDesc.Size = New System.Drawing.Size(333, 19)
        Me.lblMethodDesc.TabIndex = 27
        '
        'fndMethodCode
        '
        Me.fndMethodCode.CalculationExpression = Nothing
        Me.fndMethodCode.FieldCode = Nothing
        Me.fndMethodCode.FieldDesc = Nothing
        Me.fndMethodCode.FieldMaxLength = 0
        Me.fndMethodCode.FieldName = Nothing
        Me.fndMethodCode.isCalculatedField = False
        Me.fndMethodCode.IsSourceFromTable = False
        Me.fndMethodCode.IsSourceFromValueList = False
        Me.fndMethodCode.IsUnique = False
        Me.fndMethodCode.Location = New System.Drawing.Point(88, 12)
        Me.fndMethodCode.MendatroryField = True
        Me.fndMethodCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMethodCode.MyLinkLable1 = Me.MyLabel6
        Me.fndMethodCode.MyLinkLable2 = Nothing
        Me.fndMethodCode.MyReadOnly = False
        Me.fndMethodCode.MyShowMasterFormButton = False
        Me.fndMethodCode.Name = "fndMethodCode"
        Me.fndMethodCode.ReferenceFieldDesc = Nothing
        Me.fndMethodCode.ReferenceFieldName = Nothing
        Me.fndMethodCode.ReferenceTableName = Nothing
        Me.fndMethodCode.Size = New System.Drawing.Size(175, 18)
        Me.fndMethodCode.TabIndex = 25
        Me.fndMethodCode.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(13, 13)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel6.TabIndex = 26
        Me.MyLabel6.Text = "Method Name"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Location = New System.Drawing.Point(151, 2)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(69, 22)
        Me.btnAddNew.TabIndex = 6
        Me.btnAddNew.Text = "Reset"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(78, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(797, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer6.IsSplitterFixed = True
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.fndMethodCode)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblMethodDesc)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel6)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.gv4)
        Me.SplitContainer6.Size = New System.Drawing.Size(848, 364)
        Me.SplitContainer6.SplitterDistance = 35
        Me.SplitContainer6.TabIndex = 28
        '
        'gv4
        '
        Me.gv4.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv4.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv4.ForeColor = System.Drawing.Color.Black
        Me.gv4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv4.Location = New System.Drawing.Point(0, 0)
        '
        'gv4
        '
        Me.gv4.MasterTemplate.AllowAddNewRow = False
        Me.gv4.MasterTemplate.AllowDeleteRow = False
        Me.gv4.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv4.Name = "gv4"
        Me.gv4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv4.ShowGroupPanel = False
        Me.gv4.ShowHeaderCellButtons = True
        Me.gv4.Size = New System.Drawing.Size(848, 325)
        Me.gv4.TabIndex = 3
        Me.gv4.TabStop = False
        Me.gv4.Text = "RadGridView1"
        '
        'frmCustomFieldEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(869, 477)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCustomFieldEditor"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Custom Field Editor"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkButtons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFieldDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFieldName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbacc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCalculatedField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkManualEntryField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.chkIsForPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsForDetailLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsUnique, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaxLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.isMandatory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.Panel2.PerformLayout()
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.chkManualList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFromTable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.btnEditExpression, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpression, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.lblMethodDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.gv4.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkCalculatedField As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkManualEntryField As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblFieldDescription As common.Controls.MyLabel
    Friend WithEvents lblFieldName As common.Controls.MyLabel
    Friend WithEvents fndCustomFieldName As common.UserControls.txtFinder
    Friend WithEvents lblbacc As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents chkManualList As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkFromTable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtMaxLength As common.Controls.MyTextBox
    Friend WithEvents isMandatory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndFieldName As common.UserControls.txtFinder
    Friend WithEvents fndReferenceTable As common.UserControls.txtFinder
    Friend WithEvents gv3 As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIsUnique As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnEditExpression As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtExpression As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtFieldHeight As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIsForDetailLevel As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIsForPrint As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblMethodDesc As common.Controls.MyLabel
    Friend WithEvents fndMethodCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents chkButtons As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv4 As common.UserControls.MyRadGridView
End Class
