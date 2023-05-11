<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMandatoryFieldChecker
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
    '<System.Diagnostics.DebuggerStepThrough()> _

    'Friend WithEvents txtDescription As common.Controls.MyTextBox
    'Friend WithEvents lblRemarks As common.Controls.MyLabel
    'Friend WithEvents txtCode As common.UserControls.txtNavigator
    'Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents gvLabelSetting As common.UserControls.MyRadGridView
    'Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    'Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    'Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    'Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton

    Private Sub InitializeComponent()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.gvLabelSetting = New common.UserControls.MyRadGridView
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gvLabelSetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLabelSetting.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(678, 459)
        Me.RadGroupBox3.TabIndex = 64
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvLabelSetting)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(658, 429)
        Me.SplitContainer1.SplitterDistance = 383
        Me.SplitContainer1.TabIndex = 0
        '
        'gvLabelSetting
        '
        Me.gvLabelSetting.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvLabelSetting.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvLabelSetting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLabelSetting.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvLabelSetting.ForeColor = System.Drawing.Color.Black
        Me.gvLabelSetting.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvLabelSetting.Location = New System.Drawing.Point(0, 0)
        '
        'gvLabelSetting
        '
        Me.gvLabelSetting.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvLabelSetting.MasterTemplate.AutoGenerateColumns = False
        Me.gvLabelSetting.MasterTemplate.EnableGrouping = False
        Me.gvLabelSetting.Name = "gvLabelSetting"
        Me.gvLabelSetting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvLabelSetting.Size = New System.Drawing.Size(658, 383)
        Me.gvLabelSetting.TabIndex = 8
        Me.gvLabelSetting.TabStop = False
        Me.gvLabelSetting.Text = "RadGridView4"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 15)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(583, 15)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(87, 15)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmClientFormLableDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(678, 459)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmClientFormLableDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmLableChanging"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gvLabelSetting.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLabelSetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    'Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    'Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    'Friend WithEvents UsLock1 As common.usLock
    'Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton

    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblReimbursementDate As common.Controls.MyLabel
    Friend WithEvents dtpReimbursementDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents gvReimbursement As common.UserControls.MyRadGridView
    Friend WithEvents lblPayPeriodCode As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblEmpName As common.Controls.MyLabel
End Class
