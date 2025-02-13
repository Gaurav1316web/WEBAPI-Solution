<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptCattleFeedProduction
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
        Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim CartesianArea2 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim CartesianArea3 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.ToDate = New common.Controls.MyDateTimePicker()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.cvProduction = New Telerik.WinControls.UI.RadChartView()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.gvProduction = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.CVPurchase = New Telerik.WinControls.UI.RadChartView()
        Me.GVPurchase = New common.UserControls.MyRadGridView()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.CVSale = New Telerik.WinControls.UI.RadChartView()
        Me.GVSale = New common.UserControls.MyRadGridView()
        Me.chkFG = New common.Controls.MyRadioButton()
        Me.ChkSFG = New common.Controls.MyRadioButton()
        Me.ChkBoth = New common.Controls.MyRadioButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.cvProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProduction.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.CVPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVPurchase.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.CVSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVSale.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkBoth)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkSFG)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkFG)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btngo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1152, 538)
        Me.SplitContainer1.SplitterDistance = 41
        Me.SplitContainer1.TabIndex = 0
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1056, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 39
        Me.btnclose.Text = "Close"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(76, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 38
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblfromDate.Location = New System.Drawing.Point(12, 12)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 34
        Me.lblfromDate.Text = "From Date"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(359, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(55, 22)
        Me.btnReset.TabIndex = 37
        Me.btnReset.Text = "Reset"
        '
        'ToDate
        '
        Me.ToDate.CalculationExpression = Nothing
        Me.ToDate.CustomFormat = "dd-MM-yyyy"
        Me.ToDate.FieldCode = Nothing
        Me.ToDate.FieldDesc = Nothing
        Me.ToDate.FieldMaxLength = 0
        Me.ToDate.FieldName = Nothing
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.isCalculatedField = False
        Me.ToDate.IsSourceFromTable = False
        Me.ToDate.IsSourceFromValueList = False
        Me.ToDate.IsUnique = False
        Me.ToDate.Location = New System.Drawing.Point(213, 11)
        Me.ToDate.MendatroryField = False
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.MyLinkLable1 = Nothing
        Me.ToDate.MyLinkLable2 = Nothing
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.ReferenceFieldDesc = Nothing
        Me.ToDate.ReferenceFieldName = Nothing
        Me.ToDate.ReferenceTableName = Nothing
        Me.ToDate.Size = New System.Drawing.Size(82, 20)
        Me.ToDate.TabIndex = 33
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "17-12-2011"
        Me.ToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'btngo
        '
        Me.btngo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngo.Location = New System.Drawing.Point(300, 10)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(54, 22)
        Me.btngo.TabIndex = 36
        Me.btngo.Text = ">>"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblToDate.Location = New System.Drawing.Point(163, 12)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 35
        Me.lblToDate.Text = "To Date"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.PageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(1152, 493)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.ViewMode = Telerik.WinControls.UI.PageViewMode.Backstage
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Center
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.Fill
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemContentOrientation = Telerik.WinControls.UI.PageViewContentOrientation.Horizontal
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.SplitContainer3)
        Me.Attachments.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.Attachments.ItemSize = New System.Drawing.SizeF(398.0!, 45.0!)
        Me.Attachments.Location = New System.Drawing.Point(5, 60)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1143, 429)
        Me.Attachments.Text = "CATTLE FEED PRODUCTION"
        Me.Attachments.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.cvProduction)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvProduction)
        Me.SplitContainer3.Size = New System.Drawing.Size(1143, 429)
        Me.SplitContainer3.SplitterDistance = 337
        Me.SplitContainer3.TabIndex = 1
        '
        'cvProduction
        '
        Me.cvProduction.AreaDesign = CartesianArea1
        Me.cvProduction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvProduction.Location = New System.Drawing.Point(0, 8)
        Me.cvProduction.Name = "cvProduction"
        Me.cvProduction.ShowGrid = False
        Me.cvProduction.Size = New System.Drawing.Size(1143, 329)
        Me.cvProduction.TabIndex = 5
        '
        'MyLabel2
        '
        Me.MyLabel2.AutoSize = False
        Me.MyLabel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MyLabel2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.MyLabel2.Location = New System.Drawing.Point(0, 0)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(1143, 8)
        Me.MyLabel2.TabIndex = 16
        Me.MyLabel2.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'gvProduction
        '
        Me.gvProduction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProduction.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvProduction.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvProduction.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProduction.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvProduction.MyStopExport = False
        Me.gvProduction.Name = "gvProduction"
        Me.gvProduction.ShowHeaderCellButtons = True
        Me.gvProduction.Size = New System.Drawing.Size(1143, 88)
        Me.gvProduction.TabIndex = 6
        Me.gvProduction.VarID = ""
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(381.0!, 45.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1143, 429)
        Me.RadPageViewPage1.Text = "CATTLE FEED PURCHASE"
        Me.RadPageViewPage1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer4)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(348.0!, 45.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1143, 429)
        Me.RadPageViewPage2.Text = "CATTLE FEED SALE"
        Me.RadPageViewPage2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.CVPurchase)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GVPurchase)
        Me.SplitContainer2.Size = New System.Drawing.Size(1143, 429)
        Me.SplitContainer2.SplitterDistance = 332
        Me.SplitContainer2.TabIndex = 0
        '
        'CVPurchase
        '
        Me.CVPurchase.AreaDesign = CartesianArea2
        Me.CVPurchase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CVPurchase.Location = New System.Drawing.Point(0, 0)
        Me.CVPurchase.Name = "CVPurchase"
        Me.CVPurchase.ShowGrid = False
        Me.CVPurchase.Size = New System.Drawing.Size(1143, 332)
        Me.CVPurchase.TabIndex = 6
        '
        'GVPurchase
        '
        Me.GVPurchase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GVPurchase.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GVPurchase.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.GVPurchase.MasterTemplate.ShowHeaderCellButtons = True
        Me.GVPurchase.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.GVPurchase.MyStopExport = False
        Me.GVPurchase.Name = "GVPurchase"
        Me.GVPurchase.ShowHeaderCellButtons = True
        Me.GVPurchase.Size = New System.Drawing.Size(1143, 93)
        Me.GVPurchase.TabIndex = 7
        Me.GVPurchase.VarID = ""
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.CVSale)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.GVSale)
        Me.SplitContainer4.Size = New System.Drawing.Size(1143, 429)
        Me.SplitContainer4.SplitterDistance = 323
        Me.SplitContainer4.TabIndex = 0
        '
        'CVSale
        '
        Me.CVSale.AreaDesign = CartesianArea3
        Me.CVSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CVSale.Location = New System.Drawing.Point(0, 0)
        Me.CVSale.Name = "CVSale"
        Me.CVSale.ShowGrid = False
        Me.CVSale.Size = New System.Drawing.Size(1143, 323)
        Me.CVSale.TabIndex = 6
        '
        'GVSale
        '
        Me.GVSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GVSale.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GVSale.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.GVSale.MasterTemplate.ShowHeaderCellButtons = True
        Me.GVSale.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.GVSale.MyStopExport = False
        Me.GVSale.Name = "GVSale"
        Me.GVSale.ShowHeaderCellButtons = True
        Me.GVSale.Size = New System.Drawing.Size(1143, 102)
        Me.GVSale.TabIndex = 7
        Me.GVSale.VarID = ""
        '
        'chkFG
        '
        Me.chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFG.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.chkFG.Location = New System.Drawing.Point(420, 13)
        Me.chkFG.MyLinkLable1 = Nothing
        Me.chkFG.MyLinkLable2 = Nothing
        Me.chkFG.Name = "chkFG"
        Me.chkFG.Size = New System.Drawing.Size(33, 18)
        Me.chkFG.TabIndex = 40
        Me.chkFG.TabStop = False
        Me.chkFG.Text = "FG"
        Me.chkFG.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'ChkSFG
        '
        Me.ChkSFG.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.ChkSFG.Location = New System.Drawing.Point(459, 13)
        Me.ChkSFG.MyLinkLable1 = Nothing
        Me.ChkSFG.MyLinkLable2 = Nothing
        Me.ChkSFG.Name = "ChkSFG"
        Me.ChkSFG.Size = New System.Drawing.Size(39, 18)
        Me.ChkSFG.TabIndex = 41
        Me.ChkSFG.TabStop = False
        Me.ChkSFG.Text = "SFG"
        '
        'ChkBoth
        '
        Me.ChkBoth.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.ChkBoth.Location = New System.Drawing.Point(504, 14)
        Me.ChkBoth.MyLinkLable1 = Nothing
        Me.ChkBoth.MyLinkLable2 = Nothing
        Me.ChkBoth.Name = "ChkBoth"
        Me.ChkBoth.Size = New System.Drawing.Size(44, 18)
        Me.ChkBoth.TabIndex = 42
        Me.ChkBoth.TabStop = False
        Me.ChkBoth.Text = "Both"
        '
        'rptCattleFeedProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1152, 538)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptCattleFeedProduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptCattleFeedProduction"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.cvProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProduction.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProduction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.CVPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVPurchase.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.CVSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVSale.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents Attachments As RadPageViewPage
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents cvProduction As RadChartView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents gvProduction As common.UserControls.MyRadGridView
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents btnReset As RadButton
    Friend WithEvents ToDate As common.Controls.MyDateTimePicker
    Friend WithEvents btngo As RadButton
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents btnclose As RadButton
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents CVPurchase As RadChartView
    Friend WithEvents GVPurchase As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents CVSale As RadChartView
    Friend WithEvents GVSale As common.UserControls.MyRadGridView
    Friend WithEvents chkFG As common.Controls.MyRadioButton
    Friend WithEvents ChkSFG As common.Controls.MyRadioButton
    Friend WithEvents ChkBoth As common.Controls.MyRadioButton
End Class
