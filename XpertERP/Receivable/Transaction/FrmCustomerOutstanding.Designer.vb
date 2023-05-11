<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomerOutstanding
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.lblpaymentno = New common.Controls.MyLabel()
        Me.CustomerName = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.locationname = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.Fndcustomer = New common.UserControls.txtFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.dtpAgeof = New common.Controls.MyDateTimePicker()
        Me.ChkSecurity = New Telerik.WinControls.UI.RadCheckBox()
        Me.ddlAgedRcvbl = New common.Controls.MyComboBox()
        Me.chkType = New common.Controls.MyCheckBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.ChkISParentCust = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.ddlCurrencyType = New Telerik.WinControls.UI.RadDropDownList()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.rdb = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayour = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.locationname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAgeof, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlAgedRcvbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1140, 489)
        Me.RadPageView1.TabIndex = 22
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(74.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1119, 441)
        Me.RadPageViewPage1.Text = "Transaction"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(1119, 441)
        Me.SplitContainer1.SplitterDistance = 347
        Me.SplitContainer1.TabIndex = 22
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblpaymentno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.CustomerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.locationname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Fndcustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpAgeof)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ChkSecurity)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlAgedRcvbl)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ChkISParentCust)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnPrint)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlCurrencyType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1119, 441)
        Me.SplitContainer2.SplitterDistance = 91
        Me.SplitContainer2.TabIndex = 12119
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(551, 11)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 5
        '
        'lblpaymentno
        '
        Me.lblpaymentno.FieldName = Nothing
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblpaymentno.Location = New System.Drawing.Point(9, 15)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(44, 16)
        Me.lblpaymentno.TabIndex = 402
        Me.lblpaymentno.Text = "Doc No"
        '
        'CustomerName
        '
        Me.CustomerName.AutoSize = False
        Me.CustomerName.BorderVisible = True
        Me.CustomerName.FieldName = Nothing
        Me.CustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomerName.Location = New System.Drawing.Point(276, 61)
        Me.CustomerName.Name = "CustomerName"
        Me.CustomerName.Size = New System.Drawing.Size(310, 18)
        Me.CustomerName.TabIndex = 12118
        Me.CustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(805, 38)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(64, 18)
        Me.RadLabel1.TabIndex = 112
        Me.RadLabel1.Text = "ReportType"
        Me.RadLabel1.Visible = False
        '
        'locationname
        '
        Me.locationname.AutoSize = False
        Me.locationname.BorderVisible = True
        Me.locationname.FieldName = Nothing
        Me.locationname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.locationname.Location = New System.Drawing.Point(276, 37)
        Me.locationname.Name = "locationname"
        Me.locationname.Size = New System.Drawing.Size(310, 18)
        Me.locationname.TabIndex = 12117
        Me.locationname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(376, 13)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel2.TabIndex = 113
        Me.RadLabel2.Text = "Age As Of"
        '
        'Fndcustomer
        '
        Me.Fndcustomer.CalculationExpression = Nothing
        Me.Fndcustomer.FieldCode = Nothing
        Me.Fndcustomer.FieldDesc = Nothing
        Me.Fndcustomer.FieldMaxLength = 0
        Me.Fndcustomer.FieldName = Nothing
        Me.Fndcustomer.isCalculatedField = False
        Me.Fndcustomer.IsSourceFromTable = False
        Me.Fndcustomer.IsSourceFromValueList = False
        Me.Fndcustomer.IsUnique = False
        Me.Fndcustomer.Location = New System.Drawing.Point(90, 60)
        Me.Fndcustomer.MendatroryField = True
        Me.Fndcustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fndcustomer.MyLinkLable1 = Me.lblCustomer
        Me.Fndcustomer.MyLinkLable2 = Nothing
        Me.Fndcustomer.MyReadOnly = False
        Me.Fndcustomer.MyShowMasterFormButton = False
        Me.Fndcustomer.Name = "Fndcustomer"
        Me.Fndcustomer.ReferenceFieldDesc = Nothing
        Me.Fndcustomer.ReferenceFieldName = Nothing
        Me.Fndcustomer.ReferenceTableName = Nothing
        Me.Fndcustomer.Size = New System.Drawing.Size(180, 19)
        Me.Fndcustomer.TabIndex = 404
        Me.Fndcustomer.Value = ""
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(9, 61)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 380
        Me.lblCustomer.Text = "Customer"
        '
        'dtpAgeof
        '
        Me.dtpAgeof.CalculationExpression = Nothing
        Me.dtpAgeof.CustomFormat = "dd/MM/yyyy"
        Me.dtpAgeof.FieldCode = Nothing
        Me.dtpAgeof.FieldDesc = Nothing
        Me.dtpAgeof.FieldMaxLength = 0
        Me.dtpAgeof.FieldName = Nothing
        Me.dtpAgeof.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAgeof.isCalculatedField = False
        Me.dtpAgeof.IsSourceFromTable = False
        Me.dtpAgeof.IsSourceFromValueList = False
        Me.dtpAgeof.IsUnique = False
        Me.dtpAgeof.Location = New System.Drawing.Point(435, 11)
        Me.dtpAgeof.MendatroryField = False
        Me.dtpAgeof.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAgeof.MyLinkLable1 = Nothing
        Me.dtpAgeof.MyLinkLable2 = Nothing
        Me.dtpAgeof.Name = "dtpAgeof"
        Me.dtpAgeof.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAgeof.ReferenceFieldDesc = Nothing
        Me.dtpAgeof.ReferenceFieldName = Nothing
        Me.dtpAgeof.ReferenceTableName = Nothing
        Me.dtpAgeof.Size = New System.Drawing.Size(96, 20)
        Me.dtpAgeof.TabIndex = 114
        Me.dtpAgeof.TabStop = False
        Me.dtpAgeof.Text = "04/08/2011"
        Me.dtpAgeof.Value = New Date(2011, 8, 4, 11, 41, 7, 406)
        '
        'ChkSecurity
        '
        Me.ChkSecurity.Location = New System.Drawing.Point(805, 60)
        Me.ChkSecurity.Name = "ChkSecurity"
        Me.ChkSecurity.Size = New System.Drawing.Size(90, 18)
        Me.ChkSecurity.TabIndex = 144
        Me.ChkSecurity.Text = "Show Security"
        Me.ChkSecurity.Visible = False
        '
        'ddlAgedRcvbl
        '
        Me.ddlAgedRcvbl.AutoCompleteDisplayMember = Nothing
        Me.ddlAgedRcvbl.AutoCompleteValueMember = Nothing
        Me.ddlAgedRcvbl.CalculationExpression = Nothing
        Me.ddlAgedRcvbl.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlAgedRcvbl.FieldCode = Nothing
        Me.ddlAgedRcvbl.FieldDesc = Nothing
        Me.ddlAgedRcvbl.FieldMaxLength = 0
        Me.ddlAgedRcvbl.FieldName = Nothing
        Me.ddlAgedRcvbl.isCalculatedField = False
        Me.ddlAgedRcvbl.IsSourceFromTable = False
        Me.ddlAgedRcvbl.IsSourceFromValueList = False
        Me.ddlAgedRcvbl.IsUnique = False
        RadListDataItem3.Text = "Aged Trial Balance By Due Date"
        RadListDataItem4.Text = "Aged Trial Balance By Document date"
        Me.ddlAgedRcvbl.Items.Add(RadListDataItem3)
        Me.ddlAgedRcvbl.Items.Add(RadListDataItem4)
        Me.ddlAgedRcvbl.Location = New System.Drawing.Point(886, 36)
        Me.ddlAgedRcvbl.MendatroryField = False
        Me.ddlAgedRcvbl.MyLinkLable1 = Nothing
        Me.ddlAgedRcvbl.MyLinkLable2 = Nothing
        Me.ddlAgedRcvbl.Name = "ddlAgedRcvbl"
        Me.ddlAgedRcvbl.ReferenceFieldDesc = Nothing
        Me.ddlAgedRcvbl.ReferenceFieldName = Nothing
        Me.ddlAgedRcvbl.ReferenceTableName = Nothing
        Me.ddlAgedRcvbl.Size = New System.Drawing.Size(211, 20)
        Me.ddlAgedRcvbl.TabIndex = 111
        Me.ddlAgedRcvbl.Visible = False
        '
        'chkType
        '
        Me.chkType.Location = New System.Drawing.Point(901, 60)
        Me.chkType.MyLinkLable1 = Nothing
        Me.chkType.MyLinkLable2 = Nothing
        Me.chkType.Name = "chkType"
        Me.chkType.Size = New System.Drawing.Size(67, 18)
        Me.chkType.TabIndex = 110
        Me.chkType.Tag1 = Nothing
        Me.chkType.Text = "Summary"
        Me.chkType.Visible = False
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(9, 37)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 376
        Me.lblLocation.Text = "Location"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(90, 36)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.lblCustomer
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(180, 19)
        Me.fndLocation.TabIndex = 403
        Me.fndLocation.Value = ""
        '
        'ChkISParentCust
        '
        Me.ChkISParentCust.Location = New System.Drawing.Point(974, 60)
        Me.ChkISParentCust.Name = "ChkISParentCust"
        Me.ChkISParentCust.Size = New System.Drawing.Size(115, 18)
        Me.ChkISParentCust.TabIndex = 143
        Me.ChkISParentCust.Text = "Is Parent Customer"
        Me.ChkISParentCust.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(592, 42)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(95, 24)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = ">>>"
        '
        'ddlCurrencyType
        '
        Me.ddlCurrencyType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCurrencyType.Location = New System.Drawing.Point(886, 13)
        Me.ddlCurrencyType.Name = "ddlCurrencyType"
        Me.ddlCurrencyType.Size = New System.Drawing.Size(211, 20)
        Me.ddlCurrencyType.TabIndex = 399
        Me.ddlCurrencyType.Visible = False
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.ERP.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(353, 11)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 401
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(805, 15)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel5.TabIndex = 398
        Me.MyLabel5.Text = "Currency "
        Me.MyLabel5.Visible = False
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(90, 11)
        Me.txtDocNo.MendatroryField = True
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblpaymentno
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(257, 20)
        Me.txtDocNo.TabIndex = 400
        Me.txtDocNo.Value = ""
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1119, 346)
        Me.gv1.TabIndex = 4
        Me.gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnPost)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 509)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1140, 51)
        Me.Panel1.TabIndex = 23
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(15, 17)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 24)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(85, 17)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 24)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(155, 17)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 24)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(1071, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'rdb
        '
        Me.rdb.Location = New System.Drawing.Point(110, 14)
        Me.rdb.Name = "rdb"
        Me.rdb.Size = New System.Drawing.Size(78, 18)
        Me.rdb.TabIndex = 105
        Me.rdb.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(9, 14)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(78, 18)
        Me.rdbSummary.TabIndex = 105
        Me.rdbSummary.Text = "Summary"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btSaveLayout, Me.btnDeleteLayour})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'btSaveLayout
        '
        Me.btSaveLayout.AccessibleDescription = "Save Layout"
        Me.btSaveLayout.AccessibleName = "Save Layout"
        Me.btSaveLayout.Name = "btSaveLayout"
        Me.btSaveLayout.Text = "Save Layout"
        Me.btSaveLayout.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'btnDeleteLayour
        '
        Me.btnDeleteLayour.AccessibleDescription = "Delete Layout"
        Me.btnDeleteLayour.AccessibleName = "Delete Layout"
        Me.btnDeleteLayour.Name = "btnDeleteLayour"
        Me.btnDeleteLayour.Text = "Delete Layout"
        Me.btnDeleteLayour.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1140, 20)
        Me.RadMenu1.TabIndex = 24
        Me.RadMenu1.Text = "RadMenu1"
        '
        'FrmCustomerOutstanding
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1140, 560)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmCustomerOutstanding"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Outstanding"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.locationname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAgeof, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlAgedRcvbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdb As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnDeleteLayour As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents chkType As common.Controls.MyCheckBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents ddlAgedRcvbl As common.Controls.MyComboBox
    Friend WithEvents dtpAgeof As common.Controls.MyDateTimePicker
    Friend WithEvents ChkISParentCust As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents ChkSecurity As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents ddlCurrencyType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents Fndcustomer As common.UserControls.txtFinder
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents CustomerName As common.Controls.MyLabel
    Friend WithEvents locationname As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
End Class

