<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVendorBankAdvice
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.rbtnSaving = New common.Controls.MyRadioButton()
        Me.rbtnSavingBankWiseSummary = New common.Controls.MyRadioButton()
        Me.rbtnSavingCompulsory = New common.Controls.MyRadioButton()
        Me.txtbankgroupname = New common.Controls.MyTextBox()
        Me.txtBankGroup = New common.UserControls.txtFinder()
        Me.lblBankGroup = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.rbtnCurrentBankWiseSummary = New common.Controls.MyRadioButton()
        Me.rbtnBankWiseSummary = New common.Controls.MyRadioButton()
        Me.rbtnBankAdvice = New common.Controls.MyRadioButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.txtPaymentCycleTo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtPaymentCycleFrom = New common.UserControls.txtFinder()
        Me.txtFiscalYear = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExcelGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
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
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSavingBankWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSavingCompulsory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankgroupname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCurrentBankWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBankWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBankAdvice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadMenu1.Size = New System.Drawing.Size(815, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Visible = False
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(815, 376)
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
        Me.RadPageView1.Size = New System.Drawing.Size(815, 326)
        Me.RadPageView1.TabIndex = 11
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtbankgroupname)
        Me.RadPageViewPage1.Controls.Add(Me.txtBankGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblBankGroup)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.txtPaymentCycleTo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtPaymentCycleFrom)
        Me.RadPageViewPage1.Controls.Add(Me.txtFiscalYear)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(794, 278)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.rbtnSaving)
        Me.RadGroupBox2.Controls.Add(Me.rbtnSavingBankWiseSummary)
        Me.RadGroupBox2.Controls.Add(Me.rbtnSavingCompulsory)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(18, 170)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(506, 28)
        Me.RadGroupBox2.TabIndex = 446
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(6, 3)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(83, 18)
        Me.MyLabel6.TabIndex = 446
        Me.MyLabel6.Text = "Saving Account"
        '
        'rbtnSaving
        '
        Me.rbtnSaving.Location = New System.Drawing.Point(117, 5)
        Me.rbtnSaving.MyLinkLable1 = Nothing
        Me.rbtnSaving.MyLinkLable2 = Nothing
        Me.rbtnSaving.Name = "rbtnSaving"
        Me.rbtnSaving.Size = New System.Drawing.Size(53, 18)
        Me.rbtnSaving.TabIndex = 445
        Me.rbtnSaving.TabStop = False
        Me.rbtnSaving.Text = "Saving"
        '
        'rbtnSavingBankWiseSummary
        '
        Me.rbtnSavingBankWiseSummary.Location = New System.Drawing.Point(343, 5)
        Me.rbtnSavingBankWiseSummary.MyLinkLable1 = Nothing
        Me.rbtnSavingBankWiseSummary.MyLinkLable2 = Nothing
        Me.rbtnSavingBankWiseSummary.Name = "rbtnSavingBankWiseSummary"
        Me.rbtnSavingBankWiseSummary.Size = New System.Drawing.Size(158, 18)
        Me.rbtnSavingBankWiseSummary.TabIndex = 2
        Me.rbtnSavingBankWiseSummary.TabStop = False
        Me.rbtnSavingBankWiseSummary.Text = "Saving Bank Wise Summary"
        '
        'rbtnSavingCompulsory
        '
        Me.rbtnSavingCompulsory.Location = New System.Drawing.Point(206, 5)
        Me.rbtnSavingCompulsory.MyLinkLable1 = Nothing
        Me.rbtnSavingCompulsory.MyLinkLable2 = Nothing
        Me.rbtnSavingCompulsory.Name = "rbtnSavingCompulsory"
        Me.rbtnSavingCompulsory.Size = New System.Drawing.Size(116, 18)
        Me.rbtnSavingCompulsory.TabIndex = 1
        Me.rbtnSavingCompulsory.TabStop = False
        Me.rbtnSavingCompulsory.Text = "Saving Compulsory"
        '
        'txtbankgroupname
        '
        Me.txtbankgroupname.CalculationExpression = Nothing
        Me.txtbankgroupname.FieldCode = Nothing
        Me.txtbankgroupname.FieldDesc = Nothing
        Me.txtbankgroupname.FieldMaxLength = 0
        Me.txtbankgroupname.FieldName = Nothing
        Me.txtbankgroupname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankgroupname.isCalculatedField = False
        Me.txtbankgroupname.IsSourceFromTable = False
        Me.txtbankgroupname.IsSourceFromValueList = False
        Me.txtbankgroupname.IsUnique = False
        Me.txtbankgroupname.Location = New System.Drawing.Point(248, 76)
        Me.txtbankgroupname.MaxLength = 49
        Me.txtbankgroupname.MendatroryField = False
        Me.txtbankgroupname.MyLinkLable1 = Nothing
        Me.txtbankgroupname.MyLinkLable2 = Nothing
        Me.txtbankgroupname.Name = "txtbankgroupname"
        Me.txtbankgroupname.ReadOnly = True
        Me.txtbankgroupname.ReferenceFieldDesc = Nothing
        Me.txtbankgroupname.ReferenceFieldName = Nothing
        Me.txtbankgroupname.ReferenceTableName = Nothing
        Me.txtbankgroupname.Size = New System.Drawing.Size(177, 18)
        Me.txtbankgroupname.TabIndex = 443
        Me.txtbankgroupname.TabStop = False
        '
        'txtBankGroup
        '
        Me.txtBankGroup.CalculationExpression = Nothing
        Me.txtBankGroup.FieldCode = Nothing
        Me.txtBankGroup.FieldDesc = Nothing
        Me.txtBankGroup.FieldMaxLength = 0
        Me.txtBankGroup.FieldName = Nothing
        Me.txtBankGroup.isCalculatedField = False
        Me.txtBankGroup.IsSourceFromTable = False
        Me.txtBankGroup.IsSourceFromValueList = False
        Me.txtBankGroup.IsUnique = False
        Me.txtBankGroup.Location = New System.Drawing.Point(95, 75)
        Me.txtBankGroup.MendatroryField = True
        Me.txtBankGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankGroup.MyLinkLable1 = Me.lblBankGroup
        Me.txtBankGroup.MyLinkLable2 = Nothing
        Me.txtBankGroup.MyReadOnly = False
        Me.txtBankGroup.MyShowMasterFormButton = False
        Me.txtBankGroup.Name = "txtBankGroup"
        Me.txtBankGroup.ReferenceFieldDesc = Nothing
        Me.txtBankGroup.ReferenceFieldName = Nothing
        Me.txtBankGroup.ReferenceTableName = Nothing
        Me.txtBankGroup.Size = New System.Drawing.Size(138, 19)
        Me.txtBankGroup.TabIndex = 442
        Me.txtBankGroup.Value = ""
        '
        'lblBankGroup
        '
        Me.lblBankGroup.FieldName = Nothing
        Me.lblBankGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankGroup.Location = New System.Drawing.Point(15, 75)
        Me.lblBankGroup.Name = "lblBankGroup"
        Me.lblBankGroup.Size = New System.Drawing.Size(67, 16)
        Me.lblBankGroup.TabIndex = 441
        Me.lblBankGroup.Text = "Bank Group"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.rbtnCurrentBankWiseSummary)
        Me.RadGroupBox1.Controls.Add(Me.rbtnBankWiseSummary)
        Me.RadGroupBox1.Controls.Add(Me.rbtnBankAdvice)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(17, 134)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(506, 28)
        Me.RadGroupBox1.TabIndex = 439
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(6, 5)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(88, 18)
        Me.MyLabel5.TabIndex = 435
        Me.MyLabel5.Text = "Current Account"
        '
        'rbtnCurrentBankWiseSummary
        '
        Me.rbtnCurrentBankWiseSummary.Location = New System.Drawing.Point(343, 5)
        Me.rbtnCurrentBankWiseSummary.MyLinkLable1 = Nothing
        Me.rbtnCurrentBankWiseSummary.MyLinkLable2 = Nothing
        Me.rbtnCurrentBankWiseSummary.Name = "rbtnCurrentBankWiseSummary"
        Me.rbtnCurrentBankWiseSummary.Size = New System.Drawing.Size(162, 18)
        Me.rbtnCurrentBankWiseSummary.TabIndex = 2
        Me.rbtnCurrentBankWiseSummary.TabStop = False
        Me.rbtnCurrentBankWiseSummary.Text = "Current Bank Wise Summary"
        '
        'rbtnBankWiseSummary
        '
        Me.rbtnBankWiseSummary.Location = New System.Drawing.Point(207, 5)
        Me.rbtnBankWiseSummary.MyLinkLable1 = Nothing
        Me.rbtnBankWiseSummary.MyLinkLable2 = Nothing
        Me.rbtnBankWiseSummary.Name = "rbtnBankWiseSummary"
        Me.rbtnBankWiseSummary.Size = New System.Drawing.Size(122, 18)
        Me.rbtnBankWiseSummary.TabIndex = 1
        Me.rbtnBankWiseSummary.TabStop = False
        Me.rbtnBankWiseSummary.Text = "Bank Wise Summary"
        '
        'rbtnBankAdvice
        '
        Me.rbtnBankAdvice.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnBankAdvice.Location = New System.Drawing.Point(117, 5)
        Me.rbtnBankAdvice.MyLinkLable1 = Nothing
        Me.rbtnBankAdvice.MyLinkLable2 = Nothing
        Me.rbtnBankAdvice.Name = "rbtnBankAdvice"
        Me.rbtnBankAdvice.Size = New System.Drawing.Size(81, 18)
        Me.rbtnBankAdvice.TabIndex = 0
        Me.rbtnBankAdvice.Text = "Bank Advice"
        Me.rbtnBankAdvice.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(15, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel4.TabIndex = 438
        Me.MyLabel4.Text = "MCC"
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(95, 3)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(330, 18)
        Me.txtMCC.TabIndex = 437
        Me.txtMCC.Value = ""
        '
        'txtPaymentCycleTo
        '
        Me.txtPaymentCycleTo.CalculationExpression = Nothing
        Me.txtPaymentCycleTo.FieldCode = Nothing
        Me.txtPaymentCycleTo.FieldDesc = Nothing
        Me.txtPaymentCycleTo.FieldMaxLength = 0
        Me.txtPaymentCycleTo.FieldName = Nothing
        Me.txtPaymentCycleTo.isCalculatedField = False
        Me.txtPaymentCycleTo.IsSourceFromTable = False
        Me.txtPaymentCycleTo.IsSourceFromValueList = False
        Me.txtPaymentCycleTo.IsUnique = False
        Me.txtPaymentCycleTo.Location = New System.Drawing.Point(279, 51)
        Me.txtPaymentCycleTo.MendatroryField = True
        Me.txtPaymentCycleTo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentCycleTo.MyLinkLable1 = Nothing
        Me.txtPaymentCycleTo.MyLinkLable2 = Nothing
        Me.txtPaymentCycleTo.MyReadOnly = False
        Me.txtPaymentCycleTo.MyShowMasterFormButton = False
        Me.txtPaymentCycleTo.Name = "txtPaymentCycleTo"
        Me.txtPaymentCycleTo.ReferenceFieldDesc = Nothing
        Me.txtPaymentCycleTo.ReferenceFieldName = Nothing
        Me.txtPaymentCycleTo.ReferenceTableName = Nothing
        Me.txtPaymentCycleTo.Size = New System.Drawing.Size(146, 18)
        Me.txtPaymentCycleTo.TabIndex = 436
        Me.txtPaymentCycleTo.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(248, 51)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel3.TabIndex = 435
        Me.MyLabel3.Text = "To"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(15, 51)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(79, 18)
        Me.MyLabel2.TabIndex = 434
        Me.MyLabel2.Text = "Payment Cycle"
        '
        'txtPaymentCycleFrom
        '
        Me.txtPaymentCycleFrom.CalculationExpression = Nothing
        Me.txtPaymentCycleFrom.FieldCode = Nothing
        Me.txtPaymentCycleFrom.FieldDesc = Nothing
        Me.txtPaymentCycleFrom.FieldMaxLength = 0
        Me.txtPaymentCycleFrom.FieldName = Nothing
        Me.txtPaymentCycleFrom.isCalculatedField = False
        Me.txtPaymentCycleFrom.IsSourceFromTable = False
        Me.txtPaymentCycleFrom.IsSourceFromValueList = False
        Me.txtPaymentCycleFrom.IsUnique = False
        Me.txtPaymentCycleFrom.Location = New System.Drawing.Point(95, 51)
        Me.txtPaymentCycleFrom.MendatroryField = True
        Me.txtPaymentCycleFrom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentCycleFrom.MyLinkLable1 = Nothing
        Me.txtPaymentCycleFrom.MyLinkLable2 = Nothing
        Me.txtPaymentCycleFrom.MyReadOnly = False
        Me.txtPaymentCycleFrom.MyShowMasterFormButton = False
        Me.txtPaymentCycleFrom.Name = "txtPaymentCycleFrom"
        Me.txtPaymentCycleFrom.ReferenceFieldDesc = Nothing
        Me.txtPaymentCycleFrom.ReferenceFieldName = Nothing
        Me.txtPaymentCycleFrom.ReferenceTableName = Nothing
        Me.txtPaymentCycleFrom.Size = New System.Drawing.Size(138, 18)
        Me.txtPaymentCycleFrom.TabIndex = 433
        Me.txtPaymentCycleFrom.Value = ""
        '
        'txtFiscalYear
        '
        Me.txtFiscalYear.CalculationExpression = Nothing
        Me.txtFiscalYear.FieldCode = Nothing
        Me.txtFiscalYear.FieldDesc = Nothing
        Me.txtFiscalYear.FieldMaxLength = 0
        Me.txtFiscalYear.FieldName = Nothing
        Me.txtFiscalYear.isCalculatedField = False
        Me.txtFiscalYear.IsSourceFromTable = False
        Me.txtFiscalYear.IsSourceFromValueList = False
        Me.txtFiscalYear.IsUnique = False
        Me.txtFiscalYear.Location = New System.Drawing.Point(95, 27)
        Me.txtFiscalYear.MendatroryField = False
        Me.txtFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiscalYear.MyLinkLable1 = Me.RadLabel1
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.MyReadOnly = False
        Me.txtFiscalYear.MyShowMasterFormButton = False
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(330, 20)
        Me.txtFiscalYear.TabIndex = 432
        Me.txtFiscalYear.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(2, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(15, 27)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel1.TabIndex = 431
        Me.MyLabel1.Text = "Fiscal Year"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(17, 101)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(392, 28)
        Me.RadGroupBox3.TabIndex = 424
        Me.RadGroupBox3.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(228, 5)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(255, 4)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(124, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(80, 4)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(110, 20)
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
        Me.RadPageViewPage2.Size = New System.Drawing.Size(794, 278)
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
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(794, 278)
        Me.Gv1.TabIndex = 0
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF, Me.rmiExcelGrid, Me.RadMenuItem2})
        Me.btnExp.Location = New System.Drawing.Point(160, 15)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(83, 22)
        Me.btnExp.TabIndex = 157
        Me.btnExp.Text = "Export"
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
        'rmiExcelGrid
        '
        Me.rmiExcelGrid.Name = "rmiExcelGrid"
        Me.rmiExcelGrid.Text = "Excel(Grid)"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(722, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(9, 15)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(83, 15)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'frmVendorBankAdvice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 396)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmVendorBankAdvice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Bank Advice"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSavingBankWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSavingCompulsory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankgroupname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCurrentBankWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBankWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBankAdvice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFiscalYear As common.UserControls.txtFinder
    Friend WithEvents txtPaymentCycleFrom As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtPaymentCycleTo As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents rmiExcelGrid As RadMenuItem
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbtnBankWiseSummary As common.Controls.MyRadioButton
    Friend WithEvents rbtnBankAdvice As common.Controls.MyRadioButton
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents lblBankGroup As common.Controls.MyLabel
    Friend WithEvents txtBankGroup As common.UserControls.txtFinder
    Friend WithEvents txtbankgroupname As common.Controls.MyTextBox
    Friend WithEvents rbtnCurrentBankWiseSummary As common.Controls.MyRadioButton
    Friend WithEvents rbtnSaving As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnSavingBankWiseSummary As common.Controls.MyRadioButton
    Friend WithEvents rbtnSavingCompulsory As common.Controls.MyRadioButton
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
End Class

