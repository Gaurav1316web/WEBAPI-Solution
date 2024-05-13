<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDCSDemandBooking
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendEmailSMS = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkIndividualCustomer = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.lblCustomerCode = New common.Controls.MyLabel()
        Me.txtCustomerNo = New common.UserControls.txtFinder()
        Me.txtVehicleNo = New Telerik.WinControls.UI.RadTextBox()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.rbtnOther = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.rbtnGhee = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnCatelFeed = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnreverse = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkIndividualCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnGhee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCatelFeed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(916, 20)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(916, 456)
        Me.SplitContainer1.SplitterDistance = 421
        Me.SplitContainer1.TabIndex = 9
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkIndividualCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCustomerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVehicleNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVehicleNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnOther)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnGhee)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCatelFeed)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRouteNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRouteNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(916, 421)
        Me.SplitContainer2.SplitterDistance = 95
        Me.SplitContainer2.TabIndex = 0
        '
        'chkIndividualCustomer
        '
        Me.chkIndividualCustomer.Location = New System.Drawing.Point(414, 29)
        Me.chkIndividualCustomer.Name = "chkIndividualCustomer"
        Me.chkIndividualCustomer.Size = New System.Drawing.Size(120, 18)
        Me.chkIndividualCustomer.TabIndex = 1475
        Me.chkIndividualCustomer.Text = "Individual Customer"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(590, 52)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(196, 18)
        Me.lblCustomerName.TabIndex = 1473
        Me.lblCustomerName.TextWrap = False
        '
        'lblCustomerCode
        '
        Me.lblCustomerCode.FieldName = Nothing
        Me.lblCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerCode.Location = New System.Drawing.Point(414, 52)
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(55, 16)
        Me.lblCustomerCode.TabIndex = 1474
        Me.lblCustomerCode.Text = "Customer"
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
        Me.txtCustomerNo.Location = New System.Drawing.Point(469, 51)
        Me.txtCustomerNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerNo.MendatroryField = False
        Me.txtCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerNo.MyLinkLable1 = Me.lblCustomerCode
        Me.txtCustomerNo.MyLinkLable2 = Me.lblCustomerName
        Me.txtCustomerNo.MyReadOnly = False
        Me.txtCustomerNo.MyShowMasterFormButton = False
        Me.txtCustomerNo.Name = "txtCustomerNo"
        Me.txtCustomerNo.ReferenceFieldDesc = Nothing
        Me.txtCustomerNo.ReferenceFieldName = Nothing
        Me.txtCustomerNo.ReferenceTableName = Nothing
        Me.txtCustomerNo.Size = New System.Drawing.Size(115, 19)
        Me.txtCustomerNo.TabIndex = 1472
        Me.txtCustomerNo.Value = ""
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.Location = New System.Drawing.Point(91, 68)
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.Size = New System.Drawing.Size(115, 20)
        Me.txtVehicleNo.TabIndex = 1471
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Location = New System.Drawing.Point(12, 69)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(60, 18)
        Me.lblVehicleNo.TabIndex = 1470
        Me.lblVehicleNo.Text = "Vehicle No"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDesc.Location = New System.Drawing.Point(212, 48)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(196, 18)
        Me.lblLocationDesc.TabIndex = 1469
        Me.lblLocationDesc.TextWrap = False
        '
        'rbtnOther
        '
        Me.rbtnOther.Location = New System.Drawing.Point(856, 8)
        Me.rbtnOther.Name = "rbtnOther"
        Me.rbtnOther.Size = New System.Drawing.Size(49, 18)
        Me.rbtnOther.TabIndex = 1469
        Me.rbtnOther.Text = "Other"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(12, 48)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 1468
        Me.lblLocation.Text = "Location"
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
        Me.txtLocation.Location = New System.Drawing.Point(91, 48)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(115, 19)
        Me.txtLocation.TabIndex = 1467
        Me.txtLocation.Value = ""
        '
        'rbtnGhee
        '
        Me.rbtnGhee.Location = New System.Drawing.Point(804, 8)
        Me.rbtnGhee.Name = "rbtnGhee"
        Me.rbtnGhee.Size = New System.Drawing.Size(46, 18)
        Me.rbtnGhee.TabIndex = 1469
        Me.rbtnGhee.Text = "Ghee"
        '
        'rbtnCatelFeed
        '
        Me.rbtnCatelFeed.Location = New System.Drawing.Point(725, 7)
        Me.rbtnCatelFeed.Name = "rbtnCatelFeed"
        Me.rbtnCatelFeed.Size = New System.Drawing.Size(73, 18)
        Me.rbtnCatelFeed.TabIndex = 1468
        Me.rbtnCatelFeed.Text = "Catel Feed"
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.Location = New System.Drawing.Point(12, 6)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(44, 16)
        Me.lblDocNo.TabIndex = 1462
        Me.lblDocNo.Text = "Doc No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(91, 4)
        Me.txtDocNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblDocNo
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(297, 20)
        Me.txtDocNo.TabIndex = 1459
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(593, 5)
        Me.UsLock1.Margin = New System.Windows.Forms.Padding(4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1467
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
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteDesc.Location = New System.Drawing.Point(212, 27)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(196, 18)
        Me.lblRouteDesc.TabIndex = 1466
        Me.lblRouteDesc.TextWrap = False
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
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(12, 27)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 1465
        Me.lblRouteNo.Text = "Route No"
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
        Me.txtRouteNo.Location = New System.Drawing.Point(91, 27)
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
        Me.txtRouteNo.TabIndex = 1464
        Me.txtRouteNo.Value = ""
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(916, 322)
        Me.gv1.TabIndex = 0
        '
        'btnreverse
        '
        Me.btnreverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnreverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreverse.Location = New System.Drawing.Point(769, 6)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(69, 20)
        Me.btnreverse.TabIndex = 20
        Me.btnreverse.Text = "Reverse"
        Me.btnreverse.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(844, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 20)
        Me.btnClose.TabIndex = 19
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(76, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 18
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(149, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 17
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 20)
        Me.btnSave.TabIndex = 16
        Me.btnSave.Text = "Save"
        '
        'frmDCSDemandBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 476)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmDCSDemandBooking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "DCS Demand Booking"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkIndividualCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnGhee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCatelFeed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents rbtnOther As RadRadioButton
    Friend WithEvents rbtnGhee As RadRadioButton
    Friend WithEvents rbtnCatelFeed As RadRadioButton
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents gv1 As RadGridView
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnreverse As RadButton
    Friend WithEvents txtVehicleNo As RadTextBox
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents chkIndividualCustomer As RadCheckBox
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents lblCustomerCode As common.Controls.MyLabel
    Friend WithEvents txtCustomerNo As common.UserControls.txtFinder
End Class
