<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPartNoMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPartNoMaster))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtBrand = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblBrand = New common.Controls.MyLabel()
        Me.txtReleasedDate = New common.Controls.MyDateTimePicker()
        Me.txttype = New common.Controls.MyTextBox()
        Me.txtReleasedBy = New common.Controls.MyTextBox()
        Me.txtSubPart = New common.Controls.MyTextBox()
        Me.lblSubPart = New common.Controls.MyLabel()
        Me.lblReleasedDate = New common.Controls.MyLabel()
        Me.lblReleasedBy = New common.Controls.MyLabel()
        Me.lblType = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.txtPartNo = New common.UserControls.txtNavigator()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtBrand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBrand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReleasedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReleasedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubPart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubPart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReleasedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReleasedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(538, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExport, Me.btnImport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'btnExport
        '
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export"
        '
        'btnImport
        '
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Text = "Import"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBrand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBrand)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReleasedDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txttype)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReleasedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSubPart)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSubPart)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReleasedDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReleasedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPartNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(538, 342)
        Me.SplitContainer1.SplitterDistance = 306
        Me.SplitContainer1.TabIndex = 1
        '
        'txtBrand
        '
        Me.txtBrand.CalculationExpression = Nothing
        Me.txtBrand.FieldCode = Nothing
        Me.txtBrand.FieldDesc = Nothing
        Me.txtBrand.FieldMaxLength = 0
        Me.txtBrand.FieldName = Nothing
        Me.txtBrand.isCalculatedField = False
        Me.txtBrand.IsSourceFromTable = False
        Me.txtBrand.IsSourceFromValueList = False
        Me.txtBrand.IsUnique = False
        Me.txtBrand.Location = New System.Drawing.Point(94, 62)
        Me.txtBrand.MaxLength = 200
        Me.txtBrand.MendatroryField = False
        Me.txtBrand.MyLinkLable1 = Me.MyLabel1
        Me.txtBrand.MyLinkLable2 = Nothing
        Me.txtBrand.Name = "txtBrand"
        Me.txtBrand.ReferenceFieldDesc = Nothing
        Me.txtBrand.ReferenceFieldName = Nothing
        Me.txtBrand.ReferenceTableName = Nothing
        Me.txtBrand.Size = New System.Drawing.Size(414, 20)
        Me.txtBrand.TabIndex = 19
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 40)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "Description"
        '
        'lblBrand
        '
        Me.lblBrand.FieldName = Nothing
        Me.lblBrand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrand.Location = New System.Drawing.Point(12, 62)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(36, 16)
        Me.lblBrand.TabIndex = 18
        Me.lblBrand.Text = "Brand"
        '
        'txtReleasedDate
        '
        Me.txtReleasedDate.CalculationExpression = Nothing
        Me.txtReleasedDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReleasedDate.FieldCode = Nothing
        Me.txtReleasedDate.FieldDesc = Nothing
        Me.txtReleasedDate.FieldMaxLength = 0
        Me.txtReleasedDate.FieldName = Nothing
        Me.txtReleasedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReleasedDate.isCalculatedField = False
        Me.txtReleasedDate.IsSourceFromTable = False
        Me.txtReleasedDate.IsSourceFromValueList = False
        Me.txtReleasedDate.IsUnique = False
        Me.txtReleasedDate.Location = New System.Drawing.Point(422, 108)
        Me.txtReleasedDate.MendatroryField = False
        Me.txtReleasedDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReleasedDate.MyLinkLable1 = Nothing
        Me.txtReleasedDate.MyLinkLable2 = Nothing
        Me.txtReleasedDate.Name = "txtReleasedDate"
        Me.txtReleasedDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReleasedDate.ReferenceFieldDesc = Nothing
        Me.txtReleasedDate.ReferenceFieldName = Nothing
        Me.txtReleasedDate.ReferenceTableName = Nothing
        Me.txtReleasedDate.Size = New System.Drawing.Size(86, 20)
        Me.txtReleasedDate.TabIndex = 17
        Me.txtReleasedDate.TabStop = False
        Me.txtReleasedDate.Text = "16/11/2011"
        Me.txtReleasedDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'txttype
        '
        Me.txttype.CalculationExpression = Nothing
        Me.txttype.FieldCode = Nothing
        Me.txttype.FieldDesc = Nothing
        Me.txttype.FieldMaxLength = 0
        Me.txttype.FieldName = Nothing
        Me.txttype.isCalculatedField = False
        Me.txttype.IsSourceFromTable = False
        Me.txttype.IsSourceFromValueList = False
        Me.txttype.IsUnique = False
        Me.txttype.Location = New System.Drawing.Point(94, 84)
        Me.txttype.MaxLength = 200
        Me.txttype.MendatroryField = False
        Me.txttype.MyLinkLable1 = Me.MyLabel1
        Me.txttype.MyLinkLable2 = Nothing
        Me.txttype.Name = "txttype"
        Me.txttype.ReferenceFieldDesc = Nothing
        Me.txttype.ReferenceFieldName = Nothing
        Me.txttype.ReferenceTableName = Nothing
        Me.txttype.Size = New System.Drawing.Size(414, 20)
        Me.txttype.TabIndex = 16
        '
        'txtReleasedBy
        '
        Me.txtReleasedBy.CalculationExpression = Nothing
        Me.txtReleasedBy.FieldCode = Nothing
        Me.txtReleasedBy.FieldDesc = Nothing
        Me.txtReleasedBy.FieldMaxLength = 0
        Me.txtReleasedBy.FieldName = Nothing
        Me.txtReleasedBy.isCalculatedField = False
        Me.txtReleasedBy.IsSourceFromTable = False
        Me.txtReleasedBy.IsSourceFromValueList = False
        Me.txtReleasedBy.IsUnique = False
        Me.txtReleasedBy.Location = New System.Drawing.Point(94, 106)
        Me.txtReleasedBy.MaxLength = 200
        Me.txtReleasedBy.MendatroryField = False
        Me.txtReleasedBy.MyLinkLable1 = Me.MyLabel1
        Me.txtReleasedBy.MyLinkLable2 = Nothing
        Me.txtReleasedBy.Name = "txtReleasedBy"
        Me.txtReleasedBy.ReferenceFieldDesc = Nothing
        Me.txtReleasedBy.ReferenceFieldName = Nothing
        Me.txtReleasedBy.ReferenceTableName = Nothing
        Me.txtReleasedBy.Size = New System.Drawing.Size(239, 20)
        Me.txtReleasedBy.TabIndex = 15
        '
        'txtSubPart
        '
        Me.txtSubPart.CalculationExpression = Nothing
        Me.txtSubPart.FieldCode = Nothing
        Me.txtSubPart.FieldDesc = Nothing
        Me.txtSubPart.FieldMaxLength = 0
        Me.txtSubPart.FieldName = Nothing
        Me.txtSubPart.isCalculatedField = False
        Me.txtSubPart.IsSourceFromTable = False
        Me.txtSubPart.IsSourceFromValueList = False
        Me.txtSubPart.IsUnique = False
        Me.txtSubPart.Location = New System.Drawing.Point(94, 130)
        Me.txtSubPart.MaxLength = 200
        Me.txtSubPart.MendatroryField = False
        Me.txtSubPart.MyLinkLable1 = Me.MyLabel1
        Me.txtSubPart.MyLinkLable2 = Nothing
        Me.txtSubPart.Name = "txtSubPart"
        Me.txtSubPart.ReferenceFieldDesc = Nothing
        Me.txtSubPart.ReferenceFieldName = Nothing
        Me.txtSubPart.ReferenceTableName = Nothing
        Me.txtSubPart.Size = New System.Drawing.Size(414, 20)
        Me.txtSubPart.TabIndex = 13
        '
        'lblSubPart
        '
        Me.lblSubPart.FieldName = Nothing
        Me.lblSubPart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubPart.Location = New System.Drawing.Point(12, 130)
        Me.lblSubPart.Name = "lblSubPart"
        Me.lblSubPart.Size = New System.Drawing.Size(50, 16)
        Me.lblSubPart.TabIndex = 12
        Me.lblSubPart.Text = "Sub Part"
        '
        'lblReleasedDate
        '
        Me.lblReleasedDate.FieldName = Nothing
        Me.lblReleasedDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReleasedDate.Location = New System.Drawing.Point(340, 110)
        Me.lblReleasedDate.Name = "lblReleasedDate"
        Me.lblReleasedDate.Size = New System.Drawing.Size(81, 16)
        Me.lblReleasedDate.TabIndex = 11
        Me.lblReleasedDate.Text = "Released Date"
        '
        'lblReleasedBy
        '
        Me.lblReleasedBy.FieldName = Nothing
        Me.lblReleasedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReleasedBy.Location = New System.Drawing.Point(12, 106)
        Me.lblReleasedBy.Name = "lblReleasedBy"
        Me.lblReleasedBy.Size = New System.Drawing.Size(70, 16)
        Me.lblReleasedBy.TabIndex = 10
        Me.lblReleasedBy.Text = "Released By"
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(12, 84)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 16)
        Me.lblType.TabIndex = 9
        Me.lblType.Text = "Type"
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
        Me.txtDesc.Location = New System.Drawing.Point(94, 40)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.MyLabel1
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(414, 20)
        Me.txtDesc.TabIndex = 1
        '
        'txtPartNo
        '
        Me.txtPartNo.FieldName = Nothing
        Me.txtPartNo.Location = New System.Drawing.Point(93, 16)
        Me.txtPartNo.MendatroryField = True
        Me.txtPartNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtPartNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtPartNo.MyLinkLable1 = Me.lblLocation
        Me.txtPartNo.MyLinkLable2 = Nothing
        Me.txtPartNo.MyMaxLength = 100
        Me.txtPartNo.MyReadOnly = False
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.Size = New System.Drawing.Size(270, 21)
        Me.txtPartNo.TabIndex = 5
        Me.txtPartNo.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(12, 16)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(48, 16)
        Me.lblLocation.TabIndex = 4
        Me.lblLocation.Text = "Part No."
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(362, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(455, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 22)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(82, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(75, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(75, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(160, 5)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(75, 22)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "History"
        '
        'FrmPartNoMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 362)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmPartNoMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPartNoMaster"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtBrand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBrand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReleasedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReleasedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubPart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubPart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReleasedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReleasedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtPartNo As common.UserControls.txtNavigator
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txttype As common.Controls.MyTextBox
    Friend WithEvents txtReleasedBy As common.Controls.MyTextBox
    Friend WithEvents txtSubPart As common.Controls.MyTextBox
    Friend WithEvents lblSubPart As common.Controls.MyLabel
    Friend WithEvents lblReleasedDate As common.Controls.MyLabel
    Friend WithEvents lblReleasedBy As common.Controls.MyLabel
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents txtReleasedDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtBrand As common.Controls.MyTextBox
    Friend WithEvents lblBrand As common.Controls.MyLabel
    Friend WithEvents btnHistory As RadButton
End Class

