Imports common
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptdailydispatch
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
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnMilk = New System.Windows.Forms.RadioButton()
        Me.rbtnproduct = New System.Windows.Forms.RadioButton()
        Me.txtlocation = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lbllocation = New common.Controls.MyLabel()
        Me.txtcustomer = New common.UserControls.txtFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txttodate = New common.Controls.MyDateTimePicker()
        Me.txtfromdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblcustomer = New common.Controls.MyLabel()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtrouteno = New common.UserControls.txtMultiSelectFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txttodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadPanel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 418
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
        Me.RadPageView1.Size = New System.Drawing.Size(800, 418)
        Me.RadPageView1.TabIndex = 5
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 370)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.txtrouteno)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox7)
        Me.RadPanel1.Controls.Add(Me.txtlocation)
        Me.RadPanel1.Controls.Add(Me.txtcustomer)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.lbllocation)
        Me.RadPanel1.Controls.Add(Me.RadLabel2)
        Me.RadPanel1.Controls.Add(Me.RadLabel15)
        Me.RadPanel1.Controls.Add(Me.lblcustomer)
        Me.RadPanel1.Controls.Add(Me.lblRouteNo)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(779, 370)
        Me.RadPanel1.TabIndex = 15
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.rbtnMilk)
        Me.RadGroupBox7.Controls.Add(Me.rbtnproduct)
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(437, 13)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Size = New System.Drawing.Size(157, 26)
        Me.RadGroupBox7.TabIndex = 1464
        '
        'rbtnMilk
        '
        Me.rbtnMilk.AutoSize = True
        Me.rbtnMilk.Checked = True
        Me.rbtnMilk.Location = New System.Drawing.Point(5, 4)
        Me.rbtnMilk.Name = "rbtnMilk"
        Me.rbtnMilk.Size = New System.Drawing.Size(47, 17)
        Me.rbtnMilk.TabIndex = 440
        Me.rbtnMilk.TabStop = True
        Me.rbtnMilk.Text = "Milk"
        Me.rbtnMilk.UseVisualStyleBackColor = True
        '
        'rbtnproduct
        '
        Me.rbtnproduct.AutoSize = True
        Me.rbtnproduct.Location = New System.Drawing.Point(83, 4)
        Me.rbtnproduct.Name = "rbtnproduct"
        Me.rbtnproduct.Size = New System.Drawing.Size(65, 17)
        Me.rbtnproduct.TabIndex = 441
        Me.rbtnproduct.Text = "Product"
        Me.rbtnproduct.UseVisualStyleBackColor = True
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(80, 99)
        Me.txtlocation.Margin = New System.Windows.Forms.Padding(4)
        Me.txtlocation.MendatroryField = False
        Me.txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.MyLinkLable1 = Me.RadLabel2
        Me.txtlocation.MyLinkLable2 = Me.lbllocation
        Me.txtlocation.MyReadOnly = False
        Me.txtlocation.MyShowMasterFormButton = False
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(157, 18)
        Me.txtlocation.TabIndex = 1461
        Me.txtlocation.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(18, 81)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 1458
        Me.RadLabel2.Text = "Customer"
        '
        'lbllocation
        '
        Me.lbllocation.AutoSize = False
        Me.lbllocation.BorderVisible = True
        Me.lbllocation.FieldName = Nothing
        Me.lbllocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllocation.Location = New System.Drawing.Point(239, 99)
        Me.lbllocation.Name = "lbllocation"
        Me.lbllocation.Size = New System.Drawing.Size(192, 18)
        Me.lbllocation.TabIndex = 1457
        Me.lbllocation.TextWrap = False
        '
        'txtcustomer
        '
        Me.txtcustomer.CalculationExpression = Nothing
        Me.txtcustomer.FieldCode = Nothing
        Me.txtcustomer.FieldDesc = Nothing
        Me.txtcustomer.FieldMaxLength = 0
        Me.txtcustomer.FieldName = Nothing
        Me.txtcustomer.isCalculatedField = False
        Me.txtcustomer.IsSourceFromTable = False
        Me.txtcustomer.IsSourceFromValueList = False
        Me.txtcustomer.IsUnique = False
        Me.txtcustomer.Location = New System.Drawing.Point(80, 79)
        Me.txtcustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.txtcustomer.MendatroryField = False
        Me.txtcustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.MyLinkLable1 = Me.RadLabel2
        Me.txtcustomer.MyLinkLable2 = Me.lbllocation
        Me.txtcustomer.MyReadOnly = False
        Me.txtcustomer.MyShowMasterFormButton = False
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.ReferenceFieldDesc = Nothing
        Me.txtcustomer.ReferenceFieldName = Nothing
        Me.txtcustomer.ReferenceTableName = Nothing
        Me.txtcustomer.Size = New System.Drawing.Size(157, 18)
        Me.txtcustomer.TabIndex = 1460
        Me.txtcustomer.Value = ""
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txttodate)
        Me.RadGroupBox1.Controls.Add(Me.txtfromdate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(18, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(413, 39)
        Me.RadGroupBox1.TabIndex = 1459
        '
        'txttodate
        '
        Me.txttodate.CalculationExpression = Nothing
        Me.txttodate.CustomFormat = "dd-MM-yyyy"
        Me.txttodate.FieldCode = Nothing
        Me.txttodate.FieldDesc = Nothing
        Me.txttodate.FieldMaxLength = 0
        Me.txttodate.FieldName = Nothing
        Me.txttodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txttodate.isCalculatedField = False
        Me.txttodate.IsSourceFromTable = False
        Me.txttodate.IsSourceFromValueList = False
        Me.txttodate.IsUnique = False
        Me.txttodate.Location = New System.Drawing.Point(221, 11)
        Me.txttodate.MendatroryField = False
        Me.txttodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txttodate.MyLinkLable1 = Nothing
        Me.txttodate.MyLinkLable2 = Nothing
        Me.txttodate.Name = "txttodate"
        Me.txttodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txttodate.ReferenceFieldDesc = Nothing
        Me.txttodate.ReferenceFieldName = Nothing
        Me.txttodate.ReferenceTableName = Nothing
        Me.txttodate.Size = New System.Drawing.Size(82, 20)
        Me.txttodate.TabIndex = 1
        Me.txttodate.TabStop = False
        Me.txttodate.Text = "17-12-2011"
        Me.txttodate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtfromdate
        '
        Me.txtfromdate.CalculationExpression = Nothing
        Me.txtfromdate.CustomFormat = "dd-MM-yyyy"
        Me.txtfromdate.FieldCode = Nothing
        Me.txtfromdate.FieldDesc = Nothing
        Me.txtfromdate.FieldMaxLength = 0
        Me.txtfromdate.FieldName = Nothing
        Me.txtfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfromdate.isCalculatedField = False
        Me.txtfromdate.IsSourceFromTable = False
        Me.txtfromdate.IsSourceFromValueList = False
        Me.txtfromdate.IsUnique = False
        Me.txtfromdate.Location = New System.Drawing.Point(82, 11)
        Me.txtfromdate.MendatroryField = False
        Me.txtfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromdate.MyLinkLable1 = Nothing
        Me.txtfromdate.MyLinkLable2 = Nothing
        Me.txtfromdate.Name = "txtfromdate"
        Me.txtfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfromdate.ReferenceFieldDesc = Nothing
        Me.txtfromdate.ReferenceFieldName = Nothing
        Me.txtfromdate.ReferenceTableName = Nothing
        Me.txtfromdate.Size = New System.Drawing.Size(82, 20)
        Me.txtfromdate.TabIndex = 0
        Me.txtfromdate.TabStop = False
        Me.txtfromdate.Text = "17-12-2011"
        Me.txtfromdate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(170, 13)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 14
        Me.MyLabel1.Text = "To Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(9, 12)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 13
        Me.MyLabel2.Text = "From Date"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(20, 100)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 153
        Me.RadLabel15.Text = "Location"
        '
        'lblcustomer
        '
        Me.lblcustomer.AutoSize = False
        Me.lblcustomer.BorderVisible = True
        Me.lblcustomer.FieldName = Nothing
        Me.lblcustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcustomer.Location = New System.Drawing.Point(239, 79)
        Me.lblcustomer.Name = "lblcustomer"
        Me.lblcustomer.Size = New System.Drawing.Size(192, 18)
        Me.lblcustomer.TabIndex = 152
        Me.lblcustomer.TextWrap = False
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(18, 59)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 150
        Me.lblRouteNo.Text = "Route No"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 370)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 370)
        Me.gv1.TabIndex = 2
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.btngo)
        Me.RadPanel2.Controls.Add(Me.btnclose)
        Me.RadPanel2.Controls.Add(Me.btnPrint)
        Me.RadPanel2.Controls.Add(Me.btnreset)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel2.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(800, 28)
        Me.RadPanel2.TabIndex = 339
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngo.Location = New System.Drawing.Point(10, 7)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(77, 18)
        Me.btngo.TabIndex = 340
        Me.btngo.Text = ">>>"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(711, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(77, 18)
        Me.btnclose.TabIndex = 343
        Me.btnclose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(90, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(77, 18)
        Me.btnPrint.TabIndex = 341
        Me.btnPrint.Text = "Print"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(170, 7)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(77, 18)
        Me.btnreset.TabIndex = 342
        Me.btnreset.Text = "Reset"
        '
        'txtrouteno
        '
        Me.txtrouteno.arrDispalyMember = Nothing
        Me.txtrouteno.arrValueMember = Nothing
        Me.txtrouteno.Location = New System.Drawing.Point(80, 59)
        Me.txtrouteno.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrouteno.MyLinkLable1 = Nothing
        Me.txtrouteno.MyLinkLable2 = Nothing
        Me.txtrouteno.MyNullText = "All"
        Me.txtrouteno.Name = "txtrouteno"
        Me.txtrouteno.Size = New System.Drawing.Size(351, 18)
        Me.txtrouteno.TabIndex = 1465
        '
        'rptdailydispatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptdailydispatch"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptdailydispatch"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbllocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txttodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As UserControls.MyRadGridView
    Friend WithEvents lblRouteNo As Controls.MyLabel
    Friend WithEvents lblcustomer As Controls.MyLabel
    Friend WithEvents RadLabel15 As Controls.MyLabel
    Friend WithEvents lbllocation As Controls.MyLabel
    Friend WithEvents RadLabel2 As Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents txttodate As Controls.MyDateTimePicker
    Friend WithEvents txtfromdate As Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As Controls.MyLabel
    Friend WithEvents MyLabel2 As Controls.MyLabel
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnreset As RadButton
    Friend WithEvents RadPanel2 As RadPanel
    Friend WithEvents btngo As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents txtlocation As UserControls.txtFinder
    Friend WithEvents txtcustomer As UserControls.txtFinder
    Friend WithEvents RadGroupBox7 As RadGroupBox
    Friend WithEvents rbtnMilk As RadioButton
    Friend WithEvents rbtnproduct As RadioButton
    Friend WithEvents txtrouteno As UserControls.txtMultiSelectFinder
End Class
