<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPendingPI
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gvHead = New common.UserControls.MyRadGridView()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnOk = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.tp_ToDate = New common.Controls.MyDateTimePicker()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.tp_FromDate = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvHead.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tp_ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tp_FromDate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadLabel12)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOk)
        Me.SplitContainer1.Size = New System.Drawing.Size(1025, 476)
        Me.SplitContainer1.SplitterDistance = 443
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1025, 443)
        Me.SplitContainer2.SplitterDistance = 222
        Me.SplitContainer2.TabIndex = 0
        '
        'gvHead
        '
        Me.gvHead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvHead.Location = New System.Drawing.Point(0, 0)
        '
        'gvHead
        '
        Me.gvHead.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvHead.Name = "gvHead"
        Me.gvHead.ShowHeaderCellButtons = True
        Me.gvHead.Size = New System.Drawing.Size(1015, 179)
        Me.gvHead.TabIndex = 0
        Me.gvHead.Text = "RadGridView1"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1025, 217)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(659, 10)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(359, 16)
        Me.RadLabel12.TabIndex = 26
        Me.RadLabel12.Text = "Double click on SRN No to select/Unselect All Items of Same SRN"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.Location = New System.Drawing.Point(515, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(130, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Esc : Cancel"
        '
        'btnOk
        '
        Me.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOk.Location = New System.Drawing.Point(379, 2)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(130, 24)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "F5 : OK"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.gvHead)
        Me.Panel1.Location = New System.Drawing.Point(3, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1015, 179)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tp_FromDate)
        Me.GroupBox1.Controls.Add(Me.btnGo)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.tp_ToDate)
        Me.GroupBox1.Controls.Add(Me.RadLabel4)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1022, 31)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(9, 9)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel4.TabIndex = 30
        Me.RadLabel4.Text = "From Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(172, 9)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 32
        Me.MyLabel1.Text = "To Date"
        '
        'tp_ToDate
        '
        Me.tp_ToDate.CalculationExpression = Nothing
        Me.tp_ToDate.CustomFormat = "dd/MM/yyyy"
        Me.tp_ToDate.FieldCode = Nothing
        Me.tp_ToDate.FieldDesc = Nothing
        Me.tp_ToDate.FieldMaxLength = 0
        Me.tp_ToDate.FieldName = Nothing
        Me.tp_ToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.tp_ToDate.isCalculatedField = False
        Me.tp_ToDate.IsSourceFromTable = False
        Me.tp_ToDate.IsSourceFromValueList = False
        Me.tp_ToDate.IsUnique = False
        Me.tp_ToDate.Location = New System.Drawing.Point(242, 8)
        Me.tp_ToDate.MendatroryField = False
        Me.tp_ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.tp_ToDate.MyLinkLable1 = Me.MyLabel1
        Me.tp_ToDate.MyLinkLable2 = Nothing
        Me.tp_ToDate.Name = "tp_ToDate"
        Me.tp_ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.tp_ToDate.ReferenceFieldDesc = Nothing
        Me.tp_ToDate.ReferenceFieldName = Nothing
        Me.tp_ToDate.ReferenceTableName = Nothing
        Me.tp_ToDate.Size = New System.Drawing.Size(77, 18)
        Me.tp_ToDate.TabIndex = 31
        Me.tp_ToDate.TabStop = False
        Me.tp_ToDate.Text = "13/06/2011"
        Me.tp_ToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(346, 6)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(69, 22)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = ">>>"
        '
        'tp_FromDate
        '
        Me.tp_FromDate.CalculationExpression = Nothing
        Me.tp_FromDate.CustomFormat = "dd/MM/yyyy"
        Me.tp_FromDate.FieldCode = Nothing
        Me.tp_FromDate.FieldDesc = Nothing
        Me.tp_FromDate.FieldMaxLength = 0
        Me.tp_FromDate.FieldName = Nothing
        Me.tp_FromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.tp_FromDate.isCalculatedField = False
        Me.tp_FromDate.IsSourceFromTable = False
        Me.tp_FromDate.IsSourceFromValueList = False
        Me.tp_FromDate.IsUnique = False
        Me.tp_FromDate.Location = New System.Drawing.Point(75, 10)
        Me.tp_FromDate.MendatroryField = False
        Me.tp_FromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.tp_FromDate.MyLinkLable1 = Me.MyLabel1
        Me.tp_FromDate.MyLinkLable2 = Nothing
        Me.tp_FromDate.Name = "tp_FromDate"
        Me.tp_FromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.tp_FromDate.ReferenceFieldDesc = Nothing
        Me.tp_FromDate.ReferenceFieldName = Nothing
        Me.tp_FromDate.ReferenceTableName = Nothing
        Me.tp_FromDate.Size = New System.Drawing.Size(77, 18)
        Me.tp_FromDate.TabIndex = 33
        Me.tp_FromDate.TabStop = False
        Me.tp_FromDate.Text = "13/06/2011"
        Me.tp_FromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'frmPendingPI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 476)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "frmPendingPI"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Pending Purchase Invoice to Return"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gvHead.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tp_ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tp_FromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvHead As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents tp_ToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents tp_FromDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
End Class

