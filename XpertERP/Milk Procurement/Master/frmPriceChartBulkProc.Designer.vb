<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPriceChartBulkProc
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
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.RmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImportDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.RmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.pnlTotalSolid = New System.Windows.Forms.Panel()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtTotalSolidRate = New common.MyNumBox()
        Me.chkPriceGradeWise = New common.Controls.MyCheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkPriceItemWise = New common.Controls.MyCheckBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.dtpExpiryDate = New common.Controls.MyDateTimePicker()
        Me.lblMilkType = New common.Controls.MyLabel()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.dtpEffectiveDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtMilktypeCode = New common.UserControls.txtFinder()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.chkDefaultForTankerDispatch = New common.Controls.MyCheckBox()
        Me.txtPricedate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndcode = New common.UserControls.txtNavigator()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.TxtFatWeightage = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtfatPercentage = New common.MyNumBox()
        Me.fndVendor = New common.UserControls.txtFinder()
        Me.lblVendorBulk = New common.Controls.MyLabel()
        Me.TxtSNFWeightage = New common.MyNumBox()
        Me.txtStanadardrate = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtsnfPercentage = New common.MyNumBox()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.txtTolerance = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvPriceChart = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.pnlUOM = New System.Windows.Forms.Panel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.pnlTotalSolid.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalSolidRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPriceGradeWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.chkPriceItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEffectiveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultForTankerDispatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPricedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfatPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsnfPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvPriceChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPriceChart.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUOM.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RmImport, Me.RmExport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'RmImport
        '
        Me.RmImport.AccessibleDescription = "Import"
        Me.RmImport.AccessibleName = "Import"
        Me.RmImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnImportDetail})
        Me.RmImport.Name = "RmImport"
        Me.RmImport.Text = "Import"
        '
        'btnImportDetail
        '
        Me.btnImportDetail.AccessibleDescription = "Import Detail"
        Me.btnImportDetail.AccessibleName = "Import Detail"
        Me.btnImportDetail.Name = "btnImportDetail"
        Me.btnImportDetail.Text = "Import Detail"
        '
        'RmExport
        '
        Me.RmExport.AccessibleDescription = "Export"
        Me.RmExport.AccessibleName = "Export"
        Me.RmExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExportDetail})
        Me.RmExport.Name = "RmExport"
        Me.RmExport.Text = "Export"
        '
        'btnExportDetail
        '
        Me.btnExportDetail.AccessibleDescription = "Export Detail"
        Me.btnExportDetail.AccessibleName = "Export Detail"
        Me.btnExportDetail.Name = "btnExportDetail"
        Me.btnExportDetail.Text = "Export Detail"
        Me.btnExportDetail.Visibility = Telerik.WinControls.ElementVisibility.Hidden
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1073, 362)
        Me.SplitContainer1.SplitterDistance = 324
        Me.SplitContainer1.TabIndex = 13
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1073, 324)
        Me.SplitContainer2.SplitterDistance = 175
        Me.SplitContainer2.TabIndex = 278
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.pnlUOM)
        Me.SplitContainer3.Panel1.Controls.Add(Me.pnlTotalSolid)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkPriceGradeWise)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblvandorno)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkDefaultForTankerDispatch)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtPricedate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndcode)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer3.Panel2.Controls.Add(Me.TxtFatWeightage)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel6)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel12)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtfatPercentage)
        Me.SplitContainer3.Panel2.Controls.Add(Me.fndVendor)
        Me.SplitContainer3.Panel2.Controls.Add(Me.TxtSNFWeightage)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtStanadardrate)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel5)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtsnfPercentage)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblVendorBulk)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblVendorName)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtTolerance)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel4)
        Me.SplitContainer3.Size = New System.Drawing.Size(1073, 175)
        Me.SplitContainer3.SplitterDistance = 76
        Me.SplitContainer3.TabIndex = 0
        '
        'pnlTotalSolid
        '
        Me.pnlTotalSolid.Controls.Add(Me.MyLabel9)
        Me.pnlTotalSolid.Controls.Add(Me.txtTotalSolidRate)
        Me.pnlTotalSolid.Location = New System.Drawing.Point(3, 51)
        Me.pnlTotalSolid.Name = "pnlTotalSolid"
        Me.pnlTotalSolid.Size = New System.Drawing.Size(224, 22)
        Me.pnlTotalSolid.TabIndex = 277
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
        Me.txtUOM.Location = New System.Drawing.Point(97, 2)
        Me.txtUOM.MendatroryField = True
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Me.MyLabel13
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(125, 19)
        Me.txtUOM.TabIndex = 292
        Me.txtUOM.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(4, 3)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel13.TabIndex = 293
        Me.MyLabel13.Text = "Calculation UOM"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(4, 2)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel9.TabIndex = 287
        Me.MyLabel9.Text = "Total Solid Rate"
        '
        'txtTotalSolidRate
        '
        Me.txtTotalSolidRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotalSolidRate.CalculationExpression = Nothing
        Me.txtTotalSolidRate.DecimalPlaces = 2
        Me.txtTotalSolidRate.FieldCode = Nothing
        Me.txtTotalSolidRate.FieldDesc = Nothing
        Me.txtTotalSolidRate.FieldMaxLength = 0
        Me.txtTotalSolidRate.FieldName = Nothing
        Me.txtTotalSolidRate.isCalculatedField = False
        Me.txtTotalSolidRate.IsSourceFromTable = False
        Me.txtTotalSolidRate.IsSourceFromValueList = False
        Me.txtTotalSolidRate.IsUnique = False
        Me.txtTotalSolidRate.Location = New System.Drawing.Point(96, 0)
        Me.txtTotalSolidRate.MendatroryField = True
        Me.txtTotalSolidRate.MyLinkLable1 = Nothing
        Me.txtTotalSolidRate.MyLinkLable2 = Nothing
        Me.txtTotalSolidRate.Name = "txtTotalSolidRate"
        Me.txtTotalSolidRate.ReferenceFieldDesc = Nothing
        Me.txtTotalSolidRate.ReferenceFieldName = Nothing
        Me.txtTotalSolidRate.ReferenceTableName = Nothing
        Me.txtTotalSolidRate.Size = New System.Drawing.Size(125, 20)
        Me.txtTotalSolidRate.TabIndex = 286
        Me.txtTotalSolidRate.Text = "0"
        Me.txtTotalSolidRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalSolidRate.Value = 0.0R
        '
        'chkPriceGradeWise
        '
        Me.chkPriceGradeWise.Location = New System.Drawing.Point(667, 7)
        Me.chkPriceGradeWise.MyLinkLable1 = Nothing
        Me.chkPriceGradeWise.MyLinkLable2 = Nothing
        Me.chkPriceGradeWise.Name = "chkPriceGradeWise"
        Me.chkPriceGradeWise.Size = New System.Drawing.Size(116, 18)
        Me.chkPriceGradeWise.TabIndex = 285
        Me.chkPriceGradeWise.Tag1 = Nothing
        Me.chkPriceGradeWise.Text = "Is Price Grade Wise"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkPriceItemWise)
        Me.Panel1.Controls.Add(Me.MyLabel8)
        Me.Panel1.Controls.Add(Me.dtpExpiryDate)
        Me.Panel1.Controls.Add(Me.lblMilkType)
        Me.Panel1.Controls.Add(Me.lblSRNDate)
        Me.Panel1.Controls.Add(Me.dtpEffectiveDate)
        Me.Panel1.Controls.Add(Me.MyLabel7)
        Me.Panel1.Controls.Add(Me.txtMilktypeCode)
        Me.Panel1.Location = New System.Drawing.Point(425, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(648, 41)
        Me.Panel1.TabIndex = 278
        Me.Panel1.Visible = False
        '
        'chkPriceItemWise
        '
        Me.chkPriceItemWise.Location = New System.Drawing.Point(521, 2)
        Me.chkPriceItemWise.MyLinkLable1 = Nothing
        Me.chkPriceItemWise.MyLinkLable2 = Nothing
        Me.chkPriceItemWise.Name = "chkPriceItemWise"
        Me.chkPriceItemWise.Size = New System.Drawing.Size(108, 18)
        Me.chkPriceItemWise.TabIndex = 287
        Me.chkPriceItemWise.Tag1 = Nothing
        Me.chkPriceItemWise.Text = "Is Price Item Wise"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(364, 4)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel8.TabIndex = 285
        Me.MyLabel8.Text = "Expiry Date"
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.CalculationExpression = Nothing
        Me.dtpExpiryDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpExpiryDate.Enabled = False
        Me.dtpExpiryDate.FieldCode = Nothing
        Me.dtpExpiryDate.FieldDesc = Nothing
        Me.dtpExpiryDate.FieldMaxLength = 0
        Me.dtpExpiryDate.FieldName = Nothing
        Me.dtpExpiryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.isCalculatedField = False
        Me.dtpExpiryDate.IsSourceFromTable = False
        Me.dtpExpiryDate.IsSourceFromValueList = False
        Me.dtpExpiryDate.IsUnique = False
        Me.dtpExpiryDate.Location = New System.Drawing.Point(437, 2)
        Me.dtpExpiryDate.MendatroryField = True
        Me.dtpExpiryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpExpiryDate.MyLinkLable1 = Me.MyLabel8
        Me.dtpExpiryDate.MyLinkLable2 = Nothing
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpExpiryDate.ReadOnly = True
        Me.dtpExpiryDate.ReferenceFieldDesc = Nothing
        Me.dtpExpiryDate.ReferenceFieldName = Nothing
        Me.dtpExpiryDate.ReferenceTableName = Nothing
        Me.dtpExpiryDate.Size = New System.Drawing.Size(78, 18)
        Me.dtpExpiryDate.TabIndex = 286
        Me.dtpExpiryDate.TabStop = False
        Me.dtpExpiryDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpExpiryDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblMilkType
        '
        Me.lblMilkType.AutoSize = False
        Me.lblMilkType.BorderVisible = True
        Me.lblMilkType.FieldName = Nothing
        Me.lblMilkType.Location = New System.Drawing.Point(242, 21)
        Me.lblMilkType.Name = "lblMilkType"
        Me.lblMilkType.Size = New System.Drawing.Size(81, 19)
        Me.lblMilkType.TabIndex = 284
        Me.lblMilkType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(3, 6)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(77, 16)
        Me.lblSRNDate.TabIndex = 0
        Me.lblSRNDate.Text = "Effective Date"
        '
        'dtpEffectiveDate
        '
        Me.dtpEffectiveDate.CalculationExpression = Nothing
        Me.dtpEffectiveDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpEffectiveDate.FieldCode = Nothing
        Me.dtpEffectiveDate.FieldDesc = Nothing
        Me.dtpEffectiveDate.FieldMaxLength = 0
        Me.dtpEffectiveDate.FieldName = Nothing
        Me.dtpEffectiveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEffectiveDate.isCalculatedField = False
        Me.dtpEffectiveDate.IsSourceFromTable = False
        Me.dtpEffectiveDate.IsSourceFromValueList = False
        Me.dtpEffectiveDate.IsUnique = False
        Me.dtpEffectiveDate.Location = New System.Drawing.Point(90, 3)
        Me.dtpEffectiveDate.MendatroryField = True
        Me.dtpEffectiveDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEffectiveDate.MyLinkLable1 = Me.lblSRNDate
        Me.dtpEffectiveDate.MyLinkLable2 = Nothing
        Me.dtpEffectiveDate.Name = "dtpEffectiveDate"
        Me.dtpEffectiveDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEffectiveDate.ReferenceFieldDesc = Nothing
        Me.dtpEffectiveDate.ReferenceFieldName = Nothing
        Me.dtpEffectiveDate.ReferenceTableName = Nothing
        Me.dtpEffectiveDate.Size = New System.Drawing.Size(151, 18)
        Me.dtpEffectiveDate.TabIndex = 0
        Me.dtpEffectiveDate.TabStop = False
        Me.dtpEffectiveDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpEffectiveDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(3, 22)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel7.TabIndex = 283
        Me.MyLabel7.Text = "Milk Type Code"
        '
        'txtMilktypeCode
        '
        Me.txtMilktypeCode.CalculationExpression = Nothing
        Me.txtMilktypeCode.FieldCode = Nothing
        Me.txtMilktypeCode.FieldDesc = Nothing
        Me.txtMilktypeCode.FieldMaxLength = 0
        Me.txtMilktypeCode.FieldName = Nothing
        Me.txtMilktypeCode.isCalculatedField = False
        Me.txtMilktypeCode.IsSourceFromTable = False
        Me.txtMilktypeCode.IsSourceFromValueList = False
        Me.txtMilktypeCode.IsUnique = False
        Me.txtMilktypeCode.Location = New System.Drawing.Point(90, 21)
        Me.txtMilktypeCode.MendatroryField = True
        Me.txtMilktypeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMilktypeCode.MyLinkLable1 = Me.MyLabel7
        Me.txtMilktypeCode.MyLinkLable2 = Nothing
        Me.txtMilktypeCode.MyReadOnly = False
        Me.txtMilktypeCode.MyShowMasterFormButton = False
        Me.txtMilktypeCode.Name = "txtMilktypeCode"
        Me.txtMilktypeCode.ReferenceFieldDesc = Nothing
        Me.txtMilktypeCode.ReferenceFieldName = Nothing
        Me.txtMilktypeCode.ReferenceTableName = Nothing
        Me.txtMilktypeCode.Size = New System.Drawing.Size(151, 17)
        Me.txtMilktypeCode.TabIndex = 3
        Me.txtMilktypeCode.Value = ""
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(7, 9)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(62, 16)
        Me.lblvandorno.TabIndex = 14
        Me.lblvandorno.Text = "Price Code"
        '
        'chkDefaultForTankerDispatch
        '
        Me.chkDefaultForTankerDispatch.Location = New System.Drawing.Point(225, 32)
        Me.chkDefaultForTankerDispatch.MyLinkLable1 = Nothing
        Me.chkDefaultForTankerDispatch.MyLinkLable2 = Nothing
        Me.chkDefaultForTankerDispatch.Name = "chkDefaultForTankerDispatch"
        Me.chkDefaultForTankerDispatch.Size = New System.Drawing.Size(159, 18)
        Me.chkDefaultForTankerDispatch.TabIndex = 277
        Me.chkDefaultForTankerDispatch.Tag1 = Nothing
        Me.chkDefaultForTankerDispatch.Text = "Default For Tanker Dispatch"
        '
        'txtPricedate
        '
        Me.txtPricedate.CalculationExpression = Nothing
        Me.txtPricedate.CustomFormat = "dd/MM/yyyy"
        Me.txtPricedate.FieldCode = Nothing
        Me.txtPricedate.FieldDesc = Nothing
        Me.txtPricedate.FieldMaxLength = 0
        Me.txtPricedate.FieldName = Nothing
        Me.txtPricedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPricedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPricedate.isCalculatedField = False
        Me.txtPricedate.IsSourceFromTable = False
        Me.txtPricedate.IsSourceFromValueList = False
        Me.txtPricedate.IsUnique = False
        Me.txtPricedate.Location = New System.Drawing.Point(99, 31)
        Me.txtPricedate.MendatroryField = True
        Me.txtPricedate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPricedate.MyLinkLable1 = Me.MyLabel1
        Me.txtPricedate.MyLinkLable2 = Nothing
        Me.txtPricedate.Name = "txtPricedate"
        Me.txtPricedate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPricedate.ReferenceFieldDesc = Nothing
        Me.txtPricedate.ReferenceFieldName = Nothing
        Me.txtPricedate.ReferenceTableName = Nothing
        Me.txtPricedate.Size = New System.Drawing.Size(125, 18)
        Me.txtPricedate.TabIndex = 1
        Me.txtPricedate.TabStop = False
        Me.txtPricedate.Text = "13/06/2011"
        Me.txtPricedate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 33)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel1.TabIndex = 17
        Me.MyLabel1.Text = "Price Date"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(399, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(18, 21)
        Me.btnnew.TabIndex = 1
        '
        'fndcode
        '
        Me.fndcode.FieldName = Nothing
        Me.fndcode.Location = New System.Drawing.Point(99, 7)
        Me.fndcode.MendatroryField = True
        Me.fndcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcode.MyLinkLable1 = Me.lblvandorno
        Me.fndcode.MyLinkLable2 = Nothing
        Me.fndcode.MyMaxLength = 32767
        Me.fndcode.MyReadOnly = False
        Me.fndcode.Name = "fndcode"
        Me.fndcode.Size = New System.Drawing.Size(300, 21)
        Me.fndcode.TabIndex = 0
        Me.fndcode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(7, 5)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel3.TabIndex = 37
        Me.MyLabel3.Text = "FAT Weightage"
        '
        'TxtFatWeightage
        '
        Me.TxtFatWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtFatWeightage.CalculationExpression = Nothing
        Me.TxtFatWeightage.DecimalPlaces = 2
        Me.TxtFatWeightage.FieldCode = Nothing
        Me.TxtFatWeightage.FieldDesc = Nothing
        Me.TxtFatWeightage.FieldMaxLength = 0
        Me.TxtFatWeightage.FieldName = Nothing
        Me.TxtFatWeightage.isCalculatedField = False
        Me.TxtFatWeightage.IsSourceFromTable = False
        Me.TxtFatWeightage.IsSourceFromValueList = False
        Me.TxtFatWeightage.IsUnique = False
        Me.TxtFatWeightage.Location = New System.Drawing.Point(97, 3)
        Me.TxtFatWeightage.MendatroryField = True
        Me.TxtFatWeightage.MyLinkLable1 = Nothing
        Me.TxtFatWeightage.MyLinkLable2 = Nothing
        Me.TxtFatWeightage.Name = "TxtFatWeightage"
        Me.TxtFatWeightage.ReferenceFieldDesc = Nothing
        Me.TxtFatWeightage.ReferenceFieldName = Nothing
        Me.TxtFatWeightage.ReferenceTableName = Nothing
        Me.TxtFatWeightage.Size = New System.Drawing.Size(125, 20)
        Me.TxtFatWeightage.TabIndex = 0
        Me.TxtFatWeightage.Text = "0"
        Me.TxtFatWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFatWeightage.Value = 0.0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(7, 49)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel6.TabIndex = 40
        Me.MyLabel6.Text = "Standard Rate"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(223, 5)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel12.TabIndex = 41
        Me.MyLabel12.Text = "SNF Weightage"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(226, 49)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel2.TabIndex = 276
        Me.MyLabel2.Text = "Tolerance (%)"
        '
        'txtfatPercentage
        '
        Me.txtfatPercentage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtfatPercentage.CalculationExpression = Nothing
        Me.txtfatPercentage.DecimalPlaces = 2
        Me.txtfatPercentage.FieldCode = Nothing
        Me.txtfatPercentage.FieldDesc = Nothing
        Me.txtfatPercentage.FieldMaxLength = 0
        Me.txtfatPercentage.FieldName = Nothing
        Me.txtfatPercentage.isCalculatedField = False
        Me.txtfatPercentage.IsSourceFromTable = False
        Me.txtfatPercentage.IsSourceFromValueList = False
        Me.txtfatPercentage.IsUnique = False
        Me.txtfatPercentage.Location = New System.Drawing.Point(97, 25)
        Me.txtfatPercentage.MendatroryField = True
        Me.txtfatPercentage.MyLinkLable1 = Nothing
        Me.txtfatPercentage.MyLinkLable2 = Nothing
        Me.txtfatPercentage.Name = "txtfatPercentage"
        Me.txtfatPercentage.ReferenceFieldDesc = Nothing
        Me.txtfatPercentage.ReferenceFieldName = Nothing
        Me.txtfatPercentage.ReferenceTableName = Nothing
        Me.txtfatPercentage.Size = New System.Drawing.Size(125, 20)
        Me.txtfatPercentage.TabIndex = 2
        Me.txtfatPercentage.Text = "0"
        Me.txtfatPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtfatPercentage.Value = 0.0R
        '
        'fndVendor
        '
        Me.fndVendor.CalculationExpression = Nothing
        Me.fndVendor.FieldCode = Nothing
        Me.fndVendor.FieldDesc = Nothing
        Me.fndVendor.FieldMaxLength = 0
        Me.fndVendor.FieldName = Nothing
        Me.fndVendor.isCalculatedField = False
        Me.fndVendor.IsSourceFromTable = False
        Me.fndVendor.IsSourceFromValueList = False
        Me.fndVendor.IsUnique = False
        Me.fndVendor.Location = New System.Drawing.Point(97, 69)
        Me.fndVendor.MendatroryField = False
        Me.fndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor.MyLinkLable1 = Me.lblVendorBulk
        Me.fndVendor.MyLinkLable2 = Nothing
        Me.fndVendor.MyReadOnly = False
        Me.fndVendor.MyShowMasterFormButton = False
        Me.fndVendor.Name = "fndVendor"
        Me.fndVendor.ReferenceFieldDesc = Nothing
        Me.fndVendor.ReferenceFieldName = Nothing
        Me.fndVendor.ReferenceTableName = Nothing
        Me.fndVendor.Size = New System.Drawing.Size(125, 19)
        Me.fndVendor.TabIndex = 6
        Me.fndVendor.Value = ""
        Me.fndVendor.Visible = False
        '
        'lblVendorBulk
        '
        Me.lblVendorBulk.FieldName = Nothing
        Me.lblVendorBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorBulk.Location = New System.Drawing.Point(7, 70)
        Me.lblVendorBulk.Name = "lblVendorBulk"
        Me.lblVendorBulk.Size = New System.Drawing.Size(43, 16)
        Me.lblVendorBulk.TabIndex = 273
        Me.lblVendorBulk.Text = "Vendor"
        Me.lblVendorBulk.Visible = False
        '
        'TxtSNFWeightage
        '
        Me.TxtSNFWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtSNFWeightage.CalculationExpression = Nothing
        Me.TxtSNFWeightage.DecimalPlaces = 2
        Me.TxtSNFWeightage.FieldCode = Nothing
        Me.TxtSNFWeightage.FieldDesc = Nothing
        Me.TxtSNFWeightage.FieldMaxLength = 0
        Me.TxtSNFWeightage.FieldName = Nothing
        Me.TxtSNFWeightage.isCalculatedField = False
        Me.TxtSNFWeightage.IsSourceFromTable = False
        Me.TxtSNFWeightage.IsSourceFromValueList = False
        Me.TxtSNFWeightage.IsUnique = False
        Me.TxtSNFWeightage.Location = New System.Drawing.Point(318, 3)
        Me.TxtSNFWeightage.MendatroryField = True
        Me.TxtSNFWeightage.MyLinkLable1 = Nothing
        Me.TxtSNFWeightage.MyLinkLable2 = Nothing
        Me.TxtSNFWeightage.Name = "TxtSNFWeightage"
        Me.TxtSNFWeightage.ReferenceFieldDesc = Nothing
        Me.TxtSNFWeightage.ReferenceFieldName = Nothing
        Me.TxtSNFWeightage.ReferenceTableName = Nothing
        Me.TxtSNFWeightage.Size = New System.Drawing.Size(125, 20)
        Me.TxtSNFWeightage.TabIndex = 1
        Me.TxtSNFWeightage.Text = "0"
        Me.TxtSNFWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSNFWeightage.Value = 0.0R
        '
        'txtStanadardrate
        '
        Me.txtStanadardrate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStanadardrate.CalculationExpression = Nothing
        Me.txtStanadardrate.DecimalPlaces = 2
        Me.txtStanadardrate.FieldCode = Nothing
        Me.txtStanadardrate.FieldDesc = Nothing
        Me.txtStanadardrate.FieldMaxLength = 0
        Me.txtStanadardrate.FieldName = Nothing
        Me.txtStanadardrate.isCalculatedField = False
        Me.txtStanadardrate.IsSourceFromTable = False
        Me.txtStanadardrate.IsSourceFromValueList = False
        Me.txtStanadardrate.IsUnique = False
        Me.txtStanadardrate.Location = New System.Drawing.Point(97, 47)
        Me.txtStanadardrate.MendatroryField = True
        Me.txtStanadardrate.MyLinkLable1 = Nothing
        Me.txtStanadardrate.MyLinkLable2 = Nothing
        Me.txtStanadardrate.Name = "txtStanadardrate"
        Me.txtStanadardrate.ReferenceFieldDesc = Nothing
        Me.txtStanadardrate.ReferenceFieldName = Nothing
        Me.txtStanadardrate.ReferenceTableName = Nothing
        Me.txtStanadardrate.Size = New System.Drawing.Size(125, 20)
        Me.txtStanadardrate.TabIndex = 4
        Me.txtStanadardrate.Text = "0"
        Me.txtStanadardrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStanadardrate.Value = 0.0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(7, 27)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel5.TabIndex = 39
        Me.MyLabel5.Text = "FAT Ratio"
        '
        'txtsnfPercentage
        '
        Me.txtsnfPercentage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtsnfPercentage.CalculationExpression = Nothing
        Me.txtsnfPercentage.DecimalPlaces = 2
        Me.txtsnfPercentage.FieldCode = Nothing
        Me.txtsnfPercentage.FieldDesc = Nothing
        Me.txtsnfPercentage.FieldMaxLength = 0
        Me.txtsnfPercentage.FieldName = Nothing
        Me.txtsnfPercentage.isCalculatedField = False
        Me.txtsnfPercentage.IsSourceFromTable = False
        Me.txtsnfPercentage.IsSourceFromValueList = False
        Me.txtsnfPercentage.IsUnique = False
        Me.txtsnfPercentage.Location = New System.Drawing.Point(318, 25)
        Me.txtsnfPercentage.MendatroryField = True
        Me.txtsnfPercentage.MyLinkLable1 = Nothing
        Me.txtsnfPercentage.MyLinkLable2 = Nothing
        Me.txtsnfPercentage.Name = "txtsnfPercentage"
        Me.txtsnfPercentage.ReferenceFieldDesc = Nothing
        Me.txtsnfPercentage.ReferenceFieldName = Nothing
        Me.txtsnfPercentage.ReferenceTableName = Nothing
        Me.txtsnfPercentage.Size = New System.Drawing.Size(125, 20)
        Me.txtsnfPercentage.TabIndex = 3
        Me.txtsnfPercentage.Text = "0"
        Me.txtsnfPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtsnfPercentage.Value = 0.0R
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(226, 69)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(219, 19)
        Me.lblVendorName.TabIndex = 274
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.Visible = False
        '
        'txtTolerance
        '
        Me.txtTolerance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTolerance.CalculationExpression = Nothing
        Me.txtTolerance.DecimalPlaces = 2
        Me.txtTolerance.FieldCode = Nothing
        Me.txtTolerance.FieldDesc = Nothing
        Me.txtTolerance.FieldMaxLength = 0
        Me.txtTolerance.FieldName = Nothing
        Me.txtTolerance.isCalculatedField = False
        Me.txtTolerance.IsSourceFromTable = False
        Me.txtTolerance.IsSourceFromValueList = False
        Me.txtTolerance.IsUnique = False
        Me.txtTolerance.Location = New System.Drawing.Point(318, 47)
        Me.txtTolerance.MendatroryField = True
        Me.txtTolerance.MyLinkLable1 = Nothing
        Me.txtTolerance.MyLinkLable2 = Nothing
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.ReferenceFieldDesc = Nothing
        Me.txtTolerance.ReferenceFieldName = Nothing
        Me.txtTolerance.ReferenceTableName = Nothing
        Me.txtTolerance.Size = New System.Drawing.Size(125, 20)
        Me.txtTolerance.TabIndex = 5
        Me.txtTolerance.Text = "0"
        Me.txtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTolerance.Value = 0.0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(226, 27)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel4.TabIndex = 38
        Me.MyLabel4.Text = "SNF Ratio"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvPriceChart)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Price Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1073, 145)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Price Details"
        Me.RadGroupBox1.Visible = False
        '
        'gvPriceChart
        '
        Me.gvPriceChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPriceChart.Location = New System.Drawing.Point(2, 18)
        '
        'gvPriceChart
        '
        Me.gvPriceChart.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPriceChart.Name = "gvPriceChart"
        Me.gvPriceChart.ShowHeaderCellButtons = True
        Me.gvPriceChart.Size = New System.Drawing.Size(1069, 125)
        Me.gvPriceChart.TabIndex = 264
        Me.gvPriceChart.Text = "RadGridView1"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(246, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 20)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(167, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(996, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "&Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(88, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "&Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "&Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1073, 20)
        Me.RadMenu1.TabIndex = 14
        Me.RadMenu1.Text = "RadMenu1"
        '
        'pnlUOM
        '
        Me.pnlUOM.Controls.Add(Me.txtUOM)
        Me.pnlUOM.Controls.Add(Me.MyLabel13)
        Me.pnlUOM.Location = New System.Drawing.Point(223, 51)
        Me.pnlUOM.Name = "pnlUOM"
        Me.pnlUOM.Size = New System.Drawing.Size(223, 22)
        Me.pnlUOM.TabIndex = 286
        '
        'frmPriceChartBulkProc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1073, 382)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmPriceChartBulkProc"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPriceChartBulkProc"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        Me.pnlTotalSolid.ResumeLayout(False)
        Me.pnlTotalSolid.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalSolidRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPriceGradeWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkPriceItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEffectiveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultForTankerDispatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPricedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfatPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsnfPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvPriceChart.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPriceChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUOM.ResumeLayout(False)
        Me.pnlUOM.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndcode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtPricedate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkDefaultForTankerDispatch As common.Controls.MyCheckBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtfatPercentage As common.MyNumBox
    Friend WithEvents txtTolerance As common.MyNumBox
    Friend WithEvents txtsnfPercentage As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndVendor As common.UserControls.txtFinder
    Friend WithEvents lblVendorBulk As common.Controls.MyLabel
    Friend WithEvents txtStanadardrate As common.MyNumBox
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtSNFWeightage As common.MyNumBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents TxtFatWeightage As common.MyNumBox
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents dtpEffectiveDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvPriceChart As common.UserControls.MyRadGridView
    Friend WithEvents lblMilkType As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtMilktypeCode As common.UserControls.txtFinder
    Friend WithEvents btnExportDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImportDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkPriceGradeWise As common.Controls.MyCheckBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents dtpExpiryDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkPriceItemWise As common.Controls.MyCheckBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtTotalSolidRate As common.MyNumBox
    Friend WithEvents pnlTotalSolid As System.Windows.Forms.Panel
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents pnlUOM As System.Windows.Forms.Panel
End Class

