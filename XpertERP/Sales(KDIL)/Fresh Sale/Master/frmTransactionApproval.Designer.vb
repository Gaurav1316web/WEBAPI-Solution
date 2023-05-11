<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransactionApproval
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
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnSelect = New Telerik.WinControls.UI.RadButton()
        Me.btnuSel = New Telerik.WinControls.UI.RadButton()
        Me.lblScreenName = New common.Controls.MyLabel()
        Me.ddApprovalType = New common.Controls.MyComboBox()
        Me.lblDocumentDate = New common.Controls.MyLabel()
        Me.lblApprovalType = New common.Controls.MyLabel()
        Me.lblDocumentNo = New common.Controls.MyLabel()
        Me.LblDocDate = New common.Controls.MyLabel()
        Me.cmbScreenName = New common.Controls.MyComboBox()
        Me.FndDocumnetNo = New common.UserControls.txtFinder()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnUnapprove = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnApprove = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblQCDate = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblQCNo = New common.Controls.MyLabel()
        Me.GridQC = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnuSel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScreenName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddApprovalType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovalType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbScreenName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnapprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridQC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridQC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnapprove)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnApprove)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1136, 389)
        Me.SplitContainer1.SplitterDistance = 347
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1136, 347)
        Me.SplitContainer2.SplitterDistance = 107
        Me.SplitContainer2.TabIndex = 137
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnSelect)
        Me.RadGroupBox1.Controls.Add(Me.btnuSel)
        Me.RadGroupBox1.Controls.Add(Me.lblScreenName)
        Me.RadGroupBox1.Controls.Add(Me.ddApprovalType)
        Me.RadGroupBox1.Controls.Add(Me.lblDocumentDate)
        Me.RadGroupBox1.Controls.Add(Me.lblApprovalType)
        Me.RadGroupBox1.Controls.Add(Me.lblDocumentNo)
        Me.RadGroupBox1.Controls.Add(Me.LblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.cmbScreenName)
        Me.RadGroupBox1.Controls.Add(Me.FndDocumnetNo)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1136, 107)
        Me.RadGroupBox1.TabIndex = 136
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(911, 10)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(66, 18)
        Me.btnSelect.TabIndex = 137
        Me.btnSelect.Text = "Select All"
        Me.btnSelect.Visible = False
        '
        'btnuSel
        '
        Me.btnuSel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnuSel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnuSel.Location = New System.Drawing.Point(979, 10)
        Me.btnuSel.Name = "btnuSel"
        Me.btnuSel.Size = New System.Drawing.Size(66, 18)
        Me.btnuSel.TabIndex = 136
        Me.btnuSel.Text = "Unselect All"
        Me.btnuSel.Visible = False
        '
        'lblScreenName
        '
        Me.lblScreenName.FieldName = Nothing
        Me.lblScreenName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblScreenName.Location = New System.Drawing.Point(5, 11)
        Me.lblScreenName.Name = "lblScreenName"
        Me.lblScreenName.Size = New System.Drawing.Size(75, 16)
        Me.lblScreenName.TabIndex = 127
        Me.lblScreenName.Text = "Screen Name"
        '
        'ddApprovalType
        '
        Me.ddApprovalType.AutoCompleteDisplayMember = Nothing
        Me.ddApprovalType.AutoCompleteValueMember = Nothing
        Me.ddApprovalType.CalculationExpression = Nothing
        Me.ddApprovalType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddApprovalType.FieldCode = Nothing
        Me.ddApprovalType.FieldDesc = Nothing
        Me.ddApprovalType.FieldMaxLength = 0
        Me.ddApprovalType.FieldName = Nothing
        Me.ddApprovalType.isCalculatedField = False
        Me.ddApprovalType.IsSourceFromTable = False
        Me.ddApprovalType.IsSourceFromValueList = False
        Me.ddApprovalType.IsUnique = False
        RadListDataItem7.Text = "Rate"
        RadListDataItem8.Text = "Credit Limit"
        RadListDataItem9.Text = "Credit Days"
        RadListDataItem10.Text = "Advance Receipt"
        RadListDataItem11.Text = "Cancellation"
        RadListDataItem12.Text = "Other"
        Me.ddApprovalType.Items.Add(RadListDataItem7)
        Me.ddApprovalType.Items.Add(RadListDataItem8)
        Me.ddApprovalType.Items.Add(RadListDataItem9)
        Me.ddApprovalType.Items.Add(RadListDataItem10)
        Me.ddApprovalType.Items.Add(RadListDataItem11)
        Me.ddApprovalType.Items.Add(RadListDataItem12)
        Me.ddApprovalType.Location = New System.Drawing.Point(96, 83)
        Me.ddApprovalType.MendatroryField = True
        Me.ddApprovalType.MyLinkLable1 = Me.lblScreenName
        Me.ddApprovalType.MyLinkLable2 = Nothing
        Me.ddApprovalType.Name = "ddApprovalType"
        Me.ddApprovalType.ReferenceFieldDesc = Nothing
        Me.ddApprovalType.ReferenceFieldName = Nothing
        Me.ddApprovalType.ReferenceTableName = Nothing
        Me.ddApprovalType.Size = New System.Drawing.Size(180, 20)
        Me.ddApprovalType.TabIndex = 135
        '
        'lblDocumentDate
        '
        Me.lblDocumentDate.FieldName = Nothing
        Me.lblDocumentDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocumentDate.Location = New System.Drawing.Point(5, 59)
        Me.lblDocumentDate.Name = "lblDocumentDate"
        Me.lblDocumentDate.Size = New System.Drawing.Size(85, 16)
        Me.lblDocumentDate.TabIndex = 129
        Me.lblDocumentDate.Text = "Document Date"
        '
        'lblApprovalType
        '
        Me.lblApprovalType.FieldName = Nothing
        Me.lblApprovalType.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblApprovalType.Location = New System.Drawing.Point(5, 81)
        Me.lblApprovalType.Name = "lblApprovalType"
        Me.lblApprovalType.Size = New System.Drawing.Size(79, 16)
        Me.lblApprovalType.TabIndex = 134
        Me.lblApprovalType.Text = "Approval Type"
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.FieldName = Nothing
        Me.lblDocumentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentNo.Location = New System.Drawing.Point(5, 34)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(79, 16)
        Me.lblDocumentNo.TabIndex = 128
        Me.lblDocumentNo.Text = "Document No."
        '
        'LblDocDate
        '
        Me.LblDocDate.AutoSize = False
        Me.LblDocDate.BorderVisible = True
        Me.LblDocDate.FieldName = Nothing
        Me.LblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocDate.Location = New System.Drawing.Point(96, 59)
        Me.LblDocDate.Name = "LblDocDate"
        Me.LblDocDate.Size = New System.Drawing.Size(180, 18)
        Me.LblDocDate.TabIndex = 133
        Me.LblDocDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblDocDate.TextWrap = False
        '
        'cmbScreenName
        '
        Me.cmbScreenName.AutoCompleteDisplayMember = Nothing
        Me.cmbScreenName.AutoCompleteValueMember = Nothing
        Me.cmbScreenName.CalculationExpression = Nothing
        Me.cmbScreenName.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbScreenName.FieldCode = Nothing
        Me.cmbScreenName.FieldDesc = Nothing
        Me.cmbScreenName.FieldMaxLength = 0
        Me.cmbScreenName.FieldName = Nothing
        Me.cmbScreenName.isCalculatedField = False
        Me.cmbScreenName.IsSourceFromTable = False
        Me.cmbScreenName.IsSourceFromValueList = False
        Me.cmbScreenName.IsUnique = False
        Me.cmbScreenName.Location = New System.Drawing.Point(96, 9)
        Me.cmbScreenName.MendatroryField = True
        Me.cmbScreenName.MyLinkLable1 = Me.lblScreenName
        Me.cmbScreenName.MyLinkLable2 = Nothing
        Me.cmbScreenName.Name = "cmbScreenName"
        Me.cmbScreenName.ReferenceFieldDesc = Nothing
        Me.cmbScreenName.ReferenceFieldName = Nothing
        Me.cmbScreenName.ReferenceTableName = Nothing
        Me.cmbScreenName.Size = New System.Drawing.Size(293, 20)
        Me.cmbScreenName.TabIndex = 126
        '
        'FndDocumnetNo
        '
        Me.FndDocumnetNo.CalculationExpression = Nothing
        Me.FndDocumnetNo.FieldCode = Nothing
        Me.FndDocumnetNo.FieldDesc = Nothing
        Me.FndDocumnetNo.FieldMaxLength = 0
        Me.FndDocumnetNo.FieldName = Nothing
        Me.FndDocumnetNo.isCalculatedField = False
        Me.FndDocumnetNo.IsSourceFromTable = False
        Me.FndDocumnetNo.IsSourceFromValueList = False
        Me.FndDocumnetNo.IsUnique = False
        Me.FndDocumnetNo.Location = New System.Drawing.Point(96, 35)
        Me.FndDocumnetNo.MendatroryField = True
        Me.FndDocumnetNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndDocumnetNo.MyLinkLable1 = Nothing
        Me.FndDocumnetNo.MyLinkLable2 = Nothing
        Me.FndDocumnetNo.MyReadOnly = False
        Me.FndDocumnetNo.MyShowMasterFormButton = False
        Me.FndDocumnetNo.Name = "FndDocumnetNo"
        Me.FndDocumnetNo.ReferenceFieldDesc = Nothing
        Me.FndDocumnetNo.ReferenceFieldName = Nothing
        Me.FndDocumnetNo.ReferenceTableName = Nothing
        Me.FndDocumnetNo.Size = New System.Drawing.Size(180, 18)
        Me.FndDocumnetNo.TabIndex = 131
        Me.FndDocumnetNo.Value = ""
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        'Gv1
        '
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1136, 236)
        Me.Gv1.TabIndex = 1
        Me.Gv1.Text = "RadGridView1"
        Me.Gv1.Visible = False
        '
        'btnUnapprove
        '
        Me.btnUnapprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnapprove.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnapprove.Location = New System.Drawing.Point(147, 10)
        Me.btnUnapprove.Name = "btnUnapprove"
        Me.btnUnapprove.Size = New System.Drawing.Size(66, 18)
        Me.btnUnapprove.TabIndex = 138
        Me.btnUnapprove.Text = "Unapprove"
        Me.btnUnapprove.Visible = False
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(78, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(66, 18)
        Me.btnReset.TabIndex = 9
        Me.btnReset.Text = "Reset"
        '
        'btnApprove
        '
        Me.btnApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApprove.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApprove.Location = New System.Drawing.Point(8, 10)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Size = New System.Drawing.Size(66, 18)
        Me.btnApprove.TabIndex = 7
        Me.btnApprove.Text = "Approve"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1062, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1157, 437)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "Level 1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(48.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1136, 389)
        Me.RadPageViewPage1.Text = "Level1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(48.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1136, 389)
        Me.RadPageViewPage2.Text = "Level2"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(64.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1136, 389)
        Me.RadPageViewPage3.Text = "QC Detail"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblQCDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblQCNo)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GridQC)
        Me.SplitContainer3.Size = New System.Drawing.Size(1136, 389)
        Me.SplitContainer3.SplitterDistance = 69
        Me.SplitContainer3.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(12, 39)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel3.TabIndex = 136
        Me.MyLabel3.Text = "QC Date"
        '
        'lblQCDate
        '
        Me.lblQCDate.AutoSize = False
        Me.lblQCDate.BorderVisible = True
        Me.lblQCDate.FieldName = Nothing
        Me.lblQCDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCDate.Location = New System.Drawing.Point(103, 39)
        Me.lblQCDate.Name = "lblQCDate"
        Me.lblQCDate.Size = New System.Drawing.Size(180, 18)
        Me.lblQCDate.TabIndex = 137
        Me.lblQCDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblQCDate.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(12, 17)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel1.TabIndex = 134
        Me.MyLabel1.Text = "QC No"
        '
        'lblQCNo
        '
        Me.lblQCNo.AutoSize = False
        Me.lblQCNo.BorderVisible = True
        Me.lblQCNo.FieldName = Nothing
        Me.lblQCNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCNo.Location = New System.Drawing.Point(103, 17)
        Me.lblQCNo.Name = "lblQCNo"
        Me.lblQCNo.Size = New System.Drawing.Size(180, 18)
        Me.lblQCNo.TabIndex = 135
        Me.lblQCNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblQCNo.TextWrap = False
        '
        'GridQC
        '
        Me.GridQC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridQC.Location = New System.Drawing.Point(0, 0)
        '
        'GridQC
        '
        Me.GridQC.MasterTemplate.ShowHeaderCellButtons = True
        Me.GridQC.Name = "GridQC"
        Me.GridQC.ShowHeaderCellButtons = True
        Me.GridQC.Size = New System.Drawing.Size(1136, 316)
        Me.GridQC.TabIndex = 3
        Me.GridQC.Text = "RadGridView1"
        Me.GridQC.Visible = False
        '
        'FrmTransactionApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1157, 437)
        Me.Controls.Add(Me.RadPageView1)
        Me.Name = "FrmTransactionApproval"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmTransactionApproval"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnuSel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScreenName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddApprovalType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovalType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbScreenName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnapprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridQC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridQC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnApprove As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblDocDate As common.Controls.MyLabel
    Friend WithEvents FndDocumnetNo As common.UserControls.txtFinder
    Friend WithEvents lblScreenName As common.Controls.MyLabel
    Friend WithEvents cmbScreenName As common.Controls.MyComboBox
    Friend WithEvents lblDocumentNo As common.Controls.MyLabel
    Friend WithEvents lblDocumentDate As common.Controls.MyLabel
    Friend WithEvents ddApprovalType As common.Controls.MyComboBox
    Friend WithEvents lblApprovalType As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnuSel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUnapprove As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents GridQC As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblQCDate As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblQCNo As common.Controls.MyLabel
End Class

