
Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ShareMaster
    'Inherits System.Windows.Forms.Form
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtAmount = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.TxtRate = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtCertificateTo = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.TxtShare = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCerificateFrom = New common.Controls.MyTextBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.UsLock1 = New common.usLock()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCertificateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtShare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCerificateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(949, 479)
        Me.SplitContainer1.SplitterDistance = 434
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(949, 414)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.TxtAmount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.TxtCertificateTo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.TxtShare)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtCerificateFrom)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage1.Controls.Add(Me.txtName)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(82.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(928, 366)
        Me.RadPageViewPage1.Text = "Share Master"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(3, 151)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel7.TabIndex = 112
        Me.MyLabel7.Text = "Amount"
        '
        'TxtAmount
        '
        Me.TxtAmount.BackColor = System.Drawing.Color.White
        Me.TxtAmount.CalculationExpression = Nothing
        Me.TxtAmount.FieldCode = Nothing
        Me.TxtAmount.FieldDesc = Nothing
        Me.TxtAmount.FieldMaxLength = 0
        Me.TxtAmount.FieldName = Nothing
        Me.TxtAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAmount.isCalculatedField = False
        Me.TxtAmount.IsSourceFromTable = False
        Me.TxtAmount.IsSourceFromValueList = False
        Me.TxtAmount.IsUnique = False
        Me.TxtAmount.Location = New System.Drawing.Point(100, 150)
        Me.TxtAmount.MaxLength = 12
        Me.TxtAmount.MendatroryField = False
        Me.TxtAmount.MyLinkLable1 = Me.MyLabel7
        Me.TxtAmount.MyLinkLable2 = Nothing
        Me.TxtAmount.Name = "TxtAmount"
        Me.TxtAmount.ReadOnly = True
        Me.TxtAmount.ReferenceFieldDesc = Nothing
        Me.TxtAmount.ReferenceFieldName = Nothing
        Me.TxtAmount.ReferenceTableName = Nothing
        Me.TxtAmount.Size = New System.Drawing.Size(271, 18)
        Me.TxtAmount.TabIndex = 111
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(3, 59)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel6.TabIndex = 110
        Me.MyLabel6.Text = "Certificate From"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(3, 128)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel5.TabIndex = 109
        Me.MyLabel5.Text = "Rate"
        '
        'TxtRate
        '
        Me.TxtRate.BackColor = System.Drawing.Color.White
        Me.TxtRate.CalculationExpression = Nothing
        Me.TxtRate.FieldCode = Nothing
        Me.TxtRate.FieldDesc = Nothing
        Me.TxtRate.FieldMaxLength = 0
        Me.TxtRate.FieldName = Nothing
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.isCalculatedField = False
        Me.TxtRate.IsSourceFromTable = False
        Me.TxtRate.IsSourceFromValueList = False
        Me.TxtRate.IsUnique = False
        Me.TxtRate.Location = New System.Drawing.Point(100, 127)
        Me.TxtRate.MaxLength = 12
        Me.TxtRate.MendatroryField = False
        Me.TxtRate.MyLinkLable1 = Me.MyLabel5
        Me.TxtRate.MyLinkLable2 = Nothing
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.ReferenceFieldDesc = Nothing
        Me.TxtRate.ReferenceFieldName = Nothing
        Me.TxtRate.ReferenceTableName = Nothing
        Me.TxtRate.Size = New System.Drawing.Size(271, 18)
        Me.TxtRate.TabIndex = 108
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(3, 105)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel4.TabIndex = 107
        Me.MyLabel4.Text = "Certificate To"
        '
        'TxtCertificateTo
        '
        Me.TxtCertificateTo.BackColor = System.Drawing.Color.White
        Me.TxtCertificateTo.CalculationExpression = Nothing
        Me.TxtCertificateTo.FieldCode = Nothing
        Me.TxtCertificateTo.FieldDesc = Nothing
        Me.TxtCertificateTo.FieldMaxLength = 0
        Me.TxtCertificateTo.FieldName = Nothing
        Me.TxtCertificateTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCertificateTo.isCalculatedField = False
        Me.TxtCertificateTo.IsSourceFromTable = False
        Me.TxtCertificateTo.IsSourceFromValueList = False
        Me.TxtCertificateTo.IsUnique = False
        Me.TxtCertificateTo.Location = New System.Drawing.Point(100, 104)
        Me.TxtCertificateTo.MaxLength = 12
        Me.TxtCertificateTo.MendatroryField = False
        Me.TxtCertificateTo.MyLinkLable1 = Me.MyLabel4
        Me.TxtCertificateTo.MyLinkLable2 = Nothing
        Me.TxtCertificateTo.Name = "TxtCertificateTo"
        Me.TxtCertificateTo.ReadOnly = True
        Me.TxtCertificateTo.ReferenceFieldDesc = Nothing
        Me.TxtCertificateTo.ReferenceFieldName = Nothing
        Me.TxtCertificateTo.ReferenceTableName = Nothing
        Me.TxtCertificateTo.Size = New System.Drawing.Size(271, 18)
        Me.TxtCertificateTo.TabIndex = 106
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 82)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel3.TabIndex = 105
        Me.MyLabel3.Text = "No. Of Share"
        '
        'TxtShare
        '
        Me.TxtShare.BackColor = System.Drawing.Color.White
        Me.TxtShare.CalculationExpression = Nothing
        Me.TxtShare.FieldCode = Nothing
        Me.TxtShare.FieldDesc = Nothing
        Me.TxtShare.FieldMaxLength = 0
        Me.TxtShare.FieldName = Nothing
        Me.TxtShare.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShare.isCalculatedField = False
        Me.TxtShare.IsSourceFromTable = False
        Me.TxtShare.IsSourceFromValueList = False
        Me.TxtShare.IsUnique = False
        Me.TxtShare.Location = New System.Drawing.Point(100, 81)
        Me.TxtShare.MaxLength = 12
        Me.TxtShare.MendatroryField = False
        Me.TxtShare.MyLinkLable1 = Me.MyLabel3
        Me.TxtShare.MyLinkLable2 = Nothing
        Me.TxtShare.Name = "TxtShare"
        Me.TxtShare.ReferenceFieldDesc = Nothing
        Me.TxtShare.ReferenceFieldName = Nothing
        Me.TxtShare.ReferenceTableName = Nothing
        Me.TxtShare.Size = New System.Drawing.Size(271, 18)
        Me.TxtShare.TabIndex = 104
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 65)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel2.TabIndex = 103
        '
        'txtCerificateFrom
        '
        Me.txtCerificateFrom.BackColor = System.Drawing.Color.White
        Me.txtCerificateFrom.CalculationExpression = Nothing
        Me.txtCerificateFrom.FieldCode = Nothing
        Me.txtCerificateFrom.FieldDesc = Nothing
        Me.txtCerificateFrom.FieldMaxLength = 0
        Me.txtCerificateFrom.FieldName = Nothing
        Me.txtCerificateFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCerificateFrom.isCalculatedField = False
        Me.txtCerificateFrom.IsSourceFromTable = False
        Me.txtCerificateFrom.IsSourceFromValueList = False
        Me.txtCerificateFrom.IsUnique = False
        Me.txtCerificateFrom.Location = New System.Drawing.Point(100, 58)
        Me.txtCerificateFrom.MaxLength = 12
        Me.txtCerificateFrom.MendatroryField = False
        Me.txtCerificateFrom.MyLinkLable1 = Me.MyLabel6
        Me.txtCerificateFrom.MyLinkLable2 = Nothing
        Me.txtCerificateFrom.Name = "txtCerificateFrom"
        Me.txtCerificateFrom.ReferenceFieldDesc = Nothing
        Me.txtCerificateFrom.ReferenceFieldName = Nothing
        Me.txtCerificateFrom.ReferenceTableName = Nothing
        Me.txtCerificateFrom.Size = New System.Drawing.Size(271, 18)
        Me.txtCerificateFrom.TabIndex = 102
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(3, 36)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel24.TabIndex = 101
        Me.MyLabel24.Text = "Name"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(100, 35)
        Me.txtName.MaxLength = 12
        Me.txtName.MendatroryField = False
        Me.txtName.MyLinkLable1 = Me.MyLabel24
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(271, 18)
        Me.txtName.TabIndex = 100
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(507, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(101, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 81
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
        Me.txtDate.Location = New System.Drawing.Point(425, 11)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDate.TabIndex = 80
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(389, 12)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 79
        Me.RadLabel4.Text = "Date"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(353, 11)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(18, 19)
        Me.btnAddNew.TabIndex = 8
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 12)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel1.TabIndex = 7
        Me.MyLabel1.Text = "Document Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(100, 11)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.MyLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(252, 19)
        Me.txtCode.TabIndex = 6
        Me.txtCode.Value = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(949, 20)
        Me.RadMenu1.TabIndex = 7
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(860, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(154, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(81, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 9
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(8, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save"
        '
        'ShareMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(949, 479)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ShareMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "ShareMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCertificateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtShare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCerificateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtRate As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtCertificateTo As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtShare As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCerificateFrom As common.Controls.MyTextBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtAmount As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
End Class
