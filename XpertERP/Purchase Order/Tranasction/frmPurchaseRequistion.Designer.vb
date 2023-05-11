<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurchaseRequistion
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
        Me.chkTender = New common.Controls.MyCheckBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.pnl_capex = New System.Windows.Forms.Panel()
        Me.txtEmail = New common.Controls.MyTextBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.lblEmail = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.lbl_rebudgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_rebudgetamt = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lbl_budgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_budgetamt = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblApprovalDate = New common.Controls.MyLabel()
        Me.chk_emergency = New common.Controls.MyCheckBox()
        Me.ddl_category = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.fndcapexsubcode = New common.UserControls.txtFinder()
        Me.lbl_capexsubcode = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.fndcapexcode = New common.UserControls.txtFinder()
        Me.lbl_capexcode = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.chkOpenPO = New common.Controls.MyCheckBox()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.lblProject = New common.Controls.MyLabel()
        Me.cboPOType = New common.Controls.MyComboBox()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.txtRequestBy = New common.Controls.MyTextBox()
        Me.lblDept = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
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
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.chkInternal = New Telerik.WinControls.UI.RadCheckBox()
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
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnEmailsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.BtnMailPreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnMailSendemail = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkprclose = New System.Windows.Forms.CheckBox()
        Me.btnUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SaveLayoutbtn = New Telerik.WinControls.UI.RadMenuItem()
        Me.DeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RdEmailAndSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.WorkOrder = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblTo = New common.Controls.MyLabel()
        Me.txtTo = New common.Controls.MyTextBox()
        Me.txtSubject = New common.Controls.MyTextBox()
        Me.txtContent = New common.Controls.MyTextBox()
        Me.txtCopySubmit = New common.Controls.MyTextBox()
        Me.lblSubject = New common.Controls.MyLabel()
        Me.lblContent = New common.Controls.MyLabel()
        Me.lblSubmittedto = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkTender, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.pnl_capex.SuspendLayout()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovalDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_emergency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOpenPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        CType(Me.btnEmailsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WorkOrder.SuspendLayout()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCopySubmit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubmittedto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnEmailsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkprclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(935, 509)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.WorkOrder)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(935, 465)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "Custom Fields"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkTender)
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.Controls.Add(Me.chkOpenPO)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage1.Controls.Add(Me.cboPOType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequestBy)
        Me.RadPageViewPage1.Controls.Add(Me.lblDept)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.txtDept)
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
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkInternal)
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
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(99.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(914, 419)
        Me.RadPageViewPage1.Text = "Purchase Indent"
        '
        'chkTender
        '
        Me.chkTender.Location = New System.Drawing.Point(311, 108)
        Me.chkTender.MyLinkLable1 = Nothing
        Me.chkTender.MyLinkLable2 = Nothing
        Me.chkTender.Name = "chkTender"
        Me.chkTender.Size = New System.Drawing.Size(55, 18)
        Me.chkTender.TabIndex = 73
        Me.chkTender.Tag1 = Nothing
        Me.chkTender.Text = "Tender"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(3, 129)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.pnl_capex)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer3.Size = New System.Drawing.Size(908, 218)
        Me.SplitContainer3.SplitterDistance = 92
        Me.SplitContainer3.TabIndex = 72
        '
        'pnl_capex
        '
        Me.pnl_capex.Controls.Add(Me.txtEmail)
        Me.pnl_capex.Controls.Add(Me.lblEmail)
        Me.pnl_capex.Controls.Add(Me.MyLabel37)
        Me.pnl_capex.Controls.Add(Me.MyLabel38)
        Me.pnl_capex.Controls.Add(Me.MyLabel40)
        Me.pnl_capex.Controls.Add(Me.lbl_rebudgetamtwithtolerence)
        Me.pnl_capex.Controls.Add(Me.lbl_rebudgetamt)
        Me.pnl_capex.Controls.Add(Me.MyLabel35)
        Me.pnl_capex.Controls.Add(Me.lbl_budgetamtwithtolerence)
        Me.pnl_capex.Controls.Add(Me.lbl_budgetamt)
        Me.pnl_capex.Controls.Add(Me.MyLabel3)
        Me.pnl_capex.Controls.Add(Me.lblApprovalDate)
        Me.pnl_capex.Controls.Add(Me.chk_emergency)
        Me.pnl_capex.Controls.Add(Me.ddl_category)
        Me.pnl_capex.Controls.Add(Me.MyLabel2)
        Me.pnl_capex.Controls.Add(Me.MyLabel36)
        Me.pnl_capex.Controls.Add(Me.fndcapexsubcode)
        Me.pnl_capex.Controls.Add(Me.lbl_capexsubcode)
        Me.pnl_capex.Controls.Add(Me.MyLabel34)
        Me.pnl_capex.Controls.Add(Me.fndcapexcode)
        Me.pnl_capex.Controls.Add(Me.lbl_capexcode)
        Me.pnl_capex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_capex.Location = New System.Drawing.Point(0, 0)
        Me.pnl_capex.Name = "pnl_capex"
        Me.pnl_capex.Size = New System.Drawing.Size(908, 92)
        Me.pnl_capex.TabIndex = 71
        '
        'txtEmail
        '
        Me.txtEmail.CalculationExpression = Nothing
        Me.txtEmail.FieldCode = Nothing
        Me.txtEmail.FieldDesc = Nothing
        Me.txtEmail.FieldMaxLength = 0
        Me.txtEmail.FieldName = Nothing
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.isCalculatedField = False
        Me.txtEmail.IsSourceFromTable = False
        Me.txtEmail.IsSourceFromValueList = False
        Me.txtEmail.IsUnique = False
        Me.txtEmail.Location = New System.Drawing.Point(569, 51)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.RadLabel7
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReferenceFieldDesc = Nothing
        Me.txtEmail.ReferenceFieldName = Nothing
        Me.txtEmail.ReferenceTableName = Nothing
        Me.txtEmail.Size = New System.Drawing.Size(336, 18)
        Me.txtEmail.TabIndex = 55
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(463, 25)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(105, 16)
        Me.RadLabel7.TabIndex = 27
        Me.RadLabel7.Text = "Customer Order No"
        '
        'lblEmail
        '
        Me.lblEmail.FieldName = Nothing
        Me.lblEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(468, 51)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(47, 16)
        Me.lblEmail.TabIndex = 54
        Me.lblEmail.Text = "Email Id"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(211, 72)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(147, 16)
        Me.MyLabel37.TabIndex = 52
        Me.MyLabel37.Text = "Bal. Amount With Tolerence"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(211, 50)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(125, 16)
        Me.MyLabel38.TabIndex = 48
        Me.MyLabel38.Text = "Amount With Tolerence"
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(5, 72)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel40.TabIndex = 50
        Me.MyLabel40.Text = "Bal. Amount"
        '
        'lbl_rebudgetamtwithtolerence
        '
        Me.lbl_rebudgetamtwithtolerence.AutoSize = False
        Me.lbl_rebudgetamtwithtolerence.BorderVisible = True
        Me.lbl_rebudgetamtwithtolerence.FieldName = Nothing
        Me.lbl_rebudgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamtwithtolerence.Location = New System.Drawing.Point(363, 71)
        Me.lbl_rebudgetamtwithtolerence.Name = "lbl_rebudgetamtwithtolerence"
        Me.lbl_rebudgetamtwithtolerence.Size = New System.Drawing.Size(100, 19)
        Me.lbl_rebudgetamtwithtolerence.TabIndex = 53
        Me.lbl_rebudgetamtwithtolerence.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_rebudgetamtwithtolerence.TextWrap = False
        '
        'lbl_rebudgetamt
        '
        Me.lbl_rebudgetamt.AutoSize = False
        Me.lbl_rebudgetamt.BorderVisible = True
        Me.lbl_rebudgetamt.FieldName = Nothing
        Me.lbl_rebudgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamt.Location = New System.Drawing.Point(109, 71)
        Me.lbl_rebudgetamt.Name = "lbl_rebudgetamt"
        Me.lbl_rebudgetamt.Size = New System.Drawing.Size(100, 19)
        Me.lbl_rebudgetamt.TabIndex = 51
        Me.lbl_rebudgetamt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_rebudgetamt.TextWrap = False
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(5, 50)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel35.TabIndex = 46
        Me.MyLabel35.Text = "Amount"
        '
        'lbl_budgetamtwithtolerence
        '
        Me.lbl_budgetamtwithtolerence.AutoSize = False
        Me.lbl_budgetamtwithtolerence.BorderVisible = True
        Me.lbl_budgetamtwithtolerence.FieldName = Nothing
        Me.lbl_budgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamtwithtolerence.Location = New System.Drawing.Point(363, 49)
        Me.lbl_budgetamtwithtolerence.Name = "lbl_budgetamtwithtolerence"
        Me.lbl_budgetamtwithtolerence.Size = New System.Drawing.Size(100, 19)
        Me.lbl_budgetamtwithtolerence.TabIndex = 49
        Me.lbl_budgetamtwithtolerence.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_budgetamtwithtolerence.TextWrap = False
        '
        'lbl_budgetamt
        '
        Me.lbl_budgetamt.AutoSize = False
        Me.lbl_budgetamt.BorderVisible = True
        Me.lbl_budgetamt.FieldName = Nothing
        Me.lbl_budgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamt.Location = New System.Drawing.Point(109, 49)
        Me.lbl_budgetamt.Name = "lbl_budgetamt"
        Me.lbl_budgetamt.Size = New System.Drawing.Size(100, 19)
        Me.lbl_budgetamt.TabIndex = 47
        Me.lbl_budgetamt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_budgetamt.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(468, 6)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel3.TabIndex = 44
        Me.MyLabel3.Text = "Approval Date"
        '
        'lblApprovalDate
        '
        Me.lblApprovalDate.AutoSize = False
        Me.lblApprovalDate.BorderVisible = True
        Me.lblApprovalDate.FieldName = Nothing
        Me.lblApprovalDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovalDate.Location = New System.Drawing.Point(569, 4)
        Me.lblApprovalDate.Name = "lblApprovalDate"
        Me.lblApprovalDate.Size = New System.Drawing.Size(193, 19)
        Me.lblApprovalDate.TabIndex = 45
        Me.lblApprovalDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblApprovalDate.TextWrap = False
        '
        'chk_emergency
        '
        Me.chk_emergency.Location = New System.Drawing.Point(258, 6)
        Me.chk_emergency.MyLinkLable1 = Nothing
        Me.chk_emergency.MyLinkLable2 = Nothing
        Me.chk_emergency.Name = "chk_emergency"
        Me.chk_emergency.Size = New System.Drawing.Size(75, 18)
        Me.chk_emergency.TabIndex = 43
        Me.chk_emergency.Tag1 = Nothing
        Me.chk_emergency.Text = "Emergency"
        '
        'ddl_category
        '
        Me.ddl_category.AutoCompleteDisplayMember = Nothing
        Me.ddl_category.AutoCompleteValueMember = Nothing
        Me.ddl_category.CalculationExpression = Nothing
        Me.ddl_category.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddl_category.FieldCode = Nothing
        Me.ddl_category.FieldDesc = Nothing
        Me.ddl_category.FieldMaxLength = 0
        Me.ddl_category.FieldName = Nothing
        Me.ddl_category.isCalculatedField = False
        Me.ddl_category.IsSourceFromTable = False
        Me.ddl_category.IsSourceFromValueList = False
        Me.ddl_category.IsUnique = False
        Me.ddl_category.Location = New System.Drawing.Point(109, 4)
        Me.ddl_category.MendatroryField = True
        Me.ddl_category.MyLinkLable1 = Me.MyLabel2
        Me.ddl_category.MyLinkLable2 = Nothing
        Me.ddl_category.Name = "ddl_category"
        Me.ddl_category.ReferenceFieldDesc = Nothing
        Me.ddl_category.ReferenceFieldName = Nothing
        Me.ddl_category.ReferenceTableName = Nothing
        Me.ddl_category.Size = New System.Drawing.Size(136, 20)
        Me.ddl_category.TabIndex = 41
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel2.TabIndex = 42
        Me.MyLabel2.Text = "Category"
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(5, 27)
        Me.MyLabel36.Name = "MyLabel36"
        Me.MyLabel36.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel36.TabIndex = 7
        Me.MyLabel36.Text = "Capex Sub-Code"
        '
        'fndcapexsubcode
        '
        Me.fndcapexsubcode.CalculationExpression = Nothing
        Me.fndcapexsubcode.FieldCode = Nothing
        Me.fndcapexsubcode.FieldDesc = Nothing
        Me.fndcapexsubcode.FieldMaxLength = 0
        Me.fndcapexsubcode.FieldName = Nothing
        Me.fndcapexsubcode.isCalculatedField = False
        Me.fndcapexsubcode.IsSourceFromTable = False
        Me.fndcapexsubcode.IsSourceFromValueList = False
        Me.fndcapexsubcode.IsUnique = False
        Me.fndcapexsubcode.Location = New System.Drawing.Point(109, 27)
        Me.fndcapexsubcode.MendatroryField = True
        Me.fndcapexsubcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexsubcode.MyLinkLable1 = Me.MyLabel36
        Me.fndcapexsubcode.MyLinkLable2 = Me.lbl_capexsubcode
        Me.fndcapexsubcode.MyReadOnly = False
        Me.fndcapexsubcode.MyShowMasterFormButton = False
        Me.fndcapexsubcode.Name = "fndcapexsubcode"
        Me.fndcapexsubcode.ReferenceFieldDesc = Nothing
        Me.fndcapexsubcode.ReferenceFieldName = Nothing
        Me.fndcapexsubcode.ReferenceTableName = Nothing
        Me.fndcapexsubcode.Size = New System.Drawing.Size(143, 20)
        Me.fndcapexsubcode.TabIndex = 6
        Me.fndcapexsubcode.Value = ""
        '
        'lbl_capexsubcode
        '
        Me.lbl_capexsubcode.AutoSize = False
        Me.lbl_capexsubcode.BorderVisible = True
        Me.lbl_capexsubcode.FieldName = Nothing
        Me.lbl_capexsubcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexsubcode.Location = New System.Drawing.Point(258, 27)
        Me.lbl_capexsubcode.Name = "lbl_capexsubcode"
        Me.lbl_capexsubcode.Size = New System.Drawing.Size(205, 19)
        Me.lbl_capexsubcode.TabIndex = 8
        Me.lbl_capexsubcode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_capexsubcode.TextWrap = False
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(467, 30)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel34.TabIndex = 4
        Me.MyLabel34.Text = "Capex Code"
        '
        'fndcapexcode
        '
        Me.fndcapexcode.CalculationExpression = Nothing
        Me.fndcapexcode.Enabled = False
        Me.fndcapexcode.FieldCode = Nothing
        Me.fndcapexcode.FieldDesc = Nothing
        Me.fndcapexcode.FieldMaxLength = 0
        Me.fndcapexcode.FieldName = Nothing
        Me.fndcapexcode.isCalculatedField = False
        Me.fndcapexcode.IsSourceFromTable = False
        Me.fndcapexcode.IsSourceFromValueList = False
        Me.fndcapexcode.IsUnique = False
        Me.fndcapexcode.Location = New System.Drawing.Point(569, 27)
        Me.fndcapexcode.MendatroryField = False
        Me.fndcapexcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexcode.MyLinkLable1 = Me.MyLabel34
        Me.fndcapexcode.MyLinkLable2 = Me.lbl_capexcode
        Me.fndcapexcode.MyReadOnly = False
        Me.fndcapexcode.MyShowMasterFormButton = False
        Me.fndcapexcode.Name = "fndcapexcode"
        Me.fndcapexcode.ReferenceFieldDesc = Nothing
        Me.fndcapexcode.ReferenceFieldName = Nothing
        Me.fndcapexcode.ReferenceTableName = Nothing
        Me.fndcapexcode.Size = New System.Drawing.Size(143, 19)
        Me.fndcapexcode.TabIndex = 3
        Me.fndcapexcode.Value = ""
        '
        'lbl_capexcode
        '
        Me.lbl_capexcode.AutoSize = False
        Me.lbl_capexcode.BorderVisible = True
        Me.lbl_capexcode.FieldName = Nothing
        Me.lbl_capexcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexcode.Location = New System.Drawing.Point(715, 28)
        Me.lbl_capexcode.Name = "lbl_capexcode"
        Me.lbl_capexcode.Size = New System.Drawing.Size(192, 19)
        Me.lbl_capexcode.TabIndex = 5
        Me.lbl_capexcode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_capexcode.TextWrap = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(908, 122)
        Me.RadGroupBox2.TabIndex = 18
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
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(888, 92)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'chkOpenPO
        '
        Me.chkOpenPO.Location = New System.Drawing.Point(228, 107)
        Me.chkOpenPO.MyLinkLable1 = Nothing
        Me.chkOpenPO.MyLinkLable2 = Nothing
        Me.chkOpenPO.Name = "chkOpenPO"
        Me.chkOpenPO.Size = New System.Drawing.Size(77, 18)
        Me.chkOpenPO.TabIndex = 37
        Me.chkOpenPO.Tag1 = Nothing
        Me.chkOpenPO.Text = "Is Open PO"
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteDisplayMember = Nothing
        Me.cboItemType.AutoCompleteValueMember = Nothing
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(86, 85)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.MyLabel1
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(136, 20)
        Me.cboItemType.TabIndex = 38
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(1, 108)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 31
        Me.MyLabel1.Text = "Type"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = True
        Me.UcItemBalance1.CommitedQtyLbl = True
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 353)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 65)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 65)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 65)
        Me.UcItemBalance1.TabIndex = 20
        Me.UcItemBalance1.TabStop = False
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.RadLabel15)
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Location = New System.Drawing.Point(457, 106)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(455, 22)
        Me.pnlPCJ.TabIndex = 17
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(6, 1)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(41, 16)
        Me.RadLabel15.TabIndex = 1
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
        Me.fndProject.Location = New System.Drawing.Point(115, 1)
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
        Me.fndProject.Size = New System.Drawing.Size(143, 19)
        Me.fndProject.TabIndex = 0
        Me.fndProject.Value = ""
        '
        'lblProject
        '
        Me.lblProject.AutoSize = False
        Me.lblProject.BorderVisible = True
        Me.lblProject.FieldName = Nothing
        Me.lblProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProject.Location = New System.Drawing.Point(261, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(193, 20)
        Me.lblProject.TabIndex = 2
        Me.lblProject.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblProject.TextWrap = False
        '
        'cboPOType
        '
        Me.cboPOType.AutoCompleteDisplayMember = Nothing
        Me.cboPOType.AutoCompleteValueMember = Nothing
        Me.cboPOType.CalculationExpression = Nothing
        Me.cboPOType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPOType.FieldCode = Nothing
        Me.cboPOType.FieldDesc = Nothing
        Me.cboPOType.FieldMaxLength = 0
        Me.cboPOType.FieldName = Nothing
        Me.cboPOType.isCalculatedField = False
        Me.cboPOType.IsSourceFromTable = False
        Me.cboPOType.IsSourceFromValueList = False
        Me.cboPOType.IsUnique = False
        Me.cboPOType.Location = New System.Drawing.Point(86, 107)
        Me.cboPOType.MendatroryField = True
        Me.cboPOType.MyLinkLable1 = Me.MyLabel1
        Me.cboPOType.MyLinkLable2 = Nothing
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.ReferenceFieldDesc = Nothing
        Me.cboPOType.ReferenceFieldName = Nothing
        Me.cboPOType.ReferenceTableName = Nothing
        Me.cboPOType.Size = New System.Drawing.Size(136, 20)
        Me.cboPOType.TabIndex = 16
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(227, 86)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel12.TabIndex = 30
        Me.RadLabel12.Text = "Requested By"
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
        Me.txtRequestBy.Location = New System.Drawing.Point(310, 85)
        Me.txtRequestBy.MaxLength = 100
        Me.txtRequestBy.MendatroryField = False
        Me.txtRequestBy.MyLinkLable1 = Me.RadLabel12
        Me.txtRequestBy.MyLinkLable2 = Nothing
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.ReferenceFieldDesc = Nothing
        Me.txtRequestBy.ReferenceFieldName = Nothing
        Me.txtRequestBy.ReferenceTableName = Nothing
        Me.txtRequestBy.Size = New System.Drawing.Size(148, 18)
        Me.txtRequestBy.TabIndex = 14
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(718, 83)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(193, 20)
        Me.lblDept.TabIndex = 19
        Me.lblDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(463, 86)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel14.TabIndex = 24
        Me.RadLabel14.Text = "Department"
        '
        'txtDept
        '
        Me.txtDept.CalculationExpression = Nothing
        Me.txtDept.FieldCode = Nothing
        Me.txtDept.FieldDesc = Nothing
        Me.txtDept.FieldMaxLength = 0
        Me.txtDept.FieldName = Nothing
        Me.txtDept.isCalculatedField = False
        Me.txtDept.IsSourceFromTable = False
        Me.txtDept.IsSourceFromValueList = False
        Me.txtDept.IsUnique = False
        Me.txtDept.Location = New System.Drawing.Point(572, 84)
        Me.txtDept.MendatroryField = True
        Me.txtDept.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.MyLinkLable1 = Me.RadLabel14
        Me.txtDept.MyLinkLable2 = Me.lblDept
        Me.txtDept.MyReadOnly = False
        Me.txtDept.MyShowMasterFormButton = False
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReferenceFieldDesc = Nothing
        Me.txtDept.ReferenceFieldName = Nothing
        Me.txtDept.ReferenceTableName = Nothing
        Me.txtDept.Size = New System.Drawing.Size(143, 19)
        Me.txtDept.TabIndex = 15
        Me.txtDept.Value = ""
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(1, 86)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(66, 16)
        Me.RadLabel10.TabIndex = 32
        Me.RadLabel10.Text = "Indent Type"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(718, 45)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel9.TabIndex = 21
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
        Me.txtRequiredDate.Location = New System.Drawing.Point(797, 43)
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
        Me.txtRequiredDate.TabIndex = 10
        Me.txtRequiredDate.TabStop = False
        Me.txtRequiredDate.Text = "13/06/2011"
        Me.txtRequiredDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'cboModeOfTransport
        '
        Me.cboModeOfTransport.AutoCompleteDisplayMember = Nothing
        Me.cboModeOfTransport.AutoCompleteValueMember = Nothing
        Me.cboModeOfTransport.CalculationExpression = Nothing
        Me.cboModeOfTransport.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModeOfTransport.FieldCode = Nothing
        Me.cboModeOfTransport.FieldDesc = Nothing
        Me.cboModeOfTransport.FieldMaxLength = 0
        Me.cboModeOfTransport.FieldName = Nothing
        Me.cboModeOfTransport.isCalculatedField = False
        Me.cboModeOfTransport.IsSourceFromTable = False
        Me.cboModeOfTransport.IsSourceFromValueList = False
        Me.cboModeOfTransport.IsUnique = False
        Me.cboModeOfTransport.Location = New System.Drawing.Point(572, 42)
        Me.cboModeOfTransport.MendatroryField = False
        Me.cboModeOfTransport.MyLinkLable1 = Me.RadLabel11
        Me.cboModeOfTransport.MyLinkLable2 = Nothing
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.ReferenceFieldDesc = Nothing
        Me.cboModeOfTransport.ReferenceFieldName = Nothing
        Me.cboModeOfTransport.ReferenceTableName = Nothing
        Me.cboModeOfTransport.Size = New System.Drawing.Size(143, 20)
        Me.cboModeOfTransport.TabIndex = 9
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(463, 45)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel11.TabIndex = 26
        Me.RadLabel11.Text = "Mode Of Transport"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(463, 3)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel13.TabIndex = 28
        Me.RadLabel13.Text = "Expiry Date"
        '
        'RadLabel27
        '
        Me.RadLabel27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(724, 402)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel27.TabIndex = 20
        Me.RadLabel27.Text = "Total Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(803, 400)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 19
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(718, 63)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(193, 20)
        Me.lblLocation.TabIndex = 20
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(1, 25)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 35
        Me.RadLabel5.Text = "Description"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(718, 25)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel3.TabIndex = 22
        Me.RadLabel3.Text = "Reference No"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(1, 65)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 33
        Me.RadLabel2.Text = "Comment"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(1, 45)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 34
        Me.RadLabel8.Text = "Remarks"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(463, 65)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel6.TabIndex = 25
        Me.RadLabel6.Text = "Location"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(348, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 29
        Me.RadLabel4.Text = "Date"
        '
        'chkInternal
        '
        Me.chkInternal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInternal.Location = New System.Drawing.Point(718, 3)
        Me.chkInternal.Name = "chkInternal"
        Me.chkInternal.Size = New System.Drawing.Size(58, 16)
        Me.chkInternal.TabIndex = 4
        Me.chkInternal.Text = "Internal"
        Me.chkInternal.Visible = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(1, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel1.TabIndex = 36
        Me.RadLabel1.Text = "Indent No"
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
        Me.txtLocation.Location = New System.Drawing.Point(572, 64)
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
        Me.txtLocation.TabIndex = 12
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
        Me.UsLock1.TabIndex = 23
        '
        'txtReqNo
        '
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.Location = New System.Drawing.Point(86, 1)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtReqNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel1
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyMaxLength = 32767
        Me.txtReqNo.MyReadOnly = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.Size = New System.Drawing.Size(230, 20)
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
        Me.txtExpireDate.Location = New System.Drawing.Point(572, 2)
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
        Me.txtExpireDate.TabIndex = 3
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
        Me.txtComment.Location = New System.Drawing.Point(86, 64)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(372, 18)
        Me.txtComment.TabIndex = 11
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
        Me.txtRefNo.Location = New System.Drawing.Point(797, 23)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel3
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(114, 18)
        Me.txtRefNo.TabIndex = 7
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
        Me.txtRmks.Location = New System.Drawing.Point(86, 44)
        Me.txtRmks.MaxLength = 200
        Me.txtRmks.MendatroryField = False
        Me.txtRmks.MyLinkLable1 = Me.RadLabel8
        Me.txtRmks.MyLinkLable2 = Nothing
        Me.txtRmks.Name = "txtRmks"
        Me.txtRmks.ReferenceFieldDesc = Nothing
        Me.txtRmks.ReferenceFieldName = Nothing
        Me.txtRmks.ReferenceTableName = Nothing
        Me.txtRmks.Size = New System.Drawing.Size(372, 18)
        Me.txtRmks.TabIndex = 8
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
        Me.txtCustOrderNo.Location = New System.Drawing.Point(572, 22)
        Me.txtCustOrderNo.MaxLength = 50
        Me.txtCustOrderNo.MendatroryField = False
        Me.txtCustOrderNo.MyLinkLable1 = Me.RadLabel7
        Me.txtCustOrderNo.MyLinkLable2 = Nothing
        Me.txtCustOrderNo.Name = "txtCustOrderNo"
        Me.txtCustOrderNo.ReferenceFieldDesc = Nothing
        Me.txtCustOrderNo.ReferenceFieldName = Nothing
        Me.txtCustOrderNo.ReferenceTableName = Nothing
        Me.txtCustOrderNo.Size = New System.Drawing.Size(143, 18)
        Me.txtCustOrderNo.TabIndex = 6
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
        Me.txtDate.TabIndex = 2
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
        Me.txtDesc.Location = New System.Drawing.Point(86, 24)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(372, 18)
        Me.txtDesc.TabIndex = 5
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(317, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(914, 419)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(914, 419)
        Me.UcCustomFields1.TabIndex = 0
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(914, 419)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(914, 419)
        Me.UcAttachment1.TabIndex = 0
        '
        'btnEmailsetting
        '
        Me.btnEmailsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEmailsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnMailPreview, Me.btnMailSendemail})
        Me.btnEmailsetting.Location = New System.Drawing.Point(305, 6)
        Me.btnEmailsetting.Name = "btnEmailsetting"
        Me.btnEmailsetting.Size = New System.Drawing.Size(83, 22)
        Me.btnEmailsetting.TabIndex = 9
        Me.btnEmailsetting.Text = "E-Mail/SMS"
        '
        'BtnMailPreview
        '
        Me.BtnMailPreview.AccessibleDescription = "Preview"
        Me.BtnMailPreview.AccessibleName = "Preview"
        Me.BtnMailPreview.Name = "BtnMailPreview"
        Me.BtnMailPreview.Text = "Preview"
        '
        'btnMailSendemail
        '
        Me.btnMailSendemail.AccessibleDescription = "Send E-Mail/SMS"
        Me.btnMailSendemail.AccessibleName = "Send E-Mail/SMS"
        Me.btnMailSendemail.Name = "btnMailSendemail"
        Me.btnMailSendemail.Text = "Send E-Mail/SMS"
        '
        'chkprclose
        '
        Me.chkprclose.AutoSize = True
        Me.chkprclose.BackColor = System.Drawing.Color.Transparent
        Me.chkprclose.Location = New System.Drawing.Point(691, 8)
        Me.chkprclose.Name = "chkprclose"
        Me.chkprclose.Size = New System.Drawing.Size(157, 18)
        Me.chkprclose.TabIndex = 5
        Me.chkprclose.Text = "Close Purchase Requisition"
        Me.chkprclose.UseVisualStyleBackColor = False
        '
        'btnUnpost
        '
        Me.btnUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(394, 6)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(69, 22)
        Me.btnUnpost.TabIndex = 4
        Me.btnUnpost.Text = "Reverse"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(230, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(155, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(861, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(935, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Layout"
        Me.RadMenuItem1.AccessibleName = "Layout"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.SaveLayoutbtn, Me.DeleteLayout, Me.RdEmailAndSmsSetting})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'SaveLayoutbtn
        '
        Me.SaveLayoutbtn.AccessibleDescription = "Save Layout"
        Me.SaveLayoutbtn.AccessibleName = "Save Layout"
        Me.SaveLayoutbtn.Name = "SaveLayoutbtn"
        Me.SaveLayoutbtn.Text = "Save Layout"
        '
        'DeleteLayout
        '
        Me.DeleteLayout.AccessibleDescription = "Delete Layout"
        Me.DeleteLayout.AccessibleName = "Delete Layout"
        Me.DeleteLayout.Name = "DeleteLayout"
        Me.DeleteLayout.Text = "Delete Layout"
        '
        'RdEmailAndSmsSetting
        '
        Me.RdEmailAndSmsSetting.AccessibleDescription = "Email And SMS Setting"
        Me.RdEmailAndSmsSetting.AccessibleName = "Email And SMS Setting"
        Me.RdEmailAndSmsSetting.Name = "RdEmailAndSmsSetting"
        Me.RdEmailAndSmsSetting.Text = "Email And SMS Setting"
        '
        'WorkOrder
        '
        Me.WorkOrder.Controls.Add(Me.lblSubmittedto)
        Me.WorkOrder.Controls.Add(Me.lblContent)
        Me.WorkOrder.Controls.Add(Me.lblSubject)
        Me.WorkOrder.Controls.Add(Me.txtCopySubmit)
        Me.WorkOrder.Controls.Add(Me.txtContent)
        Me.WorkOrder.Controls.Add(Me.txtSubject)
        Me.WorkOrder.Controls.Add(Me.txtTo)
        Me.WorkOrder.Controls.Add(Me.lblTo)
        Me.WorkOrder.ItemSize = New System.Drawing.SizeF(75.0!, 26.0!)
        Me.WorkOrder.Location = New System.Drawing.Point(10, 35)
        Me.WorkOrder.Name = "WorkOrder"
        Me.WorkOrder.Size = New System.Drawing.Size(914, 419)
        Me.WorkOrder.Text = "Work Order"
        '
        'lblTo
        '
        Me.lblTo.FieldName = Nothing
        Me.lblTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(16, 19)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(19, 16)
        Me.lblTo.TabIndex = 43
        Me.lblTo.Text = "To"
        '
        'txtTo
        '
        Me.txtTo.AutoSize = False
        Me.txtTo.CalculationExpression = Nothing
        Me.txtTo.FieldCode = Nothing
        Me.txtTo.FieldDesc = Nothing
        Me.txtTo.FieldMaxLength = 0
        Me.txtTo.FieldName = Nothing
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.isCalculatedField = False
        Me.txtTo.IsSourceFromTable = False
        Me.txtTo.IsSourceFromValueList = False
        Me.txtTo.IsUnique = False
        Me.txtTo.Location = New System.Drawing.Point(118, 19)
        Me.txtTo.MaxLength = 200
        Me.txtTo.MendatroryField = False
        Me.txtTo.Multiline = True
        Me.txtTo.MyLinkLable1 = Me.MyLabel2
        Me.txtTo.MyLinkLable2 = Nothing
        Me.txtTo.Name = "txtTo"
        Me.txtTo.ReferenceFieldDesc = Nothing
        Me.txtTo.ReferenceFieldName = Nothing
        Me.txtTo.ReferenceTableName = Nothing
        Me.txtTo.Size = New System.Drawing.Size(536, 38)
        Me.txtTo.TabIndex = 48
        '
        'txtSubject
        '
        Me.txtSubject.AutoSize = False
        Me.txtSubject.CalculationExpression = Nothing
        Me.txtSubject.FieldCode = Nothing
        Me.txtSubject.FieldDesc = Nothing
        Me.txtSubject.FieldMaxLength = 0
        Me.txtSubject.FieldName = Nothing
        Me.txtSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubject.isCalculatedField = False
        Me.txtSubject.IsSourceFromTable = False
        Me.txtSubject.IsSourceFromValueList = False
        Me.txtSubject.IsUnique = False
        Me.txtSubject.Location = New System.Drawing.Point(118, 66)
        Me.txtSubject.MaxLength = 200
        Me.txtSubject.MendatroryField = False
        Me.txtSubject.Multiline = True
        Me.txtSubject.MyLinkLable1 = Me.MyLabel2
        Me.txtSubject.MyLinkLable2 = Nothing
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.ReferenceFieldDesc = Nothing
        Me.txtSubject.ReferenceFieldName = Nothing
        Me.txtSubject.ReferenceTableName = Nothing
        Me.txtSubject.Size = New System.Drawing.Size(536, 38)
        Me.txtSubject.TabIndex = 49
        '
        'txtContent
        '
        Me.txtContent.AutoSize = False
        Me.txtContent.CalculationExpression = Nothing
        Me.txtContent.FieldCode = Nothing
        Me.txtContent.FieldDesc = Nothing
        Me.txtContent.FieldMaxLength = 0
        Me.txtContent.FieldName = Nothing
        Me.txtContent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContent.isCalculatedField = False
        Me.txtContent.IsSourceFromTable = False
        Me.txtContent.IsSourceFromValueList = False
        Me.txtContent.IsUnique = False
        Me.txtContent.Location = New System.Drawing.Point(118, 114)
        Me.txtContent.MaxLength = 200
        Me.txtContent.MendatroryField = False
        Me.txtContent.Multiline = True
        Me.txtContent.MyLinkLable1 = Me.MyLabel2
        Me.txtContent.MyLinkLable2 = Nothing
        Me.txtContent.Name = "txtContent"
        Me.txtContent.ReferenceFieldDesc = Nothing
        Me.txtContent.ReferenceFieldName = Nothing
        Me.txtContent.ReferenceTableName = Nothing
        Me.txtContent.Size = New System.Drawing.Size(536, 38)
        Me.txtContent.TabIndex = 50
        '
        'txtCopySubmit
        '
        Me.txtCopySubmit.AutoSize = False
        Me.txtCopySubmit.CalculationExpression = Nothing
        Me.txtCopySubmit.FieldCode = Nothing
        Me.txtCopySubmit.FieldDesc = Nothing
        Me.txtCopySubmit.FieldMaxLength = 0
        Me.txtCopySubmit.FieldName = Nothing
        Me.txtCopySubmit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopySubmit.isCalculatedField = False
        Me.txtCopySubmit.IsSourceFromTable = False
        Me.txtCopySubmit.IsSourceFromValueList = False
        Me.txtCopySubmit.IsUnique = False
        Me.txtCopySubmit.Location = New System.Drawing.Point(118, 160)
        Me.txtCopySubmit.MaxLength = 200
        Me.txtCopySubmit.MendatroryField = False
        Me.txtCopySubmit.Multiline = True
        Me.txtCopySubmit.MyLinkLable1 = Me.MyLabel2
        Me.txtCopySubmit.MyLinkLable2 = Nothing
        Me.txtCopySubmit.Name = "txtCopySubmit"
        Me.txtCopySubmit.ReferenceFieldDesc = Nothing
        Me.txtCopySubmit.ReferenceFieldName = Nothing
        Me.txtCopySubmit.ReferenceTableName = Nothing
        Me.txtCopySubmit.Size = New System.Drawing.Size(536, 38)
        Me.txtCopySubmit.TabIndex = 51
        '
        'lblSubject
        '
        Me.lblSubject.FieldName = Nothing
        Me.lblSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubject.Location = New System.Drawing.Point(16, 66)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(44, 16)
        Me.lblSubject.TabIndex = 52
        Me.lblSubject.Text = "Subject"
        '
        'lblContent
        '
        Me.lblContent.FieldName = Nothing
        Me.lblContent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContent.Location = New System.Drawing.Point(16, 114)
        Me.lblContent.Name = "lblContent"
        Me.lblContent.Size = New System.Drawing.Size(46, 16)
        Me.lblContent.TabIndex = 53
        Me.lblContent.Text = "Content"
        '
        'lblSubmittedto
        '
        Me.lblSubmittedto.FieldName = Nothing
        Me.lblSubmittedto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubmittedto.Location = New System.Drawing.Point(16, 160)
        Me.lblSubmittedto.Name = "lblSubmittedto"
        Me.lblSubmittedto.Size = New System.Drawing.Size(99, 16)
        Me.lblSubmittedto.TabIndex = 54
        Me.lblSubmittedto.Text = "Copy Submitted to"
        '
        'frmPurchaseRequistion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 529)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmPurchaseRequistion"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Purchase Indent"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkTender, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.pnl_capex.ResumeLayout(False)
        Me.pnl_capex.PerformLayout()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovalDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_emergency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddl_category, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOpenPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInternal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnEmailsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WorkOrder.ResumeLayout(False)
        Me.WorkOrder.PerformLayout()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCopySubmit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubmittedto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkInternal As Telerik.WinControls.UI.RadCheckBox
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
    Friend WithEvents txtDept As common.UserControls.txtFinder
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
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents cboPOType As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnUnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents chkprclose As System.Windows.Forms.CheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveLayoutbtn As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RdEmailAndSmsSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnEmailsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BtnMailPreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnMailSendemail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkOpenPO As common.Controls.MyCheckBox
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents ddl_category As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents pnl_capex As System.Windows.Forms.Panel
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents fndcapexsubcode As common.UserControls.txtFinder
    Friend WithEvents lbl_capexsubcode As common.Controls.MyLabel
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents fndcapexcode As common.UserControls.txtFinder
    Friend WithEvents lbl_capexcode As common.Controls.MyLabel
    Friend WithEvents chk_emergency As common.Controls.MyCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblApprovalDate As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamt As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamt As common.Controls.MyLabel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkTender As common.Controls.MyCheckBox
    Friend WithEvents lblEmail As common.Controls.MyLabel
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents WorkOrder As RadPageViewPage
    Friend WithEvents lblTo As common.Controls.MyLabel
    Friend WithEvents txtTo As common.Controls.MyTextBox
    Friend WithEvents lblSubmittedto As common.Controls.MyLabel
    Friend WithEvents lblContent As common.Controls.MyLabel
    Friend WithEvents lblSubject As common.Controls.MyLabel
    Friend WithEvents txtCopySubmit As common.Controls.MyTextBox
    Friend WithEvents txtContent As common.Controls.MyTextBox
    Friend WithEvents txtSubject As common.Controls.MyTextBox
End Class

