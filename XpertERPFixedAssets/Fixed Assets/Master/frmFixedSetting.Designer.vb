Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedSetting
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblRoundType = New common.Controls.MyLabel()
        Me.cboRoundType = New common.Controls.MyComboBox()
        Me.txtDecimal = New common.MyNumBox()
        Me.lblDecimal = New common.Controls.MyLabel()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblRoundType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboRoundType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDecimal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDecimal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(811, 616)
        Me.SplitContainer1.SplitterDistance = 581
        Me.SplitContainer1.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblRoundType)
        Me.RadGroupBox1.Controls.Add(Me.cboRoundType)
        Me.RadGroupBox1.Controls.Add(Me.txtDecimal)
        Me.RadGroupBox1.Controls.Add(Me.lblDecimal)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(811, 581)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblRoundType
        '
        Me.lblRoundType.FieldName = Nothing
        Me.lblRoundType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoundType.Location = New System.Drawing.Point(19, 25)
        Me.lblRoundType.Name = "lblRoundType"
        Me.lblRoundType.Size = New System.Drawing.Size(68, 16)
        Me.lblRoundType.TabIndex = 45
        Me.lblRoundType.Text = "Round Type"
        '
        'cboRoundType
        '
        Me.cboRoundType.AutoCompleteDisplayMember = Nothing
        Me.cboRoundType.AutoCompleteValueMember = Nothing
        Me.cboRoundType.CalculationExpression = Nothing
        Me.cboRoundType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboRoundType.FieldCode = Nothing
        Me.cboRoundType.FieldDesc = Nothing
        Me.cboRoundType.FieldMaxLength = 0
        Me.cboRoundType.FieldName = Nothing
        Me.cboRoundType.isCalculatedField = False
        Me.cboRoundType.IsSourceFromTable = False
        Me.cboRoundType.IsSourceFromValueList = False
        Me.cboRoundType.IsUnique = False
        Me.cboRoundType.Location = New System.Drawing.Point(93, 25)
        Me.cboRoundType.MendatroryField = True
        Me.cboRoundType.MyLinkLable1 = Me.lblRoundType
        Me.cboRoundType.MyLinkLable2 = Nothing
        Me.cboRoundType.Name = "cboRoundType"
        Me.cboRoundType.ReferenceFieldDesc = Nothing
        Me.cboRoundType.ReferenceFieldName = Nothing
        Me.cboRoundType.ReferenceTableName = Nothing
        Me.cboRoundType.Size = New System.Drawing.Size(121, 20)
        Me.cboRoundType.TabIndex = 44
        '
        'txtDecimal
        '
        Me.txtDecimal.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDecimal.CalculationExpression = Nothing
        Me.txtDecimal.DecimalPlaces = 2
        Me.txtDecimal.FieldCode = Nothing
        Me.txtDecimal.FieldDesc = Nothing
        Me.txtDecimal.FieldMaxLength = 0
        Me.txtDecimal.FieldName = Nothing
        Me.txtDecimal.isCalculatedField = False
        Me.txtDecimal.IsSourceFromTable = False
        Me.txtDecimal.IsSourceFromValueList = False
        Me.txtDecimal.IsUnique = False
        Me.txtDecimal.Location = New System.Drawing.Point(273, 25)
        Me.txtDecimal.MendatroryField = False
        Me.txtDecimal.MyLinkLable1 = Nothing
        Me.txtDecimal.MyLinkLable2 = Nothing
        Me.txtDecimal.Name = "txtDecimal"
        Me.txtDecimal.ReferenceFieldDesc = Nothing
        Me.txtDecimal.ReferenceFieldName = Nothing
        Me.txtDecimal.ReferenceTableName = Nothing
        Me.txtDecimal.Size = New System.Drawing.Size(73, 20)
        Me.txtDecimal.TabIndex = 39
        Me.txtDecimal.Text = "1"
        Me.txtDecimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDecimal.Value = 1.0R
        '
        'lblDecimal
        '
        Me.lblDecimal.FieldName = Nothing
        Me.lblDecimal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDecimal.Location = New System.Drawing.Point(220, 27)
        Me.lblDecimal.Name = "lblDecimal"
        Me.lblDecimal.Size = New System.Drawing.Size(47, 16)
        Me.lblDecimal.TabIndex = 38
        Me.lblDecimal.Text = "Decimal"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(12, 8)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(711, 9)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'FrmFixedSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(811, 616)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmFixedSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmFixedSetting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblRoundType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboRoundType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDecimal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDecimal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblRoundType As common.Controls.MyLabel
    Friend WithEvents cboRoundType As common.Controls.MyComboBox
    Friend WithEvents txtDecimal As common.MyNumBox
    Friend WithEvents lblDecimal As common.Controls.MyLabel
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
End Class

