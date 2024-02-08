<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOutgoingQCEntry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOutgoingQCEntry))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.QCEnddate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.QcStartdata = New common.Controls.MyDateTimePicker()
        Me.UsLock1 = New common.usLock()
        Me.rbtnUD = New common.Controls.MyRadioButton()
        Me.rbtnRej = New common.Controls.MyRadioButton()
        Me.rbtnApp = New common.Controls.MyRadioButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.txtprodCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.docDate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.lblfgcode = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.TxtFgcode = New common.UserControls.txtFinder()
        Me.txtlocation = New common.UserControls.txtFinder()
        Me.lblBMC = New common.Controls.MyLabel()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.brnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.txtAccept = New System.Windows.Forms.Label()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.QCEnddate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QcStartdata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnUD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnRej, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.docDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfgcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.brnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.brnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Size = New System.Drawing.Size(1030, 472)
        Me.SplitContainer1.SplitterDistance = 428
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1030, 428)
        Me.RadPageView1.TabIndex = 12
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtAccept)
        Me.RadPageViewPage1.Controls.Add(Me.QCEnddate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.QcStartdata)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnUD)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnRej)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnApp)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.txtprodCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.docDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.lblfgcode)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.TxtFgcode)
        Me.RadPageViewPage1.Controls.Add(Me.txtlocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblBMC)
        Me.RadPageViewPage1.Controls.Add(Me.lblRoute)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1009, 380)
        Me.RadPageViewPage1.Text = "QC Production"
        '
        'QCEnddate
        '
        Me.QCEnddate.CalculationExpression = Nothing
        Me.QCEnddate.CustomFormat = "dd/MM/yyyy"
        Me.QCEnddate.FieldCode = Nothing
        Me.QCEnddate.FieldDesc = Nothing
        Me.QCEnddate.FieldMaxLength = 0
        Me.QCEnddate.FieldName = Nothing
        Me.QCEnddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.QCEnddate.isCalculatedField = False
        Me.QCEnddate.IsSourceFromTable = False
        Me.QCEnddate.IsSourceFromValueList = False
        Me.QCEnddate.IsUnique = False
        Me.QCEnddate.Location = New System.Drawing.Point(761, 40)
        Me.QCEnddate.MendatroryField = False
        Me.QCEnddate.MyLinkLable1 = Me.MyLabel3
        Me.QCEnddate.MyLinkLable2 = Nothing
        Me.QCEnddate.Name = "QCEnddate"
        Me.QCEnddate.ReferenceFieldDesc = Nothing
        Me.QCEnddate.ReferenceFieldName = Nothing
        Me.QCEnddate.ReferenceTableName = Nothing
        Me.QCEnddate.Size = New System.Drawing.Size(82, 20)
        Me.QCEnddate.TabIndex = 1477
        Me.QCEnddate.TabStop = False
        Me.QCEnddate.Text = "24/02/2015"
        Me.QCEnddate.Value = New Date(2015, 2, 24, 17, 7, 15, 425)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(394, 17)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel3.TabIndex = 453
        Me.MyLabel3.Text = "Date"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(686, 40)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel7.TabIndex = 1476
        Me.MyLabel7.Text = "QC end date"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(518, 40)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(74, 18)
        Me.MyLabel6.TabIndex = 1475
        Me.MyLabel6.Text = "QC Start date"
        '
        'QcStartdata
        '
        Me.QcStartdata.CalculationExpression = Nothing
        Me.QcStartdata.CustomFormat = "dd/MM/yyyy"
        Me.QcStartdata.FieldCode = Nothing
        Me.QcStartdata.FieldDesc = Nothing
        Me.QcStartdata.FieldMaxLength = 0
        Me.QcStartdata.FieldName = Nothing
        Me.QcStartdata.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.QcStartdata.isCalculatedField = False
        Me.QcStartdata.IsSourceFromTable = False
        Me.QcStartdata.IsSourceFromValueList = False
        Me.QcStartdata.IsUnique = False
        Me.QcStartdata.Location = New System.Drawing.Point(598, 40)
        Me.QcStartdata.MendatroryField = False
        Me.QcStartdata.MyLinkLable1 = Me.MyLabel3
        Me.QcStartdata.MyLinkLable2 = Nothing
        Me.QcStartdata.Name = "QcStartdata"
        Me.QcStartdata.ReferenceFieldDesc = Nothing
        Me.QcStartdata.ReferenceFieldName = Nothing
        Me.QcStartdata.ReferenceTableName = Nothing
        Me.QcStartdata.Size = New System.Drawing.Size(82, 20)
        Me.QcStartdata.TabIndex = 1474
        Me.QcStartdata.TabStop = False
        Me.QcStartdata.Text = "24/02/2015"
        Me.QcStartdata.Value = New Date(2015, 2, 24, 17, 7, 15, 425)
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(856, 11)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(107, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1473
        '
        'rbtnUD
        '
        Me.rbtnUD.Location = New System.Drawing.Point(668, 13)
        Me.rbtnUD.MyLinkLable1 = Nothing
        Me.rbtnUD.MyLinkLable2 = Nothing
        Me.rbtnUD.Name = "rbtnUD"
        Me.rbtnUD.Size = New System.Drawing.Size(101, 18)
        Me.rbtnUD.TabIndex = 1470
        Me.rbtnUD.Text = "Under Deviation"
        Me.rbtnUD.Visible = False
        '
        'rbtnRej
        '
        Me.rbtnRej.Location = New System.Drawing.Point(598, 13)
        Me.rbtnRej.MyLinkLable1 = Nothing
        Me.rbtnRej.MyLinkLable2 = Nothing
        Me.rbtnRej.Name = "rbtnRej"
        Me.rbtnRej.Size = New System.Drawing.Size(63, 18)
        Me.rbtnRej.TabIndex = 1471
        Me.rbtnRej.Text = "Rejected"
        '
        'rbtnApp
        '
        Me.rbtnApp.Location = New System.Drawing.Point(527, 13)
        Me.rbtnApp.MyLinkLable1 = Nothing
        Me.rbtnApp.MyLinkLable2 = Nothing
        Me.rbtnApp.Name = "rbtnApp"
        Me.rbtnApp.Size = New System.Drawing.Size(67, 18)
        Me.rbtnApp.TabIndex = 1472
        Me.rbtnApp.Text = "Accepted"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(11, 121)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(90, 18)
        Me.MyLabel5.TabIndex = 1469
        Me.MyLabel5.Text = "Production Code"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 100)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel4.TabIndex = 1468
        Me.MyLabel4.Text = "Remarks"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 81)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel2.TabIndex = 1467
        Me.MyLabel2.Text = "Comment"
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
        Me.txtRemarks.Location = New System.Drawing.Point(106, 100)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Modified = True
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(407, 18)
        Me.txtRemarks.TabIndex = 1466
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
        Me.txtComment.Location = New System.Drawing.Point(106, 81)
        Me.txtComment.MaxLength = 50
        Me.txtComment.MendatroryField = False
        Me.txtComment.Modified = True
        Me.txtComment.MyLinkLable1 = Nothing
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(407, 18)
        Me.txtComment.TabIndex = 1465
        '
        'txtprodCode
        '
        Me.txtprodCode.arrDispalyMember = Nothing
        Me.txtprodCode.arrValueMember = Nothing
        Me.txtprodCode.Location = New System.Drawing.Point(106, 120)
        Me.txtprodCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprodCode.MyLinkLable1 = Nothing
        Me.txtprodCode.MyLinkLable2 = Nothing
        Me.txtprodCode.MyNullText = "All"
        Me.txtprodCode.Name = "txtprodCode"
        Me.txtprodCode.Size = New System.Drawing.Size(407, 19)
        Me.txtprodCode.TabIndex = 455
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 17)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel1.TabIndex = 451
        Me.MyLabel1.Text = "Document No."
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(360, 15)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 449
        Me.btnNew.Text = " "
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(106, 15)
        Me.txtDocNo.MendatroryField = True
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.MyLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 100
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(253, 21)
        Me.txtDocNo.TabIndex = 452
        Me.txtDocNo.Value = ""
        '
        'docDate
        '
        Me.docDate.CalculationExpression = Nothing
        Me.docDate.CustomFormat = "dd/MM/yyyy"
        Me.docDate.FieldCode = Nothing
        Me.docDate.FieldDesc = Nothing
        Me.docDate.FieldMaxLength = 0
        Me.docDate.FieldName = Nothing
        Me.docDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.docDate.isCalculatedField = False
        Me.docDate.IsSourceFromTable = False
        Me.docDate.IsSourceFromValueList = False
        Me.docDate.IsUnique = False
        Me.docDate.Location = New System.Drawing.Point(430, 14)
        Me.docDate.MendatroryField = False
        Me.docDate.MyLinkLable1 = Me.MyLabel3
        Me.docDate.MyLinkLable2 = Nothing
        Me.docDate.Name = "docDate"
        Me.docDate.ReferenceFieldDesc = Nothing
        Me.docDate.ReferenceFieldName = Nothing
        Me.docDate.ReferenceTableName = Nothing
        Me.docDate.Size = New System.Drawing.Size(82, 20)
        Me.docDate.TabIndex = 450
        Me.docDate.TabStop = False
        Me.docDate.Text = "24/02/2015"
        Me.docDate.Value = New Date(2015, 2, 24, 17, 7, 15, 425)
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Production Detail for QC"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 177)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1006, 220)
        Me.RadGroupBox2.TabIndex = 36
        Me.RadGroupBox2.Text = "Production Detail for QC"
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
        Me.gv1.Size = New System.Drawing.Size(986, 190)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'lblfgcode
        '
        Me.lblfgcode.AutoSize = False
        Me.lblfgcode.BorderVisible = True
        Me.lblfgcode.FieldName = Nothing
        Me.lblfgcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfgcode.Location = New System.Drawing.Point(268, 60)
        Me.lblfgcode.Name = "lblfgcode"
        Me.lblfgcode.Size = New System.Drawing.Size(244, 18)
        Me.lblfgcode.TabIndex = 448
        Me.lblfgcode.TextWrap = False
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(268, 40)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(244, 18)
        Me.lblLocation.TabIndex = 447
        Me.lblLocation.TextWrap = False
        '
        'TxtFgcode
        '
        Me.TxtFgcode.CalculationExpression = Nothing
        Me.TxtFgcode.FieldCode = Nothing
        Me.TxtFgcode.FieldDesc = Nothing
        Me.TxtFgcode.FieldMaxLength = 0
        Me.TxtFgcode.FieldName = Nothing
        Me.TxtFgcode.isCalculatedField = False
        Me.TxtFgcode.IsSourceFromTable = False
        Me.TxtFgcode.IsSourceFromValueList = False
        Me.TxtFgcode.IsUnique = False
        Me.TxtFgcode.Location = New System.Drawing.Point(106, 61)
        Me.TxtFgcode.MendatroryField = True
        Me.TxtFgcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFgcode.MyLinkLable1 = Nothing
        Me.TxtFgcode.MyLinkLable2 = Nothing
        Me.TxtFgcode.MyReadOnly = False
        Me.TxtFgcode.MyShowMasterFormButton = False
        Me.TxtFgcode.Name = "TxtFgcode"
        Me.TxtFgcode.ReferenceFieldDesc = Nothing
        Me.TxtFgcode.ReferenceFieldName = Nothing
        Me.TxtFgcode.ReferenceTableName = Nothing
        Me.TxtFgcode.Size = New System.Drawing.Size(156, 18)
        Me.TxtFgcode.TabIndex = 446
        Me.TxtFgcode.Value = ""
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(106, 40)
        Me.txtlocation.MendatroryField = True
        Me.txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.MyLinkLable1 = Nothing
        Me.txtlocation.MyLinkLable2 = Nothing
        Me.txtlocation.MyReadOnly = False
        Me.txtlocation.MyShowMasterFormButton = False
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(156, 18)
        Me.txtlocation.TabIndex = 445
        Me.txtlocation.Value = ""
        '
        'lblBMC
        '
        Me.lblBMC.FieldName = Nothing
        Me.lblBMC.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMC.Location = New System.Drawing.Point(11, 40)
        Me.lblBMC.Name = "lblBMC"
        Me.lblBMC.Size = New System.Drawing.Size(49, 18)
        Me.lblBMC.TabIndex = 394
        Me.lblBMC.Text = "Location"
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(12, 60)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(49, 18)
        Me.lblRoute.TabIndex = 392
        Me.lblRoute.Text = "FG Code"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(305, 11)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(71, 22)
        Me.btnReverse.TabIndex = 440
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(232, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(71, 22)
        Me.btnPrint.TabIndex = 438
        Me.btnPrint.Text = "Print"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(86, 11)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(71, 22)
        Me.btndelete.TabIndex = 439
        Me.btndelete.Text = "Delete"
        '
        'brnSave
        '
        Me.brnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.brnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.brnSave.Location = New System.Drawing.Point(13, 11)
        Me.brnSave.Name = "brnSave"
        Me.brnSave.Size = New System.Drawing.Size(71, 22)
        Me.brnSave.TabIndex = 437
        Me.brnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(936, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 436
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(159, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(71, 22)
        Me.btnPost.TabIndex = 435
        Me.btnPost.Text = "Post"
        '
        'txtAccept
        '
        Me.txtAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAccept.AutoSize = True
        Me.txtAccept.BackColor = System.Drawing.Color.LightGreen
        Me.txtAccept.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccept.Location = New System.Drawing.Point(856, 39)
        Me.txtAccept.MaximumSize = New System.Drawing.Size(107, 20)
        Me.txtAccept.MinimumSize = New System.Drawing.Size(107, 20)
        Me.txtAccept.Name = "txtAccept"
        Me.txtAccept.Size = New System.Drawing.Size(107, 20)
        Me.txtAccept.TabIndex = 1478
        Me.txtAccept.Text = "Accepted"
        Me.txtAccept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmOutgoingQCEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 472)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmOutgoingQCEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmOutgoingQCEntry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.QCEnddate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QcStartdata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnUD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnRej, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.docDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfgcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.brnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents lblfgcode As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents TxtFgcode As common.UserControls.txtFinder
    Friend WithEvents txtlocation As common.UserControls.txtFinder
    Friend WithEvents lblBMC As common.Controls.MyLabel
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents brnSave As RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnNew As RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents docDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtprodCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents rbtnUD As common.Controls.MyRadioButton
    Friend WithEvents rbtnRej As common.Controls.MyRadioButton
    Friend WithEvents rbtnApp As common.Controls.MyRadioButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btndelete As RadButton
    Friend WithEvents btnReverse As RadButton
    Friend WithEvents QcStartdata As common.Controls.MyDateTimePicker
    Friend WithEvents QCEnddate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtAccept As Label
End Class
