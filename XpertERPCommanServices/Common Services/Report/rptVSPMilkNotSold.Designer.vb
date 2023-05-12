<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptVSPMilkNotSold
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMultiDeduction = New common.UserControls.txtMultiSelectFinder()
        Me.chkDeduction = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtPaymentCycleCode = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtMCC = New common.Controls.MyTextBox()
        Me.lblMCC = New common.Controls.MyTextBox()
        Me.lblMCC2 = New common.Controls.MyLabel()
        Me.txtLocName = New common.Controls.MyTextBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.fndLoc = New common.UserControls.txtFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(692, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(692, 376)
        Me.SplitContainer1.SplitterDistance = 326
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(692, 326)
        Me.RadPageView1.TabIndex = 11
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiDeduction)
        Me.RadPageViewPage1.Controls.Add(Me.chkDeduction)
        Me.RadPageViewPage1.Controls.Add(Me.txtPaymentCycleCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCC)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCC2)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocName)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.fndLoc)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(671, 278)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(368, 41)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 1523
        Me.MyLabel3.Text = "Deduction"
        Me.MyLabel3.Visible = False
        '
        'txtMultiDeduction
        '
        Me.txtMultiDeduction.arrDispalyMember = Nothing
        Me.txtMultiDeduction.arrValueMember = Nothing
        Me.txtMultiDeduction.Location = New System.Drawing.Point(427, 39)
        Me.txtMultiDeduction.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMultiDeduction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiDeduction.MyLinkLable1 = Nothing
        Me.txtMultiDeduction.MyLinkLable2 = Nothing
        Me.txtMultiDeduction.MyNullText = "All"
        Me.txtMultiDeduction.Name = "txtMultiDeduction"
        Me.txtMultiDeduction.Size = New System.Drawing.Size(225, 19)
        Me.txtMultiDeduction.TabIndex = 1522
        Me.txtMultiDeduction.Visible = False
        '
        'chkDeduction
        '
        Me.chkDeduction.Location = New System.Drawing.Point(369, 19)
        Me.chkDeduction.Name = "chkDeduction"
        Me.chkDeduction.Size = New System.Drawing.Size(72, 18)
        Me.chkDeduction.TabIndex = 1520
        Me.chkDeduction.Text = "Deduction"
        '
        'txtPaymentCycleCode
        '
        Me.txtPaymentCycleCode.CalculationExpression = Nothing
        Me.txtPaymentCycleCode.FieldCode = Nothing
        Me.txtPaymentCycleCode.FieldDesc = Nothing
        Me.txtPaymentCycleCode.FieldMaxLength = 0
        Me.txtPaymentCycleCode.FieldName = Nothing
        Me.txtPaymentCycleCode.isCalculatedField = False
        Me.txtPaymentCycleCode.IsSourceFromTable = False
        Me.txtPaymentCycleCode.IsSourceFromValueList = False
        Me.txtPaymentCycleCode.IsUnique = False
        Me.txtPaymentCycleCode.Location = New System.Drawing.Point(119, 64)
        Me.txtPaymentCycleCode.MendatroryField = True
        Me.txtPaymentCycleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentCycleCode.MyLinkLable1 = Nothing
        Me.txtPaymentCycleCode.MyLinkLable2 = Nothing
        Me.txtPaymentCycleCode.MyReadOnly = False
        Me.txtPaymentCycleCode.MyShowMasterFormButton = False
        Me.txtPaymentCycleCode.Name = "txtPaymentCycleCode"
        Me.txtPaymentCycleCode.ReferenceFieldDesc = Nothing
        Me.txtPaymentCycleCode.ReferenceFieldName = Nothing
        Me.txtPaymentCycleCode.ReferenceTableName = Nothing
        Me.txtPaymentCycleCode.Size = New System.Drawing.Size(229, 19)
        Me.txtPaymentCycleCode.TabIndex = 401
        Me.txtPaymentCycleCode.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(4, 65)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel6.TabIndex = 402
        Me.MyLabel6.Text = "Pyment Cycle Code"
        '
        'txtMCC
        '
        Me.txtMCC.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.Enabled = False
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(64, 39)
        Me.txtMCC.MendatroryField = False
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(136, 20)
        Me.txtMCC.TabIndex = 291
        '
        'lblMCC
        '
        Me.lblMCC.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblMCC.CalculationExpression = Nothing
        Me.lblMCC.Enabled = False
        Me.lblMCC.FieldCode = Nothing
        Me.lblMCC.FieldDesc = Nothing
        Me.lblMCC.FieldMaxLength = 0
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.isCalculatedField = False
        Me.lblMCC.IsSourceFromTable = False
        Me.lblMCC.IsSourceFromValueList = False
        Me.lblMCC.IsUnique = False
        Me.lblMCC.Location = New System.Drawing.Point(202, 39)
        Me.lblMCC.MendatroryField = False
        Me.lblMCC.MyLinkLable1 = Nothing
        Me.lblMCC.MyLinkLable2 = Nothing
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.ReferenceFieldDesc = Nothing
        Me.lblMCC.ReferenceFieldName = Nothing
        Me.lblMCC.ReferenceTableName = Nothing
        Me.lblMCC.Size = New System.Drawing.Size(146, 20)
        Me.lblMCC.TabIndex = 289
        '
        'lblMCC2
        '
        Me.lblMCC2.FieldName = Nothing
        Me.lblMCC2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC2.Location = New System.Drawing.Point(4, 41)
        Me.lblMCC2.Name = "lblMCC2"
        Me.lblMCC2.Size = New System.Drawing.Size(32, 16)
        Me.lblMCC2.TabIndex = 290
        Me.lblMCC2.Text = "MCC"
        '
        'txtLocName
        '
        Me.txtLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLocName.CalculationExpression = Nothing
        Me.txtLocName.Enabled = False
        Me.txtLocName.FieldCode = Nothing
        Me.txtLocName.FieldDesc = Nothing
        Me.txtLocName.FieldMaxLength = 0
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.isCalculatedField = False
        Me.txtLocName.IsSourceFromTable = False
        Me.txtLocName.IsSourceFromValueList = False
        Me.txtLocName.IsUnique = False
        Me.txtLocName.Location = New System.Drawing.Point(203, 17)
        Me.txtLocName.MendatroryField = False
        Me.txtLocName.MyLinkLable1 = Nothing
        Me.txtLocName.MyLinkLable2 = Nothing
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.ReferenceFieldDesc = Nothing
        Me.txtLocName.ReferenceFieldName = Nothing
        Me.txtLocName.ReferenceTableName = Nothing
        Me.txtLocName.Size = New System.Drawing.Size(146, 20)
        Me.txtLocName.TabIndex = 283
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(4, 19)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 284
        Me.lblLocation.Text = "Location"
        '
        'fndLoc
        '
        Me.fndLoc.CalculationExpression = Nothing
        Me.fndLoc.FieldCode = Nothing
        Me.fndLoc.FieldDesc = Nothing
        Me.fndLoc.FieldMaxLength = 0
        Me.fndLoc.FieldName = Nothing
        Me.fndLoc.isCalculatedField = False
        Me.fndLoc.IsSourceFromTable = False
        Me.fndLoc.IsSourceFromValueList = False
        Me.fndLoc.IsUnique = False
        Me.fndLoc.Location = New System.Drawing.Point(65, 18)
        Me.fndLoc.MendatroryField = True
        Me.fndLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoc.MyLinkLable1 = Me.lblLocation
        Me.fndLoc.MyLinkLable2 = Nothing
        Me.fndLoc.MyReadOnly = False
        Me.fndLoc.MyShowMasterFormButton = False
        Me.fndLoc.Name = "fndLoc"
        Me.fndLoc.ReferenceFieldDesc = Nothing
        Me.fndLoc.ReferenceFieldName = Nothing
        Me.fndLoc.ReferenceTableName = Nothing
        Me.fndLoc.Size = New System.Drawing.Size(136, 19)
        Me.fndLoc.TabIndex = 282
        Me.fndLoc.Value = ""
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox3.Controls.Add(Me.dtpFromDate)
        Me.RadGroupBox3.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.HeaderText = "Payment Cycle"
        Me.RadGroupBox3.Location = New System.Drawing.Point(4, 93)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(277, 50)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Payment Cycle"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 23)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel1.TabIndex = 279
        Me.MyLabel1.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(46, 21)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.MyLabel1
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpFromDate.TabIndex = 278
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "10/06/2011"
        Me.dtpFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(182, 21)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel2
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReadOnly = True
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 280
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "10/06/2011"
        Me.dtpToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(152, 23)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel2.TabIndex = 281
        Me.MyLabel2.Text = "To"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(671, 278)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Gv1.ForeColor = System.Drawing.Color.Black
        Me.Gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(671, 278)
        Me.Gv1.TabIndex = 0
        '
        'RadSplitExp
        '
        Me.RadSplitExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitExp.Location = New System.Drawing.Point(166, 15)
        Me.RadSplitExp.Name = "RadSplitExp"
        Me.RadSplitExp.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitExp.TabIndex = 155
        Me.RadSplitExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(599, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(14, 15)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(88, 15)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'rptVSPMilkNotSold
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 396)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptVSPMilkNotSold"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "VSP Milk Not Sold"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadSplitExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtLocName As common.Controls.MyTextBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents fndLoc As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.Controls.MyTextBox
    Friend WithEvents lblMCC As common.Controls.MyTextBox
    Friend WithEvents lblMCC2 As common.Controls.MyLabel
    Friend WithEvents txtPaymentCycleCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents chkDeduction As RadCheckBox
    Friend WithEvents txtMultiDeduction As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

