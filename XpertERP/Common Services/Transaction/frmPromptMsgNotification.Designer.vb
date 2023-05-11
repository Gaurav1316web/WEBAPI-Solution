<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPromptMsgNotification
    Inherits Telerik.WinControls.UI.RadForm
    'Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.grdLoginInfo = New common.UserControls.MyRadGridView()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.grdLoginInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLoginInfo.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdLoginInfo
        '
        Me.grdLoginInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLoginInfo.Location = New System.Drawing.Point(0, 0)
        '
        'grdLoginInfo
        '
        Me.grdLoginInfo.MasterTemplate.ShowHeaderCellButtons = True
        Me.grdLoginInfo.Name = "grdLoginInfo"
        Me.grdLoginInfo.ShowHeaderCellButtons = True
        Me.grdLoginInfo.Size = New System.Drawing.Size(292, 229)
        Me.grdLoginInfo.TabIndex = 0
        Me.grdLoginInfo.Text = "RadGridView1"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(228, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(55, 24)
        Me.RadButton1.TabIndex = 1
        Me.RadButton1.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdLoginInfo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Size = New System.Drawing.Size(292, 270)
        Me.SplitContainer1.SplitterDistance = 229
        Me.SplitContainer1.TabIndex = 2
        '
        'Timer1
        '
        '
        'frmPromptMsgNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 270)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(300, 300)
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "frmPromptMsgNotification"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(652, 501)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Notification"
        CType(Me.grdLoginInfo.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLoginInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents grdLoginInfo As common.UserControls.MyRadGridView
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Timer1 As Timer
End Class

