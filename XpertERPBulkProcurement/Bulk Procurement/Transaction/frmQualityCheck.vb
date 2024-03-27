Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System
Public Class FrmQualityCheck
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ItemwiseCorrectionFactoronQC As Integer = 0
    Dim IsCalculateCLR As Boolean = False
    Dim ParameterForSNFatQC As Integer = 0
    Dim FinalChamberwise As Integer = 0
    Dim MCCChamberwise As Integer = 0
    Dim blnSave As Boolean = False
    Dim ChangeFATCLRafterspecialApprovalonQC As Integer = 0
    Dim AllowBulkProcTransDateSameasGateEntryDate As Integer = 0
    Dim PickCorrectionFactorProcurementTypewise As Integer = 0
    Dim CheckParameterRangerProcurementTypewise As Integer = 0
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim strMGradeRejected As String = ""
    Dim strParameterRejected As String = ""
    Dim IsItemMilkType As Integer = 0
    Dim RunBulkProcWithoutMilkGrade As Integer = 0
    Dim IsPriceChartGradeWise As Integer = 0
    Dim AllowbulkProcurementSequencewise As Integer = 0
    Dim FirstQualityThenWeighment As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Public strDocCode As String = Nothing
    Public strDocType As String = Nothing
    Public Const colSLNo As String = "SLNO"
    Public Const colSealName As String = "SealName"
    Public Const colSealStatus As String = "SealStatus"
    Public Const colItemCode As String = "ItemCode"
    Public Const colHSN As String = "HSNCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colMilkTypeCode As String = "colMilkTypeCode"
    Public Const colMilkGradeCode As String = "colMilkGradeCode"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colDipValue As String = "colDipValue"
    Dim docType As String = String.Empty
    Public Shared isPortOpened As Boolean = False
    Dim obj As clsQualityCheck = Nothing
    Dim isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const colAutoFat As String = "AutoFat"
    Public Const colAutoSnf As String = "AutoSnf"
    Public Const colAutoCLR As String = "AutoCLR"
    Dim isForcellyStarted As Boolean = False
    Dim IsCanceled As Boolean = False
    Dim oldValue As Integer = 0
    Dim objEco As New clsEkoPro
    Dim objSr As New clsSerialPort
    Public CfColName As String = String.Empty
    Public FATColName As String = String.Empty
    Public SNFColName As String = String.Empty
    Public CLRColName As String = String.Empty
    Dim isContractorJobWork As Boolean = False
    Dim AllowManualRejection As Integer = 0
    Dim RunBulkProcOnAdjustFATCLR As Integer = 0
    Public Const colAdjustFAT As String = "Adjusted_FAT"
    Public Const colAdjustSNF As String = "Adjusted_SNF"
    Public Const colAdjustCLR As String = "Adjusted_CLR"
    Public Const colFromLocation As String = "colFromLocation"
    Dim settMODValueForFAT As Integer = 0
    Dim SettCalculateSNFFromCLRForMCCMilk As Boolean = False
    Dim RejectiononQCforSeparationofBulkProcurementMCC As Boolean = False
    Dim FillGeneralWeighmentDetailsByJobworkTypeGateInNo As Boolean = False
    Dim intBulkProcRunOneTypeGateEntry As Integer
    '=======update by preeti gupta against tickrt no[ERO/06/07/19-000676]
#End Region

    Private Sub FrmQualityCheck_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Panel3.Enabled = False
        SetUserMgmtNew()
        ItemwiseCorrectionFactoronQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemwiseCorrectionFactoronQC, clsFixedParameterCode.ItemwiseCorrectionFactoronQC, Nothing))
        ParameterForSNFatQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ParameterForSNFatQC, clsFixedParameterCode.ParameterForSNFatQC, Nothing))
        MCCChamberwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, Nothing))
        ChangeFATCLRafterspecialApprovalonQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ChangeFATCLRafterspecialApprovalonQC, clsFixedParameterCode.ChangeFATCLRafterspecialApprovalonQC, Nothing))
        AllowBulkProcTransDateSameasGateEntryDate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcTransDateSameasGateEntryDate, clsFixedParameterCode.AllowBulkProcTransDateSameasGateEntryDate, Nothing))
        CheckParameterRangerProcurementTypewise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckParameterRangerProcurementTypewise, clsFixedParameterCode.CheckParameterRangerProcurementTypewise, Nothing))
        PickCorrectionFactorProcurementTypewise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickCorrectionFactorProcurementTypewise, clsFixedParameterCode.PickCorrectionFactorProcurementTypewise, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        RunBulkProcWithoutMilkGrade = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBulkProcWithoutMilkGrade, clsFixedParameterCode.RunBulkProcWithoutMilkGrade, Nothing))
        IsItemMilkType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
        IsPriceChartGradeWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
        AllowbulkProcurementSequencewise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcurementSequencewise, clsFixedParameterCode.AllowBulkProcurementSequencewise, Nothing))
        FirstQualityThenWeighment = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QualityThenWeighmentinBulkProcurement, clsFixedParameterCode.QualityThenWeighmentinBulkProcurement, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))

        AllowManualRejection = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualRejectionOfTanker, clsFixedParameterCode.AllowManualRejectionOfTanker, Nothing))
        settMODValueForFAT = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MODValueForFAT, clsFixedParameterCode.MODValueForFAT, Nothing))

        RunBulkProcOnAdjustFATCLR = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBulkProcOnAdjustedFATCLR, clsFixedParameterCode.RunBulkProcOnAdjustedFATCLR, Nothing))
        SettCalculateSNFFromCLRForMCCMilk = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateSNFFromCLRForMCCMilk, clsFixedParameterCode.CalculateSNFFromCLRForMCCMilk, Nothing)) = 1)
        If ChangeFATCLRafterspecialApprovalonQC = 1 And RunBulkProcOnAdjustFATCLR = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Change FAT CLR setting is ON .Setting Run Bulk Proc On Adjust FAT CLR should be ON for this fuctionality.", Me.Text)
            Me.Close()
        End If
        If clsCommon.CompairString(AllowManualRejection, 1) = CompairStringResult.Equal Then
            btnManualReject.Visible = True
        Else
            btnManualReject.Visible = False
        End If
        RejectiononQCforSeparationofBulkProcurementMCC = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectiononQCforSeparationofBulkProcurementMCC, clsFixedParameterCode.RejectiononQCforSeparationofBulkProcurementMCC, Nothing)) = 1, True, False)
        FillGeneralWeighmentDetailsByJobworkTypeGateInNo = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FillGeneralWeighmentDetailsByJobworkTypeGateInNo, clsFixedParameterCode.FillGeneralWeighmentDetailsByJobworkTypeGateInNo, Nothing)) = 1, True, False)
        'If clsERPFuncationality.isCurrentUserMCC() Then
        '    chkMccProc.IsChecked = True
        '    chkBulkMilkProc.Enabled = False
        'Else
        '    chkBulkMilkProc.IsChecked = True
        '    chkBulkMilkProc.Enabled = True
        'End If
        intBulkProcRunOneTypeGateEntry = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcRunOneTypeGateEntry, clsFixedParameterCode.BulkProcRunOneTypeGateEntry, Nothing))
        If intBulkProcRunOneTypeGateEntry = 1 Then
            grpDocType.Visible = False
            chkBulkMilkProc.IsChecked = True
            chkBothDoc.Checked = False
            chkBothDoc.Visible = False
        ElseIf intBulkProcRunOneTypeGateEntry = 2 Then
            grpDocType.Visible = False
            chkMccProc.IsChecked = True
            chkBothDoc.Checked = False
            chkBothDoc.Visible = False
        Else
            grpDocType.Visible = True
        End If

        reset(True, True)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")

        If strDocCode IsNot Nothing AndAlso clsCommon.myLen(strDocCode) > 0 Then
            loadData(strDocCode, strDocType, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), strDocType, NavigatorType.Current)
        End If
        LoadParameterinAnalyzer()
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
        mnuEmailSmsSetting.Visibility = ElementVisibility.Hidden
        Dim AllowGenerateReferenceNoForBulkGateEntry As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowGenerateReferenceNoForBulkGateEntry, clsFixedParameterCode.AllowGenerateReferenceNoForBulkGateEntry, Nothing)) = 0, False, True)
        If AllowGenerateReferenceNoForBulkGateEntry = True Then
            EnableDisableControlByRefNoBase(False)

            MyLabel38.Visible = True
            fndReferenceNo.Visible = True
        Else
            EnableDisableControlByRefNoBase(True)
            MyLabel38.Visible = False
            fndReferenceNo.Visible = False
        End If
    End Sub
    Public Sub EnableDisableControlByRefNoBase(ByVal isVisibale As Boolean)
        fndTankerNo.Visible = isVisibale
        lblTankerNo.Visible = isVisibale
        lblQcInDateAndTime.Visible = isVisibale
        dtpQCInDateTime.Visible = isVisibale
        lblGateEntryNO.Visible = isVisibale
        fndGateEntryNo.Visible = isVisibale
        lblDateAndTime.Visible = isVisibale
        dtpGateEntryDateTime.Visible = isVisibale
        lblChallanNo.Visible = isVisibale
        txtChallanNo.Visible = isVisibale
        lblChallanDate.Visible = isVisibale
        dtpChallanDate.Visible = isVisibale
        lblDipValue.Visible = isVisibale
        txtDipValue.Visible = isVisibale
        MyLabel1.Visible = isVisibale
        TxtDeductionAmount.Visible = isVisibale
        lblStatus.Visible = isVisibale
        lblStatusValue.Visible = isVisibale
        MyLabel2.Visible = isVisibale
        txtWeighmentNo.Visible = isVisibale
        MyLabel3.Visible = isVisibale
        dtpWeighmentDate.Visible = isVisibale
        lblCleaningTester.Visible = isVisibale
        txtCleaning.Visible = isVisibale
        lblVendor.Visible = isVisibale
        fndVendor.Visible = isVisibale
        lblVendorName.Visible = isVisibale
        lblLocation.Visible = isVisibale
        fndLocation.Visible = isVisibale
        lblLocationName.Visible = isVisibale
        BtnRead.Visible = isVisibale
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub reset(ByVal isLoad As Boolean)
        reset(isLoad, True)
    End Sub

    Sub reset(ByVal isLoad As Boolean, ByVal isResetQCFinder As Boolean)
        fndLocation.Enabled = True
        GetECOPro()
        'grpBulkProc.Visible = True
        If isResetQCFinder Then
            fndQcNo.Value = ""
        End If
        IsCalculateCLR = False
        btnChangeFatClr.Visible = False
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        btnReverse.Visible = False
        lblQcAcceptedOrRejected.Text = ""
        fndGateEntryNo.Value = ""
        dtpQCInDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpQCOutDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        fndTankerNo.Value = ""
        fndReferenceNo.Value = ""
        txtChallanNo.Text = ""
        txtDipValue.Text = ""
        dtpChallanDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        lblStatusValue.Text = ""
        txtWeighmentNo.Text = ""
        dtpWeighmentDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        fndVendor.Value = ""
        lblVendorName.Text = ""
        fndLocation.Value = ""
        lblLocationName.Text = ""
        ''richa Against Ticket No.BM00000003719 on 04/09/2014
        TxtDeductionAmount.Value = 0
        TxtDeductionAmount.Enabled = False
        ''-----------------------------
        If chkBulkMilkProc.IsChecked Then
            If Not clsfrmParameterMaster.isFATParmExist() Then
                Throw New Exception("FAT parameter Does not exist. Please make it first")
                'chkMccProc.IsChecked = True
            End If
            If Not clsfrmParameterMaster.isSNFParmExist() Then
                Throw New Exception("SNF parameter Does not exist. Please make it first")
                'chkMccProc.IsChecked = True
            End If
            'If (Not clsfrmParameterMaster.isCLRParmExist()) AndAlso (Not clsERPFuncationality.isCurrentUserMCC) Then
            '    Throw New Exception("CLR parameter Does not exist. Please make it first")
            '    'chkMccProc.IsChecked = True
            'End If
            'If Not clsfrmParameterMaster.isCFParmExist() AndAlso (Not clsERPFuncationality.isCurrentUserMCC) Then
            '    Throw New Exception("CF parameter Does not exist. Please make it first")
            '    'chkMccProc.IsChecked = True
            'End If
            lblVendor.Text = "Vendor "
            'RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Hidden
            'RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        ElseIf chkMccProc.IsChecked Then
            lblVendor.Text = "From MCC "
            'RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
        End If
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSendForApproval.Enabled = False
        btnSendForSeparation.Enabled = False
        btnPrint.Enabled = False
        btnSave.Text = "Save"
        loadBlankItemGrid()
        loadBlankParameterGrid()
        lblPending.Status = ERPTransactionStatus.Pending
        'RadPageView1.SelectedPage = RadPageViewPage1
        objSr.SetPortNameValues(cboComPort)
        cboECOPro.SelectedValue = 0 'Nothing
        clsPortSetting.GetMachineType(CboMachine)
        clsPortSetting.GetMachineType(CmbMachine)
        loadBlankGVPaperSeal()
        loadBlankGVManualSeal()
        GrpControlSample.Visible = chkMccProc.IsChecked
        txtDispControlSampleFAT.Text = ""
        txtDispControlSampleSNF.Text = ""
        txtRcptControlSampleFAT.Text = ""
        txtRcptControlSampleSNF.Text = ""
        isContractorJobWork = False
        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpQCInDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpQCOutDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpQCInDateTime.CustomFormat = "dd/MM/yyyy"
            dtpQCOutDateTime.CustomFormat = "dd/MM/yyyy"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy"
        End If
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
        fndShift.Value = ""
        txtCleaning.Text = ""
        lblShiftName.Text = ""
        btnAmendment.Visible = False
    End Sub

    Private Sub chkBulkMilkProc_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBulkMilkProc.ToggleStateChanged
        reset(False, True)
    End Sub

    Private Sub chkMccProc_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMccProc.ToggleStateChanged
        reset(False, True)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        blnSave = True
        If allowToSave(False) Then
            If FinalChamberwise = 0 Then
                If clsCommon.myCdbl(gvParam.Rows(0).Cells(SNFColName).Value) <= 0 AndAlso clsCommon.myCdbl(gvParam.Rows(0).Cells(FATColName).Value) <= 0 Then
                    If clsCommon.MyMessageBoxShow(Me, "SNF Value is 0 and FAT value is 0, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        SaveData(False)
                    Else
                        Exit Sub
                    End If

                ElseIf clsCommon.myCdbl(gvParam.Rows(0).Cells(FATColName).Value) <= 0 Then
                    If clsCommon.MyMessageBoxShow(Me, "FAT Value is 0, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        SaveData(False)
                    Else
                        Exit Sub
                    End If

                ElseIf clsCommon.myCdbl(gvParam.Rows(0).Cells(SNFColName).Value) <= 0 Then
                    If clsCommon.MyMessageBoxShow(Me, "SNF Value is 0 , Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        SaveData(False)
                    Else
                        Exit Sub
                    End If
                Else

                    SaveData(False)
                End If
            Else
                If FinalChamberwise = 1 Then
                    Dim blnSave As Boolean = True
                    For ii As Integer = 0 To gvParam.Rows.Count - 1
                        If clsCommon.myCdbl(gvParam.Rows(ii).Cells(SNFColName).Value) <= 0 AndAlso clsCommon.myCdbl(gvParam.Rows(ii).Cells(FATColName).Value) <= 0 Then
                            If clsCommon.MyMessageBoxShow(Me, "SNF Value is 0 and FAT value is 0 , At Line No " + clsCommon.myCstr(ii + 1) + " Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                                blnSave = True
                            Else
                                Exit Sub
                            End If

                        ElseIf clsCommon.myCdbl(gvParam.Rows(ii).Cells(FATColName).Value) <= 0 Then
                            If clsCommon.MyMessageBoxShow(Me, "FAT Value is 0, At Line No" + clsCommon.myCstr(ii + 1) + " Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                                blnSave = True
                            Else
                                Exit Sub
                            End If

                        ElseIf clsCommon.myCdbl(gvParam.Rows(ii).Cells(SNFColName).Value) <= 0 Then
                            If clsCommon.MyMessageBoxShow(Me, "SNF Value is 0  At Line No" + clsCommon.myCstr(ii + 1) + ", Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                                blnSave = True
                            Else
                                Exit Sub
                            End If

                        End If
                    Next
                    SaveData(False)

                End If
            End If

        End If
        blnSave = False
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset(False, True)
    End Sub

    Sub loadBlankItemGrid()

        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing

        gvItem.Columns.Add(colSLNo, "SL. NO.")
        gvItem.Columns(colSLNo).Width = 60
        gvItem.Columns(colSLNo).ReadOnly = True

        gvItem.Columns.Add(colItemCode, "Item Code")
        gvItem.Columns(colItemCode).Width = 100
        gvItem.Columns(colItemCode).ReadOnly = True

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colHSN, "HSN Code")
        gvItem.Columns(colHSN).Width = 100
        gvItem.Columns(colHSN).ReadOnly = True

        gvItem.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItem.Columns(colChamberDesc).Width = 150
        gvItem.Columns(colChamberDesc).ReadOnly = True
        gvItem.Columns(colChamberDesc).IsVisible = False

        gvItem.Columns.Add(colMilkTypeCode, "Milk Type")
        gvItem.Columns(colMilkTypeCode).Width = 150
        ' gvItem.Columns(colMilkTypeCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        ' gvItem.Columns(colMilkTypeCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItem.Columns(colMilkTypeCode).ReadOnly = True
        gvItem.Columns(colMilkTypeCode).IsVisible = False

        gvItem.Columns.Add(colMilkGradeCode, "Milk grade")
        gvItem.Columns(colMilkGradeCode).Width = 150
        ' gvItem.Columns(colMilkGradeCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        ' gvItem.Columns(colMilkGradeCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItem.Columns(colMilkGradeCode).ReadOnly = True
        gvItem.Columns(colMilkGradeCode).IsVisible = False


        gvItem.Columns.Add(colQty, "Qty")
        gvItem.Columns(colQty).Width = 120
        gvItem.Columns(colQty).ReadOnly = True
        gvItem.Columns(colQty).IsVisible = False

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = True
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colUOM).IsVisible = False

        gvItem.Columns.Add(colFat, "FAT (%)")
        gvItem.Columns(colFat).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colFat).IsVisible = False
        gvItem.Columns(colFat).ReadOnly = True

        gvItem.Columns.Add(colSNF, "SNF (%)")
        gvItem.Columns(colSNF).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colSNF).IsVisible = False
        gvItem.Columns(colSNF).ReadOnly = True

        gvItem.Columns.Add(colFatKG, "FAT (KG)")
        gvItem.Columns(colFatKG).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colFatKG).IsVisible = False
        gvItem.Columns(colFatKG).ReadOnly = True

        gvItem.Columns.Add(colSNFKG, "SNF (KG)")
        gvItem.Columns(colSNFKG).Width = 75
        If chkBulkMilkProc.IsChecked Then gvItem.Columns(colSNFKG).IsVisible = False
        gvItem.Columns(colSNFKG).ReadOnly = True


        'gvItem.Columns.Add(colDipValue, "DIP Value")
        'gvItem.Columns(colDipValue).Width = 75
        'gvItem.Columns(colDipValue).ReadOnly = False
        If (TankerFromMaster = 1) Then
            gvItem.Columns(colChamberDesc).IsVisible = True
            gvItem.Columns(colMilkTypeCode).IsVisible = True
            gvItem.Columns(colMilkGradeCode).IsVisible = True
            gvItem.Columns(colQty).IsVisible = True
        End If


        gvItem.Columns.Add(colAdjustFAT, "Adjusted FAT (%)")
        gvItem.Columns(colAdjustFAT).Width = 250
        If RunBulkProcOnAdjustFATCLR = 0 Then gvItem.Columns(colAdjustFAT).IsVisible = False
        gvItem.Columns(colAdjustFAT).ReadOnly = False

        gvItem.Columns.Add(colAdjustSNF, "Adjusted SNF (%)")
        gvItem.Columns(colAdjustSNF).Width = 250
        If RunBulkProcOnAdjustFATCLR = 0 Then gvItem.Columns(colAdjustSNF).IsVisible = False
        gvItem.Columns(colAdjustSNF).ReadOnly = True

        gvItem.Columns.Add(colAdjustCLR, "Adjusted CLR")
        gvItem.Columns(colAdjustCLR).Width = 250
        If RunBulkProcOnAdjustFATCLR = 0 Then gvItem.Columns(colAdjustCLR).IsVisible = False
        gvItem.Columns(colAdjustCLR).ReadOnly = False

        gvItem.Columns.Add(colFromLocation, "From Location")
        gvItem.Columns(colFromLocation).Width = 120
        gvItem.Columns(colFromLocation).ReadOnly = True


        gvItem.Rows.AddNew()
        gvItem.Rows(0).Cells(colSLNo).Value = "1"

        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = False
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        ReStoreGridLayout()
    End Sub

    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

    Function LoadSealStatus() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all  select  'Yes' as  Value union all  select  'Change' as  Value union all  select  'N/A' as  Value   "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

    Function FillYesNoValue() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

    Sub loadBlankParameterGrid()
        Dim whrCls As String = String.Empty
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            gvParam.Rows.Clear()
            gvParam.Columns.Clear()
            gvParam.DataSource = Nothing
            Exit Sub
        End If

        If (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
            whrCls = " where 1=1 AND (Param_for='MCC' or Param_for='BOTH') "
        Else
            If (Not clsfrmParameterMaster.isCLRParmExist()) Then
                clsCommon.MyMessageBoxShow(Me, "CLR parameter Does not exist. Please make it first", Me.Text)
                Exit Sub
            End If
            If Not clsfrmParameterMaster.isCFParmExist() Then
                clsCommon.MyMessageBoxShow(Me, "CF parameter Does not exist. Please make it first", Me.Text)
                Exit Sub
            End If
            whrCls = " where 1=1 AND ( Param_for='PLANT' or Param_for='BOTH') "
        End If
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        ''richa 22 Sep,2016 BM00000009810
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") <> CompairStringResult.Equal Then
            whrCls += " AND Type NOT in ('AAB','ABB')"
        End If
        ''---------------------
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & " order by Ordering "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        Dim repoComboColumn As GridViewComboBoxColumn
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn = Nothing
        gvParam.Columns.Add("colSLNO", "SL. No.")
        gvParam.Columns("colSLNO").Width = 60
        gvParam.Columns("colSLNO").ReadOnly = True
        gvParam.Columns("colSLNO").Tag = "SLNO"
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
                If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then
                    repoDecimalColumn = New GridViewDecimalColumn()
                    repoDecimalColumn.Name = dt.Rows(i)("Code")
                    repoDecimalColumn.Width = 120
                    repoDecimalColumn.FormatString = "{0:n3}"

                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                        If settMODValueForFAT = 0 Then
                            repoDecimalColumn.FormatString = "{0:n2}"
                        End If

                    End If
                    repoDecimalColumn.DecimalPlaces = 3
                    repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                    repoDecimalColumn.Tag = dt.Rows(i)("Type")
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                        repoDecimalColumn.ReadOnly = True
                    Else
                        repoDecimalColumn.ReadOnly = False
                    End If

                    gvParam.MasterTemplate.Columns.Add(repoDecimalColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "A") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = OpenParameterValueList(dt.Rows(i)("Code"))
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                        repoComboColumn.ReadOnly = True
                    Else
                        repoComboColumn.ReadOnly = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoComboColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "B") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = FillYesNoValue()
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                        repoComboColumn.ReadOnly = True
                    Else
                        repoComboColumn.ReadOnly = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                        repoTextColumn.ReadOnly = True
                    Else
                        repoTextColumn.ReadOnly = False
                    End If
                    gvParam.MasterTemplate.Columns.Add(repoDecimalColumn)
                End If

                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    gvParam.Columns.Add(colAutoFat, "Auto FAT")
                    gvParam.Columns(colAutoFat).Width = 120
                    gvParam.Columns(colAutoFat).ReadOnly = True
                    gvParam.Columns(colAutoFat).Tag = "AutoFAT"
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    gvParam.Columns.Add(colAutoSnf, "Auto SNF")
                    gvParam.Columns(colAutoSnf).Width = 120
                    gvParam.Columns(colAutoSnf).ReadOnly = True
                    gvParam.Columns(colAutoSnf).Tag = "AutoSNF"
                End If
                If Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") = CompairStringResult.Equal Then
                        gvParam.Columns.Add(colAutoCLR, "Auto CLR")
                        gvParam.Columns(colAutoCLR).Width = 120
                        gvParam.Columns(colAutoCLR).ReadOnly = True
                        gvParam.Columns(colAutoCLR).Tag = "AutoCLR"
                    End If
                End If
            Next
            Try
                If Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
                    gvParam.Columns(CfColName).IsVisible = True
                Else
                    gvParam.Columns(CfColName).IsVisible = False
                End If
            Catch ex As Exception
            End Try

            Try
                If Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
                    gvParam.Columns(CLRColName).IsVisible = True
                Else
                    gvParam.Columns(CLRColName).IsVisible = False
                End If
            Catch ex As Exception

            End Try

            ''richa 22 Sep,2016 BM00000009810
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                gvParam.Columns.Add("ColDifference", "Difference")
                gvParam.Columns("ColDifference").Width = 300
                gvParam.Columns("ColDifference").ReadOnly = True
                gvParam.Columns("ColDifference").Tag = "Difference"
                gvParam.Columns("ColDifference").WrapText = True
            End If
            ''---------------------

            gvParam.Columns.Add("colRemarks", "Remarks")
            gvParam.Columns("colRemarks").Width = 300
            gvParam.Columns("colRemarks").ReadOnly = False
            gvParam.Columns("colRemarks").Tag = "REM"
            gvParam.Columns("colRemarks").WrapText = True

        End If
        Dim blnExit As Boolean = False
        If (TankerFromMaster = 1 And chkBulkMilkProc.IsChecked = True) OrElse (MCCChamberwise = 1 AndAlso chkMccProc.IsChecked = True) Then
            Dim ii As Integer = gvItem.Rows.Count
            Dim intCount As Integer = 0
            For intCount = 0 To ii - 1
                gvParam.Rows.AddNew()
                gvParam.Rows(intCount).Cells("colSLNO").Value = intCount + 1

                If clsCommon.myLen(CfColName) > 0 Then
                    Dim strGateEntryType As String = ""
                    If ItemwiseCorrectionFactoronQC = 0 Then
                        If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
                            strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
                        End If
                        If chkMccProc.IsChecked Then
                            gvParam.Rows(intCount).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, Nothing)
                        ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                            gvParam.Rows(intCount).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.PurchasedefaultCorrectionFactorBS, clsFixedParameterCode.PurchasedefaultCorrectionFactorBS, Nothing)
                        ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                            gvParam.Rows(intCount).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.JOBdefaultCorrectionFactorBS, clsFixedParameterCode.JOBdefaultCorrectionFactorBS, Nothing)
                        End If
                    Else
                        If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
                            strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
                        End If
                        Dim strItem As String = clsCommon.myCstr(gvItem.Rows(intCount).Cells(colItemCode).Value)
                        Dim CorrectionFactor As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Correction_Factor from TSPL_ITEM_MASTER where item_code='" & gvItem.Rows(intCount).Cells(colItemCode).Value & "'"))
                        If CorrectionFactor = 0 AndAlso blnExit = False Then
                            clsCommon.MyMessageBoxShow(Me, "Please add correction factor of item " & strItem & " on item master.", Me.Text)
                            blnExit = True
                            'Exit For
                        End If
                        gvParam.Rows(intCount).Cells(CfColName).Value = CorrectionFactor
                    End If

                End If
            Next
        Else
            gvParam.Rows.AddNew()
            gvParam.Rows(0).Cells("colSLNO").Value = "1"

            If clsCommon.myLen(CfColName) > 0 Then
                If PickCorrectionFactorProcurementTypewise = 0 Then
                    gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
                Else
                    Dim strGateEntryType As String = ""
                    If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
                        strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
                    End If
                    If chkMccProc.IsChecked Then
                        gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, Nothing)
                    ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                        gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.PurchasedefaultCorrectionFactorBS, clsFixedParameterCode.PurchasedefaultCorrectionFactorBS, Nothing)
                    ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                        gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.JOBdefaultCorrectionFactorBS, clsFixedParameterCode.JOBdefaultCorrectionFactorBS, Nothing)
                    Else
                        gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
                    End If
                End If
            End If
        End If

        Try
            If (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
                gvParam.Columns(CLRColName).IsVisible = False
                gvParam.Columns(CfColName).IsVisible = False
            Else
                gvParam.Columns(CLRColName).IsVisible = True
                gvParam.Columns(CfColName).IsVisible = True
            End If
        Catch ex As Exception

        End Try

        gvParam.AllowAddNewRow = False
        gvParam.AllowDeleteRow = False
        gvParam.AllowRowReorder = False
        gvParam.ShowGroupPanel = False
        gvParam.EnableFiltering = False
        gvParam.EnableSorting = False
        gvParam.EnableGrouping = False
        'gvParam.AutoSizeRows = True
        gvParam.AllowColumnChooser = True
        gvParam.AllowColumnReorder = True
        ReStoreGridLayout()
    End Sub

    Private Sub FrmQualityCheck_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
            If AllowAmendmentWithPasssword(MyBase.Form_ID, Nothing) Then
                btnAmendment.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                "tspl_Quality_check " + Environment.NewLine +
                                                "TSPL_Quality_Chember_Details ( Only in case of chamber wise setting ON) " + Environment.NewLine +
                                                "TSPL_QC_Parameter_Detail ( For QC Parameters) " + Environment.NewLine +
                                                "tspl_Qquality_check_History ( For History) " + Environment.NewLine +
                                                "TSPL_QC_Paper_Seal_Details ( For Paper Seal) " + Environment.NewLine +
                                                "TSPL_QC_Manual_Seal_Details ( For Manual Seal.) ")

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
        ElseIf e.KeyCode = Keys.F2 Then
            If (FinalChamberwise = 0) Then
                If gvParam.Rows.Count > 0 Then
                    gvParam.Rows(0).Cells(colAutoFat).Value = clsEkoPro.FAT
                    gvParam.Rows(0).Cells(colAutoSnf).Value = clsEkoPro.SNF
                    If clsCommon.myLen(CfColName) > 0 AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                        gvParam.Rows(0).Cells(colAutoCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(gvParam.Rows(0).Cells(colAutoFat).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(colAutoSnf).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(CfColName).Value))
                    Else
                        gvParam.Rows(0).Cells(colAutoCLR).Value = "0"

                    End If
                End If
            Else
                If gvParam.Rows.Count > 0 Then
                    gvParam.CurrentRow.Cells(colAutoFat).Value = clsEkoPro.FAT
                    gvParam.CurrentRow.Cells(colAutoSnf).Value = clsEkoPro.SNF
                    If clsCommon.myLen(CfColName) > 0 AndAlso (Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk)) Then
                        gvParam.CurrentRow.Cells(colAutoCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(gvParam.CurrentRow.Cells(colAutoFat).Value), clsCommon.myCdbl(gvParam.CurrentRow.Cells(colAutoSnf).Value), clsCommon.myCdbl(gvParam.CurrentRow.Cells(CfColName).Value))
                    Else
                        gvParam.CurrentRow.Cells(colAutoCLR).Value = "0"

                    End If
                End If

            End If
            'ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.A And chkBulkMilkProc.IsChecked And btnSendForApproval.Enabled Then
            '    Dim frm As New FrmPWD(Nothing)
            '    frm.strType = "SIRC"
            '    frm.strCode = "SIReversAndCreate"
            '    frm.ShowDialog()
            '    If frm.isPasswordCorrect Then
            '        If clsCommon.myLen(fndQcNo) <= 0 Then
            '            clsCommon.MyMessageBoxShow("Please select QC document to approve")
            '            Exit Sub
            '        End If
            '        Dim qry As String = " update TSPL_QUALITY_CHECK set is_Param_Accepted=2,isPosted=0 where QC_No ='" & fndQcNo.Value & "' and Isposted='1' and is_param_accepted=0 "
            '        clsDBFuncationality.ExecuteNonQuery(qry)
            '        loadData(fndQcNo.Value, "BulkProc", NavigatorType.Current)
            '        clsCommon.MyMessageBoxShow("Approved.")
            '    End If
        End If
    End Sub

    Sub LoadParameterinAnalyzer()
        Try
            LblVariable.TextAlign = ContentAlignment.TopCenter
            Dim Lbl_Zero As New common.Controls.MyLabel
            Lbl_Zero.Text = "00.00"
            With Lbl_Zero
                .ForeColor = Color.Red
                '.Font 
            End With
            If clsFixedParameter.GetData(clsFixedParameterType.DisplayAllParameterinQualityCheck, clsFixedParameterCode.DisplayAllParameterinQualityCheck, Nothing) = "1" Then
                grpEcoPro1.Visible = False
                GrpAdditional.Visible = True
                Dim Dt As DataTable = clsDBFuncationality.GetDataTable("select * from   TSPL_PARAMETER_RANGE_MASTER_QC where Show_in_Analyzer='YES' order by ANalyzer_Index")
                For Each row As DataRow In Dt.Rows
                    LblVariable.Text = clsCommon.myCstr(row.Item("Text_in_Analyzer")) & " " & Lbl_Zero.Text & "  "
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmQualityCheck)
        'If Not (MyBase.isReadFlag) Then
        '    If MDI.blnShowAllMenu = False Then
        '        Throw New Exception("Permission Denied")
        '    Else
        '        Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
        '    End If
        'End If
        btnPost.Visible = MyBase.isPostFlag
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        ' done by priti UDL/15/06/18-000188
        If MyBase.isModifyFlag OrElse MyBase.isPostFlag Then
            btnSendForApproval.Visible = True
        Else
            btnSendForApproval.Visible = False
        End If

        If MyBase.isAmendmentFlag Then
            btnAmendment.Enabled = True
        Else
            btnAmendment.Enabled = False
        End If

    End Sub

    Sub LoadGateEntryData(ByVal strGateEntryNo As String)
        Dim isFoundQc As String = String.Empty
        isFoundQc = clsDBFuncationality.getSingleValue("select QC_No from tspl_quality_check where gate_entry_no='" & strGateEntryNo & "'")
        If clsCommon.myLen(isFoundQc) > 0 Then
            loadData(isFoundQc, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)
            Exit Sub
        End If
        Dim objGt As New clsGateEntry()
        objGt = clsGateEntry.getData(strGateEntryNo, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)
        If objGt IsNot Nothing Then
            txtSubLocation.Value = objGt.Sublocation_Code
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            chkJobWork.Checked = IIf(objGt.IsAgainstJobWork = 1, True, False)
            fndGateEntryNo.Value = objGt.Gate_Entry_No
            dtpGateEntryDateTime.Value = objGt.Date_And_Time
            fndTankerNo.Value = objGt.Tanker_No
            fndReferenceNo.Value = objGt.Reference_No
            txtChallanNo.Text = objGt.Challan_No
            dtpChallanDate.Value = objGt.Challan_Date
            isContractorJobWork = False
            If chkBulkMilkProc.IsChecked Then
                If clsCommon.CompairString(objGt.Gate_Entry_Type, "J") = CompairStringResult.Equal Then
                    isContractorJobWork = True
                End If
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_weighment_Detail where  gate_entry_no='" & objGt.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
                txtWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MM/yyyy")
                lblStatusValue.Text = "Done"
            Else
                dtpWeighmentDate.Value = Nothing
                lblStatusValue.Text = "Not Done"
            End If
            fndLocation.Value = objGt.location_Code
            lblLocationName.Text = objGt.Location_Desc
            If chkBulkMilkProc.IsChecked Then
                fndVendor.Value = objGt.Vendor_Code
                lblVendorName.Text = objGt.Vendor_Desc
            Else
                fndVendor.Value = objGt.Dispatched_From_Mcc
                lblVendorName.Text = clsLocation.GetName(objGt.Dispatched_From_Mcc, Nothing)
                LoadSealDataManual()
                LoadSealDataPaper()
            End If

            If FinalChamberwise = 1 Then
                If objGt.Arr IsNot Nothing AndAlso objGt.Arr.Count > 0 Then
                    gvItem.Rows.Clear()
                    For Each objTr As clsGateEntryChemberNoDetails In objGt.Arr
                        gvItem.Rows.AddNew()
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSLNo).Value = objTr.Line_No
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkTypeCode).Value = objTr.MIKL_TYPE_CODE
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objGt.Qty_In_Kg / 100
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objGt.Qty_In_Kg / 100
                    Next
                End If

                Dim dtFromLocation As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_TANKER_GATE_OUT.MCC_CODE,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE2,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE3 from TSPL_MCC_TANKER_GATE_OUT left outer join tspl_mcc_dispatch_challan on tspl_mcc_dispatch_challan.Against_Gate_Out=TSPL_MCC_TANKER_GATE_OUT.gate_out_no where tspl_mcc_dispatch_challan.Chalan_NO='" & txtChallanNo.Text & "'")
                If dtFromLocation IsNot Nothing AndAlso dtFromLocation.Rows.Count > 0 Then
                    If clsCommon.myLen(dtFromLocation.Rows(0).Item("MCC_CODE")) > 0 Then
                        gvItem.Rows(0).Cells(colFromLocation).Value = clsCommon.myCstr(dtFromLocation.Rows(0).Item("MCC_CODE"))
                    End If
                    If clsCommon.myLen(dtFromLocation.Rows(0).Item("MCC_CODE2")) > 0 Then
                        gvItem.Rows(1).Cells(colFromLocation).Value = clsCommon.myCstr(dtFromLocation.Rows(0).Item("MCC_CODE2"))
                    End If
                    If clsCommon.myLen(dtFromLocation.Rows(0).Item("MCC_CODE3")) > 0 Then
                        gvItem.Rows(2).Cells(colFromLocation).Value = clsCommon.myCstr(dtFromLocation.Rows(0).Item("MCC_CODE3"))
                    End If
                End If


            Else
                gvItem.Rows(0).Cells(colItemCode).Value = objGt.Item_Code
                gvItem.Rows(0).Cells(colItemDesc).Value = objGt.Item_Desc
                gvItem.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objGt.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colUOM).Value = objGt.UOM
                gvItem.Rows(0).Cells(colQty).Value = objGt.Qty_In_Kg
                gvItem.Rows(0).Cells(colFat).Value = objGt.fat_per
                gvItem.Rows(0).Cells(colSNF).Value = objGt.snf_Per
                gvItem.Rows(0).Cells(colFatKG).Value = objGt.Qty_In_Kg * objGt.fat_per / 100
                gvItem.Rows(0).Cells(colSNFKG).Value = objGt.Qty_In_Kg * objGt.snf_Per / 100
            End If



            loadBlankParameterGrid()
            lblQcAcceptedOrRejected.Text = ""
            btnSendForApproval.Enabled = False
            If chkMccProc.IsChecked Then
                GrpControlSample.Visible = True
                txtDispControlSampleFAT.Text = clsQualityCheck.ControlSampleFAT(objGt.Gate_Entry_No)
                txtDispControlSampleSNF.Text = clsQualityCheck.ControlSampleSNF(objGt.Gate_Entry_No)
                txtRcptControlSampleFAT.Text = ""
                txtRcptControlSampleSNF.Text = ""
                If clsQualityCheck.isControlSample(objGt.Gate_Entry_No) Then
                    txtRcptControlSampleFAT.Enabled = True
                    txtRcptControlSampleSNF.Enabled = True
                Else
                    txtRcptControlSampleFAT.Enabled = False
                    txtRcptControlSampleSNF.Enabled = False
                End If
            Else
                GrpControlSample.Visible = False
                txtDispControlSampleFAT.Text = ""
                txtDispControlSampleSNF.Text = ""
                txtRcptControlSampleFAT.Text = ""
                txtRcptControlSampleSNF.Text = ""
            End If
        End If
        EnableDisable_SNFClR_BasisOnItem()
    End Sub
    ''richa ERO/31/07/21-001453
    Sub EnableDisable_SNFClR_BasisOnItem()
        Dim arritemlst As New ArrayList
        Dim dblCLRIndex As Integer = -1
        Dim dblSNFIndex As Integer = -1
        For i As Integer = 0 To gvItem.Rows.Count - 1
            Dim stritemCode As String = clsCommon.myCstr(gvItem.Rows(i).Cells(colItemCode).Value)
            Dim stris_QC_SNF_Based As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Is_QC_SNF_Based from Tspl_Item_Master where Item_Code='" & stritemCode & "'"))
            If clsCommon.CompairString(stris_QC_SNF_Based, "1") = CompairStringResult.Equal Then
                arritemlst.Add(clsCommon.myCstr(i))
            End If
        Next

        If arritemlst IsNot Nothing AndAlso arritemlst.Count > 0 Then
            For i = 0 To gvParam.Columns.Count - 1
                Dim Param_Type As String = clsCommon.myCstr(gvParam.Columns(i).Tag)
                If clsCommon.CompairString(Param_Type, "CLR") = CompairStringResult.Equal Then
                    gvParam.Columns(i).ReadOnly = False
                    dblCLRIndex = i
                End If
                If clsCommon.CompairString(Param_Type, "SNF") = CompairStringResult.Equal Then
                    gvParam.Columns(i).ReadOnly = False
                    dblSNFIndex = i
                End If
            Next
            For i As Integer = 0 To gvParam.Rows.Count - 1
                If dblCLRIndex > -1 Then
                    gvParam.Rows(i).Cells(dblCLRIndex).ReadOnly = False
                End If
                If dblSNFIndex > -1 Then
                    gvParam.Rows(i).Cells(dblSNFIndex).ReadOnly = True
                End If
            Next

            For i As Integer = 0 To arritemlst.Count - 1
                If dblCLRIndex > -1 Then
                    gvParam.Rows(arritemlst(i)).Cells(dblCLRIndex).ReadOnly = True
                End If
                If dblSNFIndex > -1 Then
                    gvParam.Rows(arritemlst(i)).Cells(dblSNFIndex).ReadOnly = False
                End If

            Next


            IsCalculateCLR = True
        Else
            IsCalculateCLR = False
        End If

    End Sub

    Sub LoadSealDataPaper()
        loadBlankGVPaperSeal()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select * from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details  where chalan_no='" & txtChallanNo.Text & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                gvPaperSeal.Rows.AddNew()
                gvPaperSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
                gvPaperSeal.Rows(i).Cells(colSealName).Value = clsCommon.myCstr(dt.Rows(i)("Seal_No"))
                gvPaperSeal.Rows(i).Cells(colSealStatus).Value = "Yes"
            Next
        End If
    End Sub

    Sub LoadSealDataManual()
        loadBlankGVManualSeal()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select xx.Seal_No as Seal_No ,xx.Chalan_NO as Challan_No  from(select Seal_No1 as Seal_No,Chalan_NO   from TSPL_Mcc_Dispatch_Challan union All select Seal_No2  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No3  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No4  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No5  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No6  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No7  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No8  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No9  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan union All select Seal_No10  as Seal_No,Chalan_NO  from TSPL_Mcc_Dispatch_Challan) xx where xx.Seal_No <>''  and xx.Chalan_NO='" & txtChallanNo.Text & "' ")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                gvManualSeal.Rows.AddNew()
                gvManualSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
                gvManualSeal.Rows(i).Cells(colSealName).Value = clsCommon.myCstr(dt.Rows(i)("Seal_No"))
                gvManualSeal.Rows(i).Cells(colSealStatus).Value = "Yes"
            Next
        End If
    End Sub

    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        'Dim whrcls As String = String.Empty
        'If Not clsMccMaster.isCurrentUserHO() Then
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        whrcls = "and  TSPL_gate_entry_details.location_code in ( " & objCommonVar.strCurrUserLocations & ")"
        '    End If
        'End If
        'fndGateEntryNo.Value = clsQualityCheck.getGateEntryFinder("TSPL_gate_entry_details.isPosted='1' and TSPL_gate_entry_details.Doc_Type='" & IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc") & "' and TSPL_gate_entry_details.Gate_Entry_No not in (select tspl_quality_check.Gate_Entry_No from tspl_quality_check where tspl_quality_check.gate_entry_no<>'" & fndGateEntryNo.Value & "' ) " & whrcls, fndGateEntryNo.Value, isButtonClicked)
        'If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
        '    LoadGateEntryData(fndGateEntryNo.Value)
        'Else
        '    reset(False, False)
        'End If
    End Sub

    Sub calculateBoilingDifference(ByVal intRow As Integer)
        Try
            Dim AABField As String = String.Empty
            Dim ABBField As String = String.Empty

            For i As Integer = 0 To gvParam.Columns.Count - 1

                If clsCommon.CompairString(gvParam.Columns(i).Tag, "AAB") = CompairStringResult.Equal Then
                    AABField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "ABB") = CompairStringResult.Equal Then
                    ABBField = gvParam.Columns(i).Name
                End If
                If clsCommon.myLen(AABField) > 0 AndAlso clsCommon.myLen(ABBField) > 0 Then
                    gvParam.Rows(intRow).Cells("colDifference").Value = clsCommon.myCdbl(gvParam.Rows(intRow).Cells(ABBField).Value) - clsCommon.myCdbl(gvParam.Rows(intRow).Cells(AABField).Value)
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Sub calculateSNF(ByVal intRow As Integer)
        Dim isParamOK As Boolean = True
        Dim snfField As String = ""
        Dim fatField As String = ""
        Dim clrField As String = ""
        Dim cfField As String = ""
        For i As Integer = 0 To gvParam.Columns.Count - 1
            If FinalChamberwise = 0 Then
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gvParam.Rows(0).Cells(i).Value) <= 0 Or (Not IsNumeric(gvParam.Rows(0).Cells(i).Value)) Then
                        isParamOK = False
                    End If
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    snfField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                    fatField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    clrField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                    cfField = gvParam.Columns(i).Name
                End If
            Else
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gvParam.Rows(intRow).Cells(i).Value) <= 0 Or (Not IsNumeric(gvParam.Rows(intRow).Cells(i).Value)) Then
                        isParamOK = False
                    End If
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    snfField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                    fatField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    clrField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                    cfField = gvParam.Columns(i).Name
                End If
            End If

        Next
        Try
            If FinalChamberwise = 0 Then
                If isParamOK Then
                    gvParam.Rows(0).Cells(snfField).Value = getSnfOnCalculation(clsCommon.myCdbl(gvParam.Rows(0).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(clrField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(cfField).Value))
                Else
                    gvParam.Rows(0).Cells(snfField).Value = 0
                End If
            Else
                If isParamOK Then
                    gvParam.Rows(intRow).Cells(snfField).Value = getSnfOnCalculation(clsCommon.myCdbl(gvParam.Rows(intRow).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(intRow).Cells(clrField).Value), clsCommon.myCdbl(gvParam.Rows(intRow).Cells(cfField).Value))
                Else
                    gvParam.Rows(intRow).Cells(snfField).Value = 0
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function getSnfOnCalculation(ByVal FatPer As Double, ByVal CLR As Double, ByVal CorrectionFactor As Double, Optional ByVal DeciPace As Integer = -1) As Double
        Dim ParameterForSNFatQC As Decimal = objCommonVar.ParameterForSNFatQC
        If ParameterForSNFatQC = 0 Then
            ParameterForSNFatQC = 0.2
        End If
        Dim SNF As Double = 0
        If DeciPace <> -1 Then
            FatPer = Microsoft.VisualBasic.Left(FatPer.ToString, InStr(0, FatPer.ToString, ".") + 1)
        End If
        SNF = CLR / 4 + ParameterForSNFatQC * FatPer + CorrectionFactor
        If ParameterForSNFatQC = 0.2 Then
            If settMODValueForFAT = 0 Then
                SNF = MyMath.RoundDown(SNF, 2)
            Else

                SNF = Math.Truncate(clsCommon.myCdbl(SNF) * 1000) / 1000

            End If

        Else
            If settMODValueForFAT = 0 Then
                SNF = Math.Round(SNF, 2, MidpointRounding.ToEven)
            Else
                SNF = Math.Truncate(clsCommon.myCdbl(SNF) * 1000) / 1000
            End If

        End If
        Return SNF
    End Function
    Sub calculateCLR(ByVal intRow As Integer)
        Dim isParamOK As Boolean = True
        Dim snfField As String = ""
        Dim fatField As String = ""
        Dim clrField As String = ""
        Dim cfField As String = ""
        For i As Integer = 0 To gvParam.Columns.Count - 1
            If FinalChamberwise = 0 Then
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gvParam.Rows(0).Cells(i).Value) <= 0 Or (Not IsNumeric(gvParam.Rows(0).Cells(i).Value)) Then
                        isParamOK = False
                    End If
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    snfField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                    fatField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    clrField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                    cfField = gvParam.Columns(i).Name
                End If
            Else
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gvParam.Rows(intRow).Cells(i).Value) <= 0 Or (Not IsNumeric(gvParam.Rows(intRow).Cells(i).Value)) Then
                        isParamOK = False
                    End If
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
                    snfField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
                    fatField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                    clrField = gvParam.Columns(i).Name
                End If
                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                    cfField = gvParam.Columns(i).Name
                End If
            End If

        Next
        Try
            If FinalChamberwise = 0 Then
                If isParamOK Then
                    gvParam.Rows(0).Cells(clrField).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(gvParam.Rows(0).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(snfField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(cfField).Value))
                Else
                    gvParam.Rows(0).Cells(clrField).Value = 0
                End If
            Else
                If isParamOK Then
                    gvParam.Rows(intRow).Cells(clrField).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(gvParam.Rows(intRow).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(intRow).Cells(snfField).Value), clsCommon.myCdbl(gvParam.Rows(intRow).Cells(cfField).Value))
                Else
                    gvParam.Rows(intRow).Cells(clrField).Value = 0
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function allowToSave(ByVal isPost As Boolean) As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpQCInDateTime.Value, Nothing) = False Then
                dtpQCInDateTime.Select()
                Return False
            End If
            '=======================================================
            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                Throw New Exception("Please enter Tanker No")
                errorControl.SetError(fndTankerNo, "Please enter Tanker No")
            Else
                errorControl.ResetError(fndTankerNo)
            End If
            If clsCommon.myLen(fndGateEntryNo.Value) <= 0 Then
                Throw New Exception("Please enter Gate Entry No")
                errorControl.SetError(fndGateEntryNo, "Please enter Gate Entry No")
            Else
                errorControl.ResetError(fndGateEntryNo)
            End If
            Dim isManadatory As Integer = 0
            Dim chk As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Tanker_Return from TSPL_gate_entry_details where gate_entry_no='" & fndGateEntryNo.Value & "'"))
            If chk = 1 Then
                Throw New Exception("You cannot Create QC.This tanker is Already Returned.")
            End If
            Dim NatureType As String = ""
            Dim IsCanType As Integer = 0
            Dim strAWRMColumn As String = ""
            Dim strBWRMColumn As String = ""
            Dim strDIFFRMColumn As String = ""
            Dim isCanTypefromTankerDispatch As Boolean = False
            Dim strItemQty As Double = 0
            Dim strItemCode As String = String.Empty
            For jj As Integer = 0 To gvParam.Rows.Count - 1
                Dim blnExit As Boolean = False
                For i As Integer = 0 To gvParam.Columns.Count - 1
                    Dim qry As String = "select IsMandatory,Nature,Type,IsCanType  from TSPL_PARAMETER_MASTER where Code='" & gvParam.Columns(i).Name & "'"
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        isManadatory = clsCommon.myCdbl(dtTemp.Rows(0)("IsMandatory"))
                        IsCanType = clsCommon.myCdbl(dtTemp.Rows(0)("IsCanType"))
                        NatureType = clsCommon.myCstr(dtTemp.Rows(0)("Nature"))

                        If chkMccProc.IsChecked Then
                            strItemCode = clsCommon.myCstr(gvItem.Rows(jj).Cells(colItemCode).Value)
                            strItemQty = clsCommon.myCdbl(gvItem.Rows(jj).Cells(colQty).Value)
                            isCanTypefromTankerDispatch = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isCanType from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No ='" & clsCommon.myCstr(txtChallanNo.Text) & "' and Item_code='" & clsCommon.myCstr(strItemCode) & "' and Qty_KG=" & clsCommon.myCdbl(strItemQty) & " ")) = 0, False, True)
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Type")), "AWRM") = CompairStringResult.Equal Then
                            strAWRMColumn = gvParam.Columns(i).Name
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Type")), "BWRM") = CompairStringResult.Equal Then
                            strBWRMColumn = gvParam.Columns(i).Name
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Type")), "DIFFRM") = CompairStringResult.Equal Then
                            strDIFFRMColumn = gvParam.Columns(i).Name
                        End If
                        If clsCommon.CompairString(NatureType, "R") = CompairStringResult.Equal Then

                            If isCanTypefromTankerDispatch = True Then
                                If isManadatory = 1 And IsCanType = 1 And clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) = 0 Then
                                    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            Else
                                If isManadatory = 1 And clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) = 0 Then
                                    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            End If

                        ElseIf clsCommon.CompairString(NatureType, "A") = CompairStringResult.Equal Then
                            If isCanTypefromTankerDispatch = True Then
                                If isManadatory = 1 And IsCanType = 1 And clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 Then
                                    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            Else
                                If isManadatory = 1 And clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 Then
                                    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            End If

                        ElseIf clsCommon.CompairString(NatureType, "B") = CompairStringResult.Equal Then
                            'If isManadatory = 1 And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "Yes") <> CompairStringResult.Equal) And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then 'OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then
                            '    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                            'End If

                            If isCanTypefromTankerDispatch = True Then
                                If isManadatory = 1 And IsCanType = 1 And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "Yes") <> CompairStringResult.Equal) And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then 'OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then
                                    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            Else
                                If isManadatory = 1 And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "Yes") <> CompairStringResult.Equal) And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then 'OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then
                                    Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                                End If
                            End If

                        End If

                        'If clsCommon.CompairString(NatureType, "R") = CompairStringResult.Equal Then
                        '    If isManadatory = 1 And clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) = 0 Then
                        '        Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                        '    End If
                        'ElseIf clsCommon.CompairString(NatureType, "A") = CompairStringResult.Equal Then
                        '    If isManadatory = 1 And clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 Then
                        '        Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                        '    End If
                        'ElseIf clsCommon.CompairString(NatureType, "B") = CompairStringResult.Equal Then
                        '    If isManadatory = 1 And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "Yes") <> CompairStringResult.Equal) And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then 'OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then
                        '        Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                        '    End If
                        'End If

                        'done by priti BHA/02/08/18-000211 for correction item wise from item master
                        If FinalChamberwise = 1 AndAlso clsCommon.myLen(fndQcNo.Value) = 0 Then
                            If ItemwiseCorrectionFactoronQC = 1 Then
                                Dim CFField As String = ""
                                If clsCommon.CompairString(gvParam.Columns(i).Tag, "CF") = CompairStringResult.Equal Then
                                    CFField = gvParam.Columns(i).Name
                                End If
                                If CFField <> "" Then
                                    Dim strGateEntryType As String = ""
                                    If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
                                        strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
                                    End If
                                    Dim strItem As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colItemCode).Value)
                                    Dim CorrectionFactor As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Correction_Factor from TSPL_ITEM_MASTER where item_code='" & strItem & "'"))
                                    If CorrectionFactor = 0 Then
                                        Throw New Exception("Please add correction factor of item " & strItem & " on item master.")
                                        Exit For
                                    End If
                                    gvParam.Rows(jj).Cells(CFField).Value = CorrectionFactor
                                    calculateSNF(jj)
                                End If
                            End If
                        End If
                    End If

                Next
                If blnExit = True Then
                    Exit For
                End If
            Next

            If clsCommon.myLen(strAWRMColumn) > 0 AndAlso clsCommon.myLen(strBWRMColumn) > 0 AndAlso clsCommon.myLen(strDIFFRMColumn) > 0 Then
                For jj As Integer = 0 To gvParam.Rows.Count - 1
                    gvParam.Rows(jj).Cells(strDIFFRMColumn).Value = (clsCommon.myCdbl(gvParam.Rows(jj).Cells(strBWRMColumn).Value) - clsCommon.myCdbl(gvParam.Rows(jj).Cells(strAWRMColumn).Value))
                Next
            End If



            If FinalChamberwise = 1 Then
                If RunBulkProcWithoutMilkGrade = 0 Then
                    If clsCommon.myLen(strParameterRejected) = 0 Then
                        strMGradeRejected = ""
                        SetAutoMilkGrade()
                        For ii As Integer = 0 To gvItem.Rows.Count - 1
                            If FinalChamberwise = 1 AndAlso IsItemMilkType = 1 Then
                                Dim strMilkGrade As String = gvItem.Rows(ii).Cells(colMilkGradeCode).Value
                                If clsCommon.myLen(strMGradeRejected) = 0 Then
                                    If clsCommon.myLen(strMilkGrade) = 0 Then
                                        strMGradeRejected = "Rejected"
                                        common.clsCommon.MyMessageBoxShow(Me, "W/o Milk Grade Rejected the Current Document", Me.Text)
                                        'If common.clsCommon.MyMessageBoxShow("W/o Milk Grade Rejected the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                                        '    strMGradeRejected = "Rejected"
                                        'Else
                                        '    Throw New Exception("Milk Grade is not mapped.Please Map milk grade.")
                                        'End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            '==========Sanjeet==============
            If RunBulkProcOnAdjustFATCLR = 1 AndAlso ChangeFATCLRafterspecialApprovalonQC = 0 Then
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    ' For ij As Integer = 0 To gvItem.Columns.Count - 1
                    If clsCommon.myCdbl(gvItem.Rows(ii).Cells(colAdjustFAT).Value) = 0 Then
                        Throw New Exception("Please Fill : " & gvItem.Columns(colAdjustFAT).HeaderText & " , It is Mandatory ")
                    End If
                    If clsCommon.myCdbl(gvItem.Rows(ii).Cells(colAdjustCLR).Value) = 0 Then
                        Throw New Exception("Please Fill : " & gvItem.Columns(colAdjustCLR).HeaderText & " , It is Mandatory ")
                    End If
                    'Next
                Next
            End If
            '========================================

            ''richa 22 Sep,2016 BM00000009810
            Dim whrCls As String = String.Empty
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                If (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
                    whrCls = " where 1=1 AND (Param_for='MCC' or Param_for='BOTH') "
                Else
                    whrCls = " where 1=1 AND ( Param_for='PLANT' or Param_for='BOTH') "
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & "  AND Type ='ABB' order by Ordering ")
                If dt.Rows.Count = 0 AndAlso dt IsNot Nothing Then
                    Throw New Exception("Please create parameter for Acid Before Boiling Type in Parameter Master")
                End If

                dt = clsDBFuncationality.GetDataTable(" select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master  " & whrCls & "  AND Type ='AAB' order by Ordering ")
                If dt.Rows.Count = 0 AndAlso dt IsNot Nothing Then
                    Throw New Exception("Please create parameter for Acid After Boiling Type in Parameter Master")
                End If

            End If
            ''---------------------

            'If dtpQCInDateTime.Value > dtpQCOutDateTime.Value Then
            '    Throw New Exception("'In Date Time' Can Not be Larger Then 'Out Date Time'")
            'End If
            '            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from TSPL_weighment_detail where gate_entry_no='" & fndGateEntryNo.Value & "' ")) <= 0 AndAlso clsCommon.myCdbl(txtDipValue.Text) <= 0 Then
            'txtDipValue.Focus()
            'Throw New Exception("Please fill Dip Value")
            'End If

            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            If TxtDeductionAmount.Enabled Then
                If clsCommon.myCdbl(TxtDeductionAmount.Value) <= 0 Then
                    Throw New Exception("Deduction amount cannot be left blank or 0")
                End If
            End If
            ''--------------------------------------------------
            If gvManualSeal IsNot Nothing AndAlso gvManualSeal.Rows.Count > 0 Then
                For i As Integer = 0 To gvManualSeal.Rows.Count - 1
                    If clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSLNo).Value) > 0 AndAlso clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealName).Value) > 0 AndAlso clsCommon.myLen(gvManualSeal.Rows(i).Cells(colSealStatus).Value) = 0 Then
                        Throw New Exception("Please select Seal status for Manual Seal at row no. " & (i + 1))
                    End If
                Next
            End If
            If gvPaperSeal IsNot Nothing AndAlso gvPaperSeal.Rows.Count > 0 Then
                For i As Integer = 0 To gvPaperSeal.Rows.Count - 1
                    If clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSLNo).Value) > 0 AndAlso clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSealName).Value) > 0 AndAlso clsCommon.myLen(gvPaperSeal.Rows(i).Cells(colSealStatus).Value) = 0 Then
                        Throw New Exception("Please select Seal status for Paper Seal at row no. " & (i + 1))
                    End If
                Next
            End If
            If Not clsQualityCheck.chkIsGridColumnHasTag(gvParam) Then
                Throw New Exception(" Grid's Column is not being recognized, Please delete layout and try loading Document Again ")
            End If
            'Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from tspl_quality_check where gate_entry_no='" & fndGateEntryNo.Value & "' and QC_NO <>'" & fndQcNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowQcDateAfterCurrentDate, Nothing)) = 0 Then
            '    Dim dt As Date = clsCommon.GETSERVERDATE()
            '    If clsCommon.myCDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
            '        dtpQCInDateTime.Value = dt
            '        Throw New Exception("QC In Date should not be Larger than current date")
            '    End If
            'End If
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowQcDateAfterCurrentDate, Nothing)) = 0 Then
            '    Dim dt As Date = clsCommon.GETSERVERDATE()
            '    If clsCommon.myCDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
            '        dtpQCOutDateTime.Value = dt
            '        Throw New Exception("QC Out Date should not be Larger than current date")
            '    End If
            'End If
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowQcDateBeforeGateEntryDateTime, Nothing)) = 0 Then
            '    If dtpGateEntryDateTime.Value > dtpQCInDateTime.Value Then
            '        Throw New Exception("QC in Date/Time should not be Smaller than Gate Entry Date/Time")
            '        dtpQCInDateTime.Focus()
            '    End If
            '    If dtpGateEntryDateTime.Value > dtpQCOutDateTime.Value Then
            '        Throw New Exception("QC Out Date/Time should not be Smaller than Gate Entry Date/Time")
            '        dtpQCOutDateTime.Focus()
            '    End If
            'End If
            '==================Added by preeti Gupta Against ticket No[]
            If clsCommon.GetDateWithStartTime(dtpQCInDateTime.Value) < clsCommon.GetDateWithStartTime(dtpWeighmentDate.Value) Then
                Throw New Exception("QC In Date can not be less than Weighment Date")
            End If

            If clsCommon.GetDateWithStartTime(dtpQCOutDateTime.Value) < clsCommon.GetDateWithStartTime(dtpWeighmentDate.Value) Then
                Throw New Exception("QC Out Date can not be less than Weighment Date")
            End If
            '===================================================================================================================
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            blnSave = False
            Return False
        End Try

    End Function

    Sub SaveData(ByVal isPost As Boolean)
        Dim trans As SqlTransaction = Nothing
        Try
            obj = New clsQualityCheck()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                If AllowBulkProcTransDateSameasGateEntryDate = 1 Then
                    dtpQCInDateTime.Value = dtpGateEntryDateTime.Value
                    dtpQCOutDateTime.Value = dtpGateEntryDateTime.Value
                End If
                'Dim Isjobwork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(IsAgainstJobWork,0) from tspl_gate_entry_details where gate_entry_no='" & fndGateEntryNo.Value & "'", trans))
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementCounterOnEntryType, clsFixedParameterCode.BulkProcurementCounterOnEntryType, trans)) = 0, False, True) ''Make Setting Balwinder
                If isPODocumentTypeWise AndAlso chkBulkMilkProc.IsChecked Then
                    Dim strGateEntryType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'", trans))
                    If clsCommon.myLen(strGateEntryType) <= 0 Then
                        Throw New Exception("Gate Entry Type not found on Gate Entry screen")
                    End If
                    If clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                        obj.QC_No = clsERPFuncationality.GetNextCode(trans, dtpQCInDateTime.Value, clsDocType.QualityCheck, clsDocTransactionType.BulkProcPurchase, fndLocation.Value)
                    ElseIf clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                        obj.QC_No = clsERPFuncationality.GetNextCode(trans, dtpQCInDateTime.Value, clsDocType.QualityCheck, clsDocTransactionType.BulkProcJobWork, fndLocation.Value)
                    Else
                        Throw New Exception("Wrong Gate Entry Type")
                    End If
                Else
                    If chkJobWork.Checked Then
                        obj.QC_No = clsERPFuncationality.GetNextCode(trans, dtpQCInDateTime.Value, clsDocType.QualityCheck, IIf(chkBulkMilkProc.IsChecked, clsDocTransactionType.BulkProcJobWorkOutward, clsDocTransactionType.MCCProcJobWorkOutward), txtSubLocation.Value)
                    Else
                        obj.QC_No = clsERPFuncationality.GetNextCode(trans, dtpQCInDateTime.Value, clsDocType.QualityCheck, IIf(chkBulkMilkProc.IsChecked, clsDocTransactionType.BulkProc, clsDocTransactionType.MccProc), fndLocation.Value)
                    End If
                End If
                If clsCommon.myLen(obj.QC_No) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Error In QC  No Genertion", Me.Text)
                    Exit Sub
                End If
            Else
                obj.QC_No = clsCommon.myCstr(fndQcNo.Value)
            End If

            obj.Shift_Code = clsCommon.myCstr(fndShift.Value)
            obj.Cleaning_Tester = clsCommon.myCstr(txtCleaning.Text)
            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            fndQcNo.Value = obj.QC_No
            obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNo.Value)
            obj.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(dtpGateEntryDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.QC_In_Date_Time = clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.QC_Out_Date_Time = clsCommon.GetPrintDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.location_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Location_Desc = clsCommon.myCstr(lblLocationName.Text)
            If chkBulkMilkProc.IsChecked Then
                obj.Doc_Type = "BulkProc"
                obj.Vendor_Code = clsCommon.myCstr(fndVendor.Value)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorName.Text)
            Else
                obj.Doc_Type = "MccProc"
                obj.Dispatched_From_Mcc_Code = clsCommon.myCstr(fndVendor.Value)
                obj.Dispatched_From_Mcc_Desc = clsCommon.myCstr(lblVendorName.Text)
            End If
            obj.Tanker_No = clsCommon.myCstr(fndTankerNo.Value)
            obj.Challan_No = clsCommon.myCstr(txtChallanNo.Text)
            obj.Challan_Date = clsCommon.GetPrintDate(dtpChallanDate.Value, "dd/MMM/yyyy")
            If clsCommon.myLen(txtWeighmentNo.Text) > 0 Then
                obj.Weighment_No = clsCommon.myCstr(txtWeighmentNo.Text)
                obj.Weighment_Date = clsCommon.myCDate(dtpWeighmentDate.Value, "dd/MMM/yyyy")
            End If
            obj.Remarks = clsCommon.myCstr(gvParam.Rows(0).Cells("colRemarks").Value)
            obj.Item_Code = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value)
            obj.Item_Desc = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemDesc).Value)
            obj.UOM = clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value)
            obj.Qty_In_Kg = clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value)
            obj.fat_per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFat).Value)
            obj.snf_Per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNF).Value)
            obj.snf_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value)
            obj.fat_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value)
            obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
            obj.Receipt_Control_FAT = clsCommon.myCdbl(txtRcptControlSampleFAT.Text)
            obj.Receipt_Control_SNF = clsCommon.myCdbl(txtRcptControlSampleSNF.Text)
            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            obj.DeductionAmount = clsCommon.myCdbl(TxtDeductionAmount.Value)
            '============Sanjeet=============
            obj.Adjust_fat_per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colAdjustFAT).Value)
            obj.Adjust_snf_Per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colAdjustSNF).Value)
            obj.Adjust_clr = clsCommon.myCdbl(gvItem.Rows(0).Cells(colAdjustCLR).Value)
            '==============================
            ''-----------------------------
            If Not isPost Then
                obj.isPosted = 0
            End If

            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            'If clsCommon.myCdbl(TxtDeductionAmount.Value) > 0 Then
            '    obj.isPosted = 0
            'End If
            ''--------------------------------------------------
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            ''richa agarwal remove settings as per Ranjana Mam 
            'If (FinalChamberwise = 1) Then
            obj.Arr = New List(Of clsQualityChemberNoDetails)
            For Each grow As GridViewRowInfo In gvItem.Rows
                Dim objTr As New clsQualityChemberNoDetails()
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSLNo).Value)
                objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.MILK_GRADE_CODE = clsCommon.myCstr(grow.Cells(colMilkGradeCode).Value)
                objTr.MIKL_TYPE_CODE = clsCommon.myCstr(grow.Cells(colMilkTypeCode).Value)
                objTr.Adjust_fat_per = clsCommon.myCdbl(grow.Cells(colAdjustFAT).Value)
                objTr.Adjust_snf_Per = clsCommon.myCdbl(grow.Cells(colAdjustSNF).Value)
                objTr.Adjust_clr = clsCommon.myCdbl(grow.Cells(colAdjustCLR).Value)
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If

            Next
            'End If
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim objParam As New clsQcParam
            obj.arrQcParam = New List(Of clsQcParam)
            If (FinalChamberwise = 0) Then
                For i = 0 To gvParam.Columns.Count - 1
                    If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colRemarks") = CompairStringResult.Equal Then
                    Else
                        objParam = New clsQcParam
                        objParam.LINE_NO = 1
                        objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                        objParam.Param_Field_Code = clsCommon.myCstr(gvParam.Columns(i).Name)
                        objParam.Param_Field_Desc = clsCommon.myCstr(gvParam.Columns(i).HeaderText)
                        objParam.Param_Field_Value = clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                        objParam.Remarks = clsCommon.myCstr(gvParam.Rows(0).Cells("colRemarks").Value)

                        objParam.Param_Type = clsCommon.myCstr(gvParam.Columns(i).Tag)
                        If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                            objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value))
                        End If
                        obj.arrQcParam.Add(objParam)
                    End If
                Next

            Else
                For j = 0 To gvParam.Rows.Count - 1
                    For i = 0 To gvParam.Columns.Count - 1
                        If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Then
                        Else
                            objParam = New clsQcParam
                            objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                            objParam.Param_Field_Code = clsCommon.myCstr(gvParam.Columns(i).Name)
                            objParam.Param_Field_Desc = clsCommon.myCstr(gvParam.Columns(i).HeaderText)
                            objParam.Param_Field_Value = clsCommon.myCstr(gvParam.Rows(j).Cells(i).Value)
                            objParam.Param_Type = clsCommon.myCstr(gvParam.Columns(i).Tag)
                            objParam.LINE_NO = clsCommon.myCstr(gvParam.Rows(j).Cells(0).Value)
                            objParam.Remarks = clsCommon.myCstr(gvParam.Rows(j).Cells("colRemarks").Value)
                            ''richa 23 Sep,2016 BM00000009810
                            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, trans)), "1") = CompairStringResult.Equal Then
                                objParam.BoilingDifference = clsCommon.myCdbl(gvParam.Rows(j).Cells("colDifference").Value)
                            End If
                            ''---------------------
                            If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                                If settMODValueForFAT = 0 Then
                                    objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(gvParam.Rows(j).Cells(i).Value))
                                Else
                                    objParam.Param_Field_Value = clsCommon.myCdbl(clsCommon.myCstr(gvParam.Rows(j).Cells(i).Value))
                                End If

                            End If

                            obj.arrQcParam.Add(objParam)
                        End If
                    Next
                Next
            End If

            If clsQualityCheck.saveData(obj, trans) Then
                If (chkMccProc.IsChecked) Then
                    If gvManualSeal IsNot Nothing AndAlso gvManualSeal.Rows.Count > 0 Then
                        Dim arrMSeal As List(Of clsQCManualSealDetail) = New List(Of clsQCManualSealDetail)
                        Dim objMSeal As clsQCManualSealDetail = Nothing
                        For i = 0 To gvManualSeal.Rows.Count - 1
                            objMSeal = New clsQCManualSealDetail()
                            objMSeal.Chalan_No = clsCommon.myCstr(txtChallanNo.Text)
                            objMSeal.Seal_No = clsCommon.myCstr(gvManualSeal.Rows(i).Cells(colSealName).Value)
                            objMSeal.Status = clsCommon.myCstr(gvManualSeal.Rows(i).Cells(colSealStatus).Value)
                            arrMSeal.Add(objMSeal)
                        Next
                        clsQCManualSealDetail.SaveData(arrMSeal, trans)
                    End If
                    If gvPaperSeal IsNot Nothing AndAlso gvPaperSeal.Rows.Count > 0 Then
                        Dim arrPSeal As List(Of clsQCPaperSealDetail) = New List(Of clsQCPaperSealDetail)
                        Dim objPSeal As clsQCPaperSealDetail = Nothing
                        For i = 0 To gvPaperSeal.Rows.Count - 1
                            objPSeal = New clsQCPaperSealDetail()
                            objPSeal.Chalan_No = clsCommon.myCstr(txtChallanNo.Text)
                            objPSeal.Seal_No = clsCommon.myCstr(gvPaperSeal.Rows(i).Cells(colSealName).Value)
                            objPSeal.Status = clsCommon.myCstr(gvPaperSeal.Rows(i).Cells(colSealStatus).Value)
                            arrPSeal.Add(objPSeal)
                        Next
                        clsQCPaperSealDetail.SaveData(arrPSeal, trans)
                    End If


                End If

                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                End If
                btnSave.Text = "Update"
                fndQcNo.MyReadOnly = True
                'btnDelete.Enabled = True
                btnPost.Enabled = True
                btnPrint.Enabled = True
                btnSendForApproval.Enabled = False
                loadData(obj.QC_No, obj.Doc_Type, NavigatorType.Current)
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btnPrint.Enabled = False
            btnSendForApproval.Enabled = False
            fndQcNo.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            blnSave = False

        End Try

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

        CmbEcoProAdditional.DataSource = dt
        CmbEcoProAdditional.ValueMember = "Code"
        CmbEcoProAdditional.DisplayMember = "Name"

    End Sub

    Sub loadData(ByVal str As String, ByVal docType As String, ByVal navtype As NavigatorType)
        If clsCommon.myLen(docType) > 0 Then
            obj = clsQualityCheck.getData(str, docType, navtype)
        Else
            obj = clsQualityCheck.getData(str, navtype)
        End If

        If obj IsNot Nothing Then
            If clsCommon.CompairString(obj.Doc_Type, "BulkProc") = CompairStringResult.Equal Then
                chkBulkMilkProc.IsChecked = True
            Else
                chkMccProc.IsChecked = True
            End If
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            txtSubLocation.Value = obj.Joblocation_Code
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            End If
            'reset(False, False)

            txtCleaning.Text = obj.Cleaning_Tester
            fndShift.Value = obj.Shift_Code
            If clsCommon.myLen(fndShift.Value) > 0 Then
                lblShiftName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select shift_name as Name from TSPL_SHIFT_MASTER where shift_code='" + fndShift.Value + "'"))
            End If
            fndLocation.Enabled = False
            fndQcNo.Value = obj.QC_No
            fndGateEntryNo.Value = obj.Gate_Entry_No
            dtpQCInDateTime.Value = obj.QC_In_Date_Time
            dtpQCOutDateTime.Value = obj.QC_Out_Date_Time
            dtpGateEntryDateTime.Value = obj.Gate_Entry_Date_And_Time
            fndTankerNo.Value = obj.Tanker_No
            fndReferenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
            txtChallanNo.Text = obj.Challan_No
            dtpChallanDate.Value = obj.Challan_Date
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_weighment_detail where gate_entry_no='" & obj.Gate_Entry_No & "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MM/yyyy")
                lblStatusValue.Text = "Done"
            Else
                dtpWeighmentDate.Value = Nothing
                lblStatusValue.Text = "Not Done"
            End If
            fndLocation.Value = obj.location_Code
            lblLocationName.Text = obj.Location_Desc

            If clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                fndVendor.Value = obj.Dispatched_From_Mcc_Code
                lblVendorName.Text = obj.Dispatched_From_Mcc_Desc
                Dim arrPSeal As List(Of clsQCPaperSealDetail) = clsQCPaperSealDetail.getData(txtChallanNo.Text)
                Dim arrMSeal As List(Of clsQCManualSealDetail) = clsQCManualSealDetail.getData(txtChallanNo.Text)
                If arrPSeal IsNot Nothing AndAlso arrPSeal.Count > 0 Then
                    loadBlankGVPaperSeal()
                    For i As Integer = 0 To arrPSeal.Count - 1
                        gvPaperSeal.Rows.AddNew()
                        gvPaperSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
                        gvPaperSeal.Rows(i).Cells(colSealName).Value = arrPSeal.Item(i).Seal_No
                        gvPaperSeal.Rows(i).Cells(colSealStatus).Value = arrPSeal.Item(i).Status
                    Next
                End If
                If arrMSeal IsNot Nothing AndAlso arrMSeal.Count > 0 Then
                    loadBlankGVManualSeal()
                    For i As Integer = 0 To arrMSeal.Count - 1
                        gvManualSeal.Rows.AddNew()
                        gvManualSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
                        gvManualSeal.Rows(i).Cells(colSealName).Value = arrMSeal.Item(i).Seal_No
                        gvManualSeal.Rows(i).Cells(colSealStatus).Value = arrMSeal.Item(i).Status
                    Next
                End If
            Else
                fndVendor.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Desc
            End If
            ''richa agarwal remove settings as per Ranjana Mam 
            loadBlankItemGrid()
            If FinalChamberwise = 0 Then
                gvItem.Rows(0).Cells(colSLNo).Value = "1"
                gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gvItem.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                gvItem.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colUOM).Value = obj.UOM
                gvItem.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                gvItem.Rows(0).Cells(colFat).Value = obj.fat_per
                gvItem.Rows(0).Cells(colSNF).Value = obj.snf_Per
                gvItem.Rows(0).Cells(colFatKG).Value = obj.Qty_In_Kg * obj.fat_per / 100
                gvItem.Rows(0).Cells(colSNFKG).Value = obj.Qty_In_Kg * obj.snf_Per / 100

                gvItem.Rows(0).Cells(colAdjustFAT).Value = obj.Adjust_fat_per
                gvItem.Rows(0).Cells(colAdjustSNF).Value = obj.Adjust_snf_Per
                gvItem.Rows(0).Cells(colAdjustCLR).Value = obj.Adjust_clr
            End If

            'If FinalChamberwise = 1 Then

            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                gvItem.Rows.Clear()
                For Each objTr As clsQualityChemberNoDetails In obj.Arr
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSLNo).Value = objTr.Line_No
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkGradeCode).Value = objTr.MILK_GRADE_CODE
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkTypeCode).Value = objTr.MIKL_TYPE_CODE
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAdjustFAT).Value = objTr.Adjust_fat_per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAdjustSNF).Value = objTr.Adjust_snf_Per
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAdjustCLR).Value = objTr.Adjust_clr
                Next
                Dim dtFromLocation As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_TANKER_GATE_OUT.MCC_CODE,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE2,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE3 from TSPL_MCC_TANKER_GATE_OUT left outer join tspl_mcc_dispatch_challan on tspl_mcc_dispatch_challan.Against_Gate_Out=TSPL_MCC_TANKER_GATE_OUT.gate_out_no where tspl_mcc_dispatch_challan.Chalan_NO='" & txtChallanNo.Text & "'")
                If dtFromLocation IsNot Nothing AndAlso dtFromLocation.Rows.Count > 0 Then
                    If clsCommon.myLen(dtFromLocation.Rows(0).Item("MCC_CODE")) > 0 Then
                        gvItem.Rows(0).Cells(colFromLocation).Value = clsCommon.myCstr(dtFromLocation.Rows(0).Item("MCC_CODE"))
                    End If
                    If clsCommon.myLen(dtFromLocation.Rows(0).Item("MCC_CODE2")) > 0 Then
                        gvItem.Rows(1).Cells(colFromLocation).Value = clsCommon.myCstr(dtFromLocation.Rows(0).Item("MCC_CODE2"))
                    End If
                    If clsCommon.myLen(dtFromLocation.Rows(0).Item("MCC_CODE3")) > 0 Then
                        gvItem.Rows(2).Cells(colFromLocation).Value = clsCommon.myCstr(dtFromLocation.Rows(0).Item("MCC_CODE3"))
                    End If
                End If
            End If
            'Else

            'End If
            txtDipValue.Text = obj.Dip_Value
            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            TxtDeductionAmount.Value = obj.DeductionAmount
            ''-----------------------------
            If clsCommon.myCdbl(txtDipValue.Text) <= 0 Then
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_quality_check where  gate_entry_no='" & obj.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
            End If

            If obj.arrQcParam IsNot Nothing Then
                loadBlankParameterGrid()
                If (FinalChamberwise = 0) Then
                    For i As Integer = 0 To obj.arrQcParam.Count - 1
                        Try
                            If obj.isPosted = 0 AndAlso (clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal) Then
                                If TypeOf (gvParam.Columns(obj.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                    gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value), 2)
                                Else
                                    gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(obj.arrQcParam(i).Param_Field_Value, 2)
                                End If
                            Else
                                If TypeOf (gvParam.Columns(obj.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                    gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value)
                                Else
                                    gvParam.Rows(0).Cells(obj.arrQcParam(i).Param_Field_Code).Value = obj.arrQcParam(i).Param_Field_Value
                                End If
                            End If

                        Catch exx As Exception
                        End Try

                    Next
                    gvParam.Rows(0).Cells("colRemarks").Value = obj.Remarks
                Else
                    'New work here
                    Dim ii As Integer = gvItem.Rows.Count
                    Dim intCount As Integer = 0

                    For i As Integer = 0 To obj.arrQcParam.Count - 1
                        Try
                            If obj.isPosted = 0 AndAlso (clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal) Then
                                If TypeOf (gvParam.Columns(obj.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                    If settMODValueForFAT = 0 Then
                                        gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells(obj.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value), 2)
                                    Else
                                        gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells(obj.arrQcParam(i).Param_Field_Code).Value = Math.Truncate(clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value) * 1000) / 1000
                                    End If

                                Else
                                        gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells(obj.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(obj.arrQcParam(i).Param_Field_Value, 2)
                                End If
                            Else
                                If TypeOf (gvParam.Columns(obj.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                    gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells(obj.arrQcParam(i).Param_Field_Code).Value = clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value)
                                Else
                                    gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells(obj.arrQcParam(i).Param_Field_Code).Value = obj.arrQcParam(i).Param_Field_Value
                                End If
                            End If
                            gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells("colRemarks").Value = obj.arrQcParam(i).Remarks
                            ''richa 23 Sep,2016 BM00000009810
                            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                                gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells("colDifference").Value = obj.arrQcParam(i).BoilingDifference
                            End If
                            ''---------------------



                        Catch exx As Exception
                            common.clsCommon.MyMessageBoxShow(Me, exx.Message, Me.Text)
                        End Try

                    Next


                End If

            End If

            If chkBulkMilkProc.IsChecked Then
                Dim strGateType = clsDBFuncationality.getSingleValue("select Gate_Entry_Type from  Tspl_Gate_Entry_Details where Gate_Entry_No='" & fndGateEntryNo.Value & "'")
                If obj.is_Param_accepted = 0 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC rejected"
                    btnSendForApproval.Enabled = True
                    If FinalChamberwise = 1 AndAlso clsCommon.CompairString(strGateType, "P") = CompairStringResult.Equal Then
                        btnSendForApproval.Enabled = False
                    End If
                    ''richa VIJ/14/10/19-000006
                    btnSendForSeparation.Enabled = True
                    If obj.is_QC_Separated = 1 Then
                        lblQcAcceptedOrRejected.Text = "QC Send For Separation"
                        btnSendForSeparation.Enabled = False
                    End If
                ElseIf obj.is_Param_accepted = 1 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted"
                    btnSendForApproval.Enabled = False
                ElseIf obj.is_Param_accepted = 2 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted with special approval"
                    btnSendForApproval.Enabled = False
                    ''richa Against Ticket No.BM00000003719 on 04/09/2014
                ElseIf obj.is_Param_accepted = 2 And obj.isPosted = 0 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted with special approval"
                    btnSendForApproval.Enabled = False
                    ''-------------------------------
                Else
                    lblQcAcceptedOrRejected.Text = ""
                    btnSendForApproval.Enabled = False
                End If
                If ChangeFATCLRafterspecialApprovalonQC = 1 AndAlso obj.is_Param_accepted = 2 Then
                    btnChangeFatClr.Visible = True
                Else
                    btnChangeFatClr.Visible = False
                End If
                ''richa VIJ/14/10/19-000006
            ElseIf chkMccProc.IsChecked = True And RejectiononQCforSeparationofBulkProcurementMCC = True Then
                btnSendForSeparation.Enabled = True
                If obj.is_QC_Separated = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC Send For Separation"
                    btnSendForSeparation.Enabled = False
                End If
            End If

            If chkMccProc.IsChecked Then
                GrpControlSample.Visible = True
                txtDispControlSampleFAT.Text = clsQualityCheck.ControlSampleFAT(obj.Gate_Entry_No)
                txtDispControlSampleSNF.Text = clsQualityCheck.ControlSampleSNF(obj.Gate_Entry_No)
                txtRcptControlSampleFAT.Text = obj.Receipt_Control_FAT
                txtRcptControlSampleSNF.Text = obj.Receipt_Control_SNF
                If clsQualityCheck.isControlSample(obj.Gate_Entry_No) Then
                    txtRcptControlSampleFAT.Enabled = True
                    txtRcptControlSampleSNF.Enabled = True
                Else
                    txtRcptControlSampleFAT.Enabled = False
                    txtRcptControlSampleSNF.Enabled = False
                End If
            Else
                GrpControlSample.Visible = False
                txtDispControlSampleFAT.Text = ""
                txtDispControlSampleSNF.Text = ""
                txtRcptControlSampleFAT.Text = ""
                txtRcptControlSampleSNF.Text = ""
            End If

            isContractorJobWork = False
            If chkBulkMilkProc.IsChecked Then
                Dim strGateEntryType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + obj.Gate_Entry_No + "'"))
                If clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                    isContractorJobWork = True
                End If
            End If
            EnableDisable_SNFClR_BasisOnItem()
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnPrint.Enabled = True
            If obj.isPosted = 1 Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            End If

            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            If clsCommon.CompairString(lblQcAcceptedOrRejected.Text, "QC accepted with special approval") = CompairStringResult.Equal And clsCommon.CompairString(lblPending.Status, ERPTransactionStatus.Approved) = CompairStringResult.Equal Then  'And obj.DeductionAmount > 0
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            ElseIf clsCommon.CompairString(lblQcAcceptedOrRejected.Text, "QC accepted with special approval") = CompairStringResult.Equal Then
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = False
                TxtDeductionAmount.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            Else
                TxtDeductionAmount.Enabled = False
            End If
            If isSentForApproval() Then
                lblQcAcceptedOrRejected.Text = "Sent For Special Approval"
                btnSendForApproval.Enabled = False
                btnPost.Enabled = False
            End If
            '-----------------------------------------------------
        End If
    End Sub

    Function isSentForApproval() As Boolean
        Dim rValue As Boolean = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_TRANSACTION_APPROVAL where Document_No='" & fndQcNo.Value & "' and Program_Code='" & clsUserMgtCode.frmQualityCheck & "' and Approve=0 and approval_type='Special Approval'")) = 1 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function

    Sub DeleteData()
        Dim arr As List(Of String) = New List(Of String)
        Dim trans As SqlTransaction = Nothing
        Try
            If clsCommon.myLen(fndQcNo.Value) > 0 Then
                If deleteConfirm() Then
                    trans = clsDBFuncationality.GetTransactin()
                    arr.Add(fndQcNo.Value)
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.frmQualityCheck, trans) Then
                        If clsQualityCheck.deleteData(fndQcNo.Value, trans) Then
                            clsQCPaperSealDetail.DeleteData(txtChallanNo.Text, trans)
                            clsQCManualSealDetail.DeleteData(txtChallanNo.Text, trans)
                            trans.Commit()
                            reset(False, True)
                            clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Could Not Deleted. Try Again", Me.Text)
                            trans.Rollback()
                        End If
                    End If

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select a QC No To delete", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    Function isParamOk(ByVal strValueList As String, ByVal value As String) As Boolean
        Dim strValue() As String = strValueList.Split(",")
        If strValue IsNot Nothing AndAlso strValue.Count > 0 Then
            For i As Integer = 0 To strValue.Count - 1
                If clsCommon.CompairString(strValue(i), value) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Function SetAutoMilkGrade() As String
        Dim i As Integer = 0
        Dim blnMilkGrade As Boolean = True
        Dim rvalue As String = String.Empty
        Dim q As String = String.Empty
        Dim dt As DataTable = Nothing
        For jj As Integer = 0 To gvParam.Rows.Count - 1
            q = "select MILK_GRADE_CODE,GRADE_TYPE from TSPL_MILK_GRADE_MASTER WHERE MILK_TYPE_CODE ='" & clsCommon.myCstr(gvItem.Rows(jj).Cells(colMilkTypeCode).Value) & "'  order by SequenceNo"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(q)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For ii As Integer = 0 To dt1.Rows.Count - 1
                    blnMilkGrade = True
                    q = "select MILK_GRADE_CODE,Parameter_Code,Upper_Range,Lower_Range,Status,Value1 from tspl_milk_grade_detail  where MILK_GRADE_CODE='" & clsCommon.myCstr(dt1.Rows(ii)("MILK_GRADE_CODE")) & "' "
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(q)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For kk As Integer = 0 To dt2.Rows.Count - 1

                            For i = 0 To gvParam.Columns.Count - 1
                                If clsCommon.CompairString(clsCommon.myCstr(dt2.Rows(kk)("Parameter_Code")), clsCommon.myCstr(gvParam.Columns(i).Name)) = CompairStringResult.Equal Then
                                    q = "select  case when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='R' then 1 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='B' then 2 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='A' then 3 else 0 end as Value_Type  from  TSPL_PARAMETER_MASTER where  TSPL_PARAMETER_MASTER.Code='" & clsCommon.myCstr(gvParam.Columns(i).Name) & "'"
                                    dt = clsDBFuncationality.GetDataTable(q)
                                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                        If dt.Rows(0)("Value_Type") = 1 Then
                                            If Not (clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) >= dt2.Rows(kk)("Lower_range") And clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) <= dt2.Rows(kk)("Upper_range")) Then
                                                blnMilkGrade = False
                                            End If
                                            Exit For
                                        ElseIf dt.Rows(0)("Value_Type") = 2 Then
                                            If Not (clsCommon.CompairString(dt2.Rows(kk)("Status"), clsCommon.myCstr(gvParam.Rows(jj).Cells(i).Value)) = CompairStringResult.Equal) Then
                                                blnMilkGrade = False
                                            End If
                                            Exit For
                                        ElseIf dt.Rows(0)("Value_Type") = 3 Then
                                            If Not isParamOk(dt2.Rows(kk)("Value1"), clsCommon.myCstr(gvParam.Rows(jj).Cells(i).Value)) Then
                                                blnMilkGrade = False
                                            End If
                                            Exit For
                                        End If
                                    End If
                                End If

                            Next

                        Next
                        If blnMilkGrade = True Then
                            gvItem.Rows(jj).Cells(colMilkGradeCode).Value = clsCommon.myCstr(dt1.Rows(ii)("MILK_GRADE_CODE"))
                            Exit For
                        End If
                    End If
                Next
            End If

        Next

        If clsCommon.myLen(rvalue) > 0 Then
            Return rvalue
        Else
            Return "1"
        End If
    End Function

    Function chkParameterRange() As String
        Dim i As Integer = 0
        Dim rvalue As String = String.Empty
        Dim q As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim strlocation As String = ""
        Dim strProcType As String = ""
        If chkJobWork.Checked Then
            strlocation = txtSubLocation.Value
        Else
            strlocation = fndLocation.Value
        End If
        If CheckParameterRangerProcurementTypewise = 0 Then
            strProcType = ""
        Else
            If chkBulkMilkProc.IsChecked Then
                strProcType = " and Procurement_Type = 'C'"
            ElseIf chkMccProc.IsChecked Then
                strProcType = " and Procurement_Type = 'M'"
            End If
        End If

        If FinalChamberwise = 0 Then
            For i = 0 To gvParam.Columns.Count - 1
                If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colRemarks") = CompairStringResult.Equal Then
                Else
                    q = "select TSPL_PARAMETER_RANGE_MASTER.*, case when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='R' then 1 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='B' then 2 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='A' then 3 else 0 end as Value_Type  from TSPL_PARAMETER_RANGE_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.IsReject=1 and CONVERT (date, Effective_Date,103) <= '" & clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy") & "'  and TSPL_PARAMETER_RANGE_MASTER.Code='" & clsCommon.myCstr(gvParam.Columns(i).Name) & "' and TSPL_PARAMETER_RANGE_MASTER.vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & fndVendor.Value & "' ") & "' and TSPL_PARAMETER_RANGE_MASTER.Loc_Code='" & strlocation & "' " & strProcType & " order by Effective_Date desc "

                    dt = clsDBFuncationality.GetDataTable(q)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        If dt.Rows(0)("Value_Type") = 1 Then
                            If Not (clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value) >= dt.Rows(0)("Lower_range") And clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value) <= dt.Rows(0)("Upper_range")) Then
                            Else
                                rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Must Not Be  Upper Range: " & dt.Rows(0)("Upper_range") & ", Lower Range: " & dt.Rows(0)("Lower_range") & ", QC Value: " & clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value)
                            End If
                        ElseIf dt.Rows(0)("Value_Type") = 2 Then
                            If Not (clsCommon.CompairString(dt.Rows(0)("Status"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) = CompairStringResult.Equal) Then
                            Else
                                rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Status Should Not Be , Parameter Status: " & dt.Rows(0)("Status") & ", QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                            End If
                        ElseIf dt.Rows(0)("Value_Type") = 3 Then
                            If Not isParamOk(dt.Rows(0)("Condition_Value"), clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)) Then
                            Else
                                rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Value Should Not Be In : ( " & dt.Rows(0)("Condition_Value") & " ), But QC Value: " & clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                            End If

                        End If

                    End If
                End If
            Next

        Else

            For jj As Integer = 0 To gvParam.Rows.Count - 1
                For i = 0 To gvParam.Columns.Count - 1
                    If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colRemarks") = CompairStringResult.Equal Then
                    Else
                        q = "select TSPL_PARAMETER_RANGE_MASTER.*, case when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='R' then 1 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='B' then 2 when ISNULL( TSPL_PARAMETER_MASTER.Nature,'')='A' then 3 else 0 end as Value_Type  from TSPL_PARAMETER_RANGE_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.IsReject=1 and CONVERT (date, Effective_Date,103) <= '" & clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy") & "'  and TSPL_PARAMETER_RANGE_MASTER.Code='" & clsCommon.myCstr(gvParam.Columns(i).Name) & "' and TSPL_PARAMETER_RANGE_MASTER.MIKL_TYPE_CODE='" & gvItem.Rows(jj).Cells(colMilkTypeCode).Value & "'  and TSPL_PARAMETER_RANGE_MASTER.Loc_Code='" & fndLocation.Value & "' " & strProcType & " order by Effective_Date desc "

                        dt = clsDBFuncationality.GetDataTable(q)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                            If dt.Rows(0)("Value_Type") = 1 Then
                                If Not (clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) >= dt.Rows(0)("Lower_range") And clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) <= dt.Rows(0)("Upper_range")) Then
                                Else
                                    rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Must Not Be  Upper Range: " & dt.Rows(0)("Upper_range") & ", Lower Range: " & dt.Rows(0)("Lower_range") & ", QC Value: " & clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value)
                                End If
                            ElseIf dt.Rows(0)("Value_Type") = 2 Then
                                If Not (clsCommon.CompairString(dt.Rows(0)("Status"), clsCommon.myCstr(gvParam.Rows(jj).Cells(i).Value)) = CompairStringResult.Equal) Then
                                Else
                                    rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Status Should Not Be , Parameter Status: " & dt.Rows(0)("Status") & ", QC Value: " & clsCommon.myCstr(gvParam.Rows(jj).Cells(i).Value)
                                End If
                            ElseIf dt.Rows(0)("Value_Type") = 3 Then
                                If Not isParamOk(dt.Rows(0)("Condition_Value"), clsCommon.myCstr(gvParam.Rows(jj).Cells(i).Value)) Then
                                Else
                                    rvalue = rvalue & Environment.NewLine & clsCommon.myCstr(gvParam.Columns(i).HeaderText) & ": Parameter Value Should Not Be In : ( " & dt.Rows(0)("Condition_Value") & " ), But QC Value: " & clsCommon.myCstr(gvParam.Rows(jj).Cells(i).Value)
                                End If

                            End If

                        End If
                    End If
                Next

            Next
        End If

        If clsCommon.myLen(rvalue) > 0 Then
            Return rvalue
        Else
            Return "1"
        End If
    End Function

    Sub PostData()
        Dim str As String = String.Empty
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
                strParameterRejected = ""
                If Not chkMccProc.IsChecked Then
                    ''richa Against Ticket No.BM00000003719 on 04/09/2014
                    If clsCommon.CompairString(lblQcAcceptedOrRejected.Text, "QC accepted with special approval") <> CompairStringResult.Equal Then

                        str = chkParameterRange()
                        If clsCommon.CompairString(str, "1") = CompairStringResult.Equal Then
                        ElseIf clsCommon.CompairString(str, "-1") = CompairStringResult.Equal Then
                            Throw New Exception("Some Of the Parameter Range Not Found in Master")
                        Else
                            If clsCommon.MyMessageBoxShow(Me, "Following Parameters Rejects The Milk, " & Environment.NewLine & str & Environment.NewLine & "Want To Continue Posting ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                                gvParam.Rows(0).Cells("colRemarks").Value = gvParam.Rows(0).Cells("colRemarks").Value & Environment.NewLine & "Rejection Remarks: " & str
                            Else
                                Exit Sub
                            End If
                        End If

                        ''richa Against Ticket No.BM00000003719 on 04/09/2014
                    Else
                        str = "2"
                    End If

                Else
                    ''richa VIJ/14/10/19-000006
                    If RejectiononQCforSeparationofBulkProcurementMCC = True AndAlso chkMccProc.IsChecked = True Then
                        str = chkParameterRange()
                        If clsCommon.CompairString(str, "1") = CompairStringResult.Equal Then
                        ElseIf clsCommon.CompairString(str, "-1") = CompairStringResult.Equal Then
                            Throw New Exception("Some Of the Parameter Range Not Found in Master")
                        Else
                            If clsCommon.MyMessageBoxShow(Me, "Following Parameters Rejects The Milk, " & Environment.NewLine & str & Environment.NewLine & "Want To Continue Posting ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                                'gvParam.Rows(0).Cells("colRemarks").Value = gvParam.Rows(0).Cells("colRemarks").Value & Environment.NewLine & "Rejection Remarks: " & str
                                str = "0"
                                gvParam.Rows(0).Cells("colRemarks").Value = "QC Rejected"
                            Else
                                Exit Sub
                            End If
                        End If
                    Else
                        str = "1"
                    End If
                End If
                If TankerFromMaster = 1 AndAlso chkBulkMilkProc.IsChecked = True AndAlso Not clsCommon.CompairString(str, "1") = CompairStringResult.Equal Then
                    strParameterRejected = "Rejected"
                End If

                If Not allowToSave(True) Then
                    Exit Sub
                End If
                Dim strRemarks As String = ""
                If gvParam.Rows.Count > 0 Then
                    strRemarks = gvParam.Rows(0).Cells("colRemarks").Value
                End If
                If TankerFromMaster = 1 AndAlso chkBulkMilkProc.IsChecked = True AndAlso clsCommon.CompairString(str, "1") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strMGradeRejected) > 0 Then
                        str = "0"
                        strRemarks = "Rejected due to Auto Grading not Mapped"
                    End If
                End If

                SaveData(True)
                If (clsQualityCheck.postData(fndQcNo.Value, strDocType, Me.Form_ID, Nothing)) Then
                    ''richa Against Ticket No.BM00000003719 on 04/09/2014
                    If clsCommon.CompairString(str, "2") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery(" update tspl_quality_check set is_Param_Accepted=2 where qc_no='" & fndQcNo.Value & "'")
                        msg = "Successfully Posted"
                    Else
                        clsDBFuncationality.ExecuteNonQuery(" update tspl_quality_check set Remarks='" & strRemarks & "', is_Param_Accepted=" & IIf(clsCommon.CompairString(str, "1") = CompairStringResult.Equal, 1, 0) & " where qc_no='" & fndQcNo.Value & "'")
                        msg = "Successfully Posted"
                    End If
                    ''-----------------------------------------
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
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                loadData(fndQcNo.Value, strDocType, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub funPrint(ByVal strDocNo As String, Optional ByVal docType As String = Nothing)
        Dim PrintTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToPrintTimeWithDocumentDate, clsFixedParameterCode.AllowToPrintTimeWithDocumentDate, Nothing)
        Try
            'Ticket No :  BHA/12/07/18-000150  By Prabhakar  for Bharat Print same as UDL
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                Dim strQuery As String = Nothing
                Dim strChamberQuery As String = Nothing
                Dim strCHAMBER_DESC As String = ""
                Dim dblCHAMBER_Count As Integer = 0
                strChamberQuery = "select STUFF((SELECT ',' + QUOTENAME(TSPL_Quality_Chember_Details.CHAMBER_DESC) as Alies_Name from TSPL_Quality_Chember_Details where TSPL_Quality_Chember_Details.QC_No='" + strDocNo + "' order by TSPL_Quality_Chember_Details.Line_No FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
                strCHAMBER_DESC = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strChamberQuery))
                dblCHAMBER_Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as aa from TSPL_Quality_Chember_Details where TSPL_Quality_Chember_Details.QC_No='" + strDocNo + "'"))
                Dim strRemarks As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT   STUFF((SELECT char(10) + (case when TSPL_QC_Parameter_Detail.Param_Field_Value='' then '' else QUOTENAME(convert(varchar,line_no)+'.'+TSPL_QC_Parameter_Detail.Param_Field_Value) end) as Alies_Name FROM TSPL_QC_Parameter_Detail where Param_Field_Code='colRemarks' and TSPL_QC_Parameter_Detail.QC_No='" + strDocNo + "' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
                strQuery = "select '" + strRemarks + "' as Remarks,TSPL_SHIFT_MASTER.SHIFT_NAME,tspl_company_master.Logo_Img,tspl_company_master.Logo_img2,FINAL.* from (select * from " & _
             " (" & _
             " select 1 as sn,TSPL_QUALITY_CHECK.Shift_Code,TSPL_QUALITY_CHECK.Cleaning_Tester,TSPL_QUALITY_CHECK.Qty_In_Kg,TSPL_QUALITY_CHECK.QC_NO,convert(varchar(15),tspl_quality_check.QC_In_Date_Time,103) as  Weighment_date,tspl_quality_check.QC_In_Date_Time,Tspl_Gate_Entry_Details.Date_And_Time as Ge_Date, " & _
            " TSPL_Weighment_Detail.TANKER_NO,TSPL_Quality_Chember_Details.CHAMBER_DESC ,'Quantity (In Kg)' as Param_Type,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name," & _
            " (case when TSPL_QUALITY_CHECK.Doc_Type='BulkProc' then TSPL_VENDOR_MASTER.Vendor_Code else TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Code end )as Vendor_Code ," & _
            " (case when  TSPL_QUALITY_CHECK.Doc_Type='MccProc' then TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Desc else TSPL_VENDOR_MASTER.Vendor_Name end )as Vendor_Name ," & _
            " 'Quantity (In Kg)' as Param_Field_Desc,convert(varchar(25),TSPL_Quality_Chember_Details.Chamber_Qty) as Param_Field_Value " & _
            " from   TSPL_QUALITY_CHECK  left outer join TSPL_Quality_Chember_Details on TSPL_QUALITY_CHECK.QC_No=TSPL_Quality_Chember_Details.QC_No " & _
            " left outer join TSPL_QC_Parameter_Detail  on TSPL_QC_Parameter_Detail.QC_No=TSPL_QUALITY_CHECK.QC_No and  TSPL_QC_Parameter_Detail.line_no=TSPL_Quality_Chember_Details.line_no " & _
            " LEFT OUTER JOIN Tspl_Gate_Entry_Details ON TSPL_QUALITY_CHECK.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No " & _
            " left outer join TSPL_Weighment_Detail ON TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No " & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Weighment_Detail.Vendor_Code" & _
            " LEFT OUTER JOIN TSPL_CONTRACT_TANKER_MASTER cm on cm.TANKER_CODE=Tspl_Gate_Entry_Details.Tanker_No " & _
            " LEFT OUTER JOIN TSPL_CONTRACT_TANKER_DETAIL  on TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE=Tspl_Gate_Entry_Details.Tanker_No " & _
            " LEFT OUTER JOIN TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=tspl_quality_check.comp_code " & _
            " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.code=TSPL_QC_Parameter_Detail.Param_Field_Code " & _
             " where IsForPrintOnQC =1 and tspl_quality_check.QC_No='" + strDocNo + "' AND TSPL_QUALITY_CHECK.Doc_Type='" + docType + "' " & _
            " union all " & _
            " select 2 as sn,TSPL_QUALITY_CHECK.Shift_Code,TSPL_QUALITY_CHECK.Cleaning_Tester ,TSPL_QUALITY_CHECK.Qty_In_Kg,TSPL_QUALITY_CHECK.QC_NO,convert(varchar(15),tspl_quality_check.QC_In_Date_Time,103) as  Weighment_date,tspl_quality_check.QC_In_Date_Time,Tspl_Gate_Entry_Details.Date_And_Time as Ge_Date, " & _
            " TSPL_Weighment_Detail.TANKER_NO,TSPL_Quality_Chember_Details.CHAMBER_DESC ,TSPL_QC_Parameter_Detail.Param_Type,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name," & _
            " (case when TSPL_QUALITY_CHECK.Doc_Type='BulkProc' then TSPL_VENDOR_MASTER.Vendor_Code else TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Code end )as Vendor_Code ," & _
            " (case when  TSPL_QUALITY_CHECK.Doc_Type='MccProc' then TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Desc else TSPL_VENDOR_MASTER.Vendor_Name end )as Vendor_Name ," & _
            " TSPL_QC_Parameter_Detail.Param_Field_Desc,convert(varchar(25),TSPL_QC_Parameter_Detail.Param_Field_Value) as Param_Field_Value " & _
            " from   TSPL_QUALITY_CHECK  left outer join TSPL_Quality_Chember_Details on TSPL_QUALITY_CHECK.QC_No=TSPL_Quality_Chember_Details.QC_No " & _
            " left outer join TSPL_QC_Parameter_Detail  on TSPL_QC_Parameter_Detail.QC_No=TSPL_QUALITY_CHECK.QC_No and  TSPL_QC_Parameter_Detail.line_no=TSPL_Quality_Chember_Details.line_no " & _
            " LEFT OUTER JOIN Tspl_Gate_Entry_Details ON TSPL_QUALITY_CHECK.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No " & _
            " left outer join TSPL_Weighment_Detail ON TSPL_Weighment_Detail.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No " & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Weighment_Detail.Vendor_Code" & _
            " LEFT OUTER JOIN TSPL_CONTRACT_TANKER_MASTER cm on cm.TANKER_CODE=Tspl_Gate_Entry_Details.Tanker_No " & _
            " LEFT OUTER JOIN TSPL_CONTRACT_TANKER_DETAIL  on TSPL_CONTRACT_TANKER_DETAIL.TANKER_CODE=Tspl_Gate_Entry_Details.Tanker_No " & _
            " LEFT OUTER JOIN TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=tspl_quality_check.comp_code " & _
            " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.code=TSPL_QC_Parameter_Detail.Param_Field_Code " & _
             " where IsForPrintOnQC =1 and tspl_quality_check.QC_No='" + strDocNo + "' AND TSPL_QUALITY_CHECK.Doc_Type='" + docType + "' " & _
              " union all " & _
              " select 3 as sn,'' as Shift_Code,'' as Cleaning_Tester ,0 as Qty_In_Kg,TSPL_Quality_Chember_Details.QC_NO,null as  Weighment_date,null as QC_In_Date_Time,null as Ge_Date, " & _
              " '' as TANKER_NO,TSPL_Quality_Chember_Details.Chamber_Desc as CHAMBER_DESC ,'GRADE' as Param_Type,'' as Comp_Code,'' as Comp_Name,'' as Vendor_Code , " & _
              " '' as Vendor_Name , 'GRADE' AS Param_Field_Desc,TSPL_Quality_Chember_Details.MILK_GRADE_CODE AS Param_Field_Value from TSPL_Quality_Chember_Details " & _
              " where QC_No='" + strDocNo + "' " & _
             " )as Main pivot ( max(Param_Field_Value) for CHAMBER_DESC in (" + strCHAMBER_DESC + ")) PIVT) as Final left join tspl_company_master on Final.Comp_Code=tspl_company_master.Comp_Code LEFT JOIN TSPL_SHIFT_MASTER on Final.Shift_Code=TSPL_SHIFT_MASTER.Shift_Code order by Sn "
                '" )as Main pivot ( max(Param_Field_Value) for CHAMBER_DESC in ([F],[R],[M])) PIVT) as Final left join tspl_company_master on Final.Comp_Code=tspl_company_master.Comp_Code order by Sn "
                '" AND TSPL_QC_Parameter_Detail.Param_Field_Code IN ('FAT','CLR','SNF','ABB','AAB','GRADE','CHHENA','NA-PPM','RM','SODIUM CONTENT') " & _
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal AndAlso dblCHAMBER_Count < 3 Then
                    If dblCHAMBER_Count > 0 Then
                        dt.Columns(dt.Columns.Count - dblCHAMBER_Count).ColumnName = "F"
                        dblCHAMBER_Count = dblCHAMBER_Count - 1
                    End If
                    If dblCHAMBER_Count > 0 Then
                        dt.Columns(dt.Columns.Count - dblCHAMBER_Count).ColumnName = "R"
                        dblCHAMBER_Count = dblCHAMBER_Count - 1
                    End If

                Else
                    If dblCHAMBER_Count > 0 Then
                        dt.Columns(dt.Columns.Count - dblCHAMBER_Count).ColumnName = "F"
                        dblCHAMBER_Count = dblCHAMBER_Count - 1
                    End If
                    If dblCHAMBER_Count > 0 Then
                        dt.Columns(dt.Columns.Count - dblCHAMBER_Count).ColumnName = "M"
                        dblCHAMBER_Count = dblCHAMBER_Count - 1
                    End If
                    If dblCHAMBER_Count > 0 Then
                        dt.Columns(dt.Columns.Count - dblCHAMBER_Count).ColumnName = "R"
                        dblCHAMBER_Count = dblCHAMBER_Count - 1
                    End If
                End If
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMCCMilkReceiptSlip", "Milk Transfer In")
                    frmCRV = Nothing
                End If
            Else

                Dim Qry As String = " select TSPL_QC_Parameter_Detail.LINE_NO, TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,  case when TSPL_QUALITY_CHECK.Doc_Type ='MccProc'  then TSPL_LOCATION_MASTER_For_MCC.GSTNO else  case when TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' then TSPL_VENDOR_MASTER.GSTFinalNo  end end as MCC_Vendor_GstInNo, case when TSPL_QUALITY_CHECK.Doc_Type ='MccProc'  then TSPL_STATE_MASTER_For_MCC_GST.GST_STATE_Code else  case when TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' then TSPL_STATE_MASTER_For_Vendor_GST.GST_STATE_Code end end as MCC_Vendor_GST_State_Code, Weighment_No ,TSPL_QUALITY_CHECK.remarks,case when ISNULL(weighment_no,'')='' then 'Not Done' else 'Done' end as Weighment_Status,convert(varchar,Weighment_Date,103)  as Weighment_Date,case when TSPL_QUALITY_CHECK.Doc_Type ='MccProc'  then TSPL_MCC_MASTER .MCC_NAME else  case when TSPL_QUALITY_CHECK.Doc_Type ='BulkProc' then TSPL_VENDOR_MASTER.Vendor_Name "
                Qry += " end end as MCC_Vendor_Name ,"

                Qry += " case when TSPL_QUALITY_CHECK.Doc_Type ='MccProc'  then"

                Qry += " TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end  + case when LEN(TSPL_CITY_MASTER_for_MCC.City_Name )>0 then ', '+TSPL_CITY_MASTER_for_MCC.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_for_MCC.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_for_MCC.STATE_NAME  else '' end +   "
                Qry += " case when LEN(TSPL_MCC_MASTER.Pin_code   )>0 then ', '+TSPL_MCC_MASTER.Pin_code  else ' ' end + case when len(TSPL_MCC_MASTER.Email   )>0 then ', ' +TSPL_MCC_MASTER.Email   else '' end  "
                Qry += "   else case when TSPL_QUALITY_CHECK.Doc_Type ='BulkProc'  then"

                Qry += " TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when len(TSPL_VENDOR_MASTER.add3)>0 then ', '+TSPL_VENDOR_MASTER.add3 else '' end  + case when LEN(TSPL_CITY_MASTER_for_Vendor.City_Name  )>0 then ', '+TSPL_CITY_MASTER_for_Vendor.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_for_Vendor.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_for_Vendor.STATE_NAME  else '' end +   "
                Qry += " case when LEN(TSPL_VENDOR_MASTER.Pin_code   )>0 then ', '+TSPL_VENDOR_MASTER.Pin_code  else ' ' end + case when len(TSPL_VENDOR_MASTER.Email   )>0 then ', ' +TSPL_VENDOR_MASTER.Email   else '' end end  end"

                Qry += " as MCC_Vendor_Add,TSPL_ITEM_MASTER .Item_Code ,TSPL_ITEM_MASTER .Item_Desc,isPosted,TSPL_QUALITY_CHECK.Dip_Value ,is_Param_Accepted,case when isPosted ='0' and is_Param_Accepted ='0' then 'Pending' else  case  when isPosted ='1' and is_Param_Accepted ='0' then 'Rejected' else case when isPosted ='0' and is_Param_Accepted = is_Param_Accepted then 'Pending' else  case when isPosted ='1' and is_Param_Accepted ='1' then 'Accepted'  else case when isPosted ='1' and is_Param_Accepted ='2' then 'Accepted with Special Approval' end end end end end  as ParameterAccepted , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name )>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then TSPL_STATE_MASTER.STATE_NAME  else '' end +   "
                Qry += " case when LEN(TSPL_COMPANY_MASTER.Tin_No  )>0 then ', '+TSPL_COMPANY_MASTER.Tin_No else ' ' end  "

                Qry += " as Comp_address ,TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,"

                Qry += " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name )>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Location.STATE_NAME  )>0 then TSPL_STATE_MASTER_For_Location.STATE_NAME  else '' end +   "
                Qry += " case when LEN(TSPL_LOCATION_MASTER.Tin_No  )>0 then ', '+TSPL_LOCATION_MASTER.Tin_No else ' ' end  "

                Qry += "  as Loc_address,TSPL_QUALITY_CHECK.QC_No "

                If PrintTime = "1" Then
                    Qry += " ,TSPL_QUALITY_CHECK.QC_In_Date_Time "
                    Qry += " ,TSPL_QUALITY_CHECK.QC_Out_Date_Time "
                    Qry += " ,TSPL_QUALITY_CHECK.Gate_Entry_Date_And_Time "
                Else
                    Qry += " ,Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) as QC_In_Date_Time"
                    Qry += " ,convert(varchar,TSPL_QUALITY_CHECK.QC_Out_Date_Time,103) as QC_Out_Date_Time  "
                    Qry += " ,convert(varchar,TSPL_QUALITY_CHECK.Gate_Entry_Date_And_Time,103) as Gate_Entry_Date_And_Time "
                End If



                Qry += "  ,TSPL_QUALITY_CHECK.Tanker_No ,TSPL_QUALITY_CHECK.Gate_Entry_No ,"

                Qry += "   TSPL_QUALITY_CHECK.Challan_No ,convert(varchar,TSPL_QUALITY_CHECK.Challan_Date,103) as Challan_Date,TSPL_QC_Parameter_Detail.Param_Field_Desc ,TSPL_QC_Parameter_Detail.Param_Field_Value,TSPL_QC_Parameter_Detail.Param_type "
                Qry += "   from TSPL_QUALITY_CHECK"
                Qry += " left outer join TSPL_QC_Parameter_Detail on TSPL_QC_Parameter_Detail.QC_No =TSPL_QUALITY_CHECK.QC_No "
                Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_QUALITY_CHECK.comp_code "
                Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_COMPANY_MASTER.City_Code "
                Qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State "
                Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_QUALITY_CHECK.location_Code "
                Qry += " left outer join TSPL_CITY_MASTER  as TSPL_CITY_MASTER_For_Location on TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.location_Code "
                Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Location on TSPL_STATE_MASTER_For_Location.STATE_CODE =TSPL_LOCATION_MASTER.State  "
                Qry += "  left outer join  TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_QUALITY_CHECK.Item_Code"
                Qry += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Code "
                Qry += " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_for_MCC on TSPL_CITY_MASTER_for_MCC .City_Code =TSPL_MCC_MASTER.City_code "
                Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_for_MCC on TSPL_STATE_MASTER_for_MCC .STATE_CODE =TSPL_MCC_MASTER.State_Code "
                Qry += " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Vendor_Code  =TSPL_QUALITY_CHECK .Vendor_Code"
                Qry += " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_for_Vendor on TSPL_CITY_MASTER_for_Vendor .City_Code =TSPL_VENDOR_MASTER.City_code "
                Qry += " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_for_Vendor on TSPL_STATE_MASTER_for_Vendor .STATE_CODE =TSPL_VENDOR_MASTER.State_Code"
                Qry += " left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.code=TSPL_QC_Parameter_Detail.Param_Field_Code"
                Qry += " left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Vendor_GST on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER_For_Vendor_GST.State_Code left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_MCC_GST on TSPL_MCC_MASTER.State_Code =TSPL_STATE_MASTER_For_MCC_GST.STATE_CODE left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_For_MCC on TSPL_LOCATION_MASTER_For_MCC.Location_Code =TSPL_MCC_MASTER.MCC_Code "
                Qry += "   where IsForPrintOnQC =1 and TSPL_QUALITY_CHECK.QC_No ='" + strDocNo + "'"
                'Qry += " and TSPL_QC_Parameter_Detail.Param_Field_Desc <>'AUTO SNF' and TSPL_QC_Parameter_Detail.Param_Field_Desc <>'Auto FAT' and TSPL_QC_Parameter_Detail.Param_Field_Desc <>'AUTO CLR'"

                Qry = " select xx.*, case when xx.Param_Type='NA'  then 1 when  xx.Param_Type='FAT' then 2 when xx.Param_Type='SNF' then 3 when xx.Param_Type='CLR' then 4 when xx.Param_Type='OTHERS' then 5 else 6 end as Ordering from ( " & Qry & " ) xx  order by ordering"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptQualityCheck", "Quality Check", clsCommon.myCDate(dt.Rows(0)("QC_In_Date_Time")))
                    frmCRV = Nothing
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(fndQcNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Doc not found to Print", Me.Text)
        Else
            funPrint(fndQcNo.Value, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"))
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub fndQcNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndQcNo._MYNavigator
        If chkBothDoc.Checked Then
            loadData(fndQcNo.Value, "", NavType)
        Else
            loadData(fndQcNo.Value, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavType)
        End If
    End Sub

    Private Sub fndQcNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndQcNo._MYValidating
        Dim whrCls As String = ""
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " And tspl_quality_check.location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        If chkBothDoc.Checked Then
            fndQcNo.Value = clsQualityCheck.getFinder(" 1=1 " & whrCls, fndQcNo.Value, isButtonClicked)
        Else
            fndQcNo.Value = clsQualityCheck.getFinder(" TSPL_QUALITY_CHECK.Doc_type='" & IIf(chkMccProc.IsChecked, "MccProc", "BulkProc") & "' " & whrCls, fndQcNo.Value, isButtonClicked)
        End If

        If clsCommon.myLen(fndQcNo.Value) > 0 Then

            If chkBothDoc.Checked Then
                loadData(fndQcNo.Value, "", NavigatorType.Current)
            Else
                loadData(fndQcNo.Value, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)
            End If
        End If
    End Sub


    Private Sub gvParam_CellValidated(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatedEventArgs) Handles gvParam.CellValidated
        If e.Column Is gvParam.Columns(FATColName) Then
            Dim fracValue1 As Double = 0
            If FinalChamberwise = 0 Then
                If clsCommon.myLen(FATColName) > 0 Then
                    fracValue1 = clsCommon.myCdbl(gvParam.Rows(0).Cells(FATColName).Value)
                    fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                    If settMODValueForFAT > 0 Then
                        If CInt(fracValue1) Mod settMODValueForFAT <> 0 Then
                            If Not isContractorJobWork Then
                                clsCommon.MyMessageBoxShow(Me, " FAT value in Grid, must have its decimal part multiple of 5", Me.Text)
                                gvParam.Rows(0).Cells(FATColName).Value = 0
                                gvParam.CurrentRow = gvParam.Rows(0)
                                gvParam.CurrentColumn = gvParam.Columns(FATColName)
                                'gvParam.Rows(0).Cells(FATColName).BeginEdit()
                            End If
                        End If
                    End If
                End If
                gvParam.Rows(0).Cells(FATColName).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(FATColName).Value)
            Else
                If clsCommon.myLen(FATColName) > 0 Then
                    fracValue1 = clsCommon.myCdbl(gvParam.CurrentRow.Cells(FATColName).Value)
                    fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                    If settMODValueForFAT > 0 Then
                        If CInt(fracValue1) Mod settMODValueForFAT <> 0 Then
                            If Not isContractorJobWork Then
                                clsCommon.MyMessageBoxShow(Me, " FAT value in Grid, must have its decimal part multiple of 5", Me.Text)
                                gvParam.CurrentRow.Cells(FATColName).Value = 0
                                gvParam.CurrentRow = gvParam.Rows(0)
                                gvParam.CurrentColumn = gvParam.Columns(FATColName)
                                'gvParam.Rows(0).Cells(FATColName).BeginEdit()
                            End If
                        End If
                    End If
                End If
                If settMODValueForFAT = 0 Then
                    gvParam.CurrentRow.Cells(FATColName).Value = clsCommon.myFormat(gvParam.CurrentRow.Cells(FATColName).Value)
                Else
                    gvParam.CurrentRow.Cells(FATColName).Value = Math.Truncate(clsCommon.myCdbl(gvParam.CurrentRow.Cells(FATColName).Value) * 1000) / 1000



                End If

            End If

        End If
        If e.Column Is gvParam.Columns(SNFColName) Then
            If FinalChamberwise = 0 Then
                gvParam.Rows(0).Cells(SNFColName).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(SNFColName).Value)
            Else
                gvParam.CurrentRow.Cells(SNFColName).Value = clsCommon.myFormat(gvParam.CurrentRow.Cells(SNFColName).Value)
            End If

        End If
    End Sub

    Private Sub gvParam_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvParam.CellValueChanged
        If blnSave = False Then
            If Not (clsERPFuncationality.isLocationMcc(fndLocation.Value) AndAlso Not SettCalculateSNFFromCLRForMCCMilk) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If clsCommon.CompairString(gvParam.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "SNF") = CompairStringResult.Equal Then

                        If IsCalculateCLR = True AndAlso clsCommon.CompairString(gvParam.CurrentColumn.Tag, "SNF") = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvParam.Rows(e.RowIndex).Cells(SNFColName).ReadOnly, False) = CompairStringResult.Equal Then
                            calculateCLR(e.RowIndex)
                        Else
                            calculateSNF(e.RowIndex)
                        End If
                        If FinalChamberwise = 0 Then
                            gvParam.Rows(0).Cells(FATColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(FATColName).Value, 2))
                            gvParam.Rows(0).Cells(SNFColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(SNFColName).Value, 2))
                        Else
                            If settMODValueForFAT = 0 Then
                                gvParam.CurrentRow.Cells(FATColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.CurrentRow.Cells(FATColName).Value, 2))
                                gvParam.CurrentRow.Cells(SNFColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.CurrentRow.Cells(SNFColName).Value, 2))
                            Else
                                gvParam.CurrentRow.Cells(FATColName).Value = Math.Truncate(clsCommon.myCdbl(gvParam.CurrentRow.Cells(FATColName).Value) * 1000) / 1000
                                gvParam.CurrentRow.Cells(SNFColName).Value = Math.Truncate(clsCommon.myCdbl(gvParam.CurrentRow.Cells(SNFColName).Value) * 1000) / 1000
                            End If

                        End If

                        If ChangeFATCLRafterspecialApprovalonQC = 1 Then
                            Dim CFValue As Double = 0
                            If FinalChamberwise = 0 Then
                                If clsCommon.CompairString(gvParam.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Then
                                    gvItem.Rows(0).Cells(colAdjustFAT).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(FATColName).Value, 2))
                                    gvItem.Rows(0).Cells(colAdjustSNF).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(SNFColName).Value, 2))
                                    gvItem.Rows(0).Cells(colAdjustCLR).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(CLRColName).Value, 2))
                                End If
                            End If
                        End If
                    End If

                    ''richa 22 Sep,2016 BM00000009810
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                        calculateBoilingDifference(e.RowIndex)
                    End If
                    ''---------------------
                    'Sanjay, Auto Remarks (Pick from Parameter value master)
                    If gvParam.Columns(e.ColumnIndex).GetType Is GetType(Telerik.WinControls.UI.GridViewComboBoxColumn) Then
                        Dim NatureType As String = ""
                        NatureType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Nature  from TSPL_PARAMETER_MASTER where Code='" & gvParam.Columns(e.ColumnIndex).Name & "'"))
                        If clsCommon.CompairString(NatureType, "A") = CompairStringResult.Equal Then
                            Dim ParametervalueSpecification As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Specification from tspl_Parameter_value_master where Parameter_CODE='" & clsCommon.myCstr(gvParam.Columns(e.ColumnIndex).Name) & "' and value='" & clsCommon.myCstr(gvParam.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) & "'"))
                            If clsCommon.myLen(ParametervalueSpecification) > 0 Then
                                gvParam.Rows(e.RowIndex).Cells("colRemarks").Value = clsCommon.myCstr(gvParam.Rows(e.RowIndex).Cells("colRemarks").Value) + IIf(clsCommon.myLen(clsCommon.myCstr(gvParam.Rows(e.RowIndex).Cells("colRemarks").Value)) > 0, ", ", "") + gvParam.Columns(e.ColumnIndex).HeaderText + "-" + ParametervalueSpecification
                            End If
                        End If
                    End If

                    isCellValueChangedOpen = False
                        'gvParam.AutoSizeRows = True
                    End If
                End If

        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsQualityCheck.ReverseAndUnpost(fndQcNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItem.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gvItem"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItem.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItem.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gvItem.MasterTemplate.FilterDescriptors.Clear()
                Dim obj1 As New clsGridLayout()
                obj1.ReportID = MyBase.Form_ID & "gvParam"
                obj1.UserID = objCommonVar.CurrentUserCode
                obj1.GridLayout = New MemoryStream()
                gvParam.SaveLayout(obj1.GridLayout)
                obj1.GridColumns = gvParam.ColumnCount
                obj1.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj1.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                ''stuti regarding memory leakage
                obj1.GridLayout.Close()
                obj1.GridLayout.Dispose()
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvItem", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvParam", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub mnuEmailSmsSetting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEmailSmsSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmQualityCheck
        frm.ShowDialog()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvItem", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItem.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItem.Columns.Count - 1 Step ii + 1
                        gvItem.Columns(ii).IsVisible = False
                        gvItem.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItem.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvParam", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvParam.ColumnCount Then
                    Dim ii As Integer
                    Dim arrTag As New Dictionary(Of String, Object)
                    For ii = 0 To gvParam.Columns.Count - 1 Step ii + 1
                        gvParam.Columns(ii).IsVisible = False
                        gvParam.Columns(ii).VisibleInColumnChooser = True
                        arrTag.Add(gvParam.Columns(ii).Name, gvParam.Columns(ii).Tag)
                    Next
                    gvParam.LoadLayout(obj.GridLayout)
                    For ii = 0 To gvParam.Columns.Count - 1
                        If arrTag.ContainsKey(gvParam.Columns(ii).Name) Then
                            gvParam.Columns(ii).Tag = arrTag(gvParam.Columns(ii).Name)
                        End If
                    Next
                     
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    'Private Sub SendEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmQualityCheck)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.QcNo, fndQcNo.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.inDateTime, clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.outDateTime, clsCommon.GetPrintDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.QcNo, fndQcNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.inDateTime, clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.outDateTime, clsCommon.GetPrintDate(dtpQCOutDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"))

    '        strbody = strbody.Replace(clsEmailSMSConstants.LocationCode, fndLocation.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.LocationName, lblLocationName.Text)

    '        strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, fndVendor.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)

    '        Dim strRptPath As String = ""
    '        ''''' It will be done after creating report
    '        'If obj.atchmnt = "Y" Then
    '        '    attachQry = GetAttachQry()
    '        '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachQry)
    '        '    If dt1.Rows.Count > 0 Then
    '        '        strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptShipment", "Shippment Detail")
    '        '    End If
    '        'End If

    '        Dim strPath As String = ""
    '        For Each strUser As String In lstUsers
    '            'lstUsers.Add(dr("User_Code").ToString())
    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '            End If

    '            strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '            lstReceiptents.Add(emailId)

    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '            clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strPath)
    '        Next

    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    'End Sub

    Private Sub btnSendForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click

        Try
            If clsCommon.myLen(fndQcNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select QC No. First", Me.Text)
                fndQcNo.Focus()
                Return
            End If

            'If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective QC No. " + fndQcNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
            '    Return
            'End If
            'loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)

            'Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            'Dim lstUsers As New List(Of String)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        lstUsers.Add(dr("User_Code").ToString())
            '    Next
            'End If

            'If lstUsers.Count = 0 Then
            '    Throw New Exception("No Receiptent Found")
            'End If
            'SendEmail(lstUsers, True)
            Dim objApprov As New ClsTransactionApproval
            objApprov.Document_No = fndQcNo.Value
            objApprov.Doc_Date = clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            objApprov.Approval_Type = "Special Approval"
            objApprov.Approve = 0
            objApprov.Program_Code = Me.Form_ID
            Dim qryp As String = "select Program_Name a   from TSPL_PROGRAM_MASTER where  Program_Code ='" & Me.Form_ID & "'"
            objApprov.Screen_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryp))
            ClsTransactionApproval.SaveData(objApprov, True)
            btnSendForApproval.Enabled = False
            clsCommon.MyMessageBoxShow(Me, "Document Sent For special Approval Successfully", Me.Text)
            loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Private Sub gvParam_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvParam.Validated

    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating

        Dim whrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = " and  TSPL_gate_entry_details.location_code in ( " & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        whrcls = whrcls & " and TSPL_gate_entry_details.gate_entry_no not in( select Gate_Entry_No  from TSPL_QUALITY_CHECK) "
        If FillGeneralWeighmentDetailsByJobworkTypeGateInNo = True Then
            whrcls = whrcls & " and TSPL_gate_entry_details.gate_entry_type<>'J' "
        End If

        If AllowbulkProcurementSequencewise = 1 Then
            If FirstQualityThenWeighment = 0 Then
                whrcls = whrcls & " and (TSPL_gate_entry_details.gate_entry_no  in( select Gate_Entry_No  from TSPL_Weighment_Detail) or TSPL_gate_entry_details.IsNetWeight=1) "
            End If
        Else
            If FirstQualityThenWeighment = 0 Then
                whrcls = whrcls & " and (TSPL_gate_entry_details.gate_entry_no  in( select Gate_Entry_No  from TSPL_Weighment_Detail)) "
            End If
        End If
        'whrcls = whrcls & " and TSPL_gate_entry_details.Tanker_Return=0 "
        whrcls = whrcls & " and TSPL_gate_entry_details.Tanker_Return=0  and tspl_gate_entry_details.In_Return=0  "
        Dim gtNo As String = fndTankerNo.Value
        Dim dt As DataRow
        dt = clsQualityCheck.getGateEntryFinder(gtNo, "TSPL_gate_entry_details.isPosted='1' and TSPL_gate_entry_details.Doc_Type='" & IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc") & "'  " & whrcls)
        If dt IsNot Nothing Then
            gtNo = dt("Gate Entry No")
        End If
        If clsCommon.myLen(gtNo) > 0 Then
            LoadGateEntryData(gtNo)
        Else
            reset(False, False)
        End If
    End Sub

    Sub loadBlankGVPaperSeal()
        gvPaperSeal.Rows.Clear()
        gvPaperSeal.Columns.Clear()
        gvPaperSeal.DataSource = Nothing

        gvPaperSeal.Columns.Add(colSLNo, "SL. No.")
        gvPaperSeal.Columns(colSLNo).ReadOnly = True
        gvPaperSeal.Columns(colSLNo).Width = 60

        gvPaperSeal.Columns.Add(colSealName, "Seal Desc")
        gvPaperSeal.Columns(colSealName).ReadOnly = True
        gvPaperSeal.Columns(colSealName).Width = 180

        Dim repoComboColumn As GridViewComboBoxColumn
        repoComboColumn = New GridViewComboBoxColumn()
        repoComboColumn.Name = colSealStatus
        repoComboColumn.Width = 120
        repoComboColumn.HeaderText = "Status"
        repoComboColumn.DataSource = LoadSealStatus()
        repoComboColumn.DisplayMember = "Value"
        repoComboColumn.ValueMember = "Value"
        repoComboColumn.ReadOnly = False
        gvPaperSeal.MasterTemplate.Columns.Add(repoComboColumn)
        'For i As Integer = 0 To 9
        '    gvPaperSeal.Rows.AddNew()
        '    gvPaperSeal.Rows(i).Cells(colSlNo).Value = (i + 1)
        '    gvPaperSeal.Rows(i).Cells(colSealName).Value = ""
        '    gvPaperSeal.Rows(i).Cells(colSealStatus).Value = ""
        'Next
        gvPaperSeal.AllowAddNewRow = False
        gvPaperSeal.AllowDeleteRow = False
        gvPaperSeal.AllowRowReorder = False
        gvPaperSeal.ShowGroupPanel = False
        gvPaperSeal.EnableFiltering = False
        gvPaperSeal.EnableSorting = False
        gvPaperSeal.EnableGrouping = False
    End Sub

    Sub loadBlankGVManualSeal()
        gvManualSeal.Rows.Clear()
        gvManualSeal.Columns.Clear()
        gvManualSeal.DataSource = Nothing

        gvManualSeal.Columns.Add(colSLNo, "SL. No.")
        gvManualSeal.Columns(colSLNo).ReadOnly = True
        gvManualSeal.Columns(colSLNo).Width = 60

        gvManualSeal.Columns.Add(colSealName, "Seal Desc")
        gvManualSeal.Columns(colSealName).ReadOnly = True
        gvManualSeal.Columns(colSealName).Width = 180

        Dim repoComboColumn As GridViewComboBoxColumn
        repoComboColumn = New GridViewComboBoxColumn()
        repoComboColumn.Name = colSealStatus
        repoComboColumn.Width = 120
        repoComboColumn.HeaderText = "Status"
        repoComboColumn.DataSource = LoadSealStatus()
        repoComboColumn.DisplayMember = "Value"
        repoComboColumn.ValueMember = "Value"
        repoComboColumn.ReadOnly = False
        gvManualSeal.MasterTemplate.Columns.Add(repoComboColumn)
        'For i As Integer = 0 To 9
        '    gvManualSeal.Rows.AddNew()
        '    gvManualSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
        '    gvManualSeal.Rows(i).Cells(colSealName).Value = ""
        '    gvManualSeal.Rows(i).Cells(colSealStatus).Value = ""
        'Next
        gvManualSeal.AllowAddNewRow = False
        gvManualSeal.AllowDeleteRow = False
        gvManualSeal.AllowRowReorder = False
        gvManualSeal.ShowGroupPanel = False
        gvManualSeal.EnableFiltering = False
        gvManualSeal.EnableSorting = False
        gvManualSeal.EnableGrouping = False
    End Sub

    Private Sub gvItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Dim CFValue As Double = 0
        If blnSave = False Then
            If FinalChamberwise = 1 Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colMilkTypeCode) AndAlso chkBulkMilkProc.IsChecked Then
                        gvItem.CurrentRow.Cells(colMilkTypeCode).Value = clsMilkTypeMaster.getFinder("", gvItem.CurrentRow.Cells(colMilkTypeCode).Value, False)
                        'ElseIf e.Column Is gvItem.Columns(colMilkGradeCode) AndAlso chkBulkMilkProc.IsChecked Then
                        '    gvItem.CurrentRow.Cells(colMilkGradeCode).Value = clsMilkGradeMaster.getFinder("", gvItem.CurrentRow.Cells(colMilkGradeCode).Value, False)
                    End If
                End If
                isCellValueChangedOpen = False
                '====Sanjeet=================
                '  If RunBulkProcOnAdjustFATCLR = 1 Then
                Dim strGateEntryType As String = ""
                If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
                    strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
                End If
                If chkMccProc.IsChecked Then
                    CFValue = clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, Nothing)
                ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                    CFValue = clsFixedParameter.GetData(clsFixedParameterType.PurchasedefaultCorrectionFactorBS, clsFixedParameterCode.PurchasedefaultCorrectionFactorBS, Nothing)
                ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                    CFValue = clsFixedParameter.GetData(clsFixedParameterType.JOBdefaultCorrectionFactorBS, clsFixedParameterCode.JOBdefaultCorrectionFactorBS, Nothing)
                End If

                'End If
                If e.Column Is gvItem.Columns(colAdjustFAT) Or e.Column Is gvItem.Columns(colAdjustCLR) Then
                    gvItem.CurrentRow.Cells(colAdjustSNF).Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvItem.CurrentRow.Cells(colAdjustFAT).Value), clsCommon.myCdbl(gvItem.CurrentRow.Cells(colAdjustCLR).Value), CFValue)
                End If
                '====================================
            Else
                ' If RunBulkProcOnAdjustFATCLR = 1 Then
                If PickCorrectionFactorProcurementTypewise = 0 Then
                    CFValue = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing))
                Else
                    Dim strGateEntryType As String = ""
                    If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
                        strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
                    End If
                    If chkMccProc.IsChecked Then
                        CFValue = clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, Nothing)
                    ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                        CFValue = clsFixedParameter.GetData(clsFixedParameterType.PurchasedefaultCorrectionFactorBS, clsFixedParameterCode.PurchasedefaultCorrectionFactorBS, Nothing)
                    ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                        CFValue = clsFixedParameter.GetData(clsFixedParameterType.JOBdefaultCorrectionFactorBS, clsFixedParameterCode.JOBdefaultCorrectionFactorBS, Nothing)
                    End If
                End If
                If e.Column Is gvItem.Columns(colAdjustFAT) Or e.Column Is gvItem.Columns(colAdjustCLR) Then
                    gvItem.Rows(0).Cells(colAdjustSNF).Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvItem.CurrentRow.Cells(colAdjustFAT).Value), clsCommon.myCdbl(gvItem.CurrentRow.Cells(colAdjustCLR).Value), CFValue)
                End If
                'End If
            End If
        End If
    End Sub

    Private Sub btnManualReject_Click(sender As Object, e As EventArgs) Handles btnManualReject.Click
        ManualReject()
    End Sub

    Sub ManualReject()
        Try
            If common.clsCommon.MyMessageBoxShow(Me, " Do You Want To Reject This Document ?", "Quality Check", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If (clsCommon.myLen(fndQcNo.Value) <= 0) Then
                    clsCommon.MyMessageBoxShow(Me, "QC No not found to Reject", Me.Text)
                    Exit Sub
                End If
                Dim strDocType As String = String.Empty
                If chkBulkMilkProc.IsChecked Then
                    strDocType = "BulkProc"
                ElseIf chkMccProc.IsChecked Then
                    strDocType = "MccProc"
                End If
                Dim obj As clsQualityCheck = clsQualityCheck.getData(fndQcNo.Value, strDocType, NavigatorType.Current, Nothing)
                If (obj Is Nothing OrElse clsCommon.myLen(obj.QC_No) <= 0) Then
                    Throw New Exception("No Data found to Post")
                End If
                Dim strRemarks As String = "Manual Rejected"
                If (obj.isPosted = 1) AndAlso obj.is_Param_accepted <> 0 Then
                    clsDBFuncationality.ExecuteNonQuery(" update tspl_quality_check set Remarks='" & strRemarks & "', is_Param_Accepted=0,is_Manual_Rejected=1,Manual_Rejected_Date_Time=getdate() where qc_no='" & fndQcNo.Value & "'")
                    clsCommon.MyMessageBoxShow(Me, "QC Rejected..", Me.Text)
                    loadData(fndQcNo.Value, strDocType, NavigatorType.Current)
                Else
                    clsCommon.MyMessageBoxShow(Me, "You can not Reject Unposted Document Manually..", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub UpdateAdjFATSNF()
        Dim trans As SqlTransaction = Nothing
        Try
            Dim qry As String = Nothing

            trans = clsDBFuncationality.GetTransactin()
            If FinalChamberwise = 0 Then
                qry = "update TSPL_QUALITY_CHECK set Adjusted_FAT='" & gvItem.Rows(0).Cells(colAdjustFAT).Value & "',Adjusted_SNF='" & gvItem.Rows(0).Cells(colAdjustSNF).Value & "',Adjusted_CLR='" & gvItem.Rows(0).Cells(colAdjustCLR).Value & "' where QC_No='" & fndQcNo.Value & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                For Each grow As GridViewRowInfo In gvItem.Rows
                    qry = "update TSPL_QUALITY_CHEMBER_DETAILS set Adjusted_FAT='" & grow.Cells(colAdjustFAT).Value & "',Adjusted_SNF='" & grow.Cells(colAdjustSNF).Value & "',Adjusted_CLR='" & grow.Cells(colAdjustCLR).Value & "' where QC_No='" & fndQcNo.Value & "' and Chamber_Desc='" & grow.Cells(colChamberDesc).Value & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    Private Sub btnChangeFatClr_Click(sender As Object, e As EventArgs) Handles btnChangeFatClr.Click
        UpdateAdjFATSNF()
    End Sub
    ''richa VIJ/14/10/19-000006 for send for Separtion after rejection of QC
    Private Sub btnSendForSeparation_Click(sender As Object, e As EventArgs) Handles btnSendForSeparation.Click
        Try
            If clsCommon.myLen(fndQcNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select QC No. First", Me.Text)
                fndQcNo.Focus()
                Return
            End If
            If clsCommon.MyMessageBoxShow(Me, "Do you want to send QC for separation", "Send for QC Separation", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim qry As String = "update tspl_quality_check set is_QC_Separated =1,QC_Separated_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' where QC_No='" & clsCommon.myCstr(fndQcNo.Value) & "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


  
    Private Sub fndShift__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndShift._MYValidating
        Dim str As String = "select shift_code as Code,shift_name as Name from TSPL_SHIFT_MASTER"
        fndShift.Value = clsShiftMaster.getFinder("", fndShift.Value, isButtonClicked)
        If clsCommon.myLen(fndShift.Value) > 0 Then
            lblShiftName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select shift_name as Name from TSPL_SHIFT_MASTER where shift_code='" + fndShift.Value + "'"))
        End If
    End Sub

    Private Sub BtnAmendment_Click(sender As Object, e As EventArgs) Handles btnAmendment.Click
        'Update only Parameter Value
        'Dim Reason As String = ""
        Dim trans As SqlTransaction = Nothing
        Try
            If lblPending.Status = ERPTransactionStatus.Approved Then
                Dim StrMessase As String = ""
                If chkBulkMilkProc.IsChecked = True Then
                    StrMessase = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SRN_NO from TSPL_Bulk_MILK_SRN where Qc_No='" + fndQcNo.Value + "'"))
                    If clsCommon.myLen(StrMessase) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Bulk SRN No - " + StrMessase + " is created against this QC.", Me.Text)
                        Exit Sub
                    End If
                ElseIf chkMccProc.IsChecked = True Then
                    StrMessase = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_Challan_No from tspl_milk_transfer_in where Qc_No='" + fndQcNo.Value + "'"))
                    If clsCommon.myLen(StrMessase) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Milk Transfer In " + StrMessase + " is created against this QC.", Me.Text)
                        Exit Sub
                    End If
                End If
                If common.clsCommon.MyMessageBoxShow(Me, "Do you want to make Amendment Qc Parameter", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    If Not allowToSave(True) Then
                        Exit Sub
                    End If

                    '' REASON FOR Amendment 
                    'Dim frm As New FrmFreeTxtBox1
                    'frm.Text = "Remarks for Amendment"
                    'frm.ShowDialog()
                    'If clsCommon.myLen(frm.strRmks) <= 0 Then
                    '    Exit Sub
                    'Else
                    '    Reason = frm.strRmks
                    'End If

                    trans = clsDBFuncationality.GetTransactin()
                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    obj = New clsQualityCheck()
                    obj.QC_No = fndQcNo.Value
                    Dim objParam As New clsQcParam
                    obj.arrQcParam = New List(Of clsQcParam)
                    If (FinalChamberwise = 0) Then
                        For i = 0 To gvParam.Columns.Count - 1
                            If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.Columns(i).Name, "colRemarks") = CompairStringResult.Equal Then
                            Else
                                objParam = New clsQcParam
                                objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                                objParam.Param_Field_Code = clsCommon.myCstr(gvParam.Columns(i).Name)
                                objParam.Param_Field_Desc = clsCommon.myCstr(gvParam.Columns(i).HeaderText)
                                objParam.Param_Field_Value = clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value)
                                objParam.Remarks = clsCommon.myCstr(gvParam.Rows(0).Cells("colRemarks").Value)

                                objParam.Param_Type = clsCommon.myCstr(gvParam.Columns(i).Tag)
                                If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                                    objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(gvParam.Rows(0).Cells(i).Value))
                                End If
                                obj.arrQcParam.Add(objParam)
                            End If
                        Next

                    Else
                        For j = 0 To gvParam.Rows.Count - 1
                            For i = 0 To gvParam.Columns.Count - 1
                                If clsCommon.CompairString(gvParam.Columns(i).Name, "colSLNO") = CompairStringResult.Equal Then
                                Else
                                    objParam = New clsQcParam
                                    objParam.QC_No = clsCommon.myCstr(obj.QC_No)
                                    objParam.Param_Field_Code = clsCommon.myCstr(gvParam.Columns(i).Name)
                                    objParam.Param_Field_Desc = clsCommon.myCstr(gvParam.Columns(i).HeaderText)
                                    objParam.Param_Field_Value = clsCommon.myCstr(gvParam.Rows(j).Cells(i).Value)
                                    objParam.Param_Type = clsCommon.myCstr(gvParam.Columns(i).Tag)
                                    objParam.LINE_NO = clsCommon.myCstr(gvParam.Rows(j).Cells(0).Value)
                                    objParam.Remarks = clsCommon.myCstr(gvParam.Rows(j).Cells("colRemarks").Value)
                                    ''richa 23 Sep,2016 BM00000009810
                                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, trans)), "1") = CompairStringResult.Equal Then
                                        objParam.BoilingDifference = clsCommon.myCdbl(gvParam.Rows(j).Cells("colDifference").Value)
                                    End If
                                    ''---------------------
                                    If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                                        objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(gvParam.Rows(j).Cells(i).Value))
                                    End If

                                    obj.arrQcParam.Add(objParam)
                                End If
                            Next
                        Next
                    End If


                    'saveCancelLog(Reason, "Amendment", trans)

                    If clsQcParam.saveData(obj.arrQcParam, obj.QC_No, trans) Then
                        trans.Commit()
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Amendmented", Me.Text)
                    End If

                    loadData(fndQcNo.Value, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)

                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndReferenceNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndReferenceNo._MYValidating
        Dim whrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = " and  TSPL_gate_entry_details.location_code in ( " & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        whrcls = whrcls & " and TSPL_gate_entry_details.gate_entry_no not in( select Gate_Entry_No  from TSPL_QUALITY_CHECK) "


        If AllowbulkProcurementSequencewise = 1 Then
            If FirstQualityThenWeighment = 0 Then
                whrcls = whrcls & " and (TSPL_gate_entry_details.gate_entry_no  in( select Gate_Entry_No  from TSPL_Weighment_Detail) or TSPL_gate_entry_details.IsNetWeight=1) "
            End If
        End If
        'whrcls = whrcls & " and TSPL_gate_entry_details.Tanker_Return=0 "
        whrcls = whrcls & " and TSPL_gate_entry_details.Tanker_Return=0  and tspl_gate_entry_details.In_Return=0  "
        Dim RefNo As String = fndReferenceNo.Value
        Dim gtNo As String = fndTankerNo.Value
        Dim dt As DataRow
        dt = clsQualityCheck.getRefrenceNoFinder(RefNo, "TSPL_gate_entry_details.isPosted='1' and len (isnull(TSPL_gate_entry_details.Reference_No,'')) > 0  and TSPL_gate_entry_details.Doc_Type='" & IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc") & "'  " & whrcls)
        If dt IsNot Nothing Then
            RefNo = dt("ReferenceNo")
            If clsCommon.myLen(RefNo) > 0 Then
                gtNo = clsDBFuncationality.getSingleValue("Select Gate_Entry_No from TSPL_gate_entry_details where TSPL_gate_entry_details.Reference_No = '" + RefNo + "' ")
            End If
        End If
        If clsCommon.myLen(gtNo) > 0 Then
            LoadGateEntryData(gtNo)
        Else
            reset(False, False)
        End If
    End Sub

    'Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
    '    Dim obj As New clsCancelLog
    '    obj.Program_Code = Form_ID
    '    obj.DOCUMENT_NO = clsCommon.myCstr(fndQcNo.Value)
    '    obj.REASON = Reason
    '    obj.ACTIVITY_TYPE = Activity_Type
    '    Return clsCancelLog.SaveData(obj, True, trans)
    'End Function
End Class
