<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettings
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
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkReceive = New Telerik.WinControls.UI.RadCheckBox
        Me.txtAutoClose = New common.MyNumBox
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.chk6decimal = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.chkActivate = New Telerik.WinControls.UI.RadCheckBox
        Me.ChkAutoclose = New Telerik.WinControls.UI.RadCheckBox
        Me.rdgrpbxgeneralledgeraccounts = New Telerik.WinControls.UI.RadGroupBox
        Me.txtReceipt = New common.MyNumBox
        Me.txtIssue = New common.MyNumBox
        Me.ddlReceipt = New Telerik.WinControls.UI.RadDropDownList
        Me.ddlissue = New Telerik.WinControls.UI.RadDropDownList
        Me.fndLocation = New common.UserControls.txtFinder
        Me.rdlblWriteoffs = New common.Controls.MyLabel
        Me.fndProduction = New common.UserControls.txtFinder
        Me.rdlblAdvance = New common.Controls.MyLabel
        Me.txtCost = New common.Controls.MyTextBox
        Me.txtLocation = New common.Controls.MyTextBox
        Me.lblcontainer = New common.Controls.MyLabel
        Me.txtProduction = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.rdlblrecieptdiscount = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.WIP = New common.Controls.MyLabel
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.txtmixing_charge = New common.MyNumBox
        Me.MyLabel8 = New common.Controls.MyLabel
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkReceive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAutoClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk6decimal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAutoclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxgeneralledgeraccounts.SuspendLayout()
        CType(Me.txtReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlissue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblWriteoffs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcontainer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblrecieptdiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtmixing_charge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.RadGroupBox1)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdgrpbxgeneralledgeraccounts)
        Me.rdgpbxcustomeraccountset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = "Processing"
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(0, 0)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(577, 291)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        Me.rdgpbxcustomeraccountset.Text = "Processing"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkReceive)
        Me.RadGroupBox1.Controls.Add(Me.txtAutoClose)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.chk6decimal)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.chkActivate)
        Me.RadGroupBox1.Controls.Add(Me.ChkAutoclose)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 23)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(552, 102)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkReceive
        '
        Me.chkReceive.Location = New System.Drawing.Point(5, 81)
        Me.chkReceive.Name = "chkReceive"
        Me.chkReceive.Size = New System.Drawing.Size(181, 18)
        Me.chkReceive.TabIndex = 4
        Me.chkReceive.Text = "Allow Receive without Insurance"
        '
        'txtAutoClose
        '
        Me.txtAutoClose.BackColor = System.Drawing.Color.White
        Me.txtAutoClose.DecimalPlaces = 0
        Me.txtAutoClose.Location = New System.Drawing.Point(417, 7)
        Me.txtAutoClose.MendatroryField = False
        Me.txtAutoClose.MyLinkLable1 = Nothing
        Me.txtAutoClose.MyLinkLable2 = Nothing
        Me.txtAutoClose.Name = "txtAutoClose"
        Me.txtAutoClose.Size = New System.Drawing.Size(100, 20)
        Me.txtAutoClose.TabIndex = 1
        Me.txtAutoClose.Text = "0"
        Me.txtAutoClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAutoClose.Value = 0
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(517, 9)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(16, 16)
        Me.MyLabel7.TabIndex = 5
        Me.MyLabel7.Text = "%"
        '
        'chk6decimal
        '
        Me.chk6decimal.Location = New System.Drawing.Point(5, 57)
        Me.chk6decimal.Name = "chk6decimal"
        Me.chk6decimal.Size = New System.Drawing.Size(198, 18)
        Me.chk6decimal.TabIndex = 3
        Me.chk6decimal.Text = "Allow 6 Decimal Standard Unit Cost"
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(299, 9)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(116, 16)
        Me.MyLabel6.TabIndex = 6
        Me.MyLabel6.Text = "Auto-Close Tolerance"
        '
        'chkActivate
        '
        Me.chkActivate.Location = New System.Drawing.Point(5, 33)
        Me.chkActivate.Name = "chkActivate"
        Me.chkActivate.Size = New System.Drawing.Size(284, 18)
        Me.chkActivate.TabIndex = 2
        Me.chkActivate.Text = "Activate Manufacturing Order instead of Batch Order"
        '
        'ChkAutoclose
        '
        Me.ChkAutoclose.Location = New System.Drawing.Point(5, 9)
        Me.ChkAutoclose.Name = "ChkAutoclose"
        Me.ChkAutoclose.Size = New System.Drawing.Size(208, 18)
        Me.ChkAutoclose.TabIndex = 0
        Me.ChkAutoclose.Text = "Allow auto-close  MO during Receipt "
        '
        'rdgrpbxgeneralledgeraccounts
        '
        Me.rdgrpbxgeneralledgeraccounts.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtmixing_charge)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel8)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtReceipt)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtIssue)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.ddlReceipt)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.ddlissue)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndLocation)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndProduction)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtCost)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtLocation)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblcontainer)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtProduction)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblWriteoffs)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblAdvance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel2)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel5)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblrecieptdiscount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel4)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel3)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.WIP)
        Me.rdgrpbxgeneralledgeraccounts.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxgeneralledgeraccounts.HeaderText = ""
        Me.rdgrpbxgeneralledgeraccounts.Location = New System.Drawing.Point(10, 128)
        Me.rdgrpbxgeneralledgeraccounts.Name = "rdgrpbxgeneralledgeraccounts"
        Me.rdgrpbxgeneralledgeraccounts.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxgeneralledgeraccounts.Size = New System.Drawing.Size(554, 155)
        Me.rdgrpbxgeneralledgeraccounts.TabIndex = 1
        '
        'txtReceipt
        '
        Me.txtReceipt.BackColor = System.Drawing.Color.White
        Me.txtReceipt.DecimalPlaces = 0
        Me.txtReceipt.Location = New System.Drawing.Point(419, 48)
        Me.txtReceipt.MendatroryField = False
        Me.txtReceipt.MyLinkLable1 = Nothing
        Me.txtReceipt.MyLinkLable2 = Nothing
        Me.txtReceipt.Name = "txtReceipt"
        Me.txtReceipt.Size = New System.Drawing.Size(100, 20)
        Me.txtReceipt.TabIndex = 3
        Me.txtReceipt.Text = "0"
        Me.txtReceipt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtReceipt.Value = 0
        '
        'txtIssue
        '
        Me.txtIssue.BackColor = System.Drawing.Color.White
        Me.txtIssue.DecimalPlaces = 0
        Me.txtIssue.Location = New System.Drawing.Point(419, 23)
        Me.txtIssue.MendatroryField = False
        Me.txtIssue.MyLinkLable1 = Nothing
        Me.txtIssue.MyLinkLable2 = Nothing
        Me.txtIssue.Name = "txtIssue"
        Me.txtIssue.Size = New System.Drawing.Size(100, 20)
        Me.txtIssue.TabIndex = 1
        Me.txtIssue.Text = "0"
        Me.txtIssue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIssue.Value = 0
        '
        'ddlReceipt
        '
        Me.ddlReceipt.AllowShowFocusCues = False
        Me.ddlReceipt.AutoCompleteDisplayMember = Nothing
        Me.ddlReceipt.AutoCompleteValueMember = Nothing
        Me.ddlReceipt.Location = New System.Drawing.Point(159, 47)
        Me.ddlReceipt.Name = "ddlReceipt"
        Me.ddlReceipt.Size = New System.Drawing.Size(106, 20)
        Me.ddlReceipt.TabIndex = 2
        '
        'ddlissue
        '
        Me.ddlissue.AllowShowFocusCues = False
        Me.ddlissue.AutoCompleteDisplayMember = Nothing
        Me.ddlissue.AutoCompleteValueMember = Nothing
        Me.ddlissue.Location = New System.Drawing.Point(159, 21)
        Me.ddlissue.Name = "ddlissue"
        Me.ddlissue.Size = New System.Drawing.Size(106, 20)
        Me.ddlissue.TabIndex = 0
        '
        'fndLocation
        '
        Me.fndLocation.Location = New System.Drawing.Point(159, 99)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.rdlblWriteoffs
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.Size = New System.Drawing.Size(143, 19)
        Me.fndLocation.TabIndex = 5
        Me.fndLocation.Value = ""
        '
        'rdlblWriteoffs
        '
        Me.rdlblWriteoffs.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblWriteoffs.Location = New System.Drawing.Point(15, 99)
        Me.rdlblWriteoffs.Name = "rdlblWriteoffs"
        Me.rdlblWriteoffs.Size = New System.Drawing.Size(49, 16)
        Me.rdlblWriteoffs.TabIndex = 8
        Me.rdlblWriteoffs.Text = "Location"
        '
        'fndProduction
        '
        Me.fndProduction.Location = New System.Drawing.Point(159, 74)
        Me.fndProduction.MendatroryField = False
        Me.fndProduction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProduction.MyLinkLable1 = Me.rdlblAdvance
        Me.fndProduction.MyLinkLable2 = Nothing
        Me.fndProduction.MyReadOnly = False
        Me.fndProduction.MyShowMasterFormButton = False
        Me.fndProduction.Name = "fndProduction"
        Me.fndProduction.Size = New System.Drawing.Size(143, 19)
        Me.fndProduction.TabIndex = 4
        Me.fndProduction.Value = ""
        '
        'rdlblAdvance
        '
        Me.rdlblAdvance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblAdvance.Location = New System.Drawing.Point(13, 74)
        Me.rdlblAdvance.Name = "rdlblAdvance"
        Me.rdlblAdvance.Size = New System.Drawing.Size(87, 16)
        Me.rdlblAdvance.TabIndex = 9
        Me.rdlblAdvance.Text = "Production Area"
        '
        'txtCost
        '
        Me.txtCost.Location = New System.Drawing.Point(159, 124)
        Me.txtCost.MendatroryField = False
        Me.txtCost.MyLinkLable1 = Nothing
        Me.txtCost.MyLinkLable2 = Nothing
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(143, 20)
        Me.txtCost.TabIndex = 6
        Me.txtCost.Text = "Posting"
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(312, 98)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyLinkLable1 = Me.rdlblWriteoffs
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(206, 20)
        Me.txtLocation.TabIndex = 7
        Me.txtLocation.TabStop = False
        '
        'lblcontainer
        '
        Me.lblcontainer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcontainer.Location = New System.Drawing.Point(15, 125)
        Me.lblcontainer.Name = "lblcontainer"
        Me.lblcontainer.Size = New System.Drawing.Size(114, 16)
        Me.lblcontainer.TabIndex = 7
        Me.lblcontainer.Text = "I/C Cost Items During"
        '
        'txtProduction
        '
        Me.txtProduction.Location = New System.Drawing.Point(312, 74)
        Me.txtProduction.MendatroryField = False
        Me.txtProduction.MyLinkLable1 = Me.rdlblAdvance
        Me.txtProduction.MyLinkLable2 = Nothing
        Me.txtProduction.Name = "txtProduction"
        Me.txtProduction.Size = New System.Drawing.Size(207, 20)
        Me.txtProduction.TabIndex = 5
        Me.txtProduction.TabStop = False
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(314, 49)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(98, 16)
        Me.MyLabel2.TabIndex = 12
        Me.MyLabel2.Text = "Receipt Tolerance"
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(519, 50)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(16, 16)
        Me.MyLabel5.TabIndex = 14
        Me.MyLabel5.Text = "%"
        '
        'rdlblrecieptdiscount
        '
        Me.rdlblrecieptdiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblrecieptdiscount.Location = New System.Drawing.Point(13, 49)
        Me.rdlblrecieptdiscount.Name = "rdlblrecieptdiscount"
        Me.rdlblrecieptdiscount.Size = New System.Drawing.Size(139, 16)
        Me.rdlblrecieptdiscount.TabIndex = 10
        Me.rdlblrecieptdiscount.Text = "Exceed Receipt Tolerance"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(519, 25)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(16, 16)
        Me.MyLabel4.TabIndex = 15
        Me.MyLabel4.Text = "%"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(312, 23)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel3.TabIndex = 13
        Me.MyLabel3.Text = " Issue Tolerance"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(312, 25)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel1.TabIndex = 11
        Me.MyLabel1.Text = " Issue Tolerance"
        '
        'WIP
        '
        Me.WIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WIP.Location = New System.Drawing.Point(13, 23)
        Me.WIP.Name = "WIP"
        Me.WIP.Size = New System.Drawing.Size(128, 16)
        Me.WIP.TabIndex = 11
        Me.WIP.Text = "Exceed Issue Tolerance"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(12, 6)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(477, 7)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(577, 329)
        Me.SplitContainer1.SplitterDistance = 291
        Me.SplitContainer1.TabIndex = 12
        '
        'txtmixing_charge
        '
        Me.txtmixing_charge.BackColor = System.Drawing.Color.White
        Me.txtmixing_charge.DecimalPlaces = 0
        Me.txtmixing_charge.Location = New System.Drawing.Point(421, 125)
        Me.txtmixing_charge.MendatroryField = False
        Me.txtmixing_charge.MyLinkLable1 = Me.MyLabel8
        Me.txtmixing_charge.MyLinkLable2 = Nothing
        Me.txtmixing_charge.Name = "txtmixing_charge"
        Me.txtmixing_charge.Size = New System.Drawing.Size(100, 20)
        Me.txtmixing_charge.TabIndex = 16
        Me.txtmixing_charge.Text = "0"
        Me.txtmixing_charge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtmixing_charge.Value = 0
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(314, 126)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel8.TabIndex = 18
        Me.MyLabel8.Text = "Mixing Charge"
        '
        'FrmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 329)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Settings"
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkReceive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAutoClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk6decimal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAutoclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxgeneralledgeraccounts.ResumeLayout(False)
        Me.rdgrpbxgeneralledgeraccounts.PerformLayout()
        CType(Me.txtReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlissue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblWriteoffs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcontainer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblrecieptdiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtmixing_charge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkAutoclose As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdgrpbxgeneralledgeraccounts As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblcontainer As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents rdlblWriteoffs As common.Controls.MyLabel
    Friend WithEvents fndProduction As common.UserControls.txtFinder
    Friend WithEvents rdlblAdvance As common.Controls.MyLabel
    Friend WithEvents rdlblrecieptdiscount As common.Controls.MyLabel
    Friend WithEvents WIP As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.Controls.MyTextBox
    Friend WithEvents txtProduction As common.Controls.MyTextBox
    Friend WithEvents chkReceive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chk6decimal As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkActivate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtIssue As common.MyNumBox
    Friend WithEvents ddlReceipt As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents ddlissue As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtAutoClose As common.MyNumBox
    Friend WithEvents txtReceipt As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtCost As common.Controls.MyTextBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtmixing_charge As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
End Class

