Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmESIOnline
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnLoad = New Telerik.WinControls.UI.RadButton
        Me.ChkIncludeZero = New System.Windows.Forms.CheckBox
        Me.lblFrompp = New common.Controls.MyLabel
        Me.txtFromPP = New common.UserControls.txtFinder
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemSave = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItemDelete = New Telerik.WinControls.UI.RadMenuItem
        Me.btnExpoPDF = New Telerik.WinControls.UI.RadButton
        Me.btnExpoExl = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExpoPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExpoExl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 38)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1033, 439)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = " "
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AutoGenerateColumns = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.gv1.RootElement.ForeColor = System.Drawing.Color.Black
        Me.gv1.Size = New System.Drawing.Size(1013, 409)
        Me.gv1.TabIndex = 146
        Me.gv1.Text = "RadGridView4"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(914, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(113, 21)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnLoad)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkIncludeZero)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFrompp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromPP)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExpoPDF)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExpoExl)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1042, 516)
        Me.SplitContainer1.SplitterDistance = 482
        Me.SplitContainer1.TabIndex = 0
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnLoad.Location = New System.Drawing.Point(957, 22)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(80, 21)
        Me.btnLoad.TabIndex = 221
        Me.btnLoad.Text = "Load Data"
        '
        'ChkIncludeZero
        '
        Me.ChkIncludeZero.AutoSize = True
        Me.ChkIncludeZero.Location = New System.Drawing.Point(517, 27)
        Me.ChkIncludeZero.Name = "ChkIncludeZero"
        Me.ChkIncludeZero.Size = New System.Drawing.Size(253, 17)
        Me.ChkIncludeZero.TabIndex = 220
        Me.ChkIncludeZero.Text = "Include Employee Whose ESI Earning Is Zero"
        Me.ChkIncludeZero.UseVisualStyleBackColor = True
        '
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.Location = New System.Drawing.Point(319, 26)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(191, 19)
        Me.lblFrompp.TabIndex = 219
        Me.lblFrompp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFromPP
        '
        Me.txtFromPP.Location = New System.Drawing.Point(106, 26)
        Me.txtFromPP.MendatroryField = True
        Me.txtFromPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromPP.MyLinkLable1 = Me.RadLabel1
        Me.txtFromPP.MyLinkLable2 = Me.lblFrompp
        Me.txtFromPP.MyReadOnly = False
        Me.txtFromPP.Name = "txtFromPP"
        Me.txtFromPP.Size = New System.Drawing.Size(208, 18)
        Me.txtFromPP.TabIndex = 218
        Me.txtFromPP.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(4, 26)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(92, 18)
        Me.RadLabel1.TabIndex = 217
        Me.RadLabel1.Text = "Select Pay Period"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1042, 20)
        Me.RadMenu1.TabIndex = 214
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Class = ""
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemSave, Me.RadMenuItemDelete})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemSave
        '
        Me.RadMenuItemSave.AccessibleDescription = "Save Layout"
        Me.RadMenuItemSave.AccessibleName = "Save Layout"
        Me.RadMenuItemSave.Name = "RadMenuItemSave"
        Me.RadMenuItemSave.Text = "Save Layout"
        Me.RadMenuItemSave.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItemDelete
        '
        Me.RadMenuItemDelete.AccessibleDescription = "Delete Layout"
        Me.RadMenuItemDelete.AccessibleName = "Delete Layout"
        Me.RadMenuItemDelete.Name = "RadMenuItemDelete"
        Me.RadMenuItemDelete.Text = "Delete Layout"
        Me.RadMenuItemDelete.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnExpoPDF
        '
        Me.btnExpoPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExpoPDF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExpoPDF.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnExpoPDF.Location = New System.Drawing.Point(134, 3)
        Me.btnExpoPDF.Name = "btnExpoPDF"
        Me.btnExpoPDF.Size = New System.Drawing.Size(113, 21)
        Me.btnExpoPDF.TabIndex = 216
        Me.btnExpoPDF.Text = "Export To PDF"
        '
        'btnExpoExl
        '
        Me.btnExpoExl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExpoExl.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExpoExl.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnExpoExl.Location = New System.Drawing.Point(14, 3)
        Me.btnExpoExl.Name = "btnExpoExl"
        Me.btnExpoExl.Size = New System.Drawing.Size(113, 21)
        Me.btnExpoExl.TabIndex = 215
        Me.btnExpoExl.Text = "Export To Excel"
        '
        'frmESIOnline
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "frmESIOnline"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "ESI Online"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExpoPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExpoExl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemSave As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemDelete As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExpoExl As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents txtFromPP As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ChkIncludeZero As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoad As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExpoPDF As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class

