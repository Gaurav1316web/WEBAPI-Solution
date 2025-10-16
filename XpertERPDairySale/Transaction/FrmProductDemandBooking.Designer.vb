<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProductDemandBooking
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendEmailSMS = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rgbProduct = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblPTotCount = New common.Controls.MyLabel()
        Me.txtPCount = New common.Controls.MyLabel()
        Me.lblPTotAmt = New common.Controls.MyLabel()
        Me.txtPAmt = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtcustomersearch = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnSearch = New Telerik.WinControls.UI.RadButton()
        Me.lblTransporterName = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.txtCustomerNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.TxtCity = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtn_IceCream = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtn_Product = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkIndividualCustomer = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblCityName = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrintLoadinSlip = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnreverse = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnAssessment = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rgbProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbProduct.SuspendLayout()
        CType(Me.lblPTotCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPTotAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustomersearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rbtn_IceCream, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_Product, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIndividualCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintLoadinSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAssessment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1006, 20)
        Me.RadMenu1.TabIndex = 8
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSaveLayout, Me.btnLayout, Me.btnExport, Me.btnImport, Me.btnSendEmailSMS})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        Me.btnSaveLayout.UseCompatibleTextRendering = False
        '
        'btnLayout
        '
        Me.btnLayout.Name = "btnLayout"
        Me.btnLayout.Text = "Delete Layout"
        Me.btnLayout.UseCompatibleTextRendering = False
        '
        'btnExport
        '
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export"
        Me.btnExport.UseCompatibleTextRendering = False
        '
        'btnImport
        '
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Text = "Import"
        Me.btnImport.UseCompatibleTextRendering = False
        '
        'btnSendEmailSMS
        '
        Me.btnSendEmailSMS.Name = "btnSendEmailSMS"
        Me.btnSendEmailSMS.Text = "E-Mail/SMS Setting"
        Me.btnSendEmailSMS.UseCompatibleTextRendering = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintLoadinSlip)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAssessment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1006, 493)
        Me.SplitContainer1.SplitterDistance = 455
        Me.SplitContainer1.TabIndex = 9
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbProduct)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcustomersearch)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSearch)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTransporterName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRouteNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVehicleNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVehicleNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCustomerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtCity)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkIndividualCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCityName)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1006, 455)
        Me.SplitContainer2.SplitterDistance = 159
        Me.SplitContainer2.TabIndex = 0
        '
        'rgbProduct
        '
        Me.rgbProduct.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbProduct.Controls.Add(Me.lblPTotCount)
        Me.rgbProduct.Controls.Add(Me.txtPCount)
        Me.rgbProduct.Controls.Add(Me.lblPTotAmt)
        Me.rgbProduct.Controls.Add(Me.txtPAmt)
        Me.rgbProduct.HeaderText = "Product"
        Me.rgbProduct.Location = New System.Drawing.Point(583, 32)
        Me.rgbProduct.Name = "rgbProduct"
        Me.rgbProduct.Size = New System.Drawing.Size(225, 63)
        Me.rgbProduct.TabIndex = 1488
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
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(212, 72)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(227, 18)
        Me.lblLocation.TabIndex = 1466
        Me.lblLocation.TextWrap = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel1.TabIndex = 1462
        Me.RadLabel1.Text = "Booking No"
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
        Me.txtLocation.Location = New System.Drawing.Point(91, 72)
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
        Me.txtLocation.TabIndex = 1465
        Me.txtLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(12, 73)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 1467
        Me.RadLabel15.Text = "Location"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(593, 4)
        Me.UsLock1.Margin = New System.Windows.Forms.Padding(4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1464
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
        Me.txtcustomersearch.Location = New System.Drawing.Point(442, 114)
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
        Me.txtcustomersearch.TabIndex = 1487
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(388, 4)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 1463
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
        Me.txtDate.Location = New System.Drawing.Point(441, 5)
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
        Me.txtDate.TabIndex = 1461
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel4.Location = New System.Drawing.Point(408, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1460
        Me.RadLabel4.Text = "Date"
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(446, 135)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(131, 18)
        Me.btnSearch.TabIndex = 1486
        Me.btnSearch.Text = "Customer Search"
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = False
        Me.lblTransporterName.BorderVisible = True
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporterName.Location = New System.Drawing.Point(91, 135)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(224, 18)
        Me.lblTransporterName.TabIndex = 1485
        Me.lblTransporterName.TextWrap = False
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(91, 4)
        Me.txtDocNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(297, 20)
        Me.txtDocNo.TabIndex = 1459
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
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
        Me.txtRouteNo.Location = New System.Drawing.Point(91, 28)
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
        Me.txtRouteNo.TabIndex = 1469
        Me.txtRouteNo.Value = ""
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(12, 28)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 1470
        Me.lblRouteNo.Text = "Route No"
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
        Me.txtVehicleNo.Location = New System.Drawing.Point(91, 93)
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
        Me.txtVehicleNo.TabIndex = 1477
        Me.txtVehicleNo.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 94)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel1.TabIndex = 1479
        Me.MyLabel1.Text = "Vehicle"
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = False
        Me.lblVehicleNo.BorderVisible = True
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.Location = New System.Drawing.Point(212, 93)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(227, 18)
        Me.lblVehicleNo.TabIndex = 1478
        Me.lblVehicleNo.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 136)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel2.TabIndex = 1484
        Me.MyLabel2.Text = "Transporter"
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteDesc.Location = New System.Drawing.Point(212, 28)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(227, 18)
        Me.lblRouteDesc.TabIndex = 1471
        Me.lblRouteDesc.TextWrap = False
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
        Me.txtCustomerNo.Location = New System.Drawing.Point(91, 114)
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
        Me.txtCustomerNo.TabIndex = 1480
        Me.txtCustomerNo.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(10, 115)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 1482
        Me.RadLabel2.Text = "Customer"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(213, 114)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(226, 18)
        Me.lblCustomerName.TabIndex = 1481
        Me.lblCustomerName.TextWrap = False
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
        Me.TxtCity.Location = New System.Drawing.Point(91, 50)
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
        Me.TxtCity.TabIndex = 1472
        Me.TxtCity.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(12, 50)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(25, 18)
        Me.MyLabel5.TabIndex = 1473
        Me.MyLabel5.Text = "City"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rbtn_IceCream)
        Me.RadGroupBox3.Controls.Add(Me.rbtn_Product)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(446, 28)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(131, 49)
        Me.RadGroupBox3.TabIndex = 1468
        '
        'rbtn_IceCream
        '
        Me.rbtn_IceCream.Location = New System.Drawing.Point(3, 24)
        Me.rbtn_IceCream.Name = "rbtn_IceCream"
        Me.rbtn_IceCream.Size = New System.Drawing.Size(70, 18)
        Me.rbtn_IceCream.TabIndex = 1
        Me.rbtn_IceCream.TabStop = False
        Me.rbtn_IceCream.Text = "Ice Cream"
        '
        'rbtn_Product
        '
        Me.rbtn_Product.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtn_Product.Location = New System.Drawing.Point(3, 4)
        Me.rbtn_Product.Name = "rbtn_Product"
        Me.rbtn_Product.Size = New System.Drawing.Size(59, 18)
        Me.rbtn_Product.TabIndex = 0
        Me.rbtn_Product.TabStop = False
        Me.rbtn_Product.Text = "Product"
        Me.rbtn_Product.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkIndividualCustomer
        '
        Me.chkIndividualCustomer.Location = New System.Drawing.Point(318, 134)
        Me.chkIndividualCustomer.Name = "chkIndividualCustomer"
        Me.chkIndividualCustomer.Size = New System.Drawing.Size(120, 18)
        Me.chkIndividualCustomer.TabIndex = 1483
        Me.chkIndividualCustomer.Text = "Individual Customer"
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = False
        Me.lblCityName.BorderVisible = True
        Me.lblCityName.FieldName = Nothing
        Me.lblCityName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCityName.Location = New System.Drawing.Point(212, 50)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(227, 18)
        Me.lblCityName.TabIndex = 1474
        Me.lblCityName.TextWrap = False
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1006, 292)
        Me.gv1.TabIndex = 0
        Me.gv1.VarID = ""
        '
        'btnPrintLoadinSlip
        '
        Me.btnPrintLoadinSlip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintLoadinSlip.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintLoadinSlip.Location = New System.Drawing.Point(497, 7)
        Me.btnPrintLoadinSlip.Name = "btnPrintLoadinSlip"
        Me.btnPrintLoadinSlip.Size = New System.Drawing.Size(142, 20)
        Me.btnPrintLoadinSlip.TabIndex = 25
        Me.btnPrintLoadinSlip.Text = "Print Loadin Slip"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(422, 8)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(69, 20)
        Me.btnHistory.TabIndex = 24
        Me.btnHistory.Text = "History"
        '
        'btnreverse
        '
        Me.btnreverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreverse.Location = New System.Drawing.Point(832, 8)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(87, 20)
        Me.btnreverse.TabIndex = 23
        Me.btnreverse.Text = "Reverse/Recreate of Demand"
        Me.btnreverse.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(925, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 20)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        '
        'btnAssessment
        '
        Me.btnAssessment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAssessment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssessment.Location = New System.Drawing.Point(297, 8)
        Me.btnAssessment.Name = "btnAssessment"
        Me.btnAssessment.Size = New System.Drawing.Size(123, 20)
        Me.btnAssessment.TabIndex = 17
        Me.btnAssessment.Text = "Assessment"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 20
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(149, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 18
        Me.btnDelete.Text = "Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(222, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 20)
        Me.btnPrint.TabIndex = 19
        Me.btnPrint.Text = "Print"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 20)
        Me.btnSave.TabIndex = 16
        Me.btnSave.Text = "Save"
        '
        'FrmProductDemandBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 513)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmProductDemandBooking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Product Demand "
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rgbProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbProduct.ResumeLayout(False)
        Me.rgbProduct.PerformLayout()
        CType(Me.lblPTotCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPTotAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustomersearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rbtn_IceCream, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_Product, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIndividualCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintLoadinSlip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAssessment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents btnSaveLayout As RadMenuItem
    Friend WithEvents btnLayout As RadMenuItem
    Friend WithEvents btnExport As RadMenuItem
    Friend WithEvents btnImport As RadMenuItem
    Friend WithEvents btnSendEmailSMS As RadMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents btnAssessment As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtcustomersearch As common.Controls.MyTextBox
    Friend WithEvents btnSearch As RadButton
    Friend WithEvents lblTransporterName As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents TxtCity As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents rbtn_IceCream As RadRadioButton
    Friend WithEvents rbtn_Product As RadRadioButton
    Friend WithEvents lblCityName As common.Controls.MyLabel
    Friend WithEvents chkIndividualCustomer As RadCheckBox
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustomerNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.UserControls.txtFinder
    Friend WithEvents rgbProduct As RadGroupBox
    Friend WithEvents lblPTotCount As common.Controls.MyLabel
    Friend WithEvents txtPCount As common.Controls.MyLabel
    Friend WithEvents lblPTotAmt As common.Controls.MyLabel
    Friend WithEvents txtPAmt As common.Controls.MyLabel
    Friend WithEvents btnreverse As RadButton
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents btnPrintLoadinSlip As RadButton
End Class
