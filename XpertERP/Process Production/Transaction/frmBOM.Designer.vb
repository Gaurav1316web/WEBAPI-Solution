<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBOM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBOM))
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageItemDetails = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtByproductQty = New common.MyNumBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtbuildUnit = New common.Controls.MyLabel()
        Me.txtByproductUOM = New common.UserControls.txtFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtuomname = New common.Controls.MyLabel()
        Me.txtByproductItem = New common.UserControls.txtFinder()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.lblMasterItemName = New common.Controls.MyLabel()
        Me.txtJobWorkLoc = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.chkOSP_JW = New System.Windows.Forms.CheckBox()
        Me.txtvendorName = New common.Controls.MyLabel()
        Me.TxtitemType = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtVendorCode = New common.UserControls.txtFinder()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtvalidupto = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtvalid = New common.Controls.MyDateTimePicker()
        Me.txtrevisionno = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtsubcat_name = New common.Controls.MyLabel()
        Me.txtsubcatcode = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtUomCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtCategory = New common.Controls.MyLabel()
        Me.lblBomDate = New common.Controls.MyLabel()
        Me.dtpBOMDate = New common.Controls.MyDateTimePicker()
        Me.txtProducedItem = New common.UserControls.txtFinder()
        Me.lblMasterItem = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.fndItemCategory = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.txtBuildQty = New common.MyNumBox()
        Me.lblBuildQty = New common.Controls.MyLabel()
        Me.cboBOMStatus = New common.Controls.MyComboBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvBOM = New common.UserControls.MyRadGridView()
        Me.RadPageViewSectionDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtSection = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.fndSection = New common.UserControls.txtFinder()
        Me.gvStages = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv_History = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtCostGroup = New common.UserControls.txtFinder()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnexcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImportOverheadCost = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuExportOverheadCost = New Telerik.WinControls.UI.RadMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView.SuspendLayout()
        Me.RadPageItemDetails.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtByproductQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbuildUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtuomname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtitemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvalidupto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrevisionno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsubcat_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsubcatcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewSectionDetail.SuspendLayout()
        CType(Me.TxtSection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvStages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvStages.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv_History, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_History.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(128, 9)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(235, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(5, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(62, 16)
        Me.lblCode.TabIndex = 1
        Me.lblCode.Text = "BOM Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(943, 548)
        Me.RadGroupBox1.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(923, 518)
        Me.SplitContainer1.SplitterDistance = 479
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView
        '
        Me.RadPageView.Controls.Add(Me.RadPageItemDetails)
        Me.RadPageView.Controls.Add(Me.RadPageViewSectionDetail)
        Me.RadPageView.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView.Name = "RadPageView"
        Me.RadPageView.SelectedPage = Me.RadPageItemDetails
        Me.RadPageView.Size = New System.Drawing.Size(917, 473)
        Me.RadPageView.TabIndex = 0
        Me.RadPageView.Text = "RADPAGEVIEW"
        CType(Me.RadPageView.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageItemDetails
        '
        Me.RadPageItemDetails.Controls.Add(Me.SplitContainer2)
        Me.RadPageItemDetails.ItemSize = New System.Drawing.SizeF(88.0!, 28.0!)
        Me.RadPageItemDetails.Location = New System.Drawing.Point(10, 37)
        Me.RadPageItemDetails.Name = "RadPageItemDetails"
        Me.RadPageItemDetails.Size = New System.Drawing.Size(896, 425)
        Me.RadPageItemDetails.Text = "Bill of Material"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtitemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtbuildUnit)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtvalidupto)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtvalid)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtrevisionno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtsubcat_name)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtsubcatcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtUomCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtuomname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtCategory)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpBOMDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtProducedItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMasterItemName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMasterItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndItemCategory)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBOMStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBuildQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboBOMStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBuildQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(896, 425)
        Me.SplitContainer2.SplitterDistance = 205
        Me.SplitContainer2.TabIndex = 34
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtByproductQty)
        Me.GroupBox1.Controls.Add(Me.MyLabel16)
        Me.GroupBox1.Controls.Add(Me.txtByproductUOM)
        Me.GroupBox1.Controls.Add(Me.MyLabel14)
        Me.GroupBox1.Controls.Add(Me.txtByproductItem)
        Me.GroupBox1.Controls.Add(Me.MyLabel15)
        Me.GroupBox1.Location = New System.Drawing.Point(695, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(193, 93)
        Me.GroupBox1.TabIndex = 53
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Byproduct Item"
        '
        'txtByproductQty
        '
        Me.txtByproductQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtByproductQty.CalculationExpression = Nothing
        Me.txtByproductQty.DecimalPlaces = 0
        Me.txtByproductQty.FieldCode = Nothing
        Me.txtByproductQty.FieldDesc = Nothing
        Me.txtByproductQty.FieldMaxLength = 0
        Me.txtByproductQty.FieldName = Nothing
        Me.txtByproductQty.isCalculatedField = False
        Me.txtByproductQty.IsSourceFromTable = False
        Me.txtByproductQty.IsSourceFromValueList = False
        Me.txtByproductQty.IsUnique = False
        Me.txtByproductQty.Location = New System.Drawing.Point(57, 68)
        Me.txtByproductQty.MendatroryField = False
        Me.txtByproductQty.MyLinkLable1 = Me.MyLabel16
        Me.txtByproductQty.MyLinkLable2 = Me.txtbuildUnit
        Me.txtByproductQty.Name = "txtByproductQty"
        Me.txtByproductQty.ReferenceFieldDesc = Nothing
        Me.txtByproductQty.ReferenceFieldName = Nothing
        Me.txtByproductQty.ReferenceTableName = Nothing
        Me.txtByproductQty.Size = New System.Drawing.Size(132, 20)
        Me.txtByproductQty.TabIndex = 40
        Me.txtByproductQty.Text = "0"
        Me.txtByproductQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtByproductQty.Value = 0.0R
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(6, 70)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel16.TabIndex = 41
        Me.MyLabel16.Text = "Quantity"
        '
        'txtbuildUnit
        '
        Me.txtbuildUnit.AutoSize = False
        Me.txtbuildUnit.BorderVisible = True
        Me.txtbuildUnit.FieldName = Nothing
        Me.txtbuildUnit.Location = New System.Drawing.Point(553, 126)
        Me.txtbuildUnit.Name = "txtbuildUnit"
        Me.txtbuildUnit.Size = New System.Drawing.Size(138, 19)
        Me.txtbuildUnit.TabIndex = 12
        Me.txtbuildUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtByproductUOM
        '
        Me.txtByproductUOM.CalculationExpression = Nothing
        Me.txtByproductUOM.FieldCode = Nothing
        Me.txtByproductUOM.FieldDesc = Nothing
        Me.txtByproductUOM.FieldMaxLength = 0
        Me.txtByproductUOM.FieldName = Nothing
        Me.txtByproductUOM.isCalculatedField = False
        Me.txtByproductUOM.IsSourceFromTable = False
        Me.txtByproductUOM.IsSourceFromValueList = False
        Me.txtByproductUOM.IsUnique = False
        Me.txtByproductUOM.Location = New System.Drawing.Point(57, 45)
        Me.txtByproductUOM.MendatroryField = False
        Me.txtByproductUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtByproductUOM.MyLinkLable1 = Me.MyLabel14
        Me.txtByproductUOM.MyLinkLable2 = Me.txtuomname
        Me.txtByproductUOM.MyReadOnly = False
        Me.txtByproductUOM.MyShowMasterFormButton = False
        Me.txtByproductUOM.Name = "txtByproductUOM"
        Me.txtByproductUOM.ReferenceFieldDesc = Nothing
        Me.txtByproductUOM.ReferenceFieldName = Nothing
        Me.txtByproductUOM.ReferenceTableName = Nothing
        Me.txtByproductUOM.Size = New System.Drawing.Size(132, 19)
        Me.txtByproductUOM.TabIndex = 37
        Me.txtByproductUOM.Value = ""
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel14.Location = New System.Drawing.Point(6, 45)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel14.TabIndex = 39
        Me.MyLabel14.Text = "UOM"
        '
        'txtuomname
        '
        Me.txtuomname.AutoSize = False
        Me.txtuomname.BorderVisible = True
        Me.txtuomname.FieldName = Nothing
        Me.txtuomname.Location = New System.Drawing.Point(284, 104)
        Me.txtuomname.Name = "txtuomname"
        Me.txtuomname.Size = New System.Drawing.Size(407, 19)
        Me.txtuomname.TabIndex = 34
        Me.txtuomname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtByproductItem
        '
        Me.txtByproductItem.CalculationExpression = Nothing
        Me.txtByproductItem.FieldCode = Nothing
        Me.txtByproductItem.FieldDesc = Nothing
        Me.txtByproductItem.FieldMaxLength = 0
        Me.txtByproductItem.FieldName = Nothing
        Me.txtByproductItem.isCalculatedField = False
        Me.txtByproductItem.IsSourceFromTable = False
        Me.txtByproductItem.IsSourceFromValueList = False
        Me.txtByproductItem.IsUnique = False
        Me.txtByproductItem.Location = New System.Drawing.Point(57, 22)
        Me.txtByproductItem.MendatroryField = False
        Me.txtByproductItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtByproductItem.MyLinkLable1 = Me.MyLabel15
        Me.txtByproductItem.MyLinkLable2 = Me.lblMasterItemName
        Me.txtByproductItem.MyReadOnly = False
        Me.txtByproductItem.MyShowMasterFormButton = False
        Me.txtByproductItem.Name = "txtByproductItem"
        Me.txtByproductItem.ReferenceFieldDesc = Nothing
        Me.txtByproductItem.ReferenceFieldName = Nothing
        Me.txtByproductItem.ReferenceTableName = Nothing
        Me.txtByproductItem.Size = New System.Drawing.Size(132, 19)
        Me.txtByproductItem.TabIndex = 36
        Me.txtByproductItem.Value = ""
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel15.Location = New System.Drawing.Point(6, 22)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel15.TabIndex = 38
        Me.MyLabel15.Text = "Item"
        '
        'lblMasterItemName
        '
        Me.lblMasterItemName.AutoSize = False
        Me.lblMasterItemName.BorderVisible = True
        Me.lblMasterItemName.FieldName = Nothing
        Me.lblMasterItemName.Location = New System.Drawing.Point(284, 81)
        Me.lblMasterItemName.Name = "lblMasterItemName"
        Me.lblMasterItemName.Size = New System.Drawing.Size(407, 19)
        Me.lblMasterItemName.TabIndex = 27
        Me.lblMasterItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJobWorkLoc
        '
        Me.txtJobWorkLoc.CalculationExpression = Nothing
        Me.txtJobWorkLoc.FieldCode = Nothing
        Me.txtJobWorkLoc.FieldDesc = Nothing
        Me.txtJobWorkLoc.FieldMaxLength = 0
        Me.txtJobWorkLoc.FieldName = Nothing
        Me.txtJobWorkLoc.isCalculatedField = False
        Me.txtJobWorkLoc.IsSourceFromTable = False
        Me.txtJobWorkLoc.IsSourceFromValueList = False
        Me.txtJobWorkLoc.IsUnique = False
        Me.txtJobWorkLoc.Location = New System.Drawing.Point(530, 17)
        Me.txtJobWorkLoc.MendatroryField = False
        Me.txtJobWorkLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobWorkLoc.MyLinkLable1 = Me.MyLabel10
        Me.txtJobWorkLoc.MyLinkLable2 = Me.txtuomname
        Me.txtJobWorkLoc.MyReadOnly = False
        Me.txtJobWorkLoc.MyShowMasterFormButton = False
        Me.txtJobWorkLoc.Name = "txtJobWorkLoc"
        Me.txtJobWorkLoc.ReferenceFieldDesc = Nothing
        Me.txtJobWorkLoc.ReferenceFieldName = Nothing
        Me.txtJobWorkLoc.ReferenceTableName = Nothing
        Me.txtJobWorkLoc.Size = New System.Drawing.Size(147, 19)
        Me.txtJobWorkLoc.TabIndex = 51
        Me.txtJobWorkLoc.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(476, 16)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel10.TabIndex = 52
        Me.MyLabel10.Text = "Location"
        '
        'chkOSP_JW
        '
        Me.chkOSP_JW.AutoSize = True
        Me.chkOSP_JW.Location = New System.Drawing.Point(0, 0)
        Me.chkOSP_JW.Name = "chkOSP_JW"
        Me.chkOSP_JW.Size = New System.Drawing.Size(75, 17)
        Me.chkOSP_JW.TabIndex = 4
        Me.chkOSP_JW.Text = "Job Work"
        Me.chkOSP_JW.UseVisualStyleBackColor = True
        '
        'txtvendorName
        '
        Me.txtvendorName.AutoSize = False
        Me.txtvendorName.BorderVisible = True
        Me.txtvendorName.FieldName = Nothing
        Me.txtvendorName.Location = New System.Drawing.Point(275, 17)
        Me.txtvendorName.Name = "txtvendorName"
        Me.txtvendorName.Size = New System.Drawing.Size(201, 19)
        Me.txtvendorName.TabIndex = 49
        Me.txtvendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtitemType
        '
        Me.TxtitemType.AutoCompleteDisplayMember = Nothing
        Me.TxtitemType.AutoCompleteValueMember = Nothing
        Me.TxtitemType.CalculationExpression = Nothing
        Me.TxtitemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.TxtitemType.FieldCode = Nothing
        Me.TxtitemType.FieldDesc = Nothing
        Me.TxtitemType.FieldMaxLength = 0
        Me.TxtitemType.FieldName = Nothing
        Me.TxtitemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtitemType.isCalculatedField = False
        Me.TxtitemType.IsSourceFromTable = False
        Me.TxtitemType.IsSourceFromValueList = False
        Me.TxtitemType.IsUnique = False
        RadListDataItem1.Text = "Open"
        RadListDataItem2.Text = "Approved"
        RadListDataItem3.Text = "On Hold"
        RadListDataItem4.Text = "In-Active"
        Me.TxtitemType.Items.Add(RadListDataItem1)
        Me.TxtitemType.Items.Add(RadListDataItem2)
        Me.TxtitemType.Items.Add(RadListDataItem3)
        Me.TxtitemType.Items.Add(RadListDataItem4)
        Me.TxtitemType.Location = New System.Drawing.Point(129, 127)
        Me.TxtitemType.MendatroryField = True
        Me.TxtitemType.MyLinkLable1 = Me.MyLabel3
        Me.TxtitemType.MyLinkLable2 = Nothing
        Me.TxtitemType.Name = "TxtitemType"
        Me.TxtitemType.ReferenceFieldDesc = Nothing
        Me.TxtitemType.ReferenceFieldName = Nothing
        Me.TxtitemType.ReferenceTableName = Nothing
        Me.TxtitemType.Size = New System.Drawing.Size(153, 18)
        Me.TxtitemType.TabIndex = 9
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(5, 127)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel3.TabIndex = 32
        Me.MyLabel3.Text = "Item Type"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(518, 127)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel11.TabIndex = 47
        Me.MyLabel11.Text = "UOM"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(4, 18)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel12.TabIndex = 50
        Me.MyLabel12.Text = "Vendor"
        '
        'txtVendorCode
        '
        Me.txtVendorCode.CalculationExpression = Nothing
        Me.txtVendorCode.FieldCode = Nothing
        Me.txtVendorCode.FieldDesc = Nothing
        Me.txtVendorCode.FieldMaxLength = 0
        Me.txtVendorCode.FieldName = Nothing
        Me.txtVendorCode.isCalculatedField = False
        Me.txtVendorCode.IsSourceFromTable = False
        Me.txtVendorCode.IsSourceFromValueList = False
        Me.txtVendorCode.IsUnique = False
        Me.txtVendorCode.Location = New System.Drawing.Point(121, 17)
        Me.txtVendorCode.MendatroryField = True
        Me.txtVendorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.MyLinkLable1 = Me.MyLabel12
        Me.txtVendorCode.MyLinkLable2 = Me.txtvendorName
        Me.txtVendorCode.MyReadOnly = False
        Me.txtVendorCode.MyShowMasterFormButton = False
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.ReferenceFieldDesc = Nothing
        Me.txtVendorCode.ReferenceFieldName = Nothing
        Me.txtVendorCode.ReferenceTableName = Nothing
        Me.txtVendorCode.Size = New System.Drawing.Size(153, 19)
        Me.txtVendorCode.TabIndex = 5
        Me.txtVendorCode.Value = ""
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(129, 34)
        Me.txtdesc.MaxLength = 200
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.MyLabel9
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(562, 20)
        Me.txtdesc.TabIndex = 3
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(5, 35)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel9.TabIndex = 45
        Me.MyLabel9.Text = "Description"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(454, 149)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel8.TabIndex = 44
        Me.MyLabel8.Text = "Valid Upto"
        '
        'txtvalidupto
        '
        Me.txtvalidupto.CalculationExpression = Nothing
        Me.txtvalidupto.CustomFormat = "dd/MM/yyyy"
        Me.txtvalidupto.FieldCode = Nothing
        Me.txtvalidupto.FieldDesc = Nothing
        Me.txtvalidupto.FieldMaxLength = 0
        Me.txtvalidupto.FieldName = Nothing
        Me.txtvalidupto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvalidupto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtvalidupto.isCalculatedField = False
        Me.txtvalidupto.IsSourceFromTable = False
        Me.txtvalidupto.IsSourceFromValueList = False
        Me.txtvalidupto.IsUnique = False
        Me.txtvalidupto.Location = New System.Drawing.Point(517, 148)
        Me.txtvalidupto.MendatroryField = True
        Me.txtvalidupto.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtvalidupto.MyLinkLable1 = Me.MyLabel8
        Me.txtvalidupto.MyLinkLable2 = Nothing
        Me.txtvalidupto.Name = "txtvalidupto"
        Me.txtvalidupto.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtvalidupto.ReferenceFieldDesc = Nothing
        Me.txtvalidupto.ReferenceFieldName = Nothing
        Me.txtvalidupto.ReferenceTableName = Nothing
        Me.txtvalidupto.Size = New System.Drawing.Size(86, 18)
        Me.txtvalidupto.TabIndex = 12
        Me.txtvalidupto.TabStop = False
        Me.txtvalidupto.Text = "03/05/2011"
        Me.txtvalidupto.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(286, 150)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel7.TabIndex = 42
        Me.MyLabel7.Text = "Valid From"
        '
        'txtvalid
        '
        Me.txtvalid.CalculationExpression = Nothing
        Me.txtvalid.CustomFormat = "dd/MM/yyyy"
        Me.txtvalid.FieldCode = Nothing
        Me.txtvalid.FieldDesc = Nothing
        Me.txtvalid.FieldMaxLength = 0
        Me.txtvalid.FieldName = Nothing
        Me.txtvalid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvalid.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtvalid.isCalculatedField = False
        Me.txtvalid.IsSourceFromTable = False
        Me.txtvalid.IsSourceFromValueList = False
        Me.txtvalid.IsUnique = False
        Me.txtvalid.Location = New System.Drawing.Point(366, 148)
        Me.txtvalid.MendatroryField = True
        Me.txtvalid.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtvalid.MyLinkLable1 = Me.MyLabel7
        Me.txtvalid.MyLinkLable2 = Nothing
        Me.txtvalid.Name = "txtvalid"
        Me.txtvalid.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtvalid.ReferenceFieldDesc = Nothing
        Me.txtvalid.ReferenceFieldName = Nothing
        Me.txtvalid.ReferenceTableName = Nothing
        Me.txtvalid.Size = New System.Drawing.Size(86, 18)
        Me.txtvalid.TabIndex = 11
        Me.txtvalid.TabStop = False
        Me.txtvalid.Text = "03/05/2011"
        Me.txtvalid.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtrevisionno
        '
        Me.txtrevisionno.AutoSize = False
        Me.txtrevisionno.BorderVisible = True
        Me.txtrevisionno.FieldName = Nothing
        Me.txtrevisionno.Location = New System.Drawing.Point(128, 148)
        Me.txtrevisionno.Name = "txtrevisionno"
        Me.txtrevisionno.Size = New System.Drawing.Size(154, 19)
        Me.txtrevisionno.TabIndex = 9
        Me.txtrevisionno.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(5, 149)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel5.TabIndex = 39
        Me.MyLabel5.Text = "Revision No."
        '
        'txtsubcat_name
        '
        Me.txtsubcat_name.AutoSize = False
        Me.txtsubcat_name.BorderVisible = True
        Me.txtsubcat_name.FieldName = Nothing
        Me.txtsubcat_name.Location = New System.Drawing.Point(817, 34)
        Me.txtsubcat_name.Name = "txtsubcat_name"
        Me.txtsubcat_name.Size = New System.Drawing.Size(30, 19)
        Me.txtsubcat_name.TabIndex = 29
        Me.txtsubcat_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtsubcat_name.Visible = False
        '
        'txtsubcatcode
        '
        Me.txtsubcatcode.AutoSize = False
        Me.txtsubcatcode.BorderVisible = True
        Me.txtsubcatcode.FieldName = Nothing
        Me.txtsubcatcode.Location = New System.Drawing.Point(796, 34)
        Me.txtsubcatcode.Name = "txtsubcatcode"
        Me.txtsubcatcode.Size = New System.Drawing.Size(26, 19)
        Me.txtsubcatcode.TabIndex = 37
        Me.txtsubcatcode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtsubcatcode.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(697, 35)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel4.TabIndex = 36
        Me.MyLabel4.Text = "Item SubCategory"
        Me.MyLabel4.Visible = False
        '
        'txtUomCode
        '
        Me.txtUomCode.CalculationExpression = Nothing
        Me.txtUomCode.FieldCode = Nothing
        Me.txtUomCode.FieldDesc = Nothing
        Me.txtUomCode.FieldMaxLength = 0
        Me.txtUomCode.FieldName = Nothing
        Me.txtUomCode.isCalculatedField = False
        Me.txtUomCode.IsSourceFromTable = False
        Me.txtUomCode.IsSourceFromValueList = False
        Me.txtUomCode.IsUnique = False
        Me.txtUomCode.Location = New System.Drawing.Point(129, 104)
        Me.txtUomCode.MendatroryField = True
        Me.txtUomCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUomCode.MyLinkLable1 = Me.MyLabel1
        Me.txtUomCode.MyLinkLable2 = Me.txtuomname
        Me.txtUomCode.MyReadOnly = False
        Me.txtUomCode.MyShowMasterFormButton = False
        Me.txtUomCode.Name = "txtUomCode"
        Me.txtUomCode.ReferenceFieldDesc = Nothing
        Me.txtUomCode.ReferenceFieldName = Nothing
        Me.txtUomCode.ReferenceTableName = Nothing
        Me.txtUomCode.Size = New System.Drawing.Size(153, 19)
        Me.txtUomCode.TabIndex = 8
        Me.txtUomCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(5, 104)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(80, 18)
        Me.MyLabel1.TabIndex = 35
        Me.MyLabel1.Text = "Produce UOM "
        '
        'TxtCategory
        '
        Me.TxtCategory.AutoSize = False
        Me.TxtCategory.BorderVisible = True
        Me.TxtCategory.FieldName = Nothing
        Me.TxtCategory.Location = New System.Drawing.Point(284, 58)
        Me.TxtCategory.Name = "TxtCategory"
        Me.TxtCategory.Size = New System.Drawing.Size(407, 19)
        Me.TxtCategory.TabIndex = 28
        Me.TxtCategory.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBomDate
        '
        Me.lblBomDate.FieldName = Nothing
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(388, 12)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBomDate.TabIndex = 1
        Me.lblBomDate.Text = "Date"
        '
        'dtpBOMDate
        '
        Me.dtpBOMDate.CalculationExpression = Nothing
        Me.dtpBOMDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBOMDate.FieldCode = Nothing
        Me.dtpBOMDate.FieldDesc = Nothing
        Me.dtpBOMDate.FieldMaxLength = 0
        Me.dtpBOMDate.FieldName = Nothing
        Me.dtpBOMDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBOMDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBOMDate.isCalculatedField = False
        Me.dtpBOMDate.IsSourceFromTable = False
        Me.dtpBOMDate.IsSourceFromValueList = False
        Me.dtpBOMDate.IsUnique = False
        Me.dtpBOMDate.Location = New System.Drawing.Point(420, 11)
        Me.dtpBOMDate.MendatroryField = True
        Me.dtpBOMDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpBOMDate.MyLinkLable2 = Nothing
        Me.dtpBOMDate.Name = "dtpBOMDate"
        Me.dtpBOMDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.ReferenceFieldDesc = Nothing
        Me.dtpBOMDate.ReferenceFieldName = Nothing
        Me.dtpBOMDate.ReferenceTableName = Nothing
        Me.dtpBOMDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpBOMDate.TabIndex = 1
        Me.dtpBOMDate.TabStop = False
        Me.dtpBOMDate.Text = "03/05/2011"
        Me.dtpBOMDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtProducedItem
        '
        Me.txtProducedItem.CalculationExpression = Nothing
        Me.txtProducedItem.FieldCode = Nothing
        Me.txtProducedItem.FieldDesc = Nothing
        Me.txtProducedItem.FieldMaxLength = 0
        Me.txtProducedItem.FieldName = Nothing
        Me.txtProducedItem.isCalculatedField = False
        Me.txtProducedItem.IsSourceFromTable = False
        Me.txtProducedItem.IsSourceFromValueList = False
        Me.txtProducedItem.IsUnique = False
        Me.txtProducedItem.Location = New System.Drawing.Point(129, 81)
        Me.txtProducedItem.MendatroryField = True
        Me.txtProducedItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducedItem.MyLinkLable1 = Me.lblMasterItem
        Me.txtProducedItem.MyLinkLable2 = Me.lblMasterItemName
        Me.txtProducedItem.MyReadOnly = False
        Me.txtProducedItem.MyShowMasterFormButton = False
        Me.txtProducedItem.Name = "txtProducedItem"
        Me.txtProducedItem.ReferenceFieldDesc = Nothing
        Me.txtProducedItem.ReferenceFieldName = Nothing
        Me.txtProducedItem.ReferenceTableName = Nothing
        Me.txtProducedItem.Size = New System.Drawing.Size(153, 19)
        Me.txtProducedItem.TabIndex = 7
        Me.txtProducedItem.Value = ""
        '
        'lblMasterItem
        '
        Me.lblMasterItem.FieldName = Nothing
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(5, 81)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(73, 18)
        Me.lblMasterItem.TabIndex = 30
        Me.lblMasterItem.Text = "Produce Item"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 59)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel2.TabIndex = 32
        Me.MyLabel2.Text = "Production Category"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(363, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'fndItemCategory
        '
        Me.fndItemCategory.CalculationExpression = Nothing
        Me.fndItemCategory.FieldCode = Nothing
        Me.fndItemCategory.FieldDesc = Nothing
        Me.fndItemCategory.FieldMaxLength = 0
        Me.fndItemCategory.FieldName = Nothing
        Me.fndItemCategory.isCalculatedField = False
        Me.fndItemCategory.IsSourceFromTable = False
        Me.fndItemCategory.IsSourceFromValueList = False
        Me.fndItemCategory.IsUnique = False
        Me.fndItemCategory.Location = New System.Drawing.Point(129, 58)
        Me.fndItemCategory.MendatroryField = True
        Me.fndItemCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndItemCategory.MyLinkLable1 = Me.MyLabel2
        Me.fndItemCategory.MyLinkLable2 = Me.TxtCategory
        Me.fndItemCategory.MyReadOnly = False
        Me.fndItemCategory.MyShowMasterFormButton = False
        Me.fndItemCategory.Name = "fndItemCategory"
        Me.fndItemCategory.ReferenceFieldDesc = Nothing
        Me.fndItemCategory.ReferenceFieldName = Nothing
        Me.fndItemCategory.ReferenceTableName = Nothing
        Me.fndItemCategory.Size = New System.Drawing.Size(153, 19)
        Me.fndItemCategory.TabIndex = 6
        Me.fndItemCategory.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(774, 9)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 29
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(505, 12)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(65, 16)
        Me.lblBOMStatus.TabIndex = 1
        Me.lblBOMStatus.Text = "Bom Status"
        '
        'txtBuildQty
        '
        Me.txtBuildQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBuildQty.CalculationExpression = Nothing
        Me.txtBuildQty.DecimalPlaces = 0
        Me.txtBuildQty.FieldCode = Nothing
        Me.txtBuildQty.FieldDesc = Nothing
        Me.txtBuildQty.FieldMaxLength = 0
        Me.txtBuildQty.FieldName = Nothing
        Me.txtBuildQty.isCalculatedField = False
        Me.txtBuildQty.IsSourceFromTable = False
        Me.txtBuildQty.IsSourceFromValueList = False
        Me.txtBuildQty.IsUnique = False
        Me.txtBuildQty.Location = New System.Drawing.Point(366, 126)
        Me.txtBuildQty.MendatroryField = True
        Me.txtBuildQty.MyLinkLable1 = Me.lblBuildQty
        Me.txtBuildQty.MyLinkLable2 = Me.txtbuildUnit
        Me.txtBuildQty.Name = "txtBuildQty"
        Me.txtBuildQty.ReferenceFieldDesc = Nothing
        Me.txtBuildQty.ReferenceFieldName = Nothing
        Me.txtBuildQty.ReferenceTableName = Nothing
        Me.txtBuildQty.Size = New System.Drawing.Size(146, 20)
        Me.txtBuildQty.TabIndex = 10
        Me.txtBuildQty.Text = "0"
        Me.txtBuildQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBuildQty.Value = 0.0R
        '
        'lblBuildQty
        '
        Me.lblBuildQty.FieldName = Nothing
        Me.lblBuildQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBuildQty.Location = New System.Drawing.Point(286, 128)
        Me.lblBuildQty.Name = "lblBuildQty"
        Me.lblBuildQty.Size = New System.Drawing.Size(77, 16)
        Me.lblBuildQty.TabIndex = 24
        Me.lblBuildQty.Text = "Build Quantity"
        '
        'cboBOMStatus
        '
        Me.cboBOMStatus.AutoCompleteDisplayMember = Nothing
        Me.cboBOMStatus.AutoCompleteValueMember = Nothing
        Me.cboBOMStatus.CalculationExpression = Nothing
        Me.cboBOMStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBOMStatus.FieldCode = Nothing
        Me.cboBOMStatus.FieldDesc = Nothing
        Me.cboBOMStatus.FieldMaxLength = 0
        Me.cboBOMStatus.FieldName = Nothing
        Me.cboBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBOMStatus.isCalculatedField = False
        Me.cboBOMStatus.IsSourceFromTable = False
        Me.cboBOMStatus.IsSourceFromValueList = False
        Me.cboBOMStatus.IsUnique = False
        RadListDataItem5.Text = "Open"
        RadListDataItem6.Text = "Approved"
        RadListDataItem7.Text = "On Hold"
        RadListDataItem8.Text = "In-Active"
        Me.cboBOMStatus.Items.Add(RadListDataItem5)
        Me.cboBOMStatus.Items.Add(RadListDataItem6)
        Me.cboBOMStatus.Items.Add(RadListDataItem7)
        Me.cboBOMStatus.Items.Add(RadListDataItem8)
        Me.cboBOMStatus.Location = New System.Drawing.Point(573, 11)
        Me.cboBOMStatus.MendatroryField = True
        Me.cboBOMStatus.MyLinkLable1 = Me.lblBOMStatus
        Me.cboBOMStatus.MyLinkLable2 = Nothing
        Me.cboBOMStatus.Name = "cboBOMStatus"
        Me.cboBOMStatus.ReferenceFieldDesc = Nothing
        Me.cboBOMStatus.ReferenceFieldName = Nothing
        Me.cboBOMStatus.ReferenceTableName = Nothing
        Me.cboBOMStatus.Size = New System.Drawing.Size(118, 18)
        Me.cboBOMStatus.TabIndex = 2
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gvBOM)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(890, 210)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gvBOM
        '
        Me.gvBOM.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvBOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvBOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBOM.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvBOM.ForeColor = System.Drawing.Color.Black
        Me.gvBOM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvBOM.Location = New System.Drawing.Point(2, 18)
        '
        'gvBOM
        '
        Me.gvBOM.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvBOM.MasterTemplate.AllowAddNewRow = False
        Me.gvBOM.MasterTemplate.AutoGenerateColumns = False
        Me.gvBOM.MasterTemplate.EnableGrouping = False
        Me.gvBOM.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBOM.Name = "gvBOM"
        Me.gvBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBOM.ShowHeaderCellButtons = True
        Me.gvBOM.Size = New System.Drawing.Size(886, 190)
        Me.gvBOM.TabIndex = 0
        Me.gvBOM.TabStop = False
        Me.gvBOM.Text = "RadGridView1"
        '
        'RadPageViewSectionDetail
        '
        Me.RadPageViewSectionDetail.Controls.Add(Me.TxtSection)
        Me.RadPageViewSectionDetail.Controls.Add(Me.MyLabel6)
        Me.RadPageViewSectionDetail.Controls.Add(Me.fndSection)
        Me.RadPageViewSectionDetail.Controls.Add(Me.gvStages)
        Me.RadPageViewSectionDetail.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewSectionDetail.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewSectionDetail.Name = "RadPageViewSectionDetail"
        Me.RadPageViewSectionDetail.Size = New System.Drawing.Size(896, 425)
        Me.RadPageViewSectionDetail.Text = "Section Details"
        '
        'TxtSection
        '
        Me.TxtSection.AutoSize = False
        Me.TxtSection.BorderVisible = True
        Me.TxtSection.FieldName = Nothing
        Me.TxtSection.Location = New System.Drawing.Point(211, 8)
        Me.TxtSection.Name = "TxtSection"
        Me.TxtSection.Size = New System.Drawing.Size(296, 19)
        Me.TxtSection.TabIndex = 33
        Me.TxtSection.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(1, 8)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 18)
        Me.MyLabel6.TabIndex = 32
        Me.MyLabel6.Text = "Section"
        '
        'fndSection
        '
        Me.fndSection.CalculationExpression = Nothing
        Me.fndSection.FieldCode = Nothing
        Me.fndSection.FieldDesc = Nothing
        Me.fndSection.FieldMaxLength = 0
        Me.fndSection.FieldName = Nothing
        Me.fndSection.isCalculatedField = False
        Me.fndSection.IsSourceFromTable = False
        Me.fndSection.IsSourceFromValueList = False
        Me.fndSection.IsUnique = False
        Me.fndSection.Location = New System.Drawing.Point(56, 8)
        Me.fndSection.MendatroryField = True
        Me.fndSection.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSection.MyLinkLable1 = Me.MyLabel6
        Me.fndSection.MyLinkLable2 = Me.TxtSection
        Me.fndSection.MyReadOnly = False
        Me.fndSection.MyShowMasterFormButton = False
        Me.fndSection.Name = "fndSection"
        Me.fndSection.ReferenceFieldDesc = Nothing
        Me.fndSection.ReferenceFieldName = Nothing
        Me.fndSection.ReferenceTableName = Nothing
        Me.fndSection.Size = New System.Drawing.Size(150, 19)
        Me.fndSection.TabIndex = 0
        Me.fndSection.Value = ""
        '
        'gvStages
        '
        Me.gvStages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvStages.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvStages.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvStages.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvStages.ForeColor = System.Drawing.Color.Black
        Me.gvStages.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvStages.Location = New System.Drawing.Point(1, 37)
        '
        '
        '
        Me.gvStages.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvStages.MasterTemplate.AllowAddNewRow = False
        Me.gvStages.MasterTemplate.AutoGenerateColumns = False
        Me.gvStages.MasterTemplate.EnableGrouping = False
        Me.gvStages.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvStages.Name = "gvStages"
        Me.gvStages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvStages.ShowHeaderCellButtons = True
        Me.gvStages.Size = New System.Drawing.Size(894, 388)
        Me.gvStages.TabIndex = 1
        Me.gvStages.TabStop = False
        Me.gvStages.Text = "RadGridView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(896, 425)
        Me.RadPageViewPage1.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(896, 425)
        Me.UcAttachment1.TabIndex = 3
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv_History)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(52.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(896, 425)
        Me.RadPageViewPage2.Text = "History"
        '
        'gv_History
        '
        Me.gv_History.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_History.Location = New System.Drawing.Point(0, 0)
        '
        'gv_History
        '
        Me.gv_History.MasterTemplate.AllowAddNewRow = False
        Me.gv_History.MasterTemplate.ShowGroupedColumns = True
        Me.gv_History.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_History.Name = "gv_History"
        Me.gv_History.ShowHeaderCellButtons = True
        Me.gv_History.Size = New System.Drawing.Size(896, 425)
        Me.gv_History.TabIndex = 5
        Me.gv_History.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gv1)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage3.Controls.Add(Me.txtCostGroup)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(173.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(896, 425)
        Me.RadPageViewPage3.Text = "Overhead Cost Group Mapping"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 29)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AutoGenerateColumns = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(894, 395)
        Me.gv1.TabIndex = 66
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(3, 5)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel13.TabIndex = 65
        Me.MyLabel13.Text = "Cost Group Code"
        '
        'txtCostGroup
        '
        Me.txtCostGroup.CalculationExpression = Nothing
        Me.txtCostGroup.FieldCode = Nothing
        Me.txtCostGroup.FieldDesc = Nothing
        Me.txtCostGroup.FieldMaxLength = 0
        Me.txtCostGroup.FieldName = Nothing
        Me.txtCostGroup.isCalculatedField = False
        Me.txtCostGroup.IsSourceFromTable = False
        Me.txtCostGroup.IsSourceFromValueList = False
        Me.txtCostGroup.IsUnique = False
        Me.txtCostGroup.Location = New System.Drawing.Point(103, 5)
        Me.txtCostGroup.MendatroryField = True
        Me.txtCostGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostGroup.MyLinkLable1 = Nothing
        Me.txtCostGroup.MyLinkLable2 = Nothing
        Me.txtCostGroup.MyReadOnly = False
        Me.txtCostGroup.MyShowMasterFormButton = False
        Me.txtCostGroup.Name = "txtCostGroup"
        Me.txtCostGroup.ReferenceFieldDesc = Nothing
        Me.txtCostGroup.ReferenceFieldName = Nothing
        Me.txtCostGroup.ReferenceTableName = Nothing
        Me.txtCostGroup.Size = New System.Drawing.Size(225, 18)
        Me.txtCostGroup.TabIndex = 64
        Me.txtCostGroup.Value = ""
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexcel, Me.btnPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(403, 5)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(94, 22)
        Me.RadSplitButton1.TabIndex = 5
        Me.RadSplitButton1.Text = "Export History"
        Me.RadSplitButton1.Visible = False
        '
        'btnexcel
        '
        Me.btnexcel.AccessibleDescription = "Excel"
        Me.btnexcel.AccessibleName = "Excel"
        Me.btnexcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(303, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(94, 22)
        Me.btnHistory.TabIndex = 4
        Me.btnHistory.Text = "Show History"
        Me.btnHistory.Visible = False
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(502, 5)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(94, 22)
        Me.btnReverse.TabIndex = 6
        Me.btnReverse.Text = "Amendment"
        Me.btnReverse.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(154, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(830, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 22)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(80, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(943, 20)
        Me.RadMenu2.TabIndex = 1
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose, Me.MenuImportOverheadCost, Me.MenuExportOverheadCost})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.AccessibleDescription = "Import"
        Me.MenuItemImport.AccessibleName = "Import"
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.AccessibleDescription = "Export"
        Me.MenuItemExport.AccessibleName = "Export"
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.AccessibleDescription = "Close"
        Me.MenuItemClose.AccessibleName = "Close"
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'MenuImportOverheadCost
        '
        Me.MenuImportOverheadCost.AccessibleDescription = "Import Overhead Cost"
        Me.MenuImportOverheadCost.AccessibleName = "Import Overhead Cost"
        Me.MenuImportOverheadCost.Name = "MenuImportOverheadCost"
        Me.MenuImportOverheadCost.Text = "Import Overhead Cost"
        '
        'MenuExportOverheadCost
        '
        Me.MenuExportOverheadCost.AccessibleDescription = "Export OverHead Cost"
        Me.MenuExportOverheadCost.AccessibleName = "Export OverHead Cost"
        Me.MenuExportOverheadCost.Name = "MenuExportOverheadCost"
        Me.MenuExportOverheadCost.Text = "Export OverHead Cost"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkOSP_JW)
        Me.GroupBox2.Controls.Add(Me.txtJobWorkLoc)
        Me.GroupBox2.Controls.Add(Me.MyLabel12)
        Me.GroupBox2.Controls.Add(Me.MyLabel10)
        Me.GroupBox2.Controls.Add(Me.txtVendorCode)
        Me.GroupBox2.Controls.Add(Me.txtvendorName)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 164)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(683, 37)
        Me.GroupBox2.TabIndex = 53
        Me.GroupBox2.TabStop = False
        '
        'frmBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 548)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmBOM"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bill of Material"
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView.ResumeLayout(False)
        Me.RadPageItemDetails.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtByproductQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbuildUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtuomname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtitemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvalidupto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrevisionno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsubcat_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsubcatcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewSectionDetail.ResumeLayout(False)
        Me.RadPageViewSectionDetail.PerformLayout()
        CType(Me.TxtSection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvStages.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvStages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv_History.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_History, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtProducedItem As common.UserControls.txtFinder
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpBOMDate As common.Controls.MyDateTimePicker
    Friend WithEvents gvBOM As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblMasterItemName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cboBOMStatus As common.Controls.MyComboBox
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents lblBuildQty As common.Controls.MyLabel
    Friend WithEvents txtBuildQty As common.MyNumBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndItemCategory As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageView As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageItemDetails As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewSectionDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndSection As common.UserControls.txtFinder
    Friend WithEvents gvStages As common.UserControls.MyRadGridView
    Friend WithEvents TxtSection As common.Controls.MyLabel
    Friend WithEvents TxtCategory As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtUomCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtuomname As common.Controls.MyLabel
    Friend WithEvents txtsubcatcode As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtsubcat_name As common.Controls.MyLabel
    Friend WithEvents txtrevisionno As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtvalidupto As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtvalid As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtbuildUnit As common.Controls.MyLabel
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnexcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv_History As common.UserControls.MyRadGridView
    Friend WithEvents TxtitemType As common.Controls.MyComboBox
    Friend WithEvents txtvendorName As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtVendorCode As common.UserControls.txtFinder
    Friend WithEvents chkOSP_JW As System.Windows.Forms.CheckBox
    Friend WithEvents txtJobWorkLoc As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtCostGroup As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MenuImportOverheadCost As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuExportOverheadCost As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtByproductQty As common.MyNumBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtByproductUOM As common.UserControls.txtFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtByproductItem As common.UserControls.txtFinder
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
