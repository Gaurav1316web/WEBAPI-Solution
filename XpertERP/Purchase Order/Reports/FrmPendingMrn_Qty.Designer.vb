<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPendingMrn_Qty
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
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkCancel = New common.Controls.MyCheckBox()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox13 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.rdobtnCompleted = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdobtnSRNPartial = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdobtnSRNnever = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdobtnAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblstatus = New System.Windows.Forms.Label()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVendor1 = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chk_Vendor_Select = New common.Controls.MyRadioButton()
        Me.chk_Vendor_All = New common.Controls.MyRadioButton()
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDoc = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chk_Doc_Select = New common.Controls.MyRadioButton()
        Me.chkall = New common.Controls.MyRadioButton()
        Me.dtpToDate1 = New common.Controls.MyDateTimePicker()
        Me.dtpFromdate1 = New common.Controls.MyDateTimePicker()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton()
        Me.btnClose1 = New Telerik.WinControls.UI.RadButton()
        Me.btnprint1 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.chkCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox13.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnCompleted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnSRNPartial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnSRNnever, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chk_Vendor_Select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_Vendor_All, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chk_Doc_Select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox7.Controls.Add(Me.chkCancel)
        Me.RadGroupBox7.Controls.Add(Me.lblVendor)
        Me.RadGroupBox7.Controls.Add(Me.txtVendor)
        Me.RadGroupBox7.Controls.Add(Me.lblLocation)
        Me.RadGroupBox7.Controls.Add(Me.txtLocation)
        Me.RadGroupBox7.Controls.Add(Me.lblDocNo)
        Me.RadGroupBox7.Controls.Add(Me.txtDocNo)
        Me.RadGroupBox7.Controls.Add(Me.RadGroupBox13)
        Me.RadGroupBox7.Controls.Add(Me.rdobtnCompleted)
        Me.RadGroupBox7.Controls.Add(Me.rdobtnSRNPartial)
        Me.RadGroupBox7.Controls.Add(Me.rdobtnSRNnever)
        Me.RadGroupBox7.Controls.Add(Me.rdobtnAll)
        Me.RadGroupBox7.Controls.Add(Me.lblstatus)
        Me.RadGroupBox7.Controls.Add(Me.RadGroupBox8)
        Me.RadGroupBox7.Controls.Add(Me.RadGroupBox9)
        Me.RadGroupBox7.Controls.Add(Me.dtpToDate1)
        Me.RadGroupBox7.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox7.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox7.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(1182, 545)
        Me.RadGroupBox7.TabIndex = 10
        '
        'chkCancel
        '
        Me.chkCancel.Location = New System.Drawing.Point(473, 10)
        Me.chkCancel.MyLinkLable1 = Nothing
        Me.chkCancel.MyLinkLable2 = Nothing
        Me.chkCancel.Name = "chkCancel"
        Me.chkCancel.Size = New System.Drawing.Size(82, 18)
        Me.chkCancel.TabIndex = 356
        Me.chkCancel.Tag1 = Nothing
        Me.chkCancel.Text = "Cancel MRN"
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(13, 122)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 18)
        Me.lblVendor.TabIndex = 338
        Me.lblVendor.Text = "Vendor"
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(98, 121)
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.lblVendor
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyNullText = "All"
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(344, 19)
        Me.txtVendor.TabIndex = 337
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 98)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 336
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(98, 97)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 335
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.Location = New System.Drawing.Point(13, 74)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 334
        Me.lblDocNo.Text = "Document No."
        '
        'txtDocNo
        '
        Me.txtDocNo.arrDispalyMember = Nothing
        Me.txtDocNo.arrValueMember = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(98, 73)
        Me.txtDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.MyLinkLable1 = Me.lblDocNo
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyNullText = "All"
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(344, 19)
        Me.txtDocNo.TabIndex = 333
        '
        'RadGroupBox13
        '
        Me.RadGroupBox13.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox13.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox13.Controls.Add(Me.Panel9)
        Me.RadGroupBox13.HeaderText = " Location"
        Me.RadGroupBox13.Location = New System.Drawing.Point(1083, 85)
        Me.RadGroupBox13.Name = "RadGroupBox13"
        Me.RadGroupBox13.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox13.Size = New System.Drawing.Size(86, 54)
        Me.RadGroupBox13.TabIndex = 74
        Me.RadGroupBox13.Text = " Location"
        Me.RadGroupBox13.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(66, 0)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkLocationSelect)
        Me.Panel9.Controls.Add(Me.chkLocationAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(66, 25)
        Me.Panel9.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(193, 3)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(141, 4)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'rdobtnCompleted
        '
        Me.rdobtnCompleted.Location = New System.Drawing.Point(366, 35)
        Me.rdobtnCompleted.Name = "rdobtnCompleted"
        Me.rdobtnCompleted.Size = New System.Drawing.Size(75, 18)
        Me.rdobtnCompleted.TabIndex = 12
        Me.rdobtnCompleted.Text = "Completed"
        '
        'rdobtnSRNPartial
        '
        Me.rdobtnSRNPartial.Location = New System.Drawing.Point(241, 35)
        Me.rdobtnSRNPartial.Name = "rdobtnSRNPartial"
        Me.rdobtnSRNPartial.Size = New System.Drawing.Size(118, 18)
        Me.rdobtnSRNPartial.TabIndex = 12
        Me.rdobtnSRNPartial.Text = "SRN Partial Created"
        '
        'rdobtnSRNnever
        '
        Me.rdobtnSRNnever.Location = New System.Drawing.Point(117, 35)
        Me.rdobtnSRNnever.Name = "rdobtnSRNnever"
        Me.rdobtnSRNnever.Size = New System.Drawing.Size(116, 18)
        Me.rdobtnSRNnever.TabIndex = 12
        Me.rdobtnSRNnever.Text = "SRN Never Created"
        '
        'rdobtnAll
        '
        Me.rdobtnAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdobtnAll.Location = New System.Drawing.Point(62, 35)
        Me.rdobtnAll.Name = "rdobtnAll"
        Me.rdobtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rdobtnAll.TabIndex = 11
        Me.rdobtnAll.Text = "All"
        Me.rdobtnAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Location = New System.Drawing.Point(17, 40)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(39, 13)
        Me.lblstatus.TabIndex = 10
        Me.lblstatus.Text = "Status"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbgVendor1)
        Me.RadGroupBox8.Controls.Add(Me.Panel5)
        Me.RadGroupBox8.HeaderText = "Vendor"
        Me.RadGroupBox8.Location = New System.Drawing.Point(1110, 142)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(38, 63)
        Me.RadGroupBox8.TabIndex = 4
        Me.RadGroupBox8.Text = "Vendor"
        Me.RadGroupBox8.Visible = False
        '
        'cbgVendor1
        '
        Me.cbgVendor1.AccessibleName = "cbgVendor"
        Me.cbgVendor1.CheckedValue = Nothing
        Me.cbgVendor1.DataSource = Nothing
        Me.cbgVendor1.DisplayMember = "Name"
        Me.cbgVendor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor1.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor1.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor1.MyShowHeadrText = False
        Me.cbgVendor1.Name = "cbgVendor1"
        Me.cbgVendor1.Size = New System.Drawing.Size(18, 13)
        Me.cbgVendor1.TabIndex = 1
        Me.cbgVendor1.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chk_Vendor_Select)
        Me.Panel5.Controls.Add(Me.chk_Vendor_All)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(18, 20)
        Me.Panel5.TabIndex = 0
        '
        'chk_Vendor_Select
        '
        Me.chk_Vendor_Select.AccessibleName = "chk_Vendor_Select"
        Me.chk_Vendor_Select.Location = New System.Drawing.Point(192, 1)
        Me.chk_Vendor_Select.MyLinkLable1 = Nothing
        Me.chk_Vendor_Select.MyLinkLable2 = Nothing
        Me.chk_Vendor_Select.Name = "chk_Vendor_Select"
        Me.chk_Vendor_Select.Size = New System.Drawing.Size(50, 18)
        Me.chk_Vendor_Select.TabIndex = 1
        Me.chk_Vendor_Select.Text = "Select"
        '
        'chk_Vendor_All
        '
        Me.chk_Vendor_All.AccessibleName = "chk_Vendor_All"
        Me.chk_Vendor_All.Location = New System.Drawing.Point(141, 1)
        Me.chk_Vendor_All.MyLinkLable1 = Nothing
        Me.chk_Vendor_All.MyLinkLable2 = Nothing
        Me.chk_Vendor_All.Name = "chk_Vendor_All"
        Me.chk_Vendor_All.Size = New System.Drawing.Size(33, 18)
        Me.chk_Vendor_All.TabIndex = 0
        Me.chk_Vendor_All.Text = "All"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.cbgDoc)
        Me.RadGroupBox9.Controls.Add(Me.Panel6)
        Me.RadGroupBox9.HeaderText = "Document No"
        Me.RadGroupBox9.Location = New System.Drawing.Point(1110, 34)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(38, 45)
        Me.RadGroupBox9.TabIndex = 3
        Me.RadGroupBox9.Text = "Document No"
        Me.RadGroupBox9.Visible = False
        '
        'cbgDoc
        '
        Me.cbgDoc.AccessibleName = "cbgDoc"
        Me.cbgDoc.CheckedValue = Nothing
        Me.cbgDoc.DataSource = Nothing
        Me.cbgDoc.DisplayMember = "Name"
        Me.cbgDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgDoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDoc.MyShowHeadrText = False
        Me.cbgDoc.Name = "cbgDoc"
        Me.cbgDoc.Size = New System.Drawing.Size(18, 0)
        Me.cbgDoc.TabIndex = 1
        Me.cbgDoc.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chk_Doc_Select)
        Me.Panel6.Controls.Add(Me.chkall)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(18, 20)
        Me.Panel6.TabIndex = 0
        '
        'chk_Doc_Select
        '
        Me.chk_Doc_Select.AccessibleName = "chk_Doc_Select"
        Me.chk_Doc_Select.Location = New System.Drawing.Point(192, 1)
        Me.chk_Doc_Select.MyLinkLable1 = Nothing
        Me.chk_Doc_Select.MyLinkLable2 = Nothing
        Me.chk_Doc_Select.Name = "chk_Doc_Select"
        Me.chk_Doc_Select.Size = New System.Drawing.Size(50, 18)
        Me.chk_Doc_Select.TabIndex = 1
        Me.chk_Doc_Select.Text = "Select"
        '
        'chkall
        '
        Me.chkall.AccessibleName = "chkAll"
        Me.chkall.Location = New System.Drawing.Point(141, 1)
        Me.chkall.MyLinkLable1 = Nothing
        Me.chkall.MyLinkLable2 = Nothing
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(33, 18)
        Me.chkall.TabIndex = 0
        Me.chkall.Text = "All"
        '
        'dtpToDate1
        '
        Me.dtpToDate1.AccessibleName = "dtpToDate"
        Me.dtpToDate1.CalculationExpression = Nothing
        Me.dtpToDate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate1.FieldCode = Nothing
        Me.dtpToDate1.FieldDesc = Nothing
        Me.dtpToDate1.FieldMaxLength = 0
        Me.dtpToDate1.FieldName = Nothing
        Me.dtpToDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate1.isCalculatedField = False
        Me.dtpToDate1.IsSourceFromTable = False
        Me.dtpToDate1.IsSourceFromValueList = False
        Me.dtpToDate1.IsUnique = False
        Me.dtpToDate1.Location = New System.Drawing.Point(366, 9)
        Me.dtpToDate1.MendatroryField = False
        Me.dtpToDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.MyLinkLable1 = Nothing
        Me.dtpToDate1.MyLinkLable2 = Nothing
        Me.dtpToDate1.Name = "dtpToDate1"
        Me.dtpToDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate1.ReferenceFieldDesc = Nothing
        Me.dtpToDate1.ReferenceFieldName = Nothing
        Me.dtpToDate1.ReferenceTableName = Nothing
        Me.dtpToDate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate1.TabIndex = 1
        Me.dtpToDate1.TabStop = False
        Me.dtpToDate1.Text = "14-09-2011"
        Me.dtpToDate1.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromdate"
        Me.dtpFromdate1.CalculationExpression = Nothing
        Me.dtpFromdate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpFromdate1.FieldCode = Nothing
        Me.dtpFromdate1.FieldDesc = Nothing
        Me.dtpFromdate1.FieldMaxLength = 0
        Me.dtpFromdate1.FieldName = Nothing
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.isCalculatedField = False
        Me.dtpFromdate1.IsSourceFromTable = False
        Me.dtpFromdate1.IsSourceFromValueList = False
        Me.dtpFromdate1.IsUnique = False
        Me.dtpFromdate1.Location = New System.Drawing.Point(70, 8)
        Me.dtpFromdate1.MendatroryField = False
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.MyLinkLable1 = Nothing
        Me.dtpFromdate1.MyLinkLable2 = Nothing
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.ReferenceFieldDesc = Nothing
        Me.dtpFromdate1.ReferenceFieldName = Nothing
        Me.dtpFromdate1.ReferenceTableName = Nothing
        Me.dtpFromdate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpFromdate1.TabIndex = 0
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "14-09-2011"
        Me.dtpFromdate1.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Location = New System.Drawing.Point(315, 9)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel5.TabIndex = 9
        Me.RadLabel5.Text = "To Date"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Location = New System.Drawing.Point(7, 9)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel6.TabIndex = 8
        Me.RadLabel6.Text = "From Date"
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleName = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(83, 7)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 6
        Me.btnreset1.Text = "Reset"
        '
        'btnClose1
        '
        Me.btnClose1.AccessibleName = "btnClose"
        Me.btnClose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose1.Location = New System.Drawing.Point(1120, 6)
        Me.btnClose1.Name = "btnClose1"
        Me.btnClose1.Size = New System.Drawing.Size(68, 18)
        Me.btnClose1.TabIndex = 7
        Me.btnClose1.Text = "Close"
        '
        'btnprint1
        '
        Me.btnprint1.AccessibleName = "btnPrint"
        Me.btnprint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint1.Location = New System.Drawing.Point(157, 7)
        Me.btnprint1.Name = "btnprint1"
        Me.btnprint1.Size = New System.Drawing.Size(68, 18)
        Me.btnprint1.TabIndex = 5
        Me.btnprint1.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1203, 630)
        Me.SplitContainer1.SplitterDistance = 593
        Me.SplitContainer1.TabIndex = 9
        '
        'RadPageView1
        '
        Me.RadPageView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1203, 593)
        Me.RadPageView1.TabIndex = 12
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1182, 545)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1182, 545)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.EnableFiltering = True
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1182, 545)
        Me.Gv1.TabIndex = 0
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(231, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 20)
        Me.btnExport.TabIndex = 134
        Me.btnExport.Text = "Export"
        '
        'Export
        '
        Me.Export.Image = Global.ERP.My.Resources.Resources.MSE
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'PDF
        '
        Me.PDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(11, 6)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 20)
        Me.btnRefresh.TabIndex = 133
        Me.btnRefresh.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1203, 20)
        Me.RadMenu1.TabIndex = 10
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'FrmPendingMrn_Qty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1203, 650)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmPendingMrn_Qty"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Pending MRN"
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.chkCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox13.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnCompleted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnSRNPartial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnSRNnever, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chk_Vendor_Select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_Vendor_All, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chk_Doc_Select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor1 As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chk_Vendor_Select As common.Controls.MyRadioButton
    Friend WithEvents chk_Vendor_All As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDoc As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chk_Doc_Select As common.Controls.MyRadioButton
    Friend WithEvents chkall As common.Controls.MyRadioButton
    Friend WithEvents dtpToDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromdate1 As common.Controls.MyDateTimePicker
    Friend WithEvents btnClose1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdobtnAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdobtnCompleted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdobtnSRNPartial As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdobtnSRNnever As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox13 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkCancel As common.Controls.MyCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

