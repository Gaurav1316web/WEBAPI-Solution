Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWeeklyHolidays
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtEmp = New common.UserControls.txtMultiSelectFinder()
        Me.lblCompany = New common.Controls.MyLabel()
        Me.fndDivision = New common.UserControls.txtFinder()
        Me.lblDivisionName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.lblApplyOn = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.CboSelectedDay = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblcode = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.CboapplyIn = New common.Controls.MyComboBox()
        Me.dtpApplicableFrom = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplyOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboSelectedDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboapplyIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox3.Size = New System.Drawing.Size(729, 464)
        Me.RadGroupBox3.TabIndex = 59
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCompany)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDivisionName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApplyOn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CboSelectedDay)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel19)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CboapplyIn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpApplicableFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(709, 434)
        Me.SplitContainer1.SplitterDistance = 382
        Me.SplitContainer1.TabIndex = 0
        '
        'txtEmp
        '
        Me.txtEmp.arrDispalyMember = Nothing
        Me.txtEmp.arrValueMember = Nothing
        Me.txtEmp.Location = New System.Drawing.Point(102, 127)
        Me.txtEmp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.MyLinkLable1 = Me.lblCompany
        Me.txtEmp.MyLinkLable2 = Nothing
        Me.txtEmp.MyNullText = "Please select..."
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.Size = New System.Drawing.Size(460, 19)
        Me.txtEmp.TabIndex = 388
        '
        'lblCompany
        '
        Me.lblCompany.FieldName = Nothing
        Me.lblCompany.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(16, 126)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(60, 18)
        Me.lblCompany.TabIndex = 387
        Me.lblCompany.Text = "Employees"
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
        Me.fndDivision.Location = New System.Drawing.Point(102, 102)
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
        Me.fndDivision.Size = New System.Drawing.Size(215, 19)
        Me.fndDivision.TabIndex = 205
        Me.fndDivision.Value = ""
        '
        'lblDivisionName
        '
        Me.lblDivisionName.AutoSize = False
        Me.lblDivisionName.BorderVisible = True
        Me.lblDivisionName.FieldName = Nothing
        Me.lblDivisionName.Location = New System.Drawing.Point(323, 102)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.Size = New System.Drawing.Size(239, 19)
        Me.lblDivisionName.TabIndex = 204
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(17, 104)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel5.TabIndex = 203
        Me.MyLabel5.Text = "Division Code"
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
        Me.fndLocation.Location = New System.Drawing.Point(102, 80)
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
        Me.fndLocation.Size = New System.Drawing.Size(215, 19)
        Me.fndLocation.TabIndex = 202
        Me.fndLocation.Value = ""
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(323, 80)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(239, 19)
        Me.lblLocationName.TabIndex = 201
        '
        'lblLocationCode
        '
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(17, 82)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(79, 16)
        Me.lblLocationCode.TabIndex = 200
        Me.lblLocationCode.Text = "Location Code"
        '
        'lblApplyOn
        '
        Me.lblApplyOn.AutoSize = False
        Me.lblApplyOn.BorderVisible = True
        Me.lblApplyOn.FieldName = Nothing
        Me.lblApplyOn.Location = New System.Drawing.Point(454, 57)
        Me.lblApplyOn.Name = "lblApplyOn"
        Me.lblApplyOn.Size = New System.Drawing.Size(108, 18)
        Me.lblApplyOn.TabIndex = 6
        Me.lblApplyOn.Visible = False
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(318, 9)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 1
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(15, 152)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AutoGenerateColumns = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(679, 219)
        Me.gv1.TabIndex = 7
        Me.gv1.TabStop = False
        '
        'CboSelectedDay
        '
        Me.CboSelectedDay.CalculationExpression = Nothing
        Me.CboSelectedDay.DropDownAnimationEnabled = True
        Me.CboSelectedDay.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboSelectedDay.FieldCode = Nothing
        Me.CboSelectedDay.FieldDesc = Nothing
        Me.CboSelectedDay.FieldMaxLength = 0
        Me.CboSelectedDay.FieldName = Nothing
        Me.CboSelectedDay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSelectedDay.isCalculatedField = False
        Me.CboSelectedDay.IsSourceFromTable = False
        Me.CboSelectedDay.IsSourceFromValueList = False
        Me.CboSelectedDay.IsUnique = False
        RadListDataItem1.Text = "Yes"
        RadListDataItem2.Text = "No"
        Me.CboSelectedDay.Items.Add(RadListDataItem1)
        Me.CboSelectedDay.Items.Add(RadListDataItem2)
        Me.CboSelectedDay.Location = New System.Drawing.Point(102, 56)
        Me.CboSelectedDay.MendatroryField = False
        Me.CboSelectedDay.MyLinkLable1 = Me.MyLabel3
        Me.CboSelectedDay.MyLinkLable2 = Nothing
        Me.CboSelectedDay.Name = "CboSelectedDay"
        Me.CboSelectedDay.ReferenceFieldDesc = Nothing
        Me.CboSelectedDay.ReferenceFieldName = Nothing
        Me.CboSelectedDay.ReferenceTableName = Nothing
        Me.CboSelectedDay.Size = New System.Drawing.Size(215, 18)
        Me.CboSelectedDay.TabIndex = 5
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(16, 57)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel3.TabIndex = 151
        Me.MyLabel3.Text = "Select Day"
        '
        'lblcode
        '
        Me.lblcode.FieldName = Nothing
        Me.lblcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcode.Location = New System.Drawing.Point(15, 11)
        Me.lblcode.Name = "lblcode"
        Me.lblcode.Size = New System.Drawing.Size(72, 16)
        Me.lblcode.TabIndex = 56
        Me.lblcode.Text = "Setting Code"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(15, 34)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel18.TabIndex = 55
        Me.MyLabel18.Text = "Setting Name"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(357, 58)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel1.TabIndex = 149
        Me.MyLabel1.Text = "Applied On"
        Me.MyLabel1.Visible = False
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
        Me.txtName.Location = New System.Drawing.Point(102, 33)
        Me.txtName.MaxLength = 49
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.MyLabel18
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(215, 18)
        Me.txtName.TabIndex = 3
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(367, 34)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel19.TabIndex = 107
        Me.MyLabel19.Text = "Apply In"
        Me.MyLabel19.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(357, 13)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel2.TabIndex = 147
        Me.MyLabel2.Text = "Applicable From"
        '
        'CboapplyIn
        '
        Me.CboapplyIn.CalculationExpression = Nothing
        Me.CboapplyIn.DropDownAnimationEnabled = True
        Me.CboapplyIn.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboapplyIn.FieldCode = Nothing
        Me.CboapplyIn.FieldDesc = Nothing
        Me.CboapplyIn.FieldMaxLength = 0
        Me.CboapplyIn.FieldName = Nothing
        Me.CboapplyIn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboapplyIn.isCalculatedField = False
        Me.CboapplyIn.IsSourceFromTable = False
        Me.CboapplyIn.IsSourceFromValueList = False
        Me.CboapplyIn.IsUnique = False
        RadListDataItem3.Text = "Yes"
        RadListDataItem4.Text = "No"
        Me.CboapplyIn.Items.Add(RadListDataItem3)
        Me.CboapplyIn.Items.Add(RadListDataItem4)
        Me.CboapplyIn.Location = New System.Drawing.Point(454, 33)
        Me.CboapplyIn.MendatroryField = False
        Me.CboapplyIn.MyLinkLable1 = Me.MyLabel19
        Me.CboapplyIn.MyLinkLable2 = Nothing
        Me.CboapplyIn.Name = "CboapplyIn"
        Me.CboapplyIn.ReferenceFieldDesc = Nothing
        Me.CboapplyIn.ReferenceFieldName = Nothing
        Me.CboapplyIn.ReferenceTableName = Nothing
        Me.CboapplyIn.Size = New System.Drawing.Size(215, 18)
        Me.CboapplyIn.TabIndex = 4
        Me.CboapplyIn.Visible = False
        '
        'dtpApplicableFrom
        '
        Me.dtpApplicableFrom.CalculationExpression = Nothing
        Me.dtpApplicableFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpApplicableFrom.FieldCode = Nothing
        Me.dtpApplicableFrom.FieldDesc = Nothing
        Me.dtpApplicableFrom.FieldMaxLength = 0
        Me.dtpApplicableFrom.FieldName = Nothing
        Me.dtpApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpApplicableFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApplicableFrom.isCalculatedField = False
        Me.dtpApplicableFrom.IsSourceFromTable = False
        Me.dtpApplicableFrom.IsSourceFromValueList = False
        Me.dtpApplicableFrom.IsUnique = False
        Me.dtpApplicableFrom.Location = New System.Drawing.Point(454, 12)
        Me.dtpApplicableFrom.MendatroryField = False
        Me.dtpApplicableFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.MyLinkLable1 = Me.MyLabel2
        Me.dtpApplicableFrom.MyLinkLable2 = Nothing
        Me.dtpApplicableFrom.Name = "dtpApplicableFrom"
        Me.dtpApplicableFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.ReferenceFieldDesc = Nothing
        Me.dtpApplicableFrom.ReferenceFieldName = Nothing
        Me.dtpApplicableFrom.ReferenceTableName = Nothing
        Me.dtpApplicableFrom.Size = New System.Drawing.Size(108, 18)
        Me.dtpApplicableFrom.TabIndex = 2
        Me.dtpApplicableFrom.TabStop = False
        Me.dtpApplicableFrom.Text = "03/05/2011"
        Me.dtpApplicableFrom.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(102, 9)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(215, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(17, 14)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(89, 14)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(628, 14)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'frmWeeklyHolidays
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 464)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmWeeklyHolidays"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Weekly Holidays"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplyOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboSelectedDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboapplyIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblcode As common.Controls.MyLabel
    Friend WithEvents CboapplyIn As common.Controls.MyComboBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpApplicableFrom As common.Controls.MyDateTimePicker
    Friend WithEvents CboSelectedDay As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblApplyOn As common.Controls.MyLabel
    Friend WithEvents fndDivision As common.UserControls.txtFinder
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents lblCompany As common.Controls.MyLabel
    Friend WithEvents txtEmp As common.UserControls.txtMultiSelectFinder
End Class
