<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankMaster
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
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.lblbank = New common.Controls.MyLabel()
        Me.lbldes = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.bankimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.bankexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.banlclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.bankstab = New Telerik.WinControls.UI.RadPageView()
        Me.bankstabprofile = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkUnpaid = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtBankGroup = New common.UserControls.txtFinder()
        Me.lblBankGroup = New common.Controls.MyLabel()
        Me.chkSettlementBankForAD = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkProvisionBank = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblBankClearingAcount = New common.Controls.MyTextBox()
        Me.fndBankOpeningClearingAcount = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.fndBranchName = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.chkDefaultBank = New Telerik.WinControls.UI.RadCheckBox()
        Me.pnlGatewayType = New System.Windows.Forms.Panel()
        Me.cboGetewayType = New common.Controls.MyComboBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtswiftcode = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.TxtIbanno = New common.Controls.MyTextBox()
        Me.fndtransferclearing = New common.UserControls.txtFinder()
        Me.txttransferclearing = New common.Controls.MyTextBox()
        Me.rdlbltransferclearing = New common.Controls.MyLabel()
        Me.fndSubAcc = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtSubAcc = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFDPer = New common.MyNumBox()
        Me.txtLCCreditLimit = New common.MyNumBox()
        Me.lblChequeValidity = New common.Controls.MyLabel()
        Me.fndcredit = New common.UserControls.txtFinder()
        Me.lblcredit = New common.Controls.MyLabel()
        Me.txtChequeValidity = New common.Controls.MyTextBox()
        Me.fndwriteoff = New common.UserControls.txtFinder()
        Me.lblwriteoff = New common.Controls.MyLabel()
        Me.fndbankacc = New common.UserControls.txtFinder()
        Me.lblbacc = New common.Controls.MyLabel()
        Me.ddlbanktype = New common.Controls.MyComboBox()
        Me.lblbanktype = New common.Controls.MyLabel()
        Me.chkactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblbaccno = New common.Controls.MyLabel()
        Me.txtcredit = New common.Controls.MyTextBox()
        Me.txtwriteoff = New common.Controls.MyTextBox()
        Me.txtbankacc = New common.Controls.MyTextBox()
        Me.txtbankaccno = New common.Controls.MyTextBox()
        Me.bankstabadd = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtEmail = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.TxtMainBankCode = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.TxtMainBankName = New common.Controls.MyTextBox()
        Me.chkClearanceBank = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtfax = New common.Controls.MyTextBox()
        Me.txtphone = New common.Controls.MyTextBox()
        Me.txtcountry = New common.Controls.MyTextBox()
        Me.txtcontact = New common.Controls.MyTextBox()
        Me.txtzip = New common.Controls.MyTextBox()
        Me.txtstate = New common.Controls.MyTextBox()
        Me.txtcity = New common.Controls.MyTextBox()
        Me.txtadd4 = New common.Controls.MyTextBox()
        Me.txtadd3 = New common.Controls.MyTextBox()
        Me.txtadd2 = New common.Controls.MyTextBox()
        Me.txtadd1 = New common.Controls.MyTextBox()
        Me.lblfax = New common.Controls.MyLabel()
        Me.lblphone = New common.Controls.MyLabel()
        Me.lblcontact = New common.Controls.MyLabel()
        Me.lblcountry = New common.Controls.MyLabel()
        Me.lblzip = New common.Controls.MyLabel()
        Me.lblstate = New common.Controls.MyLabel()
        Me.lblcity = New common.Controls.MyLabel()
        Me.lbladdress = New common.Controls.MyLabel()
        Me.pageCheckPrinting = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCheckSetting1 = New XpertERPCommanServices.ucCheckSetting()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvPP = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvNEFTPerforma = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.chkDefaultNEFTDBT = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnclear = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.ToolTipbank = New System.Windows.Forms.ToolTip(Me.components)
        Me.fndbank = New common.UserControls.txtNavigator()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblbank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bankstab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bankstab.SuspendLayout()
        Me.bankstabprofile.SuspendLayout()
        CType(Me.chkUnpaid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSettlementBankForAD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProvisionBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankClearingAcount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultBank, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGatewayType.SuspendLayout()
        CType(Me.cboGetewayType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtswiftcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtIbanno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttransferclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltransferclearing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFDPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLCCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChequeValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblwriteoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbacc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlbanktype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbanktype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbaccno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtwriteoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankacc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankaccno, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bankstabadd.SuspendLayout()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMainBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkClearanceBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtphone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcontact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtzip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblphone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcontact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblzip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbladdress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageCheckPrinting.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvPP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPP.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvNEFTPerforma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvNEFTPerforma.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultNEFTDBT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblbank
        '
        Me.lblbank.FieldName = Nothing
        Me.lblbank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblbank.Location = New System.Drawing.Point(0, 3)
        Me.lblbank.Name = "lblbank"
        Me.lblbank.Size = New System.Drawing.Size(65, 16)
        Me.lblbank.TabIndex = 5
        Me.lblbank.Text = "Bank Code"
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(312, 3)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 4
        Me.lbldes.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(270, 2)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 18)
        Me.btnnew.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(725, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.bankimport, Me.bankexport, Me.banlclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'bankimport
        '
        Me.bankimport.Name = "bankimport"
        Me.bankimport.Text = "Import"
        '
        'bankexport
        '
        Me.bankexport.Name = "bankexport"
        Me.bankexport.Text = "Export"
        '
        'banlclose
        '
        Me.banlclose.Name = "banlclose"
        Me.banlclose.Text = "Close"
        '
        'bankstab
        '
        Me.bankstab.BackColor = System.Drawing.Color.Transparent
        Me.bankstab.Controls.Add(Me.bankstabprofile)
        Me.bankstab.Controls.Add(Me.bankstabadd)
        Me.bankstab.Controls.Add(Me.pageCheckPrinting)
        Me.bankstab.Controls.Add(Me.RadPageViewPage1)
        Me.bankstab.Controls.Add(Me.RadPageViewPage2)
        Me.bankstab.Controls.Add(Me.RadPageViewPage3)
        Me.bankstab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.bankstab.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bankstab.Location = New System.Drawing.Point(0, 24)
        Me.bankstab.Name = "bankstab"
        Me.bankstab.SelectedPage = Me.RadPageViewPage1
        Me.bankstab.Size = New System.Drawing.Size(725, 387)
        Me.bankstab.TabIndex = 3
        CType(Me.bankstab.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'bankstabprofile
        '
        Me.bankstabprofile.Controls.Add(Me.chkUnpaid)
        Me.bankstabprofile.Controls.Add(Me.txtBankGroup)
        Me.bankstabprofile.Controls.Add(Me.lblBankGroup)
        Me.bankstabprofile.Controls.Add(Me.chkSettlementBankForAD)
        Me.bankstabprofile.Controls.Add(Me.chkProvisionBank)
        Me.bankstabprofile.Controls.Add(Me.lblBankClearingAcount)
        Me.bankstabprofile.Controls.Add(Me.fndBankOpeningClearingAcount)
        Me.bankstabprofile.Controls.Add(Me.MyLabel10)
        Me.bankstabprofile.Controls.Add(Me.fndBranchName)
        Me.bankstabprofile.Controls.Add(Me.MyLabel7)
        Me.bankstabprofile.Controls.Add(Me.chkDefaultBank)
        Me.bankstabprofile.Controls.Add(Me.pnlGatewayType)
        Me.bankstabprofile.Controls.Add(Me.MyLabel5)
        Me.bankstabprofile.Controls.Add(Me.txtswiftcode)
        Me.bankstabprofile.Controls.Add(Me.MyLabel3)
        Me.bankstabprofile.Controls.Add(Me.TxtIbanno)
        Me.bankstabprofile.Controls.Add(Me.fndtransferclearing)
        Me.bankstabprofile.Controls.Add(Me.txttransferclearing)
        Me.bankstabprofile.Controls.Add(Me.rdlbltransferclearing)
        Me.bankstabprofile.Controls.Add(Me.fndSubAcc)
        Me.bankstabprofile.Controls.Add(Me.txtSubAcc)
        Me.bankstabprofile.Controls.Add(Me.MyLabel4)
        Me.bankstabprofile.Controls.Add(Me.MyLabel2)
        Me.bankstabprofile.Controls.Add(Me.MyLabel1)
        Me.bankstabprofile.Controls.Add(Me.txtFDPer)
        Me.bankstabprofile.Controls.Add(Me.txtLCCreditLimit)
        Me.bankstabprofile.Controls.Add(Me.lblChequeValidity)
        Me.bankstabprofile.Controls.Add(Me.fndcredit)
        Me.bankstabprofile.Controls.Add(Me.txtChequeValidity)
        Me.bankstabprofile.Controls.Add(Me.fndwriteoff)
        Me.bankstabprofile.Controls.Add(Me.fndbankacc)
        Me.bankstabprofile.Controls.Add(Me.ddlbanktype)
        Me.bankstabprofile.Controls.Add(Me.lblbanktype)
        Me.bankstabprofile.Controls.Add(Me.chkactive)
        Me.bankstabprofile.Controls.Add(Me.lblcredit)
        Me.bankstabprofile.Controls.Add(Me.lblwriteoff)
        Me.bankstabprofile.Controls.Add(Me.lblbacc)
        Me.bankstabprofile.Controls.Add(Me.lblbaccno)
        Me.bankstabprofile.Controls.Add(Me.txtcredit)
        Me.bankstabprofile.Controls.Add(Me.txtwriteoff)
        Me.bankstabprofile.Controls.Add(Me.txtbankacc)
        Me.bankstabprofile.Controls.Add(Me.txtbankaccno)
        Me.bankstabprofile.ItemSize = New System.Drawing.SizeF(48.0!, 26.0!)
        Me.bankstabprofile.Location = New System.Drawing.Point(10, 35)
        Me.bankstabprofile.Name = "bankstabprofile"
        Me.bankstabprofile.Size = New System.Drawing.Size(704, 341)
        Me.bankstabprofile.Text = "Profile"
        '
        'chkUnpaid
        '
        Me.chkUnpaid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUnpaid.Location = New System.Drawing.Point(607, 8)
        Me.chkUnpaid.Name = "chkUnpaid"
        Me.chkUnpaid.Size = New System.Drawing.Size(56, 16)
        Me.chkUnpaid.TabIndex = 73
        Me.chkUnpaid.Text = "Unpaid"
        '
        'txtBankGroup
        '
        Me.txtBankGroup.CalculationExpression = Nothing
        Me.txtBankGroup.FieldCode = Nothing
        Me.txtBankGroup.FieldDesc = Nothing
        Me.txtBankGroup.FieldMaxLength = 0
        Me.txtBankGroup.FieldName = Nothing
        Me.txtBankGroup.isCalculatedField = False
        Me.txtBankGroup.IsSourceFromTable = False
        Me.txtBankGroup.IsSourceFromValueList = False
        Me.txtBankGroup.IsUnique = False
        Me.txtBankGroup.Location = New System.Drawing.Point(439, 273)
        Me.txtBankGroup.MendatroryField = True
        Me.txtBankGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankGroup.MyLinkLable1 = Me.lblBankGroup
        Me.txtBankGroup.MyLinkLable2 = Nothing
        Me.txtBankGroup.MyReadOnly = False
        Me.txtBankGroup.MyShowMasterFormButton = False
        Me.txtBankGroup.Name = "txtBankGroup"
        Me.txtBankGroup.ReferenceFieldDesc = Nothing
        Me.txtBankGroup.ReferenceFieldName = Nothing
        Me.txtBankGroup.ReferenceTableName = Nothing
        Me.txtBankGroup.Size = New System.Drawing.Size(167, 19)
        Me.txtBankGroup.TabIndex = 72
        Me.txtBankGroup.Value = ""
        '
        'lblBankGroup
        '
        Me.lblBankGroup.FieldName = Nothing
        Me.lblBankGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankGroup.Location = New System.Drawing.Point(354, 274)
        Me.lblBankGroup.Name = "lblBankGroup"
        Me.lblBankGroup.Size = New System.Drawing.Size(67, 16)
        Me.lblBankGroup.TabIndex = 71
        Me.lblBankGroup.Text = "Bank Group"
        '
        'chkSettlementBankForAD
        '
        Me.chkSettlementBankForAD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSettlementBankForAD.Location = New System.Drawing.Point(355, 160)
        Me.chkSettlementBankForAD.Name = "chkSettlementBankForAD"
        Me.chkSettlementBankForAD.Size = New System.Drawing.Size(206, 16)
        Me.chkSettlementBankForAD.TabIndex = 70
        Me.chkSettlementBankForAD.Text = "Settlement Bank for Apply Document"
        '
        'chkProvisionBank
        '
        Me.chkProvisionBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProvisionBank.Location = New System.Drawing.Point(505, 8)
        Me.chkProvisionBank.Name = "chkProvisionBank"
        Me.chkProvisionBank.Size = New System.Drawing.Size(96, 16)
        Me.chkProvisionBank.TabIndex = 69
        Me.chkProvisionBank.Text = "Provision Bank"
        '
        'lblBankClearingAcount
        '
        Me.lblBankClearingAcount.CalculationExpression = Nothing
        Me.lblBankClearingAcount.FieldCode = Nothing
        Me.lblBankClearingAcount.FieldDesc = Nothing
        Me.lblBankClearingAcount.FieldMaxLength = 0
        Me.lblBankClearingAcount.FieldName = Nothing
        Me.lblBankClearingAcount.isCalculatedField = False
        Me.lblBankClearingAcount.IsSourceFromTable = False
        Me.lblBankClearingAcount.IsSourceFromValueList = False
        Me.lblBankClearingAcount.IsUnique = False
        Me.lblBankClearingAcount.Location = New System.Drawing.Point(351, 136)
        Me.lblBankClearingAcount.MendatroryField = False
        Me.lblBankClearingAcount.MyLinkLable1 = Nothing
        Me.lblBankClearingAcount.MyLinkLable2 = Nothing
        Me.lblBankClearingAcount.Name = "lblBankClearingAcount"
        Me.lblBankClearingAcount.ReferenceFieldDesc = Nothing
        Me.lblBankClearingAcount.ReferenceFieldName = Nothing
        Me.lblBankClearingAcount.ReferenceTableName = Nothing
        Me.lblBankClearingAcount.Size = New System.Drawing.Size(258, 20)
        Me.lblBankClearingAcount.TabIndex = 68
        Me.lblBankClearingAcount.TabStop = False
        '
        'fndBankOpeningClearingAcount
        '
        Me.fndBankOpeningClearingAcount.CalculationExpression = Nothing
        Me.fndBankOpeningClearingAcount.FieldCode = Nothing
        Me.fndBankOpeningClearingAcount.FieldDesc = Nothing
        Me.fndBankOpeningClearingAcount.FieldMaxLength = 0
        Me.fndBankOpeningClearingAcount.FieldName = Nothing
        Me.fndBankOpeningClearingAcount.isCalculatedField = False
        Me.fndBankOpeningClearingAcount.IsSourceFromTable = False
        Me.fndBankOpeningClearingAcount.IsSourceFromValueList = False
        Me.fndBankOpeningClearingAcount.IsUnique = False
        Me.fndBankOpeningClearingAcount.Location = New System.Drawing.Point(166, 137)
        Me.fndBankOpeningClearingAcount.MendatroryField = False
        Me.fndBankOpeningClearingAcount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankOpeningClearingAcount.MyLinkLable1 = Nothing
        Me.fndBankOpeningClearingAcount.MyLinkLable2 = Nothing
        Me.fndBankOpeningClearingAcount.MyReadOnly = False
        Me.fndBankOpeningClearingAcount.MyShowMasterFormButton = False
        Me.fndBankOpeningClearingAcount.Name = "fndBankOpeningClearingAcount"
        Me.fndBankOpeningClearingAcount.ReferenceFieldDesc = Nothing
        Me.fndBankOpeningClearingAcount.ReferenceFieldName = Nothing
        Me.fndBankOpeningClearingAcount.ReferenceTableName = Nothing
        Me.fndBankOpeningClearingAcount.Size = New System.Drawing.Size(183, 18)
        Me.fndBankOpeningClearingAcount.TabIndex = 67
        Me.fndBankOpeningClearingAcount.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(7, 137)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(138, 18)
        Me.MyLabel10.TabIndex = 66
        Me.MyLabel10.Text = "Opening Clearing Account"
        '
        'fndBranchName
        '
        Me.fndBranchName.CalculationExpression = Nothing
        Me.fndBranchName.FieldCode = Nothing
        Me.fndBranchName.FieldDesc = Nothing
        Me.fndBranchName.FieldMaxLength = 0
        Me.fndBranchName.FieldName = Nothing
        Me.fndBranchName.isCalculatedField = False
        Me.fndBranchName.IsSourceFromTable = False
        Me.fndBranchName.IsSourceFromValueList = False
        Me.fndBranchName.IsUnique = False
        Me.fndBranchName.Location = New System.Drawing.Point(439, 251)
        Me.fndBranchName.MendatroryField = True
        Me.fndBranchName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBranchName.MyLinkLable1 = Nothing
        Me.fndBranchName.MyLinkLable2 = Nothing
        Me.fndBranchName.MyReadOnly = False
        Me.fndBranchName.MyShowMasterFormButton = False
        Me.fndBranchName.Name = "fndBranchName"
        Me.fndBranchName.ReferenceFieldDesc = Nothing
        Me.fndBranchName.ReferenceFieldName = Nothing
        Me.fndBranchName.ReferenceTableName = Nothing
        Me.fndBranchName.Size = New System.Drawing.Size(167, 19)
        Me.fndBranchName.TabIndex = 62
        Me.fndBranchName.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(354, 252)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel7.TabIndex = 61
        Me.MyLabel7.Text = "Branch Code"
        '
        'chkDefaultBank
        '
        Me.chkDefaultBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDefaultBank.Location = New System.Drawing.Point(416, 8)
        Me.chkDefaultBank.Name = "chkDefaultBank"
        Me.chkDefaultBank.Size = New System.Drawing.Size(85, 16)
        Me.chkDefaultBank.TabIndex = 60
        Me.chkDefaultBank.Text = "Default Bank"
        '
        'pnlGatewayType
        '
        Me.pnlGatewayType.Controls.Add(Me.cboGetewayType)
        Me.pnlGatewayType.Controls.Add(Me.MyLabel6)
        Me.pnlGatewayType.Location = New System.Drawing.Point(351, 225)
        Me.pnlGatewayType.Name = "pnlGatewayType"
        Me.pnlGatewayType.Size = New System.Drawing.Size(259, 22)
        Me.pnlGatewayType.TabIndex = 59
        Me.pnlGatewayType.Visible = False
        '
        'cboGetewayType
        '
        Me.cboGetewayType.AutoCompleteDisplayMember = Nothing
        Me.cboGetewayType.AutoCompleteValueMember = Nothing
        Me.cboGetewayType.CalculationExpression = Nothing
        Me.cboGetewayType.DropDownAnimationEnabled = True
        Me.cboGetewayType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboGetewayType.FieldCode = Nothing
        Me.cboGetewayType.FieldDesc = Nothing
        Me.cboGetewayType.FieldMaxLength = 0
        Me.cboGetewayType.FieldName = Nothing
        Me.cboGetewayType.isCalculatedField = False
        Me.cboGetewayType.IsSourceFromTable = False
        Me.cboGetewayType.IsSourceFromValueList = False
        Me.cboGetewayType.IsUnique = False
        Me.cboGetewayType.Location = New System.Drawing.Point(88, 1)
        Me.cboGetewayType.MendatroryField = False
        Me.cboGetewayType.MyLinkLable1 = Me.MyLabel6
        Me.cboGetewayType.MyLinkLable2 = Nothing
        Me.cboGetewayType.Name = "cboGetewayType"
        Me.cboGetewayType.ReferenceFieldDesc = Nothing
        Me.cboGetewayType.ReferenceFieldName = Nothing
        Me.cboGetewayType.ReferenceTableName = Nothing
        Me.cboGetewayType.Size = New System.Drawing.Size(167, 20)
        Me.cboGetewayType.TabIndex = 58
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel6.TabIndex = 57
        Me.MyLabel6.Text = "Gateway Type"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(7, 274)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel5.TabIndex = 55
        Me.MyLabel5.Text = "Swift Code"
        '
        'txtswiftcode
        '
        Me.txtswiftcode.CalculationExpression = Nothing
        Me.txtswiftcode.FieldCode = Nothing
        Me.txtswiftcode.FieldDesc = Nothing
        Me.txtswiftcode.FieldMaxLength = 0
        Me.txtswiftcode.FieldName = Nothing
        Me.txtswiftcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtswiftcode.isCalculatedField = False
        Me.txtswiftcode.IsSourceFromTable = False
        Me.txtswiftcode.IsSourceFromValueList = False
        Me.txtswiftcode.IsUnique = False
        Me.txtswiftcode.Location = New System.Drawing.Point(166, 273)
        Me.txtswiftcode.MaxLength = 30
        Me.txtswiftcode.MendatroryField = False
        Me.txtswiftcode.MyLinkLable1 = Me.MyLabel5
        Me.txtswiftcode.MyLinkLable2 = Nothing
        Me.txtswiftcode.Name = "txtswiftcode"
        Me.txtswiftcode.ReferenceFieldDesc = Nothing
        Me.txtswiftcode.ReferenceFieldName = Nothing
        Me.txtswiftcode.ReferenceTableName = Nothing
        Me.txtswiftcode.Size = New System.Drawing.Size(183, 18)
        Me.txtswiftcode.TabIndex = 56
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 252)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel3.TabIndex = 53
        Me.MyLabel3.Text = "IBAN No."
        '
        'TxtIbanno
        '
        Me.TxtIbanno.CalculationExpression = Nothing
        Me.TxtIbanno.FieldCode = Nothing
        Me.TxtIbanno.FieldDesc = Nothing
        Me.TxtIbanno.FieldMaxLength = 0
        Me.TxtIbanno.FieldName = Nothing
        Me.TxtIbanno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIbanno.isCalculatedField = False
        Me.TxtIbanno.IsSourceFromTable = False
        Me.TxtIbanno.IsSourceFromValueList = False
        Me.TxtIbanno.IsUnique = False
        Me.TxtIbanno.Location = New System.Drawing.Point(166, 251)
        Me.TxtIbanno.MaxLength = 30
        Me.TxtIbanno.MendatroryField = False
        Me.TxtIbanno.MyLinkLable1 = Me.MyLabel3
        Me.TxtIbanno.MyLinkLable2 = Nothing
        Me.TxtIbanno.Name = "TxtIbanno"
        Me.TxtIbanno.ReferenceFieldDesc = Nothing
        Me.TxtIbanno.ReferenceFieldName = Nothing
        Me.TxtIbanno.ReferenceTableName = Nothing
        Me.TxtIbanno.Size = New System.Drawing.Size(183, 18)
        Me.TxtIbanno.TabIndex = 54
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
        Me.fndtransferclearing.Location = New System.Drawing.Point(166, 95)
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
        Me.fndtransferclearing.Size = New System.Drawing.Size(183, 19)
        Me.fndtransferclearing.TabIndex = 50
        Me.fndtransferclearing.Value = ""
        '
        'txttransferclearing
        '
        Me.txttransferclearing.CalculationExpression = Nothing
        Me.txttransferclearing.FieldCode = Nothing
        Me.txttransferclearing.FieldDesc = Nothing
        Me.txttransferclearing.FieldMaxLength = 0
        Me.txttransferclearing.FieldName = Nothing
        Me.txttransferclearing.isCalculatedField = False
        Me.txttransferclearing.IsSourceFromTable = False
        Me.txttransferclearing.IsSourceFromValueList = False
        Me.txttransferclearing.IsUnique = False
        Me.txttransferclearing.Location = New System.Drawing.Point(352, 94)
        Me.txttransferclearing.MendatroryField = False
        Me.txttransferclearing.MyLinkLable1 = Nothing
        Me.txttransferclearing.MyLinkLable2 = Nothing
        Me.txttransferclearing.Name = "txttransferclearing"
        Me.txttransferclearing.ReferenceFieldDesc = Nothing
        Me.txttransferclearing.ReferenceFieldName = Nothing
        Me.txttransferclearing.ReferenceTableName = Nothing
        Me.txttransferclearing.Size = New System.Drawing.Size(258, 20)
        Me.txttransferclearing.TabIndex = 52
        Me.txttransferclearing.TabStop = False
        '
        'rdlbltransferclearing
        '
        Me.rdlbltransferclearing.FieldName = Nothing
        Me.rdlbltransferclearing.Location = New System.Drawing.Point(7, 95)
        Me.rdlbltransferclearing.Name = "rdlbltransferclearing"
        Me.rdlbltransferclearing.Size = New System.Drawing.Size(91, 18)
        Me.rdlbltransferclearing.TabIndex = 51
        Me.rdlbltransferclearing.Text = "Transfer Clearing"
        '
        'fndSubAcc
        '
        Me.fndSubAcc.CalculationExpression = Nothing
        Me.fndSubAcc.FieldCode = Nothing
        Me.fndSubAcc.FieldDesc = Nothing
        Me.fndSubAcc.FieldMaxLength = 0
        Me.fndSubAcc.FieldName = Nothing
        Me.fndSubAcc.isCalculatedField = False
        Me.fndSubAcc.IsSourceFromTable = False
        Me.fndSubAcc.IsSourceFromValueList = False
        Me.fndSubAcc.IsUnique = False
        Me.fndSubAcc.Location = New System.Drawing.Point(166, 117)
        Me.fndSubAcc.MendatroryField = False
        Me.fndSubAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubAcc.MyLinkLable1 = Me.MyLabel4
        Me.fndSubAcc.MyLinkLable2 = Nothing
        Me.fndSubAcc.MyReadOnly = False
        Me.fndSubAcc.MyShowMasterFormButton = False
        Me.fndSubAcc.Name = "fndSubAcc"
        Me.fndSubAcc.ReferenceFieldDesc = Nothing
        Me.fndSubAcc.ReferenceFieldName = Nothing
        Me.fndSubAcc.ReferenceTableName = Nothing
        Me.fndSubAcc.Size = New System.Drawing.Size(183, 18)
        Me.fndSubAcc.TabIndex = 8
        Me.fndSubAcc.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(7, 117)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel4.TabIndex = 49
        Me.MyLabel4.Text = "Sub Account"
        '
        'txtSubAcc
        '
        Me.txtSubAcc.CalculationExpression = Nothing
        Me.txtSubAcc.FieldCode = Nothing
        Me.txtSubAcc.FieldDesc = Nothing
        Me.txtSubAcc.FieldMaxLength = 0
        Me.txtSubAcc.FieldName = Nothing
        Me.txtSubAcc.isCalculatedField = False
        Me.txtSubAcc.IsSourceFromTable = False
        Me.txtSubAcc.IsSourceFromValueList = False
        Me.txtSubAcc.IsUnique = False
        Me.txtSubAcc.Location = New System.Drawing.Point(352, 116)
        Me.txtSubAcc.MendatroryField = False
        Me.txtSubAcc.MyLinkLable1 = Nothing
        Me.txtSubAcc.MyLinkLable2 = Nothing
        Me.txtSubAcc.Name = "txtSubAcc"
        Me.txtSubAcc.ReferenceFieldDesc = Nothing
        Me.txtSubAcc.ReferenceFieldName = Nothing
        Me.txtSubAcc.ReferenceTableName = Nothing
        Me.txtSubAcc.Size = New System.Drawing.Size(258, 20)
        Me.txtSubAcc.TabIndex = 9
        Me.txtSubAcc.TabStop = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 205)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel2.TabIndex = 8
        Me.MyLabel2.Text = "FD %"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 181)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel1.TabIndex = 15
        Me.MyLabel1.Text = "LC Credit Limit"
        '
        'txtFDPer
        '
        Me.txtFDPer.BackColor = System.Drawing.Color.White
        Me.txtFDPer.CalculationExpression = Nothing
        Me.txtFDPer.DecimalPlaces = 2
        Me.txtFDPer.FieldCode = Nothing
        Me.txtFDPer.FieldDesc = Nothing
        Me.txtFDPer.FieldMaxLength = 0
        Me.txtFDPer.FieldName = Nothing
        Me.txtFDPer.isCalculatedField = False
        Me.txtFDPer.IsSourceFromTable = False
        Me.txtFDPer.IsSourceFromValueList = False
        Me.txtFDPer.IsUnique = False
        Me.txtFDPer.Location = New System.Drawing.Point(166, 203)
        Me.txtFDPer.MendatroryField = False
        Me.txtFDPer.MyLinkLable1 = Nothing
        Me.txtFDPer.MyLinkLable2 = Nothing
        Me.txtFDPer.Name = "txtFDPer"
        Me.txtFDPer.ReferenceFieldDesc = Nothing
        Me.txtFDPer.ReferenceFieldName = Nothing
        Me.txtFDPer.ReferenceTableName = Nothing
        Me.txtFDPer.Size = New System.Drawing.Size(183, 20)
        Me.txtFDPer.TabIndex = 12
        Me.txtFDPer.Text = "0"
        Me.txtFDPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFDPer.Value = 0R
        '
        'txtLCCreditLimit
        '
        Me.txtLCCreditLimit.BackColor = System.Drawing.Color.White
        Me.txtLCCreditLimit.CalculationExpression = Nothing
        Me.txtLCCreditLimit.DecimalPlaces = 2
        Me.txtLCCreditLimit.FieldCode = Nothing
        Me.txtLCCreditLimit.FieldDesc = Nothing
        Me.txtLCCreditLimit.FieldMaxLength = 0
        Me.txtLCCreditLimit.FieldName = Nothing
        Me.txtLCCreditLimit.isCalculatedField = False
        Me.txtLCCreditLimit.IsSourceFromTable = False
        Me.txtLCCreditLimit.IsSourceFromValueList = False
        Me.txtLCCreditLimit.IsUnique = False
        Me.txtLCCreditLimit.Location = New System.Drawing.Point(166, 179)
        Me.txtLCCreditLimit.MendatroryField = False
        Me.txtLCCreditLimit.MyLinkLable1 = Nothing
        Me.txtLCCreditLimit.MyLinkLable2 = Nothing
        Me.txtLCCreditLimit.Name = "txtLCCreditLimit"
        Me.txtLCCreditLimit.ReferenceFieldDesc = Nothing
        Me.txtLCCreditLimit.ReferenceFieldName = Nothing
        Me.txtLCCreditLimit.ReferenceTableName = Nothing
        Me.txtLCCreditLimit.Size = New System.Drawing.Size(183, 20)
        Me.txtLCCreditLimit.TabIndex = 11
        Me.txtLCCreditLimit.Text = "0"
        Me.txtLCCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLCCreditLimit.Value = 0R
        '
        'lblChequeValidity
        '
        Me.lblChequeValidity.FieldName = Nothing
        Me.lblChequeValidity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeValidity.Location = New System.Drawing.Point(7, 159)
        Me.lblChequeValidity.Name = "lblChequeValidity"
        Me.lblChequeValidity.Size = New System.Drawing.Size(155, 16)
        Me.lblChequeValidity.TabIndex = 7
        Me.lblChequeValidity.Text = "Cheque Validity (No. of Days)"
        '
        'fndcredit
        '
        Me.fndcredit.CalculationExpression = Nothing
        Me.fndcredit.FieldCode = Nothing
        Me.fndcredit.FieldDesc = Nothing
        Me.fndcredit.FieldMaxLength = 0
        Me.fndcredit.FieldName = Nothing
        Me.fndcredit.isCalculatedField = False
        Me.fndcredit.IsSourceFromTable = False
        Me.fndcredit.IsSourceFromValueList = False
        Me.fndcredit.IsUnique = False
        Me.fndcredit.Location = New System.Drawing.Point(166, 73)
        Me.fndcredit.MendatroryField = True
        Me.fndcredit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcredit.MyLinkLable1 = Me.lblcredit
        Me.fndcredit.MyLinkLable2 = Nothing
        Me.fndcredit.MyReadOnly = False
        Me.fndcredit.MyShowMasterFormButton = False
        Me.fndcredit.Name = "fndcredit"
        Me.fndcredit.ReferenceFieldDesc = Nothing
        Me.fndcredit.ReferenceFieldName = Nothing
        Me.fndcredit.ReferenceTableName = Nothing
        Me.fndcredit.Size = New System.Drawing.Size(183, 18)
        Me.fndcredit.TabIndex = 6
        Me.fndcredit.Value = ""
        '
        'lblcredit
        '
        Me.lblcredit.FieldName = Nothing
        Me.lblcredit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcredit.Location = New System.Drawing.Point(7, 74)
        Me.lblcredit.Name = "lblcredit"
        Me.lblcredit.Size = New System.Drawing.Size(78, 16)
        Me.lblcredit.TabIndex = 9
        Me.lblcredit.Text = "Bank Charges"
        '
        'txtChequeValidity
        '
        Me.txtChequeValidity.CalculationExpression = Nothing
        Me.txtChequeValidity.FieldCode = Nothing
        Me.txtChequeValidity.FieldDesc = Nothing
        Me.txtChequeValidity.FieldMaxLength = 0
        Me.txtChequeValidity.FieldName = Nothing
        Me.txtChequeValidity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChequeValidity.isCalculatedField = False
        Me.txtChequeValidity.IsSourceFromTable = False
        Me.txtChequeValidity.IsSourceFromValueList = False
        Me.txtChequeValidity.IsUnique = False
        Me.txtChequeValidity.Location = New System.Drawing.Point(166, 158)
        Me.txtChequeValidity.MaxLength = 29
        Me.txtChequeValidity.MendatroryField = False
        Me.txtChequeValidity.MyLinkLable1 = Me.lblChequeValidity
        Me.txtChequeValidity.MyLinkLable2 = Nothing
        Me.txtChequeValidity.Name = "txtChequeValidity"
        Me.txtChequeValidity.ReferenceFieldDesc = Nothing
        Me.txtChequeValidity.ReferenceFieldName = Nothing
        Me.txtChequeValidity.ReferenceTableName = Nothing
        Me.txtChequeValidity.Size = New System.Drawing.Size(183, 18)
        Me.txtChequeValidity.TabIndex = 10
        '
        'fndwriteoff
        '
        Me.fndwriteoff.CalculationExpression = Nothing
        Me.fndwriteoff.FieldCode = Nothing
        Me.fndwriteoff.FieldDesc = Nothing
        Me.fndwriteoff.FieldMaxLength = 0
        Me.fndwriteoff.FieldName = Nothing
        Me.fndwriteoff.isCalculatedField = False
        Me.fndwriteoff.IsSourceFromTable = False
        Me.fndwriteoff.IsSourceFromValueList = False
        Me.fndwriteoff.IsUnique = False
        Me.fndwriteoff.Location = New System.Drawing.Point(166, 51)
        Me.fndwriteoff.MendatroryField = True
        Me.fndwriteoff.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndwriteoff.MyLinkLable1 = Me.lblwriteoff
        Me.fndwriteoff.MyLinkLable2 = Nothing
        Me.fndwriteoff.MyReadOnly = False
        Me.fndwriteoff.MyShowMasterFormButton = False
        Me.fndwriteoff.Name = "fndwriteoff"
        Me.fndwriteoff.ReferenceFieldDesc = Nothing
        Me.fndwriteoff.ReferenceFieldName = Nothing
        Me.fndwriteoff.ReferenceTableName = Nothing
        Me.fndwriteoff.Size = New System.Drawing.Size(183, 18)
        Me.fndwriteoff.TabIndex = 4
        Me.fndwriteoff.Value = ""
        '
        'lblwriteoff
        '
        Me.lblwriteoff.FieldName = Nothing
        Me.lblwriteoff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwriteoff.Location = New System.Drawing.Point(7, 52)
        Me.lblwriteoff.Name = "lblwriteoff"
        Me.lblwriteoff.Size = New System.Drawing.Size(95, 16)
        Me.lblwriteoff.TabIndex = 10
        Me.lblwriteoff.Text = "Write Off Account"
        '
        'fndbankacc
        '
        Me.fndbankacc.CalculationExpression = Nothing
        Me.fndbankacc.FieldCode = Nothing
        Me.fndbankacc.FieldDesc = Nothing
        Me.fndbankacc.FieldMaxLength = 0
        Me.fndbankacc.FieldName = Nothing
        Me.fndbankacc.isCalculatedField = False
        Me.fndbankacc.IsSourceFromTable = False
        Me.fndbankacc.IsSourceFromValueList = False
        Me.fndbankacc.IsUnique = False
        Me.fndbankacc.Location = New System.Drawing.Point(166, 29)
        Me.fndbankacc.MendatroryField = True
        Me.fndbankacc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndbankacc.MyLinkLable1 = Me.lblbacc
        Me.fndbankacc.MyLinkLable2 = Nothing
        Me.fndbankacc.MyReadOnly = False
        Me.fndbankacc.MyShowMasterFormButton = False
        Me.fndbankacc.Name = "fndbankacc"
        Me.fndbankacc.ReferenceFieldDesc = Nothing
        Me.fndbankacc.ReferenceFieldName = Nothing
        Me.fndbankacc.ReferenceTableName = Nothing
        Me.fndbankacc.Size = New System.Drawing.Size(183, 18)
        Me.fndbankacc.TabIndex = 2
        Me.fndbankacc.Value = ""
        '
        'lblbacc
        '
        Me.lblbacc.FieldName = Nothing
        Me.lblbacc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbacc.Location = New System.Drawing.Point(7, 30)
        Me.lblbacc.Name = "lblbacc"
        Me.lblbacc.Size = New System.Drawing.Size(76, 16)
        Me.lblbacc.TabIndex = 11
        Me.lblbacc.Text = "Bank Account"
        '
        'ddlbanktype
        '
        Me.ddlbanktype.AutoCompleteDisplayMember = Nothing
        Me.ddlbanktype.AutoCompleteValueMember = Nothing
        Me.ddlbanktype.CalculationExpression = Nothing
        Me.ddlbanktype.DropDownAnimationEnabled = True
        Me.ddlbanktype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlbanktype.FieldCode = Nothing
        Me.ddlbanktype.FieldDesc = Nothing
        Me.ddlbanktype.FieldMaxLength = 0
        Me.ddlbanktype.FieldName = Nothing
        Me.ddlbanktype.isCalculatedField = False
        Me.ddlbanktype.IsSourceFromTable = False
        Me.ddlbanktype.IsSourceFromValueList = False
        Me.ddlbanktype.IsUnique = False
        RadListDataItem6.Text = "Cash"
        RadListDataItem7.Text = "Bank"
        RadListDataItem8.Text = "Petty Cash"
        RadListDataItem9.Text = "Other"
        RadListDataItem10.Text = "Settlement"
        Me.ddlbanktype.Items.Add(RadListDataItem6)
        Me.ddlbanktype.Items.Add(RadListDataItem7)
        Me.ddlbanktype.Items.Add(RadListDataItem8)
        Me.ddlbanktype.Items.Add(RadListDataItem9)
        Me.ddlbanktype.Items.Add(RadListDataItem10)
        Me.ddlbanktype.Location = New System.Drawing.Point(166, 227)
        Me.ddlbanktype.MendatroryField = True
        Me.ddlbanktype.MyLinkLable1 = Me.lblbanktype
        Me.ddlbanktype.MyLinkLable2 = Nothing
        Me.ddlbanktype.Name = "ddlbanktype"
        Me.ddlbanktype.ReferenceFieldDesc = Nothing
        Me.ddlbanktype.ReferenceFieldName = Nothing
        Me.ddlbanktype.ReferenceTableName = Nothing
        Me.ddlbanktype.Size = New System.Drawing.Size(183, 20)
        Me.ddlbanktype.TabIndex = 13
        '
        'lblbanktype
        '
        Me.lblbanktype.FieldName = Nothing
        Me.lblbanktype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbanktype.Location = New System.Drawing.Point(7, 228)
        Me.lblbanktype.Name = "lblbanktype"
        Me.lblbanktype.Size = New System.Drawing.Size(60, 16)
        Me.lblbanktype.TabIndex = 8
        Me.lblbanktype.Text = "Bank Type"
        '
        'chkactive
        '
        Me.chkactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkactive.Location = New System.Drawing.Point(352, 8)
        Me.chkactive.Name = "chkactive"
        Me.chkactive.Size = New System.Drawing.Size(60, 16)
        Me.chkactive.TabIndex = 1
        Me.chkactive.Text = "InActive"
        '
        'lblbaccno
        '
        Me.lblbaccno.FieldName = Nothing
        Me.lblbaccno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbaccno.Location = New System.Drawing.Point(7, 8)
        Me.lblbaccno.Name = "lblbaccno"
        Me.lblbaccno.Size = New System.Drawing.Size(120, 16)
        Me.lblbaccno.TabIndex = 12
        Me.lblbaccno.Text = "Bank Account Number"
        '
        'txtcredit
        '
        Me.txtcredit.CalculationExpression = Nothing
        Me.txtcredit.FieldCode = Nothing
        Me.txtcredit.FieldDesc = Nothing
        Me.txtcredit.FieldMaxLength = 0
        Me.txtcredit.FieldName = Nothing
        Me.txtcredit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcredit.isCalculatedField = False
        Me.txtcredit.IsSourceFromTable = False
        Me.txtcredit.IsSourceFromValueList = False
        Me.txtcredit.IsUnique = False
        Me.txtcredit.Location = New System.Drawing.Point(352, 73)
        Me.txtcredit.MaxLength = 49
        Me.txtcredit.MendatroryField = False
        Me.txtcredit.MyLinkLable1 = Nothing
        Me.txtcredit.MyLinkLable2 = Nothing
        Me.txtcredit.Name = "txtcredit"
        Me.txtcredit.ReadOnly = True
        Me.txtcredit.ReferenceFieldDesc = Nothing
        Me.txtcredit.ReferenceFieldName = Nothing
        Me.txtcredit.ReferenceTableName = Nothing
        Me.txtcredit.Size = New System.Drawing.Size(258, 18)
        Me.txtcredit.TabIndex = 7
        Me.txtcredit.TabStop = False
        '
        'txtwriteoff
        '
        Me.txtwriteoff.CalculationExpression = Nothing
        Me.txtwriteoff.FieldCode = Nothing
        Me.txtwriteoff.FieldDesc = Nothing
        Me.txtwriteoff.FieldMaxLength = 0
        Me.txtwriteoff.FieldName = Nothing
        Me.txtwriteoff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtwriteoff.isCalculatedField = False
        Me.txtwriteoff.IsSourceFromTable = False
        Me.txtwriteoff.IsSourceFromValueList = False
        Me.txtwriteoff.IsUnique = False
        Me.txtwriteoff.Location = New System.Drawing.Point(352, 51)
        Me.txtwriteoff.MaxLength = 49
        Me.txtwriteoff.MendatroryField = False
        Me.txtwriteoff.MyLinkLable1 = Nothing
        Me.txtwriteoff.MyLinkLable2 = Nothing
        Me.txtwriteoff.Name = "txtwriteoff"
        Me.txtwriteoff.ReadOnly = True
        Me.txtwriteoff.ReferenceFieldDesc = Nothing
        Me.txtwriteoff.ReferenceFieldName = Nothing
        Me.txtwriteoff.ReferenceTableName = Nothing
        Me.txtwriteoff.Size = New System.Drawing.Size(258, 18)
        Me.txtwriteoff.TabIndex = 5
        Me.txtwriteoff.TabStop = False
        '
        'txtbankacc
        '
        Me.txtbankacc.CalculationExpression = Nothing
        Me.txtbankacc.FieldCode = Nothing
        Me.txtbankacc.FieldDesc = Nothing
        Me.txtbankacc.FieldMaxLength = 0
        Me.txtbankacc.FieldName = Nothing
        Me.txtbankacc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankacc.isCalculatedField = False
        Me.txtbankacc.IsSourceFromTable = False
        Me.txtbankacc.IsSourceFromValueList = False
        Me.txtbankacc.IsUnique = False
        Me.txtbankacc.Location = New System.Drawing.Point(352, 29)
        Me.txtbankacc.MaxLength = 49
        Me.txtbankacc.MendatroryField = False
        Me.txtbankacc.MyLinkLable1 = Nothing
        Me.txtbankacc.MyLinkLable2 = Nothing
        Me.txtbankacc.Name = "txtbankacc"
        Me.txtbankacc.ReadOnly = True
        Me.txtbankacc.ReferenceFieldDesc = Nothing
        Me.txtbankacc.ReferenceFieldName = Nothing
        Me.txtbankacc.ReferenceTableName = Nothing
        Me.txtbankacc.Size = New System.Drawing.Size(258, 18)
        Me.txtbankacc.TabIndex = 3
        Me.txtbankacc.TabStop = False
        '
        'txtbankaccno
        '
        Me.txtbankaccno.CalculationExpression = Nothing
        Me.txtbankaccno.FieldCode = Nothing
        Me.txtbankaccno.FieldDesc = Nothing
        Me.txtbankaccno.FieldMaxLength = 0
        Me.txtbankaccno.FieldName = Nothing
        Me.txtbankaccno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankaccno.isCalculatedField = False
        Me.txtbankaccno.IsSourceFromTable = False
        Me.txtbankaccno.IsSourceFromValueList = False
        Me.txtbankaccno.IsUnique = False
        Me.txtbankaccno.Location = New System.Drawing.Point(166, 7)
        Me.txtbankaccno.MaxLength = 29
        Me.txtbankaccno.MendatroryField = False
        Me.txtbankaccno.MyLinkLable1 = Me.lblbaccno
        Me.txtbankaccno.MyLinkLable2 = Nothing
        Me.txtbankaccno.Name = "txtbankaccno"
        Me.txtbankaccno.ReferenceFieldDesc = Nothing
        Me.txtbankaccno.ReferenceFieldName = Nothing
        Me.txtbankaccno.ReferenceTableName = Nothing
        Me.txtbankaccno.Size = New System.Drawing.Size(183, 18)
        Me.txtbankaccno.TabIndex = 0
        '
        'bankstabadd
        '
        Me.bankstabadd.Controls.Add(Me.txtEmail)
        Me.bankstabadd.Controls.Add(Me.MyLabel9)
        Me.bankstabadd.Controls.Add(Me.TxtMainBankCode)
        Me.bankstabadd.Controls.Add(Me.MyLabel8)
        Me.bankstabadd.Controls.Add(Me.TxtMainBankName)
        Me.bankstabadd.Controls.Add(Me.chkClearanceBank)
        Me.bankstabadd.Controls.Add(Me.txtfax)
        Me.bankstabadd.Controls.Add(Me.txtphone)
        Me.bankstabadd.Controls.Add(Me.txtcountry)
        Me.bankstabadd.Controls.Add(Me.txtcontact)
        Me.bankstabadd.Controls.Add(Me.txtzip)
        Me.bankstabadd.Controls.Add(Me.txtstate)
        Me.bankstabadd.Controls.Add(Me.txtcity)
        Me.bankstabadd.Controls.Add(Me.txtadd4)
        Me.bankstabadd.Controls.Add(Me.txtadd3)
        Me.bankstabadd.Controls.Add(Me.txtadd2)
        Me.bankstabadd.Controls.Add(Me.txtadd1)
        Me.bankstabadd.Controls.Add(Me.lblfax)
        Me.bankstabadd.Controls.Add(Me.lblphone)
        Me.bankstabadd.Controls.Add(Me.lblcontact)
        Me.bankstabadd.Controls.Add(Me.lblcountry)
        Me.bankstabadd.Controls.Add(Me.lblzip)
        Me.bankstabadd.Controls.Add(Me.lblstate)
        Me.bankstabadd.Controls.Add(Me.lblcity)
        Me.bankstabadd.Controls.Add(Me.lbladdress)
        Me.bankstabadd.ItemSize = New System.Drawing.SizeF(58.0!, 26.0!)
        Me.bankstabadd.Location = New System.Drawing.Point(10, 35)
        Me.bankstabadd.Name = "bankstabadd"
        Me.bankstabadd.Size = New System.Drawing.Size(704, 341)
        Me.bankstabadd.Text = "Address"
        '
        'txtEmail
        '
        Me.txtEmail.CalculationExpression = Nothing
        Me.txtEmail.FieldCode = Nothing
        Me.txtEmail.FieldDesc = Nothing
        Me.txtEmail.FieldMaxLength = 0
        Me.txtEmail.FieldName = Nothing
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.isCalculatedField = False
        Me.txtEmail.IsSourceFromTable = False
        Me.txtEmail.IsSourceFromValueList = False
        Me.txtEmail.IsUnique = False
        Me.txtEmail.Location = New System.Drawing.Point(130, 239)
        Me.txtEmail.MaxLength = 30
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Nothing
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReferenceFieldDesc = Nothing
        Me.txtEmail.ReferenceFieldName = Nothing
        Me.txtEmail.ReferenceTableName = Nothing
        Me.txtEmail.Size = New System.Drawing.Size(224, 18)
        Me.txtEmail.TabIndex = 23
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(13, 241)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel9.TabIndex = 24
        Me.MyLabel9.Text = "Email"
        '
        'TxtMainBankCode
        '
        Me.TxtMainBankCode.CalculationExpression = Nothing
        Me.TxtMainBankCode.FieldCode = Nothing
        Me.TxtMainBankCode.FieldDesc = Nothing
        Me.TxtMainBankCode.FieldMaxLength = 0
        Me.TxtMainBankCode.FieldName = Nothing
        Me.TxtMainBankCode.isCalculatedField = False
        Me.TxtMainBankCode.IsSourceFromTable = False
        Me.TxtMainBankCode.IsSourceFromValueList = False
        Me.TxtMainBankCode.IsUnique = False
        Me.TxtMainBankCode.Location = New System.Drawing.Point(130, 306)
        Me.TxtMainBankCode.MendatroryField = False
        Me.TxtMainBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMainBankCode.MyLinkLable1 = Me.MyLabel8
        Me.TxtMainBankCode.MyLinkLable2 = Nothing
        Me.TxtMainBankCode.MyReadOnly = False
        Me.TxtMainBankCode.MyShowMasterFormButton = False
        Me.TxtMainBankCode.Name = "TxtMainBankCode"
        Me.TxtMainBankCode.ReferenceFieldDesc = Nothing
        Me.TxtMainBankCode.ReferenceFieldName = Nothing
        Me.TxtMainBankCode.ReferenceTableName = Nothing
        Me.TxtMainBankCode.Size = New System.Drawing.Size(183, 18)
        Me.TxtMainBankCode.TabIndex = 20
        Me.TxtMainBankCode.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(13, 308)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel8.TabIndex = 22
        Me.MyLabel8.Text = "Main Bank "
        '
        'TxtMainBankName
        '
        Me.TxtMainBankName.CalculationExpression = Nothing
        Me.TxtMainBankName.FieldCode = Nothing
        Me.TxtMainBankName.FieldDesc = Nothing
        Me.TxtMainBankName.FieldMaxLength = 0
        Me.TxtMainBankName.FieldName = Nothing
        Me.TxtMainBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMainBankName.isCalculatedField = False
        Me.TxtMainBankName.IsSourceFromTable = False
        Me.TxtMainBankName.IsSourceFromValueList = False
        Me.TxtMainBankName.IsUnique = False
        Me.TxtMainBankName.Location = New System.Drawing.Point(319, 306)
        Me.TxtMainBankName.MaxLength = 49
        Me.TxtMainBankName.MendatroryField = False
        Me.TxtMainBankName.MyLinkLable1 = Nothing
        Me.TxtMainBankName.MyLinkLable2 = Nothing
        Me.TxtMainBankName.Name = "TxtMainBankName"
        Me.TxtMainBankName.ReadOnly = True
        Me.TxtMainBankName.ReferenceFieldDesc = Nothing
        Me.TxtMainBankName.ReferenceFieldName = Nothing
        Me.TxtMainBankName.ReferenceTableName = Nothing
        Me.TxtMainBankName.Size = New System.Drawing.Size(316, 18)
        Me.TxtMainBankName.TabIndex = 21
        Me.TxtMainBankName.TabStop = False
        '
        'chkClearanceBank
        '
        Me.chkClearanceBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkClearanceBank.Location = New System.Drawing.Point(130, 286)
        Me.chkClearanceBank.Name = "chkClearanceBank"
        Me.chkClearanceBank.Size = New System.Drawing.Size(101, 16)
        Me.chkClearanceBank.TabIndex = 19
        Me.chkClearanceBank.Text = "Clearance Bank"
        '
        'txtfax
        '
        Me.txtfax.CalculationExpression = Nothing
        Me.txtfax.FieldCode = Nothing
        Me.txtfax.FieldDesc = Nothing
        Me.txtfax.FieldMaxLength = 0
        Me.txtfax.FieldName = Nothing
        Me.txtfax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfax.isCalculatedField = False
        Me.txtfax.IsSourceFromTable = False
        Me.txtfax.IsSourceFromValueList = False
        Me.txtfax.IsUnique = False
        Me.txtfax.Location = New System.Drawing.Point(130, 262)
        Me.txtfax.MaxLength = 30
        Me.txtfax.MendatroryField = False
        Me.txtfax.MyLinkLable1 = Nothing
        Me.txtfax.MyLinkLable2 = Nothing
        Me.txtfax.Name = "txtfax"
        Me.txtfax.ReferenceFieldDesc = Nothing
        Me.txtfax.ReferenceFieldName = Nothing
        Me.txtfax.ReferenceTableName = Nothing
        Me.txtfax.Size = New System.Drawing.Size(224, 18)
        Me.txtfax.TabIndex = 10
        '
        'txtphone
        '
        Me.txtphone.CalculationExpression = Nothing
        Me.txtphone.FieldCode = Nothing
        Me.txtphone.FieldDesc = Nothing
        Me.txtphone.FieldMaxLength = 0
        Me.txtphone.FieldName = Nothing
        Me.txtphone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.isCalculatedField = False
        Me.txtphone.IsSourceFromTable = False
        Me.txtphone.IsSourceFromValueList = False
        Me.txtphone.IsUnique = False
        Me.txtphone.Location = New System.Drawing.Point(130, 217)
        Me.txtphone.MaxLength = 30
        Me.txtphone.MendatroryField = False
        Me.txtphone.MyLinkLable1 = Nothing
        Me.txtphone.MyLinkLable2 = Nothing
        Me.txtphone.Name = "txtphone"
        Me.txtphone.ReferenceFieldDesc = Nothing
        Me.txtphone.ReferenceFieldName = Nothing
        Me.txtphone.ReferenceTableName = Nothing
        Me.txtphone.Size = New System.Drawing.Size(224, 18)
        Me.txtphone.TabIndex = 9
        '
        'txtcountry
        '
        Me.txtcountry.CalculationExpression = Nothing
        Me.txtcountry.FieldCode = Nothing
        Me.txtcountry.FieldDesc = Nothing
        Me.txtcountry.FieldMaxLength = 0
        Me.txtcountry.FieldName = Nothing
        Me.txtcountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcountry.isCalculatedField = False
        Me.txtcountry.IsSourceFromTable = False
        Me.txtcountry.IsSourceFromValueList = False
        Me.txtcountry.IsUnique = False
        Me.txtcountry.Location = New System.Drawing.Point(130, 171)
        Me.txtcountry.MaxLength = 30
        Me.txtcountry.MendatroryField = False
        Me.txtcountry.MyLinkLable1 = Nothing
        Me.txtcountry.MyLinkLable2 = Nothing
        Me.txtcountry.Name = "txtcountry"
        Me.txtcountry.ReferenceFieldDesc = Nothing
        Me.txtcountry.ReferenceFieldName = Nothing
        Me.txtcountry.ReferenceTableName = Nothing
        Me.txtcountry.Size = New System.Drawing.Size(224, 18)
        Me.txtcountry.TabIndex = 7
        '
        'txtcontact
        '
        Me.txtcontact.CalculationExpression = Nothing
        Me.txtcontact.FieldCode = Nothing
        Me.txtcontact.FieldDesc = Nothing
        Me.txtcontact.FieldMaxLength = 0
        Me.txtcontact.FieldName = Nothing
        Me.txtcontact.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcontact.isCalculatedField = False
        Me.txtcontact.IsSourceFromTable = False
        Me.txtcontact.IsSourceFromValueList = False
        Me.txtcontact.IsUnique = False
        Me.txtcontact.Location = New System.Drawing.Point(130, 194)
        Me.txtcontact.MaxLength = 60
        Me.txtcontact.MendatroryField = False
        Me.txtcontact.MyLinkLable1 = Nothing
        Me.txtcontact.MyLinkLable2 = Nothing
        Me.txtcontact.Name = "txtcontact"
        Me.txtcontact.ReferenceFieldDesc = Nothing
        Me.txtcontact.ReferenceFieldName = Nothing
        Me.txtcontact.ReferenceTableName = Nothing
        Me.txtcontact.Size = New System.Drawing.Size(505, 18)
        Me.txtcontact.TabIndex = 8
        '
        'txtzip
        '
        Me.txtzip.CalculationExpression = Nothing
        Me.txtzip.FieldCode = Nothing
        Me.txtzip.FieldDesc = Nothing
        Me.txtzip.FieldMaxLength = 0
        Me.txtzip.FieldName = Nothing
        Me.txtzip.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtzip.isCalculatedField = False
        Me.txtzip.IsSourceFromTable = False
        Me.txtzip.IsSourceFromValueList = False
        Me.txtzip.IsUnique = False
        Me.txtzip.Location = New System.Drawing.Point(130, 148)
        Me.txtzip.MaxLength = 30
        Me.txtzip.MendatroryField = False
        Me.txtzip.MyLinkLable1 = Nothing
        Me.txtzip.MyLinkLable2 = Nothing
        Me.txtzip.Name = "txtzip"
        Me.txtzip.ReferenceFieldDesc = Nothing
        Me.txtzip.ReferenceFieldName = Nothing
        Me.txtzip.ReferenceTableName = Nothing
        Me.txtzip.Size = New System.Drawing.Size(224, 18)
        Me.txtzip.TabIndex = 6
        '
        'txtstate
        '
        Me.txtstate.CalculationExpression = Nothing
        Me.txtstate.FieldCode = Nothing
        Me.txtstate.FieldDesc = Nothing
        Me.txtstate.FieldMaxLength = 0
        Me.txtstate.FieldName = Nothing
        Me.txtstate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstate.isCalculatedField = False
        Me.txtstate.IsSourceFromTable = False
        Me.txtstate.IsSourceFromValueList = False
        Me.txtstate.IsUnique = False
        Me.txtstate.Location = New System.Drawing.Point(130, 125)
        Me.txtstate.MaxLength = 30
        Me.txtstate.MendatroryField = False
        Me.txtstate.MyLinkLable1 = Nothing
        Me.txtstate.MyLinkLable2 = Nothing
        Me.txtstate.Name = "txtstate"
        Me.txtstate.ReferenceFieldDesc = Nothing
        Me.txtstate.ReferenceFieldName = Nothing
        Me.txtstate.ReferenceTableName = Nothing
        Me.txtstate.Size = New System.Drawing.Size(224, 18)
        Me.txtstate.TabIndex = 5
        '
        'txtcity
        '
        Me.txtcity.CalculationExpression = Nothing
        Me.txtcity.FieldCode = Nothing
        Me.txtcity.FieldDesc = Nothing
        Me.txtcity.FieldMaxLength = 0
        Me.txtcity.FieldName = Nothing
        Me.txtcity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcity.isCalculatedField = False
        Me.txtcity.IsSourceFromTable = False
        Me.txtcity.IsSourceFromValueList = False
        Me.txtcity.IsUnique = False
        Me.txtcity.Location = New System.Drawing.Point(130, 102)
        Me.txtcity.MaxLength = 30
        Me.txtcity.MendatroryField = False
        Me.txtcity.MyLinkLable1 = Nothing
        Me.txtcity.MyLinkLable2 = Nothing
        Me.txtcity.Name = "txtcity"
        Me.txtcity.ReferenceFieldDesc = Nothing
        Me.txtcity.ReferenceFieldName = Nothing
        Me.txtcity.ReferenceTableName = Nothing
        Me.txtcity.Size = New System.Drawing.Size(224, 18)
        Me.txtcity.TabIndex = 4
        '
        'txtadd4
        '
        Me.txtadd4.CalculationExpression = Nothing
        Me.txtadd4.FieldCode = Nothing
        Me.txtadd4.FieldDesc = Nothing
        Me.txtadd4.FieldMaxLength = 0
        Me.txtadd4.FieldName = Nothing
        Me.txtadd4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd4.isCalculatedField = False
        Me.txtadd4.IsSourceFromTable = False
        Me.txtadd4.IsSourceFromValueList = False
        Me.txtadd4.IsUnique = False
        Me.txtadd4.Location = New System.Drawing.Point(130, 79)
        Me.txtadd4.MaxLength = 60
        Me.txtadd4.MendatroryField = False
        Me.txtadd4.MyLinkLable1 = Nothing
        Me.txtadd4.MyLinkLable2 = Nothing
        Me.txtadd4.Name = "txtadd4"
        Me.txtadd4.ReferenceFieldDesc = Nothing
        Me.txtadd4.ReferenceFieldName = Nothing
        Me.txtadd4.ReferenceTableName = Nothing
        Me.txtadd4.Size = New System.Drawing.Size(505, 18)
        Me.txtadd4.TabIndex = 3
        '
        'txtadd3
        '
        Me.txtadd3.CalculationExpression = Nothing
        Me.txtadd3.FieldCode = Nothing
        Me.txtadd3.FieldDesc = Nothing
        Me.txtadd3.FieldMaxLength = 0
        Me.txtadd3.FieldName = Nothing
        Me.txtadd3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd3.isCalculatedField = False
        Me.txtadd3.IsSourceFromTable = False
        Me.txtadd3.IsSourceFromValueList = False
        Me.txtadd3.IsUnique = False
        Me.txtadd3.Location = New System.Drawing.Point(130, 56)
        Me.txtadd3.MaxLength = 60
        Me.txtadd3.MendatroryField = False
        Me.txtadd3.MyLinkLable1 = Nothing
        Me.txtadd3.MyLinkLable2 = Nothing
        Me.txtadd3.Name = "txtadd3"
        Me.txtadd3.ReferenceFieldDesc = Nothing
        Me.txtadd3.ReferenceFieldName = Nothing
        Me.txtadd3.ReferenceTableName = Nothing
        Me.txtadd3.Size = New System.Drawing.Size(505, 18)
        Me.txtadd3.TabIndex = 2
        '
        'txtadd2
        '
        Me.txtadd2.CalculationExpression = Nothing
        Me.txtadd2.FieldCode = Nothing
        Me.txtadd2.FieldDesc = Nothing
        Me.txtadd2.FieldMaxLength = 0
        Me.txtadd2.FieldName = Nothing
        Me.txtadd2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd2.isCalculatedField = False
        Me.txtadd2.IsSourceFromTable = False
        Me.txtadd2.IsSourceFromValueList = False
        Me.txtadd2.IsUnique = False
        Me.txtadd2.Location = New System.Drawing.Point(130, 33)
        Me.txtadd2.MaxLength = 60
        Me.txtadd2.MendatroryField = False
        Me.txtadd2.MyLinkLable1 = Nothing
        Me.txtadd2.MyLinkLable2 = Nothing
        Me.txtadd2.Name = "txtadd2"
        Me.txtadd2.ReferenceFieldDesc = Nothing
        Me.txtadd2.ReferenceFieldName = Nothing
        Me.txtadd2.ReferenceTableName = Nothing
        Me.txtadd2.Size = New System.Drawing.Size(505, 18)
        Me.txtadd2.TabIndex = 1
        '
        'txtadd1
        '
        Me.txtadd1.CalculationExpression = Nothing
        Me.txtadd1.FieldCode = Nothing
        Me.txtadd1.FieldDesc = Nothing
        Me.txtadd1.FieldMaxLength = 0
        Me.txtadd1.FieldName = Nothing
        Me.txtadd1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd1.isCalculatedField = False
        Me.txtadd1.IsSourceFromTable = False
        Me.txtadd1.IsSourceFromValueList = False
        Me.txtadd1.IsUnique = False
        Me.txtadd1.Location = New System.Drawing.Point(130, 11)
        Me.txtadd1.MaxLength = 60
        Me.txtadd1.MendatroryField = False
        Me.txtadd1.MyLinkLable1 = Nothing
        Me.txtadd1.MyLinkLable2 = Nothing
        Me.txtadd1.Name = "txtadd1"
        Me.txtadd1.ReferenceFieldDesc = Nothing
        Me.txtadd1.ReferenceFieldName = Nothing
        Me.txtadd1.ReferenceTableName = Nothing
        Me.txtadd1.Size = New System.Drawing.Size(505, 18)
        Me.txtadd1.TabIndex = 0
        '
        'lblfax
        '
        Me.lblfax.FieldName = Nothing
        Me.lblfax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfax.Location = New System.Drawing.Point(13, 263)
        Me.lblfax.Name = "lblfax"
        Me.lblfax.Size = New System.Drawing.Size(68, 16)
        Me.lblfax.TabIndex = 11
        Me.lblfax.Text = "Fax Number"
        '
        'lblphone
        '
        Me.lblphone.FieldName = Nothing
        Me.lblphone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblphone.Location = New System.Drawing.Point(13, 219)
        Me.lblphone.Name = "lblphone"
        Me.lblphone.Size = New System.Drawing.Size(39, 16)
        Me.lblphone.TabIndex = 12
        Me.lblphone.Text = "Phone"
        '
        'lblcontact
        '
        Me.lblcontact.FieldName = Nothing
        Me.lblcontact.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcontact.Location = New System.Drawing.Point(13, 196)
        Me.lblcontact.Name = "lblcontact"
        Me.lblcontact.Size = New System.Drawing.Size(45, 16)
        Me.lblcontact.TabIndex = 13
        Me.lblcontact.Text = "Contact"
        '
        'lblcountry
        '
        Me.lblcountry.FieldName = Nothing
        Me.lblcountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcountry.Location = New System.Drawing.Point(13, 173)
        Me.lblcountry.Name = "lblcountry"
        Me.lblcountry.Size = New System.Drawing.Size(46, 16)
        Me.lblcountry.TabIndex = 14
        Me.lblcountry.Text = "Country"
        '
        'lblzip
        '
        Me.lblzip.FieldName = Nothing
        Me.lblzip.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblzip.Location = New System.Drawing.Point(13, 151)
        Me.lblzip.Name = "lblzip"
        Me.lblzip.Size = New System.Drawing.Size(87, 16)
        Me.lblzip.TabIndex = 15
        Me.lblzip.Text = "Zip/Postal Code"
        '
        'lblstate
        '
        Me.lblstate.FieldName = Nothing
        Me.lblstate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstate.Location = New System.Drawing.Point(13, 128)
        Me.lblstate.Name = "lblstate"
        Me.lblstate.Size = New System.Drawing.Size(33, 16)
        Me.lblstate.TabIndex = 16
        Me.lblstate.Text = "State"
        '
        'lblcity
        '
        Me.lblcity.FieldName = Nothing
        Me.lblcity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcity.Location = New System.Drawing.Point(13, 105)
        Me.lblcity.Name = "lblcity"
        Me.lblcity.Size = New System.Drawing.Size(26, 16)
        Me.lblcity.TabIndex = 17
        Me.lblcity.Text = "City"
        '
        'lbladdress
        '
        Me.lbladdress.FieldName = Nothing
        Me.lbladdress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladdress.Location = New System.Drawing.Point(13, 11)
        Me.lbladdress.Name = "lbladdress"
        Me.lbladdress.Size = New System.Drawing.Size(48, 16)
        Me.lbladdress.TabIndex = 18
        Me.lbladdress.Text = "Address"
        '
        'pageCheckPrinting
        '
        Me.pageCheckPrinting.Controls.Add(Me.gv1)
        Me.pageCheckPrinting.ItemSize = New System.Drawing.SizeF(90.0!, 26.0!)
        Me.pageCheckPrinting.Location = New System.Drawing.Point(10, 35)
        Me.pageCheckPrinting.Name = "pageCheckPrinting"
        Me.pageCheckPrinting.Size = New System.Drawing.Size(704, 341)
        Me.pageCheckPrinting.Text = "Check Printing"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.White
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ShowRowHeaderColumn = False
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(704, 341)
        Me.gv1.TabIndex = 19
        Me.gv1.TabStop = False
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.UcCheckSetting1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(128.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(704, 341)
        Me.RadPageViewPage1.Text = "Check Printing Setting"
        '
        'UcCheckSetting1
        '
        Me.UcCheckSetting1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCheckSetting1.Location = New System.Drawing.Point(0, 0)
        Me.UcCheckSetting1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.UcCheckSetting1.MinimumSize = New System.Drawing.Size(574, 305)
        Me.UcCheckSetting1.Name = "UcCheckSetting1"
        Me.UcCheckSetting1.Size = New System.Drawing.Size(704, 341)
        Me.UcCheckSetting1.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvPP)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(59.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(722, 336)
        Me.RadPageViewPage2.Text = "Charges"
        '
        'gvPP
        '
        Me.gvPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvPP.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvPP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPP.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvPP.ForeColor = System.Drawing.Color.Black
        Me.gvPP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvPP.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvPP.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvPP.MasterTemplate.AllowAddNewRow = False
        Me.gvPP.MasterTemplate.AutoGenerateColumns = False
        Me.gvPP.MasterTemplate.EnableGrouping = False
        Me.gvPP.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvPP.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPP.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gvPP.Name = "gvPP"
        Me.gvPP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvPP.ShowHeaderCellButtons = True
        Me.gvPP.Size = New System.Drawing.Size(722, 336)
        Me.gvPP.TabIndex = 2
        Me.gvPP.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvNEFTPerforma)
        Me.RadPageViewPage3.Controls.Add(Me.Panel1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(95.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(704, 341)
        Me.RadPageViewPage3.Text = "NEFT Performa"
        '
        'gvNEFTPerforma
        '
        Me.gvNEFTPerforma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvNEFTPerforma.Location = New System.Drawing.Point(0, 33)
        '
        '
        '
        Me.gvNEFTPerforma.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvNEFTPerforma.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvNEFTPerforma.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gvNEFTPerforma.Name = "gvNEFTPerforma"
        Me.gvNEFTPerforma.ShowHeaderCellButtons = True
        Me.gvNEFTPerforma.Size = New System.Drawing.Size(704, 308)
        Me.gvNEFTPerforma.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Controls.Add(Me.chkDefaultNEFTDBT)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(704, 33)
        Me.Panel1.TabIndex = 2
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(5, 7)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(68, 20)
        Me.RadButton2.TabIndex = 72
        Me.RadButton2.Text = "Reset"
        '
        'chkDefaultNEFTDBT
        '
        Me.chkDefaultNEFTDBT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDefaultNEFTDBT.Location = New System.Drawing.Point(151, 9)
        Me.chkDefaultNEFTDBT.Name = "chkDefaultNEFTDBT"
        Me.chkDefaultNEFTDBT.Size = New System.Drawing.Size(144, 16)
        Me.chkDefaultNEFTDBT.TabIndex = 71
        Me.chkDefaultNEFTDBT.Text = "Default NEFT DBT Bank"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(77, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 20)
        Me.RadButton1.TabIndex = 4
        Me.RadButton1.Text = "Add New"
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.Location = New System.Drawing.Point(656, 7)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(66, 18)
        Me.btnclear.TabIndex = 2
        Me.btnclear.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(76, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(5, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'fndbank
        '
        Me.fndbank.FieldName = Nothing
        Me.fndbank.Location = New System.Drawing.Point(68, 2)
        Me.fndbank.MendatroryField = True
        Me.fndbank.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndbank.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndbank.MyLinkLable1 = Me.lblbank
        Me.fndbank.MyLinkLable2 = Nothing
        Me.fndbank.MyMaxLength = 32767
        Me.fndbank.MyReadOnly = False
        Me.fndbank.Name = "fndbank"
        Me.fndbank.Size = New System.Drawing.Size(202, 18)
        Me.fndbank.TabIndex = 0
        Me.fndbank.Value = ""
        '
        'txtdes
        '
        Me.txtdes.CalculationExpression = Nothing
        Me.txtdes.FieldCode = Nothing
        Me.txtdes.FieldDesc = Nothing
        Me.txtdes.FieldMaxLength = 0
        Me.txtdes.FieldName = Nothing
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.isCalculatedField = False
        Me.txtdes.IsSourceFromTable = False
        Me.txtdes.IsSourceFromValueList = False
        Me.txtdes.IsUnique = False
        Me.txtdes.Location = New System.Drawing.Point(381, 2)
        Me.txtdes.MaxLength = 59
        Me.txtdes.MendatroryField = True
        Me.txtdes.MyLinkLable1 = Me.lbldes
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        Me.txtdes.ReferenceFieldDesc = Nothing
        Me.txtdes.ReferenceFieldName = Nothing
        Me.txtdes.ReferenceTableName = Nothing
        Me.txtdes.Size = New System.Drawing.Size(292, 18)
        Me.txtdes.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.bankstab)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclear)
        Me.SplitContainer1.Size = New System.Drawing.Size(725, 446)
        Me.SplitContainer1.SplitterDistance = 411
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblbank)
        Me.Panel2.Controls.Add(Me.fndbank)
        Me.Panel2.Controls.Add(Me.txtdes)
        Me.Panel2.Controls.Add(Me.btnnew)
        Me.Panel2.Controls.Add(Me.lbldes)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(725, 24)
        Me.Panel2.TabIndex = 6
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(148, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "&History"
        '
        'frmBankMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 466)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmBankMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Master"
        CType(Me.lblbank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bankstab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bankstab.ResumeLayout(False)
        Me.bankstabprofile.ResumeLayout(False)
        Me.bankstabprofile.PerformLayout()
        CType(Me.chkUnpaid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSettlementBankForAD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProvisionBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankClearingAcount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultBank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGatewayType.ResumeLayout(False)
        Me.pnlGatewayType.PerformLayout()
        CType(Me.cboGetewayType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtswiftcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtIbanno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttransferclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltransferclearing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFDPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLCCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChequeValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblwriteoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbacc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlbanktype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbanktype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbaccno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtwriteoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankacc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankaccno, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bankstabadd.ResumeLayout(False)
        Me.bankstabadd.PerformLayout()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMainBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkClearanceBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtphone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcontact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtzip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblphone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcontact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblzip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbladdress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageCheckPrinting.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvPP.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvNEFTPerforma.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvNEFTPerforma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultNEFTDBT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents bankstab As Telerik.WinControls.UI.RadPageView
    Friend WithEvents bankstabprofile As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents bankstabadd As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnclear As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtbankaccno As common.Controls.MyTextBox
    Friend WithEvents txtcredit As common.Controls.MyTextBox
    Friend WithEvents txtwriteoff As common.Controls.MyTextBox
    Friend WithEvents txtbankacc As common.Controls.MyTextBox
    Friend WithEvents txtadd2 As common.Controls.MyTextBox
    Friend WithEvents txtadd1 As common.Controls.MyTextBox
    Friend WithEvents txtadd4 As common.Controls.MyTextBox
    Friend WithEvents txtadd3 As common.Controls.MyTextBox
    Friend WithEvents txtfax As common.Controls.MyTextBox
    Friend WithEvents txtphone As common.Controls.MyTextBox
    Friend WithEvents txtcountry As common.Controls.MyTextBox
    Friend WithEvents txtcontact As common.Controls.MyTextBox
    Friend WithEvents txtzip As common.Controls.MyTextBox
    Friend WithEvents txtstate As common.Controls.MyTextBox
    Friend WithEvents txtcity As common.Controls.MyTextBox
    Friend WithEvents ToolTipbank As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents bankimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents bankexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents banlclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ddlbanktype As common.Controls.MyComboBox
    Friend WithEvents lblbank As common.Controls.MyLabel
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents lblbaccno As common.Controls.MyLabel
    Friend WithEvents lblbacc As common.Controls.MyLabel
    Friend WithEvents lblwriteoff As common.Controls.MyLabel
    Friend WithEvents lblcredit As common.Controls.MyLabel
    Friend WithEvents lblfax As common.Controls.MyLabel
    Friend WithEvents lblphone As common.Controls.MyLabel
    Friend WithEvents lblcontact As common.Controls.MyLabel
    Friend WithEvents lblcountry As common.Controls.MyLabel
    Friend WithEvents lblzip As common.Controls.MyLabel
    Friend WithEvents lblstate As common.Controls.MyLabel
    Friend WithEvents lblcity As common.Controls.MyLabel
    Friend WithEvents lbladdress As common.Controls.MyLabel
    Friend WithEvents lblbanktype As common.Controls.MyLabel
    Friend WithEvents fndbank As common.UserControls.txtNavigator
    Friend WithEvents fndbankacc As common.UserControls.txtFinder
    Friend WithEvents fndwriteoff As common.UserControls.txtFinder
    Friend WithEvents fndcredit As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pageCheckPrinting As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblChequeValidity As common.Controls.MyLabel
    Friend WithEvents txtChequeValidity As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFDPer As common.MyNumBox
    Friend WithEvents txtLCCreditLimit As common.MyNumBox
    Friend WithEvents fndSubAcc As common.UserControls.txtFinder
    Friend WithEvents txtSubAcc As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndtransferclearing As common.UserControls.txtFinder
    Friend WithEvents txttransferclearing As common.Controls.MyTextBox
    Friend WithEvents rdlbltransferclearing As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCheckSetting1 As XpertERPCommanServices.ucCheckSetting
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtswiftcode As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtIbanno As common.Controls.MyTextBox
    Friend WithEvents cboGetewayType As common.Controls.MyComboBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents pnlGatewayType As System.Windows.Forms.Panel
    Friend WithEvents chkDefaultBank As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents fndBranchName As common.UserControls.txtFinder
    Friend WithEvents chkClearanceBank As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtMainBankCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents TxtMainBankName As common.Controls.MyTextBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvPP As common.UserControls.MyRadGridView
    Friend WithEvents lblBankClearingAcount As common.Controls.MyTextBox
    Friend WithEvents fndBankOpeningClearingAcount As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents chkProvisionBank As RadCheckBox
    Friend WithEvents chkSettlementBankForAD As RadCheckBox
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents gvNEFTPerforma As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chkDefaultNEFTDBT As RadCheckBox
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtBankGroup As common.UserControls.txtFinder
    Friend WithEvents lblBankGroup As common.Controls.MyLabel
    Friend WithEvents chkUnpaid As RadCheckBox
End Class

