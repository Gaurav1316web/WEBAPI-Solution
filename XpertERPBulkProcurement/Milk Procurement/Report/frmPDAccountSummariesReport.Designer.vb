<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPDAccountSummariesReport
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtMultUnion = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblFromdate = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToMonth = New common.Controls.MyDateTimePicker()
        Me.txtFromMonth = New common.Controls.MyDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnNEFTUploader = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNEFTUploader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnNEFTUploader)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 402)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtMultUnion)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 354)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'txtMultUnion
        '
        Me.txtMultUnion.arrDispalyMember = Nothing
        Me.txtMultUnion.arrValueMember = Nothing
        Me.txtMultUnion.Location = New System.Drawing.Point(66, 60)
        Me.txtMultUnion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultUnion.MyLinkLable1 = Me.MyLabel16
        Me.txtMultUnion.MyLinkLable2 = Nothing
        Me.txtMultUnion.MyNullText = "All"
        Me.txtMultUnion.Name = "txtMultUnion"
        Me.txtMultUnion.Size = New System.Drawing.Size(261, 19)
        Me.txtMultUnion.TabIndex = 448
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(23, 60)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel16.TabIndex = 449
        Me.MyLabel16.Text = "Union"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.lblFromdate)
        Me.RadGroupBox3.Controls.Add(Me.lblToDate)
        Me.RadGroupBox3.Controls.Add(Me.txtToMonth)
        Me.RadGroupBox3.Controls.Add(Me.txtFromMonth)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(17, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(310, 51)
        Me.RadGroupBox3.TabIndex = 447
        Me.RadGroupBox3.Text = "Date Range"
        '
        'lblFromdate
        '
        Me.lblFromdate.FieldName = Nothing
        Me.lblFromdate.Location = New System.Drawing.Point(5, 20)
        Me.lblFromdate.Name = "lblFromdate"
        Me.lblFromdate.Size = New System.Drawing.Size(69, 18)
        Me.lblFromdate.TabIndex = 77
        Me.lblFromdate.Text = "From Month"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(161, 20)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(55, 18)
        Me.lblToDate.TabIndex = 78
        Me.lblToDate.Text = "To Month"
        '
        'txtToMonth
        '
        Me.txtToMonth.CalculationExpression = Nothing
        Me.txtToMonth.CustomFormat = "MMM/yyyy"
        Me.txtToMonth.FieldCode = Nothing
        Me.txtToMonth.FieldDesc = Nothing
        Me.txtToMonth.FieldMaxLength = 0
        Me.txtToMonth.FieldName = Nothing
        Me.txtToMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToMonth.isCalculatedField = False
        Me.txtToMonth.IsSourceFromTable = False
        Me.txtToMonth.IsSourceFromValueList = False
        Me.txtToMonth.IsUnique = False
        Me.txtToMonth.Location = New System.Drawing.Point(221, 19)
        Me.txtToMonth.MendatroryField = False
        Me.txtToMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToMonth.MyLinkLable1 = Nothing
        Me.txtToMonth.MyLinkLable2 = Nothing
        Me.txtToMonth.Name = "txtToMonth"
        Me.txtToMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToMonth.ReferenceFieldDesc = Nothing
        Me.txtToMonth.ReferenceFieldName = Nothing
        Me.txtToMonth.ReferenceTableName = Nothing
        Me.txtToMonth.Size = New System.Drawing.Size(82, 20)
        Me.txtToMonth.TabIndex = 2
        Me.txtToMonth.TabStop = False
        Me.txtToMonth.Text = "May/2011"
        Me.txtToMonth.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'txtFromMonth
        '
        Me.txtFromMonth.CalculationExpression = Nothing
        Me.txtFromMonth.CustomFormat = "MMM/yyyy"
        Me.txtFromMonth.FieldCode = Nothing
        Me.txtFromMonth.FieldDesc = Nothing
        Me.txtFromMonth.FieldMaxLength = 0
        Me.txtFromMonth.FieldName = Nothing
        Me.txtFromMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromMonth.isCalculatedField = False
        Me.txtFromMonth.IsSourceFromTable = False
        Me.txtFromMonth.IsSourceFromValueList = False
        Me.txtFromMonth.IsUnique = False
        Me.txtFromMonth.Location = New System.Drawing.Point(77, 19)
        Me.txtFromMonth.MendatroryField = False
        Me.txtFromMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromMonth.MyLinkLable1 = Nothing
        Me.txtFromMonth.MyLinkLable2 = Nothing
        Me.txtFromMonth.Name = "txtFromMonth"
        Me.txtFromMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromMonth.ReferenceFieldDesc = Nothing
        Me.txtFromMonth.ReferenceFieldName = Nothing
        Me.txtFromMonth.ReferenceTableName = Nothing
        Me.txtFromMonth.Size = New System.Drawing.Size(78, 20)
        Me.txtFromMonth.TabIndex = 1
        Me.txtFromMonth.TabStop = False
        Me.txtFromMonth.Text = "May/2011"
        Me.txtFromMonth.Value = New Date(2011, 5, 1, 12, 41, 0, 0)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 354)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(779, 354)
        Me.gv1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(716, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(73, 20)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(85, 12)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(73, 20)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(10, 12)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(73, 20)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = ">>>"
        '
        'btnNEFTUploader
        '
        Me.btnNEFTUploader.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNEFTUploader.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnNEFTUploader.Location = New System.Drawing.Point(160, 12)
        Me.btnNEFTUploader.Name = "btnNEFTUploader"
        Me.btnNEFTUploader.Size = New System.Drawing.Size(73, 20)
        Me.btnNEFTUploader.TabIndex = 159
        Me.btnNEFTUploader.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'frmPDAccountSummariesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPDAccountSummariesReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmPDAccountSummariesReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromMonth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNEFTUploader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents gv1 As RadGridView
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents lblFromdate As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToMonth As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromMonth As common.Controls.MyDateTimePicker
    Friend WithEvents txtMultUnion As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents btnNEFTUploader As RadSplitButton
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
End Class
