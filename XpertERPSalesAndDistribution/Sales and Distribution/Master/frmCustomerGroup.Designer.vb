Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerGroup))
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtCustomerGroupDesc = New common.Controls.MyTextBox()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImport1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtTermsCode = New common.Controls.MyTextBox()
        Me.txtAccountSetDesc = New common.Controls.MyTextBox()
        Me.lblTaxGroup = New common.Controls.MyLabel()
        Me.txtTaxGroup = New common.Controls.MyTextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndSalespersoncode = New common.UserControls.txtFinder()
        Me.lblSalespersoncode = New common.Controls.MyLabel()
        Me.txtPercentage = New common.Controls.MyTextBox()
        Me.lblPercentage = New common.Controls.MyLabel()
        Me.txtSalespersonname = New common.Controls.MyTextBox()
        Me.txtVSPPriceCodeCash = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblVSPPriceCodeCash = New common.Controls.MyTextBox()
        Me.chkDefaultVSP = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkPONOMandatory = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkShowGrouponCVReport = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndCustomerGroupCode = New common.UserControls.txtNavigator()
        Me.fndTaxGroup = New common.UserControls.txtFinder()
        Me.fndTermsCode = New common.UserControls.txtFinder()
        Me.fndAccountSet = New common.UserControls.txtFinder()
        Me.LblShlfLife = New common.Controls.MyLabel()
        Me.TxtShfLife = New common.MyNumBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.rgbVSP = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblVSPPriceCodeCredit = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtVSPPriceCodeCredit = New common.UserControls.txtFinder()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New XpertERPSalesAndDistribution.ucCustomFields()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerGroupDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTermsCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccountSetDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblSalespersoncode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalespersonname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPPriceCodeCash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPONOMandatory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowGrouponCVReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblShlfLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtShfLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.rgbVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbVSP.SuspendLayout()
        CType(Me.lblVSPPriceCodeCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(3, 12)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(90, 16)
        Me.lblCustomerGroup.TabIndex = 16
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroupDesc
        '
        Me.txtCustomerGroupDesc.CalculationExpression = Nothing
        Me.txtCustomerGroupDesc.FieldCode = Nothing
        Me.txtCustomerGroupDesc.FieldDesc = Nothing
        Me.txtCustomerGroupDesc.FieldMaxLength = 0
        Me.txtCustomerGroupDesc.FieldName = Nothing
        Me.txtCustomerGroupDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroupDesc.isCalculatedField = False
        Me.txtCustomerGroupDesc.IsSourceFromTable = False
        Me.txtCustomerGroupDesc.IsSourceFromValueList = False
        Me.txtCustomerGroupDesc.IsUnique = False
        Me.txtCustomerGroupDesc.Location = New System.Drawing.Point(340, 11)
        Me.txtCustomerGroupDesc.MaxLength = 50
        Me.txtCustomerGroupDesc.MendatroryField = False
        Me.txtCustomerGroupDesc.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroupDesc.MyLinkLable2 = Nothing
        Me.txtCustomerGroupDesc.Name = "txtCustomerGroupDesc"
        Me.txtCustomerGroupDesc.ReferenceFieldDesc = Nothing
        Me.txtCustomerGroupDesc.ReferenceFieldName = Nothing
        Me.txtCustomerGroupDesc.ReferenceTableName = Nothing
        Me.txtCustomerGroupDesc.Size = New System.Drawing.Size(313, 18)
        Me.txtCustomerGroupDesc.TabIndex = 3
        Me.txtCustomerGroupDesc.Text = " "
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(629, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 24)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        '
        'MenuImport
        '
        Me.MenuImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.MenuImport1, Me.MenuExport, Me.RadMenuItem6})
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "File"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Print.."
        '
        'MenuImport1
        '
        Me.MenuImport1.Name = "MenuImport1"
        Me.MenuImport1.Text = "Import.."
        '
        'MenuExport
        '
        Me.MenuExport.AccessibleDescription = "Export"
        Me.MenuExport.AccessibleName = "Export"
        Me.MenuExport.Name = "MenuExport"
        Me.MenuExport.Text = "Export.."
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Close"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 36)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel2.TabIndex = 15
        Me.RadLabel2.Text = "Account Set"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(3, 60)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(68, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Terms Code"
        '
        'txtTermsCode
        '
        Me.txtTermsCode.CalculationExpression = Nothing
        Me.txtTermsCode.FieldCode = Nothing
        Me.txtTermsCode.FieldDesc = Nothing
        Me.txtTermsCode.FieldMaxLength = 0
        Me.txtTermsCode.FieldName = Nothing
        Me.txtTermsCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTermsCode.isCalculatedField = False
        Me.txtTermsCode.IsSourceFromTable = False
        Me.txtTermsCode.IsSourceFromValueList = False
        Me.txtTermsCode.IsUnique = False
        Me.txtTermsCode.Location = New System.Drawing.Point(340, 59)
        Me.txtTermsCode.MendatroryField = False
        Me.txtTermsCode.MyLinkLable1 = Me.RadLabel3
        Me.txtTermsCode.MyLinkLable2 = Nothing
        Me.txtTermsCode.Name = "txtTermsCode"
        Me.txtTermsCode.ReadOnly = True
        Me.txtTermsCode.ReferenceFieldDesc = Nothing
        Me.txtTermsCode.ReferenceFieldName = Nothing
        Me.txtTermsCode.ReferenceTableName = Nothing
        Me.txtTermsCode.Size = New System.Drawing.Size(313, 18)
        Me.txtTermsCode.TabIndex = 5
        Me.txtTermsCode.TabStop = False
        Me.txtTermsCode.Text = " "
        '
        'txtAccountSetDesc
        '
        Me.txtAccountSetDesc.CalculationExpression = Nothing
        Me.txtAccountSetDesc.FieldCode = Nothing
        Me.txtAccountSetDesc.FieldDesc = Nothing
        Me.txtAccountSetDesc.FieldMaxLength = 0
        Me.txtAccountSetDesc.FieldName = Nothing
        Me.txtAccountSetDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountSetDesc.isCalculatedField = False
        Me.txtAccountSetDesc.IsSourceFromTable = False
        Me.txtAccountSetDesc.IsSourceFromValueList = False
        Me.txtAccountSetDesc.IsUnique = False
        Me.txtAccountSetDesc.Location = New System.Drawing.Point(340, 35)
        Me.txtAccountSetDesc.MendatroryField = False
        Me.txtAccountSetDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtAccountSetDesc.MyLinkLable2 = Nothing
        Me.txtAccountSetDesc.Name = "txtAccountSetDesc"
        Me.txtAccountSetDesc.ReadOnly = True
        Me.txtAccountSetDesc.ReferenceFieldDesc = Nothing
        Me.txtAccountSetDesc.ReferenceFieldName = Nothing
        Me.txtAccountSetDesc.ReferenceTableName = Nothing
        Me.txtAccountSetDesc.Size = New System.Drawing.Size(313, 18)
        Me.txtAccountSetDesc.TabIndex = 3
        Me.txtAccountSetDesc.TabStop = False
        Me.txtAccountSetDesc.Text = " "
        '
        'lblTaxGroup
        '
        Me.lblTaxGroup.FieldName = Nothing
        Me.lblTaxGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGroup.Location = New System.Drawing.Point(3, 84)
        Me.lblTaxGroup.Name = "lblTaxGroup"
        Me.lblTaxGroup.Size = New System.Drawing.Size(60, 16)
        Me.lblTaxGroup.TabIndex = 13
        Me.lblTaxGroup.Text = "Tax Group"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.CalculationExpression = Nothing
        Me.txtTaxGroup.FieldCode = Nothing
        Me.txtTaxGroup.FieldDesc = Nothing
        Me.txtTaxGroup.FieldMaxLength = 0
        Me.txtTaxGroup.FieldName = Nothing
        Me.txtTaxGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.isCalculatedField = False
        Me.txtTaxGroup.IsSourceFromTable = False
        Me.txtTaxGroup.IsSourceFromValueList = False
        Me.txtTaxGroup.IsUnique = False
        Me.txtTaxGroup.Location = New System.Drawing.Point(340, 83)
        Me.txtTaxGroup.MendatroryField = False
        Me.txtTaxGroup.MyLinkLable1 = Me.lblTaxGroup
        Me.txtTaxGroup.MyLinkLable2 = Nothing
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReadOnly = True
        Me.txtTaxGroup.ReferenceFieldDesc = Nothing
        Me.txtTaxGroup.ReferenceFieldName = Nothing
        Me.txtTaxGroup.ReferenceTableName = Nothing
        Me.txtTaxGroup.Size = New System.Drawing.Size(313, 18)
        Me.txtTaxGroup.TabIndex = 7
        Me.txtTaxGroup.TabStop = False
        Me.txtTaxGroup.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(318, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 2
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New")
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(700, 20)
        Me.RadMenu1.TabIndex = 32
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(10, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 24)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndSalespersoncode)
        Me.RadGroupBox1.Controls.Add(Me.txtPercentage)
        Me.RadGroupBox1.Controls.Add(Me.lblPercentage)
        Me.RadGroupBox1.Controls.Add(Me.txtSalespersonname)
        Me.RadGroupBox1.Controls.Add(Me.lblSalespersoncode)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 191)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(663, 58)
        Me.RadGroupBox1.TabIndex = 6
        Me.RadGroupBox1.Visible = False
        '
        'fndSalespersoncode
        '
        Me.fndSalespersoncode.CalculationExpression = Nothing
        Me.fndSalespersoncode.FieldCode = Nothing
        Me.fndSalespersoncode.FieldDesc = Nothing
        Me.fndSalespersoncode.FieldMaxLength = 0
        Me.fndSalespersoncode.FieldName = Nothing
        Me.fndSalespersoncode.isCalculatedField = False
        Me.fndSalespersoncode.IsSourceFromTable = False
        Me.fndSalespersoncode.IsSourceFromValueList = False
        Me.fndSalespersoncode.IsUnique = False
        Me.fndSalespersoncode.Location = New System.Drawing.Point(120, 9)
        Me.fndSalespersoncode.MendatroryField = False
        Me.fndSalespersoncode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalespersoncode.MyLinkLable1 = Me.lblSalespersoncode
        Me.fndSalespersoncode.MyLinkLable2 = Nothing
        Me.fndSalespersoncode.MyReadOnly = False
        Me.fndSalespersoncode.MyShowMasterFormButton = False
        Me.fndSalespersoncode.Name = "fndSalespersoncode"
        Me.fndSalespersoncode.ReferenceFieldDesc = Nothing
        Me.fndSalespersoncode.ReferenceFieldName = Nothing
        Me.fndSalespersoncode.ReferenceTableName = Nothing
        Me.fndSalespersoncode.Size = New System.Drawing.Size(219, 21)
        Me.fndSalespersoncode.TabIndex = 8
        Me.fndSalespersoncode.Value = ""
        '
        'lblSalespersoncode
        '
        Me.lblSalespersoncode.FieldName = Nothing
        Me.lblSalespersoncode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalespersoncode.Location = New System.Drawing.Point(3, 11)
        Me.lblSalespersoncode.Name = "lblSalespersoncode"
        Me.lblSalespersoncode.Size = New System.Drawing.Size(99, 16)
        Me.lblSalespersoncode.TabIndex = 0
        Me.lblSalespersoncode.Text = "Salesperson Code"
        '
        'txtPercentage
        '
        Me.txtPercentage.CalculationExpression = Nothing
        Me.txtPercentage.FieldCode = Nothing
        Me.txtPercentage.FieldDesc = Nothing
        Me.txtPercentage.FieldMaxLength = 0
        Me.txtPercentage.FieldName = Nothing
        Me.txtPercentage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPercentage.isCalculatedField = False
        Me.txtPercentage.IsSourceFromTable = False
        Me.txtPercentage.IsSourceFromValueList = False
        Me.txtPercentage.IsUnique = False
        Me.txtPercentage.Location = New System.Drawing.Point(120, 35)
        Me.txtPercentage.MaxLength = 3
        Me.txtPercentage.MendatroryField = False
        Me.txtPercentage.MyLinkLable1 = Me.lblPercentage
        Me.txtPercentage.MyLinkLable2 = Nothing
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.ReferenceFieldDesc = Nothing
        Me.txtPercentage.ReferenceFieldName = Nothing
        Me.txtPercentage.ReferenceTableName = Nothing
        Me.txtPercentage.Size = New System.Drawing.Size(66, 18)
        Me.txtPercentage.TabIndex = 8
        '
        'lblPercentage
        '
        Me.lblPercentage.FieldName = Nothing
        Me.lblPercentage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercentage.Location = New System.Drawing.Point(3, 36)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(64, 16)
        Me.lblPercentage.TabIndex = 2
        Me.lblPercentage.Text = "Percentage"
        '
        'txtSalespersonname
        '
        Me.txtSalespersonname.CalculationExpression = Nothing
        Me.txtSalespersonname.FieldCode = Nothing
        Me.txtSalespersonname.FieldDesc = Nothing
        Me.txtSalespersonname.FieldMaxLength = 0
        Me.txtSalespersonname.FieldName = Nothing
        Me.txtSalespersonname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalespersonname.isCalculatedField = False
        Me.txtSalespersonname.IsSourceFromTable = False
        Me.txtSalespersonname.IsSourceFromValueList = False
        Me.txtSalespersonname.IsUnique = False
        Me.txtSalespersonname.Location = New System.Drawing.Point(341, 10)
        Me.txtSalespersonname.MaxLength = 50
        Me.txtSalespersonname.MendatroryField = False
        Me.txtSalespersonname.MyLinkLable1 = Nothing
        Me.txtSalespersonname.MyLinkLable2 = Nothing
        Me.txtSalespersonname.Name = "txtSalespersonname"
        Me.txtSalespersonname.ReadOnly = True
        Me.txtSalespersonname.ReferenceFieldDesc = Nothing
        Me.txtSalespersonname.ReferenceFieldName = Nothing
        Me.txtSalespersonname.ReferenceTableName = Nothing
        Me.txtSalespersonname.Size = New System.Drawing.Size(313, 18)
        Me.txtSalespersonname.TabIndex = 2
        Me.txtSalespersonname.TabStop = False
        Me.txtSalespersonname.Text = " "
        '
        'txtVSPPriceCodeCash
        '
        Me.txtVSPPriceCodeCash.CalculationExpression = Nothing
        Me.txtVSPPriceCodeCash.FieldCode = Nothing
        Me.txtVSPPriceCodeCash.FieldDesc = Nothing
        Me.txtVSPPriceCodeCash.FieldMaxLength = 0
        Me.txtVSPPriceCodeCash.FieldName = Nothing
        Me.txtVSPPriceCodeCash.isCalculatedField = False
        Me.txtVSPPriceCodeCash.IsSourceFromTable = False
        Me.txtVSPPriceCodeCash.IsSourceFromValueList = False
        Me.txtVSPPriceCodeCash.IsUnique = False
        Me.txtVSPPriceCodeCash.Location = New System.Drawing.Point(117, 6)
        Me.txtVSPPriceCodeCash.MendatroryField = False
        Me.txtVSPPriceCodeCash.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSPPriceCodeCash.MyLinkLable1 = Me.MyLabel1
        Me.txtVSPPriceCodeCash.MyLinkLable2 = Nothing
        Me.txtVSPPriceCodeCash.MyReadOnly = False
        Me.txtVSPPriceCodeCash.MyShowMasterFormButton = False
        Me.txtVSPPriceCodeCash.Name = "txtVSPPriceCodeCash"
        Me.txtVSPPriceCodeCash.ReferenceFieldDesc = Nothing
        Me.txtVSPPriceCodeCash.ReferenceFieldName = Nothing
        Me.txtVSPPriceCodeCash.ReferenceTableName = Nothing
        Me.txtVSPPriceCodeCash.Size = New System.Drawing.Size(219, 21)
        Me.txtVSPPriceCodeCash.TabIndex = 309
        Me.txtVSPPriceCodeCash.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(1, 8)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel1.TabIndex = 311
        Me.MyLabel1.Text = "Cash Price Code"
        '
        'lblVSPPriceCodeCash
        '
        Me.lblVSPPriceCodeCash.CalculationExpression = Nothing
        Me.lblVSPPriceCodeCash.FieldCode = Nothing
        Me.lblVSPPriceCodeCash.FieldDesc = Nothing
        Me.lblVSPPriceCodeCash.FieldMaxLength = 0
        Me.lblVSPPriceCodeCash.FieldName = Nothing
        Me.lblVSPPriceCodeCash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVSPPriceCodeCash.isCalculatedField = False
        Me.lblVSPPriceCodeCash.IsSourceFromTable = False
        Me.lblVSPPriceCodeCash.IsSourceFromValueList = False
        Me.lblVSPPriceCodeCash.IsUnique = False
        Me.lblVSPPriceCodeCash.Location = New System.Drawing.Point(338, 7)
        Me.lblVSPPriceCodeCash.MendatroryField = False
        Me.lblVSPPriceCodeCash.MyLinkLable1 = Me.MyLabel1
        Me.lblVSPPriceCodeCash.MyLinkLable2 = Nothing
        Me.lblVSPPriceCodeCash.Name = "lblVSPPriceCodeCash"
        Me.lblVSPPriceCodeCash.ReadOnly = True
        Me.lblVSPPriceCodeCash.ReferenceFieldDesc = Nothing
        Me.lblVSPPriceCodeCash.ReferenceFieldName = Nothing
        Me.lblVSPPriceCodeCash.ReferenceTableName = Nothing
        Me.lblVSPPriceCodeCash.Size = New System.Drawing.Size(313, 18)
        Me.lblVSPPriceCodeCash.TabIndex = 310
        Me.lblVSPPriceCodeCash.TabStop = False
        Me.lblVSPPriceCodeCash.Text = " "
        '
        'chkDefaultVSP
        '
        Me.chkDefaultVSP.Location = New System.Drawing.Point(574, 107)
        Me.chkDefaultVSP.Name = "chkDefaultVSP"
        Me.chkDefaultVSP.Size = New System.Drawing.Size(79, 18)
        Me.chkDefaultVSP.TabIndex = 308
        Me.chkDefaultVSP.Text = "Default VSP"
        '
        'chkPONOMandatory
        '
        Me.chkPONOMandatory.Location = New System.Drawing.Point(481, 107)
        Me.chkPONOMandatory.Name = "chkPONOMandatory"
        Me.chkPONOMandatory.Size = New System.Drawing.Size(93, 18)
        Me.chkPONOMandatory.TabIndex = 307
        Me.chkPONOMandatory.Text = "PO Mandatory"
        '
        'chkShowGrouponCVReport
        '
        Me.chkShowGrouponCVReport.Location = New System.Drawing.Point(340, 107)
        Me.chkShowGrouponCVReport.Name = "chkShowGrouponCVReport"
        Me.chkShowGrouponCVReport.Size = New System.Drawing.Size(135, 18)
        Me.chkShowGrouponCVReport.TabIndex = 306
        Me.chkShowGrouponCVReport.Text = "Show Group on Report"
        '
        'fndCustomerGroupCode
        '
        Me.fndCustomerGroupCode.FieldName = Nothing
        Me.fndCustomerGroupCode.Location = New System.Drawing.Point(119, 9)
        Me.fndCustomerGroupCode.MendatroryField = True
        Me.fndCustomerGroupCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomerGroupCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomerGroupCode.MyLinkLable1 = Me.lblCustomerGroup
        Me.fndCustomerGroupCode.MyLinkLable2 = Nothing
        Me.fndCustomerGroupCode.MyMaxLength = 32767
        Me.fndCustomerGroupCode.MyReadOnly = False
        Me.fndCustomerGroupCode.Name = "fndCustomerGroupCode"
        Me.fndCustomerGroupCode.Size = New System.Drawing.Size(199, 22)
        Me.fndCustomerGroupCode.TabIndex = 1
        Me.fndCustomerGroupCode.Value = ""
        '
        'fndTaxGroup
        '
        Me.fndTaxGroup.CalculationExpression = Nothing
        Me.fndTaxGroup.FieldCode = Nothing
        Me.fndTaxGroup.FieldDesc = Nothing
        Me.fndTaxGroup.FieldMaxLength = 0
        Me.fndTaxGroup.FieldName = Nothing
        Me.fndTaxGroup.isCalculatedField = False
        Me.fndTaxGroup.IsSourceFromTable = False
        Me.fndTaxGroup.IsSourceFromValueList = False
        Me.fndTaxGroup.IsUnique = False
        Me.fndTaxGroup.Location = New System.Drawing.Point(119, 82)
        Me.fndTaxGroup.MendatroryField = False
        Me.fndTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTaxGroup.MyLinkLable1 = Me.lblTaxGroup
        Me.fndTaxGroup.MyLinkLable2 = Nothing
        Me.fndTaxGroup.MyReadOnly = False
        Me.fndTaxGroup.MyShowMasterFormButton = False
        Me.fndTaxGroup.Name = "fndTaxGroup"
        Me.fndTaxGroup.ReferenceFieldDesc = Nothing
        Me.fndTaxGroup.ReferenceFieldName = Nothing
        Me.fndTaxGroup.ReferenceTableName = Nothing
        Me.fndTaxGroup.Size = New System.Drawing.Size(219, 21)
        Me.fndTaxGroup.TabIndex = 6
        Me.fndTaxGroup.Value = ""
        '
        'fndTermsCode
        '
        Me.fndTermsCode.CalculationExpression = Nothing
        Me.fndTermsCode.FieldCode = Nothing
        Me.fndTermsCode.FieldDesc = Nothing
        Me.fndTermsCode.FieldMaxLength = 0
        Me.fndTermsCode.FieldName = Nothing
        Me.fndTermsCode.isCalculatedField = False
        Me.fndTermsCode.IsSourceFromTable = False
        Me.fndTermsCode.IsSourceFromValueList = False
        Me.fndTermsCode.IsUnique = False
        Me.fndTermsCode.Location = New System.Drawing.Point(119, 58)
        Me.fndTermsCode.MendatroryField = False
        Me.fndTermsCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTermsCode.MyLinkLable1 = Me.RadLabel3
        Me.fndTermsCode.MyLinkLable2 = Nothing
        Me.fndTermsCode.MyReadOnly = False
        Me.fndTermsCode.MyShowMasterFormButton = False
        Me.fndTermsCode.Name = "fndTermsCode"
        Me.fndTermsCode.ReferenceFieldDesc = Nothing
        Me.fndTermsCode.ReferenceFieldName = Nothing
        Me.fndTermsCode.ReferenceTableName = Nothing
        Me.fndTermsCode.Size = New System.Drawing.Size(219, 21)
        Me.fndTermsCode.TabIndex = 5
        Me.fndTermsCode.Value = ""
        '
        'fndAccountSet
        '
        Me.fndAccountSet.CalculationExpression = Nothing
        Me.fndAccountSet.FieldCode = Nothing
        Me.fndAccountSet.FieldDesc = Nothing
        Me.fndAccountSet.FieldMaxLength = 0
        Me.fndAccountSet.FieldName = Nothing
        Me.fndAccountSet.isCalculatedField = False
        Me.fndAccountSet.IsSourceFromTable = False
        Me.fndAccountSet.IsSourceFromValueList = False
        Me.fndAccountSet.IsUnique = False
        Me.fndAccountSet.Location = New System.Drawing.Point(119, 34)
        Me.fndAccountSet.MendatroryField = False
        Me.fndAccountSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAccountSet.MyLinkLable1 = Me.RadLabel2
        Me.fndAccountSet.MyLinkLable2 = Nothing
        Me.fndAccountSet.MyReadOnly = False
        Me.fndAccountSet.MyShowMasterFormButton = False
        Me.fndAccountSet.Name = "fndAccountSet"
        Me.fndAccountSet.ReferenceFieldDesc = Nothing
        Me.fndAccountSet.ReferenceFieldName = Nothing
        Me.fndAccountSet.ReferenceTableName = Nothing
        Me.fndAccountSet.Size = New System.Drawing.Size(219, 21)
        Me.fndAccountSet.TabIndex = 4
        Me.fndAccountSet.Value = ""
        '
        'LblShlfLife
        '
        Me.LblShlfLife.FieldName = Nothing
        Me.LblShlfLife.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShlfLife.Location = New System.Drawing.Point(3, 108)
        Me.LblShlfLife.Name = "LblShlfLife"
        Me.LblShlfLife.Size = New System.Drawing.Size(53, 16)
        Me.LblShlfLife.TabIndex = 305
        Me.LblShlfLife.Text = "Shelf Life"
        Me.LblShlfLife.Visible = False
        '
        'TxtShfLife
        '
        Me.TxtShfLife.BackColor = System.Drawing.Color.White
        Me.TxtShfLife.CalculationExpression = Nothing
        Me.TxtShfLife.DecimalPlaces = 0
        Me.TxtShfLife.FieldCode = Nothing
        Me.TxtShfLife.FieldDesc = Nothing
        Me.TxtShfLife.FieldMaxLength = 0
        Me.TxtShfLife.FieldName = Nothing
        Me.TxtShfLife.isCalculatedField = False
        Me.TxtShfLife.IsSourceFromTable = False
        Me.TxtShfLife.IsSourceFromValueList = False
        Me.TxtShfLife.IsUnique = False
        Me.TxtShfLife.Location = New System.Drawing.Point(119, 106)
        Me.TxtShfLife.MendatroryField = False
        Me.TxtShfLife.MyLinkLable1 = Me.LblShlfLife
        Me.TxtShfLife.MyLinkLable2 = Nothing
        Me.TxtShfLife.Name = "TxtShfLife"
        Me.TxtShfLife.ReferenceFieldDesc = Nothing
        Me.TxtShfLife.ReferenceFieldName = Nothing
        Me.TxtShfLife.ReferenceTableName = Nothing
        Me.TxtShfLife.Size = New System.Drawing.Size(219, 20)
        Me.TxtShfLife.TabIndex = 7
        Me.TxtShfLife.Text = "0"
        Me.TxtShfLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtShfLife.Value = 0R
        Me.TxtShfLife.Visible = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(700, 367)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.rgbVSP)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblTaxGroup)
        Me.RadPageViewPage1.Controls.Add(Me.chkDefaultVSP)
        Me.RadPageViewPage1.Controls.Add(Me.txtAccountSetDesc)
        Me.RadPageViewPage1.Controls.Add(Me.chkPONOMandatory)
        Me.RadPageViewPage1.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage1.Controls.Add(Me.chkShowGrouponCVReport)
        Me.RadPageViewPage1.Controls.Add(Me.txtTermsCode)
        Me.RadPageViewPage1.Controls.Add(Me.fndCustomerGroupCode)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.fndTaxGroup)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.fndTermsCode)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.fndAccountSet)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerGroupDesc)
        Me.RadPageViewPage1.Controls.Add(Me.LblShlfLife)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.TxtShfLife)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(84.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(679, 319)
        Me.RadPageViewPage1.Text = "Group Details"
        '
        'rgbVSP
        '
        Me.rgbVSP.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbVSP.Controls.Add(Me.lblVSPPriceCodeCredit)
        Me.rgbVSP.Controls.Add(Me.lblVSPPriceCodeCash)
        Me.rgbVSP.Controls.Add(Me.txtVSPPriceCodeCash)
        Me.rgbVSP.Controls.Add(Me.txtVSPPriceCodeCredit)
        Me.rgbVSP.Controls.Add(Me.MyLabel1)
        Me.rgbVSP.Controls.Add(Me.MyLabel2)
        Me.rgbVSP.HeaderText = ""
        Me.rgbVSP.Location = New System.Drawing.Point(1, 130)
        Me.rgbVSP.Name = "rgbVSP"
        Me.rgbVSP.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rgbVSP.Size = New System.Drawing.Size(663, 58)
        Me.rgbVSP.TabIndex = 312
        Me.rgbVSP.Visible = False
        '
        'lblVSPPriceCodeCredit
        '
        Me.lblVSPPriceCodeCredit.CalculationExpression = Nothing
        Me.lblVSPPriceCodeCredit.FieldCode = Nothing
        Me.lblVSPPriceCodeCredit.FieldDesc = Nothing
        Me.lblVSPPriceCodeCredit.FieldMaxLength = 0
        Me.lblVSPPriceCodeCredit.FieldName = Nothing
        Me.lblVSPPriceCodeCredit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVSPPriceCodeCredit.isCalculatedField = False
        Me.lblVSPPriceCodeCredit.IsSourceFromTable = False
        Me.lblVSPPriceCodeCredit.IsSourceFromValueList = False
        Me.lblVSPPriceCodeCredit.IsUnique = False
        Me.lblVSPPriceCodeCredit.Location = New System.Drawing.Point(338, 32)
        Me.lblVSPPriceCodeCredit.MendatroryField = False
        Me.lblVSPPriceCodeCredit.MyLinkLable1 = Me.MyLabel2
        Me.lblVSPPriceCodeCredit.MyLinkLable2 = Nothing
        Me.lblVSPPriceCodeCredit.Name = "lblVSPPriceCodeCredit"
        Me.lblVSPPriceCodeCredit.ReadOnly = True
        Me.lblVSPPriceCodeCredit.ReferenceFieldDesc = Nothing
        Me.lblVSPPriceCodeCredit.ReferenceFieldName = Nothing
        Me.lblVSPPriceCodeCredit.ReferenceTableName = Nothing
        Me.lblVSPPriceCodeCredit.Size = New System.Drawing.Size(313, 18)
        Me.lblVSPPriceCodeCredit.TabIndex = 313
        Me.lblVSPPriceCodeCredit.TabStop = False
        Me.lblVSPPriceCodeCredit.Text = " "
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(1, 33)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel2.TabIndex = 314
        Me.MyLabel2.Text = "Credit Price Code"
        '
        'txtVSPPriceCodeCredit
        '
        Me.txtVSPPriceCodeCredit.CalculationExpression = Nothing
        Me.txtVSPPriceCodeCredit.FieldCode = Nothing
        Me.txtVSPPriceCodeCredit.FieldDesc = Nothing
        Me.txtVSPPriceCodeCredit.FieldMaxLength = 0
        Me.txtVSPPriceCodeCredit.FieldName = Nothing
        Me.txtVSPPriceCodeCredit.isCalculatedField = False
        Me.txtVSPPriceCodeCredit.IsSourceFromTable = False
        Me.txtVSPPriceCodeCredit.IsSourceFromValueList = False
        Me.txtVSPPriceCodeCredit.IsUnique = False
        Me.txtVSPPriceCodeCredit.Location = New System.Drawing.Point(117, 31)
        Me.txtVSPPriceCodeCredit.MendatroryField = False
        Me.txtVSPPriceCodeCredit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSPPriceCodeCredit.MyLinkLable1 = Me.MyLabel2
        Me.txtVSPPriceCodeCredit.MyLinkLable2 = Nothing
        Me.txtVSPPriceCodeCredit.MyReadOnly = False
        Me.txtVSPPriceCodeCredit.MyShowMasterFormButton = False
        Me.txtVSPPriceCodeCredit.Name = "txtVSPPriceCodeCredit"
        Me.txtVSPPriceCodeCredit.ReferenceFieldDesc = Nothing
        Me.txtVSPPriceCodeCredit.ReferenceFieldName = Nothing
        Me.txtVSPPriceCodeCredit.ReferenceTableName = Nothing
        Me.txtVSPPriceCodeCredit.Size = New System.Drawing.Size(219, 21)
        Me.txtVSPPriceCodeCredit.TabIndex = 312
        Me.txtVSPPriceCodeCredit.Value = ""
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 33)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(679, 235)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(679, 235)
        Me.UcCustomFields1.TabIndex = 2
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(700, 410)
        Me.SplitContainer1.SplitterDistance = 367
        Me.SplitContainer1.TabIndex = 33
        '
        'frmCustomerGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 430)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmCustomerGroup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = " Customer Groups"
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerGroupDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTermsCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccountSetDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblSalespersoncode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalespersonname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPPriceCodeCash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultVSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPONOMandatory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowGrouponCVReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblShlfLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtShfLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.rgbVSP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbVSP.ResumeLayout(False)
        Me.rgbVSP.PerformLayout()
        CType(Me.lblVSPPriceCodeCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCustomerGroupDesc As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    ' Friend WithEvents GridFinder1 As finder.gridFinder
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents txtTermsCode As common.Controls.MyTextBox
    Friend WithEvents txtAccountSetDesc As common.Controls.MyTextBox
    Friend WithEvents txtTaxGroup As common.Controls.MyTextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImport1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtSalespersonname As common.Controls.MyTextBox
    Friend WithEvents txtPercentage As common.Controls.MyTextBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtShfLife As common.MyNumBox
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblTaxGroup As common.Controls.MyLabel
    Friend WithEvents lblSalespersoncode As common.Controls.MyLabel
    Friend WithEvents lblPercentage As common.Controls.MyLabel
    Friend WithEvents LblShlfLife As common.Controls.MyLabel
    Friend WithEvents fndAccountSet As common.UserControls.txtFinder
    Friend WithEvents fndTermsCode As common.UserControls.txtFinder
    Friend WithEvents fndTaxGroup As common.UserControls.txtFinder
    Friend WithEvents fndSalespersoncode As common.UserControls.txtFinder
    Friend WithEvents fndCustomerGroupCode As common.UserControls.txtNavigator
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents chkShowGrouponCVReport As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkPONOMandatory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkDefaultVSP As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtVSPPriceCodeCash As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVSPPriceCodeCash As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents rgbVSP As RadGroupBox
    Friend WithEvents lblVSPPriceCodeCredit As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtVSPPriceCodeCredit As common.UserControls.txtFinder
End Class

