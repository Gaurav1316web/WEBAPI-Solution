<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmJEReverse
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.lblMonthYear = New Telerik.WinControls.UI.RadLabel
        Me.dtpMonthYear = New Telerik.WinControls.UI.RadDateTimePicker
        Me.gvVouchers = New common.UserControls.MyRadGridView
        Me.btnReverse = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.lblRecordCounter = New Telerik.WinControls.UI.RadLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMonthYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVouchers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvVouchers.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRecordCounter, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblRecordCounter)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(809, 385)
        Me.SplitContainer1.SplitterDistance = 356
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMonthYear)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpMonthYear)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvVouchers)
        Me.SplitContainer2.Size = New System.Drawing.Size(809, 356)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 0
        '
        'lblMonthYear
        '
        Me.lblMonthYear.Location = New System.Drawing.Point(9, 3)
        Me.lblMonthYear.Name = "lblMonthYear"
        Me.lblMonthYear.Size = New System.Drawing.Size(65, 18)
        Me.lblMonthYear.TabIndex = 1
        Me.lblMonthYear.Text = "Month Year"
        '
        'dtpMonthYear
        '
        Me.dtpMonthYear.CustomFormat = "MMMM/yyyy"
        Me.dtpMonthYear.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpMonthYear.Location = New System.Drawing.Point(92, 2)
        Me.dtpMonthYear.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpMonthYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonthYear.Name = "dtpMonthYear"
        Me.dtpMonthYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonthYear.Size = New System.Drawing.Size(130, 20)
        Me.dtpMonthYear.TabIndex = 0
        Me.dtpMonthYear.Text = "RadDateTimePicker1"
        Me.dtpMonthYear.Value = New Date(2013, 5, 20, 15, 24, 41, 41)
        '
        'gvVouchers
        '
        Me.gvVouchers.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvVouchers.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvVouchers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvVouchers.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvVouchers.ForeColor = System.Drawing.Color.Black
        Me.gvVouchers.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvVouchers.Location = New System.Drawing.Point(0, 0)
        '
        'gvVouchers
        '
        Me.gvVouchers.MasterTemplate.AllowDeleteRow = False
        Me.gvVouchers.MasterTemplate.EnableFiltering = True
        Me.gvVouchers.Name = "gvVouchers"
        Me.gvVouchers.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.gvVouchers.RootElement.ForeColor = System.Drawing.Color.Black
        Me.gvVouchers.ShowGroupPanel = False
        Me.gvVouchers.Size = New System.Drawing.Size(809, 327)
        Me.gvVouchers.TabIndex = 1
        Me.gvVouchers.Text = "RadGridView1"
        '
        'btnReverse
        '
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(6, 1)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(69, 22)
        Me.btnReverse.TabIndex = 3
        Me.btnReverse.Text = "Reverse"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(734, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'lblRecordCounter
        '
        Me.lblRecordCounter.Location = New System.Drawing.Point(627, 3)
        Me.lblRecordCounter.Name = "lblRecordCounter"
        Me.lblRecordCounter.Size = New System.Drawing.Size(79, 18)
        Me.lblRecordCounter.TabIndex = 5
        Me.lblRecordCounter.Text = "Records found"
        '
        'FrmJEReverse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(809, 385)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmJEReverse"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Reverse Journal Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMonthYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVouchers.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvVouchers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRecordCounter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblMonthYear As Telerik.WinControls.UI.RadLabel
    Friend WithEvents dtpMonthYear As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents gvVouchers As common.UserControls.MyRadGridView
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblRecordCounter As Telerik.WinControls.UI.RadLabel
End Class

