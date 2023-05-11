<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserLog
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
        Me.gvTemp = New common.UserControls.MyRadGridView
        Me.gv = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadButton
        Me.btnSelect = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gvTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTemp.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gvTemp.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvTemp)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(816, 474)
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 0
        '
        'gvTemp
        '
        Me.gvTemp.Controls.Add(Me.gv)
        Me.gvTemp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTemp.Location = New System.Drawing.Point(0, 0)
        '
        'gvTemp
        '
        Me.gvTemp.MasterTemplate.AllowAddNewRow = False
        Me.gvTemp.MasterTemplate.AllowEditRow = False
        Me.gvTemp.MasterTemplate.EnableFiltering = True
        Me.gvTemp.Name = "gvTemp"
        Me.gvTemp.ShowGroupPanel = False
        Me.gvTemp.Size = New System.Drawing.Size(816, 444)
        Me.gvTemp.TabIndex = 2
        Me.gvTemp.Text = "RadGridView1"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowEditRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(816, 444)
        Me.gv.TabIndex = 2
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.Location = New System.Drawing.Point(130, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(103, 22)
        Me.btnExport.TabIndex = 4
        Me.btnExport.Text = "Export To Excel"
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(0, 2)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(125, 22)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "Select All"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(744, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'FrmUserLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(816, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmUserLog"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "User Logs"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gvTemp.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gvTemp.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvTemp As common.UserControls.MyRadGridView
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

