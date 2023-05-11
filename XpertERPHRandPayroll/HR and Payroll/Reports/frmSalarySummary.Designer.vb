Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalarySummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSalarySummary))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.FndLocationCode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblFrompp = New common.Controls.MyLabel()
        Me.txtFromPP = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(-1, 2)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(544, 450)
        Me.SplitContainer1.SplitterDistance = 414
        Me.SplitContainer1.TabIndex = 9
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtDivisionMult)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.FndLocationCode)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblLocationName)
        Me.RadGroupBox1.Controls.Add(Me.lblFrompp)
        Me.RadGroupBox1.Controls.Add(Me.txtFromPP)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(528, 99)
        Me.RadGroupBox1.TabIndex = 2
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(111, 60)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(404, 19)
        Me.txtDivisionMult.TabIndex = 341
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(9, 62)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(46, 18)
        Me.MyLabel2.TabIndex = 340
        Me.MyLabel2.Text = "Division"
        '
        'FndLocationCode
        '
        Me.FndLocationCode.Location = New System.Drawing.Point(111, 38)
        Me.FndLocationCode.MendatroryField = True
        Me.FndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLocationCode.MyLinkLable1 = Me.lblLocation
        Me.FndLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.FndLocationCode.MyReadOnly = False
        Me.FndLocationCode.MyShowMasterFormButton = False
        Me.FndLocationCode.Name = "FndLocationCode"
        Me.FndLocationCode.Size = New System.Drawing.Size(207, 18)
        Me.FndLocationCode.TabIndex = 224
        Me.FndLocationCode.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(9, 40)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 225
        Me.lblLocation.Text = "Location"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocationName.Location = New System.Drawing.Point(324, 38)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(191, 18)
        Me.lblLocationName.TabIndex = 226
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        '
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.Location = New System.Drawing.Point(324, 16)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(191, 19)
        Me.lblFrompp.TabIndex = 216
        Me.lblFrompp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFromPP
        '
        Me.txtFromPP.Location = New System.Drawing.Point(111, 16)
        Me.txtFromPP.MendatroryField = True
        Me.txtFromPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromPP.MyLinkLable1 = Me.RadLabel1
        Me.txtFromPP.MyLinkLable2 = Me.lblFrompp
        Me.txtFromPP.MyReadOnly = False
        Me.txtFromPP.MyShowMasterFormButton = False
        Me.txtFromPP.Name = "txtFromPP"
        Me.txtFromPP.Size = New System.Drawing.Size(208, 18)
        Me.txtFromPP.TabIndex = 215
        Me.txtFromPP.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(9, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(92, 18)
        Me.RadLabel1.TabIndex = 214
        Me.RadLabel1.Text = "Select Pay Period"
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
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(6, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(465, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'FrmSalarySummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 452)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSalarySummary"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmSalarySummary"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents txtFromPP As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents FndLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

