<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequestMaster
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnhead_ex = New Telerik.WinControls.UI.RadMenuItem()
        Me.btngrid_ex = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnhead_im = New Telerik.WinControls.UI.RadMenuItem()
        Me.btngrid_im = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.UsLock2 = New common.usLock()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtRequestTo = New common.Controls.MyTextBox()
        Me.txtRequestBy = New common.Controls.MyTextBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtReason = New common.Controls.MyTextBox()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.dtpRequestDate = New common.Controls.MyDateTimePicker()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.lblRequestTo = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.textRequestCode = New common.UserControls.txtNavigator()
        Me.lblRequestBy = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtScreen = New common.UserControls.txtFinder()
        Me.lblScreen = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.UcCustomFields1 = New XpertERPCommanServices.ucCustomFields()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRequestTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpRequestDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRequestTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRequestBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScreen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(853, 497)
        Me.SplitContainer1.SplitterDistance = 457
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(5)
        Me.SplitContainer2.Size = New System.Drawing.Size(847, 451)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 53
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(847, 20)
        Me.RadMenu1.TabIndex = 10
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnhead_ex, Me.btngrid_ex})
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnhead_ex
        '
        Me.btnhead_ex.Name = "btnhead_ex"
        Me.btnhead_ex.Text = "Head Export"
        '
        'btngrid_ex
        '
        Me.btngrid_ex.Name = "btngrid_ex"
        Me.btngrid_ex.Text = "Grid Export"
        '
        'btnimport
        '
        Me.btnimport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnhead_im, Me.btngrid_im})
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'btnhead_im
        '
        Me.btnhead_im.Name = "btnhead_im"
        Me.btnhead_im.Text = "Head Import"
        '
        'btngrid_im
        '
        Me.btngrid_im.Name = "btngrid_im"
        Me.btngrid_im.Text = "Grid Import"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(5, 5)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(837, 412)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequestTo)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequestBy)
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.dtpRequestDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblvandorno)
        Me.RadPageViewPage1.Controls.Add(Me.lblRequestTo)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.textRequestCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblRequestBy)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtScreen)
        Me.RadPageViewPage1.Controls.Add(Me.lblScreen)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 24.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(816, 368)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(604, 35)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel5.TabIndex = 1455
        Me.MyLabel5.Text = "Approval Status"
        '
        'UsLock2
        '
        Me.UsLock2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock2.Location = New System.Drawing.Point(716, 33)
        Me.UsLock2.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock2.Name = "UsLock2"
        Me.UsLock2.Size = New System.Drawing.Size(88, 21)
        Me.UsLock2.Status = common.ERPTransactionStatus.Pending
        Me.UsLock2.TabIndex = 1454
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(604, 12)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel3.TabIndex = 1453
        Me.MyLabel3.Text = "Posting Status"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(716, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1452
        '
        'txtRequestTo
        '
        Me.txtRequestTo.CalculationExpression = Nothing
        Me.txtRequestTo.FieldCode = Nothing
        Me.txtRequestTo.FieldDesc = Nothing
        Me.txtRequestTo.FieldMaxLength = 0
        Me.txtRequestTo.FieldName = Nothing
        Me.txtRequestTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestTo.isCalculatedField = False
        Me.txtRequestTo.IsSourceFromTable = False
        Me.txtRequestTo.IsSourceFromValueList = False
        Me.txtRequestTo.IsUnique = False
        Me.txtRequestTo.Location = New System.Drawing.Point(85, 57)
        Me.txtRequestTo.MaxLength = 50
        Me.txtRequestTo.MendatroryField = True
        Me.txtRequestTo.MyLinkLable1 = Nothing
        Me.txtRequestTo.MyLinkLable2 = Nothing
        Me.txtRequestTo.Name = "txtRequestTo"
        Me.txtRequestTo.ReadOnly = True
        Me.txtRequestTo.ReferenceFieldDesc = Nothing
        Me.txtRequestTo.ReferenceFieldName = Nothing
        Me.txtRequestTo.ReferenceTableName = Nothing
        Me.txtRequestTo.Size = New System.Drawing.Size(173, 18)
        Me.txtRequestTo.TabIndex = 1451
        '
        'txtRequestBy
        '
        Me.txtRequestBy.CalculationExpression = Nothing
        Me.txtRequestBy.FieldCode = Nothing
        Me.txtRequestBy.FieldDesc = Nothing
        Me.txtRequestBy.FieldMaxLength = 0
        Me.txtRequestBy.FieldName = Nothing
        Me.txtRequestBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestBy.isCalculatedField = False
        Me.txtRequestBy.IsSourceFromTable = False
        Me.txtRequestBy.IsSourceFromValueList = False
        Me.txtRequestBy.IsUnique = False
        Me.txtRequestBy.Location = New System.Drawing.Point(85, 34)
        Me.txtRequestBy.MaxLength = 50
        Me.txtRequestBy.MendatroryField = True
        Me.txtRequestBy.MyLinkLable1 = Nothing
        Me.txtRequestBy.MyLinkLable2 = Nothing
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.ReadOnly = True
        Me.txtRequestBy.ReferenceFieldDesc = Nothing
        Me.txtRequestBy.ReferenceFieldName = Nothing
        Me.txtRequestBy.ReferenceTableName = Nothing
        Me.txtRequestBy.Size = New System.Drawing.Size(173, 18)
        Me.txtRequestBy.TabIndex = 1450
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 103)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtReason)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtRemarks)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Size = New System.Drawing.Size(804, 265)
        Me.SplitContainer3.SplitterDistance = 172
        Me.SplitContainer3.TabIndex = 1449
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(6, 4)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel9.TabIndex = 110
        Me.MyLabel9.Text = "Reason"
        '
        'txtReason
        '
        Me.txtReason.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReason.CalculationExpression = Nothing
        Me.txtReason.FieldCode = Nothing
        Me.txtReason.FieldDesc = Nothing
        Me.txtReason.FieldMaxLength = 0
        Me.txtReason.FieldName = Nothing
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.isCalculatedField = False
        Me.txtReason.IsSourceFromTable = False
        Me.txtReason.IsSourceFromValueList = False
        Me.txtReason.IsUnique = False
        Me.txtReason.Location = New System.Drawing.Point(57, 4)
        Me.txtReason.MaxLength = 50
        Me.txtReason.MendatroryField = True
        Me.txtReason.Multiline = True
        Me.txtReason.MyLinkLable1 = Nothing
        Me.txtReason.MyLinkLable2 = Nothing
        Me.txtReason.Name = "txtReason"
        Me.txtReason.ReferenceFieldDesc = Nothing
        Me.txtReason.ReferenceFieldName = Nothing
        Me.txtReason.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtReason.RootElement.StretchVertically = True
        Me.txtReason.Size = New System.Drawing.Size(744, 165)
        Me.txtReason.TabIndex = 103
        '
        'txtRemarks
        '
        Me.txtRemarks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.txtRemarks.Location = New System.Drawing.Point(57, 5)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.MendatroryField = True
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtRemarks.RootElement.StretchVertically = True
        Me.txtRemarks.Size = New System.Drawing.Size(744, 81)
        Me.txtRemarks.TabIndex = 112
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 5)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel2.TabIndex = 111
        Me.MyLabel2.Text = "Remarks"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(459, 12)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel12.TabIndex = 1448
        Me.MyLabel12.Text = "Date"
        '
        'dtpRequestDate
        '
        Me.dtpRequestDate.CalculationExpression = Nothing
        Me.dtpRequestDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpRequestDate.FieldCode = Nothing
        Me.dtpRequestDate.FieldDesc = Nothing
        Me.dtpRequestDate.FieldMaxLength = 0
        Me.dtpRequestDate.FieldName = Nothing
        Me.dtpRequestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRequestDate.isCalculatedField = False
        Me.dtpRequestDate.IsSourceFromTable = False
        Me.dtpRequestDate.IsSourceFromValueList = False
        Me.dtpRequestDate.IsUnique = False
        Me.dtpRequestDate.Location = New System.Drawing.Point(497, 10)
        Me.dtpRequestDate.MendatroryField = False
        Me.dtpRequestDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpRequestDate.MyLinkLable1 = Nothing
        Me.dtpRequestDate.MyLinkLable2 = Nothing
        Me.dtpRequestDate.Name = "dtpRequestDate"
        Me.dtpRequestDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpRequestDate.ReferenceFieldDesc = Nothing
        Me.dtpRequestDate.ReferenceFieldName = Nothing
        Me.dtpRequestDate.ReferenceTableName = Nothing
        Me.dtpRequestDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpRequestDate.TabIndex = 1447
        Me.dtpRequestDate.TabStop = False
        Me.dtpRequestDate.Text = "10/06/2011"
        Me.dtpRequestDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(6, 12)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(79, 16)
        Me.lblvandorno.TabIndex = 6
        Me.lblvandorno.Text = "Request Code"
        '
        'lblRequestTo
        '
        Me.lblRequestTo.AutoSize = False
        Me.lblRequestTo.BorderVisible = True
        Me.lblRequestTo.FieldName = Nothing
        Me.lblRequestTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequestTo.Location = New System.Drawing.Point(264, 57)
        Me.lblRequestTo.Name = "lblRequestTo"
        Me.lblRequestTo.Size = New System.Drawing.Size(326, 18)
        Me.lblRequestTo.TabIndex = 52
        Me.lblRequestTo.TextWrap = False
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(385, 10)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 0
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(6, 58)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel7.TabIndex = 51
        Me.MyLabel7.Text = "Request To"
        '
        'textRequestCode
        '
        Me.textRequestCode.FieldName = Nothing
        Me.textRequestCode.Location = New System.Drawing.Point(85, 10)
        Me.textRequestCode.MendatroryField = True
        Me.textRequestCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.textRequestCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.textRequestCode.MyLinkLable1 = Me.lblvandorno
        Me.textRequestCode.MyLinkLable2 = Nothing
        Me.textRequestCode.MyMaxLength = 32767
        Me.textRequestCode.MyReadOnly = False
        Me.textRequestCode.Name = "textRequestCode"
        Me.textRequestCode.Size = New System.Drawing.Size(300, 21)
        Me.textRequestCode.TabIndex = 7
        Me.textRequestCode.TabStop = False
        Me.textRequestCode.Value = ""
        '
        'lblRequestBy
        '
        Me.lblRequestBy.AutoSize = False
        Me.lblRequestBy.BorderVisible = True
        Me.lblRequestBy.FieldName = Nothing
        Me.lblRequestBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequestBy.Location = New System.Drawing.Point(264, 34)
        Me.lblRequestBy.Name = "lblRequestBy"
        Me.lblRequestBy.Size = New System.Drawing.Size(327, 18)
        Me.lblRequestBy.TabIndex = 49
        Me.lblRequestBy.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 35)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 48
        Me.MyLabel4.Text = "Request By"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 80)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel1.TabIndex = 14
        Me.MyLabel1.Text = "Screen"
        '
        'txtScreen
        '
        Me.txtScreen.CalculationExpression = Nothing
        Me.txtScreen.FieldCode = Nothing
        Me.txtScreen.FieldDesc = Nothing
        Me.txtScreen.FieldMaxLength = 0
        Me.txtScreen.FieldName = Nothing
        Me.txtScreen.isCalculatedField = False
        Me.txtScreen.IsSourceFromTable = False
        Me.txtScreen.IsSourceFromValueList = False
        Me.txtScreen.IsUnique = False
        Me.txtScreen.Location = New System.Drawing.Point(85, 79)
        Me.txtScreen.MendatroryField = True
        Me.txtScreen.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScreen.MyLinkLable1 = Nothing
        Me.txtScreen.MyLinkLable2 = Me.lblScreen
        Me.txtScreen.MyReadOnly = False
        Me.txtScreen.MyShowMasterFormButton = False
        Me.txtScreen.Name = "txtScreen"
        Me.txtScreen.ReferenceFieldDesc = Nothing
        Me.txtScreen.ReferenceFieldName = Nothing
        Me.txtScreen.ReferenceTableName = Nothing
        Me.txtScreen.Size = New System.Drawing.Size(173, 18)
        Me.txtScreen.TabIndex = 5
        Me.txtScreen.Value = ""
        '
        'lblScreen
        '
        Me.lblScreen.AutoSize = False
        Me.lblScreen.BorderVisible = True
        Me.lblScreen.FieldName = Nothing
        Me.lblScreen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScreen.Location = New System.Drawing.Point(264, 79)
        Me.lblScreen.Name = "lblScreen"
        Me.lblScreen.Size = New System.Drawing.Size(327, 18)
        Me.lblScreen.TabIndex = 32
        Me.lblScreen.TextWrap = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(71.0!, 24.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(603, 338)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(603, 338)
        Me.UcAttachment1.TabIndex = 2
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(82.0!, 24.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(603, 364)
        Me.pvpCustomFields.Text = "Custum Fields"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(159, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 20)
        Me.btnPost.TabIndex = 111
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(772, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(83, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(8, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(603, 364)
        Me.UcCustomFields1.TabIndex = 2
        '
        'frmRequestMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 497)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmRequestMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmRequestMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRequestTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRequestBy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpRequestDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRequestTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRequestBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScreen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents textRequestCode As common.UserControls.txtNavigator
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtScreen As common.UserControls.txtFinder
    Friend WithEvents lblScreen As common.Controls.MyLabel
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnhead_ex As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btngrid_ex As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnhead_im As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btngrid_im As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblRequestTo As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblRequestBy As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtReason As common.Controls.MyTextBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As XpertERPCommanServices.ucCustomFields
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents dtpRequestDate As common.Controls.MyDateTimePicker
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRequestTo As common.Controls.MyTextBox
    Friend WithEvents txtRequestBy As common.Controls.MyTextBox
    Friend WithEvents btnPost As RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents UsLock2 As common.usLock
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
End Class

