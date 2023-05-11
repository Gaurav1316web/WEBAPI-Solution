<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmsaleReturnGateEntry
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkCancel = New System.Windows.Forms.CheckBox()
        Me.lblSR = New common.Controls.MyLabel()
        Me.txtmulticapex = New common.UserControls.txtMultiSelectFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtManualTransport = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.Remarks = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.lblTransportName = New common.Controls.MyLabel()
        Me.lblVehicleCode = New common.Controls.MyLabel()
        Me.txtManualVehicle = New common.Controls.MyTextBox()
        Me.lblManualVehicle = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.ddlDocType = New common.Controls.MyComboBox()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.txtTransporterCode = New common.UserControls.txtFinder()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.txtVehicleCode = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblSR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Remarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransportName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblManualVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1037, 421)
        Me.SplitContainer1.SplitterDistance = 371
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkCancel)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSR)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtmulticapex)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtManualTransport)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Remarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTransportName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVehicleCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtManualVehicle)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblManualVehicle)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel14)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVendorNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ddlDocType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel31)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtTransporterCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBillToLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBillToLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVehicleCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(1037, 371)
        Me.SplitContainer2.SplitterDistance = 166
        Me.SplitContainer2.TabIndex = 0
        '
        'chkCancel
        '
        Me.chkCancel.AutoSize = True
        Me.chkCancel.Location = New System.Drawing.Point(410, 21)
        Me.chkCancel.Name = "chkCancel"
        Me.chkCancel.Size = New System.Drawing.Size(60, 17)
        Me.chkCancel.TabIndex = 1457
        Me.chkCancel.Text = "Cancel"
        Me.chkCancel.UseVisualStyleBackColor = True
        '
        'lblSR
        '
        Me.lblSR.FieldName = Nothing
        Me.lblSR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSR.Location = New System.Drawing.Point(557, 66)
        Me.lblSR.Name = "lblSR"
        Me.lblSR.Size = New System.Drawing.Size(60, 16)
        Me.lblSR.TabIndex = 1456
        Me.lblSR.Text = "Invoice No"
        '
        'txtmulticapex
        '
        Me.txtmulticapex.arrDispalyMember = Nothing
        Me.txtmulticapex.arrValueMember = Nothing
        Me.txtmulticapex.Location = New System.Drawing.Point(642, 63)
        Me.txtmulticapex.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmulticapex.MyLinkLable1 = Nothing
        Me.txtmulticapex.MyLinkLable2 = Nothing
        Me.txtmulticapex.MyNullText = "All"
        Me.txtmulticapex.Name = "txtmulticapex"
        Me.txtmulticapex.Size = New System.Drawing.Size(380, 19)
        Me.txtmulticapex.TabIndex = 1455
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(928, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1453
        '
        'txtManualTransport
        '
        Me.txtManualTransport.CalculationExpression = Nothing
        Me.txtManualTransport.FieldCode = Nothing
        Me.txtManualTransport.FieldDesc = Nothing
        Me.txtManualTransport.FieldMaxLength = 0
        Me.txtManualTransport.FieldName = Nothing
        Me.txtManualTransport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualTransport.isCalculatedField = False
        Me.txtManualTransport.IsSourceFromTable = False
        Me.txtManualTransport.IsSourceFromValueList = False
        Me.txtManualTransport.IsUnique = False
        Me.txtManualTransport.Location = New System.Drawing.Point(115, 133)
        Me.txtManualTransport.MaxLength = 1000
        Me.txtManualTransport.MendatroryField = True
        Me.txtManualTransport.MyLinkLable1 = Nothing
        Me.txtManualTransport.MyLinkLable2 = Nothing
        Me.txtManualTransport.Name = "txtManualTransport"
        Me.txtManualTransport.ReferenceFieldDesc = Nothing
        Me.txtManualTransport.ReferenceFieldName = Nothing
        Me.txtManualTransport.ReferenceTableName = Nothing
        Me.txtManualTransport.Size = New System.Drawing.Size(436, 18)
        Me.txtManualTransport.TabIndex = 15
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(12, 133)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel1.TabIndex = 1451
        Me.MyLabel1.Text = "Manual Transport"
        '
        'Remarks
        '
        Me.Remarks.FieldName = Nothing
        Me.Remarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Remarks.Location = New System.Drawing.Point(556, 128)
        Me.Remarks.Name = "Remarks"
        Me.Remarks.Size = New System.Drawing.Size(51, 16)
        Me.Remarks.TabIndex = 1450
        Me.Remarks.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(642, 127)
        Me.txtRemarks.MaxLength = 1000
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.Remarks
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(383, 18)
        Me.txtRemarks.TabIndex = 10
        '
        'lblTransportName
        '
        Me.lblTransportName.AutoSize = False
        Me.lblTransportName.BorderVisible = True
        Me.lblTransportName.FieldName = Nothing
        Me.lblTransportName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTransportName.Location = New System.Drawing.Point(260, 112)
        Me.lblTransportName.Name = "lblTransportName"
        Me.lblTransportName.Size = New System.Drawing.Size(287, 18)
        Me.lblTransportName.TabIndex = 14
        Me.lblTransportName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTransportName.TextWrap = False
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.AutoSize = False
        Me.lblVehicleCode.BorderVisible = True
        Me.lblVehicleCode.FieldName = Nothing
        Me.lblVehicleCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVehicleCode.Location = New System.Drawing.Point(260, 43)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(287, 18)
        Me.lblVehicleCode.TabIndex = 5
        Me.lblVehicleCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVehicleCode.TextWrap = False
        '
        'txtManualVehicle
        '
        Me.txtManualVehicle.CalculationExpression = Nothing
        Me.txtManualVehicle.FieldCode = Nothing
        Me.txtManualVehicle.FieldDesc = Nothing
        Me.txtManualVehicle.FieldMaxLength = 0
        Me.txtManualVehicle.FieldName = Nothing
        Me.txtManualVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualVehicle.isCalculatedField = False
        Me.txtManualVehicle.IsSourceFromTable = False
        Me.txtManualVehicle.IsSourceFromValueList = False
        Me.txtManualVehicle.IsUnique = False
        Me.txtManualVehicle.Location = New System.Drawing.Point(115, 65)
        Me.txtManualVehicle.MaxLength = 1000
        Me.txtManualVehicle.MendatroryField = True
        Me.txtManualVehicle.MyLinkLable1 = Nothing
        Me.txtManualVehicle.MyLinkLable2 = Nothing
        Me.txtManualVehicle.Name = "txtManualVehicle"
        Me.txtManualVehicle.ReferenceFieldDesc = Nothing
        Me.txtManualVehicle.ReferenceFieldName = Nothing
        Me.txtManualVehicle.ReferenceTableName = Nothing
        Me.txtManualVehicle.Size = New System.Drawing.Size(436, 18)
        Me.txtManualVehicle.TabIndex = 7
        '
        'lblManualVehicle
        '
        Me.lblManualVehicle.FieldName = Nothing
        Me.lblManualVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManualVehicle.Location = New System.Drawing.Point(12, 65)
        Me.lblManualVehicle.Name = "lblManualVehicle"
        Me.lblManualVehicle.Size = New System.Drawing.Size(84, 16)
        Me.lblManualVehicle.TabIndex = 1445
        Me.lblManualVehicle.Text = "Manual Vehicle"
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(556, 109)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel14.TabIndex = 1416
        Me.RadLabel14.Text = "Comment"
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(642, 108)
        Me.txtComment.MaxLength = 1000
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel14
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(383, 18)
        Me.txtComment.TabIndex = 12
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel2.Location = New System.Drawing.Point(556, 87)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 1414
        Me.RadLabel2.Text = "Customer No"
        '
        'txtVendorNo
        '
        Me.txtVendorNo.CalculationExpression = Nothing
        Me.txtVendorNo.FieldCode = Nothing
        Me.txtVendorNo.FieldDesc = Nothing
        Me.txtVendorNo.FieldMaxLength = 0
        Me.txtVendorNo.FieldName = Nothing
        Me.txtVendorNo.isCalculatedField = False
        Me.txtVendorNo.IsSourceFromTable = False
        Me.txtVendorNo.IsSourceFromValueList = False
        Me.txtVendorNo.IsUnique = False
        Me.txtVendorNo.Location = New System.Drawing.Point(642, 85)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(159, 20)
        Me.txtVendorNo.TabIndex = 8
        Me.txtVendorNo.Value = ""
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVendorName.Location = New System.Drawing.Point(807, 86)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(218, 18)
        Me.lblVendorName.TabIndex = 9
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.TextWrap = False
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(556, 42)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel18.TabIndex = 1410
        Me.MyLabel18.Text = "Doc Type"
        '
        'ddlDocType
        '
        Me.ddlDocType.AutoCompleteDisplayMember = Nothing
        Me.ddlDocType.AutoCompleteValueMember = Nothing
        Me.ddlDocType.CalculationExpression = Nothing
        Me.ddlDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlDocType.FieldCode = Nothing
        Me.ddlDocType.FieldDesc = Nothing
        Me.ddlDocType.FieldMaxLength = 0
        Me.ddlDocType.FieldName = Nothing
        Me.ddlDocType.isCalculatedField = False
        Me.ddlDocType.IsSourceFromTable = False
        Me.ddlDocType.IsSourceFromValueList = False
        Me.ddlDocType.IsUnique = False
        Me.ddlDocType.Location = New System.Drawing.Point(642, 40)
        Me.ddlDocType.MendatroryField = True
        Me.ddlDocType.MyLinkLable1 = Nothing
        Me.ddlDocType.MyLinkLable2 = Nothing
        Me.ddlDocType.Name = "ddlDocType"
        Me.ddlDocType.ReferenceFieldDesc = Nothing
        Me.ddlDocType.ReferenceFieldName = Nothing
        Me.ddlDocType.ReferenceTableName = Nothing
        Me.ddlDocType.Size = New System.Drawing.Size(159, 20)
        Me.ddlDocType.TabIndex = 6
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel31.Location = New System.Drawing.Point(12, 111)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel31.TabIndex = 1408
        Me.MyLabel31.Text = "Transport"
        '
        'txtTransporterCode
        '
        Me.txtTransporterCode.CalculationExpression = Nothing
        Me.txtTransporterCode.FieldCode = Nothing
        Me.txtTransporterCode.FieldDesc = Nothing
        Me.txtTransporterCode.FieldMaxLength = 0
        Me.txtTransporterCode.FieldName = Nothing
        Me.txtTransporterCode.isCalculatedField = False
        Me.txtTransporterCode.IsSourceFromTable = False
        Me.txtTransporterCode.IsSourceFromValueList = False
        Me.txtTransporterCode.IsUnique = False
        Me.txtTransporterCode.Location = New System.Drawing.Point(115, 110)
        Me.txtTransporterCode.MendatroryField = True
        Me.txtTransporterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterCode.MyLinkLable1 = Me.MyLabel31
        Me.txtTransporterCode.MyLinkLable2 = Nothing
        Me.txtTransporterCode.MyReadOnly = False
        Me.txtTransporterCode.MyShowMasterFormButton = False
        Me.txtTransporterCode.Name = "txtTransporterCode"
        Me.txtTransporterCode.ReferenceFieldDesc = Nothing
        Me.txtTransporterCode.ReferenceFieldName = Nothing
        Me.txtTransporterCode.ReferenceTableName = Nothing
        Me.txtTransporterCode.Size = New System.Drawing.Size(143, 20)
        Me.txtTransporterCode.TabIndex = 13
        Me.txtTransporterCode.Value = ""
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBillToLocation.Location = New System.Drawing.Point(260, 89)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(287, 18)
        Me.lblBillToLocation.TabIndex = 11
        Me.lblBillToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBillToLocation.TextWrap = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel15.Location = New System.Drawing.Point(12, 89)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 154
        Me.RadLabel15.Text = "Location"
        '
        'txtBillToLocation
        '
        Me.txtBillToLocation.CalculationExpression = Nothing
        Me.txtBillToLocation.FieldCode = Nothing
        Me.txtBillToLocation.FieldDesc = Nothing
        Me.txtBillToLocation.FieldMaxLength = 0
        Me.txtBillToLocation.FieldName = Nothing
        Me.txtBillToLocation.isCalculatedField = False
        Me.txtBillToLocation.IsSourceFromTable = False
        Me.txtBillToLocation.IsSourceFromValueList = False
        Me.txtBillToLocation.IsUnique = False
        Me.txtBillToLocation.Location = New System.Drawing.Point(115, 87)
        Me.txtBillToLocation.MendatroryField = True
        Me.txtBillToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillToLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtBillToLocation.MyLinkLable2 = Me.lblBillToLocation
        Me.txtBillToLocation.MyReadOnly = False
        Me.txtBillToLocation.MyShowMasterFormButton = False
        Me.txtBillToLocation.Name = "txtBillToLocation"
        Me.txtBillToLocation.ReferenceFieldDesc = Nothing
        Me.txtBillToLocation.ReferenceFieldName = Nothing
        Me.txtBillToLocation.ReferenceTableName = Nothing
        Me.txtBillToLocation.Size = New System.Drawing.Size(143, 20)
        Me.txtBillToLocation.TabIndex = 10
        Me.txtBillToLocation.Value = ""
        '
        'txtVehicleCode
        '
        Me.txtVehicleCode.CalculationExpression = Nothing
        Me.txtVehicleCode.FieldCode = Nothing
        Me.txtVehicleCode.FieldDesc = Nothing
        Me.txtVehicleCode.FieldMaxLength = 0
        Me.txtVehicleCode.FieldName = Nothing
        Me.txtVehicleCode.isCalculatedField = False
        Me.txtVehicleCode.IsSourceFromTable = False
        Me.txtVehicleCode.IsSourceFromValueList = False
        Me.txtVehicleCode.IsUnique = False
        Me.txtVehicleCode.Location = New System.Drawing.Point(115, 43)
        Me.txtVehicleCode.MendatroryField = True
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.MyLabel10
        Me.txtVehicleCode.MyLinkLable2 = Nothing
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.MyShowMasterFormButton = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.ReferenceFieldDesc = Nothing
        Me.txtVehicleCode.ReferenceFieldName = Nothing
        Me.txtVehicleCode.ReferenceTableName = Nothing
        Me.txtVehicleCode.Size = New System.Drawing.Size(143, 19)
        Me.txtVehicleCode.TabIndex = 4
        Me.txtVehicleCode.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(12, 45)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel10.TabIndex = 150
        Me.MyLabel10.Text = "Vehicle Code"
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
        Me.txtDate.Location = New System.Drawing.Point(642, 19)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(159, 18)
        Me.txtDate.TabIndex = 3
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel4.Location = New System.Drawing.Point(556, 21)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel4.TabIndex = 48
        Me.RadLabel4.Text = "Gate Entry Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 21)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel1.TabIndex = 50
        Me.RadLabel1.Text = "Gate Entry no."
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(115, 19)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 1
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(369, 19)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 2
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1037, 201)
        Me.RadGroupBox2.TabIndex = 41
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1017, 171)
        Me.gv1.TabIndex = 16
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(224, 14)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(69, 20)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(958, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 20)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(293, 14)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 20)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(155, 14)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(86, 14)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(17, 14)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 20)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        '
        'FrmsaleReturnGateEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1037, 421)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmsaleReturnGateEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmsaleReturnGateEntry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblSR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Remarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransportName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblManualVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblBillToLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents txtTransporterCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents ddlDocType As common.Controls.MyComboBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents txtManualVehicle As common.Controls.MyTextBox
    Friend WithEvents lblManualVehicle As common.Controls.MyLabel
    Friend WithEvents lblTransportName As common.Controls.MyLabel
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents Remarks As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtManualTransport As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtmulticapex As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblSR As common.Controls.MyLabel
    Friend WithEvents chkCancel As System.Windows.Forms.CheckBox
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
End Class

