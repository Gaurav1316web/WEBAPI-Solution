Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssetAgreement
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.lblDoc_No = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.txtEmpNo = New common.UserControls.txtFinder()
        Me.lblEMPName = New common.Controls.MyLabel()
        Me.lblEmpContactNo = New common.Controls.MyLabel()
        Me.txtEmpContactNo = New common.Controls.MyTextBox()
        Me.lblLocCode = New common.Controls.MyLabel()
        Me.txtLocCode = New common.UserControls.txtFinder()
        Me.lblLocDesc = New common.Controls.MyLabel()
        Me.lblCourierNo = New common.Controls.MyLabel()
        Me.txtCourierNo = New common.Controls.MyTextBox()
        Me.lblCourierCompanyName = New common.Controls.MyLabel()
        Me.txtCourierCompanyName = New common.Controls.MyTextBox()
        Me.dtpCourierDate = New common.Controls.MyDateTimePicker()
        Me.lblCourierDate = New common.Controls.MyLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rMenueImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rMenueExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rMenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDoc_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEMPName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpContactNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpContactNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCourierNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCourierNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCourierCompanyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCourierCompanyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpCourierDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCourierDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Asset Details With Outlet"
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 145)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1065, 304)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "Asset Details With Outlet"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1045, 274)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Location = New System.Drawing.Point(12, 580)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1060, 70)
        Me.Panel1.TabIndex = 44
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 44)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(988, 43)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(8, 44)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(453, 36)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 3
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(422, 37)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 47
        Me.lblDate.Text = "Date"
        '
        'lblDoc_No
        '
        Me.lblDoc_No.FieldName = Nothing
        Me.lblDoc_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoc_No.Location = New System.Drawing.Point(27, 36)
        Me.lblDoc_No.Name = "lblDoc_No"
        Me.lblDoc_No.Size = New System.Drawing.Size(79, 16)
        Me.lblDoc_No.TabIndex = 49
        Me.lblDoc_No.Text = "Document No."
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(147, 33)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblDoc_No
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 2
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPService.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(400, 32)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'lblEmpCode
        '
        Me.lblEmpCode.FieldName = Nothing
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.Location = New System.Drawing.Point(27, 58)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmpCode.TabIndex = 53
        Me.lblEmpCode.Text = "Employee Code"
        '
        'txtEmpNo
        '
        Me.txtEmpNo.CalculationExpression = Nothing
        Me.txtEmpNo.FieldCode = Nothing
        Me.txtEmpNo.FieldDesc = Nothing
        Me.txtEmpNo.FieldMaxLength = 0
        Me.txtEmpNo.FieldName = Nothing
        Me.txtEmpNo.isCalculatedField = False
        Me.txtEmpNo.IsSourceFromTable = False
        Me.txtEmpNo.IsSourceFromValueList = False
        Me.txtEmpNo.IsUnique = False
        Me.txtEmpNo.Location = New System.Drawing.Point(147, 55)
        Me.txtEmpNo.MendatroryField = True
        Me.txtEmpNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpNo.MyLinkLable1 = Me.lblEmpCode
        Me.txtEmpNo.MyLinkLable2 = Me.lblEMPName
        Me.txtEmpNo.MyReadOnly = False
        Me.txtEmpNo.MyShowMasterFormButton = False
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.ReferenceFieldDesc = Nothing
        Me.txtEmpNo.ReferenceFieldName = Nothing
        Me.txtEmpNo.ReferenceTableName = Nothing
        Me.txtEmpNo.Size = New System.Drawing.Size(155, 20)
        Me.txtEmpNo.TabIndex = 4
        Me.txtEmpNo.Value = ""
        '
        'lblEMPName
        '
        Me.lblEMPName.AutoSize = False
        Me.lblEMPName.BorderVisible = True
        Me.lblEMPName.FieldName = Nothing
        Me.lblEMPName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEMPName.Location = New System.Drawing.Point(302, 56)
        Me.lblEMPName.Name = "lblEMPName"
        Me.lblEMPName.Size = New System.Drawing.Size(287, 18)
        Me.lblEMPName.TabIndex = 52
        Me.lblEMPName.TextWrap = False
        '
        'lblEmpContactNo
        '
        Me.lblEmpContactNo.FieldName = Nothing
        Me.lblEmpContactNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpContactNo.Location = New System.Drawing.Point(27, 76)
        Me.lblEmpContactNo.Name = "lblEmpContactNo"
        Me.lblEmpContactNo.Size = New System.Drawing.Size(116, 16)
        Me.lblEmpContactNo.TabIndex = 55
        Me.lblEmpContactNo.Text = "Employee Contact No"
        '
        'txtEmpContactNo
        '
        Me.txtEmpContactNo.CalculationExpression = Nothing
        Me.txtEmpContactNo.FieldCode = Nothing
        Me.txtEmpContactNo.FieldDesc = Nothing
        Me.txtEmpContactNo.FieldMaxLength = 0
        Me.txtEmpContactNo.FieldName = Nothing
        Me.txtEmpContactNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpContactNo.isCalculatedField = False
        Me.txtEmpContactNo.IsSourceFromTable = False
        Me.txtEmpContactNo.IsSourceFromValueList = False
        Me.txtEmpContactNo.IsUnique = False
        Me.txtEmpContactNo.Location = New System.Drawing.Point(147, 77)
        Me.txtEmpContactNo.MaxLength = 200
        Me.txtEmpContactNo.MendatroryField = False
        Me.txtEmpContactNo.MyLinkLable1 = Me.lblEmpContactNo
        Me.txtEmpContactNo.MyLinkLable2 = Nothing
        Me.txtEmpContactNo.Name = "txtEmpContactNo"
        Me.txtEmpContactNo.ReferenceFieldDesc = Nothing
        Me.txtEmpContactNo.ReferenceFieldName = Nothing
        Me.txtEmpContactNo.ReferenceTableName = Nothing
        Me.txtEmpContactNo.Size = New System.Drawing.Size(347, 18)
        Me.txtEmpContactNo.TabIndex = 5
        '
        'lblLocCode
        '
        Me.lblLocCode.FieldName = Nothing
        Me.lblLocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocCode.Location = New System.Drawing.Point(26, 100)
        Me.lblLocCode.Name = "lblLocCode"
        Me.lblLocCode.Size = New System.Drawing.Size(79, 16)
        Me.lblLocCode.TabIndex = 58
        Me.lblLocCode.Text = "Location Code"
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
        Me.txtLocCode.Location = New System.Drawing.Point(147, 98)
        Me.txtLocCode.MendatroryField = True
        Me.txtLocCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocCode.MyLinkLable1 = Me.lblLocCode
        Me.txtLocCode.MyLinkLable2 = Me.lblLocDesc
        Me.txtLocCode.MyReadOnly = False
        Me.txtLocCode.MyShowMasterFormButton = False
        Me.txtLocCode.Name = "txtLocCode"
        Me.txtLocCode.ReferenceFieldDesc = Nothing
        Me.txtLocCode.ReferenceFieldName = Nothing
        Me.txtLocCode.ReferenceTableName = Nothing
        Me.txtLocCode.Size = New System.Drawing.Size(155, 20)
        Me.txtLocCode.TabIndex = 6
        Me.txtLocCode.Value = ""
        '
        'lblLocDesc
        '
        Me.lblLocDesc.AutoSize = False
        Me.lblLocDesc.BorderVisible = True
        Me.lblLocDesc.FieldName = Nothing
        Me.lblLocDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocDesc.Location = New System.Drawing.Point(302, 99)
        Me.lblLocDesc.Name = "lblLocDesc"
        Me.lblLocDesc.Size = New System.Drawing.Size(287, 18)
        Me.lblLocDesc.TabIndex = 57
        Me.lblLocDesc.TextWrap = False
        '
        'lblCourierNo
        '
        Me.lblCourierNo.FieldName = Nothing
        Me.lblCourierNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCourierNo.Location = New System.Drawing.Point(25, 123)
        Me.lblCourierNo.Name = "lblCourierNo"
        Me.lblCourierNo.Size = New System.Drawing.Size(64, 16)
        Me.lblCourierNo.TabIndex = 60
        Me.lblCourierNo.Text = "Courier No."
        '
        'txtCourierNo
        '
        Me.txtCourierNo.CalculationExpression = Nothing
        Me.txtCourierNo.FieldCode = Nothing
        Me.txtCourierNo.FieldDesc = Nothing
        Me.txtCourierNo.FieldMaxLength = 0
        Me.txtCourierNo.FieldName = Nothing
        Me.txtCourierNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCourierNo.isCalculatedField = False
        Me.txtCourierNo.IsSourceFromTable = False
        Me.txtCourierNo.IsSourceFromValueList = False
        Me.txtCourierNo.IsUnique = False
        Me.txtCourierNo.Location = New System.Drawing.Point(146, 120)
        Me.txtCourierNo.MaxLength = 200
        Me.txtCourierNo.MendatroryField = True
        Me.txtCourierNo.MyLinkLable1 = Me.lblCourierNo
        Me.txtCourierNo.MyLinkLable2 = Nothing
        Me.txtCourierNo.Name = "txtCourierNo"
        Me.txtCourierNo.ReferenceFieldDesc = Nothing
        Me.txtCourierNo.ReferenceFieldName = Nothing
        Me.txtCourierNo.ReferenceTableName = Nothing
        Me.txtCourierNo.Size = New System.Drawing.Size(154, 18)
        Me.txtCourierNo.TabIndex = 7
        '
        'lblCourierCompanyName
        '
        Me.lblCourierCompanyName.FieldName = Nothing
        Me.lblCourierCompanyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCourierCompanyName.Location = New System.Drawing.Point(306, 123)
        Me.lblCourierCompanyName.Name = "lblCourierCompanyName"
        Me.lblCourierCompanyName.Size = New System.Drawing.Size(128, 16)
        Me.lblCourierCompanyName.TabIndex = 62
        Me.lblCourierCompanyName.Text = "Courier Company Name"
        '
        'txtCourierCompanyName
        '
        Me.txtCourierCompanyName.CalculationExpression = Nothing
        Me.txtCourierCompanyName.FieldCode = Nothing
        Me.txtCourierCompanyName.FieldDesc = Nothing
        Me.txtCourierCompanyName.FieldMaxLength = 0
        Me.txtCourierCompanyName.FieldName = Nothing
        Me.txtCourierCompanyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCourierCompanyName.isCalculatedField = False
        Me.txtCourierCompanyName.IsSourceFromTable = False
        Me.txtCourierCompanyName.IsSourceFromValueList = False
        Me.txtCourierCompanyName.IsUnique = False
        Me.txtCourierCompanyName.Location = New System.Drawing.Point(440, 121)
        Me.txtCourierCompanyName.MaxLength = 200
        Me.txtCourierCompanyName.MendatroryField = True
        Me.txtCourierCompanyName.MyLinkLable1 = Me.lblCourierCompanyName
        Me.txtCourierCompanyName.MyLinkLable2 = Nothing
        Me.txtCourierCompanyName.Name = "txtCourierCompanyName"
        Me.txtCourierCompanyName.ReferenceFieldDesc = Nothing
        Me.txtCourierCompanyName.ReferenceFieldName = Nothing
        Me.txtCourierCompanyName.ReferenceTableName = Nothing
        Me.txtCourierCompanyName.Size = New System.Drawing.Size(284, 18)
        Me.txtCourierCompanyName.TabIndex = 8
        '
        'dtpCourierDate
        '
        Me.dtpCourierDate.CalculationExpression = Nothing
        Me.dtpCourierDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpCourierDate.FieldCode = Nothing
        Me.dtpCourierDate.FieldDesc = Nothing
        Me.dtpCourierDate.FieldMaxLength = 0
        Me.dtpCourierDate.FieldName = Nothing
        Me.dtpCourierDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpCourierDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCourierDate.isCalculatedField = False
        Me.dtpCourierDate.IsSourceFromTable = False
        Me.dtpCourierDate.IsSourceFromValueList = False
        Me.dtpCourierDate.IsUnique = False
        Me.dtpCourierDate.Location = New System.Drawing.Point(808, 122)
        Me.dtpCourierDate.MendatroryField = True
        Me.dtpCourierDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCourierDate.MyLinkLable1 = Me.lblCourierDate
        Me.dtpCourierDate.MyLinkLable2 = Nothing
        Me.dtpCourierDate.Name = "dtpCourierDate"
        Me.dtpCourierDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpCourierDate.ReferenceFieldDesc = Nothing
        Me.dtpCourierDate.ReferenceFieldName = Nothing
        Me.dtpCourierDate.ReferenceTableName = Nothing
        Me.dtpCourierDate.Size = New System.Drawing.Size(126, 18)
        Me.dtpCourierDate.TabIndex = 9
        Me.dtpCourierDate.TabStop = False
        Me.dtpCourierDate.Text = "13/06/2011"
        Me.dtpCourierDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblCourierDate
        '
        Me.lblCourierDate.AllowShowFocusCues = True
        Me.lblCourierDate.FieldName = Nothing
        Me.lblCourierDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCourierDate.Location = New System.Drawing.Point(730, 123)
        Me.lblCourierDate.Name = "lblCourierDate"
        Me.lblCourierDate.Size = New System.Drawing.Size(70, 16)
        Me.lblCourierDate.TabIndex = 64
        Me.lblCourierDate.Text = "Courier Date"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(12, 23)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1050, 147)
        Me.Panel2.TabIndex = 65
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1041, 117)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(1084, 20)
        Me.rdmenufile.TabIndex = 66
        '
        'RadMenufile
        '
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rMenueImport, Me.rMenueExport, Me.rMenuClose})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        '
        'rMenueImport
        '
        Me.rMenueImport.Name = "rMenueImport"
        Me.rMenueImport.Text = "Import"
        '
        'rMenueExport
        '
        Me.rMenueExport.Name = "rMenueExport"
        Me.rMenueExport.Text = "Export"
        '
        'rMenuClose
        '
        Me.rMenuClose.Name = "rMenuClose"
        Me.rMenuClose.Text = "Close"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 455)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel1.TabIndex = 68
        Me.MyLabel1.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
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
        Me.txtRemarks.Location = New System.Drawing.Point(127, 456)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Me.MyLabel1
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(430, 71)
        Me.txtRemarks.TabIndex = 10
        '
        'FrmAssetAgreement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 662)
        Me.Controls.Add(Me.MyLabel1)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.rdmenufile)
        Me.Controls.Add(Me.dtpCourierDate)
        Me.Controls.Add(Me.lblCourierDate)
        Me.Controls.Add(Me.lblCourierCompanyName)
        Me.Controls.Add(Me.txtCourierCompanyName)
        Me.Controls.Add(Me.lblCourierNo)
        Me.Controls.Add(Me.txtCourierNo)
        Me.Controls.Add(Me.lblLocCode)
        Me.Controls.Add(Me.txtLocCode)
        Me.Controls.Add(Me.lblLocDesc)
        Me.Controls.Add(Me.lblEmpContactNo)
        Me.Controls.Add(Me.txtEmpContactNo)
        Me.Controls.Add(Me.lblEmpCode)
        Me.Controls.Add(Me.txtEmpNo)
        Me.Controls.Add(Me.lblEMPName)
        Me.Controls.Add(Me.btnAddNew)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblDoc_No)
        Me.Controls.Add(Me.txtDocNo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "FrmAssetAgreement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Agreement"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDoc_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEMPName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpContactNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpContactNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCourierNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCourierNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCourierCompanyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCourierCompanyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpCourierDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCourierDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblDoc_No As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents txtEmpNo As common.UserControls.txtFinder
    Friend WithEvents lblEMPName As common.Controls.MyLabel
    Friend WithEvents lblEmpContactNo As common.Controls.MyLabel
    Friend WithEvents txtEmpContactNo As common.Controls.MyTextBox
    Friend WithEvents lblLocCode As common.Controls.MyLabel
    Friend WithEvents txtLocCode As common.UserControls.txtFinder
    Friend WithEvents lblLocDesc As common.Controls.MyLabel
    Friend WithEvents lblCourierNo As common.Controls.MyLabel
    Friend WithEvents txtCourierNo As common.Controls.MyTextBox
    Friend WithEvents txtCourierCompanyName As common.Controls.MyTextBox
    Friend WithEvents lblCourierCompanyName As common.Controls.MyLabel
    Friend WithEvents dtpCourierDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblCourierDate As common.Controls.MyLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rMenueImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rMenueExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rMenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
End Class

