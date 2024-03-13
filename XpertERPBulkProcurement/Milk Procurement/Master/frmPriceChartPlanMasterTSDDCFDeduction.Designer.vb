<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPriceChartPlanMasterTSDDCFDeduction
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtStart = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.rbtnSame = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnBottomToTop = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtValue = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.rbtnTopToBottom = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnAddition = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnDeduction = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkBeginFromCurrentRow = New Telerik.WinControls.UI.RadCheckBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBottomToTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTopToBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnAddition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBeginFromCurrentRow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 76)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Size = New System.Drawing.Size(571, 349)
        Me.SplitContainer1.SplitterDistance = 317
        Me.SplitContainer1.TabIndex = 2
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(571, 317)
        Me.gv1.TabIndex = 0
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.RadButton2.Location = New System.Drawing.Point(211, 2)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(73, 24)
        Me.RadButton2.TabIndex = 1
        Me.RadButton2.Text = "OK"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.RadButton1.Location = New System.Drawing.Point(286, 2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(73, 24)
        Me.RadButton1.TabIndex = 0
        Me.RadButton1.Text = "Cancel"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(571, 76)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtStart)
        Me.GroupBox2.Controls.Add(Me.chkBeginFromCurrentRow)
        Me.GroupBox2.Controls.Add(Me.rbtnSame)
        Me.GroupBox2.Controls.Add(Me.MyLabel2)
        Me.GroupBox2.Controls.Add(Me.rbtnBottomToTop)
        Me.GroupBox2.Controls.Add(Me.txtValue)
        Me.GroupBox2.Controls.Add(Me.MyLabel1)
        Me.GroupBox2.Controls.Add(Me.rbtnTopToBottom)
        Me.GroupBox2.Controls.Add(Me.btnclose)
        Me.GroupBox2.Location = New System.Drawing.Point(309, 1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(259, 72)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Auto Fill Deduction"
        '
        'txtStart
        '
        Me.txtStart.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStart.CalculationExpression = Nothing
        Me.txtStart.DecimalPlaces = 2
        Me.txtStart.FieldCode = Nothing
        Me.txtStart.FieldDesc = Nothing
        Me.txtStart.FieldMaxLength = 0
        Me.txtStart.FieldName = Nothing
        Me.txtStart.isCalculatedField = False
        Me.txtStart.IsSourceFromTable = False
        Me.txtStart.IsSourceFromValueList = False
        Me.txtStart.IsUnique = False
        Me.txtStart.Location = New System.Drawing.Point(40, 32)
        Me.txtStart.MendatroryField = True
        Me.txtStart.MyLinkLable1 = Me.MyLabel2
        Me.txtStart.MyLinkLable2 = Nothing
        Me.txtStart.Name = "txtStart"
        Me.txtStart.ReferenceFieldDesc = Nothing
        Me.txtStart.ReferenceFieldName = Nothing
        Me.txtStart.ReferenceTableName = Nothing
        Me.txtStart.Size = New System.Drawing.Size(57, 20)
        Me.txtStart.TabIndex = 31
        Me.txtStart.Text = "0"
        Me.txtStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStart.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(6, 34)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 32
        Me.MyLabel2.Text = "Start"
        '
        'rbtnSame
        '
        Me.rbtnSame.Location = New System.Drawing.Point(208, 13)
        Me.rbtnSame.Name = "rbtnSame"
        Me.rbtnSame.Size = New System.Drawing.Size(48, 18)
        Me.rbtnSame.TabIndex = 2
        Me.rbtnSame.TabStop = False
        Me.rbtnSame.Text = "Same"
        '
        'rbtnBottomToTop
        '
        Me.rbtnBottomToTop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnBottomToTop.Location = New System.Drawing.Point(6, 13)
        Me.rbtnBottomToTop.Name = "rbtnBottomToTop"
        Me.rbtnBottomToTop.Size = New System.Drawing.Size(95, 18)
        Me.rbtnBottomToTop.TabIndex = 0
        Me.rbtnBottomToTop.Text = "Bottom To Top"
        Me.rbtnBottomToTop.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtValue
        '
        Me.txtValue.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtValue.CalculationExpression = Nothing
        Me.txtValue.DecimalPlaces = 2
        Me.txtValue.FieldCode = Nothing
        Me.txtValue.FieldDesc = Nothing
        Me.txtValue.FieldMaxLength = 0
        Me.txtValue.FieldName = Nothing
        Me.txtValue.isCalculatedField = False
        Me.txtValue.IsSourceFromTable = False
        Me.txtValue.IsSourceFromValueList = False
        Me.txtValue.IsUnique = False
        Me.txtValue.Location = New System.Drawing.Point(136, 32)
        Me.txtValue.MendatroryField = True
        Me.txtValue.MyLinkLable1 = Me.MyLabel1
        Me.txtValue.MyLinkLable2 = Nothing
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReferenceFieldDesc = Nothing
        Me.txtValue.ReferenceFieldName = Nothing
        Me.txtValue.ReferenceTableName = Nothing
        Me.txtValue.Size = New System.Drawing.Size(57, 20)
        Me.txtValue.TabIndex = 3
        Me.txtValue.Text = "0"
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtValue.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(97, 34)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel1.TabIndex = 30
        Me.MyLabel1.Text = "Value"
        '
        'rbtnTopToBottom
        '
        Me.rbtnTopToBottom.Location = New System.Drawing.Point(110, 13)
        Me.rbtnTopToBottom.Name = "rbtnTopToBottom"
        Me.rbtnTopToBottom.Size = New System.Drawing.Size(95, 18)
        Me.rbtnTopToBottom.TabIndex = 1
        Me.rbtnTopToBottom.TabStop = False
        Me.rbtnTopToBottom.Text = "Top To Bottom"
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(199, 32)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(55, 36)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Apply"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnAddition)
        Me.GroupBox1.Controls.Add(Me.rbtnDeduction)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(92, 59)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SNF Nature"
        '
        'rbtnAddition
        '
        Me.rbtnAddition.Location = New System.Drawing.Point(4, 36)
        Me.rbtnAddition.Name = "rbtnAddition"
        Me.rbtnAddition.Size = New System.Drawing.Size(63, 18)
        Me.rbtnAddition.TabIndex = 22
        Me.rbtnAddition.Text = "Addition"
        '
        'rbtnDeduction
        '
        Me.rbtnDeduction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnDeduction.Location = New System.Drawing.Point(4, 16)
        Me.rbtnDeduction.Name = "rbtnDeduction"
        Me.rbtnDeduction.Size = New System.Drawing.Size(72, 18)
        Me.rbtnDeduction.TabIndex = 21
        Me.rbtnDeduction.Text = "Deduction"
        Me.rbtnDeduction.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkBeginFromCurrentRow
        '
        Me.chkBeginFromCurrentRow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBeginFromCurrentRow.Location = New System.Drawing.Point(6, 53)
        Me.chkBeginFromCurrentRow.Name = "chkBeginFromCurrentRow"
        Me.chkBeginFromCurrentRow.Size = New System.Drawing.Size(135, 16)
        Me.chkBeginFromCurrentRow.TabIndex = 356
        Me.chkBeginFromCurrentRow.Text = "Begin from current row"
        '
        'frmPriceChartPlanMasterTSDDCFDeduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(571, 425)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmPriceChartPlanMasterTSDDCFDeduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SNF Deduction/Addition"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBottomToTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTopToBottom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnAddition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBeginFromCurrentRow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnAddition As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnDeduction As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtValue As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnSame As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnBottomToTop As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnTopToBottom As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtStart As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkBeginFromCurrentRow As RadCheckBox
End Class

