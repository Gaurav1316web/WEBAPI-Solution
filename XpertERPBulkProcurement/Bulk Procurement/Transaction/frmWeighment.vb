' Done By Pankaj Jha on dated 14/07/2014 against ticket no: BM00000002725
'done by priti ERO/10/05/18-000306
Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmWeighment
    Inherits FrmMainTranScreen
    Dim FinalChamberwise As Integer = 0
    Dim MCCChamberwise As Integer = 0
    Dim AllowBulkProcTransDateSameasGateEntryDate As Integer = 0
    Public AllowCanInformationintoGridForTankerDispatch As Boolean = False
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim AllowRandomOnlyOneSecondaryQC As Integer = 0
    Dim AllowBulkProcMCCwithoutTankerDispatch As Integer = 0
    Dim blnLoad As Boolean = False
    Dim TankerFromMaster As Integer = 0
    Dim AllowbulkProcurementSequencewise As Integer = 0
    Dim FirstQualityThenWeighment As Integer = 0
    Public Const colIsCanType As String = "colIsCanType"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colDIPStatus As String = "colDIPStatus"
    Public Const colSampleLifted As String = "colSampleLifted"
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colQty As String = "Qty"
    Public Const colHSN As String = "HSN"
    Public Const colWeighmentSeqNo As String = "colWeighmentSeqNo"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colDipValue As String = "colDipValue"
    Public Const colSubLoc As String = "colSubLoc"
    Public strDocCode As String = ""
    Dim docType As String = String.Empty
    Dim obj As clsWeighment = Nothing
    Dim isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isGrossWeightReadOnly As Boolean = True
    Dim isTareWeightReadOnly As Boolean = True
    Dim IsWeighmentUnloadingSequencewise As Integer = 0
    Dim NetWeightWithVendorWeight As Integer = 0
    Public Const colVendorWeight As String = "colVendorWeight"
    Dim AllowFractionInMCCTankerDispatchGrossQty As Boolean = False
    Dim EnterWeightManuallyOnWeighmentInGrid As Boolean = False
    Dim VendorWeight As Decimal = 0
    Dim isNetWeight As Boolean = False
    Dim settTankerDispatchIntermittentSingleGateIn As Boolean = False
    Dim intBulkProcRunOneTypeGateEntry As Integer

    Private Sub FrmWeighment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Panel3.Enabled = False
        MCCChamberwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, Nothing))
        AllowBulkProcTransDateSameasGateEntryDate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcTransDateSameasGateEntryDate, clsFixedParameterCode.AllowBulkProcTransDateSameasGateEntryDate, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        AllowBulkProcMCCwithoutTankerDispatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcMCCwithoutTankerDispatch, clsFixedParameterCode.AllowBulkProcMCCwithoutTankerDispatch, Nothing))
        SetUserMgmtNew()
        AllowCanInformationintoGridForTankerDispatch = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCanInformationintoGridForTankerDispatch, clsFixedParameterCode.AllowCanInformationintoGridForTankerDispatch, Nothing)) = 0, False, True)
        AllowRandomOnlyOneSecondaryQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowRandomOnlyOneSecondaryQC, clsFixedParameterCode.AllowRandomOnlyOneSecondaryQC, Nothing))
        IsWeighmentUnloadingSequencewise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowUnloadingandWeighmentSequencewise, clsFixedParameterCode.ShowUnloadingandWeighmentSequencewise, Nothing))
        AllowbulkProcurementSequencewise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcurementSequencewise, clsFixedParameterCode.AllowBulkProcurementSequencewise, Nothing))
        FirstQualityThenWeighment = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QualityThenWeighmentinBulkProcurement, clsFixedParameterCode.QualityThenWeighmentinBulkProcurement, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        NetWeightWithVendorWeight = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcNetWeightCalculationWithVendorWeight, clsFixedParameterCode.BulkProcNetWeightCalculationWithVendorWeight, Nothing))
        AllowFractionInMCCTankerDispatchGrossQty = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowFractionInMCCTankerDispatchGrossQty, clsFixedParameterCode.AllowFractionInMCCTankerDispatchGrossQty, Nothing)) = 1, True, False)
        EnterWeightManuallyOnWeighmentInGrid = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnterWeightManuallyOnWeighmentInGrid, clsFixedParameterCode.EnterWeightManuallyOnWeighmentInGrid, Nothing)) = 1, True, False)
        settTankerDispatchIntermittentSingleGateIn = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, Nothing)) = 1)
        intBulkProcRunOneTypeGateEntry = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcRunOneTypeGateEntry, clsFixedParameterCode.BulkProcRunOneTypeGateEntry, Nothing))
        If intBulkProcRunOneTypeGateEntry = 1 Then
            grpGateEntryType.Visible = False
            chkBulkMilkProc.IsChecked = True
            chkBothDoc.Checked = False
            chkBothDoc.Visible = False
        ElseIf intBulkProcRunOneTypeGateEntry = 2 Then
            grpGateEntryType.Visible = False
            chkMccProc.IsChecked = True
            chkBothDoc.Checked = False
            chkBothDoc.Visible = False
        Else
            grpGateEntryType.Visible = True
        End If

        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        dtpTareWeight.Enabled = True

        If clsCommon.myLen(strDocCode) > 0 Then
            loadData(strDocCode, "", NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), "", NavigatorType.Current)
        End If

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoTankerWeightment, clsFixedParameterCode.IsAutoTankerWeightment, Nothing)) > 0 Then
            UcWeighing1.Enabled = True
            UcWeighing1.form_ID = MyBase.Form_ID
            UcWeighing1.LoadPortAndMachine()
            UcWeighing1.LoadSettingAndStart()
        Else
            UcWeighing1.Enabled = False
        End If

        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
        Me.Close()
        GC.Collect()
    End Sub
    Sub reset()
        fndRefrencesNo.Value = ""
        isNetWeight = False
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        txtGrossWt.Value = 0
        isGrossWeightReadOnly = True
        fndTankerNo.Value = ""
        fndDocNO.Value = ""
        fndDocNO.MyReadOnly = False
        fndGateEntryNoBulk.Value = ""

        lblLocationCodeBulk.Text = ""
        lblLocationNameBulk.Text = ""
        lblVendorCodeBulk.Text = ""
        lblVendorNameBulk.Text = ""
        lblChallanNoBulk.Text = ""


        lblTankerNoBulk.Text = ""
        lblStatusBulk.Text = ""
        txtDipValue.Text = ""
        txtDipValue.ReadOnly = False
        dtpWeighmentDateBulk.Value = clsCommon.GETSERVERDATE()
        dtpWeighmentDateBulk.Enabled = True
        'lblChallanDateBulk.Text = ""
        txtChallanDate.Value = dtpWeighmentDateBulk.Value
        'dtpTareWeight.Enabled = False
        dtpTareWeight.Value = dtpWeighmentDateBulk.Value
        'lblGateEntryDateAndTimeValueBulk.Text = ""
        txtGateEntryDate.Value = dtpWeighmentDateBulk.Value
        loadBlankGrid()
        If chkBulkMilkProc.IsChecked Then
            lblVendorBulk.Text = "Vendor"
            chkTankerReturn.Visible = True
        Else
            lblVendorBulk.Text = "From MCC"
            chkTankerReturn.Visible = False
        End If
        chkTankerReturn.Checked = False
        txtWeighmentSlipNo.Text = ""
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        btnPost.Enabled = False
        btnReverse.Visible = False
        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpWeighmentDateBulk.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpTareWeight.CustomFormat = "dd/MM/yyyy hh:mm tt"
            txtGateEntryDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpWeighmentDateBulk.CustomFormat = "dd/MM/yyyy"
            dtpTareWeight.CustomFormat = "dd/MM/yyyy"
            txtGateEntryDate.CustomFormat = "dd/MM/yyyy"
        End If
        txtChallanDate.CustomFormat = "dd/MM/yyyy"
        '==========================================================
        If chkBulkMilkProc.IsChecked Then
            If TankerFromMaster = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        ElseIf chkMccProc.IsChecked Then
            If MCCChamberwise = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        End If
        If FinalChamberwise = 1 Then
            Panel1.Visible = True
        End If
    End Sub
    Sub loadBlankGrid()
        If chkBulkMilkProc.IsChecked Then
            loadBlankGvItemBulk()
        Else
            loadBlankGvItemMcc()
        End If
        ReStoreGridLayout()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmWeighment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
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
    Sub loadBlankGvItemBulk()
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing

        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True


        If AllowCanInformationintoGridForTankerDispatch = True AndAlso chkMccProc.IsChecked = True Then
            Dim repoisCanType As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoisCanType.HeaderText = "Is Can"
            repoisCanType.Name = colIsCanType
            repoisCanType.ReadOnly = True
            repoisCanType.IsVisible = True
            repoisCanType.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gvItemBulk.MasterTemplate.Columns.Add(repoisCanType)
        End If


        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).ReadOnly = True

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItemBulk.Columns(colChamberDesc).Width = 150
        gvItemBulk.Columns(colChamberDesc).ReadOnly = True
        gvItemBulk.Columns(colChamberDesc).IsVisible = False

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = True

        gvItemBulk.Columns.Add(colQty, "Qty ")
        gvItemBulk.Columns(colQty).Width = 120
        gvItemBulk.Columns(colQty).IsVisible = False
        gvItemBulk.Columns(colQty).ReadOnly = True

        gvItemBulk.Columns.Add(colHSN, "HSN Code ")
        gvItemBulk.Columns(colHSN).Width = 120
        gvItemBulk.Columns(colHSN).ReadOnly = True

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 75
        gvItemBulk.Columns(colFat).ReadOnly = True
        gvItemBulk.Columns(colFat).IsVisible = False

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 75
        gvItemBulk.Columns(colSNF).ReadOnly = True
        gvItemBulk.Columns(colSNF).IsVisible = False


        gvItemBulk.Columns.Add(colFatKG, "FAT KG")
        gvItemBulk.Columns(colFatKG).Width = 75
        gvItemBulk.Columns(colFatKG).ReadOnly = True
        gvItemBulk.Columns(colFatKG).IsVisible = False

        gvItemBulk.Columns.Add(colSNFKG, "SNF KG")
        gvItemBulk.Columns(colSNFKG).Width = 75
        gvItemBulk.Columns(colSNFKG).ReadOnly = True
        gvItemBulk.Columns(colSNFKG).IsVisible = False

        gvItemBulk.Columns.Add(colGrossWeight, "Gross Weight")
        gvItemBulk.Columns(colGrossWeight).Width = 75
        gvItemBulk.Columns(colGrossWeight).ReadOnly = False
        isGrossWeightReadOnly = False


        gvItemBulk.Columns.Add(colTareWeight, "Tare Weight")
        gvItemBulk.Columns(colTareWeight).Width = 75
        gvItemBulk.Columns(colTareWeight).ReadOnly = True
        isTareWeightReadOnly = True

        gvItemBulk.Columns.Add(colNetWeight, "Net Weight")
        gvItemBulk.Columns(colNetWeight).Width = 75
        gvItemBulk.Columns(colNetWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colVendorWeight, "Vendor Weight")
        gvItemBulk.Columns(colVendorWeight).Width = 75
        If NetWeightWithVendorWeight = 0 Then gvItemBulk.Columns(colVendorWeight).IsVisible = False
        gvItemBulk.Columns(colVendorWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colSampleLifted, "Sample Lifted")
        gvItemBulk.Columns(colSampleLifted).Width = 75
        gvItemBulk.Columns(colSampleLifted).ReadOnly = True
        gvItemBulk.Columns(colSampleLifted).IsVisible = False

        gvItemBulk.Columns.Add(colDIPStatus, "DIP Status")
        gvItemBulk.Columns(colDIPStatus).Width = 75
        gvItemBulk.Columns(colDIPStatus).ReadOnly = True
        gvItemBulk.Columns(colDIPStatus).IsVisible = False

        gvItemBulk.Columns.Add(colWeighmentSeqNo, "Sequence No")
        gvItemBulk.Columns(colWeighmentSeqNo).Width = 120
        gvItemBulk.Columns(colWeighmentSeqNo).ReadOnly = True
        gvItemBulk.Columns(colWeighmentSeqNo).IsVisible = False

        If FinalChamberwise = 1 Then
            gvItemBulk.Columns(colFat).IsVisible = True
            gvItemBulk.Columns(colSNF).IsVisible = True
            gvItemBulk.Columns(colChamberDesc).IsVisible = True
            gvItemBulk.Columns(colSampleLifted).IsVisible = True
            gvItemBulk.Columns(colDIPStatus).IsVisible = True
            gvItemBulk.Columns(colWeighmentSeqNo).IsVisible = True
            If IsWeighmentUnloadingSequencewise = 0 Then
                gvItemBulk.Columns(colGrossWeight).ReadOnly = True
            End If
            If UcWeighing1.Enabled = True Then
                gvItemBulk.Columns(colTareWeight).ReadOnly = True
                txtGrossWt.ReadOnly = True
            End If

        End If

        gvItemBulk.Columns.Add(colSubLoc, "Silo Location")
        gvItemBulk.Columns(colSubLoc).Width = 150
        gvItemBulk.Columns(colSubLoc).ReadOnly = False
        gvItemBulk.Columns(colSubLoc).IsVisible = True

        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        gvItemBulk.AllowAddNewRow = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub
    Function allowToSave(ByVal IsPostingClick As Boolean) As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpWeighmentDateBulk.Value, Nothing) = False Then
                dtpWeighmentDateBulk.Select()
                Return False
            End If
            '=======================================================
            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                errorControl.SetError(fndTankerNo, "Please Select Tanker No First ")
                Throw New Exception("Please Select Tanker No    ")
            Else
                errorControl.SetError(fndTankerNo, "")
            End If
            If clsCommon.myLen(fndGateEntryNoBulk.Value) <= 0 Then
                errorControl.SetError(fndGateEntryNoBulk, "Please Select Gate Entry No First ")
                Throw New Exception("Please Select Gate Entry No    ")
            Else
                errorControl.SetError(fndGateEntryNoBulk, "")
            End If
            If FinalChamberwise = 0 Then
                If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value) < 0 Then
                    Throw New Exception("Gross Weight is mandatory, And it must not be negative in Grid at Row No 1")
                End If
                If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value) = 0 Then
                    Throw New Exception("Gross Weight is mandatory, And it must not be zero or blank in Grid at Row No 1")
                End If
                If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value) < 0 Then
                    Throw New Exception("Net Weight must not be negative in Grid at Row No 1")
                End If
                If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value) < 0 Then
                    Throw New Exception("Tare Weight must not be negative in Grid at Row No 1")
                End If
            End If
            If FinalChamberwise = 1 Then
                If isNetWeight = False Then
                    If clsCommon.myCdbl(txtGrossWt.Value) = 0 Then
                        Throw New Exception("Please enter Gross Weight on header.")
                    End If
                End If
                If clsCommon.myLen(fndDocNO.Value) > 0 AndAlso clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_UNLOADING where Tanker_No='" & fndTankerNo.Value & "'")) = 0 Then
                    txtGrossWt.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Gross_Weight_Header from TSPL_Weighment_Detail where Weighment_No='" & fndDocNO.Value & "'"))
                    If isNetWeight = False Then
                        Throw New Exception("Unloading is pending for this tanker " & fndTankerNo.Value & " .Please Create unloading ")
                    End If
                End If
                ''richa BHA/27/07/18-000200
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ConsiderSiloCapicityForStockIn, clsFixedParameterCode.ConsiderSiloCapicityForStockIn, Nothing)), "1") = CompairStringResult.Equal Then
                    For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                        Dim strsilo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_MILK_UNLOADING_CHEMBER_DETAILS.Sublocation_Code from TSPL_MILK_UNLOADING_CHEMBER_DETAILS left outer join TSPL_MILK_UNLOADING  on TSPL_MILK_UNLOADING_CHEMBER_DETAILS .Unloading_No =TSPL_MILK_UNLOADING.Unloading_No where TSPL_MILK_UNLOADING.Weighment_No ='" & clsCommon.myCstr(fndDocNO.Value) & "' and TSPL_MILK_UNLOADING_CHEMBER_DETAILS.Line_No ='" & clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colSlNo).Value) & "' and TSPL_MILK_UNLOADING_CHEMBER_DETAILS.Chamber_Desc ='" & clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colChamberDesc).Value) & "'"))
                        If clsCommon.myLen(strsilo) > 0 Then
                            Dim balqtyofvl As Double = clsCommon.myCdbl(ClsLoadingTanker.getBalance(clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colItemCode).Value), lblLocationCodeBulk.Text, strsilo, fndDocNO.Value, dtpTareWeight.Value, Nothing, "LTR"))
                            Dim itemQty As Double = clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colNetWeight).Value)
                            Dim DblFinalQty As Double = balqtyofvl + itemQty
                            Dim SiloCapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Silo_Capacity,0) from TSPL_LOCATION_MASTER where location_code='" & strsilo & "'"))
                            If DblFinalQty > SiloCapacity Then
                                Throw New Exception("Silo Qty should be less than or equal to " & SiloCapacity & " at row no " + clsCommon.myCstr(ii + 1) + "")
                            End If
                        End If
                    Next
                End If
            End If

            'If dtpWeighmentDateBulk.Value < clsCommon.myCDate(lblGateEntryDateAndTimeValueBulk.Text) Then
            If clsCommon.GetDateWithStartTime(dtpWeighmentDateBulk.Value) < clsCommon.GetDateWithStartTime(txtGateEntryDate.Value) Then
                Throw New Exception("Weighment Date can not be less than Gate Entry Date")
            End If
            '==================Added by preeti Gupta Against ticket No[KDI/13/06/18-000363]
            If clsCommon.GetDateWithStartTime(dtpTareWeight.Value) < clsCommon.GetDateWithStartTime(txtGateEntryDate.Value) Then
                Throw New Exception("Tare Date can not be less than Gate Entry Date")
            End If

            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_weighment_detail where gate_entry_no='" & fndGateEntryNoBulk.Value & "' and weighment_no <>'" & fndDocNO.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If

            If chkTankerReturn.Checked Then
                chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_quality_check where gate_entry_no='" & fndGateEntryNoBulk.Value & "'"))
                If chk > 0 Then
                    Throw New Exception("You cannot return this Tanker.This tanker is Already used in QC.")
                End If
            End If
            For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                If AllowFractionInMCCTankerDispatchGrossQty = False Then
                    Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colGrossWeight).Value))
                    If (clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colGrossWeight).Value) - intPart) > 0 Then
                        gvItemBulk.Rows(ii).Cells(colGrossWeight).Value = intPart
                        CalculateNetWeight(gvItemBulk.Rows(ii).Index)
                        Throw New Exception("Gross Weight must be integer")
                    End If
                    gvItemBulk.Rows(ii).Cells(colGrossWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.Rows(ii).Cells(colGrossWeight).Value, 0))

                    intPart = Math.Truncate(clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colTareWeight).Value))
                    If (clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colTareWeight).Value) - intPart) > 0 Then
                        gvItemBulk.Rows(ii).Cells(colTareWeight).Value = intPart
                        Throw New Exception("Tare Weight must be integer")
                    End If
                    gvItemBulk.Rows(ii).Cells(colTareWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.Rows(ii).Cells(colTareWeight).Value, 0))
                End If
                CalculateNetWeight(ii)
            Next
            'If IsPostingClick = True Then
            '    If isNetWeight = True Then
            '        For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
            '            If clsCommon.myLen(clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colSubLoc).Value)) <= 0 Then
            '                Throw New Exception("Please Select Silo" & " at row no " + clsCommon.myCstr(ii + 1) + "")
            '            End If
            '        Next
            '    End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Function SaveData(ByVal isPostbtnClick As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            obj = New clsWeighment()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans)
            If obj.isNewEntry Then
                If AllowBulkProcTransDateSameasGateEntryDate = 1 Then
                    dtpWeighmentDateBulk.Value = txtGateEntryDate.Value
                End If
                'Dim Isjobwork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(IsAgainstJobWork,0) from tspl_gate_entry_details where gate_entry_no='" & fndGateEntryNoBulk.Value & "'", trans))
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementCounterOnEntryType, clsFixedParameterCode.BulkProcurementCounterOnEntryType, trans)) = 0, False, True) ''Make Setting Balwinder
                If isPODocumentTypeWise AndAlso chkBulkMilkProc.IsChecked Then
                    Dim strGateEntryType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNoBulk.Value + "'", trans))
                    If clsCommon.myLen(strGateEntryType) <= 0 Then
                        Throw New Exception("Gate Entry Type not found on Gate Entry screen")
                    End If
                    If clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                        obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dtpWeighmentDateBulk.Value, clsDocType.Weighment, clsDocTransactionType.BulkProcPurchase, lblLocationCodeBulk.Text)
                    ElseIf clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                        obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dtpWeighmentDateBulk.Value, clsDocType.Weighment, clsDocTransactionType.BulkProcJobWork, lblLocationCodeBulk.Text)
                    Else
                        Throw New Exception("Wrong Gate Entry Type")
                    End If
                Else
                    If chkJobWork.Checked Then
                        obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dtpWeighmentDateBulk.Value, clsDocType.Weighment, IIf(chkBulkMilkProc.IsChecked, clsDocTransactionType.BulkProcJobWorkOutward, clsDocTransactionType.MCCProcJobWorkOutward), txtSubLocation.Value)
                    Else
                        obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dtpWeighmentDateBulk.Value, clsDocType.Weighment, IIf(chkBulkMilkProc.IsChecked, clsDocTransactionType.BulkProc, clsDocTransactionType.MccProc), lblLocationCodeBulk.Text)
                    End If
                End If
                If clsCommon.myLen(obj.Weighment_No) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Error In Weighment No Genertion")
                    'Exit Function
                End If
            Else
                obj.Weighment_No = clsCommon.myCstr(fndDocNO.Value)
            End If
            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            fndDocNO.Value = obj.Weighment_No
            'If dtpTareWeight.Enabled = True Then
            If AllowBulkProcTransDateSameasGateEntryDate = 1 Then
                obj.Tare_Weight_date = dtpWeighmentDateBulk.Value
            Else
                obj.Tare_Weight_date = dtpTareWeight.Value
            End If

            'End If
            If chkBulkMilkProc.IsChecked Then
                obj.Gross_Weight_Header = clsCommon.myCdbl(txtGrossWt.Value)
                obj.Weighment_Date = dtpWeighmentDateBulk.Value
                obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNoBulk.Value)
                obj.Doc_Type = "BulkProc"
                'obj.Date_And_Time = clsCommon.GetPrintDate(clsCommon.myCDate(lblGateEntryDateAndTimeValueBulk.Text), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Date_And_Time = txtGateEntryDate.Value
                obj.Challan_No = clsCommon.myCstr(lblChallanNoBulk.Text)
                'obj.Challan_Date = clsCommon.GetPrintDate(clsCommon.myCDate(lblChallanDateBulk.Text), "dd/MMM/yyyy")
                obj.Challan_Date = txtChallanDate.Value

                obj.Tanker_No = clsCommon.myCstr(lblTankerNoBulk.Text)
                obj.location_Code = clsCommon.myCstr(lblLocationCodeBulk.Text)
                obj.Location_Desc = clsCommon.myCstr(lblLocationNameBulk.Text)
                obj.Vendor_Code = clsCommon.myCstr(lblVendorCodeBulk.Text)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorNameBulk.Text)
                obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)
                obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
                obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                obj.Gross_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value)
                obj.Tare_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value)
                obj.Net_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value)
                obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
                obj.Weighment_Slip_No = clsCommon.myCstr(txtWeighmentSlipNo.Text)
                obj.Tanker_Return = IIf(chkTankerReturn.Checked, 1, 0)
                obj.Vendor_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colVendorWeight).Value)
            ElseIf chkMccProc.IsChecked Then
                obj.Gross_Weight_Header = clsCommon.myCdbl(txtGrossWt.Value)
                obj.Weighment_Date = dtpWeighmentDateBulk.Value
                obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNoBulk.Value)
                obj.Doc_Type = "MccProc"
                'obj.Date_And_Time = clsCommon.GetPrintDate(clsCommon.myCDate(lblGateEntryDateAndTimeValueBulk.Text), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Date_And_Time = txtGateEntryDate.Value
                obj.Challan_No = clsCommon.myCstr(lblChallanNoBulk.Text)
                'obj.Challan_Date = clsCommon.GetPrintDate(clsCommon.myCDate(lblChallanDateBulk.Text), "dd/MMM/yyyy")
                obj.Challan_Date = txtChallanDate.Value
                obj.Tanker_No = clsCommon.myCstr(lblTankerNoBulk.Text)
                obj.Dispatched_From_Mcc = clsCommon.myCstr(lblVendorCodeBulk.Text)
                obj.location_Code = clsCommon.myCstr(lblLocationCodeBulk.Text)
                obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                obj.Location_Desc = clsCommon.myCstr(lblLocationNameBulk.Text)
                obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)
                obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
                obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                obj.Gross_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value)
                obj.Tare_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value)
                obj.Net_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value)
                obj.Vendor_Weight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colVendorWeight).Value)
                obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
                obj.Weighment_Slip_No = clsCommon.myCstr(txtWeighmentSlipNo.Text)
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            ''richa agarwal remove settings as per Ranjana Mam 
            'If FinalChamberwise = 1 Then

            obj.Arr = New List(Of clsWeighmentChemberNoDetails)
            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                Dim objTr As New clsWeighmentChemberNoDetails()
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.DIP_Status = clsCommon.myCstr(grow.Cells(colDIPStatus).Value)
                objTr.Sample_Lifted = clsCommon.myCstr(grow.Cells(colSampleLifted).Value)
                Dim dblGrosswt As Double = 0
                Dim intWtSeq As Integer = 0
                If clsCommon.myLen(fndDocNO.Value) > 0 Then
                    dblGrosswt = clsDBFuncationality.getSingleValue("select Gross_Weight from tspl_weighment_chember_details where Chamber_Desc='" & objTr.Chamber_Desc & "' and Weighment_No='" & fndDocNO.Value & "'", trans)
                    intWtSeq = clsDBFuncationality.getSingleValue("select Weighment_Sequence from tspl_weighment_chember_details where Chamber_Desc='" & objTr.Chamber_Desc & "' and Weighment_No='" & fndDocNO.Value & "'", trans)

                End If
                If dblGrosswt > 0 AndAlso clsCommon.myCdbl(grow.Cells(colGrossWeight).Value) = 0 Then
                    objTr.Gross_Weight = dblGrosswt
                Else
                    objTr.Gross_Weight = clsCommon.myCdbl(grow.Cells(colGrossWeight).Value)
                End If
                If intWtSeq > 0 AndAlso clsCommon.myCdbl(grow.Cells(colWeighmentSeqNo).Value) = 0 Then
                    objTr.Weighment_Sequence = intWtSeq
                Else
                    objTr.Weighment_Sequence = clsCommon.myCdbl(grow.Cells(colWeighmentSeqNo).Value)
                End If
                objTr.Tare_Weight = clsCommon.myCdbl(grow.Cells(colTareWeight).Value)
                objTr.Net_Weight = clsCommon.myCdbl(grow.Cells(colNetWeight).Value)

                objTr.Vendor_Weight = clsCommon.myCdbl(grow.Cells(colVendorWeight).Value)
                objTr.Sublocation_Code = clsCommon.myCstr(grow.Cells(colSubLoc).Value)
                If AllowCanInformationintoGridForTankerDispatch = True And chkMccProc.IsChecked = True Then
                    If clsCommon.myCBool(grow.Cells(colIsCanType).Value) = True Then
                        objTr.isCanType = 1
                    Else
                        objTr.isCanType = 0
                    End If
                Else
                    objTr.isCanType = 0
                End If
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            'End If

            If clsWeighment.saveData(obj, trans) Then
                trans.Commit()
                If Not isPostbtnClick Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                Return True
                'btnSave.Text = "Update"
                'fndDocNO.MyReadOnly = True
                'loadData(obj.Weighment_No, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)
                'Exit Function
            End If
            'clsCommon.MyMessageBoxShow("Data Not Saved ")
            'btnSave.Text = "Save"
            'btnDelete.Enabled = False
            'btnPrint.Enabled = False
            'btnPost.Enabled = False
            'fndDocNO.MyReadOnly = False
            'trans.Rollback()
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
        Return True
    End Function
    Sub deleteData()
        Try
            Dim arr As List(Of String) = New List(Of String)
            If clsCommon.myLen(fndDocNO.Value) > 0 Then
                If deleteConfirm() Then
                    arr.Add(fndDocNO.Value)
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.frmWeighment, Nothing) Then
                        If clsWeighment.deleteData(fndDocNO.Value) Then
                            reset()
                            myMessages.delete()
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Could not deleted")
                        End If
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select a weighment no to delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub
    Sub postData()
        Try
            Dim strDocType As String = String.Empty
            If chkBulkMilkProc.IsChecked Then
                strDocType = "BulkProc"
            ElseIf chkMccProc.IsChecked Then
                strDocType = "MccProc"
            End If
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If allowToSave(True) Then
                    If (TankerFromMaster = 0 OrElse chkMccProc.IsChecked) Then
                        If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colNetWeight).Value) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Net Qty can not be 0")
                            Exit Sub
                        End If
                    End If
                    Dim trWeight As Double = 0
                    Dim GrsWeight As Double = 0
                    Dim netWeight As Double = 0
                    trWeight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value)
                    GrsWeight = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value)
                    If trWeight < 0 Then
                        Throw New Exception("Tare Weight can not be negative")
                    End If
                    If isNetWeight = False Then
                        If trWeight = 0 Then
                            Throw New Exception("Tare Weight can not be Zero. Please Fill Tare Weight")
                        End If
                    End If
                    If trWeight > GrsWeight Then
                        Throw New Exception("value of Tare Weight can not be more than Gross Weight")
                    End If
                    For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                        If clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colGrossWeight).Value) = clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colNetWeight).Value) Then
                            Throw New Exception("Tare Weight can not be Zero. Please Fill Tare Weight at row " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    Next

                Else
                    Exit Sub
                End If

                If SaveData(True) Then
                    ' SaveData(True)
                    'If isNetWeight = True Then
                    '    Dim frm As New FrmUnloading
                    '    frm.SetUserMgmt(clsUserMgtCode.frmUnloading)
                    '    frm.Show()
                    '    frm.LoadGateEntryData(fndGateEntryNoBulk.Value)
                    '    For ii As Integer = 0 To frm.gvItem.Rows.Count - 1
                    '        frm.gvItem.Rows(ii).Cells("colISelect").Value = True
                    '        frm.gvItem.Rows(ii).Cells("colSubLoc").Value = clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colSubLoc).Value)
                    '    Next
                    '    frm.btnSave.Text = "Save"
                    '    frm.AutoCreateUnloading = True
                    '    If frm.SaveData(False) = True Then 'frm.btnSave.PerformClick()
                    '        Dim StrUnloadingNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Unloading_No from TSPL_MILK_UNLOADING where Weighment_No='" & fndDocNO.Value & "'"))
                    '        If clsCommon.myLen(StrUnloadingNo) > 0 Then
                    '            If clsUnloading.postData(StrUnloadingNo, clsUserMgtCode.frmUnloading) Then
                    '                frm.Close()
                    '            End If
                    '        End If
                    '    End If
                    'End If
                    If (clsWeighment.postData(fndDocNO.Value, strDocType, Me.Form_ID, Nothing)) Then
                        If TankerFromMaster = 1 And chkBulkMilkProc.IsChecked = True AndAlso AllowRandomOnlyOneSecondaryQC = 0 Then
                            Dim intQC As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SECONDARY_SETTING_QC_HEAD where gate_entry_no='" & fndGateEntryNoBulk.Value & "' and posted=1"))
                            Dim intSRN As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Bulk_MILK_SRN where gate_entry_no='" & fndGateEntryNoBulk.Value & "' "))
                            If intQC = 1 And intSRN = 0 Then
                                Dim frm As New FrmBulkMilkSRN
                                frm.SetUserMgmt(clsUserMgtCode.frmBulkMilkSRN)
                                frm.Show()
                                frm.fndWeighmentNo.Value = fndDocNO.Value
                                frm.loadWeighmentData(fndDocNO.Value)
                                frm.SaveData(False, True)
                                Dim SRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 SRN_NO from TSPL_Bulk_MILK_SRN where Weighment_No='" & fndDocNO.Value & "'"))
                                If clsCommon.myLen(SRNNo) > 0 Then
                                    clsBulkMilkSRN.postData(SRNNo, "M-SRN-B")
                                End If
                                frm.Close()
                            End If
                        ElseIf MCCChamberwise = 1 And chkMccProc.IsChecked = True AndAlso AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                            Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select isintermittent from TSPL_MCC_Dispatch_Challan where Chalan_NO ='" & lblChallanNoBulk.Text & "' ", Nothing))
                            If rValue = 0 OrElse (rValue = 1 AndAlso settTankerDispatchIntermittentSingleGateIn = True) Then
                                Dim frm As New FrmMilkTransferIn
                                frm.SetUserMgmt(clsUserMgtCode.frmMilkTransferIn)
                                frm.Show()
                                frm.fndDispatchChallanNo.Value = lblChallanNoBulk.Text
                                frm.txtMccPlantCode.Value = lblLocationCodeBulk.Text
                                ''richa agarwal 9 Jan,2019 milk transfer in date should be same as weighment tare date as per ranjana mam ERO/09/01/19-000460
                                frm.dtpRcptDateAndTime.Value = dtpTareWeight.Value
                                ''------------------
                                frm.loadDispatchData(lblChallanNoBulk.Text)
                                frm.SaveData(False, True)
                                Dim MilkTrasferInNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Receipt_Challan_No from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No='" & lblChallanNoBulk.Text & "'"))
                                If clsCommon.myLen(MilkTrasferInNo) > 0 Then
                                    If clsMilkTransferIn.postData(MilkTrasferInNo) Then
                                        frm.Close()
                                        'Else
                                        '    qry = " update tspl_weighment_detail set isPosted='0' where Weighment_No='" & fndDocNO.Value & "'"
                                        '    clsDBFuncationality.ExecuteNonQuery(qry)

                                        '    qry = " delete from TSPL_Gate_Out where Gate_Entry_No='" & fndDocNO.Value & "'"
                                        '    clsDBFuncationality.ExecuteNonQuery(qry)
                                    End If
                                    'Else
                                    '    qry = " update tspl_weighment_detail set isPosted='0' where Weighment_No='" & fndDocNO.Value & "'"
                                    '    clsDBFuncationality.ExecuteNonQuery(qry)

                                    '    qry = " delete from TSPL_Gate_Out where Gate_Entry_No='" & fndDocNO.Value & "'"
                                    '    clsDBFuncationality.ExecuteNonQuery(qry)
                                End If
                            End If
                        End If
                        msg = "Successfully Posted"
                    Else
                        qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim level As String = dt.Rows(0)("LEVEL").ToString()
                            Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                            If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                                msg = "Level 1 Approval done. "
                                If NoOflevel = 1 Then
                                    msg += "Successfully Posted. "
                                Else
                                    msg += "Level 2 Approval Required."
                                End If
                            ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                                msg = "Level 2 Approval done. "
                                If NoOflevel = 2 Then
                                    msg += "Successfully Posted "
                                Else
                                    msg += "Level 3 Approval Required."
                                End If
                            Else
                                msg = "Level 3 Approval done. Successfully Posted. "
                            End If
                        End If
                    End If
                    common.clsCommon.MyMessageBoxShow(gvItemBulk, msg)
                    loadData(fndDocNO.Value, strDocType, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Sub sendForQC()
    '    If clsCommon.myLen(fndDocNO.Value) > 0 Then
    '        Dim strqry As String = "Update tspl_weighment_detail set status=1, sent_to_qc_by='" & objCommonVar.CurrentUserCode & "',sent_to_qc_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' where Weighment_No='" & fndDocNO.Value & "'"
    '        clsDBFuncationality.ExecuteNonQuery(strqry)
    '        clsCommon.MyMessageBoxShow("SuccessFully Sent For QC")
    '        loadData(fndDocNO.Value, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)
    '    Else
    '        clsCommon.MyMessageBoxShow("Please Select a Weighment No")
    '    End If
    'End Sub
    Sub printData()
        clsCommon.MyMessageBoxShow(Me, "No Print Format Found", Me.Text)
    End Sub
    Sub loadData(ByVal strWeighmentNo As String, ByVal docType As String, ByVal navType As NavigatorType)
        Try
            blnLoad = True
            obj = New clsWeighment()
            If clsCommon.myLen(docType) > 0 Then
                obj = clsWeighment.getData(strWeighmentNo, docType, navType, False)
            Else
                obj = clsWeighment.getData(strWeighmentNo, navType, False)
            End If

            If obj IsNot Nothing Then
                chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
                txtSubLocation.Value = obj.Joblocation_Code
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                End If

                If clsCommon.CompairString(obj.Doc_Type, "BulkProc") = CompairStringResult.Equal Then
                    chkBulkMilkProc.IsChecked = True
                    fndDocNO.Value = obj.Weighment_No
                    txtGrossWt.Value = obj.Gross_Weight_Header
                    fndGateEntryNoBulk.Value = obj.Gate_Entry_No
                    dtpWeighmentDateBulk.Value = obj.Weighment_Date
                    'lblGateEntryDateAndTimeValueBulk.Text = obj.Date_And_Time
                    txtGateEntryDate.Value = obj.Date_And_Time
                    lblLocationCodeBulk.Text = obj.location_Code
                    lblLocationNameBulk.Text = obj.Location_Desc
                    lblVendorCodeBulk.Text = obj.Vendor_Code
                    lblVendorNameBulk.Text = obj.Vendor_Desc
                    lblChallanNoBulk.Text = obj.Challan_No
                    'lblChallanDateBulk.Text = obj.Challan_Date
                    txtChallanDate.Value = obj.Challan_Date
                    lblTankerNoBulk.Text = obj.Tanker_No
                    fndTankerNo.Value = obj.Tanker_No
                    fndRefrencesNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
                    txtDipValue.Text = obj.Dip_Value
                    txtWeighmentSlipNo.Text = obj.Weighment_Slip_No
                    chkTankerReturn.Checked = IIf(obj.Tanker_Return = 1, True, False)
                    If obj.Tare_Weight_date IsNot Nothing Then
                        dtpTareWeight.Value = obj.Tare_Weight_date
                    End If
                    If clsCommon.myCdbl(obj.Dip_Value) = 0 Then
                        txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_quality_check where  isposted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'"))
                    End If
                    If obj.status = 0 Then
                        lblStatusBulk.Text = "Not Done"
                        gvItemBulk.Columns(colTareWeight).ReadOnly = True
                        isTareWeightReadOnly = True
                    ElseIf obj.status = 1 Then
                        lblStatusBulk.Text = "Done"
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                        isTareWeightReadOnly = False
                    End If
                    btnPrint.Enabled = True
                    loadBlankGvItemBulk()
                    If FinalChamberwise = 0 Then
                        gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                        gvItemBulk.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                        gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                        gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                        gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                        gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                        gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                        gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
                        gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
                        gvItemBulk.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.Gross_Weight)
                        gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(obj.Tare_Weight)
                        gvItemBulk.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(obj.Net_Weight)
                        gvItemBulk.Rows(0).Cells(colVendorWeight).Value = clsCommon.myFormat(obj.Vendor_Weight)
                    End If
                    ''richa agarwal remove settings as per Ranjana Mam 
                    'If FinalChamberwise = 1 Then
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        gvItemBulk.Rows.Clear()
                        For Each objTr As clsWeighmentChemberNoDetails In obj.Arr
                            gvItemBulk.Rows.AddNew()
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPStatus).Value = objTr.DIP_Status
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSampleLifted).Value = objTr.Sample_Lifted
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myFormat(objTr.Gross_Weight)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myFormat(objTr.Tare_Weight)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objTr.Net_Weight)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colWeighmentSeqNo).Value = objTr.Weighment_Sequence
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colVendorWeight).Value = clsCommon.myFormat(objTr.Vendor_Weight)
                        Next
                    End If
                    'End If
                    If obj.isPosted = 1 Then
                        lblPending.Status = ERPTransactionStatus.Approved
                        gvItemBulk.Columns(colTareWeight).ReadOnly = True
                        isTareWeightReadOnly = True
                    Else
                        lblPending.Status = ERPTransactionStatus.Pending
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                        isTareWeightReadOnly = False
                    End If
                ElseIf clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                    chkMccProc.IsChecked = True
                    fndDocNO.Value = obj.Weighment_No
                    txtGrossWt.Value = obj.Gross_Weight_Header
                    fndGateEntryNoBulk.Value = obj.Gate_Entry_No
                    dtpWeighmentDateBulk.Value = obj.Weighment_Date
                    'lblGateEntryDateAndTimeValueBulk.Text = obj.Date_And_Time
                    txtGateEntryDate.Value = obj.Date_And_Time
                    lblLocationCodeBulk.Text = obj.location_Code
                    lblLocationNameBulk.Text = clsLocation.GetName(obj.location_Code, Nothing)
                    lblVendorCodeBulk.Text = clsCommon.myCstr(obj.Dispatched_From_Mcc)
                    lblVendorNameBulk.Text = clsCommon.myCstr(clsLocation.GetName(obj.Dispatched_From_Mcc, Nothing))
                    lblChallanNoBulk.Text = obj.Challan_No
                    'lblChallanDateBulk.Text = obj.Challan_Date
                    txtChallanDate.Value = obj.Challan_Date
                    lblTankerNoBulk.Text = obj.Tanker_No
                    fndTankerNo.Value = obj.Tanker_No
                    fndRefrencesNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
                    txtDipValue.Text = obj.Dip_Value
                    txtWeighmentSlipNo.Text = obj.Weighment_Slip_No
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsNetWeight from tspl_gate_entry_details where Gate_Entry_No='" & fndGateEntryNoBulk.Value & "'")) = 1 Then
                        isNetWeight = True
                    Else
                        isNetWeight = False
                    End If
                    If obj.Tare_Weight_date IsNot Nothing Then
                        dtpTareWeight.Value = obj.Tare_Weight_date
                    End If
                    If clsCommon.myCdbl(obj.Dip_Value) = 0 Then
                        obj.Dip_Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_quality_check where isposted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'"))
                    End If
                    If obj.status = 0 Then
                        lblStatusBulk.Text = "Not Done"
                        'If isNetWeight = True Then
                        '    For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                        '        gvItemBulk.Rows(ii).Cells(colGrossWeight).ReadOnly = False
                        '        gvItemBulk.Rows(ii).Cells(colTareWeight).ReadOnly = False
                        '    Next
                        '    gvItemBulk.Columns(colGrossWeight).ReadOnly = False
                        '    gvItemBulk.Columns(colTareWeight).ReadOnly = False
                        '    isGrossWeightReadOnly = False
                        '    isTareWeightReadOnly = False
                        '    gvItemBulk.Columns(colSubLoc).ReadOnly = False
                        '    gvItemBulk.Columns(colSubLoc).IsVisible = True
                        'Else
                        '    gvItemBulk.Columns(colSubLoc).ReadOnly = True
                        '    gvItemBulk.Columns(colSubLoc).IsVisible = False
                        'End If
                    ElseIf obj.status = 1 Then
                        lblStatusBulk.Text = "Done"
                    End If
                    btnPrint.Enabled = True
                    loadBlankGvItemBulk()
                    If MCCChamberwise = 0 Then
                        gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                        gvItemBulk.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                        gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                        gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                        gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                        gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                        gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                        gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
                        gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
                        gvItemBulk.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.Gross_Weight)
                        gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(obj.Tare_Weight)
                        gvItemBulk.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(obj.Net_Weight)
                        gvItemBulk.Rows(0).Cells(colVendorWeight).Value = clsCommon.myFormat(obj.Vendor_Weight)
                    End If
                    If MCCChamberwise = 1 Then
                        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                            gvItemBulk.Rows.Clear()
                            For Each objTr As clsWeighmentChemberNoDetails In obj.Arr
                                gvItemBulk.Rows.AddNew()
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPStatus).Value = objTr.DIP_Status
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSampleLifted).Value = objTr.Sample_Lifted
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myFormat(objTr.Gross_Weight)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myFormat(objTr.Tare_Weight)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objTr.Net_Weight)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colWeighmentSeqNo).Value = objTr.Weighment_Sequence
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colVendorWeight).Value = clsCommon.myFormat(objTr.Vendor_Weight)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSubLoc).Value = clsCommon.myCstr(objTr.Sublocation_Code)

                                If chkMccProc.IsChecked AndAlso AllowCanInformationintoGridForTankerDispatch = True Then
                                    If clsCommon.CompairString(objTr.isCanType, "1") = CompairStringResult.Equal Then
                                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colIsCanType).Value = True

                                    End If
                                End If

                            Next
                        End If
                    End If
                    If obj.isPosted = 1 Then
                        lblPending.Status = ERPTransactionStatus.Approved
                        gvItemBulk.Columns(colTareWeight).ReadOnly = True
                        isTareWeightReadOnly = True
                        If isNetWeight = True Then
                            gvItemBulk.Columns(colSubLoc).IsVisible = True
                        End If
                    Else
                        lblPending.Status = ERPTransactionStatus.Pending
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                        isTareWeightReadOnly = False
                        If isNetWeight = True Then
                            For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                                gvItemBulk.Rows(ii).Cells(colGrossWeight).ReadOnly = False
                                gvItemBulk.Rows(ii).Cells(colTareWeight).ReadOnly = False
                            Next
                            isNetWeight = True
                            gvItemBulk.Columns(colGrossWeight).ReadOnly = False
                            gvItemBulk.Columns(colTareWeight).ReadOnly = False
                            isGrossWeightReadOnly = False
                            isTareWeightReadOnly = False
                            gvItemBulk.Columns(colSubLoc).ReadOnly = False
                            gvItemBulk.Columns(colSubLoc).IsVisible = True
                        Else
                            gvItemBulk.Columns(colSubLoc).ReadOnly = True
                            gvItemBulk.Columns(colSubLoc).IsVisible = False
                        End If
                    End If
                End If

                If obj.isPosted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnPrint.Enabled = True
                Else
                    btnSave.Enabled = True
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    If FinalChamberwise = 0 Then
                        If (clsCleaning.isCleaningDone(obj.Gate_Entry_No, Nothing) And chkMccProc.IsChecked And (Not clsERPFuncationality.isLocationMcc(obj.location_Code))) OrElse (clsUnloading.isUnloadingDone(obj.Gate_Entry_No, Nothing) And chkMccProc.IsChecked And (clsERPFuncationality.isLocationMcc(obj.location_Code))) OrElse (clsUnloading.isUnloadingDone(obj.Gate_Entry_No, Nothing) And chkBulkMilkProc.IsChecked) Then
                            btnPost.Enabled = True
                            gvItemBulk.Columns(colTareWeight).ReadOnly = False
                            isTareWeightReadOnly = False
                            gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = True
                            isGrossWeightReadOnly = True
                            dtpWeighmentDateBulk.Enabled = False
                        Else
                            btnPost.Enabled = False
                            gvItemBulk.Columns(colTareWeight).ReadOnly = True
                            isTareWeightReadOnly = True
                            gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = False
                            isGrossWeightReadOnly = False
                            dtpWeighmentDateBulk.Enabled = True
                        End If
                    Else
                        If (clsCleaning.isCleaningDone(obj.Gate_Entry_No, Nothing) And chkMccProc.IsChecked And (Not clsERPFuncationality.isLocationMcc(obj.location_Code))) OrElse (clsUnloading.isUnloadingDone(obj.Gate_Entry_No, Nothing) And chkMccProc.IsChecked And (clsERPFuncationality.isLocationMcc(obj.location_Code))) OrElse (clsUnloading.isUnloadingDone(obj.Gate_Entry_No, Nothing) And chkBulkMilkProc.IsChecked) Then
                            btnPost.Enabled = True
                            gvItemBulk.Columns(colTareWeight).ReadOnly = False
                            isTareWeightReadOnly = False
                            gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = True
                            isGrossWeightReadOnly = True
                            dtpWeighmentDateBulk.Enabled = False
                        Else
                            btnPost.Enabled = False
                            isTareWeightReadOnly = True
                            If IsWeighmentUnloadingSequencewise = 1 Then
                                gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = True
                            Else
                                gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = False
                            End If

                            isGrossWeightReadOnly = False
                            dtpWeighmentDateBulk.Enabled = True
                        End If
                    End If

                    btnPrint.Enabled = True
                End If

                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
                If obj.isPosted = 0 Then
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select isintermittent from TSPL_MCC_Dispatch_Challan where Chalan_NO ='" & obj.Challan_No & "' ")) = 1 Then
                        Dim isReachedAtFinal As Integer = 0
                        isReachedAtFinal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(reachedAtFinal,0) from TSPL_MCC_Dispatch_Challan where  Chalan_NO ='" & obj.Challan_No & "'  "))
                        'gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')")))
                        If isReachedAtFinal = 1 And MCCChamberwise = 0 Then
                            If clsUnloading.isUnloadingDone(obj.Gate_Entry_No, Nothing) = False Then
                                'gvItemBulk.Columns(colTareWeight).ReadOnly = False
                                'gvItemBulk.Rows(0).Cells(colTareWeight).Value = IIf(clsCommon.myCdbl(obj.Tare_Weight) = 0, clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')"))), clsCommon.myFormat(obj.Tare_Weight))
                                gvItemBulk.Columns(colTareWeight).ReadOnly = True
                                isTareWeightReadOnly = True
                            Else
                                gvItemBulk.Columns(colTareWeight).ReadOnly = False
                                isTareWeightReadOnly = False
                                If (MCCChamberwise = 0) Then
                                    gvItemBulk.Rows(0).Cells(colTareWeight).Value = IIf(clsCommon.myCdbl(obj.Tare_Weight) = 0, clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')"))), clsCommon.myFormat(obj.Tare_Weight)) 'clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')")))
                                Else
                                    gvItemBulk.Columns(colTareWeight).ReadOnly = False
                                    isTareWeightReadOnly = False
                                End If
                            End If
                        Else
                            gvItemBulk.Columns(colTareWeight).ReadOnly = True
                            isTareWeightReadOnly = True
                            If (MCCChamberwise = 0) Then
                                gvItemBulk.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Tare_Weight  from TSPL_MCC_Dispatch_Challan where Chalan_NO=(select Level1ChallanNo  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Challan_No & "')")))
                            Else
                                gvItemBulk.Columns(colTareWeight).ReadOnly = False
                                isTareWeightReadOnly = False
                            End If
                        End If
                        If (MCCChamberwise = 0) Then
                            gvItemBulk.Rows(0).Cells(colGrossWeight).ReadOnly = False
                        End If
                        isGrossWeightReadOnly = False
                        If (MCCChamberwise = 0) Then
                            gvItemBulk.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colGrossWeight).Value) - clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colTareWeight).Value))
                        End If
                        btnPost.Enabled = True
                    End If
                    If isNetWeight = True Then
                        btnPost.Enabled = True
                    End If
                    If EnterWeightManuallyOnWeighmentInGrid = True Then
                        For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                            gvItemBulk.Rows(ii).Cells(colGrossWeight).ReadOnly = False
                            gvItemBulk.Rows(ii).Cells(colTareWeight).ReadOnly = False
                        Next
                        gvItemBulk.Columns(colGrossWeight).ReadOnly = False
                        gvItemBulk.Columns(colTareWeight).ReadOnly = False
                    End If
                End If

            End If
                blnLoad = False
        Catch ex As Exception
            blnLoad = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub loadBlankGvItemMcc()

        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing

        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        If AllowCanInformationintoGridForTankerDispatch = True AndAlso chkMccProc.IsChecked = True Then
            Dim repoisCanType As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoisCanType.HeaderText = "Is Can"
            repoisCanType.Name = colIsCanType
            repoisCanType.ReadOnly = True
            repoisCanType.IsVisible = True
            repoisCanType.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gvItemBulk.MasterTemplate.Columns.Add(repoisCanType)
        End If


        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).ReadOnly = True

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItemBulk.Columns(colChamberDesc).Width = 150
        gvItemBulk.Columns(colChamberDesc).ReadOnly = True
        gvItemBulk.Columns(colChamberDesc).IsVisible = False

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = True

        gvItemBulk.Columns.Add(colQty, "Qty")
        gvItemBulk.Columns(colQty).Width = 120
        gvItemBulk.Columns(colQty).IsVisible = False
        gvItemBulk.Columns(colQty).ReadOnly = True

        gvItemBulk.Columns.Add(colHSN, "HSN Code")
        gvItemBulk.Columns(colHSN).Width = 120
        gvItemBulk.Columns(colHSN).ReadOnly = False

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 75
        gvItemBulk.Columns(colFat).IsVisible = False
        gvItemBulk.Columns(colFat).ReadOnly = True

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 75
        gvItemBulk.Columns(colSNF).IsVisible = False
        gvItemBulk.Columns(colSNF).ReadOnly = True

        gvItemBulk.Columns.Add(colFatKG, "FAT KG")
        gvItemBulk.Columns(colFatKG).Width = 75
        gvItemBulk.Columns(colFatKG).IsVisible = False
        gvItemBulk.Columns(colFatKG).ReadOnly = True

        gvItemBulk.Columns.Add(colSNFKG, "SNF KG")
        gvItemBulk.Columns(colSNFKG).Width = 75
        gvItemBulk.Columns(colSNFKG).IsVisible = False
        gvItemBulk.Columns(colSNFKG).ReadOnly = True

        gvItemBulk.Columns.Add(colGrossWeight, "Gross Weight")
        gvItemBulk.Columns(colGrossWeight).Width = 75
        gvItemBulk.Columns(colGrossWeight).ReadOnly = False
        isGrossWeightReadOnly = False
        'gvItemBulk.Columns.Add(colDipValue, "DIP Value")
        'gvItemBulk.Columns(colDipValue).Width = 75
        'gvItemBulk.Columns(colDipValue).ReadOnly = False



        gvItemBulk.Columns.Add(colTareWeight, "Tare Weight")
        gvItemBulk.Columns(colTareWeight).Width = 75
        gvItemBulk.Columns(colTareWeight).ReadOnly = True
        isTareWeightReadOnly = True

        gvItemBulk.Columns.Add(colNetWeight, "Net Weight")
        gvItemBulk.Columns(colNetWeight).Width = 75
        gvItemBulk.Columns(colNetWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colVendorWeight, "Vendor Weight")
        gvItemBulk.Columns(colVendorWeight).Width = 75
        If NetWeightWithVendorWeight = 0 Then gvItemBulk.Columns(colVendorWeight).IsVisible = False
        gvItemBulk.Columns(colVendorWeight).ReadOnly = True

        gvItemBulk.Columns.Add(colSampleLifted, "Sample Lifted")
        gvItemBulk.Columns(colSampleLifted).Width = 75
        gvItemBulk.Columns(colSampleLifted).ReadOnly = True
        gvItemBulk.Columns(colSampleLifted).IsVisible = False

        gvItemBulk.Columns.Add(colDIPStatus, "DIP Status")
        gvItemBulk.Columns(colDIPStatus).Width = 75
        gvItemBulk.Columns(colDIPStatus).ReadOnly = True
        gvItemBulk.Columns(colDIPStatus).IsVisible = False

        gvItemBulk.Columns.Add(colWeighmentSeqNo, "Sequence No")
        gvItemBulk.Columns(colWeighmentSeqNo).Width = 120
        gvItemBulk.Columns(colWeighmentSeqNo).ReadOnly = True
        gvItemBulk.Columns(colWeighmentSeqNo).IsVisible = False

        If (MCCChamberwise = 1) Then
            gvItemBulk.Columns(colFat).IsVisible = True
            gvItemBulk.Columns(colSNF).IsVisible = True
            gvItemBulk.Columns(colChamberDesc).IsVisible = True
            gvItemBulk.Columns(colSampleLifted).IsVisible = True
            gvItemBulk.Columns(colDIPStatus).IsVisible = True
            gvItemBulk.Columns(colWeighmentSeqNo).IsVisible = True
            If IsWeighmentUnloadingSequencewise = 0 Then
                gvItemBulk.Columns(colGrossWeight).ReadOnly = True
            End If
        End If

        gvItemBulk.Columns.Add(colSubLoc, "Silo Location")
        gvItemBulk.Columns(colSubLoc).Width = 150
        gvItemBulk.Columns(colSubLoc).ReadOnly = False
        gvItemBulk.Columns(colSubLoc).IsVisible = True

        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        gvItemBulk.AllowAddNewRow = False
        'gvItemBulk.AllowColumnReorder = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        'gvItemBulk.AllowColumnChooser = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub

    Private Sub FrmWeighment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Then
            Try
                If FinalChamberwise = 0 Then '(TankerFromMaster = 0 And MCCChamberwise = 0) OrElse (MCCChamberwise = 0 And chkMccProc.IsChecked) OrElse (TankerFromMaster = 0 And chkBulkMilkProc.IsChecked) Then ' (TankerFromMaster = 0 And chkBulkMilkProc.IsChecked = True) OrElse (MCCChamberwise = 0 And chkMccProc.IsChecked = True) Then
                    If isGrossWeightReadOnly = False Then
                        gvItemBulk.CurrentRow = gvItemBulk.Rows(0)
                        gvItemBulk.CurrentColumn = gvItemBulk.Columns(colGrossWeight)
                        gvItemBulk.Rows(0).Cells(colGrossWeight).Value = UcWeighing1.LiveReading
                    ElseIf isTareWeightReadOnly = False Then
                        gvItemBulk.CurrentRow = gvItemBulk.Rows(0)
                        gvItemBulk.CurrentColumn = gvItemBulk.Columns(colTareWeight)
                        gvItemBulk.Rows(0).Cells(colTareWeight).Value = UcWeighing1.LiveReading
                        ''richa 
                        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
                        If DateTime = "1" Then
                            dtpTareWeight.Value = clsCommon.GETSERVERDATE()
                        End If
                    End If
                Else
                    If UcWeighing1.Enabled = True Then
                        Dim Qry As String = ""
                        Dim intCount As Integer = 0
                        If clsCommon.myLen(fndDocNO.Value) > 0 Then
                            If clsCommon.myLen(fndDocNO.Value) > 0 AndAlso clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_UNLOADING where Tanker_No='" & fndTankerNo.Value & "'")) = 0 Then
                                Throw New Exception("Unloading is pending for this tanker " & fndTankerNo.Value & " .Please Create unloading ")
                            End If
                            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                                If clsCommon.myCdbl(grow.Cells(colGrossWeight).Value) = 0 Then
                                    grow.Cells(colGrossWeight).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Gross_Weight from TSPL_WEIGHMENT_CHEMBER_DETAILS where Weighment_No='" & fndDocNO.Value & "' and Line_No='" & grow.Index + 1 & "' "))
                                End If
                                If clsCommon.myCdbl(grow.Cells(colGrossWeight).Value) > 0 Then
                                    intCount = 1
                                End If
                            Next
                        Else
                            intCount = 0
                        End If

                        If intCount = 0 Then
                            txtGrossWt.Value = UcWeighing1.LiveReading
                        Else
                            Dim intRow As Integer = 0
                            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                                gvItemBulk.CurrentRow = gvItemBulk.Rows(intRow)
                                If chkMccProc.IsChecked AndAlso AllowCanInformationintoGridForTankerDispatch = True Then
                                    If clsCommon.myCBool(grow.Cells(colIsCanType).Value) = False Then
                                        If clsCommon.myCdbl(grow.Cells(colGrossWeight).Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colTareWeight).Value) = 0 Then
                                            grow.Cells(colTareWeight).Value = UcWeighing1.LiveReading
                                        End If
                                    End If
                                Else
                                    If clsCommon.myCdbl(grow.Cells(colGrossWeight).Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colTareWeight).Value) = 0 Then
                                        grow.Cells(colTareWeight).Value = UcWeighing1.LiveReading
                                    End If
                                End If

                                intRow += 1
                            Next
                            ''richa 
                            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
                            If DateTime = "1" Then
                                dtpTareWeight.Value = clsCommon.GETSERVERDATE()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F3 Then
            ' done by priti ERO/08/06/18-000337
            If clsCommon.myLen(fndDocNO.Value) > 0 Then ' And lblPending.Status = ERPTransactionStatus.Approved
                Dim intexist As Integer = 0
                If chkBulkMilkProc.IsChecked Then
                    intexist = clsDBFuncationality.getSingleValue("select count(*) from TSPL_Bulk_MILK_SRN where Weighment_No='" & fndDocNO.Value & "' and isPosted=1")
                Else
                    intexist = clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_TRANSFER_IN where weighment_no='" & fndDocNO.Value & "' and isPosted=1")
                End If
                If intexist = 0 Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "WEUpdateAfterPost"
                    frm.strCode = "WEUpdateAfterPost"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        Panel2.Visible = True
                    End If
                Else
                    If chkBulkMilkProc.IsChecked Then
                        clsCommon.MyMessageBoxShow(Me, "Bulk Milk SRN already has been created.First Unpost Bulk Milk SRN.")
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Milk Transfer In already has been created.First Unpost Milk transfer In.")
                    End If
                End If
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                  "Tspl_Weighment_detail " + Environment.NewLine +
                                                  "TSPL_Weighment_Chember_Details (  Only in case of chamber wise setting ON) " + Environment.NewLine +
                                                  "tspl_weighment_detail_history  ( For History) .")
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub



    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub chkBulkMilkProc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBulkMilkProc.ToggleStateChanged
        reset()
    End Sub

    Private Sub chkMccProc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMccProc.ToggleStateChanged
        reset()
    End Sub

    Private Sub fndGateEntryNoBulk__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNoBulk._MYValidating


    End Sub
    Sub loadGateEntryDataBulk(ByVal strGateEntryNo As String)

        '' checking that weighemnt has done against this gate entry no, if yes then show its already shown details
        Dim strWeighmentNo As String = String.Empty
        strWeighmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from TSPL_Weighment_Detail where gate_entry_no='" & strGateEntryNo & "'"))
        If clsCommon.myLen(strWeighmentNo) > 0 Then
            loadData(strWeighmentNo, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
            Exit Sub
        End If

        Dim obj As clsGateEntry = clsGateEntry.getData(strGateEntryNo, "BulkProc", NavigatorType.Current)
        If obj IsNot Nothing Then
            fndGateEntryNoBulk.Value = obj.Gate_Entry_No
            'lblGateEntryDateAndTimeValueBulk.Text = clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MM/yyyy hh:mm:ss tt")
            txtGateEntryDate.Value = obj.Date_And_Time
            lblLocationCodeBulk.Text = obj.location_Code
            lblLocationNameBulk.Text = obj.Location_Desc
            lblVendorCodeBulk.Text = obj.Vendor_Code
            lblVendorNameBulk.Text = obj.Vendor_Desc
            lblTankerNoBulk.Text = obj.Tanker_No
            fndTankerNo.Value = obj.Tanker_No
            fndRefrencesNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
            lblChallanNoBulk.Text = obj.Challan_No
            'lblChallanDateBulk.Text = clsCommon.GetPrintDate(obj.Challan_Date, "dd/MM/yyyy")
            txtChallanDate.Value = obj.Challan_Date
            txtSubLocation.Value = obj.Sublocation_Code
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            If FinalChamberwise = 0 Then ' (TankerFromMaster = 0 And MCCChamberwise = 0) OrElse (TankerFromMaster = 1 And MCCChamberwise = 0 And chkMccProc.IsChecked) OrElse (TankerFromMaster = 0 And MCCChamberwise = 1 And chkBulkMilkProc.IsChecked) Then ' (TankerFromMaster = 0 And chkBulkMilkProc.IsChecked = True) OrElse (MCCChamberwise = 0 And chkMccProc.IsChecked = True) Then
                gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gvItemBulk.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
            Else
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    gvItemBulk.Rows.Clear()
                    For Each objTr As clsGateEntryChemberNoDetails In obj.Arr
                        gvItemBulk.Rows.AddNew()
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPStatus).Value = objTr.DIP_Status
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSampleLifted).Value = objTr.Sample_Lifted
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100
                    Next
                End If
            End If

            If clsDBFuncationality.getSingleValue("select count(*) from tspl_quality_check where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'") > 0 Then
                lblStatusBulk.Text = "Done"
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_quality_check where  gate_entry_no='" & obj.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
            Else
                lblStatusBulk.Text = "Not Done"
            End If

            If EnterWeightManuallyOnWeighmentInGrid = True Then
                For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                    gvItemBulk.Rows(ii).Cells(colGrossWeight).ReadOnly = False
                    gvItemBulk.Rows(ii).Cells(colTareWeight).ReadOnly = False
                Next
                gvItemBulk.Columns(colGrossWeight).ReadOnly = False
                gvItemBulk.Columns(colTareWeight).ReadOnly = False
            End If

            btnSave.Text = "Save"
            btnSave.Enabled = True
            btnPrint.Enabled = False
            btnDelete.Enabled = False
            btnPost.Enabled = False
        End If
    End Sub
    Sub loadGateEntryDataMcc(ByVal strGateEntryNo As String)

        '' checking that weighemnt has done against this gate entry no, if yes then show its already shown details
        Dim strWeighmentNo As String = String.Empty
        strWeighmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weighment_no from TSPL_Weighment_Detail where gate_entry_no='" & strGateEntryNo & "'"))
        If clsCommon.myLen(strWeighmentNo) > 0 Then
            loadData(strWeighmentNo, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
            Exit Sub
        End If


        Dim obj As clsGateEntry = clsGateEntry.getData(strGateEntryNo, "MccProc", NavigatorType.Current, False)
        If obj IsNot Nothing Then
            fndGateEntryNoBulk.Value = obj.Gate_Entry_No
            'lblGateEntryDateAndTimeValueBulk.Text = clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MM/yyyy hh:mm:ss tt")
            txtGateEntryDate.Value = obj.Date_And_Time
            lblLocationCodeBulk.Text = obj.location_Code
            lblLocationNameBulk.Text = obj.Location_Desc
            lblVendorCodeBulk.Text = obj.Dispatched_From_Mcc
            lblVendorNameBulk.Text = clsLocation.GetName(obj.Dispatched_From_Mcc, Nothing)
            fndTankerNo.Value = obj.Tanker_No
            fndRefrencesNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
            lblTankerNoBulk.Text = obj.Tanker_No
            lblChallanNoBulk.Text = obj.Challan_No
            'lblChallanDateBulk.Text = clsCommon.GetPrintDate(obj.Challan_Date, "dd/MM/yyyy")
            txtChallanDate.Value = obj.Challan_Date
            txtSubLocation.Value = obj.Sublocation_Code
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)

            If FinalChamberwise = 0 Then
                gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gvItemBulk.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100
            End If

            If FinalChamberwise = 1 Then
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    gvItemBulk.Rows.Clear()
                    For Each objTr As clsGateEntryChemberNoDetails In obj.Arr
                        gvItemBulk.Rows.AddNew()
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPStatus).Value = objTr.DIP_Status
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSampleLifted).Value = objTr.Sample_Lifted
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100
                        If chkMccProc.IsChecked AndAlso AllowCanInformationintoGridForTankerDispatch = True Then
                            Dim isCanType As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isCanType from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No ='" & clsCommon.myCstr(lblChallanNoBulk.Text) & "' and Item_code='" & clsCommon.myCstr(objTr.Item_Code) & "' and Qty_KG=" & clsCommon.myCdbl(objTr.Chamber_Qty) & " ")) = 0, False, True)
                            If isCanType = True Then
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colIsCanType).Value = True
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colGrossWeight).Value = objTr.Chamber_Qty
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colNetWeight).Value = objTr.Chamber_Qty
                            End If

                        End If
                    Next
                End If
            End If

            If clsDBFuncationality.getSingleValue("select count(*) from tspl_quality_check where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'") > 0 Then
                lblStatusBulk.Text = "Done"
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_quality_check where  gate_entry_no='" & obj.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
            Else
                lblStatusBulk.Text = "Not Done"
            End If
            If obj.IsNetWeight = 1 Then
                isNetWeight = True
                For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                    gvItemBulk.Rows(ii).Cells(colGrossWeight).ReadOnly = False
                    gvItemBulk.Rows(ii).Cells(colTareWeight).ReadOnly = False
                Next
                gvItemBulk.Columns(colGrossWeight).ReadOnly = False
                gvItemBulk.Columns(colTareWeight).ReadOnly = False
                isGrossWeightReadOnly = False
                isTareWeightReadOnly = False
                gvItemBulk.Columns(colSubLoc).ReadOnly = False
                gvItemBulk.Columns(colSubLoc).IsVisible = True
            Else
                gvItemBulk.Columns(colSubLoc).ReadOnly = True
                gvItemBulk.Columns(colSubLoc).IsVisible = False
            End If
            If EnterWeightManuallyOnWeighmentInGrid = True Then
                For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                    gvItemBulk.Rows(ii).Cells(colGrossWeight).ReadOnly = False
                    gvItemBulk.Rows(ii).Cells(colTareWeight).ReadOnly = False
                Next
                gvItemBulk.Columns(colGrossWeight).ReadOnly = False
                gvItemBulk.Columns(colTareWeight).ReadOnly = False
            End If
            btnSave.Text = "Save"
            btnSave.Enabled = True
            btnPrint.Enabled = False
            btnDelete.Enabled = False
            btnPost.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal Then
        '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_quality_check where isposted=1 and weighment_no='" & fndDocNO.Value & "' ")) > 0 Then
        '        clsCommon.MyMessageBoxShow("This document is in use, you can not update it.")
        '    End If
        'End If
        If allowToSave(False) Then
            If SaveData(False) Then
                btnSave.Text = "update"
                fndDocNO.MyReadOnly = True
                loadData(fndDocNO.Value, IIf(chkBulkMilkProc.IsChecked, "bulkproc", "mccproc"), NavigatorType.Current)
            Else
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select isintermittent from TSPL_MCC_Dispatch_Challan where Chalan_NO ='" & lblChallanNoBulk.Text & "' ", Nothing))
        'If MCCChamberwise = 1 AndAlso chkMccProc.IsChecked = True AndAlso rValue = 1 AndAlso settTankerDispatchIntermittentSingleGateIn = True Then
        '    'No QC Check required, QC is made before Weighment entry in Intermittent tanker
        'Else
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_quality_check where isposted=1 and weighment_no='" & fndDocNO.Value & "' ")) > 0 Then
            clsCommon.MyMessageBoxShow(Me, "This document is in use, you can not delete it.", Me.Text)
            Exit Sub
        End If
        If myMessages.deleteConfirm() Then
            deleteData()
        End If
    End Sub

    Private Sub fndDocNO__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNO._MYNavigator
        If chkBothDoc.Checked Then
            loadData(fndDocNO.Value, "", NavType)
        Else
            loadData(fndDocNO.Value, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavType)
        End If

    End Sub

    Private Sub fndDocNO__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNO._MYValidating
        Dim strDocType As String = ""

        If Not chkBothDoc.Checked Then

            If chkBulkMilkProc.IsChecked Then
                strDocType = "BulkProc"
            ElseIf chkMccProc.IsChecked Then
                strDocType = "MccProc"
            Else
                fndDocNO.Value = ""
                clsCommon.MyMessageBoxShow(Me, "Please Select a Weighment Type i.e Bulk Procurement or Mcc Procurement")
                Exit Sub
            End If

        End If

        Dim whrcls As String = "  1=1  "

        If clsCommon.myLen(strDocType) > 0 Then
            whrcls = " doc_type='" & strDocType & "'"
        End If
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = whrcls & " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        If chkPendingTare.Checked Then
            whrcls = whrcls & "  and isPosted=0 and gate_entry_no not in(select gate_entry_no from tspl_gate_out) and tanker_return=0 "
        End If
        fndDocNO.Value = clsWeighment.getFinder(whrcls, fndDocNO.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNO.Value) > 0 Then
            loadData(fndDocNO.Value, strDocType, NavigatorType.Current)
        Else
            reset()
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Private Sub gvItemBulk_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemBulk.CellEndEdit
        'Try
        '    If Not isCellValueChangedOpen Then
        '        isCellValueChangedOpen = True

        '        If e.Column Is gvItemBulk.Columns(colGrossWeight) Then
        '            Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value))
        '            If (clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value) - intPart) > 0 Then
        '                gvItemBulk.CurrentRow.Cells(colGrossWeight).Value = intPart
        '                Throw New Exception("Gross Weight must be integer")
        '            End If
        '            gvItemBulk.CurrentRow.Cells(colGrossWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value, 0))
        '            gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 2)
        '            gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
        '        End If

        '        If e.Column Is gvItemBulk.Columns(colTareWeight) Then
        '            Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value))
        '            If (clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) - intPart) > 0 Then
        '                gvItemBulk.CurrentRow.Cells(colTareWeight).Value = intPart
        '                Throw New Exception("Tare Weight must be integer")
        '            End If
        '            If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value) < clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) Then
        '                gvItemBulk.CurrentRow.Cells(colTareWeight).Value = "0"
        '                gvItemBulk.CurrentRow.Cells(colNetWeight).Value = "0"
        '                Throw New Exception("Tare Weight Can not be more than Gross weight")
        '            End If
        '            If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) < 0 Then
        '                gvItemBulk.CurrentRow.Cells(colTareWeight).Value = "0"
        '                gvItemBulk.CurrentRow.Cells(colNetWeight).Value = "0"
        '                Throw New Exception("Tare Weight Can not be negative or Zero")
        '            End If
        '            gvItemBulk.CurrentRow.Cells(colTareWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 0))
        '            gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 2)
        '            gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
        '        End If
        '    End If
        '    isCellValueChangedOpen = False
        'Catch ex As Exception
        '    isCellValueChangedOpen = False
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            'If FinalChamberwise = 1 AndAlso isCreateBulkProcPriceChartItemWise = 1 Then 
            If e.Column Is gvItemBulk.Columns(colSubLoc) Then
                    If clsCommon.myLen(lblLocationCodeBulk.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, " Please select a tanker first ", Me.Text)
                    Exit Sub
                    End If
                    Dim whrCls As String = String.Empty
                    If Not clsMccMaster.isCurrentUserHO() Then
                        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                            whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                        End If
                    End If
                    ' done by priti BHA/27/07/18-000202 to show silo Location in grid mapped with item in location item mapping screen
                    Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, Nothing))
                    Dim strItemLoc As String = ""
                    If ShowLocationItemLocationwise = 1 Then
                        strItemLoc = " and Location_Code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value) & "')"
                    End If
                'If AllowJobWorkonGateEntryBulkProc = 1 And chkJobWork.Checked Then
                '    gvItemBulk.CurrentRow.Cells(colSubLoc).Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & lblLocationCodeBulk.Text & "'  and Location_Type='Virtual' and UseInJobWork=1 " & whrCls & strItemLoc, lblLocationCodeBulk.Text, True)
                'Else
                gvItemBulk.CurrentRow.Cells(colSubLoc).Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & lblLocationCodeBulk.Text & "' " & whrCls & strItemLoc, lblLocationCodeBulk.Text, True)
                'End If
                'If clsCommon.myLen(fndSubLocation.Value) > 0 Then
                '    lblSubLocationName.Text = clsLocation.GetName(fndSubLocation.Value, Nothing)
                'Else
                '    lblLocationName.Text = ""
                'End If
            End If
                'End If
                isCellValueChangedOpen = False
        End If
    End Sub



    Private Sub SplitContainer3_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer3.Panel1.Paint

    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItemBulk.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItemBulk.Columns.Count - 1 Step ii + 1
                        gvItemBulk.Columns(ii).IsVisible = False
                        gvItemBulk.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItemBulk.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItemBulk.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItemBulk.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItemBulk.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsWeighment.ReverseAndUnpost(fndDocNO.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    loadData(fndDocNO.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        Try
            If chkBulkMilkProc.IsChecked = False AndAlso chkMccProc.IsChecked = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select Weighment Type.", Me.Text)
                Exit Sub
            End If
            Dim whrCls As String = String.Empty
            ' Ticket No : ERO/25/02/19-000493 By Prabhakar 
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GateOutTankerNoAfterGeneralWeighment, clsFixedParameterCode.GateOutTankerNoAfterGeneralWeighment, Nothing)) = "1", True, False)) Then
                whrCls = " Gate_Entry_Type not in ('J') and "
            End If
            whrCls = whrCls & " isPosted=1  " & IIf(chkBulkMilkProc.IsChecked, " and doc_type='BulkProc'", IIf(chkMccProc.IsChecked, " and doc_type='MccProc'", "")) & "  and gate_entry_no not in (select gate_entry_no from  tspl_weighment_detail where gate_entry_no<>'" & fndGateEntryNoBulk.Value & "' " & IIf(chkPendingTare.Checked, " and  isPosted=1 and Tare_Weight>0 ", "") & " union all select gate_entry_no from tspl_gate_out   ) "
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = whrCls & " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
                Else
                    whrCls = whrCls & ""
                End If
            End If
            'If AllowbulkProcurementSequencewise = 1 Then
            '    If FirstQualityThenWeighment = 0 Then
            '        whrCls = whrCls & ""
            '    End If
            'End If

            'If AllowbulkProcurementSequencewise = 1 Then
            '    If FirstQualityThenWeighment = 0 Then
            '        whrCls = whrCls & " and (TSPL_gate_entry_details.gate_entry_no  in( select Gate_Entry_No  from TSPL_QUALITY_CHECK where TSPL_QUALITY_CHECK.is_Param_Accepted <> 0  and isPosted=1) or TSPL_gate_entry_details.IsNetWeight=1) "
            '    End If
            'Else


            'End If

            If AllowbulkProcurementSequencewise = 1 Then
                If FirstQualityThenWeighment = 1 Then
                    whrCls = whrCls & " and TSPL_gate_entry_details.gate_entry_no  in( select Gate_Entry_No  from TSPL_QUALITY_CHECK where TSPL_QUALITY_CHECK.is_Param_Accepted <> 0  and isPosted=1) "
                End If
            End If

            'If chkMccProc.IsChecked = True Then
            '    whrCls = whrCls & " And  (2= (case when exists( select 1   from TSPL_QUALITY_CHECK where TSPL_gate_entry_details.gate_entry_no =TSPL_QUALITY_CHECK.Gate_Entry_No And TSPL_QUALITY_CHECK.is_Param_Accepted <> 0  And isPosted=1) And tspl_gate_entry_details.IsNetWeight=1 then 2 else 3 end) or tspl_gate_entry_details.IsNetWeight=0) "
            'End If



            'whrCls = whrCls & " and TSPL_gate_entry_details.Tanker_Return=0  "
            'and tspl_gate_entry_details.In_Return=0"
            whrCls = whrCls & " and TSPL_gate_entry_details.Tanker_Return=0  and tspl_gate_entry_details.In_Return=0"
            fndGateEntryNoBulk.Value = clsGateEntry.getTankerFinder(whrCls, fndTankerNo.Value)
            'reset()
            If clsCommon.myLen(fndGateEntryNoBulk.Value) > 0 Then
                If chkBulkMilkProc.IsChecked Then
                    loadGateEntryDataBulk(fndGateEntryNoBulk.Value)
                Else
                    loadGateEntryDataMcc(fndGateEntryNoBulk.Value)
                End If
                'btnSave.Enabled = True
            Else
                reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub funPrint(ByVal strDocNo As String)
        Dim frmCRV As New frmCrystalReportViewer()
        Dim PrintTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToPrintTimeWithDocumentDate, clsFixedParameterCode.AllowToPrintTimeWithDocumentDate, Nothing)
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                Dim StrDoc_Type As String = Nothing
                Dim Qry As String = Nothing
                If chkBulkMilkProc.CheckState = CheckState.Checked Then
                    StrDoc_Type = "BulkProc"
                Else
                    StrDoc_Type = "MccProc"
                End If
                Dim chkQuery As String = "select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + clsCommon.myCstr(fndGateEntryNoBulk.Value) + "' "
                Dim dtchk As DataTable = clsDBFuncationality.GetDataTable(chkQuery)

                'If clsCommon.CompairString(clsCommon.myCstr(dtchk.Rows(0)("Gate_Entry_Type")), "J") = CompairStringResult.Equal Then
                '    'Dim StrSelectParameters As String = Nothing

                '    'Dim StrParaMeters As String = Nothing

                '    'StrSelectParameters = clsDBFuncationality.getSingleValue("select ISNULL(substring(a.xvalue,0,len(a.xvalue)-0),'') from (select distinct (select 'MAX('+'['+description+']'+') '+ 'AS ' +'['+Description+'],' from tspl_parameter_master WHERE ParameterType in('Job Work','procurement') for xml path('')) as xvalue)a")
                '    'If clsCommon.myLen(StrSelectParameters) > 0 Then

                '    '    StrParaMeters = clsDBFuncationality.getSingleValue("select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct (select '['+description+'],' from tspl_parameter_master WHERE ParameterType in('Job Work','procurement') for xml path('')) as xvalue)a")

                '    '    Qry = "select max(Weighment_No) as Weighment_No ,max(Weighment_date) as Weighment_date,max(Time) as Time,max(Vendor_Code) as Vendor_Code,max(vendor_Name) as vendor_Name  ,max(tanker_no) as tanker_no,max(Gross_Weight) as Gross_Weight,max(Tare_Weight)as Tare_Weight,max(Net_Weight) as Net_Weight,max(Date_And_Time) as Date_And_Time,max(Gate_Entry_No) as Gate_Entry_No ,max(Supplier_Code) as Supplier_Code ,max(Sub_SupplierName) as Sub_SupplierName ,max(CHAMBER_DESC) as CHAMBER_DESC ,max(Chamber_Qty) as Chamber_Qty,max(MILK_GRADE_CODE) as MILK_GRADE_CODE, max(Doc_Type) as Doc_Type," & _
                '    '    " " + StrSelectParameters + ",max(Seal_Status) as Seal_Status,max(Gate_Entry_Type) as Gate_Entry_Type,MAX(MIKL_TYPE_CODE) AS MIKL_TYPE_CODE,MAX(Comp_Name) AS Comp_Name " & _
                '    '    " from ( select * from " & _
                '    ' "(select max(Weighment_No) as Weighment_No ,max(Weighment_date) as Weighment_date,max(Time) as Time ,max(Vendor_Code) as Vendor_Code,max(vendor_Name) as vendor_Name  ,max(tanker_no) as tanker_no,max(Gross_Weight) as Gross_Weight,max(Tare_Weight)as Tare_Weight, max(Net_Weight) as Net_Weight,max(Date_And_Time) as Date_And_Time,max(Gate_Entry_No) as Gate_Entry_No ,max(Supplier_Code) as Supplier_Code ,max(Sub_SupplierName) as Sub_SupplierName ,pp.LINE_NO,max(CHAMBER_DESC) as CHAMBER_DESC ,(Param_Type) as Param_Type ,max(Param_Field_Desc) as Param_Field_Desc,max(Param_Field_Value) as Param_Field_Value ,max(Chamber_Qty) as Chamber_Qty,max(MILK_GRADE_CODE)  as MILK_GRADE_CODE ,max(Doc_Type) as Doc_Type,max(Seal_Status) as Seal_Status,max(Gate_Entry_Type) as Gate_Entry_Type,MAX(MIKL_TYPE_CODE) AS MIKL_TYPE_CODE,MAX(Comp_Name) AS Comp_Name  " & _
                '    '"from(select TSPL_Weighment_Detail.Weighment_No ,convert(varchar(15),cast(TSPL_Weighment_Detail.Weighment_date as Time) ,100) as  Time,convert(varchar,TSPL_Weighment_Detail.Weighment_date,103)  as Weighment_date, " & _
                '    ' "(CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_Weighment_Detail.Dispatched_From_Mcc ELSE  TSPL_Weighment_Detail.Vendor_Code END)AS Vendor_Code, " & _
                '    '" (CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_MCC_MASTER.MCC_NAME ELSE TSPL_VENDOR_MASTER.Vendor_Name END) AS vendor_Name ," & _
                '    ' "TSPL_Weighment_Detail.Doc_Type,TSPL_Weighment_Detail.tanker_no,TSPL_Weighment_Detail.Gross_Weight_Header as Gross_Weight,TSPL_Weighment_Detail.Tare_Weight,TSPL_Weighment_Detail.Net_Weight,TSPL_Weighment_Detail.Date_And_Time ,TSPL_Weighment_Detail.Gate_Entry_No ,Tspl_Gate_Entry_Details.Supplier_Code ,TSPL_SUPPLIER_MASTER.DESCRIPTION as Sub_SupplierName ,TSPL_WEIGHMENT_CHEMBER_DETAILS.LINE_NO,TSPL_CONTRACT_TANKER_DETAIL.CHAMBER_DESC  " & _
                '    ' ",TSPL_QC_Parameter_Detail.Param_Type ,TSPL_QC_Parameter_Detail.Param_Field_Desc ,TSPL_QC_Parameter_Detail.Param_Field_Value as Param_Field_Value ,TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Qty ,TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE ,Tspl_Gate_Entry_Details.Seal_Status,Tspl_Gate_Entry_Details.Gate_Entry_Type,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE,TSPL_COMPANY_MASTER.Comp_Name " & _
                '    ' " from TSPL_Weighment_Detail " & _
                '    ' "left join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No  =TSPL_Weighment_Detail.Weighment_No  " & _
                '    ' "left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_Weighment_Detail.Vendor_Code  " & _
                '    ' "left join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_Weighment_Detail.Gate_Entry_No  " & _
                '    ' "left join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.weighment_no =TSPL_Weighment_Detail.weighment_no " & _
                '    ' "left join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHEMBER_DETAILS.QC_No =TSPL_QUALITY_CHECK.QC_No " & _
                '    ' "and TSPL_QUALITY_CHEMBER_DETAILS.Line_No =TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No " & _
                '    ' "LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_Weighment_Detail.comp_code " & _
                '    ' "left join TSPL_QC_Parameter_Detail on TSPL_QC_Parameter_Detail.QC_No =TSPL_QUALITY_CHECK.QC_No " & _
                '    ' "and .LINE_NO =TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No " & _
                '    ' "left join TSPL_SUPPLIER_MASTER on TSPL_SUPPLIER_MASTER.SUPPLIER_CODE =Tspl_Gate_Entry_Details.Supplier_Code " & _
                '    ' "left join TSPL_CONTRACT_TANKER_DETAIL  on TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE=TSPL_QUALITY_CHECK.Tanker_No and " & _
                '    ' "TSPL_QC_Parameter_Detail.LINE_NO=TSPL_CONTRACT_TANKER_DETAIL.LINE_NO " & _
                '    '  " LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_Weighment_Detail.Dispatched_From_Mcc " & _
                '    ' "where TSPL_Weighment_Detail.Weighment_No ='" + strDocNo + "'  " & _
                '    '  ") as pp group by pp. LINE_NO ,Param_Type  ) as final " & _
                '    ' "pivot (MAX(Param_Field_Value) for Param_Field_Desc in (" + StrParaMeters + " )) as Parameter " & _
                '    '" ) as xx group by Line_No "TSPL_QC_Parameter_Detail

                '    Qry = "select wd.Weighment_No,convert(varchar(15),wd.Weighment_date,103) as Weighment_date,WD.Doc_Type,(CASE WHEN WD.Doc_Type='MccProc' THEN TSPL_MCC_MASTER.MCC_NAME ELSE vn.Vendor_Name END) AS Vendor_Name ,wd.Date_And_Time,wd.TANKER_NO,wd.Gate_Entry_No ," & _
                '  "ct.CHAMBER_DESC  ,qcp.Param_Field_Desc,qcp.Param_Field_Value,qcp.Param_Type,c.Comp_Name,(CASE WHEN WD.Doc_Type='MccProc' THEN WD.Dispatched_From_Mcc ELSE  vn.Vendor_Code END)AS Vendor_Code,GE.Gate_Entry_Type,ge.Supplier_Code,SUB_VM.DESCRIPTION AS Sub_SupplierName,wd.Gross_Weight_Header as Gross_Weight,wd.Tare_Weight,wd.Net_Weight,ge.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE,wd.Challan_No,wd.Challan_Date " & _
                '   " from TSPL_Weighment_Detail wd " & _
                '   " left join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No  =wd.Weighment_No  " & _
                '  " left join TSPL_VENDOR_MASTER vn on vn.Vendor_Code=wd.Vendor_Code" & _
                '  " left outer join Tspl_Gate_Entry_Details ge on ge.Gate_Entry_No=wd.Gate_Entry_No " & _
                '   " left outer join TSPL_CONTRACT_TANKER_MASTER cm on cm.TANKER_CODE=ge.Tanker_No " & _
                '     " left outer join TSPL_CONTRACT_TANKER_DETAIL ct on ct.TANKER_CODE=ge.Tanker_No " & _
                '     " left outer join TSPL_QUALITY_CHECK qc on qc.Gate_Entry_No=ge.Gate_Entry_No " & _
                '     " left join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHEMBER_DETAILS.QC_No =qc.QC_No " & _
                '    "and TSPL_QUALITY_CHEMBER_DETAILS.Line_No =TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No " & _
                '    " left outer join TSPL_QC_Parameter_Detail qcp on qcp.QC_No=qc.QC_No " & _
                '     " left outer join TSPL_COMPANY_MASTER c on c.Comp_Code=wd.comp_code " & _
                '     " LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=WD.Dispatched_From_Mcc " & _
                '     " left outer join TSPL_SUPPLIER_MASTER SUB_VM on SUB_VM.SUPPLIER_CODE=ge.Supplier_Code " & _
                '    " where wd.Weighment_No='" + strDocNo + "'  and " & _
                '    " qcp.Param_Field_Code in(select Code from tspl_parameter_master where ParameterType in('procurement','Job Work'))  "

                '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                '    If dtchk.Rows.Count > 0 Then
                '        ' frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "WeigntmenUDLRpt", "Milk Receipt Slip")
                '        frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkReceiptSlipUDL_CH", "Milk Receipt Slip")
                '    Else
                '        clsCommon.MyMessageBoxShow("Please set the Parameter First.")
                '    End If

                'Else

                '    Qry = "select wd.Weighment_No,convert(varchar(15),wd.Weighment_date,103) as Weighment_date,WD.Doc_Type,(CASE WHEN WD.Doc_Type='MccProc' THEN TSPL_MCC_MASTER.MCC_NAME ELSE vn.Vendor_Name END) AS Vendor_Name ,wd.Date_And_Time,wd.TANKER_NO,wd.Gate_Entry_No ," & _
                '        "ct.CHAMBER_DESC  ,qcp.Param_Field_Desc,qcp.Param_Field_Value,qcp.Param_Type,c.Comp_Name,(CASE WHEN WD.Doc_Type='MccProc' THEN WD.Dispatched_From_Mcc ELSE  vn.Vendor_Code END)AS Vendor_Code,GE.Gate_Entry_Type,ge.Supplier_Code,SUB_VM.Vendor_Name AS Sub_SupplierName,wd.Gross_Weight_Header as Gross_Weight,wd.Tare_Weight,wd.Net_Weight,wd.Challan_No,wd.Challan_Date "
                '    Qry += " from TSPL_Weighment_Detail wd "
                '    Qry += " left join TSPL_VENDOR_MASTER vn on vn.Vendor_Code=wd.Vendor_Code"
                '    Qry += " left outer join Tspl_Gate_Entry_Details ge on ge.Gate_Entry_No=wd.Gate_Entry_No"
                '    Qry += " left outer join TSPL_CONTRACT_TANKER_MASTER cm on cm.TANKER_CODE=ge.Tanker_No"
                '    Qry += " left outer join TSPL_CONTRACT_TANKER_DETAIL ct on ct.TANKER_CODE=ge.Tanker_No"
                '    Qry += " left outer join TSPL_QUALITY_CHECK qc on qc.Gate_Entry_No=ge.Gate_Entry_No"
                '    Qry += " left outer join TSPL_QC_Parameter_Detail qcp on qcp.QC_No=qc.QC_No "
                '    Qry += " left outer join TSPL_COMPANY_MASTER c on c.Comp_Code=wd.comp_code " & _
                '           " LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=WD.Dispatched_From_Mcc " & _
                '           " left outer join TSPL_VENDOR_MASTER SUB_VM on SUB_VM.Vendor_Code=ge.Supplier_Code "
                '    Qry += " where wd.Weighment_No='" + strDocNo + "'  and "
                '    'If clsCommon.CompairString(clsCommon.myCstr(dtchk.Rows(0)("Gate_Entry_Type")), "J") = CompairStringResult.Equal Then

                '    'Qry += "qcp.Param_Field_Code in(select Description from tspl_parameter_master where ParameterType in('procurement','Job Work'))  "
                '    'Else
                '    Qry += "qcp.Param_Field_Code in(select Code from tspl_parameter_master where ParameterType in('procurement','Purchase')) "
                '    '  End If
                '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                '    Dim Chamber As Integer = ((From row In dt
                '                             Select row("CHAMBER_DESC") Distinct).Count())
                '    Dim rpt As String = "rptMilkReceiptSlipUDL_CH" + Chamber.ToString()

                '    frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, rpt, "Milk Receipt Slip")

                'End If
                'Qry = " select * from (select 1 as SNO, wd.Weighment_No,convert(varchar(15),wd.Weighment_date,103) as Weighment_date,WD.Doc_Type,(CASE WHEN WD.Doc_Type='MccProc' THEN TSPL_MCC_MASTER.MCC_NAME ELSE vn.Vendor_Name END) AS Vendor_Name ,wd.Date_And_Time,wd.TANKER_NO,wd.Gate_Entry_No ,ct.CHAMBER_DESC ,qcp.QC_No ,qcp.Param_Field_Desc,qcp.Param_Field_Value,qcp.Param_Type,c.Comp_Name,(CASE WHEN WD.Doc_Type='MccProc' THEN WD.Dispatched_From_Mcc ELSE  vn.Vendor_Code END)AS Vendor_Code,GE.Gate_Entry_Type,ge.Supplier_Code,SUB_VM.Vendor_Name AS Sub_SupplierName,wd.Gross_Weight_Header as Gross_Weight,wd.Tare_Weight,wd.Net_Weight,wd.Challan_No,wd.Challan_Date  from TSPL_Weighment_Detail wd  left join TSPL_VENDOR_MASTER vn on vn.Vendor_Code=wd.Vendor_Code left outer join Tspl_Gate_Entry_Details ge on ge.Gate_Entry_No=wd.Gate_Entry_No left outer join TSPL_CONTRACT_TANKER_MASTER cm on cm.TANKER_CODE=ge.Tanker_No left outer join TSPL_CONTRACT_TANKER_DETAIL ct on ct.TANKER_CODE=ge.Tanker_No left outer join TSPL_QUALITY_CHECK qc on qc.Gate_Entry_No=ge.Gate_Entry_No left outer join TSPL_QC_Parameter_Detail qcp on qcp.QC_No=qc.QC_No  left outer join TSPL_COMPANY_MASTER c on c.Comp_Code=wd.comp_code  LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=WD.Dispatched_From_Mcc  left outer join TSPL_VENDOR_MASTER SUB_VM on SUB_VM.Vendor_Code=ge.Supplier_Code  where wd.Weighment_No='" + strDocNo + "'  and " & _
                '      " qcp.Param_Field_Code IN ('FAT','CLR','SNF','ABB','AAB','GRADE','CHHENA','NA-PPM','REICHERT MEISSL VALUE') " & _
                '      " union all " & _
                '      " select 2 as SNO, '' as Weighment_No,'' AS Weighment_date,'' AS Doc_Type,'' AS Vendor_Name,  '' AS Date_And_Time,'' AS TANKER_NO ,'' AS Gate_Entry_No,TSPL_Quality_Chember_Details.Chamber_Desc AS CHAMBER_DESC,   '' AS QC_No ,'Quantity in Kg' AS Param_Field_Desc ,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight AS Param_Field_Value,'' AS Param_Type,'' as Comp_Name,'' as  Vendor_Code ,'' as Gate_Entry_Type,'' as Supplier_Code,'' as Sub_SupplierName,0 as Gross_Weight, 0 as Tare_Weight, '' as Net_Weight,  '' as  Challan_No , '' as Challan_Date  from TSPL_Quality_Chember_Details  left join " & _
                '     "(select  top 1 TSPL_QC_Parameter_Detail.QC_No,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight  from TSPL_Weighment_Detail left outer join Tspl_Gate_Entry_Details  on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.Gate_Entry_No=TSPL_Weighment_Detail.Gate_Entry_No  left outer join TSPL_QC_Parameter_Detail on TSPL_QC_Parameter_Detail.QC_No=TSPL_QUALITY_CHECK.QC_No " & _
                '      " left join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.weighment_No=TSPL_Weighment_Detail.weighment_No and TSPL_WEIGHMENT_CHEMBER_DETAILS.line_no=TSPL_QC_Parameter_Detail.line_no " & _
                '     " where TSPL_Weighment_Detail.Weighment_No='" + strDocNo + "') as TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.QC_No =TSPL_Quality_Chember_Details.QC_No where TSPL_Quality_Chember_Details.QC_No=(select  top 1 TSPL_QC_Parameter_Detail.QC_No from TSPL_Weighment_Detail left outer join Tspl_Gate_Entry_Details  on TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No " & _
                '      " left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.Gate_Entry_No=TSPL_Weighment_Detail.Gate_Entry_No " & _
                '      " left outer join TSPL_QC_Parameter_Detail on TSPL_QC_Parameter_Detail.QC_No=TSPL_QUALITY_CHECK.QC_No " & _
                '      " where TSPL_Weighment_Detail.Weighment_No='" + strDocNo + "') " & _
                '      " ) as main " & _
                '      " pivot ( max(Param_Field_Value) for CHAMBER_DESC in ([F],[R],[M])) PIVT "
                Dim LEFTjOINQRY As String = Nothing
                LEFTjOINQRY = " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Weighment_Detail.Vendor_Code" & _
                       " left outer join Tspl_Gate_Entry_Details  on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_Weighment_Detail.Gate_Entry_No " & _
                       "left outer join TSPL_CONTRACT_TANKER_MASTER cm on cm.TANKER_CODE=Tspl_Gate_Entry_Details.Tanker_No " & _
                       "left outer join TSPL_CONTRACT_TANKER_DETAIL on TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE=Tspl_Gate_Entry_Details.Tanker_No  " & _
                       "left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Weighment_Detail.comp_code  " & _
                       "LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_Weighment_Detail.Dispatched_From_Mcc  " & _
                       " left outer join TSPL_VENDOR_MASTER SUB_VM on SUB_VM.Vendor_Code=Tspl_Gate_Entry_Details.Supplier_Code " & _
                       " left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No"

                If chkBulkMilkProc.IsChecked = True Then
                    LEFTjOINQRY += " and TSPL_CONTRACT_TANKER_DETAIL.LINE_NO = TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No "
                End If

                LEFTjOINQRY += " where TSPL_Weighment_Detail.Weighment_No='" + strDocNo + "'        "


                Qry = " SELECT max(Comp_Name) as Comp_Name, max(Weighment_No) as Weighment_No,max(Weighment_date) as Weighment_date,max(Vendor_Name) as Vendor_Name ," & _
                        " max(Vendor_Code) as Vendor_Code,max(TANKER_NO) as TANKER_NO,MAX(Parm_Desc) AS Parm_Desc ,isnull(sum(M),0)+isnull(sum([2]),0) AS 'M',isnull(SUM(F),0)+isnull(sum([1]),0) AS 'F' ,isnull(SUM(R),0)+isnull(sum([3]),0) AS 'R',SUM(Net_Weight) AS Total_Qty " & _
                        " FROM (" & _
                        " select 1 as SNO, TSPL_Weighment_Detail.Weighment_No,convert(varchar(15),TSPL_Weighment_Detail.Weighment_date,103) as Weighment_date,TSPL_Weighment_Detail.Doc_Type," & _
                         " (CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_MCC_MASTER.MCC_NAME ELSE TSPL_VENDOR_MASTER.Vendor_Name END) AS Vendor_Name ," & _
                        " TSPL_Weighment_Detail.Date_And_Time,TSPL_Weighment_Detail.TANKER_NO,TSPL_Weighment_Detail.Gate_Entry_No , TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc ," & _
                          " TSPL_COMPANY_MASTER.Comp_Name,(CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_Weighment_Detail.Dispatched_From_Mcc ELSE  TSPL_VENDOR_MASTER.Vendor_Code " & _
                        " END)AS Vendor_Code,Tspl_Gate_Entry_Details.Gate_Entry_Type,Tspl_Gate_Entry_Details.Supplier_Code,SUB_VM.Vendor_Name AS Sub_SupplierName," & _
                        " TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight as Gross_Weight,TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight," & _
                        " TSPL_Weighment_Detail.Challan_No,TSPL_Weighment_Detail.Challan_Date ,TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight AS Param_Values,'Gross Weight' as Parm_Desc   " & _
                        " from TSPL_Weighment_Detail  " & _
                        "" + LEFTjOINQRY + ""

                Qry += " UNION ALL" & _
                        " select 2 as SNO, TSPL_Weighment_Detail.Weighment_No,convert(varchar(15),TSPL_Weighment_Detail.Weighment_date,103) as Weighment_date," & _
                        " TSPL_Weighment_Detail.Doc_Type,(CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_MCC_MASTER.MCC_NAME ELSE TSPL_VENDOR_MASTER.Vendor_Name END) AS Vendor_Name ," & _
                        " TSPL_Weighment_Detail.Date_And_Time,TSPL_Weighment_Detail.TANKER_NO,TSPL_Weighment_Detail.Gate_Entry_No , TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc ," & _
                          " TSPL_COMPANY_MASTER.Comp_Name,(CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_Weighment_Detail.Dispatched_From_Mcc ELSE  " & _
 " TSPL_VENDOR_MASTER.Vendor_Code END)AS Vendor_Code,Tspl_Gate_Entry_Details.Gate_Entry_Type,Tspl_Gate_Entry_Details.Supplier_Code,SUB_VM.Vendor_Name AS Sub_SupplierName," & _
" TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight as Gross_Weight,TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight," & _
" TSPL_Weighment_Detail.Challan_No,TSPL_Weighment_Detail.Challan_Date ,TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight AS Param_Values,'Tare Weight' as Parm_Desc " & _
               " from TSPL_Weighment_Detail " & _
                  "" + LEFTjOINQRY + ""

                Qry += "  UNION ALL " & _
           "  select 3 as SNO, TSPL_Weighment_Detail.Weighment_No,convert(varchar(15),TSPL_Weighment_Detail.Weighment_date,103) as Weighment_date," & _
         " TSPL_Weighment_Detail.Doc_Type,(CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_MCC_MASTER.MCC_NAME ELSE TSPL_VENDOR_MASTER.Vendor_Name END) AS Vendor_Name " & _
        " ,TSPL_Weighment_Detail.Date_And_Time,TSPL_Weighment_Detail.TANKER_NO,TSPL_Weighment_Detail.Gate_Entry_No , TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc ," & _
         " TSPL_COMPANY_MASTER.Comp_Name,(CASE WHEN TSPL_Weighment_Detail.Doc_Type='MccProc' THEN TSPL_Weighment_Detail.Dispatched_From_Mcc ELSE " & _
        " TSPL_VENDOR_MASTER.Vendor_Code END)AS Vendor_Code,Tspl_Gate_Entry_Details.Gate_Entry_Type,Tspl_Gate_Entry_Details.Supplier_Code,SUB_VM.Vendor_Name AS Sub_SupplierName," & _
        " TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight as Gross_Weight,TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight," & _
        " TSPL_Weighment_Detail.Challan_No,TSPL_Weighment_Detail.Challan_Date ,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight AS Param_Values,'Net Weight' as Parm_Desc   " & _
        " from TSPL_Weighment_Detail  " & _
          "" + LEFTjOINQRY + "" & _
     " ) as main  pivot ( max(Param_Values) for CHAMBER_DESC in ([F],[R],[M],[1],[2],[3])) PIVT " & _
    " group by SNO"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptWeighmentSlip", "Milk Receipt Slip")
            ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                ' Ticket No : BHA/03/07/18-000125 By Prabhakar -- Multi chember wise Print For Bharat
                ' Ticket No : BHA/16/07/18-000179 By prabhakar for  QC Status  on print 
                '==============================================================================================================================================

                Dim strReference_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 isnull (tspl_gate_entry_details.Reference_No,'') as Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + fndGateEntryNoBulk.Value + "'"))
                Dim isMultiChember As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Weighment_Chember_Details where TSPL_Weighment_Chember_Details.Weighment_No ='" + strDocNo + "'"))
                Dim Qry As String = ""
                Qry = "  select TSPL_LOCATION_MASTER_for_MCC.GSTNO as Mcc_GstInNo ,tspl_state_master_for_MCC.GST_STATE_Code as Mcc_GST_State_Code, TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_LOCATION_MASTER_for_MCC.CST_No as Mcc_CstNo,TSPL_VENDOR_MASTER.CST as Ven_CSTNo,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_Add3,TSPL_LOCATION_MASTER.CST_No as Loc_Cst,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,"
                Qry += " Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER.Phone1 end  +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Loc_Phone,tspl_weighment_detail.Weighment_No as Doc_no,TSPL_LOCATION_MASTER.Location_Desc "
                If PrintTime = "1" Then
                    Qry += "   ,tspl_weighment_detail.Weighment_date as Weighment_date"
                    Qry += "    ,tspl_weighment_detail.Challan_Date as Challan_Date "
                    Qry += "  ,tspl_weighment_detail.Date_And_Time "
                    Qry += " ,substring(convert(varchar(20), tspl_weighment_detail.Weighment_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Weighment_date, 9), 25, 2) as Weighment_time ,convert(varchar,tspl_weighment_detail.Tare_Weight_date,103) as tare_date ,substring(convert(varchar(20), tspl_weighment_detail.Tare_Weight_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Tare_Weight_date, 9), 25, 2) as Tare_time ,tspl_weighment_detail.Modify_By as Opereter"
                Else
                    Qry += "  ,convert(varchar,tspl_weighment_detail.Weighment_date,103) as Weighment_date"
                    Qry += " ,convert(varchar,tspl_weighment_detail.Challan_Date,103) as Challan_Date "
                    Qry += "   ,convert(varchar,tspl_weighment_detail.Date_And_Time,103)  as Date_And_Time"
                    Qry += " ,substring(convert(varchar(20), tspl_weighment_detail.Weighment_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Weighment_date, 9), 25, 2) as Weighment_time ,convert(varchar,tspl_weighment_detail.Tare_Weight_date,103) as tare_date ,substring(convert(varchar(20), tspl_weighment_detail.Tare_Weight_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Tare_Weight_date, 9), 25, 2) as Tare_time ,tspl_weighment_detail.Modify_By as Opereter"
                End If
                If isMultiChember > 0 Then
                    Qry += " ,TSPL_Weighment_Chember_Details.Item_Code, TSPL_Weighment_Chember_Details.Gross_Weight ,TSPL_Weighment_Chember_Details.Tare_Weight ,TSPL_Weighment_Chember_Details.Net_Weight,tspl_weighment_detail.UOM  "
                Else
                    Qry += " ,tspl_weighment_detail.Item_Code, tspl_weighment_detail.Gross_Weight ,tspl_weighment_detail.Tare_Weight ,tspl_weighment_detail.Net_Weight,tspl_weighment_detail.UOM  "
                End If

                Qry += "  ,tspl_weighment_detail.Gate_Entry_No ,'" + strReference_No + "' as Reference_No,tspl_weighment_detail.Doc_Type ,tspl_weighment_detail.Challan_No ,"
                Qry += "  tspl_weighment_detail.Tanker_No ,tspl_weighment_detail.isPosted ,convert(varchar,tspl_weighment_detail.Posting_Date,103) as Posting_Date,tspl_weighment_detail.Dispatched_From_Mcc,TSPL_LOCATION_MASTER_for_MCC.Location_Desc as Mcc_Name ,tspl_weighment_detail.location_Code ,tspl_weighment_detail.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_ITEM_MASTER .Item_Desc ,tspl_weighment_detail.Qty_In_Kg ,tspl_weighment_detail.Created_By ,tspl_weighment_detail.Created_Date ,tspl_weighment_detail.Modify_By ,tspl_weighment_detail.Modify_Date ,tspl_weighment_detail.comp_code ,tspl_weighment_detail.Dip_Value   ,case when  tspl_quality_check.isPosted=1 then 'Done' else 'Not Done' end as status ,tspl_weighment_detail.Weighment_Slip_No ,tspl_weighment_detail.Sent_to_QC_By ,convert(varchar,tspl_weighment_detail.Sent_To_QC_Date,103) as Sent_To_QC_Date ,tspl_weighment_detail.QC_Done_By ,tspl_weighment_detail.QC_Done_Date,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2  as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as comp_add3,TSPL_COMPANY_MASTER.Tin_No as Comp_tinNo,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Pincode as Comp_PinCode,TSPL_COMPANY_MASTER.Email as Comp_Email,Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_COMPANY_MASTER.Phone1 end "
                Qry += " +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Comp_Phone ,TSPL_LOCATION_MASTER_for_MCC.Location_Desc  as MCC_Desc ,TSPL_LOCATION_MASTER_for_MCC.Add1 as Mcc_Add1,TSPL_LOCATION_MASTER_for_MCC.Add2 as mcc_Add2,TSPL_LOCATION_MASTER_for_MCC.Add3 as Mcc_Add3,TSPL_LOCATION_MASTER_for_MCC.TIN_No as MCC_TinNo,Case when len(ISNULL(TSPL_LOCATION_MASTER_for_MCC.Phone1,''))>0 and TSPL_LOCATION_MASTER_for_MCC.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER_for_MCC.Phone1 end "
                Qry += "   +  Case When   ISNULL(TSPL_LOCATION_MASTER_for_MCC.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER_for_MCC.Phone2 Else'' End as MCC_Phone,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.Add1 as Vend_Add1,TSPL_VENDOR_MASTER.Add2 as Ven_Add2,TSPL_VENDOR_MASTER.Add3 as Ven_Add3,TSPL_VENDOR_MASTER.Tin_No as ven_tinno,Case when len(ISNULL(TSPL_VENDOR_MASTER.Phone1,''))>0 and TSPL_VENDOR_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_VENDOR_MASTER.Phone1 end "
                Qry += "  +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phone,TSPL_VENDOR_MASTER.City_Code_Desc as VendorCityName "


                Qry += "  from tspl_weighment_detail"
                Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_weighment_detail.comp_code "
                Qry += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_for_MCC on TSPL_LOCATION_MASTER_for_MCC.Location_Code =tspl_weighment_detail.Dispatched_From_Mcc "
                Qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =tspl_weighment_detail.Vendor_Code "
                Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_weighment_detail.Item_Code"
                Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =tspl_weighment_detail.location_Code    left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code left outer join  tspl_state_master as tspl_state_master_for_MCC on TSPL_LOCATION_MASTER_for_MCC.state =tspl_state_master_for_MCC.State_Code  "
                If isMultiChember > 0 Then
                    Qry += " left outer join TSPL_Weighment_Chember_Details on TSPL_Weighment_Chember_Details.Weighment_No = tspl_weighment_detail.Weighment_No "
                End If
                Qry += " left outer join tspl_quality_check on tspl_quality_check.Gate_Entry_No = tspl_weighment_detail.Gate_Entry_No "
                Qry += " where 2=2 and tspl_weighment_detail.Weighment_No ='" + strDocNo + "'"
                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                If dt3.Rows.Count > 0 Then

                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt3, clsERPFuncationality.CompanyAddresShowinFooter(), "crptWeighmetBulk", "Weighment", clsCommon.myCDate(dt3.Rows(0)("Weighment_date")), "rptCompanyAddress.rpt")
                End If
                '=============================================================================================================================================
            Else
                Dim Qry As String = "  select tspl_weighment_detail.Gross_Weight_Header,TSPL_LOCATION_MASTER_for_MCC.GSTNO as Mcc_GstInNo ,tspl_state_master_for_MCC.GST_STATE_Code as Mcc_GST_State_Code, TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_LOCATION_MASTER_for_MCC.CST_No as Mcc_CstNo,TSPL_VENDOR_MASTER.CST as Ven_CSTNo,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_Add3,TSPL_LOCATION_MASTER.CST_No as Loc_Cst,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,"
                Qry += " Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER.Phone1 end  +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Loc_Phone,tspl_weighment_detail.Weighment_No as Doc_no,TSPL_LOCATION_MASTER.Location_Desc "

                If PrintTime = "1" Then
                    Qry += "   ,tspl_weighment_detail.Weighment_date as Weighment_date"
                    Qry += "    ,tspl_weighment_detail.Challan_Date as Challan_Date "
                    Qry += "  ,tspl_weighment_detail.Date_And_Time "
                    Qry += " ,substring(convert(varchar(20), tspl_weighment_detail.Weighment_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Weighment_date, 9), 25, 2) as Weighment_time ,convert(varchar,tspl_weighment_detail.Tare_Weight_date,103) as tare_date ,substring(convert(varchar(20), tspl_weighment_detail.Tare_Weight_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Tare_Weight_date, 9), 25, 2) as Tare_time ,tspl_weighment_detail.Modify_By as Opereter"
                Else
                    Qry += "  ,convert(varchar,tspl_weighment_detail.Weighment_date,103) as Weighment_date"
                    Qry += " ,convert(varchar,tspl_weighment_detail.Challan_Date,103) as Challan_Date "
                    Qry += "   ,convert(varchar,tspl_weighment_detail.Date_And_Time,103)  as Date_And_Time"
                    Qry += " ,substring(convert(varchar(20), tspl_weighment_detail.Weighment_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Weighment_date, 9), 25, 2) as Weighment_time ,convert(varchar,tspl_weighment_detail.Tare_Weight_date,103) as tare_date ,substring(convert(varchar(20), tspl_weighment_detail.Tare_Weight_date, 9), 13, 5) + ' ' + substring(convert(varchar(30), tspl_weighment_detail.Tare_Weight_date, 9), 25, 2) as Tare_time ,tspl_weighment_detail.Modify_By as Opereter"
                End If



                Qry += "  ,tspl_weighment_detail.Gate_Entry_No ,tspl_weighment_detail.Doc_Type ,tspl_weighment_detail.Challan_No ,"
                Qry += "  tspl_weighment_detail.Tanker_No ,tspl_weighment_detail.isPosted ,convert(varchar,tspl_weighment_detail.Posting_Date,103) as Posting_Date,tspl_weighment_detail.Dispatched_From_Mcc,TSPL_LOCATION_MASTER_for_MCC.Location_Desc as Mcc_Name ,tspl_weighment_detail.location_Code ,tspl_weighment_detail.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name ,tspl_weighment_detail.Item_Code ,TSPL_ITEM_MASTER .Item_Desc ,tspl_weighment_detail.Qty_In_Kg ,tspl_weighment_detail.Created_By ,tspl_weighment_detail.Created_Date ,tspl_weighment_detail.Modify_By ,tspl_weighment_detail.Modify_Date ,tspl_weighment_detail.comp_code ,tspl_weighment_detail.Gross_Weight ,tspl_weighment_detail.Dip_Value ,tspl_weighment_detail.Tare_Weight ,tspl_weighment_detail.Net_Weight ,case when tspl_weighment_detail.status=1 then 'Not Done' else 'Done' end as status ,tspl_weighment_detail.Weighment_Slip_No ,tspl_weighment_detail.UOM ,tspl_weighment_detail.Sent_to_QC_By ,convert(varchar,tspl_weighment_detail.Sent_To_QC_Date,103) as Sent_To_QC_Date ,tspl_weighment_detail.QC_Done_By ,tspl_weighment_detail.QC_Done_Date,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2  as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as comp_add3,TSPL_COMPANY_MASTER.Tin_No as Comp_tinNo,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Pincode as Comp_PinCode,TSPL_COMPANY_MASTER.Email as Comp_Email,Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_COMPANY_MASTER.Phone1 end "
                Qry += " +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Comp_Phone ,TSPL_LOCATION_MASTER_for_MCC.Location_Desc  as MCC_Desc ,TSPL_LOCATION_MASTER_for_MCC.Add1 as Mcc_Add1,TSPL_LOCATION_MASTER_for_MCC.Add2 as mcc_Add2,TSPL_LOCATION_MASTER_for_MCC.Add3 as Mcc_Add3,TSPL_LOCATION_MASTER_for_MCC.TIN_No as MCC_TinNo,Case when len(ISNULL(TSPL_LOCATION_MASTER_for_MCC.Phone1,''))>0 and TSPL_LOCATION_MASTER_for_MCC.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER_for_MCC.Phone1 end "
                Qry += "   +  Case When   ISNULL(TSPL_LOCATION_MASTER_for_MCC.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER_for_MCC.Phone2 Else'' End as MCC_Phone,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.Add1 as Vend_Add1,TSPL_VENDOR_MASTER.Add2 as Ven_Add2,TSPL_VENDOR_MASTER.Add3 as Ven_Add3,TSPL_VENDOR_MASTER.Tin_No as ven_tinno,Case when len(ISNULL(TSPL_VENDOR_MASTER.Phone1,''))>0 and TSPL_VENDOR_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_VENDOR_MASTER.Phone1 end "
                Qry += "  +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phone,TSPL_VENDOR_MASTER.City_Code_Desc as VendorCityName "


                Qry += "  from tspl_weighment_detail"
                Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =tspl_weighment_detail.comp_code "
                Qry += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_for_MCC on TSPL_LOCATION_MASTER_for_MCC.Location_Code =tspl_weighment_detail.Dispatched_From_Mcc "
                Qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =tspl_weighment_detail.Vendor_Code "
                Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_weighment_detail.Item_Code"
                Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =tspl_weighment_detail.location_Code    left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code left outer join  tspl_state_master as tspl_state_master_for_MCC on TSPL_LOCATION_MASTER_for_MCC.state =tspl_state_master_for_MCC.State_Code  where 2=2"

                Qry += " and tspl_weighment_detail.Weighment_No ='" + strDocNo + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)


                Qry = " select max(Weighment_No) as Weighment_No ,max(Weighment_date) as Weighment_date,max(Time) as Time,max(Vendor_Code) as Vendor_Code,max(vendor_Name) as vendor_Name  ,max(tanker_no) as tanker_no,max(Net_Weight) as Net_Weight,max(Gate_Entry_No) as Gate_Entry_No ,max(Supplier_Code) as Supplier_Code ,max(SUpplier_name) as SUpplier_name ,max(CHAMBER_DESC) as CHAMBER_DESC ,max(Chamber_Qty) as Chamber_Qty,max(MILK_GRADE_CODE) as MILK_GRADE_CODE, " & _
                " max([Acid After Boiling]) as [Acid After Boiling],max([Acid Before Boiling]) as [Acid Before Boiling],max([CLR]) as [CLR],max([FAT]) as [FAT],max([SNF]) as [SNF],max([CHANNA]) as [CHANNA] from ( " & _
                " select * from (select max(Weighment_No) as Weighment_No ,max(Weighment_date) as Weighment_date,max(Time) as Time ,max(Vendor_Code) as Vendor_Code,max(vendor_Name) as vendor_Name  ,max(tanker_no) as tanker_no,max(Net_Weight) as Net_Weight,max(Gate_Entry_No) as Gate_Entry_No ,max(Supplier_Code) as Supplier_Code ,max(SUpplier_name) as SUpplier_name ,pp.LINE_NO,max(CHAMBER_DESC) as CHAMBER_DESC ,(Param_Type) as Param_Type ,max(Param_Field_Desc) as Param_Field_Desc,max(Param_Field_Value) as Param_Field_Value ,max(Chamber_Qty) as Chamber_Qty,max(MILK_GRADE_CODE)  as MILK_GRADE_CODE " & _
                " from (select TSPL_Weighment_Detail.Weighment_No ,convert(varchar(15),cast(TSPL_Weighment_Detail.Weighment_date as Time) ,100) as  Time,convert(varchar,TSPL_Weighment_Detail.Weighment_date,103)  as Weighment_date,TSPL_Weighment_Detail.Vendor_Code,TSPL_VENDOR_MASTER.vendor_Name  ,TSPL_Weighment_Detail.tanker_no,TSPL_Weighment_Detail.Net_Weight,TSPL_Weighment_Detail.Gate_Entry_No ,Tspl_Gate_Entry_Details.Supplier_Code ,TSPL_SUPPLIER_MASTER.DESCRIPTION as SUpplier_name ,TSPL_WEIGHMENT_CHEMBER_DETAILS.LINE_NO,TSPL_CONTRACT_TANKER_DETAIL.CHAMBER_DESC  " & _
                " ,TSPL_QC_Parameter_Detail.Param_Type ,TSPL_QC_Parameter_Detail.Param_Field_Desc ,convert(decimal(18,2),TSPL_QC_Parameter_Detail.Param_Field_Value) as Param_Field_Value ,TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Qty ,TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  " & _
                 " from TSPL_Weighment_Detail  " & _
                " left join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No  =TSPL_Weighment_Detail.Weighment_No  " & _
                " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_Weighment_Detail.Vendor_Code  " & _
                " left join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_Weighment_Detail.Gate_Entry_No  " & _
                " left join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.weighment_no =TSPL_Weighment_Detail.weighment_no " & _
                " left join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHEMBER_DETAILS.QC_No =TSPL_QUALITY_CHECK.QC_No " & _
                " and TSPL_QUALITY_CHEMBER_DETAILS.Line_No =TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No" & _
                " left join TSPL_QC_Parameter_Detail on TSPL_QC_Parameter_Detail.QC_No =TSPL_QUALITY_CHECK.QC_No " & _
                " and TSPL_QC_Parameter_Detail.LINE_NO =TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No " & _
                " left join TSPL_SUPPLIER_MASTER on TSPL_SUPPLIER_MASTER.SUPPLIER_CODE =Tspl_Gate_Entry_Details.Supplier_Code " & _
                " left join TSPL_CONTRACT_TANKER_DETAIL  on TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE=TSPL_QUALITY_CHECK.Tanker_No and TSPL_QC_Parameter_Detail.LINE_NO=TSPL_CONTRACT_TANKER_DETAIL.LINE_NO " & _
                " where TSPL_Weighment_Detail.Weighment_No ='" + strDocNo + "' and " & _
                " TSPL_Weighment_Detail.isPosted =1 and TSPL_QC_Parameter_Detail.Param_Type in ('FAT','SNF','CLR','AAB','ABB','CHANNA')" & _
                " ) as pp group by pp. LINE_NO ,Param_Type  ) as final " & _
                " pivot (sum (Param_Field_Value) for Param_Field_Desc in ([Acid After Boiling],[Acid Before Boiling],[CLR],[FAT],[SNF],[CHANNA])) as Parameter " & _
                " ) as xx group by Line_No "


                '============added by preeti gupta ticket no [BM00000009841] 28/09/2016
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                    If dt1.Rows.Count > 0 Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt1, clsERPFuncationality.CompanyAddresShowinFooter(), "WeigntmenUDLRpt", "WeigntmenUDLRpt", "rptCompanyAddress.rpt")
                    End If
                Else
                    If dt.Rows.Count > 0 Then

                        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptWeighmetBulk", "Weighment", clsCommon.myCDate(dt.Rows(0)("Weighment_date")), "rptCompanyAddress.rpt")
                    End If
                End If

            End If

            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(fndDocNO.Value) <= 0 Then
            myMessages.blankValue("Weighment not found to Print")
        Else
            funPrint(fndDocNO.Value)
        End If
    End Sub

    Private Sub gvItemBulk_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvItemBulk.CellFormatting
        Try
            If isNetWeight = True OrElse EnterWeightManuallyOnWeighmentInGrid = True Then
                Exit Sub
            End If
            If UcWeighing1.Enabled Then
                If e.Column.Index >= 0 Then
                    If e.Column Is gvItemBulk.Columns(colGrossWeight) Then
                        gvItemBulk.CurrentRow.Cells(colGrossWeight).ReadOnly = True
                    ElseIf e.Column Is gvItemBulk.Columns(colTareWeight) Then
                        gvItemBulk.CurrentRow.Cells(colTareWeight).ReadOnly = True
                    End If
                End If
            End If

            If FinalChamberwise = 1 Then
                If e.Column Is gvItemBulk.Columns(colGrossWeight) Then
                    If IsWeighmentUnloadingSequencewise = 1 Then
                        If e.Row.Index = 0 Then
                            gvItemBulk.CurrentRow.Cells(colGrossWeight).ReadOnly = False
                        Else
                            gvItemBulk.CurrentRow.Cells(colGrossWeight).ReadOnly = True
                        End If
                    End If
                End If
                If e.Column Is gvItemBulk.Columns(colTareWeight) Then
                    Dim UnLoadingCount As Integer = 0
                    If IsWeighmentUnloadingSequencewise = 1 Then
                        UnLoadingCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 Line_No from TSPL_MILK_UNLOADING left outer join TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No=TSPL_Milk_unloading_Chember_Details.Unloading_No where Weighment_No='" & fndDocNO.Value & "' and UnLoading_Status=1 order by Line_No desc"))
                        If e.RowIndex = UnLoadingCount OrElse e.RowIndex = UnLoadingCount - 1 Then
                            If UcWeighing1.Enabled = False Then
                                gvItemBulk.CurrentRow.Cells(colTareWeight).ReadOnly = False
                            End If
                        Else
                            gvItemBulk.CurrentRow.Cells(colTareWeight).ReadOnly = True
                        End If
                    Else
                        UnLoadingCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 Line_No from TSPL_MILK_UNLOADING left outer join TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No=TSPL_Milk_unloading_Chember_Details.Unloading_No where Weighment_No='" & fndDocNO.Value & "' and UnLoading_Status=1 order by Unloading_Sequence desc"))
                        If e.RowIndex = UnLoadingCount - 1 Then
                            If UcWeighing1.Enabled = False Then
                                gvItemBulk.CurrentRow.Cells(colTareWeight).ReadOnly = False
                            End If
                        Else
                            gvItemBulk.CurrentRow.Cells(colTareWeight).ReadOnly = True
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Sub CalculateNetWeight(ByVal intRowIndex As Integer)
        If NetWeightWithVendorWeight = 0 Then
            gvItemBulk.Rows(intRowIndex).Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.Rows(intRowIndex).Cells(colGrossWeight).Value - gvItemBulk.Rows(intRowIndex).Cells(colTareWeight).Value, 2)
            gvItemBulk.Rows(intRowIndex).Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.Rows(intRowIndex).Cells(colNetWeight).Value)
        Else
            If NetWeightWithVendorWeight = 1 Then
                VendorWeight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(weight,0) as Weight from TSPL_VENDOR_MASTER where Vendor_Code='" & lblVendorCodeBulk.Text & "'"))
            End If
            gvItemBulk.Rows(intRowIndex).Cells(colVendorWeight).Value = VendorWeight
            gvItemBulk.Rows(intRowIndex).Cells(colNetWeight).Value = MyMath.RoundDown((gvItemBulk.Rows(intRowIndex).Cells(colGrossWeight).Value - gvItemBulk.Rows(intRowIndex).Cells(colTareWeight).Value) - gvItemBulk.Rows(intRowIndex).Cells(colVendorWeight).Value, 2)
            gvItemBulk.Rows(intRowIndex).Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.Rows(intRowIndex).Cells(colNetWeight).Value)
        End If

    End Sub


    Private Sub gvItemBulk_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItemBulk.CellValueChanged
        Try
            If isNetWeight = True OrElse EnterWeightManuallyOnWeighmentInGrid = True Then
                If e.Column Is gvItemBulk.Columns(colGrossWeight) OrElse e.Column Is gvItemBulk.Columns(colTareWeight) Then
                    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 2)
                    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
                End If
                Exit Sub
            End If
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvItemBulk.Columns(colGrossWeight) Then
                    If AllowFractionInMCCTankerDispatchGrossQty = False Then
                        Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value))
                        If (clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value) - intPart) > 0 Then
                            gvItemBulk.CurrentRow.Cells(colGrossWeight).Value = intPart
                            CalculateNetWeight(gvItemBulk.CurrentRow.Index)
                            Throw New Exception("Gross Weight must be integer")
                        End If
                        gvItemBulk.CurrentRow.Cells(colGrossWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value, 0))
                    End If
                    CalculateNetWeight(gvItemBulk.CurrentRow.Index)

                    ''=======Sanjeet(Calculate Weight With Vendor Weight)================
                    'If NetWeightWithVendorWeight = 0 Then
                    '    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 2)
                    '    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
                    'Else
                    '    If NetWeightWithVendorWeight = 1 Then
                    '        VendorWeight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(weight,0) as Weight from TSPL_VENDOR_MASTER where Vendor_Code='" & lblVendorCodeBulk.Text & "'"))
                    '    End If
                    '    gvItemBulk.CurrentRow.Cells(colVendorWeight).Value = VendorWeight
                    '    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown((gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value) - gvItemBulk.CurrentRow.Cells(colVendorWeight).Value, 2)
                    '    gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
                    'End If
                    ''======================================================================
                End If

                If e.Column Is gvItemBulk.Columns(colTareWeight) Then
                    gvItemBulk.CurrentColumn = gvItemBulk.Columns(colSampleLifted)
                    If AllowFractionInMCCTankerDispatchGrossQty = False Then
                        Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value))
                        If (clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) - intPart) > 0 Then
                            gvItemBulk.CurrentRow.Cells(colTareWeight).Value = intPart
                            Throw New Exception("Tare Weight must be integer")
                        End If
                        gvItemBulk.CurrentRow.Cells(colTareWeight).Value = clsCommon.myFormat(MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 0))
                    End If
                    If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value) < clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) Then
                        gvItemBulk.CurrentRow.Cells(colTareWeight).Value = "0"
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = "0"
                        Throw New Exception("Tare Weight Can not be more than Gross weight")
                    End If
                    If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) < 0 Then
                        gvItemBulk.CurrentRow.Cells(colTareWeight).Value = "0"
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = "0"
                        Throw New Exception("Tare Weight Can not be negative or Zero")
                    End If

                    If NetWeightWithVendorWeight = 0 Then
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown(gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value, 2)
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
                    Else
                        If NetWeightWithVendorWeight = 1 Then
                            VendorWeight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(weight,0) as Weight from TSPL_VENDOR_MASTER where Vendor_Code='" & lblVendorCodeBulk.Text & "'"))
                        End If
                        gvItemBulk.CurrentRow.Cells(colVendorWeight).Value = VendorWeight
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = MyMath.RoundDown((gvItemBulk.CurrentRow.Cells(colGrossWeight).Value - gvItemBulk.CurrentRow.Cells(colTareWeight).Value) - gvItemBulk.CurrentRow.Cells(colVendorWeight).Value, 2)
                        gvItemBulk.CurrentRow.Cells(colNetWeight).Value = clsCommon.myFormat(gvItemBulk.CurrentRow.Cells(colNetWeight).Value)
                    End If
                    If FinalChamberwise = 1 And blnLoad = False AndAlso IsWeighmentUnloadingSequencewise = 1 Then
                        If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colTareWeight).Value) > 0 Then
                            Dim intCurrentRow As Integer = gvItemBulk.CurrentRow.Index
                            If intCurrentRow <> gvItemBulk.Rows.Count - 1 Then
                                If clsCommon.myLen(gvItemBulk.Rows(intCurrentRow + 1).Cells(colItemCode).Value) > 0 Then
                                    gvItemBulk.Rows(intCurrentRow + 1).Cells(colGrossWeight).Value = gvItemBulk.CurrentRow.Cells(colTareWeight).Value
                                End If
                            End If
                        End If
                    End If

                End If
            End If
            isCellValueChangedOpen = False
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateWeight_Click(sender As Object, e As EventArgs) Handles btnUpdateWeight.Click
        Try
            Dim qry As String = ""
            'If FinalChamberwise = 0 Then
            qry = clsDBFuncationality.ExecuteNonQuery("update TSPL_WEIGHMENT_DETAIL set Gross_Weight=" & txtNewGross.Value & ",Tare_Weight=" & txtNewTare.Value & ",Net_Weight=" & txtNewNetWt.Value & " , Gross_Weight_Header = " & txtNewGross.Value & " where Weighment_No='" & fndDocNO.Value & "'")
            'Else
            qry = clsDBFuncationality.ExecuteNonQuery("update TSPL_WEIGHMENT_CHEMBER_DETAILS set Gross_Weight=" & txtNewGross.Value & ",Tare_Weight=" & txtNewTare.Value & ",Net_Weight=" & txtNewNetWt.Value & " where Weighment_No='" & fndDocNO.Value & "' and Line_No=" & txtLineNo.Value & "")
            'End If
            clsCommon.MyMessageBoxShow(Me, "Updated Successfully", Me.Text)
            Panel2.Visible = False
            txtLineNo.Value = 0
            txtNewGross.Value = 0
            txtNewTare.Value = 0
            txtNewNetWt.Value = 0
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtNewTare_TextChanged(sender As Object, e As EventArgs) Handles txtNewTare.TextChanged
        txtNewNetWt.Value = clsCommon.myCdbl(txtNewGross.Value) - clsCommon.myCdbl(txtNewTare.Value)
    End Sub
End Class
