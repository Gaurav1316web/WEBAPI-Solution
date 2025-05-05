Imports XpertERPEngineFine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmShowCrystalReportAction
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnUpdate = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(152, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 21)
        Me.btnClose.TabIndex = 29
        Me.btnClose.Text = "Esc : Cancel"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 263)
        Me.Splitter1.TabIndex = 31
        Me.Splitter1.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Gv1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(298, 263)
        Me.SplitContainer1.SplitterDistance = 227
        Me.SplitContainer1.TabIndex = 32
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
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(298, 227)
        Me.Gv1.TabIndex = 1
        Me.Gv1.VarID = ""
        '
        'btnUpdate
        '
        Me.btnUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(67, 5)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(83, 21)
        Me.btnUpdate.TabIndex = 30
        Me.btnUpdate.Text = "F5 : Update"
        '
        'frmShowCrystalReportAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 263)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Splitter1)
        Me.MaximumSize = New System.Drawing.Size(309, 293)
        Me.MinimumSize = New System.Drawing.Size(309, 293)
        Me.Name = "frmShowCrystalReportAction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(309, 293)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Show Crystal Report Action Type"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnClose As RadButton
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Gv1 As UserControls.MyRadGridView
    Friend WithEvents btnUpdate As RadButton
End Class
