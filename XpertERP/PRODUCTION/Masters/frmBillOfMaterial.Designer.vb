<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillOfMaterial
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillOfMaterial))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblCode = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvBOM = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnNewFile = New Telerik.WinControls.UI.RadButton
        Me.lblApprovedByName = New common.Controls.MyLabel
        Me.lblApprovedBy = New common.Controls.MyLabel
        Me.lblCreatedByName = New common.Controls.MyLabel
        Me.lblCreatedBy = New common.Controls.MyLabel
        Me.txtMinBatchQty = New common.MyNumBox
        Me.lblMinBatchSize = New common.Controls.MyLabel
        Me.lblUnitName = New common.Controls.MyLabel
        Me.txtBuildQty = New common.MyNumBox
        Me.lblBuildQty = New common.Controls.MyLabel
        Me.lblRevisionNo = New common.Controls.MyLabel
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton
        Me.txtDocPath = New common.Controls.MyTextBox
        Me.lblDocument = New common.Controls.MyLabel
        Me.chkDefaultBOM = New Telerik.WinControls.UI.RadCheckBox
        Me.cboBOMStatus = New common.Controls.MyComboBox
        Me.lblBOMStatus = New common.Controls.MyLabel
        Me.lblBomDesc = New common.Controls.MyLabel
        Me.dtpEndDate = New common.Controls.MyDateTimePicker
        Me.lblEndDate = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.txtDescription = New common.Controls.MyTextBox
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.dtpStartDate = New common.Controls.MyDateTimePicker
        Me.lblStartDate = New common.Controls.MyLabel
        Me.lblMasterItem = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblMasterItemName = New common.Controls.MyLabel
        Me.txtProducedItem = New common.UserControls.txtFinder
        Me.dtpBOMDate = New common.Controls.MyDateTimePicker
        Me.lblBomDate = New common.Controls.MyLabel
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnNewFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinBatchQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(110, 14)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(322, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(20, 40)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(60, 16)
        Me.lblCode.TabIndex = 1
        Me.lblCode.Text = "Bom Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.gvBOM)
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(943, 548)
        Me.RadGroupBox1.TabIndex = 1
        '
        'gvBOM
        '
        Me.gvBOM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvBOM.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvBOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvBOM.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvBOM.ForeColor = System.Drawing.Color.Black
        Me.gvBOM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvBOM.Location = New System.Drawing.Point(13, 190)
        '
        'gvBOM
        '
        Me.gvBOM.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvBOM.MasterTemplate.AllowAddNewRow = False
        Me.gvBOM.MasterTemplate.AutoGenerateColumns = False
        Me.gvBOM.MasterTemplate.EnableGrouping = False
        Me.gvBOM.Name = "gvBOM"
        Me.gvBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBOM.Size = New System.Drawing.Size(917, 290)
        Me.gvBOM.TabIndex = 0
        Me.gvBOM.TabStop = False
        Me.gvBOM.Text = "RadGridView1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNewFile)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApprovedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApprovedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCreatedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCreatedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMinBatchQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMinBatchSize)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblUnitName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBuildQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBuildQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRevisionNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnBrowse)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocPath)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocument)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkDefaultBOM)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboBOMStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBOMStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBomDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpEndDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEndDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMasterItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMasterItemName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtProducedItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpBOMDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBomDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(923, 518)
        Me.SplitContainer1.SplitterDistance = 468
        Me.SplitContainer1.TabIndex = 1
        '
        'btnNewFile
        '
        Me.btnNewFile.Image = CType(resources.GetObject("btnNewFile.Image"), System.Drawing.Image)
        Me.btnNewFile.Location = New System.Drawing.Point(833, 147)
        Me.btnNewFile.Name = "btnNewFile"
        Me.btnNewFile.Size = New System.Drawing.Size(14, 20)
        Me.btnNewFile.TabIndex = 12
        Me.btnNewFile.Text = " "
        '
        'lblApprovedByName
        '
        Me.lblApprovedByName.AutoSize = False
        Me.lblApprovedByName.BorderVisible = True
        Me.lblApprovedByName.Location = New System.Drawing.Point(657, 127)
        Me.lblApprovedByName.Name = "lblApprovedByName"
        Me.lblApprovedByName.Size = New System.Drawing.Size(260, 19)
        Me.lblApprovedByName.TabIndex = 18
        Me.lblApprovedByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblApprovedBy.Location = New System.Drawing.Point(566, 129)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(71, 16)
        Me.lblApprovedBy.TabIndex = 17
        Me.lblApprovedBy.Text = "Approved By"
        '
        'lblCreatedByName
        '
        Me.lblCreatedByName.AutoSize = False
        Me.lblCreatedByName.BorderVisible = True
        Me.lblCreatedByName.Location = New System.Drawing.Point(657, 107)
        Me.lblCreatedByName.Name = "lblCreatedByName"
        Me.lblCreatedByName.Size = New System.Drawing.Size(260, 19)
        Me.lblCreatedByName.TabIndex = 19
        Me.lblCreatedByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCreatedBy.Location = New System.Drawing.Point(566, 107)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(63, 16)
        Me.lblCreatedBy.TabIndex = 20
        Me.lblCreatedBy.Text = "Created By"
        '
        'txtMinBatchQty
        '
        Me.txtMinBatchQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMinBatchQty.DecimalPlaces = 0
        Me.txtMinBatchQty.Location = New System.Drawing.Point(657, 84)
        Me.txtMinBatchQty.MendatroryField = True
        Me.txtMinBatchQty.MyLinkLable1 = Me.lblMinBatchSize
        Me.txtMinBatchQty.MyLinkLable2 = Nothing
        Me.txtMinBatchQty.Name = "txtMinBatchQty"
        Me.txtMinBatchQty.Size = New System.Drawing.Size(129, 20)
        Me.txtMinBatchQty.TabIndex = 6
        Me.txtMinBatchQty.Text = "0"
        Me.txtMinBatchQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinBatchQty.Value = 0
        '
        'lblMinBatchSize
        '
        Me.lblMinBatchSize.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMinBatchSize.Location = New System.Drawing.Point(566, 86)
        Me.lblMinBatchSize.Name = "lblMinBatchSize"
        Me.lblMinBatchSize.Size = New System.Drawing.Size(85, 16)
        Me.lblMinBatchSize.TabIndex = 23
        Me.lblMinBatchSize.Text = "Min. Batch Size"
        '
        'lblUnitName
        '
        Me.lblUnitName.AutoSize = False
        Me.lblUnitName.BorderVisible = True
        Me.lblUnitName.Location = New System.Drawing.Point(791, 62)
        Me.lblUnitName.Name = "lblUnitName"
        Me.lblUnitName.Size = New System.Drawing.Size(128, 19)
        Me.lblUnitName.TabIndex = 25
        Me.lblUnitName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBuildQty
        '
        Me.txtBuildQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBuildQty.DecimalPlaces = 0
        Me.txtBuildQty.Location = New System.Drawing.Point(657, 61)
        Me.txtBuildQty.MendatroryField = True
        Me.txtBuildQty.MyLinkLable1 = Me.lblBuildQty
        Me.txtBuildQty.MyLinkLable2 = Nothing
        Me.txtBuildQty.Name = "txtBuildQty"
        Me.txtBuildQty.Size = New System.Drawing.Size(129, 20)
        Me.txtBuildQty.TabIndex = 4
        Me.txtBuildQty.Text = "0"
        Me.txtBuildQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBuildQty.Value = 0
        '
        'lblBuildQty
        '
        Me.lblBuildQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBuildQty.Location = New System.Drawing.Point(566, 63)
        Me.lblBuildQty.Name = "lblBuildQty"
        Me.lblBuildQty.Size = New System.Drawing.Size(77, 16)
        Me.lblBuildQty.TabIndex = 24
        Me.lblBuildQty.Text = "Build Quantity"
        '
        'lblRevisionNo
        '
        Me.lblRevisionNo.AutoSize = False
        Me.lblRevisionNo.BorderVisible = True
        Me.lblRevisionNo.Location = New System.Drawing.Point(338, 83)
        Me.lblRevisionNo.Name = "lblRevisionNo"
        Me.lblRevisionNo.Size = New System.Drawing.Size(218, 19)
        Me.lblRevisionNo.TabIndex = 26
        Me.lblRevisionNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(851, 149)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnBrowse.TabIndex = 13
        Me.btnBrowse.Text = "Browse"
        '
        'txtDocPath
        '
        Me.txtDocPath.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocPath.Location = New System.Drawing.Point(657, 148)
        Me.txtDocPath.MaxLength = 49
        Me.txtDocPath.MendatroryField = False
        Me.txtDocPath.MyLinkLable1 = Me.lblDocument
        Me.txtDocPath.MyLinkLable2 = Nothing
        Me.txtDocPath.Name = "txtDocPath"
        Me.txtDocPath.Size = New System.Drawing.Size(173, 18)
        Me.txtDocPath.TabIndex = 11
        '
        'lblDocument
        '
        Me.lblDocument.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocument.Location = New System.Drawing.Point(566, 149)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(58, 16)
        Me.lblDocument.TabIndex = 15
        Me.lblDocument.Text = "Document"
        '
        'chkDefaultBOM
        '
        Me.chkDefaultBOM.Location = New System.Drawing.Point(338, 148)
        Me.chkDefaultBOM.Name = "chkDefaultBOM"
        Me.chkDefaultBOM.Size = New System.Drawing.Size(96, 18)
        Me.chkDefaultBOM.TabIndex = 10
        Me.chkDefaultBOM.Text = "Is Default BOM"
        '
        'cboBOMStatus
        '
        Me.cboBOMStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Open"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Approved"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "On Hold"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Discountinued"
        RadListDataItem4.TextWrap = True
        Me.cboBOMStatus.Items.Add(RadListDataItem1)
        Me.cboBOMStatus.Items.Add(RadListDataItem2)
        Me.cboBOMStatus.Items.Add(RadListDataItem3)
        Me.cboBOMStatus.Items.Add(RadListDataItem4)
        Me.cboBOMStatus.Location = New System.Drawing.Point(110, 148)
        Me.cboBOMStatus.MendatroryField = True
        Me.cboBOMStatus.MyLinkLable1 = Me.lblBOMStatus
        Me.cboBOMStatus.MyLinkLable2 = Nothing
        Me.cboBOMStatus.Name = "cboBOMStatus"
        Me.cboBOMStatus.Size = New System.Drawing.Size(219, 18)
        Me.cboBOMStatus.TabIndex = 9
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(11, 148)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(65, 16)
        Me.lblBOMStatus.TabIndex = 14
        Me.lblBOMStatus.Text = "Bom Status"
        '
        'lblBomDesc
        '
        Me.lblBomDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDesc.Location = New System.Drawing.Point(10, 40)
        Me.lblBomDesc.Name = "lblBomDesc"
        Me.lblBomDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblBomDesc.TabIndex = 31
        Me.lblBomDesc.Text = "Description"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(110, 127)
        Me.dtpEndDate.MendatroryField = False
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.MyLinkLable1 = Me.lblEndDate
        Me.dtpEndDate.MyLinkLable2 = Nothing
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpEndDate.TabIndex = 8
        Me.dtpEndDate.TabStop = False
        Me.dtpEndDate.Text = "03/05/2011"
        Me.dtpEndDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblEndDate
        '
        Me.lblEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.Location = New System.Drawing.Point(12, 129)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(53, 16)
        Me.lblEndDate.TabIndex = 16
        Me.lblEndDate.Text = "End Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(458, 15)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 29
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(110, 38)
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.MyLinkLable1 = Me.lblBomDesc
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(807, 20)
        Me.txtDescription.TabIndex = 2
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(438, 15)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(110, 105)
        Me.dtpStartDate.MendatroryField = False
        Me.dtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.MyLinkLable1 = Me.lblStartDate
        Me.dtpStartDate.MyLinkLable2 = Nothing
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpStartDate.TabIndex = 7
        Me.dtpStartDate.TabStop = False
        Me.dtpStartDate.Text = "03/05/2011"
        Me.dtpStartDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblStartDate
        '
        Me.lblStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(11, 107)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(57, 16)
        Me.lblStartDate.TabIndex = 21
        Me.lblStartDate.Text = "Start Date"
        '
        'lblMasterItem
        '
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(12, 63)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(57, 18)
        Me.lblMasterItem.TabIndex = 30
        Me.lblMasterItem.Text = "Main Item"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(262, 86)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel1.TabIndex = 28
        Me.MyLabel1.Text = "Revision No"
        '
        'lblMasterItemName
        '
        Me.lblMasterItemName.AutoSize = False
        Me.lblMasterItemName.BorderVisible = True
        Me.lblMasterItemName.Location = New System.Drawing.Point(338, 62)
        Me.lblMasterItemName.Name = "lblMasterItemName"
        Me.lblMasterItemName.Size = New System.Drawing.Size(218, 19)
        Me.lblMasterItemName.TabIndex = 27
        Me.lblMasterItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtProducedItem
        '
        Me.txtProducedItem.Location = New System.Drawing.Point(110, 62)
        Me.txtProducedItem.MendatroryField = True
        Me.txtProducedItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducedItem.MyLinkLable1 = Me.lblMasterItem
        Me.txtProducedItem.MyLinkLable2 = Nothing
        Me.txtProducedItem.MyReadOnly = False
        Me.txtProducedItem.Name = "txtProducedItem"
        Me.txtProducedItem.Size = New System.Drawing.Size(219, 19)
        Me.txtProducedItem.TabIndex = 3
        Me.txtProducedItem.Value = ""
        '
        'dtpBOMDate
        '
        Me.dtpBOMDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBOMDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBOMDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBOMDate.Location = New System.Drawing.Point(110, 84)
        Me.dtpBOMDate.MendatroryField = True
        Me.dtpBOMDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpBOMDate.MyLinkLable2 = Nothing
        Me.dtpBOMDate.Name = "dtpBOMDate"
        Me.dtpBOMDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBOMDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpBOMDate.TabIndex = 5
        Me.dtpBOMDate.TabStop = False
        Me.dtpBOMDate.Text = "03/05/2011"
        Me.dtpBOMDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblBomDate
        '
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(12, 86)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(59, 16)
        Me.lblBomDate.TabIndex = 22
        Me.lblBomDate.Text = "BOM Date"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 13)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(156, 13)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(851, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(943, 20)
        Me.RadMenu2.TabIndex = 1
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuItemImport
        '
        Me.MenuItemImport.AccessibleDescription = "Import"
        Me.MenuItemImport.AccessibleName = "Import"
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        Me.MenuItemImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuItemExport
        '
        Me.MenuItemExport.AccessibleDescription = "Export"
        Me.MenuItemExport.AccessibleName = "Export"
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        Me.MenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'MenuItemClose
        '
        Me.MenuItemClose.AccessibleDescription = "Close"
        Me.MenuItemClose.AccessibleName = "Close"
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        Me.MenuItemClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmBillOfMaterial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 548)
        Me.Controls.Add(Me.RadMenu2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmBillOfMaterial"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bill of Material"
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.gvBOM.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnNewFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinBatchQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinBatchSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultBOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBOMDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtProducedItem As common.UserControls.txtFinder
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpBOMDate As common.Controls.MyDateTimePicker
    Friend WithEvents gvBOM As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblMasterItemName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents dtpEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblEndDate As common.Controls.MyLabel
    Friend WithEvents dtpStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblStartDate As common.Controls.MyLabel
    Friend WithEvents lblBomDesc As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents cboBOMStatus As common.Controls.MyComboBox
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents chkDefaultBOM As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblDocument As common.Controls.MyLabel
    Friend WithEvents btnBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocPath As common.Controls.MyTextBox
    Friend WithEvents lblRevisionNo As common.Controls.MyLabel
    Friend WithEvents lblBuildQty As common.Controls.MyLabel
    Friend WithEvents txtBuildQty As common.MyNumBox
    Friend WithEvents lblUnitName As common.Controls.MyLabel
    Friend WithEvents txtMinBatchQty As common.MyNumBox
    Friend WithEvents lblMinBatchSize As common.Controls.MyLabel
    Friend WithEvents lblApprovedByName As common.Controls.MyLabel
    Friend WithEvents lblApprovedBy As common.Controls.MyLabel
    Friend WithEvents lblCreatedByName As common.Controls.MyLabel
    Friend WithEvents lblCreatedBy As common.Controls.MyLabel
    Friend WithEvents btnNewFile As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class
