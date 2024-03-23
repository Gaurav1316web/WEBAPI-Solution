<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMakeSavingPayment
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtBMC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtVSP = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtPaymentDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaymentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(896, 451)
        Me.SplitContainer1.SplitterDistance = 411
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPaymentDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBMC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVSP)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(896, 411)
        Me.SplitContainer2.SplitterDistance = 78
        Me.SplitContainer2.SplitterWidth = 3
        Me.SplitContainer2.TabIndex = 617
        '
        'txtBMC
        '
        Me.txtBMC.arrDispalyMember = Nothing
        Me.txtBMC.arrValueMember = Nothing
        Me.txtBMC.Location = New System.Drawing.Point(71, 28)
        Me.txtBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMC.MyLinkLable1 = Me.MyLabel1
        Me.txtBMC.MyLinkLable2 = Nothing
        Me.txtBMC.MyNullText = "Please Select..."
        Me.txtBMC.Name = "txtBMC"
        Me.txtBMC.Size = New System.Drawing.Size(402, 22)
        Me.txtBMC.TabIndex = 624
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 30)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel1.TabIndex = 625
        Me.MyLabel1.Text = "BMC"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(479, 28)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(69, 23)
        Me.RadButton1.TabIndex = 623
        Me.RadButton1.Text = ">>>"
        '
        'txtVSP
        '
        Me.txtVSP.arrDispalyMember = Nothing
        Me.txtVSP.arrValueMember = Nothing
        Me.txtVSP.Location = New System.Drawing.Point(71, 52)
        Me.txtVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSP.MyLinkLable1 = Me.MyLabel4
        Me.txtVSP.MyLinkLable2 = Nothing
        Me.txtVSP.MyNullText = "Please select..."
        Me.txtVSP.Name = "txtVSP"
        Me.txtVSP.Size = New System.Drawing.Size(402, 22)
        Me.txtVSP.TabIndex = 621
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(8, 51)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel4.TabIndex = 622
        Me.MyLabel4.Text = "DCS"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowFilteringRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(896, 330)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(815, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(8, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Process"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(896, 451)
        Me.Panel1.TabIndex = 3
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(162, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 629
        Me.MyLabel2.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(213, 5)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(86, 20)
        Me.txtToDate.TabIndex = 628
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(8, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 627
        Me.RadLabel1.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(71, 5)
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(86, 20)
        Me.txtFromDate.TabIndex = 626
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "24/10/2011"
        Me.txtFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadButton2
        '
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(479, 52)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(69, 23)
        Me.RadButton2.TabIndex = 624
        Me.RadButton2.Text = "Reset"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(305, 6)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(76, 18)
        Me.MyLabel3.TabIndex = 631
        Me.MyLabel3.Text = "Payment Date"
        '
        'txtPaymentDate
        '
        Me.txtPaymentDate.CustomFormat = "dd/MM/yyyy"
        Me.txtPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPaymentDate.Location = New System.Drawing.Point(387, 5)
        Me.txtPaymentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPaymentDate.Name = "txtPaymentDate"
        Me.txtPaymentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPaymentDate.Size = New System.Drawing.Size(86, 20)
        Me.txtPaymentDate.TabIndex = 630
        Me.txtPaymentDate.TabStop = False
        Me.txtPaymentDate.Text = "24/10/2011"
        Me.txtPaymentDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'frmMakeSavingPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 451)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(904, 481)
        Me.Name = "frmMakeSavingPayment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Make Saving Payment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaymentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents txtVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents txtBMC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToDate As RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As RadDateTimePicker
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtPaymentDate As RadDateTimePicker
End Class

