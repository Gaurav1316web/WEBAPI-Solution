<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDBTNEFTUnionReport
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnYearly = New common.Controls.MyRadioButton()
        Me.rbtnQuarterly = New common.Controls.MyRadioButton()
        Me.chkOnlyReject = New System.Windows.Forms.CheckBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnYearly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnQuarterly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 407
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
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(800, 407)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnlyReject)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 359)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnYearly)
        Me.RadGroupBox2.Controls.Add(Me.rbtnQuarterly)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 6)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(191, 37)
        Me.RadGroupBox2.TabIndex = 442
        '
        'rbtnYearly
        '
        Me.rbtnYearly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnYearly.Location = New System.Drawing.Point(5, 9)
        Me.rbtnYearly.MyLinkLable1 = Nothing
        Me.rbtnYearly.MyLinkLable2 = Nothing
        Me.rbtnYearly.Name = "rbtnYearly"
        Me.rbtnYearly.Size = New System.Drawing.Size(50, 18)
        Me.rbtnYearly.TabIndex = 396
        Me.rbtnYearly.Text = "Yearly"
        Me.rbtnYearly.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnQuarterly
        '
        Me.rbtnQuarterly.Location = New System.Drawing.Point(87, 9)
        Me.rbtnQuarterly.MyLinkLable1 = Nothing
        Me.rbtnQuarterly.MyLinkLable2 = Nothing
        Me.rbtnQuarterly.Name = "rbtnQuarterly"
        Me.rbtnQuarterly.Size = New System.Drawing.Size(67, 18)
        Me.rbtnQuarterly.TabIndex = 391
        Me.rbtnQuarterly.TabStop = False
        Me.rbtnQuarterly.Text = "Quarterly"
        '
        'chkOnlyReject
        '
        Me.chkOnlyReject.AutoSize = True
        Me.chkOnlyReject.Location = New System.Drawing.Point(223, 16)
        Me.chkOnlyReject.Name = "chkOnlyReject"
        Me.chkOnlyReject.Size = New System.Drawing.Size(84, 17)
        Me.chkOnlyReject.TabIndex = 15
        Me.chkOnlyReject.Text = "Only Reject"
        Me.chkOnlyReject.UseVisualStyleBackColor = True
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "MMM-yyyy"
        Me.txtToDate.Enabled = False
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(227, 58)
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
        Me.txtToDate.TabIndex = 13
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "Dec-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(167, 59)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 14
        Me.MyLabel1.Text = "To Month"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "MMM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(79, 58)
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
        Me.txtFromDate.TabIndex = 11
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "Dec-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(7, 59)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(69, 18)
        Me.RadLabel3.TabIndex = 12
        Me.RadLabel3.Text = "From Month"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 359)
        Me.RadPageViewPage2.Text = "Report"
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel})
        Me.btnExp.Location = New System.Drawing.Point(236, 10)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(83, 21)
        Me.btnExp.TabIndex = 158
        Me.btnExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(162, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(89, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(70, 22)
        Me.btnReset.TabIndex = 5
        Me.btnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(718, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 22)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReport.Location = New System.Drawing.Point(16, 9)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 22)
        Me.btnReport.TabIndex = 7
        Me.btnReport.Text = ">>>"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowGroupedColumns = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(779, 359)
        Me.gv.TabIndex = 3
        Me.gv.VarID = ""
        '
        'frmDBTNEFTUnionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDBTNEFTUnionReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDBTNEFTUnionReport"
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
        CType(Me.rbtnYearly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnQuarterly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnReport As RadButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents chkOnlyReject As CheckBox
    Friend WithEvents btnExp As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnYearly As common.Controls.MyRadioButton
    Friend WithEvents rbtnQuarterly As common.Controls.MyRadioButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
End Class
