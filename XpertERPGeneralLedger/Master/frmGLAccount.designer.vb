<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLAccount
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
        Dim GridViewMultiComboBoxColumn1 As Telerik.WinControls.UI.GridViewMultiComboBoxColumn = New Telerik.WinControls.UI.GridViewMultiComboBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn()
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn1 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewComboBoxColumn2 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Basic = New Telerik.WinControls.UI.RadMenuItem()
        Me.Rollup = New Telerik.WinControls.UI.RadMenuItem()
        Me.ImportRollupSeq = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BasicEx = New Telerik.WinControls.UI.RadMenuItem()
        Me.RollUpex = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExportRollupSorting = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.lblaccount = New common.Controls.MyLabel()
        Me.lbldescription = New common.Controls.MyLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtAccountSubGroup = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblSubGroup = New common.Controls.MyTextBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnNA = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnPurhcase = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnSale = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblType = New common.Controls.MyLabel()
        Me.cboTaxType = New common.Controls.MyComboBox()
        Me.fndstructurecode = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtclosetoaccount = New common.Controls.MyTextBox()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.ddlclosetosegment = New common.Controls.MyComboBox()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.chkmulticurrency = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkautoallocation = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkcontrolaccount = New Telerik.WinControls.UI.RadCheckBox()
        Me.grpstatus = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdactive = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdinactive = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.ddlnormalbal = New common.Controls.MyComboBox()
        Me.txtstrdesc = New common.Controls.MyTextBox()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.dgvsubledger = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.fndsourcecode = New common.UserControls.txtFinder()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.dgvallocation = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.dgvsegment = New common.UserControls.MyRadGridView()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndaccount = New common.UserControls.txtNavigator()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rbtnNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnPurhcase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTaxType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtclosetoaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlclosetosegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkmulticurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkautoallocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcontrolaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpstatus.SuspendLayout()
        CType(Me.rdactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdinactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlnormalbal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstrdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.dgvsubledger, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvsubledger.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvallocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvallocation.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.dgvsegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvsegment.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(957, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Basic, Me.Rollup, Me.ImportRollupSeq, Me.RadMenuItem6})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'Basic
        '
        Me.Basic.Name = "Basic"
        Me.Basic.Text = "Basic Profile"
        Me.Basic.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'Rollup
        '
        Me.Rollup.Name = "Rollup"
        Me.Rollup.Text = "Roll Up"
        Me.Rollup.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'ImportRollupSeq
        '
        Me.ImportRollupSeq.Name = "ImportRollupSeq"
        Me.ImportRollupSeq.Text = "Import RollUp A/c  Sorting"
        Me.ImportRollupSeq.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AccessibleDescription = "mnuCombinedImport"
        Me.RadMenuItem6.AccessibleName = "mnuCombinedImport"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "GL Account"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BasicEx, Me.RollUpex, Me.ExportRollupSorting, Me.RadMenuItem5})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'BasicEx
        '
        Me.BasicEx.Name = "BasicEx"
        Me.BasicEx.Text = "Basic Profile"
        Me.BasicEx.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'RollUpex
        '
        Me.RollUpex.Name = "RollUpex"
        Me.RollUpex.Text = "Roll Up"
        Me.RollUpex.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'ExportRollupSorting
        '
        Me.ExportRollupSorting.Name = "ExportRollupSorting"
        Me.ExportRollupSorting.Text = "Export RollUp A/c  Sorting"
        Me.ExportRollupSorting.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "mnuCombined"
        Me.RadMenuItem5.AccessibleName = "mnuCombined"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "GL Account"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Close"
        '
        'lblaccount
        '
        Me.lblaccount.FieldName = Nothing
        Me.lblaccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblaccount.Location = New System.Drawing.Point(16, 10)
        Me.lblaccount.Name = "lblaccount"
        Me.lblaccount.Size = New System.Drawing.Size(47, 16)
        Me.lblaccount.TabIndex = 1
        Me.lblaccount.Text = "Account"
        '
        'lbldescription
        '
        Me.lbldescription.FieldName = Nothing
        Me.lbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldescription.Location = New System.Drawing.Point(16, 38)
        Me.lbldescription.Name = "lbldescription"
        Me.lbldescription.Size = New System.Drawing.Size(63, 16)
        Me.lbldescription.TabIndex = 2
        Me.lbldescription.Text = "Description"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.WindowText
        Me.TextBox1.Location = New System.Drawing.Point(0, 26)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(584, 1)
        Me.TextBox1.TabIndex = 0
        '
        'btnreset
        '
        Me.btnreset.Image = Global.XpertERPGeneralLedger.My.Resources.Resources._new
        Me.btnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreset.Location = New System.Drawing.Point(362, 11)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(19, 21)
        Me.btnreset.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(6, 60)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(945, 402)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtAccountSubGroup)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblSubGroup)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.lblType)
        Me.RadPageViewPage1.Controls.Add(Me.cboTaxType)
        Me.RadPageViewPage1.Controls.Add(Me.fndstructurecode)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.chkmulticurrency)
        Me.RadPageViewPage1.Controls.Add(Me.chkautoallocation)
        Me.RadPageViewPage1.Controls.Add(Me.chkcontrolaccount)
        Me.RadPageViewPage1.Controls.Add(Me.grpstatus)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.ddlnormalbal)
        Me.RadPageViewPage1.Controls.Add(Me.txtstrdesc)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(51.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(924, 356)
        Me.RadPageViewPage1.Text = "Details"
        '
        'txtAccountSubGroup
        '
        Me.txtAccountSubGroup.CalculationExpression = Nothing
        Me.txtAccountSubGroup.FieldCode = Nothing
        Me.txtAccountSubGroup.FieldDesc = Nothing
        Me.txtAccountSubGroup.FieldMaxLength = 0
        Me.txtAccountSubGroup.FieldName = Nothing
        Me.txtAccountSubGroup.isCalculatedField = False
        Me.txtAccountSubGroup.IsSourceFromTable = False
        Me.txtAccountSubGroup.IsSourceFromValueList = False
        Me.txtAccountSubGroup.IsUnique = False
        Me.txtAccountSubGroup.Location = New System.Drawing.Point(102, 48)
        Me.txtAccountSubGroup.MendatroryField = True
        Me.txtAccountSubGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountSubGroup.MyLinkLable1 = Me.MyLabel1
        Me.txtAccountSubGroup.MyLinkLable2 = Nothing
        Me.txtAccountSubGroup.MyReadOnly = False
        Me.txtAccountSubGroup.MyShowMasterFormButton = False
        Me.txtAccountSubGroup.Name = "txtAccountSubGroup"
        Me.txtAccountSubGroup.ReferenceFieldDesc = Nothing
        Me.txtAccountSubGroup.ReferenceFieldName = Nothing
        Me.txtAccountSubGroup.ReferenceTableName = Nothing
        Me.txtAccountSubGroup.Size = New System.Drawing.Size(155, 19)
        Me.txtAccountSubGroup.TabIndex = 34
        Me.txtAccountSubGroup.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(3, 48)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel1.TabIndex = 36
        Me.MyLabel1.Text = "Main Account"
        '
        'lblSubGroup
        '
        Me.lblSubGroup.CalculationExpression = Nothing
        Me.lblSubGroup.FieldCode = Nothing
        Me.lblSubGroup.FieldDesc = Nothing
        Me.lblSubGroup.FieldMaxLength = 0
        Me.lblSubGroup.FieldName = Nothing
        Me.lblSubGroup.isCalculatedField = False
        Me.lblSubGroup.IsSourceFromTable = False
        Me.lblSubGroup.IsSourceFromValueList = False
        Me.lblSubGroup.IsUnique = False
        Me.lblSubGroup.Location = New System.Drawing.Point(275, 47)
        Me.lblSubGroup.MaxLength = 90
        Me.lblSubGroup.MendatroryField = False
        Me.lblSubGroup.MyLinkLable1 = Me.MyLabel1
        Me.lblSubGroup.MyLinkLable2 = Nothing
        Me.lblSubGroup.Name = "lblSubGroup"
        Me.lblSubGroup.ReadOnly = True
        Me.lblSubGroup.ReferenceFieldDesc = Nothing
        Me.lblSubGroup.ReferenceFieldName = Nothing
        Me.lblSubGroup.ReferenceTableName = Nothing
        Me.lblSubGroup.Size = New System.Drawing.Size(436, 20)
        Me.lblSubGroup.TabIndex = 35
        Me.lblSubGroup.TabStop = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rbtnNA)
        Me.RadGroupBox3.Controls.Add(Me.rbtnPurhcase)
        Me.RadGroupBox3.Controls.Add(Me.rbtnSale)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(275, 70)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(166, 22)
        Me.RadGroupBox3.TabIndex = 7
        '
        'rbtnNA
        '
        Me.rbtnNA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnNA.Location = New System.Drawing.Point(117, 2)
        Me.rbtnNA.Name = "rbtnNA"
        Me.rbtnNA.Size = New System.Drawing.Size(36, 18)
        Me.rbtnNA.TabIndex = 2
        Me.rbtnNA.Text = "NA"
        Me.rbtnNA.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnPurhcase
        '
        Me.rbtnPurhcase.Location = New System.Drawing.Point(7, 1)
        Me.rbtnPurhcase.Name = "rbtnPurhcase"
        Me.rbtnPurhcase.Size = New System.Drawing.Size(65, 18)
        Me.rbtnPurhcase.TabIndex = 0
        Me.rbtnPurhcase.Text = "Purchase"
        '
        'rbtnSale
        '
        Me.rbtnSale.Location = New System.Drawing.Point(74, 1)
        Me.rbtnSale.Name = "rbtnSale"
        Me.rbtnSale.Size = New System.Drawing.Size(41, 18)
        Me.rbtnSale.TabIndex = 1
        Me.rbtnSale.Text = "Sale"
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Location = New System.Drawing.Point(3, 72)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(50, 18)
        Me.lblType.TabIndex = 33
        Me.lblType.Text = "Tax Type"
        '
        'cboTaxType
        '
        Me.cboTaxType.AutoCompleteDisplayMember = Nothing
        Me.cboTaxType.AutoCompleteValueMember = Nothing
        Me.cboTaxType.CalculationExpression = Nothing
        Me.cboTaxType.DropDownAnimationEnabled = True
        Me.cboTaxType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTaxType.FieldCode = Nothing
        Me.cboTaxType.FieldDesc = Nothing
        Me.cboTaxType.FieldMaxLength = 0
        Me.cboTaxType.FieldName = Nothing
        Me.cboTaxType.isCalculatedField = False
        Me.cboTaxType.IsSourceFromTable = False
        Me.cboTaxType.IsSourceFromValueList = False
        Me.cboTaxType.IsUnique = False
        Me.cboTaxType.Location = New System.Drawing.Point(102, 71)
        Me.cboTaxType.MendatroryField = False
        Me.cboTaxType.MyLinkLable1 = Me.lblType
        Me.cboTaxType.MyLinkLable2 = Nothing
        Me.cboTaxType.Name = "cboTaxType"
        Me.cboTaxType.ReferenceFieldDesc = Nothing
        Me.cboTaxType.ReferenceFieldName = Nothing
        Me.cboTaxType.ReferenceTableName = Nothing
        Me.cboTaxType.Size = New System.Drawing.Size(155, 20)
        Me.cboTaxType.TabIndex = 6
        '
        'fndstructurecode
        '
        Me.fndstructurecode.CalculationExpression = Nothing
        Me.fndstructurecode.FieldCode = Nothing
        Me.fndstructurecode.FieldDesc = Nothing
        Me.fndstructurecode.FieldMaxLength = 0
        Me.fndstructurecode.FieldName = Nothing
        Me.fndstructurecode.isCalculatedField = False
        Me.fndstructurecode.IsSourceFromTable = False
        Me.fndstructurecode.IsSourceFromValueList = False
        Me.fndstructurecode.IsUnique = False
        Me.fndstructurecode.Location = New System.Drawing.Point(102, 3)
        Me.fndstructurecode.MendatroryField = False
        Me.fndstructurecode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstructurecode.MyLinkLable1 = Me.RadLabel1
        Me.fndstructurecode.MyLinkLable2 = Nothing
        Me.fndstructurecode.MyReadOnly = False
        Me.fndstructurecode.MyShowMasterFormButton = False
        Me.fndstructurecode.Name = "fndstructurecode"
        Me.fndstructurecode.ReferenceFieldDesc = Nothing
        Me.fndstructurecode.ReferenceFieldName = Nothing
        Me.fndstructurecode.ReferenceTableName = Nothing
        Me.fndstructurecode.Size = New System.Drawing.Size(155, 19)
        Me.fndstructurecode.TabIndex = 0
        Me.fndstructurecode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(81, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Structure Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtclosetoaccount)
        Me.RadGroupBox1.Controls.Add(Me.ddlclosetosegment)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Close To"
        Me.RadGroupBox1.Location = New System.Drawing.Point(11, 140)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(713, 71)
        Me.RadGroupBox1.TabIndex = 13
        Me.RadGroupBox1.Text = "Close To"
        Me.RadGroupBox1.Visible = False
        '
        'txtclosetoaccount
        '
        Me.txtclosetoaccount.CalculationExpression = Nothing
        Me.txtclosetoaccount.FieldCode = Nothing
        Me.txtclosetoaccount.FieldDesc = Nothing
        Me.txtclosetoaccount.FieldMaxLength = 0
        Me.txtclosetoaccount.FieldName = Nothing
        Me.txtclosetoaccount.isCalculatedField = False
        Me.txtclosetoaccount.IsSourceFromTable = False
        Me.txtclosetoaccount.IsSourceFromValueList = False
        Me.txtclosetoaccount.IsUnique = False
        Me.txtclosetoaccount.Location = New System.Drawing.Point(83, 45)
        Me.txtclosetoaccount.MendatroryField = False
        Me.txtclosetoaccount.MyLinkLable1 = Me.RadLabel6
        Me.txtclosetoaccount.MyLinkLable2 = Nothing
        Me.txtclosetoaccount.Name = "txtclosetoaccount"
        Me.txtclosetoaccount.ReadOnly = True
        Me.txtclosetoaccount.ReferenceFieldDesc = Nothing
        Me.txtclosetoaccount.ReferenceFieldName = Nothing
        Me.txtclosetoaccount.ReferenceTableName = Nothing
        Me.txtclosetoaccount.Size = New System.Drawing.Size(624, 20)
        Me.txtclosetoaccount.TabIndex = 1
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(13, 45)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(47, 16)
        Me.RadLabel6.TabIndex = 2
        Me.RadLabel6.Text = "Account"
        '
        'ddlclosetosegment
        '
        Me.ddlclosetosegment.AutoCompleteDisplayMember = Nothing
        Me.ddlclosetosegment.AutoCompleteValueMember = Nothing
        Me.ddlclosetosegment.CalculationExpression = Nothing
        Me.ddlclosetosegment.DropDownAnimationEnabled = True
        Me.ddlclosetosegment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlclosetosegment.FieldCode = Nothing
        Me.ddlclosetosegment.FieldDesc = Nothing
        Me.ddlclosetosegment.FieldMaxLength = 0
        Me.ddlclosetosegment.FieldName = Nothing
        Me.ddlclosetosegment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlclosetosegment.isCalculatedField = False
        Me.ddlclosetosegment.IsSourceFromTable = False
        Me.ddlclosetosegment.IsSourceFromValueList = False
        Me.ddlclosetosegment.IsUnique = False
        Me.ddlclosetosegment.Location = New System.Drawing.Point(83, 21)
        Me.ddlclosetosegment.MendatroryField = False
        Me.ddlclosetosegment.MyLinkLable1 = Me.RadLabel5
        Me.ddlclosetosegment.MyLinkLable2 = Nothing
        Me.ddlclosetosegment.Name = "ddlclosetosegment"
        Me.ddlclosetosegment.ReferenceFieldDesc = Nothing
        Me.ddlclosetosegment.ReferenceFieldName = Nothing
        Me.ddlclosetosegment.ReferenceTableName = Nothing
        Me.ddlclosetosegment.Size = New System.Drawing.Size(218, 18)
        Me.ddlclosetosegment.TabIndex = 0
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(13, 21)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(52, 16)
        Me.RadLabel5.TabIndex = 0
        Me.RadLabel5.Text = "Segment"
        '
        'chkmulticurrency
        '
        Me.chkmulticurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkmulticurrency.Location = New System.Drawing.Point(274, 111)
        Me.chkmulticurrency.Name = "chkmulticurrency"
        Me.chkmulticurrency.Size = New System.Drawing.Size(90, 16)
        Me.chkmulticurrency.TabIndex = 12
        Me.chkmulticurrency.Text = "MultiCurrency"
        Me.chkmulticurrency.Visible = False
        '
        'chkautoallocation
        '
        Me.chkautoallocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkautoallocation.Location = New System.Drawing.Point(379, 111)
        Me.chkautoallocation.Name = "chkautoallocation"
        Me.chkautoallocation.Size = New System.Drawing.Size(96, 16)
        Me.chkautoallocation.TabIndex = 9
        Me.chkautoallocation.Text = "Auto Allocation"
        Me.chkautoallocation.Visible = False
        '
        'chkcontrolaccount
        '
        Me.chkcontrolaccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcontrolaccount.Location = New System.Drawing.Point(161, 111)
        Me.chkcontrolaccount.Name = "chkcontrolaccount"
        Me.chkcontrolaccount.Size = New System.Drawing.Size(101, 16)
        Me.chkcontrolaccount.TabIndex = 11
        Me.chkcontrolaccount.Text = "Control Account"
        Me.chkcontrolaccount.Visible = False
        '
        'grpstatus
        '
        Me.grpstatus.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpstatus.Controls.Add(Me.rdactive)
        Me.grpstatus.Controls.Add(Me.rdinactive)
        Me.grpstatus.HeaderText = "Status"
        Me.grpstatus.Location = New System.Drawing.Point(11, 97)
        Me.grpstatus.Name = "grpstatus"
        Me.grpstatus.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpstatus.Size = New System.Drawing.Size(142, 37)
        Me.grpstatus.TabIndex = 8
        Me.grpstatus.Text = "Status"
        '
        'rdactive
        '
        Me.rdactive.Location = New System.Drawing.Point(13, 12)
        Me.rdactive.Name = "rdactive"
        Me.rdactive.Size = New System.Drawing.Size(51, 18)
        Me.rdactive.TabIndex = 0
        Me.rdactive.Text = "Active"
        '
        'rdinactive
        '
        Me.rdinactive.Location = New System.Drawing.Point(70, 12)
        Me.rdinactive.Name = "rdinactive"
        Me.rdinactive.Size = New System.Drawing.Size(60, 18)
        Me.rdinactive.TabIndex = 1
        Me.rdinactive.Text = "InActive"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(3, 26)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(85, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Normal Balance"
        '
        'ddlnormalbal
        '
        Me.ddlnormalbal.AutoCompleteDisplayMember = Nothing
        Me.ddlnormalbal.AutoCompleteValueMember = Nothing
        Me.ddlnormalbal.CalculationExpression = Nothing
        Me.ddlnormalbal.DropDownAnimationEnabled = True
        Me.ddlnormalbal.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlnormalbal.FieldCode = Nothing
        Me.ddlnormalbal.FieldDesc = Nothing
        Me.ddlnormalbal.FieldMaxLength = 0
        Me.ddlnormalbal.FieldName = Nothing
        Me.ddlnormalbal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlnormalbal.isCalculatedField = False
        Me.ddlnormalbal.IsSourceFromTable = False
        Me.ddlnormalbal.IsSourceFromValueList = False
        Me.ddlnormalbal.IsUnique = False
        RadListDataItem1.Text = "Credit"
        RadListDataItem2.Text = "Debit"
        Me.ddlnormalbal.Items.Add(RadListDataItem1)
        Me.ddlnormalbal.Items.Add(RadListDataItem2)
        Me.ddlnormalbal.Location = New System.Drawing.Point(102, 26)
        Me.ddlnormalbal.MendatroryField = False
        Me.ddlnormalbal.MyLinkLable1 = Me.RadLabel2
        Me.ddlnormalbal.MyLinkLable2 = Nothing
        Me.ddlnormalbal.Name = "ddlnormalbal"
        Me.ddlnormalbal.ReferenceFieldDesc = Nothing
        Me.ddlnormalbal.ReferenceFieldName = Nothing
        Me.ddlnormalbal.ReferenceTableName = Nothing
        Me.ddlnormalbal.Size = New System.Drawing.Size(155, 18)
        Me.ddlnormalbal.TabIndex = 2
        Me.ddlnormalbal.Text = "select"
        '
        'txtstrdesc
        '
        Me.txtstrdesc.CalculationExpression = Nothing
        Me.txtstrdesc.FieldCode = Nothing
        Me.txtstrdesc.FieldDesc = Nothing
        Me.txtstrdesc.FieldMaxLength = 0
        Me.txtstrdesc.FieldName = Nothing
        Me.txtstrdesc.isCalculatedField = False
        Me.txtstrdesc.IsSourceFromTable = False
        Me.txtstrdesc.IsSourceFromValueList = False
        Me.txtstrdesc.IsUnique = False
        Me.txtstrdesc.Location = New System.Drawing.Point(275, 2)
        Me.txtstrdesc.MaxLength = 55
        Me.txtstrdesc.MendatroryField = False
        Me.txtstrdesc.MyLinkLable1 = Me.RadLabel1
        Me.txtstrdesc.MyLinkLable2 = Nothing
        Me.txtstrdesc.Name = "txtstrdesc"
        Me.txtstrdesc.ReadOnly = True
        Me.txtstrdesc.ReferenceFieldDesc = Nothing
        Me.txtstrdesc.ReferenceFieldName = Nothing
        Me.txtstrdesc.ReferenceTableName = Nothing
        Me.txtstrdesc.Size = New System.Drawing.Size(436, 20)
        Me.txtstrdesc.TabIndex = 1
        Me.txtstrdesc.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.dgvsubledger)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(72.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(924, 356)
        Me.RadPageViewPage3.Text = "SubLedger"
        '
        'dgvsubledger
        '
        Me.dgvsubledger.BackColor = System.Drawing.Color.White
        Me.dgvsubledger.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvsubledger.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvsubledger.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvsubledger.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvsubledger.Location = New System.Drawing.Point(14, 19)
        '
        '
        '
        Me.dgvsubledger.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewMultiComboBoxColumn1.HeaderText = "Sub Ledger"
        GridViewMultiComboBoxColumn1.Name = "column1"
        GridViewMultiComboBoxColumn1.Width = 180
        Me.dgvsubledger.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewMultiComboBoxColumn1})
        Me.dgvsubledger.MasterTemplate.EnableGrouping = False
        Me.dgvsubledger.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvsubledger.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvsubledger.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.dgvsubledger.MyStopExport = False
        Me.dgvsubledger.Name = "dgvsubledger"
        Me.dgvsubledger.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvsubledger.ShowHeaderCellButtons = True
        Me.dgvsubledger.Size = New System.Drawing.Size(199, 283)
        Me.dgvsubledger.TabIndex = 0
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.fndsourcecode)
        Me.RadPageViewPage4.Controls.Add(Me.dgvallocation)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(65.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(924, 356)
        Me.RadPageViewPage4.Text = "Allocation"
        '
        'fndsourcecode
        '
        Me.fndsourcecode.CalculationExpression = Nothing
        Me.fndsourcecode.FieldCode = Nothing
        Me.fndsourcecode.FieldDesc = Nothing
        Me.fndsourcecode.FieldMaxLength = 0
        Me.fndsourcecode.FieldName = Nothing
        Me.fndsourcecode.isCalculatedField = False
        Me.fndsourcecode.IsSourceFromTable = False
        Me.fndsourcecode.IsSourceFromValueList = False
        Me.fndsourcecode.IsUnique = False
        Me.fndsourcecode.Location = New System.Drawing.Point(93, 16)
        Me.fndsourcecode.MendatroryField = False
        Me.fndsourcecode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndsourcecode.MyLinkLable1 = Me.RadLabel8
        Me.fndsourcecode.MyLinkLable2 = Nothing
        Me.fndsourcecode.MyReadOnly = False
        Me.fndsourcecode.MyShowMasterFormButton = False
        Me.fndsourcecode.Name = "fndsourcecode"
        Me.fndsourcecode.ReferenceFieldDesc = Nothing
        Me.fndsourcecode.ReferenceFieldName = Nothing
        Me.fndsourcecode.ReferenceTableName = Nothing
        Me.fndsourcecode.Size = New System.Drawing.Size(206, 19)
        Me.fndsourcecode.TabIndex = 3
        Me.fndsourcecode.Value = ""
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Location = New System.Drawing.Point(14, 16)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(72, 16)
        Me.RadLabel8.TabIndex = 0
        Me.RadLabel8.Text = "Source Code"
        '
        'dgvallocation
        '
        Me.dgvallocation.BackColor = System.Drawing.Color.White
        Me.dgvallocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvallocation.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvallocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvallocation.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvallocation.Location = New System.Drawing.Point(14, 51)
        '
        '
        '
        Me.dgvallocation.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewComboBoxColumn1.HeaderText = "Account"
        GridViewComboBoxColumn1.Name = "column1"
        GridViewComboBoxColumn1.Width = 130
        GridViewTextBoxColumn1.HeaderText = "Account's Description"
        GridViewTextBoxColumn1.Name = "column2"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 145
        GridViewTextBoxColumn2.HeaderText = "Refrence"
        GridViewTextBoxColumn2.MaxLength = 49
        GridViewTextBoxColumn2.Name = "column3"
        GridViewTextBoxColumn2.Width = 150
        GridViewTextBoxColumn3.HeaderText = "Description"
        GridViewTextBoxColumn3.MaxLength = 49
        GridViewTextBoxColumn3.Name = "column4"
        GridViewTextBoxColumn3.Width = 150
        GridViewDecimalColumn1.HeaderText = "Percentage"
        GridViewDecimalColumn1.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        GridViewDecimalColumn1.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        GridViewDecimalColumn1.Name = "column5"
        GridViewDecimalColumn1.Width = 100
        Me.dgvallocation.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewComboBoxColumn1, GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewDecimalColumn1})
        Me.dgvallocation.MasterTemplate.EnableGrouping = False
        Me.dgvallocation.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvallocation.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvallocation.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.dgvallocation.MyStopExport = False
        Me.dgvallocation.Name = "dgvallocation"
        Me.dgvallocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvallocation.ShowHeaderCellButtons = True
        Me.dgvallocation.Size = New System.Drawing.Size(858, 267)
        Me.dgvallocation.TabIndex = 2
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.dgvsegment)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(62.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(924, 356)
        Me.RadPageViewPage5.Text = "Segment"
        '
        'dgvsegment
        '
        Me.dgvsegment.BackColor = System.Drawing.Color.White
        Me.dgvsegment.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvsegment.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvsegment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvsegment.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvsegment.Location = New System.Drawing.Point(5, 30)
        '
        '
        '
        Me.dgvsegment.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn4.HeaderText = "Segment Name"
        GridViewTextBoxColumn4.Name = "column1"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.Width = 200
        GridViewComboBoxColumn2.HeaderText = "Segment Code"
        GridViewComboBoxColumn2.Name = "column2"
        GridViewComboBoxColumn2.Width = 200
        GridViewTextBoxColumn5.HeaderText = "Description"
        GridViewTextBoxColumn5.Name = "column3"
        GridViewTextBoxColumn5.ReadOnly = True
        GridViewTextBoxColumn5.Width = 250
        Me.dgvsegment.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn4, GridViewComboBoxColumn2, GridViewTextBoxColumn5})
        Me.dgvsegment.MasterTemplate.EnableGrouping = False
        Me.dgvsegment.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvsegment.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvsegment.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.dgvsegment.MyStopExport = False
        Me.dgvsegment.Name = "dgvsegment"
        Me.dgvsegment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvsegment.ShowHeaderCellButtons = True
        Me.dgvsegment.Size = New System.Drawing.Size(856, 292)
        Me.dgvsegment.TabIndex = 0
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(72, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(878, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'RadPanel1
        '
        Me.RadPanel1.Location = New System.Drawing.Point(574, 26)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1, 445)
        Me.RadPanel1.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndaccount)
        Me.RadGroupBox2.Controls.Add(Me.RadPageView1)
        Me.RadGroupBox2.Controls.Add(Me.lblaccount)
        Me.RadGroupBox2.Controls.Add(Me.btnreset)
        Me.RadGroupBox2.Controls.Add(Me.lbldescription)
        Me.RadGroupBox2.Controls.Add(Me.txtdesc)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(957, 468)
        Me.RadGroupBox2.TabIndex = 1
        '
        'fndaccount
        '
        Me.fndaccount.FieldName = Nothing
        Me.fndaccount.Location = New System.Drawing.Point(91, 11)
        Me.fndaccount.MendatroryField = False
        Me.fndaccount.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndaccount.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccount.MyLinkLable1 = Nothing
        Me.fndaccount.MyLinkLable2 = Nothing
        Me.fndaccount.MyMaxLength = 30
        Me.fndaccount.MyReadOnly = False
        Me.fndaccount.Name = "fndaccount"
        Me.fndaccount.Size = New System.Drawing.Size(271, 21)
        Me.fndaccount.TabIndex = 0
        Me.fndaccount.Value = ""
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(91, 38)
        Me.txtdesc.MaxLength = 60
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.lbldescription
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(675, 18)
        Me.txtdesc.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(957, 497)
        Me.SplitContainer1.SplitterDistance = 468
        Me.SplitContainer1.TabIndex = 17
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(957, 468)
        Me.Panel1.TabIndex = 0
        '
        'frmGLAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(957, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGLAccount"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "GL Account"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rbtnNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnPurhcase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTaxType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtclosetoaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlclosetosegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkmulticurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkautoallocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcontrolaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpstatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpstatus.ResumeLayout(False)
        Me.grpstatus.PerformLayout()
        CType(Me.rdactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdinactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlnormalbal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstrdesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.dgvsubledger.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvsubledger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvallocation.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvallocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.dgvsegment.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvsegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtstrdesc As common.Controls.MyTextBox
    Friend WithEvents ddlnormalbal As common.Controls.MyComboBox
    Friend WithEvents grpstatus As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdactive As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdinactive As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkmulticurrency As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkautoallocation As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkcontrolaccount As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvsubledger As common.UserControls.MyRadGridView
    Friend WithEvents dgvallocation As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtclosetoaccount As common.Controls.MyTextBox
    Friend WithEvents ddlclosetosegment As common.Controls.MyComboBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dgvsegment As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Basic As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Rollup As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BasicEx As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RollUpex As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblaccount As common.Controls.MyLabel
    Friend WithEvents lbldescription As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents fndstructurecode As common.UserControls.txtFinder
    Friend WithEvents fndsourcecode As common.UserControls.txtFinder
    Friend WithEvents ExportRollupSorting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ImportRollupSeq As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndaccount As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents cboTaxType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnPurhcase As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSale As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnNA As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtAccountSubGroup As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblSubGroup As common.Controls.MyTextBox
End Class

