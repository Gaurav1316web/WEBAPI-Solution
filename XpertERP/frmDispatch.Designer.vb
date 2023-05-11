<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDispatch
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance
        Me.RadLabel21 = New common.Controls.MyLabel
        Me.chlCust = New Telerik.WinControls.UI.RadCheckBox
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.txtCostCentre = New common.Controls.MyLabel
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.fndCostCentre = New common.UserControls.txtFinder
        Me.txtGPDate = New common.Controls.MyDateTimePicker
        Me.RadLabel20 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.RadLabel11 = New common.Controls.MyLabel
        Me.txtVehicleNo = New common.Controls.MyTextBox
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.chkNonInventoryItem = New Telerik.WinControls.UI.RadCheckBox
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox
        Me.chkAgainst_Sale = New Telerik.WinControls.UI.RadCheckBox
        Me.lblVendorName = New common.Controls.MyLabel
        Me.lblDepartment = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.lblDeliveredBy = New common.Controls.MyLabel
        Me.txtGPNo = New common.Controls.MyTextBox
        Me.lblLocation = New common.Controls.MyLabel
        Me.txtModeOfTransport = New common.Controls.MyTextBox
        Me.Department = New common.Controls.MyLabel
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.txtCashMemoDetail = New common.Controls.MyTextBox
        Me.txtDepartment = New common.UserControls.txtFinder
        Me.RadLabel15 = New common.Controls.MyLabel
        Me.txtDeliveredBy = New common.UserControls.txtFinder
        Me.txtVendorNo = New common.UserControls.txtFinder
        Me.txtLocation = New common.UserControls.txtFinder
        Me.UsLock1 = New common.usLock
        Me.txtDocNo = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.txtReason = New common.Controls.MyTextBox
        Me.lblBilling = New common.Controls.MyLabel
        Me.ddlBilling = New common.Controls.MyComboBox
        Me.cboDocType = New common.Controls.MyComboBox
        Me.RadLabel29 = New common.Controls.MyLabel
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ERP.ucCustomFields
        Me.lblDocumentAmt = New common.Controls.MyLabel
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.btnReverse = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chlCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCostCentre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNonInventoryItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAgainst_Sale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeliveredBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGPNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Department, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCashMemoDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBilling, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBilling, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.lblDocumentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1050, 517)
        Me.RadPageView1.TabIndex = 39
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.chlCust)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtCostCentre)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.fndCostCentre)
        Me.RadPageViewPage1.Controls.Add(Me.txtGPDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.chkNonInventoryItem)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.chkAgainst_Sale)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.lblDepartment)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblDeliveredBy)
        Me.RadPageViewPage1.Controls.Add(Me.txtGPNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtModeOfTransport)
        Me.RadPageViewPage1.Controls.Add(Me.Department)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtCashMemoDetail)
        Me.RadPageViewPage1.Controls.Add(Me.txtDepartment)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel20)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtDeliveredBy)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtReason)
        Me.RadPageViewPage1.Controls.Add(Me.lblBilling)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.ddlBilling)
        Me.RadPageViewPage1.Controls.Add(Me.cboDocType)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1029, 469)
        Me.RadPageViewPage1.Text = "RGP / NRGP"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(3, 391)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.TabIndex = 56
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'RadLabel21
        '
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(3, 123)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(40, 16)
        Me.RadLabel21.TabIndex = 22
        Me.RadLabel21.Text = "GP No"
        '
        'chlCust
        '
        Me.chlCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chlCust.Location = New System.Drawing.Point(607, 64)
        Me.chlCust.Name = "chlCust"
        Me.chlCust.Size = New System.Drawing.Size(110, 16)
        Me.chlCust.TabIndex = 38
        Me.chlCust.Text = "Against Customer"
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(3, 171)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 25
        Me.RadLabel6.Text = "Remarks"
        '
        'txtCostCentre
        '
        Me.txtCostCentre.AutoSize = False
        Me.txtCostCentre.BorderVisible = True
        Me.txtCostCentre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostCentre.Location = New System.Drawing.Point(250, 102)
        Me.txtCostCentre.Name = "txtCostCentre"
        Me.txtCostCentre.Size = New System.Drawing.Size(242, 18)
        Me.txtCostCentre.TabIndex = 36
        Me.txtCostCentre.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCostCentre.TextWrap = False
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(3, 141)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel3.TabIndex = 24
        Me.RadLabel3.Text = "Reason"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 102)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel2.TabIndex = 37
        Me.MyLabel2.Text = "Material Used For"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 214)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1023, 176)
        Me.RadGroupBox2.TabIndex = 14
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
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(1003, 146)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'fndCostCentre
        '
        Me.fndCostCentre.Location = New System.Drawing.Point(106, 102)
        Me.fndCostCentre.MendatroryField = False
        Me.fndCostCentre.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCostCentre.MyLinkLable1 = Me.MyLabel2
        Me.fndCostCentre.MyLinkLable2 = Me.txtCostCentre
        Me.fndCostCentre.MyReadOnly = False
        Me.fndCostCentre.Name = "fndCostCentre"
        Me.fndCostCentre.Size = New System.Drawing.Size(143, 20)
        Me.fndCostCentre.TabIndex = 35
        Me.fndCostCentre.Value = ""
        '
        'txtGPDate
        '
        Me.txtGPDate.CustomFormat = "dd/MM/yyyy"
        Me.txtGPDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGPDate.Location = New System.Drawing.Point(307, 122)
        Me.txtGPDate.MendatroryField = False
        Me.txtGPDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGPDate.MyLinkLable1 = Me.RadLabel20
        Me.txtGPDate.MyLinkLable2 = Nothing
        Me.txtGPDate.Name = "txtGPDate"
        Me.txtGPDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGPDate.Size = New System.Drawing.Size(77, 18)
        Me.txtGPDate.TabIndex = 11
        Me.txtGPDate.TabStop = False
        Me.txtGPDate.Text = "13/06/2011"
        Me.txtGPDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel20
        '
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(251, 123)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(50, 16)
        Me.RadLabel20.TabIndex = 23
        Me.RadLabel20.Text = "GP Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(498, 123)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel1.TabIndex = 34
        Me.MyLabel1.Text = "Cash Memo Detail"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(382, 4)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 15
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel11
        '
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(498, 104)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel11.TabIndex = 34
        Me.RadLabel11.Text = "Mode Of Transport"
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.Location = New System.Drawing.Point(561, 23)
        Me.txtVehicleNo.MaxLength = 50
        Me.txtVehicleNo.MendatroryField = False
        Me.txtVehicleNo.MyLinkLable1 = Me.RadLabel5
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.Size = New System.Drawing.Size(220, 18)
        Me.txtVehicleNo.TabIndex = 4
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(500, 24)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel5.TabIndex = 18
        Me.RadLabel5.Text = "Vehicle No"
        '
        'chkNonInventoryItem
        '
        Me.chkNonInventoryItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNonInventoryItem.Location = New System.Drawing.Point(500, 85)
        Me.chkNonInventoryItem.Name = "chkNonInventoryItem"
        Me.chkNonInventoryItem.Size = New System.Drawing.Size(116, 16)
        Me.chkNonInventoryItem.TabIndex = 32
        Me.chkNonInventoryItem.Text = "Non Inventory Item"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(721, 44)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 7
        Me.chkOnHold.Text = "On Hold"
        '
        'chkAgainst_Sale
        '
        Me.chkAgainst_Sale.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgainst_Sale.Location = New System.Drawing.Point(500, 65)
        Me.chkAgainst_Sale.Name = "chkAgainst_Sale"
        Me.chkAgainst_Sale.Size = New System.Drawing.Size(84, 16)
        Me.chkAgainst_Sale.TabIndex = 31
        Me.chkAgainst_Sale.Text = "Against Sale"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(249, 23)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(242, 18)
        Me.lblVendorName.TabIndex = 3
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.TextWrap = False
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = False
        Me.lblDepartment.BorderVisible = True
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(250, 83)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(242, 18)
        Me.lblDepartment.TabIndex = 9
        Me.lblDepartment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDepartment.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 24)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 17
        Me.RadLabel2.Text = "Vendor No"
        '
        'lblDeliveredBy
        '
        Me.lblDeliveredBy.AutoSize = False
        Me.lblDeliveredBy.BorderVisible = True
        Me.lblDeliveredBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeliveredBy.Location = New System.Drawing.Point(250, 63)
        Me.lblDeliveredBy.Name = "lblDeliveredBy"
        Me.lblDeliveredBy.Size = New System.Drawing.Size(242, 18)
        Me.lblDeliveredBy.TabIndex = 9
        Me.lblDeliveredBy.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDeliveredBy.TextWrap = False
        '
        'txtGPNo
        '
        Me.txtGPNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGPNo.Location = New System.Drawing.Point(106, 122)
        Me.txtGPNo.MaxLength = 50
        Me.txtGPNo.MendatroryField = False
        Me.txtGPNo.MyLinkLable1 = Me.RadLabel21
        Me.txtGPNo.MyLinkLable2 = Nothing
        Me.txtGPNo.Name = "txtGPNo"
        Me.txtGPNo.Size = New System.Drawing.Size(143, 18)
        Me.txtGPNo.TabIndex = 10
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(250, 43)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(242, 18)
        Me.lblLocation.TabIndex = 6
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
        '
        'txtModeOfTransport
        '
        Me.txtModeOfTransport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModeOfTransport.Location = New System.Drawing.Point(607, 103)
        Me.txtModeOfTransport.MaxLength = 50
        Me.txtModeOfTransport.MendatroryField = False
        Me.txtModeOfTransport.MyLinkLable1 = Nothing
        Me.txtModeOfTransport.MyLinkLable2 = Nothing
        Me.txtModeOfTransport.Name = "txtModeOfTransport"
        Me.txtModeOfTransport.Size = New System.Drawing.Size(173, 18)
        Me.txtModeOfTransport.TabIndex = 10
        '
        'Department
        '
        Me.Department.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Department.Location = New System.Drawing.Point(3, 84)
        Me.Department.Name = "Department"
        Me.Department.Size = New System.Drawing.Size(65, 16)
        Me.Department.TabIndex = 21
        Me.Department.Text = "Department"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(358, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 20)
        Me.btnAddNew.TabIndex = 30
        '
        'RadLabel8
        '
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(3, 64)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(70, 16)
        Me.RadLabel8.TabIndex = 21
        Me.RadLabel8.Text = "Delivered By"
        '
        'txtCashMemoDetail
        '
        Me.txtCashMemoDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashMemoDetail.Location = New System.Drawing.Point(607, 122)
        Me.txtCashMemoDetail.MaxLength = 50
        Me.txtCashMemoDetail.MendatroryField = False
        Me.txtCashMemoDetail.MyLinkLable1 = Nothing
        Me.txtCashMemoDetail.MyLinkLable2 = Nothing
        Me.txtCashMemoDetail.Name = "txtCashMemoDetail"
        Me.txtCashMemoDetail.Size = New System.Drawing.Size(173, 18)
        Me.txtCashMemoDetail.TabIndex = 10
        '
        'txtDepartment
        '
        Me.txtDepartment.Location = New System.Drawing.Point(106, 83)
        Me.txtDepartment.MendatroryField = False
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Me.Department
        Me.txtDepartment.MyLinkLable2 = Me.lblDepartment
        Me.txtDepartment.MyReadOnly = False
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(143, 19)
        Me.txtDepartment.TabIndex = 8
        Me.txtDepartment.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(3, 44)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 19
        Me.RadLabel15.Text = "Location"
        '
        'txtDeliveredBy
        '
        Me.txtDeliveredBy.Location = New System.Drawing.Point(106, 63)
        Me.txtDeliveredBy.MendatroryField = True
        Me.txtDeliveredBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveredBy.MyLinkLable1 = Me.RadLabel8
        Me.txtDeliveredBy.MyLinkLable2 = Me.lblDeliveredBy
        Me.txtDeliveredBy.MyReadOnly = False
        Me.txtDeliveredBy.Name = "txtDeliveredBy"
        Me.txtDeliveredBy.Size = New System.Drawing.Size(143, 19)
        Me.txtDeliveredBy.TabIndex = 8
        Me.txtDeliveredBy.Value = ""
        '
        'txtVendorNo
        '
        Me.txtVendorNo.Location = New System.Drawing.Point(106, 23)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.Size = New System.Drawing.Size(143, 19)
        Me.txtVendorNo.TabIndex = 2
        Me.txtVendorNo.Value = ""
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(106, 43)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(143, 19)
        Me.txtLocation.TabIndex = 5
        Me.txtLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(711, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(70, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 20
        '
        'txtDocNo
        '
        Me.txtDocNo.Location = New System.Drawing.Point(106, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 29
        Me.txtDocNo.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(3, 4)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 28
        Me.RadLabel1.Text = "Document No"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(414, 3)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(106, 178)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(674, 32)
        Me.txtRemarks.TabIndex = 13
        '
        'txtReason
        '
        Me.txtReason.AutoSize = False
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.Location = New System.Drawing.Point(106, 141)
        Me.txtReason.MaxLength = 200
        Me.txtReason.MendatroryField = False
        Me.txtReason.Multiline = True
        Me.txtReason.MyLinkLable1 = Me.RadLabel3
        Me.txtReason.MyLinkLable2 = Nothing
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(674, 32)
        Me.txtReason.TabIndex = 12
        '
        'lblBilling
        '
        Me.lblBilling.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBilling.Location = New System.Drawing.Point(500, 44)
        Me.lblBilling.Name = "lblBilling"
        Me.lblBilling.Size = New System.Drawing.Size(36, 16)
        Me.lblBilling.TabIndex = 16
        Me.lblBilling.Text = "Billing"
        '
        'ddlBilling
        '
        Me.ddlBilling.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlBilling.Location = New System.Drawing.Point(562, 42)
        Me.ddlBilling.MendatroryField = True
        Me.ddlBilling.MyLinkLable1 = Me.lblBilling
        Me.ddlBilling.MyLinkLable2 = Nothing
        Me.ddlBilling.Name = "ddlBilling"
        Me.ddlBilling.Size = New System.Drawing.Size(153, 20)
        Me.ddlBilling.TabIndex = 1
        '
        'cboDocType
        '
        Me.cboDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDocType.Location = New System.Drawing.Point(562, 2)
        Me.cboDocType.MendatroryField = True
        Me.cboDocType.MyLinkLable1 = Me.RadLabel29
        Me.cboDocType.MyLinkLable2 = Nothing
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(145, 20)
        Me.cboDocType.TabIndex = 1
        '
        'RadLabel29
        '
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(500, 4)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel29.TabIndex = 16
        Me.RadLabel29.Text = "Doc. Type"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1029, 469)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1029, 469)
        Me.UcAttachment1.TabIndex = 0
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1029, 469)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1029, 469)
        Me.UcCustomFields1.TabIndex = 1
        '
        'lblDocumentAmt
        '
        Me.lblDocumentAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDocumentAmt.AutoSize = False
        Me.lblDocumentAmt.BorderVisible = True
        Me.lblDocumentAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentAmt.Location = New System.Drawing.Point(927, 531)
        Me.lblDocumentAmt.Name = "lblDocumentAmt"
        Me.lblDocumentAmt.Size = New System.Drawing.Size(118, 16)
        Me.lblDocumentAmt.TabIndex = 27
        Me.lblDocumentAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel7
        '
        Me.RadLabel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(824, 531)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel7.TabIndex = 26
        Me.RadLabel7.Text = "Document Amount"
        '
        'btnReverse
        '
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(294, 4)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(123, 22)
        Me.btnReverse.TabIndex = 137
        Me.btnReverse.Text = "Reverse and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnprint
        '
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(221, 4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(69, 22)
        Me.btnprint.TabIndex = 3
        Me.btnprint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(149, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(977, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocumentAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel7)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1050, 550)
        Me.SplitContainer1.SplitterDistance = 517
        Me.SplitContainer1.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmDispatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 550)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDispatch"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmDispatch"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chlCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCostCentre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNonInventoryItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAgainst_Sale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeliveredBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGPNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Department, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCashMemoDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBilling, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBilling, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.lblDocumentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents chlCust As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtCostCentre As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents fndCostCentre As common.UserControls.txtFinder
    Friend WithEvents txtGPDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.Controls.MyTextBox
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents chkNonInventoryItem As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAgainst_Sale As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblDeliveredBy As common.Controls.MyLabel
    Friend WithEvents txtGPNo As common.Controls.MyTextBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtModeOfTransport As common.Controls.MyTextBox
    Friend WithEvents Department As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents txtCashMemoDetail As common.Controls.MyTextBox
    Friend WithEvents txtDepartment As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtDeliveredBy As common.UserControls.txtFinder
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtReason As common.Controls.MyTextBox
    Friend WithEvents lblBilling As common.Controls.MyLabel
    Friend WithEvents ddlBilling As common.Controls.MyComboBox
    Friend WithEvents cboDocType As common.Controls.MyComboBox
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents lblDocumentAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
End Class

