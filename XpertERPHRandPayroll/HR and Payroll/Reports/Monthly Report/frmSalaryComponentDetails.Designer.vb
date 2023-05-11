Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryComponentDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalaryComponentDetails))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbDeduction = New common.Controls.MyRadioButton
        Me.rdbEarning = New common.Controls.MyRadioButton
        Me.lblComponentName = New common.Controls.MyLabel
        Me.txtComponent = New common.UserControls.txtFinder
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.lblToPPName = New common.Controls.MyLabel
        Me.txtToPP = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.lblFrompp = New common.Controls.MyLabel
        Me.txtFromPP = New common.UserControls.txtFinder
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.chkShowZero = New Telerik.WinControls.UI.RadCheckBox
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbEarning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComponentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToPPName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkShowZero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkShowZero)
        Me.RadGroupBox1.Controls.Add(Me.rdbDeduction)
        Me.RadGroupBox1.Controls.Add(Me.rdbEarning)
        Me.RadGroupBox1.Controls.Add(Me.lblComponentName)
        Me.RadGroupBox1.Controls.Add(Me.txtComponent)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.lblToPPName)
        Me.RadGroupBox1.Controls.Add(Me.txtToPP)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lblFrompp)
        Me.RadGroupBox1.Controls.Add(Me.txtFromPP)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 10)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(528, 146)
        Me.RadGroupBox1.TabIndex = 0
        '
        'rdbDeduction
        '
        Me.rdbDeduction.Location = New System.Drawing.Point(195, 12)
        Me.rdbDeduction.MyLinkLable1 = Nothing
        Me.rdbDeduction.MyLinkLable2 = Nothing
        Me.rdbDeduction.Name = "rdbDeduction"
        Me.rdbDeduction.Size = New System.Drawing.Size(104, 18)
        Me.rdbDeduction.TabIndex = 224
        Me.rdbDeduction.Text = "Deductions"
        '
        'rdbEarning
        '
        Me.rdbEarning.Location = New System.Drawing.Point(113, 12)
        Me.rdbEarning.MyLinkLable1 = Nothing
        Me.rdbEarning.MyLinkLable2 = Nothing
        Me.rdbEarning.Name = "rdbEarning"
        Me.rdbEarning.Size = New System.Drawing.Size(76, 18)
        Me.rdbEarning.TabIndex = 223
        Me.rdbEarning.Text = "Earning"
        '
        'lblComponentName
        '
        Me.lblComponentName.AutoSize = False
        Me.lblComponentName.BorderVisible = True
        Me.lblComponentName.Location = New System.Drawing.Point(324, 86)
        Me.lblComponentName.Name = "lblComponentName"
        Me.lblComponentName.Size = New System.Drawing.Size(191, 19)
        Me.lblComponentName.TabIndex = 222
        Me.lblComponentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComponent
        '
        Me.txtComponent.Location = New System.Drawing.Point(111, 86)
        Me.txtComponent.MendatroryField = True
        Me.txtComponent.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComponent.MyLinkLable1 = Me.MyLabel3
        Me.txtComponent.MyLinkLable2 = Me.lblComponentName
        Me.txtComponent.MyReadOnly = False
        Me.txtComponent.Name = "txtComponent"
        Me.txtComponent.Size = New System.Drawing.Size(208, 18)
        Me.txtComponent.TabIndex = 221
        Me.txtComponent.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(11, 86)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(65, 18)
        Me.MyLabel3.TabIndex = 220
        Me.MyLabel3.Text = "Component"
        '
        'lblToPPName
        '
        Me.lblToPPName.AutoSize = False
        Me.lblToPPName.BorderVisible = True
        Me.lblToPPName.Location = New System.Drawing.Point(324, 62)
        Me.lblToPPName.Name = "lblToPPName"
        Me.lblToPPName.Size = New System.Drawing.Size(191, 19)
        Me.lblToPPName.TabIndex = 219
        Me.lblToPPName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtToPP
        '
        Me.txtToPP.Location = New System.Drawing.Point(111, 62)
        Me.txtToPP.MendatroryField = True
        Me.txtToPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToPP.MyLinkLable1 = Me.MyLabel2
        Me.txtToPP.MyLinkLable2 = Me.lblToPPName
        Me.txtToPP.MyReadOnly = False
        Me.txtToPP.Name = "txtToPP"
        Me.txtToPP.Size = New System.Drawing.Size(208, 18)
        Me.txtToPP.TabIndex = 218
        Me.txtToPP.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(9, 62)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel2.TabIndex = 217
        Me.MyLabel2.Text = "To Pay Period"
        '
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.Location = New System.Drawing.Point(324, 38)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(191, 19)
        Me.lblFrompp.TabIndex = 216
        Me.lblFrompp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFromPP
        '
        Me.txtFromPP.Location = New System.Drawing.Point(111, 38)
        Me.txtFromPP.MendatroryField = True
        Me.txtFromPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromPP.MyLinkLable1 = Me.RadLabel1
        Me.txtFromPP.MyLinkLable2 = Me.lblFrompp
        Me.txtFromPP.MyReadOnly = False
        Me.txtFromPP.Name = "txtFromPP"
        Me.txtFromPP.Size = New System.Drawing.Size(208, 18)
        Me.txtFromPP.TabIndex = 215
        Me.txtFromPP.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(9, 38)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(88, 18)
        Me.RadLabel1.TabIndex = 214
        Me.RadLabel1.Text = "From Pay Period"
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(474, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(3, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Print"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Class = ""
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        Me.RadMenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Class = ""
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Class = ""
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(543, 196)
        Me.SplitContainer1.SplitterDistance = 159
        Me.SplitContainer1.TabIndex = 0
        '
        'chkShowZero
        '
        Me.chkShowZero.Location = New System.Drawing.Point(111, 115)
        Me.chkShowZero.Name = "chkShowZero"
        Me.chkShowZero.Size = New System.Drawing.Size(194, 18)
        Me.chkShowZero.TabIndex = 225
        Me.chkShowZero.Text = "Show zero amount employees also"
        '
        'frmSalaryComponentDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(543, 196)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "frmSalaryComponentDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salary Componenet Details"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbEarning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComponentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToPPName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkShowZero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents txtFromPP As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblToPPName As common.Controls.MyLabel
    Friend WithEvents txtToPP As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblComponentName As common.Controls.MyLabel
    Friend WithEvents txtComponent As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents rdbDeduction As common.Controls.MyRadioButton
    Friend WithEvents rdbEarning As common.Controls.MyRadioButton
    Friend WithEvents chkShowZero As Telerik.WinControls.UI.RadCheckBox
End Class

