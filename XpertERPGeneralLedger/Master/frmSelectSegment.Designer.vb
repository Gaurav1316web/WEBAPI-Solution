<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSelectSegment
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.gbVehicle = New Telerik.WinControls.UI.RadGroupBox
        Me.cbg1 = New common.MyCheckBoxGrid
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gbVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVehicle.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbVehicle)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(507, 369)
        Me.SplitContainer1.SplitterDistance = 340
        Me.SplitContainer1.TabIndex = 0
        '
        'gbVehicle
        '
        Me.gbVehicle.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVehicle.Controls.Add(Me.cbg1)
        Me.gbVehicle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbVehicle.HeaderText = "Select Segment"
        Me.gbVehicle.Location = New System.Drawing.Point(0, 0)
        Me.gbVehicle.Name = "gbVehicle"
        Me.gbVehicle.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbVehicle.Size = New System.Drawing.Size(507, 340)
        Me.gbVehicle.TabIndex = 74
        Me.gbVehicle.Text = "Select Segment"
        '
        'cbg1
        '
        Me.cbg1.CheckedValue = Nothing
        Me.cbg1.DataSource = Nothing
        Me.cbg1.DisplayMember = "Name"
        Me.cbg1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbg1.Location = New System.Drawing.Point(10, 20)
        Me.cbg1.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbg1.MyShowHeadrText = False
        Me.cbg1.Name = "cbg1"
        Me.cbg1.Size = New System.Drawing.Size(487, 310)
        Me.cbg1.TabIndex = 1
        Me.cbg1.ValueMember = "Code"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(254, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Cancel"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(180, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 1
        Me.btnsave.Text = "Ok"
        '
        'FrmSelectSegment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 369)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSelectSegment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Segment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gbVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVehicle.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gbVehicle As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbg1 As common.MyCheckBoxGrid
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
End Class

