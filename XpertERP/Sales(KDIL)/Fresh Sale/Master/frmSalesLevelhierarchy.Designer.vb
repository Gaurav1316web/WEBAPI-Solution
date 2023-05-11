<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesLevelhierarchy
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cboSubType = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtSeqNo = New common.MyNumBox()
        Me.lblSeqNo = New common.Controls.MyLabel()
        Me.chkIsLastLevel = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIsFirstLevel = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.cboLevelType = New common.Controls.MyComboBox()
        Me.lblLevelType = New common.Controls.MyLabel()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cboSubType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSeqNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSeqNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsLastLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsFirstLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLevelType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevelType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboSubType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSeqNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSeqNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkIsLastLevel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkIsFirstLevel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboLevelType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLevelType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1135, 423)
        Me.SplitContainer1.SplitterDistance = 389
        Me.SplitContainer1.TabIndex = 0
        '
        'cboSubType
        '
        Me.cboSubType.AutoCompleteDisplayMember = Nothing
        Me.cboSubType.AutoCompleteValueMember = Nothing
        Me.cboSubType.CalculationExpression = Nothing
        Me.cboSubType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSubType.FieldCode = Nothing
        Me.cboSubType.FieldDesc = Nothing
        Me.cboSubType.FieldMaxLength = 0
        Me.cboSubType.FieldName = Nothing
        Me.cboSubType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSubType.isCalculatedField = False
        Me.cboSubType.IsSourceFromTable = False
        Me.cboSubType.IsSourceFromValueList = False
        Me.cboSubType.IsUnique = False
        RadListDataItem1.Text = "Sales Person"
        RadListDataItem2.Text = "Sales Region"
        Me.cboSubType.Items.Add(RadListDataItem1)
        Me.cboSubType.Items.Add(RadListDataItem2)
        Me.cboSubType.Location = New System.Drawing.Point(110, 56)
        Me.cboSubType.MendatroryField = False
        Me.cboSubType.MyLinkLable1 = Me.MyLabel1
        Me.cboSubType.MyLinkLable2 = Nothing
        Me.cboSubType.Name = "cboSubType"
        Me.cboSubType.ReferenceFieldDesc = Nothing
        Me.cboSubType.ReferenceFieldName = Nothing
        Me.cboSubType.ReferenceTableName = Nothing
        Me.cboSubType.Size = New System.Drawing.Size(282, 18)
        Me.cboSubType.TabIndex = 422
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(16, 55)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel1.TabIndex = 423
        Me.MyLabel1.Text = "Sub Type"
        '
        'txtSeqNo
        '
        Me.txtSeqNo.BackColor = System.Drawing.Color.White
        Me.txtSeqNo.CalculationExpression = Nothing
        Me.txtSeqNo.DecimalPlaces = 2
        Me.txtSeqNo.FieldCode = Nothing
        Me.txtSeqNo.FieldDesc = Nothing
        Me.txtSeqNo.FieldMaxLength = 0
        Me.txtSeqNo.FieldName = Nothing
        Me.txtSeqNo.isCalculatedField = False
        Me.txtSeqNo.IsSourceFromTable = False
        Me.txtSeqNo.IsSourceFromValueList = False
        Me.txtSeqNo.IsUnique = False
        Me.txtSeqNo.Location = New System.Drawing.Point(110, 140)
        Me.txtSeqNo.MendatroryField = False
        Me.txtSeqNo.MyLinkLable1 = Nothing
        Me.txtSeqNo.MyLinkLable2 = Nothing
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.ReferenceFieldDesc = Nothing
        Me.txtSeqNo.ReferenceFieldName = Nothing
        Me.txtSeqNo.ReferenceTableName = Nothing
        Me.txtSeqNo.Size = New System.Drawing.Size(116, 20)
        Me.txtSeqNo.TabIndex = 420
        Me.txtSeqNo.Text = "0"
        Me.txtSeqNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSeqNo.Value = 0.0R
        '
        'lblSeqNo
        '
        Me.lblSeqNo.FieldName = Nothing
        Me.lblSeqNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeqNo.Location = New System.Drawing.Point(16, 140)
        Me.lblSeqNo.Name = "lblSeqNo"
        Me.lblSeqNo.Size = New System.Drawing.Size(47, 16)
        Me.lblSeqNo.TabIndex = 421
        Me.lblSeqNo.Text = "Seq No."
        '
        'chkIsLastLevel
        '
        Me.chkIsLastLevel.Location = New System.Drawing.Point(218, 116)
        Me.chkIsLastLevel.Name = "chkIsLastLevel"
        Me.chkIsLastLevel.Size = New System.Drawing.Size(79, 18)
        Me.chkIsLastLevel.TabIndex = 419
        Me.chkIsLastLevel.Text = "Is Last Level"
        '
        'chkIsFirstLevel
        '
        Me.chkIsFirstLevel.Location = New System.Drawing.Point(111, 116)
        Me.chkIsFirstLevel.Name = "chkIsFirstLevel"
        Me.chkIsFirstLevel.Size = New System.Drawing.Size(80, 18)
        Me.chkIsFirstLevel.TabIndex = 418
        Me.chkIsFirstLevel.Text = "Is First Level"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(15, 75)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 416
        Me.lblDescription.Text = "Description"
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
        Me.txtDescription.Location = New System.Drawing.Point(110, 77)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(282, 33)
        Me.txtDescription.TabIndex = 417
        '
        'cboLevelType
        '
        Me.cboLevelType.AutoCompleteDisplayMember = Nothing
        Me.cboLevelType.AutoCompleteValueMember = Nothing
        Me.cboLevelType.CalculationExpression = Nothing
        Me.cboLevelType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLevelType.FieldCode = Nothing
        Me.cboLevelType.FieldDesc = Nothing
        Me.cboLevelType.FieldMaxLength = 0
        Me.cboLevelType.FieldName = Nothing
        Me.cboLevelType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLevelType.isCalculatedField = False
        Me.cboLevelType.IsSourceFromTable = False
        Me.cboLevelType.IsSourceFromValueList = False
        Me.cboLevelType.IsUnique = False
        RadListDataItem3.Text = "Sales Person"
        RadListDataItem4.Text = "Sales Region"
        Me.cboLevelType.Items.Add(RadListDataItem3)
        Me.cboLevelType.Items.Add(RadListDataItem4)
        Me.cboLevelType.Location = New System.Drawing.Point(110, 35)
        Me.cboLevelType.MendatroryField = False
        Me.cboLevelType.MyLinkLable1 = Me.lblLevelType
        Me.cboLevelType.MyLinkLable2 = Nothing
        Me.cboLevelType.Name = "cboLevelType"
        Me.cboLevelType.ReferenceFieldDesc = Nothing
        Me.cboLevelType.ReferenceFieldName = Nothing
        Me.cboLevelType.ReferenceTableName = Nothing
        Me.cboLevelType.Size = New System.Drawing.Size(282, 18)
        Me.cboLevelType.TabIndex = 414
        '
        'lblLevelType
        '
        Me.lblLevelType.FieldName = Nothing
        Me.lblLevelType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevelType.Location = New System.Drawing.Point(16, 34)
        Me.lblLevelType.Name = "lblLevelType"
        Me.lblLevelType.Size = New System.Drawing.Size(62, 16)
        Me.lblLevelType.TabIndex = 415
        Me.lblLevelType.Text = "Level Type"
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDocCode.Location = New System.Drawing.Point(16, 13)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(93, 16)
        Me.lblDocCode.TabIndex = 59
        Me.lblDocCode.Text = "Document Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(111, 12)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDocCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(266, 21)
        Me.txtCode.TabIndex = 57
        Me.txtCode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(377, 13)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 58
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(84, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 5
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1057, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'FrmSalesLevelhierarchy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1135, 423)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSalesLevelhierarchy"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmSalesLevelhierarchy"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cboSubType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSeqNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSeqNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsLastLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsFirstLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLevelType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevelType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents cboLevelType As common.Controls.MyComboBox
    Friend WithEvents lblLevelType As common.Controls.MyLabel
    Friend WithEvents chkIsLastLevel As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIsFirstLevel As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtSeqNo As common.MyNumBox
    Friend WithEvents lblSeqNo As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboSubType As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

