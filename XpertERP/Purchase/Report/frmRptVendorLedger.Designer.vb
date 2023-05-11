<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptVendorLedger
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkExcludeOpening = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTurnOver = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkImprest = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAdvanceImprest = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAdvanceTravel = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTravel = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTADA = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSalary = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAgainstSalaryAdvance = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludeApplyDocument = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.ddlCurrencyType = New Telerik.WinControls.UI.RadDropDownList()
        Me.ChkchildVendor = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtAccountSet = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtChildVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtVendorGroup = New common.UserControls.txtMultiSelectFinder()
        Me.chkItemWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbLandScape = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbPortrait = New Telerik.WinControls.UI.RadRadioButton()
        Me.ChkVendorWithZero = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbntDocWiseMerge = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnDocWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVendorGrupWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVendorWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.ChkPDC = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgchild = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbtnchildslct = New common.Controls.MyRadioButton()
        Me.rbtnchildall = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkLocSelect = New common.Controls.MyRadioButton()
        Me.chkLocAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVndrGroup = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkVndrSelect = New common.Controls.MyRadioButton()
        Me.chkVndrAll = New common.Controls.MyRadioButton()
        Me.dtptodate = New common.Controls.MyDateTimePicker()
        Me.dtpFromdate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkfully = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkapplied = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkadjustment = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkpayment = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAdvance = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkonaccount = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkcreditnote = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkdebitnote = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkinvoice = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVendor = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkVendorSelect = New common.Controls.MyRadioButton()
        Me.chkVendorAll = New common.Controls.MyRadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvVendor = New common.UserControls.MyRadGridView()
        Me.gvVendorGroup = New common.UserControls.MyRadGridView()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnQExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.QExpExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.QExpCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExcelGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDFGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.pnlAdminSetting = New System.Windows.Forms.Panel()
        Me.chkMismatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkReconcile = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RbtnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RbtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkExcludeOpening, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.chkExcludeOpening.SuspendLayout()
        CType(Me.chkTurnOver, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.chkImprest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdvanceImprest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdvanceTravel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTravel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTADA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAgainstSalaryAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeApplyDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkchildVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbLandScape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbPortrait, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkVendorWithZero, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.rbntDocWiseMerge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDocWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorGrupWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkPDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnchildslct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnchildall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkVndrSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVndrAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.chkfully, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkapplied, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.chkadjustment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkpayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkonaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcreditnote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdebitnote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkinvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVendorGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVendorGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnQExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdminSetting.SuspendLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1052, 465)
        Me.RadPageView1.TabIndex = 3
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1031, 417)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.chkExcludeOpening)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.chkIncludeApplyDocument)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.ddlCurrencyType)
        Me.RadGroupBox1.Controls.Add(Me.ChkchildVendor)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtAccountSet)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtChildVendor)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.txtVendor)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtVendorGroup)
        Me.RadGroupBox1.Controls.Add(Me.chkItemWise)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.ChkVendorWithZero)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.ChkPDC)
        Me.RadGroupBox1.Controls.Add(Me.txtCurrencyCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox7)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1031, 417)
        Me.RadGroupBox1.TabIndex = 3
        '
        'chkExcludeOpening
        '
        Me.chkExcludeOpening.Controls.Add(Me.chkTurnOver)
        Me.chkExcludeOpening.Location = New System.Drawing.Point(123, 157)
        Me.chkExcludeOpening.Name = "chkExcludeOpening"
        '
        '
        '
        Me.chkExcludeOpening.RootElement.StretchHorizontally = True
        Me.chkExcludeOpening.RootElement.StretchVertically = True
        Me.chkExcludeOpening.Size = New System.Drawing.Size(181, 18)
        Me.chkExcludeOpening.TabIndex = 401
        Me.chkExcludeOpening.Text = "Exclude Opening"
        '
        'chkTurnOver
        '
        Me.chkTurnOver.Location = New System.Drawing.Point(113, 0)
        Me.chkTurnOver.Name = "chkTurnOver"
        Me.chkTurnOver.Size = New System.Drawing.Size(70, 18)
        Me.chkTurnOver.TabIndex = 404
        Me.chkTurnOver.Text = "Turn Over"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkImprest)
        Me.GroupBox3.Controls.Add(Me.chkAdvanceImprest)
        Me.GroupBox3.Controls.Add(Me.chkAdvanceTravel)
        Me.GroupBox3.Controls.Add(Me.chkTravel)
        Me.GroupBox3.Controls.Add(Me.chkTADA)
        Me.GroupBox3.Controls.Add(Me.chkSalary)
        Me.GroupBox3.Controls.Add(Me.chkAgainstSalaryAdvance)
        Me.GroupBox3.Location = New System.Drawing.Point(645, 35)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(145, 176)
        Me.GroupBox3.TabIndex = 398
        Me.GroupBox3.TabStop = False
        '
        'chkImprest
        '
        Me.chkImprest.Location = New System.Drawing.Point(6, 126)
        Me.chkImprest.Name = "chkImprest"
        '
        '
        '
        Me.chkImprest.RootElement.StretchHorizontally = True
        Me.chkImprest.RootElement.StretchVertically = True
        Me.chkImprest.Size = New System.Drawing.Size(150, 18)
        Me.chkImprest.TabIndex = 401
        Me.chkImprest.Text = "Imprest"
        '
        'chkAdvanceImprest
        '
        Me.chkAdvanceImprest.Location = New System.Drawing.Point(6, 149)
        Me.chkAdvanceImprest.Name = "chkAdvanceImprest"
        '
        '
        '
        Me.chkAdvanceImprest.RootElement.StretchHorizontally = True
        Me.chkAdvanceImprest.RootElement.StretchVertically = True
        Me.chkAdvanceImprest.Size = New System.Drawing.Size(150, 18)
        Me.chkAdvanceImprest.TabIndex = 398
        Me.chkAdvanceImprest.Text = "Advance Imprest"
        '
        'chkAdvanceTravel
        '
        Me.chkAdvanceTravel.Location = New System.Drawing.Point(6, 103)
        Me.chkAdvanceTravel.Name = "chkAdvanceTravel"
        '
        '
        '
        Me.chkAdvanceTravel.RootElement.StretchHorizontally = True
        Me.chkAdvanceTravel.RootElement.StretchVertically = True
        Me.chkAdvanceTravel.Size = New System.Drawing.Size(150, 18)
        Me.chkAdvanceTravel.TabIndex = 400
        Me.chkAdvanceTravel.Text = "Advance Travel"
        '
        'chkTravel
        '
        Me.chkTravel.Location = New System.Drawing.Point(6, 80)
        Me.chkTravel.Name = "chkTravel"
        '
        '
        '
        Me.chkTravel.RootElement.StretchHorizontally = True
        Me.chkTravel.RootElement.StretchVertically = True
        Me.chkTravel.Size = New System.Drawing.Size(150, 18)
        Me.chkTravel.TabIndex = 399
        Me.chkTravel.Text = "Travel"
        '
        'chkTADA
        '
        Me.chkTADA.Location = New System.Drawing.Point(6, 11)
        Me.chkTADA.Name = "chkTADA"
        '
        '
        '
        Me.chkTADA.RootElement.StretchHorizontally = True
        Me.chkTADA.RootElement.StretchVertically = True
        Me.chkTADA.Size = New System.Drawing.Size(150, 18)
        Me.chkTADA.TabIndex = 398
        Me.chkTADA.Text = "TA-DA"
        '
        'chkSalary
        '
        Me.chkSalary.Location = New System.Drawing.Point(6, 34)
        Me.chkSalary.Name = "chkSalary"
        '
        '
        '
        Me.chkSalary.RootElement.StretchHorizontally = True
        Me.chkSalary.RootElement.StretchVertically = True
        Me.chkSalary.Size = New System.Drawing.Size(150, 18)
        Me.chkSalary.TabIndex = 397
        Me.chkSalary.Text = "Salary"
        '
        'chkAgainstSalaryAdvance
        '
        Me.chkAgainstSalaryAdvance.Location = New System.Drawing.Point(6, 57)
        Me.chkAgainstSalaryAdvance.Name = "chkAgainstSalaryAdvance"
        '
        '
        '
        Me.chkAgainstSalaryAdvance.RootElement.StretchHorizontally = True
        Me.chkAgainstSalaryAdvance.RootElement.StretchVertically = True
        Me.chkAgainstSalaryAdvance.Size = New System.Drawing.Size(150, 18)
        Me.chkAgainstSalaryAdvance.TabIndex = 396
        Me.chkAgainstSalaryAdvance.Text = "Advance Salary"
        '
        'chkIncludeApplyDocument
        '
        Me.chkIncludeApplyDocument.Location = New System.Drawing.Point(310, 157)
        Me.chkIncludeApplyDocument.Name = "chkIncludeApplyDocument"
        '
        '
        '
        Me.chkIncludeApplyDocument.RootElement.StretchHorizontally = True
        Me.chkIncludeApplyDocument.RootElement.StretchVertically = True
        Me.chkIncludeApplyDocument.Size = New System.Drawing.Size(181, 18)
        Me.chkIncludeApplyDocument.TabIndex = 397
        Me.chkIncludeApplyDocument.Text = "Include Apply Document"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(449, 43)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel5.TabIndex = 394
        Me.MyLabel5.Text = "Currency "
        '
        'ddlCurrencyType
        '
        Me.ddlCurrencyType.DropDownAnimationEnabled = True
        Me.ddlCurrencyType.Location = New System.Drawing.Point(503, 43)
        Me.ddlCurrencyType.Name = "ddlCurrencyType"
        Me.ddlCurrencyType.Size = New System.Drawing.Size(136, 20)
        Me.ddlCurrencyType.TabIndex = 395
        '
        'ChkchildVendor
        '
        Me.ChkchildVendor.Location = New System.Drawing.Point(475, 114)
        Me.ChkchildVendor.Name = "ChkchildVendor"
        '
        '
        '
        Me.ChkchildVendor.RootElement.StretchHorizontally = True
        Me.ChkchildVendor.RootElement.StretchVertically = True
        Me.ChkchildVendor.Size = New System.Drawing.Size(185, 18)
        Me.ChkchildVendor.TabIndex = 393
        Me.ChkchildVendor.Text = "Show Child Vendor Data also"
        Me.ChkchildVendor.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 115)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel4.TabIndex = 392
        Me.MyLabel4.Text = "Account Set"
        '
        'txtAccountSet
        '
        Me.txtAccountSet.arrDispalyMember = Nothing
        Me.txtAccountSet.arrValueMember = Nothing
        Me.txtAccountSet.Location = New System.Drawing.Point(124, 114)
        Me.txtAccountSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountSet.MyLinkLable1 = Me.MyLabel4
        Me.txtAccountSet.MyLinkLable2 = Nothing
        Me.txtAccountSet.MyNullText = "All"
        Me.txtAccountSet.Name = "txtAccountSet"
        Me.txtAccountSet.Size = New System.Drawing.Size(344, 19)
        Me.txtAccountSet.TabIndex = 391
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 136)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 390
        Me.MyLabel2.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(124, 135)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel2
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 389
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(942, 11)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel3.TabIndex = 388
        Me.MyLabel3.Text = "Child Vendor"
        Me.MyLabel3.Visible = False
        '
        'txtChildVendor
        '
        Me.txtChildVendor.arrDispalyMember = Nothing
        Me.txtChildVendor.arrValueMember = Nothing
        Me.txtChildVendor.Location = New System.Drawing.Point(922, 35)
        Me.txtChildVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChildVendor.MyLinkLable1 = Me.MyLabel3
        Me.txtChildVendor.MyLinkLable2 = Nothing
        Me.txtChildVendor.MyNullText = "All"
        Me.txtChildVendor.Name = "txtChildVendor"
        Me.txtChildVendor.Size = New System.Drawing.Size(100, 19)
        Me.txtChildVendor.TabIndex = 387
        Me.txtChildVendor.Visible = False
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(13, 93)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(43, 18)
        Me.lblCustomerGroup.TabIndex = 386
        Me.lblCustomerGroup.Text = "Vendor"
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(124, 92)
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyNullText = "All"
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(344, 19)
        Me.txtVendor.TabIndex = 385
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 72)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(77, 18)
        Me.lblLocation.TabIndex = 384
        Me.lblLocation.Text = "Vendor Group"
        '
        'txtVendorGroup
        '
        Me.txtVendorGroup.arrDispalyMember = Nothing
        Me.txtVendorGroup.arrValueMember = Nothing
        Me.txtVendorGroup.Location = New System.Drawing.Point(124, 71)
        Me.txtVendorGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorGroup.MyLinkLable1 = Me.lblLocation
        Me.txtVendorGroup.MyLinkLable2 = Nothing
        Me.txtVendorGroup.MyNullText = "All"
        Me.txtVendorGroup.Name = "txtVendorGroup"
        Me.txtVendorGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtVendorGroup.TabIndex = 383
        '
        'chkItemWise
        '
        Me.chkItemWise.Location = New System.Drawing.Point(475, 138)
        Me.chkItemWise.Name = "chkItemWise"
        '
        '
        '
        Me.chkItemWise.RootElement.StretchHorizontally = True
        Me.chkItemWise.RootElement.StretchVertically = True
        Me.chkItemWise.Size = New System.Drawing.Size(77, 18)
        Me.chkItemWise.TabIndex = 115
        Me.chkItemWise.Text = "Item Wise"
        Me.chkItemWise.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbLandScape)
        Me.GroupBox1.Controls.Add(Me.rbPortrait)
        Me.GroupBox1.Location = New System.Drawing.Point(260, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(187, 36)
        Me.GroupBox1.TabIndex = 126
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Page Setup"
        '
        'rbLandScape
        '
        Me.rbLandScape.Location = New System.Drawing.Point(96, 13)
        Me.rbLandScape.Name = "rbLandScape"
        Me.rbLandScape.Size = New System.Drawing.Size(77, 18)
        Me.rbLandScape.TabIndex = 4
        Me.rbLandScape.Text = "Land Scape"
        '
        'rbPortrait
        '
        Me.rbPortrait.Location = New System.Drawing.Point(38, 13)
        Me.rbPortrait.Name = "rbPortrait"
        Me.rbPortrait.Size = New System.Drawing.Size(57, 18)
        Me.rbPortrait.TabIndex = 3
        Me.rbPortrait.Text = "Portrait"
        '
        'ChkVendorWithZero
        '
        Me.ChkVendorWithZero.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkVendorWithZero.Location = New System.Drawing.Point(475, 70)
        Me.ChkVendorWithZero.Name = "ChkVendorWithZero"
        '
        '
        '
        Me.ChkVendorWithZero.RootElement.StretchHorizontally = True
        Me.ChkVendorWithZero.RootElement.StretchVertically = True
        Me.ChkVendorWithZero.Size = New System.Drawing.Size(151, 18)
        Me.ChkVendorWithZero.TabIndex = 18
        Me.ChkVendorWithZero.Text = "Vendor with Zero Amount"
        Me.ChkVendorWithZero.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.ChkVendorWithZero.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbntDocWiseMerge)
        Me.GroupBox2.Controls.Add(Me.rbtnDocWise)
        Me.GroupBox2.Controls.Add(Me.chkNone)
        Me.GroupBox2.Controls.Add(Me.chkVendorGrupWise)
        Me.GroupBox2.Controls.Add(Me.chkVendorWise)
        Me.GroupBox2.Location = New System.Drawing.Point(260, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(520, 34)
        Me.GroupBox2.TabIndex = 127
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Summary"
        '
        'rbntDocWiseMerge
        '
        Me.rbntDocWiseMerge.Location = New System.Drawing.Point(312, 12)
        Me.rbntDocWiseMerge.Name = "rbntDocWiseMerge"
        Me.rbntDocWiseMerge.Size = New System.Drawing.Size(135, 18)
        Me.rbntDocWiseMerge.TabIndex = 6
        Me.rbntDocWiseMerge.Text = "Document Wise Merge"
        '
        'rbtnDocWise
        '
        Me.rbtnDocWise.Location = New System.Drawing.Point(449, 12)
        Me.rbtnDocWise.Name = "rbtnDocWise"
        Me.rbtnDocWise.Size = New System.Drawing.Size(67, 18)
        Me.rbtnDocWise.TabIndex = 6
        Me.rbtnDocWise.Text = "Doc Wise"
        '
        'chkNone
        '
        Me.chkNone.Location = New System.Drawing.Point(211, 12)
        Me.chkNone.Name = "chkNone"
        Me.chkNone.Size = New System.Drawing.Size(99, 18)
        Me.chkNone.TabIndex = 5
        Me.chkNone.Text = "Document Wise"
        '
        'chkVendorGrupWise
        '
        Me.chkVendorGrupWise.Location = New System.Drawing.Point(5, 12)
        Me.chkVendorGrupWise.Name = "chkVendorGrupWise"
        Me.chkVendorGrupWise.Size = New System.Drawing.Size(118, 18)
        Me.chkVendorGrupWise.TabIndex = 3
        Me.chkVendorGrupWise.Text = "Vendor Group Wise"
        '
        'chkVendorWise
        '
        Me.chkVendorWise.Location = New System.Drawing.Point(125, 12)
        Me.chkVendorWise.Name = "chkVendorWise"
        Me.chkVendorWise.Size = New System.Drawing.Size(84, 18)
        Me.chkVendorWise.TabIndex = 4
        Me.chkVendorWise.Text = "Vendor Wise"
        '
        'ChkPDC
        '
        Me.ChkPDC.Location = New System.Drawing.Point(475, 92)
        Me.ChkPDC.Name = "ChkPDC"
        '
        '
        '
        Me.ChkPDC.RootElement.StretchHorizontally = True
        Me.ChkPDC.RootElement.StretchVertically = True
        Me.ChkPDC.Size = New System.Drawing.Size(143, 18)
        Me.ChkPDC.TabIndex = 124
        Me.ChkPDC.Text = "PDC Cheque"
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.CalculationExpression = Nothing
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(123, 49)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Me.MyLabel1
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.ReferenceFieldDesc = Nothing
        Me.txtCurrencyCode.ReferenceFieldName = Nothing
        Me.txtCurrencyCode.ReferenceTableName = Nothing
        Me.txtCurrencyCode.Size = New System.Drawing.Size(130, 19)
        Me.txtCurrencyCode.TabIndex = 121
        Me.txtCurrencyCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 50)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel1.TabIndex = 122
        Me.MyLabel1.Text = "Currency"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.cbgchild)
        Me.RadGroupBox7.Controls.Add(Me.Panel4)
        Me.RadGroupBox7.HeaderText = "Child Vendor"
        Me.RadGroupBox7.Location = New System.Drawing.Point(443, 330)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(430, 104)
        Me.RadGroupBox7.TabIndex = 120
        Me.RadGroupBox7.Text = "Child Vendor"
        Me.RadGroupBox7.Visible = False
        '
        'cbgchild
        '
        Me.cbgchild.CheckedValue = Nothing
        Me.cbgchild.DataSource = Nothing
        Me.cbgchild.DisplayMember = "Name"
        Me.cbgchild.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgchild.Location = New System.Drawing.Point(10, 40)
        Me.cbgchild.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgchild.MyShowHeadrText = False
        Me.cbgchild.Name = "cbgchild"
        Me.cbgchild.Size = New System.Drawing.Size(410, 54)
        Me.cbgchild.TabIndex = 2
        Me.cbgchild.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbtnchildslct)
        Me.Panel4.Controls.Add(Me.rbtnchildall)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(410, 20)
        Me.Panel4.TabIndex = 1
        '
        'rbtnchildslct
        '
        Me.rbtnchildslct.Location = New System.Drawing.Point(192, 1)
        Me.rbtnchildslct.MyLinkLable1 = Nothing
        Me.rbtnchildslct.MyLinkLable2 = Nothing
        Me.rbtnchildslct.Name = "rbtnchildslct"
        Me.rbtnchildslct.Size = New System.Drawing.Size(50, 18)
        Me.rbtnchildslct.TabIndex = 2
        Me.rbtnchildslct.Text = "Select"
        '
        'rbtnchildall
        '
        Me.rbtnchildall.Location = New System.Drawing.Point(141, 1)
        Me.rbtnchildall.MyLinkLable1 = Nothing
        Me.rbtnchildall.MyLinkLable2 = Nothing
        Me.rbtnchildall.Name = "rbtnchildall"
        Me.rbtnchildall.Size = New System.Drawing.Size(33, 18)
        Me.rbtnchildall.TabIndex = 1
        Me.rbtnchildall.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(7, 330)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(430, 104)
        Me.RadGroupBox3.TabIndex = 4
        Me.RadGroupBox3.Text = "Location"
        Me.RadGroupBox3.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(410, 54)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocSelect)
        Me.Panel1.Controls.Add(Me.chkLocAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(410, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(141, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgVndrGroup)
        Me.RadGroupBox6.Controls.Add(Me.Panel3)
        Me.RadGroupBox6.HeaderText = "Vendor Group"
        Me.RadGroupBox6.Location = New System.Drawing.Point(7, 217)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(430, 107)
        Me.RadGroupBox6.TabIndex = 19
        Me.RadGroupBox6.Text = "Vendor Group"
        Me.RadGroupBox6.Visible = False
        '
        'cbgVndrGroup
        '
        Me.cbgVndrGroup.CheckedValue = Nothing
        Me.cbgVndrGroup.DataSource = Nothing
        Me.cbgVndrGroup.DisplayMember = "Name"
        Me.cbgVndrGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVndrGroup.Location = New System.Drawing.Point(10, 40)
        Me.cbgVndrGroup.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVndrGroup.MyShowHeadrText = False
        Me.cbgVndrGroup.Name = "cbgVndrGroup"
        Me.cbgVndrGroup.Size = New System.Drawing.Size(410, 57)
        Me.cbgVndrGroup.TabIndex = 2
        Me.cbgVndrGroup.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkVndrSelect)
        Me.Panel3.Controls.Add(Me.chkVndrAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(410, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkVndrSelect
        '
        Me.chkVndrSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkVndrSelect.MyLinkLable1 = Nothing
        Me.chkVndrSelect.MyLinkLable2 = Nothing
        Me.chkVndrSelect.Name = "chkVndrSelect"
        Me.chkVndrSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVndrSelect.TabIndex = 2
        Me.chkVndrSelect.Text = "Select"
        '
        'chkVndrAll
        '
        Me.chkVndrAll.Location = New System.Drawing.Point(141, 1)
        Me.chkVndrAll.MyLinkLable1 = Nothing
        Me.chkVndrAll.MyLinkLable2 = Nothing
        Me.chkVndrAll.Name = "chkVndrAll"
        Me.chkVndrAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVndrAll.TabIndex = 1
        Me.chkVndrAll.Text = "All"
        '
        'dtptodate
        '
        Me.dtptodate.CalculationExpression = Nothing
        Me.dtptodate.CustomFormat = "dd/MM/yyyy"
        Me.dtptodate.FieldCode = Nothing
        Me.dtptodate.FieldDesc = Nothing
        Me.dtptodate.FieldMaxLength = 0
        Me.dtptodate.FieldName = Nothing
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.isCalculatedField = False
        Me.dtptodate.IsSourceFromTable = False
        Me.dtptodate.IsSourceFromValueList = False
        Me.dtptodate.IsUnique = False
        Me.dtptodate.Location = New System.Drawing.Point(124, 27)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.ReferenceFieldDesc = Nothing
        Me.dtptodate.ReferenceFieldName = Nothing
        Me.dtptodate.ReferenceTableName = Nothing
        Me.dtptodate.Size = New System.Drawing.Size(129, 20)
        Me.dtptodate.TabIndex = 2
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "30/09/2016"
        Me.dtptodate.Value = New Date(2016, 9, 30, 0, 0, 0, 0)
        '
        'dtpFromdate
        '
        Me.dtpFromdate.CalculationExpression = Nothing
        Me.dtpFromdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate.FieldCode = Nothing
        Me.dtpFromdate.FieldDesc = Nothing
        Me.dtpFromdate.FieldMaxLength = 0
        Me.dtpFromdate.FieldName = Nothing
        Me.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate.isCalculatedField = False
        Me.dtpFromdate.IsSourceFromTable = False
        Me.dtpFromdate.IsSourceFromValueList = False
        Me.dtpFromdate.IsUnique = False
        Me.dtpFromdate.Location = New System.Drawing.Point(124, 4)
        Me.dtpFromdate.MendatroryField = False
        Me.dtpFromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.MyLinkLable1 = Nothing
        Me.dtpFromdate.MyLinkLable2 = Nothing
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.ReferenceFieldDesc = Nothing
        Me.dtpFromdate.ReferenceFieldName = Nothing
        Me.dtpFromdate.ReferenceTableName = Nothing
        Me.dtpFromdate.Size = New System.Drawing.Size(129, 20)
        Me.dtpFromdate.TabIndex = 1
        Me.dtpFromdate.TabStop = False
        Me.dtpFromdate.Text = "01/09/2016"
        Me.dtpFromdate.Value = New Date(2016, 9, 1, 0, 0, 0, 0)
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.chkfully)
        Me.RadGroupBox5.Controls.Add(Me.chkapplied)
        Me.RadGroupBox5.HeaderText = "Show"
        Me.RadGroupBox5.Location = New System.Drawing.Point(880, 271)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(145, 77)
        Me.RadGroupBox5.TabIndex = 5
        Me.RadGroupBox5.Text = "Show"
        Me.RadGroupBox5.Visible = False
        '
        'chkfully
        '
        Me.chkfully.Location = New System.Drawing.Point(5, 47)
        Me.chkfully.Name = "chkfully"
        '
        '
        '
        Me.chkfully.RootElement.StretchHorizontally = True
        Me.chkfully.RootElement.StretchVertically = True
        Me.chkfully.Size = New System.Drawing.Size(128, 18)
        Me.chkfully.TabIndex = 2
        Me.chkfully.Text = "Fully Paid Transaction"
        '
        'chkapplied
        '
        Me.chkapplied.Location = New System.Drawing.Point(5, 23)
        Me.chkapplied.Name = "chkapplied"
        '
        '
        '
        Me.chkapplied.RootElement.StretchHorizontally = True
        Me.chkapplied.RootElement.StretchVertically = True
        Me.chkapplied.Size = New System.Drawing.Size(96, 18)
        Me.chkapplied.TabIndex = 1
        Me.chkapplied.Text = "Applied Details"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkadjustment)
        Me.RadGroupBox4.Controls.Add(Me.chkpayment)
        Me.RadGroupBox4.Controls.Add(Me.chkAdvance)
        Me.RadGroupBox4.Controls.Add(Me.chkonaccount)
        Me.RadGroupBox4.Controls.Add(Me.chkcreditnote)
        Me.RadGroupBox4.Controls.Add(Me.chkdebitnote)
        Me.RadGroupBox4.Controls.Add(Me.chkinvoice)
        Me.RadGroupBox4.HeaderText = "Select Transaction Type"
        Me.RadGroupBox4.Location = New System.Drawing.Point(880, 75)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(145, 190)
        Me.RadGroupBox4.TabIndex = 4
        Me.RadGroupBox4.Text = "Select Transaction Type"
        Me.RadGroupBox4.Visible = False
        '
        'chkadjustment
        '
        Me.chkadjustment.Location = New System.Drawing.Point(13, 163)
        Me.chkadjustment.Name = "chkadjustment"
        '
        '
        '
        Me.chkadjustment.RootElement.StretchHorizontally = True
        Me.chkadjustment.RootElement.StretchVertically = True
        Me.chkadjustment.Size = New System.Drawing.Size(78, 18)
        Me.chkadjustment.TabIndex = 7
        Me.chkadjustment.Text = "Adjustment"
        '
        'chkpayment
        '
        Me.chkpayment.Location = New System.Drawing.Point(13, 138)
        Me.chkpayment.Name = "chkpayment"
        '
        '
        '
        Me.chkpayment.RootElement.StretchHorizontally = True
        Me.chkpayment.RootElement.StretchVertically = True
        Me.chkpayment.Size = New System.Drawing.Size(64, 18)
        Me.chkpayment.TabIndex = 6
        Me.chkpayment.Text = "Payment"
        '
        'chkAdvance
        '
        Me.chkAdvance.Location = New System.Drawing.Point(13, 90)
        Me.chkAdvance.Name = "chkAdvance"
        '
        '
        '
        Me.chkAdvance.RootElement.StretchHorizontally = True
        Me.chkAdvance.RootElement.StretchVertically = True
        Me.chkAdvance.Size = New System.Drawing.Size(63, 18)
        Me.chkAdvance.TabIndex = 5
        Me.chkAdvance.Text = "Advance"
        '
        'chkonaccount
        '
        Me.chkonaccount.Location = New System.Drawing.Point(12, 42)
        Me.chkonaccount.Name = "chkonaccount"
        '
        '
        '
        Me.chkonaccount.RootElement.StretchHorizontally = True
        Me.chkonaccount.RootElement.StretchVertically = True
        Me.chkonaccount.Size = New System.Drawing.Size(79, 18)
        Me.chkonaccount.TabIndex = 4
        Me.chkonaccount.Text = "On Account"
        '
        'chkcreditnote
        '
        Me.chkcreditnote.Location = New System.Drawing.Point(13, 114)
        Me.chkcreditnote.Name = "chkcreditnote"
        '
        '
        '
        Me.chkcreditnote.RootElement.StretchHorizontally = True
        Me.chkcreditnote.RootElement.StretchVertically = True
        Me.chkcreditnote.Size = New System.Drawing.Size(78, 18)
        Me.chkcreditnote.TabIndex = 3
        Me.chkcreditnote.Text = "Credit Note"
        '
        'chkdebitnote
        '
        Me.chkdebitnote.Location = New System.Drawing.Point(12, 66)
        Me.chkdebitnote.Name = "chkdebitnote"
        '
        '
        '
        Me.chkdebitnote.RootElement.StretchHorizontally = True
        Me.chkdebitnote.RootElement.StretchVertically = True
        Me.chkdebitnote.Size = New System.Drawing.Size(75, 18)
        Me.chkdebitnote.TabIndex = 2
        Me.chkdebitnote.Text = "Debit Note"
        '
        'chkinvoice
        '
        Me.chkinvoice.Location = New System.Drawing.Point(12, 18)
        Me.chkinvoice.Name = "chkinvoice"
        '
        '
        '
        Me.chkinvoice.RootElement.StretchHorizontally = True
        Me.chkinvoice.RootElement.StretchVertically = True
        Me.chkinvoice.Size = New System.Drawing.Size(56, 18)
        Me.chkinvoice.TabIndex = 1
        Me.chkinvoice.Text = "Invoice"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Vendor"
        Me.RadGroupBox2.Location = New System.Drawing.Point(443, 217)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(430, 108)
        Me.RadGroupBox2.TabIndex = 3
        Me.RadGroupBox2.Text = "Vendor"
        Me.RadGroupBox2.Visible = False
        '
        'cbgVendor
        '
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(410, 58)
        Me.cbgVendor.TabIndex = 2
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkVendorSelect)
        Me.Panel2.Controls.Add(Me.chkVendorAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(410, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 2
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(141, 1)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 1
        Me.chkVendorAll.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(13, 26)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvVendor)
        Me.RadPageViewPage2.Controls.Add(Me.gvVendorGroup)
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1031, 417)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvVendor
        '
        Me.gvVendor.Location = New System.Drawing.Point(0, 295)
        '
        '
        '
        Me.gvVendor.MasterTemplate.AllowAddNewRow = False
        Me.gvVendor.MasterTemplate.AllowEditRow = False
        Me.gvVendor.MasterTemplate.EnableFiltering = True
        Me.gvVendor.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvVendor.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvVendor.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvVendor.Name = "gvVendor"
        Me.gvVendor.ShowGroupPanel = False
        Me.gvVendor.ShowHeaderCellButtons = True
        Me.gvVendor.Size = New System.Drawing.Size(883, 141)
        Me.gvVendor.TabIndex = 3
        '
        'gvVendorGroup
        '
        Me.gvVendorGroup.Location = New System.Drawing.Point(0, 148)
        '
        '
        '
        Me.gvVendorGroup.MasterTemplate.AllowAddNewRow = False
        Me.gvVendorGroup.MasterTemplate.AllowEditRow = False
        Me.gvVendorGroup.MasterTemplate.EnableFiltering = True
        Me.gvVendorGroup.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvVendorGroup.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvVendorGroup.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvVendorGroup.Name = "gvVendorGroup"
        Me.gvVendorGroup.ShowGroupPanel = False
        Me.gvVendorGroup.ShowHeaderCellButtons = True
        Me.gvVendorGroup.Size = New System.Drawing.Size(883, 141)
        Me.gvVendorGroup.TabIndex = 2
        '
        'gv
        '
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(883, 141)
        Me.gv.TabIndex = 1
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(3, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 112
        Me.btnRefresh.Text = ">>>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(980, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(77, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 7
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(151, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnQExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlAdminSetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1052, 494)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 4
        '
        'btnQExport
        '
        Me.btnQExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.QExpExcel, Me.QExpCSV, Me.PDF, Me.BulkExcel, Me.BulkCSV, Me.ExcelGrid, Me.PDFGrid})
        Me.btnQExport.Location = New System.Drawing.Point(226, 4)
        Me.btnQExport.Name = "btnQExport"
        Me.btnQExport.Size = New System.Drawing.Size(103, 18)
        Me.btnQExport.TabIndex = 334
        Me.btnQExport.Text = "Export"
        '
        'QExpExcel
        '
        Me.QExpExcel.Name = "QExpExcel"
        Me.QExpExcel.Text = "Excel"
        '
        'QExpCSV
        '
        Me.QExpCSV.Name = "QExpCSV"
        Me.QExpCSV.Text = "CSV"
        '
        'PDF
        '
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'BulkExcel
        '
        Me.BulkExcel.Name = "BulkExcel"
        Me.BulkExcel.Text = "Bulk Excel"
        '
        'BulkCSV
        '
        Me.BulkCSV.Name = "BulkCSV"
        Me.BulkCSV.Text = "Bulk CSV"
        '
        'ExcelGrid
        '
        Me.ExcelGrid.AccessibleDescription = "ExcelGrid"
        Me.ExcelGrid.AccessibleName = "ExcelGrid"
        Me.ExcelGrid.Name = "ExcelGrid"
        Me.ExcelGrid.Text = "Excel(Grid)"
        '
        'PDFGrid
        '
        Me.PDFGrid.AccessibleDescription = "PDFGrid"
        Me.PDFGrid.AccessibleName = "PDFGrid"
        Me.PDFGrid.Name = "PDFGrid"
        Me.PDFGrid.Text = "PDF(Grid)"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(905, 3)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(69, 18)
        Me.btnBack.TabIndex = 330
        Me.btnBack.Text = "<<Back"
        '
        'pnlAdminSetting
        '
        Me.pnlAdminSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlAdminSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminSetting.Controls.Add(Me.chkMismatch)
        Me.pnlAdminSetting.Controls.Add(Me.chkReconcile)
        Me.pnlAdminSetting.Location = New System.Drawing.Point(535, 3)
        Me.pnlAdminSetting.Name = "pnlAdminSetting"
        Me.pnlAdminSetting.Size = New System.Drawing.Size(170, 19)
        Me.pnlAdminSetting.TabIndex = 329
        Me.pnlAdminSetting.Visible = False
        '
        'chkMismatch
        '
        Me.chkMismatch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMismatch.Location = New System.Drawing.Point(81, -1)
        Me.chkMismatch.Name = "chkMismatch"
        Me.chkMismatch.Size = New System.Drawing.Size(81, 18)
        Me.chkMismatch.TabIndex = 19
        Me.chkMismatch.Text = "Mismatched"
        Me.chkMismatch.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkReconcile
        '
        Me.chkReconcile.Location = New System.Drawing.Point(3, -1)
        Me.chkReconcile.Name = "chkReconcile"
        Me.chkReconcile.Size = New System.Drawing.Size(68, 18)
        Me.chkReconcile.TabIndex = 18
        Me.chkReconcile.Text = "Reconcile"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1052, 20)
        Me.RadMenu1.TabIndex = 16
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RbtnSaveLayout, Me.RbtnDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RbtnSaveLayout
        '
        Me.RbtnSaveLayout.Name = "RbtnSaveLayout"
        Me.RbtnSaveLayout.Text = "Save Layout"
        '
        'RbtnDeleteLayout
        '
        Me.RbtnDeleteLayout.Name = "RbtnDeleteLayout"
        Me.RbtnDeleteLayout.Text = "Delete Layout"
        '
        'frmRptVendorLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1052, 514)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmRptVendorLedger"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Ledger Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkExcludeOpening, System.ComponentModel.ISupportInitialize).EndInit()
        Me.chkExcludeOpening.ResumeLayout(False)
        Me.chkExcludeOpening.PerformLayout()
        CType(Me.chkTurnOver, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.chkImprest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdvanceImprest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdvanceTravel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTravel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTADA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAgainstSalaryAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeApplyDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkchildVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbLandScape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbPortrait, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkVendorWithZero, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.rbntDocWiseMerge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDocWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorGrupWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkPDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnchildslct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnchildall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkVndrSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVndrAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.chkfully, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkapplied, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.chkadjustment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkpayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkonaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcreditnote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdebitnote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkinvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVendorGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVendorGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnQExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdminSetting.ResumeLayout(False)
        Me.pnlAdminSetting.PerformLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVndrGroup As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkVndrSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVndrAll As common.Controls.MyRadioButton
    Friend WithEvents ChkVendorWithZero As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkfully As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkapplied As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkadjustment As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkpayment As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAdvance As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkonaccount As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkcreditnote As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkdebitnote As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkinvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlAdminSetting As System.Windows.Forms.Panel
    Friend WithEvents chkMismatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkReconcile As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkItemWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents chkNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkVendorWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkVendorGrupWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gvVendor As common.UserControls.MyRadGridView
    Friend WithEvents gvVendorGroup As common.UserControls.MyRadGridView
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgchild As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbtnchildslct As common.Controls.MyRadioButton
    Friend WithEvents rbtnchildall As common.Controls.MyRadioButton
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents rbLandScape As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbPortrait As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents ChkPDC As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtChildVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtVendorGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtAccountSet As common.UserControls.txtMultiSelectFinder
    Friend WithEvents ChkchildVendor As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents ddlCurrencyType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents chkAgainstSalaryAdvance As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnDocWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RbtnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RbtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkIncludeApplyDocument As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkSalary As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkImprest As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAdvanceImprest As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAdvanceTravel As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkTravel As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkTADA As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnQExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents QExpExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents QExpCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkExcludeOpening As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExcelGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDFGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkTurnOver As RadCheckBox
    Friend WithEvents rbntDocWiseMerge As RadRadioButton
End Class

