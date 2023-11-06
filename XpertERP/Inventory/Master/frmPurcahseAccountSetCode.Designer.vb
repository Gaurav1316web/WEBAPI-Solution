<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurcahseAccountSetCode
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
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.rdtxtdescription = New common.Controls.MyTextBox()
        Me.rdlbldescription = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rdgpbxpurchaseaccountset = New Telerik.WinControls.UI.RadGroupBox()
        Me.chk_indentrequired = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblaccountsetdesc = New common.Controls.MyTextBox()
        Me.cboCostingMethod = New common.Controls.MyComboBox()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.fndaccountsetcode = New common.UserControls.txtNavigator()
        Me.rdlblaccountsetcode = New common.Controls.MyLabel()
        Me.rdgrpbxgeneralledgeraccounts = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblItemOpeningClearing = New Telerik.WinControls.UI.RadTextBox()
        Me.fndItemOpeningClearing = New common.UserControls.txtFinder()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.fndWrekageAccount = New common.UserControls.txtFinder()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtWrekageAccount = New Telerik.WinControls.UI.RadTextBox()
        Me.fndPurchaseLoss = New common.UserControls.txtFinder()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtPurchaseLoss = New Telerik.WinControls.UI.RadTextBox()
        Me.fndFAAccount = New common.UserControls.txtFinder()
        Me.lblFaAccountDes = New Telerik.WinControls.UI.RadTextBox()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.txtStoreConsumtion = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.fndStoreConsumptionAcc = New common.UserControls.txtFinder()
        Me.txtStockTransferJobWork = New common.UserControls.txtFinder()
        Me.lblStockTransferJobWorkDesc = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.fndEMP = New common.UserControls.txtFinder()
        Me.fndHandlingCharge = New common.UserControls.txtFinder()
        Me.lblEMP = New common.Controls.MyLabel()
        Me.txtEMP = New Telerik.WinControls.UI.RadTextBox()
        Me.txtHandlingCharge = New Telerik.WinControls.UI.RadTextBox()
        Me.lblHandlingCharge = New common.Controls.MyLabel()
        Me.txtDifferenceAccount = New common.UserControls.txtFinder()
        Me.lblDifferenceAccount = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtPurchaseJobWork = New common.UserControls.txtFinder()
        Me.lblPurchaseJobwork = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtFreightCharges = New common.UserControls.txtFinder()
        Me.txtChiilingCharges = New common.UserControls.txtFinder()
        Me.lblFreightCharges = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblChiilingCharges = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtProvisionClearing = New common.UserControls.txtFinder()
        Me.lblProvisioinClearing = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.FndStockTransferAccount = New common.UserControls.txtFinder()
        Me.txtStockTransferIn = New common.UserControls.txtFinder()
        Me.lblStockTransferIn = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtStockTransferAccount = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.FndJobWork = New common.UserControls.txtFinder()
        Me.LblJobwork = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.fndTransferGainLoss = New common.UserControls.txtFinder()
        Me.txtPurchaseCTRL_Ac = New common.UserControls.txtFinder()
        Me.txtTransferGainLossDesc = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtPurchaseCtrlAcDesc = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtLossAc = New Telerik.WinControls.UI.RadTextBox()
        Me.fndLossAc = New common.UserControls.txtFinder()
        Me.lblLossAc = New common.Controls.MyLabel()
        Me.fndOther2 = New common.UserControls.txtFinder()
        Me.fndOther1 = New common.UserControls.txtFinder()
        Me.fndRMCons = New common.UserControls.txtFinder()
        Me.fndWIPAcc = New common.UserControls.txtFinder()
        Me.rdtxtOther2 = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.rdtxtOther1 = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.rdtxtRMCons = New common.Controls.MyTextBox()
        Me.rdtxtWIPAcc = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndbreakageglaccount = New common.UserControls.txtFinder()
        Me.fndReserveStock = New common.UserControls.txtFinder()
        Me.fndcreditdebitnoteclr = New common.UserControls.txtFinder()
        Me.fndphysicalinventrycontrol = New common.UserControls.txtFinder()
        Me.fnddisassamblyexpense = New common.UserControls.txtFinder()
        Me.fndshipmentclearing = New common.UserControls.txtFinder()
        Me.fndtransferclearing = New common.UserControls.txtFinder()
        Me.fndnonstockclearing = New common.UserControls.txtFinder()
        Me.fndassamblycostoff = New common.UserControls.txtFinder()
        Me.fndadjustmentwriteoff = New common.UserControls.txtFinder()
        Me.fndpayableclearing = New common.UserControls.txtFinder()
        Me.fndInventoryControl = New common.UserControls.txtFinder()
        Me.txtbreakage = New Telerik.WinControls.UI.RadTextBox()
        Me.lblbreakage = New common.Controls.MyLabel()
        Me.txtReserveStock = New common.Controls.MyTextBox()
        Me.lblReserveStock = New common.Controls.MyLabel()
        Me.rdtxtcreditdebitnoteclr = New common.Controls.MyTextBox()
        Me.rdtxtphysicalinventryadj = New common.Controls.MyTextBox()
        Me.rdtxtdisassamblyexpense = New common.Controls.MyTextBox()
        Me.rdtxtshipmentexpense = New common.Controls.MyTextBox()
        Me.rdtxttransferclearing = New common.Controls.MyTextBox()
        Me.rdtxtnonstockclearing = New common.Controls.MyTextBox()
        Me.rdtxtassamblycostcredit = New common.Controls.MyTextBox()
        Me.rdtxtadjustmentwriteoff = New common.Controls.MyTextBox()
        Me.rdtxtpayableclearing = New common.Controls.MyTextBox()
        Me.rdtxtinventrycontrol = New common.Controls.MyTextBox()
        Me.rdlblcreditdebitnoteclr = New common.Controls.MyLabel()
        Me.rdlblphysicalinventryadj = New common.Controls.MyLabel()
        Me.rdlbldisassemblyexpenxe = New common.Controls.MyLabel()
        Me.rdlblshipmentclearing = New common.Controls.MyLabel()
        Me.rdlbltransferclearing = New common.Controls.MyLabel()
        Me.rdlblnonstockclearing = New common.Controls.MyLabel()
        Me.rdlblassamblycostcredit = New common.Controls.MyLabel()
        Me.rdlbladjustmentwriteoff = New common.Controls.MyLabel()
        Me.rdlblpayableclearing = New common.Controls.MyLabel()
        Me.rdlblinventorycontrol = New common.Controls.MyLabel()
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.radmenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.Bsws1 = New ERP.BSWS.BSWS()
        CType(Me.rdtxtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rdgpbxpurchaseaccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxpurchaseaccountset.SuspendLayout()
        CType(Me.chk_indentrequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblaccountsetdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCostingMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblaccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxgeneralledgeraccounts.SuspendLayout()
        CType(Me.lblItemOpeningClearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWrekageAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchaseLoss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFaAccountDes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStoreConsumtion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStockTransferJobWorkDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEMP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEMP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHandlingCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHandlingCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDifferenceAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPurchaseJobwork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFreightCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChiilingCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProvisioinClearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStockTransferIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtStockTransferAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblJobwork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransferGainLossDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchaseCtrlAcDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLossAc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLossAc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtOther2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtOther1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtRMCons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtWIPAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbreakage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbreakage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReserveStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReserveStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtcreditdebitnoteclr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtphysicalinventryadj, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtdisassamblyexpense, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtshipmentexpense, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxttransferclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtnonstockclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtassamblycostcredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtadjustmentwriteoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtpayableclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtinventrycontrol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblcreditdebitnoteclr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblphysicalinventryadj, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldisassemblyexpenxe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblshipmentclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltransferclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblnonstockclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblassamblycostcredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbladjustmentwriteoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblpayableclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblinventorycontrol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdtxtdescription
        '
        Me.rdtxtdescription.CalculationExpression = Nothing
        Me.rdtxtdescription.FieldCode = Nothing
        Me.rdtxtdescription.FieldDesc = Nothing
        Me.rdtxtdescription.FieldMaxLength = 0
        Me.rdtxtdescription.FieldName = Nothing
        Me.rdtxtdescription.isCalculatedField = False
        Me.rdtxtdescription.IsSourceFromTable = False
        Me.rdtxtdescription.IsSourceFromValueList = False
        Me.rdtxtdescription.IsUnique = False
        Me.rdtxtdescription.Location = New System.Drawing.Point(118, 39)
        Me.rdtxtdescription.MaxLength = 50
        Me.rdtxtdescription.MendatroryField = False
        Me.rdtxtdescription.MyLinkLable1 = Me.rdlbldescription
        Me.rdtxtdescription.MyLinkLable2 = Nothing
        Me.rdtxtdescription.Name = "rdtxtdescription"
        Me.rdtxtdescription.ReferenceFieldDesc = Nothing
        Me.rdtxtdescription.ReferenceFieldName = Nothing
        Me.rdtxtdescription.ReferenceTableName = Nothing
        Me.rdtxtdescription.Size = New System.Drawing.Size(537, 20)
        Me.rdtxtdescription.TabIndex = 2
        '
        'rdlbldescription
        '
        Me.rdlbldescription.FieldName = Nothing
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(365, 18)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 6
        Me.rdlbldescription.Text = "Description"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxpurchaseaccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(960, 478)
        Me.SplitContainer1.SplitterDistance = 440
        Me.SplitContainer1.TabIndex = 0
        '
        'rdgpbxpurchaseaccountset
        '
        Me.rdgpbxpurchaseaccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.chk_indentrequired)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.lblaccountsetdesc)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.cboCostingMethod)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.RadLabel10)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.fndaccountsetcode)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.rdgrpbxgeneralledgeraccounts)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.rdlbldescription)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxpurchaseaccountset.Controls.Add(Me.rdlblaccountsetcode)
        Me.rdgpbxpurchaseaccountset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdgpbxpurchaseaccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxpurchaseaccountset.HeaderText = "Purchase Account Set"
        Me.rdgpbxpurchaseaccountset.Location = New System.Drawing.Point(0, 0)
        Me.rdgpbxpurchaseaccountset.Name = "rdgpbxpurchaseaccountset"
        Me.rdgpbxpurchaseaccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxpurchaseaccountset.Size = New System.Drawing.Size(960, 440)
        Me.rdgpbxpurchaseaccountset.TabIndex = 0
        Me.rdgpbxpurchaseaccountset.Text = "Purchase Account Set"
        '
        'chk_indentrequired
        '
        Me.chk_indentrequired.Location = New System.Drawing.Point(862, 19)
        Me.chk_indentrequired.Name = "chk_indentrequired"
        Me.chk_indentrequired.Size = New System.Drawing.Size(101, 18)
        Me.chk_indentrequired.TabIndex = 69
        Me.chk_indentrequired.Text = "Indent Required"
        '
        'lblaccountsetdesc
        '
        Me.lblaccountsetdesc.CalculationExpression = Nothing
        Me.lblaccountsetdesc.FieldCode = Nothing
        Me.lblaccountsetdesc.FieldDesc = Nothing
        Me.lblaccountsetdesc.FieldMaxLength = 0
        Me.lblaccountsetdesc.FieldName = Nothing
        Me.lblaccountsetdesc.isCalculatedField = False
        Me.lblaccountsetdesc.IsSourceFromTable = False
        Me.lblaccountsetdesc.IsSourceFromValueList = False
        Me.lblaccountsetdesc.IsUnique = False
        Me.lblaccountsetdesc.Location = New System.Drawing.Point(434, 17)
        Me.lblaccountsetdesc.MendatroryField = False
        Me.lblaccountsetdesc.MyLinkLable1 = Nothing
        Me.lblaccountsetdesc.MyLinkLable2 = Nothing
        Me.lblaccountsetdesc.Name = "lblaccountsetdesc"
        Me.lblaccountsetdesc.ReferenceFieldDesc = Nothing
        Me.lblaccountsetdesc.ReferenceFieldName = Nothing
        Me.lblaccountsetdesc.ReferenceTableName = Nothing
        Me.lblaccountsetdesc.Size = New System.Drawing.Size(222, 20)
        Me.lblaccountsetdesc.TabIndex = 2
        '
        'cboCostingMethod
        '
        Me.cboCostingMethod.AutoCompleteDisplayMember = Nothing
        Me.cboCostingMethod.AutoCompleteValueMember = Nothing
        Me.cboCostingMethod.CalculationExpression = Nothing
        Me.cboCostingMethod.DropDownAnimationEnabled = True
        Me.cboCostingMethod.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCostingMethod.FieldCode = Nothing
        Me.cboCostingMethod.FieldDesc = Nothing
        Me.cboCostingMethod.FieldMaxLength = 0
        Me.cboCostingMethod.FieldName = Nothing
        Me.cboCostingMethod.isCalculatedField = False
        Me.cboCostingMethod.IsSourceFromTable = False
        Me.cboCostingMethod.IsSourceFromValueList = False
        Me.cboCostingMethod.IsUnique = False
        Me.cboCostingMethod.Location = New System.Drawing.Point(751, 17)
        Me.cboCostingMethod.MendatroryField = False
        Me.cboCostingMethod.MyLinkLable1 = Me.RadLabel10
        Me.cboCostingMethod.MyLinkLable2 = Nothing
        Me.cboCostingMethod.Name = "cboCostingMethod"
        Me.cboCostingMethod.ReferenceFieldDesc = Nothing
        Me.cboCostingMethod.ReferenceFieldName = Nothing
        Me.cboCostingMethod.ReferenceTableName = Nothing
        '
        '
        '
        Me.cboCostingMethod.RootElement.StretchVertically = True
        Me.cboCostingMethod.Size = New System.Drawing.Size(105, 20)
        Me.cboCostingMethod.TabIndex = 3
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(659, 19)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(86, 16)
        Me.RadLabel10.TabIndex = 5
        Me.RadLabel10.Text = "Costing Method"
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.FieldName = Nothing
        Me.fndaccountsetcode.Location = New System.Drawing.Point(118, 17)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Me.rdlblaccountsetcode
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 32767
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(225, 20)
        Me.fndaccountsetcode.TabIndex = 0
        Me.fndaccountsetcode.Value = ""
        '
        'rdlblaccountsetcode
        '
        Me.rdlblaccountsetcode.FieldName = Nothing
        Me.rdlblaccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rdlblaccountsetcode.Location = New System.Drawing.Point(8, 19)
        Me.rdlblaccountsetcode.Name = "rdlblaccountsetcode"
        Me.rdlblaccountsetcode.Size = New System.Drawing.Size(104, 16)
        Me.rdlblaccountsetcode.TabIndex = 7
        Me.rdlblaccountsetcode.Text = "Account Set Code"
        '
        'rdgrpbxgeneralledgeraccounts
        '
        Me.rdgrpbxgeneralledgeraccounts.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxgeneralledgeraccounts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblItemOpeningClearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndItemOpeningClearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel20)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndWrekageAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel18)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtWrekageAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndPurchaseLoss)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel17)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtPurchaseLoss)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndFAAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblFaAccountDes)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.UcCustomFields1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.RadLabel1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtStoreConsumtion)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel16)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndStoreConsumptionAcc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtStockTransferJobWork)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblStockTransferJobWorkDesc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel15)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndEMP)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndHandlingCharge)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblEMP)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtEMP)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtHandlingCharge)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblHandlingCharge)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtDifferenceAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblDifferenceAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel14)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtPurchaseJobWork)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblPurchaseJobwork)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel13)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtFreightCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtChiilingCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblFreightCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel12)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblChiilingCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel11)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtProvisionClearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblProvisioinClearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel10)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.FndStockTransferAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtStockTransferIn)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblStockTransferIn)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.TxtStockTransferAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel9)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel8)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.FndJobWork)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.LblJobwork)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel7)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndTransferGainLoss)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtPurchaseCTRL_Ac)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtTransferGainLossDesc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel6)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtPurchaseCtrlAcDesc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel5)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtLossAc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndLossAc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblLossAc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndOther2)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndOther1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndRMCons)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndWIPAcc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtOther2)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtOther1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel2)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtRMCons)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtWIPAcc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel3)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel4)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndbreakageglaccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndReserveStock)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndcreditdebitnoteclr)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndphysicalinventrycontrol)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fnddisassamblyexpense)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndshipmentclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndtransferclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndnonstockclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndassamblycostoff)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndadjustmentwriteoff)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndpayableclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndInventoryControl)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtbreakage)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblbreakage)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtReserveStock)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblReserveStock)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtcreditdebitnoteclr)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtphysicalinventryadj)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtdisassamblyexpense)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtshipmentexpense)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxttransferclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtnonstockclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtassamblycostcredit)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtadjustmentwriteoff)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtpayableclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtinventrycontrol)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblcreditdebitnoteclr)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblphysicalinventryadj)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlbldisassemblyexpenxe)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblshipmentclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlbltransferclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblnonstockclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblassamblycostcredit)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlbladjustmentwriteoff)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblpayableclearing)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblinventorycontrol)
        Me.rdgrpbxgeneralledgeraccounts.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxgeneralledgeraccounts.HeaderText = "General Ledger accounts"
        Me.rdgrpbxgeneralledgeraccounts.Location = New System.Drawing.Point(7, 43)
        Me.rdgrpbxgeneralledgeraccounts.Name = "rdgrpbxgeneralledgeraccounts"
        Me.rdgrpbxgeneralledgeraccounts.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxgeneralledgeraccounts.Size = New System.Drawing.Size(946, 391)
        Me.rdgrpbxgeneralledgeraccounts.TabIndex = 4
        Me.rdgrpbxgeneralledgeraccounts.Text = "General Ledger accounts"
        '
        'lblItemOpeningClearing
        '
        Me.lblItemOpeningClearing.Location = New System.Drawing.Point(239, 362)
        Me.lblItemOpeningClearing.Name = "lblItemOpeningClearing"
        Me.lblItemOpeningClearing.ReadOnly = True
        Me.lblItemOpeningClearing.Size = New System.Drawing.Size(225, 20)
        Me.lblItemOpeningClearing.TabIndex = 101
        Me.lblItemOpeningClearing.TabStop = False
        '
        'fndItemOpeningClearing
        '
        Me.fndItemOpeningClearing.CalculationExpression = Nothing
        Me.fndItemOpeningClearing.FieldCode = Nothing
        Me.fndItemOpeningClearing.FieldDesc = Nothing
        Me.fndItemOpeningClearing.FieldMaxLength = 0
        Me.fndItemOpeningClearing.FieldName = Nothing
        Me.fndItemOpeningClearing.isCalculatedField = False
        Me.fndItemOpeningClearing.IsSourceFromTable = False
        Me.fndItemOpeningClearing.IsSourceFromValueList = False
        Me.fndItemOpeningClearing.IsUnique = False
        Me.fndItemOpeningClearing.Location = New System.Drawing.Point(130, 362)
        Me.fndItemOpeningClearing.MendatroryField = False
        Me.fndItemOpeningClearing.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndItemOpeningClearing.MyLinkLable1 = Nothing
        Me.fndItemOpeningClearing.MyLinkLable2 = Nothing
        Me.fndItemOpeningClearing.MyReadOnly = False
        Me.fndItemOpeningClearing.MyShowMasterFormButton = False
        Me.fndItemOpeningClearing.Name = "fndItemOpeningClearing"
        Me.fndItemOpeningClearing.ReferenceFieldDesc = Nothing
        Me.fndItemOpeningClearing.ReferenceFieldName = Nothing
        Me.fndItemOpeningClearing.ReferenceTableName = Nothing
        Me.fndItemOpeningClearing.Size = New System.Drawing.Size(107, 19)
        Me.fndItemOpeningClearing.TabIndex = 100
        Me.fndItemOpeningClearing.Value = ""
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Location = New System.Drawing.Point(6, 363)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(119, 18)
        Me.MyLabel20.TabIndex = 98
        Me.MyLabel20.Text = "Item Opening Clearing"
        '
        'fndWrekageAccount
        '
        Me.fndWrekageAccount.CalculationExpression = Nothing
        Me.fndWrekageAccount.FieldCode = Nothing
        Me.fndWrekageAccount.FieldDesc = Nothing
        Me.fndWrekageAccount.FieldMaxLength = 0
        Me.fndWrekageAccount.FieldName = Nothing
        Me.fndWrekageAccount.isCalculatedField = False
        Me.fndWrekageAccount.IsSourceFromTable = False
        Me.fndWrekageAccount.IsSourceFromValueList = False
        Me.fndWrekageAccount.IsUnique = False
        Me.fndWrekageAccount.Location = New System.Drawing.Point(604, 342)
        Me.fndWrekageAccount.MendatroryField = False
        Me.fndWrekageAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndWrekageAccount.MyLinkLable1 = Nothing
        Me.fndWrekageAccount.MyLinkLable2 = Nothing
        Me.fndWrekageAccount.MyReadOnly = False
        Me.fndWrekageAccount.MyShowMasterFormButton = False
        Me.fndWrekageAccount.Name = "fndWrekageAccount"
        Me.fndWrekageAccount.ReferenceFieldDesc = Nothing
        Me.fndWrekageAccount.ReferenceFieldName = Nothing
        Me.fndWrekageAccount.ReferenceTableName = Nothing
        Me.fndWrekageAccount.Size = New System.Drawing.Size(107, 19)
        Me.fndWrekageAccount.TabIndex = 96
        Me.fndWrekageAccount.Value = ""
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(473, 342)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(95, 18)
        Me.MyLabel18.TabIndex = 94
        Me.MyLabel18.Text = "Wrekage Account"
        '
        'txtWrekageAccount
        '
        Me.txtWrekageAccount.Location = New System.Drawing.Point(714, 342)
        Me.txtWrekageAccount.Name = "txtWrekageAccount"
        Me.txtWrekageAccount.ReadOnly = True
        Me.txtWrekageAccount.Size = New System.Drawing.Size(225, 20)
        Me.txtWrekageAccount.TabIndex = 95
        Me.txtWrekageAccount.TabStop = False
        '
        'fndPurchaseLoss
        '
        Me.fndPurchaseLoss.CalculationExpression = Nothing
        Me.fndPurchaseLoss.FieldCode = Nothing
        Me.fndPurchaseLoss.FieldDesc = Nothing
        Me.fndPurchaseLoss.FieldMaxLength = 0
        Me.fndPurchaseLoss.FieldName = Nothing
        Me.fndPurchaseLoss.isCalculatedField = False
        Me.fndPurchaseLoss.IsSourceFromTable = False
        Me.fndPurchaseLoss.IsSourceFromValueList = False
        Me.fndPurchaseLoss.IsUnique = False
        Me.fndPurchaseLoss.Location = New System.Drawing.Point(130, 341)
        Me.fndPurchaseLoss.MendatroryField = False
        Me.fndPurchaseLoss.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPurchaseLoss.MyLinkLable1 = Nothing
        Me.fndPurchaseLoss.MyLinkLable2 = Nothing
        Me.fndPurchaseLoss.MyReadOnly = False
        Me.fndPurchaseLoss.MyShowMasterFormButton = False
        Me.fndPurchaseLoss.Name = "fndPurchaseLoss"
        Me.fndPurchaseLoss.ReferenceFieldDesc = Nothing
        Me.fndPurchaseLoss.ReferenceFieldName = Nothing
        Me.fndPurchaseLoss.ReferenceTableName = Nothing
        Me.fndPurchaseLoss.Size = New System.Drawing.Size(107, 19)
        Me.fndPurchaseLoss.TabIndex = 93
        Me.fndPurchaseLoss.Value = ""
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(6, 342)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(76, 18)
        Me.MyLabel17.TabIndex = 91
        Me.MyLabel17.Text = "Purchase Loss"
        '
        'txtPurchaseLoss
        '
        Me.txtPurchaseLoss.Location = New System.Drawing.Point(239, 341)
        Me.txtPurchaseLoss.Name = "txtPurchaseLoss"
        Me.txtPurchaseLoss.ReadOnly = True
        Me.txtPurchaseLoss.Size = New System.Drawing.Size(225, 20)
        Me.txtPurchaseLoss.TabIndex = 92
        Me.txtPurchaseLoss.TabStop = False
        '
        'fndFAAccount
        '
        Me.fndFAAccount.CalculationExpression = Nothing
        Me.fndFAAccount.FieldCode = Nothing
        Me.fndFAAccount.FieldDesc = Nothing
        Me.fndFAAccount.FieldMaxLength = 0
        Me.fndFAAccount.FieldName = Nothing
        Me.fndFAAccount.isCalculatedField = False
        Me.fndFAAccount.IsSourceFromTable = False
        Me.fndFAAccount.IsSourceFromValueList = False
        Me.fndFAAccount.IsUnique = False
        Me.fndFAAccount.Location = New System.Drawing.Point(605, 321)
        Me.fndFAAccount.MendatroryField = False
        Me.fndFAAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFAAccount.MyLinkLable1 = Nothing
        Me.fndFAAccount.MyLinkLable2 = Nothing
        Me.fndFAAccount.MyReadOnly = False
        Me.fndFAAccount.MyShowMasterFormButton = False
        Me.fndFAAccount.Name = "fndFAAccount"
        Me.fndFAAccount.ReferenceFieldDesc = Nothing
        Me.fndFAAccount.ReferenceFieldName = Nothing
        Me.fndFAAccount.ReferenceTableName = Nothing
        Me.fndFAAccount.Size = New System.Drawing.Size(107, 19)
        Me.fndFAAccount.TabIndex = 90
        Me.fndFAAccount.Value = ""
        '
        'lblFaAccountDes
        '
        Me.lblFaAccountDes.Location = New System.Drawing.Point(714, 320)
        Me.lblFaAccountDes.Name = "lblFaAccountDes"
        Me.lblFaAccountDes.ReadOnly = True
        Me.lblFaAccountDes.Size = New System.Drawing.Size(224, 20)
        Me.lblFaAccountDes.TabIndex = 89
        Me.lblFaAccountDes.TabStop = False
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Location = New System.Drawing.Point(606, 289)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(87, 8)
        Me.UcCustomFields1.TabIndex = 88
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(471, 319)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel1.TabIndex = 87
        Me.RadLabel1.Text = "FA Account"
        '
        'txtStoreConsumtion
        '
        Me.txtStoreConsumtion.Location = New System.Drawing.Point(714, 276)
        Me.txtStoreConsumtion.Name = "txtStoreConsumtion"
        Me.txtStoreConsumtion.ReadOnly = True
        Me.txtStoreConsumtion.Size = New System.Drawing.Size(224, 20)
        Me.txtStoreConsumtion.TabIndex = 86
        Me.txtStoreConsumtion.TabStop = False
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Location = New System.Drawing.Point(467, 276)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(123, 18)
        Me.MyLabel16.TabIndex = 85
        Me.MyLabel16.Text = "Store Consumption A/c"
        '
        'fndStoreConsumptionAcc
        '
        Me.fndStoreConsumptionAcc.CalculationExpression = Nothing
        Me.fndStoreConsumptionAcc.FieldCode = Nothing
        Me.fndStoreConsumptionAcc.FieldDesc = Nothing
        Me.fndStoreConsumptionAcc.FieldMaxLength = 0
        Me.fndStoreConsumptionAcc.FieldName = Nothing
        Me.fndStoreConsumptionAcc.isCalculatedField = False
        Me.fndStoreConsumptionAcc.IsSourceFromTable = False
        Me.fndStoreConsumptionAcc.IsSourceFromValueList = False
        Me.fndStoreConsumptionAcc.IsUnique = False
        Me.fndStoreConsumptionAcc.Location = New System.Drawing.Point(605, 276)
        Me.fndStoreConsumptionAcc.MendatroryField = False
        Me.fndStoreConsumptionAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndStoreConsumptionAcc.MyLinkLable1 = Nothing
        Me.fndStoreConsumptionAcc.MyLinkLable2 = Nothing
        Me.fndStoreConsumptionAcc.MyReadOnly = False
        Me.fndStoreConsumptionAcc.MyShowMasterFormButton = False
        Me.fndStoreConsumptionAcc.Name = "fndStoreConsumptionAcc"
        Me.fndStoreConsumptionAcc.ReferenceFieldDesc = Nothing
        Me.fndStoreConsumptionAcc.ReferenceFieldName = Nothing
        Me.fndStoreConsumptionAcc.ReferenceTableName = Nothing
        Me.fndStoreConsumptionAcc.Size = New System.Drawing.Size(107, 19)
        Me.fndStoreConsumptionAcc.TabIndex = 84
        Me.fndStoreConsumptionAcc.Value = ""
        '
        'txtStockTransferJobWork
        '
        Me.txtStockTransferJobWork.CalculationExpression = Nothing
        Me.txtStockTransferJobWork.FieldCode = Nothing
        Me.txtStockTransferJobWork.FieldDesc = Nothing
        Me.txtStockTransferJobWork.FieldMaxLength = 0
        Me.txtStockTransferJobWork.FieldName = Nothing
        Me.txtStockTransferJobWork.isCalculatedField = False
        Me.txtStockTransferJobWork.IsSourceFromTable = False
        Me.txtStockTransferJobWork.IsSourceFromValueList = False
        Me.txtStockTransferJobWork.IsUnique = False
        Me.txtStockTransferJobWork.Location = New System.Drawing.Point(605, 256)
        Me.txtStockTransferJobWork.MendatroryField = False
        Me.txtStockTransferJobWork.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockTransferJobWork.MyLinkLable1 = Nothing
        Me.txtStockTransferJobWork.MyLinkLable2 = Nothing
        Me.txtStockTransferJobWork.MyReadOnly = False
        Me.txtStockTransferJobWork.MyShowMasterFormButton = False
        Me.txtStockTransferJobWork.Name = "txtStockTransferJobWork"
        Me.txtStockTransferJobWork.ReferenceFieldDesc = Nothing
        Me.txtStockTransferJobWork.ReferenceFieldName = Nothing
        Me.txtStockTransferJobWork.ReferenceTableName = Nothing
        Me.txtStockTransferJobWork.Size = New System.Drawing.Size(107, 19)
        Me.txtStockTransferJobWork.TabIndex = 83
        Me.txtStockTransferJobWork.Value = ""
        '
        'lblStockTransferJobWorkDesc
        '
        Me.lblStockTransferJobWorkDesc.Location = New System.Drawing.Point(714, 255)
        Me.lblStockTransferJobWorkDesc.Name = "lblStockTransferJobWorkDesc"
        Me.lblStockTransferJobWorkDesc.ReadOnly = True
        Me.lblStockTransferJobWorkDesc.Size = New System.Drawing.Size(224, 20)
        Me.lblStockTransferJobWorkDesc.TabIndex = 82
        Me.lblStockTransferJobWorkDesc.TabStop = False
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Location = New System.Drawing.Point(468, 257)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(127, 18)
        Me.MyLabel15.TabIndex = 81
        Me.MyLabel15.Text = "Stock Transfer Job Work"
        '
        'fndEMP
        '
        Me.fndEMP.CalculationExpression = Nothing
        Me.fndEMP.FieldCode = Nothing
        Me.fndEMP.FieldDesc = Nothing
        Me.fndEMP.FieldMaxLength = 0
        Me.fndEMP.FieldName = Nothing
        Me.fndEMP.isCalculatedField = False
        Me.fndEMP.IsSourceFromTable = False
        Me.fndEMP.IsSourceFromValueList = False
        Me.fndEMP.IsUnique = False
        Me.fndEMP.Location = New System.Drawing.Point(130, 319)
        Me.fndEMP.MendatroryField = False
        Me.fndEMP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEMP.MyLinkLable1 = Nothing
        Me.fndEMP.MyLinkLable2 = Nothing
        Me.fndEMP.MyReadOnly = False
        Me.fndEMP.MyShowMasterFormButton = False
        Me.fndEMP.Name = "fndEMP"
        Me.fndEMP.ReferenceFieldDesc = Nothing
        Me.fndEMP.ReferenceFieldName = Nothing
        Me.fndEMP.ReferenceTableName = Nothing
        Me.fndEMP.Size = New System.Drawing.Size(107, 19)
        Me.fndEMP.TabIndex = 80
        Me.fndEMP.Value = ""
        '
        'fndHandlingCharge
        '
        Me.fndHandlingCharge.CalculationExpression = Nothing
        Me.fndHandlingCharge.FieldCode = Nothing
        Me.fndHandlingCharge.FieldDesc = Nothing
        Me.fndHandlingCharge.FieldMaxLength = 0
        Me.fndHandlingCharge.FieldName = Nothing
        Me.fndHandlingCharge.isCalculatedField = False
        Me.fndHandlingCharge.IsSourceFromTable = False
        Me.fndHandlingCharge.IsSourceFromValueList = False
        Me.fndHandlingCharge.IsUnique = False
        Me.fndHandlingCharge.Location = New System.Drawing.Point(130, 298)
        Me.fndHandlingCharge.MendatroryField = False
        Me.fndHandlingCharge.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndHandlingCharge.MyLinkLable1 = Nothing
        Me.fndHandlingCharge.MyLinkLable2 = Nothing
        Me.fndHandlingCharge.MyReadOnly = False
        Me.fndHandlingCharge.MyShowMasterFormButton = False
        Me.fndHandlingCharge.Name = "fndHandlingCharge"
        Me.fndHandlingCharge.ReferenceFieldDesc = Nothing
        Me.fndHandlingCharge.ReferenceFieldName = Nothing
        Me.fndHandlingCharge.ReferenceTableName = Nothing
        Me.fndHandlingCharge.Size = New System.Drawing.Size(107, 19)
        Me.fndHandlingCharge.TabIndex = 77
        Me.fndHandlingCharge.Value = ""
        '
        'lblEMP
        '
        Me.lblEMP.FieldName = Nothing
        Me.lblEMP.Location = New System.Drawing.Point(6, 320)
        Me.lblEMP.Name = "lblEMP"
        Me.lblEMP.Size = New System.Drawing.Size(28, 18)
        Me.lblEMP.TabIndex = 78
        Me.lblEMP.Text = "EMP"
        '
        'txtEMP
        '
        Me.txtEMP.Location = New System.Drawing.Point(239, 319)
        Me.txtEMP.Name = "txtEMP"
        Me.txtEMP.ReadOnly = True
        Me.txtEMP.Size = New System.Drawing.Size(225, 20)
        Me.txtEMP.TabIndex = 79
        Me.txtEMP.TabStop = False
        '
        'txtHandlingCharge
        '
        Me.txtHandlingCharge.Location = New System.Drawing.Point(239, 297)
        Me.txtHandlingCharge.Name = "txtHandlingCharge"
        Me.txtHandlingCharge.ReadOnly = True
        Me.txtHandlingCharge.Size = New System.Drawing.Size(224, 20)
        Me.txtHandlingCharge.TabIndex = 76
        Me.txtHandlingCharge.TabStop = False
        '
        'lblHandlingCharge
        '
        Me.lblHandlingCharge.FieldName = Nothing
        Me.lblHandlingCharge.Location = New System.Drawing.Point(5, 298)
        Me.lblHandlingCharge.Name = "lblHandlingCharge"
        Me.lblHandlingCharge.Size = New System.Drawing.Size(90, 18)
        Me.lblHandlingCharge.TabIndex = 75
        Me.lblHandlingCharge.Text = "Handling Charge"
        '
        'txtDifferenceAccount
        '
        Me.txtDifferenceAccount.CalculationExpression = Nothing
        Me.txtDifferenceAccount.FieldCode = Nothing
        Me.txtDifferenceAccount.FieldDesc = Nothing
        Me.txtDifferenceAccount.FieldMaxLength = 0
        Me.txtDifferenceAccount.FieldName = Nothing
        Me.txtDifferenceAccount.isCalculatedField = False
        Me.txtDifferenceAccount.IsSourceFromTable = False
        Me.txtDifferenceAccount.IsSourceFromValueList = False
        Me.txtDifferenceAccount.IsUnique = False
        Me.txtDifferenceAccount.Location = New System.Drawing.Point(130, 277)
        Me.txtDifferenceAccount.MendatroryField = False
        Me.txtDifferenceAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDifferenceAccount.MyLinkLable1 = Nothing
        Me.txtDifferenceAccount.MyLinkLable2 = Nothing
        Me.txtDifferenceAccount.MyReadOnly = False
        Me.txtDifferenceAccount.MyShowMasterFormButton = False
        Me.txtDifferenceAccount.Name = "txtDifferenceAccount"
        Me.txtDifferenceAccount.ReferenceFieldDesc = Nothing
        Me.txtDifferenceAccount.ReferenceFieldName = Nothing
        Me.txtDifferenceAccount.ReferenceTableName = Nothing
        Me.txtDifferenceAccount.Size = New System.Drawing.Size(107, 19)
        Me.txtDifferenceAccount.TabIndex = 74
        Me.txtDifferenceAccount.Value = ""
        '
        'lblDifferenceAccount
        '
        Me.lblDifferenceAccount.Location = New System.Drawing.Point(239, 276)
        Me.lblDifferenceAccount.Name = "lblDifferenceAccount"
        Me.lblDifferenceAccount.ReadOnly = True
        Me.lblDifferenceAccount.Size = New System.Drawing.Size(224, 20)
        Me.lblDifferenceAccount.TabIndex = 73
        Me.lblDifferenceAccount.TabStop = False
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(5, 277)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(101, 18)
        Me.MyLabel14.TabIndex = 72
        Me.MyLabel14.Text = "Difference Account"
        '
        'txtPurchaseJobWork
        '
        Me.txtPurchaseJobWork.CalculationExpression = Nothing
        Me.txtPurchaseJobWork.FieldCode = Nothing
        Me.txtPurchaseJobWork.FieldDesc = Nothing
        Me.txtPurchaseJobWork.FieldMaxLength = 0
        Me.txtPurchaseJobWork.FieldName = Nothing
        Me.txtPurchaseJobWork.isCalculatedField = False
        Me.txtPurchaseJobWork.IsSourceFromTable = False
        Me.txtPurchaseJobWork.IsSourceFromValueList = False
        Me.txtPurchaseJobWork.IsUnique = False
        Me.txtPurchaseJobWork.Location = New System.Drawing.Point(130, 257)
        Me.txtPurchaseJobWork.MendatroryField = False
        Me.txtPurchaseJobWork.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseJobWork.MyLinkLable1 = Nothing
        Me.txtPurchaseJobWork.MyLinkLable2 = Nothing
        Me.txtPurchaseJobWork.MyReadOnly = False
        Me.txtPurchaseJobWork.MyShowMasterFormButton = False
        Me.txtPurchaseJobWork.Name = "txtPurchaseJobWork"
        Me.txtPurchaseJobWork.ReferenceFieldDesc = Nothing
        Me.txtPurchaseJobWork.ReferenceFieldName = Nothing
        Me.txtPurchaseJobWork.ReferenceTableName = Nothing
        Me.txtPurchaseJobWork.Size = New System.Drawing.Size(107, 19)
        Me.txtPurchaseJobWork.TabIndex = 71
        Me.txtPurchaseJobWork.Value = ""
        '
        'lblPurchaseJobwork
        '
        Me.lblPurchaseJobwork.Location = New System.Drawing.Point(239, 256)
        Me.lblPurchaseJobwork.Name = "lblPurchaseJobwork"
        Me.lblPurchaseJobwork.ReadOnly = True
        Me.lblPurchaseJobwork.Size = New System.Drawing.Size(224, 20)
        Me.lblPurchaseJobwork.TabIndex = 70
        Me.lblPurchaseJobwork.TabStop = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(5, 257)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(101, 18)
        Me.MyLabel13.TabIndex = 69
        Me.MyLabel13.Text = "Purchase Job Work"
        '
        'txtFreightCharges
        '
        Me.txtFreightCharges.CalculationExpression = Nothing
        Me.txtFreightCharges.FieldCode = Nothing
        Me.txtFreightCharges.FieldDesc = Nothing
        Me.txtFreightCharges.FieldMaxLength = 0
        Me.txtFreightCharges.FieldName = Nothing
        Me.txtFreightCharges.isCalculatedField = False
        Me.txtFreightCharges.IsSourceFromTable = False
        Me.txtFreightCharges.IsSourceFromValueList = False
        Me.txtFreightCharges.IsUnique = False
        Me.txtFreightCharges.Location = New System.Drawing.Point(605, 235)
        Me.txtFreightCharges.MendatroryField = False
        Me.txtFreightCharges.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFreightCharges.MyLinkLable1 = Nothing
        Me.txtFreightCharges.MyLinkLable2 = Nothing
        Me.txtFreightCharges.MyReadOnly = False
        Me.txtFreightCharges.MyShowMasterFormButton = False
        Me.txtFreightCharges.Name = "txtFreightCharges"
        Me.txtFreightCharges.ReferenceFieldDesc = Nothing
        Me.txtFreightCharges.ReferenceFieldName = Nothing
        Me.txtFreightCharges.ReferenceTableName = Nothing
        Me.txtFreightCharges.Size = New System.Drawing.Size(107, 20)
        Me.txtFreightCharges.TabIndex = 60
        Me.txtFreightCharges.Value = ""
        '
        'txtChiilingCharges
        '
        Me.txtChiilingCharges.CalculationExpression = Nothing
        Me.txtChiilingCharges.FieldCode = Nothing
        Me.txtChiilingCharges.FieldDesc = Nothing
        Me.txtChiilingCharges.FieldMaxLength = 0
        Me.txtChiilingCharges.FieldName = Nothing
        Me.txtChiilingCharges.isCalculatedField = False
        Me.txtChiilingCharges.IsSourceFromTable = False
        Me.txtChiilingCharges.IsSourceFromValueList = False
        Me.txtChiilingCharges.IsUnique = False
        Me.txtChiilingCharges.Location = New System.Drawing.Point(605, 215)
        Me.txtChiilingCharges.MendatroryField = False
        Me.txtChiilingCharges.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChiilingCharges.MyLinkLable1 = Nothing
        Me.txtChiilingCharges.MyLinkLable2 = Nothing
        Me.txtChiilingCharges.MyReadOnly = False
        Me.txtChiilingCharges.MyShowMasterFormButton = False
        Me.txtChiilingCharges.Name = "txtChiilingCharges"
        Me.txtChiilingCharges.ReferenceFieldDesc = Nothing
        Me.txtChiilingCharges.ReferenceFieldName = Nothing
        Me.txtChiilingCharges.ReferenceTableName = Nothing
        Me.txtChiilingCharges.Size = New System.Drawing.Size(107, 20)
        Me.txtChiilingCharges.TabIndex = 66
        Me.txtChiilingCharges.Value = ""
        '
        'lblFreightCharges
        '
        Me.lblFreightCharges.Location = New System.Drawing.Point(715, 235)
        Me.lblFreightCharges.Name = "lblFreightCharges"
        Me.lblFreightCharges.ReadOnly = True
        Me.lblFreightCharges.Size = New System.Drawing.Size(224, 20)
        Me.lblFreightCharges.TabIndex = 62
        Me.lblFreightCharges.TabStop = False
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(467, 236)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel12.TabIndex = 61
        Me.MyLabel12.Text = "Freight Charges"
        '
        'lblChiilingCharges
        '
        Me.lblChiilingCharges.Location = New System.Drawing.Point(714, 215)
        Me.lblChiilingCharges.Name = "lblChiilingCharges"
        Me.lblChiilingCharges.ReadOnly = True
        Me.lblChiilingCharges.Size = New System.Drawing.Size(224, 20)
        Me.lblChiilingCharges.TabIndex = 68
        Me.lblChiilingCharges.TabStop = False
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(467, 216)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(87, 18)
        Me.MyLabel11.TabIndex = 67
        Me.MyLabel11.Text = "Chilling Charges"
        '
        'txtProvisionClearing
        '
        Me.txtProvisionClearing.CalculationExpression = Nothing
        Me.txtProvisionClearing.FieldCode = Nothing
        Me.txtProvisionClearing.FieldDesc = Nothing
        Me.txtProvisionClearing.FieldMaxLength = 0
        Me.txtProvisionClearing.FieldName = Nothing
        Me.txtProvisionClearing.isCalculatedField = False
        Me.txtProvisionClearing.IsSourceFromTable = False
        Me.txtProvisionClearing.IsSourceFromValueList = False
        Me.txtProvisionClearing.IsUnique = False
        Me.txtProvisionClearing.Location = New System.Drawing.Point(130, 236)
        Me.txtProvisionClearing.MendatroryField = False
        Me.txtProvisionClearing.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProvisionClearing.MyLinkLable1 = Nothing
        Me.txtProvisionClearing.MyLinkLable2 = Nothing
        Me.txtProvisionClearing.MyReadOnly = False
        Me.txtProvisionClearing.MyShowMasterFormButton = False
        Me.txtProvisionClearing.Name = "txtProvisionClearing"
        Me.txtProvisionClearing.ReferenceFieldDesc = Nothing
        Me.txtProvisionClearing.ReferenceFieldName = Nothing
        Me.txtProvisionClearing.ReferenceTableName = Nothing
        Me.txtProvisionClearing.Size = New System.Drawing.Size(107, 19)
        Me.txtProvisionClearing.TabIndex = 65
        Me.txtProvisionClearing.Value = ""
        '
        'lblProvisioinClearing
        '
        Me.lblProvisioinClearing.Location = New System.Drawing.Point(239, 235)
        Me.lblProvisioinClearing.Name = "lblProvisioinClearing"
        Me.lblProvisioinClearing.ReadOnly = True
        Me.lblProvisioinClearing.Size = New System.Drawing.Size(224, 20)
        Me.lblProvisioinClearing.TabIndex = 64
        Me.lblProvisioinClearing.TabStop = False
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(5, 236)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(105, 18)
        Me.MyLabel10.TabIndex = 63
        Me.MyLabel10.Text = "Provisional Clearing"
        '
        'FndStockTransferAccount
        '
        Me.FndStockTransferAccount.CalculationExpression = Nothing
        Me.FndStockTransferAccount.FieldCode = Nothing
        Me.FndStockTransferAccount.FieldDesc = Nothing
        Me.FndStockTransferAccount.FieldMaxLength = 0
        Me.FndStockTransferAccount.FieldName = Nothing
        Me.FndStockTransferAccount.isCalculatedField = False
        Me.FndStockTransferAccount.IsSourceFromTable = False
        Me.FndStockTransferAccount.IsSourceFromValueList = False
        Me.FndStockTransferAccount.IsUnique = False
        Me.FndStockTransferAccount.Location = New System.Drawing.Point(130, 216)
        Me.FndStockTransferAccount.MendatroryField = False
        Me.FndStockTransferAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndStockTransferAccount.MyLinkLable1 = Nothing
        Me.FndStockTransferAccount.MyLinkLable2 = Nothing
        Me.FndStockTransferAccount.MyReadOnly = False
        Me.FndStockTransferAccount.MyShowMasterFormButton = False
        Me.FndStockTransferAccount.Name = "FndStockTransferAccount"
        Me.FndStockTransferAccount.ReferenceFieldDesc = Nothing
        Me.FndStockTransferAccount.ReferenceFieldName = Nothing
        Me.FndStockTransferAccount.ReferenceTableName = Nothing
        Me.FndStockTransferAccount.Size = New System.Drawing.Size(107, 19)
        Me.FndStockTransferAccount.TabIndex = 62
        Me.FndStockTransferAccount.Value = ""
        '
        'txtStockTransferIn
        '
        Me.txtStockTransferIn.CalculationExpression = Nothing
        Me.txtStockTransferIn.FieldCode = Nothing
        Me.txtStockTransferIn.FieldDesc = Nothing
        Me.txtStockTransferIn.FieldMaxLength = 0
        Me.txtStockTransferIn.FieldName = Nothing
        Me.txtStockTransferIn.isCalculatedField = False
        Me.txtStockTransferIn.IsSourceFromTable = False
        Me.txtStockTransferIn.IsSourceFromValueList = False
        Me.txtStockTransferIn.IsUnique = False
        Me.txtStockTransferIn.Location = New System.Drawing.Point(605, 195)
        Me.txtStockTransferIn.MendatroryField = False
        Me.txtStockTransferIn.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockTransferIn.MyLinkLable1 = Nothing
        Me.txtStockTransferIn.MyLinkLable2 = Nothing
        Me.txtStockTransferIn.MyReadOnly = False
        Me.txtStockTransferIn.MyShowMasterFormButton = False
        Me.txtStockTransferIn.Name = "txtStockTransferIn"
        Me.txtStockTransferIn.ReferenceFieldDesc = Nothing
        Me.txtStockTransferIn.ReferenceFieldName = Nothing
        Me.txtStockTransferIn.ReferenceTableName = Nothing
        Me.txtStockTransferIn.Size = New System.Drawing.Size(107, 20)
        Me.txtStockTransferIn.TabIndex = 57
        Me.txtStockTransferIn.Value = ""
        '
        'lblStockTransferIn
        '
        Me.lblStockTransferIn.Location = New System.Drawing.Point(714, 195)
        Me.lblStockTransferIn.Name = "lblStockTransferIn"
        Me.lblStockTransferIn.ReadOnly = True
        Me.lblStockTransferIn.Size = New System.Drawing.Size(224, 20)
        Me.lblStockTransferIn.TabIndex = 59
        Me.lblStockTransferIn.TabStop = False
        '
        'TxtStockTransferAccount
        '
        Me.TxtStockTransferAccount.Location = New System.Drawing.Point(239, 215)
        Me.TxtStockTransferAccount.Name = "TxtStockTransferAccount"
        Me.TxtStockTransferAccount.ReadOnly = True
        Me.TxtStockTransferAccount.Size = New System.Drawing.Size(224, 20)
        Me.TxtStockTransferAccount.TabIndex = 61
        Me.TxtStockTransferAccount.TabStop = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(5, 216)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel9.TabIndex = 60
        Me.MyLabel9.Text = "Stock Transfer A/C"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(467, 196)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(89, 18)
        Me.MyLabel8.TabIndex = 58
        Me.MyLabel8.Text = "Stock Transfer In"
        '
        'FndJobWork
        '
        Me.FndJobWork.CalculationExpression = Nothing
        Me.FndJobWork.FieldCode = Nothing
        Me.FndJobWork.FieldDesc = Nothing
        Me.FndJobWork.FieldMaxLength = 0
        Me.FndJobWork.FieldName = Nothing
        Me.FndJobWork.isCalculatedField = False
        Me.FndJobWork.IsSourceFromTable = False
        Me.FndJobWork.IsSourceFromValueList = False
        Me.FndJobWork.IsUnique = False
        Me.FndJobWork.Location = New System.Drawing.Point(605, 176)
        Me.FndJobWork.MendatroryField = True
        Me.FndJobWork.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndJobWork.MyLinkLable1 = Nothing
        Me.FndJobWork.MyLinkLable2 = Nothing
        Me.FndJobWork.MyReadOnly = False
        Me.FndJobWork.MyShowMasterFormButton = False
        Me.FndJobWork.Name = "FndJobWork"
        Me.FndJobWork.ReferenceFieldDesc = Nothing
        Me.FndJobWork.ReferenceFieldName = Nothing
        Me.FndJobWork.ReferenceTableName = Nothing
        Me.FndJobWork.Size = New System.Drawing.Size(107, 19)
        Me.FndJobWork.TabIndex = 54
        Me.FndJobWork.Value = ""
        '
        'LblJobwork
        '
        Me.LblJobwork.Location = New System.Drawing.Point(714, 175)
        Me.LblJobwork.Name = "LblJobwork"
        Me.LblJobwork.ReadOnly = True
        Me.LblJobwork.Size = New System.Drawing.Size(224, 20)
        Me.LblJobwork.TabIndex = 56
        Me.LblJobwork.TabStop = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(467, 176)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel7.TabIndex = 55
        Me.MyLabel7.Text = "JobWork Account"
        '
        'fndTransferGainLoss
        '
        Me.fndTransferGainLoss.CalculationExpression = Nothing
        Me.fndTransferGainLoss.FieldCode = Nothing
        Me.fndTransferGainLoss.FieldDesc = Nothing
        Me.fndTransferGainLoss.FieldMaxLength = 0
        Me.fndTransferGainLoss.FieldName = Nothing
        Me.fndTransferGainLoss.isCalculatedField = False
        Me.fndTransferGainLoss.IsSourceFromTable = False
        Me.fndTransferGainLoss.IsSourceFromValueList = False
        Me.fndTransferGainLoss.IsUnique = False
        Me.fndTransferGainLoss.Location = New System.Drawing.Point(605, 156)
        Me.fndTransferGainLoss.MendatroryField = False
        Me.fndTransferGainLoss.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransferGainLoss.MyLinkLable1 = Nothing
        Me.fndTransferGainLoss.MyLinkLable2 = Nothing
        Me.fndTransferGainLoss.MyReadOnly = False
        Me.fndTransferGainLoss.MyShowMasterFormButton = False
        Me.fndTransferGainLoss.Name = "fndTransferGainLoss"
        Me.fndTransferGainLoss.ReferenceFieldDesc = Nothing
        Me.fndTransferGainLoss.ReferenceFieldName = Nothing
        Me.fndTransferGainLoss.ReferenceTableName = Nothing
        Me.fndTransferGainLoss.Size = New System.Drawing.Size(107, 19)
        Me.fndTransferGainLoss.TabIndex = 51
        Me.fndTransferGainLoss.Value = ""
        '
        'txtPurchaseCTRL_Ac
        '
        Me.txtPurchaseCTRL_Ac.CalculationExpression = Nothing
        Me.txtPurchaseCTRL_Ac.FieldCode = Nothing
        Me.txtPurchaseCTRL_Ac.FieldDesc = Nothing
        Me.txtPurchaseCTRL_Ac.FieldMaxLength = 0
        Me.txtPurchaseCTRL_Ac.FieldName = Nothing
        Me.txtPurchaseCTRL_Ac.isCalculatedField = False
        Me.txtPurchaseCTRL_Ac.IsSourceFromTable = False
        Me.txtPurchaseCTRL_Ac.IsSourceFromValueList = False
        Me.txtPurchaseCTRL_Ac.IsUnique = False
        Me.txtPurchaseCTRL_Ac.Location = New System.Drawing.Point(605, 298)
        Me.txtPurchaseCTRL_Ac.MendatroryField = True
        Me.txtPurchaseCTRL_Ac.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseCTRL_Ac.MyLinkLable1 = Nothing
        Me.txtPurchaseCTRL_Ac.MyLinkLable2 = Nothing
        Me.txtPurchaseCTRL_Ac.MyReadOnly = False
        Me.txtPurchaseCTRL_Ac.MyShowMasterFormButton = False
        Me.txtPurchaseCTRL_Ac.Name = "txtPurchaseCTRL_Ac"
        Me.txtPurchaseCTRL_Ac.ReferenceFieldDesc = Nothing
        Me.txtPurchaseCTRL_Ac.ReferenceFieldName = Nothing
        Me.txtPurchaseCTRL_Ac.ReferenceTableName = Nothing
        Me.txtPurchaseCTRL_Ac.Size = New System.Drawing.Size(107, 19)
        Me.txtPurchaseCTRL_Ac.TabIndex = 51
        Me.txtPurchaseCTRL_Ac.Value = ""
        Me.txtPurchaseCTRL_Ac.Visible = False
        '
        'txtTransferGainLossDesc
        '
        Me.txtTransferGainLossDesc.Location = New System.Drawing.Point(714, 155)
        Me.txtTransferGainLossDesc.Name = "txtTransferGainLossDesc"
        Me.txtTransferGainLossDesc.ReadOnly = True
        Me.txtTransferGainLossDesc.Size = New System.Drawing.Size(224, 20)
        Me.txtTransferGainLossDesc.TabIndex = 53
        Me.txtTransferGainLossDesc.TabStop = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(467, 156)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(123, 18)
        Me.MyLabel6.TabIndex = 52
        Me.MyLabel6.Text = "Transfer Profit/Loss A/c"
        '
        'txtPurchaseCtrlAcDesc
        '
        Me.txtPurchaseCtrlAcDesc.Location = New System.Drawing.Point(714, 298)
        Me.txtPurchaseCtrlAcDesc.Name = "txtPurchaseCtrlAcDesc"
        Me.txtPurchaseCtrlAcDesc.ReadOnly = True
        Me.txtPurchaseCtrlAcDesc.Size = New System.Drawing.Size(224, 20)
        Me.txtPurchaseCtrlAcDesc.TabIndex = 53
        Me.txtPurchaseCtrlAcDesc.TabStop = False
        Me.txtPurchaseCtrlAcDesc.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(468, 298)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(136, 18)
        Me.MyLabel5.TabIndex = 52
        Me.MyLabel5.Text = "Purchase Control Account"
        Me.MyLabel5.Visible = False
        '
        'txtLossAc
        '
        Me.txtLossAc.Location = New System.Drawing.Point(714, 135)
        Me.txtLossAc.Name = "txtLossAc"
        Me.txtLossAc.ReadOnly = True
        Me.txtLossAc.Size = New System.Drawing.Size(224, 20)
        Me.txtLossAc.TabIndex = 50
        Me.txtLossAc.TabStop = False
        '
        'fndLossAc
        '
        Me.fndLossAc.CalculationExpression = Nothing
        Me.fndLossAc.FieldCode = Nothing
        Me.fndLossAc.FieldDesc = Nothing
        Me.fndLossAc.FieldMaxLength = 0
        Me.fndLossAc.FieldName = Nothing
        Me.fndLossAc.isCalculatedField = False
        Me.fndLossAc.IsSourceFromTable = False
        Me.fndLossAc.IsSourceFromValueList = False
        Me.fndLossAc.IsUnique = False
        Me.fndLossAc.Location = New System.Drawing.Point(605, 136)
        Me.fndLossAc.MendatroryField = False
        Me.fndLossAc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLossAc.MyLinkLable1 = Me.lblLossAc
        Me.fndLossAc.MyLinkLable2 = Nothing
        Me.fndLossAc.MyReadOnly = False
        Me.fndLossAc.MyShowMasterFormButton = False
        Me.fndLossAc.Name = "fndLossAc"
        Me.fndLossAc.ReferenceFieldDesc = Nothing
        Me.fndLossAc.ReferenceFieldName = Nothing
        Me.fndLossAc.ReferenceTableName = Nothing
        Me.fndLossAc.Size = New System.Drawing.Size(107, 18)
        Me.fndLossAc.TabIndex = 49
        Me.fndLossAc.Value = ""
        Me.fndLossAc.Visible = False
        '
        'lblLossAc
        '
        Me.lblLossAc.FieldName = Nothing
        Me.lblLossAc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLossAc.Location = New System.Drawing.Point(467, 137)
        Me.lblLossAc.Name = "lblLossAc"
        Me.lblLossAc.Size = New System.Drawing.Size(52, 16)
        Me.lblLossAc.TabIndex = 48
        Me.lblLossAc.Text = "Loss A/C"
        Me.lblLossAc.Visible = False
        '
        'fndOther2
        '
        Me.fndOther2.CalculationExpression = Nothing
        Me.fndOther2.FieldCode = Nothing
        Me.fndOther2.FieldDesc = Nothing
        Me.fndOther2.FieldMaxLength = 0
        Me.fndOther2.FieldName = Nothing
        Me.fndOther2.isCalculatedField = False
        Me.fndOther2.IsSourceFromTable = False
        Me.fndOther2.IsSourceFromValueList = False
        Me.fndOther2.IsUnique = False
        Me.fndOther2.Location = New System.Drawing.Point(605, 116)
        Me.fndOther2.MendatroryField = False
        Me.fndOther2.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOther2.MyLinkLable1 = Nothing
        Me.fndOther2.MyLinkLable2 = Nothing
        Me.fndOther2.MyReadOnly = False
        Me.fndOther2.MyShowMasterFormButton = False
        Me.fndOther2.Name = "fndOther2"
        Me.fndOther2.ReferenceFieldDesc = Nothing
        Me.fndOther2.ReferenceFieldName = Nothing
        Me.fndOther2.ReferenceTableName = Nothing
        Me.fndOther2.Size = New System.Drawing.Size(107, 19)
        Me.fndOther2.TabIndex = 15
        Me.fndOther2.Value = ""
        '
        'fndOther1
        '
        Me.fndOther1.CalculationExpression = Nothing
        Me.fndOther1.FieldCode = Nothing
        Me.fndOther1.FieldDesc = Nothing
        Me.fndOther1.FieldMaxLength = 0
        Me.fndOther1.FieldName = Nothing
        Me.fndOther1.isCalculatedField = False
        Me.fndOther1.IsSourceFromTable = False
        Me.fndOther1.IsSourceFromValueList = False
        Me.fndOther1.IsUnique = False
        Me.fndOther1.Location = New System.Drawing.Point(605, 96)
        Me.fndOther1.MendatroryField = False
        Me.fndOther1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOther1.MyLinkLable1 = Nothing
        Me.fndOther1.MyLinkLable2 = Nothing
        Me.fndOther1.MyReadOnly = False
        Me.fndOther1.MyShowMasterFormButton = False
        Me.fndOther1.Name = "fndOther1"
        Me.fndOther1.ReferenceFieldDesc = Nothing
        Me.fndOther1.ReferenceFieldName = Nothing
        Me.fndOther1.ReferenceTableName = Nothing
        Me.fndOther1.Size = New System.Drawing.Size(107, 19)
        Me.fndOther1.TabIndex = 14
        Me.fndOther1.Value = ""
        '
        'fndRMCons
        '
        Me.fndRMCons.CalculationExpression = Nothing
        Me.fndRMCons.FieldCode = Nothing
        Me.fndRMCons.FieldDesc = Nothing
        Me.fndRMCons.FieldMaxLength = 0
        Me.fndRMCons.FieldName = Nothing
        Me.fndRMCons.isCalculatedField = False
        Me.fndRMCons.IsSourceFromTable = False
        Me.fndRMCons.IsSourceFromValueList = False
        Me.fndRMCons.IsUnique = False
        Me.fndRMCons.Location = New System.Drawing.Point(605, 76)
        Me.fndRMCons.MendatroryField = False
        Me.fndRMCons.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRMCons.MyLinkLable1 = Nothing
        Me.fndRMCons.MyLinkLable2 = Nothing
        Me.fndRMCons.MyReadOnly = False
        Me.fndRMCons.MyShowMasterFormButton = False
        Me.fndRMCons.Name = "fndRMCons"
        Me.fndRMCons.ReferenceFieldDesc = Nothing
        Me.fndRMCons.ReferenceFieldName = Nothing
        Me.fndRMCons.ReferenceTableName = Nothing
        Me.fndRMCons.Size = New System.Drawing.Size(107, 19)
        Me.fndRMCons.TabIndex = 13
        Me.fndRMCons.Value = ""
        '
        'fndWIPAcc
        '
        Me.fndWIPAcc.CalculationExpression = Nothing
        Me.fndWIPAcc.FieldCode = Nothing
        Me.fndWIPAcc.FieldDesc = Nothing
        Me.fndWIPAcc.FieldMaxLength = 0
        Me.fndWIPAcc.FieldName = Nothing
        Me.fndWIPAcc.isCalculatedField = False
        Me.fndWIPAcc.IsSourceFromTable = False
        Me.fndWIPAcc.IsSourceFromValueList = False
        Me.fndWIPAcc.IsUnique = False
        Me.fndWIPAcc.Location = New System.Drawing.Point(605, 56)
        Me.fndWIPAcc.MendatroryField = False
        Me.fndWIPAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndWIPAcc.MyLinkLable1 = Nothing
        Me.fndWIPAcc.MyLinkLable2 = Nothing
        Me.fndWIPAcc.MyReadOnly = False
        Me.fndWIPAcc.MyShowMasterFormButton = False
        Me.fndWIPAcc.Name = "fndWIPAcc"
        Me.fndWIPAcc.ReferenceFieldDesc = Nothing
        Me.fndWIPAcc.ReferenceFieldName = Nothing
        Me.fndWIPAcc.ReferenceTableName = Nothing
        Me.fndWIPAcc.Size = New System.Drawing.Size(107, 19)
        Me.fndWIPAcc.TabIndex = 12
        Me.fndWIPAcc.Value = ""
        '
        'rdtxtOther2
        '
        Me.rdtxtOther2.Location = New System.Drawing.Point(714, 115)
        Me.rdtxtOther2.Name = "rdtxtOther2"
        Me.rdtxtOther2.ReadOnly = True
        Me.rdtxtOther2.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtOther2.TabIndex = 44
        Me.rdtxtOther2.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(467, 116)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(51, 18)
        Me.MyLabel1.TabIndex = 40
        Me.MyLabel1.Text = "Shortage"
        '
        'rdtxtOther1
        '
        Me.rdtxtOther1.CalculationExpression = Nothing
        Me.rdtxtOther1.FieldCode = Nothing
        Me.rdtxtOther1.FieldDesc = Nothing
        Me.rdtxtOther1.FieldMaxLength = 0
        Me.rdtxtOther1.FieldName = Nothing
        Me.rdtxtOther1.isCalculatedField = False
        Me.rdtxtOther1.IsSourceFromTable = False
        Me.rdtxtOther1.IsSourceFromValueList = False
        Me.rdtxtOther1.IsUnique = False
        Me.rdtxtOther1.Location = New System.Drawing.Point(714, 95)
        Me.rdtxtOther1.MendatroryField = False
        Me.rdtxtOther1.MyLinkLable1 = Nothing
        Me.rdtxtOther1.MyLinkLable2 = Nothing
        Me.rdtxtOther1.Name = "rdtxtOther1"
        Me.rdtxtOther1.ReadOnly = True
        Me.rdtxtOther1.ReferenceFieldDesc = Nothing
        Me.rdtxtOther1.ReferenceFieldName = Nothing
        Me.rdtxtOther1.ReferenceTableName = Nothing
        Me.rdtxtOther1.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtOther1.TabIndex = 45
        Me.rdtxtOther1.TabStop = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(467, 96)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 41
        Me.MyLabel2.Text = "Rejected"
        '
        'rdtxtRMCons
        '
        Me.rdtxtRMCons.CalculationExpression = Nothing
        Me.rdtxtRMCons.FieldCode = Nothing
        Me.rdtxtRMCons.FieldDesc = Nothing
        Me.rdtxtRMCons.FieldMaxLength = 0
        Me.rdtxtRMCons.FieldName = Nothing
        Me.rdtxtRMCons.isCalculatedField = False
        Me.rdtxtRMCons.IsSourceFromTable = False
        Me.rdtxtRMCons.IsSourceFromValueList = False
        Me.rdtxtRMCons.IsUnique = False
        Me.rdtxtRMCons.Location = New System.Drawing.Point(714, 75)
        Me.rdtxtRMCons.MendatroryField = False
        Me.rdtxtRMCons.MyLinkLable1 = Nothing
        Me.rdtxtRMCons.MyLinkLable2 = Nothing
        Me.rdtxtRMCons.Name = "rdtxtRMCons"
        Me.rdtxtRMCons.ReferenceFieldDesc = Nothing
        Me.rdtxtRMCons.ReferenceFieldName = Nothing
        Me.rdtxtRMCons.ReferenceTableName = Nothing
        Me.rdtxtRMCons.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtRMCons.TabIndex = 46
        Me.rdtxtRMCons.TabStop = False
        '
        'rdtxtWIPAcc
        '
        Me.rdtxtWIPAcc.CalculationExpression = Nothing
        Me.rdtxtWIPAcc.FieldCode = Nothing
        Me.rdtxtWIPAcc.FieldDesc = Nothing
        Me.rdtxtWIPAcc.FieldMaxLength = 0
        Me.rdtxtWIPAcc.FieldName = Nothing
        Me.rdtxtWIPAcc.isCalculatedField = False
        Me.rdtxtWIPAcc.IsSourceFromTable = False
        Me.rdtxtWIPAcc.IsSourceFromValueList = False
        Me.rdtxtWIPAcc.IsUnique = False
        Me.rdtxtWIPAcc.Location = New System.Drawing.Point(714, 55)
        Me.rdtxtWIPAcc.MendatroryField = False
        Me.rdtxtWIPAcc.MyLinkLable1 = Nothing
        Me.rdtxtWIPAcc.MyLinkLable2 = Nothing
        Me.rdtxtWIPAcc.Name = "rdtxtWIPAcc"
        Me.rdtxtWIPAcc.ReferenceFieldDesc = Nothing
        Me.rdtxtWIPAcc.ReferenceFieldName = Nothing
        Me.rdtxtWIPAcc.ReferenceTableName = Nothing
        Me.rdtxtWIPAcc.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtWIPAcc.TabIndex = 47
        Me.rdtxtWIPAcc.TabStop = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(467, 76)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel3.TabIndex = 42
        Me.MyLabel3.Text = "RM Consumption"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(467, 56)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel4.TabIndex = 43
        Me.MyLabel4.Text = "WIP Account"
        '
        'fndbreakageglaccount
        '
        Me.fndbreakageglaccount.CalculationExpression = Nothing
        Me.fndbreakageglaccount.FieldCode = Nothing
        Me.fndbreakageglaccount.FieldDesc = Nothing
        Me.fndbreakageglaccount.FieldMaxLength = 0
        Me.fndbreakageglaccount.FieldName = Nothing
        Me.fndbreakageglaccount.isCalculatedField = False
        Me.fndbreakageglaccount.IsSourceFromTable = False
        Me.fndbreakageglaccount.IsSourceFromValueList = False
        Me.fndbreakageglaccount.IsUnique = False
        Me.fndbreakageglaccount.Location = New System.Drawing.Point(605, 36)
        Me.fndbreakageglaccount.MendatroryField = True
        Me.fndbreakageglaccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndbreakageglaccount.MyLinkLable1 = Nothing
        Me.fndbreakageglaccount.MyLinkLable2 = Nothing
        Me.fndbreakageglaccount.MyReadOnly = False
        Me.fndbreakageglaccount.MyShowMasterFormButton = False
        Me.fndbreakageglaccount.Name = "fndbreakageglaccount"
        Me.fndbreakageglaccount.ReferenceFieldDesc = Nothing
        Me.fndbreakageglaccount.ReferenceFieldName = Nothing
        Me.fndbreakageglaccount.ReferenceTableName = Nothing
        Me.fndbreakageglaccount.Size = New System.Drawing.Size(107, 19)
        Me.fndbreakageglaccount.TabIndex = 11
        Me.fndbreakageglaccount.Value = ""
        '
        'fndReserveStock
        '
        Me.fndReserveStock.CalculationExpression = Nothing
        Me.fndReserveStock.FieldCode = Nothing
        Me.fndReserveStock.FieldDesc = Nothing
        Me.fndReserveStock.FieldMaxLength = 0
        Me.fndReserveStock.FieldName = Nothing
        Me.fndReserveStock.isCalculatedField = False
        Me.fndReserveStock.IsSourceFromTable = False
        Me.fndReserveStock.IsSourceFromValueList = False
        Me.fndReserveStock.IsUnique = False
        Me.fndReserveStock.Location = New System.Drawing.Point(605, 16)
        Me.fndReserveStock.MendatroryField = True
        Me.fndReserveStock.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndReserveStock.MyLinkLable1 = Nothing
        Me.fndReserveStock.MyLinkLable2 = Nothing
        Me.fndReserveStock.MyReadOnly = False
        Me.fndReserveStock.MyShowMasterFormButton = False
        Me.fndReserveStock.Name = "fndReserveStock"
        Me.fndReserveStock.ReferenceFieldDesc = Nothing
        Me.fndReserveStock.ReferenceFieldName = Nothing
        Me.fndReserveStock.ReferenceTableName = Nothing
        Me.fndReserveStock.Size = New System.Drawing.Size(107, 19)
        Me.fndReserveStock.TabIndex = 10
        Me.fndReserveStock.Value = ""
        '
        'fndcreditdebitnoteclr
        '
        Me.fndcreditdebitnoteclr.CalculationExpression = Nothing
        Me.fndcreditdebitnoteclr.FieldCode = Nothing
        Me.fndcreditdebitnoteclr.FieldDesc = Nothing
        Me.fndcreditdebitnoteclr.FieldMaxLength = 0
        Me.fndcreditdebitnoteclr.FieldName = Nothing
        Me.fndcreditdebitnoteclr.isCalculatedField = False
        Me.fndcreditdebitnoteclr.IsSourceFromTable = False
        Me.fndcreditdebitnoteclr.IsSourceFromValueList = False
        Me.fndcreditdebitnoteclr.IsUnique = False
        Me.fndcreditdebitnoteclr.Location = New System.Drawing.Point(130, 196)
        Me.fndcreditdebitnoteclr.MendatroryField = True
        Me.fndcreditdebitnoteclr.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcreditdebitnoteclr.MyLinkLable1 = Nothing
        Me.fndcreditdebitnoteclr.MyLinkLable2 = Nothing
        Me.fndcreditdebitnoteclr.MyReadOnly = False
        Me.fndcreditdebitnoteclr.MyShowMasterFormButton = False
        Me.fndcreditdebitnoteclr.Name = "fndcreditdebitnoteclr"
        Me.fndcreditdebitnoteclr.ReferenceFieldDesc = Nothing
        Me.fndcreditdebitnoteclr.ReferenceFieldName = Nothing
        Me.fndcreditdebitnoteclr.ReferenceTableName = Nothing
        Me.fndcreditdebitnoteclr.Size = New System.Drawing.Size(107, 19)
        Me.fndcreditdebitnoteclr.TabIndex = 9
        Me.fndcreditdebitnoteclr.Value = ""
        '
        'fndphysicalinventrycontrol
        '
        Me.fndphysicalinventrycontrol.CalculationExpression = Nothing
        Me.fndphysicalinventrycontrol.FieldCode = Nothing
        Me.fndphysicalinventrycontrol.FieldDesc = Nothing
        Me.fndphysicalinventrycontrol.FieldMaxLength = 0
        Me.fndphysicalinventrycontrol.FieldName = Nothing
        Me.fndphysicalinventrycontrol.isCalculatedField = False
        Me.fndphysicalinventrycontrol.IsSourceFromTable = False
        Me.fndphysicalinventrycontrol.IsSourceFromValueList = False
        Me.fndphysicalinventrycontrol.IsUnique = False
        Me.fndphysicalinventrycontrol.Location = New System.Drawing.Point(130, 176)
        Me.fndphysicalinventrycontrol.MendatroryField = True
        Me.fndphysicalinventrycontrol.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndphysicalinventrycontrol.MyLinkLable1 = Nothing
        Me.fndphysicalinventrycontrol.MyLinkLable2 = Nothing
        Me.fndphysicalinventrycontrol.MyReadOnly = False
        Me.fndphysicalinventrycontrol.MyShowMasterFormButton = False
        Me.fndphysicalinventrycontrol.Name = "fndphysicalinventrycontrol"
        Me.fndphysicalinventrycontrol.ReferenceFieldDesc = Nothing
        Me.fndphysicalinventrycontrol.ReferenceFieldName = Nothing
        Me.fndphysicalinventrycontrol.ReferenceTableName = Nothing
        Me.fndphysicalinventrycontrol.Size = New System.Drawing.Size(107, 19)
        Me.fndphysicalinventrycontrol.TabIndex = 8
        Me.fndphysicalinventrycontrol.Value = ""
        '
        'fnddisassamblyexpense
        '
        Me.fnddisassamblyexpense.CalculationExpression = Nothing
        Me.fnddisassamblyexpense.FieldCode = Nothing
        Me.fnddisassamblyexpense.FieldDesc = Nothing
        Me.fnddisassamblyexpense.FieldMaxLength = 0
        Me.fnddisassamblyexpense.FieldName = Nothing
        Me.fnddisassamblyexpense.isCalculatedField = False
        Me.fnddisassamblyexpense.IsSourceFromTable = False
        Me.fnddisassamblyexpense.IsSourceFromValueList = False
        Me.fnddisassamblyexpense.IsUnique = False
        Me.fnddisassamblyexpense.Location = New System.Drawing.Point(130, 156)
        Me.fnddisassamblyexpense.MendatroryField = True
        Me.fnddisassamblyexpense.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnddisassamblyexpense.MyLinkLable1 = Nothing
        Me.fnddisassamblyexpense.MyLinkLable2 = Nothing
        Me.fnddisassamblyexpense.MyReadOnly = False
        Me.fnddisassamblyexpense.MyShowMasterFormButton = False
        Me.fnddisassamblyexpense.Name = "fnddisassamblyexpense"
        Me.fnddisassamblyexpense.ReferenceFieldDesc = Nothing
        Me.fnddisassamblyexpense.ReferenceFieldName = Nothing
        Me.fnddisassamblyexpense.ReferenceTableName = Nothing
        Me.fnddisassamblyexpense.Size = New System.Drawing.Size(107, 19)
        Me.fnddisassamblyexpense.TabIndex = 7
        Me.fnddisassamblyexpense.Value = ""
        '
        'fndshipmentclearing
        '
        Me.fndshipmentclearing.CalculationExpression = Nothing
        Me.fndshipmentclearing.FieldCode = Nothing
        Me.fndshipmentclearing.FieldDesc = Nothing
        Me.fndshipmentclearing.FieldMaxLength = 0
        Me.fndshipmentclearing.FieldName = Nothing
        Me.fndshipmentclearing.isCalculatedField = False
        Me.fndshipmentclearing.IsSourceFromTable = False
        Me.fndshipmentclearing.IsSourceFromValueList = False
        Me.fndshipmentclearing.IsUnique = False
        Me.fndshipmentclearing.Location = New System.Drawing.Point(130, 136)
        Me.fndshipmentclearing.MendatroryField = True
        Me.fndshipmentclearing.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndshipmentclearing.MyLinkLable1 = Nothing
        Me.fndshipmentclearing.MyLinkLable2 = Nothing
        Me.fndshipmentclearing.MyReadOnly = False
        Me.fndshipmentclearing.MyShowMasterFormButton = False
        Me.fndshipmentclearing.Name = "fndshipmentclearing"
        Me.fndshipmentclearing.ReferenceFieldDesc = Nothing
        Me.fndshipmentclearing.ReferenceFieldName = Nothing
        Me.fndshipmentclearing.ReferenceTableName = Nothing
        Me.fndshipmentclearing.Size = New System.Drawing.Size(107, 19)
        Me.fndshipmentclearing.TabIndex = 6
        Me.fndshipmentclearing.Value = ""
        '
        'fndtransferclearing
        '
        Me.fndtransferclearing.CalculationExpression = Nothing
        Me.fndtransferclearing.FieldCode = Nothing
        Me.fndtransferclearing.FieldDesc = Nothing
        Me.fndtransferclearing.FieldMaxLength = 0
        Me.fndtransferclearing.FieldName = Nothing
        Me.fndtransferclearing.isCalculatedField = False
        Me.fndtransferclearing.IsSourceFromTable = False
        Me.fndtransferclearing.IsSourceFromValueList = False
        Me.fndtransferclearing.IsUnique = False
        Me.fndtransferclearing.Location = New System.Drawing.Point(130, 116)
        Me.fndtransferclearing.MendatroryField = True
        Me.fndtransferclearing.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndtransferclearing.MyLinkLable1 = Nothing
        Me.fndtransferclearing.MyLinkLable2 = Nothing
        Me.fndtransferclearing.MyReadOnly = False
        Me.fndtransferclearing.MyShowMasterFormButton = False
        Me.fndtransferclearing.Name = "fndtransferclearing"
        Me.fndtransferclearing.ReferenceFieldDesc = Nothing
        Me.fndtransferclearing.ReferenceFieldName = Nothing
        Me.fndtransferclearing.ReferenceTableName = Nothing
        Me.fndtransferclearing.Size = New System.Drawing.Size(107, 19)
        Me.fndtransferclearing.TabIndex = 5
        Me.fndtransferclearing.Value = ""
        '
        'fndnonstockclearing
        '
        Me.fndnonstockclearing.CalculationExpression = Nothing
        Me.fndnonstockclearing.FieldCode = Nothing
        Me.fndnonstockclearing.FieldDesc = Nothing
        Me.fndnonstockclearing.FieldMaxLength = 0
        Me.fndnonstockclearing.FieldName = Nothing
        Me.fndnonstockclearing.isCalculatedField = False
        Me.fndnonstockclearing.IsSourceFromTable = False
        Me.fndnonstockclearing.IsSourceFromValueList = False
        Me.fndnonstockclearing.IsUnique = False
        Me.fndnonstockclearing.Location = New System.Drawing.Point(130, 96)
        Me.fndnonstockclearing.MendatroryField = True
        Me.fndnonstockclearing.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndnonstockclearing.MyLinkLable1 = Nothing
        Me.fndnonstockclearing.MyLinkLable2 = Nothing
        Me.fndnonstockclearing.MyReadOnly = False
        Me.fndnonstockclearing.MyShowMasterFormButton = False
        Me.fndnonstockclearing.Name = "fndnonstockclearing"
        Me.fndnonstockclearing.ReferenceFieldDesc = Nothing
        Me.fndnonstockclearing.ReferenceFieldName = Nothing
        Me.fndnonstockclearing.ReferenceTableName = Nothing
        Me.fndnonstockclearing.Size = New System.Drawing.Size(107, 19)
        Me.fndnonstockclearing.TabIndex = 4
        Me.fndnonstockclearing.Value = ""
        '
        'fndassamblycostoff
        '
        Me.fndassamblycostoff.CalculationExpression = Nothing
        Me.fndassamblycostoff.FieldCode = Nothing
        Me.fndassamblycostoff.FieldDesc = Nothing
        Me.fndassamblycostoff.FieldMaxLength = 0
        Me.fndassamblycostoff.FieldName = Nothing
        Me.fndassamblycostoff.isCalculatedField = False
        Me.fndassamblycostoff.IsSourceFromTable = False
        Me.fndassamblycostoff.IsSourceFromValueList = False
        Me.fndassamblycostoff.IsUnique = False
        Me.fndassamblycostoff.Location = New System.Drawing.Point(130, 76)
        Me.fndassamblycostoff.MendatroryField = True
        Me.fndassamblycostoff.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndassamblycostoff.MyLinkLable1 = Nothing
        Me.fndassamblycostoff.MyLinkLable2 = Nothing
        Me.fndassamblycostoff.MyReadOnly = False
        Me.fndassamblycostoff.MyShowMasterFormButton = False
        Me.fndassamblycostoff.Name = "fndassamblycostoff"
        Me.fndassamblycostoff.ReferenceFieldDesc = Nothing
        Me.fndassamblycostoff.ReferenceFieldName = Nothing
        Me.fndassamblycostoff.ReferenceTableName = Nothing
        Me.fndassamblycostoff.Size = New System.Drawing.Size(107, 19)
        Me.fndassamblycostoff.TabIndex = 3
        Me.fndassamblycostoff.Value = ""
        '
        'fndadjustmentwriteoff
        '
        Me.fndadjustmentwriteoff.CalculationExpression = Nothing
        Me.fndadjustmentwriteoff.FieldCode = Nothing
        Me.fndadjustmentwriteoff.FieldDesc = Nothing
        Me.fndadjustmentwriteoff.FieldMaxLength = 0
        Me.fndadjustmentwriteoff.FieldName = Nothing
        Me.fndadjustmentwriteoff.isCalculatedField = False
        Me.fndadjustmentwriteoff.IsSourceFromTable = False
        Me.fndadjustmentwriteoff.IsSourceFromValueList = False
        Me.fndadjustmentwriteoff.IsUnique = False
        Me.fndadjustmentwriteoff.Location = New System.Drawing.Point(130, 56)
        Me.fndadjustmentwriteoff.MendatroryField = True
        Me.fndadjustmentwriteoff.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndadjustmentwriteoff.MyLinkLable1 = Nothing
        Me.fndadjustmentwriteoff.MyLinkLable2 = Nothing
        Me.fndadjustmentwriteoff.MyReadOnly = False
        Me.fndadjustmentwriteoff.MyShowMasterFormButton = False
        Me.fndadjustmentwriteoff.Name = "fndadjustmentwriteoff"
        Me.fndadjustmentwriteoff.ReferenceFieldDesc = Nothing
        Me.fndadjustmentwriteoff.ReferenceFieldName = Nothing
        Me.fndadjustmentwriteoff.ReferenceTableName = Nothing
        Me.fndadjustmentwriteoff.Size = New System.Drawing.Size(107, 19)
        Me.fndadjustmentwriteoff.TabIndex = 2
        Me.fndadjustmentwriteoff.Value = ""
        '
        'fndpayableclearing
        '
        Me.fndpayableclearing.CalculationExpression = Nothing
        Me.fndpayableclearing.FieldCode = Nothing
        Me.fndpayableclearing.FieldDesc = Nothing
        Me.fndpayableclearing.FieldMaxLength = 0
        Me.fndpayableclearing.FieldName = Nothing
        Me.fndpayableclearing.isCalculatedField = False
        Me.fndpayableclearing.IsSourceFromTable = False
        Me.fndpayableclearing.IsSourceFromValueList = False
        Me.fndpayableclearing.IsUnique = False
        Me.fndpayableclearing.Location = New System.Drawing.Point(130, 36)
        Me.fndpayableclearing.MendatroryField = True
        Me.fndpayableclearing.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndpayableclearing.MyLinkLable1 = Nothing
        Me.fndpayableclearing.MyLinkLable2 = Nothing
        Me.fndpayableclearing.MyReadOnly = False
        Me.fndpayableclearing.MyShowMasterFormButton = False
        Me.fndpayableclearing.Name = "fndpayableclearing"
        Me.fndpayableclearing.ReferenceFieldDesc = Nothing
        Me.fndpayableclearing.ReferenceFieldName = Nothing
        Me.fndpayableclearing.ReferenceTableName = Nothing
        Me.fndpayableclearing.Size = New System.Drawing.Size(107, 19)
        Me.fndpayableclearing.TabIndex = 1
        Me.fndpayableclearing.Value = ""
        '
        'fndInventoryControl
        '
        Me.fndInventoryControl.CalculationExpression = Nothing
        Me.fndInventoryControl.FieldCode = Nothing
        Me.fndInventoryControl.FieldDesc = Nothing
        Me.fndInventoryControl.FieldMaxLength = 0
        Me.fndInventoryControl.FieldName = Nothing
        Me.fndInventoryControl.isCalculatedField = False
        Me.fndInventoryControl.IsSourceFromTable = False
        Me.fndInventoryControl.IsSourceFromValueList = False
        Me.fndInventoryControl.IsUnique = False
        Me.fndInventoryControl.Location = New System.Drawing.Point(130, 16)
        Me.fndInventoryControl.MendatroryField = True
        Me.fndInventoryControl.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndInventoryControl.MyLinkLable1 = Nothing
        Me.fndInventoryControl.MyLinkLable2 = Nothing
        Me.fndInventoryControl.MyReadOnly = False
        Me.fndInventoryControl.MyShowMasterFormButton = False
        Me.fndInventoryControl.Name = "fndInventoryControl"
        Me.fndInventoryControl.ReferenceFieldDesc = Nothing
        Me.fndInventoryControl.ReferenceFieldName = Nothing
        Me.fndInventoryControl.ReferenceTableName = Nothing
        Me.fndInventoryControl.Size = New System.Drawing.Size(107, 19)
        Me.fndInventoryControl.TabIndex = 0
        Me.fndInventoryControl.Value = ""
        '
        'txtbreakage
        '
        Me.txtbreakage.Location = New System.Drawing.Point(714, 35)
        Me.txtbreakage.Name = "txtbreakage"
        Me.txtbreakage.ReadOnly = True
        Me.txtbreakage.Size = New System.Drawing.Size(224, 20)
        Me.txtbreakage.TabIndex = 24
        Me.txtbreakage.TabStop = False
        '
        'lblbreakage
        '
        Me.lblbreakage.FieldName = Nothing
        Me.lblbreakage.Location = New System.Drawing.Point(467, 36)
        Me.lblbreakage.Name = "lblbreakage"
        Me.lblbreakage.Size = New System.Drawing.Size(90, 18)
        Me.lblbreakage.TabIndex = 12
        Me.lblbreakage.Text = "Breakage GL A/C"
        '
        'txtReserveStock
        '
        Me.txtReserveStock.CalculationExpression = Nothing
        Me.txtReserveStock.FieldCode = Nothing
        Me.txtReserveStock.FieldDesc = Nothing
        Me.txtReserveStock.FieldMaxLength = 0
        Me.txtReserveStock.FieldName = Nothing
        Me.txtReserveStock.isCalculatedField = False
        Me.txtReserveStock.IsSourceFromTable = False
        Me.txtReserveStock.IsSourceFromValueList = False
        Me.txtReserveStock.IsUnique = False
        Me.txtReserveStock.Location = New System.Drawing.Point(714, 15)
        Me.txtReserveStock.MendatroryField = False
        Me.txtReserveStock.MyLinkLable1 = Nothing
        Me.txtReserveStock.MyLinkLable2 = Nothing
        Me.txtReserveStock.Name = "txtReserveStock"
        Me.txtReserveStock.ReadOnly = True
        Me.txtReserveStock.ReferenceFieldDesc = Nothing
        Me.txtReserveStock.ReferenceFieldName = Nothing
        Me.txtReserveStock.ReferenceTableName = Nothing
        Me.txtReserveStock.Size = New System.Drawing.Size(224, 20)
        Me.txtReserveStock.TabIndex = 25
        Me.txtReserveStock.TabStop = False
        '
        'lblReserveStock
        '
        Me.lblReserveStock.FieldName = Nothing
        Me.lblReserveStock.Location = New System.Drawing.Point(467, 16)
        Me.lblReserveStock.Name = "lblReserveStock"
        Me.lblReserveStock.Size = New System.Drawing.Size(74, 18)
        Me.lblReserveStock.TabIndex = 13
        Me.lblReserveStock.Text = "RGP Clearing "
        '
        'rdtxtcreditdebitnoteclr
        '
        Me.rdtxtcreditdebitnoteclr.CalculationExpression = Nothing
        Me.rdtxtcreditdebitnoteclr.FieldCode = Nothing
        Me.rdtxtcreditdebitnoteclr.FieldDesc = Nothing
        Me.rdtxtcreditdebitnoteclr.FieldMaxLength = 0
        Me.rdtxtcreditdebitnoteclr.FieldName = Nothing
        Me.rdtxtcreditdebitnoteclr.isCalculatedField = False
        Me.rdtxtcreditdebitnoteclr.IsSourceFromTable = False
        Me.rdtxtcreditdebitnoteclr.IsSourceFromValueList = False
        Me.rdtxtcreditdebitnoteclr.IsUnique = False
        Me.rdtxtcreditdebitnoteclr.Location = New System.Drawing.Point(239, 195)
        Me.rdtxtcreditdebitnoteclr.MendatroryField = False
        Me.rdtxtcreditdebitnoteclr.MyLinkLable1 = Nothing
        Me.rdtxtcreditdebitnoteclr.MyLinkLable2 = Nothing
        Me.rdtxtcreditdebitnoteclr.Name = "rdtxtcreditdebitnoteclr"
        Me.rdtxtcreditdebitnoteclr.ReferenceFieldDesc = Nothing
        Me.rdtxtcreditdebitnoteclr.ReferenceFieldName = Nothing
        Me.rdtxtcreditdebitnoteclr.ReferenceTableName = Nothing
        Me.rdtxtcreditdebitnoteclr.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtcreditdebitnoteclr.TabIndex = 26
        Me.rdtxtcreditdebitnoteclr.TabStop = False
        '
        'rdtxtphysicalinventryadj
        '
        Me.rdtxtphysicalinventryadj.CalculationExpression = Nothing
        Me.rdtxtphysicalinventryadj.FieldCode = Nothing
        Me.rdtxtphysicalinventryadj.FieldDesc = Nothing
        Me.rdtxtphysicalinventryadj.FieldMaxLength = 0
        Me.rdtxtphysicalinventryadj.FieldName = Nothing
        Me.rdtxtphysicalinventryadj.isCalculatedField = False
        Me.rdtxtphysicalinventryadj.IsSourceFromTable = False
        Me.rdtxtphysicalinventryadj.IsSourceFromValueList = False
        Me.rdtxtphysicalinventryadj.IsUnique = False
        Me.rdtxtphysicalinventryadj.Location = New System.Drawing.Point(239, 175)
        Me.rdtxtphysicalinventryadj.MendatroryField = False
        Me.rdtxtphysicalinventryadj.MyLinkLable1 = Nothing
        Me.rdtxtphysicalinventryadj.MyLinkLable2 = Nothing
        Me.rdtxtphysicalinventryadj.Name = "rdtxtphysicalinventryadj"
        Me.rdtxtphysicalinventryadj.ReferenceFieldDesc = Nothing
        Me.rdtxtphysicalinventryadj.ReferenceFieldName = Nothing
        Me.rdtxtphysicalinventryadj.ReferenceTableName = Nothing
        Me.rdtxtphysicalinventryadj.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtphysicalinventryadj.TabIndex = 27
        Me.rdtxtphysicalinventryadj.TabStop = False
        '
        'rdtxtdisassamblyexpense
        '
        Me.rdtxtdisassamblyexpense.CalculationExpression = Nothing
        Me.rdtxtdisassamblyexpense.FieldCode = Nothing
        Me.rdtxtdisassamblyexpense.FieldDesc = Nothing
        Me.rdtxtdisassamblyexpense.FieldMaxLength = 0
        Me.rdtxtdisassamblyexpense.FieldName = Nothing
        Me.rdtxtdisassamblyexpense.isCalculatedField = False
        Me.rdtxtdisassamblyexpense.IsSourceFromTable = False
        Me.rdtxtdisassamblyexpense.IsSourceFromValueList = False
        Me.rdtxtdisassamblyexpense.IsUnique = False
        Me.rdtxtdisassamblyexpense.Location = New System.Drawing.Point(239, 155)
        Me.rdtxtdisassamblyexpense.MendatroryField = False
        Me.rdtxtdisassamblyexpense.MyLinkLable1 = Nothing
        Me.rdtxtdisassamblyexpense.MyLinkLable2 = Nothing
        Me.rdtxtdisassamblyexpense.Name = "rdtxtdisassamblyexpense"
        Me.rdtxtdisassamblyexpense.ReferenceFieldDesc = Nothing
        Me.rdtxtdisassamblyexpense.ReferenceFieldName = Nothing
        Me.rdtxtdisassamblyexpense.ReferenceTableName = Nothing
        Me.rdtxtdisassamblyexpense.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtdisassamblyexpense.TabIndex = 28
        Me.rdtxtdisassamblyexpense.TabStop = False
        '
        'rdtxtshipmentexpense
        '
        Me.rdtxtshipmentexpense.CalculationExpression = Nothing
        Me.rdtxtshipmentexpense.FieldCode = Nothing
        Me.rdtxtshipmentexpense.FieldDesc = Nothing
        Me.rdtxtshipmentexpense.FieldMaxLength = 0
        Me.rdtxtshipmentexpense.FieldName = Nothing
        Me.rdtxtshipmentexpense.isCalculatedField = False
        Me.rdtxtshipmentexpense.IsSourceFromTable = False
        Me.rdtxtshipmentexpense.IsSourceFromValueList = False
        Me.rdtxtshipmentexpense.IsUnique = False
        Me.rdtxtshipmentexpense.Location = New System.Drawing.Point(239, 135)
        Me.rdtxtshipmentexpense.MendatroryField = False
        Me.rdtxtshipmentexpense.MyLinkLable1 = Nothing
        Me.rdtxtshipmentexpense.MyLinkLable2 = Nothing
        Me.rdtxtshipmentexpense.Name = "rdtxtshipmentexpense"
        Me.rdtxtshipmentexpense.ReferenceFieldDesc = Nothing
        Me.rdtxtshipmentexpense.ReferenceFieldName = Nothing
        Me.rdtxtshipmentexpense.ReferenceTableName = Nothing
        Me.rdtxtshipmentexpense.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtshipmentexpense.TabIndex = 29
        Me.rdtxtshipmentexpense.TabStop = False
        '
        'rdtxttransferclearing
        '
        Me.rdtxttransferclearing.CalculationExpression = Nothing
        Me.rdtxttransferclearing.FieldCode = Nothing
        Me.rdtxttransferclearing.FieldDesc = Nothing
        Me.rdtxttransferclearing.FieldMaxLength = 0
        Me.rdtxttransferclearing.FieldName = Nothing
        Me.rdtxttransferclearing.isCalculatedField = False
        Me.rdtxttransferclearing.IsSourceFromTable = False
        Me.rdtxttransferclearing.IsSourceFromValueList = False
        Me.rdtxttransferclearing.IsUnique = False
        Me.rdtxttransferclearing.Location = New System.Drawing.Point(239, 115)
        Me.rdtxttransferclearing.MendatroryField = False
        Me.rdtxttransferclearing.MyLinkLable1 = Nothing
        Me.rdtxttransferclearing.MyLinkLable2 = Nothing
        Me.rdtxttransferclearing.Name = "rdtxttransferclearing"
        Me.rdtxttransferclearing.ReferenceFieldDesc = Nothing
        Me.rdtxttransferclearing.ReferenceFieldName = Nothing
        Me.rdtxttransferclearing.ReferenceTableName = Nothing
        Me.rdtxttransferclearing.Size = New System.Drawing.Size(224, 20)
        Me.rdtxttransferclearing.TabIndex = 30
        Me.rdtxttransferclearing.TabStop = False
        '
        'rdtxtnonstockclearing
        '
        Me.rdtxtnonstockclearing.CalculationExpression = Nothing
        Me.rdtxtnonstockclearing.FieldCode = Nothing
        Me.rdtxtnonstockclearing.FieldDesc = Nothing
        Me.rdtxtnonstockclearing.FieldMaxLength = 0
        Me.rdtxtnonstockclearing.FieldName = Nothing
        Me.rdtxtnonstockclearing.isCalculatedField = False
        Me.rdtxtnonstockclearing.IsSourceFromTable = False
        Me.rdtxtnonstockclearing.IsSourceFromValueList = False
        Me.rdtxtnonstockclearing.IsUnique = False
        Me.rdtxtnonstockclearing.Location = New System.Drawing.Point(239, 95)
        Me.rdtxtnonstockclearing.MendatroryField = False
        Me.rdtxtnonstockclearing.MyLinkLable1 = Nothing
        Me.rdtxtnonstockclearing.MyLinkLable2 = Nothing
        Me.rdtxtnonstockclearing.Name = "rdtxtnonstockclearing"
        Me.rdtxtnonstockclearing.ReferenceFieldDesc = Nothing
        Me.rdtxtnonstockclearing.ReferenceFieldName = Nothing
        Me.rdtxtnonstockclearing.ReferenceTableName = Nothing
        Me.rdtxtnonstockclearing.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtnonstockclearing.TabIndex = 31
        Me.rdtxtnonstockclearing.TabStop = False
        '
        'rdtxtassamblycostcredit
        '
        Me.rdtxtassamblycostcredit.CalculationExpression = Nothing
        Me.rdtxtassamblycostcredit.FieldCode = Nothing
        Me.rdtxtassamblycostcredit.FieldDesc = Nothing
        Me.rdtxtassamblycostcredit.FieldMaxLength = 0
        Me.rdtxtassamblycostcredit.FieldName = Nothing
        Me.rdtxtassamblycostcredit.isCalculatedField = False
        Me.rdtxtassamblycostcredit.IsSourceFromTable = False
        Me.rdtxtassamblycostcredit.IsSourceFromValueList = False
        Me.rdtxtassamblycostcredit.IsUnique = False
        Me.rdtxtassamblycostcredit.Location = New System.Drawing.Point(239, 75)
        Me.rdtxtassamblycostcredit.MendatroryField = False
        Me.rdtxtassamblycostcredit.MyLinkLable1 = Nothing
        Me.rdtxtassamblycostcredit.MyLinkLable2 = Nothing
        Me.rdtxtassamblycostcredit.Name = "rdtxtassamblycostcredit"
        Me.rdtxtassamblycostcredit.ReferenceFieldDesc = Nothing
        Me.rdtxtassamblycostcredit.ReferenceFieldName = Nothing
        Me.rdtxtassamblycostcredit.ReferenceTableName = Nothing
        Me.rdtxtassamblycostcredit.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtassamblycostcredit.TabIndex = 32
        Me.rdtxtassamblycostcredit.TabStop = False
        '
        'rdtxtadjustmentwriteoff
        '
        Me.rdtxtadjustmentwriteoff.CalculationExpression = Nothing
        Me.rdtxtadjustmentwriteoff.FieldCode = Nothing
        Me.rdtxtadjustmentwriteoff.FieldDesc = Nothing
        Me.rdtxtadjustmentwriteoff.FieldMaxLength = 0
        Me.rdtxtadjustmentwriteoff.FieldName = Nothing
        Me.rdtxtadjustmentwriteoff.isCalculatedField = False
        Me.rdtxtadjustmentwriteoff.IsSourceFromTable = False
        Me.rdtxtadjustmentwriteoff.IsSourceFromValueList = False
        Me.rdtxtadjustmentwriteoff.IsUnique = False
        Me.rdtxtadjustmentwriteoff.Location = New System.Drawing.Point(239, 55)
        Me.rdtxtadjustmentwriteoff.MendatroryField = False
        Me.rdtxtadjustmentwriteoff.MyLinkLable1 = Nothing
        Me.rdtxtadjustmentwriteoff.MyLinkLable2 = Nothing
        Me.rdtxtadjustmentwriteoff.Name = "rdtxtadjustmentwriteoff"
        Me.rdtxtadjustmentwriteoff.ReferenceFieldDesc = Nothing
        Me.rdtxtadjustmentwriteoff.ReferenceFieldName = Nothing
        Me.rdtxtadjustmentwriteoff.ReferenceTableName = Nothing
        Me.rdtxtadjustmentwriteoff.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtadjustmentwriteoff.TabIndex = 33
        Me.rdtxtadjustmentwriteoff.TabStop = False
        '
        'rdtxtpayableclearing
        '
        Me.rdtxtpayableclearing.CalculationExpression = Nothing
        Me.rdtxtpayableclearing.FieldCode = Nothing
        Me.rdtxtpayableclearing.FieldDesc = Nothing
        Me.rdtxtpayableclearing.FieldMaxLength = 0
        Me.rdtxtpayableclearing.FieldName = Nothing
        Me.rdtxtpayableclearing.isCalculatedField = False
        Me.rdtxtpayableclearing.IsSourceFromTable = False
        Me.rdtxtpayableclearing.IsSourceFromValueList = False
        Me.rdtxtpayableclearing.IsUnique = False
        Me.rdtxtpayableclearing.Location = New System.Drawing.Point(239, 35)
        Me.rdtxtpayableclearing.MendatroryField = False
        Me.rdtxtpayableclearing.MyLinkLable1 = Nothing
        Me.rdtxtpayableclearing.MyLinkLable2 = Nothing
        Me.rdtxtpayableclearing.Name = "rdtxtpayableclearing"
        Me.rdtxtpayableclearing.ReferenceFieldDesc = Nothing
        Me.rdtxtpayableclearing.ReferenceFieldName = Nothing
        Me.rdtxtpayableclearing.ReferenceTableName = Nothing
        Me.rdtxtpayableclearing.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtpayableclearing.TabIndex = 34
        Me.rdtxtpayableclearing.TabStop = False
        '
        'rdtxtinventrycontrol
        '
        Me.rdtxtinventrycontrol.CalculationExpression = Nothing
        Me.rdtxtinventrycontrol.FieldCode = Nothing
        Me.rdtxtinventrycontrol.FieldDesc = Nothing
        Me.rdtxtinventrycontrol.FieldMaxLength = 0
        Me.rdtxtinventrycontrol.FieldName = Nothing
        Me.rdtxtinventrycontrol.isCalculatedField = False
        Me.rdtxtinventrycontrol.IsSourceFromTable = False
        Me.rdtxtinventrycontrol.IsSourceFromValueList = False
        Me.rdtxtinventrycontrol.IsUnique = False
        Me.rdtxtinventrycontrol.Location = New System.Drawing.Point(239, 15)
        Me.rdtxtinventrycontrol.MendatroryField = False
        Me.rdtxtinventrycontrol.MyLinkLable1 = Nothing
        Me.rdtxtinventrycontrol.MyLinkLable2 = Nothing
        Me.rdtxtinventrycontrol.Name = "rdtxtinventrycontrol"
        Me.rdtxtinventrycontrol.ReferenceFieldDesc = Nothing
        Me.rdtxtinventrycontrol.ReferenceFieldName = Nothing
        Me.rdtxtinventrycontrol.ReferenceTableName = Nothing
        Me.rdtxtinventrycontrol.Size = New System.Drawing.Size(224, 20)
        Me.rdtxtinventrycontrol.TabIndex = 35
        Me.rdtxtinventrycontrol.TabStop = False
        '
        'rdlblcreditdebitnoteclr
        '
        Me.rdlblcreditdebitnoteclr.FieldName = Nothing
        Me.rdlblcreditdebitnoteclr.Location = New System.Drawing.Point(5, 196)
        Me.rdlblcreditdebitnoteclr.Name = "rdlblcreditdebitnoteclr"
        Me.rdlblcreditdebitnoteclr.Size = New System.Drawing.Size(115, 18)
        Me.rdlblcreditdebitnoteclr.TabIndex = 14
        Me.rdlblcreditdebitnoteclr.Text = "Credit/Debit Note Clr."
        '
        'rdlblphysicalinventryadj
        '
        Me.rdlblphysicalinventryadj.FieldName = Nothing
        Me.rdlblphysicalinventryadj.Location = New System.Drawing.Point(5, 176)
        Me.rdlblphysicalinventryadj.Name = "rdlblphysicalinventryadj"
        Me.rdlblphysicalinventryadj.Size = New System.Drawing.Size(120, 18)
        Me.rdlblphysicalinventryadj.TabIndex = 15
        Me.rdlblphysicalinventryadj.Text = "Physical Inventory Adj-"
        '
        'rdlbldisassemblyexpenxe
        '
        Me.rdlbldisassemblyexpenxe.FieldName = Nothing
        Me.rdlbldisassemblyexpenxe.Location = New System.Drawing.Point(5, 156)
        Me.rdlbldisassemblyexpenxe.Name = "rdlbldisassemblyexpenxe"
        Me.rdlbldisassemblyexpenxe.Size = New System.Drawing.Size(111, 18)
        Me.rdlbldisassemblyexpenxe.TabIndex = 16
        Me.rdlbldisassemblyexpenxe.Text = "Disassembly Expense"
        '
        'rdlblshipmentclearing
        '
        Me.rdlblshipmentclearing.FieldName = Nothing
        Me.rdlblshipmentclearing.Location = New System.Drawing.Point(5, 136)
        Me.rdlblshipmentclearing.Name = "rdlblshipmentclearing"
        Me.rdlblshipmentclearing.Size = New System.Drawing.Size(98, 18)
        Me.rdlblshipmentclearing.TabIndex = 17
        Me.rdlblshipmentclearing.Text = "Shipment Clearing"
        '
        'rdlbltransferclearing
        '
        Me.rdlbltransferclearing.FieldName = Nothing
        Me.rdlbltransferclearing.Location = New System.Drawing.Point(5, 116)
        Me.rdlbltransferclearing.Name = "rdlbltransferclearing"
        Me.rdlbltransferclearing.Size = New System.Drawing.Size(91, 18)
        Me.rdlbltransferclearing.TabIndex = 18
        Me.rdlbltransferclearing.Text = "Transfer Clearing"
        '
        'rdlblnonstockclearing
        '
        Me.rdlblnonstockclearing.FieldName = Nothing
        Me.rdlblnonstockclearing.Location = New System.Drawing.Point(5, 96)
        Me.rdlblnonstockclearing.Name = "rdlblnonstockclearing"
        Me.rdlblnonstockclearing.Size = New System.Drawing.Size(102, 18)
        Me.rdlblnonstockclearing.TabIndex = 19
        Me.rdlblnonstockclearing.Text = "BMC Milk Purchase"
        '
        'rdlblassamblycostcredit
        '
        Me.rdlblassamblycostcredit.FieldName = Nothing
        Me.rdlblassamblycostcredit.Location = New System.Drawing.Point(5, 76)
        Me.rdlblassamblycostcredit.Name = "rdlblassamblycostcredit"
        Me.rdlblassamblycostcredit.Size = New System.Drawing.Size(112, 18)
        Me.rdlblassamblycostcredit.TabIndex = 20
        Me.rdlblassamblycostcredit.Text = "FG Shortage Account"
        '
        'rdlbladjustmentwriteoff
        '
        Me.rdlbladjustmentwriteoff.FieldName = Nothing
        Me.rdlbladjustmentwriteoff.Location = New System.Drawing.Point(5, 56)
        Me.rdlbladjustmentwriteoff.Name = "rdlbladjustmentwriteoff"
        Me.rdlbladjustmentwriteoff.Size = New System.Drawing.Size(114, 18)
        Me.rdlbladjustmentwriteoff.TabIndex = 21
        Me.rdlbladjustmentwriteoff.Text = "Adjustment Write-Off"
        '
        'rdlblpayableclearing
        '
        Me.rdlblpayableclearing.FieldName = Nothing
        Me.rdlblpayableclearing.Location = New System.Drawing.Point(5, 36)
        Me.rdlblpayableclearing.Name = "rdlblpayableclearing"
        Me.rdlblpayableclearing.Size = New System.Drawing.Size(94, 18)
        Me.rdlblpayableclearing.TabIndex = 22
        Me.rdlblpayableclearing.Text = "Payables Clearing"
        '
        'rdlblinventorycontrol
        '
        Me.rdlblinventorycontrol.FieldName = Nothing
        Me.rdlblinventorycontrol.Location = New System.Drawing.Point(5, 16)
        Me.rdlblinventorycontrol.Name = "rdlblinventorycontrol"
        Me.rdlblinventorycontrol.Size = New System.Drawing.Size(94, 18)
        Me.rdlblinventorycontrol.TabIndex = 23
        Me.rdlblinventorycontrol.Text = "Inventory Control"
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnnew.Location = New System.Drawing.Point(343, 17)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(19, 20)
        Me.rdbtnnew.TabIndex = 1
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(148, 9)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "&History"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(6, 8)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(886, 8)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(76, 8)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.radmenu})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(960, 20)
        Me.rdmenufile.TabIndex = 1
        '
        'radmenu
        '
        Me.radmenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.radmenu.Name = "radmenu"
        Me.radmenu.Text = "File"
        '
        'menuimport
        '
        Me.menuimport.Name = "menuimport"
        Me.menuimport.Text = "Import"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "RadMenuItem3"
        Me.rdmenuexport.AccessibleName = "RadMenuItem3"
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'Bsws1
        '
        Me.Bsws1.Credentials = Nothing
        Me.Bsws1.Url = "http://www.businesssms.co.in/WebService/BSWS.asmx"
        Me.Bsws1.UseDefaultCredentials = False
        '
        'frmPurcahseAccountSetCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 498)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPurcahseAccountSetCode"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Purcahse Account  Set"
        CType(Me.rdtxtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rdgpbxpurchaseaccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxpurchaseaccountset.ResumeLayout(False)
        Me.rdgpbxpurchaseaccountset.PerformLayout()
        CType(Me.chk_indentrequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblaccountsetdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCostingMethod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblaccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxgeneralledgeraccounts.ResumeLayout(False)
        Me.rdgrpbxgeneralledgeraccounts.PerformLayout()
        CType(Me.lblItemOpeningClearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWrekageAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchaseLoss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFaAccountDes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStoreConsumtion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStockTransferJobWorkDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEMP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEMP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHandlingCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHandlingCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDifferenceAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPurchaseJobwork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFreightCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChiilingCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProvisioinClearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStockTransferIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtStockTransferAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblJobwork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransferGainLossDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchaseCtrlAcDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLossAc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLossAc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtOther2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtOther1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtRMCons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtWIPAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbreakage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbreakage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReserveStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReserveStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtcreditdebitnoteclr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtphysicalinventryadj, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtdisassamblyexpense, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtshipmentexpense, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxttransferclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtnonstockclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtassamblycostcredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtadjustmentwriteoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtpayableclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtinventrycontrol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblcreditdebitnoteclr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblphysicalinventryadj, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldisassemblyexpenxe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblshipmentclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltransferclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblnonstockclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblassamblycostcredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbladjustmentwriteoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblpayableclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblinventorycontrol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents radmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdgpbxpurchaseaccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cboCostingMethod As common.Controls.MyComboBox
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents rdlblaccountsetcode As common.Controls.MyLabel
    Friend WithEvents rdgrpbxgeneralledgeraccounts As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndReserveStock As common.UserControls.txtFinder
    Friend WithEvents fndcreditdebitnoteclr As common.UserControls.txtFinder
    Friend WithEvents fndphysicalinventrycontrol As common.UserControls.txtFinder
    Friend WithEvents fnddisassamblyexpense As common.UserControls.txtFinder
    Friend WithEvents fndshipmentclearing As common.UserControls.txtFinder
    Friend WithEvents fndtransferclearing As common.UserControls.txtFinder
    Friend WithEvents fndnonstockclearing As common.UserControls.txtFinder
    Friend WithEvents fndassamblycostoff As common.UserControls.txtFinder
    Friend WithEvents fndadjustmentwriteoff As common.UserControls.txtFinder
    Friend WithEvents fndpayableclearing As common.UserControls.txtFinder
    Friend WithEvents fndInventoryControl As common.UserControls.txtFinder
    Friend WithEvents txtbreakage As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblbreakage As common.Controls.MyLabel
    Friend WithEvents txtReserveStock As common.Controls.MyTextBox
    Friend WithEvents lblReserveStock As common.Controls.MyLabel
    Friend WithEvents rdtxtcreditdebitnoteclr As common.Controls.MyTextBox
    Friend WithEvents rdtxtphysicalinventryadj As common.Controls.MyTextBox
    Friend WithEvents rdtxtdisassamblyexpense As common.Controls.MyTextBox
    Friend WithEvents rdtxtshipmentexpense As common.Controls.MyTextBox
    Friend WithEvents rdtxttransferclearing As common.Controls.MyTextBox
    Friend WithEvents rdtxtnonstockclearing As common.Controls.MyTextBox
    Friend WithEvents rdtxtassamblycostcredit As common.Controls.MyTextBox
    Friend WithEvents rdtxtadjustmentwriteoff As common.Controls.MyTextBox
    Friend WithEvents rdtxtpayableclearing As common.Controls.MyTextBox
    Friend WithEvents rdtxtinventrycontrol As common.Controls.MyTextBox
    Friend WithEvents rdlblcreditdebitnoteclr As common.Controls.MyLabel
    Friend WithEvents rdlblphysicalinventryadj As common.Controls.MyLabel
    Friend WithEvents rdlbldisassemblyexpenxe As common.Controls.MyLabel
    Friend WithEvents rdlblshipmentclearing As common.Controls.MyLabel
    Friend WithEvents rdlbltransferclearing As common.Controls.MyLabel
    Friend WithEvents rdlblnonstockclearing As common.Controls.MyLabel
    Friend WithEvents rdlblassamblycostcredit As common.Controls.MyLabel
    Friend WithEvents rdlbladjustmentwriteoff As common.Controls.MyLabel
    Friend WithEvents rdlblpayableclearing As common.Controls.MyLabel
    Friend WithEvents rdlblinventorycontrol As common.Controls.MyLabel
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdtxtdescription As common.Controls.MyTextBox
    Friend WithEvents fndbreakageglaccount As common.UserControls.txtFinder
    Friend WithEvents lblaccountsetdesc As common.Controls.MyTextBox
    Friend WithEvents fndOther2 As common.UserControls.txtFinder
    Friend WithEvents fndOther1 As common.UserControls.txtFinder
    Friend WithEvents fndRMCons As common.UserControls.txtFinder
    Friend WithEvents fndWIPAcc As common.UserControls.txtFinder
    Friend WithEvents rdtxtOther2 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents rdtxtOther1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents rdtxtRMCons As common.Controls.MyTextBox
    Friend WithEvents rdtxtWIPAcc As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtLossAc As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents fndLossAc As common.UserControls.txtFinder
    Friend WithEvents lblLossAc As common.Controls.MyLabel
    Friend WithEvents txtPurchaseCTRL_Ac As common.UserControls.txtFinder
    Friend WithEvents txtPurchaseCtrlAcDesc As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndTransferGainLoss As common.UserControls.txtFinder
    Friend WithEvents txtTransferGainLossDesc As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents FndJobWork As common.UserControls.txtFinder
    Friend WithEvents LblJobwork As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtStockTransferIn As common.UserControls.txtFinder
    Friend WithEvents lblStockTransferIn As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents TxtStockTransferAccount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents FndStockTransferAccount As common.UserControls.txtFinder
    Friend WithEvents txtProvisionClearing As common.UserControls.txtFinder
    Friend WithEvents lblProvisioinClearing As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtFreightCharges As common.UserControls.txtFinder
    Friend WithEvents txtChiilingCharges As common.UserControls.txtFinder
    Friend WithEvents lblFreightCharges As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblChiilingCharges As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents chk_indentrequired As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtPurchaseJobWork As common.UserControls.txtFinder
    Friend WithEvents lblPurchaseJobwork As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtDifferenceAccount As common.UserControls.txtFinder
    Friend WithEvents lblDifferenceAccount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents fndEMP As common.UserControls.txtFinder
    Friend WithEvents fndHandlingCharge As common.UserControls.txtFinder
    Friend WithEvents lblEMP As common.Controls.MyLabel
    Friend WithEvents txtEMP As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtHandlingCharge As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblHandlingCharge As common.Controls.MyLabel
    Friend WithEvents txtStockTransferJobWork As common.UserControls.txtFinder
    Friend WithEvents lblStockTransferJobWorkDesc As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents fndStoreConsumptionAcc As common.UserControls.txtFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtStoreConsumtion As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndFAAccount As common.UserControls.txtFinder
    Friend WithEvents lblFaAccountDes As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents Bsws1 As ERP.BSWS.BSWS
    Friend WithEvents fndPurchaseLoss As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtPurchaseLoss As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents fndWrekageAccount As common.UserControls.txtFinder
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtWrekageAccount As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblItemOpeningClearing As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents fndItemOpeningClearing As common.UserControls.txtFinder
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
End Class

