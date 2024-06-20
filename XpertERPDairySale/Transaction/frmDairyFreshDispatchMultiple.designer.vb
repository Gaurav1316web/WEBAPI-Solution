<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDairyFreshDispatchMultiple
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
        Dim WindowsSettings1 As Telerik.WinControls.WindowsSettings = New Telerik.WinControls.WindowsSettings()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.lblTotalCrate = New common.Controls.MyLabel()
        Me.txtTotalCrateQty = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.rgbShiftType = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnEvening = New System.Windows.Forms.RadioButton()
        Me.rbtnMorning = New System.Windows.Forms.RadioButton()
        Me.rgbItemType = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnNonTaxable = New System.Windows.Forms.RadioButton()
        Me.rbtnTaxable = New System.Windows.Forms.RadioButton()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_Head = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_details = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalCrateQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbShiftType.SuspendLayout()
        CType(Me.rgbItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbItemType.SuspendLayout()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AnimationEnabled = False
        Me.RadMenuItem2.AnimationFrames = 1
        Me.RadMenuItem2.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.None
        Me.RadMenuItem2.AutoSize = True
        Me.RadMenuItem2.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem2.DropShadow = True
        Me.RadMenuItem2.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem2.EnableAeroEffects = False
        Me.RadMenuItem2.FadeAnimationFrames = 10
        Me.RadMenuItem2.FadeAnimationSpeed = 10
        Me.RadMenuItem2.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem2.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem2.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.LastShowDpiScaleFactor = New System.Drawing.SizeF(1.0!, 1.0!)
        Me.RadMenuItem2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem2.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Opacity = 1.0!
        Me.RadMenuItem2.ProcessKeyboard = False
        Me.RadMenuItem2.RollOverItemSelection = True
        Me.RadMenuItem2.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.TabIndex = 0
        Me.RadMenuItem2.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Visible = False
        WindowsSettings1.EnableRoundedCorners = Nothing
        WindowsSettings1.RoundedCornersStyle = Telerik.WinControls.RoundedCornersStyle.Round
        Me.RadMenuItem2.WindowsSettings = WindowsSettings1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(882, 417)
        Me.Panel1.TabIndex = 4
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(882, 417)
        Me.SplitContainer1.SplitterDistance = 385
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTotalCrate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtTotalCrateQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbShiftType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbItemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFromDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(882, 385)
        Me.SplitContainer2.SplitterDistance = 77
        Me.SplitContainer2.TabIndex = 0
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(207, 54)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(181, 18)
        Me.btnGo.TabIndex = 1452
        Me.btnGo.Text = ">>>"
        '
        'lblTotalCrate
        '
        Me.lblTotalCrate.FieldName = Nothing
        Me.lblTotalCrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCrate.Location = New System.Drawing.Point(9, 54)
        Me.lblTotalCrate.Name = "lblTotalCrate"
        Me.lblTotalCrate.Size = New System.Drawing.Size(83, 16)
        Me.lblTotalCrate.TabIndex = 1450
        Me.lblTotalCrate.Text = "Total Crate Qty"
        '
        'txtTotalCrateQty
        '
        Me.txtTotalCrateQty.CalculationExpression = Nothing
        Me.txtTotalCrateQty.FieldCode = Nothing
        Me.txtTotalCrateQty.FieldDesc = Nothing
        Me.txtTotalCrateQty.FieldMaxLength = 0
        Me.txtTotalCrateQty.FieldName = Nothing
        Me.txtTotalCrateQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCrateQty.isCalculatedField = False
        Me.txtTotalCrateQty.IsSourceFromTable = False
        Me.txtTotalCrateQty.IsSourceFromValueList = False
        Me.txtTotalCrateQty.IsUnique = False
        Me.txtTotalCrateQty.Location = New System.Drawing.Point(97, 53)
        Me.txtTotalCrateQty.MaxLength = 50
        Me.txtTotalCrateQty.MendatroryField = False
        Me.txtTotalCrateQty.Modified = True
        Me.txtTotalCrateQty.MyLinkLable1 = Nothing
        Me.txtTotalCrateQty.MyLinkLable2 = Nothing
        Me.txtTotalCrateQty.Name = "txtTotalCrateQty"
        Me.txtTotalCrateQty.ReadOnly = True
        Me.txtTotalCrateQty.ReferenceFieldDesc = Nothing
        Me.txtTotalCrateQty.ReferenceFieldName = Nothing
        Me.txtTotalCrateQty.ReferenceTableName = Nothing
        Me.txtTotalCrateQty.Size = New System.Drawing.Size(91, 18)
        Me.txtTotalCrateQty.TabIndex = 1451
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(388, 8)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 58
        '
        'rgbShiftType
        '
        Me.rgbShiftType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbShiftType.Controls.Add(Me.rbtnEvening)
        Me.rgbShiftType.Controls.Add(Me.rbtnMorning)
        Me.rgbShiftType.HeaderText = "Shift Type"
        Me.rgbShiftType.Location = New System.Drawing.Point(536, 4)
        Me.rgbShiftType.Name = "rgbShiftType"
        Me.rgbShiftType.Size = New System.Drawing.Size(118, 66)
        Me.rgbShiftType.TabIndex = 57
        Me.rgbShiftType.Text = "Shift Type"
        '
        'rbtnEvening
        '
        Me.rbtnEvening.AutoSize = True
        Me.rbtnEvening.Location = New System.Drawing.Point(6, 44)
        Me.rbtnEvening.Name = "rbtnEvening"
        Me.rbtnEvening.Size = New System.Drawing.Size(66, 17)
        Me.rbtnEvening.TabIndex = 1
        Me.rbtnEvening.TabStop = True
        Me.rbtnEvening.Text = "Evening"
        Me.rbtnEvening.UseVisualStyleBackColor = True
        '
        'rbtnMorning
        '
        Me.rbtnMorning.AutoSize = True
        Me.rbtnMorning.Location = New System.Drawing.Point(6, 21)
        Me.rbtnMorning.Name = "rbtnMorning"
        Me.rbtnMorning.Size = New System.Drawing.Size(70, 17)
        Me.rbtnMorning.TabIndex = 0
        Me.rbtnMorning.TabStop = True
        Me.rbtnMorning.Text = "Morning"
        Me.rbtnMorning.UseVisualStyleBackColor = True
        '
        'rgbItemType
        '
        Me.rgbItemType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbItemType.Controls.Add(Me.rbtnNonTaxable)
        Me.rgbItemType.Controls.Add(Me.rbtnTaxable)
        Me.rgbItemType.HeaderText = "Item Type"
        Me.rgbItemType.Location = New System.Drawing.Point(413, 3)
        Me.rgbItemType.Name = "rgbItemType"
        Me.rgbItemType.Size = New System.Drawing.Size(118, 66)
        Me.rgbItemType.TabIndex = 56
        Me.rgbItemType.Text = "Item Type"
        '
        'rbtnNonTaxable
        '
        Me.rbtnNonTaxable.AutoSize = True
        Me.rbtnNonTaxable.Location = New System.Drawing.Point(6, 46)
        Me.rbtnNonTaxable.Name = "rbtnNonTaxable"
        Me.rbtnNonTaxable.Size = New System.Drawing.Size(88, 17)
        Me.rbtnNonTaxable.TabIndex = 1
        Me.rbtnNonTaxable.TabStop = True
        Me.rbtnNonTaxable.Text = "Non Taxable"
        Me.rbtnNonTaxable.UseVisualStyleBackColor = True
        '
        'rbtnTaxable
        '
        Me.rbtnTaxable.AutoSize = True
        Me.rbtnTaxable.Location = New System.Drawing.Point(6, 22)
        Me.rbtnTaxable.Name = "rbtnTaxable"
        Me.rbtnTaxable.Size = New System.Drawing.Size(63, 17)
        Me.rbtnTaxable.TabIndex = 0
        Me.rbtnTaxable.TabStop = True
        Me.rbtnTaxable.Text = "Taxable"
        Me.rbtnTaxable.UseVisualStyleBackColor = True
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDesc.Location = New System.Drawing.Point(206, 31)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(181, 18)
        Me.lblLocationDesc.TabIndex = 54
        Me.lblLocationDesc.TextWrap = False
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(75, 30)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Me.lblLocationDesc
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(115, 19)
        Me.txtLocation.TabIndex = 53
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(9, 31)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 55
        Me.lblLocation.Text = "Location"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(202, 10)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 51
        Me.lblToDate.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(267, 9)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(121, 18)
        Me.txtToDate.TabIndex = 52
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13/06/2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(10, 9)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(60, 16)
        Me.lblFromDate.TabIndex = 49
        Me.lblFromDate.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(75, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.lblFromDate
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(121, 18)
        Me.txtFromDate.TabIndex = 50
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13/06/2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(882, 304)
        Me.gv1.TabIndex = 0
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(5, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(65, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(809, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 20)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem6})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(882, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "E-Mail/SMS Setting"
        Me.RadMenuItem5.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.Import})
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "File"
        Me.RadMenuItem6.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'Export
        '
        Me.Export.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export_Head, Me.Export_details})
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'Export_Head
        '
        Me.Export_Head.Name = "Export_Head"
        Me.Export_Head.Text = "Export Shipment Head"
        '
        'Export_details
        '
        Me.Export_details.Name = "Export_details"
        Me.Export_details.Text = "Export Shipment Details"
        '
        'Import
        '
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        Me.Import.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'frmDairyFreshDispatchMultiple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(882, 437)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmDairyFreshDispatchMultiple"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Dairy Fresh Dispatch Multiple"
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalCrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalCrateQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbShiftType.ResumeLayout(False)
        Me.rgbShiftType.PerformLayout()
        CType(Me.rgbItemType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbItemType.ResumeLayout(False)
        Me.rgbItemType.PerformLayout()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export_Head As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export_details As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents gv1 As RadGridView
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents rgbShiftType As RadGroupBox
    Friend WithEvents rbtnEvening As RadioButton
    Friend WithEvents rbtnMorning As RadioButton
    Friend WithEvents rgbItemType As RadGroupBox
    Friend WithEvents rbtnNonTaxable As RadioButton
    Friend WithEvents rbtnTaxable As RadioButton
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents lblTotalCrate As common.Controls.MyLabel
    Friend WithEvents txtTotalCrateQty As common.Controls.MyTextBox
    Friend WithEvents btnGo As RadButton
End Class

