Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveSetting))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvCTC = New common.UserControls.MyRadGridView()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkLeaveEncash = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtminleavebal = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cboRoundoffType = New common.Controls.MyComboBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtLapseAfterDays = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkLapseNegativeLeaves = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.cboMonth = New common.Controls.MyComboBox()
        Me.chkLapseUnAvailed = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtExceeding = New common.MyNumBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtAvailDays = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtAvailMonths = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.rdbAvailAfter = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAvailPPCompDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAvailConfirmDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAvailJoinDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkCarryForward = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtCFUpper = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtCFLower = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkAutoAllot = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtPresentDays = New common.MyNumBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtAllotedDays = New common.MyNumBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.cboAllotType = New common.Controls.MyComboBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.cboPeriodicity = New common.Controls.MyComboBox()
        Me.txtdays = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtmonths = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.rdbAllotafter = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbppcompDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbconfirmDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbJoiningDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.gvCTC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCTC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.chkLeaveEncash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtminleavebal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboRoundoffType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.txtLapseAfterDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLapseNegativeLeaves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLapseUnAvailed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExceeding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtAvailDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAvailMonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAvailAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAvailPPCompDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAvailConfirmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAvailJoinDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.chkCarryForward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCFUpper, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCFLower, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkAutoAllot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAllotedDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAllotType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPeriodicity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAllotafter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbppcompDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbconfirmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbJoiningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(807, 568)
        Me.RadGroupBox3.TabIndex = 58
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel17)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblempcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(787, 538)
        Me.SplitContainer1.SplitterDistance = 502
        Me.SplitContainer1.TabIndex = 0
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(360, 31)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(374, 19)
        Me.lblLocationName.TabIndex = 258
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
        Me.fndLocation.Location = New System.Drawing.Point(137, 31)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel17
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(219, 19)
        Me.fndLocation.TabIndex = 256
        Me.fndLocation.Value = ""
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel17.Location = New System.Drawing.Point(16, 32)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel17.TabIndex = 257
        Me.MyLabel17.Text = "Location"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox7.Controls.Add(Me.gvCTC)
        Me.RadGroupBox7.HeaderText = "Salary Slab for Leave Allotment (Yearly)"
        Me.RadGroupBox7.Location = New System.Drawing.Point(16, 399)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(718, 97)
        Me.RadGroupBox7.TabIndex = 120
        Me.RadGroupBox7.Text = "Salary Slab for Leave Allotment (Yearly)"
        '
        'gvCTC
        '
        Me.gvCTC.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvCTC.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCTC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCTC.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvCTC.ForeColor = System.Drawing.Color.Black
        Me.gvCTC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCTC.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvCTC.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvCTC.MasterTemplate.AllowAddNewRow = False
        Me.gvCTC.MasterTemplate.AllowDeleteRow = False
        Me.gvCTC.MasterTemplate.AutoGenerateColumns = False
        Me.gvCTC.MasterTemplate.EnableGrouping = False
        Me.gvCTC.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCTC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCTC.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvCTC.MyStopExport = False
        Me.gvCTC.Name = "gvCTC"
        Me.gvCTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCTC.ShowHeaderCellButtons = True
        Me.gvCTC.Size = New System.Drawing.Size(698, 67)
        Me.gvCTC.TabIndex = 6
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.chkLeaveEncash)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox6.Controls.Add(Me.txtminleavebal)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox6.Controls.Add(Me.cboRoundoffType)
        Me.RadGroupBox6.HeaderText = "Leave Encashment"
        Me.RadGroupBox6.Location = New System.Drawing.Point(377, 319)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(357, 78)
        Me.RadGroupBox6.TabIndex = 7
        Me.RadGroupBox6.Text = "Leave Encashment"
        '
        'chkLeaveEncash
        '
        Me.chkLeaveEncash.Location = New System.Drawing.Point(6, 23)
        Me.chkLeaveEncash.Name = "chkLeaveEncash"
        Me.chkLeaveEncash.Size = New System.Drawing.Size(112, 18)
        Me.chkLeaveEncash.TabIndex = 0
        Me.chkLeaveEncash.Text = "Leave Encashment"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(159, 23)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel7.TabIndex = 1
        Me.MyLabel7.Text = "Minimum Balance"
        '
        'txtminleavebal
        '
        Me.txtminleavebal.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtminleavebal.CalculationExpression = Nothing
        Me.txtminleavebal.DecimalPlaces = 2
        Me.txtminleavebal.FieldCode = Nothing
        Me.txtminleavebal.FieldDesc = Nothing
        Me.txtminleavebal.FieldMaxLength = 0
        Me.txtminleavebal.FieldName = Nothing
        Me.txtminleavebal.isCalculatedField = False
        Me.txtminleavebal.IsSourceFromTable = False
        Me.txtminleavebal.IsSourceFromValueList = False
        Me.txtminleavebal.IsUnique = False
        Me.txtminleavebal.Location = New System.Drawing.Point(261, 21)
        Me.txtminleavebal.MaxLength = 6
        Me.txtminleavebal.MendatroryField = True
        Me.txtminleavebal.MyLinkLable1 = Me.MyLabel7
        Me.txtminleavebal.MyLinkLable2 = Nothing
        Me.txtminleavebal.Name = "txtminleavebal"
        Me.txtminleavebal.ReferenceFieldDesc = Nothing
        Me.txtminleavebal.ReferenceFieldName = Nothing
        Me.txtminleavebal.ReferenceTableName = Nothing
        Me.txtminleavebal.Size = New System.Drawing.Size(79, 20)
        Me.txtminleavebal.TabIndex = 2
        Me.txtminleavebal.Text = "0"
        Me.txtminleavebal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtminleavebal.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 49)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(128, 16)
        Me.MyLabel3.TabIndex = 3
        Me.MyLabel3.Text = "Balance Round off Type"
        '
        'cboRoundoffType
        '
        Me.cboRoundoffType.AutoCompleteDisplayMember = Nothing
        Me.cboRoundoffType.AutoCompleteValueMember = Nothing
        Me.cboRoundoffType.CalculationExpression = Nothing
        Me.cboRoundoffType.DropDownAnimationEnabled = True
        Me.cboRoundoffType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboRoundoffType.FieldCode = Nothing
        Me.cboRoundoffType.FieldDesc = Nothing
        Me.cboRoundoffType.FieldMaxLength = 0
        Me.cboRoundoffType.FieldName = Nothing
        Me.cboRoundoffType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRoundoffType.isCalculatedField = False
        Me.cboRoundoffType.IsSourceFromTable = False
        Me.cboRoundoffType.IsSourceFromValueList = False
        Me.cboRoundoffType.IsUnique = False
        Me.cboRoundoffType.Location = New System.Drawing.Point(159, 47)
        Me.cboRoundoffType.MendatroryField = False
        Me.cboRoundoffType.MyLinkLable1 = Me.MyLabel3
        Me.cboRoundoffType.MyLinkLable2 = Nothing
        Me.cboRoundoffType.Name = "cboRoundoffType"
        Me.cboRoundoffType.ReferenceFieldDesc = Nothing
        Me.cboRoundoffType.ReferenceFieldName = Nothing
        Me.cboRoundoffType.ReferenceTableName = Nothing
        Me.cboRoundoffType.Size = New System.Drawing.Size(181, 18)
        Me.cboRoundoffType.TabIndex = 4
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(341, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.txtLapseAfterDays)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox5.Controls.Add(Me.chkLapseNegativeLeaves)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox5.Controls.Add(Me.cboMonth)
        Me.RadGroupBox5.Controls.Add(Me.chkLapseUnAvailed)
        Me.RadGroupBox5.Controls.Add(Me.txtExceeding)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox5.HeaderText = "Lapse Setting"
        Me.RadGroupBox5.Location = New System.Drawing.Point(377, 173)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(357, 144)
        Me.RadGroupBox5.TabIndex = 6
        Me.RadGroupBox5.Text = "Lapse Setting"
        '
        'txtLapseAfterDays
        '
        Me.txtLapseAfterDays.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLapseAfterDays.CalculationExpression = Nothing
        Me.txtLapseAfterDays.DecimalPlaces = 2
        Me.txtLapseAfterDays.FieldCode = Nothing
        Me.txtLapseAfterDays.FieldDesc = Nothing
        Me.txtLapseAfterDays.FieldMaxLength = 0
        Me.txtLapseAfterDays.FieldName = Nothing
        Me.txtLapseAfterDays.isCalculatedField = False
        Me.txtLapseAfterDays.IsSourceFromTable = False
        Me.txtLapseAfterDays.IsSourceFromValueList = False
        Me.txtLapseAfterDays.IsUnique = False
        Me.txtLapseAfterDays.Location = New System.Drawing.Point(173, 111)
        Me.txtLapseAfterDays.MaxLength = 6
        Me.txtLapseAfterDays.MendatroryField = True
        Me.txtLapseAfterDays.MyLinkLable1 = Me.MyLabel4
        Me.txtLapseAfterDays.MyLinkLable2 = Nothing
        Me.txtLapseAfterDays.Name = "txtLapseAfterDays"
        Me.txtLapseAfterDays.ReferenceFieldDesc = Nothing
        Me.txtLapseAfterDays.ReferenceFieldName = Nothing
        Me.txtLapseAfterDays.ReferenceTableName = Nothing
        Me.txtLapseAfterDays.Size = New System.Drawing.Size(122, 20)
        Me.txtLapseAfterDays.TabIndex = 5
        Me.txtLapseAfterDays.Text = "0"
        Me.txtLapseAfterDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLapseAfterDays.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(24, 115)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel4.TabIndex = 4
        Me.MyLabel4.Text = "Lapse after days"
        '
        'chkLapseNegativeLeaves
        '
        Me.chkLapseNegativeLeaves.Location = New System.Drawing.Point(23, 68)
        Me.chkLapseNegativeLeaves.Name = "chkLapseNegativeLeaves"
        Me.chkLapseNegativeLeaves.Size = New System.Drawing.Size(139, 18)
        Me.chkLapseNegativeLeaves.TabIndex = 2
        Me.chkLapseNegativeLeaves.Text = "Lapse Negative Leaves  "
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(23, 48)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel12.TabIndex = 1
        Me.MyLabel12.Text = "Lapse in Month"
        '
        'cboMonth
        '
        Me.cboMonth.AutoCompleteDisplayMember = Nothing
        Me.cboMonth.AutoCompleteValueMember = Nothing
        Me.cboMonth.CalculationExpression = Nothing
        Me.cboMonth.DropDownAnimationEnabled = True
        Me.cboMonth.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMonth.FieldCode = Nothing
        Me.cboMonth.FieldDesc = Nothing
        Me.cboMonth.FieldMaxLength = 0
        Me.cboMonth.FieldName = Nothing
        Me.cboMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMonth.isCalculatedField = False
        Me.cboMonth.IsSourceFromTable = False
        Me.cboMonth.IsSourceFromValueList = False
        Me.cboMonth.IsUnique = False
        RadListDataItem1.Text = "Lower Half day"
        RadListDataItem2.Text = "Higher Half Day"
        RadListDataItem3.Text = "Lower Full day"
        RadListDataItem4.Text = "Higher Full Day"
        RadListDataItem5.Text = "Nearest Half day"
        RadListDataItem6.Text = "Nearest Full day"
        Me.cboMonth.Items.Add(RadListDataItem1)
        Me.cboMonth.Items.Add(RadListDataItem2)
        Me.cboMonth.Items.Add(RadListDataItem3)
        Me.cboMonth.Items.Add(RadListDataItem4)
        Me.cboMonth.Items.Add(RadListDataItem5)
        Me.cboMonth.Items.Add(RadListDataItem6)
        Me.cboMonth.Location = New System.Drawing.Point(173, 48)
        Me.cboMonth.MendatroryField = False
        Me.cboMonth.MyLinkLable1 = Me.MyLabel12
        Me.cboMonth.MyLinkLable2 = Nothing
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.ReferenceFieldDesc = Nothing
        Me.cboMonth.ReferenceFieldName = Nothing
        Me.cboMonth.ReferenceTableName = Nothing
        Me.cboMonth.Size = New System.Drawing.Size(122, 18)
        Me.cboMonth.TabIndex = 1
        '
        'chkLapseUnAvailed
        '
        Me.chkLapseUnAvailed.Location = New System.Drawing.Point(23, 25)
        Me.chkLapseUnAvailed.Name = "chkLapseUnAvailed"
        Me.chkLapseUnAvailed.Size = New System.Drawing.Size(119, 18)
        Me.chkLapseUnAvailed.TabIndex = 0
        Me.chkLapseUnAvailed.Text = "Lapse Unavailed On"
        '
        'txtExceeding
        '
        Me.txtExceeding.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtExceeding.CalculationExpression = Nothing
        Me.txtExceeding.DecimalPlaces = 2
        Me.txtExceeding.FieldCode = Nothing
        Me.txtExceeding.FieldDesc = Nothing
        Me.txtExceeding.FieldMaxLength = 0
        Me.txtExceeding.FieldName = Nothing
        Me.txtExceeding.isCalculatedField = False
        Me.txtExceeding.IsSourceFromTable = False
        Me.txtExceeding.IsSourceFromValueList = False
        Me.txtExceeding.IsUnique = False
        Me.txtExceeding.Location = New System.Drawing.Point(172, 85)
        Me.txtExceeding.MaxLength = 6
        Me.txtExceeding.MendatroryField = True
        Me.txtExceeding.MyLinkLable1 = Me.MyLabel10
        Me.txtExceeding.MyLinkLable2 = Nothing
        Me.txtExceeding.Name = "txtExceeding"
        Me.txtExceeding.ReferenceFieldDesc = Nothing
        Me.txtExceeding.ReferenceFieldName = Nothing
        Me.txtExceeding.ReferenceTableName = Nothing
        Me.txtExceeding.Size = New System.Drawing.Size(122, 20)
        Me.txtExceeding.TabIndex = 3
        Me.txtExceeding.Text = "0"
        Me.txtExceeding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtExceeding.Value = 0R
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(23, 89)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel10.TabIndex = 3
        Me.MyLabel10.Text = "Lapse Exceeding"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtAvailDays)
        Me.RadGroupBox2.Controls.Add(Me.txtAvailMonths)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.rdbAvailAfter)
        Me.RadGroupBox2.Controls.Add(Me.rdbAvailPPCompDate)
        Me.RadGroupBox2.Controls.Add(Me.rdbAvailConfirmDate)
        Me.RadGroupBox2.Controls.Add(Me.rdbAvailJoinDate)
        Me.RadGroupBox2.HeaderText = "Leave Avail Setting"
        Me.RadGroupBox2.Location = New System.Drawing.Point(377, 50)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(357, 120)
        Me.RadGroupBox2.TabIndex = 4
        Me.RadGroupBox2.Text = "Leave Avail Setting"
        '
        'txtAvailDays
        '
        Me.txtAvailDays.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAvailDays.CalculationExpression = Nothing
        Me.txtAvailDays.DecimalPlaces = 0
        Me.txtAvailDays.FieldCode = Nothing
        Me.txtAvailDays.FieldDesc = Nothing
        Me.txtAvailDays.FieldMaxLength = 0
        Me.txtAvailDays.FieldName = Nothing
        Me.txtAvailDays.isCalculatedField = False
        Me.txtAvailDays.IsSourceFromTable = False
        Me.txtAvailDays.IsSourceFromValueList = False
        Me.txtAvailDays.IsUnique = False
        Me.txtAvailDays.Location = New System.Drawing.Point(229, 92)
        Me.txtAvailDays.MendatroryField = True
        Me.txtAvailDays.MyLinkLable1 = Me.MyLabel5
        Me.txtAvailDays.MyLinkLable2 = Nothing
        Me.txtAvailDays.Name = "txtAvailDays"
        Me.txtAvailDays.ReferenceFieldDesc = Nothing
        Me.txtAvailDays.ReferenceFieldName = Nothing
        Me.txtAvailDays.ReferenceTableName = Nothing
        Me.txtAvailDays.Size = New System.Drawing.Size(62, 20)
        Me.txtAvailDays.TabIndex = 6
        Me.txtAvailDays.Text = "0"
        Me.txtAvailDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAvailDays.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(290, 94)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel5.TabIndex = 7
        Me.MyLabel5.Text = "Days"
        '
        'txtAvailMonths
        '
        Me.txtAvailMonths.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAvailMonths.CalculationExpression = Nothing
        Me.txtAvailMonths.DecimalPlaces = 0
        Me.txtAvailMonths.FieldCode = Nothing
        Me.txtAvailMonths.FieldDesc = Nothing
        Me.txtAvailMonths.FieldMaxLength = 0
        Me.txtAvailMonths.FieldName = Nothing
        Me.txtAvailMonths.isCalculatedField = False
        Me.txtAvailMonths.IsSourceFromTable = False
        Me.txtAvailMonths.IsSourceFromValueList = False
        Me.txtAvailMonths.IsUnique = False
        Me.txtAvailMonths.Location = New System.Drawing.Point(114, 92)
        Me.txtAvailMonths.MendatroryField = True
        Me.txtAvailMonths.MyLinkLable1 = Me.MyLabel6
        Me.txtAvailMonths.MyLinkLable2 = Nothing
        Me.txtAvailMonths.Name = "txtAvailMonths"
        Me.txtAvailMonths.ReferenceFieldDesc = Nothing
        Me.txtAvailMonths.ReferenceFieldName = Nothing
        Me.txtAvailMonths.ReferenceTableName = Nothing
        Me.txtAvailMonths.Size = New System.Drawing.Size(62, 20)
        Me.txtAvailMonths.TabIndex = 4
        Me.txtAvailMonths.Text = "0"
        Me.txtAvailMonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAvailMonths.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(175, 94)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel6.TabIndex = 5
        Me.MyLabel6.Text = "Months"
        '
        'rdbAvailAfter
        '
        Me.rdbAvailAfter.Location = New System.Drawing.Point(23, 93)
        Me.rdbAvailAfter.Name = "rdbAvailAfter"
        Me.rdbAvailAfter.Size = New System.Drawing.Size(72, 18)
        Me.rdbAvailAfter.TabIndex = 3
        Me.rdbAvailAfter.Text = "Avail After"
        '
        'rdbAvailPPCompDate
        '
        Me.rdbAvailPPCompDate.Location = New System.Drawing.Point(23, 69)
        Me.rdbAvailPPCompDate.Name = "rdbAvailPPCompDate"
        Me.rdbAvailPPCompDate.Size = New System.Drawing.Size(193, 18)
        Me.rdbAvailPPCompDate.TabIndex = 2
        Me.rdbAvailPPCompDate.Text = "Probation Period Completion Date"
        '
        'rdbAvailConfirmDate
        '
        Me.rdbAvailConfirmDate.Location = New System.Drawing.Point(23, 45)
        Me.rdbAvailConfirmDate.Name = "rdbAvailConfirmDate"
        Me.rdbAvailConfirmDate.Size = New System.Drawing.Size(112, 18)
        Me.rdbAvailConfirmDate.TabIndex = 1
        Me.rdbAvailConfirmDate.Text = "Confirmation Date"
        '
        'rdbAvailJoinDate
        '
        Me.rdbAvailJoinDate.Location = New System.Drawing.Point(23, 21)
        Me.rdbAvailJoinDate.Name = "rdbAvailJoinDate"
        Me.rdbAvailJoinDate.Size = New System.Drawing.Size(82, 18)
        Me.rdbAvailJoinDate.TabIndex = 0
        Me.rdbAvailJoinDate.Text = "Joining Date"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.chkCarryForward)
        Me.RadGroupBox4.Controls.Add(Me.txtCFUpper)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox4.Controls.Add(Me.txtCFLower)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox4.HeaderText = "Carry Over Setting"
        Me.RadGroupBox4.Location = New System.Drawing.Point(16, 287)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(357, 110)
        Me.RadGroupBox4.TabIndex = 5
        Me.RadGroupBox4.Text = "Carry Over Setting"
        '
        'chkCarryForward
        '
        Me.chkCarryForward.Location = New System.Drawing.Point(23, 25)
        Me.chkCarryForward.Name = "chkCarryForward"
        Me.chkCarryForward.Size = New System.Drawing.Size(90, 18)
        Me.chkCarryForward.TabIndex = 0
        Me.chkCarryForward.Text = "Carry Forward"
        '
        'txtCFUpper
        '
        Me.txtCFUpper.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCFUpper.CalculationExpression = Nothing
        Me.txtCFUpper.DecimalPlaces = 2
        Me.txtCFUpper.FieldCode = Nothing
        Me.txtCFUpper.FieldDesc = Nothing
        Me.txtCFUpper.FieldMaxLength = 0
        Me.txtCFUpper.FieldName = Nothing
        Me.txtCFUpper.isCalculatedField = False
        Me.txtCFUpper.IsSourceFromTable = False
        Me.txtCFUpper.IsSourceFromValueList = False
        Me.txtCFUpper.IsUnique = False
        Me.txtCFUpper.Location = New System.Drawing.Point(117, 71)
        Me.txtCFUpper.MaxLength = 6
        Me.txtCFUpper.MendatroryField = True
        Me.txtCFUpper.MyLinkLable1 = Me.MyLabel9
        Me.txtCFUpper.MyLinkLable2 = Nothing
        Me.txtCFUpper.Name = "txtCFUpper"
        Me.txtCFUpper.ReferenceFieldDesc = Nothing
        Me.txtCFUpper.ReferenceFieldName = Nothing
        Me.txtCFUpper.ReferenceTableName = Nothing
        Me.txtCFUpper.Size = New System.Drawing.Size(122, 20)
        Me.txtCFUpper.TabIndex = 2
        Me.txtCFUpper.Text = "0"
        Me.txtCFUpper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCFUpper.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(23, 73)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel9.TabIndex = 139
        Me.MyLabel9.Text = "Upper Limit"
        '
        'txtCFLower
        '
        Me.txtCFLower.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCFLower.CalculationExpression = Nothing
        Me.txtCFLower.DecimalPlaces = 2
        Me.txtCFLower.FieldCode = Nothing
        Me.txtCFLower.FieldDesc = Nothing
        Me.txtCFLower.FieldMaxLength = 0
        Me.txtCFLower.FieldName = Nothing
        Me.txtCFLower.isCalculatedField = False
        Me.txtCFLower.IsSourceFromTable = False
        Me.txtCFLower.IsSourceFromValueList = False
        Me.txtCFLower.IsUnique = False
        Me.txtCFLower.Location = New System.Drawing.Point(117, 47)
        Me.txtCFLower.MaxLength = 6
        Me.txtCFLower.MendatroryField = True
        Me.txtCFLower.MyLinkLable1 = Me.MyLabel8
        Me.txtCFLower.MyLinkLable2 = Nothing
        Me.txtCFLower.Name = "txtCFLower"
        Me.txtCFLower.ReferenceFieldDesc = Nothing
        Me.txtCFLower.ReferenceFieldName = Nothing
        Me.txtCFLower.ReferenceTableName = Nothing
        Me.txtCFLower.Size = New System.Drawing.Size(122, 20)
        Me.txtCFLower.TabIndex = 1
        Me.txtCFLower.Text = "0"
        Me.txtCFLower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCFLower.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(23, 49)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel8.TabIndex = 137
        Me.MyLabel8.Text = "Lower Limit"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(397, 12)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel18.TabIndex = 55
        Me.MyLabel18.Text = "Leave Name"
        '
        'txtName
        '
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(473, 9)
        Me.txtName.MaxLength = 49
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.MyLabel8
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(261, 18)
        Me.txtName.TabIndex = 2
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblempcode.Location = New System.Drawing.Point(16, 11)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(70, 16)
        Me.lblempcode.TabIndex = 119
        Me.lblempcode.Text = "Leave Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkAutoAllot)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtPresentDays)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.txtAllotedDays)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox1.Controls.Add(Me.cboAllotType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.cboPeriodicity)
        Me.RadGroupBox1.Controls.Add(Me.txtdays)
        Me.RadGroupBox1.Controls.Add(Me.txtmonths)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.rdbAllotafter)
        Me.RadGroupBox1.Controls.Add(Me.rdbppcompDate)
        Me.RadGroupBox1.Controls.Add(Me.rdbconfirmDate)
        Me.RadGroupBox1.Controls.Add(Me.rdbJoiningDate)
        Me.RadGroupBox1.HeaderText = "Leave Allotment Setting"
        Me.RadGroupBox1.Location = New System.Drawing.Point(16, 50)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(357, 233)
        Me.RadGroupBox1.TabIndex = 3
        Me.RadGroupBox1.Text = "Leave Allotment Setting"
        '
        'chkAutoAllot
        '
        Me.chkAutoAllot.Location = New System.Drawing.Point(24, 202)
        Me.chkAutoAllot.Name = "chkAutoAllot"
        Me.chkAutoAllot.Size = New System.Drawing.Size(198, 18)
        Me.chkAutoAllot.TabIndex = 143
        Me.chkAutoAllot.Text = "Auto Allot during Salary Generation"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(271, 172)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel16.TabIndex = 142
        Me.MyLabel16.Text = "Present Days"
        '
        'txtPresentDays
        '
        Me.txtPresentDays.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtPresentDays.CalculationExpression = Nothing
        Me.txtPresentDays.DecimalPlaces = 2
        Me.txtPresentDays.FieldCode = Nothing
        Me.txtPresentDays.FieldDesc = Nothing
        Me.txtPresentDays.FieldMaxLength = 0
        Me.txtPresentDays.FieldName = Nothing
        Me.txtPresentDays.isCalculatedField = False
        Me.txtPresentDays.IsSourceFromTable = False
        Me.txtPresentDays.IsSourceFromValueList = False
        Me.txtPresentDays.IsUnique = False
        Me.txtPresentDays.Location = New System.Drawing.Point(220, 170)
        Me.txtPresentDays.MaxLength = 6
        Me.txtPresentDays.MendatroryField = True
        Me.txtPresentDays.MyLinkLable1 = Me.MyLabel15
        Me.txtPresentDays.MyLinkLable2 = Nothing
        Me.txtPresentDays.Name = "txtPresentDays"
        Me.txtPresentDays.ReferenceFieldDesc = Nothing
        Me.txtPresentDays.ReferenceFieldName = Nothing
        Me.txtPresentDays.ReferenceTableName = Nothing
        Me.txtPresentDays.Size = New System.Drawing.Size(45, 20)
        Me.txtPresentDays.TabIndex = 140
        Me.txtPresentDays.Text = "0"
        Me.txtPresentDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPresentDays.Value = 0R
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(208, 172)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(9, 16)
        Me.MyLabel15.TabIndex = 141
        Me.MyLabel15.Text = "/"
        '
        'txtAllotedDays
        '
        Me.txtAllotedDays.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAllotedDays.CalculationExpression = Nothing
        Me.txtAllotedDays.DecimalPlaces = 2
        Me.txtAllotedDays.FieldCode = Nothing
        Me.txtAllotedDays.FieldDesc = Nothing
        Me.txtAllotedDays.FieldMaxLength = 0
        Me.txtAllotedDays.FieldName = Nothing
        Me.txtAllotedDays.isCalculatedField = False
        Me.txtAllotedDays.IsSourceFromTable = False
        Me.txtAllotedDays.IsSourceFromValueList = False
        Me.txtAllotedDays.IsUnique = False
        Me.txtAllotedDays.Location = New System.Drawing.Point(159, 170)
        Me.txtAllotedDays.MaxLength = 6
        Me.txtAllotedDays.MendatroryField = True
        Me.txtAllotedDays.MyLinkLable1 = Me.MyLabel14
        Me.txtAllotedDays.MyLinkLable2 = Nothing
        Me.txtAllotedDays.Name = "txtAllotedDays"
        Me.txtAllotedDays.ReferenceFieldDesc = Nothing
        Me.txtAllotedDays.ReferenceFieldName = Nothing
        Me.txtAllotedDays.ReferenceTableName = Nothing
        Me.txtAllotedDays.Size = New System.Drawing.Size(45, 20)
        Me.txtAllotedDays.TabIndex = 138
        Me.txtAllotedDays.Text = "0"
        Me.txtAllotedDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAllotedDays.Value = 0R
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(24, 172)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel14.TabIndex = 139
        Me.MyLabel14.Text = "Alloted Days"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(23, 149)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel13.TabIndex = 10
        Me.MyLabel13.Text = "Allotment Type"
        '
        'cboAllotType
        '
        Me.cboAllotType.AutoCompleteDisplayMember = Nothing
        Me.cboAllotType.AutoCompleteValueMember = Nothing
        Me.cboAllotType.CalculationExpression = Nothing
        Me.cboAllotType.DropDownAnimationEnabled = True
        Me.cboAllotType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAllotType.FieldCode = Nothing
        Me.cboAllotType.FieldDesc = Nothing
        Me.cboAllotType.FieldMaxLength = 0
        Me.cboAllotType.FieldName = Nothing
        Me.cboAllotType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAllotType.isCalculatedField = False
        Me.cboAllotType.IsSourceFromTable = False
        Me.cboAllotType.IsSourceFromValueList = False
        Me.cboAllotType.IsUnique = False
        RadListDataItem7.Text = "Fixed"
        RadListDataItem8.Text = "Attendance Based"
        RadListDataItem9.Text = "Salary Slab"
        Me.cboAllotType.Items.Add(RadListDataItem7)
        Me.cboAllotType.Items.Add(RadListDataItem8)
        Me.cboAllotType.Items.Add(RadListDataItem9)
        Me.cboAllotType.Location = New System.Drawing.Point(159, 149)
        Me.cboAllotType.MendatroryField = False
        Me.cboAllotType.MyLinkLable1 = Me.MyLabel13
        Me.cboAllotType.MyLinkLable2 = Nothing
        Me.cboAllotType.Name = "cboAllotType"
        Me.cboAllotType.ReferenceFieldDesc = Nothing
        Me.cboAllotType.ReferenceFieldName = Nothing
        Me.cboAllotType.ReferenceTableName = Nothing
        Me.cboAllotType.Size = New System.Drawing.Size(181, 18)
        Me.cboAllotType.TabIndex = 11
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(23, 128)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel11.TabIndex = 8
        Me.MyLabel11.Text = "Allotment Periodicity"
        '
        'cboPeriodicity
        '
        Me.cboPeriodicity.AutoCompleteDisplayMember = Nothing
        Me.cboPeriodicity.AutoCompleteValueMember = Nothing
        Me.cboPeriodicity.CalculationExpression = Nothing
        Me.cboPeriodicity.DropDownAnimationEnabled = True
        Me.cboPeriodicity.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPeriodicity.FieldCode = Nothing
        Me.cboPeriodicity.FieldDesc = Nothing
        Me.cboPeriodicity.FieldMaxLength = 0
        Me.cboPeriodicity.FieldName = Nothing
        Me.cboPeriodicity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPeriodicity.isCalculatedField = False
        Me.cboPeriodicity.IsSourceFromTable = False
        Me.cboPeriodicity.IsSourceFromValueList = False
        Me.cboPeriodicity.IsUnique = False
        RadListDataItem10.Text = "Monthly"
        RadListDataItem11.Text = "Yearly"
        Me.cboPeriodicity.Items.Add(RadListDataItem10)
        Me.cboPeriodicity.Items.Add(RadListDataItem11)
        Me.cboPeriodicity.Location = New System.Drawing.Point(159, 128)
        Me.cboPeriodicity.MendatroryField = False
        Me.cboPeriodicity.MyLinkLable1 = Me.MyLabel11
        Me.cboPeriodicity.MyLinkLable2 = Nothing
        Me.cboPeriodicity.Name = "cboPeriodicity"
        Me.cboPeriodicity.ReferenceFieldDesc = Nothing
        Me.cboPeriodicity.ReferenceFieldName = Nothing
        Me.cboPeriodicity.ReferenceTableName = Nothing
        Me.cboPeriodicity.Size = New System.Drawing.Size(181, 18)
        Me.cboPeriodicity.TabIndex = 9
        '
        'txtdays
        '
        Me.txtdays.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtdays.CalculationExpression = Nothing
        Me.txtdays.DecimalPlaces = 0
        Me.txtdays.FieldCode = Nothing
        Me.txtdays.FieldDesc = Nothing
        Me.txtdays.FieldMaxLength = 0
        Me.txtdays.FieldName = Nothing
        Me.txtdays.isCalculatedField = False
        Me.txtdays.IsSourceFromTable = False
        Me.txtdays.IsSourceFromValueList = False
        Me.txtdays.IsUnique = False
        Me.txtdays.Location = New System.Drawing.Point(231, 92)
        Me.txtdays.MendatroryField = True
        Me.txtdays.MyLinkLable1 = Me.MyLabel2
        Me.txtdays.MyLinkLable2 = Nothing
        Me.txtdays.Name = "txtdays"
        Me.txtdays.ReferenceFieldDesc = Nothing
        Me.txtdays.ReferenceFieldName = Nothing
        Me.txtdays.ReferenceTableName = Nothing
        Me.txtdays.Size = New System.Drawing.Size(62, 20)
        Me.txtdays.TabIndex = 6
        Me.txtdays.Text = "0"
        Me.txtdays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtdays.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(293, 94)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel2.TabIndex = 7
        Me.MyLabel2.Text = "Days"
        '
        'txtmonths
        '
        Me.txtmonths.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtmonths.CalculationExpression = Nothing
        Me.txtmonths.DecimalPlaces = 0
        Me.txtmonths.FieldCode = Nothing
        Me.txtmonths.FieldDesc = Nothing
        Me.txtmonths.FieldMaxLength = 0
        Me.txtmonths.FieldName = Nothing
        Me.txtmonths.isCalculatedField = False
        Me.txtmonths.IsSourceFromTable = False
        Me.txtmonths.IsSourceFromValueList = False
        Me.txtmonths.IsUnique = False
        Me.txtmonths.Location = New System.Drawing.Point(116, 92)
        Me.txtmonths.MendatroryField = True
        Me.txtmonths.MyLinkLable1 = Me.MyLabel1
        Me.txtmonths.MyLinkLable2 = Nothing
        Me.txtmonths.Name = "txtmonths"
        Me.txtmonths.ReferenceFieldDesc = Nothing
        Me.txtmonths.ReferenceFieldName = Nothing
        Me.txtmonths.ReferenceTableName = Nothing
        Me.txtmonths.Size = New System.Drawing.Size(62, 20)
        Me.txtmonths.TabIndex = 4
        Me.txtmonths.Text = "0"
        Me.txtmonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtmonths.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(178, 94)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "Months"
        '
        'rdbAllotafter
        '
        Me.rdbAllotafter.Location = New System.Drawing.Point(23, 93)
        Me.rdbAllotafter.Name = "rdbAllotafter"
        Me.rdbAllotafter.Size = New System.Drawing.Size(71, 18)
        Me.rdbAllotafter.TabIndex = 3
        Me.rdbAllotafter.Text = "Allot After"
        '
        'rdbppcompDate
        '
        Me.rdbppcompDate.Location = New System.Drawing.Point(23, 69)
        Me.rdbppcompDate.Name = "rdbppcompDate"
        Me.rdbppcompDate.Size = New System.Drawing.Size(193, 18)
        Me.rdbppcompDate.TabIndex = 2
        Me.rdbppcompDate.Text = "Probation Period Completion Date"
        '
        'rdbconfirmDate
        '
        Me.rdbconfirmDate.Location = New System.Drawing.Point(23, 45)
        Me.rdbconfirmDate.Name = "rdbconfirmDate"
        Me.rdbconfirmDate.Size = New System.Drawing.Size(112, 18)
        Me.rdbconfirmDate.TabIndex = 1
        Me.rdbconfirmDate.Text = "Confirmation Date"
        '
        'rdbJoiningDate
        '
        Me.rdbJoiningDate.Location = New System.Drawing.Point(23, 21)
        Me.rdbJoiningDate.Name = "rdbJoiningDate"
        Me.rdbJoiningDate.Size = New System.Drawing.Size(82, 18)
        Me.rdbJoiningDate.TabIndex = 0
        Me.rdbJoiningDate.Text = "Joining Date"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(137, 8)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblempcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(713, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'frmLeaveSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(807, 568)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmLeaveSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Leave Setting"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        CType(Me.gvCTC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCTC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.chkLeaveEncash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtminleavebal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboRoundoffType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.txtLapseAfterDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLapseNegativeLeaves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLapseUnAvailed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExceeding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtAvailDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAvailMonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAvailAfter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAvailPPCompDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAvailConfirmDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAvailJoinDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.chkCarryForward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCFUpper, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCFLower, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkAutoAllot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAllotedDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAllotType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPeriodicity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAllotafter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbppcompDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbconfirmDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbJoiningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents rdbJoiningDate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents rdbAllotafter As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbppcompDate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbconfirmDate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtdays As common.MyNumBox
    Friend WithEvents txtmonths As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboRoundoffType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtAvailDays As common.MyNumBox
    Friend WithEvents txtAvailMonths As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents rdbAvailAfter As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAvailPPCompDate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAvailConfirmDate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAvailJoinDate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkLeaveEncash As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtminleavebal As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCFUpper As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtCFLower As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkLapseUnAvailed As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtExceeding As common.MyNumBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents chkCarryForward As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents cboMonth As common.Controls.MyComboBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkLapseNegativeLeaves As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtLapseAfterDays As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvCTC As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents cboPeriodicity As common.Controls.MyComboBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents cboAllotType As common.Controls.MyComboBox
    Friend WithEvents txtAllotedDays As common.MyNumBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtPresentDays As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents chkAutoAllot As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
End Class
