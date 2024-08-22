<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIssueOrReturnItemWiseSummary
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.dtpFromdate = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvCategory = New common.UserControls.MyRadGridView()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton()
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton()
        Me.lblItem = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.lblCostCenter = New common.Controls.MyLabel()
        Me.txtCostCenter = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.RdgbCostCenter = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCostCenter = New common.MyCheckBoxGrid()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.chkCostCenterSelect = New common.Controls.MyRadioButton()
        Me.chkCostCenterAll = New common.Controls.MyRadioButton()
        Me.chkDocWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkCostCentreReport = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkVehicleWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgMachine = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkMachineSelect = New common.Controls.MyRadioButton()
        Me.chkmachineAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVehicle = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkVehicleSelect = New common.Controls.MyRadioButton()
        Me.chkVehicleNoAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox13 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.ddlRptType = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.grpbxSubCategory = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgSubCategroy = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkSubCategroySelect = New common.Controls.MyRadioButton()
        Me.chkSubCategoryAll = New common.Controls.MyRadioButton()
        Me.grpbxItemWise = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgItem = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkItemSelect = New common.Controls.MyRadioButton()
        Me.chkItemAll = New common.Controls.MyRadioButton()
        Me.grpbxItemCategory = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCategory = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkCategorySelect = New common.Controls.MyRadioButton()
        Me.chkCategoryAll = New common.Controls.MyRadioButton()
        Me.grpbxDepartment = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDepartment = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkDepartmentSelect = New common.Controls.MyRadioButton()
        Me.chkDeapartmentAll = New common.Controls.MyRadioButton()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.BtnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dtpFromdate.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RdgbCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RdgbCostCenter.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.chkCostCenterSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCostCenterAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDocWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCostCentreReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVehicleWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkMachineSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkmachineAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkVehicleSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVehicleNoAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox13.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlRptType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxSubCategory.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkSubCategroySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSubCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxItemWise.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxItemCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxItemCategory.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxDepartment.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkDepartmentSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDeapartmentAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpFromdate
        '
        Me.dtpFromdate.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.dtpFromdate.Controls.Add(Me.RadGroupBox3)
        Me.dtpFromdate.Controls.Add(Me.lblItem)
        Me.dtpFromdate.Controls.Add(Me.txtItem)
        Me.dtpFromdate.Controls.Add(Me.lblCostCenter)
        Me.dtpFromdate.Controls.Add(Me.txtCostCenter)
        Me.dtpFromdate.Controls.Add(Me.lblLocation)
        Me.dtpFromdate.Controls.Add(Me.txtLocation)
        Me.dtpFromdate.Controls.Add(Me.RdgbCostCenter)
        Me.dtpFromdate.Controls.Add(Me.chkDocWise)
        Me.dtpFromdate.Controls.Add(Me.chkCostCentreReport)
        Me.dtpFromdate.Controls.Add(Me.chkVehicleWise)
        Me.dtpFromdate.Controls.Add(Me.RadGroupBox2)
        Me.dtpFromdate.Controls.Add(Me.RadGroupBox1)
        Me.dtpFromdate.Controls.Add(Me.RadGroupBox13)
        Me.dtpFromdate.Controls.Add(Me.ddlRptType)
        Me.dtpFromdate.Controls.Add(Me.RadLabel1)
        Me.dtpFromdate.Controls.Add(Me.dtpToDate)
        Me.dtpFromdate.Controls.Add(Me.dtpFromdate1)
        Me.dtpFromdate.Controls.Add(Me.grpbxSubCategory)
        Me.dtpFromdate.Controls.Add(Me.grpbxItemWise)
        Me.dtpFromdate.Controls.Add(Me.grpbxItemCategory)
        Me.dtpFromdate.Controls.Add(Me.grpbxDepartment)
        Me.dtpFromdate.Controls.Add(Me.lblToDate)
        Me.dtpFromdate.Controls.Add(Me.lblFromDate)
        Me.dtpFromdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFromdate.HeaderText = ""
        Me.dtpFromdate.Location = New System.Drawing.Point(0, 0)
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.dtpFromdate.Size = New System.Drawing.Size(885, 519)
        Me.dtpFromdate.TabIndex = 0
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox3.Controls.Add(Me.gvCategory)
        Me.RadGroupBox3.Controls.Add(Me.Panel8)
        Me.RadGroupBox3.HeaderText = "Category"
        Me.RadGroupBox3.Location = New System.Drawing.Point(447, 38)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(412, 445)
        Me.RadGroupBox3.TabIndex = 379
        Me.RadGroupBox3.Text = "Category"
        Me.RadGroupBox3.Visible = False
        '
        'gvCategory
        '
        Me.gvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCategory.Location = New System.Drawing.Point(10, 40)
        '
        '
        '
        Me.gvCategory.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCategory.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCategory.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvCategory.MyStopExport = False
        Me.gvCategory.Name = "gvCategory"
        Me.gvCategory.ShowHeaderCellButtons = True
        Me.gvCategory.Size = New System.Drawing.Size(392, 395)
        Me.gvCategory.TabIndex = 2
        Me.gvCategory.VarID = ""
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.SplitContainer2)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(392, 20)
        Me.Panel8.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCategorySelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCategoryAll)
        Me.SplitContainer2.Size = New System.Drawing.Size(392, 20)
        Me.SplitContainer2.SplitterDistance = 189
        Me.SplitContainer2.TabIndex = 2
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(93, 1)
        Me.rbtnCategorySelect.MyLinkLable1 = Nothing
        Me.rbtnCategorySelect.MyLinkLable2 = Nothing
        Me.rbtnCategorySelect.Name = "rbtnCategorySelect"
        Me.rbtnCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCategorySelect.TabIndex = 2
        Me.rbtnCategorySelect.Text = "Select"
        '
        'rbtnCategoryAll
        '
        Me.rbtnCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(54, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 1
        Me.rbtnCategoryAll.Text = "All"
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(13, 86)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 378
        Me.lblItem.Text = "Item"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(98, 85)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.lblItem
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(344, 19)
        Me.txtItem.TabIndex = 377
        '
        'lblCostCenter
        '
        Me.lblCostCenter.FieldName = Nothing
        Me.lblCostCenter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenter.Location = New System.Drawing.Point(12, 62)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(65, 18)
        Me.lblCostCenter.TabIndex = 376
        Me.lblCostCenter.Text = "Cost Center"
        '
        'txtCostCenter
        '
        Me.txtCostCenter.arrDispalyMember = Nothing
        Me.txtCostCenter.arrValueMember = Nothing
        Me.txtCostCenter.Location = New System.Drawing.Point(97, 61)
        Me.txtCostCenter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostCenter.MyLinkLable1 = Me.lblCostCenter
        Me.txtCostCenter.MyLinkLable2 = Nothing
        Me.txtCostCenter.MyNullText = "All"
        Me.txtCostCenter.Name = "txtCostCenter"
        Me.txtCostCenter.Size = New System.Drawing.Size(344, 19)
        Me.txtCostCenter.TabIndex = 375
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 38)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 374
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(98, 37)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 373
        '
        'RdgbCostCenter
        '
        Me.RdgbCostCenter.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RdgbCostCenter.Controls.Add(Me.cbgCostCenter)
        Me.RdgbCostCenter.Controls.Add(Me.Panel7)
        Me.RdgbCostCenter.HeaderText = "Cost Center"
        Me.RdgbCostCenter.Location = New System.Drawing.Point(81, 282)
        Me.RdgbCostCenter.Name = "RdgbCostCenter"
        Me.RdgbCostCenter.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RdgbCostCenter.Size = New System.Drawing.Size(98, 52)
        Me.RdgbCostCenter.TabIndex = 82
        Me.RdgbCostCenter.Text = "Cost Center"
        Me.RdgbCostCenter.Visible = False
        '
        'cbgCostCenter
        '
        Me.cbgCostCenter.AccessibleName = ""
        Me.cbgCostCenter.CheckedValue = Nothing
        Me.cbgCostCenter.DataSource = Nothing
        Me.cbgCostCenter.DisplayMember = "Name"
        Me.cbgCostCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCostCenter.Location = New System.Drawing.Point(10, 40)
        Me.cbgCostCenter.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCostCenter.MyShowHeadrText = False
        Me.cbgCostCenter.Name = "cbgCostCenter"
        Me.cbgCostCenter.Size = New System.Drawing.Size(78, 2)
        Me.cbgCostCenter.TabIndex = 1
        Me.cbgCostCenter.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.chkCostCenterSelect)
        Me.Panel7.Controls.Add(Me.chkCostCenterAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(78, 20)
        Me.Panel7.TabIndex = 0
        '
        'chkCostCenterSelect
        '
        Me.chkCostCenterSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkCostCenterSelect.MyLinkLable1 = Nothing
        Me.chkCostCenterSelect.MyLinkLable2 = Nothing
        Me.chkCostCenterSelect.Name = "chkCostCenterSelect"
        Me.chkCostCenterSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCostCenterSelect.TabIndex = 1
        Me.chkCostCenterSelect.Text = "Select"
        '
        'chkCostCenterAll
        '
        Me.chkCostCenterAll.Location = New System.Drawing.Point(128, 1)
        Me.chkCostCenterAll.MyLinkLable1 = Nothing
        Me.chkCostCenterAll.MyLinkLable2 = Nothing
        Me.chkCostCenterAll.Name = "chkCostCenterAll"
        Me.chkCostCenterAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCostCenterAll.TabIndex = 0
        Me.chkCostCenterAll.Text = "All"
        '
        'chkDocWise
        '
        Me.chkDocWise.Location = New System.Drawing.Point(304, 7)
        Me.chkDocWise.Name = "chkDocWise"
        Me.chkDocWise.Size = New System.Drawing.Size(99, 18)
        Me.chkDocWise.TabIndex = 81
        Me.chkDocWise.Text = "Document Wise"
        '
        'chkCostCentreReport
        '
        Me.chkCostCentreReport.Location = New System.Drawing.Point(580, 8)
        Me.chkCostCentreReport.Name = "chkCostCentreReport"
        Me.chkCostCentreReport.Size = New System.Drawing.Size(143, 18)
        Me.chkCostCentreReport.TabIndex = 79
        Me.chkCostCentreReport.Text = "Cost Centre Excel Report"
        Me.chkCostCentreReport.Visible = False
        '
        'chkVehicleWise
        '
        Me.chkVehicleWise.Location = New System.Drawing.Point(596, 23)
        Me.chkVehicleWise.Name = "chkVehicleWise"
        Me.chkVehicleWise.Size = New System.Drawing.Size(83, 18)
        Me.chkVehicleWise.TabIndex = 78
        Me.chkVehicleWise.Text = "Vehicle Wise"
        Me.chkVehicleWise.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgMachine)
        Me.RadGroupBox2.Controls.Add(Me.Panel6)
        Me.RadGroupBox2.HeaderText = "Machine No"
        Me.RadGroupBox2.Location = New System.Drawing.Point(284, 383)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(78, 162)
        Me.RadGroupBox2.TabIndex = 15
        Me.RadGroupBox2.Text = "Machine No"
        Me.RadGroupBox2.Visible = False
        '
        'cbgMachine
        '
        Me.cbgMachine.AccessibleName = "cbgMachine"
        Me.cbgMachine.CheckedValue = Nothing
        Me.cbgMachine.DataSource = Nothing
        Me.cbgMachine.DisplayMember = "Name"
        Me.cbgMachine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMachine.Location = New System.Drawing.Point(10, 40)
        Me.cbgMachine.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMachine.MyShowHeadrText = False
        Me.cbgMachine.Name = "cbgMachine"
        Me.cbgMachine.Size = New System.Drawing.Size(58, 112)
        Me.cbgMachine.TabIndex = 1
        Me.cbgMachine.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkMachineSelect)
        Me.Panel6.Controls.Add(Me.chkmachineAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(58, 20)
        Me.Panel6.TabIndex = 0
        '
        'chkMachineSelect
        '
        Me.chkMachineSelect.AccessibleName = "chkMachineSelect"
        Me.chkMachineSelect.Location = New System.Drawing.Point(71, 2)
        Me.chkMachineSelect.MyLinkLable1 = Nothing
        Me.chkMachineSelect.MyLinkLable2 = Nothing
        Me.chkMachineSelect.Name = "chkMachineSelect"
        Me.chkMachineSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkMachineSelect.TabIndex = 1
        Me.chkMachineSelect.Text = "Select"
        '
        'chkmachineAll
        '
        Me.chkmachineAll.AccessibleName = "chkmachineAll"
        Me.chkmachineAll.Location = New System.Drawing.Point(25, 1)
        Me.chkmachineAll.MyLinkLable1 = Nothing
        Me.chkmachineAll.MyLinkLable2 = Nothing
        Me.chkmachineAll.Name = "chkmachineAll"
        Me.chkmachineAll.Size = New System.Drawing.Size(33, 18)
        Me.chkmachineAll.TabIndex = 0
        Me.chkmachineAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgVehicle)
        Me.RadGroupBox1.Controls.Add(Me.Panel5)
        Me.RadGroupBox1.HeaderText = "Vehicle No"
        Me.RadGroupBox1.Location = New System.Drawing.Point(380, 383)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(84, 162)
        Me.RadGroupBox1.TabIndex = 14
        Me.RadGroupBox1.Text = "Vehicle No"
        Me.RadGroupBox1.Visible = False
        '
        'cbgVehicle
        '
        Me.cbgVehicle.AccessibleName = "cbgVehicle"
        Me.cbgVehicle.CheckedValue = Nothing
        Me.cbgVehicle.DataSource = Nothing
        Me.cbgVehicle.DisplayMember = "Name"
        Me.cbgVehicle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVehicle.Location = New System.Drawing.Point(10, 40)
        Me.cbgVehicle.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVehicle.MyShowHeadrText = False
        Me.cbgVehicle.Name = "cbgVehicle"
        Me.cbgVehicle.Size = New System.Drawing.Size(64, 112)
        Me.cbgVehicle.TabIndex = 1
        Me.cbgVehicle.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkVehicleSelect)
        Me.Panel5.Controls.Add(Me.chkVehicleNoAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(64, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkVehicleSelect
        '
        Me.chkVehicleSelect.AccessibleName = "chkVehicleSelect"
        Me.chkVehicleSelect.Location = New System.Drawing.Point(115, 1)
        Me.chkVehicleSelect.MyLinkLable1 = Nothing
        Me.chkVehicleSelect.MyLinkLable2 = Nothing
        Me.chkVehicleSelect.Name = "chkVehicleSelect"
        Me.chkVehicleSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVehicleSelect.TabIndex = 1
        Me.chkVehicleSelect.Text = "Select"
        '
        'chkVehicleNoAll
        '
        Me.chkVehicleNoAll.AccessibleName = "chkVehicleNoAll"
        Me.chkVehicleNoAll.Location = New System.Drawing.Point(52, 1)
        Me.chkVehicleNoAll.MyLinkLable1 = Nothing
        Me.chkVehicleNoAll.MyLinkLable2 = Nothing
        Me.chkVehicleNoAll.Name = "chkVehicleNoAll"
        Me.chkVehicleNoAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVehicleNoAll.TabIndex = 0
        Me.chkVehicleNoAll.Text = "All"
        '
        'RadGroupBox13
        '
        Me.RadGroupBox13.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox13.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox13.Controls.Add(Me.Panel9)
        Me.RadGroupBox13.HeaderText = " Location"
        Me.RadGroupBox13.Location = New System.Drawing.Point(80, 224)
        Me.RadGroupBox13.Name = "RadGroupBox13"
        Me.RadGroupBox13.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox13.Size = New System.Drawing.Size(109, 52)
        Me.RadGroupBox13.TabIndex = 77
        Me.RadGroupBox13.Text = " Location"
        Me.RadGroupBox13.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(89, 0)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkLocationSelect)
        Me.Panel9.Controls.Add(Me.chkLocationAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(89, 25)
        Me.Panel9.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(166, 4)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(124, 4)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'ddlRptType
        '
        Me.ddlRptType.AutoCompleteDisplayMember = Nothing
        Me.ddlRptType.AutoCompleteValueMember = Nothing
        Me.ddlRptType.DropDownAnimationEnabled = True
        RadListDataItem1.Text = "Net Issue"
        RadListDataItem2.Text = "Net Issue Return"
        Me.ddlRptType.Items.Add(RadListDataItem1)
        Me.ddlRptType.Items.Add(RadListDataItem2)
        Me.ddlRptType.Location = New System.Drawing.Point(472, 6)
        Me.ddlRptType.Name = "ddlRptType"
        Me.ddlRptType.Size = New System.Drawing.Size(102, 20)
        Me.ddlRptType.TabIndex = 23
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(399, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(67, 18)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Report Type"
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(219, 6)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 21
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "01/02/2012"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(75, 6)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(88, 20)
        Me.dtpFromdate1.TabIndex = 20
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01/02/2012"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'grpbxSubCategory
        '
        Me.grpbxSubCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxSubCategory.Controls.Add(Me.cbgSubCategroy)
        Me.grpbxSubCategory.Controls.Add(Me.Panel3)
        Me.grpbxSubCategory.HeaderText = "Sub Category"
        Me.grpbxSubCategory.Location = New System.Drawing.Point(480, 380)
        Me.grpbxSubCategory.Name = "grpbxSubCategory"
        Me.grpbxSubCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxSubCategory.Size = New System.Drawing.Size(121, 165)
        Me.grpbxSubCategory.TabIndex = 16
        Me.grpbxSubCategory.Text = "Sub Category"
        Me.grpbxSubCategory.Visible = False
        '
        'cbgSubCategroy
        '
        Me.cbgSubCategroy.AccessibleName = ""
        Me.cbgSubCategroy.CheckedValue = Nothing
        Me.cbgSubCategroy.DataSource = Nothing
        Me.cbgSubCategroy.DisplayMember = "Name"
        Me.cbgSubCategroy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSubCategroy.Location = New System.Drawing.Point(10, 40)
        Me.cbgSubCategroy.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSubCategroy.MyShowHeadrText = False
        Me.cbgSubCategroy.Name = "cbgSubCategroy"
        Me.cbgSubCategroy.Size = New System.Drawing.Size(101, 115)
        Me.cbgSubCategroy.TabIndex = 1
        Me.cbgSubCategroy.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkSubCategroySelect)
        Me.Panel3.Controls.Add(Me.chkSubCategoryAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(101, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkSubCategroySelect
        '
        Me.chkSubCategroySelect.Location = New System.Drawing.Point(106, 1)
        Me.chkSubCategroySelect.MyLinkLable1 = Nothing
        Me.chkSubCategroySelect.MyLinkLable2 = Nothing
        Me.chkSubCategroySelect.Name = "chkSubCategroySelect"
        Me.chkSubCategroySelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSubCategroySelect.TabIndex = 1
        Me.chkSubCategroySelect.Text = "Select"
        '
        'chkSubCategoryAll
        '
        Me.chkSubCategoryAll.Location = New System.Drawing.Point(46, 1)
        Me.chkSubCategoryAll.MyLinkLable1 = Nothing
        Me.chkSubCategoryAll.MyLinkLable2 = Nothing
        Me.chkSubCategoryAll.Name = "chkSubCategoryAll"
        Me.chkSubCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSubCategoryAll.TabIndex = 0
        Me.chkSubCategoryAll.Text = "All"
        '
        'grpbxItemWise
        '
        Me.grpbxItemWise.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxItemWise.Controls.Add(Me.cbgItem)
        Me.grpbxItemWise.Controls.Add(Me.Panel2)
        Me.grpbxItemWise.HeaderText = "Item"
        Me.grpbxItemWise.Location = New System.Drawing.Point(125, 166)
        Me.grpbxItemWise.Name = "grpbxItemWise"
        Me.grpbxItemWise.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemWise.Size = New System.Drawing.Size(64, 52)
        Me.grpbxItemWise.TabIndex = 15
        Me.grpbxItemWise.Text = "Item"
        Me.grpbxItemWise.Visible = False
        '
        'cbgItem
        '
        Me.cbgItem.AccessibleName = ""
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(44, 2)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkItemSelect)
        Me.Panel2.Controls.Add(Me.chkItemAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(44, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(161, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.Location = New System.Drawing.Point(123, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'grpbxItemCategory
        '
        Me.grpbxItemCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxItemCategory.Controls.Add(Me.cbgCategory)
        Me.grpbxItemCategory.Controls.Add(Me.Panel1)
        Me.grpbxItemCategory.HeaderText = "Category"
        Me.grpbxItemCategory.Location = New System.Drawing.Point(632, 385)
        Me.grpbxItemCategory.Name = "grpbxItemCategory"
        Me.grpbxItemCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemCategory.Size = New System.Drawing.Size(95, 165)
        Me.grpbxItemCategory.TabIndex = 14
        Me.grpbxItemCategory.Text = "Category"
        Me.grpbxItemCategory.Visible = False
        '
        'cbgCategory
        '
        Me.cbgCategory.AccessibleName = ""
        Me.cbgCategory.CheckedValue = Nothing
        Me.cbgCategory.DataSource = Nothing
        Me.cbgCategory.DisplayMember = "Name"
        Me.cbgCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCategory.Location = New System.Drawing.Point(10, 40)
        Me.cbgCategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCategory.MyShowHeadrText = False
        Me.cbgCategory.Name = "cbgCategory"
        Me.cbgCategory.Size = New System.Drawing.Size(75, 115)
        Me.cbgCategory.TabIndex = 1
        Me.cbgCategory.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCategorySelect)
        Me.Panel1.Controls.Add(Me.chkCategoryAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(75, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkCategorySelect
        '
        Me.chkCategorySelect.Location = New System.Drawing.Point(115, 1)
        Me.chkCategorySelect.MyLinkLable1 = Nothing
        Me.chkCategorySelect.MyLinkLable2 = Nothing
        Me.chkCategorySelect.Name = "chkCategorySelect"
        Me.chkCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCategorySelect.TabIndex = 1
        Me.chkCategorySelect.Text = "Select"
        '
        'chkCategoryAll
        '
        Me.chkCategoryAll.Location = New System.Drawing.Point(52, 1)
        Me.chkCategoryAll.MyLinkLable1 = Nothing
        Me.chkCategoryAll.MyLinkLable2 = Nothing
        Me.chkCategoryAll.Name = "chkCategoryAll"
        Me.chkCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCategoryAll.TabIndex = 0
        Me.chkCategoryAll.Text = "All"
        '
        'grpbxDepartment
        '
        Me.grpbxDepartment.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxDepartment.Controls.Add(Me.cbgDepartment)
        Me.grpbxDepartment.Controls.Add(Me.Panel4)
        Me.grpbxDepartment.HeaderText = "Department"
        Me.grpbxDepartment.Location = New System.Drawing.Point(172, 383)
        Me.grpbxDepartment.Name = "grpbxDepartment"
        Me.grpbxDepartment.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxDepartment.Size = New System.Drawing.Size(97, 162)
        Me.grpbxDepartment.TabIndex = 13
        Me.grpbxDepartment.Text = "Department"
        Me.grpbxDepartment.Visible = False
        '
        'cbgDepartment
        '
        Me.cbgDepartment.AccessibleName = ""
        Me.cbgDepartment.CheckedValue = Nothing
        Me.cbgDepartment.DataSource = Nothing
        Me.cbgDepartment.DisplayMember = "Name"
        Me.cbgDepartment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDepartment.Location = New System.Drawing.Point(10, 40)
        Me.cbgDepartment.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDepartment.MyShowHeadrText = False
        Me.cbgDepartment.Name = "cbgDepartment"
        Me.cbgDepartment.Size = New System.Drawing.Size(77, 112)
        Me.cbgDepartment.TabIndex = 1
        Me.cbgDepartment.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkDepartmentSelect)
        Me.Panel4.Controls.Add(Me.chkDeapartmentAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(77, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkDepartmentSelect
        '
        Me.chkDepartmentSelect.Location = New System.Drawing.Point(99, 1)
        Me.chkDepartmentSelect.MyLinkLable1 = Nothing
        Me.chkDepartmentSelect.MyLinkLable2 = Nothing
        Me.chkDepartmentSelect.Name = "chkDepartmentSelect"
        Me.chkDepartmentSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkDepartmentSelect.TabIndex = 1
        Me.chkDepartmentSelect.Text = "Select"
        '
        'chkDeapartmentAll
        '
        Me.chkDeapartmentAll.Location = New System.Drawing.Point(52, 1)
        Me.chkDeapartmentAll.MyLinkLable1 = Nothing
        Me.chkDeapartmentAll.MyLinkLable2 = Nothing
        Me.chkDeapartmentAll.Name = "chkDeapartmentAll"
        Me.chkDeapartmentAll.Size = New System.Drawing.Size(33, 18)
        Me.chkDeapartmentAll.TabIndex = 0
        Me.chkDeapartmentAll.Text = "All"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(169, 8)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 2
        Me.lblToDate.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Location = New System.Drawing.Point(13, 6)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 1
        Me.lblFromDate.Text = "From Date"
        '
        'BtnGo
        '
        Me.BtnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnGo.Location = New System.Drawing.Point(14, 5)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(76, 24)
        Me.BtnGo.TabIndex = 83
        Me.BtnGo.Text = ">>>>"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(95, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(86, 24)
        Me.btnExport.TabIndex = 80
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(187, 5)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(76, 24)
        Me.btnreset.TabIndex = 19
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(818, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 18
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(269, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 24)
        Me.btnPrint.TabIndex = 17
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(906, 567)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromdate)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(885, 519)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(885, 519)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(885, 519)
        Me.gv.TabIndex = 0
        Me.gv.VarID = ""
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(906, 611)
        Me.SplitContainer1.SplitterDistance = 567
        Me.SplitContainer1.TabIndex = 2
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(906, 20)
        Me.RadMenu1.TabIndex = 4
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
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
        'FrmIssueOrReturnItemWiseSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 631)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmIssueOrReturnItemWiseSummary"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Net Iss-Ret Item wise summary"
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dtpFromdate.ResumeLayout(False)
        Me.dtpFromdate.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RdgbCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RdgbCostCenter.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.chkCostCenterSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCostCenterAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDocWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCostCentreReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVehicleWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkMachineSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkmachineAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkVehicleSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVehicleNoAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox13.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlRptType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxSubCategory.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkSubCategroySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSubCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxItemWise.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxItemCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxItemCategory.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxDepartment.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkDepartmentSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDeapartmentAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpFromdate As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents grpbxItemCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCategory As common.MyCheckBoxGrid
    Friend WithEvents chkCategorySelect As common.Controls.MyRadioButton
    Friend WithEvents chkCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents grpbxDepartment As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDepartment As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkDepartmentSelect As common.Controls.MyRadioButton
    Friend WithEvents chkDeapartmentAll As common.Controls.MyRadioButton
    Friend WithEvents grpbxSubCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSubCategroy As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSubCategroySelect As common.Controls.MyRadioButton
    Friend WithEvents chkSubCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents grpbxItemWise As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents ddlRptType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadGroupBox13 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVehicle As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkVehicleSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVehicleNoAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgMachine As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkMachineSelect As common.Controls.MyRadioButton
    Friend WithEvents chkmachineAll As common.Controls.MyRadioButton
    Friend WithEvents chkVehicleWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkCostCentreReport As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkDocWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RdgbCostCenter As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCostCenter As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents chkCostCenterSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCostCenterAll As common.Controls.MyRadioButton
    Friend WithEvents BtnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCostCenter As common.Controls.MyLabel
    Friend WithEvents txtCostCenter As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvCategory As common.UserControls.MyRadGridView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

