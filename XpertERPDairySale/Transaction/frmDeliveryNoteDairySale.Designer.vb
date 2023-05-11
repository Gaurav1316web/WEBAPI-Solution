<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeliveryNoteDairySale
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblShipToLocation = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtShipToLocation = New common.UserControls.txtFinder()
        Me.lblVehicleNo = New common.Controls.MyTextBox()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.TxtRouteNo = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtLorryNo = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblTaxGrp = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtPriceCode = New common.Controls.MyLabel()
        Me.txtCreditLimit = New common.MyNumBox()
        Me.lblPriceCode = New common.Controls.MyLabel()
        Me.lblCreditLimit = New common.Controls.MyLabel()
        Me.chkShortClose = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.lblRoadPermit = New common.Controls.MyLabel()
        Me.txtRoadPermitNo = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtTransporterName = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.ddlStatus = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.txtVehicleCapacity = New common.Controls.MyTextBox()
        Me.txtLorryNo1 = New common.Controls.MyTextBox()
        Me.lblBookingDate = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.ddlFreight = New common.Controls.MyComboBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtFreightAmt = New common.MyNumBox()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.fndCustomerNo = New common.UserControls.txtFinder()
        Me.fndBookingNo = New common.UserControls.txtFinder()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnUpdateVehicle = New Telerik.WinControls.UI.RadButton()
        Me.btnApproveCreditLimit = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.BtnPreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSendForApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.EmailSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.EmailSettingCreditApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShortClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoadPermit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoadPermitNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLorryNo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBookingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreightAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.btnUpdateVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnApproveCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdateVehicle)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnApproveCreditLimit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1025, 501)
        Me.SplitContainer1.SplitterDistance = 469
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1025, 469)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.lblRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtLorryNo)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.lblTaxGrp)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtPriceCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtCreditLimit)
        Me.RadPageViewPage1.Controls.Add(Me.lblPriceCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblCreditLimit)
        Me.RadPageViewPage1.Controls.Add(Me.chkShortClose)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.lblRoadPermit)
        Me.RadPageViewPage1.Controls.Add(Me.txtRoadPermitNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporterName)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.ddlStatus)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleCapacity)
        Me.RadPageViewPage1.Controls.Add(Me.txtLorryNo1)
        Me.RadPageViewPage1.Controls.Add(Me.lblBookingDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.fndCustomerNo)
        Me.RadPageViewPage1.Controls.Add(Me.fndBookingNo)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1004, 423)
        Me.RadPageViewPage1.Text = "Delivery Note"
        '
        'lblShipToLocation
        '
        Me.lblShipToLocation.AutoSize = False
        Me.lblShipToLocation.BorderVisible = True
        Me.lblShipToLocation.Enabled = False
        Me.lblShipToLocation.FieldName = Nothing
        Me.lblShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipToLocation.Location = New System.Drawing.Point(242, 131)
        Me.lblShipToLocation.Name = "lblShipToLocation"
        Me.lblShipToLocation.Size = New System.Drawing.Size(287, 18)
        Me.lblShipToLocation.TabIndex = 1495
        Me.lblShipToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShipToLocation.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(2, 131)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel3.TabIndex = 1496
        Me.MyLabel3.Text = "Ship To Location"
        '
        'txtShipToLocation
        '
        Me.txtShipToLocation.CalculationExpression = Nothing
        Me.txtShipToLocation.FieldCode = Nothing
        Me.txtShipToLocation.FieldDesc = Nothing
        Me.txtShipToLocation.FieldMaxLength = 0
        Me.txtShipToLocation.FieldName = Nothing
        Me.txtShipToLocation.isCalculatedField = False
        Me.txtShipToLocation.IsSourceFromTable = False
        Me.txtShipToLocation.IsSourceFromValueList = False
        Me.txtShipToLocation.IsUnique = False
        Me.txtShipToLocation.Location = New System.Drawing.Point(98, 130)
        Me.txtShipToLocation.MendatroryField = False
        Me.txtShipToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipToLocation.MyLinkLable1 = Me.MyLabel3
        Me.txtShipToLocation.MyLinkLable2 = Me.lblShipToLocation
        Me.txtShipToLocation.MyReadOnly = False
        Me.txtShipToLocation.MyShowMasterFormButton = False
        Me.txtShipToLocation.Name = "txtShipToLocation"
        Me.txtShipToLocation.ReferenceFieldDesc = Nothing
        Me.txtShipToLocation.ReferenceFieldName = Nothing
        Me.txtShipToLocation.ReferenceTableName = Nothing
        Me.txtShipToLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtShipToLocation.TabIndex = 1494
        Me.txtShipToLocation.Value = ""
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.CalculationExpression = Nothing
        Me.lblVehicleNo.FieldCode = Nothing
        Me.lblVehicleNo.FieldDesc = Nothing
        Me.lblVehicleNo.FieldMaxLength = 0
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.isCalculatedField = False
        Me.lblVehicleNo.IsSourceFromTable = False
        Me.lblVehicleNo.IsSourceFromValueList = False
        Me.lblVehicleNo.IsUnique = False
        Me.lblVehicleNo.Location = New System.Drawing.Point(810, 44)
        Me.lblVehicleNo.MaxLength = 200
        Me.lblVehicleNo.MendatroryField = False
        Me.lblVehicleNo.MyLinkLable1 = Nothing
        Me.lblVehicleNo.MyLinkLable2 = Nothing
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.ReferenceFieldDesc = Nothing
        Me.lblVehicleNo.ReferenceFieldName = Nothing
        Me.lblVehicleNo.ReferenceTableName = Nothing
        Me.lblVehicleNo.Size = New System.Drawing.Size(178, 18)
        Me.lblVehicleNo.TabIndex = 149
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 346)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.TabIndex = 141
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'lblRouteNo
        '
        Me.lblRouteNo.AutoSize = False
        Me.lblRouteNo.BorderVisible = True
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteNo.Location = New System.Drawing.Point(810, 22)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(178, 18)
        Me.lblRouteNo.TabIndex = 140
        Me.lblRouteNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRouteNo.TextWrap = False
        '
        'TxtRouteNo
        '
        Me.TxtRouteNo.CalculationExpression = Nothing
        Me.TxtRouteNo.FieldCode = Nothing
        Me.TxtRouteNo.FieldDesc = Nothing
        Me.TxtRouteNo.FieldMaxLength = 0
        Me.TxtRouteNo.FieldName = Nothing
        Me.TxtRouteNo.isCalculatedField = False
        Me.TxtRouteNo.IsSourceFromTable = False
        Me.TxtRouteNo.IsSourceFromValueList = False
        Me.TxtRouteNo.IsUnique = False
        Me.TxtRouteNo.Location = New System.Drawing.Point(689, 21)
        Me.TxtRouteNo.MendatroryField = False
        Me.TxtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRouteNo.MyLinkLable1 = Me.MyLabel6
        Me.TxtRouteNo.MyLinkLable2 = Me.lblRouteNo
        Me.TxtRouteNo.MyReadOnly = False
        Me.TxtRouteNo.MyShowMasterFormButton = False
        Me.TxtRouteNo.Name = "TxtRouteNo"
        Me.TxtRouteNo.ReferenceFieldDesc = Nothing
        Me.TxtRouteNo.ReferenceFieldName = Nothing
        Me.TxtRouteNo.ReferenceTableName = Nothing
        Me.TxtRouteNo.Size = New System.Drawing.Size(106, 20)
        Me.TxtRouteNo.TabIndex = 139
        Me.TxtRouteNo.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(544, 23)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel6.TabIndex = 138
        Me.MyLabel6.Text = "Route No"
        '
        'txtLorryNo
        '
        Me.txtLorryNo.CalculationExpression = Nothing
        Me.txtLorryNo.FieldCode = Nothing
        Me.txtLorryNo.FieldDesc = Nothing
        Me.txtLorryNo.FieldMaxLength = 0
        Me.txtLorryNo.FieldName = Nothing
        Me.txtLorryNo.isCalculatedField = False
        Me.txtLorryNo.IsSourceFromTable = False
        Me.txtLorryNo.IsSourceFromValueList = False
        Me.txtLorryNo.IsUnique = False
        Me.txtLorryNo.Location = New System.Drawing.Point(690, 44)
        Me.txtLorryNo.MendatroryField = False
        Me.txtLorryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLorryNo.MyLinkLable1 = Me.RadLabel5
        Me.txtLorryNo.MyLinkLable2 = Nothing
        Me.txtLorryNo.MyReadOnly = False
        Me.txtLorryNo.MyShowMasterFormButton = False
        Me.txtLorryNo.Name = "txtLorryNo"
        Me.txtLorryNo.ReferenceFieldDesc = Nothing
        Me.txtLorryNo.ReferenceFieldName = Nothing
        Me.txtLorryNo.ReferenceTableName = Nothing
        Me.txtLorryNo.Size = New System.Drawing.Size(105, 19)
        Me.txtLorryNo.TabIndex = 136
        Me.txtLorryNo.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(544, 45)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel5.TabIndex = 34
        Me.RadLabel5.Text = "Vehicle No"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(878, -2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 135
        '
        'lblTaxGrp
        '
        Me.lblTaxGrp.AutoSize = False
        Me.lblTaxGrp.BorderVisible = True
        Me.lblTaxGrp.FieldName = Nothing
        Me.lblTaxGrp.Location = New System.Drawing.Point(355, 111)
        Me.lblTaxGrp.Name = "lblTaxGrp"
        Me.lblTaxGrp.Size = New System.Drawing.Size(139, 19)
        Me.lblTaxGrp.TabIndex = 133
        Me.lblTaxGrp.TextWrap = False
        Me.lblTaxGrp.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(258, 112)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel7.TabIndex = 134
        Me.MyLabel7.Text = "Tax Group"
        Me.MyLabel7.Visible = False
        '
        'txtPriceCode
        '
        Me.txtPriceCode.AutoSize = False
        Me.txtPriceCode.BorderVisible = True
        Me.txtPriceCode.FieldName = Nothing
        Me.txtPriceCode.Location = New System.Drawing.Point(97, 111)
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(139, 19)
        Me.txtPriceCode.TabIndex = 122
        Me.txtPriceCode.TextWrap = False
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCreditLimit.CalculationExpression = Nothing
        Me.txtCreditLimit.DecimalPlaces = 5
        Me.txtCreditLimit.Enabled = False
        Me.txtCreditLimit.FieldCode = Nothing
        Me.txtCreditLimit.FieldDesc = Nothing
        Me.txtCreditLimit.FieldMaxLength = 0
        Me.txtCreditLimit.FieldName = Nothing
        Me.txtCreditLimit.isCalculatedField = False
        Me.txtCreditLimit.IsSourceFromTable = False
        Me.txtCreditLimit.IsSourceFromValueList = False
        Me.txtCreditLimit.IsUnique = False
        Me.txtCreditLimit.Location = New System.Drawing.Point(691, 113)
        Me.txtCreditLimit.MendatroryField = True
        Me.txtCreditLimit.MyLinkLable1 = Nothing
        Me.txtCreditLimit.MyLinkLable2 = Nothing
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.ReferenceFieldDesc = Nothing
        Me.txtCreditLimit.ReferenceFieldName = Nothing
        Me.txtCreditLimit.ReferenceTableName = Nothing
        Me.txtCreditLimit.Size = New System.Drawing.Size(105, 20)
        Me.txtCreditLimit.TabIndex = 132
        Me.txtCreditLimit.Text = "0"
        Me.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCreditLimit.Value = 0R
        '
        'lblPriceCode
        '
        Me.lblPriceCode.FieldName = Nothing
        Me.lblPriceCode.Location = New System.Drawing.Point(0, 112)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceCode.TabIndex = 123
        Me.lblPriceCode.Text = "Price Code"
        '
        'lblCreditLimit
        '
        Me.lblCreditLimit.FieldName = Nothing
        Me.lblCreditLimit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditLimit.Location = New System.Drawing.Point(544, 115)
        Me.lblCreditLimit.Name = "lblCreditLimit"
        Me.lblCreditLimit.Size = New System.Drawing.Size(63, 16)
        Me.lblCreditLimit.TabIndex = 131
        Me.lblCreditLimit.Text = "Credit Limit"
        '
        'chkShortClose
        '
        Me.chkShortClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShortClose.Location = New System.Drawing.Point(689, 3)
        Me.chkShortClose.Name = "chkShortClose"
        Me.chkShortClose.Size = New System.Drawing.Size(79, 16)
        Me.chkShortClose.TabIndex = 130
        Me.chkShortClose.Text = "Short Close"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(543, 3)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(59, 16)
        Me.chkOnHold.TabIndex = 3
        Me.chkOnHold.Text = "OnHold"
        Me.chkOnHold.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(544, 92)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel5.TabIndex = 128
        Me.MyLabel5.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(690, 91)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(106, 18)
        Me.lblTotRAmt1.TabIndex = 127
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRoadPermit
        '
        Me.lblRoadPermit.FieldName = Nothing
        Me.lblRoadPermit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoadPermit.Location = New System.Drawing.Point(879, 154)
        Me.lblRoadPermit.Name = "lblRoadPermit"
        Me.lblRoadPermit.Size = New System.Drawing.Size(87, 16)
        Me.lblRoadPermit.TabIndex = 54
        Me.lblRoadPermit.Text = "Road Permit No"
        Me.lblRoadPermit.Visible = False
        '
        'txtRoadPermitNo
        '
        Me.txtRoadPermitNo.CalculationExpression = Nothing
        Me.txtRoadPermitNo.FieldCode = Nothing
        Me.txtRoadPermitNo.FieldDesc = Nothing
        Me.txtRoadPermitNo.FieldMaxLength = 0
        Me.txtRoadPermitNo.FieldName = Nothing
        Me.txtRoadPermitNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoadPermitNo.isCalculatedField = False
        Me.txtRoadPermitNo.IsSourceFromTable = False
        Me.txtRoadPermitNo.IsSourceFromValueList = False
        Me.txtRoadPermitNo.IsUnique = False
        Me.txtRoadPermitNo.Location = New System.Drawing.Point(970, 153)
        Me.txtRoadPermitNo.MaxLength = 50
        Me.txtRoadPermitNo.MendatroryField = False
        Me.txtRoadPermitNo.MyLinkLable1 = Me.lblRoadPermit
        Me.txtRoadPermitNo.MyLinkLable2 = Nothing
        Me.txtRoadPermitNo.Name = "txtRoadPermitNo"
        Me.txtRoadPermitNo.ReferenceFieldDesc = Nothing
        Me.txtRoadPermitNo.ReferenceFieldName = Nothing
        Me.txtRoadPermitNo.ReferenceTableName = Nothing
        Me.txtRoadPermitNo.Size = New System.Drawing.Size(25, 18)
        Me.txtRoadPermitNo.TabIndex = 7
        Me.txtRoadPermitNo.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(543, 70)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(98, 16)
        Me.MyLabel2.TabIndex = 54
        Me.MyLabel2.Text = "Transporter Name"
        '
        'txtTransporterName
        '
        Me.txtTransporterName.CalculationExpression = Nothing
        Me.txtTransporterName.FieldCode = Nothing
        Me.txtTransporterName.FieldDesc = Nothing
        Me.txtTransporterName.FieldMaxLength = 0
        Me.txtTransporterName.FieldName = Nothing
        Me.txtTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterName.isCalculatedField = False
        Me.txtTransporterName.IsSourceFromTable = False
        Me.txtTransporterName.IsSourceFromValueList = False
        Me.txtTransporterName.IsUnique = False
        Me.txtTransporterName.Location = New System.Drawing.Point(689, 69)
        Me.txtTransporterName.MaxLength = 50
        Me.txtTransporterName.MendatroryField = False
        Me.txtTransporterName.MyLinkLable1 = Me.MyLabel2
        Me.txtTransporterName.MyLinkLable2 = Nothing
        Me.txtTransporterName.Name = "txtTransporterName"
        Me.txtTransporterName.ReferenceFieldDesc = Nothing
        Me.txtTransporterName.ReferenceFieldName = Nothing
        Me.txtTransporterName.ReferenceTableName = Nothing
        Me.txtTransporterName.Size = New System.Drawing.Size(299, 18)
        Me.txtTransporterName.TabIndex = 9
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
        Me.txtDate.Location = New System.Drawing.Point(406, 1)
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
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(375, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 40
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(809, 92)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel29.TabIndex = 43
        Me.RadLabel29.Text = "Status"
        Me.RadLabel29.Visible = False
        '
        'ddlStatus
        '
        Me.ddlStatus.AutoCompleteDisplayMember = Nothing
        Me.ddlStatus.AutoCompleteValueMember = Nothing
        Me.ddlStatus.CalculationExpression = Nothing
        Me.ddlStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlStatus.FieldCode = Nothing
        Me.ddlStatus.FieldDesc = Nothing
        Me.ddlStatus.FieldMaxLength = 0
        Me.ddlStatus.FieldName = Nothing
        Me.ddlStatus.isCalculatedField = False
        Me.ddlStatus.IsSourceFromTable = False
        Me.ddlStatus.IsSourceFromValueList = False
        Me.ddlStatus.IsUnique = False
        Me.ddlStatus.Location = New System.Drawing.Point(878, 90)
        Me.ddlStatus.MendatroryField = True
        Me.ddlStatus.MyLinkLable1 = Me.RadLabel29
        Me.ddlStatus.MyLinkLable2 = Nothing
        Me.ddlStatus.Name = "ddlStatus"
        Me.ddlStatus.ReferenceFieldDesc = Nothing
        Me.ddlStatus.ReferenceFieldName = Nothing
        Me.ddlStatus.ReferenceTableName = Nothing
        Me.ddlStatus.Size = New System.Drawing.Size(110, 20)
        Me.ddlStatus.TabIndex = 2
        Me.ddlStatus.Visible = False
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(562, 151)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel8.TabIndex = 52
        Me.RadLabel8.Text = "Vehicle Capacity Required"
        Me.RadLabel8.Visible = False
        '
        'txtVehicleCapacity
        '
        Me.txtVehicleCapacity.CalculationExpression = Nothing
        Me.txtVehicleCapacity.FieldCode = Nothing
        Me.txtVehicleCapacity.FieldDesc = Nothing
        Me.txtVehicleCapacity.FieldMaxLength = 0
        Me.txtVehicleCapacity.FieldName = Nothing
        Me.txtVehicleCapacity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCapacity.isCalculatedField = False
        Me.txtVehicleCapacity.IsSourceFromTable = False
        Me.txtVehicleCapacity.IsSourceFromValueList = False
        Me.txtVehicleCapacity.IsUnique = False
        Me.txtVehicleCapacity.Location = New System.Drawing.Point(708, 150)
        Me.txtVehicleCapacity.MaxLength = 50
        Me.txtVehicleCapacity.MendatroryField = False
        Me.txtVehicleCapacity.MyLinkLable1 = Me.RadLabel8
        Me.txtVehicleCapacity.MyLinkLable2 = Nothing
        Me.txtVehicleCapacity.Name = "txtVehicleCapacity"
        Me.txtVehicleCapacity.ReferenceFieldDesc = Nothing
        Me.txtVehicleCapacity.ReferenceFieldName = Nothing
        Me.txtVehicleCapacity.ReferenceTableName = Nothing
        Me.txtVehicleCapacity.Size = New System.Drawing.Size(106, 18)
        Me.txtVehicleCapacity.TabIndex = 11
        Me.txtVehicleCapacity.Visible = False
        '
        'txtLorryNo1
        '
        Me.txtLorryNo1.CalculationExpression = Nothing
        Me.txtLorryNo1.FieldCode = Nothing
        Me.txtLorryNo1.FieldDesc = Nothing
        Me.txtLorryNo1.FieldMaxLength = 0
        Me.txtLorryNo1.FieldName = Nothing
        Me.txtLorryNo1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLorryNo1.isCalculatedField = False
        Me.txtLorryNo1.IsSourceFromTable = False
        Me.txtLorryNo1.IsSourceFromValueList = False
        Me.txtLorryNo1.IsUnique = False
        Me.txtLorryNo1.Location = New System.Drawing.Point(970, 134)
        Me.txtLorryNo1.MaxLength = 50
        Me.txtLorryNo1.MendatroryField = False
        Me.txtLorryNo1.MyLinkLable1 = Me.RadLabel5
        Me.txtLorryNo1.MyLinkLable2 = Nothing
        Me.txtLorryNo1.Name = "txtLorryNo1"
        Me.txtLorryNo1.ReferenceFieldDesc = Nothing
        Me.txtLorryNo1.ReferenceFieldName = Nothing
        Me.txtLorryNo1.ReferenceTableName = Nothing
        Me.txtLorryNo1.Size = New System.Drawing.Size(25, 18)
        Me.txtLorryNo1.TabIndex = 5
        Me.txtLorryNo1.Visible = False
        '
        'lblBookingDate
        '
        Me.lblBookingDate.AutoSize = False
        Me.lblBookingDate.BorderVisible = True
        Me.lblBookingDate.FieldName = Nothing
        Me.lblBookingDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookingDate.Location = New System.Drawing.Point(242, 68)
        Me.lblBookingDate.Name = "lblBookingDate"
        Me.lblBookingDate.Size = New System.Drawing.Size(287, 18)
        Me.lblBookingDate.TabIndex = 54
        Me.lblBookingDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBookingDate.TextWrap = False
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(242, 46)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(287, 18)
        Me.lblLocationName.TabIndex = 55
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(0, 92)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel14.TabIndex = 47
        Me.RadLabel14.Text = "Comment"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(-1, 69)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel18.TabIndex = 36
        Me.RadLabel18.Text = "Booking No"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(-1, 47)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 37
        Me.RadLabel15.Text = "Location"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 173)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(999, 170)
        Me.RadGroupBox2.TabIndex = 27
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Controls.Add(Me.MyLabel1)
        Me.gv1.Controls.Add(Me.ddlFreight)
        Me.gv1.Controls.Add(Me.MyLabel4)
        Me.gv1.Controls.Add(Me.txtFreightAmt)
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(979, 140)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(819, 12)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel1.TabIndex = 58
        Me.MyLabel1.Text = "Freight"
        Me.MyLabel1.Visible = False
        '
        'ddlFreight
        '
        Me.ddlFreight.AutoCompleteDisplayMember = Nothing
        Me.ddlFreight.AutoCompleteValueMember = Nothing
        Me.ddlFreight.CalculationExpression = Nothing
        Me.ddlFreight.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlFreight.FieldCode = Nothing
        Me.ddlFreight.FieldDesc = Nothing
        Me.ddlFreight.FieldMaxLength = 0
        Me.ddlFreight.FieldName = Nothing
        Me.ddlFreight.isCalculatedField = False
        Me.ddlFreight.IsSourceFromTable = False
        Me.ddlFreight.IsSourceFromValueList = False
        Me.ddlFreight.IsUnique = False
        Me.ddlFreight.Location = New System.Drawing.Point(917, 10)
        Me.ddlFreight.MendatroryField = False
        Me.ddlFreight.MyLinkLable1 = Me.MyLabel1
        Me.ddlFreight.MyLinkLable2 = Nothing
        Me.ddlFreight.Name = "ddlFreight"
        Me.ddlFreight.ReferenceFieldDesc = Nothing
        Me.ddlFreight.ReferenceFieldName = Nothing
        Me.ddlFreight.ReferenceTableName = Nothing
        Me.ddlFreight.Size = New System.Drawing.Size(143, 20)
        Me.ddlFreight.TabIndex = 10
        Me.ddlFreight.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(1077, 12)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel4.TabIndex = 45
        Me.MyLabel4.Text = "Freight Amount"
        Me.MyLabel4.Visible = False
        '
        'txtFreightAmt
        '
        Me.txtFreightAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFreightAmt.CalculationExpression = Nothing
        Me.txtFreightAmt.DecimalPlaces = 5
        Me.txtFreightAmt.FieldCode = Nothing
        Me.txtFreightAmt.FieldDesc = Nothing
        Me.txtFreightAmt.FieldMaxLength = 0
        Me.txtFreightAmt.FieldName = Nothing
        Me.txtFreightAmt.isCalculatedField = False
        Me.txtFreightAmt.IsSourceFromTable = False
        Me.txtFreightAmt.IsSourceFromValueList = False
        Me.txtFreightAmt.IsUnique = False
        Me.txtFreightAmt.Location = New System.Drawing.Point(1169, 10)
        Me.txtFreightAmt.MendatroryField = True
        Me.txtFreightAmt.MyLinkLable1 = Nothing
        Me.txtFreightAmt.MyLinkLable2 = Nothing
        Me.txtFreightAmt.Name = "txtFreightAmt"
        Me.txtFreightAmt.ReferenceFieldDesc = Nothing
        Me.txtFreightAmt.ReferenceFieldName = Nothing
        Me.txtFreightAmt.ReferenceTableName = Nothing
        Me.txtFreightAmt.Size = New System.Drawing.Size(179, 20)
        Me.txtFreightAmt.TabIndex = 129
        Me.txtFreightAmt.Text = "0"
        Me.txtFreightAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFreightAmt.Value = 0R
        Me.txtFreightAmt.Visible = False
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(242, 24)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(287, 18)
        Me.lblCustomerName.TabIndex = 56
        Me.lblCustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustomerName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(-1, 25)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 38
        Me.RadLabel2.Text = "Customer No"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(-1, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel1.TabIndex = 39
        Me.RadLabel1.Text = "Delivery No"
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(98, 91)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel14
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(431, 18)
        Me.txtComment.TabIndex = 12
        '
        'fndCustomerNo
        '
        Me.fndCustomerNo.CalculationExpression = Nothing
        Me.fndCustomerNo.FieldCode = Nothing
        Me.fndCustomerNo.FieldDesc = Nothing
        Me.fndCustomerNo.FieldMaxLength = 0
        Me.fndCustomerNo.FieldName = Nothing
        Me.fndCustomerNo.isCalculatedField = False
        Me.fndCustomerNo.IsSourceFromTable = False
        Me.fndCustomerNo.IsSourceFromValueList = False
        Me.fndCustomerNo.IsUnique = False
        Me.fndCustomerNo.Location = New System.Drawing.Point(98, 24)
        Me.fndCustomerNo.MendatroryField = True
        Me.fndCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomerNo.MyLinkLable1 = Me.RadLabel2
        Me.fndCustomerNo.MyLinkLable2 = Me.lblCustomerName
        Me.fndCustomerNo.MyReadOnly = False
        Me.fndCustomerNo.MyShowMasterFormButton = False
        Me.fndCustomerNo.Name = "fndCustomerNo"
        Me.fndCustomerNo.ReferenceFieldDesc = Nothing
        Me.fndCustomerNo.ReferenceFieldName = Nothing
        Me.fndCustomerNo.ReferenceTableName = Nothing
        Me.fndCustomerNo.Size = New System.Drawing.Size(143, 18)
        Me.fndCustomerNo.TabIndex = 4
        Me.fndCustomerNo.Value = ""
        '
        'fndBookingNo
        '
        Me.fndBookingNo.CalculationExpression = Nothing
        Me.fndBookingNo.FieldCode = Nothing
        Me.fndBookingNo.FieldDesc = Nothing
        Me.fndBookingNo.FieldMaxLength = 0
        Me.fndBookingNo.FieldName = Nothing
        Me.fndBookingNo.isCalculatedField = False
        Me.fndBookingNo.IsSourceFromTable = False
        Me.fndBookingNo.IsSourceFromValueList = False
        Me.fndBookingNo.IsUnique = False
        Me.fndBookingNo.Location = New System.Drawing.Point(98, 68)
        Me.fndBookingNo.MendatroryField = True
        Me.fndBookingNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBookingNo.MyLinkLable1 = Me.RadLabel18
        Me.fndBookingNo.MyLinkLable2 = Me.lblBookingDate
        Me.fndBookingNo.MyReadOnly = False
        Me.fndBookingNo.MyShowMasterFormButton = False
        Me.fndBookingNo.Name = "fndBookingNo"
        Me.fndBookingNo.ReferenceFieldDesc = Nothing
        Me.fndBookingNo.ReferenceFieldName = Nothing
        Me.fndBookingNo.ReferenceTableName = Nothing
        Me.fndBookingNo.Size = New System.Drawing.Size(143, 18)
        Me.fndBookingNo.TabIndex = 8
        Me.fndBookingNo.Value = ""
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
        Me.fndLocation.Location = New System.Drawing.Point(98, 46)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.RadLabel15
        Me.fndLocation.MyLinkLable2 = Me.lblLocationName
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(143, 18)
        Me.fndLocation.TabIndex = 6
        Me.fndLocation.Value = ""
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(98, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(350, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(949, 415)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(949, 415)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'btnUpdateVehicle
        '
        Me.btnUpdateVehicle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateVehicle.Location = New System.Drawing.Point(5, 4)
        Me.btnUpdateVehicle.Name = "btnUpdateVehicle"
        Me.btnUpdateVehicle.Size = New System.Drawing.Size(100, 22)
        Me.btnUpdateVehicle.TabIndex = 1
        Me.btnUpdateVehicle.Text = "Update Vehicle"
        Me.btnUpdateVehicle.Visible = False
        '
        'btnApproveCreditLimit
        '
        Me.btnApproveCreditLimit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApproveCreditLimit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApproveCreditLimit.Location = New System.Drawing.Point(423, 3)
        Me.btnApproveCreditLimit.Name = "btnApproveCreditLimit"
        Me.btnApproveCreditLimit.Size = New System.Drawing.Size(126, 22)
        Me.btnApproveCreditLimit.TabIndex = 6
        Me.btnApproveCreditLimit.Text = "Approval Credit Limit"
        Me.btnApproveCreditLimit.Visible = False
        '
        'btnsetting
        '
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnPreview, Me.BtnSend, Me.BtnSendForApproval})
        Me.btnsetting.Location = New System.Drawing.Point(330, 4)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(87, 22)
        Me.btnsetting.TabIndex = 4
        Me.btnsetting.Text = "E-Mail/SMS"
        Me.btnsetting.Visible = False
        '
        'BtnPreview
        '
        Me.BtnPreview.AccessibleDescription = "Preview"
        Me.BtnPreview.AccessibleName = "Preview"
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Text = "Preview"
        '
        'BtnSend
        '
        Me.BtnSend.AccessibleDescription = "Send Mail/Sms"
        Me.BtnSend.AccessibleName = "Send Mail/Sms"
        Me.BtnSend.Name = "BtnSend"
        Me.BtnSend.Text = "Send Mail/Sms"
        '
        'BtnSendForApproval
        '
        Me.BtnSendForApproval.AccessibleDescription = "Send Mail For Approval"
        Me.BtnSendForApproval.AccessibleName = "Send Mail For Approval"
        Me.BtnSendForApproval.Name = "BtnSendForApproval"
        Me.BtnSendForApproval.Text = "Send Mail For Approval"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(113, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(259, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(188, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(941, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(555, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.Visible = False
        '
        'EmailSmsSetting
        '
        Me.EmailSmsSetting.AccessibleDescription = "Email/SMS Setting"
        Me.EmailSmsSetting.AccessibleName = "Email/SMS Setting"
        Me.EmailSmsSetting.Name = "EmailSmsSetting"
        Me.EmailSmsSetting.Text = "Email/SMS Setting"
        Me.EmailSmsSetting.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem4.AccessibleName = "Delete Layout"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1025, 501)
        Me.Panel1.TabIndex = 7
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1025, 20)
        Me.RadMenu1.TabIndex = 6
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.EmailSmsSetting, Me.EmailSettingCreditApproval})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'EmailSettingCreditApproval
        '
        Me.EmailSettingCreditApproval.AccessibleDescription = "Email setting for Credit Approval"
        Me.EmailSettingCreditApproval.AccessibleName = "Email setting for Credit Approval"
        Me.EmailSettingCreditApproval.Name = "EmailSettingCreditApproval"
        Me.EmailSettingCreditApproval.Text = "Email setting for Credit Approval"
        Me.EmailSettingCreditApproval.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AnimationEnabled = False
        Me.RadMenuItem2.AnimationFrames = 1
        Me.RadMenuItem2.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.None
        Me.RadMenuItem2.AutoSize = True
        Me.RadMenuItem2.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem2.DropShadow = True
        Me.RadMenuItem2.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem2.EnableAeroEffects = False
        Me.RadMenuItem2.FadeAnimationFrames = 10
        Me.RadMenuItem2.FadeAnimationSpeed = 10
        Me.RadMenuItem2.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem2.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem2.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Location = New System.Drawing.Point(150, 573)
        Me.RadMenuItem2.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Opacity = 1.0!
        Me.RadMenuItem2.ProcessKeyboard = False
        Me.RadMenuItem2.RollOverItemSelection = True
        Me.RadMenuItem2.Size = New System.Drawing.Size(27, 2)
        Me.RadMenuItem2.TabIndex = 5
        Me.RadMenuItem2.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Visible = False
        '
        'frmDeliveryNoteDairySale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 521)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadMenuItem2)
        Me.Name = "frmDeliveryNoteDairySale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Delivery Note"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShortClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoadPermit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoadPermitNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLorryNo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBookingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv1.ResumeLayout(False)
        Me.gv1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreightAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnUpdateVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnApproveCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents ddlStatus As common.Controls.MyComboBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents txtVehicleCapacity As common.Controls.MyTextBox
    Friend WithEvents txtLorryNo1 As common.Controls.MyTextBox
    Friend WithEvents lblBookingDate As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents fndCustomerNo As common.UserControls.txtFinder
    Friend WithEvents fndBookingNo As common.UserControls.txtFinder
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BtnPreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSendForApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents EmailSmsSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddlFreight As common.Controls.MyComboBox
    Friend WithEvents lblRoadPermit As common.Controls.MyLabel
    Friend WithEvents txtRoadPermitNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtTransporterName As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtFreightAmt As common.MyNumBox
    Friend WithEvents chkShortClose As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtCreditLimit As common.MyNumBox
    Friend WithEvents lblCreditLimit As common.Controls.MyLabel
    Friend WithEvents lblTaxGrp As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtPriceCode As common.Controls.MyLabel
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLorryNo As common.UserControls.txtFinder
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents TxtRouteNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents btnApproveCreditLimit As Telerik.WinControls.UI.RadButton
    Friend WithEvents EmailSettingCreditApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents lblVehicleNo As common.Controls.MyTextBox
    Friend WithEvents btnUpdateVehicle As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblShipToLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtShipToLocation As common.UserControls.txtFinder
End Class

