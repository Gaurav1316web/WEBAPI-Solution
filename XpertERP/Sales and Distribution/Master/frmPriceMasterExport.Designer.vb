<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPriceMasterExport
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.radioLandingCostCst = New Telerik.WinControls.UI.RadRadioButton
        Me.radioLandingCostVat = New Telerik.WinControls.UI.RadRadioButton
        Me.radioAll = New Telerik.WinControls.UI.RadRadioButton
        Me.radioMrpCst = New Telerik.WinControls.UI.RadRadioButton
        Me.radioMrpVat = New Telerik.WinControls.UI.RadRadioButton
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.radioReset = New Telerik.WinControls.UI.RadButton
        Me.rbtnRefresh = New Telerik.WinControls.UI.RadButton
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton
        Me.rbtnExport = New Telerik.WinControls.UI.RadButton
        Me.chkBackCalculation = New Telerik.WinControls.UI.RadRadioButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.radioLandingCostCst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioLandingCostVat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioMrpCst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioMrpVat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.radioReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBackCalculation, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(867, 497)
        Me.SplitContainer1.SplitterDistance = 453
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(867, 453)
        Me.SplitContainer2.SplitterDistance = 41
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkBackCalculation)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(867, 41)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Export"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.radioLandingCostCst)
        Me.Panel1.Controls.Add(Me.radioLandingCostVat)
        Me.Panel1.Controls.Add(Me.radioAll)
        Me.Panel1.Controls.Add(Me.radioMrpCst)
        Me.Panel1.Controls.Add(Me.radioMrpVat)
        Me.Panel1.Location = New System.Drawing.Point(58, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(463, 25)
        Me.Panel1.TabIndex = 4
        '
        'radioLandingCostCst
        '
        Me.radioLandingCostCst.Location = New System.Drawing.Point(3, 3)
        Me.radioLandingCostCst.Name = "radioLandingCostCst"
        Me.radioLandingCostCst.Size = New System.Drawing.Size(109, 18)
        Me.radioLandingCostCst.TabIndex = 1
        Me.radioLandingCostCst.Text = "Landing Cost-CST"
        '
        'radioLandingCostVat
        '
        Me.radioLandingCostVat.Location = New System.Drawing.Point(117, 3)
        Me.radioLandingCostVat.Name = "radioLandingCostVat"
        Me.radioLandingCostVat.Size = New System.Drawing.Size(110, 18)
        Me.radioLandingCostVat.TabIndex = 2
        Me.radioLandingCostVat.Text = "Landing Cost-VAT"
        '
        'radioAll
        '
        Me.radioAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.radioAll.Location = New System.Drawing.Point(413, 3)
        Me.radioAll.Name = "radioAll"
        Me.radioAll.Size = New System.Drawing.Size(33, 18)
        Me.radioAll.TabIndex = 2
        Me.radioAll.TabStop = True
        Me.radioAll.Text = "All"
        Me.radioAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'radioMrpCst
        '
        Me.radioMrpCst.Location = New System.Drawing.Point(246, 3)
        Me.radioMrpCst.Name = "radioMrpCst"
        Me.radioMrpCst.Size = New System.Drawing.Size(67, 18)
        Me.radioMrpCst.TabIndex = 2
        Me.radioMrpCst.Text = "MRP-CST"
        '
        'radioMrpVat
        '
        Me.radioMrpVat.Location = New System.Drawing.Point(330, 3)
        Me.radioMrpVat.Name = "radioMrpVat"
        Me.radioMrpVat.Size = New System.Drawing.Size(68, 18)
        Me.radioMrpVat.TabIndex = 2
        Me.radioMrpVat.Text = "MRP-VAT"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowCellContextMenu = False
        Me.gv1.MasterTemplate.AllowColumnChooser = False
        Me.gv1.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv1.MasterTemplate.AllowColumnReorder = False
        Me.gv1.MasterTemplate.AllowColumnResize = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.AllowRowResize = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.Name = "gv1"
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(867, 408)
        Me.gv1.TabIndex = 1
        Me.gv1.Text = "RadGridView1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.radioReset)
        Me.GroupBox2.Controls.Add(Me.rbtnRefresh)
        Me.GroupBox2.Controls.Add(Me.rbtnClose)
        Me.GroupBox2.Controls.Add(Me.rbtnExport)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(867, 40)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'radioReset
        '
        Me.radioReset.Location = New System.Drawing.Point(12, 18)
        Me.radioReset.Name = "radioReset"
        Me.radioReset.Size = New System.Drawing.Size(94, 19)
        Me.radioReset.TabIndex = 2
        Me.radioReset.Text = "Reset"
        '
        'rbtnRefresh
        '
        Me.rbtnRefresh.Location = New System.Drawing.Point(203, 18)
        Me.rbtnRefresh.Name = "rbtnRefresh"
        Me.rbtnRefresh.Size = New System.Drawing.Size(50, 19)
        Me.rbtnRefresh.TabIndex = 1
        Me.rbtnRefresh.Text = ">>"
        Me.rbtnRefresh.Visible = False
        '
        'rbtnClose
        '
        Me.rbtnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbtnClose.Location = New System.Drawing.Point(778, 18)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(86, 19)
        Me.rbtnClose.TabIndex = 1
        Me.rbtnClose.Text = "Close"
        '
        'rbtnExport
        '
        Me.rbtnExport.Location = New System.Drawing.Point(105, 18)
        Me.rbtnExport.Name = "rbtnExport"
        Me.rbtnExport.Size = New System.Drawing.Size(89, 19)
        Me.rbtnExport.TabIndex = 0
        Me.rbtnExport.Text = "Export"
        '
        'chkBackCalculation
        '
        Me.chkBackCalculation.Location = New System.Drawing.Point(58, 15)
        Me.chkBackCalculation.Name = "chkBackCalculation"
        Me.chkBackCalculation.Size = New System.Drawing.Size(102, 18)
        Me.chkBackCalculation.TabIndex = 5
        Me.chkBackCalculation.Text = "Back Calculation"
        '
        'FrmPriceMasterExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 497)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPriceMasterExport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Price Master Export"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.radioLandingCostCst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioLandingCostVat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioMrpCst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioMrpVat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.radioReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBackCalculation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radioAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radioMrpVat As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radioMrpCst As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radioLandingCostVat As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radioLandingCostCst As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents radioReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkBackCalculation As Telerik.WinControls.UI.RadRadioButton
End Class

