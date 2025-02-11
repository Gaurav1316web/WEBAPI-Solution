<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RCDFUnionDashboard
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.cvProc = New Telerik.WinControls.UI.RadChartView()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.gvProc = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.cvSale = New Telerik.WinControls.UI.RadChartView()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.gvSale = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.cvProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvProc.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.cvSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSale.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnclose)
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
        Me.txtFromDate.Location = New System.Drawing.Point(70, 8)
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
        Me.txtFromDate.TabIndex = 32
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(353, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(55, 22)
        Me.btnReset.TabIndex = 31
        Me.btnReset.Text = "Reset"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(294, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(54, 22)
        Me.RadButton1.TabIndex = 30
        Me.RadButton1.Text = ">>"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblfromDate.Location = New System.Drawing.Point(6, 9)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1061, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblToDate.Location = New System.Drawing.Point(157, 9)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
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
        Me.txtToDate.Location = New System.Drawing.Point(207, 8)
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
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.PageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(1149, 497)
        Me.RadPageView1.TabIndex = 1
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
        Me.Attachments.ItemSize = New System.Drawing.SizeF(572.0!, 45.0!)
        Me.Attachments.Location = New System.Drawing.Point(5, 60)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1140, 433)
        Me.Attachments.Text = "MILK PROCUREMENT"
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
        Me.SplitContainer3.Panel1.Controls.Add(Me.cvProc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gvProc)
        Me.SplitContainer3.Size = New System.Drawing.Size(1140, 433)
        Me.SplitContainer3.SplitterDistance = 341
        Me.SplitContainer3.TabIndex = 1
        '
        'cvProc
        '
        Me.cvProc.AreaDesign = CartesianArea1
        Me.cvProc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvProc.Location = New System.Drawing.Point(0, 8)
        Me.cvProc.Name = "cvProc"
        Me.cvProc.ShowGrid = False
        Me.cvProc.Size = New System.Drawing.Size(1140, 333)
        Me.cvProc.TabIndex = 5
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
        Me.MyLabel2.Size = New System.Drawing.Size(1140, 8)
        Me.MyLabel2.TabIndex = 16
        Me.MyLabel2.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'gvProc
        '
        Me.gvProc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProc.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvProc.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvProc.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvProc.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvProc.MyStopExport = False
        Me.gvProc.Name = "gvProc"
        Me.gvProc.ShowHeaderCellButtons = True
        Me.gvProc.Size = New System.Drawing.Size(1140, 88)
        Me.gvProc.TabIndex = 6
        Me.gvProc.VarID = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(553.0!, 45.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(5, 60)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1140, 433)
        Me.RadPageViewPage2.Text = "MILK MARKETING"
        Me.RadPageViewPage2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.cvSale)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvSale)
        Me.SplitContainer2.Size = New System.Drawing.Size(1136, 429)
        Me.SplitContainer2.SplitterDistance = 337
        Me.SplitContainer2.TabIndex = 2
        '
        'cvSale
        '
        Me.cvSale.AreaDesign = CartesianArea2
        Me.cvSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cvSale.Location = New System.Drawing.Point(0, 8)
        Me.cvSale.Name = "cvSale"
        Me.cvSale.ShowGrid = False
        Me.cvSale.Size = New System.Drawing.Size(1136, 329)
        Me.cvSale.TabIndex = 5
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
        Me.MyLabel1.Size = New System.Drawing.Size(1136, 8)
        Me.MyLabel1.TabIndex = 16
        Me.MyLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'gvSale
        '
        Me.gvSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSale.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvSale.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvSale.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSale.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvSale.MyStopExport = False
        Me.gvSale.Name = "gvSale"
        Me.gvSale.ShowHeaderCellButtons = True
        Me.gvSale.Size = New System.Drawing.Size(1136, 88)
        Me.gvSale.TabIndex = 6
        Me.gvSale.VarID = ""
        '
        'RCDFUnionDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1149, 535)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RCDFUnionDashboard"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sale Visual Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.cvProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProc.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvProc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.cvSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSale.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents Attachments As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents cvProc As RadChartView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents gvProc As common.UserControls.MyRadGridView
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents cvSale As RadChartView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents gvSale As common.UserControls.MyRadGridView
End Class

