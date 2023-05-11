<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReconciliationSetting
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
        Me.gbAcc = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgAccount = New common.MyCheckBoxGrid
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.cboReportComponent = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.cboReportName = New common.Controls.MyComboBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gbAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAcc.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboReportComponent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboReportName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbAcc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboReportComponent)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboReportName)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(510, 412)
        Me.SplitContainer1.SplitterDistance = 377
        Me.SplitContainer1.TabIndex = 0
        '
        'gbAcc
        '
        Me.gbAcc.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbAcc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbAcc.Controls.Add(Me.cbgAccount)
        Me.gbAcc.FooterImageIndex = -1
        Me.gbAcc.FooterImageKey = ""
        Me.gbAcc.HeaderImageIndex = -1
        Me.gbAcc.HeaderImageKey = ""
        Me.gbAcc.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.gbAcc.HeaderText = "Account"
        Me.gbAcc.Location = New System.Drawing.Point(12, 64)
        Me.gbAcc.Name = "gbAcc"
        Me.gbAcc.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.gbAcc.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbAcc.Size = New System.Drawing.Size(482, 308)
        Me.gbAcc.TabIndex = 313
        Me.gbAcc.Text = "Account"
        '
        'cbgAccount
        '
        Me.cbgAccount.CheckedValue = Nothing
        Me.cbgAccount.DataSource = Nothing
        Me.cbgAccount.DisplayMember = "Name"
        Me.cbgAccount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgAccount.Location = New System.Drawing.Point(10, 20)
        Me.cbgAccount.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgAccount.MyShowHeadrText = False
        Me.cbgAccount.Name = "cbgAccount"
        Me.cbgAccount.Size = New System.Drawing.Size(462, 278)
        Me.cbgAccount.TabIndex = 1
        Me.cbgAccount.ValueMember = "Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(12, 38)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(102, 18)
        Me.MyLabel2.TabIndex = 311
        Me.MyLabel2.Text = "Report Component"
        '
        'cboReportComponent
        '
        Me.cboReportComponent.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboReportComponent.Location = New System.Drawing.Point(126, 38)
        Me.cboReportComponent.MendatroryField = True
        Me.cboReportComponent.MyLinkLable1 = Me.MyLabel2
        Me.cboReportComponent.MyLinkLable2 = Nothing
        Me.cboReportComponent.Name = "cboReportComponent"
        Me.cboReportComponent.ShowImageInEditorArea = True
        Me.cboReportComponent.Size = New System.Drawing.Size(368, 20)
        Me.cboReportComponent.TabIndex = 312
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(12, 12)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(73, 18)
        Me.MyLabel1.TabIndex = 309
        Me.MyLabel1.Text = "Report Name"
        '
        'cboReportName
        '
        Me.cboReportName.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboReportName.Location = New System.Drawing.Point(126, 12)
        Me.cboReportName.MendatroryField = True
        Me.cboReportName.MyLinkLable1 = Me.MyLabel1
        Me.cboReportName.MyLinkLable2 = Nothing
        Me.cboReportName.Name = "cboReportName"
        Me.cboReportName.ShowImageInEditorArea = True
        Me.cboReportName.Size = New System.Drawing.Size(368, 20)
        Me.cboReportName.TabIndex = 310
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(439, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 22)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        '
        'FrmReconciliationSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 412)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmReconciliationSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Reconciliation Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gbAcc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAcc.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboReportComponent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboReportName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cboReportName As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cboReportComponent As common.Controls.MyComboBox
    Friend WithEvents gbAcc As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgAccount As common.MyCheckBoxGrid
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

