Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetRegister
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtAssetCodeFinder = New common.UserControls.txtFinder()
        Me.lblAssetCodeFinder = New common.Controls.MyLabel()
        Me.TxtMultiVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.TxtMultiCategory = New common.UserControls.txtMultiSelectFinder()
        Me.lblCategory = New common.Controls.MyLabel()
        Me.TxtMultiGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblGroup = New common.Controls.MyLabel()
        Me.TxtMultiAssetId = New common.UserControls.txtMultiSelectFinder()
        Me.lblAssetId = New common.Controls.MyLabel()
        Me.TxtMultiCostCenter = New common.UserControls.txtMultiSelectFinder()
        Me.lblCostCenter = New common.Controls.MyLabel()
        Me.TxtMultiLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadGroupBox11 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyComboBox1 = New common.Controls.MyComboBox()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVendor = New common.MyCheckBoxGrid()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.chkVendorSelect = New common.Controls.MyRadioButton()
        Me.chkVendorAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCategory = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkCategSelect = New common.Controls.MyRadioButton()
        Me.chkCategAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgGroup = New common.MyCheckBoxGrid()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.chkGroupSelect = New common.Controls.MyRadioButton()
        Me.chkGroupAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgAsset = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkAssetSelect = New common.Controls.MyRadioButton()
        Me.chkAssetAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox10 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCostCenter = New common.MyCheckBoxGrid()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.chkCostCentSelect = New common.Controls.MyRadioButton()
        Me.chkCostCentAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbTax = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbBook = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyComboBox2 = New common.Controls.MyComboBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyCheckBoxGrid4 = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.MyRadioButton7 = New common.Controls.MyRadioButton()
        Me.MyRadioButton8 = New common.Controls.MyRadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyCheckBoxGrid5 = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.MyRadioButton9 = New common.Controls.MyRadioButton()
        Me.MyRadioButton10 = New common.Controls.MyRadioButton()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyCheckBoxGrid6 = New common.MyCheckBoxGrid()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.MyRadioButton11 = New common.Controls.MyRadioButton()
        Me.MyRadioButton12 = New common.Controls.MyRadioButton()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLoc = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GV1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.export = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.MyCheckBoxGrid2 = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.MyRadioButton3 = New common.Controls.MyRadioButton()
        Me.MyRadioButton4 = New common.Controls.MyRadioButton()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblAssetCodeFinder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox11.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCategSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCategAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkGroupSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroupAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkAssetSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAssetAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox10.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.chkCostCentSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCostCentAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.MyRadioButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.MyRadioButton9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.MyRadioButton11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.MyRadioButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(840, 346)
        Me.SplitContainer1.SplitterDistance = 317
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(840, 317)
        Me.RadPageView1.TabIndex = 115
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.txtAssetCodeFinder)
        Me.RadPageViewPage1.Controls.Add(Me.lblAssetCodeFinder)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiVendor)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendor)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiCategory)
        Me.RadPageViewPage1.Controls.Add(Me.lblCategory)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblGroup)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiAssetId)
        Me.RadPageViewPage1.Controls.Add(Me.lblAssetId)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiCostCenter)
        Me.RadPageViewPage1.Controls.Add(Me.lblCostCenter)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox11)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox9)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox10)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.Locationgb)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(819, 269)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'txtAssetCodeFinder
        '
        Me.txtAssetCodeFinder.CalculationExpression = Nothing
        Me.txtAssetCodeFinder.FieldCode = Nothing
        Me.txtAssetCodeFinder.FieldDesc = Nothing
        Me.txtAssetCodeFinder.FieldMaxLength = 0
        Me.txtAssetCodeFinder.FieldName = Nothing
        Me.txtAssetCodeFinder.isCalculatedField = False
        Me.txtAssetCodeFinder.IsSourceFromTable = False
        Me.txtAssetCodeFinder.IsSourceFromValueList = False
        Me.txtAssetCodeFinder.IsUnique = False
        Me.txtAssetCodeFinder.Location = New System.Drawing.Point(96, 284)
        Me.txtAssetCodeFinder.MendatroryField = True
        Me.txtAssetCodeFinder.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetCodeFinder.MyLinkLable1 = Nothing
        Me.txtAssetCodeFinder.MyLinkLable2 = Me.lblAssetCodeFinder
        Me.txtAssetCodeFinder.MyReadOnly = False
        Me.txtAssetCodeFinder.MyShowMasterFormButton = False
        Me.txtAssetCodeFinder.Name = "txtAssetCodeFinder"
        Me.txtAssetCodeFinder.ReferenceFieldDesc = Nothing
        Me.txtAssetCodeFinder.ReferenceFieldName = Nothing
        Me.txtAssetCodeFinder.ReferenceTableName = Nothing
        Me.txtAssetCodeFinder.Size = New System.Drawing.Size(185, 18)
        Me.txtAssetCodeFinder.TabIndex = 130
        Me.txtAssetCodeFinder.Value = ""
        Me.txtAssetCodeFinder.Visible = False
        '
        'lblAssetCodeFinder
        '
        Me.lblAssetCodeFinder.AutoSize = False
        Me.lblAssetCodeFinder.BorderVisible = True
        Me.lblAssetCodeFinder.FieldName = Nothing
        Me.lblAssetCodeFinder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetCodeFinder.Location = New System.Drawing.Point(282, 284)
        Me.lblAssetCodeFinder.Name = "lblAssetCodeFinder"
        Me.lblAssetCodeFinder.Size = New System.Drawing.Size(263, 18)
        Me.lblAssetCodeFinder.TabIndex = 131
        Me.lblAssetCodeFinder.TextWrap = False
        Me.lblAssetCodeFinder.Visible = False
        '
        'TxtMultiVendor
        '
        Me.TxtMultiVendor.arrDispalyMember = Nothing
        Me.TxtMultiVendor.arrValueMember = Nothing
        Me.TxtMultiVendor.Location = New System.Drawing.Point(96, 175)
        Me.TxtMultiVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiVendor.MyLinkLable1 = Nothing
        Me.TxtMultiVendor.MyLinkLable2 = Nothing
        Me.TxtMultiVendor.MyNullText = "All"
        Me.TxtMultiVendor.Name = "TxtMultiVendor"
        Me.TxtMultiVendor.Size = New System.Drawing.Size(300, 19)
        Me.TxtMultiVendor.TabIndex = 127
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Location = New System.Drawing.Point(3, 176)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 18)
        Me.lblVendor.TabIndex = 126
        Me.lblVendor.Text = "Vendor"
        '
        'TxtMultiCategory
        '
        Me.TxtMultiCategory.arrDispalyMember = Nothing
        Me.TxtMultiCategory.arrValueMember = Nothing
        Me.TxtMultiCategory.Location = New System.Drawing.Point(96, 151)
        Me.TxtMultiCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCategory.MyLinkLable1 = Nothing
        Me.TxtMultiCategory.MyLinkLable2 = Nothing
        Me.TxtMultiCategory.MyNullText = "All"
        Me.TxtMultiCategory.Name = "TxtMultiCategory"
        Me.TxtMultiCategory.Size = New System.Drawing.Size(300, 19)
        Me.TxtMultiCategory.TabIndex = 125
        '
        'lblCategory
        '
        Me.lblCategory.FieldName = Nothing
        Me.lblCategory.Location = New System.Drawing.Point(3, 152)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(51, 18)
        Me.lblCategory.TabIndex = 124
        Me.lblCategory.Text = "Category"
        '
        'TxtMultiGroup
        '
        Me.TxtMultiGroup.arrDispalyMember = Nothing
        Me.TxtMultiGroup.arrValueMember = Nothing
        Me.TxtMultiGroup.Location = New System.Drawing.Point(96, 127)
        Me.TxtMultiGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiGroup.MyLinkLable1 = Nothing
        Me.TxtMultiGroup.MyLinkLable2 = Nothing
        Me.TxtMultiGroup.MyNullText = "All"
        Me.TxtMultiGroup.Name = "TxtMultiGroup"
        Me.TxtMultiGroup.Size = New System.Drawing.Size(300, 19)
        Me.TxtMultiGroup.TabIndex = 123
        '
        'lblGroup
        '
        Me.lblGroup.FieldName = Nothing
        Me.lblGroup.Location = New System.Drawing.Point(3, 128)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(41, 18)
        Me.lblGroup.TabIndex = 122
        Me.lblGroup.Text = "Group "
        '
        'TxtMultiAssetId
        '
        Me.TxtMultiAssetId.arrDispalyMember = Nothing
        Me.TxtMultiAssetId.arrValueMember = Nothing
        Me.TxtMultiAssetId.Location = New System.Drawing.Point(96, 105)
        Me.TxtMultiAssetId.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiAssetId.MyLinkLable1 = Nothing
        Me.TxtMultiAssetId.MyLinkLable2 = Nothing
        Me.TxtMultiAssetId.MyNullText = "All"
        Me.TxtMultiAssetId.Name = "TxtMultiAssetId"
        Me.TxtMultiAssetId.Size = New System.Drawing.Size(300, 19)
        Me.TxtMultiAssetId.TabIndex = 121
        '
        'lblAssetId
        '
        Me.lblAssetId.FieldName = Nothing
        Me.lblAssetId.Location = New System.Drawing.Point(3, 107)
        Me.lblAssetId.Name = "lblAssetId"
        Me.lblAssetId.Size = New System.Drawing.Size(46, 18)
        Me.lblAssetId.TabIndex = 120
        Me.lblAssetId.Text = "Asset Id"
        '
        'TxtMultiCostCenter
        '
        Me.TxtMultiCostCenter.arrDispalyMember = Nothing
        Me.TxtMultiCostCenter.arrValueMember = Nothing
        Me.TxtMultiCostCenter.Location = New System.Drawing.Point(96, 82)
        Me.TxtMultiCostCenter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCostCenter.MyLinkLable1 = Nothing
        Me.TxtMultiCostCenter.MyLinkLable2 = Nothing
        Me.TxtMultiCostCenter.MyNullText = "All"
        Me.TxtMultiCostCenter.Name = "TxtMultiCostCenter"
        Me.TxtMultiCostCenter.Size = New System.Drawing.Size(300, 19)
        Me.TxtMultiCostCenter.TabIndex = 119
        '
        'lblCostCenter
        '
        Me.lblCostCenter.FieldName = Nothing
        Me.lblCostCenter.Location = New System.Drawing.Point(3, 83)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(65, 18)
        Me.lblCostCenter.TabIndex = 118
        Me.lblCostCenter.Text = "Cost Center"
        '
        'TxtMultiLocation
        '
        Me.TxtMultiLocation.arrDispalyMember = Nothing
        Me.TxtMultiLocation.arrValueMember = Nothing
        Me.TxtMultiLocation.Location = New System.Drawing.Point(96, 58)
        Me.TxtMultiLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiLocation.MyLinkLable1 = Nothing
        Me.TxtMultiLocation.MyLinkLable2 = Nothing
        Me.TxtMultiLocation.MyNullText = "All"
        Me.TxtMultiLocation.Name = "TxtMultiLocation"
        Me.TxtMultiLocation.Size = New System.Drawing.Size(300, 19)
        Me.TxtMultiLocation.TabIndex = 117
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(3, 58)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 116
        Me.lblLocation.Text = "Location"
        '
        'RadGroupBox11
        '
        Me.RadGroupBox11.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox11.Controls.Add(Me.rdbDetail)
        Me.RadGroupBox11.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox11.Controls.Add(Me.MyComboBox1)
        Me.RadGroupBox11.HeaderText = ""
        Me.RadGroupBox11.Location = New System.Drawing.Point(464, 12)
        Me.RadGroupBox11.Name = "RadGroupBox11"
        Me.RadGroupBox11.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox11.Size = New System.Drawing.Size(239, 36)
        Me.RadGroupBox11.TabIndex = 87
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(158, 8)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 106
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(13, 8)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 105
        Me.rdbSummary.Text = "Summary"
        '
        'MyComboBox1
        '
        Me.MyComboBox1.AutoCompleteDisplayMember = Nothing
        Me.MyComboBox1.AutoCompleteValueMember = Nothing
        Me.MyComboBox1.CalculationExpression = Nothing
        Me.MyComboBox1.DropDownAnimationEnabled = True
        Me.MyComboBox1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.MyComboBox1.FieldCode = Nothing
        Me.MyComboBox1.FieldDesc = Nothing
        Me.MyComboBox1.FieldMaxLength = 0
        Me.MyComboBox1.FieldName = Nothing
        Me.MyComboBox1.isCalculatedField = False
        Me.MyComboBox1.IsSourceFromTable = False
        Me.MyComboBox1.IsSourceFromValueList = False
        Me.MyComboBox1.IsUnique = False
        Me.MyComboBox1.Location = New System.Drawing.Point(367, 12)
        Me.MyComboBox1.MendatroryField = False
        Me.MyComboBox1.MyLinkLable1 = Nothing
        Me.MyComboBox1.MyLinkLable2 = Nothing
        Me.MyComboBox1.Name = "MyComboBox1"
        Me.MyComboBox1.ReferenceFieldDesc = Nothing
        Me.MyComboBox1.ReferenceFieldName = Nothing
        Me.MyComboBox1.ReferenceTableName = Nothing
        Me.MyComboBox1.Size = New System.Drawing.Size(11, 20)
        Me.MyComboBox1.TabIndex = 84
        Me.MyComboBox1.Visible = False
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox8.Controls.Add(Me.Panel8)
        Me.RadGroupBox8.HeaderText = "Vendor"
        Me.RadGroupBox8.Location = New System.Drawing.Point(728, 264)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(76, 52)
        Me.RadGroupBox8.TabIndex = 115
        Me.RadGroupBox8.Text = "Vendor"
        Me.RadGroupBox8.Visible = False
        '
        'cbgVendor
        '
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(56, 2)
        Me.cbgVendor.TabIndex = 1
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chkVendorSelect)
        Me.Panel8.Controls.Add(Me.chkVendorAll)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(56, 20)
        Me.Panel8.TabIndex = 0
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 1
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(114, 1)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 0
        Me.chkVendorAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgCategory)
        Me.RadGroupBox4.Controls.Add(Me.Panel4)
        Me.RadGroupBox4.HeaderText = "Category"
        Me.RadGroupBox4.Location = New System.Drawing.Point(728, 217)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(81, 51)
        Me.RadGroupBox4.TabIndex = 112
        Me.RadGroupBox4.Text = "Category"
        Me.RadGroupBox4.Visible = False
        '
        'cbgCategory
        '
        Me.cbgCategory.CheckedValue = Nothing
        Me.cbgCategory.DataSource = Nothing
        Me.cbgCategory.DisplayMember = "Name"
        Me.cbgCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCategory.Location = New System.Drawing.Point(10, 40)
        Me.cbgCategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCategory.MyShowHeadrText = False
        Me.cbgCategory.Name = "cbgCategory"
        Me.cbgCategory.Size = New System.Drawing.Size(61, 1)
        Me.cbgCategory.TabIndex = 1
        Me.cbgCategory.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkCategSelect)
        Me.Panel4.Controls.Add(Me.chkCategAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(61, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkCategSelect
        '
        Me.chkCategSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkCategSelect.MyLinkLable1 = Nothing
        Me.chkCategSelect.MyLinkLable2 = Nothing
        Me.chkCategSelect.Name = "chkCategSelect"
        Me.chkCategSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCategSelect.TabIndex = 1
        Me.chkCategSelect.Text = "Select"
        '
        'chkCategAll
        '
        Me.chkCategAll.Location = New System.Drawing.Point(114, 1)
        Me.chkCategAll.MyLinkLable1 = Nothing
        Me.chkCategAll.MyLinkLable2 = Nothing
        Me.chkCategAll.Name = "chkCategAll"
        Me.chkCategAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCategAll.TabIndex = 0
        Me.chkCategAll.Text = "All"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.cbgGroup)
        Me.RadGroupBox9.Controls.Add(Me.Panel9)
        Me.RadGroupBox9.HeaderText = "Group"
        Me.RadGroupBox9.Location = New System.Drawing.Point(728, 169)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(81, 42)
        Me.RadGroupBox9.TabIndex = 114
        Me.RadGroupBox9.Text = "Group"
        Me.RadGroupBox9.Visible = False
        '
        'cbgGroup
        '
        Me.cbgGroup.CheckedValue = Nothing
        Me.cbgGroup.DataSource = Nothing
        Me.cbgGroup.DisplayMember = "Name"
        Me.cbgGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgGroup.Location = New System.Drawing.Point(10, 40)
        Me.cbgGroup.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgGroup.MyShowHeadrText = False
        Me.cbgGroup.Name = "cbgGroup"
        Me.cbgGroup.Size = New System.Drawing.Size(61, 0)
        Me.cbgGroup.TabIndex = 1
        Me.cbgGroup.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkGroupSelect)
        Me.Panel9.Controls.Add(Me.chkGroupAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(61, 20)
        Me.Panel9.TabIndex = 0
        '
        'chkGroupSelect
        '
        Me.chkGroupSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkGroupSelect.MyLinkLable1 = Nothing
        Me.chkGroupSelect.MyLinkLable2 = Nothing
        Me.chkGroupSelect.Name = "chkGroupSelect"
        Me.chkGroupSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkGroupSelect.TabIndex = 1
        Me.chkGroupSelect.Text = "Select"
        '
        'chkGroupAll
        '
        Me.chkGroupAll.Location = New System.Drawing.Point(114, 1)
        Me.chkGroupAll.MyLinkLable1 = Nothing
        Me.chkGroupAll.MyLinkLable2 = Nothing
        Me.chkGroupAll.Name = "chkGroupAll"
        Me.chkGroupAll.Size = New System.Drawing.Size(33, 18)
        Me.chkGroupAll.TabIndex = 0
        Me.chkGroupAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgAsset)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Asset Id"
        Me.RadGroupBox2.Location = New System.Drawing.Point(728, 123)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(71, 37)
        Me.RadGroupBox2.TabIndex = 111
        Me.RadGroupBox2.Text = "Asset Id"
        Me.RadGroupBox2.Visible = False
        '
        'cbgAsset
        '
        Me.cbgAsset.CheckedValue = Nothing
        Me.cbgAsset.DataSource = Nothing
        Me.cbgAsset.DisplayMember = "Name"
        Me.cbgAsset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgAsset.Location = New System.Drawing.Point(10, 40)
        Me.cbgAsset.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgAsset.MyShowHeadrText = False
        Me.cbgAsset.Name = "cbgAsset"
        Me.cbgAsset.Size = New System.Drawing.Size(51, 0)
        Me.cbgAsset.TabIndex = 1
        Me.cbgAsset.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkAssetSelect)
        Me.Panel1.Controls.Add(Me.chkAssetAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(51, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkAssetSelect
        '
        Me.chkAssetSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkAssetSelect.MyLinkLable1 = Nothing
        Me.chkAssetSelect.MyLinkLable2 = Nothing
        Me.chkAssetSelect.Name = "chkAssetSelect"
        Me.chkAssetSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkAssetSelect.TabIndex = 1
        Me.chkAssetSelect.Text = "Select"
        '
        'chkAssetAll
        '
        Me.chkAssetAll.Location = New System.Drawing.Point(114, 1)
        Me.chkAssetAll.MyLinkLable1 = Nothing
        Me.chkAssetAll.MyLinkLable2 = Nothing
        Me.chkAssetAll.Name = "chkAssetAll"
        Me.chkAssetAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAssetAll.TabIndex = 0
        Me.chkAssetAll.Text = "All"
        '
        'RadGroupBox10
        '
        Me.RadGroupBox10.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox10.Controls.Add(Me.cbgCostCenter)
        Me.RadGroupBox10.Controls.Add(Me.Panel10)
        Me.RadGroupBox10.HeaderText = "Cost Center"
        Me.RadGroupBox10.Location = New System.Drawing.Point(728, 83)
        Me.RadGroupBox10.Name = "RadGroupBox10"
        Me.RadGroupBox10.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox10.Size = New System.Drawing.Size(81, 34)
        Me.RadGroupBox10.TabIndex = 113
        Me.RadGroupBox10.Text = "Cost Center"
        Me.RadGroupBox10.Visible = False
        '
        'cbgCostCenter
        '
        Me.cbgCostCenter.CheckedValue = Nothing
        Me.cbgCostCenter.DataSource = Nothing
        Me.cbgCostCenter.DisplayMember = "Name"
        Me.cbgCostCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCostCenter.Location = New System.Drawing.Point(10, 40)
        Me.cbgCostCenter.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCostCenter.MyShowHeadrText = False
        Me.cbgCostCenter.Name = "cbgCostCenter"
        Me.cbgCostCenter.Size = New System.Drawing.Size(61, 0)
        Me.cbgCostCenter.TabIndex = 1
        Me.cbgCostCenter.ValueMember = "Code"
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.chkCostCentSelect)
        Me.Panel10.Controls.Add(Me.chkCostCentAll)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(10, 20)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(61, 20)
        Me.Panel10.TabIndex = 0
        '
        'chkCostCentSelect
        '
        Me.chkCostCentSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkCostCentSelect.MyLinkLable1 = Nothing
        Me.chkCostCentSelect.MyLinkLable2 = Nothing
        Me.chkCostCentSelect.Name = "chkCostCentSelect"
        Me.chkCostCentSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCostCentSelect.TabIndex = 1
        Me.chkCostCentSelect.Text = "Select"
        '
        'chkCostCentAll
        '
        Me.chkCostCentAll.Location = New System.Drawing.Point(114, 1)
        Me.chkCostCentAll.MyLinkLable1 = Nothing
        Me.chkCostCentAll.MyLinkLable2 = Nothing
        Me.chkCostCentAll.Name = "chkCostCentAll"
        Me.chkCostCentAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCostCentAll.TabIndex = 0
        Me.chkCostCentAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdbTax)
        Me.RadGroupBox1.Controls.Add(Me.rdbBook)
        Me.RadGroupBox1.Controls.Add(Me.MyComboBox2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(267, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(190, 36)
        Me.RadGroupBox1.TabIndex = 107
        '
        'rdbTax
        '
        Me.rdbTax.Location = New System.Drawing.Point(106, 8)
        Me.rdbTax.Name = "rdbTax"
        Me.rdbTax.Size = New System.Drawing.Size(37, 18)
        Me.rdbTax.TabIndex = 106
        Me.rdbTax.Text = "Tax"
        '
        'rdbBook
        '
        Me.rdbBook.Location = New System.Drawing.Point(13, 8)
        Me.rdbBook.Name = "rdbBook"
        Me.rdbBook.Size = New System.Drawing.Size(46, 18)
        Me.rdbBook.TabIndex = 105
        Me.rdbBook.Text = "Book"
        '
        'MyComboBox2
        '
        Me.MyComboBox2.AutoCompleteDisplayMember = Nothing
        Me.MyComboBox2.AutoCompleteValueMember = Nothing
        Me.MyComboBox2.CalculationExpression = Nothing
        Me.MyComboBox2.DropDownAnimationEnabled = True
        Me.MyComboBox2.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.MyComboBox2.FieldCode = Nothing
        Me.MyComboBox2.FieldDesc = Nothing
        Me.MyComboBox2.FieldMaxLength = 0
        Me.MyComboBox2.FieldName = Nothing
        Me.MyComboBox2.isCalculatedField = False
        Me.MyComboBox2.IsSourceFromTable = False
        Me.MyComboBox2.IsSourceFromValueList = False
        Me.MyComboBox2.IsUnique = False
        Me.MyComboBox2.Location = New System.Drawing.Point(367, 12)
        Me.MyComboBox2.MendatroryField = False
        Me.MyComboBox2.MyLinkLable1 = Nothing
        Me.MyComboBox2.MyLinkLable2 = Nothing
        Me.MyComboBox2.Name = "MyComboBox2"
        Me.MyComboBox2.ReferenceFieldDesc = Nothing
        Me.MyComboBox2.ReferenceFieldName = Nothing
        Me.MyComboBox2.ReferenceTableName = Nothing
        Me.MyComboBox2.Size = New System.Drawing.Size(11, 20)
        Me.MyComboBox2.TabIndex = 84
        Me.MyComboBox2.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox7)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 6)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(258, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Select Date"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.MyCheckBoxGrid4)
        Me.RadGroupBox5.Controls.Add(Me.Panel5)
        Me.RadGroupBox5.HeaderText = "RadGroupBox5"
        Me.RadGroupBox5.Location = New System.Drawing.Point(398, 316)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(330, 171)
        Me.RadGroupBox5.TabIndex = 115
        Me.RadGroupBox5.Text = "RadGroupBox5"
        '
        'MyCheckBoxGrid4
        '
        Me.MyCheckBoxGrid4.CheckedValue = Nothing
        Me.MyCheckBoxGrid4.DataSource = Nothing
        Me.MyCheckBoxGrid4.DisplayMember = "Name"
        Me.MyCheckBoxGrid4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyCheckBoxGrid4.Location = New System.Drawing.Point(10, 40)
        Me.MyCheckBoxGrid4.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.MyCheckBoxGrid4.MyShowHeadrText = False
        Me.MyCheckBoxGrid4.Name = "MyCheckBoxGrid4"
        Me.MyCheckBoxGrid4.Size = New System.Drawing.Size(310, 121)
        Me.MyCheckBoxGrid4.TabIndex = 1
        Me.MyCheckBoxGrid4.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.MyRadioButton7)
        Me.Panel5.Controls.Add(Me.MyRadioButton8)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(310, 20)
        Me.Panel5.TabIndex = 0
        '
        'MyRadioButton7
        '
        Me.MyRadioButton7.Location = New System.Drawing.Point(165, 1)
        Me.MyRadioButton7.MyLinkLable1 = Nothing
        Me.MyRadioButton7.MyLinkLable2 = Nothing
        Me.MyRadioButton7.Name = "MyRadioButton7"
        Me.MyRadioButton7.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton7.TabIndex = 1
        Me.MyRadioButton7.Text = "Select"
        '
        'MyRadioButton8
        '
        Me.MyRadioButton8.Location = New System.Drawing.Point(114, 1)
        Me.MyRadioButton8.MyLinkLable1 = Nothing
        Me.MyRadioButton8.MyLinkLable2 = Nothing
        Me.MyRadioButton8.Name = "MyRadioButton8"
        Me.MyRadioButton8.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton8.TabIndex = 0
        Me.MyRadioButton8.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(141, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.MyCheckBoxGrid5)
        Me.RadGroupBox6.Controls.Add(Me.Panel6)
        Me.RadGroupBox6.HeaderText = "RadGroupBox6"
        Me.RadGroupBox6.Location = New System.Drawing.Point(399, 139)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(330, 171)
        Me.RadGroupBox6.TabIndex = 114
        Me.RadGroupBox6.Text = "RadGroupBox6"
        '
        'MyCheckBoxGrid5
        '
        Me.MyCheckBoxGrid5.CheckedValue = Nothing
        Me.MyCheckBoxGrid5.DataSource = Nothing
        Me.MyCheckBoxGrid5.DisplayMember = "Name"
        Me.MyCheckBoxGrid5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyCheckBoxGrid5.Location = New System.Drawing.Point(10, 40)
        Me.MyCheckBoxGrid5.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.MyCheckBoxGrid5.MyShowHeadrText = False
        Me.MyCheckBoxGrid5.Name = "MyCheckBoxGrid5"
        Me.MyCheckBoxGrid5.Size = New System.Drawing.Size(310, 121)
        Me.MyCheckBoxGrid5.TabIndex = 1
        Me.MyCheckBoxGrid5.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.MyRadioButton9)
        Me.Panel6.Controls.Add(Me.MyRadioButton10)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(310, 20)
        Me.Panel6.TabIndex = 0
        '
        'MyRadioButton9
        '
        Me.MyRadioButton9.Location = New System.Drawing.Point(165, 1)
        Me.MyRadioButton9.MyLinkLable1 = Nothing
        Me.MyRadioButton9.MyLinkLable2 = Nothing
        Me.MyRadioButton9.Name = "MyRadioButton9"
        Me.MyRadioButton9.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton9.TabIndex = 1
        Me.MyRadioButton9.Text = "Select"
        '
        'MyRadioButton10
        '
        Me.MyRadioButton10.Location = New System.Drawing.Point(114, 1)
        Me.MyRadioButton10.MyLinkLable1 = Nothing
        Me.MyRadioButton10.MyLinkLable2 = Nothing
        Me.MyRadioButton10.Name = "MyRadioButton10"
        Me.MyRadioButton10.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton10.TabIndex = 0
        Me.MyRadioButton10.Text = "All"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.MyCheckBoxGrid6)
        Me.RadGroupBox7.Controls.Add(Me.Panel7)
        Me.RadGroupBox7.HeaderText = "RadGroupBox7"
        Me.RadGroupBox7.Location = New System.Drawing.Point(399, -35)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(330, 171)
        Me.RadGroupBox7.TabIndex = 113
        Me.RadGroupBox7.Text = "RadGroupBox7"
        '
        'MyCheckBoxGrid6
        '
        Me.MyCheckBoxGrid6.CheckedValue = Nothing
        Me.MyCheckBoxGrid6.DataSource = Nothing
        Me.MyCheckBoxGrid6.DisplayMember = "Name"
        Me.MyCheckBoxGrid6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyCheckBoxGrid6.Location = New System.Drawing.Point(10, 40)
        Me.MyCheckBoxGrid6.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.MyCheckBoxGrid6.MyShowHeadrText = False
        Me.MyCheckBoxGrid6.Name = "MyCheckBoxGrid6"
        Me.MyCheckBoxGrid6.Size = New System.Drawing.Size(310, 121)
        Me.MyCheckBoxGrid6.TabIndex = 1
        Me.MyCheckBoxGrid6.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.MyRadioButton11)
        Me.Panel7.Controls.Add(Me.MyRadioButton12)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(310, 20)
        Me.Panel7.TabIndex = 0
        '
        'MyRadioButton11
        '
        Me.MyRadioButton11.Location = New System.Drawing.Point(165, 1)
        Me.MyRadioButton11.MyLinkLable1 = Nothing
        Me.MyRadioButton11.MyLinkLable2 = Nothing
        Me.MyRadioButton11.Name = "MyRadioButton11"
        Me.MyRadioButton11.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton11.TabIndex = 1
        Me.MyRadioButton11.Text = "Select"
        '
        'MyRadioButton12
        '
        Me.MyRadioButton12.Location = New System.Drawing.Point(114, 1)
        Me.MyRadioButton12.MyLinkLable1 = Nothing
        Me.MyRadioButton12.MyLinkLable2 = Nothing
        Me.MyRadioButton12.Name = "MyRadioButton12"
        Me.MyRadioButton12.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton12.TabIndex = 0
        Me.MyRadioButton12.Text = "All"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011 02:29 AM"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011 02:29 AM"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLoc)
        Me.Locationgb.Controls.Add(Me.Panel3)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(728, 37)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(66, 40)
        Me.Locationgb.TabIndex = 110
        Me.Locationgb.Text = "Location"
        Me.Locationgb.Visible = False
        '
        'cbgLoc
        '
        Me.cbgLoc.CheckedValue = Nothing
        Me.cbgLoc.DataSource = Nothing
        Me.cbgLoc.DisplayMember = "Name"
        Me.cbgLoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgLoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLoc.MyShowHeadrText = False
        Me.cbgLoc.Name = "cbgLoc"
        Me.cbgLoc.Size = New System.Drawing.Size(46, 0)
        Me.cbgLoc.TabIndex = 1
        Me.cbgLoc.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocationSelect)
        Me.Panel3.Controls.Add(Me.chkLocationAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(46, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(114, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(819, 575)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.AllowAddNewRow = False
        Me.GV1.MasterTemplate.AllowEditRow = False
        Me.GV1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.GV1.MasterTemplate.ShowHeaderCellButtons = True
        Me.GV1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.GV1.Name = "GV1"
        Me.GV1.ShowGroupPanel = False
        Me.GV1.ShowHeaderCellButtons = True
        Me.GV1.Size = New System.Drawing.Size(819, 575)
        Me.GV1.TabIndex = 0
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(153, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 126
        Me.btnExport.Text = "Export"
        '
        'export
        '
        Me.export.Name = "export"
        Me.export.Text = "Export"
        '
        'PDF
        '
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(755, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(77, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 7
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(3, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = ">>>"
        '
        'MyCheckBoxGrid2
        '
        Me.MyCheckBoxGrid2.CheckedValue = Nothing
        Me.MyCheckBoxGrid2.DataSource = Nothing
        Me.MyCheckBoxGrid2.DisplayMember = "Name"
        Me.MyCheckBoxGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyCheckBoxGrid2.Location = New System.Drawing.Point(10, 40)
        Me.MyCheckBoxGrid2.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.MyCheckBoxGrid2.MyShowHeadrText = False
        Me.MyCheckBoxGrid2.Name = "MyCheckBoxGrid2"
        Me.MyCheckBoxGrid2.Size = New System.Drawing.Size(310, 121)
        Me.MyCheckBoxGrid2.TabIndex = 1
        Me.MyCheckBoxGrid2.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.MyRadioButton3)
        Me.Panel2.Controls.Add(Me.MyRadioButton4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(310, 20)
        Me.Panel2.TabIndex = 0
        '
        'MyRadioButton3
        '
        Me.MyRadioButton3.Location = New System.Drawing.Point(165, 1)
        Me.MyRadioButton3.MyLinkLable1 = Nothing
        Me.MyRadioButton3.MyLinkLable2 = Nothing
        Me.MyRadioButton3.Name = "MyRadioButton3"
        Me.MyRadioButton3.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton3.TabIndex = 1
        Me.MyRadioButton3.Text = "Select"
        '
        'MyRadioButton4
        '
        Me.MyRadioButton4.Location = New System.Drawing.Point(114, 1)
        Me.MyRadioButton4.MyLinkLable1 = Nothing
        Me.MyRadioButton4.MyLinkLable2 = Nothing
        Me.MyRadioButton4.Name = "MyRadioButton4"
        Me.MyRadioButton4.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton4.TabIndex = 0
        Me.MyRadioButton4.Text = "All"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(840, 20)
        Me.RadMenu1.TabIndex = 64
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'FrmAssetRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(840, 366)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmAssetRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Register"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblAssetCodeFinder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox11.ResumeLayout(False)
        Me.RadGroupBox11.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCategSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCategAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.chkGroupSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroupAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkAssetSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAssetAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox10.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.chkCostCentSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCostCentAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.MyRadioButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.MyRadioButton9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.MyRadioButton11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.MyRadioButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgAsset As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkAssetSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAssetAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbTax As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbBook As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyComboBox2 As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoc As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCategory As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkCategSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCategAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyCheckBoxGrid4 As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton7 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton8 As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyCheckBoxGrid5 As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton9 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton10 As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyCheckBoxGrid6 As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton11 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton12 As common.Controls.MyRadioButton
    Friend WithEvents MyCheckBoxGrid2 As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton3 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton4 As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgGroup As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkGroupSelect As common.Controls.MyRadioButton
    Friend WithEvents chkGroupAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox10 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCostCenter As common.MyCheckBoxGrid
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents chkCostCentSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCostCentAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox11 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyComboBox1 As common.Controls.MyComboBox
    Friend WithEvents export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents TxtMultiVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents TxtMultiCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCategory As common.Controls.MyLabel
    Friend WithEvents TxtMultiGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblGroup As common.Controls.MyLabel
    Friend WithEvents TxtMultiAssetId As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAssetId As common.Controls.MyLabel
    Friend WithEvents TxtMultiCostCenter As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCostCenter As common.Controls.MyLabel
    Friend WithEvents TxtMultiLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtAssetCodeFinder As common.UserControls.txtFinder
    Friend WithEvents lblAssetCodeFinder As common.Controls.MyLabel
End Class

