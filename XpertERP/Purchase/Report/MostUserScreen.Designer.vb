<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MostUserScreen
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkDateRange = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lbltoDate = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyComboBox1 = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.Txt2 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtTopCount = New common.MyNumBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkDateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.lbltoDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTopCount, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(531, 315)
        Me.SplitContainer1.SplitterDistance = 282
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
        Me.RadPageView1.Size = New System.Drawing.Size(531, 282)
        Me.RadPageView1.TabIndex = 15
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtTopCount)
        Me.RadPageViewPage1.Controls.Add(Me.chkDateRange)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.MyComboBox1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.Txt2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(510, 234)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'chkDateRange
        '
        Me.chkDateRange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDateRange.Location = New System.Drawing.Point(238, 12)
        Me.chkDateRange.Name = "chkDateRange"
        Me.chkDateRange.Size = New System.Drawing.Size(121, 16)
        Me.chkDateRange.TabIndex = 1588
        Me.chkDateRange.Text = "Counter DateRange"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.lbltoDate)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(28, 66)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(350, 42)
        Me.RadGroupBox3.TabIndex = 440
        '
        'lbltoDate
        '
        Me.lbltoDate.FieldName = Nothing
        Me.lbltoDate.Location = New System.Drawing.Point(186, 12)
        Me.lbltoDate.Name = "lbltoDate"
        Me.lbltoDate.Size = New System.Drawing.Size(45, 18)
        Me.lbltoDate.TabIndex = 4
        Me.lbltoDate.Text = "To Date"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(236, 11)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(107, 20)
        Me.ToDate.TabIndex = 3
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
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
        'MyComboBox1
        '
        Me.MyComboBox1.AutoCompleteDisplayMember = Nothing
        Me.MyComboBox1.AutoCompleteValueMember = Nothing
        Me.MyComboBox1.CalculationExpression = Nothing
        Me.MyComboBox1.DropDownAnimationEnabled = True
        Me.MyComboBox1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.MyComboBox1.FieldCode = Nothing
        Me.MyComboBox1.FieldDesc = Nothing
        Me.MyComboBox1.FieldMaxLength = 0
        Me.MyComboBox1.FieldName = Nothing
        Me.MyComboBox1.isCalculatedField = False
        Me.MyComboBox1.IsSourceFromTable = False
        Me.MyComboBox1.IsSourceFromValueList = False
        Me.MyComboBox1.IsUnique = False
        RadListDataItem5.Text = "Setup"
        RadListDataItem6.Text = "Transaction"
        RadListDataItem7.Text = "Report"
        RadListDataItem8.Text = "All"
        Me.MyComboBox1.Items.Add(RadListDataItem5)
        Me.MyComboBox1.Items.Add(RadListDataItem6)
        Me.MyComboBox1.Items.Add(RadListDataItem7)
        Me.MyComboBox1.Items.Add(RadListDataItem8)
        Me.MyComboBox1.Location = New System.Drawing.Point(112, 40)
        Me.MyComboBox1.MendatroryField = False
        Me.MyComboBox1.MyLinkLable1 = Nothing
        Me.MyComboBox1.MyLinkLable2 = Nothing
        Me.MyComboBox1.Name = "MyComboBox1"
        Me.MyComboBox1.ReferenceFieldDesc = Nothing
        Me.MyComboBox1.ReferenceFieldName = Nothing
        Me.MyComboBox1.ReferenceTableName = Nothing
        Me.MyComboBox1.Size = New System.Drawing.Size(120, 20)
        Me.MyComboBox1.TabIndex = 439
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(28, 40)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel1.TabIndex = 437
        Me.MyLabel1.Text = "Screen Type"
        '
        'Txt2
        '
        Me.Txt2.FieldName = Nothing
        Me.Txt2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt2.Location = New System.Drawing.Point(28, 15)
        Me.Txt2.Name = "Txt2"
        Me.Txt2.Size = New System.Drawing.Size(27, 18)
        Me.Txt2.TabIndex = 436
        Me.Txt2.Text = "TOP"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(510, 234)
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
        Me.Gv1.Size = New System.Drawing.Size(510, 234)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(438, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 20)
        Me.btnClose.TabIndex = 171
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(84, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 20)
        Me.btnReset.TabIndex = 170
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(7, 5)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 20)
        Me.btnGo.TabIndex = 169
        Me.btnGo.Text = ">>>"
        '
        'txtTopCount
        '
        Me.txtTopCount.BackColor = System.Drawing.Color.Transparent
        Me.txtTopCount.CalculationExpression = Nothing
        Me.txtTopCount.DecimalPlaces = 2
        Me.txtTopCount.FieldCode = Nothing
        Me.txtTopCount.FieldDesc = Nothing
        Me.txtTopCount.FieldMaxLength = 0
        Me.txtTopCount.FieldName = Nothing
        Me.txtTopCount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtTopCount.isCalculatedField = False
        Me.txtTopCount.IsSourceFromTable = False
        Me.txtTopCount.IsSourceFromValueList = False
        Me.txtTopCount.IsUnique = False
        Me.txtTopCount.Location = New System.Drawing.Point(112, 13)
        Me.txtTopCount.MaxLength = 6
        Me.txtTopCount.MendatroryField = False
        Me.txtTopCount.MyLinkLable1 = Nothing
        Me.txtTopCount.MyLinkLable2 = Nothing
        Me.txtTopCount.Name = "txtTopCount"
        Me.txtTopCount.ReferenceFieldDesc = Nothing
        Me.txtTopCount.ReferenceFieldName = Nothing
        Me.txtTopCount.ReferenceTableName = Nothing
        Me.txtTopCount.Size = New System.Drawing.Size(120, 21)
        Me.txtTopCount.TabIndex = 1589
        Me.txtTopCount.Text = "0"
        Me.txtTopCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTopCount.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'MostUserScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 315)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "MostUserScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MostUserScreen"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkDateRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.lbltoDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTopCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents Txt2 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyComboBox1 As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents lbltoDate As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents chkDateRange As RadCheckBox
    Friend WithEvents txtTopCount As common.MyNumBox
End Class
