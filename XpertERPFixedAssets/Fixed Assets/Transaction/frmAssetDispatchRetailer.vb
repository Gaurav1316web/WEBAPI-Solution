'sanjay 11-Nov-2021 
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.Data

Public Class frmAssetDispatchRetailer
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqIssueNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoAssetId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoDepositType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Dim repoDepositRecNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoDepositValue As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repolBrandName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoPIDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
    Dim repoPINumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCapacity As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoSerialNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoReturnQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoPenQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim PickAvgCostonAssetissue As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colReq_IssueNo As String = "Req_IssueNo"
    Const colAssetCode As String = "colAssetCode"
    Const colAssetName As String = "colAssetName"
    Const colITEMCODE As String = "COLITEMCODE"
    Const colITEMNAME As String = "COLITEMNAME"
    Const colASSETID As String = "COLASSETID"
    Const colDepositType As String = "colDepositType"
    Const colDepositRecNo As String = "colDepositRecNo"
    Const colDepositValue As String = "colDepositValue"
    Const colBrandName As String = "colBrandName"
    Const colPIDate As String = "colPIDate"
    Const colPINumber As String = "colPINumber"
    Const colCapacity As String = "colCapacity"
    Const colSerialNo As String = "colSerialNo"
    Const colHierarchyCode As String = "colHierarchyCode"
    Const colHierarchyName As String = "colHierarchyName"
    Const colHierarchyLevelNumber As String = "colHierarchyLevelNumber"
    Const colCCCode As String = "COLCCCode"
    Const colCCDesc As String = "COLCCDesc"
    Const colQty As String = "COLQTY"
    Const colRetQty As String = "COLRetQTY"
    Const colUnit As String = "COLUNIT"
    Const colReqQty As String = "COLREQQTY"
    Const colPenQty As String = "COLPenQTY"

    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"

    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"

    Const colIsExcisable1 As String = "ISEXCISABLE1"
    Const colIsExcisable2 As String = "ISEXCISABLE2"
    Const colIsExcisable3 As String = "ISEXCISABLE3"
    Const colIsExcisable4 As String = "ISEXCISABLE4"
    Const colIsExcisable5 As String = "ISEXCISABLE5"
    Const colIsExcisable6 As String = "ISEXCISABLE6"
    Const colIsExcisable7 As String = "ISEXCISABLE7"
    Const colIsExcisable8 As String = "ISEXCISABLE8"
    Const colIsExcisable9 As String = "ISEXCISABLE9"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"

    Const colEMIAssetValue As String = "colEMIAssetValue"
    Const colEMINoOfPaymentCycle As String = "colEMINoOfPaymentCycle"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public DocumentNo As String = Nothing
    Dim IsApplyEMIOnAssetValue As Boolean = False

#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAssetDispatchRetailer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnprint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = True

        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If MyBase.isExport = True Then
            Btn_export.Enabled = True
            rmUploderBlankSheet.Enabled = True
            BtnImport.Enabled = True
        Else
            Btn_export.Enabled = False
            rmUploderBlankSheet.Enabled = False
            BtnImport.Enabled = False
        End If
    End Sub

    Private Sub frmAssetDispatchRetailer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Doc_No", "varchar(30) NOT NULL Primary Key")
        coll.Add("Doc_Date", "varchar(10) NOT NULL")
        coll.Add("Doc_Type", "Varchar(10) NULL")
        coll.Add("From_Location", "varchar(12) NULL")
        coll.Add("To_Location", "varchar(12) NULL")
        coll.Add("Distributor_Code", "varchar(12) NULL references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Remarks", "varchar(200) NULL")
        coll.Add("Comment", "varchar(200) NULL")
        coll.Add("Issue_To", "varchar(12) NULL")
        coll.Add("Request_By", "varchar(12) NULL")
        coll.Add("comp_code", "Varchar(8) NOT NULL")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Varchar(10) NOT NULL")
        coll.Add("Modify_By", "varchar(12) NOT NULL")
        coll.Add("Modify_Date", "Varchar(10) NOT NULL")
        coll.Add("Status", "integer not null default 0")
        coll.Add("On_Hold", "integer not null default 0")
        coll.Add("Posting_Date", "Varchar(10) NULL")
        coll.Add("Dept", "Varchar(12) null")
        coll.Add("Dept_Desc", "Varchar(50) NULL")
        coll.Add("Tax_Group", "varchar(12)  NULL")
        coll.Add("Tax_Desc", "varchar(50) NULL")
        coll.Add("TAX1", "varchar(12) NULL")
        coll.Add("TAX1_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX1_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX1_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX2", "varchar(12) NULL")
        coll.Add("TAX2_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX2_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX2_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX3", "varchar(12) NULL")
        coll.Add("TAX3_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX3_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX3_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX4", "varchar(12) NULL")
        coll.Add("TAX4_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX4_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX4_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX5", "varchar(12) NULL")
        coll.Add("TAX5_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX5_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX5_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX6", "varchar(12) NULL")
        coll.Add("TAX6_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX6_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX6_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX7", "varchar(12) NULL")
        coll.Add("TAX7_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX7_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX7_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX8", "varchar(12) NULL")
        coll.Add("TAX8_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX8_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX8_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX9", "varchar(12) NULL")
        coll.Add("TAX9_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX9_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX9_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX10", "varchar(12) NULL")
        coll.Add("TAX10_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX10_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX10_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("BeforeTax_Amt", "decimal(18, 2) NULL")
        coll.Add("Total_Tax_Amt", "decimal(18, 2) NULL")
        coll.Add("Doc_Amt", "decimal(18, 2) NULL")
        coll.Add("Vehicle_Id", "varchar(12)")
        coll.Add("Machine_Id", "varchar(12)")
        coll.Add("Req_IssueNo", "varchar(30)")
        coll.Add("RequisitionNo", "varchar(30)")
        coll.Add("Issue_No", "varchar(30) Null")
        coll.Add("IS_LOST", "integer not null default 0")
        coll.Add("IsItemWise", "integer not null default 0")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ASSET_DISPATCH_RETAILER_HEAD", coll, Nothing, False, False, "", "Doc_No", "Doc_Date")

        coll = New Dictionary(Of String, String)
        coll.Add("Doc_No", "Varchar(30) not null References TSPL_ASSET_DISPATCH_RETAILER_HEAD(Doc_No)")
        coll.Add("Line_No", "integer not null default 0")
        coll.Add("Item_Code", "varchar(50) NOT NULL")
        coll.Add("Item_Desc", "varchar(100) NULL")
        coll.Add("Cost_Code", "varchar(30) References TSPL_CostCenter_MASTER(Cost_Code)")
        coll.Add("Required_Qty", "decimal(18, 2) NULL")
        coll.Add("Issued_Qty", "decimal(18, 2) NULL")
        coll.Add("Unit_code", "varchar(12) NULL")
        coll.Add("Unit_Cost", "decimal(18, 2) NULL")
        coll.Add("Issued_Qty_againstret", "decimal(18, 2) NULL")
        coll.Add("Req_IssueNo", "varchar(30)")
        coll.Add("TAX1", "varchar(12) NULL")
        coll.Add("TAX1_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX1_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX1_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX2", "varchar(12) NULL")
        coll.Add("TAX2_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX2_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX2_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX3", "varchar(12) NULL")
        coll.Add("TAX3_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX3_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX3_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX4", "varchar(12) NULL")
        coll.Add("TAX4_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX4_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX4_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX5", "varchar(12) NULL")
        coll.Add("TAX5_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX5_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX5_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX6", "varchar(12) NULL")
        coll.Add("TAX6_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX6_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX6_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX7", "varchar(12) NULL")
        coll.Add("TAX7_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX7_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX7_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX8", "varchar(12) NULL")
        coll.Add("TAX8_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX8_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX8_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX9", "varchar(12) NULL")
        coll.Add("TAX9_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX9_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX9_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX10", "varchar(12) NULL")
        coll.Add("TAX10_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX10_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX10_Amt", "decimal(18, 2) NULL")
        coll.Add("Amount", "decimal(18, 2) NULL")
        coll.Add("Total_Tax_Amt", "decimal(18, 2) NULL")
        coll.Add("Item_Net_Amt", "decimal(18, 2) NULL")

        coll.Add("EMI_Asset_Value", "decimal(18, 2) NULL")
        coll.Add("EMI_No_Of_Payment_Cycle", "decimal(18, 2) NULL")
        coll.Add("Capacity", "decimal(18, 2) NULL")
        coll.Add("DepositType", "varchar(50) NULL")
        coll.Add("DepositReceiptNo", "varchar(100) NULL")
        coll.Add("DepositValue", "decimal(18, 2) NULL")
        coll.Add("BrandName", "varchar(100) NULL")
        coll.Add("PurchaseInvoiceDate", "datetime null")
        coll.Add("PurchaseInvoiceNo", "varchar(100) NULL")
        coll.Add("SerialNo", "varchar(100) NULL")
        coll.Add("AssetCode", "varchar(100) NULL")
        coll.Add("AssetID", "varchar(100) NULL")


        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ASSET_DISPATCH_RETAILER_DETAIL", coll, Nothing, False, False, "TSPL_ASSET_DISPATCH_RETAILER_HEAD", "Doc_No", "")
        '' VSP Asset Issue Ends


        IsApplyEMIOnAssetValue = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsApplyEMIOnAssetValue, clsFixedParameterCode.IsApplyEMIOnAssetValue, Nothing)) = 1, True, False)
        PickAvgCostonAssetissue = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickAvgCostonAssetissue, clsFixedParameterCode.PickAvgCostonAssetissue, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        RadPageView1.SelectedPage = RadPageViewPage1



        RadPageView1.Pages("TaxDetails").Item.Visibility = ElementVisibility.Collapsed
        LoadBlankGrid()
        LoadDocType()
        AddNew()
        SetLength()

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If

        ''End of For Custom Fields
        txtFromLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join tspl_mcc_master on mcc_code=Default_Location  where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtFromLocation.Value) > 0 Then
            lblFromLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtFromLocation.Value + "' "))
        End If
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
        txtComment.MaxLength = 200

    End Sub

    Sub LoadDocType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Issue"
        dr("Name") = "Issue"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Return"
        dr("Name") = "Return"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Transfer"
        'dr("Name") = "Transfer"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Sale"
        'dr("Name") = "Sale"
        'dt.Rows.Add(dr)

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        chkLost.Checked = False
        txtDocNo.Value = ""
        txtComment.Text = ""
        chkOnHold.Checked = False
        chkItem.Enabled = True
        txtIssueTo.Value = ""
        lblIssueTo.Text = ""
        TxtDistributor.Value = ""
        lblDistributor.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtComment.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        cboDocType.SelectedIndex = 0
        cboDocType.Enabled = True
        txtRequestBy.Enabled = True
        txtRequestBy.Value = ""
        lblRequestBy.Text = ""
        txtFromLocation.Value = ""
        lblFromLocation.Text = ""
        txtIssueNo.Value = ""
        'txtDepartment.Value = ""
        'lblDepartment.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        lblDocAmount.Text = ""
        TxtVehicle.Value = ""
        'TxtMachinery.Value = ""
        lblVehicleDesc.Text = ""
        'lblMachineDesc.Text = ""
        txtRouteNo.Value = ""
        lblRouteDesc.Text = ""
        txtAltVehicle.Value = ""
        lblAltVehicleDesc.Text = ""
        txtTaxGroup.Enabled = False
        lblTaxGrpName.Enabled = False
        'fndReqNo.Value = ""
        'lblReqDate.Text = ""

        'added by priti
        'fndReqNo.Enabled = True
        txtFromLocation.Enabled = True
        txtToLocation.Enabled = True
        cboDocType.Enabled = True
        'fndReqNo.Visible = True
        'lblReqDate.Visible = True
        'lblReq.Visible = True
        'lblReq.Text = "Requisition No"

        ' ended by priti

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Public Shared Function GetDepositType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Deposit"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Commitment"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Replacement"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        repoLineNo = New GridViewDecimalColumn()
        repoICode = New GridViewTextBoxColumn()
        repoReqIssueNo = New GridViewTextBoxColumn()
        repoIName = New GridViewTextBoxColumn()
        repoCCCode = New GridViewTextBoxColumn()
        repoCCDesc = New GridViewTextBoxColumn()
        repoQty = New GridViewDecimalColumn()
        repoUnit = New GridViewTextBoxColumn()
        repoReqQty = New GridViewDecimalColumn()
        repoReturnQty = New GridViewDecimalColumn()
        repoPenQty = New GridViewDecimalColumn()

        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "Req. / Issue No"
        repoReqIssueNo.Name = colReq_IssueNo
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        repoReqIssueNo.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)

        repoICode.FormatString = ""
        repoICode.HeaderText = "Asset Code"
        repoICode.Name = colAssetCode
        repoICode.HeaderImage = Global.XpertERPFixedAssets.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 200
        repoICode.ReadOnly = IIf(chkItem.Checked = True, True, False)
        gv1.MasterTemplate.Columns.Add(repoICode)

        repoIName.FormatString = ""
        repoIName.HeaderText = "Asset Description"
        repoIName.Name = colAssetName
        repoIName.Width = 300
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoItemCode = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colITEMCODE
        repoItemCode.Width = 200
        repoItemCode.ReadOnly = IIf(chkItem.Checked = True, False, True)
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        repoItemName = New GridViewTextBoxColumn()
        repoItemName.FormatString = ""
        repoItemName.HeaderText = "Item Description"
        repoItemName.Name = colITEMNAME
        repoItemName.Width = 300
        repoItemName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemName)

        repoAssetId = New GridViewTextBoxColumn()
        repoAssetId.FormatString = ""
        repoAssetId.HeaderText = "Asset Id"
        repoAssetId.Name = colASSETID
        repoAssetId.Width = 200
        repoAssetId.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAssetId)
        '''''''''''''''''''''''''
        repoDepositType = New GridViewComboBoxColumn()
        repoDepositType.FormatString = ""
        repoDepositType.HeaderText = "Deposit Type"
        repoDepositType.Name = colDepositType
        repoDepositType.Width = 100
        repoDepositType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDepositType.DataSource = GetDepositType()
        repoDepositType.ValueMember = "Code"
        repoDepositType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoDepositType)
        repoDepositType = Nothing

        repoDepositRecNo = New GridViewTextBoxColumn()
        repoDepositRecNo.FormatString = ""
        repoDepositRecNo.HeaderText = "Deposit Receipt No"
        repoDepositRecNo.Name = colDepositRecNo
        repoDepositRecNo.Width = 200
        gv1.MasterTemplate.Columns.Add(repoDepositRecNo)

        repoDepositValue = New GridViewDecimalColumn()
        repoDepositValue.FormatString = ""
        repoDepositValue.HeaderText = "Deposit Value"
        repoDepositValue.Name = colDepositValue
        repoDepositValue.Width = 100
        repoDepositValue.Minimum = 0
        repoDepositValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDepositValue)

        repolBrandName = New GridViewTextBoxColumn()
        repolBrandName.FormatString = ""
        repolBrandName.HeaderText = "Brand Name"
        repolBrandName.Name = colBrandName
        repolBrandName.Width = 200
        gv1.MasterTemplate.Columns.Add(repolBrandName)

        repoPIDate = New GridViewDateTimeColumn()
        repoPIDate.Format = DateTimePickerFormat.Custom
        repoPIDate.CustomFormat = "dd-MM-yyyy"
        repoPIDate.HeaderText = "Purchase Invoice Date"
        repoPIDate.FormatString = "{0:d}"
        repoPIDate.Name = colPIDate
        repoPIDate.WrapText = True
        repoPIDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoPIDate)

        repoPINumber = New GridViewTextBoxColumn()
        repoPINumber.FormatString = ""
        repoPINumber.HeaderText = "Purchase Invoice No"
        repoPINumber.Name = colPINumber
        repoPINumber.Width = 200
        gv1.MasterTemplate.Columns.Add(repoPINumber)

        repoCapacity = New GridViewTextBoxColumn()
        repoCapacity.FormatString = ""
        repoCapacity.HeaderText = "Capacity"
        repoCapacity.Name = colCapacity
        repoCapacity.Width = 200
        gv1.MasterTemplate.Columns.Add(repoCapacity)

        repoSerialNo = New GridViewTextBoxColumn()
        repoSerialNo.FormatString = ""
        repoSerialNo.HeaderText = "Serial No"
        repoSerialNo.Name = colSerialNo
        repoSerialNo.Width = 200
        gv1.MasterTemplate.Columns.Add(repoSerialNo)


        '''''''''''''''''''''''''

        Dim repoHierarchyLevelCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelCode.FormatString = ""
        repoHierarchyLevelCode.HeaderText = "Hierarchy Level Code"
        repoHierarchyLevelCode.Name = colHierarchyCode
        repoHierarchyLevelCode.Width = 100
        repoHierarchyLevelCode.ReadOnly = False
        repoHierarchyLevelCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelCode)


        Dim repoHierarchyLevelName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelName.FormatString = ""
        repoHierarchyLevelName.HeaderText = "Hierarchy Level Name"
        repoHierarchyLevelName.Name = colHierarchyName
        repoHierarchyLevelName.Width = 150
        repoHierarchyLevelName.ReadOnly = True
        repoHierarchyLevelName.IsVisible = False
        repoHierarchyLevelName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelName)

        Dim repoHierarchyLevelNumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchyLevelNumber.FormatString = ""
        repoHierarchyLevelNumber.HeaderText = "Hierarchy Level Number"
        repoHierarchyLevelNumber.Name = colHierarchyLevelNumber
        repoHierarchyLevelNumber.Width = 150
        repoHierarchyLevelNumber.ReadOnly = True
        repoHierarchyLevelNumber.IsVisible = False
        repoHierarchyLevelNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelNumber)



        '''''''''''''''''''''''''
        repoCCCode.HeaderText = "Cost Center"
        repoCCCode.Name = colCCCode
        repoCCCode.HeaderImage = Global.XpertERPFixedAssets.My.Resources.Resources.search4
        repoCCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCCCode.Width = 100
        repoCCCode.ReadOnly = False
        repoCCCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCCCode)

        repoCCDesc.FormatString = ""
        repoCCDesc.HeaderText = "Description"
        repoCCDesc.Name = colCCDesc
        repoCCDesc.Width = 150
        repoCCDesc.ReadOnly = True
        repoCCDesc.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCCDesc)

        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        repoReqQty = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "Required Quantity"
        repoReqQty.Name = colReqQty
        repoReqQty.Width = 100
        repoReqQty.Minimum = 0
        repoReqQty.IsVisible = False
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReqQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.ReadOnly = IIf(chkItem.Checked = True, False, True)
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        repoReturnQty = New GridViewDecimalColumn()
        repoReturnQty.FormatString = ""
        repoReturnQty.HeaderText = "Return Quantity"
        repoReturnQty.Name = colRetQty
        repoReturnQty.Width = 100
        repoReturnQty.Minimum = 0
        repoReturnQty.IsVisible = False
        repoReturnQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReturnQty)



        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.ReadOnly = False
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        ' Dim repoPenQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPenQty = New GridViewDecimalColumn()
        repoPenQty.FormatString = ""
        repoPenQty.HeaderText = "Pending Quantity"
        repoPenQty.Name = colPenQty
        repoPenQty.Width = 100
        repoPenQty.Minimum = 0
        repoPenQty.IsVisible = False
        repoPenQty.ReadOnly = True
        repoPenQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPenQty)

        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax1)

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt1)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)

        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode1)

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)


        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax2)

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt2)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax2)

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode2)

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2)

        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax3)

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt3)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax3)

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode3)

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3)

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt4)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax4)

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode4)

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4)

        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax5)

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt5)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax5)

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode5)

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5)

        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax6)

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt6)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable6)

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax7)

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable7)

        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax8)

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable8)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax9)

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable9)


        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax10)

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable10)

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.IsVisible = False
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        repoAmtAfterTax.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable1)

        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable2)

        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable3)

        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable4)

        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable5)

        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable6)

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable7)

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable8)

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable9)

        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable10)

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = "{0:N2}"
        repoTotTaxAmt.HeaderText = "Asset Value"
        repoTotTaxAmt.Name = colEMIAssetValue
        repoTotTaxAmt.Width = 100
        repoTotTaxAmt.ReadOnly = False
        repoTotTaxAmt.IsVisible = False
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)


        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = "{0:N0}"
        repoTotTaxAmt.HeaderText = "No Of Payment Cycle"
        repoTotTaxAmt.Name = colEMINoOfPaymentCycle
        repoTotTaxAmt.Width = 100
        repoTotTaxAmt.ReadOnly = False
        repoTotTaxAmt.IsVisible = False
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)


        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()

        gv1.Columns(colEMIAssetValue).IsVisible = IsApplyEMIOnAssetValue
        gv1.Columns(colEMINoOfPaymentCycle).IsVisible = IsApplyEMIOnAssetValue
    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetName).Value)
            frm.strLocationCode = txtFromLocation.Value
            'frm.strCurrDocNo = txtDocNo.Value
            'frm.strCurrDocType = "ISSTRAN"
            ' frm.strAgaintsDocNo = clsCommon.myCstr(txtIssueNo.Value)
            'If clsCommon.CompairString(cboDocType.SelectedValue, "Return") = CompairStringResult.Equal Then
            '    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetQty).Value)
            'Else
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            'End If
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
            End If
        End If
    End Sub
    '======================added by preeti gupta [05/01/2017]

    Sub OpenSerialItemIssueReturn()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetName).Value)
            frm.strLocationCode = txtFromLocation.Value
            'frm.strCurrDocNo = txtDocNo.Value
            'frm.strCurrDocType = "ISSTRAN"
            ' frm.strAgaintsDocNo = clsCommon.myCstr(txtIssueNo.Value)
            'If clsCommon.CompairString(cboDocType.SelectedValue, "Return") = CompairStringResult.Equal Then
            '    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetQty).Value)
            'Else
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            'End If
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
            End If
        End If
    End Sub
    '========================================================

    Sub OpenSerialItemReturn()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetName).Value)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetQty).Value)
            'frm.strItemType = Item_type
            frm.strAgaintsDocNo = clsCommon.myCstr(txtIssueNo.Value)
            frm.strCurrDocType = "MCC-AISSUE"
            frm.strLocationCode = clsCommon.myCstr(txtFromLocation.Value)
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
            End If
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colAssetCode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colReqQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colCCCode) OrElse e.Column Is gv1.Columns(colRetQty) OrElse e.Column Is gv1.Columns(colITEMCODE) Then
                        If gv1.CurrentColumn Is gv1.Columns(colQty) Then
                            Dim stockqty As Double = 0
                            Dim ActualQty As Double = 0
                            If clsCommon.myLen(txtFromLocation.Value) <> 0 And clsCommon.myLen(clsCommon.myCdbl(gv1.CurrentRow.Cells(colAssetCode).Value)) <> 0 Then
                                Dim str As String = "select sum(Item_Qty) from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + gv1.CurrentRow.Cells(colAssetCode).Value + "' and Location_Code='" + txtFromLocation.Value + "' "
                                stockqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                                Dim item As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value)
                                'If stockqty = 0 Then
                                '    common.clsCommon.MyMessageBoxShow("Stock Qty  not available at this location ")
                                '    gv1.CurrentRow.Cells(colQty).Value = 0
                                'Else
                                If clsCommon.CompairString(cboDocType.Text, "Issue") = CompairStringResult.Equal And clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal Then
                                    'str = "SELECT Requisition_Qty - (select isnull(SUM(Issued_Qty),0) from TSPL_ASSET_DISPATCH_RETAILER_HEAD inner join " &
                                    '"TSPL_ASSET_DISPATCH_RETAILER_DETAIL on TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No and " &
                                    '"TSPL_ASSET_DISPATCH_RETAILER_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Issue' and " &
                                    '"TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No <> '" & txtDocNo.Value & "' ) +   (select isnull(SUM(Issued_Qty),0) from TSPL_ASSET_DISPATCH_RETAILER_HEAD inner join " &
                                    '"TSPL_ASSET_DISPATCH_RETAILER_DETAIL on TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No and " &
                                    '"TSPL_ASSET_DISPATCH_RETAILER_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Return') FROM " &
                                    '"TSPL_REQUISITION_DETAIL"
                                    'Dim ReqQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                                    'ActualQty = ReqQty - clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                    'If ReqQty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                    '    common.clsCommon.MyMessageBoxShow("Qty more then Req qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(ReqQty) + "' ")
                                    '    gv1.CurrentRow.Cells(colQty).Value = 0
                                    'End If
                                    'If stockqty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                    '    common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(stockqty) + "' ")
                                    '    gv1.CurrentRow.Cells(colQty).Value = 0
                                    'End If
                                Else
                                    'If stockqty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                    '    common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(stockqty) + "' ")
                                    '    gv1.CurrentRow.Cells(colQty).Value = 0
                                    'End If
                                End If

                                'End If
                            Else
                                common.clsCommon.MyMessageBoxShow(Me, "Select the Location", Me.Text)
                                gv1.CurrentRow.Cells(colQty).Value = 0
                            End If
                        End If

                        'If (e.Column Is gv1.Columns(colQty)) Then
                        '    OpenSerialItem()
                        'End If

                        If e.Column Is gv1.Columns(colRetQty) Then
                            OpenSerialItemReturn()
                        End If



                        If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colReqQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colRetQty) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colAssetCode) Then
                            OpenAssetCodeList(False)
                        ElseIf e.Column Is gv1.Columns(colCCCode) Then
                            'OpenCCList(False)
                            OpenCostCenterList(False)
                        ElseIf e.Column Is gv1.Columns(colITEMCODE) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        End If
                        setGridFocus()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick)
        If clsCommon.myLen(txtFromLocation.Value) = 0 AndAlso clsCommon.CompairString(cboDocType.Text, "Return") <> CompairStringResult.Equal Then

            common.clsCommon.MyMessageBoxShow(Me, "Select from location", Me.Text)
            gv1.CurrentRow.Cells(colITEMCODE).Value = ""
            gv1.CurrentRow.Cells(colITEMNAME).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colRate).Value = 0
        Else
            ''then open finish item
            gv1.CurrentRow.Cells(colITEMCODE).Value = clsCommon.myCstr(clsItemMaster.getFinder(" tspl_Item_master.item_type NOT IN ('F','A') ", clsCommon.myCstr(gv1.CurrentRow.Cells(colITEMCODE).Value), isButtonClick))
            gv1.CurrentRow.Cells(colITEMNAME).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colITEMCODE).Value), Nothing)
            gv1.CurrentRow.Cells(colUnit).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colITEMCODE).Value), Nothing)
            setCost(gv1.CurrentRow.Index)
        End If

    End Sub

    Sub setCost(ByVal index As Integer)
        Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(gv1.Rows(index).Cells(colITEMCODE).Value) & "' "))
        If dblCostMethod <> 0 Then
            gv1.Rows(index).Cells(colRate).Value = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, clsCommon.myCstr(gv1.Rows(index).Cells(colITEMCODE).Value), txtFromLocation.Value, 1, txtDate.Value, txtDate.Value, False, Nothing, "TSPL_INVENTORY_MOVEMENT", clsCommon.myCstr(gv1.Rows(index).Cells(colUnit).Value))
        Else
            gv1.Rows(index).Cells(colRate).Value = 0
        End If
    End Sub
    Private Sub OpenCostCenterList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
            Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
            gv1.CurrentRow.Cells(colCCCode).Value = clsCommon.ShowSelectForm("TSPL_COST_CENTRE_FINANCIAL@AEFinder", qry, "Code", " Hirerachy_Level = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) + "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCCCode).Value), "", isButtonClick)
            gv1.CurrentRow.Cells(colCCDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cost_Center_Fin_Name  from TSPL_COST_CENTRE_FINANCIAL  where  Cost_Center_Fin_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCCCode).Value) + "'")) ' ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCostCenter).Value)
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select hirerachy level first.", Me.Text)
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colITEMCODE).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("IRTItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        End If
    End Sub

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            'If intCurrRow = gv1.Rows.Count - 1 Then
            '    gv1.Rows.AddNew()
            '    gv1.CurrentRow = gv1.Rows(intCurrRow)
            'End If
            If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetCode).Value) > 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colAssetCode) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                        gv1.CurrentColumn = gv1.Columns(colQty)
                    Else
                        gv1.CurrentColumn = gv1.Columns(colReqQty)
                    End If

                ElseIf gv1.CurrentColumn Is gv1.Columns(colReqQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    gv1.CurrentColumn = gv1.Columns(colAssetCode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenAssetCodeList(ByVal isButtonClick As Boolean)
        'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), "", isButtonClick)
        'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
        If clsCommon.myLen(txtFromLocation.Value) = 0 Then

            common.clsCommon.MyMessageBoxShow(Me, "Select the from location", Me.Text)
            gv1.CurrentRow.Cells(colAssetCode).Value = ""
            gv1.CurrentRow.Cells(colAssetName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colRate).Value = 0

        Else
            'Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItemwithBalanceQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), "F", isButtonClick, txtFromLocation.Value)
            'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), "A", True, isButtonClick, "", "")
            Dim strICode As String = ""
            Dim strIUnitCode As String = ""
            Dim qry As String = "select TSPL_ACQUISITION_DETAIL.Asset_Code as Code,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ACQUISITION_DETAIL.Item_code as [Item Code],TSPL_ITEM_MASTER.Item_desc as [Item Name] FROM TSPL_ACQUISITION_DETAIL LEFT OUTER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_ACQUISITION_DETAIL.item_code"

            strICode = clsCommon.ShowSelectForm("assetacq", qry, "Code", "TSPL_ACQUISITION_HEAD.Acquisition_Type='Asset'", clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value), "Code", False)

            If clsCommon.myLen(strICode) > 0 Then
                gv1.CurrentRow.Cells(colAssetCode).Value = strICode
                gv1.CurrentRow.Cells(colAssetName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Asset_Name from TSPL_ACQUISITION_DETAIL WHERE asset_code='" + strICode + "'"))
                gv1.CurrentRow.Cells(colITEMCODE).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_code from TSPL_ACQUISITION_DETAIL WHERE asset_code='" + strICode + "'"))
                If clsCommon.myLen(gv1.CurrentRow.Cells(colITEMCODE).Value) > 0 Then
                    gv1.CurrentRow.Cells(colITEMNAME).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_desc from tspl_item_master WHERE item_code='" + gv1.CurrentRow.Cells(colITEMCODE).Value + "'"))
                    gv1.CurrentRow.Cells(colUnit).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gv1.CurrentRow.Cells(colITEMCODE).Value & "' ")
                End If

                If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetCode).Value) > 0 Then
                    gv1.CurrentRow.Cells(colQty).Value = 1
                End If

                'Dim srtcost As String = "select  convert(decimal(18,2),sum(case when  Item_Qty =0 or Amount=0 then 0 else  (Amount/Item_Qty )end)) as cost  from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + obj.Item_Code + "' and location_code='" + txtFromLocation.Value + "'  "
                'Dim cost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(srtcost))
                ''COMMENTED BY PRITI
                'If cost <= 0 Then
                '    common.clsCommon.MyMessageBoxShow(" '" + obj.Item_Code + "' item don't have unit cost")
                'End If
                'gv1.CurrentRow.Cells(colRate).Value = cost
                'If PickAvgCostonAssetissue = True AndAlso clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal Then

                '    Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & strICode & "' "))
                '    If dblCostMethod <> 0 Then
                '        gv1.CurrentRow.Cells(colRate).Value = clsInventoryMovement.GetCost(dblCostMethod, strICode, txtFromLocation.Value, 1, txtDate.Value, txtDate.Value, False, Nothing, "TSPL_INVENTORY_MOVEMENT", strIUnitCode)

                '    Else
                '        gv1.CurrentRow.Cells(colRate).Value = 0
                '    End If
                'Else

                '    gv1.CurrentRow.Cells(colRate).Value = 0
                'End If

                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colRate).Value = clsAssetDispatchRetailerHead.GetAssetDepVale(txtDate.Value, txtFromLocation.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value))
                End If
                UpdateCurrentRow(gv1.CurrentRow.Index)

                UpdateAllTotals()
                gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(strICode)
            Else
                gv1.CurrentRow.Cells(colAssetCode).Value = ""
                gv1.CurrentRow.Cells(colAssetName).Value = ""
                gv1.CurrentRow.Cells(colUnit).Value = ""
                gv1.CurrentRow.Cells(colRate).Value = 0
            End If

            SetitemWiseTaxSetting(True, True)
        End If
        'Else

        'SetitemWiseTaxSetting(True, True)
        'End If
    End Sub

    'Private Sub OpenCCList(ByVal isButtonClick As Boolean)
    '    Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
    '    gv1.CurrentRow.Cells(colCCCode).Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colCCCode).Value), "", isButtonClick)
    '    gv1.CurrentRow.Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCCCode).Value)
    'End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0

        Dim dblTaxBaseAmt1 As Double = 0
        Dim dblTaxBaseAmt2 As Double = 0
        Dim dblTaxBaseAmt3 As Double = 0
        Dim dblTaxBaseAmt4 As Double = 0
        Dim dblTaxBaseAmt5 As Double = 0
        Dim dblTaxBaseAmt6 As Double = 0
        Dim dblTaxBaseAmt7 As Double = 0
        Dim dblTaxBaseAmt8 As Double = 0
        Dim dblTaxBaseAmt9 As Double = 0
        Dim dblTaxBaseAmt10 As Double = 0

        Dim dblTaxAmt1 As Double = 0
        Dim dblTaxAmt2 As Double = 0
        Dim dblTaxAmt3 As Double = 0
        Dim dblTaxAmt4 As Double = 0
        Dim dblTaxAmt5 As Double = 0
        Dim dblTaxAmt6 As Double = 0
        Dim dblTaxAmt7 As Double = 0
        Dim dblTaxAmt8 As Double = 0
        Dim dblTaxAmt9 As Double = 0
        Dim dblTaxAmt10 As Double = 0



        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colAssetCode).Value) > 0) Or chkItem.Checked Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt10).Value)

                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt1).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt2).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt3).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt4).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt5).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt6).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt7).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt8).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt9).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt10).Value)

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
            End If
        Next

        For ii As Integer = 1 To gv2.Rows.Count
            Select Case (ii)
                Case 1
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                    If dblTaxBaseAmt1 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 2
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                    If dblTaxBaseAmt2 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 3
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                    If dblTaxBaseAmt3 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 4
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                    If dblTaxBaseAmt4 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 5
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                    If dblTaxBaseAmt5 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 6
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                    If dblTaxBaseAmt6 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 7
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                    If dblTaxBaseAmt7 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 8
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                    If dblTaxBaseAmt8 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 9
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                    If dblTaxBaseAmt9 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 10
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                    If dblTaxBaseAmt10 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
            End Select
        Next
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblDocAmount.Text = lblTotRAmt.Text
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        'lblReq2.Visible = False
        'lblReq3.Visible = False
        BlankAllControls()
        LoadBlankGrid()
        LoadBlankGridTax()
        isNewEntry = True
        chkItem.Checked = False
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        chkLost.Enabled = False
        txtDate.Focus()
        'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
        gv1.Rows.AddNew()
        'End If
        'chkWithoutRefNo.Checked = False
        'chkWithoutRefNo.Enabled = False
        'chkWithoutRefNoChanged()
        LblLoc.Text = "From Location"
        txtIssueNo.Enabled = False
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Function AllowToSave() As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            '=======================================================
            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                    Return False
                End If
            End If

            UpdateAllTotals()





            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter From Location", Me.Text)
                txtFromLocation.Focus()
                Return False
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Return") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                '    common.clsCommon.MyMessageBoxShow("Please Enter To Location")
                '    txtFromLocation.Focus()
                '    Return False
            End If

            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Return") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtIssueNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter Issue No", Me.Text)
                txtIssueNo.Focus()
                Return False

            End If


            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Document No Not found to save", Me.Text)
                txtDocNo.Focus()
                Return False
            End If
            If clsCommon.myLen(cboDocType.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Document Type", Me.Text)
                cboDocType.Focus()
                Return False
            End If
            If clsCommon.myLen(txtIssueTo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Issue To", Me.Text)
                txtIssueTo.Focus()
                Return False
            End If

            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Route No", Me.Text)
                txtRouteNo.Focus()
                Return False
            End If

            '-----------Added By ---Pankaj Kumar---on-----04/05/2012--For Inserting Vehicle Mannually----------

            'If clsCommon.myLen(TxtVehicle.Value) <= 0 Then
            '    'common.clsCommon.MyMessageBoxShow("Please select Cost Center value")
            '    'TxtVehicle.Focus()
            '    'Return False
            'Else
            '    If clsCommon.myLen(TxtVehicle.Value) > 0 Then
            '        Dim count As Decimal = 0
            '        Dim segno As String = String.Empty
            '        Dim strvehiclenum As String = clsCommon.myCstr(TxtVehicle.Value)
            '        Dim Sql As String = "select Description from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(TxtVehicle.Value) + "' or Description = '" + Convert.ToString(TxtVehicle.Value) + "'"
            '        If Not String.IsNullOrEmpty(connectSql.RunScalar(Sql)) Then

            '        Else
            '            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            '            Dim strVehicalNo As String = clsCommon.myCstr(TxtVehicle.Value)
            '            strmessage += "Do you want to continue "
            '            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            '                count = connectSql.RunScalar("select COUNT(*) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'")
            '                TxtVehicle.Value = Convert.ToString(count + 1) + "-Man"
            '                Sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
            '                segno = CStr(connectSql.RunScalar(Sql))
            '                connectSql.RunSpTransaction("sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", clsCommon.myCstr(TxtVehicle.Value)), New SqlParameter("@desc", strVehicalNo), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
            '                connectSql.RunSpTransaction("SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", clsCommon.myCstr(TxtVehicle.Value)), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", ""), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
            '                lblVehicleDesc.Text = strvehiclenum
            '            Else
            '                TxtVehicle.Value = String.Empty
            '                Return False
            '            End If
            '        End If
            '    End If
            'End If
            '---------------------------------------Code Ends Here-------------------------------------------

            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Transfer") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gv1.Rows.Count - 1

                    Dim Icode As String = gv1.Rows(ii).Cells(colAssetCode).Value
                    Dim CCCode As String = gv1.Rows(ii).Cells(colCCCode).Value
                    For jj As Integer = 0 To ii - 1
                        Dim Icode1 As String = gv1.Rows(jj).Cells(colAssetCode).Value
                        Dim CCCode1 As String = gv1.Rows(jj).Cells(colCCCode).Value
                        If clsCommon.myLen(Icode) > 0 Then
                            If clsCommon.CompairString(Icode + CCCode, Icode1 + CCCode1) = CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "'Item' And 'Cost Code' are repeated on line '" + clsCommon.myCstr(jj + 1) + "'AND '" + clsCommon.myCstr(ii + 1) + "'")
                                Return False
                            End If
                        End If
                    Next
                Next
            Else
                For ii As Integer = 0 To gv1.Rows.Count - 1

                    Dim Icode As String = gv1.Rows(ii).Cells(colAssetCode).Value
                    Dim CCCode As String = gv1.Rows(ii).Cells(colCCCode).Value
                    Dim itemL As Integer = clsCommon.myLen(gv1.Rows(ii).Cells(colAssetCode).Value)
                    Dim qq As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    Dim Reqqty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colReqQty).Value)
                    If itemL <= 0 Then
                    Else
                        If qq = 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Qty can not be zero for '" + Icode + "' Item ")
                            Return False
                        End If

                    End If
                    For jj As Integer = 0 To ii - 1
                        Dim Icode1 As String = gv1.Rows(jj).Cells(colAssetCode).Value
                        Dim CCCode1 As String = gv1.Rows(jj).Cells(colCCCode).Value
                        If clsCommon.myLen(Icode) > 0 Then
                            If clsCommon.CompairString(Icode + CCCode, Icode1 + CCCode1) = CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "'Item' And 'Cost Code' are repeated on line '" + clsCommon.myCstr(jj + 1) + "'AND '" + clsCommon.myCstr(ii + 1) + "'")
                                Return False
                            End If
                        End If
                    Next
                Next
            End If




            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetCode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetName).Value)
                Dim dblQty As Double = 0 'clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal Then
                    dblQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Else
                    dblQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value)
                End If
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)


                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please enter UOM of Item - " + strICode + ".At Line No:   " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                    If clsCommon.CompairString(cboDocType.Text, "Return") = CompairStringResult.Equal Then
                        Dim ReturnQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value)
                        Dim strItemName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetName).Value)
                        If ReturnQty < 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Return qty can not be negative for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                        If ReturnQty > dblQty Then
                            'common.clsCommon.MyMessageBoxShow("Return qty can not be greater than from issued qty for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            common.clsCommon.MyMessageBoxShow(Me, "Item " + strICode + "( " + strItemName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(ReturnQty) + ") Can't be more than Pending Quantity(" + clsCommon.myCstr(dblQty) + ") at line no: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If
                    Else
                        ''For RM Other balance Qty check And works only for one unit.
                        Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                        Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtFromLocation.Value, txtDocNo.Value, clsCommon.GETSERVERDATE(), Nothing, strUOM)
                        Dim dblEnteredQty As Double = dblQty
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            If ii = jj Then
                                Continue For
                            End If
                            Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colAssetCode).Value)
                            Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                            Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                            Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                            If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                                dblEnteredQty += dblQtyInner
                            End If
                        Next
                        dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                        'If dblEnteredQty > dblBalQty Then
                        '    common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
                        '    Return False
                        'End If
                    End If
                    '=======================Added by preeti Gupta[05/01/2017]================================
                    If clsCommon.CompairString(cboDocType.Text, "Issue") = CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                            Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                            If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                                common.clsCommon.MyMessageBoxShow(Me, "Please provice serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                                Return False
                            End If
                        End If
                    End If




                End If
            Next


            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Return") = CompairStringResult.Equal Then
                If chkLost.Checked = True Then
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetCode).Value)) > 0 Then
                            Dim dblNoofInstallment As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colEMINoOfPaymentCycle).Value)
                            If dblNoofInstallment <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Please Enter No Of Installment on line '" + clsCommon.myCstr(ii + 1) + "' ")
                                Return False
                            End If
                        End If

                    Next
                End If
            End If

            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekPostBTn As Boolean)
        Try
            If ChekPostBTn = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Asset Dispatch Retailer", txtDate.Value) = False Then
                    Exit Sub
                End If
            End If
            ''
            If (AllowToSave()) Then
                Dim obj As New clsAssetDispatchRetailerHead()
                obj.Doc_No = txtDocNo.Value
                obj.Doc_Date = txtDate.Value
                obj.Doc_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                obj.Issue_To = txtIssueTo.Value
                obj.Request_By = txtRequestBy.Value
                obj.Remarks = txtRemarks.Text
                obj.Comment = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.IS_LOST = chkLost.Checked
                obj.From_Location = txtFromLocation.Value
                obj.To_Location = txtFromLocation.Value
                obj.Distributor_Code = TxtDistributor.Value
                obj.Route_No = txtRouteNo.Value
                obj.Alternate_Vehicle_Id = clsCommon.myCstr(txtAltVehicle.Value)
                obj.IsItemWise = chkItem.Checked
                'obj.Req_IssueNo = fndReqNo.Value
                'obj.RequisitionNo = lblReq3.Text
                'obj.Dept = txtDepartment.Value
                'obj.Dept_Desc = lblDepartment.Text
                If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    obj.Issue_No = clsCommon.myCstr(txtIssueNo.Value)
                Else
                    txtIssueNo.Value = ""
                End If
                obj.Tax_Group = txtTaxGroup.Value
                obj.Tax_Desc = lblTaxGrpName.Text

                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                    obj.TAX6_Base_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                    obj.TAX7_Base_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                    obj.TAX8_Base_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                    obj.TAX9_Base_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                    obj.TAX10_Base_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
                End If
                obj.BeforeTax_Amt = lblAmtAfterDiscount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text
                obj.doc_Amt = lblTotRAmt.Text
                obj.Vehicle_Id = clsCommon.myCstr(TxtVehicle.Value)
                'obj.Machine_Id = clsCommon.myCstr(TxtMachinery.Value)

                obj.Arr = New List(Of clsAssetDispatchRetailerDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsAssetDispatchRetailerDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Req_IssueNo = clsCommon.myCstr(grow.Cells(colReq_IssueNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colITEMCODE).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colITEMNAME).Value)
                    objTr.Cost_Code = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                    objTr.Required_Qty = clsCommon.myCdbl(grow.Cells(colReqQty).Value)

                    objTr.AssetCode = clsCommon.myCstr(grow.Cells(colAssetCode).Value)
                    objTr.DepositType = clsCommon.myCstr(grow.Cells(colDepositType).Value)
                    objTr.DepositReceiptNo = clsCommon.myCstr(grow.Cells(colDepositRecNo).Value)
                    objTr.DepositValue = clsCommon.myCdbl(grow.Cells(colDepositValue).Value)
                    objTr.Capacity = clsCommon.myCdbl(grow.Cells(colCapacity).Value)
                    objTr.PurchaseInvoiceNo = clsCommon.myCstr(grow.Cells(colPINumber).Value)
                    If grow.Cells(colPIDate).Value IsNot Nothing Then
                        objTr.PurchaseInvoiceDate = clsCommon.myCDate(grow.Cells(colPIDate).Value)
                    End If

                    objTr.BrandName = clsCommon.myCstr(grow.Cells(colBrandName).Value)
                    objTr.SerialNo = clsCommon.myCstr(grow.Cells(colSerialNo).Value)
                    objTr.AssetID = clsCommon.myCstr(grow.Cells(colASSETID).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)

                    'If clsCommon.CompairString(cboDocType.Text, "Return") = CompairStringResult.Equal Then
                    '    'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                    '    objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                    '    objTr.Issued_Qty_AgainstRet = 0
                    '    'Else
                    '    '    objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                    '    '    objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    '    'End If
                    'Else
                    objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                    'End If
                    objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                    objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                    objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                    objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                    objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                    objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                    objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                    objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                    objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                    objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                    objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                    objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                    objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                    objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                    objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                    objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                    objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                    objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                    objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                    objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                    objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                    objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                    objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                    objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                    objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                    objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                    objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                    objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                    objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                    objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)

                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)

                    objTr.EMI_Asset_Value = clsCommon.myCdbl(grow.Cells(colEMIAssetValue).Value)
                    objTr.EMI_No_Of_Payment_Cycle = clsCommon.myCdbl(grow.Cells(colEMINoOfPaymentCycle).Value)


                    objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                    If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                        If clsCommon.myLen(objTr.Item_Code) > 0 Then
                            obj.Arr.Add(objTr)
                        End If
                    ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                        If clsCommon.myLen(objTr.Item_Code) > 0 AndAlso clsCommon.myCdbl(objTr.Issued_Qty_AgainstRet) > 0 Then
                            obj.Arr.Add(objTr)
                        End If
                    End If

                Next
                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please Fill at least one Item", Me.Text)
                        Return
                    End If
                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please return qty for at least one item", Me.Text)
                        Return
                    End If
                End If


                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colAssetCode)
                End If
                ''End of For Custom Fields

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Doc_No)
                    If ChekPostBTn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.Doc_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True

            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()

            Dim obj As New clsAssetDispatchRetailerHead()
            obj = clsAssetDispatchRetailerHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                UsLock1.Status = obj.Status
                isNewEntry = False
                btnSave.Text = "Update"
                txtDocNo.Value = obj.Doc_No
                txtDate.Value = obj.Doc_Date

                txtIssueTo.Value = obj.Issue_To
                If clsCommon.myLen(txtIssueTo.Value) > 0 Then
                    lblIssueTo.Text = obj.Issue_ToName
                Else
                    lblIssueTo.Text = ""
                End If
                TxtDistributor.Value = obj.Distributor_Code
                lblDistributor.Text = clsCommon.myCstr(clsCustomerMaster.GetName(obj.Distributor_Code, Nothing))
                txtRequestBy.Value = obj.Request_By
                lblRequestBy.Text = obj.Request_ByName
                chkOnHold.Checked = obj.On_Hold
                chkLost.Checked = obj.IS_LOST
                chkItem.Checked = obj.IsItemWise
                txtComment.Text = obj.Comment
                txtRemarks.Text = obj.Remarks
                cboDocType.Enabled = False
                cboDocType.SelectedValue = obj.Doc_Type
                If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                    txtIssueNo.Value = clsCommon.myCstr(obj.Issue_No)
                    txtIssueNo.Enabled = True
                    txtIssueTo.Enabled = False
                    If chkItem.Checked Then
                        chkItem.Enabled = False
                    Else
                        chkItem.Enabled = True
                    End If
                Else
                    txtIssueNo.Value = ""
                    txtIssueNo.Enabled = False
                    txtIssueTo.Enabled = True
                End If
                txtFromLocation.Value = obj.From_Location
                lblFromLocation.Text = obj.From_LocationName
                txtToLocation.Value = obj.From_Location
                lblToLocation.Text = obj.From_LocationName
                'fndReqNo.Value = obj.Req_IssueNo
                'lblReq3.Text = obj.RequisitionNo
                lblDocAmount.Text = obj.doc_Amt
                'If clsCommon.myLen(fndReqNo.Value) > 0 Then
                '    LoadReqDataHead(fndReqNo.Value)
                '    gv1.Rows.Clear()
                'End If
                'txtDepartment.Value = obj.Dept
                'lblDepartment.Text = obj.Dept_Desc

                txtTaxGroup.Value = obj.Tax_Group
                lblTaxGrpName.Text = obj.Tax_Desc
                lblAmtAfterDiscount.Text = obj.BeforeTax_Amt
                lblTaxAmt.Text = obj.Total_Tax_Amt
                lblTotRAmt.Text = obj.doc_Amt
                lblDocAmount.Text = lblTotRAmt.Text

                txtRouteNo.Value = obj.Route_No
                fndRouteNo_TextChanged()

                TxtVehicle.Value = obj.Vehicle_Id
                lblVehicleDesc.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(TxtVehicle.Value) + "'")
                txtAltVehicle.Value = obj.Alternate_Vehicle_Id
                lblAltVehicleDesc.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtAltVehicle.Value) + "'")

                'TxtMachinery.Value = obj.Machine_Id
                'lblMachineDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where Seg_No= '5' AND Segment_Code= '" + TxtMachinery.Value + "'")


                'added by priti
                'fndReqNo.Enabled = False
                txtFromLocation.Enabled = False
                'added by priti on 25/07/2013  to allow  for wrong entry
                'txtToLocation.Enabled = False
                cboDocType.Enabled = False
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal Then
                    'lblReq2.Visible = False
                    'lblReq3.Visible = False
                    'RadLabel9.Visible = False
                    'txtToLocation.Visible = False
                    'lblToLocation.Visible = False
                    LblLoc.Visible = True
                    txtFromLocation.Visible = True
                    lblFromLocation.Visible = True
                    'lblReq.Visible = True
                    'fndReqNo.Visible = True
                    'lblReqDate.Visible = True
                    'lblReq.Text = "Requisition No"
                    repoReqIssueNo.HeaderText = "Requisition No"
                    'chkWithoutRefNo.Enabled = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Return") = CompairStringResult.Equal Then
                    'lblReq2.Visible = True
                    'lblReq3.Visible = True
                    'lblReq.Visible = True
                    'fndReqNo.Visible = True
                    'lblReqDate.Visible = True
                    'RadLabel9.Visible = True
                    'txtToLocation.Visible = True
                    'lblToLocation.Visible = True
                    LblLoc.Visible = True
                    txtFromLocation.Visible = True
                    lblFromLocation.Visible = True
                    ' lblIssueTo.Text = "Issue No"
                    repoReqIssueNo.HeaderText = "Issue No"
                    If clsCommon.myLen(obj.Req_IssueNo) <= 0 Then
                        'chkWithoutRefNo.Checked = True
                        'chkWithoutRefNo.Enabled = True
                        'chkWithoutRefNoChanged()
                    End If
                ElseIf cboDocType.Text = "Transfer" Then
                    'lblReq2.Visible = False
                    'lblReq3.Visible = False
                    'lblReq.Visible = False
                    'fndReqNo.Visible = False
                    'lblReqDate.Visible = False
                    'chkWithoutRefNo.Enabled = False
                End If
                ' ended by priti

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX1_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX2_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX3_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX4_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX5_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX6_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX6) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX7_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX7) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX8_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX8) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX9_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX9) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX10_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX10) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If



                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsAssetDispatchRetailerDetail In obj.Arr
                        If gv1.Rows.Count <= 0 Then
                            gv1.Rows.AddNew()
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = objTr.Req_IssueNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = objTr.AssetCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Asset_Name from TSPL_ACQUISITION_DETAIL WHERE asset_code='" + objTr.AssetCode + "'"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colITEMCODE).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colITEMNAME).Value = objTr.Item_Desc

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colASSETID).Value = objTr.AssetID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBrandName).Value = objTr.BrandName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositRecNo).Value = objTr.DepositReceiptNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSerialNo).Value = objTr.SerialNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPINumber).Value = objTr.PurchaseInvoiceNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = objTr.Capacity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositValue).Value = objTr.DepositValue
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositType).Value = objTr.DepositType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPIDate).Value = objTr.PurchaseInvoiceDate


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = objTr.Cost_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(objTr.Cost_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = objTr.Required_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Unit_Cost

                        'If clsCommon.CompairString(cboDocType.Text, "Return") = CompairStringResult.Equal Then
                        '    'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty
                        '    'Else
                        '    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Issued_Qty_AgainstRet
                        '    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty
                        '    'End If
                        'Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Issued_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty_AgainstRet
                        'End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objTr.TAX1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt1).Value = objTr.TAX1_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objTr.TAX2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt2).Value = objTr.TAX2_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objTr.TAX3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt3).Value = objTr.TAX3_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objTr.TAX4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt4).Value = objTr.TAX4_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objTr.TAX5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt5).Value = objTr.TAX5_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objTr.TAX6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt6).Value = objTr.TAX6_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objTr.TAX7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt7).Value = objTr.TAX7_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objTr.TAX8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt8).Value = objTr.TAX8_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objTr.TAX9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt9).Value = objTr.TAX9_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objTr.TAX10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt10).Value = objTr.TAX10_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Total_Tax_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objTr.Item_Net_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEMIAssetValue).Value = objTr.EMI_Asset_Value
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEMINoOfPaymentCycle).Value = objTr.EMI_No_Of_Payment_Cycle

                        If clsCommon.CompairString(clsCommon.myCstr(obj.Doc_Type), "Return") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = ReturnBalQty(clsCommon.myCstr(obj.Doc_No), clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(obj.Issue_No))
                        End If
                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                    End If
                End If


                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Doc_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Doc_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Doc_No)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                '' Anubhooti 09-Sep-2014 BM00000003735
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Issue/Return/Transfer", txtDate.Value) = False Then
                    Exit Sub
                End If
                ''
                If (clsAssetDispatchRetailerHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    ''If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    ''    print()
                    ''End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsAssetDispatchRetailerHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)

    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Doc_No as Code,Doc_Date as Date,from_location as [Mcc Code],Mcc_Name as [Mcc Name],issue_to as [Vsp Code]" _
        & " ,vendor_name as [VSP Name],Doc_Type as Type,case when TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] " _
        & " from TSPL_ASSET_DISPATCH_RETAILER_HEAD left join tspl_mcc_master on tspl_mcc_master.mcc_code=from_location left join tspl_vendor_master on vendor_code=issue_to"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        LoadData(clsCommon.ShowSelectForm("IRTCodeFilter", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub frmAssetDispatchRetailer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colAssetCode) Then
                gv1.CurrentColumn = gv1.Columns(colAssetName)
                OpenAssetCodeList(True)
                gv1.CurrentColumn = gv1.Columns(colAssetCode)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colEMIAssetValue) Then
                    gv1.CurrentRow.Cells(colEMIAssetValue).ReadOnly = Not IsApplyEMIOnAssetValue
                ElseIf e.Column Is gv1.Columns(colEMINoOfPaymentCycle) Then
                    gv1.CurrentRow.Cells(colEMINoOfPaymentCycle).ReadOnly = Not IsApplyEMIOnAssetValue
                End If

                'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    gv1.CurrentRow.Cells(colAssetCode).ReadOnly = True
                    gv1.CurrentRow.Cells(colCCCode).ReadOnly = True
                    gv1.CurrentRow.Cells(colCCDesc).ReadOnly = True
                    gv1.CurrentRow.Cells(colReqQty).ReadOnly = True
                    gv1.CurrentRow.Cells(colQty).ReadOnly = True
                    'gv1.CurrentRow.Cells(colEMINoOfPaymentCycle).ReadOnly = True
                    repoReturnQty.IsVisible = True
                    repoQty.HeaderText = "Pending Quantity"
                    repoPenQty.IsVisible = False
                Else
                    gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    gv1.CurrentRow.Cells(colAssetCode).ReadOnly = False
                    gv1.CurrentRow.Cells(colCCCode).ReadOnly = False
                    gv1.CurrentRow.Cells(colCCDesc).ReadOnly = False
                    gv1.CurrentRow.Cells(colReqQty).ReadOnly = False
                    gv1.CurrentRow.Cells(colQty).ReadOnly = False
                    repoReturnQty.IsVisible = False
                    repoQty.HeaderText = "Issue Quantity"
                    repoPenQty.IsVisible = False
                End If
                'Else
                '    gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                'End If

                'If clsCommon.myLen(fndReqNo.Value) <= 0 Then

                '    gv1.CurrentRow.Cells(colAssetCode).ReadOnly = True
                'End If

            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Public Sub Print()
        Try
            '            Dim qry As String = "SELECT     TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Remarks, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Comment, " & _
            '                     " case when  TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Posting_Date, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code,  " & _
            '                      " TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Desc, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Required_Qty, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty,  " & _
            '                      " TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,  " & _
            '                      " TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,  " & _
            '                      " loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy  " & _
            '    " FROM  TSPL_ASSET_DISPATCH_RETAILER_HEAD INNER JOIN TSPL_ASSET_DISPATCH_RETAILER_DETAIL ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No  " & _
            '" LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_To = emp1.EMP_CODE  " & _
            '"  LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By = emp2.EMP_CODE  " & _
            '"   LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location = loc1.Location_Code  " & _
            ' "   LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location = loc2.Location_Code  " & _
            ' "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code  " & _
            '"  where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No='" + txtDocNo.Value + "'"
            ''------------------------------ Changes by shipra on 24/06/13--------------------------------------------''

            ''Dim qry As String = " SELECT     TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Remarks, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Comment,  case when  TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Posting_Date, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code,   TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Desc, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Required_Qty, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty as returnqty,   TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,   TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy,(select xxxx.Issued_Qty  from TSPL_ASSET_DISPATCH_RETAILER_DETAIL  xxxx where xxxx.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Req_IssueNo and xxxx.Item_Code=TSPL_ASSET_DISPATCH_RETAILER_DETAIL .Item_Code  )as [Issued_Qty]     FROM  TSPL_ASSET_DISPATCH_RETAILER_HEAD "
            ''qry += " INNER JOIN TSPL_ASSET_DISPATCH_RETAILER_DETAIL ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No"
            ''qry += " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code"
            ''qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD .Issue_To = emp1.EMP_CODE  "
            ''qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By = emp2.EMP_CODE    "
            ''qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location = loc1.Location_Code"
            ''qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location = loc2.Location_Code    "
            ''qry += " where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No='" + txtDocNo.Value + "'"

            ''Dim qry As String = "     SELECT  TSPL_CostCenter_MASTER.cost_name,  TSPL_ASSET_DISPATCH_RETAILER_HEAD.Created_By ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Modify_By ,   TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Remarks, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Comment,  case when  TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Posting_Date, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code,   TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Desc, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Required_Qty, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty_AgainstRet as returnqty,   TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,   TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy,"
            ' ''qry += " --(select xxxx.Issued_Qty  from TSPL_ASSET_DISPATCH_RETAILER_DETAIL  xxxx where xxxx.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Req_IssueNo and xxxx.Item_Code=TSPL_ASSET_DISPATCH_RETAILER_DETAIL .Item_Code  )"
            ''qry += " TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty as [Issued_Qty]     FROM  TSPL_ASSET_DISPATCH_RETAILER_HEAD  INNER JOIN TSPL_ASSET_DISPATCH_RETAILER_DETAIL ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD .Issue_To = emp1.EMP_CODE   LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By = emp2.EMP_CODE     LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location = loc1.Location_Code LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location = loc2.Location_Code              LEFT OUTER JOIN  TSPL_CostCenter_MASTER   ON TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Cost_Code  = TSPL_CostCenter_MASTER.Cost_Code  "
            ''qry += " where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No='" + txtDocNo.Value + "'"



            ''------------------------------ Changes by shipra on 24/06/13--------------------------------------------''

            ''Dim demo As String
            ''If demo = "" Then
            ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            ''    demo = dt.Rows(0)("Doc_type").ToString


            ''End If


            '            Dim type As String = "select Doc_type from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No='" + txtDocNo.Value + "'"
            '            Dim val As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(type))
            '            'Dim dr As SqlDataReader
            '            'dr = connectSql.RunSqlReturnDR(type)
            '            'While dr.Read()
            '            '    val = dr(0).ToString
            '            'End While

            '            If val = "Issue" Then
            '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            '                PurchaseOrderViewer.funreport(dt, "rptissueNewV", "Issur/Return/Transfer")
            '                'PurchaseOrderViewer.funreport(dt, "rptissue", "Issur/Return/Transfer")
            '            ElseIf val = "Return" Then
            '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            '                PurchaseOrderViewer.funreport(dt, "rptreturnNewV", "Issur/Return/Transfer")

            '                ' PurchaseOrderViewer.funreport(dt, "rptreturn", "Issur/Return/Transfer")

            '            ElseIf val = "Transfer" Then
            '                ''''---------------------Added By ----Pankaj Kumar----on 04/03/2012------------------------
            '                Dim QryTrnsfr As String = "select TSPL_ASSET_DISPATCH_RETAILER_HEAD.Created_By,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Modify_By, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, TSPL_COMPANY_MASTER.logo_img, TSPL_COMPANY_MASTER.logo_img2, TSPL_COMPANY_MASTER.Comp_Name  as CompanyName, " & _
            '    " TSPL_COMPANY_MASTER.Tin_No as CompanyTin,Case when len(TSPL_COMPANY_MASTER.Add1)>0 then TSPL_COMPANY_MASTER.Add1 else '' end +case when len(TSPL_COMPANY_MASTER.Add2)>0 then ','  else  '' end  +case when len(TSPL_COMPANY_MASTER.Add2)>0 then TSPL_COMPANY_MASTER.Add2  else  '' end + case when len(TSPL_COMPANY_MASTER.Add3)>0 then ','  else  '' end +case when len(TSPL_COMPANY_MASTER.Add3)>0 then TSPL_COMPANY_MASTER.Add3  else  '' end as CompanyAddress, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type, " & _
            '    " (select Case when (len(TSPL_LOCATION_MASTER .Add1)>0) then convert(varchar(20),TSPL_LOCATION_MASTER.Add1,103) else '' end + " & _
            '" case when (len(TSPL_LOCATION_MASTER.Add2)>0) then ', '+ convert(varchar(20),TSPL_LOCATION_MASTER.Add2,103)  else  '' end + " & _
            '" case when (len(TSPL_LOCATION_MASTER.Add3)>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.Add3,103)  else  '' end + " & _
            '" case when (len(TSPL_LOCATION_MASTER.Add4)>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.Add4,103)  else  '' end + " & _
            '" case when (len(TSPL_LOCATION_MASTER.City_Code )>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.City_Code,103) else  ''  end + " & _
            '" case when (len(TSPL_LOCATION_MASTER.State )>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.State,103)  else  ''  end + " & _
            '" case when (len(TSPL_LOCATION_MASTER.Pin_Code )>0) then ', '+convert(varchar(10),TSPL_LOCATION_MASTER.Pin_Code,103)  else  ''  end + " & _
            '" case when (len(TSPL_LOCATION_MASTER.Country )>0) then ', '+TSPL_LOCATION_MASTER.Country  else  ''  end  from TSPL_LOCATION_MASTER where location_Code='L001' ) as Address, " & _
            '    " (select Location_Desc from TSPL_LOCATION_MASTER where location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location) as ToLocDesc, " & _
            '    " (select Loc_Segment_Code from TSPL_LOCATION_MASTER where location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location) as ToLocSegCode, " & _
            '    " (select TIN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location) as TinNo, " & _
            '    " (select TCAN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location) as CstNo, " & _
            '    " (select TIN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location) as CompanyTin, " & _
            '    " '' as NRG_No, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code AS ItemCode, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Desc AS Desciption, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty AS Quantity, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_code AS Uom, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_Cost AS Rate, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Amount AS Amount, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX1 AS TaxRateDesc1, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX1_Amt as TaxRate1, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX2 as TaxRateDesc2, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX2_Amt as TaxRate2, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX3 as TaxRateDesc3, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX3_Amt as TaxRate3, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX4 as TaxRateDesc4, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX4_Amt as TaxRate4, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX5 as TaxRateDesc5, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX5_Amt as TaxRate5, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX6 as TaxRateDesc6, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX6_Amt as TaxRate6, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX7 as TaxRateDesc7, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX7_Amt as TaxRate7, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX8 as TaxRateDesc8, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX8_Amt as TaxRate8, TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX9 as TaxRateDesc9, " & _
            '    " TSPL_ASSET_DISPATCH_RETAILER_HEAD.TAX9_Amt as TaxRate9, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX10 as TaxRateDesc10, TSPL_ASSET_DISPATCH_RETAILER_DETAIL.TAX10_Amt as  TaxRate10 " & _
            '    " FROM TSPL_ASSET_DISPATCH_RETAILER_HEAD " & _
            '    " INNER JOIN TSPL_ASSET_DISPATCH_RETAILER_DETAIL ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No " & _
            '    " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_To = emp1.EMP_CODE " & _
            '    " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By = emp2.EMP_CODE " & _
            '    " LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location = loc1.Location_Code " & _
            '    " LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location = loc2.Location_Code " & _
            '    " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code " & _
            '    " where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No='" + txtDocNo.Value + "' and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type='" + val + "' "
            '                ''''--------------------------------------------------Code Ends Here--------------------------------------------------
            '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(QryTrnsfr)
            '                PurchaseOrderViewer.funreport(dt, "crptscrapTransfer", "Issur/Return/Transfer")

            '            End If

            '===============Added by Shivani Tyagi=========
            Dim Qry As String = "Select TSPL_ASSET_DISPATCH_RETAILER_DETAIL.serialNo,  tspl_location_master.add1 +case when len(tspl_location_master.add2)>0 then ', '+tspl_location_master.add2 else '' end +case when LEN(isnull(tspl_location_master.Add3,''))>0  then ', '+isnull(tspl_location_master.Add3,'') else ' ' end  + case when len(state_forlocation.State_name )>0 then state_forlocation.state_name else '' end  as Loc_address, TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No  ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location,TSPL_LOCATION_MASTER.Location_Desc , TSPL_ASSET_DISPATCH_RETAILER_HEAD.Created_By ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Modify_By ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Email AS Comp_Email, case when ISNULL(TSPL_COMPANY_MASTER .Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as  Comp_Phn, TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 "
            Qry += " then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as Comp_address,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_COMPANY_MASTER.Tin_No as Comp_Tin_No,TSPL_COMPANY_MASTER.CST_LST as Comp_CSt_LST,tspl_company_master.CINNo as Comp_CINNo,tspl_company_master.Pan_No  as Comp_PAN_No,tspl_company_master.GSTReg_No as Comp_gst_no,tspl_company_master.Access_Officer as Comp_FSSAI
            , tspl_customer_master .add1 +case when len(tspl_customer_master.add2)>0 then ', '+tspl_customer_master.add2 else '' end +case when LEN(isnull(tspl_customer_master.Add3,''))>0 then ', '+isnull(tspl_customer_master.Add3,'') else ' ' end  + case when len(tspl_customer_master.State )>0 then tspl_customer_master.State else '' end  as Cust_address,tspl_customer_master.cust_Code  ,tspl_customer_master.customer_name as cust_Name  ,tspl_customer_master.CST as Cust_CST_No,tspl_customer_master.TIN_No as Cust_Tin_no, case when ISNULL(tspl_customer_master .Phone1,'')='(+__)__________' then '' else tspl_customer_master.Phone1 end +  Case When   ISNULL(tspl_customer_master.Phone2,'')<>'(+__)__________' Then ', '+ tspl_customer_master.Phone2 Else'' End as  Cust_Phn, tspl_customer_master.Email as Cust_Email,tspl_customer_master.pin_no as Cust_pin_code            
            ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No ,convert(varchar,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,103) as Doc_Date ,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_code ,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Amount ,case when TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty_againstret > 0  then TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Issued_Qty_againstret  else Issued_Qty end  as  Issued_Qty,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_Cost as Rate ,  TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Total_Tax_Amt ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Amt , TAX1 .Tax_Code_Desc as tax1name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax1_amt,0) as txt1amt,"
            Qry += "  tax2.Tax_Code_Desc as tax2name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax2_amt,0) as txt2amt,  tax3.Tax_Code_Desc as tax3name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax4_amt,0) as txt4amt,  tax5.Tax_Code_Desc as tax5name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax5_amt,0) as txt5amt, tax6.Tax_Code_Desc as tax6name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax7_amt,0) as txt7amt,  tax8.Tax_Code_Desc as tax8name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax8_amt,0) as txt8amt,   tax9.Tax_Code_Desc as tax9name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax9_amt,0) as txt9amt,  tax10.Tax_Code_Desc as tax10name,isnull (TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax10_amt,0) as txt10amt 
            ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_code,tspl_customer_master.customer_name as Distributor_name
            ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_To as Retailer_code
            ,TSPL_SECONDARY_CUSTOMER_MASTER.customer_name as Retailer_name
            ,case when isnull(TSPL_ACQUISITION_DETAIL.Asset_Name,'')<>'' then TSPL_ACQUISITION_DETAIL.Asset_Name else TSPL_ITEM_MASTER.Item_Desc end as Asset_Name
            ,case when isnull(TSPL_ASSET_DISPATCH_RETAILER_detail.AssetCode,'')<>'' then TSPL_ASSET_DISPATCH_RETAILER_detail.AssetCode else TSPL_ITEM_MASTER.Item_Code end as AssetCode
            ,TSPL_ASSET_DISPATCH_RETAILER_detail.AssetID,TSPL_ZONE_MASTER.Description AS zone_name
            ,TSPL_ROUTE_MASTER.Route_Desc,coalesce(AltVehicle.Description,TSPL_VEHICLE_MASTER.Description) as Vehicle_desc,TSPL_EMPLOYEE_MASTER.emp_name as Request_by
            from TSPL_ASSET_DISPATCH_RETAILER_DETAIL left outer join TSPL_ASSET_DISPATCH_RETAILER_HEAD on TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No =TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No "
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_ASSET_DISPATCH_RETAILER_HEAD.comp_code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code  left outer join tspl_tax_master as tax1 on tax1.tax_code = TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax1  left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_ASSET_DISPATCH_RETAILER_HEAD.tax2   left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD .TAX3  left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_ASSET_DISPATCH_RETAILER_HEAD .tax4  left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD .tax5  left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_ASSET_DISPATCH_RETAILER_HEAD .TAX6   left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_ASSET_DISPATCH_RETAILER_HEAD .TAX7  left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_ASSET_DISPATCH_RETAILER_HEAD .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_ASSET_DISPATCH_RETAILER_HEAD .TAX9  left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_ASSET_DISPATCH_RETAILER_HEAD .TAX10  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location left join TSPL_STATE_MASTER as state_forlocation on state_forlocation.state_code=TSPL_LOCATION_MASTER.state"
            Qry += " left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_code
            left outer join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_To
            left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_by
            left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.ROUTE_NO=TSPL_ASSET_DISPATCH_RETAILER_HEAD.ROUTE_NO
            left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.ZONE_CODE=tspl_customer_master.ZONE_CODE
            left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Vehicle_Id
            left outer join TSPL_VEHICLE_MASTER AltVehicle on AltVehicle.Vehicle_Id=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Alternate_Vehicle_Id
            left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.asset_code=TSPL_ASSET_DISPATCH_RETAILER_detail.AssetCode "
            Qry += "where 2=2 and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No='" + txtDocNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptAssetDispatchRetailer", "Asset Dispatch Retailer")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtIssueTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtIssueTo._MYValidating
        Dim qry As String = ""
        If clsCommon.myLen(TxtDistributor.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Firstly select Distributor No", Me.Text)
            Return
        End If


        qry = "select Cust_Code AS [Code],Customer_Name As [Retailer Name],Add1 + ' ' + Add2 + ' ' + Add3 AS [Customer Address]
            ,City_Code As [City Code], State,Country ,Phone1 ,Phone2 ,Fax,Email ,WebSite,Contact_Person_Name As [Contact Person Name]
            ,Contact_Person_Email As [Contact Person Email] ,Contact_Person_Phone As [Contact Person Phone]
            ,Contact_Person_Fax As [Contact Person Fax],Contact_Person_Website AS [Contact Person Website] ,Terms_Code AS [Terms Code]			 
            ,Payment_Code As [Payment Code],Tax_Group As [Tax Group],Status ,OnHold ,CURRENCY_CODE As [Currency Code],Form_Type As [Form Type]
            ,State As [State Code],Country AS [Country Code] from TSPL_SECONDARY_CUSTOMER_MASTER "


        txtIssueTo.Value = clsCommon.ShowSelectForm("ISSUERet@1", qry, "Code", "  TSPL_SECONDARY_CUSTOMER_MASTER.Parent_Customer_No in ('" + TxtDistributor.Value + "')", txtIssueTo.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtIssueTo.Value) > 0 Then
            lblIssueTo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Customer_Name as [Customer Name] FROM TSPL_SECONDARY_CUSTOMER_MASTER Where cust_Code='" + clsCommon.myCstr(txtIssueTo.Value) + "'"))
        Else
            lblIssueTo.Text = ""
        End If
    End Sub

    Private Sub txtRequestBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRequestBy._MYValidating
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtRequestBy.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtRequestBy.Value = obj.EMP_CODE
            lblRequestBy.Text = obj.Emp_Name
        Else
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
        End If
    End Sub

    Private Sub txtFromLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLocation._MYValidating
        'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtFromLocation.Value, isButtonClicked)
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '    txtFromLocation.Value = obj.Code
        '    lblFromLocation.Text = obj.Name
        'Else
        '    txtFromLocation.Value = ""
        '    lblFromLocation.Text = ""
        'End If


        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtFromLocation.Value = clsCommon.ShowSelectForm("IRFROMLOC", qry, "Code", WhrCls, txtFromLocation.Value, "Code", isButtonClicked)
        lblFromLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))

    End Sub

    Private Sub txtToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        'Try
        '    Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Add1 as [Address 1],Add2 as [Address 2],Add3 as [Address 3],City_Code_Desc as City,State,Country from TSPL_VENDOR_MASTER"
        '    txtToLocation.Value = clsCommon.ShowSelectForm("POVeFNDID", qry, "Code", " upper(Form_type)='VSP'", txtToLocation.Value, "Code", isButtonClicked)
        '    ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        '    qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtToLocation.Value + "'"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '        lblToLocation.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
        '    Else
        '        lblToLocation.Text = ""
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub cboDocType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocType.SelectedValueChanged

        repoReturnQty.IsVisible = False
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            repoReqQty.IsVisible = False
            repoReqQty.HeaderText = "Required Quantity"
            repoQty.HeaderText = "Issue Quantity"
            chkLost.Checked = False
            chkLost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            'repoReqQty.IsVisible = True
            repoReturnQty.IsVisible = True
            repoReturnQty.IsVisible = True
            repoReturnQty.HeaderText = "Return Quantity"
            repoQty.HeaderText = "Issue Quantity"

            chkLost.Enabled = True
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
            repoReqQty.IsVisible = False
            repoQty.HeaderText = "Transfer Quantity"
            chkLost.Checked = False
            chkLost.Enabled = False
        End If

    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        txtTaxGroup.Value = clsCommon.ShowSelectForm("IRTAXGP", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        SetTaxDetails()
    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
            Next
            SetitemWiseTaxSetting(True, False)
        Else
            lblTaxGrpName.Text = ""
            For ii As Integer = 0 To gv1.Rows.Count - 1
                BlankTaxDetails(ii)
            Next
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal Then
            dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Else
            dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRetQty).Value)
        End If

        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
            If clsCommon.myLen(strTaxCode) > 0 Then
                Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                Dim dblBaseAmt As Double = 0
                Dim dblTaxAmt As Double = 0
                If IsSurTax Then
                    Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                    dblBaseAmt = dblSurTaxAmt
                Else
                    Dim dblOtherTaxAmt As Double = 0
                    ''If IsTaxable Then
                    dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                    ''End If
                    dblBaseAmt = (dblAmt + dblOtherTaxAmt)
                End If
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
                If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                    arrTaxableAuth.Add(strTaxCode.ToUpper())
                End If
            Else
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
            End If
        Next
        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmt + dblTotTaxAmt
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
    End Sub

    Sub LoadBlankGridTax()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.ReadOnly = True
        repoTaxRate.IsVisible = True
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = True
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)




        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetCode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                        End If
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo)
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colAssetCode)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            End If
        Next
    End Sub

    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If True Then ''clsCommon.CompairString(gv2.CurrentCell.ColumnInfo.Name, colTTaxRate) = CompairStringResult.Equal Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FDTaxRate", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                    Next
                End If
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetCode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                If clsCommon.myLen(frm.strItemCode) > 0 Then
                    frm.ArrIn = New List(Of clsTempItemTaxDetails)
                    For ii As Integer = 1 To 10
                        Dim strii As String = clsCommon.myCstr(ii)
                        Dim obj As New clsTempItemTaxDetails()
                        obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" + strii).Value)
                        If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                            obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                            obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value)
                            obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
                            obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value)
                            obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
                            obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
                            obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
                            frm.ArrIn.Add(obj)
                        End If
                    Next

                    frm.ShowDialog()
                    If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                        BlankTaxDetails(gv1.CurrentRow.Index)
                        For ii As Integer = 0 To frm.ArrOut.Count - 1
                            Dim strii As String = clsCommon.myCstr(ii + 1)
                            gv1.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
                            gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
                            gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
                            gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
                            gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                            gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                            gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                        Next
                        gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged
        If isInsideLoadData = False Then
            'lblReq2.Visible = False
            'lblReq3.Visible = False
            'chkWithoutRefNo.Checked = False
            'chkWithoutRefNo.Enabled = False
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal Then
                AddNew()
                'lblToLocation.Visible = False
                'txtToLocation.Visible = False
                'RadLabel9.Visible = False
                LblLoc.Visible = True
                LblLoc.Text = "From Location"
                lblFromLocation.Visible = True
                txtFromLocation.Visible = True
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                txtIssueTo.Enabled = True
                txtFromLocation.Enabled = True
                txtIssueNo.Enabled = False
                txtRemarks.Enabled = True
                txtComment.Enabled = True
                'gv1.ReadOnly = True 
                'lblReq.Text = "Requisition No"
                'lblIssueTo.Text = "Issue To"
                'lblReq.Visible = True
                'fndReqNo.Visible = True
                'lblReqDate.Visible = True
                repoReqIssueNo.HeaderText = "Req. No"
                chkLost.Checked = False
                chkLost.Enabled = False
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Return") = CompairStringResult.Equal Then
                AddNew()
                'lblToLocation.Visible = True
                'txtToLocation.Visible = True
                lblFromLocation.Visible = True
                txtFromLocation.Visible = True
                ' RadLabel9.Visible = True
                LblLoc.Text = "To Location"
                'chkWithoutRefNo.Enabled = True
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                txtIssueTo.Enabled = False
                txtFromLocation.Enabled = False
                txtIssueNo.Enabled = True
                txtRemarks.Enabled = False
                txtComment.Enabled = False
                'lblIssueTo.Text = "Issue No"
                'lblReq.Text = "Issue No"
                'lblReq.Visible = True
                'fndReqNo.Visible = True
                'lblReqDate.Visible = True
                repoReqIssueNo.HeaderText = "Issue No"
                chkLost.Enabled = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Transfer") = CompairStringResult.Equal Then
                AddNew()
                'lblToLocation.Visible = True
                'txtToLocation.Visible = True
                lblFromLocation.Visible = True
                txtFromLocation.Visible = True
                'RadLabel9.Visible = True
                LblLoc.Visible = True
                txtTaxGroup.Enabled = True
                lblTaxGrpName.Enabled = True
                'lblReq.Visible = False
                'fndReqNo.Visible = False
                'lblReqDate.Visible = False
                chkLost.Checked = False
                chkLost.Enabled = False
            End If
        End If
    End Sub

    Private Sub TxtVehicle__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVehicle._MYValidating
        'Dim Check As String = "select Segment_code, Description From TSPL_GL_SEGMENT_CODE Where Seg_No= '2' And Segment_code='" + TxtVehicle.Value + "'"
        'Dim Check As String = "select Segment_code, Description From TSPL_GL_SEGMENT_CODE Where Segment_code='" + TxtVehicle.Value + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Check)
        'If dt.Rows.Count <= 0 And isButtonClicked = False Then
        '    lblVehicleDesc.Text = ""
        'Else
        '    Dim Qry As String = "select Segment_code as [Code], Description,Segment_name as [Segment Name] From TSPL_GL_SEGMENT_CODE  "
        '    Dim WhrCls As String = " seg_no <>'7'  "
        '    TxtVehicle.Value = clsCommon.ShowSelectForm("Vehicle Selector", Qry, "Code", WhrCls, TxtVehicle.Value, "", isButtonClicked)
        '    lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where   Segment_Code= '" + TxtVehicle.Value + "'")
        'End If

        'Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
        'TxtVehicle.Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", TxtVehicle.Value, "", isButtonClicked)
        'lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Cost_name From TSPL_CostCenter_MASTER Where   Cost_Code= '" + TxtVehicle.Value + "'")

        Dim qry As String = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
        TxtVehicle.Value = clsCommon.ShowSelectForm("VehicleNo1", qry, "vehicle_id", "", TxtVehicle.Value, "vehicle_id", isButtonClicked)
        lblVehicleDesc.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(TxtVehicle.Value) + "'")

    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.Text), "Issue") = CompairStringResult.Equal Then
            If gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If

        'End If
    End Sub

    Private Sub fndReqNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        'If clsCommon.CompairString(cboDocType.Text, "Issue") = CompairStringResult.Equal Then
        '    Dim qry As String = "select Requisition_Id as Code,Requisition_Date as Date from TSPL_REQUISITION_HEAD "
        '    Dim whrclas As String = "is_internal='Y'"
        '    fndReqNo.Value = clsCommon.ShowSelectForm("Req no", qry, "Code", whrclas, fndReqNo.Value, "", isButtonClicked)
        '    lblReq3.Text = fndReqNo.Value
        '    If clsCommon.myLen(fndReqNo.Value) > 0 Then
        '        LoadReqDataHead(fndReqNo.Value)
        '        LoadReqDataDetail(fndReqNo.Value)
        '        lblReq2.Visible = False
        '        lblReq3.Visible = False
        '    End If
        'ElseIf clsCommon.CompairString(cboDocType.Text, "Return") = CompairStringResult.Equal Then
        '    Dim qry As String = "select Doc_No as Code,Doc_date as Date from TSPL_ASSET_DISPATCH_RETAILER_HEAD "
        '    Dim whrclas As String = "doc_type='Issue' and Posting_Date <> ''"
        '    fndReqNo.Value = clsCommon.ShowSelectForm("Req no", qry, "Code", whrclas, fndReqNo.Value, "", isButtonClicked)
        '    If clsCommon.myLen(fndReqNo.Value) > 0 Then
        '        LoadReqDataHead(fndReqNo.Value)
        '        LoadReqDataDetail(fndReqNo.Value)
        '        lblReq2.Visible = True
        '        lblReq3.Visible = True
        '    End If
        'End If
    End Sub

    'Private Sub LoadReqDataHead(ByVal strReqNo As String)
    '    Dim qry As String
    '    If clsCommon.CompairString(cboDocType.Text, "Issue") = CompairStringResult.Equal Then
    '        qry = "select Requisition_Date,Location,Location_Desc from TSPL_REQUISITION_HEAD inner join " & _
    '           "TSPL_LOCATION_MASTER on TSPL_REQUISITION_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code where Requisition_Id='" & fndReqNo.Value & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            ' lblReqDate.Text = clsCommon.myCstr(dt.Rows(0)("Requisition_Date"))
    '            txtFromLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location"))
    '            lblFromLocation.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
    '        End If
    '    Else
    '        qry = "SELECT TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location,FLocation.Location_Desc as FromLocationName, " & _
    '        "TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location,TLocation.Location_Desc as ToLocationName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_To, " & _
    '        "IssueEmp.Emp_Name as IssueToName ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.vehicle_Id,Req_IssueNo " & _
    '        "FROM TSPL_ASSET_DISPATCH_RETAILER_HEAD left outer join TSPL_LOCATION_MASTER as FLocation on  " & _
    '        "FLocation.Location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as TLocation on " & _
    '        "TLocation.Location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location left outer join TSPL_EMPLOYEE_MASTER as IssueEmp on " & _
    '        "IssueEmp.EMP_CODE= TSPL_ASSET_DISPATCH_RETAILER_HEAD.issue_To left outer join  " & _
    '        "TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By where TSPL_ASSET_DISPATCH_RETAILER_HEAD.doc_no='" & fndReqNo.Value & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            txtDate.Value = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
    '            txtFromLocation.Value = clsCommon.myCstr(dt.Rows(0)("To_Location"))
    '            lblFromLocation.Text = clsCommon.myCstr(dt.Rows(0)("ToLocationName"))
    '            txtToLocation.Value = clsCommon.myCstr(dt.Rows(0)("From_Location"))
    '            lblToLocation.Text = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))

    '            txtIssueTo.Value = clsCommon.myCstr(dt.Rows(0)("Issue_To"))
    '            lblIssueTo.Text = clsCommon.myCstr(dt.Rows(0)("IssueToName"))
    '            txtRequestBy.Value = clsCommon.myCstr(dt.Rows(0)("Request_By"))
    '            lblRequestBy.Text = clsCommon.myCstr(dt.Rows(0)("RequestByName"))
    '            TxtVehicle.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_Id"))
    '            lblReq3.Text = clsCommon.myCstr(dt.Rows(0)("Req_IssueNo"))
    '        End If

    '    End If

    'End Sub

    'Private Sub LoadReqDataDetail(ByVal strReqNo As String)
    '    gv1.Rows.Clear()
    '    If clsCommon.CompairString(cboDocType.Text, "Issue") = CompairStringResult.Equal Then
    '        Dim Qry As String = "select Item_Code,Item_Desc,Unit_Code,Requisition_Qty - (select isnull(SUM(Issued_Qty),0) from TSPL_ASSET_DISPATCH_RETAILER_HEAD inner join " & _
    '        "TSPL_ASSET_DISPATCH_RETAILER_DETAIL on TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No and " & _
    '        "TSPL_ASSET_DISPATCH_RETAILER_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Issue') + " & _
    '        "(select isnull(SUM(Issued_Qty),0) from TSPL_ASSET_DISPATCH_RETAILER_HEAD inner join TSPL_ASSET_DISPATCH_RETAILER_DETAIL on " & _
    '        "TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No and " & _
    '        "TSPL_ASSET_DISPATCH_RETAILER_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Return') as Requisition_Qty  " & _
    '        "from TSPL_REQUISITION_DETAIL where Requisition_Id='" & fndReqNo.Value & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                gv1.Rows.AddNew()
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strReqNo)
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = clsCommon.myCstr(dr("Item_Code"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = clsCommon.myCstr(dr("Item_Desc"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCstr(dr("Requisition_Qty"))
    '            Next
    '        End If
    '    Else
    '        Dim Qry As String = "select Item_Code,Item_Desc,Unit_code,Required_Qty,Issued_Qty from TSPL_ASSET_DISPATCH_RETAILER_DETAIL where Doc_No='" & fndReqNo.Value & "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                gv1.Rows.AddNew()
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strReqNo)
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = clsCommon.myCstr(dr("Item_Code"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = clsCommon.myCstr(dr("Item_Desc"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCstr(dr("Required_Qty"))
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Issued_Qty"))
    '            Next
    '        End If
    '    End If
    'End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsAssetDispatchRetailerHead.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse and Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
    End Sub


    Private Sub txtToLocation__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtToLocation._MYValidating
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Sale") = CompairStringResult.Equal Then
            Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER "
            txtToLocation.Value = clsCommon.ShowSelectForm("IRTCustCoode", qry, "Code", "Inter_Branch='Y'", txtToLocation.Value, "", isButtonClicked)
            lblToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtToLocation.Value + "' "))
        Else
            Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtToLocation.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtToLocation.Value = obj.Code
                lblToLocation.Text = obj.Name
            Else
                txtToLocation.Value = ""
                lblToLocation.Text = ""
            End If
        End If
    End Sub


    Private Sub txtIssueNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtIssueNo._MYValidating
        Dim qry As String = ""
        'qry = " Select Doc_No As [Code] ,  ISNULL(Issue_To,'' ) As [Issue To],vendor_name as [Issue To Name],Doc_Date As [Doc Date],From_Location As [From Location],Mcc_Name as [Mcc Name],Remarks ,Comment ," &
        '      " Request_By As [Request By],TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status ,On_Hold AS [On Hold] from TSPL_ASSET_DISPATCH_RETAILER_HEAD left join tspl_mcc_master on tspl_mcc_master.mcc_code=from_location left join tspl_vendor_master on vendor_code=issue_to "

        qry = "select * from (select distinct Doc_No as Code ,max(doc_date) as date from ( 
select  Item_Code, Issued_Qty As Qty,1 as RI,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.doc_date From TSPL_ASSET_DISPATCH_RETAILER_DETAIL LEFT OUTER JOIN TSPL_ASSET_DISPATCH_RETAILER_HEAD ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status =1 And Doc_Type <> 'Return' 
Union All  
select  Item_Code, Issued_Qty_againstret As Qty,-1 as RI,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.doc_date From TSPL_ASSET_DISPATCH_RETAILER_DETAIL  
LEFT OUTER JOIN TSPL_ASSET_DISPATCH_RETAILER_HEAD ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No  
where  Doc_Type = 'Return'  and isnull(TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No,'')<>''
)  aa  
where  convert(date,doc_date,103)<=convert(date,'" & txtDate.Value & "',103) 
group by item_Code,Doc_No  HAVING SUM(aa.Qty * RI)<>0
)zz "

        ' txtIssueNo.Value = clsCommon.ShowSelectForm("VSPISSUENo", qry, "Code", " TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status =1 And Doc_Type <> 'Return'  and convert(date,doc_date,103)<=convert(date,'" & txtDate.Value & "',103) ", txtIssueNo.Value, "Code", isButtonClicked)
        txtIssueNo.Value = clsCommon.ShowSelectForm("VSPISSUENo", qry, "Code", "  ", txtIssueNo.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtIssueNo.Value) > 0 Then
            qry = "SELECT TSPL_ASSET_DISPATCH_RETAILER_HEAD.IsItemWise,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type,TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location,FLocation.Location_Desc as FromLocationName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location,TLocation.Location_Desc as ToLocationName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Comment,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Remarks,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_To,IssueEmp.Customer_Name as IssueToName ,ISNULL(TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No,'' ) As Issue_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_Code,TSPL_CUSTOMER_MASTER.Customer_name as DistributorName,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Route_No,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By,RequestEmp.Emp_Name as RequestByName FROM TSPL_ASSET_DISPATCH_RETAILER_HEAD left outer join TSPL_LOCATION_MASTER as FLocation on FLocation.Location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as TLocation on TLocation.Location_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.To_Location left outer join TSPL_SECONDARY_CUSTOMER_MASTER as IssueEmp on IssueEmp.Cust_Code= TSPL_ASSET_DISPATCH_RETAILER_HEAD.issue_To left outer join TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_Code Where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No='" + clsCommon.myCstr(txtIssueNo.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtIssueTo.Value = clsCommon.myCstr(dr("Issue_To"))
                    lblIssueTo.Text = clsCommon.myCstr(dr("IssueToName"))
                    txtFromLocation.Value = clsCommon.myCstr(dr("From_Location"))
                    lblFromLocation.Text = clsCommon.myCstr(dr("FromLocationName"))
                    chkItem.Checked = IIf(clsCommon.myCdbl(dr("IsItemWise")) = 1, True, False)
                    txtRemarks.Text = clsCommon.myCstr(dr("Remarks"))
                    txtComment.Text = clsCommon.myCstr(dr("Comment"))
                    txtRequestBy.Value = clsCommon.myCstr(dr("Request_By"))
                    lblRequestBy.Text = clsCommon.myCstr(dr("RequestByName"))
                    TxtDistributor.Value = clsCommon.myCstr(dr("Distributor_Code"))
                    lblDistributor.Text = clsCommon.myCstr(dr("DistributorName"))
                    txtRouteNo.Value = clsCommon.myCstr(dr("Route_No"))
                    fndRouteNo_TextChanged()
                    chkItem.Enabled = False
                Next
            End If

            LoadGridForReturn(clsCommon.myCstr(txtIssueNo.Value), "")
        Else
            txtIssueTo.Value = ""
            lblIssueTo.Text = ""
            txtFromLocation.Value = ""
            lblFromLocation.Text = ""
            txtRemarks.Text = ""
            txtComment.Text = ""
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
            TxtDistributor.Value = ""
            lblDistributor.Text = ""
            gv1.Rows.Clear()
        End If
    End Sub
    Private Function ReturnBalQty(ByVal strDocNo As String, ByVal strItemCode As String, ByVal strIssueNo As String) As Double
        Dim BalQty As Double = 0
        BalQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(aa.Qty * RI) AS BalQty from (" &
                " select  Item_Code, Issued_Qty As Qty,1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL where TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No='" & strIssueNo & "' " &
                "  Union All " &
                " select  Item_Code, Issued_Qty_againstret As Qty,-1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL " &
                " LEFT OUTER JOIN TSPL_ASSET_DISPATCH_RETAILER_HEAD ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No " &
                " where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No='" & strIssueNo & "' and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No not IN('" & strDocNo & "') " &
                " )  aa " &
                " group by item_Code HAVING item_Code='" & strItemCode & "'"))
        Return BalQty
    End Function
    Private Sub LoadGridForReturn(ByVal strIssueNo As String, Optional ByVal strDocNo As String = "")
        gv1.Rows.Clear()
        isInsideLoadData = True
        Dim IssueNo As String = ""
        Dim Qry As String = "select * from  TSPL_ASSET_DISPATCH_RETAILER_DETAIL  where Doc_No='" & strIssueNo & "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows

                'gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strDocNo)

                Dim BalQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(aa.Qty * RI) AS BalQty from (" &
                " select  Item_Code, Issued_Qty As Qty,1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL where TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No='" & strIssueNo & "' " &
                "  Union All " &
                " select  Item_Code, Issued_Qty_againstret As Qty,-1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL " &
                " LEFT OUTER JOIN TSPL_ASSET_DISPATCH_RETAILER_HEAD ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No " &
                " where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No='" & strIssueNo & "' and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No not IN('" & strDocNo & "') " &
                " )  aa " &
                " group by item_Code HAVING item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "'"))
                If BalQty > 0 Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(dr("Line_No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colITEMCODE).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colITEMNAME).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = clsCommon.myCdbl(dr("Issued_Qty_againstret"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(BalQty)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = clsCommon.myCstr(dr("Cost_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Unit_Cost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCdbl(dr("Amount"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCCDesc).Value = clsCommon.myCstr(ClsCostCenter.GetCostCenterDesc(clsCommon.myCstr(dr("Cost_Code"))))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(clsCommon.myCstr(dr("Item_Code")))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = clsCommon.myCstr(dr("AssetCode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Asset_Name from TSPL_ACQUISITION_DETAIL WHERE asset_code='" + clsCommon.myCstr(dr("AssetCode")) + "'"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colASSETID).Value = clsCommon.myCstr(dr("AssetID"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBrandName).Value = clsCommon.myCstr(dr("BrandName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositRecNo).Value = clsCommon.myCstr(dr("DepositReceiptNo"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSerialNo).Value = clsCommon.myCstr(dr("SerialNo"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPINumber).Value = clsCommon.myCstr(dr("PurchaseInvoiceNo"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = clsCommon.myCdbl(dr("Capacity"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositValue).Value = clsCommon.myCdbl(dr("DepositValue"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositType).Value = clsCommon.myCstr(dr("DepositType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPIDate).Value = clsCommon.myCstr(dr("PurchaseInvoiceDate"))
                End If

            Next

        Else
            clsCommon.MyMessageBoxShow(Me, "No data found")
        End If
        isInsideLoadData = False
    End Sub


    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveLayout.Click
        '    If clsCommon.myLen(GetReportID()) > 0 Then
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = clsUserMgtCode.frmAssetDispatchRetailer
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        'End If
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData(clsUserMgtCode.frmAssetDispatchRetailer, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(clsUserMgtCode.frmAssetDispatchRetailer, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub Btn_export_Click(sender As Object, e As EventArgs) Handles Btn_export.Click
        Dim Str As String = "select TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_no as [Document Code],Doc_date as [Document Date],Issue_No as [Issue No],From_Location as [From Location]," _
                            & " Location_Desc as [From Locattion Name],Issue_to as [Issued To],Vendor_Name as [Issued To Name],Request_by as [Requested By],Emp_Name as " _
                            & " [Requested By Name],Comment,Remarks,Item_code as [Item Code],Item_Desc as [Item Desc],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Cost_code as [Cost Center Code] " _
                            & " ,cost_name as  [Cost Center Name],Unit_code as [UOM],Issued_Qty as [Issue Qty]  from TSPL_ASSET_DISPATCH_RETAILER_HEAD inner join TSPL_ASSET_DISPATCH_RETAILER_DETAIL on " _
                            & " TSPL_ASSET_DISPATCH_RETAILER_DETAIL.doc_no=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No left join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.cost_code=" _
                            & " TSPL_ASSET_DISPATCH_RETAILER_DETAIL.cost_code left join tspl_location_master on Location_Code=From_Location left join tspl_vendor_master on" _
                            & " tspl_vendor_master.vendor_code=Issue_To left join TSPL_EMPLOYEE_MASTER on EMP_CODE=Request_By"
        transportSql.ExporttoExcel(Str, Me)
        Str = Nothing
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImport.Click
        Dim gv As New RadGridView()
        Dim totqty As Double = 0
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Document Code", "Document Date", "Issue No", "From Location", "From Location Name", "Issued To", "Issued To Name", "Requested By", "Requested By Name", "Comment", "Remarks", "Item Code", "Item Desc", "Cost Center Code", "Cost Center Name", "UOM", "Issue Qty") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim Doc_Code As String = ""
                Dim Issue_No As String = ""
                Dim Doc_Date As DateTime = Nothing
                Dim Frm_Location As String = ""
                Dim Issued_To As String = ""
                Dim Request_By As String = ""
                Dim Comment As String = ""
                Dim Remarks As String = ""
                Dim Item_Code As String = ""
                Dim Item_Desc As String = ""
                Dim Cost_Centre_code As String = ""
                Dim Cost_Centre_name As String = ""
                Dim UOM As String = ""
                Dim Issued_Qty As Double

                clsCommon.ProgressBarShow()
                Dim counter As Integer = 0
                Dim qry As String = ""
                Dim check As Integer = 0

                For Each grow As GridViewRowInfo In gv.Rows
                    'trans = clsDBFuncationality.GetTransactin()
                    counter += 1
                    Doc_Code = clsCommon.myCstr(grow.Cells("Document code").Value)
                    If clsCommon.myLen(Doc_Code) <= 0 Then
                        Throw New Exception("Fill Document Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(Doc_Code) > 30 Then
                        Throw New Exception("Length Of Document Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If


                    Issue_No = clsCommon.myCstr(grow.Cells("Issue No").Value)
                    If clsCommon.myLen(Issue_No) > 0 Then
                        qry = "select count(*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No='" + Issue_No + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("Issue No Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Issue No")
                        End If
                    End If

                    Frm_Location = clsCommon.myCstr(grow.Cells("From Location").Value)
                    If clsCommon.myLen(Frm_Location) > 0 Then
                        qry = "select count(*) from Tspl_Location_Master where Loc_Code='" + Frm_Location + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("From Location Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of From Location")
                        End If
                    End If

                    Issued_To = clsCommon.myCstr(grow.Cells("Issued_To").Value)
                    If clsCommon.myLen(Issued_To) > 0 Then
                        qry = "select count(*) from Tspl_Vendor_Master where Vendor_Code='" + Issued_To + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("Vendor Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Vendor Code")
                        End If
                    End If

                    Request_By = clsCommon.myCstr(grow.Cells("Request By").Value)
                    If clsCommon.myLen(Request_By) > 0 Then
                        qry = "select count(*) from Tspl_Employee_Master where Emp_Code='" + Request_By + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("Employee Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Employee Code")
                        End If
                    End If


                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Document Date").Value)) > 0 Then
                        Issue_No = clsCommon.myCDate(grow.Cells("Document Date").Value)
                    Else
                        Throw New Exception("Please Fill Document Date At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Comment = clsCommon.myCstr(grow.Cells("Comment").Value)
                    Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)

                    Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(Item_Code) > 0 Then
                        qry = "select count(*) from Tspl_Item_Master where Item_Code='" + Item_Code + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("Item Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Item Code")
                        End If
                    End If

                    Cost_Centre_code = clsCommon.myCstr(grow.Cells("Cost Center Code").Value)
                    If clsCommon.myLen(Cost_Centre_code) > 0 Then
                        qry = "select count(*) from TSPL_CostCenter_MASTER where Cost_Code='" + Cost_Centre_code + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("Cost Center Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Cost Center Code")
                        End If
                    End If


                    UOM = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If clsCommon.myLen(UOM) > 0 Then
                        qry = "select count(*) from TSPL_Unit_MASTER where Unit_Code='" + UOM + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("Unit Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Unit Code")
                        End If
                    End If

                    Issued_Qty = clsCommon.myCdbl(grow.Cells("Issued Qty").Value)

                    Cost_Centre_name = clsCommon.myCstr(grow.Cells("Cost Center Name").Value).Replace("'", "`")
                    Item_Desc = clsCommon.myCstr(grow.Cells("Item name").Value)

                    txtDocNo.Value = Doc_Code
                    txtIssueNo.Value = Issue_No
                    txtDate.Value = Doc_Date
                    txtFromLocation.Value = Frm_Location
                    txtIssueTo.Value = Issued_To
                    txtRequestBy.Value = Request_By
                    txtComment.Text = Comment
                    txtRemarks.Text = Remarks
                    gv1.Rows.AddNew()
                    gv1.Rows(0).Cells(colAssetCode).Value = Item_Code
                    gv1.Rows(0).Cells(colAssetName).Value = Item_Desc
                    gv1.Rows(0).Cells(colCCCode).Value = Cost_Centre_code
                    gv1.Rows(0).Cells(colCCDesc).Value = Cost_Centre_name
                    gv1.Rows(0).Cells(colQty).Value = Issued_Qty
                    gv1.Rows(0).Cells(colUnit).Value = UOM
                    gv1.Rows(0).Cells(colRate).Value = Issued_Qty



                    '                    objHead = New clsMilkReceiptMCC
                    '                    objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    '                    objHead.DOC_DATE = clsCommon.myCDate(ShiftDate)
                    '                    objHead.SHIFT = clsCommon.myCstr(Shift)
                    '                    objHead.COMM_PORT = clsCommon.myCstr(cboComPort.Text)
                    '                    objHead.MCC_CODE = clsCommon.myCstr(Mcccode)
                    '                    objHead.Irregular_MCC_CODE = clsCommon.myCstr(Irregular_Mcc_Code)
                    '                    ' objHead.MACHINE_NO = clsCommon.myCstr(txtSerialNo.Text)
                    '                    'objHead.TOTAL_WEIGHT = clsCommon.myCdbl(txtTotalWeight.Text)
                    '                    objHead.TOTAL_WEIGHT = clsCommon.myCdbl(txtTotalWeight.Text)
                    '                    Dim objList As New List(Of clsMilkReceiptMCCDetail)

                    '                    Dim obj As clsMilkReceiptMCCDetail
                    '                    obj = New clsMilkReceiptMCCDetail
                    '                    obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    '                    obj.VLC_DOC_CODE = IIf(IsNothing(lblVLCCode.Tag), "", lblVLCCode.Tag) '' generate VLC_DOC_CODE by function
                    '                    'If Not btnsave.Text = "Update" Then
                    '                    obj.SAMPLE_NO = SampleNo '0 '"" ''generate VLC_DOC_CODE by function
                    '                    'End If
                    '                    obj.VLC_CODE = clsCommon.myCstr(fndVLCCode.Tag)
                    '                    obj.ROUTE_CODE = clsCommon.myCstr(routecode)
                    '                    obj.VSP_CODE = clsCommon.myCstr(fndVspCode.Text)
                    '                    obj.Item_CODE = clsCommon.myCstr(fndItem_Code.Text)
                    '                    obj.VEHICLE_CODE = clsCommon.myCstr(fndVehicleCode.Text)
                    '                    obj.Other_VEHICLE = IIf(clsCommon.CompairString(Vlc_Is_Other, "True") = CompairStringResult.Equal, "T", "F")
                    '                    obj.Other_VLC = Other_VLc
                    '                    obj.NO_OF_CANS = clsCommon.myCstr(No_of_cans)
                    '                    obj.MILK_WEIGHT = clsCommon.myCdbl(Milk_Weight)
                    '                    obj.ACC_WEIGHT = clsCommon.myCdbl(Milk_Weight * conv_fac)
                    '                    obj.LTR_WEIGHT = clsCommon.myCdbl((Milk_Weight * conv_fac) / IIf(conv_fac_Ltr <= 0, 1, conv_fac_Ltr))
                    '                    obj.TYPE = clsCommon.myCstr(cboType.Text)
                    '                    obj.MILK_TYPE = clsCommon.myCstr(cboMilkType.Text)
                    '                    obj.SAMPLE_NO_VALUES = "" '' generate sample no values

                    '                    obj.DOC_DATE = clsCommon.myCDate(ShiftDate)
                    '                    obj.SHIFT = clsCommon.myCstr(Shift)
                    '                    obj.COMM_PORT = clsCommon.myCstr(cboComPort.Text)
                    '                    obj.MCC_CODE = clsCommon.myCstr(Mcccode)
                    '                    ' obj.MACHINE_NO = clsCommon.myCstr(txtSerialNo.Text)
                    '                    obj.IS_ENTERED_MANUAL = "Y"
                    '                    obj.UOM_Code = Unit_Code
                    '                    obj.Conversion_Factor = conv_fac
                    '                    objList.Add(obj)

                    '                    If clsMilkReceiptMCC.SaveData(objHead, objList, trans) Then
                    '                        ' trans.Commit()

                    '                        'UcAttachment1.SaveData(objHead.DOC_CODE)

                    '                        'LoadData(objHead.DOC_CODE)
                    '                        'fndVLCCode.Focus()
                    '                    End If
a:              Next
                '                ' clsCommon.MyMessageBoxShow("Data Imported Successfully", Me.Text)
                '                clsCommon.ProgressBarHide()
                '                trans.Commit()
                '                clsCommon.MyMessageBoxShow("Data Imported Successfully", Me.Text)
                '                LoadData(txtCode.Value)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    ' Ticket No : TEC/29/10/18-000352 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub

    Private Sub btnUploder_Click(sender As Object, e As EventArgs) Handles btnUploder.Click
        Dim gv As New RadGridView()
        Dim totqty As Double = 0
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Doc Type", "Document Code", "Document Date", "Issue No", "From Location", "From Locattion Name", "VLC Uploader Code", "Issued To", "Issued To Name", "Requested By", "Requested By Name", "Comment", "Remarks", "IsLost", "Item Code", "Item Desc", "Cost Center Code", "Cost Center Name", "UOM", "Unit Cost", "Issue Qty", "Return Qty", "Serial No") Then
            '**************************Validation Check**********************************

            Dim Doc_Type As String = ""
            Dim Doc_Code As String = ""
            Dim Issue_No As String = ""
            Dim Doc_Date As DateTime = Nothing
            Dim Frm_Location As String = ""
            Dim VlcUploaderCode As String = ""
            Dim Issued_To As String = ""
            Dim Request_By As String = ""
            Dim Comment As String = ""
            Dim Remarks As String = ""
            Dim Item_Code As String = ""
            Dim Item_Desc As String = ""
            Dim Cost_Centre_code As String = ""
            Dim Cost_Centre_name As String = ""
            Dim UOM As String = ""
            Dim Unit_Cost As Double = 0
            Dim Issued_Qty As Double
            Dim Return_Qty As Double = 0
            Dim IsLost As Integer = 0
            Dim SerialNo As String = ""
            clsCommon.ProgressBarShow()
            Try


                Dim counter As Integer = 0
                Dim qry As String = ""
                Dim check As Integer = 0

                'Check Duplicate Row (Serial Item)
                For ii As Integer = 0 To gv.Rows.Count - 1
                    Dim strItemCode As String = clsCommon.myCstr(gv.Rows(ii).Cells("Item Code").Value)
                    Dim strItemName As String = clsCommon.myCstr(gv.Rows(ii).Cells("Item Desc").Value)
                    If clsCommon.myLen(strItemCode) > 0 Then
                        Dim strIssueTo As String = clsCommon.myCstr(gv.Rows(ii).Cells("Issued To").Value)
                        Dim strDocType As String = clsCommon.myCstr(gv.Rows(ii).Cells("Doc Type").Value)
                        Dim strSerialNo As String = clsCommon.myCstr(gv.Rows(ii).Cells("Serial No").Value)
                        Dim Is_Serial_Item As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ITEM_MASTER where Is_Serial_Item =1 and Item_code = '" + strItemCode + "'"))
                        If Is_Serial_Item = True Then
                            If clsCommon.myLen(strSerialNo) <= 0 Then
                                Throw New Exception("Define Serial No for Item " + strItemCode.Trim() + "( " + strItemName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "")
                            End If
                            For jj As Integer = 0 To gv.Rows.Count - 1
                                If (ii = jj) Then
                                    Continue For
                                End If
                                'Item Code,Issue To,Doc Type,Serial No
                                If (clsCommon.CompairString(strItemCode, clsCommon.myCstr(gv.Rows(jj).Cells("Item Code").Value)) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strIssueTo, clsCommon.myCstr(gv.Rows(jj).Cells("Issued To").Value)) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strDocType, clsCommon.myCstr(gv.Rows(jj).Cells("Doc Type").Value)) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strSerialNo, clsCommon.myCstr(gv.Rows(jj).Cells("Serial No").Value)) = CompairStringResult.Equal) Then
                                    Throw New Exception("Duplicate Item " + strItemCode.Trim() + "( " + strItemName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                                End If
                            Next
                        End If
                    End If
                Next
                For Each grow As GridViewRowInfo In gv.Rows

                    counter += 1
                    Doc_Type = clsCommon.myCstr(grow.Cells("Doc Type").Value)
                    If clsCommon.CompairString(Doc_Type, "Issue") = CompairStringResult.Equal OrElse clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Doc Type Should be Issue/Return At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    'Doc_Code = clsCommon.myCstr(grow.Cells("Document code").Value)
                    '    If clsCommon.myLen(Doc_Code) <= 0 Then
                    '        Throw New Exception("Fill Document Code At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    '    If clsCommon.myLen(Doc_Code) > 30 Then
                    '        Throw New Exception("Length Of Document Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If


                    Issue_No = clsCommon.myCstr(grow.Cells("Issue No").Value)

                    If clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                        If clsCommon.myLen(Issue_No) > 0 Then
                            qry = "select count(*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_No='" + Issue_No + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry))

                            If check <= 0 Then
                                Throw New Exception("Issue No Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Issue No")
                            End If
                            qry = "select count(*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status = 1 and Doc_No='" + Issue_No + "'"
                            check = CInt(clsDBFuncationality.getSingleValue(qry))
                            If check <= 0 Then
                                Throw New Exception("Please First Issue No (" + Issue_No + ")  Post. At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        Else
                            Throw New Exception("Issue No can not be blank because this is  Return type document. At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Issue No")
                        End If



                    Else
                        If clsCommon.CompairString(Issue_No, "NA") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Issue No should be NA At Line No. " + clsCommon.myCstr(counter) + "")
                        End If


                    End If

                    Frm_Location = clsCommon.myCstr(grow.Cells("From Location").Value)
                    If clsCommon.myLen(Frm_Location) > 0 Then
                        qry = "select count(*) from Tspl_Location_Master where Location_Type='Physical'  and location_category='MCC' and Location_Code='" + Frm_Location + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry))

                        If check <= 0 Then
                            Throw New Exception("From Location Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of From Location")
                        End If
                        If clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                            Dim isValidFromLocation As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select  count (*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_no ='" + Issue_No + "' and From_Location = '" + Frm_Location + "'"))
                            If isValidFromLocation = False Then
                                Throw New Exception("[From Location] not Exist in Issue No (" + Issue_No + ") At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    Else
                        Throw New Exception("From Location can not be blank At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Issued_To = clsCommon.myCstr(grow.Cells("Issued To").Value)
                    VlcUploaderCode = clsCommon.myCstr(grow.Cells("VLC Uploader Code").Value)
                    If clsCommon.myLen(Issued_To) <= 0 AndAlso clsCommon.myLen(VlcUploaderCode) <= 0 Then
                        Throw New Exception("Please fill [Issued To] OR [Vlc Uploader Code] At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(Issued_To) <= 0 AndAlso clsCommon.myLen(VlcUploaderCode) > 0 Then
                        Issued_To = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VSP_Code from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + VlcUploaderCode + "' and MCC = '" + Frm_Location + "'"))
                        If clsCommon.myLen(Issued_To) <= 0 Then
                            Throw New Exception("Invalid VLC Uploader Code At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    If clsCommon.myLen(Issued_To) > 0 Then
                        qry = "select count(*) from Tspl_Vendor_Master  left join tspl_vlc_master_Head on tspl_vlc_master_Head.vsp_code=vendor_code and mcc='" + Frm_Location + "' where   Vendor_Code='" + Issued_To + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry))

                        If check <= 0 Then
                            Throw New Exception("Issue to not Exist in From Location  At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                            Dim isValidIssueTo As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select  count (*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_no ='" + Issue_No + "' and Issue_To = '" + Issued_To + "'"))
                            If isValidIssueTo = False Then
                                Throw New Exception("[Issue To] not Exist in Issue No (" + Issue_No + ") At Line No. " + clsCommon.myCstr(counter) + "")
                            End If

                        End If
                    Else
                        Throw New Exception("Issued To can not be blank At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Request_By = clsCommon.myCstr(grow.Cells("Requested By").Value)
                    If clsCommon.myLen(Request_By) > 0 Then
                        qry = "select count(*) from Tspl_Employee_Master where Emp_Code='" + Request_By + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry))

                        If check <= 0 Then
                            Throw New Exception("Employee Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Employee Code")
                        End If
                    End If


                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Document Date").Value)) > 0 Then
                        Doc_Date = clsCommon.myCDate(grow.Cells("Document Date").Value)
                        Dim IsSettingOn As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowFutureDateTransaction, clsFixedParameterType.AllowFutureDateTransaction, Nothing)) = 1, True, False)
                        If IsSettingOn = False Then
                            If Doc_Date > clsCommon.GETSERVERDATE(Nothing) Then
                                Throw New Exception("Future Date not allow At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        If clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                            Dim qry555 As String = "select  count (*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_no ='" + Issue_No + "'  and convert (date,Doc_Date,103) <= convert (date, '" + clsCommon.myCstr(clsCommon.myCDate(grow.Cells("Document Date").Value)) + "',103) "
                            Dim isValidReturnDate As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select  count (*) from TSPL_ASSET_DISPATCH_RETAILER_HEAD where Doc_no ='" + Issue_No + "'  and convert (date,Doc_Date,103) <= convert (date, '" + clsCommon.myCstr(clsCommon.myCDate(grow.Cells("Document Date").Value)) + "',103) "))
                            If isValidReturnDate = False Then
                                Throw New Exception("Document Date Should be greater then Issue No (" + Issue_No + ") document Date  At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        'If AllowFutureDateTransaction(Doc_Date, Nothing) = False Then
                        '    Throw New Exception("Future Date not allow At Line No. " + clsCommon.myCstr(counter) + "")
                        'End If
                    Else
                        Throw New Exception("Please Fill Document Date At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Comment = clsCommon.myCstr(grow.Cells("Comment").Value)
                    Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)

                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsLost").Value), "0") <> CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsLost").Value), "1") <> CompairStringResult.Equal Then
                        Throw New Exception("IsLost column should be 0 Or 1 At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    IsLost = clsCommon.myCdbl(grow.Cells("IsLost").Value)
                    If clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(IsLost, 0) = CompairStringResult.Equal OrElse clsCommon.CompairString(IsLost, 1) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("IsLost column should be 0 Or 1 At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        If clsCommon.CompairString(IsLost, 0) <> CompairStringResult.Equal Then
                            Throw New Exception("IsLost column should be 0 because Issue type document Lost not possible. At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(Item_Code) > 0 Then
                        qry = "select count(*) from Tspl_Item_Master where Item_Type = 'A' and Item_Code='" + Item_Code + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry))

                        If check <= 0 Then
                            Throw New Exception("Item Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                            Dim chckItemExistInIssue As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_ASSET_DISPATCH_RETAILER_DETAIL where Item_Code = '" + Item_Code + "' and Doc_No = '" + Issue_No + "'"))
                            If chckItemExistInIssue = False Then
                                Throw New Exception("Item Code Does Not Exist in  " + Issue_No + " Issue No  At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    Else
                        If clsCommon.myLen(Doc_Type) > 0 Then
                            Throw New Exception("Please Fill Item Code At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                    End If

                    Cost_Centre_code = clsCommon.myCstr(grow.Cells("Cost Center Code").Value)
                    If clsCommon.myLen(Cost_Centre_code) > 0 Then
                        qry = "select count(*) from TSPL_CostCenter_MASTER where Cost_Code='" + Cost_Centre_code + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry))

                        If check <= 0 Then
                            Throw New Exception("Cost Center Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Cost Center Code")
                        End If
                    End If


                    UOM = clsCommon.myCstr(grow.Cells("UOM").Value)
                    If clsCommon.myLen(UOM) > 0 Then
                        qry = "select count (*) from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + Item_Code + "' and UOM_Code = '" + UOM + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry))

                        If check <= 0 Then
                            Throw New Exception("Unit Code Does Not Exist in for " + Item_Code + " Item At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Unit Code can not be blank  for " + Item_Code + " Item At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Unit_Cost = clsCommon.myCdbl(grow.Cells("Unit Cost").Value)
                    If Unit_Cost <= 0 Then
                        Throw New Exception("Unit Cost Can not be Zero for " + Item_Code + " Item At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Issued_Qty = clsCommon.myCdbl(grow.Cells("Issue Qty").Value)
                    If Issued_Qty <= 0 Then
                        Throw New Exception("Issued Qty  can not be blank or Zero for " + Item_Code + " Item At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(Doc_Type, "Return") = CompairStringResult.Equal Then
                        Dim chckItemExistInIssue As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Issued_Qty from TSPL_ASSET_DISPATCH_RETAILER_DETAIL where Item_Code = '" + Item_Code + "' and Doc_No = '" + Issue_No + "'"))
                        If chckItemExistInIssue = Issued_Qty Then
                        Else
                            Throw New Exception("Issued Qty should be " + clsCommon.myCstr(chckItemExistInIssue) + "   For" + Issue_No + " Issue No  At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    Cost_Centre_name = clsCommon.myCstr(grow.Cells("Cost Center Name").Value).Replace("'", "`")

                    If clsCommon.CompairString(Doc_Type, "Issue") = CompairStringResult.Equal Then
                        'Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(Item_Code, UOM, Nothing)
                        'Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(Item_Code, Frm_Location, "", clsCommon.GETSERVERDATE(), Nothing, UOM)
                        'Dim dblEnteredQty As Double = Issued_Qty
                        'For jj As Integer = 0 To gv.Rows.Count - 1
                        '    If counter - 1 = jj Then
                        '        Continue For
                        '    End If
                        '    Dim strICodeInner As String = clsCommon.myCstr(gv.Rows(jj).Cells("Item Code").Value)
                        '    Dim strUOMInner As String = clsCommon.myCstr(gv.Rows(jj).Cells("UOM").Value)
                        '    Dim dblQtyInner As Double = clsCommon.myCdbl(gv.Rows(jj).Cells("Issue Qty").Value)
                        '    Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        '    If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, Item_Code) = CompairStringResult.Equal Then
                        '        dblEnteredQty += dblQtyInner
                        '    End If
                        'Next
                        'dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                        'If dblEnteredQty > dblBalQty Then
                        '    Throw New Exception("Item - " + Item_Code + Environment.NewLine + "Total Entered Quantity In Excel Sheet - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        'End If
                    Else
                        If clsCommon.myCdbl(Return_Qty) > clsCommon.myCdbl(Issued_Qty) Then
                            Throw New Exception("Return Qty  cant not greter then Issue Qty for Return  for " + Item_Code + " Item At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If



                Next

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Exit Sub
            End Try
            '************************************************************
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()



            Dim dtgv As DataTable = CType(gv.DataSource, DataTable)
            '===================================================
            For Each row As DataRow In dtgv.Rows
                'Dim aa As String = clsCommon.myCstr(row("Issued To"))
                'Dim bb As String = clsCommon.myCstr(row("VLC Uploader Code"))
                If clsCommon.myLen(clsCommon.myCstr(row("Issued To"))) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr(row("VLC Uploader Code"))) > 0 Then
                    'Dim cc As String = "select VSP_Code from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + clsCommon.myCstr(row("VLC Uploader Code")) + "' and MCC = '" + clsCommon.myCstr(row("From Location")) + "'"
                    'Dim aaaaaa As String = ""
                    Dim strIssueTo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VSP_Code from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + clsCommon.myCstr(row("VLC Uploader Code")) + "' and MCC = '" + clsCommon.myCstr(row("From Location")) + "'", trans))
                    row.SetField("Issued To", strIssueTo)
                End If
            Next
            '===================================================

            Dim dtData As DataTable = dtgv.Copy()
            Dim dtResult As DataTable = dtgv.Clone()
            Dim view As DataView = New DataView(dtData)
            Dim DistinctcolumnName() As String = {"Doc Type", "Document Date", "From Location", "Issued To", "Issue No", "IsLost"}
            Dim dtCust As DataTable = view.ToTable(True, DistinctcolumnName)
            Try


                For i As Integer = 0 To dtCust.Rows.Count - 1
                    Dim BeforeTax_Amt As Double = 0
                    Dim Doc_Amt As Double = 0
                    Dim dtCustData As DataTable = Nothing
                    Dim dr As DataRow() = dtData.Select(" [Doc Type]='" + clsCommon.myCstr(dtCust.Rows(i)("Doc Type")) + "' AND [Document Date]='" + clsCommon.myCstr(dtCust.Rows(i)("Document Date")) + "'  AND [From Location]='" + clsCommon.myCstr(dtCust.Rows(i)("From Location")) + "' AND [Issued To]='" + clsCommon.myCstr(dtCust.Rows(i)("Issued To")) + "' AND [Issue No]='" + clsCommon.myCstr(dtCust.Rows(i)("Issue No")) + "' AND [IsLost]='" + clsCommon.myCstr(dtCust.Rows(i)("IsLost")) + "'  ")
                    If dr IsNot Nothing AndAlso dr.Length > 0 Then
                        dtCustData = dr.CopyToDataTable()
                        '======================================================================
                        Dim obj As New clsAssetDispatchRetailerHead()
                        obj.Doc_No = ""
                        obj.Doc_Date = clsCommon.myCstr(dtCustData.Rows(0)("Document Date"))
                        obj.Doc_Type = clsCommon.myCstr(dtCustData.Rows(0)("Doc Type"))
                        obj.Issue_To = clsCommon.myCstr(dtCustData.Rows(0)("Issued To"))
                        obj.Request_By = clsCommon.myCstr(dtCustData.Rows(0)("Requested By"))
                        obj.Remarks = clsCommon.myCstr(dtCustData.Rows(0)("Remarks"))
                        obj.Comment = clsCommon.myCstr(dtCustData.Rows(0)("Comment"))
                        obj.On_Hold = False

                        obj.IS_LOST = clsCommon.myCdbl(dtCustData.Rows(0)("IsLost"))
                        obj.From_Location = clsCommon.myCstr(dtCustData.Rows(0)("From Location"))
                        obj.To_Location = clsCommon.myCstr(dtCustData.Rows(0)("From Location"))

                        If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                            obj.Issue_No = clsCommon.myCstr(dtCustData.Rows(0)("Issue No"))

                        End If
                        obj.Tax_Group = Nothing
                        obj.Tax_Desc = Nothing

                        obj.Arr = New List(Of clsAssetDispatchRetailerDetail)

                        Dim TempdtCustData As DataTable = dtCustData.Copy()
                        Dim viewItem As DataView = New DataView(TempdtCustData)
                        Dim DistinctcolumnNameWithItem() As String = {"Doc Type", "Document Date", "From Location", "Issued To", "Issue No", "IsLost", "Item Code"}
                        Dim dtItem As DataTable = viewItem.ToTable(True, DistinctcolumnNameWithItem)

                        For k As Integer = 0 To dtItem.Rows.Count - 1
                            Dim dtItemData As DataTable = Nothing
                            Dim drItem As DataRow() = TempdtCustData.Select(" [Doc Type]='" + clsCommon.myCstr(dtItem.Rows(k)("Doc Type")) + "' AND [Document Date]='" + clsCommon.myCstr(dtItem.Rows(k)("Document Date")) + "'  AND [From Location]='" + clsCommon.myCstr(dtItem.Rows(k)("From Location")) + "' AND [Issued To]='" + clsCommon.myCstr(dtItem.Rows(k)("Issued To")) + "' AND [Issue No]='" + clsCommon.myCstr(dtItem.Rows(k)("Issue No")) + "' AND [IsLost]='" + clsCommon.myCstr(dtItem.Rows(k)("IsLost")) + "' AND [Item Code]='" + clsCommon.myCstr(dtItem.Rows(k)("Item Code")) + "' ")
                            If drItem IsNot Nothing AndAlso drItem.Length > 0 Then
                                dtItemData = drItem.CopyToDataTable()

                                Dim objTr As New clsAssetDispatchRetailerDetail()
                                objTr.Line_No = k + 1
                                objTr.Req_IssueNo = ""
                                objTr.Item_Code = clsCommon.myCstr(dtItemData.Rows(0)("Item Code"))
                                objTr.Item_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where ITEM_Code = '" + objTr.Item_Code + "'", trans))
                                objTr.Cost_Code = Nothing
                                objTr.Required_Qty = 0

                                objTr.Unit_code = clsCommon.myCstr(dtItemData.Rows(0)("UOM"))
                                objTr.Unit_Cost = clsCommon.myCdbl(dtItemData.Rows(0)("Unit Cost"))

                                objTr.Issued_Qty = clsCommon.myCdbl(dtItemData.Rows(0)("Issue Qty"))
                                objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(dtItemData.Rows(0)("Return Qty"))

                                objTr.Total_Tax_Amt = 0
                                'objTr.Item_Net_Amt = objTr.Amount

                                objTr.EMI_Asset_Value = 0
                                objTr.EMI_No_Of_Payment_Cycle = 0

                                Dim Is_Serial_Item As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ITEM_MASTER where Is_Serial_Item =1 and Item_code = '" + objTr.Item_Code + "'   ", trans))
                                objTr.arrSrItem = Nothing
                                Dim objList As New List(Of clsSerializeInvenotry)

                                ''''''''''''''''''''''''''''''''''''''''''''
                                If Is_Serial_Item = True Then
                                    objTr.Issued_Qty = 0
                                    objTr.Issued_Qty_AgainstRet = 0
                                    For j As Integer = 0 To dtItemData.Rows.Count - 1
                                        Dim strSerialNo As String = clsCommon.myCstr(dtItemData.Rows(j)("Serial No"))
                                        If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then


                                            Dim qrySerial As String = " select Auto_Sr_No as AutoSerialNo,Auto_Bin_No,Tag_no from ( select Auto_Sr_No,Auto_Bin_No,Tag_no ,max(Document_Date) as Document_Date  from TSPL_SERIAL_ITEM LEFT JOIN TSPL_VISI_MASTER ON Serial_No=Auto_Sr_No  where Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "' and Auto_Sr_No='" + strSerialNo + "' and not( Document_Code = '' and Document_Type = '') group by Auto_Sr_No,Auto_Bin_No,Tag_no having sum(case when In_Out_Type='I' and Against_Inv_Movement_Trans_Id is not null then 1 else case when In_Out_Type='O' then -1 else 0 end end)>0   )xxx  Where 2=2  "
                                            Dim DtSerialItem As DataTable = clsDBFuncationality.GetDataTable(qrySerial, trans)
                                            If DtSerialItem IsNot Nothing AndAlso DtSerialItem.Rows.Count > 0 Then

                                                objTr.Issued_Qty = objTr.Issued_Qty + 1
                                                Dim objsri As New clsSerializeInvenotry
                                                objsri.Auto_Sr_No = strSerialNo
                                                objList.Add(objsri)

                                            Else
                                                Throw New Exception("Serialize balance is not Avilable for Item code " + clsCommon.myCstr(objTr.Item_Code) + " (" + strSerialNo + ") ")
                                            End If
                                        Else
                                            Dim qrySerial As String = " select Auto_Sr_No as AutoSerialNo,Auto_Bin_No,Tag_no from ( select Auto_Sr_No,Auto_Bin_No,Tag_no ,max(Document_Date) as Document_Date  from TSPL_SERIAL_ITEM LEFT  JOIN TSPL_VISI_MASTER ON Serial_No=Auto_Sr_No  where Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "' and Auto_Sr_No='" + strSerialNo + "' and   Document_Code = '" + obj.Issue_No + "' and Document_Type = 'MCC-AISSUE' and Auto_Sr_No not in (select Auto_Sr_No from  TSPL_SERIAL_ITEM st where st.Code>TSPL_SERIAL_ITEM.Code and Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "'  and Document_Code <> '" + obj.Issue_No + "' and Document_Type = 'MCC-AISSUE' aND In_Out_Type='I') group by Auto_Sr_No,Tag_no,Auto_Bin_No )xxx  Where 2=2  "
                                            Dim DtSerialItem As DataTable = clsDBFuncationality.GetDataTable(qrySerial, trans)
                                            If DtSerialItem IsNot Nothing AndAlso DtSerialItem.Rows.Count > 0 Then

                                                objTr.Issued_Qty_AgainstRet = objTr.Issued_Qty_AgainstRet + 1
                                                Dim objsri As New clsSerializeInvenotry
                                                objsri.Auto_Sr_No = strSerialNo
                                                objList.Add(objsri)

                                            Else
                                                Throw New Exception("Serialize balance is not Avilable for Item code " + clsCommon.myCstr(objTr.Item_Code) + " (" + strSerialNo + ") ")
                                            End If
                                        End If

                                    Next
                                End If
                                '''''''''''''''''''''''''''''''''''
                                If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                                    Dim dblIssueCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Unit_Cost  from  TSPL_ASSET_DISPATCH_RETAILER_DETAIL  left outer join TSPL_ASSET_DISPATCH_RETAILER_HEAD on TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = '" + obj.Issue_No + "' and TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code = '" + objTr.Item_Code + "' and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type = 'Issue'  ", trans))
                                    objTr.Unit_Cost = dblIssueCost
                                    objTr.Amount = dblIssueCost * objTr.Issued_Qty_AgainstRet
                                Else
                                    objTr.Amount = (clsCommon.myCdbl(dtItemData.Rows(0)("Unit Cost")) * objTr.Issued_Qty)
                                End If
                                objTr.Item_Net_Amt = objTr.Amount

                                'Check Balance
                                If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                                    'objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(dtItem.Rows(k)("Return Qty"))
                                    Dim BalQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(aa.Qty * RI) AS BalQty from (" &
                   " select  Item_Code, Issued_Qty As Qty,1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL where TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No='" & obj.Issue_No & "' " &
                   "  Union All " &
                   " select  Item_Code, Issued_Qty_againstret As Qty,-1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL " &
                   " LEFT OUTER JOIN TSPL_ASSET_DISPATCH_RETAILER_HEAD ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No " &
                   " where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No='" & obj.Issue_No & "'  " &
                   " )  aa " &
                   " group by item_Code HAVING item_Code='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    If BalQty <= 0 Then
                                        Throw New Exception("Item code " + clsCommon.myCstr(objTr.Item_Code) + " balance qty zero or less then zero  ")
                                    Else
                                        objTr.Issued_Qty = BalQty
                                        If BalQty < objTr.Issued_Qty_AgainstRet Then
                                            Throw New Exception("Item code " + clsCommon.myCstr(objTr.Item_Code) + " balance = " + clsCommon.myCstr(BalQty) + " and Return Qty = " + "" + clsCommon.myCstr(objTr.Issued_Qty_AgainstRet) + "")
                                        End If
                                    End If
                                Else
                                    Dim BalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(Item_Code, Frm_Location, "", clsCommon.GETSERVERDATE(trans), trans, UOM)
                                    If objTr.Issued_Qty > BalQty Then
                                        Throw New Exception("Balance is not Avilable.Item code = " + clsCommon.myCstr(objTr.Item_Code) + " balance = " + clsCommon.myCstr(BalQty) + " and Issue Qty = " + "" + clsCommon.myCstr(objTr.Issued_Qty) + " in Location (" + Frm_Location + ")")
                                    End If
                                End If
                                'Check Balance

                                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                                    objTr.Issued_Qty_AgainstRet = 0
                                End If

                                objTr.arrSrItem = TryCast(objList, List(Of clsSerializeInvenotry))
                                If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
                                        obj.Arr.Add(objTr)
                                    End If
                                ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                                    If clsCommon.myLen(objTr.Item_Code) > 0 AndAlso clsCommon.myCdbl(objTr.Issued_Qty_AgainstRet) > 0 Then
                                        obj.Arr.Add(objTr)
                                    End If
                                End If


                                BeforeTax_Amt = BeforeTax_Amt + objTr.Amount
                                Doc_Amt = Doc_Amt + objTr.Amount

                            End If
                        Next


                        '         For j As Integer = 0 To dtCustData.Rows.Count - 1

                        '             Dim objTr As New clsAssetDispatchRetailerDetail()
                        '             objTr.Line_No = j + 1
                        '             objTr.Req_IssueNo = ""
                        '             objTr.Item_Code = clsCommon.myCstr(dtCustData.Rows(j)("Item Code"))
                        '             objTr.Item_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where ITEM_Code = '" + objTr.Item_Code + "'", trans))
                        '             objTr.Cost_Code = Nothing
                        '             objTr.Required_Qty = 0

                        '             objTr.Unit_code = clsCommon.myCstr(dtCustData.Rows(j)("UOM"))
                        '             objTr.Unit_Cost = clsCommon.myCdbl(dtCustData.Rows(j)("Unit Cost"))

                        '             'If clsCommon.CompairString(cboDocType.Text, "Return") = CompairStringResult.Equal Then
                        '             '    'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        '             '    objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                        '             '    objTr.Issued_Qty_AgainstRet = 0
                        '             '    'Else
                        '             '    '    objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                        '             '    '    objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        '             '    'End If
                        '             'Else
                        '             objTr.Issued_Qty = clsCommon.myCdbl(dtCustData.Rows(j)("Issue Qty"))
                        '             If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                        '                 objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(dtCustData.Rows(j)("Return Qty"))
                        '                 Dim BalQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(aa.Qty * RI) AS BalQty from (" &
                        '" select  Item_Code, Issued_Qty As Qty,1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL where TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No='" & obj.Issue_No & "' " &
                        '"  Union All " &
                        '" select  Item_Code, Issued_Qty_againstret As Qty,-1 as RI From TSPL_ASSET_DISPATCH_RETAILER_DETAIL " &
                        '" LEFT OUTER JOIN TSPL_ASSET_DISPATCH_RETAILER_HEAD ON TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No " &
                        '" where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_No='" & obj.Issue_No & "'  " &
                        '" )  aa " &
                        '" group by item_Code HAVING item_Code='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                        '                 If BalQty <= 0 Then
                        '                     Throw New Exception("Item code " + clsCommon.myCstr(objTr.Item_Code) + " balance qty zero or less then zero  ")
                        '                 Else
                        '                     objTr.Issued_Qty = BalQty
                        '                     If BalQty < objTr.Issued_Qty_AgainstRet Then
                        '                         Throw New Exception("Item code " + clsCommon.myCstr(objTr.Item_Code) + " balance = " + clsCommon.myCstr(BalQty) + " and Return Qty = " + "" + clsCommon.myCstr(objTr.Issued_Qty_AgainstRet) + "")
                        '                     End If
                        '                 End If
                        '             Else
                        '                 Dim BalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(Item_Code, Frm_Location, "", clsCommon.GETSERVERDATE(trans), trans, UOM)
                        '                 If objTr.Issued_Qty > BalQty Then
                        '                     Throw New Exception("Balance is not Avilable.Item code = " + clsCommon.myCstr(objTr.Item_Code) + " balance = " + clsCommon.myCstr(BalQty) + " and Issue Qty = " + "" + clsCommon.myCstr(objTr.Issued_Qty) + " in Location (" + Frm_Location + ")")
                        '                 End If
                        '             End If

                        '             'End If
                        '             'objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                        '             'objTr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                        '             'objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                        '             'objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                        '             'objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                        '             'objTr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                        '             'objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                        '             'objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                        '             'objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                        '             'objTr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                        '             'objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                        '             'objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                        '             'objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                        '             'objTr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                        '             'objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                        '             'objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                        '             'objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                        '             'objTr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                        '             'objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                        '             'objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                        '             'objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                        '             'objTr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                        '             'objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                        '             'objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                        '             'objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                        '             'objTr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                        '             'objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                        '             'objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                        '             'objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                        '             'objTr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                        '             'objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                        '             'objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                        '             'objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                        '             'objTr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                        '             'objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                        '             'objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                        '             'objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                        '             'objTr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                        '             'objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                        '             'objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                        '             If clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                        '                 Dim dblIssueCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Unit_Cost  from  TSPL_ASSET_DISPATCH_RETAILER_DETAIL  left outer join TSPL_ASSET_DISPATCH_RETAILER_HEAD on TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Doc_No where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No = '" + obj.Issue_No + "' and TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code = '" + objTr.Item_Code + "' and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type = 'Issue'  ", trans))
                        '                 objTr.Unit_Cost = dblIssueCost
                        '                 objTr.Amount = dblIssueCost * objTr.Issued_Qty_AgainstRet
                        '             Else
                        '                 objTr.Amount = clsCommon.myCdbl(dtCustData.Rows(j)("Unit Cost")) * clsCommon.myCdbl(dtCustData.Rows(j)("Issue Qty"))
                        '             End If

                        '             objTr.Total_Tax_Amt = 0
                        '             objTr.Item_Net_Amt = objTr.Amount

                        '             objTr.EMI_Asset_Value = 0
                        '             objTr.EMI_No_Of_Payment_Cycle = 0

                        '             Dim Is_Serial_Item As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ITEM_MASTER where Is_Serial_Item =1 and Item_code = '" + objTr.Item_Code + "'   ", trans))
                        '             objTr.arrSrItem = Nothing
                        '             'Dim ItemList As New List(Of clsSerializeInvenotry)

                        '             '=============
                        '             Dim objList As New List(Of clsSerializeInvenotry)




                        '             '=============
                        '             If Is_Serial_Item = True Then
                        '                 If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then


                        '                     Dim qrySerial As String = " select Auto_Sr_No as AutoSerialNo,Auto_Bin_No,Tag_no from ( select Auto_Sr_No,Auto_Bin_No,Tag_no ,max(Document_Date) as Document_Date  from TSPL_SERIAL_ITEM LEFT JOIN TSPL_VISI_MASTER ON Serial_No=Auto_Sr_No  where Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "' and not( Document_Code = '' and Document_Type = '') group by Auto_Sr_No,Auto_Bin_No,Tag_no having sum(case when In_Out_Type='I' and Against_Inv_Movement_Trans_Id is not null then 1 else case when In_Out_Type='O' then -1 else 0 end end)>0   )xxx  Where 2=2  "
                        '                     Dim DtSerialItem As DataTable = clsDBFuncationality.GetDataTable(qrySerial, trans)
                        '                     If DtSerialItem IsNot Nothing AndAlso DtSerialItem.Rows.Count > 0 Then
                        '                         If DtSerialItem.Rows.Count >= objTr.Issued_Qty Then
                        '                             For q As Integer = 0 To objTr.Issued_Qty - 1
                        '                                 Dim objsri As New clsSerializeInvenotry
                        '                                 objsri.Auto_Sr_No = clsCommon.myCstr(DtSerialItem.Rows(q)("AutoSerialNo"))
                        '                                 objList.Add(objsri)
                        '                             Next
                        '                         Else
                        '                             Throw New Exception("Serialize balance is not Avilable for Item code " + clsCommon.myCstr(objTr.Item_Code) + " ")
                        '                         End If
                        '                     End If
                        '                 Else
                        '                     Dim qrySerial As String = " select Auto_Sr_No as AutoSerialNo,Auto_Bin_No,Tag_no from ( select Auto_Sr_No,Auto_Bin_No,Tag_no ,max(Document_Date) as Document_Date  from TSPL_SERIAL_ITEM LEFT  JOIN TSPL_VISI_MASTER ON Serial_No=Auto_Sr_No  where Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "' and   Document_Code = '" + obj.Issue_No + "' and Document_Type = 'MCC-AISSUE' and Auto_Sr_No not in (select Auto_Sr_No from  TSPL_SERIAL_ITEM st where st.Code>TSPL_SERIAL_ITEM.Code and Item_Code='" + objTr.Item_Code + "' and Location_Code='" + obj.From_Location + "'  and Document_Code <> '" + obj.Issue_No + "' and Document_Type = 'MCC-AISSUE' aND In_Out_Type='I') group by Auto_Sr_No,Tag_no,Auto_Bin_No )xxx  Where 2=2  "
                        '                     Dim DtSerialItem As DataTable = clsDBFuncationality.GetDataTable(qrySerial, trans)
                        '                     If DtSerialItem IsNot Nothing AndAlso DtSerialItem.Rows.Count > 0 Then
                        '                         If DtSerialItem.Rows.Count >= objTr.Issued_Qty_AgainstRet Then
                        '                             For q As Integer = 0 To objTr.Issued_Qty_AgainstRet - 1
                        '                                 Dim objsri As New clsSerializeInvenotry
                        '                                 objsri.Auto_Sr_No = clsCommon.myCstr(DtSerialItem.Rows(q)("AutoSerialNo"))
                        '                                 objList.Add(objsri)
                        '                             Next
                        '                         Else
                        '                             Throw New Exception("Serialize balance is not Avilable for Item code " + clsCommon.myCstr(objTr.Item_Code) + " ")
                        '                         End If
                        '                     End If
                        '                 End If

                        '             End If


                        '             objTr.arrSrItem = TryCast(objList, List(Of clsSerializeInvenotry))
                        '             If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                        '                 If clsCommon.myLen(objTr.Item_Code) > 0 Then
                        '                     obj.Arr.Add(objTr)
                        '                 End If
                        '             ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                        '                 If clsCommon.myLen(objTr.Item_Code) > 0 AndAlso clsCommon.myCdbl(objTr.Issued_Qty_AgainstRet) > 0 Then
                        '                     obj.Arr.Add(objTr)
                        '                 End If
                        '             End If


                        '             BeforeTax_Amt = BeforeTax_Amt + objTr.Amount
                        '             Doc_Amt = Doc_Amt + objTr.Amount
                        '         Next

                        obj.BeforeTax_Amt = BeforeTax_Amt
                        obj.doc_Amt = Doc_Amt

                        'If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                        '    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                        '        common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
                        '        Return
                        '    End If
                        'ElseIf clsCommon.CompairString(obj.Doc_Type, "Return") = CompairStringResult.Equal Then
                        '    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                        '        common.clsCommon.MyMessageBoxShow("Please return qty for at least one item")
                        '        Return
                        '    End If
                        'End If


                        ''For Custom Fields
                        'obj.Form_ID = MyBase.Form_ID
                        'obj.arrCustomFields = New List(Of clsCustomFieldValues)
                        'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        '    UcCustomFields1.GetData(obj.arrCustomFields)
                        'End If
                        'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                        '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colAssetCode)
                        'End If
                        ''End of For Custom Fields

                        If (obj.SaveData(obj, True, trans)) = False Then
                            trans.Rollback()
                        End If
                    End If
                    '======================================================================

                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Successfully Imported.", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmUploderBlankSheet_Click(sender As Object, e As EventArgs) Handles rmUploderBlankSheet.Click
        Try
            Dim Str As String = " select TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type as [Doc Type], TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_no as [Document Code],Doc_date as [Document Date],Issue_No as [Issue No],From_Location as [From Location],
                             Location_Desc as [From Locattion Name],isnull(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,'') as [VLC Uploader Code] ,Issue_to as [Issued To],Vendor_Name as [Issued To Name],Request_by as [Requested By],Emp_Name as
                            [Requested By Name],Comment,Remarks,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_code as [Item Code],Item_Desc as [Item Desc],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Cost_code as [Cost Center Code] 
                            ,cost_name as  [Cost Center Name],Unit_code as [UOM],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_Cost as [Unit Cost] ,Issued_Qty as [Issue Qty], Issued_Qty_againstret as [Return Qty],TSPL_ASSET_DISPATCH_RETAILER_HEAD.IS_LOST as IsLost,TSPL_SERIAL_ITEM.Auto_Sr_No as [Serial No] from TSPL_ASSET_DISPATCH_RETAILER_HEAD inner join TSPL_ASSET_DISPATCH_RETAILER_DETAIL on 
                             TSPL_ASSET_DISPATCH_RETAILER_DETAIL.doc_no=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No left join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.cost_code=
                             TSPL_ASSET_DISPATCH_RETAILER_DETAIL.cost_code left join tspl_location_master on Location_Code=From_Location left join tspl_vendor_master on
                             tspl_vendor_master.vendor_code=Issue_To left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=Request_By  
                             left join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Document_code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_no and TSPL_SERIAL_ITEM.Item_code =TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_code 
                             left join  TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_ASSET_DISPATCH_RETAILER_HEAD.Issue_to and  TSPL_VLC_MASTER_HEAD.MCC =  TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location       "
            transportSql.ExporttoExcel(Str, Me)
            Str = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtDistributor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtDistributor._MYValidating
        Dim Qry As String = "select Cust_Code ,Customer_Name from TSPL_CUSTOMER_MASTER "

        TxtDistributor.Value = clsCommon.ShowSelectForm("CUSTDISTA", Qry, "Cust_Code", "", TxtDistributor.Value, "Cust_Code", isButtonClicked)
        lblDistributor.Text = clsCommon.myCstr(clsCustomerMaster.GetName(TxtDistributor.Value, Nothing))
    End Sub

    Private Sub chkItem_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkItem.ToggleStateChanged
        LoadBlankGrid()
        LoadBlankGridTax()
        If gv1.Rows.Count <= 0 Then
            gv1.Rows.AddNew()
        End If

    End Sub

    Private Sub TxtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            If clsCommon.myLen(TxtDistributor.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Distributor First.", Me.Text)
                TxtDistributor.Focus()
                Exit Sub
            End If
            Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            Dim strWhere As String = ""
            If clsCommon.myLen(TxtDistributor.Value) > 0 Then
                qry += " inner join TSPL_Customer_Route_Master on TSPL_Customer_Route_Master.Route_No=TSPL_ROUTE_MASTER.Route_No "
                strWhere = " TSPL_Customer_Route_Master.cust_Code='" + TxtDistributor.Value + "'"
            End If
            txtRouteNo.Value = clsCommon.ShowSelectForm("ADRRoute1", qry, "Code", strWhere, txtRouteNo.Value, "", isButtonClicked)
            fndRouteNo_TextChanged()
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                TxtVehicle.Value = connectSql.RunScalar("Select vehicle_code from tspl_route_master where route_no = '" + txtRouteNo.Value + "'")
                If clsCommon.myLen(TxtVehicle.Value) > 0 Then
                    lblVehicleDesc.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + TxtVehicle.Value + "'")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndRouteNo_TextChanged()
        Try
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                Dim sql As String = "Select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"
                Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
                If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
                    lblRouteDesc.Text = dr1.Rows(0)(0).ToString()
                Else
                    lblRouteDesc.Text = String.Empty
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtAltVehicle__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtAltVehicle._MYValidating
        Try
            Dim qry As String = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
            txtAltVehicle.Value = clsCommon.ShowSelectForm("AltVehicleNo", qry, "vehicle_id", "", txtAltVehicle.Value, "vehicle_id", isButtonClicked)
            lblAltVehicleDesc.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtAltVehicle.Value) + "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

