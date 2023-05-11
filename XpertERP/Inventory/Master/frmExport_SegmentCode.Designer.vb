<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExport_SegmentCode
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnCreate = New Telerik.WinControls.UI.RadButton
        Me.dgvFilterColumn = New common.UserControls.MyRadGridView
        Me.grrpbxSetcriteria = New Telerik.WinControls.UI.RadGroupBox
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnAdd = New Telerik.WinControls.UI.RadButton
        Me.chkListOfColumn = New System.Windows.Forms.CheckedListBox
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFilterColumn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFilterColumn.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grrpbxSetcriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grrpbxSetcriteria.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(343, 366)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(70, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(14, 363)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(70, 24)
        Me.btnCreate.TabIndex = 3
        Me.btnCreate.Text = "Export"
        '
        'dgvFilterColumn
        '
        Me.dgvFilterColumn.AccessibleName = "dgvFilterColumn"
        Me.dgvFilterColumn.BackColor = System.Drawing.SystemColors.Control
        Me.dgvFilterColumn.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvFilterColumn.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvFilterColumn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvFilterColumn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvFilterColumn.Location = New System.Drawing.Point(14, 180)
        '
        'dgvFilterColumn
        '
        Me.dgvFilterColumn.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvFilterColumn.MasterTemplate.EnableGrouping = False
        Me.dgvFilterColumn.Name = "dgvFilterColumn"
        Me.dgvFilterColumn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvFilterColumn.Size = New System.Drawing.Size(399, 174)
        Me.dgvFilterColumn.TabIndex = 3
        Me.dgvFilterColumn.TabStop = False
        '
        'grrpbxSetcriteria
        '
        Me.grrpbxSetcriteria.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grrpbxSetcriteria.Controls.Add(Me.btnClose)
        Me.grrpbxSetcriteria.Controls.Add(Me.btnCreate)
        Me.grrpbxSetcriteria.Controls.Add(Me.dgvFilterColumn)
        Me.grrpbxSetcriteria.Controls.Add(Me.btnDelete)
        Me.grrpbxSetcriteria.Controls.Add(Me.btnAdd)
        Me.grrpbxSetcriteria.Controls.Add(Me.chkListOfColumn)
        Me.grrpbxSetcriteria.HeaderText = ""
        Me.grrpbxSetcriteria.Location = New System.Drawing.Point(12, 12)
        Me.grrpbxSetcriteria.Name = "grrpbxSetcriteria"
        Me.grrpbxSetcriteria.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grrpbxSetcriteria.Size = New System.Drawing.Size(430, 397)
        Me.grrpbxSetcriteria.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(343, 51)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(70, 24)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(233, 51)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 24)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Add"
        '
        'chkListOfColumn
        '
        Me.chkListOfColumn.FormattingEnabled = True
        Me.chkListOfColumn.Location = New System.Drawing.Point(14, 12)
        Me.chkListOfColumn.Name = "chkListOfColumn"
        Me.chkListOfColumn.Size = New System.Drawing.Size(175, 157)
        Me.chkListOfColumn.TabIndex = 0
        '
        'FrmExport_SegmentCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 418)
        Me.Controls.Add(Me.grrpbxSetcriteria)
        Me.Name = "FrmExport_SegmentCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Export Segment Code"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFilterColumn.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFilterColumn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grrpbxSetcriteria, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grrpbxSetcriteria.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCreate As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvFilterColumn As common.UserControls.MyRadGridView
    Friend WithEvents grrpbxSetcriteria As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkListOfColumn As System.Windows.Forms.CheckedListBox
End Class

