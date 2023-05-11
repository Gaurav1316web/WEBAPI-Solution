<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentTerms
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
        Me.components = New System.ComponentModel.Container()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txt_desc = New common.Controls.MyTextBox()
        Me.btn_save = New Telerik.WinControls.UI.RadButton()
        Me.btn_delete = New Telerik.WinControls.UI.RadButton()
        Me.btn_close = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.txt_no_of_days = New common.Controls.MyTextBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtAdvancePer = New common.MyNumBox()
        Me.chkAdvance = New System.Windows.Forms.CheckBox()
        Me.chkLCRequired = New common.Controls.MyCheckBox()
        Me.fnd_termscode = New common.UserControls.txtNavigator()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.btn_reset = New Telerik.WinControls.UI.RadButton()
        Me.ToolTipTerms = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_no_of_days, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdvancePer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLCRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(68, 16)
        Me.RadLabel1.TabIndex = 6
        Me.RadLabel1.Text = "Terms Code"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(13, 48)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel3.TabIndex = 5
        Me.RadLabel3.Text = "No Of Days"
        '
        'txt_desc
        '
        Me.txt_desc.CalculationExpression = Nothing
        Me.txt_desc.FieldCode = Nothing
        Me.txt_desc.FieldDesc = Nothing
        Me.txt_desc.FieldMaxLength = 0
        Me.txt_desc.FieldName = Nothing
        Me.txt_desc.isCalculatedField = False
        Me.txt_desc.IsSourceFromTable = False
        Me.txt_desc.IsSourceFromValueList = False
        Me.txt_desc.IsUnique = False
        Me.txt_desc.Location = New System.Drawing.Point(351, 21)
        Me.txt_desc.MaxLength = 500
        Me.txt_desc.MendatroryField = True
        Me.txt_desc.MyLinkLable1 = Nothing
        Me.txt_desc.MyLinkLable2 = Nothing
        Me.txt_desc.Name = "txt_desc"
        Me.txt_desc.ReferenceFieldDesc = Nothing
        Me.txt_desc.ReferenceFieldName = Nothing
        Me.txt_desc.ReferenceTableName = Nothing
        Me.txt_desc.Size = New System.Drawing.Size(226, 20)
        Me.txt_desc.TabIndex = 1
        '
        'btn_save
        '
        Me.btn_save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_save.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.Location = New System.Drawing.Point(3, 9)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(64, 18)
        Me.btn_save.TabIndex = 0
        Me.btn_save.Text = "Save"
        '
        'btn_delete
        '
        Me.btn_delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_delete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delete.Location = New System.Drawing.Point(68, 9)
        Me.btn_delete.Name = "btn_delete"
        Me.btn_delete.Size = New System.Drawing.Size(66, 18)
        Me.btn_delete.TabIndex = 1
        Me.btn_delete.Text = "Delete"
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_close.Location = New System.Drawing.Point(525, 9)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(68, 18)
        Me.btn_close.TabIndex = 2
        Me.btn_close.Text = "Close"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "RadMenuItem3"
        Me.RadMenuItem3.AccessibleName = "RadMenuItem3"
        Me.RadMenuItem3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import.."
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export.."
        Me.RadMenuItem2.AccessibleName = "Export.."
        Me.RadMenuItem2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export.."
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Close"
        Me.RadMenuItem4.AccessibleName = "Close"
        Me.RadMenuItem4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(636, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'txt_no_of_days
        '
        Me.txt_no_of_days.CalculationExpression = Nothing
        Me.txt_no_of_days.FieldCode = Nothing
        Me.txt_no_of_days.FieldDesc = Nothing
        Me.txt_no_of_days.FieldMaxLength = 0
        Me.txt_no_of_days.FieldName = Nothing
        Me.txt_no_of_days.isCalculatedField = False
        Me.txt_no_of_days.IsSourceFromTable = False
        Me.txt_no_of_days.IsSourceFromValueList = False
        Me.txt_no_of_days.IsUnique = False
        Me.txt_no_of_days.Location = New System.Drawing.Point(88, 46)
        Me.txt_no_of_days.MaxLength = 3
        Me.txt_no_of_days.MendatroryField = True
        Me.txt_no_of_days.MyLinkLable1 = Me.RadLabel3
        Me.txt_no_of_days.MyLinkLable2 = Nothing
        Me.txt_no_of_days.Name = "txt_no_of_days"
        Me.txt_no_of_days.ReferenceFieldDesc = Nothing
        Me.txt_no_of_days.ReferenceFieldName = Nothing
        Me.txt_no_of_days.ReferenceTableName = Nothing
        Me.txt_no_of_days.Size = New System.Drawing.Size(242, 20)
        Me.txt_no_of_days.TabIndex = 2
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtAdvancePer)
        Me.RadGroupBox1.Controls.Add(Me.chkAdvance)
        Me.RadGroupBox1.Controls.Add(Me.chkLCRequired)
        Me.RadGroupBox1.Controls.Add(Me.fnd_termscode)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.btn_reset)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txt_no_of_days)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txt_desc)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(621, 403)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(561, 48)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(16, 16)
        Me.MyLabel2.TabIndex = 23
        Me.MyLabel2.Text = "%"
        '
        'txtAdvancePer
        '
        Me.txtAdvancePer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAdvancePer.CalculationExpression = Nothing
        Me.txtAdvancePer.DecimalPlaces = 2
        Me.txtAdvancePer.FieldCode = Nothing
        Me.txtAdvancePer.FieldDesc = Nothing
        Me.txtAdvancePer.FieldMaxLength = 0
        Me.txtAdvancePer.FieldName = Nothing
        Me.txtAdvancePer.isCalculatedField = False
        Me.txtAdvancePer.IsSourceFromTable = False
        Me.txtAdvancePer.IsSourceFromValueList = False
        Me.txtAdvancePer.IsUnique = False
        Me.txtAdvancePer.Location = New System.Drawing.Point(502, 46)
        Me.txtAdvancePer.MendatroryField = True
        Me.txtAdvancePer.MyLinkLable1 = Nothing
        Me.txtAdvancePer.MyLinkLable2 = Nothing
        Me.txtAdvancePer.Name = "txtAdvancePer"
        Me.txtAdvancePer.ReferenceFieldDesc = Nothing
        Me.txtAdvancePer.ReferenceFieldName = Nothing
        Me.txtAdvancePer.ReferenceTableName = Nothing
        Me.txtAdvancePer.Size = New System.Drawing.Size(59, 20)
        Me.txtAdvancePer.TabIndex = 22
        Me.txtAdvancePer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvancePer.Value = 0.0R
        '
        'chkAdvance
        '
        Me.chkAdvance.AutoSize = True
        Me.chkAdvance.Location = New System.Drawing.Point(432, 47)
        Me.chkAdvance.Name = "chkAdvance"
        Me.chkAdvance.Size = New System.Drawing.Size(70, 18)
        Me.chkAdvance.TabIndex = 7
        Me.chkAdvance.Text = "Advance"
        Me.chkAdvance.UseVisualStyleBackColor = True
        '
        'chkLCRequired
        '
        Me.chkLCRequired.Location = New System.Drawing.Point(351, 47)
        Me.chkLCRequired.MyLinkLable1 = Nothing
        Me.chkLCRequired.MyLinkLable2 = Nothing
        Me.chkLCRequired.Name = "chkLCRequired"
        Me.chkLCRequired.Size = New System.Drawing.Size(81, 18)
        Me.chkLCRequired.TabIndex = 3
        Me.chkLCRequired.Tag1 = Nothing
        Me.chkLCRequired.Text = "LC Required"
        '
        'fnd_termscode
        '
        Me.fnd_termscode.FieldName = Nothing
        Me.fnd_termscode.Location = New System.Drawing.Point(88, 21)
        Me.fnd_termscode.MendatroryField = True
        Me.fnd_termscode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fnd_termscode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fnd_termscode.MyLinkLable1 = Me.RadLabel1
        Me.fnd_termscode.MyLinkLable2 = Nothing
        Me.fnd_termscode.MyMaxLength = 32767
        Me.fnd_termscode.MyReadOnly = False
        Me.fnd_termscode.Name = "fnd_termscode"
        Me.fnd_termscode.Size = New System.Drawing.Size(236, 20)
        Me.fnd_termscode.TabIndex = 0
        Me.fnd_termscode.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(6, 83)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(528, 308)
        Me.RadGroupBox4.TabIndex = 4
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.ShowHeaderCellButtons = True
        Me.gvDB.Size = New System.Drawing.Size(508, 278)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        Me.gvDB.Text = "RadGridView1"
        '
        'btn_reset
        '
        Me.btn_reset.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btn_reset.Location = New System.Drawing.Point(326, 21)
        Me.btn_reset.Name = "btn_reset"
        Me.btn_reset.Size = New System.Drawing.Size(15, 20)
        Me.btn_reset.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_save)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_delete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_close)
        Me.SplitContainer1.Size = New System.Drawing.Size(636, 453)
        Me.SplitContainer1.SplitterDistance = 419
        Me.SplitContainer1.TabIndex = 0
        '
        'frmPaymentTerms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmPaymentTerms"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Payment Terms"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_no_of_days, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdvancePer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLCRequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_desc As common.Controls.MyTextBox
    Friend WithEvents btn_save As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_delete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_close As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents txt_no_of_days As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ToolTipTerms As System.Windows.Forms.ToolTip
    Friend WithEvents btn_reset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents fnd_termscode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkLCRequired As common.Controls.MyCheckBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtAdvancePer As common.MyNumBox
    Friend WithEvents chkAdvance As System.Windows.Forms.CheckBox
End Class

