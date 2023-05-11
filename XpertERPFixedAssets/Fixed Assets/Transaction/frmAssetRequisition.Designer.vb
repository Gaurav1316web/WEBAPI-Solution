Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetRequisition
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.ddlMoveType = New common.Controls.MyComboBox
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.lblRemarks = New common.Controls.MyLabel
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblAssetReqCode = New common.Controls.MyLabel
        Me.dtpReqDate = New common.Controls.MyDateTimePicker
        Me.lblReqDate = New common.Controls.MyLabel
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.lblMoveType = New common.Controls.MyLabel
        Me.txtCustname = New common.Controls.MyTextBox
        Me.fndcustomerCode = New common.UserControls.txtFinder
        Me.lblCustomerCode = New common.Controls.MyLabel
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.rdgrpbxVisimaster = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtCustTelephone = New common.Controls.MyTextBox
        Me.lblCustTelephone = New common.Controls.MyLabel
        Me.txtCustContactPerson = New common.Controls.MyTextBox
        Me.lblCustContactPerson = New common.Controls.MyLabel
        Me.txtCustChannel = New common.Controls.MyTextBox
        Me.lblCustChannel = New common.Controls.MyLabel
        Me.lblCustTown = New common.Controls.MyLabel
        Me.txtCustTown = New common.Controls.MyTextBox
        Me.txtCustAddress = New common.Controls.MyTextBox
        Me.lblCustAddress = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtLocTelephone = New common.Controls.MyTextBox
        Me.lblTelephone = New common.Controls.MyLabel
        Me.txtLocContactPerson = New common.Controls.MyTextBox
        Me.lblContactPerson = New common.Controls.MyLabel
        Me.txtLocChannel = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtLocTown = New common.Controls.MyTextBox
        Me.txtLocationDesc = New common.Controls.MyTextBox
        Me.txtLocationAddress = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.fndLocationCode = New common.UserControls.txtFinder
        Me.lblLocationCode = New common.Controls.MyLabel
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ddlMoveType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetReqCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpReqDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReqDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxVisimaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxVisimaster.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txtCustTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustContactPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustContactPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustChannel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustChannel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustTown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustTown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtLocTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocContactPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblContactPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocChannel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocTown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.ddlMoveType)
        Me.RadGroupBox1.Controls.Add(Me.txtRemarks)
        Me.RadGroupBox1.Controls.Add(Me.lblRemarks)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.dtpReqDate)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblReqDate)
        Me.RadGroupBox1.Controls.Add(Me.lblMoveType)
        Me.RadGroupBox1.Controls.Add(Me.lblAssetReqCode)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(816, 75)
        Me.RadGroupBox1.TabIndex = 0
        '
        'ddlMoveType
        '
        Me.ddlMoveType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlMoveType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlMoveType.Location = New System.Drawing.Point(108, 28)
        Me.ddlMoveType.MendatroryField = False
        Me.ddlMoveType.MyLinkLable1 = Nothing
        Me.ddlMoveType.MyLinkLable2 = Nothing
        Me.ddlMoveType.Name = "ddlMoveType"
        Me.ddlMoveType.Size = New System.Drawing.Size(190, 18)
        Me.ddlMoveType.TabIndex = 3
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(107, 48)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.lblRemarks
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(438, 18)
        Me.txtRemarks.TabIndex = 4
        '
        'lblRemarks
        '
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(8, 50)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 11
        Me.lblRemarks.Text = "Remarks"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(108, 5)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAssetReqCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(190, 21)
        Me.txtCode.TabIndex = 1
        Me.txtCode.Value = ""
        '
        'lblAssetReqCode
        '
        Me.lblAssetReqCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetReqCode.Location = New System.Drawing.Point(7, 9)
        Me.lblAssetReqCode.Name = "lblAssetReqCode"
        Me.lblAssetReqCode.Size = New System.Drawing.Size(92, 16)
        Me.lblAssetReqCode.TabIndex = 9
        Me.lblAssetReqCode.Text = "Requisition Code"
        '
        'dtpReqDate
        '
        Me.dtpReqDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpReqDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReqDate.Location = New System.Drawing.Point(421, 5)
        Me.dtpReqDate.MendatroryField = False
        Me.dtpReqDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpReqDate.MyLinkLable1 = Me.lblReqDate
        Me.dtpReqDate.MyLinkLable2 = Nothing
        Me.dtpReqDate.Name = "dtpReqDate"
        Me.dtpReqDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpReqDate.Size = New System.Drawing.Size(124, 18)
        Me.dtpReqDate.TabIndex = 2
        Me.dtpReqDate.TabStop = False
        Me.dtpReqDate.Text = "02/06/2011"
        Me.dtpReqDate.Value = New Date(2011, 6, 2, 0, 0, 0, 0)
        '
        'lblReqDate
        '
        Me.lblReqDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReqDate.Location = New System.Drawing.Point(324, 7)
        Me.lblReqDate.Name = "lblReqDate"
        Me.lblReqDate.Size = New System.Drawing.Size(89, 16)
        Me.lblReqDate.TabIndex = 13
        Me.lblReqDate.Text = "Requisition Date"
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(299, 6)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(16, 18)
        Me.rdbtnnew.TabIndex = 0
        '
        'lblMoveType
        '
        Me.lblMoveType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoveType.Location = New System.Drawing.Point(7, 31)
        Me.lblMoveType.Name = "lblMoveType"
        Me.lblMoveType.Size = New System.Drawing.Size(62, 16)
        Me.lblMoveType.TabIndex = 10
        Me.lblMoveType.Text = "Move Type"
        '
        'txtCustname
        '
        Me.txtCustname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustname.Location = New System.Drawing.Point(230, 17)
        Me.txtCustname.MaxLength = 12
        Me.txtCustname.MendatroryField = False
        Me.txtCustname.MyLinkLable1 = Nothing
        Me.txtCustname.MyLinkLable2 = Nothing
        Me.txtCustname.Name = "txtCustname"
        Me.txtCustname.Size = New System.Drawing.Size(161, 18)
        Me.txtCustname.TabIndex = 1
        '
        'fndcustomerCode
        '
        Me.fndcustomerCode.Location = New System.Drawing.Point(89, 17)
        Me.fndcustomerCode.MendatroryField = False
        Me.fndcustomerCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcustomerCode.MyLinkLable1 = Me.lblCustomerCode
        Me.fndcustomerCode.MyLinkLable2 = Nothing
        Me.fndcustomerCode.MyReadOnly = False
        Me.fndcustomerCode.Name = "fndcustomerCode"
        Me.fndcustomerCode.Size = New System.Drawing.Size(139, 19)
        Me.fndcustomerCode.TabIndex = 0
        Me.fndcustomerCode.Value = ""
        '
        'lblCustomerCode
        '
        Me.lblCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerCode.Location = New System.Drawing.Point(5, 18)
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(68, 16)
        Me.lblCustomerCode.TabIndex = 12
        Me.lblCustomerCode.Text = "Customer Id"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(768, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'rdgrpbxVisimaster
        '
        Me.rdgrpbxVisimaster.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxVisimaster.Controls.Add(Me.RadGroupBox3)
        Me.rdgrpbxVisimaster.Controls.Add(Me.RadGroupBox2)
        Me.rdgrpbxVisimaster.Controls.Add(Me.RadGroupBox4)
        Me.rdgrpbxVisimaster.Controls.Add(Me.RadGroupBox1)
        Me.rdgrpbxVisimaster.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxVisimaster.HeaderText = ""
        Me.rdgrpbxVisimaster.Location = New System.Drawing.Point(3, 3)
        Me.rdgrpbxVisimaster.Name = "rdgrpbxVisimaster"
        Me.rdgrpbxVisimaster.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxVisimaster.Size = New System.Drawing.Size(831, 466)
        Me.rdgrpbxVisimaster.TabIndex = 0
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txtCustTelephone)
        Me.RadGroupBox3.Controls.Add(Me.lblCustTelephone)
        Me.RadGroupBox3.Controls.Add(Me.txtCustContactPerson)
        Me.RadGroupBox3.Controls.Add(Me.lblCustContactPerson)
        Me.RadGroupBox3.Controls.Add(Me.txtCustChannel)
        Me.RadGroupBox3.Controls.Add(Me.lblCustChannel)
        Me.RadGroupBox3.Controls.Add(Me.lblCustTown)
        Me.RadGroupBox3.Controls.Add(Me.txtCustTown)
        Me.RadGroupBox3.Controls.Add(Me.txtCustAddress)
        Me.RadGroupBox3.Controls.Add(Me.lblCustAddress)
        Me.RadGroupBox3.Controls.Add(Me.txtCustname)
        Me.RadGroupBox3.Controls.Add(Me.fndcustomerCode)
        Me.RadGroupBox3.Controls.Add(Me.lblCustomerCode)
        Me.RadGroupBox3.HeaderText = "Customer Details"
        Me.RadGroupBox3.Location = New System.Drawing.Point(427, 86)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(395, 120)
        Me.RadGroupBox3.TabIndex = 2
        Me.RadGroupBox3.Text = "Customer Details"
        '
        'txtCustTelephone
        '
        Me.txtCustTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustTelephone.Location = New System.Drawing.Point(89, 95)
        Me.txtCustTelephone.MaxLength = 50
        Me.txtCustTelephone.MendatroryField = False
        Me.txtCustTelephone.MyLinkLable1 = Me.lblCustTelephone
        Me.txtCustTelephone.MyLinkLable2 = Nothing
        Me.txtCustTelephone.Name = "txtCustTelephone"
        Me.txtCustTelephone.Size = New System.Drawing.Size(302, 18)
        Me.txtCustTelephone.TabIndex = 7
        '
        'lblCustTelephone
        '
        Me.lblCustTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustTelephone.Location = New System.Drawing.Point(5, 98)
        Me.lblCustTelephone.Name = "lblCustTelephone"
        Me.lblCustTelephone.Size = New System.Drawing.Size(60, 16)
        Me.lblCustTelephone.TabIndex = 35
        Me.lblCustTelephone.Text = "Telephone"
        '
        'txtCustContactPerson
        '
        Me.txtCustContactPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustContactPerson.Location = New System.Drawing.Point(89, 76)
        Me.txtCustContactPerson.MaxLength = 50
        Me.txtCustContactPerson.MendatroryField = False
        Me.txtCustContactPerson.MyLinkLable1 = Me.lblCustContactPerson
        Me.txtCustContactPerson.MyLinkLable2 = Nothing
        Me.txtCustContactPerson.Name = "txtCustContactPerson"
        Me.txtCustContactPerson.Size = New System.Drawing.Size(302, 18)
        Me.txtCustContactPerson.TabIndex = 5
        '
        'lblCustContactPerson
        '
        Me.lblCustContactPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustContactPerson.Location = New System.Drawing.Point(5, 79)
        Me.lblCustContactPerson.Name = "lblCustContactPerson"
        Me.lblCustContactPerson.Size = New System.Drawing.Size(84, 16)
        Me.lblCustContactPerson.TabIndex = 33
        Me.lblCustContactPerson.Text = "Contact Person"
        '
        'txtCustChannel
        '
        Me.txtCustChannel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustChannel.Location = New System.Drawing.Point(268, 57)
        Me.txtCustChannel.MaxLength = 12
        Me.txtCustChannel.MendatroryField = False
        Me.txtCustChannel.MyLinkLable1 = Nothing
        Me.txtCustChannel.MyLinkLable2 = Nothing
        Me.txtCustChannel.Name = "txtCustChannel"
        Me.txtCustChannel.Size = New System.Drawing.Size(124, 18)
        Me.txtCustChannel.TabIndex = 6
        '
        'lblCustChannel
        '
        Me.lblCustChannel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustChannel.Location = New System.Drawing.Point(218, 59)
        Me.lblCustChannel.Name = "lblCustChannel"
        Me.lblCustChannel.Size = New System.Drawing.Size(48, 16)
        Me.lblCustChannel.TabIndex = 30
        Me.lblCustChannel.Text = "Channel"
        '
        'lblCustTown
        '
        Me.lblCustTown.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustTown.Location = New System.Drawing.Point(6, 62)
        Me.lblCustTown.Name = "lblCustTown"
        Me.lblCustTown.Size = New System.Drawing.Size(34, 16)
        Me.lblCustTown.TabIndex = 29
        Me.lblCustTown.Text = "Town"
        '
        'txtCustTown
        '
        Me.txtCustTown.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustTown.Location = New System.Drawing.Point(89, 57)
        Me.txtCustTown.MaxLength = 12
        Me.txtCustTown.MendatroryField = False
        Me.txtCustTown.MyLinkLable1 = Nothing
        Me.txtCustTown.MyLinkLable2 = Nothing
        Me.txtCustTown.Name = "txtCustTown"
        Me.txtCustTown.Size = New System.Drawing.Size(124, 18)
        Me.txtCustTown.TabIndex = 4
        '
        'txtCustAddress
        '
        Me.txtCustAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAddress.Location = New System.Drawing.Point(90, 37)
        Me.txtCustAddress.MaxLength = 50
        Me.txtCustAddress.MendatroryField = False
        Me.txtCustAddress.MyLinkLable1 = Me.lblCustAddress
        Me.txtCustAddress.MyLinkLable2 = Nothing
        Me.txtCustAddress.Name = "txtCustAddress"
        Me.txtCustAddress.Size = New System.Drawing.Size(302, 18)
        Me.txtCustAddress.TabIndex = 2
        '
        'lblCustAddress
        '
        Me.lblCustAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustAddress.Location = New System.Drawing.Point(6, 40)
        Me.lblCustAddress.Name = "lblCustAddress"
        Me.lblCustAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblCustAddress.TabIndex = 27
        Me.lblCustAddress.Text = "Address"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtLocTelephone)
        Me.RadGroupBox2.Controls.Add(Me.lblTelephone)
        Me.RadGroupBox2.Controls.Add(Me.txtLocContactPerson)
        Me.RadGroupBox2.Controls.Add(Me.lblContactPerson)
        Me.RadGroupBox2.Controls.Add(Me.txtLocChannel)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.txtLocTown)
        Me.RadGroupBox2.Controls.Add(Me.txtLocationDesc)
        Me.RadGroupBox2.Controls.Add(Me.txtLocationAddress)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.fndLocationCode)
        Me.RadGroupBox2.Controls.Add(Me.lblLocationCode)
        Me.RadGroupBox2.HeaderText = "Location Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(6, 86)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(417, 120)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "Location Details"
        '
        'txtLocTelephone
        '
        Me.txtLocTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocTelephone.Location = New System.Drawing.Point(107, 94)
        Me.txtLocTelephone.MaxLength = 50
        Me.txtLocTelephone.MendatroryField = False
        Me.txtLocTelephone.MyLinkLable1 = Me.lblTelephone
        Me.txtLocTelephone.MyLinkLable2 = Nothing
        Me.txtLocTelephone.Name = "txtLocTelephone"
        Me.txtLocTelephone.Size = New System.Drawing.Size(305, 18)
        Me.txtLocTelephone.TabIndex = 6
        '
        'lblTelephone
        '
        Me.lblTelephone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelephone.Location = New System.Drawing.Point(7, 97)
        Me.lblTelephone.Name = "lblTelephone"
        Me.lblTelephone.Size = New System.Drawing.Size(60, 16)
        Me.lblTelephone.TabIndex = 25
        Me.lblTelephone.Text = "Telephone"
        '
        'txtLocContactPerson
        '
        Me.txtLocContactPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocContactPerson.Location = New System.Drawing.Point(107, 75)
        Me.txtLocContactPerson.MaxLength = 50
        Me.txtLocContactPerson.MendatroryField = False
        Me.txtLocContactPerson.MyLinkLable1 = Me.lblContactPerson
        Me.txtLocContactPerson.MyLinkLable2 = Nothing
        Me.txtLocContactPerson.Name = "txtLocContactPerson"
        Me.txtLocContactPerson.Size = New System.Drawing.Size(305, 18)
        Me.txtLocContactPerson.TabIndex = 5
        '
        'lblContactPerson
        '
        Me.lblContactPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactPerson.Location = New System.Drawing.Point(7, 78)
        Me.lblContactPerson.Name = "lblContactPerson"
        Me.lblContactPerson.Size = New System.Drawing.Size(84, 16)
        Me.lblContactPerson.TabIndex = 23
        Me.lblContactPerson.Text = "Contact Person"
        '
        'txtLocChannel
        '
        Me.txtLocChannel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocChannel.Location = New System.Drawing.Point(286, 56)
        Me.txtLocChannel.MaxLength = 12
        Me.txtLocChannel.MendatroryField = False
        Me.txtLocChannel.MyLinkLable1 = Nothing
        Me.txtLocChannel.MyLinkLable2 = Nothing
        Me.txtLocChannel.Name = "txtLocChannel"
        Me.txtLocChannel.Size = New System.Drawing.Size(127, 18)
        Me.txtLocChannel.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(236, 58)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel3.TabIndex = 20
        Me.MyLabel3.Text = "Channel"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(8, 61)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel2.TabIndex = 19
        Me.MyLabel2.Text = "Town"
        '
        'txtLocTown
        '
        Me.txtLocTown.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocTown.Location = New System.Drawing.Point(107, 56)
        Me.txtLocTown.MaxLength = 12
        Me.txtLocTown.MendatroryField = False
        Me.txtLocTown.MyLinkLable1 = Nothing
        Me.txtLocTown.MyLinkLable2 = Nothing
        Me.txtLocTown.Name = "txtLocTown"
        Me.txtLocTown.Size = New System.Drawing.Size(127, 18)
        Me.txtLocTown.TabIndex = 3
        '
        'txtLocationDesc
        '
        Me.txtLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationDesc.Location = New System.Drawing.Point(236, 16)
        Me.txtLocationDesc.MaxLength = 12
        Me.txtLocationDesc.MendatroryField = False
        Me.txtLocationDesc.MyLinkLable1 = Nothing
        Me.txtLocationDesc.MyLinkLable2 = Nothing
        Me.txtLocationDesc.Name = "txtLocationDesc"
        Me.txtLocationDesc.Size = New System.Drawing.Size(178, 18)
        Me.txtLocationDesc.TabIndex = 1
        '
        'txtLocationAddress
        '
        Me.txtLocationAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationAddress.Location = New System.Drawing.Point(108, 36)
        Me.txtLocationAddress.MaxLength = 50
        Me.txtLocationAddress.MendatroryField = False
        Me.txtLocationAddress.MyLinkLable1 = Me.MyLabel1
        Me.txtLocationAddress.MyLinkLable2 = Nothing
        Me.txtLocationAddress.Name = "txtLocationAddress"
        Me.txtLocationAddress.Size = New System.Drawing.Size(305, 18)
        Me.txtLocationAddress.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 39)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel1.TabIndex = 16
        Me.MyLabel1.Text = "Address"
        '
        'fndLocationCode
        '
        Me.fndLocationCode.Location = New System.Drawing.Point(108, 16)
        Me.fndLocationCode.MendatroryField = False
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Me.lblLocationCode
        Me.fndLocationCode.MyLinkLable2 = Nothing
        Me.fndLocationCode.MyReadOnly = False
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.Size = New System.Drawing.Size(126, 19)
        Me.fndLocationCode.TabIndex = 0
        Me.fndLocationCode.Value = ""
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(8, 19)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(79, 16)
        Me.lblLocationCode.TabIndex = 14
        Me.lblLocationCode.Text = "Location Code"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gv1)
        Me.RadGroupBox4.HeaderText = "Asset Requisition Detail"
        Me.RadGroupBox4.Location = New System.Drawing.Point(2, 212)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(820, 254)
        Me.RadGroupBox4.TabIndex = 0
        Me.RadGroupBox4.Text = "Asset Requisition Detail"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(800, 224)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgrpbxVisimaster)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(837, 512)
        Me.SplitContainer1.SplitterDistance = 472
        Me.SplitContainer1.TabIndex = 8
        '
        'frmAssetRequisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(837, 512)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmAssetRequisition"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Requisition"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ddlMoveType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetReqCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpReqDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReqDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxVisimaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxVisimaster.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txtCustTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustContactPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustContactPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustChannel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustChannel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustTown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustTown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtLocTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelephone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocContactPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblContactPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocChannel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocTown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpReqDate As common.Controls.MyDateTimePicker
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdgrpbxVisimaster As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblReqDate As common.Controls.MyLabel
    Friend WithEvents lblMoveType As common.Controls.MyLabel
    Friend WithEvents lblAssetReqCode As common.Controls.MyLabel
    Friend WithEvents lblCustomerCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents fndcustomerCode As common.UserControls.txtFinder
    Friend WithEvents txtCustname As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ddlMoveType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLocationAddress As common.Controls.MyTextBox
    Friend WithEvents MyTextBox2 As common.Controls.MyTextBox
    Friend WithEvents txtLocationDesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocTown As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtLocChannel As common.Controls.MyTextBox
    Friend WithEvents txtLocTelephone As common.Controls.MyTextBox
    Friend WithEvents lblTelephone As common.Controls.MyLabel
    Friend WithEvents txtLocContactPerson As common.Controls.MyTextBox
    Friend WithEvents lblContactPerson As common.Controls.MyLabel
    Friend WithEvents txtCustTelephone As common.Controls.MyTextBox
    Friend WithEvents lblCustTelephone As common.Controls.MyLabel
    Friend WithEvents txtCustContactPerson As common.Controls.MyTextBox
    Friend WithEvents lblCustContactPerson As common.Controls.MyLabel
    Friend WithEvents txtCustChannel As common.Controls.MyTextBox
    Friend WithEvents lblCustChannel As common.Controls.MyLabel
    Friend WithEvents lblCustTown As common.Controls.MyLabel
    Friend WithEvents txtCustTown As common.Controls.MyTextBox
    Friend WithEvents txtCustAddress As common.Controls.MyTextBox
    Friend WithEvents lblCustAddress As common.Controls.MyLabel
End Class

