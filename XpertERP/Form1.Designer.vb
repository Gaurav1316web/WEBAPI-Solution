<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeCaption
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
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.txtCaption = New common.Controls.MyTextBox()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCustomiseSno = New common.MyNumBox()
        Me.lblProgramCode = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rbtnMove = New System.Windows.Forms.RadioButton()
        Me.rbtnAddNew = New System.Windows.Forms.RadioButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.rbtnNA = New System.Windows.Forms.RadioButton()
        Me.txtModule = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtSubModule = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtNewProgramCode = New common.Controls.MyTextBox()
        Me.lblNwProgramCode = New common.Controls.MyLabel()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCaption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomiseSno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProgramCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewProgramCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNwProgramCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel24.Location = New System.Drawing.Point(8, 6)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel24.TabIndex = 0
        Me.RadLabel24.Text = "Program Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(8, 30)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 2
        Me.MyLabel2.Text = "Description"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(8, 52)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel13.TabIndex = 1
        Me.RadLabel13.Text = "Caption"
        '
        'txtCaption
        '
        Me.txtCaption.CalculationExpression = Nothing
        Me.txtCaption.FieldCode = Nothing
        Me.txtCaption.FieldDesc = Nothing
        Me.txtCaption.FieldMaxLength = 0
        Me.txtCaption.FieldName = Nothing
        Me.txtCaption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCaption.isCalculatedField = False
        Me.txtCaption.IsSourceFromTable = False
        Me.txtCaption.IsSourceFromValueList = False
        Me.txtCaption.IsUnique = False
        Me.txtCaption.Location = New System.Drawing.Point(97, 51)
        Me.txtCaption.MaxLength = 50
        Me.txtCaption.MendatroryField = False
        Me.txtCaption.MyLinkLable1 = Me.RadLabel13
        Me.txtCaption.MyLinkLable2 = Nothing
        Me.txtCaption.Name = "txtCaption"
        Me.txtCaption.ReferenceFieldDesc = Nothing
        Me.txtCaption.ReferenceFieldName = Nothing
        Me.txtCaption.ReferenceTableName = Nothing
        Me.txtCaption.Size = New System.Drawing.Size(277, 18)
        Me.txtCaption.TabIndex = 0
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(153, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(82, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 3)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel1.TabIndex = 9
        Me.MyLabel1.Text = "Customise SNo"
        '
        'txtCustomiseSno
        '
        Me.txtCustomiseSno.CalculationExpression = Nothing
        Me.txtCustomiseSno.DecimalPlaces = 2
        Me.txtCustomiseSno.FieldCode = Nothing
        Me.txtCustomiseSno.FieldDesc = Nothing
        Me.txtCustomiseSno.FieldMaxLength = 0
        Me.txtCustomiseSno.FieldName = Nothing
        Me.txtCustomiseSno.isCalculatedField = False
        Me.txtCustomiseSno.IsSourceFromTable = False
        Me.txtCustomiseSno.IsSourceFromValueList = False
        Me.txtCustomiseSno.IsUnique = False
        Me.txtCustomiseSno.Location = New System.Drawing.Point(98, 3)
        Me.txtCustomiseSno.MaxLength = 3
        Me.txtCustomiseSno.MendatroryField = False
        Me.txtCustomiseSno.MyLinkLable1 = Nothing
        Me.txtCustomiseSno.MyLinkLable2 = Nothing
        Me.txtCustomiseSno.Name = "txtCustomiseSno"
        Me.txtCustomiseSno.ReadOnly = True
        Me.txtCustomiseSno.ReferenceFieldDesc = Nothing
        Me.txtCustomiseSno.ReferenceFieldName = Nothing
        Me.txtCustomiseSno.ReferenceTableName = Nothing
        Me.txtCustomiseSno.Size = New System.Drawing.Size(276, 20)
        Me.txtCustomiseSno.TabIndex = 0
        Me.txtCustomiseSno.Text = "0"
        Me.txtCustomiseSno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCustomiseSno.Value = 0R
        '
        'lblProgramCode
        '
        Me.lblProgramCode.CalculationExpression = Nothing
        Me.lblProgramCode.FieldCode = Nothing
        Me.lblProgramCode.FieldDesc = Nothing
        Me.lblProgramCode.FieldMaxLength = 0
        Me.lblProgramCode.FieldName = Nothing
        Me.lblProgramCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgramCode.isCalculatedField = False
        Me.lblProgramCode.IsSourceFromTable = False
        Me.lblProgramCode.IsSourceFromValueList = False
        Me.lblProgramCode.IsUnique = False
        Me.lblProgramCode.Location = New System.Drawing.Point(97, 5)
        Me.lblProgramCode.MaxLength = 50
        Me.lblProgramCode.MendatroryField = False
        Me.lblProgramCode.MyLinkLable1 = Me.RadLabel13
        Me.lblProgramCode.MyLinkLable2 = Nothing
        Me.lblProgramCode.Name = "lblProgramCode"
        Me.lblProgramCode.ReadOnly = True
        Me.lblProgramCode.ReferenceFieldDesc = Nothing
        Me.lblProgramCode.ReferenceFieldName = Nothing
        Me.lblProgramCode.ReferenceTableName = Nothing
        Me.lblProgramCode.Size = New System.Drawing.Size(277, 18)
        Me.lblProgramCode.TabIndex = 4
        '
        'lblDescription
        '
        Me.lblDescription.CalculationExpression = Nothing
        Me.lblDescription.FieldCode = Nothing
        Me.lblDescription.FieldDesc = Nothing
        Me.lblDescription.FieldMaxLength = 0
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.isCalculatedField = False
        Me.lblDescription.IsSourceFromTable = False
        Me.lblDescription.IsSourceFromValueList = False
        Me.lblDescription.IsUnique = False
        Me.lblDescription.Location = New System.Drawing.Point(97, 29)
        Me.lblDescription.MaxLength = 50
        Me.lblDescription.MendatroryField = False
        Me.lblDescription.MyLinkLable1 = Me.RadLabel13
        Me.lblDescription.MyLinkLable2 = Nothing
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.ReadOnly = True
        Me.lblDescription.ReferenceFieldDesc = Nothing
        Me.lblDescription.ReferenceFieldName = Nothing
        Me.lblDescription.ReferenceTableName = Nothing
        Me.lblDescription.Size = New System.Drawing.Size(277, 18)
        Me.lblDescription.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnPost)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 203)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(375, 32)
        Me.Panel1.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCaption)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblProgramCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel24)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtNewProgramCode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblNwProgramCode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtSubModule)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtModule)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnNA)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnAddNew)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnMove)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtCustomiseSno)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(375, 203)
        Me.SplitContainer1.SplitterDistance = 74
        Me.SplitContainer1.TabIndex = 0
        '
        'rbtnMove
        '
        Me.rbtnMove.AutoSize = True
        Me.rbtnMove.Location = New System.Drawing.Point(144, 26)
        Me.rbtnMove.Name = "rbtnMove"
        Me.rbtnMove.Size = New System.Drawing.Size(53, 17)
        Me.rbtnMove.TabIndex = 2
        Me.rbtnMove.Text = "Move"
        Me.rbtnMove.UseVisualStyleBackColor = True
        '
        'rbtnAddNew
        '
        Me.rbtnAddNew.AutoSize = True
        Me.rbtnAddNew.Location = New System.Drawing.Point(203, 26)
        Me.rbtnAddNew.Name = "rbtnAddNew"
        Me.rbtnAddNew.Size = New System.Drawing.Size(120, 17)
        Me.rbtnAddNew.TabIndex = 3
        Me.rbtnAddNew.Text = "Add As New Menu"
        Me.rbtnAddNew.UseVisualStyleBackColor = True
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(8, 27)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel3.TabIndex = 8
        Me.MyLabel3.Text = "Operation"
        '
        'rbtnNA
        '
        Me.rbtnNA.AutoSize = True
        Me.rbtnNA.Checked = True
        Me.rbtnNA.Location = New System.Drawing.Point(98, 27)
        Me.rbtnNA.Name = "rbtnNA"
        Me.rbtnNA.Size = New System.Drawing.Size(40, 17)
        Me.rbtnNA.TabIndex = 1
        Me.rbtnNA.TabStop = True
        Me.rbtnNA.Text = "NA"
        Me.rbtnNA.UseVisualStyleBackColor = True
        '
        'txtModule
        '
        Me.txtModule.CalculationExpression = Nothing
        Me.txtModule.FieldCode = Nothing
        Me.txtModule.FieldDesc = Nothing
        Me.txtModule.FieldMaxLength = 0
        Me.txtModule.FieldName = Nothing
        Me.txtModule.isCalculatedField = False
        Me.txtModule.IsSourceFromTable = False
        Me.txtModule.IsSourceFromValueList = False
        Me.txtModule.IsUnique = False
        Me.txtModule.Location = New System.Drawing.Point(96, 48)
        Me.txtModule.MendatroryField = False
        Me.txtModule.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModule.MyLinkLable1 = Nothing
        Me.txtModule.MyLinkLable2 = Nothing
        Me.txtModule.MyReadOnly = False
        Me.txtModule.MyShowMasterFormButton = False
        Me.txtModule.Name = "txtModule"
        Me.txtModule.ReferenceFieldDesc = Nothing
        Me.txtModule.ReferenceFieldName = Nothing
        Me.txtModule.ReferenceTableName = Nothing
        Me.txtModule.Size = New System.Drawing.Size(273, 19)
        Me.txtModule.TabIndex = 4
        Me.txtModule.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(8, 49)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel6.TabIndex = 7
        Me.MyLabel6.Text = "Module"
        '
        'txtSubModule
        '
        Me.txtSubModule.CalculationExpression = Nothing
        Me.txtSubModule.FieldCode = Nothing
        Me.txtSubModule.FieldDesc = Nothing
        Me.txtSubModule.FieldMaxLength = 0
        Me.txtSubModule.FieldName = Nothing
        Me.txtSubModule.isCalculatedField = False
        Me.txtSubModule.IsSourceFromTable = False
        Me.txtSubModule.IsSourceFromValueList = False
        Me.txtSubModule.IsUnique = False
        Me.txtSubModule.Location = New System.Drawing.Point(96, 70)
        Me.txtSubModule.MendatroryField = False
        Me.txtSubModule.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubModule.MyLinkLable1 = Nothing
        Me.txtSubModule.MyLinkLable2 = Nothing
        Me.txtSubModule.MyReadOnly = False
        Me.txtSubModule.MyShowMasterFormButton = False
        Me.txtSubModule.Name = "txtSubModule"
        Me.txtSubModule.ReferenceFieldDesc = Nothing
        Me.txtSubModule.ReferenceFieldName = Nothing
        Me.txtSubModule.ReferenceTableName = Nothing
        Me.txtSubModule.Size = New System.Drawing.Size(273, 19)
        Me.txtSubModule.TabIndex = 5
        Me.txtSubModule.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(8, 71)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel4.TabIndex = 6
        Me.MyLabel4.Text = "Sub Module"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(223, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(69, 22)
        Me.RadButton1.TabIndex = 2
        Me.RadButton1.Text = "Advance"
        '
        'txtNewProgramCode
        '
        Me.txtNewProgramCode.CalculationExpression = Nothing
        Me.txtNewProgramCode.FieldCode = Nothing
        Me.txtNewProgramCode.FieldDesc = Nothing
        Me.txtNewProgramCode.FieldMaxLength = 0
        Me.txtNewProgramCode.FieldName = Nothing
        Me.txtNewProgramCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewProgramCode.isCalculatedField = False
        Me.txtNewProgramCode.IsSourceFromTable = False
        Me.txtNewProgramCode.IsSourceFromValueList = False
        Me.txtNewProgramCode.IsUnique = False
        Me.txtNewProgramCode.Location = New System.Drawing.Point(96, 93)
        Me.txtNewProgramCode.MaxLength = 12
        Me.txtNewProgramCode.MendatroryField = False
        Me.txtNewProgramCode.MyLinkLable1 = Me.lblNwProgramCode
        Me.txtNewProgramCode.MyLinkLable2 = Nothing
        Me.txtNewProgramCode.Name = "txtNewProgramCode"
        Me.txtNewProgramCode.ReferenceFieldDesc = Nothing
        Me.txtNewProgramCode.ReferenceFieldName = Nothing
        Me.txtNewProgramCode.ReferenceTableName = Nothing
        Me.txtNewProgramCode.Size = New System.Drawing.Size(273, 18)
        Me.txtNewProgramCode.TabIndex = 10
        '
        'lblNwProgramCode
        '
        Me.lblNwProgramCode.FieldName = Nothing
        Me.lblNwProgramCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNwProgramCode.Location = New System.Drawing.Point(8, 94)
        Me.lblNwProgramCode.Name = "lblNwProgramCode"
        Me.lblNwProgramCode.Size = New System.Drawing.Size(79, 16)
        Me.lblNwProgramCode.TabIndex = 11
        Me.lblNwProgramCode.Text = "New Program "
        '
        'frmChangeCaption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 235)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmChangeCaption"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(0, 0)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Caption"
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCaption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomiseSno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProgramCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewProgramCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNwProgramCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadLabel24 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents txtCaption As common.Controls.MyTextBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustomiseSno As common.MyNumBox
    Friend WithEvents lblProgramCode As common.Controls.MyTextBox
    Friend WithEvents lblDescription As common.Controls.MyTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents rbtnNA As RadioButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents rbtnAddNew As RadioButton
    Friend WithEvents rbtnMove As RadioButton
    Friend WithEvents txtSubModule As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtModule As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents txtNewProgramCode As common.Controls.MyTextBox
    Friend WithEvents lblNwProgramCode As common.Controls.MyLabel
End Class
