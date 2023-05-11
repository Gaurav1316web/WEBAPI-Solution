<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankBookRecoReport
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
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.dtFrm = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtTo = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.ddlBankType = New common.Controls.MyComboBox()
        Me.lbltype = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TxtDocType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.chkbankcharges = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtBank = New common.UserControls.txtMultiSelectFinder()
        Me.chkDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvReport = New common.UserControls.MyRadGridView()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.QExpExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.chkExcludeProvisionBank = New System.Windows.Forms.CheckBox()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBankType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkbankcharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(8, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(34, 18)
        Me.RadLabel2.TabIndex = 2
        Me.RadLabel2.Text = "From:"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(162, 10)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(21, 18)
        Me.RadLabel3.TabIndex = 2
        Me.RadLabel3.Text = "To:"
        '
        'dtFrm
        '
        Me.dtFrm.CustomFormat = "dd/MM/yyyy"
        Me.dtFrm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrm.Location = New System.Drawing.Point(70, 8)
        Me.dtFrm.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFrm.Name = "dtFrm"
        Me.dtFrm.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFrm.Size = New System.Drawing.Size(86, 20)
        Me.dtFrm.TabIndex = 3
        Me.dtFrm.TabStop = False
        Me.dtFrm.Text = "31/08/2011"
        Me.dtFrm.Value = New Date(2011, 8, 31, 23, 50, 36, 937)
        '
        'dtTo
        '
        Me.dtTo.CustomFormat = "dd/MM/yyyy"
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(187, 8)
        Me.dtTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.Size = New System.Drawing.Size(87, 20)
        Me.dtTo.TabIndex = 4
        Me.dtTo.TabStop = False
        Me.dtTo.Text = "31/08/2011"
        Me.dtTo.Value = New Date(2011, 8, 31, 23, 50, 36, 937)
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(624, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 19)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(83, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(72, 18)
        Me.btnReset.TabIndex = 7
        Me.btnReset.Text = "Reset"
        '
        'ddlBankType
        '
        Me.ddlBankType.AutoCompleteDisplayMember = Nothing
        Me.ddlBankType.AutoCompleteValueMember = Nothing
        Me.ddlBankType.CalculationExpression = Nothing
        Me.ddlBankType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlBankType.FieldCode = Nothing
        Me.ddlBankType.FieldDesc = Nothing
        Me.ddlBankType.FieldMaxLength = 0
        Me.ddlBankType.FieldName = Nothing
        Me.ddlBankType.isCalculatedField = False
        Me.ddlBankType.IsSourceFromTable = False
        Me.ddlBankType.IsSourceFromValueList = False
        Me.ddlBankType.IsUnique = False
        RadListDataItem1.Text = "All"
        RadListDataItem2.Text = "Document Mismatch"
        RadListDataItem3.Text = "Data Mismatch"
        Me.ddlBankType.Items.Add(RadListDataItem1)
        Me.ddlBankType.Items.Add(RadListDataItem2)
        Me.ddlBankType.Items.Add(RadListDataItem3)
        Me.ddlBankType.Location = New System.Drawing.Point(350, 8)
        Me.ddlBankType.MendatroryField = False
        Me.ddlBankType.MyLinkLable1 = Nothing
        Me.ddlBankType.MyLinkLable2 = Nothing
        Me.ddlBankType.Name = "ddlBankType"
        Me.ddlBankType.ReferenceFieldDesc = Nothing
        Me.ddlBankType.ReferenceFieldName = Nothing
        Me.ddlBankType.ReferenceTableName = Nothing
        Me.ddlBankType.Size = New System.Drawing.Size(165, 20)
        Me.ddlBankType.TabIndex = 314
        '
        'lbltype
        '
        Me.lbltype.FieldName = Nothing
        Me.lbltype.Location = New System.Drawing.Point(277, 9)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(67, 18)
        Me.lbltype.TabIndex = 3
        Me.lbltype.Text = "Report Type"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(700, 484)
        Me.RadPageView1.TabIndex = 315
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(679, 436)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.chkExcludeProvisionBank)
        Me.RadPanel1.Controls.Add(Me.TxtDocType)
        Me.RadPanel1.Controls.Add(Me.MyLabel3)
        Me.RadPanel1.Controls.Add(Me.chkbankcharges)
        Me.RadPanel1.Controls.Add(Me.MyLabel2)
        Me.RadPanel1.Controls.Add(Me.txtLocation)
        Me.RadPanel1.Controls.Add(Me.MyLabel1)
        Me.RadPanel1.Controls.Add(Me.txtBank)
        Me.RadPanel1.Controls.Add(Me.chkDetail)
        Me.RadPanel1.Controls.Add(Me.chkSummary)
        Me.RadPanel1.Controls.Add(Me.dtTo)
        Me.RadPanel1.Controls.Add(Me.RadLabel3)
        Me.RadPanel1.Controls.Add(Me.dtFrm)
        Me.RadPanel1.Controls.Add(Me.lbltype)
        Me.RadPanel1.Controls.Add(Me.ddlBankType)
        Me.RadPanel1.Controls.Add(Me.RadLabel2)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(679, 436)
        Me.RadPanel1.TabIndex = 0
        '
        'TxtDocType
        '
        Me.TxtDocType.arrDispalyMember = Nothing
        Me.TxtDocType.arrValueMember = Nothing
        Me.TxtDocType.Location = New System.Drawing.Point(70, 56)
        Me.TxtDocType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDocType.MyLinkLable1 = Nothing
        Me.TxtDocType.MyLinkLable2 = Nothing
        Me.TxtDocType.MyNullText = "All"
        Me.TxtDocType.Name = "TxtDocType"
        Me.TxtDocType.Size = New System.Drawing.Size(445, 19)
        Me.TxtDocType.TabIndex = 356
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(8, 57)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel3.TabIndex = 355
        Me.MyLabel3.Text = "Doc Type:"
        '
        'chkbankcharges
        '
        Me.chkbankcharges.Location = New System.Drawing.Point(540, 127)
        Me.chkbankcharges.Name = "chkbankcharges"
        Me.chkbankcharges.Size = New System.Drawing.Size(88, 18)
        Me.chkbankcharges.TabIndex = 354
        Me.chkbankcharges.Text = "Bank Charges"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(10, 83)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 18)
        Me.MyLabel2.TabIndex = 353
        Me.MyLabel2.Text = "Location:"
        Me.MyLabel2.Visible = False
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(88, 83)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(356, 19)
        Me.txtLocation.TabIndex = 352
        Me.txtLocation.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(8, 33)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel1.TabIndex = 351
        Me.MyLabel1.Text = "Bank:"
        '
        'txtBank
        '
        Me.txtBank.arrDispalyMember = Nothing
        Me.txtBank.arrValueMember = Nothing
        Me.txtBank.Location = New System.Drawing.Point(70, 32)
        Me.txtBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBank.MyLinkLable1 = Nothing
        Me.txtBank.MyLinkLable2 = Nothing
        Me.txtBank.MyNullText = "All"
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(445, 19)
        Me.txtBank.TabIndex = 350
        '
        'chkDetail
        '
        Me.chkDetail.Location = New System.Drawing.Point(466, 127)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(49, 18)
        Me.chkDetail.TabIndex = 318
        Me.chkDetail.Text = "Detail"
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(377, 127)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 317
        Me.chkSummary.Text = "Summary"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvReport)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(637, 436)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvReport
        '
        Me.gvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvReport.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvReport.MasterTemplate.AllowAddNewRow = False
        Me.gvReport.MasterTemplate.AllowEditRow = False
        Me.gvReport.MasterTemplate.EnableFiltering = True
        Me.gvReport.MasterTemplate.ShowFilteringRow = False
        Me.gvReport.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvReport.Name = "gvReport"
        Me.gvReport.ShowGroupPanel = False
        Me.gvReport.ShowHeaderCellButtons = True
        Me.gvReport.Size = New System.Drawing.Size(637, 436)
        Me.gvReport.TabIndex = 0
        Me.gvReport.Text = "RadGridView1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(3, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(77, 18)
        Me.btnRefresh.TabIndex = 327
        Me.btnRefresh.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(700, 20)
        Me.RadMenu1.TabIndex = 317
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.RadSplitButton1)
        Me.RadPanel2.Controls.Add(Me.btnRefresh)
        Me.RadPanel2.Controls.Add(Me.btnReset)
        Me.RadPanel2.Controls.Add(Me.btnClose)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel2.Location = New System.Drawing.Point(0, 504)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(700, 27)
        Me.RadPanel2.TabIndex = 316
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.QExpExcel, Me.PDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(158, 4)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(94, 19)
        Me.RadSplitButton1.TabIndex = 322
        Me.RadSplitButton1.Text = "Export"
        '
        'QExpExcel
        '
        Me.QExpExcel.AccessibleDescription = "Excel"
        Me.QExpExcel.AccessibleName = "Excel"
        Me.QExpExcel.Name = "QExpExcel"
        Me.QExpExcel.Text = "Excel"
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AnimationEnabled = True
        Me.RadMenuItem4.AnimationFrames = 1
        Me.RadMenuItem4.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem4.AutoSize = True
        Me.RadMenuItem4.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem4.DropShadow = True
        Me.RadMenuItem4.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem4.EnableAeroEffects = False
        Me.RadMenuItem4.FadeAnimationFrames = 10
        Me.RadMenuItem4.FadeAnimationSpeed = 10
        Me.RadMenuItem4.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem4.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem4.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem4.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem4.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem4.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Opacity = 1.0!
        Me.RadMenuItem4.ProcessKeyboard = False
        Me.RadMenuItem4.RollOverItemSelection = True
        Me.RadMenuItem4.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem4.TabIndex = 0
        Me.RadMenuItem4.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem4.Visible = False
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AnimationEnabled = True
        Me.RadMenuItem5.AnimationFrames = 1
        Me.RadMenuItem5.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem5.AutoSize = True
        Me.RadMenuItem5.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem5.DropShadow = True
        Me.RadMenuItem5.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem5.EnableAeroEffects = False
        Me.RadMenuItem5.FadeAnimationFrames = 10
        Me.RadMenuItem5.FadeAnimationSpeed = 10
        Me.RadMenuItem5.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem5.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem5.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem5.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem5.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem5.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Opacity = 1.0!
        Me.RadMenuItem5.ProcessKeyboard = False
        Me.RadMenuItem5.RollOverItemSelection = True
        Me.RadMenuItem5.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem5.TabIndex = 0
        Me.RadMenuItem5.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem5.Visible = False
        '
        'chkExcludeProvisionBank
        '
        Me.chkExcludeProvisionBank.AutoSize = True
        Me.chkExcludeProvisionBank.Location = New System.Drawing.Point(521, 10)
        Me.chkExcludeProvisionBank.Name = "chkExcludeProvisionBank"
        Me.chkExcludeProvisionBank.Size = New System.Drawing.Size(143, 17)
        Me.chkExcludeProvisionBank.TabIndex = 357
        Me.chkExcludeProvisionBank.Text = "Exclude Provision Bank"
        Me.chkExcludeProvisionBank.UseVisualStyleBackColor = True
        '
        'frmBankBookRecoReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 531)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadPanel2)
        Me.Name = "frmBankBookRecoReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Book Reco Report"
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBankType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkbankcharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtFrm As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtTo As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents ddlBankType As common.Controls.MyComboBox
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvReport As common.UserControls.MyRadGridView
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents chkDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents QExpExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtBank As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkbankcharges As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtDocType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkExcludeProvisionBank As CheckBox
End Class

