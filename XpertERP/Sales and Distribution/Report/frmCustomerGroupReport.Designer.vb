<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomerGroupReport
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndToCustomerGroup = New finder.finder
        Me.lblFromCustomerGroup = New common.Controls.MyLabel
        Me.lblToCustomerGroup = New common.Controls.MyLabel
        Me.fndFromCustomerGroup = New finder.finder
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lblFromCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 11)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(563, 55)
        Me.RadGroupBox1.TabIndex = 1
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndToCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.lblFromCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.lblToCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.fndFromCustomerGroup)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 11)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(538, 35)
        Me.RadGroupBox2.TabIndex = 46
        '
        'fndToCustomerGroup
        '
        Me.fndToCustomerGroup.Caption = Nothing
        Me.fndToCustomerGroup.ConnectionString = Nothing
        Me.fndToCustomerGroup.Icon = Nothing
        Me.fndToCustomerGroup.Location = New System.Drawing.Point(397, 8)
        Me.fndToCustomerGroup.Name = "fndToCustomerGroup"
        Me.fndToCustomerGroup.NewTimer = Nothing
        Me.fndToCustomerGroup.Query = Nothing
        Me.fndToCustomerGroup.ResultDT = Nothing
        Me.fndToCustomerGroup.SelectedRowDR = Nothing
        Me.fndToCustomerGroup.SelectedValue = Nothing
        Me.fndToCustomerGroup.SelectedValue1 = Nothing
        Me.fndToCustomerGroup.Size = New System.Drawing.Size(140, 20)
        Me.fndToCustomerGroup.TabIndex = 47
        Me.fndToCustomerGroup.ValueToSelect = Nothing
        Me.fndToCustomerGroup.ValueToSelect1 = Nothing
        '
        'lblFromCustomerGroup
        '
        Me.lblFromCustomerGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromCustomerGroup.Location = New System.Drawing.Point(13, 10)
        Me.lblFromCustomerGroup.Name = "lblFromCustomerGroup"
        Me.lblFromCustomerGroup.Size = New System.Drawing.Size(119, 16)
        Me.lblFromCustomerGroup.TabIndex = 0
        Me.lblFromCustomerGroup.Text = "From Customer Group"
        '
        'lblToCustomerGroup
        '
        Me.lblToCustomerGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToCustomerGroup.Location = New System.Drawing.Point(283, 10)
        Me.lblToCustomerGroup.Name = "lblToCustomerGroup"
        Me.lblToCustomerGroup.Size = New System.Drawing.Size(106, 16)
        Me.lblToCustomerGroup.TabIndex = 1
        Me.lblToCustomerGroup.Text = "To Customer Group"
        '
        'fndFromCustomerGroup
        '
        Me.fndFromCustomerGroup.Caption = Nothing
        Me.fndFromCustomerGroup.ConnectionString = Nothing
        Me.fndFromCustomerGroup.Icon = Nothing
        Me.fndFromCustomerGroup.Location = New System.Drawing.Point(136, 8)
        Me.fndFromCustomerGroup.Name = "fndFromCustomerGroup"
        Me.fndFromCustomerGroup.NewTimer = Nothing
        Me.fndFromCustomerGroup.Query = Nothing
        Me.fndFromCustomerGroup.ResultDT = Nothing
        Me.fndFromCustomerGroup.SelectedRowDR = Nothing
        Me.fndFromCustomerGroup.SelectedValue = Nothing
        Me.fndFromCustomerGroup.SelectedValue1 = Nothing
        Me.fndFromCustomerGroup.Size = New System.Drawing.Size(141, 20)
        Me.fndFromCustomerGroup.TabIndex = 46
        Me.fndFromCustomerGroup.ValueToSelect = Nothing
        Me.fndFromCustomerGroup.ValueToSelect1 = Nothing
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(87, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 45
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(13, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 43
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(508, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 44
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(587, 121)
        Me.SplitContainer1.SplitterDistance = 77
        Me.SplitContainer1.TabIndex = 2
        '
        'FrmCustomerGroupReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 121)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCustomerGroupReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Group Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lblFromCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndToCustomerGroup As finder.finder
    Friend WithEvents fndFromCustomerGroup As finder.finder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblFromCustomerGroup As common.Controls.MyLabel
    Friend WithEvents lblToCustomerGroup As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

