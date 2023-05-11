<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreen
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

    'Friend WithEvents lblSumModuleName As common.Controls.MyLabel
    'Friend WithEvents lblModuleName As common.Controls.MyLabel
    'Friend WithEvents findModule As common.UserControls.txtFinder
    'Friend WithEvents lblModule As common.Controls.MyLabel
    ''Friend WithEvents txtDescription As common.Controls.MyTextBox
    ''Friend WithEvents lblRemarks As common.Controls.MyLabel
    'Friend WithEvents lblFormName As common.Controls.MyLabel
    'Friend WithEvents lblSubModule As common.Controls.MyLabel
    'Friend WithEvents txtSubModule As common.UserControls.txtFinder
    ''Friend WithEvents txtCode As common.UserControls.txtNavigator
    ''Friend WithEvents lblCode As common.Controls.MyLabel
    'Friend WithEvents gvLabelSetting As common.UserControls.MyRadGridView
    ''Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    ''Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    ''Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    ''Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton

    Private Sub InitializeComponent()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gvModule = New common.MyCheckBoxGrid()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox3.Size = New System.Drawing.Size(571, 460)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvModule)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(551, 430)
        Me.SplitContainer1.SplitterDistance = 384
        Me.SplitContainer1.TabIndex = 0
        '
        'gvModule
        '
        Me.gvModule.CheckedValue = Nothing
        Me.gvModule.DataSource = Nothing
        Me.gvModule.DisplayMember = "Name"
        Me.gvModule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvModule.Location = New System.Drawing.Point(0, 0)
        Me.gvModule.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.gvModule.MyShowHeadrText = False
        Me.gvModule.Name = "gvModule"
        Me.gvModule.Size = New System.Drawing.Size(551, 384)
        Me.gvModule.TabIndex = 3
        Me.gvModule.ValueMember = "Code"
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
        Me.btnclose.Location = New System.Drawing.Point(476, 15)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'frmScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(571, 460)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Select Screen to Remove from Menu"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
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
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents gvModule As common.MyCheckBoxGrid
End Class
