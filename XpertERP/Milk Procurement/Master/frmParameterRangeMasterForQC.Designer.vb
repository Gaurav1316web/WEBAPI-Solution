<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParameterRangeMasterForQC
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParameterRangeMasterForQC))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtParameterMapping = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtDeductionRatio3 = New common.MyNumBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtDeductionURange3 = New common.MyNumBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtDeductionLRange3 = New common.MyNumBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtDeductionRatio2 = New common.MyNumBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDeductionURange2 = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtDeductionLRange2 = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtQcStatus = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.btnUpdateDeduction = New Telerik.WinControls.UI.RadButton()
        Me.txtUpperRange = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtLowerRange = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblParamDesc = New common.Controls.MyLabel()
        Me.txtDeductionRatio = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtDeductionURange = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDeductionLRange = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.fndParameterCode = New common.UserControls.txtFinder()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnDedMethodFixed = New common.Controls.MyRadioButton()
        Me.rbtnDedMethodRatio = New common.Controls.MyRadioButton()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
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
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionRatio3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionURange3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionLRange3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionRatio2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionURange2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionLRange2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQcStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUpperRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLowerRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParamDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionURange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionLRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnDedMethodFixed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDedMethodRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(807, 508)
        Me.SplitContainer1.SplitterDistance = 471
        Me.SplitContainer1.TabIndex = 1
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
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(6)
        Me.SplitContainer2.Size = New System.Drawing.Size(801, 465)
        Me.SplitContainer2.SplitterDistance = 28
        Me.SplitContainer2.TabIndex = 19
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(5, 5)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(791, 20)
        Me.RadMenu1.TabIndex = 17
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(6, 6)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(789, 421)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(768, 373)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Parameter Range Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(3, 18, 3, 3)
        Me.RadGroupBox1.Size = New System.Drawing.Size(768, 373)
        Me.RadGroupBox1.TabIndex = 18
        Me.RadGroupBox1.Text = "Parameter Range Detail"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(762, 352)
        Me.gv.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(768, 373)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(768, 373)
        Me.UcAttachment1.TabIndex = 2
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.txtParameterMapping)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionRatio3)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionURange3)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionLRange3)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionRatio2)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionURange2)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionLRange2)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage3.Controls.Add(Me.txtQcStatus)
        Me.RadPageViewPage3.Controls.Add(Me.btnUpdateDeduction)
        Me.RadPageViewPage3.Controls.Add(Me.txtUpperRange)
        Me.RadPageViewPage3.Controls.Add(Me.txtLowerRange)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage3.Controls.Add(Me.lblParamDesc)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionRatio)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionURange)
        Me.RadPageViewPage3.Controls.Add(Me.txtDeductionLRange)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage3.Controls.Add(Me.fndParameterCode)
        Me.RadPageViewPage3.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(108.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(768, 373)
        Me.RadPageViewPage3.Text = "Update Deduction"
        '
        'txtParameterMapping
        '
        Me.txtParameterMapping.arrDispalyMember = Nothing
        Me.txtParameterMapping.arrValueMember = Nothing
        Me.txtParameterMapping.Location = New System.Drawing.Point(119, 326)
        Me.txtParameterMapping.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParameterMapping.MyLinkLable1 = Me.MyLabel15
        Me.txtParameterMapping.MyLinkLable2 = Nothing
        Me.txtParameterMapping.MyNullText = ""
        Me.txtParameterMapping.Name = "txtParameterMapping"
        Me.txtParameterMapping.Size = New System.Drawing.Size(246, 19)
        Me.txtParameterMapping.TabIndex = 429
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(3, 326)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel15.TabIndex = 359
        Me.MyLabel15.Text = "Parameter Mapping"
        '
        'txtDeductionRatio3
        '
        Me.txtDeductionRatio3.BackColor = System.Drawing.Color.White
        Me.txtDeductionRatio3.CalculationExpression = Nothing
        Me.txtDeductionRatio3.DecimalPlaces = 2
        Me.txtDeductionRatio3.FieldCode = Nothing
        Me.txtDeductionRatio3.FieldDesc = Nothing
        Me.txtDeductionRatio3.FieldMaxLength = 0
        Me.txtDeductionRatio3.FieldName = Nothing
        Me.txtDeductionRatio3.isCalculatedField = False
        Me.txtDeductionRatio3.IsSourceFromTable = False
        Me.txtDeductionRatio3.IsSourceFromValueList = False
        Me.txtDeductionRatio3.IsUnique = False
        Me.txtDeductionRatio3.Location = New System.Drawing.Point(118, 304)
        Me.txtDeductionRatio3.MaxLength = 8
        Me.txtDeductionRatio3.MendatroryField = False
        Me.txtDeductionRatio3.MyLinkLable1 = Me.MyLabel14
        Me.txtDeductionRatio3.MyLinkLable2 = Nothing
        Me.txtDeductionRatio3.Name = "txtDeductionRatio3"
        Me.txtDeductionRatio3.ReferenceFieldDesc = Nothing
        Me.txtDeductionRatio3.ReferenceFieldName = Nothing
        Me.txtDeductionRatio3.ReferenceTableName = Nothing
        Me.txtDeductionRatio3.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionRatio3.TabIndex = 358
        Me.txtDeductionRatio3.Text = "0"
        Me.txtDeductionRatio3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionRatio3.Value = 0R
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(2, 304)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel14.TabIndex = 352
        Me.MyLabel14.Text = "Deduction Value3"
        '
        'txtDeductionURange3
        '
        Me.txtDeductionURange3.BackColor = System.Drawing.Color.White
        Me.txtDeductionURange3.CalculationExpression = Nothing
        Me.txtDeductionURange3.DecimalPlaces = 2
        Me.txtDeductionURange3.FieldCode = Nothing
        Me.txtDeductionURange3.FieldDesc = Nothing
        Me.txtDeductionURange3.FieldMaxLength = 0
        Me.txtDeductionURange3.FieldName = Nothing
        Me.txtDeductionURange3.isCalculatedField = False
        Me.txtDeductionURange3.IsSourceFromTable = False
        Me.txtDeductionURange3.IsSourceFromValueList = False
        Me.txtDeductionURange3.IsUnique = False
        Me.txtDeductionURange3.Location = New System.Drawing.Point(118, 282)
        Me.txtDeductionURange3.MaxLength = 8
        Me.txtDeductionURange3.MendatroryField = False
        Me.txtDeductionURange3.MyLinkLable1 = Me.MyLabel12
        Me.txtDeductionURange3.MyLinkLable2 = Nothing
        Me.txtDeductionURange3.Name = "txtDeductionURange3"
        Me.txtDeductionURange3.ReferenceFieldDesc = Nothing
        Me.txtDeductionURange3.ReferenceFieldName = Nothing
        Me.txtDeductionURange3.ReferenceTableName = Nothing
        Me.txtDeductionURange3.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionURange3.TabIndex = 357
        Me.txtDeductionURange3.Text = "0"
        Me.txtDeductionURange3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionURange3.Value = 0R
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(2, 282)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel12.TabIndex = 350
        Me.MyLabel12.Text = "Deduction U Range3"
        '
        'txtDeductionLRange3
        '
        Me.txtDeductionLRange3.BackColor = System.Drawing.Color.White
        Me.txtDeductionLRange3.CalculationExpression = Nothing
        Me.txtDeductionLRange3.DecimalPlaces = 2
        Me.txtDeductionLRange3.FieldCode = Nothing
        Me.txtDeductionLRange3.FieldDesc = Nothing
        Me.txtDeductionLRange3.FieldMaxLength = 0
        Me.txtDeductionLRange3.FieldName = Nothing
        Me.txtDeductionLRange3.isCalculatedField = False
        Me.txtDeductionLRange3.IsSourceFromTable = False
        Me.txtDeductionLRange3.IsSourceFromValueList = False
        Me.txtDeductionLRange3.IsUnique = False
        Me.txtDeductionLRange3.Location = New System.Drawing.Point(118, 260)
        Me.txtDeductionLRange3.MaxLength = 8
        Me.txtDeductionLRange3.MendatroryField = False
        Me.txtDeductionLRange3.MyLinkLable1 = Me.MyLabel10
        Me.txtDeductionLRange3.MyLinkLable2 = Nothing
        Me.txtDeductionLRange3.Name = "txtDeductionLRange3"
        Me.txtDeductionLRange3.ReferenceFieldDesc = Nothing
        Me.txtDeductionLRange3.ReferenceFieldName = Nothing
        Me.txtDeductionLRange3.ReferenceTableName = Nothing
        Me.txtDeductionLRange3.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionLRange3.TabIndex = 356
        Me.txtDeductionLRange3.Text = "0"
        Me.txtDeductionLRange3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionLRange3.Value = 0R
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(2, 260)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel10.TabIndex = 348
        Me.MyLabel10.Text = "Deduction L Range3"
        '
        'txtDeductionRatio2
        '
        Me.txtDeductionRatio2.BackColor = System.Drawing.Color.White
        Me.txtDeductionRatio2.CalculationExpression = Nothing
        Me.txtDeductionRatio2.DecimalPlaces = 2
        Me.txtDeductionRatio2.FieldCode = Nothing
        Me.txtDeductionRatio2.FieldDesc = Nothing
        Me.txtDeductionRatio2.FieldMaxLength = 0
        Me.txtDeductionRatio2.FieldName = Nothing
        Me.txtDeductionRatio2.isCalculatedField = False
        Me.txtDeductionRatio2.IsSourceFromTable = False
        Me.txtDeductionRatio2.IsSourceFromValueList = False
        Me.txtDeductionRatio2.IsUnique = False
        Me.txtDeductionRatio2.Location = New System.Drawing.Point(118, 238)
        Me.txtDeductionRatio2.MaxLength = 8
        Me.txtDeductionRatio2.MendatroryField = False
        Me.txtDeductionRatio2.MyLinkLable1 = Me.MyLabel13
        Me.txtDeductionRatio2.MyLinkLable2 = Nothing
        Me.txtDeductionRatio2.Name = "txtDeductionRatio2"
        Me.txtDeductionRatio2.ReferenceFieldDesc = Nothing
        Me.txtDeductionRatio2.ReferenceFieldName = Nothing
        Me.txtDeductionRatio2.ReferenceTableName = Nothing
        Me.txtDeductionRatio2.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionRatio2.TabIndex = 355
        Me.txtDeductionRatio2.Text = "0"
        Me.txtDeductionRatio2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionRatio2.Value = 0R
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(2, 238)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel13.TabIndex = 351
        Me.MyLabel13.Text = "Deduction Value2"
        '
        'txtDeductionURange2
        '
        Me.txtDeductionURange2.BackColor = System.Drawing.Color.White
        Me.txtDeductionURange2.CalculationExpression = Nothing
        Me.txtDeductionURange2.DecimalPlaces = 2
        Me.txtDeductionURange2.FieldCode = Nothing
        Me.txtDeductionURange2.FieldDesc = Nothing
        Me.txtDeductionURange2.FieldMaxLength = 0
        Me.txtDeductionURange2.FieldName = Nothing
        Me.txtDeductionURange2.isCalculatedField = False
        Me.txtDeductionURange2.IsSourceFromTable = False
        Me.txtDeductionURange2.IsSourceFromValueList = False
        Me.txtDeductionURange2.IsUnique = False
        Me.txtDeductionURange2.Location = New System.Drawing.Point(118, 217)
        Me.txtDeductionURange2.MaxLength = 8
        Me.txtDeductionURange2.MendatroryField = False
        Me.txtDeductionURange2.MyLinkLable1 = Me.MyLabel11
        Me.txtDeductionURange2.MyLinkLable2 = Nothing
        Me.txtDeductionURange2.Name = "txtDeductionURange2"
        Me.txtDeductionURange2.ReferenceFieldDesc = Nothing
        Me.txtDeductionURange2.ReferenceFieldName = Nothing
        Me.txtDeductionURange2.ReferenceTableName = Nothing
        Me.txtDeductionURange2.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionURange2.TabIndex = 354
        Me.txtDeductionURange2.Text = "0"
        Me.txtDeductionURange2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionURange2.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(2, 217)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel11.TabIndex = 349
        Me.MyLabel11.Text = "Deduction U Range2"
        '
        'txtDeductionLRange2
        '
        Me.txtDeductionLRange2.BackColor = System.Drawing.Color.White
        Me.txtDeductionLRange2.CalculationExpression = Nothing
        Me.txtDeductionLRange2.DecimalPlaces = 2
        Me.txtDeductionLRange2.FieldCode = Nothing
        Me.txtDeductionLRange2.FieldDesc = Nothing
        Me.txtDeductionLRange2.FieldMaxLength = 0
        Me.txtDeductionLRange2.FieldName = Nothing
        Me.txtDeductionLRange2.isCalculatedField = False
        Me.txtDeductionLRange2.IsSourceFromTable = False
        Me.txtDeductionLRange2.IsSourceFromValueList = False
        Me.txtDeductionLRange2.IsUnique = False
        Me.txtDeductionLRange2.Location = New System.Drawing.Point(118, 195)
        Me.txtDeductionLRange2.MaxLength = 8
        Me.txtDeductionLRange2.MendatroryField = False
        Me.txtDeductionLRange2.MyLinkLable1 = Me.MyLabel9
        Me.txtDeductionLRange2.MyLinkLable2 = Nothing
        Me.txtDeductionLRange2.Name = "txtDeductionLRange2"
        Me.txtDeductionLRange2.ReferenceFieldDesc = Nothing
        Me.txtDeductionLRange2.ReferenceFieldName = Nothing
        Me.txtDeductionLRange2.ReferenceTableName = Nothing
        Me.txtDeductionLRange2.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionLRange2.TabIndex = 353
        Me.txtDeductionLRange2.Text = "0"
        Me.txtDeductionLRange2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionLRange2.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(2, 195)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel9.TabIndex = 347
        Me.MyLabel9.Text = "Deduction L Range2"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(3, 105)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(98, 16)
        Me.MyLabel5.TabIndex = 345
        Me.MyLabel5.Text = "Deduction Method"
        '
        'txtQcStatus
        '
        Me.txtQcStatus.CalculationExpression = Nothing
        Me.txtQcStatus.Enabled = False
        Me.txtQcStatus.FieldCode = Nothing
        Me.txtQcStatus.FieldDesc = Nothing
        Me.txtQcStatus.FieldMaxLength = 0
        Me.txtQcStatus.FieldName = Nothing
        Me.txtQcStatus.isCalculatedField = False
        Me.txtQcStatus.IsSourceFromTable = False
        Me.txtQcStatus.IsSourceFromValueList = False
        Me.txtQcStatus.IsUnique = False
        Me.txtQcStatus.Location = New System.Drawing.Point(118, 78)
        Me.txtQcStatus.MaxLength = 200
        Me.txtQcStatus.MendatroryField = False
        Me.txtQcStatus.MyLinkLable1 = Me.MyLabel6
        Me.txtQcStatus.MyLinkLable2 = Nothing
        Me.txtQcStatus.Name = "txtQcStatus"
        Me.txtQcStatus.ReferenceFieldDesc = Nothing
        Me.txtQcStatus.ReferenceFieldName = Nothing
        Me.txtQcStatus.ReferenceTableName = Nothing
        Me.txtQcStatus.Size = New System.Drawing.Size(247, 20)
        Me.txtQcStatus.TabIndex = 344
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(2, 80)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel6.TabIndex = 341
        Me.MyLabel6.Text = "Qc Status"
        '
        'btnUpdateDeduction
        '
        Me.btnUpdateDeduction.Location = New System.Drawing.Point(371, 349)
        Me.btnUpdateDeduction.Name = "btnUpdateDeduction"
        Me.btnUpdateDeduction.Size = New System.Drawing.Size(125, 21)
        Me.btnUpdateDeduction.TabIndex = 7
        Me.btnUpdateDeduction.Text = "Update Deduction"
        '
        'txtUpperRange
        '
        Me.txtUpperRange.BackColor = System.Drawing.Color.White
        Me.txtUpperRange.CalculationExpression = Nothing
        Me.txtUpperRange.DecimalPlaces = 2
        Me.txtUpperRange.FieldCode = Nothing
        Me.txtUpperRange.FieldDesc = Nothing
        Me.txtUpperRange.FieldMaxLength = 0
        Me.txtUpperRange.FieldName = Nothing
        Me.txtUpperRange.isCalculatedField = False
        Me.txtUpperRange.IsSourceFromTable = False
        Me.txtUpperRange.IsSourceFromValueList = False
        Me.txtUpperRange.IsUnique = False
        Me.txtUpperRange.Location = New System.Drawing.Point(118, 57)
        Me.txtUpperRange.MaxLength = 8
        Me.txtUpperRange.MendatroryField = False
        Me.txtUpperRange.MyLinkLable1 = Me.MyLabel4
        Me.txtUpperRange.MyLinkLable2 = Nothing
        Me.txtUpperRange.Name = "txtUpperRange"
        Me.txtUpperRange.ReferenceFieldDesc = Nothing
        Me.txtUpperRange.ReferenceFieldName = Nothing
        Me.txtUpperRange.ReferenceTableName = Nothing
        Me.txtUpperRange.Size = New System.Drawing.Size(247, 20)
        Me.txtUpperRange.TabIndex = 343
        Me.txtUpperRange.Text = "0"
        Me.txtUpperRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtUpperRange.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(2, 59)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel4.TabIndex = 339
        Me.MyLabel4.Text = "Upper Range"
        '
        'txtLowerRange
        '
        Me.txtLowerRange.BackColor = System.Drawing.Color.White
        Me.txtLowerRange.CalculationExpression = Nothing
        Me.txtLowerRange.DecimalPlaces = 2
        Me.txtLowerRange.FieldCode = Nothing
        Me.txtLowerRange.FieldDesc = Nothing
        Me.txtLowerRange.FieldMaxLength = 0
        Me.txtLowerRange.FieldName = Nothing
        Me.txtLowerRange.isCalculatedField = False
        Me.txtLowerRange.IsSourceFromTable = False
        Me.txtLowerRange.IsSourceFromValueList = False
        Me.txtLowerRange.IsUnique = False
        Me.txtLowerRange.Location = New System.Drawing.Point(118, 36)
        Me.txtLowerRange.MaxLength = 8
        Me.txtLowerRange.MendatroryField = False
        Me.txtLowerRange.MyLinkLable1 = Me.MyLabel1
        Me.txtLowerRange.MyLinkLable2 = Nothing
        Me.txtLowerRange.Name = "txtLowerRange"
        Me.txtLowerRange.ReferenceFieldDesc = Nothing
        Me.txtLowerRange.ReferenceFieldName = Nothing
        Me.txtLowerRange.ReferenceTableName = Nothing
        Me.txtLowerRange.Size = New System.Drawing.Size(247, 20)
        Me.txtLowerRange.TabIndex = 342
        Me.txtLowerRange.Text = "0"
        Me.txtLowerRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLowerRange.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(2, 38)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel1.TabIndex = 338
        Me.MyLabel1.Text = "Lower Range"
        '
        'lblParamDesc
        '
        Me.lblParamDesc.AutoSize = False
        Me.lblParamDesc.BorderVisible = True
        Me.lblParamDesc.FieldName = Nothing
        Me.lblParamDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParamDesc.Location = New System.Drawing.Point(371, 16)
        Me.lblParamDesc.Name = "lblParamDesc"
        Me.lblParamDesc.Size = New System.Drawing.Size(251, 18)
        Me.lblParamDesc.TabIndex = 337
        Me.lblParamDesc.TextWrap = False
        '
        'txtDeductionRatio
        '
        Me.txtDeductionRatio.BackColor = System.Drawing.Color.White
        Me.txtDeductionRatio.CalculationExpression = Nothing
        Me.txtDeductionRatio.DecimalPlaces = 2
        Me.txtDeductionRatio.FieldCode = Nothing
        Me.txtDeductionRatio.FieldDesc = Nothing
        Me.txtDeductionRatio.FieldMaxLength = 0
        Me.txtDeductionRatio.FieldName = Nothing
        Me.txtDeductionRatio.isCalculatedField = False
        Me.txtDeductionRatio.IsSourceFromTable = False
        Me.txtDeductionRatio.IsSourceFromValueList = False
        Me.txtDeductionRatio.IsUnique = False
        Me.txtDeductionRatio.Location = New System.Drawing.Point(118, 171)
        Me.txtDeductionRatio.MaxLength = 8
        Me.txtDeductionRatio.MendatroryField = False
        Me.txtDeductionRatio.MyLinkLable1 = Me.MyLabel7
        Me.txtDeductionRatio.MyLinkLable2 = Nothing
        Me.txtDeductionRatio.Name = "txtDeductionRatio"
        Me.txtDeductionRatio.ReferenceFieldDesc = Nothing
        Me.txtDeductionRatio.ReferenceFieldName = Nothing
        Me.txtDeductionRatio.ReferenceTableName = Nothing
        Me.txtDeductionRatio.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionRatio.TabIndex = 336
        Me.txtDeductionRatio.Text = "0"
        Me.txtDeductionRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionRatio.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(2, 173)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel7.TabIndex = 333
        Me.MyLabel7.Text = "Deduction Value"
        '
        'txtDeductionURange
        '
        Me.txtDeductionURange.BackColor = System.Drawing.Color.White
        Me.txtDeductionURange.CalculationExpression = Nothing
        Me.txtDeductionURange.DecimalPlaces = 2
        Me.txtDeductionURange.FieldCode = Nothing
        Me.txtDeductionURange.FieldDesc = Nothing
        Me.txtDeductionURange.FieldMaxLength = 0
        Me.txtDeductionURange.FieldName = Nothing
        Me.txtDeductionURange.isCalculatedField = False
        Me.txtDeductionURange.IsSourceFromTable = False
        Me.txtDeductionURange.IsSourceFromValueList = False
        Me.txtDeductionURange.IsUnique = False
        Me.txtDeductionURange.Location = New System.Drawing.Point(118, 149)
        Me.txtDeductionURange.MaxLength = 8
        Me.txtDeductionURange.MendatroryField = False
        Me.txtDeductionURange.MyLinkLable1 = Me.MyLabel3
        Me.txtDeductionURange.MyLinkLable2 = Nothing
        Me.txtDeductionURange.Name = "txtDeductionURange"
        Me.txtDeductionURange.ReferenceFieldDesc = Nothing
        Me.txtDeductionURange.ReferenceFieldName = Nothing
        Me.txtDeductionURange.ReferenceTableName = Nothing
        Me.txtDeductionURange.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionURange.TabIndex = 335
        Me.txtDeductionURange.Text = "0"
        Me.txtDeductionURange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionURange.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(2, 151)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel3.TabIndex = 332
        Me.MyLabel3.Text = "Deduction U Range1"
        '
        'txtDeductionLRange
        '
        Me.txtDeductionLRange.BackColor = System.Drawing.Color.White
        Me.txtDeductionLRange.CalculationExpression = Nothing
        Me.txtDeductionLRange.DecimalPlaces = 2
        Me.txtDeductionLRange.FieldCode = Nothing
        Me.txtDeductionLRange.FieldDesc = Nothing
        Me.txtDeductionLRange.FieldMaxLength = 0
        Me.txtDeductionLRange.FieldName = Nothing
        Me.txtDeductionLRange.isCalculatedField = False
        Me.txtDeductionLRange.IsSourceFromTable = False
        Me.txtDeductionLRange.IsSourceFromValueList = False
        Me.txtDeductionLRange.IsUnique = False
        Me.txtDeductionLRange.Location = New System.Drawing.Point(118, 128)
        Me.txtDeductionLRange.MaxLength = 8
        Me.txtDeductionLRange.MendatroryField = False
        Me.txtDeductionLRange.MyLinkLable1 = Me.MyLabel2
        Me.txtDeductionLRange.MyLinkLable2 = Nothing
        Me.txtDeductionLRange.Name = "txtDeductionLRange"
        Me.txtDeductionLRange.ReferenceFieldDesc = Nothing
        Me.txtDeductionLRange.ReferenceFieldName = Nothing
        Me.txtDeductionLRange.ReferenceTableName = Nothing
        Me.txtDeductionLRange.Size = New System.Drawing.Size(247, 20)
        Me.txtDeductionLRange.TabIndex = 334
        Me.txtDeductionLRange.Text = "0"
        Me.txtDeductionLRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionLRange.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(2, 130)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel2.TabIndex = 327
        Me.MyLabel2.Text = "Deduction L Range1"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(2, 17)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel8.TabIndex = 176
        Me.MyLabel8.Text = "Parameter Code"
        '
        'fndParameterCode
        '
        Me.fndParameterCode.CalculationExpression = Nothing
        Me.fndParameterCode.FieldCode = Nothing
        Me.fndParameterCode.FieldDesc = Nothing
        Me.fndParameterCode.FieldMaxLength = 0
        Me.fndParameterCode.FieldName = Nothing
        Me.fndParameterCode.isCalculatedField = False
        Me.fndParameterCode.IsSourceFromTable = False
        Me.fndParameterCode.IsSourceFromValueList = False
        Me.fndParameterCode.IsUnique = False
        Me.fndParameterCode.Location = New System.Drawing.Point(118, 15)
        Me.fndParameterCode.MendatroryField = True
        Me.fndParameterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndParameterCode.MyLinkLable1 = Me.MyLabel8
        Me.fndParameterCode.MyLinkLable2 = Nothing
        Me.fndParameterCode.MyReadOnly = False
        Me.fndParameterCode.MyShowMasterFormButton = False
        Me.fndParameterCode.Name = "fndParameterCode"
        Me.fndParameterCode.ReferenceFieldDesc = Nothing
        Me.fndParameterCode.ReferenceFieldName = Nothing
        Me.fndParameterCode.ReferenceTableName = Nothing
        Me.fndParameterCode.Size = New System.Drawing.Size(247, 20)
        Me.fndParameterCode.TabIndex = 174
        Me.fndParameterCode.Value = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnDedMethodFixed)
        Me.GroupBox1.Controls.Add(Me.rbtnDedMethodRatio)
        Me.GroupBox1.Location = New System.Drawing.Point(119, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(247, 31)
        Me.GroupBox1.TabIndex = 346
        Me.GroupBox1.TabStop = False
        '
        'rbtnDedMethodFixed
        '
        Me.rbtnDedMethodFixed.Location = New System.Drawing.Point(126, 11)
        Me.rbtnDedMethodFixed.MyLinkLable1 = Nothing
        Me.rbtnDedMethodFixed.MyLinkLable2 = Nothing
        Me.rbtnDedMethodFixed.Name = "rbtnDedMethodFixed"
        Me.rbtnDedMethodFixed.Size = New System.Drawing.Size(46, 18)
        Me.rbtnDedMethodFixed.TabIndex = 1
        Me.rbtnDedMethodFixed.TabStop = False
        Me.rbtnDedMethodFixed.Text = "Fixed"
        '
        'rbtnDedMethodRatio
        '
        Me.rbtnDedMethodRatio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnDedMethodRatio.Location = New System.Drawing.Point(70, 10)
        Me.rbtnDedMethodRatio.MyLinkLable1 = Nothing
        Me.rbtnDedMethodRatio.MyLinkLable2 = Nothing
        Me.rbtnDedMethodRatio.Name = "rbtnDedMethodRatio"
        Me.rbtnDedMethodRatio.Size = New System.Drawing.Size(46, 18)
        Me.rbtnDedMethodRatio.TabIndex = 0
        Me.rbtnDedMethodRatio.Text = "Ratio"
        Me.rbtnDedMethodRatio.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(179, 6)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 6
        Me.btnNew.Text = " "
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(717, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 21)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(95, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(78, 21)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(11, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 21)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'frmParameterRangeMasterForQC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(807, 508)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmParameterRangeMasterForQC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmParameterRangeMasterForQC"
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
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionRatio3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionURange3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionLRange3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionRatio2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionURange2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionLRange2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQcStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUpperRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLowerRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParamDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionURange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionLRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnDedMethodFixed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDedMethodRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUpdateDeduction As RadButton
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents fndParameterCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDeductionRatio As common.MyNumBox
    Friend WithEvents txtDeductionURange As common.MyNumBox
    Friend WithEvents txtDeductionLRange As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblParamDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtUpperRange As common.MyNumBox
    Friend WithEvents txtLowerRange As common.MyNumBox
    Friend WithEvents txtQcStatus As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnDedMethodFixed As common.Controls.MyRadioButton
    Friend WithEvents rbtnDedMethodRatio As common.Controls.MyRadioButton
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtDeductionRatio3 As common.MyNumBox
    Friend WithEvents txtDeductionURange3 As common.MyNumBox
    Friend WithEvents txtDeductionLRange3 As common.MyNumBox
    Friend WithEvents txtDeductionRatio2 As common.MyNumBox
    Friend WithEvents txtDeductionURange2 As common.MyNumBox
    Friend WithEvents txtDeductionLRange2 As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtParameterMapping As common.UserControls.txtMultiSelectFinder
End Class

