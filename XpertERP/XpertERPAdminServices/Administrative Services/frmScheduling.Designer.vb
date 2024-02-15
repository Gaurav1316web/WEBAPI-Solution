<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmScheduling
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkTransaction = New Telerik.WinControls.UI.RadRadioButton
        Me.chkMaster = New Telerik.WinControls.UI.RadRadioButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.lblModule = New common.Controls.MyLabel
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.txtModule = New common.UserControls.txtFinder
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvLogin = New common.UserControls.MyRadGridView
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gvLogin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLogin.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblModule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtModule)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(794, 448)
        Me.SplitContainer1.SplitterDistance = 56
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkTransaction)
        Me.Panel1.Controls.Add(Me.chkMaster)
        Me.Panel1.Location = New System.Drawing.Point(224, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(400, 23)
        Me.Panel1.TabIndex = 3
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
        Me.btnReset.Location = New System.Drawing.Point(630, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(74, 22)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'lblModule
        '
        Me.lblModule.AutoSize = False
        Me.lblModule.BorderVisible = True
        Me.lblModule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblModule.Location = New System.Drawing.Point(224, 4)
        Me.lblModule.Name = "lblModule"
        Me.lblModule.Size = New System.Drawing.Size(400, 20)
        Me.lblModule.TabIndex = 1
        Me.lblModule.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.RadLabel6.Location = New System.Drawing.Point(12, 6)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel6.TabIndex = 31
        Me.RadLabel6.Text = "Module"
        '
        'txtModule
        '
        Me.txtModule.Location = New System.Drawing.Point(77, 5)
        Me.txtModule.MendatroryField = True
        Me.txtModule.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModule.MyLinkLable1 = Me.RadLabel6
        Me.txtModule.MyLinkLable2 = Me.lblModule
        Me.txtModule.MyReadOnly = False
        Me.txtModule.Name = "txtModule"
        Me.txtModule.Size = New System.Drawing.Size(143, 18)
        Me.txtModule.TabIndex = 0
        Me.txtModule.Value = ""
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Size = New System.Drawing.Size(794, 388)
        Me.SplitContainer2.SplitterDistance = 359
        Me.SplitContainer2.TabIndex = 8
        '
        'RadPageView2
        '
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView2.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView2.Name = "RadPageView2"
        Me.RadPageView2.SelectedPage = Me.RadPageViewPage5
        Me.RadPageView2.Size = New System.Drawing.Size(794, 359)
        Me.RadPageView2.TabIndex = 0
        Me.RadPageView2.Text = "RadPageView2"
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.gv)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(773, 311)
        Me.RadPageViewPage4.Text = "Screen"
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
        Me.gv.Size = New System.Drawing.Size(773, 311)
        Me.gv.TabIndex = 3
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gvLogin)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(773, 311)
        Me.RadPageViewPage5.Text = "Login"
        '
        'gvLogin
        '
        Me.gvLogin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLogin.Location = New System.Drawing.Point(0, 0)
        '
        'gvLogin
        '
        Me.gvLogin.MasterTemplate.AllowAddNewRow = False
        Me.gvLogin.MasterTemplate.AllowEditRow = False
        Me.gvLogin.MasterTemplate.EnableFiltering = True
        Me.gvLogin.Name = "gvLogin"
        Me.gvLogin.ShowGroupPanel = False
        Me.gvLogin.Size = New System.Drawing.Size(773, 311)
        Me.gvLogin.TabIndex = 0
        Me.gvLogin.TabStop = False
        Me.gvLogin.Text = "RadGridView1"
        '
        'RadButton1
        '
        Me.RadButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(82, 2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(69, 22)
        Me.RadButton1.TabIndex = 1
        Me.RadButton1.Text = "Export"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(722, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(74, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmScheduling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 448)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmScheduling"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Scheduling"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gvLogin.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLogin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblModule As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents txtModule As common.UserControls.txtFinder
    Friend WithEvents RadPageView2 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvLogin As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkTransaction As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkMaster As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
End Class

