Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmAdjProductionStoreEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isFlag As Boolean = False
    Public Const RowTypeAdjustmentQty As String = "Quantity"
    Public Const RowTypeAdjustmentCost As String = "Cost"
    Public Const RowTypeAdjustmentBoth As String = "Both"
    Public Const RowTypeAdjustmentFAT_SNF As String = "FAT/SNF"
    Dim strIndustryType As String = ""
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colIsBatchItem As String = "colIsBatchItem"
    Dim repoicodestatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Dim repoiQCstatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Const colProductyType As String = "ProductType"
    Const colBinNo As String = "colBinNo"
    Const colICodeStatus As String = "Status"
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colBarCode As String = "COLBARCODE"
    Const colAdjustmentType As String = "COLADJTYPE"
    Const colItemType As String = "colItemType"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLQTY"
    Const colItemCost As String = "ITEMCOST"
    Const colCost As String = "COLCOST"
    Const colisMRPMandatory As String = "colisMRPMandatory"
    Const colMRP As String = "MRP"
    Const colRemarks As String = "REMARKS"
    Const colComment As String = "COMMENT"
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"
    Const colIsPickAutoSrNo As String = "colIsPickAutoSrNo"
    Const colFATPers As String = "FAT Pers"
    Const colFATKG As String = "FAT KG"
    Const colSNFPers As String = "SNFPers"
    Const colSNFKG As String = "SNF Kg"
    Const colPrice_Type As String = "colPrice_Type"
    Const colMCC_Price_Code As String = "colMCC_Price_Code"
    Const colBulk_Price_Code As String = "colBulk_Price_Code"
    Const colfat_Rate As String = "colfat_Rate"
    Const colsnf_Rate As String = "colsnf_Rate"
    Const colfat_Amt As String = "colfat_Amt"
    Const colsnf_Amt As String = "colsnf_Amt"
    'Const colQCStatus As String = "colQCStatus"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocumentNo As String = ""
    Dim AllowpurchaseAccounting As Boolean = False
    Public settPickCostFromItemMaster As Boolean = False
    Public settEditItemCost As Boolean = False
    Dim SettDoNotStopOnItemBalanceExceptionStoreAdjustment As Boolean = False
    Dim RunBatchFifowise As Integer = 0
    Dim settTankerDispatchAvgFATSNFPer As Boolean = False
    Dim isFormLoad As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnStoreAdjustment)
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
        If btnSave.Visible = True Then
            RmiExport.Enabled = True
        Else
            RmiExport.Enabled = False
        End If

        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "admin") = CompairStringResult.Equal Then
            RadMenuItem2.Visibility = ElementVisibility.Visible
        Else
            RadMenuItem2.Visibility = ElementVisibility.Collapsed
        End If
    End Sub

    Private Sub LoadAdjustmentHeaderType()
        isInsideLoadData = True
        cboAdjustmentType.SelectedValue = Nothing
        'Dim qry As String = "select '' as Code,'Select' as Name union all select 'ADJ' as Code,'Adjustment' as Name union all select 'FLG' as Code,'Flushing' as Name union all select 'OPG' as Code,'Opening' as Name union all select 'CLG' as Code,'Closing' as Name union all select 'AAD' as Code,'Auto Adjustment' as Name union all select 'OTH' as Code,'Other' as Name"
        Dim qry As String = " select 'PRE' as Code,'Production Entry' as Name"
        cboAdjustmentType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboAdjustmentType.ValueMember = "Code"
        cboAdjustmentType.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Table
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("ProductionStoreEntryNo", "varchar(30)  NOT NULL PRIMARY KEY ")
        'coll.Add("Reference", "varchar(100) NULL")
        'coll.Add("Description", "varchar(300) NULL")
        'coll.Add("Posted", "char(1) NULL")
        'coll.Add("Created_By", "varchar(12)  NOT NULL")
        'coll.Add("Modify_By", "varchar(12)  NOT NULL")
        'coll.Add("Posted_By", "varchar(12)   NULL")
        ''
        'coll.Add("Comp_Code", "varchar(8)  NOT NULL")
        'coll.Add("Against_Item_Stock_Conv_Doc", "varchar(30) NULL")
        'coll.Add("Reference_Document", "Varchar(30) null")
        'coll.Add("Document_No", "Varchar(30) null")
        'coll.Add("Unit_Code", "Varchar(12) null")
        'coll.Add("ItemType", "char(2) null")
        'coll.Add("EMP_CODE", "Varchar(12) null")
        'coll.Add("EMP_NAME", "Varchar(50) null")
        'coll.Add("Customer_CODE", "Varchar(12) null")
        'coll.Add("Customer_NAME", "Varchar(50) null")
        'coll.Add("Created_time", "Varchar(10) null")
        'coll.Add("Modified_Time", "Varchar(10) null")
        'coll.Add("Vehicle_Code", "Varchar(12) null")
        'coll.Add("Vehicle_No", "Varchar(30) null")
        'coll.Add("Challan_No", "Varchar(30) null")
        'coll.Add("Challan_date", "Datetime null")
        'coll.Add("GateEntry_No", "Varchar(30) null")
        'coll.Add("GateEntry_Date", "Datetime null")
        'coll.Add("Loc_Code", "Varchar(12) null")
        'coll.Add("Loc_Desc", "Varchar(50) null")
        'coll.Add("Trans_Type", "Varchar(5) null")
        'coll.Add("ProductionStoreEntry_Date", "Datetime null")
        'coll.Add("Posting_Date", "Datetime null")
        'coll.Add("Created_Date", "Datetime null")
        'coll.Add("Modify_Date", "Datetime null")
        'coll.Add("EntryDateTime", "datetime default null")
        'coll.Add("GateEnt_No", "Varchar(50) null")
        'coll.Add("Is_Imported", "Int NOT NULL Default 0")
        'coll.Add("Stock_Type", "Char(1) Not NUll Default ''")
        'coll.Add("Third_Party_Location", "char(1) Not Null Default 'N'")
        'coll.Add("IsMilkType", "integer not null default 0")
        'coll.Add("MainLocationCode", "Varchar(12) null")
        'coll.Add("MainLocationDesc", "Varchar(50) null")
        'coll.Add("Against_Item_Stock_Conversion", "Varchar(30) null References TSPL_Item_Stock_Conversion_Head(Doc_No)")
        'coll.Add("Against_Bulk_Srn_PI_adjustment", "Varchar(30) null ")
        'coll.Add("Against_AP_Invoice_No", "Varchar(30) null References TSPL_VENDOR_INVOICE_HEAD(Document_No)")
        'coll.Add("Against_Physical_Stock_No", "varchar(50) null")
        'coll.Add("Auto_Gen_Againnt_PI_No", "Varchar(30) null References TSPL_PI_HEAD(PI_No)")
        'coll.Add("Against_Transfer_In_Doc_No", "Varchar(30) null ")
        'coll.Add("Against_Tanker_Dispatch_Doc_No", "Varchar(30) null")

        'coll.Add("FromLocation", "Varchar(30) null ")
        'coll.Add("ToLocation", "Varchar(30) null")
        'coll.Add("isAutoCreatedByMilkTransferIn", "integer not null default 0")

        'coll.Add("Against_PI_No_Difference", "Varchar(30) null References TSPL_PI_HEAD(PI_No)")
        'coll.Add("Against_PI_No_Difference_Rejected", "Varchar(30) null References TSPL_PI_HEAD(PI_No)")
        'coll.Add("AdjustType", "Varchar(10) null")
        'coll.Add("Adjustment_Type", "varchar(3) null")
        'coll.Add("Adjustment_Specification", "varchar(200) null")
        'coll.Add("Is_JobWork", "integer not null default 0")
        'coll.Add("Against_Transfer_In_Return_Doc_No", "Varchar(30) null ")
        'coll.Add("Against_Production_Entry", "varchar(30)  NOT NULL ")
        'coll.Add("Against_Production_Entry_QC", "varchar(30) NOT NULL ")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ADJUSTMENT_STORE_ENTRY_HEAD", coll, Nothing, True, False, Nothing, Nothing, Nothing, True)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("ProductionStoreEntryNo", "varchar(30)  NOT NULL References TSPL_ADJUSTMENT_STORE_ENTRY_HEAD(ProductionStoreEntryNo)")
        'coll.Add("ProductionStoreEntry_Line_No", "int  NOT NULL")
        'coll.Add("Item_Code", "varchar(50)  NOT NULL REFERENCES TSPL_ITEM_MASTER (Item_Code)")
        'coll.Add("Item_Description", "varchar(100) NULL")
        'coll.Add("Adjustment_Type", "char(2)  NOT NULL")
        'coll.Add("Location_Code", "varchar(12)  NOT NULL")
        'coll.Add("Item_Quantity", "decimal (18,2) NULL")
        'coll.Add("Item_Cost", "decimal (18,2) NULL")
        'coll.Add("Unit_Code", "varchar(12) NULL")
        'coll.Add("Account_Code", "varchar(50) NULL")
        'coll.Add("Account_Description", "varchar(100) NULL")
        'coll.Add("Remarks", "varchar(100) NULL")
        'coll.Add("Comments", "varchar(100) NULL")
        'coll.Add("MFG_Date", "date NULL")
        'coll.Add("Batch_No", "varchar(30)  NOT NULL")
        'coll.Add("Expiry_Date", "date NULL")
        'coll.Add("Breakage", "decimal (18,2) NULL")
        'coll.Add("Item_Type", "char(22) NULL")
        'coll.Add("MRP", "Decimal(18,2) null")
        'coll.Add("ItemType", "char(22) null")
        'coll.Add("BreakageType", "Varchar(20) null")
        'coll.Add("Breakage_Cost", "decimal(18,0) null")
        'coll.Add("LeakageQty", "Decimal(18,2) null")
        'coll.Add("Basic_Price", "Decimal(18,2) null")
        'coll.Add("Bar_Code", "Varchar(30) null References TSPL_ITEM_BARCODE(Bar_Code)")
        'coll.Add("Item_Status", "varchar(3) Not Null Default 'NEW'")
        'coll.Add("FAT_Pers", "float NULL")
        'coll.Add("FAT_KG", "float NULL")
        'coll.Add("SNF_Pers", "float NULL")
        'coll.Add("SNF_KG", "float NULL")
        'coll.Add("Unit_Cost", "decimal(18,0) Default 0")

        'coll.Add("Fat_Rate", "float not null default 0")
        'coll.Add("SNF_Rate", "float not null default 0")
        'coll.Add("Fat_Amt", "float not null default 0")
        'coll.Add("SNF_Amt", "float not null default 0")
        'coll.Add("Price_Type", "varchar(30) Null ")
        'coll.Add("MCC_Price_Code", "Varchar(30) null References TSPL_MILK_PRICE_MASTER(Price_Code)")
        'coll.Add("Bulk_Price_Code", "Varchar(30) null References TSPL_Bulk_Price_MASTER(Price_Code)")
        'coll.Add("Bin_No", "varchar(50) NULL")

        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL", coll, Nothing, True, False, Nothing, Nothing, Nothing, True)
        'Table
        isFormLoad = True
        settPickCostFromItemMaster = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickCostFromItemMaster, clsFixedParameterCode.PickCostFromItemMaster, Nothing)) = 1)
        settEditItemCost = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EditItemCost, clsFixedParameterCode.EditItemCost, Nothing)) = 1)
        If Not settPickCostFromItemMaster Then
            settEditItemCost = True
        End If
        AllowpurchaseAccounting = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, Nothing)) = 0, False, True)
        SettDoNotStopOnItemBalanceExceptionStoreAdjustment = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotStopOnItemBalanceExceptionStoreAdjustment, clsFixedParameterCode.DoNotStopOnItemBalanceExceptionStoreAdjustment, Nothing)) > 0)
        '========Added by preet gupta against ticket no[BHA/23/08/18-000477]
        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))
        settTankerDispatchAvgFATSNFPer = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, Nothing)) = 1)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()
        strIndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing))
        LoadTransType()

        LoadBlankGrid()
        LoadAdjustmentHeaderType()

        AddNew()

        '---------------------Done By Monika-------------------
        If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            chklocation.Visible = True
        Else
            chklocation.Checked = False
        End If
        '----------------------------------------------------

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If

        RadPageView1.SelectedPage = RadPageViewPage1
        isFormLoad = False
    End Sub

    Sub SetLength()
        txtAdjustmentNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtReference.MaxLength = 200
        cboTransType.MaxLength = 1
    End Sub

    Sub LoadTransType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "In"
        dr("Name") = "In"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Out"
        dr("Name") = "Out"
        dt.Rows.Add(dr)

        cboTransType.DataSource = dt
        cboTransType.ValueMember = "Code"
        cboTransType.DisplayMember = "Name"
    End Sub


    Private Function GetAdjustmentType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr("Code") = RowTypeAdjustmentQty
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeAdjustmentCost
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeAdjustmentBoth
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeAdjustmentFAT_SNF
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Function GetPriceType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Bulk"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)


        repoicodestatus.FormatString = ""
        repoicodestatus.HeaderText = "Status"
        repoicodestatus.Name = colICodeStatus
        repoicodestatus.ReadOnly = True
        repoicodestatus.Width = 100
        repoicodestatus.DataSource = FillComboboxGridNEW()
        repoicodestatus.DisplayMember = "value"
        repoicodestatus.ValueMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoicodestatus)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoBarcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBarcode.FormatString = ""
        repoBarcode.HeaderText = "BAR Code"
        repoBarcode.Name = colBarCode
        repoBarcode.IsVisible = False
        repoBarcode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBarcode)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Adjustment Type"
        repoRowType.Name = colAdjustmentType
        repoRowType.Width = 100
        repoRowType.ReadOnly = True
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetAdjustmentType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoproducttype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoproducttype.FormatString = ""
        repoproducttype.HeaderText = "Product Type"
        repoproducttype.Name = colProductyType
        repoproducttype.Width = 100
        repoproducttype.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoproducttype)

        repoproducttype = New GridViewTextBoxColumn()
        repoproducttype.FormatString = ""
        repoproducttype.HeaderText = "Item Type"
        repoproducttype.Name = colItemType
        repoproducttype.IsVisible = False
        repoproducttype.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoproducttype)


        Dim repoBinNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinNo.FormatString = ""
        repoBinNo.HeaderText = "Bin No"
        repoBinNo.Name = colBinNo
        repoBinNo.ReadOnly = IIf(clsCommon.CompairString(strIndustryType, "R") = CompairStringResult.Equal, False, True)
        repoBinNo.Width = 100
        repoBinNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBinNo)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:N2}"
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = False
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)



        Dim repoItemCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemCost.FormatString = "{0:N2}"
        repoItemCost.HeaderText = "Item Cost"
        repoItemCost.Name = colItemCost
        repoItemCost.ReadOnly = True
        repoItemCost.Width = 80
        repoItemCost.Minimum = 0
        repoItemCost.ShowUpDownButtons = False
        repoItemCost.Step = 0
        repoItemCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        'KUNAL > DATE : 10-JAN-2016 > CLIENT : MPD > REQ NO : MPDREQ000020  
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "MPD") = CompairStringResult.Equal Then
            repoItemCost.DecimalPlaces = 3
            repoItemCost.FormatString = "{0:N3}"
        End If

        gv1.MasterTemplate.Columns.Add(repoItemCost)

        Dim repofatpers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatpers.FormatString = "{0:N2}"
        repofatpers.HeaderText = "FAT%"
        repofatpers.Width = 60
        repofatpers.Minimum = 0
        repofatpers.DecimalPlaces = 2
        repofatpers.Name = colFATPers
        repofatpers.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatpers)

        Dim repofatkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatkg.FormatString = ""
        repofatkg.HeaderText = "FAT KG"
        repofatkg.Width = 60
        repofatkg.DecimalPlaces = 2
        repofatkg.Name = colFATKG
        repofatkg.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatkg)

        Dim reposnfpers As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfpers.FormatString = "{0:N2}"
        reposnfpers.HeaderText = "SNF%"
        reposnfpers.Width = 60
        reposnfpers.DecimalPlaces = 2
        reposnfpers.Minimum = 0
        reposnfpers.Name = colSNFPers
        reposnfpers.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfpers)

        Dim reposnfkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfkg.FormatString = ""
        reposnfkg.HeaderText = "SNF KG"
        reposnfkg.Width = 60
        reposnfkg.DecimalPlaces = 2
        reposnfkg.Name = colSNFKG
        reposnfkg.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfkg)

        Dim repoPriceType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoPriceType.FormatString = ""
        repoPriceType.HeaderText = "Price Type"
        repoPriceType.Name = colPrice_Type
        repoPriceType.Width = 100
        repoPriceType.ReadOnly = True
        repoPriceType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoPriceType.DataSource = GetPriceType()
        repoPriceType.ValueMember = "Code"
        repoPriceType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoPriceType)

        Dim repoMCCPrice As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMCCPrice.FormatString = ""
        repoMCCPrice.HeaderText = "MCC Price Code"
        repoMCCPrice.Name = colMCC_Price_Code
        repoMCCPrice.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoMCCPrice.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMCCPrice.Width = 100
        repoMCCPrice.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMCCPrice)

        Dim repoBulkPrice As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBulkPrice.FormatString = ""
        repoBulkPrice.HeaderText = "Bulk Price Code"
        repoBulkPrice.Name = colBulk_Price_Code
        repoBulkPrice.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoBulkPrice.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBulkPrice.Width = 100
        repoBulkPrice.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBulkPrice)



        Dim repofatRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatRate.FormatString = ""
        repofatRate.HeaderText = "FAT Rate"
        repofatRate.Width = 60
        repofatRate.DecimalPlaces = 2
        repofatRate.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repofatRate.Name = colfat_Rate
        repofatRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatRate)

        Dim repofatAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatAmt.FormatString = ""
        repofatAmt.HeaderText = "FAT Amount"
        repofatAmt.Width = 60
        repofatAmt.DecimalPlaces = 2
        repofatAmt.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repofatAmt.Name = colfat_Amt
        repofatAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatAmt)

        Dim reposnfRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfRate.FormatString = ""
        reposnfRate.HeaderText = "SNF Rate"
        reposnfRate.Width = 60
        reposnfRate.DecimalPlaces = 2
        reposnfRate.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        reposnfRate.Name = colsnf_Rate
        reposnfRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfRate)

        Dim reposnfAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfAmt.FormatString = ""
        reposnfAmt.HeaderText = "SNF Amount"
        reposnfAmt.Width = 60
        reposnfAmt.DecimalPlaces = 2
        reposnfAmt.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        reposnfAmt.Name = colsnf_Amt
        reposnfAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfAmt)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = "{0:N2}"
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colCost
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.ShowUpDownButtons = False
        repoAmt.Step = 0
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colisMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsMRPMandatory)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.WrapText = True
        repoMRP.ReadOnly = True
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.Minimum = 0
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ShowUpDownButtons = False
        repoMRP.Step = 0
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 150
        repoRemarks.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Comment"
        repoSpecification.Name = colComment
        repoSpecification.Width = 150
        repoSpecification.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSpecification)

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIsPickAutoSrNo
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsPickAutoSerNo)

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsBatchItem)

        'repoiQCstatus.FormatString = ""
        'repoiQCstatus.HeaderText = "QC Status"
        'repoiQCstatus.Name = colQCStatus
        'repoiQCstatus.Width = 100
        'repoiQCstatus.DataSource = FillComboboxGridNEWQC()
        'repoiQCstatus.DisplayMember = "value"
        'repoiQCstatus.ValueMember = "Code"
        'repoiQCstatus.ReadOnly = False
        'gv1.MasterTemplate.Columns.Add(repoiQCstatus)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Function FillComboboxGridNEW() As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "NEW"
        dr("Value") = "NEW"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OLD"
        dr("Value") = "OLD"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Function FillComboboxGridNEWQC() As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "OK"
        dr("Value") = "OK"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Not OK"
        dr("Value") = "Not OK"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
        txtProductionEntry.Value = ""
        txtProductionEntry.Enabled = True
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        'rbtnExportPosted.Visible = False
        'rbtnImportPosted.Visible = False
        'cmdEditAndPost.Visible = False
        ChkMilkType.Enabled = True
        txtDate.Focus()
        'gv1.Rows.AddNew()
        'gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
        chkJobWork.Visible = False
        chkJobWork.Enabled = True
        chkJobWork.Checked = False
        'btnCopy.Enabled = True
    End Sub

    Sub BlankAllControls()
        lblProductionEntry.Text = ""
        chklocation.Checked = False
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtDesc.Text = ""
        txtReference.Text = ""
        txtAdjustmentNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        cboTransType.SelectedIndex = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtReference.Text = ""
        cboAdjustmentType.SelectedValue = ""
        txtSpecification.Text = ""
        txtSpecification.Enabled = False
        cboAdjustmentType.Enabled = True


        ''added by richa 09/10/2014
        Dim desc As String = ""
        desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToShowMilkTypeinAdjustmentEntry, clsFixedParameterCode.AllowToShowMilkTypeinAdjustmentEntry, Nothing))

        If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
            ChkMilkType.Visible = True
            ChkMilkType.Checked = False
        Else
            ChkMilkType.Visible = False
            ChkMilkType.Checked = False

        End If


        FndMainLocation.Enabled = False
        RadLabel15.Text = "Location"
        FndMainLocation.Value = ""
        LblMainLocation.Text = ""
        ''======================
        'LoadQCStatus()
    End Sub

    Public Sub OpenBatchItemIfFIFIOSettingON() '============created by preeti gupta[BHA/23/10/18-000636]===============
        Dim arr As List(Of clsBatchInventory) = Nothing
        Dim strBatchunion As String = ""
        'For Each grow As GridViewRowInfo In gv1.Rows
        '    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
        '        arr = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
        '    End If
        'Next
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
        End If

        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventory In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        ''===Added by preeti gupta[23/08/2018]========='commented by preeti gupta [now function will work on F5]
        'If e.Column.Name = colIName Then
        '    Dim arr As List(Of clsBatchInventory) = Nothing
        '    Dim strBatchunion As String = ""
        '    arr = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
        '    If Not arr Is Nothing Then
        '        If arr.Count > 0 Then
        '            For Each obj As clsBatchInventory In arr
        '                strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
        '            Next
        '            clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
        '        End If
        '    End If

        'End If
        ''===========================================
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAdjustmentType) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colComment) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colAdjustmentType) Or e.Column Is gv1.Columns(colItemCost) Then

                            If e.Column Is gv1.Columns(colQty) Then
                                ' If RunBatchFifowise = 0 Then ' Or cboTransType.SelectedValue = "In"
                                If ChkMilkType.Checked Then
                                    OpenBatchItemNew()
                                Else
                                    OpenBatchItem()
                                End If
                                ' End If
                            End If
                            'End If
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        End If
                        If (e.Column Is gv1.Columns(colQty)) Then
                            If Not (clsCommon.myCBool(gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value) AndAlso clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "In") = CompairStringResult.Equal) Then
                                OpenSerialItem()
                            End If
                        End If
                    End If
                    If (e.Column Is gv1.Columns(colFATPers)) OrElse (e.Column Is gv1.Columns(colQty)) OrElse (e.Column Is gv1.Columns(colfat_Rate)) OrElse (e.Column Is gv1.Columns(colUnit)) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), "FAT/SNF") <> CompairStringResult.Equal Then
                            CalFATKG() ''when other than fat/snf then calc. done on qty. based,otherwise manual filled
                        End If

                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                        End If
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                            'If clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colfat_Rate).ReadOnly = False
                            'End If
                        End If
                    End If
                    If (e.Column Is gv1.Columns(colSNFPers)) OrElse (e.Column Is gv1.Columns(colQty)) OrElse (e.Column Is gv1.Columns(colsnf_Rate)) Then

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), "FAT/SNF") <> CompairStringResult.Equal Then
                            CalSNFKG() ''when other than fat/snf then calc. done on qty. based,otherwise manual filled
                        End If

                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                        End If
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                            'If clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colfat_Rate).ReadOnly = False
                            'End If
                        End If
                    End If
                    If clsCommon.CompairString(cboTransType.SelectedValue, "IN") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
                        If e.Column Is gv1.Columns(colPrice_Type) Then
                            If clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = False
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
                            ElseIf clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = False
                            End If
                        End If
                    Else

                        gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
                        gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
                    End If


                    If clsCommon.CompairString(cboTransType.SelectedValue, "IN") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
                        If e.Column Is gv1.Columns(colMCC_Price_Code) Then
                            OpenMCCPriceList(False)
                            If clsCommon.myLen(gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value) > 0 Then

                                If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                                End If

                                Dim arr As New clsFatSnfRateCalculator
                                Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" & gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value & "'", Nothing)
                                If dtMilkPrice IsNot Nothing AndAlso dtMilkPrice.Rows.Count > 0 Then
                                    If objCommonVar.ApplyStdFATSNFRate Then
                                        arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colQty).Value), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value))
                                    Else
                                        If clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value) And clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Pers")) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value) Then
                                            arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")))
                                        Else
                                            arr = clsFatSnfRateCalculator.CalculateIn(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Milk_Rate")), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colItemCost).Value))
                                        End If
                                    End If
                                End If


                                gv1.Rows(e.RowIndex).Cells(colfat_Rate).Value = Math.Round(arr.fatR, 2)
                                gv1.Rows(e.RowIndex).Cells(colfat_Amt).Value = Math.Round(arr.FatAmt, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Rate).Value = Math.Round(arr.snfR, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Amt).Value = Math.Round(arr.snfAmt, 2)
                                dtMilkPrice = Nothing
                                arr = Nothing
                            End If
                        ElseIf e.Column Is gv1.Columns(colBulk_Price_Code) Then
                            OpenBulkPriceList(False)
                            If clsCommon.myLen(gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value) > 0 Then
                                If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                                End If

                                Dim arr As New clsFatSnfRateCalculator
                                Dim objPrice As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value, NavigatorType.Current, Nothing)

                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value))
                                Else
                                    If clsCommon.myCdbl(objPrice.Fat_Percentage) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value) And clsCommon.myCdbl(objPrice.Snf_Percentage) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value) Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value), clsCommon.myCdbl(objPrice.Standard_Rate), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colItemCost).Value))
                                    End If
                                End If


                                gv1.Rows(e.RowIndex).Cells(colfat_Rate).Value = Math.Round(arr.fatR, 2)
                                gv1.Rows(e.RowIndex).Cells(colfat_Amt).Value = Math.Round(arr.FatAmt, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Rate).Value = Math.Round(arr.snfR, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Amt).Value = Math.Round(arr.snfAmt, 2)
                                objPrice = Nothing
                                arr = Nothing
                            End If
                        End If
                    ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Out") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
                        '' production costing columns
                        Dim objCost As New MIlkComponentType
                        Dim Loc_code As String = ""
                        Dim Main_Loc_code As String = ""
                        If clsCommon.myLen(txtLocation.Value) <= 0 Then
                            Loc_code = FndMainLocation.Value
                            Main_Loc_code = ""
                        Else
                            Loc_code = txtLocation.Value
                            Main_Loc_code = FndMainLocation.Value
                        End If

                    End If

                    If ChkMilkType.Checked = True Then
                        If AllowpurchaseAccounting = False Then
                            gv1.Rows(e.RowIndex).Cells(colCost).Value = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colfat_Amt).Value) + clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colsnf_Amt).Value)
                            If clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colAdjustmentType).Value, "Both") = CompairStringResult.Equal Then

                                If clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colQty).Value) > 0 Then
                                    gv1.Rows(e.RowIndex).Cells(colItemCost).Value = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colCost).Value) / clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colQty).Value)
                                Else
                                    gv1.Rows(e.RowIndex).Cells(colItemCost).Value = 0
                                End If


                            End If
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function GetMilkRate(ByVal Price_Type As String, ByVal Price_Code As String) As Decimal
        Dim DtMCCPrice As New DataTable
        Dim objBulkPrice As New clsPriceChartBulkProc
        Dim MilkRate As Decimal = 0
        If clsCommon.CompairString(Price_Type, "MCC") = CompairStringResult.Equal Then
            DtMCCPrice = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" & Price_Code & "'")
            MilkRate = ClsAdjustmentsQCC.GetMilkRate(DtMCCPrice.Rows(0).Item("Ratio"), DtMCCPrice.Rows(0).Item("Snf_Ratio"), DtMCCPrice.Rows(0).Item("Fat_Pers"), DtMCCPrice.Rows(0).Item("SNF_Pers"), DtMCCPrice.Rows(0).Item("Milk_Rate"), gv1.CurrentRow.Cells(colFATKG).Value, gv1.CurrentRow.Cells(colSNFKG).Value, gv1.CurrentRow.Cells(colQty).Value)

        ElseIf clsCommon.CompairString(Price_Type, "Bulk") = CompairStringResult.Equal Then
            objBulkPrice = clsPriceChartBulkProc.GetData(Price_Code, NavigatorType.Current, Nothing)
            MilkRate = ClsAdjustmentsQCC.GetMilkRate(objBulkPrice.Fat_Weightage, objBulkPrice.Snf_Weightage, objBulkPrice.Fat_Percentage, objBulkPrice.Snf_Percentage, objBulkPrice.Standard_Rate, gv1.CurrentRow.Cells(colFATKG).Value, gv1.CurrentRow.Cells(colSNFKG).Value, gv1.CurrentRow.Cells(colQty).Value)
        Else
            MilkRate = 0
        End If
        Return MilkRate
    End Function

    Function GetMilkRateImport(ByVal Price_Type As String, ByVal Price_Code As String, ByVal Fatkg As Decimal, ByVal SnfKg As Decimal, ByVal Qty As Decimal) As Decimal
        Dim DtMCCPrice As New DataTable
        Dim objBulkPrice As New clsPriceChartBulkProc
        Dim MilkRate As Decimal = 0
        If clsCommon.CompairString(Price_Type, "MCC") = CompairStringResult.Equal Then
            DtMCCPrice = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" & Price_Code & "'")
            MilkRate = ClsAdjustmentsQCC.GetMilkRate(DtMCCPrice.Rows(0).Item("Ratio"), DtMCCPrice.Rows(0).Item("Snf_Ratio"), DtMCCPrice.Rows(0).Item("Fat_Pers"), DtMCCPrice.Rows(0).Item("SNF_Pers"), DtMCCPrice.Rows(0).Item("Milk_Rate"), Fatkg, SnfKg, Qty)

        ElseIf clsCommon.CompairString(Price_Type, "Bulk") = CompairStringResult.Equal Then
            objBulkPrice = clsPriceChartBulkProc.GetData(Price_Code, NavigatorType.Current, Nothing)
            MilkRate = ClsAdjustmentsQCC.GetMilkRate(objBulkPrice.Fat_Weightage, objBulkPrice.Snf_Weightage, objBulkPrice.Fat_Percentage, objBulkPrice.Snf_Percentage, objBulkPrice.Standard_Rate, Fatkg, SnfKg, Qty)
        Else
            MilkRate = 0
        End If
        Return MilkRate
    End Function

    Sub CalFATKG()
        Try
            Dim pers As Decimal = 0
            Dim kg As Decimal = 0
            Dim qty As Decimal = 0

            qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            pers = clsCommon.myCdbl(gv1.CurrentRow.Cells(colFATPers).Value)

            If pers > 0 Then
                kg = (qty * pers) / 100

                kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), qty, pers, Nothing)
            End If

            gv1.CurrentRow.Cells(colFATKG).Value = kg
            gv1.CurrentRow.Cells(colfat_Amt).Value = gv1.CurrentRow.Cells(colFATKG).Value * gv1.CurrentRow.Cells(colfat_Rate).Value
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub CalSNFKG()
        Try
            Dim pers As Decimal = 0
            Dim kg As Decimal = 0
            Dim qty As Decimal = 0

            qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            pers = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNFPers).Value)

            If pers > 0 Then
                kg = (qty * pers) / 100

                kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), qty, pers, Nothing)
            End If

            gv1.CurrentRow.Cells(colSNFKG).Value = kg
            gv1.CurrentRow.Cells(colsnf_Amt).Value = gv1.CurrentRow.Cells(colSNFKG).Value * gv1.CurrentRow.Cells(colsnf_Rate).Value

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value)
        Dim obj As clsItemMaster
        '' changes by richa agarwal add condition in finder " Product_Type ='MI'"
        If ChkMilkType.Checked Then
            Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, Nothing))
            Dim strItemLoc As String = ""
            If ShowLocationItemLocationwise = 1 Then
                strItemLoc = " and Item_code in ( select Item_code  from TSPL_LOCATION_ITEMMAPPING where location_code ='" & clsCommon.myCstr(txtLocation.Value) & "')"
            End If
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick, " Product_Type ='MI'" & strItemLoc)
        Else
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick, " coalesce(Product_Type,'') <>'MI'")
        End If

        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            gv1.CurrentRow.Cells(colItemType).Value = obj.Item_Type
            ''richa agarwal BHA/09/05/18-000021
            If obj.Can = True Then
                Dim dblCanRate As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, Nothing))
                gv1.CurrentRow.Cells(colItemCost).Value = dblCanRate
                gv1.CurrentRow.Cells(colCost).Value = dblCanRate * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                gv1.CurrentRow.Cells(colItemCost).ReadOnly = True
            ElseIf obj.Crate = True Then
                Dim dblCrateRate As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateRate, clsFixedParameterCode.ItemCrateRate, Nothing))
                gv1.CurrentRow.Cells(colItemCost).Value = dblCrateRate
                gv1.CurrentRow.Cells(colCost).Value = dblCrateRate * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                gv1.CurrentRow.Cells(colItemCost).ReadOnly = True
            Else
                gv1.CurrentRow.Cells(colItemCost).Value = obj.Cost
                gv1.CurrentRow.Cells(colCost).Value = obj.Cost * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                gv1.CurrentRow.Cells(colItemCost).ReadOnly = False
                SetUnitCost()
            End If
            ''--------------
            gv1.CurrentRow.Cells(colIsSerialseItem).Value = obj.Is_Serial_Item
            gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value = obj.Is_Pick_Auto_SrNo
            gv1.CurrentRow.Cells(colisMRPMandatory).Value = obj.Is_MRP
            gv1.CurrentRow.Cells(colProductyType).Tag = obj.Product_Type
            gv1.CurrentRow.Cells(colIsBatchItem).Value = obj.Is_Batch_Item
            gv1.CurrentRow.Cells(colBinNo).Value = obj.Rack_No
            gv1.CurrentRow.Cells(colProductyType).Value = ProductType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + obj.Item_Code + "'")))
            '' done by panch raj against Ticket No: BM00000007708:
            '' check for milk product
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                Dim objPer As MIlkComponentType = clsItemMaster.GetItemFatSNF(gv1.CurrentRow.Cells(colICode).Value, Nothing)
                gv1.CurrentRow.Cells(colFATPers).Value = objPer.FAT_Per
                gv1.CurrentRow.Cells(colSNFPers).Value = objPer.SNF_Per
            End If
            If (ChkMilkType.Checked = True AndAlso chkJobWork.Checked = True AndAlso clsCommon.CompairString(cboAdjustmentType.SelectedValue, "ADJ") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), "Quantity") = CompairStringResult.Equal) Then
                gv1.CurrentRow.Cells(colQty).ReadOnly = True
                gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
                gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
                gv1.CurrentRow.Cells(colSNFPers).ReadOnly = True
                gv1.CurrentRow.Cells(colFATPers).ReadOnly = True
            End If

            If ChkMilkType.Checked Then
                If settTankerDispatchAvgFATSNFPer AndAlso clsCommon.CompairString(cboAdjustmentType.SelectedValue, "FLG") = CompairStringResult.Equal Then
                    Dim strLoc As String = txtLocation.Value
                    If clsCommon.myLen(strLoc) <= 0 Then
                        strLoc = FndMainLocation.Value
                    End If
                    Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, "", "MI", obj.Item_Code, strLoc, 1, obj.Unit_Code, 1, 1, txtDate.Value, txtDate.Value, False, Nothing, txtAdjustmentNo.Value)
                    If objMCT IsNot Nothing Then
                        gv1.CurrentRow.Cells(colfat_Rate).Value = objMCT.FAT_Cost
                        gv1.CurrentRow.Cells(colsnf_Rate).Value = objMCT.SNF_Cost

                        gv1.CurrentRow.Cells(colFATPers).Value = objMCT.FAT_Per
                        gv1.CurrentRow.Cells(colSNFPers).Value = objMCT.SNF_Per
                    End If
                ElseIf settPickCostFromItemMaster Then
                    Dim objQCPAR As clsItemMasterQCParameter = clsItemMasterQCParameter.GetStandardFATSNFRate(obj.Item_Code, Nothing)
                    If objQCPAR IsNot Nothing Then
                        gv1.CurrentRow.Cells(colfat_Rate).Value = objQCPAR.FATRate
                        gv1.CurrentRow.Cells(colsnf_Rate).Value = objQCPAR.SNFRate

                        gv1.CurrentRow.Cells(colFATPers).Value = objQCPAR.FATPer
                        gv1.CurrentRow.Cells(colSNFPers).Value = objQCPAR.SNFPer
                    End If
                End If
            End If

        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Private Function ProductType(ByVal Product_type As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(Product_type, "MI") = CompairStringResult.Equal Then
            values = "Milk"
        ElseIf clsCommon.CompairString(Product_type, "CH") = CompairStringResult.Equal Then
            values = "Cheese"
        ElseIf clsCommon.CompairString(Product_type, "MB") = CompairStringResult.Equal Then
            values = "Melted Butter"
        ElseIf clsCommon.CompairString(Product_type, "CU") = CompairStringResult.Equal Then
            values = "Curd"
        ElseIf clsCommon.CompairString(Product_type, "CA") = CompairStringResult.Equal Then
            values = "Cream"
        ElseIf clsCommon.CompairString(Product_type, "BU") = CompairStringResult.Equal Then
            values = "Butter"
        ElseIf clsCommon.CompairString(Product_type, "BM") = CompairStringResult.Equal Then
            values = "Butter Milk"
        ElseIf clsCommon.CompairString(Product_type, "") = CompairStringResult.Equal Then
            values = "Others"
        End If

        Return values
    End Function

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Item Code")
            Exit Sub
        End If


        Dim qry As String = "select  UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL "

        Dim WhrCls As String = "Item_Code ='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("AdjStoreUOM", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
            'qry = "select top 1 Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER where Item_Code ='" + strICode + "' and UOM='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"
            'gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            SetUnitCost()
        End If
    End Sub

    Sub SetUnitCost()
        ''BHA/01/08/18-000207 by balwinder on 02/08/2018
        If settPickCostFromItemMaster Then
            Dim qry As String = "select  Item_Cost from TSPL_ITEM_UOM_DETAIL where Item_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"
            gv1.CurrentRow.Cells(colItemCost).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        End If
    End Sub

    Sub OpenMCCPriceList(ByVal isButtonClick As Boolean)
        Dim Code As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
        If clsCommon.myLen(Code) <= 0 Then
            'common.clsCommon.MyMessageBoxShow("Please select MCC Price Code")
            Exit Sub
        End If


        Dim qry As String = " select * from (select distinct TSPL_MILK_PRICE_MASTER.Price_Code as Code,TSPL_MILK_PRICE_MASTER.Effective_Date as [Price Date]," &
                            " TSPL_MILK_PRICE_MASTER.Description,TSPL_MILK_PRICE_MASTER.Ratio as [Fat Ratio],TSPL_MILK_PRICE_MASTER.SNF_Ratio as [SNF Ratio]," &
                            " TSPL_MILK_PRICE_MASTER.FAT_Pers as [Fat %],TSPL_MILK_PRICE_MASTER.SNF_Pers as [SNF %],TSPL_MILK_PRICE_MASTER.Milk_Rate as [Milk Rate] " &
                            " from TSPL_MILK_PRICE_MASTER where Price_Code in (select Distinct Price_Code from tspl_Fat_SNf_Uploader_Master inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.code where Mcc_Code='" & FndMainLocation.Value & "')) Price"

        Dim WhrCls As String = "" ''"TSPL_FAT_SNF_UPLOADER_MASTER.Code='" & FndMainLocation.Value & "'"
        gv1.CurrentRow.Cells(colMCC_Price_Code).Value = clsCommon.ShowSelectForm("AdjStoreMCCPrice", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colMCC_Price_Code).Value), "Code", isButtonClick)

    End Sub

    Sub OpenBulkPriceList(ByVal isButtonClick As Boolean)
        Dim Code As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
        If clsCommon.myLen(Code) <= 0 Then
            'common.clsCommon.MyMessageBoxShow("Please select MCC Price Code")
            Exit Sub
        End If


        Dim qry As String = "select Price_Code as Code,Price_Date as [Price Date],Fat_Weightage as [Fat Ratio],Snf_Weightage as [SNF Ratio]," &
            " Fat_Percentage as [Fat %],Snf_Percentage as [SNF %],Standard_Rate as [Milk Rate] from TSPL_Bulk_Price_MASTER "

        Dim WhrCls As String = "" ''= "Location_Code='" & FndMainLocation.Value & "'"
        gv1.CurrentRow.Cells(colBulk_Price_Code).Value = clsCommon.ShowSelectForm("AdjStoreBulkPrice", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colBulk_Price_Code).Value), "Code", isButtonClick)

    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colCost).Value = 0
        gv1.CurrentRow.Cells(colisMRPMandatory).Value = False
        gv1.CurrentRow.Cells(colProductyType).Value = ""
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colCost).Value = 0
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colCost).Value = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemCost).Value)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colQty).Value = 0
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentFAT_SNF) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colQty).Value = 0
                gv1.Rows(IntRowNo).Cells(colCost).Value = 0
                gv1.Rows(IntRowNo).Cells(colItemCost).Value = 0
            End If
        End If
    End Sub

    Private Sub UpdateAllTotals()
        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
        '        isCellValueChangedOpen = True
        '        If ChkMilkType.Checked Then
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).Value = "None"
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).ReadOnly = False
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).Value = ""
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).Value = ""
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).ReadOnly = False
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).ReadOnly = False
        '        Else
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).Value = "None"
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).ReadOnly = True
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).Value = ""
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).Value = ""
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).ReadOnly = False
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).ReadOnly = False
        '        End If
        '        isCellValueChangedOpen = False
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        'For i As Integer = 0 To gv1.Rows.Count - 1
        '    gv1.Rows(0).Cells(0).Value = 1
        '    If i <> 0 Then
        '        gv1.Rows(i).Cells(colLineNo).Value = clsCommon.myCstr(i + 1)
        '    End If
        'Next
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            'If (ChkMilkType.Checked = True AndAlso chkJobWork.Checked = True AndAlso clsCommon.CompairString(cboAdjustmentType.SelectedValue, "ADJ") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), "Quantity") = CompairStringResult.Equal) Then
            '    gv1.CurrentRow.Cells(colQty).ReadOnly = True
            '    gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
            '    gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
            '    gv1.CurrentRow.Cells(colSNFPers).ReadOnly = True
            '    gv1.CurrentRow.Cells(colFATPers).ReadOnly = True
            'ElseIf settTankerDispatchAvgFATSNFPer AndAlso clsCommon.CompairString(cboAdjustmentType.SelectedValue, "FLG") = CompairStringResult.Equal Then
            '    gv1.CurrentRow.Cells(colItemCost).ReadOnly = True
            '    gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '    gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '    gv1.CurrentRow.Cells(colSNFPers).ReadOnly = False
            '    gv1.CurrentRow.Cells(colFATPers).ReadOnly = False
            'Else
            '    If e.Column Is gv1.Columns(colQty) Then
            '        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colQty).ReadOnly = False
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colQty).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colQty).ReadOnly = False
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentFAT_SNF) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colQty).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
            '        End If
            '    ElseIf e.Column Is gv1.Columns(colAdjustmentType) Then
            '        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = False
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentFAT_SNF) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
            '        End If
            '    ElseIf e.Column Is gv1.Columns(colCost) Then
            '        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = False
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = False
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentFAT_SNF) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colCost).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
            '        End If
            '    ElseIf e.Column Is gv1.Columns(colItemCost) Then
            '        gv1.CurrentRow.Cells(colItemCost).ReadOnly = False
            '        'If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemType).Value), "F") = CompairStringResult.Equal Then
            '        If settPickCostFromItemMaster Then
            '            If Not settEditItemCost Then
            '                gv1.CurrentRow.Cells(colItemCost).ReadOnly = True
            '            End If
            '            ' End If
            '        End If
            '    ElseIf e.Column Is gv1.Columns(colProductyType) Then
            '        If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MI") = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
            '            gv1.CurrentRow.Cells(colFATPers).ReadOnly = False
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
            '            gv1.CurrentRow.Cells(colSNFPers).ReadOnly = False
            '            gv1.CurrentRow.Cells(colfat_Rate).ReadOnly = False
            '            gv1.CurrentRow.Cells(colsnf_Rate).ReadOnly = False
            '            gv1.CurrentRow.Cells(colfat_Amt).ReadOnly = False
            '            gv1.CurrentRow.Cells(colsnf_Amt).ReadOnly = False
            '        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
            '            gv1.CurrentRow.Cells(colFATPers).ReadOnly = False
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
            '            gv1.CurrentRow.Cells(colSNFPers).ReadOnly = False
            '            gv1.CurrentRow.Cells(colfat_Rate).ReadOnly = False
            '            gv1.CurrentRow.Cells(colfat_Amt).ReadOnly = False
            '            gv1.CurrentRow.Cells(colsnf_Rate).ReadOnly = False
            '            gv1.CurrentRow.Cells(colsnf_Amt).ReadOnly = False
            '        Else

            '            gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATPers).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
            '            gv1.CurrentRow.Cells(colSNFPers).ReadOnly = True
            '            gv1.CurrentRow.Cells(colFATKG).Value = Nothing
            '            gv1.CurrentRow.Cells(colFATPers).Value = Nothing
            '            gv1.CurrentRow.Cells(colSNFKG).Value = Nothing
            '            gv1.CurrentRow.Cells(colSNFPers).Value = Nothing

            '            gv1.CurrentRow.Cells(colfat_Rate).ReadOnly = True
            '            gv1.CurrentRow.Cells(colfat_Amt).ReadOnly = True
            '            gv1.CurrentRow.Cells(colsnf_Rate).ReadOnly = True
            '            gv1.CurrentRow.Cells(colsnf_Amt).ReadOnly = True
            '            'gv1.CurrentRow.Cells(colfat_Rate).Value = 0
            '            'gv1.CurrentRow.Cells(colfat_Amt).Value = 0
            '            'gv1.CurrentRow.Cells(colsnf_Rate).Value = 0
            '            'gv1.CurrentRow.Cells(colsnf_Amt).Value = 0
            '        End If
            '    End If
            'End If
            'If clsCommon.CompairString(cboTransType.SelectedValue, "IN") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
            '    If e.Column Is gv1.Columns(colPrice_Type) Then
            '        If clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
            '            'gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
            '            'gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

            '            gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = False
            '            gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
            '        ElseIf clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
            '            'gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
            '            'gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

            '            gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
            '            gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = False
            '        End If
            '    End If
            'Else
            '    gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
            '    gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
            'End If
            ''richa 10 sep
            'If ChkMilkType.Checked = True Then
            '    If AllowpurchaseAccounting = False Then
            '        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colProductyType).Value), "Milk") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
            '            gv1.CurrentRow.Cells(colfat_Amt).ReadOnly = False
            '            gv1.CurrentRow.Cells(colsnf_Amt).ReadOnly = False
            '        Else
            '            gv1.CurrentRow.Cells(colfat_Amt).ReadOnly = True
            '            gv1.CurrentRow.Cells(colsnf_Amt).ReadOnly = True
            '        End If
            '    Else
            '        gv1.CurrentRow.Cells(colfat_Amt).ReadOnly = True
            '        gv1.CurrentRow.Cells(colsnf_Amt).ReadOnly = True
            '    End If

            'End If

        Catch ex As Exception
            '        common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
        'e.Cancel = True
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        ''richa agarwal 10/10/2014
        If ChkMilkType.Checked Then
            txtLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + FndMainLocation.Value + "'", txtLocation.Value, isButtonClicked)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
            Else
                lblLocation.Text = ""
            End If
            ''==============================
        Else
            Dim qry As String = ""
            Dim whrclas As String = ""
            qry = "select Location_Code ,Location_Desc from TSPL_LOCATION_MASTER "
            whrclas = "(Location_Type='Physical'  and GIT_Type<>'Y' and isnull(vendor_code,'')=''"
            If chklocation.Checked Then
                whrclas = "(Location_Type='Physical' and GIT_Type<>'Y' and isnull(vendor_code,'')<>''"
            End If

            If clsCommon.CompairString(MDI.IsLoc_Third_Party, "NO") = CompairStringResult.Equal Then
                whrclas = "(Location_Type='Physical'  and GIT_Type<>'Y'"
            End If

            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrclas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            whrclas += ") OR CSA_Type='Y'"
            txtLocation.Value = clsCommon.ShowSelectForm("AdjStoreLocation", qry, "Location_Code", whrclas, txtLocation.Value, "", isButtonClicked)
            lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "' ")

        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        isInsideLoadData = True
        If settTankerDispatchAvgFATSNFPer AndAlso clsCommon.CompairString(cboAdjustmentType.SelectedValue, "FLG") = CompairStringResult.Equal Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    Dim strLoc As String = txtLocation.Value
                    If clsCommon.myLen(strLoc) <= 0 Then
                        strLoc = FndMainLocation.Value
                    End If

                    Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, "", "MI", clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), strLoc, 1, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), 1, 1, txtDate.Value, txtDate.Value, False, Nothing, txtAdjustmentNo.Value)
                    If objMCT IsNot Nothing Then
                        gv1.Rows(ii).Cells(colfat_Rate).Value = objMCT.FAT_Cost
                        gv1.Rows(ii).Cells(colsnf_Rate).Value = objMCT.SNF_Cost
                        'gv1.Rows(ii).Cells(colFATPers).Value = objMCT.FAT_Per
                        'gv1.Rows(ii).Cells(colSNFPers).Value = objMCT.SNF_Per
                    End If
                    gv1.CurrentRow = gv1.Rows(ii)
                    CalFATKG()
                    CalSNFKG()
                End If
            Next
        End If
        UpdateAllTotals()
        isInsideLoadData = False


        If ChkMilkType.Checked = True Then
            Dim qry As String = clsDBFuncationality.getSingleValue("select Location_Category  from tspl_location_master where Location_Code = '" + FndMainLocation.Value + "'")
            If clsCommon.CompairString(qry, "MCC") <> CompairStringResult.Equal Then
                If clsCommon.myLen(txtLocation.Value) <= 0 Then
                    txtLocation.Focus()
                    Throw New Exception("Please select Location")
                End If
            End If
        Else
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select Location")
            End If
        End If
        ''====Sanjeet(20/02/2018)=Check while saving Main Location and Sub Location both are not as Sub Loacaion Type and not same ==============
        Dim chkMianLoc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) AS chkMainLoc from TSPL_LOCATION_MASTER where  Location_Code='" + FndMainLocation.Value + "' AND  isnull(is_sub_location,'N')='Y'"))
        Dim chkSubLoc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(1) AS chkMainLoc from TSPL_LOCATION_MASTER where  Location_Code='" + txtLocation.Value + "' AND  isnull(is_sub_location,'N')='Y'"))
        If chkMianLoc > 0 AndAlso chkSubLoc > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(FndMainLocation.Value)) = CompairStringResult.Equal Then
                txtLocation.Focus()
                Throw New Exception("Main Location and Sub Location can not be same")
            End If
        End If
        ''========================------
        'If clsCommon.myLen(txtLocation.Value) <= 0 Then
        '    txtLocation.Focus()
        '    Throw New Exception("Please select Location")
        'End If


        If clsCommon.CompairString(cboAdjustmentType.SelectedValue, "") = CompairStringResult.Equal Then
            cboAdjustmentType.Focus()
            cboAdjustmentType.Select()
            Throw New Exception("Please select transaction type.")
        End If

        If clsCommon.CompairString(cboAdjustmentType.SelectedValue, "AAD") = CompairStringResult.Equal Then
            cboAdjustmentType.Focus()
            cboAdjustmentType.Select()
            Throw New Exception("'Auto Adjustment' type is not allowed for manual entry.")
        End If

        If clsCommon.CompairString(cboAdjustmentType.SelectedValue, "OTH") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtSpecification.Text) <= 0 Then
            txtSpecification.Focus()
            txtSpecification.Select()
            Throw New Exception("Fill specification for transaction type 'Other'.")
        End If
        Dim arrItemCode As List(Of String) = New List(Of String)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim dblcost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)
            Dim adjustmenttype As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colAdjustmentType).Value)
            Dim Product_Type As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colProductyType).Tag)
            'Dim strQCStatus As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colQCStatus).Value)

            If clsCommon.CompairString(adjustmenttype, "FAT/SNF") = CompairStringResult.Equal AndAlso Not ChkMilkType.Checked Then
                gv1.CurrentRow = gv1.Rows(ii)
                gv1.CurrentColumn = gv1.Columns(colAdjustmentType)
                Throw New Exception("FAT/SNF type is not allowed for other than milk type adjustment at row no. " + clsCommon.myCstr(ii + 1) + "")
            End If

            If clsCommon.myLen(strICode) > 0 Then
                If arrItemCode.Contains(strICode) Then
                    Throw New Exception("Duplicate Item " & strICode & " Found at Row No " & (ii + 1))
                Else
                    arrItemCode.Add(strICode)
                End If
                If clsCommon.myLen(strUOM) <= 0 Then
                    Throw New Exception("Please enter UOM of item " + strICode + " ar Row No " + clsCommon.myCstr(ii + 1))
                End If

                ''richa BHA/18/09/18-000556 pick cost from item master for all type of items
                If settPickCostFromItemMaster Then
                    ''richa 16 Apr,2019 BHA/24/04/19-000866
                    If clsCommon.CompairString(gv1.Rows(ii).Cells(colAdjustmentType).Value, "Quantity") <> CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dblcost) <= 0 AndAlso clsCommon.CompairString(gv1.Rows(ii).Cells(colProductyType).Value, "Milk") <> CompairStringResult.Equal Then
                            Throw New Exception("Please provide Item cost for Item " + strICode + " and UOM " + strUOM + " in Item Master. ")
                        End If
                    End If
                End If
                ''--------------

                ''=========Added by parteek 09/01/2017
                Dim checked As String = ""
                checked = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FatSnfWhenMilktypeSelect, clsFixedParameterCode.FatSnfWhenMilktypeSelect, Nothing))
                ''Added by balwinder becuase in flg adjustment should be make with Zero FAT/SNF Percenentage.
                If Not clsCommon.CompairString("FLG", clsCommon.myCstr(cboAdjustmentType.SelectedValue)) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(checked, "1") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(gv1.Rows(ii).Cells(colProductyType).Value, ChkMilkType.Checked) = CompairStringResult.Equal Then
                            If dblQty > 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colFATPers).Value) <= 0 Then
                                Throw New Exception("Please enter FAT% of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                            End If
                            If dblQty > 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colSNFPers).Value) <= 0 Then
                                Throw New Exception("Please enter SNF% of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                            End If

                            ''richa 10 Sep BHA/07/08/18-000394
                            If clsCommon.CompairString(gv1.Rows(ii).Cells(colAdjustmentType).Value, "Quantity") <> CompairStringResult.Equal Then
                                If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colfat_Rate).Value) <= 0 Then
                                    Throw New Exception("Please enter FAT Rate of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                                End If
                                If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colsnf_Rate).Value) <= 0 Then
                                    Throw New Exception("Please enter SNF Rate of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                                End If
                            End If
                        End If
                    Else
                        If clsCommon.CompairString(gv1.Rows(ii).Cells(colProductyType).Value, "Milk") = CompairStringResult.Equal Then
                            If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPers).Value) <= 0 Then
                                Throw New Exception("Please enter FAT% of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                            End If
                            If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPers).Value) <= 0 Then
                                Throw New Exception("Please enter SNF% of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                            End If
                            ''richa 10 Sep BHA/07/08/18-000394
                            If clsCommon.CompairString(gv1.Rows(ii).Cells(colAdjustmentType).Value, "Quantity") <> CompairStringResult.Equal Then
                                If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colfat_Rate).Value) <= 0 Then
                                    Throw New Exception("Please enter FAT Rate of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                                End If
                                If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colsnf_Rate).Value) <= 0 Then
                                    Throw New Exception("Please enter SNF Rate of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                                End If
                            End If

                        End If
                    End If
                End If
                '=====================BM00000005498============================================================
                If (ChkMilkType.Checked = False AndAlso chkJobWork.Checked = False AndAlso clsCommon.CompairString(cboAdjustmentType.SelectedValue, "ADJ") <> CompairStringResult.Equal) Then

                    If (clsCommon.CompairString(adjustmenttype, "Both") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Quantity") = CompairStringResult.Equal) AndAlso dblQty <= 0 Then
                        Throw New Exception("Fill quanity at row no " + clsCommon.myCstr(ii + 1) + "")
                    End If

                End If

                ''richa agarwal BHA/09/05/18-000021
                Dim strItemCode_Can_Or_Crate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select CASE WHEN CAN=1 THEN 'CAN' WHEN CRATE=1 THEN 'CRATE' END from tspl_item_mASTER WHERE ITEM_CODE='" & strICode & "' AND (ISNULL(CAN ,0)=1 OR ISNULL(CRATE,0)=1)"))
                If clsCommon.myLen(strItemCode_Can_Or_Crate) > 0 Then
                    If (clsCommon.CompairString(adjustmenttype, "Both") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Cost") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemCost).Value) <= 0 Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colItemCost)
                        Throw New Exception("Item Cost should not be 0 at row no " + clsCommon.myCstr(ii + 1) + ", Please fill rate for " & strItemCode_Can_Or_Crate & " in utility.")
                    End If
                End If
                ''------------------

                If (clsCommon.CompairString(adjustmenttype, "Both") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Cost") = CompairStringResult.Equal) AndAlso dblcost <= 0 AndAlso clsCommon.CompairString(cboAdjustmentType.SelectedValue, "FLG") <> CompairStringResult.Equal Then
                    Throw New Exception("Fill cost at row no " + clsCommon.myCstr(ii + 1) + "")
                End If

                ''================Added by Monika======================================
                If (clsCommon.CompairString(adjustmenttype, "Both") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Cost") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Quantity") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPers).Value) > 100 Then
                    gv1.CurrentRow = gv1.Rows(ii)
                    gv1.CurrentColumn = gv1.Columns(colFATPers)
                    Throw New Exception("FAT% should not exceed 100 at row no " + clsCommon.myCstr(ii + 1) + "")
                End If
                If (clsCommon.CompairString(adjustmenttype, "Both") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Cost") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Quantity") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPers).Value) > 100 Then
                    gv1.CurrentRow = gv1.Rows(ii)
                    gv1.CurrentColumn = gv1.Columns(colSNFPers)
                    Throw New Exception("SNF% should not exceed 100 at row no " + clsCommon.myCstr(ii + 1) + "")
                End If
                If (clsCommon.CompairString(adjustmenttype, "FAT/SNF") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFKG).Value) <= 0 Then
                    gv1.CurrentRow = gv1.Rows(ii)
                    gv1.CurrentColumn = gv1.Columns(colSNFKG)
                    Throw New Exception("Please fill SNF KG at row no " + clsCommon.myCstr(ii + 1) + "")
                End If
                If (clsCommon.CompairString(adjustmenttype, "FAT/SNF") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATKG).Value) <= 0 Then
                    gv1.CurrentRow = gv1.Rows(ii)
                    gv1.CurrentColumn = gv1.Columns(colFATKG)
                    Throw New Exception("Please fill FAT KG at row no " + clsCommon.myCstr(ii + 1) + "")
                End If
                ''====================end here====================================

                If ChkMilkType.Checked And cboTransType.SelectedValue = "In" Then
                    ''richa ERO/11/01/19-000466
                    'If clsCommon.CompairString(gv1.Rows(ii).Cells(colPrice_Type).Value, "None") = CompairStringResult.Equal Then
                    '    Throw New Exception("Select any Price Type at row no " + clsCommon.myCstr(ii + 1) + "")
                    'Else

                    ''----------
                    If clsCommon.CompairString(gv1.Rows(ii).Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colMCC_Price_Code).Value) <= 0 Then
                            Throw New Exception("Select any MCC Price at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colfat_Rate).Value) <= 0 Then
                            Throw New Exception("Fat rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colsnf_Rate).Value) <= 0 Then
                            Throw New Exception("SNF rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    ElseIf clsCommon.CompairString(gv1.Rows(ii).Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colBulk_Price_Code).Value) <= 0 Then
                            Throw New Exception("Select any Bulk Price at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colfat_Rate).Value) <= 0 Then
                            Throw New Exception("Fat rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colsnf_Rate).Value) <= 0 Then
                            Throw New Exception("SNF rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If


                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ConsiderSiloCapicityForStockIn, clsFixedParameterCode.ConsiderSiloCapicityForStockIn, Nothing)), "1") = CompairStringResult.Equal Then
                        Dim balqtyofvl As Double = clsCommon.myCdbl(ClsLoadingTanker.getBalance(clsCommon.myCstr(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)), FndMainLocation.Value, txtLocation.Value, txtAdjustmentNo.Value, txtDate.Value, Nothing, "LTR"))
                        Dim itemQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                        Dim DblFinalQty As Double = balqtyofvl + itemQty
                        Dim SiloCapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Silo_Capacity,0) from TSPL_LOCATION_MASTER where location_code='" & txtLocation.Value & "'"))
                        If DblFinalQty > SiloCapacity Then
                            Throw New Exception("Silo Qty should be less than or equal to " & SiloCapacity & " at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If
                End If
                '======================================================================

                If clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "Out") = CompairStringResult.Equal Then
                    ''For RM Other balance Qty check And works only for one unit.
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double
                    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                        Dim Loc_code As String = ""
                        Dim Main_Loc_code As String = ""
                        If clsCommon.myLen(txtLocation.Value) <= 0 Then
                            Loc_code = FndMainLocation.Value
                            Main_Loc_code = ""
                        Else
                            Loc_code = txtLocation.Value
                            Main_Loc_code = FndMainLocation.Value
                        End If

                        ''richa agarwal 28/02/2016 apply tolerance limit
                        If (clsCommon.CompairString(adjustmenttype, "Cost") <> CompairStringResult.Equal) Then
                            dblBalQty = clsInventoryMovementNew.getBalance(strICode, Main_Loc_code, Loc_code, txtAdjustmentNo.Value, txtDate.Value, Nothing, strUOM)
                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                                If dblBalQty > 0 Then
                                    dblBalQty = ClsLoadingTanker.GetTolerane(dblBalQty, dblQty)
                                End If
                            End If
                        End If

                        ''-------------------------
                    Else
                        If (clsCommon.CompairString(adjustmenttype, "Cost") <> CompairStringResult.Equal) Then
                            dblBalQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtLocation.Value, txtAdjustmentNo.Value, txtDate.Value, Nothing, strUOM)
                        End If

                    End If

                    Dim dblEnteredQty As Double = dblQty
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                        Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                            dblEnteredQty += dblQtyInner
                        End If
                    Next
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    If (clsCommon.CompairString(adjustmenttype, "Cost") <> CompairStringResult.Equal) Then
                        If dblEnteredQty > dblBalQty Then
                            If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
                                Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                            End If
                        End If
                    End If

                End If
                If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                    Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "In") = CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsPickAutoSrNo).Value) Then
                            Dim arrOut As List(Of clsSerializeInvenotry) = New List(Of clsSerializeInvenotry)
                            If arrSerailNo Is Nothing OrElse arrSerailNo.Count <= 0 Then
                                For kk As Integer = 1 To dblQty
                                    Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                    obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                    arrOut.Add(obj)
                                Next
                            Else
                                For kk As Integer = 0 To arrSerailNo.Count - 1
                                    If kk > dblQty - 1 Then
                                        Exit For
                                    Else
                                        Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                        If clsCommon.myLen(arrSerailNo(kk).Auto_Sr_No) > 0 Then
                                            obj.Auto_Sr_No = arrSerailNo(kk).Auto_Sr_No
                                        Else
                                            obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                        End If
                                        arrOut.Add(obj)
                                    End If
                                Next
                                If arrOut.Count < dblQty Then
                                    For kk As Integer = arrOut.Count + 1 To dblQty
                                        Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                        obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                        arrOut.Add(obj)
                                    Next
                                End If
                            End If
                            gv1.Rows(ii).Tag = arrOut
                        End If
                    End If
                    arrSerailNo = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                        Throw New Exception("Please provide serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    End If
                End If

                If clsCommon.myCBool(gv1.Rows(ii).Cells(colisMRPMandatory).Value) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter MRP for " + strICode + ". At Line No" + clsCommon.myCstr(ii + 1))
                    Return False
                End If
            End If
            'Batch Qty Check
            If dblQty > 0 AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(colIsBatchItem).Value) Then
                If ChkMilkType.Checked Then
                    Dim arrBatchNoNew As List(Of clsBatchInventoryNew) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventoryNew))
                    If arrBatchNoNew Is Nothing Then
                        Throw New Exception("Please provide Batch no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    Else
                        Dim tQty As Decimal = 0
                        For Each objBatchNew As clsBatchInventoryNew In arrBatchNoNew
                            tQty += objBatchNew.Qty
                        Next
                        If tQty <> dblQty Then
                            Throw New Exception("Item : " + strICode + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                Else
                    Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                    If arrBatchNo Is Nothing Then
                        Throw New Exception("Please provide Batch no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    Else
                        Dim tQty As Decimal = 0
                        For Each objBatch As clsBatchInventory In arrBatchNo
                            tQty += objBatch.Qty
                        Next
                        If tQty <> dblQty Then
                            Throw New Exception("Item : " + strICode + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If
            End If
            'Batch Qty Check

        Next

        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            '' Anubhooti 09-Sep-2014 BM00000003735
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Adjustment", txtDate.Value) = False Then
                Exit Function
            End If
            If (AllowToSave()) Then
                Dim obj As New ClsAdjustmentsStoreEntry()
                obj.ProductionStoreEntryNo = txtAdjustmentNo.Value
                obj.ProductionStoreEntry_Date = txtDate.Value
                'obj.Posting_Date
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                'obj.Posted()

                obj.Unit_Code = "ALL"
                ''obj.ItemType = "E" Fill at Detail level

                obj.Loc_Code = txtLocation.Value
                obj.Loc_Desc = lblLocation.Text
                obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                'obj.Production_Entry = txtProductionEntry.Value
                obj.Against_Production_Entry = lblProductionEntry.Text
                obj.Against_Production_Entry_QC = txtProductionEntry.Value
                obj.chklocation = "N"
                If chklocation.Checked Then
                    obj.chklocation = "Y"
                End If

                If ChkMilkType.Checked Then
                    obj.IsMilkType = 1
                Else
                    obj.IsMilkType = 0
                End If
                obj.Is_JobWork = IIf(chkJobWork.Checked = True, 1, 0)
                obj.MainLocationCode = FndMainLocation.Value
                obj.MainLocationDesc = LblMainLocation.Text

                obj.Adjustment_Type = cboAdjustmentType.SelectedValue
                obj.Adjustment_Specification = txtSpecification.Text

                obj.Arr = New List(Of ClsAdjustmentsStoreEntryDetails)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
                        Dim objTr As New ClsAdjustmentsStoreEntryDetails()
                        'objTr.Adjustment_No=
                        objTr.ProductionStoreEntry_Line_No = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(colLineNo).Value))
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Description = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                        objTr.Adjustment_Type = clsCommon.myCstr(grow.Cells(colAdjustmentType).Value).Substring(0, 1) + IIf(clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal, "I", "D")
                        'objTr.Location_Code=Pick in SaveData from header
                        objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        ''richa 16 Apr,2019 unit cost should be zero in case of quantity type BHA/24/04/19-000866
                        If clsCommon.CompairString(grow.Cells(colAdjustmentType).Value, "Quantity") = CompairStringResult.Equal Then
                            objTr.Unit_Cost = 0
                        Else
                            objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                        End If

                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colCost).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        'objTr.Account_Code= Pick in SaveData
                        'objTr.Account_Description=Pick in SaveData
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.Comments = clsCommon.myCstr(grow.Cells(colComment).Value)
                        objTr.mrp = clsCommon.myCdbl(grow.Cells(colMRP).Value)

                        objTr.fat_pers = clsCommon.myCdbl(grow.Cells(colFATPers).Value)
                        objTr.fat_kg = clsCommon.myCdbl(grow.Cells(colFATKG).Value)
                        objTr.snf_kg = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        objTr.snf_pers = clsCommon.myCdbl(grow.Cells(colSNFPers).Value)

                        'objTr.MFG_Date =

                        'objTr.Expiry_Date =
                        'objTr.Breakage =
                        'objTr.Breakage_Cost =
                        objTr.ItemType = clsItemMaster.GetStoreAdjustmentItemType(objTr.Item_Code)
                        If isFirstTime Then
                            obj.ItemType = objTr.ItemType
                            isFirstTime = False
                        End If
                        objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                        objTr.Itemstatus = clsCommon.myCstr(grow.Cells(colICodeStatus).Value)

                        If clsCommon.myLen(objTr.Itemstatus) <= 0 Then
                            objTr.Itemstatus = "New"
                        End If

                        '' Ticket No : BM00000007708 : aded by Panch Raj
                        objTr.Price_Type = clsCommon.myCstr(grow.Cells(colPrice_Type).Value)
                        objTr.MCC_Price_Code = clsCommon.myCstr(grow.Cells(colMCC_Price_Code).Value)
                        objTr.Bulk_Price_Code = clsCommon.myCstr(grow.Cells(colBulk_Price_Code).Value)

                        objTr.fat_Rate = clsCommon.myCdbl(grow.Cells(colfat_Rate).Value)
                        objTr.fat_Amt = clsCommon.myCdbl(grow.Cells(colfat_Amt).Value)
                        objTr.snf_Rate = clsCommon.myCdbl(grow.Cells(colsnf_Rate).Value)
                        objTr.snf_Amt = clsCommon.myCdbl(grow.Cells(colsnf_Amt).Value)
                        objTr.Bin_No = clsCommon.myCstr(grow.Cells(colBinNo).Value)
                        'objTr.QC_Status = clsCommon.myCstr(grow.Cells(colQCStatus).Value)
                        If ChkMilkType.Checked Then
                            objTr.arrBatchItemNew = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventoryNew))
                        Else
                            objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
                        End If
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)

                '=============preet Gupta Ticket no.[BM00000005981]========
                If Not isFlag Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    LoadData(obj.ProductionStoreEntryNo, NavigatorType.Current)
                    'Else
                    '    clsCommon.MyMessageBoxShow("Data posted successfully")
                End If
                'End If

                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            txtProductionEntry.Enabled = False

            Dim obj As New ClsAdjustmentsStoreEntry()
            obj = obj.GetData(strCode, AdjustmentEnum.strCostTransaction, NavTyep, Nothing, False, True)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ProductionStoreEntryNo) > 0) Then
                If clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If


                txtAdjustmentNo.Value = obj.ProductionStoreEntryNo
                txtDate.Value = obj.ProductionStoreEntry_Date
                'obj.Posting_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                'obj.Posted()

                'obj.Unit_Code = "ALL"
                'obj.ItemType = "E"

                If obj.chklocation = "Y" Then
                    chklocation.Checked = True
                Else
                    chklocation.Checked = False
                End If

                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = obj.Loc_Desc
                cboTransType.SelectedValue = obj.Trans_Type

                cboAdjustmentType.SelectedValue = obj.Adjustment_Type
                cboAdjustmentType.Enabled = False
                txtSpecification.Text = obj.Adjustment_Specification
                If clsCommon.CompairString(obj.Adjustment_Type, "OTH") = CompairStringResult.Equal Then
                    txtSpecification.Enabled = True
                Else
                    txtSpecification.Enabled = False
                End If

                If obj.IsMilkType = 1 Then
                    ChkMilkType.Checked = True
                Else
                    ChkMilkType.Checked = False
                End If
                ChkMilkType.Enabled = False
                FndMainLocation.Value = obj.MainLocationCode
                LblMainLocation.Text = obj.MainLocationDesc
                txtProductionEntry.Value = obj.Against_Production_Entry_QC
                lblProductionEntry.Text = obj.Against_Production_Entry
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsAdjustmentsStoreEntryDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objTr.ProductionStoreEntry_Line_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        If obj.IsMilkType = 1 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItemNew
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        Dim AdjTypeFirstChar As String = objTr.Adjustment_Type.Substring(0, 1)
                        If clsCommon.CompairString(AdjTypeFirstChar, "Q") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentQty
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "C") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentCost
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "B") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "F") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentFAT_SNF
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Item_Quantity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = objTr.Unit_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComment).Value = objTr.Comments
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.mrp
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = clsItemMaster.GetItemTypeFromMaster(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Tag = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Value = ProductType(gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Tag)

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).Value = objTr.fat_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).Value = objTr.fat_pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).Value = objTr.snf_pers

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Value), "Milk") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).ReadOnly = False
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).ReadOnly = True
                        End If

                        If clsCommon.CompairString(objTr.Itemstatus, "OLD") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeStatus).Value = "OLD"
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeStatus).Value = "NEW"
                        End If

                        '' aded by Panch Raj
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).Value = objTr.Price_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).Value = objTr.MCC_Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).Value = objTr.Bulk_Price_Code

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfat_Rate).Value = objTr.fat_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfat_Amt).Value = objTr.fat_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colsnf_Rate).Value = objTr.snf_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colsnf_Amt).Value = objTr.snf_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colQCStatus).Value = objTr.QC_Status
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objTr.Item_Code)
                        ' objTr.QC_Status
                        ''richa agarwal BHA/09/05/18-000021
                        Dim strItemCode_Can_Or_Crate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select CASE WHEN CAN=1 THEN 'CAN' WHEN CRATE=1 THEN 'CRATE' END from tspl_item_mASTER WHERE ITEM_CODE='" & objTr.Item_Code & "' AND (ISNULL(CAN ,0)=1 OR ISNULL(CRATE,0)=1)"))
                        If clsCommon.CompairString(strItemCode_Can_Or_Crate, "CAN") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).ReadOnly = True
                        ElseIf clsCommon.CompairString(strItemCode_Can_Or_Crate, "CRATE") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).ReadOnly = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).ReadOnly = False
                        End If
                        '------
                    Next

                    'If Not clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                    '    gv1.Rows.AddNew()
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                    'End If
                End If
                If obj.Is_JobWork = 1 Then
                    chkJobWork.Visible = True
                End If

                chkJobWork.Checked = IIf(obj.Is_JobWork = 1, True, False)

                chkJobWork.Enabled = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub OpenBatchItem()
        Dim blnBatchqty As Boolean = False
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) Then
            If clsCommon.CompairString("In", clsCommon.myCstr(cboTransType.SelectedValue)) = CompairStringResult.Equal Then
                Dim frm As frmBatchItemIn = New frmBatchItemIn()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                End If
            Else
                If RunBatchFifowise = 0 Then
                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.strLocationCode = txtLocation.Value
                    frm.strCurrDocNo = txtAdjustmentNo.Value
                    frm.strCurrDocType = "IC-AD"
                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                    frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                    frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                    End If
                Else
                    ' fifo start preeti gupta against ticket no[BHA/23/08/18-000477]

                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                            If clsCommon.myCBool(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Is_Batch_Item  from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'", Nothing)) Then
                                Dim strBatchunion As String = ""
                                If RunBatchFifowise = 1 Then
                                    If ii > 0 Then
                                        Dim strICodeOuter As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                                        For jj As Integer = 0 To ii - 1
                                            Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                                            If clsCommon.CompairString(strICodeOuter, strICodeInner) = CompairStringResult.Equal Then
                                                Dim arr As List(Of clsBatchInventory) = Nothing
                                                arr = TryCast(gv1.Rows(jj).Cells(colICode).Tag, List(Of clsBatchInventory))
                                                For Each obj As clsBatchInventory In arr
                                                    strBatchunion += " union all select '" & clsCommon.myCstr(obj.Batch_No) & "' as Batch_No, " &
                                                        "'" & clsCommon.myCstr(obj.Manual_BatchNo) & "' as Manual_BatchNo,'O' as In_Out_Type, " &
                                                        "'" & clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value) & "' as OrgUOM," & obj.Qty & " as OrgQty,0 as OrgMRP, " &
                                                        "'" & clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy") & "' as Expiry_Date, " &
                                                        "'" & clsCommon.GetPrintDate(obj.Manufacture_Date, "dd/MMM/yyyy") & "' as Manufacture_Date, " &
                                                        "" & obj.Qty & " as Qty, 0 as MRP "
                                                Next

                                            End If
                                        Next
                                    End If
                                    gv1.CurrentRow = gv1.Rows(ii)

                                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                                    frm.strLocationCode = txtLocation.Value
                                    frm.strCurrDocNo = txtAdjustmentNo.Value
                                    frm.strCurrDocType = "IC-AD"
                                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                    frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))

                                    If frm.OpenSerialList(0, "", strBatchunion) Then
                                        gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                                        blnBatchqty = True
                                    Else
                                        Dim batchQty As Double = 0
                                        For Each obj As clsBatchInventory In frm.arr
                                            batchQty += obj.Qty
                                        Next
                                        clsCommon.MyMessageBoxShow("Please increase stock Item Code - " & frm.strItemCode & " , Entered Qty - " & clsCommon.myCstr(frm.dblqty) & " Batch Qty - " & clsCommon.myCstr(batchQty), Me.Text)
                                        blnBatchqty = False
                                        Exit Sub
                                    End If

                                End If
                            End If
                        End If
                    Next

                End If

            End If
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()

        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Doc_Code As String = ""
            isFlag = True
            If clsCommon.myLen(txtAdjustmentNo.Value) > 0 Then
                Doc_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS adjustment_no from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD  where TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo='" + txtAdjustmentNo.Value + "'"))
                If Doc_Code > 0 Then
                    If (myMessages.postConfirm()) Then

                        'Sanjay,Ticket No-UDL/04/05/20-001018
                        If clsCommon.CompairString("Out", clsCommon.myCstr(cboTransType.SelectedValue)) = CompairStringResult.Equal Then
                            If clsCommon.myLen(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AskForPwdForOutAdjustmentOnPost, clsFixedParameterCode.AskForPwdForOutAdjustmentOnPost, Nothing))) > 0 Then
                                Dim frm As New FrmPWD(Nothing)
                                frm.strType = clsFixedParameterType.AskForPwdForOutAdjustmentOnPost
                                frm.strCode = clsFixedParameterCode.AskForPwdForOutAdjustmentOnPost
                                frm.ShowDialog()
                                If frm.isPasswordCorrect Then
                                Else
                                    Exit Sub
                                End If
                            End If
                        End If
                        If SaveData() = False Then
                            Exit Sub
                        End If
                        If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Adjustment", txtDate.Value) = False Then
                            Exit Sub
                        End If
                        If (ClsAdjustmentsStoreEntry.PostData(txtAdjustmentNo.Value, AdjustmentEnum.strCostTransaction)) Then

                            clsCommon.MyMessageBoxShow("Data posted successfully.")
                            LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering Document code")
                End If

            Else
                Throw New Exception("Document code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
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
                If (ClsAdjustmentsStoreEntry.DeleteData(txtAdjustmentNo.Value, AdjustmentEnum.strCostTransaction)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    'AddNew()
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtAdjustmentNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAdjustmentNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where ProductionStoreEntryNo='" + txtAdjustmentNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtAdjustmentNo.MyReadOnly = False
            Else
                txtAdjustmentNo.MyReadOnly = True
            End If
            LoadData(txtAdjustmentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAdjustmentNo._MYValidating
        'Dim qry As String = "SELECT ProductionStoreEntryNo AS [ProductionStoreEntryNo],CONVERT(varchar(10), TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date,103)+' '+ CONVERT(varchar(5), TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date,114) as [Date], Document_No,case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location],coalesce(Against_Item_Stock_Conv_Doc,against_Item_Stock_Conversion) as [Against Item Stock Conversion],Against_AP_Invoice_No as [Against AP Invoice No]," &
        '    " (case when Adjustment_Type='ADJ' then 'Adjustment' when Adjustment_Type='FLG' then 'Flushing' when Adjustment_Type='OPG' then 'Opening' when Adjustment_Type='CLG' then 'Closing' when Adjustment_Type='AAD' then 'Auto Adjustment' when Adjustment_Type='PRE' then 'Production Entry' else 'Other' end) as [Adjustment Type] " &
        '    " FROM  TSPL_ADJUSTMENT_STORE_ENTRY_HEAD  "
        Dim qry As String = "SELECT ProductionStoreEntryNo AS [ProductionStoreEntryNo],CONVERT(varchar(10), TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date,103)+' '+ CONVERT(varchar(5), TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date,114) as [Date],case when Posted='Y' then 'Yes' else 'No' end as Posted" &
            " ,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC as [Against Production Entry QC],TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry as [Against Production Entry]" &
            " FROM  TSPL_ADJUSTMENT_STORE_ENTRY_HEAD  "
        Dim whrClas As String = " 1=1 and isnull(AdjustType,'') <> 'Consume' and Adjustment_Type = 'PRE' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND (Loc_Code in (" + objCommonVar.strCurrUserLocations + ") or  mainlocationcode in (" + objCommonVar.strCurrUserLocations + "))"
        End If
        'whrClas += " AND ItemType IN ('RM', 'OT')"


        txtAdjustmentNo.Value = clsCommon.ShowSelectForm("AdjStoreEnt1", qry, "ProductionStoreEntryNo", whrClas, txtAdjustmentNo.Value, "TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date desc", isButtonClicked)
        LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                OpenUOMList(True)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
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
            'ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.E Then
            '    Dim frm As New FrmPWD(Nothing)
            '    frm.strType = clsFixedParameterType.StoreADJExportImportAfterPost
            '    frm.strCode = clsFixedParameterCode.StoreADJExportImportAfterPost
            '    frm.ShowDialog()
            '    If frm.isPasswordCorrect Then
            '        'rbtnExportPosted.Visible = True
            '    End If
            'ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.I Then
            '    Dim frm As New FrmPWD(Nothing)
            '    frm.strType = clsFixedParameterType.StoreADJExportImportAfterPost
            '    frm.strCode = clsFixedParameterCode.StoreADJExportImportAfterPost
            '    frm.ShowDialog()
            '    If frm.isPasswordCorrect Then
            '        'rbtnImportPosted.Visible = True
            '    End If
            'ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.U Then
            '    Dim frm As New FrmPWD(Nothing)
            '    frm.strType = clsFixedParameterType.StoreADJExportImportAfterPost
            '    frm.strCode = clsFixedParameterCode.StoreADJExportImportAfterPost
            '    frm.ShowDialog()
            '    If frm.isPasswordCorrect Then
            '        ' cmdEditAndPost.Visible = True
            '    End If
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
        '    myMessages.blankValue("Purchase Order No not found to Print")
        'Else
        '    funPrint()
        'End If
    End Sub

    'Private Sub funPrint()
    '    Try
    '        Dim frmCRV As New frmCrystalReportViewer()
    '        If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
    '            Throw New Exception("Adjustment No not found to Print")
    '        End If

    '        Dim qry As String = "select TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_Type  from TSPL_ADJUSTMENT_DETAIL_QC left outer join TSPL_ADJUSTMENT_HEADER_QC   on TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No='" + txtAdjustmentNo.Value + "' and TSPL_ADJUSTMENT_HEADER_QC.ItemType='E' and TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_Line_No=1"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            qry = "select Head.Adjustment_No as [Adjustment No], Head.Adjustment_Date as [Adjustment Date],Head.Description as [Description], Head.Reference_Document AS [Reference Document], Head.Document_No as [Document No],detail.Item_Code as [Item Code], detail.Item_Description as [Item Description], Location.Location_Desc as [Location], CASE when detail.Adjustment_Type='BI' then 'Both Increase' else CASE when detail.Adjustment_Type='BD' then 'Both Decrease' else CASE when detail.Adjustment_Type='QI' then 'Quality Increase' else CASE when detail.Adjustment_Type='QD' then 'Quality Decrease' else CASE when detail.Adjustment_Type='CI' then 'Cost Increase' else CASE when detail.Adjustment_Type='CD' then 'Cost Decrease' end end end end end end  as [Adjustment Type],detail.Item_Quantity as [Quantity], detail.Item_Cost as [Cost Adjustment], detail.Breakage as [Breakage Quantity],detail.Breakage_Cost as [Breakage Cost], detail.mrp as [MRP], detail.Unit_Code as [UOM], detail.MFG_Date as [MFG Date],detail.Batch_No as [Batch No], detail.Expiry_Date  as [Exp. Date],Location.Location_Desc as [Location], TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, (Location.Add1+(case when len(Location.Add2)>0 then ', 'else '' end )+Location.Add2+(case when len(Location.Add3)>0 then ', 'else '' end )+Location.Add3+(case when len(Location.Add4)>0 then ', 'else '' end )+Location.Add4+(case when len(Location.City_Code )>0 then ', 'else '' end ) + '' +TSPL_TDS_STATE_MASTER.State_Name ) as [Add1] from TSPL_ADJUSTMENT_HEADER_QC as Head left outer join TSPL_ADJUSTMENT_DETAIL as detail on head.Adjustment_No = detail.Adjustment_No Left Outer JOIN TSPL_COMPANY_MASTER ON Head.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_LOCATION_MASTER as Location on detail.Location_Code=Location.Location_Code Left Outer Join TSPL_TDS_STATE_MASTER on Location .State=TSPL_TDS_STATE_MASTER.State_Code  where Head.Adjustment_No='" + txtAdjustmentNo.Value + "' order by detail.Adjustment_Line_No "
    '            dt = clsDBFuncationality.GetDataTable(qry)
    '            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustment", "Adjustment Detail")
    '        Else
    '            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Adjustment_Type")), "BD") = CompairStringResult.Equal Then
    '                qry = "select TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No,(TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date+' '+TSPL_ADJUSTMENT_HEADER_QC.created_time) as Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_CODE,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_CUSTOMER_MASTER.Lst_No,TSPL_ADJUSTMENT_DETAIL_QC.Item_Code,TSPL_ADJUSTMENT_DETAIL_QC.Item_Description,TSPL_ADJUSTMENT_DETAIL_QC.Item_Quantity,TSPL_ADJUSTMENT_DETAIL_QC.mrp,TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost,TSPL_ADJUSTMENT_HEADER.Vehicle_No " &
    '                " from TSPL_ADJUSTMENT_DETAIL_QC" &
    '                " left outer join TSPL_ADJUSTMENT_HEADER_QC on TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No=TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No" &
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ADJUSTMENT_HEADER_QC.Customer_CODE" &
    '                " where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No='" + txtAdjustmentNo.Value + "' ORDER by TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_Line_No"
    '                dt = clsDBFuncationality.GetDataTable(qry)
    '                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomIssue", "Adjustment Detail")

    '            Else
    '                ''For both Increase OR Receipt Challan
    '                Dim strReportName As String = "EMPTY RECEIPT CHALLAN"
    '                Dim strACaption As String = "From"
    '                Dim strIssueCaption As String = "Empty Receipt"
    '                Dim strDateCaption As String = "Receipt Date"
    '                qry = "select Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME,MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short, SUM(Amount ) as Amount, '" + strReportName + "' as ReportName,'" + strACaption + "' as ACaption,'" + strIssueCaption + "' as EmptyCaption,'" + strDateCaption + "' as DateCaption,max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date,max(Vehicle_No) as Vehicle_No ,MAX(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3,max(City_Name) as City_Name,max(State_Name) as State_Name from(" &
    '                "select TSPL_ADJUSTMENT_HEADER._QCAdjustment_No,TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date ,TSPL_ADJUSTMENT_HEADER_QC.Customer_NAME,TSPL_ADJUSTMENT_HEADER_QC.Description ,TSPL_ADJUSTMENT_DETAIL_QC.Item_Code,TSPL_ADJUSTMENT_DETAIL_QC.Item_Description,TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code,case when TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code='FC' then Item_Quantity end as FCS, case when TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code='FB' then Item_Quantity end as FBS, case when TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code='SH' then Item_Quantity end as FSH, case when TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code='EC' then Item_Quantity end as ECS,case when TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code='EB' then Item_Quantity end as EBS, 0 as Leak_Qty,TSPL_ADJUSTMENT_DETAIL_QC.Breakage,0 As Short_Qty, Case When TSPL_CUSTOMER_MASTER.Cust_Type_Code Not IN ('F','S') Then (ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost, 0)+ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Breakage_Cost, 0)) Else ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost, 0) End as Amount, TSPL_ADJUSTMENT_HEADER.EMP_NAME as SalesManName,TSPL_ADJUSTMENT_HEADER.Challan_No,TSPL_ADJUSTMENT_HEADER.Challan_date,TSPL_ADJUSTMENT_HEADER.Vehicle_No,TSPL_CUSTOMER_MASTER.Add1,TSPL_CUSTOMER_MASTER.Add2,TSPL_CUSTOMER_MASTER.Add3,TSPL_CITY_MASTER.City_Name,TSPL_TDS_STATE_MASTER.State_Name from TSPL_ADJUSTMENT_DETAIL_QC left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No= TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No "
    '                qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= TSPL_ADJUSTMENT_HEADER.Customer_CODE"
    '                qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
    '                qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
    '                qry += " where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No='" + txtAdjustmentNo.Value + "'  " &
    '                ")xxx group by Adjustment_No,Item_Code order by Item_Desc"
    '                dt = clsDBFuncationality.GetDataTable(qry)
    '                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomReceipt", "Adjustment Detail")
    '            End If
    '        End If
    '        frmCRV = Nothing
    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub OpeningwithSerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningwithSerial.Click
        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Location", "Adjustment_Date", "Adjustment_No", "Item Code", "unit code", "Quantity", "Cost Adjustment", "Third Party Location", "Serial No", "Tag No", "Type", "Transaction Type") Then
            Dim trans As SqlTransaction = Nothing
            Try
                Dim obj As New ClsAdjustmentsQCC()
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                Dim strAdcode As String = ""


                For Each grow As GridViewRowInfo In gv.Rows


                    Dim strIType As String = "RM"
                    Dim strLoc As String = grow.Cells(0).Value.ToString()
                    If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                        Throw New Exception("Check the value for Location")
                    End If
                    Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ")
                    'Dim strADate As String = grow.Cells(1).Value.ToString()
                    Dim strADate As String = clsCommon.GetPrintDate(grow.Cells(1).Value, "yyyy/MM/dd")
                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

                    Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code")
                    End If
                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = "BI"

                    '------------------------------------------------------------------------------------------------
                    Dim thirdparty As String = ""
                    thirdparty = clsCommon.myCstr(grow.Cells("Third Party Location").Value.ToString().ToUpper())

                    If Not clsCommon.CompairString(thirdparty, "N") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(thirdparty, "Y") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be N or Y In ColumnName [Third Party Location]")
                    End If
                    obj.chklocation = thirdparty
                    '--------------------------------------------------------------------------------------------------
                    '====================================Preeti=======================
                    Dim strTransactionType As String = ""
                    strTransactionType = clsCommon.myCstr(grow.Cells("Transaction Type").Value.ToString())
                    If clsCommon.myLen(strTransactionType) <= 0 Then
                        Throw New Exception("Enter Transaction Type")
                    End If
                    If Not clsCommon.CompairString(strTransactionType, "Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Flushing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Opening") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Closing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Auto Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Other") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be Adjustment/Flushing/Opening/Closing/Auto Adjustment/Other In ColumnName [Transaction Type]")
                    End If

                    If strTransactionType = "Adjustment" Then
                        obj.Adjustment_Type = "ADJ"
                    ElseIf strTransactionType = "Flushing" Then
                        obj.Adjustment_Type = "FLG"
                    ElseIf strTransactionType = "Opening" Then
                        obj.Adjustment_Type = "OPG"
                    ElseIf strTransactionType = "Closing" Then
                        obj.Adjustment_Type = "CLG"
                    ElseIf strTransactionType = "Auto Adjustment" Then
                        obj.Adjustment_Type = "AAD"
                    ElseIf strTransactionType = "Other" Then
                        obj.Adjustment_Type = "OTH"
                    End If                    '=====================================================================
                    '-------------------------------
                    Dim struom As String = grow.Cells("unit code").Value.ToString()
                    If clsCommon.myLen(struom) = 0 Then
                        struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Else
                        Dim intCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(unit_code) from tspl_unit_master where unit_code='" & struom & "'"))
                        If intCount = 0 Then
                            Throw New Exception("Unit code is not correct")
                        End If
                    End If
                    '---------------------------------------------------------------------------------------------------
                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Quantity").Value)
                    Dim CostAd As Decimal = clsCommon.myCdbl(grow.Cells("Cost Adjustment").Value)
                    Dim SerialNo As String = clsCommon.myCstr(grow.Cells("Serial No").Value)
                    If String.IsNullOrEmpty(SerialNo) Or SerialNo.Length > 50 Then
                        Throw New Exception("Check the value for Serial No Line No (" & line & ")")
                    End If
                    Dim TagNo As String = clsCommon.myCstr(grow.Cells("Tag No").Value)
                    Dim InoutType As String = clsCommon.myCdbl(grow.Cells("Type").Value)



                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE()
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE()
                    Dim rmk As String = ""
                    Dim commt As String = ""

                    'If line = 1 Then
                    '    strAdcode = clsERPFuncationality.GetNextCode(trans, strADate, clsDocType.StoreAdjustment, clsDocTransactionType.StoreAdjustmentAdjustment, strLoc)
                    '    connectSql.RunSpTransaction(trans, "sp_TSPL_ADJUSTMENT_HEADER_insert", New SqlParameter("@AdjustNum", strAdcode), New SqlParameter("@AdjustDate", strADate), New SqlParameter("@PostDate", strADate), New SqlParameter("@Reference", ""), New SqlParameter("@Description", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@CreatedBy", objCommonVar.CurrentUser), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", objCommonVar.CurrentUser), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompanyCode", objCommonVar.CurrentCompanyCode), New SqlParameter("@ReferenceDocument", ""), New SqlParameter("@DocumentNumber", ""), New SqlParameter("@Itemtype", strIType), New SqlParameter("@Unit_Code", "ALL"), New SqlParameter("@EMP_Code", ""), New SqlParameter("@EMP_NAME", ""), New SqlParameter("@Customer_CODE", ""), New SqlParameter("@Customer_NAME", ""), New SqlParameter("@Created_time", strStime), New SqlParameter("@Modified_time", strStime), New SqlParameter("@Vehicle_Code", ""), New SqlParameter("@Vehicle_No", ""), New SqlParameter("@Challan_No", ""), New SqlParameter("@Challan_date", strADate), New SqlParameter("@GateEntry_No", ""), New SqlParameter("@GateEntry_Date", strADate), New SqlParameter("@Loc_Code", strLoc), New SqlParameter("@Loc_Desc", strLocDesc), New SqlParameter("@Trans_type", cboTransType.Text))
                    '    Dim entrydatetime As String = clsCommon.GetPrintDate(strADate, "dd/MM/yyyy hh:mm tt")
                    '    Dim StrQuery As String = "update TSPL_ADJUSTMENT_HEADER  set entrydatetime=convert(datetime,'" + entrydatetime + "',103) where Adjustment_No ='" + strAdcode + "'"
                    '    clsDBFuncationality.ExecuteNonQuery(StrQuery, trans)

                    'End If

                    Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
                    'connectSql.RunSpTransaction(trans, "sp_TSPL_ADJUSTMENT_DETAIL_insert", New SqlParameter("@AdjustNum", strAdcode), New SqlParameter("@AdjustLineNum", line), New SqlParameter("@ItemNum", ItemCode), New SqlParameter("@ItemDesc", iteDesc), New SqlParameter("@AdjustType", AdjType), New SqlParameter("@LocCode", strLoc), New SqlParameter("@ItemQuantity", Iqty), New SqlParameter("@ItemCost", CostAd), New SqlParameter("@UnitCode", struom), New SqlParameter("@AccCode", account), New SqlParameter("@AccDesc", ""), New SqlParameter("@Remarks", rmk), New SqlParameter("@Comments", commt), New SqlParameter("@MRP", StrMRP), New SqlParameter("@MFG_Date", strADate), New SqlParameter("@Batch_No", Batch), New SqlParameter("@Expiry_Date", strADate), New SqlParameter("@Breakage", Bqty), New SqlParameter("@breakage_cost", Bcost), New SqlParameter("@ItemType", strIType), New SqlParameter("@BreakageType", Btype), New SqlParameter("@LeakageQty", Lqty))

                    'line = line + 1
                    'Dim strDescription As String = grow.Cells("Description").Value.ToString()
                    'If strDescription.Length > 300 Then
                    '    Throw New Exception("Length of Description can not be greater than 300")
                    'End If

                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    If line = 1 Then
                        ''started by priti

                        obj.Adjustment_No = strAdcode
                        obj.Adjustment_Date = strADate
                        'obj.Posting_Date
                        obj.Reference = ""
                        obj.Description = ""
                        'obj.Posted()

                        obj.Unit_Code = struom
                        obj.ItemType = strIType
                        obj.Loc_Code = strLoc
                        obj.Loc_Desc = strLocDesc
                        obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                        'obj.Description = strDescription


                        '' ended by priti
                    End If


                    Dim objTr As New ClsAdjustmentsQCCDetails()
                    objTr.arrSrItem = New List(Of clsSerializeInvenotry)()
                    objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = AdjType
                    'objTr.Location_Code=Pick in SaveData from header
                    objTr.Item_Quantity = Iqty
                    objTr.Item_Cost = CostAd
                    objTr.Unit_Code = struom
                    'objTr.Account_Code= Pick in SaveData
                    'objTr.Account_Description=Pick in SaveData
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP

                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty

                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate

                    objTr.ItemType = strIType


                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                    Dim objserial As New clsSerializeInvenotry
                    objserial.Auto_Sr_No = SerialNo
                    objserial.Document_Code = strAdcode
                    objserial.Document_Date = strADate
                    objserial.Item_Code = ItemCode
                    objserial.Document_Type = "IC-AD"
                    'objTr.Location_Code=Pick in SaveData from header
                    'objTr.Account_Code= Pick in SaveData
                    'objTr.Account_Description=Pick in SaveData

                    objserial.In_Out_Type = InoutType
                    objserial.Line_No = line

                    objserial.Location_Code = strLoc
                    objserial.Tag_No = TagNo
                    'objserial.Parent_Line_No = 1

                    If (clsCommon.myLen(objserial.Item_Code) > 0) Then
                        objTr.arrSrItem.Add(objserial)
                    End If


                    line = line + 1
                Next

                'trans.Commit()
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, True)
                RadMessageBox.Show("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception

                myMessages.myExceptions(ex)
                ''trans.Rollback()

            End Try

        End If
    End Sub

    Private Sub RmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmiExport.Click
        'Try
        '    Dim qryExport As String
        '    qryExport = " Select '' as [Location], '' as [Adjustment Date], '' as [Item Code], '' as [Quantity], '' as [Cost Adjustment],'N' as [Third Party Location]"
        '    transportSql.ExporttoExcel(qryExport, Me)
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        'End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        ' PrintData()
        Dim frmCrystalReportViewer As New frmCrystalReportViewer()
        Try
            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Production Store Entry No Not Available for Print .", Me.Text)
                Return
            End If
            'Dim qry As String = "  select TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.REMARKS,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Description as Production_Entry_Desc , TSPL_LOCATION_MASTER_From_loc.Location_Desc,TSPL_COMPANY_MASTER.Logo_Img,TSPL_LOCATION_MASTER_From_loc.Location_Desc as FromLoaction,TSPL_LOCATION_MASTER_From_loc.GSTNO as From_location_GSTIN,TSPL_STATE_MASTER_From_loc.GST_STATE_Code as From_Loc_GST_STATE_Code,TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, '' as Company_Address ,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo as Transfer_No,convert(datetime,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date,103) as  Date_And_Time,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntry_Line_No as SNO,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc as itemdesc,TSPL_ITEM_MASTER.HSN_Code, " &
            '                    " case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.UOM ELSE TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code END AS UOM, " &
            '                    " case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Quantity end as Qty, " &
            '                    " case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then convert (varchar, BI.Manufacture_Date,103) else '' end as Manufacture_Date, " &
            '                    " TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Cost as Rate, TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Description  as QC_Desc , " &
            '                    " case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.BATCH_NO ELSE '' END AS Item_BatchNo,TSPL_ITEM_MASTER.Is_Batch_Item  " &
            '                    "  ,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Adjustment_Type as Document_Type,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Loc_Code, " &
            '                    " TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted , TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posting_Date as Posted_Date,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Created_By,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Modify_By as Modified_By, case when TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted='Y' then tspl_user_master_Modified_By.User_Name else '' end as PostedBy,tspl_user_master_Created_By.User_Name as CreatedBy, TSPL_LOCATION_MASTER_From_loc.Add1 + case when len(TSPL_LOCATION_MASTER_From_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_From_loc. Add2 else ' '     end + Case when len( TSPL_LOCATION_MASTER_From_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_From_loc. Add3 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_From_loc.Add4 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_From_loc.City_Code else '' end + case when len    (TSPL_LOCATION_MASTER_From_loc.state) > 0 then ','+ TSPL_STATE_MASTER_From_loc.STATE_NAME else ''  end + Case when len     (TSPL_LOCATION_MASTER_From_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_From_loc.Pin_Code,103) else '' end as From_Location_Address  from TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL  " &
            '                    " left outer join TSPL_ADJUSTMENT_STORE_ENTRY_HEAD on TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo  " &
            '                    " left outer join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code  " &
            '                    " Left outer join tspl_user_master as tspl_user_master_Modified_By on tspl_user_master_Modified_By.User_Code =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.posted_By  " &
            '                    " left outer join tspl_user_master as tspl_user_master_Created_By on tspl_user_master_Created_By.User_Code =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Created_By  " &
            '                    " Left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_loc on TSPL_LOCATION_MASTER_From_loc.Location_Code =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.loc_Code  " &
            '                    " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_loc on  TSPL_STATE_MASTER_From_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State   " &
            '                    " Left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_From_location on TSPL_CITY_MASTER_From_location.City_Code = TSPL_LOCATION_MASTER_From_loc.City_Code                              Left Join TSPL_BATCH_ITEM AS BI ON TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo=BI.Document_Code And TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code=BI.Item_Code  " &
            '                    " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Comp_Code  " &
            '                    " where  TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Adjustment_Type = 'PRE' and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo= '" + txtAdjustmentNo.Value + "'  "

            '" Left Outer Join TSPL_ADJUSTMENT_STORE_ENTRY_HEAD on TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Adjustment_No =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Production_Entry " &

            Dim qry As String = "select isnull(StoreEntry.Qty,0) as StoreEntryQty,TSPL_ADJUSTMENT_DETAIL_QC.Qty as Qc_Qty,TSPL_ADJUSTMENT_DETAIL.Qty as Production_Qt " &
" , TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.REMARKS,TSPL_ADJUSTMENT_HEADER.Description As Production_Entry_Desc , TSPL_LOCATION_MASTER_From_loc.Location_Desc,TSPL_COMPANY_MASTER.Logo_Img,TSPL_LOCATION_MASTER_From_loc.Location_Desc As FromLoaction,TSPL_LOCATION_MASTER_From_loc.GSTNO As From_location_GSTIN,TSPL_STATE_MASTER_From_loc.GST_STATE_Code As From_Loc_GST_STATE_Code,TSPL_COMPANY_MASTER.Comp_Name As Comp_Name, '' as Company_Address ,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo as Transfer_No " &
      " ,convert(datetime,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntry_Date,103) as  Date_And_Time " &
      " ,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntry_Line_No as SNO,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc as itemdesc,TSPL_ITEM_MASTER.HSN_Code,  case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.UOM ELSE TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code END AS UOM " &
      " ,  case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Quantity end as Qty " &
      " ,  case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then convert (varchar, BI.Manufacture_Date,103) else '' end as Manufacture_Date " &
      " ,  TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Cost as Rate, TSPL_ADJUSTMENT_HEADER_QC.Description  as QC_Desc " &
      " , TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Description  as StoreEntry_Desc " &
      "  ,  case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.BATCH_NO ELSE '' END AS Item_BatchNo,TSPL_ITEM_MASTER.Is_Batch_Item    " &
        " ,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Adjustment_Type as Document_Type,TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Loc_Code,  TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted " &
      " , case when TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted='Y' then TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posting_Date else '' end as Posted_Date " &
      " , case when TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Posted='Y' then tspl_user_master_Modified_By.User_Name else '' end as PostedBy " &
      " , TSPL_ADJUSTMENT_HEADER_QC.Posting_Date as QcPosted_Date " &
      " ,tspl_user_master_Modified_By_Qc.User_Name as QcPostedBy " &
      " ,tspl_user_master_Created_By.User_Name as ProdEntryCreatedBy " &
      " , TSPL_LOCATION_MASTER_From_loc.Add1 + case when len(TSPL_LOCATION_MASTER_From_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_From_loc. Add2 else ' '     end + Case when len( TSPL_LOCATION_MASTER_From_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_From_loc. Add3 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_From_loc.Add4 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_From_loc.City_Code else '' end + case when len    (TSPL_LOCATION_MASTER_From_loc.state) > 0 then ','+ TSPL_STATE_MASTER_From_loc.STATE_NAME else ''  end + Case when len     (TSPL_LOCATION_MASTER_From_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_From_loc.Pin_Code,103) else '' end as From_Location_Address  " &
   " From TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL   left outer Join TSPL_ADJUSTMENT_STORE_ENTRY_HEAD On TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo    " &
   " left outer join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code    " &
   " Left outer join tspl_user_master as tspl_user_master_Modified_By on tspl_user_master_Modified_By.User_Code =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.posted_By    " &
   " Left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_loc on TSPL_LOCATION_MASTER_From_loc.Location_Code =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.loc_Code  " &
    " Left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_loc on  TSPL_STATE_MASTER_From_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State   " &
    " Left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_From_location on TSPL_CITY_MASTER_From_location.City_Code = TSPL_LOCATION_MASTER_From_loc.City_Code    " &
   " Left Join TSPL_BATCH_ITEM AS BI ON TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo=BI.Document_Code And TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code=BI.Item_Code  " &
     " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Comp_Code  " &
    " Left outer join TSPL_ADJUSTMENT_HEADER_QC on TSPL_ADJUSTMENT_HEADER_QC.Production_Entry=TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry  " &
    " AND TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No=TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC " &
    "  Left outer join tspl_user_master as tspl_user_master_Modified_By_Qc on tspl_user_master_Modified_By_Qc.User_Code =TSPL_ADJUSTMENT_HEADER_QC.posted_By " &
    " Left Outer Join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry  left outer join tspl_user_master as tspl_user_master_Created_By on tspl_user_master_Created_By.User_Code =TSPL_ADJUSTMENT_HEADER.Created_By  	 " &
       "  Left Outer Join (Select TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No, TSPL_ADJUSTMENT_HEADER_QC.Production_Entry,  case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.batch_no ELSE '' END AS Item_BatchNo,  Case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_ADJUSTMENT_DETAIL_QC.Item_Quantity end as Qty,  TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code,TSPL_ADJUSTMENT_DETAIL_QC.Item_Code from TSPL_ADJUSTMENT_HEADER_QC   Left Outer Join TSPL_ADJUSTMENT_DETAIL_QC on TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No =TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No   left outer join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_DETAIL_QC.item_code= TSPL_ITEM_MASTER.Item_Code  Left Join TSPL_BATCH_ITEM AS BI ON TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No=BI.Document_Code And TSPL_ADJUSTMENT_DETAIL_QC.Item_Code=BI.Item_Code " &
     " And TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code=BI.UOM " &
      " where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No='" + txtProductionEntry.Value + "')TSPL_ADJUSTMENT_DETAIL_QC  " &
      " On TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC =TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No  " &
      " and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry =TSPL_ADJUSTMENT_DETAIL_QC.Production_Entry  " &
      "  And TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code=TSPL_ADJUSTMENT_Detail_QC.Item_Code  " &
      "  and TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code=TSPL_ADJUSTMENT_Detail_QC.Unit_Code  " &
      "  And TSPL_ADJUSTMENT_DETAIL_QC.Item_BatchNo=(case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.BATCH_NO ELSE '' END) " &
    " Left Outer Join (Select TSPL_ADJUSTMENT_HEADER.Adjustment_No,  case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.batch_no ELSE '' END AS Item_BatchNo,  Case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_ADJUSTMENT_DETAIL.Item_Quantity end as Qty,  TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.Item_Code from TSPL_ADJUSTMENT_HEADER   Left Outer Join TSPL_ADJUSTMENT_DETAIL on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_DETAIL.Adjustment_No   left outer join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code  Left Join TSPL_BATCH_ITEM AS BI ON TSPL_ADJUSTMENT_DETAIL.Adjustment_No=BI.Document_Code And TSPL_ADJUSTMENT_DETAIL.Item_Code=BI.Item_Code and TSPL_ADJUSTMENT_Detail.Unit_Code=BI.UOM " &
     "  where TSPL_ADJUSTMENT_HEADER.Adjustment_No ='" + lblProductionEntry.Text + "')TSPL_ADJUSTMENT_DETAIL on TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry    " &
     "  And TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code  " &
     "  and TSPL_ADJUSTMENT_DETAIL.Unit_Code=TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code  " &
     "  And TSPL_ADJUSTMENT_DETAIL.Item_BatchNo=(case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.BATCH_NO ELSE '' END)  " &
     "  Left Outer Join (Select TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry as Adjustment_No,  case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.batch_no ELSE '' END AS Item_BatchNo,   " &
  "  sum( Case when TSPL_ITEM_MASTER.Is_Batch_Item=1 then BI.QTY else TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Quantity end) As Qty, TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD     " &
"  Left Outer Join TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL on TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo =TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo    " &
   "  Left outer join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code   " &
    "  Left Join TSPL_BATCH_ITEM AS BI ON TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo=BI.Document_Code   " &
    "  And TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code=BI.Item_Code And TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code=BI.UOM   where   " &
  "  TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry ='" + lblProductionEntry.Text + "'" &
  "  Group by TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry, TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code, TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code  " &
  "  ,(case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.batch_no ELSE '' END)  " &
"  )StoreEntry on StoreEntry.Adjustment_No=TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry      " &
   "  And StoreEntry.Item_Code = TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Item_Code     " &
    "  And StoreEntry.Unit_Code=TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code      " &
    "  And StoreEntry.Item_BatchNo=(case when TSPL_ITEM_MASTER.Is_Batch_Item=1 THEN BI.BATCH_NO ELSE '' END)" &
     "  where  TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Adjustment_Type = 'PRE' and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo='" + txtAdjustmentNo.Value + "'"



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                dtDocdate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "rptAdjproductionStoreEntry", "Production Store Entry", dtDocdate)
                frmCRV = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    '    Sub PrintData()
    '        Try
    '            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
    '                Throw New Exception("Transaction No not found to print")
    '            End If
    '            PrintData(txtAdjustmentNo.Value, False, False)
    '        Catch ex As Exception
    '            RadMessageBox.Show(ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Public Sub PrintData(ByVal strAdjustmentNo As String, ByVal IsPreprinted As Boolean, ByVal IsEmpty As Boolean)
    '        Try
    '            Dim frmCRV As New frmCrystalReportViewer()
    '            Dim qry As String = "select TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_Type  from TSPL_ADJUSTMENT_DETAIL_QC left outer join TSPL_ADJUSTMENT_HEADER_QC   on TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No=TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No='" + strAdjustmentNo + "' and TSPL_ADJUSTMENT_HEADER_QC.ItemType='E' and TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_Line_No=1"
    '            Dim TransType As String = clsDBFuncationality.getSingleValue("select TSPL_ADJUSTMENT_HEADER_QC.Trans_Type  from TSPL_ADJUSTMENT_HEADER_QC  where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No='" + strAdjustmentNo + "'")
    '            If (clsCommon.CompairString(TransType, "Out") = CompairStringResult.Equal) Then
    '                TransType = "Out"
    '            Else
    '                TransType = "In"
    '            End If
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                qry = "select Head.Adjustment_No as [Adjustment No], Head.Adjustment_Date as [Adjustment Date],Head.Description as [Description], Head.Reference_Document AS [Reference Document], Head.Document_No as [Document No],detail.Item_Code as [Item Code], detail.Item_Description as [Item Description], Location.Location_Desc as [Location], CASE when detail.Adjustment_Type='BI' then 'Both Increase' else CASE when detail.Adjustment_Type='BD' then 'Both Decrease' else CASE when detail.Adjustment_Type='QI' then 'Quantity Increase' else CASE when detail.Adjustment_Type='QD' then 'Quantity Decrease' else CASE when detail.Adjustment_Type='CI' then 'Cost Increase' else CASE when detail.Adjustment_Type='CD' then 'Cost Decrease' end end end end end end  as [Adjustment Type],detail.Item_Quantity as [Quantity], detail.Item_Cost as [Cost Adjustment], detail.Breakage as [Breakage Quantity],detail.Breakage_Cost as [Breakage Cost], detail.mrp as [MRP], detail.Unit_Code as [UOM], detail.MFG_Date as [MFG Date],detail.Batch_No as [Batch No], detail.Expiry_Date  as [Exp. Date],Location.Location_Desc as [Location], TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, (Location.Add1+(case when len(Location.Add2)>0 then ', 'else '' end )+Location.Add2+(case when len(Location.Add3)>0 then ', 'else '' end )+Location.Add3+(case when len(Location.Add4)>0 then ', 'else '' end )+Location.Add4+(case when len(Location.City_Code )>0 then ', 'else '' end ) + '' +TSPL_TDS_STATE_MASTER.State_Name ) as [Add1],head.created_by as [Created by],head.modify_by as [Modified by] from TSPL_ADJUSTMENT_HEADER_QC as Head left outer join TSPL_ADJUSTMENT_DETAIL_QC as detail on head.Adjustment_No = detail.Adjustment_No Left Outer JOIN TSPL_COMPANY_MASTER ON Head.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_LOCATION_MASTER as Location on detail.Location_Code=Location.Location_Code Left Outer Join TSPL_TDS_STATE_MASTER on Location .State=TSPL_TDS_STATE_MASTER.State_Code  where Head.Adjustment_No='" + strAdjustmentNo + "' order by detail.Adjustment_Line_No "
    '                dt = clsDBFuncationality.GetDataTable(qry)
    '                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustment", "Adjustment Detail")
    '            Else
    '                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Adjustment_Type")), "BD") = CompairStringResult.Equal And IsPreprinted = True Then
    '                    ''For both Decrese or Empty Issue/Sent

    '                    If IsPreprinted Then
    '                        qry = "select TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No,(TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date+' '+TSPL_ADJUSTMENT_HEADER_QC.created_time) as Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_CODE,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_CUSTOMER_MASTER.Lst_No,TSPL_ADJUSTMENT_DETAIL_QC.Item_Code,TSPL_ADJUSTMENT_DETAIL_QC.Item_Description,TSPL_ADJUSTMENT_DETAIL_QC.Item_Quantity,TSPL_ADJUSTMENT_DETAIL_QC.mrp,TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost,TSPL_ADJUSTMENT_HEADER.Vehicle_No " &
    '                    " from TSPL_ADJUSTMENT_DETAIL_QC" &
    '                    " left outer join TSPL_ADJUSTMENT_HEADER_QC on TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No=TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No" &
    '                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ADJUSTMENT_HEADER_QC.Customer_CODE" &
    '                    " where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No='" + strAdjustmentNo + "' ORDER by TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_Line_No"
    '                        dt = clsDBFuncationality.GetDataTable(qry)
    '                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomIssue", "Adjustment Detail")
    '                    Else
    '                        ''For both Increase OR Receipt Challan
    '                        Dim strReportName As String = "EMPTY RECEIPT CHALLAN"
    '                        Dim strACaption As String = "From"
    '                        Dim strIssueCaption As String = "Empty Receipt"
    '                        Dim strDateCaption As String = "Receipt Date"
    '                        qry = "select max(Tin_No) as  Tin_No,max(CST_LST) as CST_LST,max(Ecc_No) as Ecc_No,max(Comp_Name) as Comp_Name,max(CompAddress) as CompAddress,Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME, " &
    '                        "MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, " &
    '                        "SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ConvQty,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, " &
    '                        "SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short, SUM(Amount ) as Amount, " &
    '                        "'EMPTY RECEIPT CHALLAN' as ReportName,'From' as ACaption,'Empty Receipt' as EmptyCaption,'Receipt Date' as DateCaption, " &
    '                        "max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date, " &
    '                        "max(Vehicle_No) as Vehicle_No ,MAX(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3, " &
    '                        "max(City_Name) as City_Name,max(State_Name) as State_Name,0 as ChipBT,sum(isnull(Breakage,0)) as Breakage," &
    '                        "sum(isnull(Short_Qty,0)) as Short_Qty,0 as NSBT,0 as HfilledBT,0 as burstBT,0 as Expdt,0 as UnloadBKG, " &
    '                        "0 as TRLkg,0 as TRBkg,0 as rust,0 as dirty,0 as MRP,max(Document_No) as  Document_No,max(Docdate) as Docdate,MAX([Created by]) as [Created by],MAX([Modified by]) as [Modified by]  from ( " &
    '                        "SELECT  Item_Quantity/Conversion_Factor as ConvQty,TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, TSPL_COMPANY_MASTER.Comp_Name,  (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress, " &
    '                        "TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No, TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date, TSPL_ADJUSTMENT_HEADER_QC.Customer_NAME," &
    '                        "TSPL_ADJUSTMENT_HEADER_QC.Description, TSPL_ADJUSTMENT_DETAIL_QC.Item_Code, TSPL_ADJUSTMENT_DETAIL_QC.Item_Description, " &
    '                        "TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code, CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'FC' THEN Item_Quantity END AS FCS, " &
    '                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'FB' THEN Item_Quantity END AS FBS, " &
    '                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'SH' THEN Item_Quantity END AS FSH,  " &
    '                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'EC' THEN Item_Quantity END AS ECS, " &
    '                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'EB' THEN Item_Quantity END AS EBS, 0 AS Leak_Qty, TSPL_ADJUSTMENT_DETAIL_QC.Breakage, " &
    '                        "0 AS Short_Qty, CASE WHEN TSPL_CUSTOMER_MASTER.Cust_Type_Code NOT IN ('F', 'S') THEN " &
    '                        "(ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost, 0)  + ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Breakage_Cost, 0)) ELSE " &
    '                        "ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost, 0) END AS Amount,TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesManName, " &
    '                        "TSPL_ADJUSTMENT_HEADER_QC.Challan_No, " &
    '                        "TSPL_ADJUSTMENT_HEADER_QC.Challan_date, TSPL_ADJUSTMENT_HEADER_QC.Vehicle_No, TSPL_CUSTOMER_MASTER.Add1, " &
    '                        "TSPL_CUSTOMER_MASTER.Add2, TSPL_CUSTOMER_MASTER.Add3, TSPL_CITY_MASTER.City_Name, " &
    '                        "TSPL_TDS_STATE_MASTER.State_Name, TSPL_ADJUSTMENT_HEADER_QC.Document_No,case when Reference_Document='Sale Invoice' then " &
    '                        "Sale_Invoice_Date else Transfer_Date end as Docdate,TSPL_ADJUSTMENT_HEADER_QC.created_by as [Created by],TSPL_ADJUSTMENT_HEADER_QC.modify_by as [Modified by] FROM TSPL_TRANSFER_HEAD RIGHT OUTER JOIN " &
    '                        "TSPL_ADJUSTMENT_HEADER_QC ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_ADJUSTMENT_HEADER_QC.Document_No LEFT OUTER JOIN " &
    '                        "TSPL_SALE_INVOICE_HEAD ON TSPL_ADJUSTMENT_HEADER_QC.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No RIGHT OUTER JOIN " &
    '                        "TSPL_ADJUSTMENT_DETAIL_QC ON TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No LEFT OUTER JOIN " &
    '                        "TSPL_CUSTOMER_MASTER   ON TSPL_ADJUSTMENT_HEADER_QC.Customer_CODE = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " &
    '                        "TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code = TSPL_CUSTOMER_MASTER.City_Code LEFT OUTER JOIN " &
    '                        "TSPL_TDS_STATE_MASTER ON TSPL_TDS_STATE_MASTER.State_Code = TSPL_CUSTOMER_MASTER.State  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_ADJUSTMENT_HEADER.Comp_Code  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ADJUSTMENT_DETAIL_QC.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " &
    '                        "WHERE (TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = '" + strAdjustmentNo + "')  " &
    '                         ") xxx group by Adjustment_No,Item_Code order by Item_Desc"
    '                        dt = clsDBFuncationality.GetDataTable(qry)
    '                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "rptEmptyOutward", "Empty Issue Challan")
    '                    End If


    '                Else
    '                    ''For both Increase OR Receipt Challan
    '                    qry = "select '" & TransType & "' as TransType,MAX(LocAdd) as LocAdd,max(Route_Desc) as Route_Desc,MAX(GPCode) as GPCode,max(Transporter_Name) as Transporter_Name,SUM(RGB) as RGB,SUM(Pet) as Pet,SUM(FSHBreakage) as ShellBreak, " &
    '                    "max(Tin_No) as  Tin_No,max(CST_LST) as CST_LST,max(Ecc_No) as Ecc_No,max(Comp_Name) as Comp_Name,max(CompAddress) as CompAddress,Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME, " &
    '                    "MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, " &
    '                    "SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, " &
    '                    "SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short, SUM(Amount ) as Amount, " &
    '                    "'EMPTY RECEIPT CHALLAN' as ReportName,'From' as ACaption,'Empty Receipt' as EmptyCaption,'Receipt Date' as DateCaption, " &
    '                    "max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date, " &
    '                    "max(Vehicle_No) as Vehicle_No ,MAX(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3, " &
    '                    "max(City_Name) as City_Name,max(State_Name) as State_Name,0 as ChipBT,sum(isnull(Breakage,0)) as Breakage," &
    '                    "sum(isnull(Short_Qty,0)) as Short_Qty,0 as NSBT,0 as HfilledBT,0 as burstBT,0 as Expdt,0 as UnloadBKG, " &
    '                    "0 as TRLkg,0 as TRBkg,0 as rust,0 as dirty,0 as MRP,max(Document_No) as  Document_No,max(Docdate) as Docdate, MAX(locPin) as locPin, MAX(locTinNo) as locTinNo, MAX(locCSTNo) as locCSTNo, MAX(locName) as locName,MAX([Created by]) as [Created by],MAX([Modified by]) as [Modified by] " &
    '"from ( " &
    '                    "SELECT   (TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + Case When TSPL_LOCATION_MASTER.Add4='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add4 end ) as LocAdd, " &
    '                    "case when TSPL_SALE_INVOICE_HEAD.Route_Desc='' then TSPL_SALE_INVOICE_HEAD.Cust_Name else TSPL_SALE_INVOICE_HEAD.Route_Desc end as Route_Desc, " &
    '                            " TSPL_GATEPASS_DETAIL.GPCode as GPCode,Transporter_Name,0 as RGB,0 as Pet,case when TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'SH' THEN Breakage else  0 END AS FSHBreakage , " &
    '                            "TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, TSPL_COMPANY_MASTER.Comp_Name,  (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress, " &
    '                            "TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No, TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date, TSPL_ADJUSTMENT_HEADER_QC.Customer_NAME," &
    '                            "TSPL_ADJUSTMENT_HEADER.Description, TSPL_ADJUSTMENT_DETAIL_QC.Item_Code, TSPL_ADJUSTMENT_DETAIL_QC.Item_Description, " &
    '                            "TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code, CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'FC' THEN Item_Quantity END AS FCS, " &
    '                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'FB' THEN Item_Quantity END AS FBS, " &
    '                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'SH' THEN Item_Quantity END AS FSH,  " &
    '                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'EC' THEN Item_Quantity END AS ECS, " &
    '                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code = 'EB' THEN Item_Quantity END AS EBS, 0 AS Leak_Qty, " &
    '                            "case when TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code='EC' then Breakage  else Breakage end as Breakage, " &
    '                            "0 AS Short_Qty, CASE WHEN TSPL_CUSTOMER_MASTER.Cust_Type_Code NOT IN ('F', 'S') THEN " &
    '                            "(ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost, 0)  + ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Breakage_Cost, 0)) ELSE " &
    '                            "ISNULL(TSPL_ADJUSTMENT_DETAIL_QC.Item_Cost, 0) END AS Amount,TSPL_ADJUSTMENT_HEADER_QC.EMP_NAME AS SalesManName, " &
    '                            "TSPL_ADJUSTMENT_HEADER_QC.Challan_No, " &
    '                            "TSPL_ADJUSTMENT_HEADER_QC.Challan_date, TSPL_ADJUSTMENT_HEADER_QC.Vehicle_No, TSPL_CUSTOMER_MASTER.Add1, " &
    '                            "TSPL_CUSTOMER_MASTER.Add2, TSPL_CUSTOMER_MASTER.Add3, TSPL_CITY_MASTER.City_Name, " &
    '                            "TSPL_TDS_STATE_MASTER.State_Name, TSPL_ADJUSTMENT_HEADER.Document_No,case when Reference_Document='Sale Invoice' then " &
    '                            "Sale_Invoice_Date else Transfer_Date end as Docdate, TSPL_LOCATION_MASTER.Pin_Code as locPin, TSPL_LOCATION_MASTER.TIN_No as locTinNo, TSPL_LOCATION_MASTER.CST_No as locCSTNo, TSPL_LOCATION_MASTER.Location_Desc as locName ,TSPL_ADJUSTMENT_HEADER.created_by as [Created by],TSPL_ADJUSTMENT_HEADER.modify_by as [Modified by] FROM TSPL_TRANSFER_HEAD RIGHT OUTER JOIN " &
    '                            "TSPL_ADJUSTMENT_HEADER_QC ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_ADJUSTMENT_HEADER_QC.Document_No LEFT OUTER JOIN " &
    '                            "TSPL_SALE_INVOICE_HEAD ON TSPL_ADJUSTMENT_HEADER_QC.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No RIGHT OUTER JOIN " &
    '                            "TSPL_ADJUSTMENT_DETAIL_QC ON TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No LEFT OUTER JOIN " &
    '                            "TSPL_CUSTOMER_MASTER   ON TSPL_ADJUSTMENT_HEADER_QC.Customer_CODE = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " &
    '                            "TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code = TSPL_CUSTOMER_MASTER.City_Code LEFT OUTER JOIN " &
    '                            "TSPL_TDS_STATE_MASTER ON TSPL_TDS_STATE_MASTER.State_Code = TSPL_CUSTOMER_MASTER.State  LEFT OUTER JOIN " &
    '                            "TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_ADJUSTMENT_HEADER_QC.Comp_Code  " &
    '                            " left outer join TSPL_VEHICLE_MASTER on TSPL_SALE_INVOICE_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  left outer join " &
    '                            " TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id  left outer join " &
    '                            " TSPL_ITEM_UOM_DETAIL on TSPL_ADJUSTMENT_DETAIL_QC.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code   " &
    '                            " left outer join TSPL_GATEPASS_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_GATEPASS_DETAIL.DocNo " &
    '                            " left outer join TSPL_LOCATION_MASTER on TSPL_ADJUSTMENT_HEADER_QC.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  " &
    '                            " WHERE (TSPL_ADJUSTMENT_HEADER.Adjustment_No = '" + strAdjustmentNo + "') and (TSPL_ITEM_UOM_DETAIL.UOM_Code='EB' or TSPL_ITEM_UOM_DETAIL.UOM_Code='SH')  "
    '                    If IsPreprinted Then
    '                        qry += " union all " &
    '                       "SELECT   (TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + Case When TSPL_LOCATION_MASTER.Add4='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add4 end ) as LocAdd, " &
    '                       "case when TSPL_SALE_INVOICE_HEAD.Route_Desc='' then TSPL_SALE_INVOICE_HEAD.Cust_Name else TSPL_SALE_INVOICE_HEAD.Route_Desc end as Route_Desc, " &
    '                       "isnull(GPCode,'') as GPCode,Transporter_Name,case when TSPL_SALE_INVOICE_DETAIL.Empty_Value > 0 then " &
    '                       "convert(decimal(18,2),(Invoice_Qty/Conversion_Factor)) else 0 end as RGB , " &
    '                       "case when TSPL_SALE_INVOICE_DETAIL.Empty_Value > 0 then 0 else convert(decimal(18,2),(Invoice_Qty/Conversion_Factor))  end as Pet , " &
    '                       "0 AS FSHBreakage ,'' as Tin_No, '' as CST_LST, '' as Ecc_No, '' as Comp_Name,  '' AS CompAddress, " &
    '                       "TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No, TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date, " &
    '                       "TSPL_ADJUSTMENT_HEADER_QC.Customer_NAME,TSPL_ADJUSTMENT_HEADER_QC.Description, " &
    '                       "'' as Item_Code, '' as Item_Description, '' as Unit_Code,0 AS FCS, 0 AS FBS, 0 AS FSH, " &
    '                       "0 AS ECS, 0 AS EBS, 0 AS Leak_Qty, 0 as Breakage, 0 AS Short_Qty, 0 AS Amount, " &
    '                       "TSPL_ADJUSTMENT_HEADER_QC.EMP_NAME AS SalesManName, TSPL_ADJUSTMENT_HEADER_QC.Challan_No, " &
    '                       "TSPL_ADJUSTMENT_HEADER_QC.Challan_date, TSPL_ADJUSTMENT_HEADER_QC.Vehicle_No, " &
    '                       "'' as Add1, '' as Add2, '' as Add3, '' as City_Name, '' as State_Name, " &
    '                       "TSPL_ADJUSTMENT_HEADER_QC.Document_No,case when Reference_Document='Sale Invoice' then Sale_Invoice_Date else null end as Docdate , '' as  locPin, '' as locTinNo, '' as locCSTNo, '' as locName " &
    '                       ",TSPL_ADJUSTMENT_HEADER_QC.created_by as [Created by],TSPL_ADJUSTMENT_HEADER_QC.modify_by as [Modified by] from TSPL_ADJUSTMENT_HEADER_QC inner join TSPL_SALE_INVOICE_HEAD on " &
    '                       "TSPL_ADJUSTMENT_HEADER_QC.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No left outer join " &
    '                       "TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " &
    '                       "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
    '                       "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join TSPL_VEHICLE_MASTER on " &
    '                       "TSPL_SALE_INVOICE_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  left outer join TSPL_TRANSPORT_MASTER on " &
    '                       "TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id left outer join TSPL_GATEPASS_DETAIL on " &
    '                       "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_GATEPASS_DETAIL.DocNo   " &
    '                       " left outer join TSPL_LOCATION_MASTER on TSPL_ADJUSTMENT_HEADER_QC.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  " &
    '                       "WHERE (TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = '" + strAdjustmentNo + "') "
    '                    End If
    '                    qry += ") xxx group by Adjustment_No,Item_Code order by Item_Desc"
    '                    dt = clsDBFuncationality.GetDataTable(qry)
    '                    If IsEmpty Then
    '                        If IsPreprinted Then
    '                            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "crptAdjustmentCustomReceiptGun", "Adjustment Detail")
    '                        Else

    '                            If (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "guntur") = CompairStringResult.Equal) Then
    '                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "crptAdjustmentCustomReceiptGuntur", "Adjustment Detail")
    '                            Else
    '                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "crptAdjustmentCustomReceiptVizag", "Adjustment Detail")

    '                            End If
    '                        End If
    '                    Else
    '                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomReceipt", "Adjustment Detail")
    '                    End If
    '                End If
    '            End If
    '            frmCRV = Nothing
    '        Catch ex As Exception
    '            RadMessageBox.Show(ex.Message)
    '        End Try
    '    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
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
                If ClsAdjustmentsStoreEntry.ReverseAndUnpost(txtAdjustmentNo.Value, trans) Then
                    saveCancelLog(Reason, "Reverse And Recreate", trans)
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim Item_type As String = clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "'")
            If clsCommon.CompairString("In", clsCommon.myCstr(cboTransType.SelectedValue)) = CompairStringResult.Equal Then
                Dim frm As FrmSerializeItemIn = New FrmSerializeItemIn()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strBinNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colBinNo).Value)
                frm.strItemType = Item_type
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            Else
                Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.strLocationCode = txtLocation.Value
                frm.strCurrDocNo = txtAdjustmentNo.Value

                frm.strCurrDocType = "IC-AD"
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strItemType = Item_type
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            End If
        End If
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
        If e.KeyCode = Keys.F5 Then
            '===========Update by preeti gupta against ticket no[BHA/23/08/18-000477]==========
            If ChkMilkType.Checked Then
                If RunBatchFifowise = 0 Or cboTransType.SelectedValue = "In" Then
                    OpenBatchItemNew()
                Else
                    OpenBatchItemIfFIFIOSettingONNew()
                End If

            Else
                If RunBatchFifowise = 0 Or cboTransType.SelectedValue = "In" Then
                    OpenBatchItem()
                Else
                    OpenBatchItemIfFIFIOSettingON()
                End If
            End If

            '=========================================
        End If
    End Sub

    Private Sub txtBarCode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBarCode.Validating
        If clsCommon.myLen(txtBarCode.Text) > 0 Then
            Dim obj As clsBarCodeGenerator = clsBarCodeGenerator.GetData(txtBarCode.Text)
            If obj Is Nothing Then
                clsCommon.MyMessageBoxShow("Not a Valid Barcode", Me.Text)
                txtBarCode.Text = ""
                Exit Sub
            End If

            Dim isFound As Boolean = False
            Dim CurrentRow As Integer = 1
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(txtBarCode.Text, clsCommon.myCstr(gv1.Rows(ii).Cells(colBarCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + 1
                    CurrentRow = ii
                    isFound = True
                    Exit For
                End If
            Next
            If Not isFound Then

                gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = obj.Bar_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                OpenICodeList(False)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = obj.Item_Selling_Price
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.Item_MRP
                CurrentRow = gv1.Rows.Count - 1
                For ii As Integer = 1 To gv1.Rows.Count
                    gv1.Rows(ii - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)
                Next
                gv1.Rows.AddNew()

            End If

            UpdateCurrentRow(CurrentRow)
            UpdateAllTotals()
            txtBarCode.Text = ""
            txtBarCode.Focus()
        End If
    End Sub

    Private Sub chklocation_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocation.ToggleStateChanged
        If chklocation.Checked Then
            txtLocation.Value = ""
            lblLocation.Text = ""
        ElseIf Not chklocation.Checked Then
            txtLocation.Value = ""
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub ExporttoExcelWithSerial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExporttoExcelWithSerial.Click
        Try
            Dim qryExport As String = Nothing
            'qryExport = " Select '' as [Location], '' as [Adjustment Date], '' as [Item Code], '' as [Quantity], '' as [Cost Adjustment],'N' as [Third Party Location],'' as [Serial No],'' as [Tag No],'' as Type"
            'qryExport = "Select Loc_Code as [Location],Loc_desc as [Location Desc], Adjustment_Date as [Adjustment Date], TSPL_ADJUSTMENT_detail.Item_Code as [Item Code],TSPL_ADJUSTMENT_detail.unit_code as [unit code]," _
            '& " Item_Quantity as [Quantity], Item_Cost as [Cost Adjustment],'N' as [Third Party Location],Auto_Sr_No as [Serial No],Tag_No as [Tag No],'' as Type,TSPL_ADJUSTMENT_HEADER.Description " _
            '& " from TSPL_ADJUSTMENT_HEADER inner join TSPL_ADJUSTMENT_detail on TSPL_ADJUSTMENT_detail.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No " _
            '& " left join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Document_Code=TSPL_ADJUSTMENT_HEADER.Adjustment_No  and TSPL_ADJUSTMENT_detail.Adjustment_Line_No = TSPL_SERIAL_ITEM.Line_No  and TSPL_ADJUSTMENT_detail.Item_Code = TSPL_SERIAL_ITEM.Item_Code  left join TSPL_VISI_MASTER on " _
            '& " TSPL_VISI_MASTER.Serial_No=TSPL_SERIAL_ITEM.Auto_Sr_No"
            'KUNAL > JACKSON > BUG WAS : OUT OF MEMORY EXCEPTION > STATUS FIXED
            qryExport = " Select TSPL_ADJUSTMENT_DETAIL_QC.Location_Code as [Location], TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date , TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No, TSPL_ADJUSTMENT_DETAIL_QC.Item_Code as [Item Code],TSPL_ADJUSTMENT_DETAIL_QC.unit_code as [unit code], Item_Quantity as [Quantity], Item_Cost as [Cost Adjustment],'N' as [Third Party Location], TSPL_SERIAL_ITEM.Auto_Sr_No as [Serial No],'' as [Tag No],'' as Type, case when TSPL_ADJUSTMENT_HEADER_QC.adjustment_type='ADJ' then 'Adjustment'" &
                        " when TSPL_ADJUSTMENT_HEADER_QC.adjustment_type='FLG' then 'Flushing'" &
                        " when TSPL_ADJUSTMENT_HEADER_QC.adjustment_type='OPG' then 'Opening'" &
                         " when TSPL_ADJUSTMENT_HEADER_QC.adjustment_type='CLG' then 'Closing'" &
                         " when TSPL_ADJUSTMENT_HEADER_QC.adjustment_type='AAD' then 'Auto Adjustment'" &
                         "  when TSPL_ADJUSTMENT_HEADER_QC.adjustment_type='OTH' then 'Other'" &
                         " else '' end   as [Transaction Type]   from TSPL_ADJUSTMENT_DETAIL_QC  left join TSPL_ADJUSTMENT_HEADER_QC on TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No =TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No  LEFT JOIN TSPL_SERIAL_ITEM  on  TSPL_SERIAL_ITEM.Document_Code=TSPL_ADJUSTMENT_detail.Adjustment_No and TSPL_SERIAL_ITEM.Parent_Line_No =  TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No and   TSPL_SERIAL_ITEM.Item_Code = TSPL_ADJUSTMENT_DETAIL.Item_Code  left join TSPL_VISI_MASTER on  TSPL_VISI_MASTER.Serial_No=TSPL_SERIAL_ITEM.Auto_Sr_No LEFT JOIN   TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_ADJUSTMENT_DETAIL_QC.Location_Code "
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub

    Private Sub ChkMilkType_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkMilkType.ToggleStateChanged ''added by richa agarwal 09/10/2014
        If clsCommon.myLen(txtAdjustmentNo.Value) > 0 Then
        Else

            If RadLabel15.Text = "Location" Then
                If ChkMilkType.Checked Then
                    FndMainLocation.Enabled = True
                    RadLabel15.Text = "Sub Location/Section"
                    txtLocation.Value = ""
                    lblLocation.Text = ""
                    LblMainLocation.Text = ""
                    LblMainLocation.Text = ""
                    LoadBlankGrid()
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth

                    chkJobWork.Visible = True
                Else
                    FndMainLocation.Enabled = False
                    RadLabel15.Text = "Location"
                    chkJobWork.Visible = True
                End If
            Else
                If ChkMilkType.Checked Then
                    FndMainLocation.Enabled = True
                    RadLabel15.Text = "Sub Location/Section"
                    chkJobWork.Visible = True
                Else
                    FndMainLocation.Enabled = False
                    RadLabel15.Text = "Location"
                    FndMainLocation.Value = ""
                    lblLocation.Text = ""
                    txtLocation.Value = ""
                    LblMainLocation.Text = ""
                    LoadBlankGrid()
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                    chkJobWork.Visible = False
                End If
            End If


        End If


    End Sub

    Private Sub FndMainLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMainLocation._MYValidating
        If ChkMilkType.Checked Then
            FndMainLocation.Value = clsLocation.getFinder(" (Location_Type='Physical' and Is_Sub_Location='Y' and Is_Section ='N' and (isnull(Is_Jobwork,0)=1)) or (Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and (isnull(Is_Jobwork,0)=0)) ", FndMainLocation.Value, isButtonClicked)
        Else
            FndMainLocation.Value = clsLocation.getFinder(" Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' ", FndMainLocation.Value, isButtonClicked)
        End If


        If clsCommon.myLen(FndMainLocation.Value) > 0 Then
            LblMainLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndMainLocation.Value & "'"))
        Else
            LblMainLocation.Text = ""
        End If
    End Sub

    'Private Sub rbtnExportPosted_Click(sender As Object, e As EventArgs) Handles rbtnExportPosted.Click
    '    Try
    '        Dim qryExport As String
    '        qryExport = " select TSPL_ADJUSTMENT_HEADER.Adjustment_No as [Adjustment No],MainLocationCode as [Main Location],Loc_Code as [Location], convert(varchar,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as [Adjustment Date], " &
    '                    " TSPL_ADJUSTMENT_HEADER.Trans_Type as [In/Out],TSPL_ADJUSTMENT_HEADER.IsMilkType as [Is Milk],TSPL_ADJUSTMENT_DETAIL.Item_Code as [Item Code],TSPL_ADJUSTMENT_DETAIL.Unit_Code as [Unit Code], " &
    '                    " TSPL_ADJUSTMENT_DETAIL.Item_Quantity as [Quantity], TSPL_ADJUSTMENT_DETAIL.Unit_Cost as [Item Cost], TSPL_ADJUSTMENT_DETAIL.Item_Cost as [Amount], TSPL_ADJUSTMENT_HEADER.Third_Party_Location as [Third Party Location],TSPL_ADJUSTMENT_DETAIL.Adjustment_Type as [Adjustment Type(BD/BI/CD/CI/QD/QI)] " &
    '                    " from TSPL_ADJUSTMENT_HEADER " &
    '                    " inner join TSPL_ADJUSTMENT_DETAIL on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No "
    '        Dim Cond As String = "and Posted='Y' and Against_Bulk_Srn_PI_adjustment is null and Against_AP_Invoice_No is null and Auto_Gen_Againnt_PI_No is null" &
    '                             " and Against_Transfer_In_Doc_No is null and Against_Tanker_Dispatch_Doc_No is null and Against_PI_No_Difference is null " &
    '                             " and Against_PI_No_Difference_Rejected is null "
    '        transportSql.ExporttoExcel(qryExport, Cond, "[Adjustment No],[Item Code]", Me)
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
    '    End Try
    'End Sub

    Private Sub rbtnImportPosted_Click(sender As Object, e As EventArgs)
        ' done by panch raj against ticket No: BM00000008191,BM00000008189
        'Dim gv As New RadGridView()

        'Me.Controls.Add(gv)
        'Dim currentdate As Date = Date.Today
        'If transportSql.importExcel(gv, "Adjustment No", "Main Location", "Location", "Adjustment Date", "In/Out", "Is Milk", "Item Code", "Unit Code", "Quantity", "Item Cost", "Amount", "Third Party Location", "Adjustment Type(BD/BI/CD/CI/QD/QI)") Then
        '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        '    Try
        '        Dim AdjList As New List(Of String)

        '        '' get list of adjustments
        '        For Each dr As GridViewRowInfo In gv.Rows
        '            If AdjList.Contains(clsCommon.myCstr(dr.Cells("Adjustment No").Value)) = False Then
        '                AdjList.Add(clsCommon.myCstr(dr.Cells("Adjustment No").Value))
        '            End If
        '        Next
        '        For Each strAdcode As String In AdjList
        '            'Dim strAdcode As String = ""
        '            Dim obj As New ClsAdjustments()
        '            obj = obj.GetData(strAdcode, "", NavigatorType.Current, trans)
        '            obj.Arr = New List(Of ClsAdjustmentsDetails)()
        '            For Each grow As GridViewRowInfo In gv.Rows
        '                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Adjustment No").Value), strAdcode) <> CompairStringResult.Equal Then
        '                    Continue For
        '                End If
        '                Dim line As Integer = 1
        '                Dim strIType As String = "RM"
        '                Dim IsMilk As String = grow.Cells("Is Milk").Value.ToString()
        '                Dim MainLoc As String = ""
        '                If clsCommon.CompairString(IsMilk, "1") = CompairStringResult.Equal Then
        '                    MainLoc = grow.Cells("Main Location").Value.ToString()
        '                Else
        '                    MainLoc = ""
        '                End If
        '                Dim InOut As String = grow.Cells("In/Out").Value.ToString()

        '                Dim strLoc As String = grow.Cells("Location").Value.ToString()
        '                If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
        '                    Throw New Exception("Check the value for Location")
        '                End If
        '                Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ", trans)
        '                'Dim strADate As String = grow.Cells(1).Value.ToString()
        '                Dim strADate As String = clsCommon.GetPrintDate(grow.Cells("Adjustment Date").Value, "dd/MMM/yyyy")
        '                Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
        '                Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

        '                Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
        '                If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
        '                    Throw New Exception("Check the value for Item Code")
        '                End If
        '                Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')", trans)
        '                Dim AdjType As String = "BI"
        '                strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, trans))
        '                If clsCommon.myLen(strIType) <= 0 Then
        '                    strIType = "RM"
        '                End If
        '                '------------------------------------------------------------------------------------------------
        '                Dim thirdparty As String = ""
        '                thirdparty = clsCommon.myCstr(grow.Cells("Third Party Location").Value.ToString().ToUpper())

        '                If Not clsCommon.CompairString(thirdparty, "N") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(thirdparty, "Y") = CompairStringResult.Equal Then
        '                    Throw New Exception("Values Should Be N or Y In ColumnName [Third Party Location]")
        '                End If



        '                AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type(BD/BI/CD/CI/QD/QI)").Value.ToString().ToUpper())

        '                If clsCommon.myLen(AdjType) <= 0 Then
        '                    Throw New Exception("Please Fill Adjustment Type In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
        '                End If
        '                If Not clsCommon.CompairString(AdjType, "BD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal Then
        '                    Throw New Exception("Values Should Be any from BD/BI/CD/CI/QD/QI In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
        '                End If

        '                obj.chklocation = thirdparty
        '                'Adjustment Type(BD/BI/CD/CI/QD/QI)
        '                '--------------------------------------------------------------------------------------------------

        '                '-------------------------------
        '                Dim struom As String = grow.Cells("Unit Code").Value.ToString()
        '                If clsCommon.myLen(struom) = 0 Then
        '                    struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
        '                Else
        '                    Dim intCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(unit_code) from tspl_unit_master where unit_code='" & struom & "'", trans))
        '                    If intCount = 0 Then
        '                        Throw New Exception("Unit code is not correct")
        '                    End If
        '                End If

        '                ' Dim struom As String = "UNIT"
        '                '---------------------------------------------------------------------------------------------------
        '                Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Quantity").Value)

        '                Dim Btype As String = "Select"
        '                Dim Bqty As Decimal = 0
        '                Dim Bcost As Decimal = 0
        '                Dim Lqty As Decimal = 0
        '                Dim StrMRP As Decimal = 0
        '                Dim MFGDate As String = clsCommon.GETSERVERDATE(trans)
        '                Dim Batch As String = ""
        '                Dim expdate As String = clsCommon.GETSERVERDATE(trans)
        '                Dim rmk As String = ""
        '                Dim commt As String = ""

        '                Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
        '                Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
        '                obj.Adjustment_No = strAdcode
        '                obj.Adjustment_Date = strADate
        '                obj.MainLocationCode = MainLoc
        '                obj.IsMilkType = IsMilk
        '                'obj.Reference = ""
        '                'obj.Description = ""
        '                obj.Unit_Code = "ALL"
        '                obj.ItemType = strIType
        '                obj.Loc_Code = strLoc
        '                obj.Loc_Desc = strLocDesc
        '                obj.Trans_Type = InOut

        '                Dim objTr As New ClsAdjustmentsDetails()
        '                'objTr.Adjustment_No = ""
        '                objTr.Adjustment_Line_No = line
        '                objTr.Item_Code = ItemCode
        '                objTr.Item_Description = ItemDesc
        '                objTr.Adjustment_Type = AdjType
        '                'objTr.Location_Code=Pick in SaveData from header
        '                objTr.Item_Quantity = Iqty
        '                If clsCommon.myCdbl(grow.Cells("Item Cost").Value) <= 0 Then
        '                    objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans))
        '                Else
        '                    objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
        '                End If

        '                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value) 'Iqty * objTr.Unit_Cost
        '                objTr.Unit_Code = struom
        '                objTr.Remarks = rmk
        '                objTr.Comments = commt
        '                objTr.mrp = StrMRP

        '                objTr.BreakageType = Btype
        '                objTr.Breakage = Bqty
        '                objTr.Breakage_Cost = Bcost
        '                objTr.LeakageQty = Lqty

        '                objTr.MFG_Date = MFGDate
        '                objTr.Batch_No = Batch
        '                objTr.Expiry_Date = expdate

        '                objTr.ItemType = strIType
        '                obj.ItemType = strIType

        '                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
        '                    obj.Arr.Add(objTr)
        '                End If

        '                line = line + 1
        '            Next
        '            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
        '                Throw New Exception("Please Fill at list one Item")
        '            End If
        '            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No='" + obj.Adjustment_No + "'", trans)
        '            Dim isSaved As Boolean = True
        '            isSaved = isSaved AndAlso ClsAdjustments.ReverseAndUnpost(obj.Adjustment_No, trans)
        '            isSaved = isSaved AndAlso obj.SaveData(obj, False, "", trans)
        '            isSaved = isSaved AndAlso ClsAdjustments.PostData(obj.Adjustment_No, "Store Adjustment", trans, True, VoucherNo)
        '        Next
        '        trans.Commit()

        '        RadMessageBox.Show("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '    Catch ex As Exception
        '        myMessages.myExceptions(ex)
        '        trans.Rollback()

        '    End Try

        'End If
    End Sub

    Private Function CaptureScreenData() As ClsAdjustmentsQCC
        Dim obj As New ClsAdjustmentsQCC()
        Try
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Adjustment", txtDate.Value) = False Then
                Return obj
            End If
            If (AllowToSave()) Then

                obj.Adjustment_No = txtAdjustmentNo.Value
                obj.Adjustment_Date = txtDate.Value
                'obj.Posting_Date
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                'obj.Posted()

                obj.Unit_Code = "ALL"
                ''obj.ItemType = "E" Fill at Detail level

                obj.Loc_Code = txtLocation.Value
                obj.Loc_Desc = lblLocation.Text
                obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)

                obj.chklocation = "N"
                If chklocation.Checked Then
                    obj.chklocation = "Y"
                End If

                If ChkMilkType.Checked Then
                    obj.IsMilkType = 1
                Else
                    obj.IsMilkType = 0
                End If
                obj.MainLocationCode = FndMainLocation.Value
                obj.MainLocationDesc = LblMainLocation.Text
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        Dim objTr As New ClsAdjustmentsQCCDetails()
                        'objTr.Adjustment_No=
                        objTr.Adjustment_Line_No = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(colLineNo).Value))
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Description = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                        objTr.Adjustment_Type = clsCommon.myCstr(grow.Cells(colAdjustmentType).Value).Substring(0, 1) + IIf(clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal, "I", "D")
                        'objTr.Location_Code=Pick in SaveData from header
                        objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colCost).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        'objTr.Account_Code= Pick in SaveData
                        'objTr.Account_Description=Pick in SaveData
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.Comments = clsCommon.myCstr(grow.Cells(colComment).Value)
                        objTr.mrp = clsCommon.myCdbl(grow.Cells(colMRP).Value)

                        objTr.fat_pers = clsCommon.myCdbl(grow.Cells(colFATPers).Value)
                        objTr.fat_kg = clsCommon.myCdbl(grow.Cells(colFATKG).Value)
                        objTr.snf_kg = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        objTr.snf_pers = clsCommon.myCdbl(grow.Cells(colSNFPers).Value)

                        'objTr.MFG_Date =
                        'objTr.Batch_No=
                        'objTr.Expiry_Date =
                        'objTr.Breakage =
                        'objTr.Breakage_Cost =
                        objTr.ItemType = clsItemMaster.GetStoreAdjustmentItemType(objTr.Item_Code)
                        If isFirstTime Then
                            obj.ItemType = objTr.ItemType
                            isFirstTime = False
                        End If
                        objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                        objTr.Itemstatus = clsCommon.myCstr(grow.Cells(colICodeStatus).Value)

                        If clsCommon.myLen(objTr.Itemstatus) <= 0 Then
                            objTr.Itemstatus = "NEW"
                        End If

                        '' Ticket No : BM00000007708 : aded by Panch Raj
                        objTr.Price_Type = clsCommon.myCstr(grow.Cells(colPrice_Type).Value)
                        objTr.MCC_Price_Code = clsCommon.myCstr(grow.Cells(colMCC_Price_Code).Value)
                        objTr.Bulk_Price_Code = clsCommon.myCstr(grow.Cells(colBulk_Price_Code).Value)

                        objTr.fat_Rate = clsCommon.myCdbl(grow.Cells(colfat_Rate).Value)
                        objTr.fat_Amt = clsCommon.myCdbl(grow.Cells(colfat_Amt).Value)
                        objTr.snf_Rate = clsCommon.myCdbl(grow.Cells(colsnf_Rate).Value)
                        objTr.snf_Amt = clsCommon.myCdbl(grow.Cells(colsnf_Amt).Value)

                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function

    Private Sub cmdEditAndPost_Click(sender As Object, e As EventArgs)
        '' added by Panch raj against Ticket No:BM00000008482
        'If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Posted Document.")
        '    Exit Sub
        'ElseIf UsLock1.Status <> ERPTransactionStatus.Posted And UsLock1.Status <> ERPTransactionStatus.Approved Then
        '    clsCommon.MyMessageBoxShow("Document must be posted for Edit and Post.")
        '    Exit Sub
        'End If
        'Dim objNew As New ClsAdjustments
        'objNew = CaptureScreenData()
        'If Not objNew Is Nothing AndAlso clsCommon.myLen(objNew.Adjustment_No) > 0 Then
        '    EditAndPost(objNew)
        'End If

    End Sub

    'Function EditAndPost(ByVal objNew As ClsAdjustmentsQCC) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
    '    Try
    '        'Dim obj As New ClsAdjustments()
    '        'obj = obj.GetData(txtAdjustmentNo.Value, AdjustmentEnum.strCostTransaction, NavigatorType.Current, Nothing)
    '        Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No='" + objNew.Adjustment_No + "'", trans)
    '        Dim isSaved As Boolean = True

    '        isSaved = isSaved AndAlso ClsAdjustmentsQCC.ReverseAndUnpost(objNew.Adjustment_No, trans)
    '        isSaved = isSaved AndAlso objNew.SaveData(objNew, False, "", trans)
    '        isSaved = isSaved AndAlso ClsAdjustmentsQCC.PostData(objNew.Adjustment_No, "Store Adjustment", trans, True, VoucherNo)
    '        trans.Commit()
    '        RadMessageBox.Show("Edit and Posted Completed!", Me.Text, MessageBoxButtons.OK)
    '        Return isSaved
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '        trans.Rollback()
    '        Return False
    '    End Try
    'End Function

    Private Sub rmExcelforMilkType_Click(sender As Object, e As EventArgs) Handles rmExcelforMilkType.Click
        Try
            Dim qryExport As String
            qryExport = " Select '' as [Location], '' as [Adjustment Date],'' as [In/Out], '' as [Item Code],'' as [unit code], '' as [Quantity], '' as [Item Cost], '' as [Amount],'' as [Main Location],'BI' as [Adjustment Type(BD/BI/CD/CI/QD/QI)],'' as Description,'' as [FAT %] ,'' as [SNF %],'' as [Price Type],'' as [Price Code],'' as [FAT Rate],'' as [SNF Rate],'' as [Transaction Type]"
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub

    Private Sub rmOpeningForMilkType_Click(sender As Object, e As EventArgs) Handles rmOpeningForMilkType.Click
        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Location", "Adjustment Date", "In/Out", "Item Code", "unit code", "Quantity", "Item Cost", "Amount", "Main Location", "Adjustment Type(BD/BI/CD/CI/QD/QI)", "Description", "FAT %", "SNF %", "Price Type", "Price Code", "FAT Rate", "SNF Rate", "Transaction Type") Then
            Dim trans As SqlTransaction = Nothing
            Try
                Dim obj As New ClsAdjustmentsQCC()
                clsCommon.ProgressBarShow()
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                Dim Checkqry As String = ""

                Dim countindex As Integer = 0
                Dim strAdcode As String = ""
                Dim strLocDesc As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New ClsAdjustmentsQCC()
                    obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                    Dim MilkType As Integer = 1
                    Dim strIType As String = "RM"
                    '===================
                    Dim strMainLoc As String = grow.Cells("Main Location").Value.ToString()
                    'If String.IsNullOrEmpty(strMainLoc) Or strMainLoc.Length > 12 Then
                    '    Throw New Exception("Check the value for Main Location")
                    'End If
                    If clsCommon.myLen(strMainLoc) > 0 Then
                        Checkqry = "select count(Location_Code) from tspl_location_master where Location_Code='" + strMainLoc + "'"
                        countindex = clsDBFuncationality.getSingleValue(Checkqry, trans)
                        If (countindex) <= 0 Then
                            Throw New Exception("Main Location Code Is Invalid Or Does Not Exist")
                        End If
                    End If

                    Dim strMainLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strMainLoc + "' ")
                    '============================
                    'txtLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Loc_Segment_Code='" + clsLocation.GetSegmentCode(FndMainLocation.Value, Nothing) + "'", txtLocation.Value, isButtonClicked)



                    Dim strLoc As String = grow.Cells("Location").Value.ToString()
                    Dim Checkqry1 As String = ""
                    Dim qry6 As String = clsDBFuncationality.getSingleValue("select Location_Category  from tspl_location_master where Location_Code = '" + strMainLoc + "'")
                    If clsCommon.CompairString(qry6, "MCC") <> CompairStringResult.Equal Then
                        If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                            Throw New Exception("Check the value for Location")
                        End If
                    End If
                    If clsCommon.myLen(strLoc) > 0 Then
                        Checkqry1 = clsDBFuncationality.getSingleValue("select (Loc_Segment_Code) from tspl_location_master where Location_Code='" + strMainLoc + "' ")
                        If clsCommon.myLen(Checkqry1) > 0 Then

                            Dim Checkqry2 As String = "select count(Location_Code) from TSPL_LOCATION_MASTER where Loc_Segment_Code ='" + Checkqry1 + "'  and (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') "
                            Dim countindex1 As Integer = clsDBFuncationality.getSingleValue(Checkqry2, trans)
                            If (countindex1) <= 0 Then
                                Throw New Exception("Sub Location Code Is Invalid Or Does Not Exist")
                            End If
                            If clsCommon.myLen(Checkqry2) > 0 Then
                                strLocDesc = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ")
                            End If
                        End If
                    End If
                    '====================================Preeti=======================
                    Dim strTransactionType As String = ""
                    strTransactionType = clsCommon.myCstr(grow.Cells("Transaction Type").Value.ToString())
                    If clsCommon.myLen(strTransactionType) <= 0 Then
                        Throw New Exception("Enter Transaction Type")
                    End If
                    If Not clsCommon.CompairString(strTransactionType, "Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Flushing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Opening") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Closing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Auto Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Other") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be Adjustment/Flushing/Opening/Closing/Auto Adjustment/Other In ColumnName [Transaction Type]")
                    End If
                    '=========================================================
                    Dim InOut As String = grow.Cells("In/Out").Value.ToString()
                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "OUT") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Please Insert In or Out type")
                    End If

                    If grow.Cells("Adjustment Date").Value Is Nothing OrElse clsCommon.myLen(grow.Cells("Adjustment Date").Value) <= 0 OrElse Not IsDate(grow.Cells("Adjustment Date").Value) Then
                        Throw New Exception("Please check Adjsutment Date")
                    End If
                    Dim strADate As String = clsCommon.GetPrintDate(grow.Cells("Adjustment Date").Value, "yyyy/MM/dd")

                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    '===============================
                    Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code")
                    End If
                    Dim checkitem As String = "select count(Item_code) from TSPL_ITEM_MASTER where Item_Code = '" + ItemCode + "' and Product_Type ='MI'"
                    Dim CountItem As Integer = clsDBFuncationality.getSingleValue(checkitem, trans)
                    If (CountItem) <= 0 Then
                        Throw New Exception("Item Code Is Invalid for Milk Type Or Does Not Exist")
                    End If
                    Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
                    '===========================================================

                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = "BI"
                    strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, Nothing))
                    If clsCommon.myLen(strIType) <= 0 Then
                        strIType = "RM"
                    End If

                    AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type(BD/BI/CD/CI/QD/QI)").Value.ToString().ToUpper())

                    If clsCommon.myLen(AdjType) <= 0 Then
                        Throw New Exception("Please Fill Adjustment Type In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If
                    If Not clsCommon.CompairString(AdjType, "BD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be any from BD/BI/CD/CI/QD/QI In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If

                    Dim struom As String = grow.Cells("unit code").Value.ToString()

                    If clsCommon.myLen(struom) = 0 Then
                        struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Else
                        Dim intCount As String = "select count(UOM_Code) from TSPL_ITEM_UOM_DETAIL where Item_code='" & ItemCode & "'"
                        If clsCommon.myLen(intCount) > 0 Then
                            Dim Checkqry3 As String = "select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_code='" + ItemCode + "' and UOM_Code = '" + struom + "' "
                            Dim countindex3 As String = clsDBFuncationality.getSingleValue(Checkqry3, trans)
                            If clsCommon.myLen(countindex3) <= 0 Then
                                Throw New Exception("Unit Code Is Invalid Or Does Not Exist")
                            End If

                        End If

                        'Dim dt As DataTable

                        'dt = clsDBFuncationality.GetDataTable(intCount)
                        'Dim CheckUOM As String = ""
                        'If dt.Rows.Count > 0 Then
                        '    For Each dr1 As DataRow In dt.Rows
                        '        Dim check As String = "'" & dr1.Item("UOM_Code") & "'"
                        '        If clsCommon.CompairString(check, struom) <> CompairStringResult.Equal Then
                        '            Throw New Exception("Unit Code Is Invalid Or Does Not Exist")
                        '        End If
                        '    Next
                        'End If
                    End If

                    Dim strDescription As String = grow.Cells("Description").Value.ToString()
                    If strDescription.Length > 300 Then
                        Throw New Exception("Length of Description can not be greater than 300")
                    End If

                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Quantity").Value)

                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE()
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE()
                    Dim rmk As String = ""
                    Dim commt As String = ""
                    Dim Fatper As Double = clsCommon.myCdbl(grow.Cells("FAT %").Value)
                    Dim snfper As Double = clsCommon.myCdbl(grow.Cells("SNF %").Value)
                    Dim FatKG As Double = 0
                    Dim srnKG As Double = 0
                    Dim PriceType As String = clsCommon.myCstr(grow.Cells("Price Type").Value)
                    Dim PriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                    Dim fatR As Double = 0
                    Dim snfR As Double = 0
                    Dim FatAmt As Double = 0
                    Dim snfAmt As Double = 0
                    Dim FatRate As Double = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                    Dim SnfRate As Double = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)



                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    'If line = 1 Then
                    obj.Adjustment_No = strAdcode
                    obj.Adjustment_Date = strADate
                    obj.Reference = ""
                    obj.Description = ""
                    obj.Unit_Code = "ALL"
                    obj.ItemType = strIType
                    obj.Loc_Code = strLoc
                    obj.Loc_Desc = strLocDesc
                    obj.MainLocationCode = strMainLoc
                    obj.MainLocationDesc = strMainLocDesc
                    obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                    obj.IsMilkType = MilkType
                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    ElseIf clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    End If
                    If strTransactionType = "Adjustment" Then
                        obj.Adjustment_Type = "ADJ"
                    ElseIf strTransactionType = "Flushing" Then
                        obj.Adjustment_Type = "FLG"
                    ElseIf strTransactionType = "Opening" Then
                        obj.Adjustment_Type = "OPG"
                    ElseIf strTransactionType = "Closing" Then
                        obj.Adjustment_Type = "CLG"
                    ElseIf strTransactionType = "Auto Adjustment" Then
                        obj.Adjustment_Type = "AAD"
                    ElseIf strTransactionType = "Other" Then
                        obj.Adjustment_Type = "OTH"
                    End If

                    Dim objTr As New ClsAdjustmentsQCCDetails()
                    If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    Else
                        objTr.Price_Type = "None"
                    End If
                    'objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = AdjType
                    objTr.Item_Quantity = Iqty

                    If clsCommon.myCdbl(grow.Cells("Item Cost").Value) <= 0 Then
                        objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'"))
                    Else
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                    End If

                    objTr.Item_Cost = Iqty * objTr.Unit_Cost
                    objTr.Unit_Code = struom
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP
                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty
                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate
                    objTr.ItemType = strIType
                    obj.ItemType = strIType
                    obj.Description = strDescription
                    objTr.fat_pers = Fatper
                    objTr.snf_pers = snfper
                    FatKG = (Iqty * Fatper) / 100
                    srnKG = (Iqty * snfper) / 100
                    objTr.fat_kg = FatKG
                    objTr.snf_kg = srnKG
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    If (Fatper) <= 0 Then
                        Throw New Exception("Please Fill Fat%")
                    End If
                    If (snfper) <= 0 Then
                        Throw New Exception("Please Fill Snf%")
                    End If
                    If (clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal) AndAlso Iqty <= 0 Then
                        Throw New Exception("Please Fill quantity")
                    End If
                    If (clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(grow.Cells("Amount").Value) <= 0 Then
                        Throw New Exception("Please Fill Amount ")
                    End If

                    If clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        'Throw New Exception("Please fill Price Type")
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            Throw New Exception("Please fill MCC Price Code")
                        End If

                    ElseIf clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            Throw New Exception("Please fill Bulk Price Code ")
                        End If

                    End If

                    'line = line + 1
                    Dim qry As String = ""
                    Dim index As Integer = 0
                    If clsCommon.CompairString(obj.Trans_Type, "IN") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then

                            qry = "select count(Price_Code) from TSPL_Bulk_Price_MASTER where Price_Code='" + PriceCode + "'"
                            index = clsDBFuncationality.getSingleValue(qry, trans)
                            If index <= 0 Then
                                Throw New Exception("Filled Price Code Is Invalid Or Does Not Exist")
                            End If
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Unit_Cost = GetMilkRateImport(PriceType, PriceCode, FatKG, srnKG, Iqty)
                                objTr.Item_Cost = Iqty * objTr.Unit_Cost
                                Dim arr As New clsFatSnfRateCalculator

                                objTr.Price_Type = "Bulk"
                                objTr.Bulk_Price_Code = PriceCode
                                Dim objPrice As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(objTr.Bulk_Price_Code, NavigatorType.Current, trans)
                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate), objTr.fat_pers, objTr.snf_pers)
                                Else
                                    If clsCommon.myCdbl(objPrice.Fat_Percentage) = objTr.fat_pers And clsCommon.myCdbl(objPrice.Snf_Percentage) = objTr.snf_pers Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), objTr.fat_pers, objTr.snf_pers, clsCommon.myCdbl(objPrice.Standard_Rate), objTr.Unit_Cost)
                                    End If
                                End If


                                objTr.fat_Rate = Math.Round(arr.fatR, 2)
                                objTr.fat_Amt = Math.Round(arr.FatAmt, 2)
                                objTr.snf_Rate = Math.Round(arr.snfR, 2)
                                objTr.snf_Amt = Math.Round(arr.snfAmt, 2)
                                arr = Nothing

                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "Bulk"
                                objTr.Bulk_Price_Code = PriceCode
                                objTr.Item_Quantity = 0
                                objTr.Unit_Cost = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                            qry = " select count(Code) from (select distinct TSPL_MILK_PRICE_MASTER.Price_Code as Code,TSPL_MILK_PRICE_MASTER.Effective_Date as [Price Date], TSPL_MILK_PRICE_MASTER.Description,TSPL_MILK_PRICE_MASTER.Ratio as [Fat Ratio],TSPL_MILK_PRICE_MASTER.SNF_Ratio as [SNF Ratio], TSPL_MILK_PRICE_MASTER.FAT_Pers as [Fat %],TSPL_MILK_PRICE_MASTER.SNF_Pers as [SNF %],TSPL_MILK_PRICE_MASTER.Milk_Rate as [Milk Rate]  from TSPL_MILK_PRICE_MASTER where Price_Code in (select Distinct Price_Code from tspl_Fat_SNf_Uploader_Master inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.code where Mcc_Code='" + strMainLoc + "' and TSPL_MILK_PRICE_MASTER.Price_Code = '" + PriceCode + "')) Price"
                            index = clsDBFuncationality.getSingleValue(qry, trans)
                            If index <= 0 Then
                                Throw New Exception("Filled Price Code Is Invalid Or Does Not Exist")
                            End If
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Unit_Cost = GetMilkRateImport(PriceType, PriceCode, FatKG, srnKG, Iqty)
                                objTr.Item_Cost = Iqty * objTr.Unit_Cost
                                Dim arr As New clsFatSnfRateCalculator


                                objTr.Price_Type = "MCC"
                                objTr.MCC_Price_Code = PriceCode
                                Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" + objTr.MCC_Price_Code + "'", trans)
                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.fat_pers, objTr.snf_pers)
                                Else
                                    If clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")) = objTr.fat_pers And clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Pers")) = objTr.snf_pers Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), objTr.fat_pers, objTr.snf_pers, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.Unit_Cost)
                                    End If
                                End If



                                objTr.fat_Rate = Math.Round(arr.fatR, 2)
                                objTr.fat_Amt = Math.Round(arr.FatAmt, 2)
                                objTr.snf_Rate = Math.Round(arr.snfR, 2)
                                objTr.snf_Amt = Math.Round(arr.snfAmt, 2)
                                dtMilkPrice = Nothing
                                arr = Nothing

                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "MCC"
                                objTr.Item_Quantity = 0
                                objTr.MCC_Price_Code = PriceCode
                                objTr.Unit_Cost = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        Else
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then


                                objTr.Price_Type = "None"
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                                objTr.fat_Rate = FatRate
                                objTr.snf_Rate = SnfRate
                                objTr.Item_Cost = Iqty * objTr.Unit_Cost
                                objTr.fat_Amt = objTr.fat_kg * objTr.fat_Rate
                                objTr.snf_Amt = objTr.snf_kg * objTr.snf_Rate
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                objTr.Item_Quantity = 0
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        End If
                    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then

                        ''For RM Other balance Qty check And works only for one unit.
                        Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_Code, Nothing)
                        Dim dblBalQty As Double
                        ''richa agarwal 28/02/2016 apply tolerance limit BM00000007217
                        dblBalQty = clsInventoryMovementNew.getBalance(objTr.Item_Code, strMainLoc, strLoc, obj.Adjustment_No, obj.Adjustment_Date, Nothing, objTr.Unit_Code)
                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                            If dblBalQty > 0 Then
                                dblBalQty = ClsLoadingTanker.GetTolerane(dblBalQty, objTr.Item_Quantity)
                            End If
                        End If
                        ''-------------------------

                        Dim dblEnteredQty As Double = objTr.Item_Quantity
                        Dim strICodeInner As String = objTr.Item_Code
                        Dim strUOMInner As String = objTr.Unit_Code
                        Dim dblQtyInner As Double = objTr.Item_Quantity
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, objTr.Item_Code) = CompairStringResult.Equal Then
                            dblEnteredQty = dblQtyInner
                        End If

                        dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                        If dblEnteredQty > dblBalQty Then
                            If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
                                Throw New Exception("Item - " + ItemCode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                            End If
                        End If
                        If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal OrElse clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Price_Type = clsCommon.myCstr(grow.Cells("Price Type").Value)
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost("MI", objTr.Item_Code, strLoc, objTr.Item_Quantity, objTr.Unit_Code, objTr.fat_kg, objTr.snf_kg, strADate, strADate, True, Nothing)

                                objTr.fat_Rate = objCost.FAT_Cost / IIf(objTr.fat_kg <= 0, 1, objTr.fat_kg)
                                objTr.fat_Amt = objCost.FAT_Cost
                                objTr.snf_Rate = objCost.FAT_Cost / IIf(objTr.snf_kg <= 0, 1, objTr.snf_kg)
                                objTr.snf_Amt = objCost.SNF_Cost
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = clsCommon.myCstr(grow.Cells("Price Type").Value)
                                objTr.Item_Quantity = 0
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                            End If

                        Else
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost("MI", objTr.Item_Code, strLoc, objTr.Item_Quantity, objTr.Unit_Code, objTr.fat_kg, objTr.snf_kg, strADate, strADate, True, Nothing)

                                objTr.fat_Rate = objCost.FAT_Cost / IIf(objTr.fat_kg <= 0, 1, objTr.fat_kg)
                                objTr.fat_Amt = objCost.FAT_Cost
                                objTr.snf_Rate = objCost.FAT_Cost / IIf(objTr.snf_kg <= 0, 1, objTr.snf_kg)
                                objTr.snf_Amt = objCost.SNF_Cost
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                objTr.Item_Quantity = 0
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                            End If
                        End If
                    End If
                    Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)
                Next

                clsCommon.ProgressBarHide()
                RadMessageBox.Show("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Exporttoexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Exporttoexcel.Click
        Try
            Dim qryExport As String
            qryExport = " Select '' as [Location], '' as [Adjustment Date], '' as [Item Code],'' as [unit code], '' as [Quantity], '' as [Item Cost], '' as [Amount], 'N' as [Third Party Location],'BI' as [Adjustment Type(BD/BI/CD/CI/QD/QI)],'' as Description,'' as [Batch],'' as [Batch Mfg Date],'' as [Batch Exp Date],'' as [Batch Qty],'' as [Transaction Type]"
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub

    Private Sub Opening_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningExcel.Click
        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Dim arrExistBatchItem As New List(Of String)
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Location", "Adjustment Date", "Item Code", "unit code", "Quantity", "Item Cost", "Amount", "Third Party Location", "Adjustment Type(BD/BI/CD/CI/QD/QI)", "Description", "Batch", "Batch Mfg Date", "Batch Exp Date", "Batch Qty", "Transaction Type") Then
            Try
                Dim obj As New ClsAdjustmentsQCC()
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                Dim strAdcode As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strIType As String = "RM"
                    Dim strLoc As String = grow.Cells("Location").Value.ToString()
                    If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                        Throw New Exception("Check the value for Location")
                    End If
                    Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ")
                    Dim strADate As String = clsCommon.GetPrintDate(grow.Cells("Adjustment Date").Value, "yyyy/MM/dd")
                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

                    Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code")
                    End If
                    Dim ItemCodeNEw As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'"))
                    If clsCommon.myLen(ItemCodeNEw) <= 0 Then
                        Throw New Exception("Item Code " + ItemCode + " is not exits in item master")
                    Else
                        ItemCode = ItemCodeNEw
                    End If

                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = "BI"
                    strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, Nothing))
                    If clsCommon.myLen(strIType) <= 0 Then
                        strIType = "RM"
                    End If
                    '------------------------------------------------------------------------------------------------
                    Dim thirdparty As String = ""
                    thirdparty = clsCommon.myCstr(grow.Cells("Third Party Location").Value.ToString().ToUpper())

                    If Not clsCommon.CompairString(thirdparty, "N") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(thirdparty, "Y") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be N or Y In ColumnName [Third Party Location]")
                    End If

                    AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type(BD/BI/CD/CI/QD/QI)").Value.ToString().ToUpper())

                    If clsCommon.myLen(AdjType) <= 0 Then
                        Throw New Exception("Please Fill Adjustment Type In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If
                    If Not clsCommon.CompairString(AdjType, "BD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be any from BD/BI/CD/CI/QD/QI In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If

                    obj.chklocation = thirdparty
                    'Adjustment Type(BD/BI/CD/CI/QD/QI)
                    '--------------------------------------------------------------------------------------------------

                    '-------------------------------
                    Dim struom As String = grow.Cells("unit code").Value.ToString()
                    If clsCommon.myLen(struom) = 0 Then
                        struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Else
                        Dim intCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(unit_code) from tspl_unit_master where unit_code='" & struom & "'"))
                        If intCount = 0 Then
                            Throw New Exception("Unit code is not correct")
                        End If
                    End If
                    '==============update by preeti gupta against ticket no[]
                    Dim strDescription As String = grow.Cells("Description").Value.ToString()
                    If strDescription.Length > 300 Then
                        Throw New Exception("Length of Description can not be greater than 300")
                    End If
                    'obj.Description = strDescription
                    '========================================================
                    ' Dim struom As String = "UNIT"
                    '---------------------------------------------------------------------------------------------------
                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Quantity").Value)
                    '====================================Preeti=======================
                    Dim strTransactionType As String = ""
                    strTransactionType = clsCommon.myCstr(grow.Cells("Transaction Type").Value.ToString())
                    If clsCommon.myLen(strTransactionType) <= 0 Then
                        Throw New Exception("Enter Transaction Type")
                    End If
                    If Not clsCommon.CompairString(strTransactionType, "Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Flushing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Opening") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Closing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Auto Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Other") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be Adjustment/Flushing/Opening/Closing/Auto Adjustment/Other In ColumnName [Transaction Type]")
                    End If
                    '=====================================================================

                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE()
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE()
                    Dim rmk As String = ""
                    Dim commt As String = ""
                    Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    If line = 1 Then
                        ''started by priti
                        obj.Adjustment_No = strAdcode
                        obj.Adjustment_Date = strADate
                        'obj.Posting_Date
                        obj.Reference = ""
                        obj.Description = ""
                        'obj.Posted()
                        obj.Unit_Code = "ALL"
                        obj.ItemType = strIType
                        obj.Loc_Code = strLoc
                        obj.Loc_Desc = strLocDesc
                        obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                        '' ended by priti

                        If strTransactionType = "Adjustment" Then
                            obj.Adjustment_Type = "ADJ"
                        ElseIf strTransactionType = "Flushing" Then
                            obj.Adjustment_Type = "FLG"
                        ElseIf strTransactionType = "Opening" Then
                            obj.Adjustment_Type = "OPG"
                        ElseIf strTransactionType = "Closing" Then
                            obj.Adjustment_Type = "CLG"
                        ElseIf strTransactionType = "Auto Adjustment" Then
                            obj.Adjustment_Type = "AAD"
                        ElseIf strTransactionType = "Other" Then
                            obj.Adjustment_Type = "OTH"
                        End If

                    End If

                    Dim objTr As New ClsAdjustmentsQCCDetails()

                    'objTr.Adjustment_No = ""
                    objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = AdjType
                    'objTr.Location_Code=Pick in SaveData from header
                    objTr.Item_Quantity = Iqty
                    If clsCommon.myCdbl(grow.Cells("Item Cost").Value) <= 0 Then
                        objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'"))
                    Else
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                    End If

                    objTr.Item_Cost = Iqty * objTr.Unit_Cost
                    objTr.Unit_Code = struom
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP

                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty

                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate

                    objTr.ItemType = strIType
                    obj.ItemType = strIType
                    obj.Description = strDescription
                    Dim isAddItem As Boolean = True
                    If clsItemMaster.IsBatchItem(objTr.Item_Code) Then
                        If clsCommon.myLen(grow.Cells("Batch").Value) > 0 Then
                            Dim objBatch As New clsBatchInventory
                            objBatch.Batch_No = clsCommon.myCstr(grow.Cells("Batch").Value)
                            objBatch.Manual_BatchNo = objBatch.Batch_No
                            If clsCommon.myLen(grow.Cells("Batch Mfg Date").Value) <= 0 Then
                                Throw New Exception("Please provide Mfg Date Of Batch " + objBatch.Batch_No)
                            End If
                            objBatch.Manufacture_Date = clsCommon.myCDate(grow.Cells("Batch Mfg Date").Value)
                            If clsCommon.myLen(grow.Cells("Batch Exp Date").Value) <= 0 Then
                                Throw New Exception("Please provide Expiry Date Of Batch " + objBatch.Batch_No)
                            End If
                            objBatch.Expiry_Date = clsCommon.myCDate(grow.Cells("Batch Exp Date").Value)
                            objBatch.Qty = clsCommon.myCdbl(grow.Cells("Batch Qty").Value)
                            If arrExistBatchItem.Contains(objTr.Item_Code) Then
                                For kk As Integer = 0 To obj.Arr.Count - 1
                                    If clsCommon.CompairString(obj.Arr(kk).Item_Code, objTr.Item_Code) = CompairStringResult.Equal Then
                                        obj.Arr(kk).arrBatchItem.Add(objBatch)
                                        Exit For
                                    End If
                                Next
                                Continue For
                            Else
                                arrExistBatchItem.Add(objTr.Item_Code)
                                objTr.arrBatchItem = New List(Of clsBatchInventory)
                                objTr.arrBatchItem.Add(objBatch)
                            End If
                        Else
                            Throw New Exception("Please provide Batch detail for item " + objTr.Item_Code)
                        End If
                    End If
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    line = line + 1
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                If arrExistBatchItem.Count > 0 Then
                    For kk As Integer = 0 To obj.Arr.Count - 1
                        If obj.Arr(kk).arrBatchItem IsNot Nothing AndAlso obj.Arr(kk).arrBatchItem.Count > 0 Then
                            Dim TotBatQty As Decimal = 0
                            Dim TotQty As Decimal = 0
                            For ll As Integer = 0 To obj.Arr(kk).arrBatchItem.Count - 1
                                TotBatQty += obj.Arr(kk).arrBatchItem(ll).Qty
                            Next
                            If TotBatQty <> obj.Arr(kk).Item_Quantity Then
                                Throw New Exception("item " + obj.Arr(kk).Item_Code + " Quantity " + clsCommon.myCstr(obj.Arr(kk).Item_Quantity) + " Batch total Quantity " + clsCommon.myCstr(TotBatQty))
                            End If
                        End If
                    Next
                End If
                Dim isSaved As Boolean = obj.SaveData(obj, True)
                RadMessageBox.Show("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    Private Sub cboAdjustmentType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboAdjustmentType.SelectedValueChanged
        If Not isInsideLoadData Then
            If clsCommon.CompairString(cboAdjustmentType.SelectedValue, "OTH") = CompairStringResult.Equal Then
                txtSpecification.Enabled = True
            Else
                txtSpecification.Enabled = False
                txtSpecification.Text = ""
            End If

        End If
    End Sub

#Region "Correction -ve Old stock"

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        ''Below function read line by line code from excel and collect closing stock of system as per entered adjustment date.
        ''And make new OUT entry first of OLD stock and then make new IN entry of excel line record.For matching up of data.
        ''It breaks the OLD closing stock into 2 parts, if required, 1. Quantity,amount and fat/snf is +ve then 1 entry for out.
        ''2. Quantity,amount and fat/snf any 1 is -ve then 1 entry for in.

        If clsCommon.MyMessageBoxShow("This uploader knock-off closing of entered adjustment date " + Environment.NewLine + "and make new entry of excel provided opening." + Environment.NewLine + "Want to continue with the procedure?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "AdjDate", "Main Location Code", "Location Code", "Inout", "Adjustment Type", "Item Code", "UOM", "Qty", "Rate", "Amount", "Fat%", "Fat KG", "SNF%", "SNF KG", "Transaction Type") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim obj As New ClsAdjustmentsQCC()
                clsCommon.ProgressBarShow()
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()

                Dim Checkqry As String = ""

                Dim countindex As Integer = 1
                Dim strAdcode As String = ""
                Dim strLocDesc As String = ""
                Dim Adjust_Date As String = ""
                '' get list of location 
                Dim arrItem As New ArrayList
                Dim arrLoc As New ArrayList
                For Each grow As GridViewRowInfo In gv.Rows
                    If arrLoc.Contains(clsCommon.myCstr(grow.Cells("Location Code").Value)) = False Then
                        Adjust_Date = clsCommon.GetPrintDate(grow.Cells("AdjDate").Value, "dd-MMM-yyyy")
                        arrLoc.Add(grow.Cells("Location Code").Value.ToString())
                    End If
                Next
                If clsCommon.myLen(Adjust_Date) <= 0 Then
                    Adjust_Date = "31-Mar-2017"
                End If
                For Each Loc As String In arrLoc
                    arrItem = New ArrayList
                    clsCommon.ProgressBarUpdate("Loc:" & Loc & "- " & (arrLoc.IndexOf(Loc) + 1) & "/" & arrLoc.Count)
                    For Each grow As GridViewRowInfo In gv.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Location Code").Value), Loc) = CompairStringResult.Equal AndAlso arrItem.Contains(clsCommon.myCstr(grow.Cells("Item Code").Value)) = False Then
                            arrItem.Add(grow.Cells("Item Code").Value.ToString())
                        End If
                    Next
                    clsCommon.ProgressBarUpdate("Loc:" & Loc & "- " & (arrLoc.IndexOf(Loc) + 1) & "/" & arrLoc.Count)
                    OutCurrentStock(Loc, arrItem, Adjust_Date, trans)
                Next



                For Each grow As GridViewRowInfo In gv.Rows
                    clsCommon.ProgressBarUpdate((grow.Index + 1) & "/" & gv.Rows.Count)
                    obj = New ClsAdjustmentsQCC()
                    obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                    Dim MilkType As Integer = 1
                    Dim strIType As String = "RM"
                    '===================
                    Dim strMainLoc As String = grow.Cells("Main Location Code").Value.ToString()
                    If clsCommon.myLen(strMainLoc) > 0 Then
                        Checkqry = "select count(Location_Code) from tspl_location_master where Location_Code='" + strMainLoc + "'"
                        countindex = clsDBFuncationality.getSingleValue(Checkqry, trans)
                        If (countindex) <= 0 Then
                            Throw New Exception("Main Location Code Is Invalid Or Does Not Exist")
                        End If
                    End If

                    Dim strMainLocDesc As String = clsLocation.GetName(strMainLoc, trans)

                    Dim strLoc As String = grow.Cells("Location Code").Value.ToString()
                    Dim Checkqry1 As String = ""
                    Dim qry6 As String = clsDBFuncationality.getSingleValue("select Location_Category  from tspl_location_master where Location_Code = '" + strMainLoc + "'", trans)
                    If clsCommon.CompairString(qry6, "MCC") <> CompairStringResult.Equal Then
                        If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                            Throw New Exception("Check the value for Location")
                        End If
                    End If
                    If clsCommon.myLen(strLoc) > 0 And clsCommon.CompairString(strMainLoc, strLoc) <> CompairStringResult.Equal Then
                        Checkqry1 = clsDBFuncationality.getSingleValue("select (Loc_Segment_Code) from tspl_location_master where Location_Code='" + strMainLoc + "' ", trans)
                        If clsCommon.myLen(Checkqry1) > 0 Then

                            Dim Checkqry2 As String = "select count(Location_Code) from TSPL_LOCATION_MASTER where Loc_Segment_Code ='" + Checkqry1 + "'  and (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') "
                            Dim countindex1 As Integer = clsDBFuncationality.getSingleValue(Checkqry2, trans)
                            If (countindex1) <= 0 Then
                                Throw New Exception("Sub Location Code Is Invalid Or Does Not Exist")
                            End If
                            If clsCommon.myLen(Checkqry2) > 0 Then
                                strLocDesc = clsLocation.GetName(strLoc, trans)
                            End If
                        End If
                    End If

                    '=========================================================
                    Dim InOut As String = grow.Cells("InOut").Value.ToString()
                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "OUT") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Please Insert In or Out type")
                    End If

                    If grow.Cells("AdjDate").Value Is Nothing OrElse clsCommon.myLen(grow.Cells("AdjDate").Value) <= 0 Then
                        Throw New Exception("Please fill date for adjustment.")
                    End If

                    If Not IsDate(grow.Cells("AdjDate").Value) Then
                        Throw New Exception("Adjustment Date should be date in DD/MM/YYYY format.")
                    End If

                    Dim strADate As String = clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("AdjDate").Value), "yyyy/MM/dd")



                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    '====================================Preeti=======================
                    Dim strTransactionType As String = ""
                    strTransactionType = clsCommon.myCstr(grow.Cells("Transaction Type").Value.ToString())
                    If clsCommon.myLen(strTransactionType) <= 0 Then
                        Throw New Exception("Enter Transaction Type")
                    End If
                    If Not clsCommon.CompairString(strTransactionType, "Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Flushing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Opening") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Closing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Auto Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Other") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be Adjustment/Flushing/Opening/Closing/Auto Adjustment/Other In ColumnName [Transaction Type]")
                    End If
                    '===============================
                    Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code")
                    End If
                    Dim checkitem As String = "select count(Item_code) from TSPL_ITEM_MASTER where Item_Code = '" + ItemCode + "' and Product_Type ='MI'"
                    Dim CountItem As Integer = clsDBFuncationality.getSingleValue(checkitem, trans)
                    If (CountItem) <= 0 Then
                        strMainLoc = Nothing
                        strMainLocDesc = Nothing
                    End If
                    Dim ItemDesc As String = clsItemMaster.GetItemName(ItemCode, trans)
                    '===========================================================

                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')", trans)
                    Dim AdjType As String = ""
                    strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, trans))
                    If clsCommon.myLen(strIType) <= 0 Then
                        strIType = "RM"
                    End If

                    AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type").Value)

                    If clsCommon.myLen(AdjType) <= 0 Then
                        Throw New Exception("Please fill adjustment type.")
                    End If

                    If clsCommon.CompairString(AdjType, "Quantity") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(AdjType, "Cost") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(AdjType, "Both") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(AdjType, "FAT/SNF") <> CompairStringResult.Equal Then
                        Throw New Exception("Adjustment type should be Quantity,Cost,Both or FAT/SNF.")
                    End If


                    Dim struom As String = grow.Cells("UOM").Value.ToString()

                    If clsCommon.myLen(struom) = 0 Then
                        struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
                    Else
                        Dim intCount As String = "select count(UOM_Code) from TSPL_ITEM_UOM_DETAIL where Item_code='" & ItemCode & "'"
                        If clsCommon.myLen(intCount) > 0 Then
                            Dim Checkqry3 As String = "select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_code='" + ItemCode + "' and UOM_Code = '" + struom + "' "
                            Dim countindex3 As String = clsDBFuncationality.getSingleValue(Checkqry3, trans)
                            If clsCommon.myLen(countindex3) <= 0 Then
                                Throw New Exception("Unit Code Is Invalid Or Does Not Exist")
                            End If

                        End If
                    End If

                    Dim strDescription As String = "Auto adjustment for matching opening balance of new financial year."

                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Qty").Value)

                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE(trans)
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE(trans)
                    Dim rmk As String = ""
                    Dim commt As String = ""
                    Dim Fatper As Double = clsCommon.myCdbl(grow.Cells("FAT%").Value)
                    Dim snfper As Double = clsCommon.myCdbl(grow.Cells("SNF%").Value)
                    Dim FatKG As Double = clsCommon.myCdbl(grow.Cells("FAT Kg").Value)
                    Dim SNFKG As Double = clsCommon.myCdbl(grow.Cells("SNF Kg").Value)
                    Dim PriceType As String = Nothing
                    Dim PriceCode As String = Nothing
                    Dim fatR As Double = 0
                    Dim snfR As Double = 0
                    Dim FatAmt As Double = 0
                    Dim snfAmt As Double = 0
                    Dim FatRate As Double = 0
                    Dim SnfRate As Double = 0


                    ''====================check for old stock knock-off==========================
                    'GetOldStockData_OutEntry(strMainLoc, strLoc, ItemCode, strADate, account, strIType, trans)
                    ''=====================================================================

                    obj = New ClsAdjustmentsQCC()
                    obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
                    'If line = 1 Then
                    obj.Adjustment_No = strAdcode
                    obj.Adjustment_Date = strADate
                    obj.Reference = ""
                    obj.Description = strDescription
                    obj.Unit_Code = "ALL"
                    obj.ItemType = strIType
                    obj.Loc_Code = strLoc
                    obj.Loc_Desc = strLocDesc
                    obj.MainLocationCode = strMainLoc
                    obj.MainLocationDesc = strMainLocDesc
                    obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                    obj.Adjustment_Type = "AAD"
                    obj.IsMilkType = IIf(clsCommon.myLen(obj.MainLocationCode) > 0, 1, 0) ' MilkType

                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    ElseIf clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    End If
                    If strTransactionType = "Adjustment" Then
                        obj.Adjustment_Type = "ADJ"
                    ElseIf strTransactionType = "Flushing" Then
                        obj.Adjustment_Type = "FLG"
                    ElseIf strTransactionType = "Opening" Then
                        obj.Adjustment_Type = "OPG"
                    ElseIf strTransactionType = "Closing" Then
                        obj.Adjustment_Type = "CLG"
                    ElseIf strTransactionType = "Auto Adjustment" Then
                        obj.Adjustment_Type = "AAD"
                    ElseIf strTransactionType = "Other" Then
                        obj.Adjustment_Type = "OTH"
                    End If

                    Dim objTr As New ClsAdjustmentsQCCDetails()
                    If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    Else
                        objTr.Price_Type = "None"
                    End If
                    'objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = clsCommon.myCstr(AdjType).Substring(0, 1) + IIf(clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal, "I", "D")
                    objTr.Item_Quantity = Iqty

                    If clsCommon.myCdbl(grow.Cells("Rate").Value) <= 0 Then
                        objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans))
                    Else
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                    End If

                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    objTr.Unit_Code = struom
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP
                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty
                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate
                    objTr.ItemType = strIType
                    obj.ItemType = strIType
                    obj.Description = strDescription
                    objTr.fat_pers = Fatper
                    objTr.snf_pers = snfper
                    'FatKG = (Iqty * Fatper) / 100
                    'srnKG = (Iqty * snfper) / 100
                    objTr.fat_kg = FatKG
                    objTr.snf_kg = SNFKG


                    If (clsCommon.CompairString(AdjType, "Quantity") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "Both") = CompairStringResult.Equal) AndAlso Iqty <= 0 Then
                        'Throw New Exception("Please Fill quantity")
                        Continue For
                    End If
                    If (clsCommon.CompairString(AdjType, "Cost") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "Both") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(grow.Cells("Amount").Value) <= 0 Then
                        'Throw New Exception("Please Fill Amount ")
                        Continue For
                    End If
                    If clsCommon.CompairString(AdjType, "FAT/SNF") = CompairStringResult.Equal AndAlso FatKG <= 0 AndAlso SNFKG <= 0 Then
                        'Throw New Exception("Please Fill FAT/SNF value.")
                        Continue For
                    End If

                    If clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        'Throw New Exception("Please fill Price Type")
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            'Throw New Exception("Please fill MCC Price Code")
                            Continue For
                        End If

                    ElseIf clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            'Throw New Exception("Please fill Bulk Price Code ")
                            Continue For
                        End If

                    End If


                    'line = line + 1
                    Dim xnewadjtmnt = AdjType.Substring(0, 1) + IIf(clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal, "I", "D")
                    AdjType = xnewadjtmnt
                    Dim qry As String = ""
                    Dim index As Integer = 0
                    If clsCommon.CompairString(obj.Trans_Type, "IN") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then

                        ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then

                        Else
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                                objTr.fat_Rate = FatRate
                                objTr.snf_Rate = SnfRate
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_Amt = objTr.fat_kg * objTr.fat_Rate
                                objTr.snf_Amt = objTr.snf_kg * objTr.snf_Rate
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                'objTr.Item_Quantity = 0
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                'objTr.fat_kg = 0
                                'objTr.snf_kg = 0
                                objTr.fat_Rate = FatRate
                                objTr.snf_Rate = SnfRate
                            End If
                        End If
                    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then

                    End If

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)

                    ClsAdjustmentsQCC.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                RadMessageBox.Show("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message)
                trans.Rollback()
                clsCommon.ProgressBarHide()
            End Try

        End If
    End Sub
    Private Sub OutCurrentStock(ByVal Loc_Code As String, ByVal arrItem As ArrayList, ByVal Adjustment_Date As DateTime, ByVal trans As SqlTransaction)
        ''ByVal Account_Code As String, ByVal Item_Type As String
        Dim qry As String = ""
        Dim qryCTE As String = ""
        Dim qryPart2 As String = ""
        Dim dt As New DataTable()
        Dim obj As New ClsAdjustmentsQCC()
        Dim objtr As New ClsAdjustmentsQCCDetails()
        Dim Item_Type As String = ""
        Dim Main_Loc_Code As String = ""
        Try
            '' where convert(date,tspl_inv_move_dl.trans_date,103)= (select max(trans_date) as trans_date from TSPL_INV_MOVE_DL where convert(date,tspl_inv_move_dl.trans_date,103)<='" + clsCommon.GetPrintDate(Adjustment_Date, "dd/MMM/yyyy") + "' and tspl_inv_move_dl.item_code='" + Item_Code + "' and tspl_inv_move_dl.location_code='" + Loc_Code + "')
            ''"and tspl_inv_move_dl.item_code='" & Item_Code & "' and tspl_inv_move_dl.location_code='" & Loc_Code & "') " & Environment.NewLine & _
            qryCTE = " With Stock_Closing as " &
                     " (select tspl_inv_move_dl.Item_Code,tspl_inv_move_dl.Product_Type,tspl_inv_move_dl.Item_Type,tspl_inv_move_dl.location_code,tspl_inv_move_dl.Stock_UOM,cast(tspl_inv_move_dl.CL_QTY as decimal(28,3)) as op_qty,case when isnull(tspl_inv_move_dl.cl_qty,0)=0 then 0 else cast(tspl_inv_move_dl.cl_avg_cost/tspl_inv_move_dl.cl_qty as decimal(28,3)) end as Op_Rate,cast(tspl_inv_move_dl.CL_Avg_Cost as decimal(28,3)) as op_cost " &
                     " ,case when isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)>0 and isnull(tspl_inv_move_dl.CL_QTY,0)>0 then cast((round(tspl_inv_move_dl.CL_FAT_KG,2) * 100) / (tspl_inv_move_dl.CL_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as decimal(28,3)) end as op_fat_per,cast(round(tspl_inv_move_dl.CL_FAT_KG,2) as decimal(28,3)) as op_fat_kg, " &
                     " case when isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)>0 and isnull(tspl_inv_move_dl.CL_QTY,0)>0 then cast((tspl_inv_move_dl.CL_SNF_KG * 100) / (tspl_inv_move_dl.CL_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as decimal(28,3)) end as op_snf_per,cast(tspl_inv_move_dl.CL_SNF_KG as decimal(28,3)) as op_snf_kg " &
                     " from (select TSPL_INV_MOVE_DL.Item_Code,TSPL_ITEM_MASTER.Product_Type,TSPL_ITEM_MASTER.Item_Type,TSPL_INV_MOVE_DL.Location_Code,TSPL_INV_MOVE_DL.Stock_UOM,TSPL_INV_MOVE_DL.TRANS_DATE," &
                     " TSPL_INV_MOVE_DL.CL_QTY, TSPL_INV_MOVE_DL.CL_Avg_Cost,round(TSPL_INV_MOVE_DL.CL_FAT_KG,2) as CL_FAT_KG, round(TSPL_INV_MOVE_DL.CL_SNF_KG,2) as CL_SNF_KG " &
                     " from TSPL_INV_MOVE_DL inner join ( " &
                     " select Item_Code,Location_Code,max(TRANS_DATE) as TRANS_DATE from TSPL_INV_MOVE_DL where TRANS_DATE<='" + clsCommon.GetPrintDate(Adjustment_Date, "dd/MMM/yyyy") + "' " &
                     " group by Item_Code,Location_Code) as ItemTrans on TSPL_INV_MOVE_DL.Item_Code=ItemTrans.Item_Code and TSPL_INV_MOVE_DL.Location_Code=ItemTrans.Location_Code " &
                     " and TSPL_INV_MOVE_DL.TRANS_DATE=ItemTrans.TRANS_DATE " &
                     " inner join TSPL_ITEM_MASTER on TSPL_INV_MOVE_DL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                     " where (TSPL_INV_MOVE_DL.CL_QTY<>0 or TSPL_INV_MOVE_DL.CL_Avg_Cost<>0 or TSPL_INV_MOVE_DL.CL_FAT_KG<>0 " &
                     " or TSPL_INV_MOVE_DL.CL_SNF_KG<>0) and TSPL_INV_MOVE_DL.Location_Code='" & Loc_Code & "' and  TSPL_INV_MOVE_DL.Item_Code in (" & clsCommon.GetMulcallString(arrItem) & ")" &
                     " ) as tspl_inv_move_dl " &
                     " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INV_MOVE_DL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' )"

            qryPart2 = "select final.*,tspl_item_master.item_desc,tspl_location_master.Main_Location_Code from ( " & Environment.NewLine &
                  "select 'Out' as inout,'Quantity' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_qty>0 " &
                  "union all " &
                  "select 'In' as inout,'Quantity' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,0-isnull(op_qty,0) as op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_qty<0 " &
                  "union all " &
                  "select 'Out' as inout,'Cost' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,0 as op_qty,Op_Rate,op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_cost>0 " &
                  "union all " &
                  "select 'In' as inout,'Cost' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,0 as op_qty,case when op_rate<0 then 0-isnull(Op_Rate,0) else Op_Rate end as Op_Rate,0-isnull(op_cost,0) as op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_cost<0 " &
                  "union all " &
                  "select 'Out' as inout,'FAT/SNF' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,op_fat_per,op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_fat_kg>0 " &
                  "union all " &
                  "select 'In' as inout,'FAT/SNF' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,case when isnull(op_fat_per,0)<0 then 0-isnull(op_fat_per,0) else op_fat_per end as op_fat_per,0-isnull(op_fat_kg,0) as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_fat_kg<0 " &
                  "union all " &
                  "select 'Out' as inout,'FAT/SNF' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,op_snf_per,op_snf_kg from Stock_Closing where op_snf_kg>0 " &
                  "union all " &
                  "select 'In' as inout,'FAT/SNF' as transtype,Item_Code,Product_Type,Item_Type,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,case when isnull(op_snf_per,0)<0 then 0-isnull(op_snf_per,0) else op_snf_per end as op_snf_per,0-isnull(op_snf_kg,0) as op_snf_kg from Stock_Closing where op_snf_kg<0 " & Environment.NewLine &
                  ")final left outer join tspl_item_master on tspl_item_master.item_code=final.item_code  " &
                  " left join tspl_location_master on final.Location_Code=tspl_location_master.Location_Code"
            qry = qryCTE & Environment.NewLine & qryPart2 & " order by final.inout "

            Dim qryMain As String = qryCTE & "select distinct inout,Product_Type,Item_Type,location_code from (" & qryPart2 & ") TA order by inout,Product_Type,Item_Type"
            Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qryMain, trans)
            For Each drMain As DataRow In dtMain.Rows
                Item_Type = clsCommon.myCstr(drMain.Item("Item_Type"))
                Dim qryStock As String = qryCTE & "select * from (" & qryPart2 & ") Stock where inout='" & clsCommon.myCstr(drMain.Item("inout")) & "' and coalesce(Product_Type,'')='" & clsCommon.myCstr(drMain.Item("Product_Type")) & "' and Item_Type='" & clsCommon.myCstr(drMain.Item("Item_Type")) & "'"
                dt = clsDBFuncationality.GetDataTable(qryStock, trans)
                obj = New ClsAdjustmentsQCC()
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)

                Dim lineno As Integer = 1
                Dim dt_indx As Integer = 0

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj = New ClsAdjustmentsQCC()
                    obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                    dt_indx = 0
                    For Each dr As DataRow In dt.Rows
                        Main_Loc_Code = clsCommon.myCstr(dr.Item("Main_Location_Code"))
                        obj.Adjustment_Date = Adjustment_Date
                        obj.Adjustment_No = Nothing
                        obj.Adjustment_Specification = Nothing
                        obj.Unit_Code = "ALL"
                        obj.Adjustment_Type = "AAD" ''auto adjustment
                        obj.ItemType = Item_Type
                        obj.Description = "Auto stock adjustment entry for knock-off " + clsCommon.GetPrintDate(Adjustment_Date, "dd/MM/yyyy") + " dated stock with physical stock."
                        obj.IsMilkType = IIf(clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(dr("item_code")), trans), "MI") = CompairStringResult.Equal, 1, 0)
                        obj.Loc_Code = Loc_Code

                        obj.Loc_Desc = clsLocation.GetName(Loc_Code, trans)
                        If clsCommon.myLen(Main_Loc_Code) > 0 Then
                            obj.MainLocationCode = Main_Loc_Code
                            obj.MainLocationDesc = clsLocation.GetName(Main_Loc_Code, trans)
                        Else
                            obj.MainLocationCode = Nothing
                            obj.MainLocationDesc = Nothing
                        End If
                        obj.Trans_Type = clsCommon.myCstr(dr("inout"))
                        obj.IsMilkType = IIf(clsCommon.myLen(obj.MainLocationCode) > 0, 1, 0) ' MilkType

                        ''detail data========================
                        objtr = New ClsAdjustmentsQCCDetails()
                        objtr.Price_Type = "None"
                        objtr.Adjustment_Line_No = lineno
                        objtr.Item_Code = clsCommon.myCstr(dr("item_code"))
                        objtr.Item_Description = clsCommon.myCstr(dr("item_desc"))
                        objtr.Adjustment_Type = clsCommon.myCstr(dr("transtype")).Substring(0, 1) + IIf(clsCommon.CompairString(clsCommon.myCstr(dr("inout")), "In") = CompairStringResult.Equal, "I", "D")
                        objtr.Item_Quantity = clsCommon.myCdbl(dr("op_qty"))
                        objtr.Unit_Cost = clsCommon.myCdbl(dr("op_rate"))
                        objtr.Item_Cost = clsCommon.myCdbl(dr("op_cost"))
                        objtr.Unit_Code = clsCommon.myCstr(dr("stock_uom"))
                        objtr.Remarks = Nothing
                        objtr.Comments = Nothing
                        objtr.mrp = Nothing
                        objtr.BreakageType = Nothing
                        objtr.Breakage = Nothing
                        objtr.Breakage_Cost = Nothing
                        objtr.LeakageQty = Nothing
                        objtr.MFG_Date = Nothing
                        objtr.Batch_No = Nothing
                        objtr.Expiry_Date = Nothing
                        objtr.ItemType = Item_Type
                        objtr.fat_pers = clsCommon.myCdbl(dr("op_fat_per"))
                        objtr.snf_pers = clsCommon.myCdbl(dr("op_snf_per"))
                        objtr.fat_kg = clsCommon.myCdbl(dr("op_fat_kg"))
                        objtr.snf_kg = clsCommon.myCdbl(dr("op_snf_kg"))

                        If clsCommon.myLen(objtr.Item_Code) > 0 Then
                            obj.Arr.Add(objtr)
                        End If


                        'If dt.Rows.Count > dt_indx Then ''compare if dt has more rows and next level inout type is not match with current then upto current data is saved.
                        '    If clsCommon.CompairString(clsCommon.myCstr(dr("inout")), clsCommon.myCstr(dt.Rows(dt_indx)("inout"))) <> CompairStringResult.Equal OrElse dt.Rows.Count = dt_indx + 1 Then
                        '        Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)

                        '        ClsAdjustments.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans, False)

                        '        lineno = 0
                        '        obj = New ClsAdjustments()
                        '        obj.Arr = New List(Of ClsAdjustmentsDetails)
                        '    End If
                        'Else
                        '    Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)

                        '    ClsAdjustments.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans, False)

                        '    lineno = 0
                        '    obj = New ClsAdjustments()
                        '    obj.Arr = New List(Of ClsAdjustmentsDetails)
                        'End If

                        'dt_indx += 1
                        'lineno += 1

                    Next ''end datarow cond.
                    If obj.Arr.Count > 0 Then
                        Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)
                        ClsAdjustmentsQCC.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans, False)
                        obj = Nothing
                        objtr = Nothing
                    End If
                End If ''end datatable cond.
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = String.Empty
            dt = Nothing
        End Try
    End Sub
    Private Sub GetOldStockData_OutEntry(ByVal Main_Loc_Code As String, ByVal Loc_Code As String, ByVal Item_Code As String, ByVal Adjustment_Date As DateTime, ByVal Account_Code As String, ByVal Item_Type As String, ByVal trans As SqlTransaction)
        Dim qry As String = ""
        Dim qryCTE As String = ""
        Dim qryPart2 As String = ""
        Dim dt As New DataTable()
        Dim obj As New ClsAdjustmentsQCC()
        Dim objtr As New ClsAdjustmentsQCCDetails()
        Try
            '' where convert(date,tspl_inv_move_dl.trans_date,103)= (select max(trans_date) as trans_date from TSPL_INV_MOVE_DL where convert(date,tspl_inv_move_dl.trans_date,103)<='" + clsCommon.GetPrintDate(Adjustment_Date, "dd/MMM/yyyy") + "' and tspl_inv_move_dl.item_code='" + Item_Code + "' and tspl_inv_move_dl.location_code='" + Loc_Code + "')
            ''"and tspl_inv_move_dl.item_code='" & Item_Code & "' and tspl_inv_move_dl.location_code='" & Loc_Code & "') " & Environment.NewLine & _
            qryCTE = "With Stock_Closing as " &
                  "(select tspl_inv_move_dl.Item_Code,tspl_inv_move_dl.location_code,tspl_inv_move_dl.Stock_UOM,cast(tspl_inv_move_dl.CL_QTY as decimal(18,3)) as op_qty,case when isnull(tspl_inv_move_dl.cl_qty,0)=0 then 0 else cast(tspl_inv_move_dl.cl_avg_cost/tspl_inv_move_dl.cl_qty as decimal(18,3)) end as Op_Rate,cast(tspl_inv_move_dl.CL_Avg_Cost as decimal(18,3)) as op_cost " &
                  ",case when isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)>0 and isnull(tspl_inv_move_dl.CL_QTY,0)>0 then cast((tspl_inv_move_dl.CL_FAT_KG * 100) / (tspl_inv_move_dl.CL_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as decimal(18,3)) end as op_fat_per,cast(tspl_inv_move_dl.CL_FAT_KG as decimal(18,3)) as op_fat_kg, " &
                  "case when isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)>0 and isnull(tspl_inv_move_dl.CL_QTY,0)>0 then cast((tspl_inv_move_dl.CL_SNF_KG * 100) / (tspl_inv_move_dl.CL_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as decimal(18,3)) end as op_snf_per,cast(tspl_inv_move_dl.CL_SNF_KG as decimal(18,3)) as op_snf_kg " &
                  "from dbo.TSPL_FUN_ITEM_LOC_BALANCE('" & Item_Code & "','" & Loc_Code & "','" & clsCommon.GetPrintDate(Adjustment_Date, "dd-MMM-yyyy") & "') as tspl_inv_move_dl left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INV_MOVE_DL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' )"

            qryPart2 = "select final.*,tspl_item_master.item_desc from ( " & Environment.NewLine &
                  "select 'Out' as inout,'Quantity' as transtype,Item_Code,location_code,Stock_UOM,op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_qty>0 " &
                  "union all " &
                  "select 'In' as inout,'Quantity' as transtype,Item_Code,location_code,Stock_UOM,0-isnull(op_qty,0) as op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_qty<0 " &
                  "union all " &
                  "select 'Out' as inout,'Cost' as transtype,Item_Code,location_code,Stock_UOM,0 as op_qty,Op_Rate,op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_cost>0 " &
                  "union all " &
                  "select 'In' as inout,'Cost' as transtype,Item_Code,location_code,Stock_UOM,0 as op_qty,case when op_rate<0 then 0-isnull(Op_Rate,0) else Op_Rate end as Op_Rate,0-isnull(op_cost,0) as op_cost,0 as op_fat_per,0 as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_cost<0 " &
                  "union all " &
                  "select 'Out' as inout,'FAT/SNF' as transtype,Item_Code,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,op_fat_per,op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_fat_kg>0 " &
                  "union all " &
                  "select 'In' as inout,'FAT/SNF' as transtype,Item_Code,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,case when isnull(op_fat_per,0)<0 then 0-isnull(op_fat_per,0) else op_fat_per end as op_fat_per,0-isnull(op_fat_kg,0) as op_fat_kg,0 as op_snf_per,0 as op_snf_kg from Stock_Closing where op_fat_kg<0 " &
                  "union all " &
                  "select 'Out' as inout,'FAT/SNF' as transtype,Item_Code,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,op_snf_per,op_snf_kg from Stock_Closing where op_snf_kg>0 " &
                  "union all " &
                  "select 'In' as inout,'FAT/SNF' as transtype,Item_Code,location_code,Stock_UOM,0 as op_qty,0 as Op_Rate,0 as op_cost,0 as op_fat_per,0 as op_fat_kg,case when isnull(op_snf_per,0)<0 then 0-isnull(op_snf_per,0) else op_snf_per end as op_snf_per,0-isnull(op_snf_kg,0) as op_snf_kg from Stock_Closing where op_snf_kg<0 " & Environment.NewLine &
                  ")final left outer join tspl_item_master on tspl_item_master.item_code=final.item_code  "
            qry = qryCTE & Environment.NewLine & qryPart2 & " order by final.inout "

            Dim qryMain As String = qryCTE & "select distinct inout,Item_Code,location_code from (" & qryPart2 & ") TA"
            Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qryMain, trans)
            For Each drMain As DataRow In dtMain.Rows
                Dim qryStock As String = qryCTE & "select * from (" & qryPart2 & ") Stock where inout='" & clsCommon.myCstr(drMain.Item("inout")) & "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)

                obj = New ClsAdjustmentsQCC()
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)

                Dim lineno As Integer = 1
                Dim dt_indx As Integer = 0

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj = New ClsAdjustmentsQCC()
                    obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                    dt_indx = 0
                    For Each dr As DataRow In dt.Rows
                        obj.Adjustment_Date = Adjustment_Date
                        obj.Adjustment_No = Nothing
                        obj.Adjustment_Specification = Nothing
                        obj.Unit_Code = "ALL"
                        obj.Adjustment_Type = "AAD" ''auto adjustment
                        obj.ItemType = Item_Type
                        obj.Description = "Auto stock adjustment entry for knock-off " + clsCommon.GetPrintDate(Adjustment_Date, "dd/MM/yyyy") + " dated stock with physical stock."
                        obj.IsMilkType = IIf(clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(dr("item_code")), trans), "MI") = CompairStringResult.Equal, 1, 0)
                        obj.Loc_Code = Loc_Code
                        obj.Loc_Desc = clsLocation.GetName(Loc_Code, trans)
                        If clsCommon.myLen(Main_Loc_Code) > 0 Then
                            obj.MainLocationCode = Main_Loc_Code
                            obj.MainLocationDesc = clsLocation.GetName(Main_Loc_Code, trans)
                        Else
                            obj.MainLocationCode = Nothing
                            obj.MainLocationDesc = Nothing
                        End If
                        obj.Trans_Type = clsCommon.myCstr(dr("inout"))
                        obj.IsMilkType = IIf(clsCommon.myLen(obj.MainLocationCode) > 0, 1, 0) ' MilkType

                        ''detail data========================
                        objtr = New ClsAdjustmentsQCCDetails()
                        objtr.Price_Type = "None"
                        objtr.Adjustment_Line_No = lineno
                        objtr.Item_Code = clsCommon.myCstr(dr("item_code"))
                        objtr.Item_Description = clsCommon.myCstr(dr("item_desc"))
                        objtr.Adjustment_Type = clsCommon.myCstr(dr("transtype")).Substring(0, 1) + IIf(clsCommon.CompairString(clsCommon.myCstr(dr("inout")), "In") = CompairStringResult.Equal, "I", "D")
                        objtr.Item_Quantity = clsCommon.myCdbl(dr("op_qty"))
                        objtr.Unit_Cost = clsCommon.myCdbl(dr("op_rate"))
                        objtr.Item_Cost = clsCommon.myCdbl(dr("op_cost"))
                        objtr.Unit_Code = clsCommon.myCstr(dr("stock_uom"))
                        objtr.Remarks = Nothing
                        objtr.Comments = Nothing
                        objtr.mrp = Nothing
                        objtr.BreakageType = Nothing
                        objtr.Breakage = Nothing
                        objtr.Breakage_Cost = Nothing
                        objtr.LeakageQty = Nothing
                        objtr.MFG_Date = Nothing
                        objtr.Batch_No = Nothing
                        objtr.Expiry_Date = Nothing
                        objtr.ItemType = Item_Type
                        objtr.fat_pers = clsCommon.myCdbl(dr("op_fat_per"))
                        objtr.snf_pers = clsCommon.myCdbl(dr("op_snf_per"))
                        objtr.fat_kg = clsCommon.myCdbl(dr("op_fat_kg"))
                        objtr.snf_kg = clsCommon.myCdbl(dr("op_snf_kg"))

                        If clsCommon.myLen(objtr.Item_Code) > 0 Then
                            obj.Arr.Add(objtr)
                        End If


                        'If dt.Rows.Count > dt_indx Then ''compare if dt has more rows and next level inout type is not match with current then upto current data is saved.
                        '    If clsCommon.CompairString(clsCommon.myCstr(dr("inout")), clsCommon.myCstr(dt.Rows(dt_indx)("inout"))) <> CompairStringResult.Equal OrElse dt.Rows.Count = dt_indx + 1 Then
                        '        Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)

                        '        ClsAdjustments.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans, False)

                        '        lineno = 0
                        '        obj = New ClsAdjustments()
                        '        obj.Arr = New List(Of ClsAdjustmentsDetails)
                        '    End If
                        'Else
                        '    Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)

                        '    ClsAdjustments.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans, False)

                        '    lineno = 0
                        '    obj = New ClsAdjustments()
                        '    obj.Arr = New List(Of ClsAdjustmentsDetails)
                        'End If

                        'dt_indx += 1
                        'lineno += 1

                    Next ''end datarow cond.
                    If obj.Arr.Count > 0 Then
                        Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)
                        ClsAdjustmentsQCC.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans, False)
                        obj = Nothing
                        objtr = Nothing
                    End If
                End If ''end datatable cond.
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = String.Empty
            dt = Nothing
        End Try
    End Sub
    ''====================Monika==========================
    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "AdjDate", "Main Location Code", "Location Code", "Inout", "Adjustment Type", "Item Code", "UOM", "Qty", "Rate", "Amount", "Fat%", "Fat KG", "SNF%", "SNF KG", "Transaction Type") Then
            Dim trans As SqlTransaction = Nothing
            Try
                Dim obj As New ClsAdjustmentsQCC()
                clsCommon.ProgressBarShow()
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()

                Dim Checkqry As String = ""

                Dim countindex As Integer = 1
                Dim strAdcode As String = ""
                Dim strLocDesc As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New ClsAdjustmentsQCC()
                    obj.Arr = New List(Of ClsAdjustmentsQCCDetails)()
                    Dim MilkType As Integer = 1
                    Dim strIType As String = "RM"
                    '===================
                    Dim strMainLoc As String = grow.Cells("Main Location Code").Value.ToString()
                    If clsCommon.myLen(strMainLoc) > 0 Then
                        Checkqry = "select count(Location_Code) from tspl_location_master where Location_Code='" + strMainLoc + "'"
                        countindex = clsDBFuncationality.getSingleValue(Checkqry, trans)
                        If (countindex) <= 0 Then
                            Throw New Exception("Main Location Code Is Invalid Or Does Not Exist at Line No: " & (grow.Index + 1) & "")
                        End If
                    End If

                    Dim strMainLocDesc As String = clsLocation.GetName(strMainLoc, Nothing)

                    Dim strLoc As String = grow.Cells("Location Code").Value.ToString()
                    Dim Checkqry1 As String = ""
                    Dim qry6 As String = clsDBFuncationality.getSingleValue("select Location_Category  from tspl_location_master where Location_Code = '" + strMainLoc + "'")
                    If clsCommon.CompairString(qry6, "MCC") <> CompairStringResult.Equal Then
                        If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                            Throw New Exception("Check the value for Location at Line No: " & (grow.Index + 1) & "")
                        End If
                    End If
                    If clsCommon.myLen(strLoc) > 0 And clsCommon.CompairString(strMainLoc, strLoc) <> CompairStringResult.Equal Then
                        Checkqry1 = clsDBFuncationality.getSingleValue("select (Loc_Segment_Code) from tspl_location_master where Location_Code='" + strMainLoc + "' ")
                        If clsCommon.myLen(Checkqry1) > 0 Then

                            Dim Checkqry2 As String = "select count(Location_Code) from TSPL_LOCATION_MASTER where Loc_Segment_Code ='" + Checkqry1 + "'  and (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') "
                            Dim countindex1 As Integer = clsDBFuncationality.getSingleValue(Checkqry2, trans)
                            If (countindex1) <= 0 Then
                                Throw New Exception("Sub Location Code Is Invalid Or Does Not Exist at Line No: " & (grow.Index + 1) & "")
                            End If
                            If clsCommon.myLen(Checkqry2) > 0 Then
                                strLocDesc = clsLocation.GetName(strLoc, Nothing)
                            End If
                        End If
                    End If

                    '=========================================================
                    Dim InOut As String = grow.Cells("InOut").Value.ToString()
                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "OUT") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Please Insert In or Out type at Line No: " & (grow.Index + 1) & "")
                    End If

                    If grow.Cells("AdjDate").Value Is Nothing OrElse clsCommon.myLen(grow.Cells("AdjDate").Value) <= 0 Then
                        Throw New Exception("Please fill date for adjustment at Line No: " & (grow.Index + 1) & ".")
                    End If

                    If Not IsDate(grow.Cells("AdjDate").Value) Then
                        Throw New Exception("Adjustment Date should be date in DD/MM/YYYY format at Line No: " & (grow.Index + 1) & ".")
                    End If
                    '====================================Preeti=======================
                    Dim strTransactionType As String = ""
                    strTransactionType = clsCommon.myCstr(grow.Cells("Transaction Type").Value.ToString())
                    If clsCommon.myLen(strTransactionType) <= 0 Then
                        Throw New Exception("Enter Transaction Type at Line No: " & (grow.Index + 1) & "")
                    End If
                    If Not clsCommon.CompairString(strTransactionType, "Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Flushing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Opening") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Closing") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Auto Adjustment") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(strTransactionType, "Other") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be Adjustment/Flushing/Opening/Closing/Auto Adjustment/Other In ColumnName [Transaction Type] at Line No: " & (grow.Index + 1) & "")
                    End If
                    '=====================================================================

                    Dim strADate As String = clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("AdjDate").Value), "yyyy/MM/dd")



                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    '===============================
                    Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code at Line No: " & (grow.Index + 1) & "")
                    End If
                    Dim checkitem As String = "select count(Item_code) from TSPL_ITEM_MASTER where Item_Code = '" + ItemCode + "' and Product_Type ='MI'"
                    Dim CountItem As Integer = clsDBFuncationality.getSingleValue(checkitem, trans)
                    If (CountItem) <= 0 Then
                        strMainLoc = Nothing
                        strMainLocDesc = Nothing
                    End If
                    Dim ItemDesc As String = clsItemMaster.GetItemName(ItemCode, Nothing)
                    '===========================================================

                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = ""
                    strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, Nothing))
                    If clsCommon.myLen(strIType) <= 0 Then
                        strIType = "RM"
                    End If

                    AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type").Value)

                    If clsCommon.myLen(AdjType) <= 0 Then
                        Throw New Exception("Please fill adjustment type at Line No: " & (grow.Index + 1) & ".")
                    End If

                    If clsCommon.CompairString(AdjType, "Quantity") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(AdjType, "Cost") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(AdjType, "Both") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(AdjType, "FAT/SNF") <> CompairStringResult.Equal Then
                        Throw New Exception("Adjustment type should be Quantity,Cost,Both or FAT/SNF at Line No: " & (grow.Index + 1) & ".")
                    End If


                    Dim struom As String = grow.Cells("UOM").Value.ToString()

                    If clsCommon.myLen(struom) = 0 Then
                        struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Else
                        Dim intCount As String = "select count(UOM_Code) from TSPL_ITEM_UOM_DETAIL where Item_code='" & ItemCode & "'"
                        If clsCommon.myLen(intCount) > 0 Then
                            Dim Checkqry3 As String = "select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_code='" + ItemCode + "' and UOM_Code = '" + struom + "' "
                            Dim countindex3 As String = clsDBFuncationality.getSingleValue(Checkqry3, trans)
                            If clsCommon.myLen(countindex3) <= 0 Then
                                Throw New Exception("Unit Code Is Invalid Or Does Not Exist at Line No: " & (grow.Index + 1) & "")
                            End If

                        End If
                    End If

                    Dim strDescription As String = "Auto adjustment for matching opening balance of new financial year."

                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Qty").Value)

                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE()
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE()
                    Dim rmk As String = ""
                    Dim commt As String = ""
                    Dim Fatper As Double = clsCommon.myCdbl(grow.Cells("FAT%").Value)
                    Dim snfper As Double = clsCommon.myCdbl(grow.Cells("SNF%").Value)
                    Dim FatKG As Double = clsCommon.myCdbl(grow.Cells("FAT Kg").Value)
                    Dim SNFKG As Double = clsCommon.myCdbl(grow.Cells("SNF Kg").Value)
                    Dim PriceType As String = Nothing
                    Dim PriceCode As String = Nothing
                    Dim fatR As Double = 0
                    Dim snfR As Double = 0
                    Dim FatAmt As Double = 0
                    Dim snfAmt As Double = 0
                    Dim FatRate As Double = 0
                    Dim SnfRate As Double = 0



                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    'If line = 1 Then
                    obj.Adjustment_No = strAdcode
                    obj.Adjustment_Date = strADate
                    obj.Reference = ""
                    obj.Description = strDescription
                    obj.Unit_Code = "ALL"
                    obj.ItemType = strIType
                    obj.Loc_Code = strLoc
                    obj.Loc_Desc = strLocDesc
                    obj.MainLocationCode = strMainLoc
                    obj.MainLocationDesc = strMainLocDesc
                    obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                    obj.Adjustment_Type = "AAD"
                    obj.IsMilkType = IIf(clsCommon.myLen(obj.MainLocationCode) > 0, 1, 0) ' MilkType
                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    ElseIf clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    End If
                    If strTransactionType = "Adjustment" Then
                        obj.Adjustment_Type = "ADJ"
                    ElseIf strTransactionType = "Flushing" Then
                        obj.Adjustment_Type = "FLG"
                    ElseIf strTransactionType = "Opening" Then
                        obj.Adjustment_Type = "OPG"
                    ElseIf strTransactionType = "Closing" Then
                        obj.Adjustment_Type = "CLG"
                    ElseIf strTransactionType = "Auto Adjustment" Then
                        obj.Adjustment_Type = "AAD"
                    ElseIf strTransactionType = "Other" Then
                        obj.Adjustment_Type = "OTH"
                    End If
                    Dim objTr As New ClsAdjustmentsQCCDetails()
                    If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    Else
                        objTr.Price_Type = "None"
                    End If
                    'objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = clsCommon.myCstr(AdjType).Substring(0, 1) + IIf(clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal, "I", "D")
                    objTr.Item_Quantity = Iqty

                    If clsCommon.myCdbl(grow.Cells("Rate").Value) <= 0 Then
                        objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'"))
                    Else
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                    End If

                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    objTr.Unit_Code = struom
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP
                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty
                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate
                    objTr.ItemType = strIType
                    obj.ItemType = strIType
                    obj.Description = strDescription
                    objTr.fat_pers = Fatper
                    objTr.snf_pers = snfper
                    'FatKG = (Iqty * Fatper) / 100
                    'srnKG = (Iqty * snfper) / 100
                    objTr.fat_kg = FatKG
                    objTr.snf_kg = SNFKG


                    If (clsCommon.CompairString(AdjType, "Quantity") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "Both") = CompairStringResult.Equal) AndAlso Iqty <= 0 Then
                        Throw New Exception("Please Fill quantity at Line No: " & (grow.Index + 1) & "")
                    End If
                    If (clsCommon.CompairString(AdjType, "Cost") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "Both") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(grow.Cells("Amount").Value) <= 0 Then
                        Throw New Exception("Please Fill Amount at Line No: " & (grow.Index + 1) & "")
                    End If
                    If clsCommon.CompairString(AdjType, "FAT/SNF") = CompairStringResult.Equal AndAlso FatKG <= 0 AndAlso SNFKG <= 0 Then
                        Throw New Exception("Please Fill FAT/SNF value at Line No: " & (grow.Index + 1) & ".")
                    End If

                    If clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        'Throw New Exception("Please fill Price Type")
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            Throw New Exception("Please fill MCC Price Code at Line No: " & (grow.Index + 1) & "")
                        End If

                    ElseIf clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            Throw New Exception("Please fill Bulk Price Code at Line No: " & (grow.Index + 1) & "")
                        End If

                    End If


                    'line = line + 1
                    Dim xnewadjtmnt = AdjType.Substring(0, 1) + IIf(clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal, "I", "D")
                    AdjType = xnewadjtmnt
                    Dim qry As String = ""
                    Dim index As Integer = 0
                    If clsCommon.CompairString(obj.Trans_Type, "IN") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then

                            qry = "select count(Price_Code) from TSPL_Bulk_Price_MASTER where Price_Code='" + PriceCode + "'"
                            index = clsDBFuncationality.getSingleValue(qry, trans)
                            If index <= 0 Then
                                Throw New Exception("Filled Price Code Is Invalid Or Does Not Exist at Line No: " & (grow.Index + 1) & "")
                            End If
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Unit_Cost = GetMilkRateImport(PriceType, PriceCode, FatKG, SNFKG, Iqty)
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                Dim arr As New clsFatSnfRateCalculator

                                objTr.Price_Type = "Bulk"
                                objTr.Bulk_Price_Code = PriceCode
                                Dim objPrice As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(objTr.Bulk_Price_Code, NavigatorType.Current, trans)
                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate), objTr.fat_pers, objTr.snf_pers)
                                Else
                                    If clsCommon.myCdbl(objPrice.Fat_Percentage) = objTr.fat_pers And clsCommon.myCdbl(objPrice.Snf_Percentage) = objTr.snf_pers Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), objTr.fat_pers, objTr.snf_pers, clsCommon.myCdbl(objPrice.Standard_Rate), objTr.Unit_Cost)
                                    End If
                                End If


                                objTr.fat_Rate = Math.Round(arr.fatR, 2)
                                objTr.fat_Amt = Math.Round(arr.FatAmt, 2)
                                objTr.snf_Rate = Math.Round(arr.snfR, 2)
                                objTr.snf_Amt = Math.Round(arr.snfAmt, 2)
                                arr = Nothing

                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "Bulk"
                                objTr.Bulk_Price_Code = PriceCode
                                objTr.Item_Quantity = 0
                                objTr.Unit_Cost = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                            qry = " select count(Code) from (select distinct TSPL_MILK_PRICE_MASTER.Price_Code as Code,TSPL_MILK_PRICE_MASTER.Effective_Date as [Price Date], TSPL_MILK_PRICE_MASTER.Description,TSPL_MILK_PRICE_MASTER.Ratio as [Fat Ratio],TSPL_MILK_PRICE_MASTER.SNF_Ratio as [SNF Ratio], TSPL_MILK_PRICE_MASTER.FAT_Pers as [Fat %],TSPL_MILK_PRICE_MASTER.SNF_Pers as [SNF %],TSPL_MILK_PRICE_MASTER.Milk_Rate as [Milk Rate]  from TSPL_MILK_PRICE_MASTER where Price_Code in (select Distinct Price_Code from tspl_Fat_SNf_Uploader_Master inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.code where Mcc_Code='" + strMainLoc + "' and TSPL_MILK_PRICE_MASTER.Price_Code = '" + PriceCode + "')) Price"
                            index = clsDBFuncationality.getSingleValue(qry, trans)
                            If index <= 0 Then
                                Throw New Exception("Filled Price Code Is Invalid Or Does Not Exist at Line No: " & (grow.Index + 1) & "")
                            End If
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Unit_Cost = GetMilkRateImport(PriceType, PriceCode, FatKG, SNFKG, Iqty)
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                Dim arr As New clsFatSnfRateCalculator


                                objTr.Price_Type = "MCC"
                                objTr.MCC_Price_Code = PriceCode
                                Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" + objTr.MCC_Price_Code + "'", trans)
                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.fat_pers, objTr.snf_pers)
                                Else
                                    If clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")) = objTr.fat_pers And clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Pers")) = objTr.snf_pers Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), objTr.fat_pers, objTr.snf_pers, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.Unit_Cost)
                                    End If
                                End If



                                objTr.fat_Rate = Math.Round(arr.fatR, 2)
                                objTr.fat_Amt = Math.Round(arr.FatAmt, 2)
                                objTr.snf_Rate = Math.Round(arr.snfR, 2)
                                objTr.snf_Amt = Math.Round(arr.snfAmt, 2)
                                dtMilkPrice = Nothing
                                arr = Nothing

                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "MCC"
                                objTr.Item_Quantity = 0
                                objTr.MCC_Price_Code = PriceCode
                                objTr.Unit_Cost = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        Else
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                                objTr.fat_Rate = FatRate
                                objTr.snf_Rate = SnfRate
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_Amt = objTr.fat_kg * objTr.fat_Rate
                                objTr.snf_Amt = objTr.snf_kg * objTr.snf_Rate
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                'objTr.Item_Quantity = 0
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                'objTr.fat_kg = 0
                                'objTr.snf_kg = 0
                                objTr.fat_Rate = FatRate
                                objTr.snf_Rate = SnfRate
                            End If
                        End If
                    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then

                        ''For RM Other balance Qty check And works only for one unit.
                        'Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_Code, Nothing)
                        'Dim dblBalQty As Double
                        'dblBalQty = clsInventoryMovementNew.getBalance(objTr.Item_Code, strMainLoc, strLoc, obj.Adjustment_No, obj.Adjustment_Date, Nothing, objTr.Unit_Code)
                        'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                        '    If dblBalQty > 0 Then
                        '        dblBalQty = ClsLoadingTanker.GetTolerane(dblBalQty, objTr.Item_Quantity)
                        '    End If
                        'End If
                        ''-------------------------

                        'Dim dblEnteredQty As Double = objTr.Item_Quantity
                        'Dim strICodeInner As String = objTr.Item_Code
                        'Dim strUOMInner As String = objTr.Unit_Code
                        'Dim dblQtyInner As Double = objTr.Item_Quantity
                        'Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        'If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, objTr.Item_Code) = CompairStringResult.Equal Then
                        '    dblEnteredQty = dblQtyInner
                        'End If

                        'dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                        'If dblEnteredQty > dblBalQty Then
                        '    Throw New Exception("Item - " + ItemCode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        'End If
                        If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal OrElse clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                            'If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                            '    objTr.Price_Type = clsCommon.myCstr(grow.Cells("Price Type").Value)
                            '    Dim objCost As New MIlkComponentType
                            '    objCost = clsInventoryMovementNew.GetAvgCost("MI", objTr.Item_Code, strLoc, objTr.Item_Quantity, objTr.Unit_Code, objTr.fat_kg, objTr.snf_kg, strADate, strADate, True, Nothing)

                            '    objTr.fat_Rate = objCost.FAT_Cost / IIf(objTr.fat_kg <= 0, 1, objTr.fat_kg)
                            '    objTr.fat_Amt = objCost.FAT_Cost
                            '    objTr.snf_Rate = objCost.FAT_Cost / IIf(objTr.snf_kg <= 0, 1, objTr.snf_kg)
                            '    objTr.snf_Amt = objCost.SNF_Cost
                            'ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                            '    objTr.Price_Type = clsCommon.myCstr(grow.Cells("Price Type").Value)
                            '    objTr.Item_Quantity = 0
                            '    objTr.fat_kg = 0
                            '    objTr.snf_kg = 0
                            '    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                            '    objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                            'End If

                        Else
                            'If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                            '    objTr.Price_Type = "None"
                            '    Dim objCost As New MIlkComponentType
                            '    objCost = clsInventoryMovementNew.GetAvgCost("MI", objTr.Item_Code, strLoc, objTr.Item_Quantity, objTr.Unit_Code, objTr.fat_kg, objTr.snf_kg, strADate, strADate, True, Nothing)

                            '    objTr.fat_Rate = objCost.FAT_Cost / IIf(objTr.fat_kg <= 0, 1, objTr.fat_kg)
                            '    objTr.fat_Amt = objCost.FAT_Cost
                            '    objTr.snf_Rate = objCost.FAT_Cost / IIf(objTr.snf_kg <= 0, 1, objTr.snf_kg)
                            '    objTr.snf_Amt = objCost.SNF_Cost
                            'ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                            '    objTr.Price_Type = "None"
                            '    objTr.Item_Quantity = 0
                            '    objTr.fat_kg = 0
                            '    objTr.snf_kg = 0
                            '    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                            '    objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Rate").Value)
                            'End If
                        End If
                    End If

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans)

                    ClsAdjustmentsQCC.PostData(obj.Adjustment_No, AdjustmentEnum.strCostTransaction)
                Next

                clsCommon.ProgressBarHide()
                RadMessageBox.Show("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                clsCommon.ProgressBarHide()
            End Try

        End If
    End Sub
    ''=====================end here=============================

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        ''export blank sheet
        Dim qry As String = "select convert(date,'01/01/1900',103) as AdjDate,cast('001' as varchar) as [Main Location Code],cast('001' as varchar) as [Location Code],'In' as [Inout],'Quantity' as [Adjustment Type],'FG0000001' as [Item Code],'KG' as [UOM],0 as [Qty],0 as [Rate],0 as [Amount],0 as [Fat%],0 as [Fat KG],0 as [SNF%],0 as [SNF KG],'' as [Transaction Type]"
        transportSql.ExporttoExcel(qry, Me)
    End Sub
#End Region

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        Try
            Dim qryExport As String
            qryExport = " select distinct '31-Mar-2017' as AdjDate,TSPL_INV_MOVE_DL.Location_Code as [Main Location Code],TSPL_INV_MOVE_DL.Location_Code as [Location Code],'In' as Inout,'Both'as [Adjustment Type], TSPL_INV_MOVE_DL.Item_Code as [Item Code],Stock_UOM as UOM,0 as Qty,0 as Rate,0 as Amount,0 as [Fat%],0 as [Fat KG],0 as [SNF%],0 as [SNF KG],'' as [Transaction Type] " &
                        " from (select TSPL_INV_MOVE_DL.* from TSPL_INV_MOVE_DL inner join (" &
                        " select Item_Code,Location_Code,max(TRANS_DATE) as TRANS_DATE from TSPL_INV_MOVE_DL where TRANS_DATE<='31-Mar-2017' " &
                        " group by Item_Code,Location_Code) as Cond on TSPL_INV_MOVE_DL.Item_Code=Cond.Item_Code and TSPL_INV_MOVE_DL.Location_Code=Cond.Location_Code " &
                        " and TSPL_INV_MOVE_DL.TRANS_DATE=Cond.TRANS_DATE " &
                        " ) TSPL_INV_MOVE_DL left join ( select distinct TSPL_ADJUSTMENT_DETAIL_QC.Item_Code,TSPL_ADJUSTMENT_DETAIL_QC.Location_Code from TSPL_ADJUSTMENT_DETAIL_QC " &
                        " inner join TSPL_ADJUSTMENT_HEADER_QC on TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No=TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No  where TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Type='AAD') as Adj on " &
                        " TSPL_INV_MOVE_DL.Item_Code = Adj.Item_Code And TSPL_INV_MOVE_DL.Location_Code = Adj.Location_Code " &
                        " where (CL_QTY<>0 or CL_Avg_Cost<>0 or CL_FAT_KG<>0 or CL_SNF_KG<>0) and Adj.Item_Code is null and Adj.Location_Code is null and len(coalesce(TSPL_INV_MOVE_DL.Location_Code ,''))>0"
            transportSql.ExporttoExcelWithoutFilter(qryExport, "", "", Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs)  ''BHA/05/10/18-000603 By balwinder on 05/10/2018
        'Try
        '    If Not isNewEntry Then
        '        Throw New Exception("This facility is available Only for new entry")
        '    End If

        '    Dim qry As String = "SELECT Adjustment_No AS [AdjustmentNumber],CONVERT(varchar(10), TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)+' '+ CONVERT(varchar(5), TSPL_ADJUSTMENT_HEADER.Adjustment_Date,114) as [Date],case when IsMilkType=1 then 'Milk Type' else 'Non-Milk Type' end as MilkType, Document_No,case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location],coalesce(Against_Item_Stock_Conv_Doc,against_Item_Stock_Conversion) as [Against Item Stock Conversion],Against_AP_Invoice_No as [Against AP Invoice No]," &
        '   " (case when Adjustment_Type='ADJ' then 'Adjustment' when Adjustment_Type='FLG' then 'Flushing' when Adjustment_Type='OPG' then 'Opening' when Adjustment_Type='CLG' then 'Closing' when Adjustment_Type='AAD' then 'Auto Adjustment' else 'Other' end) as [Adjustment Type] " &
        '   " FROM  TSPL_ADJUSTMENT_HEADER  "
        '    Dim whrClas As String = " 1=1 and isnull(AdjustType,'') <> 'Consume' "
        '    txtAdjustmentNo.Value = clsCommon.ShowSelectForm("AdjustmentStoreDoc1", qry, "AdjustmentNumber", whrClas, txtAdjustmentNo.Value, "AdjustmentNumber", True)
        '    LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
        '    isNewEntry = True
        '    UsLock1.Status = ERPTransactionStatus.Pending
        '    btnSave.Enabled = True
        '    btnPost.Enabled = True
        '    btnDelete.Enabled = True
        '    cboAdjustmentType.Enabled = True
        '    btnSave.Text = "Save"
        '    txtAdjustmentNo.Value = ""
        '    If settPickCostFromItemMaster Then
        '        For ii As Integer = 0 To gv1.Rows.Count - 1
        '            gv1.CurrentRow = gv1.Rows(ii)
        '            If ChkMilkType.Checked Then
        '                Dim obj As clsItemMasterQCParameter = clsItemMasterQCParameter.GetStandardFATSNFRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
        '                gv1.CurrentRow.Cells(colfat_Rate).Value = obj.FATRate
        '                gv1.CurrentRow.Cells(colsnf_Rate).Value = obj.SNFRate
        '                gv1.CurrentRow.Cells(colFATPers).Value = obj.FATPer
        '                gv1.CurrentRow.Cells(colSNFPers).Value = obj.SNFPer
        '            Else
        '                SetUnitCost()
        '            End If
        '            UpdateCurrentRow(ii)
        '        Next
        '    End If

        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click  ' Ticket : TEC/29/10/18-000353 By Sanjay
        clsOpenInventory.ShowInventoryDatails(txtAdjustmentNo.Value)
    End Sub

    Sub OpenBatchItemNew()
        Dim blnBatchqty As Boolean = False
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) Then
            If clsCommon.CompairString("In", clsCommon.myCstr(cboTransType.SelectedValue)) = CompairStringResult.Equal Then
                Dim frm As frmBatchItemIn_ForMilkItem = New frmBatchItemIn_ForMilkItem()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventoryNew))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                End If
            Else
                If RunBatchFifowise = 0 Then
                    Dim frm As frmBatchItemOutNew = New frmBatchItemOutNew()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.strLocationCode = txtLocation.Value
                    frm.strCurrDocNo = txtAdjustmentNo.Value
                    frm.strCurrDocType = "IC-SE"
                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                    'frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                    frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventoryNew))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                    End If
                Else


                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                            If clsCommon.myCBool(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Is_Batch_Item  from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'", Nothing)) Then
                                Dim strBatchunion As String = ""
                                If RunBatchFifowise = 1 Then
                                    If ii > 0 Then
                                        Dim strICodeOuter As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                                        For jj As Integer = 0 To ii - 1
                                            Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                                            If clsCommon.CompairString(strICodeOuter, strICodeInner) = CompairStringResult.Equal Then
                                                Dim arr As List(Of clsBatchInventoryNew) = Nothing
                                                arr = TryCast(gv1.Rows(jj).Cells(colICode).Tag, List(Of clsBatchInventoryNew))
                                                For Each obj As clsBatchInventoryNew In arr
                                                    strBatchunion += " union all select '" & clsCommon.myCstr(obj.Batch_No) & "' as Batch_No, " &
                                                        "'O' as In_Out_Type, " &
                                                        "'" & clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value) & "' as OrgUOM," & obj.Qty & " as OrgQty,0 as OrgMRP, " &
                                                        "" & obj.Qty & " as Qty, 0 as MRP "
                                                Next

                                            End If
                                        Next
                                    End If
                                    gv1.CurrentRow = gv1.Rows(ii)

                                    Dim frm As frmBatchItemOutNew = New frmBatchItemOutNew()
                                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                                    frm.strLocationCode = txtLocation.Value
                                    frm.strCurrDocNo = txtAdjustmentNo.Value
                                    frm.strCurrDocType = "IC-AD"
                                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                    frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventoryNew))

                                    If frm.OpenSerialList(0, "", strBatchunion) Then
                                        gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                                        blnBatchqty = True
                                    Else
                                        Dim batchQty As Double = 0
                                        For Each obj As clsBatchInventoryNew In frm.arr
                                            batchQty += obj.Qty
                                        Next
                                        clsCommon.MyMessageBoxShow("Please increase stock Item Code - " & frm.strItemCode & " , Entered Qty - " & clsCommon.myCstr(frm.dblqty) & " Batch Qty - " & clsCommon.myCstr(batchQty), Me.Text)
                                        blnBatchqty = False
                                        Exit Sub
                                    End If

                                End If
                            End If
                        End If
                    Next

                End If

            End If
        End If
    End Sub

    Public Sub OpenBatchItemIfFIFIOSettingONNew()
        Dim arr As List(Of clsBatchInventoryNew) = Nothing
        Dim strBatchunion As String = ""
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventoryNew))
        End If
        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventoryNew In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub

    Private Sub rbtnExportPosted_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtProductionEntry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtProductionEntry._MYValidating
        'Dim qry As String = "SELECT Adjustment_No AS [AdjustmentNumber],CONVERT(varchar(10), TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)+' '+ CONVERT(varchar(5), TSPL_ADJUSTMENT_HEADER.Adjustment_Date,114) as [Date], Document_No,case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location],coalesce(Against_Item_Stock_Conv_Doc,against_Item_Stock_Conversion) as [Against Item Stock Conversion],Against_AP_Invoice_No as [Against AP Invoice No]," &
        '   " (case when Adjustment_Type='ADJ' then 'Adjustment' when Adjustment_Type='FLG' then 'Flushing' when Adjustment_Type='OPG' then 'Opening' when Adjustment_Type='CLG' then 'Closing' when Adjustment_Type='AAD' then 'Auto Adjustment' when Adjustment_Type='PRE' then 'Production Entry' else 'Other' end) as [Adjustment Type] " &
        '   " FROM  TSPL_ADJUSTMENT_HEADER  "
        'Dim whrClas As String = " 1=1 and isnull(AdjustType,'') <> 'Consume' and Adjustment_Type = 'PRE' and Adjustment_No not in (Select Production_Entry from TSPL_ADJUSTMENT_HEADER_QC)  and Posted = 'Y' "
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrClas += " AND (Loc_Code in (" + objCommonVar.strCurrUserLocations + ") or  mainlocationcode in (" + objCommonVar.strCurrUserLocations + "))"
        'End If
        ''whrClas += " AND ItemType IN ('RM', 'OT')"
        txtAdjustmentNo.Value = ""
        Dim qry As String = "select xx.Code As Code,CONVERT(varchar, TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Date,103) as Date from ( select distinct TT.Code from (SELECT TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No AS Code,Item_Code as ICode,TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code,Item_Quantity as Qty,1 as Chk,1 as RI FROM TSPL_ADJUSTMENT_DETAIL_QC left join TSPL_ADJUSTMENT_HEADER_QC ON TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No= TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No  where isnull(AdjustType,'') <> 'Consume' and TSPL_ADJUSTMENT_HEADER_QC.Adjustment_Type = 'PRE' and Posted = 'Y' and TSPL_ADJUSTMENT_DETAIL_QC.QC_Status='OK' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " AND (Loc_Code in (" + objCommonVar.strCurrUserLocations + ") or  mainlocationcode in (" + objCommonVar.strCurrUserLocations + "))"
        End If
        qry += " union all " &
         " SELECT TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC AS Code,Item_Code as ICode,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code,Item_Quantity as Qty,0 as Chk,-1 as RI " &
         " FROM  TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL left join TSPL_ADJUSTMENT_STORE_ENTRY_HEAD on TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo=TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo  where isnull(AdjustType,'') <> 'Consume' and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Adjustment_Type = 'PRE' " &
        ")TT group by Code,ICode,Unit_Code having SUM(Chk)>0 and SUM(Qty *RI)>0 )xx " &
        " left outer join TSPL_ADJUSTMENT_HEADER_QC on TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = xx.Code "
        'and TSPL_ADJUSTMENT_HEADER_QC.Production_Entry not in ('" + txtAdjustmentNo.Value + "')
        txtProductionEntry.Value = clsCommon.ShowSelectForm("AdjStoreDoc1", qry, "Code", "", txtProductionEntry.Value, "Code", isButtonClicked)
        LoadDataForProductionEntryQc(txtProductionEntry.Value, NavigatorType.Current)
        txtAdjustmentNo.Value = ""
    End Sub

    Sub LoadDataForProductionEntryQc(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            ' isNewEntry = False
            btnSave.Text = "Save"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New ClsAdjustmentsQCC()
            obj = obj.GetData(strCode, AdjustmentEnum.strCostTransaction, NavTyep, Nothing, False, True)
            ' obj.GetData(strCode, AdjustmentEnum.strCostTransaction, NavTyep, Nothing, False, True)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0) Then
                'If clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                '    btnSave.Enabled = False
                '    btnPost.Enabled = False
                '    btnDelete.Enabled = False
                '    UsLock1.Status = ERPTransactionStatus.Approved
                'End If

                lblProductionEntry.Text = obj.Production_Entry
                txtAdjustmentNo.Value = obj.Adjustment_No
                txtDate.Value = obj.Adjustment_Date
                'obj.Posting_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                'obj.Posted()

                'obj.Unit_Code = "ALL"
                'obj.ItemType = "E"

                If obj.chklocation = "Y" Then
                    chklocation.Checked = True
                Else
                    chklocation.Checked = False
                End If

                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = obj.Loc_Desc
                cboTransType.SelectedValue = obj.Trans_Type

                cboAdjustmentType.SelectedValue = obj.Adjustment_Type
                cboAdjustmentType.Enabled = False
                txtSpecification.Text = obj.Adjustment_Specification
                If clsCommon.CompairString(obj.Adjustment_Type, "OTH") = CompairStringResult.Equal Then
                    txtSpecification.Enabled = True
                Else
                    txtSpecification.Enabled = False
                End If

                If obj.IsMilkType = 1 Then
                    ChkMilkType.Checked = True
                Else
                    ChkMilkType.Checked = False
                End If
                ChkMilkType.Enabled = False
                FndMainLocation.Value = obj.MainLocationCode
                LblMainLocation.Text = obj.MainLocationDesc


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsAdjustmentsQCCDetails In obj.Arr

                        Dim strqry As String = "SELECT isnull(TSPL_ADJUSTMENT_DETAIL_QC.item_quantity,0)-isnull(qc.item_quantity,0) AS Pending_Qty  FROM  TSPL_ADJUSTMENT_DETAIL_QC left join TSPL_ADJUSTMENT_HEADER_QC On TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No= TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No  left join (select TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.item_code,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code,sum(TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.item_quantity) as item_quantity  From TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL   LEFT OUTER JOIN TSPL_ADJUSTMENT_STORE_ENTRY_HEAD ON TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.ProductionStoreEntryNo = TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo   Where 1 = 1 And TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC ='" & objTr.Adjustment_No & "' and TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.item_code='" & objTr.Item_Code & "'" &
                         " Group by TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.Against_Production_Entry_QC,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.item_code,TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL.Unit_Code  " &
                         "  ) qc on qc.item_code=TSPL_ADJUSTMENT_DETAIL_QC.item_code and qc.Unit_Code=TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code and qc.Against_Production_Entry_QC =TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No  " &
                        " WHERE  TSPL_ADJUSTMENT_DETAIL_QC.Adjustment_No ='" & objTr.Adjustment_No & "' and TSPL_ADJUSTMENT_DETAIL_QC.item_code='" & objTr.Item_Code & "' and TSPL_ADJUSTMENT_DETAIL_QC.Unit_Code='" & objTr.Unit_Code & "' and TSPL_ADJUSTMENT_DETAIL_QC.QC_Status='OK'"

                        Dim PendingQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strqry))
                        If PendingQty > 0 Then
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = PendingQty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(gv1.Rows.Count)
                        Else
                            Continue For
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objTr.Adjustment_Line_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        If obj.IsMilkType = 1 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItemNew
                        Else
                            Dim TemparrBatchItem As New List(Of clsBatchInventory)
                            Dim TempdblBalance As Double = 0
                            If objTr.arrBatchItem IsNot Nothing AndAlso objTr.arrBatchItem.Count > 0 Then
                                TempdblBalance = 0
                                For Each objBatch As clsBatchInventory In objTr.arrBatchItem
                                    TempdblBalance = ClsAdjustmentsStoreEntry.GetBatchBalanceAdjustmentsStoreEntry(objBatch.Item_Code, objBatch.Location_Code, objBatch.Batch_No, objBatch.MRP, objBatch.UOM, objBatch.Document_Code, objBatch.Document_Type, Nothing)
                                    If TempdblBalance = 0 Then
                                        Continue For
                                    ElseIf TempdblBalance < objBatch.Qty Then
                                        objBatch.Qty = TempdblBalance
                                        TemparrBatchItem.Add(objBatch)
                                    Else
                                        TemparrBatchItem.Add(objBatch)
                                    End If
                                Next
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = TemparrBatchItem
                            TemparrBatchItem = Nothing


                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        Dim AdjTypeFirstChar As String = objTr.Adjustment_Type.Substring(0, 1)
                        If clsCommon.CompairString(AdjTypeFirstChar, "Q") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentQty
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "C") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentCost
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "B") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "F") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentFAT_SNF
                        End If
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Item_Quantity

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = objTr.Unit_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComment).Value = objTr.Comments
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.mrp
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = clsItemMaster.GetItemTypeFromMaster(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Tag = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Value = ProductType(gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Tag)

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).Value = objTr.fat_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).Value = objTr.fat_pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).Value = objTr.snf_pers

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Value), "Milk") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).ReadOnly = False
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).ReadOnly = True
                        End If

                        If clsCommon.CompairString(objTr.Itemstatus, "OLD") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeStatus).Value = "OLD"
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeStatus).Value = "NEW"
                        End If

                        '' aded by Panch Raj
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).Value = objTr.Price_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).Value = objTr.MCC_Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).Value = objTr.Bulk_Price_Code

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfat_Rate).Value = objTr.fat_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfat_Amt).Value = objTr.fat_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colsnf_Rate).Value = objTr.snf_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colsnf_Amt).Value = objTr.snf_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objTr.Item_Code)

                        ''richa agarwal BHA/09/05/18-000021
                        Dim strItemCode_Can_Or_Crate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select CASE WHEN CAN=1 THEN 'CAN' WHEN CRATE=1 THEN 'CRATE' END from tspl_item_mASTER WHERE ITEM_CODE='" & objTr.Item_Code & "' AND (ISNULL(CAN ,0)=1 OR ISNULL(CRATE,0)=1)"))
                        If clsCommon.CompairString(strItemCode_Can_Or_Crate, "CAN") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).ReadOnly = True
                        ElseIf clsCommon.CompairString(strItemCode_Can_Or_Crate, "CRATE") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).ReadOnly = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).ReadOnly = False
                        End If
                        '------
                    Next

                    'If Not clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                    '    gv1.Rows.AddNew()
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                    'End If
                End If
                If obj.Is_JobWork = 1 Then
                    chkJobWork.Visible = True
                End If

                chkJobWork.Checked = IIf(obj.Is_JobWork = 1, True, False)

                chkJobWork.Enabled = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    'Private Sub cmbQCStatus_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbQCStatus.SelectedIndexChanged
    '    If isFormLoad = False Then

    '        For ii As Integer = 0 To gv1.Rows.Count - 1
    '            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 AndAlso clsCommon.CompairString(cmbQCStatus.SelectedValue, "Select") <> CompairStringResult.Equal Then
    '                gv1.Rows(ii).Cells(colQCStatus).Value = cmbQCStatus.SelectedValue
    '            End If
    '        Next
    '    End If
    'End Sub
End Class
