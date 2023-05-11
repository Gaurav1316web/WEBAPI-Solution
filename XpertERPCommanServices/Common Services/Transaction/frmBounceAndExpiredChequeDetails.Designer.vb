<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBounceAndExpiredChequeDetails
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.dgv = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.AutoScroll = True
        Me.dgv.AutoSizeRows = True
        Me.dgv.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgv.ForeColor = System.Drawing.Color.Black
        Me.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv.Location = New System.Drawing.Point(6, 44)
        '
        'dgv
        '
        Me.dgv.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgv.MasterTemplate.AllowAddNewRow = False
        Me.dgv.MasterTemplate.AllowCellContextMenu = False
        Me.dgv.MasterTemplate.AllowColumnChooser = False
        Me.dgv.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.dgv.MasterTemplate.AllowColumnReorder = False
        Me.dgv.MasterTemplate.AllowColumnResize = False
        Me.dgv.MasterTemplate.AllowDeleteRow = False
        Me.dgv.MasterTemplate.AllowDragToGroup = False
        Me.dgv.MasterTemplate.AllowEditRow = False
        Me.dgv.MasterTemplate.AllowRowResize = False
        Me.dgv.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.dgv.MasterTemplate.EnableGrouping = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgv.Size = New System.Drawing.Size(840, 322)
        Me.dgv.TabIndex = 2
        Me.dgv.TabStop = False
        Me.dgv.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(777, 372)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(833, 33)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Attention !!! Your Following Bounced Cheque(s) is/are expired or about to expire"
        '
        'FrmBounceAndExpiredChequeDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(858, 394)
        Me.ControlBox = False
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.dgv)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBounceAndExpiredChequeDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bounced And Expired Cheque(s) Details"
        CType(Me.dgv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgv As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
End Class

