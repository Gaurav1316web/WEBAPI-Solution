<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmQuickEntry1
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtEntryNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkPrintCheque = New System.Windows.Forms.CheckBox()
        Me.txtLocation = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtBankName = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndBankCode = New common.UserControls.txtFinder()
        Me.rdlblrouteno = New common.Controls.MyLabel()
        Me.ddlType = New common.Controls.MyComboBox()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.rdlbltransferdate = New common.Controls.MyLabel()
        Me.dtDocDate = New common.Controls.MyDateTimePicker()
        Me.MasterTemplate = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkSecurity = New System.Windows.Forms.CheckBox()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblrouteno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 13)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "Quick Entry No"
        '
        'txtEntryNo
        '
        Me.txtEntryNo.FieldName = Nothing
        Me.txtEntryNo.Location = New System.Drawing.Point(97, 10)
        Me.txtEntryNo.MendatroryField = False
        Me.txtEntryNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtEntryNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtEntryNo.MyLinkLable1 = Me.RadLabel1
        Me.txtEntryNo.MyLinkLable2 = Nothing
        Me.txtEntryNo.MyMaxLength = 32767
        Me.txtEntryNo.MyReadOnly = False
        Me.txtEntryNo.Name = "txtEntryNo"
        Me.txtEntryNo.Size = New System.Drawing.Size(254, 20)
        Me.txtEntryNo.TabIndex = 0
        Me.txtEntryNo.TabStop = False
        Me.txtEntryNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(351, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnUnSelect)
        Me.RadGroupBox1.Controls.Add(Me.btnPrint)
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnDelete)
        Me.RadGroupBox1.Controls.Add(Me.btnPost)
        Me.RadGroupBox1.Controls.Add(Me.btnSave)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 379)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(787, 34)
        Me.RadGroupBox1.TabIndex = 1
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Location = New System.Drawing.Point(296, 6)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(80, 22)
        Me.btnUnSelect.TabIndex = 259
        Me.btnUnSelect.Text = "Select All"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(221, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(711, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(148, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkSecurity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkPrintCheque)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBankName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndBankCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlblrouteno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel29)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlbltransferdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtDocDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MasterTemplate)
        Me.SplitContainer1.Size = New System.Drawing.Size(787, 359)
        Me.SplitContainer1.SplitterDistance = 86
        Me.SplitContainer1.TabIndex = 0
        '
        'chkPrintCheque
        '
        Me.chkPrintCheque.AutoSize = True
        Me.chkPrintCheque.Location = New System.Drawing.Point(583, 11)
        Me.chkPrintCheque.Name = "chkPrintCheque"
        Me.chkPrintCheque.Size = New System.Drawing.Size(93, 17)
        Me.chkPrintCheque.TabIndex = 103
        Me.chkPrintCheque.Text = "Print Cheque"
        Me.chkPrintCheque.UseVisualStyleBackColor = True
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(448, 57)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(128, 18)
        Me.txtLocation.TabIndex = 102
        Me.txtLocation.TabStop = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(388, 57)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel2.TabIndex = 12
        Me.MyLabel2.Text = "Location"
        '
        'txtBankName
        '
        Me.txtBankName.CalculationExpression = Nothing
        Me.txtBankName.FieldCode = Nothing
        Me.txtBankName.FieldDesc = Nothing
        Me.txtBankName.FieldMaxLength = 0
        Me.txtBankName.FieldName = Nothing
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.isCalculatedField = False
        Me.txtBankName.IsSourceFromTable = False
        Me.txtBankName.IsSourceFromValueList = False
        Me.txtBankName.IsUnique = False
        Me.txtBankName.Location = New System.Drawing.Point(97, 55)
        Me.txtBankName.MendatroryField = False
        Me.txtBankName.MyLinkLable1 = Nothing
        Me.txtBankName.MyLinkLable2 = Nothing
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReadOnly = True
        Me.txtBankName.ReferenceFieldDesc = Nothing
        Me.txtBankName.ReferenceFieldName = Nothing
        Me.txtBankName.ReferenceTableName = Nothing
        Me.txtBankName.Size = New System.Drawing.Size(274, 18)
        Me.txtBankName.TabIndex = 101
        Me.txtBankName.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 57)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 11
        Me.MyLabel1.Text = "Bank Name"
        '
        'fndBankCode
        '
        Me.fndBankCode.CalculationExpression = Nothing
        Me.fndBankCode.FieldCode = Nothing
        Me.fndBankCode.FieldDesc = Nothing
        Me.fndBankCode.FieldMaxLength = 0
        Me.fndBankCode.FieldName = Nothing
        Me.fndBankCode.isCalculatedField = False
        Me.fndBankCode.IsSourceFromTable = False
        Me.fndBankCode.IsSourceFromValueList = False
        Me.fndBankCode.IsUnique = False
        Me.fndBankCode.Location = New System.Drawing.Point(97, 34)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable1 = Me.rdlblrouteno
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.MyShowMasterFormButton = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.ReferenceFieldDesc = Nothing
        Me.fndBankCode.ReferenceFieldName = Nothing
        Me.fndBankCode.ReferenceTableName = Nothing
        Me.fndBankCode.Size = New System.Drawing.Size(274, 19)
        Me.fndBankCode.TabIndex = 3
        Me.fndBankCode.Value = ""
        '
        'rdlblrouteno
        '
        Me.rdlblrouteno.FieldName = Nothing
        Me.rdlblrouteno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblrouteno.Location = New System.Drawing.Point(6, 35)
        Me.rdlblrouteno.Name = "rdlblrouteno"
        Me.rdlblrouteno.Size = New System.Drawing.Size(62, 16)
        Me.rdlblrouteno.TabIndex = 10
        Me.rdlblrouteno.Text = "Bank Code"
        '
        'ddlType
        '
        Me.ddlType.CalculationExpression = Nothing
        Me.ddlType.DropDownAnimationEnabled = True
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.FieldCode = Nothing
        Me.ddlType.FieldDesc = Nothing
        Me.ddlType.FieldMaxLength = 0
        Me.ddlType.FieldName = Nothing
        Me.ddlType.isCalculatedField = False
        Me.ddlType.IsSourceFromTable = False
        Me.ddlType.IsSourceFromValueList = False
        Me.ddlType.IsUnique = False
        RadListDataItem1.Selected = True
        RadListDataItem1.Text = "Receipt"
        RadListDataItem2.Text = "Misc Receipt"
        RadListDataItem3.Text = "Payment"
        Me.ddlType.Items.Add(RadListDataItem1)
        Me.ddlType.Items.Add(RadListDataItem2)
        Me.ddlType.Items.Add(RadListDataItem3)
        Me.ddlType.Location = New System.Drawing.Point(448, 35)
        Me.ddlType.MendatroryField = True
        Me.ddlType.MyLinkLable1 = Me.RadLabel29
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.ReferenceFieldDesc = Nothing
        Me.ddlType.ReferenceFieldName = Nothing
        Me.ddlType.ReferenceTableName = Nothing
        Me.ddlType.Size = New System.Drawing.Size(128, 20)
        Me.ddlType.TabIndex = 4
        Me.ddlType.Text = "Receipt"
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(388, 37)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(31, 16)
        Me.RadLabel29.TabIndex = 11
        Me.RadLabel29.Text = "Type"
        '
        'rdlbltransferdate
        '
        Me.rdlbltransferdate.FieldName = Nothing
        Me.rdlbltransferdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbltransferdate.Location = New System.Drawing.Point(388, 13)
        Me.rdlbltransferdate.Name = "rdlbltransferdate"
        Me.rdlbltransferdate.Size = New System.Drawing.Size(30, 16)
        Me.rdlbltransferdate.TabIndex = 12
        Me.rdlbltransferdate.Text = "Date"
        '
        'dtDocDate
        '
        Me.dtDocDate.CalculationExpression = Nothing
        Me.dtDocDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtDocDate.FieldCode = Nothing
        Me.dtDocDate.FieldDesc = Nothing
        Me.dtDocDate.FieldMaxLength = 0
        Me.dtDocDate.FieldName = Nothing
        Me.dtDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDocDate.isCalculatedField = False
        Me.dtDocDate.IsSourceFromTable = False
        Me.dtDocDate.IsSourceFromValueList = False
        Me.dtDocDate.IsUnique = False
        Me.dtDocDate.Location = New System.Drawing.Point(448, 10)
        Me.dtDocDate.MendatroryField = False
        Me.dtDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtDocDate.MyLinkLable1 = Me.rdlbltransferdate
        Me.dtDocDate.MyLinkLable2 = Nothing
        Me.dtDocDate.Name = "dtDocDate"
        Me.dtDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtDocDate.ReferenceFieldDesc = Nothing
        Me.dtDocDate.ReferenceFieldName = Nothing
        Me.dtDocDate.ReferenceTableName = Nothing
        Me.dtDocDate.Size = New System.Drawing.Size(90, 20)
        Me.dtDocDate.TabIndex = 2
        Me.dtDocDate.TabStop = False
        Me.dtDocDate.Text = "27/06/2011 12:00 AM"
        Me.dtDocDate.Value = New Date(2011, 6, 27, 0, 0, 0, 0)
        '
        'MasterTemplate
        '
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MasterTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.Color.Black
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.MasterTemplate.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.MasterTemplate.MasterTemplate.AllowColumnReorder = False
        Me.MasterTemplate.MasterTemplate.AllowDeleteRow = False
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.MasterTemplate.EnableSorting = False
        Me.MasterTemplate.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.MasterTemplate.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.Size = New System.Drawing.Size(787, 269)
        Me.MasterTemplate.TabIndex = 0
        Me.MasterTemplate.TabStop = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(787, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiImport, Me.rmiExport})
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "File"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export Blank Sheet"
        '
        'chkSecurity
        '
        Me.chkSecurity.AutoSize = True
        Me.chkSecurity.Location = New System.Drawing.Point(677, 11)
        Me.chkSecurity.Name = "chkSecurity"
        Me.chkSecurity.Size = New System.Drawing.Size(66, 17)
        Me.chkSecurity.TabIndex = 104
        Me.chkSecurity.Text = "Security"
        Me.chkSecurity.UseVisualStyleBackColor = True
        '
        'FrmQuickEntry1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(787, 413)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.MinimumSize = New System.Drawing.Size(599, 443)
        Me.Name = "FrmQuickEntry1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Quick Book Entry"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblrouteno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtEntryNo As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents rdlbltransferdate As common.Controls.MyLabel
    Friend WithEvents rdlblrouteno As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtBankName As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkPrintCheque As CheckBox
    Friend WithEvents chkSecurity As CheckBox
End Class

