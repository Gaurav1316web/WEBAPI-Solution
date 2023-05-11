Imports XpertERPEngine
Imports common
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptPurchaseReco
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.chkMismatchDoc = New Telerik.WinControls.UI.RadCheckBox()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.cboType = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndMultiVendorGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndMultiAccSet = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtMultAccountNo = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtTransaction = New common.UserControls.txtMultiSelectFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton5 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadSplitButton2 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMismatchDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton2)
        Me.SplitContainer1.Size = New System.Drawing.Size(900, 495)
        Me.SplitContainer1.SplitterDistance = 457
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(900, 437)
        Me.RadPageView1.TabIndex = 71
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.chkMismatchDoc)
        Me.RadPageViewPage1.Controls.Add(Me.ToDate)
        Me.RadPageViewPage1.Controls.Add(Me.fromDate)
        Me.RadPageViewPage1.Controls.Add(Me.cboType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.fndMultiVendorGroup)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.fndMultiAccSet)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultAccountNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransaction)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(879, 389)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(171, 4)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(8, 27)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel9.TabIndex = 325
        Me.MyLabel9.Text = "Report Type"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(8, 4)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(64, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "Date Range"
        '
        'chkMismatchDoc
        '
        Me.chkMismatchDoc.Location = New System.Drawing.Point(282, 4)
        Me.chkMismatchDoc.Name = "chkMismatchDoc"
        Me.chkMismatchDoc.Size = New System.Drawing.Size(115, 18)
        Me.chkMismatchDoc.TabIndex = 351
        Me.chkMismatchDoc.Text = "Show Mismatched "
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(198, 3)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(82, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(88, 3)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(82, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(88, 27)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(309, 20)
        Me.cboType.TabIndex = 324
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(8, 73)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel4.TabIndex = 350
        Me.MyLabel4.Text = "Vendor Group"
        '
        'fndMultiVendorGroup
        '
        Me.fndMultiVendorGroup.arrDispalyMember = Nothing
        Me.fndMultiVendorGroup.arrValueMember = Nothing
        Me.fndMultiVendorGroup.Location = New System.Drawing.Point(88, 74)
        Me.fndMultiVendorGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMultiVendorGroup.MyLinkLable1 = Me.MyLabel4
        Me.fndMultiVendorGroup.MyLinkLable2 = Nothing
        Me.fndMultiVendorGroup.MyNullText = "All"
        Me.fndMultiVendorGroup.Name = "fndMultiVendorGroup"
        Me.fndMultiVendorGroup.Size = New System.Drawing.Size(309, 19)
        Me.fndMultiVendorGroup.TabIndex = 349
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 50)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel1.TabIndex = 348
        Me.MyLabel1.Text = "Account Set"
        '
        'fndMultiAccSet
        '
        Me.fndMultiAccSet.arrDispalyMember = Nothing
        Me.fndMultiAccSet.arrValueMember = Nothing
        Me.fndMultiAccSet.Location = New System.Drawing.Point(88, 51)
        Me.fndMultiAccSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMultiAccSet.MyLinkLable1 = Me.MyLabel1
        Me.fndMultiAccSet.MyLinkLable2 = Nothing
        Me.fndMultiAccSet.MyNullText = "All"
        Me.fndMultiAccSet.Name = "fndMultiAccSet"
        Me.fndMultiAccSet.Size = New System.Drawing.Size(309, 19)
        Me.fndMultiAccSet.TabIndex = 347
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(8, 142)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(65, 18)
        Me.MyLabel6.TabIndex = 346
        Me.MyLabel6.Text = "Account No"
        '
        'txtMultAccountNo
        '
        Me.txtMultAccountNo.arrDispalyMember = Nothing
        Me.txtMultAccountNo.arrValueMember = Nothing
        Me.txtMultAccountNo.Location = New System.Drawing.Point(88, 143)
        Me.txtMultAccountNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultAccountNo.MyLinkLable1 = Me.MyLabel6
        Me.txtMultAccountNo.MyLinkLable2 = Nothing
        Me.txtMultAccountNo.MyNullText = "All"
        Me.txtMultAccountNo.Name = "txtMultAccountNo"
        Me.txtMultAccountNo.Size = New System.Drawing.Size(309, 19)
        Me.txtMultAccountNo.TabIndex = 345
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(8, 119)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(43, 18)
        Me.MyLabel3.TabIndex = 334
        Me.MyLabel3.Text = "Vendor"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(88, 120)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.MyLabel3
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(309, 19)
        Me.txtCustomer.TabIndex = 333
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(8, 96)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 332
        Me.MyLabel2.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(88, 97)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel2
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(309, 19)
        Me.txtLocation.TabIndex = 331
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(8, 165)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel5.TabIndex = 328
        Me.MyLabel5.Text = "Transaction"
        Me.MyLabel5.Visible = False
        '
        'txtTransaction
        '
        Me.txtTransaction.arrDispalyMember = Nothing
        Me.txtTransaction.arrValueMember = Nothing
        Me.txtTransaction.Location = New System.Drawing.Point(88, 166)
        Me.txtTransaction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransaction.MyLinkLable1 = Me.MyLabel5
        Me.txtTransaction.MyLinkLable2 = Nothing
        Me.txtTransaction.MyNullText = "All"
        Me.txtTransaction.Name = "txtTransaction"
        Me.txtTransaction.Size = New System.Drawing.Size(309, 19)
        Me.txtTransaction.TabIndex = 327
        Me.txtTransaction.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(879, 389)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(879, 389)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(900, 20)
        Me.rdmenufile.TabIndex = 70
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
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
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(150, 9)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(65, 18)
        Me.btnBack.TabIndex = 155
        Me.btnBack.Text = "<< Back "
        Me.btnBack.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(820, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 154
        Me.btnClose.Text = "Close"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.PDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(219, 9)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton1.TabIndex = 153
        Me.RadSplitButton1.Text = "Export"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "RadMenuItem1"
        Me.rmExport.AccessibleName = "RadMenuItem1"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Excel"
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(6, 9)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 150
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(78, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 149
        Me.btnReset.Text = "Reset"
        '
        'RadSplitButton5
        '
        Me.RadSplitButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton5.Location = New System.Drawing.Point(-805, 0)
        Me.RadSplitButton5.Name = "RadSplitButton5"
        Me.RadSplitButton5.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton5.TabIndex = 151
        Me.RadSplitButton5.Text = "Export"
        '
        'RadSplitButton2
        '
        Me.RadSplitButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSetting, Me.rmSend})
        Me.RadSplitButton2.Location = New System.Drawing.Point(303, 9)
        Me.RadSplitButton2.Name = "RadSplitButton2"
        Me.RadSplitButton2.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton2.TabIndex = 152
        Me.RadSplitButton2.Text = "E-Mail/SMS"
        Me.RadSplitButton2.Visible = False
        '
        'rmSetting
        '
        Me.rmSetting.AccessibleDescription = "EMail/SMS Setting"
        Me.rmSetting.AccessibleName = "EMail/SMS Setting"
        Me.rmSetting.Name = "rmSetting"
        Me.rmSetting.Text = "EMail/SMS Setting"
        '
        'rmSend
        '
        Me.rmSend.AccessibleDescription = "EMail/SMS Send"
        Me.rmSend.AccessibleName = "EMail/SMS Send"
        Me.rmSend.Name = "rmSend"
        Me.rmSend.Text = "EMail/SMS Send"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "PDF"
        Me.rmPDF.AccessibleName = "PDF"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'rptPurchaseReco
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 495)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptPurchaseReco"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Purchase Reco"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMismatchDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
Friend WithEvents RadLabel2 As common.Controls.MyLabel
Friend WithEvents RadLabel1 As common.Controls.MyLabel
Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
Friend WithEvents Gv1 As common.UserControls.MyRadGridView
Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
Friend WithEvents RadSplitButton5 As Telerik.WinControls.UI.RadSplitButton
Friend WithEvents RadSplitButton2 As Telerik.WinControls.UI.RadSplitButton
Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
Friend WithEvents rmSetting As Telerik.WinControls.UI.RadMenuItem
Friend WithEvents rmSend As Telerik.WinControls.UI.RadMenuItem
Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
Friend WithEvents MyLabel5 As common.Controls.MyLabel
Friend WithEvents txtTransaction As common.UserControls.txtMultiSelectFinder
Friend WithEvents MyLabel2 As common.Controls.MyLabel
Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
Friend WithEvents MyLabel3 As common.Controls.MyLabel
Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
Friend WithEvents txtMultAccountNo As common.UserControls.txtMultiSelectFinder
Friend WithEvents MyLabel1 As common.Controls.MyLabel
Friend WithEvents fndMultiAccSet As common.UserControls.txtMultiSelectFinder
Friend WithEvents MyLabel4 As common.Controls.MyLabel
Friend WithEvents fndMultiVendorGroup As common.UserControls.txtMultiSelectFinder
Friend WithEvents chkMismatchDoc As Telerik.WinControls.UI.RadCheckBox
Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents cboType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
End Class

