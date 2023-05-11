<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeMaster
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
        Me.components = New System.ComponentModel.Container
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.dgstate = New common.UserControls.MyRadGridView
        Me.txtEmailId = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.TxtGLAccount = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtempty = New common.MyNumBox
        Me.txtshex = New common.MyNumBox
        Me.fnddesignation = New common.UserControls.txtFinder
        Me.dtpreleaving = New common.Controls.MyDateTimePicker
        Me.ddlemptype = New common.Controls.MyComboBox
        Me.ddlempstatus = New common.Controls.MyComboBox
        Me.dtpexdate = New common.Controls.MyDateTimePicker
        Me.dtpjoin = New common.Controls.MyDateTimePicker
        Me.lblempty = New common.Controls.MyLabel
        Me.lblcash = New common.Controls.MyLabel
        Me.lblpayroll = New common.Controls.MyLabel
        Me.txtpayroll = New common.Controls.MyTextBox
        Me.txtcardno = New common.Controls.MyTextBox
        Me.lblcardno = New common.Controls.MyLabel
        Me.lbljoin = New common.Controls.MyLabel
        Me.lbltype = New common.Controls.MyLabel
        Me.lblexdate = New common.Controls.MyLabel
        Me.lblstatus = New common.Controls.MyLabel
        Me.lblreleaving = New common.Controls.MyLabel
        Me.dtpdob = New common.Controls.MyDateTimePicker
        Me.lblname = New common.Controls.MyLabel
        Me.lbladd1 = New common.Controls.MyLabel
        Me.lbldes = New common.Controls.MyLabel
        Me.lbldatebirth = New common.Controls.MyLabel
        Me.txtadd1 = New common.Controls.MyTextBox
        Me.lbladd2 = New common.Controls.MyLabel
        Me.Phone = New common.Controls.MyLabel
        Me.txtadd2 = New common.Controls.MyTextBox
        Me.lblpin = New common.Controls.MyLabel
        Me.txtname = New common.Controls.MyTextBox
        Me.txtphone = New common.Controls.MyTextBox
        Me.txtpin = New common.Controls.MyTextBox
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.lblempcode = New common.Controls.MyLabel
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.ToolTipemp = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.empclose = New Telerik.WinControls.UI.RadMenuItem
        Me.empim = New Telerik.WinControls.UI.RadMenuItem
        Me.btnEmpdetails = New Telerik.WinControls.UI.RadMenuItem
        Me.btnAsmtype = New Telerik.WinControls.UI.RadMenuItem
        Me.empex = New Telerik.WinControls.UI.RadMenuItem
        Me.btnExpEmpdetails = New Telerik.WinControls.UI.RadMenuItem
        Me.btnExpASMdetails = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.menuEmployeeMaster = New Telerik.WinControls.UI.RadMenuItem
        Me.gbemp = New Telerik.WinControls.UI.RadGroupBox
        Me.fndempcode = New common.UserControls.txtNavigator
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.dgstate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgstate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtempty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtshex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpreleaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlemptype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlempstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpexdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpjoin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpayroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpayroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcardno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbljoin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblexdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblreleaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpdob, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbladd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldatebirth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbladd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Phone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtphone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbemp.SuspendLayout()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.dgstate)
        Me.RadGroupBox2.Controls.Add(Me.txtEmailId)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.TxtGLAccount)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.txtempty)
        Me.RadGroupBox2.Controls.Add(Me.txtshex)
        Me.RadGroupBox2.Controls.Add(Me.fnddesignation)
        Me.RadGroupBox2.Controls.Add(Me.dtpreleaving)
        Me.RadGroupBox2.Controls.Add(Me.ddlemptype)
        Me.RadGroupBox2.Controls.Add(Me.ddlempstatus)
        Me.RadGroupBox2.Controls.Add(Me.dtpexdate)
        Me.RadGroupBox2.Controls.Add(Me.dtpjoin)
        Me.RadGroupBox2.Controls.Add(Me.lblempty)
        Me.RadGroupBox2.Controls.Add(Me.lblcash)
        Me.RadGroupBox2.Controls.Add(Me.lblpayroll)
        Me.RadGroupBox2.Controls.Add(Me.txtpayroll)
        Me.RadGroupBox2.Controls.Add(Me.txtcardno)
        Me.RadGroupBox2.Controls.Add(Me.lblcardno)
        Me.RadGroupBox2.Controls.Add(Me.lbljoin)
        Me.RadGroupBox2.Controls.Add(Me.lbltype)
        Me.RadGroupBox2.Controls.Add(Me.lblexdate)
        Me.RadGroupBox2.Controls.Add(Me.lblstatus)
        Me.RadGroupBox2.Controls.Add(Me.lblreleaving)
        Me.RadGroupBox2.Controls.Add(Me.dtpdob)
        Me.RadGroupBox2.Controls.Add(Me.lblname)
        Me.RadGroupBox2.Controls.Add(Me.lbladd1)
        Me.RadGroupBox2.Controls.Add(Me.lbldes)
        Me.RadGroupBox2.Controls.Add(Me.lbldatebirth)
        Me.RadGroupBox2.Controls.Add(Me.txtadd1)
        Me.RadGroupBox2.Controls.Add(Me.lbladd2)
        Me.RadGroupBox2.Controls.Add(Me.Phone)
        Me.RadGroupBox2.Controls.Add(Me.txtadd2)
        Me.RadGroupBox2.Controls.Add(Me.lblpin)
        Me.RadGroupBox2.Controls.Add(Me.txtname)
        Me.RadGroupBox2.Controls.Add(Me.txtphone)
        Me.RadGroupBox2.Controls.Add(Me.txtpin)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 55)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(684, 412)
        Me.RadGroupBox2.TabIndex = 2
        '
        'dgstate
        '
        Me.dgstate.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgstate.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgstate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgstate.ForeColor = System.Drawing.Color.Black
        Me.dgstate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgstate.Location = New System.Drawing.Point(10, 254)
        '
        'dgstate
        '
        Me.dgstate.MasterTemplate.AllowAddNewRow = False
        Me.dgstate.MasterTemplate.EnableGrouping = False
        Me.dgstate.Name = "dgstate"
        Me.dgstate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgstate.Size = New System.Drawing.Size(661, 145)
        Me.dgstate.TabIndex = 18
        Me.dgstate.TabStop = False
        Me.dgstate.Text = "RadGridView1"
        '
        'txtEmailId
        '
        Me.txtEmailId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailId.Location = New System.Drawing.Point(451, 227)
        Me.txtEmailId.MaxLength = 50
        Me.txtEmailId.MendatroryField = False
        Me.txtEmailId.MyLinkLable1 = Me.MyLabel2
        Me.txtEmailId.MyLinkLable2 = Nothing
        Me.txtEmailId.Name = "txtEmailId"
        '
        '
        '
        Me.txtEmailId.RootElement.StretchVertically = True
        Me.txtEmailId.Size = New System.Drawing.Size(219, 20)
        Me.txtEmailId.TabIndex = 17
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(350, 227)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel2.TabIndex = 19
        Me.MyLabel2.Text = "E-Mail Id"
        '
        'TxtGLAccount
        '
        Me.TxtGLAccount.Location = New System.Drawing.Point(109, 229)
        Me.TxtGLAccount.MendatroryField = False
        Me.TxtGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGLAccount.MyLinkLable1 = Nothing
        Me.TxtGLAccount.MyLinkLable2 = Nothing
        Me.TxtGLAccount.MyReadOnly = False
        Me.TxtGLAccount.Name = "TxtGLAccount"
        Me.TxtGLAccount.Size = New System.Drawing.Size(222, 19)
        Me.TxtGLAccount.TabIndex = 16
        Me.TxtGLAccount.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 229)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 18
        Me.MyLabel1.Text = "GL Account"
        '
        'txtempty
        '
        Me.txtempty.BackColor = System.Drawing.Color.White
        Me.txtempty.DecimalPlaces = 2
        Me.txtempty.Location = New System.Drawing.Point(451, 201)
        Me.txtempty.MendatroryField = False
        Me.txtempty.MyLinkLable1 = Nothing
        Me.txtempty.MyLinkLable2 = Nothing
        Me.txtempty.Name = "txtempty"
        Me.txtempty.Size = New System.Drawing.Size(219, 20)
        Me.txtempty.TabIndex = 15
        Me.txtempty.Text = "0"
        Me.txtempty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtempty.Value = 0
        '
        'txtshex
        '
        Me.txtshex.BackColor = System.Drawing.Color.White
        Me.txtshex.DecimalPlaces = 2
        Me.txtshex.Location = New System.Drawing.Point(109, 201)
        Me.txtshex.MendatroryField = False
        Me.txtshex.MyLinkLable1 = Nothing
        Me.txtshex.MyLinkLable2 = Nothing
        Me.txtshex.Name = "txtshex"
        Me.txtshex.Size = New System.Drawing.Size(219, 20)
        Me.txtshex.TabIndex = 14
        Me.txtshex.Text = "0"
        Me.txtshex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtshex.Value = 0
        '
        'fnddesignation
        '
        Me.fnddesignation.Location = New System.Drawing.Point(109, 48)
        Me.fnddesignation.MendatroryField = True
        Me.fnddesignation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnddesignation.MyLinkLable1 = Nothing
        Me.fnddesignation.MyLinkLable2 = Nothing
        Me.fnddesignation.MyReadOnly = False
        Me.fnddesignation.Name = "fnddesignation"
        Me.fnddesignation.Size = New System.Drawing.Size(222, 19)
        Me.fnddesignation.TabIndex = 2
        Me.fnddesignation.Value = ""
        '
        'dtpreleaving
        '
        Me.dtpreleaving.CustomFormat = "dd/MM/yyyy"
        Me.dtpreleaving.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpreleaving.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpreleaving.Location = New System.Drawing.Point(452, 151)
        Me.dtpreleaving.MendatroryField = False
        Me.dtpreleaving.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpreleaving.MyLinkLable1 = Nothing
        Me.dtpreleaving.MyLinkLable2 = Nothing
        Me.dtpreleaving.Name = "dtpreleaving"
        Me.dtpreleaving.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpreleaving.Size = New System.Drawing.Size(108, 18)
        Me.dtpreleaving.TabIndex = 11
        Me.dtpreleaving.TabStop = False
        Me.dtpreleaving.Text = "03/05/2011"
        Me.dtpreleaving.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'ddlemptype
        '
        Me.ddlemptype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlemptype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "TDM"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Service Dealer"
        RadListDataItem2.TextWrap = True
        Me.ddlemptype.Items.Add(RadListDataItem1)
        Me.ddlemptype.Items.Add(RadListDataItem2)
        Me.ddlemptype.Location = New System.Drawing.Point(452, 73)
        Me.ddlemptype.MendatroryField = False
        Me.ddlemptype.MyLinkLable1 = Nothing
        Me.ddlemptype.MyLinkLable2 = Nothing
        Me.ddlemptype.Name = "ddlemptype"
        Me.ddlemptype.Size = New System.Drawing.Size(219, 18)
        Me.ddlemptype.TabIndex = 5
        '
        'ddlempstatus
        '
        Me.ddlempstatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlempstatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem3.Selected = True
        RadListDataItem3.Text = "Active"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Inactive"
        RadListDataItem4.TextWrap = True
        Me.ddlempstatus.Items.Add(RadListDataItem3)
        Me.ddlempstatus.Items.Add(RadListDataItem4)
        Me.ddlempstatus.Location = New System.Drawing.Point(451, 123)
        Me.ddlempstatus.MendatroryField = False
        Me.ddlempstatus.MyLinkLable1 = Nothing
        Me.ddlempstatus.MyLinkLable2 = Nothing
        Me.ddlempstatus.Name = "ddlempstatus"
        Me.ddlempstatus.Size = New System.Drawing.Size(220, 18)
        Me.ddlempstatus.TabIndex = 9
        Me.ddlempstatus.Text = "Active"
        '
        'dtpexdate
        '
        Me.dtpexdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpexdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpexdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpexdate.Location = New System.Drawing.Point(451, 97)
        Me.dtpexdate.MendatroryField = False
        Me.dtpexdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpexdate.MyLinkLable1 = Nothing
        Me.dtpexdate.MyLinkLable2 = Nothing
        Me.dtpexdate.Name = "dtpexdate"
        Me.dtpexdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpexdate.Size = New System.Drawing.Size(109, 18)
        Me.dtpexdate.TabIndex = 7
        Me.dtpexdate.TabStop = False
        Me.dtpexdate.Text = "03/05/2011"
        Me.dtpexdate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'dtpjoin
        '
        Me.dtpjoin.CustomFormat = "dd/MM/yyyy"
        Me.dtpjoin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpjoin.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpjoin.Location = New System.Drawing.Point(452, 47)
        Me.dtpjoin.MendatroryField = False
        Me.dtpjoin.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpjoin.MyLinkLable1 = Nothing
        Me.dtpjoin.MyLinkLable2 = Nothing
        Me.dtpjoin.Name = "dtpjoin"
        Me.dtpjoin.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpjoin.Size = New System.Drawing.Size(108, 18)
        Me.dtpjoin.TabIndex = 3
        Me.dtpjoin.TabStop = False
        Me.dtpjoin.Text = "03/05/2011"
        Me.dtpjoin.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblempty
        '
        Me.lblempty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempty.Location = New System.Drawing.Point(350, 203)
        Me.lblempty.Name = "lblempty"
        Me.lblempty.Size = New System.Drawing.Size(72, 16)
        Me.lblempty.TabIndex = 21
        Me.lblempty.Text = "Empty Sh/Ex"
        Me.lblempty.Visible = False
        '
        'lblcash
        '
        Me.lblcash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcash.Location = New System.Drawing.Point(11, 203)
        Me.lblcash.Name = "lblcash"
        Me.lblcash.Size = New System.Drawing.Size(66, 16)
        Me.lblcash.TabIndex = 20
        Me.lblcash.Text = "Cash Sh/Ex"
        '
        'lblpayroll
        '
        Me.lblpayroll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpayroll.Location = New System.Drawing.Point(350, 177)
        Me.lblpayroll.Name = "lblpayroll"
        Me.lblpayroll.Size = New System.Drawing.Size(71, 16)
        Me.lblpayroll.TabIndex = 23
        Me.lblpayroll.Text = "Payroll Code"
        '
        'txtpayroll
        '
        Me.txtpayroll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpayroll.Location = New System.Drawing.Point(451, 175)
        Me.txtpayroll.MaxLength = 49
        Me.txtpayroll.MendatroryField = False
        Me.txtpayroll.MyLinkLable1 = Nothing
        Me.txtpayroll.MyLinkLable2 = Nothing
        Me.txtpayroll.Name = "txtpayroll"
        Me.txtpayroll.Size = New System.Drawing.Size(220, 18)
        Me.txtpayroll.TabIndex = 13
        '
        'txtcardno
        '
        Me.txtcardno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcardno.Location = New System.Drawing.Point(451, 19)
        Me.txtcardno.MaxLength = 49
        Me.txtcardno.MendatroryField = False
        Me.txtcardno.MyLinkLable1 = Nothing
        Me.txtcardno.MyLinkLable2 = Nothing
        Me.txtcardno.Name = "txtcardno"
        Me.txtcardno.Size = New System.Drawing.Size(220, 18)
        Me.txtcardno.TabIndex = 1
        '
        'lblcardno
        '
        Me.lblcardno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcardno.Location = New System.Drawing.Point(350, 21)
        Me.lblcardno.Name = "lblcardno"
        Me.lblcardno.Size = New System.Drawing.Size(49, 16)
        Me.lblcardno.TabIndex = 35
        Me.lblcardno.Text = "Card-No"
        '
        'lbljoin
        '
        Me.lbljoin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbljoin.Location = New System.Drawing.Point(350, 45)
        Me.lbljoin.Name = "lbljoin"
        Me.lbljoin.Size = New System.Drawing.Size(69, 16)
        Me.lbljoin.TabIndex = 33
        Me.lbljoin.Text = "Joining Date"
        '
        'lbltype
        '
        Me.lbltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltype.Location = New System.Drawing.Point(350, 73)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(85, 16)
        Me.lbltype.TabIndex = 31
        Me.lbltype.Text = "Employee Type"
        '
        'lblexdate
        '
        Me.lblexdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexdate.Location = New System.Drawing.Point(350, 99)
        Me.lblexdate.Name = "lblexdate"
        Me.lblexdate.Size = New System.Drawing.Size(60, 16)
        Me.lblexdate.TabIndex = 29
        Me.lblexdate.Text = "Sh/ExDate"
        '
        'lblstatus
        '
        Me.lblstatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(350, 125)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(92, 16)
        Me.lblstatus.TabIndex = 27
        Me.lblstatus.Text = "Employee Status"
        '
        'lblreleaving
        '
        Me.lblreleaving.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreleaving.Location = New System.Drawing.Point(350, 151)
        Me.lblreleaving.Name = "lblreleaving"
        Me.lblreleaving.Size = New System.Drawing.Size(84, 16)
        Me.lblreleaving.TabIndex = 25
        Me.lblreleaving.Text = "Releaving Date"
        '
        'dtpdob
        '
        Me.dtpdob.CustomFormat = "dd/MM/yyyy"
        Me.dtpdob.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpdob.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdob.Location = New System.Drawing.Point(109, 177)
        Me.dtpdob.MendatroryField = False
        Me.dtpdob.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdob.MyLinkLable1 = Nothing
        Me.dtpdob.MyLinkLable2 = Nothing
        Me.dtpdob.Name = "dtpdob"
        Me.dtpdob.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpdob.Size = New System.Drawing.Size(110, 18)
        Me.dtpdob.TabIndex = 12
        Me.dtpdob.TabStop = False
        Me.dtpdob.Text = "03/05/2011"
        Me.dtpdob.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblname
        '
        Me.lblname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.Location = New System.Drawing.Point(9, 23)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(90, 16)
        Me.lblname.TabIndex = 34
        Me.lblname.Text = "Employee Name"
        '
        'lbladd1
        '
        Me.lbladd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd1.Location = New System.Drawing.Point(10, 75)
        Me.lbladd1.Name = "lbladd1"
        Me.lbladd1.Size = New System.Drawing.Size(54, 16)
        Me.lbladd1.TabIndex = 30
        Me.lbladd1.Text = "Address1"
        '
        'lbldes
        '
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(9, 47)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(66, 16)
        Me.lbldes.TabIndex = 32
        Me.lbldes.Text = "Designation"
        '
        'lbldatebirth
        '
        Me.lbldatebirth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldatebirth.Location = New System.Drawing.Point(11, 179)
        Me.lbldatebirth.Name = "lbldatebirth"
        Me.lbldatebirth.Size = New System.Drawing.Size(69, 16)
        Me.lbldatebirth.TabIndex = 22
        Me.lbldatebirth.Text = "Date of Birth"
        '
        'txtadd1
        '
        Me.txtadd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd1.Location = New System.Drawing.Point(109, 73)
        Me.txtadd1.MaxLength = 49
        Me.txtadd1.MendatroryField = False
        Me.txtadd1.MyLinkLable1 = Nothing
        Me.txtadd1.MyLinkLable2 = Nothing
        Me.txtadd1.Name = "txtadd1"
        Me.txtadd1.Size = New System.Drawing.Size(220, 18)
        Me.txtadd1.TabIndex = 4
        '
        'lbladd2
        '
        Me.lbladd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd2.Location = New System.Drawing.Point(10, 101)
        Me.lbladd2.Name = "lbladd2"
        Me.lbladd2.Size = New System.Drawing.Size(54, 16)
        Me.lbladd2.TabIndex = 28
        Me.lbladd2.Text = "Address2"
        '
        'Phone
        '
        Me.Phone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Phone.Location = New System.Drawing.Point(11, 153)
        Me.Phone.Name = "Phone"
        Me.Phone.Size = New System.Drawing.Size(39, 16)
        Me.Phone.TabIndex = 24
        Me.Phone.Text = "Phone"
        '
        'txtadd2
        '
        Me.txtadd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd2.Location = New System.Drawing.Point(109, 99)
        Me.txtadd2.MaxLength = 49
        Me.txtadd2.MendatroryField = False
        Me.txtadd2.MyLinkLable1 = Nothing
        Me.txtadd2.MyLinkLable2 = Nothing
        Me.txtadd2.Name = "txtadd2"
        Me.txtadd2.Size = New System.Drawing.Size(220, 18)
        Me.txtadd2.TabIndex = 6
        '
        'lblpin
        '
        Me.lblpin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpin.Location = New System.Drawing.Point(11, 127)
        Me.lblpin.Name = "lblpin"
        Me.lblpin.Size = New System.Drawing.Size(53, 16)
        Me.lblpin.TabIndex = 26
        Me.lblpin.Text = "Pin Code"
        '
        'txtname
        '
        Me.txtname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.Location = New System.Drawing.Point(109, 21)
        Me.txtname.MaxLength = 49
        Me.txtname.MendatroryField = False
        Me.txtname.MyLinkLable1 = Nothing
        Me.txtname.MyLinkLable2 = Nothing
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(220, 18)
        Me.txtname.TabIndex = 0
        '
        'txtphone
        '
        Me.txtphone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.Location = New System.Drawing.Point(109, 151)
        Me.txtphone.MaxLength = 20
        Me.txtphone.MendatroryField = False
        Me.txtphone.MyLinkLable1 = Nothing
        Me.txtphone.MyLinkLable2 = Nothing
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(220, 18)
        Me.txtphone.TabIndex = 10
        '
        'txtpin
        '
        Me.txtpin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpin.Location = New System.Drawing.Point(109, 125)
        Me.txtpin.MaxLength = 10
        Me.txtpin.MendatroryField = False
        Me.txtpin.MyLinkLable1 = Nothing
        Me.txtpin.MyLinkLable2 = Nothing
        Me.txtpin.Name = "txtpin"
        Me.txtpin.Size = New System.Drawing.Size(220, 18)
        Me.txtpin.TabIndex = 8
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(648, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(30, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'lblempcode
        '
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempcode.Location = New System.Drawing.Point(25, 12)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(87, 16)
        Me.lblempcode.TabIndex = 3
        Me.lblempcode.Text = "Employee Code"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(102, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.empclose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(741, 20)
        Me.RadMenu1.TabIndex = 55
        Me.RadMenu1.Text = "RadMenu1"
        '
        'empclose
        '
        Me.empclose.AccessibleDescription = "File"
        Me.empclose.AccessibleName = "File"
        Me.empclose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.empim, Me.empex, Me.RadMenuItem4, Me.menuEmployeeMaster})
        Me.empclose.Name = "empclose"
        Me.empclose.Text = "File"
        Me.empclose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'empim
        '
        Me.empim.AccessibleDescription = "Import"
        Me.empim.AccessibleName = "Import"
        Me.empim.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnEmpdetails, Me.btnAsmtype})
        Me.empim.Name = "empim"
        Me.empim.Text = "Import"
        Me.empim.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnEmpdetails
        '
        Me.btnEmpdetails.AccessibleDescription = "Emp details"
        Me.btnEmpdetails.AccessibleName = "Emp details"
        Me.btnEmpdetails.Name = "btnEmpdetails"
        Me.btnEmpdetails.Text = "Emp details"
        Me.btnEmpdetails.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnAsmtype
        '
        Me.btnAsmtype.AccessibleDescription = "ASM/ZM details"
        Me.btnAsmtype.AccessibleName = "ASM/ZM details"
        Me.btnAsmtype.Name = "btnAsmtype"
        Me.btnAsmtype.Text = "ASM/ZM details"
        Me.btnAsmtype.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'empex
        '
        Me.empex.AccessibleDescription = "Export"
        Me.empex.AccessibleName = "Export"
        Me.empex.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExpEmpdetails, Me.btnExpASMdetails})
        Me.empex.Name = "empex"
        Me.empex.Text = "Export"
        Me.empex.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnExpEmpdetails
        '
        Me.btnExpEmpdetails.AccessibleDescription = "Emp Details"
        Me.btnExpEmpdetails.AccessibleName = "Emp Details"
        Me.btnExpEmpdetails.Name = "btnExpEmpdetails"
        Me.btnExpEmpdetails.Text = "Emp Details"
        Me.btnExpEmpdetails.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnExpASMdetails
        '
        Me.btnExpASMdetails.AccessibleDescription = "ASM/ZM details"
        Me.btnExpASMdetails.AccessibleName = "ASM/ZM details"
        Me.btnExpASMdetails.Name = "btnExpASMdetails"
        Me.btnExpASMdetails.Text = "ASM/ZM details"
        Me.btnExpASMdetails.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Close"
        Me.RadMenuItem4.AccessibleName = "Close"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Close"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuEmployeeMaster
        '
        Me.menuEmployeeMaster.AccessibleDescription = "RadMenuItem1"
        Me.menuEmployeeMaster.AccessibleName = "RadMenuItem1"
        Me.menuEmployeeMaster.Name = "menuEmployeeMaster"
        Me.menuEmployeeMaster.Text = "Print"
        Me.menuEmployeeMaster.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'gbemp
        '
        Me.gbemp.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbemp.Controls.Add(Me.fndempcode)
        Me.gbemp.Controls.Add(Me.lblempcode)
        Me.gbemp.Controls.Add(Me.btnnew)
        Me.gbemp.Controls.Add(Me.RadGroupBox2)
        Me.gbemp.HeaderText = ""
        Me.gbemp.Location = New System.Drawing.Point(19, 13)
        Me.gbemp.Name = "gbemp"
        Me.gbemp.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbemp.Size = New System.Drawing.Size(705, 480)
        Me.gbemp.TabIndex = 0
        '
        'fndempcode
        '
        Me.fndempcode.Location = New System.Drawing.Point(118, 12)
        Me.fndempcode.MendatroryField = True
        Me.fndempcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndempcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndempcode.MyLinkLable1 = Nothing
        Me.fndempcode.MyLinkLable2 = Nothing
        Me.fndempcode.MyMaxLength = 12
        Me.fndempcode.MyReadOnly = False
        Me.fndempcode.Name = "fndempcode"
        Me.fndempcode.Size = New System.Drawing.Size(202, 21)
        Me.fndempcode.TabIndex = 0
        Me.fndempcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(323, 13)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbemp)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(741, 541)
        Me.SplitContainer1.SplitterDistance = 496
        Me.SplitContainer1.TabIndex = 0
        '
        'frmEmployeeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(741, 561)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmEmployeeMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Employee Master"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.dgstate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgstate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtempty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtshex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpreleaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlemptype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlempstatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpexdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpjoin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpayroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpayroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcardno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcardno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbljoin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblexdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblreleaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpdob, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbladd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldatebirth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbladd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Phone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtphone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbemp.ResumeLayout(False)
        Me.gbemp.PerformLayout()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ddlemptype As common.Controls.MyComboBox
    Friend WithEvents ddlempstatus As common.Controls.MyComboBox
    Friend WithEvents dtpexdate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpjoin As common.Controls.MyDateTimePicker
    Friend WithEvents txtpayroll As common.Controls.MyTextBox
    Friend WithEvents txtcardno As common.Controls.MyTextBox
    Friend WithEvents dtpdob As common.Controls.MyDateTimePicker
    Friend WithEvents txtadd1 As common.Controls.MyTextBox
    Friend WithEvents txtadd2 As common.Controls.MyTextBox
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents txtphone As common.Controls.MyTextBox
    Friend WithEvents txtpin As common.Controls.MyTextBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTipemp As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents empclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents empim As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents empex As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gbemp As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents menuEmployeeMaster As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents dtpreleaving As common.Controls.MyDateTimePicker
    Friend WithEvents lblempty As common.Controls.MyLabel
    Friend WithEvents lblcash As common.Controls.MyLabel
    Friend WithEvents lblpayroll As common.Controls.MyLabel
    Friend WithEvents lblcardno As common.Controls.MyLabel
    Friend WithEvents lbljoin As common.Controls.MyLabel
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents lblexdate As common.Controls.MyLabel
    Friend WithEvents lblstatus As common.Controls.MyLabel
    Friend WithEvents lblreleaving As common.Controls.MyLabel
    Friend WithEvents lblname As common.Controls.MyLabel
    Friend WithEvents lbladd1 As common.Controls.MyLabel
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents lbldatebirth As common.Controls.MyLabel
    Friend WithEvents lbladd2 As common.Controls.MyLabel
    Friend WithEvents Phone As common.Controls.MyLabel
    Friend WithEvents lblpin As common.Controls.MyLabel
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents fndempcode As common.UserControls.txtNavigator
    Friend WithEvents fnddesignation As common.UserControls.txtFinder
    Friend WithEvents txtempty As common.MyNumBox
    Friend WithEvents txtshex As common.MyNumBox
    Friend WithEvents TxtGLAccount As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtEmailId As common.Controls.MyTextBox
    Friend WithEvents dgstate As common.UserControls.MyRadGridView
    Friend WithEvents btnEmpdetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnAsmtype As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExpEmpdetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExpASMdetails As Telerik.WinControls.UI.RadMenuItem
End Class

