<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExpressionEditor
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.fndFields = New common.UserControls.txtFinder()
        Me.lblField = New common.Controls.MyLabel()
        Me.lblbaccno = New common.Controls.MyLabel()
        Me.txtOperatorsLiterals = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtExpression = New common.Controls.MyTextBox()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnAddToExpression1 = New Telerik.WinControls.UI.RadButton()
        Me.btnAddToExpressionTo = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnClearExpression = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblField, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbaccno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOperatorsLiterals, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpression, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddToExpression1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddToExpressionTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClearExpression, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fndFields
        '
        Me.fndFields.CalculationExpression = Nothing
        Me.fndFields.FieldCode = Nothing
        Me.fndFields.FieldDesc = Nothing
        Me.fndFields.FieldMaxLength = 0
        Me.fndFields.FieldName = Nothing
        Me.fndFields.isCalculatedField = False
        Me.fndFields.IsSourceFromTable = False
        Me.fndFields.IsSourceFromValueList = False
        Me.fndFields.IsUnique = False
        Me.fndFields.Location = New System.Drawing.Point(130, 11)
        Me.fndFields.MendatroryField = True
        Me.fndFields.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFields.MyLinkLable1 = Me.lblField
        Me.fndFields.MyLinkLable2 = Nothing
        Me.fndFields.MyReadOnly = False
        Me.fndFields.MyShowMasterFormButton = False
        Me.fndFields.Name = "fndFields"
        Me.fndFields.ReferenceFieldDesc = Nothing
        Me.fndFields.ReferenceFieldName = Nothing
        Me.fndFields.ReferenceTableName = Nothing
        Me.fndFields.Size = New System.Drawing.Size(336, 18)
        Me.fndFields.TabIndex = 10
        Me.fndFields.Value = ""
        '
        'lblField
        '
        Me.lblField.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblField.Location = New System.Drawing.Point(9, 13)
        Me.lblField.Name = "lblField"
        Me.lblField.Size = New System.Drawing.Size(36, 16)
        Me.lblField.TabIndex = 11
        Me.lblField.Text = "Fields"
        '
        'lblbaccno
        '
        Me.lblbaccno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbaccno.Location = New System.Drawing.Point(10, 36)
        Me.lblbaccno.Name = "lblbaccno"
        Me.lblbaccno.Size = New System.Drawing.Size(96, 16)
        Me.lblbaccno.TabIndex = 14
        Me.lblbaccno.Text = "Operators/Literals"
        '
        'txtOperatorsLiterals
        '
        Me.txtOperatorsLiterals.AutoSize = False
        Me.txtOperatorsLiterals.CalculationExpression = Nothing
        Me.txtOperatorsLiterals.FieldCode = Nothing
        Me.txtOperatorsLiterals.FieldDesc = Nothing
        Me.txtOperatorsLiterals.FieldMaxLength = 0
        Me.txtOperatorsLiterals.FieldName = Nothing
        Me.txtOperatorsLiterals.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOperatorsLiterals.isCalculatedField = False
        Me.txtOperatorsLiterals.IsSourceFromTable = False
        Me.txtOperatorsLiterals.IsSourceFromValueList = False
        Me.txtOperatorsLiterals.IsUnique = False
        Me.txtOperatorsLiterals.Location = New System.Drawing.Point(130, 33)
        Me.txtOperatorsLiterals.MaxLength = 29
        Me.txtOperatorsLiterals.MendatroryField = False
        Me.txtOperatorsLiterals.Multiline = True
        Me.txtOperatorsLiterals.MyLinkLable1 = Me.lblbaccno
        Me.txtOperatorsLiterals.MyLinkLable2 = Nothing
        Me.txtOperatorsLiterals.Name = "txtOperatorsLiterals"
        Me.txtOperatorsLiterals.ReferenceFieldDesc = Nothing
        Me.txtOperatorsLiterals.ReferenceFieldName = Nothing
        Me.txtOperatorsLiterals.ReferenceTableName = Nothing
        Me.txtOperatorsLiterals.Size = New System.Drawing.Size(336, 76)
        Me.txtOperatorsLiterals.TabIndex = 13
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 141)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel1.TabIndex = 16
        Me.MyLabel1.Text = "Expression"
        '
        'txtExpression
        '
        Me.txtExpression.AutoSize = False
        Me.txtExpression.CalculationExpression = Nothing
        Me.txtExpression.Enabled = False
        Me.txtExpression.FieldCode = Nothing
        Me.txtExpression.FieldDesc = Nothing
        Me.txtExpression.FieldMaxLength = 0
        Me.txtExpression.FieldName = Nothing
        Me.txtExpression.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpression.isCalculatedField = False
        Me.txtExpression.IsSourceFromTable = False
        Me.txtExpression.IsSourceFromValueList = False
        Me.txtExpression.IsUnique = False
        Me.txtExpression.Location = New System.Drawing.Point(130, 115)
        Me.txtExpression.MaxLength = 29
        Me.txtExpression.MendatroryField = True
        Me.txtExpression.Multiline = True
        Me.txtExpression.MyLinkLable1 = Me.MyLabel1
        Me.txtExpression.MyLinkLable2 = Nothing
        Me.txtExpression.Name = "txtExpression"
        Me.txtExpression.ReferenceFieldDesc = Nothing
        Me.txtExpression.ReferenceFieldName = Nothing
        Me.txtExpression.ReferenceTableName = Nothing
        Me.txtExpression.Size = New System.Drawing.Size(336, 123)
        Me.txtExpression.TabIndex = 15
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 254)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 17
        Me.btnSave.Text = "Done"
        '
        'btnAddToExpression1
        '
        Me.btnAddToExpression1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddToExpression1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddToExpression1.Location = New System.Drawing.Point(467, 12)
        Me.btnAddToExpression1.Name = "btnAddToExpression1"
        Me.btnAddToExpression1.Size = New System.Drawing.Size(109, 18)
        Me.btnAddToExpression1.TabIndex = 18
        Me.btnAddToExpression1.Text = "Add To Expression"
        '
        'btnAddToExpressionTo
        '
        Me.btnAddToExpressionTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddToExpressionTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddToExpressionTo.Location = New System.Drawing.Point(467, 63)
        Me.btnAddToExpressionTo.Name = "btnAddToExpressionTo"
        Me.btnAddToExpressionTo.Size = New System.Drawing.Size(109, 18)
        Me.btnAddToExpressionTo.TabIndex = 19
        Me.btnAddToExpressionTo.Text = "Add To Expression"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(557, 254)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 20
        Me.btnClose.Text = "Close"
        '
        'btnClearExpression
        '
        Me.btnClearExpression.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClearExpression.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearExpression.Location = New System.Drawing.Point(472, 128)
        Me.btnClearExpression.Name = "btnClearExpression"
        Me.btnClearExpression.Size = New System.Drawing.Size(109, 18)
        Me.btnClearExpression.TabIndex = 21
        Me.btnClearExpression.Text = "Clear Expression"
        '
        'frmExpressionEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(626, 273)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnClearExpression)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAddToExpressionTo)
        Me.Controls.Add(Me.btnAddToExpression1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.MyLabel1)
        Me.Controls.Add(Me.txtExpression)
        Me.Controls.Add(Me.lblbaccno)
        Me.Controls.Add(Me.txtOperatorsLiterals)
        Me.Controls.Add(Me.fndFields)
        Me.Controls.Add(Me.lblField)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExpressionEditor"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Expression Editor"
        CType(Me.lblField, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbaccno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOperatorsLiterals, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpression, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddToExpression1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddToExpressionTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClearExpression, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fndFields As common.UserControls.txtFinder
    Friend WithEvents lblField As common.Controls.MyLabel
    Friend WithEvents lblbaccno As common.Controls.MyLabel
    Friend WithEvents txtOperatorsLiterals As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtExpression As common.Controls.MyTextBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAddToExpression1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAddToExpressionTo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClearExpression As Telerik.WinControls.UI.RadButton
End Class
