<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDemandBooking
    Inherits FrmMainTranScreen

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
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel()
        Me.rgbDemandHead = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.chkEveningPosted = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.chkMorningPosted = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDocAmt = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.rgbMilk = New Telerik.WinControls.UI.RadGroupBox()
        Me.LblDocAmt = New common.Controls.MyLabel()
        Me.lblDocumentAmt = New common.Controls.MyLabel()
        Me.lblCrate = New common.Controls.MyLabel()
        Me.lblTotalCrate = New common.Controls.MyLabel()
        Me.lblLitre = New common.Controls.MyLabel()
        Me.lblTotalLitre = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.rgbProduct = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblPTotCount = New common.Controls.MyLabel()
        Me.txtPCount = New common.Controls.MyLabel()
        Me.lblPTotAmt = New common.Controls.MyLabel()
        Me.txtPAmt = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtcustomersearch = New common.Controls.MyTextBox()
        Me.btnSearch = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnMorningEveningBoth = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnEvening = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnMorning = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblTransporterName = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.chkEveningGatepassTruckSheetGenerated = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.chkMorningGatepassTruckSheetGenerated = New Telerik.WinControls.UI.RadCheckBox()
        Me.TxtCity = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbnFreshAmbientBoth = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtn_Ambient = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtn_Fresh = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblCityName = New common.Controls.MyLabel()
        Me.chkIndividualCustomer = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.btnUpdateCrateAndAmt = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtCustomerNo = New common.UserControls.txtFinder()
        Me.txtTripNo = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.UserControls.txtFinder()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Me.btnFullMode = New Telerik.WinControls.UI.RadButton()
        Me.SplitButtonTruckSheet = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmi_TS_PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmi_TS_Excel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btn_TSCancel = New Telerik.WinControls.UI.RadButton()
        Me.btn_GPCancel = New Telerik.WinControls.UI.RadButton()
        Me.btn_Gatepass = New Telerik.WinControls.UI.RadButton()
        Me.btn_TruckSheet = New Telerik.WinControls.UI.RadButton()
        Me.btnAssessment = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnreverse = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendEmailSMS = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnQuickDemand = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.rgbDemandHead, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbDemandHead.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEveningPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMorningPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbMilk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbMilk.SuspendLayout()
        CType(Me.LblDocAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLitre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalLitre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbProduct.SuspendLayout()
        CType(Me.lblPTotCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPTotAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustomersearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rbtnMorningEveningBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEveningGatepassTruckSheetGenerated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMorningGatepassTruckSheetGenerated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbnFreshAmbientBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_Ambient, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_Fresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIndividualCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateCrateAndAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTripNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.btnFullMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitButtonTruckSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_TSCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_GPCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_Gatepass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_TruckSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAssessment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnQuickDemand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        Me.RadSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(1124, 475)
        Me.RadSplitContainer1.TabIndex = 0
        Me.RadSplitContainer1.TabStop = False
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.rgbDemandHead)
        Me.SplitPanel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel1.Size = New System.Drawing.Size(1124, 434)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, 0.4220183!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 154)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'rgbDemandHead
        '
        Me.rgbDemandHead.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbDemandHead.Controls.Add(Me.btnQuickDemand)
        Me.rgbDemandHead.Controls.Add(Me.RadLabel1)
        Me.rgbDemandHead.Controls.Add(Me.chkEveningPosted)
        Me.rgbDemandHead.Controls.Add(Me.txtDocNo)
        Me.rgbDemandHead.Controls.Add(Me.chkMorningPosted)
        Me.rgbDemandHead.Controls.Add(Me.RadLabel4)
        Me.rgbDemandHead.Controls.Add(Me.MyLabel3)
        Me.rgbDemandHead.Controls.Add(Me.txtDate)
        Me.rgbDemandHead.Controls.Add(Me.txtDocAmt)
        Me.rgbDemandHead.Controls.Add(Me.lblLocation)
        Me.rgbDemandHead.Controls.Add(Me.rgbMilk)
        Me.rgbDemandHead.Controls.Add(Me.btnAddNew)
        Me.rgbDemandHead.Controls.Add(Me.rgbProduct)
        Me.rgbDemandHead.Controls.Add(Me.txtLocation)
        Me.rgbDemandHead.Controls.Add(Me.txtcustomersearch)
        Me.rgbDemandHead.Controls.Add(Me.RadLabel15)
        Me.rgbDemandHead.Controls.Add(Me.btnSearch)
        Me.rgbDemandHead.Controls.Add(Me.RadGroupBox3)
        Me.rgbDemandHead.Controls.Add(Me.lblTransporterName)
        Me.rgbDemandHead.Controls.Add(Me.txtRouteNo)
        Me.rgbDemandHead.Controls.Add(Me.MyLabel2)
        Me.rgbDemandHead.Controls.Add(Me.lblRouteNo)
        Me.rgbDemandHead.Controls.Add(Me.chkEveningGatepassTruckSheetGenerated)
        Me.rgbDemandHead.Controls.Add(Me.lblRouteDesc)
        Me.rgbDemandHead.Controls.Add(Me.chkMorningGatepassTruckSheetGenerated)
        Me.rgbDemandHead.Controls.Add(Me.TxtCity)
        Me.rgbDemandHead.Controls.Add(Me.UsLock1)
        Me.rgbDemandHead.Controls.Add(Me.MyLabel5)
        Me.rgbDemandHead.Controls.Add(Me.RadGroupBox1)
        Me.rgbDemandHead.Controls.Add(Me.lblCityName)
        Me.rgbDemandHead.Controls.Add(Me.chkIndividualCustomer)
        Me.rgbDemandHead.Controls.Add(Me.btnGo)
        Me.rgbDemandHead.Controls.Add(Me.lblCustomerName)
        Me.rgbDemandHead.Controls.Add(Me.btnUpdateCrateAndAmt)
        Me.rgbDemandHead.Controls.Add(Me.RadLabel2)
        Me.rgbDemandHead.Controls.Add(Me.MyLabel18)
        Me.rgbDemandHead.Controls.Add(Me.txtCustomerNo)
        Me.rgbDemandHead.Controls.Add(Me.txtTripNo)
        Me.rgbDemandHead.Controls.Add(Me.MyLabel1)
        Me.rgbDemandHead.Controls.Add(Me.lblVehicleNo)
        Me.rgbDemandHead.Controls.Add(Me.txtVehicleNo)
        Me.rgbDemandHead.HeaderText = "Demand Head"
        Me.rgbDemandHead.Location = New System.Drawing.Point(5, 6)
        Me.rgbDemandHead.Name = "rgbDemandHead"
        Me.rgbDemandHead.Size = New System.Drawing.Size(1115, 178)
        Me.rgbDemandHead.TabIndex = 1471
        Me.rgbDemandHead.Text = "Demand Head"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(17, 21)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel1.TabIndex = 49
        Me.RadLabel1.Text = "Booking No"
        '
        'chkEveningPosted
        '
        Me.chkEveningPosted.Location = New System.Drawing.Point(845, 149)
        Me.chkEveningPosted.Name = "chkEveningPosted"
        Me.chkEveningPosted.ReadOnly = True
        Me.chkEveningPosted.Size = New System.Drawing.Size(97, 18)
        Me.chkEveningPosted.TabIndex = 1470
        Me.chkEveningPosted.Text = "Evening Posted"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(96, 19)
        Me.txtDocNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(297, 20)
        Me.txtDocNo.TabIndex = 46
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'chkMorningPosted
        '
        Me.chkMorningPosted.Location = New System.Drawing.Point(845, 128)
        Me.chkMorningPosted.Name = "chkMorningPosted"
        Me.chkMorningPosted.ReadOnly = True
        Me.chkMorningPosted.Size = New System.Drawing.Size(100, 18)
        Me.chkMorningPosted.TabIndex = 1469
        Me.chkMorningPosted.Text = "Morning Posted"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(413, 21)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 47
        Me.RadLabel4.Text = "Date"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(855, 108)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel3.TabIndex = 1467
        Me.MyLabel3.Text = "Doc Amount"
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
        Me.txtDate.Location = New System.Drawing.Point(446, 20)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(136, 18)
        Me.txtDate.TabIndex = 48
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDocAmt
        '
        Me.txtDocAmt.AutoSize = False
        Me.txtDocAmt.BorderVisible = True
        Me.txtDocAmt.FieldName = Nothing
        Me.txtDocAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocAmt.Location = New System.Drawing.Point(945, 106)
        Me.txtDocAmt.Name = "txtDocAmt"
        Me.txtDocAmt.Size = New System.Drawing.Size(128, 20)
        Me.txtDocAmt.TabIndex = 1468
        Me.txtDocAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(217, 86)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(227, 18)
        Me.lblLocation.TabIndex = 22
        Me.lblLocation.TextWrap = False
        '
        'rgbMilk
        '
        Me.rgbMilk.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbMilk.Controls.Add(Me.LblDocAmt)
        Me.rgbMilk.Controls.Add(Me.lblDocumentAmt)
        Me.rgbMilk.Controls.Add(Me.lblCrate)
        Me.rgbMilk.Controls.Add(Me.lblTotalCrate)
        Me.rgbMilk.Controls.Add(Me.lblLitre)
        Me.rgbMilk.Controls.Add(Me.lblTotalLitre)
        Me.rgbMilk.HeaderText = "Milk"
        Me.rgbMilk.Location = New System.Drawing.Point(597, 42)
        Me.rgbMilk.Name = "rgbMilk"
        Me.rgbMilk.Size = New System.Drawing.Size(248, 80)
        Me.rgbMilk.TabIndex = 1466
        Me.rgbMilk.Text = "Milk"
        '
        'LblDocAmt
        '
        Me.LblDocAmt.FieldName = Nothing
        Me.LblDocAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocAmt.Location = New System.Drawing.Point(5, 59)
        Me.LblDocAmt.Name = "LblDocAmt"
        Me.LblDocAmt.Size = New System.Drawing.Size(74, 16)
        Me.LblDocAmt.TabIndex = 145
        Me.LblDocAmt.Text = "Milk Amount"
        '
        'lblDocumentAmt
        '
        Me.lblDocumentAmt.AutoSize = False
        Me.lblDocumentAmt.BorderVisible = True
        Me.lblDocumentAmt.FieldName = Nothing
        Me.lblDocumentAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentAmt.Location = New System.Drawing.Point(80, 57)
        Me.lblDocumentAmt.Name = "lblDocumentAmt"
        Me.lblDocumentAmt.Size = New System.Drawing.Size(156, 20)
        Me.lblDocumentAmt.TabIndex = 146
        Me.lblDocumentAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCrate
        '
        Me.lblCrate.FieldName = Nothing
        Me.lblCrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCrate.Location = New System.Drawing.Point(5, 15)
        Me.lblCrate.Name = "lblCrate"
        Me.lblCrate.Size = New System.Drawing.Size(65, 16)
        Me.lblCrate.TabIndex = 143
        Me.lblCrate.Text = "Total Crate"
        '
        'lblTotalCrate
        '
        Me.lblTotalCrate.AutoSize = False
        Me.lblTotalCrate.BorderVisible = True
        Me.lblTotalCrate.FieldName = Nothing
        Me.lblTotalCrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCrate.Location = New System.Drawing.Point(80, 13)
        Me.lblTotalCrate.Name = "lblTotalCrate"
        Me.lblTotalCrate.Size = New System.Drawing.Size(156, 20)
        Me.lblTotalCrate.TabIndex = 144
        Me.lblTotalCrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLitre
        '
        Me.lblLitre.FieldName = Nothing
        Me.lblLitre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLitre.Location = New System.Drawing.Point(5, 37)
        Me.lblLitre.Name = "lblLitre"
        Me.lblLitre.Size = New System.Drawing.Size(61, 16)
        Me.lblLitre.TabIndex = 141
        Me.lblLitre.Text = "Total Litre"
        '
        'lblTotalLitre
        '
        Me.lblTotalLitre.AutoSize = False
        Me.lblTotalLitre.BorderVisible = True
        Me.lblTotalLitre.FieldName = Nothing
        Me.lblTotalLitre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalLitre.Location = New System.Drawing.Point(80, 35)
        Me.lblTotalLitre.Name = "lblTotalLitre"
        Me.lblTotalLitre.Size = New System.Drawing.Size(156, 20)
        Me.lblTotalLitre.TabIndex = 142
        Me.lblTotalLitre.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(393, 19)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 51
        '
        'rgbProduct
        '
        Me.rgbProduct.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbProduct.Controls.Add(Me.lblPTotCount)
        Me.rgbProduct.Controls.Add(Me.txtPCount)
        Me.rgbProduct.Controls.Add(Me.lblPTotAmt)
        Me.rgbProduct.Controls.Add(Me.txtPAmt)
        Me.rgbProduct.HeaderText = "Product"
        Me.rgbProduct.Location = New System.Drawing.Point(855, 42)
        Me.rgbProduct.Name = "rgbProduct"
        Me.rgbProduct.Size = New System.Drawing.Size(225, 63)
        Me.rgbProduct.TabIndex = 1465
        Me.rgbProduct.Text = "Product"
        '
        'lblPTotCount
        '
        Me.lblPTotCount.FieldName = Nothing
        Me.lblPTotCount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPTotCount.Location = New System.Drawing.Point(3, 20)
        Me.lblPTotCount.Name = "lblPTotCount"
        Me.lblPTotCount.Size = New System.Drawing.Size(69, 16)
        Me.lblPTotCount.TabIndex = 141
        Me.lblPTotCount.Text = "Total Count"
        '
        'txtPCount
        '
        Me.txtPCount.AutoSize = False
        Me.txtPCount.BorderVisible = True
        Me.txtPCount.FieldName = Nothing
        Me.txtPCount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCount.Location = New System.Drawing.Point(90, 18)
        Me.txtPCount.Name = "txtPCount"
        Me.txtPCount.Size = New System.Drawing.Size(128, 20)
        Me.txtPCount.TabIndex = 142
        Me.txtPCount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPTotAmt
        '
        Me.lblPTotAmt.FieldName = Nothing
        Me.lblPTotAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPTotAmt.Location = New System.Drawing.Point(5, 42)
        Me.lblPTotAmt.Name = "lblPTotAmt"
        Me.lblPTotAmt.Size = New System.Drawing.Size(79, 16)
        Me.lblPTotAmt.TabIndex = 139
        Me.lblPTotAmt.Text = "Total Amount"
        '
        'txtPAmt
        '
        Me.txtPAmt.AutoSize = False
        Me.txtPAmt.BorderVisible = True
        Me.txtPAmt.FieldName = Nothing
        Me.txtPAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPAmt.Location = New System.Drawing.Point(90, 40)
        Me.txtPAmt.Name = "txtPAmt"
        Me.txtPAmt.Size = New System.Drawing.Size(128, 20)
        Me.txtPAmt.TabIndex = 140
        Me.txtPAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.Enabled = False
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(96, 86)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(115, 18)
        Me.txtLocation.TabIndex = 21
        Me.txtLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(17, 87)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 23
        Me.RadLabel15.Text = "Location"
        '
        'txtcustomersearch
        '
        Me.txtcustomersearch.CalculationExpression = Nothing
        Me.txtcustomersearch.FieldCode = Nothing
        Me.txtcustomersearch.FieldDesc = Nothing
        Me.txtcustomersearch.FieldMaxLength = 0
        Me.txtcustomersearch.FieldName = Nothing
        Me.txtcustomersearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomersearch.isCalculatedField = False
        Me.txtcustomersearch.IsSourceFromTable = False
        Me.txtcustomersearch.IsSourceFromValueList = False
        Me.txtcustomersearch.IsUnique = False
        Me.txtcustomersearch.Location = New System.Drawing.Point(447, 128)
        Me.txtcustomersearch.MaxLength = 50
        Me.txtcustomersearch.MendatroryField = False
        Me.txtcustomersearch.Modified = True
        Me.txtcustomersearch.MyLinkLable1 = Nothing
        Me.txtcustomersearch.MyLinkLable2 = Nothing
        Me.txtcustomersearch.Name = "txtcustomersearch"
        Me.txtcustomersearch.ReferenceFieldDesc = Nothing
        Me.txtcustomersearch.ReferenceFieldName = Nothing
        Me.txtcustomersearch.ReferenceTableName = Nothing
        Me.txtcustomersearch.Size = New System.Drawing.Size(135, 18)
        Me.txtcustomersearch.TabIndex = 1464
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(451, 149)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(131, 18)
        Me.btnSearch.TabIndex = 1463
        Me.btnSearch.Text = "Customer Search"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rbtnMorningEveningBoth)
        Me.RadGroupBox3.Controls.Add(Me.rbtnEvening)
        Me.RadGroupBox3.Controls.Add(Me.rbtnMorning)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(447, 42)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(64, 66)
        Me.RadGroupBox3.TabIndex = 67
        '
        'rbtnMorningEveningBoth
        '
        Me.rbtnMorningEveningBoth.Location = New System.Drawing.Point(4, 42)
        Me.rbtnMorningEveningBoth.Name = "rbtnMorningEveningBoth"
        Me.rbtnMorningEveningBoth.Size = New System.Drawing.Size(44, 18)
        Me.rbtnMorningEveningBoth.TabIndex = 2
        Me.rbtnMorningEveningBoth.TabStop = False
        Me.rbtnMorningEveningBoth.Text = "Both"
        '
        'rbtnEvening
        '
        Me.rbtnEvening.Location = New System.Drawing.Point(4, 24)
        Me.rbtnEvening.Name = "rbtnEvening"
        Me.rbtnEvening.Size = New System.Drawing.Size(59, 18)
        Me.rbtnEvening.TabIndex = 1
        Me.rbtnEvening.TabStop = False
        Me.rbtnEvening.Text = "Evening"
        '
        'rbtnMorning
        '
        Me.rbtnMorning.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnMorning.Location = New System.Drawing.Point(4, 4)
        Me.rbtnMorning.Name = "rbtnMorning"
        Me.rbtnMorning.Size = New System.Drawing.Size(63, 18)
        Me.rbtnMorning.TabIndex = 0
        Me.rbtnMorning.TabStop = False
        Me.rbtnMorning.Text = "Morning"
        Me.rbtnMorning.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = False
        Me.lblTransporterName.BorderVisible = True
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporterName.Location = New System.Drawing.Point(96, 149)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(224, 18)
        Me.lblTransporterName.TabIndex = 1462
        Me.lblTransporterName.TextWrap = False
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(96, 42)
        Me.txtRouteNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRouteNo.MendatroryField = True
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Me.lblRouteNo
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.MyShowMasterFormButton = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(115, 19)
        Me.txtRouteNo.TabIndex = 145
        Me.txtRouteNo.Value = ""
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(17, 42)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 146
        Me.lblRouteNo.Text = "Route No"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(17, 150)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel2.TabIndex = 1461
        Me.MyLabel2.Text = "Transporter"
        '
        'chkEveningGatepassTruckSheetGenerated
        '
        Me.chkEveningGatepassTruckSheetGenerated.Location = New System.Drawing.Point(605, 149)
        Me.chkEveningGatepassTruckSheetGenerated.Name = "chkEveningGatepassTruckSheetGenerated"
        Me.chkEveningGatepassTruckSheetGenerated.ReadOnly = True
        Me.chkEveningGatepassTruckSheetGenerated.Size = New System.Drawing.Size(224, 18)
        Me.chkEveningGatepassTruckSheetGenerated.TabIndex = 1460
        Me.chkEveningGatepassTruckSheetGenerated.Text = "Evening Gate Pass/Trucksheet Generated"
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteDesc.Location = New System.Drawing.Point(217, 42)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(227, 18)
        Me.lblRouteDesc.TabIndex = 147
        Me.lblRouteDesc.TextWrap = False
        '
        'chkMorningGatepassTruckSheetGenerated
        '
        Me.chkMorningGatepassTruckSheetGenerated.Location = New System.Drawing.Point(605, 128)
        Me.chkMorningGatepassTruckSheetGenerated.Name = "chkMorningGatepassTruckSheetGenerated"
        Me.chkMorningGatepassTruckSheetGenerated.ReadOnly = True
        Me.chkMorningGatepassTruckSheetGenerated.Size = New System.Drawing.Size(228, 18)
        Me.chkMorningGatepassTruckSheetGenerated.TabIndex = 1459
        Me.chkMorningGatepassTruckSheetGenerated.Text = "Morning Gate Pass/Trucksheet Generated"
        '
        'TxtCity
        '
        Me.TxtCity.CalculationExpression = Nothing
        Me.TxtCity.Enabled = False
        Me.TxtCity.FieldCode = Nothing
        Me.TxtCity.FieldDesc = Nothing
        Me.TxtCity.FieldMaxLength = 0
        Me.TxtCity.FieldName = Nothing
        Me.TxtCity.isCalculatedField = False
        Me.TxtCity.IsSourceFromTable = False
        Me.TxtCity.IsSourceFromValueList = False
        Me.TxtCity.IsUnique = False
        Me.TxtCity.Location = New System.Drawing.Point(96, 64)
        Me.TxtCity.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtCity.MendatroryField = True
        Me.TxtCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCity.MyLinkLable1 = Me.MyLabel5
        Me.TxtCity.MyLinkLable2 = Nothing
        Me.TxtCity.MyReadOnly = False
        Me.TxtCity.MyShowMasterFormButton = False
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.ReferenceFieldDesc = Nothing
        Me.TxtCity.ReferenceFieldName = Nothing
        Me.TxtCity.ReferenceTableName = Nothing
        Me.TxtCity.Size = New System.Drawing.Size(115, 19)
        Me.TxtCity.TabIndex = 148
        Me.TxtCity.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(17, 64)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(25, 18)
        Me.MyLabel5.TabIndex = 149
        Me.MyLabel5.Text = "City"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(598, 19)
        Me.UsLock1.Margin = New System.Windows.Forms.Padding(4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1458
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdbnFreshAmbientBoth)
        Me.RadGroupBox1.Controls.Add(Me.rbtn_Ambient)
        Me.RadGroupBox1.Controls.Add(Me.rbtn_Fresh)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(513, 42)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(69, 66)
        Me.RadGroupBox1.TabIndex = 60
        '
        'rdbnFreshAmbientBoth
        '
        Me.rdbnFreshAmbientBoth.Location = New System.Drawing.Point(3, 41)
        Me.rdbnFreshAmbientBoth.Name = "rdbnFreshAmbientBoth"
        Me.rdbnFreshAmbientBoth.Size = New System.Drawing.Size(44, 18)
        Me.rdbnFreshAmbientBoth.TabIndex = 2
        Me.rdbnFreshAmbientBoth.TabStop = False
        Me.rdbnFreshAmbientBoth.Text = "Both"
        '
        'rbtn_Ambient
        '
        Me.rbtn_Ambient.Location = New System.Drawing.Point(3, 24)
        Me.rbtn_Ambient.Name = "rbtn_Ambient"
        Me.rbtn_Ambient.Size = New System.Drawing.Size(63, 18)
        Me.rbtn_Ambient.TabIndex = 1
        Me.rbtn_Ambient.TabStop = False
        Me.rbtn_Ambient.Text = "Ambient"
        '
        'rbtn_Fresh
        '
        Me.rbtn_Fresh.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtn_Fresh.Location = New System.Drawing.Point(3, 4)
        Me.rbtn_Fresh.Name = "rbtn_Fresh"
        Me.rbtn_Fresh.Size = New System.Drawing.Size(47, 18)
        Me.rbtn_Fresh.TabIndex = 0
        Me.rbtn_Fresh.TabStop = False
        Me.rbtn_Fresh.Text = "Fresh"
        Me.rbtn_Fresh.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = False
        Me.lblCityName.BorderVisible = True
        Me.lblCityName.FieldName = Nothing
        Me.lblCityName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCityName.Location = New System.Drawing.Point(217, 64)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(227, 18)
        Me.lblCityName.TabIndex = 150
        Me.lblCityName.TextWrap = False
        '
        'chkIndividualCustomer
        '
        Me.chkIndividualCustomer.Location = New System.Drawing.Point(323, 148)
        Me.chkIndividualCustomer.Name = "chkIndividualCustomer"
        Me.chkIndividualCustomer.Size = New System.Drawing.Size(120, 18)
        Me.chkIndividualCustomer.TabIndex = 1456
        Me.chkIndividualCustomer.Text = "Individual Customer"
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(744, 20)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(97, 18)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = "Go >>>"
        Me.btnGo.Visible = False
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(218, 128)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(226, 18)
        Me.lblCustomerName.TabIndex = 1454
        Me.lblCustomerName.TextWrap = False
        '
        'btnUpdateCrateAndAmt
        '
        Me.btnUpdateCrateAndAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateCrateAndAmt.Location = New System.Drawing.Point(953, 130)
        Me.btnUpdateCrateAndAmt.Name = "btnUpdateCrateAndAmt"
        Me.btnUpdateCrateAndAmt.Size = New System.Drawing.Size(127, 35)
        Me.btnUpdateCrateAndAmt.TabIndex = 152
        Me.btnUpdateCrateAndAmt.Text = "Update Crate and Amount"
        Me.btnUpdateCrateAndAmt.TextWrap = True
        Me.btnUpdateCrateAndAmt.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(15, 129)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 1455
        Me.RadLabel2.Text = "Customer"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(446, 108)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel18.TabIndex = 1448
        Me.MyLabel18.Text = "Trip No"
        '
        'txtCustomerNo
        '
        Me.txtCustomerNo.CalculationExpression = Nothing
        Me.txtCustomerNo.FieldCode = Nothing
        Me.txtCustomerNo.FieldDesc = Nothing
        Me.txtCustomerNo.FieldMaxLength = 0
        Me.txtCustomerNo.FieldName = Nothing
        Me.txtCustomerNo.isCalculatedField = False
        Me.txtCustomerNo.IsSourceFromTable = False
        Me.txtCustomerNo.IsSourceFromValueList = False
        Me.txtCustomerNo.IsUnique = False
        Me.txtCustomerNo.Location = New System.Drawing.Point(96, 128)
        Me.txtCustomerNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerNo.MendatroryField = False
        Me.txtCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerNo.MyLinkLable1 = Me.RadLabel2
        Me.txtCustomerNo.MyLinkLable2 = Me.lblCustomerName
        Me.txtCustomerNo.MyReadOnly = False
        Me.txtCustomerNo.MyShowMasterFormButton = False
        Me.txtCustomerNo.Name = "txtCustomerNo"
        Me.txtCustomerNo.ReferenceFieldDesc = Nothing
        Me.txtCustomerNo.ReferenceFieldName = Nothing
        Me.txtCustomerNo.ReferenceTableName = Nothing
        Me.txtCustomerNo.Size = New System.Drawing.Size(115, 18)
        Me.txtCustomerNo.TabIndex = 1453
        Me.txtCustomerNo.Value = ""
        '
        'txtTripNo
        '
        Me.txtTripNo.CalculationExpression = Nothing
        Me.txtTripNo.FieldCode = Nothing
        Me.txtTripNo.FieldDesc = Nothing
        Me.txtTripNo.FieldMaxLength = 0
        Me.txtTripNo.FieldName = Nothing
        Me.txtTripNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripNo.isCalculatedField = False
        Me.txtTripNo.IsSourceFromTable = False
        Me.txtTripNo.IsSourceFromValueList = False
        Me.txtTripNo.IsUnique = False
        Me.txtTripNo.Location = New System.Drawing.Point(492, 107)
        Me.txtTripNo.MaxLength = 50
        Me.txtTripNo.MendatroryField = False
        Me.txtTripNo.Modified = True
        Me.txtTripNo.MyLinkLable1 = Nothing
        Me.txtTripNo.MyLinkLable2 = Nothing
        Me.txtTripNo.Name = "txtTripNo"
        Me.txtTripNo.ReferenceFieldDesc = Nothing
        Me.txtTripNo.ReferenceFieldName = Nothing
        Me.txtTripNo.ReferenceTableName = Nothing
        Me.txtTripNo.Size = New System.Drawing.Size(90, 18)
        Me.txtTripNo.TabIndex = 1449
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(17, 108)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel1.TabIndex = 1452
        Me.MyLabel1.Text = "Vehicle"
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = False
        Me.lblVehicleNo.BorderVisible = True
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.Location = New System.Drawing.Point(217, 107)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(227, 18)
        Me.lblVehicleNo.TabIndex = 1451
        Me.lblVehicleNo.TextWrap = False
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(96, 107)
        Me.txtVehicleNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtVehicleNo.MendatroryField = True
        Me.txtVehicleNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.MyLinkLable1 = Me.MyLabel1
        Me.txtVehicleNo.MyLinkLable2 = Me.lblVehicleNo
        Me.txtVehicleNo.MyReadOnly = False
        Me.txtVehicleNo.MyShowMasterFormButton = False
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(115, 18)
        Me.txtVehicleNo.TabIndex = 1450
        Me.txtVehicleNo.Value = ""
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv2)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(6, 190)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1114, 241)
        Me.RadGroupBox2.TabIndex = 50
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv2
        '
        Me.gv2.AutoScroll = True
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Controls.Add(Me.gv1)
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.VerticalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1094, 211)
        Me.gv2.TabIndex = 1
        Me.gv2.TabStop = False
        '
        'gv1
        '
        Me.gv1.AutoScroll = True
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.VerticalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1094, 211)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.btnFullMode)
        Me.SplitPanel2.Controls.Add(Me.SplitButtonTruckSheet)
        Me.SplitPanel2.Controls.Add(Me.btn_TSCancel)
        Me.SplitPanel2.Controls.Add(Me.btn_GPCancel)
        Me.SplitPanel2.Controls.Add(Me.btn_Gatepass)
        Me.SplitPanel2.Controls.Add(Me.btn_TruckSheet)
        Me.SplitPanel2.Controls.Add(Me.btnAssessment)
        Me.SplitPanel2.Controls.Add(Me.btnPost)
        Me.SplitPanel2.Controls.Add(Me.btnDelete)
        Me.SplitPanel2.Controls.Add(Me.btnPrint)
        Me.SplitPanel2.Controls.Add(Me.btnClose)
        Me.SplitPanel2.Controls.Add(Me.btnSave)
        Me.SplitPanel2.Controls.Add(Me.btnreverse)
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 438)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(1124, 37)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0!, -0.4220183!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -154)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'btnFullMode
        '
        Me.btnFullMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFullMode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFullMode.Location = New System.Drawing.Point(970, 11)
        Me.btnFullMode.Name = "btnFullMode"
        Me.btnFullMode.Size = New System.Drawing.Size(76, 20)
        Me.btnFullMode.TabIndex = 50
        Me.btnFullMode.Text = "Full Screen"
        '
        'SplitButtonTruckSheet
        '
        Me.SplitButtonTruckSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SplitButtonTruckSheet.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmi_TS_PDF, Me.rmi_TS_Excel})
        Me.SplitButtonTruckSheet.Location = New System.Drawing.Point(555, 11)
        Me.SplitButtonTruckSheet.Name = "SplitButtonTruckSheet"
        Me.SplitButtonTruckSheet.Size = New System.Drawing.Size(100, 20)
        Me.SplitButtonTruckSheet.TabIndex = 49
        Me.SplitButtonTruckSheet.Text = "TruckSheet"
        '
        'rmi_TS_PDF
        '
        Me.rmi_TS_PDF.Name = "rmi_TS_PDF"
        Me.rmi_TS_PDF.Text = "PDF"
        Me.rmi_TS_PDF.UseCompatibleTextRendering = False
        '
        'rmi_TS_Excel
        '
        Me.rmi_TS_Excel.Name = "rmi_TS_Excel"
        Me.rmi_TS_Excel.Text = "Excel"
        Me.rmi_TS_Excel.UseCompatibleTextRendering = False
        '
        'btn_TSCancel
        '
        Me.btn_TSCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_TSCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_TSCancel.Location = New System.Drawing.Point(743, 11)
        Me.btn_TSCancel.Name = "btn_TSCancel"
        Me.btn_TSCancel.Size = New System.Drawing.Size(76, 20)
        Me.btn_TSCancel.TabIndex = 26
        Me.btn_TSCancel.Text = "TS Cancel"
        '
        'btn_GPCancel
        '
        Me.btn_GPCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_GPCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_GPCancel.Location = New System.Drawing.Point(661, 11)
        Me.btn_GPCancel.Name = "btn_GPCancel"
        Me.btn_GPCancel.Size = New System.Drawing.Size(76, 20)
        Me.btn_GPCancel.TabIndex = 25
        Me.btn_GPCancel.Text = "GP Cancel"
        Me.btn_GPCancel.Visible = False
        '
        'btn_Gatepass
        '
        Me.btn_Gatepass.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_Gatepass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Gatepass.Location = New System.Drawing.Point(473, 11)
        Me.btn_Gatepass.Name = "btn_Gatepass"
        Me.btn_Gatepass.Size = New System.Drawing.Size(76, 20)
        Me.btn_Gatepass.TabIndex = 24
        Me.btn_Gatepass.Text = "Gate Pass"
        '
        'btn_TruckSheet
        '
        Me.btn_TruckSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_TruckSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_TruckSheet.Location = New System.Drawing.Point(430, 11)
        Me.btn_TruckSheet.Name = "btn_TruckSheet"
        Me.btn_TruckSheet.Size = New System.Drawing.Size(34, 20)
        Me.btn_TruckSheet.TabIndex = 23
        Me.btn_TruckSheet.Text = "Truck Sheet"
        Me.btn_TruckSheet.Visible = False
        '
        'btnAssessment
        '
        Me.btnAssessment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAssessment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssessment.Location = New System.Drawing.Point(299, 11)
        Me.btnAssessment.Name = "btnAssessment"
        Me.btnAssessment.Size = New System.Drawing.Size(123, 20)
        Me.btnAssessment.TabIndex = 13
        Me.btnAssessment.Text = "Assessment"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 15
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(151, 11)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(224, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 20)
        Me.btnPrint.TabIndex = 14
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1052, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 20)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 20)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save"
        '
        'btnreverse
        '
        Me.btnreverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreverse.Location = New System.Drawing.Point(826, 11)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(131, 20)
        Me.btnreverse.TabIndex = 22
        Me.btnreverse.Text = "Reverse/Recreate of Demand"
        Me.btnreverse.Visible = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1124, 20)
        Me.RadMenu1.TabIndex = 7
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSaveLayout, Me.btnLayout, Me.btnExport, Me.btnImport, Me.btnSendEmailSMS})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        '
        'btnLayout
        '
        Me.btnLayout.Name = "btnLayout"
        Me.btnLayout.Text = "Delete Layout"
        '
        'btnExport
        '
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export"
        '
        'btnImport
        '
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Text = "Import"
        '
        'btnSendEmailSMS
        '
        Me.btnSendEmailSMS.Name = "btnSendEmailSMS"
        Me.btnSendEmailSMS.Text = "E-Mail/SMS Setting"
        '
        'btnQuickDemand
        '
        Me.btnQuickDemand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuickDemand.Location = New System.Drawing.Point(855, 19)
        Me.btnQuickDemand.Name = "btnQuickDemand"
        Me.btnQuickDemand.Size = New System.Drawing.Size(126, 20)
        Me.btnQuickDemand.TabIndex = 1471
        Me.btnQuickDemand.Text = "Get Quick Demand"
        '
        'frmDemandBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1124, 495)
        Me.Controls.Add(Me.RadSplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmDemandBooking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Demand Booking Entry"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        CType(Me.rgbDemandHead, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbDemandHead.ResumeLayout(False)
        Me.rgbDemandHead.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEveningPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMorningPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbMilk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbMilk.ResumeLayout(False)
        Me.rgbMilk.PerformLayout()
        CType(Me.LblDocAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalCrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLitre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalLitre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbProduct.ResumeLayout(False)
        Me.rgbProduct.PerformLayout()
        CType(Me.lblPTotCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPTotAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustomersearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rbtnMorningEveningBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEveningGatepassTruckSheetGenerated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMorningGatepassTruckSheetGenerated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbnFreshAmbientBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_Ambient, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_Fresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIndividualCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateCrateAndAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTripNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv2.ResumeLayout(False)
        Me.gv2.PerformLayout()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.btnFullMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitButtonTruckSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_TSCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_GPCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_Gatepass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_TruckSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAssessment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnQuickDemand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnSendEmailSMS As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtn_Ambient As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtn_Fresh As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnreverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbnFreshAmbientBoth As RadRadioButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents rbtnMorningEveningBoth As RadRadioButton
    Friend WithEvents rbtnEvening As RadRadioButton
    Friend WithEvents rbtnMorning As RadRadioButton
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents lblCityName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtCity As common.UserControls.txtFinder
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnUpdateCrateAndAmt As RadButton
    Friend WithEvents btnAssessment As RadButton
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtTripNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.UserControls.txtFinder
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustomerNo As common.UserControls.txtFinder
    Friend WithEvents chkIndividualCustomer As RadCheckBox
    Friend WithEvents btn_TruckSheet As RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btn_Gatepass As RadButton
    Friend WithEvents btn_TSCancel As RadButton
    Friend WithEvents btn_GPCancel As RadButton
    Friend WithEvents chkEveningGatepassTruckSheetGenerated As RadCheckBox
    Friend WithEvents chkMorningGatepassTruckSheetGenerated As RadCheckBox
    Friend WithEvents lblTransporterName As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnSearch As RadButton
    Friend WithEvents txtcustomersearch As common.Controls.MyTextBox
    Friend WithEvents SplitButtonTruckSheet As RadSplitButton
    Friend WithEvents rmi_TS_PDF As RadMenuItem
    Friend WithEvents rmi_TS_Excel As RadMenuItem
    Friend WithEvents rgbMilk As RadGroupBox
    Friend WithEvents LblDocAmt As common.Controls.MyLabel
    Friend WithEvents lblDocumentAmt As common.Controls.MyLabel
    Friend WithEvents lblCrate As common.Controls.MyLabel
    Friend WithEvents lblTotalCrate As common.Controls.MyLabel
    Friend WithEvents lblLitre As common.Controls.MyLabel
    Friend WithEvents lblTotalLitre As common.Controls.MyLabel
    Friend WithEvents rgbProduct As RadGroupBox
    Friend WithEvents lblPTotCount As common.Controls.MyLabel
    Friend WithEvents txtPCount As common.Controls.MyLabel
    Friend WithEvents lblPTotAmt As common.Controls.MyLabel
    Friend WithEvents txtPAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDocAmt As common.Controls.MyLabel
    Friend WithEvents chkEveningPosted As RadCheckBox
    Friend WithEvents chkMorningPosted As RadCheckBox
    Friend WithEvents rgbDemandHead As RadGroupBox
    Friend WithEvents btnFullMode As RadButton
    Friend WithEvents btnQuickDemand As RadButton
End Class

