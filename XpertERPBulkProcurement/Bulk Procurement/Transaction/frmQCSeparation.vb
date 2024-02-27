Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System
Public Class FrmQCSeparation
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ItemwiseCorrectionFactoronQC As Integer = 0
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
    Public Const colItemCode As String = "ItemCode"
    Public Const colHSN As String = "HSNCode"
    Public Const colItemDesc As String = "ItemDesc"
    Dim docType As String = String.Empty
    Public Shared isPortOpened As Boolean = False
    Dim obj As clsQualityCheck = Nothing
    Dim isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isForcellyStarted As Boolean = False
    Dim IsCanceled As Boolean = False
    Dim oldValue As Integer = 0
    Dim objEco As New clsEkoPro
    Dim objSr As New clsSerialPort
    Public CfColName As String = String.Empty
    Public FATColName As String = String.Empty
    Public SNFColName As String = String.Empty
    Public CLRColName As String = String.Empty
    Public Const colAutoFat As String = "AutoFat"
    Public Const colAutoSnf As String = "AutoSnf"
    Public Const colAutoCLR As String = "AutoCLR"
    Dim isContractorJobWork As Boolean = False
    Dim AllowManualRejection As Integer = 0
    Dim RunBulkProcOnAdjustFATCLR As Integer = 0
    Dim settMODValueForFAT As Integer = 0
    Dim SettCalculateSNFFromCLRForMCCMilk As Boolean = False
    '=======update by preeti gupta against tickrt no[ERO/06/07/19-000676]
#End Region

    Private Sub FrmQCSeparation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        settMODValueForFAT = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MODValueForFAT, clsFixedParameterCode.MODValueForFAT, Nothing))

        RunBulkProcOnAdjustFATCLR = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBulkProcOnAdjustedFATCLR, clsFixedParameterCode.RunBulkProcOnAdjustedFATCLR, Nothing))
        SettCalculateSNFFromCLRForMCCMilk = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateSNFFromCLRForMCCMilk, clsFixedParameterCode.CalculateSNFFromCLRForMCCMilk, Nothing)) = 1)
        If ChangeFATCLRafterspecialApprovalonQC = 1 And RunBulkProcOnAdjustFATCLR = 0 Then
            clsCommon.MyMessageBoxShow("Change FAT CLR setting is ON .Setting Run Bulk Proc On Adjust FAT CLR should be ON for this fuctionality.")
            Me.Close()
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
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
        mnuEmailSmsSetting.Visibility = ElementVisibility.Hidden
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
      
        If isResetQCFinder Then
            fndQcNo.Value = ""
        End If
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
            End If
            If Not clsfrmParameterMaster.isSNFParmExist() Then
                Throw New Exception("SNF parameter Does not exist. Please make it first")
            End If
           
            lblVendor.Text = "Vendor "
        ElseIf chkMccProc.IsChecked Then
            lblVendor.Text = "From MCC "
        End If
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnPrint.Enabled = False
        btnSave.Text = "Save"
        loadBlankParameterGrid()
        lblPending.Status = ERPTransactionStatus.Pending
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
        gvParam.Columns.Add(colSLNo, "SL. No.")
        gvParam.Columns(colSLNo).Width = 60
        gvParam.Columns(colSLNo).ReadOnly = True
        gvParam.Columns(colSLNo).Tag = "SLNO"


        gvParam.Columns.Add(colItemCode, "Item Code")
        gvParam.Columns(colItemCode).Width = 100
        gvParam.Columns(colItemCode).ReadOnly = False

        gvParam.Columns.Add(colItemDesc, "Item Desc")
        gvParam.Columns(colItemDesc).Width = 320
        gvParam.Columns(colItemDesc).ReadOnly = False

        gvParam.Columns.Add(colHSN, "HSN Code")
        gvParam.Columns(colHSN).Width = 100
        gvParam.Columns(colHSN).ReadOnly = False

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
                        repoDecimalColumn.FormatString = "{0:n2}"
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

            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                gvParam.Columns.Add("ColDifference", "Difference")
                gvParam.Columns("ColDifference").Width = 300
                gvParam.Columns("ColDifference").ReadOnly = True
                gvParam.Columns("ColDifference").Tag = "Difference"
                gvParam.Columns("ColDifference").WrapText = True
            End If

            gvParam.Columns.Add("colRemarks", "Remarks")
            gvParam.Columns("colRemarks").Width = 300
            gvParam.Columns("colRemarks").ReadOnly = False
            gvParam.Columns("colRemarks").Tag = "REM"
            gvParam.Columns("colRemarks").WrapText = True

        End If
        Dim blnExit As Boolean = False
        'If (TankerFromMaster = 1 And chkBulkMilkProc.IsChecked = True) OrElse (MCCChamberwise = 1 AndAlso chkMccProc.IsChecked = True) Then
        '    Dim ii As Integer = gvItem.Rows.Count
        '    Dim intCount As Integer = 0
        '    For intCount = 0 To ii - 1
        '        gvParam.Rows.AddNew()
        '        gvParam.Rows(intCount).Cells("colSLNO").Value = intCount + 1

        '        If clsCommon.myLen(CfColName) > 0 Then
        '            Dim strGateEntryType As String = ""
        '            If ItemwiseCorrectionFactoronQC = 0 Then
        '                If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
        '                    strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
        '                End If
        '                If chkMccProc.IsChecked Then
        '                    gvParam.Rows(intCount).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, Nothing)
        '                ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
        '                    gvParam.Rows(intCount).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.PurchasedefaultCorrectionFactorBS, clsFixedParameterCode.PurchasedefaultCorrectionFactorBS, Nothing)
        '                ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
        '                    gvParam.Rows(intCount).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.JOBdefaultCorrectionFactorBS, clsFixedParameterCode.JOBdefaultCorrectionFactorBS, Nothing)
        '                End If
        '            Else
        '                If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
        '                    strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
        '                End If
        '                'Dim strItem As String = clsCommon.myCstr(gvItem.Rows(intCount).Cells(colItemCode).Value)
        '                'Dim CorrectionFactor As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Correction_Factor from TSPL_ITEM_MASTER where item_code='" & gvItem.Rows(intCount).Cells(colItemCode).Value & "'"))
        '                'If CorrectionFactor = 0 AndAlso blnExit = False Then
        '                '    clsCommon.MyMessageBoxShow("Please add correction factor of item " & strItem & " on item master.", Me.Text)
        '                '    blnExit = True
        '                'End If
        '                'gvParam.Rows(intCount).Cells(CfColName).Value = CorrectionFactor
        '            End If

        '        End If
        '    Next
        'Else
        '    gvParam.Rows.AddNew()
        '    gvParam.Rows(0).Cells("colSLNO").Value = "1"

        '    If clsCommon.myLen(CfColName) > 0 Then
        '        If PickCorrectionFactorProcurementTypewise = 0 Then
        '            gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
        '        Else
        '            Dim strGateEntryType As String = ""
        '            If clsCommon.myLen(fndGateEntryNo.Value) > 0 AndAlso chkBulkMilkProc.IsChecked Then
        '                strGateEntryType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'"))
        '            End If
        '            If chkMccProc.IsChecked Then
        '                gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.MCCdefaultCorrectionFactorBS, clsFixedParameterCode.MCCdefaultCorrectionFactorBS, Nothing)
        '            ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
        '                gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.PurchasedefaultCorrectionFactorBS, clsFixedParameterCode.PurchasedefaultCorrectionFactorBS, Nothing)
        '            ElseIf chkBulkMilkProc.IsChecked AndAlso clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
        '                gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.JOBdefaultCorrectionFactorBS, clsFixedParameterCode.JOBdefaultCorrectionFactorBS, Nothing)
        '            Else
        '                gvParam.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
        '            End If
        '        End If
        '    End If
        'End If
        gvParam.Rows.AddNew()
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
        gvParam.AllowDeleteRow = True
        gvParam.AllowRowReorder = False
        gvParam.ShowGroupPanel = False
        gvParam.EnableFiltering = False
        gvParam.EnableSorting = False
        gvParam.EnableGrouping = False
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
                MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        End If
    End Sub


    Private Sub SetUserMgmtNew()
       
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Sub LoadQCData(ByVal strGateEntryNo As String)
        Dim isFoundQc As String = String.Empty
        isFoundQc = clsDBFuncationality.getSingleValue("select QC_No from TSPL_QC_SEPARATION where QC_NO='" & strGateEntryNo & "'")
        If clsCommon.myLen(isFoundQc) > 0 Then
            loadData(isFoundQc, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)
            Exit Sub
        End If
        Dim objQC As New clsQualityCheck()
        objQC = clsQualityCheck.getData(strGateEntryNo, IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc"), NavigatorType.Current)
        If objQC IsNot Nothing Then
            'txtSubLocation.Value = objQC.Sublocation_Code
            'lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            chkJobWork.Checked = IIf(objQC.IsAgainstJobWork = 1, True, False)
            fndGateEntryNo.Value = objQC.Gate_Entry_No
            'dtpGateEntryDateTime.Value = objQC.Date_And_Time
            fndTankerNo.Value = objQC.Tanker_No
            txtChallanNo.Text = objQC.Challan_No
            dtpChallanDate.Value = objQC.Challan_Date
            isContractorJobWork = False


            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_weighment_detail where gate_entry_no='" & objQC.Gate_Entry_No & "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtDipValue.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select dip_value from tspl_weighment_Detail where  gate_entry_no='" & objQC.Gate_Entry_No & "'"))
                If clsCommon.myCdbl(txtDipValue.Text) > 0 Then
                    txtDipValue.ReadOnly = True
                Else
                    txtDipValue.ReadOnly = False
                End If
                txtWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MM/yyyy")
                lblStatusValue.Text = "Done"
            Else
                lblStatusValue.Text = "Not Done"
            End If
            fndLocation.Value = objQC.location_Code
            lblLocationName.Text = objQC.Location_Desc
            If chkBulkMilkProc.IsChecked Then
                fndVendor.Value = objQC.Vendor_Code
                lblVendorName.Text = objQC.Vendor_Desc
            Else
                'fndVendor.Value = objQC.Dispatched_From_Mcc
                'lblVendorName.Text = clsLocation.GetName(objQC.Dispatched_From_Mcc, Nothing)
            End If

            loadBlankParameterGrid()
            lblQcAcceptedOrRejected.Text = ""
            If chkMccProc.IsChecked Then
                GrpControlSample.Visible = True
                txtDispControlSampleFAT.Text = clsQualityCheck.ControlSampleFAT(objQC.Gate_Entry_No)
                txtDispControlSampleSNF.Text = clsQualityCheck.ControlSampleSNF(objQC.Gate_Entry_No)
                txtRcptControlSampleFAT.Text = ""
                txtRcptControlSampleSNF.Text = ""
                If clsQualityCheck.isControlSample(objQC.Gate_Entry_No) Then
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
                    gvParam.Rows(0).Cells(snfField).Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvParam.Rows(0).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(clrField).Value), clsCommon.myCdbl(gvParam.Rows(0).Cells(cfField).Value))
                Else
                    gvParam.Rows(0).Cells(snfField).Value = 0
                End If
            Else
                If isParamOK Then
                    gvParam.Rows(intRow).Cells(snfField).Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gvParam.Rows(intRow).Cells(fatField).Value), clsCommon.myCdbl(gvParam.Rows(intRow).Cells(clrField).Value), clsCommon.myCdbl(gvParam.Rows(intRow).Cells(cfField).Value))
                Else
                    gvParam.Rows(intRow).Cells(snfField).Value = 0
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function allowToSave(ByVal isPost As Boolean) As Boolean
        Try
            If AllowFutureDateTransaction(dtpQCInDateTime.Value, Nothing) = False Then
                dtpQCInDateTime.Select()
                Return False
            End If
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
            For jj As Integer = 0 To gvParam.Rows.Count - 1
                Dim blnExit As Boolean = False
                For i As Integer = 0 To gvParam.Columns.Count - 1
                    isManadatory = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsMandatory  from TSPL_PARAMETER_MASTER where Code='" & gvParam.Columns(i).Name & "'"))
                    NatureType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Nature  from TSPL_PARAMETER_MASTER where Code='" & gvParam.Columns(i).Name & "'"))

                    If clsCommon.CompairString(NatureType, "R") = CompairStringResult.Equal Then
                        If isManadatory = 1 And clsCommon.myCdbl(gvParam.Rows(jj).Cells(i).Value) = 0 Then
                            Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                        End If
                    ElseIf clsCommon.CompairString(NatureType, "A") = CompairStringResult.Equal Then
                        If isManadatory = 1 And clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 Then
                            Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                        End If
                    ElseIf clsCommon.CompairString(NatureType, "B") = CompairStringResult.Equal Then
                        If isManadatory = 1 And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "Yes") <> CompairStringResult.Equal) And (clsCommon.myLen(gvParam.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then 'OrElse clsCommon.CompairString((gvParam.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then
                            Throw New Exception("Please Fill : " & gvParam.Columns(i).HeaderText & " , It is Mandatory ")
                        End If
                    End If

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
                                'Dim strItem As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colItemCode).Value)
                                'Dim CorrectionFactor As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Correction_Factor from TSPL_ITEM_MASTER where item_code='" & strItem & "'"))
                                'If CorrectionFactor = 0 Then
                                '    Throw New Exception("Please add correction factor of item " & strItem & " on item master.")
                                '    Exit For
                                'End If
                                'gvParam.Rows(jj).Cells(CFField).Value = CorrectionFactor
                                calculateSNF(jj)
                            End If
                        End If
                    End If
                Next
                If blnExit = True Then
                    Exit For
                End If
            Next


            If FinalChamberwise = 1 Then
                If RunBulkProcWithoutMilkGrade = 0 Then
                    If clsCommon.myLen(strParameterRejected) = 0 Then
                        strMGradeRejected = ""
                    End If
                End If
            End If
           
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

            If TxtDeductionAmount.Enabled Then
                If clsCommon.myCdbl(TxtDeductionAmount.Value) <= 0 Then
                    Throw New Exception("Deduction amount cannot be left blank or 0")
                End If
            End If
       
            If Not clsQualityCheck.chkIsGridColumnHasTag(gvParam) Then
                Throw New Exception(" Grid's Column is not being recognized, Please delete layout and try loading Document Again ")
            End If
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from tspl_quality_check where gate_entry_no='" & fndGateEntryNo.Value & "' and QC_NO <>'" & fndQcNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
           
            If clsCommon.GetDateWithStartTime(dtpQCInDateTime.Value) < clsCommon.GetDateWithStartTime(dtpWeighmentDate.Value) Then
                Throw New Exception("QC In Date can not be less than Weighment Date")
            End If

            If clsCommon.GetDateWithStartTime(dtpQCOutDateTime.Value) < clsCommon.GetDateWithStartTime(dtpWeighmentDate.Value) Then
                Throw New Exception("QC Out Date can not be less than Weighment Date")
            End If
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
            'obj.Item_Code = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value)
            'obj.Item_Desc = clsCommon.myCstr(gvItem.Rows(0).Cells(colItemDesc).Value)
            'obj.UOM = clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value)
            'obj.Qty_In_Kg = clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value)
            'obj.fat_per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFat).Value)
            'obj.snf_Per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNF).Value)
            'obj.snf_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value)
            'obj.fat_KG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value)
            'obj.Dip_Value = clsCommon.myCdbl(txtDipValue.Text)
            'obj.Receipt_Control_FAT = clsCommon.myCdbl(txtRcptControlSampleFAT.Text)
            'obj.Receipt_Control_SNF = clsCommon.myCdbl(txtRcptControlSampleSNF.Text)
            'obj.DeductionAmount = clsCommon.myCdbl(TxtDeductionAmount.Value)
            'obj.Adjust_fat_per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colAdjustFAT).Value)
            'obj.Adjust_snf_Per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colAdjustSNF).Value)
            'obj.Adjust_clr = clsCommon.myCdbl(gvItem.Rows(0).Cells(colAdjustCLR).Value)
            If Not isPost Then
                obj.isPosted = 0
            End If

            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            obj.Arr = New List(Of clsQualityChemberNoDetails)
            'For Each grow As GridViewRowInfo In gvItem.Rows
            '    Dim objTr As New clsQualityChemberNoDetails()
            '    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSLNo).Value)
            '    objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
            '    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
            '    objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
            '    objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
            '    objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
            '    objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
            '    objTr.MILK_GRADE_CODE = clsCommon.myCstr(grow.Cells(colMilkGradeCode).Value)
            '    objTr.MIKL_TYPE_CODE = clsCommon.myCstr(grow.Cells(colMilkTypeCode).Value)
            '    objTr.Adjust_fat_per = clsCommon.myCdbl(grow.Cells(colAdjustFAT).Value)
            '    objTr.Adjust_snf_Per = clsCommon.myCdbl(grow.Cells(colAdjustSNF).Value)
            '    objTr.Adjust_clr = clsCommon.myCdbl(grow.Cells(colAdjustCLR).Value)
            '    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
            '        obj.Arr.Add(objTr)
            '    End If

            'Next
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
                            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, trans)), "1") = CompairStringResult.Equal Then
                                objParam.BoilingDifference = clsCommon.myCdbl(gvParam.Rows(j).Cells("colDifference").Value)
                            End If
                            If clsCommon.CompairString(objParam.Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(objParam.Param_Type, "SNF") = CompairStringResult.Equal Then
                                objParam.Param_Field_Value = clsCommon.myFormat(clsCommon.myCstr(gvParam.Rows(j).Cells(i).Value))
                            End If

                            obj.arrQcParam.Add(objParam)
                        End If
                    Next
                Next
            End If

            If clsQualityCheck.saveData(obj, trans) Then
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
                btnPost.Enabled = True
                btnPrint.Enabled = True
                loadData(obj.QC_No, obj.Doc_Type, NavigatorType.Current)
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btnPrint.Enabled = False
            fndQcNo.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            blnSave = False

        End Try

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
            fndLocation.Enabled = False
            fndQcNo.Value = obj.QC_No
            fndGateEntryNo.Value = obj.Gate_Entry_No
            dtpQCInDateTime.Value = obj.QC_In_Date_Time
            dtpQCOutDateTime.Value = obj.QC_Out_Date_Time
            dtpGateEntryDateTime.Value = obj.Gate_Entry_Date_And_Time
            fndTankerNo.Value = obj.Tanker_No
            txtChallanNo.Text = obj.Challan_No
            dtpChallanDate.Value = obj.Challan_Date
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_weighment_detail where gate_entry_no='" & obj.Gate_Entry_No & "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtWeighmentNo.Text = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("Weighment_Date"), "dd/MM/yyyy")
                lblStatusValue.Text = "Done"
            Else
                lblStatusValue.Text = "Not Done"
            End If
            fndLocation.Value = obj.location_Code
            lblLocationName.Text = obj.Location_Desc

            If clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                fndVendor.Value = obj.Dispatched_From_Mcc_Code
                lblVendorName.Text = obj.Dispatched_From_Mcc_Desc
            Else
                fndVendor.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Desc
            End If
           
            txtDipValue.Text = obj.Dip_Value
            TxtDeductionAmount.Value = obj.DeductionAmount
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
                    'Dim ii As Integer = gvItem.Rows.Count
                    Dim intCount As Integer = 0

                    For i As Integer = 0 To obj.arrQcParam.Count - 1
                        Try
                            If obj.isPosted = 0 AndAlso (clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal) Then
                                If TypeOf (gvParam.Columns(obj.arrQcParam(i).Param_Field_Code)) Is GridViewDecimalColumn Then
                                    gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells(obj.arrQcParam(i).Param_Field_Code).Value = MyMath.RoundDown(clsCommon.myCdbl(obj.arrQcParam(i).Param_Field_Value), 2)
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
                            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                                gvParam.Rows(obj.arrQcParam(i).LINE_NO - 1).Cells("colDifference").Value = obj.arrQcParam(i).BoilingDifference
                            End If


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
                    If obj.is_QC_Separated = 1 Then
                        lblQcAcceptedOrRejected.Text = "QC Send For Separation"
                    End If
                ElseIf obj.is_Param_accepted = 1 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted"
                ElseIf obj.is_Param_accepted = 2 And obj.isPosted = 1 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted with special approval"
                ElseIf obj.is_Param_accepted = 2 And obj.isPosted = 0 Then
                    lblQcAcceptedOrRejected.Text = "QC accepted with special approval"
                Else
                    lblQcAcceptedOrRejected.Text = ""
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
                btnPost.Enabled = False
            End If
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
                    If clsCommon.CompairString(lblQcAcceptedOrRejected.Text, "QC accepted with special approval") <> CompairStringResult.Equal Then
                        'str = chkParameterRange()
                        If clsCommon.CompairString(str, "1") = CompairStringResult.Equal Then
                        ElseIf clsCommon.CompairString(str, "-1") = CompairStringResult.Equal Then
                            Throw New Exception("Some Of the Parameter Range Not Found in Master")
                        Else
                            If clsCommon.MyMessageBoxShow("Following Parameters Rejects The Milk, " & Environment.NewLine & str & Environment.NewLine & "Want To Continue Posting ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                                gvParam.Rows(0).Cells("colRemarks").Value = gvParam.Rows(0).Cells("colRemarks").Value & Environment.NewLine & "Rejection Remarks: " & str
                            Else
                                Exit Sub
                            End If
                        End If

                    Else
                        str = "2"
                    End If
                Else
                    str = "1"
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
                    If clsCommon.CompairString(str, "2") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery(" update tspl_quality_check set is_Param_Accepted=2 where qc_no='" & fndQcNo.Value & "'")
                        msg = "Successfully Posted"
                    Else
                        clsDBFuncationality.ExecuteNonQuery(" update tspl_quality_check set Remarks='" & strRemarks & "', is_Param_Accepted=" & IIf(clsCommon.CompairString(str, "1") = CompairStringResult.Equal, 1, 0) & " where qc_no='" & fndQcNo.Value & "'")
                        msg = "Successfully Posted"
                    End If
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
                loadData(fndQcNo.Value, strDocType, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub funPrint(ByVal strDocNo As String, Optional ByVal docType As String = Nothing)
        Dim PrintTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToPrintTimeWithDocumentDate, clsFixedParameterCode.AllowToPrintTimeWithDocumentDate, Nothing)
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
                Dim strQuery As String = Nothing
                strQuery = "select FINAL.*,tspl_company_master.Logo_Img,tspl_company_master.Logo_img2 from (select * from " & _
             " (" & _
             " select 1 as sn ,TSPL_QUALITY_CHECK.Qty_In_Kg,TSPL_QUALITY_CHECK.QC_NO,convert(varchar(15),tspl_quality_check.QC_In_Date_Time,103) as  Weighment_date,tspl_quality_check.QC_In_Date_Time,Tspl_Gate_Entry_Details.Date_And_Time as Ge_Date, " & _
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
            " select 2 as sn ,TSPL_QUALITY_CHECK.Qty_In_Kg,TSPL_QUALITY_CHECK.QC_NO,convert(varchar(15),tspl_quality_check.QC_In_Date_Time,103) as  Weighment_date,tspl_quality_check.QC_In_Date_Time,Tspl_Gate_Entry_Details.Date_And_Time as Ge_Date, " & _
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
              " select 3 as sn ,0 as Qty_In_Kg,TSPL_Quality_Chember_Details.QC_NO,null as  Weighment_date,null as QC_In_Date_Time,null as Ge_Date, " & _
              " '' as TANKER_NO,TSPL_Quality_Chember_Details.Chamber_Desc as CHAMBER_DESC ,'GRADE' as Param_Type,'' as Comp_Code,'' as Comp_Name,'' as Vendor_Code , " & _
              " '' as Vendor_Name , 'GRADE' AS Param_Field_Desc,TSPL_Quality_Chember_Details.MILK_GRADE_CODE AS Param_Field_Value from TSPL_Quality_Chember_Details " & _
              " where QC_No='" + strDocNo + "' " & _
             " )as Main pivot ( max(Param_Field_Value) for CHAMBER_DESC in ([F],[R],[M])) PIVT) as Final left join tspl_company_master on Final.Comp_Code=tspl_company_master.Comp_Code order by Sn "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
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
            myMessages.blankValue("Doc not found to Print")
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
                    If CInt(fracValue1) Mod settMODValueForFAT <> 0 Then
                        If Not isContractorJobWork Then
                            clsCommon.MyMessageBoxShow(Me, " FAT value in Grid, must have its decimal part multiple of 5", Me.Text)
                            gvParam.Rows(0).Cells(FATColName).Value = 0
                            gvParam.CurrentRow = gvParam.Rows(0)
                            gvParam.CurrentColumn = gvParam.Columns(FATColName)
                        End If
                    End If
                End If
                gvParam.Rows(0).Cells(FATColName).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(FATColName).Value)
            Else
                If clsCommon.myLen(FATColName) > 0 Then
                    fracValue1 = clsCommon.myCdbl(gvParam.CurrentRow.Cells(FATColName).Value)
                    fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                    If CInt(fracValue1) Mod settMODValueForFAT <> 0 Then
                        If Not isContractorJobWork Then
                            clsCommon.MyMessageBoxShow(Me, " FAT value in Grid, must have its decimal part multiple of 5", Me.Text)
                            gvParam.CurrentRow.Cells(FATColName).Value = 0
                            gvParam.CurrentRow = gvParam.Rows(0)
                            gvParam.CurrentColumn = gvParam.Columns(FATColName)
                        End If
                    End If
                End If
                gvParam.CurrentRow.Cells(FATColName).Value = clsCommon.myFormat(gvParam.CurrentRow.Cells(FATColName).Value)
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
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                If e.Column Is gvParam.Columns(colItemCode) Then
                    OpenICodeList(False)
                End If
                    If clsCommon.CompairString(gvParam.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gvParam.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Then
                        calculateSNF(e.RowIndex)
                        If FinalChamberwise = 0 Then
                            gvParam.Rows(0).Cells(FATColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(FATColName).Value, 2))
                            gvParam.Rows(0).Cells(SNFColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(SNFColName).Value, 2))
                        Else
                            gvParam.CurrentRow.Cells(FATColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.CurrentRow.Cells(FATColName).Value, 2))
                            gvParam.CurrentRow.Cells(SNFColName).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.CurrentRow.Cells(SNFColName).Value, 2))
                        End If

                    End If


                    isCellValueChangedOpen = False
                End If
        End If
    End Sub
    Sub getParameterValue()
        If (TankerFromMaster = 1 And chkBulkMilkProc.IsChecked = True) OrElse (MCCChamberwise = 1 AndAlso chkMccProc.IsChecked = True) Then
            Dim intCount As Integer = 0
            For intCount = 0 To gvParam.Rows.Count - 1
                gvParam.Rows(intCount).Cells(colSLNo).Value = intCount + 1

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
                    End If

                End If
            Next
        Else

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
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gvParam.CurrentRow.Cells(colItemCode).Value), "", isButtonClick, " Product_Type='MI' ", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gvParam.CurrentRow.Cells(colItemCode).Value = obj.Item_Code
            gvParam.CurrentRow.Cells(colItemDesc).Value = obj.Item_Desc
            gvParam.CurrentRow.Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(obj.Item_Code), Nothing)
        Else
            gvParam.CurrentRow.Cells(colItemCode).Value = ""
            gvParam.CurrentRow.Cells(colItemDesc).Value = ""
            gvParam.CurrentRow.Cells(colHSN).Value = ""
        End If
    End Sub
    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
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
            Dim obj1 As New clsGridLayout()
                obj1.ReportID = MyBase.Form_ID & "gvParam"
                obj1.UserID = objCommonVar.CurrentUserCode
                obj1.GridLayout = New MemoryStream()
                gvParam.SaveLayout(obj1.GridLayout)
                obj1.GridColumns = gvParam.ColumnCount
                obj1.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj1.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
                obj1.GridLayout.Close()
                obj1.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvParam", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
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
            clsCommon.MyMessageBoxShow(err.Message, "Restore layout")
        End Try
    End Sub

    Private Sub btnSendForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If clsCommon.myLen(fndQcNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select QC No. First", Me.Text)
                fndQcNo.Focus()
                Return
            End If

            Dim objApprov As New ClsTransactionApproval
            objApprov.Document_No = fndQcNo.Value
            objApprov.Doc_Date = clsCommon.GetPrintDate(dtpQCInDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            objApprov.Approval_Type = "Special Approval"
            objApprov.Approve = 0
            objApprov.Program_Code = Me.Form_ID
            Dim qryp As String = "select Program_Name a   from TSPL_PROGRAM_MASTER where  Program_Code ='" & Me.Form_ID & "'"
            objApprov.Screen_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryp))
            ClsTransactionApproval.SaveData(objApprov, True)
            clsCommon.MyMessageBoxShow(Me, "Document Sent For special Approval Successfully", Me.Text)
            loadData(fndQcNo.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating

        Dim whrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = " and  TSPL_Quality_Check.location_code in ( " & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        whrcls = whrcls & " and TSPL_QUALITY_CHECK.is_QC_Separated =1 and TSPL_QUALITY_CHECK.QC_No  not in( select QC_No  from TSPL_QC_separation)"

        Dim gtNo As String = fndTankerNo.Value
        Dim dt As DataRow
        dt = clsQCSeparation.getGateEntryFinder(gtNo, "TSPL_Quality_Check.isPosted='1' and TSPL_Quality_Check.Doc_Type='" & IIf(chkBulkMilkProc.IsChecked, "BulkProc", "MccProc") & "'  " & whrcls)
        If dt IsNot Nothing Then
            gtNo = dt("QC No")
        End If
        If clsCommon.myLen(gtNo) > 0 Then
            LoadQCData(gtNo)
        Else
            reset(False, False)
        End If
    End Sub
    Private Sub gvParam_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvParam.CurrentColumnChanged
        If gvParam.RowCount > 0 Then

            Dim intCurrRow As Integer = gvParam.CurrentRow.Index
            gvParam.CurrentRow.Cells(colSLNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvParam.Rows.Count - 1 Then
                gvParam.Rows.AddNew()
                gvParam.CurrentRow = gvParam.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvParam_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gvParam.UserAddedRow
        For i As Integer = 0 To gvParam.Rows.Count - 1
            gvParam.Rows(0).Cells(colSLNo).Value = 1
            If i <> 0 Then
                gvParam.Rows(i).Cells(colSLNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gvParam_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvParam.UserDeletedRow
        For ii As Integer = 1 To gvParam.Rows.Count
            gvParam.Rows(ii - 1).Cells(colSLNo).Value = ii
        Next
    End Sub

    Private Sub gvParam_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvParam.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable("select top 1 * from TSPL_THIRD_PARTY_RECEIPT_ENTRY where isnull(Against_Receipt_No,'')=''", trans)
            If dtReceipt IsNot Nothing AndAlso dtReceipt.Rows.Count > 0 Then
                For Each dr As DataRow In dtReceipt.Rows

                    Dim obj As New clsRcptEntryHeader()
                    'obj.Receipt_No = clsCommon.myCstr(row.Cells("gvDocNo").Value)

                    obj.Receipt_Date = clsCommon.myCDate(dr("Transaction_Date"))
                    obj.Receipt_Post_Date = obj.Receipt_Date
                    Dim strmodeCount As Double = 0
                    Dim strbankCount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(bank_Code) from TSPL_BANK_MASTER where bank_Code='" & clsCommon.myCstr(dr("Doc_From")) & "'", trans))
                    If clsCommon.myLen(strbankCount) <= 0 Then
                        Throw New Exception("Invalid Bank Code " & clsCommon.myCstr(dr("Doc_From")))
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Doc_From")), "Axis") = CompairStringResult.Equal Then
                        obj.Bank_Code = "Axis"
                        If clsCommon.CompairString(clsCommon.myCstr(dr("pmode")), "I") = CompairStringResult.Equal Then
                            strmodeCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(Payment_Code) from TSPL_PAYMENT_CODE where Payment_Code ='IMPS'", trans))
                            If clsCommon.myLen(strmodeCount) <= 0 Then
                                Throw New Exception("Invalid Payment Mode IMPS")
                            End If
                            obj.Payment_Code = "IMPS"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("pmode")), "N") = CompairStringResult.Equal Then
                            strmodeCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(Payment_Code) from TSPL_PAYMENT_CODE where Payment_Code ='NEFT'", trans))
                            If clsCommon.myLen(strmodeCount) <= 0 Then
                                Throw New Exception("Invalid Payment Mode NEFT")
                            End If
                            obj.Payment_Code = "NEFT"
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Doc_From")), "Esewa") = CompairStringResult.Equal Then
                        obj.Bank_Code = "Esewa"
                        strmodeCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(Payment_Code) from TSPL_PAYMENT_CODE where Payment_Code ='CASH'", trans))
                        If clsCommon.myLen(strmodeCount) <= 0 Then
                            Throw New Exception("Invalid Payment Mode CASH")
                        End If
                        obj.Payment_Code = "CASH"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("Doc_From")), "Twallet") = CompairStringResult.Equal Then
                        obj.Bank_Code = "Twallet"
                        strmodeCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(Payment_Code) from TSPL_PAYMENT_CODE where Payment_Code ='CASH'", trans))
                        If clsCommon.myLen(strmodeCount) <= 0 Then
                            Throw New Exception("Invalid Payment Mode CASH")
                        End If
                        obj.Payment_Code = "CASH"
                    End If

                    obj.Receipt_Type = "O"

                    'obj.Location_GL_Code = txtRPLocation.Value
                    obj.CFormRecd = "0"
                    obj.CForm_InvoiceNo = ""
                    obj.Cheque_No = ""
                    obj.Cheque_Date = Nothing

                    obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    obj.Customer_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("sELECT customer_name from tspl_customer_master where Cust_Code='" & clsCommon.myCstr(dr("Cust_Code")) & "'", trans))
                    obj.Entry_Desc = "Receipt Entry created against " & clsCommon.myCstr(dr("Doc_From")) & " for customer " & obj.Customer_Name & " "
                    obj.Receipt_Amount = clsCommon.myCdbl(dr("Txn_amnt"))
                    obj.Balance_Amt = obj.Receipt_Amount
                    obj.UnApply_Amt = obj.Receipt_Amount
                    obj.FIFO_Balance = obj.Receipt_Amount
                    obj.RECEIVED_AMOUNT_BASE_CURRENCY = obj.Receipt_Amount
                    obj.IsSalesmanType = "N"
                    obj.SecurityDeposit = "N"
                    obj.IsRecoCleared = "N"
                    obj.IsChkReverse = "N"
                    obj.ConvRate = 1
                    obj.ConvRateOld = 1
                    obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + obj.Bank_Code + "'", trans))
                    If clsCommon.myLen(obj.Cust_Code) > 0 Then
                        Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_Customer_MASTER where Cust_CODE='" & clsCommon.myCstr(obj.Cust_Code) & "'", trans))
                        If clsCommon.myLen(VenCurr) > 0 Then
                            obj.CURRENCY_CODE = VenCurr
                        Else
                            obj.CURRENCY_CODE = "INR"
                        End If
                    Else
                        obj.CURRENCY_CODE = "INR"
                    End If
                    obj.Narration = obj.Entry_Desc
                    obj.SaveData(obj, True, trans)
                    clsRcptEntryHeader.funRcptPost(obj.Receipt_No, trans)
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_THIRD_PARTY_RECEIPT_ENTRY set Against_Receipt_No ='" & obj.Receipt_No & "' where Doc_From='" & clsCommon.myCstr(dr("Doc_From")) & "' and Doc_No='" & clsCommon.myCstr(dr("Doc_No")) & "' ", trans)
                    trans.Commit()
                Next
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
