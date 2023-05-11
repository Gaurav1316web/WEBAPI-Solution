Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHRAExemptionRule
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
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPer = New Telerik.WinControls.UI.RadButton()
        Me.btnClBr = New Telerik.WinControls.UI.RadButton()
        Me.btnOpBr = New Telerik.WinControls.UI.RadButton()
        Me.btndiv = New Telerik.WinControls.UI.RadButton()
        Me.btnMul = New Telerik.WinControls.UI.RadButton()
        Me.btnMin = New Telerik.WinControls.UI.RadButton()
        Me.btnPul = New Telerik.WinControls.UI.RadButton()
        Me.txtFormula = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.lblOperator = New common.Controls.MyLabel()
        Me.MyLabel84 = New common.Controls.MyLabel()
        Me.cmbLocation = New common.Controls.MyComboBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.TxtParticulars = New common.Controls.MyTextBox()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.txtSalStructure = New common.UserControls.txtFinder()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClBr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOpBr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndiv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMul, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPul, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFormula, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOperator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtParticulars, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(592, 20)
        Me.RadMenu2.TabIndex = 66
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(592, 478)
        Me.SplitContainer1.SplitterDistance = 436
        Me.SplitContainer1.TabIndex = 67
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSalStructure)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnPer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnClBr)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnOpBr)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btndiv)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnMul)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnMin)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnPul)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFormula)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblOperator)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel84)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtParticulars)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemCategoryCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Size = New System.Drawing.Size(592, 436)
        Me.SplitContainer2.SplitterDistance = 159
        Me.SplitContainer2.TabIndex = 0
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(519, 131)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(62, 18)
        Me.btnReset.TabIndex = 247
        Me.btnReset.Text = " Reset"
        '
        'btnPer
        '
        Me.btnPer.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPer.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPer.Location = New System.Drawing.Point(210, 78)
        Me.btnPer.Name = "btnPer"
        Me.btnPer.Size = New System.Drawing.Size(23, 20)
        Me.btnPer.TabIndex = 237
        Me.btnPer.Text = "%"
        Me.btnPer.Visible = False
        '
        'btnClBr
        '
        Me.btnClBr.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClBr.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClBr.Location = New System.Drawing.Point(262, 78)
        Me.btnClBr.Name = "btnClBr"
        Me.btnClBr.Size = New System.Drawing.Size(23, 20)
        Me.btnClBr.TabIndex = 239
        Me.btnClBr.Text = ")"
        Me.btnClBr.Visible = False
        '
        'btnOpBr
        '
        Me.btnOpBr.DisplayStyle = Telerik.WinControls.DisplayStyle.Text
        Me.btnOpBr.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpBr.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnOpBr.Location = New System.Drawing.Point(236, 78)
        Me.btnOpBr.Name = "btnOpBr"
        Me.btnOpBr.Size = New System.Drawing.Size(23, 20)
        Me.btnOpBr.TabIndex = 238
        Me.btnOpBr.Text = "("
        Me.btnOpBr.Visible = False
        '
        'btndiv
        '
        Me.btndiv.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndiv.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btndiv.Location = New System.Drawing.Point(184, 78)
        Me.btndiv.Name = "btndiv"
        Me.btndiv.Size = New System.Drawing.Size(23, 20)
        Me.btndiv.TabIndex = 236
        Me.btndiv.Text = "/"
        '
        'btnMul
        '
        Me.btnMul.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMul.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnMul.Location = New System.Drawing.Point(158, 78)
        Me.btnMul.Name = "btnMul"
        Me.btnMul.Size = New System.Drawing.Size(23, 20)
        Me.btnMul.TabIndex = 235
        Me.btnMul.Text = "X"
        '
        'btnMin
        '
        Me.btnMin.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMin.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnMin.Location = New System.Drawing.Point(132, 78)
        Me.btnMin.Name = "btnMin"
        Me.btnMin.Size = New System.Drawing.Size(23, 20)
        Me.btnMin.TabIndex = 234
        Me.btnMin.Text = "-"
        '
        'btnPul
        '
        Me.btnPul.DisplayStyle = Telerik.WinControls.DisplayStyle.Text
        Me.btnPul.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPul.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnPul.Location = New System.Drawing.Point(106, 78)
        Me.btnPul.Name = "btnPul"
        Me.btnPul.Size = New System.Drawing.Size(23, 20)
        Me.btnPul.TabIndex = 233
        Me.btnPul.Text = "+"
        '
        'txtFormula
        '
        Me.txtFormula.AutoSize = False
        Me.txtFormula.Location = New System.Drawing.Point(17, 123)
        Me.txtFormula.MaxLength = 50
        Me.txtFormula.MendatroryField = False
        Me.txtFormula.Multiline = True
        Me.txtFormula.MyLinkLable1 = Me.RadLabel3
        Me.txtFormula.MyLinkLable2 = Nothing
        Me.txtFormula.Name = "txtFormula"
        Me.txtFormula.ReadOnly = True
        Me.txtFormula.Size = New System.Drawing.Size(497, 26)
        Me.txtFormula.TabIndex = 245
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(13, 101)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(89, 18)
        Me.RadLabel3.TabIndex = 246
        Me.RadLabel3.Text = "Created Formula"
        '
        'lblOperator
        '
        Me.lblOperator.Location = New System.Drawing.Point(12, 79)
        Me.lblOperator.Name = "lblOperator"
        Me.lblOperator.Size = New System.Drawing.Size(84, 18)
        Me.lblOperator.TabIndex = 244
        Me.lblOperator.Text = "Select Operator"
        '
        'MyLabel84
        '
        Me.MyLabel84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel84.Location = New System.Drawing.Point(12, 53)
        Me.MyLabel84.Name = "MyLabel84"
        Me.MyLabel84.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel84.TabIndex = 243
        Me.MyLabel84.Text = "Salary Structure "
        '
        'cmbLocation
        '
        Me.cmbLocation.AutoCompleteDisplayMember = Nothing
        Me.cmbLocation.AutoCompleteValueMember = Nothing
        Me.cmbLocation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbLocation.Location = New System.Drawing.Point(451, 28)
        Me.cmbLocation.MendatroryField = True
        Me.cmbLocation.MyLinkLable1 = Me.MyLabel18
        Me.cmbLocation.MyLinkLable2 = Nothing
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(135, 20)
        Me.cmbLocation.TabIndex = 232
        '
        'MyLabel18
        '
        Me.MyLabel18.Location = New System.Drawing.Point(393, 30)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(52, 18)
        Me.MyLabel18.TabIndex = 242
        Me.MyLabel18.Text = "Location "
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(12, 32)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 241
        Me.lblDescription.Text = "Particulars "
        '
        'TxtParticulars
        '
        Me.TxtParticulars.AutoSize = False
        Me.TxtParticulars.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParticulars.Location = New System.Drawing.Point(107, 30)
        Me.TxtParticulars.MaxLength = 100
        Me.TxtParticulars.MendatroryField = False
        Me.TxtParticulars.Multiline = True
        Me.TxtParticulars.MyLinkLable1 = Me.lblDescription
        Me.TxtParticulars.MyLinkLable2 = Nothing
        Me.TxtParticulars.Name = "TxtParticulars"
        Me.TxtParticulars.Size = New System.Drawing.Size(226, 21)
        Me.TxtParticulars.TabIndex = 231
        Me.TxtParticulars.Text = " "
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(12, 8)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(36, 16)
        Me.lblItemCategoryCode.TabIndex = 240
        Me.lblItemCategoryCode.Text = "Code "
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(318, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 249
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(106, 7)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Nothing
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 32767
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(212, 21)
        Me.txtcode.TabIndex = 248
        Me.txtcode.Value = ""
        '
        'txtSalStructure
        '
        Me.txtSalStructure.Location = New System.Drawing.Point(106, 53)
        Me.txtSalStructure.MendatroryField = True
        Me.txtSalStructure.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalStructure.MyLinkLable1 = Nothing
        Me.txtSalStructure.MyLinkLable2 = Nothing
        Me.txtSalStructure.MyReadOnly = False
        Me.txtSalStructure.MyShowMasterFormButton = False
        Me.txtSalStructure.Name = "txtSalStructure"
        Me.txtSalStructure.Size = New System.Drawing.Size(227, 20)
        Me.txtSalStructure.TabIndex = 301
        Me.txtSalStructure.Value = ""
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.EnableCustomFiltering = True
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AutoGenerateColumns = False
        Me.gv.MasterTemplate.EnableCustomFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.Size = New System.Drawing.Size(592, 273)
        Me.gv.TabIndex = 13
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 16
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(518, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 18
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 17
        Me.btndelete.Text = "Delete"
        '
        'FrmHRAExemptionRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 498)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "FrmHRAExemptionRule"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmHRAExemptionRule"
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClBr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOpBr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndiv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMul, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPul, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFormula, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOperator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtParticulars, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPer As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClBr As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOpBr As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndiv As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnMul As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnMin As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPul As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtFormula As common.Controls.MyTextBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblOperator As common.Controls.MyLabel
    Friend WithEvents MyLabel84 As common.Controls.MyLabel
    Friend WithEvents cmbLocation As common.Controls.MyComboBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents TxtParticulars As common.Controls.MyTextBox
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents txtSalStructure As common.UserControls.txtFinder
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
End Class

