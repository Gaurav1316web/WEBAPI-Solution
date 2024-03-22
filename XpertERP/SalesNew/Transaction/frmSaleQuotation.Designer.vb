<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaleQuotation
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.lblProject = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtSalesman = New common.UserControls.txtFinder()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.txtRequestBy = New common.Controls.MyTextBox()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.txtRequiredDate = New common.Controls.MyDateTimePicker()
        Me.cboModeOfTransport = New common.Controls.MyComboBox()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtReqNo = New common.UserControls.txtNavigator()
        Me.txtExpireDate = New common.Controls.MyDateTimePicker()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.txtRmks = New common.Controls.MyTextBox()
        Me.txtCustOrderNo = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnPreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkqclose = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRateUserCustomer = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRateDefaultSetting = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdbEmailSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRequiredDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkqclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkqclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkRateUserCustomer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkRateDefaultSetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(935, 453)
        Me.SplitContainer1.SplitterDistance = 417
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(935, 417)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.txtConversionRate)
        Me.RadPageViewPage1.Controls.Add(Me.txtCurrencyCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtApplicableFrom)
        Me.RadPageViewPage1.Controls.Add(Me.lblEffectiveFrom)
        Me.RadPageViewPage1.Controls.Add(Me.lblConvRate)
        Me.RadPageViewPage1.Controls.Add(Me.lblCurrency)
        Me.RadPageViewPage1.Controls.Add(Me.lblSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequestBy)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequiredDate)
        Me.RadPageViewPage1.Controls.Add(Me.cboModeOfTransport)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtExpireDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtRmks)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustOrderNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(102.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(914, 371)
        Me.RadPageViewPage1.Text = "Sales Quotations"
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.RadLabel15)
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Location = New System.Drawing.Point(467, 106)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(445, 22)
        Me.pnlPCJ.TabIndex = 15
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(6, 1)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(41, 16)
        Me.RadLabel15.TabIndex = 34
        Me.RadLabel15.Text = "Project"
        '
        'fndProject
        '
        Me.fndProject.CalculationExpression = Nothing
        Me.fndProject.FieldCode = Nothing
        Me.fndProject.FieldDesc = Nothing
        Me.fndProject.FieldMaxLength = 0
        Me.fndProject.FieldName = Nothing
        Me.fndProject.isCalculatedField = False
        Me.fndProject.IsSourceFromTable = False
        Me.fndProject.IsSourceFromValueList = False
        Me.fndProject.IsUnique = False
        Me.fndProject.Location = New System.Drawing.Point(104, 1)
        Me.fndProject.MendatroryField = False
        Me.fndProject.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProject.MyLinkLable1 = Me.RadLabel15
        Me.fndProject.MyLinkLable2 = Me.lblProject
        Me.fndProject.MyReadOnly = False
        Me.fndProject.MyShowMasterFormButton = False
        Me.fndProject.Name = "fndProject"
        Me.fndProject.ReferenceFieldDesc = Nothing
        Me.fndProject.ReferenceFieldName = Nothing
        Me.fndProject.ReferenceTableName = Nothing
        Me.fndProject.Size = New System.Drawing.Size(144, 19)
        Me.fndProject.TabIndex = 16
        Me.fndProject.Value = ""
        '
        'lblProject
        '
        Me.lblProject.AutoSize = False
        Me.lblProject.BorderVisible = True
        Me.lblProject.FieldName = Nothing
        Me.lblProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProject.Location = New System.Drawing.Point(251, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(193, 20)
        Me.lblProject.TabIndex = 33
        Me.lblProject.TextWrap = False
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 278)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.TabIndex = 15
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'txtConversionRate
        '
        Me.txtConversionRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConversionRate.BackColor = System.Drawing.Color.White
        Me.txtConversionRate.CalculationExpression = Nothing
        Me.txtConversionRate.DecimalPlaces = 2
        Me.txtConversionRate.FieldCode = Nothing
        Me.txtConversionRate.FieldDesc = Nothing
        Me.txtConversionRate.FieldMaxLength = 0
        Me.txtConversionRate.FieldName = Nothing
        Me.txtConversionRate.isCalculatedField = False
        Me.txtConversionRate.IsSourceFromTable = False
        Me.txtConversionRate.IsSourceFromValueList = False
        Me.txtConversionRate.IsUnique = False
        Me.txtConversionRate.Location = New System.Drawing.Point(296, 353)
        Me.txtConversionRate.MendatroryField = False
        Me.txtConversionRate.MyLinkLable1 = Nothing
        Me.txtConversionRate.MyLinkLable2 = Nothing
        Me.txtConversionRate.Name = "txtConversionRate"
        Me.txtConversionRate.ReferenceFieldDesc = Nothing
        Me.txtConversionRate.ReferenceFieldName = Nothing
        Me.txtConversionRate.ReferenceTableName = Nothing
        Me.txtConversionRate.Size = New System.Drawing.Size(142, 20)
        Me.txtConversionRate.TabIndex = 19
        Me.txtConversionRate.Text = "1"
        Me.txtConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversionRate.Value = 1.0R
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCurrencyCode.CalculationExpression = Nothing
        Me.txtCurrencyCode.Enabled = False
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(69, 353)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Nothing
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.ReferenceFieldDesc = Nothing
        Me.txtCurrencyCode.ReferenceFieldName = Nothing
        Me.txtCurrencyCode.ReferenceTableName = Nothing
        Me.txtCurrencyCode.Size = New System.Drawing.Size(128, 20)
        Me.txtCurrencyCode.TabIndex = 18
        Me.txtCurrencyCode.Value = ""
        '
        'txtApplicableFrom
        '
        Me.txtApplicableFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtApplicableFrom.AutoSize = False
        Me.txtApplicableFrom.BorderVisible = True
        Me.txtApplicableFrom.FieldName = Nothing
        Me.txtApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicableFrom.Location = New System.Drawing.Point(540, 353)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(145, 18)
        Me.txtApplicableFrom.TabIndex = 41
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(444, 355)
        Me.lblEffectiveFrom.Name = "lblEffectiveFrom"
        Me.lblEffectiveFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblEffectiveFrom.TabIndex = 40
        Me.lblEffectiveFrom.Text = "Applicable From"
        '
        'lblConvRate
        '
        Me.lblConvRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(203, 355)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 38
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'lblCurrency
        '
        Me.lblCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(12, 355)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 36
        Me.lblCurrency.Text = "Currency"
        '
        'lblSalesman
        '
        Me.lblSalesman.AutoSize = False
        Me.lblSalesman.BorderVisible = True
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(246, 44)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(213, 19)
        Me.lblSalesman.TabIndex = 29
        Me.lblSalesman.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(0, 45)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 34
        Me.MyLabel3.Text = "Salesman"
        '
        'txtSalesman
        '
        Me.txtSalesman.CalculationExpression = Nothing
        Me.txtSalesman.FieldCode = Nothing
        Me.txtSalesman.FieldDesc = Nothing
        Me.txtSalesman.FieldMaxLength = 0
        Me.txtSalesman.FieldName = Nothing
        Me.txtSalesman.isCalculatedField = False
        Me.txtSalesman.IsSourceFromTable = False
        Me.txtSalesman.IsSourceFromValueList = False
        Me.txtSalesman.IsUnique = False
        Me.txtSalesman.Location = New System.Drawing.Point(73, 44)
        Me.txtSalesman.MendatroryField = False
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Me.MyLabel3
        Me.txtSalesman.MyLinkLable2 = Me.lblSalesman
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.MyShowMasterFormButton = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.ReferenceFieldDesc = Nothing
        Me.txtSalesman.ReferenceFieldName = Nothing
        Me.txtSalesman.ReferenceTableName = Nothing
        Me.txtSalesman.Size = New System.Drawing.Size(168, 19)
        Me.txtSalesman.TabIndex = 4
        Me.txtSalesman.Value = ""
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(246, 23)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(213, 19)
        Me.lblVendorName.TabIndex = 30
        Me.lblVendorName.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel1.TabIndex = 35
        Me.MyLabel1.Text = "Customer"
        '
        'txtVendorNo
        '
        Me.txtVendorNo.CalculationExpression = Nothing
        Me.txtVendorNo.FieldCode = Nothing
        Me.txtVendorNo.FieldDesc = Nothing
        Me.txtVendorNo.FieldMaxLength = 0
        Me.txtVendorNo.FieldName = Nothing
        Me.txtVendorNo.isCalculatedField = False
        Me.txtVendorNo.IsSourceFromTable = False
        Me.txtVendorNo.IsSourceFromValueList = False
        Me.txtVendorNo.IsUnique = False
        Me.txtVendorNo.Location = New System.Drawing.Point(73, 23)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.MyLabel1
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(168, 19)
        Me.txtVendorNo.TabIndex = 3
        Me.txtVendorNo.Value = ""
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(716, 45)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel12.TabIndex = 22
        Me.RadLabel12.Text = "Request By"
        '
        'txtRequestBy
        '
        Me.txtRequestBy.CalculationExpression = Nothing
        Me.txtRequestBy.FieldCode = Nothing
        Me.txtRequestBy.FieldDesc = Nothing
        Me.txtRequestBy.FieldMaxLength = 0
        Me.txtRequestBy.FieldName = Nothing
        Me.txtRequestBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestBy.isCalculatedField = False
        Me.txtRequestBy.IsSourceFromTable = False
        Me.txtRequestBy.IsSourceFromValueList = False
        Me.txtRequestBy.IsUnique = False
        Me.txtRequestBy.Location = New System.Drawing.Point(797, 44)
        Me.txtRequestBy.MaxLength = 100
        Me.txtRequestBy.MendatroryField = False
        Me.txtRequestBy.MyLinkLable1 = Me.RadLabel12
        Me.txtRequestBy.MyLinkLable2 = Nothing
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.ReferenceFieldDesc = Nothing
        Me.txtRequestBy.ReferenceFieldName = Nothing
        Me.txtRequestBy.ReferenceTableName = Nothing
        Me.txtRequestBy.Size = New System.Drawing.Size(114, 18)
        Me.txtRequestBy.TabIndex = 10
        '
        'cboItemType
        '
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownAnimationEnabled = True
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(571, 43)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel10
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(143, 20)
        Me.cboItemType.TabIndex = 9
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(463, 45)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel10.TabIndex = 27
        Me.RadLabel10.Text = "Item Type"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(716, 86)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel9.TabIndex = 24
        Me.RadLabel9.Text = "Date Required"
        '
        'txtRequiredDate
        '
        Me.txtRequiredDate.CalculationExpression = Nothing
        Me.txtRequiredDate.CustomFormat = "dd/MM/yyyy"
        Me.txtRequiredDate.FieldCode = Nothing
        Me.txtRequiredDate.FieldDesc = Nothing
        Me.txtRequiredDate.FieldMaxLength = 0
        Me.txtRequiredDate.FieldName = Nothing
        Me.txtRequiredDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRequiredDate.isCalculatedField = False
        Me.txtRequiredDate.IsSourceFromTable = False
        Me.txtRequiredDate.IsSourceFromValueList = False
        Me.txtRequiredDate.IsUnique = False
        Me.txtRequiredDate.Location = New System.Drawing.Point(797, 85)
        Me.txtRequiredDate.MendatroryField = False
        Me.txtRequiredDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRequiredDate.MyLinkLable1 = Me.RadLabel9
        Me.txtRequiredDate.MyLinkLable2 = Nothing
        Me.txtRequiredDate.Name = "txtRequiredDate"
        Me.txtRequiredDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRequiredDate.ReferenceFieldDesc = Nothing
        Me.txtRequiredDate.ReferenceFieldName = Nothing
        Me.txtRequiredDate.ReferenceTableName = Nothing
        Me.txtRequiredDate.ShowCheckBox = True
        Me.txtRequiredDate.Size = New System.Drawing.Size(114, 18)
        Me.txtRequiredDate.TabIndex = 14
        Me.txtRequiredDate.TabStop = False
        Me.txtRequiredDate.Text = "13/06/2011"
        Me.txtRequiredDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'cboModeOfTransport
        '
        Me.cboModeOfTransport.CalculationExpression = Nothing
        Me.cboModeOfTransport.DropDownAnimationEnabled = True
        Me.cboModeOfTransport.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModeOfTransport.FieldCode = Nothing
        Me.cboModeOfTransport.FieldDesc = Nothing
        Me.cboModeOfTransport.FieldMaxLength = 0
        Me.cboModeOfTransport.FieldName = Nothing
        Me.cboModeOfTransport.isCalculatedField = False
        Me.cboModeOfTransport.IsSourceFromTable = False
        Me.cboModeOfTransport.IsSourceFromValueList = False
        Me.cboModeOfTransport.IsUnique = False
        Me.cboModeOfTransport.Location = New System.Drawing.Point(571, 84)
        Me.cboModeOfTransport.MendatroryField = False
        Me.cboModeOfTransport.MyLinkLable1 = Me.RadLabel11
        Me.cboModeOfTransport.MyLinkLable2 = Nothing
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.ReferenceFieldDesc = Nothing
        Me.cboModeOfTransport.ReferenceFieldName = Nothing
        Me.cboModeOfTransport.ReferenceTableName = Nothing
        Me.cboModeOfTransport.Size = New System.Drawing.Size(143, 20)
        Me.cboModeOfTransport.TabIndex = 13
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(465, 86)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel11.TabIndex = 25
        Me.RadLabel11.Text = "Mode Of Transport"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(463, 3)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel13.TabIndex = 19
        Me.RadLabel13.Text = "Expire Date"
        '
        'RadLabel27
        '
        Me.RadLabel27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(724, 355)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel27.TabIndex = 27
        Me.RadLabel27.Text = "Total Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(803, 353)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 28
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(716, 22)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(196, 20)
        Me.lblLocation.TabIndex = 21
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(0, 66)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 33
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 127)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(909, 147)
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
        Me.gv1.Size = New System.Drawing.Size(889, 117)
        Me.gv1.TabIndex = 17
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(716, 66)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel3.TabIndex = 23
        Me.RadLabel3.Text = "Reference No"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(0, 106)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 31
        Me.RadLabel2.Text = "Comment"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(0, 86)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 32
        Me.RadLabel8.Text = "Remarks"
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(465, 66)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(66, 16)
        Me.RadLabel7.TabIndex = 26
        Me.RadLabel7.Text = "SO Number"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(463, 24)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 28
        Me.RadLabel6.Text = "Location"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(348, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(716, 3)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 16
        Me.chkOnHold.Text = "On Hold"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(0, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 16
        Me.RadLabel1.Text = "Code"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(571, 23)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel6
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtLocation.TabIndex = 8
        Me.txtLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(797, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(114, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 20
        '
        'txtReqNo
        '
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.Location = New System.Drawing.Point(73, 1)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtReqNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel1
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyMaxLength = 30
        Me.txtReqNo.MyReadOnly = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.Size = New System.Drawing.Size(246, 20)
        Me.txtReqNo.TabIndex = 0
        Me.txtReqNo.Value = ""
        '
        'txtExpireDate
        '
        Me.txtExpireDate.CalculationExpression = Nothing
        Me.txtExpireDate.CustomFormat = "dd/MM/yyyy"
        Me.txtExpireDate.FieldCode = Nothing
        Me.txtExpireDate.FieldDesc = Nothing
        Me.txtExpireDate.FieldMaxLength = 0
        Me.txtExpireDate.FieldName = Nothing
        Me.txtExpireDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtExpireDate.isCalculatedField = False
        Me.txtExpireDate.IsSourceFromTable = False
        Me.txtExpireDate.IsSourceFromValueList = False
        Me.txtExpireDate.IsUnique = False
        Me.txtExpireDate.Location = New System.Drawing.Point(571, 2)
        Me.txtExpireDate.MendatroryField = False
        Me.txtExpireDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpireDate.MyLinkLable1 = Me.RadLabel13
        Me.txtExpireDate.MyLinkLable2 = Nothing
        Me.txtExpireDate.Name = "txtExpireDate"
        Me.txtExpireDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpireDate.ReferenceFieldDesc = Nothing
        Me.txtExpireDate.ReferenceFieldName = Nothing
        Me.txtExpireDate.ReferenceTableName = Nothing
        Me.txtExpireDate.ShowCheckBox = True
        Me.txtExpireDate.Size = New System.Drawing.Size(92, 18)
        Me.txtExpireDate.TabIndex = 2
        Me.txtExpireDate.TabStop = False
        Me.txtExpireDate.Text = "13/06/2011"
        Me.txtExpireDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtComment.Location = New System.Drawing.Point(73, 105)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(386, 18)
        Me.txtComment.TabIndex = 7
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(797, 65)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel3
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(114, 18)
        Me.txtRefNo.TabIndex = 12
        '
        'txtRmks
        '
        Me.txtRmks.CalculationExpression = Nothing
        Me.txtRmks.FieldCode = Nothing
        Me.txtRmks.FieldDesc = Nothing
        Me.txtRmks.FieldMaxLength = 0
        Me.txtRmks.FieldName = Nothing
        Me.txtRmks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRmks.isCalculatedField = False
        Me.txtRmks.IsSourceFromTable = False
        Me.txtRmks.IsSourceFromValueList = False
        Me.txtRmks.IsUnique = False
        Me.txtRmks.Location = New System.Drawing.Point(73, 85)
        Me.txtRmks.MaxLength = 200
        Me.txtRmks.MendatroryField = False
        Me.txtRmks.MyLinkLable1 = Me.RadLabel8
        Me.txtRmks.MyLinkLable2 = Nothing
        Me.txtRmks.Name = "txtRmks"
        Me.txtRmks.ReferenceFieldDesc = Nothing
        Me.txtRmks.ReferenceFieldName = Nothing
        Me.txtRmks.ReferenceTableName = Nothing
        Me.txtRmks.Size = New System.Drawing.Size(386, 18)
        Me.txtRmks.TabIndex = 6
        '
        'txtCustOrderNo
        '
        Me.txtCustOrderNo.CalculationExpression = Nothing
        Me.txtCustOrderNo.FieldCode = Nothing
        Me.txtCustOrderNo.FieldDesc = Nothing
        Me.txtCustOrderNo.FieldMaxLength = 0
        Me.txtCustOrderNo.FieldName = Nothing
        Me.txtCustOrderNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustOrderNo.isCalculatedField = False
        Me.txtCustOrderNo.IsSourceFromTable = False
        Me.txtCustOrderNo.IsSourceFromValueList = False
        Me.txtCustOrderNo.IsUnique = False
        Me.txtCustOrderNo.Location = New System.Drawing.Point(571, 65)
        Me.txtCustOrderNo.MaxLength = 50
        Me.txtCustOrderNo.MendatroryField = False
        Me.txtCustOrderNo.MyLinkLable1 = Me.RadLabel7
        Me.txtCustOrderNo.MyLinkLable2 = Nothing
        Me.txtCustOrderNo.Name = "txtCustOrderNo"
        Me.txtCustOrderNo.ReferenceFieldDesc = Nothing
        Me.txtCustOrderNo.ReferenceFieldName = Nothing
        Me.txtCustOrderNo.ReferenceTableName = Nothing
        Me.txtCustOrderNo.Size = New System.Drawing.Size(143, 18)
        Me.txtCustOrderNo.TabIndex = 11
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
        Me.txtDate.Location = New System.Drawing.Point(380, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtDesc.Location = New System.Drawing.Point(73, 65)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(386, 18)
        Me.txtDesc.TabIndex = 5
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(319, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 18
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(914, 371)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(914, 371)
        Me.UcAttachment1.TabIndex = 0
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(914, 371)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(914, 371)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnPreview, Me.btnSend})
        Me.btnsetting.Location = New System.Drawing.Point(759, 5)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(87, 22)
        Me.btnsetting.TabIndex = 8
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'btnPreview
        '
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Text = "Preview"
        '
        'btnSend
        '
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Text = "Email/SMS Sent"
        '
        'chkqclose
        '
        Me.chkqclose.Location = New System.Drawing.Point(594, 7)
        Me.chkqclose.Name = "chkqclose"
        Me.chkqclose.Size = New System.Drawing.Size(101, 18)
        Me.chkqclose.TabIndex = 6
        Me.chkqclose.Text = "Close Quotation"
        '
        'chkRateUserCustomer
        '
        Me.chkRateUserCustomer.Enabled = False
        Me.chkRateUserCustomer.IsThreeState = True
        Me.chkRateUserCustomer.Location = New System.Drawing.Point(430, 6)
        Me.chkRateUserCustomer.Name = "chkRateUserCustomer"
        Me.chkRateUserCustomer.Size = New System.Drawing.Size(158, 18)
        Me.chkRateUserCustomer.TabIndex = 5
        Me.chkRateUserCustomer.Text = "User Customer Rate Setting"
        Me.chkRateUserCustomer.Visible = False
        '
        'chkRateDefaultSetting
        '
        Me.chkRateDefaultSetting.Enabled = False
        Me.chkRateDefaultSetting.IsThreeState = True
        Me.chkRateDefaultSetting.Location = New System.Drawing.Point(306, 7)
        Me.chkRateDefaultSetting.Name = "chkRateDefaultSetting"
        Me.chkRateDefaultSetting.Size = New System.Drawing.Size(120, 18)
        Me.chkRateDefaultSetting.TabIndex = 4
        Me.chkRateDefaultSetting.Text = "Rate Default Setting"
        Me.chkRateDefaultSetting.Visible = False
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
        Me.btnDelete.Location = New System.Drawing.Point(79, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(154, 5)
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
        Me.btnClose.Location = New System.Drawing.Point(861, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 7
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
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(935, 20)
        Me.RadMenu1.TabIndex = 4
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdbEmailSetting})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rdbEmailSetting
        '
        Me.rdbEmailSetting.AccessibleDescription = "RadMenuItem1"
        Me.rdbEmailSetting.AccessibleName = "RadMenuItem1"
        Me.rdbEmailSetting.Name = "rdbEmailSetting"
        Me.rdbEmailSetting.Text = "Email/SMS Setting"
        '
        'frmSaleQuotation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmSaleQuotation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Quotation"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRequiredDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkqclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRmks As common.Controls.MyTextBox
    Friend WithEvents txtCustOrderNo As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtExpireDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtReqNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents cboModeOfTransport As common.Controls.MyComboBox
    Friend WithEvents txtRequiredDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents txtRequestBy As common.Controls.MyTextBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents chkRateDefaultSetting As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRateUserCustomer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents chkqclose As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdbEmailSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnPreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSend As Telerik.WinControls.UI.RadMenuItem

End Class

