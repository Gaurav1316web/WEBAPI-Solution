<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkGateEntryIn
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.numKmReading = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.LblManualFAT_Per = New common.Controls.MyLabel()
        Me.TxtManualFat_Per = New common.MyNumBox()
        Me.LblManualSNF_Per = New common.Controls.MyLabel()
        Me.TxtManualSNF_Per = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtShiftDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.txtEmpryCans = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFilledCans = New common.MyNumBox()
        Me.lblNoOfCans = New common.Controls.MyLabel()
        Me.lblTransporterName = New common.Controls.MyLabel()
        Me.Txtdds = New common.Controls.MyLabel()
        Me.lblTransporterCode = New common.Controls.MyLabel()
        Me.chkOther = New common.Controls.MyCheckBox()
        Me.spltVehicle = New System.Windows.Forms.SplitContainer()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyTextBox()
        Me.UsLock1 = New common.usLock()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.txtRoute = New common.UserControls.txtFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.lblMcc = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.BtnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblLatePenaltyAmt = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.numKmReading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualFat_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShiftDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpryCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilledCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtdds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spltVehicle.Panel1.SuspendLayout()
        Me.spltVehicle.Panel2.SuspendLayout()
        Me.spltVehicle.SuspendLayout()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLatePenaltyAmt, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLatePenaltyAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.numKmReading)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblManualFAT_Per)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtManualFat_Per)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblManualSNF_Per)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtManualSNF_Per)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtShiftDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpryCans)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFilledCans)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblNoOfCans)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporterName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Txtdds)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporterCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkOther)
        Me.SplitContainer1.Panel1.Controls.Add(Me.spltVehicle)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRouteCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRoute)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRoute)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMcc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMCCCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMCC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBOMStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboShift)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(655, 398)
        Me.SplitContainer1.SplitterDistance = 359
        Me.SplitContainer1.TabIndex = 0
        '
        'numKmReading
        '
        Me.numKmReading.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.numKmReading.CalculationExpression = Nothing
        Me.numKmReading.DecimalPlaces = 0
        Me.numKmReading.FieldCode = Nothing
        Me.numKmReading.FieldDesc = Nothing
        Me.numKmReading.FieldMaxLength = 0
        Me.numKmReading.FieldName = Nothing
        Me.numKmReading.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numKmReading.isCalculatedField = False
        Me.numKmReading.IsSourceFromTable = False
        Me.numKmReading.IsSourceFromValueList = False
        Me.numKmReading.IsUnique = False
        Me.numKmReading.Location = New System.Drawing.Point(347, 157)
        Me.numKmReading.MaxLength = 5
        Me.numKmReading.MendatroryField = False
        Me.numKmReading.MyLinkLable1 = Me.MyLabel4
        Me.numKmReading.MyLinkLable2 = Nothing
        Me.numKmReading.Name = "numKmReading"
        Me.numKmReading.ReferenceFieldDesc = Nothing
        Me.numKmReading.ReferenceFieldName = Nothing
        Me.numKmReading.ReferenceTableName = Nothing
        Me.numKmReading.Size = New System.Drawing.Size(201, 20)
        Me.numKmReading.TabIndex = 1067
        Me.numKmReading.Text = "0"
        Me.numKmReading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numKmReading.Value = 0.0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(272, 160)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel4.TabIndex = 1068
        Me.MyLabel4.Text = "KM Reading"
        '
        'LblManualFAT_Per
        '
        Me.LblManualFAT_Per.FieldName = Nothing
        Me.LblManualFAT_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualFAT_Per.Location = New System.Drawing.Point(7, 233)
        Me.LblManualFAT_Per.Name = "LblManualFAT_Per"
        Me.LblManualFAT_Per.Size = New System.Drawing.Size(91, 16)
        Me.LblManualFAT_Per.TabIndex = 1064
        Me.LblManualFAT_Per.Text = "Opening FAT(%)"
        '
        'TxtManualFat_Per
        '
        Me.TxtManualFat_Per.BackColor = System.Drawing.Color.White
        Me.TxtManualFat_Per.CalculationExpression = Nothing
        Me.TxtManualFat_Per.DecimalPlaces = 3
        Me.TxtManualFat_Per.FieldCode = Nothing
        Me.TxtManualFat_Per.FieldDesc = Nothing
        Me.TxtManualFat_Per.FieldMaxLength = 0
        Me.TxtManualFat_Per.FieldName = Nothing
        Me.TxtManualFat_Per.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtManualFat_Per.isCalculatedField = False
        Me.TxtManualFat_Per.IsSourceFromTable = False
        Me.TxtManualFat_Per.IsSourceFromValueList = False
        Me.TxtManualFat_Per.IsUnique = False
        Me.TxtManualFat_Per.Location = New System.Drawing.Point(105, 229)
        Me.TxtManualFat_Per.MendatroryField = False
        Me.TxtManualFat_Per.MyLinkLable1 = Me.LblManualFAT_Per
        Me.TxtManualFat_Per.MyLinkLable2 = Nothing
        Me.TxtManualFat_Per.Name = "TxtManualFat_Per"
        Me.TxtManualFat_Per.ReferenceFieldDesc = Nothing
        Me.TxtManualFat_Per.ReferenceFieldName = Nothing
        Me.TxtManualFat_Per.ReferenceTableName = Nothing
        Me.TxtManualFat_Per.Size = New System.Drawing.Size(159, 20)
        Me.TxtManualFat_Per.TabIndex = 1063
        Me.TxtManualFat_Per.Text = "0"
        Me.TxtManualFat_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualFat_Per.Value = 0.0R
        '
        'LblManualSNF_Per
        '
        Me.LblManualSNF_Per.FieldName = Nothing
        Me.LblManualSNF_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualSNF_Per.Location = New System.Drawing.Point(7, 257)
        Me.LblManualSNF_Per.Name = "LblManualSNF_Per"
        Me.LblManualSNF_Per.Size = New System.Drawing.Size(92, 16)
        Me.LblManualSNF_Per.TabIndex = 1066
        Me.LblManualSNF_Per.Text = "Opening SNF(%)"
        '
        'TxtManualSNF_Per
        '
        Me.TxtManualSNF_Per.BackColor = System.Drawing.Color.White
        Me.TxtManualSNF_Per.CalculationExpression = Nothing
        Me.TxtManualSNF_Per.DecimalPlaces = 3
        Me.TxtManualSNF_Per.FieldCode = Nothing
        Me.TxtManualSNF_Per.FieldDesc = Nothing
        Me.TxtManualSNF_Per.FieldMaxLength = 0
        Me.TxtManualSNF_Per.FieldName = Nothing
        Me.TxtManualSNF_Per.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtManualSNF_Per.isCalculatedField = False
        Me.TxtManualSNF_Per.IsSourceFromTable = False
        Me.TxtManualSNF_Per.IsSourceFromValueList = False
        Me.TxtManualSNF_Per.IsUnique = False
        Me.TxtManualSNF_Per.Location = New System.Drawing.Point(105, 253)
        Me.TxtManualSNF_Per.MendatroryField = False
        Me.TxtManualSNF_Per.MyLinkLable1 = Me.LblManualSNF_Per
        Me.TxtManualSNF_Per.MyLinkLable2 = Nothing
        Me.TxtManualSNF_Per.Name = "TxtManualSNF_Per"
        Me.TxtManualSNF_Per.ReferenceFieldDesc = Nothing
        Me.TxtManualSNF_Per.ReferenceFieldName = Nothing
        Me.TxtManualSNF_Per.ReferenceTableName = Nothing
        Me.TxtManualSNF_Per.Size = New System.Drawing.Size(159, 20)
        Me.TxtManualSNF_Per.TabIndex = 1065
        Me.TxtManualSNF_Per.Text = "0"
        Me.TxtManualSNF_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualSNF_Per.Value = 0.0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(272, 36)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel3.TabIndex = 1054
        Me.MyLabel3.Text = "Shift Date"
        '
        'txtShiftDate
        '
        Me.txtShiftDate.CalculationExpression = Nothing
        Me.txtShiftDate.CustomFormat = "dd/MM/yyyy"
        Me.txtShiftDate.FieldCode = Nothing
        Me.txtShiftDate.FieldDesc = Nothing
        Me.txtShiftDate.FieldMaxLength = 0
        Me.txtShiftDate.FieldName = Nothing
        Me.txtShiftDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtShiftDate.isCalculatedField = False
        Me.txtShiftDate.IsSourceFromTable = False
        Me.txtShiftDate.IsSourceFromValueList = False
        Me.txtShiftDate.IsUnique = False
        Me.txtShiftDate.Location = New System.Drawing.Point(332, 34)
        Me.txtShiftDate.MendatroryField = True
        Me.txtShiftDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftDate.MyLinkLable1 = Me.MyLabel3
        Me.txtShiftDate.MyLinkLable2 = Nothing
        Me.txtShiftDate.Name = "txtShiftDate"
        Me.txtShiftDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftDate.ReferenceFieldDesc = Nothing
        Me.txtShiftDate.ReferenceFieldName = Nothing
        Me.txtShiftDate.ReferenceTableName = Nothing
        Me.txtShiftDate.Size = New System.Drawing.Size(82, 18)
        Me.txtShiftDate.TabIndex = 1053
        Me.txtShiftDate.TabStop = False
        Me.txtShiftDate.Text = "03/05/2011"
        Me.txtShiftDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 209)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel2.TabIndex = 1052
        Me.MyLabel2.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(105, 205)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(446, 20)
        Me.txtRemarks.TabIndex = 1051
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(7, 108)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel6.TabIndex = 1041
        Me.RadLabel6.Text = "Vehicle No"
        '
        'txtEmpryCans
        '
        Me.txtEmpryCans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtEmpryCans.CalculationExpression = Nothing
        Me.txtEmpryCans.DecimalPlaces = 0
        Me.txtEmpryCans.FieldCode = Nothing
        Me.txtEmpryCans.FieldDesc = Nothing
        Me.txtEmpryCans.FieldMaxLength = 0
        Me.txtEmpryCans.FieldName = Nothing
        Me.txtEmpryCans.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpryCans.isCalculatedField = False
        Me.txtEmpryCans.IsSourceFromTable = False
        Me.txtEmpryCans.IsSourceFromValueList = False
        Me.txtEmpryCans.IsUnique = False
        Me.txtEmpryCans.Location = New System.Drawing.Point(105, 181)
        Me.txtEmpryCans.MaxLength = 5
        Me.txtEmpryCans.MendatroryField = False
        Me.txtEmpryCans.MyLinkLable1 = Me.MyLabel1
        Me.txtEmpryCans.MyLinkLable2 = Nothing
        Me.txtEmpryCans.Name = "txtEmpryCans"
        Me.txtEmpryCans.ReferenceFieldDesc = Nothing
        Me.txtEmpryCans.ReferenceFieldName = Nothing
        Me.txtEmpryCans.ReferenceTableName = Nothing
        Me.txtEmpryCans.Size = New System.Drawing.Size(159, 20)
        Me.txtEmpryCans.TabIndex = 1049
        Me.txtEmpryCans.Text = "0"
        Me.txtEmpryCans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEmpryCans.Value = 0.0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 185)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel1.TabIndex = 1050
        Me.MyLabel1.Text = "Empty Cans"
        '
        'txtFilledCans
        '
        Me.txtFilledCans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFilledCans.CalculationExpression = Nothing
        Me.txtFilledCans.DecimalPlaces = 0
        Me.txtFilledCans.FieldCode = Nothing
        Me.txtFilledCans.FieldDesc = Nothing
        Me.txtFilledCans.FieldMaxLength = 0
        Me.txtFilledCans.FieldName = Nothing
        Me.txtFilledCans.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilledCans.isCalculatedField = False
        Me.txtFilledCans.IsSourceFromTable = False
        Me.txtFilledCans.IsSourceFromValueList = False
        Me.txtFilledCans.IsUnique = False
        Me.txtFilledCans.Location = New System.Drawing.Point(105, 157)
        Me.txtFilledCans.MaxLength = 5
        Me.txtFilledCans.MendatroryField = True
        Me.txtFilledCans.MyLinkLable1 = Me.lblNoOfCans
        Me.txtFilledCans.MyLinkLable2 = Nothing
        Me.txtFilledCans.Name = "txtFilledCans"
        Me.txtFilledCans.ReferenceFieldDesc = Nothing
        Me.txtFilledCans.ReferenceFieldName = Nothing
        Me.txtFilledCans.ReferenceTableName = Nothing
        Me.txtFilledCans.Size = New System.Drawing.Size(159, 20)
        Me.txtFilledCans.TabIndex = 1047
        Me.txtFilledCans.Text = "0"
        Me.txtFilledCans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFilledCans.Value = 0.0R
        '
        'lblNoOfCans
        '
        Me.lblNoOfCans.FieldName = Nothing
        Me.lblNoOfCans.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNoOfCans.Location = New System.Drawing.Point(7, 161)
        Me.lblNoOfCans.Name = "lblNoOfCans"
        Me.lblNoOfCans.Size = New System.Drawing.Size(63, 16)
        Me.lblNoOfCans.TabIndex = 1048
        Me.lblNoOfCans.Text = "Filled Cans"
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = False
        Me.lblTransporterName.BorderVisible = True
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Location = New System.Drawing.Point(270, 132)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(282, 21)
        Me.lblTransporterName.TabIndex = 1046
        Me.lblTransporterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Txtdds
        '
        Me.Txtdds.FieldName = Nothing
        Me.Txtdds.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Txtdds.Location = New System.Drawing.Point(7, 135)
        Me.Txtdds.Name = "Txtdds"
        Me.Txtdds.Size = New System.Drawing.Size(64, 18)
        Me.Txtdds.TabIndex = 1044
        Me.Txtdds.Text = "Transporter"
        '
        'lblTransporterCode
        '
        Me.lblTransporterCode.AutoSize = False
        Me.lblTransporterCode.BorderVisible = True
        Me.lblTransporterCode.FieldName = Nothing
        Me.lblTransporterCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTransporterCode.Location = New System.Drawing.Point(105, 132)
        Me.lblTransporterCode.Name = "lblTransporterCode"
        Me.lblTransporterCode.Size = New System.Drawing.Size(159, 21)
        Me.lblTransporterCode.TabIndex = 1045
        Me.lblTransporterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkOther
        '
        Me.chkOther.Location = New System.Drawing.Point(272, 110)
        Me.chkOther.MyLinkLable1 = Nothing
        Me.chkOther.MyLinkLable2 = Nothing
        Me.chkOther.Name = "chkOther"
        Me.chkOther.Size = New System.Drawing.Size(88, 18)
        Me.chkOther.TabIndex = 1043
        Me.chkOther.Tag1 = Nothing
        Me.chkOther.Text = "Other Vehicle"
        '
        'spltVehicle
        '
        Me.spltVehicle.Location = New System.Drawing.Point(105, 106)
        Me.spltVehicle.Name = "spltVehicle"
        '
        'spltVehicle.Panel1
        '
        Me.spltVehicle.Panel1.Controls.Add(Me.lblVehicleNo)
        '
        'spltVehicle.Panel2
        '
        Me.spltVehicle.Panel2.Controls.Add(Me.txtVehicleNo)
        Me.spltVehicle.Size = New System.Drawing.Size(159, 22)
        Me.spltVehicle.SplitterDistance = 53
        Me.spltVehicle.TabIndex = 1042
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = False
        Me.lblVehicleNo.BorderVisible = True
        Me.lblVehicleNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVehicleNo.Location = New System.Drawing.Point(0, 0)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(53, 22)
        Me.lblVehicleNo.TabIndex = 34
        Me.lblVehicleNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(0, 0)
        Me.txtVehicleNo.MaxLength = 200
        Me.txtVehicleNo.MendatroryField = True
        Me.txtVehicleNo.MyLinkLable1 = Me.RadLabel6
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(102, 22)
        Me.txtVehicleNo.TabIndex = 1040
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(451, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(101, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1039
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(7, 84)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(65, 18)
        Me.lblRouteCode.TabIndex = 1037
        Me.lblRouteCode.Text = "Route Code"
        '
        'txtRoute
        '
        Me.txtRoute.CalculationExpression = Nothing
        Me.txtRoute.FieldCode = Nothing
        Me.txtRoute.FieldDesc = Nothing
        Me.txtRoute.FieldMaxLength = 0
        Me.txtRoute.FieldName = Nothing
        Me.txtRoute.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRoute.isCalculatedField = False
        Me.txtRoute.IsSourceFromTable = False
        Me.txtRoute.IsSourceFromValueList = False
        Me.txtRoute.IsUnique = False
        Me.txtRoute.Location = New System.Drawing.Point(105, 81)
        Me.txtRoute.MendatroryField = True
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.lblRouteCode
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyReadOnly = False
        Me.txtRoute.MyShowMasterFormButton = False
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(159, 21)
        Me.txtRoute.TabIndex = 1036
        Me.txtRoute.Value = ""
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = False
        Me.lblRoute.BorderVisible = True
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Location = New System.Drawing.Point(270, 81)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(282, 21)
        Me.lblRoute.TabIndex = 1038
        Me.lblRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMcc
        '
        Me.lblMcc.AutoSize = False
        Me.lblMcc.BorderVisible = True
        Me.lblMcc.FieldName = Nothing
        Me.lblMcc.Location = New System.Drawing.Point(270, 56)
        Me.lblMcc.Name = "lblMcc"
        Me.lblMcc.Size = New System.Drawing.Size(282, 21)
        Me.lblMcc.TabIndex = 1035
        Me.lblMcc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(7, 59)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 1034
        Me.lblMCCCode.Text = "MCC Code"
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(105, 56)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.lblMCCCode
        Me.txtMCC.MyLinkLable2 = Me.lblMcc
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(159, 21)
        Me.txtMCC.TabIndex = 1033
        Me.txtMCC.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(105, 7)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(320, 21)
        Me.txtCode.TabIndex = 1032
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(7, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(78, 16)
        Me.lblCode.TabIndex = 1026
        Me.lblCode.Text = "Gate Entry No"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(425, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 1031
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(419, 36)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(29, 16)
        Me.lblBOMStatus.TabIndex = 1030
        Me.lblBOMStatus.Text = "Shift"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Location = New System.Drawing.Point(450, 34)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(102, 18)
        Me.cboShift.TabIndex = 1029
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(7, 36)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(87, 16)
        Me.lblDocDate.TabIndex = 1028
        Me.lblDocDate.Text = "Gate Entry Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(105, 32)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDocDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(159, 20)
        Me.txtDate.TabIndex = 1027
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(212, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 21)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(584, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 21)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.Visible = False
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(143, 7)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 21)
        Me.BtnPost.TabIndex = 4
        Me.BtnPost.Text = "Post"
        Me.BtnPost.Visible = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(74, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 3
        Me.btndelete.Text = "Delete"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(272, 182)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel5.TabIndex = 1069
        Me.MyLabel5.Text = "Late Penalty"
        '
        'lblLatePenaltyAmt
        '
        Me.lblLatePenaltyAmt.AutoSize = False
        Me.lblLatePenaltyAmt.BorderVisible = True
        Me.lblLatePenaltyAmt.FieldName = Nothing
        Me.lblLatePenaltyAmt.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblLatePenaltyAmt.Location = New System.Drawing.Point(347, 181)
        Me.lblLatePenaltyAmt.Name = "lblLatePenaltyAmt"
        Me.lblLatePenaltyAmt.Size = New System.Drawing.Size(201, 21)
        Me.lblLatePenaltyAmt.TabIndex = 1070
        Me.lblLatePenaltyAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmMilkGateEntryIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 398)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMilkGateEntryIn"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Gate Entry In"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.numKmReading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualFat_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShiftDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpryCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilledCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtdds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spltVehicle.Panel1.ResumeLayout(False)
        Me.spltVehicle.Panel2.ResumeLayout(False)
        Me.spltVehicle.Panel2.PerformLayout()
        Me.spltVehicle.ResumeLayout(False)
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLatePenaltyAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblMcc As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents spltVehicle As System.Windows.Forms.SplitContainer
    Friend WithEvents txtVehicleNo As common.Controls.MyTextBox
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents chkOther As common.Controls.MyCheckBox
    Friend WithEvents lblTransporterName As common.Controls.MyLabel
    Friend WithEvents Txtdds As common.Controls.MyLabel
    Friend WithEvents lblTransporterCode As common.Controls.MyLabel
    Friend WithEvents txtEmpryCans As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFilledCans As common.MyNumBox
    Friend WithEvents lblNoOfCans As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtShiftDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblManualFAT_Per As common.Controls.MyLabel
    Friend WithEvents TxtManualFat_Per As common.MyNumBox
    Friend WithEvents LblManualSNF_Per As common.Controls.MyLabel
    Friend WithEvents TxtManualSNF_Per As common.MyNumBox
    Friend WithEvents numKmReading As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblLatePenaltyAmt As common.Controls.MyLabel
End Class
