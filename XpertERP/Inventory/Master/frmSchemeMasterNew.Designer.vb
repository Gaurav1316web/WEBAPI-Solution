<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSchemeMasterNew
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem
        Me.export_main = New Telerik.WinControls.UI.RadMenuItem
        Me.export_beneficial = New Telerik.WinControls.UI.RadMenuItem
        Me.Export_Criteria = New Telerik.WinControls.UI.RadMenuItem
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem
        Me.Import_Main = New Telerik.WinControls.UI.RadMenuItem
        Me.import_beneficial = New Telerik.WinControls.UI.RadMenuItem
        Me.import_criteria = New Telerik.WinControls.UI.RadMenuItem
        Me.rmiClose = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtPercentage = New common.MyNumBox
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.txtAmount = New common.MyNumBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtQty = New common.MyNumBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.lblUnitCodeDesc = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.lblMainItemDesc = New common.Controls.MyLabel
        Me.ddlType = New common.Controls.MyComboBox
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox
        Me.txtDesc = New common.Controls.MyTextBox
        Me.dtpScheme = New common.Controls.MyDateTimePicker
        Me.fndScheme = New common.UserControls.txtNavigator
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.txtUnitCode = New common.UserControls.txtFinder
        Me.lblUnit = New common.Controls.MyLabel
        Me.dtpInactive = New common.Controls.MyDateTimePicker
        Me.txtMainItem = New common.UserControls.txtFinder
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.ddlBasicPrice = New common.Controls.MyComboBox
        Me.RadLabel9 = New common.Controls.MyLabel
        Me.txtComment = New common.Controls.MyTextBox
        Me.ddlmrp = New common.Controls.MyComboBox
        Me.lblmrp = New common.Controls.MyLabel
        Me.gvItem = New common.UserControls.MyRadGridView
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.lblCriteria = New common.Controls.MyLabel
        Me.txtCriteria = New common.UserControls.txtFinder
        Me.btnSelect = New Telerik.WinControls.UI.RadButton
        Me.ddlCriteria = New common.Controls.MyComboBox
        Me.RadLabel13 = New common.Controls.MyLabel
        Me.gvCustomer = New common.UserControls.MyRadGridView
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ERP.ucCustomFields
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMainItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpScheme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBasicPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlmrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(884, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport, Me.rmiClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmiExport
        '
        Me.rmiExport.AccessibleDescription = "Export"
        Me.rmiExport.AccessibleName = "Export"
        Me.rmiExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.export_main, Me.export_beneficial, Me.Export_Criteria})
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        Me.rmiExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'export_main
        '
        Me.export_main.AccessibleDescription = "Main Item Detail"
        Me.export_main.AccessibleName = "Main Item Detail"
        Me.export_main.Name = "export_main"
        Me.export_main.Text = "Main Item Detail"
        Me.export_main.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'export_beneficial
        '
        Me.export_beneficial.AccessibleDescription = "Beneficial Detail"
        Me.export_beneficial.AccessibleName = "Beneficial Detail"
        Me.export_beneficial.Name = "export_beneficial"
        Me.export_beneficial.Text = "Scheme Items Detail"
        Me.export_beneficial.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Export_Criteria
        '
        Me.Export_Criteria.AccessibleDescription = "Criteria Detail"
        Me.Export_Criteria.AccessibleName = "Criteria Detail"
        Me.Export_Criteria.Name = "Export_Criteria"
        Me.Export_Criteria.Text = "Criteria Detail"
        Me.Export_Criteria.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmiImport
        '
        Me.rmiImport.AccessibleDescription = "Import"
        Me.rmiImport.AccessibleName = "Import"
        Me.rmiImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import_Main, Me.import_beneficial, Me.import_criteria})
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        Me.rmiImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Import_Main
        '
        Me.Import_Main.AccessibleDescription = "Main item Detail"
        Me.Import_Main.AccessibleName = "Main item Detail"
        Me.Import_Main.Name = "Import_Main"
        Me.Import_Main.Text = "Main item Detail"
        Me.Import_Main.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'import_beneficial
        '
        Me.import_beneficial.AccessibleDescription = "Beneficial Detail"
        Me.import_beneficial.AccessibleName = "Beneficial Detail"
        Me.import_beneficial.Name = "import_beneficial"
        Me.import_beneficial.Text = "Scheme Items Detail"
        Me.import_beneficial.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'import_criteria
        '
        Me.import_criteria.AccessibleDescription = "Criteria Detail"
        Me.import_criteria.AccessibleName = "Criteria Detail"
        Me.import_criteria.Name = "import_criteria"
        Me.import_criteria.Text = "Criteria Detail"
        Me.import_criteria.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmiClose
        '
        Me.rmiClose.AccessibleDescription = "Close"
        Me.rmiClose.AccessibleName = "Close"
        Me.rmiClose.Name = "rmiClose"
        Me.rmiClose.Text = "Close"
        Me.rmiClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(884, 439)
        Me.SplitContainer1.SplitterDistance = 409
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(884, 409)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(863, 361)
        Me.RadPageViewPage1.Text = "Customer"
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
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtPercentage)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtAmount)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtQty)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblUnitCodeDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblMainItemDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkInactive)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpScheme)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndScheme)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtUnitCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpInactive)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtMainItem)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel10)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel7)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlBasicPrice)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel9)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ddlmrp)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblUnit)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblmrp)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer3.Size = New System.Drawing.Size(863, 361)
        Me.SplitContainer3.SplitterDistance = 133
        Me.SplitContainer3.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(415, 89)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 26
        Me.MyLabel2.Text = "Basic Price"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(576, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(16, 16)
        Me.MyLabel1.TabIndex = 27
        Me.MyLabel1.Text = "%"
        '
        'txtPercentage
        '
        Me.txtPercentage.BackColor = System.Drawing.Color.White
        Me.txtPercentage.DecimalPlaces = 2
        Me.txtPercentage.Location = New System.Drawing.Point(599, 87)
        Me.txtPercentage.MendatroryField = False
        Me.txtPercentage.MyLinkLable1 = Nothing
        Me.txtPercentage.MyLinkLable2 = Nothing
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.Size = New System.Drawing.Size(43, 20)
        Me.txtPercentage.TabIndex = 11
        Me.txtPercentage.Text = "0"
        Me.txtPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPercentage.Value = 0
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(5, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(78, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Scheme Code"
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.White
        Me.txtAmount.DecimalPlaces = 2
        Me.txtAmount.Location = New System.Drawing.Point(703, 87)
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Nothing
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(79, 20)
        Me.txtAmount.TabIndex = 12
        Me.txtAmount.Text = "0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmount.Value = 0
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(5, 24)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "Description"
        '
        'txtQty
        '
        Me.txtQty.BackColor = System.Drawing.Color.White
        Me.txtQty.DecimalPlaces = 2
        Me.txtQty.Location = New System.Drawing.Point(349, 87)
        Me.txtQty.MendatroryField = False
        Me.txtQty.MyLinkLable1 = Nothing
        Me.txtQty.MyLinkLable2 = Nothing
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(54, 20)
        Me.txtQty.TabIndex = 9
        Me.txtQty.Text = "0"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQty.Value = 0
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(370, 3)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 21
        Me.RadLabel3.Text = "Date"
        '
        'lblUnitCodeDesc
        '
        Me.lblUnitCodeDesc.AutoSize = False
        Me.lblUnitCodeDesc.BorderVisible = True
        Me.lblUnitCodeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitCodeDesc.Location = New System.Drawing.Point(283, 67)
        Me.lblUnitCodeDesc.Name = "lblUnitCodeDesc"
        Me.lblUnitCodeDesc.Size = New System.Drawing.Size(500, 18)
        Me.lblUnitCodeDesc.TabIndex = 24
        Me.lblUnitCodeDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(5, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel4.TabIndex = 16
        Me.RadLabel4.Text = "Scheme Type"
        '
        'lblMainItemDesc
        '
        Me.lblMainItemDesc.AutoSize = False
        Me.lblMainItemDesc.BorderVisible = True
        Me.lblMainItemDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainItemDesc.Location = New System.Drawing.Point(283, 45)
        Me.lblMainItemDesc.Name = "lblMainItemDesc"
        Me.lblMainItemDesc.Size = New System.Drawing.Size(500, 18)
        Me.lblMainItemDesc.TabIndex = 23
        Me.lblMainItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ddlType
        '
        Me.ddlType.AllowShowFocusCues = False
        Me.ddlType.AutoCompleteDisplayMember = Nothing
        Me.ddlType.AutoCompleteValueMember = Nothing
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlType.Location = New System.Drawing.Point(120, 89)
        Me.ddlType.MendatroryField = False
        Me.ddlType.MyLinkLable1 = Me.RadLabel4
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.Size = New System.Drawing.Size(157, 18)
        Me.ddlType.TabIndex = 8
        '
        'chkInactive
        '
        Me.chkInactive.Location = New System.Drawing.Point(528, 2)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 18)
        Me.chkInactive.TabIndex = 3
        Me.chkInactive.Text = "Inactive"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(120, 24)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(663, 18)
        Me.txtDesc.TabIndex = 5
        '
        'dtpScheme
        '
        Me.dtpScheme.CustomFormat = "dd/MM/yyyy"
        Me.dtpScheme.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpScheme.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpScheme.Location = New System.Drawing.Point(406, 2)
        Me.dtpScheme.MendatroryField = False
        Me.dtpScheme.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpScheme.MyLinkLable1 = Me.RadLabel3
        Me.dtpScheme.MyLinkLable2 = Nothing
        Me.dtpScheme.Name = "dtpScheme"
        Me.dtpScheme.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpScheme.Size = New System.Drawing.Size(106, 18)
        Me.dtpScheme.TabIndex = 2
        Me.dtpScheme.TabStop = False
        Me.dtpScheme.Text = "17/05/2011"
        Me.dtpScheme.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'fndScheme
        '
        Me.fndScheme.Location = New System.Drawing.Point(120, 2)
        Me.fndScheme.MendatroryField = True
        Me.fndScheme.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndScheme.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndScheme.MyLinkLable1 = Me.RadLabel1
        Me.fndScheme.MyLinkLable2 = Nothing
        Me.fndScheme.MyMaxLength = 32767
        Me.fndScheme.MyReadOnly = False
        Me.fndScheme.Name = "fndScheme"
        Me.fndScheme.Size = New System.Drawing.Size(217, 18)
        Me.fndScheme.TabIndex = 0
        Me.fndScheme.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(614, 3)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel5.TabIndex = 22
        Me.RadLabel5.Text = "InActive Date"
        '
        'txtUnitCode
        '
        Me.txtUnitCode.Location = New System.Drawing.Point(120, 67)
        Me.txtUnitCode.MendatroryField = True
        Me.txtUnitCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnitCode.MyLinkLable1 = Me.lblUnit
        Me.txtUnitCode.MyLinkLable2 = Nothing
        Me.txtUnitCode.MyReadOnly = False
        Me.txtUnitCode.MyShowMasterFormButton = False
        Me.txtUnitCode.Name = "txtUnitCode"
        Me.txtUnitCode.Size = New System.Drawing.Size(156, 18)
        Me.txtUnitCode.TabIndex = 7
        Me.txtUnitCode.Value = ""
        '
        'lblUnit
        '
        Me.lblUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.Location = New System.Drawing.Point(5, 68)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(26, 16)
        Me.lblUnit.TabIndex = 17
        Me.lblUnit.Text = "Unit"
        '
        'dtpInactive
        '
        Me.dtpInactive.CustomFormat = "dd/MM/yyyy"
        Me.dtpInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInactive.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInactive.Location = New System.Drawing.Point(698, 2)
        Me.dtpInactive.MendatroryField = False
        Me.dtpInactive.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInactive.MyLinkLable1 = Me.RadLabel5
        Me.dtpInactive.MyLinkLable2 = Nothing
        Me.dtpInactive.Name = "dtpInactive"
        Me.dtpInactive.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpInactive.Size = New System.Drawing.Size(85, 18)
        Me.dtpInactive.TabIndex = 4
        Me.dtpInactive.TabStop = False
        Me.dtpInactive.Text = "17/05/2011"
        Me.dtpInactive.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'txtMainItem
        '
        Me.txtMainItem.Location = New System.Drawing.Point(120, 45)
        Me.txtMainItem.MendatroryField = True
        Me.txtMainItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMainItem.MyLinkLable1 = Me.RadLabel6
        Me.txtMainItem.MyLinkLable2 = Nothing
        Me.txtMainItem.MyReadOnly = False
        Me.txtMainItem.MyShowMasterFormButton = False
        Me.txtMainItem.Name = "txtMainItem"
        Me.txtMainItem.Size = New System.Drawing.Size(157, 18)
        Me.txtMainItem.TabIndex = 6
        Me.txtMainItem.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(5, 48)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(56, 16)
        Me.RadLabel6.TabIndex = 18
        Me.RadLabel6.Text = "Main Item"
        '
        'RadLabel10
        '
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(650, 90)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel10.TabIndex = 28
        Me.RadLabel10.Text = "Amount"
        '
        'RadLabel7
        '
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(288, 89)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel7.TabIndex = 25
        Me.RadLabel7.Text = "Quantity"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(336, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 1
        '
        'ddlBasicPrice
        '
        Me.ddlBasicPrice.AllowShowFocusCues = False
        Me.ddlBasicPrice.AutoCompleteDisplayMember = Nothing
        Me.ddlBasicPrice.AutoCompleteValueMember = Nothing
        Me.ddlBasicPrice.Location = New System.Drawing.Point(484, 87)
        Me.ddlBasicPrice.MendatroryField = False
        Me.ddlBasicPrice.MyLinkLable1 = Nothing
        Me.ddlBasicPrice.MyLinkLable2 = Nothing
        Me.ddlBasicPrice.Name = "ddlBasicPrice"
        Me.ddlBasicPrice.Size = New System.Drawing.Size(86, 20)
        Me.ddlBasicPrice.TabIndex = 10
        '
        'RadLabel9
        '
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(288, 113)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel9.TabIndex = 29
        Me.RadLabel9.Text = "Comment"
        '
        'txtComment
        '
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.Location = New System.Drawing.Point(349, 111)
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel9
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(434, 18)
        Me.txtComment.TabIndex = 14
        '
        'ddlmrp
        '
        Me.ddlmrp.AllowShowFocusCues = False
        Me.ddlmrp.AutoCompleteDisplayMember = Nothing
        Me.ddlmrp.AutoCompleteValueMember = Nothing
        Me.ddlmrp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlmrp.Location = New System.Drawing.Point(120, 111)
        Me.ddlmrp.MendatroryField = False
        Me.ddlmrp.MyLinkLable1 = Me.lblmrp
        Me.ddlmrp.MyLinkLable2 = Nothing
        Me.ddlmrp.Name = "ddlmrp"
        Me.ddlmrp.Size = New System.Drawing.Size(156, 20)
        Me.ddlmrp.TabIndex = 13
        '
        'lblmrp
        '
        Me.lblmrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmrp.Location = New System.Drawing.Point(5, 112)
        Me.lblmrp.Name = "lblmrp"
        Me.lblmrp.Size = New System.Drawing.Size(31, 16)
        Me.lblmrp.TabIndex = 15
        Me.lblmrp.Text = "MRP"
        '
        'gvItem
        '
        Me.gvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItem.Location = New System.Drawing.Point(0, 0)
        '
        'gvItem
        '
        Me.gvItem.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvItem.MasterTemplate.AutoGenerateColumns = False
        Me.gvItem.MasterTemplate.EnableGrouping = False
        Me.gvItem.Name = "gvItem"
        Me.gvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvItem.Size = New System.Drawing.Size(863, 224)
        Me.gvItem.TabIndex = 0
        Me.gvItem.TabStop = False
        Me.gvItem.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(52.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(863, 361)
        Me.RadPageViewPage2.Text = "Criteria"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlCriteria)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel13)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvCustomer)
        Me.SplitContainer2.Size = New System.Drawing.Size(863, 361)
        Me.SplitContainer2.SplitterDistance = 26
        Me.SplitContainer2.TabIndex = 0
        '
        'lblCriteria
        '
        Me.lblCriteria.AutoSize = False
        Me.lblCriteria.BorderVisible = True
        Me.lblCriteria.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCriteria.Location = New System.Drawing.Point(383, 4)
        Me.lblCriteria.Name = "lblCriteria"
        Me.lblCriteria.Size = New System.Drawing.Size(376, 18)
        Me.lblCriteria.TabIndex = 618
        Me.lblCriteria.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCriteria
        '
        Me.txtCriteria.Location = New System.Drawing.Point(223, 4)
        Me.txtCriteria.MendatroryField = True
        Me.txtCriteria.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCriteria.MyLinkLable1 = Me.lblUnit
        Me.txtCriteria.MyLinkLable2 = Nothing
        Me.txtCriteria.MyReadOnly = False
        Me.txtCriteria.MyShowMasterFormButton = False
        Me.txtCriteria.Name = "txtCriteria"
        Me.txtCriteria.Size = New System.Drawing.Size(156, 18)
        Me.txtCriteria.TabIndex = 1
        Me.txtCriteria.Value = ""
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Location = New System.Drawing.Point(780, 4)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(80, 18)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "Select All"
        '
        'ddlCriteria
        '
        Me.ddlCriteria.AllowShowFocusCues = False
        Me.ddlCriteria.AutoCompleteDisplayMember = Nothing
        Me.ddlCriteria.AutoCompleteValueMember = Nothing
        Me.ddlCriteria.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCriteria.Location = New System.Drawing.Point(64, 3)
        Me.ddlCriteria.MendatroryField = False
        Me.ddlCriteria.MyLinkLable1 = Me.lblmrp
        Me.ddlCriteria.MyLinkLable2 = Nothing
        Me.ddlCriteria.Name = "ddlCriteria"
        Me.ddlCriteria.Size = New System.Drawing.Size(156, 20)
        Me.ddlCriteria.TabIndex = 0
        '
        'RadLabel13
        '
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(8, 5)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(43, 16)
        Me.RadLabel13.TabIndex = 613
        Me.RadLabel13.Text = "Criteria"
        '
        'gvCustomer
        '
        Me.gvCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomer.Location = New System.Drawing.Point(0, 0)
        '
        'gvCustomer
        '
        Me.gvCustomer.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomer.MasterTemplate.EnableFiltering = True
        Me.gvCustomer.MasterTemplate.EnableGrouping = False
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomer.Size = New System.Drawing.Size(863, 331)
        Me.gvCustomer.TabIndex = 0
        Me.gvCustomer.TabStop = False
        Me.gvCustomer.Text = "RadGridView1"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(863, 361)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(863, 361)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(801, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(89, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmSchemeMasterNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 459)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmSchemeMasterNew"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Scheme Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMainItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpScheme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBasicPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlmrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndScheme As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtUnitCode As common.UserControls.txtFinder
    Friend WithEvents lblUnit As common.Controls.MyLabel
    Friend WithEvents txtMainItem As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents ddlBasicPrice As common.Controls.MyComboBox
    Friend WithEvents ddlmrp As common.Controls.MyComboBox
    Friend WithEvents lblmrp As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpInactive As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents dtpScheme As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents grdScheme As common.UserControls.MyRadGridView
    Friend WithEvents chkInactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblMainItemDesc As common.Controls.MyLabel
    Friend WithEvents lblUnitCodeDesc As common.Controls.MyLabel
    Friend WithEvents txtAmount As common.MyNumBox
    Friend WithEvents txtQty As common.MyNumBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ddlCriteria As common.Controls.MyComboBox
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents gvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents lblCriteria As common.Controls.MyLabel
    Friend WithEvents txtCriteria As common.UserControls.txtFinder
    Friend WithEvents txtPercentage As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents export_main As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents export_beneficial As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export_Criteria As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import_Main As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents import_beneficial As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents import_criteria As Telerik.WinControls.UI.RadMenuItem
End Class

