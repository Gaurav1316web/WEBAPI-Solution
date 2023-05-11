<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmToolMaster
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.txtLastMaitainDate = New common.Controls.MyDateTimePicker
        Me.fnduom = New common.UserControls.txtFinder
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.txtuom = New common.Controls.MyTextBox
        Me.fndSupplier = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtSupplier = New common.Controls.MyTextBox
        Me.txtToolType = New common.Controls.MyTextBox
        Me.fndToolType = New common.UserControls.txtFinder
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.txtComments = New common.Controls.MyTextBox
        Me.MyLabel20 = New common.Controls.MyLabel
        Me.txtCustodian = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtSerialno = New common.Controls.MyTextBox
        Me.WIP = New common.Controls.MyLabel
        Me.txtConsumeQTY = New common.MyNumBox
        Me.MyLabel15 = New common.Controls.MyLabel
        Me.txtOnHandCost = New common.MyNumBox
        Me.MyLabel19 = New common.Controls.MyLabel
        Me.txtOnhandqty = New common.MyNumBox
        Me.MyLabel17 = New common.Controls.MyLabel
        Me.txtOriginalQty = New common.MyNumBox
        Me.MyLabel11 = New common.Controls.MyLabel
        Me.txtCost = New common.MyNumBox
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.txtReceiptDate = New common.Controls.MyDateTimePicker
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.txtReplacementDate = New common.Controls.MyDateTimePicker
        Me.txtPo = New common.Controls.MyTextBox
        Me.MyLabel18 = New common.Controls.MyLabel
        Me.txtReceipt = New common.Controls.MyTextBox
        Me.MyLabel16 = New common.Controls.MyLabel
        Me.txtReceivedBy = New common.Controls.MyTextBox
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.txtMaintainedBy = New common.Controls.MyTextBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.cboStatus = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.fndToolCode = New common.UserControls.txtNavigator
        Me.rdlblAccountsetcode = New common.Controls.MyLabel
        Me.rdlbldescription = New common.Controls.MyLabel
        Me.txtdescription = New common.Controls.MyTextBox
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastMaitainDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToolType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustodian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConsumeQTY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOnHandCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOnhandqty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOriginalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceiptDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReplacementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceivedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaintainedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(454, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
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
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel5)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtLastMaitainDate)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fnduom)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtuom)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel13)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndSupplier)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel1)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtSupplier)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtToolType)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndToolType)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel7)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtComments)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtCustodian)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtSerialno)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtConsumeQTY)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtOnHandCost)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtOnhandqty)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtOriginalQty)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtCost)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel15)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel19)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel17)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel11)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel9)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel3)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel20)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.WIP)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtReceiptDate)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtReplacementDate)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtPo)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtReceipt)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtReceivedBy)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtMaintainedBy)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel18)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel16)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel10)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel6)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel8)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel2)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.cboStatus)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel4)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndToolCode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlblAccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlbldescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtdescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(0, 0)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(454, 402)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.Location = New System.Drawing.Point(214, 198)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel5.TabIndex = 30
        Me.MyLabel5.Text = "Last Maintained"
        '
        'txtLastMaitainDate
        '
        Me.txtLastMaitainDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtLastMaitainDate.Enabled = False
        Me.txtLastMaitainDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastMaitainDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLastMaitainDate.Location = New System.Drawing.Point(305, 200)
        Me.txtLastMaitainDate.MendatroryField = False
        Me.txtLastMaitainDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastMaitainDate.MyLinkLable1 = Me.MyLabel5
        Me.txtLastMaitainDate.MyLinkLable2 = Nothing
        Me.txtLastMaitainDate.Name = "txtLastMaitainDate"
        Me.txtLastMaitainDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastMaitainDate.Size = New System.Drawing.Size(126, 18)
        Me.txtLastMaitainDate.TabIndex = 13
        Me.txtLastMaitainDate.TabStop = False
        Me.txtLastMaitainDate.Text = "13/06/2011 11:29 AM"
        Me.txtLastMaitainDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'fnduom
        '
        Me.fnduom.Location = New System.Drawing.Point(107, 106)
        Me.fnduom.MendatroryField = False
        Me.fnduom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnduom.MyLinkLable1 = Me.MyLabel13
        Me.fnduom.MyLinkLable2 = Nothing
        Me.fnduom.MyReadOnly = False
        Me.fnduom.Name = "fnduom"
        Me.fnduom.Size = New System.Drawing.Size(101, 19)
        Me.fnduom.TabIndex = 5
        Me.fnduom.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.Location = New System.Drawing.Point(26, 110)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel13.TabIndex = 38
        Me.MyLabel13.Text = "UOM"
        '
        'txtuom
        '
        Me.txtuom.Location = New System.Drawing.Point(214, 107)
        Me.txtuom.MaxLength = 50
        Me.txtuom.MendatroryField = False
        Me.txtuom.MyLinkLable1 = Nothing
        Me.txtuom.MyLinkLable2 = Nothing
        Me.txtuom.Name = "txtuom"
        Me.txtuom.Size = New System.Drawing.Size(218, 20)
        Me.txtuom.TabIndex = 40
        Me.txtuom.TabStop = False
        '
        'fndSupplier
        '
        Me.fndSupplier.Location = New System.Drawing.Point(107, 84)
        Me.fndSupplier.MendatroryField = False
        Me.fndSupplier.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSupplier.MyLinkLable1 = Me.MyLabel1
        Me.fndSupplier.MyLinkLable2 = Nothing
        Me.fndSupplier.MyReadOnly = False
        Me.fndSupplier.Name = "fndSupplier"
        Me.fndSupplier.Size = New System.Drawing.Size(101, 19)
        Me.fndSupplier.TabIndex = 4
        Me.fndSupplier.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(26, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel1.TabIndex = 39
        Me.MyLabel1.Text = "Supplier"
        '
        'txtSupplier
        '
        Me.txtSupplier.Location = New System.Drawing.Point(214, 83)
        Me.txtSupplier.MaxLength = 50
        Me.txtSupplier.MendatroryField = False
        Me.txtSupplier.MyLinkLable1 = Nothing
        Me.txtSupplier.MyLinkLable2 = Nothing
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.Size = New System.Drawing.Size(218, 20)
        Me.txtSupplier.TabIndex = 42
        Me.txtSupplier.TabStop = False
        '
        'txtToolType
        '
        Me.txtToolType.Location = New System.Drawing.Point(214, 59)
        Me.txtToolType.MaxLength = 50
        Me.txtToolType.MendatroryField = False
        Me.txtToolType.MyLinkLable1 = Nothing
        Me.txtToolType.MyLinkLable2 = Nothing
        Me.txtToolType.Name = "txtToolType"
        Me.txtToolType.Size = New System.Drawing.Size(218, 20)
        Me.txtToolType.TabIndex = 44
        Me.txtToolType.TabStop = False
        '
        'fndToolType
        '
        Me.fndToolType.Location = New System.Drawing.Point(107, 61)
        Me.fndToolType.MendatroryField = False
        Me.fndToolType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndToolType.MyLinkLable1 = Me.MyLabel7
        Me.fndToolType.MyLinkLable2 = Nothing
        Me.fndToolType.MyReadOnly = False
        Me.fndToolType.Name = "fndToolType"
        Me.fndToolType.Size = New System.Drawing.Size(100, 19)
        Me.fndToolType.TabIndex = 3
        Me.fndToolType.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(26, 66)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel7.TabIndex = 40
        Me.MyLabel7.Text = "Tool Type"
        '
        'txtComments
        '
        Me.txtComments.AutoScroll = True
        Me.txtComments.AutoSize = False
        Me.txtComments.Location = New System.Drawing.Point(107, 312)
        Me.txtComments.MaxLength = 500
        Me.txtComments.MendatroryField = False
        Me.txtComments.Multiline = True
        Me.txtComments.MyLinkLable1 = Me.MyLabel20
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(325, 85)
        Me.txtComments.TabIndex = 21
        '
        'MyLabel20
        '
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(26, 312)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel20.TabIndex = 22
        Me.MyLabel20.Text = "Comments"
        '
        'txtCustodian
        '
        Me.txtCustodian.Location = New System.Drawing.Point(107, 290)
        Me.txtCustodian.MaxLength = 50
        Me.txtCustodian.MendatroryField = False
        Me.txtCustodian.MyLinkLable1 = Me.MyLabel3
        Me.txtCustodian.MyLinkLable2 = Nothing
        Me.txtCustodian.Name = "txtCustodian"
        '
        '
        '
        Me.txtCustodian.RootElement.StretchVertically = True
        Me.txtCustodian.Size = New System.Drawing.Size(101, 20)
        Me.txtCustodian.TabIndex = 20
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(24, 291)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 23
        Me.MyLabel3.Text = "Custodian"
        '
        'txtSerialno
        '
        Me.txtSerialno.Location = New System.Drawing.Point(107, 268)
        Me.txtSerialno.MaxLength = 50
        Me.txtSerialno.MendatroryField = False
        Me.txtSerialno.MyLinkLable1 = Me.WIP
        Me.txtSerialno.MyLinkLable2 = Nothing
        Me.txtSerialno.Name = "txtSerialno"
        Me.txtSerialno.Size = New System.Drawing.Size(101, 20)
        Me.txtSerialno.TabIndex = 18
        '
        'WIP
        '
        Me.WIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WIP.Location = New System.Drawing.Point(26, 271)
        Me.WIP.Name = "WIP"
        Me.WIP.Size = New System.Drawing.Size(53, 16)
        Me.WIP.TabIndex = 24
        Me.WIP.Text = "Serial No"
        '
        'txtConsumeQTY
        '
        Me.txtConsumeQTY.BackColor = System.Drawing.Color.White
        Me.txtConsumeQTY.DecimalPlaces = 2
        Me.txtConsumeQTY.Location = New System.Drawing.Point(305, 176)
        Me.txtConsumeQTY.MendatroryField = False
        Me.txtConsumeQTY.MyLinkLable1 = Me.MyLabel15
        Me.txtConsumeQTY.MyLinkLable2 = Nothing
        Me.txtConsumeQTY.Name = "txtConsumeQTY"
        Me.txtConsumeQTY.Size = New System.Drawing.Size(126, 20)
        Me.txtConsumeQTY.TabIndex = 11
        Me.txtConsumeQTY.Text = "0"
        Me.txtConsumeQTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConsumeQTY.Value = 0
        '
        'MyLabel15
        '
        Me.MyLabel15.Location = New System.Drawing.Point(214, 174)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(79, 18)
        Me.MyLabel15.TabIndex = 33
        Me.MyLabel15.Text = "Consumed qty"
        '
        'txtOnHandCost
        '
        Me.txtOnHandCost.BackColor = System.Drawing.Color.White
        Me.txtOnHandCost.DecimalPlaces = 2
        Me.txtOnHandCost.Location = New System.Drawing.Point(305, 267)
        Me.txtOnHandCost.MendatroryField = False
        Me.txtOnHandCost.MyLinkLable1 = Me.MyLabel19
        Me.txtOnHandCost.MyLinkLable2 = Nothing
        Me.txtOnHandCost.Name = "txtOnHandCost"
        Me.txtOnHandCost.Size = New System.Drawing.Size(126, 20)
        Me.txtOnHandCost.TabIndex = 19
        Me.txtOnHandCost.Text = "0"
        Me.txtOnHandCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOnHandCost.Value = 0
        '
        'MyLabel19
        '
        Me.MyLabel19.Location = New System.Drawing.Point(214, 267)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel19.TabIndex = 25
        Me.MyLabel19.Text = "On Hand Cost"
        '
        'txtOnhandqty
        '
        Me.txtOnhandqty.BackColor = System.Drawing.Color.White
        Me.txtOnhandqty.DecimalPlaces = 2
        Me.txtOnhandqty.Location = New System.Drawing.Point(305, 244)
        Me.txtOnhandqty.MendatroryField = False
        Me.txtOnhandqty.MyLinkLable1 = Me.MyLabel17
        Me.txtOnhandqty.MyLinkLable2 = Nothing
        Me.txtOnhandqty.Name = "txtOnhandqty"
        Me.txtOnhandqty.Size = New System.Drawing.Size(126, 20)
        Me.txtOnhandqty.TabIndex = 17
        Me.txtOnhandqty.Text = "0"
        Me.txtOnhandqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOnhandqty.Value = 0
        '
        'MyLabel17
        '
        Me.MyLabel17.Location = New System.Drawing.Point(214, 242)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel17.TabIndex = 26
        Me.MyLabel17.Text = "On Hand qty"
        '
        'txtOriginalQty
        '
        Me.txtOriginalQty.BackColor = System.Drawing.Color.White
        Me.txtOriginalQty.DecimalPlaces = 2
        Me.txtOriginalQty.Location = New System.Drawing.Point(305, 153)
        Me.txtOriginalQty.MendatroryField = False
        Me.txtOriginalQty.MyLinkLable1 = Me.MyLabel11
        Me.txtOriginalQty.MyLinkLable2 = Nothing
        Me.txtOriginalQty.Name = "txtOriginalQty"
        Me.txtOriginalQty.Size = New System.Drawing.Size(126, 20)
        Me.txtOriginalQty.TabIndex = 9
        Me.txtOriginalQty.Text = "0"
        Me.txtOriginalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOriginalQty.Value = 0
        '
        'MyLabel11
        '
        Me.MyLabel11.Location = New System.Drawing.Point(214, 152)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(65, 18)
        Me.MyLabel11.TabIndex = 34
        Me.MyLabel11.Text = "Original qty"
        '
        'txtCost
        '
        Me.txtCost.BackColor = System.Drawing.Color.White
        Me.txtCost.DecimalPlaces = 2
        Me.txtCost.Location = New System.Drawing.Point(305, 130)
        Me.txtCost.MendatroryField = False
        Me.txtCost.MyLinkLable1 = Me.MyLabel9
        Me.txtCost.MyLinkLable2 = Nothing
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(126, 20)
        Me.txtCost.TabIndex = 7
        Me.txtCost.Text = "0"
        Me.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCost.Value = 0
        '
        'MyLabel9
        '
        Me.MyLabel9.Location = New System.Drawing.Point(214, 131)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel9.TabIndex = 37
        Me.MyLabel9.Text = "Cost per unit"
        '
        'txtReceiptDate
        '
        Me.txtReceiptDate.CustomFormat = "dd/MM/yyyy "
        Me.txtReceiptDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReceiptDate.Location = New System.Drawing.Point(304, 221)
        Me.txtReceiptDate.MendatroryField = False
        Me.txtReceiptDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReceiptDate.MyLinkLable1 = Me.MyLabel8
        Me.txtReceiptDate.MyLinkLable2 = Nothing
        Me.txtReceiptDate.Name = "txtReceiptDate"
        Me.txtReceiptDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReceiptDate.ShowCheckBox = True
        Me.txtReceiptDate.Size = New System.Drawing.Size(127, 18)
        Me.txtReceiptDate.TabIndex = 15
        Me.txtReceiptDate.TabStop = False
        Me.txtReceiptDate.Text = "13/06/2011 "
        Me.txtReceiptDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel8
        '
        Me.MyLabel8.Location = New System.Drawing.Point(214, 220)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel8.TabIndex = 29
        Me.MyLabel8.Text = "Receipt Date"
        '
        'txtReplacementDate
        '
        Me.txtReplacementDate.CustomFormat = "dd/MM/yyyy "
        Me.txtReplacementDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReplacementDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReplacementDate.Location = New System.Drawing.Point(107, 150)
        Me.txtReplacementDate.MendatroryField = False
        Me.txtReplacementDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReplacementDate.MyLinkLable1 = Nothing
        Me.txtReplacementDate.MyLinkLable2 = Nothing
        Me.txtReplacementDate.Name = "txtReplacementDate"
        Me.txtReplacementDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReplacementDate.ShowCheckBox = True
        Me.txtReplacementDate.Size = New System.Drawing.Size(101, 18)
        Me.txtReplacementDate.TabIndex = 8
        Me.txtReplacementDate.TabStop = False
        Me.txtReplacementDate.Text = "13/06/2011 "
        Me.txtReplacementDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtPo
        '
        Me.txtPo.Location = New System.Drawing.Point(107, 244)
        Me.txtPo.MaxLength = 50
        Me.txtPo.MendatroryField = False
        Me.txtPo.MyLinkLable1 = Me.MyLabel18
        Me.txtPo.MyLinkLable2 = Nothing
        Me.txtPo.Name = "txtPo"
        Me.txtPo.Size = New System.Drawing.Size(101, 20)
        Me.txtPo.TabIndex = 16
        '
        'MyLabel18
        '
        Me.MyLabel18.Location = New System.Drawing.Point(26, 248)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel18.TabIndex = 27
        Me.MyLabel18.Text = "PO No."
        '
        'txtReceipt
        '
        Me.txtReceipt.Location = New System.Drawing.Point(107, 220)
        Me.txtReceipt.MaxLength = 50
        Me.txtReceipt.MendatroryField = False
        Me.txtReceipt.MyLinkLable1 = Me.MyLabel16
        Me.txtReceipt.MyLinkLable2 = Nothing
        Me.txtReceipt.Name = "txtReceipt"
        Me.txtReceipt.Size = New System.Drawing.Size(101, 20)
        Me.txtReceipt.TabIndex = 14
        '
        'MyLabel16
        '
        Me.MyLabel16.Location = New System.Drawing.Point(26, 225)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel16.TabIndex = 28
        Me.MyLabel16.Text = "Receipt No."
        '
        'txtReceivedBy
        '
        Me.txtReceivedBy.Location = New System.Drawing.Point(107, 172)
        Me.txtReceivedBy.MaxLength = 50
        Me.txtReceivedBy.MendatroryField = False
        Me.txtReceivedBy.MyLinkLable1 = Me.MyLabel10
        Me.txtReceivedBy.MyLinkLable2 = Nothing
        Me.txtReceivedBy.Name = "txtReceivedBy"
        Me.txtReceivedBy.Size = New System.Drawing.Size(101, 20)
        Me.txtReceivedBy.TabIndex = 10
        '
        'MyLabel10
        '
        Me.MyLabel10.Location = New System.Drawing.Point(26, 179)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel10.TabIndex = 32
        Me.MyLabel10.Text = "Received By"
        '
        'txtMaintainedBy
        '
        Me.txtMaintainedBy.Enabled = False
        Me.txtMaintainedBy.Location = New System.Drawing.Point(107, 196)
        Me.txtMaintainedBy.MaxLength = 50
        Me.txtMaintainedBy.MendatroryField = False
        Me.txtMaintainedBy.MyLinkLable1 = Me.MyLabel6
        Me.txtMaintainedBy.MyLinkLable2 = Nothing
        Me.txtMaintainedBy.Name = "txtMaintainedBy"
        Me.txtMaintainedBy.Size = New System.Drawing.Size(100, 20)
        Me.txtMaintainedBy.TabIndex = 12
        '
        'MyLabel6
        '
        Me.MyLabel6.Location = New System.Drawing.Point(26, 202)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel6.TabIndex = 31
        Me.MyLabel6.Text = "Maintained By"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(26, 154)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel2.TabIndex = 35
        Me.MyLabel2.Text = "Replacement"
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.Color.Transparent
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.Location = New System.Drawing.Point(107, 128)
        Me.cboStatus.MendatroryField = False
        Me.cboStatus.MyLinkLable1 = Me.MyLabel4
        Me.cboStatus.MyLinkLable2 = Nothing
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(100, 18)
        Me.cboStatus.TabIndex = 6
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(26, 133)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(37, 18)
        Me.MyLabel4.TabIndex = 36
        Me.MyLabel4.Text = "Status"
        '
        'fndToolCode
        '
        Me.fndToolCode.Location = New System.Drawing.Point(107, 12)
        Me.fndToolCode.MendatroryField = True
        Me.fndToolCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndToolCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndToolCode.MyLinkLable1 = Me.rdlblAccountsetcode
        Me.fndToolCode.MyLinkLable2 = Nothing
        Me.fndToolCode.MyMaxLength = 30
        Me.fndToolCode.MyReadOnly = False
        Me.fndToolCode.Name = "fndToolCode"
        Me.fndToolCode.Size = New System.Drawing.Size(202, 21)
        Me.fndToolCode.TabIndex = 0
        Me.fndToolCode.Value = ""
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(26, 20)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(62, 16)
        Me.rdlblAccountsetcode.TabIndex = 42
        Me.rdlblAccountsetcode.Text = "Tool Code"
        '
        'rdlbldescription
        '
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(26, 43)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 41
        Me.rdlbldescription.Text = "Description"
        '
        'txtdescription
        '
        Me.txtdescription.Location = New System.Drawing.Point(107, 37)
        Me.txtdescription.MaxLength = 50
        Me.txtdescription.MendatroryField = True
        Me.txtdescription.MyLinkLable1 = Me.rdlbldescription
        Me.txtdescription.MyLinkLable2 = Nothing
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.Size = New System.Drawing.Size(324, 20)
        Me.txtdescription.TabIndex = 2
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(311, 14)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(454, 440)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 5
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(90, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(82, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(6, 5)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(366, 5)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'FrmToolMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 460)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmToolMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tool Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastMaitainDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToolType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustodian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerialno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConsumeQTY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOnHandCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOnhandqty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOriginalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceiptDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReplacementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceivedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaintainedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents fndToolCode As common.UserControls.txtNavigator
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents txtdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents WIP As common.Controls.MyLabel
    Friend WithEvents txtMaintainedBy As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtLastMaitainDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtReceiptDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtCost As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtReceivedBy As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtOriginalQty As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtSerialno As common.Controls.MyTextBox
    Friend WithEvents txtConsumeQTY As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtToolType As common.Controls.MyTextBox
    Friend WithEvents fndToolType As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents fndSupplier As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtSupplier As common.Controls.MyTextBox
    Friend WithEvents txtPo As common.Controls.MyTextBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtReceipt As common.Controls.MyTextBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtOnHandCost As common.MyNumBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtOnhandqty As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtReplacementDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fnduom As common.UserControls.txtFinder
    Friend WithEvents txtuom As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustodian As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

