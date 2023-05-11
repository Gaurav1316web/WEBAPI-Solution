<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostCenter
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
        Me.txtdes = New common.Controls.MyTextBox
        Me.lbldes = New common.Controls.MyLabel
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lbldesid = New common.Controls.MyLabel
        Me.ToolTipdesig = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.gbdesignation = New Telerik.WinControls.UI.RadGroupBox
        Me.lblGLAcc = New common.Controls.MyLabel
        Me.txtGLAcc = New common.UserControls.txtFinder
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.lblCostDesp = New common.Controls.MyLabel
        Me.txtcostcenter = New common.UserControls.txtFinder
        Me.lblcostcenter = New common.Controls.MyLabel
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbdesignation.SuspendLayout()
        CType(Me.lblGLAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcostcenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtdes
        '
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.Location = New System.Drawing.Point(116, 36)
        Me.txtdes.MaxLength = 100
        Me.txtdes.MendatroryField = False
        Me.txtdes.MyLinkLable1 = Nothing
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(290, 20)
        Me.txtdes.TabIndex = 2
        '
        'lbldes
        '
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(9, 36)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 36
        Me.lbldes.Text = "Description"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(116, 11)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lbldesid
        '
        Me.lbldesid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesid.Location = New System.Drawing.Point(9, 13)
        Me.lbldesid.Name = "lbldesid"
        Me.lbldesid.Size = New System.Drawing.Size(63, 16)
        Me.lbldesid.TabIndex = 37
        Me.lbldesid.Text = "Cost  Code"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPPurchase.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(317, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'gbdesignation
        '
        Me.gbdesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbdesignation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbdesignation.Controls.Add(Me.lblGLAcc)
        Me.gbdesignation.Controls.Add(Me.txtGLAcc)
        Me.gbdesignation.Controls.Add(Me.MyLabel4)
        Me.gbdesignation.Controls.Add(Me.lblCostDesp)
        Me.gbdesignation.Controls.Add(Me.txtcostcenter)
        Me.gbdesignation.Controls.Add(Me.lblcostcenter)
        Me.gbdesignation.Controls.Add(Me.txtCode)
        Me.gbdesignation.Controls.Add(Me.lbldesid)
        Me.gbdesignation.Controls.Add(Me.txtdes)
        Me.gbdesignation.Controls.Add(Me.lbldes)
        Me.gbdesignation.Controls.Add(Me.btnnew)
        Me.gbdesignation.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbdesignation.HeaderText = ""
        Me.gbdesignation.Location = New System.Drawing.Point(6, 35)
        Me.gbdesignation.Name = "gbdesignation"
        Me.gbdesignation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbdesignation.Size = New System.Drawing.Size(654, 111)
        Me.gbdesignation.TabIndex = 0
        '
        'lblGLAcc
        '
        Me.lblGLAcc.AutoSize = False
        Me.lblGLAcc.BorderVisible = True
        Me.lblGLAcc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGLAcc.Location = New System.Drawing.Point(262, 82)
        Me.lblGLAcc.Name = "lblGLAcc"
        Me.lblGLAcc.Size = New System.Drawing.Size(386, 18)
        Me.lblGLAcc.TabIndex = 53
        Me.lblGLAcc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtGLAcc
        '
        Me.txtGLAcc.Location = New System.Drawing.Point(116, 82)
        Me.txtGLAcc.MendatroryField = False
        Me.txtGLAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAcc.MyLinkLable1 = Me.MyLabel4
        Me.txtGLAcc.MyLinkLable2 = Nothing
        Me.txtGLAcc.MyReadOnly = False
        Me.txtGLAcc.Name = "txtGLAcc"
        Me.txtGLAcc.Size = New System.Drawing.Size(141, 18)
        Me.txtGLAcc.TabIndex = 51
        Me.txtGLAcc.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(9, 83)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel4.TabIndex = 4
        Me.MyLabel4.Text = "GL Account"
        '
        'lblCostDesp
        '
        Me.lblCostDesp.AutoSize = False
        Me.lblCostDesp.BorderVisible = True
        Me.lblCostDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostDesp.Location = New System.Drawing.Point(262, 59)
        Me.lblCostDesp.Name = "lblCostDesp"
        Me.lblCostDesp.Size = New System.Drawing.Size(386, 18)
        Me.lblCostDesp.TabIndex = 40
        Me.lblCostDesp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcostcenter
        '
        Me.txtcostcenter.Location = New System.Drawing.Point(116, 59)
        Me.txtcostcenter.MendatroryField = False
        Me.txtcostcenter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcostcenter.MyLinkLable1 = Me.lblcostcenter
        Me.txtcostcenter.MyLinkLable2 = Nothing
        Me.txtcostcenter.MyReadOnly = False
        Me.txtcostcenter.Name = "txtcostcenter"
        Me.txtcostcenter.Size = New System.Drawing.Size(142, 19)
        Me.txtcostcenter.TabIndex = 3
        Me.txtcostcenter.Value = ""
        '
        'lblcostcenter
        '
        Me.lblcostcenter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcostcenter.Location = New System.Drawing.Point(9, 61)
        Me.lblcostcenter.Name = "lblcostcenter"
        Me.lblcostcenter.Size = New System.Drawing.Size(101, 16)
        Me.lblcostcenter.TabIndex = 38
        Me.lblcostcenter.Text = "Cost Center Group"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(14, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(93, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbdesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(666, 181)
        Me.SplitContainer1.SplitterDistance = 152
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(666, 20)
        Me.RadMenu1.TabIndex = 322
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        Me.rmImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "Export"
        Me.rmExport.AccessibleName = "Export"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        Me.rmExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(589, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'FrmCostCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 181)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCostCenter"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cost Center"
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbdesignation.ResumeLayout(False)
        Me.gbdesignation.PerformLayout()
        CType(Me.lblGLAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcostcenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lbldesid As common.Controls.MyLabel
    Friend WithEvents ToolTipdesig As System.Windows.Forms.ToolTip
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents gbdesignation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblCostDesp As common.Controls.MyLabel
    Friend WithEvents txtcostcenter As common.UserControls.txtFinder
    Friend WithEvents lblcostcenter As common.Controls.MyLabel
    Friend WithEvents lblGLAcc As common.Controls.MyLabel
    Friend WithEvents txtGLAcc As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

