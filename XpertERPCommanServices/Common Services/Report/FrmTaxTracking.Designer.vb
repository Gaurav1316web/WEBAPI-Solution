<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaxTracking
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkTransfer = New System.Windows.Forms.CheckBox()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cgvState = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkStateselect = New common.Controls.MyRadioButton()
        Me.chkStateAll = New common.Controls.MyRadioButton()
        Me.Chksummary = New System.Windows.Forms.CheckBox()
        Me.chkARinvoice = New System.Windows.Forms.CheckBox()
        Me.ChkSRInter = New System.Windows.Forms.CheckBox()
        Me.chkSaleInvoice = New System.Windows.Forms.CheckBox()
        Me.ChkSaleReturn = New System.Windows.Forms.CheckBox()
        Me.ChkVendorInvoice = New System.Windows.Forms.CheckBox()
        Me.ChkAll = New System.Windows.Forms.CheckBox()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkLocSelect = New common.Controls.MyRadioButton()
        Me.chkLocAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdoTransfer = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdoPur = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdoSale = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cgvRate = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkRateselect = New common.Controls.MyRadioButton()
        Me.chkrateall = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cgvTaxCode = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkSelectTax = New common.Controls.MyRadioButton()
        Me.chkTaxall = New common.Controls.MyRadioButton()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.ChkItemWise = New System.Windows.Forms.CheckBox()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkStateselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkStateAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rdoTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoPur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkRateselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkrateall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkSelectTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTaxall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.ChkItemWise)
        Me.RadGroupBox1.Controls.Add(Me.chkTransfer)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.Chksummary)
        Me.RadGroupBox1.Controls.Add(Me.chkARinvoice)
        Me.RadGroupBox1.Controls.Add(Me.ChkSRInter)
        Me.RadGroupBox1.Controls.Add(Me.chkSaleInvoice)
        Me.RadGroupBox1.Controls.Add(Me.ChkSaleReturn)
        Me.RadGroupBox1.Controls.Add(Me.ChkVendorInvoice)
        Me.RadGroupBox1.Controls.Add(Me.ChkAll)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(943, 538)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkTransfer
        '
        Me.chkTransfer.AutoSize = True
        Me.chkTransfer.Location = New System.Drawing.Point(384, 36)
        Me.chkTransfer.Name = "chkTransfer"
        Me.chkTransfer.Size = New System.Drawing.Size(67, 17)
        Me.chkTransfer.TabIndex = 339
        Me.chkTransfer.Text = "Transfer"
        Me.chkTransfer.UseVisualStyleBackColor = True
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cgvState)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "State"
        Me.RadGroupBox6.Location = New System.Drawing.Point(472, 77)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(453, 232)
        Me.RadGroupBox6.TabIndex = 338
        Me.RadGroupBox6.Text = "State"
        '
        'cgvState
        '
        Me.cgvState.CheckedValue = Nothing
        Me.cgvState.DataSource = Nothing
        Me.cgvState.DisplayMember = "Name"
        Me.cgvState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvState.Enabled = False
        Me.cgvState.Location = New System.Drawing.Point(10, 40)
        Me.cgvState.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvState.MyShowHeadrText = False
        Me.cgvState.Name = "cgvState"
        Me.cgvState.Size = New System.Drawing.Size(433, 182)
        Me.cgvState.TabIndex = 2
        Me.cgvState.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkStateselect)
        Me.Panel4.Controls.Add(Me.chkStateAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(433, 20)
        Me.Panel4.TabIndex = 1
        '
        'chkStateselect
        '
        Me.chkStateselect.Location = New System.Drawing.Point(197, 1)
        Me.chkStateselect.MyLinkLable1 = Nothing
        Me.chkStateselect.MyLinkLable2 = Nothing
        Me.chkStateselect.Name = "chkStateselect"
        Me.chkStateselect.Size = New System.Drawing.Size(50, 18)
        Me.chkStateselect.TabIndex = 2
        Me.chkStateselect.Text = "Select"
        '
        'chkStateAll
        '
        Me.chkStateAll.Location = New System.Drawing.Point(144, 1)
        Me.chkStateAll.MyLinkLable1 = Nothing
        Me.chkStateAll.MyLinkLable2 = Nothing
        Me.chkStateAll.Name = "chkStateAll"
        Me.chkStateAll.Size = New System.Drawing.Size(33, 18)
        Me.chkStateAll.TabIndex = 1
        Me.chkStateAll.Text = "All"
        '
        'Chksummary
        '
        Me.Chksummary.AutoSize = True
        Me.Chksummary.Location = New System.Drawing.Point(384, 12)
        Me.Chksummary.Name = "Chksummary"
        Me.Chksummary.Size = New System.Drawing.Size(72, 17)
        Me.Chksummary.TabIndex = 337
        Me.Chksummary.Text = "Summary"
        Me.Chksummary.UseVisualStyleBackColor = True
        '
        'chkARinvoice
        '
        Me.chkARinvoice.AutoSize = True
        Me.chkARinvoice.Location = New System.Drawing.Point(384, 35)
        Me.chkARinvoice.Name = "chkARinvoice"
        Me.chkARinvoice.Size = New System.Drawing.Size(80, 17)
        Me.chkARinvoice.TabIndex = 333
        Me.chkARinvoice.Text = "AR-Invoice"
        Me.chkARinvoice.UseVisualStyleBackColor = True
        '
        'ChkSRInter
        '
        Me.ChkSRInter.AutoSize = True
        Me.ChkSRInter.Location = New System.Drawing.Point(579, 55)
        Me.ChkSRInter.Name = "ChkSRInter"
        Me.ChkSRInter.Size = New System.Drawing.Size(115, 17)
        Me.ChkSRInter.TabIndex = 336
        Me.ChkSRInter.Text = "Sale Return Inter "
        Me.ChkSRInter.UseVisualStyleBackColor = True
        Me.ChkSRInter.Visible = False
        '
        'chkSaleInvoice
        '
        Me.chkSaleInvoice.AutoSize = True
        Me.chkSaleInvoice.Location = New System.Drawing.Point(485, 55)
        Me.chkSaleInvoice.Name = "chkSaleInvoice"
        Me.chkSaleInvoice.Size = New System.Drawing.Size(86, 17)
        Me.chkSaleInvoice.TabIndex = 334
        Me.chkSaleInvoice.Text = "Sale Invoice"
        Me.chkSaleInvoice.UseVisualStyleBackColor = True
        Me.chkSaleInvoice.Visible = False
        '
        'ChkSaleReturn
        '
        Me.ChkSaleReturn.AutoSize = True
        Me.ChkSaleReturn.Location = New System.Drawing.Point(579, 36)
        Me.ChkSaleReturn.Name = "ChkSaleReturn"
        Me.ChkSaleReturn.Size = New System.Drawing.Size(83, 17)
        Me.ChkSaleReturn.TabIndex = 335
        Me.ChkSaleReturn.Text = "Sale-return"
        Me.ChkSaleReturn.UseVisualStyleBackColor = True
        Me.ChkSaleReturn.Visible = False
        '
        'ChkVendorInvoice
        '
        Me.ChkVendorInvoice.AutoSize = True
        Me.ChkVendorInvoice.Location = New System.Drawing.Point(385, 35)
        Me.ChkVendorInvoice.Name = "ChkVendorInvoice"
        Me.ChkVendorInvoice.Size = New System.Drawing.Size(103, 17)
        Me.ChkVendorInvoice.TabIndex = 332
        Me.ChkVendorInvoice.Text = "Vendor Invoice"
        Me.ChkVendorInvoice.UseVisualStyleBackColor = True
        '
        'ChkAll
        '
        Me.ChkAll.AutoSize = True
        Me.ChkAll.Location = New System.Drawing.Point(385, 55)
        Me.ChkAll.Name = "ChkAll"
        Me.ChkAll.Size = New System.Drawing.Size(39, 17)
        Me.ChkAll.TabIndex = 331
        Me.ChkAll.Text = "All"
        Me.ChkAll.UseVisualStyleBackColor = True
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(472, 315)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(453, 232)
        Me.RadGroupBox5.TabIndex = 324
        Me.RadGroupBox5.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = "cbgDoc"
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(433, 182)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocSelect)
        Me.Panel3.Controls.Add(Me.chkLocAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(433, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.AccessibleName = "chk_doc_select"
        Me.chkLocSelect.Location = New System.Drawing.Point(197, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.AccessibleName = "chk_All"
        Me.chkLocAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLocAll.Location = New System.Drawing.Point(148, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        Me.chkLocAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rdoTransfer)
        Me.RadGroupBox4.Controls.Add(Me.rdoPur)
        Me.RadGroupBox4.Controls.Add(Me.rdoSale)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(11, 36)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(238, 28)
        Me.RadGroupBox4.TabIndex = 320
        '
        'rdoTransfer
        '
        Me.rdoTransfer.Location = New System.Drawing.Point(156, 3)
        Me.rdoTransfer.Name = "rdoTransfer"
        Me.rdoTransfer.Size = New System.Drawing.Size(60, 18)
        Me.rdoTransfer.TabIndex = 2
        Me.rdoTransfer.Text = "Transfer"
        '
        'rdoPur
        '
        Me.rdoPur.Location = New System.Drawing.Point(71, 3)
        Me.rdoPur.Name = "rdoPur"
        Me.rdoPur.Size = New System.Drawing.Size(65, 18)
        Me.rdoPur.TabIndex = 1
        Me.rdoPur.Text = "Purchase"
        '
        'rdoSale
        '
        Me.rdoSale.Location = New System.Drawing.Point(15, 3)
        Me.rdoSale.Name = "rdoSale"
        Me.rdoSale.Size = New System.Drawing.Size(41, 18)
        Me.rdoSale.TabIndex = 0
        Me.rdoSale.Text = "Sale"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cgvRate)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "Tax Rate"
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 315)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(453, 232)
        Me.RadGroupBox3.TabIndex = 319
        Me.RadGroupBox3.Text = "Tax Rate"
        '
        'cgvRate
        '
        Me.cgvRate.CheckedValue = Nothing
        Me.cgvRate.DataSource = Nothing
        Me.cgvRate.DisplayMember = "Name"
        Me.cgvRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvRate.Location = New System.Drawing.Point(10, 40)
        Me.cgvRate.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvRate.MyShowHeadrText = False
        Me.cgvRate.Name = "cgvRate"
        Me.cgvRate.Size = New System.Drawing.Size(433, 182)
        Me.cgvRate.TabIndex = 2
        Me.cgvRate.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkRateselect)
        Me.Panel1.Controls.Add(Me.chkrateall)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(433, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkRateselect
        '
        Me.chkRateselect.Location = New System.Drawing.Point(192, 1)
        Me.chkRateselect.MyLinkLable1 = Nothing
        Me.chkRateselect.MyLinkLable2 = Nothing
        Me.chkRateselect.Name = "chkRateselect"
        Me.chkRateselect.Size = New System.Drawing.Size(50, 18)
        Me.chkRateselect.TabIndex = 2
        Me.chkRateselect.Text = "Select"
        '
        'chkrateall
        '
        Me.chkrateall.Location = New System.Drawing.Point(144, 1)
        Me.chkrateall.MyLinkLable1 = Nothing
        Me.chkrateall.MyLinkLable2 = Nothing
        Me.chkrateall.Name = "chkrateall"
        Me.chkrateall.Size = New System.Drawing.Size(33, 18)
        Me.chkrateall.TabIndex = 1
        Me.chkrateall.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cgvTaxCode)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Tax Code"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 77)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(453, 232)
        Me.RadGroupBox2.TabIndex = 318
        Me.RadGroupBox2.Text = "Tax Code"
        '
        'cgvTaxCode
        '
        Me.cgvTaxCode.CheckedValue = Nothing
        Me.cgvTaxCode.DataSource = Nothing
        Me.cgvTaxCode.DisplayMember = "Name"
        Me.cgvTaxCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvTaxCode.Location = New System.Drawing.Point(10, 40)
        Me.cgvTaxCode.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvTaxCode.MyShowHeadrText = False
        Me.cgvTaxCode.Name = "cgvTaxCode"
        Me.cgvTaxCode.Size = New System.Drawing.Size(433, 182)
        Me.cgvTaxCode.TabIndex = 2
        Me.cgvTaxCode.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkSelectTax)
        Me.Panel2.Controls.Add(Me.chkTaxall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(433, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkSelectTax
        '
        Me.chkSelectTax.Location = New System.Drawing.Point(192, 1)
        Me.chkSelectTax.MyLinkLable1 = Nothing
        Me.chkSelectTax.MyLinkLable2 = Nothing
        Me.chkSelectTax.Name = "chkSelectTax"
        Me.chkSelectTax.Size = New System.Drawing.Size(50, 18)
        Me.chkSelectTax.TabIndex = 2
        Me.chkSelectTax.Text = "Select"
        '
        'chkTaxall
        '
        Me.chkTaxall.Location = New System.Drawing.Point(144, 1)
        Me.chkTaxall.MyLinkLable1 = Nothing
        Me.chkTaxall.MyLinkLable2 = Nothing
        Me.chkTaxall.Name = "chkTaxall"
        Me.chkTaxall.Size = New System.Drawing.Size(33, 18)
        Me.chkTaxall.TabIndex = 1
        Me.chkTaxall.Text = "All"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(257, 9)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(119, 20)
        Me.txtToDate.TabIndex = 315
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(88, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(116, 20)
        Me.txtFromDate.TabIndex = 314
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(204, 8)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 316
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 317
        Me.RadLabel1.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(891, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 323
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(78, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 322
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(4, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 321
        Me.btnPrint.Text = ">>>"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(964, 620)
        Me.SplitContainer1.SplitterDistance = 586
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
        Me.RadPageView1.Size = New System.Drawing.Size(964, 586)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(943, 538)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(943, 373)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(943, 373)
        Me.gv.TabIndex = 324
        Me.gv.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(152, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 332
        Me.btnExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(964, 20)
        Me.RadMenu1.TabIndex = 17
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
        'ChkItemWise
        '
        Me.ChkItemWise.AutoSize = True
        Me.ChkItemWise.Location = New System.Drawing.Point(494, 32)
        Me.ChkItemWise.Name = "ChkItemWise"
        Me.ChkItemWise.Size = New System.Drawing.Size(76, 17)
        Me.ChkItemWise.TabIndex = 340
        Me.ChkItemWise.Text = "Item Wise"
        Me.ChkItemWise.UseVisualStyleBackColor = True
        '
        'FrmTaxTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 640)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmTaxTracking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tax Tracking"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkStateselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkStateAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rdoTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoPur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkRateselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkrateall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkSelectTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTaxall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvRate As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkRateselect As common.Controls.MyRadioButton
    Friend WithEvents chkrateall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvTaxCode As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkSelectTax As common.Controls.MyRadioButton
    Friend WithEvents chkTaxall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdoPur As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdoSale As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ChkAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkARinvoice As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSRInter As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaleInvoice As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSaleReturn As System.Windows.Forms.CheckBox
    Friend WithEvents ChkVendorInvoice As System.Windows.Forms.CheckBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Chksummary As System.Windows.Forms.CheckBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvState As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkStateselect As common.Controls.MyRadioButton
    Friend WithEvents chkStateAll As common.Controls.MyRadioButton
    Friend WithEvents rdoTransfer As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkTransfer As System.Windows.Forms.CheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ChkItemWise As System.Windows.Forms.CheckBox
End Class

