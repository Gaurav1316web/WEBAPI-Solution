<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPaymentCode
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.lbl_paymentcode = New common.Controls.MyLabel()
        Me.lbl_description = New common.Controls.MyLabel()
        Me.lbl_paymenttype = New common.Controls.MyLabel()
        Me.txt_description = New common.Controls.MyTextBox()
        Me.ddl_paymenttype = New common.Controls.MyComboBox()
        Me.btn_close = New Telerik.WinControls.UI.RadButton()
        Me.btn_delete = New Telerik.WinControls.UI.RadButton()
        Me.btn_save = New Telerik.WinControls.UI.RadButton()
        Me.btn_reset = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.ToolTipcode = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fnd_paymentcode = New common.UserControls.txtNavigator()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.lbl_paymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_paymenttype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddl_paymenttype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_paymentcode
        '
        Me.lbl_paymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbl_paymentcode.Location = New System.Drawing.Point(13, 19)
        Me.lbl_paymentcode.Name = "lbl_paymentcode"
        Me.lbl_paymentcode.Size = New System.Drawing.Size(85, 16)
        Me.lbl_paymentcode.TabIndex = 7
        Me.lbl_paymentcode.Text = "Payment Code"
        '
        'lbl_description
        '
        Me.lbl_description.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_description.Location = New System.Drawing.Point(13, 43)
        Me.lbl_description.Name = "lbl_description"
        Me.lbl_description.Size = New System.Drawing.Size(63, 16)
        Me.lbl_description.TabIndex = 6
        Me.lbl_description.Text = "Description"
        '
        'lbl_paymenttype
        '
        Me.lbl_paymenttype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_paymenttype.Location = New System.Drawing.Point(13, 65)
        Me.lbl_paymenttype.Name = "lbl_paymenttype"
        Me.lbl_paymenttype.Size = New System.Drawing.Size(79, 16)
        Me.lbl_paymenttype.TabIndex = 5
        Me.lbl_paymenttype.Text = "Payment Type"
        '
        'txt_description
        '
        Me.txt_description.Location = New System.Drawing.Point(168, 41)
        Me.txt_description.MaxLength = 50
        Me.txt_description.MendatroryField = False
        Me.txt_description.MyLinkLable1 = Me.lbl_description
        Me.txt_description.MyLinkLable2 = Nothing
        Me.txt_description.Name = "txt_description"
        Me.txt_description.Size = New System.Drawing.Size(370, 20)
        Me.txt_description.TabIndex = 2
        '
        'ddl_paymenttype
        '
        Me.ddl_paymenttype.AutoCompleteDisplayMember = Nothing
        Me.ddl_paymenttype.AutoCompleteValueMember = Nothing
        Me.ddl_paymenttype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddl_paymenttype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Cash"
        RadListDataItem2.Text = "Cheque"
        RadListDataItem3.Text = "Petty Cash"
        RadListDataItem4.Text = "Other"
        RadListDataItem5.Text = "NEFT"
        RadListDataItem6.Text = "RTGS"
        RadListDataItem7.Text = "Transfer"
        Me.ddl_paymenttype.Items.Add(RadListDataItem1)
        Me.ddl_paymenttype.Items.Add(RadListDataItem2)
        Me.ddl_paymenttype.Items.Add(RadListDataItem3)
        Me.ddl_paymenttype.Items.Add(RadListDataItem4)
        Me.ddl_paymenttype.Items.Add(RadListDataItem5)
        Me.ddl_paymenttype.Items.Add(RadListDataItem6)
        Me.ddl_paymenttype.Items.Add(RadListDataItem7)
        Me.ddl_paymenttype.Location = New System.Drawing.Point(168, 64)
        Me.ddl_paymenttype.MaxLength = 12
        Me.ddl_paymenttype.MendatroryField = False
        Me.ddl_paymenttype.MyLinkLable1 = Me.lbl_paymenttype
        Me.ddl_paymenttype.MyLinkLable2 = Nothing
        Me.ddl_paymenttype.Name = "ddl_paymenttype"
        Me.ddl_paymenttype.Size = New System.Drawing.Size(129, 18)
        Me.ddl_paymenttype.TabIndex = 3
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_close.Location = New System.Drawing.Point(482, 3)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(68, 18)
        Me.btn_close.TabIndex = 2
        Me.btn_close.Text = "Close"
        '
        'btn_delete
        '
        Me.btn_delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_delete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delete.Location = New System.Drawing.Point(70, 5)
        Me.btn_delete.Name = "btn_delete"
        Me.btn_delete.Size = New System.Drawing.Size(66, 18)
        Me.btn_delete.TabIndex = 1
        Me.btn_delete.Text = "Delete"
        '
        'btn_save
        '
        Me.btn_save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_save.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.Location = New System.Drawing.Point(3, 5)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(64, 18)
        Me.btn_save.TabIndex = 0
        Me.btn_save.Text = "Save"
        '
        'btn_reset
        '
        Me.btn_reset.Image = Global.ERP.My.Resources.Resources._new
        Me.btn_reset.Location = New System.Drawing.Point(372, 17)
        Me.btn_reset.Name = "btn_reset"
        Me.btn_reset.Size = New System.Drawing.Size(15, 20)
        Me.btn_reset.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(558, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem1.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "menu_import"
        Me.RadMenuItem2.AccessibleName = "menu_import"
        Me.RadMenuItem2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import.."
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "menu_export"
        Me.RadMenuItem3.AccessibleName = "menu_export"
        Me.RadMenuItem3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export.."
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "menu_close"
        Me.RadMenuItem4.AccessibleName = "menu_close"
        Me.RadMenuItem4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Close"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fnd_paymentcode)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.lbl_paymentcode)
        Me.RadGroupBox1.Controls.Add(Me.lbl_description)
        Me.RadGroupBox1.Controls.Add(Me.btn_reset)
        Me.RadGroupBox1.Controls.Add(Me.lbl_paymenttype)
        Me.RadGroupBox1.Controls.Add(Me.txt_description)
        Me.RadGroupBox1.Controls.Add(Me.ddl_paymenttype)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(551, 337)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fnd_paymentcode
        '
        Me.fnd_paymentcode.Location = New System.Drawing.Point(168, 17)
        Me.fnd_paymentcode.MendatroryField = True
        Me.fnd_paymentcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fnd_paymentcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fnd_paymentcode.MyLinkLable1 = Me.lbl_paymentcode
        Me.fnd_paymentcode.MyLinkLable2 = Nothing
        Me.fnd_paymentcode.MyMaxLength = 32767
        Me.fnd_paymentcode.MyReadOnly = False
        Me.fnd_paymentcode.Name = "fnd_paymentcode"
        Me.fnd_paymentcode.Size = New System.Drawing.Size(202, 20)
        Me.fnd_paymentcode.TabIndex = 0
        Me.fnd_paymentcode.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 107)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(527, 200)
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
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(507, 170)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        Me.gvDB.Text = "RadGridView1"
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
        Me.SplitContainer1.Size = New System.Drawing.Size(558, 347)
        Me.SplitContainer1.SplitterDistance = 317
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmPaymentCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 367)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmPaymentCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Payment Code"
        CType(Me.lbl_paymentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_paymenttype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddl_paymenttype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_description As common.Controls.MyTextBox
    Friend WithEvents ddl_paymenttype As common.Controls.MyComboBox
    Friend WithEvents btn_close As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_delete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_save As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_reset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents ToolTipcode As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents lbl_paymentcode As common.Controls.MyLabel
    Friend WithEvents lbl_description As common.Controls.MyLabel
    Friend WithEvents lbl_paymenttype As common.Controls.MyLabel
    Friend WithEvents fnd_paymentcode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

