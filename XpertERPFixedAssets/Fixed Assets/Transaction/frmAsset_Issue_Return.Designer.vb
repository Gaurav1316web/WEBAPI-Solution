Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAsset_Issue_Return
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
        Me.components = New System.ComponentModel.Container
        Me.chkReturn = New Telerik.WinControls.UI.RadRadioButton
        Me.chkIssue = New Telerik.WinControls.UI.RadRadioButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.lblFromEntity = New common.Controls.MyLabel
        Me.txtFromEntity = New common.UserControls.txtFinder
        Me.dgvVisi = New common.UserControls.MyRadGridView
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.chkReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkReturn
        '
        Me.chkReturn.Location = New System.Drawing.Point(75, 7)
        Me.chkReturn.Name = "chkReturn"
        Me.chkReturn.Size = New System.Drawing.Size(53, 18)
        Me.chkReturn.TabIndex = 1
        Me.chkReturn.Text = "Return"
        '
        'chkIssue
        '
        Me.chkIssue.Location = New System.Drawing.Point(9, 7)
        Me.chkIssue.Name = "chkIssue"
        Me.chkIssue.Size = New System.Drawing.Size(45, 18)
        Me.chkIssue.TabIndex = 0
        Me.chkIssue.Text = "Issue"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(784, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkReturn)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkIssue)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvVisi)
        Me.SplitContainer2.Size = New System.Drawing.Size(871, 412)
        Me.SplitContainer2.SplitterDistance = 74
        Me.SplitContainer2.TabIndex = 90
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(154, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(80, 18)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadLabel1)
        Me.GroupBox1.Controls.Add(Me.lblFromEntity)
        Me.GroupBox1.Controls.Add(Me.txtFromEntity)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(747, 43)
        Me.GroupBox1.TabIndex = 96
        Me.GroupBox1.TabStop = False
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(22, 19)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "From"
        '
        'lblFromEntity
        '
        Me.lblFromEntity.AutoSize = False
        Me.lblFromEntity.BorderVisible = True
        Me.lblFromEntity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromEntity.Location = New System.Drawing.Point(204, 19)
        Me.lblFromEntity.Name = "lblFromEntity"
        Me.lblFromEntity.Size = New System.Drawing.Size(538, 18)
        Me.lblFromEntity.TabIndex = 11
        Me.lblFromEntity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromEntity.TextWrap = False
        '
        'txtFromEntity
        '
        Me.txtFromEntity.Location = New System.Drawing.Point(78, 19)
        Me.txtFromEntity.MendatroryField = True
        Me.txtFromEntity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromEntity.MyLinkLable1 = Nothing
        Me.txtFromEntity.MyLinkLable2 = Me.lblFromEntity
        Me.txtFromEntity.MyReadOnly = False
        Me.txtFromEntity.Name = "txtFromEntity"
        Me.txtFromEntity.Size = New System.Drawing.Size(121, 18)
        Me.txtFromEntity.TabIndex = 0
        Me.txtFromEntity.Value = ""
        '
        'dgvVisi
        '
        Me.dgvVisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.dgvVisi.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvVisi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvVisi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvVisi.ForeColor = System.Drawing.Color.Black
        Me.dgvVisi.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvVisi.Location = New System.Drawing.Point(0, 0)
        '
        'dgvVisi
        '
        Me.dgvVisi.MasterTemplate.AllowDeleteRow = False
        Me.dgvVisi.Name = "dgvVisi"
        Me.dgvVisi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvVisi.ShowGroupPanel = False
        Me.dgvVisi.Size = New System.Drawing.Size(871, 334)
        Me.dgvVisi.TabIndex = 0
        Me.dgvVisi.Text = "RadGridView1"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(89, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(871, 441)
        Me.SplitContainer1.SplitterDistance = 412
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmAsset_Issue_Return
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(871, 441)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAsset_Issue_Return"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmAsset_Issue_Return"
        CType(Me.chkReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkReturn As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkIssue As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvVisi As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFromEntity As common.Controls.MyLabel
    Friend WithEvents txtFromEntity As common.UserControls.txtFinder
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
End Class

