<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMRPForProduction
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
        Me.txtMRPDescription = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageGeneral = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gvMRPDetal = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtReqNo = New common.UserControls.txtNavigator()
        Me.pnlDepartment = New System.Windows.Forms.Panel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.lblDept = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.chkGenAutoSchedule = New common.Controls.MyCheckBox()
        Me.chkConsiderOpenPO = New common.Controls.MyCheckBox()
        Me.txtMulProductionPlan = New common.UserControls.txtMultiSelectFinder()
        Me.TxtitemType = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.chkPendignQC = New common.Controls.MyCheckBox()
        Me.chkAutoPO = New common.Controls.MyRadioButton()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.chkAutoIndent = New common.Controls.MyRadioButton()
        Me.chkPendingPO = New common.Controls.MyCheckBox()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.chkItemLevel = New common.Controls.MyCheckBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.chkStock = New common.Controls.MyCheckBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.dtpMRPDate = New common.Controls.MyDateTimePicker()
        Me.lblMRPDate = New common.Controls.MyLabel()
        Me.pageMRPDetails = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvBalQty = New common.UserControls.MyRadGridView()
        Me.pageMRPItemDetails = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvBalItemDetails = New common.UserControls.MyRadGridView()
        Me.pagePendingPO = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvPendingPO = New common.UserControls.MyRadGridView()
        Me.PagePendingSRN = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvPendingSRN = New common.UserControls.MyRadGridView()
        Me.pageAttachment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.txtMRPDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvMRPDetal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMRPDetal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlDepartment.SuspendLayout()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGenAutoSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkConsiderOpenPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtitemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPendignQC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoIndent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPendingPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMRPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMRPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageMRPDetails.SuspendLayout()
        CType(Me.gvBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBalQty.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageMRPItemDetails.SuspendLayout()
        CType(Me.gvBalItemDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBalItemDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pagePendingPO.SuspendLayout()
        CType(Me.gvPendingPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPendingPO.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PagePendingSRN.SuspendLayout()
        CType(Me.gvPendingSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPendingSRN.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageAttachment.SuspendLayout()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtMRPDescription
        '
        Me.txtMRPDescription.CalculationExpression = Nothing
        Me.txtMRPDescription.FieldCode = Nothing
        Me.txtMRPDescription.FieldDesc = Nothing
        Me.txtMRPDescription.FieldMaxLength = 0
        Me.txtMRPDescription.FieldName = Nothing
        Me.txtMRPDescription.isCalculatedField = False
        Me.txtMRPDescription.IsSourceFromTable = False
        Me.txtMRPDescription.IsSourceFromValueList = False
        Me.txtMRPDescription.IsUnique = False
        Me.txtMRPDescription.Location = New System.Drawing.Point(108, 31)
        Me.txtMRPDescription.MaxLength = 100
        Me.txtMRPDescription.MendatroryField = False
        Me.txtMRPDescription.MyLinkLable1 = Me.MyLabel3
        Me.txtMRPDescription.MyLinkLable2 = Nothing
        Me.txtMRPDescription.Name = "txtMRPDescription"
        Me.txtMRPDescription.ReferenceFieldDesc = Nothing
        Me.txtMRPDescription.ReferenceFieldName = Nothing
        Me.txtMRPDescription.ReferenceTableName = Nothing
        Me.txtMRPDescription.Size = New System.Drawing.Size(421, 20)
        Me.txtMRPDescription.TabIndex = 2
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 33)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel3.TabIndex = 275
        Me.MyLabel3.Text = "Description"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(582, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(91, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 208
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(108, 7)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblempcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(255, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempcode.Location = New System.Drawing.Point(12, 9)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(62, 16)
        Me.lblempcode.TabIndex = 206
        Me.lblempcode.Text = "MRP Code"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(364, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 0
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(773, 617)
        Me.SplitContainer1.SplitterDistance = 581
        Me.SplitContainer1.TabIndex = 203
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageGeneral)
        Me.RadPageView1.Controls.Add(Me.pageMRPDetails)
        Me.RadPageView1.Controls.Add(Me.pageMRPItemDetails)
        Me.RadPageView1.Controls.Add(Me.pagePendingPO)
        Me.RadPageView1.Controls.Add(Me.PagePendingSRN)
        Me.RadPageView1.Controls.Add(Me.pageAttachment)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 1)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageGeneral
        Me.RadPageView1.Size = New System.Drawing.Size(771, 579)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.GroupBox1)
        Me.pageGeneral.Controls.Add(Me.Panel1)
        Me.pageGeneral.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.pageGeneral.Location = New System.Drawing.Point(10, 37)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Size = New System.Drawing.Size(750, 531)
        Me.pageGeneral.Text = "General"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gvMRPDetal)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 193)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(750, 338)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item Detail"
        '
        'gvMRPDetal
        '
        Me.gvMRPDetal.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvMRPDetal.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvMRPDetal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMRPDetal.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvMRPDetal.ForeColor = System.Drawing.Color.Black
        Me.gvMRPDetal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvMRPDetal.Location = New System.Drawing.Point(3, 18)
        '
        'gvMRPDetal
        '
        Me.gvMRPDetal.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvMRPDetal.MasterTemplate.AllowAddNewRow = False
        Me.gvMRPDetal.MasterTemplate.AutoGenerateColumns = False
        Me.gvMRPDetal.MasterTemplate.EnableGrouping = False
        Me.gvMRPDetal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMRPDetal.Name = "gvMRPDetal"
        Me.gvMRPDetal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvMRPDetal.ShowHeaderCellButtons = True
        Me.gvMRPDetal.Size = New System.Drawing.Size(744, 317)
        Me.gvMRPDetal.TabIndex = 0
        Me.gvMRPDetal.TabStop = False
        Me.gvMRPDetal.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtReqNo)
        Me.Panel1.Controls.Add(Me.pnlDepartment)
        Me.Panel1.Controls.Add(Me.chkGenAutoSchedule)
        Me.Panel1.Controls.Add(Me.chkConsiderOpenPO)
        Me.Panel1.Controls.Add(Me.txtMulProductionPlan)
        Me.Panel1.Controls.Add(Me.TxtitemType)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.MyLabel6)
        Me.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.fndLocation)
        Me.Panel1.Controls.Add(Me.chkPendignQC)
        Me.Panel1.Controls.Add(Me.lblempcode)
        Me.Panel1.Controls.Add(Me.chkAutoPO)
        Me.Panel1.Controls.Add(Me.dtpFromDate)
        Me.Panel1.Controls.Add(Me.chkAutoIndent)
        Me.Panel1.Controls.Add(Me.lblFromDate)
        Me.Panel1.Controls.Add(Me.chkPendingPO)
        Me.Panel1.Controls.Add(Me.dtpToDate)
        Me.Panel1.Controls.Add(Me.chkItemLevel)
        Me.Panel1.Controls.Add(Me.MyLabel7)
        Me.Panel1.Controls.Add(Me.chkStock)
        Me.Panel1.Controls.Add(Me.lblDescription)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.UsLock1)
        Me.Panel1.Controls.Add(Me.txtMRPDescription)
        Me.Panel1.Controls.Add(Me.btnnew)
        Me.Panel1.Controls.Add(Me.dtpMRPDate)
        Me.Panel1.Controls.Add(Me.lblMRPDate)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(750, 193)
        Me.Panel1.TabIndex = 282
        '
        'txtReqNo
        '
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.Location = New System.Drawing.Point(299, 142)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtReqNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtReqNo.MyLinkLable1 = Nothing
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyMaxLength = 32767
        Me.txtReqNo.MyReadOnly = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.Size = New System.Drawing.Size(230, 20)
        Me.txtReqNo.TabIndex = 292
        Me.txtReqNo.Value = ""
        Me.txtReqNo.Visible = False
        '
        'pnlDepartment
        '
        Me.pnlDepartment.Controls.Add(Me.RadLabel14)
        Me.pnlDepartment.Controls.Add(Me.lblDept)
        Me.pnlDepartment.Controls.Add(Me.txtDept)
        Me.pnlDepartment.Location = New System.Drawing.Point(12, 161)
        Me.pnlDepartment.Name = "pnlDepartment"
        Me.pnlDepartment.Size = New System.Drawing.Size(528, 26)
        Me.pnlDepartment.TabIndex = 291
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(3, 6)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel14.TabIndex = 290
        Me.RadLabel14.Text = "Department"
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(281, 3)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(236, 20)
        Me.lblDept.TabIndex = 289
        Me.lblDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtDept.Location = New System.Drawing.Point(96, 3)
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
        Me.txtDept.Size = New System.Drawing.Size(179, 19)
        Me.txtDept.TabIndex = 288
        Me.txtDept.Value = ""
        '
        'chkGenAutoSchedule
        '
        Me.chkGenAutoSchedule.Enabled = False
        Me.chkGenAutoSchedule.Location = New System.Drawing.Point(627, 155)
        Me.chkGenAutoSchedule.MyLinkLable1 = Nothing
        Me.chkGenAutoSchedule.MyLinkLable2 = Nothing
        Me.chkGenAutoSchedule.Name = "chkGenAutoSchedule"
        Me.chkGenAutoSchedule.Size = New System.Drawing.Size(141, 18)
        Me.chkGenAutoSchedule.TabIndex = 281
        Me.chkGenAutoSchedule.Tag1 = Nothing
        Me.chkGenAutoSchedule.Text = "Generate Auto Schedule"
        Me.chkGenAutoSchedule.Visible = False
        '
        'chkConsiderOpenPO
        '
        Me.chkConsiderOpenPO.Location = New System.Drawing.Point(627, 136)
        Me.chkConsiderOpenPO.MyLinkLable1 = Nothing
        Me.chkConsiderOpenPO.MyLinkLable2 = Nothing
        Me.chkConsiderOpenPO.Name = "chkConsiderOpenPO"
        Me.chkConsiderOpenPO.Size = New System.Drawing.Size(113, 18)
        Me.chkConsiderOpenPO.TabIndex = 280
        Me.chkConsiderOpenPO.Tag1 = Nothing
        Me.chkConsiderOpenPO.Text = "Consider Open PO"
        Me.chkConsiderOpenPO.Visible = False
        '
        'txtMulProductionPlan
        '
        Me.txtMulProductionPlan.arrDispalyMember = Nothing
        Me.txtMulProductionPlan.arrValueMember = Nothing
        Me.txtMulProductionPlan.Location = New System.Drawing.Point(108, 97)
        Me.txtMulProductionPlan.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMulProductionPlan.MyLinkLable1 = Nothing
        Me.txtMulProductionPlan.MyLinkLable2 = Nothing
        Me.txtMulProductionPlan.MyNullText = "All"
        Me.txtMulProductionPlan.Name = "txtMulProductionPlan"
        Me.txtMulProductionPlan.Size = New System.Drawing.Size(421, 19)
        Me.txtMulProductionPlan.TabIndex = 287
        '
        'TxtitemType
        '
        Me.TxtitemType.AutoCompleteDisplayMember = Nothing
        Me.TxtitemType.AutoCompleteValueMember = Nothing
        Me.TxtitemType.CalculationExpression = Nothing
        Me.TxtitemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.TxtitemType.FieldCode = Nothing
        Me.TxtitemType.FieldDesc = Nothing
        Me.TxtitemType.FieldMaxLength = 0
        Me.TxtitemType.FieldName = Nothing
        Me.TxtitemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtitemType.isCalculatedField = False
        Me.TxtitemType.IsSourceFromTable = False
        Me.TxtitemType.IsSourceFromValueList = False
        Me.TxtitemType.IsUnique = False
        RadListDataItem1.Text = "Open"
        RadListDataItem2.Text = "Approved"
        RadListDataItem3.Text = "On Hold"
        RadListDataItem4.Text = "In-Active"
        Me.TxtitemType.Items.Add(RadListDataItem1)
        Me.TxtitemType.Items.Add(RadListDataItem2)
        Me.TxtitemType.Items.Add(RadListDataItem3)
        Me.TxtitemType.Items.Add(RadListDataItem4)
        Me.TxtitemType.Location = New System.Drawing.Point(108, 119)
        Me.TxtitemType.MendatroryField = True
        Me.TxtitemType.MyLinkLable1 = Me.MyLabel2
        Me.TxtitemType.MyLinkLable2 = Nothing
        Me.TxtitemType.Name = "TxtitemType"
        Me.TxtitemType.ReferenceFieldDesc = Nothing
        Me.TxtitemType.ReferenceFieldName = Nothing
        Me.TxtitemType.ReferenceTableName = Nothing
        Me.TxtitemType.Size = New System.Drawing.Size(179, 18)
        Me.TxtitemType.TabIndex = 285
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(12, 119)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel2.TabIndex = 286
        Me.MyLabel2.Text = "Item Type"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(12, 76)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel6.TabIndex = 267
        Me.MyLabel6.Text = "Location Code"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(238, 75)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(291, 19)
        Me.lblLocationDesc.TabIndex = 268
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 98)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel1.TabIndex = 284
        Me.MyLabel1.Text = "Production Plan"
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
        Me.fndLocation.Location = New System.Drawing.Point(108, 75)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel6
        Me.fndLocation.MyLinkLable2 = Me.lblLocationDesc
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(128, 19)
        Me.fndLocation.TabIndex = 4
        Me.fndLocation.Value = ""
        '
        'chkPendignQC
        '
        Me.chkPendignQC.Location = New System.Drawing.Point(628, 96)
        Me.chkPendignQC.MyLinkLable1 = Nothing
        Me.chkPendignQC.MyLinkLable2 = Nothing
        Me.chkPendignQC.Name = "chkPendignQC"
        Me.chkPendignQC.Size = New System.Drawing.Size(127, 18)
        Me.chkPendignQC.TabIndex = 279
        Me.chkPendignQC.Tag1 = Nothing
        Me.chkPendignQC.Text = "Consider Pending QC"
        Me.chkPendignQC.Visible = False
        '
        'chkAutoPO
        '
        Me.chkAutoPO.Location = New System.Drawing.Point(146, 142)
        Me.chkAutoPO.MyLinkLable1 = Nothing
        Me.chkAutoPO.MyLinkLable2 = Nothing
        Me.chkAutoPO.Name = "chkAutoPO"
        Me.chkAutoPO.Size = New System.Drawing.Size(111, 18)
        Me.chkAutoPO.TabIndex = 12
        Me.chkAutoPO.Text = "Generate Auto PO"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Enabled = False
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
        Me.dtpFromDate.Location = New System.Drawing.Point(108, 54)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.lblFromDate
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(128, 18)
        Me.dtpFromDate.TabIndex = 5
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "03/05/2011"
        Me.dtpFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(12, 55)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(33, 16)
        Me.lblFromDate.TabIndex = 50
        Me.lblFromDate.Text = "From"
        '
        'chkAutoIndent
        '
        Me.chkAutoIndent.Location = New System.Drawing.Point(12, 142)
        Me.chkAutoIndent.MyLinkLable1 = Nothing
        Me.chkAutoIndent.MyLinkLable2 = Nothing
        Me.chkAutoIndent.Name = "chkAutoIndent"
        Me.chkAutoIndent.Size = New System.Drawing.Size(128, 18)
        Me.chkAutoIndent.TabIndex = 10
        Me.chkAutoIndent.Text = "Generate Auto Indent"
        '
        'chkPendingPO
        '
        Me.chkPendingPO.Location = New System.Drawing.Point(628, 76)
        Me.chkPendingPO.MyLinkLable1 = Nothing
        Me.chkPendingPO.MyLinkLable2 = Nothing
        Me.chkPendingPO.Name = "chkPendingPO"
        Me.chkPendingPO.Size = New System.Drawing.Size(127, 18)
        Me.chkPendingPO.TabIndex = 9
        Me.chkPendingPO.Tag1 = Nothing
        Me.chkPendingPO.Text = "Consider Pending PO"
        Me.chkPendingPO.Visible = False
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Enabled = False
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
        Me.dtpToDate.Location = New System.Drawing.Point(278, 54)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.lblFromDate
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(115, 18)
        Me.dtpToDate.TabIndex = 6
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "03/05/2011"
        Me.dtpToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'chkItemLevel
        '
        Me.chkItemLevel.Location = New System.Drawing.Point(627, 117)
        Me.chkItemLevel.MyLinkLable1 = Nothing
        Me.chkItemLevel.MyLinkLable2 = Nothing
        Me.chkItemLevel.Name = "chkItemLevel"
        Me.chkItemLevel.Size = New System.Drawing.Size(212, 18)
        Me.chkItemLevel.TabIndex = 11
        Me.chkItemLevel.Tag1 = Nothing
        Me.chkItemLevel.Text = "Consider Min., Max. and Reorder Level"
        Me.chkItemLevel.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(253, 55)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel7.TabIndex = 52
        Me.MyLabel7.Text = "To"
        '
        'chkStock
        '
        Me.chkStock.Location = New System.Drawing.Point(627, 59)
        Me.chkStock.MyLinkLable1 = Nothing
        Me.chkStock.MyLinkLable2 = Nothing
        Me.chkStock.Name = "chkStock"
        Me.chkStock.Size = New System.Drawing.Size(95, 18)
        Me.chkStock.TabIndex = 8
        Me.chkStock.Tag1 = Nothing
        Me.chkStock.Text = "Consider Stock"
        Me.chkStock.Visible = False
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(300, 120)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(51, 16)
        Me.lblDescription.TabIndex = 62
        Me.lblDescription.Text = "Remarks"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(357, 118)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(172, 20)
        Me.txtDescription.TabIndex = 7
        '
        'dtpMRPDate
        '
        Me.dtpMRPDate.CalculationExpression = Nothing
        Me.dtpMRPDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpMRPDate.FieldCode = Nothing
        Me.dtpMRPDate.FieldDesc = Nothing
        Me.dtpMRPDate.FieldMaxLength = 0
        Me.dtpMRPDate.FieldName = Nothing
        Me.dtpMRPDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMRPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMRPDate.isCalculatedField = False
        Me.dtpMRPDate.IsSourceFromTable = False
        Me.dtpMRPDate.IsSourceFromValueList = False
        Me.dtpMRPDate.IsUnique = False
        Me.dtpMRPDate.Location = New System.Drawing.Point(448, 8)
        Me.dtpMRPDate.MendatroryField = True
        Me.dtpMRPDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMRPDate.MyLinkLable1 = Me.lblMRPDate
        Me.dtpMRPDate.MyLinkLable2 = Nothing
        Me.dtpMRPDate.Name = "dtpMRPDate"
        Me.dtpMRPDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMRPDate.ReferenceFieldDesc = Nothing
        Me.dtpMRPDate.ReferenceFieldName = Nothing
        Me.dtpMRPDate.ReferenceTableName = Nothing
        Me.dtpMRPDate.Size = New System.Drawing.Size(81, 18)
        Me.dtpMRPDate.TabIndex = 1
        Me.dtpMRPDate.TabStop = False
        Me.dtpMRPDate.Text = "03/05/2011"
        Me.dtpMRPDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        Me.dtpMRPDate.Visible = False
        '
        'lblMRPDate
        '
        Me.lblMRPDate.FieldName = Nothing
        Me.lblMRPDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRPDate.Location = New System.Drawing.Point(384, 9)
        Me.lblMRPDate.Name = "lblMRPDate"
        Me.lblMRPDate.Size = New System.Drawing.Size(58, 16)
        Me.lblMRPDate.TabIndex = 48
        Me.lblMRPDate.Text = "MRP Date"
        '
        'pageMRPDetails
        '
        Me.pageMRPDetails.Controls.Add(Me.gvBalQty)
        Me.pageMRPDetails.ItemSize = New System.Drawing.SizeF(76.0!, 28.0!)
        Me.pageMRPDetails.Location = New System.Drawing.Point(10, 37)
        Me.pageMRPDetails.Name = "pageMRPDetails"
        Me.pageMRPDetails.Size = New System.Drawing.Size(750, 531)
        Me.pageMRPDetails.Text = "MRP Details"
        '
        'gvBalQty
        '
        Me.gvBalQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvBalQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvBalQty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBalQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvBalQty.ForeColor = System.Drawing.Color.Black
        Me.gvBalQty.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvBalQty.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvBalQty.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvBalQty.MasterTemplate.AllowAddNewRow = False
        Me.gvBalQty.MasterTemplate.AutoGenerateColumns = False
        Me.gvBalQty.MasterTemplate.EnableGrouping = False
        Me.gvBalQty.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBalQty.Name = "gvBalQty"
        Me.gvBalQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBalQty.ShowHeaderCellButtons = True
        Me.gvBalQty.Size = New System.Drawing.Size(750, 531)
        Me.gvBalQty.TabIndex = 6
        Me.gvBalQty.TabStop = False
        Me.gvBalQty.Text = "RadGridView1"
        '
        'pageMRPItemDetails
        '
        Me.pageMRPItemDetails.Controls.Add(Me.gvBalItemDetails)
        Me.pageMRPItemDetails.ItemSize = New System.Drawing.SizeF(102.0!, 28.0!)
        Me.pageMRPItemDetails.Location = New System.Drawing.Point(10, 37)
        Me.pageMRPItemDetails.Name = "pageMRPItemDetails"
        Me.pageMRPItemDetails.Size = New System.Drawing.Size(750, 531)
        Me.pageMRPItemDetails.Text = "MRP Item Details"
        '
        'gvBalItemDetails
        '
        Me.gvBalItemDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvBalItemDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvBalItemDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvBalItemDetails.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvBalItemDetails.ForeColor = System.Drawing.Color.Black
        Me.gvBalItemDetails.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvBalItemDetails.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvBalItemDetails.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvBalItemDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvBalItemDetails.MasterTemplate.AutoGenerateColumns = False
        Me.gvBalItemDetails.MasterTemplate.EnableGrouping = False
        Me.gvBalItemDetails.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvBalItemDetails.Name = "gvBalItemDetails"
        Me.gvBalItemDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvBalItemDetails.ShowHeaderCellButtons = True
        Me.gvBalItemDetails.Size = New System.Drawing.Size(750, 531)
        Me.gvBalItemDetails.TabIndex = 7
        Me.gvBalItemDetails.TabStop = False
        Me.gvBalItemDetails.Text = "RadGridView1"
        '
        'pagePendingPO
        '
        Me.pagePendingPO.Controls.Add(Me.gvPendingPO)
        Me.pagePendingPO.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.pagePendingPO.Location = New System.Drawing.Point(10, 37)
        Me.pagePendingPO.Name = "pagePendingPO"
        Me.pagePendingPO.Size = New System.Drawing.Size(750, 531)
        Me.pagePendingPO.Text = "Pending PO"
        '
        'gvPendingPO
        '
        Me.gvPendingPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvPendingPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvPendingPO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPendingPO.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvPendingPO.ForeColor = System.Drawing.Color.Black
        Me.gvPendingPO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvPendingPO.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvPendingPO.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvPendingPO.MasterTemplate.AllowAddNewRow = False
        Me.gvPendingPO.MasterTemplate.AutoGenerateColumns = False
        Me.gvPendingPO.MasterTemplate.EnableGrouping = False
        Me.gvPendingPO.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPendingPO.Name = "gvPendingPO"
        Me.gvPendingPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvPendingPO.ShowHeaderCellButtons = True
        Me.gvPendingPO.Size = New System.Drawing.Size(750, 531)
        Me.gvPendingPO.TabIndex = 7
        Me.gvPendingPO.TabStop = False
        Me.gvPendingPO.Text = "RadGridView1"
        '
        'PagePendingSRN
        '
        Me.PagePendingSRN.Controls.Add(Me.gvPendingSRN)
        Me.PagePendingSRN.ItemSize = New System.Drawing.SizeF(82.0!, 28.0!)
        Me.PagePendingSRN.Location = New System.Drawing.Point(10, 37)
        Me.PagePendingSRN.Name = "PagePendingSRN"
        Me.PagePendingSRN.Size = New System.Drawing.Size(750, 531)
        Me.PagePendingSRN.Text = "Pending SRN"
        '
        'gvPendingSRN
        '
        Me.gvPendingSRN.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvPendingSRN.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvPendingSRN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPendingSRN.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvPendingSRN.ForeColor = System.Drawing.Color.Black
        Me.gvPendingSRN.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvPendingSRN.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvPendingSRN.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvPendingSRN.MasterTemplate.AllowAddNewRow = False
        Me.gvPendingSRN.MasterTemplate.AutoGenerateColumns = False
        Me.gvPendingSRN.MasterTemplate.EnableGrouping = False
        Me.gvPendingSRN.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPendingSRN.Name = "gvPendingSRN"
        Me.gvPendingSRN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvPendingSRN.ShowHeaderCellButtons = True
        Me.gvPendingSRN.Size = New System.Drawing.Size(750, 531)
        Me.gvPendingSRN.TabIndex = 7
        Me.gvPendingSRN.TabStop = False
        Me.gvPendingSRN.Text = "RadGridView1"
        '
        'pageAttachment
        '
        Me.pageAttachment.Controls.Add(Me.UcAttachment1)
        Me.pageAttachment.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.pageAttachment.Location = New System.Drawing.Point(10, 37)
        Me.pageAttachment.Name = "pageAttachment"
        Me.pageAttachment.Size = New System.Drawing.Size(714, 462)
        Me.pageAttachment.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(714, 462)
        Me.UcAttachment1.TabIndex = 4
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(275, 5)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(114, 22)
        Me.btnReverse.TabIndex = 8
        Me.btnReverse.Text = "Reverse and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(3, 5)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 22)
        Me.btnGo.TabIndex = 4
        Me.btnGo.Text = ">>>"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(207, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(71, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(698, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(139, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmMRPForProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 617)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMRPForProduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MRP"
        CType(Me.txtMRPDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gvMRPDetal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMRPDetal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlDepartment.ResumeLayout(False)
        Me.pnlDepartment.PerformLayout()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGenAutoSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkConsiderOpenPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtitemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPendignQC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoIndent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPendingPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMRPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMRPDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageMRPDetails.ResumeLayout(False)
        CType(Me.gvBalQty.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageMRPItemDetails.ResumeLayout(False)
        CType(Me.gvBalItemDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBalItemDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pagePendingPO.ResumeLayout(False)
        CType(Me.gvPendingPO.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPendingPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PagePendingSRN.ResumeLayout(False)
        CType(Me.gvPendingSRN.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPendingSRN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageAttachment.ResumeLayout(False)
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageGeneral As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblMRPDate As common.Controls.MyLabel
    Friend WithEvents dtpMRPDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents pageAttachment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents gvMRPDetal As common.UserControls.MyRadGridView
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtMRPDescription As common.Controls.MyTextBox
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkPendignQC As common.Controls.MyCheckBox
    Friend WithEvents chkPendingPO As common.Controls.MyCheckBox
    Friend WithEvents chkItemLevel As common.Controls.MyCheckBox
    Friend WithEvents chkStock As common.Controls.MyCheckBox
    Friend WithEvents chkAutoIndent As common.Controls.MyRadioButton
    Friend WithEvents chkAutoPO As common.Controls.MyRadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents chkConsiderOpenPO As common.Controls.MyCheckBox
    Friend WithEvents chkGenAutoSchedule As common.Controls.MyCheckBox
    Friend WithEvents pageMRPDetails As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvBalQty As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtitemType As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtMulProductionPlan As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents pagePendingPO As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvPendingPO As common.UserControls.MyRadGridView
    Friend WithEvents PagePendingSRN As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvPendingSRN As common.UserControls.MyRadGridView
    Friend WithEvents pageMRPItemDetails As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvBalItemDetails As common.UserControls.MyRadGridView
    Friend WithEvents pnlDepartment As System.Windows.Forms.Panel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents txtDept As common.UserControls.txtFinder
    Friend WithEvents txtReqNo As common.UserControls.txtNavigator
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
End Class
