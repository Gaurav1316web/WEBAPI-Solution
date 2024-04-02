Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerAccountSet
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
        Dim TableViewDefinition7 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndaccountsetcode = New common.UserControls.txtNavigator()
        Me.rdlblAccountsetcode = New common.Controls.MyLabel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.rdlbldescription = New common.Controls.MyLabel()
        Me.rdtxtdescription = New common.Controls.MyTextBox()
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton()
        Me.rdgrpbxgeneralledgeraccounts = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.FndRateDifference = New common.UserControls.txtFinder()
        Me.lblRateDifference = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.fndCustomerSecurityOpeningClearingAC = New common.UserControls.txtFinder()
        Me.lblCustomerSecurityOpeningClearingAC = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.fndCustomerOpeningClearingAC = New common.UserControls.txtFinder()
        Me.lblCustomerOpeningClearingAC = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.fndLeakageDeduction = New common.UserControls.txtFinder()
        Me.FndPenaltyCharges = New common.UserControls.txtFinder()
        Me.txtLeakageDed = New common.Controls.MyTextBox()
        Me.lblPenaltyCharges = New common.Controls.MyTextBox()
        Me.lblSubsidyAccount = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtSubsidyAccount = New common.UserControls.txtFinder()
        Me.TxtBankChargesOther = New common.UserControls.txtFinder()
        Me.txtSubsidy = New common.Controls.MyTextBox()
        Me.lblBankChargesOther = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.TxtForeignBankCharges = New common.UserControls.txtFinder()
        Me.lblForeignBankCharges = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtloss = New common.UserControls.txtFinder()
        Me.txtloss_name = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtgain = New common.UserControls.txtFinder()
        Me.txtgian_name = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtconsignmnt = New common.UserControls.txtFinder()
        Me.txtcongnmnt_name = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtgsoc = New common.UserControls.txtFinder()
        Me.txtgsoc_name = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.fndAccount2 = New common.UserControls.txtFinder()
        Me.lblAccount2Name = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndAccount1 = New common.UserControls.txtFinder()
        Me.lblAccount1Name = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndBankGuarantee = New common.UserControls.txtFinder()
        Me.lblBankGuaranteeName = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndCreateSecurity = New common.UserControls.txtFinder()
        Me.lblCreateSecurityName = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.FndSecurity = New common.UserControls.txtFinder()
        Me.lblSecurityName = New common.Controls.MyTextBox()
        Me.lblExchangeGain = New common.Controls.MyLabel()
        Me.lblExchangeLoss = New common.Controls.MyLabel()
        Me.fndExchangeGain = New common.UserControls.txtFinder()
        Me.lblExchangeGainName = New common.Controls.MyTextBox()
        Me.fndExchangeLoss = New common.UserControls.txtFinder()
        Me.lblExchangeLossName = New common.Controls.MyTextBox()
        Me.lblCurrencyName = New common.Controls.MyTextBox()
        Me.lblcontainer = New common.Controls.MyLabel()
        Me.fndBaseCurrency = New common.UserControls.txtFinder()
        Me.lblBaseCurrency = New common.Controls.MyLabel()
        Me.fndcontainer = New common.UserControls.txtFinder()
        Me.fndwriteoffs = New common.UserControls.txtFinder()
        Me.rdlblWriteoffs = New common.Controls.MyLabel()
        Me.fndadvance = New common.UserControls.txtFinder()
        Me.rdlblAdvance = New common.Controls.MyLabel()
        Me.fndrecieptdiscount = New common.UserControls.txtFinder()
        Me.rdlblrecieptdiscount = New common.Controls.MyLabel()
        Me.fndrecisvablecontrol = New common.UserControls.txtFinder()
        Me.rdlblrecievablescontrol = New common.Controls.MyLabel()
        Me.txtcontainer = New common.Controls.MyTextBox()
        Me.rdtxtwriteoff = New common.Controls.MyTextBox()
        Me.rdtxtadvance = New common.Controls.MyTextBox()
        Me.rdtxtrecieptdicount = New common.Controls.MyTextBox()
        Me.rdtxtrecievablecontrol = New common.Controls.MyTextBox()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.RadScrollablePanel1 = New Telerik.WinControls.UI.RadScrollablePanel()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxgeneralledgeraccounts.SuspendLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRateDifference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerSecurityOpeningClearingAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerOpeningClearingAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLeakageDed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPenaltyCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubsidyAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubsidy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankChargesOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblForeignBankCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtloss_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtgian_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcongnmnt_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtgsoc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccount2Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccount1Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankGuaranteeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreateSecurityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSecurityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExchangeGain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExchangeLoss, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExchangeGainName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExchangeLossName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrencyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcontainer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblWriteoffs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblrecieptdiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblrecievablescontrol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcontainer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtwriteoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtadvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtrecieptdicount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtrecievablecontrol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel1.PanelContainer.SuspendLayout()
        Me.RadScrollablePanel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.AutoScroll = True
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(802, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'rdmenuimport
        '
        Me.rdmenuimport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndaccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.RadGroupBox4)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlblAccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlbldescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdtxtdescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdgrpbxgeneralledgeraccounts)
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(3, 3)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(639, 659)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.FieldName = Nothing
        Me.fndaccountsetcode.Location = New System.Drawing.Point(138, 4)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Me.rdlblAccountsetcode
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 32767
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(202, 21)
        Me.fndaccountsetcode.TabIndex = 0
        Me.fndaccountsetcode.Value = ""
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.FieldName = Nothing
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(26, 10)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(93, 16)
        Me.rdlblAccountsetcode.TabIndex = 8
        Me.rdlblAccountsetcode.Text = "Account set code"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 570)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(613, 80)
        Me.RadGroupBox4.TabIndex = 2
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
        Me.gvDB.MasterTemplate.ViewDefinition = TableViewDefinition7
        Me.gvDB.MyStopExport = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.ShowHeaderCellButtons = True
        Me.gvDB.Size = New System.Drawing.Size(593, 50)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        '
        'rdlbldescription
        '
        Me.rdlbldescription.FieldName = Nothing
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(26, 28)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 9
        Me.rdlbldescription.Text = "Description"
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
        Me.rdtxtdescription.Location = New System.Drawing.Point(138, 28)
        Me.rdtxtdescription.MaxLength = 50
        Me.rdtxtdescription.MendatroryField = False
        Me.rdtxtdescription.MyLinkLable1 = Me.rdlbldescription
        Me.rdtxtdescription.MyLinkLable2 = Nothing
        Me.rdtxtdescription.Name = "rdtxtdescription"
        Me.rdtxtdescription.ReferenceFieldDesc = Nothing
        Me.rdtxtdescription.ReferenceFieldName = Nothing
        Me.rdtxtdescription.ReferenceTableName = Nothing
        Me.rdtxtdescription.Size = New System.Drawing.Size(488, 20)
        Me.rdtxtdescription.TabIndex = 1
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(342, 5)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 0
        '
        'rdgrpbxgeneralledgeraccounts
        '
        Me.rdgrpbxgeneralledgeraccounts.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel16)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.FndRateDifference)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblRateDifference)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel15)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndCustomerSecurityOpeningClearingAC)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblCustomerSecurityOpeningClearingAC)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel14)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndCustomerOpeningClearingAC)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblCustomerOpeningClearingAC)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel13)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel12)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndLeakageDeduction)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.FndPenaltyCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtLeakageDed)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblPenaltyCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblSubsidyAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel10)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtSubsidyAccount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.TxtBankChargesOther)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtSubsidy)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblBankChargesOther)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel11)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.TxtForeignBankCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblForeignBankCharges)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel9)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtloss)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtloss_name)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel8)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtgain)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtgian_name)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel7)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtconsignmnt)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtcongnmnt_name)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel6)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtgsoc)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtgsoc_name)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel5)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndAccount2)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblAccount2Name)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel4)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndAccount1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblAccount1Name)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel3)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndBankGuarantee)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblBankGuaranteeName)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel2)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndCreateSecurity)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblCreateSecurityName)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel1)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.FndSecurity)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblSecurityName)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblExchangeGain)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblExchangeLoss)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndExchangeGain)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblExchangeGainName)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndExchangeLoss)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblExchangeLossName)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblCurrencyName)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndBaseCurrency)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblBaseCurrency)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndcontainer)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndwriteoffs)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndadvance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndrecieptdiscount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndrecisvablecontrol)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtcontainer)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtwriteoff)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblcontainer)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtadvance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtrecieptdicount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdtxtrecievablecontrol)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblWriteoffs)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblAdvance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblrecieptdiscount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblrecievablescontrol)
        Me.rdgrpbxgeneralledgeraccounts.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxgeneralledgeraccounts.HeaderText = ""
        Me.rdgrpbxgeneralledgeraccounts.Location = New System.Drawing.Point(13, 52)
        Me.rdgrpbxgeneralledgeraccounts.Name = "rdgrpbxgeneralledgeraccounts"
        Me.rdgrpbxgeneralledgeraccounts.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxgeneralledgeraccounts.Size = New System.Drawing.Size(613, 512)
        Me.rdgrpbxgeneralledgeraccounts.TabIndex = 4
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(6, 489)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel16.TabIndex = 160
        Me.MyLabel16.Text = "Rate Difference"
        '
        'FndRateDifference
        '
        Me.FndRateDifference.CalculationExpression = Nothing
        Me.FndRateDifference.FieldCode = Nothing
        Me.FndRateDifference.FieldDesc = Nothing
        Me.FndRateDifference.FieldMaxLength = 0
        Me.FndRateDifference.FieldName = Nothing
        Me.FndRateDifference.isCalculatedField = False
        Me.FndRateDifference.IsSourceFromTable = False
        Me.FndRateDifference.IsSourceFromValueList = False
        Me.FndRateDifference.IsUnique = False
        Me.FndRateDifference.Location = New System.Drawing.Point(127, 487)
        Me.FndRateDifference.MendatroryField = False
        Me.FndRateDifference.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndRateDifference.MyLinkLable1 = Nothing
        Me.FndRateDifference.MyLinkLable2 = Nothing
        Me.FndRateDifference.MyReadOnly = False
        Me.FndRateDifference.MyShowMasterFormButton = False
        Me.FndRateDifference.Name = "FndRateDifference"
        Me.FndRateDifference.ReferenceFieldDesc = Nothing
        Me.FndRateDifference.ReferenceFieldName = Nothing
        Me.FndRateDifference.ReferenceTableName = Nothing
        Me.FndRateDifference.Size = New System.Drawing.Size(143, 19)
        Me.FndRateDifference.TabIndex = 158
        Me.FndRateDifference.Value = ""
        '
        'lblRateDifference
        '
        Me.lblRateDifference.CalculationExpression = Nothing
        Me.lblRateDifference.FieldCode = Nothing
        Me.lblRateDifference.FieldDesc = Nothing
        Me.lblRateDifference.FieldMaxLength = 0
        Me.lblRateDifference.FieldName = Nothing
        Me.lblRateDifference.isCalculatedField = False
        Me.lblRateDifference.IsSourceFromTable = False
        Me.lblRateDifference.IsSourceFromValueList = False
        Me.lblRateDifference.IsUnique = False
        Me.lblRateDifference.Location = New System.Drawing.Point(274, 487)
        Me.lblRateDifference.MendatroryField = False
        Me.lblRateDifference.MyLinkLable1 = Nothing
        Me.lblRateDifference.MyLinkLable2 = Nothing
        Me.lblRateDifference.Name = "lblRateDifference"
        Me.lblRateDifference.ReadOnly = True
        Me.lblRateDifference.ReferenceFieldDesc = Nothing
        Me.lblRateDifference.ReferenceFieldName = Nothing
        Me.lblRateDifference.ReferenceTableName = Nothing
        Me.lblRateDifference.Size = New System.Drawing.Size(326, 20)
        Me.lblRateDifference.TabIndex = 159
        Me.lblRateDifference.TabStop = False
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(6, 467)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(164, 16)
        Me.MyLabel15.TabIndex = 157
        Me.MyLabel15.Text = "Security Opening  Clearing A/C"
        '
        'fndCustomerSecurityOpeningClearingAC
        '
        Me.fndCustomerSecurityOpeningClearingAC.CalculationExpression = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.FieldCode = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.FieldDesc = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.FieldMaxLength = 0
        Me.fndCustomerSecurityOpeningClearingAC.FieldName = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.isCalculatedField = False
        Me.fndCustomerSecurityOpeningClearingAC.IsSourceFromTable = False
        Me.fndCustomerSecurityOpeningClearingAC.IsSourceFromValueList = False
        Me.fndCustomerSecurityOpeningClearingAC.IsUnique = False
        Me.fndCustomerSecurityOpeningClearingAC.Location = New System.Drawing.Point(174, 464)
        Me.fndCustomerSecurityOpeningClearingAC.MendatroryField = False
        Me.fndCustomerSecurityOpeningClearingAC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomerSecurityOpeningClearingAC.MyLinkLable1 = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.MyLinkLable2 = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.MyReadOnly = False
        Me.fndCustomerSecurityOpeningClearingAC.MyShowMasterFormButton = False
        Me.fndCustomerSecurityOpeningClearingAC.Name = "fndCustomerSecurityOpeningClearingAC"
        Me.fndCustomerSecurityOpeningClearingAC.ReferenceFieldDesc = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.ReferenceFieldName = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.ReferenceTableName = Nothing
        Me.fndCustomerSecurityOpeningClearingAC.Size = New System.Drawing.Size(145, 19)
        Me.fndCustomerSecurityOpeningClearingAC.TabIndex = 155
        Me.fndCustomerSecurityOpeningClearingAC.Value = ""
        '
        'lblCustomerSecurityOpeningClearingAC
        '
        Me.lblCustomerSecurityOpeningClearingAC.CalculationExpression = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.FieldCode = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.FieldDesc = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.FieldMaxLength = 0
        Me.lblCustomerSecurityOpeningClearingAC.FieldName = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.isCalculatedField = False
        Me.lblCustomerSecurityOpeningClearingAC.IsSourceFromTable = False
        Me.lblCustomerSecurityOpeningClearingAC.IsSourceFromValueList = False
        Me.lblCustomerSecurityOpeningClearingAC.IsUnique = False
        Me.lblCustomerSecurityOpeningClearingAC.Location = New System.Drawing.Point(325, 464)
        Me.lblCustomerSecurityOpeningClearingAC.MendatroryField = False
        Me.lblCustomerSecurityOpeningClearingAC.MyLinkLable1 = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.MyLinkLable2 = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.Name = "lblCustomerSecurityOpeningClearingAC"
        Me.lblCustomerSecurityOpeningClearingAC.ReadOnly = True
        Me.lblCustomerSecurityOpeningClearingAC.ReferenceFieldDesc = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.ReferenceFieldName = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.ReferenceTableName = Nothing
        Me.lblCustomerSecurityOpeningClearingAC.Size = New System.Drawing.Size(275, 20)
        Me.lblCustomerSecurityOpeningClearingAC.TabIndex = 156
        Me.lblCustomerSecurityOpeningClearingAC.TabStop = False
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(6, 445)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel14.TabIndex = 154
        Me.MyLabel14.Text = "Opening  Clearing A/C"
        '
        'fndCustomerOpeningClearingAC
        '
        Me.fndCustomerOpeningClearingAC.CalculationExpression = Nothing
        Me.fndCustomerOpeningClearingAC.FieldCode = Nothing
        Me.fndCustomerOpeningClearingAC.FieldDesc = Nothing
        Me.fndCustomerOpeningClearingAC.FieldMaxLength = 0
        Me.fndCustomerOpeningClearingAC.FieldName = Nothing
        Me.fndCustomerOpeningClearingAC.isCalculatedField = False
        Me.fndCustomerOpeningClearingAC.IsSourceFromTable = False
        Me.fndCustomerOpeningClearingAC.IsSourceFromValueList = False
        Me.fndCustomerOpeningClearingAC.IsUnique = False
        Me.fndCustomerOpeningClearingAC.Location = New System.Drawing.Point(127, 442)
        Me.fndCustomerOpeningClearingAC.MendatroryField = False
        Me.fndCustomerOpeningClearingAC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomerOpeningClearingAC.MyLinkLable1 = Nothing
        Me.fndCustomerOpeningClearingAC.MyLinkLable2 = Nothing
        Me.fndCustomerOpeningClearingAC.MyReadOnly = False
        Me.fndCustomerOpeningClearingAC.MyShowMasterFormButton = False
        Me.fndCustomerOpeningClearingAC.Name = "fndCustomerOpeningClearingAC"
        Me.fndCustomerOpeningClearingAC.ReferenceFieldDesc = Nothing
        Me.fndCustomerOpeningClearingAC.ReferenceFieldName = Nothing
        Me.fndCustomerOpeningClearingAC.ReferenceTableName = Nothing
        Me.fndCustomerOpeningClearingAC.Size = New System.Drawing.Size(143, 19)
        Me.fndCustomerOpeningClearingAC.TabIndex = 152
        Me.fndCustomerOpeningClearingAC.Value = ""
        '
        'lblCustomerOpeningClearingAC
        '
        Me.lblCustomerOpeningClearingAC.CalculationExpression = Nothing
        Me.lblCustomerOpeningClearingAC.FieldCode = Nothing
        Me.lblCustomerOpeningClearingAC.FieldDesc = Nothing
        Me.lblCustomerOpeningClearingAC.FieldMaxLength = 0
        Me.lblCustomerOpeningClearingAC.FieldName = Nothing
        Me.lblCustomerOpeningClearingAC.isCalculatedField = False
        Me.lblCustomerOpeningClearingAC.IsSourceFromTable = False
        Me.lblCustomerOpeningClearingAC.IsSourceFromValueList = False
        Me.lblCustomerOpeningClearingAC.IsUnique = False
        Me.lblCustomerOpeningClearingAC.Location = New System.Drawing.Point(274, 442)
        Me.lblCustomerOpeningClearingAC.MendatroryField = False
        Me.lblCustomerOpeningClearingAC.MyLinkLable1 = Nothing
        Me.lblCustomerOpeningClearingAC.MyLinkLable2 = Nothing
        Me.lblCustomerOpeningClearingAC.Name = "lblCustomerOpeningClearingAC"
        Me.lblCustomerOpeningClearingAC.ReadOnly = True
        Me.lblCustomerOpeningClearingAC.ReferenceFieldDesc = Nothing
        Me.lblCustomerOpeningClearingAC.ReferenceFieldName = Nothing
        Me.lblCustomerOpeningClearingAC.ReferenceTableName = Nothing
        Me.lblCustomerOpeningClearingAC.Size = New System.Drawing.Size(326, 20)
        Me.lblCustomerOpeningClearingAC.TabIndex = 153
        Me.lblCustomerOpeningClearingAC.TabStop = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(6, 423)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel13.TabIndex = 151
        Me.MyLabel13.Text = "Leakage Deduction"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(6, 352)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel12.TabIndex = 151
        Me.MyLabel12.Text = "Penalty Charges"
        '
        'fndLeakageDeduction
        '
        Me.fndLeakageDeduction.CalculationExpression = Nothing
        Me.fndLeakageDeduction.FieldCode = Nothing
        Me.fndLeakageDeduction.FieldDesc = Nothing
        Me.fndLeakageDeduction.FieldMaxLength = 0
        Me.fndLeakageDeduction.FieldName = Nothing
        Me.fndLeakageDeduction.isCalculatedField = False
        Me.fndLeakageDeduction.IsSourceFromTable = False
        Me.fndLeakageDeduction.IsSourceFromValueList = False
        Me.fndLeakageDeduction.IsUnique = False
        Me.fndLeakageDeduction.Location = New System.Drawing.Point(127, 420)
        Me.fndLeakageDeduction.MendatroryField = False
        Me.fndLeakageDeduction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLeakageDeduction.MyLinkLable1 = Nothing
        Me.fndLeakageDeduction.MyLinkLable2 = Nothing
        Me.fndLeakageDeduction.MyReadOnly = False
        Me.fndLeakageDeduction.MyShowMasterFormButton = False
        Me.fndLeakageDeduction.Name = "fndLeakageDeduction"
        Me.fndLeakageDeduction.ReferenceFieldDesc = Nothing
        Me.fndLeakageDeduction.ReferenceFieldName = Nothing
        Me.fndLeakageDeduction.ReferenceTableName = Nothing
        Me.fndLeakageDeduction.Size = New System.Drawing.Size(143, 19)
        Me.fndLeakageDeduction.TabIndex = 149
        Me.fndLeakageDeduction.Value = ""
        '
        'FndPenaltyCharges
        '
        Me.FndPenaltyCharges.CalculationExpression = Nothing
        Me.FndPenaltyCharges.FieldCode = Nothing
        Me.FndPenaltyCharges.FieldDesc = Nothing
        Me.FndPenaltyCharges.FieldMaxLength = 0
        Me.FndPenaltyCharges.FieldName = Nothing
        Me.FndPenaltyCharges.isCalculatedField = False
        Me.FndPenaltyCharges.IsSourceFromTable = False
        Me.FndPenaltyCharges.IsSourceFromValueList = False
        Me.FndPenaltyCharges.IsUnique = False
        Me.FndPenaltyCharges.Location = New System.Drawing.Point(127, 350)
        Me.FndPenaltyCharges.MendatroryField = False
        Me.FndPenaltyCharges.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndPenaltyCharges.MyLinkLable1 = Nothing
        Me.FndPenaltyCharges.MyLinkLable2 = Nothing
        Me.FndPenaltyCharges.MyReadOnly = False
        Me.FndPenaltyCharges.MyShowMasterFormButton = False
        Me.FndPenaltyCharges.Name = "FndPenaltyCharges"
        Me.FndPenaltyCharges.ReferenceFieldDesc = Nothing
        Me.FndPenaltyCharges.ReferenceFieldName = Nothing
        Me.FndPenaltyCharges.ReferenceTableName = Nothing
        Me.FndPenaltyCharges.Size = New System.Drawing.Size(143, 19)
        Me.FndPenaltyCharges.TabIndex = 149
        Me.FndPenaltyCharges.Value = ""
        '
        'txtLeakageDed
        '
        Me.txtLeakageDed.CalculationExpression = Nothing
        Me.txtLeakageDed.FieldCode = Nothing
        Me.txtLeakageDed.FieldDesc = Nothing
        Me.txtLeakageDed.FieldMaxLength = 0
        Me.txtLeakageDed.FieldName = Nothing
        Me.txtLeakageDed.isCalculatedField = False
        Me.txtLeakageDed.IsSourceFromTable = False
        Me.txtLeakageDed.IsSourceFromValueList = False
        Me.txtLeakageDed.IsUnique = False
        Me.txtLeakageDed.Location = New System.Drawing.Point(274, 420)
        Me.txtLeakageDed.MendatroryField = False
        Me.txtLeakageDed.MyLinkLable1 = Nothing
        Me.txtLeakageDed.MyLinkLable2 = Nothing
        Me.txtLeakageDed.Name = "txtLeakageDed"
        Me.txtLeakageDed.ReadOnly = True
        Me.txtLeakageDed.ReferenceFieldDesc = Nothing
        Me.txtLeakageDed.ReferenceFieldName = Nothing
        Me.txtLeakageDed.ReferenceTableName = Nothing
        Me.txtLeakageDed.Size = New System.Drawing.Size(326, 20)
        Me.txtLeakageDed.TabIndex = 150
        Me.txtLeakageDed.TabStop = False
        '
        'lblPenaltyCharges
        '
        Me.lblPenaltyCharges.CalculationExpression = Nothing
        Me.lblPenaltyCharges.FieldCode = Nothing
        Me.lblPenaltyCharges.FieldDesc = Nothing
        Me.lblPenaltyCharges.FieldMaxLength = 0
        Me.lblPenaltyCharges.FieldName = Nothing
        Me.lblPenaltyCharges.isCalculatedField = False
        Me.lblPenaltyCharges.IsSourceFromTable = False
        Me.lblPenaltyCharges.IsSourceFromValueList = False
        Me.lblPenaltyCharges.IsUnique = False
        Me.lblPenaltyCharges.Location = New System.Drawing.Point(274, 350)
        Me.lblPenaltyCharges.MendatroryField = False
        Me.lblPenaltyCharges.MyLinkLable1 = Nothing
        Me.lblPenaltyCharges.MyLinkLable2 = Nothing
        Me.lblPenaltyCharges.Name = "lblPenaltyCharges"
        Me.lblPenaltyCharges.ReadOnly = True
        Me.lblPenaltyCharges.ReferenceFieldDesc = Nothing
        Me.lblPenaltyCharges.ReferenceFieldName = Nothing
        Me.lblPenaltyCharges.ReferenceTableName = Nothing
        Me.lblPenaltyCharges.Size = New System.Drawing.Size(326, 20)
        Me.lblPenaltyCharges.TabIndex = 150
        Me.lblPenaltyCharges.TabStop = False
        '
        'lblSubsidyAccount
        '
        Me.lblSubsidyAccount.FieldName = Nothing
        Me.lblSubsidyAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubsidyAccount.Location = New System.Drawing.Point(6, 376)
        Me.lblSubsidyAccount.Name = "lblSubsidyAccount"
        Me.lblSubsidyAccount.Size = New System.Drawing.Size(92, 16)
        Me.lblSubsidyAccount.TabIndex = 148
        Me.lblSubsidyAccount.Text = "SubSidy Account"
        Me.lblSubsidyAccount.Visible = False
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(6, 330)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel10.TabIndex = 145
        Me.MyLabel10.Text = "Bank Charges Other"
        '
        'txtSubsidyAccount
        '
        Me.txtSubsidyAccount.CalculationExpression = Nothing
        Me.txtSubsidyAccount.FieldCode = Nothing
        Me.txtSubsidyAccount.FieldDesc = Nothing
        Me.txtSubsidyAccount.FieldMaxLength = 0
        Me.txtSubsidyAccount.FieldName = Nothing
        Me.txtSubsidyAccount.isCalculatedField = False
        Me.txtSubsidyAccount.IsSourceFromTable = False
        Me.txtSubsidyAccount.IsSourceFromValueList = False
        Me.txtSubsidyAccount.IsUnique = False
        Me.txtSubsidyAccount.Location = New System.Drawing.Point(127, 373)
        Me.txtSubsidyAccount.MendatroryField = False
        Me.txtSubsidyAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubsidyAccount.MyLinkLable1 = Nothing
        Me.txtSubsidyAccount.MyLinkLable2 = Nothing
        Me.txtSubsidyAccount.MyReadOnly = False
        Me.txtSubsidyAccount.MyShowMasterFormButton = False
        Me.txtSubsidyAccount.Name = "txtSubsidyAccount"
        Me.txtSubsidyAccount.ReferenceFieldDesc = Nothing
        Me.txtSubsidyAccount.ReferenceFieldName = Nothing
        Me.txtSubsidyAccount.ReferenceTableName = Nothing
        Me.txtSubsidyAccount.Size = New System.Drawing.Size(143, 19)
        Me.txtSubsidyAccount.TabIndex = 146
        Me.txtSubsidyAccount.Value = ""
        Me.txtSubsidyAccount.Visible = False
        '
        'TxtBankChargesOther
        '
        Me.TxtBankChargesOther.CalculationExpression = Nothing
        Me.TxtBankChargesOther.FieldCode = Nothing
        Me.TxtBankChargesOther.FieldDesc = Nothing
        Me.TxtBankChargesOther.FieldMaxLength = 0
        Me.TxtBankChargesOther.FieldName = Nothing
        Me.TxtBankChargesOther.isCalculatedField = False
        Me.TxtBankChargesOther.IsSourceFromTable = False
        Me.TxtBankChargesOther.IsSourceFromValueList = False
        Me.TxtBankChargesOther.IsUnique = False
        Me.TxtBankChargesOther.Location = New System.Drawing.Point(127, 327)
        Me.TxtBankChargesOther.MendatroryField = False
        Me.TxtBankChargesOther.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankChargesOther.MyLinkLable1 = Nothing
        Me.TxtBankChargesOther.MyLinkLable2 = Nothing
        Me.TxtBankChargesOther.MyReadOnly = False
        Me.TxtBankChargesOther.MyShowMasterFormButton = False
        Me.TxtBankChargesOther.Name = "TxtBankChargesOther"
        Me.TxtBankChargesOther.ReferenceFieldDesc = Nothing
        Me.TxtBankChargesOther.ReferenceFieldName = Nothing
        Me.TxtBankChargesOther.ReferenceTableName = Nothing
        Me.TxtBankChargesOther.Size = New System.Drawing.Size(143, 19)
        Me.TxtBankChargesOther.TabIndex = 141
        Me.TxtBankChargesOther.Value = ""
        '
        'txtSubsidy
        '
        Me.txtSubsidy.CalculationExpression = Nothing
        Me.txtSubsidy.FieldCode = Nothing
        Me.txtSubsidy.FieldDesc = Nothing
        Me.txtSubsidy.FieldMaxLength = 0
        Me.txtSubsidy.FieldName = Nothing
        Me.txtSubsidy.isCalculatedField = False
        Me.txtSubsidy.IsSourceFromTable = False
        Me.txtSubsidy.IsSourceFromValueList = False
        Me.txtSubsidy.IsUnique = False
        Me.txtSubsidy.Location = New System.Drawing.Point(274, 373)
        Me.txtSubsidy.MendatroryField = False
        Me.txtSubsidy.MyLinkLable1 = Nothing
        Me.txtSubsidy.MyLinkLable2 = Nothing
        Me.txtSubsidy.Name = "txtSubsidy"
        Me.txtSubsidy.ReadOnly = True
        Me.txtSubsidy.ReferenceFieldDesc = Nothing
        Me.txtSubsidy.ReferenceFieldName = Nothing
        Me.txtSubsidy.ReferenceTableName = Nothing
        Me.txtSubsidy.Size = New System.Drawing.Size(326, 20)
        Me.txtSubsidy.TabIndex = 147
        Me.txtSubsidy.TabStop = False
        Me.txtSubsidy.Visible = False
        '
        'lblBankChargesOther
        '
        Me.lblBankChargesOther.CalculationExpression = Nothing
        Me.lblBankChargesOther.FieldCode = Nothing
        Me.lblBankChargesOther.FieldDesc = Nothing
        Me.lblBankChargesOther.FieldMaxLength = 0
        Me.lblBankChargesOther.FieldName = Nothing
        Me.lblBankChargesOther.isCalculatedField = False
        Me.lblBankChargesOther.IsSourceFromTable = False
        Me.lblBankChargesOther.IsSourceFromValueList = False
        Me.lblBankChargesOther.IsUnique = False
        Me.lblBankChargesOther.Location = New System.Drawing.Point(274, 327)
        Me.lblBankChargesOther.MendatroryField = False
        Me.lblBankChargesOther.MyLinkLable1 = Nothing
        Me.lblBankChargesOther.MyLinkLable2 = Nothing
        Me.lblBankChargesOther.Name = "lblBankChargesOther"
        Me.lblBankChargesOther.ReadOnly = True
        Me.lblBankChargesOther.ReferenceFieldDesc = Nothing
        Me.lblBankChargesOther.ReferenceFieldName = Nothing
        Me.lblBankChargesOther.ReferenceTableName = Nothing
        Me.lblBankChargesOther.Size = New System.Drawing.Size(326, 20)
        Me.lblBankChargesOther.TabIndex = 143
        Me.lblBankChargesOther.TabStop = False
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(6, 304)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel11.TabIndex = 144
        Me.MyLabel11.Text = "Foreign Bank Charges"
        '
        'TxtForeignBankCharges
        '
        Me.TxtForeignBankCharges.CalculationExpression = Nothing
        Me.TxtForeignBankCharges.FieldCode = Nothing
        Me.TxtForeignBankCharges.FieldDesc = Nothing
        Me.TxtForeignBankCharges.FieldMaxLength = 0
        Me.TxtForeignBankCharges.FieldName = Nothing
        Me.TxtForeignBankCharges.isCalculatedField = False
        Me.TxtForeignBankCharges.IsSourceFromTable = False
        Me.TxtForeignBankCharges.IsSourceFromValueList = False
        Me.TxtForeignBankCharges.IsUnique = False
        Me.TxtForeignBankCharges.Location = New System.Drawing.Point(127, 303)
        Me.TxtForeignBankCharges.MendatroryField = False
        Me.TxtForeignBankCharges.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtForeignBankCharges.MyLinkLable1 = Nothing
        Me.TxtForeignBankCharges.MyLinkLable2 = Nothing
        Me.TxtForeignBankCharges.MyReadOnly = False
        Me.TxtForeignBankCharges.MyShowMasterFormButton = False
        Me.TxtForeignBankCharges.Name = "TxtForeignBankCharges"
        Me.TxtForeignBankCharges.ReferenceFieldDesc = Nothing
        Me.TxtForeignBankCharges.ReferenceFieldName = Nothing
        Me.TxtForeignBankCharges.ReferenceTableName = Nothing
        Me.TxtForeignBankCharges.Size = New System.Drawing.Size(143, 19)
        Me.TxtForeignBankCharges.TabIndex = 140
        Me.TxtForeignBankCharges.Value = ""
        '
        'lblForeignBankCharges
        '
        Me.lblForeignBankCharges.CalculationExpression = Nothing
        Me.lblForeignBankCharges.FieldCode = Nothing
        Me.lblForeignBankCharges.FieldDesc = Nothing
        Me.lblForeignBankCharges.FieldMaxLength = 0
        Me.lblForeignBankCharges.FieldName = Nothing
        Me.lblForeignBankCharges.isCalculatedField = False
        Me.lblForeignBankCharges.IsSourceFromTable = False
        Me.lblForeignBankCharges.IsSourceFromValueList = False
        Me.lblForeignBankCharges.IsUnique = False
        Me.lblForeignBankCharges.Location = New System.Drawing.Point(274, 303)
        Me.lblForeignBankCharges.MendatroryField = False
        Me.lblForeignBankCharges.MyLinkLable1 = Nothing
        Me.lblForeignBankCharges.MyLinkLable2 = Nothing
        Me.lblForeignBankCharges.Name = "lblForeignBankCharges"
        Me.lblForeignBankCharges.ReadOnly = True
        Me.lblForeignBankCharges.ReferenceFieldDesc = Nothing
        Me.lblForeignBankCharges.ReferenceFieldName = Nothing
        Me.lblForeignBankCharges.ReferenceTableName = Nothing
        Me.lblForeignBankCharges.Size = New System.Drawing.Size(326, 20)
        Me.lblForeignBankCharges.TabIndex = 142
        Me.lblForeignBankCharges.TabStop = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(12, 397)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel9.TabIndex = 139
        Me.MyLabel9.Text = "Loss"
        Me.MyLabel9.Visible = False
        '
        'txtloss
        '
        Me.txtloss.CalculationExpression = Nothing
        Me.txtloss.FieldCode = Nothing
        Me.txtloss.FieldDesc = Nothing
        Me.txtloss.FieldMaxLength = 0
        Me.txtloss.FieldName = Nothing
        Me.txtloss.isCalculatedField = False
        Me.txtloss.IsSourceFromTable = False
        Me.txtloss.IsSourceFromValueList = False
        Me.txtloss.IsUnique = False
        Me.txtloss.Location = New System.Drawing.Point(127, 394)
        Me.txtloss.MendatroryField = False
        Me.txtloss.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloss.MyLinkLable1 = Me.MyLabel9
        Me.txtloss.MyLinkLable2 = Nothing
        Me.txtloss.MyReadOnly = False
        Me.txtloss.MyShowMasterFormButton = False
        Me.txtloss.Name = "txtloss"
        Me.txtloss.ReferenceFieldDesc = Nothing
        Me.txtloss.ReferenceFieldName = Nothing
        Me.txtloss.ReferenceTableName = Nothing
        Me.txtloss.Size = New System.Drawing.Size(141, 19)
        Me.txtloss.TabIndex = 16
        Me.txtloss.Value = ""
        Me.txtloss.Visible = False
        '
        'txtloss_name
        '
        Me.txtloss_name.CalculationExpression = Nothing
        Me.txtloss_name.FieldCode = Nothing
        Me.txtloss_name.FieldDesc = Nothing
        Me.txtloss_name.FieldMaxLength = 0
        Me.txtloss_name.FieldName = Nothing
        Me.txtloss_name.isCalculatedField = False
        Me.txtloss_name.IsSourceFromTable = False
        Me.txtloss_name.IsSourceFromValueList = False
        Me.txtloss_name.IsUnique = False
        Me.txtloss_name.Location = New System.Drawing.Point(274, 394)
        Me.txtloss_name.MendatroryField = False
        Me.txtloss_name.MyLinkLable1 = Nothing
        Me.txtloss_name.MyLinkLable2 = Nothing
        Me.txtloss_name.Name = "txtloss_name"
        Me.txtloss_name.ReadOnly = True
        Me.txtloss_name.ReferenceFieldDesc = Nothing
        Me.txtloss_name.ReferenceFieldName = Nothing
        Me.txtloss_name.ReferenceTableName = Nothing
        Me.txtloss_name.Size = New System.Drawing.Size(326, 20)
        Me.txtloss_name.TabIndex = 138
        Me.txtloss_name.TabStop = False
        Me.txtloss_name.Visible = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(490, 374)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel8.TabIndex = 136
        Me.MyLabel8.Text = "Gain"
        Me.MyLabel8.Visible = False
        '
        'txtgain
        '
        Me.txtgain.CalculationExpression = Nothing
        Me.txtgain.FieldCode = Nothing
        Me.txtgain.FieldDesc = Nothing
        Me.txtgain.FieldMaxLength = 0
        Me.txtgain.FieldName = Nothing
        Me.txtgain.isCalculatedField = False
        Me.txtgain.IsSourceFromTable = False
        Me.txtgain.IsSourceFromValueList = False
        Me.txtgain.IsUnique = False
        Me.txtgain.Location = New System.Drawing.Point(524, 371)
        Me.txtgain.MendatroryField = False
        Me.txtgain.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgain.MyLinkLable1 = Me.MyLabel8
        Me.txtgain.MyLinkLable2 = Nothing
        Me.txtgain.MyReadOnly = False
        Me.txtgain.MyShowMasterFormButton = False
        Me.txtgain.Name = "txtgain"
        Me.txtgain.ReferenceFieldDesc = Nothing
        Me.txtgain.ReferenceFieldName = Nothing
        Me.txtgain.ReferenceTableName = Nothing
        Me.txtgain.Size = New System.Drawing.Size(36, 19)
        Me.txtgain.TabIndex = 15
        Me.txtgain.Value = ""
        Me.txtgain.Visible = False
        '
        'txtgian_name
        '
        Me.txtgian_name.CalculationExpression = Nothing
        Me.txtgian_name.FieldCode = Nothing
        Me.txtgian_name.FieldDesc = Nothing
        Me.txtgian_name.FieldMaxLength = 0
        Me.txtgian_name.FieldName = Nothing
        Me.txtgian_name.isCalculatedField = False
        Me.txtgian_name.IsSourceFromTable = False
        Me.txtgian_name.IsSourceFromValueList = False
        Me.txtgian_name.IsUnique = False
        Me.txtgian_name.Location = New System.Drawing.Point(565, 371)
        Me.txtgian_name.MendatroryField = False
        Me.txtgian_name.MyLinkLable1 = Nothing
        Me.txtgian_name.MyLinkLable2 = Nothing
        Me.txtgian_name.Name = "txtgian_name"
        Me.txtgian_name.ReadOnly = True
        Me.txtgian_name.ReferenceFieldDesc = Nothing
        Me.txtgian_name.ReferenceFieldName = Nothing
        Me.txtgian_name.ReferenceTableName = Nothing
        Me.txtgian_name.Size = New System.Drawing.Size(35, 20)
        Me.txtgian_name.TabIndex = 135
        Me.txtgian_name.TabStop = False
        Me.txtgian_name.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(12, 369)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel7.TabIndex = 133
        Me.MyLabel7.Text = "Consignment"
        Me.MyLabel7.Visible = False
        '
        'txtconsignmnt
        '
        Me.txtconsignmnt.CalculationExpression = Nothing
        Me.txtconsignmnt.FieldCode = Nothing
        Me.txtconsignmnt.FieldDesc = Nothing
        Me.txtconsignmnt.FieldMaxLength = 0
        Me.txtconsignmnt.FieldName = Nothing
        Me.txtconsignmnt.isCalculatedField = False
        Me.txtconsignmnt.IsSourceFromTable = False
        Me.txtconsignmnt.IsSourceFromValueList = False
        Me.txtconsignmnt.IsUnique = False
        Me.txtconsignmnt.Location = New System.Drawing.Point(125, 366)
        Me.txtconsignmnt.MendatroryField = False
        Me.txtconsignmnt.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtconsignmnt.MyLinkLable1 = Me.MyLabel7
        Me.txtconsignmnt.MyLinkLable2 = Nothing
        Me.txtconsignmnt.MyReadOnly = False
        Me.txtconsignmnt.MyShowMasterFormButton = False
        Me.txtconsignmnt.Name = "txtconsignmnt"
        Me.txtconsignmnt.ReferenceFieldDesc = Nothing
        Me.txtconsignmnt.ReferenceFieldName = Nothing
        Me.txtconsignmnt.ReferenceTableName = Nothing
        Me.txtconsignmnt.Size = New System.Drawing.Size(39, 19)
        Me.txtconsignmnt.TabIndex = 14
        Me.txtconsignmnt.Value = ""
        Me.txtconsignmnt.Visible = False
        '
        'txtcongnmnt_name
        '
        Me.txtcongnmnt_name.CalculationExpression = Nothing
        Me.txtcongnmnt_name.FieldCode = Nothing
        Me.txtcongnmnt_name.FieldDesc = Nothing
        Me.txtcongnmnt_name.FieldMaxLength = 0
        Me.txtcongnmnt_name.FieldName = Nothing
        Me.txtcongnmnt_name.isCalculatedField = False
        Me.txtcongnmnt_name.IsSourceFromTable = False
        Me.txtcongnmnt_name.IsSourceFromValueList = False
        Me.txtcongnmnt_name.IsUnique = False
        Me.txtcongnmnt_name.Location = New System.Drawing.Point(170, 366)
        Me.txtcongnmnt_name.MendatroryField = False
        Me.txtcongnmnt_name.MyLinkLable1 = Nothing
        Me.txtcongnmnt_name.MyLinkLable2 = Nothing
        Me.txtcongnmnt_name.Name = "txtcongnmnt_name"
        Me.txtcongnmnt_name.ReadOnly = True
        Me.txtcongnmnt_name.ReferenceFieldDesc = Nothing
        Me.txtcongnmnt_name.ReferenceFieldName = Nothing
        Me.txtcongnmnt_name.ReferenceTableName = Nothing
        Me.txtcongnmnt_name.Size = New System.Drawing.Size(30, 20)
        Me.txtcongnmnt_name.TabIndex = 132
        Me.txtcongnmnt_name.TabStop = False
        Me.txtcongnmnt_name.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(359, 370)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel6.TabIndex = 130
        Me.MyLabel6.Text = "GSOC"
        Me.MyLabel6.Visible = False
        '
        'txtgsoc
        '
        Me.txtgsoc.CalculationExpression = Nothing
        Me.txtgsoc.FieldCode = Nothing
        Me.txtgsoc.FieldDesc = Nothing
        Me.txtgsoc.FieldMaxLength = 0
        Me.txtgsoc.FieldName = Nothing
        Me.txtgsoc.isCalculatedField = False
        Me.txtgsoc.IsSourceFromTable = False
        Me.txtgsoc.IsSourceFromValueList = False
        Me.txtgsoc.IsUnique = False
        Me.txtgsoc.Location = New System.Drawing.Point(403, 366)
        Me.txtgsoc.MendatroryField = False
        Me.txtgsoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgsoc.MyLinkLable1 = Me.MyLabel6
        Me.txtgsoc.MyLinkLable2 = Nothing
        Me.txtgsoc.MyReadOnly = False
        Me.txtgsoc.MyShowMasterFormButton = False
        Me.txtgsoc.Name = "txtgsoc"
        Me.txtgsoc.ReferenceFieldDesc = Nothing
        Me.txtgsoc.ReferenceFieldName = Nothing
        Me.txtgsoc.ReferenceTableName = Nothing
        Me.txtgsoc.Size = New System.Drawing.Size(39, 19)
        Me.txtgsoc.TabIndex = 13
        Me.txtgsoc.Value = ""
        Me.txtgsoc.Visible = False
        '
        'txtgsoc_name
        '
        Me.txtgsoc_name.CalculationExpression = Nothing
        Me.txtgsoc_name.FieldCode = Nothing
        Me.txtgsoc_name.FieldDesc = Nothing
        Me.txtgsoc_name.FieldMaxLength = 0
        Me.txtgsoc_name.FieldName = Nothing
        Me.txtgsoc_name.isCalculatedField = False
        Me.txtgsoc_name.IsSourceFromTable = False
        Me.txtgsoc_name.IsSourceFromValueList = False
        Me.txtgsoc_name.IsUnique = False
        Me.txtgsoc_name.Location = New System.Drawing.Point(446, 366)
        Me.txtgsoc_name.MendatroryField = False
        Me.txtgsoc_name.MyLinkLable1 = Nothing
        Me.txtgsoc_name.MyLinkLable2 = Nothing
        Me.txtgsoc_name.Name = "txtgsoc_name"
        Me.txtgsoc_name.ReadOnly = True
        Me.txtgsoc_name.ReferenceFieldDesc = Nothing
        Me.txtgsoc_name.ReferenceFieldName = Nothing
        Me.txtgsoc_name.ReferenceTableName = Nothing
        Me.txtgsoc_name.Size = New System.Drawing.Size(36, 20)
        Me.txtgsoc_name.TabIndex = 129
        Me.txtgsoc_name.TabStop = False
        Me.txtgsoc_name.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(6, 282)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel5.TabIndex = 127
        Me.MyLabel5.Text = "Other Security"
        '
        'fndAccount2
        '
        Me.fndAccount2.CalculationExpression = Nothing
        Me.fndAccount2.FieldCode = Nothing
        Me.fndAccount2.FieldDesc = Nothing
        Me.fndAccount2.FieldMaxLength = 0
        Me.fndAccount2.FieldName = Nothing
        Me.fndAccount2.isCalculatedField = False
        Me.fndAccount2.IsSourceFromTable = False
        Me.fndAccount2.IsSourceFromValueList = False
        Me.fndAccount2.IsUnique = False
        Me.fndAccount2.Location = New System.Drawing.Point(127, 279)
        Me.fndAccount2.MendatroryField = True
        Me.fndAccount2.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAccount2.MyLinkLable1 = Nothing
        Me.fndAccount2.MyLinkLable2 = Nothing
        Me.fndAccount2.MyReadOnly = False
        Me.fndAccount2.MyShowMasterFormButton = False
        Me.fndAccount2.Name = "fndAccount2"
        Me.fndAccount2.ReferenceFieldDesc = Nothing
        Me.fndAccount2.ReferenceFieldName = Nothing
        Me.fndAccount2.ReferenceTableName = Nothing
        Me.fndAccount2.Size = New System.Drawing.Size(143, 19)
        Me.fndAccount2.TabIndex = 12
        Me.fndAccount2.Value = ""
        '
        'lblAccount2Name
        '
        Me.lblAccount2Name.CalculationExpression = Nothing
        Me.lblAccount2Name.FieldCode = Nothing
        Me.lblAccount2Name.FieldDesc = Nothing
        Me.lblAccount2Name.FieldMaxLength = 0
        Me.lblAccount2Name.FieldName = Nothing
        Me.lblAccount2Name.isCalculatedField = False
        Me.lblAccount2Name.IsSourceFromTable = False
        Me.lblAccount2Name.IsSourceFromValueList = False
        Me.lblAccount2Name.IsUnique = False
        Me.lblAccount2Name.Location = New System.Drawing.Point(274, 279)
        Me.lblAccount2Name.MendatroryField = False
        Me.lblAccount2Name.MyLinkLable1 = Nothing
        Me.lblAccount2Name.MyLinkLable2 = Nothing
        Me.lblAccount2Name.Name = "lblAccount2Name"
        Me.lblAccount2Name.ReadOnly = True
        Me.lblAccount2Name.ReferenceFieldDesc = Nothing
        Me.lblAccount2Name.ReferenceFieldName = Nothing
        Me.lblAccount2Name.ReferenceTableName = Nothing
        Me.lblAccount2Name.Size = New System.Drawing.Size(326, 20)
        Me.lblAccount2Name.TabIndex = 25
        Me.lblAccount2Name.TabStop = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 256)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(110, 16)
        Me.MyLabel4.TabIndex = 124
        Me.MyLabel4.Text = "Refrigerator Security"
        '
        'fndAccount1
        '
        Me.fndAccount1.CalculationExpression = Nothing
        Me.fndAccount1.FieldCode = Nothing
        Me.fndAccount1.FieldDesc = Nothing
        Me.fndAccount1.FieldMaxLength = 0
        Me.fndAccount1.FieldName = Nothing
        Me.fndAccount1.isCalculatedField = False
        Me.fndAccount1.IsSourceFromTable = False
        Me.fndAccount1.IsSourceFromValueList = False
        Me.fndAccount1.IsUnique = False
        Me.fndAccount1.Location = New System.Drawing.Point(127, 255)
        Me.fndAccount1.MendatroryField = True
        Me.fndAccount1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAccount1.MyLinkLable1 = Nothing
        Me.fndAccount1.MyLinkLable2 = Nothing
        Me.fndAccount1.MyReadOnly = False
        Me.fndAccount1.MyShowMasterFormButton = False
        Me.fndAccount1.Name = "fndAccount1"
        Me.fndAccount1.ReferenceFieldDesc = Nothing
        Me.fndAccount1.ReferenceFieldName = Nothing
        Me.fndAccount1.ReferenceTableName = Nothing
        Me.fndAccount1.Size = New System.Drawing.Size(143, 19)
        Me.fndAccount1.TabIndex = 11
        Me.fndAccount1.Value = ""
        '
        'lblAccount1Name
        '
        Me.lblAccount1Name.CalculationExpression = Nothing
        Me.lblAccount1Name.FieldCode = Nothing
        Me.lblAccount1Name.FieldDesc = Nothing
        Me.lblAccount1Name.FieldMaxLength = 0
        Me.lblAccount1Name.FieldName = Nothing
        Me.lblAccount1Name.isCalculatedField = False
        Me.lblAccount1Name.IsSourceFromTable = False
        Me.lblAccount1Name.IsSourceFromValueList = False
        Me.lblAccount1Name.IsUnique = False
        Me.lblAccount1Name.Location = New System.Drawing.Point(274, 255)
        Me.lblAccount1Name.MendatroryField = False
        Me.lblAccount1Name.MyLinkLable1 = Nothing
        Me.lblAccount1Name.MyLinkLable2 = Nothing
        Me.lblAccount1Name.Name = "lblAccount1Name"
        Me.lblAccount1Name.ReadOnly = True
        Me.lblAccount1Name.ReferenceFieldDesc = Nothing
        Me.lblAccount1Name.ReferenceFieldName = Nothing
        Me.lblAccount1Name.ReferenceTableName = Nothing
        Me.lblAccount1Name.Size = New System.Drawing.Size(326, 20)
        Me.lblAccount1Name.TabIndex = 23
        Me.lblAccount1Name.TabStop = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 232)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel3.TabIndex = 121
        Me.MyLabel3.Text = "Bank Guarantee"
        '
        'fndBankGuarantee
        '
        Me.fndBankGuarantee.CalculationExpression = Nothing
        Me.fndBankGuarantee.FieldCode = Nothing
        Me.fndBankGuarantee.FieldDesc = Nothing
        Me.fndBankGuarantee.FieldMaxLength = 0
        Me.fndBankGuarantee.FieldName = Nothing
        Me.fndBankGuarantee.isCalculatedField = False
        Me.fndBankGuarantee.IsSourceFromTable = False
        Me.fndBankGuarantee.IsSourceFromValueList = False
        Me.fndBankGuarantee.IsUnique = False
        Me.fndBankGuarantee.Location = New System.Drawing.Point(127, 231)
        Me.fndBankGuarantee.MendatroryField = True
        Me.fndBankGuarantee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankGuarantee.MyLinkLable1 = Nothing
        Me.fndBankGuarantee.MyLinkLable2 = Nothing
        Me.fndBankGuarantee.MyReadOnly = False
        Me.fndBankGuarantee.MyShowMasterFormButton = False
        Me.fndBankGuarantee.Name = "fndBankGuarantee"
        Me.fndBankGuarantee.ReferenceFieldDesc = Nothing
        Me.fndBankGuarantee.ReferenceFieldName = Nothing
        Me.fndBankGuarantee.ReferenceTableName = Nothing
        Me.fndBankGuarantee.Size = New System.Drawing.Size(143, 19)
        Me.fndBankGuarantee.TabIndex = 10
        Me.fndBankGuarantee.Value = ""
        '
        'lblBankGuaranteeName
        '
        Me.lblBankGuaranteeName.CalculationExpression = Nothing
        Me.lblBankGuaranteeName.FieldCode = Nothing
        Me.lblBankGuaranteeName.FieldDesc = Nothing
        Me.lblBankGuaranteeName.FieldMaxLength = 0
        Me.lblBankGuaranteeName.FieldName = Nothing
        Me.lblBankGuaranteeName.isCalculatedField = False
        Me.lblBankGuaranteeName.IsSourceFromTable = False
        Me.lblBankGuaranteeName.IsSourceFromValueList = False
        Me.lblBankGuaranteeName.IsUnique = False
        Me.lblBankGuaranteeName.Location = New System.Drawing.Point(274, 231)
        Me.lblBankGuaranteeName.MendatroryField = False
        Me.lblBankGuaranteeName.MyLinkLable1 = Nothing
        Me.lblBankGuaranteeName.MyLinkLable2 = Nothing
        Me.lblBankGuaranteeName.Name = "lblBankGuaranteeName"
        Me.lblBankGuaranteeName.ReadOnly = True
        Me.lblBankGuaranteeName.ReferenceFieldDesc = Nothing
        Me.lblBankGuaranteeName.ReferenceFieldName = Nothing
        Me.lblBankGuaranteeName.ReferenceTableName = Nothing
        Me.lblBankGuaranteeName.Size = New System.Drawing.Size(326, 20)
        Me.lblBankGuaranteeName.TabIndex = 21
        Me.lblBankGuaranteeName.TabStop = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 210)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel2.TabIndex = 118
        Me.MyLabel2.Text = "Create Security"
        '
        'fndCreateSecurity
        '
        Me.fndCreateSecurity.CalculationExpression = Nothing
        Me.fndCreateSecurity.FieldCode = Nothing
        Me.fndCreateSecurity.FieldDesc = Nothing
        Me.fndCreateSecurity.FieldMaxLength = 0
        Me.fndCreateSecurity.FieldName = Nothing
        Me.fndCreateSecurity.isCalculatedField = False
        Me.fndCreateSecurity.IsSourceFromTable = False
        Me.fndCreateSecurity.IsSourceFromValueList = False
        Me.fndCreateSecurity.IsUnique = False
        Me.fndCreateSecurity.Location = New System.Drawing.Point(127, 207)
        Me.fndCreateSecurity.MendatroryField = True
        Me.fndCreateSecurity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCreateSecurity.MyLinkLable1 = Nothing
        Me.fndCreateSecurity.MyLinkLable2 = Nothing
        Me.fndCreateSecurity.MyReadOnly = False
        Me.fndCreateSecurity.MyShowMasterFormButton = False
        Me.fndCreateSecurity.Name = "fndCreateSecurity"
        Me.fndCreateSecurity.ReferenceFieldDesc = Nothing
        Me.fndCreateSecurity.ReferenceFieldName = Nothing
        Me.fndCreateSecurity.ReferenceTableName = Nothing
        Me.fndCreateSecurity.Size = New System.Drawing.Size(143, 19)
        Me.fndCreateSecurity.TabIndex = 9
        Me.fndCreateSecurity.Value = ""
        '
        'lblCreateSecurityName
        '
        Me.lblCreateSecurityName.CalculationExpression = Nothing
        Me.lblCreateSecurityName.FieldCode = Nothing
        Me.lblCreateSecurityName.FieldDesc = Nothing
        Me.lblCreateSecurityName.FieldMaxLength = 0
        Me.lblCreateSecurityName.FieldName = Nothing
        Me.lblCreateSecurityName.isCalculatedField = False
        Me.lblCreateSecurityName.IsSourceFromTable = False
        Me.lblCreateSecurityName.IsSourceFromValueList = False
        Me.lblCreateSecurityName.IsUnique = False
        Me.lblCreateSecurityName.Location = New System.Drawing.Point(274, 207)
        Me.lblCreateSecurityName.MendatroryField = False
        Me.lblCreateSecurityName.MyLinkLable1 = Nothing
        Me.lblCreateSecurityName.MyLinkLable2 = Nothing
        Me.lblCreateSecurityName.Name = "lblCreateSecurityName"
        Me.lblCreateSecurityName.ReadOnly = True
        Me.lblCreateSecurityName.ReferenceFieldDesc = Nothing
        Me.lblCreateSecurityName.ReferenceFieldName = Nothing
        Me.lblCreateSecurityName.ReferenceTableName = Nothing
        Me.lblCreateSecurityName.Size = New System.Drawing.Size(326, 20)
        Me.lblCreateSecurityName.TabIndex = 19
        Me.lblCreateSecurityName.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 185)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel1.TabIndex = 115
        Me.MyLabel1.Text = "Security"
        '
        'FndSecurity
        '
        Me.FndSecurity.CalculationExpression = Nothing
        Me.FndSecurity.FieldCode = Nothing
        Me.FndSecurity.FieldDesc = Nothing
        Me.FndSecurity.FieldMaxLength = 0
        Me.FndSecurity.FieldName = Nothing
        Me.FndSecurity.isCalculatedField = False
        Me.FndSecurity.IsSourceFromTable = False
        Me.FndSecurity.IsSourceFromValueList = False
        Me.FndSecurity.IsUnique = False
        Me.FndSecurity.Location = New System.Drawing.Point(127, 183)
        Me.FndSecurity.MendatroryField = True
        Me.FndSecurity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndSecurity.MyLinkLable1 = Nothing
        Me.FndSecurity.MyLinkLable2 = Nothing
        Me.FndSecurity.MyReadOnly = False
        Me.FndSecurity.MyShowMasterFormButton = False
        Me.FndSecurity.Name = "FndSecurity"
        Me.FndSecurity.ReferenceFieldDesc = Nothing
        Me.FndSecurity.ReferenceFieldName = Nothing
        Me.FndSecurity.ReferenceTableName = Nothing
        Me.FndSecurity.Size = New System.Drawing.Size(143, 19)
        Me.FndSecurity.TabIndex = 8
        Me.FndSecurity.Value = ""
        '
        'lblSecurityName
        '
        Me.lblSecurityName.CalculationExpression = Nothing
        Me.lblSecurityName.FieldCode = Nothing
        Me.lblSecurityName.FieldDesc = Nothing
        Me.lblSecurityName.FieldMaxLength = 0
        Me.lblSecurityName.FieldName = Nothing
        Me.lblSecurityName.isCalculatedField = False
        Me.lblSecurityName.IsSourceFromTable = False
        Me.lblSecurityName.IsSourceFromValueList = False
        Me.lblSecurityName.IsUnique = False
        Me.lblSecurityName.Location = New System.Drawing.Point(274, 183)
        Me.lblSecurityName.MendatroryField = False
        Me.lblSecurityName.MyLinkLable1 = Nothing
        Me.lblSecurityName.MyLinkLable2 = Nothing
        Me.lblSecurityName.Name = "lblSecurityName"
        Me.lblSecurityName.ReadOnly = True
        Me.lblSecurityName.ReferenceFieldDesc = Nothing
        Me.lblSecurityName.ReferenceFieldName = Nothing
        Me.lblSecurityName.ReferenceTableName = Nothing
        Me.lblSecurityName.Size = New System.Drawing.Size(326, 20)
        Me.lblSecurityName.TabIndex = 17
        Me.lblSecurityName.TabStop = False
        '
        'lblExchangeGain
        '
        Me.lblExchangeGain.FieldName = Nothing
        Me.lblExchangeGain.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExchangeGain.Location = New System.Drawing.Point(6, 162)
        Me.lblExchangeGain.Name = "lblExchangeGain"
        Me.lblExchangeGain.Size = New System.Drawing.Size(84, 16)
        Me.lblExchangeGain.TabIndex = 107
        Me.lblExchangeGain.Text = "Exchange Gain"
        '
        'lblExchangeLoss
        '
        Me.lblExchangeLoss.FieldName = Nothing
        Me.lblExchangeLoss.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExchangeLoss.Location = New System.Drawing.Point(6, 142)
        Me.lblExchangeLoss.Name = "lblExchangeLoss"
        Me.lblExchangeLoss.Size = New System.Drawing.Size(84, 16)
        Me.lblExchangeLoss.TabIndex = 113
        Me.lblExchangeLoss.Text = "Exchange Loss"
        '
        'fndExchangeGain
        '
        Me.fndExchangeGain.CalculationExpression = Nothing
        Me.fndExchangeGain.FieldCode = Nothing
        Me.fndExchangeGain.FieldDesc = Nothing
        Me.fndExchangeGain.FieldMaxLength = 0
        Me.fndExchangeGain.FieldName = Nothing
        Me.fndExchangeGain.isCalculatedField = False
        Me.fndExchangeGain.IsSourceFromTable = False
        Me.fndExchangeGain.IsSourceFromValueList = False
        Me.fndExchangeGain.IsUnique = False
        Me.fndExchangeGain.Location = New System.Drawing.Point(127, 160)
        Me.fndExchangeGain.MendatroryField = False
        Me.fndExchangeGain.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndExchangeGain.MyLinkLable1 = Nothing
        Me.fndExchangeGain.MyLinkLable2 = Nothing
        Me.fndExchangeGain.MyReadOnly = False
        Me.fndExchangeGain.MyShowMasterFormButton = False
        Me.fndExchangeGain.Name = "fndExchangeGain"
        Me.fndExchangeGain.ReferenceFieldDesc = Nothing
        Me.fndExchangeGain.ReferenceFieldName = Nothing
        Me.fndExchangeGain.ReferenceTableName = Nothing
        Me.fndExchangeGain.Size = New System.Drawing.Size(143, 19)
        Me.fndExchangeGain.TabIndex = 7
        Me.fndExchangeGain.Value = ""
        '
        'lblExchangeGainName
        '
        Me.lblExchangeGainName.CalculationExpression = Nothing
        Me.lblExchangeGainName.FieldCode = Nothing
        Me.lblExchangeGainName.FieldDesc = Nothing
        Me.lblExchangeGainName.FieldMaxLength = 0
        Me.lblExchangeGainName.FieldName = Nothing
        Me.lblExchangeGainName.isCalculatedField = False
        Me.lblExchangeGainName.IsSourceFromTable = False
        Me.lblExchangeGainName.IsSourceFromValueList = False
        Me.lblExchangeGainName.IsUnique = False
        Me.lblExchangeGainName.Location = New System.Drawing.Point(274, 160)
        Me.lblExchangeGainName.MendatroryField = False
        Me.lblExchangeGainName.MyLinkLable1 = Nothing
        Me.lblExchangeGainName.MyLinkLable2 = Nothing
        Me.lblExchangeGainName.Name = "lblExchangeGainName"
        Me.lblExchangeGainName.ReadOnly = True
        Me.lblExchangeGainName.ReferenceFieldDesc = Nothing
        Me.lblExchangeGainName.ReferenceFieldName = Nothing
        Me.lblExchangeGainName.ReferenceTableName = Nothing
        Me.lblExchangeGainName.Size = New System.Drawing.Size(326, 20)
        Me.lblExchangeGainName.TabIndex = 15
        Me.lblExchangeGainName.TabStop = False
        '
        'fndExchangeLoss
        '
        Me.fndExchangeLoss.CalculationExpression = Nothing
        Me.fndExchangeLoss.FieldCode = Nothing
        Me.fndExchangeLoss.FieldDesc = Nothing
        Me.fndExchangeLoss.FieldMaxLength = 0
        Me.fndExchangeLoss.FieldName = Nothing
        Me.fndExchangeLoss.isCalculatedField = False
        Me.fndExchangeLoss.IsSourceFromTable = False
        Me.fndExchangeLoss.IsSourceFromValueList = False
        Me.fndExchangeLoss.IsUnique = False
        Me.fndExchangeLoss.Location = New System.Drawing.Point(127, 140)
        Me.fndExchangeLoss.MendatroryField = False
        Me.fndExchangeLoss.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndExchangeLoss.MyLinkLable1 = Nothing
        Me.fndExchangeLoss.MyLinkLable2 = Nothing
        Me.fndExchangeLoss.MyReadOnly = False
        Me.fndExchangeLoss.MyShowMasterFormButton = False
        Me.fndExchangeLoss.Name = "fndExchangeLoss"
        Me.fndExchangeLoss.ReferenceFieldDesc = Nothing
        Me.fndExchangeLoss.ReferenceFieldName = Nothing
        Me.fndExchangeLoss.ReferenceTableName = Nothing
        Me.fndExchangeLoss.Size = New System.Drawing.Size(143, 19)
        Me.fndExchangeLoss.TabIndex = 6
        Me.fndExchangeLoss.Value = ""
        '
        'lblExchangeLossName
        '
        Me.lblExchangeLossName.CalculationExpression = Nothing
        Me.lblExchangeLossName.FieldCode = Nothing
        Me.lblExchangeLossName.FieldDesc = Nothing
        Me.lblExchangeLossName.FieldMaxLength = 0
        Me.lblExchangeLossName.FieldName = Nothing
        Me.lblExchangeLossName.isCalculatedField = False
        Me.lblExchangeLossName.IsSourceFromTable = False
        Me.lblExchangeLossName.IsSourceFromValueList = False
        Me.lblExchangeLossName.IsUnique = False
        Me.lblExchangeLossName.Location = New System.Drawing.Point(274, 139)
        Me.lblExchangeLossName.MendatroryField = False
        Me.lblExchangeLossName.MyLinkLable1 = Nothing
        Me.lblExchangeLossName.MyLinkLable2 = Nothing
        Me.lblExchangeLossName.Name = "lblExchangeLossName"
        Me.lblExchangeLossName.ReadOnly = True
        Me.lblExchangeLossName.ReferenceFieldDesc = Nothing
        Me.lblExchangeLossName.ReferenceFieldName = Nothing
        Me.lblExchangeLossName.ReferenceTableName = Nothing
        Me.lblExchangeLossName.Size = New System.Drawing.Size(326, 20)
        Me.lblExchangeLossName.TabIndex = 13
        Me.lblExchangeLossName.TabStop = False
        '
        'lblCurrencyName
        '
        Me.lblCurrencyName.CalculationExpression = Nothing
        Me.lblCurrencyName.FieldCode = Nothing
        Me.lblCurrencyName.FieldDesc = Nothing
        Me.lblCurrencyName.FieldMaxLength = 0
        Me.lblCurrencyName.FieldName = Nothing
        Me.lblCurrencyName.isCalculatedField = False
        Me.lblCurrencyName.IsSourceFromTable = False
        Me.lblCurrencyName.IsSourceFromValueList = False
        Me.lblCurrencyName.IsUnique = False
        Me.lblCurrencyName.Location = New System.Drawing.Point(274, 117)
        Me.lblCurrencyName.MendatroryField = False
        Me.lblCurrencyName.MyLinkLable1 = Me.lblcontainer
        Me.lblCurrencyName.MyLinkLable2 = Nothing
        Me.lblCurrencyName.Name = "lblCurrencyName"
        Me.lblCurrencyName.ReadOnly = True
        Me.lblCurrencyName.ReferenceFieldDesc = Nothing
        Me.lblCurrencyName.ReferenceFieldName = Nothing
        Me.lblCurrencyName.ReferenceTableName = Nothing
        Me.lblCurrencyName.Size = New System.Drawing.Size(326, 20)
        Me.lblCurrencyName.TabIndex = 11
        Me.lblCurrencyName.TabStop = False
        '
        'lblcontainer
        '
        Me.lblcontainer.FieldName = Nothing
        Me.lblcontainer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcontainer.Location = New System.Drawing.Point(6, 98)
        Me.lblcontainer.Name = "lblcontainer"
        Me.lblcontainer.Size = New System.Drawing.Size(97, 16)
        Me.lblcontainer.TabIndex = 11
        Me.lblcontainer.Text = "Container Deposit"
        '
        'fndBaseCurrency
        '
        Me.fndBaseCurrency.CalculationExpression = Nothing
        Me.fndBaseCurrency.FieldCode = Nothing
        Me.fndBaseCurrency.FieldDesc = Nothing
        Me.fndBaseCurrency.FieldMaxLength = 0
        Me.fndBaseCurrency.FieldName = Nothing
        Me.fndBaseCurrency.isCalculatedField = False
        Me.fndBaseCurrency.IsSourceFromTable = False
        Me.fndBaseCurrency.IsSourceFromValueList = False
        Me.fndBaseCurrency.IsUnique = False
        Me.fndBaseCurrency.Location = New System.Drawing.Point(127, 119)
        Me.fndBaseCurrency.MendatroryField = False
        Me.fndBaseCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBaseCurrency.MyLinkLable1 = Nothing
        Me.fndBaseCurrency.MyLinkLable2 = Nothing
        Me.fndBaseCurrency.MyReadOnly = False
        Me.fndBaseCurrency.MyShowMasterFormButton = False
        Me.fndBaseCurrency.Name = "fndBaseCurrency"
        Me.fndBaseCurrency.ReferenceFieldDesc = Nothing
        Me.fndBaseCurrency.ReferenceFieldName = Nothing
        Me.fndBaseCurrency.ReferenceTableName = Nothing
        Me.fndBaseCurrency.Size = New System.Drawing.Size(143, 19)
        Me.fndBaseCurrency.TabIndex = 5
        Me.fndBaseCurrency.Value = ""
        '
        'lblBaseCurrency
        '
        Me.lblBaseCurrency.FieldName = Nothing
        Me.lblBaseCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseCurrency.Location = New System.Drawing.Point(6, 120)
        Me.lblBaseCurrency.Name = "lblBaseCurrency"
        Me.lblBaseCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblBaseCurrency.TabIndex = 106
        Me.lblBaseCurrency.Text = "Currency"
        '
        'fndcontainer
        '
        Me.fndcontainer.CalculationExpression = Nothing
        Me.fndcontainer.FieldCode = Nothing
        Me.fndcontainer.FieldDesc = Nothing
        Me.fndcontainer.FieldMaxLength = 0
        Me.fndcontainer.FieldName = Nothing
        Me.fndcontainer.isCalculatedField = False
        Me.fndcontainer.IsSourceFromTable = False
        Me.fndcontainer.IsSourceFromValueList = False
        Me.fndcontainer.IsUnique = False
        Me.fndcontainer.Location = New System.Drawing.Point(127, 97)
        Me.fndcontainer.MendatroryField = False
        Me.fndcontainer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcontainer.MyLinkLable1 = Me.lblcontainer
        Me.fndcontainer.MyLinkLable2 = Nothing
        Me.fndcontainer.MyReadOnly = False
        Me.fndcontainer.MyShowMasterFormButton = False
        Me.fndcontainer.Name = "fndcontainer"
        Me.fndcontainer.ReferenceFieldDesc = Nothing
        Me.fndcontainer.ReferenceFieldName = Nothing
        Me.fndcontainer.ReferenceTableName = Nothing
        Me.fndcontainer.Size = New System.Drawing.Size(143, 19)
        Me.fndcontainer.TabIndex = 4
        Me.fndcontainer.Value = ""
        '
        'fndwriteoffs
        '
        Me.fndwriteoffs.CalculationExpression = Nothing
        Me.fndwriteoffs.FieldCode = Nothing
        Me.fndwriteoffs.FieldDesc = Nothing
        Me.fndwriteoffs.FieldMaxLength = 0
        Me.fndwriteoffs.FieldName = Nothing
        Me.fndwriteoffs.isCalculatedField = False
        Me.fndwriteoffs.IsSourceFromTable = False
        Me.fndwriteoffs.IsSourceFromValueList = False
        Me.fndwriteoffs.IsUnique = False
        Me.fndwriteoffs.Location = New System.Drawing.Point(127, 75)
        Me.fndwriteoffs.MendatroryField = True
        Me.fndwriteoffs.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndwriteoffs.MyLinkLable1 = Me.rdlblWriteoffs
        Me.fndwriteoffs.MyLinkLable2 = Nothing
        Me.fndwriteoffs.MyReadOnly = False
        Me.fndwriteoffs.MyShowMasterFormButton = False
        Me.fndwriteoffs.Name = "fndwriteoffs"
        Me.fndwriteoffs.ReferenceFieldDesc = Nothing
        Me.fndwriteoffs.ReferenceFieldName = Nothing
        Me.fndwriteoffs.ReferenceTableName = Nothing
        Me.fndwriteoffs.Size = New System.Drawing.Size(143, 19)
        Me.fndwriteoffs.TabIndex = 3
        Me.fndwriteoffs.Value = ""
        '
        'rdlblWriteoffs
        '
        Me.rdlblWriteoffs.FieldName = Nothing
        Me.rdlblWriteoffs.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblWriteoffs.Location = New System.Drawing.Point(6, 77)
        Me.rdlblWriteoffs.Name = "rdlblWriteoffs"
        Me.rdlblWriteoffs.Size = New System.Drawing.Size(63, 16)
        Me.rdlblWriteoffs.TabIndex = 8
        Me.rdlblWriteoffs.Text = "Write - Offs"
        '
        'fndadvance
        '
        Me.fndadvance.CalculationExpression = Nothing
        Me.fndadvance.FieldCode = Nothing
        Me.fndadvance.FieldDesc = Nothing
        Me.fndadvance.FieldMaxLength = 0
        Me.fndadvance.FieldName = Nothing
        Me.fndadvance.isCalculatedField = False
        Me.fndadvance.IsSourceFromTable = False
        Me.fndadvance.IsSourceFromValueList = False
        Me.fndadvance.IsUnique = False
        Me.fndadvance.Location = New System.Drawing.Point(127, 53)
        Me.fndadvance.MendatroryField = False
        Me.fndadvance.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndadvance.MyLinkLable1 = Me.rdlblAdvance
        Me.fndadvance.MyLinkLable2 = Nothing
        Me.fndadvance.MyReadOnly = False
        Me.fndadvance.MyShowMasterFormButton = False
        Me.fndadvance.Name = "fndadvance"
        Me.fndadvance.ReferenceFieldDesc = Nothing
        Me.fndadvance.ReferenceFieldName = Nothing
        Me.fndadvance.ReferenceTableName = Nothing
        Me.fndadvance.Size = New System.Drawing.Size(143, 19)
        Me.fndadvance.TabIndex = 2
        Me.fndadvance.Value = ""
        '
        'rdlblAdvance
        '
        Me.rdlblAdvance.FieldName = Nothing
        Me.rdlblAdvance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblAdvance.Location = New System.Drawing.Point(6, 54)
        Me.rdlblAdvance.Name = "rdlblAdvance"
        Me.rdlblAdvance.Size = New System.Drawing.Size(50, 16)
        Me.rdlblAdvance.TabIndex = 9
        Me.rdlblAdvance.Text = "Advance"
        '
        'fndrecieptdiscount
        '
        Me.fndrecieptdiscount.CalculationExpression = Nothing
        Me.fndrecieptdiscount.FieldCode = Nothing
        Me.fndrecieptdiscount.FieldDesc = Nothing
        Me.fndrecieptdiscount.FieldMaxLength = 0
        Me.fndrecieptdiscount.FieldName = Nothing
        Me.fndrecieptdiscount.isCalculatedField = False
        Me.fndrecieptdiscount.IsSourceFromTable = False
        Me.fndrecieptdiscount.IsSourceFromValueList = False
        Me.fndrecieptdiscount.IsUnique = False
        Me.fndrecieptdiscount.Location = New System.Drawing.Point(127, 30)
        Me.fndrecieptdiscount.MendatroryField = True
        Me.fndrecieptdiscount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndrecieptdiscount.MyLinkLable1 = Me.rdlblrecieptdiscount
        Me.fndrecieptdiscount.MyLinkLable2 = Nothing
        Me.fndrecieptdiscount.MyReadOnly = False
        Me.fndrecieptdiscount.MyShowMasterFormButton = False
        Me.fndrecieptdiscount.Name = "fndrecieptdiscount"
        Me.fndrecieptdiscount.ReferenceFieldDesc = Nothing
        Me.fndrecieptdiscount.ReferenceFieldName = Nothing
        Me.fndrecieptdiscount.ReferenceTableName = Nothing
        Me.fndrecieptdiscount.Size = New System.Drawing.Size(143, 19)
        Me.fndrecieptdiscount.TabIndex = 1
        Me.fndrecieptdiscount.Value = ""
        '
        'rdlblrecieptdiscount
        '
        Me.rdlblrecieptdiscount.FieldName = Nothing
        Me.rdlblrecieptdiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblrecieptdiscount.Location = New System.Drawing.Point(6, 31)
        Me.rdlblrecieptdiscount.Name = "rdlblrecieptdiscount"
        Me.rdlblrecieptdiscount.Size = New System.Drawing.Size(97, 16)
        Me.rdlblrecieptdiscount.TabIndex = 10
        Me.rdlblrecieptdiscount.Text = "Reciept Discounts"
        '
        'fndrecisvablecontrol
        '
        Me.fndrecisvablecontrol.CalculationExpression = Nothing
        Me.fndrecisvablecontrol.FieldCode = Nothing
        Me.fndrecisvablecontrol.FieldDesc = Nothing
        Me.fndrecisvablecontrol.FieldMaxLength = 0
        Me.fndrecisvablecontrol.FieldName = Nothing
        Me.fndrecisvablecontrol.isCalculatedField = False
        Me.fndrecisvablecontrol.IsSourceFromTable = False
        Me.fndrecisvablecontrol.IsSourceFromValueList = False
        Me.fndrecisvablecontrol.IsUnique = False
        Me.fndrecisvablecontrol.Location = New System.Drawing.Point(127, 7)
        Me.fndrecisvablecontrol.MendatroryField = True
        Me.fndrecisvablecontrol.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndrecisvablecontrol.MyLinkLable1 = Me.rdlblrecievablescontrol
        Me.fndrecisvablecontrol.MyLinkLable2 = Nothing
        Me.fndrecisvablecontrol.MyReadOnly = False
        Me.fndrecisvablecontrol.MyShowMasterFormButton = False
        Me.fndrecisvablecontrol.Name = "fndrecisvablecontrol"
        Me.fndrecisvablecontrol.ReferenceFieldDesc = Nothing
        Me.fndrecisvablecontrol.ReferenceFieldName = Nothing
        Me.fndrecisvablecontrol.ReferenceTableName = Nothing
        Me.fndrecisvablecontrol.Size = New System.Drawing.Size(143, 19)
        Me.fndrecisvablecontrol.TabIndex = 0
        Me.fndrecisvablecontrol.Value = ""
        '
        'rdlblrecievablescontrol
        '
        Me.rdlblrecievablescontrol.FieldName = Nothing
        Me.rdlblrecievablescontrol.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblrecievablescontrol.Location = New System.Drawing.Point(6, 8)
        Me.rdlblrecievablescontrol.Name = "rdlblrecievablescontrol"
        Me.rdlblrecievablescontrol.Size = New System.Drawing.Size(80, 16)
        Me.rdlblrecievablescontrol.TabIndex = 11
        Me.rdlblrecievablescontrol.Text = "Debtor Control"
        '
        'txtcontainer
        '
        Me.txtcontainer.CalculationExpression = Nothing
        Me.txtcontainer.FieldCode = Nothing
        Me.txtcontainer.FieldDesc = Nothing
        Me.txtcontainer.FieldMaxLength = 0
        Me.txtcontainer.FieldName = Nothing
        Me.txtcontainer.isCalculatedField = False
        Me.txtcontainer.IsSourceFromTable = False
        Me.txtcontainer.IsSourceFromValueList = False
        Me.txtcontainer.IsUnique = False
        Me.txtcontainer.Location = New System.Drawing.Point(274, 96)
        Me.txtcontainer.MendatroryField = False
        Me.txtcontainer.MyLinkLable1 = Me.lblcontainer
        Me.txtcontainer.MyLinkLable2 = Nothing
        Me.txtcontainer.Name = "txtcontainer"
        Me.txtcontainer.ReadOnly = True
        Me.txtcontainer.ReferenceFieldDesc = Nothing
        Me.txtcontainer.ReferenceFieldName = Nothing
        Me.txtcontainer.ReferenceTableName = Nothing
        Me.txtcontainer.Size = New System.Drawing.Size(326, 20)
        Me.txtcontainer.TabIndex = 9
        Me.txtcontainer.TabStop = False
        '
        'rdtxtwriteoff
        '
        Me.rdtxtwriteoff.CalculationExpression = Nothing
        Me.rdtxtwriteoff.FieldCode = Nothing
        Me.rdtxtwriteoff.FieldDesc = Nothing
        Me.rdtxtwriteoff.FieldMaxLength = 0
        Me.rdtxtwriteoff.FieldName = Nothing
        Me.rdtxtwriteoff.isCalculatedField = False
        Me.rdtxtwriteoff.IsSourceFromTable = False
        Me.rdtxtwriteoff.IsSourceFromValueList = False
        Me.rdtxtwriteoff.IsUnique = False
        Me.rdtxtwriteoff.Location = New System.Drawing.Point(274, 74)
        Me.rdtxtwriteoff.MendatroryField = False
        Me.rdtxtwriteoff.MyLinkLable1 = Me.rdlblWriteoffs
        Me.rdtxtwriteoff.MyLinkLable2 = Nothing
        Me.rdtxtwriteoff.Name = "rdtxtwriteoff"
        Me.rdtxtwriteoff.ReadOnly = True
        Me.rdtxtwriteoff.ReferenceFieldDesc = Nothing
        Me.rdtxtwriteoff.ReferenceFieldName = Nothing
        Me.rdtxtwriteoff.ReferenceTableName = Nothing
        Me.rdtxtwriteoff.Size = New System.Drawing.Size(326, 20)
        Me.rdtxtwriteoff.TabIndex = 7
        Me.rdtxtwriteoff.TabStop = False
        '
        'rdtxtadvance
        '
        Me.rdtxtadvance.CalculationExpression = Nothing
        Me.rdtxtadvance.FieldCode = Nothing
        Me.rdtxtadvance.FieldDesc = Nothing
        Me.rdtxtadvance.FieldMaxLength = 0
        Me.rdtxtadvance.FieldName = Nothing
        Me.rdtxtadvance.isCalculatedField = False
        Me.rdtxtadvance.IsSourceFromTable = False
        Me.rdtxtadvance.IsSourceFromValueList = False
        Me.rdtxtadvance.IsUnique = False
        Me.rdtxtadvance.Location = New System.Drawing.Point(274, 52)
        Me.rdtxtadvance.MendatroryField = False
        Me.rdtxtadvance.MyLinkLable1 = Me.rdlblAdvance
        Me.rdtxtadvance.MyLinkLable2 = Nothing
        Me.rdtxtadvance.Name = "rdtxtadvance"
        Me.rdtxtadvance.ReadOnly = True
        Me.rdtxtadvance.ReferenceFieldDesc = Nothing
        Me.rdtxtadvance.ReferenceFieldName = Nothing
        Me.rdtxtadvance.ReferenceTableName = Nothing
        Me.rdtxtadvance.Size = New System.Drawing.Size(326, 20)
        Me.rdtxtadvance.TabIndex = 5
        Me.rdtxtadvance.TabStop = False
        '
        'rdtxtrecieptdicount
        '
        Me.rdtxtrecieptdicount.CalculationExpression = Nothing
        Me.rdtxtrecieptdicount.FieldCode = Nothing
        Me.rdtxtrecieptdicount.FieldDesc = Nothing
        Me.rdtxtrecieptdicount.FieldMaxLength = 0
        Me.rdtxtrecieptdicount.FieldName = Nothing
        Me.rdtxtrecieptdicount.isCalculatedField = False
        Me.rdtxtrecieptdicount.IsSourceFromTable = False
        Me.rdtxtrecieptdicount.IsSourceFromValueList = False
        Me.rdtxtrecieptdicount.IsUnique = False
        Me.rdtxtrecieptdicount.Location = New System.Drawing.Point(274, 29)
        Me.rdtxtrecieptdicount.MendatroryField = False
        Me.rdtxtrecieptdicount.MyLinkLable1 = Me.rdlblrecieptdiscount
        Me.rdtxtrecieptdicount.MyLinkLable2 = Nothing
        Me.rdtxtrecieptdicount.Name = "rdtxtrecieptdicount"
        Me.rdtxtrecieptdicount.ReadOnly = True
        Me.rdtxtrecieptdicount.ReferenceFieldDesc = Nothing
        Me.rdtxtrecieptdicount.ReferenceFieldName = Nothing
        Me.rdtxtrecieptdicount.ReferenceTableName = Nothing
        Me.rdtxtrecieptdicount.Size = New System.Drawing.Size(326, 20)
        Me.rdtxtrecieptdicount.TabIndex = 3
        Me.rdtxtrecieptdicount.TabStop = False
        '
        'rdtxtrecievablecontrol
        '
        Me.rdtxtrecievablecontrol.CalculationExpression = Nothing
        Me.rdtxtrecievablecontrol.FieldCode = Nothing
        Me.rdtxtrecievablecontrol.FieldDesc = Nothing
        Me.rdtxtrecievablecontrol.FieldMaxLength = 0
        Me.rdtxtrecievablecontrol.FieldName = Nothing
        Me.rdtxtrecievablecontrol.isCalculatedField = False
        Me.rdtxtrecievablecontrol.IsSourceFromTable = False
        Me.rdtxtrecievablecontrol.IsSourceFromValueList = False
        Me.rdtxtrecievablecontrol.IsUnique = False
        Me.rdtxtrecievablecontrol.Location = New System.Drawing.Point(274, 6)
        Me.rdtxtrecievablecontrol.MendatroryField = False
        Me.rdtxtrecievablecontrol.MyLinkLable1 = Me.rdlblrecievablescontrol
        Me.rdtxtrecievablecontrol.MyLinkLable2 = Nothing
        Me.rdtxtrecievablecontrol.Name = "rdtxtrecievablecontrol"
        Me.rdtxtrecievablecontrol.ReadOnly = True
        Me.rdtxtrecievablecontrol.ReferenceFieldDesc = Nothing
        Me.rdtxtrecievablecontrol.ReferenceFieldName = Nothing
        Me.rdtxtrecievablecontrol.ReferenceTableName = Nothing
        Me.rdtxtrecievablecontrol.Size = New System.Drawing.Size(326, 20)
        Me.rdtxtrecievablecontrol.TabIndex = 1
        Me.rdtxtrecievablecontrol.TabStop = False
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(701, 4)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(102, 4)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(80, 18)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.TabStop = False
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(19, 4)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadScrollablePanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(802, 707)
        Me.SplitContainer1.SplitterDistance = 678
        Me.SplitContainer1.TabIndex = 0
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(188, 4)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(82, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "&History"
        '
        'RadScrollablePanel1
        '
        Me.RadScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadScrollablePanel1.Name = "RadScrollablePanel1"
        '
        'RadScrollablePanel1.PanelContainer
        '
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.rdgpbxcustomeraccountset)
        Me.RadScrollablePanel1.PanelContainer.Size = New System.Drawing.Size(800, 676)
        Me.RadScrollablePanel1.Size = New System.Drawing.Size(802, 678)
        Me.RadScrollablePanel1.TabIndex = 1
        '
        'frmCustomerAccountSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 727)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmCustomerAccountSet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Account Set"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxgeneralledgeraccounts.ResumeLayout(False)
        Me.rdgrpbxgeneralledgeraccounts.PerformLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRateDifference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerSecurityOpeningClearingAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerOpeningClearingAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLeakageDed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPenaltyCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubsidyAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubsidy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankChargesOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblForeignBankCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtloss_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtgian_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcongnmnt_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtgsoc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccount2Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccount1Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankGuaranteeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreateSecurityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSecurityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExchangeGain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExchangeLoss, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExchangeGainName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExchangeLossName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrencyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcontainer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblWriteoffs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblrecieptdiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblrecievablescontrol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcontainer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtwriteoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtadvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtrecieptdicount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtrecievablecontrol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.PanelContainer.ResumeLayout(False)
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdtxtdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdgrpbxgeneralledgeraccounts As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdtxtwriteoff As common.Controls.MyTextBox
    Friend WithEvents rdtxtadvance As common.Controls.MyTextBox
    Friend WithEvents rdtxtrecieptdicount As common.Controls.MyTextBox
    Friend WithEvents rdtxtrecievablecontrol As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents txtcontainer As common.Controls.MyTextBox
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents rdlblWriteoffs As common.Controls.MyLabel
    Friend WithEvents rdlblAdvance As common.Controls.MyLabel
    Friend WithEvents rdlblrecieptdiscount As common.Controls.MyLabel
    Friend WithEvents rdlblrecievablescontrol As common.Controls.MyLabel
    Friend WithEvents lblcontainer As common.Controls.MyLabel
    Friend WithEvents fndrecisvablecontrol As common.UserControls.txtFinder
    Friend WithEvents fndrecieptdiscount As common.UserControls.txtFinder
    Friend WithEvents fndadvance As common.UserControls.txtFinder
    Friend WithEvents fndwriteoffs As common.UserControls.txtFinder
    Friend WithEvents fndcontainer As common.UserControls.txtFinder
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndBaseCurrency As common.UserControls.txtFinder
    Friend WithEvents lblBaseCurrency As common.Controls.MyLabel
    Friend WithEvents lblCurrencyName As common.Controls.MyTextBox
    Friend WithEvents lblExchangeGain As common.Controls.MyLabel
    Friend WithEvents lblExchangeLoss As common.Controls.MyLabel
    Friend WithEvents fndExchangeGain As common.UserControls.txtFinder
    Friend WithEvents lblExchangeGainName As common.Controls.MyTextBox
    Friend WithEvents fndExchangeLoss As common.UserControls.txtFinder
    Friend WithEvents lblExchangeLossName As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndAccount1 As common.UserControls.txtFinder
    Friend WithEvents lblAccount1Name As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndBankGuarantee As common.UserControls.txtFinder
    Friend WithEvents lblBankGuaranteeName As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndCreateSecurity As common.UserControls.txtFinder
    Friend WithEvents lblCreateSecurityName As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents FndSecurity As common.UserControls.txtFinder
    Friend WithEvents lblSecurityName As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndAccount2 As common.UserControls.txtFinder
    Friend WithEvents lblAccount2Name As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtconsignmnt As common.UserControls.txtFinder
    Friend WithEvents txtcongnmnt_name As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtgsoc As common.UserControls.txtFinder
    Friend WithEvents txtgsoc_name As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtloss As common.UserControls.txtFinder
    Friend WithEvents txtloss_name As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtgain As common.UserControls.txtFinder
    Friend WithEvents txtgian_name As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents TxtBankChargesOther As common.UserControls.txtFinder
    Friend WithEvents lblBankChargesOther As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents TxtForeignBankCharges As common.UserControls.txtFinder
    Friend WithEvents lblForeignBankCharges As common.Controls.MyTextBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSubsidyAccount As common.Controls.MyLabel
    Friend WithEvents txtSubsidyAccount As common.UserControls.txtFinder
    Friend WithEvents txtSubsidy As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents FndPenaltyCharges As common.UserControls.txtFinder
    Friend WithEvents lblPenaltyCharges As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents fndLeakageDeduction As common.UserControls.txtFinder
    Friend WithEvents txtLeakageDed As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents fndCustomerSecurityOpeningClearingAC As common.UserControls.txtFinder
    Friend WithEvents lblCustomerSecurityOpeningClearingAC As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents fndCustomerOpeningClearingAC As common.UserControls.txtFinder
    Friend WithEvents lblCustomerOpeningClearingAC As common.Controls.MyTextBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents FndRateDifference As common.UserControls.txtFinder
    Friend WithEvents lblRateDifference As common.Controls.MyTextBox
    Friend WithEvents RadScrollablePanel1 As RadScrollablePanel
End Class

