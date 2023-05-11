<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMccGatePass
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.lblpaymentno = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtLocDesc = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtLocCode = New common.UserControls.txtFinder()
        Me.txtComments = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblVehicleDesc = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblpaymentpostdate = New common.Controls.MyLabel()
        Me.cmbtype = New common.Controls.MyComboBox()
        Me.lblfullempty = New common.Controls.MyLabel()
        Me.txtVehicle = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSelect = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.txtmultiBooking = New common.UserControls.txtMultiSelectFinder()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfullempty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 156)
        '
        'Gv1
        '
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1076, 319)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'lblSalesman
        '
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(4, 58)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(61, 16)
        Me.lblSalesman.TabIndex = 35
        Me.lblSalesman.Text = "Vehicle No"
        '
        'lblpaymentno
        '
        Me.lblpaymentno.FieldName = Nothing
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentno.Location = New System.Drawing.Point(4, 6)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(77, 16)
        Me.lblpaymentno.TabIndex = 30
        Me.lblpaymentno.Text = "Gate Pass No"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 24)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1005, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.ERP.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(330, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 32
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtmultiBooking)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.btnGo)
        Me.Panel1.Controls.Add(Me.txtLocDesc)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.txtLocCode)
        Me.Panel1.Controls.Add(Me.txtComments)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.lblVehicleDesc)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Controls.Add(Me.cmbtype)
        Me.Panel1.Controls.Add(Me.lblSalesman)
        Me.Panel1.Controls.Add(Me.lblfullempty)
        Me.Panel1.Controls.Add(Me.lblpaymentno)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Controls.Add(Me.txtVehicle)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.lblpaymentpostdate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1076, 156)
        Me.Panel1.TabIndex = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(5, 82)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel5.TabIndex = 1458
        Me.MyLabel5.Text = "Invoice No"
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnGo.Location = New System.Drawing.Point(746, 130)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(38, 22)
        Me.btnGo.TabIndex = 59
        Me.btnGo.Text = ">>"
        '
        'txtLocDesc
        '
        Me.txtLocDesc.CalculationExpression = Nothing
        Me.txtLocDesc.FieldCode = Nothing
        Me.txtLocDesc.FieldDesc = Nothing
        Me.txtLocDesc.FieldMaxLength = 0
        Me.txtLocDesc.FieldName = Nothing
        Me.txtLocDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocDesc.isCalculatedField = False
        Me.txtLocDesc.IsSourceFromTable = False
        Me.txtLocDesc.IsSourceFromValueList = False
        Me.txtLocDesc.IsUnique = False
        Me.txtLocDesc.Location = New System.Drawing.Point(252, 33)
        Me.txtLocDesc.MaxLength = 200
        Me.txtLocDesc.MendatroryField = False
        Me.txtLocDesc.MyLinkLable1 = Me.MyLabel4
        Me.txtLocDesc.MyLinkLable2 = Nothing
        Me.txtLocDesc.Name = "txtLocDesc"
        Me.txtLocDesc.ReferenceFieldDesc = Nothing
        Me.txtLocDesc.ReferenceFieldName = Nothing
        Me.txtLocDesc.ReferenceTableName = Nothing
        Me.txtLocDesc.Size = New System.Drawing.Size(474, 18)
        Me.txtLocDesc.TabIndex = 65
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 32)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel4.TabIndex = 66
        Me.MyLabel4.Text = "Location"
        '
        'txtLocCode
        '
        Me.txtLocCode.CalculationExpression = Nothing
        Me.txtLocCode.FieldCode = Nothing
        Me.txtLocCode.FieldDesc = Nothing
        Me.txtLocCode.FieldMaxLength = 0
        Me.txtLocCode.FieldName = Nothing
        Me.txtLocCode.isCalculatedField = False
        Me.txtLocCode.IsSourceFromTable = False
        Me.txtLocCode.IsSourceFromValueList = False
        Me.txtLocCode.IsUnique = False
        Me.txtLocCode.Location = New System.Drawing.Point(93, 31)
        Me.txtLocCode.MendatroryField = True
        Me.txtLocCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocCode.MyLinkLable1 = Me.MyLabel4
        Me.txtLocCode.MyLinkLable2 = Nothing
        Me.txtLocCode.MyReadOnly = False
        Me.txtLocCode.MyShowMasterFormButton = False
        Me.txtLocCode.Name = "txtLocCode"
        Me.txtLocCode.ReferenceFieldDesc = Nothing
        Me.txtLocCode.ReferenceFieldName = Nothing
        Me.txtLocCode.ReferenceTableName = Nothing
        Me.txtLocCode.Size = New System.Drawing.Size(153, 18)
        Me.txtLocCode.TabIndex = 64
        Me.txtLocCode.Value = ""
        '
        'txtComments
        '
        Me.txtComments.CalculationExpression = Nothing
        Me.txtComments.FieldCode = Nothing
        Me.txtComments.FieldDesc = Nothing
        Me.txtComments.FieldMaxLength = 0
        Me.txtComments.FieldName = Nothing
        Me.txtComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.isCalculatedField = False
        Me.txtComments.IsSourceFromTable = False
        Me.txtComments.IsSourceFromValueList = False
        Me.txtComments.IsUnique = False
        Me.txtComments.Location = New System.Drawing.Point(92, 131)
        Me.txtComments.MaxLength = 200
        Me.txtComments.MendatroryField = False
        Me.txtComments.MyLinkLable1 = Me.MyLabel2
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReferenceFieldDesc = Nothing
        Me.txtComments.ReferenceFieldName = Nothing
        Me.txtComments.ReferenceTableName = Nothing
        Me.txtComments.Size = New System.Drawing.Size(633, 18)
        Me.txtComments.TabIndex = 6
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 136)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel2.TabIndex = 36
        Me.MyLabel2.Text = "Comments"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(92, 106)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.MyLabel3
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(633, 18)
        Me.txtRemarks.TabIndex = 5
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(4, 110)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel3.TabIndex = 36
        Me.MyLabel3.Text = "Remarks"
        '
        'lblVehicleDesc
        '
        Me.lblVehicleDesc.CalculationExpression = Nothing
        Me.lblVehicleDesc.FieldCode = Nothing
        Me.lblVehicleDesc.FieldDesc = Nothing
        Me.lblVehicleDesc.FieldMaxLength = 0
        Me.lblVehicleDesc.FieldName = Nothing
        Me.lblVehicleDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleDesc.isCalculatedField = False
        Me.lblVehicleDesc.IsSourceFromTable = False
        Me.lblVehicleDesc.IsSourceFromValueList = False
        Me.lblVehicleDesc.IsUnique = False
        Me.lblVehicleDesc.Location = New System.Drawing.Point(251, 57)
        Me.lblVehicleDesc.MaxLength = 200
        Me.lblVehicleDesc.MendatroryField = False
        Me.lblVehicleDesc.MyLinkLable1 = Me.lblSalesman
        Me.lblVehicleDesc.MyLinkLable2 = Nothing
        Me.lblVehicleDesc.Name = "lblVehicleDesc"
        Me.lblVehicleDesc.ReferenceFieldDesc = Nothing
        Me.lblVehicleDesc.ReferenceFieldName = Nothing
        Me.lblVehicleDesc.ReferenceTableName = Nothing
        Me.lblVehicleDesc.Size = New System.Drawing.Size(474, 18)
        Me.lblVehicleDesc.TabIndex = 3
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy  hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(444, 4)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblpaymentpostdate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(133, 20)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "10/06/2011  11:51 AM"
        Me.txtDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentpostdate
        '
        Me.lblpaymentpostdate.FieldName = Nothing
        Me.lblpaymentpostdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentpostdate.Location = New System.Drawing.Point(354, 6)
        Me.lblpaymentpostdate.Name = "lblpaymentpostdate"
        Me.lblpaymentpostdate.Size = New System.Drawing.Size(86, 16)
        Me.lblpaymentpostdate.TabIndex = 60
        Me.lblpaymentpostdate.Text = "Gate Pass Date"
        '
        'cmbtype
        '
        Me.cmbtype.AutoCompleteDisplayMember = Nothing
        Me.cmbtype.AutoCompleteValueMember = Nothing
        Me.cmbtype.CalculationExpression = Nothing
        Me.cmbtype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbtype.FieldCode = Nothing
        Me.cmbtype.FieldDesc = Nothing
        Me.cmbtype.FieldMaxLength = 0
        Me.cmbtype.FieldName = Nothing
        Me.cmbtype.isCalculatedField = False
        Me.cmbtype.IsSourceFromTable = False
        Me.cmbtype.IsSourceFromValueList = False
        Me.cmbtype.IsUnique = False
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "Mcc"
        RadListDataItem3.Text = "Scrap"
        Me.cmbtype.Items.Add(RadListDataItem1)
        Me.cmbtype.Items.Add(RadListDataItem2)
        Me.cmbtype.Items.Add(RadListDataItem3)
        Me.cmbtype.Location = New System.Drawing.Point(645, 6)
        Me.cmbtype.MendatroryField = False
        Me.cmbtype.MyLinkLable1 = Me.lblfullempty
        Me.cmbtype.MyLinkLable2 = Nothing
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.ReferenceFieldDesc = Nothing
        Me.cmbtype.ReferenceFieldName = Nothing
        Me.cmbtype.ReferenceTableName = Nothing
        Me.cmbtype.Size = New System.Drawing.Size(80, 20)
        Me.cmbtype.TabIndex = 1
        '
        'lblfullempty
        '
        Me.lblfullempty.FieldName = Nothing
        Me.lblfullempty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfullempty.Location = New System.Drawing.Point(582, 6)
        Me.lblfullempty.Name = "lblfullempty"
        Me.lblfullempty.Size = New System.Drawing.Size(31, 16)
        Me.lblfullempty.TabIndex = 3
        Me.lblfullempty.Text = "Type"
        '
        'txtVehicle
        '
        Me.txtVehicle.CalculationExpression = Nothing
        Me.txtVehicle.FieldCode = Nothing
        Me.txtVehicle.FieldDesc = Nothing
        Me.txtVehicle.FieldMaxLength = 0
        Me.txtVehicle.FieldName = Nothing
        Me.txtVehicle.isCalculatedField = False
        Me.txtVehicle.IsSourceFromTable = False
        Me.txtVehicle.IsSourceFromValueList = False
        Me.txtVehicle.IsUnique = False
        Me.txtVehicle.Location = New System.Drawing.Point(92, 56)
        Me.txtVehicle.MendatroryField = True
        Me.txtVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.MyLinkLable1 = Me.lblSalesman
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.MyReadOnly = False
        Me.txtVehicle.MyShowMasterFormButton = False
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.ReferenceFieldDesc = Nothing
        Me.txtVehicle.ReferenceFieldName = Nothing
        Me.txtVehicle.ReferenceTableName = Nothing
        Me.txtVehicle.Size = New System.Drawing.Size(153, 18)
        Me.txtVehicle.TabIndex = 2
        Me.txtVehicle.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(92, 4)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblpaymentno
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(238, 20)
        Me.txtCode.TabIndex = 31
        Me.txtCode.Value = ""
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1076, 519)
        Me.SplitContainer1.SplitterDistance = 475
        Me.SplitContainer1.TabIndex = 3
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(79, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 24)
        Me.btnPrint.TabIndex = 55
        Me.btnPrint.Text = "Print"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadSplitButton1.Location = New System.Drawing.Point(232, 8)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(80, 23)
        Me.RadSplitButton1.TabIndex = 54
        Me.RadSplitButton1.Text = "Print"
        Me.RadSplitButton1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "PrePrinted"
        Me.RadMenuItem1.AccessibleName = "PrePrinted"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "PrePrinted"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Print"
        Me.RadMenuItem2.AccessibleName = "Print"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(320, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 24)
        Me.btnPost.TabIndex = 12
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(157, 8)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(64, 24)
        Me.btnSelect.TabIndex = 11
        Me.btnSelect.Text = "Select All"
        Me.btnSelect.Visible = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1076, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'txtmultiBooking
        '
        Me.txtmultiBooking.arrDispalyMember = Nothing
        Me.txtmultiBooking.arrValueMember = Nothing
        Me.txtmultiBooking.Location = New System.Drawing.Point(93, 80)
        Me.txtmultiBooking.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultiBooking.MyLinkLable1 = Nothing
        Me.txtmultiBooking.MyLinkLable2 = Nothing
        Me.txtmultiBooking.MyNullText = "All"
        Me.txtmultiBooking.Name = "txtmultiBooking"
        Me.txtmultiBooking.Size = New System.Drawing.Size(633, 19)
        Me.txtmultiBooking.TabIndex = 1459
        '
        'frmMccGatePass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1076, 539)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmMccGatePass"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Mcc Scrap GatePass Entry"
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfullempty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtVehicle As common.UserControls.txtFinder
    Friend WithEvents lblpaymentpostdate As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbtype As common.Controls.MyComboBox
    Friend WithEvents lblfullempty As common.Controls.MyLabel
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents lblVehicleDesc As common.Controls.MyTextBox
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLocDesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtLocCode As common.UserControls.txtFinder
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtmultiBooking As common.UserControls.txtMultiSelectFinder
End Class

