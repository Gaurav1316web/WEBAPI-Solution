<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDairyBookingUploader
    Inherits XpertERPEngine.FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtPaymentMode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCardSaleCode = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.LblFromDate = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAgainstGatePass = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAgainstTruckSheet = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAgainstCardIndent = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAgainstCashIndent = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.gvTS = New common.UserControls.MyRadGridView()
        Me.gvTSItem = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnExcept = New Telerik.WinControls.UI.RadButton()
        Me.lblExcept = New common.Controls.MyLabel()
        Me.txtExcept = New common.Controls.MyTextBox()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtBrowse = New common.Controls.MyTextBox()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.gvGP = New common.UserControls.MyRadGridView()
        Me.gvGPItem = New common.UserControls.MyRadGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtFolderBrowse = New common.Controls.MyTextBox()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.btnFolderBrowse = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSaveAndPost = New Telerik.WinControls.UI.RadButton()
        Me.btnExportInvalid = New Telerik.WinControls.UI.RadButton()
        Me.btnValidate = New Telerik.WinControls.UI.RadButton()
        Me.btnExportFormat = New Telerik.WinControls.UI.RadButton()
        Me.btnSelectSheet = New Telerik.WinControls.UI.RadButton()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdbAgainstGatePass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAgainstTruckSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAgainstCardIndent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAgainstCashIndent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.gvTS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTS.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTSItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTSItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnExcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.gvGP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvGP.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvGPItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvGPItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFolderBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnFolderBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportInvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveAndPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportInvalid)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnValidate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportFormat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelectSheet)
        Me.SplitContainer1.Size = New System.Drawing.Size(697, 398)
        Me.SplitContainer1.SplitterDistance = 361
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPaymentMode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCardSaleCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LblFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(697, 361)
        Me.SplitContainer2.SplitterDistance = 139
        Me.SplitContainer2.TabIndex = 3
        '
        'txtPaymentMode
        '
        Me.txtPaymentMode.CalculationExpression = Nothing
        Me.txtPaymentMode.FieldCode = Nothing
        Me.txtPaymentMode.FieldDesc = Nothing
        Me.txtPaymentMode.FieldMaxLength = 0
        Me.txtPaymentMode.FieldName = Nothing
        Me.txtPaymentMode.isCalculatedField = False
        Me.txtPaymentMode.IsSourceFromTable = False
        Me.txtPaymentMode.IsSourceFromValueList = False
        Me.txtPaymentMode.IsUnique = False
        Me.txtPaymentMode.Location = New System.Drawing.Point(458, 102)
        Me.txtPaymentMode.MendatroryField = True
        Me.txtPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentMode.MyLinkLable1 = Me.MyLabel1
        Me.txtPaymentMode.MyLinkLable2 = Nothing
        Me.txtPaymentMode.MyReadOnly = False
        Me.txtPaymentMode.MyShowMasterFormButton = False
        Me.txtPaymentMode.Name = "txtPaymentMode"
        Me.txtPaymentMode.ReferenceFieldDesc = Nothing
        Me.txtPaymentMode.ReferenceFieldName = Nothing
        Me.txtPaymentMode.ReferenceTableName = Nothing
        Me.txtPaymentMode.Size = New System.Drawing.Size(142, 19)
        Me.txtPaymentMode.TabIndex = 1492
        Me.txtPaymentMode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(375, 105)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel1.TabIndex = 1491
        Me.MyLabel1.Text = "Payment Mode"
        '
        'txtCardSaleCode
        '
        Me.txtCardSaleCode.CalculationExpression = Nothing
        Me.txtCardSaleCode.FieldCode = Nothing
        Me.txtCardSaleCode.FieldDesc = Nothing
        Me.txtCardSaleCode.FieldMaxLength = 0
        Me.txtCardSaleCode.FieldName = Nothing
        Me.txtCardSaleCode.isCalculatedField = False
        Me.txtCardSaleCode.IsSourceFromTable = False
        Me.txtCardSaleCode.IsSourceFromValueList = False
        Me.txtCardSaleCode.IsUnique = False
        Me.txtCardSaleCode.Location = New System.Drawing.Point(94, 81)
        Me.txtCardSaleCode.MendatroryField = False
        Me.txtCardSaleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCardSaleCode.MyLinkLable1 = Me.RadLabel15
        Me.txtCardSaleCode.MyLinkLable2 = Nothing
        Me.txtCardSaleCode.MyReadOnly = False
        Me.txtCardSaleCode.MyShowMasterFormButton = False
        Me.txtCardSaleCode.Name = "txtCardSaleCode"
        Me.txtCardSaleCode.ReferenceFieldDesc = Nothing
        Me.txtCardSaleCode.ReferenceFieldName = Nothing
        Me.txtCardSaleCode.ReferenceTableName = Nothing
        Me.txtCardSaleCode.Size = New System.Drawing.Size(370, 19)
        Me.txtCardSaleCode.TabIndex = 1490
        Me.txtCardSaleCode.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(11, 63)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 1487
        Me.RadLabel15.Text = "Location"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(237, 61)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(227, 18)
        Me.lblLocation.TabIndex = 1489
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(94, 60)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(142, 19)
        Me.txtLocation.TabIndex = 1488
        Me.txtLocation.Value = ""
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(210, 104)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel14.TabIndex = 1486
        Me.MyLabel14.Text = "To Date"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(15, 104)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel11.TabIndex = 1485
        Me.MyLabel11.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = False
        Me.lblToDate.BorderVisible = True
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(257, 103)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(113, 18)
        Me.lblToDate.TabIndex = 1484
        Me.lblToDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblToDate.TextWrap = False
        '
        'LblFromDate
        '
        Me.LblFromDate.AutoSize = False
        Me.LblFromDate.BorderVisible = True
        Me.LblFromDate.FieldName = Nothing
        Me.LblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromDate.Location = New System.Drawing.Point(93, 103)
        Me.LblFromDate.Name = "LblFromDate"
        Me.LblFromDate.Size = New System.Drawing.Size(114, 18)
        Me.LblFromDate.TabIndex = 1483
        Me.LblFromDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblFromDate.TextWrap = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(12, 82)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel13.TabIndex = 1481
        Me.MyLabel13.Text = "Card Sale Code"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
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
        Me.txtDate.Location = New System.Drawing.Point(94, 41)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(125, 18)
        Me.txtDate.TabIndex = 1480
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(12, 41)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1479
        Me.RadLabel4.Text = "Date"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstGatePass)
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstTruckSheet)
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstCardIndent)
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstCashIndent)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(665, 23)
        Me.RadGroupBox3.TabIndex = 1
        '
        'rdbAgainstGatePass
        '
        Me.rdbAgainstGatePass.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstGatePass.Location = New System.Drawing.Point(516, 2)
        Me.rdbAgainstGatePass.Name = "rdbAgainstGatePass"
        Me.rdbAgainstGatePass.Size = New System.Drawing.Size(109, 18)
        Me.rdbAgainstGatePass.TabIndex = 4
        Me.rdbAgainstGatePass.Text = "Against Gate Pass"
        Me.rdbAgainstGatePass.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbAgainstTruckSheet
        '
        Me.rdbAgainstTruckSheet.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstTruckSheet.Location = New System.Drawing.Point(372, 2)
        Me.rdbAgainstTruckSheet.Name = "rdbAgainstTruckSheet"
        Me.rdbAgainstTruckSheet.Size = New System.Drawing.Size(119, 18)
        Me.rdbAgainstTruckSheet.TabIndex = 3
        Me.rdbAgainstTruckSheet.Text = "Against Truck Sheet"
        Me.rdbAgainstTruckSheet.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbAgainstCardIndent
        '
        Me.rdbAgainstCardIndent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstCardIndent.Location = New System.Drawing.Point(215, 2)
        Me.rdbAgainstCardIndent.Name = "rdbAgainstCardIndent"
        Me.rdbAgainstCardIndent.Size = New System.Drawing.Size(120, 18)
        Me.rdbAgainstCardIndent.TabIndex = 2
        Me.rdbAgainstCardIndent.Text = "Against Card Indent"
        Me.rdbAgainstCardIndent.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbAgainstCashIndent
        '
        Me.rdbAgainstCashIndent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstCashIndent.Location = New System.Drawing.Point(35, 2)
        Me.rdbAgainstCashIndent.Name = "rdbAgainstCashIndent"
        Me.rdbAgainstCashIndent.Size = New System.Drawing.Size(120, 18)
        Me.rdbAgainstCashIndent.TabIndex = 1
        Me.rdbAgainstCashIndent.Text = "Against Cash Indent"
        Me.rdbAgainstCashIndent.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView1.Size = New System.Drawing.Size(697, 218)
        Me.RadPageView1.TabIndex = 24
        Me.RadPageView1.Text = "Truck Sheet Import"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoScroll = True
        Me.RadPageViewPage1.Controls.Add(Me.Gv1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(44.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(676, 170)
        Me.RadPageViewPage1.Text = "Items"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        Me.Gv1.Name = "Gv1"
        Me.Gv1.Size = New System.Drawing.Size(676, 170)
        Me.Gv1.TabIndex = 2
        Me.Gv1.Text = "RadGridView1"
        Me.Gv1.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage2.Controls.Add(Me.Panel1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(111.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(676, 170)
        Me.RadPageViewPage2.Text = "Truck Sheet Import"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 37)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.gvTS)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvTSItem)
        Me.SplitContainer3.Size = New System.Drawing.Size(676, 133)
        Me.SplitContainer3.SplitterDistance = 329
        Me.SplitContainer3.TabIndex = 5
        '
        'gvTS
        '
        Me.gvTS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTS.Location = New System.Drawing.Point(0, 0)
        Me.gvTS.Name = "gvTS"
        Me.gvTS.Size = New System.Drawing.Size(329, 133)
        Me.gvTS.TabIndex = 3
        Me.gvTS.Text = "RadGridView1"
        '
        'gvTSItem
        '
        Me.gvTSItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTSItem.Location = New System.Drawing.Point(0, 0)
        Me.gvTSItem.Name = "gvTSItem"
        Me.gvTSItem.Size = New System.Drawing.Size(343, 133)
        Me.gvTSItem.TabIndex = 4
        Me.gvTSItem.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExcept)
        Me.Panel1.Controls.Add(Me.lblExcept)
        Me.Panel1.Controls.Add(Me.txtExcept)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtBrowse)
        Me.Panel1.Controls.Add(Me.btnGo)
        Me.Panel1.Controls.Add(Me.btnBrowse)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(676, 37)
        Me.Panel1.TabIndex = 4
        '
        'btnExcept
        '
        Me.btnExcept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExcept.Location = New System.Drawing.Point(645, 7)
        Me.btnExcept.Name = "btnExcept"
        Me.btnExcept.Size = New System.Drawing.Size(28, 20)
        Me.btnExcept.TabIndex = 343
        Me.btnExcept.Text = "Add"
        Me.btnExcept.Visible = False
        '
        'lblExcept
        '
        Me.lblExcept.FieldName = Nothing
        Me.lblExcept.Location = New System.Drawing.Point(517, 8)
        Me.lblExcept.Name = "lblExcept"
        Me.lblExcept.Size = New System.Drawing.Size(39, 18)
        Me.lblExcept.TabIndex = 342
        Me.lblExcept.Text = "Except"
        Me.lblExcept.Visible = False
        '
        'txtExcept
        '
        Me.txtExcept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExcept.CalculationExpression = Nothing
        Me.txtExcept.FieldCode = Nothing
        Me.txtExcept.FieldDesc = Nothing
        Me.txtExcept.FieldMaxLength = 0
        Me.txtExcept.FieldName = Nothing
        Me.txtExcept.isCalculatedField = False
        Me.txtExcept.IsSourceFromTable = False
        Me.txtExcept.IsSourceFromValueList = False
        Me.txtExcept.IsUnique = False
        Me.txtExcept.Location = New System.Drawing.Point(561, 7)
        Me.txtExcept.MaxLength = 200
        Me.txtExcept.MendatroryField = True
        Me.txtExcept.MyLinkLable1 = Nothing
        Me.txtExcept.MyLinkLable2 = Nothing
        Me.txtExcept.Name = "txtExcept"
        Me.txtExcept.ReferenceFieldDesc = Nothing
        Me.txtExcept.ReferenceFieldName = Nothing
        Me.txtExcept.ReferenceTableName = Nothing
        Me.txtExcept.Size = New System.Drawing.Size(82, 20)
        Me.txtExcept.TabIndex = 341
        Me.txtExcept.Visible = False
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(456, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(57, 20)
        Me.RadButton1.TabIndex = 340
        Me.RadButton1.Text = "-->"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(5, 8)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel2.TabIndex = 339
        Me.MyLabel2.Text = "Select Notepad File"
        '
        'txtBrowse
        '
        Me.txtBrowse.CalculationExpression = Nothing
        Me.txtBrowse.FieldCode = Nothing
        Me.txtBrowse.FieldDesc = Nothing
        Me.txtBrowse.FieldMaxLength = 0
        Me.txtBrowse.FieldName = Nothing
        Me.txtBrowse.isCalculatedField = False
        Me.txtBrowse.IsSourceFromTable = False
        Me.txtBrowse.IsSourceFromValueList = False
        Me.txtBrowse.IsUnique = False
        Me.txtBrowse.Location = New System.Drawing.Point(117, 7)
        Me.txtBrowse.MaxLength = 50
        Me.txtBrowse.MendatroryField = True
        Me.txtBrowse.MyLinkLable1 = Nothing
        Me.txtBrowse.MyLinkLable2 = Nothing
        Me.txtBrowse.Name = "txtBrowse"
        Me.txtBrowse.ReadOnly = True
        Me.txtBrowse.ReferenceFieldDesc = Nothing
        Me.txtBrowse.ReferenceFieldName = Nothing
        Me.txtBrowse.ReferenceTableName = Nothing
        Me.txtBrowse.Size = New System.Drawing.Size(236, 20)
        Me.txtBrowse.TabIndex = 337
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(415, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(37, 20)
        Me.btnGo.TabIndex = 336
        Me.btnGo.Text = ">>"
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(354, 7)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(59, 20)
        Me.btnBrowse.TabIndex = 338
        Me.btnBrowse.Text = "Browse..."
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer4)
        Me.RadPageViewPage3.Controls.Add(Me.Panel2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(101.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(676, 170)
        Me.RadPageViewPage3.Text = "Gate Pass Import"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 37)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.gvGP)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.gvGPItem)
        Me.SplitContainer4.Size = New System.Drawing.Size(676, 133)
        Me.SplitContainer4.SplitterDistance = 329
        Me.SplitContainer4.TabIndex = 6
        '
        'gvGP
        '
        Me.gvGP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvGP.Location = New System.Drawing.Point(0, 0)
        Me.gvGP.Name = "gvGP"
        Me.gvGP.Size = New System.Drawing.Size(329, 133)
        Me.gvGP.TabIndex = 3
        Me.gvGP.Text = "RadGridView1"
        '
        'gvGPItem
        '
        Me.gvGPItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvGPItem.Location = New System.Drawing.Point(0, 0)
        Me.gvGPItem.Name = "gvGPItem"
        Me.gvGPItem.Size = New System.Drawing.Size(343, 133)
        Me.gvGPItem.TabIndex = 4
        Me.gvGPItem.Text = "RadGridView1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadButton3)
        Me.Panel2.Controls.Add(Me.MyLabel4)
        Me.Panel2.Controls.Add(Me.txtFolderBrowse)
        Me.Panel2.Controls.Add(Me.RadButton4)
        Me.Panel2.Controls.Add(Me.btnFolderBrowse)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(676, 37)
        Me.Panel2.TabIndex = 5
        '
        'RadButton3
        '
        Me.RadButton3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton3.Location = New System.Drawing.Point(466, 7)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(57, 20)
        Me.RadButton3.TabIndex = 340
        Me.RadButton3.Text = "-->"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(5, 8)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(117, 18)
        Me.MyLabel4.TabIndex = 339
        Me.MyLabel4.Text = "Select Notepad Folder"
        '
        'txtFolderBrowse
        '
        Me.txtFolderBrowse.CalculationExpression = Nothing
        Me.txtFolderBrowse.FieldCode = Nothing
        Me.txtFolderBrowse.FieldDesc = Nothing
        Me.txtFolderBrowse.FieldMaxLength = 0
        Me.txtFolderBrowse.FieldName = Nothing
        Me.txtFolderBrowse.isCalculatedField = False
        Me.txtFolderBrowse.IsSourceFromTable = False
        Me.txtFolderBrowse.IsSourceFromValueList = False
        Me.txtFolderBrowse.IsUnique = False
        Me.txtFolderBrowse.Location = New System.Drawing.Point(129, 7)
        Me.txtFolderBrowse.MaxLength = 50
        Me.txtFolderBrowse.MendatroryField = True
        Me.txtFolderBrowse.MyLinkLable1 = Nothing
        Me.txtFolderBrowse.MyLinkLable2 = Nothing
        Me.txtFolderBrowse.Name = "txtFolderBrowse"
        Me.txtFolderBrowse.ReadOnly = True
        Me.txtFolderBrowse.ReferenceFieldDesc = Nothing
        Me.txtFolderBrowse.ReferenceFieldName = Nothing
        Me.txtFolderBrowse.ReferenceTableName = Nothing
        Me.txtFolderBrowse.Size = New System.Drawing.Size(236, 20)
        Me.txtFolderBrowse.TabIndex = 337
        '
        'RadButton4
        '
        Me.RadButton4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton4.Location = New System.Drawing.Point(427, 7)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(37, 20)
        Me.RadButton4.TabIndex = 336
        Me.RadButton4.Text = ">>"
        '
        'btnFolderBrowse
        '
        Me.btnFolderBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFolderBrowse.Location = New System.Drawing.Point(366, 7)
        Me.btnFolderBrowse.Name = "btnFolderBrowse"
        Me.btnFolderBrowse.Size = New System.Drawing.Size(59, 20)
        Me.btnFolderBrowse.TabIndex = 338
        Me.btnFolderBrowse.Text = "Browse..."
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(237, 9)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(190, 18)
        Me.RadButton2.TabIndex = 14
        Me.RadButton2.Text = "Save CD Data Without Reference"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(628, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        '
        'btnSaveAndPost
        '
        Me.btnSaveAndPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAndPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAndPost.Location = New System.Drawing.Point(145, 9)
        Me.btnSaveAndPost.Name = "btnSaveAndPost"
        Me.btnSaveAndPost.Size = New System.Drawing.Size(88, 18)
        Me.btnSaveAndPost.TabIndex = 12
        Me.btnSaveAndPost.Text = "Save && Post"
        '
        'btnExportInvalid
        '
        Me.btnExportInvalid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportInvalid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportInvalid.Location = New System.Drawing.Point(433, 9)
        Me.btnExportInvalid.Name = "btnExportInvalid"
        Me.btnExportInvalid.Size = New System.Drawing.Size(106, 18)
        Me.btnExportInvalid.TabIndex = 11
        Me.btnExportInvalid.Text = "Export Unvalidated"
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.Location = New System.Drawing.Point(77, 9)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(66, 18)
        Me.btnValidate.TabIndex = 10
        Me.btnValidate.Text = "Validate"
        '
        'btnExportFormat
        '
        Me.btnExportFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportFormat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportFormat.Location = New System.Drawing.Point(541, 9)
        Me.btnExportFormat.Name = "btnExportFormat"
        Me.btnExportFormat.Size = New System.Drawing.Size(83, 18)
        Me.btnExportFormat.TabIndex = 9
        Me.btnExportFormat.Text = "Export Format"
        '
        'btnSelectSheet
        '
        Me.btnSelectSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectSheet.Location = New System.Drawing.Point(5, 9)
        Me.btnSelectSheet.Name = "btnSelectSheet"
        Me.btnSelectSheet.Size = New System.Drawing.Size(72, 18)
        Me.btnSelectSheet.TabIndex = 8
        Me.btnSelectSheet.Text = "Select Sheet"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'frmDairyBookingUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(697, 398)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDairyBookingUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Dairy Booking Uploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdbAgainstGatePass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAgainstTruckSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAgainstCardIndent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAgainstCashIndent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.gvTS.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTSItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTSItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnExcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.gvGP.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvGP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvGPItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvGPItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFolderBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnFolderBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportInvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveAndPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportInvalid As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnValidate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportFormat As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectSheet As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbAgainstCashIndent As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAgainstCardIndent As RadRadioButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents LblFromDate As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtCardSaleCode As common.UserControls.txtFinder
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gvTS As RadGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtBrowse As common.Controls.MyTextBox
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnBrowse As RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents gvTSItem As RadGridView
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents rdbAgainstTruckSheet As RadRadioButton
    Friend WithEvents txtPaymentMode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnExcept As RadButton
    Friend WithEvents lblExcept As common.Controls.MyLabel
    Friend WithEvents txtExcept As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents gvGP As RadGridView
    Friend WithEvents gvGPItem As RadGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtFolderBrowse As common.Controls.MyTextBox
    Friend WithEvents RadButton4 As RadButton
    Friend WithEvents btnFolderBrowse As RadButton
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents rdbAgainstGatePass As RadRadioButton
    Friend WithEvents RadButton2 As RadButton
End Class
