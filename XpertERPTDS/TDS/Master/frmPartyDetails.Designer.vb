Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPartyDetails
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
        Me.components = New System.ComponentModel.Container()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.ToolTipparty = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbdesignation = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndbranchnew = New common.UserControls.txtFinder()
        Me.lblbranch = New common.Controls.MyLabel()
        Me.fndstatenew = New common.UserControls.txtFinder()
        Me.lblsate = New common.Controls.MyLabel()
        Me.fnddeducNew = New common.UserControls.txtFinder()
        Me.lbldeduction = New common.Controls.MyLabel()
        Me.fndvendorNew = New common.UserControls.txtNavigator()
        Me.lblvendor = New common.Controls.MyLabel()
        Me.chkinactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtbranch = New common.Controls.MyTextBox()
        Me.ddlstatus = New common.Controls.MyComboBox()
        Me.Status = New common.Controls.MyLabel()
        Me.ddlventype = New common.Controls.MyComboBox()
        Me.lblventype = New common.Controls.MyLabel()
        Me.txtPan = New common.Controls.MyTextBox()
        Me.lblPan = New common.Controls.MyLabel()
        Me.txtstate = New common.Controls.MyTextBox()
        Me.txtdeduc = New common.Controls.MyTextBox()
        Me.txtvendes = New common.Controls.MyTextBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbdesignation.SuspendLayout()
        CType(Me.lblbranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblsate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkinactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Status, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlventype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblventype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdeduc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvendes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbdesignation
        '
        Me.gbdesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbdesignation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbdesignation.Controls.Add(Me.fndbranchnew)
        Me.gbdesignation.Controls.Add(Me.fndstatenew)
        Me.gbdesignation.Controls.Add(Me.fnddeducNew)
        Me.gbdesignation.Controls.Add(Me.fndvendorNew)
        Me.gbdesignation.Controls.Add(Me.chkinactive)
        Me.gbdesignation.Controls.Add(Me.txtbranch)
        Me.gbdesignation.Controls.Add(Me.ddlstatus)
        Me.gbdesignation.Controls.Add(Me.ddlventype)
        Me.gbdesignation.Controls.Add(Me.txtPan)
        Me.gbdesignation.Controls.Add(Me.Status)
        Me.gbdesignation.Controls.Add(Me.lblbranch)
        Me.gbdesignation.Controls.Add(Me.lblventype)
        Me.gbdesignation.Controls.Add(Me.lblPan)
        Me.gbdesignation.Controls.Add(Me.lblsate)
        Me.gbdesignation.Controls.Add(Me.txtstate)
        Me.gbdesignation.Controls.Add(Me.txtdeduc)
        Me.gbdesignation.Controls.Add(Me.lblvendor)
        Me.gbdesignation.Controls.Add(Me.txtvendes)
        Me.gbdesignation.Controls.Add(Me.lbldeduction)
        Me.gbdesignation.Controls.Add(Me.btnnew)
        Me.gbdesignation.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbdesignation.HeaderText = ""
        Me.gbdesignation.Location = New System.Drawing.Point(17, 19)
        Me.gbdesignation.Name = "gbdesignation"
        Me.gbdesignation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbdesignation.Size = New System.Drawing.Size(628, 218)
        Me.gbdesignation.TabIndex = 0
        '
        'fndbranchnew
        '
        Me.fndbranchnew.AccessibleName = "fndbranchnew"
        Me.fndbranchnew.CalculationExpression = Nothing
        Me.fndbranchnew.FieldCode = Nothing
        Me.fndbranchnew.FieldDesc = Nothing
        Me.fndbranchnew.FieldMaxLength = 0
        Me.fndbranchnew.FieldName = Nothing
        Me.fndbranchnew.isCalculatedField = False
        Me.fndbranchnew.IsSourceFromTable = False
        Me.fndbranchnew.IsSourceFromValueList = False
        Me.fndbranchnew.IsUnique = False
        Me.fndbranchnew.Location = New System.Drawing.Point(126, 83)
        Me.fndbranchnew.MendatroryField = False
        Me.fndbranchnew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndbranchnew.MyLinkLable1 = Me.lblbranch
        Me.fndbranchnew.MyLinkLable2 = Nothing
        Me.fndbranchnew.MyReadOnly = False
        Me.fndbranchnew.MyShowMasterFormButton = False
        Me.fndbranchnew.Name = "fndbranchnew"
        Me.fndbranchnew.ReferenceFieldDesc = Nothing
        Me.fndbranchnew.ReferenceFieldName = Nothing
        Me.fndbranchnew.ReferenceTableName = Nothing
        Me.fndbranchnew.Size = New System.Drawing.Size(202, 19)
        Me.fndbranchnew.TabIndex = 7
        Me.fndbranchnew.Value = ""
        '
        'lblbranch
        '
        Me.lblbranch.FieldName = Nothing
        Me.lblbranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbranch.Location = New System.Drawing.Point(14, 86)
        Me.lblbranch.Name = "lblbranch"
        Me.lblbranch.Size = New System.Drawing.Size(72, 16)
        Me.lblbranch.TabIndex = 45
        Me.lblbranch.Text = "Branch Code"
        '
        'fndstatenew
        '
        Me.fndstatenew.AccessibleName = "fndstatenew"
        Me.fndstatenew.CalculationExpression = Nothing
        Me.fndstatenew.FieldCode = Nothing
        Me.fndstatenew.FieldDesc = Nothing
        Me.fndstatenew.FieldMaxLength = 0
        Me.fndstatenew.FieldName = Nothing
        Me.fndstatenew.isCalculatedField = False
        Me.fndstatenew.IsSourceFromTable = False
        Me.fndstatenew.IsSourceFromValueList = False
        Me.fndstatenew.IsUnique = False
        Me.fndstatenew.Location = New System.Drawing.Point(126, 60)
        Me.fndstatenew.MendatroryField = False
        Me.fndstatenew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstatenew.MyLinkLable1 = Me.lblsate
        Me.fndstatenew.MyLinkLable2 = Nothing
        Me.fndstatenew.MyReadOnly = False
        Me.fndstatenew.MyShowMasterFormButton = False
        Me.fndstatenew.Name = "fndstatenew"
        Me.fndstatenew.ReferenceFieldDesc = Nothing
        Me.fndstatenew.ReferenceFieldName = Nothing
        Me.fndstatenew.ReferenceTableName = Nothing
        Me.fndstatenew.Size = New System.Drawing.Size(202, 19)
        Me.fndstatenew.TabIndex = 5
        Me.fndstatenew.Value = ""
        '
        'lblsate
        '
        Me.lblsate.FieldName = Nothing
        Me.lblsate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsate.Location = New System.Drawing.Point(13, 60)
        Me.lblsate.Name = "lblsate"
        Me.lblsate.Size = New System.Drawing.Size(63, 16)
        Me.lblsate.TabIndex = 42
        Me.lblsate.Text = "State Code"
        '
        'fnddeducNew
        '
        Me.fnddeducNew.AccessibleName = "fnddeducNew"
        Me.fnddeducNew.CalculationExpression = Nothing
        Me.fnddeducNew.FieldCode = Nothing
        Me.fnddeducNew.FieldDesc = Nothing
        Me.fnddeducNew.FieldMaxLength = 0
        Me.fnddeducNew.FieldName = Nothing
        Me.fnddeducNew.isCalculatedField = False
        Me.fnddeducNew.IsSourceFromTable = False
        Me.fnddeducNew.IsSourceFromValueList = False
        Me.fnddeducNew.IsUnique = False
        Me.fnddeducNew.Location = New System.Drawing.Point(126, 35)
        Me.fnddeducNew.MendatroryField = False
        Me.fnddeducNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnddeducNew.MyLinkLable1 = Me.lbldeduction
        Me.fnddeducNew.MyLinkLable2 = Nothing
        Me.fnddeducNew.MyReadOnly = False
        Me.fnddeducNew.MyShowMasterFormButton = False
        Me.fnddeducNew.Name = "fnddeducNew"
        Me.fnddeducNew.ReferenceFieldDesc = Nothing
        Me.fnddeducNew.ReferenceFieldName = Nothing
        Me.fnddeducNew.ReferenceTableName = Nothing
        Me.fnddeducNew.Size = New System.Drawing.Size(202, 19)
        Me.fnddeducNew.TabIndex = 3
        Me.fnddeducNew.Value = ""
        '
        'lbldeduction
        '
        Me.lbldeduction.FieldName = Nothing
        Me.lbldeduction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldeduction.Location = New System.Drawing.Point(13, 36)
        Me.lbldeduction.Name = "lbldeduction"
        Me.lbldeduction.Size = New System.Drawing.Size(107, 16)
        Me.lbldeduction.TabIndex = 36
        Me.lbldeduction.Text = "Nature of Deduction"
        '
        'fndvendorNew
        '
        Me.fndvendorNew.AccessibleName = "fndvendorNew"
        Me.fndvendorNew.FieldName = Nothing
        Me.fndvendorNew.Location = New System.Drawing.Point(92, 8)
        Me.fndvendorNew.MendatroryField = False
        Me.fndvendorNew.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndvendorNew.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndvendorNew.MyLinkLable1 = Me.lblvendor
        Me.fndvendorNew.MyLinkLable2 = Nothing
        Me.fndvendorNew.MyMaxLength = 30
        Me.fndvendorNew.MyReadOnly = False
        Me.fndvendorNew.Name = "fndvendorNew"
        Me.fndvendorNew.Size = New System.Drawing.Size(236, 21)
        Me.fndvendorNew.TabIndex = 0
        Me.fndvendorNew.TabStop = False
        Me.fndvendorNew.Value = ""
        '
        'lblvendor
        '
        Me.lblvendor.FieldName = Nothing
        Me.lblvendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblvendor.Location = New System.Drawing.Point(13, 9)
        Me.lblvendor.Name = "lblvendor"
        Me.lblvendor.Size = New System.Drawing.Size(77, 16)
        Me.lblvendor.TabIndex = 37
        Me.lblvendor.Text = "Vendor Code"
        '
        'chkinactive
        '
        Me.chkinactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkinactive.Location = New System.Drawing.Point(357, 110)
        Me.chkinactive.Name = "chkinactive"
        Me.chkinactive.Size = New System.Drawing.Size(62, 16)
        Me.chkinactive.TabIndex = 10
        Me.chkinactive.Text = " Inactive"
        '
        'txtbranch
        '
        Me.txtbranch.CalculationExpression = Nothing
        Me.txtbranch.FieldCode = Nothing
        Me.txtbranch.FieldDesc = Nothing
        Me.txtbranch.FieldMaxLength = 0
        Me.txtbranch.FieldName = Nothing
        Me.txtbranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbranch.isCalculatedField = False
        Me.txtbranch.IsSourceFromTable = False
        Me.txtbranch.IsSourceFromValueList = False
        Me.txtbranch.IsUnique = False
        Me.txtbranch.Location = New System.Drawing.Point(355, 84)
        Me.txtbranch.MaxLength = 49
        Me.txtbranch.MendatroryField = False
        Me.txtbranch.MyLinkLable1 = Nothing
        Me.txtbranch.MyLinkLable2 = Nothing
        Me.txtbranch.Name = "txtbranch"
        Me.txtbranch.ReadOnly = True
        Me.txtbranch.ReferenceFieldDesc = Nothing
        Me.txtbranch.ReferenceFieldName = Nothing
        Me.txtbranch.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtbranch.RootElement.StretchVertically = True
        Me.txtbranch.Size = New System.Drawing.Size(256, 20)
        Me.txtbranch.TabIndex = 8
        Me.txtbranch.TabStop = False
        '
        'ddlstatus
        '
        Me.ddlstatus.AutoCompleteDisplayMember = Nothing
        Me.ddlstatus.AutoCompleteValueMember = Nothing
        Me.ddlstatus.CalculationExpression = Nothing
        Me.ddlstatus.DropDownAnimationEnabled = True
        Me.ddlstatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlstatus.FieldCode = Nothing
        Me.ddlstatus.FieldDesc = Nothing
        Me.ddlstatus.FieldMaxLength = 0
        Me.ddlstatus.FieldName = Nothing
        Me.ddlstatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlstatus.isCalculatedField = False
        Me.ddlstatus.IsSourceFromTable = False
        Me.ddlstatus.IsSourceFromValueList = False
        Me.ddlstatus.IsUnique = False
        RadListDataItem1.Text = "Resident"
        RadListDataItem2.Text = "Non Resident"
        Me.ddlstatus.Items.Add(RadListDataItem1)
        Me.ddlstatus.Items.Add(RadListDataItem2)
        Me.ddlstatus.Location = New System.Drawing.Point(126, 158)
        Me.ddlstatus.MendatroryField = False
        Me.ddlstatus.MyLinkLable1 = Me.Status
        Me.ddlstatus.MyLinkLable2 = Nothing
        Me.ddlstatus.Name = "ddlstatus"
        Me.ddlstatus.ReferenceFieldDesc = Nothing
        Me.ddlstatus.ReferenceFieldName = Nothing
        Me.ddlstatus.ReferenceTableName = Nothing
        Me.ddlstatus.Size = New System.Drawing.Size(161, 18)
        Me.ddlstatus.TabIndex = 12
        Me.ddlstatus.Text = "Resident"
        '
        'Status
        '
        Me.Status.FieldName = Nothing
        Me.Status.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Status.Location = New System.Drawing.Point(14, 158)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(38, 16)
        Me.Status.TabIndex = 44
        Me.Status.Text = "Status"
        '
        'ddlventype
        '
        Me.ddlventype.AutoCompleteDisplayMember = Nothing
        Me.ddlventype.AutoCompleteValueMember = Nothing
        Me.ddlventype.CalculationExpression = Nothing
        Me.ddlventype.DropDownAnimationEnabled = True
        Me.ddlventype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlventype.FieldCode = Nothing
        Me.ddlventype.FieldDesc = Nothing
        Me.ddlventype.FieldMaxLength = 0
        Me.ddlventype.FieldName = Nothing
        Me.ddlventype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlventype.isCalculatedField = False
        Me.ddlventype.IsSourceFromTable = False
        Me.ddlventype.IsSourceFromValueList = False
        Me.ddlventype.IsUnique = False
        RadListDataItem3.Text = "Individual"
        RadListDataItem4.Text = "Undevided Family"
        RadListDataItem5.Text = "Partnership Firm"
        RadListDataItem6.Text = "Domestic Company"
        RadListDataItem7.Text = "Co-Operative Society"
        RadListDataItem8.Text = "Local Authority"
        Me.ddlventype.Items.Add(RadListDataItem3)
        Me.ddlventype.Items.Add(RadListDataItem4)
        Me.ddlventype.Items.Add(RadListDataItem5)
        Me.ddlventype.Items.Add(RadListDataItem6)
        Me.ddlventype.Items.Add(RadListDataItem7)
        Me.ddlventype.Items.Add(RadListDataItem8)
        Me.ddlventype.Location = New System.Drawing.Point(126, 133)
        Me.ddlventype.MendatroryField = False
        Me.ddlventype.MyLinkLable1 = Me.lblventype
        Me.ddlventype.MyLinkLable2 = Nothing
        Me.ddlventype.Name = "ddlventype"
        Me.ddlventype.ReferenceFieldDesc = Nothing
        Me.ddlventype.ReferenceFieldName = Nothing
        Me.ddlventype.ReferenceTableName = Nothing
        Me.ddlventype.Size = New System.Drawing.Size(161, 18)
        Me.ddlventype.TabIndex = 11
        Me.ddlventype.Text = "Individual"
        '
        'lblventype
        '
        Me.lblventype.FieldName = Nothing
        Me.lblventype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblventype.Location = New System.Drawing.Point(13, 133)
        Me.lblventype.Name = "lblventype"
        Me.lblventype.Size = New System.Drawing.Size(71, 16)
        Me.lblventype.TabIndex = 43
        Me.lblventype.Text = "Vendor Type"
        '
        'txtPan
        '
        Me.txtPan.CalculationExpression = Nothing
        Me.txtPan.FieldCode = Nothing
        Me.txtPan.FieldDesc = Nothing
        Me.txtPan.FieldMaxLength = 0
        Me.txtPan.FieldName = Nothing
        Me.txtPan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPan.isCalculatedField = False
        Me.txtPan.IsSourceFromTable = False
        Me.txtPan.IsSourceFromValueList = False
        Me.txtPan.IsUnique = False
        Me.txtPan.Location = New System.Drawing.Point(126, 109)
        Me.txtPan.MaxLength = 49
        Me.txtPan.MendatroryField = False
        Me.txtPan.MyLinkLable1 = Me.lblPan
        Me.txtPan.MyLinkLable2 = Nothing
        Me.txtPan.Name = "txtPan"
        Me.txtPan.ReadOnly = True
        Me.txtPan.ReferenceFieldDesc = Nothing
        Me.txtPan.ReferenceFieldName = Nothing
        Me.txtPan.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtPan.RootElement.StretchVertically = True
        Me.txtPan.Size = New System.Drawing.Size(161, 20)
        Me.txtPan.TabIndex = 9
        '
        'lblPan
        '
        Me.lblPan.FieldName = Nothing
        Me.lblPan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPan.Location = New System.Drawing.Point(14, 109)
        Me.lblPan.Name = "lblPan"
        Me.lblPan.Size = New System.Drawing.Size(29, 16)
        Me.lblPan.TabIndex = 43
        Me.lblPan.Text = "PAN"
        '
        'txtstate
        '
        Me.txtstate.CalculationExpression = Nothing
        Me.txtstate.FieldCode = Nothing
        Me.txtstate.FieldDesc = Nothing
        Me.txtstate.FieldMaxLength = 0
        Me.txtstate.FieldName = Nothing
        Me.txtstate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstate.isCalculatedField = False
        Me.txtstate.IsSourceFromTable = False
        Me.txtstate.IsSourceFromValueList = False
        Me.txtstate.IsUnique = False
        Me.txtstate.Location = New System.Drawing.Point(355, 60)
        Me.txtstate.MaxLength = 49
        Me.txtstate.MendatroryField = False
        Me.txtstate.MyLinkLable1 = Nothing
        Me.txtstate.MyLinkLable2 = Nothing
        Me.txtstate.Name = "txtstate"
        Me.txtstate.ReadOnly = True
        Me.txtstate.ReferenceFieldDesc = Nothing
        Me.txtstate.ReferenceFieldName = Nothing
        Me.txtstate.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtstate.RootElement.StretchVertically = True
        Me.txtstate.Size = New System.Drawing.Size(256, 20)
        Me.txtstate.TabIndex = 6
        Me.txtstate.TabStop = False
        '
        'txtdeduc
        '
        Me.txtdeduc.CalculationExpression = Nothing
        Me.txtdeduc.FieldCode = Nothing
        Me.txtdeduc.FieldDesc = Nothing
        Me.txtdeduc.FieldMaxLength = 0
        Me.txtdeduc.FieldName = Nothing
        Me.txtdeduc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdeduc.isCalculatedField = False
        Me.txtdeduc.IsSourceFromTable = False
        Me.txtdeduc.IsSourceFromValueList = False
        Me.txtdeduc.IsUnique = False
        Me.txtdeduc.Location = New System.Drawing.Point(355, 36)
        Me.txtdeduc.MaxLength = 49
        Me.txtdeduc.MendatroryField = False
        Me.txtdeduc.MyLinkLable1 = Nothing
        Me.txtdeduc.MyLinkLable2 = Nothing
        Me.txtdeduc.Name = "txtdeduc"
        Me.txtdeduc.ReadOnly = True
        Me.txtdeduc.ReferenceFieldDesc = Nothing
        Me.txtdeduc.ReferenceFieldName = Nothing
        Me.txtdeduc.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdeduc.RootElement.StretchVertically = True
        Me.txtdeduc.Size = New System.Drawing.Size(256, 20)
        Me.txtdeduc.TabIndex = 4
        Me.txtdeduc.TabStop = False
        '
        'txtvendes
        '
        Me.txtvendes.CalculationExpression = Nothing
        Me.txtvendes.FieldCode = Nothing
        Me.txtvendes.FieldDesc = Nothing
        Me.txtvendes.FieldMaxLength = 0
        Me.txtvendes.FieldName = Nothing
        Me.txtvendes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvendes.isCalculatedField = False
        Me.txtvendes.IsSourceFromTable = False
        Me.txtvendes.IsSourceFromValueList = False
        Me.txtvendes.IsUnique = False
        Me.txtvendes.Location = New System.Drawing.Point(355, 9)
        Me.txtvendes.MaxLength = 49
        Me.txtvendes.MendatroryField = False
        Me.txtvendes.MyLinkLable1 = Nothing
        Me.txtvendes.MyLinkLable2 = Nothing
        Me.txtvendes.Name = "txtvendes"
        Me.txtvendes.ReadOnly = True
        Me.txtvendes.ReferenceFieldDesc = Nothing
        Me.txtvendes.ReferenceFieldName = Nothing
        Me.txtvendes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtvendes.RootElement.StretchVertically = True
        Me.txtvendes.Size = New System.Drawing.Size(256, 20)
        Me.txtvendes.TabIndex = 2
        Me.txtvendes.TabStop = False
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(329, 8)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(99, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 14
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(562, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 15
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(31, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 13
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(664, 20)
        Me.RadMenu1.TabIndex = 40
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport, Me.menuExport, Me.menuClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'MenuImport
        '
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "Import"
        '
        'menuExport
        '
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        '
        'menuClose
        '
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbdesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(664, 292)
        Me.SplitContainer1.SplitterDistance = 245
        Me.SplitContainer1.TabIndex = 41
        '
        'frmPartyDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 312)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmPartyDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Party Details"
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbdesignation.ResumeLayout(False)
        Me.gbdesignation.PerformLayout()
        CType(Me.lblbranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblsate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkinactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlstatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Status, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlventype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblventype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdeduc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvendes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTipparty As System.Windows.Forms.ToolTip
    Friend WithEvents gbdesignation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtvendes As common.Controls.MyTextBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents txtstate As common.Controls.MyTextBox
    Friend WithEvents txtdeduc As common.Controls.MyTextBox
    Friend WithEvents chkinactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtbranch As common.Controls.MyTextBox
    Friend WithEvents ddlstatus As common.Controls.MyComboBox
    Friend WithEvents ddlventype As common.Controls.MyComboBox
    Friend WithEvents txtPan As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblvendor As common.Controls.MyLabel
    Friend WithEvents lbldeduction As common.Controls.MyLabel
    Friend WithEvents lblsate As common.Controls.MyLabel
    Friend WithEvents lblventype As common.Controls.MyLabel
    Friend WithEvents lblPan As common.Controls.MyLabel
    Friend WithEvents lblbranch As common.Controls.MyLabel
    Friend WithEvents Status As common.Controls.MyLabel
    Friend WithEvents fndbranchnew As common.UserControls.txtFinder
    Friend WithEvents fndstatenew As common.UserControls.txtFinder
    Friend WithEvents fnddeducNew As common.UserControls.txtFinder
    Friend WithEvents fndvendorNew As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

