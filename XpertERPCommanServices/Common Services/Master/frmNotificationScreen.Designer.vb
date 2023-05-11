<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNotificationScreen
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkTransaction = New Telerik.WinControls.UI.RadRadioButton
        Me.chkMaster = New Telerik.WinControls.UI.RadRadioButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.lblModule = New common.Controls.MyLabel
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.txtModule = New common.UserControls.txtFinder
        Me.gv = New common.UserControls.MyRadGridView
        Me.Groupbox_criteria = New Telerik.WinControls.UI.RadGroupBox
        Me.tnclose_criteria = New Telerik.WinControls.UI.RadButton
        Me.btnsave_criteria = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnexit = New Telerik.WinControls.UI.RadButton
        Me.btnsave_user = New Telerik.WinControls.UI.RadButton
        Me.gv_user = New common.UserControls.MyRadGridView
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.dg_criteria = New common.UserControls.MyRadGridView
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv.SuspendLayout()
        CType(Me.Groupbox_criteria, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Groupbox_criteria.SuspendLayout()
        CType(Me.tnclose_criteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave_criteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnexit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave_user, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_user, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_user.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dg_criteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dg_criteria.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(784, 373)
        Me.SplitContainer1.SplitterDistance = 338
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblModule)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtModule)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(778, 332)
        Me.SplitContainer2.SplitterDistance = 60
        Me.SplitContainer2.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkTransaction)
        Me.Panel1.Controls.Add(Me.chkMaster)
        Me.Panel1.Location = New System.Drawing.Point(221, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(400, 23)
        Me.Panel1.TabIndex = 41
        '
        'chkTransaction
        '
        Me.chkTransaction.Location = New System.Drawing.Point(237, 2)
        Me.chkTransaction.Name = "chkTransaction"
        Me.chkTransaction.Size = New System.Drawing.Size(82, 18)
        Me.chkTransaction.TabIndex = 1
        Me.chkTransaction.Text = "Transactions"
        '
        'chkMaster
        '
        Me.chkMaster.Location = New System.Drawing.Point(72, 2)
        Me.chkMaster.Name = "chkMaster"
        Me.chkMaster.Size = New System.Drawing.Size(59, 18)
        Me.chkMaster.TabIndex = 0
        Me.chkMaster.Text = "Masters"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(627, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(73, 20)
        Me.btnReset.TabIndex = 40
        Me.btnReset.Text = "Reset"
        '
        'lblModule
        '
        Me.lblModule.AutoSize = False
        Me.lblModule.BorderVisible = True
        Me.lblModule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblModule.Location = New System.Drawing.Point(221, 7)
        Me.lblModule.Name = "lblModule"
        Me.lblModule.Size = New System.Drawing.Size(400, 20)
        Me.lblModule.TabIndex = 39
        Me.lblModule.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel6.Location = New System.Drawing.Point(23, 9)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel6.TabIndex = 38
        Me.RadLabel6.Text = "Module"
        '
        'txtModule
        '
        Me.txtModule.Location = New System.Drawing.Point(74, 8)
        Me.txtModule.MendatroryField = True
        Me.txtModule.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModule.MyLinkLable1 = Me.RadLabel6
        Me.txtModule.MyLinkLable2 = Me.lblModule
        Me.txtModule.MyReadOnly = False
        Me.txtModule.Name = "txtModule"
        Me.txtModule.Size = New System.Drawing.Size(143, 18)
        Me.txtModule.TabIndex = 37
        Me.txtModule.Value = ""
        '
        'gv
        '
        Me.gv.Controls.Add(Me.Groupbox_criteria)
        Me.gv.Controls.Add(Me.RadGroupBox1)
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(3, 3)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(772, 262)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'Groupbox_criteria
        '
        Me.Groupbox_criteria.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Groupbox_criteria.Controls.Add(Me.dg_criteria)
        Me.Groupbox_criteria.Controls.Add(Me.tnclose_criteria)
        Me.Groupbox_criteria.Controls.Add(Me.btnsave_criteria)
        Me.Groupbox_criteria.HeaderText = "User Detail"
        Me.Groupbox_criteria.Location = New System.Drawing.Point(228, 14)
        Me.Groupbox_criteria.Name = "Groupbox_criteria"
        Me.Groupbox_criteria.Size = New System.Drawing.Size(258, 202)
        Me.Groupbox_criteria.TabIndex = 5
        Me.Groupbox_criteria.Text = "User Detail"
        Me.Groupbox_criteria.Visible = False
        '
        'tnclose_criteria
        '
        Me.tnclose_criteria.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tnclose_criteria.Location = New System.Drawing.Point(175, 179)
        Me.tnclose_criteria.Name = "tnclose_criteria"
        Me.tnclose_criteria.Size = New System.Drawing.Size(77, 20)
        Me.tnclose_criteria.TabIndex = 4
        Me.tnclose_criteria.Text = "E&xit"
        '
        'btnsave_criteria
        '
        Me.btnsave_criteria.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave_criteria.Location = New System.Drawing.Point(7, 179)
        Me.btnsave_criteria.Name = "btnsave_criteria"
        Me.btnsave_criteria.Size = New System.Drawing.Size(77, 20)
        Me.btnsave_criteria.TabIndex = 3
        Me.btnsave_criteria.Text = "S&ave"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnexit)
        Me.RadGroupBox1.Controls.Add(Me.btnsave_user)
        Me.RadGroupBox1.Controls.Add(Me.gv_user)
        Me.RadGroupBox1.HeaderText = "User Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(176, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(362, 243)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "User Detail"
        Me.RadGroupBox1.Visible = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.Location = New System.Drawing.Point(279, 220)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(77, 20)
        Me.btnexit.TabIndex = 4
        Me.btnexit.Text = "E&xit"
        '
        'btnsave_user
        '
        Me.btnsave_user.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave_user.Location = New System.Drawing.Point(5, 220)
        Me.btnsave_user.Name = "btnsave_user"
        Me.btnsave_user.Size = New System.Drawing.Size(77, 20)
        Me.btnsave_user.TabIndex = 3
        Me.btnsave_user.Text = "S&ave"
        '
        'gv_user
        '
        Me.gv_user.Location = New System.Drawing.Point(5, 17)
        '
        'gv_user
        '
        Me.gv_user.MasterTemplate.AllowDragToGroup = False
        Me.gv_user.MasterTemplate.EnableGrouping = False
        Me.gv_user.Name = "gv_user"
        Me.gv_user.Size = New System.Drawing.Size(351, 197)
        Me.gv_user.TabIndex = 0
        Me.gv_user.Text = "RadGridView1"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(89, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(77, 20)
        Me.btndelete.TabIndex = 3
        Me.btndelete.Text = "&Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(696, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(77, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "&Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(6, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(77, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "&Save"
        '
        'dg_criteria
        '
        Me.dg_criteria.Location = New System.Drawing.Point(5, 23)
        '
        'dg_criteria
        '
        Me.dg_criteria.MasterTemplate.AllowDeleteRow = False
        Me.dg_criteria.MasterTemplate.AllowDragToGroup = False
        Me.dg_criteria.MasterTemplate.EnableGrouping = False
        Me.dg_criteria.Name = "dg_criteria"
        Me.dg_criteria.ShowGroupPanel = False
        Me.dg_criteria.Size = New System.Drawing.Size(246, 150)
        Me.dg_criteria.TabIndex = 5
        Me.dg_criteria.Text = "RadGridView1"
        '
        'FrmNotificationScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 373)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmNotificationScreen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmNotificationScreen"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv.ResumeLayout(False)
        CType(Me.Groupbox_criteria, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Groupbox_criteria.ResumeLayout(False)
        CType(Me.tnclose_criteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave_criteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.btnexit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave_user, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_user.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_user, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dg_criteria.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dg_criteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkTransaction As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkMaster As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblModule As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtModule As common.UserControls.txtFinder
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_user As common.UserControls.MyRadGridView
    Friend WithEvents btnsave_user As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnexit As Telerik.WinControls.UI.RadButton
    Friend WithEvents Groupbox_criteria As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents tnclose_criteria As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave_criteria As Telerik.WinControls.UI.RadButton
    Friend WithEvents dg_criteria As common.UserControls.MyRadGridView
End Class

