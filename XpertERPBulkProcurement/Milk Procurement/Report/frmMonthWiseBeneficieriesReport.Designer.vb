<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMonthWiseBeneficieriesReport
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
        Dim TableViewDefinition13 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtDateWise = New System.Windows.Forms.RadioButton()
        Me.rbtnMonthRange = New System.Windows.Forms.RadioButton()
        Me.montrangwise = New Telerik.WinControls.UI.RadGroupBox()
        Me.daterangewise = New Telerik.WinControls.UI.RadGroupBox()
        Me.todate = New common.Controls.MyDateTimePicker()
        Me.fromdate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDCSWise = New System.Windows.Forms.RadioButton()
        Me.rbtnMPWise = New System.Windows.Forms.RadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.montrangwise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.montrangwise.SuspendLayout()
        CType(Me.daterangewise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.daterangewise.SuspendLayout()
        CType(Me.todate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 352)
        Me.SplitContainer1.SplitterDistance = 319
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
        Me.RadPageView1.Size = New System.Drawing.Size(800, 319)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.daterangewise)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.montrangwise)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 271)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rbtDateWise)
        Me.RadGroupBox3.Controls.Add(Me.rbtnMonthRange)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(349, 10)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(305, 34)
        Me.RadGroupBox3.TabIndex = 23
        '
        'rbtDateWise
        '
        Me.rbtDateWise.AutoSize = True
        Me.rbtDateWise.Location = New System.Drawing.Point(138, 8)
        Me.rbtDateWise.Name = "rbtDateWise"
        Me.rbtDateWise.Size = New System.Drawing.Size(77, 17)
        Me.rbtDateWise.TabIndex = 1
        Me.rbtDateWise.Text = "Date Wise"
        Me.rbtDateWise.UseVisualStyleBackColor = True
        '
        'rbtnMonthRange
        '
        Me.rbtnMonthRange.AutoSize = True
        Me.rbtnMonthRange.Checked = True
        Me.rbtnMonthRange.Location = New System.Drawing.Point(15, 8)
        Me.rbtnMonthRange.Name = "rbtnMonthRange"
        Me.rbtnMonthRange.Size = New System.Drawing.Size(88, 17)
        Me.rbtnMonthRange.TabIndex = 0
        Me.rbtnMonthRange.TabStop = True
        Me.rbtnMonthRange.Text = "Month Wise"
        Me.rbtnMonthRange.UseVisualStyleBackColor = True
        '
        'montrangwise
        '
        Me.montrangwise.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.montrangwise.Controls.Add(Me.RadLabel3)
        Me.montrangwise.Controls.Add(Me.txtToDate)
        Me.montrangwise.Controls.Add(Me.txtFromDate)
        Me.montrangwise.Controls.Add(Me.MyLabel1)
        Me.montrangwise.HeaderText = "Month Range"
        Me.montrangwise.Location = New System.Drawing.Point(33, 0)
        Me.montrangwise.Name = "montrangwise"
        Me.montrangwise.Size = New System.Drawing.Size(305, 51)
        Me.montrangwise.TabIndex = 22
        Me.montrangwise.Text = "Month Range"
        Me.montrangwise.Visible = False
        '
        'daterangewise
        '
        Me.daterangewise.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.daterangewise.Controls.Add(Me.todate)
        Me.daterangewise.Controls.Add(Me.fromdate)
        Me.daterangewise.Controls.Add(Me.lblToDate)
        Me.daterangewise.Controls.Add(Me.lblfromDate)
        Me.daterangewise.HeaderText = "Date Wise"
        Me.daterangewise.Location = New System.Drawing.Point(33, 10)
        Me.daterangewise.Name = "daterangewise"
        Me.daterangewise.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.daterangewise.Size = New System.Drawing.Size(305, 41)
        Me.daterangewise.TabIndex = 24
        Me.daterangewise.Text = "Date Wise"
        Me.daterangewise.Visible = False
        '
        'todate
        '
        Me.todate.CalculationExpression = Nothing
        Me.todate.CustomFormat = "dd-MM-yyyy"
        Me.todate.FieldCode = Nothing
        Me.todate.FieldDesc = Nothing
        Me.todate.FieldMaxLength = 0
        Me.todate.FieldName = Nothing
        Me.todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.todate.isCalculatedField = False
        Me.todate.IsSourceFromTable = False
        Me.todate.IsSourceFromValueList = False
        Me.todate.IsUnique = False
        Me.todate.Location = New System.Drawing.Point(214, 11)
        Me.todate.MendatroryField = False
        Me.todate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.todate.MyLinkLable1 = Nothing
        Me.todate.MyLinkLable2 = Nothing
        Me.todate.Name = "todate"
        Me.todate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.todate.ReferenceFieldDesc = Nothing
        Me.todate.ReferenceFieldName = Nothing
        Me.todate.ReferenceTableName = Nothing
        Me.todate.Size = New System.Drawing.Size(82, 20)
        Me.todate.TabIndex = 1
        Me.todate.TabStop = False
        Me.todate.Text = "17-12-2011"
        Me.todate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'fromdate
        '
        Me.fromdate.CalculationExpression = Nothing
        Me.fromdate.CustomFormat = "dd-MM-yyyy"
        Me.fromdate.FieldCode = Nothing
        Me.fromdate.FieldDesc = Nothing
        Me.fromdate.FieldMaxLength = 0
        Me.fromdate.FieldName = Nothing
        Me.fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromdate.isCalculatedField = False
        Me.fromdate.IsSourceFromTable = False
        Me.fromdate.IsSourceFromValueList = False
        Me.fromdate.IsUnique = False
        Me.fromdate.Location = New System.Drawing.Point(75, 11)
        Me.fromdate.MendatroryField = False
        Me.fromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromdate.MyLinkLable1 = Nothing
        Me.fromdate.MyLinkLable2 = Nothing
        Me.fromdate.Name = "fromdate"
        Me.fromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromdate.ReferenceFieldDesc = Nothing
        Me.fromdate.ReferenceFieldName = Nothing
        Me.fromdate.ReferenceTableName = Nothing
        Me.fromdate.Size = New System.Drawing.Size(82, 20)
        Me.fromdate.TabIndex = 0
        Me.fromdate.TabStop = False
        Me.fromdate.Text = "17-12-2011"
        Me.fromdate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(163, 13)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(9, 12)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(12, 21)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel3.TabIndex = 19
        Me.RadLabel3.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "MMM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(161, 20)
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
        Me.txtToDate.TabIndex = 20
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "Dec-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
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
        Me.txtFromDate.Location = New System.Drawing.Point(48, 20)
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
        Me.txtFromDate.TabIndex = 18
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "Dec-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(139, 21)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel1.TabIndex = 21
        Me.MyLabel1.Text = "To"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnDCSWise)
        Me.RadGroupBox1.Controls.Add(Me.rbtnMPWise)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(33, 57)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(305, 34)
        Me.RadGroupBox1.TabIndex = 0
        '
        'rbtnDCSWise
        '
        Me.rbtnDCSWise.AutoSize = True
        Me.rbtnDCSWise.Location = New System.Drawing.Point(141, 8)
        Me.rbtnDCSWise.Name = "rbtnDCSWise"
        Me.rbtnDCSWise.Size = New System.Drawing.Size(74, 17)
        Me.rbtnDCSWise.TabIndex = 1
        Me.rbtnDCSWise.TabStop = True
        Me.rbtnDCSWise.Text = "DCS Wise"
        Me.rbtnDCSWise.UseVisualStyleBackColor = True
        '
        'rbtnMPWise
        '
        Me.rbtnMPWise.AutoSize = True
        Me.rbtnMPWise.Location = New System.Drawing.Point(15, 8)
        Me.rbtnMPWise.Name = "rbtnMPWise"
        Me.rbtnMPWise.Size = New System.Drawing.Size(69, 17)
        Me.rbtnMPWise.TabIndex = 0
        Me.rbtnMPWise.TabStop = True
        Me.rbtnMPWise.Text = "MP Wise"
        Me.rbtnMPWise.UseVisualStyleBackColor = True
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 359)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition13
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(779, 359)
        Me.gv1.TabIndex = 0
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(157, 4)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(71, 21)
        Me.RadSplitButton1.TabIndex = 48
        Me.RadSplitButton1.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        Me.rmiPDF.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(701, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 21)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(84, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 21)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(12, 3)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 21)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = ">>>"
        '
        'frmMonthWiseBeneficieriesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 352)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMonthWiseBeneficieriesReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmMonthWiseBeneficieriesReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.montrangwise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.montrangwise.ResumeLayout(False)
        Me.montrangwise.PerformLayout()
        CType(Me.daterangewise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.daterangewise.ResumeLayout(False)
        Me.daterangewise.PerformLayout()
        CType(Me.todate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbtnDCSWise As RadioButton
    Friend WithEvents rbtnMPWise As RadioButton
    Friend WithEvents gv1 As RadGridView
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents montrangwise As RadGroupBox
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents rbtnMonthRange As RadioButton
    Friend WithEvents rbtDateWise As RadioButton
    Friend WithEvents daterangewise As RadGroupBox
    Friend WithEvents todate As common.Controls.MyDateTimePicker
    Friend WithEvents fromdate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
End Class
