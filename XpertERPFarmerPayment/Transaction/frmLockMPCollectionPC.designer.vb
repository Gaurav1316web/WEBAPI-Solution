Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLockMPCollectionPC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLockMPCollectionPC))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtLocName = New common.Controls.MyTextBox()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.fndLoc = New common.UserControls.txtFinder()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteVSPBill = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.gv3 = New Telerik.WinControls.UI.MasterGridViewTemplate()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeleteVSPBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadPageView1)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(568, 246)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(10, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(548, 216)
        Me.RadPageView1.TabIndex = 216
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocName)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.fndLoc)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.dtpToDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDescription)
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(527, 168)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 281
        Me.RadLabel1.Text = "Code"
        '
        'txtLocName
        '
        Me.txtLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLocName.CalculationExpression = Nothing
        Me.txtLocName.Enabled = False
        Me.txtLocName.FieldCode = Nothing
        Me.txtLocName.FieldDesc = Nothing
        Me.txtLocName.FieldMaxLength = 0
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.isCalculatedField = False
        Me.txtLocName.IsSourceFromTable = False
        Me.txtLocName.IsSourceFromValueList = False
        Me.txtLocName.IsUnique = False
        Me.txtLocName.Location = New System.Drawing.Point(307, 25)
        Me.txtLocName.MendatroryField = False
        Me.txtLocName.MyLinkLable1 = Nothing
        Me.txtLocName.MyLinkLable2 = Nothing
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.ReferenceFieldDesc = Nothing
        Me.txtLocName.ReferenceFieldName = Nothing
        Me.txtLocName.ReferenceTableName = Nothing
        Me.txtLocName.Size = New System.Drawing.Size(194, 20)
        Me.txtLocName.TabIndex = 294
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(91, 2)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 21)
        Me.txtCode.TabIndex = 282
        Me.txtCode.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(3, 26)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 295
        Me.lblLocation.Text = "Location"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(313, 2)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(21, 21)
        Me.btnNew.TabIndex = 283
        Me.btnNew.Text = " "
        '
        'fndLoc
        '
        Me.fndLoc.CalculationExpression = Nothing
        Me.fndLoc.FieldCode = Nothing
        Me.fndLoc.FieldDesc = Nothing
        Me.fndLoc.FieldMaxLength = 0
        Me.fndLoc.FieldName = Nothing
        Me.fndLoc.isCalculatedField = False
        Me.fndLoc.IsSourceFromTable = False
        Me.fndLoc.IsSourceFromValueList = False
        Me.fndLoc.IsUnique = False
        Me.fndLoc.Location = New System.Drawing.Point(91, 26)
        Me.fndLoc.MendatroryField = True
        Me.fndLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoc.MyLinkLable1 = Me.lblLocation
        Me.fndLoc.MyLinkLable2 = Nothing
        Me.fndLoc.MyReadOnly = False
        Me.fndLoc.MyShowMasterFormButton = False
        Me.fndLoc.Name = "fndLoc"
        Me.fndLoc.ReferenceFieldDesc = Nothing
        Me.fndLoc.ReferenceFieldName = Nothing
        Me.fndLoc.ReferenceTableName = Nothing
        Me.fndLoc.Size = New System.Drawing.Size(210, 19)
        Me.fndLoc.TabIndex = 293
        Me.fndLoc.Value = ""
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(4, 69)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 287
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(366, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 292
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(3, 49)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 288
        Me.RadLabel3.Text = "Description"
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(91, 92)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReadOnly = True
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(142, 18)
        Me.dtpToDate.TabIndex = 286
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "07/06/2016"
        Me.dtpToDate.Value = New Date(2016, 6, 7, 0, 0, 0, 0)
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(91, 48)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel3
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(407, 20)
        Me.txtDescription.TabIndex = 284
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(91, 71)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(142, 18)
        Me.dtpFromDate.TabIndex = 285
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "07/06/2016"
        Me.dtpFromDate.Value = New Date(2016, 6, 7, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(2, 87)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel3.TabIndex = 289
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(3, 92)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 291
        Me.MyLabel1.Text = "To Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(3, 71)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 290
        Me.MyLabel2.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(527, 168)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(527, 168)
        Me.gv.TabIndex = 2
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(499, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 20)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(141, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 20)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 20)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDeleteVSPBill)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(568, 277)
        Me.SplitContainer1.SplitterDistance = 246
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(568, 20)
        Me.RadMenu2.TabIndex = 13
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'btnDeleteVSPBill
        '
        Me.btnDeleteVSPBill.Location = New System.Drawing.Point(210, 3)
        Me.btnDeleteVSPBill.Name = "btnDeleteVSPBill"
        Me.btnDeleteVSPBill.Size = New System.Drawing.Size(118, 20)
        Me.btnDeleteVSPBill.TabIndex = 284
        Me.btnDeleteVSPBill.Text = "Reverse And Unpost"
        Me.btnDeleteVSPBill.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(72, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'gv3
        '
        Me.gv3.ShowHeaderCellButtons = True
        Me.gv3.ViewDefinition = TableViewDefinition2
        '
        'frmLockMPCollectionPC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 277)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLockMPCollectionPC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Lock MP Collection"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeleteVSPBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLocName As common.Controls.MyTextBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndLoc As common.UserControls.txtFinder
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents gv3 As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnDeleteVSPBill As Telerik.WinControls.UI.RadButton
End Class

