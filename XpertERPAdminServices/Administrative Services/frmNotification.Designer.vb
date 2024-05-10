<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmNotification
    'Inherits System.Windows.Forms.Form
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.txtUserType = New common.UserControls.txtMultiSelectFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSubject = New common.Controls.MyTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEndDate = New common.Controls.MyDateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStartDate = New common.Controls.MyDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnImport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnImport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(728, 387)
        Me.SplitContainer1.SplitterDistance = 351
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(728, 351)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtUserType)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDescription)
        Me.RadPageViewPage1.Controls.Add(Me.Label1)
        Me.RadPageViewPage1.Controls.Add(Me.txtSubject)
        Me.RadPageViewPage1.Controls.Add(Me.Label3)
        Me.RadPageViewPage1.Controls.Add(Me.txtEndDate)
        Me.RadPageViewPage1.Controls.Add(Me.Label4)
        Me.RadPageViewPage1.Controls.Add(Me.txtStartDate)
        Me.RadPageViewPage1.Controls.Add(Me.Label2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(107.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(707, 303)
        Me.RadPageViewPage1.Text = "Notification Detail"
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.Location = New System.Drawing.Point(4, 54)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(56, 18)
        Me.lblDocNo.TabIndex = 330
        Me.lblDocNo.Text = "User Type"
        '
        'txtUserType
        '
        Me.txtUserType.arrDispalyMember = Nothing
        Me.txtUserType.arrValueMember = Nothing
        Me.txtUserType.Location = New System.Drawing.Point(101, 53)
        Me.txtUserType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserType.MyLinkLable1 = Me.lblDocNo
        Me.txtUserType.MyLinkLable2 = Nothing
        Me.txtUserType.MyNullText = "All"
        Me.txtUserType.Name = "txtUserType"
        Me.txtUserType.Size = New System.Drawing.Size(270, 19)
        Me.txtUserType.TabIndex = 329
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(508, 3)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(101, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 78
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(101, 151)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.MyLabel1
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtDescription.RootElement.StretchVertically = True
        Me.txtDescription.Size = New System.Drawing.Size(508, 105)
        Me.txtDescription.TabIndex = 77
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(4, 5)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "Document Code"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 151)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 76
        Me.Label1.Text = "Description"
        '
        'txtSubject
        '
        Me.txtSubject.CalculationExpression = Nothing
        Me.txtSubject.FieldCode = Nothing
        Me.txtSubject.FieldDesc = Nothing
        Me.txtSubject.FieldMaxLength = 0
        Me.txtSubject.FieldName = Nothing
        Me.txtSubject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubject.isCalculatedField = False
        Me.txtSubject.IsSourceFromTable = False
        Me.txtSubject.IsSourceFromValueList = False
        Me.txtSubject.IsUnique = False
        Me.txtSubject.Location = New System.Drawing.Point(101, 78)
        Me.txtSubject.MaxLength = 200
        Me.txtSubject.MendatroryField = False
        Me.txtSubject.Multiline = True
        Me.txtSubject.MyLinkLable1 = Nothing
        Me.txtSubject.MyLinkLable2 = Nothing
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.ReferenceFieldDesc = Nothing
        Me.txtSubject.ReferenceFieldName = Nothing
        Me.txtSubject.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtSubject.RootElement.StretchVertically = True
        Me.txtSubject.Size = New System.Drawing.Size(508, 67)
        Me.txtSubject.TabIndex = 75
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Subject"
        '
        'txtEndDate
        '
        Me.txtEndDate.CalculationExpression = Nothing
        Me.txtEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEndDate.FieldCode = Nothing
        Me.txtEndDate.FieldDesc = Nothing
        Me.txtEndDate.FieldMaxLength = 0
        Me.txtEndDate.FieldName = Nothing
        Me.txtEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEndDate.isCalculatedField = False
        Me.txtEndDate.IsSourceFromTable = False
        Me.txtEndDate.IsSourceFromValueList = False
        Me.txtEndDate.IsUnique = False
        Me.txtEndDate.Location = New System.Drawing.Point(279, 29)
        Me.txtEndDate.MendatroryField = False
        Me.txtEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.MyLinkLable1 = Nothing
        Me.txtEndDate.MyLinkLable2 = Nothing
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.ReferenceFieldDesc = Nothing
        Me.txtEndDate.ReferenceFieldName = Nothing
        Me.txtEndDate.ReferenceTableName = Nothing
        Me.txtEndDate.ShowCheckBox = True
        Me.txtEndDate.Size = New System.Drawing.Size(92, 18)
        Me.txtEndDate.TabIndex = 73
        Me.txtEndDate.TabStop = False
        Me.txtEndDate.Text = "13/06/2011"
        Me.txtEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(214, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 72
        Me.Label4.Text = "End Date"
        '
        'txtStartDate
        '
        Me.txtStartDate.CalculationExpression = Nothing
        Me.txtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtStartDate.FieldCode = Nothing
        Me.txtStartDate.FieldDesc = Nothing
        Me.txtStartDate.FieldMaxLength = 0
        Me.txtStartDate.FieldName = Nothing
        Me.txtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStartDate.isCalculatedField = False
        Me.txtStartDate.IsSourceFromTable = False
        Me.txtStartDate.IsSourceFromValueList = False
        Me.txtStartDate.IsUnique = False
        Me.txtStartDate.Location = New System.Drawing.Point(101, 29)
        Me.txtStartDate.MendatroryField = False
        Me.txtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.MyLinkLable1 = Nothing
        Me.txtStartDate.MyLinkLable2 = Nothing
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.ReferenceFieldDesc = Nothing
        Me.txtStartDate.ReferenceFieldName = Nothing
        Me.txtStartDate.ReferenceTableName = Nothing
        Me.txtStartDate.Size = New System.Drawing.Size(87, 18)
        Me.txtStartDate.TabIndex = 59
        Me.txtStartDate.TabStop = False
        Me.txtStartDate.Text = "13/06/2011"
        Me.txtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Start Date"
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
        Me.txtDate.Location = New System.Drawing.Point(426, 4)
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
        Me.txtDate.TabIndex = 4
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(390, 5)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 3
        Me.RadLabel4.Text = "Date"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPAdminServices.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(353, 4)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(18, 19)
        Me.btnAddNew.TabIndex = 2
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(101, 4)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.MyLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(252, 19)
        Me.txtCode.TabIndex = 1
        Me.txtCode.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1235, 546)
        Me.RadPageViewPage2.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1235, 546)
        Me.UcAttachment1.TabIndex = 2
        Me.UcAttachment1.TabStop = False
        '
        'btnImport
        '
        Me.btnImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiImport, Me.rmiExport})
        Me.btnImport.Location = New System.Drawing.Point(231, 5)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(110, 22)
        Me.btnImport.TabIndex = 159
        Me.btnImport.Text = "Import/Export"
        Me.btnImport.Visible = False
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        Me.rmiImport.UseCompatibleTextRendering = False
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        Me.rmiExport.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(647, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(158, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(85, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(12, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(728, 20)
        Me.RadMenu1.TabIndex = 6
        '
        'frmNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 407)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmNotification"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmNotification"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents txtEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtSubject As common.Controls.MyTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents txtUserType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnImport As RadSplitButton
    Friend WithEvents rmiImport As RadMenuItem
    Friend WithEvents rmiExport As RadMenuItem
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
End Class
