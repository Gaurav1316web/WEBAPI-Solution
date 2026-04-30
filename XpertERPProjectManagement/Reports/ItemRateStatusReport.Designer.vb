<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemRateStatusReport
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDefaultUOM = New common.Controls.MyRadioButton()
        Me.rbtnReportUOM = New common.Controls.MyRadioButton()
        Me.rbtnPrintUOM = New common.Controls.MyRadioButton()
        Me.txtPriceCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.lblItem = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnDefaultUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReportUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnPrintUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 387
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 387)
        Me.RadPageView1.TabIndex = 16
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtPriceCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtItem)
        Me.RadPageViewPage1.Controls.Add(Me.lblItem)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 339)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnDefaultUOM)
        Me.RadGroupBox2.Controls.Add(Me.rbtnReportUOM)
        Me.RadGroupBox2.Controls.Add(Me.rbtnPrintUOM)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(214, 19)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(303, 32)
        Me.RadGroupBox2.TabIndex = 441
        '
        'rbtnDefaultUOM
        '
        Me.rbtnDefaultUOM.Location = New System.Drawing.Point(192, 7)
        Me.rbtnDefaultUOM.MyLinkLable1 = Nothing
        Me.rbtnDefaultUOM.MyLinkLable2 = Nothing
        Me.rbtnDefaultUOM.Name = "rbtnDefaultUOM"
        Me.rbtnDefaultUOM.Size = New System.Drawing.Size(86, 18)
        Me.rbtnDefaultUOM.TabIndex = 397
        Me.rbtnDefaultUOM.TabStop = False
        Me.rbtnDefaultUOM.Text = "Default UOM"
        '
        'rbtnReportUOM
        '
        Me.rbtnReportUOM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnReportUOM.Location = New System.Drawing.Point(6, 9)
        Me.rbtnReportUOM.MyLinkLable1 = Nothing
        Me.rbtnReportUOM.MyLinkLable2 = Nothing
        Me.rbtnReportUOM.Name = "rbtnReportUOM"
        Me.rbtnReportUOM.Size = New System.Drawing.Size(84, 18)
        Me.rbtnReportUOM.TabIndex = 396
        Me.rbtnReportUOM.Text = "Report UOM"
        Me.rbtnReportUOM.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnPrintUOM
        '
        Me.rbtnPrintUOM.Location = New System.Drawing.Point(102, 9)
        Me.rbtnPrintUOM.MyLinkLable1 = Nothing
        Me.rbtnPrintUOM.MyLinkLable2 = Nothing
        Me.rbtnPrintUOM.Name = "rbtnPrintUOM"
        Me.rbtnPrintUOM.Size = New System.Drawing.Size(73, 18)
        Me.rbtnPrintUOM.TabIndex = 391
        Me.rbtnPrintUOM.TabStop = False
        Me.rbtnPrintUOM.Text = "Print UOM"
        '
        'txtPriceCode
        '
        Me.txtPriceCode.arrDispalyMember = Nothing
        Me.txtPriceCode.arrValueMember = Nothing
        Me.txtPriceCode.Location = New System.Drawing.Point(78, 81)
        Me.txtPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceCode.MyLinkLable1 = Me.lblCode
        Me.txtPriceCode.MyLinkLable2 = Nothing
        Me.txtPriceCode.MyNullText = "All"
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(288, 19)
        Me.txtPriceCode.TabIndex = 393
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(23, 82)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(30, 18)
        Me.lblCode.TabIndex = 394
        Me.lblCode.Text = "Price"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(78, 57)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.lblItem
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(288, 19)
        Me.txtItem.TabIndex = 391
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(21, 58)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 392
        Me.lblItem.Text = "Item"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(192, 42)
        Me.RadGroupBox3.TabIndex = 53
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From Date"
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(67, 11)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(107, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(728, 269)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Gv1.ForeColor = System.Drawing.Color.Black
        Me.Gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.MyExportAPI = False
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(728, 269)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.Location = New System.Drawing.Point(136, 19)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(60, 22)
        Me.btnExport.TabIndex = 404
        Me.btnExport.Text = "Export"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreset.Location = New System.Drawing.Point(70, 19)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(60, 22)
        Me.btnreset.TabIndex = 403
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(717, 25)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 402
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(10, 19)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(55, 22)
        Me.btnGo.TabIndex = 401
        Me.btnGo.Text = ">>>"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(202, 19)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(55, 22)
        Me.btnPrint.TabIndex = 405
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'ItemRateStatusReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ItemRateStatusReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "ItemRateStatusReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnDefaultUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReportUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnPrintUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents txtPriceCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnreset As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnDefaultUOM As common.Controls.MyRadioButton
    Friend WithEvents rbtnReportUOM As common.Controls.MyRadioButton
    Friend WithEvents rbtnPrintUOM As common.Controls.MyRadioButton
    Friend WithEvents btnExport As RadButton
    Friend WithEvents btnPrint As RadButton
End Class
