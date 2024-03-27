<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorQuotation
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblRequistionNo = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRFQNo = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtVendorQuotationDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.txtRmks = New common.Controls.MyTextBox()
        Me.txtVendorQuotationNo = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.btnAmendment = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.Setting = New Telerik.WinControls.UI.RadMenuItem()
        Me.saveLayoutbtn = New Telerik.WinControls.UI.RadMenuItem()
        Me.DeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimporthead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimportdetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexporthead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexportdetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblRequistionNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorQuotationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorQuotationNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(884, 452)
        Me.SplitContainer1.SplitterDistance = 420
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(884, 420)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblRequistionNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendor)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtRFQNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorQuotationDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtRmks)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorQuotationNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(105.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(863, 374)
        Me.RadPageViewPage1.Text = "Vendor Quotation"
        '
        'lblRequistionNo
        '
        Me.lblRequistionNo.AutoSize = False
        Me.lblRequistionNo.BorderVisible = True
        Me.lblRequistionNo.FieldName = Nothing
        Me.lblRequistionNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequistionNo.Location = New System.Drawing.Point(86, 81)
        Me.lblRequistionNo.Name = "lblRequistionNo"
        Me.lblRequistionNo.Size = New System.Drawing.Size(372, 18)
        Me.lblRequistionNo.TabIndex = 15
        Me.lblRequistionNo.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(1, 82)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel4.TabIndex = 16
        Me.MyLabel4.Text = "Requisition No"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(719, 3)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 4
        Me.chkOnHold.Text = "On Hold"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(235, 22)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(223, 18)
        Me.lblVendorName.TabIndex = 36
        Me.lblVendorName.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(1, 23)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel3.TabIndex = 19
        Me.MyLabel3.Text = "Vendor"
        '
        'txtVendor
        '
        Me.txtVendor.CalculationExpression = Nothing
        Me.txtVendor.FieldCode = Nothing
        Me.txtVendor.FieldDesc = Nothing
        Me.txtVendor.FieldMaxLength = 0
        Me.txtVendor.FieldName = Nothing
        Me.txtVendor.isCalculatedField = False
        Me.txtVendor.IsSourceFromTable = False
        Me.txtVendor.IsSourceFromValueList = False
        Me.txtVendor.IsUnique = False
        Me.txtVendor.Location = New System.Drawing.Point(86, 22)
        Me.txtVendor.MendatroryField = True
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.MyLabel3
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyReadOnly = False
        Me.txtVendor.MyShowMasterFormButton = False
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.ReferenceFieldDesc = Nothing
        Me.txtVendor.ReferenceFieldName = Nothing
        Me.txtVendor.ReferenceTableName = Nothing
        Me.txtVendor.Size = New System.Drawing.Size(143, 19)
        Me.txtVendor.TabIndex = 5
        Me.txtVendor.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(459, 3)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel2.TabIndex = 25
        Me.MyLabel2.Text = "RFQ No"
        '
        'txtRFQNo
        '
        Me.txtRFQNo.CalculationExpression = Nothing
        Me.txtRFQNo.FieldCode = Nothing
        Me.txtRFQNo.FieldDesc = Nothing
        Me.txtRFQNo.FieldMaxLength = 0
        Me.txtRFQNo.FieldName = Nothing
        Me.txtRFQNo.isCalculatedField = False
        Me.txtRFQNo.IsSourceFromTable = False
        Me.txtRFQNo.IsSourceFromValueList = False
        Me.txtRFQNo.IsUnique = False
        Me.txtRFQNo.Location = New System.Drawing.Point(543, 2)
        Me.txtRFQNo.MendatroryField = True
        Me.txtRFQNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRFQNo.MyLinkLable1 = Me.MyLabel2
        Me.txtRFQNo.MyLinkLable2 = Nothing
        Me.txtRFQNo.MyReadOnly = False
        Me.txtRFQNo.MyShowMasterFormButton = False
        Me.txtRFQNo.Name = "txtRFQNo"
        Me.txtRFQNo.ReferenceFieldDesc = Nothing
        Me.txtRFQNo.ReferenceFieldName = Nothing
        Me.txtRFQNo.ReferenceTableName = Nothing
        Me.txtRFQNo.Size = New System.Drawing.Size(137, 19)
        Me.txtRFQNo.TabIndex = 3
        Me.txtRFQNo.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(686, 23)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel1.TabIndex = 26
        Me.MyLabel1.Text = "Vendor Qtn Date"
        '
        'txtVendorQuotationDate
        '
        Me.txtVendorQuotationDate.CalculationExpression = Nothing
        Me.txtVendorQuotationDate.CustomFormat = "dd/MM/yyyy"
        Me.txtVendorQuotationDate.FieldCode = Nothing
        Me.txtVendorQuotationDate.FieldDesc = Nothing
        Me.txtVendorQuotationDate.FieldMaxLength = 0
        Me.txtVendorQuotationDate.FieldName = Nothing
        Me.txtVendorQuotationDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorQuotationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtVendorQuotationDate.isCalculatedField = False
        Me.txtVendorQuotationDate.IsSourceFromTable = False
        Me.txtVendorQuotationDate.IsSourceFromValueList = False
        Me.txtVendorQuotationDate.IsUnique = False
        Me.txtVendorQuotationDate.Location = New System.Drawing.Point(783, 22)
        Me.txtVendorQuotationDate.MendatroryField = True
        Me.txtVendorQuotationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVendorQuotationDate.MyLinkLable1 = Me.MyLabel1
        Me.txtVendorQuotationDate.MyLinkLable2 = Nothing
        Me.txtVendorQuotationDate.Name = "txtVendorQuotationDate"
        Me.txtVendorQuotationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVendorQuotationDate.ReferenceFieldDesc = Nothing
        Me.txtVendorQuotationDate.ReferenceFieldName = Nothing
        Me.txtVendorQuotationDate.ReferenceTableName = Nothing
        Me.txtVendorQuotationDate.Size = New System.Drawing.Size(75, 18)
        Me.txtVendorQuotationDate.TabIndex = 7
        Me.txtVendorQuotationDate.TabStop = False
        Me.txtVendorQuotationDate.Text = "13/06/2011"
        Me.txtVendorQuotationDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel27
        '
        Me.RadLabel27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(673, 358)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel27.TabIndex = 14
        Me.RadLabel27.Text = "Total Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(752, 356)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 13
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(1, 43)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel5.TabIndex = 18
        Me.RadLabel5.Text = "Description"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 100)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(856, 251)
        Me.RadGroupBox2.TabIndex = 12
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
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(836, 221)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(459, 43)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel3.TabIndex = 23
        Me.RadLabel3.Text = "Reference No"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(459, 62)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 22
        Me.RadLabel2.Text = "Comment"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(1, 62)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 17
        Me.RadLabel8.Text = "Remarks"
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(459, 23)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel7.TabIndex = 24
        Me.RadLabel7.Text = "Vendor Qtn No"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(344, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 21
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(1, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Quotation No"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(783, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(75, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 16
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(86, 1)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(230, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
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
        Me.txtComment.Location = New System.Drawing.Point(543, 61)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel2
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(315, 18)
        Me.txtComment.TabIndex = 11
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(543, 42)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel3
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(315, 18)
        Me.txtRefNo.TabIndex = 9
        '
        'txtRmks
        '
        Me.txtRmks.CalculationExpression = Nothing
        Me.txtRmks.FieldCode = Nothing
        Me.txtRmks.FieldDesc = Nothing
        Me.txtRmks.FieldMaxLength = 0
        Me.txtRmks.FieldName = Nothing
        Me.txtRmks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRmks.isCalculatedField = False
        Me.txtRmks.IsSourceFromTable = False
        Me.txtRmks.IsSourceFromValueList = False
        Me.txtRmks.IsUnique = False
        Me.txtRmks.Location = New System.Drawing.Point(86, 61)
        Me.txtRmks.MaxLength = 200
        Me.txtRmks.MendatroryField = False
        Me.txtRmks.MyLinkLable1 = Me.RadLabel8
        Me.txtRmks.MyLinkLable2 = Nothing
        Me.txtRmks.Name = "txtRmks"
        Me.txtRmks.ReferenceFieldDesc = Nothing
        Me.txtRmks.ReferenceFieldName = Nothing
        Me.txtRmks.ReferenceTableName = Nothing
        Me.txtRmks.Size = New System.Drawing.Size(372, 18)
        Me.txtRmks.TabIndex = 10
        '
        'txtVendorQuotationNo
        '
        Me.txtVendorQuotationNo.CalculationExpression = Nothing
        Me.txtVendorQuotationNo.FieldCode = Nothing
        Me.txtVendorQuotationNo.FieldDesc = Nothing
        Me.txtVendorQuotationNo.FieldMaxLength = 0
        Me.txtVendorQuotationNo.FieldName = Nothing
        Me.txtVendorQuotationNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorQuotationNo.isCalculatedField = False
        Me.txtVendorQuotationNo.IsSourceFromTable = False
        Me.txtVendorQuotationNo.IsSourceFromValueList = False
        Me.txtVendorQuotationNo.IsUnique = False
        Me.txtVendorQuotationNo.Location = New System.Drawing.Point(543, 22)
        Me.txtVendorQuotationNo.MaxLength = 50
        Me.txtVendorQuotationNo.MendatroryField = True
        Me.txtVendorQuotationNo.MyLinkLable1 = Me.RadLabel7
        Me.txtVendorQuotationNo.MyLinkLable2 = Nothing
        Me.txtVendorQuotationNo.Name = "txtVendorQuotationNo"
        Me.txtVendorQuotationNo.ReferenceFieldDesc = Nothing
        Me.txtVendorQuotationNo.ReferenceFieldName = Nothing
        Me.txtVendorQuotationNo.ReferenceTableName = Nothing
        Me.txtVendorQuotationNo.Size = New System.Drawing.Size(137, 18)
        Me.txtVendorQuotationNo.TabIndex = 6
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
        Me.txtDate.Location = New System.Drawing.Point(378, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(86, 42)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel5
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(372, 18)
        Me.txtDesc.TabIndex = 8
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(317, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(863, 371)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(863, 371)
        Me.UcAttachment1.TabIndex = 0
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(863, 371)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(863, 371)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnAmendment
        '
        Me.btnAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAmendment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmendment.Location = New System.Drawing.Point(408, 5)
        Me.btnAmendment.Name = "btnAmendment"
        Me.btnAmendment.Size = New System.Drawing.Size(69, 22)
        Me.btnAmendment.TabIndex = 8
        Me.btnAmendment.Text = "Amendment"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(79, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(154, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(810, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadMenu1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(884, 25)
        Me.Panel2.TabIndex = 6
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Setting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(884, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'Setting
        '
        Me.Setting.AccessibleDescription = "Layout"
        Me.Setting.AccessibleName = "Layout"
        Me.Setting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.saveLayoutbtn, Me.DeleteLayout, Me.btnimport, Me.btnexport})
        Me.Setting.Name = "Setting"
        Me.Setting.Text = "Setting"
        '
        'saveLayoutbtn
        '
        Me.saveLayoutbtn.Name = "saveLayoutbtn"
        Me.saveLayoutbtn.Text = "Save Layout"
        '
        'DeleteLayout
        '
        Me.DeleteLayout.Name = "DeleteLayout"
        Me.DeleteLayout.Text = "Delete Layout"
        '
        'btnimport
        '
        Me.btnimport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnimporthead, Me.btnimportdetail})
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'btnimporthead
        '
        Me.btnimporthead.Name = "btnimporthead"
        Me.btnimporthead.Text = "Import Head"
        '
        'btnimportdetail
        '
        Me.btnimportdetail.Name = "btnimportdetail"
        Me.btnimportdetail.Text = "Import Detail"
        '
        'btnexport
        '
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexporthead, Me.btnexportdetail})
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnexporthead
        '
        Me.btnexporthead.Name = "btnexporthead"
        Me.btnexporthead.Text = "Export Head"
        '
        'btnexportdetail
        '
        Me.btnexportdetail.Name = "btnexportdetail"
        Me.btnexportdetail.Text = "Export Detail"
        '
        'frmVendorQuotation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 452)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmVendorQuotation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Quotation"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblRequistionNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorQuotationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorQuotationNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRmks As common.Controls.MyTextBox
    Friend WithEvents txtVendorQuotationNo As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtVendorQuotationDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRFQNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtFinder
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblRequistionNo As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Setting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents saveLayoutbtn As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimporthead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimportdetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexporthead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexportdetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnAmendment As Telerik.WinControls.UI.RadButton

End Class

