<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DairySaleDashboard
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim CartesianArea2 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim CartesianArea3 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.gvSales = New common.UserControls.MyRadGridView()
        Me.lblRMStock = New common.Controls.MyLabel()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.cvSaleitem = New Telerik.WinControls.UI.RadChartView()
        Me.lblRMSupply = New common.Controls.MyLabel()
        Me.cvSaleitemWise = New Telerik.WinControls.UI.RadChartView()
        Me.lblRMInPlant = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.gvFinishGoods = New common.UserControls.MyRadGridView()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cvFinishGoods = New Telerik.WinControls.UI.RadChartView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gvProdution = New common.UserControls.MyRadGridView()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cvProdution = New Telerik.WinControls.UI.RadChartView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer7 = New System.Windows.Forms.SplitContainer()
        Me.gvAccountVendor = New common.UserControls.MyRadGridView()
        Me.lblvendor = New common.Controls.MyLabel()
        Me.gvAccountCustomer = New common.UserControls.MyRadGridView()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.gvSales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSales.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRMStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.cvSaleitem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRMSupply, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cvSaleitemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRMInPlant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.gvFinishGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvFinishGoods.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cvFinishGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvProdution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProdution.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cvProdution, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        Me.SplitContainer7.Panel1.SuspendLayout()
        Me.SplitContainer7.Panel2.SuspendLayout()
        Me.SplitContainer7.SuspendLayout()
        CType(Me.gvAccountVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAccountVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAccountCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAccountCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1149, 535)
        Me.SplitContainer1.SplitterDistance = 34
        Me.SplitContainer1.TabIndex = 0
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.RadLabel15.Location = New System.Drawing.Point(646, 10)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 33
        Me.RadLabel15.Text = "Location"
        Me.RadLabel15.Visible = False
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
        Me.txtLocation.Location = New System.Drawing.Point(700, 9)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(143, 19)
        Me.txtLocation.TabIndex = 32
        Me.txtLocation.Value = ""
        Me.txtLocation.Visible = False
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(199, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(55, 22)
        Me.btnReset.TabIndex = 31
        Me.btnReset.Text = "Reset"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(141, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(54, 22)
        Me.RadButton1.TabIndex = 30
        Me.RadButton1.Text = ">>"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblfromDate.Location = New System.Drawing.Point(449, 9)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        Me.lblfromDate.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1061, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 3
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
        Me.txtFromDate.Location = New System.Drawing.Point(532, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReadOnly = True
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        Me.txtFromDate.Visible = False
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblToDate.Location = New System.Drawing.Point(6, 7)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(30, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(41, 6)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.PageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1149, 497)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.ViewMode = Telerik.WinControls.UI.PageViewMode.Backstage
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Center
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.Fill
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewBackstageElement).ItemContentOrientation = Telerik.WinControls.UI.PageViewContentOrientation.Horizontal
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer5)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(252.0!, 45.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1140, 433)
        Me.RadPageViewPage1.Text = "SALE"
        Me.RadPageViewPage1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.gvSales)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblRMStock)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.SplitContainer6)
        Me.SplitContainer5.Size = New System.Drawing.Size(1140, 433)
        Me.SplitContainer5.SplitterDistance = 531
        Me.SplitContainer5.TabIndex = 0
        '
        'gvSales
        '
        Me.gvSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSales.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvSales.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvSales.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSales.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvSales.MyStopExport = False
        Me.gvSales.Name = "gvSales"
        Me.gvSales.ShowHeaderCellButtons = True
        Me.gvSales.Size = New System.Drawing.Size(531, 413)
        Me.gvSales.TabIndex = 15
        '
        'lblRMStock
        '
        Me.lblRMStock.AutoSize = False
        Me.lblRMStock.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblRMStock.FieldName = Nothing
        Me.lblRMStock.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblRMStock.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblRMStock.Location = New System.Drawing.Point(0, 0)
        Me.lblRMStock.Name = "lblRMStock"
        Me.lblRMStock.Size = New System.Drawing.Size(531, 20)
        Me.lblRMStock.TabIndex = 16
        Me.lblRMStock.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.cvSaleitem)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblRMSupply)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.cvSaleitemWise)
        Me.SplitContainer6.Panel2.Controls.Add(Me.lblRMInPlant)
        Me.SplitContainer6.Size = New System.Drawing.Size(605, 433)
        Me.SplitContainer6.SplitterDistance = 271
        Me.SplitContainer6.SplitterWidth = 1
        Me.SplitContainer6.TabIndex = 2
        '
        'cvSaleitem
        '
        Me.cvSaleitem.AreaDesign = CartesianArea1
        Me.cvSaleitem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvSaleitem.Location = New System.Drawing.Point(0, 20)
        Me.cvSaleitem.Name = "cvSaleitem"
        Me.cvSaleitem.ShowGrid = False
        Me.cvSaleitem.Size = New System.Drawing.Size(605, 251)
        Me.cvSaleitem.TabIndex = 15
        '
        'lblRMSupply
        '
        Me.lblRMSupply.AutoSize = False
        Me.lblRMSupply.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblRMSupply.FieldName = Nothing
        Me.lblRMSupply.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblRMSupply.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblRMSupply.Location = New System.Drawing.Point(0, 0)
        Me.lblRMSupply.Name = "lblRMSupply"
        Me.lblRMSupply.Size = New System.Drawing.Size(605, 20)
        Me.lblRMSupply.TabIndex = 14
        Me.lblRMSupply.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'cvSaleitemWise
        '
        Me.cvSaleitemWise.AreaType = Telerik.WinControls.UI.ChartAreaType.Pie
        Me.cvSaleitemWise.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvSaleitemWise.Location = New System.Drawing.Point(0, 20)
        Me.cvSaleitemWise.Name = "cvSaleitemWise"
        Me.cvSaleitemWise.ShowGrid = False
        Me.cvSaleitemWise.Size = New System.Drawing.Size(605, 141)
        Me.cvSaleitemWise.TabIndex = 16
        '
        'lblRMInPlant
        '
        Me.lblRMInPlant.AutoSize = False
        Me.lblRMInPlant.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblRMInPlant.FieldName = Nothing
        Me.lblRMInPlant.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblRMInPlant.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblRMInPlant.Location = New System.Drawing.Point(0, 0)
        Me.lblRMInPlant.Name = "lblRMInPlant"
        Me.lblRMInPlant.Size = New System.Drawing.Size(605, 20)
        Me.lblRMInPlant.TabIndex = 15
        Me.lblRMInPlant.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.SplitContainer3)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(286.0!, 45.0!)
        Me.Attachments.Location = New System.Drawing.Point(5, 60)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1140, 433)
        Me.Attachments.Text = "PURCHASE"
        Me.Attachments.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.gvFinishGoods)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.cvFinishGoods)
        Me.SplitContainer3.Size = New System.Drawing.Size(1140, 433)
        Me.SplitContainer3.SplitterDistance = 271
        Me.SplitContainer3.TabIndex = 1
        '
        'gvFinishGoods
        '
        Me.gvFinishGoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvFinishGoods.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvFinishGoods.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvFinishGoods.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvFinishGoods.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvFinishGoods.MyStopExport = False
        Me.gvFinishGoods.Name = "gvFinishGoods"
        Me.gvFinishGoods.ShowHeaderCellButtons = True
        Me.gvFinishGoods.Size = New System.Drawing.Size(1140, 251)
        Me.gvFinishGoods.TabIndex = 6
        Me.gvFinishGoods.Visible = False
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
        Me.MyLabel2.Size = New System.Drawing.Size(1140, 20)
        Me.MyLabel2.TabIndex = 16
        Me.MyLabel2.Text = "LAST10 DAYS SALE DATA (Unit in Qtl.)"
        Me.MyLabel2.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.MyLabel2.Visible = False
        '
        'cvFinishGoods
        '
        Me.cvFinishGoods.AreaDesign = CartesianArea2
        Me.cvFinishGoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvFinishGoods.Location = New System.Drawing.Point(0, 0)
        Me.cvFinishGoods.Name = "cvFinishGoods"
        Me.cvFinishGoods.ShowGrid = False
        Me.cvFinishGoods.Size = New System.Drawing.Size(1140, 158)
        Me.cvFinishGoods.TabIndex = 5
        Me.cvFinishGoods.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(302.0!, 45.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1140, 433)
        Me.RadPageViewPage2.Text = "PRODUCTION"
        Me.RadPageViewPage2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gvProdution)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cvProdution)
        Me.SplitContainer2.Size = New System.Drawing.Size(1136, 429)
        Me.SplitContainer2.SplitterDistance = 271
        Me.SplitContainer2.TabIndex = 0
        '
        'gvProdution
        '
        Me.gvProdution.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip
        Me.gvProdution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProdution.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvProdution.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvProdution.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProdution.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvProdution.MyStopExport = False
        Me.gvProdution.Name = "gvProdution"
        Me.gvProdution.ShowHeaderCellButtons = True
        Me.gvProdution.Size = New System.Drawing.Size(1136, 251)
        Me.gvProdution.TabIndex = 6
        Me.gvProdution.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.AutoSize = False
        Me.MyLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MyLabel1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.MyLabel1.Location = New System.Drawing.Point(0, 0)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(1136, 20)
        Me.MyLabel1.TabIndex = 16
        Me.MyLabel1.Text = "LAST 10 DAYS PRODUCTION DATA (Unit in Qtl.)"
        Me.MyLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.MyLabel1.Visible = False
        '
        'cvProdution
        '
        Me.cvProdution.AreaDesign = CartesianArea3
        Me.cvProdution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvProdution.Location = New System.Drawing.Point(0, 0)
        Me.cvProdution.Name = "cvProdution"
        Me.cvProdution.ShowGrid = False
        Me.cvProdution.Size = New System.Drawing.Size(1136, 154)
        Me.cvProdution.TabIndex = 5
        Me.cvProdution.Visible = False
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.SplitContainer7)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(282.0!, 45.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1140, 433)
        Me.RadPageViewPage4.Text = "ACCOUNT"
        Me.RadPageViewPage4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer7
        '
        Me.SplitContainer7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer7.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer7.Name = "SplitContainer7"
        '
        'SplitContainer7.Panel1
        '
        Me.SplitContainer7.Panel1.Controls.Add(Me.gvAccountVendor)
        Me.SplitContainer7.Panel1.Controls.Add(Me.lblvendor)
        '
        'SplitContainer7.Panel2
        '
        Me.SplitContainer7.Panel2.Controls.Add(Me.gvAccountCustomer)
        Me.SplitContainer7.Panel2.Controls.Add(Me.lblCustomer)
        Me.SplitContainer7.Size = New System.Drawing.Size(1140, 433)
        Me.SplitContainer7.SplitterDistance = 574
        Me.SplitContainer7.TabIndex = 23
        '
        'gvAccountVendor
        '
        Me.gvAccountVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAccountVendor.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvAccountVendor.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAccountVendor.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAccountVendor.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvAccountVendor.MyStopExport = False
        Me.gvAccountVendor.Name = "gvAccountVendor"
        Me.gvAccountVendor.ShowHeaderCellButtons = True
        Me.gvAccountVendor.Size = New System.Drawing.Size(574, 413)
        Me.gvAccountVendor.TabIndex = 16
        Me.gvAccountVendor.Visible = False
        '
        'lblvendor
        '
        Me.lblvendor.AutoSize = False
        Me.lblvendor.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblvendor.FieldName = Nothing
        Me.lblvendor.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblvendor.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblvendor.Location = New System.Drawing.Point(0, 0)
        Me.lblvendor.Name = "lblvendor"
        Me.lblvendor.Size = New System.Drawing.Size(574, 20)
        Me.lblvendor.TabIndex = 21
        Me.lblvendor.Text = "VENDOR LEGDER"
        Me.lblvendor.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.lblvendor.Visible = False
        '
        'gvAccountCustomer
        '
        Me.gvAccountCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAccountCustomer.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvAccountCustomer.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAccountCustomer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAccountCustomer.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gvAccountCustomer.MyStopExport = False
        Me.gvAccountCustomer.Name = "gvAccountCustomer"
        Me.gvAccountCustomer.ShowHeaderCellButtons = True
        Me.gvAccountCustomer.Size = New System.Drawing.Size(562, 413)
        Me.gvAccountCustomer.TabIndex = 16
        Me.gvAccountCustomer.Visible = False
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = False
        Me.lblCustomer.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblCustomer.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblCustomer.Location = New System.Drawing.Point(0, 0)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(562, 20)
        Me.lblCustomer.TabIndex = 22
        Me.lblCustomer.Text = "CUSTOMER LEDGER"
        Me.lblCustomer.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.lblCustomer.Visible = False
        '
        'DairySaleDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1149, 535)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "DairySaleDashboard"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sale Visual Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.gvSales.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRMStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.cvSaleitem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRMSupply, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cvSaleitemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRMInPlant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.gvFinishGoods.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvFinishGoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cvFinishGoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gvProdution.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProdution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cvProdution, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.SplitContainer7.Panel1.ResumeLayout(False)
        Me.SplitContainer7.Panel2.ResumeLayout(False)
        Me.SplitContainer7.ResumeLayout(False)
        CType(Me.gvAccountVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAccountVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAccountCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAccountCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents Attachments As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents cvProdution As RadChartView
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents cvFinishGoods As RadChartView
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents lblRMStock As common.Controls.MyLabel
    Friend WithEvents SplitContainer6 As SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents lblRMSupply As common.Controls.MyLabel
    Friend WithEvents lblRMInPlant As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents lblvendor As common.Controls.MyLabel
    Friend WithEvents SplitContainer7 As SplitContainer
    Friend WithEvents gvProdution As common.UserControls.MyRadGridView
    Friend WithEvents gvFinishGoods As common.UserControls.MyRadGridView
    Friend WithEvents gvAccountVendor As common.UserControls.MyRadGridView
    Friend WithEvents gvAccountCustomer As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents cvSaleitemWise As RadChartView
    Friend WithEvents cvSaleitem As RadChartView
    Friend WithEvents gvSales As common.UserControls.MyRadGridView
    'End Sub
End Class
