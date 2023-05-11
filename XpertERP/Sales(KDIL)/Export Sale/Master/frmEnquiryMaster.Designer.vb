<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEnquiryMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEnquiryMaster))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.btncreate_cust = New Telerik.WinControls.UI.RadButton
        Me.txtcust_code = New common.Controls.MyLabel
        Me.txtcust_name = New common.Controls.MyLabel
        Me.txtcityName = New common.Controls.MyLabel
        Me.txtstateName = New common.Controls.MyLabel
        Me.txtcountryName = New common.Controls.MyLabel
        Me.fndCountry = New common.UserControls.txtFinder
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.fndstate = New common.UserControls.txtFinder
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.fndCity = New common.UserControls.txtFinder
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.txtAdd2 = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtAdd1 = New common.Controls.MyTextBox
        Me.txtAdd3 = New common.Controls.MyTextBox
        Me.CmbTransaction = New common.Controls.MyComboBox
        Me.lblTransaction = New common.Controls.MyLabel
        Me.txtCustomerName = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblCode = New common.Controls.MyLabel
        Me.lblBomDate = New common.Controls.MyLabel
        Me.dtpDate = New common.Controls.MyDateTimePicker
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtCode = New common.UserControls.txtNavigator
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.btncreate_cust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcust_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcust_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcountryName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(830, 376)
        Me.SplitContainer1.SplitterDistance = 338
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
        Me.RadPageView1.Size = New System.Drawing.Size(830, 318)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.btncreate_cust)
        Me.RadPageViewPage1.Controls.Add(Me.txtcust_code)
        Me.RadPageViewPage1.Controls.Add(Me.txtcust_name)
        Me.RadPageViewPage1.Controls.Add(Me.txtcityName)
        Me.RadPageViewPage1.Controls.Add(Me.txtstateName)
        Me.RadPageViewPage1.Controls.Add(Me.txtcountryName)
        Me.RadPageViewPage1.Controls.Add(Me.fndCountry)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.fndstate)
        Me.RadPageViewPage1.Controls.Add(Me.fndCity)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd2)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd1)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd3)
        Me.RadPageViewPage1.Controls.Add(Me.CmbTransaction)
        Me.RadPageViewPage1.Controls.Add(Me.lblTransaction)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblBomDate)
        Me.RadPageViewPage1.Controls.Add(Me.dtpDate)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(809, 270)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'btncreate_cust
        '
        Me.btncreate_cust.Image = CType(resources.GetObject("btncreate_cust.Image"), System.Drawing.Image)
        Me.btncreate_cust.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.btncreate_cust.Location = New System.Drawing.Point(6, 230)
        Me.btncreate_cust.Name = "btncreate_cust"
        Me.btncreate_cust.Size = New System.Drawing.Size(105, 18)
        Me.btncreate_cust.TabIndex = 10
        Me.btncreate_cust.Text = "Create Customer"
        Me.btncreate_cust.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcust_code
        '
        Me.txtcust_code.AutoSize = False
        Me.txtcust_code.BorderVisible = True
        Me.txtcust_code.Location = New System.Drawing.Point(117, 229)
        Me.txtcust_code.Name = "txtcust_code"
        Me.txtcust_code.Size = New System.Drawing.Size(144, 19)
        Me.txtcust_code.TabIndex = 1370
        Me.txtcust_code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcust_name
        '
        Me.txtcust_name.AutoSize = False
        Me.txtcust_name.BorderVisible = True
        Me.txtcust_name.Location = New System.Drawing.Point(264, 229)
        Me.txtcust_name.Name = "txtcust_name"
        Me.txtcust_name.Size = New System.Drawing.Size(411, 19)
        Me.txtcust_name.TabIndex = 1369
        Me.txtcust_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcityName
        '
        Me.txtcityName.AutoSize = False
        Me.txtcityName.BorderVisible = True
        Me.txtcityName.Location = New System.Drawing.Point(264, 182)
        Me.txtcityName.Name = "txtcityName"
        Me.txtcityName.Size = New System.Drawing.Size(411, 19)
        Me.txtcityName.TabIndex = 1366
        Me.txtcityName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtstateName
        '
        Me.txtstateName.AutoSize = False
        Me.txtstateName.BorderVisible = True
        Me.txtstateName.Location = New System.Drawing.Point(264, 159)
        Me.txtstateName.Name = "txtstateName"
        Me.txtstateName.Size = New System.Drawing.Size(411, 19)
        Me.txtstateName.TabIndex = 1366
        Me.txtstateName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcountryName
        '
        Me.txtcountryName.AutoSize = False
        Me.txtcountryName.BorderVisible = True
        Me.txtcountryName.Location = New System.Drawing.Point(264, 136)
        Me.txtcountryName.Name = "txtcountryName"
        Me.txtcountryName.Size = New System.Drawing.Size(411, 19)
        Me.txtcountryName.TabIndex = 1365
        Me.txtcountryName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndCountry
        '
        Me.fndCountry.Location = New System.Drawing.Point(118, 136)
        Me.fndCountry.MendatroryField = False
        Me.fndCountry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCountry.MyLinkLable1 = Me.MyLabel7
        Me.fndCountry.MyLinkLable2 = Me.txtcountryName
        Me.fndCountry.MyReadOnly = False
        Me.fndCountry.Name = "fndCountry"
        Me.fndCountry.Size = New System.Drawing.Size(143, 19)
        Me.fndCountry.TabIndex = 6
        Me.fndCountry.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(3, 139)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel7.TabIndex = 1364
        Me.MyLabel7.Text = "Country"
        '
        'fndstate
        '
        Me.fndstate.Location = New System.Drawing.Point(118, 159)
        Me.fndstate.MendatroryField = False
        Me.fndstate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstate.MyLinkLable1 = Me.RadLabel6
        Me.fndstate.MyLinkLable2 = Me.txtstateName
        Me.fndstate.MyReadOnly = False
        Me.fndstate.Name = "fndstate"
        Me.fndstate.Size = New System.Drawing.Size(143, 19)
        Me.fndstate.TabIndex = 7
        Me.fndstate.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(3, 163)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel6.TabIndex = 1363
        Me.RadLabel6.Text = "State"
        '
        'fndCity
        '
        Me.fndCity.Location = New System.Drawing.Point(118, 182)
        Me.fndCity.MendatroryField = False
        Me.fndCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCity.MyLinkLable1 = Me.RadLabel5
        Me.fndCity.MyLinkLable2 = Me.txtcityName
        Me.fndCity.MyReadOnly = False
        Me.fndCity.Name = "fndCity"
        Me.fndCity.Size = New System.Drawing.Size(143, 19)
        Me.fndCity.TabIndex = 8
        Me.fndCity.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(3, 186)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(26, 16)
        Me.RadLabel5.TabIndex = 1362
        Me.RadLabel5.Text = "City"
        '
        'txtAdd2
        '
        Me.txtAdd2.Location = New System.Drawing.Point(118, 88)
        Me.txtAdd2.MaxLength = 75
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.MyLabel2
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(557, 20)
        Me.txtAdd2.TabIndex = 4
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 64)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel2.TabIndex = 44
        Me.MyLabel2.Text = "Address"
        '
        'txtAdd1
        '
        Me.txtAdd1.Location = New System.Drawing.Point(118, 65)
        Me.txtAdd1.MaxLength = 150
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.MyLabel2
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(557, 20)
        Me.txtAdd1.TabIndex = 3
        '
        'txtAdd3
        '
        Me.txtAdd3.Location = New System.Drawing.Point(118, 112)
        Me.txtAdd3.MaxLength = 75
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.MyLabel2
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.Size = New System.Drawing.Size(557, 20)
        Me.txtAdd3.TabIndex = 5
        '
        'CmbTransaction
        '
        Me.CmbTransaction.AllowShowFocusCues = False
        Me.CmbTransaction.AutoCompleteDisplayMember = Nothing
        Me.CmbTransaction.AutoCompleteValueMember = Nothing
        Me.CmbTransaction.CaseSensitive = True
        Me.CmbTransaction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbTransaction.Location = New System.Drawing.Point(118, 205)
        Me.CmbTransaction.MendatroryField = True
        Me.CmbTransaction.MyLinkLable1 = Me.lblTransaction
        Me.CmbTransaction.MyLinkLable2 = Nothing
        Me.CmbTransaction.Name = "CmbTransaction"
        Me.CmbTransaction.Size = New System.Drawing.Size(143, 20)
        Me.CmbTransaction.TabIndex = 9
        '
        'lblTransaction
        '
        Me.lblTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransaction.Location = New System.Drawing.Point(3, 207)
        Me.lblTransaction.Name = "lblTransaction"
        Me.lblTransaction.Size = New System.Drawing.Size(94, 16)
        Me.lblTransaction.TabIndex = 65
        Me.lblTransaction.Text = "Transaction Type"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Location = New System.Drawing.Point(118, 41)
        Me.txtCustomerName.MaxLength = 200
        Me.txtCustomerName.MendatroryField = True
        Me.txtCustomerName.MyLinkLable1 = Me.MyLabel1
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(557, 20)
        Me.txtCustomerName.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 42)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel1.TabIndex = 43
        Me.MyLabel1.Text = "Name"
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(3, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 41
        Me.lblCode.Text = "Code"
        '
        'lblBomDate
        '
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(556, 18)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBomDate.TabIndex = 42
        Me.lblBomDate.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(589, 17)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011"
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(415, 17)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(118, 16)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(292, 21)
        Me.txtCode.TabIndex = 40
        Me.txtCode.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(809, 356)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(809, 356)
        Me.UcAttachment1.TabIndex = 6
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(830, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(745, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(85, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(6, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmEnquiryMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 376)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmEnquiryMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmEnquiryMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.btncreate_cust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcust_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcust_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcountryName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents CmbTransaction As common.Controls.MyComboBox
    Friend WithEvents lblTransaction As common.Controls.MyLabel
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents fndCountry As common.UserControls.txtFinder
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents fndstate As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents fndCity As common.UserControls.txtFinder
    Friend WithEvents txtcityName As common.Controls.MyLabel
    Friend WithEvents txtstateName As common.Controls.MyLabel
    Friend WithEvents txtcountryName As common.Controls.MyLabel
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtcust_code As common.Controls.MyLabel
    Friend WithEvents txtcust_name As common.Controls.MyLabel
    Friend WithEvents btncreate_cust As Telerik.WinControls.UI.RadButton
End Class

