'-Updation By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001258, BM00000002103, BM00000002938-16/07/2014]
'=changes by shivani [BM00000007922,BM00000008019,BM00000008015,BM00000008031,BM00000008030,BM00000008029,BM00000008028,BM00000008100,UDL/17/04/2019-000290]
' work done agast ticket no. UDL/27/09/18-000225
'==create script [BHA/30/11/18-000732,BHA/04/12/18-000739]preeti 
Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.Enumerations
Imports System.Data.SqlClient
'' Work to be done agaist ticket no.UDL/24/09/18-000223 
'Ticket No-UDL/21/02/19-000269
'Ticket No-ERO/05/11/19-001087,non editable location if we select the purchase invoice no
Public Class frmAcquisionEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim i As Integer
    Dim strTotalTaxAmount As Double = 0.0
    Private isCellValueChangedTaxOpen As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private IsFormLoad As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private objRemittance As clsRemittance
    Const colLineNo As String = "COLLNO"
    Const colcheck As String = "colcheck"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colAssetSpecificaion As String = "colAssetSpecificaion"
    Const colDepTaxRate As String = "colDepTaxRate"
    Const colDepreciatedvalue As String = "coldepre_value"

    Const colAssetID As String = "colAssetID"
    Const colOldAssetID As String = "colOldAssetID"
    Const colAssetSerialID As String = "colAssetSerialID"

    Const colAssetName As String = "colAssetName"
    Const colTemplete As String = "colTemplete"
    Const colTempleteName As String = "colTempleteName"
    Const colAcquisitionDate As String = "colAcquisitionDate"
    Const colDepMethod As String = "colDepMethod"
    Const colDepMethodName As String = "colDepMethodName"
    Const colDepMethodTax As String = "colDepMethodTax"
    Const colDepMethodNameTax As String = "colDepMethodNameTax"

    Const colCategoryCode As String = "colCategoryCode"
    Const colCategoryName As String = "colCategoryName"
    Const colGroupCode As String = "colGroupCode"
    Const colGroupName As String = "colGroupName"
    Const colHierarchyCode As String = "colHierarchyCode"
    Const colHierarchyName As String = "colHierarchyName"
    Const colHierarchyLevelNumber As String = "colHierarchyLevelNumber"
    Const colCostCenterCode As String = "colCostCenterCode"
    Const colCostCenterName As String = "colCostCenterName"
    Const colAccountSetCode As String = "colAccountSetCode"
    Const colAccountSetName As String = "colAccountSetName"

    Const colDepPeriodCode As String = "colDepPeriodCode"
    Const colDepPeriodName As String = "colDepPeriodName"
    Const colPutToUse As String = "colPutToUse"
    Const colStartDate As String = "colStartDate"
    Const colDepRate As String = "colDepRate"
    Const ColBookEstimatedLife As String = "ColBookEstimatedLife"
    Const ColBookSourceValue As String = "ColBookSourceValue"

    Const ColBookSourceOriginalValue As String = "ColBookSourceOriginalValue"
    Const ColBookSalvageValue As String = "ColBookSalvageValue"
    Const ColBookSalvageRate As String = "ColBookSalvageRate"


    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colIsAssembled As String = "colIsAssembled"

    Const colTotTaxAmt As String = "colTotTaxAmt"
    Const colNetAmt As String = "colNetAmt"

    Const colSRNQty As String = "colSRNQty"
    Const colSRNRate As String = "colSRNRate"
    Const colPI As String = "colPI"
    Const colSRNNo As String = "colSRNNo"
    Const colUOM As String = "colUOM"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    Const colBookDepType As String = "colBookDepType"
    Const colTaxDepType As String = "colTaxDepType"

    'Dim IsSRNMandatory As Boolean = False
    '===for Assembly Grid
    Const colIcode1 As String = "colIcode1"
    Const colIDesc1 As String = "colIDesc1"
    Const colIssueNo1 As String = "colIssueNo1"
    Const colCostCenter1 As String = "colCostCenter1"
    Const colUOM1 As String = "colUOM1"
    Const colReceivedQty1 As String = "colReceivedQty1"
    Const colRate1 As String = "colRate1"
    Const colAmount1 As String = "colAmount1"
    '=======================
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public objSRN_ACQUTSN As New clsAcquisitionHead()
    'Dim UDLCapexAcquisionEntry As Boolean = False

    Const colAssetsCode As String = "colAssetsCode"
    Const colAssetsExps As String = "colAssetsExps"
    Const colAssetsAmt As String = "colAssetsAmt"

    '=============added by preeti gupta=====
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim ApplyFinancialCostCenter As Boolean = False
    Dim FixedAssetAcquisitionEntryHitInventoryMovement As Boolean = False
    Const colIsCategory As String = "colIsCategory"
    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"

    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"

    '================end=========================
    ''=================Preeti[13/04/2017]==========================
    Const colItemACCode1 As String = "COLItemACCODE1"
    Const colItemACAmount1 As String = "COLItemACAMOUNT1"
    Const colItemACCalcAmount1 As String = "COLItemACCalcAMOUNT1"

    Const colItemACCode2 As String = "COLItemACCODE2"
    Const colItemACAmount2 As String = "COLItemACAMOUNT2"
    Const colItemACCalcAmount2 As String = "COLItemACCalcAMOUNT2"

    Const colItemACCode3 As String = "COLItemACCODE3"
    Const colItemACAmount3 As String = "COLItemACAMOUNT3"
    Const colItemACCalcAmount3 As String = "COLItemACCalcAMOUNT3"

    Const colItemACCode4 As String = "COLItemACCODE4"
    Const colItemACAmount4 As String = "COLItemACAMOUNT4"
    Const colItemACCalcAmount4 As String = "COLItemACCalcAMOUNT4"

    Const colItemACCode5 As String = "COLItemACCODE5"
    Const colItemACAmount5 As String = "COLItemACAMOUNT5"
    Const colItemACCalcAmount5 As String = "COLItemACCalcAMOUNT5"

    Const colItemACCode6 As String = "COLItemACCODE6"
    Const colItemACAmount6 As String = "COLItemACAMOUNT6"
    Const colItemACCalcAmount6 As String = "COLItemACCalcAMOUNT6"

    Const colItemACCode7 As String = "COLItemACCODE7"
    Const colItemACAmount7 As String = "COLItemACAMOUNT7"
    Const colItemACCalcAmount7 As String = "COLItemACCalcAMOUNT7"

    Const colItemACCode8 As String = "COLItemACCODE8"
    Const colItemACAmount8 As String = "COLItemACAMOUNT8"
    Const colItemACCalcAmount8 As String = "COLItemACCalcAMOUNT8"

    Const colItemACCode9 As String = "COLItemACCODE9"
    Const colItemACAmount9 As String = "COLItemACAMOUNT9"
    Const colItemACCalcAmount9 As String = "COLItemACCalcAMOUNT9"

    Const colItemACCode10 As String = "COLItemACCODE10"
    Const colItemACAmount10 As String = "COLItemACAMOUNT10"
    Const colItemACCalcAmount10 As String = "COLItemACCalcAMOUNT10"
    Const colItemTotalAdditionalCharge As String = "ColItemAdditionalCHarge"

    Const colTaxRecoverable As String = "colTaxRecoverable"
    Const colTaxNonRecoverable As String = "colTaxNonRecoverable"
    ''==================================================================
    ''gvAssetAssemble
    Const colType As String = "colType"
    Const colDocumentCode As String = "colDocumentCode"
    Const colDocumentDate As String = "colDocumentDate"
    Const colItemNo As String = "colItemNo"
    Const colItemDesc As String = "colItemDesc"
    Const colHierarchy As String = "colHierarchy"
    Const colCostCenter As String = "colCostCenter"
    Const colAmount As String = "colAmount"
    Const colTotalTaxAmount As String = "colTotalTaxAmount"
    Const colItemNetAmount As String = "colItemNetAmount"
    Const colDistributeAmount As String = "colDistributeAmount"
    Const colTotalAmount As String = "colTotalAmount"
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False
    Dim ImportMultipleAssetAssembled As Boolean = False
    Dim CostCenterAndHirerachyCodeUpdateAfterPost As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FAAcquisitionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TSPL_ACQUISITION_DETAIL
        'Try
        '    Dim chkValuesDetail As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(OBJECT_ID) AS TotalTables FROM sys.tables where name='TSPL_ACQUISITION_DETAIL'"))
        '    If chkValuesDetail = 1 Then
        '        Dim QryForeign As String = clsDBFuncationality.getSingleValue("SELECT  A.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS A, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE B WHERE CONSTRAINT_TYPE = 'FOREIGN KEY' AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME and a.TABLE_NAME='TSPL_ACQUISITION_DETAIL' and b.COLUMN_NAME='CostCenter_Code' ORDER BY A.TABLE_NAME")
        '        If clsCommon.myLen(QryForeign) > 0 Then
        '            clsDBFuncationality.ExecuteNonQuery("alter table TSPL_ACQUISITION_DETAIL drop constraint " & QryForeign & "")
        '        End If
        '    End If
        'Catch ex As Exception
        'End Try
        SetUserMgmtNew()
        txtVendorNo.MendatroryField = True
        lblSRNDate.Visible = True
        lblPIDate.Visible = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")


        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Collapsed
        LoadBlankGrid()
        LoadBlankGridTax()
        LoadBlankGridAC()
        LoadBlankGridAssemble()
        IsFormLoad = True
        '===============================Added by preeti gupta========================================================
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapexForFA, clsFixedParameterCode.ShowOptionforSelectingCapexForFA, Nothing)) = "1", True, False))
        ApplyFinancialCostCenter = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, Nothing)) = "1", True, False))
        '============================================================END==================================================================
        '-----------Parteek Added function 21/03/217
        'UDLCapexAcquisionEntry = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UDLCapexAcquisionEntry, clsFixedParameterCode.UDLCapexAcquisionEntry, Nothing)) = "1", True, False))
        'If UDLCapexAcquisionEntry = True Then
        '    fndcapexcode.Visible = True
        '    fndcapexsubcode.Visible = True
        '    lbl_capexcode.Visible = True
        '    lbl_capexsubcode.Visible = True
        '    lblcaption2.Visible = True
        '    lblcaption1.Visible = True
        '    lbl_budgetamt.Visible = True
        '    lbl_budgetamtwithtolerence.Visible = True
        '    lbl_rebudgetamt.Visible = True
        '    lbl_rebudgetamtwithtolerence.Visible = True
        '    MyLabel35.Visible = True
        '    MyLabel40.Visible = True
        '    MyLabel38.Visible = True
        '    MyLabel37.Visible = True
        'End If
        CostCenterAndHirerachyCodeUpdateAfterPost = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CostCenterAndHirerachyCodeUpdateAfterPost, clsFixedParameterCode.CostCenterAndHirerachyCodeUpdateAfterPost, Nothing)) = 1, True, False)

        FixedAssetAcquisitionEntryHitInventoryMovement = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FixedAssetAcquisitionEntryHitInventoryMovement, clsFixedParameterCode.FixedAssetAcquisitionEntryHitInventoryMovement, Nothing)) = "1", True, False))
        If FixedAssetAcquisitionEntryHitInventoryMovement = True Then
            btnShowInventory.Visible = True
        Else
            btnShowInventory.Visible = False
        End If
        AddNew()
        'ToolStrip1.LayoutStyle = ToolStripLayoutStyle.Table
        SetLength()

        IsFormLoad = False
        txtTaxGroup.Enabled = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        'txtDocNo.MyMaxLength = 30
        'txtDesc.MaxLength = 200
        'txtRemarks.MaxLength = 200
        'txtComment.MaxLength = 200
        'cboModeOfTransport.MaxLength = 12
        'cboPOType.MaxLength = 1
        'cboItemType.MaxLength = 1
    End Sub

    Sub BlankAllControls()
        ddlAcqType.SelectedIndex = -1
        ddlAcqType.Text = "Asset"
        rbtnnew.IsChecked = True
        rbtnold.IsChecked = False
        rbtnnew.Enabled = True
        rbtnold.Enabled = True

        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDesc.Text = ""
        txtRemarks.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        lblTotalAmt.Text = ""
        lblTaxAmt.Text = ""
        lblNetAmt.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtSRNNo.Value = ""
        fndPINo.Value = ""
        rbtnTaxCalAutomatic.IsChecked = True
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtVendorInvoiceNo.Text = ""

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repocheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repocheck = New GridViewCheckBoxColumn()
        repocheck.FormatString = ""
        repocheck.HeaderText = "Save"
        repocheck.Name = colcheck
        repocheck.Width = 40
        repocheck.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repocheck)

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "S No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoAssetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetName.FormatString = ""
        repoAssetName.HeaderText = "Asset Description"
        repoAssetName.Name = colAssetName
        repoAssetName.Width = 150
        repoAssetName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAssetName)

        Dim repoAssetSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetSpecification.FormatString = ""
        repoAssetSpecification.HeaderText = "Asset Specification"
        repoAssetSpecification.Name = colAssetSpecificaion
        repoAssetSpecification.Width = 150
        repoAssetSpecification.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAssetSpecification)

        Dim repoAssetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Code"
        repoAssetCode.Name = colAssetID
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 100
        repoAssetCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAssetCode)

        '=================ONLY FOR CALCULATION PURPOSE
        Dim repoOldAssetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOldAssetCode.FormatString = ""
        repoOldAssetCode.HeaderText = "Old Asset Code"
        repoOldAssetCode.Name = colOldAssetID
        repoOldAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoOldAssetCode.Width = 100
        repoOldAssetCode.ReadOnly = False
        repoOldAssetCode.IsVisible = False
        repoOldAssetCode.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoOldAssetCode)

        repoAssetCode = New GridViewTextBoxColumn()
        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Serial No"
        repoAssetCode.Name = colAssetSerialID
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 100
        repoAssetCode.ReadOnly = False
        repoAssetCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAssetCode)

        Dim repobookDepType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobookDepType.FormatString = ""
        repobookDepType.HeaderText = "Book Dep. Type"
        repobookDepType.Name = colBookDepType
        repobookDepType.TextImageRelation = TextImageRelation.TextBeforeImage
        repobookDepType.Width = 100
        repobookDepType.ReadOnly = True
        repobookDepType.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repobookDepType)

        Dim repoTaxDepType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxDepType.FormatString = ""
        repoTaxDepType.HeaderText = "Tax Dep.Type"
        repoTaxDepType.Name = colTaxDepType
        repoTaxDepType.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTaxDepType.Width = 100
        repoTaxDepType.ReadOnly = True
        repoTaxDepType.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxDepType)



        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        '  repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.ReadOnly = True
        repoICode.Width = 100
        repoICode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoTemplate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTemplate.FormatString = ""
        repoTemplate.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Category Code", "Template Code")
        repoTemplate.Name = colTemplete
        repoTemplate.Width = 100
        repoTemplate.ReadOnly = True
        repoTemplate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTemplate)

        Dim repoTempleteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTempleteName.FormatString = ""
        repoTempleteName.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Category Name", "Template Name")
        repoTempleteName.Name = colTempleteName
        repoTempleteName.Width = 150
        repoTempleteName.ReadOnly = True
        repoTempleteName.IsVisible = True
        repoTempleteName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoTempleteName)


        Dim repoCategoryCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCategoryCode.FormatString = ""
        repoCategoryCode.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Group Code", "Category Code")
        repoCategoryCode.Name = colCategoryCode
        repoCategoryCode.Width = 100
        repoCategoryCode.ReadOnly = True
        repoCategoryCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCategoryCode)

        Dim repoCategoryName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCategoryName.FormatString = ""
        repoCategoryName.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Group Name", "Category")
        repoCategoryName.Name = colCategoryName
        repoCategoryName.Width = 150
        repoCategoryName.ReadOnly = True
        repoCategoryName.IsVisible = True
        repoCategoryName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoCategoryName)

        Dim repoGroupCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCode.FormatString = ""
        repoGroupCode.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Sub Group Code", "Group Code")
        repoGroupCode.Name = colGroupCode
        repoGroupCode.Width = 100
        repoGroupCode.ReadOnly = True
        repoGroupCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoGroupCode)

        Dim repoGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroup.FormatString = ""
        repoGroup.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Sub Group Name", "Group")
        repoGroup.Name = colGroupName
        repoGroup.Width = 150
        repoGroup.ReadOnly = True
        repoGroup.IsVisible = True
        repoGroup.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoGroup)

        '==================================================================================

        Dim repoHierarchyLevelCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoHierarchyLevelCode.FormatString = ""
            repoHierarchyLevelCode.HeaderText = "Hierarchy Level Code"
            repoHierarchyLevelCode.Name = colHierarchyCode
            repoHierarchyLevelCode.Width = 100
        repoHierarchyLevelCode.ReadOnly = False
        If ApplyFinancialCostCenter = True Then
            repoHierarchyLevelCode.IsVisible = True
        Else
            repoHierarchyLevelCode.IsVisible = False
        End If

        gv1.MasterTemplate.Columns.Add(repoHierarchyLevelCode)


            Dim repoHierarchyLevelName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoHierarchyLevelName.FormatString = ""
            repoHierarchyLevelName.HeaderText = "Hierarchy Level Name"
            repoHierarchyLevelName.Name = colHierarchyName
            repoHierarchyLevelName.Width = 150
        repoHierarchyLevelName.ReadOnly = True
        If ApplyFinancialCostCenter = True Then
            repoHierarchyLevelName.IsVisible = True
        Else
            repoHierarchyLevelName.IsVisible = False
        End If

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



        '==================================================================================
        Dim repoCostCenterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterCode.FormatString = ""
        repoCostCenterCode.HeaderText = "Cost Center Code"
        repoCostCenterCode.Name = colCostCenterCode
        repoCostCenterCode.Width = 100
        repoCostCenterCode.ReadOnly = False
        repoCostCenterCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCostCenterCode)

        Dim repoCostCenterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterName.FormatString = ""
        repoCostCenterName.HeaderText = "Cost Center"
        repoCostCenterName.Name = colCostCenterName
        repoCostCenterName.Width = 150
        repoCostCenterName.ReadOnly = True
        repoCostCenterName.IsVisible = True
        repoCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoCostCenterName)

        Dim repoAccountSetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAccountSetCode.FormatString = ""
        repoAccountSetCode.HeaderText = "Account Set Code"
        repoAccountSetCode.Name = colAccountSetCode
        repoAccountSetCode.Width = 100
        repoAccountSetCode.ReadOnly = True
        repoAccountSetCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoAccountSetCode)

        Dim repoAccountSetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAccountSetName.FormatString = ""
        repoAccountSetName.HeaderText = "Account Set"
        repoAccountSetName.Name = colAccountSetName
        repoAccountSetName.Width = 150
        repoAccountSetName.ReadOnly = True
        repoAccountSetName.IsVisible = True
        repoAccountSetName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoAccountSetName)

        Dim repoAcquisitionDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoAcquisitionDate.Format = DateTimePickerFormat.Custom
        repoAcquisitionDate.CustomFormat = "dd-MM-yyyy"
        repoAcquisitionDate.HeaderText = "Acquisition Date"
        repoAcquisitionDate.FormatString = "{0:d}"
        repoAcquisitionDate.Name = colAcquisitionDate
        repoAcquisitionDate.WrapText = True
        repoAcquisitionDate.ReadOnly = True
        repoAcquisitionDate.IsVisible = True
        repoAcquisitionDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoAcquisitionDate)




        Dim repoDepCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepCode.FormatString = ""
        repoDepCode.HeaderText = "Depreciation Method Code"
        repoDepCode.Name = colDepMethod
        repoDepCode.Width = 100
        repoDepCode.ReadOnly = True
        repoDepCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoDepCode)

        Dim repoDepName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepName.FormatString = ""
        repoDepName.HeaderText = "Depreciation Method "
        repoDepName.Name = colDepMethodName
        repoDepName.Width = 150
        repoDepName.ReadOnly = True
        repoDepName.IsVisible = True
        repoDepName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDepName)

        Dim repoDepCodeTax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepCodeTax.FormatString = ""
        repoDepCodeTax.HeaderText = "Depreciation Method Tax Code"
        repoDepCodeTax.Name = colDepMethodTax
        repoDepCodeTax.Width = 100
        repoDepCodeTax.ReadOnly = True
        repoDepCodeTax.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoDepCodeTax)

        Dim repoDepNametax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepNametax.FormatString = ""
        repoDepNametax.HeaderText = "Depreciation Method Tax"
        repoDepNametax.Name = colDepMethodNameTax
        repoDepNametax.Width = 150
        repoDepNametax.ReadOnly = True
        repoDepNametax.IsVisible = True
        repoDepNametax.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDepNametax)

        Dim repoDepPeriodCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepPeriodCode.FormatString = ""
        repoDepPeriodCode.HeaderText = "Depreciation Period Code"
        repoDepPeriodCode.Name = colDepPeriodCode
        repoDepPeriodCode.Width = 100
        repoDepPeriodCode.ReadOnly = True
        repoDepPeriodCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoDepPeriodCode)

        Dim repoDepPeriodName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepPeriodName.FormatString = ""
        repoDepPeriodName.HeaderText = "Depreciation Method "
        repoDepPeriodName.Name = colDepPeriodName
        repoDepPeriodName.Width = 150
        repoDepPeriodName.ReadOnly = True
        repoDepPeriodName.IsVisible = True
        repoDepPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDepPeriodName)


        Dim repoPutToUse As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPutToUse.FormatString = ""
        repoPutToUse.HeaderText = "Put To Use"
        repoPutToUse.Name = colPutToUse
        repoPutToUse.Width = 100
        repoPutToUse.ReadOnly = True
        repoPutToUse.IsVisible = True
        repoPutToUse.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoPutToUse)

        Dim repoStartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoStartDate.Format = DateTimePickerFormat.Custom
        repoStartDate.CustomFormat = "dd-MM-yyyy"
        repoStartDate.HeaderText = "Put To Use Date"
        repoStartDate.FormatString = "{0:d}"
        repoStartDate.Name = colStartDate
        repoStartDate.WrapText = True
        repoStartDate.ReadOnly = True
        repoStartDate.IsVisible = True
        repoStartDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoStartDate)

        Dim repoDepTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDepTaxRate.FormatString = ""
        repoDepTaxRate.HeaderText = "Depreciation Tax Rate"
        repoDepTaxRate.Name = colDepTaxRate
        repoDepTaxRate.ReadOnly = True
        repoDepTaxRate.IsVisible = True
        repoDepTaxRate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDepTaxRate)

        Dim repoDepRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDepRate.FormatString = ""
        repoDepRate.HeaderText = "Depreciation Rate"
        repoDepRate.Name = colDepRate
        repoDepRate.ReadOnly = True
        repoDepRate.IsVisible = True
        repoDepPeriodName.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDepRate)

        Dim repoDepreciatedvalue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDepreciatedvalue.FormatString = ""
        repoDepreciatedvalue.HeaderText = "Depreciation Value"
        repoDepreciatedvalue.Name = colDepreciatedvalue
        repoDepreciatedvalue.ReadOnly = False
        repoDepreciatedvalue.IsVisible = False
        repoDepreciatedvalue.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDepreciatedvalue)

        Dim repoBookEstimatedLife As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookEstimatedLife.FormatString = ""
        repoBookEstimatedLife.HeaderText = "Book Estimated Life"
        repoBookEstimatedLife.Name = ColBookEstimatedLife
        repoBookEstimatedLife.ReadOnly = True
        repoBookEstimatedLife.IsVisible = False
        repoDepPeriodName.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBookEstimatedLife)

        Dim repoBookSourceValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSourceValue.FormatString = ""
        repoBookSourceValue.HeaderText = "Book Source Value"
        repoBookSourceValue.Name = ColBookSourceValue
        repoBookSourceValue.ReadOnly = True
        repoBookSourceValue.IsVisible = True
        repoBookSourceValue.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBookSourceValue)


        Dim repoBookSourceOriginalValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSourceOriginalValue.FormatString = ""
        repoBookSourceOriginalValue.HeaderText = "Book source Original Value"
        repoBookSourceOriginalValue.Name = ColBookSourceOriginalValue
        repoBookSourceOriginalValue.ReadOnly = True
        repoBookSourceOriginalValue.IsVisible = True
        repoBookSourceOriginalValue.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBookSourceOriginalValue)


        Dim repoBookSalvageRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSalvageRate.FormatString = ""
        repoBookSalvageRate.HeaderText = "Salvage %"
        repoBookSalvageRate.Name = ColBookSalvageRate
        repoBookSalvageRate.ReadOnly = True
        repoBookSalvageRate.IsVisible = True
        repoBookSalvageRate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBookSalvageRate)

        Dim repoBookSalvageValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSalvageValue.FormatString = ""
        repoBookSalvageValue.HeaderText = "Book Salvage Value"
        repoBookSalvageValue.Name = ColBookSalvageValue
        repoBookSalvageValue.ReadOnly = True
        repoBookSalvageValue.IsVisible = True
        repoBookSalvageValue.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBookSalvageValue)

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
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoTotalAddationalAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAddationalAmt10.WrapText = True
        repoTotalAddationalAmt10.ReadOnly = True
        repoTotalAddationalAmt10.FormatString = "{0:n2}"
        repoTotalAddationalAmt10.Width = 100
        repoTotalAddationalAmt10.HeaderText = "Total ItemAdditional Amt"
        repoTotalAddationalAmt10.Name = colItemTotalAdditionalCharge
        repoTotalAddationalAmt10.IsVisible = True
        repoTotalAddationalAmt10.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoTotalAddationalAmt10)

        Dim repoTaxRecovearbleAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRecovearbleAmt.WrapText = True
        repoTaxRecovearbleAmt.ReadOnly = True
        repoTaxRecovearbleAmt.FormatString = "{0:n2}"
        repoTaxRecovearbleAmt.Width = 100
        repoTaxRecovearbleAmt.HeaderText = "Tax Recoverable Amt"
        repoTaxRecovearbleAmt.Name = colTaxRecoverable
        repoTaxRecovearbleAmt.IsVisible = True
        repoTaxRecovearbleAmt.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoTaxRecovearbleAmt)

        Dim repoTaxNonRecovearbleAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxNonRecovearbleAmt.WrapText = True
        repoTaxNonRecovearbleAmt.ReadOnly = True
        repoTaxNonRecovearbleAmt.FormatString = "{0:n2}"
        repoTaxNonRecovearbleAmt.Width = 100
        repoTaxNonRecovearbleAmt.HeaderText = "Tax Non Recoverable Amt"
        repoTaxNonRecovearbleAmt.Name = colTaxNonRecoverable
        repoTaxNonRecovearbleAmt.IsVisible = True
        repoTaxNonRecovearbleAmt.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoTaxNonRecovearbleAmt)

        Dim repoNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetAmt = New GridViewDecimalColumn()
        repoNetAmt.FormatString = ""
        repoNetAmt.HeaderText = "Net Amount"
        repoNetAmt.Name = colNetAmt
        repoNetAmt.WrapText = True
        repoNetAmt.Width = 80
        repoNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNetAmt)

        '===
        Dim repoSRNNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRNNo.FormatString = ""
        repoSRNNo.HeaderText = "SRN No"
        repoSRNNo.Name = colSRNNo

        repoSRNNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSRNNo.ReadOnly = True
        repoSRNNo.Width = 100
        repoSRNNo.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSRNNo)

        Dim repoUnitCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnitCode.FormatString = ""
        repoUnitCode.HeaderText = "UOM"
        repoUnitCode.Name = colUOM

        repoUnitCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUnitCode.ReadOnly = True
        repoUnitCode.Width = 100
        repoUnitCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoUnitCode)

        Dim SRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        SRNQty = New GridViewDecimalColumn()
        SRNQty.FormatString = ""
        SRNQty.HeaderText = "SRN Qty"
        SRNQty.Name = colSRNQty
        SRNQty.WrapText = True
        SRNQty.IsVisible = False
        SRNQty.Width = 80
        SRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        SRNQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SRNQty)

        Dim SRNRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        SRNRate = New GridViewDecimalColumn()
        SRNRate.FormatString = ""
        SRNRate.HeaderText = "SRN Rate"
        SRNRate.Name = colSRNRate
        SRNRate.WrapText = True
        SRNRate.IsVisible = False
        SRNRate.Width = 80
        SRNRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        SRNRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SRNRate)

        Dim PI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        PI = New GridViewTextBoxColumn()
        PI.FormatString = ""
        PI.HeaderText = "PI No"
        PI.Name = colPI
        PI.WrapText = True
        PI.IsVisible = False
        PI.Width = 80
        PI.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        PI.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(PI)

        '===
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

        Dim isAssembled As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        isAssembled.HeaderText = "Is Assembled"
        isAssembled.Name = colIsAssembled
        isAssembled.ReadOnly = False
        isAssembled.IsVisible = False
        isAssembled.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(isAssembled)
        ''

        ''=============== ''done by preeti Gupta=========================================
        Dim repoIsCategory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsCategory.Checked = ToggleState.Off
        repoIsCategory.HeaderText = "Is Category"
        repoIsCategory.Name = colIsCategory
        repoIsCategory.Width = 50
        repoIsCategory.IsVisible = ShowCapexCodeandSubCode
        repoIsCategory.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoIsCategory)

        Dim repoCategoryType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoCategoryType.FormatString = ""
        repoCategoryType.HeaderText = "Category Type"
        repoCategoryType.Name = colCategoryType
        repoCategoryType.Width = 50
        repoCategoryType.IsVisible = ShowCapexCodeandSubCode
        repoCategoryType.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCategoryType.DataSource = LoadCategory()
        repoCategoryType.ValueMember = "Code"
        repoCategoryType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoCategoryType)



        Dim repoCapexSubCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Capex Sub Code"
        repoCapexSubCode.Name = colCapexSubCode
        ' repoCapexSubCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexSubCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCapexSubCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        Dim repoCapexCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexCode.FormatString = ""
        repoCapexCode.HeaderText = "Capex Code"
        repoCapexCode.Name = colCapexCode
        '  repoCapexCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexCode.Width = 100
        repoCapexCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCapexCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCapexCode)
        '================================================================================


        ''============19/10/2016--------------additional charge columns============================
        Dim repoAddationalChargeCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalChargeCode1.FormatString = ""
        repoAddationalChargeCode1.HeaderText = "Additional Charge Code1"
        repoAddationalChargeCode1.Name = colItemACCode1
        repoAddationalChargeCode1.Width = 150
        repoAddationalChargeCode1.ReadOnly = True
        repoAddationalChargeCode1.WrapText = True
        repoAddationalChargeCode1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalChargeCode1)

        Dim repoAddationalorgAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalorgAmt1.WrapText = True
        repoAddationalorgAmt1.ReadOnly = True
        repoAddationalorgAmt1.FormatString = "{0:n3}"
        repoAddationalorgAmt1.Width = 150
        repoAddationalorgAmt1.HeaderText = "Additional Org Amt1"
        repoAddationalorgAmt1.Name = colItemACAmount1
        repoAddationalorgAmt1.IsVisible = False
        repoAddationalorgAmt1.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalorgAmt1)

        Dim repoAddationalCalcAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalcAmt1.WrapText = True
        repoAddationalCalcAmt1.ReadOnly = True
        repoAddationalCalcAmt1.FormatString = "{0:n3}"
        repoAddationalCalcAmt1.Width = 150
        repoAddationalCalcAmt1.HeaderText = "Additional Calc Amt1"
        repoAddationalCalcAmt1.Name = colItemACCalcAmount1
        repoAddationalCalcAmt1.IsVisible = False
        repoAddationalCalcAmt1.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalcAmt1)

        Dim repoAddationalCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCode2.FormatString = ""
        repoAddationalCode2.HeaderText = "Additional Charge Code2"
        repoAddationalCode2.Name = colItemACCode2
        repoAddationalCode2.Width = 50
        repoAddationalCode2.ReadOnly = True
        repoAddationalCode2.WrapText = True
        repoAddationalCode2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCode2)

        Dim repoAddationalOrgAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt2.WrapText = True
        repoAddationalOrgAmt2.ReadOnly = True
        repoAddationalOrgAmt2.FormatString = "{0:n3}"
        repoAddationalOrgAmt2.Width = 50
        repoAddationalOrgAmt2.HeaderText = "Additional Org Amt2"
        repoAddationalOrgAmt2.Name = colItemACAmount2
        repoAddationalOrgAmt2.IsVisible = False
        repoAddationalOrgAmt2.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt2)

        Dim repoAddationalCalAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt2.WrapText = True
        repoAddationalCalAmt2.ReadOnly = True
        repoAddationalCalAmt2.FormatString = "{0:n3}"
        repoAddationalCalAmt2.Width = 150
        repoAddationalCalAmt2.HeaderText = "Additional Calc Amt2"
        repoAddationalCalAmt2.Name = colItemACCalcAmount2
        repoAddationalCalAmt2.IsVisible = False
        repoAddationalCalAmt2.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt2)

        Dim repoAddationalcode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalcode3.FormatString = ""
        repoAddationalcode3.HeaderText = "Additional Charge Code3"
        repoAddationalcode3.Name = colItemACCode3
        repoAddationalcode3.Width = 50
        repoAddationalcode3.ReadOnly = True
        repoAddationalcode3.WrapText = True
        repoAddationalcode3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalcode3)

        Dim repoAddationalOrgAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt3.WrapText = True
        repoAddationalOrgAmt3.ReadOnly = True
        repoAddationalOrgAmt3.FormatString = "{0:n3}"
        repoAddationalOrgAmt3.Width = 50
        repoAddationalOrgAmt3.HeaderText = "Additional Org Amt3"
        repoAddationalOrgAmt3.Name = colItemACAmount3
        repoAddationalOrgAmt3.IsVisible = False
        repoAddationalOrgAmt3.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt3)

        Dim repoAddationalCalAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt3.WrapText = True
        repoAddationalCalAmt3.ReadOnly = True
        repoAddationalCalAmt3.FormatString = "{0:n3}"
        repoAddationalCalAmt3.Width = 150
        repoAddationalCalAmt3.HeaderText = "Additional Calc Amt3"
        repoAddationalCalAmt3.Name = colItemACCalcAmount3
        repoAddationalCalAmt3.IsVisible = False
        repoAddationalCalAmt3.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt3)

        Dim repoAddationalCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCode4.FormatString = ""
        repoAddationalCode4.HeaderText = "Additional Charge Code4"
        repoAddationalCode4.Name = colItemACCode4
        repoAddationalCode4.Width = 50
        repoAddationalCode4.ReadOnly = True
        repoAddationalCode4.WrapText = True
        repoAddationalCode4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCode4)

        Dim repoAddationalorgAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalorgAmt4.WrapText = True
        repoAddationalorgAmt4.ReadOnly = True
        repoAddationalorgAmt4.FormatString = "{0:n3}"
        repoAddationalorgAmt4.Width = 50
        repoAddationalorgAmt4.HeaderText = "Additional Org Amt4"
        repoAddationalorgAmt4.Name = colItemACAmount4
        repoAddationalorgAmt4.IsVisible = False
        repoAddationalorgAmt4.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalorgAmt4)

        Dim repoAddationalCalAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt4.WrapText = True
        repoAddationalCalAmt4.ReadOnly = True
        repoAddationalCalAmt4.FormatString = "{0:n3}"
        repoAddationalCalAmt4.Width = 150
        repoAddationalCalAmt4.HeaderText = "Additional Calc Amt4"
        repoAddationalCalAmt4.Name = colItemACCalcAmount4
        repoAddationalCalAmt4.IsVisible = False
        repoAddationalCalAmt4.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt4)

        Dim repoAddationalCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCode5.FormatString = ""
        repoAddationalCode5.HeaderText = "Additional Charge Code5"
        repoAddationalCode5.Name = colItemACCode5
        repoAddationalCode5.Width = 50
        repoAddationalCode5.ReadOnly = True
        repoAddationalCode5.WrapText = True
        repoAddationalCode5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCode5)


        Dim repoAddationalOrgAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt5.WrapText = True
        repoAddationalOrgAmt5.ReadOnly = True
        repoAddationalOrgAmt5.FormatString = "{0:n3}"
        repoAddationalOrgAmt5.Width = 50
        repoAddationalOrgAmt5.HeaderText = "Additional Org Amt5"
        repoAddationalOrgAmt5.Name = colItemACAmount5
        repoAddationalOrgAmt5.IsVisible = False
        repoAddationalOrgAmt5.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt5)

        Dim repoAddationalCalAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt5.WrapText = True
        repoAddationalCalAmt5.ReadOnly = True
        repoAddationalCalAmt5.FormatString = "{0:n3}"
        repoAddationalCalAmt5.Width = 150
        repoAddationalCalAmt5.HeaderText = "Additional Calc Amt5"
        repoAddationalCalAmt5.Name = colItemACCalcAmount5
        repoAddationalCalAmt5.IsVisible = False
        repoAddationalCalAmt5.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt5)

        Dim repoAddationalCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCode6.FormatString = ""
        repoAddationalCode6.HeaderText = "Additional Charge Code6"
        repoAddationalCode6.Name = colItemACCode6
        repoAddationalCode6.Width = 50
        repoAddationalCode6.ReadOnly = True
        repoAddationalCode6.WrapText = True
        repoAddationalCode6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCode6)

        Dim repoAddationalOrgAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt6.WrapText = True
        repoAddationalOrgAmt6.ReadOnly = True
        repoAddationalOrgAmt6.FormatString = "{0:n3}"
        repoAddationalOrgAmt6.Width = 50
        repoAddationalOrgAmt6.HeaderText = "Additional Org Amt6"
        repoAddationalOrgAmt6.Name = colItemACAmount6
        repoAddationalOrgAmt6.IsVisible = False
        repoAddationalOrgAmt6.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt6)

        Dim repoAddationalCalAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt6.WrapText = True
        repoAddationalCalAmt6.ReadOnly = True
        repoAddationalCalAmt6.FormatString = "{0:n3}"
        repoAddationalCalAmt6.Width = 150
        repoAddationalCalAmt6.HeaderText = "Additional Calc Amt6"
        repoAddationalCalAmt6.Name = colItemACCalcAmount6
        repoAddationalCalAmt6.IsVisible = False
        repoAddationalCalAmt6.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt6)

        Dim repoAddationalCharge7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCharge7.FormatString = ""
        repoAddationalCharge7.HeaderText = "Additional Charge Code7"
        repoAddationalCharge7.Name = colItemACCode7
        repoAddationalCharge7.Width = 50
        repoAddationalCharge7.ReadOnly = True
        repoAddationalCharge7.WrapText = True
        repoAddationalCharge7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCharge7)

        Dim repoAddationalOrgAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt7.WrapText = True
        repoAddationalOrgAmt7.ReadOnly = True
        repoAddationalOrgAmt7.FormatString = "{0:n3}"
        repoAddationalOrgAmt7.Width = 50
        repoAddationalOrgAmt7.HeaderText = "Additional Org Amt7"
        repoAddationalOrgAmt7.Name = colItemACAmount7
        repoAddationalOrgAmt7.IsVisible = False
        repoAddationalOrgAmt7.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt7)

        Dim repoAddationalCalAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt7.WrapText = True
        repoAddationalCalAmt7.ReadOnly = True
        repoAddationalCalAmt7.FormatString = "{0:n3}"
        repoAddationalCalAmt7.Width = 150
        repoAddationalCalAmt7.HeaderText = "Additional Calc Amt7"
        repoAddationalCalAmt7.Name = colItemACCalcAmount7
        repoAddationalCalAmt7.IsVisible = False
        repoAddationalCalAmt7.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt7)

        Dim repoAddationalCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCode8.FormatString = ""
        repoAddationalCode8.HeaderText = "Additional Charge Code8"
        repoAddationalCode8.Name = colItemACCode8
        repoAddationalCode8.Width = 50
        repoAddationalCode8.ReadOnly = True
        repoAddationalCode8.WrapText = True
        repoAddationalCode8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCode8)

        Dim repoAddationalOrgAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt8.WrapText = True
        repoAddationalOrgAmt8.ReadOnly = True
        repoAddationalOrgAmt8.FormatString = "{0:n3}"
        repoAddationalOrgAmt8.Width = 50
        repoAddationalOrgAmt8.HeaderText = "Additional Org Amt8"
        repoAddationalOrgAmt8.Name = colItemACAmount8
        repoAddationalOrgAmt8.IsVisible = False
        repoAddationalOrgAmt8.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt8)

        Dim repoAddationalCalAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt8.WrapText = True
        repoAddationalCalAmt8.ReadOnly = True
        repoAddationalCalAmt8.FormatString = "{0:n3}"
        repoAddationalCalAmt8.Width = 150
        repoAddationalCalAmt8.HeaderText = "Additional Calc Amt8"
        repoAddationalCalAmt8.Name = colItemACCalcAmount8
        repoAddationalCalAmt8.IsVisible = False
        repoAddationalCalAmt8.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt8)

        Dim repoAddationalCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCode9.FormatString = ""
        repoAddationalCode9.HeaderText = "Additional Charge Code9"
        repoAddationalCode9.Name = colItemACCode9
        repoAddationalCode9.Width = 50
        repoAddationalCode9.ReadOnly = True
        repoAddationalCode9.WrapText = True
        repoAddationalCode9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCode9)

        Dim repoAddationalOrgAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt9.WrapText = True
        repoAddationalOrgAmt9.ReadOnly = True
        repoAddationalOrgAmt9.FormatString = "{0:n3}"
        repoAddationalOrgAmt9.Width = 50
        repoAddationalOrgAmt9.HeaderText = "Additional Org Amt9"
        repoAddationalOrgAmt9.Name = colItemACAmount9
        repoAddationalOrgAmt9.IsVisible = False
        repoAddationalOrgAmt9.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt9)

        Dim repoAddationalCalAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt9.WrapText = True
        repoAddationalCalAmt9.ReadOnly = True
        repoAddationalCalAmt9.FormatString = "{0:n3}"
        repoAddationalCalAmt9.Width = 150
        repoAddationalCalAmt9.HeaderText = "Additional Calc Amt9"
        repoAddationalCalAmt9.Name = colItemACCalcAmount9
        repoAddationalCalAmt9.IsVisible = False
        repoAddationalCalAmt9.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt9)

        Dim repoAddationalCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAddationalCode10.FormatString = ""
        repoAddationalCode10.HeaderText = "Additional Charge Code10"
        repoAddationalCode10.Name = colItemACCode10
        repoAddationalCode10.Width = 50
        repoAddationalCode10.ReadOnly = True
        repoAddationalCode10.WrapText = True
        repoAddationalCode10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAddationalCode10)

        Dim repoAddationalOrgAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalOrgAmt10.WrapText = True
        repoAddationalOrgAmt10.ReadOnly = True
        repoAddationalOrgAmt10.FormatString = "{0:n3}"
        repoAddationalOrgAmt10.Width = 50
        repoAddationalOrgAmt10.HeaderText = "Additional Org Amt10"
        repoAddationalOrgAmt10.Name = colItemACAmount10
        repoAddationalOrgAmt10.IsVisible = False
        repoAddationalOrgAmt10.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalOrgAmt10)

        Dim repoAddationalCalAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAddationalCalAmt10.WrapText = True
        repoAddationalCalAmt10.ReadOnly = True
        repoAddationalCalAmt10.FormatString = "{0:n3}"
        repoAddationalCalAmt10.Width = 150
        repoAddationalCalAmt10.HeaderText = "Additional Calc Amt10"
        repoAddationalCalAmt10.Name = colItemACCalcAmount10
        repoAddationalCalAmt10.IsVisible = False
        repoAddationalCalAmt10.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoAddationalCalAmt10)

        ''==============================================================================================



        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()

        If clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal Then
            gv1.Columns(colICode).IsVisible = False
            gv1.Columns(colIName).IsVisible = False
        Else
            gv1.Columns(colICode).IsVisible = False
            gv1.Columns(colIName).IsVisible = False
        End If
    End Sub

    Sub LoadBlankGridAssemble()
        gvAssemble.Rows.Clear()
        gvAssemble.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "S.No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvAssemble.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoAssetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Code"
        repoAssetCode.Name = colAssetID
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 100
        repoAssetCode.ReadOnly = True
        gvAssemble.MasterTemplate.Columns.Add(repoAssetCode)

        Dim repoType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Type"
        repoType.Name = colType
        repoType.Width = 60
        repoType.ReadOnly = True
        gvAssemble.MasterTemplate.Columns.Add(repoType)

        Dim repoDocumentCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocumentCode.FormatString = ""
        repoDocumentCode.HeaderText = "Document Code"
        repoDocumentCode.Name = colDocumentCode
        repoDocumentCode.Width = 150
        repoDocumentCode.ReadOnly = True
        gvAssemble.MasterTemplate.Columns.Add(repoDocumentCode)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Document Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colDocumentDate
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = True
        repoDate.Width = 80
        gvAssemble.MasterTemplate.Columns.Add(repoDate)

        Dim repoItemNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemNo.FormatString = ""
        repoItemNo.HeaderText = "Item No"
        repoItemNo.Name = colItemNo
        repoItemNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItemNo.Width = 100
        repoItemNo.ReadOnly = True
        repoItemNo.IsVisible = True
        'repoItemNo.VisibleInColumnChooser = False
        gvAssemble.MasterTemplate.Columns.Add(repoItemNo)

        Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.HeaderText = "Item Desc"
        repoItemDesc.Name = colItemDesc
        repoItemDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItemDesc.Width = 100
        repoItemDesc.ReadOnly = True
        repoItemDesc.IsVisible = True
        'repoItemDesc.VisibleInColumnChooser = False
        gvAssemble.MasterTemplate.Columns.Add(repoItemDesc)

        'sanjay
        Dim repoHierarchy As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHierarchy.FormatString = ""
        repoHierarchy.HeaderText = "Hierarchy"
        repoHierarchy.Name = colHierarchy
        repoHierarchy.TextImageRelation = TextImageRelation.TextBeforeImage
        repoHierarchy.Width = 100
        repoHierarchy.ReadOnly = True
        repoHierarchy.IsVisible = True
        'repoItemDesc.VisibleInColumnChooser = False
        gvAssemble.MasterTemplate.Columns.Add(repoHierarchy)

        Dim repoCostCenter As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenter.FormatString = ""
        repoCostCenter.HeaderText = "Cost Center"
        repoCostCenter.Name = colCostCenter
        repoCostCenter.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCostCenter.Width = 100
        repoCostCenter.ReadOnly = True
        repoCostCenter.IsVisible = True
        'repoItemDesc.VisibleInColumnChooser = False
        gvAssemble.MasterTemplate.Columns.Add(repoCostCenter)
        'sanjay

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.ReadOnly = True
        repoAmount.IsVisible = True
        repoAmount.Width = 80
        gvAssemble.MasterTemplate.Columns.Add(repoAmount)

        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Total Tax Amount"
        repoAmount.Name = colTotalTaxAmount
        repoAmount.ReadOnly = True
        repoAmount.IsVisible = True
        repoAmount.Width = 80
        gvAssemble.MasterTemplate.Columns.Add(repoAmount)

        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Item Net Amount"
        repoAmount.Name = colItemNetAmount
        repoAmount.ReadOnly = True
        repoAmount.IsVisible = True
        repoAmount.Width = 80
        gvAssemble.MasterTemplate.Columns.Add(repoAmount)

        Dim repocheck As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repocheck = New GridViewCheckBoxColumn()
        repocheck.FormatString = ""
        repocheck.HeaderText = "Distribute"
        repocheck.Name = colcheck
        repocheck.Width = 40
        repocheck.ReadOnly = False
        gvAssemble.MasterTemplate.Columns.Add(repocheck)

        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Distribute Amount"
        repoAmount.Name = colDistributeAmount
        repoAmount.ReadOnly = False
        repoAmount.IsVisible = True
        repoAmount.Width = 80
        gvAssemble.MasterTemplate.Columns.Add(repoAmount)

        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Total Amount"
        repoAmount.Name = colTotalAmount
        repoAmount.ReadOnly = True
        repoAmount.IsVisible = True
        repoAmount.Width = 80
        gvAssemble.MasterTemplate.Columns.Add(repoAmount)

        gvAssemble.AllowDeleteRow = False
        gvAssemble.AllowAddNewRow = False
        gvAssemble.ShowGroupPanel = False
        gvAssemble.AllowColumnReorder = True
        gvAssemble.AllowRowReorder = True
        gvAssemble.EnableSorting = False
        gvAssemble.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvAssemble.MasterTemplate.ShowRowHeaderColumn = False
        gvAssemble.TableElement.TableHeaderHeight = 40

    End Sub

    Private Function LoadCategory() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Capex"
        dr("Name") = "Capex"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Regular"
        dr("Name") = "Regular"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
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
        repoTaxRate.ReadOnly = False
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = False
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)




        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        ReStoreGridLayout()
    End Sub

    Private Sub gv1_CellPaint(sender As Object, e As GridViewCellPaintEventArgs) Handles gv1.CellPaint

    End Sub

    Sub OpenCapexSubCodeList()
        Try
            gv1.CurrentRow.Cells(colCapexSubCode).Value = clsCapexBudget.getFinder("", gv1.CurrentRow.Cells(colCapexSubCode).Value, False)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colCapexSubCode).Value) > 0 Then
                gv1.CurrentRow.Cells(colCapexCode).Value = clsCapexBudget.GetCapexCode(gv1.CurrentRow.Cells(colCapexSubCode).Value, Nothing)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) Then
                        OpenICodeList(False)
                    ElseIf e.Column Is gv1.Columns(colCapexSubCode) Then
                        OpenCapexSubCodeList()

                    ElseIf e.Column Is gv1.Columns(colHierarchyCode) Then
                        OpenHierarchyCode(False)
                    ElseIf e.Column Is gv1.Columns(colCostCenterCode) Then

                        OpenCostCenterList(False)
                    ElseIf e.Column Is gv1.Columns(colAssetSpecificaion) Then
                        If chkSameDesSpecStartDate.Checked AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colAssetSpecificaion).Value) > 0 Then
                            For kk As Integer = gv1.CurrentRow.Index To gv1.RowCount - 1
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(kk).Cells(colICode).Value)) = CompairStringResult.Equal Then
                                    If clsCommon.myLen(gv1.Rows(kk).Cells(colAssetSpecificaion).Value) <= 0 Then
                                        gv1.Rows(kk).Cells(colAssetSpecificaion).Value = gv1.CurrentRow.Cells(colAssetSpecificaion).Value
                                    End If
                                End If
                            Next
                        End If
                    ElseIf e.Column Is gv1.Columns(colAssetName) Then
                        If chkSameDesSpecStartDate.Checked AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colAssetName).Value) > 0 Then
                            For kk As Integer = gv1.CurrentRow.Index To gv1.RowCount - 1
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(kk).Cells(colICode).Value)) = CompairStringResult.Equal Then
                                    If clsCommon.myLen(gv1.Rows(kk).Cells(colAssetName).Value) <= 0 Then
                                        gv1.Rows(kk).Cells(colAssetName).Value = gv1.CurrentRow.Cells(colAssetName).Value
                                    End If
                                End If
                            Next
                        End If
                    End If

                    isCellValueChangedOpen = False
                End If

                If gv1.CurrentRow.Cells(colIsCategory).Value = True Then
                    gv1.Columns(colCategoryType).ReadOnly = False

                    gv1.Columns(colCapexSubCode).ReadOnly = True
                    gv1.Columns(colCapexCode).ReadOnly = True
                Else
                    gv1.Columns(colCategoryType).ReadOnly = True

                    gv1.Columns(colCapexSubCode).ReadOnly = True
                    gv1.Columns(colCapexCode).ReadOnly = True
                    gv1.CurrentRow.Cells(colCategoryType).Value = ""
                    gv1.CurrentRow.Cells(colCapexCode).Value = ""
                    gv1.CurrentRow.Cells(colCapexSubCode).Value = ""

                End If


                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal Then
                        grow.Cells(colAssetID).ReadOnly = True
                    End If
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetID).Value), "A", True, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
        End If
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colAssetID).Value = ""
        gv1.CurrentRow.Cells(colAssetName).Value = ""

        gv1.CurrentRow.Cells(colAcquisitionDate).Value = 0
        ''gv1.CurrentRow.Cells(colAssessableRate).Value = 0
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
                gv1.Rows(i).Cells(colcheck).Value = True
            End If
        Next
    End Sub
    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
        gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Level,0) AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
        gv1.CurrentRow.Cells(colHierarchyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Description,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
        gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
        gv1.CurrentRow.Cells(colCostCenterName).Value = ""
        'gv1.CurrentRow.Cells(colHierarchyLevel1).Value = ""
        'gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
        'gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
        'gv1.CurrentRow.Cells(colHierarchyLevel4).Value = ""

    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblAmtAfterDis As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColBookSourceValue).Value)
        Dim dblItemTotalAddationalCharge As Double = gv1.Rows(IntRowNo).Cells(colItemTotalAdditionalCharge).Value
        gv1.Rows(IntRowNo).Cells(colTaxRecoverable).Value = Nothing
        gv1.Rows(IntRowNo).Cells(colTaxNonRecoverable).Value = Nothing

        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    Dim IsRecoverable As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Recoverable from tspl_tax_master where Tax_Code ='" & strTaxCode & "'"))
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        Dim dblOtherTaxAmt As Double = 0
                        dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                        dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                    End If
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100

                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 2)


                    If IsRecoverable = "Y" Then
                        gv1.Rows(IntRowNo).Cells("colTaxRecoverable").Value = gv1.Rows(IntRowNo).Cells("colTaxRecoverable").Value + Math.Round(dblTaxAmt, 2)
                    Else
                        gv1.Rows(IntRowNo).Cells("colTaxNonRecoverable").Value = gv1.Rows(IntRowNo).Cells("colTaxNonRecoverable").Value + Math.Round(+dblTaxAmt, 2)
                    End If


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
            ElseIf rbtnTaxCalManual.IsChecked Then
                If gv2.Rows.Count >= ii Then
                    Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
                    Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(ColBookSourceValue).Value)
                    Dim dblTotAmt As Double = 0
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(ColBookSourceValue).Value)
                    Next
                    Dim dblCurrCalTax As Double = 0
                    If dblTotAmt <> 0 Then
                        dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                    End If
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                End If
            End If
        Next
        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmtAfterDis
        'Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt
        If clsCommon.myLen(txtSRNNo.Value) > 0 Then
            'gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(strTotalTaxAmount, 2)
            'gv1.Rows(IntRowNo).Cells(colNetAmt).Value = Math.Round((dblAmtAfterDis + strTotalTaxAmount), 2)
            gv1.Rows(IntRowNo).Cells(colNetAmt).Value = Math.Round((dblAmtAfterDis), 2)
        Else
            '  gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv1.Rows(IntRowNo).Cells(colNetAmt).Value = Math.Round(dblAmtAfterTax, 2)
        End If

    End Sub

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

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0

        Dim dblTaxAssessableAmt As Double = 0

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
        Dim ChkQty As Double = 0



        Dim dblTaxTotAmt As Double = 0
        Dim dblTotalTaxRecoverableAmt As Double = 0
        Dim dblTotalTaxNonRecoverableAmt As Double = 0

        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            'If (clsCommon.myLen(gv1.Rows(ii).Cells(colAssetID).Value) > 0) Then
            dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(ColBookSourceValue).Value)
            ChkQty = ChkQty + clsCommon.myCdbl(gv1.Rows(ii).Cells(ColBookSourceValue).Value)

            dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colNetAmt).Value)
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

            dblTotalTaxRecoverableAmt = dblTotalTaxRecoverableAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRecoverable).Value)
            dblTotalTaxNonRecoverableAmt = dblTotalTaxNonRecoverableAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxNonRecoverable).Value)

            dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
            dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colNetAmt).Value)
            'End If
        Next


        If rbtnTaxCalAutomatic.IsChecked Then
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
        End If
        ''=====================================================
        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
        Next
        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        dblNetAmt = dblNetAmt + dblACAmount
        '====================================================
        lblTotalAmt.Text = clsCommon.myFormat(dblTotAmt)
        If ImportMultipleAssetAssembled AndAlso chkOpening.Checked Then
            txtAssembleOpeningAmt.Text = clsCommon.myFormat(dblTotAmt)
        End If
        txtNonRecAmt.Text = dblTotalTaxNonRecoverableAmt
        txtRecAmt.Text = dblTotalTaxRecoverableAmt
        txtAssetAmount.Text = clsCommon.myFormat(lblTotalAmt.Text) + dblTotalTaxNonRecoverableAmt
        ' lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblTaxAmt.Text = 0
        lblNetAmt.Text = clsCommon.myFormat(dblAmtAfterDis) ''+ dblACAmount by balwinder on 15/05/2021
        Calc_AddtionalCharge_Itemwise(dblTotAmt)
    End Sub

#Region "item level additional charge calculation"
    Private Sub Calc_AddtionalCharge_Itemwisedemo(ByVal TotalQty As Double)
        Try

            Dim add_code1 As String = ""
            Dim add_amt1 As Double = Nothing
            Dim add_code2 As String = ""
            Dim add_amt2 As Double = Nothing
            Dim add_code3 As String = ""
            Dim add_amt3 As Double = Nothing
            Dim add_code4 As String = ""
            Dim add_amt4 As Double = Nothing
            Dim add_code5 As String = ""
            Dim add_amt5 As Double = Nothing
            Dim add_code6 As String = ""
            Dim add_amt6 As Double = Nothing
            Dim add_code7 As String = ""
            Dim add_amt7 As Double = Nothing
            Dim add_code8 As String = ""
            Dim add_amt8 As Double = Nothing
            Dim add_code9 As String = ""
            Dim add_amt9 As Double = Nothing
            Dim add_code10 As String = ""
            Dim add_amt10 As Double = Nothing
            ''==========================================================================================
            If gvAC.Rows.Count > 0 Then
                If gvAC.Rows.Count > 0 AndAlso clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                    add_code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                    add_amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 1 AndAlso clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                    add_code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                    add_amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 2 AndAlso clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                    add_code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                    add_amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 3 AndAlso clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                    add_code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                    add_amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 4 AndAlso clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                    add_code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                    add_amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 5 AndAlso clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                    add_code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                    add_amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 6 AndAlso clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                    add_code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                    add_amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 7 AndAlso clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                    add_code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                    add_amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 8 AndAlso clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                    add_code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                    add_amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 9 AndAlso clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                    add_code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                    add_amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                End If
            End If ''additional head level grid
            ''==========================================================================================
            Dim LastIndex As Integer = 0
            Dim TotalAmt1 As Double = Nothing
            Dim TotalAmt2 As Double = Nothing
            Dim TotalAmt3 As Double = Nothing
            Dim TotalAmt4 As Double = Nothing
            Dim TotalAmt5 As Double = Nothing
            Dim TotalAmt6 As Double = Nothing
            Dim TotalAmt7 As Double = Nothing
            Dim TotalAmt8 As Double = Nothing
            Dim TotalAmt9 As Double = Nothing
            Dim TotalAmt10 As Double = Nothing
            Dim qty As Double = Nothing

            For Each grow As GridViewRowInfo In gv1.Rows
                qty = clsCommon.myCdbl(grow.Cells(ColBookSourceOriginalValue).Value)
                ''=======================code=============================
                grow.Cells(colItemACCode1).Value = add_code1
                grow.Cells(colItemACCode2).Value = add_code2
                grow.Cells(colItemACCode3).Value = add_code3
                grow.Cells(colItemACCode4).Value = add_code4
                grow.Cells(colItemACCode5).Value = add_code5
                grow.Cells(colItemACCode6).Value = add_code6
                grow.Cells(colItemACCode7).Value = add_code7
                grow.Cells(colItemACCode8).Value = add_code8
                grow.Cells(colItemACCode9).Value = add_code9
                grow.Cells(colItemACCode10).Value = add_code10

                grow.Cells(colItemACAmount1).Value = System.Math.Round(add_amt1, 3)
                grow.Cells(colItemACAmount2).Value = System.Math.Round(add_amt2, 3)
                grow.Cells(colItemACAmount3).Value = System.Math.Round(add_amt3, 3)
                grow.Cells(colItemACAmount4).Value = System.Math.Round(add_amt4, 3)
                grow.Cells(colItemACAmount5).Value = System.Math.Round(add_amt5, 3)
                grow.Cells(colItemACAmount6).Value = System.Math.Round(add_amt6, 3)
                grow.Cells(colItemACAmount7).Value = System.Math.Round(add_amt7, 3)
                grow.Cells(colItemACAmount8).Value = System.Math.Round(add_amt8, 3)
                grow.Cells(colItemACAmount9).Value = System.Math.Round(add_amt9, 3)
                grow.Cells(colItemACAmount10).Value = System.Math.Round(add_amt10, 3)
                ''=============amount=========================================
                If TotalQty > 0 Then
                    grow.Cells(colItemACCalcAmount1).Value = System.Math.Round((qty * add_amt1) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount2).Value = System.Math.Round((qty * add_amt2) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount3).Value = System.Math.Round((qty * add_amt3) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount4).Value = System.Math.Round((qty * add_amt4) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount5).Value = System.Math.Round((qty * add_amt5) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount6).Value = System.Math.Round((qty * add_amt6) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount7).Value = System.Math.Round((qty * add_amt7) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount8).Value = System.Math.Round((qty * add_amt8) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount9).Value = System.Math.Round((qty * add_amt9) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount10).Value = System.Math.Round((qty * add_amt10) / TotalQty, 3)

                    TotalAmt1 = System.Math.Round(TotalAmt1 + System.Math.Round((qty * add_amt1) / TotalQty, 3), 3)
                    TotalAmt2 = System.Math.Round(TotalAmt2 + System.Math.Round((qty * add_amt2) / TotalQty, 3), 3)
                    TotalAmt3 = System.Math.Round(TotalAmt3 + System.Math.Round((qty * add_amt3) / TotalQty, 3), 3)
                    TotalAmt4 = System.Math.Round(TotalAmt4 + System.Math.Round((qty * add_amt4) / TotalQty, 3), 3)
                    TotalAmt5 = System.Math.Round(TotalAmt5 + System.Math.Round((qty * add_amt5) / TotalQty, 3), 3)
                    TotalAmt6 = System.Math.Round(TotalAmt6 + System.Math.Round((qty * add_amt6) / TotalQty, 3), 3)
                    TotalAmt7 = System.Math.Round(TotalAmt7 + System.Math.Round((qty * add_amt7) / TotalQty, 3), 3)
                    TotalAmt8 = System.Math.Round(TotalAmt8 + System.Math.Round((qty * add_amt8) / TotalQty, 3), 3)
                    TotalAmt9 = System.Math.Round(TotalAmt9 + System.Math.Round((qty * add_amt9) / TotalQty, 3), 3)
                    TotalAmt10 = System.Math.Round(TotalAmt10 + System.Math.Round((qty * add_amt10) / TotalQty, 3), 3)
                End If
                grow.Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
            Next


            'Dim totalAmountwithAddationalCharge As Double = clsCommon.myCdbl(grow.Cells(colItemTotalAdditionalCharge).Value) + clsCommon.myCdbl(grow.Cells(colNetAmt).Value)
            'grow.Cells(colNetAmt).Value = System.Math.Round(clsCommon.myCdbl(totalAmountwithAddationalCharge), 2)

            ''================check if grid amount not equal to header amount then adjust it on last item row==============
            If gv1.Rows.Count > 0 AndAlso TotalAmt1 > add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) - (TotalAmt1 - add_amt1), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt1 < add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) + (add_amt1 - TotalAmt1), 3)
            End If
            ''2.
            If gv1.Rows.Count > 0 AndAlso TotalAmt2 > add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) - (TotalAmt2 - add_amt2), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt2 < add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) + (add_amt2 - TotalAmt2), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt3 > add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) - (TotalAmt3 - add_amt3), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt3 < add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) + (add_amt3 - TotalAmt3), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt4 > add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) - (TotalAmt4 - add_amt4), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt4 < add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) + (add_amt4 - TotalAmt4), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt5 > add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) - (TotalAmt5 - add_amt5), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt5 < add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) + (add_amt5 - TotalAmt5), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt6 > add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) - (TotalAmt6 - add_amt6), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt6 < add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) + (add_amt6 - TotalAmt6), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt7 > add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) - (TotalAmt7 - add_amt7), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt7 < add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) + (add_amt7 - TotalAmt7), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt8 > add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) - (TotalAmt8 - add_amt8), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt8 < add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) + (add_amt8 - TotalAmt8), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt9 > add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) - (TotalAmt9 - add_amt9), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt9 < add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) + (add_amt9 - TotalAmt9), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt10 > add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) - (TotalAmt10 - add_amt10), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt10 < add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) + (add_amt10 - TotalAmt10), 3)
            End If

            If gv1.Columns(colItemTotalAdditionalCharge) IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                gv1.Rows(LastIndex).Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount10).Value)
            End If
            ''==========================================================================================================
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region

#Region "item level additional charge calculation"
    Private Sub Calc_AddtionalCharge_Itemwise(ByVal TotalQty As Double)
        Try

            Dim add_code1 As String = ""
            Dim add_amt1 As Double = Nothing
            Dim add_code2 As String = ""
            Dim add_amt2 As Double = Nothing
            Dim add_code3 As String = ""
            Dim add_amt3 As Double = Nothing
            Dim add_code4 As String = ""
            Dim add_amt4 As Double = Nothing
            Dim add_code5 As String = ""
            Dim add_amt5 As Double = Nothing
            Dim add_code6 As String = ""
            Dim add_amt6 As Double = Nothing
            Dim add_code7 As String = ""
            Dim add_amt7 As Double = Nothing
            Dim add_code8 As String = ""
            Dim add_amt8 As Double = Nothing
            Dim add_code9 As String = ""
            Dim add_amt9 As Double = Nothing
            Dim add_code10 As String = ""
            Dim add_amt10 As Double = Nothing
            ''==========================================================================================
            If gvAC.Rows.Count > 0 Then
                If gvAC.Rows.Count > 0 AndAlso clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                    add_code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                    add_amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 1 AndAlso clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                    add_code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                    add_amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 2 AndAlso clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                    add_code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                    add_amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 3 AndAlso clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                    add_code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                    add_amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 4 AndAlso clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                    add_code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                    add_amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 5 AndAlso clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                    add_code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                    add_amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 6 AndAlso clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                    add_code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                    add_amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 7 AndAlso clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                    add_code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                    add_amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 8 AndAlso clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                    add_code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                    add_amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 9 AndAlso clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                    add_code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                    add_amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                End If
            End If ''additional head level grid
            ''==========================================================================================
            Dim LastIndex As Integer = 0
            Dim TotalAmt1 As Double = Nothing
            Dim TotalAmt2 As Double = Nothing
            Dim TotalAmt3 As Double = Nothing
            Dim TotalAmt4 As Double = Nothing
            Dim TotalAmt5 As Double = Nothing
            Dim TotalAmt6 As Double = Nothing
            Dim TotalAmt7 As Double = Nothing
            Dim TotalAmt8 As Double = Nothing
            Dim TotalAmt9 As Double = Nothing
            Dim TotalAmt10 As Double = Nothing
            Dim qty As Double = Nothing

            For Each grow As GridViewRowInfo In gv1.Rows
                qty = clsCommon.myCdbl(grow.Cells(ColBookSourceValue).Value)
                ''=======================code=============================
                grow.Cells(colItemACCode1).Value = add_code1
                grow.Cells(colItemACCode2).Value = add_code2
                grow.Cells(colItemACCode3).Value = add_code3
                grow.Cells(colItemACCode4).Value = add_code4
                grow.Cells(colItemACCode5).Value = add_code5
                grow.Cells(colItemACCode6).Value = add_code6
                grow.Cells(colItemACCode7).Value = add_code7
                grow.Cells(colItemACCode8).Value = add_code8
                grow.Cells(colItemACCode9).Value = add_code9
                grow.Cells(colItemACCode10).Value = add_code10

                grow.Cells(colItemACAmount1).Value = System.Math.Round(add_amt1, 3)
                grow.Cells(colItemACAmount2).Value = System.Math.Round(add_amt2, 3)
                grow.Cells(colItemACAmount3).Value = System.Math.Round(add_amt3, 3)
                grow.Cells(colItemACAmount4).Value = System.Math.Round(add_amt4, 3)
                grow.Cells(colItemACAmount5).Value = System.Math.Round(add_amt5, 3)
                grow.Cells(colItemACAmount6).Value = System.Math.Round(add_amt6, 3)
                grow.Cells(colItemACAmount7).Value = System.Math.Round(add_amt7, 3)
                grow.Cells(colItemACAmount8).Value = System.Math.Round(add_amt8, 3)
                grow.Cells(colItemACAmount9).Value = System.Math.Round(add_amt9, 3)
                grow.Cells(colItemACAmount10).Value = System.Math.Round(add_amt10, 3)
                ''=============amount=========================================
                If TotalQty > 0 Then
                    grow.Cells(colItemACCalcAmount1).Value = System.Math.Round((qty * add_amt1) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount2).Value = System.Math.Round((qty * add_amt2) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount3).Value = System.Math.Round((qty * add_amt3) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount4).Value = System.Math.Round((qty * add_amt4) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount5).Value = System.Math.Round((qty * add_amt5) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount6).Value = System.Math.Round((qty * add_amt6) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount7).Value = System.Math.Round((qty * add_amt7) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount8).Value = System.Math.Round((qty * add_amt8) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount9).Value = System.Math.Round((qty * add_amt9) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount10).Value = System.Math.Round((qty * add_amt10) / TotalQty, 3)

                    TotalAmt1 = System.Math.Round(TotalAmt1 + System.Math.Round((qty * add_amt1) / TotalQty, 3), 3)
                    TotalAmt2 = System.Math.Round(TotalAmt2 + System.Math.Round((qty * add_amt2) / TotalQty, 3), 3)
                    TotalAmt3 = System.Math.Round(TotalAmt3 + System.Math.Round((qty * add_amt3) / TotalQty, 3), 3)
                    TotalAmt4 = System.Math.Round(TotalAmt4 + System.Math.Round((qty * add_amt4) / TotalQty, 3), 3)
                    TotalAmt5 = System.Math.Round(TotalAmt5 + System.Math.Round((qty * add_amt5) / TotalQty, 3), 3)
                    TotalAmt6 = System.Math.Round(TotalAmt6 + System.Math.Round((qty * add_amt6) / TotalQty, 3), 3)
                    TotalAmt7 = System.Math.Round(TotalAmt7 + System.Math.Round((qty * add_amt7) / TotalQty, 3), 3)
                    TotalAmt8 = System.Math.Round(TotalAmt8 + System.Math.Round((qty * add_amt8) / TotalQty, 3), 3)
                    TotalAmt9 = System.Math.Round(TotalAmt9 + System.Math.Round((qty * add_amt9) / TotalQty, 3), 3)
                    TotalAmt10 = System.Math.Round(TotalAmt10 + System.Math.Round((qty * add_amt10) / TotalQty, 3), 3)
                End If
                grow.Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
                ''Comment by balwidner on 13/05/2021 becuase In Net amount it will not not add.
                'Dim totalAmountwithAddationalCharge As Double = clsCommon.myCdbl(grow.Cells(colItemTotalAdditionalCharge).Value) + clsCommon.myCdbl(grow.Cells(colNetAmt).Value)
                'grow.Cells(colNetAmt).Value = System.Math.Round(clsCommon.myCdbl(totalAmountwithAddationalCharge), 2)
            Next




            ''================check if grid amount not equal to header amount then adjust it on last item row==============
            If gv1.Rows.Count > 0 AndAlso TotalAmt1 > add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) - (TotalAmt1 - add_amt1), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt1 < add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) + (add_amt1 - TotalAmt1), 3)
            End If
            ''2.
            If gv1.Rows.Count > 0 AndAlso TotalAmt2 > add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) - (TotalAmt2 - add_amt2), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt2 < add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) + (add_amt2 - TotalAmt2), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt3 > add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) - (TotalAmt3 - add_amt3), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt3 < add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) + (add_amt3 - TotalAmt3), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt4 > add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) - (TotalAmt4 - add_amt4), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt4 < add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) + (add_amt4 - TotalAmt4), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt5 > add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) - (TotalAmt5 - add_amt5), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt5 < add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) + (add_amt5 - TotalAmt5), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt6 > add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) - (TotalAmt6 - add_amt6), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt6 < add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) + (add_amt6 - TotalAmt6), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt7 > add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) - (TotalAmt7 - add_amt7), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt7 < add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) + (add_amt7 - TotalAmt7), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt8 > add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) - (TotalAmt8 - add_amt8), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt8 < add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) + (add_amt8 - TotalAmt8), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt9 > add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) - (TotalAmt9 - add_amt9), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt9 < add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) + (add_amt9 - TotalAmt9), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt10 > add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) - (TotalAmt10 - add_amt10), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt10 < add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) + (add_amt10 - TotalAmt10), 3)
            End If

            If gv1.Columns(colItemTotalAdditionalCharge) IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                gv1.Rows(LastIndex).Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount10).Value)
            End If
            ''==========================================================================================================
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        'btnTDSDetail.Enabled = False
        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        butCostCenterAndHirerachy_Update_AfterPost.Visible = False
        lblPIDate.Text = ""
        lblSRNDate.Text = ""
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        ImportMultipleAssetAssembled = If(clsFixedParameter.GetData(clsFixedParameterCode.ImportMultipleAssetAssembled, clsFixedParameterType.ImportMultipleAssetAssembled, Nothing) = 1, True, False)
        If ReadOnlyTemplateFieldsOnAcqusition Then
            lblTemplate.Text = "Asset Category"
        End If
        If ImportMultipleAssetAssembled Then
            MyLabel4.Enabled = False
            txtAssembleOpeningAmt.Enabled = False
        End If
        txtVendorNo.Enabled = True
        BlankAllControls()
        LoadBlankGrid()
        LoadBlankGridTax()
        LoadBlankGridAC()
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
        isNewEntry = True
        btnSave.Text = "Save"
        gvAC.ReadOnly = False
        rbtnnew.Enabled = True
        rbtnold.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        ddlAcqType.Enabled = True
        btnChangeDepDetail.Enabled = False
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        txtSRNNo.Enabled = True
        fndPINo.Enabled = True
        txtLocation.Enabled = True
        'CheckIsSRNMandatory()
        fndTemplateCode.Value = Nothing
        Me.lblTemplateDesc.Text = ""
        txtTaxGroup.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        ddlAcqType.Focus()
        ddlAcqType.Select()
        fndcapexcode.Value = ""
        fndcapexsubcode.Value = ""
        lbl_capexcode.Text = ""
        lbl_capexsubcode.Text = ""
        lbl_budgetamt.Text = ""
        lbl_budgetamtwithtolerence.Text = ""
        lbl_rebudgetamt.Text = ""
        lbl_rebudgetamtwithtolerence.Text = ""
        lblAddCharges.Text = ""
        gvAssemble.DataSource = Nothing
        txtNonRecAmt.Text = ""
        txtRecAmt.Text = ""
        txtAssetAmount.Text = ""
        dtpPostingDate.Value = clsCommon.GETSERVERDATE
        txtAssembleCode.Text = ""
        AcqType()
    End Sub

    Private Function AllowToSave() As Boolean
        '===============Preeti==================================
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        '=======================================================
        If clsCommon.myLen(txtSRNNo.Value) > 0 Then
            Dim AcqDate As Date? = clsCommon.GetDateWithStartTime(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
            Dim SRNDate As Date? = clsCommon.GetDateWithStartTime(clsCommon.GetPrintDate(lblSRNDate.Text, "dd/MM/yyyy"))
            If AcqDate < SRNDate Then
                ' If clsCommon.GetPrintDate(lblSRNDate.Text, "dd/MMM/yyyy") > clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") Then
                clsCommon.MyMessageBoxShow(Me, "Acquision Date (" + clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) + ") should be greater then/Equal SRN Date (" + clsCommon.myCstr(clsCommon.GetPrintDate(lblSRNDate.Text, "dd/MM/yyyy")) + ")", Me.Text)
                Return False
            End If
        End If
        If clsCommon.myLen(ddlAcqType.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select Acquisition Type", Me.Text)
            ddlAcqType.Focus()
            Return False
        End If

        If clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal AndAlso chkOpeningDirect.Checked = False Then
            'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
            If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
                    chkOpeningDirect.Checked = True
                End If
            End If
        End If

        RefreshSNo()

        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
        If Not objRemittance Is Nothing Then
            UpdateTDSAmount()
        End If

        If rbtnnew.IsChecked = False AndAlso rbtnold.IsChecked = False Then
            clsCommon.MyMessageBoxShow(Me, "Please Select One Option From Old or New Radio Button", Me.Text)
            Return False
        End If

        '===============shivani
        'If clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select Tax Group,it is manadatory")
        '    Return False
        'End If
        '======

        'CheckIsSRNMandatory()
        If clsCommon.myLen(fndPINo.Value) <= 0 AndAlso clsCommon.CompairString(ddlAcqType.Text, "Asset") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select PI No", Me.Text)
            fndPINo.Focus()
            Return False
        End If
        'If clsCommon.myLen(txtSRNNo.Value) <= 0 AndAlso clsCommon.CompairString(ddlAcqType.Text, "Asset") = CompairStringResult.Equal Then
        '    common.clsCommon.MyMessageBoxShow("Please select SRN No")
        '    txtSRNNo.Focus()
        '    Return False
        'End If
        Dim arrAssetCode As New List(Of String)
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCBool(grow.Cells(colcheck).Value) = True Then
                If (clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 Or clsCommon.myLen(txtSRNNo.Value) > 0) Then
                    If clsCommon.myCstr(grow.Cells(colPutToUse).Value) = "1" Then
                        If CDate(clsCommon.GetPrintDate(grow.Cells(colStartDate).Value, "dd/MMM/yyyy")) < CDate(clsCommon.GetPrintDate(grow.Cells(colAcquisitionDate).Value, "dd/MMM/yyyy")) Then
                            clsCommon.MyMessageBoxShow(Me, "Put To Use Date can not less than Acquisition Date at line no " & (grow.Index + 1) & ".")
                            Return False
                        End If
                    End If

                End If
                If clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 Then ''ERO/01/08/19-000978 by balwinder on 02/08/2019
                    If arrAssetCode.Contains(clsCommon.myCstr(grow.Cells(colAssetID).Value).ToUpper()) Then
                        clsCommon.MyMessageBoxShow(Me, "Repeared Asset Code [" + (clsCommon.myCstr(grow.Cells(colAssetID).Value) + "]  at line no " & (grow.Index + 1) & "."))
                        Return False
                    Else
                        arrAssetCode.Add(clsCommon.myCstr(grow.Cells(colAssetID).Value).ToUpper())
                    End If
                End If
            End If
        Next
        arrAssetCode = Nothing
        '===added by shivani[BM00000007837]
        Dim Dt_Temp_Id As New DataTable
        Dt_Temp_Id.Columns.Add("Item_Code")
        Dt_Temp_Id.Columns.Add("Temp_Code")
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCBool(grow.Cells(colcheck).Value) = True Then
                If (clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal AndAlso clsCommon.myLen(grow.Cells(colAssetID).Value) = 0) Then
                    clsCommon.MyMessageBoxShow(Me, "Asset Code cannot be left blank at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
                If (clsCommon.myLen(grow.Cells(colAssetName).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Asset Description  cannot be left blank at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
                If (clsCommon.myLen(grow.Cells(colAssetSpecificaion).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Asset Specification  cannot be left blank at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
            End If

            If (clsCommon.myLen(grow.Cells(colAssetName).Value)) > 0 Then
                If (clsCommon.myLen(grow.Cells(colTemplete).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Template code cannot be left blank at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
                If (clsCommon.myLen(grow.Cells(colGroupCode).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Group code cannot be left blank at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
                If (clsCommon.myCdbl(grow.Cells(colNetAmt).Value)) <= 0 And clsCommon.CompairString(ddlAcqType.Text, "Assembled") <> CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Net Amount is  zero at line no " & (grow.Index + 1) & ".")
                    Return False
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(ddlAcqType.Text), "Asset") = CompairStringResult.Equal Then
                If Dt_Temp_Id.Rows.Count > 0 Then
                    Dim drr() As DataRow
                    drr = Dt_Temp_Id.Select("Item_COde='" & clsCommon.myCstr(grow.Cells(colICode).Value) & "'") '(0)
                    If IsNothing(drr) OrElse drr.Count <= 0 Then
                        Dim dr As DataRow = Dt_Temp_Id.NewRow()
                        dr("Item_COde") = clsCommon.myCstr(grow.Cells(colICode).Value)
                        dr("Temp_Code") = clsCommon.myCstr(grow.Cells(colTemplete).Value)
                        Dt_Temp_Id.Rows.Add(dr)
                    Else
                        If clsCommon.CompairString(clsCommon.myCstr(drr(0).Item(1)), clsCommon.myCstr(grow.Cells(colTemplete).Value)) <> CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(Me, "Use same Template Code for Item [" & clsCommon.myCstr(grow.Cells(colICode).Value) & "]")
                            Return False
                        End If
                    End If
                Else
                    Dim dr As DataRow = Dt_Temp_Id.NewRow()
                    dr("Item_COde") = clsCommon.myCstr(grow.Cells(colICode).Value)
                    dr("Temp_Code") = clsCommon.myCstr(grow.Cells(colTemplete).Value)
                    Dt_Temp_Id.Rows.Add(dr)
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlAcqType.Text), "Assembled") = CompairStringResult.Equal Then
                If ValidateDistributeAmount() = False Then
                    clsCommon.MyMessageBoxShow(Me, "Invalid amount distribution of distributed items in remaining items.")
                    Return False
                End If
            End If


            If ShowCapexCodeandSubCode Then
                If grow.Cells(colIsCategory).Value = True Then

                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colCategoryType).Value), "") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "please select Capex Type.")
                        Return False
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCapexCode).Value)) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "please select capex code. At Line No" & (grow.Index + 1) & "")
                        Return False
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colCategoryType).Value), "Regular") = CompairStringResult.Equal AndAlso grow.Cells(colIsCategory).Value = True Then
                        Dim dtcheck As New DataTable()
                        dtcheck = ChkLimitBudget(grow.Index)



                        If dtcheck IsNot Nothing AndAlso dtcheck.Rows.Count > 0 Then
                            If clsCommon.myCdbl(dtcheck.Rows(1)(0)) < clsCommon.myCdbl(grow.Cells(colNetAmt).Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Warning:  Amount exceed budget amount but under tolerence limit.At Line No" & (grow.Index + 1) & "")
                            End If
                            If clsCommon.myCdbl(dtcheck.Rows(2)(0)) < clsCommon.myCdbl(grow.Cells(colNetAmt).Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Amount exceed budget amount and above tolerence limit.At Line No" & (grow.Index + 1) & "")
                                Return False
                            End If


                        End If
                    End If
                End If
            End If

            '====================end here================
        Next
        'Dim ShowCapexCodeandSubCode As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UDLCapexAcquisionEntry, clsFixedParameterCode.UDLCapexAcquisionEntry, Nothing)) = "1", True, False))
        'If ShowCapexCodeandSubCode = True Then
        '    If clsCommon.myLen(fndcapexsubcode.Value) <= 0 Then
        '        clsCommon.MyMessageBoxShow("please select capex sub code.")
        '        fndcapexcode.Focus()
        '        Return False
        '    End If
        '    If clsCommon.myCdbl(lbl_rebudgetamtwithtolerence.Text) < clsCommon.myCdbl(lblTotalAmt.Text) Then
        '        clsCommon.MyMessageBoxShow("Document amount exceed budget amount and above tolerence limit.")
        '        Return False
        '    End If
        '    If clsCommon.myCdbl(lbl_rebudgetamt.Text) < clsCommon.myCdbl(lblTotalAmt.Text) Then
        '        clsCommon.MyMessageBoxShow("Warning: Document amount exceed budget amount but under tolerence limit.")
        '    End If
        'End If

        '=============================added by preeti gupta==================================




        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData(False, ChekBtnPost)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean, ByVal ChekBtnPost As Boolean) As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsAcquisitionHead()
                'Dim TotalOpening As Double = 0
                If clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myLen(grow.Cells(colAssetName).Value) <= 0 Then
                            Continue For
                        End If
                        If clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
                            'If (clsCommon.myLen(grow.Cells(colICode).Value)) <= 0 Then
                            '    clsCommon.MyMessageBoxShow("Item code cannot be left blank for Assembled at line no " & (grow.Index + 1) & ".")
                            '    Exit Function
                            'End If
                        End If
                        If ImportMultipleAssetAssembled = False Then
                            '    TotalOpening = +grow.Cells(ColBookSourceValue).Value
                            '    grow.Cells(ColBookSourceValue).Value = If(chkOpening.Checked, TotalOpening, 0) + clsAcquisitionHead.GetAssembleAmount(grow.Cells(colAssetID).Value, Nothing)
                            'Else

                            grow.Cells(ColBookSourceValue).Value = If(chkOpening.Checked, clsCommon.myCdbl(txtAssembleOpeningAmt.Text), 0) + clsAcquisitionHead.GetAssembleAmount(grow.Cells(colAssetID).Value, Nothing)
                        End If

                        grow.Cells(ColBookSourceOriginalValue).Value = grow.Cells(ColBookSourceValue).Value
                        If clsCommon.myCdbl(grow.Cells(ColBookSalvageRate).Value) > 0 Then
                            grow.Cells(ColBookSalvageValue).Value = grow.Cells(ColBookSourceValue).Value * clsCommon.myCdbl(grow.Cells(ColBookSalvageRate).Value) / 100
                        End If
                        UpdateCurrentRow(grow.Index)
                        GetCurrentRowTotalTaxAmt(grow.Index)
                    Next
                    UpdateAllTotals()
                End If




                obj.Acquisition_Type = ddlAcqType.Text
                obj.Acquisition_Code = txtDocNo.Value
                obj.Acquisition_Date = txtDate.Value
                'obj.PO_No = ""
                obj.SRN_No = txtSRNNo.Value
                obj.PI_No = fndPINo.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Description = txtDesc.Text
                obj.Vendor_Invoice_No = clsCommon.myCstr(txtVendorInvoiceNo.Text)
                obj.Remarks = txtRemarks.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Is_Visi_Type = chkVisiType.Checked
                obj.Description = txtDesc.Text
                obj.Total_Amt = clsCommon.myCdbl(lblTotalAmt.Text)
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Net_Amt = clsCommon.myCdbl(lblNetAmt.Text)
                obj.Loc_Code = txtLocation.Value
                obj.Tax_Group = txtTaxGroup.Value
                obj.Templete_Code = fndTemplateCode.Value
                obj.IS_Assemble = IIf(clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal, "1", "0") 'Me.ChkISAssemble.Checked
                obj.Balance_Amt = clsCommon.myCdbl(lblNetAmt.Text)
                obj.Opening_Assemble = chkOpening.Checked
                obj.Opening_Assemble_Amt = clsCommon.myCdbl(txtAssembleOpeningAmt.Text)
                ''==
                obj.Opening_Direct = chkOpeningDirect.Checked
                If rbtnnew.IsChecked AndAlso Not rbtnold.IsChecked Then
                    obj.statusnewold = "NEW"
                ElseIf rbtnold.IsChecked AndAlso Not rbtnnew.IsChecked Then
                    obj.statusnewold = "OLD"
                End If

                obj.CapexSub_Code = fndcapexsubcode.Value
                obj.Capex_Code = fndcapexcode.Value
                obj.Tax_Recoverable = txtRecAmt.Text
                obj.Tax_Non_Recoverable = txtNonRecAmt.Text
                '===================Added by preeti [25/04/2017]===================
                '===================================================================

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
                If (gvAC.Rows.Count > 0) Then
                    If clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                        obj.Add_Charge_Name1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACName).Value)
                        obj.Add_Charge_Amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 1) Then
                    If clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                        obj.Add_Charge_Name2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACName).Value)
                        obj.Add_Charge_Amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 2) Then
                    If clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                        obj.Add_Charge_Name3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACName).Value)
                        obj.Add_Charge_Amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 3) Then
                    If clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                        obj.Add_Charge_Name4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACName).Value)
                        obj.Add_Charge_Amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 4) Then
                    If clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                        obj.Add_Charge_Name5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACName).Value)
                        obj.Add_Charge_Amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 5) Then
                    If clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                        obj.Add_Charge_Name6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACName).Value)
                        obj.Add_Charge_Amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 6) Then
                    If clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                        obj.Add_Charge_Name7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACName).Value)
                        obj.Add_Charge_Amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 7) Then
                    If clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                        obj.Add_Charge_Name8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACName).Value)
                        obj.Add_Charge_Amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 8) Then
                    If clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                        obj.Add_Charge_Name9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACName).Value)
                        obj.Add_Charge_Amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 9) Then
                    If clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                        obj.Add_Charge_Code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                        obj.Add_Charge_Name10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACName).Value)
                        obj.Add_Charge_Amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                    End If
                End If
                obj.Total_Add_Charge = clsCommon.myCdbl(lblAddCharges.Text)

                obj.Arr = New List(Of clsAcquisitionDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colcheck).Value) = True Then
                        Dim objTr As New clsAcquisitionDetail()
                        objTr.Acquisition_Code = obj.Acquisition_Code
                        objTr.SNo = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                        objTr.Asset_Serial_No = clsCommon.myCstr(grow.Cells(colAssetSerialID).Value)
                        objTr.Asset_Name = clsCommon.myCstr(grow.Cells(colAssetName).Value)
                        objTr.Templete_Code = clsCommon.myCstr(grow.Cells(colTemplete).Value)
                        objTr.Templete_Name = clsCommon.myCstr(grow.Cells(colTempleteName).Value)
                        objTr.Category_code = clsCommon.myCstr(grow.Cells(colCategoryCode).Value)
                        objTr.Category_Name = clsCommon.myCstr(grow.Cells(colCategoryName).Value)
                        objTr.Group_Code = clsCommon.myCstr(grow.Cells(colGroupCode).Value)
                        objTr.Group_Code_Name = clsCommon.myCstr(grow.Cells(colGroupName).Value)
                        objTr.AcSet_Code = clsCommon.myCstr(grow.Cells(colAccountSetCode).Value)
                        objTr.AcSet_Code_Name = clsCommon.myCstr(grow.Cells(colAccountSetName).Value)
                        If ApplyFinancialCostCenter = True Then
                            objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)
                        End If
                        objTr.CostCenter_Code = clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)
                        objTr.CostCenter_Name = clsCommon.myCstr(grow.Cells(colCostCenterName).Value)
                        objTr.Acqusition_Date = clsCommon.myCDate(grow.Cells(colAcquisitionDate).Value)
                        objTr.Dep_Method_Code = clsCommon.myCstr(grow.Cells(colDepMethod).Value)
                        objTr.Dep_Method_Name = clsCommon.myCstr(grow.Cells(colDepMethodName).Value)
                        objTr.Dep_Method_Tax_Code = clsCommon.myCstr(grow.Cells(colDepMethodTax).Value)
                        objTr.Dep_Method_Tax_Name = clsCommon.myCstr(grow.Cells(colDepMethodNameTax).Value)
                        objTr.Dep_Period_Code = clsCommon.myCstr(grow.Cells(colDepPeriodCode).Value)
                        objTr.Dep_Period_Name = clsCommon.myCstr(grow.Cells(colDepPeriodName).Value)

                        '' new column
                        'objTr.Put_To_Use = If(clsCommon.myCstr(grow.Cells(colPutToUse).Value), True, False)
                        objTr.Put_To_Use = If(clsCommon.myCBool(clsCommon.myCdbl(grow.Cells(colPutToUse).Value)), True, False)
                        objTr.Start_Date = clsCommon.myCDate(grow.Cells(colStartDate).Value)

                        objTr.Dep_Rate = clsCommon.myCdbl(grow.Cells(colDepRate).Value)
                        objTr.Book_Estimated_Life = clsCommon.myCdbl(grow.Cells(ColBookEstimatedLife).Value)
                        objTr.Book_Source_value = clsCommon.myCdbl(grow.Cells(ColBookSourceValue).Value)


                        objTr.Book_Source_Original_value = clsCommon.myCdbl(grow.Cells(ColBookSourceOriginalValue).Value)
                        objTr.Book_Salvage_Value = clsCommon.myCdbl(grow.Cells(ColBookSalvageValue).Value)
                        objTr.Tax_Recoverable = clsCommon.myCdbl(grow.Cells(colTaxRecoverable).Value)
                        objTr.Tax_Non_Recoverable = clsCommon.myCdbl(grow.Cells(colTaxNonRecoverable).Value)

                        objTr.Book_Salvage_Rate = clsCommon.myCdbl(grow.Cells(ColBookSalvageRate).Value)
                        If clsCommon.myCdbl(grow.Cells(ColBookSalvageValue).Value) <= 0 Then
                            objTr.Book_Salvage_Value = objTr.Book_Source_value * objTr.Book_Salvage_Rate / 100
                        End If

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
                        objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                        objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colNetAmt).Value)
                        '=================
                        If clsCommon.myCstr(grow.Cells(colTaxDepType).Value) = "Formula" Then
                            objTr.Tax_Dep_Type = "F"
                        Else
                            objTr.Tax_Dep_Type = "M"
                        End If
                        If clsCommon.myCstr(grow.Cells(colBookDepType).Value) = "Formula" Then
                            objTr.Book_Dep_Type = "F"
                        Else
                            objTr.Book_Dep_Type = "M"
                        End If

                        '===================
                        objTr.Asset_Specification = clsCommon.myCstr(grow.Cells(colAssetSpecificaion).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Name = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Dep_Tax_Rate = clsCommon.myCdbl(grow.Cells(colDepTaxRate).Value)
                        objTr.Is_Assembled = IIf(clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal, "1", "0")
                        ''====shivani

                        Dim AssetPrefix As String = clsDBFuncationality.getSingleValue("Select Description From TSPL_FIXED_PARAMETER where Code ='AssetGroupPrefix'")
                        If AssetPrefix = "1" Then
                            objTr.Prefix_Type = "G"
                        Else
                            objTr.Prefix_Type = "C"
                        End If
                        If clsCommon.myLen(objTr.Asset_Code) > 0 Then
                            objTr.Prefix_Type = "M"
                        End If
                        objTr.SRNQty = clsCommon.myCdbl(grow.Cells(colSRNQty).Value)
                        objTr.SRN_Rate = clsCommon.myCdbl(grow.Cells(colSRNRate).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.PI_No = clsCommon.myCstr(grow.Cells(colPI).Value)
                        objTr.SRN_No = clsCommon.myCstr(grow.Cells(colSRNNo).Value)

                        '==============
                        '==================dded by preeti gupta=========
                        objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                        objTr.CapexSub_Code = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)
                        '==========================added by preeti gupta======================
                        objTr.IsCapex = clsCommon.myCdbl(grow.Cells(colIsCategory).Value)
                        objTr.CapexType = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                        objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                        objTr.CapexSub_Code = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)

                        '=====================================================================
                        ''-----------------14/04/2017---------additional charge itemwise------------------------------------------
                        objTr.ItemAdd_Charge_Code1 = clsCommon.myCstr(grow.Cells(colItemACCode1).Value)
                        objTr.ItemAdd_Charge_Code2 = clsCommon.myCstr(grow.Cells(colItemACCode2).Value)
                        objTr.ItemAdd_Charge_Code3 = clsCommon.myCstr(grow.Cells(colItemACCode3).Value)
                        objTr.ItemAdd_Charge_Code4 = clsCommon.myCstr(grow.Cells(colItemACCode4).Value)
                        objTr.ItemAdd_Charge_Code5 = clsCommon.myCstr(grow.Cells(colItemACCode5).Value)
                        objTr.ItemAdd_Charge_Code6 = clsCommon.myCstr(grow.Cells(colItemACCode6).Value)
                        objTr.ItemAdd_Charge_Code7 = clsCommon.myCstr(grow.Cells(colItemACCode7).Value)
                        objTr.ItemAdd_Charge_Code8 = clsCommon.myCstr(grow.Cells(colItemACCode8).Value)
                        objTr.ItemAdd_Charge_Code9 = clsCommon.myCstr(grow.Cells(colItemACCode9).Value)
                        objTr.ItemAdd_Charge_Code10 = clsCommon.myCstr(grow.Cells(colItemACCode10).Value)
                        objTr.ItemAdd_Calc_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value)
                        objTr.ItemAdd_Calc_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value)
                        objTr.ItemAdd_Calc_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value)
                        objTr.ItemAdd_Calc_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value)
                        objTr.ItemAdd_Calc_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value)
                        objTr.ItemAdd_Calc_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value)
                        objTr.ItemAdd_Calc_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value)
                        objTr.ItemAdd_Calc_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value)
                        objTr.ItemAdd_Calc_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value)
                        objTr.ItemAdd_Calc_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
                        objTr.ItemAdd_Org_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACAmount1).Value)
                        objTr.ItemAdd_Org_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACAmount2).Value)
                        objTr.ItemAdd_Org_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACAmount3).Value)
                        objTr.ItemAdd_Org_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACAmount4).Value)
                        objTr.ItemAdd_Org_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACAmount5).Value)
                        objTr.ItemAdd_Org_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACAmount6).Value)
                        objTr.ItemAdd_Org_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACAmount7).Value)
                        objTr.ItemAdd_Org_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACAmount8).Value)
                        objTr.ItemAdd_Org_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACAmount9).Value)
                        objTr.ItemAdd_Org_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACAmount10).Value)
                        objTr.Total_ItemAdd_Charge = clsCommon.myCdbl(grow.Cells(colItemTotalAdditionalCharge).Value)
                        objTr.Depreciated_Value = clsCommon.myCdbl(grow.Cells(colDepreciatedvalue).Value)
                        ''=======================================================================================

                        '===============================================
                        If clsCommon.myLen(objTr.Asset_Name) > 0 Or clsCommon.myLen(objTr.Asset_Code) > 0 Or clsCommon.myLen(obj.SRN_No) > 0 Then
                            obj.Arr.Add(objTr)
                        End If

                    End If
                Next

                '==============
                If objRemittance IsNot Nothing Then
                    obj.RemittanceObject = New clsRemittance()
                    obj.RemittanceObject = objRemittance
                    obj.RemittanceObject.Vendor_Invoice_No = txtVendorInvoiceNo.Text
                    obj.TDS_Base_Actual_Amount = objRemittance.Actual_TDS_Base
                    obj.TDS_Base_Calculated_Amount = objRemittance.Calculated_TDS_Base
                    obj.TDS_Percentage = objRemittance.TDS_Per
                    obj.TDS_Actual_Amount = objRemittance.Actual_Total_TDS
                    obj.TDS_Calculated_Amount = objRemittance.Calculated_Total_TDS
                    obj.Nature_of_deduction = objRemittance.Deduction_Code
                    obj.Branch_Code = objRemittance.Branch_Code
                    obj.Balance_Amt = clsCommon.myCdbl(lblNetAmt.Text) - objRemittance.Actual_Total_TDS
                    obj.Section_Code = objRemittance.Section_Code


                End If
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill/Select at list one Item", Me.Text)
                    Return False
                End If
                '' save assemble cost detail
                Dim objTrc As New clsAssetAssembleDetail
                obj.ArrAssemble = New List(Of clsAssetAssembleDetail)
                If clsCommon.CompairString(obj.Acquisition_Type, "Assembled") = CompairStringResult.Equal Then
                    For Each grow As GridViewRowInfo In gvAssemble.Rows
                        objTrc = New clsAssetAssembleDetail
                        objTrc.Acquisition_Code = obj.Acquisition_Code
                        objTrc.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                        objTrc.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                        objTrc.Distribute = If(clsCommon.myCBool(grow.Cells(colcheck).Value) = True, "Y", "N")
                        objTrc.Distribute_Amount = clsCommon.myCdbl(grow.Cells(colDistributeAmount).Value)
                        objTrc.Document_Code = clsCommon.myCstr(grow.Cells(colDocumentCode).Value)
                        objTrc.Document_Date = clsCommon.myCDate(grow.Cells(colDocumentDate).Value)
                        objTrc.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        'sanjay
                        'objTrc.Hierarchy = clsCommon.myCstr(grow.Cells(colHierarchy).Value)
                        'objTrc.CostCenter = clsCommon.myCstr(grow.Cells(colCostCenter).Value)
                        'sanjay
                        objTrc.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colItemNetAmount).Value)
                        objTrc.Item_No = clsCommon.myCstr(grow.Cells(colItemNo).Value)
                        objTrc.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTrc.Type = clsCommon.myCstr(grow.Cells(colType).Value)
                        objTrc.Total_Amount = clsCommon.myCdbl(grow.Cells(colTotalAmount).Value)
                        obj.ArrAssemble.Add(objTrc)
                    Next
                End If
                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry, isDoAbandomentNo)
                '' make entry in tspl_visi_master if acquisition is of VISI Type
                'If chkVisiType.Checked And ChekBtnPost = True Then
                '    If isSaved Then
                '        For Each rw As GridViewRowInfo In gv1.Rows
                '            If clsCommon.myLen(rw.Cells(colAssetName).Value) <= 0 Then
                '                Continue For
                '            End If
                '            Dim frm As New FrmAssetServiceMaster
                '            frm.txtassetcode__MYValidating(Nothing, Nothing, False)
                '            frm.txtassetcode.Value = rw.Cells(colICode).Value
                '            frm.txtsno.Text = rw.Cells(colAssetID).Value
                '            frm.txttagno.Text = rw.Cells(colAssetID).Value
                '            frm.FillItemStructure()
                '            frm.SaveData(True)
                '        Next

                '    End If
                'End If
                LoadData(obj.Acquisition_Code, NavigatorType.Current)
                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnChangeDepDetail.Enabled = False
            btnDelete.Enabled = True
            isInsideLoadData = True
            ''isNewEntry = False
            ''btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            objRemittance = Nothing
            Dim obj As New clsAcquisitionHead()
            obj = clsAcquisitionHead.GetData(strCode, NavTyep)



            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Acquisition_Code) > 0) Then
                If (obj.RemittanceObject IsNot Nothing) Then
                    objRemittance = New clsRemittance()
                    objRemittance = obj.RemittanceObject
                    btnTDSDetail.Enabled = True
                End If
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnChangeDepDetail.Enabled = True
                    btnDelete.Enabled = False
                    If CostCenterAndHirerachyCodeUpdateAfterPost = True Then
                        butCostCenterAndHirerachy_Update_AfterPost.Visible = True
                    End If
                End If
                isNewEntry = False
                btnSave.Text = "Update"
                UsLock1.Status = obj.Status
                ddlAcqType.Text = obj.Acquisition_Type

                ddlAcqType.Enabled = False
                If clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
                    txtVendorNo.Enabled = False
                Else

                    txtVendorNo.Enabled = True

                End If
                txtDocNo.Value = obj.Acquisition_Code
                txtDate.Value = obj.Acquisition_Date
                'txtSRNNo.Value = obj.PO_No
                fndPINo.Value = obj.PI_No
                If clsCommon.myLen(fndPINo.Value) > 0 Then
                    txtLocation.Enabled = False
                    lblPIDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select convert (varchar, PI_Date,103) as PI_Date from TSPL_PI_HEAD where PI_No = '" + clsCommon.myCstr(fndPINo.Value) + "'"))
                Else
                    lblPIDate.Text = ""
                End If
                txtSRNNo.Value = obj.SRN_No
                If clsCommon.myLen(txtSRNNo.Value) > 0 Then
                    lblSRNDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select convert (varchar, SRN_Date,103) as SRN_Date from TSPL_SRN_HEAD where srn_no = '" + clsCommon.myCstr(txtSRNNo.Value) + "'"))
                Else
                    lblSRNDate.Text = ""
                End If
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtDesc.Text = obj.Description
                txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
                txtRemarks.Text = obj.Remarks
                chkOnHold.Checked = obj.On_Hold
                chkVisiType.Checked = obj.Is_Visi_Type
                txtDesc.Text = obj.Description
                lblTotalAmt.Text = clsCommon.myFormat(obj.Total_Amt)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblNetAmt.Text = clsCommon.myFormat(obj.Net_Amt)

                txtLocation.Value = obj.Loc_Code
                txtTaxGroup.Value = obj.Tax_Group
                fndTemplateCode.Value = obj.Templete_Code
                lblLocation.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
                lblTemplateDesc.Text = ClsTemplateMaster.GetName(obj.Templete_Code, Nothing)
                fndcapexcode.Value = obj.Capex_Code
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
                fndcapexsubcode.Value = obj.CapexSub_Code
                If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                    lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                    lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, obj.Acquisition_Code, Nothing, "AC-Capex")
                    lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, obj.Acquisition_Code, Nothing, "AC-Capex")
                End If



                Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(obj.Vendor_Code)
                If objVendor IsNot Nothing Then
                    btnTDSDetail.Enabled = True
                Else
                    btnTDSDetail.Enabled = False
                End If

                If clsCommon.CompairString(obj.statusnewold, "NEW") = CompairStringResult.Equal Then
                    rbtnnew.IsChecked = True
                End If
                If clsCommon.CompairString(obj.statusnewold, "OLD") = CompairStringResult.Equal Then
                    rbtnold.IsChecked = True
                End If
                rbtnnew.Enabled = False
                rbtnold.Enabled = False

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
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
                                Exit For
                            End If
                        Next
                    End If
                End If

                '=========Added by preeti gupta======================================

                gvAC.Rows.Clear()

                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt1

                End If
                If (clsCommon.myLen(obj.Add_Charge_Code2) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt2
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code3) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt3
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code4) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt4
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code5) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt5
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code6) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt6
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code7) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt7
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code8) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt8
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code9) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt9
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code10) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt10
                End If

                lblAddCharges.Text = clsCommon.myFormat(obj.Total_Add_Charge)
                txtRecAmt.Text = obj.Tax_Recoverable
                txtNonRecAmt.Text = obj.Tax_Non_Recoverable
                txtAssetAmount.Text = obj.Total_Amt + obj.Tax_Non_Recoverable
                If Not obj.Post_Date Is Nothing Then
                    dtpPostingDate.Value = obj.Post_Date
                    txtAssembleCode.Text = obj.Assemble_Code
                End If
                '====================================================================
                '  Dim ArrAssets As New ArrayList
                Dim ArrAssets As New ArrayList
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsAcquisitionDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colcheck).Value = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetID).Value = objTr.Asset_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOldAssetID).Value = objTr.Asset_Code '' only  for calculation purpose
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetSerialID).Value = objTr.Asset_Serial_No
                        If clsCommon.CompairString(obj.Acquisition_Type, "Assembled") = CompairStringResult.Equal Then
                            Dim arr As New ArrayList
                            arr.Add(objTr.Asset_Code)
                            Dim qry As String = clsItemIssueToAssembledAsset.GetAssembleQuery(arr, False)
                            qry = "select count(*) from (" & qry & ") Final "
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetID).ReadOnly = True
                            End If
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetID).ReadOnly = True
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = objTr.Asset_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTemplete).Value = objTr.Templete_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTempleteName).Value = objTr.Templete_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryCode).Value = objTr.Category_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryName).Value = objTr.Category_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGroupCode).Value = objTr.Group_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGroupName).Value = objTr.Group_Code_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetCode).Value = objTr.AcSet_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetName).Value = objTr.AcSet_Code_Name
                        If ApplyFinancialCostCenter = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = objTr.Hirerachy_Code
                            If clsCommon.myLen(objTr.Hirerachy_Code) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Level from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                            End If
                        End If


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = objTr.CostCenter_Code
                        If clsCommon.myLen(objTr.CostCenter_Code) > 0 AndAlso ApplyFinancialCostCenter = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Name from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.CostCenter_Code + "'"))  ' objTr.CostCenter_Name
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterName).Value = objTr.CostCenter_Name
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAcquisitionDate).Value = objTr.Acqusition_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethod).Value = objTr.Dep_Method_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodName).Value = objTr.Dep_Method_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodTax).Value = objTr.Dep_Method_Tax_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodNameTax).Value = objTr.Dep_Method_Tax_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepPeriodCode).Value = objTr.Dep_Period_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepPeriodName).Value = objTr.Dep_Period_Name

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPutToUse).Value = If(objTr.Put_To_Use = True, "1", "0")
                        If objTr.Put_To_Use = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colStartDate).Value = objTr.Start_Date
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colStartDate).Value = Nothing
                        End If


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRate).Value = objTr.Dep_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookEstimatedLife).Value = objTr.Book_Estimated_Life
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSourceValue).Value = objTr.Book_Source_value
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSourceOriginalValue).Value = objTr.Book_Source_Original_value
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRecoverable).Value = objTr.Tax_Recoverable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxNonRecoverable).Value = objTr.Tax_Non_Recoverable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSalvageRate).Value = objTr.Book_Salvage_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSalvageValue).Value = objTr.Book_Salvage_Value
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNetAmt).Value = objTr.Item_Net_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetSpecificaion).Value = objTr.Asset_Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepTaxRate).Value = objTr.Dep_Tax_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsAssembled).Value = objTr.Is_Assembled
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBookDepType).Value = objTr.Book_Dep_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxDepType).Value = objTr.Tax_Dep_Type
                        '=====================
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNQty).Value = objTr.SRNQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = objTr.SRN_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNRate).Value = objTr.SRN_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPI).Value = objTr.PI_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.Unit_Code
                        '==================added by preeti gupta===========================================
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsCategory).Value = objTr.IsCapex
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objTr.CapexType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objTr.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objTr.CapexSub_Code

                        '===================================================================================
                        ''-----------------14/04/2017---------additional charge itemwise------------------------------------------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode1).Value = objTr.ItemAdd_Charge_Code1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode2).Value = objTr.ItemAdd_Charge_Code2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode3).Value = objTr.ItemAdd_Charge_Code3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode4).Value = objTr.ItemAdd_Charge_Code4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode5).Value = objTr.ItemAdd_Charge_Code5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode6).Value = objTr.ItemAdd_Charge_Code6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode7).Value = objTr.ItemAdd_Charge_Code7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode8).Value = objTr.ItemAdd_Charge_Code8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode9).Value = objTr.ItemAdd_Charge_Code9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode10).Value = objTr.ItemAdd_Charge_Code10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount1).Value = objTr.ItemAdd_Calc_Charge_Amt1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount2).Value = objTr.ItemAdd_Calc_Charge_Amt2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount3).Value = objTr.ItemAdd_Calc_Charge_Amt3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount4).Value = objTr.ItemAdd_Calc_Charge_Amt4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount5).Value = objTr.ItemAdd_Calc_Charge_Amt5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount6).Value = objTr.ItemAdd_Calc_Charge_Amt6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount7).Value = objTr.ItemAdd_Calc_Charge_Amt7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount8).Value = objTr.ItemAdd_Calc_Charge_Amt8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount9).Value = objTr.ItemAdd_Calc_Charge_Amt9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount10).Value = objTr.ItemAdd_Calc_Charge_Amt10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount1).Value = objTr.ItemAdd_Org_Charge_Amt1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount2).Value = objTr.ItemAdd_Org_Charge_Amt2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount3).Value = objTr.ItemAdd_Org_Charge_Amt3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount4).Value = objTr.ItemAdd_Org_Charge_Amt4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount5).Value = objTr.ItemAdd_Org_Charge_Amt5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount6).Value = objTr.ItemAdd_Org_Charge_Amt6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount7).Value = objTr.ItemAdd_Org_Charge_Amt7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount8).Value = objTr.ItemAdd_Org_Charge_Amt8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount9).Value = objTr.ItemAdd_Org_Charge_Amt9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount10).Value = objTr.ItemAdd_Org_Charge_Amt10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTotalAdditionalCharge).Value = objTr.Total_ItemAdd_Charge
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepreciatedvalue).Value = objTr.Depreciated_Value
                        ''=======================================================================================

                        '============
                        'ArrAssets.Add(objTr.Asset_Code)
                        ArrAssets.Add(objTr.Asset_Code)

                    Next
                    ChkISAssemble.Checked = obj.IS_Assemble
                    chkOpening.Checked = obj.Opening_Assemble
                    txtAssembleOpeningAmt.Text = obj.Opening_Assemble_Amt
                    chkOpeningDirect.Checked = obj.Opening_Direct
                    '---- update Function 22/03/2017
                    If clsCommon.CompairString(obj.Acquisition_Type, "Assembled") = CompairStringResult.Equal Then
                        chkOpening.Visible = True
                        txtAssembleOpeningAmt.Visible = True
                        chkOpening.Enabled = True
                        txtAssembleOpeningAmt.Enabled = True
                        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
                        Dim objList As New List(Of clsAssetAssembleDetail)
                        objList = clsAcquisitionHead.GetAssembleDetail(txtDocNo.Value, ArrAssets, Nothing)
                        gvAssemble.Rows.Clear()

                        If Not objList Is Nothing AndAlso objList.Count > 0 Then
                            chkOpening.Enabled = False
                            txtAssembleOpeningAmt.Enabled = False
                            For Each objtr As clsAssetAssembleDetail In objList
                                gvAssemble.Rows.AddNew()
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colLineNo).Value = objtr.Line_No
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colAssetID).Value = objtr.Asset_Code
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colType).Value = objtr.Type
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colDocumentCode).Value = objtr.Document_Code
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colDocumentDate).Value = objtr.Document_Date
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colItemNo).Value = objtr.Item_No
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colItemDesc).Value = objtr.Item_Desc
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colHierarchy).Value = objtr.Hierarchy
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colCostCenter).Value = objtr.CostCenter
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colAmount).Value = objtr.Amount
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colTotalTaxAmount).Value = objtr.Total_Tax_Amt
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colItemNetAmount).Value = objtr.Item_Net_Amt
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colcheck).Value = If(objtr.Distribute = "Y", True, False)

                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colDistributeAmount).Value = objtr.Distribute_Amount
                                If gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colcheck).Value = True Then
                                    gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colDistributeAmount).ReadOnly = True
                                Else
                                    gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colDistributeAmount).ReadOnly = False
                                End If
                                If objtr.Item_Net_Amt < 0 Then
                                    gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colcheck).ReadOnly = True
                                    gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colDistributeAmount).ReadOnly = True
                                End If
                                gvAssemble.Rows(gvAssemble.Rows.Count - 1).Cells(colTotalAmount).Value = objtr.Total_Amount
                            Next
                        End If
                        'gvAssemble.DataSource = clsAcquisitionHead.GetAssembledInfo(ArrAssets, Nothing)
                        'gvAssemble.BestFitColumns()
                        'gvAssemble.AllowDeleteRow = True
                        'gvAssemble.AllowAddNewRow = False
                        'gvAssemble.ShowGroupPanel = False
                        'gvAssemble.AllowColumnReorder = False
                        'gvAssemble.AllowRowReorder = False
                        'gvAssemble.EnableSorting = False
                        'gvAssemble.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                        'gvAssemble.MasterTemplate.ShowRowHeaderColumn = False
                        'gvAssemble.TableElement.TableHeaderHeight = 40
                        'gvAssemble.ReadOnly = True
                        'ReStoreGridLayout()
                    ElseIf clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal Then
                        chkOpeningDirect.Visible = True
                        chkOpeningDirect.Enabled = True
                    End If
                    '---- End
                    If clsCommon.CompairString(ddlAcqType.Text, "Asset") = CompairStringResult.Equal Then
                        txtTaxGroup.Enabled = False
                        gvAC.Rows.AddNew()
                        gvAC.ReadOnly = True
                    ElseIf clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
                        txtTaxGroup.Enabled = False
                        'UpdateAllTotals()
                    Else
                        txtTaxGroup.Enabled = True
                        gvAC.Rows.AddNew()
                        gvAC.ReadOnly = False
                    End If
                    If obj.Status = ERPTransactionStatus.Pending And clsCommon.myLen(obj.SRN_No) <= 0 Then
                        gv1.Rows.AddNew()
                    End If
                End If
                SetitemWiseTaxOnlySetting()
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadAssembleDetail()
        ReStoreGridLayout()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked And clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                'If rbtnTaxCalAutomatic.IsChecked AndAlso clsCommon.CompairString(gv2.CurrentCell.ColumnInfo.Name, colTTaxRate) = CompairStringResult.Equal Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndTaxfnd", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
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

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 Then
                            'If (clsCommon.myLen(grow.Cells(colICode).Value)) <= 0 Then
                            '    clsCommon.MyMessageBoxShow("Item code cannot be left blank for Assembled at line no " & (grow.Index + 1) & ".")
                            '    Exit Sub
                            'End If
                        End If

                    Next
                End If
                If clsCommon.CompairString(ddlAcqType.Text, "Assembled") <> CompairStringResult.Equal Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 Then
                            If (clsCommon.myCdbl(grow.Cells(colNetAmt).Value)) <= 0 Then
                                If (clsCommon.myLen(grow.Cells(colAssetName).Value)) > 0 Then
                                    clsCommon.MyMessageBoxShow(Me, "Net Amount cannot be left blank  at line no " & (grow.Index + 1) & ".")
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                End If

                SavingData(True)
                If (clsAcquisitionHead.PostData(Form_ID, txtDocNo.Value, True, IIf(rbtnnew.IsChecked, "NEW", "OLD"))) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
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
                If (clsAcquisitionHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
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
            Dim qst As String = "select count(*) from TSPL_ACQUISITION_HEAD where Acquisition_Code='" + txtDocNo.Value + "'"
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
        'Dim qry As String = "select Acquisition_Code as Code,convert (varchar(10), Acquisition_Date,103) as Date, Vendor_Code as Vendor,Net_Amt as Amount,case when Status='0' then 'Pending' else 'Approved' end as [Status],Acquisition_Type as [Acquisition Type]  from TSPL_ACQUISITION_HEAD"
        Dim qry As String = " select TSPL_ACQUISITION_HEAD.Acquisition_Code as Code,convert (varchar(10), TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as Date, TSPL_ACQUISITION_HEAD.Vendor_Code as Vendor,TSPL_ACQUISITION_HEAD.Net_Amt as Amount,case when TSPL_ACQUISITION_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_ACQUISITION_HEAD.Acquisition_Type as [Acquisition Type] ,  TSPL_ACQUISITION_HEAD.PI_No, convert (varchar, TSPL_PI_HEAD.PI_Date,103) as PI_Date, TSPL_ACQUISITION_HEAD.SRN_NO,convert (varchar, TSPL_SRN_HEAD.SRN_Date,103) as SRN_Date from TSPL_ACQUISITION_HEAD left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No = TSPL_ACQUISITION_HEAD.PI_No  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD .SRN_No = TSPL_ACQUISITION_HEAD.SRN_NO "
        LoadData(clsCommon.ShowSelectForm("AcquiFndd", qry, "Code", " Acquisition_Type<>'Merge' ", txtDocNo.Value, "TSPL_ACQUISITION_HEAD.Acquisition_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Sub SaveLayout1()
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Sub DeleteLayout()
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        txtTaxGroup.Value = clsCommon.ShowSelectForm("POTaxGroupfndd", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        SetTaxDetails()

    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, txtLocation.Value, True)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If clsCommon.myLen(txtSRNNo.Value) > 0 Then
                TaxFillForSRN()
            Else
                If (dt.Rows.Count > 10) Then
                    MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                    Return
                End If
                lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
                For Each dr As DataRow In dt.Rows
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                    If rbtnTaxCalAutomatic.IsChecked Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                    End If
                Next
                SetitemWiseTaxSetting(True, False)
            End If

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

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        ''richa agarwal UDL/24/01/19-000261  clsCommon.myLen(txtSRNNo.Value) <= 0 correct condition 
        If clsCommon.myLen(txtSRNNo.Value) <= 0 Then
            Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, txtLocation.Value, True)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                If isForCurrentRow Then
                    BlankTaxDetails(gv1.CurrentRow.Index)
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetID)) > 0 Then
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
                        If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colAssetID)) > 0 Then
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
        End If



    End Sub

    Sub SetitemWiseTaxOnlySetting()

        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, txtLocation.Value, True)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colAssetID)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
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

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER"
        Dim whr = " TSPL_VENDOR_MASTER.Status='N' "
        txtVendorNo.Value = clsCommon.ShowSelectForm("POVendorFndr", qry, "Code", whr, txtVendorNo.Value, "Code", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))

            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
        Else
            lblVendorName.Text = ""

            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""
        End If
        SetTaxDetails()
        SetVendorTDSDetails()
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            Dim grow As GridViewRowInfo = gv1.CurrentRow
            If clsCommon.myLen(fndTemplateCode.Value) <= 0 Then
                RadMessageBox.Show("Please Select Template Code First")
                Exit Sub
            Else
                If clsAssetDepreciation.GetAssetDepCount(grow.Cells(colAssetID).Value, Nothing) > 0 Then
                    clsCommon.MyMessageBoxShow("Depreciation of Asset-" & grow.Cells(colAssetID).Value & " has been done. Can not change depereciation details for this Asset.")
                    Exit Sub
                End If
                If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                    If rbtnTaxCalAutomatic.IsChecked Then
                        Dim frm As New FrmPOItemTaxDetails()
                        frm.strLineNo = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                        frm.strItemCode = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                        frm.strItemName = clsCommon.myCstr(grow.Cells(colAssetName).Value)
                        frm.dblTotTax = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                        frm.dblAmtAfterDis = clsCommon.myCdbl(grow.Cells(colNetAmt).Value)

                        If clsCommon.myLen(frm.strItemCode) > 0 Then
                            frm.ArrIn = New List(Of clsTempItemTaxDetails)
                            For ii As Integer = 1 To 10
                                Dim strii As String = clsCommon.myCstr(ii)
                                Dim obj As New clsTempItemTaxDetails()
                                obj.AuthorityCode = clsCommon.myCstr(grow.Cells("COLTAX" + strii).Value)
                                If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                                    obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                                    obj.Rate = clsCommon.myCdbl(grow.Cells("COLTAXRATE" + strii).Value)
                                    obj.BaseAmt = clsCommon.myCdbl(grow.Cells("COLTAXBASEAMT" + strii).Value)
                                    obj.TaxAmt = clsCommon.myCdbl(grow.Cells("COLTAXAMT" + strii).Value)
                                    obj.isSurTax = clsCommon.myCBool(grow.Cells("ISSURTAX" + strii).Value)
                                    obj.SurTax = clsCommon.myCstr(grow.Cells("SURTAXCODE" + strii).Value)
                                    obj.IsTaxable = clsCommon.myCBool(grow.Cells("ISTAXABLE" + strii).Value)

                                    frm.ArrIn.Add(obj)
                                End If
                            Next

                            frm.ShowDialog()
                            If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                                BlankTaxDetails(grow.Index)

                                For ii As Integer = 0 To frm.ArrOut.Count - 1
                                    Dim strii As String = clsCommon.myCstr(ii + 1)
                                    grow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
                                    grow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
                                    grow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
                                    grow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
                                    grow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                                    grow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                                    grow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                                Next
                                grow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                                UpdateCurrentRow(grow.Index)
                                UpdateAllTotals()

                            End If
                        End If
                    End If
                Else
                    If clsCommon.myLen(grow.Cells(colAssetName).Value) <= 0 And clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                        RadMessageBox.Show("Please enter Asset Description")
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells(colAssetSpecificaion).Value) <= 0 And clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                        RadMessageBox.Show("Please enter Asset Specification")
                        Exit Sub
                    End If

                    Dim frm As New FrmAcquisitionEntryDetail()
                    Dim Template_Code As String
                    Dim objTemp As New ClsTemplateMaster
                    If clsCommon.myLen(grow.Cells(colTemplete).Value) > 0 Then
                        Template_Code = clsCommon.myCstr(grow.Cells(colTemplete).Value)
                    Else
                        Template_Code = fndTemplateCode.Value
                    End If
                    If clsCommon.myLen(Template_Code) > 0 Then
                        objTemp = ClsTemplateMaster.GetData(Template_Code, NavigatorType.Current, Nothing)
                    End If

                    DisableTemplateFields(frm)
                    frm.isPostedTransaction = IIf(UsLock1.Status = ERPTransactionStatus.Approved, True, False)
                    frm.obj = New clsAcquisitionDetail()
                    frm.obj.Acqusition_Date = txtDate.Value
                    frm.obj.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                    frm.obj.Asset_Name = clsCommon.myCstr(grow.Cells(colAssetName).Value)
                    frm.obj.Templete_Code = Template_Code 'clsCommon.myCstr(grow.Cells(colTemplete).Value)
                    frm.obj.Templete_Name = objTemp.template_Name

                    frm.obj.Put_To_Use = If(clsCommon.myCstr(grow.Cells(colPutToUse).Value) = "1", True, False)
                    If clsCommon.myLen(grow.Cells(colStartDate).Value) > 0 Then
                        frm.obj.Start_Date = grow.Cells(colStartDate).Value
                    Else
                        frm.obj.Start_Date = txtDate.Value
                    End If

                    If clsCommon.myLen(grow.Cells(colCategoryCode).Value) > 0 Then
                        frm.obj.Category_code = clsCommon.myCstr(grow.Cells(colCategoryCode).Value)
                    Else
                        frm.obj.Category_code = objTemp.category_code
                    End If
                    frm.obj.Asset_Specification = clsCommon.myCstr(grow.Cells(colAssetSpecificaion).Value)
                    If clsCommon.myLen(grow.Cells(colCategoryName).Value) > 0 Then
                        frm.obj.Category_Name = clsCommon.myCstr(grow.Cells(colCategoryName).Value)
                    Else
                        frm.obj.Category_Name = objTemp.category_Description
                    End If
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        frm.obj.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        frm.obj.Item_Name = clsCommon.myCstr(grow.Cells(colIName).Value)
                    End If
                    'If clsCommon.myLen(grow.Cells(colCostCenterCode).Value) > 0 Then
                    '    frm.obj.CostCenter_Code = clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)
                    'Else
                    '    frm.obj.CostCenter_Code = objTemp.CostCenter_Code
                    'End If
                    'If clsCommon.myLen(grow.Cells(colCostCenterName).Value) > 0 Then
                    '    frm.obj.CostCenter_Name = clsCommon.myCstr(grow.Cells(colCostCenterName).Value)
                    'Else
                    '    frm.obj.CostCenter_Name = objTemp.CostCenter_Description
                    'End If
                    If clsCommon.myLen(grow.Cells(colGroupCode).Value) > 0 Then
                        frm.obj.Group_Code = clsCommon.myCstr(grow.Cells(colGroupCode).Value)
                    Else
                        frm.obj.Group_Code = objTemp.group_code
                    End If

                    If clsCommon.myLen(grow.Cells(colGroupName).Value) > 0 Then
                        frm.obj.Group_Code_Name = clsCommon.myCstr(grow.Cells(colGroupName).Value)
                    Else
                        frm.obj.Group_Code_Name = objTemp.group_Description
                    End If
                    If clsCommon.myLen(grow.Cells(colAccountSetCode).Value) > 0 Then
                        frm.obj.AcSet_Code = clsCommon.myCstr(grow.Cells(colAccountSetCode).Value)
                    Else
                        frm.obj.AcSet_Code = objTemp.Acset_code
                    End If
                    If clsCommon.myLen(grow.Cells(colAccountSetName).Value) > 0 Then
                        frm.obj.AcSet_Code_Name = clsCommon.myCstr(grow.Cells(colAccountSetName).Value)
                    Else
                        frm.obj.AcSet_Code_Name = objTemp.Acset_Description
                    End If
                    If clsCommon.myLen(grow.Cells(colDepMethod).Value) > 0 Then
                        frm.obj.Dep_Method_Code = clsCommon.myCstr(grow.Cells(colDepMethod).Value)
                    Else
                        frm.obj.Dep_Method_Code = objTemp.Dep_Method_Code
                    End If
                    If clsCommon.myLen(grow.Cells(colDepMethodName).Value) > 0 Then
                        frm.obj.Dep_Method_Name = clsCommon.myCstr(grow.Cells(colDepMethodName).Value)
                    Else
                        frm.obj.Dep_Method_Name = objTemp.Dep_Method_Name
                    End If
                    If clsCommon.myLen(grow.Cells(colDepMethodName).Value) > 0 Then
                        frm.obj.Dep_Method_Tax_Code = clsCommon.myCstr(grow.Cells(colDepMethodTax).Value)
                    Else
                        frm.obj.Dep_Method_Tax_Code = objTemp.Dep_Method_Tax_Code
                    End If
                    If clsCommon.myLen(grow.Cells(colDepMethodName).Value) > 0 Then
                        frm.obj.Dep_Method_Tax_Name = clsCommon.myCstr(grow.Cells(colDepMethodNameTax).Value)
                    Else
                        frm.obj.Dep_Method_Tax_Name = objTemp.Dep_Method_Tax_Name
                    End If
                    If clsCommon.myLen(grow.Cells(colDepPeriodCode).Value) > 0 Then
                        frm.obj.Dep_Period_Code = clsCommon.myCstr(grow.Cells(colDepPeriodCode).Value)
                    Else
                        frm.obj.Dep_Period_Code = objTemp.Dep_Period_Code
                    End If
                    If clsCommon.myLen(grow.Cells(colDepPeriodName).Value) > 0 Then
                        frm.obj.Dep_Period_Name = clsCommon.myCstr(grow.Cells(colDepPeriodName).Value)
                    Else
                        frm.obj.Dep_Period_Name = objTemp.Dep_Period_Name
                    End If
                    If clsCommon.myLen(grow.Cells(colDepRate).Value) > 0 Then
                        frm.obj.Dep_Rate = clsCommon.myCstr(grow.Cells(colDepRate).Value)
                    Else
                        frm.obj.Dep_Rate = objTemp.Dep_Rate
                    End If
                    If clsCommon.myLen(grow.Cells(ColBookEstimatedLife).Value) > 0 Then
                        frm.obj.Book_Estimated_Life = clsCommon.myCstr(grow.Cells(ColBookEstimatedLife).Value)
                    Else
                        frm.obj.Book_Estimated_Life = objTemp.Book_Estimated_Life
                    End If
                    If clsCommon.myLen(grow.Cells(colDepTaxRate).Value) > 0 Then
                        frm.obj.Dep_Tax_Rate = clsCommon.myCstr(grow.Cells(colDepTaxRate).Value)
                    Else
                        frm.obj.Dep_Tax_Rate = objTemp.Dep_Tax_Rate
                    End If
                    If clsCommon.myLen(grow.Cells(ColBookSourceValue).Value) > 0 Then
                        frm.obj.Book_Source_value = clsCommon.myCstr(grow.Cells(ColBookSourceValue).Value)

                    Else
                        frm.obj.Book_Source_value = objTemp.Book_Source_value
                    End If
                    If clsCommon.myLen(grow.Cells(ColBookSourceOriginalValue).Value) > 0 Then
                        frm.obj.Book_Source_Original_value = clsCommon.myCstr(grow.Cells(ColBookSourceOriginalValue).Value)
                    Else
                        frm.obj.Book_Source_Original_value = objTemp.Book_Source_Original_value
                    End If
                    If clsCommon.myLen(grow.Cells(ColBookSalvageRate).Value) > 0 Then
                        frm.obj.Book_Salvage_Rate = clsCommon.myCstr(grow.Cells(ColBookSalvageRate).Value)
                    Else
                        frm.obj.Book_Salvage_Rate = objTemp.Book_Salvage_Rate
                    End If
                    If clsCommon.myLen(grow.Cells(ColBookSalvageValue).Value) > 0 Then
                        frm.obj.Book_Salvage_Value = clsCommon.myCstr(grow.Cells(ColBookSalvageValue).Value)
                    Else
                        frm.obj.Book_Salvage_Value = objTemp.Book_Salvage_Value
                    End If
                    If clsCommon.myLen(grow.Cells(colCapexSubCode).Value) > 0 Then
                        frm.obj.CapexSub_Code = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)
                    Else
                        frm.obj.CapexSub_Code = objTemp.CapexSub_Code
                    End If
                    If clsCommon.myLen(grow.Cells(colCapexCode).Value) > 0 Then
                        frm.obj.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                    Else
                        frm.obj.Capex_Code = objTemp.Capex_Code
                    End If
                    If clsCommon.myLen(grow.Cells(colCategoryType).Value) > 0 Then
                        frm.obj.CapexType = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                    Else
                        frm.obj.CapexType = If(clsCommon.myLen(grow.Cells(colCapexCode).Value) > 0, "Capex", "")
                    End If
                    If clsCommon.myCBool(grow.Cells(colIsCategory).Value) = True Then
                        frm.obj.IsCapex = 1
                    Else
                        frm.obj.IsCapex = If(clsCommon.myLen(grow.Cells(colCapexCode).Value) > 0, "1", "0")
                    End If
                    If clsCommon.myLen(grow.Cells(colTotTaxAmt).Value) > 0 Then
                        frm.obj.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(colNetAmt).Value) > 0 Then
                        ''richa agarwal 22 Apr,2019 ERO/22/04/19-000567
                        If clsTaxGroupMaster.IsHavingRecoverableTaxAuthority(clsCommon.myCstr(txtTaxGroup.Value), "P", Nothing) = False And clsCommon.myLen(fndPINo.Value) > 0 Then
                            frm.obj.Item_Net_Amt = frm.obj.Book_Source_value
                        Else
                            frm.obj.Item_Net_Amt = (frm.obj.Total_Tax_Amt + frm.obj.Book_Source_value)
                        End If
                        ''---------------
                    Else
                        frm.obj.Item_Net_Amt = (frm.obj.Total_Tax_Amt + frm.obj.Book_Source_value)
                    End If
                    If clsCommon.myLen(grow.Cells(colTaxDepType).Value) > 0 Then
                        frm.obj.Tax_Dep_Type = clsCommon.myCstr(grow.Cells(colTaxDepType).Value)
                    Else
                        If objTemp.Tax_Dep_Type = "F" Then
                            frm.obj.Tax_Dep_Type = "Formula"
                        ElseIf objTemp.Tax_Dep_Type = "M" Then
                            frm.obj.Tax_Dep_Type = "Manual"
                        Else
                            frm.obj.Tax_Dep_Type = ""
                        End If
                    End If
                    If clsCommon.myLen(grow.Cells(colBookDepType).Value) > 0 Then
                        frm.obj.Book_Dep_Type = clsCommon.myCstr(grow.Cells(colBookDepType).Value)
                    Else
                        If objTemp.Book_Dep_Type = "F" Then
                            frm.obj.Book_Dep_Type = "Formula"
                        ElseIf objTemp.Book_Dep_Type = "M" Then
                            frm.obj.Book_Dep_Type = "Manual"
                        Else
                            frm.obj.Book_Dep_Type = ""
                        End If
                    End If
                    If clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlAcqType.Text, "Asset") = CompairStringResult.Equal Then
                        frm.txtItem.Enabled = False
                    Else
                        frm.txtItem.Enabled = True
                    End If
                    frm.ShowDialog()

                    If frm.obj IsNot Nothing Then
                        isCellValueChangedOpen = True
                        If UsLock1.Status = ERPTransactionStatus.Posted OrElse UsLock1.Status = ERPTransactionStatus.Approved Then
                            grow.Cells(colAssetID).Value = frm.obj.Asset_Code
                        Else
                            grow.Cells(colAssetID).Value = frm.obj.Asset_Code
                            grow.Cells(colOldAssetID).Value = frm.obj.Asset_Code

                        End If
                        Dim arr As New ArrayList
                        arr.Add(frm.obj.Asset_Code)
                        Dim qry As String = clsItemIssueToAssembledAsset.GetAssembleQuery(arr, False)
                        qry = "select count(*) from (" & qry & ") Final "
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                            grow.Cells(colAssetID).ReadOnly = True
                        Else
                            If clsCommon.myLen(frm.obj.Asset_Code) <= 0 Then
                                grow.Cells(colAssetID).ReadOnly = False
                            End If
                        End If
                        grow.Cells(colAssetName).Value = frm.obj.Asset_Name
                        grow.Cells(colTemplete).Value = frm.obj.Templete_Code
                        grow.Cells(colTempleteName).Value = frm.obj.Templete_Name
                        grow.Cells(colCategoryCode).Value = frm.obj.Category_code
                        grow.Cells(colCategoryName).Value = frm.obj.Category_Name
                        'grow.Cells(colCostCenterCode).Value = frm.obj.CostCenter_Code
                        'grow.Cells(colCostCenterName).Value = frm.obj.CostCenter_Name
                        grow.Cells(colGroupCode).Value = frm.obj.Group_Code
                        grow.Cells(colGroupName).Value = frm.obj.Group_Code_Name
                        grow.Cells(colAccountSetCode).Value = frm.obj.AcSet_Code
                        grow.Cells(colAccountSetName).Value = frm.obj.AcSet_Code_Name
                        grow.Cells(colAcquisitionDate).Value = frm.obj.Acqusition_Date
                        grow.Cells(colDepMethod).Value = frm.obj.Dep_Method_Code
                        grow.Cells(colDepMethodName).Value = frm.obj.Dep_Method_Name
                        grow.Cells(colDepMethodTax).Value = frm.obj.Dep_Method_Tax_Code
                        grow.Cells(colDepMethodNameTax).Value = frm.obj.Dep_Method_Tax_Name
                        grow.Cells(colDepPeriodCode).Value = frm.obj.Dep_Period_Code
                        grow.Cells(colDepPeriodName).Value = frm.obj.Dep_Period_Name
                        grow.Cells(colPutToUse).Value = If(frm.obj.Put_To_Use = True, "1", "0")
                        grow.Cells(colStartDate).Value = frm.obj.Start_Date
                        If chkSameDesSpecStartDate.Checked AndAlso frm.obj.Put_To_Use Then ''ERO/10/09/19-001024 by balwinder on 10/09/2019
                            For kk As Integer = grow.Index To gv1.RowCount - 1
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(kk).Cells(colICode).Value)) = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(gv1.Rows(kk).Cells(colPutToUse).Value) = 0 Then
                                        gv1.Rows(kk).Cells(colPutToUse).Value = If(frm.obj.Put_To_Use = True, "1", "0")
                                        gv1.Rows(kk).Cells(colStartDate).Value = frm.obj.Start_Date
                                    End If
                                End If
                            Next
                        End If

                        grow.Cells(colDepRate).Value = frm.obj.Dep_Rate
                        grow.Cells(ColBookEstimatedLife).Value = frm.obj.Book_Estimated_Life
                        grow.Cells(ColBookSourceValue).Value = frm.obj.Book_Source_value
                        grow.Cells(ColBookSourceOriginalValue).Value = frm.obj.Book_Source_Original_value
                        grow.Cells(ColBookSalvageRate).Value = frm.obj.Book_Salvage_Rate
                        grow.Cells(ColBookSalvageValue).Value = frm.obj.Book_Salvage_Value
                        grow.Cells(colAssetSpecificaion).Value = frm.obj.Asset_Specification
                        grow.Cells(colICode).Value = frm.obj.Item_Code
                        grow.Cells(colIName).Value = frm.obj.Item_Name
                        grow.Cells(colDepTaxRate).Value = frm.obj.Dep_Tax_Rate
                        grow.Cells(colTotTaxAmt).Value = frm.obj.Total_Tax_Amt
                        grow.Cells(colNetAmt).Value = frm.obj.Item_Net_Amt
                        grow.Cells(colIsCategory).Value = frm.obj.IsCapex
                        grow.Cells(colCapexCode).Value = frm.obj.Capex_Code
                        grow.Cells(colCapexSubCode).Value = frm.obj.CapexSub_Code
                        If clsCommon.CompairString(frm.obj.Tax_Dep_Type, "Formula") = CompairStringResult.Equal Then
                            grow.Cells(colTaxDepType).Value = frm.obj.Tax_Dep_Type
                        ElseIf clsCommon.CompairString(frm.obj.Tax_Dep_Type, "Manual") = CompairStringResult.Equal Then
                            grow.Cells(colTaxDepType).Value = frm.obj.Tax_Dep_Type
                        Else
                            Throw New Exception("Select Tax Rate Type(Formula,Manual)")
                        End If
                        If clsCommon.CompairString(frm.obj.Book_Dep_Type, "Formula") = CompairStringResult.Equal Then
                            grow.Cells(colBookDepType).Value = frm.obj.Book_Dep_Type
                        ElseIf clsCommon.CompairString(frm.obj.Book_Dep_Type, "Manual") = CompairStringResult.Equal Then
                            grow.Cells(colBookDepType).Value = frm.obj.Book_Dep_Type
                        Else
                            Throw New Exception("Select Book Rate Type(Formula,Manual)")
                        End If
                        isCellValueChangedOpen = False
                    End If
                    SetitemWiseTaxOnlySetting()
                    UpdateCurrentRow(grow.Index)
                    'For ii As Integer = 0 To gv1.Rows.Count - 1
                    '    UpdateCurrentRow(ii)
                    'Next
                    'UpdateAllTotals()
                    RefreshSNo()
                End If
            End If
            If grow.Index = gv1.Rows.Count - 1 And clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                gv1.Rows.AddNew()
                If clsCommon.myLen(Me.fndTemplateCode.Value) > 0 Then
                    grow.Cells(colTemplete).Value = Me.fndTemplateCode.Value
                    grow.Cells(colTempleteName).Value = lblTemplateDesc.Text
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshSNo()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Sub DisableTemplateFields(ByVal frm As FrmAcquisitionEntryDetail)
        If clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal Or clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
            'frm.txtAssetSpecification.Enabled = True
            frm.txtItem.Enabled = True
            frm.txtTemplate.Enabled = True
            frm.txtCategory.Enabled = False
            frm.txtGroup.Enabled = True
            frm.txtCostCenter.Enabled = True
            frm.txtAccountSet.Enabled = False
            frm.txtAcquisitionDate.Enabled = True

            frm.txtDepMethod.Enabled = True
            frm.txtDepMethodTax.Enabled = True
            frm.txtDepPeriod.Enabled = True
            frm.txtStartDate.Enabled = True
            frm.txtDepRate.Enabled = True
            frm.txtDepTaxRate.Enabled = True
            frm.txtEstLife.Enabled = True
            frm.txtSourceOrgValue.Enabled = True
            frm.txtSourceValue.Enabled = True
            frm.txtSalvageRate.Enabled = True
            frm.txtSalvageValue.Enabled = True
            frm.txtNetValue.Enabled = True
            frm.txtTaxAmount.Enabled = False
        Else
            'frm.txtAssetSpecification.Enabled = False
            frm.txtItem.Enabled = False
            frm.txtTemplate.Enabled = True
            frm.txtCategory.Enabled = False
            frm.txtGroup.Enabled = True
            frm.txtCostCenter.Enabled = False
            frm.txtAccountSet.Enabled = False
            frm.txtAcquisitionDate.Enabled = False
            frm.txtTaxAmount.Enabled = False
            frm.txtDepMethod.Enabled = False
            frm.txtDepMethodTax.Enabled = False
            frm.txtDepPeriod.Enabled = False
            frm.txtStartDate.Enabled = True
            frm.txtDepRate.Enabled = False
            frm.txtDepTaxRate.Enabled = False
            frm.txtEstLife.Enabled = True
            frm.txtSourceOrgValue.Enabled = False
            frm.txtSourceValue.Enabled = False
            frm.txtSalvageRate.Enabled = True
            frm.txtSalvageValue.Enabled = False
            frm.txtNetValue.Enabled = False
        End If

    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        RefreshSNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                SetTaxDetails()

            ElseIf rbtnTaxCalManual.IsChecked Then

                For intRowNo As Integer = 0 To gv2.Rows.Count - 1
                    gv2.Rows(intRowNo).Cells(colTTaxRate).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTTaxAmt).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTBaseAmt).Value = Nothing
                Next
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    For ii As Integer = 1 To 10
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                    Next
                Next
            End If
        End If
    End Sub

    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                    gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTaxOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function GetTaxGrp(ByVal strItmType As String) As String
        Dim qry As String = "select Tax_Group  from TSPL_VENDOR_MASTER where Vendor_Code ='" + strItmType + "'"
        strItmType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return strItmType
    End Function

    Private Function SetItemWiseTax(ByVal dtAfterModify As DataTable, ByVal strShipFrm As String) As DataTable
        dtAfterModify.Columns.Add("TAX1_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX2_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX3_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX4_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt3", GetType(Double))

        dtAfterModify.Columns.Add("TAX5_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt3", GetType(Double))




        Dim qry As String = "select Tax,Rate,SUM(Amt) as TaxAmt"
        qry += " from ("
        qry += " select TAX1 as Tax,TAX1_Rate as Rate,TAX1_Amt as Amt"
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'   "
        qry += " )xxx "
        qry += " group by Tax,Rate   having SUM(Amt)>0   "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                For ii As Integer = 1 To 5
                    Dim strCol As String = "TAX" + clsCommon.myCstr(ii) + ""
                    If clsCommon.CompairString(clsCommon.myCstr(dtAfterModify.Rows(0)(strCol)), clsCommon.myCstr(dr("Tax"))) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt1")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate1") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt2")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate2") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt3")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate3") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        Else
                            Throw New Exception("Printing Support only 3 Different Rates")
                        End If

                    End If
                Next
            Next
        End If
        Return dtAfterModify
    End Function

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        'Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER"
        txtLocation.Value = clsLocation.getFinder("Location_Type='Physical'", txtLocation.Value, isButtonClicked) ' clsCommon.ShowSelectForm("POVendorFndr", qry, "Location_Code", "Location_Type='Physical'", txtLocation.Value, "Location_Code", isButtonClicked)
        'qry = "select Location_Desc from TSPL_LOCATION_MASTER where Location_Code ='" + txtLocation.Value + "'"
        lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing) 'clsDBFuncationality.getSingleValue(qry)
    End Sub

    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        'Ticket No-ERO/02/08/19-000980
        Try
            Dim frm As New frmCrystalReportViewer()
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select a Acquisition First.", Me.Text)
                Return
            End If
            Dim dtCompAddress As DataTable = Nothing
            Dim Qry As String = ""

            Qry = " select TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end " & _
                    " +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  " & _
                    " + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  " & _
                    " + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  " & _
                    " + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " & _
                    " + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else  " & _
                    "',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  " & _
                    " '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ' " & _
                    ",Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Comp_Address  " & _
                    " , case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ' CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  " & _
                    " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  as CIN_PAN" & _
                    " from tspl_company_master "
            dtCompAddress = clsDBFuncationality.GetDataTable(Qry)
            Qry = " SELECT TSPL_ACQUISITION_HEAD.Vendor_Invoice_No,(select cast(TSPL_COMPANY_MASTER.logo_img as image) from TSPL_COMPANY_MASTER) as [logo_img],'" + clsCommon.myCstr(dtCompAddress.Rows(0)("Comp_Address")) + "' as [Company Address],'" + clsCommon.myCstr(dtCompAddress.Rows(0)("CIN_PAN")) + "' as CIN_PAN,'" + objCommonVar.CurrentCompanyName + "' as [Company], TSPL_ACQUISITION_HEAD.Acquisition_Code, TSPL_ACQUISITION_HEAD.Acquisition_Date,"
            Qry += " TSPL_ACQUISITION_HEAD.PO_No,  TSPL_ACQUISITION_HEAD.Description, TSPL_ACQUISITION_HEAD.Remarks,TSPL_LOCATION_MASTER.Location_Desc ,"
            Qry += " TSPL_VENDOR_MASTER.Vendor_Name,  "
            Qry += " TSPL_ACQUISITION_HEAD.tax1,TSPL_ACQUISITION_HEAD.tax1_amt,TSPL_ACQUISITION_HEAD.tax2,TSPL_ACQUISITION_HEAD.tax2_amt,TSPL_ACQUISITION_HEAD.tax3,TSPL_ACQUISITION_HEAD.tax3_amt,TSPL_ACQUISITION_HEAD.tax4,TSPL_ACQUISITION_HEAD.tax4_amt,TSPL_ACQUISITION_HEAD.tax5,TSPL_ACQUISITION_HEAD.tax5_amt,TSPL_ACQUISITION_HEAD.tax6,TSPL_ACQUISITION_HEAD.tax6_amt,TSPL_ACQUISITION_HEAD.tax7,TSPL_ACQUISITION_HEAD.tax7_amt,TSPL_ACQUISITION_HEAD.tax8,TSPL_ACQUISITION_HEAD.tax8_amt,TSPL_ACQUISITION_HEAD.tax9,TSPL_ACQUISITION_HEAD.tax9_amt,TSPL_ACQUISITION_HEAD.tax10,TSPL_ACQUISITION_HEAD.tax10_amt,"
            Qry += " TSPL_ACQUISITION_HEAD.total_amt, TSPL_ACQUISITION_HEAD.total_tax_amt, TSPL_ACQUISITION_HEAD.Net_Amt,"
            Qry += " TSPL_ACQUISITION_DETAIL.SNo, TSPL_ACQUISITION_DETAIL.Asset_Code, TSPL_ACQUISITION_DETAIL.Asset_Name, TSPL_ACQUISITION_DETAIL.Asset_Specification, TSPL_ACQUISITION_DETAIL.Dep_Rate, TSPL_ACQUISITION_DETAIL.Book_Source_value"
            'Qry += " ,TSPL_ACQUISITION_HEAD.Capex_Code as CapexName,TSPL_ACQUISITION_HEAD.CapexSub_Code as SubCapexName"
            Qry += " ,TSPL_ACQUISITION_DETAIL.Capex_Code as CapexName,TSPL_ACQUISITION_DETAIL.Capex_SubCode as SubCapexName"
            Qry += " ,TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as SubCapexNameDesc,TSPL_CAPEX_MASTER.DESCRIPTION as CapexDesc"
            Qry += " ,case when TSPL_ACQUISITION_HEAD.Status=1 then TSPL_ACQUISITION_HEAD.ASSEMBLE_CODE else '' end as ASSEMBLE_CODE,"
            Qry += " case when TSPL_ACQUISITION_HEAD.Status=1 then convert(varchar,TSPL_ACQUISITION_HEAD.Post_Date,103) else '' end as Post_Date"
            Qry += " FROM TSPL_ACQUISITION_HEAD "
            Qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ACQUISITION_HEAD.Vendor_Code "
            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_ACQUISITION_HEAD.Loc_Code"
            Qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Acquisition_Code =TSPL_ACQUISITION_HEAD.Acquisition_Code"
            'Show Capex,SubCapex Description  Ticket No-UDL/07/05/18-000153
            'Qry += "    left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE=TSPL_ACQUISITION_HEAD.Capex_Code"
            'Qry += " left outer join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE=TSPL_ACQUISITION_HEAD.CapexSub_Code"
            Qry += "    left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE=TSPL_ACQUISITION_DETAIL.Capex_Code"
            Qry += " left outer join TSPL_CAPEX_BUDGET_MASTER on TSPL_CAPEX_BUDGET_MASTER.CODE=TSPL_ACQUISITION_DETAIL.Capex_SubCode"
            ''''''''''''
            Qry += " where TSPL_ACQUISITION_HEAD.Acquisition_Code = '" + txtDocNo.Value + " ' order by TSPL_ACQUISITION_DETAIL.SNo"

            Dim dt_final As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt_final.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                frm.funreport(CrystalReportFolder.FixedAssets, dt_final, "frmAcquisionEntryReport", "Acquision Entry Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnold_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnold.ToggleStateChanged
        txtTaxGroup.Enabled = True
        gv2.Enabled = True
        GroupBox1.Enabled = True

        'If rbtnold.IsChecked Then
        '    txtTaxGroup.Value = ""
        '    gv2.DataSource = Nothing
        '    gv2.Rows.Clear()

        '    txtTaxGroup.Enabled = False
        '    gv2.Enabled = False
        '    GroupBox1.Enabled = False
        'End If
    End Sub

    Private Sub btnChangeDepDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeDepDetail.Click
        Try
            '==shivani[BM00000007923]

            If clsCommon.MyMessageBoxShow(Me, "Do you want to change Depreciation details" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim arr As New List(Of clsAcquisitionDetail)()
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colAssetID).Value) Then
                        Dim objTr As New clsAcquisitionDetail()
                        If clsAssetDepreciation.GetAssetDepCount(grow.Cells(colAssetID).Value, Nothing) > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Depreciation of Asset-" & grow.Cells(colAssetID).Value & " has been done. Can not change depereciation details for this Asset.")
                            Continue For
                        End If

                        ''====================check if asset is merged or not======================
                        Dim qry As String = "select count(*) from TSPL_ACQUISITION_ASSET_MERGE_DETAIL where old_asset_code='" + clsCommon.myCstr(grow.Cells(colAssetID).Value) + "'"
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Merging of Asset-" & grow.Cells(colAssetID).Value & " has been done. Can not change depereciation details for this Asset.")
                            Continue For
                        End If
                        '==============================================================================


                        objTr.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                        objTr.Dep_Method_Code = clsCommon.myCstr(grow.Cells(colDepMethod).Value)
                        objTr.Dep_Method_Tax_Code = clsCommon.myCstr(grow.Cells(colDepMethodTax).Value)
                        objTr.Dep_Period_Code = clsCommon.myCstr(grow.Cells(colDepPeriodCode).Value)
                        objTr.Dep_Rate = clsCommon.myCdbl(grow.Cells(colDepRate).Value)
                        objTr.Dep_Tax_Rate = clsCommon.myCdbl(grow.Cells(colDepTaxRate).Value)
                        objTr.Asset_Specification = clsCommon.myCstr(grow.Cells(colAssetSpecificaion).Value)
                        objTr.Asset_Name = clsCommon.myCstr(grow.Cells(colAssetName).Value)
                        objTr.Book_Salvage_Rate = clsCommon.myCstr(grow.Cells(ColBookSalvageRate).Value)
                        objTr.Book_Salvage_Value = clsCommon.myCstr(grow.Cells(ColBookSalvageValue).Value)
                        objTr.Book_Estimated_Life = clsCommon.myCstr(grow.Cells(ColBookEstimatedLife).Value)
                        objTr.Put_To_Use = If(clsCommon.myCstr(grow.Cells(colPutToUse).Value) = "1", True, False)
                        objTr.Start_Date = clsCommon.myCDate(grow.Cells(colStartDate).Value)
                        arr.Add(objTr)
                    End If
                Next
                clsAcquisitionDetail.UpdateDecpreciationData(txtDocNo.Value, arr)
                clsCommon.MyMessageBoxShow(Me, "Successfully changed the depreciation details", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTemplateCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTemplateCode._MYValidating
        If UsLock1.Status = ERPTransactionStatus.Pending AndAlso clsCommon.myLen(txtDocNo.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow(Me, "On template change (Changed Column - Category Code,Group Code,AccountSet Code,Depriciation Method,Depriciation Tax Method,Depriciation Period,Start Date,Depriciation Rate,Depriciation Tax Rate,Estimated Life,Salvage Rate,Salvage Value) " + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim qry As String = " select template_code as Code,template_Name as Name,  category_code,group_code,Acset_code from TSPL_FA_TEMPLATE_MASTER "
        fndTemplateCode.Value = clsCommon.ShowSelectForm("ACQDETTemplate", qry, "Code", "", fndTemplateCode.Value, "", isButtonClicked)
        If clsCommon.myLen(clsCommon.myCstr(fndTemplateCode.Value)) > 0 Then
            Me.lblTemplateDesc.Text = ClsTemplateMaster.GetData(fndTemplateCode.Value, NavigatorType.Current).template_Name
        End If

        'If clsCommon.myLen(fndPINo.Value) <= 0 AndAlso clsCommon.CompairString(ddlAcqType.Text, "Asset") <> CompairStringResult.Equal Then
        '    If Not fndTemplateCode.Value Is Nothing Then
        '        If clsCommon.myLen(clsCommon.myCstr(fndTemplateCode.Value)) > 0 Then
        '            'Me.lblTemplateDesc.Text = ClsTemplateMaster.GetData(fndTemplateCode.Value, NavigatorType.Current).template_Name
        '            'sanjay

        '            'ddlAcqType.Text = "Assembled"
        '            gv1.Rows.Clear()
        '            gv1.Rows.AddNew()
        '            Dim obj As ClsTemplateMaster = ClsTemplateMaster.GetData(fndTemplateCode.Value, NavigatorType.Current)
        '            Me.lblTemplateDesc.Text = clsCommon.myCstr(obj.template_Name)


        '            Dim grow As GridViewRowInfo = gv1.CurrentRow

        '            ''If UsLock1.Status = ERPTransactionStatus.Posted OrElse UsLock1.Status = ERPTransactionStatus.Approved Then
        '            ''    grow.Cells(colAssetID).Value = obj.Acset_code
        '            ''Else
        '            ''    grow.Cells(colAssetID).Value = obj.Acset_code
        '            ''    grow.Cells(colOldAssetID).Value = obj.Acset_code
        '            ''End If

        '            'Dim arr As New ArrayList
        '            'arr.Add(frm.obj.Asset_Code)
        '            'Dim qry As String = clsItemIssueToAssembledAsset.GetAssembleQuery(arr)
        '            'qry = "select count(*) from (" & qry & ") Final "
        '            'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
        '            '    grow.Cells(colAssetID).ReadOnly = True
        '            'Else
        '            '    If clsCommon.myLen(obj.Acset_code) <= 0 Then
        '            '        grow.Cells(colAssetID).ReadOnly = False
        '            '    End If
        '            'End If
        '            'grow.Cells(colAssetID).Value = obj.Acset_code
        '            'grow.Cells(colAssetName).Value = obj.Acset_Description
        '            grow.Cells(colTemplete).Value = obj.template_code
        '            grow.Cells(colTempleteName).Value = obj.template_Name
        '            grow.Cells(colCategoryCode).Value = obj.category_code
        '            grow.Cells(colCategoryName).Value = obj.category_Description
        '            'grow.Cells(colCostCenterCode).Value = obj.CostCenter_Code
        '            'grow.Cells(colCostCenterName).Value = obj.CostCenter_Description
        '            grow.Cells(colGroupCode).Value = obj.group_code
        '            grow.Cells(colGroupName).Value = obj.group_Description
        '            grow.Cells(colAccountSetCode).Value = obj.Acset_code
        '            grow.Cells(colAccountSetName).Value = obj.Acset_Description
        '            grow.Cells(colAcquisitionDate).Value = obj.Created_Date
        '            grow.Cells(colDepMethod).Value = obj.Dep_Method_Code
        '            grow.Cells(colDepMethodName).Value = obj.Dep_Method_Name
        '            grow.Cells(colDepMethodTax).Value = obj.Dep_Method_Tax_Code
        '            grow.Cells(colDepMethodNameTax).Value = obj.Dep_Method_Tax_Name
        '            grow.Cells(colDepPeriodCode).Value = obj.Dep_Period_Code
        '            grow.Cells(colDepPeriodName).Value = obj.Dep_Period_Name

        '            ''grow.Cells(colPutToUse).Value = If(obj.Put_To_Use = True, "1", "0")
        '            grow.Cells(colStartDate).Value = obj.Start_Date
        '            grow.Cells(colDepRate).Value = obj.Dep_Rate
        '            grow.Cells(ColBookEstimatedLife).Value = obj.Book_Estimated_Life
        '            grow.Cells(ColBookSourceValue).Value = obj.Book_Source_value

        '            grow.Cells(ColBookSourceOriginalValue).Value = obj.Book_Source_Original_value
        '            grow.Cells(ColBookSalvageRate).Value = obj.Book_Salvage_Rate
        '            grow.Cells(ColBookSalvageValue).Value = obj.Book_Salvage_Value
        '            ''grow.Cells(colAssetSpecificaion).Value = obj.Asset_Specification
        '            ''grow.Cells(colICode).Value = obj.Item_Code
        '            ''grow.Cells(colIName).Value = obj.Item_Name
        '            ''grow.Cells(colDepTaxRate).Value = obj.Dep_Tax_Rate
        '            ''grow.Cells(colTotTaxAmt).Value = obj.Total_Tax_Amt
        '            ''grow.Cells(colNetAmt).Value = obj.Item_Net_Amt

        '            grow.Cells(colIsCategory).Value = IIf(obj.CapexSub_Code = "", False, True)  'obj.IsCapex
        '            grow.Cells(colCategoryType).Value = IIf(obj.CapexSub_Code = "", "None", "Capex")

        '            grow.Cells(colCapexCode).Value = obj.Capex_Code

        '            grow.Cells(colCapexSubCode).Value = obj.CapexSub_Code

        '            '====================================================================================
        '            'If clsCommon.CompairString(obj.Tax_Dep_Type, "Formula") = CompairStringResult.Equal Then
        '            '    grow.Cells(colTaxDepType).Value = obj.Tax_Dep_Type
        '            'ElseIf clsCommon.CompairString(obj.Tax_Dep_Type, "Manual") = CompairStringResult.Equal Then
        '            '    grow.Cells(colTaxDepType).Value = obj.Tax_Dep_Type
        '            'Else
        '            '    Throw New Exception("Select Tax Rate Type(Formula,Manual)")
        '            'End If

        '            'If clsCommon.CompairString(obj.Book_Dep_Type, "Formula") = CompairStringResult.Equal Then
        '            '    grow.Cells(colBookDepType).Value = obj.Book_Dep_Type
        '            'ElseIf clsCommon.CompairString(obj.Book_Dep_Type, "Manual") = CompairStringResult.Equal Then
        '            '    grow.Cells(colBookDepType).Value = obj.Book_Dep_Type
        '            'Else
        '            '    Throw New Exception("Select Book Rate Type(Formula,Manual)")
        '            'End If

        '            'isCellValueChangedOpen = False


        '            'SetitemWiseTaxOnlySetting()
        '            'UpdateCurrentRow(grow.Index)

        '            'For ii As Integer = 0 To gv1.Rows.Count - 1
        '            '    UpdateCurrentRow(ii)
        '            'Next

        '            'UpdateAllTotals()
        '            RefreshSNo()

        '            'sanjay

        '        End If
        '    End If
        'End If


        If clsCommon.myLen(clsCommon.myCstr(fndTemplateCode.Value)) > 0 Then
            Dim obj As ClsTemplateMaster = ClsTemplateMaster.GetData(fndTemplateCode.Value, NavigatorType.Current)
            If gv1.Rows.Count > 0 Then

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.CompairString(obj.template_code, grow.Cells(colTemplete).Value) = CompairStringResult.Equal Then
                        grow.Cells(colTemplete).Value = obj.template_code
                        grow.Cells(colTempleteName).Value = obj.template_Name
                        grow.Cells(colCategoryCode).Value = obj.category_code
                        grow.Cells(colCategoryName).Value = obj.category_Description

                        grow.Cells(colGroupCode).Value = obj.group_code
                        grow.Cells(colGroupName).Value = obj.group_Description
                        grow.Cells(colAccountSetCode).Value = obj.Acset_code
                        grow.Cells(colAccountSetName).Value = obj.Acset_Description
                        'grow.Cells(colAcquisitionDate).Value = obj.Created_Date

                        If clsCommon.CompairString(obj.Tax_Dep_Type, "Formula") = CompairStringResult.Equal Then
                            grow.Cells(colTaxDepType).Value = obj.Tax_Dep_Type
                        ElseIf clsCommon.CompairString(obj.Tax_Dep_Type, "Manual") = CompairStringResult.Equal Then
                            grow.Cells(colTaxDepType).Value = obj.Tax_Dep_Type
                        End If

                        If clsCommon.CompairString(obj.Book_Dep_Type, "Formula") = CompairStringResult.Equal Then
                            grow.Cells(colBookDepType).Value = obj.Book_Dep_Type
                        ElseIf clsCommon.CompairString(obj.Book_Dep_Type, "Manual") = CompairStringResult.Equal Then
                            grow.Cells(colBookDepType).Value = obj.Book_Dep_Type
                        End If

                        grow.Cells(colDepMethod).Value = obj.Dep_Method_Code
                        grow.Cells(colDepMethodName).Value = obj.Dep_Method_Name
                        grow.Cells(colDepMethodTax).Value = obj.Dep_Method_Tax_Code
                        grow.Cells(colDepMethodNameTax).Value = obj.Dep_Method_Tax_Name
                        grow.Cells(colDepPeriodCode).Value = obj.Dep_Period_Code
                        grow.Cells(colDepPeriodName).Value = obj.Dep_Period_Name

                        grow.Cells(colStartDate).Value = obj.Start_Date
                        grow.Cells(colDepRate).Value = obj.Dep_Rate
                        grow.Cells(colDepTaxRate).Value = obj.Dep_Tax_Rate
                        grow.Cells(ColBookEstimatedLife).Value = obj.Book_Estimated_Life
                        'grow.Cells(ColBookSourceValue).Value = obj.Book_Source_value

                        'grow.Cells(ColBookSourceOriginalValue).Value = obj.Book_Source_Original_value
                        grow.Cells(ColBookSalvageRate).Value = obj.Book_Salvage_Rate
                        'grow.Cells(ColBookSalvageValue).Value = obj.Book_Salvage_Value
                        If clsCommon.myCdbl(grow.Cells(ColBookSalvageRate).Value) > 0 Then
                            grow.Cells(ColBookSalvageValue).Value = grow.Cells(ColBookSourceValue).Value * clsCommon.myCdbl(grow.Cells(ColBookSalvageRate).Value) / 100
                        End If
                        'grow.Cells(colIsCategory).Value = IIf(obj.CapexSub_Code = "", False, True)
                        'grow.Cells(colCategoryType).Value = IIf(obj.CapexSub_Code = "", "None", "Capex")

                        'grow.Cells(colCapexCode).Value = obj.Capex_Code

                        'grow.Cells(colCapexSubCode).Value = obj.CapexSub_Code

                    End If

                Next

            End If

        End If

    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If clsCommon.myLen(fndPINo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select PI No first", Me.Text)
            Exit Sub

            'clsCommon.myLen(txtSRNNo.Value) <= 0 Then
            '            clsCommon.MyMessageBoxShow("Please Select SRN No first")
            '            Exit Sub
        ElseIf clsCommon.myLen(fndTemplateCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Template Code first", Me.Text)
            Exit Sub
        End If
        CreateAssetsFromSRN()
    End Sub

    Sub CreateAssetsFromSRN()
        Try
            isInsideLoadData = True
            If (objSRN_ACQUTSN.SRN_No) IsNot Nothing AndAlso clsCommon.myLen(objSRN_ACQUTSN.Vendor_Code) > 0 Then
                LoadBlankGrid()
                Dim objMRNHead As clsSRNHead = clsSRNHead.GetData(txtSRNNo.Value, NavigatorType.Current)
                Me.gv1.Rows.Clear()
                LoadBlankGridAC()
                txtTaxGroup.Value = objSRN_ACQUTSN.Tax_Group
                lblTaxGrpName.Text = objSRN_ACQUTSN.TaxGroupName
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt1
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code2) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt2
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code3) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt3
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code4) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt4
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code5) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt5
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code6) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt6
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code7) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt7
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code8) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt8
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code9) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt9
                End If
                If (clsCommon.myLen(objMRNHead.Add_Charge_Code10) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt10
                End If

                For Each objtr As clsAcquisitionDetail In objSRN_ACQUTSN.Arr
                    Dim SRNQty As Decimal = objtr.No_Of_Rows_Qty

                    Dim objItem As clsItemMaster = clsItemMaster.GetDataRMOther(objtr.Item_Code, NavigatorType.Current)
                    If objItem Is Nothing Then
                        Continue For
                    End If
                    Dim createSepasset As Boolean = IIf(objItem.CreateSepAssetForEachQty = "1", True, False)
                    If createSepasset = False Then
                        SRNQty = 1
                    End If

                    'If createSepasset = True Then
                    '    SRNQty = 1
                    'End If

                    'End If
                    If gvAC.Rows.Count < 1 Then
                        gvAC.Rows.AddNew()
                    End If
                    For intloop As Integer = 1 To SRNQty
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colcheck).Value = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTemplete).Value = Me.fndTemplateCode.Value
                        Dim tmp As ClsTemplateMaster = ClsTemplateMaster.GetData(Me.fndTemplateCode.Value, NavigatorType.Current)

                        If tmp IsNot Nothing Then
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetID).Value = frm.obj.Asset_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = frm.obj.Asset_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTemplete).Value = tmp.template_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTempleteName).Value = tmp.template_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryCode).Value = tmp.category_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryName).Value = tmp.category_Description
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = tmp.CostCenter_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterName).Value = tmp.CostCenter_Description
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colGroupCode).Value = tmp.group_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colGroupName).Value = tmp.group_Description
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetCode).Value = tmp.Acset_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAccountSetName).Value = tmp.Acset_Description
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAcquisitionDate).Value = txtDate.Value
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethod).Value = tmp.Dep_Method_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodName).Value = tmp.Dep_Method_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodTax).Value = tmp.Dep_Method_Tax_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodNameTax).Value = tmp.Dep_Method_Tax_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepPeriodCode).Value = tmp.Dep_Period_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepPeriodName).Value = tmp.Dep_Period_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colStartDate).Value = tmp.Start_Date
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRate).Value = tmp.Dep_Rate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookEstimatedLife).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Asset_Life from TSPL_ITEM_MASTER where item_code='" + objtr.Item_Code + "'"))
                            If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookEstimatedLife).Value) <= 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookEstimatedLife).Value = tmp.Book_Estimated_Life
                            End If
                            If createSepasset = True Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSourceValue).Value = objtr.No_Of_Rows_Qty_for_discount / SRNQty   ''tmp.Book_Source_value

                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSourceOriginalValue).Value = objtr.No_Of_Rows_Qty_for_discount / SRNQty

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objtr.Total_Tax_Amt / SRNQty
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSourceValue).Value = objtr.No_Of_Rows_Qty_for_discount  ''tmp.Book_Source_value

                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSourceOriginalValue).Value = objtr.No_Of_Rows_Qty_for_discount
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objtr.Total_Tax_Amt  ' objtr.Total_Tax_Amt / SRNQty
                            End If

                            'tmp.Book_Source_Original_value
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSalvageRate).Value = tmp.Book_Salvage_Rate
                            '==========added by shivani=======================================
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = objtr.SRN_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPI).Value = objtr.PI_No
                            If createSepasset = True Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNQty).Value = "1"
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNQty).Value = objtr.No_Of_Rows_Qty
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNRate).Value = objtr.SRN_Rate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objtr.Unit_Code
                            '==================================================================
                            '' changed by Panch Raj on 21/03/16 against ticket No:BM00000008974
                            If tmp.Book_Salvage_Rate > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSalvageValue).Value = clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSourceValue).Value) * tmp.Book_Salvage_Rate / 100
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColBookSalvageValue).Value = tmp.Book_Salvage_Value
                            End If
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepTaxRate).Value = tmp.Dep_Tax_Rate
                            If clsCommon.CompairString(tmp.Book_Dep_Type, "F") = CompairStringResult.Equal Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBookDepType).Value = "Formula"
                            ElseIf clsCommon.CompairString(tmp.Book_Dep_Type, "M") = CompairStringResult.Equal Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBookDepType).Value = "Manual"
                            Else
                                Throw New Exception("Update Book Rate Type(Formula,Manual) for the Template " & tmp.template_code & "")
                            End If

                            If clsCommon.CompairString(tmp.Tax_Dep_Type, "F") = CompairStringResult.Equal Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxDepType).Value = "Formula"
                            ElseIf clsCommon.CompairString(tmp.Tax_Dep_Type, "M") = CompairStringResult.Equal Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxDepType).Value = "Manual"
                            Else
                                Throw New Exception("Update Tax Rate Type(Formula,Manual) for the Template " & tmp.template_code & "")
                            End If
                        End If
                        SetTaxDetails()
                        SetitemWiseTaxOnlySetting()
                        ' UpdateCurrentRow(ii)
                        UpdateCurrentRow(gv1.Rows.Count - 1)

                        ''If rbtnTaxCalManual.IsChecked Then
                        ''    For ii As Integer = 0 To gv1.Rows.Count - 1
                        ''        UpdateCurrentRow(ii)
                        ''    Next
                        ''End If
                        ''UpdateAllTotals()
                        ''RefreshSNo()

                        '===============preeti=============
                        'If gvAC.Rows.Count < 1 Then
                        '======================================

                    Next


                Next
                If gv1.Rows.Count > 0 Then
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                    UpdateAllTotals()
                    RefreshSNo()
                End If
                If clsCommon.myLen(txtSRNNo.Value) > 0 Then
                    TaxFillForSRN()
                End If

            End If

            isInsideLoadData = False
        Catch ex As Exception
            gv1.Rows.Clear()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs)
        '' export header
        ExportHeader()
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs)
        '' export detail
        ExportDetail()
    End Sub

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs)
        '' import header
        ImportHeader()
    End Sub

    Private Sub RadMenuItem7_Click(sender As Object, e As EventArgs)
        '' import detail
        ImportDetail()
    End Sub

    Private Sub ExportHeader()
        Try
            Dim Qry As String
            Qry = " select Acquisition_Code as [Acqisition Code],Acquisition_Date as [Acquisition Date],Vendor_Code as [Vendor Code],Description, " & _
                  " Remarks,Loc_Code as [Location Code],SRN_No as [SRN No],Templete_Code as [Template Code],Status_New_Old as [New/Old] from TSPL_ACQUISITION_HEAD"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = " select '' as [Acqisition Code],'' as [Acquisition Date],'' as [Vendor Code],'' as Description, " & _
                 " '' as Remarks,'' as [Location Code],'' as [SRN No],'' as [Template Code],'' as [New/Old] from TSPL_ACQUISITION_HEAD"
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Private Sub ExportDetail()
        Try
            Dim Qry As String
            Qry = "select Acquisition_Code as [Acqisition Code],SNo,Asset_Code as [Asset Code],Asset_Name as [Asset Name],Asset_Specification as [Asset Specification],Item_Code as [Item Code],Templete_Code as [Template Code]," & _
                  " Category_code as [Category Code],Group_Code as [Group Code],AcSet_Code as [Account Set Code],CostCenter_Code as [Cost Center Code], " & _
                  " Acqusition_Date as [Acquisition Date],Dep_Method_Code as [Depreciation Method Code],Dep_Period_Code as [Depreciation Period Code], " & _
                  " Start_Date as [Start Date],Dep_Rate as [Depreciation Rate],Book_Estimated_Life as [Book Estimated Life],Book_Source_value as [Book Source Value], " & _
                  " Book_Source_Original_value as [Book Source Original Value], " & _
                  " Dep_Tax_Rate as [Dep Tax Rate],Book_Salvage_Value as [Book Solvage Value] from TSPL_ACQUISITION_DETAIL "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = "select '' as [Acqisition Code],'' as SNo,'' as [Asset Code],'' as [Asset Name],'' as [Asset Specification],'' as [Item Code],'' as [Template Code]," & _
                  " '' as [Category Code],'' as [Group Code],'' as [Account Set Code],'' as [Cost Center Code], " & _
                  " '' as [Acquisition Date],'' as [Depreciation Method Code],'' as [Depreciation Period Code], " & _
                  " '' as [Start Date],'' as [Depreciation Rate],'' as [Book Estimated Life],'' as [Book Source Value], " & _
                  " '' as [Book Source Original Value], " & _
                  " '' as [Dep Tax Rate],'' as [Book Solvage Value] "
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Private Sub ImportHeader()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim total As Integer = 0
        If transportSql.importExcel(gv, "Acqisition Code", "Acquisition Date", "Vendor Code", "Description", "Remarks", "Location Code", "SRN No", "Template Code", "New/Old") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsCategories)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsAcquisitionHead

                    '-------Acquisition Code---------

                    obj.Acquisition_Code = clsCommon.myCstr(grow.Cells("Acqisition Code").Value)
                    If clsCommon.myLen(obj.Acquisition_Code) > 0 Then
                        isNewEntry = Not clsAcquisitionHead.CheckCode(obj.Acquisition_Code)
                    Else
                        isNewEntry = True
                    End If
                    obj.Acquisition_Date = clsCommon.myCstr(grow.Cells("Acquisition Date").Value)
                    obj.Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                    'obj.Acquisition_Date = clsCommon.myCstr(grow.Cells("Acqisition Date").Value)
                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    obj.Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    obj.Loc_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    obj.SRN_No = clsCommon.myCstr(grow.Cells("SRN No").Value)
                    obj.Templete_Code = clsCommon.myCstr(grow.Cells("Template Code").Value)
                    obj.statusnewold = clsCommon.myCstr(grow.Cells("New/Old").Value)

                    If clsCommon.myLen(obj.Acquisition_Date) <= 0 Then
                        Throw New Exception("Please enter Acquisition Date on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                        obj.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                        If clsCommon.myLen(obj.Vendor_Name) <= 0 Then
                            Throw New Exception("Vendor Code does not exist on Line No '" + LineNo + "'")
                            Exit Sub
                        End If
                    End If

                    If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        Throw New Exception("Enter Location Code on Line No '" + LineNo + "'")
                        Exit Sub
                    ElseIf clsCommon.myLen(clsLocation.GetName(obj.Loc_Code, Nothing)) <= 0 Then
                        Throw New Exception("Location Code does not exist on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.SRN_No) > 30 Then
                        Throw New Exception("The Maximum Length of SRN No on Line No '" + LineNo + "' Is Greater Than 30")
                        Exit Sub
                    ElseIf clsCommon.myLen(obj.Templete_Code) > 30 Then
                        Throw New Exception("The Maximum Length of Template Code on Line No '" + LineNo + "' Is Greater Than 30")
                        Exit Sub
                    ElseIf clsCommon.myCdbl(obj.statusnewold) > 1 Then
                        Throw New Exception("Satus must be 0 or 1 on Line No '" + LineNo + "'")
                        Exit Sub
                        'ElseIf clsCommon.myCdbl(obj.statusnewold) > 1 Then
                        '    Throw New Exception("Satus must be 0 or 1 on Line No '" + LineNo + "'")
                        '    Exit Sub
                    End If
                    If (obj.SaveData(obj, isNewEntry, False, Nothing)) Then
                        total = total + 1
                    End If

                    'Arr.Add(obj)
                Next

                common.clsCommon.MyMessageBoxShow("" & total & " Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                clsCommon.ProgressBarHide()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub ImportDetail()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim total As Integer = 0

        If transportSql.importExcel(gv, "Acqisition Code", "SNo", "Asset Code", "Asset Name", "Asset Specification", "Item Code", "Template Code", "Category Code", "Group Code", "Account Set Code", "Cost Center Code", "Acquisition Date", "Depreciation Method Code", "Depreciation Period Code", "Start Date", "Depreciation Rate", "Book Estimated Life", "Book Source Value", "Book Source Original Value", "Dep Tax Rate", "Book Solvage Value") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsAcquisitionDetail)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsAcquisitionDetail

                    '-------Acquisition Code---------

                    obj.Acquisition_Code = clsCommon.myCstr(grow.Cells("Acqisition Code").Value)
                    If clsCommon.myLen(obj.Acquisition_Code) > 0 Then
                        isNewEntry = Not clsAcquisitionHead.CheckCode(obj.Acquisition_Code)
                    Else
                        isNewEntry = True
                    End If
                    obj.Acqusition_Date = clsCommon.myCstr(grow.Cells("Acquisition Date").Value)
                    obj.SNo = clsCommon.myCdbl(grow.Cells("SNo").Value)
                    obj.Asset_Code = clsCommon.myCstr(grow.Cells("Asset Code").Value)
                    obj.Asset_Name = clsCommon.myCstr(grow.Cells("Asset Name").Value)
                    obj.Asset_Specification = clsCommon.myCstr(grow.Cells("Asset Specification").Value)
                    obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    obj.Templete_Code = clsCommon.myCstr(grow.Cells("Template Code").Value)
                    obj.Category_code = clsCommon.myCstr(grow.Cells("Category Code").Value)
                    obj.Group_Code = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    obj.AcSet_Code = clsCommon.myCstr(grow.Cells("Account Set Code").Value)
                    obj.CostCenter_Code = clsCommon.myCstr(grow.Cells("Cost Center Code").Value)
                    obj.Dep_Method_Code = clsCommon.myCstr(grow.Cells("Depreciation Method Code").Value)
                    obj.Dep_Period_Code = clsCommon.myCstr(grow.Cells("Depreciation Period Code").Value)
                    obj.Start_Date = clsCommon.myCDate(grow.Cells("Start Date").Value)
                    obj.Dep_Rate = clsCommon.myCdbl(grow.Cells("Depreciation Rate").Value)
                    obj.Book_Estimated_Life = clsCommon.myCdbl(grow.Cells("Book Estimated Life").Value)
                    obj.Book_Source_value = clsCommon.myCdbl(grow.Cells("Book Source Value").Value)
                    obj.Book_Source_Original_value = clsCommon.myCdbl(grow.Cells("Book Source Original Value").Value)
                    obj.Dep_Tax_Rate = clsCommon.myCdbl(grow.Cells("Dep Tax Rate").Value)
                    obj.Book_Salvage_Value = clsCommon.myCdbl(grow.Cells("Book Solvage Value").Value)


                    If clsCommon.myLen(obj.Acqusition_Date) <= 0 Then
                        Throw New Exception("Please enter Acquisition Date on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    'If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                    '    Throw New Exception("Please enter Asset Code on Line No '" + LineNo + "'")
                    '    Exit Sub
                    'End If
                    If clsCommon.myLen(obj.Item_Code) <= 0 Then
                        Throw New Exception("Please enter Item Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Templete_Code) <= 0 Then
                        Throw New Exception("Please enter Template Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Category_code) <= 0 Then
                        Throw New Exception("Please enter Category Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Group_Code) <= 0 Then
                        Throw New Exception("Please enter Group Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.AcSet_Code) <= 0 Then
                        Throw New Exception("Please enter Account Set Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.CostCenter_Code) <= 0 Then
                        Throw New Exception("Please enter Cost Center Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.Dep_Method_Code) <= 0 Then
                        Throw New Exception("Please enter Depreciation Method Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    If clsCommon.myLen(obj.Dep_Period_Code) <= 0 Then
                        Throw New Exception("Please enter Depreciation Period Code on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If obj.Start_Date < obj.Acqusition_Date Then
                        Throw New Exception("Please start date must be less than Acquisition Date on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Acquisition_Code) > 0 Then
                        Arr.Add(obj)
                    End If

                Next
                clsAcquisitionDetail.SaveData("", Arr, Nothing)

                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                clsCommon.ProgressBarHide()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Sub AcqType()
        gv1.Rows.Clear()
        If clsCommon.CompairString(ddlAcqType.Text, "Asset") = CompairStringResult.Equal Then
            fndPINo.Enabled = True
            txtSRNNo.Enabled = False
            btnGo.Enabled = True
            ChkISAssemble.Checked = False
            ChkISAssemble.Enabled = False
            txtTaxGroup.Enabled = False
            chkOpening.Checked = False
            chkOpening.Enabled = False
            chkOpening.Visible = False
            chkOpeningDirect.Checked = False
            chkOpeningDirect.Enabled = False
            chkOpeningDirect.Visible = False
            txtAssembleOpeningAmt.Text = 0
            txtAssembleOpeningAmt.Enabled = False
            txtAssembleOpeningAmt.Visible = False
            'gvAC.ReadOnly = True
        ElseIf clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal Then
            fndPINo.Enabled = False
            fndPINo.Value = Nothing
            txtSRNNo.Value = Nothing
            txtSRNNo.Enabled = False
            btnGo.Enabled = False
            ChkISAssemble.Checked = False
            ChkISAssemble.Enabled = False
            txtTaxGroup.Enabled = True
            chkOpening.Checked = False
            chkOpening.Enabled = False
            chkOpening.Visible = False
            chkOpeningDirect.Checked = False
            chkOpeningDirect.Enabled = True
            chkOpeningDirect.Visible = True
            txtAssembleOpeningAmt.Text = 0
            txtAssembleOpeningAmt.Enabled = False
            txtAssembleOpeningAmt.Visible = False

            gv1.Rows.AddNew()
            gvAC.Rows.AddNew()
            'gvAC.ReadOnly = False
        ElseIf clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
            txtSRNNo.Value = Nothing
            fndPINo.Value = Nothing
            fndPINo.Enabled = False
            txtSRNNo.Enabled = False
            btnGo.Enabled = False
            ChkISAssemble.Checked = True
            ChkISAssemble.Enabled = False
            txtVendorNo.Value = ""
            txtVendorNo.Enabled = False
            lblVendorName.Text = ""
            txtTaxGroup.Enabled = False
            chkOpening.Checked = False
            chkOpening.Enabled = True
            chkOpening.Visible = True
            chkOpeningDirect.Checked = False
            chkOpeningDirect.Enabled = False
            chkOpeningDirect.Visible = False
            txtAssembleOpeningAmt.Text = 0
            If ImportMultipleAssetAssembled = True Then
                txtAssembleOpeningAmt.Enabled = False
            Else
                txtAssembleOpeningAmt.Enabled = True
            End If

            txtAssembleOpeningAmt.Visible = True

            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
            gv1.Rows.AddNew()
            gvAC.Rows.AddNew()
            'gvAC.ReadOnly = False
        Else
            txtSRNNo.Value = Nothing
            txtSRNNo.Enabled = False
            fndPINo.Enabled = False
            fndPINo.Value = Nothing
            btnGo.Enabled = False
            ChkISAssemble.Checked = False
            ChkISAssemble.Enabled = False
            txtVendorNo.Enabled = True
            txtTaxGroup.Enabled = True
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
            gv1.Rows.AddNew()
            gvAC.Rows.AddNew()
        End If

    End Sub

    Private Sub ddlYear_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlAcqType.SelectedIndexChanged
        AcqType()
        ' LoadBlankGrid()
    End Sub

    Sub Export_Asset()
        Try
            Dim Template_Code As String = "[Template Code]"
            Dim CategoryCode As String = "[Category Code]"
            Dim GroupCode As String = "[Group Code]"
            If ReadOnlyTemplateFieldsOnAcqusition Then
                Template_Code = "[Asset Category]"
                CategoryCode = "[Asset Group]"
                GroupCode = "[Asset Sub Group]"
            End If
            Dim qry As String = " select TSPL_ACQUISITION_HEAD.Acquisition_Code as [Acqisition Code],TSPL_ACQUISITION_HEAD.Acquisition_Type as [Acquisition Type],TSPL_ACQUISITION_HEAD.Loc_Code as Location,Asset_Code as [Asset Code],TSPL_ACQUISITION_DETAIL.Asset_Serial_No as [Asset Serial No],Item_Code as [Item Code]," & _
                                " TSPL_ACQUISITION_DETAIL.Templete_Code as " & Template_Code & ",Category_code as " & CategoryCode & ",Group_Code as " & GroupCode & ", convert(varchar,Acqusition_Date,103) as [Acquisition Date], " & _
                                " convert(varchar,Start_Date,103) as [Put To Use Date],Book_Estimated_Life as [Estimated Life],TSPL_ACQUISITION_DETAIL.Asset_Expired_Life as [Asset Expired Life],Book_Source_Original_value as [Original Value],Book_Source_value as [Current Value],TSPL_ACQUISITION_DETAIL.Depreciated_Value as [Depreciated Value], " & _
                                " Book_Salvage_Rate as [Solvage %],Book_Salvage_Value as [Solvage Value],Vendor_Code  as [Vendor Code] from TSPL_ACQUISITION_DETAIL inner join TSPL_ACQUISITION_HEAD on " & _
                                " TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                qry = " select '' as [Acqisition Code],'Direct' as [Acquisition Type],'' as Location,'' as [Asset Code],'' as [Asset Serial No],'' as [Item Code]," & _
                                " '' as " & Template_Code & ",'' as " & CategoryCode & ",'' as " & GroupCode & ", '' as [Acquisition Date], " & _
                                " '' as [Put To Use Date],'' as [Estimated Life],'' as [Asset Expired Life],'' as [Oiginal Value],'' as [Current Value],'' as [Depreciated Value], " & _
                                " '' as [Solvage %],'' as [Solvage Value],'' as [Vendor Code] "
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Asset Acquisition")
        End Try
    End Sub

    Private Sub Import_Asset()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim total As Integer = 0
        Dim lstACQ As New List(Of String)
        Dim lstTemplt As New List(Of String)
        Dim Template_Code As String = "Template Code"
        Dim CategoryCode As String = "Category Code"
        Dim GroupCode As String = "Group Code"
        If ReadOnlyTemplateFieldsOnAcqusition Then
            Template_Code = "Asset Category"
            CategoryCode = "Asset Group"
            GroupCode = "Asset Sub Group"
        End If
        'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
        Dim dtERPStartDate As DateTime?
        If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
            dtERPStartDate = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
        End If

        '' Ticket No: BM00000007684 for FA Opening/Import
        If transportSql.importExcel(gv, "Acqisition Code", "Acquisition Type", "Location", "Asset Code", "Asset Serial No", "Item Code", Template_Code, CategoryCode, GroupCode, "Acquisition Date", "Put To Use Date", "Estimated Life", "Asset Expired Life", "Original Value", "Current Value", "Depreciated Value", "Solvage %", "Solvage Value", "Vendor Code") Then
            Try
                clsCommon.ProgressBarShow()
                Dim objAHListNew As New List(Of clsAcquisitionHead)
                Dim objAHListOld As New List(Of clsAcquisitionHead)

                Dim objAH As New clsAcquisitionHead
                Dim Arr As New List(Of clsAcquisitionDetail)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsAcquisitionDetail

                    '-------Acquisition Code---------
                    objAH = New clsAcquisitionHead
                    obj.Acquisition_Code = clsCommon.myCstr(grow.Cells("Acqisition Code").Value)
                    If clsCommon.myLen(obj.Acquisition_Code) > 0 Then
                        isNewEntry = Not clsAcquisitionHead.CheckCode(obj.Acquisition_Code)
                    Else
                        isNewEntry = True
                    End If
                    '' check Acquisition Type : done b Panch Raj on 08-may-2018 against ticket No-UDL/07/05/18-000152
                    objAH.Acquisition_Type = clsCommon.myCstr(grow.Cells("Acquisition Type").Value)
                    If clsCommon.CompairString(objAH.Acquisition_Type, "Direct") = CompairStringResult.Equal Then
                        objAH.Opening_Assemble = False
                        objAH.Opening_Assemble_Amt = 0
                        obj.Is_Assembled = 0
                        If objCommonVar.ERPStartDate IsNot Nothing AndAlso clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                            If clsCommon.myCDate(clsCommon.GetPrintDate(grow.Cells("Acquisition Date").Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
                                objAH.Opening_Direct = True
                            End If
                        End If
                    ElseIf clsCommon.CompairString(objAH.Acquisition_Type, "Assembled") = CompairStringResult.Equal Then
                        objAH.Opening_Assemble = True
                        objAH.Opening_Assemble_Amt = clsCommon.myCdbl(grow.Cells("Current Value").Value)
                        obj.Is_Assembled = 1
                    ElseIf clsCommon.CompairString(objAH.Acquisition_Type, "Asset") = CompairStringResult.Equal Then
                        Throw New Exception("Acquisition Type - Asset can not be Imported at line no- " & (grow.Index + 1) & ". Acquisition Type must be Direct or Assembled for Import")
                    Else
                        Throw New Exception("Acquisition Type at line no- " & (grow.Index + 1) & " is invalid or blank.Acquisition Type must be Direct or Assembled for Import")
                    End If
                    obj.Acqusition_Date = clsCommon.myCstr(grow.Cells("Acquisition Date").Value)
                    obj.Asset_Code = clsCommon.myCstr(grow.Cells("Asset Code").Value)
                    ''check duplicate asset code
                    If isNewEntry Then
                        Dim strDoc As String = clsAcquisitionDetail.CheckDuplicateAsset(obj.Asset_Code, obj.Acquisition_Code, Nothing)
                        If clsCommon.myLen(strDoc) > 0 Then
                            Throw New Exception("Asset Code -" & obj.Asset_Code & " is already used in Acquisition No-" & strDoc & "")
                        End If
                    End If
                    obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    obj.Templete_Code = clsCommon.myCstr(grow.Cells(Template_Code).Value)
                    If clsCommon.myLen(obj.Templete_Code) <= 0 Then
                        Throw New Exception("Please enter " & Template_Code & " on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '' get template data
                    Dim objTmplt As ClsTemplateMaster = ClsTemplateMaster.GetData(obj.Templete_Code, NavigatorType.Current, Nothing)
                    If objTmplt Is Nothing Then
                        Throw New Exception("" & Template_Code & "- " & obj.Templete_Code & " not found.")
                    End If
                    obj.Category_code = clsCommon.myCstr(grow.Cells(CategoryCode).Value)
                    obj.Group_Code = clsCommon.myCstr(grow.Cells(GroupCode).Value)
                    obj.AcSet_Code = objTmplt.Acset_code 'clsCommon.myCstr(grow.Cells("Account Set Code").Value)
                    obj.CostCenter_Code = objTmplt.CostCenter_Code 'clsCommon.myCstr(grow.Cells("Cost Center Code").Value)
                    obj.Dep_Method_Code = objTmplt.Dep_Method_Code 'clsCommon.myCstr(grow.Cells("Depreciation Method Code").Value)
                    obj.Dep_Method_Tax_Code = objTmplt.Dep_Method_Tax_Code 'clsCommon.myCstr(grow.Cells("Depreciation Method Code").Value)
                    obj.Dep_Period_Code = objTmplt.Dep_Period_Code 'clsCommon.myCstr(grow.Cells("Depreciation Period Code").Value)
                    If clsCommon.myLen(grow.Cells("Put To Use Date").Value) > 0 Then
                        obj.Start_Date = clsCommon.myCDate(grow.Cells("Put To Use Date").Value)
                        If clsCommon.myLen(obj.Start_Date) > 0 Then
                            obj.Put_To_Use = True
                        Else
                            obj.Put_To_Use = False
                        End If
                    Else
                        obj.Put_To_Use = False
                        If clsCommon.CompairString(objAH.Acquisition_Type, "Assembled") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Put To Use date is mandatory for Direct Acquisition Type at line no- " & (grow.Index + 1) & "")
                        End If
                    End If

                    obj.Dep_Rate = objTmplt.Dep_Rate 'clsCommon.myCdbl(grow.Cells("Depreciation Rate").Value)
                    obj.Book_Estimated_Life = clsCommon.myCdbl(grow.Cells("Estimated Life").Value)
                    obj.Book_Source_value = clsCommon.myCdbl(grow.Cells("Current Value").Value)
                    obj.Book_Source_Original_value = clsCommon.myCdbl(grow.Cells("Original Value").Value)
                    obj.Dep_Tax_Rate = objTmplt.Dep_Tax_Rate 'clsCommon.myCdbl(grow.Cells("Dep Tax Rate").Value)
                    obj.Book_Salvage_Rate = clsCommon.myCdbl(grow.Cells("Solvage %").Value)
                    obj.Book_Salvage_Value = clsCommon.myCdbl(grow.Cells("Solvage Value").Value)
                    obj.Item_Net_Amt = clsCommon.myCdbl(grow.Cells("Current Value").Value)
                    obj.Asset_Specification = obj.Asset_Code
                    obj.Asset_Name = obj.Asset_Code
                    obj.Asset_Serial_No = clsCommon.myCstr(grow.Cells("Asset Serial No").Value)
                    obj.Depreciated_Value = clsCommon.myCdbl(grow.Cells("Depreciated Value").Value)
                    obj.Asset_Expired_Life = clsCommon.myCdbl(grow.Cells("Asset Expired Life").Value)
                    If clsCommon.myLen(obj.Templete_Code) > 0 Then
                        obj.Book_Dep_Type = objTmplt.Book_Dep_Type
                        obj.Tax_Dep_Type = objTmplt.Tax_Dep_Type
                    End If
                    'Original Value", "Current Value", "Solvage %", "Solvage Value"
                    If clsCommon.myLen(obj.Acqusition_Date) <= 0 Then
                        Throw New Exception("Please enter Acquisition Date on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    'If clsCommon.myLen(obj.Asset_Code) <= 0 Then
                    '    Throw New Exception("Please enter Asset Code on Line No '" + LineNo + "'")
                    '    Exit Sub
                    'End If
                    If clsCommon.myLen(obj.Item_Code) <= 0 Then
                        'Throw New Exception("Please enter Item Code on Line No '" + LineNo + "'")
                        'Exit Sub
                    End If
                    If clsCommon.myLen(obj.Templete_Code) <= 0 Then
                        Throw New Exception("Please enter " & Template_Code & " on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Category_code) <= 0 Then
                        Throw New Exception("Please enter " & CategoryCode & " on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myLen(obj.Group_Code) <= 0 Then
                        Throw New Exception("Please enter " & GroupCode & " on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    'If clsCommon.myLen(obj.AcSet_Code) <= 0 Then
                    '    Throw New Exception("Please enter Account Set Code on Line No '" + LineNo + "'")
                    '    Exit Sub
                    'End If

                    'If clsCommon.myLen(obj.CostCenter_Code) <= 0 Then
                    '    Throw New Exception("Please enter Cost Center Code on Line No '" + LineNo + "'")
                    '    Exit Sub
                    'End If

                    'If clsCommon.myLen(obj.Dep_Method_Code) <= 0 Then
                    '    Throw New Exception("Please enter Depreciation Method Code on Line No '" + LineNo + "'")
                    '    Exit Sub
                    'End If

                    'If clsCommon.myLen(obj.Dep_Period_Code) <= 0 Then
                    '    Throw New Exception("Please enter Depreciation Period Code on Line No '" + LineNo + "'")
                    '    Exit Sub
                    'End If
                    If clsCommon.CompairString(objAH.Acquisition_Type, "Assembled") = CompairStringResult.Equal Then
                        If obj.Put_To_Use = True Then
                            If obj.Start_Date < obj.Acqusition_Date Then
                                Throw New Exception("Put To date can not less than Acquisition Date at Line No '" + LineNo + "'")
                                Exit Sub
                            End If
                        End If
                    Else
                        If obj.Start_Date < obj.Acqusition_Date Then
                            Throw New Exception("Put To date can not less than Acquisition Date at Line No '" + LineNo + "'")
                            Exit Sub
                        End If
                    End If




                    'If clsCommon.myLen(obj.Acquisition_Code) > 0 Or clsCommon.myLen(obj.Templete_Code) > 0 Then
                    '    Arr.Add(obj)
                    'End If
                    '' assign header object
                    If isNewEntry Then
                        If lstTemplt.Contains(obj.Templete_Code) = False Then
                            objAH.Templete_Code = obj.Templete_Code
                            objAH.Acquisition_Code = obj.Acquisition_Code
                            objAH.Acquisition_Date = obj.Acqusition_Date
                            'objAH.Acquisition_Type = "Direct"
                            objAH.IS_Assemble = False
                            objAH.Is_Visi_Type = False
                            objAH.Loc_Code = clsCommon.myCstr(grow.Cells("Location").Value)
                            objAH.Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                            objAH.statusnewold = "Old"
                            objAH.Arr = New List(Of clsAcquisitionDetail)
                            If objAH.Arr Is Nothing OrElse objAH.Arr.Count = 0 Then
                                obj.SNo = 1
                            Else
                                obj.SNo = (objAH.Arr.Count + 1)
                            End If
                            obj.SNo = (objAH.Arr.Count + 1)
                            objAH.Arr.Add(obj)
                            objAHListNew.Add(objAH)
                            lstTemplt.Add(obj.Templete_Code)
                        Else
                            For Each objAH1 As clsAcquisitionHead In objAHListNew
                                If clsCommon.CompairString(objAH1.Templete_Code, obj.Templete_Code) = CompairStringResult.Equal Then
                                    objAH1.Arr.Add(obj)
                                End If
                            Next

                        End If
                    Else
                        If lstACQ.Contains(obj.Acquisition_Code) = False Then
                            objAH.Templete_Code = obj.Templete_Code
                            objAH.Acquisition_Code = obj.Acquisition_Code
                            objAH.Acquisition_Date = obj.Acqusition_Date
                            'objAH.Acquisition_Type = "Direct"
                            objAH.IS_Assemble = False
                            objAH.Is_Visi_Type = False
                            objAH.Loc_Code = clsCommon.myCstr(grow.Cells("Location").Value)
                            objAH.Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                            objAH.statusnewold = "Old"
                            objAH.Arr = New List(Of clsAcquisitionDetail)
                            If objAH.Arr Is Nothing OrElse objAH.Arr.Count = 0 Then
                                obj.SNo = 1
                            Else
                                obj.SNo = (objAH.Arr.Count + 1)
                            End If

                            objAH.Arr.Add(obj)
                            objAHListOld.Add(objAH)
                            lstACQ.Add(obj.Acquisition_Code)
                        Else
                            For Each objAH1 As clsAcquisitionHead In objAHListOld
                                If clsCommon.CompairString(objAH1.Acquisition_Code, obj.Acquisition_Code) = CompairStringResult.Equal Then
                                    objAH1.Arr.Add(obj)
                                End If
                            Next
                        End If

                    End If
                Next
                For Each objTr As clsAcquisitionHead In objAHListNew
                    Dim Doc_Amt As Decimal = 0
                    For Each objTr1 As clsAcquisitionDetail In objTr.Arr
                        Doc_Amt = Doc_Amt + objTr1.Item_Net_Amt
                    Next
                    objTr.Total_Amt = Doc_Amt
                    objTr.Net_Amt = Doc_Amt
                    If clsCommon.CompairString(objAH.Acquisition_Type, "Assembled") = CompairStringResult.Equal AndAlso ImportMultipleAssetAssembled Then
                        objTr.Opening_Assemble_Amt = Doc_Amt
                    End If



                    objTr.SaveData(objTr, True, False)
                Next

                For Each objTr As clsAcquisitionHead In objAHListOld
                    Dim Doc_Amt As Decimal = 0
                    For Each objTr1 As clsAcquisitionDetail In objTr.Arr
                        Doc_Amt = Doc_Amt + objTr1.Item_Net_Amt
                    Next
                    objTr.Total_Amt = Doc_Amt
                    objTr.Net_Amt = Doc_Amt
                    If clsCommon.CompairString(objAH.Acquisition_Type, "Assembled") = CompairStringResult.Equal AndAlso ImportMultipleAssetAssembled Then
                        objTr.Opening_Assemble_Amt = Doc_Amt
                    End If
                    objTr.SaveData(objTr, False, False)
                Next

                'clsAcquisitionDetail.SaveData("", Arr, Nothing)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Export_Asset()
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Import_Asset()
    End Sub
    ''shivani ==========> against ticket[BM00000008015]
    Sub SetVendorTDSDetails()
        btnTDSDetail.Enabled = False
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(txtVendorNo.Value)
        If objVendor IsNot Nothing Then
            btnTDSDetail.Enabled = True
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, clsCommon.myCdbl(lblNetAmt.Text), Nothing, False, txtVendorNo.Value)
            If (objDedDetails IsNot Nothing) Then
                objRemittance = New clsRemittance()
                objRemittance.Branch_Code = objVendor.Branch_Code
                objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                objRemittance.IsTDSOverride = False
                If isNewEntry Then
                    objRemittance.IsApplyTDS = True
                Else
                    objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(txtDocNo.Value)
                End If
                objRemittance.Section_Code = objVendor.TDSSection
                objRemittance.Section_Description = objVendor.TDSSectionDescription
                objRemittance.Select_By = objVendor.VendorTypeCode
                'objRemittance.Include_Tax = objVendor.Include_Tax

                Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + txtDate.Value + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + txtDate.Value + "',103)<=convert(date,To_Date,103) "
                objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                objRemittance.Quarter = "First"

            End If
        End If
    End Sub

    Sub UpdateTDSAmount()
        If (objRemittance Is Nothing) Then
            SetVendorTDSDetails()
        Else
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, clsCommon.myCdbl(lblNetAmt.Text), Nothing, False, txtVendorNo.Value)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If
        End If
        If (objRemittance IsNot Nothing) Then
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + txtVendorNo.Value + "'")

            objRemittance.Vendor_Code = txtVendorNo.Value
            objRemittance.Vendor_Name = lblVendorName.Text
            objRemittance.Document_Date = txtDate.Value
            objRemittance.Document_Type = clsCommon.myCstr(ddlAcqType.SelectedValue)
            objRemittance.Document_Amount = clsCommon.myCdbl(lblNetAmt.Text)

            If IncludeTax = "N" Then
                objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(lblNetAmt.Text)
            Else
                objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(lblNetAmt.Text)
            End If

            If Not objRemittance.IsTDSOverride Then

                If IncludeTax = "N" Then
                    objRemittance.Actual_TDS_Base = clsCommon.myCdbl(lblNetAmt.Text)
                Else
                    objRemittance.Actual_TDS_Base = clsCommon.myCdbl(lblNetAmt.Text)
                End If
            End If

            objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
            objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

            objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
            objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

            objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
            objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

            objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
            objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

            objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
            objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
        End If
    End Sub

    Sub ViewTDS()
        Try
            Dim frm As New FrmViewTDS()
            UpdateTDSAmount()
            frm.ObjIn = objRemittance
            frm.ShowDialog()
            'If (frm.ObjReturn IsNot Nothing) Then
            objRemittance = frm.ObjReturn
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnTDSDetail_Click(sender As Object, e As EventArgs) Handles btnTDSDetail.Click
        ViewTDS()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        SaveLayout1()
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        DeleteLayout()
    End Sub

    Private Sub fndPINo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPINo._MYValidating
        isInsideLoadData = True
        Dim frm As New frmPendingSRNForAsset()
        frm.ShowDialog()

        objSRN_ACQUTSN = New clsAcquisitionHead()
        If frm.obj IsNot Nothing AndAlso clsCommon.myLen(frm.obj.Vendor_Code) > 0 Then
            objSRN_ACQUTSN = frm.obj
            txtVendorNo.Value = frm.obj.Vendor_Code
            lblVendorName.Text = frm.obj.Vendor_Name
            txtLocation.Value = frm.obj.Loc_Code
            lblLocation.Text = clsDBFuncationality.getSingleValue(clsCommon.myCstr("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
            fndPINo.Value = frm.obj.PI_No
            txtSRNNo.Value = frm.obj.SRN_No
            If clsCommon.myLen(fndPINo.Value) > 0 Then
                txtLocation.Enabled = False
                lblPIDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select convert (varchar, PI_Date,103) as PI_Date from TSPL_PI_HEAD where PI_No = '" + clsCommon.myCstr(fndPINo.Value) + "'"))

            Else
                lblPIDate.Text = ""
            End If
        End If
        If clsCommon.myLen(txtSRNNo.Value) > 0 Then
            gv2.ReadOnly = True
            lblSRNDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select convert (varchar, SRN_Date,103) as SRN_Date from TSPL_SRN_HEAD where srn_no = '" + clsCommon.myCstr(txtSRNNo.Value) + "'"))

        Else
            gv2.ReadOnly = False
            lblSRNDate.Text = ""
        End If
        ''=================Added by preeti gupta Against ticket no[GKD/15/02/19-000175]============
        'If clsCommon.myLen(fndPINo.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select PI No first")
        '    Exit Sub

        '    'clsCommon.myLen(txtSRNNo.Value) <= 0 Then
        '    '            clsCommon.MyMessageBoxShow("Please Select SRN No first")
        '    '            Exit Sub
        'ElseIf clsCommon.myLen(fndTemplateCode.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select Template Code first")
        '    Exit Sub
        'End If
        'CreateAssetsFromSRN()
        ''==============================================================================
    End Sub

    Private Sub fndcapexsubcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            lbl_capexcode.Text = ""
            fndcapexcode.Value = ""
            Me.fndcapexsubcode.Value = clsCapexBudget.getFinder("", fndcapexsubcode.Value, isButtonClicked)
            If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                fndcapexcode.Value = clsCapexBudget.GetCapexCode(Me.fndcapexsubcode.Value, Nothing)
                lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, txtDocNo.Value, Nothing)
                lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, txtDocNo.Value, Nothing)
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv1.MasterView.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
    '======================Added  by preeti gupta=====================================
    Private Function ChkLimitBudget(ByVal rowindex As Integer) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Double))

        Dim dr As DataRow = dt.NewRow()


        Dim budgetamtwithtolerence As Double
        Dim rebudgetamt As Double
        Dim rebudgetamtwithtolerence As Double
        dr = dt.NewRow()

        budgetamtwithtolerence = clsCapexBudget.GetBudgetWithTolerence(clsCommon.myCstr(gv1.Rows(rowindex).Cells(colCapexSubCode).Value), Nothing)
        rebudgetamt = clsCapexBudget.GetReBudget(clsCommon.myCstr(gv1.Rows(rowindex).Cells(colCapexSubCode).Value), txtDocNo.Value, Nothing, "AC-Capex")
        rebudgetamtwithtolerence = clsCapexBudget.GetReBudgetWithTolerence(clsCommon.myCstr(gv1.Rows(rowindex).Cells(colCapexSubCode).Value), txtDocNo.Value, Nothing, "AC-Capex")
        dr("Code") = budgetamtwithtolerence
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = rebudgetamt
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = rebudgetamtwithtolerence
        dt.Rows.Add(dr)

        Return dt
    End Function
    '============================Added by preeti gupta===================
    Sub LoadBlankGridAC()
        gvAC.Rows.Clear()
        gvAC.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Addition Charges Code"
        repoACCode.Name = colACCode
        'repoACCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoACCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoACCode.Width = 150
        repoACCode.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colACName
        repoACName.Width = 300
        repoACName.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACName)

        Dim repoACAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "Amount"
        repoACAmt.Name = colACAmount
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACAmt)

        gvAC.AllowAddNewRow = False
        gvAC.ShowGroupPanel = False
        gvAC.AllowColumnReorder = True
        gvAC.AllowRowReorder = False
        gvAC.EnableSorting = False
        gvAC.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvAC.MasterTemplate.ShowRowHeaderColumn = False
        gvAC.TableElement.TableHeaderHeight = 40
        'gvAC.Rows.AddNew()
    End Sub
    '====================================================================
    Private Sub setGridFocusAC()
        Try
            Dim intCurrRow As Integer = gvAC.CurrentRow.Index
            If intCurrRow = gvAC.Rows.Count - 1 AndAlso gvAC.Rows.Count <= 10 Then
                gvAC.Rows.AddNew()
                gvAC.CurrentRow = gvAC.Rows(intCurrRow)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvAC_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvAC.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvAC.Columns(colACAmount) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    ElseIf e.Column Is gvAC.Columns(colACCode) Then
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.getFinder(clsCommon.myCstr(gvAC.CurrentRow.Cells(colACCode).Value), False)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                            gvAC.CurrentRow.Cells(colACCode).Value = obj.Code
                            gvAC.CurrentRow.Cells(colACName).Value = obj.desc
                        Else
                            gvAC.CurrentRow.Cells(colACCode).Value = ""
                            gvAC.CurrentRow.Cells(colACName).Value = ""
                            gvAC.CurrentRow.Cells(colACAmount).Value = 0
                        End If
                    End If
                End If
                setGridFocusAC()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub disableenableadditionaltab()
        If ddlAcqType.Text = "Direct" Then
            gvAC.Rows.AddNew()
        End If
    End Sub

    Function ValidateDistributeAmount() As Boolean
        Dim distrAmt As Decimal
        Dim distrdAmt As Decimal = 0
        For Each grow As GridViewRowInfo In gvAssemble.Rows
            If grow.Cells(colcheck).Value = True Then
                distrAmt = distrAmt + clsCommon.myCdbl(grow.Cells(colItemNetAmount).Value)
            Else
                distrdAmt = distrdAmt + clsCommon.myCdbl(grow.Cells(colDistributeAmount).Value)
            End If
        Next
        If distrAmt <> distrdAmt Then
            Return False
        End If
        Return True
    End Function

    Sub UpdateAssembleTotalAmount()
        For Each grow As GridViewRowInfo In gvAssemble.Rows
            If grow.Cells(colcheck).Value = True Then
                grow.Cells(colTotalAmount).Value = 0
            Else
                grow.Cells(colTotalAmount).Value = clsCommon.myCdbl(grow.Cells(colItemNetAmount).Value) + clsCommon.myCdbl(grow.Cells(colDistributeAmount).Value)
            End If

        Next
    End Sub

    Private Sub gvAssemble_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvAssemble.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column Is gvAssemble.Columns(colDistributeAmount) Then

                    isCellValueChangedOpen = False
                    UpdateAssembleTotalAmount()
                    isCellValueChangedOpen = True
                ElseIf e.Column Is gvAssemble.Columns(colcheck) Then
                    If gvAssemble.CurrentRow.Cells(colcheck).Value = True Then
                        gvAssemble.CurrentRow.Cells(colDistributeAmount).ReadOnly = True
                    Else
                        gvAssemble.CurrentRow.Cells(colDistributeAmount).ReadOnly = False
                    End If

                    'sanjay
                    'ElseIf e.Column Is gvAssemble.Columns(colCostCenter) Then
                    '    gvAssemble.CurrentRow.Cells(colCostCenter).Value = ClsCostCentreFinancial.getFinder("Hirerachy_Level_Code='" & gvAssemble.CurrentRow.Cells(colHierarchy).Value & "'", gvAssemble.CurrentRow.Cells(colCostCenter).Value, False)
                    'ElseIf e.Column Is gvAssemble.Columns(colHierarchy) Then
                    '    gvAssemble.CurrentRow.Cells(colHierarchy).Value = ClsHirerachyLevelMaster.getFinder("", gvAssemble.CurrentRow.Cells(colHierarchy).Value, False)

                    'sanjay
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenSerialItem()
        If clsCommon.CompairString(clsCommon.myCstr(ddlAcqType.Text), "Assembled") = CompairStringResult.Equal Then
            If clsCommon.CompairString(clsCommon.myCstr(gvAssemble.CurrentRow.Cells(colType).Value), "Item") = CompairStringResult.Equal Then
                Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                frm.strItemCode = "" 'clsCommon.myCstr(gvAssemble.CurrentRow.Cells(colAssetsCode).Value)
                frm.strItemName = "" 'clsCommon.myCstr(gvAssemble.CurrentRow.Cells(colAssetName).Value)
                frm.strLocationCode = txtLocation.Value
                'frm.strCurrDocNo = txtDocNo.Value
                'frm.strCurrDocType = "ISSTRAN"
                frm.dblqty = 0
                frm.gv1.ReadOnly = True
                frm.btnFillAuto.Enabled = False
                frm.btnOK.Enabled = False
                frm.txtBarCode.Enabled = False
                gv1.CurrentRow.Tag = clsAcquisitionHead.GetSerialData("ISSTRAN", gvAssemble.CurrentRow.Cells(colDocumentCode).Value, Nothing)
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                If Not frm.arr Is Nothing Then
                    frm.dblqty = frm.arr.Count
                End If

                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            End If
        End If
    End Sub

    Private Sub gvAssemble_KeyDown(sender As Object, e As KeyEventArgs) Handles gvAssemble.KeyDown
        If e.KeyCode = Keys.F4 Then
            'OpenSerialItem()
            If clsCommon.CompairString(clsCommon.myCstr(ddlAcqType.Text), "Assembled") = CompairStringResult.Equal Then
                OpenSerialItem()
            End If
        End If
    End Sub

    Public Sub TaxFillForSRN()
        'Ticket No : ALF/14/09/18-000081  work on print for Alfa dairy Sale Invoice
        '**************************************UDL/15/06/18-000187*********************************************************************************************************
        Dim objMRNHead As clsSRNHead = clsSRNHead.GetData(txtSRNNo.Value, NavigatorType.Current)
        Dim objTaxGrpMaster As New clsTaxGroupMaster()
        objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(objSRN_ACQUTSN.Tax_Group)

        gv2.Rows.Clear()
        If (clsCommon.myLen(objMRNHead.TAX1) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX1
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX1_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX1_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX1_Amt
            strTotalTaxAmount = objMRNHead.TAX1_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX1) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX2) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX2
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX2_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX2_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX2_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX2_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX2) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX3) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX3
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX3_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX3_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX3_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX3_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX3) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX4) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX4
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX4_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX4_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX4_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX4_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX4) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX5) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX5
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX5_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX5_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX5_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX5_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX5) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX6) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX6
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX6_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX6_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX6_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX6_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX6) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX7) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX7
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX7_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX7_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX7_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX7_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX7) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX8) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX8
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX8_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX8_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX8_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX8_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX8) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX9) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX9
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX9_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX9_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX9_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX9_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX9) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        If (clsCommon.myLen(objMRNHead.TAX10) > 0) Then
            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = objMRNHead.TAX10
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = objMRNHead.TAX10_Rate
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = objMRNHead.TAX10_Base_Amt
            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = objMRNHead.TAX10_Amt
            strTotalTaxAmount = strTotalTaxAmount + objMRNHead.TAX10_Amt
            If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                    If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, objMRNHead.TAX10) = CompairStringResult.Equal) Then
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                        Exit For
                    End If
                Next
            End If
        End If
        ' ***********************************************************************************************************************************************

    End Sub
    ''UDL/16/10/18-000231 richa 
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                trans = clsDBFuncationality.GetTransactin()
                If clsAcquisitionHead.ReverseAndUnpost(txtDocNo.Value, trans) Then
                    trans.Commit()
                    saveCancelLog(Reason, "Reverse And Recreate")
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub OpenCostCenterList(ByVal isButtonClick As Boolean)
        If ApplyFinancialCostCenter = True Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
                Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("TSPL_COST_CENTRE_FINANCIAL@AEFinder", qry, "Code", " Hirerachy_Level = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) + "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "", isButtonClick)
                gv1.CurrentRow.Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Cost_Center_Fin_Name  from TSPL_COST_CENTRE_FINANCIAL  where  Cost_Center_Fin_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'")) ' ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCostCenter).Value)
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select hirerachy level first.", Me.Text)
            End If
        Else
            Dim qry As String = "select CostCenter_Code as Code,CostCenter_Name as Name from TSPL_FA_COST_CENTER_MASTER  "
            gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "", isButtonClick)
            gv1.CurrentRow.Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select CostCenter_Name  from TSPL_FA_COST_CENTER_MASTER  where  CostCenter_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'"))
        End If

        'Dim qry As String = "select CostCenter_Code as Code,CostCenter_Name as Name from TSPL_FA_COST_CENTER_MASTER  "

    End Sub

    Private Sub BtnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub

    Private Sub txtDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDate.ValueChanged
        'If clsCommon.myLen(txtSRNNo.Value) > 0 Then
        '    If clsCommon.GetPrintDate(lblSRNDate.Text, "dd/MMM/yyyy") < clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") Then
        '        clsCommon.MyMessageBoxShow("Acquision Date (" + clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) + ") should be greater then SRN Date (" + clsCommon.myCstr(clsCommon.GetPrintDate(lblSRNDate.Text, "dd/MM/yyyy")) + ")", Me.Text)
        '    End If
        'End If
    End Sub

    Private Sub butCostCenterAndHirerachy_Update_AfterPost_Click(sender As Object, e As EventArgs) Handles butCostCenterAndHirerachy_Update_AfterPost.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strJEStatus As String = clsDBFuncationality.getSingleValue("select Authorized from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + txtDocNo.Value + "' ", trans)
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS DISABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim coll As New Hashtable()

                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAssetID).Value)) > 0 Then
                    Dim strAssetCode As String = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", clsCommon.myCstr(grow.Cells(colHierarchyCode).Value), True)
                    clsCommon.AddColumnsForChange(coll, "CostCenter_Code", clsCommon.myCstr(grow.Cells(colCostCenterCode).Value), True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ACQUISITION_DETAIL", OMInsertOrUpdate.Update, "Acquisition_Code='" + txtDocNo.Value + "' and Asset_Code = '" + strAssetCode + "'", trans)
                    Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No = '" + txtDocNo.Value + "' ", trans))
                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        Dim qry As String = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "' WHERE Voucher_No='" + strVoucherNo + "'  "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                End If
            Next
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS ENABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
