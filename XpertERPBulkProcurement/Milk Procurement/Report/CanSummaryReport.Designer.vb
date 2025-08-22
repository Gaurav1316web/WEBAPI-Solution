<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Can_Summary_Report
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
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.LblTanker = New common.Controls.MyLabel()
        Me.lblBMC = New common.Controls.MyLabel()
        Me.txtDCS = New common.UserControls.txtMultiSelectFinder()
        Me.txtBMC = New common.UserControls.txtMultiSelectFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.fndArea = New common.UserControls.txtFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.LblTanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(749, 357)
        Me.SplitContainer1.SplitterDistance = 317
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
        Me.RadPageView1.Size = New System.Drawing.Size(749, 317)
        Me.RadPageView1.TabIndex = 15
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.fndArea)
        Me.RadPageViewPage1.Controls.Add(Me.LblTanker)
        Me.RadPageViewPage1.Controls.Add(Me.txtDCS)
        Me.RadPageViewPage1.Controls.Add(Me.lblBMC)
        Me.RadPageViewPage1.Controls.Add(Me.txtBMC)
        Me.RadPageViewPage1.Controls.Add(Me.lblRoute)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(728, 269)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'LblTanker
        '
        Me.LblTanker.FieldName = Nothing
        Me.LblTanker.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTanker.Location = New System.Drawing.Point(17, 73)
        Me.LblTanker.Name = "LblTanker"
        Me.LblTanker.Size = New System.Drawing.Size(29, 18)
        Me.LblTanker.TabIndex = 396
        Me.LblTanker.Text = "Area"
        '
        'lblBMC
        '
        Me.lblBMC.FieldName = Nothing
        Me.lblBMC.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMC.Location = New System.Drawing.Point(16, 129)
        Me.lblBMC.Name = "lblBMC"
        Me.lblBMC.Size = New System.Drawing.Size(27, 18)
        Me.lblBMC.TabIndex = 394
        Me.lblBMC.Text = "DCS"
        '
        'txtDCS
        '
        Me.txtDCS.arrDispalyMember = Nothing
        Me.txtDCS.arrValueMember = Nothing
        Me.txtDCS.Location = New System.Drawing.Point(78, 126)
        Me.txtDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCS.MyLinkLable1 = Me.lblBMC
        Me.txtDCS.MyLinkLable2 = Nothing
        Me.txtDCS.MyNullText = "All"
        Me.txtDCS.Name = "txtDCS"
        Me.txtDCS.Size = New System.Drawing.Size(288, 19)
        Me.txtDCS.TabIndex = 393
        '
        'txtBMC
        '
        Me.txtBMC.arrDispalyMember = Nothing
        Me.txtBMC.arrValueMember = Nothing
        Me.txtBMC.Location = New System.Drawing.Point(78, 100)
        Me.txtBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMC.MyLinkLable1 = Me.lblRoute
        Me.txtBMC.MyLinkLable2 = Nothing
        Me.txtBMC.MyNullText = "All"
        Me.txtBMC.Name = "txtBMC"
        Me.txtBMC.Size = New System.Drawing.Size(288, 19)
        Me.txtBMC.TabIndex = 391
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(16, 101)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(30, 18)
        Me.lblRoute.TabIndex = 392
        Me.lblRoute.Text = "BMC"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(350, 42)
        Me.RadGroupBox3.TabIndex = 53
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(186, 12)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 4
        Me.MyLabel2.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(236, 11)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(107, 20)
        Me.dtpToDate.TabIndex = 3
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "24/10/2011"
        Me.dtpToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
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
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(728, 269)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(66, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(60, 22)
        Me.RadButton1.TabIndex = 400
        Me.RadButton1.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(667, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 399
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(9, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(55, 22)
        Me.btnGo.TabIndex = 398
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(128, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(58, 22)
        Me.btnReset.TabIndex = 397
        Me.btnReset.Text = "Print"
        '
        'fndArea
        '
        Me.fndArea.CalculationExpression = Nothing
        Me.fndArea.FieldCode = Nothing
        Me.fndArea.FieldDesc = Nothing
        Me.fndArea.FieldMaxLength = 0
        Me.fndArea.FieldName = Nothing
        Me.fndArea.isCalculatedField = False
        Me.fndArea.IsSourceFromTable = False
        Me.fndArea.IsSourceFromValueList = False
        Me.fndArea.IsUnique = False
        Me.fndArea.Location = New System.Drawing.Point(78, 72)
        Me.fndArea.MendatroryField = True
        Me.fndArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndArea.MyLinkLable1 = Nothing
        Me.fndArea.MyLinkLable2 = Nothing
        Me.fndArea.MyReadOnly = False
        Me.fndArea.MyShowMasterFormButton = False
        Me.fndArea.Name = "fndArea"
        Me.fndArea.ReferenceFieldDesc = Nothing
        Me.fndArea.ReferenceFieldName = Nothing
        Me.fndArea.ReferenceTableName = Nothing
        Me.fndArea.Size = New System.Drawing.Size(288, 19)
        Me.fndArea.TabIndex = 1076
        Me.fndArea.Value = ""
        '
        'Can_Summary_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 357)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Can_Summary_Report"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Can_Summary_Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.LblTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents LblTanker As common.Controls.MyLabel
    Friend WithEvents lblBMC As common.Controls.MyLabel
    Friend WithEvents txtDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtBMC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents fndArea As common.UserControls.txtFinder
End Class
