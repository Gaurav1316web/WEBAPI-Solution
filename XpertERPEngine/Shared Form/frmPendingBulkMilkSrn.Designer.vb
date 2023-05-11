<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPendingBulkMilkSrn
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel58 = New common.Controls.MyLabel()
        Me.txtExtraRate = New common.MyNumBox()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnOk = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExtraRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOk)
        Me.SplitContainer1.Size = New System.Drawing.Size(781, 472)
        Me.SplitContainer1.SplitterDistance = 440
        Me.SplitContainer1.TabIndex = 0
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 25)
        '
        'gv
        '
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(781, 415)
        Me.gv.TabIndex = 1
        Me.gv.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.MyLabel58)
        Me.Panel1.Controls.Add(Me.txtExtraRate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(781, 25)
        Me.Panel1.TabIndex = 2
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(233, 0)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(65, 24)
        Me.RadButton2.TabIndex = 1406
        Me.RadButton2.Text = "Clear All"
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(165, 0)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(65, 24)
        Me.RadButton1.TabIndex = 1405
        Me.RadButton1.Text = "Apply"
        '
        'MyLabel58
        '
        Me.MyLabel58.FieldName = Nothing
        Me.MyLabel58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel58.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel58.Name = "MyLabel58"
        Me.MyLabel58.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel58.TabIndex = 1404
        Me.MyLabel58.Text = "Extra Rate"
        '
        'txtExtraRate
        '
        Me.txtExtraRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtExtraRate.CalculationExpression = Nothing
        Me.txtExtraRate.DecimalPlaces = 2
        Me.txtExtraRate.FieldCode = Nothing
        Me.txtExtraRate.FieldDesc = Nothing
        Me.txtExtraRate.FieldMaxLength = 0
        Me.txtExtraRate.FieldName = Nothing
        Me.txtExtraRate.isCalculatedField = False
        Me.txtExtraRate.IsSourceFromTable = False
        Me.txtExtraRate.IsSourceFromValueList = False
        Me.txtExtraRate.IsUnique = False
        Me.txtExtraRate.Location = New System.Drawing.Point(69, 2)
        Me.txtExtraRate.MendatroryField = False
        Me.txtExtraRate.MyLinkLable1 = Nothing
        Me.txtExtraRate.MyLinkLable2 = Nothing
        Me.txtExtraRate.Name = "txtExtraRate"
        Me.txtExtraRate.ReferenceFieldDesc = Nothing
        Me.txtExtraRate.ReferenceFieldName = Nothing
        Me.txtExtraRate.ReferenceTableName = Nothing
        Me.txtExtraRate.Size = New System.Drawing.Size(93, 20)
        Me.txtExtraRate.TabIndex = 1403
        Me.txtExtraRate.Text = "0"
        Me.txtExtraRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtExtraRate.Value = 0R
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.Location = New System.Drawing.Point(393, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(130, 24)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Esc : Cancel"
        '
        'btnOk
        '
        Me.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOk.Location = New System.Drawing.Point(257, 2)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(130, 24)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "F5 : OK"
        '
        'FrmPendingBulkMilkSrn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 472)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPendingBulkMilkSrn"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPendingBulkMilkSrn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExtraRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MyLabel58 As Controls.MyLabel
    Friend WithEvents txtExtraRate As MyNumBox
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents RadButton1 As RadButton
End Class

