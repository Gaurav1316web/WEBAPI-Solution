Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmAcknowledgementEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isdipmarkingmendatory As Boolean = False
    Public isLoadingBulkSaleTanker As Boolean = False
    Public strDocNo As String = ""
    Const colSlNo As String = "SlNo"
    Const colSealNo As String = "SealNo"

    Const colChamberNo As String = "colChNo"
    Const colChamberDesc As String = "colChDesc"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colUOM As String = "colUOM"
    Const ColQtyInKg As String = "QtyInKg"
    Const colAutoFat As String = "colAutoFat"
    Const colAutoSnf As String = "colAutoSnf"
    Const colAutoClr As String = "colAutoClr"
    Const colACCode As String = "colACCode"
    Const colFatKG As String = "colFatKG"
    Const colSNFKG As String = "colSNFKG"
    Const colFatRate As String = "colFatRate"
    Const colSNFRate As String = "colSNFRate"
    Const colAmount As String = "colAmount"
    Const colRemarks As String = "colRemarks"
    Const ColIntermittentDispatchNo As String = "ColIntrDisNo"


    Const colSiloLocation_Code As String = "Location_Code"
    Const colSiloChamber_No As String = "Chamber_No"
    Const colSiloItem_Code As String = "Item_Code"
    Const colSiloQty As String = "Qty"
    Const colSiloUOM As String = "UOM"
    Const colSiloStock_Qty As String = "Stock_Qty"
    Const colSiloStock_UOM As String = "Stock_UOM"
    Const colSiloFat_Per As String = "Fat_Per"
    Const colSiloFat_Rate As String = "Fat_Rate"
    Const colSiloFat_KG As String = "Fat_KG"
    Const colSiloFat_Amt As String = "Fat_Amt"
    Const colSiloSNF_Per As String = "SNF_Per"
    Const colSiloSNF_Rate As String = "SNF_Rate"
    Const colSiloSNF_KG As String = "SNF_KG"
    Const colSiloSNF_Amt As String = "SNF_Amt"
    Const colSiloAvg_Cost As String = "Avg_Cost"


    Public Shared isPortOpened As Boolean = False

    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isCellValueChangedOpen As Boolean = False
    Dim isForcellyStarted As Boolean = False
    Dim IsCanceled As Boolean = False
    Dim oldValue As Integer = 0
    Dim objEco As New clsEkoPro
    Dim objSr As New clsSerialPort
    Public CfColName As String = String.Empty
    Public FATColName As String = String.Empty
    Public SNFColName As String = String.Empty
    Public CLRColName As String = String.Empty
    Dim isLoad As Boolean = False
    Dim IsAutoTankerWeightment As Boolean = False
    Dim ApplyManualTransferRateOnMultipleChamberTankerDispatch As Integer = 0
    Dim ApplyTotalSolidPriceChart As Boolean = False
    Dim settFirstGateOutProcessForBulkProcument As Boolean = False
    Dim settTankerDispatchAvgFATSNFPer As Boolean = False
    Public settAllowtoNegativeFATSNFKgAtTankerDispatch As Boolean = False
    Dim CreateProvisionOfTankerDispatchWithClosingKM As Boolean = False
    Dim settTankerDispatchIntermittentSingleGateIn As Boolean = False
    Dim EnableTankerNoInMccTankerDispWithMccTankerGateOut As Boolean = False
    Dim AllowToEnterSnfAtPlantInMccTankerDispatch As Boolean = False
    Dim ShowTankerWithoutCheckingAnyValidation As Boolean = False

#End Region

    Private Sub FrmMccDispatch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()

        coll.Add("Document_No", "Varchar(50) NOT NULL Primary Key")
        coll.Add("Document_Date", "datetime not NULL")
        coll.Add("MCC_Code", "Varchar(30) NOT NULL")
        coll.Add("MCC_Name", "Varchar(50) NOT NULL")
        coll.Add("Dispatch_Date", "datetime not NULL")
        coll.Add("Dispatch_Document_No", "varchar(50)  NULL references TSPL_MCC_Dispatch_Challan(Chalan_NO) ")
        coll.Add("Tanker_Dispatch_To", "Varchar(10) NOT NULL ")
        coll.Add("Mcc_Or_Plant_Code", "varchar(12) null references tspl_location_master(location_code)")
        coll.Add("Tanker_No", "varchar(20) null ")
        coll.Add("isPosted", "integer not null default 0")
        coll.Add("Posting_Date", "date null ")
        coll.Add("Comp_Code", "varchar(12) NOT NULL")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "varchar(10) NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "varchar(10) NOT NULL")

        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ACKNOWLEDGENT_ENTRY_Header", coll, Nothing, False, False, "", "Document_No", "Document_Date")


        coll = New Dictionary(Of String, String)()
        coll.Add("SNO", "integer not NULL default 0")
        coll.Add("Document_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        coll.Add("Chamber_No", "integer null")
        coll.Add("Chamber_Description", "VARCHAR(100) NULL")
        coll.Add("Item_code", "VARCHAR(50) NOT NULL REFERENCES TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Item_UOM", "varchar(12) not null REFERENCES TSPL_UNIT_MASTER(UNIT_CODE)")
        coll.Add("Qty_KG", "decimal(18, 3) NULL default 0")
        coll.Add("FAT_KG", "decimal(18, 3) NULL default 0")
        coll.Add("SNF_KG", "decimal(18, 3) NULL default 0")
        coll.Add("FAT_Rate", "decimal(18, 2) NULL default 0")
        coll.Add("SNF_Rate", "decimal(18, 2) NULL default 0")
        coll.Add("Amount", "decimal(18, 2) NULL default 0")

        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ACKNOWLEDGENT_ENTRY_Detail", coll, Nothing, False, False, "TSPL_ACKNOWLEDGENT_ENTRY_Header", "Document_No", "")

        coll = New Dictionary(Of String, String)
        coll.Add("Document_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        coll.Add("Param_Field_Code", "varchar(30) NOT NULL ")
        coll.Add("Param_Field_Desc", "varchar(150) NOT NULL ")
        coll.Add("Param_Field_Value", "varchar(12) NOT NULL")
        coll.Add("Param_Type", "varchar(12) NOT NULL")
        coll.Add("SNO", "integer not NULL default 0")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_Acknowlegement_Entry_Parameter_Detail", coll, Nothing, False, False, "TSPL_ACKNOWLEDGENT_ENTRY_Header", "Document_No", "")

        coll = New Dictionary(Of String, String)
        coll.Add("AcknowEntryDocument_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_GATE_ENTRY_DETAILS", coll, Nothing, False, False, "", "Gate_Entry_No", "Date_And_Time")

        coll = New Dictionary(Of String, String)
        coll.Add("AcknowEntryDocument_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_Weighment_Detail", coll, Nothing, False, False, "", "Weighment_No", "Weighment_date")

        coll = New Dictionary(Of String, String)
        coll.Add("AcknowEntryDocument_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_QUALITY_CHECK", coll, Nothing, False, False, "", "QC_No", "QC_In_Date_Time")

        coll = New Dictionary(Of String, String)
        coll.Add("AcknowEntryDocument_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_UNLOADING", coll, Nothing, False, False, "", "Unloading_No", "Unloading_Date_Time")

        coll = New Dictionary(Of String, String)
        coll.Add("AcknowEntryDocument_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_Gate_Out", coll, Nothing, False, False, "", "Doc_No", "Doc_Date")

        coll = New Dictionary(Of String, String)
        coll.Add("AcknowEntryDocument_No", "varchar(50)  NULL references TSPL_ACKNOWLEDGENT_ENTRY_Header(Document_No) ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_TRANSFER_IN", coll, Nothing, True, False, "", "Receipt_Challan_No", "Receipt_Challan_Date")

        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        ButtonToolTip.SetToolTip(btnReverse, "Press Alt+R to Reverse the Transaction")
        If clsCommon.myLen(strDocNo) > 0 Then
            fndDocNo.Value = strDocNo
            LoadData(fndDocNo.Value, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndDocNo.Value = clsCommon.myCstr(Me.Tag)
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If

        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Reset()
    End Sub



    Sub Reset()
        fndMCCCode.Enabled = True
        isLoad = False
        fndMCCCode.Value = clsGateEntry.getUsersDefaultLocation()
        txtMCCName.Text = clsLocation.GetName(clsGateEntry.getUsersDefaultLocation(), Nothing)
        lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)
        dtpDateAndTime.Value = clsCommon.GETSERVERDATE()
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        ddlTankerDispatchTo.SelectedIndex = 1
        txtTankerDispatch.Value = ""
        fndPlantOrMCCCode.Value = ""
        txtPlantOrMccName.Text = ""
        fndTnakerNo.Value = ""
        LoadBlankGrid()
        LoadBlankGridAE()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnReverse.Enabled = False
        btnPrint.Enabled = False
        btnSave.Enabled = True
        btnClose.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        txtTankerTransporterName.Text = ""
        txtTankerTransporterName.ReadOnly = True
        FindAndRestoreGridLayout(Me)
        FindAndSetTabStopFalse(Me)

    End Sub

    Function isReversed(ByVal strChallanNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Integer = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(isReversed,0) as isReversed from TSPL_MCC_Dispatch_Challan where chalan_no='" & strChallanNo & "' ", trans))
        If rValue = 1 Then
            Return True
        Else
            Return False
        End If
    End Function



    Function FillYesNoValue() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function
    Sub LoadBlankGridAE()
        'Dim CfColName As String = String.Empty
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            gvAE.Rows.Clear()
            gvAE.Columns.Clear()
            gvAE.DataSource = Nothing
            Exit Sub
        End If

        Dim whrCls As String = String.Empty
        If clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
            whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        Else
            whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        End If
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gvAE.Rows.Clear()
        gvAE.Columns.Clear()
        gvAE.DataSource = Nothing
        Dim repoComboColumn As GridViewComboBoxColumn
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Chamber No"
        repoNumBox.Name = colChamberNo
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.Tag = colChamberNo
        gvAE.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Chamber Description"
        repoTextBox.Name = colChamberDesc
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 100
        repoTextBox.Tag = colChamberDesc
        gvAE.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = colICode
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 100
        repoTextBox.Tag = colICode
        gvAE.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Item Description"
        repoTextBox.Name = colIName
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 200
        repoTextBox.Tag = colIName
        gvAE.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "UOM"
        repoTextBox.Name = colUOM
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 60
        repoTextBox.Tag = colUOM
        gvAE.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Qty In Kg"
        repoNumBox.Name = ColQtyInKg
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = False
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.Tag = ColQtyInKg
        gvAE.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Intermittent Dispatch No"
        repoTextBox.Name = ColIntermittentDispatchNo
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        repoTextBox.Tag = ColIntermittentDispatchNo
        gvAE.MasterTemplate.Columns.Add(repoTextBox)

        If pFields Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("Type"), "CF") = CompairStringResult.Equal Then
                    CfColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    FATColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    SNFColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
                    CLRColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then 'And (clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") <> CompairStringResult.Equal) And (clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") <> CompairStringResult.Equal)
                    repoDecimalColumn = New GridViewDecimalColumn()
                    repoDecimalColumn.Name = dt.Rows(i)("Code")
                    repoDecimalColumn.FormatString = "{0:n3}"
                    repoDecimalColumn.Width = 120
                    repoDecimalColumn.DecimalPlaces = 3
                    If clsCommon.CompairString(dt.Rows(i)("Code"), SNFColName) = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Code"), FATColName) = CompairStringResult.Equal Then
                        repoDecimalColumn.FormatString = "{0:n3}"
                        repoDecimalColumn.DecimalPlaces = 3
                        If settTankerDispatchAvgFATSNFPer Then
                            repoDecimalColumn.DecimalPlaces = 10
                        End If
                    End If
                    repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                    repoDecimalColumn.Tag = dt.Rows(i)("Type")
                    'If AllowToEnterSnfAtPlantInMccTankerDispatch = True AndAlso clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value)) Then
                    '    repoDecimalColumn.ReadOnly = False
                    '    ' ElseIf clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value)) Then
                    'ElseIf clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    '    repoDecimalColumn.ReadOnly = True
                    'Else
                    '    repoDecimalColumn.ReadOnly = False
                    'End If
                    gvAE.MasterTemplate.Columns.Add(repoDecimalColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "A") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = OpenParameterValueList(dt.Rows(i)("Code"))
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gvAE.MasterTemplate.Columns.Add(repoComboColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "B") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = FillYesNoValue()
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gvAE.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    repoTextColumn.ReadOnly = False
                    gvAE.MasterTemplate.Columns.Add(repoTextColumn)
                End If

                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    gvAE.Columns.Add(colAutoFat, "Auto FAT")
                    gvAE.Columns(colAutoFat).Width = 120
                    gvAE.Columns(colAutoFat).ReadOnly = settTankerDispatchAvgFATSNFPer
                    gvAE.Columns(colAutoFat).Tag = "AutoFAT"
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    gvAE.Columns.Add(colAutoSnf, "Auto SNF")
                    gvAE.Columns(colAutoSnf).Width = 120
                    gvAE.Columns(colAutoSnf).ReadOnly = settTankerDispatchAvgFATSNFPer
                    gvAE.Columns(colAutoSnf).Tag = "AutoSNF"
                End If

            Next
            If clsCommon.myLen(FATColName) > 0 Then
                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colFatKG
                repoDecimalColumn.FormatString = "{0:n3}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 3
                repoDecimalColumn.HeaderText = "FAT KG"
                repoDecimalColumn.Tag = colFatKG
                repoDecimalColumn.ReadOnly = True
                gvAE.MasterTemplate.Columns.Add(repoDecimalColumn)

                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colFatRate
                repoDecimalColumn.FormatString = "{0:n2}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 2
                repoDecimalColumn.HeaderText = "FAT Rate"
                repoDecimalColumn.Tag = colFatRate
                repoDecimalColumn.ReadOnly = True
                gvAE.MasterTemplate.Columns.Add(repoDecimalColumn)
            End If
            If clsCommon.myLen(SNFColName) > 0 Then
                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colSNFKG
                repoDecimalColumn.FormatString = "{0:n3}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 3
                repoDecimalColumn.HeaderText = "SNF KG"
                repoDecimalColumn.Tag = colSNFKG
                repoDecimalColumn.ReadOnly = True
                gvAE.MasterTemplate.Columns.Add(repoDecimalColumn)

                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colSNFRate
                repoDecimalColumn.FormatString = "{0:n2}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 2
                repoDecimalColumn.HeaderText = "SNF Rate"
                repoDecimalColumn.Tag = colSNFRate
                repoDecimalColumn.ReadOnly = True
                gvAE.MasterTemplate.Columns.Add(repoDecimalColumn)
            End If

            If clsCommon.myLen(SNFColName) > 0 AndAlso clsCommon.myLen(FATColName) > 0 Then
                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colAmount
                repoDecimalColumn.FormatString = "{0:n2}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 2
                repoDecimalColumn.HeaderText = "Amount"
                repoDecimalColumn.Tag = colAmount
                repoDecimalColumn.ReadOnly = True
                gvAE.MasterTemplate.Columns.Add(repoDecimalColumn)
            End If
            gvAE.Columns.Add(colRemarks, "Remarks")
            gvAE.Columns(colRemarks).Width = 300
            gvAE.Columns(colRemarks).ReadOnly = False
            gvAE.Columns(colRemarks).Tag = colRemarks
            gvAE.Columns(colRemarks).WrapText = True
        Else
            Throw New Exception("There is No parameter defined in Parameter Master. Please Define It First")
        End If


        gvAE.AllowAddNewRow = False
        gvAE.AllowDeleteRow = False
        gvAE.AllowRowReorder = False
        gvAE.EnableFiltering = False
        gvAE.EnableSorting = False
        gvAE.EnableGrouping = False
        gvAE.AllowColumnChooser = True
        gvAE.AllowColumnReorder = True
        ReStoreGridLayout()
        isCellValueChangedOpen = False
        repoTextColumn = Nothing
        repoDecimalColumn = Nothing
        repoComboColumn = Nothing
    End Sub
    Sub LoadBlankGrid()
        'Dim CfColName As String = String.Empty
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = Nothing
            Exit Sub
        End If

        Dim whrCls As String = String.Empty
        If clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
            whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        Else
            whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        End If
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        Dim repoComboColumn As GridViewComboBoxColumn
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Chamber No"
        repoNumBox.Name = colChamberNo
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.Tag = colChamberNo
        gv.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Chamber Description"
        repoTextBox.Name = colChamberDesc
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 100
        repoTextBox.Tag = colChamberDesc
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = colICode
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.ReadOnly = False
        repoTextBox.Width = 100
        repoTextBox.Tag = colICode
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Item Description"
        repoTextBox.Name = colIName
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 200
        repoTextBox.Tag = colIName
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "UOM"
        repoTextBox.Name = colUOM
        repoTextBox.ReadOnly = True
        repoTextBox.Width = 60
        repoTextBox.Tag = colUOM
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Qty In Kg"
        repoNumBox.Name = ColQtyInKg
        repoNumBox.Width = 100
        repoNumBox.ReadOnly = False
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.Tag = ColQtyInKg
        gv.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Intermittent Dispatch No"
        repoTextBox.Name = ColIntermittentDispatchNo
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        repoTextBox.Tag = ColIntermittentDispatchNo
        gv.MasterTemplate.Columns.Add(repoTextBox)

        If pFields Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("Type"), "CF") = CompairStringResult.Equal Then
                    CfColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    FATColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    SNFColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
                    CLRColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then 'And (clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") <> CompairStringResult.Equal) And (clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") <> CompairStringResult.Equal)
                    repoDecimalColumn = New GridViewDecimalColumn()
                    repoDecimalColumn.Name = dt.Rows(i)("Code")
                    repoDecimalColumn.FormatString = "{0:n3}"
                    repoDecimalColumn.Width = 120
                    repoDecimalColumn.DecimalPlaces = 3
                    If clsCommon.CompairString(dt.Rows(i)("Code"), SNFColName) = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Code"), FATColName) = CompairStringResult.Equal Then
                        repoDecimalColumn.FormatString = "{0:n3}"
                        repoDecimalColumn.DecimalPlaces = 3
                        If settTankerDispatchAvgFATSNFPer Then
                            repoDecimalColumn.DecimalPlaces = 10
                        End If
                    End If
                    repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                    repoDecimalColumn.Tag = dt.Rows(i)("Type")
                    If AllowToEnterSnfAtPlantInMccTankerDispatch = True AndAlso clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value)) Then
                        repoDecimalColumn.ReadOnly = False
                    ElseIf clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value)) Then
                        repoDecimalColumn.ReadOnly = True
                    Else
                        repoDecimalColumn.ReadOnly = False
                    End If
                    gv.MasterTemplate.Columns.Add(repoDecimalColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "A") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = OpenParameterValueList(dt.Rows(i)("Code"))
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gv.MasterTemplate.Columns.Add(repoComboColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "B") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = FillYesNoValue()
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gv.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    repoTextColumn.ReadOnly = False
                    gv.MasterTemplate.Columns.Add(repoTextColumn)
                End If

                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    gv.Columns.Add(colAutoFat, "Auto FAT")
                    gv.Columns(colAutoFat).Width = 120
                    gv.Columns(colAutoFat).ReadOnly = settTankerDispatchAvgFATSNFPer
                    gv.Columns(colAutoFat).Tag = "AutoFAT"
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    gv.Columns.Add(colAutoSnf, "Auto SNF")
                    gv.Columns(colAutoSnf).Width = 120
                    gv.Columns(colAutoSnf).ReadOnly = settTankerDispatchAvgFATSNFPer
                    gv.Columns(colAutoSnf).Tag = "AutoSNF"
                End If

            Next
            If clsCommon.myLen(FATColName) > 0 Then
                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colFatKG
                repoDecimalColumn.FormatString = "{0:n3}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 3
                repoDecimalColumn.HeaderText = "FAT KG"
                repoDecimalColumn.Tag = colFatKG
                repoDecimalColumn.ReadOnly = True
                gv.MasterTemplate.Columns.Add(repoDecimalColumn)

                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colFatRate
                repoDecimalColumn.FormatString = "{0:n2}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 2
                repoDecimalColumn.HeaderText = "FAT Rate"
                repoDecimalColumn.Tag = colFatRate
                repoDecimalColumn.ReadOnly = True
                gv.MasterTemplate.Columns.Add(repoDecimalColumn)
            End If
            If clsCommon.myLen(SNFColName) > 0 Then
                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colSNFKG
                repoDecimalColumn.FormatString = "{0:n3}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 3
                repoDecimalColumn.HeaderText = "SNF KG"
                repoDecimalColumn.Tag = colSNFKG
                repoDecimalColumn.ReadOnly = True
                gv.MasterTemplate.Columns.Add(repoDecimalColumn)

                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colSNFRate
                repoDecimalColumn.FormatString = "{0:n2}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 2
                repoDecimalColumn.HeaderText = "SNF Rate"
                repoDecimalColumn.Tag = colSNFRate
                repoDecimalColumn.ReadOnly = True
                gv.MasterTemplate.Columns.Add(repoDecimalColumn)
            End If

            If clsCommon.myLen(SNFColName) > 0 AndAlso clsCommon.myLen(FATColName) > 0 Then
                repoDecimalColumn = New GridViewDecimalColumn()
                repoDecimalColumn.Name = colAmount
                repoDecimalColumn.FormatString = "{0:n2}"
                repoDecimalColumn.Width = 120
                repoDecimalColumn.DecimalPlaces = 2
                repoDecimalColumn.HeaderText = "Amount"
                repoDecimalColumn.Tag = colAmount
                repoDecimalColumn.ReadOnly = True
                gv.MasterTemplate.Columns.Add(repoDecimalColumn)
            End If
            gv.Columns.Add(colRemarks, "Remarks")
            gv.Columns(colRemarks).Width = 300
            gv.Columns(colRemarks).ReadOnly = False
            gv.Columns(colRemarks).Tag = colRemarks
            gv.Columns(colRemarks).WrapText = True
        Else
            Throw New Exception("There is No parameter defined in Parameter Master. Please Define It First")
        End If


        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.AllowRowReorder = False
        gv.EnableFiltering = False
        gv.EnableSorting = False
        gv.EnableGrouping = False
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True
        ReStoreGridLayout()
        isCellValueChangedOpen = False
        repoTextColumn = Nothing
        repoDecimalColumn = Nothing
        repoComboColumn = Nothing
    End Sub
    Sub SetTankerDetail()

        LoadBlankGridAE()
        If clsCommon.myLen(fndTnakerNo.Value) > 0 Then
            Dim i As Integer = 0
            Dim ArrTankerChamber As List(Of clsTankerChamberDetail) = clsTankerChamberDetail.GetData(fndTnakerNo.Value)
            If ArrTankerChamber IsNot Nothing AndAlso ArrTankerChamber.Count > 0 Then
                Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(settTankerDispatchAvgFATSNFPer, True, False, False, "", "MI", "", fndMCCCode.Value, 1, "", 1, 1, dtpDateAndTime.Value, dtpDateAndTime.Value, False, Nothing, fndDocNo.Value)
                For Each objtr As clsTankerChamberDetail In ArrTankerChamber
                    gvAE.Rows.AddNew()
                    gvAE.Rows(gvAE.RowCount - 1).Cells(colChamberNo).Value = objtr.Chamber_No
                    gvAE.Rows(gvAE.RowCount - 1).Cells(colChamberDesc).Value = objtr.Chamber_Description
                    gvAE.Rows(gvAE.RowCount - 1).Cells(colICode).Value = clsCommon.myCstr(gv.Rows(i).Cells(colICode).Value)
                    gvAE.Rows(gvAE.RowCount - 1).Cells(colIName).Value = clsCommon.myCstr(gv.Rows(i).Cells(colIName).Value)
                    gvAE.Rows(gvAE.RowCount - 1).Cells(colUOM).Value = clsCommon.myCstr(gv.Rows(i).Cells(colUOM).Value)

                    If ApplyManualTransferRateOnMultipleChamberTankerDispatch = 1 OrElse ApplyTotalSolidPriceChart Then
                        gvAE.Rows(gvAE.RowCount - 1).Cells(colFatRate).Value = 0
                        gvAE.Rows(gvAE.RowCount - 1).Cells(colSNFRate).Value = 0
                    Else
                        If objMCT IsNot Nothing Then
                            gvAE.Rows(gvAE.RowCount - 1).Cells(colFatRate).Value = objMCT.FAT_Cost
                            gvAE.Rows(gvAE.RowCount - 1).Cells(colSNFRate).Value = objMCT.SNF_Cost
                        End If
                    End If

                    If settTankerDispatchAvgFATSNFPer Then
                        gvAE.Rows(gvAE.RowCount - 1).Cells(FATColName).Value = objMCT.FAT_Per
                        gvAE.Rows(gvAE.RowCount - 1).Cells(SNFColName).Value = objMCT.SNF_Per
                    End If
                    gvAE.Rows(gvAE.RowCount - 1).Cells(colFatRate).Value = clsCommon.myCstr(gv.Rows(i).Cells(colFatRate).Value)
                    gvAE.Rows(gvAE.RowCount - 1).Cells(colSNFRate).Value = clsCommon.myCstr(gv.Rows(i).Cells(colSNFRate).Value)
                    If clsCommon.myLen(CfColName) > 0 Then
                        Try
                            gvAE.Rows(gvAE.RowCount - 1).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
                            If clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
                                gvAE.Columns(CfColName).IsVisible = False
                            End If
                        Catch exx As Exception
                        End Try
                    End If
                    If clsCommon.myLen(CLRColName) > 0 Then
                        Try
                            If clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
                                gvAE.Columns(CLRColName).IsVisible = False
                            End If
                        Catch exx As Exception
                        End Try
                    End If
                    i = i + 1
                Next
            Else
                clsCommon.MyMessageBoxShow("Please provide chamber of vehicle no" + fndTnakerNo.Value)
            End If
        End If
    End Sub
    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Private Sub fndPlantOrMCCCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPlantOrMCCCode._MYValidating
        Dim qry As String = String.Empty

        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select MCC Name from which dispatch is being made, First", Me.Text)
            Exit Sub
        End If

        If clsCommon.myLen(ddlTankerDispatchTo.Text) > 0 Then
            If clsCommon.CompairString(ddlTankerDispatchTo.Text, "PLANT") = CompairStringResult.Equal Then
                fndPlantOrMCCCode.Value = clsLocation.getFinder("Type='" & ddlTankerDispatchTo.Text & "' and location_code <>'" & fndMCCCode.Value & "'", fndPlantOrMCCCode.Value, isButtonClicked)
            ElseIf clsCommon.CompairString(ddlTankerDispatchTo.Text, "MCC") = CompairStringResult.Equal Then
                fndPlantOrMCCCode.Value = clsLocation.getFinder("Location_Category='" & ddlTankerDispatchTo.Text & "' and location_code <>'" & fndMCCCode.Value & "'", fndPlantOrMCCCode.Value, isButtonClicked)
            End If
            If clsCommon.myLen(fndPlantOrMCCCode.Value) > 0 Then
                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                ' lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select ' Tanker Dispatch To  ' type First ")
        End If
    End Sub

    Private Sub fndTnakerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTnakerNo._MYValidating
        If Not IsAutoTankerWeightment Then
            If Not isLoad Then
                ''By Balwinder for auto weighing on 06/07/2016
                Dim qry As String = "select * from  (select TSPL_MCC_GATE_ENTRY.Tanker_No, TSPL_MCC_WEIGHMENT.Document_No,TSPL_MCC_WEIGHMENT.Document_Date,TSPL_MCC_WEIGHMENT.Gate_Entry_No,TSPL_MCC_WEIGHMENT.Location_Code,TSPL_LOCATION_MASTER.Location_Desc, " + Environment.NewLine +
                " TSPL_MCC_GATE_ENTRY.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name," + Environment.NewLine +
                " Tare_Weight, Gross_Weight, Net_Weight" + Environment.NewLine +
                " from TSPL_MCC_WEIGHMENT " + Environment.NewLine +
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_WEIGHMENT.Location_Code " + Environment.NewLine +
                " left outer join TSPL_MCC_GATE_ENTRY on TSPL_MCC_GATE_ENTRY.Document_No=TSPL_MCC_WEIGHMENT.Gate_Entry_No " + Environment.NewLine +
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MCC_GATE_ENTRY.Transporter_Code " + Environment.NewLine +
                " where Status_Tare_Weight = 1 And Status_Gross_Weight = 1 " + Environment.NewLine +
                " and not exists(select 1 from TSPL_MCC_DISPATCH_CHALLAN where TSPL_MCC_DISPATCH_CHALLAN.MCC_Weighment_No=TSPL_MCC_WEIGHMENT.Document_No and TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO not in ('" + fndDocNo.Value + "')) )xx  "
                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("MCCDFTano", qry)
                If dr IsNot Nothing AndAlso dr.ItemArray.Count > 0 Then
                    fndTnakerNo.Value = clsCommon.myCstr(dr("Tanker_No"))
                    txtTankerTransporterName.Text = clsCommon.myCstr(dr("Transporter_Name"))

                End If
            End If
        ElseIf Not isLoadingBulkSaleTanker Then
            Dim qry As String = " select distinct TankerNo  ,[Tanker Transporter Code],[Tanker Transporter Name]  from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where 1=1 "
            If ShowTankerWithoutCheckingAnyValidation = False Then
                qry += " and isGateOut=1 "
            End If
            qry += " union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='" & fndTnakerNo.Value & "'  AND isGateOut=1 AND ISNULL(Ref_Doc_No,'')='' " &
               "union all select TSPL_MCC_Dispatch_Challan.Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master LEFT OUTER JOIN TSPL_MCC_Dispatch_Challan ON  tspl_tanker_master.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No where TSPL_MCC_Dispatch_Challan.Chalan_NO='" & fndDocNo.Value & "' )xx  "
            Dim whrCls As String = "  "

            fndTnakerNo.Value = clsCommon.ShowSelectForm("TNKRNO", qry, "TankerNo", whrCls, fndTnakerNo.Value, "TankerNo", isButtonClicked)
            txtTankerTransporterName.Text = getTransporterName(fndTnakerNo.Value)
        End If


    End Sub






    Private Sub FrmMccDispatch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rbtnReset.Enabled Then
            rbtnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReverse.Enabled Then
            btnReverse_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
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

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnPost.Visible = MyBase.isPostFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Function AllowToSave() As Boolean
        Try
            'For ii As Integer = 0 To gvAE.Rows.Count - 1
            '    If clsCommon.myCdbl(gvAE.Rows(ii).Cells(ColQtyInKg).Value) > 0 Then
            '        Dim objtr As New clsMCCDispatchDetail
            '        objtr.Chamber_No = clsCommon.myCstr(gvAE.Rows(ii).Cells(colChamberNo).Value)
            '        objtr.Item_Code = clsCommon.myCstr(gvAE.Rows(ii).Cells(colICode).Value)
            '        objtr.Item_UOM = clsCommon.myCstr(gvAE.Rows(ii).Cells(colUOM).Value)
            '        objtr.Qty_KG = clsCommon.myCdbl(gvAE.Rows(ii).Cells(ColQtyInKg).Value)
            '        arr.Add(objtr)
            '    End If
            'Next


            If AllowFutureDateTransaction(dtpDateAndTime.Value, Nothing) = False Then
                dtpDateAndTime.Focus()
                Return False
            End If



            If clsCommon.myLen(txtTankerDispatch.Value) <= 0 Then
                Throw New Exception("Please select Tanker Dispatch, first")
                errorControl.SetError(fndMCCCode, "Please select Tanker Dispatch, first ")
            Else
                errorControl.SetError(fndMCCCode, "")
            End If

            'For ii As Integer = 0 To gv.RowCount - 1
            '    If clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) > 0 Then


            '        If clsCommon.myCdbl(gv.Rows(ii).Cells(colFatRate).Value) <= 0 Then
            '            Throw New Exception("FAT rate is 0 in Grid, Please reselect the price chart or Re-enter FAT % in Grid at row no " + clsCommon.myCstr(ii))
            '        End If
            '        If clsCommon.myCdbl(gv.Rows(ii).Cells(colSNFRate).Value) <= 0 Then
            '            Throw New Exception("SNF rate is 0 in Grid, Please reselect the price chart or Re-enter SNF/CLR % in Grid at row no " + clsCommon.myCstr(ii))
            '        End If

            '        If clsCommon.myCdbl(gv.Rows(ii).Cells(colAmount).Value) <= 0 Then
            '            Throw New Exception("Amount is 0 in grid, Please check Price chart, FAT, SNF/CLR in Grid or Re-Enter it at row no " + clsCommon.myCstr(ii))
            '        End If
            '    End If
            'Next


            'Asked By Ranjana Mam(Vijaya)

            For ii As Integer = 0 To gvAE.RowCount - 1
                If clsCommon.myCdbl(gvAE.Rows(ii).Cells(ColQtyInKg).Value) > 0 Then
                    For i As Integer = 1 To gvAE.Columns.Count - 1
                        Dim ismandatoryvalue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select IsMandatory from TSPL_PARAMETER_MASTER where code='" + gvAE.Columns(i).Name + "'"))
                        If ismandatoryvalue > 0 AndAlso clsCommon.myLen(gvAE.Rows(ii).Cells(i).Value) <= 0 AndAlso clsCommon.CompairString(gvAE.Columns(i).Name, CLRColName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(gvAE.Columns(i).Name, CfColName) <> CompairStringResult.Equal Then
                            Throw New Exception("" & gvAE.Columns(i).HeaderText & " is Mandatory, please fill it.")
                        End If
                    Next
                End If
            Next





            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Sub SaveData(ByVal isPostbtnClick As Boolean)
        Try
            Dim obj As clsAcknowledgementEntry = Nothing
            obj = New clsAcknowledgementEntry()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If

            obj.MCC_Code = fndMCCCode.Value
            Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Document_No = fndDocNo.Value
            obj.MCC_Name = clsCommon.myCstr(txtMCCName.Text)
            obj.Document_Date = clsCommon.myCDate(dtpDateAndTime.Value, "dd/MMM/yyyy hh:mm tt")
            obj.Dispatch_Date = clsCommon.myCDate(txtdispatchDate.Value, "dd/MMM/yyyy hh:mm tt")
            obj.Tanker_Dispatch_To = clsCommon.myCstr(ddlTankerDispatchTo.Text)
            obj.Mcc_Or_Plant_Code = clsCommon.myCstr(fndPlantOrMCCCode.Value)
            obj.Tanker_No = clsCommon.myCstr(fndTnakerNo.Value)
            obj.Dispatch_Document_No = txtTankerDispatch.Value



            Dim i As Integer = 0
            Dim objParam As New Acknowledment_Entry_Chalan_Parameter
            obj.arrParmValue = New List(Of Acknowledment_Entry_Chalan_Parameter)
            For ii As Integer = 0 To gvAE.Rows.Count - 1
                For i = 0 To gvAE.Columns.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(gvAE.Rows(ii).Cells(i).Value).Trim) > 0 Then
                        If Not clsCommon.CompairString(gvAE.Columns(i).Name, ColIntermittentDispatchNo) = CompairStringResult.Equal Then
                            objParam = New Acknowledment_Entry_Chalan_Parameter
                            objParam.SNO = ii + 1
                            objParam.Document_No = clsCommon.myCstr(obj.Document_No)
                            objParam.Param_Field_Code = clsCommon.myCstr(gvAE.Columns(i).Name)
                            objParam.Param_Field_Desc = clsCommon.myCstr(gvAE.Columns(i).HeaderText)
                            objParam.Param_Field_Value = clsCommon.myCstr(gvAE.Rows(ii).Cells(i).Value)
                            objParam.Param_Type = IIf(clsCommon.CompairString(clsCommon.myCstr(gvAE.Columns(i).Tag), "") = CompairStringResult.Equal, "NA", clsCommon.myCstr(gvAE.Columns(i).Tag))
                            obj.arrParmValue.Add(objParam)
                        End If
                    End If
                Next
            Next


            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            End If

            obj.arr = New List(Of clsAcknowledgementEntryDetail)
            For ii As Integer = 0 To gvAE.Rows.Count - 1
                Dim objtr As New clsAcknowledgementEntryDetail
                objtr.SNo = ii + 1
                objtr.Chamber_No = clsCommon.myCstr(gvAE.Rows(ii).Cells(colChamberNo).Value)
                objtr.Chamber_Description = clsCommon.myCstr(gvAE.Rows(ii).Cells(colChamberDesc).Value)
                objtr.Item_Code = clsCommon.myCstr(gvAE.Rows(ii).Cells(colICode).Value)
                objtr.Item_Name = clsCommon.myCstr(gvAE.Rows(ii).Cells(colIName).Value)
                objtr.Item_UOM = clsCommon.myCstr(gvAE.Rows(ii).Cells(colUOM).Value)
                objtr.FAT_KG = clsCommon.myCdbl(gvAE.Rows(ii).Cells(colFatKG).Value)
                objtr.FAT_Rate = clsCommon.myCdbl(gvAE.Rows(ii).Cells(colFatRate).Value)
                objtr.SNF_Rate = clsCommon.myCdbl(gvAE.Rows(ii).Cells(colSNFRate).Value)
                objtr.SNF_KG = clsCommon.myCdbl(gvAE.Rows(ii).Cells(colSNFKG).Value)
                objtr.Amount = clsCommon.myCdbl(gvAE.Rows(ii).Cells(colAmount).Value)
                objtr.Qty_KG = clsCommon.myCdbl(gvAE.Rows(objtr.SNo - 1).Cells(ColQtyInKg).Value)

                obj.arr.Add(objtr)


            Next


            If clsAcknowledgementEntry.SaveData(obj) Then
                If Not isPostbtnClick Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                End If
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True
                btnPrint.Enabled = True
                btnPost.Enabled = True
                LoadData(obj.Document_No, NavigatorType.Current)
                Exit Sub
            End If
        Catch ex As Exception
            If isPostbtnClick Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Sub deleteData()
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
                If (clsAcknowledgementEntry.DeleteData(fndDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try



    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Try
            Dim obj As clsAcknowledgementEntry = clsAcknowledgementEntry.getData(strCode, navType)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0 Then
                Reset()
                isLoad = True
                fndMCCCode.Enabled = False
                fndMCCCode.Value = obj.MCC_Code
                txtMCCName.Text = obj.MCC_Name
                lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)
                dtpDateAndTime.Value = clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy hh:mm tt")
                fndDocNo.Value = clsCommon.myCstr(obj.Document_No)
                ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr(obj.Tanker_Dispatch_To)
                fndPlantOrMCCCode.Value = clsCommon.myCstr(obj.Mcc_Or_Plant_Code)
                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                txtdispatchDate.Value = clsCommon.myCDate(obj.Dispatch_Date, "dd/MM/yyyy hh:mm tt")
                txtTankerDispatch.Value = clsCommon.myCstr(obj.Dispatch_Document_No)
                fndTnakerNo.Value = clsCommon.myCstr(obj.Tanker_No)
                LoadBlankGrid()
                LoadBlankGridAE()

                For i As Integer = 0 To obj.arrParmValue.Count - 1
                    Try
                        While obj.arrParmValue(i).SNO > gvAE.Rows.Count
                            gvAE.Rows.AddNew()
                        End While
                        gvAE.Rows(obj.arrParmValue(i).SNO - 1).Cells(obj.arrParmValue(i).Param_Field_Code).Value = obj.arrParmValue(i).Param_Field_Value
                    Catch ex1 As Exception
                    End Try
                Next
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objtr As clsAcknowledgementEntryDetail In obj.arr
                        While objtr.SNo > gvAE.Rows.Count
                            gvAE.Rows.AddNew()
                        End While
                        gvAE.Rows(objtr.SNo - 1).Cells(colChamberNo).Value = objtr.Chamber_No
                        gvAE.Rows(objtr.SNo - 1).Cells(colChamberDesc).Value = objtr.Chamber_Description
                        gvAE.Rows(objtr.SNo - 1).Cells(colICode).Value = objtr.Item_Code
                        gvAE.Rows(objtr.SNo - 1).Cells(colIName).Value = objtr.Item_Name
                        gvAE.Rows(objtr.SNo - 1).Cells(colUOM).Value = objtr.Item_UOM
                        gvAE.Rows(objtr.SNo - 1).Cells(ColQtyInKg).Value = objtr.Qty_KG
                        gvAE.Rows(objtr.SNo - 1).Cells(colFatKG).Value = objtr.FAT_KG
                        gvAE.Rows(objtr.SNo - 1).Cells(colSNFKG).Value = objtr.SNF_KG
                        gvAE.Rows(objtr.SNo - 1).Cells(colFatRate).Value = objtr.FAT_Rate
                        gvAE.Rows(objtr.SNo - 1).Cells(colSNFRate).Value = objtr.SNF_Rate
                        gvAE.Rows(objtr.SNo - 1).Cells(colAmount).Value = objtr.Amount

                    Next
                End If
                Dim objMD As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Document_No, navType)

                If objMD IsNot Nothing AndAlso clsCommon.myLen(objMD.Chalan_NO) > 0 Then
                    For i As Integer = 0 To objMD.arrParmValue.Count - 1
                        Try
                            While objMD.arrParmValue(i).SNO > gv.Rows.Count
                                gv.Rows.AddNew()
                            End While
                            gv.Rows(obj.arrParmValue(i).SNO - 1).Cells(objMD.arrParmValue(i).Param_Field_Code).Value = objMD.arrParmValue(i).Param_Field_Value
                        Catch ex1 As Exception
                        End Try
                    Next
                    If objMD.arr IsNot Nothing AndAlso objMD.arr.Count > 0 Then
                        For Each objtr As clsMCCDispatchDetail In objMD.arr
                            While objtr.SNo > gv.Rows.Count
                                gv.Rows.AddNew()
                            End While
                            gv.Rows(objtr.SNo - 1).Cells(colChamberNo).Value = objtr.Chamber_No
                            gv.Rows(objtr.SNo - 1).Cells(colChamberDesc).Value = objtr.Chamber_Description
                            gv.Rows(objtr.SNo - 1).Cells(colICode).Value = objtr.Item_Code
                            gv.Rows(objtr.SNo - 1).Cells(colIName).Value = objtr.Item_Name
                            gv.Rows(objtr.SNo - 1).Cells(colUOM).Value = objtr.Item_UOM
                            gv.Rows(objtr.SNo - 1).Cells(ColQtyInKg).Value = objtr.Qty_KG
                            gv.Rows(objtr.SNo - 1).Cells(colFatKG).Value = objtr.FAT_KG
                            gv.Rows(objtr.SNo - 1).Cells(colSNFKG).Value = objtr.SNF_KG
                            gv.Rows(objtr.SNo - 1).Cells(colFatRate).Value = objtr.FAT_Rate
                            gv.Rows(objtr.SNo - 1).Cells(colSNFRate).Value = objtr.SNF_Rate
                            gv.Rows(objtr.SNo - 1).Cells(colAmount).Value = objtr.Amount

                        Next
                    End If
                End If


                If obj.isPosted = 0 Then
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                    btnPrint.Enabled = True
                    btnPost.Enabled = True
                    btnReverse.Enabled = False

                Else
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPrint.Enabled = True
                    btnPost.Enabled = False
                    btnReverse.Enabled = True
                End If
            Else
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isLoad = False
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            SaveData(False)
        End If
    End Sub

    Private Sub fndChalanNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndChalanNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        fndDocNo.Value = clsAcknowledgementEntry.getFinder(whrCls, fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            LoadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub

    Function getTransporterName(ByVal strTankerNo As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name   from TSPL_vendor_master where Vendor_Code=(select Tanker_Transporter_Code  from TSPL_TANKER_MASTER where Tanker_No='" & strTankerNo & "' )"))
        Return str
    End Function



    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not AllowToSave() Then
                    Exit Sub
                End If
                SaveData(True)


                If (clsAcknowledgementEntry.PostData(MyBase.Form_ID, fndDocNo.Value)) Then
                    msg = "Successfully Posted"

                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub


    Private Sub mnuDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuSaveLayOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveLayOut.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsAcknowledgementEntry.ReverseAndUnpost(fndDocNo.Value, Nothing) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub calculateSNF()
        Dim isParamOK As Boolean = True
        Dim snfField As String = ""
        Dim fatField As String = ""
        Dim clrField As String = ""
        Dim cfField As String = ""
        For ii As Integer = 0 To gvAE.RowCount - 1
            For i As Integer = 0 To gvAE.Columns.Count - 1
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvAE.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvAE.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gvAE.Rows(ii).Cells(i).Value) <= 0 Or (Not IsNumeric(gvAE.Rows(ii).Cells(i).Value)) Then
                        isParamOK = False
                    End If
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    snfField = gvAE.Columns(i).Name
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                    fatField = gvAE.Columns(i).Name
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    clrField = gvAE.Columns(i).Name
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                    cfField = gvAE.Columns(i).Name
                End If
            Next
            If isParamOK Then
                gvAE.Rows(ii).Cells(snfField).Value = clsCommon.myFormat(clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvAE.Rows(ii).Cells(fatField).Value), clsCommon.myCdbl(gvAE.Rows(ii).Cells(clrField).Value), clsCommon.myCdbl(gvAE.Rows(ii).Cells(cfField).Value)))
            Else
                gvAE.Rows(ii).Cells(snfField).Value = clsCommon.myFormat(0)
            End If
        Next


    End Sub
    Sub calculateCLR()
        Dim isParamOK As Boolean = True
        Dim snfField As String = ""
        Dim fatField As String = ""
        Dim clrField As String = ""
        Dim cfField As String = ""
        For ii As Integer = 0 To gvAE.RowCount - 1
            For i As Integer = 0 To gvAE.Columns.Count - 1
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvAE.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvAE.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gvAE.Rows(ii).Cells(i).Value) <= 0 Or (Not IsNumeric(gvAE.Rows(ii).Cells(i).Value)) Then
                        isParamOK = False
                    End If
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    snfField = gvAE.Columns(i).Name
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                    fatField = gvAE.Columns(i).Name
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    clrField = gvAE.Columns(i).Name
                End If
                If clsCommon.CompairString(gvAE.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                    cfField = gvAE.Columns(i).Name
                End If
            Next
            If isParamOK Then
                gvAE.Rows(ii).Cells(clrField).Value = clsCommon.myFormat(clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(gvAE.Rows(ii).Cells(fatField).Value), clsCommon.myCdbl(gvAE.Rows(ii).Cells(snfField).Value), clsCommon.myCdbl(gvAE.Rows(ii).Cells(cfField).Value)))
            Else
                gvAE.Rows(ii).Cells(clrField).Value = clsCommon.myFormat(0)
            End If
        Next


    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("MDfndnder", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
        End If
    End Sub

    Private Sub Calculate()
        For ii As Integer = 0 To gv.Rows.Count - 1
            CalculateCurrentRow(ii)
        Next
    End Sub

    Private Sub CalculateCurrentRow(ByVal rowNo As Integer)
        gvAE.Rows(rowNo).Cells(colFatKG).Value = Math.Round(clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(FATColName).Value) / 100, 3, MidpointRounding.ToEven)
        gvAE.Rows(rowNo).Cells(colSNFKG).Value = Math.Round(clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(SNFColName).Value) / 100, 3, MidpointRounding.ToEven)
        gvAE.Rows(rowNo).Cells(colAmount).Value = Math.Round((clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(colFatRate).Value) * clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(colFatKG).Value)) + (clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(colSNFRate).Value) * clsCommon.myCdbl(gvAE.Rows(rowNo).Cells(colSNFKG).Value)), 2)
    End Sub


    Private Sub fndMCCCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_code in(" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If

        fndMCCCode.Value = clsLocation.getFinder(whrCls, fndMCCCode.Value, isButtonClicked)
        txtMCCName.Text = clsLocation.GetName(fndMCCCode.Value, Nothing)
        'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndMCCCode.Value & "'  union select TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where MCC_Code='" & fndMCCCode.Value & "'"))
        lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)
        LoadBlankGrid()

    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs)
        clsOpenInventory.ShowInventoryDatails(fndDocNo.Value)
    End Sub




    Private Sub txtTankerDispatch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerDispatch._MYValidating
        Dim whrCls As String = String.Empty
        'If Not clsMccMaster.isCurrentUserHO() Then
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        whrCls = " location_code in(" & objCommonVar.strCurrUserLocations & ") and"
        '    End If
        'End If

        whrCls += "  isPosted=1 and Chalan_NO not in (select distinct challan_no from tspl_gate_entry_details where tspl_gate_entry_details.gate_entry_No<>'' union all select distinct Dispatch_Document_No from TSPL_ACKNOWLEDGENT_ENTRY_header where TSPL_ACKNOWLEDGENT_ENTRY_header.Document_No<>'" & fndDocNo.Value & "') and Chalan_NO not in (select distinct Challan_No from TSPL_MCC_DISPATCH_TRANSFER where isPosted=0) and not exists(select 1 from TSPL_MCC_Dispatch_Challan_Return where TSPL_MCC_Dispatch_Challan_Return.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO )"
        Dim qry As String = " select tspl_mcc_dispatch_challan.Tanker_No as [TankerNo] , tspl_mcc_dispatch_challan.Chalan_NO as ChallanNo,tspl_mcc_dispatch_challan.MCC_Code as [Mcc Code] ,tspl_mcc_dispatch_challan.MCC_Name as [Mcc Name] ,tspl_mcc_dispatch_challan.Dispatch_Date as [Dispatch Date]  ,tspl_mcc_dispatch_challan.Tanker_Dispatch_To as [Tanker Dispatch To] ,tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code as [Mcc Or Plant Code]  From tspl_mcc_dispatch_challan  "
        txtTankerDispatch.Value = clsCommon.ShowSelectForm("TDFinder", qry, "ChallanNo", whrCls, txtTankerDispatch.Value, "ChallanNo", isButtonClicked)


        If clsCommon.myLen(clsCommon.myCstr(txtTankerDispatch.Value)) > 0 Then
            LoadData_TankerDispatch(txtTankerDispatch.Value)
            SetTankerDetail()
        Else
            Reset()
        End If
    End Sub
    Sub LoadData_TankerDispatch(ByVal strCode As String)
        Try
            Dim obj As clsMccDispatch = clsMccDispatch.getData(strCode, NavigatorType.Current)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Chalan_NO) > 0 Then


                fndMCCCode.Value = obj.MCC_Code
                txtMCCName.Text = obj.MCC_Name
                lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)
                txtdispatchDate.Value = clsCommon.myCDate(obj.Dispatch_Date, "dd/MM/yyyy hh:mm tt")

                ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr(obj.Tanker_Dispatch_To)
                fndPlantOrMCCCode.Value = clsCommon.myCstr(obj.Mcc_Or_Plant_Code)
                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")

                isLoadingBulkSaleTanker = obj.isBulkSaleData
                fndTnakerNo.Value = clsCommon.myCstr(obj.Tanker_No)


                LoadBlankGrid()
                For i As Integer = 0 To obj.arrParmValue.Count - 1
                    Try
                        While obj.arrParmValue(i).SNO > gv.Rows.Count
                            gv.Rows.AddNew()
                        End While
                        gv.Rows(obj.arrParmValue(i).SNO - 1).Cells(obj.arrParmValue(i).Param_Field_Code).Value = obj.arrParmValue(i).Param_Field_Value
                    Catch ex1 As Exception
                    End Try
                Next
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objtr As clsMCCDispatchDetail In obj.arr
                        While objtr.SNo > gv.Rows.Count
                            gv.Rows.AddNew()
                        End While
                        gv.Rows(objtr.SNo - 1).Cells(colChamberNo).Value = objtr.Chamber_No
                        gv.Rows(objtr.SNo - 1).Cells(colChamberDesc).Value = objtr.Chamber_Description
                        gv.Rows(objtr.SNo - 1).Cells(colICode).Value = objtr.Item_Code
                        gv.Rows(objtr.SNo - 1).Cells(colIName).Value = objtr.Item_Name
                        gv.Rows(objtr.SNo - 1).Cells(colUOM).Value = objtr.Item_UOM
                        gv.Rows(objtr.SNo - 1).Cells(ColQtyInKg).Value = objtr.Qty_KG
                        gv.Rows(objtr.SNo - 1).Cells(colFatKG).Value = objtr.FAT_KG
                        gv.Rows(objtr.SNo - 1).Cells(colSNFKG).Value = objtr.SNF_KG
                        gv.Rows(objtr.SNo - 1).Cells(colFatRate).Value = objtr.FAT_Rate
                        gv.Rows(objtr.SNo - 1).Cells(colSNFRate).Value = objtr.SNF_Rate
                        gv.Rows(objtr.SNo - 1).Cells(colAmount).Value = objtr.Amount
                        gv.Rows(objtr.SNo - 1).Cells(ColIntermittentDispatchNo).Value = objtr.Intermittent_Dispatch_No
                    Next
                End If




            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub gvAE_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvAE.CellValueChanged

        'If Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
        If Not isCellValueChangedOpen Then

                isCellValueChangedOpen = True
            If clsCommon.CompairString(gvAE.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvAE.CurrentColumn.Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvAE.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Then
                calculateSNF()
            End If
            If clsCommon.CompairString(gvAE.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvAE.CurrentColumn.Tag, "SNF") = CompairStringResult.Equal Then
                calculateCLR()
            End If
            If e.Column Is gvAE.Columns(FATColName) OrElse e.Column Is gvAE.Columns(SNFColName) OrElse e.Column Is gvAE.Columns(ColQtyInKg) Then
                CalculateCurrentRow(gvAE.CurrentRow.Index)
            End If
            isCellValueChangedOpen = False
            End If
            'End If
            If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvAE.Columns(colICode) Then
                Dim whr As String = " Product_Type ='mi' and exists (select  1 from tspl_item_uom_detail where tspl_item_uom_detail.Item_Code=TSPL_ITEM_MASTER.Item_Code and UOM_Code='KG')"
                gvAE.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(whr, gvAE.CurrentRow.Cells(colICode).Value, False)
                If clsCommon.myLen(gvAE.CurrentRow.Cells(colICode).Value) > 0 Then
                    gvAE.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(gvAE.CurrentRow.Cells(colICode).Value, Nothing)
                    gvAE.CurrentRow.Cells(colUOM).Value = "KG"
                Else
                    gvAE.CurrentRow.Cells(colIName).Value = ""
                    gvAE.CurrentRow.Cells(colUOM).Value = "KG"
                End If
            End If
            isCellValueChangedOpen = False
        End If

    End Sub
End Class

