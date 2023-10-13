Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmShareAllotment
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRate = New common.MyNumBox()
        Me.txtAmount = New common.MyNumBox()
        Me.txtNoOfShare = New common.MyNumBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblUploaderCode = New common.Controls.MyLabel()
        Me.lblName = New common.Controls.MyLabel()
        Me.lblRegistration = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndCertificate = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndShare = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblPINo = New common.Controls.MyLabel()
        Me.fndDCSCode = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfShare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblUploaderCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPINo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNoOfShare)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCertificate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndShare)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPINo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDCSCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 415
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 183)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel9.TabIndex = 1081
        Me.MyLabel9.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(110, 183)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(258, 56)
        Me.txtRemarks.TabIndex = 1080
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(349, 27)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(19, 20)
        Me.btnAddNew.TabIndex = 1079
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.White
        Me.txtRate.CalculationExpression = Nothing
        Me.txtRate.DecimalPlaces = 2
        Me.txtRate.FieldCode = Nothing
        Me.txtRate.FieldDesc = Nothing
        Me.txtRate.FieldMaxLength = 0
        Me.txtRate.FieldName = Nothing
        Me.txtRate.isCalculatedField = False
        Me.txtRate.IsSourceFromTable = False
        Me.txtRate.IsSourceFromValueList = False
        Me.txtRate.IsUnique = False
        Me.txtRate.Location = New System.Drawing.Point(110, 138)
        Me.txtRate.MendatroryField = False
        Me.txtRate.MyLinkLable1 = Nothing
        Me.txtRate.MyLinkLable2 = Nothing
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReferenceFieldDesc = Nothing
        Me.txtRate.ReferenceFieldName = Nothing
        Me.txtRate.ReferenceTableName = Nothing
        Me.txtRate.Size = New System.Drawing.Size(258, 20)
        Me.txtRate.TabIndex = 117
        Me.txtRate.Text = "0"
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRate.Value = 0R
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.White
        Me.txtAmount.CalculationExpression = Nothing
        Me.txtAmount.DecimalPlaces = 2
        Me.txtAmount.FieldCode = Nothing
        Me.txtAmount.FieldDesc = Nothing
        Me.txtAmount.FieldMaxLength = 0
        Me.txtAmount.FieldName = Nothing
        Me.txtAmount.isCalculatedField = False
        Me.txtAmount.IsSourceFromTable = False
        Me.txtAmount.IsSourceFromValueList = False
        Me.txtAmount.IsUnique = False
        Me.txtAmount.Location = New System.Drawing.Point(110, 160)
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Nothing
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReferenceFieldDesc = Nothing
        Me.txtAmount.ReferenceFieldName = Nothing
        Me.txtAmount.ReferenceTableName = Nothing
        Me.txtAmount.Size = New System.Drawing.Size(258, 20)
        Me.txtAmount.TabIndex = 117
        Me.txtAmount.Text = "0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmount.Value = 0R
        '
        'txtNoOfShare
        '
        Me.txtNoOfShare.BackColor = System.Drawing.Color.White
        Me.txtNoOfShare.CalculationExpression = Nothing
        Me.txtNoOfShare.DecimalPlaces = 2
        Me.txtNoOfShare.FieldCode = Nothing
        Me.txtNoOfShare.FieldDesc = Nothing
        Me.txtNoOfShare.FieldMaxLength = 0
        Me.txtNoOfShare.FieldName = Nothing
        Me.txtNoOfShare.isCalculatedField = False
        Me.txtNoOfShare.IsSourceFromTable = False
        Me.txtNoOfShare.IsSourceFromValueList = False
        Me.txtNoOfShare.IsUnique = False
        Me.txtNoOfShare.Location = New System.Drawing.Point(110, 93)
        Me.txtNoOfShare.MendatroryField = False
        Me.txtNoOfShare.MyLinkLable1 = Nothing
        Me.txtNoOfShare.MyLinkLable2 = Nothing
        Me.txtNoOfShare.Name = "txtNoOfShare"
        Me.txtNoOfShare.ReferenceFieldDesc = Nothing
        Me.txtNoOfShare.ReferenceFieldName = Nothing
        Me.txtNoOfShare.ReferenceTableName = Nothing
        Me.txtNoOfShare.Size = New System.Drawing.Size(258, 20)
        Me.txtNoOfShare.TabIndex = 1078
        Me.txtNoOfShare.Text = "0"
        Me.txtNoOfShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfShare.Value = 0R
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblUploaderCode)
        Me.RadGroupBox1.Controls.Add(Me.lblName)
        Me.RadGroupBox1.Controls.Add(Me.lblRegistration)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.HeaderText = "DCS Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(384, 46)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(404, 98)
        Me.RadGroupBox1.TabIndex = 1077
        Me.RadGroupBox1.Text = "DCS Details"
        '
        'lblUploaderCode
        '
        Me.lblUploaderCode.AutoSize = False
        Me.lblUploaderCode.BorderVisible = True
        Me.lblUploaderCode.FieldName = Nothing
        Me.lblUploaderCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblUploaderCode.Location = New System.Drawing.Point(100, 65)
        Me.lblUploaderCode.Name = "lblUploaderCode"
        Me.lblUploaderCode.Size = New System.Drawing.Size(288, 21)
        Me.lblUploaderCode.TabIndex = 7
        '
        'lblName
        '
        Me.lblName.AutoSize = False
        Me.lblName.BorderVisible = True
        Me.lblName.FieldName = Nothing
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblName.Location = New System.Drawing.Point(100, 41)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(288, 21)
        Me.lblName.TabIndex = 627
        '
        'lblRegistration
        '
        Me.lblRegistration.AutoSize = False
        Me.lblRegistration.BorderVisible = True
        Me.lblRegistration.FieldName = Nothing
        Me.lblRegistration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblRegistration.Location = New System.Drawing.Point(100, 16)
        Me.lblRegistration.Name = "lblRegistration"
        Me.lblRegistration.Size = New System.Drawing.Size(288, 21)
        Me.lblRegistration.TabIndex = 626
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(14, 65)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel8.TabIndex = 624
        Me.MyLabel8.Text = "Uploader Code"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(14, 43)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel7.TabIndex = 625
        Me.MyLabel7.Text = "Name"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(14, 21)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel6.TabIndex = 624
        Me.MyLabel6.Text = "Registration"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 161)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel5.TabIndex = 1075
        Me.MyLabel5.Text = "Amount"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(12, 140)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel4.TabIndex = 1073
        Me.MyLabel4.Text = "Rate"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 118)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 630
        Me.MyLabel3.Text = "Certificate"
        '
        'fndCertificate
        '
        Me.fndCertificate.arrDispalyMember = Nothing
        Me.fndCertificate.arrValueMember = Nothing
        Me.fndCertificate.Location = New System.Drawing.Point(110, 115)
        Me.fndCertificate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCertificate.MyLinkLable1 = Nothing
        Me.fndCertificate.MyLinkLable2 = Nothing
        Me.fndCertificate.MyNullText = "Please Select..."
        Me.fndCertificate.Name = "fndCertificate"
        Me.fndCertificate.Size = New System.Drawing.Size(258, 21)
        Me.fndCertificate.TabIndex = 1072
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 95)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel2.TabIndex = 629
        Me.MyLabel2.Text = "No Of Share"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(12, 72)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel1.TabIndex = 628
        Me.MyLabel1.Text = "Share"
        '
        'fndShare
        '
        Me.fndShare.CalculationExpression = Nothing
        Me.fndShare.FieldCode = Nothing
        Me.fndShare.FieldDesc = Nothing
        Me.fndShare.FieldMaxLength = 0
        Me.fndShare.FieldName = Nothing
        Me.fndShare.isCalculatedField = False
        Me.fndShare.IsSourceFromTable = False
        Me.fndShare.IsSourceFromValueList = False
        Me.fndShare.IsUnique = False
        Me.fndShare.Location = New System.Drawing.Point(110, 71)
        Me.fndShare.MendatroryField = False
        Me.fndShare.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndShare.MyLinkLable1 = Me.MyLabel1
        Me.fndShare.MyLinkLable2 = Nothing
        Me.fndShare.MyReadOnly = True
        Me.fndShare.MyShowMasterFormButton = False
        Me.fndShare.Name = "fndShare"
        Me.fndShare.ReferenceFieldDesc = Nothing
        Me.fndShare.ReferenceFieldName = Nothing
        Me.fndShare.ReferenceTableName = Nothing
        Me.fndShare.Size = New System.Drawing.Size(258, 20)
        Me.fndShare.TabIndex = 627
        Me.fndShare.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(698, 24)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 626
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel4.Location = New System.Drawing.Point(384, 29)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 625
        Me.RadLabel4.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(419, 28)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(101, 18)
        Me.txtDate.TabIndex = 624
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblPINo
        '
        Me.lblPINo.FieldName = Nothing
        Me.lblPINo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPINo.Location = New System.Drawing.Point(12, 51)
        Me.lblPINo.Name = "lblPINo"
        Me.lblPINo.Size = New System.Drawing.Size(60, 16)
        Me.lblPINo.TabIndex = 623
        Me.lblPINo.Text = "DCS Code"
        '
        'fndDCSCode
        '
        Me.fndDCSCode.CalculationExpression = Nothing
        Me.fndDCSCode.FieldCode = Nothing
        Me.fndDCSCode.FieldDesc = Nothing
        Me.fndDCSCode.FieldMaxLength = 0
        Me.fndDCSCode.FieldName = Nothing
        Me.fndDCSCode.isCalculatedField = False
        Me.fndDCSCode.IsSourceFromTable = False
        Me.fndDCSCode.IsSourceFromValueList = False
        Me.fndDCSCode.IsUnique = False
        Me.fndDCSCode.Location = New System.Drawing.Point(110, 49)
        Me.fndDCSCode.MendatroryField = False
        Me.fndDCSCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDCSCode.MyLinkLable1 = Me.lblPINo
        Me.fndDCSCode.MyLinkLable2 = Nothing
        Me.fndDCSCode.MyReadOnly = True
        Me.fndDCSCode.MyShowMasterFormButton = False
        Me.fndDCSCode.Name = "fndDCSCode"
        Me.fndDCSCode.ReferenceFieldDesc = Nothing
        Me.fndDCSCode.ReferenceFieldName = Nothing
        Me.fndDCSCode.ReferenceTableName = Nothing
        Me.fndDCSCode.Size = New System.Drawing.Size(258, 20)
        Me.fndDCSCode.TabIndex = 622
        Me.fndDCSCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(12, 29)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 621
        Me.RadLabel1.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(110, 27)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(238, 20)
        Me.txtCode.TabIndex = 620
        Me.txtCode.Value = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(724, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 22)
        Me.btnClose.TabIndex = 625
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(181, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(56, 22)
        Me.btnPrint.TabIndex = 624
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(124, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(56, 22)
        Me.btnDelete.TabIndex = 623
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(67, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(56, 22)
        Me.btnPost.TabIndex = 622
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(10, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(56, 22)
        Me.btnSave.TabIndex = 621
        Me.btnSave.Text = "Save"
        '
        'frmShareAllotment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmShareAllotment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmShareAllotment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfShare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblUploaderCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegistration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPINo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblPINo As common.Controls.MyLabel
    Friend WithEvents fndDCSCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndShare As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndCertificate As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblUploaderCode As common.Controls.MyLabel
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents lblRegistration As common.Controls.MyLabel
    Friend WithEvents txtRate As common.MyNumBox
    Friend WithEvents txtAmount As common.MyNumBox
    Friend WithEvents txtNoOfShare As common.MyNumBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
End Class
