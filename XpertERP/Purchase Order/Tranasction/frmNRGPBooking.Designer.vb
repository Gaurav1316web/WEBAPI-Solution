<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNRGPBooking
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lbxCsaVendor = New common.Controls.MyComboBox()
        Me.lblreqtype = New common.Controls.MyLabel()
        Me.ddlReqType = New common.Controls.MyComboBox()
        Me.lbl_location = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtlocation = New common.UserControls.txtFinder()
        Me.rbtn_Item = New common.Controls.MyRadioButton()
        Me.rbtn_group = New common.Controls.MyRadioButton()
        Me.txtCSAName = New common.Controls.MyLabel()
        Me.lblBatchNo = New common.Controls.MyLabel()
        Me.txtBatchNo = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblReceiptCode = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lbxCsaVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblreqtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReqType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_location, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_Item, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_group, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCSAName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(893, 435)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(893, 399)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Booking"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Booking"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lbxCsaVendor)
        Me.RadPageViewPage1.Controls.Add(Me.ddlReqType)
        Me.RadPageViewPage1.Controls.Add(Me.lblreqtype)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_location)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtlocation)
        Me.RadPageViewPage1.Controls.Add(Me.rbtn_Item)
        Me.RadPageViewPage1.Controls.Add(Me.rbtn_group)
        Me.RadPageViewPage1.Controls.Add(Me.txtCSAName)
        Me.RadPageViewPage1.Controls.Add(Me.lblBatchNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtBatchNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblReceiptCode)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.dtpDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(57.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(872, 353)
        Me.RadPageViewPage1.Text = "Booking"
        '
        'lbxCsaVendor
        '
        Me.lbxCsaVendor.CalculationExpression = Nothing
        Me.lbxCsaVendor.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.lbxCsaVendor.FieldCode = Nothing
        Me.lbxCsaVendor.FieldDesc = Nothing
        Me.lbxCsaVendor.FieldMaxLength = 0
        Me.lbxCsaVendor.FieldName = Nothing
        Me.lbxCsaVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbxCsaVendor.isCalculatedField = False
        Me.lbxCsaVendor.IsSourceFromTable = False
        Me.lbxCsaVendor.IsSourceFromValueList = False
        Me.lbxCsaVendor.IsUnique = False
        RadListDataItem1.Tag = "Select Vendor Code"
        RadListDataItem1.Text = "Vendor"
        RadListDataItem2.Text = "CSA"
        Me.lbxCsaVendor.Items.Add(RadListDataItem1)
        Me.lbxCsaVendor.Items.Add(RadListDataItem2)
        Me.lbxCsaVendor.Location = New System.Drawing.Point(255, 25)
        Me.lbxCsaVendor.MendatroryField = True
        Me.lbxCsaVendor.MyLinkLable1 = Me.lblreqtype
        Me.lbxCsaVendor.MyLinkLable2 = Nothing
        Me.lbxCsaVendor.Name = "lbxCsaVendor"
        Me.lbxCsaVendor.ReferenceFieldDesc = Nothing
        Me.lbxCsaVendor.ReferenceFieldName = Nothing
        Me.lbxCsaVendor.ReferenceTableName = Nothing
        Me.lbxCsaVendor.Size = New System.Drawing.Size(71, 18)
        Me.lbxCsaVendor.TabIndex = 27
        '
        'lblreqtype
        '
        Me.lblreqtype.FieldName = Nothing
        Me.lblreqtype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreqtype.Location = New System.Drawing.Point(599, 25)
        Me.lblreqtype.Name = "lblreqtype"
        Me.lblreqtype.Size = New System.Drawing.Size(77, 16)
        Me.lblreqtype.TabIndex = 26
        Me.lblreqtype.Text = "Request Type"
        '
        'ddlReqType
        '
        Me.ddlReqType.CalculationExpression = Nothing
        Me.ddlReqType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlReqType.FieldCode = Nothing
        Me.ddlReqType.FieldDesc = Nothing
        Me.ddlReqType.FieldMaxLength = 0
        Me.ddlReqType.FieldName = Nothing
        Me.ddlReqType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlReqType.isCalculatedField = False
        Me.ddlReqType.IsSourceFromTable = False
        Me.ddlReqType.IsSourceFromValueList = False
        Me.ddlReqType.IsUnique = False
        Me.ddlReqType.Location = New System.Drawing.Point(682, 25)
        Me.ddlReqType.MendatroryField = True
        Me.ddlReqType.MyLinkLable1 = Me.lblreqtype
        Me.ddlReqType.MyLinkLable2 = Nothing
        Me.ddlReqType.Name = "ddlReqType"
        Me.ddlReqType.ReferenceFieldDesc = Nothing
        Me.ddlReqType.ReferenceFieldName = Nothing
        Me.ddlReqType.ReferenceTableName = Nothing
        Me.ddlReqType.Size = New System.Drawing.Size(86, 18)
        Me.ddlReqType.TabIndex = 25
        '
        'lbl_location
        '
        Me.lbl_location.AutoSize = False
        Me.lbl_location.BorderVisible = True
        Me.lbl_location.FieldName = Nothing
        Me.lbl_location.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_location.Location = New System.Drawing.Point(327, 67)
        Me.lbl_location.Name = "lbl_location"
        Me.lbl_location.Size = New System.Drawing.Size(271, 20)
        Me.lbl_location.TabIndex = 24
        Me.lbl_location.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel2
        '
        Me.MyLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(10, 67)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel2.TabIndex = 23
        Me.MyLabel2.Text = "Location"
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(95, 67)
        Me.txtlocation.MendatroryField = True
        Me.txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.MyLinkLable1 = Me.MyLabel2
        Me.txtlocation.MyLinkLable2 = Me.lbl_location
        Me.txtlocation.MyReadOnly = False
        Me.txtlocation.MyShowMasterFormButton = False
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(231, 20)
        Me.txtlocation.TabIndex = 22
        Me.txtlocation.Value = ""
        '
        'rbtn_Item
        '
        Me.rbtn_Item.Location = New System.Drawing.Point(774, 25)
        Me.rbtn_Item.MyLinkLable1 = Nothing
        Me.rbtn_Item.MyLinkLable2 = Nothing
        Me.rbtn_Item.Name = "rbtn_Item"
        Me.rbtn_Item.Size = New System.Drawing.Size(71, 18)
        Me.rbtn_Item.TabIndex = 5
        Me.rbtn_Item.Text = "Item-Wise"
        '
        'rbtn_group
        '
        Me.rbtn_group.Location = New System.Drawing.Point(789, 69)
        Me.rbtn_group.MyLinkLable1 = Nothing
        Me.rbtn_group.MyLinkLable2 = Nothing
        Me.rbtn_group.Name = "rbtn_group"
        Me.rbtn_group.Size = New System.Drawing.Size(80, 18)
        Me.rbtn_group.TabIndex = 4
        Me.rbtn_group.Text = "Group-Wise"
        Me.rbtn_group.Visible = False
        '
        'txtCSAName
        '
        Me.txtCSAName.AutoSize = False
        Me.txtCSAName.BorderVisible = True
        Me.txtCSAName.FieldName = Nothing
        Me.txtCSAName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSAName.Location = New System.Drawing.Point(326, 25)
        Me.txtCSAName.Name = "txtCSAName"
        Me.txtCSAName.Size = New System.Drawing.Size(272, 20)
        Me.txtCSAName.TabIndex = 21
        Me.txtCSAName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBatchNo
        '
        Me.lblBatchNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.lblBatchNo.FieldName = Nothing
        Me.lblBatchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchNo.Location = New System.Drawing.Point(10, 23)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(33, 16)
        Me.lblBatchNo.TabIndex = 17
        Me.lblBatchNo.Text = "Code"
        '
        'txtBatchNo
        '
        Me.txtBatchNo.CalculationExpression = Nothing
        Me.txtBatchNo.FieldCode = Nothing
        Me.txtBatchNo.FieldDesc = Nothing
        Me.txtBatchNo.FieldMaxLength = 0
        Me.txtBatchNo.FieldName = Nothing
        Me.txtBatchNo.isCalculatedField = False
        Me.txtBatchNo.IsSourceFromTable = False
        Me.txtBatchNo.IsSourceFromValueList = False
        Me.txtBatchNo.IsUnique = False
        Me.txtBatchNo.Location = New System.Drawing.Point(95, 25)
        Me.txtBatchNo.MendatroryField = True
        Me.txtBatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.MyLinkLable1 = Me.lblBatchNo
        Me.txtBatchNo.MyLinkLable2 = Nothing
        Me.txtBatchNo.MyReadOnly = False
        Me.txtBatchNo.MyShowMasterFormButton = False
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.ReferenceFieldDesc = Nothing
        Me.txtBatchNo.ReferenceFieldName = Nothing
        Me.txtBatchNo.ReferenceTableName = Nothing
        Me.txtBatchNo.Size = New System.Drawing.Size(154, 19)
        Me.txtBatchNo.TabIndex = 2
        Me.txtBatchNo.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(10, 46)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 11
        Me.RadLabel5.Text = "Description"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 93)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(867, 268)
        Me.RadGroupBox2.TabIndex = 6
        Me.RadGroupBox2.Text = "Item Details"
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
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(847, 238)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(357, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 19
        Me.RadLabel4.Text = "Date"
        '
        'lblReceiptCode
        '
        Me.lblReceiptCode.FieldName = Nothing
        Me.lblReceiptCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceiptCode.Location = New System.Drawing.Point(10, 3)
        Me.lblReceiptCode.Name = "lblReceiptCode"
        Me.lblReceiptCode.Size = New System.Drawing.Size(77, 16)
        Me.lblReceiptCode.TabIndex = 18
        Me.lblReceiptCode.Text = "Booking Code"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(484, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(114, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 20
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(95, 3)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblReceiptCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(230, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'dtpDate
        '
        Me.dtpDate.CalculationExpression = Nothing
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.FieldCode = Nothing
        Me.dtpDate.FieldDesc = Nothing
        Me.dtpDate.FieldMaxLength = 0
        Me.dtpDate.FieldName = Nothing
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.isCalculatedField = False
        Me.dtpDate.IsSourceFromTable = False
        Me.dtpDate.IsSourceFromValueList = False
        Me.dtpDate.IsUnique = False
        Me.dtpDate.Location = New System.Drawing.Point(389, 2)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "13/06/2011"
        Me.dtpDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(95, 46)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(503, 18)
        Me.txtDesc.TabIndex = 3
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(326, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(16, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(872, 372)
        Me.RadPageViewPage2.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(872, 372)
        Me.UcAttachment1.TabIndex = 4
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(230, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(155, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(819, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(893, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiSaveLayout, Me.rmiDeleteLayout})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'rmiSaveLayout
        '
        Me.rmiSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmiSaveLayout.AccessibleName = "Save Layout"
        Me.rmiSaveLayout.Name = "rmiSaveLayout"
        Me.rmiSaveLayout.Text = "Save Layout"
        '
        'rmiDeleteLayout
        '
        Me.rmiDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmiDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmiDeleteLayout.Name = "rmiDeleteLayout"
        Me.rmiDeleteLayout.Text = "Delete Layout"
        '
        'frmNRGPBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(893, 455)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmNRGPBooking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "NRGP Request"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lbxCsaVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblreqtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReqType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_location, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_Item, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_group, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCSAName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceiptCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblReceiptCode As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblBatchNo As common.Controls.MyLabel
    Friend WithEvents txtBatchNo As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtCSAName As common.Controls.MyLabel
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtn_Item As common.Controls.MyRadioButton
    Friend WithEvents rbtn_group As common.Controls.MyRadioButton
    Friend WithEvents lbl_location As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtlocation As common.UserControls.txtFinder
    Friend WithEvents ddlReqType As common.Controls.MyComboBox
    Friend WithEvents lblreqtype As common.Controls.MyLabel
    Friend WithEvents lbxCsaVendor As common.Controls.MyComboBox

End Class

