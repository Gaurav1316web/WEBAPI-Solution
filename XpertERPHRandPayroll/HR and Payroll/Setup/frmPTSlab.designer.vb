Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPTSlab
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPTSlab))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblStateDesc = New common.Controls.MyLabel()
        Me.fndState = New common.UserControls.txtFinder()
        Me.lblStateProvince = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.gvOTSlab = New common.UserControls.MyRadGridView()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.dtpApplicableFrom = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStateDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStateProvince, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOTSlab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOTSlab.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(669, 476)
        Me.SplitContainer1.SplitterDistance = 431
        Me.SplitContainer1.TabIndex = 158
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Location = New System.Drawing.Point(9, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpApplicableFrom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblStateDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndState)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblStateProvince)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemCategoryCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvOTSlab)
        Me.SplitContainer2.Size = New System.Drawing.Size(649, 425)
        Me.SplitContainer2.SplitterDistance = 116
        Me.SplitContainer2.TabIndex = 322
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(18, 59)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel1.TabIndex = 183
        Me.MyLabel1.Text = "Applicable From"
        '
        'lblStateDesc
        '
        Me.lblStateDesc.AutoSize = False
        Me.lblStateDesc.BorderVisible = True
        Me.lblStateDesc.FieldName = Nothing
        Me.lblStateDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateDesc.Location = New System.Drawing.Point(338, 79)
        Me.lblStateDesc.Name = "lblStateDesc"
        Me.lblStateDesc.Size = New System.Drawing.Size(297, 20)
        Me.lblStateDesc.TabIndex = 182
        Me.lblStateDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndState
        '
        Me.fndState.CalculationExpression = Nothing
        Me.fndState.FieldCode = Nothing
        Me.fndState.FieldDesc = Nothing
        Me.fndState.FieldMaxLength = 0
        Me.fndState.FieldName = Nothing
        Me.fndState.isCalculatedField = False
        Me.fndState.IsSourceFromTable = False
        Me.fndState.IsSourceFromValueList = False
        Me.fndState.IsUnique = False
        Me.fndState.Location = New System.Drawing.Point(139, 79)
        Me.fndState.MendatroryField = True
        Me.fndState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndState.MyLinkLable1 = Me.lblStateProvince
        Me.fndState.MyLinkLable2 = Nothing
        Me.fndState.MyReadOnly = False
        Me.fndState.MyShowMasterFormButton = False
        Me.fndState.Name = "fndState"
        Me.fndState.ReferenceFieldDesc = Nothing
        Me.fndState.ReferenceFieldName = Nothing
        Me.fndState.ReferenceTableName = Nothing
        Me.fndState.Size = New System.Drawing.Size(193, 19)
        Me.fndState.TabIndex = 180
        Me.fndState.Value = ""
        '
        'lblStateProvince
        '
        Me.lblStateProvince.FieldName = Nothing
        Me.lblStateProvince.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateProvince.Location = New System.Drawing.Point(18, 80)
        Me.lblStateProvince.Name = "lblStateProvince"
        Me.lblStateProvince.Size = New System.Drawing.Size(33, 16)
        Me.lblStateProvince.TabIndex = 181
        Me.lblStateProvince.Text = "State"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
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
        Me.txtDescription.Location = New System.Drawing.Point(139, 33)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(320, 21)
        Me.txtDescription.TabIndex = 2
        Me.txtDescription.Text = " "
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(18, 36)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 176
        Me.lblDescription.Text = "Description"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(419, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(139, 10)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblItemCategoryCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(274, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(18, 15)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(51, 16)
        Me.lblItemCategoryCode.TabIndex = 158
        Me.lblItemCategoryCode.Text = "PT Code"
        '
        'gvOTSlab
        '
        Me.gvOTSlab.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvOTSlab.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvOTSlab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvOTSlab.EnableCustomFiltering = True
        Me.gvOTSlab.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvOTSlab.ForeColor = System.Drawing.Color.Black
        Me.gvOTSlab.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvOTSlab.Location = New System.Drawing.Point(0, 0)
        '
        'gvOTSlab
        '
        Me.gvOTSlab.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvOTSlab.MasterTemplate.AllowAddNewRow = False
        Me.gvOTSlab.MasterTemplate.AutoGenerateColumns = False
        Me.gvOTSlab.MasterTemplate.EnableCustomFiltering = True
        Me.gvOTSlab.MasterTemplate.EnableGrouping = False
        Me.gvOTSlab.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvOTSlab.Name = "gvOTSlab"
        Me.gvOTSlab.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvOTSlab.ShowHeaderCellButtons = True
        Me.gvOTSlab.Size = New System.Drawing.Size(649, 305)
        Me.gvOTSlab.TabIndex = 4
        Me.gvOTSlab.TabStop = False
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 10)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(594, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 10)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'dtpApplicableFrom
        '
        Me.dtpApplicableFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpApplicableFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApplicableFrom.Location = New System.Drawing.Point(139, 56)
        Me.dtpApplicableFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.Name = "dtpApplicableFrom"
        Me.dtpApplicableFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpApplicableFrom.Size = New System.Drawing.Size(140, 20)
        Me.dtpApplicableFrom.TabIndex = 184
        Me.dtpApplicableFrom.TabStop = False
        Me.dtpApplicableFrom.Text = "24/10/2011"
        Me.dtpApplicableFrom.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'frmPTSlab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(669, 476)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPTSlab"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "PT Rule"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStateDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStateProvince, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOTSlab.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOTSlab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvOTSlab As common.UserControls.MyRadGridView
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndState As common.UserControls.txtFinder
    Friend WithEvents lblStateProvince As common.Controls.MyLabel
    Friend WithEvents lblStateDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpApplicableFrom As Telerik.WinControls.UI.RadDateTimePicker
End Class
