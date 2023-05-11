<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAllotmentOfLeaves
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAllotmentOfLeaves))
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.chkread = New common.Controls.MyCheckBox()
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.cboAllotmentType = New common.Controls.MyComboBox()
        Me.lblAllotmentType = New common.Controls.MyLabel()
        Me.cboDocType = New common.Controls.MyComboBox()
        Me.lblDocType = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndPayPeriod = New common.UserControls.txtFinder()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.fndDivision = New common.UserControls.txtFinder()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.lblDivisionName = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.gvSalary = New common.UserControls.MyRadGridView()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnAllotAll = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadButton()
        Me.btnImport = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkread, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAllotmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAllotmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSalary.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAllotAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnImport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAllotAll)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(956, 508)
        Me.SplitContainer1.SplitterDistance = 466
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkread)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboAllotmentType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblAllotmentType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboDocType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndPayPeriod)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndDivision)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDivisionName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDivision)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvSalary)
        Me.SplitContainer2.Size = New System.Drawing.Size(956, 446)
        Me.SplitContainer2.SplitterDistance = 183
        Me.SplitContainer2.TabIndex = 263
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(368, 126)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(66, 21)
        Me.RadButton1.TabIndex = 423
        Me.RadButton1.Text = ">>"
        '
        'chkread
        '
        Me.chkread.Location = New System.Drawing.Point(10, 161)
        Me.chkread.MyLinkLable1 = Nothing
        Me.chkread.MyLinkLable2 = Nothing
        Me.chkread.Name = "chkread"
        Me.chkread.Size = New System.Drawing.Size(113, 18)
        Me.chkread.TabIndex = 422
        Me.chkread.Tag1 = Nothing
        Me.chkread.Text = "Select/Unselect All"
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(382, 12)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 421
        Me.lblDate.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(454, 10)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(151, 20)
        Me.txtDate.TabIndex = 420
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "16/11/2011"
        Me.txtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
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
        Me.txtDescription.Location = New System.Drawing.Point(110, 120)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(252, 33)
        Me.txtDescription.TabIndex = 414
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(9, 119)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 267
        Me.lblRemarks.Text = "Remarks"
        '
        'cboAllotmentType
        '
        Me.cboAllotmentType.AutoCompleteDisplayMember = Nothing
        Me.cboAllotmentType.AutoCompleteValueMember = Nothing
        Me.cboAllotmentType.CalculationExpression = Nothing
        Me.cboAllotmentType.DropDownAnimationEnabled = True
        Me.cboAllotmentType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAllotmentType.FieldCode = Nothing
        Me.cboAllotmentType.FieldDesc = Nothing
        Me.cboAllotmentType.FieldMaxLength = 0
        Me.cboAllotmentType.FieldName = Nothing
        Me.cboAllotmentType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAllotmentType.isCalculatedField = False
        Me.cboAllotmentType.IsSourceFromTable = False
        Me.cboAllotmentType.IsSourceFromValueList = False
        Me.cboAllotmentType.IsUnique = False
        RadListDataItem3.Text = "Individual"
        RadListDataItem4.Text = "All"
        Me.cboAllotmentType.Items.Add(RadListDataItem3)
        Me.cboAllotmentType.Items.Add(RadListDataItem4)
        Me.cboAllotmentType.Location = New System.Drawing.Point(453, 33)
        Me.cboAllotmentType.MendatroryField = True
        Me.cboAllotmentType.MyLinkLable1 = Me.lblAllotmentType
        Me.cboAllotmentType.MyLinkLable2 = Nothing
        Me.cboAllotmentType.Name = "cboAllotmentType"
        Me.cboAllotmentType.ReferenceFieldDesc = Nothing
        Me.cboAllotmentType.ReferenceFieldName = Nothing
        Me.cboAllotmentType.ReferenceTableName = Nothing
        Me.cboAllotmentType.Size = New System.Drawing.Size(152, 18)
        Me.cboAllotmentType.TabIndex = 265
        '
        'lblAllotmentType
        '
        Me.lblAllotmentType.FieldName = Nothing
        Me.lblAllotmentType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllotmentType.Location = New System.Drawing.Point(366, 34)
        Me.lblAllotmentType.Name = "lblAllotmentType"
        Me.lblAllotmentType.Size = New System.Drawing.Size(82, 16)
        Me.lblAllotmentType.TabIndex = 266
        Me.lblAllotmentType.Text = "Allotment Type"
        '
        'cboDocType
        '
        Me.cboDocType.AutoCompleteDisplayMember = Nothing
        Me.cboDocType.AutoCompleteValueMember = Nothing
        Me.cboDocType.CalculationExpression = Nothing
        Me.cboDocType.DropDownAnimationEnabled = True
        Me.cboDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDocType.FieldCode = Nothing
        Me.cboDocType.FieldDesc = Nothing
        Me.cboDocType.FieldMaxLength = 0
        Me.cboDocType.FieldName = Nothing
        Me.cboDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocType.isCalculatedField = False
        Me.cboDocType.IsSourceFromTable = False
        Me.cboDocType.IsSourceFromValueList = False
        Me.cboDocType.IsUnique = False
        RadListDataItem5.Text = "Opening Balance"
        RadListDataItem6.Text = "Leave Allocation"
        Me.cboDocType.Items.Add(RadListDataItem5)
        Me.cboDocType.Items.Add(RadListDataItem6)
        Me.cboDocType.Location = New System.Drawing.Point(110, 32)
        Me.cboDocType.MendatroryField = True
        Me.cboDocType.MyLinkLable1 = Me.lblDocType
        Me.cboDocType.MyLinkLable2 = Nothing
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.ReferenceFieldDesc = Nothing
        Me.cboDocType.ReferenceFieldName = Nothing
        Me.cboDocType.ReferenceTableName = Nothing
        Me.cboDocType.Size = New System.Drawing.Size(252, 18)
        Me.cboDocType.TabIndex = 263
        '
        'lblDocType
        '
        Me.lblDocType.FieldName = Nothing
        Me.lblDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocType.Location = New System.Drawing.Point(9, 36)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(86, 16)
        Me.lblDocType.TabIndex = 264
        Me.lblDocType.Text = "Document Type"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 14)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel1.TabIndex = 160
        Me.MyLabel1.Text = "Code"
        '
        'fndPayPeriod
        '
        Me.fndPayPeriod.CalculationExpression = Nothing
        Me.fndPayPeriod.FieldCode = Nothing
        Me.fndPayPeriod.FieldDesc = Nothing
        Me.fndPayPeriod.FieldMaxLength = 0
        Me.fndPayPeriod.FieldName = Nothing
        Me.fndPayPeriod.isCalculatedField = False
        Me.fndPayPeriod.IsSourceFromTable = False
        Me.fndPayPeriod.IsSourceFromValueList = False
        Me.fndPayPeriod.IsUnique = False
        Me.fndPayPeriod.Location = New System.Drawing.Point(110, 97)
        Me.fndPayPeriod.MendatroryField = True
        Me.fndPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayPeriod.MyLinkLable1 = Nothing
        Me.fndPayPeriod.MyLinkLable2 = Nothing
        Me.fndPayPeriod.MyReadOnly = False
        Me.fndPayPeriod.MyShowMasterFormButton = False
        Me.fndPayPeriod.Name = "fndPayPeriod"
        Me.fndPayPeriod.ReferenceFieldDesc = Nothing
        Me.fndPayPeriod.ReferenceFieldName = Nothing
        Me.fndPayPeriod.ReferenceTableName = Nothing
        Me.fndPayPeriod.Size = New System.Drawing.Size(252, 19)
        Me.fndPayPeriod.TabIndex = 262
        Me.fndPayPeriod.Value = ""
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(110, 8)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Nothing
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(252, 21)
        Me.fndCode.TabIndex = 161
        Me.fndCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 98)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel4.TabIndex = 261
        Me.MyLabel4.Text = "Pay Period Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(362, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 162
        Me.btnNew.Text = " "
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(367, 98)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(238, 19)
        Me.lblPayPeriodName.TabIndex = 260
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(9, 56)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel9.TabIndex = 254
        Me.MyLabel9.Text = "Location"
        '
        'fndDivision
        '
        Me.fndDivision.CalculationExpression = Nothing
        Me.fndDivision.FieldCode = Nothing
        Me.fndDivision.FieldDesc = Nothing
        Me.fndDivision.FieldMaxLength = 0
        Me.fndDivision.FieldName = Nothing
        Me.fndDivision.isCalculatedField = False
        Me.fndDivision.IsSourceFromTable = False
        Me.fndDivision.IsSourceFromValueList = False
        Me.fndDivision.IsUnique = False
        Me.fndDivision.Location = New System.Drawing.Point(110, 75)
        Me.fndDivision.MendatroryField = True
        Me.fndDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDivision.MyLinkLable1 = Nothing
        Me.fndDivision.MyLinkLable2 = Nothing
        Me.fndDivision.MyReadOnly = False
        Me.fndDivision.MyShowMasterFormButton = False
        Me.fndDivision.Name = "fndDivision"
        Me.fndDivision.ReferenceFieldDesc = Nothing
        Me.fndDivision.ReferenceFieldName = Nothing
        Me.fndDivision.ReferenceTableName = Nothing
        Me.fndDivision.Size = New System.Drawing.Size(252, 19)
        Me.fndDivision.TabIndex = 259
        Me.fndDivision.Value = ""
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(366, 54)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(239, 19)
        Me.lblLocationDesc.TabIndex = 255
        '
        'lblDivisionName
        '
        Me.lblDivisionName.AutoSize = False
        Me.lblDivisionName.BorderVisible = True
        Me.lblDivisionName.FieldName = Nothing
        Me.lblDivisionName.Location = New System.Drawing.Point(366, 76)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.Size = New System.Drawing.Size(239, 19)
        Me.lblDivisionName.TabIndex = 258
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
        Me.fndLocation.Location = New System.Drawing.Point(110, 54)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(252, 19)
        Me.fndLocation.TabIndex = 256
        Me.fndLocation.Value = ""
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDivision.Location = New System.Drawing.Point(9, 77)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(49, 16)
        Me.lblDivision.TabIndex = 257
        Me.lblDivision.Text = "Division "
        '
        'gvSalary
        '
        Me.gvSalary.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvSalary.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSalary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSalary.EnableCustomFiltering = True
        Me.gvSalary.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvSalary.ForeColor = System.Drawing.Color.Black
        Me.gvSalary.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSalary.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvSalary.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvSalary.MasterTemplate.AllowAddNewRow = False
        Me.gvSalary.MasterTemplate.AutoGenerateColumns = False
        Me.gvSalary.MasterTemplate.EnableCustomFiltering = True
        Me.gvSalary.MasterTemplate.EnableGrouping = False
        Me.gvSalary.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSalary.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvSalary.Name = "gvSalary"
        Me.gvSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSalary.ShowHeaderCellButtons = True
        Me.gvSalary.Size = New System.Drawing.Size(956, 259)
        Me.gvSalary.TabIndex = 7
        Me.gvSalary.TabStop = False
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(956, 20)
        Me.RadMenu2.TabIndex = 264
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'rmImport
        '
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'btnAllotAll
        '
        Me.btnAllotAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAllotAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllotAll.Location = New System.Drawing.Point(157, 8)
        Me.btnAllotAll.Name = "btnAllotAll"
        Me.btnAllotAll.Size = New System.Drawing.Size(82, 21)
        Me.btnAllotAll.TabIndex = 7
        Me.btnAllotAll.Text = "Allot All"
        Me.btnAllotAll.Visible = False
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(878, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(80, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 5
        Me.btndelete.Text = "Delete"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.Location = New System.Drawing.Point(245, 8)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(82, 21)
        Me.btnExport.TabIndex = 8
        Me.btnExport.Text = "Export"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImport.Location = New System.Drawing.Point(333, 8)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(82, 21)
        Me.btnImport.TabIndex = 9
        Me.btnImport.Text = "Import"
        '
        'FrmAllotmentOfLeaves
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 508)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAllotmentOfLeaves"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmAllotmentOfLeaves"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkread, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAllotmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAllotmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSalary.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAllotAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents fndDivision As common.UserControls.txtFinder
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents fndPayPeriod As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnAllotAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboAllotmentType As common.Controls.MyComboBox
    Friend WithEvents lblAllotmentType As common.Controls.MyLabel
    Friend WithEvents cboDocType As common.Controls.MyComboBox
    Friend WithEvents lblDocType As common.Controls.MyLabel
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkread As common.Controls.MyCheckBox
    Friend WithEvents gvSalary As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnImport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
End Class

