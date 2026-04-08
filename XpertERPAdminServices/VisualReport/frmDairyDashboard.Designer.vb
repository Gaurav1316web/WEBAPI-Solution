<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDairyDashboard
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition7 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.gvSaleMilk = New common.UserControls.MyRadGridView()
        Me.lblRMStock = New common.Controls.MyLabel()
        Me.gvSaleProduct = New common.UserControls.MyRadGridView()
        Me.lblRMSupply = New common.Controls.MyLabel()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.gvMPSummary = New common.UserControls.MyRadGridView()
        Me.lblQuality = New common.Controls.MyLabel()
        Me.gvMPTopDCS = New common.UserControls.MyRadGridView()
        Me.lblQualitySummary = New common.Controls.MyLabel()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer7 = New System.Windows.Forms.SplitContainer()
        Me.gvDBTTopDCS = New common.UserControls.MyRadGridView()
        Me.lblvendor = New common.Controls.MyLabel()
        Me.gvDBTTopMP = New common.UserControls.MyRadGridView()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.gvDBTSummary = New common.UserControls.MyRadGridView()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.gvSaleMilk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSaleMilk.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRMStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSaleProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSaleProduct.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRMSupply, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.gvMPSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMPSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQuality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMPTopDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMPTopDCS.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQualitySummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        Me.SplitContainer7.Panel1.SuspendLayout()
        Me.SplitContainer7.Panel2.SuspendLayout()
        Me.SplitContainer7.SuspendLayout()
        CType(Me.gvDBTTopDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDBTTopDCS.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDBTTopMP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDBTTopMP.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDBTSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDBTSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1012, 490)
        Me.SplitContainer1.SplitterDistance = 34
        Me.SplitContainer1.TabIndex = 0
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(192, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(55, 22)
        Me.btnReset.TabIndex = 31
        Me.btnReset.Text = "Reset"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(134, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(54, 22)
        Me.RadButton1.TabIndex = 30
        Me.RadButton1.Text = ">>"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(924, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
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
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.PageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1012, 452)
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
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(300.0!, 45.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1003, 388)
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
        Me.SplitContainer5.Panel1.Controls.Add(Me.gvSaleMilk)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblRMStock)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.gvSaleProduct)
        Me.SplitContainer5.Panel2.Controls.Add(Me.lblRMSupply)
        Me.SplitContainer5.Size = New System.Drawing.Size(1003, 388)
        Me.SplitContainer5.SplitterDistance = 466
        Me.SplitContainer5.TabIndex = 0
        '
        'gvSaleMilk
        '
        Me.gvSaleMilk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSaleMilk.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvSaleMilk.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvSaleMilk.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSaleMilk.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvSaleMilk.MyExportAPI = False
        Me.gvSaleMilk.MyExportFilePath = ""
        Me.gvSaleMilk.MyStopExport = False
        Me.gvSaleMilk.Name = "gvSaleMilk"
        Me.gvSaleMilk.ShowHeaderCellButtons = True
        Me.gvSaleMilk.Size = New System.Drawing.Size(466, 368)
        Me.gvSaleMilk.TabIndex = 15
        Me.gvSaleMilk.VarID = ""
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
        Me.lblRMStock.Size = New System.Drawing.Size(466, 20)
        Me.lblRMStock.TabIndex = 16
        Me.lblRMStock.Text = "Milk Items"
        Me.lblRMStock.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'gvSaleProduct
        '
        Me.gvSaleProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSaleProduct.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvSaleProduct.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvSaleProduct.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSaleProduct.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvSaleProduct.MyExportAPI = False
        Me.gvSaleProduct.MyExportFilePath = ""
        Me.gvSaleProduct.MyStopExport = False
        Me.gvSaleProduct.Name = "gvSaleProduct"
        Me.gvSaleProduct.ShowHeaderCellButtons = True
        Me.gvSaleProduct.Size = New System.Drawing.Size(533, 368)
        Me.gvSaleProduct.TabIndex = 6
        Me.gvSaleProduct.VarID = ""
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
        Me.lblRMSupply.Size = New System.Drawing.Size(533, 20)
        Me.lblRMSupply.TabIndex = 14
        Me.lblRMSupply.Text = "Product Items"
        Me.lblRMSupply.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer4)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(391.0!, 45.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1003, 388)
        Me.RadPageViewPage3.Text = "MILK PROCUREMENT"
        Me.RadPageViewPage3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.gvMPSummary)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblQuality)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.gvMPTopDCS)
        Me.SplitContainer4.Panel2.Controls.Add(Me.lblQualitySummary)
        Me.SplitContainer4.Size = New System.Drawing.Size(1003, 388)
        Me.SplitContainer4.SplitterDistance = 114
        Me.SplitContainer4.SplitterWidth = 1
        Me.SplitContainer4.TabIndex = 1
        '
        'gvMPSummary
        '
        Me.gvMPSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMPSummary.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvMPSummary.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvMPSummary.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMPSummary.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvMPSummary.MyExportAPI = False
        Me.gvMPSummary.MyExportFilePath = ""
        Me.gvMPSummary.MyStopExport = False
        Me.gvMPSummary.Name = "gvMPSummary"
        Me.gvMPSummary.ShowHeaderCellButtons = True
        Me.gvMPSummary.Size = New System.Drawing.Size(1003, 94)
        Me.gvMPSummary.TabIndex = 6
        Me.gvMPSummary.VarID = ""
        '
        'lblQuality
        '
        Me.lblQuality.AutoSize = False
        Me.lblQuality.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblQuality.FieldName = Nothing
        Me.lblQuality.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblQuality.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblQuality.Location = New System.Drawing.Point(0, 0)
        Me.lblQuality.Name = "lblQuality"
        Me.lblQuality.Size = New System.Drawing.Size(1003, 20)
        Me.lblQuality.TabIndex = 14
        Me.lblQuality.Text = "Short Summary"
        Me.lblQuality.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'gvMPTopDCS
        '
        Me.gvMPTopDCS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMPTopDCS.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvMPTopDCS.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvMPTopDCS.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMPTopDCS.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvMPTopDCS.MyExportAPI = False
        Me.gvMPTopDCS.MyExportFilePath = ""
        Me.gvMPTopDCS.MyStopExport = False
        Me.gvMPTopDCS.Name = "gvMPTopDCS"
        Me.gvMPTopDCS.ShowHeaderCellButtons = True
        Me.gvMPTopDCS.Size = New System.Drawing.Size(1003, 253)
        Me.gvMPTopDCS.TabIndex = 7
        Me.gvMPTopDCS.VarID = ""
        '
        'lblQualitySummary
        '
        Me.lblQualitySummary.AutoSize = False
        Me.lblQualitySummary.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblQualitySummary.FieldName = Nothing
        Me.lblQualitySummary.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblQualitySummary.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblQualitySummary.Location = New System.Drawing.Point(0, 0)
        Me.lblQualitySummary.Name = "lblQualitySummary"
        Me.lblQualitySummary.Size = New System.Drawing.Size(1003, 20)
        Me.lblQualitySummary.TabIndex = 15
        Me.lblQualitySummary.Text = "Top 10 DCS"
        Me.lblQualitySummary.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.SplitContainer7)
        Me.RadPageViewPage4.Controls.Add(Me.gvDBTSummary)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(296.0!, 45.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1003, 388)
        Me.RadPageViewPage4.Text = "DBT"
        Me.RadPageViewPage4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer7
        '
        Me.SplitContainer7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer7.Location = New System.Drawing.Point(0, 81)
        Me.SplitContainer7.Name = "SplitContainer7"
        '
        'SplitContainer7.Panel1
        '
        Me.SplitContainer7.Panel1.Controls.Add(Me.gvDBTTopDCS)
        Me.SplitContainer7.Panel1.Controls.Add(Me.lblvendor)
        '
        'SplitContainer7.Panel2
        '
        Me.SplitContainer7.Panel2.Controls.Add(Me.gvDBTTopMP)
        Me.SplitContainer7.Panel2.Controls.Add(Me.lblCustomer)
        Me.SplitContainer7.Size = New System.Drawing.Size(1003, 307)
        Me.SplitContainer7.SplitterDistance = 505
        Me.SplitContainer7.TabIndex = 23
        '
        'gvDBTTopDCS
        '
        Me.gvDBTTopDCS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDBTTopDCS.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvDBTTopDCS.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDBTTopDCS.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDBTTopDCS.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gvDBTTopDCS.MyExportAPI = False
        Me.gvDBTTopDCS.MyExportFilePath = ""
        Me.gvDBTTopDCS.MyStopExport = False
        Me.gvDBTTopDCS.Name = "gvDBTTopDCS"
        Me.gvDBTTopDCS.ShowHeaderCellButtons = True
        Me.gvDBTTopDCS.Size = New System.Drawing.Size(505, 287)
        Me.gvDBTTopDCS.TabIndex = 16
        Me.gvDBTTopDCS.VarID = ""
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
        Me.lblvendor.Size = New System.Drawing.Size(505, 20)
        Me.lblvendor.TabIndex = 21
        Me.lblvendor.Text = "Top 10 DCS"
        Me.lblvendor.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'gvDBTTopMP
        '
        Me.gvDBTTopMP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDBTTopMP.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvDBTTopMP.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDBTTopMP.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDBTTopMP.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gvDBTTopMP.MyExportAPI = False
        Me.gvDBTTopMP.MyExportFilePath = ""
        Me.gvDBTTopMP.MyStopExport = False
        Me.gvDBTTopMP.Name = "gvDBTTopMP"
        Me.gvDBTTopMP.ShowHeaderCellButtons = True
        Me.gvDBTTopMP.Size = New System.Drawing.Size(494, 287)
        Me.gvDBTTopMP.TabIndex = 16
        Me.gvDBTTopMP.VarID = ""
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
        Me.lblCustomer.Size = New System.Drawing.Size(494, 20)
        Me.lblCustomer.TabIndex = 22
        Me.lblCustomer.Text = "Top 10 Farmer"
        Me.lblCustomer.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'gvDBTSummary
        '
        Me.gvDBTSummary.Dock = System.Windows.Forms.DockStyle.Top
        Me.gvDBTSummary.Location = New System.Drawing.Point(0, 20)
        '
        '
        '
        Me.gvDBTSummary.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDBTSummary.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDBTSummary.MasterTemplate.ViewDefinition = TableViewDefinition7
        Me.gvDBTSummary.MyExportAPI = False
        Me.gvDBTSummary.MyExportFilePath = ""
        Me.gvDBTSummary.MyStopExport = False
        Me.gvDBTSummary.Name = "gvDBTSummary"
        Me.gvDBTSummary.ShowHeaderCellButtons = True
        Me.gvDBTSummary.Size = New System.Drawing.Size(1003, 61)
        Me.gvDBTSummary.TabIndex = 24
        Me.gvDBTSummary.VarID = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.AutoSize = False
        Me.MyLabel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MyLabel3.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.MyLabel3.Location = New System.Drawing.Point(0, 0)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(1003, 20)
        Me.MyLabel3.TabIndex = 25
        Me.MyLabel3.Text = "Short Summary"
        Me.MyLabel3.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'frmDairyDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1012, 490)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDairyDashboard"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sale Visual Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.gvSaleMilk.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSaleMilk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRMStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSaleProduct.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSaleProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRMSupply, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.gvMPSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMPSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQuality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMPTopDCS.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMPTopDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQualitySummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.SplitContainer7.Panel1.ResumeLayout(False)
        Me.SplitContainer7.Panel2.ResumeLayout(False)
        Me.SplitContainer7.ResumeLayout(False)
        CType(Me.gvDBTTopDCS.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDBTTopDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDBTTopMP.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDBTTopMP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDBTSummary.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDBTSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents lblQuality As common.Controls.MyLabel
    Friend WithEvents lblQualitySummary As common.Controls.MyLabel
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents lblRMStock As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents lblRMSupply As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents lblvendor As common.Controls.MyLabel
    Friend WithEvents SplitContainer7 As SplitContainer
    Friend WithEvents gvMPSummary As common.UserControls.MyRadGridView
    Friend WithEvents gvMPTopDCS As common.UserControls.MyRadGridView
    Friend WithEvents gvSaleMilk As common.UserControls.MyRadGridView
    Friend WithEvents gvSaleProduct As common.UserControls.MyRadGridView
    Friend WithEvents gvDBTTopDCS As common.UserControls.MyRadGridView
    Friend WithEvents gvDBTTopMP As common.UserControls.MyRadGridView
    Friend WithEvents gvDBTSummary As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class
