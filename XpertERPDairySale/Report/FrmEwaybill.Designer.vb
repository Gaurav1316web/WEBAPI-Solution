<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEwaybill
    Inherits FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cmbEwaybillType = New common.Controls.MyComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtewbno = New common.Controls.MyTextBox()
        Me.lblewbno = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cmbEwaybillType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtewbno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblewbno, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtewbno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblewbno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbEwaybillType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 0
        '
        'cmbEwaybillType
        '
        Me.cmbEwaybillType.AutoCompleteDisplayMember = Nothing
        Me.cmbEwaybillType.AutoCompleteValueMember = Nothing
        Me.cmbEwaybillType.CalculationExpression = Nothing
        Me.cmbEwaybillType.DropDownAnimationEnabled = True
        Me.cmbEwaybillType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbEwaybillType.FieldCode = Nothing
        Me.cmbEwaybillType.FieldDesc = Nothing
        Me.cmbEwaybillType.FieldMaxLength = 0
        Me.cmbEwaybillType.FieldName = Nothing
        Me.cmbEwaybillType.isCalculatedField = False
        Me.cmbEwaybillType.IsSourceFromTable = False
        Me.cmbEwaybillType.IsSourceFromValueList = False
        Me.cmbEwaybillType.IsUnique = False
        Me.cmbEwaybillType.Location = New System.Drawing.Point(141, 9)
        Me.cmbEwaybillType.MendatroryField = True
        Me.cmbEwaybillType.MyLinkLable1 = Nothing
        Me.cmbEwaybillType.MyLinkLable2 = Nothing
        Me.cmbEwaybillType.Name = "cmbEwaybillType"
        Me.cmbEwaybillType.ReferenceFieldDesc = Nothing
        Me.cmbEwaybillType.ReferenceFieldName = Nothing
        Me.cmbEwaybillType.ReferenceTableName = Nothing
        Me.cmbEwaybillType.Size = New System.Drawing.Size(236, 20)
        Me.cmbEwaybillType.TabIndex = 1474
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "E-Way Bill Service Type"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(93, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(70, 19)
        Me.btnGo.TabIndex = 89
        Me.btnGo.Text = "Go >>"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(714, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 19)
        Me.btnclose.TabIndex = 88
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(17, 7)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(70, 19)
        Me.btnreset.TabIndex = 87
        Me.btnreset.Text = "Reset"
        '
        'txtewbno
        '
        Me.txtewbno.CalculationExpression = Nothing
        Me.txtewbno.FieldCode = Nothing
        Me.txtewbno.FieldDesc = Nothing
        Me.txtewbno.FieldMaxLength = 0
        Me.txtewbno.FieldName = Nothing
        Me.txtewbno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtewbno.isCalculatedField = False
        Me.txtewbno.IsSourceFromTable = False
        Me.txtewbno.IsSourceFromValueList = False
        Me.txtewbno.IsUnique = False
        Me.txtewbno.Location = New System.Drawing.Point(126, 66)
        Me.txtewbno.MaxLength = 200
        Me.txtewbno.MendatroryField = False
        Me.txtewbno.MyLinkLable1 = Nothing
        Me.txtewbno.MyLinkLable2 = Nothing
        Me.txtewbno.Name = "txtewbno"
        Me.txtewbno.ReadOnly = True
        Me.txtewbno.ReferenceFieldDesc = Nothing
        Me.txtewbno.ReferenceFieldName = Nothing
        Me.txtewbno.ReferenceTableName = Nothing
        Me.txtewbno.Size = New System.Drawing.Size(125, 18)
        Me.txtewbno.TabIndex = 1476
        '
        'lblewbno
        '
        Me.lblewbno.FieldName = Nothing
        Me.lblewbno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblewbno.Location = New System.Drawing.Point(27, 67)
        Me.lblewbno.Name = "lblewbno"
        Me.lblewbno.Size = New System.Drawing.Size(73, 16)
        Me.lblewbno.TabIndex = 1475
        Me.lblewbno.Text = "Eway Bill No."
        '
        'FrmEwaybill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmEwaybill"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Eway Bill API"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cmbEwaybillType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtewbno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblewbno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Label1 As Label
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnreset As RadButton
    Friend WithEvents cmbEwaybillType As common.Controls.MyComboBox
    Friend WithEvents txtewbno As common.Controls.MyTextBox
    Friend WithEvents lblewbno As common.Controls.MyLabel
End Class
