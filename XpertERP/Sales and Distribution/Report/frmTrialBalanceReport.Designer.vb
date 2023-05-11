<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrialBalanceReport
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
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.grdTrial = New common.UserControls.MyRadGridView
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.dtpFdate = New common.Controls.MyDateTimePicker
        Me.DtpTodate = New common.Controls.MyDateTimePicker
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTrial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTrial.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel2
        '
        Me.RadLabel2.BackColor = System.Drawing.Color.Transparent
        Me.RadLabel2.Location = New System.Drawing.Point(12, 12)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel2.TabIndex = 52
        Me.RadLabel2.Text = "From Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.BackColor = System.Drawing.Color.Transparent
        Me.RadLabel1.Location = New System.Drawing.Point(220, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel1.TabIndex = 52
        Me.RadLabel1.Text = "To Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnprint)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.grdTrial)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(589, 333)
        Me.GroupBox1.TabIndex = 89
        Me.GroupBox1.Text = "Segment Details"
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(6, 305)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(69, 22)
        Me.btnprint.TabIndex = 1
        Me.btnprint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(508, 306)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'grdTrial
        '
        Me.grdTrial.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdTrial.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdTrial.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdTrial.ForeColor = System.Drawing.Color.Black
        Me.grdTrial.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdTrial.Location = New System.Drawing.Point(6, 21)
        '
        'grdTrial
        '
        Me.grdTrial.MasterTemplate.AllowAddNewRow = False
        GridViewComboBoxColumn1.DisplayMember = Nothing
        GridViewComboBoxColumn1.HeaderText = "Segment Name"
        GridViewComboBoxColumn1.MinWidth = 0
        GridViewComboBoxColumn1.Name = "Name"
        GridViewComboBoxColumn1.ValueMember = Nothing
        GridViewComboBoxColumn1.Width = 150
        GridViewTextBoxColumn1.HeaderText = "Segment Code"
        GridViewTextBoxColumn1.Name = "Code"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 150
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.Name = "Description"
        GridViewTextBoxColumn2.Width = 250
        Me.grdTrial.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewComboBoxColumn1, GridViewTextBoxColumn1, GridViewTextBoxColumn2})
        Me.grdTrial.Name = "grdTrial"
        Me.grdTrial.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.grdTrial.RootElement.ForeColor = System.Drawing.Color.Black
        Me.grdTrial.Size = New System.Drawing.Size(571, 279)
        Me.grdTrial.TabIndex = 1
        Me.grdTrial.Text = "RadGridView1"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(-292, 365)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "&Save"
        '
        'dtpFdate
        '
        Me.dtpFdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFdate.Location = New System.Drawing.Point(77, 12)
        Me.dtpFdate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpFdate.MendatroryField = False
        Me.dtpFdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.MyLinkLable1 = Nothing
        Me.dtpFdate.MyLinkLable2 = Nothing
        Me.dtpFdate.Name = "dtpFdate"
        Me.dtpFdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.Size = New System.Drawing.Size(128, 18)
        Me.dtpFdate.TabIndex = 94
        Me.dtpFdate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'DtpTodate
        '
        Me.DtpTodate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTodate.Location = New System.Drawing.Point(281, 12)
        Me.DtpTodate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DtpTodate.MendatroryField = False
        Me.DtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.MyLinkLable1 = Nothing
        Me.DtpTodate.MyLinkLable2 = Nothing
        Me.DtpTodate.Name = "DtpTodate"
        Me.DtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.Size = New System.Drawing.Size(128, 18)
        Me.DtpTodate.TabIndex = 95
        Me.DtpTodate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'frmTrialBalanceReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 395)
        Me.Controls.Add(Me.DtpTodate)
        Me.Controls.Add(Me.dtpFdate)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.RadLabel2)
        Me.KeyPreview = True
        Me.Name = "frmTrialBalanceReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Trial Balance Report"
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTrial.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTrial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdTrial As common.UserControls.MyRadGridView
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpFdate As common.Controls.MyDateTimePicker
    Friend WithEvents DtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
End Class

