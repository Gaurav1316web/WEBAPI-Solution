<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptDailyQtyReport
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblArea = New common.Controls.MyLabel()
        Me.fndArea = New common.UserControls.txtFinder()
        Me.ddlShift = New common.Controls.MyComboBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnSummary = New System.Windows.Forms.RadioButton()
        Me.rbtnDockSummary = New System.Windows.Forms.RadioButton()
        Me.rbtnBMCDock = New System.Windows.Forms.RadioButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBMCTankerCollection = New System.Windows.Forms.RadioButton()
        Me.rdbMultiple = New System.Windows.Forms.RadioButton()
        Me.rdbCollectionWise = New System.Windows.Forms.RadioButton()
        Me.rbtnBmcSummary = New System.Windows.Forms.RadioButton()
        Me.rdbTankerWise = New System.Windows.Forms.RadioButton()
        Me.rbtnTranpoterGainLoss = New System.Windows.Forms.RadioButton()
        Me.rbtnDock = New System.Windows.Forms.RadioButton()
        Me.rdbDetails = New System.Windows.Forms.RadioButton()
        Me.rdbSummary = New System.Windows.Forms.RadioButton()
        Me.TxtTankerNo = New common.UserControls.txtFinder()
        Me.txtToleranceSNF = New common.MyNumBox()
        Me.txtToleranceFat = New common.MyNumBox()
        Me.lblToleranceSNF = New common.Controls.MyLabel()
        Me.lblToleranceFAT = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtMCC_Code = New common.UserControls.txtFinder()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
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
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToleranceSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToleranceFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToleranceSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToleranceFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadMenu1.Size = New System.Drawing.Size(956, 20)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(956, 382)
        Me.SplitContainer1.SplitterDistance = 332
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
        Me.RadPageView1.Size = New System.Drawing.Size(956, 332)
        Me.RadPageView1.TabIndex = 11
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblArea)
        Me.RadPageViewPage1.Controls.Add(Me.fndArea)
        Me.RadPageViewPage1.Controls.Add(Me.ddlShift)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.TxtTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtToleranceSNF)
        Me.RadPageViewPage1.Controls.Add(Me.txtToleranceFat)
        Me.RadPageViewPage1.Controls.Add(Me.lblToleranceSNF)
        Me.RadPageViewPage1.Controls.Add(Me.lblToleranceFAT)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC_Code)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel39)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.cboShift)
        Me.RadPageViewPage1.Controls.Add(Me.ToDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(935, 284)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'lblArea
        '
        Me.lblArea.FieldName = Nothing
        Me.lblArea.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArea.Location = New System.Drawing.Point(17, 81)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(30, 16)
        Me.lblArea.TabIndex = 1501
        Me.lblArea.Text = "Area"
        Me.lblArea.Visible = False
        '
        'fndArea
        '
        Me.fndArea.CalculationExpression = Nothing
        Me.fndArea.FieldCode = Nothing
        Me.fndArea.FieldDesc = Nothing
        Me.fndArea.FieldMaxLength = 0
        Me.fndArea.FieldName = Nothing
        Me.fndArea.isCalculatedField = False
        Me.fndArea.IsSourceFromTable = False
        Me.fndArea.IsSourceFromValueList = False
        Me.fndArea.IsUnique = False
        Me.fndArea.Location = New System.Drawing.Point(84, 80)
        Me.fndArea.MendatroryField = True
        Me.fndArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndArea.MyLinkLable1 = Nothing
        Me.fndArea.MyLinkLable2 = Nothing
        Me.fndArea.MyReadOnly = False
        Me.fndArea.MyShowMasterFormButton = False
        Me.fndArea.Name = "fndArea"
        Me.fndArea.ReferenceFieldDesc = Nothing
        Me.fndArea.ReferenceFieldName = Nothing
        Me.fndArea.ReferenceTableName = Nothing
        Me.fndArea.Size = New System.Drawing.Size(243, 18)
        Me.fndArea.TabIndex = 1500
        Me.fndArea.Value = ""
        Me.fndArea.Visible = False
        '
        'ddlShift
        '
        Me.ddlShift.AutoCompleteDisplayMember = Nothing
        Me.ddlShift.AutoCompleteValueMember = Nothing
        Me.ddlShift.CalculationExpression = Nothing
        Me.ddlShift.DropDownAnimationEnabled = True
        Me.ddlShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlShift.FieldCode = Nothing
        Me.ddlShift.FieldDesc = Nothing
        Me.ddlShift.FieldMaxLength = 0
        Me.ddlShift.FieldName = Nothing
        Me.ddlShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlShift.isCalculatedField = False
        Me.ddlShift.IsSourceFromTable = False
        Me.ddlShift.IsSourceFromValueList = False
        Me.ddlShift.IsUnique = False
        Me.ddlShift.Location = New System.Drawing.Point(408, 160)
        Me.ddlShift.MendatroryField = False
        Me.ddlShift.MyLinkLable1 = Nothing
        Me.ddlShift.MyLinkLable2 = Nothing
        Me.ddlShift.Name = "ddlShift"
        Me.ddlShift.ReferenceFieldDesc = Nothing
        Me.ddlShift.ReferenceFieldName = Nothing
        Me.ddlShift.ReferenceTableName = Nothing
        Me.ddlShift.Size = New System.Drawing.Size(112, 18)
        Me.ddlShift.TabIndex = 448
        Me.ddlShift.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(369, 160)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel6.TabIndex = 447
        Me.MyLabel6.Text = "Shift"
        Me.MyLabel6.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnSummary)
        Me.RadGroupBox2.Controls.Add(Me.rbtnDockSummary)
        Me.RadGroupBox2.Controls.Add(Me.rbtnBMCDock)
        Me.RadGroupBox2.HeaderText = "Print"
        Me.RadGroupBox2.Location = New System.Drawing.Point(369, 104)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(350, 42)
        Me.RadGroupBox2.TabIndex = 446
        Me.RadGroupBox2.Text = "Print"
        '
        'rbtnSummary
        '
        Me.rbtnSummary.AutoSize = True
        Me.rbtnSummary.Location = New System.Drawing.Point(196, 15)
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(100, 17)
        Me.rbtnSummary.TabIndex = 6
        Me.rbtnSummary.Text = "Dock Summary"
        Me.rbtnSummary.UseVisualStyleBackColor = True
        '
        'rbtnDockSummary
        '
        Me.rbtnDockSummary.AutoSize = True
        Me.rbtnDockSummary.Location = New System.Drawing.Point(96, 15)
        Me.rbtnDockSummary.Name = "rbtnDockSummary"
        Me.rbtnDockSummary.Size = New System.Drawing.Size(95, 17)
        Me.rbtnDockSummary.TabIndex = 5
        Me.rbtnDockSummary.Text = "DCS Summary"
        Me.rbtnDockSummary.UseVisualStyleBackColor = True
        '
        'rbtnBMCDock
        '
        Me.rbtnBMCDock.AutoSize = True
        Me.rbtnBMCDock.Location = New System.Drawing.Point(13, 15)
        Me.rbtnBMCDock.Name = "rbtnBMCDock"
        Me.rbtnBMCDock.Size = New System.Drawing.Size(77, 17)
        Me.rbtnBMCDock.TabIndex = 4
        Me.rbtnBMCDock.Text = "BMC Dock"
        Me.rbtnBMCDock.UseVisualStyleBackColor = True
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(17, 131)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel5.TabIndex = 445
        Me.MyLabel5.Text = "Tanker No"
        Me.MyLabel5.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnBMCTankerCollection)
        Me.RadGroupBox1.Controls.Add(Me.rdbMultiple)
        Me.RadGroupBox1.Controls.Add(Me.rdbCollectionWise)
        Me.RadGroupBox1.Controls.Add(Me.rbtnBmcSummary)
        Me.RadGroupBox1.Controls.Add(Me.rdbTankerWise)
        Me.RadGroupBox1.Controls.Add(Me.rbtnTranpoterGainLoss)
        Me.RadGroupBox1.Controls.Add(Me.rbtnDock)
        Me.RadGroupBox1.Controls.Add(Me.rdbDetails)
        Me.RadGroupBox1.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(369, 9)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(552, 63)
        Me.RadGroupBox1.TabIndex = 406
        '
        'rbtnBMCTankerCollection
        '
        Me.rbtnBMCTankerCollection.AutoSize = True
        Me.rbtnBMCTankerCollection.Location = New System.Drawing.Point(293, 37)
        Me.rbtnBMCTankerCollection.Name = "rbtnBMCTankerCollection"
        Me.rbtnBMCTankerCollection.Size = New System.Drawing.Size(141, 17)
        Me.rbtnBMCTankerCollection.TabIndex = 455
        Me.rbtnBMCTankerCollection.Text = "BMC/Tanker Collection"
        Me.rbtnBMCTankerCollection.UseVisualStyleBackColor = True
        '
        'rdbMultiple
        '
        Me.rdbMultiple.AutoSize = True
        Me.rdbMultiple.Location = New System.Drawing.Point(8, 36)
        Me.rdbMultiple.Name = "rdbMultiple"
        Me.rdbMultiple.Size = New System.Drawing.Size(123, 17)
        Me.rdbMultiple.TabIndex = 454
        Me.rdbMultiple.Text = "Multiple Collection"
        Me.rdbMultiple.UseVisualStyleBackColor = True
        '
        'rdbCollectionWise
        '
        Me.rdbCollectionWise.AutoSize = True
        Me.rdbCollectionWise.Location = New System.Drawing.Point(159, 37)
        Me.rdbCollectionWise.Name = "rdbCollectionWise"
        Me.rdbCollectionWise.Size = New System.Drawing.Size(104, 17)
        Me.rdbCollectionWise.TabIndex = 453
        Me.rdbCollectionWise.Text = "Collection Data"
        Me.rdbCollectionWise.UseVisualStyleBackColor = True
        '
        'rbtnBmcSummary
        '
        Me.rbtnBmcSummary.AutoSize = True
        Me.rbtnBmcSummary.Location = New System.Drawing.Point(350, 14)
        Me.rbtnBmcSummary.Name = "rbtnBmcSummary"
        Me.rbtnBmcSummary.Size = New System.Drawing.Size(97, 17)
        Me.rbtnBmcSummary.TabIndex = 449
        Me.rbtnBmcSummary.Text = "BMC Summary"
        Me.rbtnBmcSummary.UseVisualStyleBackColor = True
        '
        'rdbTankerWise
        '
        Me.rdbTankerWise.AutoSize = True
        Me.rdbTankerWise.Location = New System.Drawing.Point(463, 14)
        Me.rdbTankerWise.Name = "rdbTankerWise"
        Me.rdbTankerWise.Size = New System.Drawing.Size(87, 17)
        Me.rdbTankerWise.TabIndex = 7
        Me.rdbTankerWise.Text = "Tanker Wise"
        Me.rdbTankerWise.UseVisualStyleBackColor = True
        '
        'rbtnTranpoterGainLoss
        '
        Me.rbtnTranpoterGainLoss.AutoSize = True
        Me.rbtnTranpoterGainLoss.Location = New System.Drawing.Point(216, 14)
        Me.rbtnTranpoterGainLoss.Name = "rbtnTranpoterGainLoss"
        Me.rbtnTranpoterGainLoss.Size = New System.Drawing.Size(128, 17)
        Me.rbtnTranpoterGainLoss.TabIndex = 6
        Me.rbtnTranpoterGainLoss.Text = "Tranpoter Gain/Loss"
        Me.rbtnTranpoterGainLoss.UseVisualStyleBackColor = True
        '
        'rbtnDock
        '
        Me.rbtnDock.AutoSize = True
        Me.rbtnDock.Location = New System.Drawing.Point(159, 13)
        Me.rbtnDock.Name = "rbtnDock"
        Me.rbtnDock.Size = New System.Drawing.Size(51, 17)
        Me.rbtnDock.TabIndex = 5
        Me.rbtnDock.Text = "Dock"
        Me.rbtnDock.UseVisualStyleBackColor = True
        '
        'rdbDetails
        '
        Me.rdbDetails.AutoSize = True
        Me.rdbDetails.Checked = True
        Me.rdbDetails.Location = New System.Drawing.Point(89, 13)
        Me.rdbDetails.Name = "rdbDetails"
        Me.rdbDetails.Size = New System.Drawing.Size(60, 17)
        Me.rdbDetails.TabIndex = 4
        Me.rdbDetails.TabStop = True
        Me.rdbDetails.Text = "Details"
        Me.rdbDetails.UseVisualStyleBackColor = True
        '
        'rdbSummary
        '
        Me.rdbSummary.AutoSize = True
        Me.rdbSummary.Location = New System.Drawing.Point(8, 13)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(71, 17)
        Me.rdbSummary.TabIndex = 3
        Me.rdbSummary.Text = "Summary"
        Me.rdbSummary.UseVisualStyleBackColor = True
        '
        'TxtTankerNo
        '
        Me.TxtTankerNo.CalculationExpression = Nothing
        Me.TxtTankerNo.FieldCode = Nothing
        Me.TxtTankerNo.FieldDesc = Nothing
        Me.TxtTankerNo.FieldMaxLength = 0
        Me.TxtTankerNo.FieldName = Nothing
        Me.TxtTankerNo.isCalculatedField = False
        Me.TxtTankerNo.IsSourceFromTable = False
        Me.TxtTankerNo.IsSourceFromValueList = False
        Me.TxtTankerNo.IsUnique = False
        Me.TxtTankerNo.Location = New System.Drawing.Point(84, 129)
        Me.TxtTankerNo.MendatroryField = True
        Me.TxtTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTankerNo.MyLinkLable1 = Nothing
        Me.TxtTankerNo.MyLinkLable2 = Nothing
        Me.TxtTankerNo.MyReadOnly = False
        Me.TxtTankerNo.MyShowMasterFormButton = False
        Me.TxtTankerNo.Name = "TxtTankerNo"
        Me.TxtTankerNo.ReferenceFieldDesc = Nothing
        Me.TxtTankerNo.ReferenceFieldName = Nothing
        Me.TxtTankerNo.ReferenceTableName = Nothing
        Me.TxtTankerNo.Size = New System.Drawing.Size(242, 18)
        Me.TxtTankerNo.TabIndex = 444
        Me.TxtTankerNo.Value = ""
        Me.TxtTankerNo.Visible = False
        '
        'txtToleranceSNF
        '
        Me.txtToleranceSNF.CalculationExpression = Nothing
        Me.txtToleranceSNF.DecimalPlaces = 3
        Me.txtToleranceSNF.FieldCode = Nothing
        Me.txtToleranceSNF.FieldDesc = Nothing
        Me.txtToleranceSNF.FieldMaxLength = 7
        Me.txtToleranceSNF.FieldName = Nothing
        Me.txtToleranceSNF.isCalculatedField = False
        Me.txtToleranceSNF.IsSourceFromTable = False
        Me.txtToleranceSNF.IsSourceFromValueList = False
        Me.txtToleranceSNF.IsUnique = False
        Me.txtToleranceSNF.Location = New System.Drawing.Point(516, 58)
        Me.txtToleranceSNF.MendatroryField = False
        Me.txtToleranceSNF.MyLinkLable1 = Nothing
        Me.txtToleranceSNF.MyLinkLable2 = Nothing
        Me.txtToleranceSNF.Name = "txtToleranceSNF"
        Me.txtToleranceSNF.ReferenceFieldDesc = Nothing
        Me.txtToleranceSNF.ReferenceFieldName = Nothing
        Me.txtToleranceSNF.ReferenceTableName = Nothing
        Me.txtToleranceSNF.Size = New System.Drawing.Size(70, 20)
        Me.txtToleranceSNF.TabIndex = 443
        Me.txtToleranceSNF.Text = "0"
        Me.txtToleranceSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToleranceSNF.Value = 0R
        '
        'txtToleranceFat
        '
        Me.txtToleranceFat.CalculationExpression = Nothing
        Me.txtToleranceFat.DecimalPlaces = 3
        Me.txtToleranceFat.FieldCode = Nothing
        Me.txtToleranceFat.FieldDesc = Nothing
        Me.txtToleranceFat.FieldMaxLength = 7
        Me.txtToleranceFat.FieldName = Nothing
        Me.txtToleranceFat.isCalculatedField = False
        Me.txtToleranceFat.IsSourceFromTable = False
        Me.txtToleranceFat.IsSourceFromValueList = False
        Me.txtToleranceFat.IsUnique = False
        Me.txtToleranceFat.Location = New System.Drawing.Point(326, 57)
        Me.txtToleranceFat.MendatroryField = False
        Me.txtToleranceFat.MyLinkLable1 = Nothing
        Me.txtToleranceFat.MyLinkLable2 = Nothing
        Me.txtToleranceFat.Name = "txtToleranceFat"
        Me.txtToleranceFat.ReferenceFieldDesc = Nothing
        Me.txtToleranceFat.ReferenceFieldName = Nothing
        Me.txtToleranceFat.ReferenceTableName = Nothing
        Me.txtToleranceFat.Size = New System.Drawing.Size(70, 20)
        Me.txtToleranceFat.TabIndex = 442
        Me.txtToleranceFat.Text = "0"
        Me.txtToleranceFat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToleranceFat.Value = 0R
        '
        'lblToleranceSNF
        '
        Me.lblToleranceSNF.FieldName = Nothing
        Me.lblToleranceSNF.Location = New System.Drawing.Point(416, 57)
        Me.lblToleranceSNF.Name = "lblToleranceSNF"
        Me.lblToleranceSNF.Size = New System.Drawing.Size(69, 18)
        Me.lblToleranceSNF.TabIndex = 441
        Me.lblToleranceSNF.Text = "Allow SNF %"
        '
        'lblToleranceFAT
        '
        Me.lblToleranceFAT.FieldName = Nothing
        Me.lblToleranceFAT.Location = New System.Drawing.Point(230, 59)
        Me.lblToleranceFAT.Name = "lblToleranceFAT"
        Me.lblToleranceFAT.Size = New System.Drawing.Size(68, 18)
        Me.lblToleranceFAT.TabIndex = 440
        Me.lblToleranceFAT.Text = "Allow FAT %"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(17, 103)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel4.TabIndex = 439
        Me.MyLabel4.Text = "MCC"
        Me.MyLabel4.Visible = False
        '
        'txtMCC_Code
        '
        Me.txtMCC_Code.CalculationExpression = Nothing
        Me.txtMCC_Code.FieldCode = Nothing
        Me.txtMCC_Code.FieldDesc = Nothing
        Me.txtMCC_Code.FieldMaxLength = 0
        Me.txtMCC_Code.FieldName = Nothing
        Me.txtMCC_Code.isCalculatedField = False
        Me.txtMCC_Code.IsSourceFromTable = False
        Me.txtMCC_Code.IsSourceFromValueList = False
        Me.txtMCC_Code.IsUnique = False
        Me.txtMCC_Code.Location = New System.Drawing.Point(84, 103)
        Me.txtMCC_Code.MendatroryField = True
        Me.txtMCC_Code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC_Code.MyLinkLable1 = Nothing
        Me.txtMCC_Code.MyLinkLable2 = Nothing
        Me.txtMCC_Code.MyReadOnly = False
        Me.txtMCC_Code.MyShowMasterFormButton = False
        Me.txtMCC_Code.Name = "txtMCC_Code"
        Me.txtMCC_Code.ReferenceFieldDesc = Nothing
        Me.txtMCC_Code.ReferenceFieldName = Nothing
        Me.txtMCC_Code.ReferenceTableName = Nothing
        Me.txtMCC_Code.Size = New System.Drawing.Size(243, 18)
        Me.txtMCC_Code.TabIndex = 438
        Me.txtMCC_Code.Value = ""
        Me.txtMCC_Code.Visible = False
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(19, 59)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel39.TabIndex = 409
        Me.MyLabel39.Text = "Status"
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteDisplayMember = Nothing
        Me.cboItemType.AutoCompleteValueMember = Nothing
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownAnimationEnabled = True
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(84, 57)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Nothing
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(130, 20)
        Me.cboItemType.TabIndex = 408
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(538, 203)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        Me.RadLabel2.Visible = False
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownAnimationEnabled = True
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        Me.cboShift.Location = New System.Drawing.Point(536, 257)
        Me.cboShift.MendatroryField = False
        Me.cboShift.MyLinkLable1 = Nothing
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(112, 18)
        Me.cboShift.TabIndex = 405
        Me.cboShift.Visible = False
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(565, 202)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(83, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        Me.ToDate.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(497, 257)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel1.TabIndex = 403
        Me.MyLabel1.Text = "Shift"
        Me.MyLabel1.Visible = False
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(379, 225)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel3
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "All"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(276, 19)
        Me.txtMCC.TabIndex = 402
        Me.txtMCC.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(314, 228)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel3.TabIndex = 400
        Me.MyLabel3.Text = "MCC Code"
        Me.MyLabel3.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(350, 42)
        Me.RadGroupBox3.TabIndex = 53
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(186, 12)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 4
        Me.MyLabel2.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(236, 11)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(107, 20)
        Me.dtpToDate.TabIndex = 3
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "24/10/2011"
        Me.dtpToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From Date"
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(67, 11)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(107, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(719, 278)
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
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(719, 278)
        Me.Gv1.TabIndex = 0
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(266, 15)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(78, 22)
        Me.btnPrint.TabIndex = 157
        Me.btnPrint.Text = "Print"
        '
        'btnSplitExport
        '
        Me.btnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnSplitExport.Location = New System.Drawing.Point(165, 15)
        Me.btnSplitExport.Name = "btnSplitExport"
        Me.btnSplitExport.Size = New System.Drawing.Size(95, 22)
        Me.btnSplitExport.TabIndex = 156
        Me.btnSplitExport.Text = "Export"
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
        Me.btnClose.Location = New System.Drawing.Point(706, 15)
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
        'rptDailyQtyReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 402)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptDailyQtyReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily Qty Report"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToleranceSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToleranceFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToleranceSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToleranceFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSplitExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rdbDetails As RadioButton
    Friend WithEvents rdbSummary As RadioButton
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As RadDateTimePicker
    Friend WithEvents rbtnDock As RadioButton
    Friend WithEvents txtMCC_Code As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtToleranceSNF As common.MyNumBox
    Friend WithEvents txtToleranceFat As common.MyNumBox
    Friend WithEvents lblToleranceSNF As common.Controls.MyLabel
    Friend WithEvents lblToleranceFAT As common.Controls.MyLabel
    Friend WithEvents rbtnTranpoterGainLoss As RadioButton
    Friend WithEvents rdbTankerWise As RadioButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtTankerNo As common.UserControls.txtFinder
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnDockSummary As RadioButton
    Friend WithEvents rbtnBMCDock As RadioButton
    Friend WithEvents ddlShift As common.Controls.MyComboBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents rbtnSummary As RadioButton
    Friend WithEvents rbtnBmcSummary As RadioButton
    Friend WithEvents rdbMultiple As RadioButton
    Friend WithEvents rdbCollectionWise As RadioButton
    Friend WithEvents rbtnBMCTankerCollection As RadioButton
    Friend WithEvents lblArea As common.Controls.MyLabel
    Friend WithEvents fndArea As common.UserControls.txtFinder
End Class

