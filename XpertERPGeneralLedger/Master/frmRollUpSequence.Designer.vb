<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRollUpSequence
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
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.dgvAccountCode = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.txtAccGroupDesc = New Telerik.WinControls.UI.RadTextBox
        Me.txtAccGroupCode = New Telerik.WinControls.UI.RadTextBox
        Me.RadLabel5 = New common.Controls.MyLabel
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountCode.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtAccGroupDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccGroupCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.btnsave)
        Me.RadPanel1.Controls.Add(Me.btnClose)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 386)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(696, 24)
        Me.RadPanel1.TabIndex = 3
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnsave.Location = New System.Drawing.Point(263, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.Location = New System.Drawing.Point(334, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'dgvAccountCode
        '
        Me.dgvAccountCode.BackColor = System.Drawing.Color.White
        Me.dgvAccountCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccountCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvAccountCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvAccountCode.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvAccountCode.Location = New System.Drawing.Point(0, 0)
        '
        'dgvAccountCode
        '
        Me.dgvAccountCode.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvAccountCode.MasterTemplate.AllowAddNewRow = False
        Me.dgvAccountCode.MasterTemplate.AllowDeleteRow = False
        Me.dgvAccountCode.MasterTemplate.EnableGrouping = False
        Me.dgvAccountCode.Name = "dgvAccountCode"
        Me.dgvAccountCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvAccountCode.Size = New System.Drawing.Size(696, 354)
        Me.dgvAccountCode.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAccGroupDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAccGroupCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel5)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvAccountCode)
        Me.SplitContainer1.Size = New System.Drawing.Size(696, 386)
        Me.SplitContainer1.SplitterDistance = 28
        Me.SplitContainer1.TabIndex = 4
        '
        'txtAccGroupDesc
        '
        Me.txtAccGroupDesc.Location = New System.Drawing.Point(202, 4)
        Me.txtAccGroupDesc.Name = "txtAccGroupDesc"
        Me.txtAccGroupDesc.Size = New System.Drawing.Size(397, 20)
        Me.txtAccGroupDesc.TabIndex = 87
        '
        'txtAccGroupCode
        '
        Me.txtAccGroupCode.Location = New System.Drawing.Point(96, 4)
        Me.txtAccGroupCode.Name = "txtAccGroupCode"
        Me.txtAccGroupCode.Size = New System.Drawing.Size(100, 20)
        Me.txtAccGroupCode.TabIndex = 86
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(12, 5)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(77, 18)
        Me.RadLabel5.TabIndex = 85
        Me.RadLabel5.Text = "Account Code"
        '
        'FrmRollUpSequence
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(696, 410)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Name = "FrmRollUpSequence"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rollup Sequence"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountCode.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtAccGroupDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccGroupCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvAccountCode As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents txtAccGroupDesc As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtAccGroupCode As Telerik.WinControls.UI.RadTextBox
End Class

