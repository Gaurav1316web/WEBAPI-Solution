<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEmpIncrement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmpIncrement))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.LocationCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblNewSalary = New common.Controls.MyLabel()
        Me.lblTotalInc1 = New common.Controls.MyLabel()
        Me.lblNewSalaryTotal = New common.Controls.MyLabel()
        Me.lblIncTotal = New common.Controls.MyLabel()
        Me.lblTotalSalary1 = New common.Controls.MyLabel()
        Me.lblTotalSalary = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.dtpArrearFrom = New common.Controls.MyDateTimePicker()
        Me.lblDivisionCode = New common.Controls.MyLabel()
        Me.lblDivisionName = New common.Controls.MyLabel()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lbLocationCode = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblSalaryStructCode = New common.Controls.MyLabel()
        Me.lblIncrementNo = New common.Controls.MyLabel()
        Me.lblDate = New common.Controls.MyLabel()
        Me.fndIncrementCode = New common.UserControls.txtNavigator()
        Me.txtIncrementDate = New common.Controls.MyDateTimePicker()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblSalaryCode = New common.Controls.MyLabel()
        Me.txtRevisionNo = New common.MyNumBox()
        Me.lblEmpSalaryCode = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndEmpCode = New common.UserControls.txtFinder()
        Me.dtpApplicableFrom = New common.Controls.MyDateTimePicker()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.lblSalStructName = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.gvSalary = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuImportWithIncrementAmt = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuImportWithFinalSalary = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExportWithIncrementAmt = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExportWithNewSalary = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNewSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalInc1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNewSalaryTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalSalary1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpArrearFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalaryStructCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncrementNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIncrementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalaryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRevisionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpSalaryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalStructName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSalary.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1150, 439)
        Me.SplitContainer1.SplitterDistance = 400
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.LocationCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblNewSalary)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTotalInc1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblNewSalaryTotal)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblIncTotal)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTotalSalary1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTotalSalary)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpArrearFrom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDivisionCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDivisionName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDivision)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbLocationCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSalaryStructCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblIncrementNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndIncrementCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtIncrementDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSalaryCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRevisionNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblEmpSalaryCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndEmpCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpApplicableFrom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblempcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSalStructName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvSalary)
        Me.SplitContainer2.Size = New System.Drawing.Size(1150, 400)
        Me.SplitContainer2.SplitterDistance = 179
        Me.SplitContainer2.TabIndex = 218
        '
        'LocationCode
        '
        Me.LocationCode.CalculationExpression = Nothing
        Me.LocationCode.FieldCode = Nothing
        Me.LocationCode.FieldDesc = Nothing
        Me.LocationCode.FieldMaxLength = 0
        Me.LocationCode.FieldName = Nothing
        Me.LocationCode.isCalculatedField = False
        Me.LocationCode.IsSourceFromTable = False
        Me.LocationCode.IsSourceFromValueList = False
        Me.LocationCode.IsUnique = False
        Me.LocationCode.Location = New System.Drawing.Point(103, 109)
        Me.LocationCode.MendatroryField = True
        Me.LocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LocationCode.MyLinkLable1 = Me.MyLabel2
        Me.LocationCode.MyLinkLable2 = Nothing
        Me.LocationCode.MyReadOnly = False
        Me.LocationCode.MyShowMasterFormButton = False
        Me.LocationCode.Name = "LocationCode"
        Me.LocationCode.ReferenceFieldDesc = Nothing
        Me.LocationCode.ReferenceFieldName = Nothing
        Me.LocationCode.ReferenceTableName = Nothing
        Me.LocationCode.Size = New System.Drawing.Size(260, 19)
        Me.LocationCode.TabIndex = 252
        Me.LocationCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 156)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel2.TabIndex = 213
        Me.MyLabel2.Text = "Applicable From"
        '
        'lblNewSalary
        '
        Me.lblNewSalary.AutoSize = False
        Me.lblNewSalary.BorderVisible = True
        Me.lblNewSalary.FieldName = Nothing
        Me.lblNewSalary.Location = New System.Drawing.Point(760, 132)
        Me.lblNewSalary.Name = "lblNewSalary"
        Me.lblNewSalary.Size = New System.Drawing.Size(157, 19)
        Me.lblNewSalary.TabIndex = 231
        '
        'lblTotalInc1
        '
        Me.lblTotalInc1.AutoSize = False
        Me.lblTotalInc1.BorderVisible = True
        Me.lblTotalInc1.FieldName = Nothing
        Me.lblTotalInc1.Location = New System.Drawing.Point(760, 107)
        Me.lblTotalInc1.Name = "lblTotalInc1"
        Me.lblTotalInc1.Size = New System.Drawing.Size(157, 19)
        Me.lblTotalInc1.TabIndex = 231
        '
        'lblNewSalaryTotal
        '
        Me.lblNewSalaryTotal.FieldName = Nothing
        Me.lblNewSalaryTotal.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblNewSalaryTotal.Location = New System.Drawing.Point(665, 133)
        Me.lblNewSalaryTotal.Name = "lblNewSalaryTotal"
        Me.lblNewSalaryTotal.Size = New System.Drawing.Size(90, 18)
        Me.lblNewSalaryTotal.TabIndex = 230
        Me.lblNewSalaryTotal.Text = "Total New Salary"
        '
        'lblIncTotal
        '
        Me.lblIncTotal.FieldName = Nothing
        Me.lblIncTotal.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblIncTotal.Location = New System.Drawing.Point(665, 108)
        Me.lblIncTotal.Name = "lblIncTotal"
        Me.lblIncTotal.Size = New System.Drawing.Size(85, 18)
        Me.lblIncTotal.TabIndex = 230
        Me.lblIncTotal.Text = "Total Increment"
        '
        'lblTotalSalary1
        '
        Me.lblTotalSalary1.AutoSize = False
        Me.lblTotalSalary1.BorderVisible = True
        Me.lblTotalSalary1.FieldName = Nothing
        Me.lblTotalSalary1.Location = New System.Drawing.Point(761, 84)
        Me.lblTotalSalary1.Name = "lblTotalSalary1"
        Me.lblTotalSalary1.Size = New System.Drawing.Size(157, 19)
        Me.lblTotalSalary1.TabIndex = 229
        '
        'lblTotalSalary
        '
        Me.lblTotalSalary.FieldName = Nothing
        Me.lblTotalSalary.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTotalSalary.Location = New System.Drawing.Point(665, 85)
        Me.lblTotalSalary.Name = "lblTotalSalary"
        Me.lblTotalSalary.Size = New System.Drawing.Size(64, 18)
        Me.lblTotalSalary.TabIndex = 228
        Me.lblTotalSalary.Text = "Total Salary"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(250, 157)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel3.TabIndex = 227
        Me.MyLabel3.Text = "Calculate Arrear From"
        '
        'dtpArrearFrom
        '
        Me.dtpArrearFrom.CalculationExpression = Nothing
        Me.dtpArrearFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpArrearFrom.FieldCode = Nothing
        Me.dtpArrearFrom.FieldDesc = Nothing
        Me.dtpArrearFrom.FieldMaxLength = 0
        Me.dtpArrearFrom.FieldName = Nothing
        Me.dtpArrearFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpArrearFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpArrearFrom.isCalculatedField = False
        Me.dtpArrearFrom.IsSourceFromTable = False
        Me.dtpArrearFrom.IsSourceFromValueList = False
        Me.dtpArrearFrom.IsUnique = False
        Me.dtpArrearFrom.Location = New System.Drawing.Point(372, 156)
        Me.dtpArrearFrom.MendatroryField = False
        Me.dtpArrearFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpArrearFrom.MyLinkLable1 = Nothing
        Me.dtpArrearFrom.MyLinkLable2 = Nothing
        Me.dtpArrearFrom.Name = "dtpArrearFrom"
        Me.dtpArrearFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpArrearFrom.ReferenceFieldDesc = Nothing
        Me.dtpArrearFrom.ReferenceFieldName = Nothing
        Me.dtpArrearFrom.ReferenceTableName = Nothing
        Me.dtpArrearFrom.Size = New System.Drawing.Size(143, 18)
        Me.dtpArrearFrom.TabIndex = 226
        Me.dtpArrearFrom.TabStop = False
        Me.dtpArrearFrom.Text = "03/05/2011"
        Me.dtpArrearFrom.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblDivisionCode
        '
        Me.lblDivisionCode.AutoSize = False
        Me.lblDivisionCode.BorderVisible = True
        Me.lblDivisionCode.FieldName = Nothing
        Me.lblDivisionCode.Location = New System.Drawing.Point(103, 131)
        Me.lblDivisionCode.Name = "lblDivisionCode"
        Me.lblDivisionCode.Size = New System.Drawing.Size(260, 19)
        Me.lblDivisionCode.TabIndex = 225
        '
        'lblDivisionName
        '
        Me.lblDivisionName.AutoSize = False
        Me.lblDivisionName.BorderVisible = True
        Me.lblDivisionName.FieldName = Nothing
        Me.lblDivisionName.Location = New System.Drawing.Point(364, 131)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.Size = New System.Drawing.Size(244, 19)
        Me.lblDivisionName.TabIndex = 224
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDivision.Location = New System.Drawing.Point(10, 132)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(46, 18)
        Me.lblDivision.TabIndex = 223
        Me.lblDivision.Text = "Division"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(364, 109)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(244, 19)
        Me.lblLocationName.TabIndex = 221
        '
        'lbLocationCode
        '
        Me.lbLocationCode.FieldName = Nothing
        Me.lbLocationCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lbLocationCode.Location = New System.Drawing.Point(10, 110)
        Me.lbLocationCode.Name = "lbLocationCode"
        Me.lbLocationCode.Size = New System.Drawing.Size(49, 18)
        Me.lbLocationCode.TabIndex = 220
        Me.lbLocationCode.Text = "Location"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(938, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 219
        '
        'lblSalaryStructCode
        '
        Me.lblSalaryStructCode.AutoSize = False
        Me.lblSalaryStructCode.BorderVisible = True
        Me.lblSalaryStructCode.FieldName = Nothing
        Me.lblSalaryStructCode.Location = New System.Drawing.Point(103, 85)
        Me.lblSalaryStructCode.Name = "lblSalaryStructCode"
        Me.lblSalaryStructCode.Size = New System.Drawing.Size(260, 19)
        Me.lblSalaryStructCode.TabIndex = 218
        '
        'lblIncrementNo
        '
        Me.lblIncrementNo.FieldName = Nothing
        Me.lblIncrementNo.Location = New System.Drawing.Point(9, 12)
        Me.lblIncrementNo.Name = "lblIncrementNo"
        Me.lblIncrementNo.Size = New System.Drawing.Size(86, 18)
        Me.lblIncrementNo.TabIndex = 1
        Me.lblIncrementNo.Text = "Increment Code"
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(394, 12)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 217
        Me.lblDate.Text = "Date"
        '
        'fndIncrementCode
        '
        Me.fndIncrementCode.FieldName = Nothing
        Me.fndIncrementCode.Location = New System.Drawing.Point(100, 9)
        Me.fndIncrementCode.MendatroryField = True
        Me.fndIncrementCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndIncrementCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndIncrementCode.MyLinkLable1 = Nothing
        Me.fndIncrementCode.MyLinkLable2 = Nothing
        Me.fndIncrementCode.MyMaxLength = 30
        Me.fndIncrementCode.MyReadOnly = False
        Me.fndIncrementCode.Name = "fndIncrementCode"
        Me.fndIncrementCode.Size = New System.Drawing.Size(260, 21)
        Me.fndIncrementCode.TabIndex = 0
        Me.fndIncrementCode.Value = ""
        '
        'txtIncrementDate
        '
        Me.txtIncrementDate.CalculationExpression = Nothing
        Me.txtIncrementDate.CustomFormat = "dd/MM/yyyy"
        Me.txtIncrementDate.FieldCode = Nothing
        Me.txtIncrementDate.FieldDesc = Nothing
        Me.txtIncrementDate.FieldMaxLength = 0
        Me.txtIncrementDate.FieldName = Nothing
        Me.txtIncrementDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncrementDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtIncrementDate.isCalculatedField = False
        Me.txtIncrementDate.IsSourceFromTable = False
        Me.txtIncrementDate.IsSourceFromValueList = False
        Me.txtIncrementDate.IsUnique = False
        Me.txtIncrementDate.Location = New System.Drawing.Point(430, 10)
        Me.txtIncrementDate.MendatroryField = False
        Me.txtIncrementDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtIncrementDate.MyLinkLable1 = Nothing
        Me.txtIncrementDate.MyLinkLable2 = Nothing
        Me.txtIncrementDate.Name = "txtIncrementDate"
        Me.txtIncrementDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtIncrementDate.ReferenceFieldDesc = Nothing
        Me.txtIncrementDate.ReferenceFieldName = Nothing
        Me.txtIncrementDate.ReferenceTableName = Nothing
        Me.txtIncrementDate.Size = New System.Drawing.Size(143, 18)
        Me.txtIncrementDate.TabIndex = 216
        Me.txtIncrementDate.TabStop = False
        Me.txtIncrementDate.Text = "03/05/2011"
        Me.txtIncrementDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(362, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 203
        Me.btnNew.Text = " "
        '
        'lblSalaryCode
        '
        Me.lblSalaryCode.AutoSize = False
        Me.lblSalaryCode.BorderVisible = True
        Me.lblSalaryCode.FieldName = Nothing
        Me.lblSalaryCode.Location = New System.Drawing.Point(103, 61)
        Me.lblSalaryCode.Name = "lblSalaryCode"
        Me.lblSalaryCode.Size = New System.Drawing.Size(260, 19)
        Me.lblSalaryCode.TabIndex = 215
        '
        'txtRevisionNo
        '
        Me.txtRevisionNo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRevisionNo.CalculationExpression = Nothing
        Me.txtRevisionNo.DecimalPlaces = 0
        Me.txtRevisionNo.FieldCode = Nothing
        Me.txtRevisionNo.FieldDesc = Nothing
        Me.txtRevisionNo.FieldMaxLength = 0
        Me.txtRevisionNo.FieldName = Nothing
        Me.txtRevisionNo.isCalculatedField = False
        Me.txtRevisionNo.IsSourceFromTable = False
        Me.txtRevisionNo.IsSourceFromValueList = False
        Me.txtRevisionNo.IsUnique = False
        Me.txtRevisionNo.Location = New System.Drawing.Point(443, 60)
        Me.txtRevisionNo.MendatroryField = True
        Me.txtRevisionNo.MyLinkLable1 = Nothing
        Me.txtRevisionNo.MyLinkLable2 = Nothing
        Me.txtRevisionNo.Name = "txtRevisionNo"
        Me.txtRevisionNo.ReadOnly = True
        Me.txtRevisionNo.ReferenceFieldDesc = Nothing
        Me.txtRevisionNo.ReferenceFieldName = Nothing
        Me.txtRevisionNo.ReferenceTableName = Nothing
        Me.txtRevisionNo.Size = New System.Drawing.Size(165, 20)
        Me.txtRevisionNo.TabIndex = 204
        Me.txtRevisionNo.Text = "0"
        Me.txtRevisionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRevisionNo.Value = 0R
        '
        'lblEmpSalaryCode
        '
        Me.lblEmpSalaryCode.FieldName = Nothing
        Me.lblEmpSalaryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpSalaryCode.Location = New System.Drawing.Point(10, 61)
        Me.lblEmpSalaryCode.Name = "lblEmpSalaryCode"
        Me.lblEmpSalaryCode.Size = New System.Drawing.Size(69, 16)
        Me.lblEmpSalaryCode.TabIndex = 214
        Me.lblEmpSalaryCode.Text = "Salary Code"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(364, 61)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel1.TabIndex = 205
        Me.MyLabel1.Text = "Revision No"
        '
        'fndEmpCode
        '
        Me.fndEmpCode.CalculationExpression = Nothing
        Me.fndEmpCode.FieldCode = Nothing
        Me.fndEmpCode.FieldDesc = Nothing
        Me.fndEmpCode.FieldMaxLength = 0
        Me.fndEmpCode.FieldName = Nothing
        Me.fndEmpCode.isCalculatedField = False
        Me.fndEmpCode.IsSourceFromTable = False
        Me.fndEmpCode.IsSourceFromValueList = False
        Me.fndEmpCode.IsUnique = False
        Me.fndEmpCode.Location = New System.Drawing.Point(103, 36)
        Me.fndEmpCode.MendatroryField = True
        Me.fndEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmpCode.MyLinkLable1 = Nothing
        Me.fndEmpCode.MyLinkLable2 = Nothing
        Me.fndEmpCode.MyReadOnly = False
        Me.fndEmpCode.MyShowMasterFormButton = False
        Me.fndEmpCode.Name = "fndEmpCode"
        Me.fndEmpCode.ReferenceFieldDesc = Nothing
        Me.fndEmpCode.ReferenceFieldName = Nothing
        Me.fndEmpCode.ReferenceTableName = Nothing
        Me.fndEmpCode.Size = New System.Drawing.Size(260, 19)
        Me.fndEmpCode.TabIndex = 206
        Me.fndEmpCode.Value = ""
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
        Me.dtpApplicableFrom.Location = New System.Drawing.Point(105, 155)
        Me.dtpApplicableFrom.MendatroryField = False
        Me.dtpApplicableFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.MyLinkLable1 = Nothing
        Me.dtpApplicableFrom.MyLinkLable2 = Nothing
        Me.dtpApplicableFrom.Name = "dtpApplicableFrom"
        Me.dtpApplicableFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.ReferenceFieldDesc = Nothing
        Me.dtpApplicableFrom.ReferenceFieldName = Nothing
        Me.dtpApplicableFrom.ReferenceTableName = Nothing
        Me.dtpApplicableFrom.Size = New System.Drawing.Size(143, 18)
        Me.dtpApplicableFrom.TabIndex = 212
        Me.dtpApplicableFrom.TabStop = False
        Me.dtpApplicableFrom.Text = "03/05/2011"
        Me.dtpApplicableFrom.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblempcode.Location = New System.Drawing.Point(10, 36)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(87, 16)
        Me.lblempcode.TabIndex = 207
        Me.lblempcode.Text = "Employee Code"
        '
        'lblSalStructName
        '
        Me.lblSalStructName.AutoSize = False
        Me.lblSalStructName.BorderVisible = True
        Me.lblSalStructName.FieldName = Nothing
        Me.lblSalStructName.Location = New System.Drawing.Point(364, 85)
        Me.lblSalStructName.Name = "lblSalStructName"
        Me.lblSalStructName.Size = New System.Drawing.Size(244, 19)
        Me.lblSalStructName.TabIndex = 210
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(364, 36)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(244, 19)
        Me.lblEmpName.TabIndex = 208
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(10, 86)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(85, 18)
        Me.lblLocation.TabIndex = 209
        Me.lblLocation.Text = "Salary Structure"
        '
        'gvSalary
        '
        Me.gvSalary.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvSalary.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSalary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSalary.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvSalary.ForeColor = System.Drawing.Color.Black
        Me.gvSalary.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSalary.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvSalary.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvSalary.MasterTemplate.AllowAddNewRow = False
        Me.gvSalary.MasterTemplate.AllowDeleteRow = False
        Me.gvSalary.MasterTemplate.AutoGenerateColumns = False
        Me.gvSalary.MasterTemplate.EnableGrouping = False
        Me.gvSalary.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvSalary.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSalary.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvSalary.MyStopExport = False
        Me.gvSalary.Name = "gvSalary"
        Me.gvSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSalary.ShowHeaderCellButtons = True
        Me.gvSalary.Size = New System.Drawing.Size(1150, 217)
        Me.gvSalary.TabIndex = 5
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(219, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(75, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1070, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 18)
        Me.btndelete.TabIndex = 6
        Me.btndelete.Text = "Delete"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(1150, 20)
        Me.RadMenu2.TabIndex = 61
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport, Me.rmClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'rmImport
        '
        Me.rmImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuImportWithIncrementAmt, Me.mnuImportWithFinalSalary})
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'mnuImportWithIncrementAmt
        '
        Me.mnuImportWithIncrementAmt.AccessibleName = "mnuImportWithIncrementAmt"
        Me.mnuImportWithIncrementAmt.Name = "mnuImportWithIncrementAmt"
        Me.mnuImportWithIncrementAmt.Text = "With Increment Amount"
        '
        'mnuImportWithFinalSalary
        '
        Me.mnuImportWithFinalSalary.AccessibleName = "mnuImportWithFinalSalary"
        Me.mnuImportWithFinalSalary.Name = "mnuImportWithFinalSalary"
        Me.mnuImportWithFinalSalary.Text = "With Final Salary"
        '
        'rmExport
        '
        Me.rmExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuExportWithIncrementAmt, Me.mnuExportWithNewSalary})
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'mnuExportWithIncrementAmt
        '
        Me.mnuExportWithIncrementAmt.AccessibleName = "mnuExportWithIncrementAmt"
        Me.mnuExportWithIncrementAmt.Name = "mnuExportWithIncrementAmt"
        Me.mnuExportWithIncrementAmt.Text = "With Increment Amount"
        '
        'mnuExportWithNewSalary
        '
        Me.mnuExportWithNewSalary.AccessibleName = "mnuExportWithNewSalary"
        Me.mnuExportWithNewSalary.Name = "mnuExportWithNewSalary"
        Me.mnuExportWithNewSalary.Text = "With New Salary"
        '
        'rmClose
        '
        Me.rmClose.Name = "rmClose"
        Me.rmClose.Text = "Close"
        '
        'FrmEmpIncrement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1150, 459)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "FrmEmpIncrement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmEmpIncrement"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNewSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalInc1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNewSalaryTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalSalary1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpArrearFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalaryStructCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncrementNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIncrementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalaryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRevisionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpSalaryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalStructName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSalary.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndIncrementCode As common.UserControls.txtNavigator
    Friend WithEvents lblIncrementNo As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtRevisionNo As common.MyNumBox
    Friend WithEvents fndEmpCode As common.UserControls.txtFinder
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblSalStructName As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpApplicableFrom As common.Controls.MyDateTimePicker
    Friend WithEvents lblSalaryCode As common.Controls.MyLabel
    Friend WithEvents lblEmpSalaryCode As common.Controls.MyLabel
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtIncrementDate As common.Controls.MyDateTimePicker
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblSalaryStructCode As common.Controls.MyLabel
    Friend WithEvents gvSalary As common.UserControls.MyRadGridView
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblDivisionCode As common.Controls.MyLabel
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lbLocationCode As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents dtpArrearFrom As common.Controls.MyDateTimePicker
    Friend WithEvents mnuExportWithIncrementAmt As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExportWithNewSalary As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuImportWithIncrementAmt As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuImportWithFinalSalary As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblTotalInc1 As common.Controls.MyLabel
    Friend WithEvents lblIncTotal As common.Controls.MyLabel
    Friend WithEvents lblTotalSalary1 As common.Controls.MyLabel
    Friend WithEvents lblTotalSalary As common.Controls.MyLabel
    Friend WithEvents lblNewSalary As common.Controls.MyLabel
    Friend WithEvents lblNewSalaryTotal As common.Controls.MyLabel
    Friend WithEvents LocationCode As common.UserControls.txtFinder
End Class

