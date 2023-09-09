<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEInvoiceHead
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
        Me.btnCopy = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtLocDes = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtVendorName = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtReqFor = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtGSTin = New common.Controls.MyTextBox()
        Me.Mylabel = New common.Controls.MyLabel()
        Me.txtClientSecret = New common.Controls.MyTextBox()
        Me.txtUrl = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtClientID = New common.Controls.MyTextBox()
        Me.txtUserName = New common.Controls.MyTextBox()
        Me.txtPassword = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtIPAddr = New common.Controls.MyTextBox()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnViewDocument = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocDes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReqFor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGSTin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mylabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClientSecret, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUrl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClientID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIPAddr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewDocument, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCopy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocDes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVendorName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReqFor)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGSTin)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Mylabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtClientSecret)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtUrl)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtClientID)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtUserName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtIPAddr)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.CausesValidation = False
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewDocument)
        Me.SplitContainer1.Size = New System.Drawing.Size(666, 440)
        Me.SplitContainer1.SplitterDistance = 385
        Me.SplitContainer1.TabIndex = 0
        '
        'btnCopy
        '
        Me.btnCopy.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCopy.Location = New System.Drawing.Point(486, 12)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(21, 27)
        Me.btnCopy.TabIndex = 1087
        Me.btnCopy.Text = "CC"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(164, 12)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(300, 27)
        Me.txtCode.TabIndex = 88
        Me.txtCode.Value = ""
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
        Me.txtLocation.Location = New System.Drawing.Point(164, 329)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel9
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(194, 25)
        Me.txtLocation.TabIndex = 87
        Me.txtLocation.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(38, 205)
        Me.MyLabel9.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel9.TabIndex = 70
        Me.MyLabel9.Text = "Client Secret"
        '
        'txtLocDes
        '
        Me.txtLocDes.CalculationExpression = Nothing
        Me.txtLocDes.FieldCode = Nothing
        Me.txtLocDes.FieldDesc = Nothing
        Me.txtLocDes.FieldMaxLength = 0
        Me.txtLocDes.FieldName = Nothing
        Me.txtLocDes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocDes.isCalculatedField = False
        Me.txtLocDes.IsSourceFromTable = False
        Me.txtLocDes.IsSourceFromValueList = False
        Me.txtLocDes.IsUnique = False
        Me.txtLocDes.Location = New System.Drawing.Point(367, 328)
        Me.txtLocDes.MaxLength = 200
        Me.txtLocDes.MendatroryField = False
        Me.txtLocDes.MyLinkLable1 = Me.MyLabel1
        Me.txtLocDes.MyLinkLable2 = Nothing
        Me.txtLocDes.Name = "txtLocDes"
        Me.txtLocDes.ReadOnly = True
        Me.txtLocDes.ReferenceFieldDesc = Nothing
        Me.txtLocDes.ReferenceFieldName = Nothing
        Me.txtLocDes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtLocDes.RootElement.StretchVertically = True
        Me.txtLocDes.Size = New System.Drawing.Size(141, 25)
        Me.txtLocDes.TabIndex = 80
        Me.txtLocDes.WordWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(38, 172)
        Me.MyLabel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel1.TabIndex = 69
        Me.MyLabel1.Text = "Client ID"
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(465, 12)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(19, 27)
        Me.btnNew.TabIndex = 79
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(38, 21)
        Me.MyLabel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel2.TabIndex = 75
        Me.MyLabel2.Text = "Code"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(38, 45)
        Me.MyLabel14.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(21, 16)
        Me.MyLabel14.TabIndex = 65
        Me.MyLabel14.Text = "Url"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(38, 303)
        Me.MyLabel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel4.TabIndex = 73
        Me.MyLabel4.Text = "Vendor Name"
        '
        'txtVendorName
        '
        Me.txtVendorName.CalculationExpression = Nothing
        Me.txtVendorName.FieldCode = Nothing
        Me.txtVendorName.FieldDesc = Nothing
        Me.txtVendorName.FieldMaxLength = 0
        Me.txtVendorName.FieldName = Nothing
        Me.txtVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorName.isCalculatedField = False
        Me.txtVendorName.IsSourceFromTable = False
        Me.txtVendorName.IsSourceFromValueList = False
        Me.txtVendorName.IsUnique = False
        Me.txtVendorName.Location = New System.Drawing.Point(164, 298)
        Me.txtVendorName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtVendorName.MaxLength = 200
        Me.txtVendorName.MendatroryField = False
        Me.txtVendorName.MyLinkLable1 = Nothing
        Me.txtVendorName.MyLinkLable2 = Nothing
        Me.txtVendorName.Name = "txtVendorName"
        Me.txtVendorName.ReferenceFieldDesc = Nothing
        Me.txtVendorName.ReferenceFieldName = Nothing
        Me.txtVendorName.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtVendorName.RootElement.StretchVertically = True
        Me.txtVendorName.Size = New System.Drawing.Size(344, 19)
        Me.txtVendorName.TabIndex = 63
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(38, 272)
        Me.MyLabel6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel6.TabIndex = 72
        Me.MyLabel6.Text = "Required For"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(38, 239)
        Me.MyLabel13.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel13.TabIndex = 71
        Me.MyLabel13.Text = "GSTin"
        '
        'txtReqFor
        '
        Me.txtReqFor.CalculationExpression = Nothing
        Me.txtReqFor.FieldCode = Nothing
        Me.txtReqFor.FieldDesc = Nothing
        Me.txtReqFor.FieldMaxLength = 0
        Me.txtReqFor.FieldName = Nothing
        Me.txtReqFor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqFor.isCalculatedField = False
        Me.txtReqFor.IsSourceFromTable = False
        Me.txtReqFor.IsSourceFromValueList = False
        Me.txtReqFor.IsUnique = False
        Me.txtReqFor.Location = New System.Drawing.Point(164, 269)
        Me.txtReqFor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtReqFor.MaxLength = 200
        Me.txtReqFor.MendatroryField = False
        Me.txtReqFor.MyLinkLable1 = Nothing
        Me.txtReqFor.MyLinkLable2 = Nothing
        Me.txtReqFor.Name = "txtReqFor"
        Me.txtReqFor.ReferenceFieldDesc = Nothing
        Me.txtReqFor.ReferenceFieldName = Nothing
        Me.txtReqFor.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtReqFor.RootElement.StretchVertically = True
        Me.txtReqFor.Size = New System.Drawing.Size(344, 19)
        Me.txtReqFor.TabIndex = 62
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(38, 109)
        Me.MyLabel10.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel10.TabIndex = 67
        Me.MyLabel10.Text = "Password"
        '
        'txtGSTin
        '
        Me.txtGSTin.CalculationExpression = Nothing
        Me.txtGSTin.FieldCode = Nothing
        Me.txtGSTin.FieldDesc = Nothing
        Me.txtGSTin.FieldMaxLength = 0
        Me.txtGSTin.FieldName = Nothing
        Me.txtGSTin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTin.isCalculatedField = False
        Me.txtGSTin.IsSourceFromTable = False
        Me.txtGSTin.IsSourceFromValueList = False
        Me.txtGSTin.IsUnique = False
        Me.txtGSTin.Location = New System.Drawing.Point(164, 236)
        Me.txtGSTin.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGSTin.MaxLength = 200
        Me.txtGSTin.MendatroryField = False
        Me.txtGSTin.MyLinkLable1 = Nothing
        Me.txtGSTin.MyLinkLable2 = Nothing
        Me.txtGSTin.Name = "txtGSTin"
        Me.txtGSTin.ReferenceFieldDesc = Nothing
        Me.txtGSTin.ReferenceFieldName = Nothing
        Me.txtGSTin.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtGSTin.RootElement.StretchVertically = True
        Me.txtGSTin.Size = New System.Drawing.Size(344, 19)
        Me.txtGSTin.TabIndex = 61
        '
        'Mylabel
        '
        Me.Mylabel.FieldName = Nothing
        Me.Mylabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Mylabel.Location = New System.Drawing.Point(38, 338)
        Me.Mylabel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Mylabel.Name = "Mylabel"
        Me.Mylabel.Size = New System.Drawing.Size(79, 16)
        Me.Mylabel.TabIndex = 74
        Me.Mylabel.Text = "Location Code"
        '
        'txtClientSecret
        '
        Me.txtClientSecret.CalculationExpression = Nothing
        Me.txtClientSecret.FieldCode = Nothing
        Me.txtClientSecret.FieldDesc = Nothing
        Me.txtClientSecret.FieldMaxLength = 0
        Me.txtClientSecret.FieldName = Nothing
        Me.txtClientSecret.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClientSecret.isCalculatedField = False
        Me.txtClientSecret.IsSourceFromTable = False
        Me.txtClientSecret.IsSourceFromValueList = False
        Me.txtClientSecret.IsUnique = False
        Me.txtClientSecret.Location = New System.Drawing.Point(164, 205)
        Me.txtClientSecret.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtClientSecret.MaxLength = 200
        Me.txtClientSecret.MendatroryField = False
        Me.txtClientSecret.MyLinkLable1 = Nothing
        Me.txtClientSecret.MyLinkLable2 = Nothing
        Me.txtClientSecret.Name = "txtClientSecret"
        Me.txtClientSecret.ReferenceFieldDesc = Nothing
        Me.txtClientSecret.ReferenceFieldName = Nothing
        Me.txtClientSecret.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtClientSecret.RootElement.StretchVertically = True
        Me.txtClientSecret.Size = New System.Drawing.Size(344, 19)
        Me.txtClientSecret.TabIndex = 60
        '
        'txtUrl
        '
        Me.txtUrl.CalculationExpression = Nothing
        Me.txtUrl.FieldCode = Nothing
        Me.txtUrl.FieldDesc = Nothing
        Me.txtUrl.FieldMaxLength = 0
        Me.txtUrl.FieldName = Nothing
        Me.txtUrl.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrl.isCalculatedField = False
        Me.txtUrl.IsSourceFromTable = False
        Me.txtUrl.IsSourceFromValueList = False
        Me.txtUrl.IsUnique = False
        Me.txtUrl.Location = New System.Drawing.Point(164, 45)
        Me.txtUrl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUrl.MaxLength = 200
        Me.txtUrl.MendatroryField = False
        Me.txtUrl.MyLinkLable1 = Nothing
        Me.txtUrl.MyLinkLable2 = Nothing
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.ReferenceFieldDesc = Nothing
        Me.txtUrl.ReferenceFieldName = Nothing
        Me.txtUrl.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtUrl.RootElement.StretchVertically = True
        Me.txtUrl.Size = New System.Drawing.Size(344, 19)
        Me.txtUrl.TabIndex = 55
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(38, 78)
        Me.MyLabel8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel8.TabIndex = 66
        Me.MyLabel8.Text = "User Name"
        '
        'txtClientID
        '
        Me.txtClientID.CalculationExpression = Nothing
        Me.txtClientID.FieldCode = Nothing
        Me.txtClientID.FieldDesc = Nothing
        Me.txtClientID.FieldMaxLength = 0
        Me.txtClientID.FieldName = Nothing
        Me.txtClientID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClientID.isCalculatedField = False
        Me.txtClientID.IsSourceFromTable = False
        Me.txtClientID.IsSourceFromValueList = False
        Me.txtClientID.IsUnique = False
        Me.txtClientID.Location = New System.Drawing.Point(164, 172)
        Me.txtClientID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtClientID.MaxLength = 200
        Me.txtClientID.MendatroryField = False
        Me.txtClientID.MyLinkLable1 = Nothing
        Me.txtClientID.MyLinkLable2 = Nothing
        Me.txtClientID.Name = "txtClientID"
        Me.txtClientID.ReferenceFieldDesc = Nothing
        Me.txtClientID.ReferenceFieldName = Nothing
        Me.txtClientID.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtClientID.RootElement.StretchVertically = True
        Me.txtClientID.Size = New System.Drawing.Size(344, 19)
        Me.txtClientID.TabIndex = 59
        '
        'txtUserName
        '
        Me.txtUserName.CalculationExpression = Nothing
        Me.txtUserName.FieldCode = Nothing
        Me.txtUserName.FieldDesc = Nothing
        Me.txtUserName.FieldMaxLength = 0
        Me.txtUserName.FieldName = Nothing
        Me.txtUserName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.isCalculatedField = False
        Me.txtUserName.IsSourceFromTable = False
        Me.txtUserName.IsSourceFromValueList = False
        Me.txtUserName.IsUnique = False
        Me.txtUserName.Location = New System.Drawing.Point(164, 75)
        Me.txtUserName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUserName.MaxLength = 200
        Me.txtUserName.MendatroryField = False
        Me.txtUserName.MyLinkLable1 = Nothing
        Me.txtUserName.MyLinkLable2 = Nothing
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReferenceFieldDesc = Nothing
        Me.txtUserName.ReferenceFieldName = Nothing
        Me.txtUserName.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtUserName.RootElement.StretchVertically = True
        Me.txtUserName.Size = New System.Drawing.Size(344, 19)
        Me.txtUserName.TabIndex = 56
        '
        'txtPassword
        '
        Me.txtPassword.CalculationExpression = Nothing
        Me.txtPassword.FieldCode = Nothing
        Me.txtPassword.FieldDesc = Nothing
        Me.txtPassword.FieldMaxLength = 0
        Me.txtPassword.FieldName = Nothing
        Me.txtPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.isCalculatedField = False
        Me.txtPassword.IsSourceFromTable = False
        Me.txtPassword.IsSourceFromValueList = False
        Me.txtPassword.IsUnique = False
        Me.txtPassword.Location = New System.Drawing.Point(164, 109)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPassword.MaxLength = 200
        Me.txtPassword.MendatroryField = False
        Me.txtPassword.MyLinkLable1 = Nothing
        Me.txtPassword.MyLinkLable2 = Nothing
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.ReferenceFieldDesc = Nothing
        Me.txtPassword.ReferenceFieldName = Nothing
        Me.txtPassword.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtPassword.RootElement.StretchVertically = True
        Me.txtPassword.Size = New System.Drawing.Size(344, 19)
        Me.txtPassword.TabIndex = 57
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(38, 143)
        Me.MyLabel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel3.TabIndex = 68
        Me.MyLabel3.Text = "IP Address"
        '
        'txtIPAddr
        '
        Me.txtIPAddr.CalculationExpression = Nothing
        Me.txtIPAddr.FieldCode = Nothing
        Me.txtIPAddr.FieldDesc = Nothing
        Me.txtIPAddr.FieldMaxLength = 0
        Me.txtIPAddr.FieldName = Nothing
        Me.txtIPAddr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIPAddr.isCalculatedField = False
        Me.txtIPAddr.IsSourceFromTable = False
        Me.txtIPAddr.IsSourceFromValueList = False
        Me.txtIPAddr.IsUnique = False
        Me.txtIPAddr.Location = New System.Drawing.Point(164, 143)
        Me.txtIPAddr.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIPAddr.MaxLength = 200
        Me.txtIPAddr.MendatroryField = False
        Me.txtIPAddr.MyLinkLable1 = Nothing
        Me.txtIPAddr.MyLinkLable2 = Nothing
        Me.txtIPAddr.Name = "txtIPAddr"
        Me.txtIPAddr.ReferenceFieldDesc = Nothing
        Me.txtIPAddr.ReferenceFieldName = Nothing
        Me.txtIPAddr.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtIPAddr.RootElement.StretchVertically = True
        Me.txtIPAddr.Size = New System.Drawing.Size(344, 19)
        Me.txtIPAddr.TabIndex = 58
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(108, 7)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(60, 21)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        '
        'btnViewDocument
        '
        Me.btnViewDocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnViewDocument.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewDocument.Location = New System.Drawing.Point(12, 7)
        Me.btnViewDocument.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnViewDocument.Name = "btnViewDocument"
        Me.btnViewDocument.Size = New System.Drawing.Size(90, 21)
        Me.btnViewDocument.TabIndex = 1
        Me.btnViewDocument.Text = "View Document"
        '
        'frmEInvoiceHead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 440)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEInvoiceHead"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "EInvoiceHead"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocDes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReqFor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGSTin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mylabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClientSecret, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUrl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClientID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIPAddr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtVendorName As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtReqFor As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtGSTin As common.Controls.MyTextBox
    Friend WithEvents Mylabel As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtClientID As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtUserName As common.Controls.MyTextBox
    Friend WithEvents txtPassword As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtIPAddr As common.Controls.MyTextBox
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnViewDocument As RadButton
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtClientSecret As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtUrl As common.Controls.MyTextBox
    Friend WithEvents btnNew As RadButton
    Friend WithEvents txtLocDes As common.Controls.MyTextBox
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnCopy As RadButton
End Class
