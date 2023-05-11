<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateTemplate
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
        Me.lblName = New common.Controls.MyLabel()
        Me.lblTemplate = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.lblReportId = New common.Controls.MyLabel()
        Me.lblUserId = New common.Controls.MyLabel()
        Me.txtReportId = New common.Controls.MyTextBox()
        Me.txtUserId = New common.Controls.MyTextBox()
        Me.ddlTemplate = New Telerik.WinControls.UI.RadDropDownList()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReportId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUserId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReportId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlTemplate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtUserId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReportId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblUserId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblReportId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTemplate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(558, 143)
        Me.SplitContainer1.SplitterDistance = 114
        Me.SplitContainer1.TabIndex = 158
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Location = New System.Drawing.Point(12, 50)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 18)
        Me.lblName.TabIndex = 180
        Me.lblName.Text = "Name"
        '
        'lblTemplate
        '
        Me.lblTemplate.FieldName = Nothing
        Me.lblTemplate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemplate.Location = New System.Drawing.Point(12, 28)
        Me.lblTemplate.Name = "lblTemplate"
        Me.lblTemplate.Size = New System.Drawing.Size(53, 16)
        Me.lblTemplate.TabIndex = 176
        Me.lblTemplate.Text = "Template"
        '
        'txtName
        '
        Me.txtName.AutoSize = False
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(82, 47)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = False
        Me.txtName.Multiline = True
        Me.txtName.MyLinkLable1 = Me.lblTemplate
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(249, 21)
        Me.txtName.TabIndex = 2
        Me.txtName.Text = " "
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(10, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(478, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'lblReportId
        '
        Me.lblReportId.FieldName = Nothing
        Me.lblReportId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportId.Location = New System.Drawing.Point(12, 6)
        Me.lblReportId.Name = "lblReportId"
        Me.lblReportId.Size = New System.Drawing.Size(53, 16)
        Me.lblReportId.TabIndex = 181
        Me.lblReportId.Text = "Report Id"
        '
        'lblUserId
        '
        Me.lblUserId.FieldName = Nothing
        Me.lblUserId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserId.Location = New System.Drawing.Point(273, 6)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(43, 16)
        Me.lblUserId.TabIndex = 182
        Me.lblUserId.Text = "User Id"
        '
        'txtReportId
        '
        Me.txtReportId.AutoSize = False
        Me.txtReportId.CalculationExpression = Nothing
        Me.txtReportId.Enabled = False
        Me.txtReportId.FieldCode = Nothing
        Me.txtReportId.FieldDesc = Nothing
        Me.txtReportId.FieldMaxLength = 0
        Me.txtReportId.FieldName = Nothing
        Me.txtReportId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportId.isCalculatedField = False
        Me.txtReportId.IsSourceFromTable = False
        Me.txtReportId.IsSourceFromValueList = False
        Me.txtReportId.IsUnique = False
        Me.txtReportId.Location = New System.Drawing.Point(82, 3)
        Me.txtReportId.MaxLength = 50
        Me.txtReportId.MendatroryField = False
        Me.txtReportId.Multiline = True
        Me.txtReportId.MyLinkLable1 = Me.lblTemplate
        Me.txtReportId.MyLinkLable2 = Nothing
        Me.txtReportId.Name = "txtReportId"
        Me.txtReportId.ReferenceFieldDesc = Nothing
        Me.txtReportId.ReferenceFieldName = Nothing
        Me.txtReportId.ReferenceTableName = Nothing
        Me.txtReportId.Size = New System.Drawing.Size(162, 21)
        Me.txtReportId.TabIndex = 183
        Me.txtReportId.Text = " "
        '
        'txtUserId
        '
        Me.txtUserId.AutoSize = False
        Me.txtUserId.CalculationExpression = Nothing
        Me.txtUserId.Enabled = False
        Me.txtUserId.FieldCode = Nothing
        Me.txtUserId.FieldDesc = Nothing
        Me.txtUserId.FieldMaxLength = 0
        Me.txtUserId.FieldName = Nothing
        Me.txtUserId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserId.isCalculatedField = False
        Me.txtUserId.IsSourceFromTable = False
        Me.txtUserId.IsSourceFromValueList = False
        Me.txtUserId.IsUnique = False
        Me.txtUserId.Location = New System.Drawing.Point(322, 3)
        Me.txtUserId.MaxLength = 50
        Me.txtUserId.MendatroryField = False
        Me.txtUserId.Multiline = True
        Me.txtUserId.MyLinkLable1 = Me.lblTemplate
        Me.txtUserId.MyLinkLable2 = Nothing
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.ReferenceFieldDesc = Nothing
        Me.txtUserId.ReferenceFieldName = Nothing
        Me.txtUserId.ReferenceTableName = Nothing
        Me.txtUserId.Size = New System.Drawing.Size(162, 21)
        Me.txtUserId.TabIndex = 184
        Me.txtUserId.Text = " "
        '
        'ddlTemplate
        '
        Me.ddlTemplate.AutoCompleteDisplayMember = Nothing
        Me.ddlTemplate.AutoCompleteValueMember = Nothing
        Me.ddlTemplate.Location = New System.Drawing.Point(82, 26)
        Me.ddlTemplate.Name = "ddlTemplate"
        Me.ddlTemplate.Size = New System.Drawing.Size(249, 20)
        Me.ddlTemplate.TabIndex = 185
        '
        'frmCreateTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 143)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCreateTemplate"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Template"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReportId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUserId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReportId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTemplate As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents txtReportId As common.Controls.MyTextBox
    Friend WithEvents lblUserId As common.Controls.MyLabel
    Friend WithEvents lblReportId As common.Controls.MyLabel
    Friend WithEvents txtUserId As common.Controls.MyTextBox
    Friend WithEvents ddlTemplate As Telerik.WinControls.UI.RadDropDownList
End Class
