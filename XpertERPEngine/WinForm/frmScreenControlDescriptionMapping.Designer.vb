<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenControlDescriptionMapping
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.lblDesc = New common.Controls.MyLabel()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.txtControlType = New common.Controls.MyTextBox()
        Me.lblControl = New common.Controls.MyLabel()
        Me.txtControlName = New common.Controls.MyTextBox()
        Me.txtScreenDesc = New common.Controls.MyTextBox()
        Me.lblScreen = New common.Controls.MyLabel()
        Me.txtScreenCode = New common.Controls.MyTextBox()
        Me.lblbacc = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndFieldName = New common.UserControls.txtFinder()
        Me.fndReferenceTable = New common.UserControls.txtFinder()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtControlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtControlName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScreenDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScreen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScreenCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbacc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(441, 110)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 22
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 110)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 21
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(1, 110)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 20
        Me.btnSave.Text = "Save"
        '
        'lblDesc
        '
        Me.lblDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.Location = New System.Drawing.Point(3, 45)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblDesc.TabIndex = 19
        Me.lblDesc.Text = "Description"
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(67, 44)
        Me.txtdesc.MaxLength = 50
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.lblDesc
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(419, 18)
        Me.txtdesc.TabIndex = 18
        '
        'txtControlType
        '
        Me.txtControlType.CalculationExpression = Nothing
        Me.txtControlType.FieldCode = Nothing
        Me.txtControlType.FieldDesc = Nothing
        Me.txtControlType.FieldMaxLength = 0
        Me.txtControlType.FieldName = Nothing
        Me.txtControlType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtControlType.isCalculatedField = False
        Me.txtControlType.IsSourceFromTable = False
        Me.txtControlType.IsSourceFromValueList = False
        Me.txtControlType.IsUnique = False
        Me.txtControlType.Location = New System.Drawing.Point(203, 24)
        Me.txtControlType.MaxLength = 50
        Me.txtControlType.MendatroryField = False
        Me.txtControlType.MyLinkLable1 = Me.lblControl
        Me.txtControlType.MyLinkLable2 = Nothing
        Me.txtControlType.Name = "txtControlType"
        Me.txtControlType.ReadOnly = True
        Me.txtControlType.ReferenceFieldDesc = Nothing
        Me.txtControlType.ReferenceFieldName = Nothing
        Me.txtControlType.ReferenceTableName = Nothing
        Me.txtControlType.Size = New System.Drawing.Size(283, 18)
        Me.txtControlType.TabIndex = 17
        '
        'lblControl
        '
        Me.lblControl.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControl.Location = New System.Drawing.Point(3, 24)
        Me.lblControl.Name = "lblControl"
        Me.lblControl.Size = New System.Drawing.Size(43, 16)
        Me.lblControl.TabIndex = 16
        Me.lblControl.Text = "Control"
        '
        'txtControlName
        '
        Me.txtControlName.CalculationExpression = Nothing
        Me.txtControlName.FieldCode = Nothing
        Me.txtControlName.FieldDesc = Nothing
        Me.txtControlName.FieldMaxLength = 0
        Me.txtControlName.FieldName = Nothing
        Me.txtControlName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtControlName.isCalculatedField = False
        Me.txtControlName.IsSourceFromTable = False
        Me.txtControlName.IsSourceFromValueList = False
        Me.txtControlName.IsUnique = False
        Me.txtControlName.Location = New System.Drawing.Point(67, 24)
        Me.txtControlName.MaxLength = 50
        Me.txtControlName.MendatroryField = False
        Me.txtControlName.MyLinkLable1 = Me.lblControl
        Me.txtControlName.MyLinkLable2 = Nothing
        Me.txtControlName.Name = "txtControlName"
        Me.txtControlName.ReadOnly = True
        Me.txtControlName.ReferenceFieldDesc = Nothing
        Me.txtControlName.ReferenceFieldName = Nothing
        Me.txtControlName.ReferenceTableName = Nothing
        Me.txtControlName.Size = New System.Drawing.Size(133, 18)
        Me.txtControlName.TabIndex = 15
        '
        'txtScreenDesc
        '
        Me.txtScreenDesc.CalculationExpression = Nothing
        Me.txtScreenDesc.FieldCode = Nothing
        Me.txtScreenDesc.FieldDesc = Nothing
        Me.txtScreenDesc.FieldMaxLength = 0
        Me.txtScreenDesc.FieldName = Nothing
        Me.txtScreenDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScreenDesc.isCalculatedField = False
        Me.txtScreenDesc.IsSourceFromTable = False
        Me.txtScreenDesc.IsSourceFromValueList = False
        Me.txtScreenDesc.IsUnique = False
        Me.txtScreenDesc.Location = New System.Drawing.Point(204, 3)
        Me.txtScreenDesc.MaxLength = 50
        Me.txtScreenDesc.MendatroryField = False
        Me.txtScreenDesc.MyLinkLable1 = Me.lblScreen
        Me.txtScreenDesc.MyLinkLable2 = Nothing
        Me.txtScreenDesc.Name = "txtScreenDesc"
        Me.txtScreenDesc.ReadOnly = True
        Me.txtScreenDesc.ReferenceFieldDesc = Nothing
        Me.txtScreenDesc.ReferenceFieldName = Nothing
        Me.txtScreenDesc.ReferenceTableName = Nothing
        Me.txtScreenDesc.Size = New System.Drawing.Size(283, 18)
        Me.txtScreenDesc.TabIndex = 14
        '
        'lblScreen
        '
        Me.lblScreen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScreen.Location = New System.Drawing.Point(4, 3)
        Me.lblScreen.Name = "lblScreen"
        Me.lblScreen.Size = New System.Drawing.Size(42, 16)
        Me.lblScreen.TabIndex = 13
        Me.lblScreen.Text = "Screen"
        '
        'txtScreenCode
        '
        Me.txtScreenCode.CalculationExpression = Nothing
        Me.txtScreenCode.FieldCode = Nothing
        Me.txtScreenCode.FieldDesc = Nothing
        Me.txtScreenCode.FieldMaxLength = 0
        Me.txtScreenCode.FieldName = Nothing
        Me.txtScreenCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScreenCode.isCalculatedField = False
        Me.txtScreenCode.IsSourceFromTable = False
        Me.txtScreenCode.IsSourceFromValueList = False
        Me.txtScreenCode.IsUnique = False
        Me.txtScreenCode.Location = New System.Drawing.Point(68, 3)
        Me.txtScreenCode.MaxLength = 50
        Me.txtScreenCode.MendatroryField = False
        Me.txtScreenCode.MyLinkLable1 = Me.lblScreen
        Me.txtScreenCode.MyLinkLable2 = Nothing
        Me.txtScreenCode.Name = "txtScreenCode"
        Me.txtScreenCode.ReadOnly = True
        Me.txtScreenCode.ReferenceFieldDesc = Nothing
        Me.txtScreenCode.ReferenceFieldName = Nothing
        Me.txtScreenCode.ReferenceTableName = Nothing
        Me.txtScreenCode.Size = New System.Drawing.Size(133, 18)
        Me.txtScreenCode.TabIndex = 12
        '
        'lblbacc
        '
        Me.lblbacc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbacc.Location = New System.Drawing.Point(3, 68)
        Me.lblbacc.Name = "lblbacc"
        Me.lblbacc.Size = New System.Drawing.Size(35, 16)
        Me.lblbacc.TabIndex = 24
        Me.lblbacc.Text = "Table"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 89)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel2.TabIndex = 26
        Me.MyLabel2.Text = "Field"
        '
        'fndFieldName
        '
        Me.fndFieldName.CalculationExpression = Nothing
        Me.fndFieldName.FieldCode = Nothing
        Me.fndFieldName.FieldDesc = Nothing
        Me.fndFieldName.FieldMaxLength = 0
        Me.fndFieldName.FieldName = Nothing
        Me.fndFieldName.isCalculatedField = False
        Me.fndFieldName.IsSourceFromTable = False
        Me.fndFieldName.IsSourceFromValueList = False
        Me.fndFieldName.IsUnique = False
        Me.fndFieldName.Location = New System.Drawing.Point(67, 88)
        Me.fndFieldName.MendatroryField = True
        Me.fndFieldName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFieldName.MyLinkLable1 = Me.MyLabel2
        Me.fndFieldName.MyLinkLable2 = Nothing
        Me.fndFieldName.MyReadOnly = False
        Me.fndFieldName.MyShowMasterFormButton = False
        Me.fndFieldName.Name = "fndFieldName"
        Me.fndFieldName.ReferenceFieldDesc = Nothing
        Me.fndFieldName.ReferenceFieldName = Nothing
        Me.fndFieldName.ReferenceTableName = Nothing
        Me.fndFieldName.Size = New System.Drawing.Size(417, 18)
        Me.fndFieldName.TabIndex = 25
        Me.fndFieldName.Value = ""
        '
        'fndReferenceTable
        '
        Me.fndReferenceTable.CalculationExpression = Nothing
        Me.fndReferenceTable.FieldCode = Nothing
        Me.fndReferenceTable.FieldDesc = Nothing
        Me.fndReferenceTable.FieldMaxLength = 0
        Me.fndReferenceTable.FieldName = Nothing
        Me.fndReferenceTable.isCalculatedField = False
        Me.fndReferenceTable.IsSourceFromTable = False
        Me.fndReferenceTable.IsSourceFromValueList = False
        Me.fndReferenceTable.IsUnique = False
        Me.fndReferenceTable.Location = New System.Drawing.Point(67, 66)
        Me.fndReferenceTable.MendatroryField = True
        Me.fndReferenceTable.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndReferenceTable.MyLinkLable1 = Me.lblbacc
        Me.fndReferenceTable.MyLinkLable2 = Nothing
        Me.fndReferenceTable.MyReadOnly = False
        Me.fndReferenceTable.MyShowMasterFormButton = False
        Me.fndReferenceTable.Name = "fndReferenceTable"
        Me.fndReferenceTable.ReferenceFieldDesc = Nothing
        Me.fndReferenceTable.ReferenceFieldName = Nothing
        Me.fndReferenceTable.ReferenceTableName = Nothing
        Me.fndReferenceTable.Size = New System.Drawing.Size(417, 18)
        Me.fndReferenceTable.TabIndex = 23
        Me.fndReferenceTable.Value = ""
        '
        'frmScreenControlDescriptionMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 134)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblbacc)
        Me.Controls.Add(Me.MyLabel2)
        Me.Controls.Add(Me.fndFieldName)
        Me.Controls.Add(Me.fndReferenceTable)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.txtdesc)
        Me.Controls.Add(Me.txtControlType)
        Me.Controls.Add(Me.lblControl)
        Me.Controls.Add(Me.txtControlName)
        Me.Controls.Add(Me.txtScreenDesc)
        Me.Controls.Add(Me.lblScreen)
        Me.Controls.Add(Me.txtScreenCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmScreenControlDescriptionMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ShowInTaskbar = False
        Me.ShowItemToolTips = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control's Description Mapping"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtControlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtControlName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScreenDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScreen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScreenCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbacc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblScreen As common.Controls.MyLabel
    Friend WithEvents txtScreenCode As common.Controls.MyTextBox
    Friend WithEvents txtScreenDesc As common.Controls.MyTextBox
    Friend WithEvents txtControlType As common.Controls.MyTextBox
    Friend WithEvents lblControl As common.Controls.MyLabel
    Friend WithEvents txtControlName As common.Controls.MyTextBox
    Friend WithEvents lblDesc As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblbacc As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndFieldName As common.UserControls.txtFinder
    Friend WithEvents fndReferenceTable As common.UserControls.txtFinder
End Class
