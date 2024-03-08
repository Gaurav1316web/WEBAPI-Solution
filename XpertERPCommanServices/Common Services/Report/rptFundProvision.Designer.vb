<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptFundProvision
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.ftrdate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1BnkDetail = New common.UserControls.MyRadGridView()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1UBD = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ftrdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv1BnkDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1BnkDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1UBD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1UBD.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ftrdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btngo)
        Me.SplitContainer1.Size = New System.Drawing.Size(756, 450)
        Me.SplitContainer1.SplitterDistance = 421
        Me.SplitContainer1.TabIndex = 0
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(37, 27)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel15.TabIndex = 44
        Me.RadLabel15.Text = "Date"
        '
        'ftrdate
        '
        Me.ftrdate.CustomFormat = "dd/MM/yyyy"
        Me.ftrdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ftrdate.Location = New System.Drawing.Point(73, 24)
        Me.ftrdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ftrdate.Name = "ftrdate"
        Me.ftrdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ftrdate.Size = New System.Drawing.Size(92, 20)
        Me.ftrdate.TabIndex = 43
        Me.ftrdate.TabStop = False
        Me.ftrdate.Text = "31/08/2011"
        Me.ftrdate.Value = New Date(2011, 8, 31, 23, 50, 36, 937)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gv1BnkDetail)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Bank Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 53)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(753, 164)
        Me.RadGroupBox1.TabIndex = 42
        Me.RadGroupBox1.Text = "Bank Detail"
        '
        'gv1BnkDetail
        '
        Me.gv1BnkDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1BnkDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1BnkDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1BnkDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1BnkDetail.ForeColor = System.Drawing.Color.Black
        Me.gv1BnkDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1BnkDetail.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1BnkDetail.MasterTemplate.AllowDeleteRow = False
        Me.gv1BnkDetail.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1BnkDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1BnkDetail.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1BnkDetail.MyStopExport = False
        Me.gv1BnkDetail.Name = "gv1BnkDetail"
        Me.gv1BnkDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1BnkDetail.ShowGroupPanel = False
        Me.gv1BnkDetail.ShowHeaderCellButtons = True
        Me.gv1BnkDetail.Size = New System.Drawing.Size(733, 134)
        Me.gv1BnkDetail.TabIndex = 0
        Me.gv1BnkDetail.TabStop = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1UBD)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Union Balance Detail"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 223)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(753, 195)
        Me.RadGroupBox2.TabIndex = 41
        Me.RadGroupBox2.Text = "Union Balance Detail"
        '
        'gv1UBD
        '
        Me.gv1UBD.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1UBD.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1UBD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1UBD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1UBD.ForeColor = System.Drawing.Color.Black
        Me.gv1UBD.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1UBD.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1UBD.MasterTemplate.AllowDeleteRow = False
        Me.gv1UBD.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1UBD.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1UBD.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1UBD.MyStopExport = False
        Me.gv1UBD.Name = "gv1UBD"
        Me.gv1UBD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1UBD.ShowGroupPanel = False
        Me.gv1UBD.ShowHeaderCellButtons = True
        Me.gv1UBD.Size = New System.Drawing.Size(733, 165)
        Me.gv1UBD.TabIndex = 0
        Me.gv1UBD.TabStop = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(88, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(77, 18)
        Me.btnPrint.TabIndex = 329
        Me.btnPrint.Text = "Print"
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngo.Location = New System.Drawing.Point(13, 4)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(73, 18)
        Me.btngo.TabIndex = 328
        Me.btngo.Text = ">>>"
        '
        'rptFundProvision
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(756, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptFundProvision"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptFundProvision"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ftrdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv1BnkDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1BnkDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1UBD.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1UBD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents gv1BnkDetail As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents gv1UBD As common.UserControls.MyRadGridView
    Friend WithEvents ftrdate As RadDateTimePicker
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btngo As RadButton
End Class
