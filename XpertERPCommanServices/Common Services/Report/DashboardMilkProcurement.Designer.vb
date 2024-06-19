Imports common
Imports XpertERPEngine
Imports XpertERPEngineFine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashboardMilkProcurement
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbdAllTrans = New System.Windows.Forms.RadioButton()
        Me.rdbPosted = New System.Windows.Forms.RadioButton()
        Me.rdbUnposted = New System.Windows.Forms.RadioButton()
        Me.chkRJSBNS = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromdate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv3 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv4 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.chkRJSBNS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gv4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv4.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btngo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 413)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.chkRJSBNS)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblToDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblFromdate)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 365)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbdAllTrans)
        Me.RadGroupBox2.Controls.Add(Me.rdbPosted)
        Me.RadGroupBox2.Controls.Add(Me.rdbUnposted)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(304, 6)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(269, 29)
        Me.RadGroupBox2.TabIndex = 443
        Me.RadGroupBox2.Visible = False
        '
        'rbdAllTrans
        '
        Me.rbdAllTrans.AutoSize = True
        Me.rbdAllTrans.Checked = True
        Me.rbdAllTrans.Location = New System.Drawing.Point(165, 6)
        Me.rbdAllTrans.Name = "rbdAllTrans"
        Me.rbdAllTrans.Size = New System.Drawing.Size(95, 17)
        Me.rbdAllTrans.TabIndex = 439
        Me.rbdAllTrans.TabStop = True
        Me.rbdAllTrans.Text = "All Transation"
        Me.rbdAllTrans.UseVisualStyleBackColor = True
        '
        'rdbPosted
        '
        Me.rdbPosted.AutoSize = True
        Me.rdbPosted.Location = New System.Drawing.Point(13, 5)
        Me.rdbPosted.Name = "rdbPosted"
        Me.rdbPosted.Size = New System.Drawing.Size(60, 17)
        Me.rdbPosted.TabIndex = 437
        Me.rdbPosted.Text = "Posted"
        Me.rdbPosted.UseVisualStyleBackColor = True
        '
        'rdbUnposted
        '
        Me.rdbUnposted.AutoSize = True
        Me.rdbUnposted.Location = New System.Drawing.Point(83, 6)
        Me.rdbUnposted.Name = "rdbUnposted"
        Me.rdbUnposted.Size = New System.Drawing.Size(76, 17)
        Me.rdbUnposted.TabIndex = 438
        Me.rdbUnposted.Text = "Unposted"
        Me.rdbUnposted.UseVisualStyleBackColor = True
        '
        'chkRJSBNS
        '
        Me.chkRJSBNS.Location = New System.Drawing.Point(653, 17)
        Me.chkRJSBNS.Name = "chkRJSBNS"
        Me.chkRJSBNS.Size = New System.Drawing.Size(64, 18)
        Me.chkRJSBNS.TabIndex = 444
        Me.chkRJSBNS.Text = "RJS & BNS"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(68, 15)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(78, 20)
        Me.txtFromDate.TabIndex = 79
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "30/05/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(203, 15)
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
        Me.txtToDate.TabIndex = 80
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "30/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(152, 17)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 82
        Me.lblToDate.Text = "To Date"
        '
        'lblFromdate
        '
        Me.lblFromdate.FieldName = Nothing
        Me.lblFromdate.Location = New System.Drawing.Point(3, 15)
        Me.lblFromdate.Name = "lblFromdate"
        Me.lblFromdate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromdate.TabIndex = 81
        Me.lblFromdate.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(70.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 365)
        Me.RadPageViewPage2.Text = "UnionWise"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 365)
        Me.gv1.TabIndex = 3
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gv2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(94.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(779, 365)
        Me.RadPageViewPage3.Text = "Count Of Route"
        '
        'gv2
        '
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowGroupedColumns = True
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(779, 365)
        Me.gv2.TabIndex = 3
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.gv3)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(779, 365)
        Me.RadPageViewPage4.Text = "Count Of DCS"
        '
        'gv3
        '
        Me.gv3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv3.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv3.MasterTemplate.AllowAddNewRow = False
        Me.gv3.MasterTemplate.AllowDeleteRow = False
        Me.gv3.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv3.MasterTemplate.ShowGroupedColumns = True
        Me.gv3.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv3.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv3.MyStopExport = False
        Me.gv3.Name = "gv3"
        Me.gv3.ShowHeaderCellButtons = True
        Me.gv3.Size = New System.Drawing.Size(779, 365)
        Me.gv3.TabIndex = 3
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gv4)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(76.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(779, 365)
        Me.RadPageViewPage5.Text = "7 Days Data"
        '
        'gv4
        '
        Me.gv4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv4.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv4.MasterTemplate.AllowAddNewRow = False
        Me.gv4.MasterTemplate.AllowDeleteRow = False
        Me.gv4.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv4.MasterTemplate.ShowGroupedColumns = True
        Me.gv4.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv4.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv4.MyStopExport = False
        Me.gv4.Name = "gv4"
        Me.gv4.ShowHeaderCellButtons = True
        Me.gv4.Size = New System.Drawing.Size(779, 365)
        Me.gv4.TabIndex = 4
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(252, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(77, 18)
        Me.btnPrint.TabIndex = 337
        Me.btnPrint.Text = "Print"
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(173, 7)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(77, 18)
        Me.btnExp.TabIndex = 337
        Me.btnExp.Text = "Export"
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
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(93, 7)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(77, 18)
        Me.btnreset.TabIndex = 336
        Me.btnreset.Text = "Reset"
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngo.Location = New System.Drawing.Point(11, 7)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(77, 18)
        Me.btngo.TabIndex = 335
        Me.btngo.Text = ">>>"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(712, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(77, 18)
        Me.btnclose.TabIndex = 334
        Me.btnclose.Text = "Close"
        '
        'DashboardMilkProcurement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "DashboardMilkProcurement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "DashboardMilkProcurement"
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
        CType(Me.chkRJSBNS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gv4.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents gv3 As common.UserControls.MyRadGridView
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromdate As common.Controls.MyLabel
    Friend WithEvents btnreset As RadButton
    Friend WithEvents btngo As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnExp As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents chkRJSBNS As RadCheckBox
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents gv4 As UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbdAllTrans As RadioButton
    Friend WithEvents rdbPosted As RadioButton
    Friend WithEvents rdbUnposted As RadioButton
End Class
