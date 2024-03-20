Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSecondaryCustomerMaster
    Inherits XpertERPEngine.FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cboBusinessType = New common.Controls.MyComboBox()
        Me.lblTransaction = New common.Controls.MyLabel()
        Me.lblBusinessType = New common.Controls.MyLabel()
        Me.dtpAggClose = New common.Controls.MyDateTimePicker()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpAggMade = New common.Controls.MyDateTimePicker()
        Me.txtParentCstmrNo = New common.Controls.MyTextBox()
        Me.lblParentCustDesc = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.txtParentCstNo = New common.UserControls.txtFinder()
        Me.CmbCustomerType = New common.Controls.MyComboBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblAliesName = New common.Controls.MyLabel()
        Me.txtCustomerName = New common.Controls.MyTextBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtAliesName = New common.Controls.MyTextBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.fndCustCurrency = New common.UserControls.txtFinder()
        Me.lblBaseCurrency = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndCustomer = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.CmbTransaction = New common.Controls.MyComboBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.pageCus = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtPhone2 = New common.Controls.MyTextBox()
        Me.txtPhone1 = New common.Controls.MyTextBox()
        Me.txtPinNo = New common.Controls.MyTextBox()
        Me.lblPinNo = New common.Controls.MyLabel()
        Me.fndCountry = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.TxtCountryName = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtStateName = New common.Controls.MyTextBox()
        Me.fndstate = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.fndCity = New common.UserControls.txtFinder()
        Me.fndCusgrp = New common.UserControls.txtFinder()
        Me.lblCusGrp = New common.Controls.MyLabel()
        Me.chkcredit = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.txtWeb = New common.Controls.MyTextBox()
        Me.txtfax = New common.Controls.MyTextBox()
        Me.txtEmail = New common.Controls.MyTextBox()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.txtCity = New common.Controls.MyTextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dtClosing = New common.Controls.MyDateTimePicker()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.chkInActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtAdd2 = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtAdd1 = New common.Controls.MyTextBox()
        Me.txtAdd3 = New common.Controls.MyTextBox()
        Me.txtCusgrp = New common.Controls.MyTextBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtContPhone = New common.Controls.MyTextBox()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.RadLabel41 = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtContactFax = New common.Controls.MyTextBox()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtContactWeb = New common.Controls.MyTextBox()
        Me.txtContactEmail = New common.Controls.MyTextBox()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.txtContactName = New common.Controls.MyTextBox()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GBGST = New Telerik.WinControls.UI.RadGroupBox()
        Me.ChkRegistered = New common.Controls.MyCheckBox()
        Me.txtGSTDegit = New common.Controls.MyTextBox()
        Me.lblGSTNo = New common.Controls.MyLabel()
        Me.txtGSTBlank = New common.Controls.MyTextBox()
        Me.txtGSTEntityNo = New common.Controls.MyTextBox()
        Me.txtGSTPANNO = New common.Controls.MyTextBox()
        Me.txtGstNo = New common.Controls.MyTextBox()
        Me.txtGstState = New common.Controls.MyTextBox()
        Me.ChkOther = New common.Controls.MyCheckBox()
        Me.ChkCheckCreditLimit = New Telerik.WinControls.UI.RadCheckBox()
        Me.txttempCreditLimitTo = New common.Controls.MyDateTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txttempCreditLimitFrom = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtTempCreditLimit = New common.Controls.MyTextBox()
        Me.fndPayCode = New common.UserControls.txtFinder()
        Me.RadLabel26 = New common.Controls.MyLabel()
        Me.fndAccntSet = New common.UserControls.txtFinder()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.fndTrmsCode = New common.UserControls.txtFinder()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.txtpan = New common.Controls.MyTextBox()
        Me.lblpan = New common.Controls.MyLabel()
        Me.drpformtype = New Telerik.WinControls.UI.RadDropDownList()
        Me.lbldivision = New common.Controls.MyLabel()
        Me.txtdivision = New common.Controls.MyTextBox()
        Me.lblcollectorate = New common.Controls.MyLabel()
        Me.txtcollect = New common.Controls.MyTextBox()
        Me.lblrange = New common.Controls.MyLabel()
        Me.lblecc = New common.Controls.MyLabel()
        Me.txtrange = New common.Controls.MyTextBox()
        Me.txtecc = New common.Controls.MyTextBox()
        Me.lblcst = New common.Controls.MyLabel()
        Me.txtcst = New common.Controls.MyTextBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndTxGrp = New common.UserControls.txtFinder()
        Me.RadLabel35 = New common.Controls.MyLabel()
        Me.grdTax = New common.UserControls.MyRadGridView()
        Me.txtTxGrp = New common.Controls.MyTextBox()
        Me.RadLabel30 = New common.Controls.MyLabel()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.txtCredit = New common.Controls.MyTextBox()
        Me.txtLstNo = New common.Controls.MyTextBox()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.RadLabel28 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.txtTinNo = New common.Controls.MyTextBox()
        Me.txtStaxNo = New common.Controls.MyTextBox()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtCrateOpeningQty = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.TxtCrateOpeningDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.fndZone = New common.UserControls.txtFinder()
        Me.lblZone = New common.Controls.MyLabel()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtpgfnd = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.chkpricegrpslctr = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.RadLabel42 = New common.Controls.MyLabel()
        Me.txtPriceCode = New common.UserControls.txtFinder()
        Me.txtPriceCodeNon = New common.UserControls.txtFinder()
        Me.fndroutegroup = New common.UserControls.txtFinder()
        Me.fndChannel = New common.UserControls.txtFinder()
        Me.fndSalePerson = New common.UserControls.txtFinder()
        Me.fndCusType = New common.UserControls.txtFinder()
        Me.fndRoute = New common.UserControls.txtFinder()
        Me.fndCusCategory = New common.UserControls.txtFinder()
        Me.cboCustomerClass = New common.Controls.MyComboBox()
        Me.RadLabel33 = New common.Controls.MyLabel()
        Me.txtroutegroup = New common.Controls.MyTextBox()
        Me.lblroutegrp = New common.Controls.MyLabel()
        Me.txtSalesPerson = New common.Controls.MyTextBox()
        Me.RadLabel34 = New common.Controls.MyLabel()
        Me.txtChannel = New common.Controls.MyTextBox()
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.txtRoute = New common.Controls.MyTextBox()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.RadLabel40 = New common.Controls.MyLabel()
        Me.RadLabel36 = New common.Controls.MyLabel()
        Me.txtAddInfo3 = New common.Controls.MyTextBox()
        Me.RadLabel37 = New common.Controls.MyLabel()
        Me.txtRemarks2 = New common.Controls.MyTextBox()
        Me.RadLabel38 = New common.Controls.MyLabel()
        Me.txtAddInfo2 = New common.Controls.MyTextBox()
        Me.txtAddInfo1 = New common.Controls.MyTextBox()
        Me.RadLabel39 = New common.Controls.MyLabel()
        Me.txtRemarks1 = New common.Controls.MyTextBox()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New XpertERPSalesAndDistribution.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.CrateAccounting = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvCrate = New common.UserControls.MyRadGridView()
        Me.Competitor = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvCompetitor = New common.UserControls.MyRadGridView()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImportCompetitor = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExportCompetitor = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cboBusinessType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBusinessType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAggClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAggMade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtParentCstmrNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParentCustDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbCustomerType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAliesName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAliesName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.pageCus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageCus.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCountryName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCusGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.dtClosing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCusgrp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.txtContPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactFax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactWeb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContactName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.GBGST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBGST.SuspendLayout()
        CType(Me.ChkRegistered, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGSTDegit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGSTNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGSTBlank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGSTEntityNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGSTPANNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGstNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGstState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCheckCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttempCreditLimitTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttempCreditLimitFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTempCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.drpformtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcollectorate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcollect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblecc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtecc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTax.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTxGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLstNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStaxNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.TxtCrateOpeningQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCrateOpeningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkpricegrpslctr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCustomerClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtroutegroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblroutegrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChannel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddInfo3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddInfo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.CrateAccounting.SuspendLayout()
        CType(Me.gvCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCrate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Competitor.SuspendLayout()
        CType(Me.gvCompetitor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCompetitor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboBusinessType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBusinessType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAggClose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAggMade)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtParentCstmrNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblParentCustDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel32)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtParentCstNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CmbCustomerType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAliesName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomerName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAliesName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransaction)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCustCurrency)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBaseCurrency)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CmbTransaction)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(956, 570)
        Me.SplitContainer1.SplitterDistance = 100
        Me.SplitContainer1.TabIndex = 2
        '
        'cboBusinessType
        '
        Me.cboBusinessType.AutoCompleteDisplayMember = Nothing
        Me.cboBusinessType.AutoCompleteValueMember = Nothing
        Me.cboBusinessType.CalculationExpression = Nothing
        Me.cboBusinessType.CaseSensitive = True
        Me.cboBusinessType.DropDownAnimationEnabled = True
        Me.cboBusinessType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBusinessType.FieldCode = Nothing
        Me.cboBusinessType.FieldDesc = Nothing
        Me.cboBusinessType.FieldMaxLength = 0
        Me.cboBusinessType.FieldName = Nothing
        Me.cboBusinessType.isCalculatedField = False
        Me.cboBusinessType.IsSourceFromTable = False
        Me.cboBusinessType.IsSourceFromValueList = False
        Me.cboBusinessType.IsUnique = False
        RadListDataItem1.Text = "Commercial"
        RadListDataItem2.Text = "Domestic"
        Me.cboBusinessType.Items.Add(RadListDataItem1)
        Me.cboBusinessType.Items.Add(RadListDataItem2)
        Me.cboBusinessType.Location = New System.Drawing.Point(710, 74)
        Me.cboBusinessType.MendatroryField = True
        Me.cboBusinessType.MyLinkLable1 = Me.lblTransaction
        Me.cboBusinessType.MyLinkLable2 = Nothing
        Me.cboBusinessType.Name = "cboBusinessType"
        Me.cboBusinessType.ReferenceFieldDesc = Nothing
        Me.cboBusinessType.ReferenceFieldName = Nothing
        Me.cboBusinessType.ReferenceTableName = Nothing
        Me.cboBusinessType.Size = New System.Drawing.Size(231, 20)
        Me.cboBusinessType.TabIndex = 111
        Me.cboBusinessType.Text = "Select"
        '
        'lblTransaction
        '
        Me.lblTransaction.FieldName = Nothing
        Me.lblTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransaction.Location = New System.Drawing.Point(592, 12)
        Me.lblTransaction.Name = "lblTransaction"
        Me.lblTransaction.Size = New System.Drawing.Size(94, 16)
        Me.lblTransaction.TabIndex = 91
        Me.lblTransaction.Text = "Transaction Type"
        '
        'lblBusinessType
        '
        Me.lblBusinessType.FieldName = Nothing
        Me.lblBusinessType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBusinessType.Location = New System.Drawing.Point(592, 78)
        Me.lblBusinessType.Name = "lblBusinessType"
        Me.lblBusinessType.Size = New System.Drawing.Size(80, 16)
        Me.lblBusinessType.TabIndex = 110
        Me.lblBusinessType.Text = "Business Type"
        '
        'dtpAggClose
        '
        Me.dtpAggClose.CalculationExpression = Nothing
        Me.dtpAggClose.CustomFormat = "dd/MM/yyyy"
        Me.dtpAggClose.FieldCode = Nothing
        Me.dtpAggClose.FieldDesc = Nothing
        Me.dtpAggClose.FieldMaxLength = 0
        Me.dtpAggClose.FieldName = Nothing
        Me.dtpAggClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAggClose.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAggClose.isCalculatedField = False
        Me.dtpAggClose.IsSourceFromTable = False
        Me.dtpAggClose.IsSourceFromValueList = False
        Me.dtpAggClose.IsUnique = False
        Me.dtpAggClose.Location = New System.Drawing.Point(495, 28)
        Me.dtpAggClose.MendatroryField = False
        Me.dtpAggClose.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAggClose.MyLinkLable1 = Me.MyLabel6
        Me.dtpAggClose.MyLinkLable2 = Nothing
        Me.dtpAggClose.Name = "dtpAggClose"
        Me.dtpAggClose.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAggClose.ReferenceFieldDesc = Nothing
        Me.dtpAggClose.ReferenceFieldName = Nothing
        Me.dtpAggClose.ReferenceTableName = Nothing
        Me.dtpAggClose.Size = New System.Drawing.Size(81, 18)
        Me.dtpAggClose.TabIndex = 109
        Me.dtpAggClose.TabStop = False
        Me.dtpAggClose.Text = "07/10/2016"
        Me.dtpAggClose.Value = New Date(2016, 10, 7, 0, 0, 0, 0)
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(287, 158)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(125, 16)
        Me.MyLabel6.TabIndex = 106
        Me.MyLabel6.Text = "Temp Credit Limit From"
        '
        'dtpAggMade
        '
        Me.dtpAggMade.CalculationExpression = Nothing
        Me.dtpAggMade.CustomFormat = "dd/MM/yyyy"
        Me.dtpAggMade.FieldCode = Nothing
        Me.dtpAggMade.FieldDesc = Nothing
        Me.dtpAggMade.FieldMaxLength = 0
        Me.dtpAggMade.FieldName = Nothing
        Me.dtpAggMade.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAggMade.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAggMade.isCalculatedField = False
        Me.dtpAggMade.IsSourceFromTable = False
        Me.dtpAggMade.IsSourceFromValueList = False
        Me.dtpAggMade.IsUnique = False
        Me.dtpAggMade.Location = New System.Drawing.Point(495, 5)
        Me.dtpAggMade.MendatroryField = False
        Me.dtpAggMade.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAggMade.MyLinkLable1 = Me.MyLabel6
        Me.dtpAggMade.MyLinkLable2 = Nothing
        Me.dtpAggMade.Name = "dtpAggMade"
        Me.dtpAggMade.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAggMade.ReferenceFieldDesc = Nothing
        Me.dtpAggMade.ReferenceFieldName = Nothing
        Me.dtpAggMade.ReferenceTableName = Nothing
        Me.dtpAggMade.Size = New System.Drawing.Size(81, 18)
        Me.dtpAggMade.TabIndex = 108
        Me.dtpAggMade.TabStop = False
        Me.dtpAggMade.Text = "07/10/2016"
        Me.dtpAggMade.Value = New Date(2016, 10, 7, 0, 0, 0, 0)
        '
        'txtParentCstmrNo
        '
        Me.txtParentCstmrNo.CalculationExpression = Nothing
        Me.txtParentCstmrNo.FieldCode = Nothing
        Me.txtParentCstmrNo.FieldDesc = Nothing
        Me.txtParentCstmrNo.FieldMaxLength = 0
        Me.txtParentCstmrNo.FieldName = Nothing
        Me.txtParentCstmrNo.isCalculatedField = False
        Me.txtParentCstmrNo.IsSourceFromTable = False
        Me.txtParentCstmrNo.IsSourceFromValueList = False
        Me.txtParentCstmrNo.IsUnique = False
        Me.txtParentCstmrNo.Location = New System.Drawing.Point(710, 53)
        Me.txtParentCstmrNo.MaxLength = 50
        Me.txtParentCstmrNo.MendatroryField = False
        Me.txtParentCstmrNo.MyLinkLable1 = Me.lblParentCustDesc
        Me.txtParentCstmrNo.MyLinkLable2 = Nothing
        Me.txtParentCstmrNo.Name = "txtParentCstmrNo"
        Me.txtParentCstmrNo.ReferenceFieldDesc = Nothing
        Me.txtParentCstmrNo.ReferenceFieldName = Nothing
        Me.txtParentCstmrNo.ReferenceTableName = Nothing
        Me.txtParentCstmrNo.Size = New System.Drawing.Size(231, 20)
        Me.txtParentCstmrNo.TabIndex = 106
        '
        'lblParentCustDesc
        '
        Me.lblParentCustDesc.FieldName = Nothing
        Me.lblParentCustDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParentCustDesc.Location = New System.Drawing.Point(592, 56)
        Me.lblParentCustDesc.Name = "lblParentCustDesc"
        Me.lblParentCustDesc.Size = New System.Drawing.Size(95, 16)
        Me.lblParentCustDesc.TabIndex = 107
        Me.lblParentCustDesc.Text = "Parent Cust Desc"
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(364, 52)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(113, 16)
        Me.RadLabel32.TabIndex = 105
        Me.RadLabel32.Text = "Parent Customer No."
        '
        'txtParentCstNo
        '
        Me.txtParentCstNo.AccessibleName = "txtParentCstNo"
        Me.txtParentCstNo.CalculationExpression = Nothing
        Me.txtParentCstNo.FieldCode = Nothing
        Me.txtParentCstNo.FieldDesc = Nothing
        Me.txtParentCstNo.FieldMaxLength = 0
        Me.txtParentCstNo.FieldName = Nothing
        Me.txtParentCstNo.isCalculatedField = False
        Me.txtParentCstNo.IsSourceFromTable = False
        Me.txtParentCstNo.IsSourceFromValueList = False
        Me.txtParentCstNo.IsUnique = False
        Me.txtParentCstNo.Location = New System.Drawing.Point(495, 51)
        Me.txtParentCstNo.MendatroryField = True
        Me.txtParentCstNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParentCstNo.MyLinkLable1 = Me.RadLabel32
        Me.txtParentCstNo.MyLinkLable2 = Nothing
        Me.txtParentCstNo.MyReadOnly = False
        Me.txtParentCstNo.MyShowMasterFormButton = False
        Me.txtParentCstNo.Name = "txtParentCstNo"
        Me.txtParentCstNo.ReferenceFieldDesc = Nothing
        Me.txtParentCstNo.ReferenceFieldName = Nothing
        Me.txtParentCstNo.ReferenceTableName = Nothing
        Me.txtParentCstNo.Size = New System.Drawing.Size(81, 20)
        Me.txtParentCstNo.TabIndex = 104
        Me.txtParentCstNo.Value = ""
        '
        'CmbCustomerType
        '
        Me.CmbCustomerType.AutoCompleteDisplayMember = Nothing
        Me.CmbCustomerType.AutoCompleteValueMember = Nothing
        Me.CmbCustomerType.CalculationExpression = Nothing
        Me.CmbCustomerType.CaseSensitive = True
        Me.CmbCustomerType.DropDownAnimationEnabled = True
        Me.CmbCustomerType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbCustomerType.FieldCode = Nothing
        Me.CmbCustomerType.FieldDesc = Nothing
        Me.CmbCustomerType.FieldMaxLength = 0
        Me.CmbCustomerType.FieldName = Nothing
        Me.CmbCustomerType.isCalculatedField = False
        Me.CmbCustomerType.IsSourceFromTable = False
        Me.CmbCustomerType.IsSourceFromValueList = False
        Me.CmbCustomerType.IsUnique = False
        RadListDataItem3.Text = "Retailer"
        RadListDataItem4.Text = "Distributor"
        Me.CmbCustomerType.Items.Add(RadListDataItem3)
        Me.CmbCustomerType.Items.Add(RadListDataItem4)
        Me.CmbCustomerType.Location = New System.Drawing.Point(710, 34)
        Me.CmbCustomerType.MendatroryField = True
        Me.CmbCustomerType.MyLinkLable1 = Me.lblTransaction
        Me.CmbCustomerType.MyLinkLable2 = Nothing
        Me.CmbCustomerType.Name = "CmbCustomerType"
        Me.CmbCustomerType.ReferenceFieldDesc = Nothing
        Me.CmbCustomerType.ReferenceFieldName = Nothing
        Me.CmbCustomerType.ReferenceTableName = Nothing
        Me.CmbCustomerType.Size = New System.Drawing.Size(231, 20)
        Me.CmbCustomerType.TabIndex = 103
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(592, 34)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel10.TabIndex = 102
        Me.MyLabel10.Text = "Parent Cust Type"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(12, 4)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(66, 16)
        Me.RadLabel1.TabIndex = 92
        Me.RadLabel1.Text = "Retailer No."
        '
        'lblAliesName
        '
        Me.lblAliesName.FieldName = Nothing
        Me.lblAliesName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAliesName.Location = New System.Drawing.Point(12, 49)
        Me.lblAliesName.Name = "lblAliesName"
        Me.lblAliesName.Size = New System.Drawing.Size(64, 16)
        Me.lblAliesName.TabIndex = 101
        Me.lblAliesName.Text = "Alias Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CalculationExpression = Nothing
        Me.txtCustomerName.FieldCode = Nothing
        Me.txtCustomerName.FieldDesc = Nothing
        Me.txtCustomerName.FieldMaxLength = 0
        Me.txtCustomerName.FieldName = Nothing
        Me.txtCustomerName.isCalculatedField = False
        Me.txtCustomerName.IsSourceFromTable = False
        Me.txtCustomerName.IsSourceFromValueList = False
        Me.txtCustomerName.IsUnique = False
        Me.txtCustomerName.Location = New System.Drawing.Point(113, 28)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.MendatroryField = True
        Me.txtCustomerName.MyLinkLable1 = Me.RadLabel4
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReferenceFieldDesc = Nothing
        Me.txtCustomerName.ReferenceFieldName = Nothing
        Me.txtCustomerName.ReferenceTableName = Nothing
        Me.txtCustomerName.Size = New System.Drawing.Size(245, 20)
        Me.txtCustomerName.TabIndex = 84
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(12, 28)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel4.TabIndex = 93
        Me.RadLabel4.Text = "Retailer Name"
        '
        'txtAliesName
        '
        Me.txtAliesName.CalculationExpression = Nothing
        Me.txtAliesName.FieldCode = Nothing
        Me.txtAliesName.FieldDesc = Nothing
        Me.txtAliesName.FieldMaxLength = 0
        Me.txtAliesName.FieldName = Nothing
        Me.txtAliesName.isCalculatedField = False
        Me.txtAliesName.IsSourceFromTable = False
        Me.txtAliesName.IsSourceFromValueList = False
        Me.txtAliesName.IsUnique = False
        Me.txtAliesName.Location = New System.Drawing.Point(113, 49)
        Me.txtAliesName.MaxLength = 50
        Me.txtAliesName.MendatroryField = False
        Me.txtAliesName.MyLinkLable1 = Me.lblAliesName
        Me.txtAliesName.MyLinkLable2 = Nothing
        Me.txtAliesName.Name = "txtAliesName"
        Me.txtAliesName.ReferenceFieldDesc = Nothing
        Me.txtAliesName.ReferenceFieldName = Nothing
        Me.txtAliesName.ReferenceTableName = Nothing
        Me.txtAliesName.Size = New System.Drawing.Size(245, 20)
        Me.txtAliesName.TabIndex = 100
        '
        'btnNew
        '
        Me.btnNew.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(333, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 80
        '
        'fndCustCurrency
        '
        Me.fndCustCurrency.CalculationExpression = Nothing
        Me.fndCustCurrency.FieldCode = Nothing
        Me.fndCustCurrency.FieldDesc = Nothing
        Me.fndCustCurrency.FieldMaxLength = 0
        Me.fndCustCurrency.FieldName = Nothing
        Me.fndCustCurrency.isCalculatedField = False
        Me.fndCustCurrency.IsSourceFromTable = False
        Me.fndCustCurrency.IsSourceFromValueList = False
        Me.fndCustCurrency.IsUnique = False
        Me.fndCustCurrency.Location = New System.Drawing.Point(841, 12)
        Me.fndCustCurrency.MendatroryField = False
        Me.fndCustCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustCurrency.MyLinkLable1 = Nothing
        Me.fndCustCurrency.MyLinkLable2 = Nothing
        Me.fndCustCurrency.MyReadOnly = False
        Me.fndCustCurrency.MyShowMasterFormButton = False
        Me.fndCustCurrency.Name = "fndCustCurrency"
        Me.fndCustCurrency.ReferenceFieldDesc = Nothing
        Me.fndCustCurrency.ReferenceFieldName = Nothing
        Me.fndCustCurrency.ReferenceTableName = Nothing
        Me.fndCustCurrency.Size = New System.Drawing.Size(100, 19)
        Me.fndCustCurrency.TabIndex = 88
        Me.fndCustCurrency.Value = ""
        '
        'lblBaseCurrency
        '
        Me.lblBaseCurrency.FieldName = Nothing
        Me.lblBaseCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseCurrency.Location = New System.Drawing.Point(786, 12)
        Me.lblBaseCurrency.Name = "lblBaseCurrency"
        Me.lblBaseCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblBaseCurrency.TabIndex = 98
        Me.lblBaseCurrency.Text = "Currency"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(362, 29)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(130, 16)
        Me.MyLabel2.TabIndex = 97
        Me.MyLabel2.Text = "Agreement Closing Date"
        '
        'fndCustomer
        '
        Me.fndCustomer.FieldName = Nothing
        Me.fndCustomer.Location = New System.Drawing.Point(113, 4)
        Me.fndCustomer.MendatroryField = True
        Me.fndCustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyMaxLength = 12
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(218, 21)
        Me.fndCustomer.TabIndex = 79
        Me.fndCustomer.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(362, 9)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel1.TabIndex = 96
        Me.MyLabel1.Text = "Agreement Made Date"
        '
        'CmbTransaction
        '
        Me.CmbTransaction.AutoCompleteDisplayMember = Nothing
        Me.CmbTransaction.AutoCompleteValueMember = Nothing
        Me.CmbTransaction.CalculationExpression = Nothing
        Me.CmbTransaction.CaseSensitive = True
        Me.CmbTransaction.DropDownAnimationEnabled = True
        Me.CmbTransaction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbTransaction.FieldCode = Nothing
        Me.CmbTransaction.FieldDesc = Nothing
        Me.CmbTransaction.FieldMaxLength = 0
        Me.CmbTransaction.FieldName = Nothing
        Me.CmbTransaction.isCalculatedField = False
        Me.CmbTransaction.IsSourceFromTable = False
        Me.CmbTransaction.IsSourceFromValueList = False
        Me.CmbTransaction.IsUnique = False
        RadListDataItem5.Text = "Retail"
        RadListDataItem6.Text = "Tax"
        Me.CmbTransaction.Items.Add(RadListDataItem5)
        Me.CmbTransaction.Items.Add(RadListDataItem6)
        Me.CmbTransaction.Location = New System.Drawing.Point(710, 12)
        Me.CmbTransaction.MendatroryField = True
        Me.CmbTransaction.MyLinkLable1 = Me.lblTransaction
        Me.CmbTransaction.MyLinkLable2 = Nothing
        Me.CmbTransaction.Name = "CmbTransaction"
        Me.CmbTransaction.ReferenceFieldDesc = Nothing
        Me.CmbTransaction.ReferenceFieldName = Nothing
        Me.CmbTransaction.ReferenceTableName = Nothing
        Me.CmbTransaction.Size = New System.Drawing.Size(74, 20)
        Me.CmbTransaction.TabIndex = 87
        Me.CmbTransaction.Text = "Select"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.pageCus)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Size = New System.Drawing.Size(956, 466)
        Me.SplitContainer2.SplitterDistance = 436
        Me.SplitContainer2.TabIndex = 0
        '
        'pageCus
        '
        Me.pageCus.AccessibleName = "txtParentCstmrNo"
        Me.pageCus.Controls.Add(Me.RadPageViewPage1)
        Me.pageCus.Controls.Add(Me.RadPageViewPage2)
        Me.pageCus.Controls.Add(Me.RadPageViewPage4)
        Me.pageCus.Controls.Add(Me.RadPageViewPage3)
        Me.pageCus.Controls.Add(Me.RadPageViewPage5)
        Me.pageCus.Controls.Add(Me.pvpCustomFields)
        Me.pageCus.Controls.Add(Me.Attachments)
        Me.pageCus.Controls.Add(Me.CrateAccounting)
        Me.pageCus.Controls.Add(Me.Competitor)
        Me.pageCus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pageCus.ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.Individual
        Me.pageCus.Location = New System.Drawing.Point(0, 0)
        Me.pageCus.Name = "pageCus"
        Me.pageCus.SelectedPage = Me.Competitor
        Me.pageCus.Size = New System.Drawing.Size(956, 436)
        Me.pageCus.TabIndex = 13
        CType(Me.pageCus.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.pageCus.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.Individual
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone2)
        Me.RadPageViewPage1.Controls.Add(Me.txtPhone1)
        Me.RadPageViewPage1.Controls.Add(Me.txtPinNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblPinNo)
        Me.RadPageViewPage1.Controls.Add(Me.fndCountry)
        Me.RadPageViewPage1.Controls.Add(Me.TxtCountryName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtStateName)
        Me.RadPageViewPage1.Controls.Add(Me.fndstate)
        Me.RadPageViewPage1.Controls.Add(Me.fndCity)
        Me.RadPageViewPage1.Controls.Add(Me.fndCusgrp)
        Me.RadPageViewPage1.Controls.Add(Me.chkcredit)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtWeb)
        Me.RadPageViewPage1.Controls.Add(Me.txtfax)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmail)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtCity)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd2)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd1)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblCusGrp)
        Me.RadPageViewPage1.Controls.Add(Me.txtCusgrp)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(56.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(935, 388)
        Me.RadPageViewPage1.Text = "Address"
        '
        'txtPhone2
        '
        Me.txtPhone2.CalculationExpression = Nothing
        Me.txtPhone2.FieldCode = Nothing
        Me.txtPhone2.FieldDesc = Nothing
        Me.txtPhone2.FieldMaxLength = 0
        Me.txtPhone2.FieldName = Nothing
        Me.txtPhone2.isCalculatedField = False
        Me.txtPhone2.IsSourceFromTable = False
        Me.txtPhone2.IsSourceFromValueList = False
        Me.txtPhone2.IsUnique = False
        Me.txtPhone2.Location = New System.Drawing.Point(110, 229)
        Me.txtPhone2.MaxLength = 15
        Me.txtPhone2.MendatroryField = False
        Me.txtPhone2.MyLinkLable1 = Nothing
        Me.txtPhone2.MyLinkLable2 = Nothing
        Me.txtPhone2.Name = "txtPhone2"
        Me.txtPhone2.ReferenceFieldDesc = Nothing
        Me.txtPhone2.ReferenceFieldName = Nothing
        Me.txtPhone2.ReferenceTableName = Nothing
        Me.txtPhone2.Size = New System.Drawing.Size(225, 20)
        Me.txtPhone2.TabIndex = 15
        '
        'txtPhone1
        '
        Me.txtPhone1.CalculationExpression = Nothing
        Me.txtPhone1.FieldCode = Nothing
        Me.txtPhone1.FieldDesc = Nothing
        Me.txtPhone1.FieldMaxLength = 0
        Me.txtPhone1.FieldName = Nothing
        Me.txtPhone1.isCalculatedField = False
        Me.txtPhone1.IsSourceFromTable = False
        Me.txtPhone1.IsSourceFromValueList = False
        Me.txtPhone1.IsUnique = False
        Me.txtPhone1.Location = New System.Drawing.Point(111, 206)
        Me.txtPhone1.MaxLength = 15
        Me.txtPhone1.MendatroryField = False
        Me.txtPhone1.MyLinkLable1 = Nothing
        Me.txtPhone1.MyLinkLable2 = Nothing
        Me.txtPhone1.Name = "txtPhone1"
        Me.txtPhone1.ReferenceFieldDesc = Nothing
        Me.txtPhone1.ReferenceFieldName = Nothing
        Me.txtPhone1.ReferenceTableName = Nothing
        Me.txtPhone1.Size = New System.Drawing.Size(225, 20)
        Me.txtPhone1.TabIndex = 15
        '
        'txtPinNo
        '
        Me.txtPinNo.CalculationExpression = Nothing
        Me.txtPinNo.FieldCode = Nothing
        Me.txtPinNo.FieldDesc = Nothing
        Me.txtPinNo.FieldMaxLength = 0
        Me.txtPinNo.FieldName = Nothing
        Me.txtPinNo.isCalculatedField = False
        Me.txtPinNo.IsSourceFromTable = False
        Me.txtPinNo.IsSourceFromValueList = False
        Me.txtPinNo.IsUnique = False
        Me.txtPinNo.Location = New System.Drawing.Point(434, 229)
        Me.txtPinNo.MaxLength = 15
        Me.txtPinNo.MendatroryField = False
        Me.txtPinNo.MyLinkLable1 = Nothing
        Me.txtPinNo.MyLinkLable2 = Nothing
        Me.txtPinNo.Name = "txtPinNo"
        Me.txtPinNo.ReferenceFieldDesc = Nothing
        Me.txtPinNo.ReferenceFieldName = Nothing
        Me.txtPinNo.ReferenceTableName = Nothing
        Me.txtPinNo.Size = New System.Drawing.Size(276, 20)
        Me.txtPinNo.TabIndex = 1358
        '
        'lblPinNo
        '
        Me.lblPinNo.FieldName = Nothing
        Me.lblPinNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPinNo.Location = New System.Drawing.Point(376, 232)
        Me.lblPinNo.Name = "lblPinNo"
        Me.lblPinNo.Size = New System.Drawing.Size(43, 16)
        Me.lblPinNo.TabIndex = 1359
        Me.lblPinNo.Text = "Pin No."
        '
        'fndCountry
        '
        Me.fndCountry.CalculationExpression = Nothing
        Me.fndCountry.FieldCode = Nothing
        Me.fndCountry.FieldDesc = Nothing
        Me.fndCountry.FieldMaxLength = 0
        Me.fndCountry.FieldName = Nothing
        Me.fndCountry.isCalculatedField = False
        Me.fndCountry.IsSourceFromTable = False
        Me.fndCountry.IsSourceFromValueList = False
        Me.fndCountry.IsUnique = False
        Me.fndCountry.Location = New System.Drawing.Point(111, 137)
        Me.fndCountry.MendatroryField = False
        Me.fndCountry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCountry.MyLinkLable1 = Me.RadLabel5
        Me.fndCountry.MyLinkLable2 = Nothing
        Me.fndCountry.MyReadOnly = False
        Me.fndCountry.MyShowMasterFormButton = False
        Me.fndCountry.Name = "fndCountry"
        Me.fndCountry.ReferenceFieldDesc = Nothing
        Me.fndCountry.ReferenceFieldName = Nothing
        Me.fndCountry.ReferenceTableName = Nothing
        Me.fndCountry.Size = New System.Drawing.Size(143, 19)
        Me.fndCountry.TabIndex = 8
        Me.fndCountry.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(9, 186)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(26, 16)
        Me.RadLabel5.TabIndex = 69
        Me.RadLabel5.Text = "City"
        '
        'TxtCountryName
        '
        Me.TxtCountryName.CalculationExpression = Nothing
        Me.TxtCountryName.FieldCode = Nothing
        Me.TxtCountryName.FieldDesc = Nothing
        Me.TxtCountryName.FieldMaxLength = 0
        Me.TxtCountryName.FieldName = Nothing
        Me.TxtCountryName.isCalculatedField = False
        Me.TxtCountryName.IsSourceFromTable = False
        Me.TxtCountryName.IsSourceFromValueList = False
        Me.TxtCountryName.IsUnique = False
        Me.TxtCountryName.Location = New System.Drawing.Point(265, 136)
        Me.TxtCountryName.MendatroryField = False
        Me.TxtCountryName.MyLinkLable1 = Nothing
        Me.TxtCountryName.MyLinkLable2 = Nothing
        Me.TxtCountryName.Name = "TxtCountryName"
        Me.TxtCountryName.ReadOnly = True
        Me.TxtCountryName.ReferenceFieldDesc = Nothing
        Me.TxtCountryName.ReferenceFieldName = Nothing
        Me.TxtCountryName.ReferenceTableName = Nothing
        Me.TxtCountryName.Size = New System.Drawing.Size(197, 20)
        Me.TxtCountryName.TabIndex = 9
        Me.TxtCountryName.TabStop = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(8, 139)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel7.TabIndex = 84
        Me.MyLabel7.Text = "Country"
        '
        'txtStateName
        '
        Me.txtStateName.CalculationExpression = Nothing
        Me.txtStateName.FieldCode = Nothing
        Me.txtStateName.FieldDesc = Nothing
        Me.txtStateName.FieldMaxLength = 0
        Me.txtStateName.FieldName = Nothing
        Me.txtStateName.isCalculatedField = False
        Me.txtStateName.IsSourceFromTable = False
        Me.txtStateName.IsSourceFromValueList = False
        Me.txtStateName.IsUnique = False
        Me.txtStateName.Location = New System.Drawing.Point(265, 159)
        Me.txtStateName.MendatroryField = False
        Me.txtStateName.MyLinkLable1 = Nothing
        Me.txtStateName.MyLinkLable2 = Nothing
        Me.txtStateName.Name = "txtStateName"
        Me.txtStateName.ReadOnly = True
        Me.txtStateName.ReferenceFieldDesc = Nothing
        Me.txtStateName.ReferenceFieldName = Nothing
        Me.txtStateName.ReferenceTableName = Nothing
        Me.txtStateName.Size = New System.Drawing.Size(197, 20)
        Me.txtStateName.TabIndex = 11
        Me.txtStateName.TabStop = False
        '
        'fndstate
        '
        Me.fndstate.CalculationExpression = Nothing
        Me.fndstate.FieldCode = Nothing
        Me.fndstate.FieldDesc = Nothing
        Me.fndstate.FieldMaxLength = 0
        Me.fndstate.FieldName = Nothing
        Me.fndstate.isCalculatedField = False
        Me.fndstate.IsSourceFromTable = False
        Me.fndstate.IsSourceFromValueList = False
        Me.fndstate.IsUnique = False
        Me.fndstate.Location = New System.Drawing.Point(111, 160)
        Me.fndstate.MendatroryField = False
        Me.fndstate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstate.MyLinkLable1 = Me.RadLabel6
        Me.fndstate.MyLinkLable2 = Nothing
        Me.fndstate.MyReadOnly = False
        Me.fndstate.MyShowMasterFormButton = False
        Me.fndstate.Name = "fndstate"
        Me.fndstate.ReferenceFieldDesc = Nothing
        Me.fndstate.ReferenceFieldName = Nothing
        Me.fndstate.ReferenceTableName = Nothing
        Me.fndstate.Size = New System.Drawing.Size(143, 19)
        Me.fndstate.TabIndex = 10
        Me.fndstate.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(9, 163)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel6.TabIndex = 70
        Me.RadLabel6.Text = "State"
        '
        'fndCity
        '
        Me.fndCity.CalculationExpression = Nothing
        Me.fndCity.FieldCode = Nothing
        Me.fndCity.FieldDesc = Nothing
        Me.fndCity.FieldMaxLength = 0
        Me.fndCity.FieldName = Nothing
        Me.fndCity.isCalculatedField = False
        Me.fndCity.IsSourceFromTable = False
        Me.fndCity.IsSourceFromValueList = False
        Me.fndCity.IsUnique = False
        Me.fndCity.Location = New System.Drawing.Point(111, 183)
        Me.fndCity.MendatroryField = False
        Me.fndCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCity.MyLinkLable1 = Me.RadLabel5
        Me.fndCity.MyLinkLable2 = Nothing
        Me.fndCity.MyReadOnly = False
        Me.fndCity.MyShowMasterFormButton = False
        Me.fndCity.Name = "fndCity"
        Me.fndCity.ReferenceFieldDesc = Nothing
        Me.fndCity.ReferenceFieldName = Nothing
        Me.fndCity.ReferenceTableName = Nothing
        Me.fndCity.Size = New System.Drawing.Size(143, 19)
        Me.fndCity.TabIndex = 12
        Me.fndCity.Value = ""
        '
        'fndCusgrp
        '
        Me.fndCusgrp.CalculationExpression = Nothing
        Me.fndCusgrp.FieldCode = Nothing
        Me.fndCusgrp.FieldDesc = Nothing
        Me.fndCusgrp.FieldMaxLength = 0
        Me.fndCusgrp.FieldName = Nothing
        Me.fndCusgrp.isCalculatedField = False
        Me.fndCusgrp.IsSourceFromTable = False
        Me.fndCusgrp.IsSourceFromValueList = False
        Me.fndCusgrp.IsUnique = False
        Me.fndCusgrp.Location = New System.Drawing.Point(111, 6)
        Me.fndCusgrp.MendatroryField = True
        Me.fndCusgrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCusgrp.MyLinkLable1 = Me.lblCusGrp
        Me.fndCusgrp.MyLinkLable2 = Nothing
        Me.fndCusgrp.MyReadOnly = False
        Me.fndCusgrp.MyShowMasterFormButton = False
        Me.fndCusgrp.Name = "fndCusgrp"
        Me.fndCusgrp.ReferenceFieldDesc = Nothing
        Me.fndCusgrp.ReferenceFieldName = Nothing
        Me.fndCusgrp.ReferenceTableName = Nothing
        Me.fndCusgrp.Size = New System.Drawing.Size(143, 19)
        Me.fndCusgrp.TabIndex = 0
        Me.fndCusgrp.Value = ""
        '
        'lblCusGrp
        '
        Me.lblCusGrp.FieldName = Nothing
        Me.lblCusGrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCusGrp.Location = New System.Drawing.Point(9, 8)
        Me.lblCusGrp.Name = "lblCusGrp"
        Me.lblCusGrp.Size = New System.Drawing.Size(68, 16)
        Me.lblCusGrp.TabIndex = 67
        Me.lblCusGrp.Text = "Group Code"
        '
        'chkcredit
        '
        Me.chkcredit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcredit.Location = New System.Drawing.Point(606, 38)
        Me.chkcredit.Name = "chkcredit"
        Me.chkcredit.Size = New System.Drawing.Size(93, 16)
        Me.chkcredit.TabIndex = 4
        Me.chkcredit.Text = "Credit Retailer"
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(9, 279)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel12.TabIndex = 75
        Me.RadLabel12.Text = "WebSite"
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(9, 256)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel11.TabIndex = 75
        Me.RadLabel11.Text = "E-Mail"
        '
        'txtWeb
        '
        Me.txtWeb.CalculationExpression = Nothing
        Me.txtWeb.FieldCode = Nothing
        Me.txtWeb.FieldDesc = Nothing
        Me.txtWeb.FieldMaxLength = 0
        Me.txtWeb.FieldName = Nothing
        Me.txtWeb.isCalculatedField = False
        Me.txtWeb.IsSourceFromTable = False
        Me.txtWeb.IsSourceFromValueList = False
        Me.txtWeb.IsUnique = False
        Me.txtWeb.Location = New System.Drawing.Point(110, 275)
        Me.txtWeb.MaxLength = 50
        Me.txtWeb.MendatroryField = False
        Me.txtWeb.MyLinkLable1 = Me.RadLabel12
        Me.txtWeb.MyLinkLable2 = Nothing
        Me.txtWeb.Name = "txtWeb"
        Me.txtWeb.ReferenceFieldDesc = Nothing
        Me.txtWeb.ReferenceFieldName = Nothing
        Me.txtWeb.ReferenceTableName = Nothing
        Me.txtWeb.Size = New System.Drawing.Size(599, 20)
        Me.txtWeb.TabIndex = 17
        '
        'txtfax
        '
        Me.txtfax.CalculationExpression = Nothing
        Me.txtfax.FieldCode = Nothing
        Me.txtfax.FieldDesc = Nothing
        Me.txtfax.FieldMaxLength = 0
        Me.txtfax.FieldName = Nothing
        Me.txtfax.isCalculatedField = False
        Me.txtfax.IsSourceFromTable = False
        Me.txtfax.IsSourceFromValueList = False
        Me.txtfax.IsUnique = False
        Me.txtfax.Location = New System.Drawing.Point(434, 206)
        Me.txtfax.MaxLength = 15
        Me.txtfax.MendatroryField = False
        Me.txtfax.MyLinkLable1 = Nothing
        Me.txtfax.MyLinkLable2 = Nothing
        Me.txtfax.Name = "txtfax"
        Me.txtfax.ReferenceFieldDesc = Nothing
        Me.txtfax.ReferenceFieldName = Nothing
        Me.txtfax.ReferenceTableName = Nothing
        Me.txtfax.Size = New System.Drawing.Size(276, 20)
        Me.txtfax.TabIndex = 14
        '
        'txtEmail
        '
        Me.txtEmail.CalculationExpression = Nothing
        Me.txtEmail.FieldCode = Nothing
        Me.txtEmail.FieldDesc = Nothing
        Me.txtEmail.FieldMaxLength = 0
        Me.txtEmail.FieldName = Nothing
        Me.txtEmail.isCalculatedField = False
        Me.txtEmail.IsSourceFromTable = False
        Me.txtEmail.IsSourceFromValueList = False
        Me.txtEmail.IsUnique = False
        Me.txtEmail.Location = New System.Drawing.Point(110, 252)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.MendatroryField = False
        Me.txtEmail.MyLinkLable1 = Me.RadLabel11
        Me.txtEmail.MyLinkLable2 = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReferenceFieldDesc = Nothing
        Me.txtEmail.ReferenceFieldName = Nothing
        Me.txtEmail.ReferenceTableName = Nothing
        Me.txtEmail.Size = New System.Drawing.Size(599, 20)
        Me.txtEmail.TabIndex = 16
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(376, 210)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(25, 16)
        Me.RadLabel10.TabIndex = 76
        Me.RadLabel10.Text = "Fax"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(9, 233)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel8.TabIndex = 76
        Me.RadLabel8.Text = "Phone2"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(9, 209)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel9.TabIndex = 74
        Me.RadLabel9.Text = "Phone1"
        '
        'txtCity
        '
        Me.txtCity.CalculationExpression = Nothing
        Me.txtCity.FieldCode = Nothing
        Me.txtCity.FieldDesc = Nothing
        Me.txtCity.FieldMaxLength = 0
        Me.txtCity.FieldName = Nothing
        Me.txtCity.isCalculatedField = False
        Me.txtCity.IsSourceFromTable = False
        Me.txtCity.IsSourceFromValueList = False
        Me.txtCity.IsUnique = False
        Me.txtCity.Location = New System.Drawing.Point(265, 182)
        Me.txtCity.MendatroryField = False
        Me.txtCity.MyLinkLable1 = Nothing
        Me.txtCity.MyLinkLable2 = Nothing
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.ReferenceFieldDesc = Nothing
        Me.txtCity.ReferenceFieldName = Nothing
        Me.txtCity.ReferenceTableName = Nothing
        Me.txtCity.Size = New System.Drawing.Size(197, 20)
        Me.txtCity.TabIndex = 1356
        Me.txtCity.TabStop = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.dtClosing)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel18)
        Me.RadGroupBox2.Controls.Add(Me.chkInActive)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(111, 30)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(282, 33)
        Me.RadGroupBox2.TabIndex = 2
        '
        'dtClosing
        '
        Me.dtClosing.CalculationExpression = Nothing
        Me.dtClosing.CustomFormat = "dd/MM/yyyy"
        Me.dtClosing.FieldCode = Nothing
        Me.dtClosing.FieldDesc = Nothing
        Me.dtClosing.FieldMaxLength = 0
        Me.dtClosing.FieldName = Nothing
        Me.dtClosing.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtClosing.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtClosing.isCalculatedField = False
        Me.dtClosing.IsSourceFromTable = False
        Me.dtClosing.IsSourceFromValueList = False
        Me.dtClosing.IsUnique = False
        Me.dtClosing.Location = New System.Drawing.Point(188, 8)
        Me.dtClosing.MendatroryField = False
        Me.dtClosing.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtClosing.MyLinkLable1 = Nothing
        Me.dtClosing.MyLinkLable2 = Nothing
        Me.dtClosing.Name = "dtClosing"
        Me.dtClosing.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtClosing.ReferenceFieldDesc = Nothing
        Me.dtClosing.ReferenceFieldName = Nothing
        Me.dtClosing.ReferenceTableName = Nothing
        Me.dtClosing.Size = New System.Drawing.Size(81, 18)
        Me.dtClosing.TabIndex = 2
        Me.dtClosing.TabStop = False
        Me.dtClosing.Text = "17/05/2011"
        Me.dtClosing.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(105, 8)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel18.TabIndex = 1
        Me.RadLabel18.Text = "InActive Date"
        '
        'chkInActive
        '
        Me.chkInActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInActive.Location = New System.Drawing.Point(13, 8)
        Me.chkInActive.Name = "chkInActive"
        Me.chkInActive.Size = New System.Drawing.Size(60, 16)
        Me.chkInActive.TabIndex = 0
        Me.chkInActive.Text = "InActive"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkHold)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(399, 30)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(197, 33)
        Me.RadGroupBox1.TabIndex = 3
        '
        'chkHold
        '
        Me.chkHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHold.Location = New System.Drawing.Point(6, 8)
        Me.chkHold.Name = "chkHold"
        Me.chkHold.Size = New System.Drawing.Size(179, 16)
        Me.chkHold.TabIndex = 0
        Me.chkHold.Text = "Full && Final Settlement Pending"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(9, 38)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel3.TabIndex = 69
        Me.RadLabel3.Text = "Status"
        '
        'txtAdd2
        '
        Me.txtAdd2.CalculationExpression = Nothing
        Me.txtAdd2.FieldCode = Nothing
        Me.txtAdd2.FieldDesc = Nothing
        Me.txtAdd2.FieldMaxLength = 0
        Me.txtAdd2.FieldName = Nothing
        Me.txtAdd2.isCalculatedField = False
        Me.txtAdd2.IsSourceFromTable = False
        Me.txtAdd2.IsSourceFromValueList = False
        Me.txtAdd2.IsUnique = False
        Me.txtAdd2.Location = New System.Drawing.Point(111, 89)
        Me.txtAdd2.MaxLength = 75
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.ReferenceFieldDesc = Nothing
        Me.txtAdd2.ReferenceFieldName = Nothing
        Me.txtAdd2.ReferenceTableName = Nothing
        Me.txtAdd2.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd2.TabIndex = 6
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(9, 66)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel2.TabIndex = 68
        Me.RadLabel2.Text = "Address"
        '
        'txtAdd1
        '
        Me.txtAdd1.CalculationExpression = Nothing
        Me.txtAdd1.FieldCode = Nothing
        Me.txtAdd1.FieldDesc = Nothing
        Me.txtAdd1.FieldMaxLength = 0
        Me.txtAdd1.FieldName = Nothing
        Me.txtAdd1.isCalculatedField = False
        Me.txtAdd1.IsSourceFromTable = False
        Me.txtAdd1.IsSourceFromValueList = False
        Me.txtAdd1.IsUnique = False
        Me.txtAdd1.Location = New System.Drawing.Point(111, 66)
        Me.txtAdd1.MaxLength = 75
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.ReferenceFieldDesc = Nothing
        Me.txtAdd1.ReferenceFieldName = Nothing
        Me.txtAdd1.ReferenceTableName = Nothing
        Me.txtAdd1.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd1.TabIndex = 5
        '
        'txtAdd3
        '
        Me.txtAdd3.CalculationExpression = Nothing
        Me.txtAdd3.FieldCode = Nothing
        Me.txtAdd3.FieldDesc = Nothing
        Me.txtAdd3.FieldMaxLength = 0
        Me.txtAdd3.FieldName = Nothing
        Me.txtAdd3.isCalculatedField = False
        Me.txtAdd3.IsSourceFromTable = False
        Me.txtAdd3.IsSourceFromValueList = False
        Me.txtAdd3.IsUnique = False
        Me.txtAdd3.Location = New System.Drawing.Point(111, 113)
        Me.txtAdd3.MaxLength = 75
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.RadLabel2
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.ReferenceFieldDesc = Nothing
        Me.txtAdd3.ReferenceFieldName = Nothing
        Me.txtAdd3.ReferenceTableName = Nothing
        Me.txtAdd3.Size = New System.Drawing.Size(599, 20)
        Me.txtAdd3.TabIndex = 7
        '
        'txtCusgrp
        '
        Me.txtCusgrp.CalculationExpression = Nothing
        Me.txtCusgrp.FieldCode = Nothing
        Me.txtCusgrp.FieldDesc = Nothing
        Me.txtCusgrp.FieldMaxLength = 0
        Me.txtCusgrp.FieldName = Nothing
        Me.txtCusgrp.isCalculatedField = False
        Me.txtCusgrp.IsSourceFromTable = False
        Me.txtCusgrp.IsSourceFromValueList = False
        Me.txtCusgrp.IsUnique = False
        Me.txtCusgrp.Location = New System.Drawing.Point(260, 5)
        Me.txtCusgrp.MaxLength = 50
        Me.txtCusgrp.MendatroryField = False
        Me.txtCusgrp.MyLinkLable1 = Nothing
        Me.txtCusgrp.MyLinkLable2 = Nothing
        Me.txtCusgrp.Name = "txtCusgrp"
        Me.txtCusgrp.ReferenceFieldDesc = Nothing
        Me.txtCusgrp.ReferenceFieldName = Nothing
        Me.txtCusgrp.ReferenceTableName = Nothing
        Me.txtCusgrp.Size = New System.Drawing.Size(449, 20)
        Me.txtCusgrp.TabIndex = 1
        Me.txtCusgrp.TabStop = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtContPhone)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel41)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel16)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactFax)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactWeb)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactEmail)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage2.Controls.Add(Me.txtContactName)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(92.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(935, 388)
        Me.RadPageViewPage2.Text = "Contact Person"
        '
        'txtContPhone
        '
        Me.txtContPhone.CalculationExpression = Nothing
        Me.txtContPhone.FieldCode = Nothing
        Me.txtContPhone.FieldDesc = Nothing
        Me.txtContPhone.FieldMaxLength = 0
        Me.txtContPhone.FieldName = Nothing
        Me.txtContPhone.isCalculatedField = False
        Me.txtContPhone.IsSourceFromTable = False
        Me.txtContPhone.IsSourceFromValueList = False
        Me.txtContPhone.IsUnique = False
        Me.txtContPhone.Location = New System.Drawing.Point(100, 45)
        Me.txtContPhone.MaxLength = 15
        Me.txtContPhone.MendatroryField = False
        Me.txtContPhone.MyLinkLable1 = Me.RadLabel16
        Me.txtContPhone.MyLinkLable2 = Nothing
        Me.txtContPhone.Name = "txtContPhone"
        Me.txtContPhone.ReferenceFieldDesc = Nothing
        Me.txtContPhone.ReferenceFieldName = Nothing
        Me.txtContPhone.ReferenceTableName = Nothing
        Me.txtContPhone.Size = New System.Drawing.Size(229, 20)
        Me.txtContPhone.TabIndex = 3
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(8, 76)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(25, 16)
        Me.RadLabel16.TabIndex = 81
        Me.RadLabel16.Text = "Fax"
        '
        'RadLabel41
        '
        Me.RadLabel41.FieldName = Nothing
        Me.RadLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel41.Location = New System.Drawing.Point(8, 46)
        Me.RadLabel41.Name = "RadLabel41"
        Me.RadLabel41.Size = New System.Drawing.Size(39, 16)
        Me.RadLabel41.TabIndex = 83
        Me.RadLabel41.Text = "Phone"
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(8, 128)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel14.TabIndex = 78
        Me.RadLabel14.Text = "WebSite"
        '
        'txtContactFax
        '
        Me.txtContactFax.CalculationExpression = Nothing
        Me.txtContactFax.FieldCode = Nothing
        Me.txtContactFax.FieldDesc = Nothing
        Me.txtContactFax.FieldMaxLength = 0
        Me.txtContactFax.FieldName = Nothing
        Me.txtContactFax.isCalculatedField = False
        Me.txtContactFax.IsSourceFromTable = False
        Me.txtContactFax.IsSourceFromValueList = False
        Me.txtContactFax.IsUnique = False
        Me.txtContactFax.Location = New System.Drawing.Point(100, 73)
        Me.txtContactFax.MaxLength = 15
        Me.txtContactFax.MendatroryField = False
        Me.txtContactFax.MyLinkLable1 = Me.RadLabel16
        Me.txtContactFax.MyLinkLable2 = Nothing
        Me.txtContactFax.Name = "txtContactFax"
        Me.txtContactFax.ReferenceFieldDesc = Nothing
        Me.txtContactFax.ReferenceFieldName = Nothing
        Me.txtContactFax.ReferenceTableName = Nothing
        Me.txtContactFax.Size = New System.Drawing.Size(229, 20)
        Me.txtContactFax.TabIndex = 2
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(8, 102)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(38, 16)
        Me.RadLabel15.TabIndex = 79
        Me.RadLabel15.Text = "E-Mail"
        '
        'txtContactWeb
        '
        Me.txtContactWeb.CalculationExpression = Nothing
        Me.txtContactWeb.FieldCode = Nothing
        Me.txtContactWeb.FieldDesc = Nothing
        Me.txtContactWeb.FieldMaxLength = 0
        Me.txtContactWeb.FieldName = Nothing
        Me.txtContactWeb.isCalculatedField = False
        Me.txtContactWeb.IsSourceFromTable = False
        Me.txtContactWeb.IsSourceFromValueList = False
        Me.txtContactWeb.IsUnique = False
        Me.txtContactWeb.Location = New System.Drawing.Point(100, 126)
        Me.txtContactWeb.MaxLength = 50
        Me.txtContactWeb.MendatroryField = False
        Me.txtContactWeb.MyLinkLable1 = Me.RadLabel14
        Me.txtContactWeb.MyLinkLable2 = Nothing
        Me.txtContactWeb.Name = "txtContactWeb"
        Me.txtContactWeb.ReferenceFieldDesc = Nothing
        Me.txtContactWeb.ReferenceFieldName = Nothing
        Me.txtContactWeb.ReferenceTableName = Nothing
        Me.txtContactWeb.Size = New System.Drawing.Size(586, 20)
        Me.txtContactWeb.TabIndex = 4
        '
        'txtContactEmail
        '
        Me.txtContactEmail.CalculationExpression = Nothing
        Me.txtContactEmail.FieldCode = Nothing
        Me.txtContactEmail.FieldDesc = Nothing
        Me.txtContactEmail.FieldMaxLength = 0
        Me.txtContactEmail.FieldName = Nothing
        Me.txtContactEmail.isCalculatedField = False
        Me.txtContactEmail.IsSourceFromTable = False
        Me.txtContactEmail.IsSourceFromValueList = False
        Me.txtContactEmail.IsUnique = False
        Me.txtContactEmail.Location = New System.Drawing.Point(100, 99)
        Me.txtContactEmail.MaxLength = 50
        Me.txtContactEmail.MendatroryField = False
        Me.txtContactEmail.MyLinkLable1 = Me.RadLabel15
        Me.txtContactEmail.MyLinkLable2 = Nothing
        Me.txtContactEmail.Name = "txtContactEmail"
        Me.txtContactEmail.ReferenceFieldDesc = Nothing
        Me.txtContactEmail.ReferenceFieldName = Nothing
        Me.txtContactEmail.ReferenceTableName = Nothing
        Me.txtContactEmail.Size = New System.Drawing.Size(586, 20)
        Me.txtContactEmail.TabIndex = 3
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(8, 19)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel13.TabIndex = 66
        Me.RadLabel13.Text = "Contact Name"
        '
        'txtContactName
        '
        Me.txtContactName.CalculationExpression = Nothing
        Me.txtContactName.FieldCode = Nothing
        Me.txtContactName.FieldDesc = Nothing
        Me.txtContactName.FieldMaxLength = 0
        Me.txtContactName.FieldName = Nothing
        Me.txtContactName.isCalculatedField = False
        Me.txtContactName.IsSourceFromTable = False
        Me.txtContactName.IsSourceFromValueList = False
        Me.txtContactName.IsUnique = False
        Me.txtContactName.Location = New System.Drawing.Point(100, 19)
        Me.txtContactName.MaxLength = 50
        Me.txtContactName.MendatroryField = False
        Me.txtContactName.MyLinkLable1 = Me.RadLabel13
        Me.txtContactName.MyLinkLable2 = Nothing
        Me.txtContactName.Name = "txtContactName"
        Me.txtContactName.ReferenceFieldDesc = Nothing
        Me.txtContactName.ReferenceFieldName = Nothing
        Me.txtContactName.ReferenceTableName = Nothing
        Me.txtContactName.Size = New System.Drawing.Size(586, 20)
        Me.txtContactName.TabIndex = 0
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.GBGST)
        Me.RadPageViewPage4.Controls.Add(Me.ChkOther)
        Me.RadPageViewPage4.Controls.Add(Me.ChkCheckCreditLimit)
        Me.RadPageViewPage4.Controls.Add(Me.txttempCreditLimitTo)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage4.Controls.Add(Me.txttempCreditLimitFrom)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage4.Controls.Add(Me.txtTempCreditLimit)
        Me.RadPageViewPage4.Controls.Add(Me.fndPayCode)
        Me.RadPageViewPage4.Controls.Add(Me.fndAccntSet)
        Me.RadPageViewPage4.Controls.Add(Me.fndTrmsCode)
        Me.RadPageViewPage4.Controls.Add(Me.txtpan)
        Me.RadPageViewPage4.Controls.Add(Me.drpformtype)
        Me.RadPageViewPage4.Controls.Add(Me.lbldivision)
        Me.RadPageViewPage4.Controls.Add(Me.txtdivision)
        Me.RadPageViewPage4.Controls.Add(Me.lblpan)
        Me.RadPageViewPage4.Controls.Add(Me.lblcollectorate)
        Me.RadPageViewPage4.Controls.Add(Me.txtcollect)
        Me.RadPageViewPage4.Controls.Add(Me.lblrange)
        Me.RadPageViewPage4.Controls.Add(Me.lblecc)
        Me.RadPageViewPage4.Controls.Add(Me.txtrange)
        Me.RadPageViewPage4.Controls.Add(Me.txtecc)
        Me.RadPageViewPage4.Controls.Add(Me.lblcst)
        Me.RadPageViewPage4.Controls.Add(Me.txtcst)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel30)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage4.Controls.Add(Me.txtCredit)
        Me.RadPageViewPage4.Controls.Add(Me.txtLstNo)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel31)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel28)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.txtTinNo)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel26)
        Me.RadPageViewPage4.Controls.Add(Me.txtStaxNo)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel23)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(54.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(935, 388)
        Me.RadPageViewPage4.Text = "Process"
        '
        'GBGST
        '
        Me.GBGST.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GBGST.Controls.Add(Me.ChkRegistered)
        Me.GBGST.Controls.Add(Me.txtGSTDegit)
        Me.GBGST.Controls.Add(Me.txtGSTBlank)
        Me.GBGST.Controls.Add(Me.txtGSTEntityNo)
        Me.GBGST.Controls.Add(Me.txtGSTPANNO)
        Me.GBGST.Controls.Add(Me.txtGstNo)
        Me.GBGST.Controls.Add(Me.txtGstState)
        Me.GBGST.Controls.Add(Me.lblGSTNo)
        Me.GBGST.HeaderText = "GST"
        Me.GBGST.Location = New System.Drawing.Point(3, 203)
        Me.GBGST.Name = "GBGST"
        Me.GBGST.Size = New System.Drawing.Size(696, 29)
        Me.GBGST.TabIndex = 110
        Me.GBGST.Text = "GST"
        '
        'ChkRegistered
        '
        Me.ChkRegistered.Location = New System.Drawing.Point(607, 8)
        Me.ChkRegistered.MyLinkLable1 = Nothing
        Me.ChkRegistered.MyLinkLable2 = Nothing
        Me.ChkRegistered.Name = "ChkRegistered"
        Me.ChkRegistered.Size = New System.Drawing.Size(73, 18)
        Me.ChkRegistered.TabIndex = 110
        Me.ChkRegistered.Tag1 = Nothing
        Me.ChkRegistered.Text = "Registered"
        '
        'txtGSTDegit
        '
        Me.txtGSTDegit.CalculationExpression = Nothing
        Me.txtGSTDegit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGSTDegit.FieldCode = Nothing
        Me.txtGSTDegit.FieldDesc = Nothing
        Me.txtGSTDegit.FieldMaxLength = 0
        Me.txtGSTDegit.FieldName = Nothing
        Me.txtGSTDegit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTDegit.isCalculatedField = False
        Me.txtGSTDegit.IsSourceFromTable = False
        Me.txtGSTDegit.IsSourceFromValueList = False
        Me.txtGSTDegit.IsUnique = False
        Me.txtGSTDegit.Location = New System.Drawing.Point(330, 8)
        Me.txtGSTDegit.MaxLength = 1
        Me.txtGSTDegit.MendatroryField = False
        Me.txtGSTDegit.MyLinkLable1 = Me.lblGSTNo
        Me.txtGSTDegit.MyLinkLable2 = Nothing
        Me.txtGSTDegit.Name = "txtGSTDegit"
        Me.txtGSTDegit.ReferenceFieldDesc = Nothing
        Me.txtGSTDegit.ReferenceFieldName = Nothing
        Me.txtGSTDegit.ReferenceTableName = Nothing
        Me.txtGSTDegit.Size = New System.Drawing.Size(35, 18)
        Me.txtGSTDegit.TabIndex = 71
        '
        'lblGSTNo
        '
        Me.lblGSTNo.FieldName = Nothing
        Me.lblGSTNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTNo.Location = New System.Drawing.Point(7, 10)
        Me.lblGSTNo.Name = "lblGSTNo"
        Me.lblGSTNo.Size = New System.Drawing.Size(58, 16)
        Me.lblGSTNo.TabIndex = 62
        Me.lblGSTNo.Text = "GSTIN No"
        '
        'txtGSTBlank
        '
        Me.txtGSTBlank.CalculationExpression = Nothing
        Me.txtGSTBlank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGSTBlank.FieldCode = Nothing
        Me.txtGSTBlank.FieldDesc = Nothing
        Me.txtGSTBlank.FieldMaxLength = 0
        Me.txtGSTBlank.FieldName = Nothing
        Me.txtGSTBlank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTBlank.isCalculatedField = False
        Me.txtGSTBlank.IsSourceFromTable = False
        Me.txtGSTBlank.IsSourceFromValueList = False
        Me.txtGSTBlank.IsUnique = False
        Me.txtGSTBlank.Location = New System.Drawing.Point(292, 8)
        Me.txtGSTBlank.MaxLength = 1
        Me.txtGSTBlank.MendatroryField = False
        Me.txtGSTBlank.MyLinkLable1 = Me.lblGSTNo
        Me.txtGSTBlank.MyLinkLable2 = Nothing
        Me.txtGSTBlank.Name = "txtGSTBlank"
        Me.txtGSTBlank.ReferenceFieldDesc = Nothing
        Me.txtGSTBlank.ReferenceFieldName = Nothing
        Me.txtGSTBlank.ReferenceTableName = Nothing
        Me.txtGSTBlank.Size = New System.Drawing.Size(35, 18)
        Me.txtGSTBlank.TabIndex = 70
        '
        'txtGSTEntityNo
        '
        Me.txtGSTEntityNo.CalculationExpression = Nothing
        Me.txtGSTEntityNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGSTEntityNo.FieldCode = Nothing
        Me.txtGSTEntityNo.FieldDesc = Nothing
        Me.txtGSTEntityNo.FieldMaxLength = 0
        Me.txtGSTEntityNo.FieldName = Nothing
        Me.txtGSTEntityNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTEntityNo.isCalculatedField = False
        Me.txtGSTEntityNo.IsSourceFromTable = False
        Me.txtGSTEntityNo.IsSourceFromValueList = False
        Me.txtGSTEntityNo.IsUnique = False
        Me.txtGSTEntityNo.Location = New System.Drawing.Point(255, 8)
        Me.txtGSTEntityNo.MaxLength = 1
        Me.txtGSTEntityNo.MendatroryField = False
        Me.txtGSTEntityNo.MyLinkLable1 = Me.lblGSTNo
        Me.txtGSTEntityNo.MyLinkLable2 = Nothing
        Me.txtGSTEntityNo.Name = "txtGSTEntityNo"
        Me.txtGSTEntityNo.ReferenceFieldDesc = Nothing
        Me.txtGSTEntityNo.ReferenceFieldName = Nothing
        Me.txtGSTEntityNo.ReferenceTableName = Nothing
        Me.txtGSTEntityNo.Size = New System.Drawing.Size(35, 18)
        Me.txtGSTEntityNo.TabIndex = 69
        '
        'txtGSTPANNO
        '
        Me.txtGSTPANNO.CalculationExpression = Nothing
        Me.txtGSTPANNO.FieldCode = Nothing
        Me.txtGSTPANNO.FieldDesc = Nothing
        Me.txtGSTPANNO.FieldMaxLength = 0
        Me.txtGSTPANNO.FieldName = Nothing
        Me.txtGSTPANNO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTPANNO.isCalculatedField = False
        Me.txtGSTPANNO.IsSourceFromTable = False
        Me.txtGSTPANNO.IsSourceFromValueList = False
        Me.txtGSTPANNO.IsUnique = False
        Me.txtGSTPANNO.Location = New System.Drawing.Point(157, 8)
        Me.txtGSTPANNO.MaxLength = 50
        Me.txtGSTPANNO.MendatroryField = False
        Me.txtGSTPANNO.MyLinkLable1 = Me.lblGSTNo
        Me.txtGSTPANNO.MyLinkLable2 = Nothing
        Me.txtGSTPANNO.Name = "txtGSTPANNO"
        Me.txtGSTPANNO.ReadOnly = True
        Me.txtGSTPANNO.ReferenceFieldDesc = Nothing
        Me.txtGSTPANNO.ReferenceFieldName = Nothing
        Me.txtGSTPANNO.ReferenceTableName = Nothing
        Me.txtGSTPANNO.Size = New System.Drawing.Size(97, 18)
        Me.txtGSTPANNO.TabIndex = 68
        '
        'txtGstNo
        '
        Me.txtGstNo.CalculationExpression = Nothing
        Me.txtGstNo.FieldCode = Nothing
        Me.txtGstNo.FieldDesc = Nothing
        Me.txtGstNo.FieldMaxLength = 0
        Me.txtGstNo.FieldName = Nothing
        Me.txtGstNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGstNo.isCalculatedField = False
        Me.txtGstNo.IsSourceFromTable = False
        Me.txtGstNo.IsSourceFromValueList = False
        Me.txtGstNo.IsUnique = False
        Me.txtGstNo.Location = New System.Drawing.Point(374, 8)
        Me.txtGstNo.MaxLength = 50
        Me.txtGstNo.MendatroryField = False
        Me.txtGstNo.MyLinkLable1 = Nothing
        Me.txtGstNo.MyLinkLable2 = Nothing
        Me.txtGstNo.Name = "txtGstNo"
        Me.txtGstNo.ReadOnly = True
        Me.txtGstNo.ReferenceFieldDesc = Nothing
        Me.txtGstNo.ReferenceFieldName = Nothing
        Me.txtGstNo.ReferenceTableName = Nothing
        Me.txtGstNo.Size = New System.Drawing.Size(227, 18)
        Me.txtGstNo.TabIndex = 67
        '
        'txtGstState
        '
        Me.txtGstState.CalculationExpression = Nothing
        Me.txtGstState.FieldCode = Nothing
        Me.txtGstState.FieldDesc = Nothing
        Me.txtGstState.FieldMaxLength = 0
        Me.txtGstState.FieldName = Nothing
        Me.txtGstState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGstState.isCalculatedField = False
        Me.txtGstState.IsSourceFromTable = False
        Me.txtGstState.IsSourceFromValueList = False
        Me.txtGstState.IsUnique = False
        Me.txtGstState.Location = New System.Drawing.Point(116, 8)
        Me.txtGstState.MaxLength = 50
        Me.txtGstState.MendatroryField = False
        Me.txtGstState.MyLinkLable1 = Me.lblGSTNo
        Me.txtGstState.MyLinkLable2 = Nothing
        Me.txtGstState.Name = "txtGstState"
        Me.txtGstState.ReadOnly = True
        Me.txtGstState.ReferenceFieldDesc = Nothing
        Me.txtGstState.ReferenceFieldName = Nothing
        Me.txtGstState.ReferenceTableName = Nothing
        Me.txtGstState.Size = New System.Drawing.Size(35, 18)
        Me.txtGstState.TabIndex = 63
        '
        'ChkOther
        '
        Me.ChkOther.Location = New System.Drawing.Point(251, 182)
        Me.ChkOther.MyLinkLable1 = Nothing
        Me.ChkOther.MyLinkLable2 = Nothing
        Me.ChkOther.Name = "ChkOther"
        Me.ChkOther.Size = New System.Drawing.Size(49, 18)
        Me.ChkOther.TabIndex = 109
        Me.ChkOther.Tag1 = Nothing
        Me.ChkOther.Text = "Other"
        '
        'ChkCheckCreditLimit
        '
        Me.ChkCheckCreditLimit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCheckCreditLimit.Location = New System.Drawing.Point(115, 181)
        Me.ChkCheckCreditLimit.Name = "ChkCheckCreditLimit"
        Me.ChkCheckCreditLimit.Size = New System.Drawing.Size(113, 16)
        Me.ChkCheckCreditLimit.TabIndex = 108
        Me.ChkCheckCreditLimit.Text = "Check Credit Limit"
        '
        'txttempCreditLimitTo
        '
        Me.txttempCreditLimitTo.CalculationExpression = Nothing
        Me.txttempCreditLimitTo.CustomFormat = "dd/MM/yyyy"
        Me.txttempCreditLimitTo.FieldCode = Nothing
        Me.txttempCreditLimitTo.FieldDesc = Nothing
        Me.txttempCreditLimitTo.FieldMaxLength = 0
        Me.txttempCreditLimitTo.FieldName = Nothing
        Me.txttempCreditLimitTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttempCreditLimitTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txttempCreditLimitTo.isCalculatedField = False
        Me.txttempCreditLimitTo.IsSourceFromTable = False
        Me.txttempCreditLimitTo.IsSourceFromValueList = False
        Me.txttempCreditLimitTo.IsUnique = False
        Me.txttempCreditLimitTo.Location = New System.Drawing.Point(627, 157)
        Me.txttempCreditLimitTo.MendatroryField = False
        Me.txttempCreditLimitTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txttempCreditLimitTo.MyLinkLable1 = Me.MyLabel5
        Me.txttempCreditLimitTo.MyLinkLable2 = Nothing
        Me.txttempCreditLimitTo.Name = "txttempCreditLimitTo"
        Me.txttempCreditLimitTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txttempCreditLimitTo.ReferenceFieldDesc = Nothing
        Me.txttempCreditLimitTo.ReferenceFieldName = Nothing
        Me.txttempCreditLimitTo.ReferenceTableName = Nothing
        Me.txttempCreditLimitTo.Size = New System.Drawing.Size(81, 18)
        Me.txttempCreditLimitTo.TabIndex = 16
        Me.txttempCreditLimitTo.TabStop = False
        Me.txttempCreditLimitTo.Text = "07/10/2016"
        Me.txttempCreditLimitTo.Value = New Date(2016, 10, 7, 0, 0, 0, 0)
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(511, 158)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(112, 16)
        Me.MyLabel5.TabIndex = 107
        Me.MyLabel5.Text = "Temp Credit Limit To"
        '
        'txttempCreditLimitFrom
        '
        Me.txttempCreditLimitFrom.CalculationExpression = Nothing
        Me.txttempCreditLimitFrom.CustomFormat = "dd/MM/yyyy"
        Me.txttempCreditLimitFrom.FieldCode = Nothing
        Me.txttempCreditLimitFrom.FieldDesc = Nothing
        Me.txttempCreditLimitFrom.FieldMaxLength = 0
        Me.txttempCreditLimitFrom.FieldName = Nothing
        Me.txttempCreditLimitFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttempCreditLimitFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txttempCreditLimitFrom.isCalculatedField = False
        Me.txttempCreditLimitFrom.IsSourceFromTable = False
        Me.txttempCreditLimitFrom.IsSourceFromValueList = False
        Me.txttempCreditLimitFrom.IsUnique = False
        Me.txttempCreditLimitFrom.Location = New System.Drawing.Point(424, 157)
        Me.txttempCreditLimitFrom.MendatroryField = False
        Me.txttempCreditLimitFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txttempCreditLimitFrom.MyLinkLable1 = Me.MyLabel6
        Me.txttempCreditLimitFrom.MyLinkLable2 = Nothing
        Me.txttempCreditLimitFrom.Name = "txttempCreditLimitFrom"
        Me.txttempCreditLimitFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txttempCreditLimitFrom.ReferenceFieldDesc = Nothing
        Me.txttempCreditLimitFrom.ReferenceFieldName = Nothing
        Me.txttempCreditLimitFrom.ReferenceTableName = Nothing
        Me.txttempCreditLimitFrom.Size = New System.Drawing.Size(81, 18)
        Me.txttempCreditLimitFrom.TabIndex = 15
        Me.txttempCreditLimitFrom.TabStop = False
        Me.txttempCreditLimitFrom.Text = "07/10/2016"
        Me.txttempCreditLimitFrom.Value = New Date(2016, 10, 7, 0, 0, 0, 0)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(10, 158)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel4.TabIndex = 103
        Me.MyLabel4.Text = "Temp Credit Limit"
        '
        'txtTempCreditLimit
        '
        Me.txtTempCreditLimit.CalculationExpression = Nothing
        Me.txtTempCreditLimit.FieldCode = Nothing
        Me.txtTempCreditLimit.FieldDesc = Nothing
        Me.txtTempCreditLimit.FieldMaxLength = 0
        Me.txtTempCreditLimit.FieldName = Nothing
        Me.txtTempCreditLimit.isCalculatedField = False
        Me.txtTempCreditLimit.IsSourceFromTable = False
        Me.txtTempCreditLimit.IsSourceFromValueList = False
        Me.txtTempCreditLimit.IsUnique = False
        Me.txtTempCreditLimit.Location = New System.Drawing.Point(116, 156)
        Me.txtTempCreditLimit.MaxLength = 12
        Me.txtTempCreditLimit.MendatroryField = False
        Me.txtTempCreditLimit.MyLinkLable1 = Me.MyLabel4
        Me.txtTempCreditLimit.MyLinkLable2 = Nothing
        Me.txtTempCreditLimit.Name = "txtTempCreditLimit"
        Me.txtTempCreditLimit.ReferenceFieldDesc = Nothing
        Me.txtTempCreditLimit.ReferenceFieldName = Nothing
        Me.txtTempCreditLimit.ReferenceTableName = Nothing
        Me.txtTempCreditLimit.Size = New System.Drawing.Size(165, 20)
        Me.txtTempCreditLimit.TabIndex = 14
        Me.txtTempCreditLimit.Text = "0.00"
        Me.txtTempCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'fndPayCode
        '
        Me.fndPayCode.CalculationExpression = Nothing
        Me.fndPayCode.FieldCode = Nothing
        Me.fndPayCode.FieldDesc = Nothing
        Me.fndPayCode.FieldMaxLength = 0
        Me.fndPayCode.FieldName = Nothing
        Me.fndPayCode.isCalculatedField = False
        Me.fndPayCode.IsSourceFromTable = False
        Me.fndPayCode.IsSourceFromValueList = False
        Me.fndPayCode.IsUnique = False
        Me.fndPayCode.Location = New System.Drawing.Point(116, 46)
        Me.fndPayCode.MendatroryField = False
        Me.fndPayCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayCode.MyLinkLable1 = Me.RadLabel26
        Me.fndPayCode.MyLinkLable2 = Nothing
        Me.fndPayCode.MyReadOnly = False
        Me.fndPayCode.MyShowMasterFormButton = False
        Me.fndPayCode.Name = "fndPayCode"
        Me.fndPayCode.ReferenceFieldDesc = Nothing
        Me.fndPayCode.ReferenceFieldName = Nothing
        Me.fndPayCode.ReferenceTableName = Nothing
        Me.fndPayCode.Size = New System.Drawing.Size(143, 19)
        Me.fndPayCode.TabIndex = 4
        Me.fndPayCode.Value = ""
        '
        'RadLabel26
        '
        Me.RadLabel26.FieldName = Nothing
        Me.RadLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel26.Location = New System.Drawing.Point(10, 47)
        Me.RadLabel26.Name = "RadLabel26"
        Me.RadLabel26.Size = New System.Drawing.Size(81, 16)
        Me.RadLabel26.TabIndex = 69
        Me.RadLabel26.Text = "Payment Code"
        '
        'fndAccntSet
        '
        Me.fndAccntSet.CalculationExpression = Nothing
        Me.fndAccntSet.FieldCode = Nothing
        Me.fndAccntSet.FieldDesc = Nothing
        Me.fndAccntSet.FieldMaxLength = 0
        Me.fndAccntSet.FieldName = Nothing
        Me.fndAccntSet.isCalculatedField = False
        Me.fndAccntSet.IsSourceFromTable = False
        Me.fndAccntSet.IsSourceFromValueList = False
        Me.fndAccntSet.IsUnique = False
        Me.fndAccntSet.Location = New System.Drawing.Point(116, 25)
        Me.fndAccntSet.MendatroryField = False
        Me.fndAccntSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAccntSet.MyLinkLable1 = Me.RadLabel25
        Me.fndAccntSet.MyLinkLable2 = Nothing
        Me.fndAccntSet.MyReadOnly = False
        Me.fndAccntSet.MyShowMasterFormButton = False
        Me.fndAccntSet.Name = "fndAccntSet"
        Me.fndAccntSet.ReferenceFieldDesc = Nothing
        Me.fndAccntSet.ReferenceFieldName = Nothing
        Me.fndAccntSet.ReferenceTableName = Nothing
        Me.fndAccntSet.Size = New System.Drawing.Size(143, 19)
        Me.fndAccntSet.TabIndex = 2
        Me.fndAccntSet.Value = ""
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(10, 26)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel25.TabIndex = 67
        Me.RadLabel25.Text = "Account Set"
        '
        'fndTrmsCode
        '
        Me.fndTrmsCode.CalculationExpression = Nothing
        Me.fndTrmsCode.FieldCode = Nothing
        Me.fndTrmsCode.FieldDesc = Nothing
        Me.fndTrmsCode.FieldMaxLength = 0
        Me.fndTrmsCode.FieldName = Nothing
        Me.fndTrmsCode.isCalculatedField = False
        Me.fndTrmsCode.IsSourceFromTable = False
        Me.fndTrmsCode.IsSourceFromValueList = False
        Me.fndTrmsCode.IsUnique = False
        Me.fndTrmsCode.Location = New System.Drawing.Point(116, 4)
        Me.fndTrmsCode.MendatroryField = False
        Me.fndTrmsCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTrmsCode.MyLinkLable1 = Me.RadLabel23
        Me.fndTrmsCode.MyLinkLable2 = Nothing
        Me.fndTrmsCode.MyReadOnly = False
        Me.fndTrmsCode.MyShowMasterFormButton = False
        Me.fndTrmsCode.Name = "fndTrmsCode"
        Me.fndTrmsCode.ReferenceFieldDesc = Nothing
        Me.fndTrmsCode.ReferenceFieldName = Nothing
        Me.fndTrmsCode.ReferenceTableName = Nothing
        Me.fndTrmsCode.Size = New System.Drawing.Size(143, 19)
        Me.fndTrmsCode.TabIndex = 0
        Me.fndTrmsCode.Value = ""
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(10, 7)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(68, 16)
        Me.RadLabel23.TabIndex = 65
        Me.RadLabel23.Text = "Terms Code"
        '
        'txtpan
        '
        Me.txtpan.CalculationExpression = Nothing
        Me.txtpan.FieldCode = Nothing
        Me.txtpan.FieldDesc = Nothing
        Me.txtpan.FieldMaxLength = 0
        Me.txtpan.FieldName = Nothing
        Me.txtpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpan.isCalculatedField = False
        Me.txtpan.IsSourceFromTable = False
        Me.txtpan.IsSourceFromValueList = False
        Me.txtpan.IsUnique = False
        Me.txtpan.Location = New System.Drawing.Point(423, 90)
        Me.txtpan.MaxLength = 30
        Me.txtpan.MendatroryField = False
        Me.txtpan.MyLinkLable1 = Me.lblpan
        Me.txtpan.MyLinkLable2 = Nothing
        Me.txtpan.Name = "txtpan"
        Me.txtpan.ReferenceFieldDesc = Nothing
        Me.txtpan.ReferenceFieldName = Nothing
        Me.txtpan.ReferenceTableName = Nothing
        Me.txtpan.Size = New System.Drawing.Size(242, 18)
        Me.txtpan.TabIndex = 9
        '
        'lblpan
        '
        Me.lblpan.FieldName = Nothing
        Me.lblpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpan.Location = New System.Drawing.Point(350, 91)
        Me.lblpan.Name = "lblpan"
        Me.lblpan.Size = New System.Drawing.Size(50, 16)
        Me.lblpan.TabIndex = 99
        Me.lblpan.Text = "PAN No."
        '
        'drpformtype
        '
        Me.drpformtype.AutoCompleteDisplayMember = Nothing
        Me.drpformtype.AutoCompleteValueMember = Nothing
        Me.drpformtype.DropDownAnimationEnabled = True
        RadListDataItem7.Text = "Form C"
        RadListDataItem8.Text = "Form F"
        Me.drpformtype.Items.Add(RadListDataItem7)
        Me.drpformtype.Items.Add(RadListDataItem8)
        Me.drpformtype.Location = New System.Drawing.Point(116, 133)
        Me.drpformtype.Name = "drpformtype"
        Me.drpformtype.Size = New System.Drawing.Size(109, 20)
        Me.drpformtype.TabIndex = 12
        '
        'lbldivision
        '
        Me.lbldivision.FieldName = Nothing
        Me.lbldivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldivision.Location = New System.Drawing.Point(350, 113)
        Me.lbldivision.Name = "lbldivision"
        Me.lbldivision.Size = New System.Drawing.Size(46, 16)
        Me.lbldivision.TabIndex = 101
        Me.lbldivision.Text = "Division"
        '
        'txtdivision
        '
        Me.txtdivision.CalculationExpression = Nothing
        Me.txtdivision.FieldCode = Nothing
        Me.txtdivision.FieldDesc = Nothing
        Me.txtdivision.FieldMaxLength = 0
        Me.txtdivision.FieldName = Nothing
        Me.txtdivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdivision.isCalculatedField = False
        Me.txtdivision.IsSourceFromTable = False
        Me.txtdivision.IsSourceFromValueList = False
        Me.txtdivision.IsUnique = False
        Me.txtdivision.Location = New System.Drawing.Point(423, 112)
        Me.txtdivision.MaxLength = 30
        Me.txtdivision.MendatroryField = False
        Me.txtdivision.MyLinkLable1 = Me.lbldivision
        Me.txtdivision.MyLinkLable2 = Nothing
        Me.txtdivision.Name = "txtdivision"
        Me.txtdivision.ReferenceFieldDesc = Nothing
        Me.txtdivision.ReferenceFieldName = Nothing
        Me.txtdivision.ReferenceTableName = Nothing
        Me.txtdivision.Size = New System.Drawing.Size(242, 18)
        Me.txtdivision.TabIndex = 11
        '
        'lblcollectorate
        '
        Me.lblcollectorate.FieldName = Nothing
        Me.lblcollectorate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcollectorate.Location = New System.Drawing.Point(350, 69)
        Me.lblcollectorate.Name = "lblcollectorate"
        Me.lblcollectorate.Size = New System.Drawing.Size(67, 16)
        Me.lblcollectorate.TabIndex = 95
        Me.lblcollectorate.Text = "Collectorate"
        '
        'txtcollect
        '
        Me.txtcollect.CalculationExpression = Nothing
        Me.txtcollect.FieldCode = Nothing
        Me.txtcollect.FieldDesc = Nothing
        Me.txtcollect.FieldMaxLength = 0
        Me.txtcollect.FieldName = Nothing
        Me.txtcollect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcollect.isCalculatedField = False
        Me.txtcollect.IsSourceFromTable = False
        Me.txtcollect.IsSourceFromValueList = False
        Me.txtcollect.IsUnique = False
        Me.txtcollect.Location = New System.Drawing.Point(423, 68)
        Me.txtcollect.MaxLength = 30
        Me.txtcollect.MendatroryField = False
        Me.txtcollect.MyLinkLable1 = Me.lblcollectorate
        Me.txtcollect.MyLinkLable2 = Nothing
        Me.txtcollect.Name = "txtcollect"
        Me.txtcollect.ReferenceFieldDesc = Nothing
        Me.txtcollect.ReferenceFieldName = Nothing
        Me.txtcollect.ReferenceTableName = Nothing
        Me.txtcollect.Size = New System.Drawing.Size(242, 18)
        Me.txtcollect.TabIndex = 7
        '
        'lblrange
        '
        Me.lblrange.FieldName = Nothing
        Me.lblrange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrange.Location = New System.Drawing.Point(350, 47)
        Me.lblrange.Name = "lblrange"
        Me.lblrange.Size = New System.Drawing.Size(40, 16)
        Me.lblrange.TabIndex = 94
        Me.lblrange.Text = "Range"
        '
        'lblecc
        '
        Me.lblecc.FieldName = Nothing
        Me.lblecc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblecc.Location = New System.Drawing.Point(350, 26)
        Me.lblecc.Name = "lblecc"
        Me.lblecc.Size = New System.Drawing.Size(48, 16)
        Me.lblecc.TabIndex = 97
        Me.lblecc.Text = "ECC No"
        '
        'txtrange
        '
        Me.txtrange.CalculationExpression = Nothing
        Me.txtrange.FieldCode = Nothing
        Me.txtrange.FieldDesc = Nothing
        Me.txtrange.FieldMaxLength = 0
        Me.txtrange.FieldName = Nothing
        Me.txtrange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrange.isCalculatedField = False
        Me.txtrange.IsSourceFromTable = False
        Me.txtrange.IsSourceFromValueList = False
        Me.txtrange.IsUnique = False
        Me.txtrange.Location = New System.Drawing.Point(423, 46)
        Me.txtrange.MaxLength = 30
        Me.txtrange.MendatroryField = False
        Me.txtrange.MyLinkLable1 = Me.lblrange
        Me.txtrange.MyLinkLable2 = Nothing
        Me.txtrange.Name = "txtrange"
        Me.txtrange.ReferenceFieldDesc = Nothing
        Me.txtrange.ReferenceFieldName = Nothing
        Me.txtrange.ReferenceTableName = Nothing
        Me.txtrange.Size = New System.Drawing.Size(242, 18)
        Me.txtrange.TabIndex = 5
        '
        'txtecc
        '
        Me.txtecc.CalculationExpression = Nothing
        Me.txtecc.FieldCode = Nothing
        Me.txtecc.FieldDesc = Nothing
        Me.txtecc.FieldMaxLength = 0
        Me.txtecc.FieldName = Nothing
        Me.txtecc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtecc.isCalculatedField = False
        Me.txtecc.IsSourceFromTable = False
        Me.txtecc.IsSourceFromValueList = False
        Me.txtecc.IsUnique = False
        Me.txtecc.Location = New System.Drawing.Point(423, 25)
        Me.txtecc.MaxLength = 30
        Me.txtecc.MendatroryField = False
        Me.txtecc.MyLinkLable1 = Me.lblecc
        Me.txtecc.MyLinkLable2 = Nothing
        Me.txtecc.Name = "txtecc"
        Me.txtecc.ReferenceFieldDesc = Nothing
        Me.txtecc.ReferenceFieldName = Nothing
        Me.txtecc.ReferenceTableName = Nothing
        Me.txtecc.Size = New System.Drawing.Size(242, 18)
        Me.txtecc.TabIndex = 3
        '
        'lblcst
        '
        Me.lblcst.FieldName = Nothing
        Me.lblcst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcst.Location = New System.Drawing.Point(350, 4)
        Me.lblcst.Name = "lblcst"
        Me.lblcst.Size = New System.Drawing.Size(46, 16)
        Me.lblcst.TabIndex = 96
        Me.lblcst.Text = "CST No"
        '
        'txtcst
        '
        Me.txtcst.CalculationExpression = Nothing
        Me.txtcst.FieldCode = Nothing
        Me.txtcst.FieldDesc = Nothing
        Me.txtcst.FieldMaxLength = 0
        Me.txtcst.FieldName = Nothing
        Me.txtcst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcst.isCalculatedField = False
        Me.txtcst.IsSourceFromTable = False
        Me.txtcst.IsSourceFromValueList = False
        Me.txtcst.IsUnique = False
        Me.txtcst.Location = New System.Drawing.Point(423, 4)
        Me.txtcst.MaxLength = 30
        Me.txtcst.MendatroryField = False
        Me.txtcst.MyLinkLable1 = Me.lblcst
        Me.txtcst.MyLinkLable2 = Nothing
        Me.txtcst.Name = "txtcst"
        Me.txtcst.ReferenceFieldDesc = Nothing
        Me.txtcst.ReferenceFieldName = Nothing
        Me.txtcst.ReferenceTableName = Nothing
        Me.txtcst.Size = New System.Drawing.Size(242, 18)
        Me.txtcst.TabIndex = 1
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.fndTxGrp)
        Me.RadGroupBox3.Controls.Add(Me.grdTax)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel35)
        Me.RadGroupBox3.Controls.Add(Me.txtTxGrp)
        Me.RadGroupBox3.HeaderText = "Tax Group"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 238)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(696, 161)
        Me.RadGroupBox3.TabIndex = 17
        Me.RadGroupBox3.Text = "Tax Group"
        '
        'fndTxGrp
        '
        Me.fndTxGrp.CalculationExpression = Nothing
        Me.fndTxGrp.FieldCode = Nothing
        Me.fndTxGrp.FieldDesc = Nothing
        Me.fndTxGrp.FieldMaxLength = 0
        Me.fndTxGrp.FieldName = Nothing
        Me.fndTxGrp.isCalculatedField = False
        Me.fndTxGrp.IsSourceFromTable = False
        Me.fndTxGrp.IsSourceFromValueList = False
        Me.fndTxGrp.IsUnique = False
        Me.fndTxGrp.Location = New System.Drawing.Point(112, 17)
        Me.fndTxGrp.MendatroryField = False
        Me.fndTxGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTxGrp.MyLinkLable1 = Me.RadLabel35
        Me.fndTxGrp.MyLinkLable2 = Nothing
        Me.fndTxGrp.MyReadOnly = False
        Me.fndTxGrp.MyShowMasterFormButton = False
        Me.fndTxGrp.Name = "fndTxGrp"
        Me.fndTxGrp.ReferenceFieldDesc = Nothing
        Me.fndTxGrp.ReferenceFieldName = Nothing
        Me.fndTxGrp.ReferenceTableName = Nothing
        Me.fndTxGrp.Size = New System.Drawing.Size(143, 19)
        Me.fndTxGrp.TabIndex = 0
        Me.fndTxGrp.Value = ""
        '
        'RadLabel35
        '
        Me.RadLabel35.FieldName = Nothing
        Me.RadLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel35.Location = New System.Drawing.Point(13, 20)
        Me.RadLabel35.Name = "RadLabel35"
        Me.RadLabel35.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel35.TabIndex = 70
        Me.RadLabel35.Text = "Tax Group"
        '
        'grdTax
        '
        Me.grdTax.BackColor = System.Drawing.Color.White
        Me.grdTax.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdTax.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdTax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdTax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdTax.Location = New System.Drawing.Point(13, 42)
        '
        '
        '
        Me.grdTax.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Tax Authority"
        GridViewTextBoxColumn1.Name = "gdTxAuth"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 350
        GridViewComboBoxColumn1.HeaderText = "Tax Rate"
        GridViewComboBoxColumn1.Name = "gdTxRate"
        GridViewComboBoxColumn1.Width = 300
        Me.grdTax.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewComboBoxColumn1})
        Me.grdTax.MasterTemplate.EnableGrouping = False
        Me.grdTax.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.grdTax.MasterTemplate.ShowHeaderCellButtons = True
        Me.grdTax.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.grdTax.MyStopExport = False
        Me.grdTax.Name = "grdTax"
        Me.grdTax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdTax.ShowHeaderCellButtons = True
        Me.grdTax.Size = New System.Drawing.Size(670, 115)
        Me.grdTax.TabIndex = 2
        Me.grdTax.TabStop = False
        '
        'txtTxGrp
        '
        Me.txtTxGrp.CalculationExpression = Nothing
        Me.txtTxGrp.FieldCode = Nothing
        Me.txtTxGrp.FieldDesc = Nothing
        Me.txtTxGrp.FieldMaxLength = 0
        Me.txtTxGrp.FieldName = Nothing
        Me.txtTxGrp.isCalculatedField = False
        Me.txtTxGrp.IsSourceFromTable = False
        Me.txtTxGrp.IsSourceFromValueList = False
        Me.txtTxGrp.IsUnique = False
        Me.txtTxGrp.Location = New System.Drawing.Point(266, 16)
        Me.txtTxGrp.MendatroryField = False
        Me.txtTxGrp.MyLinkLable1 = Nothing
        Me.txtTxGrp.MyLinkLable2 = Nothing
        Me.txtTxGrp.Name = "txtTxGrp"
        Me.txtTxGrp.ReadOnly = True
        Me.txtTxGrp.ReferenceFieldDesc = Nothing
        Me.txtTxGrp.ReferenceFieldName = Nothing
        Me.txtTxGrp.ReferenceTableName = Nothing
        Me.txtTxGrp.Size = New System.Drawing.Size(417, 20)
        Me.txtTxGrp.TabIndex = 1
        '
        'RadLabel30
        '
        Me.RadLabel30.FieldName = Nothing
        Me.RadLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel30.Location = New System.Drawing.Point(10, 113)
        Me.RadLabel30.Name = "RadLabel30"
        Me.RadLabel30.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel30.TabIndex = 75
        Me.RadLabel30.Text = "Credit Limit"
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(10, 91)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel29.TabIndex = 71
        Me.RadLabel29.Text = "LST No."
        '
        'txtCredit
        '
        Me.txtCredit.CalculationExpression = Nothing
        Me.txtCredit.FieldCode = Nothing
        Me.txtCredit.FieldDesc = Nothing
        Me.txtCredit.FieldMaxLength = 0
        Me.txtCredit.FieldName = Nothing
        Me.txtCredit.isCalculatedField = False
        Me.txtCredit.IsSourceFromTable = False
        Me.txtCredit.IsSourceFromValueList = False
        Me.txtCredit.IsUnique = False
        Me.txtCredit.Location = New System.Drawing.Point(116, 111)
        Me.txtCredit.MaxLength = 12
        Me.txtCredit.MendatroryField = False
        Me.txtCredit.MyLinkLable1 = Me.RadLabel30
        Me.txtCredit.MyLinkLable2 = Nothing
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.ReferenceFieldDesc = Nothing
        Me.txtCredit.ReferenceFieldName = Nothing
        Me.txtCredit.ReferenceTableName = Nothing
        Me.txtCredit.Size = New System.Drawing.Size(165, 20)
        Me.txtCredit.TabIndex = 10
        Me.txtCredit.Text = "0.00"
        Me.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLstNo
        '
        Me.txtLstNo.CalculationExpression = Nothing
        Me.txtLstNo.FieldCode = Nothing
        Me.txtLstNo.FieldDesc = Nothing
        Me.txtLstNo.FieldMaxLength = 0
        Me.txtLstNo.FieldName = Nothing
        Me.txtLstNo.isCalculatedField = False
        Me.txtLstNo.IsSourceFromTable = False
        Me.txtLstNo.IsSourceFromValueList = False
        Me.txtLstNo.IsUnique = False
        Me.txtLstNo.Location = New System.Drawing.Point(116, 89)
        Me.txtLstNo.MaxLength = 15
        Me.txtLstNo.MendatroryField = False
        Me.txtLstNo.MyLinkLable1 = Me.RadLabel29
        Me.txtLstNo.MyLinkLable2 = Nothing
        Me.txtLstNo.Name = "txtLstNo"
        Me.txtLstNo.ReferenceFieldDesc = Nothing
        Me.txtLstNo.ReferenceFieldName = Nothing
        Me.txtLstNo.ReferenceTableName = Nothing
        Me.txtLstNo.Size = New System.Drawing.Size(165, 20)
        Me.txtLstNo.TabIndex = 8
        '
        'RadLabel31
        '
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(10, 135)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel31.TabIndex = 73
        Me.RadLabel31.Text = "Form Type"
        '
        'RadLabel28
        '
        Me.RadLabel28.FieldName = Nothing
        Me.RadLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel28.Location = New System.Drawing.Point(350, 135)
        Me.RadLabel28.Name = "RadLabel28"
        Me.RadLabel28.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel28.TabIndex = 68
        Me.RadLabel28.Text = "TIN No."
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(10, 69)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel27.TabIndex = 66
        Me.RadLabel27.Text = "Service Tax No."
        '
        'txtTinNo
        '
        Me.txtTinNo.CalculationExpression = Nothing
        Me.txtTinNo.FieldCode = Nothing
        Me.txtTinNo.FieldDesc = Nothing
        Me.txtTinNo.FieldMaxLength = 0
        Me.txtTinNo.FieldName = Nothing
        Me.txtTinNo.isCalculatedField = False
        Me.txtTinNo.IsSourceFromTable = False
        Me.txtTinNo.IsSourceFromValueList = False
        Me.txtTinNo.IsUnique = False
        Me.txtTinNo.Location = New System.Drawing.Point(423, 133)
        Me.txtTinNo.MaxLength = 15
        Me.txtTinNo.MendatroryField = False
        Me.txtTinNo.MyLinkLable1 = Me.RadLabel28
        Me.txtTinNo.MyLinkLable2 = Nothing
        Me.txtTinNo.Name = "txtTinNo"
        Me.txtTinNo.ReferenceFieldDesc = Nothing
        Me.txtTinNo.ReferenceFieldName = Nothing
        Me.txtTinNo.ReferenceTableName = Nothing
        Me.txtTinNo.Size = New System.Drawing.Size(242, 20)
        Me.txtTinNo.TabIndex = 13
        '
        'txtStaxNo
        '
        Me.txtStaxNo.CalculationExpression = Nothing
        Me.txtStaxNo.FieldCode = Nothing
        Me.txtStaxNo.FieldDesc = Nothing
        Me.txtStaxNo.FieldMaxLength = 0
        Me.txtStaxNo.FieldName = Nothing
        Me.txtStaxNo.isCalculatedField = False
        Me.txtStaxNo.IsSourceFromTable = False
        Me.txtStaxNo.IsSourceFromValueList = False
        Me.txtStaxNo.IsUnique = False
        Me.txtStaxNo.Location = New System.Drawing.Point(116, 67)
        Me.txtStaxNo.MaxLength = 15
        Me.txtStaxNo.MendatroryField = False
        Me.txtStaxNo.MyLinkLable1 = Me.RadLabel27
        Me.txtStaxNo.MyLinkLable2 = Nothing
        Me.txtStaxNo.Name = "txtStaxNo"
        Me.txtStaxNo.ReferenceFieldDesc = Nothing
        Me.txtStaxNo.ReferenceFieldName = Nothing
        Me.txtStaxNo.ReferenceTableName = Nothing
        Me.txtStaxNo.Size = New System.Drawing.Size(165, 20)
        Me.txtStaxNo.TabIndex = 6
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.TxtCrateOpeningQty)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage3.Controls.Add(Me.TxtCrateOpeningDate)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage3.Controls.Add(Me.fndZone)
        Me.RadPageViewPage3.Controls.Add(Me.lblZone)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage3.Controls.Add(Me.fndroutegroup)
        Me.RadPageViewPage3.Controls.Add(Me.fndChannel)
        Me.RadPageViewPage3.Controls.Add(Me.fndSalePerson)
        Me.RadPageViewPage3.Controls.Add(Me.fndCusType)
        Me.RadPageViewPage3.Controls.Add(Me.fndRoute)
        Me.RadPageViewPage3.Controls.Add(Me.fndCusCategory)
        Me.RadPageViewPage3.Controls.Add(Me.cboCustomerClass)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel33)
        Me.RadPageViewPage3.Controls.Add(Me.txtroutegroup)
        Me.RadPageViewPage3.Controls.Add(Me.lblroutegrp)
        Me.RadPageViewPage3.Controls.Add(Me.txtSalesPerson)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel34)
        Me.RadPageViewPage3.Controls.Add(Me.txtChannel)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel24)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage3.Controls.Add(Me.txtRoute)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel17)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(53.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(935, 388)
        Me.RadPageViewPage3.Text = "Activity"
        '
        'TxtCrateOpeningQty
        '
        Me.TxtCrateOpeningQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtCrateOpeningQty.CalculationExpression = Nothing
        Me.TxtCrateOpeningQty.DecimalPlaces = 0
        Me.TxtCrateOpeningQty.FieldCode = Nothing
        Me.TxtCrateOpeningQty.FieldDesc = Nothing
        Me.TxtCrateOpeningQty.FieldMaxLength = 0
        Me.TxtCrateOpeningQty.FieldName = Nothing
        Me.TxtCrateOpeningQty.isCalculatedField = False
        Me.TxtCrateOpeningQty.IsSourceFromTable = False
        Me.TxtCrateOpeningQty.IsSourceFromValueList = False
        Me.TxtCrateOpeningQty.IsUnique = False
        Me.TxtCrateOpeningQty.Location = New System.Drawing.Point(156, 277)
        Me.TxtCrateOpeningQty.MendatroryField = False
        Me.TxtCrateOpeningQty.MyLinkLable1 = Nothing
        Me.TxtCrateOpeningQty.MyLinkLable2 = Nothing
        Me.TxtCrateOpeningQty.Name = "TxtCrateOpeningQty"
        Me.TxtCrateOpeningQty.ReferenceFieldDesc = Nothing
        Me.TxtCrateOpeningQty.ReferenceFieldName = Nothing
        Me.TxtCrateOpeningQty.ReferenceTableName = Nothing
        Me.TxtCrateOpeningQty.Size = New System.Drawing.Size(143, 20)
        Me.TxtCrateOpeningQty.TabIndex = 109
        Me.TxtCrateOpeningQty.Text = "0"
        Me.TxtCrateOpeningQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCrateOpeningQty.Value = 0R
        Me.TxtCrateOpeningQty.Visible = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(10, 278)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(101, 16)
        Me.MyLabel9.TabIndex = 82
        Me.MyLabel9.Text = "Crate Opening Qty"
        Me.MyLabel9.Visible = False
        '
        'TxtCrateOpeningDate
        '
        Me.TxtCrateOpeningDate.CalculationExpression = Nothing
        Me.TxtCrateOpeningDate.CustomFormat = "dd/MM/yyyy"
        Me.TxtCrateOpeningDate.FieldCode = Nothing
        Me.TxtCrateOpeningDate.FieldDesc = Nothing
        Me.TxtCrateOpeningDate.FieldMaxLength = 0
        Me.TxtCrateOpeningDate.FieldName = Nothing
        Me.TxtCrateOpeningDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCrateOpeningDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtCrateOpeningDate.isCalculatedField = False
        Me.TxtCrateOpeningDate.IsSourceFromTable = False
        Me.TxtCrateOpeningDate.IsSourceFromValueList = False
        Me.TxtCrateOpeningDate.IsUnique = False
        Me.TxtCrateOpeningDate.Location = New System.Drawing.Point(418, 277)
        Me.TxtCrateOpeningDate.MendatroryField = False
        Me.TxtCrateOpeningDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TxtCrateOpeningDate.MyLinkLable1 = Me.MyLabel8
        Me.TxtCrateOpeningDate.MyLinkLable2 = Nothing
        Me.TxtCrateOpeningDate.Name = "TxtCrateOpeningDate"
        Me.TxtCrateOpeningDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TxtCrateOpeningDate.ReferenceFieldDesc = Nothing
        Me.TxtCrateOpeningDate.ReferenceFieldName = Nothing
        Me.TxtCrateOpeningDate.ReferenceTableName = Nothing
        Me.TxtCrateOpeningDate.ShowCheckBox = True
        Me.TxtCrateOpeningDate.Size = New System.Drawing.Size(105, 18)
        Me.TxtCrateOpeningDate.TabIndex = 107
        Me.TxtCrateOpeningDate.TabStop = False
        Me.TxtCrateOpeningDate.Text = "17/05/2011"
        Me.TxtCrateOpeningDate.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        Me.TxtCrateOpeningDate.Visible = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(305, 278)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel8.TabIndex = 108
        Me.MyLabel8.Text = "Crate Opening Date"
        Me.MyLabel8.Visible = False
        '
        'fndZone
        '
        Me.fndZone.CalculationExpression = Nothing
        Me.fndZone.FieldCode = Nothing
        Me.fndZone.FieldDesc = Nothing
        Me.fndZone.FieldMaxLength = 0
        Me.fndZone.FieldName = Nothing
        Me.fndZone.isCalculatedField = False
        Me.fndZone.IsSourceFromTable = False
        Me.fndZone.IsSourceFromValueList = False
        Me.fndZone.IsUnique = False
        Me.fndZone.Location = New System.Drawing.Point(360, 12)
        Me.fndZone.MendatroryField = False
        Me.fndZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndZone.MyLinkLable1 = Nothing
        Me.fndZone.MyLinkLable2 = Nothing
        Me.fndZone.MyReadOnly = False
        Me.fndZone.MyShowMasterFormButton = False
        Me.fndZone.Name = "fndZone"
        Me.fndZone.ReferenceFieldDesc = Nothing
        Me.fndZone.ReferenceFieldName = Nothing
        Me.fndZone.ReferenceTableName = Nothing
        Me.fndZone.Size = New System.Drawing.Size(143, 19)
        Me.fndZone.TabIndex = 82
        Me.fndZone.Value = ""
        '
        'lblZone
        '
        Me.lblZone.FieldName = Nothing
        Me.lblZone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZone.Location = New System.Drawing.Point(320, 15)
        Me.lblZone.Name = "lblZone"
        Me.lblZone.Size = New System.Drawing.Size(32, 16)
        Me.lblZone.TabIndex = 83
        Me.lblZone.Text = "Zone"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox5.Controls.Add(Me.txtpgfnd)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox5.Controls.Add(Me.chkpricegrpslctr)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel20)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel42)
        Me.RadGroupBox5.Controls.Add(Me.txtPriceCode)
        Me.RadGroupBox5.Controls.Add(Me.txtPriceCodeNon)
        Me.RadGroupBox5.HeaderText = "Price Code"
        Me.RadGroupBox5.Location = New System.Drawing.Point(10, 108)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(689, 85)
        Me.RadGroupBox5.TabIndex = 6
        Me.RadGroupBox5.Text = "Price Code"
        '
        'txtpgfnd
        '
        Me.txtpgfnd.CalculationExpression = Nothing
        Me.txtpgfnd.FieldCode = Nothing
        Me.txtpgfnd.FieldDesc = Nothing
        Me.txtpgfnd.FieldMaxLength = 0
        Me.txtpgfnd.FieldName = Nothing
        Me.txtpgfnd.isCalculatedField = False
        Me.txtpgfnd.IsSourceFromTable = False
        Me.txtpgfnd.IsSourceFromValueList = False
        Me.txtpgfnd.IsUnique = False
        Me.txtpgfnd.Location = New System.Drawing.Point(146, 50)
        Me.txtpgfnd.MendatroryField = False
        Me.txtpgfnd.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpgfnd.MyLinkLable1 = Nothing
        Me.txtpgfnd.MyLinkLable2 = Nothing
        Me.txtpgfnd.MyReadOnly = False
        Me.txtpgfnd.MyShowMasterFormButton = False
        Me.txtpgfnd.Name = "txtpgfnd"
        Me.txtpgfnd.ReferenceFieldDesc = Nothing
        Me.txtpgfnd.ReferenceFieldName = Nothing
        Me.txtpgfnd.ReferenceTableName = Nothing
        Me.txtpgfnd.Size = New System.Drawing.Size(143, 19)
        Me.txtpgfnd.TabIndex = 3
        Me.txtpgfnd.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(10, 50)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel3.TabIndex = 2
        Me.MyLabel3.Text = "Price Group Code"
        '
        'chkpricegrpslctr
        '
        Me.chkpricegrpslctr.Location = New System.Drawing.Point(10, 26)
        Me.chkpricegrpslctr.Name = "chkpricegrpslctr"
        Me.chkpricegrpslctr.Size = New System.Drawing.Size(108, 18)
        Me.chkpricegrpslctr.TabIndex = 0
        Me.chkpricegrpslctr.Text = "Price Group Code"
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(386, 29)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(121, 16)
        Me.RadLabel20.TabIndex = 66
        Me.RadLabel20.Text = "Price Code (Excisable)"
        '
        'RadLabel42
        '
        Me.RadLabel42.FieldName = Nothing
        Me.RadLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel42.Location = New System.Drawing.Point(386, 55)
        Me.RadLabel42.Name = "RadLabel42"
        Me.RadLabel42.Size = New System.Drawing.Size(146, 16)
        Me.RadLabel42.TabIndex = 68
        Me.RadLabel42.Text = "Price Code (Non-Excisable)"
        '
        'txtPriceCode
        '
        Me.txtPriceCode.CalculationExpression = Nothing
        Me.txtPriceCode.FieldCode = Nothing
        Me.txtPriceCode.FieldDesc = Nothing
        Me.txtPriceCode.FieldMaxLength = 0
        Me.txtPriceCode.FieldName = Nothing
        Me.txtPriceCode.isCalculatedField = False
        Me.txtPriceCode.IsSourceFromTable = False
        Me.txtPriceCode.IsSourceFromValueList = False
        Me.txtPriceCode.IsUnique = False
        Me.txtPriceCode.Location = New System.Drawing.Point(535, 29)
        Me.txtPriceCode.MendatroryField = False
        Me.txtPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceCode.MyLinkLable1 = Nothing
        Me.txtPriceCode.MyLinkLable2 = Nothing
        Me.txtPriceCode.MyReadOnly = False
        Me.txtPriceCode.MyShowMasterFormButton = False
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.ReferenceFieldDesc = Nothing
        Me.txtPriceCode.ReferenceFieldName = Nothing
        Me.txtPriceCode.ReferenceTableName = Nothing
        Me.txtPriceCode.Size = New System.Drawing.Size(143, 19)
        Me.txtPriceCode.TabIndex = 1
        Me.txtPriceCode.Value = ""
        '
        'txtPriceCodeNon
        '
        Me.txtPriceCodeNon.CalculationExpression = Nothing
        Me.txtPriceCodeNon.FieldCode = Nothing
        Me.txtPriceCodeNon.FieldDesc = Nothing
        Me.txtPriceCodeNon.FieldMaxLength = 0
        Me.txtPriceCodeNon.FieldName = Nothing
        Me.txtPriceCodeNon.isCalculatedField = False
        Me.txtPriceCodeNon.IsSourceFromTable = False
        Me.txtPriceCodeNon.IsSourceFromValueList = False
        Me.txtPriceCodeNon.IsUnique = False
        Me.txtPriceCodeNon.Location = New System.Drawing.Point(535, 54)
        Me.txtPriceCodeNon.MendatroryField = False
        Me.txtPriceCodeNon.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceCodeNon.MyLinkLable1 = Nothing
        Me.txtPriceCodeNon.MyLinkLable2 = Nothing
        Me.txtPriceCodeNon.MyReadOnly = False
        Me.txtPriceCodeNon.MyShowMasterFormButton = False
        Me.txtPriceCodeNon.Name = "txtPriceCodeNon"
        Me.txtPriceCodeNon.ReferenceFieldDesc = Nothing
        Me.txtPriceCodeNon.ReferenceFieldName = Nothing
        Me.txtPriceCodeNon.ReferenceTableName = Nothing
        Me.txtPriceCodeNon.Size = New System.Drawing.Size(143, 19)
        Me.txtPriceCodeNon.TabIndex = 4
        Me.txtPriceCodeNon.Value = ""
        '
        'fndroutegroup
        '
        Me.fndroutegroup.CalculationExpression = Nothing
        Me.fndroutegroup.FieldCode = Nothing
        Me.fndroutegroup.FieldDesc = Nothing
        Me.fndroutegroup.FieldMaxLength = 0
        Me.fndroutegroup.FieldName = Nothing
        Me.fndroutegroup.isCalculatedField = False
        Me.fndroutegroup.IsSourceFromTable = False
        Me.fndroutegroup.IsSourceFromValueList = False
        Me.fndroutegroup.IsUnique = False
        Me.fndroutegroup.Location = New System.Drawing.Point(156, 224)
        Me.fndroutegroup.MendatroryField = False
        Me.fndroutegroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndroutegroup.MyLinkLable1 = Nothing
        Me.fndroutegroup.MyLinkLable2 = Nothing
        Me.fndroutegroup.MyReadOnly = False
        Me.fndroutegroup.MyShowMasterFormButton = False
        Me.fndroutegroup.Name = "fndroutegroup"
        Me.fndroutegroup.ReferenceFieldDesc = Nothing
        Me.fndroutegroup.ReferenceFieldName = Nothing
        Me.fndroutegroup.ReferenceTableName = Nothing
        Me.fndroutegroup.Size = New System.Drawing.Size(143, 19)
        Me.fndroutegroup.TabIndex = 8
        Me.fndroutegroup.Value = ""
        '
        'fndChannel
        '
        Me.fndChannel.CalculationExpression = Nothing
        Me.fndChannel.FieldCode = Nothing
        Me.fndChannel.FieldDesc = Nothing
        Me.fndChannel.FieldMaxLength = 0
        Me.fndChannel.FieldName = Nothing
        Me.fndChannel.isCalculatedField = False
        Me.fndChannel.IsSourceFromTable = False
        Me.fndChannel.IsSourceFromValueList = False
        Me.fndChannel.IsUnique = False
        Me.fndChannel.Location = New System.Drawing.Point(156, 199)
        Me.fndChannel.MendatroryField = False
        Me.fndChannel.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndChannel.MyLinkLable1 = Nothing
        Me.fndChannel.MyLinkLable2 = Nothing
        Me.fndChannel.MyReadOnly = False
        Me.fndChannel.MyShowMasterFormButton = False
        Me.fndChannel.Name = "fndChannel"
        Me.fndChannel.ReferenceFieldDesc = Nothing
        Me.fndChannel.ReferenceFieldName = Nothing
        Me.fndChannel.ReferenceTableName = Nothing
        Me.fndChannel.Size = New System.Drawing.Size(143, 19)
        Me.fndChannel.TabIndex = 6
        Me.fndChannel.Value = ""
        '
        'fndSalePerson
        '
        Me.fndSalePerson.CalculationExpression = Nothing
        Me.fndSalePerson.Enabled = False
        Me.fndSalePerson.FieldCode = Nothing
        Me.fndSalePerson.FieldDesc = Nothing
        Me.fndSalePerson.FieldMaxLength = 0
        Me.fndSalePerson.FieldName = Nothing
        Me.fndSalePerson.isCalculatedField = False
        Me.fndSalePerson.IsSourceFromTable = False
        Me.fndSalePerson.IsSourceFromValueList = False
        Me.fndSalePerson.IsUnique = False
        Me.fndSalePerson.Location = New System.Drawing.Point(156, 89)
        Me.fndSalePerson.MendatroryField = False
        Me.fndSalePerson.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalePerson.MyLinkLable1 = Nothing
        Me.fndSalePerson.MyLinkLable2 = Nothing
        Me.fndSalePerson.MyReadOnly = False
        Me.fndSalePerson.MyShowMasterFormButton = False
        Me.fndSalePerson.Name = "fndSalePerson"
        Me.fndSalePerson.ReferenceFieldDesc = Nothing
        Me.fndSalePerson.ReferenceFieldName = Nothing
        Me.fndSalePerson.ReferenceTableName = Nothing
        Me.fndSalePerson.Size = New System.Drawing.Size(143, 19)
        Me.fndSalePerson.TabIndex = 4
        Me.fndSalePerson.Value = ""
        '
        'fndCusType
        '
        Me.fndCusType.CalculationExpression = Nothing
        Me.fndCusType.FieldCode = Nothing
        Me.fndCusType.FieldDesc = Nothing
        Me.fndCusType.FieldMaxLength = 0
        Me.fndCusType.FieldName = Nothing
        Me.fndCusType.isCalculatedField = False
        Me.fndCusType.IsSourceFromTable = False
        Me.fndCusType.IsSourceFromValueList = False
        Me.fndCusType.IsUnique = False
        Me.fndCusType.Location = New System.Drawing.Point(156, 37)
        Me.fndCusType.MendatroryField = False
        Me.fndCusType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCusType.MyLinkLable1 = Nothing
        Me.fndCusType.MyLinkLable2 = Nothing
        Me.fndCusType.MyReadOnly = False
        Me.fndCusType.MyShowMasterFormButton = False
        Me.fndCusType.Name = "fndCusType"
        Me.fndCusType.ReferenceFieldDesc = Nothing
        Me.fndCusType.ReferenceFieldName = Nothing
        Me.fndCusType.ReferenceTableName = Nothing
        Me.fndCusType.Size = New System.Drawing.Size(143, 19)
        Me.fndCusType.TabIndex = 1
        Me.fndCusType.Value = ""
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
        Me.fndRoute.Location = New System.Drawing.Point(156, 63)
        Me.fndRoute.MendatroryField = False
        Me.fndRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRoute.MyLinkLable1 = Nothing
        Me.fndRoute.MyLinkLable2 = Nothing
        Me.fndRoute.MyReadOnly = False
        Me.fndRoute.MyShowMasterFormButton = False
        Me.fndRoute.Name = "fndRoute"
        Me.fndRoute.ReferenceFieldDesc = Nothing
        Me.fndRoute.ReferenceFieldName = Nothing
        Me.fndRoute.ReferenceTableName = Nothing
        Me.fndRoute.Size = New System.Drawing.Size(143, 19)
        Me.fndRoute.TabIndex = 2
        Me.fndRoute.Value = ""
        '
        'fndCusCategory
        '
        Me.fndCusCategory.CalculationExpression = Nothing
        Me.fndCusCategory.FieldCode = Nothing
        Me.fndCusCategory.FieldDesc = Nothing
        Me.fndCusCategory.FieldMaxLength = 0
        Me.fndCusCategory.FieldName = Nothing
        Me.fndCusCategory.isCalculatedField = False
        Me.fndCusCategory.IsSourceFromTable = False
        Me.fndCusCategory.IsSourceFromValueList = False
        Me.fndCusCategory.IsUnique = False
        Me.fndCusCategory.Location = New System.Drawing.Point(156, 11)
        Me.fndCusCategory.MendatroryField = False
        Me.fndCusCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCusCategory.MyLinkLable1 = Nothing
        Me.fndCusCategory.MyLinkLable2 = Nothing
        Me.fndCusCategory.MyReadOnly = False
        Me.fndCusCategory.MyShowMasterFormButton = False
        Me.fndCusCategory.Name = "fndCusCategory"
        Me.fndCusCategory.ReferenceFieldDesc = Nothing
        Me.fndCusCategory.ReferenceFieldName = Nothing
        Me.fndCusCategory.ReferenceTableName = Nothing
        Me.fndCusCategory.Size = New System.Drawing.Size(143, 19)
        Me.fndCusCategory.TabIndex = 0
        Me.fndCusCategory.Value = ""
        '
        'cboCustomerClass
        '
        Me.cboCustomerClass.AutoCompleteDisplayMember = Nothing
        Me.cboCustomerClass.AutoCompleteValueMember = Nothing
        Me.cboCustomerClass.CalculationExpression = Nothing
        Me.cboCustomerClass.DropDownAnimationEnabled = True
        Me.cboCustomerClass.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCustomerClass.Enabled = False
        Me.cboCustomerClass.FieldCode = Nothing
        Me.cboCustomerClass.FieldDesc = Nothing
        Me.cboCustomerClass.FieldMaxLength = 0
        Me.cboCustomerClass.FieldName = Nothing
        Me.cboCustomerClass.isCalculatedField = False
        Me.cboCustomerClass.IsSourceFromTable = False
        Me.cboCustomerClass.IsSourceFromValueList = False
        Me.cboCustomerClass.IsUnique = False
        Me.cboCustomerClass.Location = New System.Drawing.Point(156, 250)
        Me.cboCustomerClass.MendatroryField = False
        Me.cboCustomerClass.MyLinkLable1 = Nothing
        Me.cboCustomerClass.MyLinkLable2 = Nothing
        Me.cboCustomerClass.Name = "cboCustomerClass"
        Me.cboCustomerClass.ReferenceFieldDesc = Nothing
        Me.cboCustomerClass.ReferenceFieldName = Nothing
        Me.cboCustomerClass.ReferenceTableName = Nothing
        Me.cboCustomerClass.Size = New System.Drawing.Size(143, 20)
        Me.cboCustomerClass.TabIndex = 10
        '
        'RadLabel33
        '
        Me.RadLabel33.FieldName = Nothing
        Me.RadLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel33.Location = New System.Drawing.Point(10, 253)
        Me.RadLabel33.Name = "RadLabel33"
        Me.RadLabel33.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel33.TabIndex = 81
        Me.RadLabel33.Text = "Retailer Class"
        '
        'txtroutegroup
        '
        Me.txtroutegroup.CalculationExpression = Nothing
        Me.txtroutegroup.FieldCode = Nothing
        Me.txtroutegroup.FieldDesc = Nothing
        Me.txtroutegroup.FieldMaxLength = 0
        Me.txtroutegroup.FieldName = Nothing
        Me.txtroutegroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtroutegroup.isCalculatedField = False
        Me.txtroutegroup.IsSourceFromTable = False
        Me.txtroutegroup.IsSourceFromValueList = False
        Me.txtroutegroup.IsUnique = False
        Me.txtroutegroup.Location = New System.Drawing.Point(305, 225)
        Me.txtroutegroup.MendatroryField = False
        Me.txtroutegroup.MyLinkLable1 = Nothing
        Me.txtroutegroup.MyLinkLable2 = Nothing
        Me.txtroutegroup.Name = "txtroutegroup"
        Me.txtroutegroup.ReadOnly = True
        Me.txtroutegroup.ReferenceFieldDesc = Nothing
        Me.txtroutegroup.ReferenceFieldName = Nothing
        Me.txtroutegroup.ReferenceTableName = Nothing
        Me.txtroutegroup.Size = New System.Drawing.Size(394, 18)
        Me.txtroutegroup.TabIndex = 9
        Me.txtroutegroup.TabStop = False
        '
        'lblroutegrp
        '
        Me.lblroutegrp.FieldName = Nothing
        Me.lblroutegrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblroutegrp.Location = New System.Drawing.Point(10, 226)
        Me.lblroutegrp.Name = "lblroutegrp"
        Me.lblroutegrp.Size = New System.Drawing.Size(101, 16)
        Me.lblroutegrp.TabIndex = 80
        Me.lblroutegrp.Text = "Route Group Code"
        '
        'txtSalesPerson
        '
        Me.txtSalesPerson.CalculationExpression = Nothing
        Me.txtSalesPerson.Enabled = False
        Me.txtSalesPerson.FieldCode = Nothing
        Me.txtSalesPerson.FieldDesc = Nothing
        Me.txtSalesPerson.FieldMaxLength = 0
        Me.txtSalesPerson.FieldName = Nothing
        Me.txtSalesPerson.isCalculatedField = False
        Me.txtSalesPerson.IsSourceFromTable = False
        Me.txtSalesPerson.IsSourceFromValueList = False
        Me.txtSalesPerson.IsUnique = False
        Me.txtSalesPerson.Location = New System.Drawing.Point(305, 90)
        Me.txtSalesPerson.MendatroryField = False
        Me.txtSalesPerson.MyLinkLable1 = Nothing
        Me.txtSalesPerson.MyLinkLable2 = Nothing
        Me.txtSalesPerson.Name = "txtSalesPerson"
        Me.txtSalesPerson.ReadOnly = True
        Me.txtSalesPerson.ReferenceFieldDesc = Nothing
        Me.txtSalesPerson.ReferenceFieldName = Nothing
        Me.txtSalesPerson.ReferenceTableName = Nothing
        Me.txtSalesPerson.Size = New System.Drawing.Size(394, 20)
        Me.txtSalesPerson.TabIndex = 5
        Me.txtSalesPerson.TabStop = False
        '
        'RadLabel34
        '
        Me.RadLabel34.FieldName = Nothing
        Me.RadLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel34.Location = New System.Drawing.Point(10, 86)
        Me.RadLabel34.Name = "RadLabel34"
        Me.RadLabel34.Size = New System.Drawing.Size(104, 16)
        Me.RadLabel34.TabIndex = 77
        Me.RadLabel34.Text = "Sales Person Code"
        '
        'txtChannel
        '
        Me.txtChannel.CalculationExpression = Nothing
        Me.txtChannel.FieldCode = Nothing
        Me.txtChannel.FieldDesc = Nothing
        Me.txtChannel.FieldMaxLength = 0
        Me.txtChannel.FieldName = Nothing
        Me.txtChannel.isCalculatedField = False
        Me.txtChannel.IsSourceFromTable = False
        Me.txtChannel.IsSourceFromValueList = False
        Me.txtChannel.IsUnique = False
        Me.txtChannel.Location = New System.Drawing.Point(305, 198)
        Me.txtChannel.MendatroryField = False
        Me.txtChannel.MyLinkLable1 = Nothing
        Me.txtChannel.MyLinkLable2 = Nothing
        Me.txtChannel.Name = "txtChannel"
        Me.txtChannel.ReadOnly = True
        Me.txtChannel.ReferenceFieldDesc = Nothing
        Me.txtChannel.ReferenceFieldName = Nothing
        Me.txtChannel.ReferenceTableName = Nothing
        Me.txtChannel.Size = New System.Drawing.Size(394, 20)
        Me.txtChannel.TabIndex = 7
        Me.txtChannel.TabStop = False
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel24.Location = New System.Drawing.Point(10, 200)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel24.TabIndex = 71
        Me.RadLabel24.Text = "Channel Code"
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(10, 61)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel22.TabIndex = 67
        Me.RadLabel22.Text = "Route Code"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(10, 36)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel19.TabIndex = 67
        Me.RadLabel19.Text = "Retailer Type"
        '
        'txtRoute
        '
        Me.txtRoute.CalculationExpression = Nothing
        Me.txtRoute.FieldCode = Nothing
        Me.txtRoute.FieldDesc = Nothing
        Me.txtRoute.FieldMaxLength = 0
        Me.txtRoute.FieldName = Nothing
        Me.txtRoute.isCalculatedField = False
        Me.txtRoute.IsSourceFromTable = False
        Me.txtRoute.IsSourceFromValueList = False
        Me.txtRoute.IsUnique = False
        Me.txtRoute.Location = New System.Drawing.Point(305, 63)
        Me.txtRoute.MendatroryField = False
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReadOnly = True
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(393, 20)
        Me.txtRoute.TabIndex = 3
        Me.txtRoute.TabStop = False
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel17.Location = New System.Drawing.Point(10, 10)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(94, 16)
        Me.RadLabel17.TabIndex = 65
        Me.RadLabel17.Text = "Retailer Category"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel40)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel36)
        Me.RadPageViewPage5.Controls.Add(Me.txtAddInfo3)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel37)
        Me.RadPageViewPage5.Controls.Add(Me.txtRemarks2)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel38)
        Me.RadPageViewPage5.Controls.Add(Me.txtAddInfo2)
        Me.RadPageViewPage5.Controls.Add(Me.txtAddInfo1)
        Me.RadPageViewPage5.Controls.Add(Me.RadLabel39)
        Me.RadPageViewPage5.Controls.Add(Me.txtRemarks1)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(93.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(935, 388)
        Me.RadPageViewPage5.Text = "Additional Info."
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(137, 161)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(499, 209)
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
        Me.gvDB.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvDB.MyStopExport = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.ShowHeaderCellButtons = True
        Me.gvDB.Size = New System.Drawing.Size(479, 179)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        '
        'RadLabel40
        '
        Me.RadLabel40.FieldName = Nothing
        Me.RadLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel40.Location = New System.Drawing.Point(9, 127)
        Me.RadLabel40.Name = "RadLabel40"
        Me.RadLabel40.Size = New System.Drawing.Size(122, 16)
        Me.RadLabel40.TabIndex = 88
        Me.RadLabel40.Text = "Additional Information3"
        '
        'RadLabel36
        '
        Me.RadLabel36.FieldName = Nothing
        Me.RadLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel36.Location = New System.Drawing.Point(9, 51)
        Me.RadLabel36.Name = "RadLabel36"
        Me.RadLabel36.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel36.TabIndex = 89
        Me.RadLabel36.Text = "Remarks2"
        '
        'txtAddInfo3
        '
        Me.txtAddInfo3.CalculationExpression = Nothing
        Me.txtAddInfo3.FieldCode = Nothing
        Me.txtAddInfo3.FieldDesc = Nothing
        Me.txtAddInfo3.FieldMaxLength = 0
        Me.txtAddInfo3.FieldName = Nothing
        Me.txtAddInfo3.isCalculatedField = False
        Me.txtAddInfo3.IsSourceFromTable = False
        Me.txtAddInfo3.IsSourceFromValueList = False
        Me.txtAddInfo3.IsUnique = False
        Me.txtAddInfo3.Location = New System.Drawing.Point(137, 125)
        Me.txtAddInfo3.MaxLength = 75
        Me.txtAddInfo3.MendatroryField = False
        Me.txtAddInfo3.MyLinkLable1 = Nothing
        Me.txtAddInfo3.MyLinkLable2 = Nothing
        Me.txtAddInfo3.Name = "txtAddInfo3"
        Me.txtAddInfo3.ReferenceFieldDesc = Nothing
        Me.txtAddInfo3.ReferenceFieldName = Nothing
        Me.txtAddInfo3.ReferenceTableName = Nothing
        Me.txtAddInfo3.Size = New System.Drawing.Size(550, 20)
        Me.txtAddInfo3.TabIndex = 4
        '
        'RadLabel37
        '
        Me.RadLabel37.FieldName = Nothing
        Me.RadLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel37.Location = New System.Drawing.Point(9, 101)
        Me.RadLabel37.Name = "RadLabel37"
        Me.RadLabel37.Size = New System.Drawing.Size(122, 16)
        Me.RadLabel37.TabIndex = 86
        Me.RadLabel37.Text = "Additional Information2"
        '
        'txtRemarks2
        '
        Me.txtRemarks2.CalculationExpression = Nothing
        Me.txtRemarks2.FieldCode = Nothing
        Me.txtRemarks2.FieldDesc = Nothing
        Me.txtRemarks2.FieldMaxLength = 0
        Me.txtRemarks2.FieldName = Nothing
        Me.txtRemarks2.isCalculatedField = False
        Me.txtRemarks2.IsSourceFromTable = False
        Me.txtRemarks2.IsSourceFromValueList = False
        Me.txtRemarks2.IsUnique = False
        Me.txtRemarks2.Location = New System.Drawing.Point(137, 47)
        Me.txtRemarks2.MaxLength = 75
        Me.txtRemarks2.MendatroryField = False
        Me.txtRemarks2.MyLinkLable1 = Nothing
        Me.txtRemarks2.MyLinkLable2 = Nothing
        Me.txtRemarks2.Name = "txtRemarks2"
        Me.txtRemarks2.ReferenceFieldDesc = Nothing
        Me.txtRemarks2.ReferenceFieldName = Nothing
        Me.txtRemarks2.ReferenceTableName = Nothing
        Me.txtRemarks2.Size = New System.Drawing.Size(550, 20)
        Me.txtRemarks2.TabIndex = 1
        '
        'RadLabel38
        '
        Me.RadLabel38.FieldName = Nothing
        Me.RadLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel38.Location = New System.Drawing.Point(9, 76)
        Me.RadLabel38.Name = "RadLabel38"
        Me.RadLabel38.Size = New System.Drawing.Size(122, 16)
        Me.RadLabel38.TabIndex = 87
        Me.RadLabel38.Text = "Additional Information1"
        '
        'txtAddInfo2
        '
        Me.txtAddInfo2.CalculationExpression = Nothing
        Me.txtAddInfo2.FieldCode = Nothing
        Me.txtAddInfo2.FieldDesc = Nothing
        Me.txtAddInfo2.FieldMaxLength = 0
        Me.txtAddInfo2.FieldName = Nothing
        Me.txtAddInfo2.isCalculatedField = False
        Me.txtAddInfo2.IsSourceFromTable = False
        Me.txtAddInfo2.IsSourceFromValueList = False
        Me.txtAddInfo2.IsUnique = False
        Me.txtAddInfo2.Location = New System.Drawing.Point(137, 99)
        Me.txtAddInfo2.MaxLength = 75
        Me.txtAddInfo2.MendatroryField = False
        Me.txtAddInfo2.MyLinkLable1 = Nothing
        Me.txtAddInfo2.MyLinkLable2 = Nothing
        Me.txtAddInfo2.Name = "txtAddInfo2"
        Me.txtAddInfo2.ReferenceFieldDesc = Nothing
        Me.txtAddInfo2.ReferenceFieldName = Nothing
        Me.txtAddInfo2.ReferenceTableName = Nothing
        Me.txtAddInfo2.Size = New System.Drawing.Size(550, 20)
        Me.txtAddInfo2.TabIndex = 3
        '
        'txtAddInfo1
        '
        Me.txtAddInfo1.CalculationExpression = Nothing
        Me.txtAddInfo1.FieldCode = Nothing
        Me.txtAddInfo1.FieldDesc = Nothing
        Me.txtAddInfo1.FieldMaxLength = 0
        Me.txtAddInfo1.FieldName = Nothing
        Me.txtAddInfo1.isCalculatedField = False
        Me.txtAddInfo1.IsSourceFromTable = False
        Me.txtAddInfo1.IsSourceFromValueList = False
        Me.txtAddInfo1.IsUnique = False
        Me.txtAddInfo1.Location = New System.Drawing.Point(137, 73)
        Me.txtAddInfo1.MaxLength = 75
        Me.txtAddInfo1.MendatroryField = False
        Me.txtAddInfo1.MyLinkLable1 = Nothing
        Me.txtAddInfo1.MyLinkLable2 = Nothing
        Me.txtAddInfo1.Name = "txtAddInfo1"
        Me.txtAddInfo1.ReferenceFieldDesc = Nothing
        Me.txtAddInfo1.ReferenceFieldName = Nothing
        Me.txtAddInfo1.ReferenceTableName = Nothing
        Me.txtAddInfo1.Size = New System.Drawing.Size(550, 20)
        Me.txtAddInfo1.TabIndex = 2
        '
        'RadLabel39
        '
        Me.RadLabel39.FieldName = Nothing
        Me.RadLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel39.Location = New System.Drawing.Point(9, 21)
        Me.RadLabel39.Name = "RadLabel39"
        Me.RadLabel39.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel39.TabIndex = 83
        Me.RadLabel39.Text = "Remarks1"
        '
        'txtRemarks1
        '
        Me.txtRemarks1.CalculationExpression = Nothing
        Me.txtRemarks1.FieldCode = Nothing
        Me.txtRemarks1.FieldDesc = Nothing
        Me.txtRemarks1.FieldMaxLength = 0
        Me.txtRemarks1.FieldName = Nothing
        Me.txtRemarks1.isCalculatedField = False
        Me.txtRemarks1.IsSourceFromTable = False
        Me.txtRemarks1.IsSourceFromValueList = False
        Me.txtRemarks1.IsUnique = False
        Me.txtRemarks1.Location = New System.Drawing.Point(137, 21)
        Me.txtRemarks1.MaxLength = 75
        Me.txtRemarks1.MendatroryField = False
        Me.txtRemarks1.MyLinkLable1 = Nothing
        Me.txtRemarks1.MyLinkLable2 = Nothing
        Me.txtRemarks1.Name = "txtRemarks1"
        Me.txtRemarks1.ReferenceFieldDesc = Nothing
        Me.txtRemarks1.ReferenceFieldName = Nothing
        Me.txtRemarks1.ReferenceTableName = Nothing
        Me.txtRemarks1.Size = New System.Drawing.Size(550, 20)
        Me.txtRemarks1.TabIndex = 0
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(935, 388)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(935, 388)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(935, 388)
        Me.Attachments.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(935, 388)
        Me.UcAttachment1.TabIndex = 1
        '
        'CrateAccounting
        '
        Me.CrateAccounting.Controls.Add(Me.gvCrate)
        Me.CrateAccounting.ItemSize = New System.Drawing.SizeF(103.0!, 28.0!)
        Me.CrateAccounting.Location = New System.Drawing.Point(10, 37)
        Me.CrateAccounting.Name = "CrateAccounting"
        Me.CrateAccounting.Size = New System.Drawing.Size(935, 388)
        Me.CrateAccounting.Text = "Crate Accounting"
        '
        'gvCrate
        '
        Me.gvCrate.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCrate.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gvCrate.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCrate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCrate.ForeColor = System.Drawing.Color.Black
        Me.gvCrate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCrate.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvCrate.MasterTemplate.AllowAddNewRow = False
        Me.gvCrate.MasterTemplate.AllowDeleteRow = False
        Me.gvCrate.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCrate.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCrate.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvCrate.MyStopExport = False
        Me.gvCrate.Name = "gvCrate"
        Me.gvCrate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCrate.ShowHeaderCellButtons = True
        Me.gvCrate.Size = New System.Drawing.Size(935, 388)
        Me.gvCrate.TabIndex = 2
        Me.gvCrate.TabStop = False
        '
        'Competitor
        '
        Me.Competitor.Controls.Add(Me.gvCompetitor)
        Me.Competitor.ItemSize = New System.Drawing.SizeF(105.0!, 28.0!)
        Me.Competitor.Location = New System.Drawing.Point(10, 37)
        Me.Competitor.Name = "Competitor"
        Me.Competitor.Size = New System.Drawing.Size(935, 388)
        Me.Competitor.Text = "Competitor Detail"
        '
        'gvCompetitor
        '
        Me.gvCompetitor.BackColor = System.Drawing.Color.Transparent
        Me.gvCompetitor.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCompetitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCompetitor.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvCompetitor.ForeColor = System.Drawing.Color.Black
        Me.gvCompetitor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCompetitor.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvCompetitor.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCompetitor.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCompetitor.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvCompetitor.MyStopExport = False
        Me.gvCompetitor.Name = "gvCompetitor"
        Me.gvCompetitor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCompetitor.ShowGroupPanel = False
        Me.gvCompetitor.ShowHeaderCellButtons = True
        Me.gvCompetitor.Size = New System.Drawing.Size(935, 388)
        Me.gvCompetitor.TabIndex = 1451
        Me.gvCompetitor.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 16
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(873, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 18
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(86, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 17
        Me.btnDelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(956, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.rmiImportCompetitor, Me.RadMenuItem3, Me.rmiExportCompetitor, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'rmiImportCompetitor
        '
        Me.rmiImportCompetitor.Name = "rmiImportCompetitor"
        Me.rmiImportCompetitor.Text = "Import Competitor"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'rmiExportCompetitor
        '
        Me.rmiExportCompetitor.Name = "rmiExportCompetitor"
        Me.rmiExportCompetitor.Text = "Export Competitor"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Exit"
        '
        'FrmSecondaryCustomerMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 590)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmSecondaryCustomerMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Secondary Customer Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cboBusinessType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBusinessType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAggClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAggMade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtParentCstmrNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParentCustDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbCustomerType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAliesName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAliesName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBaseCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.pageCus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageCus.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtPhone2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCountryName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCusGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.dtClosing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCusgrp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.txtContPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactFax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactWeb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContactName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.GBGST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBGST.ResumeLayout(False)
        Me.GBGST.PerformLayout()
        CType(Me.ChkRegistered, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGSTDegit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGSTNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGSTBlank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGSTEntityNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGSTPANNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGstNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGstState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCheckCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttempCreditLimitTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttempCreditLimitFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTempCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.drpformtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcollectorate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcollect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblecc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtecc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTax.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTxGrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLstNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTinNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStaxNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.TxtCrateOpeningQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCrateOpeningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkpricegrpslctr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCustomerClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtroutegroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblroutegrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChannel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.RadPageViewPage5.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddInfo3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddInfo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.CrateAccounting.ResumeLayout(False)
        CType(Me.gvCrate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCrate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Competitor.ResumeLayout(False)
        CType(Me.gvCompetitor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCompetitor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblAliesName As common.Controls.MyLabel
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtAliesName As common.Controls.MyTextBox
    Friend WithEvents lblTransaction As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndCustCurrency As common.UserControls.txtFinder
    Friend WithEvents lblBaseCurrency As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCustomer As common.UserControls.txtNavigator
    Friend WithEvents CmbTransaction As common.Controls.MyComboBox
    Friend WithEvents pageCus As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtPinNo As common.Controls.MyTextBox
    Friend WithEvents lblPinNo As common.Controls.MyLabel
    Friend WithEvents fndCountry As common.UserControls.txtFinder
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtCountryName As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtStateName As common.Controls.MyTextBox
    Friend WithEvents fndstate As common.UserControls.txtFinder
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents fndCity As common.UserControls.txtFinder
    Friend WithEvents fndCusgrp As common.UserControls.txtFinder
    Friend WithEvents lblCusGrp As common.Controls.MyLabel
    Friend WithEvents chkcredit As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents txtWeb As common.Controls.MyTextBox
    Friend WithEvents txtfax As common.Controls.MyTextBox
    Friend WithEvents txtEmail As common.Controls.MyTextBox
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents txtCity As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtClosing As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents chkInActive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents txtCusgrp As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel41 As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents txtContactFax As common.Controls.MyTextBox
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtContactWeb As common.Controls.MyTextBox
    Friend WithEvents txtContactEmail As common.Controls.MyTextBox
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents txtContactName As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents ChkOther As common.Controls.MyCheckBox
    Friend WithEvents ChkCheckCreditLimit As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txttempCreditLimitTo As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txttempCreditLimitFrom As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtTempCreditLimit As common.Controls.MyTextBox
    Friend WithEvents fndPayCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel26 As common.Controls.MyLabel
    Friend WithEvents fndAccntSet As common.UserControls.txtFinder
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents fndTrmsCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents drpformtype As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lbldivision As common.Controls.MyLabel
    Friend WithEvents txtdivision As common.Controls.MyTextBox
    Friend WithEvents lblpan As common.Controls.MyLabel
    Friend WithEvents txtpan As common.Controls.MyTextBox
    Friend WithEvents lblcollectorate As common.Controls.MyLabel
    Friend WithEvents txtcollect As common.Controls.MyTextBox
    Friend WithEvents lblrange As common.Controls.MyLabel
    Friend WithEvents lblecc As common.Controls.MyLabel
    Friend WithEvents txtrange As common.Controls.MyTextBox
    Friend WithEvents txtecc As common.Controls.MyTextBox
    Friend WithEvents lblcst As common.Controls.MyLabel
    Friend WithEvents txtcst As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndTxGrp As common.UserControls.txtFinder
    Friend WithEvents RadLabel35 As common.Controls.MyLabel
    Friend WithEvents grdTax As common.UserControls.MyRadGridView
    Friend WithEvents txtTxGrp As common.Controls.MyTextBox
    Friend WithEvents RadLabel30 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents txtCredit As common.Controls.MyTextBox
    Friend WithEvents txtLstNo As common.Controls.MyTextBox
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents RadLabel28 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents txtTinNo As common.Controls.MyTextBox
    Friend WithEvents txtStaxNo As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel40 As common.Controls.MyLabel
    Friend WithEvents RadLabel36 As common.Controls.MyLabel
    Friend WithEvents txtAddInfo3 As common.Controls.MyTextBox
    Friend WithEvents RadLabel37 As common.Controls.MyLabel
    Friend WithEvents txtRemarks2 As common.Controls.MyTextBox
    Friend WithEvents RadLabel38 As common.Controls.MyLabel
    Friend WithEvents txtAddInfo2 As common.Controls.MyTextBox
    Friend WithEvents txtAddInfo1 As common.Controls.MyTextBox
    Friend WithEvents RadLabel39 As common.Controls.MyLabel
    Friend WithEvents txtRemarks1 As common.Controls.MyTextBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents CrateAccounting As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvCrate As common.UserControls.MyRadGridView
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents CmbCustomerType As common.Controls.MyComboBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtCrateOpeningQty As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents TxtCrateOpeningDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents fndZone As common.UserControls.txtFinder
    Friend WithEvents lblZone As common.Controls.MyLabel
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtpgfnd As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkpricegrpslctr As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadLabel42 As common.Controls.MyLabel
    Friend WithEvents txtPriceCode As common.UserControls.txtFinder
    Friend WithEvents txtPriceCodeNon As common.UserControls.txtFinder
    Friend WithEvents fndroutegroup As common.UserControls.txtFinder
    Friend WithEvents fndChannel As common.UserControls.txtFinder
    Friend WithEvents fndSalePerson As common.UserControls.txtFinder
    Friend WithEvents fndCusType As common.UserControls.txtFinder
    Friend WithEvents fndRoute As common.UserControls.txtFinder
    Friend WithEvents fndCusCategory As common.UserControls.txtFinder
    Friend WithEvents cboCustomerClass As common.Controls.MyComboBox
    Friend WithEvents RadLabel33 As common.Controls.MyLabel
    Friend WithEvents txtroutegroup As common.Controls.MyTextBox
    Friend WithEvents lblroutegrp As common.Controls.MyLabel
    Friend WithEvents txtSalesPerson As common.Controls.MyTextBox
    Friend WithEvents RadLabel34 As common.Controls.MyLabel
    Friend WithEvents txtChannel As common.Controls.MyTextBox
    Friend WithEvents RadLabel24 As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.Controls.MyTextBox
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents txtParentCstNo As common.UserControls.txtFinder
    Friend WithEvents txtParentCstmrNo As common.Controls.MyTextBox
    Friend WithEvents lblParentCustDesc As common.Controls.MyLabel
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents dtpAggClose As common.Controls.MyDateTimePicker
    Friend WithEvents dtpAggMade As common.Controls.MyDateTimePicker
    Friend WithEvents GBGST As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblGSTNo As common.Controls.MyLabel
    Friend WithEvents txtGstState As common.Controls.MyTextBox
    Friend WithEvents txtGSTDegit As common.Controls.MyTextBox
    Friend WithEvents txtGSTBlank As common.Controls.MyTextBox
    Friend WithEvents txtGSTEntityNo As common.Controls.MyTextBox
    Friend WithEvents txtGSTPANNO As common.Controls.MyTextBox
    Friend WithEvents txtGstNo As common.Controls.MyTextBox
    Friend WithEvents ChkRegistered As common.Controls.MyCheckBox
    Friend WithEvents lblBusinessType As common.Controls.MyLabel
    Friend WithEvents cboBusinessType As common.Controls.MyComboBox
    Friend WithEvents Competitor As RadPageViewPage
    Friend WithEvents gvCompetitor As common.UserControls.MyRadGridView
    Friend WithEvents rmiImportCompetitor As RadMenuItem
    Friend WithEvents rmiExportCompetitor As RadMenuItem
    Friend WithEvents txtPhone2 As common.Controls.MyTextBox
    Friend WithEvents txtPhone1 As common.Controls.MyTextBox
    Friend WithEvents txtContPhone As common.Controls.MyTextBox
End Class

