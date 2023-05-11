<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventorySourceCode
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboInCatg = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cboOutCatg = New common.Controls.MyComboBox()
        Me.txtType = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.cboInOutType = New common.Controls.MyComboBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.fndaccgp = New common.UserControls.txtNavigator()
        Me.lblaccgp = New common.Controls.MyLabel()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.lbldes = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.txtSequence = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboInCatg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOutCatg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboInOutType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSequence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSequence)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboInOutType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndaccgp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblaccgp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(560, 244)
        Me.SplitContainer1.SplitterDistance = 207
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Location = New System.Drawing.Point(6, 122)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboInCatg)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cboOutCatg)
        Me.SplitContainer2.Size = New System.Drawing.Size(509, 21)
        Me.SplitContainer2.SplitterDistance = 254
        Me.SplitContainer2.TabIndex = 345
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(4, 1)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel2.TabIndex = 344
        Me.MyLabel2.Text = "In Category"
        '
        'cboInCatg
        '
        Me.cboInCatg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboInCatg.AutoCompleteDisplayMember = Nothing
        Me.cboInCatg.AutoCompleteValueMember = Nothing
        Me.cboInCatg.CalculationExpression = Nothing
        Me.cboInCatg.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboInCatg.FieldCode = Nothing
        Me.cboInCatg.FieldDesc = Nothing
        Me.cboInCatg.FieldMaxLength = 0
        Me.cboInCatg.FieldName = Nothing
        Me.cboInCatg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInCatg.isCalculatedField = False
        Me.cboInCatg.IsSourceFromTable = False
        Me.cboInCatg.IsSourceFromValueList = False
        Me.cboInCatg.IsUnique = False
        RadListDataItem1.Text = "By Road"
        RadListDataItem2.Text = "By Air"
        RadListDataItem3.Text = "By Sea"
        Me.cboInCatg.Items.Add(RadListDataItem1)
        Me.cboInCatg.Items.Add(RadListDataItem2)
        Me.cboInCatg.Items.Add(RadListDataItem3)
        Me.cboInCatg.Location = New System.Drawing.Point(75, 1)
        Me.cboInCatg.MendatroryField = False
        Me.cboInCatg.MyLinkLable1 = Me.MyLabel2
        Me.cboInCatg.MyLinkLable2 = Nothing
        Me.cboInCatg.Name = "cboInCatg"
        Me.cboInCatg.ReferenceFieldDesc = Nothing
        Me.cboInCatg.ReferenceFieldName = Nothing
        Me.cboInCatg.ReferenceTableName = Nothing
        Me.cboInCatg.Size = New System.Drawing.Size(176, 18)
        Me.cboInCatg.TabIndex = 343
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(1, 1)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(73, 18)
        Me.MyLabel3.TabIndex = 346
        Me.MyLabel3.Text = "Out Category"
        '
        'cboOutCatg
        '
        Me.cboOutCatg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOutCatg.AutoCompleteDisplayMember = Nothing
        Me.cboOutCatg.AutoCompleteValueMember = Nothing
        Me.cboOutCatg.CalculationExpression = Nothing
        Me.cboOutCatg.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOutCatg.FieldCode = Nothing
        Me.cboOutCatg.FieldDesc = Nothing
        Me.cboOutCatg.FieldMaxLength = 0
        Me.cboOutCatg.FieldName = Nothing
        Me.cboOutCatg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOutCatg.isCalculatedField = False
        Me.cboOutCatg.IsSourceFromTable = False
        Me.cboOutCatg.IsSourceFromValueList = False
        Me.cboOutCatg.IsUnique = False
        RadListDataItem4.Text = "By Road"
        RadListDataItem5.Text = "By Air"
        RadListDataItem6.Text = "By Sea"
        Me.cboOutCatg.Items.Add(RadListDataItem4)
        Me.cboOutCatg.Items.Add(RadListDataItem5)
        Me.cboOutCatg.Items.Add(RadListDataItem6)
        Me.cboOutCatg.Location = New System.Drawing.Point(81, 1)
        Me.cboOutCatg.MendatroryField = False
        Me.cboOutCatg.MyLinkLable1 = Me.MyLabel3
        Me.cboOutCatg.MyLinkLable2 = Nothing
        Me.cboOutCatg.Name = "cboOutCatg"
        Me.cboOutCatg.ReferenceFieldDesc = Nothing
        Me.cboOutCatg.ReferenceFieldName = Nothing
        Me.cboOutCatg.ReferenceTableName = Nothing
        Me.cboOutCatg.Size = New System.Drawing.Size(167, 18)
        Me.cboOutCatg.TabIndex = 345
        '
        'txtType
        '
        Me.txtType.CalculationExpression = Nothing
        Me.txtType.FieldCode = Nothing
        Me.txtType.FieldDesc = Nothing
        Me.txtType.FieldMaxLength = 0
        Me.txtType.FieldName = Nothing
        Me.txtType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtType.isCalculatedField = False
        Me.txtType.IsSourceFromTable = False
        Me.txtType.IsSourceFromValueList = False
        Me.txtType.IsUnique = False
        Me.txtType.Location = New System.Drawing.Point(81, 79)
        Me.txtType.MaxLength = 100
        Me.txtType.MendatroryField = False
        Me.txtType.MyLinkLable1 = Me.MyLabel1
        Me.txtType.MyLinkLable2 = Nothing
        Me.txtType.Name = "txtType"
        Me.txtType.ReferenceFieldDesc = Nothing
        Me.txtType.ReferenceFieldName = Nothing
        Me.txtType.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtType.RootElement.StretchVertically = True
        Me.txtType.Size = New System.Drawing.Size(348, 20)
        Me.txtType.TabIndex = 343
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 81)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 344
        Me.MyLabel1.Text = "Type"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(10, 102)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel7.TabIndex = 342
        Me.MyLabel7.Text = "In/Out Type"
        '
        'cboInOutType
        '
        Me.cboInOutType.AutoCompleteDisplayMember = Nothing
        Me.cboInOutType.AutoCompleteValueMember = Nothing
        Me.cboInOutType.CalculationExpression = Nothing
        Me.cboInOutType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboInOutType.FieldCode = Nothing
        Me.cboInOutType.FieldDesc = Nothing
        Me.cboInOutType.FieldMaxLength = 0
        Me.cboInOutType.FieldName = Nothing
        Me.cboInOutType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInOutType.isCalculatedField = False
        Me.cboInOutType.IsSourceFromTable = False
        Me.cboInOutType.IsSourceFromValueList = False
        Me.cboInOutType.IsUnique = False
        RadListDataItem7.Text = "By Road"
        RadListDataItem8.Text = "By Air"
        RadListDataItem9.Text = "By Sea"
        Me.cboInOutType.Items.Add(RadListDataItem7)
        Me.cboInOutType.Items.Add(RadListDataItem8)
        Me.cboInOutType.Items.Add(RadListDataItem9)
        Me.cboInOutType.Location = New System.Drawing.Point(81, 102)
        Me.cboInOutType.MendatroryField = False
        Me.cboInOutType.MyLinkLable1 = Me.MyLabel7
        Me.cboInOutType.MyLinkLable2 = Nothing
        Me.cboInOutType.Name = "cboInOutType"
        Me.cboInOutType.ReferenceFieldDesc = Nothing
        Me.cboInOutType.ReferenceFieldName = Nothing
        Me.cboInOutType.ReferenceTableName = Nothing
        Me.cboInOutType.Size = New System.Drawing.Size(176, 18)
        Me.cboInOutType.TabIndex = 341
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(560, 20)
        Me.RadMenu1.TabIndex = 47
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "RadMenuItem2"
        Me.rmImport.AccessibleName = "RadMenuItem2"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "RadMenuItem3"
        Me.rmExport.AccessibleName = "RadMenuItem3"
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'fndaccgp
        '
        Me.fndaccgp.FieldName = Nothing
        Me.fndaccgp.Location = New System.Drawing.Point(81, 32)
        Me.fndaccgp.MendatroryField = True
        Me.fndaccgp.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndaccgp.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccgp.MyLinkLable1 = Me.lblaccgp
        Me.fndaccgp.MyLinkLable2 = Nothing
        Me.fndaccgp.MyMaxLength = 20
        Me.fndaccgp.MyReadOnly = False
        Me.fndaccgp.Name = "fndaccgp"
        Me.fndaccgp.Size = New System.Drawing.Size(219, 21)
        Me.fndaccgp.TabIndex = 0
        Me.fndaccgp.TabStop = False
        Me.fndaccgp.Value = ""
        '
        'lblaccgp
        '
        Me.lblaccgp.FieldName = Nothing
        Me.lblaccgp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblaccgp.Location = New System.Drawing.Point(10, 34)
        Me.lblaccgp.Name = "lblaccgp"
        Me.lblaccgp.Size = New System.Drawing.Size(68, 16)
        Me.lblaccgp.TabIndex = 4
        Me.lblaccgp.Text = "Souce Code"
        '
        'txtdes
        '
        Me.txtdes.CalculationExpression = Nothing
        Me.txtdes.FieldCode = Nothing
        Me.txtdes.FieldDesc = Nothing
        Me.txtdes.FieldMaxLength = 0
        Me.txtdes.FieldName = Nothing
        Me.txtdes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.isCalculatedField = False
        Me.txtdes.IsSourceFromTable = False
        Me.txtdes.IsSourceFromValueList = False
        Me.txtdes.IsUnique = False
        Me.txtdes.Location = New System.Drawing.Point(81, 56)
        Me.txtdes.MaxLength = 50
        Me.txtdes.MendatroryField = True
        Me.txtdes.MyLinkLable1 = Me.lbldes
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        Me.txtdes.ReferenceFieldDesc = Nothing
        Me.txtdes.ReferenceFieldName = Nothing
        Me.txtdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(348, 20)
        Me.txtdes.TabIndex = 1
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(10, 58)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 5
        Me.lbldes.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(300, 32)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 3
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(491, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(5, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(74, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'txtSequence
        '
        Me.txtSequence.CalculationExpression = Nothing
        Me.txtSequence.FieldCode = Nothing
        Me.txtSequence.FieldDesc = Nothing
        Me.txtSequence.FieldMaxLength = 0
        Me.txtSequence.FieldName = Nothing
        Me.txtSequence.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSequence.isCalculatedField = False
        Me.txtSequence.IsSourceFromTable = False
        Me.txtSequence.IsSourceFromValueList = False
        Me.txtSequence.IsUnique = False
        Me.txtSequence.Location = New System.Drawing.Point(81, 149)
        Me.txtSequence.MaxLength = 100
        Me.txtSequence.MendatroryField = False
        Me.txtSequence.MyLinkLable1 = Me.MyLabel4
        Me.txtSequence.MyLinkLable2 = Nothing
        Me.txtSequence.Name = "txtSequence"
        Me.txtSequence.ReferenceFieldDesc = Nothing
        Me.txtSequence.ReferenceFieldName = Nothing
        Me.txtSequence.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtSequence.RootElement.StretchVertically = True
        Me.txtSequence.Size = New System.Drawing.Size(176, 20)
        Me.txtSequence.TabIndex = 346
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(10, 151)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel4.TabIndex = 347
        Me.MyLabel4.Text = "Sequence"
        '
        'frmInventorySourceCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 244)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmInventorySourceCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Inventory Souce Code"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboInCatg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOutCatg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboInOutType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSequence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndaccgp As common.UserControls.txtNavigator
    Friend WithEvents lblaccgp As common.Controls.MyLabel
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Protected WithEvents cboInOutType As common.Controls.MyComboBox
    Friend WithEvents txtType As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Protected WithEvents cboInCatg As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Protected WithEvents cboOutCatg As common.Controls.MyComboBox
    Friend WithEvents txtSequence As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

