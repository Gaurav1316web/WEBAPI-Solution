Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerCategory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerCategory))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.lblCustomerId = New common.Controls.MyLabel()
        Me.txtCustomerDesc = New common.Controls.MyTextBox()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuPrint = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.grpCustomerCategory = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndCustomerId = New common.UserControls.txtNavigator()
        Me.fndPriceCodeNon = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.fndPriceCode = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndRoute = New common.UserControls.txtFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.txtPriceCodeNon = New common.Controls.MyTextBox()
        Me.LblShlfLife = New common.Controls.MyLabel()
        Me.TxtShfLife = New common.MyNumBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.txtRouteDesc = New common.Controls.MyTextBox()
        Me.txtPriceCodeDesc = New common.Controls.MyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCustomerCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomerCategory.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCodeNon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblShlfLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtShfLife, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(574, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(91, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(67, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(17, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'lblCustomerId
        '
        Me.lblCustomerId.FieldName = Nothing
        Me.lblCustomerId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCustomerId.Location = New System.Drawing.Point(5, 18)
        Me.lblCustomerId.Name = "lblCustomerId"
        Me.lblCustomerId.Size = New System.Drawing.Size(114, 16)
        Me.lblCustomerId.TabIndex = 24
        Me.lblCustomerId.Text = " Customer Category"
        '
        'txtCustomerDesc
        '
        Me.txtCustomerDesc.AutoSize = False
        Me.txtCustomerDesc.CalculationExpression = Nothing
        Me.txtCustomerDesc.FieldCode = Nothing
        Me.txtCustomerDesc.FieldDesc = Nothing
        Me.txtCustomerDesc.FieldMaxLength = 0
        Me.txtCustomerDesc.FieldName = Nothing
        Me.txtCustomerDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerDesc.isCalculatedField = False
        Me.txtCustomerDesc.IsSourceFromTable = False
        Me.txtCustomerDesc.IsSourceFromValueList = False
        Me.txtCustomerDesc.IsUnique = False
        Me.txtCustomerDesc.Location = New System.Drawing.Point(373, 16)
        Me.txtCustomerDesc.MaxLength = 50
        Me.txtCustomerDesc.MendatroryField = False
        Me.txtCustomerDesc.Multiline = True
        Me.txtCustomerDesc.MyLinkLable1 = Me.lblCustomerId
        Me.txtCustomerDesc.MyLinkLable2 = Nothing
        Me.txtCustomerDesc.Name = "txtCustomerDesc"
        Me.txtCustomerDesc.ReferenceFieldDesc = Nothing
        Me.txtCustomerDesc.ReferenceFieldName = Nothing
        Me.txtCustomerDesc.ReferenceTableName = Nothing
        Me.txtCustomerDesc.Size = New System.Drawing.Size(245, 20)
        Me.txtCustomerDesc.TabIndex = 2
        Me.txtCustomerDesc.TabStop = False
        Me.txtCustomerDesc.Text = " "
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuPrint, Me.MenuImport, Me.MenuExport, Me.MenuClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'MenuPrint
        '
        Me.MenuPrint.Name = "MenuPrint"
        Me.MenuPrint.Text = "Print"
        '
        'MenuImport
        '
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "Import.."
        '
        'MenuExport
        '
        Me.MenuExport.AccessibleDescription = "RadMenuItem5"
        Me.MenuExport.AccessibleName = "RadMenuItem5"
        Me.MenuExport.Name = "MenuExport"
        Me.MenuExport.Text = "Export.."
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "RadMenuItem6"
        Me.MenuClose.AccessibleName = "RadMenuItem6"
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(348, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New")
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(655, 20)
        Me.RadMenu1.TabIndex = 31
        '
        'grpCustomerCategory
        '
        Me.grpCustomerCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomerCategory.Controls.Add(Me.fndCustomerId)
        Me.grpCustomerCategory.Controls.Add(Me.fndPriceCodeNon)
        Me.grpCustomerCategory.Controls.Add(Me.fndPriceCode)
        Me.grpCustomerCategory.Controls.Add(Me.fndRoute)
        Me.grpCustomerCategory.Controls.Add(Me.txtPriceCodeNon)
        Me.grpCustomerCategory.Controls.Add(Me.RadLabel2)
        Me.grpCustomerCategory.Controls.Add(Me.LblShlfLife)
        Me.grpCustomerCategory.Controls.Add(Me.TxtShfLife)
        Me.grpCustomerCategory.Controls.Add(Me.RadGroupBox4)
        Me.grpCustomerCategory.Controls.Add(Me.txtRouteDesc)
        Me.grpCustomerCategory.Controls.Add(Me.lblRoute)
        Me.grpCustomerCategory.Controls.Add(Me.txtPriceCodeDesc)
        Me.grpCustomerCategory.Controls.Add(Me.RadLabel1)
        Me.grpCustomerCategory.Controls.Add(Me.lblCustomerId)
        Me.grpCustomerCategory.Controls.Add(Me.btnNew)
        Me.grpCustomerCategory.Controls.Add(Me.txtCustomerDesc)
        Me.grpCustomerCategory.HeaderText = ""
        Me.grpCustomerCategory.Location = New System.Drawing.Point(17, 13)
        Me.grpCustomerCategory.Name = "grpCustomerCategory"
        Me.grpCustomerCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomerCategory.Size = New System.Drawing.Size(623, 395)
        Me.grpCustomerCategory.TabIndex = 1
        '
        'fndCustomerId
        '
        Me.fndCustomerId.FieldName = Nothing
        Me.fndCustomerId.Location = New System.Drawing.Point(155, 16)
        Me.fndCustomerId.MendatroryField = True
        Me.fndCustomerId.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomerId.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomerId.MyLinkLable1 = Me.lblCustomerId
        Me.fndCustomerId.MyLinkLable2 = Nothing
        Me.fndCustomerId.MyMaxLength = 32767
        Me.fndCustomerId.MyReadOnly = False
        Me.fndCustomerId.Name = "fndCustomerId"
        Me.fndCustomerId.Size = New System.Drawing.Size(192, 21)
        Me.fndCustomerId.TabIndex = 0
        Me.fndCustomerId.Value = ""
        '
        'fndPriceCodeNon
        '
        Me.fndPriceCodeNon.CalculationExpression = Nothing
        Me.fndPriceCodeNon.FieldCode = Nothing
        Me.fndPriceCodeNon.FieldDesc = Nothing
        Me.fndPriceCodeNon.FieldMaxLength = 0
        Me.fndPriceCodeNon.FieldName = Nothing
        Me.fndPriceCodeNon.isCalculatedField = False
        Me.fndPriceCodeNon.IsSourceFromTable = False
        Me.fndPriceCodeNon.IsSourceFromValueList = False
        Me.fndPriceCodeNon.IsUnique = False
        Me.fndPriceCodeNon.Location = New System.Drawing.Point(155, 91)
        Me.fndPriceCodeNon.MendatroryField = False
        Me.fndPriceCodeNon.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceCodeNon.MyLinkLable1 = Me.RadLabel2
        Me.fndPriceCodeNon.MyLinkLable2 = Nothing
        Me.fndPriceCodeNon.MyReadOnly = False
        Me.fndPriceCodeNon.MyShowMasterFormButton = False
        Me.fndPriceCodeNon.Name = "fndPriceCodeNon"
        Me.fndPriceCodeNon.ReferenceFieldDesc = Nothing
        Me.fndPriceCodeNon.ReferenceFieldName = Nothing
        Me.fndPriceCodeNon.ReferenceTableName = Nothing
        Me.fndPriceCodeNon.Size = New System.Drawing.Size(192, 21)
        Me.fndPriceCodeNon.TabIndex = 7
        Me.fndPriceCodeNon.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(8, 93)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(146, 16)
        Me.RadLabel2.TabIndex = 302
        Me.RadLabel2.Text = "Price Code (Non-Excisalbe)"
        '
        'fndPriceCode
        '
        Me.fndPriceCode.CalculationExpression = Nothing
        Me.fndPriceCode.FieldCode = Nothing
        Me.fndPriceCode.FieldDesc = Nothing
        Me.fndPriceCode.FieldMaxLength = 0
        Me.fndPriceCode.FieldName = Nothing
        Me.fndPriceCode.isCalculatedField = False
        Me.fndPriceCode.IsSourceFromTable = False
        Me.fndPriceCode.IsSourceFromValueList = False
        Me.fndPriceCode.IsUnique = False
        Me.fndPriceCode.Location = New System.Drawing.Point(155, 66)
        Me.fndPriceCode.MendatroryField = False
        Me.fndPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceCode.MyLinkLable1 = Me.RadLabel1
        Me.fndPriceCode.MyLinkLable2 = Nothing
        Me.fndPriceCode.MyReadOnly = False
        Me.fndPriceCode.MyShowMasterFormButton = False
        Me.fndPriceCode.Name = "fndPriceCode"
        Me.fndPriceCode.ReferenceFieldDesc = Nothing
        Me.fndPriceCode.ReferenceFieldName = Nothing
        Me.fndPriceCode.ReferenceTableName = Nothing
        Me.fndPriceCode.Size = New System.Drawing.Size(192, 21)
        Me.fndPriceCode.TabIndex = 5
        Me.fndPriceCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(9, 68)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(121, 16)
        Me.RadLabel1.TabIndex = 32
        Me.RadLabel1.Text = "Price Code (Excisable)"
        '
        'fndRoute
        '
        Me.fndRoute.CalculationExpression = Nothing
        Me.fndRoute.FieldCode = Nothing
        Me.fndRoute.FieldDesc = Nothing
        Me.fndRoute.FieldMaxLength = 0
        Me.fndRoute.FieldName = Nothing
        Me.fndRoute.isCalculatedField = False
        Me.fndRoute.IsSourceFromTable = False
        Me.fndRoute.IsSourceFromValueList = False
        Me.fndRoute.IsUnique = False
        Me.fndRoute.Location = New System.Drawing.Point(155, 42)
        Me.fndRoute.MendatroryField = False
        Me.fndRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRoute.MyLinkLable1 = Me.lblRoute
        Me.fndRoute.MyLinkLable2 = Nothing
        Me.fndRoute.MyReadOnly = False
        Me.fndRoute.MyShowMasterFormButton = False
        Me.fndRoute.Name = "fndRoute"
        Me.fndRoute.ReferenceFieldDesc = Nothing
        Me.fndRoute.ReferenceFieldName = Nothing
        Me.fndRoute.ReferenceTableName = Nothing
        Me.fndRoute.Size = New System.Drawing.Size(192, 21)
        Me.fndRoute.TabIndex = 3
        Me.fndRoute.Value = ""
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(11, 44)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(49, 16)
        Me.lblRoute.TabIndex = 35
        Me.lblRoute.Text = "Route Id"
        '
        'txtPriceCodeNon
        '
        Me.txtPriceCodeNon.AutoSize = False
        Me.txtPriceCodeNon.CalculationExpression = Nothing
        Me.txtPriceCodeNon.FieldCode = Nothing
        Me.txtPriceCodeNon.FieldDesc = Nothing
        Me.txtPriceCodeNon.FieldMaxLength = 0
        Me.txtPriceCodeNon.FieldName = Nothing
        Me.txtPriceCodeNon.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceCodeNon.isCalculatedField = False
        Me.txtPriceCodeNon.IsSourceFromTable = False
        Me.txtPriceCodeNon.IsSourceFromValueList = False
        Me.txtPriceCodeNon.IsUnique = False
        Me.txtPriceCodeNon.Location = New System.Drawing.Point(373, 91)
        Me.txtPriceCodeNon.MaxLength = 50
        Me.txtPriceCodeNon.MendatroryField = False
        Me.txtPriceCodeNon.Multiline = True
        Me.txtPriceCodeNon.MyLinkLable1 = Me.RadLabel2
        Me.txtPriceCodeNon.MyLinkLable2 = Nothing
        Me.txtPriceCodeNon.Name = "txtPriceCodeNon"
        Me.txtPriceCodeNon.ReadOnly = True
        Me.txtPriceCodeNon.ReferenceFieldDesc = Nothing
        Me.txtPriceCodeNon.ReferenceFieldName = Nothing
        Me.txtPriceCodeNon.ReferenceTableName = Nothing
        Me.txtPriceCodeNon.Size = New System.Drawing.Size(245, 20)
        Me.txtPriceCodeNon.TabIndex = 8
        Me.txtPriceCodeNon.TabStop = False
        Me.txtPriceCodeNon.Text = " "
        '
        'LblShlfLife
        '
        Me.LblShlfLife.FieldName = Nothing
        Me.LblShlfLife.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShlfLife.Location = New System.Drawing.Point(7, 116)
        Me.LblShlfLife.Name = "LblShlfLife"
        Me.LblShlfLife.Size = New System.Drawing.Size(53, 16)
        Me.LblShlfLife.TabIndex = 303
        Me.LblShlfLife.Text = "Shelf Life"
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
        Me.TxtShfLife.Location = New System.Drawing.Point(155, 114)
        Me.TxtShfLife.MendatroryField = False
        Me.TxtShfLife.MyLinkLable1 = Me.LblShlfLife
        Me.TxtShfLife.MyLinkLable2 = Nothing
        Me.TxtShfLife.Name = "TxtShfLife"
        Me.TxtShfLife.ReferenceFieldDesc = Nothing
        Me.TxtShfLife.ReferenceFieldName = Nothing
        Me.TxtShfLife.ReferenceTableName = Nothing
        Me.TxtShfLife.Size = New System.Drawing.Size(192, 20)
        Me.TxtShfLife.TabIndex = 9
        Me.TxtShfLife.Text = "0"
        Me.TxtShfLife.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtShfLife.Value = 0R
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(11, 148)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(599, 232)
        Me.RadGroupBox4.TabIndex = 5
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDB.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDB.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvDB.MyStopExport = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.ShowHeaderCellButtons = True
        Me.gvDB.Size = New System.Drawing.Size(579, 202)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        Me.gvDB.VarID = ""
        '
        'txtRouteDesc
        '
        Me.txtRouteDesc.AutoSize = False
        Me.txtRouteDesc.CalculationExpression = Nothing
        Me.txtRouteDesc.FieldCode = Nothing
        Me.txtRouteDesc.FieldDesc = Nothing
        Me.txtRouteDesc.FieldMaxLength = 0
        Me.txtRouteDesc.FieldName = Nothing
        Me.txtRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteDesc.isCalculatedField = False
        Me.txtRouteDesc.IsSourceFromTable = False
        Me.txtRouteDesc.IsSourceFromValueList = False
        Me.txtRouteDesc.IsUnique = False
        Me.txtRouteDesc.Location = New System.Drawing.Point(373, 42)
        Me.txtRouteDesc.MaxLength = 50
        Me.txtRouteDesc.MendatroryField = False
        Me.txtRouteDesc.Multiline = True
        Me.txtRouteDesc.MyLinkLable1 = Me.lblRoute
        Me.txtRouteDesc.MyLinkLable2 = Nothing
        Me.txtRouteDesc.Name = "txtRouteDesc"
        Me.txtRouteDesc.ReadOnly = True
        Me.txtRouteDesc.ReferenceFieldDesc = Nothing
        Me.txtRouteDesc.ReferenceFieldName = Nothing
        Me.txtRouteDesc.ReferenceTableName = Nothing
        Me.txtRouteDesc.Size = New System.Drawing.Size(245, 20)
        Me.txtRouteDesc.TabIndex = 4
        Me.txtRouteDesc.TabStop = False
        Me.txtRouteDesc.Text = " "
        '
        'txtPriceCodeDesc
        '
        Me.txtPriceCodeDesc.AutoSize = False
        Me.txtPriceCodeDesc.CalculationExpression = Nothing
        Me.txtPriceCodeDesc.FieldCode = Nothing
        Me.txtPriceCodeDesc.FieldDesc = Nothing
        Me.txtPriceCodeDesc.FieldMaxLength = 0
        Me.txtPriceCodeDesc.FieldName = Nothing
        Me.txtPriceCodeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceCodeDesc.isCalculatedField = False
        Me.txtPriceCodeDesc.IsSourceFromTable = False
        Me.txtPriceCodeDesc.IsSourceFromValueList = False
        Me.txtPriceCodeDesc.IsUnique = False
        Me.txtPriceCodeDesc.Location = New System.Drawing.Point(373, 66)
        Me.txtPriceCodeDesc.MaxLength = 50
        Me.txtPriceCodeDesc.MendatroryField = False
        Me.txtPriceCodeDesc.Multiline = True
        Me.txtPriceCodeDesc.MyLinkLable1 = Me.RadLabel1
        Me.txtPriceCodeDesc.MyLinkLable2 = Nothing
        Me.txtPriceCodeDesc.Name = "txtPriceCodeDesc"
        Me.txtPriceCodeDesc.ReadOnly = True
        Me.txtPriceCodeDesc.ReferenceFieldDesc = Nothing
        Me.txtPriceCodeDesc.ReferenceFieldName = Nothing
        Me.txtPriceCodeDesc.ReferenceTableName = Nothing
        Me.txtPriceCodeDesc.Size = New System.Drawing.Size(245, 20)
        Me.txtPriceCodeDesc.TabIndex = 6
        Me.txtPriceCodeDesc.TabStop = False
        Me.txtPriceCodeDesc.Text = " "
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpCustomerCategory)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(655, 452)
        Me.SplitContainer1.SplitterDistance = 411
        Me.SplitContainer1.TabIndex = 32
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(293, 9)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(68, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "History"
        '
        'frmCustomerCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 472)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmCustomerCategory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Category"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCustomerCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomerCategory.ResumeLayout(False)
        Me.grpCustomerCategory.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCodeNon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblShlfLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtShfLife, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustomerDesc As common.Controls.MyTextBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuPrint As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents grpCustomerCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtPriceCodeDesc As common.Controls.MyTextBox
    Friend WithEvents txtRouteDesc As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents TxtShfLife As common.MyNumBox
    Friend WithEvents txtPriceCodeNon As common.Controls.MyTextBox
    Friend WithEvents lblCustomerId As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents LblShlfLife As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents fndRoute As common.UserControls.txtFinder
    Friend WithEvents fndPriceCode As common.UserControls.txtFinder
    Friend WithEvents fndPriceCodeNon As common.UserControls.txtFinder
    Friend WithEvents fndCustomerId As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnHistory As RadButton
End Class

