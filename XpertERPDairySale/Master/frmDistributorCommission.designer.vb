<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDistributorCommission
    Inherits XpertERPEngine.FrmMainTranScreen
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.cmbItemType = New common.Controls.MyComboBox()
        Me.lblItemType = New common.Controls.MyLabel()
        Me.txtInActiveDate = New common.Controls.MyDateTimePicker()
        Me.lblApplicableDate = New common.Controls.MyLabel()
        Me.chkInActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSecurity = New Telerik.WinControls.UI.RadCheckBox()
        Me.rbtnTranspotation = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnCommission = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDistributorTagging = New common.UserControls.txtFinder()
        Me.lblDistributorTagging = New common.Controls.MyLabel()
        Me.lblStatus = New common.usLock()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.lblItems = New common.Controls.MyLabel()
        Me.txtItems = New common.UserControls.txtMultiSelectFinder()
        Me.lblCommission = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtApplicableDate = New common.Controls.MyDateTimePicker()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.GV1 = New common.UserControls.MyRadGridView()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnImport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.cmbItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInActiveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplicableDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTranspotation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCommission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDistributorTagging, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCommission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnImport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(832, 450)
        Me.SplitContainer1.SplitterDistance = 407
        Me.SplitContainer1.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbItemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtInActiveDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkInActive)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkSecurity)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnTranspotation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCommission)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDistributorTagging)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDistributorTagging)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItems)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtItems)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCommission)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtApplicableDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblApplicableDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GV1)
        Me.SplitContainer2.Size = New System.Drawing.Size(832, 407)
        Me.SplitContainer2.SplitterDistance = 120
        Me.SplitContainer2.TabIndex = 0
        '
        'cmbItemType
        '
        Me.cmbItemType.AutoCompleteDisplayMember = Nothing
        Me.cmbItemType.AutoCompleteValueMember = Nothing
        Me.cmbItemType.CalculationExpression = Nothing
        Me.cmbItemType.DropDownAnimationEnabled = True
        Me.cmbItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbItemType.FieldCode = Nothing
        Me.cmbItemType.FieldDesc = Nothing
        Me.cmbItemType.FieldMaxLength = 0
        Me.cmbItemType.FieldName = Nothing
        Me.cmbItemType.isCalculatedField = False
        Me.cmbItemType.IsSourceFromTable = False
        Me.cmbItemType.IsSourceFromValueList = False
        Me.cmbItemType.IsUnique = False
        RadListDataItem1.Text = "Milk"
        RadListDataItem2.Text = "Product"
        RadListDataItem3.Selected = True
        RadListDataItem3.Text = "Both"
        Me.cmbItemType.Items.Add(RadListDataItem1)
        Me.cmbItemType.Items.Add(RadListDataItem2)
        Me.cmbItemType.Items.Add(RadListDataItem3)
        Me.cmbItemType.Location = New System.Drawing.Point(687, 34)
        Me.cmbItemType.MendatroryField = False
        Me.cmbItemType.MyLinkLable1 = Nothing
        Me.cmbItemType.MyLinkLable2 = Nothing
        Me.cmbItemType.Name = "cmbItemType"
        Me.cmbItemType.ReferenceFieldDesc = Nothing
        Me.cmbItemType.ReferenceFieldName = Nothing
        Me.cmbItemType.ReferenceTableName = Nothing
        Me.cmbItemType.Size = New System.Drawing.Size(126, 20)
        Me.cmbItemType.TabIndex = 1529
        Me.cmbItemType.Text = "Both"
        '
        'lblItemType
        '
        Me.lblItemType.FieldName = Nothing
        Me.lblItemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemType.Location = New System.Drawing.Point(615, 36)
        Me.lblItemType.Name = "lblItemType"
        Me.lblItemType.Size = New System.Drawing.Size(57, 16)
        Me.lblItemType.TabIndex = 1530
        Me.lblItemType.Text = "Item Type"
        '
        'txtInActiveDate
        '
        Me.txtInActiveDate.CalculationExpression = Nothing
        Me.txtInActiveDate.CustomFormat = "dd/MM/yyyy"
        Me.txtInActiveDate.FieldCode = Nothing
        Me.txtInActiveDate.FieldDesc = Nothing
        Me.txtInActiveDate.FieldMaxLength = 0
        Me.txtInActiveDate.FieldName = Nothing
        Me.txtInActiveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInActiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInActiveDate.isCalculatedField = False
        Me.txtInActiveDate.IsSourceFromTable = False
        Me.txtInActiveDate.IsSourceFromValueList = False
        Me.txtInActiveDate.IsUnique = False
        Me.txtInActiveDate.Location = New System.Drawing.Point(699, 58)
        Me.txtInActiveDate.MendatroryField = False
        Me.txtInActiveDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInActiveDate.MyLinkLable1 = Me.lblApplicableDate
        Me.txtInActiveDate.MyLinkLable2 = Nothing
        Me.txtInActiveDate.Name = "txtInActiveDate"
        Me.txtInActiveDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInActiveDate.ReferenceFieldDesc = Nothing
        Me.txtInActiveDate.ReferenceFieldName = Nothing
        Me.txtInActiveDate.ReferenceTableName = Nothing
        Me.txtInActiveDate.Size = New System.Drawing.Size(114, 18)
        Me.txtInActiveDate.TabIndex = 1528
        Me.txtInActiveDate.TabStop = False
        Me.txtInActiveDate.Text = "04/07/2023"
        Me.txtInActiveDate.Value = New Date(2023, 7, 4, 0, 0, 0, 0)
        '
        'lblApplicableDate
        '
        Me.lblApplicableDate.FieldName = Nothing
        Me.lblApplicableDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblApplicableDate.Location = New System.Drawing.Point(398, 35)
        Me.lblApplicableDate.Name = "lblApplicableDate"
        Me.lblApplicableDate.Size = New System.Drawing.Size(91, 16)
        Me.lblApplicableDate.TabIndex = 51
        Me.lblApplicableDate.Text = "Applicable Date"
        '
        'chkInActive
        '
        Me.chkInActive.Location = New System.Drawing.Point(678, 60)
        Me.chkInActive.Name = "chkInActive"
        Me.chkInActive.Size = New System.Drawing.Size(15, 15)
        Me.chkInActive.TabIndex = 1527
        '
        'chkSecurity
        '
        Me.chkSecurity.Location = New System.Drawing.Point(609, 59)
        Me.chkSecurity.Name = "chkSecurity"
        Me.chkSecurity.Size = New System.Drawing.Size(63, 18)
        Me.chkSecurity.TabIndex = 1526
        Me.chkSecurity.Text = "Security "
        '
        'rbtnTranspotation
        '
        Me.rbtnTranspotation.Location = New System.Drawing.Point(493, 58)
        Me.rbtnTranspotation.Name = "rbtnTranspotation"
        Me.rbtnTranspotation.Size = New System.Drawing.Size(109, 18)
        Me.rbtnTranspotation.TabIndex = 1525
        Me.rbtnTranspotation.Text = "TRANSPOTATION"
        '
        'rbtnCommission
        '
        Me.rbtnCommission.Location = New System.Drawing.Point(398, 59)
        Me.rbtnCommission.Name = "rbtnCommission"
        Me.rbtnCommission.Size = New System.Drawing.Size(91, 18)
        Me.rbtnCommission.TabIndex = 1524
        Me.rbtnCommission.Text = "COMMISSION"
        '
        'txtDistributorTagging
        '
        Me.txtDistributorTagging.CalculationExpression = Nothing
        Me.txtDistributorTagging.FieldCode = Nothing
        Me.txtDistributorTagging.FieldDesc = Nothing
        Me.txtDistributorTagging.FieldMaxLength = 0
        Me.txtDistributorTagging.FieldName = Nothing
        Me.txtDistributorTagging.isCalculatedField = False
        Me.txtDistributorTagging.IsSourceFromTable = False
        Me.txtDistributorTagging.IsSourceFromValueList = False
        Me.txtDistributorTagging.IsUnique = False
        Me.txtDistributorTagging.Location = New System.Drawing.Point(114, 82)
        Me.txtDistributorTagging.MendatroryField = False
        Me.txtDistributorTagging.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributorTagging.MyLinkLable1 = Nothing
        Me.txtDistributorTagging.MyLinkLable2 = Nothing
        Me.txtDistributorTagging.MyReadOnly = False
        Me.txtDistributorTagging.MyShowMasterFormButton = False
        Me.txtDistributorTagging.Name = "txtDistributorTagging"
        Me.txtDistributorTagging.ReferenceFieldDesc = Nothing
        Me.txtDistributorTagging.ReferenceFieldName = Nothing
        Me.txtDistributorTagging.ReferenceTableName = Nothing
        Me.txtDistributorTagging.Size = New System.Drawing.Size(248, 19)
        Me.txtDistributorTagging.TabIndex = 1523
        Me.txtDistributorTagging.Value = ""
        '
        'lblDistributorTagging
        '
        Me.lblDistributorTagging.FieldName = Nothing
        Me.lblDistributorTagging.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDistributorTagging.Location = New System.Drawing.Point(9, 83)
        Me.lblDistributorTagging.Name = "lblDistributorTagging"
        Me.lblDistributorTagging.Size = New System.Drawing.Size(102, 16)
        Me.lblDistributorTagging.TabIndex = 1522
        Me.lblDistributorTagging.Text = "Distributor Tagging"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblStatus.Location = New System.Drawing.Point(615, 8)
        Me.lblStatus.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(198, 20)
        Me.lblStatus.Status = common.ERPTransactionStatus.Pending
        Me.lblStatus.TabIndex = 45
        '
        'txtUOM
        '
        Me.txtUOM.CalculationExpression = Nothing
        Me.txtUOM.FieldCode = Nothing
        Me.txtUOM.FieldDesc = Nothing
        Me.txtUOM.FieldMaxLength = 0
        Me.txtUOM.FieldName = Nothing
        Me.txtUOM.isCalculatedField = False
        Me.txtUOM.IsSourceFromTable = False
        Me.txtUOM.IsSourceFromValueList = False
        Me.txtUOM.IsUnique = False
        Me.txtUOM.Location = New System.Drawing.Point(114, 60)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Nothing
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(248, 19)
        Me.txtUOM.TabIndex = 1521
        Me.txtUOM.Value = ""
        '
        'lblItems
        '
        Me.lblItems.FieldName = Nothing
        Me.lblItems.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItems.Location = New System.Drawing.Point(14, 36)
        Me.lblItems.Name = "lblItems"
        Me.lblItems.Size = New System.Drawing.Size(34, 18)
        Me.lblItems.TabIndex = 1520
        Me.lblItems.Text = "Items"
        '
        'txtItems
        '
        Me.txtItems.arrDispalyMember = Nothing
        Me.txtItems.arrValueMember = Nothing
        Me.txtItems.Location = New System.Drawing.Point(114, 35)
        Me.txtItems.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItems.MyLinkLable1 = Me.lblItems
        Me.txtItems.MyLinkLable2 = Nothing
        Me.txtItems.MyNullText = "All"
        Me.txtItems.Name = "txtItems"
        Me.txtItems.Size = New System.Drawing.Size(248, 19)
        Me.txtItems.TabIndex = 1519
        '
        'lblCommission
        '
        Me.lblCommission.FieldName = Nothing
        Me.lblCommission.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommission.Location = New System.Drawing.Point(9, 61)
        Me.lblCommission.Name = "lblCommission"
        Me.lblCommission.Size = New System.Drawing.Size(106, 16)
        Me.lblCommission.TabIndex = 1518
        Me.lblCommission.Text = "Commission (UOM)"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(342, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 166
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(114, 11)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(228, 19)
        Me.txtDocNo.TabIndex = 165
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'txtApplicableDate
        '
        Me.txtApplicableDate.CalculationExpression = Nothing
        Me.txtApplicableDate.CustomFormat = "dd/MM/yyyy"
        Me.txtApplicableDate.FieldCode = Nothing
        Me.txtApplicableDate.FieldDesc = Nothing
        Me.txtApplicableDate.FieldMaxLength = 0
        Me.txtApplicableDate.FieldName = Nothing
        Me.txtApplicableDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicableDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtApplicableDate.isCalculatedField = False
        Me.txtApplicableDate.IsSourceFromTable = False
        Me.txtApplicableDate.IsSourceFromValueList = False
        Me.txtApplicableDate.IsUnique = False
        Me.txtApplicableDate.Location = New System.Drawing.Point(491, 33)
        Me.txtApplicableDate.MendatroryField = False
        Me.txtApplicableDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtApplicableDate.MyLinkLable1 = Me.lblApplicableDate
        Me.txtApplicableDate.MyLinkLable2 = Nothing
        Me.txtApplicableDate.Name = "txtApplicableDate"
        Me.txtApplicableDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtApplicableDate.ReferenceFieldDesc = Nothing
        Me.txtApplicableDate.ReferenceFieldName = Nothing
        Me.txtApplicableDate.ReferenceTableName = Nothing
        Me.txtApplicableDate.Size = New System.Drawing.Size(114, 18)
        Me.txtApplicableDate.TabIndex = 52
        Me.txtApplicableDate.TabStop = False
        Me.txtApplicableDate.Text = "04/07/2023"
        Me.txtApplicableDate.Value = New Date(2023, 7, 4, 0, 0, 0, 0)
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Location = New System.Drawing.Point(12, 12)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(88, 18)
        Me.lblDocCode.TabIndex = 164
        Me.lblDocCode.Text = "Document Code"
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(374, 83)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(115, 18)
        Me.btnGo.TabIndex = 162
        Me.btnGo.Text = "Go >>"
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
        Me.txtDate.Location = New System.Drawing.Point(489, 11)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(114, 18)
        Me.txtDate.TabIndex = 50
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "04/07/2023"
        Me.txtDate.Value = New Date(2023, 7, 4, 0, 0, 0, 0)
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDate.Location = New System.Drawing.Point(398, 13)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(31, 16)
        Me.lblDate.TabIndex = 49
        Me.lblDate.Text = "Date"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.GV1.MasterTemplate.ShowHeaderCellButtons = True
        Me.GV1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.GV1.MyStopExport = False
        Me.GV1.Name = "GV1"
        Me.GV1.ShowHeaderCellButtons = True
        Me.GV1.Size = New System.Drawing.Size(832, 283)
        Me.GV1.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(367, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 24)
        Me.btnDelete.TabIndex = 159
        Me.btnDelete.Text = "Delete"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Location = New System.Drawing.Point(456, 7)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(149, 24)
        Me.btnReverse.TabIndex = 158
        Me.btnReverse.Text = "Reverse and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(278, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(86, 24)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiImport, Me.rmiExport})
        Me.btnImport.Location = New System.Drawing.Point(167, 7)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(108, 24)
        Me.btnImport.TabIndex = 157
        Me.btnImport.Text = "Import/Export"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        Me.rmiImport.UseCompatibleTextRendering = False
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        Me.rmiExport.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(738, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(90, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(74, 24)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(13, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(74, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmDistributorCommission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDistributorCommission"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDistributorCommission"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.cmbItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInActiveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplicableDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTranspotation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCommission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDistributorTagging, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCommission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents txtApplicableDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblApplicableDate As common.Controls.MyLabel
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents btnGo As RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents lblCommission As common.Controls.MyLabel
    Friend WithEvents lblItems As common.Controls.MyLabel
    Friend WithEvents txtItems As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents lblStatus As common.usLock
    Friend WithEvents btnImport As RadSplitButton
    Friend WithEvents rmiImport As RadMenuItem
    Friend WithEvents rmiExport As RadMenuItem
    Friend WithEvents txtDistributorTagging As common.UserControls.txtFinder
    Friend WithEvents lblDistributorTagging As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents rbtnTranspotation As RadRadioButton
    Friend WithEvents rbtnCommission As RadRadioButton
    Friend WithEvents chkSecurity As RadCheckBox
    Friend WithEvents btnReverse As RadButton
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents chkInActive As RadCheckBox
    Friend WithEvents txtInActiveDate As common.Controls.MyDateTimePicker
    Friend WithEvents cmbItemType As common.Controls.MyComboBox
    Friend WithEvents lblItemType As common.Controls.MyLabel
End Class
