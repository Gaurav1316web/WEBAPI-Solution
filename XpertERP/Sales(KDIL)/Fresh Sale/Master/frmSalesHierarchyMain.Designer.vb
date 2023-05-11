<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesHierarchyMain
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
        Me.dtpApplicableFrom = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblParentStructName = New common.Controls.MyLabel()
        Me.fndParentStructCode = New common.UserControls.txtFinder()
        Me.lblParentStructCode = New common.Controls.MyLabel()
        Me.lbldescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.txtlevelName = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.fndLevelCode = New common.UserControls.txtFinder()
        Me.lblLevel = New common.Controls.MyLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.BtnSave = New Telerik.WinControls.UI.RadButton()
        Me.txtType = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtSubType = New common.Controls.MyTextBox()
        Me.lblSourceDocDesc = New common.Controls.MyLabel()
        Me.fndSource = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParentStructName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParentStructCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlevelName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSourceDocDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSourceDocDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndSource)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSubType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpApplicableFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblParentStructName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndParentStructCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblParentStructCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtlevelName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLevelCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLevel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(590, 362)
        Me.SplitContainer1.SplitterDistance = 318
        Me.SplitContainer1.TabIndex = 0
        '
        'dtpApplicableFrom
        '
        Me.dtpApplicableFrom.CalculationExpression = Nothing
        Me.dtpApplicableFrom.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpApplicableFrom.FieldCode = Nothing
        Me.dtpApplicableFrom.FieldDesc = Nothing
        Me.dtpApplicableFrom.FieldMaxLength = 0
        Me.dtpApplicableFrom.FieldName = Nothing
        Me.dtpApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpApplicableFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApplicableFrom.isCalculatedField = False
        Me.dtpApplicableFrom.IsSourceFromTable = False
        Me.dtpApplicableFrom.IsSourceFromValueList = False
        Me.dtpApplicableFrom.IsUnique = False
        Me.dtpApplicableFrom.Location = New System.Drawing.Point(117, 173)
        Me.dtpApplicableFrom.MendatroryField = False
        Me.dtpApplicableFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.MyLinkLable1 = Me.RadLabel4
        Me.dtpApplicableFrom.MyLinkLable2 = Nothing
        Me.dtpApplicableFrom.Name = "dtpApplicableFrom"
        Me.dtpApplicableFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.ReferenceFieldDesc = Nothing
        Me.dtpApplicableFrom.ReferenceFieldName = Nothing
        Me.dtpApplicableFrom.ReferenceTableName = Nothing
        Me.dtpApplicableFrom.Size = New System.Drawing.Size(126, 18)
        Me.dtpApplicableFrom.TabIndex = 463
        Me.dtpApplicableFrom.TabStop = False
        Me.dtpApplicableFrom.Text = "13/06/2011 11:29 AM"
        Me.dtpApplicableFrom.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(12, 174)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(88, 16)
        Me.RadLabel4.TabIndex = 462
        Me.RadLabel4.Text = "Applicable From"
        '
        'RadButton1
        '
        Me.RadButton1.Image = Global.ERP.My.Resources.Resources._new
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(330, 22)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(15, 20)
        Me.RadButton1.TabIndex = 461
        '
        'lblParentStructName
        '
        Me.lblParentStructName.AutoSize = False
        Me.lblParentStructName.BorderVisible = True
        Me.lblParentStructName.FieldName = Nothing
        Me.lblParentStructName.Location = New System.Drawing.Point(349, 150)
        Me.lblParentStructName.Name = "lblParentStructName"
        Me.lblParentStructName.Size = New System.Drawing.Size(217, 19)
        Me.lblParentStructName.TabIndex = 460
        Me.lblParentStructName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndParentStructCode
        '
        Me.fndParentStructCode.CalculationExpression = Nothing
        Me.fndParentStructCode.FieldCode = Nothing
        Me.fndParentStructCode.FieldDesc = Nothing
        Me.fndParentStructCode.FieldMaxLength = 0
        Me.fndParentStructCode.FieldName = Nothing
        Me.fndParentStructCode.isCalculatedField = False
        Me.fndParentStructCode.IsSourceFromTable = False
        Me.fndParentStructCode.IsSourceFromValueList = False
        Me.fndParentStructCode.IsUnique = False
        Me.fndParentStructCode.Location = New System.Drawing.Point(117, 150)
        Me.fndParentStructCode.MendatroryField = False
        Me.fndParentStructCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndParentStructCode.MyLinkLable1 = Me.lblParentStructCode
        Me.fndParentStructCode.MyLinkLable2 = Nothing
        Me.fndParentStructCode.MyReadOnly = False
        Me.fndParentStructCode.MyShowMasterFormButton = False
        Me.fndParentStructCode.Name = "fndParentStructCode"
        Me.fndParentStructCode.ReferenceFieldDesc = Nothing
        Me.fndParentStructCode.ReferenceFieldName = Nothing
        Me.fndParentStructCode.ReferenceTableName = Nothing
        Me.fndParentStructCode.Size = New System.Drawing.Size(227, 19)
        Me.fndParentStructCode.TabIndex = 458
        Me.fndParentStructCode.Value = ""
        '
        'lblParentStructCode
        '
        Me.lblParentStructCode.FieldName = Nothing
        Me.lblParentStructCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblParentStructCode.Location = New System.Drawing.Point(10, 151)
        Me.lblParentStructCode.Name = "lblParentStructCode"
        Me.lblParentStructCode.Size = New System.Drawing.Size(103, 16)
        Me.lblParentStructCode.TabIndex = 459
        Me.lblParentStructCode.Text = "Parent Struct Code"
        '
        'lbldescription
        '
        Me.lbldescription.FieldName = Nothing
        Me.lbldescription.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbldescription.Location = New System.Drawing.Point(12, 46)
        Me.lbldescription.Name = "lbldescription"
        Me.lbldescription.Size = New System.Drawing.Size(63, 16)
        Me.lbldescription.TabIndex = 457
        Me.lbldescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(117, 46)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lbldescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(447, 18)
        Me.txtDescription.TabIndex = 456
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(12, 26)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(34, 16)
        Me.lblCode.TabIndex = 450
        Me.lblCode.Text = "Code"
        '
        'txtcode
        '
        Me.txtcode.FieldName = Nothing
        Me.txtcode.Location = New System.Drawing.Point(117, 22)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(213, 20)
        Me.txtcode.TabIndex = 448
        Me.txtcode.Value = ""
        '
        'txtlevelName
        '
        Me.txtlevelName.AutoSize = False
        Me.txtlevelName.BorderVisible = True
        Me.txtlevelName.FieldName = Nothing
        Me.txtlevelName.Location = New System.Drawing.Point(348, 67)
        Me.txtlevelName.Name = "txtlevelName"
        Me.txtlevelName.Size = New System.Drawing.Size(217, 19)
        Me.txtlevelName.TabIndex = 455
        Me.txtlevelName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnReset
        '
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(295, 22)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(15, 20)
        Me.btnReset.TabIndex = 449
        '
        'fndLevelCode
        '
        Me.fndLevelCode.CalculationExpression = Nothing
        Me.fndLevelCode.FieldCode = Nothing
        Me.fndLevelCode.FieldDesc = Nothing
        Me.fndLevelCode.FieldMaxLength = 0
        Me.fndLevelCode.FieldName = Nothing
        Me.fndLevelCode.isCalculatedField = False
        Me.fndLevelCode.IsSourceFromTable = False
        Me.fndLevelCode.IsSourceFromValueList = False
        Me.fndLevelCode.IsUnique = False
        Me.fndLevelCode.Location = New System.Drawing.Point(117, 67)
        Me.fndLevelCode.MendatroryField = True
        Me.fndLevelCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLevelCode.MyLinkLable1 = Me.lblLevel
        Me.fndLevelCode.MyLinkLable2 = Nothing
        Me.fndLevelCode.MyReadOnly = False
        Me.fndLevelCode.MyShowMasterFormButton = False
        Me.fndLevelCode.Name = "fndLevelCode"
        Me.fndLevelCode.ReferenceFieldDesc = Nothing
        Me.fndLevelCode.ReferenceFieldName = Nothing
        Me.fndLevelCode.ReferenceTableName = Nothing
        Me.fndLevelCode.Size = New System.Drawing.Size(227, 19)
        Me.fndLevelCode.TabIndex = 453
        Me.fndLevelCode.Value = ""
        '
        'lblLevel
        '
        Me.lblLevel.FieldName = Nothing
        Me.lblLevel.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLevel.Location = New System.Drawing.Point(12, 68)
        Me.lblLevel.Name = "lblLevel"
        Me.lblLevel.Size = New System.Drawing.Size(36, 16)
        Me.lblLevel.TabIndex = 454
        Me.lblLevel.Text = "Level "
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(92, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(76, 20)
        Me.btnDelete.TabIndex = 463
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(511, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 20)
        Me.btnClose.TabIndex = 461
        Me.btnClose.Text = "Close"
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.Location = New System.Drawing.Point(12, 8)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(76, 20)
        Me.BtnSave.TabIndex = 462
        Me.BtnSave.Text = "Save"
        '
        'txtType
        '
        Me.txtType.CalculationExpression = Nothing
        Me.txtType.FieldCode = Nothing
        Me.txtType.FieldDesc = Nothing
        Me.txtType.FieldMaxLength = 0
        Me.txtType.FieldName = Nothing
        Me.txtType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtType.isCalculatedField = False
        Me.txtType.IsSourceFromTable = False
        Me.txtType.IsSourceFromValueList = False
        Me.txtType.IsUnique = False
        Me.txtType.Location = New System.Drawing.Point(117, 89)
        Me.txtType.MaxLength = 100
        Me.txtType.MendatroryField = False
        Me.txtType.MyLinkLable1 = Me.lbldescription
        Me.txtType.MyLinkLable2 = Nothing
        Me.txtType.Name = "txtType"
        Me.txtType.ReadOnly = True
        Me.txtType.ReferenceFieldDesc = Nothing
        Me.txtType.ReferenceFieldName = Nothing
        Me.txtType.ReferenceTableName = Nothing
        Me.txtType.Size = New System.Drawing.Size(227, 18)
        Me.txtType.TabIndex = 464
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(10, 92)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 465
        Me.MyLabel1.Text = "Type"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(10, 111)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel2.TabIndex = 467
        Me.MyLabel2.Text = "Sub Type"
        '
        'txtSubType
        '
        Me.txtSubType.CalculationExpression = Nothing
        Me.txtSubType.FieldCode = Nothing
        Me.txtSubType.FieldDesc = Nothing
        Me.txtSubType.FieldMaxLength = 0
        Me.txtSubType.FieldName = Nothing
        Me.txtSubType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubType.isCalculatedField = False
        Me.txtSubType.IsSourceFromTable = False
        Me.txtSubType.IsSourceFromValueList = False
        Me.txtSubType.IsUnique = False
        Me.txtSubType.Location = New System.Drawing.Point(117, 109)
        Me.txtSubType.MaxLength = 100
        Me.txtSubType.MendatroryField = False
        Me.txtSubType.MyLinkLable1 = Me.lbldescription
        Me.txtSubType.MyLinkLable2 = Nothing
        Me.txtSubType.Name = "txtSubType"
        Me.txtSubType.ReadOnly = True
        Me.txtSubType.ReferenceFieldDesc = Nothing
        Me.txtSubType.ReferenceFieldName = Nothing
        Me.txtSubType.ReferenceTableName = Nothing
        Me.txtSubType.Size = New System.Drawing.Size(227, 18)
        Me.txtSubType.TabIndex = 466
        '
        'lblSourceDocDesc
        '
        Me.lblSourceDocDesc.AutoSize = False
        Me.lblSourceDocDesc.BorderVisible = True
        Me.lblSourceDocDesc.FieldName = Nothing
        Me.lblSourceDocDesc.Location = New System.Drawing.Point(348, 129)
        Me.lblSourceDocDesc.Name = "lblSourceDocDesc"
        Me.lblSourceDocDesc.Size = New System.Drawing.Size(217, 19)
        Me.lblSourceDocDesc.TabIndex = 470
        Me.lblSourceDocDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndSource
        '
        Me.fndSource.CalculationExpression = Nothing
        Me.fndSource.FieldCode = Nothing
        Me.fndSource.FieldDesc = Nothing
        Me.fndSource.FieldMaxLength = 0
        Me.fndSource.FieldName = Nothing
        Me.fndSource.isCalculatedField = False
        Me.fndSource.IsSourceFromTable = False
        Me.fndSource.IsSourceFromValueList = False
        Me.fndSource.IsUnique = False
        Me.fndSource.Location = New System.Drawing.Point(117, 129)
        Me.fndSource.MendatroryField = False
        Me.fndSource.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSource.MyLinkLable1 = Me.MyLabel4
        Me.fndSource.MyLinkLable2 = Nothing
        Me.fndSource.MyReadOnly = False
        Me.fndSource.MyShowMasterFormButton = False
        Me.fndSource.Name = "fndSource"
        Me.fndSource.ReferenceFieldDesc = Nothing
        Me.fndSource.ReferenceFieldName = Nothing
        Me.fndSource.ReferenceTableName = Nothing
        Me.fndSource.Size = New System.Drawing.Size(227, 19)
        Me.fndSource.TabIndex = 468
        Me.fndSource.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(9, 130)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel4.TabIndex = 469
        Me.MyLabel4.Text = "Source Doc No"
        '
        'FrmSalesHierarchyMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 362)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSalesHierarchyMain"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = ""
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParentStructName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParentStructCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlevelName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSourceDocDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents txtlevelName As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndLevelCode As common.UserControls.txtFinder
    Friend WithEvents lblLevel As common.Controls.MyLabel
    Friend WithEvents lbldescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblParentStructName As common.Controls.MyLabel
    Friend WithEvents fndParentStructCode As common.UserControls.txtFinder
    Friend WithEvents lblParentStructCode As common.Controls.MyLabel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpApplicableFrom As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtType As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSubType As common.Controls.MyTextBox
    Friend WithEvents lblSourceDocDesc As common.Controls.MyLabel
    Friend WithEvents fndSource As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

