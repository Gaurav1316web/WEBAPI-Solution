Imports XpertERPEngine
Imports common
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmShortCloseDODairy
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
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton
        Me.btnGo = New Telerik.WinControls.UI.RadButton
        Me.GbDate = New Telerik.WinControls.UI.RadGroupBox
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.lblDate = New common.Controls.MyLabel
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.GV1 = New common.UserControls.MyRadGridView
        Me.BtnReset = New Telerik.WinControls.UI.RadButton
        Me.btnShortCloseDo = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GbDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbDate.SuspendLayout()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShortCloseDo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUnSelect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GbDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1105, 417)
        Me.SplitContainer1.SplitterDistance = 66
        Me.SplitContainer1.TabIndex = 0
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnSelect.Location = New System.Drawing.Point(313, 25)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(80, 18)
        Me.btnUnSelect.TabIndex = 3
        Me.btnUnSelect.Text = "UnSelect All"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(250, 25)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(57, 18)
        Me.btnGo.TabIndex = 9
        Me.btnGo.Text = ">>>"
        '
        'GbDate
        '
        Me.GbDate.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GbDate.Controls.Add(Me.txtDate)
        Me.GbDate.Controls.Add(Me.lblDate)
        Me.GbDate.HeaderText = "Date"
        Me.GbDate.Location = New System.Drawing.Point(14, 6)
        Me.GbDate.Name = "GbDate"
        Me.GbDate.Size = New System.Drawing.Size(221, 46)
        Me.GbDate.TabIndex = 0
        Me.GbDate.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(56, 17)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(151, 20)
        Me.txtDate.TabIndex = 14
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "17/12/2011 12:00 AM"
        Me.txtDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblDate
        '
        Me.lblDate.Location = New System.Drawing.Point(17, 19)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(33, 18)
        Me.lblDate.TabIndex = 15
        Me.lblDate.Text = " Date"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GV1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnShortCloseDo)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Size = New System.Drawing.Size(1105, 347)
        Me.SplitContainer2.SplitterDistance = 308
        Me.SplitContainer2.TabIndex = 0
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        Me.GV1.Name = "GV1"
        Me.GV1.Size = New System.Drawing.Size(1105, 308)
        Me.GV1.TabIndex = 1
        Me.GV1.Text = "GV1"
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(14, 7)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(71, 21)
        Me.BtnReset.TabIndex = 13
        Me.BtnReset.Text = "Reset"
        '
        'btnShortCloseDo
        '
        Me.btnShortCloseDo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShortCloseDo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShortCloseDo.Location = New System.Drawing.Point(91, 7)
        Me.btnShortCloseDo.Name = "btnShortCloseDo"
        Me.btnShortCloseDo.Size = New System.Drawing.Size(103, 21)
        Me.btnShortCloseDo.TabIndex = 12
        Me.btnShortCloseDo.Text = "Short Close DO"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1017, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(77, 22)
        Me.btnclose.TabIndex = 14
        Me.btnclose.Text = "Close"
        '
        'FrmShortCloseDO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 417)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmShortCloseDO"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmShortCloseDO"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GbDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbDate.ResumeLayout(False)
        Me.GbDate.PerformLayout()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShortCloseDo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents GbDate As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnShortCloseDo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
End Class

