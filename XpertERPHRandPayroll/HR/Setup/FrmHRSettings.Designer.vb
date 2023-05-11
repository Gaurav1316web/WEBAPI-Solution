Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHRSettings
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GrpPara = New System.Windows.Forms.GroupBox()
        Me.rbnSingleP = New common.Controls.MyRadioButton()
        Me.rbnDoubleP = New common.Controls.MyRadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GrpPara.SuspendLayout()
        CType(Me.rbnSingleP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbnDoubleP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpPara)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel27)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(427, 288)
        Me.SplitContainer1.SplitterDistance = 242
        Me.SplitContainer1.TabIndex = 0
        '
        'GrpPara
        '
        Me.GrpPara.Controls.Add(Me.rbnSingleP)
        Me.GrpPara.Controls.Add(Me.rbnDoubleP)
        Me.GrpPara.Location = New System.Drawing.Point(11, 56)
        Me.GrpPara.Name = "GrpPara"
        Me.GrpPara.Size = New System.Drawing.Size(409, 49)
        Me.GrpPara.TabIndex = 64
        Me.GrpPara.TabStop = False
        Me.GrpPara.Text = "Parameter"
        '
        'rbnSingleP
        '
        Me.rbnSingleP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbnSingleP.Location = New System.Drawing.Point(15, 21)
        Me.rbnSingleP.MyLinkLable1 = Nothing
        Me.rbnSingleP.MyLinkLable2 = Nothing
        Me.rbnSingleP.Name = "rbnSingleP"
        Me.rbnSingleP.Size = New System.Drawing.Size(105, 18)
        Me.rbnSingleP.TabIndex = 62
        Me.rbnSingleP.Text = "Single Parameter"
        Me.rbnSingleP.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbnDoubleP
        '
        Me.rbnDoubleP.Location = New System.Drawing.Point(288, 21)
        Me.rbnDoubleP.MyLinkLable1 = Nothing
        Me.rbnDoubleP.MyLinkLable2 = Nothing
        Me.rbnDoubleP.Name = "rbnDoubleP"
        Me.rbnDoubleP.Size = New System.Drawing.Size(111, 18)
        Me.rbnDoubleP.TabIndex = 63
        Me.rbnDoubleP.TabStop = False
        Me.rbnDoubleP.Text = "Double Parameter"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(216, 21)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 61
        Me.MyLabel1.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(273, 19)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(101, 20)
        Me.dtpToDate.TabIndex = 60
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "19/06/2014"
        Me.dtpToDate.Value = New Date(2014, 6, 19, 11, 12, 11, 559)
        '
        'MyLabel27
        '
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(14, 21)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel27.TabIndex = 59
        Me.MyLabel27.Text = "From Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(81, 19)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Nothing
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(101, 20)
        Me.dtpFromDate.TabIndex = 58
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "19/06/2014"
        Me.dtpFromDate.Value = New Date(2014, 6, 19, 11, 12, 11, 559)
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(332, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(83, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(11, 12)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(82, 18)
        Me.btnsave.TabIndex = 1
        Me.btnsave.Text = "Save"
        '
        'FrmHRSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 288)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmHRSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "HR Settings"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GrpPara.ResumeLayout(False)
        Me.GrpPara.PerformLayout()
        CType(Me.rbnSingleP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbnDoubleP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents GrpPara As System.Windows.Forms.GroupBox
    Friend WithEvents rbnSingleP As common.Controls.MyRadioButton
    Friend WithEvents rbnDoubleP As common.Controls.MyRadioButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
End Class

