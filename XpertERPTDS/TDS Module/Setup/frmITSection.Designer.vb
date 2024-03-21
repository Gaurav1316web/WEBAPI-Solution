Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmITSection
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
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtITAct = New common.Controls.MyTextBox()
        Me.txtMaxAmt = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMinAmt = New common.MyNumBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.TxtDesp = New common.Controls.MyTextBox()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtITAct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtITAct)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMaxAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMinAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtDesp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemCategoryCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(517, 225)
        Me.SplitContainer1.SplitterDistance = 186
        Me.SplitContainer1.TabIndex = 0
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(377, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 214
        '
        'txtcode
        '
        Me.txtcode.FieldName = Nothing
        Me.txtcode.Location = New System.Drawing.Point(116, 11)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Nothing
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(261, 21)
        Me.txtcode.TabIndex = 213
        Me.txtcode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 65)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel2.TabIndex = 212
        Me.MyLabel2.Text = "Income Tax Act "
        '
        'txtITAct
        '
        Me.txtITAct.AutoSize = False
        Me.txtITAct.CalculationExpression = Nothing
        Me.txtITAct.FieldCode = Nothing
        Me.txtITAct.FieldDesc = Nothing
        Me.txtITAct.FieldMaxLength = 0
        Me.txtITAct.FieldName = Nothing
        Me.txtITAct.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtITAct.isCalculatedField = False
        Me.txtITAct.IsSourceFromTable = False
        Me.txtITAct.IsSourceFromValueList = False
        Me.txtITAct.IsUnique = False
        Me.txtITAct.Location = New System.Drawing.Point(116, 62)
        Me.txtITAct.MaxLength = 50
        Me.txtITAct.MendatroryField = True
        Me.txtITAct.Multiline = True
        Me.txtITAct.MyLinkLable1 = Me.MyLabel2
        Me.txtITAct.MyLinkLable2 = Nothing
        Me.txtITAct.Name = "txtITAct"
        Me.txtITAct.ReferenceFieldDesc = Nothing
        Me.txtITAct.ReferenceFieldName = Nothing
        Me.txtITAct.ReferenceTableName = Nothing
        Me.txtITAct.Size = New System.Drawing.Size(274, 21)
        Me.txtITAct.TabIndex = 205
        Me.txtITAct.Text = " "
        '
        'txtMaxAmt
        '
        Me.txtMaxAmt.BackColor = System.Drawing.Color.White
        Me.txtMaxAmt.CalculationExpression = Nothing
        Me.txtMaxAmt.DecimalPlaces = 2
        Me.txtMaxAmt.FieldCode = Nothing
        Me.txtMaxAmt.FieldDesc = Nothing
        Me.txtMaxAmt.FieldMaxLength = 0
        Me.txtMaxAmt.FieldName = Nothing
        Me.txtMaxAmt.isCalculatedField = False
        Me.txtMaxAmt.IsSourceFromTable = False
        Me.txtMaxAmt.IsSourceFromValueList = False
        Me.txtMaxAmt.IsUnique = False
        Me.txtMaxAmt.Location = New System.Drawing.Point(116, 110)
        Me.txtMaxAmt.MaxLength = 10
        Me.txtMaxAmt.MendatroryField = False
        Me.txtMaxAmt.MyLinkLable1 = Me.MyLabel1
        Me.txtMaxAmt.MyLinkLable2 = Nothing
        Me.txtMaxAmt.Name = "txtMaxAmt"
        Me.txtMaxAmt.ReferenceFieldDesc = Nothing
        Me.txtMaxAmt.ReferenceFieldName = Nothing
        Me.txtMaxAmt.ReferenceTableName = Nothing
        Me.txtMaxAmt.Size = New System.Drawing.Size(110, 20)
        Me.txtMaxAmt.TabIndex = 207
        Me.txtMaxAmt.Text = "0"
        Me.txtMaxAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaxAmt.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(11, 110)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel1.TabIndex = 211
        Me.MyLabel1.Text = "Maximum Amount "
        '
        'txtMinAmt
        '
        Me.txtMinAmt.BackColor = System.Drawing.Color.White
        Me.txtMinAmt.CalculationExpression = Nothing
        Me.txtMinAmt.DecimalPlaces = 2
        Me.txtMinAmt.FieldCode = Nothing
        Me.txtMinAmt.FieldDesc = Nothing
        Me.txtMinAmt.FieldMaxLength = 0
        Me.txtMinAmt.FieldName = Nothing
        Me.txtMinAmt.isCalculatedField = False
        Me.txtMinAmt.IsSourceFromTable = False
        Me.txtMinAmt.IsSourceFromValueList = False
        Me.txtMinAmt.IsUnique = False
        Me.txtMinAmt.Location = New System.Drawing.Point(116, 87)
        Me.txtMinAmt.MaxLength = 10
        Me.txtMinAmt.MendatroryField = False
        Me.txtMinAmt.MyLinkLable1 = Me.RadLabel8
        Me.txtMinAmt.MyLinkLable2 = Nothing
        Me.txtMinAmt.Name = "txtMinAmt"
        Me.txtMinAmt.ReferenceFieldDesc = Nothing
        Me.txtMinAmt.ReferenceFieldName = Nothing
        Me.txtMinAmt.ReferenceTableName = Nothing
        Me.txtMinAmt.Size = New System.Drawing.Size(110, 20)
        Me.txtMinAmt.TabIndex = 206
        Me.txtMinAmt.Text = "0"
        Me.txtMinAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinAmt.Value = 0R
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Location = New System.Drawing.Point(12, 88)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(101, 18)
        Me.RadLabel8.TabIndex = 210
        Me.RadLabel8.Text = "Minimum Amount "
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(12, 40)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(66, 16)
        Me.lblDescription.TabIndex = 209
        Me.lblDescription.Text = "Description "
        '
        'TxtDesp
        '
        Me.TxtDesp.AutoSize = False
        Me.TxtDesp.CalculationExpression = Nothing
        Me.TxtDesp.FieldCode = Nothing
        Me.TxtDesp.FieldDesc = Nothing
        Me.TxtDesp.FieldMaxLength = 0
        Me.TxtDesp.FieldName = Nothing
        Me.TxtDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesp.isCalculatedField = False
        Me.TxtDesp.IsSourceFromTable = False
        Me.TxtDesp.IsSourceFromValueList = False
        Me.TxtDesp.IsUnique = False
        Me.TxtDesp.Location = New System.Drawing.Point(116, 38)
        Me.TxtDesp.MaxLength = 100
        Me.TxtDesp.MendatroryField = False
        Me.TxtDesp.Multiline = True
        Me.TxtDesp.MyLinkLable1 = Me.lblDescription
        Me.TxtDesp.MyLinkLable2 = Nothing
        Me.TxtDesp.Name = "TxtDesp"
        Me.TxtDesp.ReferenceFieldDesc = Nothing
        Me.TxtDesp.ReferenceFieldName = Nothing
        Me.TxtDesp.ReferenceTableName = Nothing
        Me.TxtDesp.Size = New System.Drawing.Size(274, 21)
        Me.TxtDesp.TabIndex = 204
        Me.TxtDesp.Text = " "
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(12, 15)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(91, 16)
        Me.lblItemCategoryCode.TabIndex = 208
        Me.lblItemCategoryCode.Text = "IT Section Code "
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 12
        Me.btnsave.Text = "Save"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(443, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(66, 21)
        Me.RadButton1.TabIndex = 14
        Me.RadButton1.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(79, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 13
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(619, -13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 11
        Me.btnclose.Text = "Close"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(517, 20)
        Me.RadMenu2.TabIndex = 64
        '
        'rmImport
        '
        Me.rmImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RmExport, Me.RadMenuItem2})
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "File"
        '
        'RmExport
        '
        Me.RmExport.Name = "RmExport"
        Me.RmExport.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "rmImporter"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'FrmITSection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 245)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "FrmITSection"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmITSection"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtITAct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtITAct As common.Controls.MyTextBox
    Friend WithEvents txtMaxAmt As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMinAmt As common.MyNumBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents TxtDesp As common.Controls.MyTextBox
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents RmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
End Class

