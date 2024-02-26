Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDailySMPProduction
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim WindowsSettings1 As Telerik.WinControls.WindowsSettings = New Telerik.WinControls.WindowsSettings()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnAddSource = New Telerik.WinControls.UI.RadButton()
        Me.btnAddParty = New Telerik.WinControls.UI.RadButton()
        Me.txtReportingDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtReportDate = New common.Controls.MyDateTimePicker()
        Me.txt = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtFreightAmt = New common.MyNumBox()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.EmailSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.EmailSettingCreditApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.btnAddSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReportingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReportDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv2.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreightAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1025, 501)
        Me.SplitContainer1.SplitterDistance = 469
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1025, 469)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.btnAddSource)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddParty)
        Me.RadPageViewPage1.Controls.Add(Me.txtReportingDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtReportDate)
        Me.RadPageViewPage1.Controls.Add(Me.txt)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1004, 423)
        Me.RadPageViewPage1.Text = "Milk Receipts"
        '
        'btnAddSource
        '
        Me.btnAddSource.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddSource.Location = New System.Drawing.Point(3, 25)
        Me.btnAddSource.Name = "btnAddSource"
        Me.btnAddSource.Size = New System.Drawing.Size(81, 18)
        Me.btnAddSource.TabIndex = 404
        Me.btnAddSource.Text = "Add Source"
        '
        'btnAddParty
        '
        Me.btnAddParty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddParty.Location = New System.Drawing.Point(871, 25)
        Me.btnAddParty.Name = "btnAddParty"
        Me.btnAddParty.Size = New System.Drawing.Size(99, 18)
        Me.btnAddParty.TabIndex = 403
        Me.btnAddParty.Text = "Add Party"
        '
        'txtReportingDate
        '
        Me.txtReportingDate.CalculationExpression = Nothing
        Me.txtReportingDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReportingDate.Enabled = False
        Me.txtReportingDate.FieldCode = Nothing
        Me.txtReportingDate.FieldDesc = Nothing
        Me.txtReportingDate.FieldMaxLength = 0
        Me.txtReportingDate.FieldName = Nothing
        Me.txtReportingDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReportingDate.isCalculatedField = False
        Me.txtReportingDate.IsSourceFromTable = False
        Me.txtReportingDate.IsSourceFromValueList = False
        Me.txtReportingDate.IsUnique = False
        Me.txtReportingDate.Location = New System.Drawing.Point(734, 24)
        Me.txtReportingDate.MendatroryField = False
        Me.txtReportingDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportingDate.MyLinkLable1 = Nothing
        Me.txtReportingDate.MyLinkLable2 = Nothing
        Me.txtReportingDate.Name = "txtReportingDate"
        Me.txtReportingDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportingDate.ReferenceFieldDesc = Nothing
        Me.txtReportingDate.ReferenceFieldName = Nothing
        Me.txtReportingDate.ReferenceTableName = Nothing
        Me.txtReportingDate.Size = New System.Drawing.Size(126, 18)
        Me.txtReportingDate.TabIndex = 402
        Me.txtReportingDate.TabStop = False
        Me.txtReportingDate.Text = "08/02/2024"
        Me.txtReportingDate.Value = New Date(2024, 2, 8, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(642, 24)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel3.TabIndex = 401
        Me.MyLabel3.Text = "Reporting Date"
        '
        'txtReportDate
        '
        Me.txtReportDate.CalculationExpression = Nothing
        Me.txtReportDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReportDate.Enabled = False
        Me.txtReportDate.FieldCode = Nothing
        Me.txtReportDate.FieldDesc = Nothing
        Me.txtReportDate.FieldMaxLength = 0
        Me.txtReportDate.FieldName = Nothing
        Me.txtReportDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReportDate.isCalculatedField = False
        Me.txtReportDate.IsSourceFromTable = False
        Me.txtReportDate.IsSourceFromValueList = False
        Me.txtReportDate.IsUnique = False
        Me.txtReportDate.Location = New System.Drawing.Point(733, 0)
        Me.txtReportDate.MendatroryField = False
        Me.txtReportDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportDate.MyLinkLable1 = Nothing
        Me.txtReportDate.MyLinkLable2 = Nothing
        Me.txtReportDate.Name = "txtReportDate"
        Me.txtReportDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportDate.ReferenceFieldDesc = Nothing
        Me.txtReportDate.ReferenceFieldName = Nothing
        Me.txtReportDate.ReferenceTableName = Nothing
        Me.txtReportDate.Size = New System.Drawing.Size(126, 18)
        Me.txtReportDate.TabIndex = 400
        Me.txtReportDate.TabStop = False
        Me.txtReportDate.Text = "07/02/2024"
        Me.txtReportDate.Value = New Date(2024, 2, 7, 0, 0, 0, 0)
        '
        'txt
        '
        Me.txt.FieldName = Nothing
        Me.txt.Location = New System.Drawing.Point(642, 0)
        Me.txt.Name = "txt"
        Me.txt.Size = New System.Drawing.Size(67, 18)
        Me.txt.TabIndex = 399
        Me.txt.Text = "Report Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(878, -2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 135
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 48)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(999, 372)
        Me.RadGroupBox2.TabIndex = 27
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
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(979, 342)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(-1, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 39
        Me.RadLabel1.Text = "Document No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(98, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPMIS.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(350, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(55.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1004, 423)
        Me.RadPageViewPage2.Text = "Powder"
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Controls.Add(Me.MyLabel4)
        Me.gv2.Controls.Add(Me.txtFreightAmt)
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1004, 423)
        Me.gv2.TabIndex = 1
        Me.gv2.TabStop = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(1077, 12)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel4.TabIndex = 45
        Me.MyLabel4.Visible = False
        '
        'txtFreightAmt
        '
        Me.txtFreightAmt.AcceptsTab = True
        Me.txtFreightAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFreightAmt.CalculationExpression = Nothing
        Me.txtFreightAmt.DecimalPlaces = 5
        Me.txtFreightAmt.FieldCode = Nothing
        Me.txtFreightAmt.FieldDesc = Nothing
        Me.txtFreightAmt.FieldMaxLength = 0
        Me.txtFreightAmt.FieldName = Nothing
        Me.txtFreightAmt.HideSelection = False
        Me.txtFreightAmt.isCalculatedField = False
        Me.txtFreightAmt.IsSourceFromTable = False
        Me.txtFreightAmt.IsSourceFromValueList = False
        Me.txtFreightAmt.IsUnique = False
        Me.txtFreightAmt.Location = New System.Drawing.Point(1169, 10)
        Me.txtFreightAmt.MaxLength = 65535
        Me.txtFreightAmt.MendatroryField = True
        Me.txtFreightAmt.MyLinkLable1 = Nothing
        Me.txtFreightAmt.MyLinkLable2 = Nothing
        Me.txtFreightAmt.Name = "txtFreightAmt"
        Me.txtFreightAmt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtFreightAmt.ReferenceFieldDesc = Nothing
        Me.txtFreightAmt.ReferenceFieldName = Nothing
        Me.txtFreightAmt.ReferenceTableName = Nothing
        Me.txtFreightAmt.Size = New System.Drawing.Size(179, 20)
        Me.txtFreightAmt.TabIndex = 129
        Me.txtFreightAmt.Text = "0"
        Me.txtFreightAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFreightAmt.Value = 0R
        Me.txtFreightAmt.Visible = False
        Me.txtFreightAmt.WordWrap = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(158, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(941, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'EmailSmsSetting
        '
        Me.EmailSmsSetting.Name = "EmailSmsSetting"
        Me.EmailSmsSetting.Text = "Email/SMS Setting"
        Me.EmailSmsSetting.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1025, 501)
        Me.Panel1.TabIndex = 7
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1025, 20)
        Me.RadMenu1.TabIndex = 6
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.EmailSmsSetting, Me.EmailSettingCreditApproval})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'EmailSettingCreditApproval
        '
        Me.EmailSettingCreditApproval.Name = "EmailSettingCreditApproval"
        Me.EmailSettingCreditApproval.Text = "Email setting for Credit Approval"
        Me.EmailSettingCreditApproval.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AnimationEnabled = False
        Me.RadMenuItem2.AnimationFrames = 1
        Me.RadMenuItem2.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.None
        Me.RadMenuItem2.AutoSize = True
        Me.RadMenuItem2.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem2.DropShadow = True
        Me.RadMenuItem2.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem2.EnableAeroEffects = False
        Me.RadMenuItem2.FadeAnimationFrames = 10
        Me.RadMenuItem2.FadeAnimationSpeed = 10
        Me.RadMenuItem2.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem2.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem2.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.LastShowDpiScaleFactor = New System.Drawing.SizeF(1.0!, 1.0!)
        Me.RadMenuItem2.Location = New System.Drawing.Point(209, 737)
        Me.RadMenuItem2.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Opacity = 1.0!
        Me.RadMenuItem2.ProcessKeyboard = False
        Me.RadMenuItem2.RollOverItemSelection = True
        Me.RadMenuItem2.Size = New System.Drawing.Size(27, 2)
        Me.RadMenuItem2.TabIndex = 5
        Me.RadMenuItem2.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Visible = False
        WindowsSettings1.EnableRoundedCorners = Nothing
        WindowsSettings1.RoundedCornersStyle = Telerik.WinControls.RoundedCornersStyle.Round
        Me.RadMenuItem2.WindowsSettings = WindowsSettings1
        '
        'frmDailySMPProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 521)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadMenuItem2)
        Me.Name = "frmDailySMPProduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily SMP Production"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.btnAddSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReportingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReportDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv2.ResumeLayout(False)
        Me.gv2.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreightAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents EmailSmsSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents EmailSettingCreditApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtReportingDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtReportDate As common.Controls.MyDateTimePicker
    Friend WithEvents txt As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtFreightAmt As common.MyNumBox
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnAddParty As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAddSource As Telerik.WinControls.UI.RadButton
End Class
