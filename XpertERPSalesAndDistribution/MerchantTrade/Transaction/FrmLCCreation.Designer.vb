<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLCCreation
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
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtPurchaseInvoiceNo = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.TxtLCNo = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.FndFDNo = New common.UserControls.txtFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.cmbFDType = New common.Controls.MyComboBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.TxtLCCharges = New common.MyNumBox()
        Me.TxtLocationCode = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.TxtBenefeciery = New common.Controls.MyLabel()
        Me.TxtPurchaseOrderNo = New common.Controls.MyLabel()
        Me.TxtBankCode = New common.Controls.MyLabel()
        Me.cboLCPeriod = New common.Controls.MyComboBox()
        Me.lblLCExpiryDate = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblRequest = New common.Controls.MyLabel()
        Me.fndLCCreationcode = New common.UserControls.txtNavigator()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtLCCreationdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.ddlLCType = New common.Controls.MyComboBox()
        Me.TxtLCPeriod = New common.MyNumBox()
        Me.FndLCRequestNo = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.LblBankName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.TxtLCAmount = New common.MyNumBox()
        Me.lblBenefeciery = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.RDTotal = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.txtConversionRate = New common.MyNumBox()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.BtnLcCancellation = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.TxtPurchaseInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFDType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBenefeciery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPurchaseOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLCPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLCExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLCCreationdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlLCType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBenefeciery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.RDTotal.SuspendLayout()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnLcCancellation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(670, 16)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(90, 16)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(9, 16)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(170, 16)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 6
        Me.btnPost.Text = "Post"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.BtnLcCancellation)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer2.Size = New System.Drawing.Size(755, 441)
        Me.SplitContainer2.SplitterDistance = 397
        Me.SplitContainer2.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RDTotal)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(755, 397)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtPurchaseInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.FndFDNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.cmbFDType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCCharges)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationDesc)
        Me.RadPageViewPage1.Controls.Add(Me.TxtBenefeciery)
        Me.RadPageViewPage1.Controls.Add(Me.TxtPurchaseOrderNo)
        Me.RadPageViewPage1.Controls.Add(Me.TxtBankCode)
        Me.RadPageViewPage1.Controls.Add(Me.cboLCPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.lblLCExpiryDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.lblRequest)
        Me.RadPageViewPage1.Controls.Add(Me.fndLCCreationcode)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtLCCreationdate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.ddlLCType)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.FndLCRequestNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.LblBankName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCAmount)
        Me.RadPageViewPage1.Controls.Add(Me.lblBenefeciery)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(77.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(734, 351)
        Me.RadPageViewPage1.Text = "LC Creation"
        '
        'TxtPurchaseInvoiceNo
        '
        Me.TxtPurchaseInvoiceNo.AutoSize = False
        Me.TxtPurchaseInvoiceNo.BorderVisible = True
        Me.TxtPurchaseInvoiceNo.Location = New System.Drawing.Point(438, 95)
        Me.TxtPurchaseInvoiceNo.Name = "TxtPurchaseInvoiceNo"
        Me.TxtPurchaseInvoiceNo.Size = New System.Drawing.Size(217, 18)
        Me.TxtPurchaseInvoiceNo.TabIndex = 397
        Me.TxtPurchaseInvoiceNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel16
        '
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(324, 96)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel16.TabIndex = 396
        Me.MyLabel16.Text = "Proforma Invoice No"
        '
        'TxtLCNo
        '
        Me.TxtLCNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLCNo.Location = New System.Drawing.Point(116, 301)
        Me.TxtLCNo.MaxLength = 200
        Me.TxtLCNo.MendatroryField = False
        Me.TxtLCNo.MyLinkLable1 = Nothing
        Me.TxtLCNo.MyLinkLable2 = Nothing
        Me.TxtLCNo.Name = "TxtLCNo"
        Me.TxtLCNo.Size = New System.Drawing.Size(201, 18)
        Me.TxtLCNo.TabIndex = 394
        '
        'MyLabel14
        '
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel14.Location = New System.Drawing.Point(7, 302)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel14.TabIndex = 395
        Me.MyLabel14.Text = "LC No"
        '
        'FndFDNo
        '
        Me.FndFDNo.Location = New System.Drawing.Point(116, 279)
        Me.FndFDNo.MendatroryField = False
        Me.FndFDNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndFDNo.MyLinkLable1 = Nothing
        Me.FndFDNo.MyLinkLable2 = Nothing
        Me.FndFDNo.MyReadOnly = False
        Me.FndFDNo.MyShowMasterFormButton = True
        Me.FndFDNo.Name = "FndFDNo"
        Me.FndFDNo.Size = New System.Drawing.Size(201, 19)
        Me.FndFDNo.TabIndex = 368
        Me.FndFDNo.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(7, 276)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(39, 16)
        Me.MyLabel13.TabIndex = 367
        Me.MyLabel13.Text = "FD No"
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(7, 259)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel12.TabIndex = 366
        Me.MyLabel12.Text = "FD Type"
        '
        'cmbFDType
        '
        Me.cmbFDType.AutoCompleteDisplayMember = Nothing
        Me.cmbFDType.AutoCompleteValueMember = Nothing
        Me.cmbFDType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFDType.Location = New System.Drawing.Point(116, 255)
        Me.cmbFDType.MendatroryField = False
        Me.cmbFDType.MyLinkLable1 = Nothing
        Me.cmbFDType.MyLinkLable2 = Nothing
        Me.cmbFDType.Name = "cmbFDType"
        Me.cmbFDType.Size = New System.Drawing.Size(202, 20)
        Me.cmbFDType.TabIndex = 365
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(7, 233)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel10.TabIndex = 363
        Me.MyLabel10.Text = "LC Charges"
        '
        'TxtLCCharges
        '
        Me.TxtLCCharges.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtLCCharges.DecimalPlaces = 2
        Me.TxtLCCharges.Location = New System.Drawing.Point(116, 231)
        Me.TxtLCCharges.MendatroryField = False
        Me.TxtLCCharges.MyLinkLable1 = Nothing
        Me.TxtLCCharges.MyLinkLable2 = Nothing
        Me.TxtLCCharges.Name = "TxtLCCharges"
        Me.TxtLCCharges.Size = New System.Drawing.Size(201, 20)
        Me.TxtLCCharges.TabIndex = 364
        Me.TxtLCCharges.Text = "0"
        Me.TxtLCCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLCCharges.Value = 0.0R
        '
        'TxtLocationCode
        '
        Me.TxtLocationCode.AutoSize = False
        Me.TxtLocationCode.BorderVisible = True
        Me.TxtLocationCode.Location = New System.Drawing.Point(116, 139)
        Me.TxtLocationCode.Name = "TxtLocationCode"
        Me.TxtLocationCode.Size = New System.Drawing.Size(202, 18)
        Me.TxtLocationCode.TabIndex = 362
        Me.TxtLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(7, 140)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel4.TabIndex = 360
        Me.MyLabel4.Text = "Location"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.Location = New System.Drawing.Point(321, 139)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(334, 18)
        Me.lblLocationDesc.TabIndex = 361
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtBenefeciery
        '
        Me.TxtBenefeciery.AutoSize = False
        Me.TxtBenefeciery.BorderVisible = True
        Me.TxtBenefeciery.Location = New System.Drawing.Point(117, 117)
        Me.TxtBenefeciery.Name = "TxtBenefeciery"
        Me.TxtBenefeciery.Size = New System.Drawing.Size(201, 18)
        Me.TxtBenefeciery.TabIndex = 359
        Me.TxtBenefeciery.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPurchaseOrderNo
        '
        Me.TxtPurchaseOrderNo.AutoSize = False
        Me.TxtPurchaseOrderNo.BorderVisible = True
        Me.TxtPurchaseOrderNo.Location = New System.Drawing.Point(117, 95)
        Me.TxtPurchaseOrderNo.Name = "TxtPurchaseOrderNo"
        Me.TxtPurchaseOrderNo.Size = New System.Drawing.Size(201, 18)
        Me.TxtPurchaseOrderNo.TabIndex = 358
        Me.TxtPurchaseOrderNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtBankCode
        '
        Me.TxtBankCode.AutoSize = False
        Me.TxtBankCode.BorderVisible = True
        Me.TxtBankCode.Location = New System.Drawing.Point(116, 73)
        Me.TxtBankCode.Name = "TxtBankCode"
        Me.TxtBankCode.Size = New System.Drawing.Size(201, 18)
        Me.TxtBankCode.TabIndex = 357
        Me.TxtBankCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboLCPeriod
        '
        Me.cboLCPeriod.AutoCompleteDisplayMember = Nothing
        Me.cboLCPeriod.AutoCompleteValueMember = Nothing
        Me.cboLCPeriod.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLCPeriod.Location = New System.Drawing.Point(321, 207)
        Me.cboLCPeriod.MendatroryField = False
        Me.cboLCPeriod.MyLinkLable1 = Nothing
        Me.cboLCPeriod.MyLinkLable2 = Nothing
        Me.cboLCPeriod.Name = "cboLCPeriod"
        Me.cboLCPeriod.Size = New System.Drawing.Size(171, 20)
        Me.cboLCPeriod.TabIndex = 356
        '
        'lblLCExpiryDate
        '
        Me.lblLCExpiryDate.AutoSize = False
        Me.lblLCExpiryDate.BorderVisible = True
        Me.lblLCExpiryDate.Location = New System.Drawing.Point(659, 9)
        Me.lblLCExpiryDate.Name = "lblLCExpiryDate"
        Me.lblLCExpiryDate.Size = New System.Drawing.Size(58, 18)
        Me.lblLCExpiryDate.TabIndex = 355
        Me.lblLCExpiryDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLCExpiryDate.Visible = False
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(550, 10)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel11.TabIndex = 354
        Me.MyLabel11.Text = "LC Expiry"
        Me.MyLabel11.Visible = False
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(422, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 334
        '
        'lblRequest
        '
        Me.lblRequest.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRequest.Location = New System.Drawing.Point(7, 8)
        Me.lblRequest.Name = "lblRequest"
        Me.lblRequest.Size = New System.Drawing.Size(84, 16)
        Me.lblRequest.TabIndex = 17
        Me.lblRequest.Text = "LC Creation No"
        '
        'fndLCCreationcode
        '
        Me.fndLCCreationcode.Location = New System.Drawing.Point(116, 6)
        Me.fndLCCreationcode.MendatroryField = True
        Me.fndLCCreationcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndLCCreationcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndLCCreationcode.MyLinkLable1 = Me.lblRequest
        Me.fndLCCreationcode.MyLinkLable2 = Nothing
        Me.fndLCCreationcode.MyMaxLength = 32767
        Me.fndLCCreationcode.MyReadOnly = False
        Me.fndLCCreationcode.Name = "fndLCCreationcode"
        Me.fndLCCreationcode.Size = New System.Drawing.Size(282, 18)
        Me.fndLCCreationcode.TabIndex = 18
        Me.fndLCCreationcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(402, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 19)
        Me.btnnew.TabIndex = 19
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 74)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "Bank Code"
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(7, 186)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel8.TabIndex = 331
        Me.MyLabel8.Text = "LC Type"
        '
        'txtLCCreationdate
        '
        Me.txtLCCreationdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtLCCreationdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCCreationdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLCCreationdate.Location = New System.Drawing.Point(116, 29)
        Me.txtLCCreationdate.MendatroryField = True
        Me.txtLCCreationdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLCCreationdate.MyLinkLable1 = Me.MyLabel2
        Me.txtLCCreationdate.MyLinkLable2 = Nothing
        Me.txtLCCreationdate.Name = "txtLCCreationdate"
        Me.txtLCCreationdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLCCreationdate.Size = New System.Drawing.Size(201, 18)
        Me.txtLCCreationdate.TabIndex = 21
        Me.txtLCCreationdate.TabStop = False
        Me.txtLCCreationdate.Text = "13/06/2011 11:29 AM"
        Me.txtLCCreationdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 30)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel2.TabIndex = 20
        Me.MyLabel2.Text = "LC Creation Date"
        '
        'ddlLCType
        '
        Me.ddlLCType.AutoCompleteDisplayMember = Nothing
        Me.ddlLCType.AutoCompleteValueMember = Nothing
        Me.ddlLCType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlLCType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Sight"
        RadListDataItem2.Text = "Usance"
        Me.ddlLCType.Items.Add(RadListDataItem1)
        Me.ddlLCType.Items.Add(RadListDataItem2)
        Me.ddlLCType.Location = New System.Drawing.Point(116, 185)
        Me.ddlLCType.MendatroryField = False
        Me.ddlLCType.MyLinkLable1 = Nothing
        Me.ddlLCType.MyLinkLable2 = Nothing
        Me.ddlLCType.Name = "ddlLCType"
        '
        '
        '
        Me.ddlLCType.RootElement.StretchVertically = True
        Me.ddlLCType.Size = New System.Drawing.Size(201, 18)
        Me.ddlLCType.TabIndex = 55
        '
        'TxtLCPeriod
        '
        Me.TxtLCPeriod.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtLCPeriod.DecimalPlaces = 0
        Me.TxtLCPeriod.Location = New System.Drawing.Point(116, 207)
        Me.TxtLCPeriod.MaxLength = 6
        Me.TxtLCPeriod.MendatroryField = True
        Me.TxtLCPeriod.MyLinkLable1 = Nothing
        Me.TxtLCPeriod.MyLinkLable2 = Nothing
        Me.TxtLCPeriod.Name = "TxtLCPeriod"
        Me.TxtLCPeriod.Size = New System.Drawing.Size(201, 20)
        Me.TxtLCPeriod.TabIndex = 33
        Me.TxtLCPeriod.Text = "0"
        Me.TxtLCPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLCPeriod.Value = 0.0R
        '
        'FndLCRequestNo
        '
        Me.FndLCRequestNo.Location = New System.Drawing.Point(116, 51)
        Me.FndLCRequestNo.MendatroryField = True
        Me.FndLCRequestNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLCRequestNo.MyLinkLable1 = Nothing
        Me.FndLCRequestNo.MyLinkLable2 = Nothing
        Me.FndLCRequestNo.MyReadOnly = False
        Me.FndLCRequestNo.MyShowMasterFormButton = False
        Me.FndLCRequestNo.Name = "FndLCRequestNo"
        Me.FndLCRequestNo.Size = New System.Drawing.Size(201, 18)
        Me.FndLCRequestNo.TabIndex = 35
        Me.FndLCRequestNo.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(7, 209)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel7.TabIndex = 32
        Me.MyLabel7.Text = "LC Period"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(7, 52)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel3.TabIndex = 34
        Me.MyLabel3.Text = "LC Request No"
        '
        'LblBankName
        '
        Me.LblBankName.AutoSize = False
        Me.LblBankName.BorderVisible = True
        Me.LblBankName.Location = New System.Drawing.Point(321, 73)
        Me.LblBankName.Name = "LblBankName"
        Me.LblBankName.Size = New System.Drawing.Size(334, 18)
        Me.LblBankName.TabIndex = 24
        Me.LblBankName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(7, 163)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel5.TabIndex = 30
        Me.MyLabel5.Text = "LC Amount"
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(7, 96)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel6.TabIndex = 25
        Me.MyLabel6.Text = "Purchase Order No"
        '
        'TxtLCAmount
        '
        Me.TxtLCAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtLCAmount.DecimalPlaces = 2
        Me.TxtLCAmount.Location = New System.Drawing.Point(116, 161)
        Me.TxtLCAmount.MendatroryField = True
        Me.TxtLCAmount.MyLinkLable1 = Nothing
        Me.TxtLCAmount.MyLinkLable2 = Nothing
        Me.TxtLCAmount.Name = "TxtLCAmount"
        Me.TxtLCAmount.Size = New System.Drawing.Size(201, 20)
        Me.TxtLCAmount.TabIndex = 31
        Me.TxtLCAmount.Text = "0"
        Me.TxtLCAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLCAmount.Value = 0.0R
        '
        'lblBenefeciery
        '
        Me.lblBenefeciery.AutoSize = False
        Me.lblBenefeciery.BorderVisible = True
        Me.lblBenefeciery.Location = New System.Drawing.Point(321, 117)
        Me.lblBenefeciery.Name = "lblBenefeciery"
        Me.lblBenefeciery.Size = New System.Drawing.Size(334, 18)
        Me.lblBenefeciery.TabIndex = 29
        Me.lblBenefeciery.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(7, 118)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel9.TabIndex = 27
        Me.MyLabel9.Text = "Benefeciery"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(568, 257)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(568, 257)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'RDTotal
        '
        Me.RDTotal.Controls.Add(Me.pnlCurrConv)
        Me.RDTotal.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RDTotal.Location = New System.Drawing.Point(10, 35)
        Me.RDTotal.Name = "RDTotal"
        Me.RDTotal.Size = New System.Drawing.Size(734, 323)
        Me.RDTotal.Text = "Total"
        '
        'pnlCurrConv
        '
        Me.pnlCurrConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCurrConv.Controls.Add(Me.txtCurrencyCode)
        Me.pnlCurrConv.Controls.Add(Me.txtConversionRate)
        Me.pnlCurrConv.Controls.Add(Me.lblCurrency)
        Me.pnlCurrConv.Controls.Add(Me.lblConvRate)
        Me.pnlCurrConv.Location = New System.Drawing.Point(3, 3)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(492, 38)
        Me.pnlCurrConv.TabIndex = 2
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCurrencyCode.Location = New System.Drawing.Point(80, 6)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Nothing
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(170, 24)
        Me.txtCurrencyCode.TabIndex = 4
        Me.txtCurrencyCode.Value = ""
        '
        'txtConversionRate
        '
        Me.txtConversionRate.BackColor = System.Drawing.Color.White
        Me.txtConversionRate.DecimalPlaces = 2
        Me.txtConversionRate.Location = New System.Drawing.Point(353, 8)
        Me.txtConversionRate.MendatroryField = False
        Me.txtConversionRate.MyLinkLable1 = Nothing
        Me.txtConversionRate.MyLinkLable2 = Nothing
        Me.txtConversionRate.Name = "txtConversionRate"
        Me.txtConversionRate.Size = New System.Drawing.Size(124, 20)
        Me.txtConversionRate.TabIndex = 1
        Me.txtConversionRate.Text = "1"
        Me.txtConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversionRate.Value = 1.0R
        '
        'lblCurrency
        '
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(22, 10)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 2
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(256, 10)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 3
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'BtnLcCancellation
        '
        Me.BtnLcCancellation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnLcCancellation.Location = New System.Drawing.Point(249, 16)
        Me.BtnLcCancellation.Name = "BtnLcCancellation"
        Me.BtnLcCancellation.Size = New System.Drawing.Size(126, 20)
        Me.BtnLcCancellation.TabIndex = 7
        Me.BtnLcCancellation.Text = "LC Cancellation"
        '
        'FrmLCCreation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 441)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "FrmLCCreation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "LC Creation"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.TxtPurchaseInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFDType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBenefeciery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPurchaseOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLCPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLCExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLCCreationdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlLCType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBenefeciery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.RDTotal.ResumeLayout(False)
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnLcCancellation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblRequest As common.Controls.MyLabel
    Friend WithEvents fndLCCreationcode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtLCCreationdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents ddlLCType As common.Controls.MyComboBox
    Friend WithEvents TxtLCPeriod As common.MyNumBox
    Friend WithEvents FndLCRequestNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents LblBankName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents TxtLCAmount As common.MyNumBox
    Friend WithEvents lblBenefeciery As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents RDTotal As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblLCExpiryDate As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents cboLCPeriod As common.Controls.MyComboBox
    Friend WithEvents TxtBenefeciery As common.Controls.MyLabel
    Friend WithEvents TxtPurchaseOrderNo As common.Controls.MyLabel
    Friend WithEvents TxtBankCode As common.Controls.MyLabel
    Friend WithEvents TxtLocationCode As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents TxtLCCharges As common.MyNumBox
    Friend WithEvents BtnLcCancellation As Telerik.WinControls.UI.RadButton
    Friend WithEvents FndFDNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents cmbFDType As common.Controls.MyComboBox
    Friend WithEvents TxtLCNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents TxtPurchaseInvoiceNo As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
End Class

