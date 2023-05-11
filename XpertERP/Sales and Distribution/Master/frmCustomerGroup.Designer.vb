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
        Me.GridFinder1 = New finder.gridFinder()
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
        Me.lblSalespersonname = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndCustomerGroupCode = New common.UserControls.txtNavigator()
        Me.fndTaxGroup = New common.UserControls.txtFinder()
        Me.fndTermsCode = New common.UserControls.txtFinder()
        Me.fndAccountSet = New common.UserControls.txtFinder()
        Me.LblShlfLife = New common.Controls.MyLabel()
        Me.TxtShfLife = New common.MyNumBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
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
        CType(Me.lblSalespersonname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.LblShlfLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtShfLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(13, 23)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(90, 16)
        Me.lblCustomerGroup.TabIndex = 16
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroupDesc
        '
        Me.txtCustomerGroupDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroupDesc.Location = New System.Drawing.Point(355, 22)
        Me.txtCustomerGroupDesc.MaxLength = 50
        Me.txtCustomerGroupDesc.MendatroryField = False
        Me.txtCustomerGroupDesc.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroupDesc.MyLinkLable2 = Nothing
        Me.txtCustomerGroupDesc.Name = "txtCustomerGroupDesc"
        Me.txtCustomerGroupDesc.Size = New System.Drawing.Size(281, 18)
        Me.txtCustomerGroupDesc.TabIndex = 3
        Me.txtCustomerGroupDesc.Text = " "
        '
        'GridFinder1
        '
        Me.GridFinder1.Caption = Nothing
        Me.GridFinder1.ConnectionString = Nothing
        Me.GridFinder1.Location = New System.Drawing.Point(176, 58)
        Me.GridFinder1.Margin = New System.Windows.Forms.Padding(0)
        Me.GridFinder1.Name = "GridFinder1"
        Me.GridFinder1.Query = Nothing
        Me.GridFinder1.ResultDT = Nothing
        Me.GridFinder1.SelectedRowDR = Nothing
        Me.GridFinder1.SelectedValue = Nothing
        Me.GridFinder1.SelectedValue1 = Nothing
        Me.GridFinder1.Size = New System.Drawing.Size(15, 15)
        Me.GridFinder1.TabIndex = 0
        Me.GridFinder1.ValueToSelect = Nothing
        Me.GridFinder1.ValueToSelect1 = Nothing
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(569, 272)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(83, 272)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        '
        'MenuImport
        '
        Me.MenuImport.AccessibleDescription = "File"
        Me.MenuImport.AccessibleName = "File"
        Me.MenuImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.MenuImport1, Me.MenuExport, Me.RadMenuItem6})
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "File"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Print.."
        Me.RadMenuItem3.AccessibleName = "Print.."
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Print.."
        '
        'MenuImport1
        '
        Me.MenuImport1.AccessibleDescription = "Import.."
        Me.MenuImport1.AccessibleName = "Import.."
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
        Me.RadMenuItem6.AccessibleDescription = "Close"
        Me.RadMenuItem6.AccessibleName = "Close"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Close"
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(11, 45)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(70, 16)
        Me.RadLabel2.TabIndex = 15
        Me.RadLabel2.Text = " Account Set"
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(13, 70)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(68, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Terms Code"
        '
        'txtTermsCode
        '
        Me.txtTermsCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTermsCode.Location = New System.Drawing.Point(356, 69)
        Me.txtTermsCode.MendatroryField = False
        Me.txtTermsCode.MyLinkLable1 = Me.RadLabel3
        Me.txtTermsCode.MyLinkLable2 = Nothing
        Me.txtTermsCode.Name = "txtTermsCode"
        Me.txtTermsCode.ReadOnly = True
        Me.txtTermsCode.Size = New System.Drawing.Size(281, 18)
        Me.txtTermsCode.TabIndex = 5
        Me.txtTermsCode.TabStop = False
        Me.txtTermsCode.Text = " "
        '
        'txtAccountSetDesc
        '
        Me.txtAccountSetDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountSetDesc.Location = New System.Drawing.Point(356, 44)
        Me.txtAccountSetDesc.MendatroryField = False
        Me.txtAccountSetDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtAccountSetDesc.MyLinkLable2 = Nothing
        Me.txtAccountSetDesc.Name = "txtAccountSetDesc"
        Me.txtAccountSetDesc.ReadOnly = True
        Me.txtAccountSetDesc.Size = New System.Drawing.Size(281, 18)
        Me.txtAccountSetDesc.TabIndex = 3
        Me.txtAccountSetDesc.TabStop = False
        Me.txtAccountSetDesc.Text = " "
        '
        'lblTaxGroup
        '
        Me.lblTaxGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGroup.Location = New System.Drawing.Point(13, 94)
        Me.lblTaxGroup.Name = "lblTaxGroup"
        Me.lblTaxGroup.Size = New System.Drawing.Size(60, 16)
        Me.lblTaxGroup.TabIndex = 13
        Me.lblTaxGroup.Text = "Tax Group"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.Location = New System.Drawing.Point(356, 93)
        Me.txtTaxGroup.MendatroryField = False
        Me.txtTaxGroup.MyLinkLable1 = Me.lblTaxGroup
        Me.txtTaxGroup.MyLinkLable2 = Nothing
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReadOnly = True
        Me.txtTaxGroup.Size = New System.Drawing.Size(281, 18)
        Me.txtTaxGroup.TabIndex = 7
        Me.txtTaxGroup.TabStop = False
        Me.txtTaxGroup.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(331, 22)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 18)
        Me.btnNew.TabIndex = 2
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New")
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(681, 20)
        Me.RadMenu1.TabIndex = 32
        Me.RadMenu1.Text = "RadMenu1"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 272)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
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
        Me.RadGroupBox1.Controls.Add(Me.lblSalespersonname)
        Me.RadGroupBox1.Controls.Add(Me.lblSalespersoncode)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 164)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(624, 98)
        Me.RadGroupBox1.TabIndex = 6
        Me.RadGroupBox1.Visible = False
        '
        'fndSalespersoncode
        '
        Me.fndSalespersoncode.Location = New System.Drawing.Point(119, 9)
        Me.fndSalespersoncode.MendatroryField = False
        Me.fndSalespersoncode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalespersoncode.MyLinkLable1 = Me.lblSalespersoncode
        Me.fndSalespersoncode.MyLinkLable2 = Nothing
        Me.fndSalespersoncode.MyReadOnly = False
        Me.fndSalespersoncode.MyShowMasterFormButton = False
        Me.fndSalespersoncode.Name = "fndSalespersoncode"
        Me.fndSalespersoncode.Size = New System.Drawing.Size(199, 21)
        Me.fndSalespersoncode.TabIndex = 8
        Me.fndSalespersoncode.Value = ""
        '
        'lblSalespersoncode
        '
        Me.lblSalespersoncode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalespersoncode.Location = New System.Drawing.Point(13, 11)
        Me.lblSalespersoncode.Name = "lblSalespersoncode"
        Me.lblSalespersoncode.Size = New System.Drawing.Size(99, 16)
        Me.lblSalespersoncode.TabIndex = 0
        Me.lblSalespersoncode.Text = "Salesperson Code"
        '
        'txtPercentage
        '
        Me.txtPercentage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPercentage.Location = New System.Drawing.Point(120, 57)
        Me.txtPercentage.MaxLength = 3
        Me.txtPercentage.MendatroryField = False
        Me.txtPercentage.MyLinkLable1 = Me.lblPercentage
        Me.txtPercentage.MyLinkLable2 = Nothing
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.Size = New System.Drawing.Size(66, 18)
        Me.txtPercentage.TabIndex = 8
        '
        'lblPercentage
        '
        Me.lblPercentage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercentage.Location = New System.Drawing.Point(13, 58)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(64, 16)
        Me.lblPercentage.TabIndex = 2
        Me.lblPercentage.Text = "Percentage"
        '
        'txtSalespersonname
        '
        Me.txtSalespersonname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalespersonname.Location = New System.Drawing.Point(119, 33)
        Me.txtSalespersonname.MaxLength = 50
        Me.txtSalespersonname.MendatroryField = False
        Me.txtSalespersonname.MyLinkLable1 = Me.lblSalespersonname
        Me.txtSalespersonname.MyLinkLable2 = Nothing
        Me.txtSalespersonname.Name = "txtSalespersonname"
        Me.txtSalespersonname.ReadOnly = True
        Me.txtSalespersonname.Size = New System.Drawing.Size(317, 18)
        Me.txtSalespersonname.TabIndex = 2
        Me.txtSalespersonname.TabStop = False
        Me.txtSalespersonname.Text = " "
        '
        'lblSalespersonname
        '
        Me.lblSalespersonname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalespersonname.Location = New System.Drawing.Point(13, 34)
        Me.lblSalespersonname.Name = "lblSalespersonname"
        Me.lblSalespersonname.Size = New System.Drawing.Size(103, 16)
        Me.lblSalespersonname.TabIndex = 3
        Me.lblSalespersonname.Text = "Salesperson Name"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndCustomerGroupCode)
        Me.RadGroupBox2.Controls.Add(Me.fndTaxGroup)
        Me.RadGroupBox2.Controls.Add(Me.fndTermsCode)
        Me.RadGroupBox2.Controls.Add(Me.fndAccountSet)
        Me.RadGroupBox2.Controls.Add(Me.LblShlfLife)
        Me.RadGroupBox2.Controls.Add(Me.TxtShfLife)
        Me.RadGroupBox2.Controls.Add(Me.lblCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.btnSave)
        Me.RadGroupBox2.Controls.Add(Me.txtCustomerGroupDesc)
        Me.RadGroupBox2.Controls.Add(Me.btnDelete)
        Me.RadGroupBox2.Controls.Add(Me.btnClose)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox2.Controls.Add(Me.btnNew)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox2.Controls.Add(Me.txtTermsCode)
        Me.RadGroupBox2.Controls.Add(Me.txtTaxGroup)
        Me.RadGroupBox2.Controls.Add(Me.txtAccountSetDesc)
        Me.RadGroupBox2.Controls.Add(Me.lblTaxGroup)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(1, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(657, 300)
        Me.RadGroupBox2.TabIndex = 0
        '
        'fndCustomerGroupCode
        '
        Me.fndCustomerGroupCode.Location = New System.Drawing.Point(129, 21)
        Me.fndCustomerGroupCode.MendatroryField = True
        Me.fndCustomerGroupCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomerGroupCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomerGroupCode.MyLinkLable1 = Me.lblCustomerGroup
        Me.fndCustomerGroupCode.MyLinkLable2 = Nothing
        Me.fndCustomerGroupCode.MyMaxLength = 32767
        Me.fndCustomerGroupCode.MyReadOnly = False
        Me.fndCustomerGroupCode.Name = "fndCustomerGroupCode"
        Me.fndCustomerGroupCode.Size = New System.Drawing.Size(199, 21)
        Me.fndCustomerGroupCode.TabIndex = 1
        Me.fndCustomerGroupCode.Value = ""
        '
        'fndTaxGroup
        '
        Me.fndTaxGroup.Location = New System.Drawing.Point(129, 92)
        Me.fndTaxGroup.MendatroryField = False
        Me.fndTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTaxGroup.MyLinkLable1 = Me.lblTaxGroup
        Me.fndTaxGroup.MyLinkLable2 = Nothing
        Me.fndTaxGroup.MyReadOnly = False
        Me.fndTaxGroup.MyShowMasterFormButton = False
        Me.fndTaxGroup.Name = "fndTaxGroup"
        Me.fndTaxGroup.Size = New System.Drawing.Size(199, 21)
        Me.fndTaxGroup.TabIndex = 6
        Me.fndTaxGroup.Value = ""
        '
        'fndTermsCode
        '
        Me.fndTermsCode.Location = New System.Drawing.Point(129, 68)
        Me.fndTermsCode.MendatroryField = False
        Me.fndTermsCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTermsCode.MyLinkLable1 = Me.RadLabel3
        Me.fndTermsCode.MyLinkLable2 = Nothing
        Me.fndTermsCode.MyReadOnly = False
        Me.fndTermsCode.MyShowMasterFormButton = False
        Me.fndTermsCode.Name = "fndTermsCode"
        Me.fndTermsCode.Size = New System.Drawing.Size(199, 21)
        Me.fndTermsCode.TabIndex = 5
        Me.fndTermsCode.Value = ""
        '
        'fndAccountSet
        '
        Me.fndAccountSet.Location = New System.Drawing.Point(129, 46)
        Me.fndAccountSet.MendatroryField = False
        Me.fndAccountSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAccountSet.MyLinkLable1 = Me.RadLabel2
        Me.fndAccountSet.MyLinkLable2 = Nothing
        Me.fndAccountSet.MyReadOnly = False
        Me.fndAccountSet.MyShowMasterFormButton = False
        Me.fndAccountSet.Name = "fndAccountSet"
        Me.fndAccountSet.Size = New System.Drawing.Size(199, 21)
        Me.fndAccountSet.TabIndex = 4
        Me.fndAccountSet.Value = ""
        '
        'LblShlfLife
        '
        Me.LblShlfLife.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShlfLife.Location = New System.Drawing.Point(15, 118)
        Me.LblShlfLife.Name = "LblShlfLife"
        Me.LblShlfLife.Size = New System.Drawing.Size(53, 16)
        Me.LblShlfLife.TabIndex = 305
        Me.LblShlfLife.Text = "Shelf Life"
        Me.LblShlfLife.Visible = False
        '
        'TxtShfLife
        '
        Me.TxtShfLife.BackColor = System.Drawing.Color.White
        Me.TxtShfLife.DecimalPlaces = 0
        Me.TxtShfLife.Location = New System.Drawing.Point(129, 116)
        Me.TxtShfLife.MendatroryField = False
        Me.TxtShfLife.MyLinkLable1 = Me.LblShlfLife
        Me.TxtShfLife.MyLinkLable2 = Nothing
        Me.TxtShfLife.Name = "TxtShfLife"
        Me.TxtShfLife.Size = New System.Drawing.Size(199, 20)
        Me.TxtShfLife.TabIndex = 7
        Me.TxtShfLife.Text = "0"
        Me.TxtShfLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtShfLife.Value = 0.0R
        Me.TxtShfLife.Visible = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(681, 342)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(84.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(660, 294)
        Me.RadPageViewPage1.Text = "Group Details"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(105.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(660, 294)
        Me.RadPageViewPage2.Text = "Additional Details"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(543, 288)
        Me.RadGroupBox4.TabIndex = 91
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
        Me.gvDB.Size = New System.Drawing.Size(523, 258)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(660, 294)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(660, 294)
        Me.UcCustomFields1.TabIndex = 2
        '
        'frmCustomerGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(681, 362)
        Me.Controls.Add(Me.RadPageView1)
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
        CType(Me.lblSalespersonname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.LblShlfLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtShfLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCustomerGroupDesc As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents GridFinder1 As finder.gridFinder
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
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtPercentage As common.Controls.MyTextBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents TxtShfLife As common.MyNumBox
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblTaxGroup As common.Controls.MyLabel
    Friend WithEvents lblSalespersoncode As common.Controls.MyLabel
    Friend WithEvents lblPercentage As common.Controls.MyLabel
    Friend WithEvents lblSalespersonname As common.Controls.MyLabel
    Friend WithEvents LblShlfLife As common.Controls.MyLabel
    Friend WithEvents fndAccountSet As common.UserControls.txtFinder
    Friend WithEvents fndTermsCode As common.UserControls.txtFinder
    Friend WithEvents fndTaxGroup As common.UserControls.txtFinder
    Friend WithEvents fndSalespersoncode As common.UserControls.txtFinder
    Friend WithEvents fndCustomerGroupCode As common.UserControls.txtNavigator
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
End Class

