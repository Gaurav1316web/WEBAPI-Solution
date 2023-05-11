<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmchildRouteFreight
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtFixedAmt = New common.MyNumBox()
        Me.txtFreight = New common.MyNumBox()
        Me.MyTextBox1 = New common.MyNumBox()
        Me.lblFromLocation = New common.Controls.MyLabel()
        Me.txtfromLocation = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lbltranspoterName = New common.Controls.MyLabel()
        Me.ddl_transtype = New common.Controls.MyComboBox()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblCityName = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndTranspoter = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndCity = New common.UserControls.txtFinder()
        Me.lblInvoiceType = New common.Controls.MyLabel()
        Me.rdlbltrnasfertype = New common.Controls.MyLabel()
        Me.ddltype = New common.Controls.MyComboBox()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFixedAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltranspoterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddl_transtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltrnasfertype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(662, 467)
        Me.SplitContainer1.SplitterDistance = 434
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtFixedAmt)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtFreight)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyTextBox1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblFromLocation)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtfromLocation)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lbltranspoterName)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ddl_transtype)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblLocationName)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblCityName)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.fndTranspoter)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.fndLocation)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.fndCity)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblInvoiceType)
        Me.SplitContainer2.Panel2.Controls.Add(Me.rdlbltrnasfertype)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ddltype)
        Me.SplitContainer2.Size = New System.Drawing.Size(662, 434)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 0
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(662, 20)
        Me.rdmenufile.TabIndex = 5
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmImport})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "Export"
        Me.rmExport.AccessibleName = "Export"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'txtFixedAmt
        '
        Me.txtFixedAmt.BackColor = System.Drawing.Color.White
        Me.txtFixedAmt.CalculationExpression = Nothing
        Me.txtFixedAmt.DecimalPlaces = 2
        Me.txtFixedAmt.FieldCode = Nothing
        Me.txtFixedAmt.FieldDesc = Nothing
        Me.txtFixedAmt.FieldMaxLength = 0
        Me.txtFixedAmt.FieldName = Nothing
        Me.txtFixedAmt.isCalculatedField = False
        Me.txtFixedAmt.IsSourceFromTable = False
        Me.txtFixedAmt.IsSourceFromValueList = False
        Me.txtFixedAmt.IsUnique = False
        Me.txtFixedAmt.Location = New System.Drawing.Point(131, 183)
        Me.txtFixedAmt.MaxLength = 8
        Me.txtFixedAmt.MendatroryField = False
        Me.txtFixedAmt.MyLinkLable1 = Nothing
        Me.txtFixedAmt.MyLinkLable2 = Nothing
        Me.txtFixedAmt.Name = "txtFixedAmt"
        Me.txtFixedAmt.ReferenceFieldDesc = Nothing
        Me.txtFixedAmt.ReferenceFieldName = Nothing
        Me.txtFixedAmt.ReferenceTableName = Nothing
        Me.txtFixedAmt.Size = New System.Drawing.Size(252, 20)
        Me.txtFixedAmt.TabIndex = 309
        Me.txtFixedAmt.Text = "0"
        Me.txtFixedAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFixedAmt.Value = 0.0R
        '
        'txtFreight
        '
        Me.txtFreight.BackColor = System.Drawing.Color.White
        Me.txtFreight.CalculationExpression = Nothing
        Me.txtFreight.DecimalPlaces = 2
        Me.txtFreight.FieldCode = Nothing
        Me.txtFreight.FieldDesc = Nothing
        Me.txtFreight.FieldMaxLength = 0
        Me.txtFreight.FieldName = Nothing
        Me.txtFreight.isCalculatedField = False
        Me.txtFreight.IsSourceFromTable = False
        Me.txtFreight.IsSourceFromValueList = False
        Me.txtFreight.IsUnique = False
        Me.txtFreight.Location = New System.Drawing.Point(132, 161)
        Me.txtFreight.MaxLength = 8
        Me.txtFreight.MendatroryField = False
        Me.txtFreight.MyLinkLable1 = Nothing
        Me.txtFreight.MyLinkLable2 = Nothing
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.ReferenceFieldDesc = Nothing
        Me.txtFreight.ReferenceFieldName = Nothing
        Me.txtFreight.ReferenceTableName = Nothing
        Me.txtFreight.Size = New System.Drawing.Size(252, 20)
        Me.txtFreight.TabIndex = 308
        Me.txtFreight.Text = "0"
        Me.txtFreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFreight.Value = 0.0R
        '
        'MyTextBox1
        '
        Me.MyTextBox1.BackColor = System.Drawing.Color.White
        Me.MyTextBox1.CalculationExpression = Nothing
        Me.MyTextBox1.DecimalPlaces = 2
        Me.MyTextBox1.FieldCode = Nothing
        Me.MyTextBox1.FieldDesc = Nothing
        Me.MyTextBox1.FieldMaxLength = 0
        Me.MyTextBox1.FieldName = Nothing
        Me.MyTextBox1.isCalculatedField = False
        Me.MyTextBox1.IsSourceFromTable = False
        Me.MyTextBox1.IsSourceFromValueList = False
        Me.MyTextBox1.IsUnique = False
        Me.MyTextBox1.Location = New System.Drawing.Point(131, 138)
        Me.MyTextBox1.MaxLength = 8
        Me.MyTextBox1.MendatroryField = False
        Me.MyTextBox1.MyLinkLable1 = Nothing
        Me.MyTextBox1.MyLinkLable2 = Nothing
        Me.MyTextBox1.Name = "MyTextBox1"
        Me.MyTextBox1.ReferenceFieldDesc = Nothing
        Me.MyTextBox1.ReferenceFieldName = Nothing
        Me.MyTextBox1.ReferenceTableName = Nothing
        Me.MyTextBox1.Size = New System.Drawing.Size(252, 20)
        Me.MyTextBox1.TabIndex = 307
        Me.MyTextBox1.Text = "0"
        Me.MyTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MyTextBox1.Value = 0.0R
        '
        'lblFromLocation
        '
        Me.lblFromLocation.AutoSize = False
        Me.lblFromLocation.BorderVisible = True
        Me.lblFromLocation.FieldName = Nothing
        Me.lblFromLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLocation.Location = New System.Drawing.Point(398, 62)
        Me.lblFromLocation.Name = "lblFromLocation"
        Me.lblFromLocation.Size = New System.Drawing.Size(242, 18)
        Me.lblFromLocation.TabIndex = 173
        Me.lblFromLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromLocation.TextWrap = False
        '
        'txtfromLocation
        '
        Me.txtfromLocation.CalculationExpression = Nothing
        Me.txtfromLocation.FieldCode = Nothing
        Me.txtfromLocation.FieldDesc = Nothing
        Me.txtfromLocation.FieldMaxLength = 0
        Me.txtfromLocation.FieldName = Nothing
        Me.txtfromLocation.isCalculatedField = False
        Me.txtfromLocation.IsSourceFromTable = False
        Me.txtfromLocation.IsSourceFromValueList = False
        Me.txtfromLocation.IsUnique = False
        Me.txtfromLocation.Location = New System.Drawing.Point(132, 60)
        Me.txtfromLocation.MendatroryField = True
        Me.txtfromLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfromLocation.MyLinkLable1 = Nothing
        Me.txtfromLocation.MyLinkLable2 = Nothing
        Me.txtfromLocation.MyReadOnly = False
        Me.txtfromLocation.MyShowMasterFormButton = False
        Me.txtfromLocation.Name = "txtfromLocation"
        Me.txtfromLocation.ReferenceFieldDesc = Nothing
        Me.txtfromLocation.ReferenceFieldName = Nothing
        Me.txtfromLocation.ReferenceTableName = Nothing
        Me.txtfromLocation.Size = New System.Drawing.Size(252, 20)
        Me.txtfromLocation.TabIndex = 171
        Me.txtfromLocation.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(22, 38)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel8.TabIndex = 172
        Me.MyLabel8.Text = "Location Code"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
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
        Me.txtDate.Location = New System.Drawing.Point(134, 234)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 168
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(23, 235)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel4.TabIndex = 169
        Me.RadLabel4.Text = "Effective Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(22, 13)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel1.TabIndex = 155
        Me.MyLabel1.Text = "Transaction Type"
        '
        'lbltranspoterName
        '
        Me.lbltranspoterName.AutoSize = False
        Me.lbltranspoterName.BorderVisible = True
        Me.lbltranspoterName.FieldName = Nothing
        Me.lbltranspoterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltranspoterName.Location = New System.Drawing.Point(398, 114)
        Me.lbltranspoterName.Name = "lbltranspoterName"
        Me.lbltranspoterName.Size = New System.Drawing.Size(242, 18)
        Me.lbltranspoterName.TabIndex = 167
        Me.lbltranspoterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbltranspoterName.TextWrap = False
        '
        'ddl_transtype
        '
        Me.ddl_transtype.AutoCompleteDisplayMember = Nothing
        Me.ddl_transtype.AutoCompleteValueMember = Nothing
        Me.ddl_transtype.CalculationExpression = Nothing
        Me.ddl_transtype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddl_transtype.FieldCode = Nothing
        Me.ddl_transtype.FieldDesc = Nothing
        Me.ddl_transtype.FieldMaxLength = 0
        Me.ddl_transtype.FieldName = Nothing
        Me.ddl_transtype.isCalculatedField = False
        Me.ddl_transtype.IsSourceFromTable = False
        Me.ddl_transtype.IsSourceFromValueList = False
        Me.ddl_transtype.IsUnique = False
        RadListDataItem1.Text = "Fixed"
        RadListDataItem2.Text = "MT"
        RadListDataItem3.Text = "Both"
        Me.ddl_transtype.Items.Add(RadListDataItem1)
        Me.ddl_transtype.Items.Add(RadListDataItem2)
        Me.ddl_transtype.Items.Add(RadListDataItem3)
        Me.ddl_transtype.Location = New System.Drawing.Point(132, 10)
        Me.ddl_transtype.MendatroryField = False
        Me.ddl_transtype.MyLinkLable1 = Nothing
        Me.ddl_transtype.MyLinkLable2 = Nothing
        Me.ddl_transtype.Name = "ddl_transtype"
        Me.ddl_transtype.ReferenceFieldDesc = Nothing
        Me.ddl_transtype.ReferenceFieldName = Nothing
        Me.ddl_transtype.ReferenceTableName = Nothing
        Me.ddl_transtype.Size = New System.Drawing.Size(139, 20)
        Me.ddl_transtype.TabIndex = 156
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(398, 37)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(242, 18)
        Me.lblLocationName.TabIndex = 166
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = False
        Me.lblCityName.BorderVisible = True
        Me.lblCityName.FieldName = Nothing
        Me.lblCityName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCityName.Location = New System.Drawing.Point(398, 86)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(242, 18)
        Me.lblCityName.TabIndex = 165
        Me.lblCityName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCityName.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(22, 183)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel6.TabIndex = 164
        Me.MyLabel6.Text = "Fixed Amount"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(22, 161)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel5.TabIndex = 162
        Me.MyLabel5.Text = "Freight"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(22, 138)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel4.TabIndex = 160
        Me.MyLabel4.Text = "Capacity(MT)"
        '
        'fndTranspoter
        '
        Me.fndTranspoter.CalculationExpression = Nothing
        Me.fndTranspoter.FieldCode = Nothing
        Me.fndTranspoter.FieldDesc = Nothing
        Me.fndTranspoter.FieldMaxLength = 0
        Me.fndTranspoter.FieldName = Nothing
        Me.fndTranspoter.isCalculatedField = False
        Me.fndTranspoter.IsSourceFromTable = False
        Me.fndTranspoter.IsSourceFromValueList = False
        Me.fndTranspoter.IsUnique = False
        Me.fndTranspoter.Location = New System.Drawing.Point(131, 112)
        Me.fndTranspoter.MendatroryField = True
        Me.fndTranspoter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTranspoter.MyLinkLable1 = Nothing
        Me.fndTranspoter.MyLinkLable2 = Nothing
        Me.fndTranspoter.MyReadOnly = False
        Me.fndTranspoter.MyShowMasterFormButton = False
        Me.fndTranspoter.Name = "fndTranspoter"
        Me.fndTranspoter.ReferenceFieldDesc = Nothing
        Me.fndTranspoter.ReferenceFieldName = Nothing
        Me.fndTranspoter.ReferenceTableName = Nothing
        Me.fndTranspoter.Size = New System.Drawing.Size(252, 20)
        Me.fndTranspoter.TabIndex = 157
        Me.fndTranspoter.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(22, 114)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel3.TabIndex = 158
        Me.MyLabel3.Text = "Transporter Code"
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
        Me.fndLocation.Location = New System.Drawing.Point(131, 36)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(252, 20)
        Me.fndLocation.TabIndex = 155
        Me.fndLocation.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(20, 61)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel2.TabIndex = 156
        Me.MyLabel2.Text = "To Location Code"
        '
        'fndCity
        '
        Me.fndCity.CalculationExpression = Nothing
        Me.fndCity.FieldCode = Nothing
        Me.fndCity.FieldDesc = Nothing
        Me.fndCity.FieldMaxLength = 0
        Me.fndCity.FieldName = Nothing
        Me.fndCity.isCalculatedField = False
        Me.fndCity.IsSourceFromTable = False
        Me.fndCity.IsSourceFromValueList = False
        Me.fndCity.IsUnique = False
        Me.fndCity.Location = New System.Drawing.Point(131, 86)
        Me.fndCity.MendatroryField = True
        Me.fndCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCity.MyLinkLable1 = Nothing
        Me.fndCity.MyLinkLable2 = Nothing
        Me.fndCity.MyReadOnly = False
        Me.fndCity.MyShowMasterFormButton = False
        Me.fndCity.Name = "fndCity"
        Me.fndCity.ReferenceFieldDesc = Nothing
        Me.fndCity.ReferenceFieldName = Nothing
        Me.fndCity.ReferenceTableName = Nothing
        Me.fndCity.Size = New System.Drawing.Size(252, 20)
        Me.fndCity.TabIndex = 6
        Me.fndCity.Value = ""
        '
        'lblInvoiceType
        '
        Me.lblInvoiceType.FieldName = Nothing
        Me.lblInvoiceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceType.Location = New System.Drawing.Point(292, 13)
        Me.lblInvoiceType.Name = "lblInvoiceType"
        Me.lblInvoiceType.Size = New System.Drawing.Size(31, 16)
        Me.lblInvoiceType.TabIndex = 153
        Me.lblInvoiceType.Text = "Type"
        '
        'rdlbltrnasfertype
        '
        Me.rdlbltrnasfertype.FieldName = Nothing
        Me.rdlbltrnasfertype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbltrnasfertype.Location = New System.Drawing.Point(22, 88)
        Me.rdlbltrnasfertype.Name = "rdlbltrnasfertype"
        Me.rdlbltrnasfertype.Size = New System.Drawing.Size(56, 16)
        Me.rdlbltrnasfertype.TabIndex = 7
        Me.rdlbltrnasfertype.Text = "City Code"
        '
        'ddltype
        '
        Me.ddltype.AutoCompleteDisplayMember = Nothing
        Me.ddltype.AutoCompleteValueMember = Nothing
        Me.ddltype.CalculationExpression = Nothing
        Me.ddltype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddltype.FieldCode = Nothing
        Me.ddltype.FieldDesc = Nothing
        Me.ddltype.FieldMaxLength = 0
        Me.ddltype.FieldName = Nothing
        Me.ddltype.isCalculatedField = False
        Me.ddltype.IsSourceFromTable = False
        Me.ddltype.IsSourceFromValueList = False
        Me.ddltype.IsUnique = False
        RadListDataItem4.Text = "Fixed"
        RadListDataItem5.Text = "MT"
        RadListDataItem6.Text = "Both"
        RadListDataItem7.Text = "KG"
        Me.ddltype.Items.Add(RadListDataItem4)
        Me.ddltype.Items.Add(RadListDataItem5)
        Me.ddltype.Items.Add(RadListDataItem6)
        Me.ddltype.Items.Add(RadListDataItem7)
        Me.ddltype.Location = New System.Drawing.Point(398, 10)
        Me.ddltype.MendatroryField = False
        Me.ddltype.MyLinkLable1 = Nothing
        Me.ddltype.MyLinkLable2 = Nothing
        Me.ddltype.Name = "ddltype"
        Me.ddltype.ReferenceFieldDesc = Nothing
        Me.ddltype.ReferenceFieldName = Nothing
        Me.ddltype.ReferenceTableName = Nothing
        Me.ddltype.Size = New System.Drawing.Size(140, 20)
        Me.ddltype.TabIndex = 154
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(587, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'frmchildRouteFreight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 467)
        Me.Controls.Add(Me.SplitContainer1)
        Me.IsMdiContainer = True
        Me.Name = "frmchildRouteFreight"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chid Route Freight Details"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFixedAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltranspoterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddl_transtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltrnasfertype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblInvoiceType As common.Controls.MyLabel
    Friend WithEvents ddltype As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddl_transtype As common.Controls.MyComboBox
    Friend WithEvents fndTranspoter As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndCity As common.UserControls.txtFinder
    Friend WithEvents rdlbltrnasfertype As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lbltranspoterName As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblCityName As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblFromLocation As common.Controls.MyLabel
    Friend WithEvents txtfromLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtFixedAmt As common.MyNumBox
    Friend WithEvents txtFreight As common.MyNumBox
    Friend WithEvents MyTextBox1 As common.MyNumBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
End Class

