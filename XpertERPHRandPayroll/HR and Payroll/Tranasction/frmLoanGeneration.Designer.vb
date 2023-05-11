Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoanGeneration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoanGeneration))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblDivisionName = New common.Controls.MyLabel()
        Me.fndDivision = New common.UserControls.txtFinder()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.btnGenerate = New Telerik.WinControls.UI.RadButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblGeneratedByName = New common.Controls.MyLabel()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblPayPeriodCode = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblGeneratedBy = New common.Controls.MyLabel()
        Me.findGeneratedBy = New common.UserControls.txtFinder()
        Me.lblGenerateDate = New common.Controls.MyLabel()
        Me.dtpGenerateDate = New common.Controls.MyDateTimePicker()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblGenerationCode = New common.Controls.MyLabel()
        Me.gvLoanGeneration = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGeneratedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGeneratedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGenerateDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGenerateDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGenerationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLoanGeneration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLoanGeneration.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox3.Size = New System.Drawing.Size(843, 518)
        Me.RadGroupBox3.TabIndex = 64
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDivisionName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGenerate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGeneratedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGeneratedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findGeneratedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGenerateDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpGenerateDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGenerationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvLoanGeneration)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(823, 488)
        Me.SplitContainer1.SplitterDistance = 439
        Me.SplitContainer1.TabIndex = 0
        '
        'lblDivisionName
        '
        Me.lblDivisionName.AutoSize = False
        Me.lblDivisionName.BorderVisible = True
        Me.lblDivisionName.FieldName = Nothing
        Me.lblDivisionName.Location = New System.Drawing.Point(354, 88)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.Size = New System.Drawing.Size(222, 19)
        Me.lblDivisionName.TabIndex = 213
        Me.lblDivisionName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.fndDivision.Location = New System.Drawing.Point(132, 88)
        Me.fndDivision.MendatroryField = True
        Me.fndDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDivision.MyLinkLable1 = Me.lblDivision
        Me.fndDivision.MyLinkLable2 = Nothing
        Me.fndDivision.MyReadOnly = False
        Me.fndDivision.MyShowMasterFormButton = False
        Me.fndDivision.Name = "fndDivision"
        Me.fndDivision.ReferenceFieldDesc = Nothing
        Me.fndDivision.ReferenceFieldName = Nothing
        Me.fndDivision.ReferenceTableName = Nothing
        Me.fndDivision.Size = New System.Drawing.Size(221, 19)
        Me.fndDivision.TabIndex = 212
        Me.fndDivision.Value = ""
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.Location = New System.Drawing.Point(12, 91)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(46, 16)
        Me.lblDivision.TabIndex = 214
        Me.lblDivision.Text = "Division"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(354, 66)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(222, 19)
        Me.lblLocationName.TabIndex = 210
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.fndLocation.Location = New System.Drawing.Point(132, 66)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.lblLocation
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(221, 19)
        Me.fndLocation.TabIndex = 209
        Me.fndLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(12, 69)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 211
        Me.lblLocation.Text = "Location"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(478, 20)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 208
        '
        'btnGenerate
        '
        Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.Location = New System.Drawing.Point(698, 188)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(116, 18)
        Me.btnGenerate.TabIndex = 8
        Me.btnGenerate.Text = "Generate Loan"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(354, 20)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'lblGeneratedByName
        '
        Me.lblGeneratedByName.AutoSize = False
        Me.lblGeneratedByName.BorderVisible = True
        Me.lblGeneratedByName.FieldName = Nothing
        Me.lblGeneratedByName.Location = New System.Drawing.Point(358, 136)
        Me.lblGeneratedByName.Name = "lblGeneratedByName"
        Me.lblGeneratedByName.Size = New System.Drawing.Size(222, 19)
        Me.lblGeneratedByName.TabIndex = 6
        Me.lblGeneratedByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(354, 44)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(222, 19)
        Me.lblPayPeriodName.TabIndex = 3
        Me.lblPayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'findPayperiod
        '
        Me.findPayperiod.CalculationExpression = Nothing
        Me.findPayperiod.FieldCode = Nothing
        Me.findPayperiod.FieldDesc = Nothing
        Me.findPayperiod.FieldMaxLength = 0
        Me.findPayperiod.FieldName = Nothing
        Me.findPayperiod.isCalculatedField = False
        Me.findPayperiod.IsSourceFromTable = False
        Me.findPayperiod.IsSourceFromValueList = False
        Me.findPayperiod.IsUnique = False
        Me.findPayperiod.Location = New System.Drawing.Point(132, 44)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Me.lblPayPeriodCode
        Me.findPayperiod.MyLinkLable2 = Nothing
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(221, 19)
        Me.findPayperiod.TabIndex = 2
        Me.findPayperiod.Value = ""
        '
        'lblPayPeriodCode
        '
        Me.lblPayPeriodCode.FieldName = Nothing
        Me.lblPayPeriodCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriodCode.Location = New System.Drawing.Point(12, 47)
        Me.lblPayPeriodCode.Name = "lblPayPeriodCode"
        Me.lblPayPeriodCode.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriodCode.TabIndex = 198
        Me.lblPayPeriodCode.Text = "Pay Period Code"
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
        Me.txtDescription.Location = New System.Drawing.Point(131, 161)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(313, 45)
        Me.txtDescription.TabIndex = 7
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(17, 158)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'lblGeneratedBy
        '
        Me.lblGeneratedBy.FieldName = Nothing
        Me.lblGeneratedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneratedBy.Location = New System.Drawing.Point(14, 136)
        Me.lblGeneratedBy.Name = "lblGeneratedBy"
        Me.lblGeneratedBy.Size = New System.Drawing.Size(75, 16)
        Me.lblGeneratedBy.TabIndex = 165
        Me.lblGeneratedBy.Text = "Generated by"
        '
        'findGeneratedBy
        '
        Me.findGeneratedBy.CalculationExpression = Nothing
        Me.findGeneratedBy.FieldCode = Nothing
        Me.findGeneratedBy.FieldDesc = Nothing
        Me.findGeneratedBy.FieldMaxLength = 0
        Me.findGeneratedBy.FieldName = Nothing
        Me.findGeneratedBy.isCalculatedField = False
        Me.findGeneratedBy.IsSourceFromTable = False
        Me.findGeneratedBy.IsSourceFromValueList = False
        Me.findGeneratedBy.IsUnique = False
        Me.findGeneratedBy.Location = New System.Drawing.Point(131, 136)
        Me.findGeneratedBy.MendatroryField = True
        Me.findGeneratedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findGeneratedBy.MyLinkLable1 = Me.lblGeneratedBy
        Me.findGeneratedBy.MyLinkLable2 = Nothing
        Me.findGeneratedBy.MyReadOnly = False
        Me.findGeneratedBy.MyShowMasterFormButton = False
        Me.findGeneratedBy.Name = "findGeneratedBy"
        Me.findGeneratedBy.ReferenceFieldDesc = Nothing
        Me.findGeneratedBy.ReferenceFieldName = Nothing
        Me.findGeneratedBy.ReferenceTableName = Nothing
        Me.findGeneratedBy.Size = New System.Drawing.Size(221, 19)
        Me.findGeneratedBy.TabIndex = 5
        Me.findGeneratedBy.Value = ""
        '
        'lblGenerateDate
        '
        Me.lblGenerateDate.FieldName = Nothing
        Me.lblGenerateDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGenerateDate.Location = New System.Drawing.Point(14, 113)
        Me.lblGenerateDate.Name = "lblGenerateDate"
        Me.lblGenerateDate.Size = New System.Drawing.Size(80, 16)
        Me.lblGenerateDate.TabIndex = 164
        Me.lblGenerateDate.Text = "Generate Date"
        '
        'dtpGenerateDate
        '
        Me.dtpGenerateDate.CalculationExpression = Nothing
        Me.dtpGenerateDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpGenerateDate.FieldCode = Nothing
        Me.dtpGenerateDate.FieldDesc = Nothing
        Me.dtpGenerateDate.FieldMaxLength = 0
        Me.dtpGenerateDate.FieldName = Nothing
        Me.dtpGenerateDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpGenerateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGenerateDate.isCalculatedField = False
        Me.dtpGenerateDate.IsSourceFromTable = False
        Me.dtpGenerateDate.IsSourceFromValueList = False
        Me.dtpGenerateDate.IsUnique = False
        Me.dtpGenerateDate.Location = New System.Drawing.Point(132, 113)
        Me.dtpGenerateDate.MendatroryField = True
        Me.dtpGenerateDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGenerateDate.MyLinkLable1 = Me.lblGenerateDate
        Me.dtpGenerateDate.MyLinkLable2 = Nothing
        Me.dtpGenerateDate.Name = "dtpGenerateDate"
        Me.dtpGenerateDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGenerateDate.ReferenceFieldDesc = Nothing
        Me.dtpGenerateDate.ReferenceFieldName = Nothing
        Me.dtpGenerateDate.ReferenceTableName = Nothing
        Me.dtpGenerateDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpGenerateDate.TabIndex = 4
        Me.dtpGenerateDate.TabStop = False
        Me.dtpGenerateDate.Text = "03/05/2011"
        Me.dtpGenerateDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(133, 19)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblGenerationCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblGenerationCode
        '
        Me.lblGenerationCode.FieldName = Nothing
        Me.lblGenerationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGenerationCode.Location = New System.Drawing.Point(12, 24)
        Me.lblGenerationCode.Name = "lblGenerationCode"
        Me.lblGenerationCode.Size = New System.Drawing.Size(92, 16)
        Me.lblGenerationCode.TabIndex = 161
        Me.lblGenerationCode.Text = "Generation Code"
        '
        'gvLoanGeneration
        '
        Me.gvLoanGeneration.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gvLoanGeneration.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvLoanGeneration.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvLoanGeneration.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvLoanGeneration.ForeColor = System.Drawing.Color.Black
        Me.gvLoanGeneration.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvLoanGeneration.Location = New System.Drawing.Point(3, 212)
        '
        'gvLoanGeneration
        '
        Me.gvLoanGeneration.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvLoanGeneration.MasterTemplate.AllowAddNewRow = False
        Me.gvLoanGeneration.MasterTemplate.AutoGenerateColumns = False
        Me.gvLoanGeneration.MasterTemplate.EnableGrouping = False
        Me.gvLoanGeneration.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvLoanGeneration.Name = "gvLoanGeneration"
        Me.gvLoanGeneration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvLoanGeneration.ShowHeaderCellButtons = True
        Me.gvLoanGeneration.Size = New System.Drawing.Size(817, 223)
        Me.gvLoanGeneration.TabIndex = 9
        Me.gvLoanGeneration.TabStop = False
        Me.gvLoanGeneration.Text = "RadGridView4"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(75, 18)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 18)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(748, 18)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(141, 18)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmLoanGeneration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(843, 518)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmLoanGeneration"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Loan Generation"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGeneratedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGeneratedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGenerateDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGenerateDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGenerationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLoanGeneration.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLoanGeneration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblGeneratedBy As common.Controls.MyLabel
    Friend WithEvents findGeneratedBy As common.UserControls.txtFinder
    Friend WithEvents lblGenerateDate As common.Controls.MyLabel
    Friend WithEvents dtpGenerateDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblGenerationCode As common.Controls.MyLabel
    Friend WithEvents gvLoanGeneration As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblPayPeriodCode As common.Controls.MyLabel
    Friend WithEvents lblGeneratedByName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGenerate As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents fndDivision As common.UserControls.txtFinder
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
End Class
