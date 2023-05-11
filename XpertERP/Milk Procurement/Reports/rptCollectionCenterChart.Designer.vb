<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptCollectionCenterChart
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
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.lblRootDesg = New common.Controls.MyLabel
        Me.cboRootDesg = New common.Controls.MyComboBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.chkDepartment = New Telerik.WinControls.UI.RadCheckBox
        Me.RadScrollablePanel1 = New Telerik.WinControls.UI.RadScrollablePanel
        Me.picOrgChart = New System.Windows.Forms.PictureBox
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRootDesg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboRootDesg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel1.PanelContainer.SuspendLayout()
        Me.RadScrollablePanel1.SuspendLayout()
        CType(Me.picOrgChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRootDesg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboRootDesg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkDepartment)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadScrollablePanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(763, 483)
        Me.SplitContainer1.SplitterDistance = 29
        Me.SplitContainer1.TabIndex = 0
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(583, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(84, 23)
        Me.btnExport.TabIndex = 26
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Image"
        Me.RadMenuItem2.AccessibleName = "Image"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Image"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(491, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(87, 24)
        Me.btnRefresh.TabIndex = 25
        Me.btnRefresh.Text = ">>>"
        '
        'lblRootDesg
        '
        Me.lblRootDesg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRootDesg.Location = New System.Drawing.Point(139, 6)
        Me.lblRootDesg.Name = "lblRootDesg"
        Me.lblRootDesg.Size = New System.Drawing.Size(120, 16)
        Me.lblRootDesg.TabIndex = 24
        Me.lblRootDesg.Text = "Root Collection Center"
        '
        'cboRootDesg
        '
        Me.cboRootDesg.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboRootDesg.Location = New System.Drawing.Point(265, 3)
        Me.cboRootDesg.MendatroryField = True
        Me.cboRootDesg.MyLinkLable1 = Me.lblRootDesg
        Me.cboRootDesg.MyLinkLable2 = Nothing
        Me.cboRootDesg.Name = "cboRootDesg"
        Me.cboRootDesg.Size = New System.Drawing.Size(215, 20)
        Me.cboRootDesg.TabIndex = 23
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(673, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 24)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        '
        'chkDepartment
        '
        Me.chkDepartment.Location = New System.Drawing.Point(5, 5)
        Me.chkDepartment.Name = "chkDepartment"
        Me.chkDepartment.Size = New System.Drawing.Size(129, 18)
        Me.chkDepartment.TabIndex = 0
        Me.chkDepartment.Text = "Show Collection Level"
        Me.chkDepartment.Visible = False
        '
        'RadScrollablePanel1
        '
        Me.RadScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadScrollablePanel1.Name = "RadScrollablePanel1"
        '
        'RadScrollablePanel1.PanelContainer
        '
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.picOrgChart)
        Me.RadScrollablePanel1.PanelContainer.Size = New System.Drawing.Size(761, 448)
        Me.RadScrollablePanel1.Size = New System.Drawing.Size(763, 450)
        Me.RadScrollablePanel1.TabIndex = 40
        Me.RadScrollablePanel1.Text = "RadScrollablePanel1"
        '
        'picOrgChart
        '
        Me.picOrgChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picOrgChart.Location = New System.Drawing.Point(0, 0)
        Me.picOrgChart.Name = "picOrgChart"
        Me.picOrgChart.Size = New System.Drawing.Size(761, 448)
        Me.picOrgChart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picOrgChart.TabIndex = 40
        Me.picOrgChart.TabStop = False
        '
        'rptCollectionCenterChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(763, 483)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptCollectionCenterChart"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Collection Level Chart"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRootDesg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboRootDesg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.PanelContainer.ResumeLayout(False)
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.ResumeLayout(False)
        CType(Me.picOrgChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkDepartment As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblRootDesg As common.Controls.MyLabel
    Friend WithEvents cboRootDesg As common.Controls.MyComboBox
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadScrollablePanel1 As Telerik.WinControls.UI.RadScrollablePanel
    Friend WithEvents picOrgChart As System.Windows.Forms.PictureBox
End Class

