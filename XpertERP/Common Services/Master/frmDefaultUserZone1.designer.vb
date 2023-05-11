<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDefaultUserZone1
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
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboZone = New common.Controls.MyComboBox()
        Me.lblaccgp = New common.Controls.MyLabel()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cboZone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(203, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(80, 24)
        Me.RadButton1.TabIndex = 1
        Me.RadButton1.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Size = New System.Drawing.Size(292, 79)
        Me.SplitContainer1.SplitterDistance = 39
        Me.SplitContainer1.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cboZone)
        Me.Panel1.Controls.Add(Me.lblaccgp)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(292, 39)
        Me.Panel1.TabIndex = 0
        '
        'cboZone
        '
        Me.cboZone.AutoCompleteDisplayMember = Nothing
        Me.cboZone.AutoCompleteValueMember = Nothing
        Me.cboZone.CalculationExpression = Nothing
        Me.cboZone.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboZone.FieldCode = Nothing
        Me.cboZone.FieldDesc = Nothing
        Me.cboZone.FieldMaxLength = 0
        Me.cboZone.FieldName = Nothing
        Me.cboZone.isCalculatedField = False
        Me.cboZone.IsSourceFromTable = False
        Me.cboZone.IsSourceFromValueList = False
        Me.cboZone.IsUnique = False
        Me.cboZone.Location = New System.Drawing.Point(104, 10)
        Me.cboZone.MendatroryField = True
        Me.cboZone.MyLinkLable1 = Nothing
        Me.cboZone.MyLinkLable2 = Nothing
        Me.cboZone.Name = "cboZone"
        Me.cboZone.ReferenceFieldDesc = Nothing
        Me.cboZone.ReferenceFieldName = Nothing
        Me.cboZone.ReferenceTableName = Nothing
        Me.cboZone.Size = New System.Drawing.Size(176, 20)
        Me.cboZone.TabIndex = 46
        '
        'lblaccgp
        '
        Me.lblaccgp.FieldName = Nothing
        Me.lblaccgp.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblaccgp.Location = New System.Drawing.Point(7, 12)
        Me.lblaccgp.Name = "lblaccgp"
        Me.lblaccgp.Size = New System.Drawing.Size(89, 16)
        Me.lblaccgp.TabIndex = 45
        Me.lblaccgp.Text = "User Zone Code"
        '
        'frmDefaultUserZone1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 79)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(300, 150)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 100)
        Me.Name = "frmDefaultUserZone1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(300, 300)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Default User Zone"
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cboZone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblaccgp As common.Controls.MyLabel
    Friend WithEvents cboZone As common.Controls.MyComboBox
End Class

