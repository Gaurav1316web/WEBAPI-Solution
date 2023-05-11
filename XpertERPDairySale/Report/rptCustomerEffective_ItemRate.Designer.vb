<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptCustomerEffective_ItemRate
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
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RbtnAll = New System.Windows.Forms.RadioButton()
        Me.RbtnLatest = New System.Windows.Forms.RadioButton()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtCustomerGroup = New common.UserControls.txtMultiSelectFinder()
        Me.TxtMultiPriceCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtMultiLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.ChkExcisablePrice = New common.Controls.MyCheckBox()
        Me.txtTotalRow = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.lblProductGroup = New common.Controls.MyLabel()
        Me.txtCustomerMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.lblItem = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.TxtMultiItem = New common.UserControls.txtMultiSelectFinder()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.TxtMultiProductGroup = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem7 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem8 = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkStockingUOM = New common.Controls.MyCheckBox()
        Me.chkPriceCodeWise = New common.Controls.MyCheckBox()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkExcisablePrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalRow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProductGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkStockingUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPriceCodeWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(5, 2)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 27
        Me.btnGo.Text = ">>>"
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(80, 2)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(71, 22)
        Me.BtnReset.TabIndex = 28
        Me.BtnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(931, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 29
        Me.btnClose.Text = "Close"
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AccessibleDescription = "Export to Excel"
        Me.RadMenuItem6.AccessibleName = "Export to Excel"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Export to Excel"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem6, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(156, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 22)
        Me.btnExport.TabIndex = 30
        Me.btnExport.Text = "Export"
        Me.btnExport.Visible = False
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "Export to PDF"
        Me.btnPDF.AccessibleName = "Export to PDF"
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "Export to PDF"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem5})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Save Layout"
        Me.RadMenuItem2.AccessibleName = "Save Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem3.AccessibleName = "Delete Layout"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Delete Layout"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "Export to PDF"
        Me.RadMenuItem5.AccessibleName = "Export to PDF"
        Me.RadMenuItem5.Image = Global.XpertERPDairySale.My.Resources.Resources.pdf
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Export to PDF"
        '
        'RadMenu1
        '
        Me.RadMenu1.AnimationEnabled = False
        Me.RadMenu1.AnimationFrames = 4
        Me.RadMenu1.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.None
        Me.RadMenu1.AutoSize = True
        Me.RadMenu1.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Right
        Me.RadMenu1.DropShadow = True
        Me.RadMenu1.EasingType = Telerik.WinControls.RadEasingType.Linear
        Me.RadMenu1.EnableAeroEffects = False
        Me.RadMenu1.FadeAnimationFrames = 10
        Me.RadMenu1.FadeAnimationSpeed = 10
        Me.RadMenu1.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenu1.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenu1.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.Smooth
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem5})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenu1.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Opacity = 1.0!
        Me.RadMenu1.ProcessKeyboard = False
        Me.RadMenu1.RollOverItemSelection = True
        Me.RadMenu1.Size = New System.Drawing.Size(135, 74)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenu1.Visible = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(2, 22)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1004, 420)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoScroll = True
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(983, 372)
        Me.RadPageViewPage1.Text = "Report"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkPriceCodeWise)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkStockingUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RbtnAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RbtnLatest)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerGroup)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCustomerGroup)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtMultiPriceCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtMultiLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ChkExcisablePrice)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtTotalRow)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblfromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblProductGroup)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCustomerMult)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtMultiItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtMultiProductGroup)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Size = New System.Drawing.Size(983, 372)
        Me.SplitContainer2.SplitterDistance = 110
        Me.SplitContainer2.TabIndex = 31
        '
        'RbtnAll
        '
        Me.RbtnAll.AutoSize = True
        Me.RbtnAll.Location = New System.Drawing.Point(785, 60)
        Me.RbtnAll.Name = "RbtnAll"
        Me.RbtnAll.Size = New System.Drawing.Size(38, 17)
        Me.RbtnAll.TabIndex = 384
        Me.RbtnAll.Text = "All"
        Me.RbtnAll.UseVisualStyleBackColor = True
        '
        'RbtnLatest
        '
        Me.RbtnLatest.AutoSize = True
        Me.RbtnLatest.Checked = True
        Me.RbtnLatest.Location = New System.Drawing.Point(723, 60)
        Me.RbtnLatest.Name = "RbtnLatest"
        Me.RbtnLatest.Size = New System.Drawing.Size(55, 17)
        Me.RbtnLatest.TabIndex = 1
        Me.RbtnLatest.TabStop = True
        Me.RbtnLatest.Text = "Latest"
        Me.RbtnLatest.UseVisualStyleBackColor = True
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(13, 35)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(89, 18)
        Me.lblCustomerGroup.TabIndex = 383
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroup
        '
        Me.txtCustomerGroup.arrDispalyMember = Nothing
        Me.txtCustomerGroup.arrValueMember = Nothing
        Me.txtCustomerGroup.Location = New System.Drawing.Point(104, 34)
        Me.txtCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroup.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroup.MyLinkLable2 = Nothing
        Me.txtCustomerGroup.MyNullText = "All"
        Me.txtCustomerGroup.Name = "txtCustomerGroup"
        Me.txtCustomerGroup.Size = New System.Drawing.Size(242, 19)
        Me.txtCustomerGroup.TabIndex = 382
        '
        'TxtMultiPriceCode
        '
        Me.TxtMultiPriceCode.arrDispalyMember = Nothing
        Me.TxtMultiPriceCode.arrValueMember = Nothing
        Me.TxtMultiPriceCode.Location = New System.Drawing.Point(450, 59)
        Me.TxtMultiPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiPriceCode.MyLinkLable1 = Me.MyLabel2
        Me.TxtMultiPriceCode.MyLinkLable2 = Nothing
        Me.TxtMultiPriceCode.MyNullText = "All"
        Me.TxtMultiPriceCode.Name = "TxtMultiPriceCode"
        Me.TxtMultiPriceCode.Size = New System.Drawing.Size(267, 19)
        Me.TxtMultiPriceCode.TabIndex = 35
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(363, 60)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel2.TabIndex = 36
        Me.MyLabel2.Text = "Price Code"
        '
        'TxtMultiLocation
        '
        Me.TxtMultiLocation.arrDispalyMember = Nothing
        Me.TxtMultiLocation.arrValueMember = Nothing
        Me.TxtMultiLocation.Location = New System.Drawing.Point(779, 36)
        Me.TxtMultiLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiLocation.MyLinkLable1 = Me.MyLabel1
        Me.TxtMultiLocation.MyLinkLable2 = Nothing
        Me.TxtMultiLocation.MyNullText = "All"
        Me.TxtMultiLocation.Name = "TxtMultiLocation"
        Me.TxtMultiLocation.Size = New System.Drawing.Size(190, 19)
        Me.TxtMultiLocation.TabIndex = 33
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(723, 37)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel1.TabIndex = 34
        Me.MyLabel1.Text = "Location"
        '
        'ChkExcisablePrice
        '
        Me.ChkExcisablePrice.Location = New System.Drawing.Point(723, 10)
        Me.ChkExcisablePrice.MyLinkLable1 = Nothing
        Me.ChkExcisablePrice.MyLinkLable2 = Nothing
        Me.ChkExcisablePrice.Name = "ChkExcisablePrice"
        Me.ChkExcisablePrice.Size = New System.Drawing.Size(122, 18)
        Me.ChkExcisablePrice.TabIndex = 32
        Me.ChkExcisablePrice.Tag1 = Nothing
        Me.ChkExcisablePrice.Text = "Excisable Price Code"
        '
        'txtTotalRow
        '
        Me.txtTotalRow.FieldName = Nothing
        Me.txtTotalRow.Location = New System.Drawing.Point(862, 11)
        Me.txtTotalRow.Name = "txtTotalRow"
        Me.txtTotalRow.Size = New System.Drawing.Size(72, 18)
        Me.txtTotalRow.TabIndex = 31
        Me.txtTotalRow.Text = "Total Rows: 0"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(13, 13)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'lblProductGroup
        '
        Me.lblProductGroup.FieldName = Nothing
        Me.lblProductGroup.Location = New System.Drawing.Point(363, 36)
        Me.lblProductGroup.Name = "lblProductGroup"
        Me.lblProductGroup.Size = New System.Drawing.Size(80, 18)
        Me.lblProductGroup.TabIndex = 30
        Me.lblProductGroup.Text = "Product Group"
        '
        'txtCustomerMult
        '
        Me.txtCustomerMult.arrDispalyMember = Nothing
        Me.txtCustomerMult.arrValueMember = Nothing
        Me.txtCustomerMult.Location = New System.Drawing.Point(104, 55)
        Me.txtCustomerMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerMult.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomerMult.MyLinkLable2 = Nothing
        Me.txtCustomerMult.MyNullText = "All"
        Me.txtCustomerMult.Name = "txtCustomerMult"
        Me.txtCustomerMult.Size = New System.Drawing.Size(242, 19)
        Me.txtCustomerMult.TabIndex = 24
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Location = New System.Drawing.Point(13, 56)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 28
        Me.lblCustomer.Text = "Customer"
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Location = New System.Drawing.Point(363, 11)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 29
        Me.lblItem.Text = "Item"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(193, 12)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
        '
        'TxtMultiItem
        '
        Me.TxtMultiItem.arrDispalyMember = Nothing
        Me.TxtMultiItem.arrValueMember = Nothing
        Me.TxtMultiItem.Location = New System.Drawing.Point(450, 11)
        Me.TxtMultiItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiItem.MyLinkLable1 = Me.lblItem
        Me.TxtMultiItem.MyLinkLable2 = Nothing
        Me.TxtMultiItem.MyNullText = "All"
        Me.TxtMultiItem.Name = "TxtMultiItem"
        Me.TxtMultiItem.Size = New System.Drawing.Size(267, 19)
        Me.TxtMultiItem.TabIndex = 26
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(245, 12)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17/12/2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(105, 12)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.lblfromDate
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17/12/2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'TxtMultiProductGroup
        '
        Me.TxtMultiProductGroup.arrDispalyMember = Nothing
        Me.TxtMultiProductGroup.arrValueMember = Nothing
        Me.TxtMultiProductGroup.Location = New System.Drawing.Point(450, 36)
        Me.TxtMultiProductGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiProductGroup.MyLinkLable1 = Me.lblProductGroup
        Me.TxtMultiProductGroup.MyLinkLable2 = Nothing
        Me.TxtMultiProductGroup.MyNullText = "All"
        Me.TxtMultiProductGroup.Name = "TxtMultiProductGroup"
        Me.TxtMultiProductGroup.Size = New System.Drawing.Size(267, 19)
        Me.TxtMultiProductGroup.TabIndex = 27
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item List"
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(979, 254)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Item List"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(2, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowFilteringRow = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(975, 234)
        Me.gv.TabIndex = 4
        Me.gv.Text = "gv"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1008, 476)
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 1
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem4})
        Me.RadMenu2.Location = New System.Drawing.Point(2, 2)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(1004, 20)
        Me.RadMenu2.TabIndex = 5
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Setting"
        Me.RadMenuItem4.AccessibleName = "Setting"
        Me.RadMenuItem4.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem7, Me.RadMenuItem8})
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Setting"
        '
        'RadMenuItem7
        '
        Me.RadMenuItem7.AccessibleDescription = "Save Layout"
        Me.RadMenuItem7.AccessibleName = "Save Layout"
        Me.RadMenuItem7.Name = "RadMenuItem7"
        Me.RadMenuItem7.Text = "Save Layout"
        '
        'RadMenuItem8
        '
        Me.RadMenuItem8.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem8.AccessibleName = "Delete Layout"
        Me.RadMenuItem8.Name = "RadMenuItem8"
        Me.RadMenuItem8.Text = "Delete Layout"
        '
        'chkStockingUOM
        '
        Me.chkStockingUOM.Location = New System.Drawing.Point(719, 84)
        Me.chkStockingUOM.MyLinkLable1 = Nothing
        Me.chkStockingUOM.MyLinkLable2 = Nothing
        Me.chkStockingUOM.Name = "chkStockingUOM"
        Me.chkStockingUOM.Size = New System.Drawing.Size(93, 18)
        Me.chkStockingUOM.TabIndex = 385
        Me.chkStockingUOM.Tag1 = Nothing
        Me.chkStockingUOM.Text = "Stocking UOM"
        '
        'chkPriceCodeWise
        '
        Me.chkPriceCodeWise.Location = New System.Drawing.Point(847, 84)
        Me.chkPriceCodeWise.MyLinkLable1 = Nothing
        Me.chkPriceCodeWise.MyLinkLable2 = Nothing
        Me.chkPriceCodeWise.Name = "chkPriceCodeWise"
        Me.chkPriceCodeWise.Size = New System.Drawing.Size(101, 18)
        Me.chkPriceCodeWise.TabIndex = 386
        Me.chkPriceCodeWise.Tag1 = Nothing
        Me.chkPriceCodeWise.Text = "Price Code Wise"
        '
        'RptCustomerEffective_ItemRate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 476)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptCustomerEffective_ItemRate"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Effective Price List"
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkExcisablePrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalRow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProductGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkStockingUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPriceCodeWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem7 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem8 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents TxtMultiProductGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents TxtMultiItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtCustomerMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblProductGroup As common.Controls.MyLabel
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents txtTotalRow As common.Controls.MyLabel
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ChkExcisablePrice As common.Controls.MyCheckBox
    Friend WithEvents TxtMultiLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtMultiPriceCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtCustomerGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RbtnAll As System.Windows.Forms.RadioButton
    Friend WithEvents RbtnLatest As System.Windows.Forms.RadioButton
    Friend WithEvents chkPriceCodeWise As common.Controls.MyCheckBox
    Friend WithEvents chkStockingUOM As common.Controls.MyCheckBox
End Class

