<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCapexMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCapexMaster))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblcurrentBudget = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.NumIncBudget = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblIncBudget = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txt_revisionno = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.txt_tolerence = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txt_budget = New common.MyNumBox()
        Me.txt_revisedbudget = New common.MyNumBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.chkProvisional = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcurrentBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumIncBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_revisionno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_tolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_budget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_revisedbudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProvisional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(679, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Export"
        Me.RadMenuItem3.AccessibleName = "Export"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Close"
        Me.RadMenuItem4.AccessibleName = "Close"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkProvisional)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblcurrentBudget)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.NumIncBudget)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIncBudget)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_revisionno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel28)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_tolerence)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_budget)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_revisedbudget)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(679, 253)
        Me.SplitContainer1.SplitterDistance = 198
        Me.SplitContainer1.TabIndex = 1
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(546, 15)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.MyLabel12
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(84, 20)
        Me.txtdate.TabIndex = 205
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "13/06/2011"
        Me.txtdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(510, 16)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel12.TabIndex = 206
        Me.MyLabel12.Text = "Date"
        '
        'lblcurrentBudget
        '
        Me.lblcurrentBudget.AutoSize = False
        Me.lblcurrentBudget.BorderVisible = True
        Me.lblcurrentBudget.FieldName = Nothing
        Me.lblcurrentBudget.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcurrentBudget.Location = New System.Drawing.Point(144, 87)
        Me.lblcurrentBudget.Name = "lblcurrentBudget"
        Me.lblcurrentBudget.Size = New System.Drawing.Size(126, 20)
        Me.lblcurrentBudget.TabIndex = 204
        Me.lblcurrentBudget.Text = "0"
        Me.lblcurrentBudget.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 86)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(83, 18)
        Me.MyLabel1.TabIndex = 203
        Me.MyLabel1.Text = "Current Budget"
        '
        'NumIncBudget
        '
        Me.NumIncBudget.BackColor = System.Drawing.Color.White
        Me.NumIncBudget.CalculationExpression = Nothing
        Me.NumIncBudget.DecimalPlaces = 2
        Me.NumIncBudget.FieldCode = Nothing
        Me.NumIncBudget.FieldDesc = Nothing
        Me.NumIncBudget.FieldMaxLength = 100
        Me.NumIncBudget.FieldName = Nothing
        Me.NumIncBudget.isCalculatedField = False
        Me.NumIncBudget.IsSourceFromTable = False
        Me.NumIncBudget.IsSourceFromValueList = False
        Me.NumIncBudget.IsUnique = False
        Me.NumIncBudget.Location = New System.Drawing.Point(389, 61)
        Me.NumIncBudget.MendatroryField = False
        Me.NumIncBudget.MyLinkLable1 = Me.MyLabel2
        Me.NumIncBudget.MyLinkLable2 = Nothing
        Me.NumIncBudget.Name = "NumIncBudget"
        Me.NumIncBudget.ReferenceFieldDesc = Nothing
        Me.NumIncBudget.ReferenceFieldName = Nothing
        Me.NumIncBudget.ReferenceTableName = Nothing
        Me.NumIncBudget.Size = New System.Drawing.Size(115, 20)
        Me.NumIncBudget.TabIndex = 202
        Me.NumIncBudget.Text = "0"
        Me.NumIncBudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumIncBudget.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(279, 88)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel2.TabIndex = 197
        Me.MyLabel2.Text = "Revised Budget"
        '
        'lblIncBudget
        '
        Me.lblIncBudget.FieldName = Nothing
        Me.lblIncBudget.Location = New System.Drawing.Point(279, 62)
        Me.lblIncBudget.Name = "lblIncBudget"
        Me.lblIncBudget.Size = New System.Drawing.Size(104, 18)
        Me.lblIncBudget.TabIndex = 201
        Me.lblIncBudget.Text = "Incremental Budget"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(14, 140)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel5.TabIndex = 199
        Me.MyLabel5.Text = "Revision No"
        '
        'txt_revisionno
        '
        Me.txt_revisionno.AutoSize = False
        Me.txt_revisionno.BorderVisible = True
        Me.txt_revisionno.FieldName = Nothing
        Me.txt_revisionno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_revisionno.Location = New System.Drawing.Point(144, 137)
        Me.txt_revisionno.Name = "txt_revisionno"
        Me.txt_revisionno.Size = New System.Drawing.Size(126, 20)
        Me.txt_revisionno.TabIndex = 200
        Me.txt_revisionno.Text = "0"
        Me.txt_revisionno.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Location = New System.Drawing.Point(12, 64)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel28.TabIndex = 194
        Me.MyLabel28.Text = "Org. Budget"
        '
        'txt_tolerence
        '
        Me.txt_tolerence.BackColor = System.Drawing.Color.White
        Me.txt_tolerence.CalculationExpression = Nothing
        Me.txt_tolerence.DecimalPlaces = 2
        Me.txt_tolerence.FieldCode = Nothing
        Me.txt_tolerence.FieldDesc = Nothing
        Me.txt_tolerence.FieldMaxLength = 100
        Me.txt_tolerence.FieldName = Nothing
        Me.txt_tolerence.isCalculatedField = False
        Me.txt_tolerence.IsSourceFromTable = False
        Me.txt_tolerence.IsSourceFromValueList = False
        Me.txt_tolerence.IsUnique = False
        Me.txt_tolerence.Location = New System.Drawing.Point(144, 111)
        Me.txt_tolerence.MendatroryField = False
        Me.txt_tolerence.MyLinkLable1 = Me.MyLabel3
        Me.txt_tolerence.MyLinkLable2 = Nothing
        Me.txt_tolerence.Name = "txt_tolerence"
        Me.txt_tolerence.ReferenceFieldDesc = Nothing
        Me.txt_tolerence.ReferenceFieldName = Nothing
        Me.txt_tolerence.ReferenceTableName = Nothing
        Me.txt_tolerence.Size = New System.Drawing.Size(129, 20)
        Me.txt_tolerence.TabIndex = 195
        Me.txt_tolerence.Text = "0"
        Me.txt_tolerence.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_tolerence.Value = 0.0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 112)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(74, 18)
        Me.MyLabel3.TabIndex = 198
        Me.MyLabel3.Text = "Tolerence (%)"
        '
        'txt_budget
        '
        Me.txt_budget.BackColor = System.Drawing.Color.White
        Me.txt_budget.CalculationExpression = Nothing
        Me.txt_budget.DecimalPlaces = 2
        Me.txt_budget.FieldCode = Nothing
        Me.txt_budget.FieldDesc = Nothing
        Me.txt_budget.FieldMaxLength = 100
        Me.txt_budget.FieldName = Nothing
        Me.txt_budget.isCalculatedField = False
        Me.txt_budget.IsSourceFromTable = False
        Me.txt_budget.IsSourceFromValueList = False
        Me.txt_budget.IsUnique = False
        Me.txt_budget.Location = New System.Drawing.Point(144, 62)
        Me.txt_budget.MendatroryField = True
        Me.txt_budget.MyLinkLable1 = Me.MyLabel28
        Me.txt_budget.MyLinkLable2 = Nothing
        Me.txt_budget.Name = "txt_budget"
        Me.txt_budget.ReferenceFieldDesc = Nothing
        Me.txt_budget.ReferenceFieldName = Nothing
        Me.txt_budget.ReferenceTableName = Nothing
        Me.txt_budget.Size = New System.Drawing.Size(129, 20)
        Me.txt_budget.TabIndex = 193
        Me.txt_budget.Text = "0"
        Me.txt_budget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_budget.Value = 0.0R
        '
        'txt_revisedbudget
        '
        Me.txt_revisedbudget.BackColor = System.Drawing.Color.White
        Me.txt_revisedbudget.CalculationExpression = Nothing
        Me.txt_revisedbudget.DecimalPlaces = 2
        Me.txt_revisedbudget.FieldCode = Nothing
        Me.txt_revisedbudget.FieldDesc = Nothing
        Me.txt_revisedbudget.FieldMaxLength = 100
        Me.txt_revisedbudget.FieldName = Nothing
        Me.txt_revisedbudget.isCalculatedField = False
        Me.txt_revisedbudget.IsSourceFromTable = False
        Me.txt_revisedbudget.IsSourceFromValueList = False
        Me.txt_revisedbudget.IsUnique = False
        Me.txt_revisedbudget.Location = New System.Drawing.Point(375, 86)
        Me.txt_revisedbudget.MendatroryField = False
        Me.txt_revisedbudget.MyLinkLable1 = Me.MyLabel2
        Me.txt_revisedbudget.MyLinkLable2 = Nothing
        Me.txt_revisedbudget.Name = "txt_revisedbudget"
        Me.txt_revisedbudget.ReadOnly = True
        Me.txt_revisedbudget.ReferenceFieldDesc = Nothing
        Me.txt_revisedbudget.ReferenceFieldName = Nothing
        Me.txt_revisedbudget.ReferenceTableName = Nothing
        Me.txt_revisedbudget.Size = New System.Drawing.Size(128, 20)
        Me.txt_revisedbudget.TabIndex = 196
        Me.txt_revisedbudget.Text = "0"
        Me.txt_revisedbudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_revisedbudget.Value = 0.0R
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(490, 17)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 5
        Me.btnNew.Text = " "
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(144, 39)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(359, 20)
        Me.txtDesc.TabIndex = 7
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(12, 40)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel2.TabIndex = 6
        Me.RadLabel2.Text = "Description"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(144, 16)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(345, 21)
        Me.txtCode.TabIndex = 3
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(12, 17)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 4
        Me.RadLabel1.Text = "Code"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(4, 29)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(75, 29)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(607, 29)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'chkProvisional
        '
        Me.chkProvisional.AccessibleDescription = ""
        Me.chkProvisional.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProvisional.Location = New System.Drawing.Point(510, 43)
        Me.chkProvisional.Name = "chkProvisional"
        Me.chkProvisional.Size = New System.Drawing.Size(76, 16)
        Me.chkProvisional.TabIndex = 207
        Me.chkProvisional.Text = "Provisional"
        '
        'FrmCapexMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 273)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCapexMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCapexMaster"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcurrentBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumIncBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_revisionno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_tolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_budget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_revisedbudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProvisional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents NumIncBudget As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblIncBudget As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txt_revisionno As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents txt_tolerence As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txt_budget As common.MyNumBox
    Friend WithEvents txt_revisedbudget As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblcurrentBudget As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents chkProvisional As Telerik.WinControls.UI.RadCheckBox
End Class

