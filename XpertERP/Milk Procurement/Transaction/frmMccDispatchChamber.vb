Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmMccDispatchChamber
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isdipmarkingmendatory As Boolean = False
    Public isLoadingBulkSaleTanker As Boolean = False
    Public AllowCanInformationintoGridForTankerDispatch As Boolean = False
    Public strDocNo As String = ""
    Const colSlNo As String = "SlNo"
    Const colSealNo As String = "SealNo"
    Const colIsCanType As String = "colIsCanType"
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
    Public obj As clsMccDispatch = Nothing
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
    Dim settFATPerShouldBeMultipleOf5 As Boolean = True


#End Region

    Private Sub FrmMccDispatch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MCC_DISPATCH_SILO_WISE alter column FAT_Per decimal(18,10)")
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MCC_DISPATCH_SILO_WISE alter column SNF_Per decimal(18,10)")

        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        ButtonToolTip.SetToolTip(btnReverse, "Press Alt+R to Reverse the Transaction")
        If clsCommon.myLen(strDocNo) > 0 Then
            fndChalanNo.Value = strDocNo
            LoadData(fndChalanNo.Value, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndChalanNo.Value = clsCommon.myCstr(Me.Tag)
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        settFATPerShouldBeMultipleOf5 = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATPerShouldBeMultipleOf5, clsFixedParameterCode.FATPerShouldBeMultipleOf5, Nothing)) = 1)
        settTankerDispatchAvgFATSNFPer = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, Nothing)) = 1)
        settAllowtoNegativeFATSNFKgAtTankerDispatch = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeFATSNFKgAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeFATSNFKgAtTankerDispatch, Nothing)) = 1)
        settTankerDispatchIntermittentSingleGateIn = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, Nothing)) = 1)
        IsAutoTankerWeightment = True ''IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoTankerWeightment, clsFixedParameterCode.IsAutoTankerWeightment, Nothing)) = 1, False, True)
        CreateProvisionOfTankerDispatchWithClosingKM = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOfTankerDispatchWithClosingKM, clsFixedParameterCode.CreateProvisionOfTankerDispatchWithClosingKM, Nothing)) = 0, False, True)
        ShowTankerWithoutCheckingAnyValidation = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTankerWithoutCheckingAnyValidation, clsFixedParameterCode.ShowTankerWithoutCheckingAnyValidation, Nothing)) = 0, False, True)
        AllowCanInformationintoGridForTankerDispatch = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCanInformationintoGridForTankerDispatch, clsFixedParameterCode.AllowCanInformationintoGridForTankerDispatch, Nothing)) = 0, False, True)
        RadPageView1.SelectedPage = RadPageViewPage1
        ''=====Preeti========
        ApplyManualTransferRateOnMultipleChamberTankerDispatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyMaTransRateOnMultChamberTankerDis, clsFixedParameterCode.ApplyMaTransRateOnMultChamberTankerDis, Nothing))
        If ApplyManualTransferRateOnMultipleChamberTankerDispatch = 1 Then
            GroupBox1.Visible = True
        Else
            GroupBox1.Visible = False
        End If
        If CreateProvisionOfTankerDispatchWithClosingKM = True Then
            RadGroupBox4.Visible = True
        Else
            RadGroupBox4.Visible = False
        End If
        '=======
        ''ERO/07/05/18-000294 by Balwinder on 08/05/2018
        ApplyTotalSolidPriceChart = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyTotalSolidPriceChart, clsFixedParameterCode.ApplyTotalSolidPriceChart, Nothing)) = 1)
        grpTS.Visible = ApplyTotalSolidPriceChart
        If ApplyTotalSolidPriceChart AndAlso ApplyManualTransferRateOnMultipleChamberTankerDispatch = 1 Then
            clsCommon.MyMessageBoxShow("Setting -" + clsFixedParameterType.ApplyMaTransRateOnMultChamberTankerDis + " and " + clsFixedParameterType.ApplyTotalSolidPriceChart + "cannot be On.Please Off one/Both settings")
            Me.Close()
        End If
        AllowToEnterSnfAtPlantInMccTankerDispatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToEnterSnfAtPlantInMccTankerDispatch, clsFixedParameterCode.AllowToEnterSnfAtPlantInMccTankerDispatch, Nothing))
        settFirstGateOutProcessForBulkProcument = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FirstGateOutProcessForMCCBulkProcument, clsFixedParameterCode.FirstGateOutProcessForMCCBulkProcument, Nothing))
        EnableTankerNoInMccTankerDispWithMccTankerGateOut = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTankerNoInMccTankerDispWithMccTankerGateOut, clsFixedParameterCode.EnableTankerNoInMccTankerDispWithMccTankerGateOut, Nothing))
        lblGateOut.Visible = settFirstGateOutProcessForBulkProcument
        txtGateOut.Visible = settFirstGateOutProcessForBulkProcument
        txtGateOut.Enabled = settFirstGateOutProcessForBulkProcument
        If settFirstGateOutProcessForBulkProcument = True AndAlso EnableTankerNoInMccTankerDispWithMccTankerGateOut = True Then
            fndTnakerNo.Enabled = True
        Else
            fndTnakerNo.Enabled = Not settFirstGateOutProcessForBulkProcument
        End If

        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, Nothing)) = 0, False, True) = False Then
            MyLabel25.Visible = True
            txtTollAmount.Visible = True
            btnUpdateAfterPost.Enabled = False
            btnClKM.Enabled = False
        Else
            MyLabel25.Visible = False
            txtTollAmount.Visible = False
            btnUpdateAfterPost.Enabled = True
            btnClKM.Enabled = True
        End If


    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Reset()
    End Sub

    Sub loadBlankGridManualSeal()
        gvManualSeal.Rows.Clear()
        gvManualSeal.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvManualSeal.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.Name = colSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = False
        gvManualSeal.MasterTemplate.Columns.Add(repoCode)

        For i As Integer = 1 To 10
            gvManualSeal.Rows.AddNew()
            gvManualSeal.Rows(i - 1).Cells(colSlNo).Value = i.ToString
            gvManualSeal.Rows(i - 1).Cells(colSealNo).Value = ""
        Next
        gvManualSeal.AllowAddNewRow = False
        gvManualSeal.AllowDeleteRow = False
        gvManualSeal.ShowGroupPanel = False
        gvManualSeal.AllowColumnReorder = False
        gvManualSeal.AllowRowReorder = False
        gvManualSeal.EnableSorting = False
        gvManualSeal.MasterTemplate.ShowRowHeaderColumn = False
        gvManualSeal.MasterTemplate.ShowColumnHeaders = True
        gvManualSeal.EnableAlternatingRowColor = True
        gvManualSeal.EnableFiltering = False
        gvManualSeal.ShowFilteringRow = False
        gvManualSeal.TableElement.TableHeaderHeight = 40
    End Sub

    Sub loadBlankGridPaperSeal()
        gvPaperSeal.Rows.Clear()
        gvPaperSeal.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvPaperSeal.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.Name = colSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = False
        gvPaperSeal.MasterTemplate.Columns.Add(repoCode)

        For i As Integer = 1 To 10
            gvPaperSeal.Rows.AddNew()
            gvPaperSeal.Rows(i - 1).Cells(colSlNo).Value = i.ToString
            gvPaperSeal.Rows(i - 1).Cells(colSealNo).Value = ""
        Next
        gvPaperSeal.AllowAddNewRow = False
        gvPaperSeal.AllowDeleteRow = False
        gvPaperSeal.ShowGroupPanel = False
        gvPaperSeal.AllowColumnReorder = False
        gvPaperSeal.AllowRowReorder = False
        gvPaperSeal.EnableSorting = False
        gvPaperSeal.MasterTemplate.ShowRowHeaderColumn = False
        gvPaperSeal.MasterTemplate.ShowColumnHeaders = True
        gvPaperSeal.EnableAlternatingRowColor = True
        gvPaperSeal.EnableFiltering = False
        gvPaperSeal.ShowFilteringRow = False
        gvPaperSeal.TableElement.TableHeaderHeight = 40
    End Sub

    Public Sub GetECOPro()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "[None]"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Eco Pro 1"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "Eco Pro 2"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "Eco Pro 3"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "Eco Pro 4"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "5"
        dr("Name") = "Eco Pro 5"
        dt.Rows.Add(dr)


        cboECOPro.DataSource = dt
        cboECOPro.ValueMember = "Code"
        cboECOPro.DisplayMember = "Name"

    End Sub

    Public Sub GetLevel()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Level 1"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "Level 2"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "Level 3"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "Level 4"
        dt.Rows.Add(dr)


        ddlLevel.DataSource = dt
        ddlLevel.ValueMember = "Code"
        ddlLevel.DisplayMember = "Name"

    End Sub

    Sub Reset()
        txtTollAmount.Text = 0
        fndMCCCode.Enabled = True
        txtCollectionOfMilk.Text = ""
        txtDeliveryChallanNo.Text = ""
        isLoad = False
        fndMCCCode.Value = clsGateEntry.getUsersDefaultLocation()
        txtMCCName.Text = clsLocation.GetName(clsGateEntry.getUsersDefaultLocation(), Nothing)
        lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)
        txtTotalAmount.TextAlign = HorizontalAlignment.Right
        GetECOPro()
        dtpDateAndTime.Value = clsCommon.GETSERVERDATE()
        fndChalanNo.Value = ""
        fndChalanNo.MyReadOnly = False
        ddlTankerDispatchTo.SelectedIndex = 1
        fndPlantOrMCCCode.Value = ""
        txtPlantOrMccName.Text = ""
        fndTnakerNo.Value = ""
        txtDripMarking.Text = ""
        ddlTankerFull.Text = ""
        txtControlSampleFAT.Text = ""
        txtControlSampleSNF.Text = ""
        Dim isControlSampleMandatory As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ControlSampleMandatory, clsFixedParameterCode.MilkSetting, Nothing)) = 1, True, False)
        ddlControlSample.Text = "NO"
        If isControlSampleMandatory Then
            ddlControlSample.Enabled = False
        Else
            ddlControlSample.Enabled = True
        End If
        If clsCommon.CompairString(ddlControlSample.Text, "YES") = CompairStringResult.Equal Then
            txtControlSampleFAT.Enabled = True
            txtControlSampleSNF.Enabled = True
        Else
            txtControlSampleFAT.Enabled = True
            txtControlSampleSNF.Enabled = True
        End If
        txtNameOfCustodian.Text = "None"
        LoadBlankGrid()
        loadBlankGridManualSeal()
        loadBlankGridPaperSeal()
        txtTankerKMReading.Text = 0
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnReverse.Enabled = False
        BtnResetProv.Enabled = False
        btnPrint.Enabled = False
        btnSave.Enabled = True
        btnClose.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        fndItemCode.Value = ""
        objSr.SetPortNameValues(cboComPort)
        cboECOPro.SelectedValue = 0 'Nothing
        clsPortSetting.GetMachineType(CboMachine)
        fndItemCode.Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing))
        Dim isItemEditable As Boolean = IIf(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.IsItemEditableOnMCCDispatch, Nothing) = "1", True, False)
        isdipmarkingmendatory = IIf(clsFixedParameter.GetData(clsFixedParameterType.DipMarkingMendatory, clsFixedParameterCode.DipMarkingMendatory, Nothing) = "1", True, False) 'added by stuti on 24/11/2016
        fndItemCode.Enabled = isItemEditable
        lblWeighmentNo.Text = ""
        txtTankerTransporterName.Text = ""
        txtTankerTransporterName.ReadOnly = True
        txtPaymentType.Text = ""
        txtPaymentType.ReadOnly = True
        txtPaymentRate.Text = ""
        txtPaymentRate.ReadOnly = True
        txtChargesFor.Text = ""
        txtChargesFor.ReadOnly = True
        txtTotalAmount.Text = ""
        txtTotalAmount.ReadOnly = True
        txtDripMarking.MendatroryField = isdipmarkingmendatory
        fndChemistCode.Value = ""
        txtChemistName.Text = ""
        txtChemistName.ReadOnly = True
        fndChemistCode.MyReadOnly = False
        txtChemistName.Enabled = True
        fndChemistCode.Enabled = True
        fndUOM.Value = ""
        fndUOM.Enabled = True
        fndUOM.MyReadOnly = False
        txtUOMdesc.Text = ""
        txtUOMdesc.ReadOnly = True
        txtUOMdesc.Enabled = True
        chkReject.Checked = False
        chkReject.Enabled = True
        fndUOM.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItemCode.Value & "' and Default_UOM='1' "))
        If clsCommon.myLen(fndUOM.Value) <= 0 Then
            fndUOM.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItemCode.Value & "' and Stocking_Unit='Y' "))
            txtUOMdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Description   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItemCode.Value & "' and Stocking_Unit='Y' "))
        End If
        txtUOMdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Unit_Desc  FROM TSPL_UNIT_MASTER  WHERE Unit_Code='" & fndUOM.Value & "'"))
        Dim isUOMEditable As Boolean = IIf(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.IsUOMSelectableOnMCCDispatch, Nothing) = "1", True, False)
        fndUOM.Enabled = isUOMEditable
        FindAndRestoreGridLayout(Me)
        FindAndSetTabStopFalse(Me)
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        chkIntermittent.Checked = False
        GroupBox2.Enabled = False
        GetLevel()
        ddlLevel.SelectedIndex = 0
        fndFinalLoc.Value = ""
        txtFinalLoc.Text = ""
        chkIntermittent.Enabled = True
        fndLevel1ChallanNo.Value = ""
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
            UcCustomFields1.SetDefaultValues()
        End If

        txtEWayBillNo.Text = ""
        txtEWayBillDate.Value = dtpDateAndTime.Value
        txtEWayBillDate.Checked = False
        txtElectronicRefNo.Text = ""
        lblClosingDate.Text = ""
        '===================Added by preeti Gupta===============
        txtOpeningKM.Text = 0
        txtClosingKM.Text = 0
        fndPriceChart.Value = ""
        TxtFatWeightage.Text = ""
        TxtSNFWeightage.Text = ""
        txtfatPercentage.Text = ""
        txtSNFPercentage.Text = ""
        txtTransferPrice.Text = 0
        txtTSPrice.Text = ""
        txtTSTransferPrice.Value = 0
        txtTSRate.Value = 0
        txtNoOfCans.Value = 0
        txtGateOut.Value = ""
        lblToPlantName.Text = ""
        LoadBlankGridSilo()
        If ApplyManualTransferRateOnMultipleChamberTankerDispatch = 1 Then
            calculateAmount()
        ElseIf ApplyTotalSolidPriceChart Then
            calculateAmountOnTotalSolids()
        End If
        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, Nothing)) = 0, False, True) = False Then
            btnUpdateAfterPost.Enabled = False
            btnClKM.Enabled = False
        Else
            btnUpdateAfterPost.Enabled = True
            btnClKM.Enabled = True
        End If
        '==========================================================
    End Sub

    Function chkInterMittentValidation() As Boolean
        Try
            If chkIntermittent.Checked = True Then
                If clsCommon.myLen(ddlLevel.SelectedValue) <= 0 Then
                    Throw New Exception("Please select a Level")
                End If

                If clsCommon.myLen(fndFinalLoc.Value) <= 0 Then
                    fndFinalLoc.Focus()
                    Throw New Exception("Please Fill Final Location")
                End If


                If clsCommon.myLen(fndLevel1ChallanNo.Value) <= 0 AndAlso clsCommon.myCdbl(ddlLevel.SelectedValue) > 1 Then
                    Throw New Exception("Please select Level 1 Challan No")
                End If
                If clsCommon.CompairString(fndPlantOrMCCCode.Value, fndFinalLoc.Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlLevel.Text, "Level 1") = CompairStringResult.Equal Then
                    Throw New Exception("In Intermittemt Dispatch Final Location and Current Location can not be same at Level 1")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Function isReversed(ByVal strChallanNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Integer = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(isReversed,0) as isReversed from TSPL_MCC_Dispatch_Challan where chalan_no='" & strChallanNo & "' ", trans))
        If rValue = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Function getCurrentLoggedInMccName() As String
        Dim qry As String = "  select mcc_name from tspl_mcc_master left outer join tspl_user_master on tspl_user_master.Default_Location=tspl_mcc_master.mcc_code where tspl_user_master.user_code='" & objCommonVar.CurrentUserCode & "' "
        Dim str As String = clsDBFuncationality.getSingleValue(qry)
        If clsCommon.myLen(str) <= 0 Then
            str = ""
        End If
        Return str
    End Function

    Function getCurrentLoggedInMccName(ByVal trans As SqlTransaction) As String
        Dim qry As String = "  select mcc_name from tspl_mcc_master left outer join tspl_user_master on tspl_user_master.Default_Location=tspl_mcc_master.mcc_code where tspl_user_master.user_code='" & objCommonVar.CurrentUserCode & "' "
        Dim str As String = clsDBFuncationality.getSingleValue(qry, trans)
        If clsCommon.myLen(str) <= 0 Then
            str = ""
        End If
        Return str
    End Function

    Function getCurrentLoggedInMccCode() As String
        Dim qry As String = "  select mcc_code from tspl_mcc_master left outer join tspl_user_master on tspl_user_master.Default_Location=tspl_mcc_master.mcc_code where tspl_user_master.user_code='" & objCommonVar.CurrentUserCode & "' "
        Dim str As String = clsDBFuncationality.getSingleValue(qry)
        If clsCommon.myLen(str) <= 0 Then
            str = ""
        End If
        Return str
    End Function

    Function FillYesNoValue() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

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

        If AllowCanInformationintoGridForTankerDispatch Then
            Dim repoisCanType As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoisCanType.HeaderText = "Is Can"
            repoisCanType.Name = colIsCanType
            repoisCanType.ReadOnly = True
            repoisCanType.IsVisible = True
            repoisCanType.Tag = colIsCanType
            repoisCanType.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv.MasterTemplate.Columns.Add(repoisCanType)
        End If

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
                repoDecimalColumn.FormatString = "{0:n2}"
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
                repoDecimalColumn.FormatString = "{0:n2}"
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
        gv.ShowGroupPanel = False
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
                lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select ' Tanker Dispatch To  ' type First ", Me.Text)
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
                " and not exists(select 1 from TSPL_MCC_DISPATCH_CHALLAN where TSPL_MCC_DISPATCH_CHALLAN.MCC_Weighment_No=TSPL_MCC_WEIGHMENT.Document_No and TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO not in ('" + fndChalanNo.Value + "')) )xx  "
                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("MCCDFTano", qry)
                If dr IsNot Nothing AndAlso dr.ItemArray.Count > 0 Then
                    fndTnakerNo.Value = clsCommon.myCstr(dr("Tanker_No"))
                    txtTankerTransporterName.Text = clsCommon.myCstr(dr("Transporter_Name"))
                    lblWeighmentNo.Text = clsCommon.myCstr(dr("Document_No"))
                End If
            End If
        ElseIf Not isLoadingBulkSaleTanker Then
            Dim qry As String = " select distinct TankerNo  ,[Tanker Transporter Code],[Tanker Transporter Name]  from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where 1=1 "
            If ShowTankerWithoutCheckingAnyValidation = False Then
                qry += " and isGateOut=1 "
            End If
            qry += " union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='" & fndTnakerNo.Value & "'  AND isGateOut=1 AND ISNULL(Ref_Doc_No,'')='' " &
               "union all select TSPL_MCC_Dispatch_Challan.Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master LEFT OUTER JOIN TSPL_MCC_Dispatch_Challan ON  tspl_tanker_master.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No where TSPL_MCC_Dispatch_Challan.Chalan_NO='" & fndChalanNo.Value & "' )xx  "
            Dim whrCls As String = "  "
            If chkShowIntermittentOnly.Checked = True Then
                qry = "  select distinct TankerNo  ,[Tanker Transporter Code],[Tanker Transporter Name]  from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where 1=1 "
                If ShowTankerWithoutCheckingAnyValidation = False Then
                    qry += " and isGateOut=1 "
                End If
                qry += " and Tanker_No in (select Tanker_No  from TSPL_MCC_Dispatch_Challan where  isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1)  " &
                "union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='" & fndTnakerNo.Value & "'  AND isGateOut=1 AND ISNULL(Ref_Doc_No,'')='' " &
                "union all select TSPL_MCC_Dispatch_Challan.Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master LEFT OUTER JOIN TSPL_MCC_Dispatch_Challan ON  tspl_tanker_master.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No where TSPL_MCC_Dispatch_Challan.Chalan_NO='" & fndChalanNo.Value & "' )xx  "
            End If
            fndTnakerNo.Value = clsCommon.ShowSelectForm("TNKRNO", qry, "TankerNo", whrCls, fndTnakerNo.Value, "TankerNo", isButtonClicked)
            txtTankerTransporterName.Text = getTransporterName(fndTnakerNo.Value)
            SetIntermittentDetails(fndTnakerNo.Value)
        End If
        SetTankerDetail()

        FillOldData()
        If AllowCanInformationintoGridForTankerDispatch Then
            SetCanDetail()
        End If

    End Sub

    Sub FillOldData()
        If chkIntermittent.Checked And settTankerDispatchIntermittentSingleGateIn Then
            Try
                Dim qry As String = "  select Chalan_NO  from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" + fndTnakerNo.Value + "' and CurrentLevel='" + clsCommon.myCstr(clsCommon.myCdbl(ddlLevel.SelectedValue) - 1) + "'"
                If clsCommon.myLen(txtGateOut.Value) > 0 Then
                    qry += " and TSPL_MCC_Dispatch_Challan.Against_Gate_Out='" & txtGateOut.Value & "' "
                End If
                qry += "order by Dispatch_Date asc "

                qry = clsDBFuncationality.getSingleValue(qry)
                Dim obj As clsMccDispatch = clsMccDispatch.getData(qry, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Chalan_NO) > 0 Then
                    For i As Integer = 0 To obj.arrParmValue.Count - 1
                        Try
                            gv.Rows(obj.arrParmValue(i).SNO - 1).Cells(obj.arrParmValue(i).Param_Field_Code).Value = obj.arrParmValue(i).Param_Field_Value
                        Catch ex1 As Exception
                        End Try
                    Next
                    If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                        For Each objtr As clsMCCDispatchDetail In obj.arr
                            If objtr.Qty_KG > 0 Then
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
                                If clsCommon.myLen(objtr.Intermittent_Dispatch_No) > 0 Then
                                    gv.Rows(objtr.SNo - 1).Cells(ColIntermittentDispatchNo).Value = objtr.Intermittent_Dispatch_No
                                Else
                                    gv.Rows(objtr.SNo - 1).Cells(ColIntermittentDispatchNo).Value = obj.Chalan_NO
                                End If
                            End If
                        Next
                    End If
                End If

            Catch ex As Exception

            End Try

        End If
    End Sub

    Sub SetTankerDetail()
        If settFirstGateOutProcessForBulkProcument Then
            If Not isLoadingBulkSaleTanker Then
                txtTankerTransporterName.Text = getTransporterName(fndTnakerNo.Value)
                SetIntermittentDetails(fndTnakerNo.Value)
            End If
        End If
        LoadBlankGrid()
        If clsCommon.myLen(fndTnakerNo.Value) > 0 Then
            chkReject.Enabled = False
            Dim ArrTankerChamber As List(Of clsTankerChamberDetail) = clsTankerChamberDetail.GetData(fndTnakerNo.Value)
            If ArrTankerChamber IsNot Nothing AndAlso ArrTankerChamber.Count > 0 Then
                Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(settTankerDispatchAvgFATSNFPer, True, chkReject.Checked, False, "", "MI", fndItemCode.Value, fndMCCCode.Value, 1, fndUOM.Value, 1, 1, dtpDateAndTime.Value, dtpDateAndTime.Value, False, Nothing, fndChalanNo.Value)
                For Each objtr As clsTankerChamberDetail In ArrTankerChamber
                    gv.Rows.AddNew()
                    gv.Rows(gv.RowCount - 1).Cells(colChamberNo).Value = objtr.Chamber_No
                    gv.Rows(gv.RowCount - 1).Cells(colChamberDesc).Value = objtr.Chamber_Description
                    gv.Rows(gv.RowCount - 1).Cells(colICode).Value = fndItemCode.Value
                    gv.Rows(gv.RowCount - 1).Cells(colIName).Value = clsItemMaster.GetItemName(fndItemCode.Value, Nothing)
                    gv.Rows(gv.RowCount - 1).Cells(colUOM).Value = fndUOM.Value
                    '=====Added by preeti [16/04/2018]======[]
                    If ApplyManualTransferRateOnMultipleChamberTankerDispatch = 1 OrElse ApplyTotalSolidPriceChart Then
                        gv.Rows(gv.RowCount - 1).Cells(colFatRate).Value = 0
                        gv.Rows(gv.RowCount - 1).Cells(colSNFRate).Value = 0
                    Else
                        If objMCT IsNot Nothing Then
                            gv.Rows(gv.RowCount - 1).Cells(colFatRate).Value = objMCT.FAT_Cost
                            gv.Rows(gv.RowCount - 1).Cells(colSNFRate).Value = objMCT.SNF_Cost
                        End If
                    End If
                    '=======================
                    If settTankerDispatchAvgFATSNFPer Then
                        gv.Rows(gv.RowCount - 1).Cells(FATColName).Value = objMCT.FAT_Per
                        gv.Rows(gv.RowCount - 1).Cells(SNFColName).Value = objMCT.SNF_Per
                    End If

                    If clsCommon.myLen(CfColName) > 0 Then
                        Try
                            gv.Rows(gv.RowCount - 1).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
                            If clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
                                gv.Columns(CfColName).IsVisible = False
                            End If
                        Catch exx As Exception
                        End Try
                    End If
                    If clsCommon.myLen(CLRColName) > 0 Then
                        Try
                            If clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
                                gv.Columns(CLRColName).IsVisible = False
                            End If
                        Catch exx As Exception
                        End Try
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow("Please provide chamber of vehicle no" + fndTnakerNo.Value)
            End If
        End If
    End Sub

    Sub SetCanDetail()
        If clsCommon.myLen(fndTnakerNo.Value) > 0 AndAlso AllowCanInformationintoGridForTankerDispatch = True Then
            Dim ArrTankerChamber As List(Of clsTankerChamberDetail) = clsTankerChamberDetail.GetData(fndTnakerNo.Value)
            If ArrTankerChamber IsNot Nothing AndAlso ArrTankerChamber.Count > 0 Then
                If gv.Rows.Count > ArrTankerChamber.Count Then
                    For i As Integer = ArrTankerChamber.Count To gv.Rows.Count - 1
                        gv.Rows.RemoveAt(gv.Rows.Count - 1)
                    Next
                End If
            End If
            If clsCommon.myCdbl(txtNoOfCans.Value) > 0 Then
                gv.Rows.AddNew()
                gv.Rows(gv.RowCount - 1).Cells(colIsCanType).Value = True
            End If

        End If
    End Sub

    Sub SetIntermittentDetails(ByVal strTankerNo As String)
        If Not isLoad Then
            Dim qry As String = ""
            Dim isInterMittent As Boolean = False
            Dim nextLevel As Double = 0
            Dim FinalLoc As String = ""
            Dim Level1ChallanNo As String = ""
            Dim qry2 As String = ""
            qry = " select count(*) from  ( select distinct TankerNo    from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where 1=1 "
            If ShowTankerWithoutCheckingAnyValidation = False Then
                qry += " and isGateOut=1 "
            End If
            qry += " and Tanker_No in (select Tanker_No  from TSPL_MCC_Dispatch_Challan where  isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1) union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='')xx  ) yyyy where yyyy.TankerNo='" & strTankerNo & "' "
            Dim dtGateOut As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO ='" & clsCommon.myCstr(txtGateOut.Value) & "'")
            'qry2 = "  select MAX(ISNULL(currentlevel,0))+1 from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and TSPL_MCC_Dispatch_Challan.Against_Gate_Out='" & txtGateOut.Value & "'"
            If settFirstGateOutProcessForBulkProcument = True AndAlso clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0 AndAlso clsCommon.myLen(fndChalanNo.Value) = 0 AndAlso clsCommon.myLen(txtGateOut.Value) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dtGateOut.Rows(0)("MCC_CODE2"))) > 0 Then
                isInterMittent = True
                GroupBox2.Enabled = False
                chkIntermittent.Enabled = False
                chkIntermittent.Checked = True
                'orelse in below line- After one level entry,second level entry not done and same tanker again alllocate
            ElseIf (clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0) OrElse (clsCommon.myLen(txtGateOut.Value) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dtGateOut.Rows(0)("MCC_CODE2"))) <= 0) Then
                isInterMittent = False
                GroupBox2.Enabled = False
                chkIntermittent.Enabled = True
                chkIntermittent.Checked = False
            Else
                isInterMittent = True
                GroupBox2.Enabled = False
                chkIntermittent.Enabled = False
                chkIntermittent.Checked = True
            End If

            If isInterMittent = True Then
                If clsCommon.myLen(txtGateOut.Value) > 0 Then
                    qry = "  select isnull(MAX(ISNULL(currentlevel,0)),0)+1 from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and TSPL_MCC_Dispatch_Challan.Against_Gate_Out='" & txtGateOut.Value & "' "
                Else
                    qry = "  select MAX(ISNULL(currentlevel,0))+1 from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' "
                End If

                nextLevel = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                ddlLevel.SelectedValue = CInt(nextLevel).ToString()
                qry = "  select finalLoc from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1' order by Dispatch_Date asc " ' order by Dispatch_Date desc
                FinalLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                fndFinalLoc.Value = FinalLoc
                txtFinalLoc.Text = clsLocation.GetName(FinalLoc, Nothing)

                qry = "  select Chalan_NO  from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1' "
                If clsCommon.myLen(txtGateOut.Value) > 0 Then
                    qry += " and TSPL_MCC_Dispatch_Challan.Against_Gate_Out='" & txtGateOut.Value & "' "
                End If
                qry += "order by Dispatch_Date asc "
                Level1ChallanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                fndLevel1ChallanNo.Value = Level1ChallanNo

                '''''''''''''''
                If settFirstGateOutProcessForBulkProcument = True Then
                    Dim dtGateOutDeatil As DataTable = Nothing
                    If ddlLevel.SelectedValue = 1 And clsCommon.myLen(txtGateOut.Value) > 0 Then
                        fndFinalLoc.Value = clsCommon.myCstr(dtGateOut.Rows(0)("LOCATION_CODE"))
                        txtFinalLoc.Text = clsLocation.GetName(FinalLoc, Nothing)
                        dtGateOutDeatil = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO ='" & clsCommon.myCstr(txtGateOut.Value) & "'")
                    Else
                        dtGateOutDeatil = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO = (select max(isnull(Against_Gate_Out,'')) from TSPL_MCC_Dispatch_Challan where level1ChallaNNo ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "')")
                    End If

                    If ddlLevel.SelectedValue = 1 AndAlso clsCommon.myLen(fndChalanNo.Value) = 0 AndAlso clsCommon.myLen(txtGateOut.Value) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE2"))) > 0 Then
                        ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr("MCC")
                        fndPlantOrMCCCode.Value = clsCommon.myCstr(dtGateOut.Rows(0)("MCC_CODE2"))
                        txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                        lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
                    ElseIf ddlLevel.SelectedValue = 2 AndAlso clsCommon.myLen(fndChalanNo.Value) = 0 AndAlso clsCommon.myLen(txtGateOut.Value) = 0 Then
                        If dtGateOutDeatil IsNot Nothing AndAlso dtGateOutDeatil.Rows.Count > 0 Then
                            If clsCommon.myLen(clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE3"))) > 0 Then
                                fndMCCCode.Value = clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE2"))
                                txtMCCName.Text = clsLocation.GetName(fndMCCCode.Value, Nothing)
                                lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)

                                ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr("MCC")
                                fndPlantOrMCCCode.Value = clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE3"))
                                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                                lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
                            ElseIf clsCommon.myLen(clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE2"))) > 0 Then
                                fndMCCCode.Value = clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE2"))
                                txtMCCName.Text = clsLocation.GetName(fndMCCCode.Value, Nothing)
                                lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)

                                ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr("Plant")
                                fndPlantOrMCCCode.Value = clsCommon.myCstr(dtGateOutDeatil.Rows(0)("LOCATION_CODE"))
                                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                                lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
                            End If
                        End If
                    ElseIf ddlLevel.SelectedValue = 3 AndAlso clsCommon.myLen(fndChalanNo.Value) = 0 AndAlso clsCommon.myLen(txtGateOut.Value) = 0 Then
                        If dtGateOutDeatil IsNot Nothing AndAlso dtGateOutDeatil.Rows.Count > 0 Then
                            If clsCommon.myLen(clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE3"))) > 0 Then
                                fndMCCCode.Value = clsCommon.myCstr(dtGateOutDeatil.Rows(0)("MCC_CODE3"))
                                txtMCCName.Text = clsLocation.GetName(fndMCCCode.Value, Nothing)
                                lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)

                                ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr("Plant")
                                fndPlantOrMCCCode.Value = clsCommon.myCstr(dtGateOutDeatil.Rows(0)("LOCATION_CODE"))
                                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                                lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FrmMccDispatch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If clsCommon.CompairString(BtnStart.Text, "Stop") = CompairStringResult.Equal Then
            e.Cancel = True
            clsCommon.MyMessageBoxShow(Me, "Please stop the port before application close", Me.Text)
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
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "sirc"
            frm.strCode = "sireversandcreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                BtnResetProv.Visible = True
            End If
        ElseIf e.KeyCode = Keys.F8 Then
            If gv.Rows.Count > 0 Then
                gv.CurrentRow.Cells(colAutoFat).Value = LblSnf.Text
                gv.CurrentRow.Cells(colAutoSnf).Value = LblFAT.Text
                Try
                    If clsCommon.myLen(CfColName) > 0 Then
                        gv.CurrentRow.Cells(colAutoClr).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(LblFAT.Text), clsCommon.myCdbl(LblSnf.Text), clsCommon.myCdbl(gv.CurrentRow.Cells(CfColName).Value))
                    Else
                        gv.CurrentRow.Cells(colAutoClr).Value = "0"
                    End If
                Catch exx As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMCCDispatch)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
            ' Me.Close()
            'ElseIf clsCommon.CompairString(getCurrentLoggedInMccName(), "NA") = CompairStringResult.Equal Then
            '   Throw New Exception("Current User is not of type MCC, Contact to administrator and Map current User Default Location to any MCC")
            'Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then

            btnReverse.Enabled = True
            BtnResetProv.Enabled = True
        Else
            btnReverse.Enabled = False
            BtnResetProv.Enabled = False
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Function AllowToSave() As Boolean
        Try
            If settTankerDispatchAvgFATSNFPer Then
                Dim arr As New List(Of clsMCCDispatchDetail)
                For ii As Integer = 0 To gv.Rows.Count - 1
                    If clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) > 0 Then
                        If settTankerDispatchIntermittentSingleGateIn Then
                            If clsCommon.myLen(gv.Rows(ii).Cells(ColIntermittentDispatchNo).Value) > 0 AndAlso chkIntermittent.Checked Then
                                Continue For
                            End If
                        End If

                        Dim objtr As New clsMCCDispatchDetail
                        If AllowCanInformationintoGridForTankerDispatch = True Then
                            If clsCommon.myCBool(gv.Rows(ii).Cells(colIsCanType).Value) = True Then
                                objtr.isCanType = 1
                            Else
                                objtr.Chamber_No = clsCommon.myCstr(gv.Rows(ii).Cells(colChamberNo).Value)
                                objtr.isCanType = 0
                            End If
                        Else
                            objtr.Chamber_No = clsCommon.myCstr(gv.Rows(ii).Cells(colChamberNo).Value)
                            objtr.isCanType = 0
                        End If


                        objtr.Item_Code = clsCommon.myCstr(gv.Rows(ii).Cells(colICode).Value)
                        objtr.Item_UOM = clsCommon.myCstr(gv.Rows(ii).Cells(colUOM).Value)
                        objtr.Qty_KG = clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value)
                        arr.Add(objtr)
                    End If
                Next
                Dim dtSummary As New DataTable

                Dim ArrSilo As List(Of clsMCCDispatchSiloWise) = clsMCCDispatchSiloWise.InventoryMovementAvgFATSNF(fndChalanNo.Value, dtpDateAndTime.Value, arr, fndMCCCode.Value, fndPlantOrMCCCode.Value, chkReject.Checked, Nothing, Nothing, dtSummary, False)
                If ArrSilo IsNot Nothing AndAlso ArrSilo.Count > 0 Then
                    LoadDataSilo(ArrSilo)
                    For Each drSumm As DataRow In dtSummary.Rows
                        For rowNo As Integer = 0 To gv.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(rowNo).Cells(colChamberNo).Value), clsCommon.myCstr(drSumm("Chamber_No"))) = CompairStringResult.Equal Then
                                gv.Rows(rowNo).Cells(FATColName).Value = Math.Round(clsCommon.myCdbl(drSumm("Fat_Per")), 2, MidpointRounding.ToEven)
                                gv.Rows(rowNo).Cells(colFatRate).Value = Math.Round(clsCommon.myCdbl(drSumm("Fat_Rate")), 2, MidpointRounding.ToEven)
                                gv.Rows(rowNo).Cells(colFatKG).Value = Math.Round(clsCommon.myCdbl(drSumm("Fat_KG")), 3, MidpointRounding.ToEven)
                                gv.Rows(rowNo).Cells(SNFColName).Value = Math.Round(clsCommon.myCdbl(drSumm("SNF_Per")), 2, MidpointRounding.ToEven)
                                gv.Rows(rowNo).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(drSumm("SNF_Rate")), 2, MidpointRounding.ToEven)
                                gv.Rows(rowNo).Cells(colSNFKG).Value = Math.Round(clsCommon.myCdbl(drSumm("SNF_KG")), 3, MidpointRounding.ToEven)
                                gv.Rows(rowNo).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(drSumm("Amount")), 3, MidpointRounding.ToEven)

                            End If
                        Next
                    Next
                    If AllowCanInformationintoGridForTankerDispatch Then
                        For Each drSumm As DataRow In dtSummary.Rows
                            If clsCommon.CompairString(clsCommon.myCstr(drSumm("Chamber_No")), "0") = CompairStringResult.Equal Then
                                For rowNo As Integer = 0 To gv.Rows.Count - 1
                                    If clsCommon.myCBool(gv.Rows(rowNo).Cells(colIsCanType).Value) = True Then
                                        gv.Rows(rowNo).Cells(FATColName).Value = Math.Round(clsCommon.myCdbl(drSumm("Fat_Per")), 2, MidpointRounding.ToEven)
                                        gv.Rows(rowNo).Cells(colFatRate).Value = Math.Round(clsCommon.myCdbl(drSumm("Fat_Rate")), 2, MidpointRounding.ToEven)
                                        gv.Rows(rowNo).Cells(colFatKG).Value = Math.Round(clsCommon.myCdbl(drSumm("Fat_KG")), 3, MidpointRounding.ToEven)
                                        gv.Rows(rowNo).Cells(SNFColName).Value = Math.Round(clsCommon.myCdbl(drSumm("SNF_Per")), 2, MidpointRounding.ToEven)
                                        gv.Rows(rowNo).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(drSumm("SNF_Rate")), 2, MidpointRounding.ToEven)
                                        gv.Rows(rowNo).Cells(colSNFKG).Value = Math.Round(clsCommon.myCdbl(drSumm("SNF_KG")), 3, MidpointRounding.ToEven)
                                        gv.Rows(rowNo).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(drSumm("Amount")), 3, MidpointRounding.ToEven)
                                    End If

                                Next
                            End If

                        Next
                    End If

                End If
            Else
                Calculate()
            End If


            If AllowFutureDateTransaction(dtpDateAndTime.Value, Nothing) = False Then
                dtpDateAndTime.Focus()
                Return False
            End If

            If ShowTankerWithoutCheckingAnyValidation = False Then
                If clsMccDispatch.CheckTankerGateOut(fndTnakerNo.Value, fndChalanNo.Value) = False Then
                    Throw New Exception("Tanker No- " & fndTnakerNo.Value & " is not available for new dispatch.")
                End If
            End If

            If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                Throw New Exception("Please select MCC/Plant from which dispatch is being made, first")
                errorControl.SetError(fndMCCCode, "Please select MCC/Plant from which dispatch is being made, first ")
            Else
                errorControl.SetError(fndMCCCode, "")
            End If
            If clsCommon.myLen(ddlTankerDispatchTo.Text) = 0 Then
                errorControl.SetError(ddlTankerDispatchTo, "Please Select Tanker Dispatch To Either PLANT/MCC ")
                Throw New Exception("Please Select Tanker Dispatch To Either PLANT/MCC")
            Else
                errorControl.SetError(ddlTankerDispatchTo, "")
            End If

            If clsCommon.myLen(fndPlantOrMCCCode.Value) = 0 Then
                fndPlantOrMCCCode.Focus()
                errorControl.SetError(fndPlantOrMCCCode, "Please Select Dispatch To Which Plant/MCC  ")
                Throw New Exception("Please Select Dispatch To Which Plant/MCC ")
            Else
                errorControl.SetError(fndPlantOrMCCCode, "")
            End If

            If clsCommon.myLen(fndTnakerNo.Value) = 0 Then
                fndTnakerNo.Focus()
                errorControl.SetError(fndTnakerNo, "Please Select Tanker No")
                Throw New Exception("Please Select Tanker No ")
            Else
                errorControl.SetError(fndTnakerNo, "")
            End If
            If clsCommon.myLen(txtNameOfCustodian.Text) = 0 Then
                errorControl.SetError(txtNameOfCustodian, "Please enter name of custodian or None")
                Throw New Exception("Please enter name of custodian or None ")
            Else
                errorControl.SetError(txtNameOfCustodian, "")
            End If
            Dim isControlSample As Boolean = False
            If clsCommon.CompairString(ddlControlSample.Text, "YES") = CompairStringResult.Equal Then
                isControlSample = True
            Else
                isControlSample = False
            End If

            If isControlSample Then
                If clsCommon.myCdbl(txtControlSampleFAT.Text) < 0 Then
                    Throw New Exception("Control Sample FAT must not be negative")
                    txtControlSampleFAT.Focus()
                End If
                If clsCommon.myCdbl(txtControlSampleFAT.Text) = 0 Then
                    Throw New Exception("Control Sample FAT must not be blank or zero")
                    txtControlSampleFAT.Focus()
                End If

                If clsCommon.myCdbl(txtControlSampleSNF.Text) < 0 Then
                    Throw New Exception("Control Sample SNF must not be negative")
                    txtControlSampleSNF.Focus()
                End If
                If clsCommon.myCdbl(txtControlSampleSNF.Text) = 0 Then
                    Throw New Exception("Control Sample SNF must not be blank or zero")
                    txtControlSampleSNF.Focus()
                End If

                Dim fracValue As Double = 0

                fracValue = clsCommon.myCdbl(txtControlSampleFAT.Text)
                fracValue = Math.Round((fracValue - CInt(fracValue)) * 100, 2)
                If CInt(fracValue) Mod 5 <> 0 Then
                    Throw New Exception("Control Sample FAT value, must have its decimal part multiple of 5")
                    txtControlSampleFAT.Focus()
                End If
            End If
            Dim fracValue1 As Double = 0
            If settTankerDispatchAvgFATSNFPer Then
            ElseIf ApplyManualTransferRateOnMultipleChamberTankerDispatch = 1 Then
                calculateAmount()
            ElseIf ApplyTotalSolidPriceChart Then
                calculateAmountOnTotalSolids()
                If clsCommon.myLen(txtTSPrice.Text) <= 0 Then
                    Throw New Exception("Please set tanker dispatch price for mcc " + fndMCCCode.Value)
                End If
            End If

            For ii As Integer = 0 To gv.RowCount - 1
                If clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) > 0 Then
                    If clsCommon.myLen(FATColName) > 0 Then
                        fracValue1 = clsCommon.myCdbl(gv.Rows(ii).Cells(FATColName).Value)
                        fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                        If CInt(fracValue1) Mod 5 <> 0 Then

                            If Not settTankerDispatchAvgFATSNFPer AndAlso settFATPerShouldBeMultipleOf5 Then
                                Throw New Exception(" FAT value in Grid, must have its decimal part multiple of 5 at row no " + clsCommon.myCstr(ii))
                            End If
                        End If
                    End If

                    If clsCommon.myCdbl(gv.Rows(ii).Cells(colFatRate).Value) <= 0 Then
                        Throw New Exception("FAT rate is 0 in Grid, Please reselect the price chart or Re-enter FAT % in Grid at row no " + clsCommon.myCstr(ii))
                    End If
                    If clsCommon.myCdbl(gv.Rows(ii).Cells(colSNFRate).Value) <= 0 Then
                        Throw New Exception("SNF rate is 0 in Grid, Please reselect the price chart or Re-enter SNF/CLR % in Grid at row no " + clsCommon.myCstr(ii))
                    End If

                    If clsCommon.myCdbl(gv.Rows(ii).Cells(colAmount).Value) <= 0 Then
                        Throw New Exception("Amount is 0 in grid, Please check Price chart, FAT, SNF/CLR in Grid or Re-Enter it at row no " + clsCommon.myCstr(ii))
                    End If
                End If
            Next


            'Asked By Ranjana Mam(Vijaya)
            'If clsCommon.myCdbl(txtTankerKMReading.Text) <= 0 Then
            '    txtTankerKMReading.Focus()
            '    errorControl.SetError(txtTankerKMReading, "Please Enter Tanker KM Reading")
            '    Throw New Exception("Please Enter Tanker KM Reading")
            'Else
            '    errorControl.SetError(txtTankerKMReading, "")
            'End If
            If isdipmarkingmendatory Then
                If clsCommon.myLen(txtDripMarking.Text) = 0 Then
                    txtDripMarking.Focus()
                    errorControl.SetError(txtDripMarking, "Please Enter Dip Marking")
                    Throw New Exception("Please Enter Dip Marking")
                Else
                    errorControl.SetError(txtDripMarking, "")
                End If
            End If
            If clsCommon.myLen(fndItemCode.Value) = 0 Then
                fndItemCode.Focus()
                errorControl.SetError(fndItemCode, "Please Select an Item. It Is manadatory  ")
                Throw New Exception("Please Select an Item. It Is manadatory  ")
            Else
                errorControl.SetError(fndItemCode, "")
            End If
            For ii As Integer = 0 To gv.RowCount - 1
                ' Ticket No :  BHA/09/08/18-000405  By Prabhakar - Qty should be greater then zero
                'If String.IsNullOrEmpty(clsCommon.myCstr(gv.Rows(ii).Cells(ColQtyInKg).Value)) = True Or clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) <= 0 Then
                'Throw New Exception("Qty of Chember No: " & clsCommon.myCstr(gv.Rows(ii).Cells(colChamberNo).Value) & " is Mandatory and should be greater than 0, please fill it.")
                'End If
                If clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) > 0 Then
                    For i As Integer = 1 To gv.Columns.Count - 1
                        Dim ismandatoryvalue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select IsMandatory from TSPL_PARAMETER_MASTER where code='" + gv.Columns(i).Name + "'"))
                        If AllowCanInformationintoGridForTankerDispatch = True Then
                            If clsCommon.myCBool(gv.Rows(ii).Cells(colIsCanType).Value) = True Then
                                Dim IsCanType As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select IsCanType from TSPL_PARAMETER_MASTER where code='" + gv.Columns(i).Name + "'"))
                                If IsCanType > 0 AndAlso ismandatoryvalue > 0 AndAlso clsCommon.myLen(gv.Rows(ii).Cells(i).Value) <= 0 AndAlso clsCommon.CompairString(gv.Columns(i).Name, CLRColName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(gv.Columns(i).Name, CfColName) <> CompairStringResult.Equal Then
                                    Throw New Exception("" & gv.Columns(i).HeaderText & " is Mandatory, please fill it.")
                                End If
                            Else
                                If ismandatoryvalue > 0 AndAlso clsCommon.myLen(gv.Rows(ii).Cells(i).Value) <= 0 AndAlso clsCommon.CompairString(gv.Columns(i).Name, CLRColName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(gv.Columns(i).Name, CfColName) <> CompairStringResult.Equal Then
                                    Throw New Exception("" & gv.Columns(i).HeaderText & " is Mandatory, please fill it.")
                                End If
                            End If
                        Else
                            If ismandatoryvalue > 0 AndAlso clsCommon.myLen(gv.Rows(ii).Cells(i).Value) <= 0 AndAlso clsCommon.CompairString(gv.Columns(i).Name, CLRColName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(gv.Columns(i).Name, CfColName) <> CompairStringResult.Equal Then
                                Throw New Exception("" & gv.Columns(i).HeaderText & " is Mandatory, please fill it.")
                            End If
                        End If

                    Next
                End If
            Next

            'Sanjay Ticket no- ERO/13/09/18-000397,  If tanker is chamberwise, qty in atleast  one chamber should be mandatory.
            Dim intcount As Integer = 0
            For ii As Integer = 0 To gv.Rows.Count - 1
                Dim dblQty As Double = clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value)
                If dblQty > 0 Then
                    If settTankerDispatchIntermittentSingleGateIn AndAlso chkIntermittent.Checked Then
                        If clsCommon.myLen(gv.Rows(ii).Cells(ColIntermittentDispatchNo).Value) <= 0 Then
                            intcount += 1
                        End If
                    Else
                        intcount += 1
                    End If
                End If
            Next
            If intcount = 0 Then
                Throw New Exception("Please enter atleast one chamber qty. ")
            ElseIf settTankerDispatchIntermittentSingleGateIn AndAlso chkIntermittent.Checked Then
                If intcount > 1 Then
                    Throw New Exception("Please fill qty in only one chammber")
                End If
            ElseIf intcount <> gv.Rows.Count Then
                If clsCommon.MyMessageBoxShow("You have not entered qty for all chamber, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Return False
                End If
            End If
            'Sanjay 

            ''-------------------------------------------
            For i As Integer = 0 To gvManualSeal.Rows.Count - 2
                For j As Integer = i + 1 To gvManualSeal.Rows.Count - 1
                    If clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealNo).Value) > 0 AndAlso clsCommon.myLen(gvManualSeal.Rows(j).Cells(colSealNo).Value) > 0 Then
                        If clsCommon.CompairString(gvManualSeal.Rows(i).Cells(colSealNo).Value, gvManualSeal.Rows(j).Cells(colSealNo).Value) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate Manual Seal No found at Row no " & (j + 1))
                        End If
                    End If
                Next
            Next

            For i As Integer = 0 To gvPaperSeal.Rows.Count - 2
                For j As Integer = i + 1 To gvPaperSeal.Rows.Count - 1
                    If clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSealNo).Value) > 0 AndAlso clsCommon.myLen(gvPaperSeal.Rows(j).Cells(colSealNo).Value) > 0 Then
                        If clsCommon.CompairString(gvPaperSeal.Rows(i).Cells(colSealNo).Value, gvPaperSeal.Rows(j).Cells(colSealNo).Value) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate Paper Seal No found at Row no " & (j + 1))
                        End If
                    End If
                Next
            Next

            If Not settTankerDispatchAvgFATSNFPer Then
                Dim CurBal As Double = 0
                Dim CurBalOFVL As Double = 0
                Dim CurBalOFML As Double = 0

                Dim CurBal_SNF As Double = 0
                Dim CurBalOFVL_SNF As Double = 0
                Dim CurBalOFML_SNF As Double = 0

                Dim CurBal_FAT As Double = 0
                Dim CurBalOFVL_FAT As Double = 0
                Dim CurBalOFML_FAT As Double = 0
                ''richa agarwal 04/06/2018 MIL/04/06/18-000023
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, Nothing), "0") = CompairStringResult.Equal Then
                    Dim CurBalFATKg As Double = 0
                    Dim CurBalFATKgOFVL As Double = 0
                    Dim CurBalFATKgOFML As Double = 0

                    Dim CurBalSNFKg As Double = 0
                    Dim CurBalSNFKgOFVL As Double = 0
                    Dim CurBalSNFKgOFML As Double = 0

                    Dim FATKg As Decimal = 0
                    Dim SNFKg As Decimal = 0

                    ''TO CHECK item qty 
                    Dim arrItem As New List(Of String)
                    For ii As Integer = 0 To gv.RowCount - 1
                        If Not arrItem.Contains(clsCommon.myCstr(gv.Rows(ii).Cells(colICode).Value)) Then
                            arrItem.Add(clsCommon.myCstr(gv.Rows(ii).Cells(colICode).Value))
                        End If
                    Next
                    If arrItem.Count <= 0 Then
                        Throw New Exception("No item found to dispatch")
                    End If
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' order by TSPL_Location_MASTER.Location_Code ")
                    Dim strsublocation As String = String.Empty
                    For Each strIcode As String In arrItem
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                                If clsCommon.myLen(strsublocation) > 0 Then
                                    CurBalOFVL = CurBalOFVL + clsCommon.myCdbl(ClsLoadingTanker.getBalance(strIcode, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG", FATKg, SNFKg))
                                    CurBalFATKgOFVL += FATKg
                                    CurBalSNFKgOFVL += SNFKg
                                End If
                            Next
                        End If
                        CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strIcode, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG", FATKg, SNFKg))
                        CurBal = CurBalOFVL + CurBalOFML
                        CurBalFATKg = CurBalFATKgOFVL + FATKg
                        CurBalSNFKg = CurBalSNFKgOFVL + SNFKg
                        Dim dblEnteredQty As Double = 0
                        Dim dblEnteredFATKg As Double = 0
                        Dim dblEnteredSNFKg As Double = 0
                        For ii As Integer = 0 To gv.RowCount - 1
                            If clsCommon.CompairString(strIcode, clsCommon.myCstr(gv.Rows(ii).Cells(colICode).Value)) = CompairStringResult.Equal Then
                                Dim flag As Boolean = True
                                If settTankerDispatchIntermittentSingleGateIn AndAlso chkIntermittent.Checked Then
                                    If clsCommon.myLen(gv.Rows(ii).Cells(ColIntermittentDispatchNo).Value) > 0 Then
                                        flag = False
                                    Else
                                        flag = True
                                    End If
                                End If
                                If flag Then
                                    dblEnteredQty += clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value)
                                    dblEnteredFATKg += clsCommon.myCdbl(gv.Rows(ii).Cells(colFatKG).Value)
                                    dblEnteredSNFKg += clsCommon.myCdbl(gv.Rows(ii).Cells(colSNFKG).Value)
                                End If
                            End If
                        Next
                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                            If CurBal > 0 Then
                                CurBal = ClsLoadingTanker.GetTolerane(CurBal, dblEnteredQty)
                            End If
                        End If
                        ''richa agarwal 30 Jan,2019 check only when entered qty is greater than 0 ERO/30/01/19-000481
                        If clsCommon.myCdbl(dblEnteredQty) > 0 Then
                            If Math.Round(CurBal, 2) < Math.Round(dblEnteredQty, 2) Then
                                Throw New Exception("Item : " + strIcode + " Available Qty is :     " & Math.Round(CurBal, 2) & Environment.NewLine & "Required Qty :     " & clsCommon.myCstr(dblEnteredQty) & " ")
                            End If
                        End If
                        ''--------------------
                        If Not settAllowtoNegativeFATSNFKgAtTankerDispatch Then
                            If clsCommon.myCdbl(dblEnteredFATKg) > 0 Then
                                If Math.Round(CurBalFATKg, 2) < Math.Round(dblEnteredFATKg, 2) Then
                                    Throw New Exception("Item : " + strIcode + " Available FATKg is :     " & Math.Round(CurBalFATKg, 2) & Environment.NewLine & "Required FATKg:     " & clsCommon.myCstr(dblEnteredFATKg) & " ")
                                End If
                            End If
                            If clsCommon.myCdbl(dblEnteredSNFKg) > 0 Then
                                If Math.Round(CurBalSNFKg, 2) < Math.Round(dblEnteredSNFKg, 2) Then
                                    Throw New Exception("Item : " + strIcode + " Available SNFKg is :     " & Math.Round(CurBalSNFKg, 2) & Environment.NewLine & "Required SNFKg:     " & clsCommon.myCstr(dblEnteredSNFKg) & " ")
                                End If
                            End If
                        End If
                    Next

                    ''End TO CHECK item qty 



                    ''TO CHECK FAT AND SNF
                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing), "1") = CompairStringResult.Equal Then
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                                If clsCommon.myLen(strsublocation) > 0 Then
                                    Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & strsublocation & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
                                    If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                                        CurBalOFVL_SNF = CurBalOFVL_SNF + Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                                        CurBalOFVL_FAT = CurBalOFVL_FAT + Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                                    End If
                                End If
                            Next
                        End If
                        Dim DTSNFFAT_MAIN As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & fndMCCCode.Value & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
                        If DTSNFFAT_MAIN IsNot Nothing And DTSNFFAT_MAIN.Rows.Count > 0 Then
                            CurBalOFML_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                            CurBalOFML_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                        End If
                        CurBal_SNF = CurBalOFVL_SNF + CurBalOFML_SNF
                        CurBal_FAT = CurBalOFVL_FAT + CurBalOFML_FAT
                        If Not settAllowtoNegativeFATSNFKgAtTankerDispatch Then
                            If CurBal_SNF < clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) Then
                                Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) & " ")
                            End If
                            If CurBal_FAT < clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) Then
                                Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) & " ")
                            End If
                        End If
                    End If


                End If
                ''-----------
            End If



            If clsCommon.myLen(fndItemCode.Value) <= 0 Then
                fndItemCode.Focus()
                Throw New Exception("Please Select Item")
            End If

            If clsCommon.myLen(fndUOM.Value) <= 0 Then
                fndUOM.Focus()
                Throw New Exception("Please Select UOM")
            End If
            chkInterMittentValidation()
            If Not clsQualityCheck.chkIsGridColumnHasTag(gv) Then
                Throw New Exception(" Grid's Column is not being recognized, Please delete layout and try loading Document Again ")
            End If
            ''----------- richa agarwal 07 Sep, 2016
            If chkIntermittent.Checked = True Then
                If clsCommon.CompairString(fndPlantOrMCCCode.Value, fndFinalLoc.Value) <> CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select isnull(location_category,'') from tspl_location_master where location_code='" & clsCommon.myCstr(fndPlantOrMCCCode.Value) & "' ")), "MCC") <> CompairStringResult.Equal Then
                        fndPlantOrMCCCode.Focus()
                        Throw New Exception(" Select Plant/MCC should be MCC Type because final Location and Plant/MCC location should not be same.")
                    End If
                End If
                If clsCommon.CompairString(clsCommon.myCstr(ddlLevel.SelectedValue), "4") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(fndPlantOrMCCCode.Value), clsCommon.myCstr(fndFinalLoc.Value)) <> CompairStringResult.Equal Then
                        fndPlantOrMCCCode.Focus()
                        Throw New Exception("Select Plant/MCC Location should be same as Final Location. ")
                    End If
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select isnull(location_category,'') from tspl_location_master where location_code='" & clsCommon.myCstr(fndMCCCode.Value) & "' ")), "MCC") <> CompairStringResult.Equal Then
                    fndMCCCode.Focus()
                    Throw New Exception("Plant/MCC should be MCC Type in case of intermittent.")
                End If
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtCollectionOfMilk.Text) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage4
                    txtCollectionOfMilk.Focus()
                    Throw New Exception("Collection of Milk cannot be blank.")
                End If
                If clsCommon.myLen(txtDeliveryChallanNo.Text) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage4
                    txtDeliveryChallanNo.Focus()
                    Throw New Exception("Delivery Challan No. cannot be blank.")
                End If
            End If
            ''-------------
            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Function isPaperSealEmpty() As Boolean
        Dim isEmpty As Boolean = True
        If gvPaperSeal IsNot Nothing AndAlso gvPaperSeal.Rows.Count > 0 Then

            Try
                For i As Integer = 0 To gvPaperSeal.Rows.Count - 1
                    If clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSealNo).Value) <= 0 Then
                        isEmpty = True
                    Else
                        isEmpty = False
                        Exit For
                    End If
                Next
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try

        End If
        Return isEmpty
    End Function

    Function isManualSealEmpty() As Boolean
        Dim isEmpty As Boolean = True
        If gvManualSeal IsNot Nothing AndAlso gvManualSeal.Rows.Count > 0 Then

            Try
                For i As Integer = 0 To gvManualSeal.Rows.Count - 1
                    If clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealNo).Value) <= 0 Then
                        isEmpty = True
                    Else
                        isEmpty = False
                        Exit For
                    End If
                Next

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try

        End If
        Return isEmpty
    End Function

    Sub SaveData(ByVal isPostbtnClick As Boolean)
        Try
            obj = New clsMccDispatch()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            If chkIntermittent.Checked Then
                If clsCommon.CompairString(fndPlantOrMCCCode.Value, fndFinalLoc.Value) = CompairStringResult.Equal Then
                    getProvisionBookingInterMittent()
                End If
            Else
                getProvisionBooking()
            End If
            obj.MCC_Code = fndMCCCode.Value
            Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Chalan_NO = fndChalanNo.Value
            obj.MCC_Name = clsCommon.myCstr(txtMCCName.Text)
            obj.Dispatch_Date = clsCommon.myCDate(dtpDateAndTime.Value, "dd/MMM/yyyy hh:mm tt")
            obj.Tanker_Dispatch_To = clsCommon.myCstr(ddlTankerDispatchTo.Text)
            obj.Collection_Of_Milk = clsCommon.myCstr(txtCollectionOfMilk.Text)
            obj.Delivery_Challan_No = clsCommon.myCstr(txtDeliveryChallanNo.Text)
            obj.Mcc_Or_Plant_Code = clsCommon.myCstr(fndPlantOrMCCCode.Value)
            obj.Tanker_No = clsCommon.myCstr(fndTnakerNo.Value)
            obj.Tanker_KM_Reading = clsCommon.myCdbl(txtTankerKMReading.Text)
            obj.Drip_Marking = clsCommon.myCstr(txtDripMarking.Text)
            obj.Tanker_Full = clsCommon.myCstr(ddlTankerFull.Text)
            obj.Control_Sample = clsCommon.myCstr(ddlControlSample.Text)
            obj.Name_Of_Custodian = clsCommon.myCstr(txtNameOfCustodian.Text)
            obj.Seal_No1 = clsCommon.myCstr(gvManualSeal.Rows(0).Cells(colSealNo).Value)
            obj.Seal_No2 = clsCommon.myCstr(gvManualSeal.Rows(1).Cells(colSealNo).Value)
            obj.Seal_No3 = clsCommon.myCstr(gvManualSeal.Rows(2).Cells(colSealNo).Value)
            obj.Seal_No4 = clsCommon.myCstr(gvManualSeal.Rows(3).Cells(colSealNo).Value)
            obj.Seal_No5 = clsCommon.myCstr(gvManualSeal.Rows(4).Cells(colSealNo).Value)
            obj.Seal_No6 = clsCommon.myCstr(gvManualSeal.Rows(5).Cells(colSealNo).Value)
            obj.Seal_No7 = clsCommon.myCstr(gvManualSeal.Rows(6).Cells(colSealNo).Value)
            obj.Seal_No8 = clsCommon.myCstr(gvManualSeal.Rows(7).Cells(colSealNo).Value)
            obj.Seal_No9 = clsCommon.myCstr(gvManualSeal.Rows(8).Cells(colSealNo).Value)
            obj.Seal_No10 = clsCommon.myCstr(gvManualSeal.Rows(9).Cells(colSealNo).Value)
            obj.control_sample_fat = clsCommon.myCdbl(txtControlSampleFAT.Text)
            obj.control_sample_snf = clsCommon.myCdbl(txtControlSampleSNF.Text)
            obj.Item_Code = clsCommon.myCstr(fndItemCode.Value)
            obj.Tanker_Transporter_Name = clsCommon.myCstr(txtTankerTransporterName.Text)
            obj.Payment_Type = clsCommon.myCstr(txtPaymentType.Text)
            obj.Payment_Rate = clsCommon.myCstr(txtPaymentRate.Text)
            obj.Charge_For = clsCommon.myCstr(txtChargesFor.Text)
            obj.Payment_Amount = clsCommon.myCdbl(txtTotalAmount.Text)
            obj.Chemist_Code = clsCommon.myCstr(fndChemistCode.Value)
            obj.Chemist_Name = clsCommon.myCstr(txtChemistName.Text)
            obj.UOM_Code = clsCommon.myCstr(fndUOM.Value)
            obj.UOM_desc = clsCommon.myCstr(txtUOMdesc.Text)
            obj.MCC_Weighment_No = lblWeighmentNo.Text
            obj.Rejected_Only = chkReject.Checked
            '===============================Added by preeti Gupta[13/04/2018]=============
            obj.openingKM = clsCommon.myCdbl(txtOpeningKM.Text)
            obj.closingKM = clsCommon.myCdbl(txtClosingKM.Text)
            obj.Transfer_Price = clsCommon.myCdbl(txtTransferPrice.Text)
            obj.PriceCode = clsCommon.myCstr(fndPriceChart.Value)
            obj.FAT_W = clsCommon.myCdbl(TxtFatWeightage.Text)
            obj.SNF_W = clsCommon.myCdbl(TxtSNFWeightage.Text)
            obj.FAT_R = clsCommon.myCdbl(txtfatPercentage.Text)
            obj.SNF_R = clsCommon.myCdbl(txtSNFPercentage.Text)
            '=============================================================================
            If ApplyTotalSolidPriceChart Then
                obj.PriceCode = txtTSPrice.Text
                obj.Transfer_Price = txtTSTransferPrice.Value
                obj.Total_Solid_Rate = txtTSRate.Value
            End If

            If chkIntermittent.Checked Then
                obj.isIntermittent = 1
            Else
                obj.isIntermittent = 0
            End If
            If obj.isIntermittent = 1 Then
                obj.CurrentLevel = ddlLevel.SelectedValue
                obj.FinalLoc = fndFinalLoc.Value
                If obj.CurrentLevel = 1 Then
                    obj.Level1ChallanNo = fndChalanNo.Value
                    fndLevel1ChallanNo.Value = fndChalanNo.Value
                Else
                    obj.Level1ChallanNo = fndLevel1ChallanNo.Value
                End If


                If clsCommon.CompairString(ddlTankerDispatchTo.Text, "Plant") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.DisAllowIntermittentTankerForPlantDispatch, Nothing)) = 1 Then
                        Throw New Exception(" Intermittent Tanker dispatch to Plant is not allowed")
                    End If
                    If clsCommon.MyMessageBoxShow("Do you want to dispatch this tanker as intermittent on Plant ? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
            Else
                obj.CurrentLevel = 0
                obj.FinalLoc = ""
                fndLevel1ChallanNo.Value = ""
            End If
            obj.No_Of_Cans = txtNoOfCans.Value
            obj.Against_Gate_Out = txtGateOut.Value
            If (Not obj.isNewEntry) AndAlso isReversed(obj.Chalan_NO, Nothing) Then
                obj.isReversed = 1
            Else
                obj.isReversed = 0
            End If
            Dim i As Integer = 0
            Dim objParam As New Mcc_Dispatch_Chalan_Parameter
            obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Parameter)
            For ii As Integer = 0 To gv.Rows.Count - 1
                For i = 0 To gv.Columns.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(gv.Rows(ii).Cells(i).Value).Trim) > 0 Then
                        If Not clsCommon.CompairString(gv.Columns(i).Name, ColIntermittentDispatchNo) = CompairStringResult.Equal Then
                            objParam = New Mcc_Dispatch_Chalan_Parameter
                            objParam.SNO = ii + 1
                            objParam.Chalan_No = clsCommon.myCstr(obj.Chalan_NO)
                            objParam.Param_Field_Code = clsCommon.myCstr(gv.Columns(i).Name)
                            objParam.Param_Field_Desc = clsCommon.myCstr(gv.Columns(i).HeaderText)
                            objParam.Param_Field_Value = clsCommon.myCstr(gv.Rows(ii).Cells(i).Value)
                            objParam.Param_Type = IIf(clsCommon.CompairString(clsCommon.myCstr(gv.Columns(i).Tag), "") = CompairStringResult.Equal, "NA", clsCommon.myCstr(gv.Columns(i).Tag))
                            obj.arrParmValue.Add(objParam)
                        End If
                    End If
                Next
            Next

            Dim objPaperSeal As clsMccDispatchPaperSealDetail
            obj.arrPaperSeal = New List(Of clsMccDispatchPaperSealDetail)
            For i = 0 To gvPaperSeal.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gvPaperSeal.Rows(i).Cells(colSealNo).Value)) > 0 Then
                    objPaperSeal = New clsMccDispatchPaperSealDetail()
                    objPaperSeal.Chalan_No = obj.Chalan_NO
                    objPaperSeal.Seal_No = clsCommon.myCstr(gvPaperSeal.Rows(i).Cells(colSealNo).Value)
                    obj.arrPaperSeal.Add(objPaperSeal)
                End If
            Next
            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            ''For Custom Fields
            obj.Form_ID = MyBase.Form_ID
            obj.arrCustomFields = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(obj.arrCustomFields)
            End If
            If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                clsCustomFieldGrid.GetData(obj.arrCustomFields, gv, MyBase.ArrDetailFields, colACCode)
            End If
            ''End of For Custom Fields
            obj.arr = New List(Of clsMCCDispatchDetail)
            For ii As Integer = 0 To gv.Rows.Count - 1
                Dim objtr As New clsMCCDispatchDetail
                objtr.SNo = ii + 1
                If AllowCanInformationintoGridForTankerDispatch = True Then
                    If clsCommon.myCBool(gv.Rows(ii).Cells(colIsCanType).Value) = True Then
                        objtr.isCanType = 1
                    Else
                        objtr.Chamber_No = clsCommon.myCstr(gv.Rows(ii).Cells(colChamberNo).Value)
                        objtr.isCanType = 0
                    End If
                Else
                    objtr.Chamber_No = clsCommon.myCstr(gv.Rows(ii).Cells(colChamberNo).Value)
                    objtr.isCanType = 0
                End If


                objtr.Chamber_Description = clsCommon.myCstr(gv.Rows(ii).Cells(colChamberDesc).Value)
                objtr.Item_Code = clsCommon.myCstr(gv.Rows(ii).Cells(colICode).Value)
                objtr.Item_Name = clsCommon.myCstr(gv.Rows(ii).Cells(colIName).Value)
                objtr.Item_UOM = clsCommon.myCstr(gv.Rows(ii).Cells(colUOM).Value)
                objtr.FAT_KG = clsCommon.myCstr(gv.Rows(ii).Cells(colFatKG).Value)
                objtr.FAT_Rate = clsCommon.myCstr(gv.Rows(ii).Cells(colFatRate).Value)
                objtr.SNF_Rate = clsCommon.myCstr(gv.Rows(ii).Cells(colSNFRate).Value)
                objtr.SNF_KG = clsCommon.myCstr(gv.Rows(ii).Cells(colSNFKG).Value)
                objtr.Amount = clsCommon.myCstr(gv.Rows(ii).Cells(colAmount).Value)
                objtr.Qty_KG = clsCommon.myCdbl(gv.Rows(objtr.SNo - 1).Cells(ColQtyInKg).Value)
                objtr.Intermittent_Dispatch_No = clsCommon.myCstr(gv.Rows(objtr.SNo - 1).Cells(ColIntermittentDispatchNo).Value)
                obj.arr.Add(objtr)


                ''Update total qty
                If ii = 0 Then
                    obj.FAT_RATE = objtr.FAT_Rate ''Willbe same for every row
                    obj.SNF_RATE = objtr.SNF_Rate ''Willbe same for every row
                End If
                obj.Net_Qty += objtr.Qty_KG
                obj.FAT_KG += objtr.FAT_KG
                obj.SNF_KG += objtr.SNF_KG
                obj.Amount += objtr.Amount
            Next
            obj.ArrSiloWise = New List(Of clsMCCDispatchSiloWise)
            If gvSilo.Rows.Count > 0 Then
                For jj As Integer = 0 To gvSilo.Rows.Count - 1
                    Dim objtr As New clsMCCDispatchSiloWise
                    objtr.Chamber_No = clsCommon.myCstr(gvSilo.Rows(jj).Cells(colSiloChamber_No).Value)
                    objtr.Item_Code = clsCommon.myCstr(gvSilo.Rows(jj).Cells(colSiloItem_Code).Value)
                    objtr.Location_Code = clsCommon.myCstr(gvSilo.Rows(jj).Cells(colSiloLocation_Code).Value)
                    objtr.Qty = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloQty).Value)
                    objtr.UOM = clsCommon.myCstr(gvSilo.Rows(jj).Cells(colSiloUOM).Value)
                    objtr.Stock_Qty = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloStock_Qty).Value)
                    objtr.Stock_UOM = clsCommon.myCstr(gvSilo.Rows(jj).Cells(colSiloStock_UOM).Value)
                    objtr.FAT_Per = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloFat_Per).Value)
                    objtr.Fat_Rate = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloFat_Rate).Value)
                    objtr.FAT_KG = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloFat_KG).Value)
                    objtr.Fat_Amt = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloFat_Amt).Value)
                    objtr.SNF_Per = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloSNF_Per).Value)
                    objtr.SNF_Rate = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloSNF_Rate).Value)
                    objtr.SNF_KG = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloSNF_KG).Value)
                    objtr.SNF_Amt = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloSNF_Amt).Value)
                    objtr.Amount = clsCommon.myCdbl(gvSilo.Rows(jj).Cells(colSiloAvg_Cost).Value)
                    obj.ArrSiloWise.Add(objtr)
                Next
            End If


            If clsMccDispatch.SaveData(obj) Then
                If Not isPostbtnClick Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                End If
                btnSave.Text = "Update"
                fndChalanNo.MyReadOnly = True
                btnDelete.Enabled = True
                btnPrint.Enabled = True
                btnPost.Enabled = True
                LoadData(obj.Chalan_NO, NavigatorType.Current)
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
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndChalanNo.Value) > 0 Then
                If clsDBFuncationality.getSingleValue("select count(*) from tspl_mcc_dispatch_challan where chalan_no='" & fndChalanNo.Value & "'", tran) > 0 Then
                    If clsCommon.MyMessageBoxShow("Want To Delete The Challan No : " & fndChalanNo.Value & " ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        If clsMccDispatch.deleteData(fndChalanNo.Value, tran) Then
                            tran.Commit()
                            clsCommon.MyMessageBoxShow(Me, "Deleted successFully", Me.Text)
                            Reset()
                        End If
                    End If
                Else
                    Throw New Exception("Challan No not Found to delete")
                End If
            Else
                Throw New Exception("Please Enter Challan  no to delete")
            End If
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Try
            Dim obj As clsMccDispatch = clsMccDispatch.getData(strCode, navType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Chalan_NO) > 0 Then
                Reset()
                isLoad = True
                fndMCCCode.Enabled = False
                chkReject.Enabled = False
                fndMCCCode.Value = obj.MCC_Code
                txtMCCName.Text = obj.MCC_Name
                lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)
                dtpDateAndTime.Value = clsCommon.myCDate(obj.Dispatch_Date, "dd/MM/yyyy hh:mm tt")
                fndChalanNo.Value = clsCommon.myCstr(obj.Chalan_NO)
                ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr(obj.Tanker_Dispatch_To)
                txtCollectionOfMilk.Text = clsCommon.myCstr(obj.Collection_Of_Milk)
                txtDeliveryChallanNo.Text = clsCommon.myCstr(obj.Delivery_Challan_No)
                fndPlantOrMCCCode.Value = clsCommon.myCstr(obj.Mcc_Or_Plant_Code)
                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
                isLoadingBulkSaleTanker = obj.isBulkSaleData
                fndTnakerNo.Value = clsCommon.myCstr(obj.Tanker_No)
                txtTankerKMReading.Text = clsCommon.myCdbl(obj.Tanker_KM_Reading)
                txtDripMarking.Text = clsCommon.myCstr(obj.Drip_Marking)
                ddlTankerFull.Text = clsCommon.myCstr(obj.Tanker_Full)
                ddlControlSample.SelectedValue = clsCommon.myCstr(obj.Control_Sample)
                txtNameOfCustodian.Text = clsCommon.myCstr(obj.Name_Of_Custodian)
                chkReject.Checked = obj.Rejected_Only
                txtEWayBillNo.Text = obj.EWayBillNo
                If obj.EWayBillDate IsNot Nothing Then
                    txtEWayBillDate.Checked = True
                    txtEWayBillDate.Value = obj.EWayBillDate
                End If
                txtElectronicRefNo.Text = obj.Electronic_Ref_No
                '===================added by preeti Gupta [13/04/2018]============
                lblClosingDate.Text = clsCommon.myCstr(obj.Closing_Date)
                txtOpeningKM.Text = clsCommon.myCdbl(obj.openingKM)
                txtClosingKM.Text = clsCommon.myCdbl(obj.closingKM)
                fndPriceChart.Value = clsCommon.myCstr(obj.PriceCode)
                TxtFatWeightage.Text = clsCommon.myCdbl(obj.FAT_W)
                TxtSNFWeightage.Text = clsCommon.myCdbl(obj.SNF_W)
                txtfatPercentage.Text = clsCommon.myCdbl(obj.FAT_R)
                txtSNFPercentage.Text = clsCommon.myCdbl(obj.SNF_R)
                txtTransferPrice.Text = clsCommon.myCdbl(obj.Transfer_Price)
                txtTollAmount.Text = clsCommon.myCdbl(obj.Toll_Amount)
                '=================================================================
                If ApplyTotalSolidPriceChart Then
                    txtTSPrice.Text = obj.PriceCode
                    txtTSTransferPrice.Value = obj.Transfer_Price
                    txtTSRate.Value = obj.Total_Solid_Rate
                End If
                txtNoOfCans.Value = obj.No_Of_Cans
                txtGateOut.Value = obj.Against_Gate_Out
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
                        If AllowCanInformationintoGridForTankerDispatch = True Then
                            If clsCommon.CompairString(objtr.isCanType, "1") = CompairStringResult.Equal Then
                                gv.Rows(objtr.SNo - 1).Cells(colIsCanType).Value = True
                            Else
                                gv.Rows(objtr.SNo - 1).Cells(colChamberNo).Value = objtr.Chamber_No
                                gv.Rows(objtr.SNo - 1).Cells(colIsCanType).Value = False
                            End If
                        Else
                            gv.Rows(objtr.SNo - 1).Cells(colChamberNo).Value = objtr.Chamber_No
                        End If

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
                gvManualSeal.Rows(0).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No1)
                gvManualSeal.Rows(1).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No2)
                gvManualSeal.Rows(2).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No3)
                gvManualSeal.Rows(3).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No4)
                gvManualSeal.Rows(4).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No5)
                gvManualSeal.Rows(5).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No6)
                gvManualSeal.Rows(6).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No7)
                gvManualSeal.Rows(7).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No8)
                gvManualSeal.Rows(8).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No9)
                gvManualSeal.Rows(9).Cells(colSealNo).Value = clsCommon.myCstr(obj.Seal_No10)

                If obj.arrPaperSeal IsNot Nothing AndAlso obj.arrPaperSeal.Count > 0 Then
                    Try
                        For i As Integer = 0 To obj.arrPaperSeal.Count - 1
                            gvPaperSeal.Rows(i).Cells(colSealNo).Value = obj.arrPaperSeal(i).Seal_No
                        Next
                    Catch exx As Exception
                    End Try
                End If



                txtControlSampleFAT.Text = clsCommon.myCdbl(obj.control_sample_fat)
                txtControlSampleSNF.Text = clsCommon.myCdbl(obj.control_sample_snf)
                fndItemCode.Value = clsCommon.myCstr(obj.Item_Code)
                txtTankerTransporterName.Text = clsCommon.myCstr(obj.Tanker_Transporter_Name)
                txtPaymentType.Text = clsCommon.myCstr(obj.Payment_Type)
                txtPaymentRate.Text = clsCommon.myCstr(obj.Payment_Rate)
                txtChargesFor.Text = clsCommon.myCstr(obj.Charge_For)
                txtTotalAmount.Text = clsCommon.myCdbl(obj.Payment_Amount)
                fndChemistCode.Value = clsCommon.myCstr(obj.Chemist_Code)
                txtChemistName.Text = clsCommon.myCstr(clsEmployeeMaster.GetName(fndChemistCode.Value, Nothing))
                fndChalanNo.MyReadOnly = True
                fndUOM.Value = obj.UOM_Code
                txtUOMdesc.Text = obj.UOM_desc
                lblWeighmentNo.Text = obj.MCC_Weighment_No
                LoadDataSilo(obj.ArrSiloWise)
                'gv.Rows(0).Cells(colFatRate).Value = clsCommon.myCdbl(obj.FAT_RATE)
                'gv.Rows(0).Cells(colSNFRate).Value = clsCommon.myCdbl(obj.SNF_RATE)
                'gv.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(obj.FAT_KG)
                'gv.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(obj.SNF_KG)
                'gv.Rows(0).Cells(colAmount).Value = clsCommon.myCdbl(obj.Amount)
                If obj.isIntermittent = 1 Then
                    ddlLevel.SelectedValue = (obj.CurrentLevel).ToString
                    fndFinalLoc.Value = obj.FinalLoc
                    txtFinalLoc.Text = clsLocation.GetName(obj.FinalLoc, Nothing)
                    If clsCommon.myLen(obj.Level1ChallanNo) > 0 Then
                        fndLevel1ChallanNo.Value = obj.Level1ChallanNo
                    End If
                    GroupBox2.Enabled = False
                    chkIntermittent.Checked = True
                Else
                    chkIntermittent.Checked = False
                    ddlLevel.SelectedIndex = 0
                    fndFinalLoc.Value = ""
                    txtFinalLoc.Text = ""
                    GroupBox2.Enabled = False
                End If
                If obj.CurrentLevel >= 2 Then
                    chkIntermittent.Enabled = False
                Else
                    chkIntermittent.Enabled = True
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Chalan_NO)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Chalan_NO, MyBase.Form_ID, gv)
                ''End of For Custom Fields
                If obj.isPosted = 0 Then
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                    btnPrint.Enabled = True
                    btnPost.Enabled = True
                    btnReverse.Enabled = False
                    BtnResetProv.Enabled = False

                Else
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPrint.Enabled = True
                    btnPost.Enabled = False
                    btnReverse.Enabled = True
                    BtnResetProv.Enabled = True
                    If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, Nothing)) = 0, False, True) = False Then
                        btnUpdateAfterPost.Enabled = True
                        btnClKM.Enabled = True
                    End If
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
            If isPaperSealEmpty() AndAlso isManualSealEmpty() Then
                If clsCommon.MyMessageBoxShow("You have not entered any Seal, Want to continue without seal", "Empty Seal", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                    SaveData(False)
                End If
            Else
                SaveData(False)
            End If
        End If
    End Sub

    Private Sub fndChalanNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndChalanNo._MYNavigator
        LoadData(fndChalanNo.Value, NavType)
    End Sub

    Private Sub fndChalanNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndChalanNo._MYValidating
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        fndChalanNo.Value = clsMccDispatch.getFinder(whrCls, fndChalanNo.Value, isButtonClicked)
        If clsCommon.myLen(fndChalanNo.Value) > 0 Then
            LoadData(fndChalanNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub
    '================update by preeti gupta against ticket no[SHR/14/06/18-000031]
    Sub printData(ByVal ChallanCode As String, ByVal MCCCode As String, ByVal PlantOrMCCCode As String)
        Dim ItemDesc As String = ""
        ItemDesc = clsFixedParameter.GetData(clsFixedParameterType.ItemDescForTankerdispatchPrint, clsFixedParameterCode.ItemDescForTankerDispatchPrint, Nothing)
        Dim MccAddress As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select REPLACE ( replace (REPLACE ( Add1+ ',' + Add2 + ',' + Add3 +','+ Add4,',,',','),',,',','),',,',',')  from TSPL_LOCATION_MASTER where Location_Code='" & MCCCode & "' "))
        Dim ToAddress As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select REPLACE ( replace (REPLACE ( Add1+ ',' + Add2 + ',' + Add3 +','+ Add4,',,',','),',,',','),',,',',')  from TSPL_LOCATION_MASTER where Location_Code='" & PlantOrMCCCode & "' "))
        'Show Driver,Chemist and MCC Incharge name  Ticket No- ERO/09/05/18-000301
        'Dim strQuery As String = " select tspl_company_master.Logo_Img as img, TSPL_COMPANY_MASTER.Comp_Name as compname, '" & MccAddress & "' as 'address','" & ToAddress & "' as 'ToAddress', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, tspl_mcc_dispatch_challan.MCC_Code as MCC_Code,TSPL_LOCATION_MASTER.Location_Desc as [Plant or Mcc Name]  ,tspl_mcc_dispatch_challan.MCC_Name as MCC_Name ,tspl_mcc_dispatch_challan.Dispatch_Date as Dispatch_Date ,tspl_mcc_dispatch_challan.Chalan_NO as Chalan_NO ,tspl_mcc_dispatch_challan.Tanker_Dispatch_To as Tanker_Dispatch_To ,tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code as Mcc_Or_Plant_Code ,tspl_mcc_dispatch_challan.Tanker_No as Tanker_No ,tspl_mcc_dispatch_challan.Tanker_KM_Reading as Tanker_KM_Reading ,tspl_mcc_dispatch_challan.Drip_Marking as Drip_Marking ,tspl_mcc_dispatch_challan.Tanker_Full as Tanker_Full ,tspl_mcc_dispatch_challan.Control_Sample as Control_Sample ,tspl_mcc_dispatch_challan.Name_Of_Custodian as Name_Of_Custodian ,case when isnull(tspl_mcc_dispatch_challan.Seal_No1,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No1+','end + case when isnull(tspl_mcc_dispatch_challan.Seal_No2,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No2+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No3,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No3+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No4,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No4+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No5,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No5+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No6,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No6+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No7,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No7+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No8,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No8+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No9,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No9+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No10,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No10 end as seal_No,  tspl_mcc_dispatch_challan.isPosted as isPosted ,tspl_mcc_dispatch_challan.Posting_Date as Posting_Date ,tspl_mcc_dispatch_challan.item_code,'" & ItemDesc & "' item_desc,tspl_mcc_dispatch_challan.Tare_Weight as Tare_Weight ,tspl_mcc_dispatch_challan.Gross_Weight as Gross_Weight ,tspl_mcc_dispatch_challan.Net_Qty as Net_Qty ,tspl_mcc_dispatch_challan.Transfer_Price as Transfer_Price ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code as Param_Field_Code ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Desc as Param_Field_Desc ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Value as Param_Field_Value ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type as Param_Type, case when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='NA' then 1 when  TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='FAT' then 2 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='SNF' then 3 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='CLR' then 4 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='OTHERS' then 5 else 6 end as Ordering from tspl_mcc_dispatch_challan left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail on TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.chalan_no=tspl_mcc_dispatch_challan.chalan_no left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MCC_Dispatch_Challan.Comp_Code left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code inner join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code =TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code   where TSPL_MCC_Dispatch_Challan.chalan_no='" & ChallanCode & "' and TSPL_PARAMETER_MASTER.IsForPrintOnDispatch=1 order by Ordering "
        Dim strQuery As String = " select tspl_company_master.Logo_Img as img, TSPL_COMPANY_MASTER.Comp_Name as compname, '" & MccAddress & "' as 'address','" & ToAddress & "' as 'ToAddress', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, tspl_mcc_dispatch_challan.MCC_Code as MCC_Code,TSPL_LOCATION_MASTER.Location_Desc as [Plant or Mcc Name]  ,tspl_mcc_dispatch_challan.MCC_Name as MCC_Name ,tspl_mcc_dispatch_challan.Dispatch_Date as Dispatch_Date ,tspl_mcc_dispatch_challan.Chalan_NO as Chalan_NO ,tspl_mcc_dispatch_challan.Tanker_Dispatch_To as Tanker_Dispatch_To ,tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code as Mcc_Or_Plant_Code ,tspl_mcc_dispatch_challan.Tanker_No as Tanker_No ,tspl_mcc_dispatch_challan.Tanker_KM_Reading as Tanker_KM_Reading ,tspl_mcc_dispatch_challan.Drip_Marking as Drip_Marking ,tspl_mcc_dispatch_challan.Tanker_Full as Tanker_Full ,tspl_mcc_dispatch_challan.Control_Sample as Control_Sample ,tspl_mcc_dispatch_challan.Name_Of_Custodian as Name_Of_Custodian ,case when isnull(tspl_mcc_dispatch_challan.Seal_No1,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No1+','end + case when isnull(tspl_mcc_dispatch_challan.Seal_No2,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No2+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No3,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No3+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No4,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No4+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No5,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No5+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No6,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No6+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No7,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No7+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No8,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No8+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No9,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No9+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No10,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No10 end as seal_No,  tspl_mcc_dispatch_challan.isPosted as isPosted ,tspl_mcc_dispatch_challan.Posting_Date as Posting_Date ,tspl_mcc_dispatch_challan.item_code,'" & ItemDesc & "' item_desc,tspl_mcc_dispatch_challan.Tare_Weight as Tare_Weight ,tspl_mcc_dispatch_challan.Gross_Weight as Gross_Weight ,tspl_mcc_dispatch_challan.Net_Qty as Net_Qty ,tspl_mcc_dispatch_challan.Transfer_Price as Transfer_Price ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code as Param_Field_Code ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Desc as Param_Field_Desc ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Value as Param_Field_Value ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type as Param_Type, case when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='NA' then 1 when  TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='FAT' then 2 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='SNF' then 3 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='CLR' then 4 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='OTHERS' then 5 else 6 end as Ordering ,isnull(tspl_mcc_dispatch_challan.Name_of_custodian,'') as Driver_Name,isnull(tspl_mcc_dispatch_challan.Chemist_Name,'') as Chemist_Name,isnull(TSPL_EMPLOYEE_MASTER.Emp_Name,'') as MCC_In_Charge from tspl_mcc_dispatch_challan left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail on TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.chalan_no=tspl_mcc_dispatch_challan.chalan_no left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MCC_Dispatch_Challan.Comp_Code left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code inner join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code =TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code left join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MCC_Dispatch_Challan.mcc_code left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=tspl_mcc_master.MCC_In_Charge  where TSPL_MCC_Dispatch_Challan.chalan_no='" & ChallanCode & "' and TSPL_PARAMETER_MASTER.IsForPrintOnDispatch=1 order by Ordering "
        'Show Driver,Chemist and MCC Incharge name  Ticket No- ERO/09/05/18-000301
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDispatchChallan", "Dispatch Challan", "rptCompanyAddress.rpt")
        frmCRV = Nothing
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
            Load_Report_UDL()
        Else
            printData(fndChalanNo.Value, fndMCCCode.Value, fndPlantOrMCCCode.Value)
        End If

    End Sub

    Function getTransporterName(ByVal strTankerNo As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name   from TSPL_vendor_master where Vendor_Code=(select Tanker_Transporter_Code  from TSPL_TANKER_MASTER where Tanker_No='" & strTankerNo & "' )"))
        Return str
    End Function

    Function getPaymentType(ByVal strTankerNo As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & strTankerNo & "'"))
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            str = "Slab Wise KM-Range"
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            str = "Rate Per KM"
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            str = "" ' "Rate Per Day + Diesel"
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            str = "" '"Rental Basis"
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            str = "Rate Ltr/KG"
        Else
            str = ""
        End If
        Return str
    End Function

    Function getSlabDetail(ByVal strTankerNo As String) As String
        Dim rValue As String = String.Empty
        Dim fromValue As Double = 0
        Dim qry As String = "select * from tspl_slab_range_detail where form_id='" & clsUserMgtCode.frmTankerMaster & "' and trans_id='" & strTankerNo & "' order by slab_upto"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If i = 0 Then
                    fromValue = 1
                Else
                    fromValue = clsCommon.myCdbl(dt.Rows(i - 1)("Slab_Upto")) + 1
                End If
                rValue = rValue & "From " & CInt(fromValue) & " KM  To  " & CInt(clsCommon.myCdbl(dt.Rows(i)("Slab_Upto"))) & " KM,  Rate: " & clsCommon.myCdbl(dt.Rows(i)("Slab_Rate")) & Environment.NewLine
            Next
        End If
        Return rValue
    End Function

    Function getPaymentRate(ByVal strTankerNo As String) As String
        Dim rValue As String = String.Empty
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select *  from tspl_tanker_master where Tanker_No ='" & strTankerNo & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim str As String = clsCommon.myCstr(dt.Rows(0)("Status"))
            If clsCommon.myLen(str) > 0 Then
                If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
                    rValue = getSlabDetail(strTankerNo)
                ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
                    rValue = " Rate  : " & clsCommon.myCdbl(dt.Rows(0)("Price_KM")) & " Per KM "
                ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
                    'rValue = " Charges Per Day: " & clsCommon.myCdbl(dt.Rows(0)("Shift_Charges")) & Environment.NewLine
                    'rValue = rValue & " Avg. KM Per Ltr : " & clsCommon.myCdbl(dt.Rows(0)("Avg_KM_Ltr")) & Environment.NewLine
                    'rValue = rValue & " Diesel Rate Per/Ltr : " & clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))
                    rValue = ""
                ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
                    rValue = "" '" Rent  : " & clsCommon.myCdbl(dt.Rows(0)("Rental_Amount")) & " Per " & clsCommon.myCstr(dt.Rows(0)("Rental_Type"))
                ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
                    rValue = " Rate : " & clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG")) & " Per " & clsCommon.myCstr(dt.Rows(0)("Rate_Type"))
                Else
                    rValue = ""
                End If
            End If
        End If

        Return rValue

    End Function

    Function getDistance(ByVal fromLoc As String, ByVal toLoc As String) As Double
        Dim Distance As Double = 0
        Dim qry As String = " select Distance  from tspl_location_distance_master  where (From_Location_code ='" & fromLoc & "' and to_Location_Code ='" & toLoc & "' ) or (From_Location_code ='" & toLoc & "' and to_Location_Code ='" & fromLoc & "' ) "
        Distance = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If Distance = 0 Then
            Distance = -1
        End If
        Return Distance
    End Function

    Function getSlabLowerRange() As Double
        Dim qry As String = " select  Top 1 Slab_Upto + 1 as [From Range]   from tspl_slab_range_detail  where Trans_ID ='" & fndTnakerNo.Value & "' and Slab_Upto < " & getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value) & " order by Slab_Upto desc "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return value
    End Function

    Function getSlabLowerRangeInterMittent() As Double
        Dim Level1Mcc As String = clsDBFuncationality.getSingleValue(" select MCC_Code  from tspl_mcc_dispatch_challan where Chalan_NO='" & fndLevel1ChallanNo.Value & "' ")
        Dim qry As String = " select  Top 1 Slab_Upto + 1 as [From Range]   from tspl_slab_range_detail  where Trans_ID ='" & fndTnakerNo.Value & "' and Slab_Upto < " & getDistance(Level1Mcc, fndPlantOrMCCCode.Value) & " order by Slab_Upto desc "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return value
    End Function

    Function getSlabUpperRange() As Double
        Dim qry As String = " select Top 1 Slab_Upto   from tspl_slab_range_detail  where Trans_ID ='" & fndTnakerNo.Value & "' and Slab_Upto >= " & getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value) & " order by Slab_Upto  "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return value
    End Function

    Function getSlabUpperRangeInterMittent() As Double
        Dim Level1Mcc As String = clsDBFuncationality.getSingleValue(" select MCC_Code  from tspl_mcc_dispatch_challan where Chalan_NO='" & fndLevel1ChallanNo.Value & "' ")
        Dim qry As String = " select Top 1 Slab_Upto   from tspl_slab_range_detail  where Trans_ID ='" & fndTnakerNo.Value & "' and Slab_Upto >= " & getDistance(Level1Mcc, fndPlantOrMCCCode.Value) & " order by Slab_Upto  "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return value
    End Function

    Function getChargesFor(ByVal strTankerNo As String) As String
        'Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & strTankerNo & "'"))
        'If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
        '    'str = "Slab From " & getSlabLowerRange() & "  To " & getSlabUpperRange()
        'ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
        '    If getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value) = -1 Then
        '        Throw New Exception("Please Map Distance Between " & fndMCCCode.Value & " And " & fndPlantOrMCCCode.Value)
        '    End If
        '    str = " Total  " & getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value) & "  KM "
        'ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
        '    str = ""
        'ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
        '    str = ""
        'ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
        '    Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
        '    Dim QtyApply As Double = 0
        '    If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
        '        QtyApply = tCap
        '    Else
        '        QtyApply = clsCommon.myCdbl(txtNetQty.Text)
        '    End If
        '    str = " Total  " & QtyApply & " KG "
        'Else
        '    str = ""
        'End If
        'Return str
        Return ""
    End Function

    Function getChargesForInterMittent(ByVal strTankerNo As String) As String
        'Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & strTankerNo & "'"))
        'Dim Level1Mcc As String = clsDBFuncationality.getSingleValue(" select MCC_Code  from tspl_mcc_dispatch_challan where Chalan_NO='" & fndLevel1ChallanNo.Value & "' ")
        'If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
        '    str = "Slab From " & getSlabLowerRangeInterMittent() & "  To " & getSlabUpperRangeInterMittent()
        'ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
        '    If getDistance(Level1Mcc, fndPlantOrMCCCode.Value) = -1 Then
        '        Throw New Exception("Please Map Distance Between " & Level1Mcc & " And " & fndPlantOrMCCCode.Value)
        '    End If
        '    str = " Total  " & getDistance(Level1Mcc, fndPlantOrMCCCode.Value) & "  KM "
        'ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
        '    str = ""
        'ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
        '    str = ""
        'ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
        '    Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
        '    Dim QtyApply As Double = 0
        '    If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
        '        QtyApply = tCap
        '    Else
        '        QtyApply = clsCommon.myCdbl(txtNetQty.Text)
        '    End If
        '    str = " Total  " & QtyApply & " KG "
        'Else
        '    str = ""
        'End If
        'Return str
        Return ""
    End Function

    Function getSlabPaymentAmount() As Double
        Dim dist As Double = getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value)
        If dist = -1 Then
            Throw New Exception("Please Map Distance Between " & fndMCCCode.Value & " And " & fndPlantOrMCCCode.Value)
        End If
        Dim qry As String = " select Top 1 Slab_Rate   from tspl_slab_range_detail  where Trans_ID ='" & fndTnakerNo.Value & "' and Slab_Upto >= " & getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value) & " order by Slab_Upto  "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return (value * dist)

    End Function

    Function getSlabPaymentAmountInterMittent() As Double
        Dim Level1Mcc As String = clsDBFuncationality.getSingleValue(" select MCC_Code  from tspl_mcc_dispatch_challan where Chalan_NO='" & fndLevel1ChallanNo.Value & "' ")
        Dim dist As Double = getDistance(Level1Mcc, fndPlantOrMCCCode.Value)
        If dist = -1 Then
            Throw New Exception("Please Map Distance Between " & Level1Mcc & " And " & fndPlantOrMCCCode.Value)
        End If
        Dim qry As String = " select Top 1 Slab_Rate   from tspl_slab_range_detail  where Trans_ID ='" & fndTnakerNo.Value & "' and Slab_Upto >= " & getDistance(Level1Mcc, fndPlantOrMCCCode.Value) & " order by Slab_Upto  "
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If value <= 0 Then
            Throw New Exception(" No Slab found Of this range ")
        End If
        Return (value * dist)

    End Function

    Function getRatePerKMPaymentAmount() As Double
        Dim qry As String = " select Price_KM  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "'"
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return value

    End Function

    Function getRatePerLTRKGPaymentAmount() As Double
        Dim qry1 As String = " select Rate_Type  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "'"
        Dim qry2 As String = " select Price_Ltr_KG  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "'"
        Dim value As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry2))
        Dim ItemUom As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
        value = Math.Round(value / clsItemMaster.GetConvertionFactor(fndItemCode.Value, ItemUom, Nothing), 2)
        Return value
    End Function

    Function getPaymentAmount() As Double
        'Dim rValue As Double = 0
        'Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & fndTnakerNo.Value & "'"))
        'If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
        '    rValue = getSlabPaymentAmount()
        'ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
        '    rValue = getRatePerKMPaymentAmount() * getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value)
        'ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
        '    rValue = 0
        'ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
        '    rValue = 0
        'ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
        '    Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
        '    Dim QtyApply As Double = 0
        '    If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
        '        QtyApply = tCap
        '    Else
        '        QtyApply = clsCommon.myCdbl(txtNetQty.Text)
        '    End If
        '    rValue = getRatePerLTRKGPaymentAmount() * QtyApply
        'Else
        '    rValue = 0
        'End If
        'Return rValue
    End Function

    Function getPaymentAmountIntermittent() As Double
        'Dim rValue As Double = 0
        'Dim Level1Mcc As String = clsDBFuncationality.getSingleValue(" select MCC_Code  from tspl_mcc_dispatch_challan where Chalan_NO='" & fndLevel1ChallanNo.Value & "' ")
        'Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & fndTnakerNo.Value & "'"))
        'If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
        '    rValue = getSlabPaymentAmountInterMittent()
        'ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
        '    rValue = getRatePerKMPaymentAmount() * getDistance(Level1Mcc, fndPlantOrMCCCode.Value)
        'ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
        '    rValue = 0
        'ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
        '    rValue = 0
        'ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
        '    Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
        '    Dim QtyApply As Double = 0
        '    If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
        '        QtyApply = tCap
        '    Else
        '        QtyApply = clsCommon.myCdbl(txtNetQty.Text)
        '    End If
        '    rValue = getRatePerLTRKGPaymentAmount() * QtyApply
        'Else
        '    rValue = 0
        'End If
        'Return rValue
    End Function

    Sub getProvisionBooking()
        Try
            txtTankerTransporterName.Text = getTransporterName(fndTnakerNo.Value)
            txtPaymentType.Text = getPaymentType(fndTnakerNo.Value)
            txtPaymentRate.Text = getPaymentRate(fndTnakerNo.Value)
            txtChargesFor.Text = getChargesFor(fndTnakerNo.Value)
            txtTotalAmount.Text = getPaymentAmount()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub getProvisionBookingInterMittent()
        Try
            txtTankerTransporterName.Text = getTransporterName(fndTnakerNo.Value)
            txtPaymentType.Text = getPaymentType(fndTnakerNo.Value)
            txtPaymentRate.Text = getPaymentRate(fndTnakerNo.Value)
            txtChargesFor.Text = getChargesForInterMittent(fndTnakerNo.Value)
            txtTotalAmount.Text = getPaymentAmountIntermittent()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

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

                ''DONE BY RICHA AGAINST TICKET NO BHA/23/01/19-000788 REMOVE CONDITION BECUASE SAME CONDITION IS CHECKED ON ALLOW TO SAVE FUNCTION AND BEOLW CONDITION IS WRONG IN CASE OF MULTI CHAMBER WHEN ITEM CODE IS DIFFERENT ON GRID.
                'Dim CurBal As Double = 0
                'Dim CurBalOFVL As Double = 0
                'Dim CurBalOFML As Double = 0

                'Dim CurBal_SNF As Double = 0
                'Dim CurBalOFVL_SNF As Double = 0
                'Dim CurBalOFML_SNF As Double = 0

                'Dim CurBal_FAT As Double = 0
                'Dim CurBalOFVL_FAT As Double = 0
                'Dim CurBalOFML_FAT As Double = 0

                'If clsCommon.myLen(fndLevel1ChallanNo.Value) > 0 AndAlso clsCommon.myCdbl(ddlLevel.SelectedValue) > 1 Then
                '    'Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "')"))
                '    Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No in (Select Chalan_NO  from TSPL_MCC_Dispatch_Challan where Level1ChallanNo  ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "'  and CurrentLevel =" & clsCommon.myCdbl(ddlLevel.SelectedValue) - 1 & " and isnull(isIntermittent,0) =1))"))
                '    CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '    CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '    CurBal = CurBalOFVL + CurBalOFML
                'Else
                '    '  CurBal = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '    ''richa agarwal 17/08/2016
                '    Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' and Location_Type='Physical'"))
                '    If clsCommon.myLen(strsublocation) > 0 Then
                '        CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '    End If
                '    CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '    CurBal = CurBalOFVL + CurBalOFML
                'End If



                ''richa agarwal 04/06/2018 MIL/04/06/18-000023
                'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, Nothing), "0") = CompairStringResult.Equal Then
                '    dt = clsDBFuncationality.GetDataTable("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' order by TSPL_Location_MASTER.Location_Code ")
                '    Dim strsublocation As String = String.Empty
                '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '        For i As Integer = 0 To dt.Rows.Count - 1
                '            strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                '            If clsCommon.myLen(strsublocation) > 0 Then
                '                CurBalOFVL = CurBalOFVL + clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '            End If
                '        Next
                '    End If

                '    CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '    CurBal = CurBalOFVL + CurBalOFML

                '    ''TO CHECK FAT AND SNF
                '    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing), "1") = CompairStringResult.Equal Then
                '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '            For i As Integer = 0 To dt.Rows.Count - 1
                '                strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                '                If clsCommon.myLen(strsublocation) > 0 Then
                '                    Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & strsublocation & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
                '                    If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                '                        CurBalOFVL_SNF = CurBalOFVL_SNF + Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                '                        CurBalOFVL_FAT = CurBalOFVL_FAT + Math.Round(clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                '                    End If
                '                End If
                '            Next
                '        End If

                '        Dim DTSNFFAT_MAIN As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & fndMCCCode.Value & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
                '        If DTSNFFAT_MAIN IsNot Nothing And DTSNFFAT_MAIN.Rows.Count > 0 Then
                '            CurBalOFML_SNF = Math.Round(clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_SNF_KG")), 2, MidpointRounding.ToEven)
                '            CurBalOFML_FAT = Math.Round(clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_FAT_KG")), 2, MidpointRounding.ToEven)
                '        End If
                '        CurBal_SNF = CurBalOFVL_SNF + CurBalOFML_SNF
                '        CurBal_FAT = CurBalOFVL_FAT + CurBalOFML_FAT

                '        If CurBal_SNF < clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) Then
                '            Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) & " ")
                '        End If
                '        If CurBal_FAT < clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) Then
                '            Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) & " ")
                '        End If
                '    End If

                '     Dim dblEnteredQty As Double = 0
                '    For ii As Integer = 0 To gv.RowCount - 1
                '        dblEnteredQty += clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value)
                '    Next

                '    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                '        If CurBal > 0 Then
                '            CurBal = ClsLoadingTanker.GetTolerane(CurBal, dblEnteredQty)
                '        End If
                '    End If
                '    ''richa agarwal 23 Jan,2019 check only when entered qty is greater than 0
                '    If clsCommon.myCdbl(dblEnteredQty) > 0 Then
                '        ''richa MIL/07/06/18-000025
                '        If Math.Round(CurBal, 2) < Math.Round(dblEnteredQty, 2) Then
                '            Throw New Exception("Available Qty is :     " & Math.Round(CurBal, 2) & Environment.NewLine & "Required Qty :     " & clsCommon.myCstr(dblEnteredQty) & " ")
                '        End If
                '    End If

                'End If


                If (clsMccDispatch.PostData(MyBase.Form_ID, fndChalanNo.Value)) Then
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
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndChalanNo.Value, NavigatorType.Current)
                If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    ' printData(fndChalanNo.Value, fndMCCCode.Value, fndPlantOrMCCCode.Value)
                    ' Ticket No : BHA/27/07/18-000199 By prabhakar - for same type of print out click on print and post time print 
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
                        Load_Report_UDL()
                    Else
                        printData(fndChalanNo.Value, fndMCCCode.Value, fndPlantOrMCCCode.Value)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub RadGroupBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox6.Click

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
                If clsMccDispatch.ReverseAndUnpost(fndChalanNo.Value, Nothing) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndChalanNo.Value, NavigatorType.Current)
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
        For ii As Integer = 0 To gv.RowCount - 1
            For i As Integer = 0 To gv.Columns.Count - 1
                If clsCommon.CompairString(gv.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gv.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gv.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gv.Rows(ii).Cells(i).Value) <= 0 Or (Not IsNumeric(gv.Rows(ii).Cells(i).Value)) Then
                        isParamOK = False
                    End If
                End If
                If clsCommon.CompairString(gv.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    snfField = gv.Columns(i).Name
                End If
                If clsCommon.CompairString(gv.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                    fatField = gv.Columns(i).Name
                End If
                If clsCommon.CompairString(gv.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    clrField = gv.Columns(i).Name
                End If
                If clsCommon.CompairString(gv.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                    cfField = gv.Columns(i).Name
                End If
            Next
            If isParamOK Then
                gv.Rows(ii).Cells(snfField).Value = clsCommon.myFormat(clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gv.Rows(ii).Cells(fatField).Value), clsCommon.myCdbl(gv.Rows(ii).Cells(clrField).Value), clsCommon.myCdbl(gv.Rows(ii).Cells(cfField).Value)))
            Else
                gv.Rows(ii).Cells(snfField).Value = clsCommon.myFormat(0)
            End If
        Next


    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gv.Columns(FATColName) OrElse e.Column Is gv.Columns(SNFColName) OrElse e.Column Is gv.Columns(ColQtyInKg) Then
                CalculateCurrentRow(gv.CurrentRow.Index)
            End If
            isCellValueChangedOpen = False
        End If
        If Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
            If Not isCellValueChangedOpen Then

                isCellValueChangedOpen = True
                If clsCommon.CompairString(gv.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gv.CurrentColumn.Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gv.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Then
                    calculateSNF()
                End If
                isCellValueChangedOpen = False
            End If
        End If
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gv.Columns(colICode) Then
                Dim whr As String = " Product_Type ='mi' and exists (select  1 from tspl_item_uom_detail where tspl_item_uom_detail.Item_Code=TSPL_ITEM_MASTER.Item_Code and UOM_Code='KG')"
                gv.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(whr, gv.CurrentRow.Cells(colICode).Value, False)
                If clsCommon.myLen(gv.CurrentRow.Cells(colICode).Value) > 0 Then
                    gv.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(gv.CurrentRow.Cells(colICode).Value, Nothing)
                    gv.CurrentRow.Cells(colUOM).Value = "KG"
                Else
                    gv.CurrentRow.Cells(colIName).Value = ""
                    gv.CurrentRow.Cells(colUOM).Value = "KG"
                End If
            End If
            isCellValueChangedOpen = False
        End If

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
        gv.Rows(rowNo).Cells(colFatKG).Value = Math.Round(clsCommon.myCdbl(gv.Rows(rowNo).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gv.Rows(rowNo).Cells(FATColName).Value) / 100, 3, MidpointRounding.ToEven)
        gv.Rows(rowNo).Cells(colSNFKG).Value = Math.Round(clsCommon.myCdbl(gv.Rows(rowNo).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gv.Rows(rowNo).Cells(SNFColName).Value) / 100, 3, MidpointRounding.ToEven)
        gv.Rows(rowNo).Cells(colAmount).Value = Math.Round((clsCommon.myCdbl(gv.Rows(rowNo).Cells(colFatRate).Value) * clsCommon.myCdbl(gv.Rows(rowNo).Cells(colFatKG).Value)) + (clsCommon.myCdbl(gv.Rows(rowNo).Cells(colSNFRate).Value) * clsCommon.myCdbl(gv.Rows(rowNo).Cells(colSNFKG).Value)), 2)
    End Sub

    Private Sub ddlControlSample_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlControlSample.TextChanged
        If clsCommon.CompairString(ddlControlSample.Text, "YES") = CompairStringResult.Equal Then
            txtControlSampleFAT.Enabled = True
            txtControlSampleSNF.Enabled = True
        Else
            txtControlSampleFAT.Enabled = False
            txtControlSampleSNF.Enabled = False
        End If
    End Sub

    Function getUsedSealNoOnCurrentScreen(ByVal curRow As Integer) As String
        Dim strSealNo As String = String.Empty
        For i As Integer = 0 To gvPaperSeal.Rows.Count - 1
            If clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSealNo).Value) > 0 And i <> curRow Then
                strSealNo = strSealNo & "'" & clsCommon.myCstr(gvPaperSeal.Rows(i).Cells(colSealNo).Value) & "',"
            End If
        Next
        If clsCommon.myLen(strSealNo) > 0 Then
            strSealNo = Microsoft.VisualBasic.Left(strSealNo, Microsoft.VisualBasic.Len(strSealNo) - 1)
        End If
        If clsCommon.myLen(strSealNo) <= 0 Then
            strSealNo = ""
        End If
        Return strSealNo
    End Function

    Function getUsedPaperSealNoOnCurrentScreen(ByVal curRow As Integer) As String
        Dim strSealNo As String = String.Empty
        For i As Integer = 0 To gvManualSeal.Rows.Count - 1
            If clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealNo).Value) > 0 And i <> curRow Then
                strSealNo = strSealNo & "'" & clsCommon.myCstr(gvManualSeal.Rows(i).Cells(colSealNo).Value) & "',"
            End If
        Next
        If clsCommon.myLen(strSealNo) > 0 Then
            strSealNo = Microsoft.VisualBasic.Left(strSealNo, Microsoft.VisualBasic.Len(strSealNo) - 1)
        End If
        If clsCommon.myLen(strSealNo) <= 0 Then
            strSealNo = ""
        End If
        Return strSealNo
    End Function

    Private Sub gvPaperSeal_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvPaperSeal.CellEndEdit
        Dim qry As String = String.Empty
        Dim strItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='PS' "))
        If clsCommon.myLen(strItemCode) > 0 Then
        Else
            gvManualSeal.CurrentRow.Cells(colSealNo).Value = ""
            clsCommon.MyMessageBoxShow(Me, "No Item Of Seal Type Found", Me.Text)
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        whrCls = getUsedSealNoOnCurrentScreen(gvPaperSeal.CurrentRow.Index)
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvPaperSeal.Columns(colSealNo) Then
                qry = " select Auto_Sr_No as SealNo,Location_Code,Item_Code  from TSPL_SERIAL_ITEM  "
                Dim whrCls1 As String = " Item_Code ='" & strItemCode & "' and Location_Code= '" & fndMCCCode.Value & "'and In_Out_Type='I' and Auto_Sr_No not in (select seal_no from Tspl_Lost_defect_sealNo_Details union all select seal_no from  TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details union all select seal_no from  TSPL_Milk_Transfer_In_Paper_Seal_Details) "
                If clsCommon.myLen(whrCls) > 0 Then
                    whrCls = " and Auto_Sr_No not in (" & whrCls & ")"
                End If
                whrCls = whrCls1 & "  " & whrCls
                Try
                    gvPaperSeal.CurrentRow.Cells(colSealNo).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("sealFnd", qry, "SealNO", whrCls, gvPaperSeal.CurrentRow.Cells(colSealNo).Value, "SealNO", Not isCellValueChangedOpen))
                Catch
                End Try
            End If
        End If
        isCellValueChangedOpen = False
    End Sub

    Private Sub gvManualSeal_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvManualSeal.CellEndEdit
        Dim qry As String = String.Empty
        Dim strItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='MS' "))
        If clsCommon.myLen(strItemCode) > 0 Then
        Else
            gvManualSeal.CurrentRow.Cells(colSealNo).Value = ""
            clsCommon.MyMessageBoxShow("No Item Of Seal Type Found", Me.Text)
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        whrCls = getUsedPaperSealNoOnCurrentScreen(gvManualSeal.CurrentRow.Index)
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvManualSeal.Columns(colSealNo) Then
                qry = " select Auto_Sr_No as SealNo,Location_Code,Item_Code  from TSPL_SERIAL_ITEM  "
                Dim whrCls1 As String = " Item_Code ='" & strItemCode & "' and Location_Code= '" & fndMCCCode.Value & "'and In_Out_Type='I' and Auto_Sr_No not in (select seal_no from Tspl_Lost_defect_sealNo_Details union all select Seal_No1 as seal_No  from (select Seal_No1  from TSPL_MCC_Dispatch_Challan union all select  Seal_No2  from TSPL_MCC_Dispatch_Challan union all select Seal_No3  from TSPL_MCC_Dispatch_Challan union all select Seal_No4  from TSPL_MCC_Dispatch_Challan union all select Seal_No5  from TSPL_MCC_Dispatch_Challan union all select Seal_No6  from TSPL_MCC_Dispatch_Challan union all select Seal_No7  from TSPL_MCC_Dispatch_Challan union all select Seal_No8  from TSPL_MCC_Dispatch_Challan union all select Seal_No9  from TSPL_MCC_Dispatch_Challan union all select Seal_No10  from TSPL_MCC_Dispatch_Challan union all select New_Seal_No1  from tspl_milk_transfer_in union all select  New_Seal_No2  from tspl_milk_transfer_in union all select New_Seal_No3  from tspl_milk_transfer_in union all select New_Seal_No4  from tspl_milk_transfer_in union all select New_Seal_No5  from tspl_milk_transfer_in union all select New_Seal_No6  from tspl_milk_transfer_in union all select New_Seal_No7  from tspl_milk_transfer_in union all select New_Seal_No8  from tspl_milk_transfer_in union all select New_Seal_No9  from tspl_milk_transfer_in union all select New_Seal_No10  from tspl_milk_transfer_in  ) xx  where Seal_No1 <>'') "
                If clsCommon.myLen(whrCls) > 0 Then
                    whrCls = " and Auto_Sr_No not in (" & whrCls & ")"
                End If
                whrCls = whrCls1 & "  " & whrCls
                Try
                    gvManualSeal.CurrentRow.Cells(colSealNo).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("sealFnd", qry, "SealNO", whrCls, gvManualSeal.CurrentRow.Cells(colSealNo).Value, "SealNO", Not isCellValueChangedOpen))
                Catch exx As Exception
                End Try
            End If
            isCellValueChangedOpen = False
        End If

    End Sub

    Private Sub BtnStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnStart.Click
        Try
            If clsCommon.CompairString(BtnStart.Text, "Start") = CompairStringResult.Equal Then
                FrmMccDispatch.isPortOpened = True
                Timer1_Start()
                cboComPort.Enabled = False
                CboMachine.Enabled = False
                cboECOPro.Enabled = False
                BtnStart.Text = "Stop"

            Else
                FrmMccDispatch.isPortOpened = False
                cboComPort.Enabled = True
                CboMachine.Enabled = True
                cboECOPro.Enabled = True
                BtnStart.Text = "Start"
                LblFAT.Text = "00.00"
                LblSnf.Text = "00.00"
                objSr.ClosePort()
            End If
        Catch ex As Exception
            cboComPort.Enabled = True
            CboMachine.Enabled = True
            cboECOPro.Enabled = True
            BtnStart.Text = "Start"
            LblFAT.Text = "00.00"
            LblSnf.Text = "00.00"
            objSr.ClosePort()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Timer1_Start()
        Try

            Dim obj As clsPortSetting = clsPortSetting.getData(0, CboMachine.Text) ' 0 for Milk analyzer and 1 for weighing machine,
            If obj Is Nothing Then
                Throw New Exception("No setting found for device")
            End If
            objSr.BaudRate = obj.baud_rate
            objSr.Parity = obj.parity
            objSr.PortName = cboComPort.Text
            objSr.StopBits = obj.stop_bits
            objSr.DataBits = obj.data_bits
            objSr.DataForm = clsPortSetting.getMachineDataFormat(CboMachine.Text)
            objSr.isEkoProMachine = True
            objSr.isWeighingMachine = False
            objSr._LblFat = LblSnf
            objSr._LblSNF = LblFAT
            objSr._LblWeight = Nothing
            objSr.MachineName = clsPortSetting.getMachineMakePrefix(CboMachine.Text, 0)
            objSr.OpenPort()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub BtnRead_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            objEco.getDataMachineWise(cboComPort.Text, IIf(clsCommon.CompairString(CboMachine.Text, "kanha") = CompairStringResult.Equal, "K", "E"))
            LblSnf.Text = clsEkoPro.FAT.ToString()
            LblFAT.Text = clsEkoPro.SNF.ToString()

            If clsCommon.myCdbl(LblFAT.Text) > 0 Or clsCommon.myCdbl(LblSnf.Text) > 0 Then
                Timer1.Stop()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If BtnStart.Text = "Stop" AndAlso clsCommon.myLen(cboECOPro.SelectedValue) > 0 AndAlso (isForcellyStarted) Then
                ' Timer1.Stop()
                BtnRead_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadPageViewPage2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub fndChemistCode__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndChemistCode._MYOpenMasterForm

    End Sub

    Private Sub fndChemistCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndChemistCode._MYValidating
        fndChemistCode.Value = clsCommon.myCstr(clsEmployeeMaster.getFinder(" Emp_status='Active' ", fndChemistCode.Value, isButtonClicked))
        txtChemistName.Text = clsCommon.myCstr(clsEmployeeMaster.GetName(fndChemistCode.Value, Nothing))
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            getProvisionBooking()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndUOM__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndUOM._MYValidating
        If clsCommon.myLen(fndItemCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Item Code", Me.Text)
            fndItemCode.Focus()
            Exit Sub
        End If
        Dim qry As String = " select UOM_Code as UOM,UOM_Description   from TSPL_ITEM_UOM_DETAIL "
        Dim Whrcls As String = " item_code='" & fndItemCode.Value & "'"
        fndUOM.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("FNDUOM", qry, "UOM", Whrcls, fndUOM.Value))
        txtUOMdesc.Text = clsCommon.myCstr(clsItemUOMDetails.GetName(fndUOM.Value))
    End Sub

    Private Sub TxtFinder1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndUOM.Load

    End Sub

    Private Sub MyLabel16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblUOM.Click

    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub

    Private Sub txtControlSampleFAT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtControlSampleFAT.Validating
        Try
            Dim fracValue As Double = 0
            fracValue = clsCommon.myCdbl(txtControlSampleFAT.Text)
            fracValue = Math.Round((fracValue - CInt(fracValue)) * 100, 2)
            If CInt(fracValue) Mod 5 <> 0 AndAlso clsCommon.myCdbl(txtControlSampleFAT.Text) > 0 Then
                Throw New Exception("Control Sample FAT value, must have its decimal part multiple of 5")
                txtControlSampleFAT.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

        If Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value) Then
            chkIntermittent.Enabled = False
        Else
            chkIntermittent.Enabled = True
        End If
    End Sub

    Private Sub chkIntermittent_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIntermittent.ToggleStateChanged
        Dim qry As String = "   select MAX(ISNULL(currentlevel,0))+1 from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & fndTnakerNo.Value & "' "

        Dim curLevel As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If curLevel >= 2 Then
            GroupBox2.Enabled = Not chkIntermittent.Checked

        Else
            GroupBox2.Enabled = chkIntermittent.Checked
        End If
        ddlLevel.Enabled = False
        fndLevel1ChallanNo.Enabled = False
        ' GroupBox2.Enabled = chkIntermittent.Checked
        If chkIntermittent.Checked = False Then
            fndFinalLoc.Value = ""
            txtFinalLoc.Text = ""
            ddlLevel.SelectedIndex = 0
        End If
    End Sub

    Private Sub fndFinalLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndFinalLoc._MYValidating

        Dim whrCls As String = String.Empty
        'If Not clsMccMaster.isCurrentUserHO() Then
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        whrCls = " location_code in(" & objCommonVar.strCurrUserLocations & ")"
        '    End If
        'End If
        fndFinalLoc.Value = clsLocation.getFinder(whrCls, fndFinalLoc.Value, isButtonClicked)
        txtFinalLoc.Text = clsLocation.GetName(fndFinalLoc.Value, Nothing)

    End Sub

    Private Sub ddlLevel_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLevel.SelectedValueChanged
        If clsCommon.myCdbl(ddlLevel.SelectedValue) > 1 Then
            'txtTareWeight.ReadOnly = True
        Else
            'txtTareWeight.ReadOnly = False
        End If
    End Sub

    Private Sub fndLevel1ChallanNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLevel1ChallanNo._MYValidating
        Dim whrcls As String = " isIntermittent=1 and CurrentLevel=1 "
        fndLevel1ChallanNo.Value = clsMccDispatch.getFinder(whrcls, fndLevel1ChallanNo.Value, isButtonClicked)
        'txtTareWeight.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select tare_weight from tspl_mcc_dispatch_challan where chalan_no='" & fndLevel1ChallanNo.Value & "' "))
    End Sub

    Private Sub btnUpdateTanker_Click(sender As Object, e As EventArgs) Handles btnUpdateTanker.Click
        Try
            If clsCommon.myLen(fndChalanNo.Value) <= 0 Then
                Throw New Exception("Please select a Challan")
            End If
            If clsCommon.myLen(fndTnakerNo.Value) <= 0 Then
                Throw New Exception("Please select a Tanker")
            End If
            If chkIntermittent.Checked Then
                Throw New Exception("Update of Intermittent Tanker is Not Allowed")
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isPosted  from TSPL_MCC_Dispatch_Challan  where Chalan_NO='" & fndChalanNo.Value & "'")) = 0 Then
                Throw New Exception("Update of tanker is only allowed for Posted Documents")
            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from Tspl_Gate_Entry_Details where Challan_No ='" & fndChalanNo.Value & "'")) > 0 Then
                Throw New Exception("Update of tanker is Not allowed Due to its Gate-In is Done")
            End If
            If updateTanker() Then
                clsCommon.MyMessageBoxShow(Me, "Tanker Updated Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function updateTanker() As Boolean
        Dim isSaved As Boolean = False
        Try
            'Dim PrevTanker As String = clsMccDispatch.GetTankerNO(fndChalanNo.Value, Nothing)
            Dim obj As clsMccDispatch = clsMccDispatch.getData(fndChalanNo.Value, NavigatorType.Current)
            obj.Tanker_No = fndTnakerNo.Value
            obj.isNewEntry = False
            isSaved = clsMccDispatch.SaveData(obj, Nothing, 1, False)
            '' richa agarwal KDI/10/08/18-000415 16 Aug,2018
            Dim strQry1 As String = " update TSPL_MCC_Dispatch_Challan_Stock_Detail set isPosted='1',Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy") & "' where chalan_no='" & obj.Chalan_NO & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry1)
            ''---------
            'clsMccDispatch.UpdateTankerStatus(PrevTanker, 1)
            'clsMccDispatch.UpdateTankerStatus(fndTnakerNo.Value, 0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Private Sub txtItemDesc_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub fndItemCode_Load(sender As Object, e As EventArgs) Handles fndItemCode.Load

    End Sub

    Private Sub lblItemCode_Click(sender As Object, e As EventArgs) Handles lblItemCode.Click

    End Sub

    Public Sub Load_Report_UDL()
        Dim sQuery As String
        Dim dtgv As New DataTable
        Dim frmCRV As New frmCrystalReportViewer()
        Dim strCodeColumn As String = ""


        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then


            sQuery = "select Last_Query.Chamber_head, Final_Query.* from ( select TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name"

            sQuery += " ,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Email,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Tin_No,TSPL_COMPANY_MASTER.TinNo_Issue_Date,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.PanNo_Issue_Date,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2 , TSPL_COMPANY_MASTER.Add2 ,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code ,TSPL_ITEM_MASTER.HSN_Code, (SELECT TSPL_ITEM_MASTER.Item_Desc FROM TSPL_ITEM_MASTER WHERE 1=1 AND Item_Code =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code ) AS Item_Desc,  convert(decimal(18,2),t_BF .Param_Field_Value) as t_BF , CONVERT(decimal(18, 2), t_FAT.Param_Field_Value) AS t_FAT, convert(decimal(18,2),t_SNF.Param_Field_Value) as t_SNF,convert(decimal(18,2),t_AAB.Param_Field_Value) as t_AAB,convert(decimal(18,2),t_ABB .Param_Field_Value) as ABB,t_COB.Param_Field_Value as t_COB,convert(decimal(18,2),t_TC.Param_Field_Value) as t_TC,t_remarks.Param_Field_Value as t_remarks," &
     " TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chamber_Description  as CHAMBER_DESC, TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO , tspl_mcc_dispatch_challan.Chalan_NO ,   format(tspl_mcc_dispatch_challan.Dispatch_Date,'dd/MMM/yyyy') as ChalanDate, tanker_no,tspl_mcc_dispatch_challan.MCC_Code as from_Location,TSPL_LOCATION_MASTER.Location_Desc as From_Location_Name, TSPL_LOCATION_MASTER.Add1 as From_Location_Add1,TSPL_LOCATION_MASTER.Add2 as From_Location_Add2,TSPL_LOCATION_MASTER.Add3 as From_Location_Add3,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code_For,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo_For ,      tspl_mcc_dispatch_challan.mcc_or_plant_code as To_Location,ToLoca.Location_Desc as To_Location_Name,ToLoca.Add1 as To_Location_Add1,ToLoca.Add2 as To_Location_Add2,ToLoca.Add3 as To_Location_Add3,tspl_state_master_To_location_state.GST_STATE_Code as LOC_GST_State_Code_To,ToLoca.GSTNO as Loc_GstInNo_To ,                tspl_mcc_dispatch_challan.Dispatch_Date,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG,TSPL_LOCATION_MASTER.Add1 as Loc_Add1 ,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_Add3, TSPL_MCC_Dispatch_Challan.EWayBillNo,case when TSPL_MCC_Dispatch_Challan.EWayBillDate is null then '' else convert (varchar,TSPL_MCC_Dispatch_Challan.EWayBillDate,103) end as EWayBillDate,TSPL_MCC_Dispatch_Challan.Electronic_Ref_No   from tspl_mcc_dispatch_challan " &
     " left join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chalan_no=tspl_mcc_dispatch_challan.chalan_no  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MCC_Dispatch_Challan.Comp_Code " &
     " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =tspl_mcc_dispatch_challan.MCC_Code " &
     " left join TSPL_LOCATION_MASTER as ToLoca on ToLoca.Location_Code =tspl_mcc_dispatch_challan.mcc_or_plant_code " &
    " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO  and    TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'COB') t_COB     On t_COB .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO  and t_COB.SNO =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno" &
    " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'BUTYROREFRACTOMETER') t_BF    On t_BF .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and t_BF.SNO =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno" &
    " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'SNF') t_SNF     On t_SNF .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and t_SNF.SNO =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno" &
    "   LEFT OUTER JOIN (SELECT TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.* FROM TSPL_MCC_Dispatch_Challan LEFT OUTER JOIN TSPL_Mcc_Dispatch_Chalan_Parameter_Detail ON TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO AND TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'FAT') t_FAT ON t_FAT.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO AND t_FAT.SNO = TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno " &
    " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'ACIDITY') t_AAB     On t_AAB .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and t_AAB.SNO =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno" &
     " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'ABB') t_ABB    On t_ABB .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and t_ABB.SNO =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno" &
    " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'T*C') t_TC     On t_TC .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO  and t_TC.SNO =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno" &
    " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code = 'colRemarks') t_Remarks    On t_Remarks .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO  and t_Remarks.SNO =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno" &
    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code  left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state  left outer join tspl_state_master as tspl_state_master_To_location_state on tspl_state_master_To_location_state.state_code=ToLoca.state "

            sQuery += " where 2=2  AND tspl_mcc_dispatch_challan.Chalan_NO ='" + fndChalanNo.Value + "' ) as Final_Query " &
     " right outer join (select 'FRONT' AS Chamber_head,'F' AS Chamber_Desc union all select 'MIDDLE' AS Chamber_head,'M' AS Chamber_Desc " &
     " UNION ALL select 'REAR' AS Chamber_head,'R' AS Chamber_Desc) AS Last_Query on Last_Query.Chamber_Desc =Final_Query.CHAMBER_DESC "
        Else

            Dim qry As String = " select count(*) from (select distinct Description from TSPL_PARAMETER_MASTER left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail on TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code=TSPL_PARAMETER_MASTER.Code"
            qry += " where Chalan_No='" & fndChalanNo.Value & "' and IsForPrintOnDispatch=1 ) as xx"
            Dim NoofParameter As Integer = clsDBFuncationality.getSingleValue(qry, Nothing)
            If clsCommon.CompairString(NoofParameter, 5) = CompairStringResult.Greater Then
                clsCommon.MyMessageBoxShow(Me, "Maximun 5 Parameter ", Me.Text)
                Exit Sub
            End If
            sQuery = "select * from (select distinct "
            sQuery += " TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Email,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Tin_No,TSPL_COMPANY_MASTER.TinNo_Issue_Date,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.PanNo_Issue_Date,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2 , TSPL_COMPANY_MASTER.Add2 "

            sQuery += "   ,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code ,(SELECT TSPL_ITEM_MASTER.HSN_Code FROM TSPL_ITEM_MASTER WHERE 1=1 AND Item_Code =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code )  as HSN_Code, (SELECT TSPL_ITEM_MASTER.Item_Desc FROM TSPL_ITEM_MASTER WHERE 1=1 AND Item_Code =TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code ) AS Item_Desc"
            sQuery += " , TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chamber_Description  as CHAMBER_DESC, TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO , tspl_mcc_dispatch_challan.Chalan_NO ,   format(tspl_mcc_dispatch_challan.Dispatch_Date,'dd/MMM/yyyy') as ChalanDate, tanker_no,tspl_mcc_dispatch_challan.MCC_Code as from_Location,TSPL_LOCATION_MASTER.Location_Desc as From_Location_Name, TSPL_LOCATION_MASTER.Add1 as From_Location_Add1,TSPL_LOCATION_MASTER.Add2 as From_Location_Add2,TSPL_LOCATION_MASTER.Add3 as From_Location_Add3,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code_For,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo_For ,      tspl_mcc_dispatch_challan.mcc_or_plant_code as To_Location,ToLoca.Location_Desc as To_Location_Name,ToLoca.Add1 as To_Location_Add1,ToLoca.Add2 as To_Location_Add2,ToLoca.Add3 as To_Location_Add3,tspl_state_master_To_location_state.GST_STATE_Code as LOC_GST_State_Code_To,ToLoca.GSTNO as Loc_GstInNo_To ,                tspl_mcc_dispatch_challan.Dispatch_Date,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG,TSPL_LOCATION_MASTER.Add1 as Loc_Add1 ,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_Add3, TSPL_MCC_Dispatch_Challan.EWayBillNo,case when TSPL_MCC_Dispatch_Challan.EWayBillDate is null then '' else convert (varchar,TSPL_MCC_Dispatch_Challan.EWayBillDate,103) end as EWayBillDate,TSPL_MCC_Dispatch_Challan.Electronic_Ref_No "

            sQuery += " ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Desc as Param_Field_Desc "
            sQuery += " ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Value,isnull(TSPL_MCC_DISPATCH_CHALLAN.No_Of_Cans,0) as No_Of_Cans "
            sQuery += " from tspl_mcc_dispatch_challan"
            sQuery += " left join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chalan_no=tspl_mcc_dispatch_challan.chalan_no  left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MCC_Dispatch_Challan.Comp_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =tspl_mcc_dispatch_challan.MCC_Code  left join TSPL_LOCATION_MASTER as ToLoca on ToLoca.Location_Code =tspl_mcc_dispatch_challan.mcc_or_plant_code "
            sQuery += " left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state  left outer join tspl_state_master as tspl_state_master_To_location_state on tspl_state_master_To_location_state.state_code=ToLoca.state"
            sQuery += " left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail on TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_NO and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO=TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.SNO"
            sQuery += " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.code=TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code"
            sQuery += "   where 2=2  AND tspl_mcc_dispatch_challan.Chalan_NO ='" & fndChalanNo.Value & "' and TSPL_PARAMETER_MASTER.IsForPrintOnDispatch=1)as Main "
            sQuery += " right outer join (select 'FRONT' AS Chamber_head,'F' AS Chamber_Desc "
            sQuery += " union all select 'MIDDLE' AS Chamber_head,'M' AS Chamber_Desc  UNION ALL select 'REAR' AS Chamber_head,'R' AS Chamber_Desc) AS Last_Query on Last_Query.Chamber_Desc =Main.CHAMBER_DESC"
        End If

        Dim dt As New DataTable
        dt = clsDBFuncationality.GetDataTable(sQuery)

        If dt.Rows.Count > 0 Then
            'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "MCCTankerDispatchUDL", "WeigntmenUDLRpt", "rptCompanyAddress.rpt")
            Dim strFromStateCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select State  from TSPL_LOCATION_MASTER where location_Code = '" + clsCommon.myCstr(dt.Rows(0)("from_Location")) + "' "))
            Dim strToStateCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select State  from TSPL_LOCATION_MASTER where location_Code = '" + clsCommon.myCstr(dt.Rows(0)("To_Location")) + "' "))
            If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dtpDateAndTime.Value)) Then
                If clsCommon.CompairString(strFromStateCode, strToStateCode) = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptTankerDispatch", "MCC Tanker Dispatch", clsCommon.myCDate(dtpDateAndTime.Value))
                Else
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptTankerDispatch_Bill_of_Supply", "MCC Tanker Dispatch", clsCommon.myCDate(dtpDateAndTime.Value))
                End If
            Else
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptTankerDispatch", "MCC Tanker Dispatch", clsCommon.myCDate(dtpDateAndTime.Value))
            End If

            'If clsCommon.CompairString(strFromStateCode, strToStateCode) = CompairStringResult.Equal Then
            '    frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptTankerDispatch", "MCC Tanker Dispatch", clsCommon.myCDate(dtpDateAndTime.Value))
            'Else
            '    frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptTankerDispatch_Bill_of_Supply", "MCC Tanker Dispatch", clsCommon.myCDate(dtpDateAndTime.Value))
            'End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        frmCRV = Nothing
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If clsCommon.myLen(fndChalanNo.Value) > 0 Then
                Dim obj As New clsMccDispatch
                obj.EWayBillNo = txtEWayBillNo.Text
                If txtEWayBillDate.Checked Then
                    obj.EWayBillDate = txtEWayBillDate.Value
                End If
                obj.Electronic_Ref_No = txtElectronicRefNo.Text
                If clsMccDispatch.UpdateAfterPosting(obj, fndChalanNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            Else
                Throw New Exception("Document no not found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUpdateAfterPost_Click(sender As Object, e As EventArgs) Handles btnUpdateAfterPost.Click
        Try
            If (clsCommon.myCdbl(txtOpeningKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Enter Opening KM", Me.Text)
                txtOpeningKM.Focus()
                Exit Sub
            End If
            If (clsCommon.myCdbl(txtClosingKM.Text) <= clsCommon.myCdbl(txtOpeningKM.Text)) Then
                clsCommon.MyMessageBoxShow(Me, "Closing KM must be greater than Opening KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If


            If clsCommon.myLen(fndChalanNo.Value) > 0 Then
                Dim obj As New clsMccDispatch
                obj.openingKM = txtOpeningKM.Text
                obj.closingKM = txtClosingKM.Text

                If clsMccDispatch.UpdateAfterPosting(obj, fndChalanNo.Value, Nothing) Then
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            Else
                Throw New Exception("Document no not found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub calculateAmountOnTotalSolids()
        Dim obj As clsTankerDispatchPriceMaster = clsTankerDispatchPriceMaster.GetLastestPriceChart(fndMCCCode.Value, dtpDateAndTime.Value, Nothing)
        If obj Is Nothing OrElse clsCommon.myLen(obj.PRICE_CODE) <= 0 Then
            Throw New Exception("Please set tanker dispatch price for mcc " + fndMCCCode.Value)
        End If
        txtTSPrice.Text = obj.PRICE_CODE
        txtTSRate.Value = obj.TOTAL_SOLID_RATE
        Dim totalQty As Decimal = 0
        Dim totalAmount As Decimal = 0
        Try
            For ii As Integer = 0 To gv.RowCount - 1
                If clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) >= 0 Then
                    totalQty += gv.Rows(ii).Cells(ColQtyInKg).Value
                    gv.Rows(ii).Cells(colFatKG).Value = clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gv.Rows(ii).Cells(FATColName).Value) / 100
                    gv.Rows(ii).Cells(colSNFKG).Value = clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gv.Rows(ii).Cells(SNFColName).Value) / 100
                    gv.Rows(ii).Cells(colFatRate).Value = Math.Round(obj.TOTAL_SOLID_RATE, 4)
                    gv.Rows(ii).Cells(colSNFRate).Value = Math.Round(obj.TOTAL_SOLID_RATE, 4)
                    gv.Rows(ii).Cells(colAmount).Value = Math.Round((obj.TOTAL_SOLID_RATE * (clsCommon.myCdbl(gv.Rows(ii).Cells(colFatKG).Value) + clsCommon.myCdbl(gv.Rows(ii).Cells(colSNFKG).Value))), 2)
                    totalAmount += clsCommon.myCdbl(gv.Rows(ii).Cells(colAmount).Value)
                End If
            Next
        Catch ex As Exception
        End Try
        If totalQty = 0 OrElse totalAmount = 0 Then
            txtTSTransferPrice.Value = 0
        Else
            txtTSTransferPrice.Value = Math.Round(totalAmount / totalQty, 2, MidpointRounding.AwayFromZero)
        End If

    End Sub

    Sub calculateAmount()
        Dim FatW As Double = 0
        Dim SNfW As Double = 0
        Dim FATRate As Double = 0
        Dim SNFRate As Double = 0
        Dim FATValue As Double = 0
        Dim SNfValue As Double = 0
        Dim FATRatio As Double = 0
        Dim SNFRatio As Double = 0
        Dim StdRate As Double = 0
        Dim fatKG As Double = 0
        Dim snfKG As Double = 0
        If clsCommon.myLen(fndPriceChart.Value) > 0 AndAlso clsCommon.myLen(txtTransferPrice.Text) > 0 AndAlso clsCommon.myCdbl(txtTransferPrice.Text) > 0 AndAlso (clsCommon.myCdbl(TxtFatWeightage.Text) + clsCommon.myCdbl(TxtSNFWeightage.Text)) = 100 AndAlso clsCommon.myCdbl(txtSNFPercentage.Text) > 0 AndAlso clsCommon.myCdbl(txtfatPercentage.Text) > 0 AndAlso clsCommon.myLen(FATColName) > 0 AndAlso clsCommon.myLen(SNFColName) > 0 Then
            Try
                FatW = clsCommon.myCdbl(TxtFatWeightage.Text)
                SNfW = clsCommon.myCdbl(TxtSNFWeightage.Text)
                FATRatio = clsCommon.myCdbl(txtfatPercentage.Text)
                SNFRatio = clsCommon.myCdbl(txtSNFPercentage.Text)

                FATRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Text) * FatW / FATRatio, 4)
                SNFRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Text) * SNfW / SNFRatio, 4)

                For ii As Integer = 0 To gv.RowCount - 1
                    If clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) >= 0 Then
                        gv.Rows(ii).Cells(colFatKG).Value = clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gv.Rows(ii).Cells(FATColName).Value) / 100
                        gv.Rows(ii).Cells(colSNFKG).Value = clsCommon.myCdbl(gv.Rows(ii).Cells(ColQtyInKg).Value) * clsCommon.myCdbl(gv.Rows(ii).Cells(SNFColName).Value) / 100

                        fatKG = clsCommon.myCdbl(gv.Rows(ii).Cells(colFatKG).Value)
                        snfKG = clsCommon.myCdbl(gv.Rows(ii).Cells(colSNFKG).Value)

                        gv.Rows(ii).Cells(colFatRate).Value = Math.Round(FATRate, 4)
                        gv.Rows(ii).Cells(colSNFRate).Value = Math.Round(SNFRate, 4)

                        gv.Rows(ii).Cells(colAmount).Value = Math.Round((FATRate * fatKG) + (SNFRate * snfKG), 2)
                    End If
                Next
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub fndPriceChart__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPriceChart._MYValidating
        fndPriceChart.Value = clsPriceChartBulkProc.getFinder("", fndPriceChart.Value, isButtonClicked)
        If clsCommon.myLen(fndPriceChart.Value) > 0 Then
            Dim objP As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(fndPriceChart.Value, NavigatorType.Current)
            If objP IsNot Nothing Then
                TxtFatWeightage.Text = objP.Fat_Weightage
                TxtSNFWeightage.Text = objP.Snf_Weightage
                txtfatPercentage.Text = objP.Fat_Percentage
                txtSNFPercentage.Text = objP.Snf_Percentage
            Else

                TxtFatWeightage.Text = ""
                TxtSNFWeightage.Text = ""
                txtfatPercentage.Text = ""
                txtSNFPercentage.Text = ""
                gv.Rows(0).Cells(colFatRate).Value = ""
                gv.Rows(0).Cells(colSNFRate).Value = ""
                gv.Rows(0).Cells(colAmount).Value = ""
            End If
        Else
            TxtFatWeightage.Text = ""
            TxtSNFWeightage.Text = ""
            txtfatPercentage.Text = ""
            txtSNFPercentage.Text = ""
            gv.Rows(0).Cells(colFatRate).Value = 0
            gv.Rows(0).Cells(colSNFRate).Value = 0
            gv.Rows(0).Cells(colAmount).Value = 0
        End If
        calculateAmount()

    End Sub

    Private Sub txtTransferPrice_TextChanged(sender As Object, e As EventArgs) Handles txtTransferPrice.TextChanged
        calculateAmount()
    End Sub

    Private Sub txtGateOut__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGateOut._MYValidating
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TNKdRNO1", clsMCCTankerGateOut.GetPendingGateOutQry(fndChalanNo.Value, Nothing))
        If Not dr Is Nothing Then
            txtGateOut.Value = clsCommon.myCstr(dr("GATE_OUT_NO"))
            fndPlantOrMCCCode.Value = clsCommon.myCstr(dr("LOCATION_CODE"))
            txtPlantOrMccName.Text = clsCommon.myCstr(dr("Location_Desc"))
            lblToPlantName.Text = clsLocation.GetPlantNameFromMCC(fndPlantOrMCCCode.Value, Nothing)
            fndMCCCode.Value = clsCommon.myCstr(dr("MCC_CODE"))
            txtMCCName.Text = clsCommon.myCstr(dr("MCC_NAME"))
            lblFromPlantName.Text = clsLocation.GetPlantNameFromMCC(fndMCCCode.Value, Nothing)
            fndTnakerNo.Value = clsCommon.myCstr(dr("TANKER_NO"))
            txtOpeningKM.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select opening_KM From TSPL_MCC_TANKER_GATE_OUT where Gate_out_No='" + txtGateOut.Value + "'"))
            SetTankerDetail()
            FillOldData()
        End If
    End Sub
    ' Ticket No : TEC/29/10/18-000352 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(fndChalanNo.Value)
    End Sub

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        Try
            If e.Column Is gv.Columns(FATColName) Then
                gv.CurrentRow.Cells(FATColName).ReadOnly = settTankerDispatchAvgFATSNFPer
            ElseIf e.Column Is gv.Columns(SNFColName) Then
                gv.CurrentRow.Cells(SNFColName).ReadOnly = settTankerDispatchAvgFATSNFPer
            End If

            If settTankerDispatchIntermittentSingleGateIn Then
                If gv.CurrentCell IsNot Nothing Then
                    If clsCommon.myLen(gv.CurrentColumn.Name) > 0 Then
                        If clsCommon.myLen(gv.CurrentRow.Cells(ColIntermittentDispatchNo).Value) > 0 AndAlso chkIntermittent.Checked Then
                            gv.CurrentRow.Cells(gv.CurrentColumn.Name).ReadOnly = True
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadDataSilo(ByVal Arr As List(Of clsMCCDispatchSiloWise))
        LoadBlankGridSilo()
        For Each objtr As clsMCCDispatchSiloWise In Arr
            gvSilo.Rows.AddNew()
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloChamber_No).Value = objtr.Chamber_No
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloItem_Code).Value = objtr.Item_Code
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloLocation_Code).Value = objtr.Location_Code
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloQty).Value = objtr.Qty
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloUOM).Value = objtr.UOM
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloStock_Qty).Value = objtr.Stock_Qty
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloStock_UOM).Value = objtr.Stock_UOM
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloFat_Per).Value = objtr.FAT_Per
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloFat_Rate).Value = objtr.Fat_Rate
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloFat_KG).Value = objtr.FAT_KG
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloFat_Amt).Value = objtr.Fat_Amt
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloSNF_Per).Value = objtr.SNF_Per
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloSNF_Rate).Value = objtr.SNF_Rate
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloSNF_KG).Value = objtr.SNF_KG
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloSNF_Amt).Value = objtr.SNF_Amt
            gvSilo.Rows(gvSilo.Rows.Count - 1).Cells(colSiloAvg_Cost).Value = objtr.Amount
        Next
    End Sub

    Private Sub LoadBlankGridSilo()
        gvSilo.Columns.Clear()
        gvSilo.Rows.Clear()


        Dim repoText As New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Location"
        repoText.Width = 100
        repoText.Name = colSiloLocation_Code
        repoText.ReadOnly = True
        repoText.IsVisible = True
        gvSilo.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Chamber"
        repoText.Width = 100
        repoText.Name = colSiloChamber_No
        repoText.ReadOnly = True
        repoText.IsVisible = True
        gvSilo.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Item Code"
        repoText.Width = 100
        repoText.Name = colSiloItem_Code
        repoText.ReadOnly = True
        repoText.IsVisible = True
        gvSilo.MasterTemplate.Columns.Add(repoText)


        Dim repoNum As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n2}"
        repoNum.HeaderText = "Quantity"
        repoNum.Name = colSiloQty
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 2
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum) '8


        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "UOM"
        repoText.Width = 50
        repoText.Name = colSiloUOM
        repoText.ReadOnly = True
        repoText.IsVisible = True
        gvSilo.MasterTemplate.Columns.Add(repoText)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n10}"
        repoNum.HeaderText = "Stock Qty"
        repoNum.Name = colSiloStock_Qty
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 2
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)


        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Stock UOM"
        repoText.Width = 50
        repoText.Name = colSiloStock_UOM
        repoText.ReadOnly = True
        repoText.IsVisible = True
        gvSilo.MasterTemplate.Columns.Add(repoText)


        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n10}"
        repoNum.HeaderText = "Fat %"
        repoNum.Name = colSiloFat_Per
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 10
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n2}"
        repoNum.HeaderText = "Fat Rate"
        repoNum.Name = colSiloFat_Rate
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 2
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n3}"
        repoNum.HeaderText = "Fat KG"
        repoNum.Name = colSiloFat_KG
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 3
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n2}"
        repoNum.HeaderText = "Fat Amount"
        repoNum.Name = colSiloFat_Amt
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 2
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n2}"
        repoNum.HeaderText = "SNF %"
        repoNum.Name = colSiloSNF_Per
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 10
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n2}"
        repoNum.HeaderText = "SNF Rate"
        repoNum.Name = colSiloSNF_Rate
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 2
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n3}"
        repoNum.HeaderText = "SNF KG"
        repoNum.Name = colSiloSNF_KG
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 3
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)


        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n2}"
        repoNum.HeaderText = "SNF Amt"
        repoNum.Name = colSiloSNF_Amt
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 2
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = "{0:n2}"
        repoNum.HeaderText = "Amount"
        repoNum.Name = colSiloAvg_Cost
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.DecimalPlaces = 2
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSilo.MasterTemplate.Columns.Add(repoNum)
        For ii As Integer = 0 To gvSilo.Columns.Count - 1
            gvSilo.Columns(ii).ReadOnly = True
        Next

        'gvSilo.BestFitColumns()
        gvSilo.ShowFilteringRow = False
        gvSilo.AllowAddNewRow = False
    End Sub

    Private Sub BtnClKM_Click(sender As Object, e As EventArgs) Handles btnClKM.Click
        If CreateProvisionOfTankerDispatchWithClosingKM = True Then
            Dim strProvNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + fndChalanNo.Value + "' and Prov_type='Freight'")
            If clsCommon.myLen(strProvNo) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Provision Entry Already Exist", Me.Text)
                Exit Sub
            End If
            If (clsCommon.myLen(fndChalanNo.Value) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Document No not found", Me.Text)
                Exit Sub
            End If

            If (clsCommon.myCdbl(txtOpeningKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Enter Opening KM", Me.Text)
                txtOpeningKM.Focus()
                Exit Sub
            End If

            If (clsCommon.myCdbl(txtClosingKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Enter Closing KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If
            If (clsCommon.myCdbl(txtClosingKM.Text) <= clsCommon.myCdbl(txtOpeningKM.Text)) Then
                clsCommon.MyMessageBoxShow(Me, "Closing KM must be greater than Opening KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim obj As New clsMccDispatch
                obj.openingKM = txtOpeningKM.Text
                obj.closingKM = txtClosingKM.Text
                obj.Toll_Amount = txtTollAmount.Text
                obj.Closing_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                clsMccDispatch.UpdateAfterPosting(obj, fndChalanNo.Value, trans)

                clsMccDispatch.CreateProvison(fndChalanNo.Value, MyBase.Form_ID, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Provision Created successfully", Me.Text)
                LoadData(fndChalanNo.Value, NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Sub BtnResetProv_Click(sender As Object, e As EventArgs) Handles BtnResetProv.Click
        If CreateProvisionOfTankerDispatchWithClosingKM = True Then
            If (clsCommon.myLen(fndChalanNo.Value) <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Document No not found", Me.Text)
                Exit Sub
            End If
            'Dim strAPINVNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + fndChalanNo.Value + "' and Prov_type='Freight'", trans)
            'If clsCommon.myLen(strAPINVNo) > 0 Then
            '    clsCommon.MyMessageBoxShow("AP Invoice already generated - " & strAPINVNo, Me.Text)
            '    Exit Sub
            'End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim Qry As String = "update TSPL_MCC_DISPATCH_CHALLAN set ClosingKM=0 , Closing_Date = null where Chalan_NO='" + fndChalanNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Dim strProvNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + fndChalanNo.Value + "' and Prov_type='Freight'", trans)
                If clsCommon.myLen(strProvNo) > 0 Then

                    Qry = "delete from tspl_provision_entry where Ref_Doc_No ='" + fndChalanNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-EN' and Source_Doc_No='" + strProvNo + "'", trans)
                    If clsCommon.myLen(VoucherNo) > 0 Then
                        Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If

                    'clsProvisionEntry.ReverseAndUnpost(strProvNo, trans)
                    'clsProvisionEntry.deleteData(strProvNo, trans)
                    clsCommon.MyMessageBoxShow(Me, "Provision Delete successfully", Me.Text)
                End If

                trans.Commit()
                LoadData(fndChalanNo.Value, NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub

    Private Sub txtNoOfCans_TextChanged(sender As Object, e As EventArgs) Handles txtNoOfCans.TextChanged
        If AllowCanInformationintoGridForTankerDispatch Then
            SetCanDetail()
        End If

    End Sub

    Private Sub btnJE_Click(sender As Object, e As EventArgs) Handles btnJE.Click
        ShowJE(MyBase.Form_ID, fndChalanNo.Value)
    End Sub
End Class

